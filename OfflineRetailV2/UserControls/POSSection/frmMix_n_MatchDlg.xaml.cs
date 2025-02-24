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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmMix_n_MatchDlg.xaml
    /// </summary>
    public partial class frmMix_n_MatchDlg : Window
    {

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frmMix_n_MatchDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frm_Mix_n_MatchBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;

        private string strPrev = "";

        private string strSTIME = "";
        private string strETIME = "";

        private DataTable dtblNF = null;

        private DateTime dtStartDate = DateTime.Now;
        private DateTime dtEndDate = DateTime.Now;

        private bool blCallFromProduct;

        public bool CallFromProduct
        {
            get { return blCallFromProduct; }
            set { blCallFromProduct = value; }
        }

        private bool blViewMode;

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

        public frm_Mix_n_MatchBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_Mix_n_MatchBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateProductFamily()
        {
            PosDataObject.Brand objDept = new PosDataObject.Brand();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchLookupData2();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbFamily.ItemsSource = dtblTemp;
            dtbl.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();

            dtblNF = new DataTable();
            dtblNF.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblNF.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblNF.Columns.Add("Product", System.Type.GetType("System.String"));
            dtblNF.Columns.Add("BrandID", System.Type.GetType("System.String"));
            dtblNF.Columns.Add("chk", System.Type.GetType("System.Boolean"));
            dtblNF.Columns.Add("Image", System.Type.GetType("System.Byte[]"));
            grdItem.ItemsSource = dtblNF;
            PopulateProductFamily();
            grdItem.Visibility = Visibility.Hidden;
            rb1.IsChecked = true;
            rb11.IsChecked = true;
            numPerc.IsEnabled = true;
            numAbsolute.IsEnabled = false;

            lbs.Visibility = lbf.Visibility = dtStart.Visibility = dtFinish.Visibility = Visibility.Hidden;
            dtStart.EditValue = dtFinish.EditValue = null;
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Mix_and_Match;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Mix_and_Match;
                AddNonFamilyItemOnLoad();
                ShowHeader();
                strPrev = txtName.Text;
                ShowDetails();
            }
            boolControlChanged = false;
        }


        public void ShowHeader()
        {
            PosDataObject.Discounts fq = new PosDataObject.Discounts();
            fq.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = fq.ShowMixNmatchHeader(intID);

            foreach (DataRow dr in dtbl.Rows)
            {
                txtName.Text = dr["DiscountName"].ToString();
                txtDesc.Text = dr["DiscountDescription"].ToString();
                if (dr["DiscountType"].ToString() == "A")
                {
                    rb12.IsChecked = true;
                    numAbsolute.IsEnabled = true;
                    numPerc.IsEnabled = false;
                }
                if (dr["DiscountType"].ToString() == "P")
                {
                    rb11.IsChecked = true;
                    numAbsolute.IsEnabled = false;
                    numPerc.IsEnabled = true;
                }

                if (dr["DiscountCategory"].ToString() == "N")
                {
                    rb1.IsChecked = true;
                }

                if (dr["DiscountCategory"].ToString() == "P")
                {
                    rb2.IsChecked = true;
                }

                if (dr["DiscountCategory"].ToString() == "A")
                {
                    rb3.IsChecked = true;
                }

                if (dr["DiscountCategory"].ToString() == "X")
                {
                    rb1.IsChecked = true;
                }

                cmbType.SelectedIndex = GeneralFunctions.fnInt32(dr["BasedOn"].ToString());

                txtQty.Text = dr["DiscountPlusQty"].ToString();

                numAbsolute.Text = GeneralFunctions.fnDouble(dr["DiscountAmount"].ToString()).ToString("f2");
                numPerc.Text = GeneralFunctions.fnDouble(dr["DiscountPercentage"].ToString()).ToString("f2");

                chkPeriod.IsChecked = dr["LimitedPeriodCheck"].ToString() == "Y";

                if (dr["OfferStartOn"].ToString() != "") dtStart.EditValue = GeneralFunctions.fnDate(dr["OfferStartOn"].ToString()).Date;
                if (dr["OfferEndOn"].ToString() != "") dtFinish.EditValue = GeneralFunctions.fnDate(dr["OfferEndOn"].ToString()).Date;

                if (dr["DiscountStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

                cmbFamily.EditValue = dr["DiscountFamilyID"].ToString();

                numPrice.Text = GeneralFunctions.fnDouble(dr["AbsolutePrice"].ToString()).ToString("f2");
            }
            dtbl.Dispose();
        }

        public void ShowDetails()
        {
            PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl1 = new DataTable();

            dtbl1 = fq1.FetchMixNmatchDetails(intID);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtbl = grdItem.ItemsSource as DataTable;
            foreach (DataRow dr in dtbl.Rows)
            {
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    if (dr["ID"].ToString() == dr1["ItemID"].ToString())
                    {
                        dr["chk"] = true;
                    }
                }

                try
                {
                    dr["Image"] = strip;
                }
                catch (Exception)
                {

                  
                }
            }

            grdItem.ItemsSource = dtbl;
            dtbl.Dispose();
            dtbl1.Dispose();
        }


        private void AddNonFamilyItem(int ItemID, string ItemDesc)
        {
            // check if already exists

            bool exists = false;

            foreach (DataRow dr in dtblNF.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == ItemID)
                {
                    exists = true;
                    break;
                }
            }
            if (exists) return;

            string sku = "";
            PosDataObject.Product objP = new PosDataObject.Product();
            objP.Connection = SystemVariables.Conn;
            sku = objP.GetProductSKU(ItemID);
            dtblNF.Rows.Add(new object[] { ItemID.ToString(), sku, ItemDesc, "0", true, GeneralFunctions.GetImageAsByteArray() });

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
            dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("chk", System.Type.GetType("System.Boolean"));
            dtbl.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            if (grdItem.ItemsSource != null)
            {
                DataRow[] dRR = (grdItem.ItemsSource as DataTable).Select("BrandID <> 0");
                foreach (DataRow dr in dRR)
                {
                    dtbl.ImportRow(dr);
                }
            }

            foreach (DataRow dr in dtblNF.Rows)
            {
                dtbl.ImportRow(dr);
            }

            dtbl.DefaultView.Sort = "BrandID desc, Product desc";
            dtbl.DefaultView.ApplyDefaultSort = true;

            grdItem.ItemsSource = dtbl;
            grdItem.ExpandAllGroups();

            DataTable dtbl1 = new DataTable();
            dtbl1 = grdItem.ItemsSource as DataTable;

            int rH = -1;
            foreach (DataRow dr in dtbl1.Rows)
            {
                rH++;
                if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == ItemID)
                {
                    break;
                }
            }
            //Todo:grdItem.MakeRowVisible(rH);
            dtbl1.Dispose();
            dtbl.Dispose();

            grdItem.Visibility = (grdItem.ItemsSource as DataTable).Rows.Count > 0 ? Visibility.Visible : Visibility.Hidden;

        }

        private void AddNonFamilyItem()
        {

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
            dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("chk", System.Type.GetType("System.Boolean"));

            if (grdItem.ItemsSource != null)
            {
                DataRow[] dRR = (grdItem.ItemsSource as DataTable).Select("BrandID <> 0");
                foreach (DataRow dr in dRR)
                {
                    dtbl.ImportRow(dr);
                }
            }

            foreach (DataRow dr in dtblNF.Rows)
            {
                dtbl.ImportRow(dr);
            }

            dtbl.DefaultView.Sort = "BrandID desc, Product desc";
            dtbl.DefaultView.ApplyDefaultSort = true;

            grdItem.ItemsSource = dtbl;
            grdItem.ExpandAllGroups();

            dtbl.Dispose();
        }

        private void AddNonFamilyItemOnLoad()
        {
            PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl1 = new DataTable();

            dtbl1 = fq1.FetchMixNmatchDetails(intID);

            foreach (DataRow dr in dtbl1.Rows)
            {
                PosDataObject.Product objP = new PosDataObject.Product();
                objP.Connection = SystemVariables.Conn;
                DataTable dtbl2 = objP.ShowRecord(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));
                foreach (DataRow dr2 in dtbl2.Rows)
                {
                    int brd = GeneralFunctions.fnInt32(dr2["BrandID"].ToString());
                    string sku = dr2["SKU"].ToString();
                    string prd = dr2["Description"].ToString();
                    if (brd == 0)
                    {
                        dtblNF.Rows.Add(new object[] { dr["ItemID"].ToString(), sku, prd, "0", true });
                    }
                }
                dtbl2.Dispose();
            }
        }

        private void Rb1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if ((rb1.IsChecked == true) || (rb2.IsChecked == true))
            {
                lbAbsolute.Visibility = Visibility.Hidden;
                numPrice.Visibility = Visibility.Hidden;
                numPrice.Text = "0.00";
                rb11.Visibility = rb12.Visibility = Visibility.Visible;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Visible;
            }
            else
            {
                lbAbsolute.Visibility = Visibility.Visible;
                numPrice.Visibility = Visibility.Visible;
                rb11.Visibility = rb12.Visibility = Visibility.Hidden;
                rb11.IsChecked = true;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Hidden;
                numPerc.Text = numAbsolute.Text = "0.00";
            }
        }

        private void Rb2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if ((rb1.IsChecked == true) || (rb2.IsChecked == true))
            {
                lbAbsolute.Visibility = Visibility.Hidden;
                numPrice.Visibility = Visibility.Hidden;
                numPrice.Text = "0.00";
                rb11.Visibility = rb12.Visibility = Visibility.Visible;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Visible;
            }
            else
            {
                lbAbsolute.Visibility = Visibility.Visible;
                numPrice.Visibility = Visibility.Visible;
                rb11.Visibility = rb12.Visibility = Visibility.Hidden;
                rb11.IsChecked = true;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Hidden;
                numPerc.Text = numAbsolute.Text = "0.00";
            }
        }

        private void Rb3_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if ((rb1.IsChecked == true) || (rb2.IsChecked == true))
            {
                lbAbsolute.Visibility = Visibility.Hidden;
                numPrice.Visibility = Visibility.Hidden;
                numPrice.Text = "0.00";
                rb11.Visibility = rb12.Visibility = Visibility.Visible;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Visible;
            }
            else
            {
                lbAbsolute.Visibility = Visibility.Visible;
                numPrice.Visibility = Visibility.Visible;
                rb11.Visibility = rb12.Visibility = Visibility.Hidden;
                rb11.IsChecked = true;
                numPerc.Visibility = numAbsolute.Visibility = Visibility.Hidden;
                numPerc.Text = numAbsolute.Text = "0.00";
            }
        }

        private void Rb11_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            numPerc.IsEnabled = rb11.IsChecked == true;
            numAbsolute.IsEnabled = rb12.IsChecked == true;
        }

        private void Rb12_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            numPerc.IsEnabled = rb11.IsChecked == true;
            numAbsolute.IsEnabled = rb12.IsChecked == true;
        }

        private bool SaveData()
        {
            
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.DiscountName = txtName.Text.Trim();
            objClass.DiscountDescription = txtDesc.Text.Trim();

            gridView1.PostEditor();
            objClass.SplitDataTable = grdItem.ItemsSource as DataTable;

            if (chkActive.IsChecked == true)
            {
                objClass.DiscountStatus = "Y";
            }
            else
            {
                objClass.DiscountStatus = "N";
            }

            if (rb11.IsChecked == true)
            {
                objClass.DiscountType = "P";
                objClass.DiscountAmount = 0;
                objClass.DiscountPercentage = GeneralFunctions.fnDouble(numPerc.Text);
            }
            else
            {
                objClass.DiscountType = "A";
                objClass.DiscountAmount = GeneralFunctions.fnDouble(numAbsolute.Text);
                objClass.DiscountPercentage = 0;
            }

            if (cmbType.SelectedIndex == 0)
            {
                if (rb1.IsChecked == true)
                {
                    objClass.DiscountCategory = "N";
                }
                else if (rb2.IsChecked == true)
                {
                    objClass.DiscountCategory = "P";
                }
                else
                {
                    objClass.DiscountCategory = "A";
                }
            }

            if (cmbType.SelectedIndex == 1)
            {
                objClass.DiscountCategory = "X";
            }
            objClass.BasedOn = cmbType.SelectedIndex;

            objClass.DiscountPlusQty = GeneralFunctions.fnInt32(txtQty.Text);
            objClass.DiscountFamilyID = GeneralFunctions.fnInt32(cmbFamily.EditValue);
            objClass.LimitedPeriodCheck = chkPeriod.IsChecked == true ? "Y" : "N";

            objClass.LimitedStartDate = chkPeriod.IsChecked == true ? dtStart.DateTime.Date : Convert.ToDateTime(null);
            objClass.LimitedEndDate = chkPeriod.IsChecked == true ? dtFinish.DateTime.Date : Convert.ToDateTime(null);

            objClass.AbsolutePrice = ((rb12.IsChecked == true) || (rb3.IsChecked == true)) ? GeneralFunctions.fnDouble(numPrice.Text) : 0;

            objClass.ID = intID;

            objClass.BeginTransaction();
            bool ret = objClass.ProcessMixNmatchSetup();
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

        private int DuplicateCount()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            return objClass.DuplicateMixNmatchCount(txtName.Text.Trim());
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

            

            if (cmbFamily.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Item_Family);
                GeneralFunctions.SetFocus(cmbFamily);
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
                    DocMessage.MsgEnter(Properties.Resources.Name_already_exists);
                    GeneralFunctions.SetFocus(dtStart);
                    return false;
                }
            }




            int chk = 0;
            gridView1.PostEditor();
            DataTable dtbl = grdItem.ItemsSource as DataTable;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (Convert.ToBoolean(dr["chk"].ToString())) chk++;
            }
            dtbl.Dispose();

            if (chk == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Item_selected);
                GeneralFunctions.SetFocus(grdItem);
                return false;
            }

            if (cmbType.SelectedIndex == 0)
            {
                if (rb1.IsChecked == true)
                {

                    if (GeneralFunctions.fnInt32(txtQty.Text) < 2)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Minimum_No_of_Items_is_2);
                        GeneralFunctions.SetFocus(txtQty);
                        return false;
                    }
                }

                if (rb2.IsChecked == true)
                {
                    if (GeneralFunctions.fnInt32(txtQty.Text) <= 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Minimum_No_of_Items_is_2);
                        GeneralFunctions.SetFocus(txtQty);
                        return false;
                    }
                }

                if (rb3.IsChecked == true)
                {

                    if (GeneralFunctions.fnInt32(txtQty.Text) < 2)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Minimum_No_of_Items_is_2);
                        GeneralFunctions.SetFocus(txtQty);
                        return false;
                    }

                    if (GeneralFunctions.fnDouble(numPrice.Text) <= 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Enter_Valid_Absolute_Price);
                        GeneralFunctions.SetFocus(numPrice);
                        return false;
                    }
                }
            }

            if (cmbType.SelectedIndex == 1)
            {
                if (GeneralFunctions.fnDouble(numPrice.Text) <= 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Enter_Valid_Minimum_Amount);
                    GeneralFunctions.SetFocus(numPrice);
                    return false;
                }
            }

            if ((rb1.IsChecked == true) || (rb2.IsChecked == true))
            {
                if ((rb11.IsChecked == true) && (GeneralFunctions.fnDouble(numPerc.Text) <= 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.Enter_Valid_Perc_Off);
                    GeneralFunctions.SetFocus(numPerc);
                    return false;
                }

                if ((rb12.IsChecked == true) && (GeneralFunctions.fnDouble(numAbsolute.Text) <= 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.Enter_Valid_Amount_Off);
                    GeneralFunctions.SetFocus(numAbsolute);
                    return false;
                }
            }

            return true;
        }

        private void BtnNoFamily_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_SelectItemDlg itm = new frm_SelectItemDlg();
            try
            {
                itm.LevelID = 11;
                itm.ShowDialog();
                if (itm.OK)
                {
                    AddNonFamilyItem(itm.ITMID, itm.ITMTXT);
                }
            }
            finally
            {
                itm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                rb1.Visibility = rb2.Visibility = rb3.Visibility = Visibility.Visible;
                lbQty.Visibility = Visibility.Visible;
                txtQty.Visibility = Visibility.Visible;
                lbAbsolute.Text = "Absolute Price";
               

                lbAbsolute.Visibility = rb3.IsChecked == true? Visibility.Visible : Visibility.Hidden;
                numPrice.Visibility = rb3.IsChecked == true ? Visibility.Visible : Visibility.Hidden;


            }

            if (cmbType.SelectedIndex == 1)
            {
                rb1.IsChecked = true;
                rb1.Visibility = rb2.Visibility = rb3.Visibility = Visibility.Hidden;
                lbQty.Visibility = Visibility.Hidden;
                txtQty.Visibility = Visibility.Hidden;
                lbAbsolute.Visibility = Visibility.Visible;
                numPrice.Visibility = Visibility.Visible;
                lbAbsolute.Text = "Minimum Amount";

            }
        }

        private void CmbFamily_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            PosDataObject.Discounts objDept = new PosDataObject.Discounts();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchItemsOfFamily(GeneralFunctions.fnInt32(cmbFamily.EditValue));
            if (intID == 0)
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["chk"] = GeneralFunctions.fnInt32(dr["BrandID"].ToString()) > 0 ? true : false;
                }
            }

            grdItem.ItemsSource = dtbl;

            AddNonFamilyItem();

            grdItem.Visibility = (grdItem.ItemsSource as DataTable).Rows.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            dtbl.Dispose();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (!blCallFromProduct) BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void GrdItem_CustomGroupDisplayText(object sender, DevExpress.Xpf.Grid.CustomGroupDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colbrand")
            {
                if (e.Value.ToString() == "0")
                {
                    e.DisplayText = Properties.Resources.Non_Family_Items;
                }
                else
                {
                    e.DisplayText = Properties.Resources.Family_Items;
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

        private void CmbType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbFamily_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
