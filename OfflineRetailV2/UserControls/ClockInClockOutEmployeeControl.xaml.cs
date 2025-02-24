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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for ClockInClockOutEmployeeControl.xaml
    /// </summary>
    public partial class ClockInClockOutEmployeeControl : UserControl
    {
        public CommandBase ClockInClockOutEmployeeControlClosed;
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

        private DateTime prevmodifiedon = Convert.ToDateTime(null);
        private bool bIsCallForChangeExpiredPassword = false;

        private bool boolBookingExportFlagChanged;
        private string prevBookingExpFlag = "N";
        public ClockInClockOutEmployeeControl()
        {
            InitializeComponent();
        }

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
        private DockPanel SelectedDocPanel = null;

        public async Task Load()
        {
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.PreviewMouseLeftButtonDown += BtnFrontOffice_PreviewMouseLeftButtonDown;
            //Title.Text = Properties.Resources.My_Desktop;

            pnlMenuHeader.Width = 140.0;
            imghide.Visibility = Visibility.Visible;
            imgshow.Visibility = Visibility.Collapsed;
            lbShowHideH.Text = "Hide";
            btnECHeader.SetValue(Canvas.LeftProperty, 70.0);
            HideAllGroupItems1();
            SelectedDocPanel = mnuItem;

            SetItemsPanelFromSelectedGroup(SelectedDocPanel);
        }

        private void BtnFrontOffice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
            (Window.GetWindow(this) as MainWindow).UpdateLayout();
            (Window.GetWindow(this) as MainWindow).GoToFrontOffice();
        }


        private void BtnECHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pnlMenuHeader.Width > 50.0)
            {
                pnlMenuHeader.Width = 50.0;
                imghide.Visibility = Visibility.Collapsed;
                imgshow.Visibility = Visibility.Visible;
                lbShowHideH.Text = "Show";
                btnECHeader.SetValue(Canvas.LeftProperty, -20.0);
            }
            else
            {
                pnlMenuHeader.Width = 140.0;
                imghide.Visibility = Visibility.Visible;
                imgshow.Visibility = Visibility.Collapsed;
                lbShowHideH.Text = "Hide";
                btnECHeader.SetValue(Canvas.LeftProperty, 70.0);
            }
        }

        private bool HideAllBrowseForm1(string VisibleControlName)
        {
            if (pnlBody != null)
            {
                foreach (UserControl uc in pnlBody.Children)
                {
                    if (uc.Name == VisibleControlName)
                    {
                        uc.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        uc.Visibility = Visibility.Collapsed;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private void NavGroupExit1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Do you want to Exit?") == MessageBoxResult.Yes)
            {
                SystemVariables.CurrentUserID = -1;
                SystemVariables.CurrentUserName = "";
                (Window.GetWindow(this) as MainWindow).LoggedInUserTextBlock.Text = "No Logged In User";
                this.Visibility = Visibility.Collapsed;
                (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
                (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
                (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
                (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Collapsed;
                //(Window.GetWindow(this) as MainWindow).LoginSection = LoginSection.None;
                //(Window.GetWindow(this) as MainWindow).LoginControl.txtUser.Text = "";
                //(Window.GetWindow(this) as MainWindow).LoginControl.txtPswd.PasswordBox.Password = "";
                (Window.GetWindow(this) as MainWindow).UpdateLayout();
            }
        }


        private void HideAllGroupItems1()
        {

            imgMenu_P.Source = this.FindResource("ItemMenu") as ImageSource;


        }

        private void SetItemsPanelFromSelectedGroup(DockPanel dp)
        {
            HideAllGroupItems1();

            foreach (UIElement uI in MenuPanel.Children)
            {
                //if ((uI as DockPanel).Tag.ToString() == "99") continue; // Do not consider Exit
                if ((uI as DockPanel).Name == dp.Name)
                {
                    (uI as DockPanel).Tag = "1";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));

                    if ((uI as DockPanel).Name == "mnuItem") imgMenu_P.Source = this.FindResource("ItemMenuS") as ImageSource;


                }
                else
                {
                    (uI as DockPanel).Tag = "0";
                    if (SystemVariables.SelectedTheme == "Dark") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
                    if (SystemVariables.SelectedTheme == "Light") (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));

                }
            }


            InitiateClickOnGroupChange(dp);

        }

        private void Grp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool boolProccedChanged = true;
            DockPanel dp = sender as DockPanel;
            foreach (UIElement uI in MenuPanel.Children)
            {
                if ((uI as DockPanel).Tag.ToString() == "99") continue; // Do not consider Exit
                if ((uI as DockPanel).Tag.ToString() == "1")
                {
                    if ((uI as DockPanel).Name == dp.Name)
                    {
                        boolProccedChanged = false;
                        break;
                    }
                }
            }

            if (boolProccedChanged)
            {
                SetItemsPanelFromSelectedGroup(dp);
            }
        }

        public void RedirectToSubmenu(string MenuGroup)
        {
            if (MenuGroup == "Menu")
            {
                HideAllBrowseForm("frm_SMEmpBrwUC");
                frm_SMEmpBrwUC.ParentForm = this;
                frm_SMEmpBrwUC.LoadSubmenu();
            }
        }

        public void ExecuteClickFromSubMenu(string clickitem)
        {
            if (clickitem == "Attendance")
            {
                if (!HideAllBrowseForm1("frm_AttendanceBrwUC")) return;
                frm_AttendanceBrwUC.ParentForm = this;
                frm_AttendanceBrwUC.InitializeForm();
                frm_AttendanceBrwUC.FetchGridData(frm_AttendanceBrwUC.SQLfilter, frm_AttendanceBrwUC.SQLempID, frm_AttendanceBrwUC.SQLSDate, frm_AttendanceBrwUC.SQLFDate);
                lbHeading.Text = "Attendance";
            }

            
        }



        private void InitiateClickOnGroupChange(DockPanel wp)
        {
            if (wp == mnuItem)
            {
                HideAllBrowseForm("frm_SMEmpBrwUC");
                frm_SMEmpBrwUC.ParentForm = this;
                frm_SMEmpBrwUC.LoadSubmenu();

               /* if (frm_AttendanceBrwUC.Visibility == Visibility.Collapsed)
                {
                    if (!HideAllBrowseForm1("frm_AttendanceBrwUC")) return;
                    frm_AttendanceBrwUC.ParentForm = this;
                    frm_AttendanceBrwUC.InitializeForm();
                    frm_AttendanceBrwUC.FetchGridData(frm_AttendanceBrwUC.SQLfilter, frm_AttendanceBrwUC.SQLempID, frm_AttendanceBrwUC.SQLSDate, frm_AttendanceBrwUC.SQLFDate);
                    lbHeading.Text = "Attendance";
                } */
            }




        }


        private void SetSeletedItem(WrapPanel wp, string itemname)
        {
            foreach (UIElement uI in wp.Children)
            {
                if ((uI as DockPanel).Name == itemname)
                {
                    (uI as DockPanel).Tag = "1";
                    (uI as DockPanel).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364A5D"));
                    foreach (UIElement ulsub in (uI as DockPanel).Children)
                    {
                        if (ulsub is Grid)
                        {
                            foreach (UIElement cr in (ulsub as Grid).Children)
                            {
                                if (cr is TextBlock)
                                {
                                    (cr as TextBlock).FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                                    (cr as TextBlock).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EC1DA"));
                                }

                                if (cr is Image)
                                {
                                    (cr as Image).Visibility = Visibility.Visible;
                                }
                            }
                        }

                    }

                }
            }
        }


        private void CustomItemClick(string clickitem)
        {
            if (clickitem == "nbAttn1")
            {
                if (!HideAllBrowseForm1("frm_AttendanceBrwUC")) return;
                frm_AttendanceBrwUC.InitializeForm();
                frm_AttendanceBrwUC.FetchGridData(frm_AttendanceBrwUC.SQLfilter, frm_AttendanceBrwUC.SQLempID, frm_AttendanceBrwUC.SQLSDate, frm_AttendanceBrwUC.SQLFDate);
                lbHeading.Text = "Attendance";
            }
        }

        private void HideAllBrowseForm(string VisibleControlName)
        {
            
            foreach (UserControl uc in pnlBody.Children)
            {
                if (uc.Name == VisibleControlName)
                {
                    uc.Visibility = Visibility.Visible;
                }
                else
                {
                    uc.Visibility = Visibility.Collapsed;
                }
            }
        }


       

        private void Nvview_ItemSelected(object sender, DevExpress.Xpf.NavBar.NavBarItemSelectedEventArgs e)
        {
            if (e.Item.Name == "nbAttn")
            {
                if (pnlBody == null) return;
                HideAllBrowseForm("frm_AttendanceBrwUC");
                frm_AttendanceBrwUC.InitializeForm();
                frm_AttendanceBrwUC.FetchGridData(frm_AttendanceBrwUC.SQLfilter, frm_AttendanceBrwUC.SQLempID, frm_AttendanceBrwUC.SQLSDate, frm_AttendanceBrwUC.SQLFDate);
                lbHeading.Text = Properties.Resources.Attendance;
            }
                
        }
    }
}
