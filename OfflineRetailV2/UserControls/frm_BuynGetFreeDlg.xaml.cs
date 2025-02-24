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
using System.Data.SqlClient;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_BuynGetFreeDlg.xaml
    /// </summary>
    public partial class frm_BuynGetFreeDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_BuynGetFreeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private string strPrev = "";
        private frm_BuynGetFreeBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private bool blViewMode;

        private DataTable dtblFreeItem = null;
        private int intFreeItemID = 0;
        private string strFreeItemTxt = "";
        private int intFreeQty = 0;

        public bool ViewMode
        {
            get { return blViewMode; }
            set { blViewMode = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public frm_BuynGetFreeBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_BuynGetFreeBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateBuyItemlkup()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupBuyItem();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbBuyItem.ItemsSource = dtblTemp;
        }

        private async Task EditFreeItem()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            if ((grdFree.ItemsSource as DataTable).Rows.Count == 0) return;
            
            int refBuy = GeneralFunctions.fnInt32(cmbBuyItem.EditValue);

            if (refBuy == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Select_Buy_Item);
                GeneralFunctions.SetFocus(cmbBuyItem);
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_FreeItemDlg frmdlg = new frm_FreeItemDlg();
            try
            {
                frmdlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdFree, colSL));
                frmdlg.BuyItemID = refBuy;
                frmdlg.ITMID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdFree, colFreeItemID));
                frmdlg.ITMQTY = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdFree, colFreeQty));
                frmdlg.ShowDialog();
                if (frmdlg.OK)
                {
                    intFreeItemID = frmdlg.ITMID;
                    strFreeItemTxt = frmdlg.ITMTXT;
                    intFreeQty = frmdlg.ITMQTY;
                    GetFreeData(frmdlg.ID);
                    boolControlChanged = true;
                }
            }
            finally
            {
                frmdlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void GetFreeData(int iAddEdit)
        {
            bool f = false;
            foreach (DataRow dr in dtblFreeItem.Rows)
            {
                if (dr["Del"].ToString() == "Y") continue;
                if (iAddEdit == 0) // Check if free item already exists in ADD mode
                {
                    if (GeneralFunctions.fnInt32(dr["ItemID"].ToString()) == intFreeItemID)
                    {
                        f = true;
                        break;
                    }
                }

                if (iAddEdit > 0) // Check if free item already exists in Edit mode
                {
                    if ((GeneralFunctions.fnInt32(dr["ItemID"].ToString()) == intFreeItemID) && (GeneralFunctions.fnInt32(dr["SL"].ToString()) != iAddEdit))
                    {
                        f = true;
                        break;
                    }
                }
            }

            if (f)
            {
                return;
            }

            if (iAddEdit == 0)
            {
                if (intFreeItemID > 0)
                {
                    dtblFreeItem.Rows.Add(new object[] { "0", strFreeItemTxt, intFreeQty, intFreeItemID.ToString(), "N", dtblFreeItem.Rows.Count + 1, GeneralFunctions.GetImageAsByteArray() });
                }
            }

            if (iAddEdit > 0)
            {
                if (intFreeItemID > 0)
                {
                    foreach (DataRow dr in dtblFreeItem.Rows)
                    {
                        if (dr["Del"].ToString() == "Y") continue;
                        if (GeneralFunctions.fnInt32(dr["SL"].ToString()) == iAddEdit)
                        {
                            dr["Item"] = strFreeItemTxt;
                            dr["ItemID"] = intFreeItemID;
                            dr["Qty"] = intFreeQty;
                            break;
                        }
                    }
                }
            }




            grdFree.ItemsSource = dtblFreeItem;
        }



        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;


            

            int refBuy = GeneralFunctions.fnInt32(cmbBuyItem.EditValue);

            if (refBuy == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Select_Buy_Item);
                GeneralFunctions.SetFocus(cmbBuyItem);
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_FreeItemDlg frmdlg = new frm_FreeItemDlg();
            try
            {
                frmdlg.ID = 0;
                frmdlg.BuyItemID = refBuy;
                frmdlg.ShowDialog();
                if (frmdlg.OK)
                {
                    intFreeItemID = frmdlg.ITMID;
                    strFreeItemTxt = frmdlg.ITMTXT;
                    intFreeQty = frmdlg.ITMQTY;
                    GetFreeData(0);
                    boolControlChanged = true;
                }
            }
            finally
            {
                frmdlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditFreeItem();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            if ((grdFree.ItemsSource as DataTable).Rows.Count == 0) return;
            grdFree.SetCellValue(gridView1.FocusedRowHandle, colDelete, "Y");
            grdFree.RefreshData();
            boolControlChanged = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private bool SaveData()
        {
            

            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.DiscountName = txtName.Text.Trim();
            objClass.DiscountDescription = txtDesc.Text.Trim();

            gridView1.PostEditor();
            objClass.SplitDataTable = grdFree.ItemsSource as DataTable;

            if (chkActive.IsChecked == true)
            {
                objClass.DiscountStatus = "Y";
            }
            else
            {
                objClass.DiscountStatus = "N";
            }



            objClass.BuyQty = GeneralFunctions.fnInt32(txtQty.Text);
            objClass.BuyProductID = GeneralFunctions.fnInt32(cmbBuyItem.EditValue);
            objClass.LimitedPeriodCheck = chkPeriod.IsChecked == true ? "Y" : "N";

            objClass.LimitedStartDate = chkPeriod.IsChecked == true ? dtStart.DateTime.Date : Convert.ToDateTime(null);
            objClass.LimitedEndDate = chkPeriod.IsChecked == true ? dtFinish.DateTime.Date : Convert.ToDateTime(null);

            objClass.ID = intID;

            objClass.BeginTransaction();
            bool ret = objClass.ProcessBuyNGetFree();
            if (ret)
            {
                intNewID = objClass.ID;
            }
            objClass.EndTransaction();

            if (ret)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Name);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }

            if (intID == 0)
            {
                if (DuplicateCount() > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Name_already_exists);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if (intID > 0)
            {
                if ((strPrev != txtName.Text.Trim()) && (DuplicateCount() > 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.Name_already_exists);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            

            if (cmbBuyItem.EditValue == null)
            {
                DocMessage.MsgInformation(Properties.Resources.Select_Buy_Item);
                GeneralFunctions.SetFocus(cmbBuyItem);
                return false;
            }

            if (chkPeriod.IsChecked == true)
            {
                if ((dtStart.EditValue == null) || (dtFinish.EditValue == null))
                {
                    DocMessage.MsgEnter(Properties.Resources.Valid_Limited_Period);
                    GeneralFunctions.SetFocus(dtStart);
                    return false;
                }
                if ((dtFinish.DateTime.Date >= dtStart.DateTime.Date) /*&& (dtFinish.DateTime.Date >= DateTime.Today.Date)*/)
                {
                }
                else
                {
                    DocMessage.MsgEnter(Properties.Resources.Valid_Limited_Period);
                    GeneralFunctions.SetFocus(dtStart);
                    return false;
                }
            }




            int chk = 0;
            gridView1.PostEditor();

            foreach (DataRow dr in dtblFreeItem.Rows)
            {
                if (dr["Del"].ToString() == "N") chk++;
            }


            if (chk == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Free_Item_selected);
                GeneralFunctions.SetFocus(grdFree);
                return false;
            }

            if (GeneralFunctions.fnInt32(txtQty.Text) < 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Qty);
                GeneralFunctions.SetFocus(txtQty);
                return false;
            }


            if (chkActive.IsChecked == true)
            {
                DateTime f = DateTime.Today;
                DateTime t = DateTime.Today;
                if (chkPeriod.IsChecked == true)
                {
                    f = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                    GeneralFunctions.fnDate(dtStart.DateTime).Day, 0, 0, 0);

                    t = new DateTime(GeneralFunctions.fnDate(dtFinish.DateTime).Year, GeneralFunctions.fnDate(dtFinish.DateTime).Month,
                    GeneralFunctions.fnDate(dtFinish.DateTime).Day, 0, 0, 0);
                }

                PosDataObject.Discounts objClass = new PosDataObject.Discounts();
                objClass.Connection = SystemVariables.Conn;
                bool boverlap = objClass.IsOverlappingPromotion(intID, GeneralFunctions.fnInt32(cmbBuyItem.EditValue), GeneralFunctions.fnInt32(txtQty.Text),
                                                                chkPeriod.IsChecked == true ? true : false, f, t);

                if (boverlap)
                {
                    DocMessage.MsgInformation(Properties.Resources.This_combination_already_exists_in_another_active_promotion);
                    GeneralFunctions.SetFocus(cmbBuyItem);
                    return false;
                }
            }

            return true;
        }

        private int DuplicateCount()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            return objClass.DuplicateBuynGetFreeCount(txtName.Text.Trim());
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void ChkPeriod_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (chkPeriod.IsChecked == true)
            {
                lbs.Visibility = lbf.Visibility = dtStart.Visibility = dtFinish.Visibility = Visibility.Visible;
                dtStart.EditValue = DateTime.Today.Date;
                dtFinish.EditValue = DateTime.Today.AddMonths(1).Date;
            }
            else
            {
                lbs.Visibility = lbf.Visibility = dtStart.Visibility = dtFinish.Visibility = Visibility.Hidden;
                dtStart.EditValue = dtFinish.EditValue = null;
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditFreeItem();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();

            dtblFreeItem = new DataTable();
            dtblFreeItem.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("Item", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("Qty", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("ItemID", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("Del", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("SL", System.Type.GetType("System.String"));
            dtblFreeItem.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            grdFree.ItemsSource = dtblFreeItem;
            PopulateBuyItemlkup();
            lbs.Visibility = lbf.Visibility = dtStart.Visibility = dtFinish.Visibility = Visibility.Hidden;
            dtStart.EditValue = dtFinish.EditValue = null;
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Buy_N_Get_Free;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Buy_N_Get_Free;
                ShowHeader();
                ShowDetails();
            }
            boolControlChanged = false;
        }

        public void ShowHeader()
        {

            PosDataObject.Discounts fq = new PosDataObject.Discounts();
            fq.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = fq.ShowBuyNGetFreeHeader(intID);

            foreach (DataRow dr in dtbl.Rows)
            {
                txtName.Text = dr["PromotionName"].ToString();
                txtDesc.Text = dr["PromotionDescription"].ToString();

                cmbBuyItem.EditValue = dr["BuyProductID"].ToString();

                txtQty.Text = dr["BuyQty"].ToString();

                chkPeriod.IsChecked = dr["ForLimitedPeriod"].ToString() == "Y";

                if (dr["PromotionStartDate"].ToString() != "") dtStart.EditValue = GeneralFunctions.fnDate(dr["PromotionStartDate"].ToString()).Date;
                if (dr["PromotionEndDate"].ToString() != "") dtFinish.EditValue = GeneralFunctions.fnDate(dr["PromotionEndDate"].ToString()).Date;

                if (dr["PromotionStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

            }
            dtbl.Dispose();
            strPrev = txtName.Text;
        }

        public void ShowDetails()
        {
            PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl1 = new DataTable();
            dtblFreeItem = fq1.FetchFreeItemDetails(intID);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblFreeItem.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }



            grdFree.ItemsSource = dtblTemp;
        }

        private void GrdFree_CustomRowFilter(object sender, DevExpress.Xpf.Grid.RowFilterEventArgs e)
        {
            if ((e.Source.ItemsSource as DataTable).Rows[e.ListSourceRowIndex]["Del"].ToString() == "N")
            {
                e.Visible = true;
                e.Handled = false;
            }
            if ((e.Source.ItemsSource as DataTable).Rows[e.ListSourceRowIndex]["Del"].ToString() == "Y")
            {
                e.Visible = false;
                e.Handled = true;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.FetchData();
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Unchecked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbBuyItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
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

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
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
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
