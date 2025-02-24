using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
using OfflineRetailV2;
using OfflineRetailV2.Data;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSApptBookBrw.xaml
    /// </summary>
    /// 


    public partial class frm_POSApptBookDlg : Window
    {
        bool boolSelectCustomer = false;
        private bool boolExecuteOnScreenSearch;
        public bool ExecuteOnScreenSearch
        {
            get { return boolExecuteOnScreenSearch; }
            set { boolExecuteOnScreenSearch = value; }
        }

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_POSApptBookDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        #region Variables

        private int intID;
        private int intNewID;
        private int intEmpID;
        private int intposcustomer;
        private bool blfetchCustomer = false;
        private int ServiceTimeLength = 0;
        private DateTime dtLocateStart;
        private DateTime dtLocateEnd;
        private DateTime dtApptStartTime;
        private string operatepermission = "N";
        private bool isoperate;
        public bool bloperate
        {
            get { return isoperate; }
            set { isoperate = value; }
        }
        private bool IsCancel = false;
        public bool blCancel
        {
            get { return IsCancel; }
            set { IsCancel = value; }
        }
        private bool IsLoadedData = false;

        private bool blChangeCustomer = false;
        private int intCustID = 0;
        private string strCustomerID = "";
        private string strTaxExempt = "N";
        private string strDiscountLevel = "A";
        private int intUsePriceLevel = 0;
        private double dblCustAcctBalance = 0;
        private double dblCustAcctLimit = 0;
        private double dblStoreCr = 0;
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private DataTable dtblCustNotes = null;

        private bool boolBookingExportFlagChanged;
        private string prevBookingExpFlag = "N";

        private bool boolCallFromAdmin;

        private frm_CustomerBrwUC frm_CustomerBrw;

        public int poscustomer
        {
            get { return intposcustomer; }
            set { intposcustomer = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int EmpID
        {
            get { return intEmpID; }
            set { intEmpID = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public DateTime ApptStartTime
        {
            get { return dtApptStartTime; }
            set { dtApptStartTime = value; }
        }

        public DateTime LocateStart
        {
            get { return dtLocateStart; }
            set { dtLocateStart = value; }
        }

        public DateTime LocateEnd
        {
            get { return dtLocateEnd; }
            set { dtLocateEnd = value; }
        }

        public bool CallFromAdmin
        {
            get { return boolCallFromAdmin; }
            set { boolCallFromAdmin = value; }
        }



        #endregion

        // Get Staffs

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();

            }

            if (boolExecuteOnScreenSearch)
            {
                boolExecuteOnScreenSearch = false;

                if (txtCust.Text.Trim() != "")
                {
                    int intpCust = IsValidCustID();
                    if (intpCust > 0)
                    {
                        blChangeCustomer = true;
                        string refTaxExempt = "";
                        string refDiscountLevel = "";
                        string refTaxID = "";
                        string refStoreCr = "";
                        string refCID = "";
                        string refCName = "";
                        string refCAdd = "";
                        double dblBalance = 0;
                        string refARCredit = "";
                        string refPOSNotes = "";
                        intCustID = GetCustID();
                        FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd,
                            ref refTaxExempt, ref refDiscountLevel, ref refTaxID, ref refStoreCr, ref refARCredit,
                            ref refPOSNotes);
                        strTaxExempt = refTaxExempt;
                        strDiscountLevel = refDiscountLevel;
                        if (strDiscountLevel == "") strDiscountLevel = "A";

                        dblBalance = GetAccountBalance(intCustID);
                        txtCust.Text = refCID;

                        strCustomerID = refCID;
                        lbCustName.Text = refCName;

                        lbCustAddress.Text = refCAdd;
                        ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
                        //lbCustNotes.Text = refPOSNotes;
                        //ShowCustomerPhoto();
                        //FetchCustomerNote("", "Customer", intCustID, DateTime.Today.Year, DateTime.Today.Month);
                        dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
                        dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
                        dblCustAcctBalance = dblBalance;
                        GeneralFunctions.SetFocus(emplkup);
                    }
                    else
                    {
                        if (fkybrd != null)
                        {
                            fkybrd.Close();

                        }

                        DocMessage.MsgError("Invalid/Inactive Customer ID");



                        intCustID = 0;
                        txtCust.Text = "";
                        txtCustStore.Text = "";

                        lbCustName.Text = "";
                        lbCustAddress.Text = "";
                        lbCustBal.Text = "";

                        //picCustPhoto.Image = null;
                        strTaxExempt = "N";
                        strDiscountLevel = "A";

                        return;
                    }
                }
                else
                {
                    intCustID = 0;
                    //lbCustID.Text = "";
                    lbCustName.Text = "";
                    txtCustStore.Text = "";
                    lbCustAddress.Text = "";
                    lbCustBal.Text = "";
                    //lbCustNotes.Text = "";
                    //lbCustTax.Text = "";
                    //picCustPhoto.Image = null;
                    strTaxExempt = "N";
                    strDiscountLevel = "A";
                }
            }

        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (boolSelectCustomer) return;
            string ctrlname = "";
            try
            {
                ctrlname = (sender as DevExpress.Xpf.Editors.TextEdit).Name;
            }
            catch
            {

            }
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();
                fkybrd.Top = 50;
               
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                try
                {
                    var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                    //var location = new Point(0, 0);
                    if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                    {
                        fkybrd.Top = location.Y - 35 - 320;
                    }
                    else
                    {
                        fkybrd.Top = location.Y + 35;
                    }
                }
                catch (Exception)
                {

                }
                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.execsearchonreturn = (ctrlname == "txtCust");
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

        public void PopulateStaff()
        {
            PosDataObject.Employee objCategory = new PosDataObject.Employee();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.GetAppointmentEmployee("");
            if (operatepermission == "N")
            {
                DataTable newdtbl = new DataTable();
                newdtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                newdtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                newdtbl.Columns.Add("EmployeeName", System.Type.GetType("System.String"));
                foreach (DataRow dr in dbtblCat.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == SystemVariables.CurrentUserID)
                    {
                        newdtbl.Rows.Add(new object[] { dr["ID"].ToString(), dr["EmployeeID"].ToString(), dr["EmployeeName"].ToString() });
                        break;
                    }
                }
                dbtblCat = newdtbl;
            }

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

            emplkup.ItemsSource = dtblTemp;

            emplkup.EditValue = "0";
            dbtblCat.Dispose();
        }

        // Show Detail info. of the appointment

        private void ShowData()
        {
            DataTable dtblH = new DataTable();
            DataTable dtblD = new DataTable();

            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = SystemVariables.Conn;
            dtblH = objp1.FetchApptHeader(intID);
            foreach (DataRow dr1 in dtblH.Rows)
            {
                prevBookingExpFlag = dr1["BookingExpFlag"].ToString();
                intCustID = GeneralFunctions.fnInt32(dr1["CustomerID"].ToString());
                emplkup.EditValue = dr1["EmployeeID"].ToString();
                ServiceTimeLength = GeneralFunctions.fnInt32(dr1["ServiceTime"].ToString());
                dtSchedule.DateTime = GeneralFunctions.fnDate(dr1["ScheduleTime"].ToString());
                tmApptStart.EditValue = GeneralFunctions.fnDate(dr1["ScheduleTime"].ToString());

                txtRemarks.Text = dr1["ApptRemarks"].ToString();
                lbbooking.Text = "Booking on" + " : "
                    + GeneralFunctions.fnDate(dr1["BookingTime"].ToString()).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
            }
            dtblH.Dispose();

            PosDataObject.POS objp2 = new PosDataObject.POS();
            objp2.Connection = SystemVariables.Conn;
            dtblD = objp2.FetchApptDetail(intID);
            grdService.ItemsSource = dtblD;
            dtblD.Dispose();

            string refTaxExempt = "";
            string refDiscountLevel = "";
            string refTaxID = "";
            string refStoreCr = "";
            string refCID = "";
            string refCName = "";
            string refCAdd = "";
            double dblBalance = 0;
            string refARCredit = "";
            string refPOSNotes = "";

            FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd, ref refTaxExempt, ref refDiscountLevel, ref refTaxID,
                                    ref refStoreCr, ref refARCredit, ref refPOSNotes);

            strTaxExempt = refTaxExempt;
            strDiscountLevel = refDiscountLevel.Trim();
            if (strDiscountLevel == "") strDiscountLevel = "A";
            dblBalance = GetAccountBalance(intCustID);
            txtCust.Text = refCID;
            if (Settings.CentralExportImport == "Y")
            {
                if (cmbStore.Text == Settings.StoreCode) txtCustStore.Text = "";
                else txtCustStore.Text = cmbStore.Text;
            }

            strCustomerID = refCID;
            lbCustName.Text = refCName;

            lbCustAddress.Text = refCAdd;
            ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
            //lbCustNotes.Text = refPOSNotes;
            //ShowCustomerPhoto();
            //FetchCustomerNote("", "Customer", intCustID, DateTime.Today.Year, DateTime.Today.Month);
            dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
            dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
            dblCustAcctBalance = dblBalance;

        }

        private void FetchCustomer(int iCustID, ref string refCID, ref string refCName, ref string refCAdd,
                                   ref string refTaxExempt, ref string refDiscountLevel, ref string refTaxID,
                                   ref string refStoreCr, ref string refARCredit, ref string refPOSNotes)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtblCust = objPOS.FetchCustomerRecord(iCustID);
            foreach (DataRow dr in dtblCust.Rows)
            {
                refTaxExempt = dr["TaxExempt"].ToString();
                if (refTaxExempt.Trim() == "") refTaxExempt = "N";
                refDiscountLevel = dr["DiscountLevel"].ToString();
                if (refDiscountLevel == "") refDiscountLevel = "A";
                refTaxID = dr["TaxID"].ToString();
                refStoreCr = dr["StoreCredit"].ToString();
                refARCredit = dr["ARCreditLimit"].ToString();
                refCID = dr["CustomerID"].ToString();
                refCName = dr["Salutation"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                refCAdd = dr["Address1"].ToString() + "\n" + dr["City"].ToString();
                refPOSNotes = dr["POSNotes"].ToString();
            }
            dtblCust.Dispose();
        }

        private double GetAccountBalance(int intCID)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.FetchCustomerAcctPayBalance(intCID);
        }

        private string GetCustomerIssueStore(int intCID)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.FetchCustomerIssueStore(intCID);
        }

        private void ArrangeCustomerLine(double Scr, double ABal, string Tx)
        {
            string str1 = "";
            string str2 = "";
            if (Settings.DecimalPlace == 3)
            {
                if (Scr < 0) str1 = str1 + "Store Cr: " + "(" + Scr.ToString("f3").Remove(0, 1) + ")";
                else str1 = str1 + "Store Cr: " + Scr.ToString("f3");
                if (ABal < 0) str1 = str1 + "    " + "Acct Balance: " + "(" + ABal.ToString("f3").Remove(0, 1) + ")";
                else str1 = str1 + "    " + "Acct Balance: " + ABal.ToString("f3");
            }
            else
            {
                if (Scr < 0) str1 = str1 + "Store Cr: " + "(" + Scr.ToString("f").Remove(0, 1) + ")";
                else str1 = str1 + "Store Cr: " + Scr.ToString("f");
                if (ABal < 0) str1 = str1 + "    " + "Acct Balance: " + "(" + ABal.ToString("f").Remove(0, 1) + ")";
                else str1 = str1 + "    " + "Acct Balance: " + ABal.ToString("f");
            }


            lbCustBal.Text = str1;
            //lbCustTax.Text = str2;
        }

        private int IsValidCustID()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            string scd = Settings.StoreCode;
            if (Settings.CentralExportImport == "Y") if (txtCustStore.Text.Trim() != "") scd = txtCustStore.Text;
            return objPOS.ValidCustomerID(txtCust.Text.Trim(), scd);
        }

        private int GetCustID()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            string scd = Settings.StoreCode;
            if (Settings.CentralExportImport == "Y") if (txtCustStore.Text.Trim() != "") scd = txtCustStore.Text;
            return objPOS.GetCustomerID(txtCust.Text.Trim(), scd);
        }

        private void SetDefaultApptTime()
        {
            DateTime apptstart = new DateTime();
            apptstart = new DateTime(dtSchedule.DateTime.Year, dtSchedule.DateTime.Month, dtSchedule.DateTime.Day, GeneralFunctions.fnDate(tmApptStart.EditValue).Hour, GeneralFunctions.fnDate(tmApptStart.EditValue).Minute, 0);
            if (ServiceTimeLength <= 0)
            {
                tmApptStart.EditValue = apptstart;
                tmApptEnd.EditValue = apptstart;
            }
            else
            {
                tmApptStart.EditValue = apptstart;
                tmApptEnd.EditValue = apptstart.AddMinutes(ServiceTimeLength);
            }
        }

        private void GetServiceTime()
        {
            TimeSpan ts = new TimeSpan();
            ts = GeneralFunctions.fnDate(tmApptEnd.EditValue).Subtract(GeneralFunctions.fnDate(tmApptStart.EditValue));
            ServiceTimeLength = ts.Hours * 60 + ts.Minutes;
        }

        public void EnableDisableButton(System.Windows.Controls.Button bup, System.Windows.Controls.Button bdown, GridControl grdvw)
        {
            if (grdvw.ItemsSource != null)
                if (((grdvw.ItemsSource as DataTable).Rows.Count == 0) || ((grdvw.ItemsSource as DataTable).Rows.Count == 1))
                {
                    bup.IsEnabled = false;
                    bdown.IsEnabled = false;
                }
                /*else if (grdvw.IsFirstRow)
                {
                    bup.Enabled = false;
                    bdown.Enabled = true;
                }
                else if (grdvw.IsLastRow)
                {
                    bup.Enabled = true;
                    bdown.Enabled = false;
                }*/
                else
                {
                    bup.IsEnabled = true;
                    bdown.IsEnabled = true;
                }
        }

        private void Tbctrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbctrl.SelectedIndex == 0)
            {
                //GeneralFunctions.SetFocus(txtCust);
            }
            if (tbctrl.SelectedIndex == 1)
            {
                if (!blfetchCustomer)
                {
                    frm_CustomerBrw = new frm_CustomerBrwUC();
                    frm_CustomerBrw.bPOS = true;
                    //frm_CustomerBrw.GetCustomerGridDimensions();
                    frm_CustomerBrw.bar1.Visibility = Visibility.Collapsed;
                    frm_CustomerBrw.cmbFilter.Visibility = Visibility.Collapsed;
                    pnlCustMain.Children.Add(frm_CustomerBrw);
                    frm_CustomerBrw.storecd = cmbStore.Text;
                    frm_CustomerBrw.PopulateCustomerStatus();
                    frm_CustomerBrw.cmbFilter.EditValue = "Active Customers";
                    frm_CustomerBrw.FetchData(frm_CustomerBrw.cmbFilter.EditValue.ToString());
                    blfetchCustomer = true;
                    EnableDisableButton(btnUpCust, btnDownCust, frm_CustomerBrw.grdCustomer);
                }
                else
                {
                    frm_CustomerBrw.GetCustomerGridDimensions();
                }
            }
        }

        private void TxtCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCust.Text.Trim() != "")
            {
                int intpCust = IsValidCustID();
                if (intpCust > 0)
                {
                    blChangeCustomer = true;
                    string refTaxExempt = "";
                    string refDiscountLevel = "";
                    string refTaxID = "";
                    string refStoreCr = "";
                    string refCID = "";
                    string refCName = "";
                    string refCAdd = "";
                    double dblBalance = 0;
                    string refARCredit = "";
                    string refPOSNotes = "";
                    intCustID = GetCustID();
                    FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd,
                        ref refTaxExempt, ref refDiscountLevel, ref refTaxID, ref refStoreCr, ref refARCredit,
                        ref refPOSNotes);
                    strTaxExempt = refTaxExempt;
                    strDiscountLevel = refDiscountLevel;
                    if (strDiscountLevel == "") strDiscountLevel = "A";

                    dblBalance = GetAccountBalance(intCustID);
                    txtCust.Text = refCID;

                    strCustomerID = refCID;
                    lbCustName.Text = refCName;

                    lbCustAddress.Text = refCAdd;
                    ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
                    //lbCustNotes.Text = refPOSNotes;
                    //ShowCustomerPhoto();
                    //FetchCustomerNote("", "Customer", intCustID, DateTime.Today.Year, DateTime.Today.Month);
                    dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
                    dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
                    dblCustAcctBalance = dblBalance;
                    GeneralFunctions.SetFocus(emplkup);
                }
                else
                {
                    DocMessage.MsgError("Invalid/Inactive Customer ID");

                    if (fkybrd != null)
                    {
                        fkybrd.Close();

                    }

                    intCustID = 0;
                    txtCust.Text = "";
                    txtCustStore.Text = "";

                    lbCustName.Text = "";
                    lbCustAddress.Text = "";
                    lbCustBal.Text = "";

                    //picCustPhoto.Image = null;
                    strTaxExempt = "N";
                    strDiscountLevel = "A";

                    return;
                }
            }
            else
            {
                intCustID = 0;
                //lbCustID.Text = "";
                lbCustName.Text = "";
                txtCustStore.Text = "";
                lbCustAddress.Text = "";
                lbCustBal.Text = "";
                //lbCustNotes.Text = "";
                //lbCustTax.Text = "";
                //picCustPhoto.Image = null;
                strTaxExempt = "N";
                strDiscountLevel = "A";
            }
        }

        private void TxtCustStore_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            GeneralFunctions.SetFocus(txtCust);
        }

        private void CmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (frm_CustomerBrw != null)
            {
                frm_CustomerBrw.storecd = cmbStore.Text;
                frm_CustomerBrw.FetchData(frm_CustomerBrw.cmbFilter.SelectedIndex.ToString());
                if (cmbStore.Text != Settings.StoreCode) btnAddCust.Visibility = Visibility.Collapsed;
                else btnAddCust.Visibility = Visibility.Visible;
            }
        }

        private async void BtnCustSelect_Click(object sender, RoutedEventArgs e)
        {
            boolSelectCustomer = true;
            if (frm_CustomerBrw.gridView1.FocusedRowHandle > -1)
            {
                if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(frm_CustomerBrw.gridView1.FocusedRowHandle, frm_CustomerBrw.grdCustomer, frm_CustomerBrw.colID)) != intCustID)
                {
                    blChangeCustomer = true;
                    boolBookingExportFlagChanged = true;
                    string refTaxExempt = "";
                    string refDiscountLevel = "";
                    string refTaxID = "";
                    string refStoreCr = "";
                    string refCID = "";
                    string refCName = "";
                    string refCAdd = "";
                    double dblBalance = 0;
                    string refARCredit = "";
                    string refPOSNotes = "";

                    intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(frm_CustomerBrw.gridView1.FocusedRowHandle,
                                                         frm_CustomerBrw.grdCustomer, frm_CustomerBrw.colID));

                    FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd, ref refTaxExempt, ref refDiscountLevel, ref refTaxID,
                                    ref refStoreCr, ref refARCredit, ref refPOSNotes);

                    strTaxExempt = refTaxExempt;
                    strDiscountLevel = refDiscountLevel.Trim();
                    if (strDiscountLevel == "") strDiscountLevel = "A";
                    dblBalance = GetAccountBalance(intCustID);
                    txtCust.Text = refCID;
                    if (Settings.CentralExportImport == "Y")
                    {
                        if (cmbStore.Text == Settings.StoreCode) txtCustStore.Text = "";
                        else txtCustStore.Text = cmbStore.Text;
                    }

                    strCustomerID = refCID;
                    lbCustName.Text = refCName;

                    lbCustAddress.Text = refCAdd;
                    ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
                    //lbCustNotes.Text = refPOSNotes;
                    //ShowCustomerPhoto();
                    //FetchCustomerNote("", "Customer", intCustID, DateTime.Today.Year, DateTime.Today.Month);
                    dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
                    dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
                    dblCustAcctBalance = dblBalance;


                }
            }
            tbctrl.SelectedIndex = 0;
            emplkup.Focus();
            boolSelectCustomer = false;
        }

        private async void BtnAddCust_Click(object sender, RoutedEventArgs e)
        {
            if ((SecurityPermission.AcssCustomerAdd) || (SystemVariables.CurrentUserID <= 0))
            {
                int intNewRecID = 0;
                blurGrid.Visibility = Visibility.Visible;
                frm_CustomerDlg frm_CustomerDlg = new frm_CustomerDlg();
                try
                {

                    frm_CustomerDlg.ID = 0;
                    frm_CustomerDlg.Duplicate = false;
                    frm_CustomerDlg.AddFromPOS = true;
                    frm_CustomerDlg.OtherStoreRecord = false;
                    frm_CustomerDlg.Top = 0;
                    frm_CustomerDlg.BrowseFormUC = frm_CustomerBrw;
                    frm_CustomerDlg.ShowDialog();
                    intNewRecID = frm_CustomerDlg.NewID;
                }
                finally
                {
                    frm_CustomerDlg.Close();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
                if (intNewRecID > 0)
                {
                    await frm_CustomerBrw.SetCurrentRow(intNewRecID);
                    BtnCustSelect_Click(sender, e);
                }
            }
            else
            {
                DocMessage.MsgPermission();
            }
        }

        private void BtnUpCust_Click(object sender, RoutedEventArgs e)
        {
            frm_CustomerBrw.gridView1.FocusedRowHandle = frm_CustomerBrw.gridView1.FocusedRowHandle - 1;
            EnableDisableButton(btnUpCust, btnDownCust, frm_CustomerBrw.grdCustomer);
        }

        private void BtnDownCust_Click(object sender, RoutedEventArgs e)
        {
            frm_CustomerBrw.gridView1.FocusedRowHandle = frm_CustomerBrw.gridView1.FocusedRowHandle + 1;
            EnableDisableButton(btnUpCust, btnDownCust, frm_CustomerBrw.grdCustomer);
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            gridView7.FocusedRowHandle = gridView7.FocusedRowHandle - 1;
            EnableDisableButton(btnUpHeader, btnDownHeader, grdService);
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            gridView7.FocusedRowHandle = gridView7.FocusedRowHandle + 1;
            EnableDisableButton(btnUpHeader, btnDownHeader, grdService);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (emplkup.EditValue == null) return;
            if (GeneralFunctions.fnInt32(emplkup.EditValue) == 0) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_POSApptServiceDlg frm_POSApptServiceDlg = new frm_POSApptServiceDlg();
            try
            {
                frm_POSApptServiceDlg.EmployeeID = GeneralFunctions.fnInt32(emplkup.EditValue);
                frm_POSApptServiceDlg.ShowDialog();
                if (frm_POSApptServiceDlg.DialogResult == true)
                {
                    AdjustServices(frm_POSApptServiceDlg.SelectedService);
                    SetDefaultApptTime();
                    boolBookingExportFlagChanged = true;
                }
            }
            finally
            {
                frm_POSApptServiceDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        // Adjust Services after add new service 

        private void AdjustServices(DataTable newdtbl)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
            dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
            dtbl.DefaultView.Sort = "SKU asc";
            dtbl.DefaultView.ApplyDefaultSort = true;
            DataTable dtbl1 = grdService.ItemsSource as DataTable;
            if (dtbl1 == null)
            {
                dtbl1 = dtbl;
            }
            else
            {
                foreach (DataRow dro in dtbl1.Rows)
                {
                    dtbl.Rows.Add(new object[] { dro["ID"].ToString(),
                                                        dro["SKU"].ToString(),
                                                        dro["Description"].ToString(),
                                                        dro["MinimumServiceTime"].ToString()});
                }
            }
            bool flg = false;
            newdtbl.DefaultView.Sort = "LineNo desc";
            newdtbl.DefaultView.ApplyDefaultSort = true;
            foreach (DataRowView dr in newdtbl.DefaultView)
            {
                flg = false;
                foreach (DataRow dr1 in dtbl.Rows)
                {
                    if (dr["ID"].ToString() == dr1["ID"].ToString())
                    {
                        flg = true;
                        break;
                    }
                }
                if (!flg)
                {
                    dtbl.Rows.Add(new object[] { dr["ID"].ToString(),
                                                        dr["SKU"].ToString(),
                                                        dr["Description"].ToString(),
                                                        dr["MinimumServiceTime"].ToString()});

                    ServiceTimeLength = ServiceTimeLength + GeneralFunctions.fnInt32(dr["MinimumServiceTime"].ToString());
                }
            }
            grdService.ItemsSource = dtbl;
        }

        // Check validity before appointment posting

        private bool ValidAll()
        {
            if (intCustID == 0)
            {
                DocMessage.MsgInformation("Select Customer");
                GeneralFunctions.SetFocus(txtCust);
                return false;
            }
            if ((emplkup.EditValue == null) || (emplkup.EditValue == "0"))
            {
                DocMessage.MsgInformation("Select Staff");
                GeneralFunctions.SetFocus(emplkup);
                return false;
            }

            if (dtSchedule.EditValue == null)
            {
                DocMessage.MsgInformation("Select Appt. Date");
                GeneralFunctions.SetFocus(dtSchedule);
                return false;
            }

            if (dtSchedule.DateTime.Date < DateTime.Today)
            {
                DocMessage.MsgInformation("Previous date can not be allowed as Appt. Date");
                GeneralFunctions.SetFocus(dtSchedule);
                return false;
            }

            if (GeneralFunctions.fnDate(tmApptStart.EditValue) > GeneralFunctions.fnDate(tmApptEnd.EditValue))
            {
                DocMessage.MsgInformation("Invalid Appt. Time");
                GeneralFunctions.SetFocus(tmApptStart);
                return false;
            }

            if (grdService.ItemsSource == null)
            {
                DocMessage.MsgInformation("Select atleast 1 service.");
                GeneralFunctions.SetFocus(grdService);
                return false;
            }

            if ((grdService.ItemsSource as DataTable).Rows.Count == 0)
            {
                DocMessage.MsgInformation("Select atleast 1 service.");
                GeneralFunctions.SetFocus(grdService);
                return false;
            }

            GetServiceTime();

            SetDefaultApptTime();

            /*if (IsAttachedCustomer())
            {
                DocMessage.MsgInformation("This customer has another appointment within this time interval");
                GeneralFunctions.SetFocus(txtCust);
                return false;
            }

            if (IsAttachedStaff())
            {
                DocMessage.MsgInformation("This staff has another appointment within this time interval");
                GeneralFunctions.SetFocus(emplkup);
                return false;
            }*/

            return true;
        }

        // Check if this customer has another appointment within this time interval or not

        private bool IsAttachedCustomer()
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            return objp.IsAttachedClient(intID, intCustID, GeneralFunctions.fnDate(tmApptStart.EditValue), GeneralFunctions.fnDate(tmApptEnd.EditValue));
        }

        // Check if this staff has another appointment within this time interval or not

        private bool IsAttachedStaff()
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            return objp.IsAttachedStaff(intID, GeneralFunctions.fnInt32(emplkup.EditValue), GeneralFunctions.fnDate(tmApptStart.EditValue), GeneralFunctions.fnDate(tmApptEnd.EditValue));
        }

        private void GetPostData(ref DateTime sdldt, ref int srvtime)
        {
            sdldt = new DateTime(dtSchedule.DateTime.Year, dtSchedule.DateTime.Month, dtSchedule.DateTime.Day, GeneralFunctions.fnDate(tmApptStart.EditValue).Hour, GeneralFunctions.fnDate(tmApptStart.EditValue).Minute, 0);
            TimeSpan tspn = new TimeSpan();
            tspn = GeneralFunctions.fnDate(tmApptEnd.EditValue).Subtract(GeneralFunctions.fnDate(tmApptStart.EditValue));
            srvtime = tspn.Hours * 60 + tspn.Minutes;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = 0;
            if ((grdService.ItemsSource as DataTable).Rows.Count == 0) return;
            intRowID = gridView7.FocusedRowHandle;
            if (intRowID < 0) return;

            int i = 0;
            int mtime = 0;

            DataTable dsource = grdService.ItemsSource as DataTable;
            foreach (DataRow srv in dsource.Rows)
            {
                if (i == intRowID)
                {
                    mtime = GeneralFunctions.fnInt32(srv["MinimumServiceTime"].ToString());
                    break;
                }
                i++;
            }

            ServiceTimeLength = ServiceTimeLength - mtime;
            gridView7.DeleteRow(intRowID);

            SetDefaultApptTime();

        }

        private void Emplkup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (IsLoadedData)
            {
                grdService.ItemsSource = null;
                ServiceTimeLength = 0;
                SetDefaultApptTime();
                boolBookingExportFlagChanged = true;
            }
        }

        private void DtSchedule_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolBookingExportFlagChanged = true;
        }

        private void TmApptStart_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            SetDefaultApptTime();
            boolBookingExportFlagChanged = true;
        }

        private void TmApptEnd_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolBookingExportFlagChanged = true;
        }

        private void TxtRemarks_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolBookingExportFlagChanged = true;
        }

        private void BtnCancel_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();

            Close();
        }



        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidAll())
            {
                bool flag = false;
                int intval = 0;
                DateTime dtval = DateTime.Now;
                GetPostData(ref dtval, ref intval);
                PosDataObject.POS objpos = new PosDataObject.POS();
                objpos.Connection = SystemVariables.Conn;
                objpos.ID = intID;
                objpos.CustomerID = intCustID;
                objpos.EmployeeID = GeneralFunctions.fnInt32(emplkup.EditValue);
                objpos.LoginUserID = SystemVariables.CurrentUserID;
                objpos.ScheduleTime = dtval;
                objpos.ApptServiceTime = intval;
                objpos.ApptRemarks = txtRemarks.Text.Trim();
                objpos.ItemDataTable = grdService.ItemsSource as DataTable;
                objpos.BookingExpFlag = boolBookingExportFlagChanged ? "N" : prevBookingExpFlag;
                objpos.BeginTransaction();
                if (objpos.AddAppointment())
                {
                    intNewID = objpos.ID;
                    flag = true;
                }
                objpos.EndTransaction();

                if (flag)
                {
                    if (intID == 0)
                    {
                        SendEmail(intNewID);
                    }
                    isoperate = true;
                    intEmpID = GeneralFunctions.fnInt32(emplkup.EditValue);
                    dtApptStartTime = dtval;
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void SendEmail(int iApptID)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl1 = objPOS.GetAppointmentInfoToSendEMail(iApptID);

            foreach (DataRow dr in dtbl1.Rows)
            {
                if (dr["Email"].ToString() == "") continue;

                int ApptID = Convert.ToInt32(dr["ID"].ToString());

                /*
                string services = objPOS.GetAppointmentServiceName(ApptID.ToString());

                string strEmailHeader = "Apointment scheduled on " + Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("MMM d,yyyy hh:mm tt");

                string strEmailBody = dr["Client"].ToString() + " has booked an appointment with " + dr["Provider"].ToString() + " for " +
                services + " at " + Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("dd-MM-yyyy") + " " +
                Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("HH:mm");

                string sendemail = GeneralFunctions.SendEmailOnAppointmentBooking(dr["Client"].ToString(), dr["Email"].ToString(), strEmailHeader, strEmailBody);

                if (sendemail == "success")
                {
                    objPOS.UpdateEmailSentFlag(ApptID.ToString());
                }
                */


                string ApptStartTime = "";
                string ApptEndTime = "";

                string ApptStartTimeMinF = Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("h:mm");
                string ApptEndTimeMinF = Convert.ToDateTime(dr["ApptEnd"].ToString()).ToString("h:mm");

                if (ApptStartTimeMinF.EndsWith(":00"))
                {
                    ApptStartTime = Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("MMM d, yyyy htt");
                }
                else
                {
                    ApptStartTime = Convert.ToDateTime(dr["ApptStart"].ToString()).ToString("MMM d, yyyy h:mmtt");
                }

                if (ApptEndTimeMinF.EndsWith(":00"))
                {
                    ApptEndTime = Convert.ToDateTime(dr["ApptEnd"].ToString()).ToString("MMM d, yyyy htt");
                }
                else
                {
                    ApptEndTime = Convert.ToDateTime(dr["ApptEnd"].ToString()).ToString("MMM d, yyyy h:mmtt");
                }


                string sendemail = GeneralFunctions.SendEmailOnAppointmentBookingNew
                    (dr["Client"].ToString(), dr["Email"].ToString(), dr["Provider"].ToString(), ApptStartTime + " to " + ApptEndTime);

                if (sendemail == "success")
                {
                    objPOS.UpdateEmailSentFlag(ApptID.ToString());
                }

            }

            dtbl1.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void BtnApptCancel_Click(object sender, RoutedEventArgs e)
        {
            PosDataObject.POS objpos = new PosDataObject.POS();
            objpos.Connection = SystemVariables.Conn;
            objpos.ID = intID;
            objpos.LoginUserID = SystemVariables.CurrentUserID;
            objpos.ApptRemarks = txtRemarks.Text.Trim();
            objpos.BeginTransaction();
            bool retval = false;
            retval = objpos.CancelAppointment();
            objpos.EndTransaction();
            if (retval)
            {
                isoperate = true;
                IsCancel = true;
                intEmpID = GeneralFunctions.fnInt32(emplkup.EditValue);
                dtApptStartTime = dtSchedule.DateTime;
                ResMan.closeKeyboard();
                CloseKeyboards();

                Close();
            }
        }

        public void PopulateCustomerStores()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupCustomerStore();
            cmbStore.Items.Clear();
            foreach (DataRow dr in dbtblCust.Rows)
            {
                cmbStore.Items.Add(dr["IssueStore"].ToString());
            }
            dbtblCust.Dispose();
            cmbStore.EditValue = Settings.StoreCode;
            //cmbFilterCustomerStore.SelectedIndex = cmbFilterCustomerStore.Items.IndexOf(Settings.StoreCode);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtSchedule.Mask = SystemVariables.DateFormat;
            dtSchedule.MaskUseAsDisplayFormat = true;
            dtSchedule.DisplayFormatString = SystemVariables.DateFormat;

            fkybrd = new FullKeyboard();
            lbCustName.Text = "";
            lbCustAddress.Text = "";
            lbCustBal.Text = "";

            dtblCustNotes = new DataTable();
            dtblCustNotes.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCustNotes.Columns.Add("Note", System.Type.GetType("System.String"));
            dtblCustNotes.Columns.Add("DateTime", System.Type.GetType("System.String"));
            dtblCustNotes.Columns.Add("SpecialEvent", System.Type.GetType("System.String"));
            PopulateCustomerStores();
            if (Settings.CentralExportImport == "N") pnlcuststore.Visibility = Visibility.Collapsed;
            if (Settings.CentralExportImport == "Y")
            {
                txtCustStore.Visibility = Visibility.Visible;
                txtCustStore.Text = Settings.StoreCode;
            }
            else
            {
                txtCustStore.Visibility = Visibility.Hidden;
            }
            if (SystemVariables.CurrentUserID <= 0) operatepermission = "Y";
            else
            {
                PosDataObject.Employee objp1 = new PosDataObject.Employee();
                objp1.Connection = SystemVariables.Conn;
                operatepermission = objp1.CanOperateonOthersAppointment(SystemVariables.CurrentUserID);
            }

            if (operatepermission == "N") emplkup.IsReadOnly = true;
            //PopulateCustomer();
            PopulateStaff();
            if (intID == 0)
            {
                Title.Text = "New Appointment";
                dtSchedule.DateTime = dtApptStartTime;
                tmApptStart.EditValue = dtApptStartTime;
                //custlkup.EditValue = intposcustomer;
                emplkup.EditValue = intEmpID.ToString();
                lbbooking.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title.Text = "Update Appointment";
                btnApptCancel.Visibility = Visibility.Visible;
                if (CallFromAdmin)
                {
                    Title.Text = "View Appointment";
                    btnApptCancel.Visibility = Visibility.Collapsed;
                    tpCustomer.Visibility = Visibility.Collapsed;

                    txtCust.IsReadOnly = true;
                    txtCustStore.IsReadOnly = true;
                    emplkup.IsReadOnly = true;
                    btnAdd.Visibility = btnDelete.Visibility = Visibility.Collapsed;
                    dtSchedule.IsReadOnly = tmApptEnd.IsReadOnly = tmApptStart.IsReadOnly = txtRemarks.IsReadOnly = true;

                    btnOK.Visibility = Visibility.Collapsed;
                    btnKybrd.Visibility = Visibility.Hidden;
                    btnCancel.Content = "Close";
                }
                ShowData();
            }
            IsLoadedData = true;
            isoperate = false;
            IsCancel = false;




            boolBookingExportFlagChanged = false;
        }

        private void CmbStore_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Emplkup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtSchedule_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtCust_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Return)
            {
                if (txtCust.Text.Trim() != "")
                {
                    int intpCust = IsValidCustID();
                    if (intpCust > 0)
                    {
                        blChangeCustomer = true;
                        string refTaxExempt = "";
                        string refDiscountLevel = "";
                        string refTaxID = "";
                        string refStoreCr = "";
                        string refCID = "";
                        string refCName = "";
                        string refCAdd = "";
                        double dblBalance = 0;
                        string refARCredit = "";
                        string refPOSNotes = "";
                        intCustID = GetCustID();
                        FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd,
                            ref refTaxExempt, ref refDiscountLevel, ref refTaxID, ref refStoreCr, ref refARCredit,
                            ref refPOSNotes);
                        strTaxExempt = refTaxExempt;
                        strDiscountLevel = refDiscountLevel;
                        if (strDiscountLevel == "") strDiscountLevel = "A";

                        dblBalance = GetAccountBalance(intCustID);
                        txtCust.Text = refCID;

                        strCustomerID = refCID;
                        lbCustName.Text = refCName;

                        lbCustAddress.Text = refCAdd;
                        ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
                        //lbCustNotes.Text = refPOSNotes;
                        //ShowCustomerPhoto();
                        //FetchCustomerNote("", "Customer", intCustID, DateTime.Today.Year, DateTime.Today.Month);
                        dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
                        dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
                        dblCustAcctBalance = dblBalance;
                        GeneralFunctions.SetFocus(emplkup);
                    }
                    else
                    {
                        if (fkybrd != null)
                        {
                            fkybrd.Close();

                        }

                        DocMessage.MsgError("Invalid/Inactive Customer ID");

                        

                        intCustID = 0;
                        txtCust.Text = "";
                        txtCustStore.Text = "";

                        lbCustName.Text = "";
                        lbCustAddress.Text = "";
                        lbCustBal.Text = "";

                        //picCustPhoto.Image = null;
                        strTaxExempt = "N";
                        strDiscountLevel = "A";

                        return;
                    }
                }
                else
                {
                    intCustID = 0;
                    //lbCustID.Text = "";
                    lbCustName.Text = "";
                    txtCustStore.Text = "";
                    lbCustAddress.Text = "";
                    lbCustBal.Text = "";
                    //lbCustNotes.Text = "";
                    //lbCustTax.Text = "";
                    //picCustPhoto.Image = null;
                    strTaxExempt = "N";
                    strDiscountLevel = "A";
                }
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
