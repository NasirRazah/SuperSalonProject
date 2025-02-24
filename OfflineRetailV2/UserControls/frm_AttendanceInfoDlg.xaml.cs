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
    /// Interaction logic for frm_AttendanceInfoDlg.xaml
    /// </summary>
    public partial class frm_AttendanceInfoDlg : Window
    {
        private frm_EmployeeDlg frmBrowsee;
        private int intID;
        private int intNewID;
        private int intEMPID;
        private const string SEPARATOR = "  |  ";
        private String EmpStartDate = "";
        private String EmpStartTime = "";
        private bool boolControlChanged;

        private string strViewEdit;

        private DateTime SDT = Convert.ToDateTime(null);
        private DateTime FDT = Convert.ToDateTime(null);

        DateTime GetShiftStartDate = Convert.ToDateTime(null);
        DateTime GetShiftEndDate = Convert.ToDateTime(null);

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

        public int EMPID
        {
            get { return intEMPID; }
            set { intEMPID = value; }
        }

        public string ViewEdit
        {
            get { return strViewEdit; }
            set { strViewEdit = value; }
        }

        public frm_EmployeeDlg BrowseFormE
        {
            get
            {
                return frmBrowsee;
            }
            set
            {
                frmBrowsee = value;
            }
        }

        public frm_AttendanceInfoDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        public void PopulateShift()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            DataTable dbtblShift = new DataTable();
            dbtblShift = objShift.FetchData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblShift.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbShift.ItemsSource = dtblTemp;
            cmbShift.EditValue = null;
            dbtblShift.Dispose();
        }

        private void ShowData()
        {
            int intTEMPID = 0;
            int intTShiftID = 0;
            DateTime dtTStartDate = Convert.ToDateTime(null);
            DateTime dtTEndDate = Convert.ToDateTime(null);

            PosDataObject.Attendance objAttn = new PosDataObject.Attendance();
            objAttn.Connection = SystemVariables.Conn;
            objAttn.ID = intID;
            DataTable dbtbl = new DataTable();
            dbtbl = objAttn.FetchRecord();
            foreach (DataRow dr in dbtbl.Rows)
            {
                intTEMPID = GeneralFunctions.fnInt32(dr["EMPID"].ToString());
                intTShiftID = GeneralFunctions.fnInt32(dr["SHIFTID"].ToString());
                lbEmployee.Text = dr["EMP"].ToString();
                dtTStartDate = GeneralFunctions.fnDate(dr["DAYSTART"].ToString());
                if (dr["DAYEND"].ToString() == "")
                {
                    dtTEndDate = Convert.ToDateTime(null);
                    dtFinish.EditValue = null;
                    timeFinish.EditValue = null;
                }
                else
                {
                    dtTEndDate = GeneralFunctions.fnDate(dr["DAYEND"].ToString());
                    dtFinish.EditValue = dtTEndDate;
                    timeFinish.EditValue = dtTEndDate;
                }
            }

            cmbShift.EditValue = intTShiftID.ToString();
            intEMPID = intTEMPID;
            dtStart.EditValue = dtTStartDate;
            timeStart.EditValue = dtTStartDate;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateShift();
            if (intID > 0)
            {
                ShowData();
            }

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_attendance;
                PosDataObject.Employee objemp = new PosDataObject.Employee();
                objemp.Connection = SystemVariables.Conn;
                lbEmployee.Text = objemp.GetEmployeeName(intEMPID);

                timeStart.EditValue = null;
                timeFinish.EditValue = null;
            }
            else
            {
                if (strViewEdit == "View")
                {
                    Title.Text = Properties.Resources.View_attendance;
                    cmbShift.IsEnabled = false;
                    dtStart.IsEnabled = dtFinish.IsEnabled = timeStart.IsEnabled = timeFinish.IsEnabled = false;
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Content = "Close";
                }
                if (strViewEdit == "Edit")
                {
                    Title.Text = Properties.Resources.EditView_attendance;
                    btnOk.Visibility = Visibility.Visible;
                }
            }
            boolControlChanged = false;
        }

        private bool ValidAllFields()
        {

            if (GeneralFunctions.fnInt32(cmbShift.EditValue) == 0)
            {
                DocMessage.MsgEnter(Properties.Resources.Shift);
                GeneralFunctions.SetFocus(cmbShift);
                return false;
            }

            if (dtStart.EditText == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Start_Date);
                GeneralFunctions.SetFocus(dtStart);
                return false;
            }
            if (timeStart.EditText == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Start_Time);
                GeneralFunctions.SetFocus(timeStart);
                return false;
            }

            if (dtFinish.EditText != "")
            {
                if (timeFinish.EditText == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.End_Time);
                    GeneralFunctions.SetFocus(timeFinish);
                    return false;
                }
            }

            if (timeFinish.EditText != "")
            {
                if (dtFinish.EditText == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.End_Date);
                    GeneralFunctions.SetFocus(dtFinish);
                    return false;
                }
            }

            if (dtFinish.Text.Trim() != "")
            {
                DateTime dvFrom = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day, timeStart.DateTime.Hour, timeStart.DateTime.Minute, 0);
                DateTime dvTo = new DateTime(dtFinish.DateTime.Year, dtFinish.DateTime.Month, dtFinish.DateTime.Day, timeFinish.DateTime.Hour, timeFinish.DateTime.Minute, 0);
                if (dvTo <= dvFrom)
                {
                    DocMessage.MsgInformation(Properties.Resources.End_DateTime_can_not_be_before_or_equal_to_Start_DateTime);

                    GeneralFunctions.SetFocus(dtFinish);
                    return false;
                }
            }

            if (!IsValidShift())
            {
                return false;
            }

            return true;
        }

        private bool IsValidShift()
        {
            int TSID = GeneralFunctions.GetShiftID(intEMPID);
            if (GeneralFunctions.fnInt32(cmbShift.EditValue.ToString()) != TSID)
            {

                DocMessage.MsgInformation(Properties.Resources.Selected_shift_has_not_assigned_to_this_employee);

                GeneralFunctions.SetFocus(cmbShift);
                return false;
            }

            string ShiftDetail = GeneralFunctions.GetShiftDetail(intEMPID);
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

            int intTotalMinutes = 0;
            TimeSpan TS = new TimeSpan();


            DateTime AdminStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day, timeStart.DateTime.Hour, timeStart.DateTime.Minute, 0);
            if ((strSampm == "PM") && (strEampm == "AM"))
            {
                if ((AdminStartDate.Hour > 12) && (AdminStartDate.Hour <= 23))
                {
                    GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SShr, SSmin, 0);
                    GetShiftEndDate = new DateTime(dtStart.DateTime.AddDays(1).Year, dtStart.DateTime.AddDays(1).Month,
                                                     dtStart.DateTime.AddDays(1).Day, SEhr, SEmin, 0);
                }
                else
                {
                    GetShiftStartDate = new DateTime(dtStart.DateTime.AddDays(-1).Year, dtStart.DateTime.AddDays(-1).Month,
                                                     dtStart.DateTime.AddDays(-1).Day, SShr, SSmin, 0);
                    GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                     dtStart.DateTime.Day, SEhr, SEmin, 0);
                }
            }
            if ((strSampm == "AM") && (strEampm == "AM"))
            {
                if (SEhr >= SShr)
                {

                    GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SShr, SSmin, 0);
                    GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SEhr, SEmin, 0);
                }
                else
                {
                    if ((AdminStartDate.Hour > 12) && (AdminStartDate.Hour <= 23))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                            SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SEhr, SEmin, 0);
                    }
                    if ((AdminStartDate.Hour > 0) && (AdminStartDate.Hour <= 12) &&
                              (DateTime.Now.Hour >= SShr) && (DateTime.Now.Hour < SEhr))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.AddDays(1).Year, dtStart.DateTime.AddDays(1).Month,
                                                         dtStart.DateTime.AddDays(1).Day, SEhr, SEmin, 0);

                    }

                    if ((AdminStartDate.Hour > 0) && (AdminStartDate.Hour <= 12) &&
                              (AdminStartDate.Hour >= SEhr) && (AdminStartDate.Hour < SShr))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.AddDays(-1).Year, dtStart.DateTime.AddDays(-1).Month,
                                                     dtStart.DateTime.AddDays(-1).Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SEhr, SEmin, 0);

                    }
                }
            }
            if ((strSampm == "PM") && (strEampm == "PM"))
            {
                if (SEhr >= SShr)
                {

                    GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SShr, SSmin, 0);
                    GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SEhr, SEmin, 0);
                }
                else
                {
                    if ((AdminStartDate.Hour > 0) && (AdminStartDate.Hour <= 12))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.AddDays(1).Year, dtStart.DateTime.AddDays(1).Month,
                                                         dtStart.DateTime.AddDays(1).Day, SEhr, SEmin, 0);
                    }
                    if ((AdminStartDate.Hour > 12) && (AdminStartDate.Hour <= 23) &&
                              (AdminStartDate.Hour >= SShr))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.AddDays(1).Year, dtStart.DateTime.AddDays(1).Month,
                                                         dtStart.DateTime.AddDays(1).Day, SEhr, SEmin, 0);

                    }

                    if ((AdminStartDate.Hour > 12) && (AdminStartDate.Hour <= 23) &&
                              (AdminStartDate.Hour <= SEhr))
                    {
                        GetShiftStartDate = new DateTime(dtStart.DateTime.AddDays(-1).Year, dtStart.DateTime.AddDays(-1).Month,
                                                     dtStart.DateTime.AddDays(-1).Day, SShr, SSmin, 0);
                        GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month,
                                                         dtStart.DateTime.Day, SEhr, SEmin, 0);

                    }
                }
            }

            if ((strSampm == "AM") && (strEampm == "PM"))
            {
                GetShiftStartDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                        SShr, SSmin, 0);
                GetShiftEndDate = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day,
                                                    SEhr, SEmin, 0);
            }



            return true;

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

        private bool SaveData()
        {

            SDT = new DateTime(dtStart.DateTime.Year, dtStart.DateTime.Month, dtStart.DateTime.Day, timeStart.DateTime.Hour, timeStart.DateTime.Minute, 0);
            if (dtFinish.EditText == "")
            {
                FDT = DateTime.MinValue;
            }
            else FDT = new DateTime(dtFinish.DateTime.Year, dtFinish.DateTime.Month, dtFinish.DateTime.Day, timeFinish.DateTime.Hour, timeFinish.DateTime.Minute, 0);
            int inIndex = Convert.ToString(SDT).IndexOf(" ");
            int inIndex1 = Convert.ToString(SDT).IndexOf(":00 ");
            EmpStartTime = Convert.ToString(SDT).Substring(inIndex + 1, inIndex1 - inIndex - 1);
            EmpStartTime = EmpStartTime + " " + Convert.ToString(SDT).Substring(inIndex1 + 3, Convert.ToString(SDT).Length - inIndex1 - 3);
            EmpStartDate = Convert.ToString(SDT).Substring(0, inIndex);

            string strError = "";
            if (ValidAllFields())
            {
                if (GetHolidayRecord(SDT))
                {

                    if (DocMessage.MsgConfirmation(Properties.Resources.This_date_is_set_as_holiday_Do_you_want_to_continue) == MessageBoxResult.Yes)
                    {
                        boolControlChanged = false;
                        PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                        objTask.Connection = SystemVariables.Conn;
                        objTask.ID = intID;
                        objTask.ShiftID = GeneralFunctions.fnInt32(cmbShift.EditValue.ToString());
                        objTask.EMPID = intEMPID;
                        objTask.TaskStartDate = SDT;
                        objTask.TaskEndDate = FDT;

                        objTask.ShiftStartDate = GetShiftStartDate;
                        objTask.ShiftEndDate = GetShiftEndDate;
                        objTask.LoginID = SystemVariables.CurrentUserID;

                        strError = intID == 0 ? objTask.InsertAttendanceData() : objTask.UpdateAttendanceData();

                        intID = objTask.ID;

                        if (strError != "")
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    boolControlChanged = false;
                    PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                    objTask.Connection = SystemVariables.Conn;
                    objTask.ID = intID;
                    objTask.ShiftID = GeneralFunctions.fnInt32(cmbShift.EditValue.ToString());
                    objTask.EMPID = intEMPID;
                    objTask.TaskStartDate = SDT;
                    objTask.TaskEndDate = FDT;

                    objTask.ShiftStartDate = GetShiftStartDate;
                    objTask.ShiftEndDate = GetShiftEndDate;
                    objTask.LoginID = SystemVariables.CurrentUserID;

                    strError = intID == 0 ? objTask.InsertAttendanceData() : objTask.UpdateAttendanceData();

                    intID = objTask.ID;

                    if (strError != "")
                    {
                        return false;
                    }
                    else
                        return true;
                }

            }
            else
                return false;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            Close();
        }

        private void CmbShift_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (SaveData())
                    {
                        boolControlChanged = false;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void CmbShift_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
