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
using System.IO;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_CustSalesByProductReport.xaml
    /// 
    /// 
    /// /// </summary>
    public partial class frm_CustSalesByProductReport : Window
    {
        private DataTable dtblData = null;
        public frm_CustSalesByProductReport()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void PopulateCustomer()
        {
            PosDataObject.Customer objEmployee = new PosDataObject.Customer();
            objEmployee.Connection = SystemVariables.Conn;
            objEmployee.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblEmp = new DataTable();
            dbtblEmp = objEmployee.FetchLookupData3(Settings.StoreCode);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblEmp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbEmployee.ItemsSource = dtblTemp;
            int SelectID = 0;
            foreach (DataRow dr in dbtblEmp.Rows)
            {
                SelectID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                break;
            }
            cmbEmployee.EditValue = "0";
            dbtblEmp.Dispose();
        }


        private void PopulateDepartment()
        {
            PosDataObject.Department objDept = new PosDataObject.Department();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
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



            grdDept.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void PopulateCategory()
        {
            PosDataObject.Category objCat = new PosDataObject.Category();
            objCat.Connection = SystemVariables.Conn;
            DataTable dtbl = objCat.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
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

            grdCat.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void PopulateProduct()
        {
            PosDataObject.Product objCat = new PosDataObject.Product();
            objCat.Connection = SystemVariables.Conn;
            DataTable dtbl = objCat.FetchLookupData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["SKU"].ToString(), dr["ProductName"].ToString(), false });
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

            grdProduct.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Customer - Sales by Product";
            gridBand1.Visible = false;
            gridBand2.Fixed = DevExpress.Xpf.Grid.FixedStyle.None;
            //gridBand2.Width = grdData.Width - 6;
            dtblData = new DataTable();
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            PopulateCustomer();
            PopulateProduct();
            PopulateDepartment();
            PopulateCategory();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            gridBand1.Visible = false;
            gridBand2.Fixed = DevExpress.Xpf.Grid.FixedStyle.None;
            //gridBand2.Width = grdData.Width - 6;
            if (Convert.ToInt32(cmbEmployee.EditValue.ToString()) == 0)
            {
                gridBand1.Visible = true;
                gridBand2.Fixed = DevExpress.Xpf.Grid.FixedStyle.Right;
                gridBand2.Width = 453;
            }
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                return;
            }

            if (cmbEmployee.EditValue == null)
            {
                DocMessage.MsgInformation("Select Customer");
                return;
            }

            int i = 0;
            int j = 0;
            int k = 0;
            DataTable dG = grdDept.ItemsSource as DataTable;
            foreach (DataRow drG in dG.Rows)
            {
                if (Convert.ToBoolean(drG["Check"].ToString()))
                {
                    i++;
                }
            }
            dG.Dispose();
            DataTable dG1 = grdCat.ItemsSource as DataTable;
            foreach (DataRow drG in dG1.Rows)
            {
                if (Convert.ToBoolean(drG["Check"].ToString()))
                {
                    j++;
                }
            }
            dG1.Dispose();
            DataTable dG2 = grdProduct.ItemsSource as DataTable;
            foreach (DataRow drG in dG2.Rows)
            {
                if (Convert.ToBoolean(drG["Check"].ToString()))
                {
                    k++;
                }
            }
            dG2.Dispose();

            if ((i == 0) && (j == 0) && (k == 0))
            {
                DocMessage.MsgInformation("Please select atleast 1 criteria from departments, categories or products.");
                return;
            }

            Cursor = Cursors.Wait;
            try
            {
                DataTable dtblI = new DataTable();
                PosDataObject.Sales objsl = new PosDataObject.Sales();
                objsl.Connection = SystemVariables.Conn;
                dtblI = objsl.FetchCustomerSalesByProductData(GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString()),
                                                grdDept.ItemsSource as DataTable, grdCat.ItemsSource as DataTable, grdProduct.ItemsSource as DataTable,
                                                dtFrom.DateTime, dtTo.DateTime, Settings.StoreCode, false);

                DataTable dtblT = new DataTable();
                dtblT = dtblI;

                DataTable dtblC = new DataTable();
                dtblC.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtblC = dtblI.DefaultView.ToTable(true, "CustID");

                DataTable dtbl = new DataTable();
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Item", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CatName", System.Type.GetType("System.String"));

                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HomePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                foreach (DataRow dr in dtblC.Rows)
                {
                    foreach (DataRow dr1 in dtblI.Rows)
                    {
                        if (dr["CustID"].ToString() != dr1["CustID"].ToString()) continue;
                        if (dr["CustID"].ToString() == dr1["CustID"].ToString())
                        {
                            string sqlexp = " CustID = '" + dr["CustID"].ToString() + "'";
                            DataRow[] d;
                            d = dtblT.Select(sqlexp);
                            DataTable dtbl1 = new DataTable();
                            dtbl1 = dtblT.Clone();
                            foreach (DataRow dd in d)
                            {
                                dtbl1.ImportRow(dd);
                            }
                            DataTable dtbl2 = new DataTable();
                            dtbl2 = dtbl1.DefaultView.ToTable(true, new String[] { "SKU", "Item" });

                            foreach (DataRow drF in dtbl2.Rows)
                            {
                                double dblRev = 0;
                                double dblCost = 0;
                                double dblDCost = 0;
                                double dblQty = 0;
                                double dblProfit = 0;

                                foreach (DataRow drF1 in dtbl1.Rows)
                                {
                                    if (drF["SKU"].ToString() == drF1["SKU"].ToString())
                                    {
                                        dblQty = dblQty + GeneralFunctions.fnDouble(drF1["Qty"].ToString());
                                        dblRev = dblRev + (GeneralFunctions.fnDouble(drF1["Qty"].ToString()) * GeneralFunctions.fnDouble(drF1["Price"].ToString()))
                                                    - GeneralFunctions.fnDouble(drF1["Discount"].ToString());
                                        dblCost = dblCost + GeneralFunctions.fnDouble(drF1["Qty"].ToString()) * GeneralFunctions.fnDouble(drF1["Cost"].ToString());
                                        dblDCost = dblDCost + GeneralFunctions.fnDouble(drF1["Qty"].ToString()) * GeneralFunctions.fnDouble(drF1["DCost"].ToString());
                                    }
                                }

                                dblProfit = dblRev - dblCost;

                                bool insertflag = true;

                                foreach (DataRow dr10 in dtbl.Rows)
                                {
                                    if ((dr10["CustID"].ToString() == dr1["CustID"].ToString()) && (dr10["SKU"].ToString() == drF["SKU"].ToString()))
                                    {
                                        insertflag = false;
                                        break;
                                    }
                                }

                                if (insertflag)
                                    dtbl.Rows.Add(new object[] {
                                                   dr1["CustID"].ToString(),
                                                   drF["SKU"].ToString(),
                                                   drF["Item"].ToString(),
                                                   Convert.ToDouble(dblQty.ToString("f")),
                                                   Convert.ToDouble(dblCost.ToString("f")),
                                                   Convert.ToDouble(dblDCost.ToString("f")),
                                                   Convert.ToDouble(dblRev.ToString("f")),
                                                   Convert.ToDouble(dblProfit.ToString("f")),
                                                   dr1["DeptID"].ToString(),
                                                   dr1["DeptName"].ToString(),
                                                   dr1["CatID"].ToString(),
                                                   dr1["CatName"].ToString(),
                                                   dr1["CustomerID"].ToString(),
                                                   dr1["CustomerName"].ToString(),
                                                   dr1["MailAddress"].ToString(),
                                                   dr1["ShipAddress"].ToString(),
                                                   dr1["Company"].ToString(),
                                                   dr1["FirstName"].ToString(),
                                                   dr1["LastName"].ToString(),
                                                   dr1["Address1"].ToString(),
                                                   dr1["Address2"].ToString(),
                                                   dr1["City"].ToString(),
                                                   dr1["State"].ToString(),
                                                   dr1["Country"].ToString(),
                                                   dr1["Zip"].ToString(),
                                                   dr1["HomePhone"].ToString(),
                                                   dr1["WorkPhone"].ToString(),
                                                   dr1["MobilePhone"].ToString(),
                                                   dr1["Fax"].ToString(),
                                                   dr1["EMail"].ToString(),
                                                   dr1["ActiveStatus"].ToString(),
                                                    dr1["CGroup"].ToString(),
                                                    dr1["CClass"].ToString(),
                                                    strip});
                            }
                        }
                    }
                }
                dtblData.Rows.Clear();
                dtblData = dtbl;
                if (dtblData.Rows.Count > 0)
                {
                    grdData.ItemsSource = dtblData;
                }
                else grdData.ItemsSource = null;
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteReport(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                OfflineRetailV2.Report.Customer.repCustSalesByProducts rep_Sales1 = new OfflineRetailV2.Report.Customer.repCustSalesByProducts();
                rep_Sales1.Report.DataSource = dtblData;
                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_Sales1.DecimalPlace = Settings.DecimalPlace;

                rep_Sales1.rHeader.Text = "Customer - Sales By Product";

                rep_Sales1.GroupHeader1.GroupFields.Add(rep_Sales1.CreateGroupField("CustID"));
                rep_Sales1.rCustID.DataBindings.Add("Text", dtblData, "CustID");
                rep_Sales1.rCustName.DataBindings.Add("Text", dtblData, "CustomerName");
                rep_Sales1.rCompany.DataBindings.Add("Text", dtblData, "Company");

                rep_Sales1.rCI1.DataBindings.Add("Text", dtblData, "Address1");
                rep_Sales1.rCI2.DataBindings.Add("Text", dtblData, "Address2");
                rep_Sales1.rCI3.DataBindings.Add("Text", dtblData, "City");
                rep_Sales1.rCI4.DataBindings.Add("Text", dtblData, "State");
                rep_Sales1.rCI5.DataBindings.Add("Text", dtblData, "Zip");
                rep_Sales1.rCI6.DataBindings.Add("Text", dtblData, "Country");
                rep_Sales1.rCI7.DataBindings.Add("Text", dtblData, "WorkPhone");
                rep_Sales1.rCI8.DataBindings.Add("Text", dtblData, "Email");

                rep_Sales1.rGroup.DataBindings.Add("Text", dtblData, "CGroup");
                rep_Sales1.rClass.DataBindings.Add("Text", dtblData, "CClass");

                rep_Sales1.rSKU.DataBindings.Add("Text", dtblData, "SKU");
                rep_Sales1.rProduct.DataBindings.Add("Text", dtblData, "Item");
                rep_Sales1.rQty.DataBindings.Add("Text", dtblData, "Qty");
                rep_Sales1.rCost.DataBindings.Add("Text", dtblData, "Cost");
                rep_Sales1.rDCost.DataBindings.Add("Text", dtblData, "DCost");
                rep_Sales1.rRevenue.DataBindings.Add("Text", dtblData, "Price");
                rep_Sales1.rProfit.DataBindings.Add("Text", dtblData, "Profit");

                rep_Sales1.rDept.DataBindings.Add("Text", dtblData, "DeptName");
                rep_Sales1.rCat.DataBindings.Add("Text", dtblData, "CatName");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Sales1;
                        window.ShowDialog();

                        //rep_Sales1.ShowPreviewDialog();

                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        //frm_PreviewControl.dv.DocumentSource = null;
                        //frm_PreviewControl.Dispose();

                        dtblData.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Sales1);
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "cust_sales_by_item.pdf";
                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Customer Sales by Product");
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                    }
                }

                if (eventtype == "Export")
                {
                    try
                    {
                        string fileName = GeneralFunctions.ShowSaveFileDialog("CSV Document", "CSV Files|*.csv");
                        if (fileName != "")
                        {
                            FileInfo t = new FileInfo(fileName);
                            StreamWriter Tex = t.CreateText();
                            string hding = "Cust ID" + Settings.ExportDelimiter
                                            + "Customer" + Settings.ExportDelimiter
                                            + "Company" + Settings.ExportDelimiter
                                            + "Address 1" + Settings.ExportDelimiter
                                             + "Address 2" + Settings.ExportDelimiter
                                            + "City" + Settings.ExportDelimiter
                                            + "State" + Settings.ExportDelimiter
                                            + "Zip" + Settings.ExportDelimiter
                                            + "Country" + Settings.ExportDelimiter
                                            + "Work Phone" + Settings.ExportDelimiter
                                            + "EMail" + Settings.ExportDelimiter
                                            + "Group" + Settings.ExportDelimiter
                                            + "Class" + Settings.ExportDelimiter
                                            + "SKU" + Settings.ExportDelimiter
                                            + "Item" + Settings.ExportDelimiter
                                            + "Qty" + Settings.ExportDelimiter
                                            + "Cost" + Settings.ExportDelimiter
                                            + "Profit";
                            Tex.WriteLine(hding);
                            int i = 0;
                            foreach (DataRow dr in dtblData.Rows)
                            {
                                i++;
                                string str = dr["CustID"].ToString() + Settings.ExportDelimiter
                                            + dr["CustomerName"].ToString() + Settings.ExportDelimiter
                                            + dr["Company"].ToString() + Settings.ExportDelimiter
                                            + dr["Address1"].ToString() + Settings.ExportDelimiter
                                            + dr["Address2"].ToString() + Settings.ExportDelimiter
                                            + dr["City"].ToString() + Settings.ExportDelimiter
                                            + dr["State"].ToString() + Settings.ExportDelimiter
                                            + dr["Zip"].ToString() + Settings.ExportDelimiter
                                            + dr["Country"].ToString() + Settings.ExportDelimiter
                                            + dr["WorkPhone"].ToString() + Settings.ExportDelimiter
                                            + dr["EMail"].ToString() + Settings.ExportDelimiter
                                            + dr["CGroup"].ToString() + Settings.ExportDelimiter
                                            + dr["CClass"].ToString() + Settings.ExportDelimiter
                                            + dr["SKU"].ToString() + Settings.ExportDelimiter
                                            + dr["Item"].ToString() + Settings.ExportDelimiter
                                            + dr["Qty"].ToString() + Settings.ExportDelimiter
                                            + dr["Cost"].ToString() + Settings.ExportDelimiter
                                            + dr["Cost"].ToString() + Settings.ExportDelimiter
                                            + dr["Profit"].ToString();
                                Tex.WriteLine(str);
                            }
                            Tex.Close();
                            GeneralFunctions.OpenFile(fileName);
                        }
                    }
                    finally
                    {

                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (dtblData.Rows.Count > 0) ExecuteReport(eventtype);
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

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Export");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void ExecuteLabel()
        {
            int intskip = 0;
            int r = 0;
            int c = 0;
            r = GeneralFunctions.fnInt32(spnrow.Text);
            c = GeneralFunctions.fnInt32(spncol.Text);
            if (r == 1)
            {
                intskip = c - 1;
            }
            else
            {
                intskip = (r * 3) - 3 + c - 1;
            }

            DataTable dtblP = new DataTable();
            dtblP = dtblData.DefaultView.ToTable(true, new String[] { "CustID", "CustomerID", "CustomerName", "MailAddress", "ShipAddress",
                                                                      "Company", "FirstName", "Address1", "Address2", "City","State","Country",
                                                                      "Zip","HomePhone","WorkPhone","MobilePhone","Fax","EMail","ActiveStatus"});

            DataTable dtblActive = dtblP.Clone();
            DataRow[] d = dtblP.Select("ActiveStatus = 'Y' ");
            foreach (DataRow dr in d)
            {
                dtblActive.ImportRow(dr);
            }

            if (dtblActive.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing.");
                return;
            }

            DataTable dlbl = new DataTable();
            dlbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Address", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Attn", System.Type.GetType("System.String"));

            if (rgReportType1.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["Company"].ToString(),
                                                    dr1["MailAddress"].ToString(),
                                                    dr1["CustomerName"].ToString()});
                }
            }

            if (rgReportType2.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["Company"].ToString(),
                                                    dr1["ShipAddress"].ToString(),
                                                    dr1["CustomerName"].ToString()});
                }
            }


            dlbl.Rows.Add(new object[] { "", "", "" });

            OfflineRetailV2.Report.Customer.repCustomerLabel rep_CustomerLabel = new OfflineRetailV2.Report.Customer.repCustomerLabel(intskip, dlbl.Rows.Count);
            DataSet ds = new DataSet();
            ds.Tables.Add(dlbl);
            rep_CustomerLabel.DataSource = ds;

            if (Settings.UseCustomerNameInLabelPrint == "N")
            {
                rep_CustomerLabel.rAttn.Visible = false;
                rep_CustomerLabel.rAttnLb.Visible = false;
            }


            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                if (Settings.ReportPrinterName != "") rep_CustomerLabel.PrinterName = Settings.ReportPrinterName;
                rep_CustomerLabel.CreateDocument();
                rep_CustomerLabel.PrintingSystem.ShowMarginsWarning = false;
                rep_CustomerLabel.PrintingSystem.ShowPrintStatusDialog = false;
                rep_CustomerLabel.ShowPreviewDialog();
                //frm_PreviewControl.dv.DocumentSource = rep_CustomerLabel;
                //frm_PreviewControl.dv.PrintingSystem = rep_CustomerLabel.PrintingSystem;
                //frm_PreviewControl.dv.PrintingSystem.ShowMarginsWarning = false;
                //frm_PreviewControl.dv.PrintingSystem.ShowPrintStatusDialog = false;
                //frm_PreviewControl.ShowDialog();
            }
            finally
            {
                rep_CustomerLabel.Dispose();
                //frm_PreviewControl.dv.DocumentSource = null;
                //frm_PreviewControl.Dispose();
                dtblP.Dispose();
                dlbl.Dispose();
                dtblActive.Dispose();
            }
        }

        private void BtnPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            if (dtblData.Rows.Count > 0)
            {
                Cursor = Cursors.Wait;
                try
                {
                    ExecuteLabel();
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void ChkSelectGrid_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSelectGrid.IsChecked == true)
            {
                chkSelectGrid.Content = "Uncheck All";
                DataTable dtbl = grdProduct.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["Check"] = true;
                }
                grdProduct.ItemsSource = dtbl;
                dtbl.Dispose();

                DataTable dtbl1 = grdDept.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl1.Rows)
                {
                    dr["Check"] = true;
                }
                grdDept.ItemsSource = dtbl1;
                dtbl1.Dispose();

                DataTable dtbl2 = grdCat.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl2.Rows)
                {
                    dr["Check"] = true;
                }
                grdCat.ItemsSource = dtbl2;
                dtbl2.Dispose();
            }
            else
            {
                chkSelectGrid.Content = "Check All";
                DataTable dtbl = grdProduct.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["Check"] = false;
                }
                grdProduct.ItemsSource = dtbl;
                dtbl.Dispose();

                DataTable dtbl1 = grdDept.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl1.Rows)
                {
                    dr["Check"] = false;
                }
                grdDept.ItemsSource = dtbl1;
                dtbl1.Dispose();

                DataTable dtbl2 = grdCat.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl2.Rows)
                {
                    dr["Check"] = false;
                }
                grdCat.ItemsSource = dtbl2;
                dtbl2.Dispose();
            }
        }

        private void Spnrow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbEmployee_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
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
