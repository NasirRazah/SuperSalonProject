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
using DevExpress.XtraReports;
using DevExpress.Xpf.Printing.PreviewControl;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_TranJournalDlg.xaml
    /// </summary>
    public partial class frm_TranJournalDlg : Window
    {
        public frm_TranJournalDlg()
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
            Title.Text = "Transaction Journal Report";
            dtFrom.EditValue = DateTime.Today.Date;
            dtTo.EditValue = DateTime.Today.Date;
        }

        private void ClickButton(string eventtype)
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date Range");
                GeneralFunctions.SetFocus(dtFrom);
                return;
            }
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

        private void ExecuteReport(string eventtype)
        {

            DataTable dtbl = new DataTable();
            PosDataObject.POS objSales1 = new PosDataObject.POS();
            objSales1.Connection = SystemVariables.Conn;
            objSales1.BeginTransaction();
            dtbl = objSales1.FetchTransactionJournal(dtFrom.DateTime, dtTo.DateTime, SystemVariables.DateFormat);
            objSales1.EndTransaction();
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Misc.repTranJournal rep = new OfflineRetailV2.Report.Misc.repTranJournal();
            GeneralFunctions.MakeReportWatermark(rep);
            rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep.rReportHeader.Text = Settings.ReportHeader_Address;
            rep.rRange.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

            rep.Report.DataSource = dtbl;

            rep.r1.DataBindings.Add("Text", dtbl, "TranNo");
            rep.r2.DataBindings.Add("Text", dtbl, "TranDate");
            rep.r3.DataBindings.Add("Text", dtbl, "TranType");
            rep.r4.DataBindings.Add("Text", dtbl, "InvNo");
            rep.r5.DataBindings.Add("Text", dtbl, "InvAmt");
            rep.r6.DataBindings.Add("Text", dtbl, "Tender");
            rep.r7.DataBindings.Add("Text", dtbl, "User");
            rep.r8.DataBindings.Add("Text", dtbl, "Terminal");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {

                    if (Settings.ReportPrinterName != "") rep.PrinterName = Settings.ReportPrinterName;
                    rep.CreateDocument();
                    rep.PrintingSystem.ShowMarginsWarning = false;
                    rep.PrintingSystem.ShowPrintStatusDialog = false;



                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep;
                    window.ShowDialog();
                    //PrintHelper.ShowPrintPreviewDialog(this, rep);

                    //rep.ShowPreviewDialog();

                }
                finally
                {
                    rep.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep);
                }
                finally
                {
                    rep.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "tr_journal.pdf";
                    GeneralFunctions.EmailReport(rep, attachfile, "Transaction Journal");
                }
                finally
                {
                    rep.Dispose();
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
