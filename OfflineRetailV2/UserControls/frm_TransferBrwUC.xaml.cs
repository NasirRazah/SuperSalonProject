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
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_TransferBrwUC.xaml
    /// </summary>
    public partial class frm_TransferBrwUC : UserControl
    {
        public frm_TransferBrwUC()
        {
            InitializeComponent();
        }

        private int intSelectedRowHandle;
        private bool blFlag;
        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
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
            for (intColCtr = 0; intColCtr < (grdTransfer.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdTransfer, colID)));
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
            if ((grdTransfer.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTransfer, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdTransfer.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void SetDecimalPlace()
        {
            if (Settings.Use4Decimal == "N")
            {
                if (Settings.DecimalPlace == 3)
                {
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
                else
                {
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
            }
            else
            {
                colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
            }
        }

        public void FetchData(DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.PO objTransfer = new PosDataObject.PO();
            objTransfer.Connection = SystemVariables.Conn;
            dbtbl = objTransfer.FetchTransferHeader(fFrmDate, fToDate);

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



            grdTransfer.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();


            
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssPOAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_TransferDlg frm_Transfer = new frm_TransferDlg();
            try
            {
                frm_Transfer.ID = 0;
                frm_Transfer.BrowseForm = this;
                frm_Transfer.ShowDialog();
                intNewRecID = frm_Transfer.NewID;
            }
            finally
            {
                frm_Transfer.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssPOEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdTransfer.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_TransferDlg frm_Transfer = new frm_TransferDlg();
            try
            {
                frm_Transfer.ID = await ReturnRowID();
                if (frm_Transfer.ID > 0)
                {

                    frm_Transfer.BrowseForm = this;
                    frm_Transfer.ShowDialog();
                    intNewRecID = frm_Transfer.ID;
                }
            }
            finally
            {
                frm_Transfer.Close();
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
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdTransfer.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Transfer_Record))
                {
                    PosDataObject.PO objPO = new PosDataObject.PO();
                    objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objPO.ID = intRecdID;
                    objPO.TerminalName = Settings.TerminalName;
                    objPO.BeginTransaction();
                    bool blFlag = objPO.DeleteTransfer();
                    objPO.EndTransaction();
                    string ErrMsg = objPO.ErrorMsg;
                    if ((ErrMsg == null) && (!blFlag))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_as_transfer_record_already_sent);
                        return;
                    }
                    if (ErrMsg != "")
                    {
                        DocMessage.ShowException("Deleting Transfer Record", ErrMsg);
                    }
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                    if ((grdTransfer.ItemsSource as DataTable).Rows.Count > 1)
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

        private void FDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void TDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }
            if ((grdTransfer.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objPO.FetchTransferRecordForReport(await ReturnRowID());
                OfflineRetailV2.Report.repTransfer rep_PO = new OfflineRetailV2.Report.repTransfer();

                GeneralFunctions.MakeReportWatermark(rep_PO);
                rep_PO.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_PO.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_PO.DecimalPlace = Settings.Use4Decimal == "Y" ? 4 : Settings.DecimalPlace;
                rep_PO.Report.DataSource = dtbl;


                rep_PO.rProduct.DataBindings.Add("Text", dtbl, "Product");
                rep_PO.rSKU.DataBindings.Add("Text", dtbl, "SKU");
                rep_PO.rQty.DataBindings.Add("Text", dtbl, "DQty");
                rep_PO.rPrice.DataBindings.Add("Text", dtbl, "DCost");
                rep_PO.rTotPrice.DataBindings.Add("Text", dtbl, "DTCost");
                rep_PO.rOrderNo.DataBindings.Add("Text", dtbl, "TransferNo");
                rep_PO.rOrderDate.DataBindings.Add("Text", dtbl, "TransferDate");
                rep_PO.rTransferTo.DataBindings.Add("Text", dtbl, "TransferTo");
                rep_PO.rStatus.DataBindings.Add("Text", dtbl, "TransferStatus");
                rep_PO.rNotes.DataBindings.Add("Text", dtbl, "Notes");
                rep_PO.rTQty.DataBindings.Add("Text", dtbl, "TQty");
                rep_PO.rTCost.DataBindings.Add("Text", dtbl, "TCost");

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
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
    }
}
