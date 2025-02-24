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
    /// Interaction logic for frm_PackinglistDlg.xaml
    /// </summary>
    public partial class frm_PaidInOutReportDlg : Window
    {
       

        public frm_PaidInOutReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private bool IsValidInput()
        {
            if (dtFrom.EditValue == null)
            {
                DocMessage.MsgInformation("Select From Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }

            if (dtTo.EditValue == null)
            {
                DocMessage.MsgInformation("Select To Date");
                GeneralFunctions.SetFocus(dtTo);
                return false;
            }
            return true;
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (IsValidInput()) GetReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void GetReport(string eventtype)
        {
            PosDataObject.Sales objAttendance = new PosDataObject.Sales();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            int intID = 0;
            intID = cmbCategory.SelectedIndex;

            dtbl = objAttendance.FetchPaidInOutReportData(cmbCategory.Text, dtFrom.DateTime, dtTo.DateTime, SystemVariables.DateFormat);

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


            OfflineRetailV2.Report.Misc.repPaidInOut rep_LateDetails = new OfflineRetailV2.Report.Misc.repPaidInOut();

            if (cmbCategory.Text == "All")
            {
                rep_LateDetails.rHeader.Text = "Paid In/Out Report";

            }
            else
            {
                rep_LateDetails.rHeader.Text = cmbCategory.Text + " Report";
                rep_LateDetails.rGroupRow.Visible = false;
            }


            rep_LateDetails.rAddHeader.Text = "From " + dtFrom.DateTime.ToString(SystemVariables.DateFormat) + " " + "to" + " " +
                                                    dtTo.DateTime.ToString(SystemVariables.DateFormat);

            GeneralFunctions.MakeReportWatermark(rep_LateDetails);
            rep_LateDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LateDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_LateDetails.Report.DataSource = dtbl;
            rep_LateDetails.GroupHeader1.GroupFields.Add(rep_LateDetails.CreateGroupField("TranType"));
            rep_LateDetails.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

            rep_LateDetails.grpType.DataBindings.Add("Text", dtbl, "TranType");
            rep_LateDetails.rDate.DataBindings.Add("Text", dtbl, "TranDate");
            rep_LateDetails.rNotes.DataBindings.Add("Text", dtbl, "TranNotes");
            rep_LateDetails.rAmount.DataBindings.Add("Text", dtbl, "TranAmount");
            rep_LateDetails.rStaff.DataBindings.Add("Text", dtbl, "Staff");
            rep_LateDetails.rTotal.DataBindings.Add("Text", dtbl, "TranAmount");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_LateDetails.PrinterName = Settings.ReportPrinterName;
                    rep_LateDetails.CreateDocument();
                    rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                    rep_LateDetails.PrintingSystem.ShowPrintStatusDialog = false;
                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_LateDetails;
                    window.ShowDialog();
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_LateDetails.CreateDocument();
                rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_LateDetails);
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_LateDetails.CreateDocument();
                rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    if (cmbCategory.Text == "All") attachfile = "paidin_out.pdf";
                    if (cmbCategory.Text == "Paid In") attachfile = "paidin.pdf";
                    if (cmbCategory.Text == "Paid Out") attachfile = "paidout.pdf";
                    GeneralFunctions.EmailReport(rep_LateDetails, attachfile, cmbCategory.Text == "All" ? "Paid In/Out" : cmbCategory.Text);
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Paid In/ Paid Out Report";
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
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
            ClickButton("Link");
        }

        private void CmbGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
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
    }
}
