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
    /// Interaction logic for frm_SalesPriceReportDlg.xaml
    /// </summary>
    public partial class frm_SalesPriceReportDlg : Window
    {
        public frm_SalesPriceReportDlg()
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
            Title.Text = "Sales Price List";
            dtStart.EditValue = DateTime.Today.Date.AddDays(1).Date;
            dtEnd.EditValue = DateTime.Today.AddDays(8).Date;
            tmStart.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
            tmEnd.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 21, 0, 0);
        }

        private bool IsValidAll()
        {
            if ((dtStart.EditValue == null) || (dtEnd.EditValue == null)
                || (tmStart.EditValue == null) || (tmEnd.EditValue == null))
            {
                DocMessage.MsgEnter("Valid Period");
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }

            DateTime f = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                GeneralFunctions.fnDate(dtStart.DateTime).Day,
                GeneralFunctions.fnDate(tmStart.EditValue).Hour, GeneralFunctions.fnDate(tmStart.EditValue).Minute, 0);

            DateTime t = new DateTime(GeneralFunctions.fnDate(dtEnd.DateTime).Year, GeneralFunctions.fnDate(dtEnd.DateTime).Month,
                GeneralFunctions.fnDate(dtEnd.DateTime).Day,
                GeneralFunctions.fnDate(tmEnd.EditValue).Hour, GeneralFunctions.fnDate(tmEnd.EditValue).Minute, 0);

            if (t < f)
            {
                DocMessage.MsgEnter("Valid Period");
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }

            return true;
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

                DateTime EffectiveFrom = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                GeneralFunctions.fnDate(dtStart.DateTime).Day,
                GeneralFunctions.fnDate(tmStart.EditValue).Hour, GeneralFunctions.fnDate(tmStart.EditValue).Minute, 0);

                DateTime EffectiveTo = new DateTime(GeneralFunctions.fnDate(dtEnd.DateTime).Year, GeneralFunctions.fnDate(dtEnd.DateTime).Month,
                    GeneralFunctions.fnDate(dtEnd.DateTime).Day,
                    GeneralFunctions.fnDate(tmEnd.EditValue).Hour, GeneralFunctions.fnDate(tmEnd.EditValue).Minute, 0);

                DataTable dtbl = new DataTable();
                PosDataObject.Discounts os1 = new PosDataObject.Discounts();
                os1.Connection = SystemVariables.Conn;
                dtbl = os1.FetchSalePriceDataForReport(EffectiveFrom, EffectiveTo);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No record found in the selected date range");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Sales.repSalesPriceList rep_Sales1 = new OfflineRetailV2.Report.Sales.repSalesPriceList();
                rep_Sales1.Report.DataSource = dtbl;

                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rDate1.Text = EffectiveFrom.ToString((SystemVariables.DateFormat.Replace("/", "-")) + "  hh:mm tt");
                rep_Sales1.rDate2.Text = EffectiveTo.ToString((SystemVariables.DateFormat.Replace("/", "-")) + "  hh:mm tt");


                rep_Sales1.rData1.DataBindings.Add("Text", dtbl, "SKU");
                rep_Sales1.rData2.DataBindings.Add("Text", dtbl, "Description");
                rep_Sales1.rData3.DataBindings.Add("Text", dtbl, "SPrice");
                rep_Sales1.rData4.DataBindings.Add("Text", dtbl, "RPrice");
                rep_Sales1.rData5.DataBindings.Add("Text", dtbl, "Batch");


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
                        attachfile = "sale_price_list.pdf";
                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Sales Price List");
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
                if (IsValidAll())
                {
                    ExecuteReport(eventtype);
                }
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
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void DtStart_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
