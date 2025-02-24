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
    /// Interaction logic for frm_CardTranRepDlg.xaml
    /// </summary>
    public partial class frm_CardTranRepDlg : Window
    {
        public frm_CardTranRepDlg()
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

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Title.Text = "Card Transaction Report";
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
        }

        private bool CheckBlankDate()
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }
            return true;
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (CheckBlankDate())
                {
                    if (dtTo.DateTime.Date > DateTime.Today)
                    {
                        DocMessage.MsgInformation("To Date can not be after Today");
                        GeneralFunctions.SetFocus(dtTo);
                        return;
                    }

                    if (Settings.POSCardPaymentMerged == "B")
                    {
                        GetReportData_NewMerged(eventtype);
                    }
                    else if (Settings.POSCardPayment == "Y") GetReportData_New(eventtype);
                    else if (Settings.POSCardPayment == "N") GetReportData(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void GetReportData(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();

            dtbl = objAttendance.ReportCardTranData(dtFrom.DateTime, dtTo.DateTime);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                return;
            }

            OfflineRetailV2.Report.Sales.repCardTransaction rep_CardTransaction = new OfflineRetailV2.Report.Sales.repCardTransaction();

            rep_CardTransaction.rHeader2.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_CardTransaction);
            rep_CardTransaction.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CardTransaction.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CardTransaction.Report.DataSource = dtbl;
            rep_CardTransaction.DecimalPlace = Settings.DecimalPlace;
            rep_CardTransaction.rlcardtype.DataBindings.Add("Text", dtbl, "CardType");
            //rep_CardTransaction.rlAutoNo.DataBindings.Add("Text", dtbl, "AuthorisedTranNo");

            rep_CardTransaction.rlAuthDate.DataBindings.Add("Text", dtbl, "AuthorisedOn");
            rep_CardTransaction.rlInvNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
            rep_CardTransaction.rlCardAmount.DataBindings.Add("Text", dtbl, "InvoiceAmount");
            rep_CardTransaction.rlEmp.DataBindings.Add("Text", dtbl, "Employee");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CardTransaction.PrinterName = Settings.ReportPrinterName;
                    rep_CardTransaction.CreateDocument();
                    rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                    rep_CardTransaction.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CardTransaction;
                    window.ShowDialog();

                    //rep_CardTransaction.ShowPreviewDialog();


                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    // frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();

                }
            }

            if (eventtype == "Print")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CardTransaction);
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cc_tran.pdf";
                    GeneralFunctions.EmailReport(rep_CardTransaction, attachfile, "CC Transaction");
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void GetReportData_New(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();

            dtbl = objAttendance.ReportCardTranData_New(dtFrom.DateTime, dtTo.DateTime);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Sales.repCardTransactionNew rep_CardTransaction = new OfflineRetailV2.Report.Sales.repCardTransactionNew();
            GeneralFunctions.MakeReportWatermark(rep_CardTransaction);
            rep_CardTransaction.rHeader2.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            rep_CardTransaction.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CardTransaction.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CardTransaction.Report.DataSource = dtbl;
            rep_CardTransaction.DecimalPlace = Settings.DecimalPlace;
            rep_CardTransaction.rcardtype.DataBindings.Add("Text", dtbl, "CardType");
            rep_CardTransaction.rGateway.DataBindings.Add("Text", dtbl, "PaymentGateway");
            rep_CardTransaction.rAuthDate.DataBindings.Add("Text", dtbl, "TransactionDate");
            rep_CardTransaction.rTranNo.DataBindings.Add("Text", dtbl, "TransactionNo");
            rep_CardTransaction.rCardAmount.DataBindings.Add("Text", dtbl, "CardAmount");

            rep_CardTransaction.rAuthID.DataBindings.Add("Text", dtbl, "RefCardAuthID");
            rep_CardTransaction.rRefTran.DataBindings.Add("Text", dtbl, "RefCardTranID");
            rep_CardTransaction.rAuthAmount.DataBindings.Add("Text", dtbl, "RefCardAuthAmount");
            rep_CardTransaction.rCardLogo.DataBindings.Add("Text", dtbl, "RefCardLogo");
            rep_CardTransaction.rTranType.DataBindings.Add("Text", dtbl, "TransactionType");

            rep_CardTransaction.rlEmp.DataBindings.Add("Text", dtbl, "Employee");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CardTransaction.PrinterName = Settings.ReportPrinterName;
                    rep_CardTransaction.CreateDocument();
                    rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                    rep_CardTransaction.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CardTransaction;
                    window.ShowDialog();

                    //rep_CardTransaction.ShowPreviewDialog();


                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtbl.Dispose();
                }

            }
            if (eventtype == "Print")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CardTransaction);
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cc_tran.pdf";
                    GeneralFunctions.EmailReport(rep_CardTransaction, attachfile, "CC Transaction");
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
            }


        }


        private void GetReportData_NewMerged(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();

            dtbl = objAttendance.ReportCardTranData_NewCType(dtFrom.DateTime, dtTo.DateTime);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Sales.repCardTransactionNew rep_CardTransaction = new OfflineRetailV2.Report.Sales.repCardTransactionNew();
            GeneralFunctions.MakeReportWatermark(rep_CardTransaction);
            rep_CardTransaction.rHeader2.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            rep_CardTransaction.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CardTransaction.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CardTransaction.Report.DataSource = dtbl;
            rep_CardTransaction.DecimalPlace = Settings.DecimalPlace;
            rep_CardTransaction.rcardtype.DataBindings.Add("Text", dtbl, "CardType");
            rep_CardTransaction.rGateway.DataBindings.Add("Text", dtbl, "PaymentGateway");
            rep_CardTransaction.rAuthDate.DataBindings.Add("Text", dtbl, "TransactionDate");
            rep_CardTransaction.rTranNo.DataBindings.Add("Text", dtbl, "TransactionNo");
            rep_CardTransaction.rCardAmount.DataBindings.Add("Text", dtbl, "CardAmount");

            rep_CardTransaction.rAuthID.DataBindings.Add("Text", dtbl, "RefCardAuthID");
            rep_CardTransaction.rRefTran.DataBindings.Add("Text", dtbl, "RefCardTranID");
            rep_CardTransaction.rAuthAmount.DataBindings.Add("Text", dtbl, "RefCardAuthAmount");
            rep_CardTransaction.rCardLogo.DataBindings.Add("Text", dtbl, "RefCardLogo");
            rep_CardTransaction.rTranType.DataBindings.Add("Text", dtbl, "TransactionType");

            rep_CardTransaction.rlEmp.DataBindings.Add("Text", dtbl, "Employee");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CardTransaction.PrinterName = Settings.ReportPrinterName;
                    rep_CardTransaction.CreateDocument();
                    rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                    rep_CardTransaction.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CardTransaction;
                    window.ShowDialog();

                    //rep_CardTransaction.ShowPreviewDialog();


                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtbl.Dispose();
                }

            }
            if (eventtype == "Print")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CardTransaction);
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CardTransaction.CreateDocument();
                rep_CardTransaction.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cc_tran.pdf";
                    GeneralFunctions.EmailReport(rep_CardTransaction, attachfile, "CC Transaction");
                }
                finally
                {
                    rep_CardTransaction.Dispose();
                    dtbl.Dispose();
                }
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
    }
}
