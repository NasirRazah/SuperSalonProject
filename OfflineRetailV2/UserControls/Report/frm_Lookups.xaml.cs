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
using OfflineRetailV2.Data;
using System.Data;
using DevExpress.Xpf.Grid;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_Lookups.xaml
    /// </summary>
    public partial class frm_Lookups : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private bool boolOk;
        public bool Ok
        {
            get { return boolOk; }
            set { boolOk = value; }
        }
        private string strSearchType;
        public string SearchType
        {
            get { return strSearchType; }
            set { strSearchType = value; }
        }

        private string strPrint;
        public string Print
        {
            get { return strPrint; }
            set { strPrint = value; }
        }

        public async Task<int> GetID()
        {
            if (gridView1.FocusedRowHandle < 0) return 0;
            int intRowID = 0;
            if ((grdLookUp.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdLookUp, colID)));
        }

        public async Task<string> GetColoum1Value()
        {
            int intRowID = 0;
            if ((grdLookUp.ItemsSource as DataTable).Rows.Count == 0) return "";
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return "";
            return (await GeneralFunctions.GetCellValue1(intRowID, grdLookUp, col1)).Trim();
        }

        public async Task<string> GetColoum2Value()
        {
            int intRowID = 0;
            if ((grdLookUp.ItemsSource as DataTable).Rows.Count == 0) return "";
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return "";
            return (await GeneralFunctions.GetCellValue1(intRowID, grdLookUp, col2)).Trim();
        }

        public frm_Lookups()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();

            DataTable dtblCriteria = new DataTable();
            dtblCriteria.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCriteria.Columns.Add("FilterText", System.Type.GetType("System.String"));

            string displayresource = "";
            if (strSearchType == "Customer") displayresource = "Customer";
            if (strSearchType == "Employee") displayresource ="Employee";
            if (strSearchType == "Vendor") displayresource = "Vendor";
            if (strSearchType == "Product") displayresource = "Product";
            if (strSearchType == "Product (For Break Pack)") displayresource = "Product (For Break Pack)";

            if (strPrint == "N")
            {
                Title.Text = Properties.Resources.Select + " " + displayresource;
                btnOk.Content = "DONE";
                btnOk.Style = this.FindResource("SaveButtonStyle") as Style;
                btnEmail.Visibility = Visibility.Hidden;
                btnPreview.Visibility = Visibility.Hidden;
            }
            else
            {
                Title.Text = Properties.Resources.Print + " " + displayresource;
                btnOk.Content = "PRINT";
                btnOk.Style = this.FindResource("PrintButtonStyle") as Style;
                btnEmail.Visibility = Visibility.Visible;
                btnPreview.Visibility = Visibility.Visible;
            }
            if (strSearchType == "Customer")
            {
                label1.Visibility = Visibility.Visible;
                cmbCriteria.Visibility = Visibility.Visible;
                col1.Header = "Customer ID";
                col2.Header = "Customer Name";
                col3.Header = "Company";
                col4.Header = "Address";


                dtblCriteria.Rows.Add(new object[] { "0", "Customer ID" });
                dtblCriteria.Rows.Add(new object[] { "1", "First Name" });
                dtblCriteria.Rows.Add(new object[] { "2", "Last Name" });
                dtblCriteria.Rows.Add(new object[] { "3", "Company" });

                cmbCriteria.ItemsSource = dtblCriteria;
                cmbCriteria.EditValue = "0";
                dtblCriteria.Dispose();
            }

            if (strSearchType == "Employee")
            {
                label1.Visibility = Visibility.Visible;
                cmbCriteria.Visibility = Visibility.Visible;
                col1.Header = "Emp. ID";
                col2.Header = "Emp. Name";
                col3.Header = "Address";
                col4.Header = "Security Profile";


                dtblCriteria.Rows.Add(new object[] { "0", "Emp. ID" });
                dtblCriteria.Rows.Add(new object[] { "1", "First Name" });
                dtblCriteria.Rows.Add(new object[] { "2", "Last Name" });

                cmbCriteria.ItemsSource = dtblCriteria;
                cmbCriteria.EditValue = "0";
                dtblCriteria.Dispose();
            }

            if (strSearchType == "Vendor")
            {
                label1.Visibility = Visibility.Visible;
                cmbCriteria.Visibility = Visibility.Visible;
                col1.Header = "Vendor ID";
                col2.Header = "Company";
                col3.Header = "Address";
                col4.Header = "Contact";

                dtblCriteria.Rows.Add(new object[] { "0", "Vendor ID" });
                dtblCriteria.Rows.Add(new object[] { "1", "Company" });
                dtblCriteria.Rows.Add(new object[] { "2", "Contact Person" });
                dtblCriteria.Rows.Add(new object[] { "3", "Address" });

                cmbCriteria.ItemsSource = dtblCriteria;
                cmbCriteria.EditValue = "0";
                //dtblCriteria.Dispose();
            }

            if (strSearchType == "Product")
            {
                label1.Visibility = Visibility.Visible;
                cmbCriteria.Visibility = Visibility.Visible;
                col1.Header = "SKU";
                col2.Header = "Description";
                col3.Header = "Department";
                col4.Header = "Product Type";

                dtblCriteria.Rows.Add(new object[] { "0", "SKU" });
                dtblCriteria.Rows.Add(new object[] { "1", "Description" });

                cmbCriteria.ItemsSource = dtblCriteria;
                cmbCriteria.EditValue = "0";
                dtblCriteria.Dispose();
            }

            if (strSearchType == "Product (For Break Pack)")
            {
                label1.Visibility = Visibility.Visible;
                cmbCriteria.Visibility = Visibility.Visible;
                col1.Header = "SKU";
                col2.Header = "Description";
                col3.Header = "Department";
                col4.Header = "Product Type";

                dtblCriteria.Rows.Add(new object[] { "0", "SKU" });
                dtblCriteria.Rows.Add(new object[] { "1", "Description" });

                cmbCriteria.ItemsSource = dtblCriteria;
                cmbCriteria.EditValue = "0";
                dtblCriteria.Dispose();
            }

            GeneralFunctions.SetFocus(txtSearch);
        }

        private void CmbCriteria_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                FindFilterData();
            }
        }


        private void FindFilterData()
        {
            int intFilterCategory = 0;
            string FilterType = strSearchType;
            if ((strSearchType == "Customer") || (strSearchType == "Employee")
                || (strSearchType == "Product") || (strSearchType == "Vendor") || (strSearchType == "Product (For Break Pack)"))
            {
                intFilterCategory = GeneralFunctions.fnInt32(cmbCriteria.EditValue.ToString());
            }
            PosDataObject.Lookup objLookupData = new PosDataObject.Lookup();
            objLookupData.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objLookupData.FetchData(FilterType, intFilterCategory, txtSearch.Text.Trim(), Settings.StoreCode);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdLookUp.ItemsSource = dtblTemp;
            dbtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void TxtSearch_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                FindFilterData();
            }
        }

        private async Task ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (strPrint == "Y")
                {
                    if (strSearchType == "Customer") await ExecuteCustomerReport(eventtype);
                    if (strSearchType == "Vendor") await ExecuteVendorReport(eventtype);
                    if (strSearchType == "Product") await ExecuteProductReport(eventtype);
                    if (strSearchType == "Employee") await ExecuteEmployeeReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (strPrint == "N")
            {
                boolOk = true;
                CloseKeyboards();
                Close();
            }
            else
            {
                await ClickButton("Print");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private async Task ExecuteCustomerReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }


                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();

                OfflineRetailV2.Report.Customer.repCustomerSnapDetail rep_CustomerSnapDetail = new OfflineRetailV2.Report.Customer.repCustomerSnapDetail();
                OfflineRetailV2.Report.Customer.repCustomerSnap rep_CustomerSnap = new OfflineRetailV2.Report.Customer.repCustomerSnap();
                OfflineRetailV2.Report.Customer.repCustomerGroup rep_CustomerGroup = new OfflineRetailV2.Report.Customer.repCustomerGroup();
                OfflineRetailV2.Report.Customer.repCustomerClass rep_CustomerClass = new OfflineRetailV2.Report.Customer.repCustomerClass();

                PosDataObject.Customer objCust = new PosDataObject.Customer();
                objCust.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCust.DecimalPlace = Settings.DecimalPlace;
                dtbl = objCust.FetchRecordForReport(await GetID());
                rep_CustomerSnapDetail.subrepGeneral.ReportSource = rep_CustomerSnap;

                rep_CustomerSnap.Report.DataSource = dtbl;
                rep_CustomerSnap.rPic.DataBindings.Add("Text", dtbl, "ID");

                GeneralFunctions.MakeReportWatermark(rep_CustomerSnapDetail);
                rep_CustomerSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CustomerSnap.rReportHeader.Text = Settings.ReportHeader_Address;

                rep_CustomerSnap.DecimalPlace = Settings.DecimalPlace;

                rep_CustomerSnap.rID.DataBindings.Add("Text", dtbl, "CustomerID");
                rep_CustomerSnap.rCompany.DataBindings.Add("Text", dtbl, "Company");
                rep_CustomerSnap.rName.DataBindings.Add("Text", dtbl, "Name");
                rep_CustomerSnap.rSalutation.DataBindings.Add("Text", dtbl, "Salutation");
                rep_CustomerSnap.rSpouse.DataBindings.Add("Text", dtbl, "Spouse");
                rep_CustomerSnap.rDOB.DataBindings.Add("Text", dtbl, "DateOfBirth");
                rep_CustomerSnap.rDOM.DataBindings.Add("Text", dtbl, "DateOfMarriage");
                rep_CustomerSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_CustomerSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_CustomerSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_CustomerSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");
                rep_CustomerSnap.rBstate.DataBindings.Add("Text", dtbl, "State");
                rep_CustomerSnap.rBcountry.DataBindings.Add("Text", dtbl, "Country");
                rep_CustomerSnap.rSAdd1.DataBindings.Add("Text", dtbl, "ShipAddress1");
                rep_CustomerSnap.rSadd2.DataBindings.Add("Text", dtbl, "ShipAddress2");
                rep_CustomerSnap.rScity.DataBindings.Add("Text", dtbl, "ShipCity");
                rep_CustomerSnap.rSzip.DataBindings.Add("Text", dtbl, "ShipZip");
                rep_CustomerSnap.rSstate.DataBindings.Add("Text", dtbl, "ShipState");
                rep_CustomerSnap.rScountry.DataBindings.Add("Text", dtbl, "ShipCountry");
                rep_CustomerSnap.rWphone.DataBindings.Add("Text", dtbl, "WorkPhone");
                rep_CustomerSnap.rHphone.DataBindings.Add("Text", dtbl, "HomePhone");
                rep_CustomerSnap.rMphone.DataBindings.Add("Text", dtbl, "MobilePhone");
                rep_CustomerSnap.rFax.DataBindings.Add("Text", dtbl, "Fax");
                rep_CustomerSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");

                rep_CustomerSnap.rTaxExempt.DataBindings.Add("Text", dtbl, "TaxExempt");
                rep_CustomerSnap.rTaxID.DataBindings.Add("Text", dtbl, "TaxID");
                rep_CustomerSnap.rPriceLebel.DataBindings.Add("Text", dtbl, "DiscountLevel");

                rep_CustomerSnap.rCL.DataBindings.Add("Text", dtbl, "ARCreditLimit");
                rep_CustomerSnap.rOB.DataBindings.Add("Text", dtbl, "ClosingBalance");
                rep_CustomerSnap.rCB.DataBindings.Add("Text", dtbl, "ClosingBalance");
                rep_CustomerSnap.rEP.DataBindings.Add("Text", dtbl, "Points");
                rep_CustomerSnap.rStoreCard.DataBindings.Add("Text", dtbl, "StoreCreditCard");


                PosDataObject.Customer objCustomer1 = new PosDataObject.Customer();
                objCustomer1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objCustomer1.ShowGroup(await GetID());


                if (dtbl1.Rows.Count > 0)
                {
                    rep_CustomerSnapDetail.subrepGroup.ReportSource = rep_CustomerGroup;
                    rep_CustomerGroup.Report.DataSource = dtbl1;
                    rep_CustomerGroup.rgrpid.DataBindings.Add("Text", dtbl1, "GroupIDName");
                    rep_CustomerGroup.rgrpname.DataBindings.Add("Text", dtbl1, "Description");
                }

                PosDataObject.Customer objCustomer2 = new PosDataObject.Customer();
                objCustomer2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCustomer2.ShowClass(await GetID());

                if (dtbl2.Rows.Count > 0)
                {
                    rep_CustomerSnapDetail.subrepClass.ReportSource = rep_CustomerClass;
                    rep_CustomerClass.Report.DataSource = dtbl2;
                    rep_CustomerClass.rclsid.DataBindings.Add("Text", dtbl2, "ClassIDName");
                    rep_CustomerClass.rclsname.DataBindings.Add("Text", dtbl2, "Description");
                }

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_CustomerSnapDetail.PrinterName = Settings.ReportPrinterName;
                        rep_CustomerSnapDetail.CreateDocument();
                        rep_CustomerSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                        rep_CustomerSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_CustomerSnapDetail.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_CustomerSnapDetail;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_CustomerSnap.Dispose();
                        rep_CustomerGroup.Dispose();
                        rep_CustomerClass.Dispose();
                        rep_CustomerSnapDetail.Dispose();

                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }

                }

                if (eventtype == "Print")
                {
                    rep_CustomerSnapDetail.CreateDocument();
                    rep_CustomerSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_CustomerSnapDetail);
                    }
                    finally
                    {
                        rep_CustomerSnapDetail.Dispose();
                        rep_CustomerSnap.Dispose();
                        rep_CustomerGroup.Dispose();
                        rep_CustomerClass.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_CustomerSnapDetail.CreateDocument();
                    rep_CustomerSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "cust_g.pdf";
                        GeneralFunctions.EmailReport(rep_CustomerSnapDetail, attachfile, "Customer - General");
                    }
                    finally
                    {
                        rep_CustomerSnapDetail.Dispose();
                        rep_CustomerSnap.Dispose();
                        rep_CustomerGroup.Dispose();
                        rep_CustomerClass.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }
                }

            }
        }

        private async Task ExecuteEmployeeReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                DataTable dtbl = new DataTable();
                PosDataObject.Employee objEmp = new PosDataObject.Employee();
                objEmp.Connection = SystemVariables.Conn;
                dtbl = objEmp.FetchRecordForReport(await GetID());
                OfflineRetailV2.Report.Employee.repEmployeeSnap rep_EmployeeSnap = new OfflineRetailV2.Report.Employee.repEmployeeSnap();

                rep_EmployeeSnap.Report.DataSource = dtbl;
                rep_EmployeeSnap.rPic.DataBindings.Add("Text", dtbl, "ID");
                GeneralFunctions.MakeReportWatermark(rep_EmployeeSnap);
                rep_EmployeeSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_EmployeeSnap.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_EmployeeSnap.rID.DataBindings.Add("Text", dtbl, "EmployeeID");
                rep_EmployeeSnap.rName.DataBindings.Add("Text", dtbl, "Name");
                rep_EmployeeSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_EmployeeSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_EmployeeSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_EmployeeSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");
                rep_EmployeeSnap.rBstate.DataBindings.Add("Text", dtbl, "State");

                rep_EmployeeSnap.rphone1.DataBindings.Add("Text", dtbl, "Phone1");
                rep_EmployeeSnap.rphone2.DataBindings.Add("Text", dtbl, "Phone2");
                rep_EmployeeSnap.rEphone.DataBindings.Add("Text", dtbl, "EmergencyPhone");
                rep_EmployeeSnap.rEcontact.DataBindings.Add("Text", dtbl, "EmergencyContact");
                rep_EmployeeSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");

                rep_EmployeeSnap.rSecurityNo.DataBindings.Add("Text", dtbl, "SSNumber");
                rep_EmployeeSnap.rShift.DataBindings.Add("Text", dtbl, "Shift");
                rep_EmployeeSnap.rProfile.DataBindings.Add("Text", dtbl, "SecurityProfile");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_EmployeeSnap.PrinterName = Settings.ReportPrinterName;
                        rep_EmployeeSnap.CreateDocument();
                        rep_EmployeeSnap.PrintingSystem.ShowMarginsWarning = false;
                        rep_EmployeeSnap.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_EmployeeSnap.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_EmployeeSnap;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_EmployeeSnap.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_EmployeeSnap.CreateDocument();
                    rep_EmployeeSnap.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_EmployeeSnap);
                    }
                    finally
                    {
                        rep_EmployeeSnap.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_EmployeeSnap.CreateDocument();
                    rep_EmployeeSnap.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "emp_g.pdf";
                        GeneralFunctions.EmailReport(rep_EmployeeSnap, attachfile, "Employee - General");
                    }
                    finally
                    {
                        rep_EmployeeSnap.Dispose();
                        dtbl.Dispose();
                    }
                }
            }
        }

        private async Task ExecuteVendorReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
                objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objVendor.ShowRecord(await GetID());
                OfflineRetailV2.Report.Vendor.repVendorSnapDetail rep_VendorSnapDetail = new OfflineRetailV2.Report.Vendor.repVendorSnapDetail();
                OfflineRetailV2.Report.Vendor.repVendorSnap rep_VendorSnap = new OfflineRetailV2.Report.Vendor.repVendorSnap();
                OfflineRetailV2.Report.Vendor.repVendorPart rep_VendorPart = new OfflineRetailV2.Report.Vendor.repVendorPart();

                rep_VendorSnapDetail.subrepGeneral.ReportSource = rep_VendorSnap;
                rep_VendorSnap.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_VendorSnapDetail);
                rep_VendorSnap.rReportHeader.Text = Settings.ReportHeader;
                rep_VendorSnap.rName.DataBindings.Add("Text", dtbl, "Name");

                rep_VendorSnap.rID.DataBindings.Add("Text", dtbl, "VendorID");
                rep_VendorSnap.rContact.DataBindings.Add("Text", dtbl, "Contact");
                rep_VendorSnap.rPhone.DataBindings.Add("Text", dtbl, "Phone");
                rep_VendorSnap.rFax.DataBindings.Add("Text", dtbl, "Fax");
                rep_VendorSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");
                rep_VendorSnap.rNotes.DataBindings.Add("Text", dtbl, "Notes");

                rep_VendorSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_VendorSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_VendorSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_VendorSnap.rBcountry.DataBindings.Add("Text", dtbl, "Country");
                rep_VendorSnap.rBstate.DataBindings.Add("Text", dtbl, "State");
                rep_VendorSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");

                PosDataObject.Vendor objVendor1 = new PosDataObject.Vendor();
                objVendor1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objVendor1.ShowPartNumber(await GetID());

                if (dtbl1.Rows.Count > 0)
                {
                    rep_VendorSnapDetail.subrepPart.ReportSource = rep_VendorPart;
                    rep_VendorPart.Report.DataSource = dtbl1;
                    rep_VendorPart.DecimalPlace = Settings.DecimalPlace;
                    rep_VendorPart.rProduct.DataBindings.Add("Text", dtbl1, "ProductName");
                    rep_VendorPart.rPart.DataBindings.Add("Text", dtbl1, "PartNumber");
                    rep_VendorPart.rCost.DataBindings.Add("Text", dtbl1, "Price");
                    rep_VendorPart.rPrimary.DataBindings.Add("Text", dtbl1, "Primary");
                }

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_VendorSnapDetail.PrinterName = Settings.ReportPrinterName;
                        rep_VendorSnapDetail.CreateDocument();
                        rep_VendorSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                        rep_VendorSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_VendorSnapDetail.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_VendorSnapDetail;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_VendorSnap.Dispose();
                        rep_VendorPart.Dispose();
                        rep_VendorSnapDetail.Dispose();


                        dtbl.Dispose();
                        dtbl1.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_VendorSnapDetail.CreateDocument();
                    rep_VendorSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_VendorSnapDetail);
                    }
                    finally
                    {
                        rep_VendorSnapDetail.Dispose();
                        rep_VendorSnap.Dispose();
                        rep_VendorPart.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_VendorSnapDetail.CreateDocument();
                    rep_VendorSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "vend_g.pdf";
                        GeneralFunctions.EmailReport(rep_VendorSnapDetail, attachfile, "Vendor - General");
                    }
                    finally
                    {
                        rep_VendorSnapDetail.Dispose();
                        rep_VendorSnap.Dispose();
                        rep_VendorPart.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                    }
                }
            }
        }

        private async Task ExecuteProductReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();
                DataTable dtbl3 = new DataTable();
                PosDataObject.Product objProduct = new PosDataObject.Product();
                objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objProduct.DecimalPlace = Settings.DecimalPlace;
                dtbl = objProduct.FetchRecordForReport(await GetID());

                int LinkSKU = 0;
                string ptype = "";
                foreach (DataRow dr in dtbl.Rows)
                {
                    LinkSKU = GeneralFunctions.fnInt32(dr["LinkSKUID"].ToString());
                    ptype = dr["ProductType"].ToString();
                    break;
                }

                OfflineRetailV2.Report.Product.repProductSnapDetail rep_ProductSnapDetail = new OfflineRetailV2.Report.Product.repProductSnapDetail();
                OfflineRetailV2.Report.Product.repProductSnap1 rep_ProductSnap = new OfflineRetailV2.Report.Product.repProductSnap1();
                OfflineRetailV2.Report.Product.repProductVendor rep_ProductVendor = new OfflineRetailV2.Report.Product.repProductVendor();
                OfflineRetailV2.Report.Product.repProductTax rep_ProductTax = new OfflineRetailV2.Report.Product.repProductTax();
                OfflineRetailV2.Report.Product.repProductBreakPack rep_ProductBreakPack = new OfflineRetailV2.Report.Product.repProductBreakPack();

                rep_ProductSnapDetail.subrepGeneral.ReportSource = rep_ProductSnap;
                rep_ProductSnap.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_ProductSnapDetail);
                rep_ProductSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_ProductSnap.rReportHeader.Text = Settings.ReportHeader_Address;

                if (ptype == "Service")
                {
                    rep_ProductSnap.lbSKU.Text = "Code";
                    rep_ProductSnap.lb11.Text = "Cost";
                    rep_ProductSnap.lb7.Text = "Min. Age";
                    rep_ProductSnap.lb9.Text = "Min. Time";
                    rep_ProductSnap.lb100.Visible = false;
                    rep_ProductSnap.rDCost.Visible = false;
                    rep_ProductSnap.lb1.Visible = false;
                    rep_ProductSnap.lb2.Visible = false;
                    rep_ProductSnap.lb3.Visible = false;
                    rep_ProductSnap.lb8.Visible = false;
                    rep_ProductSnap.lb10.Visible = false;
                    rep_ProductSnap.lb12.Visible = false;
                    rep_ProductSnap.lb13.Visible = false;
                    rep_ProductSnap.lb14.Visible = false;
                    rep_ProductSnap.lb15.Visible = false;
                    rep_ProductSnap.lb16.Visible = false;
                    rep_ProductSnap.lb17.Visible = false;
                    rep_ProductSnap.lb18.Visible = false;
                    rep_ProductSnap.lb21.Visible = false;
                    rep_ProductSnap.lb22.Visible = false;
                    rep_ProductSnap.lb23.Visible = false;
                    rep_ProductSnap.lb24.Visible = false;
                    rep_ProductSnap.lb25.Visible = false;
                    rep_ProductSnap.lb26.Visible = false;
                    rep_ProductSnap.lb27.Visible = false;


                    rep_ProductSnap.rAltSKU.Visible = false;
                    rep_ProductSnap.rPT.Visible = false;
                    rep_ProductSnap.rMinAge.Visible = false;
                    rep_ProductSnap.chkBar.Visible = false;
                    rep_ProductSnap.chkFood.Visible = false;
                    rep_ProductSnap.chkNoPL.Visible = false;
                    rep_ProductSnap.chkPL.Visible = false;
                    rep_ProductSnap.chkZero.Visible = false;
                    rep_ProductSnap.chkDispStk.Visible = false;
                    rep_ProductSnap.chkDigit.Visible = false;
                    rep_ProductSnap.rUPC.Visible = false;
                    rep_ProductSnap.rSeason.Visible = false;
                    rep_ProductSnap.rLastPurchase.Visible = false;
                    rep_ProductSnap.rPoints.Visible = false;
                    rep_ProductSnap.rNoPrint.Visible = false;
                    rep_ProductSnap.rNormalQty.Visible = false;
                    rep_ProductSnap.rOnLawaway.Visible = false;
                    rep_ProductSnap.rBinLocation.Visible = false;

                }

                rep_ProductSnap.rPic.DataBindings.Add("Text", dtbl, "ID");
                rep_ProductSnap.rSKU.DataBindings.Add("Text", dtbl, "SKU");
                rep_ProductSnap.rAltSKU.DataBindings.Add("Text", dtbl, "SKU2");
                rep_ProductSnap.rDesc.DataBindings.Add("Text", dtbl, "Description");
                rep_ProductSnap.rDept.DataBindings.Add("Text", dtbl, "Department");
                rep_ProductSnap.rCategory.DataBindings.Add("Text", dtbl, "Category");
                rep_ProductSnap.rPT.DataBindings.Add("Text", dtbl, "ProductType");
                rep_ProductSnap.rMinAge.DataBindings.Add("Text", dtbl, "MinimumAge");

                rep_ProductSnap.chkBar.DataBindings.Add("Text", dtbl, "ScaleBarCode");
                rep_ProductSnap.chkFood.DataBindings.Add("Text", dtbl, "FoodStampEligible");
                rep_ProductSnap.chkNoPL.DataBindings.Add("Text", dtbl, "NoPriceOnLabel");
                rep_ProductSnap.chkPL.DataBindings.Add("Text", dtbl, "PrintBarCode");
                rep_ProductSnap.chkPOS.DataBindings.Add("Text", dtbl, "AddtoPOSScreen");
                rep_ProductSnap.chkPrice.DataBindings.Add("Text", dtbl, "PromptForPrice");

                rep_ProductSnap.rDCost.DataBindings.Add("Text", dtbl, "DCost");
                rep_ProductSnap.rCurrentCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_ProductSnap.rLastPurchase.DataBindings.Add("Text", dtbl, "LastCost");
                rep_ProductSnap.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");
                rep_ProductSnap.rPriceB.DataBindings.Add("Text", dtbl, "PriceB");
                rep_ProductSnap.rPriceC.DataBindings.Add("Text", dtbl, "PriceC");
                rep_ProductSnap.rPoints.DataBindings.Add("Text", dtbl, "Points");

                rep_ProductSnap.chkZero.DataBindings.Add("Text", dtbl, "AllowZeroStock");
                rep_ProductSnap.chkDispStk.DataBindings.Add("Text", dtbl, "DisplayStockinPOS");
                rep_ProductSnap.chkActive.DataBindings.Add("Text", dtbl, "ProductStatus");
                rep_ProductSnap.chkDigit.DataBindings.Add("Text", dtbl, "DecimalPlace");

                rep_ProductSnap.rUPC.DataBindings.Add("Text", dtbl, "UPC");
                rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "Brand");
                rep_ProductSnap.rSeason.DataBindings.Add("Text", dtbl, "Season");


                if (LinkSKU > 0) // Breack pack
                {
                    rep_ProductSnap.lbSKU.Text = "UPC";
                    rep_ProductSnap.lb1.Text = "Ratio";
                    rep_ProductSnap.rAltSKU.DataBindings.Add("Text", dtbl, "BreakPackRatio");
                    rep_ProductSnap.lb4.Text = "Link SKU";
                    rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "LinkSKU");
                    rep_ProductSnap.rDept.DataBindings.Add("Text", dtbl, "LinkSKUDesc");

                    rep_ProductSnap.lb2.Visible = false;
                    rep_ProductSnap.lb3.Visible = false;
                    rep_ProductSnap.lb5.Visible = false;
                    rep_ProductSnap.lb6.Visible = false;
                    rep_ProductSnap.lb7.Visible = false;
                    rep_ProductSnap.lb8.Visible = false;
                    rep_ProductSnap.lb9.Visible = false;
                    rep_ProductSnap.lb10.Visible = false;
                    rep_ProductSnap.rPT.Visible = false;
                    rep_ProductSnap.rUPC.Visible = false;
                    rep_ProductSnap.rCategory.Visible = false;
                    rep_ProductSnap.rOnHandQty.Visible = false;
                    rep_ProductSnap.rReorderQty.Visible = false;
                    rep_ProductSnap.rNormalQty.Visible = false;
                    rep_ProductSnap.rOnLawaway.Visible = false;
                }
                else
                {
                    if (ptype == "Service")
                    {
                        rep_ProductSnap.rOnHandQty.DataBindings.Add("Text", dtbl, "MinimumAge");
                        rep_ProductSnap.rReorderQty.DataBindings.Add("Text", dtbl, "MinimumServiceTime");
                    }
                    else
                    {
                        rep_ProductSnap.rOnHandQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
                        rep_ProductSnap.rReorderQty.DataBindings.Add("Text", dtbl, "ReorderQty");
                    }
                    rep_ProductSnap.rNormalQty.DataBindings.Add("Text", dtbl, "NormalQty");
                    rep_ProductSnap.rOnLawaway.DataBindings.Add("Text", dtbl, "QtyOnLayaway");
                }
                rep_ProductSnap.rNoPrint.DataBindings.Add("Text", dtbl, "QtyToPrint");
                rep_ProductSnap.rNotes.DataBindings.Add("Text", dtbl, "ProductNotes");
                rep_ProductSnap.rBinLocation.DataBindings.Add("Text", dtbl, "BinLocation");

                PosDataObject.Product objProduct1 = new PosDataObject.Product();
                objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objProduct1.ShowPrimayVendor(await GetID());

                if (dtbl1.Rows.Count > 0)
                {
                    rep_ProductSnapDetail.subrepVendor.ReportSource = rep_ProductVendor;
                    rep_ProductVendor.Report.DataSource = dtbl1;
                    rep_ProductVendor.DecimalPlace = Settings.DecimalPlace;
                    rep_ProductVendor.rVendor.DataBindings.Add("Text", dtbl1, "VendorName");
                    rep_ProductVendor.rPart.DataBindings.Add("Text", dtbl1, "PartNumber");
                    rep_ProductVendor.rCost.DataBindings.Add("Text", dtbl1, "Price");
                    rep_ProductVendor.rPrimary.DataBindings.Add("Text", dtbl1, "IsPrimary");
                }

                PosDataObject.Product objProduct2 = new PosDataObject.Product();
                objProduct2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objProduct2.DecimalPlace = Settings.DecimalPlace;
                dtbl2 = objProduct2.ShowTaxes(await GetID());

                if (dtbl2.Rows.Count > 0)
                {
                    rep_ProductSnapDetail.subrepTax.ReportSource = rep_ProductTax;
                    rep_ProductTax.Report.DataSource = dtbl2;
                    rep_ProductTax.rTax.DataBindings.Add("Text", dtbl2, "TaxName");
                }

                if (LinkSKU == -1)
                {
                    PosDataObject.Product objProduct3 = new PosDataObject.Product();
                    objProduct3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objProduct3.DecimalPlace = Settings.DecimalPlace;
                    dtbl3 = objProduct3.ShowBreakPacks(await GetID());

                    if (dtbl3.Rows.Count > 0)
                    {
                        rep_ProductSnapDetail.subrepbreakpack.ReportSource = rep_ProductBreakPack;
                        rep_ProductBreakPack.Report.DataSource = dtbl3;
                        rep_ProductBreakPack.DecimalPlace = Settings.DecimalPlace;
                        rep_ProductBreakPack.rSKU.DataBindings.Add("Text", dtbl3, "SKU");
                        rep_ProductBreakPack.rDesc.DataBindings.Add("Text", dtbl3, "Description");
                        rep_ProductBreakPack.rRatio.DataBindings.Add("Text", dtbl3, "Ratio");
                    }
                }

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_ProductSnapDetail.PrinterName = Settings.ReportPrinterName;
                        rep_ProductSnapDetail.CreateDocument();
                        rep_ProductSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                        rep_ProductSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_ProductSnapDetail.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_ProductSnapDetail;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_ProductSnap.Dispose();
                        rep_ProductVendor.Dispose();
                        rep_ProductTax.Dispose();
                        rep_ProductSnapDetail.Dispose();

                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_ProductSnapDetail.CreateDocument();
                    rep_ProductSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_ProductSnapDetail);
                    }
                    finally
                    {
                        rep_ProductSnapDetail.Dispose();
                        rep_ProductSnap.Dispose();
                        rep_ProductVendor.Dispose();
                        rep_ProductTax.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_ProductSnapDetail.CreateDocument();
                    rep_ProductSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "prod_g.pdf";
                        GeneralFunctions.EmailReport(rep_ProductSnapDetail, attachfile, "Product - General");
                    }
                    finally
                    {
                        rep_ProductSnapDetail.Dispose();
                        rep_ProductSnap.Dispose();
                        rep_ProductVendor.Dispose();
                        rep_ProductTax.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtbl2.Dispose();
                    }
                }
            }
        }

        private async void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            await ClickButton("Preview");
        }

        private async void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            await ClickButton("Email");
        }

        private void CmbCriteria_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
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
