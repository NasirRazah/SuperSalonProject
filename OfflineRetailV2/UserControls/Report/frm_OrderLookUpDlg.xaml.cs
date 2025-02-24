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
using OfflineRetailV2.Data;
using System.Data;
using DevExpress.Xpf.Grid;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_OrderLookUpDlg.xaml
    /// </summary>
    public partial class frm_OrderLookUpDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_OrderLookUpDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private string strSearchType;
        private string strPrint;

        private bool blSelected;

        public string SearchType
        {
            get { return strSearchType; }
            set { strSearchType = value; }
        }

        public string Print
        {
            get { return strPrint; }
            set { strPrint = value; }
        }

        public bool boolSelected
        {
            get { return blSelected; }
            set { blSelected = value; }
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPO, colID)));
        }


        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
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
            cmbVendor.EditValue = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();

            //SetDecimalPlace();
            PopulateVendor();
            dtFrom.EditValue = DateTime.Today.Date.AddDays(-7);
            dtTo.EditValue = DateTime.Today.Date;
            if (strPrint == "N")
            {
                Title.Text =  Properties.Resources.Select + " " + strSearchType;
                btnOK.Content = "DONE";
                btnOK.Style = this.FindResource("SaveButtonStyle") as Style;


                btnEmail.Visibility = Visibility.Hidden;
                btnPreview.Visibility = Visibility.Hidden;
            }
            else
            {
                btnOK.Content = "PRINT";
                Title.Text = Properties.Resources.Print + " " + strSearchType;
                btnOK.Style = this.FindResource("PrintButtonStyle") as Style;
            }
            if (strSearchType == "Purchase Order")
            {
                colRecvDate.Visible = false;

            }
            if (strSearchType == "Receiving Items")
            {
                colRecvDate.FieldName = "ReceiveDate";
                colOrderDate.FieldName = "DateOrdered";
                colOrderNo.FieldName = "PurchaseOrder";
                colNetAmount.FieldName = "InvoiceTotal";
                colNetAmount.Header =  "Invoice Total";

                btnPrintAll.Visibility = btnPreviewAll.Visibility = Visibility.Visible;
                btnPreview.Content = "PREVIEW SELECTED";
            }
        }

        private void FetchPOData(int fVendor, DateTime fFrmDate, DateTime fToDate, string fPO)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            dbtbl = objPO.FetchPOHeader(fVendor, fFrmDate, fToDate, fPO);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdPO.ItemsSource = dtblTemp;
            dbtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void FetchRecvData(int fVendor, DateTime fFrmDate, DateTime fToDate, string fPO)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Recv objRecv = new PosDataObject.Recv();
            objRecv.Connection = SystemVariables.Conn;
            dbtbl = objRecv.FetchRecvHeader(fVendor, fFrmDate, fToDate, fPO);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdPO.ItemsSource = dtblTemp;
            dbtbl.Dispose();
            dtblTemp.Dispose();
        }

        public async Task<string> GetPO()
        {
            int intRowID = 0;
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return "";
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return "";
            return (await GeneralFunctions.GetCellValue1(intRowID, grdPO, colOrderNo));
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            if (strSearchType == "Purchase Order")
            {
                if ((cmbVendor.EditValue != null) && (dtFrom.EditValue != null) && (dtTo.EditValue != null))
                {
                    FetchPOData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), GeneralFunctions.fnDate(dtFrom.EditValue.ToString()), GeneralFunctions.fnDate(dtTo.EditValue.ToString()), txtPOno.Text.Trim());
                }
            }
            if (strSearchType == "Receiving Items")
            {
                if ((cmbVendor.EditValue != null) && (dtFrom.EditValue != null) && (dtTo.EditValue != null))
                {
                    FetchRecvData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), GeneralFunctions.fnDate(dtFrom.EditValue.ToString()), GeneralFunctions.fnDate(dtTo.EditValue.ToString()), txtPOno.Text.Trim());
                }
            }
        }

        private async void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (strPrint == "N")
            {
                blSelected = true;
                CloseKeyboards();
                Close();
            }
            else
            {
                await ClickButton("Print");
            }
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


        private async Task ExecutePOReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle != -1)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }


                DataTable dtbl = new DataTable();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objPO.FetchHeaderRecordForReport(await GetID());
                OfflineRetailV2.Report.repPO rep_PO = new OfflineRetailV2.Report.repPO();
                GeneralFunctions.MakeReportWatermark(rep_PO);
                rep_PO.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_PO.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_PO.DecimalPlace = Settings.DecimalPlace;
                rep_PO.Report.DataSource = dtbl;
                rep_PO.GroupHeader1.GroupFields.Add(rep_PO.CreateGroupField("ID"));

                rep_PO.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
                rep_PO.rAdd1.DataBindings.Add("Text", dtbl, "VAddress");
                rep_PO.rAdd2.DataBindings.Add("Text", dtbl, "VOthers");
                rep_PO.rProduct.DataBindings.Add("Text", dtbl, "ProductName");
                rep_PO.rPartNo.DataBindings.Add("Text", dtbl, "VendorPartNo");
                rep_PO.rQty.DataBindings.Add("Text", dtbl, "DQty");
                rep_PO.rPrice.DataBindings.Add("Text", dtbl, "DCost");
                rep_PO.rFreight.DataBindings.Add("Text", dtbl, "DFreight");
                rep_PO.rTax.DataBindings.Add("Text", dtbl, "DTax");
                rep_PO.rTotPrice.DataBindings.Add("Text", dtbl, "DExtCost");
                rep_PO.rOrderNo.DataBindings.Add("Text", dtbl, "OrderNo");
                rep_PO.rOrderDate.DataBindings.Add("Text", dtbl, "OrderDate");
                rep_PO.rRefNo.DataBindings.Add("Text", dtbl, "RefNo");
                rep_PO.rExpectedDelivery.DataBindings.Add("Text", dtbl, "ExpectedDeliveryDate");
                rep_PO.rInstruction.DataBindings.Add("Text", dtbl, "SupplierInstructions");

                rep_PO.rSubtatal.DataBindings.Add("Text", dtbl, "GrossAmount");
                rep_PO.rTotFreight.DataBindings.Add("Text", dtbl, "Freight");
                rep_PO.rTotTax.DataBindings.Add("Text", dtbl, "Tax");
                rep_PO.rGrandTotal.DataBindings.Add("Text", dtbl, "NetAmount");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_PO.PrinterName = Settings.ReportPrinterName;
                        rep_PO.CreateDocument();
                        rep_PO.PrintingSystem.ShowMarginsWarning = false;
                        rep_PO.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_PO.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_PO;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_PO.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_PO.CreateDocument();
                    rep_PO.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_PO);
                    }
                    finally
                    {
                        rep_PO.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_PO.CreateDocument();
                    rep_PO.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "po.pdf";
                        GeneralFunctions.EmailReport(rep_PO, attachfile, "Purchase Order");
                    }
                    finally
                    {
                        rep_PO.Dispose();
                        dtbl.Dispose();
                    }
                }

            }
        }

        private async Task ExecuteRecvReport(string eventtype)
        {
            if (gridView1.FocusedRowHandle != -1)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.Recv objRecv = new PosDataObject.Recv();
                objRecv.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objRecv.FetchHeaderRecordForReport(await GetID());
                OfflineRetailV2.Report.repReceiving rep_Receiving = new OfflineRetailV2.Report.repReceiving();
                GeneralFunctions.MakeReportWatermark(rep_Receiving);
                rep_Receiving.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Receiving.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Receiving.DecimalPlace = Settings.DecimalPlace;
                rep_Receiving.Report.DataSource = dtbl;
                rep_Receiving.GroupHeader1.GroupFields.Add(rep_Receiving.CreateGroupField("ID"));

                rep_Receiving.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
                rep_Receiving.rAdd1.DataBindings.Add("Text", dtbl, "VAddress");
                rep_Receiving.rAdd2.DataBindings.Add("Text", dtbl, "VOthers");
                rep_Receiving.rProduct.DataBindings.Add("Text", dtbl, "ProductName");
                rep_Receiving.rPartNo.DataBindings.Add("Text", dtbl, "VendorPartNo");
                rep_Receiving.rQty.DataBindings.Add("Text", dtbl, "DQty");
                rep_Receiving.rPrice.DataBindings.Add("Text", dtbl, "DCost");
                rep_Receiving.rFreight.DataBindings.Add("Text", dtbl, "DFreight");
                rep_Receiving.rTax.DataBindings.Add("Text", dtbl, "DTax");
                rep_Receiving.rTotPrice.DataBindings.Add("Text", dtbl, "DExtCost");
                rep_Receiving.rOrderNo.DataBindings.Add("Text", dtbl, "PurchaseOrder");
                rep_Receiving.rOrderDate.DataBindings.Add("Text", dtbl, "DateOrdered");
                rep_Receiving.rInvoiceNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
                rep_Receiving.rReceiveDate.DataBindings.Add("Text", dtbl, "ReceiveDate");
                rep_Receiving.rCheckClerk.DataBindings.Add("Text", dtbl, "CheckInClerk");
                rep_Receiving.rReceiveClerk.DataBindings.Add("Text", dtbl, "ReceivingClerk");
                rep_Receiving.rNotes.DataBindings.Add("Text", dtbl, "Note");

                rep_Receiving.rSubtatal.DataBindings.Add("Text", dtbl, "GrossAmount");
                rep_Receiving.rTotFreight.DataBindings.Add("Text", dtbl, "Freight");
                rep_Receiving.rTotTax.DataBindings.Add("Text", dtbl, "Tax");
                rep_Receiving.rGrandTotal.DataBindings.Add("Text", dtbl, "InvoiceTotal");



                if (eventtype == "Preview")
                {

                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Receiving.PrinterName = Settings.ReportPrinterName;
                        rep_Receiving.CreateDocument();
                        rep_Receiving.PrintingSystem.ShowMarginsWarning = false;
                        rep_Receiving.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_Receiving.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Receiving;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_Receiving.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Receiving.CreateDocument();
                    rep_Receiving.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Receiving);
                    }
                    finally
                    {
                        rep_Receiving.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Receiving.CreateDocument();
                    rep_Receiving.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "recv.pdf";
                        GeneralFunctions.EmailReport(rep_Receiving, attachfile, "Receiving Order");
                    }
                    finally
                    {
                        rep_Receiving.Dispose();
                        dtbl.Dispose();
                    }
                }

            }
        }

        private async Task ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                int intRowID = 0;
                if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return;
                intRowID = gridView1.FocusedRowHandle;
                if (intRowID < 0) return;
                if (strPrint == "Y")
                {
                    if (strSearchType == "Purchase Order") await ExecutePOReport(eventtype);
                    if (strSearchType == "Receiving Items") await ExecuteRecvReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            await ClickButton("Email");
        }

        private async void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            await ClickButton("Preview");
        }

        private void BtnPrintAll_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                if ((grdPO.ItemsSource as DataTable).Rows.Count > 0)
                {
                    if (Settings.ReportHeader == "")
                    {
                        DocMessage.MsgInformation("Enter Store Details in Settings");
                        return;
                    }

                    DataTable dtblC = new DataTable();
                    dtblC = grdPO.ItemsSource as DataTable;

                    XtraReport rpt = new XtraReport();

                    foreach (DataRow dr in dtblC.Rows)
                    {
                        int rcd = GeneralFunctions.fnInt32(dr["ID"].ToString());


                        //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                        DataTable dtbl = new DataTable();
                        PosDataObject.Recv objRecv = new PosDataObject.Recv();
                        objRecv.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl = objRecv.FetchHeaderRecordForReport(rcd);
                        OfflineRetailV2.Report.repReceiving rep_Receiving = new OfflineRetailV2.Report.repReceiving();
                        GeneralFunctions.MakeReportWatermark(rep_Receiving);
                        rep_Receiving.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                        rep_Receiving.rReportHeader.Text = Settings.ReportHeader_Address;
                        rep_Receiving.DecimalPlace = Settings.DecimalPlace;
                        rep_Receiving.Report.DataSource = dtbl;
                        rep_Receiving.GroupHeader1.GroupFields.Add(rep_Receiving.CreateGroupField("ID"));

                        rep_Receiving.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
                        rep_Receiving.rAdd1.DataBindings.Add("Text", dtbl, "VAddress");
                        rep_Receiving.rAdd2.DataBindings.Add("Text", dtbl, "VOthers");
                        rep_Receiving.rProduct.DataBindings.Add("Text", dtbl, "ProductName");
                        rep_Receiving.rPartNo.DataBindings.Add("Text", dtbl, "VendorPartNo");
                        rep_Receiving.rQty.DataBindings.Add("Text", dtbl, "DQty");
                        rep_Receiving.rPrice.DataBindings.Add("Text", dtbl, "DCost");
                        rep_Receiving.rFreight.DataBindings.Add("Text", dtbl, "DFreight");
                        rep_Receiving.rTax.DataBindings.Add("Text", dtbl, "DTax");
                        rep_Receiving.rTotPrice.DataBindings.Add("Text", dtbl, "DExtCost");
                        rep_Receiving.rOrderNo.DataBindings.Add("Text", dtbl, "PurchaseOrder");
                        rep_Receiving.rOrderDate.DataBindings.Add("Text", dtbl, "DateOrdered");
                        rep_Receiving.rInvoiceNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
                        rep_Receiving.rReceiveDate.DataBindings.Add("Text", dtbl, "ReceiveDate");
                        rep_Receiving.rCheckClerk.DataBindings.Add("Text", dtbl, "CheckInClerk");
                        rep_Receiving.rReceiveClerk.DataBindings.Add("Text", dtbl, "ReceivingClerk");
                        rep_Receiving.rNotes.DataBindings.Add("Text", dtbl, "Note");

                        rep_Receiving.rSubtatal.DataBindings.Add("Text", dtbl, "GrossAmount");
                        rep_Receiving.rTotFreight.DataBindings.Add("Text", dtbl, "Freight");
                        rep_Receiving.rTotTax.DataBindings.Add("Text", dtbl, "Tax");
                        rep_Receiving.rGrandTotal.DataBindings.Add("Text", dtbl, "InvoiceTotal");

                        rep_Receiving.CreateDocument();
                        rep_Receiving.PrintingSystem.ShowMarginsWarning = false;

                        rpt.Pages.AddRange(rep_Receiving.Pages);
                        rpt.PrintingSystem.ContinuousPageNumbering = true;
                    }

                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rpt.PrinterName = Settings.ReportPrinterName;
                        rpt.PrintingSystem.ShowMarginsWarning = false;
                        rpt.PrintingSystem.ShowPrintStatusDialog = false;
                        GeneralFunctions.PrintReport(rpt);
                    }
                    finally
                    {
                        rpt.Dispose();
                        dtblC.Dispose();
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnPreviewAll_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (grdPO.ItemsSource != null && (grdPO.ItemsSource as DataTable).Rows.Count > 0)
                {
                    if (Settings.ReportHeader == "")
                    {
                        DocMessage.MsgInformation("Enter Store Details in Settings");
                        return;
                    }

                    DataTable dtblC = new DataTable();
                    dtblC = grdPO.ItemsSource as DataTable;

                    XtraReport rpt = new XtraReport();

                    foreach (DataRow dr in dtblC.Rows)
                    {
                        int rcd = GeneralFunctions.fnInt32(dr["ID"].ToString());


                        //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                        DataTable dtbl = new DataTable();
                        PosDataObject.Recv objRecv = new PosDataObject.Recv();
                        objRecv.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl = objRecv.FetchHeaderRecordForReport(rcd);
                        OfflineRetailV2.Report.repReceiving rep_Receiving = new OfflineRetailV2.Report.repReceiving();
                        GeneralFunctions.MakeReportWatermark(rep_Receiving);
                        rep_Receiving.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                        rep_Receiving.rReportHeader.Text = Settings.ReportHeader_Address;
                        rep_Receiving.DecimalPlace = Settings.DecimalPlace;
                        rep_Receiving.Report.DataSource = dtbl;
                        rep_Receiving.GroupHeader1.GroupFields.Add(rep_Receiving.CreateGroupField("ID"));

                        rep_Receiving.rVendor.DataBindings.Add("Text", dtbl, "Vendor");
                        rep_Receiving.rAdd1.DataBindings.Add("Text", dtbl, "VAddress");
                        rep_Receiving.rAdd2.DataBindings.Add("Text", dtbl, "VOthers");
                        rep_Receiving.rProduct.DataBindings.Add("Text", dtbl, "ProductName");
                        rep_Receiving.rPartNo.DataBindings.Add("Text", dtbl, "VendorPartNo");
                        rep_Receiving.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");
                        rep_Receiving.rQty.DataBindings.Add("Text", dtbl, "DQty");
                        rep_Receiving.rPrice.DataBindings.Add("Text", dtbl, "DCost");
                        rep_Receiving.rFreight.DataBindings.Add("Text", dtbl, "DFreight");
                        rep_Receiving.rTax.DataBindings.Add("Text", dtbl, "DTax");
                        rep_Receiving.rTotPrice.DataBindings.Add("Text", dtbl, "DExtCost");
                        rep_Receiving.rOrderNo.DataBindings.Add("Text", dtbl, "PurchaseOrder");
                        rep_Receiving.rOrderDate.DataBindings.Add("Text", dtbl, "DateOrdered");
                        rep_Receiving.rInvoiceNo.DataBindings.Add("Text", dtbl, "InvoiceNo");
                        rep_Receiving.rReceiveDate.DataBindings.Add("Text", dtbl, "ReceiveDate");
                        rep_Receiving.rCheckClerk.DataBindings.Add("Text", dtbl, "CheckInClerk");
                        rep_Receiving.rReceiveClerk.DataBindings.Add("Text", dtbl, "ReceivingClerk");
                        rep_Receiving.rNotes.DataBindings.Add("Text", dtbl, "Note");

                        rep_Receiving.rSubtatal.DataBindings.Add("Text", dtbl, "GrossAmount");
                        rep_Receiving.rTotFreight.DataBindings.Add("Text", dtbl, "Freight");
                        rep_Receiving.rTotTax.DataBindings.Add("Text", dtbl, "Tax");
                        rep_Receiving.rGrandTotal.DataBindings.Add("Text", dtbl, "InvoiceTotal");

                        rep_Receiving.CreateDocument();
                        rep_Receiving.PrintingSystem.ShowMarginsWarning = false;

                        rpt.Pages.AddRange(rep_Receiving.Pages);
                        rpt.PrintingSystem.ContinuousPageNumbering = true;
                    }

                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rpt.PrinterName = Settings.ReportPrinterName;
                        rpt.PrintingSystem.ShowMarginsWarning = false;
                        rpt.PrintingSystem.ShowPrintStatusDialog = false;
                        rpt.ShowPreviewDialog();

                    }
                    finally
                    {
                        rpt.Dispose();
                        dtblC.Dispose();
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
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
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
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
