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
    /// Interaction logic for frm_KitReportDlg.xaml
    /// </summary>
    public partial class frm_KitReportDlg : Window
    {
        public frm_KitReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
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
                if (i == 0)
                {
                    DocMessage.MsgInformation("Please check atleast 1 department.");
                    return;
                }
                ExecuteGroupReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
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
            dtbl = objProduct.FetchDataForKitProductReport(grdGroup.ItemsSource as DataTable);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Product.repKitReport rep_KitReport = new OfflineRetailV2.Report.Product.repKitReport();

            GeneralFunctions.MakeReportWatermark(rep_KitReport);
            rep_KitReport.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_KitReport.rReportHeader.Text = Settings.ReportHeader_Address;

            rep_KitReport.DecimalPlace = Settings.DecimalPlace;
            if (Settings.DecimalPlace == 3)
            {
                rep_KitReport.rTot2.Summary.FormatString = "{0:0.000}";
                rep_KitReport.rTot3.Summary.FormatString = "{0:0.000}";
                rep_KitReport.rRTot2.Summary.FormatString = "{0:0.000}";
                rep_KitReport.rRTot3.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_KitReport.rTot2.Summary.FormatString = "{0:0.00}";
                rep_KitReport.rTot3.Summary.FormatString = "{0:0.00}";
                rep_KitReport.rRTot2.Summary.FormatString = "{0:0.00}";
                rep_KitReport.rRTot3.Summary.FormatString = "{0:0.00}";
            }

            DataTable p = new DataTable("Parent");
            p.Columns.Add("ID", System.Type.GetType("System.String"));
            p.Columns.Add("SKU", System.Type.GetType("System.String"));
            p.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
            p.Columns.Add("ProductType", System.Type.GetType("System.String"));
            p.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
            p.Columns.Add("UnitCost", System.Type.GetType("System.String"));
            p.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
            p.Columns.Add("ExtCost", System.Type.GetType("System.String"));
            p.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
            p.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
            p.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
            p.Columns.Add("Department", System.Type.GetType("System.String"));
            p.Columns.Add("VendorID", System.Type.GetType("System.String"));
            p.Columns.Add("Vendor", System.Type.GetType("System.String"));
            p.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
            p.Columns.Add("DCost", System.Type.GetType("System.String"));
            foreach (DataRow dr in dtbl.Rows)
            {
                DataRow r1 = p.NewRow();
                r1["ID"] = dr["ID"].ToString();
                r1["SKU"] = dr["SKU"].ToString();
                r1["ProductDesc"] = dr["ProductDesc"].ToString();
                r1["ProductType"] = dr["ProductType"].ToString();
                r1["QtyOnHand"] = dr["QtyOnHand"].ToString();
                r1["UnitCost"] = dr["UnitCost"].ToString();
                r1["UnitRetailValue"] = dr["UnitRetailValue"].ToString();
                r1["ExtCost"] = dr["ExtCost"].ToString();
                r1["ExtRetailValue"] = dr["ExtRetailValue"].ToString();
                r1["CurrentMargin"] = dr["CurrentMargin"].ToString();
                r1["DepartmentID"] = dr["DepartmentID"].ToString();
                r1["Department"] = dr["Department"].ToString();
                r1["VendorID"] = dr["VendorID"].ToString();
                r1["Vendor"] = dr["Vendor"].ToString();
                r1["DecimalPlace"] = dr["DecimalPlace"].ToString();
                r1["DCost"] = dr["DCost"].ToString();

                p.Rows.Add(r1);
            }

            DataTable dtbl1 = new DataTable();
            PosDataObject.Product objProduct1 = new PosDataObject.Product();
            objProduct1.Connection = SystemVariables.Conn;
            dtbl1 = objProduct1.FetchKitItem(grdGroup.ItemsSource as DataTable);

            DataTable c = new DataTable("Child");

            c.Columns.Add("ID", System.Type.GetType("System.String"));
            c.Columns.Add("SKU", System.Type.GetType("System.String"));
            c.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
            c.Columns.Add("ProductType", System.Type.GetType("System.String"));
            c.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
            c.Columns.Add("UnitCost", System.Type.GetType("System.String"));
            c.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
            c.Columns.Add("ExtCost", System.Type.GetType("System.String"));
            c.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
            c.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
            c.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
            c.Columns.Add("Department", System.Type.GetType("System.String"));
            c.Columns.Add("VendorID", System.Type.GetType("System.String"));
            c.Columns.Add("Vendor", System.Type.GetType("System.String"));
            c.Columns.Add("KitID", System.Type.GetType("System.String"));
            c.Columns.Add("KitSKU", System.Type.GetType("System.String"));
            c.Columns.Add("KitDesc", System.Type.GetType("System.String"));
            c.Columns.Add("ItemCount", System.Type.GetType("System.String"));
            c.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
            c.Columns.Add("DCost", System.Type.GetType("System.String"));
            foreach (DataRow dr in dtbl1.Rows)
            {
                DataRow r1 = c.NewRow();
                r1["ID"] = dr["ID"].ToString();
                r1["SKU"] = dr["SKU"].ToString();
                r1["ProductDesc"] = dr["ProductDesc"].ToString();
                r1["ProductType"] = dr["ProductType"].ToString();
                r1["QtyOnHand"] = dr["QtyOnHand"].ToString();
                r1["UnitCost"] = dr["UnitCost"].ToString();
                r1["UnitRetailValue"] = dr["UnitRetailValue"].ToString();
                r1["ExtCost"] = dr["ExtCost"].ToString();
                r1["ExtRetailValue"] = dr["ExtRetailValue"].ToString();
                r1["CurrentMargin"] = dr["CurrentMargin"].ToString();
                r1["DepartmentID"] = dr["DepartmentID"].ToString();
                r1["Department"] = dr["Department"].ToString();
                r1["VendorID"] = dr["VendorID"].ToString();
                r1["Vendor"] = dr["Vendor"].ToString();
                r1["KitID"] = dr["KitID"].ToString();
                r1["KitSKU"] = dr["KitSKU"].ToString();
                r1["KitDesc"] = dr["KitDesc"].ToString();
                r1["ItemCount"] = dr["ItemCount"].ToString();
                r1["DecimalPlace"] = dr["DecimalPlace"].ToString();
                r1["DCost"] = dr["DCost"].ToString();
                c.Rows.Add(r1);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(p);
            ds.Tables.Add(c);

            DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["ID"],
            ds.Tables["Child"].Columns["KitID"]);
            //relation.Nested = true;
            ds.Relations.Add(relation);


            rep_KitReport.GroupHeader1.GroupFields.Add(rep_KitReport.CreateGroupField("DepartmentID"));
            rep_KitReport.DataSource = ds;
            //rep_KitReport.DetailReport.DataSource = dtbl;


            rep_KitReport.rGroupIDCaption.Text = "Department ID : ";
            rep_KitReport.rGroupDescCaption.Text = "Department : ";
            //rep_KitReport.rGroupID.DataBindings.Add("Text", ds, "DepartmentID");
            //rep_KitReport.rGroupDesc.DataBindings.Add("Text", ds, "Department");


            //rep_KitReport.rSKU.DataBindings.Add("Text", ds, "SKU");
            //rep_KitReport.rProduct.DataBindings.Add("Text", ds, "ProductDesc");
            //rep_KitReport.rType.DataBindings.Add("Text", ds, "ProductType");
            //rep_KitReport.rVendor.DataBindings.Add("Text", ds, "Vendor");
            //rep_KitReport.rQty.DataBindings.Add("Text", ds, "QtyOnHand");
            //rep_KitReport.rUnitCost.DataBindings.Add("Text", ds, "UnitCost");
            //rep_KitReport.rUnitRetailCost.DataBindings.Add("Text", ds, "UnitRetailValue");
            //rep_KitReport.rExtCost.DataBindings.Add("Text", ds, "ExtCost");
            //rep_KitReport.rExtRetail.DataBindings.Add("Text", ds, "ExtRetailValue");
            //rep_KitReport.rMargin.DataBindings.Add("Text", ds, "CurrentMargin");

            //rep_KitReport.rTot1.DataBindings.Add("Text", dtbl, "QtyOnHand");
            //rep_KitReport.rTot2.DataBindings.Add("Text", dtbl, "ExtCost");
            //rep_KitReport.rTot3.DataBindings.Add("Text", dtbl, "ExtRetailValue");

            //rep_KitReport.rkSKU.DataBindings.Add("Text", ds, "KitSKU");
            //rep_KitReport.rkDesc.DataBindings.Add("Text", ds, "KitDesc");
            //rep_KitReport.rkCount.DataBindings.Add("Text", ds, "ItemCount");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_KitReport.PrinterName = Settings.ReportPrinterName;
                    rep_KitReport.CreateDocument();
                    rep_KitReport.PrintingSystem.ShowMarginsWarning = false;
                    rep_KitReport.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_KitReport.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_KitReport;
                    window.ShowDialog();
                }
                finally
                {
                    rep_KitReport.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_KitReport.CreateDocument();
                rep_KitReport.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_KitReport);
                }
                finally
                {
                    rep_KitReport.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_KitReport.CreateDocument();
                rep_KitReport.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "prod_kit.pdf";
                    GeneralFunctions.EmailReport(rep_KitReport, attachfile, "Kit Products");
                }
                finally
                {
                    rep_KitReport.Dispose();
                    dtbl.Dispose();
                }
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateDepartment();
            chkGroup.IsChecked = true;
            Title.Text = "Kit Product Report";
        }

        private void BtnEmail_Loaded(object sender, RoutedEventArgs e)
        {

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

        private void GridView1_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {

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
    }
}
