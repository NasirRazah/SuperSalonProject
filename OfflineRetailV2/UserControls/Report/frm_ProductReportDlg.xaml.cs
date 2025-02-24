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
    /// Interaction logic for frm_ProductReportDlg.xaml
    /// </summary>
    public partial class frm_ProductReportDlg : Window
    {
        public frm_ProductReportDlg()
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
            dtblOrderBy.Rows.Add(new object[] { "4", "Vendor" });
            dtblOrderBy.Rows.Add(new object[] { "5", "POS Screen Category" });
            lkupOrderBy.ItemsSource = dtblOrderBy;
            lkupOrderBy.DisplayMember = "Desc";
            lkupOrderBy.ValueMember = "ID";
            lkupOrderBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private void PopulateGroupBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "0", "None" });
            dtblOrderBy.Rows.Add(new object[] { "1", "Department" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Vendor" });
            dtblOrderBy.Rows.Add(new object[] { "3", "POS Screen Category" });
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

        private void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dtbl = objVendor.FetchData();
            DataTable dtblCls = new DataTable();
            dtblCls.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("CheckVendor", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblCls.Rows.Add(new object[] { dr["ID"].ToString(), dr["Name"].ToString(), false });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblCls.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdClass.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblCls.Dispose();
            dtblTemp.Dispose();
        }

        private void PopulatePOSScreenCategory()
        {
            PosDataObject.Category objDept = new PosDataObject.Category();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckCategory", System.Type.GetType("System.Boolean"));
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

            grdcat.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Product Report";
            PopulateOrderBy();
            PopulateGroupBy();
            PopulateDepartment();
            PopulatePOSScreenCategory();
            PopulateVendor();
            chkGroup.IsChecked = true;
            chkClass.IsChecked = true;
            chkCat.IsChecked = true;
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
            dtbl = objProduct.FetchDataForGeneralReport(txtSKU.Text.Trim(), txtProduct.Text.Trim(),
                               grdGroup.ItemsSource as DataTable, grdClass.ItemsSource as DataTable, grdcat.ItemsSource as DataTable,
                               lkupOrderBy.EditValue.ToString(), lkupGroupBy.EditValue.ToString());

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Product.repProductGeneral_group rep_ProductGeneral_group = new OfflineRetailV2.Report.Product.repProductGeneral_group();

            rep_ProductGeneral_group.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_ProductGeneral_group);
            rep_ProductGeneral_group.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_ProductGeneral_group.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_ProductGeneral_group.DecimalPlace = Settings.DecimalPlace;
            if (Settings.DecimalPlace == 3)
            {
                rep_ProductGeneral_group.rTot2.Summary.FormatString = "{0:0.000}";
                rep_ProductGeneral_group.rTot3.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_ProductGeneral_group.rTot2.Summary.FormatString = "{0:0.00}";
                rep_ProductGeneral_group.rTot3.Summary.FormatString = "{0:0.00}";
            }

            if (lkupGroupBy.EditValue.ToString() == "1") // Department
            {
                rep_ProductGeneral_group.GroupHeader1.GroupFields.Add(rep_ProductGeneral_group.CreateGroupField("DepartmentID"));
                rep_ProductGeneral_group.rGroupIDCaption.Text = "Department ID" + " : ";
                rep_ProductGeneral_group.rGroupDescCaption.Text = "Department" + " : ";
                rep_ProductGeneral_group.rGroupID.DataBindings.Add("Text", dtbl, "DepartmentID");
                rep_ProductGeneral_group.rGroupDesc.DataBindings.Add("Text", dtbl, "Department");
                rep_ProductGeneral_group.GroupType = "D";

                rep_ProductGeneral_group.rH1.Text = "Primary Vendor";
                rep_ProductGeneral_group.rH2.Text = "POS Screen Category";
                rep_ProductGeneral_group.rV1.DataBindings.Add("Text", dtbl, "Vendor");
                rep_ProductGeneral_group.rV2.DataBindings.Add("Text", dtbl, "Category");
            }

            if (lkupGroupBy.EditValue.ToString() == "2") // Vendor
            {
                rep_ProductGeneral_group.GroupHeader1.GroupFields.Add(rep_ProductGeneral_group.CreateGroupField("VendorID"));
                rep_ProductGeneral_group.rGroupIDCaption.Text = "Vendor ID" + " : ";
                rep_ProductGeneral_group.rGroupDescCaption.Text = "Vendor" + " : ";
                rep_ProductGeneral_group.rGroupID.DataBindings.Add("Text", dtbl, "VendorID");
                rep_ProductGeneral_group.rGroupDesc.DataBindings.Add("Text", dtbl, "Vendor");
                rep_ProductGeneral_group.GroupType = "V";

                rep_ProductGeneral_group.rH1.Text = "Department";
                rep_ProductGeneral_group.rH2.Text = "POS Screen Category";
                rep_ProductGeneral_group.rV1.DataBindings.Add("Text", dtbl, "Department");
                rep_ProductGeneral_group.rV2.DataBindings.Add("Text", dtbl, "Category");
            }

            if (lkupGroupBy.EditValue.ToString() == "3") // POS Screen Categort
            {
                rep_ProductGeneral_group.GroupHeader1.GroupFields.Add(rep_ProductGeneral_group.CreateGroupField("CategoryID"));
                rep_ProductGeneral_group.rGroupIDCaption.Text = "Category ID" + " : ";
                rep_ProductGeneral_group.rGroupDescCaption.Text = "POS Screen Category" + " : ";
                rep_ProductGeneral_group.rGroupID.DataBindings.Add("Text", dtbl, "CategoryID");
                rep_ProductGeneral_group.rGroupDesc.DataBindings.Add("Text", dtbl, "Category");
                rep_ProductGeneral_group.GroupType = "C";

                rep_ProductGeneral_group.rH1.Text = "Primary Vendor";
                rep_ProductGeneral_group.rH2.Text = "Department";
                rep_ProductGeneral_group.rV1.DataBindings.Add("Text", dtbl, "Vendor");
                rep_ProductGeneral_group.rV2.DataBindings.Add("Text", dtbl, "Department");

            }

            rep_ProductGeneral_group.rDP.DataBindings.Add("Text", dtbl, "DecimalPlace");
            rep_ProductGeneral_group.rSKU.DataBindings.Add("Text", dtbl, "SKU");
            rep_ProductGeneral_group.rProduct.DataBindings.Add("Text", dtbl, "ProductDesc");
            rep_ProductGeneral_group.rType.DataBindings.Add("Text", dtbl, "ProductType");

            rep_ProductGeneral_group.rQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_ProductGeneral_group.rUnitCost.DataBindings.Add("Text", dtbl, "UnitCost");
            rep_ProductGeneral_group.rDCost.DataBindings.Add("Text", dtbl, "DiscountedCost");
            rep_ProductGeneral_group.rUnitRetailCost.DataBindings.Add("Text", dtbl, "UnitRetailValue");
            rep_ProductGeneral_group.rExtCost.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_ProductGeneral_group.rExtRetail.DataBindings.Add("Text", dtbl, "ExtRetailValue");
            rep_ProductGeneral_group.rMargin.DataBindings.Add("Text", dtbl, "CurrentMargin");

            rep_ProductGeneral_group.rTot1.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_ProductGeneral_group.rTot2.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_ProductGeneral_group.rTot3.DataBindings.Add("Text", dtbl, "ExtRetailValue");

            rep_ProductGeneral_group.rTTot1.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_ProductGeneral_group.rTTot2.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_ProductGeneral_group.rTTot3.DataBindings.Add("Text", dtbl, "ExtRetailValue");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_ProductGeneral_group.PrinterName = Settings.ReportPrinterName;
                    rep_ProductGeneral_group.CreateDocument();
                    rep_ProductGeneral_group.PrintingSystem.ShowMarginsWarning = false;
                    rep_ProductGeneral_group.PrintingSystem.ShowPrintStatusDialog = false;


                    //rep_ProductGeneral_group.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_ProductGeneral_group;
                    window.ShowDialog();

                }
                finally
                {
                    rep_ProductGeneral_group.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_ProductGeneral_group.CreateDocument();
                rep_ProductGeneral_group.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_ProductGeneral_group);
                }
                finally
                {
                    rep_ProductGeneral_group.Dispose();
                    dtbl.Dispose();
                }
            }

            if (eventtype == "Email")
            {
                rep_ProductGeneral_group.CreateDocument();
                rep_ProductGeneral_group.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "product_grp.pdf";
                    GeneralFunctions.EmailReport(rep_ProductGeneral_group, attachfile, (lkupGroupBy.EditValue.ToString() == "1") ? "Product by Dept" : "Product by Vendor");
                }
                finally
                {
                    rep_ProductGeneral_group.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void ExecuteReport(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            DataTable dtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dtbl = objProduct.FetchDataForGeneralReport(txtSKU.Text.Trim(), txtProduct.Text.Trim(),
                               grdGroup.ItemsSource as DataTable, grdClass.ItemsSource as DataTable, grdcat.ItemsSource as DataTable,
                               lkupOrderBy.EditValue.ToString(), lkupGroupBy.EditValue.ToString());

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Product.repProductGeneral rep_ProductGeneral = new OfflineRetailV2.Report.Product.repProductGeneral();

            rep_ProductGeneral.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_ProductGeneral);
            rep_ProductGeneral.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_ProductGeneral.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_ProductGeneral.DecimalPlace = Settings.DecimalPlace;

            if (Settings.DecimalPlace == 3)
            {
                rep_ProductGeneral.rTot2.Summary.FormatString = "{0:0.000}";
                rep_ProductGeneral.rTot3.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_ProductGeneral.rTot2.Summary.FormatString = "{0:0.00}";
                rep_ProductGeneral.rTot3.Summary.FormatString = "{0:0.00}";
            }


            rep_ProductGeneral.rDP.DataBindings.Add("Text", dtbl, "DecimalPlace");

            rep_ProductGeneral.rSKU.DataBindings.Add("Text", dtbl, "SKU");
            rep_ProductGeneral.rProduct.DataBindings.Add("Text", dtbl, "ProductDesc");
            rep_ProductGeneral.rType.DataBindings.Add("Text", dtbl, "ProductType");
            rep_ProductGeneral.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
            rep_ProductGeneral.rDept.DataBindings.Add("Text", dtbl, "Department");
            rep_ProductGeneral.rCat.DataBindings.Add("Text", dtbl, "Category");
            rep_ProductGeneral.rQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_ProductGeneral.rUnitCost.DataBindings.Add("Text", dtbl, "UnitCost");
            rep_ProductGeneral.rDCost.DataBindings.Add("Text", dtbl, "DiscountedCost");
            rep_ProductGeneral.rUnitRetailCost.DataBindings.Add("Text", dtbl, "UnitRetailValue");
            rep_ProductGeneral.rExtCost.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_ProductGeneral.rExtRetail.DataBindings.Add("Text", dtbl, "ExtRetailValue");
            rep_ProductGeneral.rMargin.DataBindings.Add("Text", dtbl, "CurrentMargin");

            //rep_ProductGeneral.rTot1.DataBindings.Add("Text", dtbl, "QtyOnHand");
            rep_ProductGeneral.rTot2.DataBindings.Add("Text", dtbl, "ExtCost");
            rep_ProductGeneral.rTot3.DataBindings.Add("Text", dtbl, "ExtRetailValue");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_ProductGeneral.PrinterName = Settings.ReportPrinterName;
                    rep_ProductGeneral.CreateDocument();
                    rep_ProductGeneral.PrintingSystem.ShowMarginsWarning = false;
                    rep_ProductGeneral.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_ProductGeneral.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_ProductGeneral;
                    window.ShowDialog();

                }
                finally
                {
                    rep_ProductGeneral.Dispose();


                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_ProductGeneral.CreateDocument();
                rep_ProductGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_ProductGeneral);
                }
                finally
                {
                    rep_ProductGeneral.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_ProductGeneral.CreateDocument();
                rep_ProductGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "product_g.pdf";
                    GeneralFunctions.EmailReport(rep_ProductGeneral, attachfile, "Product");
                }
                finally
                {
                    rep_ProductGeneral.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void ClickButton(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                int i = 0;
                int j = 0;
                int k = 0;
                DataTable dG = grdGroup.ItemsSource as DataTable;

                foreach (DataRow drG in dG.Rows)
                {
                    if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                    {
                        i++;
                    }
                }
                DataTable dC = grdClass.ItemsSource as DataTable;
                foreach (DataRow drC in dC.Rows)
                {
                    if (Convert.ToBoolean(drC["CheckVendor"].ToString()))
                    {
                        j++;
                    }
                }

                DataTable dPC = grdcat.ItemsSource as DataTable;
                foreach (DataRow drPC in dPC.Rows)
                {
                    if (Convert.ToBoolean(drPC["CheckCategory"].ToString()))
                    {
                        k++;
                    }
                }

                dG.Dispose();
                dC.Dispose();
                dPC.Dispose();
                if ((txtSKU.Text.Trim() == "") && (txtProduct.Text.Trim() == "") && (i == 0) && (j == 0) && (k == 0))
                {
                    if (DocMessage.MsgConfirmation("No criteria selected" + "\n" + "Do you want to go for all records?") == MessageBoxResult.Yes)
                    {
                        if (lkupGroupBy.EditValue.ToString() != "0") ExecuteGroupReport(eventtype);
                        else ExecuteReport(eventtype);
                    }
                }
                else
                {
                    if (lkupGroupBy.EditValue.ToString() != "0") ExecuteGroupReport(eventtype);
                    else ExecuteReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
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

        private void ChkClass_Checked(object sender, RoutedEventArgs e)
        {
            if (chkClass.IsChecked == true)
            {
                chkClass.Content = "Uncheck All";
                DataTable dtbl = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckVendor"] = true;
                }
                grdClass.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkClass.Content = "Check All";
                DataTable dtbl1 = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckVendor"] = false;
                }
                grdClass.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void ChkCat_Checked(object sender, RoutedEventArgs e)
        {
            if (chkCat.IsChecked == true)
            {
                chkCat.Content = "Uncheck All";
                DataTable dtbl = grdcat.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckCategory"] = true;
                }
                grdcat.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkCat.Content = "Check All";
                DataTable dtbl1 = grdcat.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckCategory"] = false;
                }
                grdcat.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckDept")
            {
                grdGroup.SetCellValue(e.RowHandle, colCheckDept, e.Value);

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

        private void GridView2_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckVendor")
            {
                grdClass.SetCellValue(e.RowHandle, colCheckVendor, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckVendor"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdClass.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkClass.IsChecked = true;
                    chkClass.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkClass.IsChecked = false;
                    chkClass.Content = "Check All";
                }
                dtbl.Dispose();
            }
        }

        private void GridView4_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckCat")
            {
                grdcat.SetCellValue(e.RowHandle, colCheckCat, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdcat.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckCategory"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdcat.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkCat.IsChecked = true;
                    chkCat.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkCat.IsChecked = false;
                    chkCat.Content = "Check All";
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
