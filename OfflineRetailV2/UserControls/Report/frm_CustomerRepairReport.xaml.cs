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
    /// Interaction logic for frm_CustomerRepairReport.xaml
    /// </summary>
    public partial class frm_CustomerRepairReport : Window
    {
        public frm_CustomerRepairReport()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void GetReport(string eventtype)
        {
            PosDataObject.Customer objAttendance = new PosDataObject.Customer();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            int intID = 0;
            intID = cmbCategory.SelectedIndex;

            dtbl = objAttendance.FetchRepairList(cmbDate.SelectedIndex, dtFrom.DateTime, dtTo.DateTime, intID);

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


            OfflineRetailV2.Report.Customer.repCustomerRepairList rep_LateDetails = new OfflineRetailV2.Report.Customer.repCustomerRepairList();

            rep_LateDetails.rAddHeader.Text = cmbDate.Text + " - " + "From " + dtFrom.DateTime.ToString(SystemVariables.DateFormat.Replace("/", "-")) + " " + "to" + " " +
                                                    dtTo.DateTime.ToString(SystemVariables.DateFormat);

            GeneralFunctions.MakeReportWatermark(rep_LateDetails);
            rep_LateDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LateDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_LateDetails.Report.DataSource = dtbl;
            rep_LateDetails.GroupHeader1.GroupFields.Add(rep_LateDetails.CreateGroupField("Status"));

            rep_LateDetails.grpRepairType.DataBindings.Add("Text", dtbl, "Status");
            rep_LateDetails.rRepairDate.DataBindings.Add("Text", dtbl, "RepairDate");
            rep_LateDetails.rDeliveryDate.DataBindings.Add("Text", dtbl, "DeliveryDate");
            rep_LateDetails.rRepairNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
            rep_LateDetails.rCustomer.DataBindings.Add("Text", dtbl, "Customer");

            rep_LateDetails.rRepairItemName.DataBindings.Add("Text", dtbl, "RepairItem");
            rep_LateDetails.rRepairSl.DataBindings.Add("Text", dtbl, "RepairItemSL");
            rep_LateDetails.rAmount.DataBindings.Add("Text", dtbl, "RepairAmount");

            rep_LateDetails.rTotal.DataBindings.Add("Text", dtbl, "RepairAmount");

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

                    //rep_LateDetails.ShowPreviewDialog();

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
                    attachfile = "cust_repair.pdf";
                    GeneralFunctions.EmailReport(rep_LateDetails, attachfile, "Customer - Repair List");
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                GetReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Repair List";
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
            ClickButton("Print");
        }

        private void CmbCategory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
