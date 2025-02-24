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
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_JournalBrwUC.xaml
    /// </summary>
    public partial class frm_JournalBrwUC : UserControl
    {
        private bool blFlag;

        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }

        public frm_JournalBrwUC()
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
            fParent.RedirectToSubmenu("Items");
        }

        public void PopulateAction()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "Stock In", Properties.Resources.Stock_In });
            dtbl.Rows.Add(new object[] { "Stock Out", Properties.Resources.Stock_Out });
            dtbl.Rows.Add(new object[] { "ALL", Properties.Resources.All });
            cmbAction.ItemsSource = dtbl;
            cmbAction.EditValue = "ALL";
        }

        public void PopulateProduct()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.DataObjectCulture_All = Settings.DataObjectCulture_All;
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchJournalLookup();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSKU.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbItem.ItemsSource = dtblTemp;
            cmbItem.EditValue = "0";
            dbtblSKU.Dispose();
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdJournal.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdJournal, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdJournal.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdJournal, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdJournal.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdJournal, colID)));
            return intRecID;
        }

        public void FetchData(DateTime fFrmDate, DateTime fToDate, string Filter, int p)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.StockJournal objsj = new PosDataObject.StockJournal();
            objsj.Connection = SystemVariables.Conn;
            dbtbl = objsj.FetchData(fFrmDate, fToDate, Filter, p, SystemVariables.DateFormat);

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



            grdJournal.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            int intNewRecID = 0;
            frm_JournalDlg frm_JournalDlg = new frm_JournalDlg();
            try
            {
                frm_JournalDlg.BrowseForm = this;
                frm_JournalDlg.ShowDialog();
                intNewRecID = frm_JournalDlg.NewID;
            }
            finally
            {
                frm_JournalDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private void FDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()), cmbAction.EditValue.ToString(),
                        GeneralFunctions.fnInt32(cmbItem.EditValue));
                }
            }
        }

        private void CmbItem_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()), cmbAction.EditValue.ToString(),
                        GeneralFunctions.fnInt32(cmbItem.EditValue));
                }
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                DataTable dtbl = new DataTable();
                dtbl = grdJournal.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep10CList rep_list = new OfflineRetailV2.Report.Misc.rep10CList();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "Stock Journal";
                rep_list.rFilter.Text = "from" + " " + FDate.EditValue.ToString() + " " + "to" + " " + TDate.EditValue.ToString() + "\n\r"
                    + "Action" + " : " + cmbAction.EditValue.ToString() + "\n\r" + (GeneralFunctions.fnInt32(cmbItem.EditValue) > 0 ? "Product" + " : " + cmbItem.GetDisplayValue(0) : "All Products");



                rep_list.rCol1.DataBindings.Add("Text", dtbl, "DocDate");
                rep_list.rCol2.DataBindings.Add("Text", dtbl, "DocNo");
                rep_list.rCol3.DataBindings.Add("Text", dtbl, "TranType");
                rep_list.rCol4.DataBindings.Add("Text", dtbl, "TranSubType");
                rep_list.rCol5.DataBindings.Add("Text", dtbl, "SKU");
                rep_list.rCol6.DataBindings.Add("Text", dtbl, "Qty");
                rep_list.rCol7.DataBindings.Add("Text", dtbl, "StockOnHand");
                rep_list.rCol8.DataBindings.Add("Text", dtbl, "Cost");
                rep_list.rCol9.DataBindings.Add("Text", dtbl, "EmployeeID");
                rep_list.rCol10.DataBindings.Add("Text", dtbl, "TerminalName");

                if (Settings.ReportPrinterName != "") rep_list.PrinterName = Settings.ReportPrinterName;
                rep_list.CreateDocument();
                rep_list.PrintingSystem.ShowMarginsWarning = false;

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;

                //rep_list.ShowPreviewDialog();

                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                window.PreviewControl.DocumentSource = rep_list;
                window.ShowDialog();

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                rep_list.Dispose();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void CmbAction_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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
