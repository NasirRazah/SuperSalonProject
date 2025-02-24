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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_FeesDlg.xaml
    /// </summary>
    public partial class frm_FeesDlg : Window
    {
        private frm_FeesBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private DataTable dtblDate = null;
        private DataTable dtblTime = null;
        private DataTable dtblIG = null;
        private DataTable dtblI = null;
        private DataTable dtblF = null;
        private DataTable dtblD = null;
        private int intLevel;
        private int intItemID = 0;
        private string strItemTxt = "";

        private string strPrev = "";

        private string strSTIME = "";
        private string strETIME = "";

        private DateTime dtStartDate = DateTime.Now;
        private DateTime dtEndDate = DateTime.Now;

        private bool blViewMode;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

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

        public frm_FeesBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_FeesBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_FeesDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void ChkTax_Checked(object sender, RoutedEventArgs e)
        {
            if (chkTax.IsChecked == true)
            {
                grdTax.Visibility = Visibility.Visible;
            }
            else
            {
                grdTax.Visibility = Visibility.Hidden;
            }
            boolControlChanged = true;
        }

        private void ChkRestricted_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRestricted.IsChecked == true)
            {
                groupControl3.Visibility = Visibility.Visible;
                groupControl4.Visibility = Visibility.Visible;
                groupControl1.Visibility = Visibility.Visible;
                groupControl2.Visibility = Visibility.Visible;
            }
            else
            {
                groupControl3.Visibility = Visibility.Collapsed;
                groupControl4.Visibility = Visibility.Collapsed;
                groupControl1.Visibility = Visibility.Collapsed;
                groupControl2.Visibility = Visibility.Collapsed;
            }
            boolControlChanged = true;
        }

        private void RbP_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (rbA.IsChecked == true)
            {
                numPerc.IsEnabled = false;
                numAbsolute.IsEnabled = true;
            }
            else
            {
                numPerc.IsEnabled = true;
                numAbsolute.IsEnabled = false;
            }
        }

        private void ChkItem_Checked(object sender, RoutedEventArgs e)
        {
            if (chkItem.IsChecked == true)
            {
                chkInclude.IsEnabled = true;
                chkItemQty.IsEnabled = true;
                chkDiscount.IsEnabled = true;
            }
            else
            {
                chkInclude.IsChecked = false;
                chkItemQty.IsChecked = false;
                chkDiscount.IsChecked = false;
                chkInclude.IsEnabled = false;
                chkItemQty.IsEnabled = false;
                chkDiscount.IsEnabled = false;
            }
        }

        private void populategitem()
        {
            if (intItemID > 0)
            {
                dtblIG.Rows.Add(new object[] { intItemID.ToString(), strItemTxt, GeneralFunctions.GetImageAsByteArray() });
            }
            grdG.ItemsSource = dtblIG;
        }

        private void populateitem()
        {
            if (intItemID > 0)
            {
                dtblI.Rows.Add(new object[] { intItemID.ToString(), strItemTxt, GeneralFunctions.GetImageAsByteArray() });
            }
            grdI.ItemsSource = dtblI;
        }

        private void populatefamily()
        {
            if (intItemID > 0)
            {
                dtblF.Rows.Add(new object[] { intItemID.ToString(), strItemTxt, GeneralFunctions.GetImageAsByteArray() });
            }
            grdB.ItemsSource = dtblF;
        }

        private void populatedept()
        {
            if (intItemID > 0)
            {
                dtblD.Rows.Add(new object[] { intItemID.ToString(), strItemTxt, GeneralFunctions.GetImageAsByteArray() });
            }
            grdD.ItemsSource = dtblD;
        }

        private void BarButtonItem3_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_SelectItemDlg frm_SelectItemDlg = new POSSection.frm_SelectItemDlg();
            try
            {
                frm_SelectItemDlg.LevelID = 2;
                frm_SelectItemDlg.ShowDialog();
                if (frm_SelectItemDlg.OK)
                {
                    intItemID = frm_SelectItemDlg.ITMID;
                    strItemTxt = frm_SelectItemDlg.ITMTXT;
                    populatefamily();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_SelectItemDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem4_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdB.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdB, colbid) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grdB.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdB.ItemsSource = dtbl;
                    dtblF = (grdB.ItemsSource) as DataTable;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }

        private void BarButtonItem9_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_SelectItemDlg frm_SelectItemDlg = new POSSection.frm_SelectItemDlg();
            try
            {
                frm_SelectItemDlg.LevelID = 3;
                frm_SelectItemDlg.ShowDialog();
                if (frm_SelectItemDlg.OK)
                {
                    intItemID = frm_SelectItemDlg.ITMID;
                    strItemTxt = frm_SelectItemDlg.ITMTXT;
                    populatedept();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_SelectItemDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem10_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView2.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdD, coldid) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grdD.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdD.ItemsSource = dtbl;
                    dtblD = (grdD.ItemsSource) as DataTable;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }

        private void BarButtonItem5_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_SelectItemDlg frm_SelectItemDlg = new POSSection.frm_SelectItemDlg();
            try
            {
                frm_SelectItemDlg.LevelID = 0;
                frm_SelectItemDlg.ShowDialog();
                if (frm_SelectItemDlg.OK)
                {
                    intItemID = frm_SelectItemDlg.ITMID;
                    strItemTxt = frm_SelectItemDlg.ITMTXT;
                    populategitem();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_SelectItemDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem6_Click(object sender, RoutedEventArgs e)
        {

            if (blViewMode) return;
            if ((grdG.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView3.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdG, colicid) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grdG.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdG.ItemsSource = dtbl;
                    dtblIG = (grdG.ItemsSource) as DataTable;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }

        private void BarButtonItem7_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_SelectItemDlg frm_SelectItemDlg = new POSSection.frm_SelectItemDlg();
            try
            {
                frm_SelectItemDlg.LevelID = 1;
                frm_SelectItemDlg.ShowDialog();
                if (frm_SelectItemDlg.OK)
                {
                    intItemID = frm_SelectItemDlg.ITMID;
                    strItemTxt = frm_SelectItemDlg.ITMTXT;
                    populateitem();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_SelectItemDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem8_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdI.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView4.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdI, coliid) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grdI.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdI.ItemsSource = dtbl;
                    dtblI = (grdI.ItemsSource) as DataTable;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }


        public void PopulateTax()
        {
            PosDataObject.Tax objTax = new PosDataObject.Tax();
            objTax.Connection = SystemVariables.Conn;
            DataTable dbtblTax = new DataTable();
            dbtblTax = objTax.ShowTaxCombo();


            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblTax.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            PART_Editor_Tax.AutoPopulateColumns = false;
            PART_Editor_Tax.ItemsSource = dtblTemp;
        }

        private void PopulateBatchGrid()
        {
            try
            {
                DataTable dtblF_Tx = new DataTable();
                dtblF_Tx.Columns.Add("ID");
                dtblF_Tx.Columns.Add("TaxID");
                dtblF_Tx.Columns.Add("TaxName");

                PosDataObject.Product objTaxProduct = new PosDataObject.Product();
                objTaxProduct.Connection = SystemVariables.Conn;
                objTaxProduct.DecimalPlace = Settings.DecimalPlace;
                DataTable dtblTx = new DataTable();
                dtblTx = objTaxProduct.ShowTaxes(intID,"Fees");
                foreach (DataRow dr in dtblTx.Rows)
                {
                    dtblF_Tx.Rows.Add(new object[] { dr["ID"].ToString(), dr["TaxID"].ToString(), dr["TaxName"].ToString() });
                }

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtblF_Tx.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }

                grdTax.ItemsSource = dtblTemp;
                FillRowinTaxGrid(((grdTax.ItemsSource) as DataTable).Rows.Count);
                gridView5.MoveFirstRow();
            }
            finally
            {
            }
        }

        private void FillRowinTaxGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = 3 - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdTax.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount < intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "0", "", strip });
                }

                grdTax.ItemsSource = dtbl;
                //dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();


            InitialiseScreen();
            PopulateTax();
            PopulateBatchGrid();
            dtblTime = new DataTable();
            dtblTime.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblTime.Columns.Add("StartTime", System.Type.GetType("System.String"));
            dtblTime.Columns.Add("EndTime", System.Type.GetType("System.String"));
            dtblTime.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            dtblI = new DataTable();
            dtblI.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblI.Columns.Add("Item", System.Type.GetType("System.String"));
            dtblI.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            dtblIG = new DataTable();
            dtblIG.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblIG.Columns.Add("Item", System.Type.GetType("System.String"));
            dtblIG.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            dtblF = new DataTable();
            dtblF.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblF.Columns.Add("Item", System.Type.GetType("System.String"));
            dtblF.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            dtblD = new DataTable();
            dtblD.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblD.Columns.Add("Item", System.Type.GetType("System.String"));
            dtblD.Columns.Add("Image", System.Type.GetType("System.Byte[]"));


            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Fees__Charges;
            }
            else
            {
                ShowHeader();
                if (blViewMode)
                {
                    /*Todo:GeneralFunctions.MakeReadOnly(pnlBody);
                    GeneralFunctions.MakeReadOnly(groupControl3);
                    GeneralFunctions.MakeReadOnly(groupControl4);
                    GeneralFunctions.MakeReadOnly(panelControl1);
                    GeneralFunctions.MakeReadOnly(panelControl2);
                    GeneralFunctions.MakeReadOnly(panelControl3);*/
                    Title.Text = Properties.Resources.View_Fees__Charges;
                    btnOK.Visibility = Visibility.Hidden;
                    btnCancel.Content = "Close";
                }
                else
                    Title.Text = Properties.Resources.Edit_Fees__Charges;

                strPrev = txtName.Text.Trim();
                ShowDetails();
            }
            boolControlChanged = false;
        }


        public void InitialiseScreen()
        {
           
            chkRestricted.IsChecked = false;

            groupControl3.Visibility = Visibility.Hidden;
            groupControl4.Visibility = Visibility.Hidden;
            groupControl1.Visibility = Visibility.Hidden;
            groupControl2.Visibility = Visibility.Hidden;
            chkTax.IsChecked = false;
            grdTax.Visibility = Visibility.Hidden;
            rbP.IsChecked = true;
            numPerc.IsEnabled = true;
            numAbsolute.IsEnabled = false;
            chkTax.IsChecked = false;
            chkItemQty.IsChecked = false;
            chkItem.IsChecked = true;
        }

        public void ShowHeader()
        {
            PosDataObject.Fees fq = new PosDataObject.Fees();
            fq.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = fq.ShowRecord(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                txtName.Text = dr["FeesName"].ToString();
                txtDesc.Text = dr["FeesDescription"].ToString();
                if (dr["FeesType"].ToString() == "A")
                {
                    rbA.IsChecked = true;
                    numAbsolute.IsEnabled = true;
                    numPerc.IsEnabled = false;
                }
                if (dr["FeesType"].ToString() == "P")
                {
                    rbP.IsChecked = true;
                    numAbsolute.IsEnabled = false;
                    numPerc.IsEnabled = true;
                }

                numAbsolute.Text = GeneralFunctions.fnDouble(dr["FeesAmount"].ToString()).ToString("f2");
                numPerc.Text = GeneralFunctions.fnDouble(dr["FeesPercentage"].ToString()).ToString("f2");

                if (dr["FeesStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }


                if (dr["ChkTax"].ToString() == "Y")
                {
                    chkTax.IsChecked = true;
                }
                else
                {
                    chkTax.IsChecked = false;
                }

                if (dr["ChkRestrictedItems"].ToString() == "Y")
                {
                    chkRestricted.IsChecked = true;
                }
                else
                {
                    chkRestricted.IsChecked = false;
                }

                if (dr["ChkDiscount"].ToString() == "Y")
                {
                    chkDiscount.IsChecked = true;
                }
                else
                {
                    chkDiscount.IsChecked = false;
                }

                if (dr["ChkFoodStamp"].ToString() == "Y")
                {
                    chkFoodStamp.IsChecked = true;
                }
                else
                {
                    chkFoodStamp.IsChecked = false;
                }

                if (dr["ChkInclude"].ToString() == "Y")
                {
                    chkInclude.IsChecked = true;
                }
                else
                {
                    chkInclude.IsChecked = false;
                }

                if (dr["ChkAutoApply"].ToString() == "Y")
                {
                    chkAutoApply.IsChecked = true;
                }
                else
                {
                    chkAutoApply.IsChecked = false;
                }

                if (dr["ChkItemQty"].ToString() == "Y")
                {
                    chkItemQty.IsChecked = true;
                }
                else
                {
                    chkItemQty.IsChecked = false;
                }

                if (dr["ChkApplyItemTicket"].ToString() == "I") chkItem.IsChecked = true;
                if (dr["ChkApplyItemTicket"].ToString() == "T") chkTicket.IsChecked = true;
            }
            dtbl.Dispose();
        }

        public void ShowDetails()
        {
            PosDataObject.Fees fq1 = new PosDataObject.Fees();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Fees fq2 = new PosDataObject.Fees();
            fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Fees fq3 = new PosDataObject.Fees();
            fq3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Fees fq4 = new PosDataObject.Fees();
            fq4.Connection = new SqlConnection(SystemVariables.ConnectionString);

            DataTable dtbl1 = new DataTable();
            DataTable dtbl2 = new DataTable();
            DataTable dtbl3 = new DataTable();
            DataTable dtbl4 = new DataTable();
            dtbl1 = fq1.FetchRestrictedItem(intID, "F");
            dtbl2 = fq2.FetchRestrictedItem(intID, "D");
            dtbl3 = fq3.FetchRestrictedItem(intID, "G");
            dtbl4 = fq4.FetchRestrictedItem(intID, "I");

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp1 = dtbl1.DefaultView.ToTable();
            DataColumn column1 = new DataColumn("Image");
            column1.DataType = System.Type.GetType("System.Byte[]");
            column1.AllowDBNull = true;
            column1.Caption = "Image";
            dtblTemp1.Columns.Add(column1);
            foreach (DataRow dr in dtblTemp1.Rows)
            {
                dr["Image"] = strip;
            }


            DataTable dtblTemp2 = dtbl2.DefaultView.ToTable();
            DataColumn column2 = new DataColumn("Image");
            column2.DataType = System.Type.GetType("System.Byte[]");
            column2.AllowDBNull = true;
            column2.Caption = "Image";
            dtblTemp2.Columns.Add(column2);
            foreach (DataRow dr in dtblTemp2.Rows)
            {
                dr["Image"] = strip;
            }

            DataTable dtblTemp3 = dtbl3.DefaultView.ToTable();
            DataColumn column3 = new DataColumn("Image");
            column3.DataType = System.Type.GetType("System.Byte[]");
            column3.AllowDBNull = true;
            column3.Caption = "Image";
            dtblTemp3.Columns.Add(column3);
            foreach (DataRow dr in dtblTemp3.Rows)
            {
                dr["Image"] = strip;
            }

            DataTable dtblTemp4 = dtbl4.DefaultView.ToTable();
            DataColumn column4 = new DataColumn("Image");
            column4.DataType = System.Type.GetType("System.Byte[]");
            column4.AllowDBNull = true;
            column4.Caption = "Image";
            dtblTemp4.Columns.Add(column4);
            foreach (DataRow dr in dtblTemp4.Rows)
            {
                dr["Image"] = strip;
            }


            dtblF = dtblTemp1;
            dtblD = dtblTemp2;
            dtblIG = dtblTemp3;
            dtblI = dtblTemp4;
            grdB.ItemsSource = dtblF;
            grdD.ItemsSource = dtblD;
            grdG.ItemsSource = dtblIG;
            grdI.ItemsSource = dtblI;

            dtbl1.Dispose();
            dtbl2.Dispose();
            dtbl3.Dispose();
            dtbl4.Dispose();
        }

        private int DuplicateCount()
        {
            PosDataObject.Fees objClass = new PosDataObject.Fees();
            objClass.Connection = SystemVariables.Conn;
            return objClass.DuplicateCount(txtName.Text.Trim());
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Fees_Name);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateCount() > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Fees);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if (intID > 0)
            {
                if ((strPrev != txtName.Text.Trim()) && (DuplicateCount() > 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.Fees_name_already_exists);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if (chkRestricted.IsChecked == true)
            {
                int a = grdB.ItemsSource == null ? 0 : (grdB.ItemsSource as DataTable).Rows.Count;
                int b = grdD.ItemsSource == null ? 0 : (grdD.ItemsSource as DataTable).Rows.Count;
                int c = grdG.ItemsSource == null ? 0 : (grdG.ItemsSource as DataTable).Rows.Count;
                int d = grdI.ItemsSource == null ? 0 : (grdI.ItemsSource as DataTable).Rows.Count;

                if ((a == 0) && (b == 0) && (c == 0) && (d == 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.No_resricted_item_found);
                    GeneralFunctions.SetFocus(chkRestricted);
                    return false;
                }
            }

            return true;
        }

        private bool SaveData()
        {
            gridView5.PostEditor();
            string strError = "";
            PosDataObject.Fees objClass = new PosDataObject.Fees();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.FeesName = txtName.Text.Trim();
            objClass.FeesDescription = txtDesc.Text.Trim();

            objClass.SplitDataTableTax = grdTax.ItemsSource as DataTable;
            if (!chkRestricted.IsChecked == true)
            {
                dtblF.Rows.Clear();
                dtblD.Rows.Clear();
                dtblI.Rows.Clear();
                dtblIG.Rows.Clear();
            }

            objClass.SplitDataTableRF = dtblF;
            objClass.SplitDataTableRD = dtblD;

            objClass.SplitDataTableRI = dtblI;
            objClass.SplitDataTableRIG = dtblIG;

            if (chkActive.IsChecked == true)
            {
                objClass.FeesStatus = "Y";
            }
            else
            {
                objClass.FeesStatus = "N";
            }

            if (chkRestricted.IsChecked == true)
            {
                objClass.ChkRestrictedItems = "Y";
            }
            else
            {
                objClass.ChkRestrictedItems = "N";
            }

            if (chkTax.IsChecked == true)
            {
                objClass.ChkTax = "Y";
            }
            else
            {
                objClass.ChkTax = "N";
            }

            if (chkDiscount.IsChecked == true)
            {
                objClass.ChkDiscount = "Y";
            }
            else
            {
                objClass.ChkDiscount = "N";
            }

            if (chkFoodStamp.IsChecked == true)
            {
                objClass.ChkFoodStamp = "Y";
            }
            else
            {
                objClass.ChkFoodStamp = "N";
            }

            if (chkInclude.IsChecked == true)
            {
                objClass.ChkInclude = "Y";
            }
            else
            {
                objClass.ChkInclude = "N";
            }

            if (chkAutoApply.IsChecked == true)
            {
                objClass.ChkAutoApply = "Y";
            }
            else
            {
                objClass.ChkAutoApply = "N";
            }

            if (chkItemQty.IsChecked == true)
            {
                objClass.ChkItemQty = "Y";
            }
            else
            {
                objClass.ChkItemQty = "N";
            }

            if (rbP.IsChecked == true)
            {
                objClass.FeesType = "P";
                objClass.FeesAmount = 0;
                objClass.FeesPercentage = GeneralFunctions.fnDouble(numPerc.Text);
            }
            else
            {
                objClass.FeesType = "A";
                objClass.FeesAmount = GeneralFunctions.fnDouble(numAbsolute.Text);
                objClass.FeesPercentage = 0;
            }
            if (chkItem.IsChecked == true) objClass.ChkApplyItemTicket = "I";
            if (chkTicket.IsChecked == true) objClass.ChkApplyItemTicket = "T";

            objClass.ID = intID;
            objClass.BeginTransaction();
            bool ret = objClass.AddEditFees();
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

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

        private bool IsDuplicateTax(int refTax)
        {
            bool bDuplicate = false;
            DataTable dsource = grdTax.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == refTax)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private void GridView5_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colTaxID")
            {
                if (IsDuplicateTax(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation(Properties.Resources.Tax_already_entered);

                    Dispatcher.BeginInvoke(new Action(() => gridView5.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Tax objGroup = new PosDataObject.Tax();
                    objGroup.Connection = SystemVariables.Conn;
                    grdTax.SetCellValue(e.RowHandle, colTaxName, objGroup.GetTaxDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                }


            }
        }

        private void PART_Editor_Tax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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
    }
}
