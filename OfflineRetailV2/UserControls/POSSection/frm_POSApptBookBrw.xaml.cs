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
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Visual;
using DevExpress.XtraScheduler;
using OfflineRetailV2;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSApptBookBrw.xaml
    /// </summary>
    public partial class frm_POSApptBookBrw : Window
    {
        DevExpress.Xpf.Scheduling.SchedulerControl schdctrl1;

        #region Variables

        private string strApptAction;
        //private Appointment apt;
        //private Resource rs;
        private int TempID = 0;
        private int TempEmpID = 0;
        private int NewID = 0;
        private DateTime TempApptDate = DateTime.Now;
        private DateTime dtNew = DateTime.Now;
        private bool blLocateAppt = false;
        private bool blCancelAppt = false;
        private DataTable dtblRecallTbl;
        private string operatepermission = "N";
        private int intstep = 0;
        private int intposcustomer;
        private DateTime dtFunction = DateTime.Now;
        private bool AllLoad = false;

        private int intEmployeeID;
        private bool boolCallFromAdmin;

        public int poscustomer
        {
            get { return intposcustomer; }
            set { intposcustomer = value; }
        }

        public DataTable RecallTbl
        {
            get { return dtblRecallTbl; }
            set { dtblRecallTbl = value; }
        }

        public string ApptAction
        {
            get { return strApptAction; }
            set { strApptAction = value; }
        }

        public bool CallFromAdmin
        {
            get { return boolCallFromAdmin; }
            set { boolCallFromAdmin = value; }
        }

        public int EmployeeID
        {
            get { return intEmployeeID; }
            set { intEmployeeID = value; }
        }

        #endregion

        public frm_POSApptBookBrw()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        private void LoadScheduler()
        {
            schdctrl1 = new DevExpress.Xpf.Scheduling.SchedulerControl();
            pnlschd.Children.Add(schdctrl1);
        }

        private void SetStaffPanel(DataTable dtbl)
        {
            pnlstaff.Children.Clear();

            int i = 0;
            int leftpos = 0;
            int toppos = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                i++;
                TextBlock tb = new TextBlock();
                tb.TextAlignment = TextAlignment.Center;
                tb.TextWrapping = TextWrapping.WrapWithOverflow;
                tb.Height = 30;
                tb.Width = 100;
                tb.Foreground = Brushes.Black;
                tb.Text = dr["Employee"].ToString();
                leftpos = leftpos + (110 * (i - 1));
                
                
                if (dr["Rank"].ToString() == "11")
                {
                    tb.Background = Brushes.MistyRose;
                }

                if (dr["Rank"].ToString() == "12")
                {
                    tb.Background = Brushes.Lavender;

                }
                if (dr["Rank"].ToString() == "13")
                {
                    tb.Background = Brushes.SandyBrown;

                }
                if (dr["Rank"].ToString() == "14")
                {
                    tb.Background = Brushes.PaleGreen;

                }
                if (dr["Rank"].ToString() == "15")
                {
                    tb.Background = Brushes.Khaki;

                }
                if (dr["Rank"].ToString() == "16")
                {
                    tb.Background = Brushes.YellowGreen;

                }
                if (dr["Rank"].ToString() == "17")
                {
                    tb.Background = Brushes.Turquoise;

                }
                if (dr["Rank"].ToString() == "18")
                {
                    tb.Background = Brushes.Violet;

                }
                if (dr["Rank"].ToString() == "19")
                {
                    tb.Background = Brushes.Plum;

                }
                if (dr["Rank"].ToString() == "20")
                {
                    tb.Background = Brushes.LightPink;

                }

                tb.SetValue(Canvas.LeftProperty, Convert.ToDouble(leftpos));
                tb.SetValue(Canvas.TopProperty, Convert.ToDouble(toppos));

                pnlstaff.Children.Add(tb);
            }
        }

        private void PopulateStaff(string opt)
        {
            PosDataObject.Employee objCategory = new PosDataObject.Employee();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.GetAppointmentEmployee(opt);
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
            
            int firstid = 0;
            foreach (DataRow d in dbtblCat.Rows)
            {
                firstid = GeneralFunctions.fnInt32(d["ID"].ToString());
                break;
            }
            emplkup.EditValue = firstid.ToString();
            dbtblCat.Dispose();
        }

        private string GetService(int pHID)
        {
            string val = "";
            DataTable dtblD = new DataTable();
            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = SystemVariables.Conn;
            dtblD = objp1.FetchApptMappingData(pHID);
            foreach (DataRow dr in dtblD.Rows)
            {
                if (val == "") val = dr["Service"].ToString(); else val = val + ", " + dr["Service"].ToString();
            }
            return val;
        }

        private void GetApptCalender(DateTime dtF, DateTime dtT, int emp)
        {
            lbcnt.Text = "";

            int aptcnt = 0;
            DataTable dtblH = new DataTable();
            DataTable dtblD = new DataTable();
            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = SystemVariables.Conn;
            dtblH = objp1.FetchApptHeaderData(dtF, dtF, emp, SystemVariables.DateFormat);
            foreach (DataRow dr1 in dtblH.Rows)
            {
                string apptsubject = "";
                string apptdetail = "";
                string srv = "";
                string nt = "";
                srv = GetService(GeneralFunctions.fnInt32(dr1["ID"].ToString()));
                nt = dr1["Remarks"].ToString();
                apptsubject = "    "
                    + dr1["Customer"].ToString()
                    + "  / " + srv
                    + "         " + "Staff: "
                    + dr1["Employee"].ToString();



                apptdetail =
                    "Appt. Time: "
                    + GeneralFunctions.fnDate(dr1["ApptStartD"].ToString()).ToString("t")
                    + " " + "to" + " "
                    + GeneralFunctions.fnDate(dr1["ApptEndD"].ToString()).ToString("t") + "\n" +
                    "Booking Date: " + dr1["BookingDate"].ToString();
                if (nt != "") apptdetail = apptdetail + "\n" + nt;

                dr1["ApptSubject"] = apptsubject;
                dr1["ApptDetail"] = apptdetail;

            }
            aptcnt = dtblH.Rows.Count;

            schdctrl.RefreshData();

            //schdstrg.RefreshData();
            //schdstrg.BeginUpdate();

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("APPTID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("APPTSUB", System.Type.GetType("System.String"));
            dtbl.Columns.Add("APPTDET", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("EDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("LABEL", System.Type.GetType("System.String"));

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("APPTID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("APPT", System.Type.GetType("System.String"));
            foreach (DataRow dr in dtblH.Rows)
            {
                dtbl1.Rows.Add(new object[] { dr["ID"].ToString(), dr["ApptSubject"].ToString() });
            }


            DataTable dtblE = new DataTable();

            if (dtblH.Rows.Count > 0)
            {
                if (emp == 0) // all employees
                {
                    string[] strval = { "EmployeeID", "ShortName" };
                    dtblE = dtblH.DefaultView.ToTable(true, strval);
                }
            }

            DataTable dtblEmp = new DataTable();
            dtblEmp.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
            dtblEmp.Columns.Add("Employee", System.Type.GetType("System.String"));
            dtblEmp.Columns.Add("Rank", System.Type.GetType("System.String"));

            int erank = 10;

            if (dtblH.Rows.Count > 0)
            {
                if (emp == 0) // all employees
                {
                    foreach (DataRow dre in dtblE.Rows)
                    {
                        erank++;
                        dtblEmp.Rows.Add(new object[] { dre["EmployeeID"].ToString(), dre["ShortName"].ToString(), erank.ToString() });
                    }
                }
            }

            pnlstaff.Visibility = Visibility.Collapsed;
            //pnlemp.Height = 10;
            int emppnl = 0;
            foreach (DataRow dr in dtblH.Rows)
            {
                emppnl++;
                string strLabel = "2";
                if (emp != 0)
                {
                    if (intposcustomer > 0) if (GeneralFunctions.fnInt32(dr["CustomerID"].ToString()) == intposcustomer) strLabel = "8";
                }
                else
                {
                    if (erank - 10 <= Settings.MaxEmployeeInAppointmentScreen)
                    {
                        if (emppnl == 1)
                        {
                            SetStaffPanel(dtblEmp);
                            pnlstaff.Visibility = Visibility.Visible;
                        }

                        foreach (DataRow dre1 in dtblEmp.Rows)
                        {
                            if (dr["EmployeeID"].ToString() == dre1["EmployeeID"].ToString())
                            {
                                strLabel = dre1["Rank"].ToString();
                                break;
                            }
                        }
                    }
                }

                dtbl.Rows.Add(new object[] {
                                            dr["ID"].ToString(),
                                            dr["ID"].ToString(),
                                            dr["ApptSubject"].ToString(),
                                            dr["ApptDetail"].ToString(),
                                            GeneralFunctions.fnDate(dr["ApptStartD"].ToString()),
                                            GeneralFunctions.fnDate(dr["ApptEndD"].ToString()),
                                            strLabel });
                //schdstrg.Appointments.Add(apt);
            }

            //schdstrg.Resources.Add(rs);

            schdctrl.DataSource.AppointmentsSource = dtbl;
            schdctrl.DataSource.ResourcesSource = dtbl1.DefaultView.ToTable(true);
            //schdstrg.Appointments.DataSource = dtbl;
            //schdstrg.Resources.DataSource = dtbl1.DefaultView.ToTable(true);

            dtbl.Dispose();
            dtbl1.Dispose();
            dtblH.Dispose();

            schdctrl.Start = dtF;

            /*schdctrl.DataSource.AppointmentMappings.Subject =  "APPTSUB";
            schdctrl.DataSource.AppointmentMappings.Description = "APPTDET";
            schdctrl.DataSource.AppointmentMappings.End = "EDATE";
            schdctrl.DataSource.AppointmentMappings.Start = "SDATE";
            schdctrl.DataSource.AppointmentMappings.LabelId = "LABEL";
            schdctrl.DataSource.AppointmentMappings.ResourceId = "APPTID";
            schdctrl.DataSource.ResourceMappings.Id = "APPTID";
            schdctrl.DataSource.ResourceMappings.Caption = "APPT";*/

            //schdstrg.Appointments.Mappings.Subject = "APPTSUB";
            //schdstrg.Appointments.Mappings.Description = "APPTDET";
            //schdstrg.Appointments.Mappings.End = "EDATE";
            //schdstrg.Appointments.Mappings.Start = "SDATE";
            //schdstrg.Appointments.Mappings.Label = "LABEL";
            //schdstrg.Appointments.Mappings.ResourceId = "APPTID";
            //schdstrg.Resources.Mappings.Id = "APPTID";
            //schdstrg.Resources.Mappings.Caption = "APPT";

            schdctrl.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.DayView;
            schdctrl.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;

            if (aptcnt == 0) lbcnt.Text = "No Appointment";
            else if (aptcnt == 1) lbcnt.Text = "1 Appointment";
            else lbcnt.Text = aptcnt.ToString() + " " + " Appointments";

            //schdstrg.EndUpdate();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            blLocateAppt = false;
            blCancelAppt = false;
            if ((TempID == 0) || (btnAdd.Content.ToString() == "New Appointment"))
            {
                if (strApptAction == "Booking")
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_POSApptBookDlg frm_POSApptBookDlg = new frm_POSApptBookDlg();
                    try
                    {
                        frm_POSApptBookDlg.ApptStartTime = TempApptDate;
                        frm_POSApptBookDlg.EmpID = GeneralFunctions.fnInt32(emplkup.EditValue);
                        frm_POSApptBookDlg.ID = 0;
                        frm_POSApptBookDlg.poscustomer = intposcustomer;
                        frm_POSApptBookDlg.ShowDialog();
                        schdctrl.RefreshData();
                        TempID = frm_POSApptBookDlg.NewID;
                        dtNew = frm_POSApptBookDlg.ApptStartTime;
                        TempEmpID = frm_POSApptBookDlg.EmpID;
                        blLocateAppt = frm_POSApptBookDlg.bloperate;
                    }
                    finally
                    {
                        frm_POSApptBookDlg.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }

                    if (blLocateAppt)
                    {
                        dtCalendar.DateTime = dtNew;
                        emplkup.EditValue = TempEmpID.ToString();
                        GetApptCalender(dtCalendar.DateTime, dtCalendar.DateTime, Convert.ToInt32(emplkup.EditValue));

                        /*
                        Appointment apt = schdctrl.Storage.CreateAppointment(AppointmentType.Normal);
                        Resource rs = schdstrg.CreateResource(ResourceEmpty.Id);
                        apt.Start = dtNew;
                        apt.Duration = new TimeSpan(0, 2, 0);
                        //apt = new Appointment(dtNew, new TimeSpan(0, 2, 0));
                        schdctrl.ActiveView.SelectAppointment(apt, rs);
                        */
                        try
                        {
                            if (schdctrl.Views.DayView.TopRowTime != new TimeSpan(7, 10, 0))
                            {
                            TimeSpan ts = new TimeSpan(2, 0, 0);
                            schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Add(ts);
                            }
                        }
                        catch
                        {
                        }

                        if (strApptAction == "Booking") btnAdd.Content  = "Update Appointment";
                    }
                }
                if (strApptAction == "Invoice")
                {
                }
            }
            else
            {
                if (strApptAction == "Booking")
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_POSApptBookDlg frm_POSApptBookDlg1 = new frm_POSApptBookDlg();
                    try
                    {
                        frm_POSApptBookDlg1.ApptStartTime = TempApptDate;
                        frm_POSApptBookDlg1.ID = TempID;
                        if (boolCallFromAdmin)
                        {
                            frm_POSApptBookDlg1.CallFromAdmin = true;
                        }
                        frm_POSApptBookDlg1.ShowDialog();
                        schdctrl.RefreshData();
                        TempID = frm_POSApptBookDlg1.NewID;
                        dtNew = frm_POSApptBookDlg1.ApptStartTime;
                        TempEmpID = frm_POSApptBookDlg1.EmpID;
                        blLocateAppt = frm_POSApptBookDlg1.bloperate;
                        blCancelAppt = frm_POSApptBookDlg1.blCancel;
                    }
                    finally
                    {
                        frm_POSApptBookDlg1.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                    if (blLocateAppt)
                    {
                        dtCalendar.DateTime = dtNew;
                        emplkup.EditValue = TempEmpID.ToString();
                        GetApptCalender(dtCalendar.DateTime, dtCalendar.DateTime, Convert.ToInt32(emplkup.EditValue));


                        if (strApptAction == "Booking")
                        {
                            btnAdd.Content  = "Update Appointment";
                            if (blCancelAppt)
                            {
                                //schdctrl.SelectNextAppointment();
                                btnAdd.Content = "New Appointment";
                            }
                            else
                            {
                                /*
                                Appointment apt = schdctrl.Storage.CreateAppointment(AppointmentType.Normal);
                                Resource rs = schdstrg.CreateResource(ResourceEmpty.Id);

                                apt.Start = dtNew;
                                apt.Duration = new TimeSpan(0, 2, 0);

                                //apt = new Appointment(dtNew, new TimeSpan(0, 2, 0));
                                schdctrl.ActiveView.SelectAppointment(apt, rs);
                                */
                                try
                                {
                                    if (schdctrl.Views.DayView.TopRowTime != new TimeSpan(7, 10, 0))
                                    {
                                     TimeSpan ts = new TimeSpan(2, 0, 0);
                                    schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Add(ts);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }

                    }
                }
                if (strApptAction == "Invoice")
                {
                    dtblRecallTbl.Rows.Add(new object[] { TempID.ToString() });
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    Close();
                }
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        private void DtCalendar_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((dtCalendar.EditValue != null) && (emplkup.EditValue != null))
            {
                dtFunction = dtCalendar.DateTime;
                lbDate.Text = dtFunction.Date.ToString("D");
                GetApptCalender(dtFunction, dtFunction, Convert.ToInt32(emplkup.EditValue));
                if (blLocateAppt)
                {

                }
                else
                {
                    DevExpress.Xpf.Scheduling.ResourceItem resourceItem = schdctrl.CreateResourceItem();
                    //Resource rs = schdstrg.CreateResource(ResourceEmpty.Id);


                    DateTime selectdt = new DateTime(dtCalendar.DateTime.Year, dtCalendar.DateTime.Month, dtCalendar.DateTime.Day,
                        schdctrl.WorkTime.Start.Hours, schdctrl.WorkTime.Start.Minutes, 0);


                    //schdctrl.ActiveView.SetSelection(new TimeInterval(selectdt, new TimeSpan(0, 10, 0)), rs);
                    TempID = 0;
                    TempApptDate = selectdt;
                    try
                    {
                        if (schdctrl.Views.DayView.TopRowTime != new TimeSpan(7, 10, 0))
                        {
                            TimeSpan ts = new TimeSpan(2, 0, 0);
                            schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Add(ts);
                        }
                    }
                    catch
                    {
                    }

                    if (strApptAction == "Booking") btnAdd.Content = "New Appointment";
                }
            }
        }

        private void Emplkup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((dtCalendar.EditValue != null) && (emplkup.EditValue != null))
            {
                if (dtCalendar.EditValue == "")
                {
                    dtCalendar.DateTime = DateTime.Today;
                }
                dtFunction = dtCalendar.DateTime;
                lbDate.Text = dtFunction.Date.ToString("D");
                GetApptCalender(dtFunction, dtFunction, Convert.ToInt32(emplkup.EditValue));
                if (blLocateAppt)
                {
                    //LocateAppt();
                }
                else
                {
                    DevExpress.Xpf.Scheduling.ResourceItem resourceItem = schdctrl.CreateResourceItem();
                    //Resource rs = schdstrg.CreateResource(ResourceEmpty.Id);


                    DateTime selectdt = new DateTime(dtCalendar.DateTime.Year, dtCalendar.DateTime.Month, dtCalendar.DateTime.Day,
                        schdctrl.WorkTime.Start.Hours, schdctrl.WorkTime.Start.Minutes, 0);


                    //schdctrl.ActiveView.SetSelection(new TimeInterval(selectdt, new TimeSpan(0, 10, 0)), rs);
                    TempID = 0;
                    TempApptDate = selectdt;
                    try
                    {
                        if (schdctrl.Views.DayView.TopRowTime != new TimeSpan(7, 10, 0))
                        {
                            TimeSpan ts = new TimeSpan(2, 0, 0);
                            schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Add(ts);
                        }
                    }
                    catch
                    {
                    }
                    if (strApptAction == "Booking") btnAdd.Content = "New Appointment";
                }
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (dtCalendar.EditValue != null)
            {
                blLocateAppt = false;
                dtCalendar.DateTime = dtCalendar.DateTime.AddDays(-1);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (dtCalendar.EditValue != null)
            {
                blLocateAppt = false;
                dtCalendar.DateTime = dtCalendar.DateTime.AddDays(1);
            }
        }

        private void UpScrollButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan ts = new TimeSpan(2, 0, 0);
                schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Subtract(ts);
            }
            catch
            {
                schdctrl.Views.DayView.TopRowTime = new TimeSpan(0, 0, 0);
            }
        }

        private void DownScrollButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan ts = new TimeSpan(2, 0, 0);
                schdctrl.Views.DayView.TopRowTime = schdctrl.Views.DayView.TopRowTime.Add(ts);
            }
            catch
            {
                schdctrl.Views.DayView.TopRowTime = new TimeSpan(24, 0, 0);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtCalendar.Mask = SystemVariables.DateFormat;
            dtCalendar.MaskUseAsDisplayFormat = true;
            dtCalendar.DisplayFormatString = SystemVariables.DateFormat;

            if (strApptAction == "Booking")
            {
                Title.Text = "Book Appointment";
            }
            else
            {
                Title.Text = "Recall Appointment";
            }


            if (SystemVariables.CurrentUserID <= 0) operatepermission = "Y";
            else
            {
                PosDataObject.Employee objp1 = new PosDataObject.Employee();
                objp1.Connection = SystemVariables.Conn;
                operatepermission = objp1.CanOperateonOthersAppointment(SystemVariables.CurrentUserID);
            }

            if (operatepermission == "N")
            {
                PopulateStaff("");
                emplkup.IsReadOnly = true;
            }
            else
            {
                PopulateStaff("ALL");
            }
            dtblRecallTbl = new DataTable();
            dtblRecallTbl.Columns.Add("ID", System.Type.GetType("System.String"));
            if (strApptAction == "Booking") dtCalendar.EditValue = DateTime.Today.AddDays(7);
            if (strApptAction == "Invoice") dtCalendar.EditValue = DateTime.Today;
            if (strApptAction == "Invoice") btnAdd.Content = "OK";
            if (strApptAction == "Invoice") emplkup.EditValue = "0";
            AllLoad = true;
        }

        private void Schdctrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ISchedulerHitInfo r = schdctrl.CalcHitInfo(e.GetPosition(schdctrl));
                if ((r.HitTestType == SchedulerHitTestType.Appointment) || (r.HitTestType == SchedulerHitTestType.Cell))
                {
                    if (r.HitTestType == SchedulerHitTestType.Cell)
                    {
                        if (boolCallFromAdmin) return;
                        TempApptDate = r.ViewModel.Interval.Start;
                        if (strApptAction == "Booking") btnAdd.Content = "New Appointment";
                    }

                    if (r.HitTestType == SchedulerHitTestType.Appointment)
                    {
                        TempID = GeneralFunctions.fnInt32((r.ViewModel as DevExpress.Xpf.Scheduling.VisualData.AppointmentViewModel).Appointment.ResourceId);
                        TempApptDate = (r.ViewModel as DevExpress.Xpf.Scheduling.VisualData.AppointmentViewModel).Appointment.Start;

                        if (strApptAction == "Booking") btnAdd.Content = "Update Appointment";
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
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

        private void DtCalendar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
