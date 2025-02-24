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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_SalePriceBrwUC.xaml
    /// </summary>
    public partial class frm_SalePriceBrwUC : UserControl
    {
        public frm_SalePriceBrwUC()
        {
            InitializeComponent();
        }

        private FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool bOpenKeyBrd = false;




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

        public void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private int intSelectedRowHandle;

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Discounts");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grd.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grd, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0)
            {
                gridView1.FocusedRowHandle = intColCtr;
            }
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grd, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grd, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void PopulateSaleBatchFilters()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "Active", Properties.Resources.Active });
            dtbl.Rows.Add(new object[] { "Inactive", Properties.Resources.Inactive });
            dtbl.Rows.Add(new object[] { "All", Properties.Resources.All });

            cmbFilter.ItemsSource = dtbl;

            cmbFilter.EditValue = "Active";
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            dbtbl = objClass.FetchSalePriceData(cmbFilter.EditValue.ToString(), SystemVariables.DateFormat);

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



            grd.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_SalePriceDlg frm_Dlg = new frm_SalePriceDlg();
            try
            {
                frm_Dlg.ID = 0;
                frm_Dlg.BrowseForm = this;
                frm_Dlg.ShowDialog();
                intNewRecID = frm_Dlg.NewID;
            }
            finally
            {
                frm_Dlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_SalePriceDlg frm_Dlg = new frm_SalePriceDlg();
            try
            {
                frm_Dlg.ID = await ReturnRowID();
                if (frm_Dlg.ID > 0)
                {

                    //Todo:frm_Dlg.ShowHeader();
                    frm_Dlg.BrowseForm = this;
                    frm_Dlg.ShowDialog();
                    intNewRecID = frm_Dlg.ID;
                }
            }
            finally
            {
                frm_Dlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grd, colID));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Sale_Batch))
                {
                    if (GeneralFunctions.CheckSaleBatchInOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_sale_batch_as_it_is_already_used_in_order);
                        return;
                    }

                    PosDataObject.Discounts objCustomer = new PosDataObject.Discounts();
                    objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intrError = objCustomer.DeleteSaleBatch(intRecdID);
                    if (intrError != 0)
                    {

                    }
                    FetchData();
                    if ((grd.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        gridView1.FocusedRowHandle = intRowID - 1;
                    }
                }
            }
        }

        private void CmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                    return;
                }

                PosDataObject.Discounts fq = new PosDataObject.Discounts();
                fq.Connection = SystemVariables.Conn;
                DataTable dtblH = new DataTable();
                dtblH = fq.ShowSalePriceHeader(await ReturnRowID());

                DataTable dtblD = new DataTable();
                dtblD.Columns.Add("ID", System.Type.GetType("System.String"));
                dtblD.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtblD.Columns.Add("ItemName", System.Type.GetType("System.String"));
                dtblD.Columns.Add("ApplyFamily", System.Type.GetType("System.String"));
                dtblD.Columns.Add("SalePrice", System.Type.GetType("System.Double"));

                PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
                fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);

                DataTable dtbl1 = new DataTable();
                dtbl1 = fq1.FetchSalePriceDetails(await ReturnRowID());

                foreach (DataRow dr in dtbl1.Rows)
                {
                    string v = "";
                    if (dr["ApplyFamily"].ToString() == "N")
                    {
                        PosDataObject.Product fq2 = new PosDataObject.Product();
                        fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        DataTable dp = new DataTable();
                        dp = fq2.ShowRecord(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));
                        foreach (DataRow dr1 in dp.Rows)
                        {
                            v = dr1["Description"].ToString() + "   (" + dr1["SKU"].ToString() + ")";
                        }
                        dp.Dispose();
                    }

                    if (dr["ApplyFamily"].ToString() == "Y")
                    {
                        PosDataObject.Brand fq2 = new PosDataObject.Brand();
                        fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        DataTable dp = new DataTable();
                        dp = fq2.ShowRecord(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));
                        foreach (DataRow dr1 in dp.Rows)
                        {
                            v = dr1["BrandDescription"].ToString() + "   (" + dr1["BrandID"].ToString() + ")";
                        }
                        dp.Dispose();
                    }
                    dr["ItemName"] = v;
                }

                dtblD = dtbl1;


                OfflineRetailV2.Report.Discount.repSaleBatch rep = new OfflineRetailV2.Report.Discount.repSaleBatch();
                GeneralFunctions.MakeReportWatermark(rep);
                rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep.rReportHeader.Text = Settings.ReportHeader_Address;

                foreach (DataRow dr in dtblH.Rows)
                {
                    rep.rBatch.Text = dr["SaleBatchName"].ToString();
                    rep.rBatchD.Text = dr["SaleBatchDesc"].ToString();
                    if (dr["SaleStatus"].ToString() == "Y")
                    {
                        rep.rStatus.Text = "Active";
                    }
                    else
                    {
                        rep.rStatus.Text = "Inactive";
                    }

                    rep.rFrom.Text = GeneralFunctions.fnDate(dr["EffectiveFrom"].ToString()).ToString("MMM d, yyyy  hh:mm tt");
                    rep.rTo.Text = GeneralFunctions.fnDate(dr["EffectiveTo"].ToString()).ToString("MMM d, yyyy  hh:mm tt");
                }

                dtblD.DefaultView.Sort = "ApplyFamily desc,ItemName asc";
                dtblD.DefaultView.ApplyDefaultSort = true;
                rep.Report.DataSource = dtblD;

                rep.rApplicable.DataBindings.Add("Text", dtblD, "ApplyFamily");
                rep.rDesc.DataBindings.Add("Text", dtblD, "ItemName");
                rep.rPrice.DataBindings.Add("Text", dtblD, "SalePrice");

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                try
                {
                    if (Settings.ReportPrinterName != "") rep.PrinterName = Settings.ReportPrinterName;
                    rep.CreateDocument();
                    rep.PrintingSystem.ShowMarginsWarning = false;

                    //rep.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep;
                    window.ShowDialog();
                }
                finally
                {
                    rep.Dispose();
                    dtblH.Dispose();
                    dtblD.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void CmbFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Sale Batches")
            {
                txtSearchGrdData.Text = "";
            }

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            if (bOpenKeyBrd) return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }

        private void TxtSearchGrdData_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.Text == "")
            {
                txtSearchGrdData.InfoText = "Search Sale Batches";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Sale Batches") return;
            if (txtSearchGrdData.Text == "")
            {
                grd.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grd.FilterString = "([SaleBatchName] LIKE '%" + filterValue + "%')";
            }
        }

        private void TxtSearchGrdData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutFullKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                bOpenKeyBrd = true;
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }
    }
}
