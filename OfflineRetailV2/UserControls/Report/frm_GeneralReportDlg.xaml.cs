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
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_GeneralReportDlg.xaml
    /// </summary>
    public partial class frm_GeneralReportDlg : Window
    {
        private int intID;

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public frm_GeneralReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (intID == 1)
            {
                Title.Text = "House Account Report (Summary)";
            }

            if (intID == 2)
            {
                Title.Text = "Store Credit Report";
            }
        }

        private void ExecuteHouseAccountReport(string eventtype)
        {
            try
            {

                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objSales.FetchCustomerHAReportData(Settings.StoreCode);

                DataTable dtbl1 = new DataTable();
                PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objSales1.FetchCustomerHALastPay(Settings.StoreCode);

                DataTable dtblR = new DataTable();
                dtblR.Columns.Add("CID", System.Type.GetType("System.String"));
                dtblR.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtblR.Columns.Add("Company", System.Type.GetType("System.String"));
                dtblR.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtblR.Columns.Add("Email", System.Type.GetType("System.String"));
                dtblR.Columns.Add("Balance", System.Type.GetType("System.String"));
                dtblR.Columns.Add("LPDate", System.Type.GetType("System.String"));
                dtblR.Columns.Add("LPAmount", System.Type.GetType("System.String"));

                foreach (DataRow dr in dtbl.Rows)
                {
                    string LPdt = "";
                    string LPAmt = "0";
                    foreach (DataRow dr1 in dtbl1.Rows)
                    {
                        if (dr["CID"].ToString() == dr1["CID"].ToString())
                        {
                            LPdt = dr1["Date"].ToString();
                            LPAmt = dr1["Amount"].ToString();
                            break;
                        }
                    }
                    if ((GeneralFunctions.fnDouble(LPAmt) != 0) || (GeneralFunctions.fnDouble(dr["Balance"].ToString()) != 0))
                    {
                        dtblR.Rows.Add(new object[] {   dr["CID"].ToString(),
                                                dr["CustID"].ToString(),
                                                dr["Company"].ToString(),
                                                dr["Customer"].ToString(),
                                                dr["Email"].ToString(),
                                                dr["Balance"].ToString(),
                                                LPdt,
                                                LPAmt});
                    }
                }
                if (dtblR.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtblR.Dispose();
                    return;
                }
                OfflineRetailV2.Report.Misc.repHASummary rep_HASummary = new OfflineRetailV2.Report.Misc.repHASummary();

                rep_HASummary.Report.DataSource = dtblR;
                GeneralFunctions.MakeReportWatermark(rep_HASummary);
                rep_HASummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_HASummary.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_HASummary.DecimalPlace = Settings.DecimalPlace;

                if (Settings.DecimalPlace == 3) rep_HASummary.rTotal.Summary.FormatString = "{0:0.000}";
                else rep_HASummary.rTotal.Summary.FormatString = "{0:0.00}";

                rep_HASummary.rCustID.DataBindings.Add("Text", dtblR, "CustID");
                rep_HASummary.rCustName.DataBindings.Add("Text", dtblR, "Customer");
                rep_HASummary.rEmail.DataBindings.Add("Text", dtblR, "Email");
                rep_HASummary.rDate.DataBindings.Add("Text", dtblR, "LPDate");
                rep_HASummary.rLP.DataBindings.Add("Text", dtblR, "LPAmount");
                rep_HASummary.rB.DataBindings.Add("Text", dtblR, "Balance");

                rep_HASummary.rTotal.DataBindings.Add("Text", dtblR, "Balance");

                if (eventtype == "Preview")
                {

                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_HASummary.PrinterName = Settings.ReportPrinterName;
                        rep_HASummary.CreateDocument();
                        rep_HASummary.PrintingSystem.ShowMarginsWarning = false;
                        rep_HASummary.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_HASummary.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_HASummary;
                        window.ShowDialog();
                    }
                    finally
                    {
                        rep_HASummary.Dispose();
                        //frm_PreviewControl.dv.DocumentSource = null;
                        //frm_PreviewControl.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtblR.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_HASummary.CreateDocument();
                    rep_HASummary.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_HASummary);
                    }
                    finally
                    {
                        rep_HASummary.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtblR.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_HASummary.CreateDocument();
                    rep_HASummary.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "cust_house_ac_s.pdf";
                        GeneralFunctions.EmailReport(rep_HASummary, attachfile, "Customer House Account (Summary)");
                    }
                    finally
                    {
                        rep_HASummary.Dispose();
                        dtbl.Dispose();
                        dtbl1.Dispose();
                        dtblR.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        private void ExecuteStoreCreditReport(string eventtype)
        {
            try
            {
                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objSales.FetchCustomerSCReportData(Settings.StoreCode);

                OfflineRetailV2.Report.Misc.repStoreCredit rep_StoreCredit = new OfflineRetailV2.Report.Misc.repStoreCredit();

                rep_StoreCredit.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_StoreCredit);
                rep_StoreCredit.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_StoreCredit.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_StoreCredit.DecimalPlace = Settings.DecimalPlace;

                if (Settings.DecimalPlace == 3) rep_StoreCredit.rTotal.Summary.FormatString = "{0:0.000}";
                else rep_StoreCredit.rTotal.Summary.FormatString = "{0:0.00}";

                rep_StoreCredit.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                rep_StoreCredit.rCustName.DataBindings.Add("Text", dtbl, "Customer");
                rep_StoreCredit.rCompany.DataBindings.Add("Text", dtbl, "Company");
                rep_StoreCredit.rAddress.DataBindings.Add("Text", dtbl, "Address1");
                rep_StoreCredit.rCity.DataBindings.Add("Text", dtbl, "City");
                rep_StoreCredit.rState.DataBindings.Add("Text", dtbl, "State");
                rep_StoreCredit.rZip.DataBindings.Add("Text", dtbl, "Zip");
                rep_StoreCredit.rPhone.DataBindings.Add("Text", dtbl, "Phone");
                rep_StoreCredit.rEmail.DataBindings.Add("Text", dtbl, "Email");

                rep_StoreCredit.rCredit.DataBindings.Add("Text", dtbl, "StoreCredit");
                rep_StoreCredit.rTotal.DataBindings.Add("Text", dtbl, "StoreCredit");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_StoreCredit.PrinterName = Settings.ReportPrinterName;
                        rep_StoreCredit.CreateDocument();
                        rep_StoreCredit.PrintingSystem.ShowMarginsWarning = false;
                        rep_StoreCredit.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_StoreCredit.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_StoreCredit;
                        window.ShowDialog();
                    }
                    finally
                    {
                        rep_StoreCredit.Dispose();
                        //frm_PreviewControl.dv.DocumentSource = null;
                        // frm_PreviewControl.Dispose();

                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_StoreCredit.CreateDocument();
                    rep_StoreCredit.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_StoreCredit);
                    }
                    finally
                    {
                        rep_StoreCredit.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_StoreCredit.CreateDocument();
                    rep_StoreCredit.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "cust_cr.pdf";
                        GeneralFunctions.EmailReport(rep_StoreCredit, attachfile, "Customer Store Credit");
                    }
                    finally
                    {
                        rep_StoreCredit.Dispose();
                        dtbl.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (intID == 1) ExecuteHouseAccountReport(eventtype);
                if (intID == 2) ExecuteStoreCreditReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }
    }
}
