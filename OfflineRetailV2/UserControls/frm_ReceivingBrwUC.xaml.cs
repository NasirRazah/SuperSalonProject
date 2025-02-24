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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_ReceivingBrwUC.xaml
    /// </summary>
    public partial class frm_ReceivingBrwUC : UserControl
    {
        private int intSelectedRowHandle;
        private bool blFlag;
        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }

        public frm_ReceivingBrwUC()
        {
            InitializeComponent();
        }

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Ordering");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdRecv.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdRecv, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public int GetID()
        {
            return 0;
            /*int intRowID = 0;
            if ((grdPO.ItemsSource as ICollection).Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((GeneralFunctions.GetCellValue(intRowID, gridView1, colName)));*/
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdRecv, colID)));
            return intRecID;
        }

        public async Task<int> ReturnBatchID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdRecv, colBatchNo)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void SetDecimalPlace()
        {
            if (Settings.Use4Decimal == "N")
            {
                if (Settings.DecimalPlace == 3)
                {
                    colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colGross.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colInvoiceTot.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
                else
                {
                    colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colGross.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colInvoiceTot.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
            }
            else
            {
                colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colGross.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colInvoiceTot.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
            }
        }

        public void FetchData(int fVendor, DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Recv objRecv = new PosDataObject.Recv();
            objRecv.Connection = SystemVariables.Conn;
            dbtbl = objRecv.FetchRecvHeader(fVendor, fFrmDate, fToDate, "");


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



            grdRecv.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        public void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            objVendor.DataObjectCulture_All = "All";// Todo:Settings.DataObjectCulture_All;
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

            lkupVendor.ItemsSource = dtblTemp;
            lkupVendor.EditValue = "0";


        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssRecvAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_ReceivingDlg frm_Receiving = new frm_ReceivingDlg();
            try
            {
                frm_Receiving.ID = 0;
                frm_Receiving.BrowseForm = this;
                frm_Receiving.ShowDialog();
                intNewRecID = frm_Receiving.NewID;
            }
            finally
            {
                frm_Receiving.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssRecvEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_ReceivingDlg frm_Receiving = new frm_ReceivingDlg();
            try
            {
                frm_Receiving.ID = await ReturnRowID();
                if (frm_Receiving.ID > 0)
                {

                    frm_Receiving.BrowseForm = this;
                    frm_Receiving.ShowDialog();
                    intNewRecID = frm_Receiving.ID;
                }
            }
            finally
            {
                frm_Receiving.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssRecvDelete) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnBatchID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.received_items))
                {
                    PosDataObject.Recv objRecv = new PosDataObject.Recv();
                    objRecv.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objRecv.ConnString = SystemVariables.ConnectionString;
                    objRecv.ID = intRecdID;
                    objRecv.TerminalName = Settings.TerminalName;
                    objRecv.BeginTransaction();
                    bool blFlag = objRecv.DeleteRecv();
                    objRecv.EndTransaction();
                    string ErrMsg = objRecv.ErrorMsg;

                    if (ErrMsg != "")
                    {
                        DocMessage.ShowException("Deleting Receipt item", ErrMsg);
                    }
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                    if ((grdRecv.ItemsSource as DataTable).Rows.Count > 0)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void LkupVendor_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void FDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void TDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void LkupVendor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssRecvPrint) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                return;
            }
            if ((grdRecv.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.Recv objRecv = new PosDataObject.Recv();
                objRecv.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objRecv.FetchHeaderRecordForReport(await ReturnRowID());
                OfflineRetailV2.Report.repReceiving rep_Receiving = new OfflineRetailV2.Report.repReceiving();
                rep_Receiving.DecimalPlace = Settings.Use4Decimal == "Y" ? 4 : Settings.DecimalPlace;
                GeneralFunctions.MakeReportWatermark(rep_Receiving);
                rep_Receiving.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Receiving.rReportHeader.Text = Settings.ReportHeader_Address;
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
                rep_Receiving.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
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
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void FDate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
