using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ProductTaxPriceReportDlg.xaml
    /// </summary>
    public partial class frm_ProductTaxPriceReportDlg : Window
    {
        public frm_ProductTaxPriceReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateOrderBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "1", "SKU" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Description" });
            dtblOrderBy.Rows.Add(new object[] { "3", "Department" });
            lkupOrderBy.ItemsSource = dtblOrderBy;
            lkupOrderBy.DisplayMember = "Desc";
            lkupOrderBy.ValueMember = "ID";
            lkupOrderBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private void PopulateDepartment()
        {
            PosDataObject.Department objDept = new PosDataObject.Department();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckDepartment", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblGrp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdGroup.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Product Report - Tax, Price, Food Stamp";
            PopulateOrderBy();
            PopulateDepartment();
            chkGroup.IsChecked = true;
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void ExecuteGroupReport(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            DataTable dtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dtbl = objProduct.FetchDataForPriceTaxReport(grdGroup.ItemsSource as DataTable, lkupOrderBy.EditValue.ToString());

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("PriceA", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("PriceB", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("PriceC", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("FS", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Department", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Tax", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl.Rows)
            {
                dtbl1.Rows.Add(new object[] {      dr["ID"].ToString(),
                                                   dr["SKU"].ToString(),
                                                   dr["ProductDesc"].ToString(),
                                                   dr["PriceA"].ToString(),
                                                   dr["PriceB"].ToString(),
                                                   dr["PriceC"].ToString(),
                                                   dr["FS"].ToString(),
                                                   dr["DepartmentID"].ToString(),
                                                   dr["Department"].ToString(),
                                                   dr["DecimalPlace"].ToString(),
                                                   TaxString(GeneralFunctions.fnInt32(dr["ID"].ToString()))});
            }


            OfflineRetailV2.Report.Product.repProductPriceTax rep_ProductPriceTax = new OfflineRetailV2.Report.Product.repProductPriceTax();

            rep_ProductPriceTax.Report.DataSource = dtbl1;
            GeneralFunctions.MakeReportWatermark(rep_ProductPriceTax);
            rep_ProductPriceTax.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_ProductPriceTax.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_ProductPriceTax.DecimalPlace = Settings.DecimalPlace;

            rep_ProductPriceTax.GroupHeader1.GroupFields.Add(rep_ProductPriceTax.CreateGroupField("DepartmentID"));
            rep_ProductPriceTax.rGroupIDCaption.Text = "Department ID : ";
            rep_ProductPriceTax.rGroupDescCaption.Text = "Department : ";
            rep_ProductPriceTax.rGroupID.DataBindings.Add("Text", dtbl1, "DepartmentID");
            rep_ProductPriceTax.rGroupDesc.DataBindings.Add("Text", dtbl1, "Department");


            rep_ProductPriceTax.rDP.DataBindings.Add("Text", dtbl1, "DecimalPlace");
            rep_ProductPriceTax.rSKU.DataBindings.Add("Text", dtbl1, "SKU");
            rep_ProductPriceTax.rProduct.DataBindings.Add("Text", dtbl1, "ProductDesc");
            rep_ProductPriceTax.rTax.DataBindings.Add("Text", dtbl1, "Tax");
            rep_ProductPriceTax.rPriceA.DataBindings.Add("Text", dtbl1, "PriceA");
            rep_ProductPriceTax.rPriceB.DataBindings.Add("Text", dtbl1, "PriceB");
            rep_ProductPriceTax.rPriceC.DataBindings.Add("Text", dtbl1, "PriceC");
            rep_ProductPriceTax.rFS.DataBindings.Add("Text", dtbl1, "FS");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_ProductPriceTax.PrinterName = Settings.ReportPrinterName;
                    rep_ProductPriceTax.CreateDocument();
                    rep_ProductPriceTax.PrintingSystem.ShowMarginsWarning = false;
                    rep_ProductPriceTax.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_ProductPriceTax.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_ProductPriceTax;
                    window.ShowDialog();

                }
                finally
                {
                    rep_ProductPriceTax.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_ProductPriceTax.CreateDocument();
                rep_ProductPriceTax.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_ProductPriceTax);
                }
                finally
                {
                    rep_ProductPriceTax.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_ProductPriceTax.CreateDocument();
                rep_ProductPriceTax.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "prod_price_tax.pdf";
                    GeneralFunctions.EmailReport(rep_ProductPriceTax, attachfile, "Product Price, Tax");
                }
                finally
                {
                    rep_ProductPriceTax.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
                }
            }

        }

        private string TaxString(int PID)
        {
            string strtx = "";
            DataTable dtbl = new DataTable();
            PosDataObject.Product objP = new PosDataObject.Product();
            objP.Connection = SystemVariables.Conn;
            dtbl = objP.ShowProductTaxes(PID);
            if (dtbl.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (strtx == "") strtx = dr["TaxName"].ToString();
                    else strtx = strtx + ", " + dr["TaxName"].ToString();
                }
            }

            return strtx;
        }

        private void ClickButton(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                int i = 0;
                DataTable dG = grdGroup.ItemsSource as DataTable;

                foreach (DataRow drG in dG.Rows)
                {
                    if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                    {
                        i++;
                    }
                }
                dG.Dispose();
                ExecuteGroupReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ChkGroup_Checked(object sender, RoutedEventArgs e)
        {
            if (chkGroup.IsChecked == true)
            {
                chkGroup.Content = "Uncheck All";
                DataTable dtbl = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckDepartment"] = true;
                }
                grdGroup.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkGroup.Content = "Check All";
                DataTable dtbl1 = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckDepartment"] = false;
                }
                grdGroup.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGroup")
            {
                grdGroup.SetCellValue(e.RowHandle, colCheckGroup, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckDepartment"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdGroup.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkGroup.IsChecked = true;
                    chkGroup.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkGroup.IsChecked = false;
                    chkGroup.Content = "Check All";
                }
                dtbl.Dispose();
            }
        }

        private void LkupOrderBy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
