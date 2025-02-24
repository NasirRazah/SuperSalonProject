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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_Mix_n_MatchBrwUC.xaml
    /// </summary>
    public partial class frm_Mix_n_MatchBrwUC : UserControl
    {
        public frm_Mix_n_MatchBrwUC()
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
            for (intColCtr = 0; intColCtr < (grdD.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdD, colID)));
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
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdD, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdD, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            dbtbl = objClass.FetchMixNmatchData();

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



            grdD.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();


            
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            POSSection.frmMix_n_MatchDlg matchDlg = new POSSection.frmMix_n_MatchDlg();
            try
            {
                matchDlg.ID = 0;
                matchDlg.ViewMode = false;
                //frm_DiscountDlg.InitialiseScreen();
                matchDlg.BrowseForm = this;
                matchDlg.ShowDialog();
                intNewRecID = matchDlg.NewID;
            }
            finally
            {
                matchDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            POSSection.frmMix_n_MatchDlg matchDlg = new POSSection.frmMix_n_MatchDlg();
            try
            {
                matchDlg.ID = await ReturnRowID();
                if (matchDlg.ID > 0)
                {
                    matchDlg.ViewMode = false;
                    matchDlg.BrowseForm = this;
                    matchDlg.ShowDialog();
                    intNewRecID = matchDlg.ID;
                }
            }
            finally
            {
                matchDlg.Close();
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
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;

           int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdD, colID));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Mix_and_Match))
                {
                    PosDataObject.Discounts objCustomer = new PosDataObject.Discounts();
                    objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intrError = objCustomer.DeleteMixNmatch(intRecdID);
                    if (intrError != 0)
                    {
                        if (intrError == 1)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_mix_and_match_as_it_is_already_used_in + " " + Properties.Resources.orders);
                        if (intrError == 2)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_mix_and_match_as_it_is_already_used_in + " " + Properties.Resources.suspend_orders);
                        if (intrError == 3)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_mix_and_match_as_it_is_already_used_in + " " + Properties.Resources.workorders);
                        return;
                    }
                    else FetchData();
                }
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;
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
                dtblH = fq.ShowMixNmatchHeader(await ReturnRowID());

                int FmlyID = 0;
                string Fmly = "";

                foreach (DataRow dr in dtblH.Rows)
                {
                    FmlyID = GeneralFunctions.fnInt32(dr["DiscountFamilyID"].ToString());
                    break;
                }

                if (FmlyID > 0)
                {
                    PosDataObject.Brand fqf = new PosDataObject.Brand();
                    fqf.Connection = SystemVariables.Conn;
                    Fmly = fqf.GetBrand(FmlyID);
                }

                DataTable dtblD = new DataTable();
                dtblD.Columns.Add("FamilyID", System.Type.GetType("System.String"));
                dtblD.Columns.Add("Family", System.Type.GetType("System.String"));
                dtblD.Columns.Add("Item", System.Type.GetType("System.String"));


                PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
                fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);

                DataTable dtbl1 = new DataTable();
                dtbl1 = fq1.FetchMixNmatchDetails(await ReturnRowID());

                foreach (DataRow dr in dtbl1.Rows)
                {
                    string v = "";
                    PosDataObject.Product fq2 = new PosDataObject.Product();
                    fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    DataTable dp = new DataTable();
                    dp = fq2.ShowRecord(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));
                    int pFamily = 0;
                    foreach (DataRow dr1 in dp.Rows)
                    {
                        pFamily = GeneralFunctions.fnInt32(dr1["BrandID"].ToString());
                        v = dr1["Description"].ToString() + "   (" + dr1["SKU"].ToString() + ")";
                    }
                    dp.Dispose();


                    if (pFamily == 0)
                    {
                        dtblD.Rows.Add(new object[] { pFamily, "Non Family", v });

                    }
                    else
                    {
                        dtblD.Rows.Add(new object[] { pFamily, Fmly, v });
                    }

                }




                OfflineRetailV2.Report.Discount.repMixNMatch rep = new OfflineRetailV2.Report.Discount.repMixNMatch();
                GeneralFunctions.MakeReportWatermark(rep);
                rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep.rReportHeader.Text = Settings.ReportHeader_Address;

                foreach (DataRow dr in dtblH.Rows)
                {
                    rep.rBatch.Text = dr["DiscountName"].ToString();
                    rep.rBatchD.Text = dr["DiscountDescription"].ToString();
                    if (dr["DiscountStatus"].ToString() == "Y")
                    {
                        rep.rStatus.Text = "Active";
                    }
                    else
                    {
                        rep.rStatus.Text = "Inactive";
                    }


                    if (dr["DiscountType"].ToString() == "A")
                    {
                        rep.rValueOff.Text = "Amount " + GeneralFunctions.fnDouble(dr["DiscountAmount"].ToString()).ToString("f2") + " off";
                    }
                    if (dr["DiscountType"].ToString() == "P")
                    {
                        rep.rValueOff.Text = GeneralFunctions.fnDouble(dr["DiscountPercentage"].ToString()).ToString("#.##") + "% off";
                    }

                    if (GeneralFunctions.fnInt32(dr["BasedOn"].ToString()) == 0) rep.rBased.Text = "Item Qty";
                    if (GeneralFunctions.fnInt32(dr["BasedOn"].ToString()) == 1) rep.rBased.Text = "Amount";

                    if (dr["DiscountCategory"].ToString() == "N")
                    {
                        rep.rCategory.Text = "Normal Pricing";
                    }

                    if (dr["DiscountCategory"].ToString() == "P")
                    {
                        rep.rCategory.Text = "+ Pricing";
                    }

                    if (dr["DiscountCategory"].ToString() == "A")
                    {
                        rep.rCategory.Text = "Absolute Pricing";
                    }

                    if (dr["DiscountCategory"].ToString() == "X")
                    {
                        rep.rCategory.Text = "";
                        rep.rlbCategory.Visible = false;
                    }

                    if (dr["LimitedPeriodCheck"].ToString() == "Y")
                    {
                        if (dr["OfferStartOn"].ToString() != "") rep.rFrom.Text = GeneralFunctions.fnDate(dr["OfferStartOn"].ToString()).ToString("MMM d, yyyy");
                        if (dr["OfferEndOn"].ToString() != "") rep.rTo.Text = GeneralFunctions.fnDate(dr["OfferEndOn"].ToString()).ToString("MMM d, yyyy");
                    }
                    else
                    {
                        rep.rlbPeriod1.Visible = rep.rlbPeriod2.Visible = rep.rFrom.Visible = rep.rTo.Visible = false;
                    }

                    rep.rItemCount.Text = dr["DiscountPlusQty"].ToString();
                    rep.rAbsPrice.Text = GeneralFunctions.fnDouble(dr["AbsolutePrice"].ToString()).ToString("f2");

                    if (GeneralFunctions.fnInt32(dr["BasedOn"].ToString()) == 0)
                    {
                        if (dr["DiscountCategory"].ToString() == "A")
                        {
                            rep.rlbAbsolutePrice.Visible = rep.rAbsPrice.Visible = true;
                        }
                        else
                        {
                            rep.rlbAbsolutePrice.Visible = rep.rAbsPrice.Visible = false;
                        }
                    }
                    else
                    {
                        rep.rlbAbsolutePrice.Text = "Min. Amount";
                        rep.rlbItemCount.Visible = rep.rItemCount.Visible = false;
                    }

                }

                dtblD.DefaultView.Sort = "FamilyID desc, Item asc";
                dtblD.DefaultView.ApplyDefaultSort = true;
                rep.Report.DataSource = dtblD;

                rep.rApplicable.DataBindings.Add("Text", dtblD, "Family");
                rep.rDesc.DataBindings.Add("Text", dtblD, "Item");
                //rep.rPrice.DataBindings.Add("Text", dtblD, "SalePrice");
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

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Mix and Matches")
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
                txtSearchGrdData.InfoText = "Search Mix and Matches";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Mix and Matches") return;
            if (txtSearchGrdData.Text == "")
            {
                grdD.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdD.FilterString = "([DiscountName] LIKE '%" + filterValue + "%')";
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
