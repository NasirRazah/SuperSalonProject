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
    /// Interaction logic for frm_SuspendReportDlg.xaml
    /// </summary>
    public partial class frm_SuspendReportDlg : Window
    {
        public frm_SuspendReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
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

                DataTable dtbl = new DataTable();
                PosDataObject.POS os1 = new PosDataObject.POS();
                os1.Connection = SystemVariables.Conn;
                dtbl = os1.FetchSuspendedData();

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No record found in the selected date range");
                    dtbl.Dispose();
                    return;
                }

                 OfflineRetailV2.Report.Sales.repSuspendedOrder rep_Sales1 = new OfflineRetailV2.Report.Sales.repSuspendedOrder();
                rep_Sales1.Report.DataSource = dtbl;

                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;

                rep_Sales1.rData1.DataBindings.Add("Text", dtbl, "InvoiceNo");
                rep_Sales1.rData4.DataBindings.Add("Text", dtbl, "Amount");
                rep_Sales1.rData3.DataBindings.Add("Text", dtbl, "DateTimeSuspended");
                rep_Sales1.rData2.DataBindings.Add("Text", dtbl, "Customer");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;


                        //rep_Sales1.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Sales1;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_Sales1.Dispose();

                        dtbl.Dispose();
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
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "suspended.pdf";
                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Suspended Orders");
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        dtbl.Dispose();
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
                ExecuteReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
    }
}
