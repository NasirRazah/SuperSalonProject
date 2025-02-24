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
    /// Interaction logic for frm_DiscountBrwUC.xaml
    /// </summary>
    public partial class frm_DiscountBrwUC : UserControl
    {
        public frm_DiscountBrwUC()
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
            dbtbl = objClass.FetchData();

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
            frm_DiscountDlg frm_Discount = new frm_DiscountDlg();
            try
            {
                frm_Discount.ID = 0;
                frm_Discount.ViewMode = false;
                //frm_Discount.InitialiseScreen();
                frm_Discount.BrowseForm = this;
                frm_Discount.ShowDialog();
                intNewRecID = frm_Discount.NewID;
            }
            finally
            {
                frm_Discount.Close();
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
            frm_DiscountDlg frm_Discount = new frm_DiscountDlg();
            try
            {
                frm_Discount.ID = await ReturnRowID();
                if (frm_Discount.ID > 0)
                {
                    frm_Discount.ViewMode = false;
                    frm_Discount.BrowseForm = this;
                    frm_Discount.ShowDialog();
                    intNewRecID = frm_Discount.ID;
                }
            }
            finally
            {
                frm_Discount.Close();
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
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdD, colID));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Discount))
                {
                    if (GeneralFunctions.CheckItemDiscountInOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_discount_as_it_is_already_used_in_orders);
                        return;
                    }
                    if (GeneralFunctions.CheckTicketDiscountInOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_discount_as_it_is_already_used_in_orders);
                        return;
                    }

                    if (GeneralFunctions.CheckDiscountInCustomer(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_discount_as_it_is_already_attached_to_customers);
                        return;
                    }

                    PosDataObject.Discounts objCustomer = new PosDataObject.Discounts();
                    objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intrError = objCustomer.DeleteDiscount(intRecdID);
                    if (intrError != 0)
                    {

                    }
                    else
                    {
                        FetchData();
                        gridView1.FocusedRowHandle = intRowID - 1;
                    }
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
                int HID = await ReturnRowID();
                DataTable dtblReportData = new DataTable();
                dtblReportData.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDescription", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountValue", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnItem", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnTicket", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountActive", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountAuto", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnCustomer", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountLimited", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOfferPeriod", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnDayOfWeek", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnDayOfMonth", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountOnTime", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountRestricted", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountCategory", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountItem", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountTime", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_1", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_2", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_3", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_4", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_5", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_6", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountWeekDay_7", System.Type.GetType("System.String"));

                dtblReportData.Columns.Add("DiscountDateMonth_1", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_2", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_3", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_4", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_5", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_6", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_7", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_8", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_9", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_10", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_11", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_12", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_13", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_14", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_15", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_16", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_17", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_18", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_19", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_20", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_21", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_22", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_23", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_24", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_25", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_26", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_27", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_28", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_29", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_30", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("DiscountDateMonth_31", System.Type.GetType("System.String"));

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
                dtbl1 = fq1.FetchDiscountDate(HID);
                dtbl2 = fq2.FetchDiscountTime(HID);
                dtbl3 = fq3.FetchRestrictedItem(HID, "G");
                dtbl4 = fq4.FetchRestrictedItem(HID, "I");

                string cat = "";
                foreach (DataRow dr in dtbl3.Rows)
                {
                    cat = cat + dr["Item"].ToString() + "\n";
                }
                string itm = "";
                foreach (DataRow dr in dtbl4.Rows)
                {
                    itm = itm + dr["Item"].ToString() + "\n";
                }
                string time = "";
                foreach (DataRow dr in dtbl2.Rows)
                {
                    time = time + dr["StartTime"].ToString() + " - " + dr["EndTime"].ToString() + "\n";
                }



                PosDataObject.Discounts fq = new PosDataObject.Discounts();
                fq.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = fq.ShowRecord(HID);

                string valN = "";
                string valD = "";
                string valV = "";
                string valActive = "";
                string valAuto = "";
                string valCustomer = "";
                string valDay = "";
                string valDate = "";
                string valTime = "";
                string valRestrict = "";
                string valItem = "";
                string valTkt = "";
                string valLimited = "";
                string valPeriod = "";

                string valDay_1 = "";
                string valDay_2 = "";
                string valDay_3 = "";
                string valDay_4 = "";
                string valDay_5 = "";
                string valDay_6 = "";
                string valDay_7 = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    valN = dr["DiscountName"].ToString();
                    valD = dr["DiscountDescription"].ToString();
                    if (dr["DiscountType"].ToString() == "A")
                    {
                        valV = "Amt. " + GeneralFunctions.fnDouble(dr["DiscountAmount"].ToString()) + " off";
                    }
                    if (dr["DiscountType"].ToString() == "P")
                    {
                        valV = GeneralFunctions.fnDouble(dr["DiscountPercentage"].ToString()) + "% off";
                    }

                    valActive = dr["DiscountStatus"].ToString();
                    valDay = dr["ChkDaysAvailable"].ToString();

                    valDay_1 = dr["ChkMonday"].ToString();
                    valDay_2 = dr["ChkTuesday"].ToString();
                    valDay_3 = dr["ChkWednesday"].ToString();
                    valDay_4 = dr["ChkThursday"].ToString();
                    valDay_5 = dr["ChkFriday"].ToString();
                    valDay_6 = dr["ChkSaturday"].ToString();
                    valDay_7 = dr["ChkSunday"].ToString();

                    valTime = dr["ChkAllDay"].ToString();
                    valDate = dr["ChkAllDate"].ToString();
                    valRestrict = dr["ChkRestrictedItems"].ToString();
                    valAuto = dr["ChkAutoApply"].ToString();

                    valItem = dr["ChkApplyOnItem"].ToString();

                    valTkt = dr["ChkApplyOnTicket"].ToString();

                    valLimited = dr["ChkLimitedPeriod"].ToString();

                    if (dr["ChkLimitedPeriod"].ToString() == "Y")
                    {
                        valPeriod = GeneralFunctions.fnDate(dr["LimitedStartDate"].ToString()).ToShortDateString() + " " + "to" + " " + GeneralFunctions.fnDate(dr["LimitedEndDate"].ToString()).ToShortDateString();
                    }
                    valCustomer = dr["ChkApplyOnCustomer"].ToString();
                }
                dtbl.Dispose();

                string valMday_1 = "0";
                string valMday_2 = "0";
                string valMday_3 = "0";
                string valMday_4 = "0";
                string valMday_5 = "0";
                string valMday_6 = "0";
                string valMday_7 = "0";
                string valMday_8 = "0";
                string valMday_9 = "0";
                string valMday_10 = "0";
                string valMday_11 = "0";
                string valMday_12 = "0";
                string valMday_13 = "0";
                string valMday_14 = "0";
                string valMday_15 = "0";
                string valMday_16 = "0";
                string valMday_17 = "0";
                string valMday_18 = "0";
                string valMday_19 = "0";
                string valMday_20 = "0";
                string valMday_21 = "0";
                string valMday_22 = "0";
                string valMday_23 = "0";
                string valMday_24 = "0";
                string valMday_25 = "0";
                string valMday_26 = "0";
                string valMday_27 = "0";
                string valMday_28 = "0";
                string valMday_29 = "0";
                string valMday_30 = "0";
                string valMday_31 = "0";

                if (valDate == "N")
                {
                    foreach (DataRow dr in dtbl1.Rows)
                    {
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 1) valMday_1 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 2) valMday_2 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 3) valMday_3 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 4) valMday_4 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 5) valMday_5 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 6) valMday_6 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 7) valMday_7 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 8) valMday_8 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 9) valMday_9 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 10) valMday_10 = "1";

                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 11) valMday_11 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 12) valMday_12 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 13) valMday_13 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 14) valMday_14 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 15) valMday_15 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 16) valMday_16 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 17) valMday_17 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 18) valMday_18 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 19) valMday_19 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 20) valMday_20 = "1";

                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 21) valMday_21 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 22) valMday_22 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 23) valMday_23 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 24) valMday_24 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 25) valMday_25 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 26) valMday_26 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 27) valMday_27 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 28) valMday_28 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 29) valMday_29 = "1";
                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 30) valMday_30 = "1";

                        if (GeneralFunctions.fnInt32(dr["DateOfMonth"].ToString()) == 31) valMday_31 = "1";

                    }
                }

                if (valTime == "Y")
                {
                    time = "";
                }

                if (valRestrict == "N")
                {
                    cat = "";
                    itm = "";
                }

                dtblReportData.Rows.Add(new object[] {
                    valN,valD,valV,valItem,valTkt,valActive,valAuto,valCustomer,valLimited,valPeriod,valDay,valDate,valTime,valRestrict,cat,itm,time,
                    valDay_1,valDay_2,valDay_3,valDay_4,valDay_5,valDay_6,valDay_7,
                    valMday_1,valMday_2,valMday_3,valMday_4,valMday_5,valMday_6,valMday_7,valMday_8,valMday_9,valMday_10,
                    valMday_11,valMday_12,valMday_13,valMday_14,valMday_15,valMday_16,valMday_17,valMday_18,valMday_19,valMday_20,
                    valMday_21,valMday_22,valMday_23,valMday_24,valMday_25,valMday_26,valMday_27,valMday_28,valMday_29,valMday_30,valMday_31
                });

                OfflineRetailV2.Report.Discount.repDiscount rep = new OfflineRetailV2.Report.Discount.repDiscount();
                GeneralFunctions.MakeReportWatermark(rep);
                rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep.rReportHeader.Text = Settings.ReportHeader_Address;

                rep.Report.DataSource = dtblReportData;






                rep.rDiscountName.DataBindings.Add("Text", dtblReportData, "DiscountName");
                rep.rDesc.DataBindings.Add("Text", dtblReportData, "DiscountDescription");
                rep.rValueOff.DataBindings.Add("Text", dtblReportData, "DiscountValue");
                rep.rItem.DataBindings.Add("Text", dtblReportData, "DiscountOnItem");
                rep.rTicket.DataBindings.Add("Text", dtblReportData, "DiscountOnTicket");
                rep.rActive.DataBindings.Add("Text", dtblReportData, "DiscountActive");
                rep.rAuto.DataBindings.Add("Text", dtblReportData, "DiscountAuto");
                rep.rCustomer.DataBindings.Add("Text", dtblReportData, "DiscountOnCustomer");
                rep.rLimited.DataBindings.Add("Text", dtblReportData, "DiscountLimited");
                rep.rPeriod.DataBindings.Add("Text", dtblReportData, "DiscountOfferPeriod");

                rep.rDay.DataBindings.Add("Text", dtblReportData, "DiscountOnDayOfWeek");
                rep.rDate.DataBindings.Add("Text", dtblReportData, "DiscountOnDayOfMonth");
                rep.rTime.DataBindings.Add("Text", dtblReportData, "DiscountOnTime");
                rep.rTimeDetail.DataBindings.Add("Text", dtblReportData, "DiscountTime");

                rep.rDay1.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_1");
                rep.rDay2.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_2");
                rep.rDay3.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_3");
                rep.rDay4.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_4");
                rep.rDay5.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_5");
                rep.rDay6.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_6");
                rep.rDay7.DataBindings.Add("Text", dtblReportData, "DiscountWeekDay_7");

                rep.rRestrict.DataBindings.Add("Text", dtblReportData, "DiscountRestricted");
                rep.rcatdet.DataBindings.Add("Text", dtblReportData, "DiscountCategory");
                rep.ritemdet.DataBindings.Add("Text", dtblReportData, "DiscountItem");

                rep.r1.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_1");
                rep.r2.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_2");
                rep.r3.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_3");
                rep.r4.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_4");
                rep.r5.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_5");
                rep.r6.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_6");
                rep.r7.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_7");
                rep.r8.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_8");
                rep.r9.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_9");
                rep.r10.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_10");
                rep.r11.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_11");
                rep.r12.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_12");
                rep.r13.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_13");
                rep.r14.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_14");
                rep.r15.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_15");
                rep.r16.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_16");
                rep.r17.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_17");
                rep.r18.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_18");
                rep.r19.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_19");
                rep.r20.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_20");
                rep.r21.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_21");
                rep.r22.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_22");
                rep.r23.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_23");
                rep.r24.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_24");
                rep.r25.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_25");
                rep.r26.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_26");
                rep.r27.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_27");
                rep.r28.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_28");
                rep.r29.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_29");
                rep.r30.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_30");
                rep.r31.DataBindings.Add("Text", dtblReportData, "DiscountDateMonth_31");

                if (valRestrict == "N")
                {
                    rep.rRCat.Visible = rep.rRItem.Visible = false;
                }

                if (valDay == "Y")
                {
                    rep.rDay1.Visible = rep.rDay2.Visible = rep.rDay3.Visible = rep.rDay4.Visible = rep.rDay5.Visible = rep.rDay6.Visible = rep.rDay7.Visible = false;
                    rep.xrLabel14.Visible = rep.xrLabel22.Visible = rep.xrLabel16.Visible = rep.xrLabel18.Visible = rep.xrLabel20.Visible = rep.xrLabel24.Visible = rep.xrLabel26.Visible = false;
                }
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
                    dtblReportData.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Discounts")
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
                txtSearchGrdData.InfoText = "Search Discounts";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Discounts") return;
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
