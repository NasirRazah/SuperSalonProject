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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;

using Task = System.Threading.Tasks.Task;
using System.Windows.Threading;
using System.Diagnostics;
using DevExpress.XtraReports.UI;
using Microsoft.Win32.TaskScheduler;
using TaskScheduler;
using Microsoft.Win32.TaskScheduler;
using System.Text.RegularExpressions;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_DashboardDlg_SalesSnapshot.xaml
    /// </summary>
    public partial class frm_DashboardDlg_SalesSnapshot : UserControl
    {
        public frm_DashboardDlg_SalesSnapshot()
        {
            InitializeComponent();
        }

        public CommandBase frm_DashboardDlg_SalesSnapshotClosed { get; set; }

        // Auto Display Update Status 

        private void AutoUpdateInfo()
        {
            string backuptype = "";
            string backupstoragetype = "";
            if (Settings.BackupType == 0) backuptype = "Cloud";
            if (Settings.BackupType == 1) backuptype = "Local";
            if (Settings.BackupType == 2) backuptype = "Both";

            if (Settings.BackupStorageType == 0) backupstoragetype = "Generate Daily files";
            if (Settings.BackupStorageType == 1) backupstoragetype = "Overwrite single file";

            lbautobkuptype.Text = backuptype;
            lbautobkupstoragetype.Text = backupstoragetype;

            DataTable dtbl = new DataTable();
            PosDataObject.Closeout objclsout = new PosDataObject.Closeout();
            objclsout.Connection = SystemVariables.Conn;
            dtbl = objclsout.ShowAutoProcessRecord();
            if (dtbl.Rows.Count == 0)
            {
                //pnlautoupdt.Visibility = Visibility.Collapsed;
                lbupdate_1.Visibility = lbupdate_2.Visibility = lbupdate_3.Visibility = lbupdate_4.Visibility = lbupdate_5.Visibility = Visibility.Hidden;
                lbautobkup.Visibility = lbautopurge.Visibility = lbautoshrink.Visibility = lbautocloseout.Visibility = lbautoempcout.Visibility = Visibility.Hidden;
                pnlautoupdtdt.Visibility = Visibility.Collapsed;
                btncloseout.Visibility = Visibility.Collapsed;
                lbautomsg.Text = Properties.Resources.Dashboard_not_executed;
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    lbstartdt.Text = GeneralFunctions.fnDate(dr["StartTime"]).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
                    if ((dr["EndTime"].ToString() == null) || (dr["EndTime"].ToString() == ""))
                    {
                        End.Visibility = Visibility.Hidden; lbenddt.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lbenddt.Text = GeneralFunctions.fnDate(dr["EndTime"]).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
                    }

                    lbupdate_1.Visibility = lbupdate_2.Visibility = lbupdate_3.Visibility = lbupdate_4.Visibility = lbupdate_5.Visibility = Visibility.Visible;
                    lbautobkup.Visibility = lbautopurge.Visibility = lbautoshrink.Visibility = lbautocloseout.Visibility = lbautoempcout.Visibility = Visibility.Visible;

                    if (Settings.BackupType == 0)
                    {
                        if (dr["CloudBackupFlag"].ToString() == "N")
                        {
                            lbautobkup.Text = Properties.Resources.No;
                        }
                        if (dr["CloudBackupFlag"].ToString() == "Y")
                        {
                            lbautobkup.Text = Properties.Resources.Yes;
                        }
                    }

                    if (Settings.BackupType == 1)
                    {
                        if (dr["BackupFlag"].ToString() == "N")
                        {
                            lbautobkup.Text = Properties.Resources.No;
                        }
                        if (dr["BackupFlag"].ToString() == "Y")
                        {
                            lbautobkup.Text = Properties.Resources.Yes;
                        }
                    }

                    if (Settings.BackupType == 2)
                    {
                        
                        if ((dr["BackupFlag"].ToString() == "Y") && (dr["CloudBackupFlag"].ToString() == "Y"))
                        {
                            lbautobkup.Text = Properties.Resources.Yes;
                        }
                        else
                        {
                            lbautobkup.Text = Properties.Resources.No;
                        }
                    }

                    if (dr["DataPurgingFlag"].ToString() == "N")
                    {
                        lbautopurge.Text = Properties.Resources.No;
                    }
                    if (dr["DataPurgingFlag"].ToString() == "Y")
                    {
                        lbautopurge.Text = Properties.Resources.Yes;
                    }

                    if (dr["ShrinkFlag"].ToString() == "N")
                    {
                        lbautoshrink.Text = Properties.Resources.No;
                    }
                    if (dr["ShrinkFlag"].ToString() == "Y")
                    {
                        lbautoshrink.Text = Properties.Resources.Yes;
                    }

                    if (dr["CloseoutFlag"].ToString() == "N")
                    {
                        lbautocloseout.Text = Properties.Resources.No;
                    }
                    if (dr["CloseoutFlag"].ToString() == "Y")
                    {
                        lbautocloseout.Text = Properties.Resources.Yes;
                    }

                    if (dr["EmpClockoutFlag"].ToString() == "N")
                    {
                        lbautoempcout.Text = Properties.Resources.No;
                    }
                    if (dr["EmpClockoutFlag"].ToString() == "Y")
                    {
                        lbautoempcout.Text = Properties.Resources.Yes;
                    }
                    lbautomsg.Text = dr["StatusMessage"].ToString();
                    if (lbautobkup.Text == Properties.Resources.Yes)
                    {
                        lbautomsg.Text = "Database Backup completed successfully";
                    } 
                    else
                    {
                        lbautomsg.Text = "Database Backup completed failed";
                    }

                    /*if (lbautomsg.Text == "Auto update was completed successfully")
                    {
                        lbautomsg.Text = Properties.Resources.Dashboard_run_successfully;
                    }*/
                }
                int cid = 0;
                PosDataObject.Closeout objclsout1 = new PosDataObject.Closeout();
                objclsout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                cid = objclsout1.GetLastConsolidatedCloseoutID();
                if (cid == 0)
                {
                    btncloseout.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btncloseout.Visibility = Visibility.Visible;
                    btncloseout.Tag = cid.ToString();
                }
            }
        }

        public async Task Load()
        {
            //lbTitle.Text = Properties.Resources.Dashboard;
            try
            {
                Cursor = Cursors.Wait;




                /* Previous Code
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));

                    if (t1 != null)
                    {
                        foreach (DailyTrigger dt1 in t1.Triggers)
                        {
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                        }

                        btnRunScheduler.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);
                    }
                }
                catch
                {
                }*/

                txtTime.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);
                if (Settings.OSVersion == "Win 10")
                {
                    try
                    {
                        ScheduledTasks st1 = new ScheduledTasks();
                        TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));

                        foreach (TaskScheduler.DailyTrigger dt1 in t1.Triggers)
                        {
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                        }

                        btnAutoSchedulr.Tag = "Modify Scheduler";
                        btnRunScheduler.Visibility = Visibility.Visible;
                    }
                    catch
                    {
                        btnAutoSchedulr.Tag = "Add Scheduler";
                    }
                }

                if (Settings.OSVersion == "Win 11")
                {
                    try
                    {
                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));
                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                            btnAutoSchedulr.Tag = "Modify Scheduler";
                            btnRunScheduler.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            btnAutoSchedulr.Tag = "Add Scheduler";
                            btnRunScheduler.Visibility = Visibility.Hidden;
                        }

                    }
                    catch
                    {
                        btnAutoSchedulr.Tag = "Add Scheduler";
                        btnRunScheduler.Visibility = Visibility.Hidden;
                    }
                }

                PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCloseoutM.ExecuteDashBoard(Settings.TerminalName, Settings.TaxInclusive);

                DataTable dtbl4 = new DataTable("Header");
                PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
                objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl4 = objCloseout3.ShowDashBoardHeaderRecord(Settings.TerminalName);

                foreach (DataRow dr in dtbl4.Rows)
                {
                    val5.Text = dr["HoursWorked"].ToString();

                    val1.Text = dr["Sales"].ToString();
                    val3.Text = dr["Voids"].ToString();
                    val2.Text = dr["Discounts"].ToString();
                }

                DataTable dtbl5 = new DataTable("SH");
                PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl5 = objCloseout4.ShowDashBoardSalesByHourRecord(Settings.TerminalName);


                DataTable dtbl6 = new DataTable("SD");
                PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl6 = objCloseout5.ShowDashBoardEmpClockInRecord(Settings.TerminalName);


                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtbl5.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }

                DataTable dtblTemp1 = dtbl6.DefaultView.ToTable();
                DataColumn column1 = new DataColumn("Image");
                column1.DataType = System.Type.GetType("System.Byte[]");
                column1.AllowDBNull = true;
                column1.Caption = "Image";
                dtblTemp1.Columns.Add(column1);

                foreach (DataRow dr in dtblTemp1.Rows)
                {
                    dr["Image"] = strip;
                }

                grdSH.ItemsSource = dtblTemp;
                grdEMP.ItemsSource = dtblTemp1;

                dtbl4.Dispose();
                dtbl5.Dispose();
                dtbl6.Dispose();
                dtblTemp.Dispose();
                dtblTemp1.Dispose();

                AutoUpdateInfo();
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        

        private void BtnAutoSchedulr_Click(object sender, RoutedEventArgs e)
        {
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory; // System.Reflection.Assembly.GetExecutingAssembly().Location;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }

            /*ScheduledTasks st = new ScheduledTasks();
            st.DeleteTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));
            try
            {
                ScheduledTasks st1 = new ScheduledTasks();

                TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));

                t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                t.ApplicationName = csConnPath;
                t.Parameters = "  D";
                t.SetAccountInformation(Environment.UserName, (String)null);
                DailyTrigger dt = new DailyTrigger((short)txtTime.DateTime.Hour, (short)txtTime.DateTime.Minute, 1);
                t.Triggers.Add(dt);

                t.Save();
                t.Close();
                DocMessage.MsgInformation("Scheduler successfully set");
                btnRunScheduler.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                DocMessage.MsgInformation("Error while scheduling ...");
            }*/


            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  D";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtTime.DateTime.Hour, (short)txtTime.DateTime.Minute, 1);
                    t.Triggers.Add(dt);

                    t.Save();
                    t.Close();

                    if (btnAutoSchedulr.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnAutoSchedulr.Tag = "Modify Scheduler";
                        btnRunScheduler.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated");
                    }

                }
                catch (Exception ex)
                {
                    btnAutoSchedulr.Tag = "Add Scheduler";
                    btnRunScheduler.Visibility = Visibility.Hidden;
                    DocMessage.MsgInformation("Error while scheduling tasks...");
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtTime.DateTime.Minute);
                    daily.DaysInterval = 1;

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  D", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    if (btnAutoSchedulr.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnAutoSchedulr.Tag = "Modify Scheduler";
                        btnRunScheduler.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated");
                    }
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling tasks...");
                    btnAutoSchedulr.Tag = "Add Scheduler";
                    btnRunScheduler.Visibility = Visibility.Hidden;
                }
            }

        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;

                //pnlAuto.Visible = SystemVariables.ConnectionString.ToLower().Contains(Settings.TerminalName.ToLower());

                /*try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));

                    foreach (DailyTrigger dt1 in t1.Triggers)
                    {
                        txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                    }
                }
                catch
                {
                }*/

                if (Settings.OSVersion == "Win 10")
                {
                    try
                    {
                        ScheduledTasks st1 = new ScheduledTasks();
                        TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));

                        foreach (TaskScheduler.DailyTrigger dt1 in t1.Triggers)
                        {
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                        }

                        btnRunScheduler.Visibility = Visibility.Visible;
                    }
                    catch
                    {

                    }
                }

                if (Settings.OSVersion == "Win 11")
                {
                    try
                    {
                        Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                        Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Dashboard".Replace("XEPOS", SystemVariables.BrandName));
                        if (t != null)
                        {
                            Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                            txtTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                            btnRunScheduler.Visibility = Visibility.Visible;
                        }


                    }
                    catch
                    {

                    }
                }

                PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCloseoutM.ExecuteDashBoard(Settings.TerminalName, Settings.TaxInclusive);

                DataTable dtbl4 = new DataTable("Header");
                PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
                objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl4 = objCloseout3.ShowDashBoardHeaderRecord(Settings.TerminalName);

                foreach (DataRow dr in dtbl4.Rows)
                {
                    val5.Text = dr["HoursWorked"].ToString();

                    val1.Text = dr["Sales"].ToString();
                    val3.Text = dr["Voids"].ToString();
                    val2.Text = dr["Discounts"].ToString();
                }

                DataTable dtbl5 = new DataTable("SH");
                PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl5 = objCloseout4.ShowDashBoardSalesByHourRecord(Settings.TerminalName);


                DataTable dtbl6 = new DataTable("SD");
                PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl6 = objCloseout5.ShowDashBoardEmpClockInRecord(Settings.TerminalName);

                grdSH.ItemsSource = dtbl5;
                grdEMP.ItemsSource = dtbl6;

                dtbl4.Dispose();
                dtbl5.Dispose();
                dtbl6.Dispose();

                AutoUpdateInfo();
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            SystemVariables.CurrentUserID = -1;
            SystemVariables.CurrentUserName = "";
            (Window.GetWindow(this) as MainWindow).LoggedInUserTextBlock.Text = Properties.Resources.NoLoggedInUser;
            this.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
            //(Window.GetWindow(this) as MainWindow).LoginSection = LoginSection.None;
            //(Window.GetWindow(this) as MainWindow).LoginControl.txtUser.Text = "";
            //(Window.GetWindow(this) as MainWindow).LoginControl.txtPswd.PasswordBox.Password = "";
            (Window.GetWindow(this) as MainWindow).UpdateLayout();
        }

    

        private void BtnRunScheduler_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "AutoExe_Retail.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\AutoExe_Retail.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"DM";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteDashboardReport()
        {

            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            int exe = objSales.ExecDashboardReport(Settings.TerminalName, Settings.TaxInclusive);

            DataTable dtbl = objSales.FetchDashboardReportData(Settings.TerminalName);

            DataTable dtblSH = new DataTable();

            dtblSH.Columns.Add("Hour", System.Type.GetType("System.String"));
            dtblSH.Columns.Add("Invoice", System.Type.GetType("System.Int32"));
            dtblSH.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSH.Columns.Add("Avg", System.Type.GetType("System.Double"));

            DataRow[] DR1 = dtbl.Select("RecordType = 'BY HOUR' ");

            foreach (DataRow dr in DR1)
            {
                if (GeneralFunctions.fnInt32(dr["RecordCount"].ToString()) > 0)
                {
                    dtblSH.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnInt32(dr["RecordCount"].ToString()), GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())),
                    GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())/GeneralFunctions.fnInt32(dr["RecordCount"].ToString()))});
                }
            }

            DataTable dtblSC = new DataTable();

            dtblSC.Columns.Add("Category", System.Type.GetType("System.String"));
            dtblSC.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSC.Columns.Add("Percent", System.Type.GetType("System.Double"));
            dtblSC.Columns.Add("Color", System.Type.GetType("System.String"));

            DataRow[] DR2 = dtbl.Select("RecordType = 'BY CATEGORY' ");

            double TotalCatAmount = 0;
            foreach (DataRow dr in DR2)
            {
                dtblSC.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), 0, "" });
                TotalCatAmount = TotalCatAmount + GeneralFunctions.fnDouble(dr["RecordAmount"].ToString());
            }



            dtblSC.DefaultView.Sort = "Category asc";
            dtblSC.DefaultView.ApplyDefaultSort = true;

            DataTable dtblSD = new DataTable();

            dtblSD.Columns.Add("Department", System.Type.GetType("System.String"));
            dtblSD.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSD.Columns.Add("Percent", System.Type.GetType("System.Double"));
            DataRow[] DR10 = dtbl.Select("RecordType = 'BY DEPARTMENT' ");

            double TotalDeptAmount = 0;
            foreach (DataRow dr in DR10)
            {
                dtblSD.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), 0 });
                TotalDeptAmount = TotalDeptAmount + GeneralFunctions.fnDouble(dr["RecordAmount"].ToString());
            }



            dtblSD.DefaultView.Sort = "Department asc";
            dtblSD.DefaultView.ApplyDefaultSort = true;

            DataTable dtblT = new DataTable();

            dtblT.Columns.Add("TenderType", System.Type.GetType("System.String"));
            dtblT.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblT.Columns.Add("Color", System.Type.GetType("System.String"));

            DataRow[] DR3 = dtbl.Select("RecordType = 'TENDER' ");

            foreach (DataRow dr in DR3)
            {
                dtblT.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), "" });
            }

            DataTable dtblGC = new DataTable();
            dtblGC.Columns.Add("Qty", System.Type.GetType("System.Int32"));
            dtblGC.Columns.Add("Total", System.Type.GetType("System.Double"));
            dtblGC.Columns.Add("Avg", System.Type.GetType("System.Double"));


            DataRow[] DR4 = dtbl.Select("RecordType = 'GC' ");

            foreach (DataRow dr in DR4)
            {
                dtblGC.Rows.Add(new object[] { GeneralFunctions.fnInt32(dr["RecordCount"].ToString()), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()),
                GeneralFunctions.fnInt32(dr["RecordCount"].ToString()) == 0 ? 0 : GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()) / GeneralFunctions.fnInt32(dr["RecordCount"].ToString()))});
            }


            DataTable dtblTx = new DataTable();
            dtblTx.Columns.Add("Tax", System.Type.GetType("System.String"));
            dtblTx.Columns.Add("Taxable", System.Type.GetType("System.Double"));
            dtblTx.Columns.Add("Collected", System.Type.GetType("System.Double"));

            DataRow[] DR5 = dtbl.Select("RecordType = 'TAX' ");
            DataRow[] DR6 = dtbl.Select("RecordType = 'TAXABLE' ");

            foreach (DataRow dr in DR5)
            {
                foreach (DataRow dr1 in DR6)
                {
                    if (GeneralFunctions.fnInt32(dr["RecordSL"].ToString()) == GeneralFunctions.fnInt32(dr1["RecordSL"].ToString()))
                    {
                        dtblTx.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr1["RecordAmount"].ToString()),
                        GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())});
                        break;
                    }
                }
            }

            OfflineRetailV2.Report.Sales.repDashboard repM = new OfflineRetailV2.Report.Sales.repDashboard();
            GeneralFunctions.MakeReportWatermark(repM);
            OfflineRetailV2.Report.Sales.repDashboard_SH rep_SHS = new OfflineRetailV2.Report.Sales.repDashboard_SH();
            OfflineRetailV2.Report.Sales.repDashboard_SC rep_SCS = new OfflineRetailV2.Report.Sales.repDashboard_SC();
            OfflineRetailV2.Report.Sales.repDashboard_SD rep_SDS = new OfflineRetailV2.Report.Sales.repDashboard_SD();
            OfflineRetailV2.Report.Sales.repDashboard_T rep_T = new OfflineRetailV2.Report.Sales.repDashboard_T();
            OfflineRetailV2.Report.Sales.repDashboard_GC rep_GC = new OfflineRetailV2.Report.Sales.repDashboard_GC();
            OfflineRetailV2.Report.Sales.repDashboard_TX rep_TX = new OfflineRetailV2.Report.Sales.repDashboard_TX();
            repM.srepSC.ReportSource = rep_SCS;
            repM.srepSD.ReportSource = rep_SDS;
            repM.srepSH.ReportSource = rep_SHS;
            repM.srepT.ReportSource = rep_T;
            repM.srepGC.ReportSource = rep_GC;
            repM.srepTX.ReportSource = rep_TX;

            GeneralFunctions.MakeReportWatermark(repM);

            repM.rDate.Text = "Today" + ": " + DateTime.Now.Date.ToShortDateString();
            repM.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            repM.rReportHeader.Text = Settings.ReportHeader_Address;


            foreach (DataRow dr in dtblSH.Rows)
            {
                rep_SHS.xrChart1.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Hour"].ToString(), new double[] { 0, GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            foreach (DataRow dr in dtblSH.Rows)
            {
                rep_SHS.xrChart1.Series[1].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Hour"].ToString(), new double[] { 0, GeneralFunctions.fnDouble(dr["Invoice"].ToString()) }));
            }

            rep_SHS.xrChart1.Series[0].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SHS.xrChart1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

            rep_SHS.xrChart1.Series[1].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SHS.xrChart1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

            string resource1 = "Hourly Sales";
            string resource2 = "No. of Invoices";

            rep_SHS.xrChart1.Series[0].Name = "Hourly Sales";
            rep_SHS.xrChart1.Series[1].Name = "No. of Invoices";


            string resource5 = "Hour";

            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Hour";
            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Sales";


            rep_SHS.xrChart1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            rep_SHS.xrChart1.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.GanttDiagram).AxisY.Label.NumericOptions.Precision = 2;
            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.GanttDiagram).AxisY.Label.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;

            rep_SHS.Report.DataSource = dtblSH;
            rep_SHS.rHour.DataBindings.Add("Text", dtblSH, "Hour");
            rep_SHS.rSales.DataBindings.Add("Text", dtblSH, "Amount");
            rep_SHS.rInv.DataBindings.Add("Text", dtblSH, "Invoice");
            rep_SHS.rAvg.DataBindings.Add("Text", dtblSH, "Avg");
            rep_SHS.rTSales.DataBindings.Add("Text", dtblSH, "Amount");
            rep_SHS.rTInv.DataBindings.Add("Text", dtblSH, "Invoice");


            foreach (DataRow dr in dtblSD.Rows)
            {
                dr["Percent"] = GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString()) * 100 / TotalCatAmount).ToString());
            }

            Double TPercent1 = 0;
            foreach (DataRow dr in dtblSD.Rows)
            {
                TPercent1 = TPercent1 + GeneralFunctions.fnDouble(dr["Percent"].ToString());
            }

            double var1 = 100 - TPercent1;
            if (var1 != 0)
            {
                foreach (DataRow dr in dtblSD.Rows)
                {
                    dr["Percent"] = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Percent"].ToString()) + var1);
                    break;
                }
            }


            rep_SDS.xrChart1.Series.Clear();
            rep_SDS.xrChart1.Series.Add("", DevExpress.XtraCharts.ViewType.Bar);
            foreach (DataRow dr in dtblSD.Rows)
            {
                rep_SDS.xrChart1.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Department"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));

            }
            ((DevExpress.XtraCharts.SideBySideBarSeriesView)rep_SDS.xrChart1.Series[0].View).ColorEach = true;

            rep_SDS.xrChart1.Series[0].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SDS.xrChart1.Series[0].Label.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;
            rep_SDS.xrChart1.Series[0].Label.PointOptions.ValueNumericOptions.Precision = 2;
            rep_SDS.xrChart1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            rep_SDS.xrChart1.Series[0].Label.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.Default;
            rep_SDS.xrChart1.Series[0].ShowInLegend = false;

            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Label.NumericOptions.Precision = 2;
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Label.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;

            string resource3 = "Sales";
            string resource4 = "Department";

            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Sales";
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Department";
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Visible = true;
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Visible = true;

            foreach (DataRow dr in dtblSC.Rows)
            {
                rep_SCS.xrChart2.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Category"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            DevExpress.XtraCharts.PaletteRepository pr = rep_SCS.xrChart2.PaletteRepository;

            DevExpress.XtraCharts.Palette p = pr["MyPalette"];

            int colorcount = p.Count;
            int i = 0;
            foreach (DataRow dr in dtblSC.Rows)
            {
                if (i == colorcount)
                {
                    i = 0;
                }
                DevExpress.XtraCharts.PaletteEntry pe = p[i];
                dr["Color"] = pe.Color.ToArgb();
                i++;
            }

            foreach (DataRow dr in dtblSC.Rows)
            {
                dr["Percent"] = GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString()) * 100 / TotalCatAmount).ToString());
            }

            Double TPercent = 0;
            foreach (DataRow dr in dtblSC.Rows)
            {
                TPercent = TPercent + GeneralFunctions.fnDouble(dr["Percent"].ToString());
            }

            double var = 100 - TPercent;
            if (var != 0)
            {
                foreach (DataRow dr in dtblSC.Rows)
                {
                    dr["Percent"] = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Percent"].ToString()) + var);
                    break;
                }
            }
            rep_SCS.Report.DataSource = dtblSC;
            rep_SCS.xrLabel1.DataBindings.Add("Text", dtblSC, "Color");
            rep_SCS.xrLabel3.DataBindings.Add("Text", dtblSC, "Category");
            rep_SCS.xrLabel4.DataBindings.Add("Text", dtblSC, "Amount");
            rep_SCS.xrLabel6.DataBindings.Add("Text", dtblSC, "Percent");
            rep_SCS.rTAmount.DataBindings.Add("Text", dtblSC, "Amount");
            //rep_SCS.rTPerc.DataBindings.Add("Text", dtblSC, "Percent");


            foreach (DataRow dr in dtblT.Rows)
            {
                rep_T.xrChart2.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["TenderType"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            DevExpress.XtraCharts.PaletteRepository pr1 = rep_T.xrChart2.PaletteRepository;

            DevExpress.XtraCharts.Palette p1 = pr1["MyPalette"];

            int colorcount1 = p1.Count;
            int i1 = 0;
            foreach (DataRow dr in dtblT.Rows)
            {
                if (i1 == colorcount1)
                {
                    i1 = 0;
                }
                DevExpress.XtraCharts.PaletteEntry pe1 = p1[i1];
                dr["Color"] = pe1.Color.ToArgb();
                i1++;
            }
            rep_T.Report.DataSource = dtblT;
            rep_T.xrLabel1.DataBindings.Add("Text", dtblT, "Color");
            rep_T.xrLabel3.DataBindings.Add("Text", dtblT, "TenderType");
            rep_T.xrLabel4.DataBindings.Add("Text", dtblT, "Amount");
            rep_T.rTAmount.DataBindings.Add("Text", dtblT, "Amount");

            rep_GC.Report.DataSource = dtblGC;
            rep_GC.rQty.DataBindings.Add("Text", dtblGC, "Qty");
            rep_GC.rTotal.DataBindings.Add("Text", dtblGC, "Total");
            rep_GC.rAvg.DataBindings.Add("Text", dtblGC, "Avg");

            rep_TX.Report.DataSource = dtblTx;
            rep_TX.rTaxName.DataBindings.Add("Text", dtblTx, "Tax");
            rep_TX.rTaxable.DataBindings.Add("Text", dtblTx, "Taxable");
            rep_TX.rTax.DataBindings.Add("Text", dtblTx, "Collected");
            rep_TX.rTTax.DataBindings.Add("Text", dtblTx, "Collected");



            if (dtblSH.Rows.Count == 0)
            {
                rep_SHS.xrChart1.Visible = false;
                rep_SHS.xrTable1.Visible = false;
                rep_SHS.Detail.Visible = false;
                rep_SHS.ReportFooter.Visible = false;
                rep_SHS.lbNoData.BringToFront();
                rep_SHS.xrChart1.SendToBack();
            }

            if (dtblSD.Rows.Count == 0)
            {
                rep_SDS.xrChart1.Visible = false;
                rep_SDS.lbNoData.BringToFront();
                rep_SDS.xrChart1.SendToBack();
            }

            if (dtblSC.Rows.Count == 0)
            {
                rep_SCS.xrChart2.Visible = false;
                rep_SCS.Detail.Visible = false;
                rep_SCS.ReportFooter.Visible = false;
                rep_SCS.lbNoData.BringToFront();
                rep_SCS.xrChart2.SendToBack();
            }

            if (dtblT.Rows.Count == 0)
            {
                rep_T.xrChart2.Visible = false;
                rep_T.Detail.Visible = false;
                rep_T.ReportFooter.Visible = false;

                rep_T.lbNoData.BringToFront();
                rep_T.xrChart2.SendToBack();
            }

            if (dtblTx.Rows.Count == 0)
            {
                rep_TX.xrTable1.Visible = false;
                rep_TX.Detail.Visible = false;
                rep_TX.xrLine1.Visible = false;
                rep_TX.xrTable3.Visible = false;
                rep_TX.lbNoData.BringToFront();
                rep_TX.xrLine1.SendToBack();
                rep_TX.xrTable3.SendToBack();
            }
            else
            {
                rep_TX.lbNoData.SendToBack();
                rep_TX.lbNoData.Visible = false;
                rep_TX.xrLine1.Visible = true;
                rep_TX.xrTable3.Visible = true;
            }

            int HrCount = dtblSH.Rows.Count;

            if (HrCount > 4)
            {
                HrCount = HrCount - 4;
                rep_SHS.ReportHeader.HeightF = rep_SHS.ReportHeader.HeightF + HrCount * 20;
                rep_SHS.xrChart1.HeightF = rep_SHS.xrChart1.HeightF + HrCount * 20;
                rep_SHS.xrTable1.TopF = rep_SHS.xrTable1.TopF + HrCount * 19;
            }

            repM.CreateDocument();
            repM.ShowPreview();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Cursor = Cursors.Wait;
                ExecuteDashboardReport();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Btncloseout_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                ExecuteReport(GeneralFunctions.fnInt32(btncloseout.Tag.ToString()));
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteReport(int intCloseoutID)
        {

            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutReportProc("C", intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "N"); //Settings.CentralExportImport == "Y" ? "Y" : "N"

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
            dtbl1 = objCloseout.ShowTenderRecord("T", intCloseoutID, Settings.TerminalName);

            DataTable dtbl2 = new DataTable("TenderC");
            PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
            objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl2 = objCloseout1.ShowTenderRecord("C", intCloseoutID, Settings.TerminalName);

            DataTable dtbl3 = new DataTable("TenderOverShort");
            PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
            objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl3 = objCloseout2.ShowTenderRecord("R", intCloseoutID, Settings.TerminalName);

            DataTable dtbl4 = new DataTable("Header");
            PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
            objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl4 = objCloseout3.ShowHeaderRecord(Settings.TerminalName, SystemVariables.DateFormat);

            int rnt = 0;
            int rpr = 0;
            double rprsecurity = 0;
            foreach (DataRow d in dtbl4.Rows)
            {
                rnt = GeneralFunctions.fnInt32(d["RentInvoiceCount"].ToString());
                rpr = GeneralFunctions.fnInt32(d["RepairInvoiceCount"].ToString());
                rprsecurity = GeneralFunctions.fnDouble(d["RepairDeposit"].ToString());
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
            rep_CloseOut.rType.Text = Properties.Resources.Consolidated;

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
            rep_CloseoutMain.COType = "C";

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

            if ((rpr > 0) || (rprsecurity > 0))
            {
                rep_CloseOut.xrSubreport6.ReportSource = rep_CloseoutRepair;
                rep_CloseoutRepair.DataSource = ds4;
                rep_CloseoutRepair.DecimalPlace = Settings.DecimalPlace;
            }

            if ((rnt == 0) && (rpr == 0) && (rprsecurity == 0))
            {
                rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
            }

            if ((rnt == 0) && ((rpr > 0) || (rprsecurity > 0)))
            {
                rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;
                rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
            }

            if ((rnt > 0) && ((rpr == 0) && (rprsecurity == 0)))
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
            rep_SalesByHour.COType = "C";
            rep_SalesByHour.DataSource = ds5;
            rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
            rep_SalesByDept.COType = "C";
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
                rep_TenderOverShort.Dispose();
                rep_TenderCount.Dispose();
                rep_CloseOut.Dispose();

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

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
