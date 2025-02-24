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
    /// Interaction logic for frm_SalePriceDlg.xaml
    /// </summary>
    public partial class frm_SalePriceDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_SalePriceDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frm_SalePriceBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private DataTable dtblI = null;
        private int intLevel;
        private string strItemFamily = "";
        private int intItemID = 0;
        private string strItemTxt = "";
        private string strItemSKU = "";
        private double dblItemPrice = 0;

        private string strPrev = "";

        private string strSTIME = "";
        private string strETIME = "";

        private DateTime dtStartDate = DateTime.Now;
        private DateTime dtEndDate = DateTime.Now;

        private bool blViewMode;

        private bool blCallFromProduct;

        public bool CallFromProduct
        {
            get { return blCallFromProduct; }
            set { blCallFromProduct = value; }
        }

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

        public frm_SalePriceBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_SalePriceBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            dtStart.EditValue = DateTime.Today.Date.AddDays(1).Date;
            dtEnd.EditValue = DateTime.Today.AddDays(8).Date;
            tmStart.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
            tmEnd.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 21, 0, 0);

            dtblI = new DataTable();
            dtblI.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblI.Columns.Add("ItemID", System.Type.GetType("System.String"));
            dtblI.Columns.Add("ItemName", System.Type.GetType("System.String"));
            dtblI.Columns.Add("ApplyFamily", System.Type.GetType("System.String"));
            dtblI.Columns.Add("SalePrice", System.Type.GetType("System.Double"));
            dtblI.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Sale_Price;

            }
            else
            {
                Title.Text = Properties.Resources.Edit_Sale_Price;
                ShowHeader();
                strPrev = txtName.Text.Trim();
                ShowDetails();
                btnGenerateShelfTg.Visibility = Visibility.Visible;
            }
            boolControlChanged = false;
        }

        public void ShowHeader()
        {
            PosDataObject.Discounts fq = new PosDataObject.Discounts();
            fq.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = fq.ShowSalePriceHeader(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                txtName.Text = dr["SaleBatchName"].ToString();
                txtDesc.Text = dr["SaleBatchDesc"].ToString();

                if (dr["SaleStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

                dtStart.EditValue = GeneralFunctions.fnDate(dr["EffectiveFrom"].ToString());
                dtEnd.EditValue = GeneralFunctions.fnDate(dr["EffectiveTo"].ToString());

                tmStart.DateTime = GeneralFunctions.fnDate(dr["EffectiveFrom"].ToString());
                tmEnd.DateTime = GeneralFunctions.fnDate(dr["EffectiveTo"].ToString());
            }
            dtbl.Dispose();
        }

        public void ShowDetails()
        {
            PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);

            DataTable dtbl1 = new DataTable();
            dtbl1 = fq1.FetchSalePriceDetails(intID);

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


            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl1.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            

            dtblI = dtblTemp;
            grd.ItemsSource = dtblI;
            dtbl1.Dispose();

        }


        private void populateitem()
        {
            if (intItemID > 0)
            {
                dtblI.Rows.Add(new object[] { "0", intItemID.ToString(), strItemTxt + "   (" + strItemSKU + ")", strItemFamily == "I" ? "N" : "Y", dblItemPrice, GeneralFunctions.GetImageAsByteArray() });
            }
            grd.ItemsSource = dtblI;
        }

        private bool IsDuplicateItem(int itm, string bFamily)
        {
            bool b = false;
            DataTable dtbl = grd.ItemsSource as DataTable;

            if (dtbl == null) b = false;
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    if ((GeneralFunctions.fnInt32(dr["ItemID"].ToString()) == itm)
                        && (dr["ApplyFamily"].ToString() == bFamily))
                    {
                        b = true;
                        break;
                    }
                }

                if (b)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate + " " + (bFamily == "N" ? Properties.Resources.Item : Properties.Resources.Family));
                }
                else
                {
                    if (bFamily == "Y") // check if any item of this specific family already added or not
                    {
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            if (dr["ApplyFamily"].ToString() == "Y") continue;

                            int bid = 0;
                            PosDataObject.Product op = new PosDataObject.Product();
                            op.Connection = SystemVariables.Conn;
                            bid = op.GetProductBrandID(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));
                            if ((bid > 0) && (bid == itm))
                            {
                                b = true;
                                break;
                            }
                        }
                        if (b)
                        {
                            DocMessage.MsgInformation(Properties.Resources.Item_is_already_added_against_this_family);
                        }
                    }
                    else
                    {
                        int bid = 0;
                        PosDataObject.Product op = new PosDataObject.Product();
                        op.Connection = SystemVariables.Conn;
                        bid = op.GetProductBrandID(itm);
                        if (bid > 0)
                        {
                            foreach (DataRow dr1 in dtbl.Rows)
                            {
                                if (dr1["ApplyFamily"].ToString() == "N") continue;
                                if (GeneralFunctions.fnInt32(dr1["ItemID"].ToString()) == bid)
                                {
                                    b = true;
                                    break;
                                }
                            }
                        }
                        if (b)
                        {
                            DocMessage.MsgInformation(Properties.Resources.Item_family_is_already_added);
                        }
                    }
                }
            }
            return b;
        }

        private bool SaveData()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.SaleBatchName = txtName.Text.Trim();
            objClass.SaleBatchDesc = txtDesc.Text.Trim();

            objClass.SalePriceData = dtblI;

            if (chkActive.IsChecked == true)
            {
                objClass.SaleStatus = "Y";
            }
            else
            {
                objClass.SaleStatus = "N";
            }

            objClass.EffectiveFrom = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                GeneralFunctions.fnDate(dtStart.DateTime).Day,
                GeneralFunctions.fnDate(tmStart.DateTime).Hour, GeneralFunctions.fnDate(tmStart.DateTime).Minute, 0);

            objClass.EffectiveTo = new DateTime(GeneralFunctions.fnDate(dtEnd.DateTime).Year, GeneralFunctions.fnDate(dtEnd.DateTime).Month,
                GeneralFunctions.fnDate(dtEnd.DateTime).Day,
                GeneralFunctions.fnDate(tmEnd.DateTime).Hour, GeneralFunctions.fnDate(tmEnd.DateTime).Minute, 0);

            objClass.ID = intID;
            objClass.BeginTransaction();
            bool ret = objClass.ProcessSalePrice();
            intNewID = objClass.ID;
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

        private bool DuplicateBatch()
        {
            PosDataObject.Discounts objdisc = new PosDataObject.Discounts();
            objdisc.Connection = SystemVariables.Conn;
            return (objdisc.DuplicateSaleBatch(txtName.Text.Trim()) > 0);
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Sale_Batch);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (strPrev != txtName.Text.Trim())))
            {
                if (DuplicateBatch())
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Batch);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if ((dtStart.EditValue == null) || (dtEnd.EditValue == null)
                || (tmStart.EditValue == null) || (tmEnd.EditValue == null))
            {
                DocMessage.MsgEnter(Properties.Resources.Valid_Period);
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }

            DateTime f = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                GeneralFunctions.fnDate(dtStart.DateTime).Day,
                GeneralFunctions.fnDate(tmStart.DateTime).Hour, GeneralFunctions.fnDate(tmStart.DateTime).Minute, 0);

            DateTime t = new DateTime(GeneralFunctions.fnDate(dtEnd.DateTime).Year, GeneralFunctions.fnDate(dtEnd.DateTime).Month,
                GeneralFunctions.fnDate(dtEnd.DateTime).Day,
                GeneralFunctions.fnDate(tmEnd.DateTime).Hour, GeneralFunctions.fnDate(tmEnd.DateTime).Minute, 0);


            DataTable dtbl = grd.ItemsSource as DataTable;
            if (dtbl == null)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Item_Family_selected);
                return false;
            }
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Item_Family_selected);
                return false;
            }


            if (t >= f)
            {
                PosDataObject.Discounts odisc = new PosDataObject.Discounts();
                odisc.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int overlap = odisc.OverlappingSaleBatch(intID, f, t);
                if (overlap > 0)
                {
                    bool bflag = false;
                    string itm = "";
                    foreach (DataRow dr in dtblI.Rows)
                    {
                        PosDataObject.Discounts odisc1 = new PosDataObject.Discounts();
                        odisc1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        int overlapitem = odisc1.OverlappingSaleItemInBatch(intID, f, t, GeneralFunctions.fnInt32(dr["ItemID"].ToString()), dr["ApplyFamily"].ToString());
                        if (overlapitem > 0)
                        {
                            bflag = true;
                            itm = dr["ItemName"].ToString();
                            break;
                        }
                    }
                    if (bflag)
                    {
                        DocMessage.MsgInformation(itm + " " + Properties.Resources.already_exists_in_another_active_sale);
                        GeneralFunctions.SetFocus(dtStart);
                        return false;
                    }
                }
            }
            else
            {
                DocMessage.MsgEnter(Properties.Resources.Valid_Period);
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }

            return true;
        }

        private bool IsValidAllForGenerateShelfTag()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Sale_Batch);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (strPrev != txtName.Text.Trim())))
            {
                if (DuplicateBatch())
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Batch);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if ((dtStart.EditValue == null) || (dtEnd.EditValue == null)
                || (tmStart.EditValue == null) || (tmEnd.EditValue == null))
            {
                DocMessage.MsgEnter(Properties.Resources.Valid_Period);
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }

            DateTime f = new DateTime(GeneralFunctions.fnDate(dtStart.DateTime).Year, GeneralFunctions.fnDate(dtStart.DateTime).Month,
                GeneralFunctions.fnDate(dtStart.DateTime).Day,
                GeneralFunctions.fnDate(tmStart.DateTime).Hour, GeneralFunctions.fnDate(tmStart.DateTime).Minute, 0);

            DateTime t = new DateTime(GeneralFunctions.fnDate(dtEnd.DateTime).Year, GeneralFunctions.fnDate(dtEnd.DateTime).Month,
                GeneralFunctions.fnDate(dtEnd.DateTime).Day,
                GeneralFunctions.fnDate(tmEnd.DateTime).Hour, GeneralFunctions.fnDate(tmEnd.DateTime).Minute, 0);


            DataTable dtbl = grd.ItemsSource as DataTable;
            if (dtbl == null)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Item_Family_selected);
                return false;
            }
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Item_Family_selected);
                return false;
            }


            /*if (t >= f)
            {
                PosDataObject.Discounts odisc = new PosDataObject.Discounts();
                odisc.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int overlap = odisc.OverlappingSaleBatch(intID, f, t);
                if (overlap > 0)
                {
                    bool bflag = false;
                    string itm = "";
                    foreach (DataRow dr in dtblI.Rows)
                    {
                        PosDataObject.Discounts odisc1 = new PosDataObject.Discounts();
                        odisc1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        int overlapitem = odisc1.OverlappingSaleItemInBatch(intID, f, t, GeneralFunctions.fnInt32(dr["ItemID"].ToString()), dr["ApplyFamily"].ToString());
                        if (overlapitem > 0)
                        {
                            bflag = true;
                            itm = dr["ItemName"].ToString();
                            break;
                        }
                    }
                    if (bflag)
                    {
                        DocMessage.MsgInformation(itm + " already exists in another active sale");
                        GeneralFunctions.SetFocus(dtStart);
                        return false;
                    }
                }
            }
            else
            {
                DocMessage.MsgEnter(Translation.SetMultilingualTextInCodes("Valid Period");
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }
            */
            return true;
        }

        private bool CheckIfAlreadyGenerate()
        {
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            return objPO.CheckShelfTagHeaderFromSaleBatch(intID);
        }

        private int GetGeneratedHeaderID()
        {
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            return objPO.GetShelfTagHeaderIDFromSaleBatch(intID);
        }

        private string GetProductSKU(int PID)
        {
            PosDataObject.Product objPO = new PosDataObject.Product();
            objPO.Connection = SystemVariables.Conn;
            return objPO.GetProductSKU(PID);
        }

        private string GetProductName(int PID)
        {
            PosDataObject.Product objPO = new PosDataObject.Product();
            objPO.Connection = SystemVariables.Conn;
            return objPO.GetProductName(PID);
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

        private void BtnGenerateShelfTg_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_continue_to_generate_shelf_tagged_labels) == MessageBoxResult.No) return;
            if (IsValidAllForGenerateShelfTag())
            {
                int HeaderID = 0;
                bool f = CheckIfAlreadyGenerate();
                bool proceed = false;
                if (f)
                {
                    if (DocMessage.MsgConfirmation(Properties.Resources .Shelf_tagged_batch_already_generated_against_this_Sale_Price_batch+ "\n" + Properties.Resources.Do_you_want_to_continue_) == MessageBoxResult.Yes)
                    {
                        HeaderID = GetGeneratedHeaderID();
                        proceed = true;
                    }
                }
                else
                {
                    proceed = true;
                }

                if (proceed)
                {
                    DataTable dtbl = new DataTable();
                    dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                    dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                    dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                    dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                    dtbl.Columns.Add("PrintLabelFlag", System.Type.GetType("System.Boolean"));
                    dtbl.Columns.Add("D", System.Type.GetType("System.Boolean"));
                    dtbl.Columns.Add("Price", System.Type.GetType("System.String"));

                    foreach (DataRow dr in (grd.ItemsSource as DataTable).Rows)
                    {
                        if (dr["ApplyFamily"].ToString() == "N")
                        {
                            dtbl.Rows.Add(new object[] {
                                       GetProductSKU(GeneralFunctions.fnInt32(dr["ItemID"].ToString())),
                                       "1",
                                       GetProductName(GeneralFunctions.fnInt32(dr["ItemID"].ToString())),
                                       dr["ItemID"].ToString(),
                                       true,
                                       false,
                                       dr["SalePrice"].ToString()
                            });
                        }
                        else
                        {
                            DataTable dtblBrand = new DataTable();
                            PosDataObject.Product objProduct = new PosDataObject.Product();
                            objProduct.Connection = SystemVariables.Conn;
                            dtblBrand = objProduct.ShowRecordOfFamily(GeneralFunctions.fnInt32(dr["ItemID"].ToString()));

                            foreach (DataRow dr1 in dtblBrand.Rows)
                            {
                                dtbl.Rows.Add(new object[] {
                                       dr1["SKU"].ToString(),
                                       "1",
                                       dr1["Description"].ToString(),
                                       dr1["ID"].ToString(),
                                       true,
                                       false,
                                       dr["SalePrice"].ToString()
                                        });
                            }
                        }
                    }


                    string ErrMsg = "";
                    gridView1.PostEditor();

                    PosDataObject.PO objPO = new PosDataObject.PO();
                    objPO.Connection = SystemVariables.Conn;
                    objPO.ConnString = SystemVariables.ConnectionString;
                    objPO.TerminalName = Settings.TerminalName;
                    objPO.GeneralNotes = "";

                    objPO.OrderNo = txtName.Text.Trim();
                    objPO.OrderDate = dtStart.DateTime.Date;
                    objPO.CreateID = intID;
                    objPO.CreateType = "S";
                    objPO.SplitDataTable = dtbl;
                    objPO.LoginUserID = SystemVariables.CurrentUserID;
                    objPO.ErrorMsg = "";
                    objPO.ID = HeaderID;
                    objPO.batchprintFlag = true;
                    if (HeaderID == 0)
                    {
                        objPO.Mode = "Add";
                    }
                    else
                    {
                        objPO.Mode = "Edit";
                    }

                    //B e g i n   T r a n s a ct i o n
                    objPO.BeginTransaction();
                    if (objPO.PostShelfTag())
                    {
                        int intNewBatchID = objPO.ID;
                    }
                    objPO.EndTransaction();
                    //E n d  T r a n s a ct i o n

                    ErrMsg = objPO.ErrorMsg;

                    if (ErrMsg == "")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Shelf_tagged_labels_generated_successfully);
                    }


                }


            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
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

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtDesc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grd.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grd, colItemID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_SaleSetDlg fdlg = new frm_SaleSetDlg();
            try
            {
                fdlg.ITMFAMILY = "I";
                fdlg.ITMID = 0;
                fdlg.ShowDialog();
                if (fdlg.OK == true)
                {
                    strItemFamily = fdlg.ITMFAMILY;
                    intItemID = fdlg.ITMID;
                    strItemTxt = fdlg.ITMTXT;
                    strItemSKU = fdlg.ITMSKU;
                    dblItemPrice = fdlg.ITMPRICE;
                    if (!IsDuplicateItem(intItemID, strItemFamily == "I" ? "N" : "Y"))
                    {
                        populateitem();
                        await SetCurrentRow(intItemID);
                        boolControlChanged = true;
                    }
                }
            }
            finally
            {
                fdlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async Task EditLine()
        {
            int rw = gridView1.FocusedRowHandle;
            if (rw < 0) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_SaleSetDlg fdlg = new frm_SaleSetDlg();
            try
            {
                fdlg.ITMFAMILY = await GeneralFunctions.GetCellValue1(rw, grd, colFamily) == "N" ? "I" : "F";
                fdlg.ITMID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(rw, grd, colItemID));
                fdlg.ITMPRICE = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(rw, grd, colPrice));

                int previtm = fdlg.ITMID;
                string prevcat = fdlg.ITMFAMILY;

                fdlg.ShowDialog();
                if (fdlg.OK == true)
                {
                    strItemFamily = fdlg.ITMFAMILY;
                    intItemID = fdlg.ITMID;
                    strItemTxt = fdlg.ITMTXT;
                    strItemSKU = fdlg.ITMSKU;
                    dblItemPrice = fdlg.ITMPRICE;

                    if (((intItemID == previtm) && (strItemFamily == prevcat)) || (((intItemID != previtm) || (strItemFamily != prevcat)) && (!IsDuplicateItem(intItemID, strItemFamily == "I" ? "N" : "Y"))))
                    {
                        dtblI.Rows.RemoveAt(rw);
                        populateitem();
                        await SetCurrentRow(intItemID);
                        boolControlChanged = true;
                    }
                }
            }
            finally
            {
                fdlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditLine();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grd, colID) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grd.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grd.ItemsSource = dtbl;
                    dtblI = (grd.ItemsSource) as DataTable;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditLine();
        }

        private void Grd_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ApplyFamily")
            {
                e.DisplayText = e.Value.ToString() == "N" ? Properties.Resources.Item : Properties.Resources.Family;
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
    }
}
