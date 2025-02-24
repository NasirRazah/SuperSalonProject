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
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_DiscountDlg.xaml
    /// </summary>
    public partial class frm_DiscountDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_DiscountDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frm_DiscountBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private DataTable dtblDate = null;
        private DataTable dtblTime = null;
        private DataTable dtblIG = null;
        private DataTable dtblI = null;
        private int intLevel;
        private int intItemID = 0;
        private string strItemTxt = "";

        private string strPrev = "";

        private string strSTIME = "";
        private string strETIME = "";

        private DateTime dtStartDate = DateTime.Now;
        private DateTime dtEndDate = DateTime.Now;

        private bool blViewMode;

        private bool PreviousCustomerChecked = false;

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

        public frm_DiscountBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_DiscountBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
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

        private void ChkRestricted_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRestricted.IsChecked == true)
            {
                pnlRestricted.IsEnabled = true;
            }
            else
            {
                pnlRestricted.IsEnabled = false;
            }
            boolControlChanged = true;
        }

        private void ChkAllDay_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAllDay.IsChecked == true)
            {
                pnlAnyTime.IsEnabled = false;
            }
            else
            {
                pnlAnyTime.IsEnabled = true;
            }
            boolControlChanged = true;
        }

        private void ChkAllAvailableDays_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAllAvailableDays.IsChecked == true)
            {
                chkMon.IsChecked = true;
                chkTues.IsChecked = true;
                chkWed.IsChecked = true;
                chkThur.IsChecked = true;
                chkFri.IsChecked = true;
                chkSat.IsChecked = true;
                chkSun.IsChecked = true;

                chkMon.IsEnabled = false;
                chkTues.IsEnabled = false;
                chkWed.IsEnabled = false;
                chkThur.IsEnabled = false;
                chkFri.IsEnabled = false;
                chkSat.IsEnabled = false;
                chkSun.IsEnabled = false;
            }
            else
            {
                chkMon.IsEnabled = true;
                chkTues.IsEnabled = true;
                chkWed.IsEnabled = true;
                chkThur.IsEnabled = true;
                chkFri.IsEnabled = true;
                chkSat.IsEnabled = true;
                chkSun.IsEnabled = true;
            }

            boolControlChanged = true;
        }

        private void ChkAllDate_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAllDate.IsChecked == true)
            {
                grdDate.IsEnabled = false;
            }
            else
            {
                grdDate.IsEnabled = true;
            }
            boolControlChanged = true;
        }

        public void InitialiseScreen()
        {
            chkActive.IsChecked = true;
            chkAllAvailableDays.IsChecked = true;
            chkMon.IsChecked = true;
            chkTues.IsChecked = true;
            chkWed.IsChecked = true;
            chkThur.IsChecked = true;
            chkFri.IsChecked = true;
            chkSat.IsChecked = true;
            chkSun.IsChecked = true;

            chkMon.IsEnabled = false;
            chkTues.IsEnabled = false;
            chkWed.IsEnabled = false;
            chkThur.IsEnabled = false;
            chkFri.IsEnabled = false;
            chkSat.IsEnabled = false;
            chkSun.IsEnabled = false;

            chkAllDay.IsChecked = true;
            pnlAnyTime.IsEnabled = false;

            chkAllDate.IsChecked = true;
            grdDate.IsEnabled = false;

            chkRestricted.IsChecked = false;
            pnlRestricted.IsEnabled = false;

            rbP.IsChecked = true;
            numPerc.IsEnabled = true;
            numAbsolute.IsEnabled = false;

            chkAuto.IsChecked = false;
            chkItem.IsChecked = true;
            chkTicket.IsChecked = false;

            lbs.IsEnabled = false;
            lbf.IsEnabled = false;
            dtStart.EditValue = null;
            dtFinish.EditValue = null;
            dtStart.IsEnabled = false;
            dtFinish.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();

            InitialiseScreen();

            dtblDate = new DataTable();
            dtblDate.Columns.Add("Value1", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value2", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value3", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value4", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value5", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value6", System.Type.GetType("System.String"));
            dtblDate.Columns.Add("Value7", System.Type.GetType("System.String"));
            dtblDate.Rows.Add(new object[] { "1", "2", "3", "4", "5", "6", "7" });
            dtblDate.Rows.Add(new object[] { "8", "9", "10", "11", "12", "13", "14" });
            dtblDate.Rows.Add(new object[] { "15", "16", "17", "18", "19", "20", "21" });
            dtblDate.Rows.Add(new object[] { "22", "23", "24", "25", "26", "27", "28" });
            dtblDate.Rows.Add(new object[] { "29", "30", "31", "", "", "", "" });
            grdDate.ItemsSource = dtblDate;

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

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Discount;
            }
            else
            {
                ShowHeader();
                if (blViewMode)
                {
                    chkActive.IsEnabled = false;
                    chkAllAvailableDays.IsEnabled = false;
                    chkAllDate.IsEnabled = false;
                    chkAllDay.IsEnabled = false;
                    chkAuto.IsEnabled = false;
                    chkCustomer.IsEnabled = false;
                    chkItem.IsEnabled = false;
                    chkLimitedPeriod.IsEnabled = false;
                    chkRestricted.IsEnabled = false;
                    chkTicket.IsEnabled = false;
                    
                    grdDate.IsEnabled = false;
                    pnlRestricted.IsEnabled = false;
                    pnlAnyTime.IsEnabled = false;
                    Title.Text = Properties.Resources.View_Discount;
                    btnOK.Visibility = Visibility.Hidden;
                    btnCancel.Content = "Close";
                    chkMon.IsEnabled = false;
                    chkTues.IsEnabled = false;
                    chkWed.IsEnabled = false;
                    chkThur.IsEnabled = false;
                    chkFri.IsEnabled = false;
                    chkSat.IsEnabled = false;
                    chkSun.IsEnabled = false;
                    txtName.IsEnabled = false;
                    txtDesc.IsEnabled = false;
                    lbs.IsEnabled = false;
                    lbf.IsEnabled = false;
                    dtStart.IsEnabled = false;
                    dtFinish.IsEnabled = false;

                }
                else
                    Title.Text = Properties.Resources.Edit_Discount;
                strPrev = txtName.Text.Trim();
                ShowDetails();
            }
            boolControlChanged = false;
        }

        public void ShowHeader()
        {
            PosDataObject.Discounts fq = new PosDataObject.Discounts();
            fq.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = fq.ShowRecord(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                txtName.Text = dr["DiscountName"].ToString();
                txtDesc.Text = dr["DiscountDescription"].ToString();
                if (dr["DiscountType"].ToString() == "A")
                {
                    rbA.IsChecked = true;
                    numAbsolute.IsEnabled = true;
                    numPerc.IsEnabled = false;
                }
                if (dr["DiscountType"].ToString() == "P")
                {
                    rbP.IsChecked = true;
                    numAbsolute.IsEnabled = false;
                    numPerc.IsEnabled = true;
                }

                numAbsolute.Text = GeneralFunctions.fnDouble(dr["DiscountAmount"].ToString()).ToString("f2");
                numPerc.Text = GeneralFunctions.fnDouble(dr["DiscountPercentage"].ToString()).ToString("f2");

                if (dr["DiscountStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }
                

                if (dr["ChkDaysAvailable"].ToString() == "Y")
                {
                    chkAllAvailableDays.IsChecked = true;
                }
                else
                {
                    chkAllAvailableDays.IsChecked = false;
                }


                if (dr["ChkMonday"].ToString() == "Y")
                {
                    chkMon.IsChecked = true;
                }
                else
                {
                    chkMon.IsChecked = false;
                }

                if (dr["ChkTuesday"].ToString() == "Y")
                {
                    chkTues.IsChecked = true;
                }
                else
                {
                    chkTues.IsChecked = false;
                }

                if (dr["ChkWednesday"].ToString() == "Y")
                {
                    chkWed.IsChecked = true;
                }
                else
                {
                    chkWed.IsChecked = false;
                }

                if (dr["ChkThursday"].ToString() == "Y")
                {
                    chkThur.IsChecked = true;
                }
                else
                {
                    chkThur.IsChecked = false;
                }

                if (dr["ChkFriday"].ToString() == "Y")
                {
                    chkFri.IsChecked = true;
                }
                else
                {
                    chkFri.IsChecked = false;
                }

                if (dr["ChkSaturday"].ToString() == "Y")
                {
                    chkSat.IsChecked = true;
                }
                else
                {
                    chkSat.IsChecked = false;
                }

                if (dr["ChkSunday"].ToString() == "Y")
                {
                    chkSun.IsChecked = true;
                }
                else
                {
                    chkSun.IsChecked = false;
                }

                if (dr["ChkAllDay"].ToString() == "Y")
                {
                    chkAllDay.IsChecked = true;
                }
                else
                {
                    chkAllDay.IsChecked = false;
                }

                if (dr["ChkAllDate"].ToString() == "Y")
                {
                    chkAllDate.IsChecked = true;
                }
                else
                {
                    chkAllDate.IsChecked = false;
                }

                if (dr["ChkRestrictedItems"].ToString() == "Y")
                {
                    chkRestricted.IsChecked = true;
                }
                else
                {
                    chkRestricted.IsChecked = false;
                }


                if (dr["ChkAutoApply"].ToString() == "Y")
                {
                    chkAuto.IsChecked = true;
                }
                else
                {
                    chkAuto.IsChecked = false;
                }

                if (dr["ChkApplyOnItem"].ToString() == "Y")
                {
                    chkItem.IsChecked = true;
                }
                else
                {
                    chkItem.IsChecked = false;
                }

                if (dr["ChkApplyOnTicket"].ToString() == "Y")
                {
                    chkTicket.IsChecked = true;
                }
                else
                {
                    chkTicket.IsChecked = false;
                }

                if (dr["ChkLimitedPeriod"].ToString() == "Y")
                {
                    chkLimitedPeriod.IsChecked = true;
                }
                else
                {
                    chkLimitedPeriod.IsChecked = false;
                }

                if (dr["LimitedStartDate"].ToString() == "")
                {
                    dtStart.EditValue = null;
                }
                else
                {
                    dtStart.EditValue = GeneralFunctions.fnDate(dr["LimitedStartDate"].ToString());
                }
                if (dr["LimitedEndDate"].ToString() == "")
                {
                    dtFinish.EditValue = null;
                }
                else
                {
                    dtFinish.EditValue = GeneralFunctions.fnDate(dr["LimitedEndDate"].ToString());
                }


                if (dr["ChkApplyOnCustomer"].ToString() == "Y")
                {
                    chkCustomer.IsChecked = true;
                    PreviousCustomerChecked = true;
                }
                else
                {
                    chkCustomer.IsChecked = false;
                    PreviousCustomerChecked = false;
                }
            }
            dtbl.Dispose();
        }

        public void ShowDetails()
        {
            PosDataObject.Discounts fq1 = new PosDataObject.Discounts();
            fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Discounts fq2 = new PosDataObject.Discounts();
            fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Discounts fq3 = new PosDataObject.Discounts();
            fq3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            PosDataObject.Discounts fq4 = new PosDataObject.Discounts();
            fq4.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl1 = new DataTable();
            DataTable dtbl2 = new DataTable();
            DataTable dtbl3 = new DataTable();
            DataTable dtbl4 = new DataTable();
            dtbl1 = fq1.FetchDiscountDate(intID);
            dtbl2 = fq2.FetchDiscountTime(intID);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl2.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            dtbl3 = fq3.FetchRestrictedItem(intID, "G");

            DataTable dtblTemp1 = dtbl3.DefaultView.ToTable();
            DataColumn column1 = new DataColumn("Image");
            column1.DataType = System.Type.GetType("System.Byte[]");
            column1.AllowDBNull = true;
            column1.Caption = "Image";
            dtblTemp1.Columns.Add(column1);

            foreach (DataRow dr in dtblTemp1.Rows)
            {
                dr["Image"] = strip;
            }

            dtbl4 = fq4.FetchRestrictedItem(intID, "I");

            DataTable dtblTemp2 = dtbl4.DefaultView.ToTable();
            DataColumn column2 = new DataColumn("Image");
            column2.DataType = System.Type.GetType("System.Byte[]");
            column2.AllowDBNull = true;
            column2.Caption = "Image";
            dtblTemp2.Columns.Add(column2);

            foreach (DataRow dr in dtblTemp2.Rows)
            {
                dr["Image"] = strip;
            }

            SelectDateCell(dtbl1);

            dtblTime = dtblTemp;
            dtblIG = dtblTemp1;
            dtblI = dtblTemp2;

            //grdDate.DataSource = dtblDate;
            grdTime.ItemsSource = dtblTime;
            grdG.ItemsSource = dtblIG;
            grdI.ItemsSource = dtblI;
            dtbl1.Dispose();
            dtbl2.Dispose();
            dtbl3.Dispose();
            dtbl4.Dispose();
        }

        private void SelectDateCell(DataTable dtbl)
        {
            //grdDate.ForceInitialize();
            foreach (DataRow dr in dtbl.Rows)
            {
                if (dr["DateOfMonth"].ToString() == "1") gridView1.SelectCell(0, col1);
                if (dr["DateOfMonth"].ToString() == "2") gridView1.SelectCell(0, col2);
                if (dr["DateOfMonth"].ToString() == "3") gridView1.SelectCell(0, col3);
                if (dr["DateOfMonth"].ToString() == "4") gridView1.SelectCell(0, col4);
                if (dr["DateOfMonth"].ToString() == "5") gridView1.SelectCell(0, col5);
                if (dr["DateOfMonth"].ToString() == "6") gridView1.SelectCell(0, col6);
                if (dr["DateOfMonth"].ToString() == "7") gridView1.SelectCell(0, col7);

                if (dr["DateOfMonth"].ToString() == "8") gridView1.SelectCell(1, col1);
                if (dr["DateOfMonth"].ToString() == "9") gridView1.SelectCell(1, col2);
                if (dr["DateOfMonth"].ToString() == "10") gridView1.SelectCell(1, col3);
                if (dr["DateOfMonth"].ToString() == "11") gridView1.SelectCell(1, col4);
                if (dr["DateOfMonth"].ToString() == "12") gridView1.SelectCell(1, col5);
                if (dr["DateOfMonth"].ToString() == "13") gridView1.SelectCell(1, col6);
                if (dr["DateOfMonth"].ToString() == "14") gridView1.SelectCell(1, col7);

                if (dr["DateOfMonth"].ToString() == "15") gridView1.SelectCell(2, col1);
                if (dr["DateOfMonth"].ToString() == "16") gridView1.SelectCell(2, col2);
                if (dr["DateOfMonth"].ToString() == "17") gridView1.SelectCell(2, col3);
                if (dr["DateOfMonth"].ToString() == "18") gridView1.SelectCell(2, col4);
                if (dr["DateOfMonth"].ToString() == "19") gridView1.SelectCell(2, col5);
                if (dr["DateOfMonth"].ToString() == "20") gridView1.SelectCell(2, col6);
                if (dr["DateOfMonth"].ToString() == "21") gridView1.SelectCell(2, col7);

                if (dr["DateOfMonth"].ToString() == "22") gridView1.SelectCell(3, col1);
                if (dr["DateOfMonth"].ToString() == "23") gridView1.SelectCell(3, col2);
                if (dr["DateOfMonth"].ToString() == "24") gridView1.SelectCell(3, col3);
                if (dr["DateOfMonth"].ToString() == "25") gridView1.SelectCell(3, col4);
                if (dr["DateOfMonth"].ToString() == "26") gridView1.SelectCell(3, col5);
                if (dr["DateOfMonth"].ToString() == "27") gridView1.SelectCell(3, col6);
                if (dr["DateOfMonth"].ToString() == "28") gridView1.SelectCell(3, col7);

                if (dr["DateOfMonth"].ToString() == "29") gridView1.SelectCell(4, col1);
                if (dr["DateOfMonth"].ToString() == "30") gridView1.SelectCell(4, col2);
                if (dr["DateOfMonth"].ToString() == "31") gridView1.SelectCell(4, col3);
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            return objClass.DuplicateCount(txtName.Text.Trim());
        }

        private int CustomerDiscountCount()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            return objClass.GetCustomerDiscountCount(intID);
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Discount_Name);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateCount() > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Discount_name_already_exists);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if (intID > 0)
            {
                if ((strPrev != txtName.Text.Trim()) && (DuplicateCount() > 0))
                {
                    DocMessage.MsgInformation(Properties.Resources.Discount_name_already_exists);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }

            if (intID > 0)
            {
                if ((PreviousCustomerChecked) && (!chkCustomer.IsChecked == true))
                {
                    if (CustomerDiscountCount() > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.This_discount_is_already_attached_to_customers);
                        GeneralFunctions.SetFocus(chkCustomer);
                        return false;
                    }
                }
            }

            if ((!chkItem.IsChecked == true) && (!chkTicket.IsChecked == true))
            {
                DocMessage.MsgInformation(Properties.Resources.Choose_applicable_on_for_this_discount);
                GeneralFunctions.SetFocus(chkItem);
                return false;
            }

            if (chkLimitedPeriod.IsChecked == true)
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


            return true;
        }

        private DataTable GetSelectedDate()
        {
            DataTable dtnlNew = new DataTable();
            dtnlNew.Columns.Add("DateValue", System.Type.GetType("System.String"));

            foreach (GridCell cell in gridView1.GetSelectedCells())
            {
                
                dtnlNew.Rows.Add(new object[] { cell.Value.ToString() });
            }

            return dtnlNew;
        }

        private bool SaveData()
        {
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.DiscountName = txtName.Text.Trim();
            objClass.DiscountDescription = txtDesc.Text.Trim();

            objClass.SplitDataTableDate = GetSelectedDate();
            objClass.SplitDataTableTime = dtblTime;
            objClass.SplitDataTableRI = dtblI;
            objClass.SplitDataTableRIG = dtblIG;

            if (chkActive.IsChecked == true)
            {
                objClass.DiscountStatus = "Y";
            }
            else
            {
                objClass.DiscountStatus = "N";
            }

            objClass.RequiredReason = "N";

            if (chkAllAvailableDays.IsChecked == true)
            {
                objClass.ChkDaysAvailable = "Y";
            }
            else
            {
                objClass.ChkDaysAvailable = "N";
            }

            if (chkAllDay.IsChecked == true)
            {
                objClass.ChkAllDay = "Y";
            }
            else
            {
                objClass.ChkAllDay = "N";
            }

            if (chkAllDate.IsChecked == true)
            {
                objClass.ChkAllDate = "Y";
            }
            else
            {
                objClass.ChkAllDate = "N";
            }

            if (chkRestricted.IsChecked == true)
            {
                objClass.ChkRestrictedItems = "Y";
            }
            else
            {
                objClass.ChkRestrictedItems = "N";
            }

            if (chkMon.IsChecked == true)
            {
                objClass.ChkMonday = "Y";
            }
            else
            {
                objClass.ChkMonday = "N";
            }

            if (chkTues.IsChecked == true)
            {
                objClass.ChkTuesday = "Y";
            }
            else
            {
                objClass.ChkTuesday = "N";
            }

            if (chkWed.IsChecked == true)
            {
                objClass.ChkWednesday = "Y";
            }
            else
            {
                objClass.ChkWednesday = "N";
            }

            if (chkThur.IsChecked == true)
            {
                objClass.ChkThursday = "Y";
            }
            else
            {
                objClass.ChkThursday = "N";
            }

            if (chkFri.IsChecked == true)
            {
                objClass.ChkFriday = "Y";
            }
            else
            {
                objClass.ChkFriday = "N";
            }

            if (chkSat.IsChecked == true)
            {
                objClass.ChkSaturday = "Y";
            }
            else
            {
                objClass.ChkSaturday = "N";
            }

            if (chkSun.IsChecked == true)
            {
                objClass.ChkSunday = "Y";
            }
            else
            {
                objClass.ChkSunday = "N";
            }

            if (chkAuto.IsChecked == true)
            {
                objClass.ChkAutoApply = "Y";
            }
            else
            {
                objClass.ChkAutoApply = "N";
            }


            if (chkItem.IsChecked == true)
            {
                objClass.ChkApplyOnItem = "Y";
            }
            else
            {
                objClass.ChkApplyOnItem = "N";
            }

            if (chkTicket.IsChecked == true)
            {
                objClass.ChkApplyOnTicket = "Y";
            }
            else
            {
                objClass.ChkApplyOnTicket = "N";
            }

            if (rbP.IsChecked == true)
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

            if (chkLimitedPeriod.IsChecked == true)
            {
                objClass.ChkLimitedPeriod = "Y";
            }
            else
            {
                objClass.ChkLimitedPeriod = "N";
            }
            if (dtStart.EditValue == null)
            {
                objClass.LimitedStartDate = Convert.ToDateTime(null);
            }
            else
            {
                objClass.LimitedStartDate = dtStart.DateTime.Date;
            }

            if (dtFinish.EditValue == null)
            {
                objClass.LimitedEndDate = Convert.ToDateTime(null);
            }
            else
            {
                objClass.LimitedEndDate = dtFinish.DateTime.Date;
            }

            if (chkCustomer.IsChecked == true)
            {
                objClass.ChkApplyOnCustomer = "Y";
            }
            else
            {
                objClass.ChkApplyOnCustomer = "N";
            }

            objClass.ID = intID;
            objClass.BeginTransaction();
            bool ret = objClass.AddEditDiscount();
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

        private void ChkLimitedPeriod_Checked(object sender, RoutedEventArgs e)
        {
            if (chkLimitedPeriod.IsChecked == true)
            {
                lbs.IsEnabled = true;
                lbf.IsEnabled = true;
                dtStart.IsEnabled = true;
                dtFinish.IsEnabled = true;
                dtStart.EditValue = DateTime.Today.Date;
                dtFinish.EditValue = DateTime.Today.AddMonths(1);
            }
            else
            {
                lbs.IsEnabled = false;
                lbf.IsEnabled = false;
                dtStart.EditValue = null;
                dtFinish.EditValue = null;
                dtStart.IsEnabled = false;
                dtFinish.IsEnabled = false;
            }
        }

        private void populatetime()
        {
            if (strSTIME != "")
            {
                dtblTime.Rows.Add(new object[] { intID.ToString(), strSTIME, strETIME, GeneralFunctions.GetImageAsByteArray() });
            }
            grdTime.ItemsSource = dtblTime;
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

        private void BtnAddTime_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_SelectTimeDlg frm_SelectTimeDlg = new frm_SelectTimeDlg();
            try
            {
                frm_SelectTimeDlg.ShowDialog();
                if (frm_SelectTimeDlg.OK)
                {
                    strSTIME = frm_SelectTimeDlg.STIME;
                    strETIME = frm_SelectTimeDlg.ETIME;
                    populatetime();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_SelectTimeDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnDeleteTime_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdTime.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView2.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdTime, colID) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (grdTime.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdTime.ItemsSource = dtbl;
                    dtblTime = (grdTime.ItemsSource) as DataTable;
                    if (dtblTime.Rows.Count == 0) chkAllDay.IsChecked = true;
                    dtbl.Dispose();
                    boolControlChanged = true;
                }
            }
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
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

        private async void BtnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdG.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView3.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdG, colID) != "")
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

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_SelectItemDlg frm_SelectItemDlg = new POSSection.frm_SelectItemDlg();
            try
            {
                frm_SelectItemDlg.LevelID = 10;
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

        private async void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (blViewMode) return;
            if ((grdI.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gridView4.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdI, colID) != "")
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

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
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
    }
}
