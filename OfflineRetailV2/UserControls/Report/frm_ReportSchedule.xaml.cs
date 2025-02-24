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

using System.IO;
using System.Diagnostics;
using Microsoft.Win32.TaskScheduler;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ReportSchedule.xaml
    /// </summary>
    public partial class frm_ReportSchedule : Window
    {
        public frm_ReportSchedule()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private bool blChangeScheduleTime = false;
        private bool blLoading = false;

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        public async System.Threading.Tasks.Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grd.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grd, colID)));
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
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grd, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grd, colID)));
            return intRecID;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Setup obj = new PosDataObject.Setup();
            obj.Connection = SystemVariables.Conn;
            dbtbl = obj.FetchReportEntries();

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



            grd.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();



        }

        private async void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            string reptype = (sender as DevExpress.Xpf.Bars.BarButtonItem).Tag.ToString();
            int intNewRecID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_ReportSchdEntry fEntry = new frm_ReportSchdEntry();
            try
            {
                fEntry.ID = 0;
                fEntry.BrowseF = this;
                fEntry.ReportTag = reptype;
                fEntry.ShowDialog();
                intNewRecID = fEntry.ID;
            }
            finally
            {
                fEntry.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                ScheduledTasks st1 = new ScheduledTasks();
                TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));

                foreach (DailyTrigger dt1 in t1.Triggers)
                {
                    txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                }
                btnReportSchd.Content = "Modify Scheduler".ToUpper();
                btnRunReportSchd.Visibility = Visibility.Visible;
                
            }
            catch
            {
                txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, 0);
                btnReportSchd.Content = "Set Scheduler".ToUpper(); 
            }*/


            try
            {
                Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
                if (t != null)
                {
                    DailyTrigger dt = t.Definition.Triggers[0] as DailyTrigger;
                    txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                    btnReportSchd.Content = "Modify Scheduler".ToUpper();
                    btnRunReportSchd.Visibility = Visibility.Visible;
                }
                else
                {
                    txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, 0);
                    btnReportSchd.Content = "Set Scheduler".ToUpper();
                }

            }
            catch
            {
                txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, 0);
                btnReportSchd.Content = "Set Scheduler".ToUpper();
            }


            FetchData();
            blLoading = true;
        }

        private async System.Threading.Tasks.Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_ReportSchdEntry fEntry = new frm_ReportSchdEntry();
            try
            {
                fEntry.ID = await ReturnRowID();
                fEntry.BrowseF = this;
                fEntry.ReportTag = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,  grd, colTag);
                fEntry.ShowDialog();
                intNewRecID = fEntry.ID;
            }
            finally
            {
                fEntry.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BarButtonItem3_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BarButtonItem17_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            string reportpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            reportpath = directory.Parent.FullName;

            if (reportpath.EndsWith("\\")) reportpath = reportpath + SystemVariables.BrandName + "\\Report\\";
            else reportpath = reportpath + "\\" + SystemVariables.BrandName + "\\Report\\";

            string FilePath = reportpath + await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grd, colTitle);

            if (File.Exists(FilePath))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = FilePath;
                process.Start();
            }
            else
            {
                DocMessage.MsgInformation("Data not available for this Report.");
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grd.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Report Schedule"))
                {
                    PosDataObject.Setup objClass = new PosDataObject.Setup();
                    objClass.Connection = SystemVariables.Conn;
                    string strError = objClass.DeleteReportEntryRecord(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Deleting Report Schedule", strError);
                    }
                    FetchData();
                    if ((grd.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private void BtnReportSchd_Click(object sender, RoutedEventArgs e)
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

            /*
             *
            ScheduledTasks st = new ScheduledTasks();
            st.DeleteTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
            try
            {
                ScheduledTasks st1 = new ScheduledTasks();

                TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));

                t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                t.ApplicationName = csConnPath;
                t.Parameters = "  R";
                t.SetAccountInformation(Environment.UserName, (String)null);
                DailyTrigger dt = new DailyTrigger((short)txtReportTime.DateTime.Hour, (short)txtReportTime.DateTime.Minute, 1);
                t.Triggers.Add(dt);

                t.Save();
                t.Close();
                if (btnReportSchd.Content.ToString() == "SET SCHEDULER")
                {
                    DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                    btnReportSchd.Content = "Modify Scheduler".ToUpper();
                    btnRunReportSchd.Visibility = Visibility.Visible;
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                }
            }
            catch (Exception ex)
            {
                DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
            }

            */


            try

            {

                Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                string TaskName = SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName);
                bool boolFindTask = false;
                boolFindTask = ts.GetTask(@"" + TaskName) != null;
                if (boolFindTask)
                {
                    ts.RootFolder.DeleteTask(@"" + TaskName);
                }

                Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = TaskName;

                Microsoft.Win32.TaskScheduler.DailyTrigger daily = new DailyTrigger();

                daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtReportTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtReportTime.DateTime.Minute);
                daily.DaysInterval = 1;

                td.Triggers.Add(daily);

                td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  R", null));

                ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                if (btnReportSchd.Content.ToString() == "SET SCHEDULER")
                {
                    DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_created);
                    btnReportSchd.Content = "Modify Scheduler".ToUpper();
                    btnRunReportSchd.Visibility = Visibility.Visible;
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources.Task_scheduler_successfully_updated);
                }
            }
            catch
            {
                DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
               
            }
        }

        private void BtnRunReportSchd_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                ScheduledTasks st1 = new ScheduledTasks();
                TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
                if (t1 != null)
                {
                    Dispatcher.BeginInvoke(new Action(() => t1.Run()));
                }
            }
            catch
            {
            }*/

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(100);
            string schdName = SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName);
            try
            {
                Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();
                bool boolFindTask = false;
                boolFindTask = ts.GetTask(@"" + schdName) != null;
                if (boolFindTask) ts.GetTask(@"" + schdName).Run();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnOldRepView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string reportpath = "";

                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                var directory = new DirectoryInfo(documentsPath);
                reportpath = directory.Parent.FullName;


                if (reportpath.EndsWith("\\")) reportpath = reportpath + SystemVariables.BrandName + "\\Report\\Older Reports\\";
                else reportpath = reportpath + "\\" + SystemVariables.BrandName + "\\Report\\Older Reports\\";
                if (Directory.Exists(reportpath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe", reportpath);
                    Process.Start(startInfo);
                }
                else
                {
                    DocMessage.MsgInformation("Older reports do not exist");
                }
            }
            catch
            {
            }
        }

        private void BtnOldRepClear_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Do you want to clear all older reports?") == MessageBoxResult.Yes)
            {
                try
                {
                    string reportpath = "";

                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    var directory = new DirectoryInfo(documentsPath);
                    reportpath = directory.Parent.FullName;

                    if (reportpath.EndsWith("\\")) reportpath = reportpath + SystemVariables.BrandName + "\\Report\\Older Reports\\";
                    else reportpath = reportpath + "\\" + SystemVariables.BrandName + "\\Report\\Older Reports\\";
                    if (Directory.Exists(reportpath))
                    {
                        foreach (var subDir in new DirectoryInfo(reportpath).GetDirectories())
                        {
                            subDir.Delete(true);
                        }

                    }
                    else
                    {
                        DocMessage.MsgInformation("Older reports do not exist");
                    }
                }
                catch
                {
                }
            }
        }
    }
}
