using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POBrwUC.xaml
    /// </summary>
    public partial class frm_POBrwUC : UserControl
    {
        private int intSelectedRowHandle;
        private bool blFlag;
        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }
        public frm_POBrwUC()
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
            for (intColCtr = 0; intColCtr < (grdPO.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdPO, colID)));
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
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPO, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return;
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
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
                else
                {
                    colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                    colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                }
            }
            else
            {
                colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
                colNetAmount.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = DevExpress.Xpf.Editors.MaskType.Numeric };
            }
        }

        public void FetchData(int fVendor, DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            dbtbl = objPO.FetchPOHeader(fVendor, fFrmDate, fToDate, "");

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
            GeneralFunctions.SetRecordCountStatus(dbtbl.Rows.Count);
            dtblTemp.Dispose();
            dbtbl.Dispose();


        }

        private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((!SecurityPermission.AcssPOAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            int intNewRecID = 0;
            frm_PODlg frm_PODlg = new frm_PODlg();
            try
            {
                frm_PODlg.ID = 0;
                frm_PODlg.BrowseForm = this;
                frm_PODlg.ShowDialog();
                intNewRecID = frm_PODlg.NewID;
            }
            finally
            {
                frm_PODlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PODlg frm_PODlg = new frm_PODlg();
            try
            {
                frm_PODlg.ID = await ReturnRowID();
                if (frm_PODlg.ID > 0)
                {
                    
                    frm_PODlg.BrowseForm = this;
                    frm_PODlg.ShowDialog();
                    intNewRecID = frm_PODlg.ID;
                }
            }
            finally
            {
                frm_PODlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void barButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssPOAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PODlg frm_PODlg = new frm_PODlg();
            try
            {
                frm_PODlg.ID = 0;
                frm_PODlg.BrowseForm = this;
                frm_PODlg.ShowDialog();
                intNewRecID = frm_PODlg.NewID;
            }
            finally
            {
                frm_PODlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async void barButtonItem2_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssPOEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            await EditProcess();
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssPOPrint) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Enter_Store_Details_in_Settings_before_printing_the_report);
                return;
            }
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                DataTable dtbl = new DataTable();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objPO.FetchHeaderRecordForReport(await ReturnRowID());
                OfflineRetailV2.Report.repPO rep_PO = new OfflineRetailV2.Report.repPO();

                GeneralFunctions.MakeReportWatermark(rep_PO);
                rep_PO.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_PO.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_PO.DecimalPlace = Settings.Use4Decimal == "Y" ? 4 : Settings.DecimalPlace;
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

        private void editItemDateEdit1_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void FDate_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void TDate_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

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
        private async void barButtonItem3_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssPODelete) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdPO.ItemsSource as DataTable).Rows.Count == 0)
            {
                // DocMessage.MsgInformation(Properties.Resources."Please Select an Customer");
                return;
            }
            //SetRowIndex();

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Purchase Order"))
                {
                    PosDataObject.PO objPO = new PosDataObject.PO();
                    objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objPO.ID = intRecdID;
                    objPO.BeginTransaction();
                    bool blFlag = objPO.DeletePO();
                    objPO.EndTransaction();
                    string ErrMsg = objPO.ErrorMsg;
                    if ((ErrMsg == null) && (!blFlag))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_Purchase_Order_as_it_exists_in_Receipt_);
                        return;
                    }
                    if (ErrMsg != "")
                    {
                        DocMessage.ShowException("Deleting Purchase Order", ErrMsg);
                    }
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                    if ((grdPO.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private async void gridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssPOEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            await EditProcess();
        }

        private void lkupVendor_EditValueChanged(object sender, RoutedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void LkupVendor_EditValueChanged_1(object sender, RoutedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void FDate_EditValueChanged(object sender, RoutedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private void TDate_EditValueChanged(object sender, RoutedEventArgs e)
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

        private void LkupVendor_EditValueChanged_1(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((lkupVendor.EditValue != null) && (FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnInt32(lkupVendor.EditValue.ToString()), GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
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
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
