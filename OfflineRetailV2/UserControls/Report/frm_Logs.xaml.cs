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

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_Logs.xaml
    /// </summary>
    public partial class frm_Logs : Window
    {
        public frm_Logs()
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
            Title.Text = "View Logs";
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
        }

        private void GetReportData()
        {
            PosDataObject.Setup objAttendance = new PosDataObject.Setup();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();

            dtbl = objAttendance.ViewLog(dtFrom.DateTime, dtTo.DateTime);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }

            OfflineRetailV2.Report.Misc.repLogs rep_Logs = new OfflineRetailV2.Report.Misc.repLogs();
            GeneralFunctions.MakeReportWatermark(rep_Logs);
            rep_Logs.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Logs.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Logs.Report.DataSource = dtbl;

            rep_Logs.GroupHeader1.GroupFields.Add(rep_Logs.CreateGroupField("GroupDate"));

            rep_Logs.rDate.DataBindings.Add("Text", dtbl, "LogDate");
            rep_Logs.rUser.DataBindings.Add("Text", dtbl, "UserName");
            rep_Logs.rTerminal.DataBindings.Add("Text", dtbl, "LogTerminal");
            rep_Logs.rTime.DataBindings.Add("Text", dtbl, "LogTime");
            rep_Logs.rEvent.DataBindings.Add("Text", dtbl, "LogEvent");
            rep_Logs.rStatus.DataBindings.Add("Text", dtbl, "EventStatus");
            rep_Logs.rRemarks.DataBindings.Add("Text", dtbl, "EventIdentity");

            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                if (Settings.ReportPrinterName != "") rep_Logs.PrinterName = Settings.ReportPrinterName;
                rep_Logs.CreateDocument();
                rep_Logs.PrintingSystem.ShowMarginsWarning = false;
                rep_Logs.PrintingSystem.ShowPrintStatusDialog = false;
                rep_Logs.ShowPreviewDialog();

            }
            finally
            {
                rep_Logs.Dispose();

                dtbl.Dispose();
            }
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBlankDate())
            {
                if (dtTo.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation("To Date can not be after Today");
                    GeneralFunctions.SetFocus(dtTo);
                    return;
                }
                Cursor = Cursors.Wait;
                try
                {
                    GetReportData();
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
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
    }
}
