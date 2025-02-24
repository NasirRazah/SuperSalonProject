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
    /// Interaction logic for frm_VendorReportDlg.xaml
    /// </summary>
    public partial class frm_VendorReportDlg : Window
    {
        public frm_VendorReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateOrderBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "1", "Vendor ID" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Vendor Name" });
            dtblOrderBy.Rows.Add(new object[] { "3", "Contact" });
            dtblOrderBy.Rows.Add(new object[] { "4", "Zip Code" });
            lkupOrderBy.ItemsSource = dtblOrderBy;
            lkupOrderBy.DisplayMember = "Desc";
            lkupOrderBy.ValueMember = "ID";
            lkupOrderBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private void ExecuteReport(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            DataTable dtbl = new DataTable();
            PosDataObject.Vendor objVend = new PosDataObject.Vendor();
            objVend.Connection = SystemVariables.Conn;
            dtbl = objVend.FetchDataForGeneralReport(txtVID.Text.Trim(), txtVName.Text.Trim(),
                                    txtContact.Text.Trim(), txtZip.Text.Trim(), lkupOrderBy.EditValue.ToString());

            OfflineRetailV2.Report.Vendor.repVendorGeneral rep_VendorGeneral = new OfflineRetailV2.Report.Vendor.repVendorGeneral();

            rep_VendorGeneral.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_VendorGeneral);
            rep_VendorGeneral.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_VendorGeneral.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_VendorGeneral.rVID.DataBindings.Add("Text", dtbl, "VendorID");
            rep_VendorGeneral.rVName.DataBindings.Add("Text", dtbl, "Name");
            rep_VendorGeneral.rContact.DataBindings.Add("Text", dtbl, "Contact");
            rep_VendorGeneral.rAddress.DataBindings.Add("Text", dtbl, "Address1");
            rep_VendorGeneral.rCity.DataBindings.Add("Text", dtbl, "City");
            rep_VendorGeneral.rState.DataBindings.Add("Text", dtbl, "State");
            rep_VendorGeneral.rZip.DataBindings.Add("Text", dtbl, "Zip");
            rep_VendorGeneral.rPhone.DataBindings.Add("Text", dtbl, "Phone");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_VendorGeneral.PrinterName = Settings.ReportPrinterName;
                    rep_VendorGeneral.CreateDocument();
                    rep_VendorGeneral.PrintingSystem.ShowMarginsWarning = false;
                    rep_VendorGeneral.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_VendorGeneral;
                    window.ShowDialog();

                    //rep_VendorGeneral.ShowPreviewDialog();

                }
                finally
                {
                    rep_VendorGeneral.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_VendorGeneral.CreateDocument();
                rep_VendorGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_VendorGeneral);
                }
                finally
                {
                    rep_VendorGeneral.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_VendorGeneral.CreateDocument();
                rep_VendorGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "vendor.pdf";
                    GeneralFunctions.EmailReport(rep_VendorGeneral, attachfile, "Vendor");
                }
                finally
                {
                    rep_VendorGeneral.Dispose();
                    dtbl.Dispose();
                }
            }


        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if ((txtVID.Text.Trim() == "") && (txtVName.Text.Trim() == "") &&
                   (txtContact.Text.Trim() == "") && (txtZip.Text.Trim() == ""))
                {
                    if (DocMessage.MsgConfirmation("No Criteria Selected" + "\n" + "Do you want to go for all records?") == MessageBoxResult.Yes)
                    {
                        ExecuteReport(eventtype);
                    }
                }
                else
                {
                    ExecuteReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Vendor Report";
            PopulateOrderBy();
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

        private void LkupOrderBy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
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
