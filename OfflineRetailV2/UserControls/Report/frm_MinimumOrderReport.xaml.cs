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
    /// Interaction logic for frm_MinimumOrderReport.xaml
    /// </summary>
    public partial class frm_MinimumOrderReport : Window
    {
        public frm_MinimumOrderReport()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            objVendor.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblVendor = new DataTable();
            dbtblVendor = objVendor.FetchLookupData("B");

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblVendor.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbVendor.ItemsSource = dtblTemp;


            cmbVendor.EditValue = "0";
            dbtblVendor.Dispose();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Vendor Minimum Order Report";
            PopulateVendor();
            dtFrom.EditValue = DateTime.Today.Date.AddDays(-7);
            dtTo.EditValue = DateTime.Today.Date;
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

        private void ExecuteReport(string eventtype)
        {
            if ((cmbVendor.EditValue.ToString() != "") && (dtFrom.EditValue != null) && (dtTo.EditValue != null))
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                OfflineRetailV2.Report.repMinOrderReport rep_MinOrderReport = new OfflineRetailV2.Report.repMinOrderReport();
                DataTable dtbl = new DataTable();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;
                dtbl = objPO.FetchMinAmountPO(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), GeneralFunctions.fnDate(dtFrom.EditValue.ToString()), GeneralFunctions.fnDate(dtTo.EditValue.ToString()));
                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }
                GeneralFunctions.MakeReportWatermark(rep_MinOrderReport);
                rep_MinOrderReport.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_MinOrderReport.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_MinOrderReport.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_MinOrderReport.DecimalPlace = Settings.DecimalPlace;

                rep_MinOrderReport.Report.DataSource = dtbl;
                rep_MinOrderReport.GroupHeader1.GroupFields.Add(rep_MinOrderReport.CreateGroupField("ID"));

                rep_MinOrderReport.rVendor.DataBindings.Add("Text", dtbl, "VendorName");

                rep_MinOrderReport.rPO.DataBindings.Add("Text", dtbl, "OrderNo");
                rep_MinOrderReport.rPODate.DataBindings.Add("Text", dtbl, "OrderDate");
                rep_MinOrderReport.rGAmt.DataBindings.Add("Text", dtbl, "GrossAmount");
                rep_MinOrderReport.rFreight.DataBindings.Add("Text", dtbl, "Freight");
                rep_MinOrderReport.rTax.DataBindings.Add("Text", dtbl, "Tax");
                rep_MinOrderReport.rNetAmt.DataBindings.Add("Text", dtbl, "NetAmount");
                rep_MinOrderReport.rMinAmt.DataBindings.Add("Text", dtbl, "VendorMinOrderAmount");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_MinOrderReport.PrinterName = Settings.ReportPrinterName;
                        rep_MinOrderReport.CreateDocument();
                        rep_MinOrderReport.PrintingSystem.ShowMarginsWarning = false;
                        rep_MinOrderReport.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_MinOrderReport.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_MinOrderReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_MinOrderReport.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_MinOrderReport.CreateDocument();
                    rep_MinOrderReport.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_MinOrderReport);
                    }
                    finally
                    {
                        rep_MinOrderReport.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_MinOrderReport.CreateDocument();
                    rep_MinOrderReport.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "vendor_min_order.pdf";
                        GeneralFunctions.EmailReport(rep_MinOrderReport, attachfile, "Vendor Min. Order");
                    }
                    finally
                    {
                        rep_MinOrderReport.Dispose();
                        dtbl.Dispose();
                    }
                }

            }

        }

        private void BtnEmail_Loaded(object sender, RoutedEventArgs e)
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

        private void CmbVendor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
