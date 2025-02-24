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
    /// Interaction logic for frm_GCBalReportDlg.xaml
    /// </summary>
    public partial class frm_GCBalReportDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_GCBalReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void PopulateGCCriteria()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "A", "All Gift Certificates issued" });
            //dtblOrderBy.Rows.Add(new object[] { "C", "Gift Certificates issued to all customers" });
            lkupGC.ItemsSource = dtblOrderBy;
            lkupGC.EditValue = "A";
            dtblOrderBy.Dispose();
        }

        private void PopulateBalCriteria()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "A", "All Balance" });
            dtblOrderBy.Rows.Add(new object[] { "G", "Balance Greater than" });
            dtblOrderBy.Rows.Add(new object[] { "L", "Balance Less than" });
            lkupBal.ItemsSource = dtblOrderBy;
            lkupBal.EditValue = "A";
            dtblOrderBy.Dispose();
        }

        private void ExecuteReport(string eventtype)
        {

            DataTable dtblI = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblI = objSales.FetchAllGiftCertIssueAmount(lkupGC.EditValue.ToString(), lkupBal.EditValue.ToString(), GeneralFunctions.fnDouble(numAmount.Text), Settings.CentralExportImport, Settings.StoreCode);

            DataTable dtblT = new DataTable();
            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblT = objSales1.FetchAllGiftCertTranAmount(lkupGC.EditValue.ToString(), lkupBal.EditValue.ToString(), GeneralFunctions.fnDouble(numAmount.Text), Settings.CentralExportImport, Settings.StoreCode);

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("GC", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("AMOUNT", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CUSTOMER", System.Type.GetType("System.String"));


            foreach (DataRow dr in dtblI.Rows)
            {
                double IAMT = 0;
                double TAMT = 0;
                double BAMT = 0;
                IAMT = GeneralFunctions.fnDouble(dr["AMOUNT"].ToString());

                foreach (DataRow dr1 in dtblT.Rows)
                {
                    TAMT = 0;
                    if (dr["GC"].ToString() == dr1["GC"].ToString())
                    {
                        TAMT = GeneralFunctions.fnDouble(dr1["AMOUNT"].ToString());
                        break;
                    }
                }
                BAMT = IAMT - TAMT;

                dtbl1.Rows.Add(new object[] {   dr["GC"].ToString(),
                                                BAMT.ToString(),//dr["AMOUNT"].ToString(),
                                                dr["CUSTOMER"].ToString()});
            }

            if (dtbl1.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl1.Dispose();
                return;
            }

            OfflineRetailV2.Report.Sales.repGCBal rep_GCBal = new OfflineRetailV2.Report.Sales.repGCBal();

            rep_GCBal.rHeader.Text = lkupGC.Text;

            if (lkupBal.EditValue.ToString() == "A")
            {
                rep_GCBal.rDetail.Text = "All balance";
            }
            if (lkupBal.EditValue.ToString() == "G")
            {
                if (Settings.DecimalPlace == 3)
                    rep_GCBal.rDetail.Text = "Balance" + " > " + GeneralFunctions.fnDouble(numAmount.Text).ToString("#0.000");
                if (Settings.DecimalPlace == 2)
                    rep_GCBal.rDetail.Text = "Balance" + " > " + GeneralFunctions.fnDouble(numAmount.Text).ToString("#0.00");
            }
            if (lkupBal.EditValue.ToString() == "L")
            {
                if (Settings.DecimalPlace == 3)
                    rep_GCBal.rDetail.Text = "Balance" + " < " + GeneralFunctions.fnDouble(numAmount.Text).ToString("#0.000");
                if (Settings.DecimalPlace == 2)
                    rep_GCBal.rDetail.Text = "Balance" + " < " + GeneralFunctions.fnDouble(numAmount.Text).ToString("#0.00");
            }

            rep_GCBal.Report.DataSource = dtbl1;
            GeneralFunctions.MakeReportWatermark(rep_GCBal);
            rep_GCBal.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_GCBal.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_GCBal.DecimalPlace = Settings.DecimalPlace;



            rep_GCBal.rGC.DataBindings.Add("Text", dtbl1, "GC");
            rep_GCBal.rBal.DataBindings.Add("Text", dtbl1, "AMOUNT");

            rep_GCBal.rTotal.DataBindings.Add("Text", dtbl1, "AMOUNT");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_GCBal.PrinterName = Settings.ReportPrinterName;
                    rep_GCBal.CreateDocument();
                    rep_GCBal.PrintingSystem.ShowMarginsWarning = false;
                    rep_GCBal.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_GCBal.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_GCBal;
                    window.ShowDialog();
                }
                finally
                {
                    rep_GCBal.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtblI.Dispose();
                    dtblT.Dispose();
                    dtbl1.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_GCBal.CreateDocument();
                rep_GCBal.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_GCBal);
                }
                finally
                {
                    rep_GCBal.Dispose();
                    dtblI.Dispose();
                    dtblT.Dispose();
                    dtbl1.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_GCBal.CreateDocument();
                rep_GCBal.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "GC_balance.pdf";
                    GeneralFunctions.EmailReport(rep_GCBal, attachfile, "GC Balance");
                }
                finally
                {
                    rep_GCBal.Dispose();
                    dtblI.Dispose();
                    dtblT.Dispose();
                    dtbl1.Dispose();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = "Gift Cert. Balance Report";
            PopulateGCCriteria();
            PopulateBalCriteria();
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

        private void LkupBal_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (lkupBal.EditValue.ToString() == "A")
            {
                lbAmt.Visibility = Visibility.Hidden;
                numAmount.Visibility = Visibility.Hidden;
            }
            else
            {
                lbAmt.Visibility = Visibility.Visible;
                numAmount.Visibility = Visibility.Visible;
            }
        }

        private void LkupGC_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
