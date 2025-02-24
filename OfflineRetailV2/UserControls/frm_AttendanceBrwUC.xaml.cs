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
    /// Interaction logic for frm_AttendanceBrwUC.xaml
    /// </summary>
    public partial class frm_AttendanceBrwUC : UserControl
    {
        private bool blFlag;

        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }

        private int COUTID = 0;
        private DataTable dtblCloseout = null;
        private int intSelectedRowHandle;
        private bool lbDeleteMode;
        private const string SEPARATOR = "  |  ";
        private string strSQLfilter = "";
        private int intSQLempID = 0;
        private DateTime dtSQLSDate = Convert.ToDateTime(null);
        private DateTime dtSQLFDate = Convert.ToDateTime(null);
        private bool blStopfiring = true;

        private DateTime SDT = Convert.ToDateTime(null);
        private DateTime FDT = Convert.ToDateTime(null);

        DateTime GetShiftStartDate = Convert.ToDateTime(null);
        DateTime GetShiftEndDate = Convert.ToDateTime(null);


        private ClockInClockOutEmployeeControl fParent;

        public ClockInClockOutEmployeeControl ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        public bool DeleteMode
        {
            get { return lbDeleteMode; }
            set { lbDeleteMode = value; }
        }

        public int SelectedRowHandle
        {
            get { return intSelectedRowHandle; }
            set { intSelectedRowHandle = value; }
        }

        public string SQLfilter
        {
            get { return strSQLfilter; }
            set { strSQLfilter = value; }
        }

        public int SQLempID
        {
            get { return intSQLempID; }
            set { intSQLempID = value; }
        }

        public DateTime SQLSDate
        {
            get { return dtSQLSDate; }
            set { dtSQLSDate = value; }
        }

        public DateTime SQLFDate
        {
            get { return dtSQLFDate; }
            set { dtSQLFDate = value; }
        }

        public frm_AttendanceBrwUC()
        {
            InitializeComponent();
        }

        

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdTask.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdTask, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdTask.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTask, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdTask.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTask, colID)));
            return intRecID;
        }

        private DateTime SystemDateTime()
        {
            PosDataObject.Attendance obattn = new PosDataObject.Attendance();
            obattn.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return obattn.GetSystemDateTime();
        }

        public void InitializeForm()
        {
            dtFrom.EditValue = SystemDateTime().Date;
            dtTo.EditValue = SystemDateTime().Date;

            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            if (objShift.GetEmpShiftStatus(SystemVariables.CurrentUserID) == 0)
            {
                btnStartShift.IsEnabled = true;
                btnEndShift.IsEnabled = false;
            }
            else
            {
                btnStartShift.IsEnabled = false;
                btnEndShift.IsEnabled = true;
            }
            intSQLempID = SystemVariables.CurrentUserID;
            dtSQLSDate = SystemDateTime().Date;
            dtSQLFDate = SystemDateTime().Date;
            strSQLfilter = "  and a.empid = @EID and a.DayStart between @SDT and @FDT ";

            blStopfiring = false;
        }

        public void FetchGridData(string sqlFilter, int pEmp, DateTime pStart, DateTime pEnd)
        {
            if (!blStopfiring)
            {
                PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                objTask.Connection = SystemVariables.Conn;
                DataTable dbtbl = new DataTable();
                dbtbl = objTask.FetchData(sqlFilter, pEmp, pStart, pEnd, SystemVariables.DateFormat);
                grdTask.ItemsSource = dbtbl;
                dbtbl.Dispose();
                GeneralFunctions.SetRecordCountStatusOfEmployee(dbtbl.Rows.Count);
            }
        }

        private int GetStartShiftID()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetStartShift(SystemVariables.CurrentUserID);
        }

        private int GetEmployeeShiftID()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetEmpShiftID(SystemVariables.CurrentUserID);
        }

        private string GetEmployeeShiftStatus()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetShiftStatus(GetEmployeeShiftID());
        }

        private int GetOpenShift()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetOpenShift(GetEmployeeShiftID());
        }

        private bool GetHolidayRecord(DateTime pStart)
        {
            int intCount = 0;
            PosDataObject.Attendance objReportData = new PosDataObject.Attendance();
            objReportData.Connection = SystemVariables.Conn;
            intCount = objReportData.IsHolidayFound(pStart);
            if (intCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetShiftTime()
        {

            string ShiftDetail = GeneralFunctions.GetShiftDetail(SystemVariables.CurrentUserID);
            int intIndex = ShiftDetail.IndexOf(SEPARATOR);
            ShiftDetail = ShiftDetail.Substring(intIndex + SEPARATOR.Length, ShiftDetail.Length - SEPARATOR.Length - intIndex);
            int SpaceIndex = ShiftDetail.IndexOf(" - ");
            string ShiftStart = ShiftDetail.Substring(0, SpaceIndex);
            string ShiftEnd = ShiftDetail.Substring(SpaceIndex + 3, ShiftDetail.Length - SpaceIndex - 3);

            string strSampm = GeneralFunctions.AMPM(ShiftStart);
            string strEampm = GeneralFunctions.AMPM(ShiftEnd);

            int SShr = 0;
            int SSmin = 0;
            int SEhr = 0;
            int SEmin = 0;

            SShr = GeneralFunctions.GetHour(ShiftStart);
            SEhr = GeneralFunctions.GetHour(ShiftEnd);

            SSmin = GeneralFunctions.GetMinute(ShiftStart);
            SEmin = GeneralFunctions.GetMinute(ShiftEnd);

            TimeSpan TS = new TimeSpan();

            if (SystemVariables.CurrentUserID > 0)
            {
                if ((strSampm == "PM") && (strEampm == "AM"))
                {
                    if ((DateTime.Now.Hour > 12) && (DateTime.Now.Hour <= 23))
                    {
                        GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(SystemDateTime().AddDays(1).Year, SystemDateTime().AddDays(1).Month, SystemDateTime().AddDays(1).Day, SEhr, SEmin, 0);
                    }
                    else
                    {
                        GetShiftStartDate = new DateTime(SystemDateTime().AddDays(-1).Year, SystemDateTime().AddDays(-1).Month, SystemDateTime().AddDays(-1).Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                    }
                }
                if ((strSampm == "AM") && (strEampm == "AM"))
                {
                    if (SEhr >= SShr)
                    {
                        GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                    }
                    else
                    {
                        if ((SystemDateTime().Hour > 12) && (SystemDateTime().Hour <= 23))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                        }
                        if ((SystemDateTime().Hour > 0) && (SystemDateTime().Hour <= 12) &&
                                  (SystemDateTime().Hour >= SShr) && (SystemDateTime().Hour < SEhr))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().AddDays(1).Year, SystemDateTime().AddDays(1).Month, SystemDateTime().AddDays(1).Day, SEhr, SEmin, 0);
                        }

                        if ((SystemDateTime().Hour > 0) && (SystemDateTime().Hour <= 12) && (SystemDateTime().Hour >= SEhr) && (SystemDateTime().Hour < SShr))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().AddDays(-1).Year, SystemDateTime().AddDays(-1).Month, SystemDateTime().AddDays(-1).Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                        }
                    }
                }
                if ((strSampm == "PM") && (strEampm == "PM"))
                {
                    if (SEhr >= SShr)
                    {
                        GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                    }
                    else
                    {
                        if ((SystemDateTime().Hour > 0) && (SystemDateTime().Hour <= 12))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().AddDays(1).Year, SystemDateTime().AddDays(1).Month, SystemDateTime().AddDays(1).Day, SEhr, SEmin, 0);
                        }
                        if ((SystemDateTime().Hour > 12) && (SystemDateTime().Hour <= 23) && (SystemDateTime().Hour >= SShr))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().AddDays(1).Year, SystemDateTime().AddDays(1).Month, SystemDateTime().AddDays(1).Day, SEhr, SEmin, 0);
                        }

                        if ((SystemDateTime().Hour > 12) && (SystemDateTime().Hour <= 23) &&
                                  (SystemDateTime().Hour <= SEhr))
                        {
                            GetShiftStartDate = new DateTime(SystemDateTime().AddDays(-1).Year, SystemDateTime().AddDays(-1).Month, SystemDateTime().AddDays(-1).Day, SShr, SSmin, 0);
                            GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                        }
                    }
                }

                if ((strSampm == "AM") && (strEampm == "PM"))
                {
                    GetShiftStartDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SShr, SSmin, 0);
                    GetShiftEndDate = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SEhr, SEmin, 0);
                }
            }
        }

        public void RefreshFetchStatus()
        {
            intSQLempID = SystemVariables.CurrentUserID;
            dtSQLSDate = GeneralFunctions.fnDate(dtFrom.EditValue.ToString());
            dtSQLFDate = GeneralFunctions.fnDate(dtTo.EditValue.ToString());
            strSQLfilter = "  and a.empid = @EID and a.DayStart between @SDT and @FDT ";
        }


        private DateTime GetClockInDate()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetStartDate(SystemVariables.CurrentUserID);
        }

        private int GetClockIn()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.GetClockIN(SystemVariables.CurrentUserID);
        }

        private void GetInitialData()
        {
            bool blf = true;
            if (Settings.POSCardPayment == "Y") if (Settings.PaymentGateway == 1) blf = false;
            DataTable dtbl = new DataTable();
            PosDataObject.TenderTypes objTType = new PosDataObject.TenderTypes();
            objTType.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objTType.FetchPOSData(blf);
            foreach (DataRow dr in dtbl.Rows)
            {
                DataTable dtbl1 = new DataTable();
                PosDataObject.Closeout objC = new PosDataObject.Closeout();
                objC.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objC.FetchBlindTender("E", COUTID, GeneralFunctions.fnInt32(dr["ID"].ToString()));
                int cnt = 0;
                double amt = 0;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    cnt = GeneralFunctions.fnInt32(dr1["ECount"].ToString());
                    amt = GeneralFunctions.fnDouble(dr1["EReceipt"].ToString());
                }
                if (dr["Name"].ToString() == "House Account")
                {
                    dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["Name"].ToString() ,
                                                   amt,amt,cnt.ToString(),0});
                }
                else
                {
                    dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["Name"].ToString() ,
                                                   0,amt,cnt.ToString(),0-amt});
                }
            }

        }

        private bool IsProceedCloseout()
        {
            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            COUTID = objCloseout.GetCloseOutID("T", "E", SystemVariables.CurrentUserID, Settings.TerminalName);
            if (COUTID == 0) return false;
            else return true;
        }

        private string OpenSuspendTranCount()
        {
            string strReturn = "";
            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int cnt = objCloseout.OpenSuspndItem(COUTID);
            if (cnt == 0)
            {
                strReturn = "";
            }
            else if (cnt == 1)
            {
                strReturn = Properties.Resources.There_is + cnt.ToString() + Properties.Resources.open_suspended_transaction_exists;
            }
            else
            {
                strReturn = Properties.Resources.There_are + cnt.ToString() + Properties.Resources.open_suspended_transactions_exists;
            }
            return strReturn;
        }


        private void FDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (!blStopfiring)
                RefreshFetchStatus();

            if (dtSQLFDate >= dtSQLSDate)
            {
                FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
            }
        }

        private void CmbItem_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (!blStopfiring)
                RefreshFetchStatus();

            if (dtSQLFDate >= dtSQLSDate)
            {
                FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
            }
        }

        private void BtnStartShift_Click(object sender, RoutedEventArgs e)
        {
            string strError = "";
            if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_check_in) == MessageBoxResult.Yes)
            {
                if (GetEmployeeShiftStatus() == "Stop")
                {
                    //DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Shift not Started by Administrator.");
                    //return;
                    PosDataObject.Shift objshift = new PosDataObject.Shift();
                    objshift.Connection = SystemVariables.Conn;
                    objshift.ID = GetEmployeeShiftID();
                    objshift.LoginID = SystemVariables.CurrentUserID;
                    objshift.UpdateShiftStatus("Start");
                }

                SDT = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SystemDateTime().Hour, SystemDateTime().Minute, 0);
                GetShiftTime();
                if (GetHolidayRecord(SDT))
                {
                    if (DocMessage.MsgConfirmation(Properties.Resources.This_date_is_set_as_holiday_Do_you_want_to_continue) == MessageBoxResult.Yes)
                    {

                        PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                        objTask.Connection = SystemVariables.Conn;
                        objTask.EMPID = SystemVariables.CurrentUserID;
                        objTask.ShiftID = GeneralFunctions.GetShiftID(SystemVariables.CurrentUserID);

                        objTask.TaskStartDate = SDT;
                        objTask.TaskEndDate = FDT;

                        objTask.ShiftStartDate = GetShiftStartDate;
                        objTask.ShiftEndDate = GetShiftEndDate;
                        objTask.LoginID = SystemVariables.CurrentUserID;
                        strError = objTask.InsertShiftStartData();
                        btnStartShift.IsEnabled = false;
                        btnEndShift.IsEnabled = true;
                        FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                    objTask.Connection = SystemVariables.Conn;
                    objTask.EMPID = SystemVariables.CurrentUserID;
                    objTask.ShiftID = GeneralFunctions.GetShiftID(SystemVariables.CurrentUserID);

                    objTask.TaskStartDate = SDT;
                    objTask.TaskEndDate = FDT;

                    objTask.ShiftStartDate = GetShiftStartDate;
                    objTask.ShiftEndDate = GetShiftEndDate;
                    objTask.LoginID = SystemVariables.CurrentUserID;
                    strError = objTask.InsertShiftStartData();

                    btnStartShift.IsEnabled = false;
                    btnEndShift.IsEnabled = true;
                    RefreshFetchStatus();
                    FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
                }


            }
        }

        private async void BtnEndShift_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_check_out) == MessageBoxResult.Yes)
            {
                bool proceedflg = true;

                if (gridView1.FocusedRowHandle < 0)
                {
                    int intclkin = 0;
                    intclkin = GetClockIn();
                    if (intclkin == 0) return;
                    if (intclkin == 1)
                    {
                        DateTime dtF = GetClockInDate();
                        DateTime CDT1 = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SystemDateTime().Hour, SystemDateTime().Minute, 0);
                        if (CDT1 == dtF)
                        {
                            DocMessage.MsgInformation(Properties.Resources.Check_out_can_not_be_performed_within_a_minute);
                            proceedflg = false;
                            return;
                        }
                    }
                }
                else
                {
                    DateTime CDT = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SystemDateTime().Hour, SystemDateTime().Minute, 0);
                    if (CDT == GeneralFunctions.fnDate(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTask, coldt)))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Check_out_can_not_be_performed_within_a_minute);
                        proceedflg = false;
                        return;
                    }
                }

                if (proceedflg)
                {

                    double cshT = 0;
                    double ccT = 0;
                    double TS = 0;
                    string chk1 = "N";
                    string chk2 = "N";
                    
                    
                    // TotalSalesBeforeCheckout(ref TS);

                    /* Todo:
                    if (Settings.AcceptTips == "Y")
                    {
                        frmTipDlg frm_TipDlg = new frmTipDlg();
                        try
                        {
                            frm_TipDlg.ShowDialog();
                            if (frm_TipDlg.DialogResult == DialogResult.OK)
                            {
                                cshT = frm_TipDlg.val1;
                                ccT = frm_TipDlg.val2;
                                chk1 = frm_TipDlg.chkPrintF;
                                chk2 = frm_TipDlg.chkReceiptPrintF;
                            }
                            else
                            {
                                return;
                            }
                        }
                        finally
                        {
                            frm_TipDlg.Dispose();
                        }
                    }
                    */

                    int AID = GetStartShiftID();
                    PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                    objTask.Connection = SystemVariables.Conn;
                    DateTime FDT = Convert.ToDateTime(null);
                    FDT = new DateTime(SystemDateTime().Year, SystemDateTime().Month, SystemDateTime().Day, SystemDateTime().Hour, SystemDateTime().Minute, 0);
                    objTask.TaskEndDate = FDT;
                    objTask.LoginID = SystemVariables.CurrentUserID;
                    objTask.CashTip = cshT;
                    objTask.CCTip = ccT;
                    objTask.TotalSales = TS;
                    string strdelete = objTask.UpdateShiftEndData(GetStartShiftID());

                    if (GetOpenShift() == 0)
                    {
                        PosDataObject.Shift objshift = new PosDataObject.Shift();
                        objshift.Connection = SystemVariables.Conn;
                        objshift.ID = GetEmployeeShiftID();
                        objshift.LoginID = SystemVariables.CurrentUserID;
                        objshift.UpdateShiftStatus("Stop");
                    }

                    //if (chk1 == "Y")
                    //ExecuteCheckoutReport(AID, chk2);

                    btnStartShift.IsEnabled = true;
                    btnEndShift.IsEnabled = false;
                    RefreshFetchStatus();
                    FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);

                    if (Settings.AcceptTips == "Y")
                    {
                        if ((SecurityPermission.AccessBlindDrop) || (SystemVariables.CurrentUserID <= 0))
                        {
                            if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_Close_out) == MessageBoxResult.Yes)
                            {
                                if (IsProceedCloseout())
                                {
                                    string suspndstr = "";
                                    suspndstr = OpenSuspendTranCount();
                                    bool proceed = false;
                                    if (suspndstr == "") proceed = true;

                                    if (suspndstr != "")
                                    {
                                        DocMessage.MsgInformation(suspndstr);
                                        proceed = false;
                                    }
                                    if (proceed)
                                    {
                                        dtblCloseout = new DataTable();
                                        dtblCloseout.Columns.Add("ID", System.Type.GetType("System.String"));
                                        dtblCloseout.Columns.Add("TenderName", System.Type.GetType("System.String"));
                                        dtblCloseout.Columns.Add("ActualReceipt", System.Type.GetType("System.Double"));
                                        dtblCloseout.Columns.Add("ExpectedReceipt", System.Type.GetType("System.Double"));
                                        dtblCloseout.Columns.Add("ExpectedCount", System.Type.GetType("System.String"));
                                        dtblCloseout.Columns.Add("Difference", System.Type.GetType("System.Double"));
                                        GetInitialData();

                                        PosDataObject.Closeout objcls = new PosDataObject.Closeout();
                                        objcls.Connection = SystemVariables.Conn;
                                        objcls.CloseOutID = COUTID;
                                        objcls.CloseoutType = "E";
                                        objcls.Notes = "";
                                        objcls.LoginUserID = SystemVariables.CurrentUserID;
                                        objcls.TenderDataTable = dtblCloseout;
                                        objcls.TerminalName = Settings.TerminalName;
                                        objcls.BeginTransaction();
                                        if (objcls.CloseOutTransaction())
                                        {

                                        }
                                        objcls.EndTransaction();
                                        string strerrmsg = objcls.ErrorMsg;
                                        if ((strerrmsg == "") || (strerrmsg == null))
                                        {
                                            if (Settings.ReceiptPrinterName != "")
                                            {
                                                try
                                                {
                                                    RawPrinterHelper.OpenCashDrawer1(Settings.ReceiptPrinterName, Settings.CashDrawerCode);
                                                }
                                                catch
                                                {
                                                }
                                            }


                                            ExecuteReport();
                                        }
                                    }
                                }
                                else
                                {
                                    DocMessage.MsgInformation(Properties.Resources.No_transaction_found_for_close_out);
                                }
                            }
                        }
                    }
                }

            }
        }

        private void ExecuteReport()
        {

            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutReportProc("E", COUTID, Settings.TerminalName, Settings.TaxInclusive, "Y");//Settings.CentralExportImport == "Y" ? "Y" : "N"

            if (Settings.GeneralReceiptPrint == "N")
            {
                /*
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/

                POSSection.frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new POSSection.frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.PrintType = "Closeout";
                    frm_POSInvoicePrintDlg.CloseoutID = COUTID;
                    frm_POSInvoicePrintDlg.CloseoutType = "E";
                    frm_POSInvoicePrintDlg.IsCloseout = true;
                    frm_POSInvoicePrintDlg.CloseoutSaleHour = true;
                    frm_POSInvoicePrintDlg.CloseoutSaleDept = true;
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    frm_POSInvoicePrintDlg.Close();
                }
            }
            else
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

                OfflineRetailV2.Report.Closeout.repCloseOut rep_CloseOut = new OfflineRetailV2.Report.Closeout.repCloseOut();
                OfflineRetailV2.Report.Closeout.repCloseoutMain rep_CloseoutMain = new OfflineRetailV2.Report.Closeout.repCloseoutMain();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1 rep_CloseoutMain1 = new OfflineRetailV2.Report.Closeout.repCloseoutMain1();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx rep_CloseoutMain1_tx = new OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx();
                OfflineRetailV2.Report.Closeout.repCloseoutMain2 rep_CloseoutMain2 = new OfflineRetailV2.Report.Closeout.repCloseoutMain2();
                OfflineRetailV2.Report.Closeout.repTender rep_Tender = new OfflineRetailV2.Report.Closeout.repTender();
                OfflineRetailV2.Report.Closeout.repTenderCount rep_TenderCount = new OfflineRetailV2.Report.Closeout.repTenderCount();
                OfflineRetailV2.Report.Closeout.repTenderOverShort rep_TenderOverShort = new OfflineRetailV2.Report.Closeout.repTenderOverShort();
                OfflineRetailV2.Report.Closeout.repReturn rep_Return = new OfflineRetailV2.Report.Closeout.repReturn();
                OfflineRetailV2.Report.Closeout.repCloseoutAdditional rep_CloseoutAdditional = new OfflineRetailV2.Report.Closeout.repCloseoutAdditional();
                OfflineRetailV2.Report.Closeout.repSalesByDept rep_SalesByDept = new OfflineRetailV2.Report.Closeout.repSalesByDept();
                OfflineRetailV2.Report.Closeout.repSalesByHour rep_SalesByHour = new OfflineRetailV2.Report.Closeout.repSalesByHour();

                OfflineRetailV2.Report.Closeout.repCloseoutRent rep_CloseoutRent = new OfflineRetailV2.Report.Closeout.repCloseoutRent();
                OfflineRetailV2.Report.Closeout.repCloseoutRepair rep_CloseoutRepair = new OfflineRetailV2.Report.Closeout.repCloseoutRepair();

                DataTable dtbl1 = new DataTable("Tender");
                PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
                objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objCloseout.ShowTenderRecord("T", COUTID, Settings.TerminalName);

                DataTable dtbl2 = new DataTable("TenderC");
                PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
                objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCloseout1.ShowTenderRecord("C", COUTID, Settings.TerminalName);

                DataTable dtbl3 = new DataTable("TenderOverShort");
                PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
                objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objCloseout2.ShowTenderRecord("R", COUTID, Settings.TerminalName);

                DataTable dtbl4 = new DataTable("Header");
                PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
                objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl4 = objCloseout3.ShowHeaderRecord(Settings.TerminalName, SystemVariables.DateFormat);

                int rnt = 0;
                int rpr = 0;
                foreach (DataRow d in dtbl4.Rows)
                {
                    rnt = GeneralFunctions.fnInt32(d["RentInvoiceCount"].ToString());
                    rpr = GeneralFunctions.fnInt32(d["RepairInvoiceCount"].ToString());
                }


                DataTable dtbl7 = new DataTable("RET");
                PosDataObject.Closeout objCloseout6 = new PosDataObject.Closeout();
                objCloseout6.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl7 = objCloseout6.ShowReturnedRecord(Settings.TerminalName);

                DataTable dtbl5 = new DataTable("SH");
                PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl5 = objCloseout4.ShowSalesByHourRecord(Settings.TerminalName, SystemVariables.DateFormat);


                DataTable dtbl6 = new DataTable("SD");
                PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl6 = objCloseout5.ShowSalesByDeptRecord(Settings.TerminalName, SystemVariables.DateFormat);

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dtbl1);
                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dtbl2);
                DataSet ds3 = new DataSet();
                ds3.Tables.Add(dtbl3);

                DataSet ds4 = new DataSet();
                ds4.Tables.Add(dtbl4);

                DataSet ds7 = new DataSet();
                ds7.Tables.Add(dtbl7);

                DataSet ds5 = new DataSet();
                ds5.Tables.Add(dtbl5);

                DataSet ds6 = new DataSet();
                ds6.Tables.Add(dtbl6);

                GeneralFunctions.MakeReportWatermark(rep_CloseOut);
                rep_CloseOut.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseOut.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_CloseOut.rType.Text = Properties.Resources.By_Employee;

                rep_CloseOut.xrSubreport1.ReportSource = rep_CloseoutMain;
                if (Settings.TaxInclusive == "N")
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1;
                    rep_CloseoutMain1.DataSource = ds4;
                    rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                }
                else
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1_tx;
                    rep_CloseoutMain1_tx.DataSource = ds4;
                    rep_CloseoutMain1_tx.DecimalPlace = Settings.DecimalPlace;
                }
                rep_CloseOut.xrSubreport3.ReportSource = rep_CloseoutMain2;
                rep_CloseoutMain.DataSource = ds4;

                rep_CloseoutMain2.DataSource = ds4;
                rep_CloseoutMain.COType = "E";

                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;

                rep_CloseOut.xrSubreport7.ReportSource = rep_Tender;
                rep_Tender.DataSource = ds1;
                rep_CloseOut.xrSubreport8.ReportSource = rep_TenderCount;
                rep_TenderCount.DataSource = ds2;
                rep_TenderCount.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport10.ReportSource = rep_TenderOverShort;
                rep_TenderOverShort.DataSource = ds3;
                rep_TenderOverShort.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport4.ReportSource = rep_Return;
                rep_Return.DataSource = ds7;
                rep_Return.DecimalPlace = Settings.DecimalPlace;

                if (rnt > 0)
                {
                    rep_CloseOut.xrSubreport5.ReportSource = rep_CloseoutRent;
                    rep_CloseoutRent.DataSource = ds4;
                    rep_CloseoutRent.DecimalPlace = Settings.DecimalPlace;
                }

                if (rpr > 0)
                {
                    rep_CloseOut.xrSubreport6.ReportSource = rep_CloseoutRepair;
                    rep_CloseoutRepair.DataSource = ds4;
                    rep_CloseoutRepair.DecimalPlace = Settings.DecimalPlace;
                }

                if ((rnt == 0) && (rpr == 0))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt == 0) && (rpr > 0))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt > 0) && (rpr == 0))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top + rep_CloseOut.xrSubreport5.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                rep_SalesByHour.COType = "E";
                rep_SalesByHour.DataSource = ds5;
                rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                rep_SalesByDept.COType = "E";
                rep_SalesByDept.DecimalPlace = Settings.DecimalPlace;
                rep_SalesByDept.DataSource = ds6;

                /*if (Settings.CloseoutExport == "Y")
                {
                    int Expr = 0;
                    Expr = ExecuteExport(dtbl4, dtbl1);
                    if (Expr == 0)
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Export completed successfully." + "\n" + "Export data saved to " + strCloseoutExportLocation);
                    }
                    else
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Error while exporting...");
                    }
                }*/

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CloseOut.PrinterName = Settings.ReportPrinterName;
                    rep_CloseOut.CreateDocument();
                    rep_CloseOut.PrintingSystem.ShowMarginsWarning = false;
                    rep_CloseOut.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_CloseOut.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CloseOut;
                    window.ShowDialog();

                }
                finally
                {
                    rep_CloseoutMain.Dispose();
                    rep_CloseoutMain1.Dispose();
                    rep_CloseoutMain1_tx.Dispose();
                    rep_Tender.Dispose();
                    rep_CloseOut.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                }

                /*if ((Settings.CentralExportImport == "Y") && (strCloseoutType == "C"))
                {
                    strExportPath1 = ExpFileName();

                    bool blproceed = true;
                    bool blprev = CheckIfExportedToday();
                    if (blprev)
                    {
                        if (DocMessage.MsgConfirmation(Translation.SetMultilingualTextInCodes("Data has already been exported to central office today." + "\n" + "Do you want to continue agian?") == DialogResult.Yes)
                        {
                            blproceed = true;
                        }
                        else
                        {
                            blproceed = false;
                        }
                    }
                    else
                    {
                        if (DocMessage.MsgConfirmation(Translation.SetMultilingualTextInCodes("Do you want to export data to central office?") == DialogResult.Yes)
                        {
                            blproceed = true;
                        }
                        else
                        {
                            blproceed = false;
                        }
                    }
                    if (!blproceed) return;
                    blViewPrevFile = false;
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int rtn = ExecuteExport();
                        if (rtn == 0)
                        {
                            UpdateSalesExportFlag();
                            UpdateEmployeeAttnExportFlag();
                            InsertExpImpLog("E", strExportFile, strExportPath1);

                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Data exported to central office successfully." + "\n" + "File saved in " + strExportPath1);
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = strExportPath1;
                            p.Start();
                        }
                        if (rtn == 1)
                        {
                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("No data found for export to central office.");
                        }
                        if (rtn == 2)
                        {
                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Permission error while exporting to central office...");
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }*/
            }
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void BtnBack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Menu");
        }
    }
}
