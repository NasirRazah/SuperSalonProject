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
using Microsoft.Win32;
using System.IO;
using System.Collections;
using DevExpress.Xpf.Editors;
using System.Text.RegularExpressions;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_EmployeeDlg.xaml
    /// </summary>
    public partial class frm_EmployeeDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_EmployeeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frm_EmployeeBrwUC frmBrowse;
        //public frmCustomerNoteCalender frm_CustomerNoteCalender;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private bool blStopfiring = true;
        private string strSQLfilter = "";
        private int intSQLempID = 0;
        private DateTime dtSQLSDate = Convert.ToDateTime(null);
        private DateTime dtSQLFDate = Convert.ToDateTime(null);

        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool blFetchAppointment = false;
        private bool blFetchAttendance = false;
        private bool blFetchAttendanceUpdate = false;
        private int intNoteYear;
        private int intNoteMonth;
        private bool blNotesSetting = false;
        private bool blNotesFiring = false;

        private int intSelectedRowHandle;
        private bool lbDeleteMode;
        private bool blfocus = false;

        private bool isload = false;
        private bool bpswdchanged = false;
        private string prevadminpswd = "";
        private bool prevlocked = false;

        private int OldTabIndex = 0;

        private DateTime prevmodifiedon = Convert.ToDateTime(null);
        private bool bIsCallForChangeExpiredPassword = false;

        private bool boolBookingExportFlagChanged;
        private string prevBookingExpFlag = "N";

        public bool IsCallForChangeExpiredPassword
        {
            get { return bIsCallForChangeExpiredPassword; }
            set { bIsCallForChangeExpiredPassword = value; }
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

        public int NoteYear
        {
            get { return intNoteYear; }
            set { intNoteYear = value; }
        }

        public int NoteMonth
        {
            get { return intNoteMonth; }
            set { intNoteMonth = value; }
        }

        public frm_EmployeeBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_EmployeeBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
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

        public class NoteData
        {
            public string ID { get; set; }
            public string Note { get; set; }
            public string DateTime { get; set; }
            public string SpecialEvent { get; set; }
            public string SEDateTime { get; set; }
            public DateTime CommonDateTime { get; set; }
            public ImageSource AttachMark { get; set; }
            public ImageSource Image { get; set; }
        }
        ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        public void PopulateNoteView()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "List View", Properties.Resources.List_View });
            dtbl.Rows.Add(new object[] { "Calendar View", Properties.Resources.Calendar_View });

            cmbView.ItemsSource = dtbl;
            cmbView.EditValue = "List View";
        }

        public void FetchNote(string sqlCriteria, string pRefType, int pRef)
        {
            if (blNotesFiring)
            {
                PosDataObject.Notes objNotes = new PosDataObject.Notes();
                objNotes.Connection = SystemVariables.Conn;
                DataTable dbtbl = new DataTable();
                dbtbl = objNotes.FetchNoteData(sqlCriteria, pRefType, pRef, SystemVariables.DateFormat);

                List<NoteData> dsource = new List<NoteData>();

                foreach (DataRow dr in dbtbl.Rows)
                {
                    dsource.Add(new NoteData()
                    {
                        ID = dr["ID"].ToString(),
                        Note = dr["Note"].ToString(),
                        SpecialEvent = dr["SpecialEvent"].ToString(),
                        DateTime = dr["DateTime"].ToString(),
                        CommonDateTime = GeneralFunctions.fnDate(dr["CommonDateTime"].ToString()),
                        SEDateTime = dr["DateTime"].ToString(),
                        AttachMark = dr["Attach"].ToString() == "Y" ? GetImage("/Resources/Icons/Pen.png") : null,
                        Image = GetImage("/Resources/Image/indicator.png")
                    });
                }

                foreach (DataRow dr in dbtbl.Rows)
                {


                    //MemoryStream ms = new MemoryStream();

                    //ImageSource image = new BitmapImage(new Uri("/Resources/Icons/Pen.png", UriKind.Relative));

                    //if (dr["Attach"].ToString() == "Y") dr["AttachMark"] = ms.ToArray();
                }

                grdNotes.ItemsSource = dsource;


                //dbtbl.Dispose();
            }

        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdNotes.ItemsSource as ICollection).Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdNotes, colNoteID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView5.FocusedRowHandle = intColCtr;
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdNotes.ItemsSource as ICollection).Count == 0) return intRecID;
            if (gridView5.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView5.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdNotes, colNoteID)));
            return intRecID;
        }

        public async Task SetCurrentRow1(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdTask.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdTask, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView2.FocusedRowHandle = intColCtr;
        }

        public async Task<int> ReturnRowID1()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdTask.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView2.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView2.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdTask, colID)));
            return intRecID;
        }

        private async Task EditNote()
        {
            int intRowID = -1;
            int RecordID = 0;
            intRowID = gridView5.FocusedRowHandle;

            if ((grdNotes.ItemsSource as ICollection).Count == 0) return;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_CustNotesDlg frm_CustNotesDlg = new POSSection.frm_CustNotesDlg();
            try
            {
                RecordID = await ReturnRowID();
                frm_CustNotesDlg.ID = RecordID;

                if (frm_CustNotesDlg.ID > 0)
                {
                    frm_CustNotesDlg.RefID = intID;
                    frm_CustNotesDlg.RefType = "Employee";
                    frm_CustNotesDlg.BrowseFormE = this;
                    frm_CustNotesDlg.ShowDialog();
                }
            }
            finally
            {
                //frm_CustNotesDlg.Dispose();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            await SetCurrentRow(RecordID);

            if (cmbView.EditValue.ToString() == "Calender View")
            {
                CallCalendar();
            }
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int RecordID = 0;
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_CustNotesDlg frm_CustNotesDlg = new POSSection.frm_CustNotesDlg();
            try
            {
                frm_CustNotesDlg.ID = 0;
                frm_CustNotesDlg.RefID = intID;
                frm_CustNotesDlg.RefType = "Employee";
                frm_CustNotesDlg.BrowseFormE = this;
                frm_CustNotesDlg.ShowDialog();
            }
            finally
            {
                if (frm_CustNotesDlg.NewID > 0) RecordID = frm_CustNotesDlg.NewID;
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (cmbView.EditValue.ToString() == "Calender View")
            {
                CallCalendar();
            }
            if (RecordID > 0) await SetCurrentRow(RecordID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditNote();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void GridView5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditNote();
        }

        private void GridView5_CustomCellAppearance(object sender, DevExpress.Xpf.Grid.CustomCellAppearanceEventArgs e)
        {

        }

        private void CmbView_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbView.EditValue.ToString() == "List View")
            {
                tcNotes.SelectedIndex = 0;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;

                tcNotes.SelectedIndex = 1;

                CallCalendar();
            }
        }

        public void CallCalendar()
        {
            GetCalendarData(intID, "Employee", CalenderStartDate(cbView.EditValue.ToString()));
        }

        private DateTime CalenderStartDate(string ViewType)
        {
            DateTime rdt = DateTime.Now;
            string strrtn = "";
            PosDataObject.Notes objnotes = new PosDataObject.Notes();
            objnotes.Connection = SystemVariables.Conn;
            objnotes.RefType = "Employee";
            objnotes.RefID = intID;
            strrtn = objnotes.GetMinSpecialEventDate();
            if (strrtn == "")
            {
                rdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                if ((ViewType == "Month View") || (ViewType == "Week View"))
                {
                    rdt = new DateTime(GeneralFunctions.fnDate(strrtn).Year, GeneralFunctions.fnDate(strrtn).Month, 1);
                }
                else
                {
                    rdt = new DateTime(GeneralFunctions.fnDate(strrtn).Year, GeneralFunctions.fnDate(strrtn).Month, GeneralFunctions.fnDate(strrtn).Day);
                }

            }
            return rdt;
        }

        public void GetCalendarData(int intRefID, string strRefType, DateTime CalenderSDate)
        {

            DateTime dtCalendarEnd = Convert.ToDateTime(null);

            DataTable dtblFetch = new DataTable();
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            dtblFetch = objNotes.FetchNoteData("S", strRefType, intRefID, SystemVariables.DateFormat);
            foreach (DataRow dr in dtblFetch.Rows)
            {
                /*MemoryStream ms = new MemoryStream();
                pos.Properties.Resources.attach.Save(ms, pos.Properties.Resources.attach.RawFormat);
                if (dr["Attach"].ToString() == "Y") dr["AttachMark"] = ms.ToArray();*/
            }
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTEID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTES", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTESDTL", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("EDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("LABEL", System.Type.GetType("System.String"));
            //dtbl.Columns.Add("ATTACH", System.Type.GetType("System.Byte[]"));
            foreach (DataRow dr in dtblFetch.Rows)
            {
                string strLabel = "2";
                string strEvent = dr["SpecialEvent"].ToString();
                if (strEvent == "Yes") strLabel = "11";
                else strLabel = "12";
                dtbl.Rows.Add(new object[] {
                                            dr["ID"].ToString(),
                                            dr["ID"].ToString(),
                                            dr["Note"].ToString(),
                                            dr["Note"].ToString(),
                                            GeneralFunctions.fnDate(dr["SDateTime"].ToString()),
                                            Convert.ToDateTime(null),strLabel });
                //dr["AttachMark"] });
            }





            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("NOTEID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CAPTION", System.Type.GetType("System.String"));
            //dtbl1.Columns.Add("ATTACH", System.Type.GetType("System.Byte[]"));
            foreach (DataRow dr in dtblFetch.Rows)
            {

                dtbl1.Rows.Add(new object[] {
                                            dr["ID"].ToString(),
                                            "Note" });
                //dr["AttachMark"]});
            }
            schedulerControl1.DataSource.AppointmentsSource = dtbl;
            schedulerControl1.DataSource.ResourcesSource = dtbl1;

            dtbl.Dispose();
            dtbl1.Dispose();
            dtblFetch.Dispose();

            schedulerControl1.Start = CalenderSDate;

            if (cbView.EditValue.ToString() == "Month View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.MonthView;
                
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            if (cbView.EditValue.ToString() == "Week View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.WeekView;
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            if (cbView.EditValue.ToString() == "Day View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.DayView;
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            schedulerControl1.UpdateLayout();

        }

        private void CbView_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            CallCalendar();
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

            dbtblShift.Dispose();
        }

        public void PopulateSecurityProfile()
        {
            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = SystemVariables.Conn;
            DataTable dbtblSProfile = new DataTable();
            dbtblSProfile = objSecurity.FetchData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSProfile.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbSecurity.ItemsSource = dtblTemp;

            int intFocusID = 0;
            foreach (DataRow dr in dbtblSProfile.Rows)
            {
                intFocusID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                break;
            }
            cmbSecurity.EditValue = intFocusID;
            dbtblSProfile.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            string RegisterModule = GeneralFunctions.RegisteredModules();
            if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
            {
                tpService.Visibility = Visibility.Collapsed;
            }
            if (Settings.ActiveAdminPswdForMercury)
            {
                lbpswd1.Visibility = Visibility.Visible;
                lbpswd2.Visibility = Visibility.Visible;
                txtadminpswd.Visibility = Visibility.Visible;
                txtcadminpswd.Visibility = Visibility.Visible;

            }

            PopulateShift();
            PopulateSecurityProfile();
            numPassword.PasswordChar = (char)0x25CF;
            numCPassword.PasswordChar = (char)0x25CF;

            txtadminpswd.PasswordChar = (char)0x25CF;
            txtcadminpswd.PasswordChar = (char)0x25CF;

            //SetDecimalPlace();

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Employee;
                txtEmpID.IsEnabled = true;
                txtEmpID.Text = "";
                numPassword.Text = "";
                numCPassword.Text = "";
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Employee;
                txtEmpID.IsEnabled = false;
                if (bIsCallForChangeExpiredPassword)
                {
                    SetScreenForChangePassword();
                }
                ShowData();
            }
            PermissionSettings();
            PopulateServices();
            //csStorePath = Application.StartupPath + "\\CustomerPhotos";
            intImageWidth = 128;
            intImageHeight = 112;
            isload = true;
            boolControlChanged = false;
            boolBookingExportFlagChanged = false;
        }

        private void SetScreenForChangePassword()
        {
            txtFName.IsEnabled = false;
            txtLName.IsEnabled = false;
            txtBadd1.IsEnabled = false;
            txtBadd2.IsEnabled = false;
            txtBcity.IsEnabled = false;
            txtBstate.IsEnabled = false;
            cmbBzip.IsEnabled = false;
            txtPhone1.IsEnabled = false;
            txtPhone2.IsEnabled = false;
            txtEmgPhone.IsEnabled = false;
            txtEmgContact.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtSSN.IsEnabled = false;
            cmbSecurity.IsEnabled = false;
            numPassword.IsEnabled = false;
            numCPassword.IsEnabled = false;
            cmbShift.IsEnabled = false;
            numRate.IsEnabled = false;
            lnkZip.IsEnabled = false;
            GeneralFunctions.SetFocus(txtadminpswd);
        }

        private void PermissionSettings()
        {
            if ((!SecurityPermission.AcssEmployeePassword) && (SystemVariables.CurrentUserID > 0))
            {
                cmbSecurity.IsReadOnly = true;
                numPassword.IsReadOnly = true;
            }
        }

        private DataTable GetAllServices()
        {
            PosDataObject.Employee objemp = new PosDataObject.Employee();
            objemp.Connection = SystemVariables.Conn;
            return objemp.FetchAllServices();
        }

        private DataTable GetAssignedServices()
        {
            PosDataObject.Employee objemp = new PosDataObject.Employee();
            objemp.Connection = SystemVariables.Conn;
            return objemp.FetchAssignedServices(intID);
        }

        private void PopulateServices()
        {
            DataTable dtbl = GetAllServices();
            DataTable dtbl1 = GetAssignedServices();
            foreach (DataRow dr in dtbl.Rows)
            {

                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    if (dr["ID"].ToString() == dr1["ID"].ToString())
                    {
                        dr["ServiceCheck"] = true;
                    }
                }
            }


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



            grdService.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dtbl.Dispose();
            dtbl1.Dispose();
        }

        public void ShowData()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            objEmployee.DecimalPlace = Settings.DecimalPlace;
            dbtbl = objEmployee.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                prevBookingExpFlag = dr["BookingExpFlag"].ToString();
                txtEmpID.Text = dr["EmployeeID"].ToString();
                txtFName.Text = dr["FirstName"].ToString();
                txtLName.Text = dr["LastName"].ToString();
                txtEmgContact.Text = dr["EmergencyContact"].ToString();

                txtBadd1.Text = dr["Address1"].ToString();
                txtBadd2.Text = dr["Address2"].ToString();
                txtBcity.Text = dr["City"].ToString();
                txtBstate.Text = dr["State"].ToString();
                cmbBzip.Text = dr["Zip"].ToString();

                txtPhone1.Text = dr["Phone1"].ToString();
                txtPhone2.Text = dr["Phone2"].ToString();
                txtEmgPhone.Text = dr["EmergencyPhone"].ToString();
                txtEmail.Text = dr["EMail"].ToString();
                txtSSN.Text = dr["SSNumber"].ToString();

                numPassword.Text = dr["Password"].ToString();
                numCPassword.Text = dr["Password"].ToString();
                cmbSecurity.EditValue = dr["ProfileID"].ToString();
                cmbShift.EditValue = dr["EmpShift"].ToString();
                numRate.Text = GeneralFunctions.fnDouble(dr["EmpRate"].ToString()).ToString("f2");

                if (dr["IncludeInAppointmentBook"].ToString() == "Y") chkAppointment.IsChecked = true; else chkAppointment.IsChecked = false;

                if (dr["ForcedPasscode"].ToString() == "Y")
                {
                    chkForcedPasscode.IsChecked = true;
                }
                else
                {
                    chkForcedPasscode.IsChecked = false;
                }

                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (dr["AdminPassword"].ToString() == "")
                    {
                        txtadminpswd.Text = "";
                    }
                    else
                    {
                        txtadminpswd.Text = GeneralFunctions.DecryptString(dr["AdminPassword"].ToString(), Settings.PasswordCode);

                    }
                    txtcadminpswd.Text = txtadminpswd.Text;
                    prevadminpswd = txtadminpswd.Text;

                    if (dr["AdminPasswordModifiedOn"].ToString() != "")
                    {
                        prevmodifiedon = Convert.ToDateTime(dr["AdminPasswordModifiedOn"].ToString());
                    }

                    if (dr["Locked"].ToString() == "Y")
                    {
                        chkLocked.IsChecked = true;
                        chkLocked.Visibility = Visibility.Visible;
                        prevlocked = true;
                    }

                    
                }


            }

            GeneralFunctions.LoadPhotofromDB("Employee", intID, pictPhoto);
        }

        private int CheckZip(string zip)
        {
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            return objZip.DuplicateCount(zip);
        }

        private void GetZipData(TextEdit zip, TextEdit state, TextEdit city)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            dtbl = objZip.ZIPData(zip.Text.Trim());
            foreach (DataRow dr in dtbl.Rows)
            {
                state.Text = dr["STATE"].ToString();
                city.Text = dr["CITY"].ToString();
            }
            dtbl.Dispose();
        }

        private async void LnkZip_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frmZipLookup frm_ZipLookup = new POSSection.frmZipLookup();
            try
            {
                frm_ZipLookup.ShowDialog();
                if (frm_ZipLookup.DialogResult == true)
                {
                    if (await frm_ZipLookup.GetColoum1Value() != "")
                    {
                        cmbBzip.Text = await frm_ZipLookup.GetColoum1Value();
                        //if (IsOtherStoreRecord) return;
                        if (cmbBzip.Text.Trim() != "")
                        {
                            if (CheckZip(cmbBzip.Text.Trim()) == 0)
                            {
                                blurGrid.Visibility = Visibility.Visible;
                                POSSection.frmZipCodeDlg frm_ZipCodeDlg = new POSSection.frmZipCodeDlg();
                                try
                                {
                                    frm_ZipCodeDlg.ID = 0;
                                    frm_ZipCodeDlg.ForceAdd = true;
                                    frm_ZipCodeDlg.ForceZip = cmbBzip.Text.Trim();
                                    frm_ZipCodeDlg.ShowDialog();
                                    if (frm_ZipCodeDlg.DialogResult == true)
                                    {
                                        txtBcity.Text = frm_ZipCodeDlg.ForceCity;
                                        txtBstate.Text = frm_ZipCodeDlg.ForceState;
                                        GeneralFunctions.SetFocus(txtPhone1);
                                    }
                                    if (frm_ZipCodeDlg.DialogResult == false)
                                    {
                                        cmbBzip.Text = "";
                                        txtBcity.Text = "";
                                        txtBstate.Text = "";
                                    }
                                }
                                finally
                                {
                                    blurGrid.Visibility = Visibility.Collapsed;
                                }
                            }
                            else
                            {
                                GetZipData(cmbBzip, txtBstate, txtBcity);
                                GeneralFunctions.SetFocus(txtPhone1);
                            }
                        }
                        else
                        {
                            txtBcity.Text = "";
                            txtBstate.Text = "";
                        }
                    }
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbBzip_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbBzip.Text.Trim() != "")
            {
                if (CheckZip(cmbBzip.Text.Trim()) == 0)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    POSSection.frmZipCodeDlg frm_ZipCodeDlg = new POSSection.frmZipCodeDlg();
                    try
                    {
                        frm_ZipCodeDlg.ID = 0;
                        frm_ZipCodeDlg.ForceAdd = true;
                        frm_ZipCodeDlg.ForceZip = cmbBzip.Text.Trim();
                        frm_ZipCodeDlg.ShowDialog();
                        if (frm_ZipCodeDlg.DialogResult == true)
                        {
                            txtBcity.Text = frm_ZipCodeDlg.ForceCity;
                            txtBstate.Text = frm_ZipCodeDlg.ForceState;
                            GeneralFunctions.SetFocus(txtPhone1);
                        }
                        if (frm_ZipCodeDlg.DialogResult == false)
                        {
                            cmbBzip.Text = "";
                            txtBcity.Text = "";
                            txtBstate.Text = "";
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    GetZipData(cmbBzip, txtBstate, txtBcity);
                    GeneralFunctions.SetFocus(txtPhone1);
                }
            }
            else
            {
                txtBcity.Text = "";
                txtBstate.Text = "";
            }
        }

        private bool IsDuplicateAdminPassword()
        {
            PosDataObject.Employee oe = new PosDataObject.Employee();
            oe.Connection = SystemVariables.Conn;
            oe.ID = intID;
            oe.AdminPassword = GeneralFunctions.EncryptString(txtadminpswd.Text, Settings.PasswordCode);
            return oe.IsDuplicatePassword();
        }

        private void IsValidAdminPasswordForMercury(ref int k)
        {
            int retval = 0;
            string val = txtadminpswd.Text;
            if (val.Length < 7)
            {
                retval = 1;
            }
            else
            {
                bool find_alphabet = false;
                bool find_number = false;
                for (int i = 0; i < val.Length; i++)
                {
                    if (char.IsLetter(val[i])) find_alphabet = true;
                    if (char.IsNumber(val[i])) find_number = true;
                }
                if ((!find_alphabet) && (!find_number))
                {
                    retval = 4;
                }
                if ((!find_alphabet) && (find_number))
                {
                    retval = 2;
                }
                if ((find_alphabet) && (!find_number))
                {
                    retval = 3;
                }
                if ((find_alphabet) && (find_number))
                {
                    retval = 0;
                }
            }
            k = retval;
        }

        private int DuplicateCount()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objEmployee.DuplicateCount(txtEmpID.Text.Trim());
        }

        private int DuplicatePassword()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objEmployee.DuplicatePassword(intID, numPassword.Text.Trim());
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                boolControlChanged = true;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            if (chkAll.IsChecked == true)
            {
                DataTable dtbl = grdService.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["ServiceCheck"] = true;
                }
                grdService.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                DataTable dtbl1 = grdService.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["ServiceCheck"] = false;
                }
                grdService.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private bool IsValidAll()
        {
            if (txtEmpID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Employee_ID);
                tcEmployee.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtEmpID);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtEmpID); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (txtFName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.First_Name);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtFName);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtFName); }), System.Windows.Threading.DispatcherPriority.Render);
                }

                return false;
            }

            if (txtLName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Last_Name);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtLName);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtLName); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (numPassword.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Password);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            

            if (numCPassword.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Confirm_Password);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numCPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numCPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (numPassword.Text.Trim().Length < 4)
            {
                DocMessage.MsgInformation("Password length must be 4 digits");
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (numCPassword.Text.Trim().Length < 4)
            {
                DocMessage.MsgInformation("Confirm Password length must be 4 digits");
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numCPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numCPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (cmbSecurity.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Security_Profile);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(cmbSecurity);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbSecurity); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (cmbShift.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Shift);
                tcEmployee.SelectedIndex = 0;

                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(cmbShift);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbShift); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (txtEmail.Text != "")
            {
                if (!GeneralFunctions.isEmail(txtEmail.Text))
                {
                    DocMessage.MsgEnter(Properties.Resources.Vaild_Email);
                    tcEmployee.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtEmail);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtEmail); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }
            
            if ((numPassword.Text.Trim() != "") && (numCPassword.Text.Trim() != "") &&
                (numPassword.Text.Trim() != numCPassword.Text.Trim()))
            {
                DocMessage.MsgInformation(Properties.Resources.Password__Confirm_Password_do_not_match_Please_Check);
                tcEmployee.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (Settings.ActiveAdminPswdForMercury)
            {
                if (txtadminpswd.Text == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Admin_Password);
                    tcEmployee.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtadminpswd);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if (txtcadminpswd.Text == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Confirm_Password);
                    tcEmployee.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtcadminpswd);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtcadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }


                int v = 0;
                IsValidAdminPasswordForMercury(ref v);
                if (v != 0)
                {
                    if (v == 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Admin_password_must_be_minimum_of_7_characters);
                        tcEmployee.SelectedIndex = 0;
                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(txtadminpswd);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                    if ((v == 2) || (v == 3) || (v == 4))
                    {
                        if (v == 2)
                            DocMessage.MsgInformation(Properties.Resources.Admin_password_must_have_an_alphabet);
                        if (v == 3)
                            DocMessage.MsgInformation(Properties.Resources.Admin_password_must_have_a_digit);
                        if (v == 4)
                            DocMessage.MsgInformation(Properties.Resources.Admin_password_must_have_both_alphabet_and_digit);
                        tcEmployee.SelectedIndex = 0;
                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(txtadminpswd);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }

                if ((txtadminpswd.Text.Trim() != "") && (txtcadminpswd.Text.Trim() != "") &&
                (txtadminpswd.Text.Trim() != txtcadminpswd.Text.Trim()))
                {
                    DocMessage.MsgInformation(Properties.Resources.Admin_Password__Confirm_Password_do_not_match_Please_Check);
                    tcEmployee.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtadminpswd);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if (intID > 0)
                {
                    if (prevadminpswd != txtadminpswd.Text)
                    {
                        if (IsDuplicateAdminPassword())
                        {
                            DocMessage.MsgInformation(Properties.Resources.Duplicate_Admin_Password);
                            tcEmployee.SelectedIndex = 0;
                            if (OldTabIndex == 0)
                            {
                                GeneralFunctions.SetFocus(txtadminpswd);
                            }
                            else
                            {
                                Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtadminpswd); }), System.Windows.Threading.DispatcherPriority.Render);
                            }
                            return false;
                        }
                    }
                }

            }

            if (numPassword.Text == Settings.ADMINPASSWORD)
            {
                DocMessage.MsgInformation("Invalid Employee ID/ Password");
                tcEmployee.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numPassword);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numPassword); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            objEmployee.LoginUserID = SystemVariables.CurrentUserID;

            objEmployee.EmployeeID = txtEmpID.Text;
            objEmployee.FirstName = txtFName.Text.Trim();
            objEmployee.LastName = txtLName.Text.Trim();
            objEmployee.EmergencyContact = txtEmgContact.Text.Trim();

            objEmployee.Address1 = txtBadd1.Text.Trim();
            objEmployee.Address2 = txtBadd2.Text.Trim();
            objEmployee.City = txtBcity.Text.Trim();
            objEmployee.State = txtBstate.Text.Trim();
            objEmployee.Zip = cmbBzip.Text.Trim();

            objEmployee.EmergencyPhone = txtEmgPhone.Text.Trim();
            objEmployee.Phone1 = txtPhone1.Text.Trim();
            objEmployee.Phone2 = txtPhone2.Text.Trim();
            objEmployee.EMail = txtEmail.Text.Trim();
            objEmployee.SSNumber = txtSSN.Text.Trim();

            objEmployee.EmpPassword = numPassword.Text.Trim();
            objEmployee.ProfileCRC = 111;
            objEmployee.ProfileID = GeneralFunctions.fnInt32(cmbSecurity.EditValue.ToString()); ;

            objEmployee.EmpShift = GeneralFunctions.fnInt32(cmbShift.EditValue.ToString());
            objEmployee.EmpRate =  GeneralFunctions.fnDouble(numRate.Text);

            if (chkAppointment.IsChecked == true) objEmployee.IncludeInAppointmentBook = "Y"; else objEmployee.IncludeInAppointmentBook = "N";

            if (chkForcedPasscode.IsChecked == true) objEmployee.ForcedPasscode = "Y"; else objEmployee.ForcedPasscode = "N";

            objEmployee.ID = intID;

            if (ReScan == 1)  // Photo Scanned or Selected from Folder but not saved
            {
                objEmployee.PhotoFilePath = strPhotoFile;
            }
            else
            {
                objEmployee.PhotoFilePath = "";
            }

            if (pictPhoto.Source != null)
            {
                objEmployee.EmployeePhoto = GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source);
            }
            else
            {
                objEmployee.EmployeePhoto = null;
            }

            if (Settings.ActiveAdminPswdForMercury)
            {
                objEmployee.AdminPassword = GeneralFunctions.EncryptString(txtadminpswd.Text, Settings.PasswordCode);
                objEmployee.AdminPasswordModifiedOn = (txtadminpswd.Text == prevadminpswd) ? prevmodifiedon : DateTime.Now;

                objEmployee.Locked = chkLocked.IsChecked == true ? "Y" : "N";
            }
            else
            {
                objEmployee.AdminPassword = "";
                objEmployee.AdminPasswordModifiedOn = prevmodifiedon;
                objEmployee.Locked = "N";
                objEmployee.IsChangingAdminPassword = false;
            }

            objEmployee.ThisStoreCode = Settings.StoreCode;

            objEmployee.BookingExpFlag = boolBookingExportFlagChanged ? "N" : prevBookingExpFlag;

            if (intID == 0)
            {
                strError = objEmployee.InsertData();
                intNewID = objEmployee.ID;

                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (SystemVariables.CurrentUserID == 0)
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event5, "Successful", txtEmpID.Text);
                    else
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event5, "Successful", txtEmpID.Text);
                }
            }
            else
            {
                strError = objEmployee.UpdateData();
                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (txtadminpswd.Text != prevadminpswd)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event4, "Successful", txtEmpID.Text);
                        else
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event4, "Successful", txtEmpID.Text);
                    }

                    bool bcheck = chkLocked.IsChecked == true ? true : false;
                    if (prevlocked != bcheck)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event3, "Successful", txtEmpID.Text);
                        else
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event3, "Successful", txtEmpID.Text);
                    }
                }
            }
            if (strError == "")
            {
                bool flag = false;
                PosDataObject.Employee objEmployee1 = new PosDataObject.Employee();
                objEmployee1.Connection = SystemVariables.Conn;
                objEmployee1.LoginUserID = SystemVariables.CurrentUserID;
                objEmployee1.ID = intID;
                objEmployee1.SplitDataTable = grdService.ItemsSource as DataTable;
                objEmployee1.AdminPassword = (Settings.ActiveAdminPswdForMercury) ? GeneralFunctions.EncryptString(txtadminpswd.Text, Settings.PasswordCode) : "";
                objEmployee1.IsChangingAdminPassword = (txtadminpswd.Text == prevadminpswd) ? false : true;
                objEmployee1.AppEmpID = (intID == 0) ? intNewID : intID;
                objEmployee1.BeginTransaction();
                if (objEmployee1.AddServiceMapping())
                {
                    flag = true;
                }
                objEmployee1.EndTransaction();
                if (flag) return true; else return false;
            }
            else
            {
                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (intID == 0)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event5, "Failed", txtEmpID.Text);
                        else
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event5, "Failed", txtEmpID.Text);
                    }
                    else
                    {
                        if (txtadminpswd.Text != prevadminpswd)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event4, "Failed", txtEmpID.Text);
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event4, "Failed", txtEmpID.Text);
                        }
                        bool bcheck = chkLocked.IsChecked == true ? true : false;
                        if (prevlocked != bcheck)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event3, "Failed", txtEmpID.Text);
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event3, "Failed", txtEmpID.Text);
                        }
                    }
                }
                return false;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (!bIsCallForChangeExpiredPassword) BrowseForm.FetchData();
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

        private void TpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TpService_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TabpAttendance_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void TabpNotes_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 3;
        }

        private void TabpAppointments_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 4;
        }

        private void TcEmployee_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = tcEmployee.SelectedIndex;

            

            if (i == 1)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            intID = NewID;
                        }
                    }
                }
            }

            if (i == 2)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            intID = NewID;
                        }
                    }
                }

                if (intID > 0)
                {
                    if (!blFetchAttendance)
                    {
                        PopulateAttendanceFilter();

                        dtFrom.EditValue = DateTime.Today.Date;
                        dtTo.EditValue = DateTime.Today.Date;
                        intSQLempID = intID;
                        dtSQLSDate = DateTime.Today.Date;
                        dtSQLFDate = DateTime.Today.Date;
                        strSQLfilter = "  AND a.EMPID = @EID AND a.DayStart BETWEEN @SDT AND @FDT ";
                        blStopfiring = false;
                        blFetchAttendance = true;
                        RefreshFetchStatus();
                        FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
                        blFetchAttendanceUpdate = SecurityPermission.AcssEmployeeAttendanceUpdate;
                        if (SystemVariables.CurrentUserID <= 0)
                        {
                            blFetchAttendanceUpdate = true;
                        }
                        if (blFetchAttendanceUpdate) btnAttnEdit.Content = "Edit/View";
                        else btnAttnEdit.Content = "View";
                    }
                }
            }

            if (i == 3)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            intID = NewID;
                        }
                    }
                }
                if (!blNotesSetting)
                {
                    blNotesSetting = true;
                    blNotesFiring = true;
                    PopulateNoteView();
                    cmbView.EditValue = "List View";
                    FetchNote("ALL", "Employee", intID);
                }
            }

            
        }

        public void RefreshFetchStatus()
        {
            intSQLempID = intID;
            strSQLfilter = "  AND a.EMPID = @EID AND a.DayStart BETWEEN @SDT AND @FDT ";
        }

        public void FetchGridData(string sqlFilter, int pEmp, DateTime pStart, DateTime pEnd)
        {
            if (((dtFilter.EditValue.ToString() == "This Week") || (dtFilter.EditValue.ToString() == "This Month")) || ((dtFilter.EditValue.ToString() == "Custom") && (dtFrom.EditValue != null) && (dtTo.EditValue != null)))
            {
                PosDataObject.Attendance objTask = new PosDataObject.Attendance();
                objTask.Connection = SystemVariables.Conn;
                DataTable dbtbl = new DataTable();

                DateTime dt1 = Convert.ToDateTime(null);
                DateTime dt2 = Convert.ToDateTime(null);
                if (dtFilter.EditValue.ToString() == "This Week")
                {
                    DateTime now = DateTime.Now;
                    int dayOfWeek = (int)now.DayOfWeek;
                    dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
                    DateTime startOfWeek = now.AddDays(1 - (int)now.DayOfWeek);
                    dt1 = startOfWeek.Date;
                    dt2 = dt1.AddDays(6);
                }

                if (dtFilter.EditValue.ToString() == "This Month")
                {
                    dt1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    dt2 = dt1.AddMonths(1).AddDays(-1);
                }

                if (dtFilter.EditValue.ToString() == "Custom")
                {
                    dt1 = GeneralFunctions.fnDate(dtFrom.EditValue.ToString());
                    dt2 = GeneralFunctions.fnDate(dtTo.EditValue.ToString());
                }

                dbtbl = objTask.FetchData(sqlFilter, pEmp, dt1, dt2, SystemVariables.DateFormat);


               
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


                grdTask.ItemsSource = dtblTemp;
                dbtbl.Dispose();
                dtblTemp.Dispose();
                GeneralFunctions.SetRecordCountStatusOfEmployee(dbtbl.Rows.Count);
            }

        }

        public void PopulateAttendanceFilter()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "This Week", "This Week" });
            dtbl.Rows.Add(new object[] { "This Month", "This Month" });
            dtbl.Rows.Add(new object[] { "Custom", "Custom" });
            dtFilter.ItemsSource = dtbl;
            dtFilter.EditValue = "This Week";
        }

        private async Task EditAttendance()
        {
            int intRowID = -1;
            int RecordID = 0;
            intRowID = gridView2.FocusedRowHandle;

            if ((grdTask.ItemsSource as DataTable).Rows.Count == 0) return;
            RecordID = await ReturnRowID1();
            blurGrid.Visibility = Visibility.Visible;
            frm_AttendanceInfoDlg frm_AttendanceInfoDlg = new frm_AttendanceInfoDlg();
            try
            {
                frm_AttendanceInfoDlg.ID = RecordID;
                
                if (frm_AttendanceInfoDlg.ID > 0)
                {
                    if (blFetchAttendanceUpdate) frm_AttendanceInfoDlg.ViewEdit = "Edit";
                    else frm_AttendanceInfoDlg.ViewEdit = "View";
                    frm_AttendanceInfoDlg.BrowseFormE = this;
                    frm_AttendanceInfoDlg.ShowDialog();
                }
            }
            finally
            {
                frm_AttendanceInfoDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            if (!blStopfiring)
                RefreshFetchStatus();

            if (dtSQLFDate >= dtSQLSDate)
            {
                FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
            }

            await SetCurrentRow1(RecordID);
        }

        private async void BtnAttnAdd_Click(object sender, RoutedEventArgs e)
        {
            int val = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_AttendanceInfoDlg frm_AttendanceInfoDlg = new frm_AttendanceInfoDlg();
            try
            {
                frm_AttendanceInfoDlg.ID = 0;
                frm_AttendanceInfoDlg.EMPID = intID;
                frm_AttendanceInfoDlg.BrowseFormE = this;
                frm_AttendanceInfoDlg.ShowDialog();
            }
            finally
            {
                val = frm_AttendanceInfoDlg.ID;
                frm_AttendanceInfoDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            if (!blStopfiring)
                RefreshFetchStatus();

            if (dtSQLFDate >= dtSQLSDate)
            {
                FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
            }

            if (val > 0) await SetCurrentRow1(val);
        }

        private void DtFilter_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (dtFilter.EditValue.ToString() == "Custom")
            {
                lbFrom.Visibility = lbTo.Visibility = dtFrom.Visibility = dtTo.Visibility = Visibility.Visible;
            }
            else
            {
                lbFrom.Visibility = lbTo.Visibility = dtFrom.Visibility = dtTo.Visibility = Visibility.Hidden;
            }

            RefreshFetchStatus();
            FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
        }

        private void DtFrom_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            RefreshFetchStatus();
            FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
        }

        private async void BtnAttnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditAttendance();
        }

        private async void BtnAttnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView2.FocusedRowHandle;
            if ((grdTask.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID1();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("shift"))
                {
                    PosDataObject.Attendance objNotes = new PosDataObject.Attendance();
                    objNotes.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objNotes.DeleteAttendanceRecord(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Deleting shift", strError);
                    }
                    else
                    {
                        if (dtSQLFDate >= dtSQLSDate)
                        {
                            FetchGridData(strSQLfilter, intSQLempID, dtSQLSDate, dtSQLFDate);
                        }

                        await SetCurrentRow1(intRecdID - 1);
                    }
                }
            }
        }

        private void GrdTask_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "SHIFTTIME")
            {
                int tm = GeneralFunctions.fnInt32(e.Value);
                if (tm < 60)
                {
                    e.DisplayText = tm.ToString() + " " + "min";
                }
                else
                {
                    int hr = tm / 60;
                    if (tm - hr * 60 == 0)
                    {
                        e.DisplayText = hr.ToString() + " " + "hr";
                    }
                    else
                    {
                        e.DisplayText = hr.ToString() + " " + "hr" + " " + (tm - hr * 60).ToString() + " " + "min";
                    }
                }
            }

            if (e.Column.FieldName == "WORKTIME")
            {
                if (e.Value.ToString() != "")
                {
                    int tm = GeneralFunctions.fnInt32(e.Value);
                    if (tm < 60)
                    {
                        e.DisplayText = tm.ToString() + " " + "min";
                    }
                    else
                    {
                        int hr = tm / 60;
                        if (tm - hr * 60 == 0)
                        {
                            e.DisplayText = hr.ToString() + " " + "hr";
                        }
                        else
                        {
                            e.DisplayText = hr.ToString() + " " + "hr" + " " + (tm - hr * 60).ToString() + " " + "min";
                        }
                    }
                }
                else
                {
                    e.DisplayText = "";
                }
            }
        }

        private async void GridView2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditAttendance();
        }

        private void GrdTask_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.FieldValue.ToString() == "WORKTIME")
            {
                if (e.TotalValue.ToString() != "")
                {
                    int tm = GeneralFunctions.fnInt32(e.TotalValue);
                    if (tm < 60)
                    {
                        e.TotalValue = tm.ToString() + " " + "min";
                    }
                    else
                    {
                        int hr = tm / 60;
                        if (tm - hr * 60 == 0)
                        {
                            e.TotalValue = hr.ToString() + " " + "hr";
                        }
                        else
                        {
                            e.TotalValue = hr.ToString() + " " + "hr" + " " + (tm - hr * 60).ToString() + " " + "min";
                        }
                    }
                }
                else
                {
                    e.TotalValue = "";
                }
            }
        }

        private void TxtEmpID_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkAppointment_Checked(object sender, RoutedEventArgs e)
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
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            if (!bIsCallForChangeExpiredPassword) BrowseForm.FetchData();
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

        private void CmbSecurity_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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

        private void GridView7_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                boolControlChanged = true;
                boolBookingExportFlagChanged = true;
            }
        }

        private void NumPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }



        private void NumCPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void DtFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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


        private void Full_GotFocusPswd(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).PointToScreen(new Point(0, 0));
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

        private void chkForcedPasscode_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void NumP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).PointToScreen(new Point(0, 0));
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
