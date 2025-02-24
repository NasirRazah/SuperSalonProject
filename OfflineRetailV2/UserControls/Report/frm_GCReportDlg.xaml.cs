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
    /// Interaction logic for frm_GCReportDlg.xaml
    /// </summary>
    public partial class frm_GCReportDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_GCReportDlg()
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
            nkybrd = new NumKeyboard();
            Title.Text = "Gift Certificate Activity Report";
            dtFrom.EditValue = DateTime.Today.Date;
            dtTo.EditValue = DateTime.Today.Date;
        }

        private void ExecuteReport(string eventtype)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objSales1.FetchGCRedeemedData(txtGiftCertNo.Text.Trim(), dtFrom.DateTime, dtTo.DateTime, Settings.CentralExportImport, Settings.StoreCode);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Sales.repGCRedeem rep_GCRedeem = new OfflineRetailV2.Report.Sales.repGCRedeem();

            rep_GCRedeem.rHeader.Text = "Gift Certificate Activity";
            GeneralFunctions.MakeReportWatermark(rep_GCRedeem);
            rep_GCRedeem.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_GCRedeem.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_GCRedeem.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "To" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

            rep_GCRedeem.Report.DataSource = dtbl;
            rep_GCRedeem.DecimalPlace = Settings.DecimalPlace;

            rep_GCRedeem.GroupHeader1.GroupFields.Add(rep_GCRedeem.CreateGroupField("GiftCertID"));
            rep_GCRedeem.rGC.DataBindings.Add("Text", dtbl, "GiftCertID");
            rep_GCRedeem.rTrDate.DataBindings.Add("Text", dtbl, "TransDate");
            rep_GCRedeem.rInvNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
            rep_GCRedeem.rAmount.DataBindings.Add("Text", dtbl, "TenderAmount");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_GCRedeem.PrinterName = Settings.ReportPrinterName;
                    rep_GCRedeem.CreateDocument();
                    rep_GCRedeem.PrintingSystem.ShowMarginsWarning = false;
                    rep_GCRedeem.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_GCRedeem.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_GCRedeem;
                    window.ShowDialog();
                }
                finally
                {
                    rep_GCRedeem.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_GCRedeem.CreateDocument();
                rep_GCRedeem.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_GCRedeem);
                }
                finally
                {
                    rep_GCRedeem.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_GCRedeem.CreateDocument();
                rep_GCRedeem.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "GC_activity.pdf";
                    GeneralFunctions.EmailReport(rep_GCRedeem, attachfile, "GC Activity");
                }
                finally
                {
                    rep_GCRedeem.Dispose();
                    dtbl.Dispose();
                }
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
            CloseKeyboards();
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

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }


        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }

        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }
    }
}
