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
    /// Interaction logic for frm_ReorderReportDlg.xaml
    /// </summary>
    public partial class frm_ReorderReportDlg : Window
    {
        public frm_ReorderReportDlg()
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
            dtblOrderBy.Rows.Add(new object[] { "3", "Vendor Part #" });
            lkupOrderBy.ItemsSource = dtblOrderBy;
            lkupOrderBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private void PopulateGroupBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "0", "Vendor" });
            dtblOrderBy.Rows.Add(new object[] { "1", "Department" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Category" });
            lkupGroupBy.ItemsSource = dtblOrderBy;
            lkupGroupBy.EditValue = "0";
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
            dtblGrp.Columns.Add("CheckFlag", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false, strip });
            }
            grdReorder.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dtbl = objVendor.FetchData();
            DataTable dtblCls = new DataTable();
            dtblCls.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("CheckFlag", System.Type.GetType("System.Boolean"));
            dtblCls.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblCls.Rows.Add(new object[] { dr["ID"].ToString(), dr["Name"].ToString(), false, strip });
            }
            grdReorder.ItemsSource = dtblCls;
            dtbl.Dispose();
            dtblCls.Dispose();
        }

        private void PopulateCategory()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dtbl = objCategory.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckFlag", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false, strip });
            }
            grdReorder.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Reorder Report";
            PopulateGroupBy();
            PopulateOrderBy();
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

        private void Chkgrid_Checked(object sender, RoutedEventArgs e)
        {
            if (chkgrid.IsChecked == true)
            {
                chkgrid.Content = "Uncheck All";
                DataTable dtbl = grdReorder.ItemsSource as DataTable;
                if (dtbl == null) return;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckFlag"] = true;
                }
                grdReorder.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkgrid.Content = "Check All";
                DataTable dtbl1 = grdReorder.ItemsSource as DataTable;
                if (dtbl1 == null) return;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckFlag"] = false;
                }
                grdReorder.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                grdReorder.SetCellValue(e.RowHandle, colCheck, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdReorder.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckFlag"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdReorder.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkgrid.IsChecked = true;
                    chkgrid.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkgrid.IsChecked = false;
                    chkgrid.Content = "Check All";
                }
                dtbl.Dispose();
            }
        }

        private void LkupGroupBy_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (lkupGroupBy.EditValue.ToString() == "0")
            {
                colDesc.Header = "Vendor";
                PopulateVendor();
                chkoption1.Visibility = Visibility.Visible;
                chkoption4.Visibility = Visibility.Hidden;
                chkoption3.Content = "Page break on Vendor";
            }
            if (lkupGroupBy.EditValue.ToString() == "1")
            {
                colDesc.Header = "Department";
                PopulateDepartment();
                chkoption1.Visibility = Visibility.Hidden;
                chkoption4.Visibility = Visibility.Hidden;
                chkoption3.Content = "Page break on Department";
            }
            if (lkupGroupBy.EditValue.ToString() == "2")
            {
                colDesc.Header = "Category";
                PopulateCategory();
                chkoption1.Visibility = Visibility.Hidden;
                chkoption3.Content = "Page break on Category";
                chkoption4.Visibility = Visibility.Visible;
            }
            chkgrid.IsChecked = false;
            chkgrid.IsChecked = true;
        }

        private void ExecuteReport(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }
            string strCriteria1 = "";
            string strCriteria2 = "";
            string strCriteria3 = "";
            if (chkoption1.IsChecked == true) strCriteria1 = "Y";
            if (chkoption2.IsChecked == true) strCriteria2 = "Y";
            if (chkoption3.IsChecked == true) strCriteria3 = "Y";


            DataTable dtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dtbl = objProduct.FetchDataForReorderReport(lkupGroupBy.EditValue.ToString(), strCriteria1, strCriteria2,
                grdReorder.ItemsSource as DataTable, lkupOrderBy.EditValue.ToString(), (lkupGroupBy.EditValue.ToString() == "2") ? (chkoption4.IsChecked == true ?true: false) : false);
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.repReorder rep_Reorder = new OfflineRetailV2.Report.repReorder();
            GeneralFunctions.MakeReportWatermark(rep_Reorder);
            rep_Reorder.DecimalPlace = Settings.DecimalPlace;
            rep_Reorder.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Reorder.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Reorder.Report.DataSource = dtbl;

            if (lkupGroupBy.EditValue.ToString() == "0") // Vendor
            {
                rep_Reorder.GroupHeader1.GroupFields.Add(rep_Reorder.CreateGroupField("VendorID"));
                rep_Reorder.rGroupDescCaption.Text = "Vendor : ";
                rep_Reorder.rGroupDesc.DataBindings.Add("Text", dtbl, "Vendor");
                rep_Reorder.GroupType = "V";
                rep_Reorder.rGroupHeader1.Text = "Department";
                rep_Reorder.rGroupHeader2.Text = "Category";
                rep_Reorder.rVDC1.DataBindings.Add("Text", dtbl, "Department");
                rep_Reorder.rVDC2.DataBindings.Add("Text", dtbl, "Category");

                rep_Reorder.rVCaption1.Text = "Contact : ";
                rep_Reorder.rVCaption2.Text = "Phone : ";
                rep_Reorder.rVCaption3.Text = "Fax : ";
                rep_Reorder.rContact.DataBindings.Add("Text", dtbl, "VContact");
                rep_Reorder.rPhone.DataBindings.Add("Text", dtbl, "VPhone");
                rep_Reorder.rFax.DataBindings.Add("Text", dtbl, "VFax");
            }

            if (lkupGroupBy.EditValue.ToString() == "1") // Department
            {
                rep_Reorder.GroupHeader1.GroupFields.Add(rep_Reorder.CreateGroupField("DepartmentID"));
                rep_Reorder.rGroupDescCaption.Text = "Department : ";
                rep_Reorder.rGroupDesc.DataBindings.Add("Text", dtbl, "Department");
                rep_Reorder.GroupType = "D";
                rep_Reorder.rGroupHeader1.Text = "Vendor";
                rep_Reorder.rGroupHeader2.Text = "Category";
                rep_Reorder.rVDC1.DataBindings.Add("Text", dtbl, "Vendor");
                rep_Reorder.rVDC2.DataBindings.Add("Text", dtbl, "Category");

                rep_Reorder.rVCaption1.Text = "";
                rep_Reorder.rVCaption2.Text = "";
                rep_Reorder.rVCaption3.Text = "";
                rep_Reorder.rContact.Text = "";
                rep_Reorder.rPhone.Text = "";
                rep_Reorder.rFax.Text = "";
            }

            if (lkupGroupBy.EditValue.ToString() == "2") // Category
            {
                rep_Reorder.GroupHeader1.GroupFields.Add(rep_Reorder.CreateGroupField("CategoryID"));
                rep_Reorder.rGroupDescCaption.Text = "Category : ";
                rep_Reorder.rGroupDesc.DataBindings.Add("Text", dtbl, "Category");
                rep_Reorder.GroupType = "C";
                rep_Reorder.rGroupHeader1.Text = "Vendor";
                rep_Reorder.rGroupHeader2.Text = "Department";
                rep_Reorder.rVDC1.DataBindings.Add("Text", dtbl, "Vendor");
                rep_Reorder.rVDC2.DataBindings.Add("Text", dtbl, "Department");
                rep_Reorder.rVCaption1.Text = "";
                rep_Reorder.rVCaption2.Text = "";
                rep_Reorder.rVCaption3.Text = "";
                rep_Reorder.rContact.Text = "";
                rep_Reorder.rPhone.Text = "";
                rep_Reorder.rFax.Text = "";
            }
            if (strCriteria1 == "Y") rep_Reorder.rCheck1.Checked = true;
            if (strCriteria2 == "Y") rep_Reorder.rCheck2.Checked = true;
            if (strCriteria3 == "Y") rep_Reorder.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;

            rep_Reorder.rSKU.DataBindings.Add("Text", dtbl, "SKU");
            rep_Reorder.rPart.DataBindings.Add("Text", dtbl, "VendorPartNumber");
            rep_Reorder.rProduct.DataBindings.Add("Text", dtbl, "ProductDesc");

            rep_Reorder.rHQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_Reorder.rRQty.DataBindings.Add("Text", dtbl, "ReorderQty");
            rep_Reorder.rNQty.DataBindings.Add("Text", dtbl, "NormalQty");
            rep_Reorder.rCost.DataBindings.Add("Text", dtbl, "UnitCost");
            rep_Reorder.rExtCost.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_Reorder.rSQty.DataBindings.Add("Text", dtbl, "SuggOrderQty");
            rep_Reorder.rDP.DataBindings.Add("Text", dtbl, "DP");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_Reorder.PrinterName = Settings.ReportPrinterName;
                    rep_Reorder.CreateDocument();
                    rep_Reorder.PrintingSystem.ShowMarginsWarning = false;
                    rep_Reorder.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_Reorder.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_Reorder;
                    window.ShowDialog();

                }
                finally
                {
                    rep_Reorder.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_Reorder.CreateDocument();
                rep_Reorder.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_Reorder);
                }
                finally
                {
                    rep_Reorder.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_Reorder.CreateDocument();
                rep_Reorder.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "reorder.pdf";
                    GeneralFunctions.EmailReport(rep_Reorder, attachfile, "Reorder Item");
                }
                finally
                {
                    rep_Reorder.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                int i = 0;
                DataTable dG = grdReorder.ItemsSource as DataTable;
                foreach (DataRow drG in dG.Rows)
                {
                    if (Convert.ToBoolean(drG["CheckFlag"].ToString()))
                    {
                        i++;
                    }
                }
                if (i > 0) ExecuteReport(eventtype);
                else
                {
                    if (lkupGroupBy.EditValue.ToString() == "0")
                        DocMessage.MsgInformation("Please select atleast 1 vendor");
                    if (lkupGroupBy.EditValue.ToString() == "1")
                        DocMessage.MsgInformation("Please select atleast 1 department");
                    if (lkupGroupBy.EditValue.ToString() == "2")
                        DocMessage.MsgInformation("Please select atleast 1 category");
                    GeneralFunctions.SetFocus(grdReorder);
                    return;
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void LkupGroupBy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
