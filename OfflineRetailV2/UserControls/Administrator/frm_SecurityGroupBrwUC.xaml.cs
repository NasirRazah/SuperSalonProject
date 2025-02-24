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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_SecurityGroupBrwUC.xaml
    /// </summary>
    public partial class frm_SecurityGroupBrwUC : UserControl
    {
        public frm_SecurityGroupBrwUC()
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
            fParent.RedirectToSubmenu("Administrator");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdSecGroup.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdSecGroup, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdSecGroup, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdSecGroup, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public async Task<string> ReturnProfile()
        {
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0) return "";
            if (gridView1.FocusedRowHandle < 0) return "";
            int intRowID = gridView1.FocusedRowHandle;
            return await GeneralFunctions.GetCellValue1(intRowID, grdSecGroup, colGroup);
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = SystemVariables.Conn;
            dbtbl = objSecurity.FetchData();

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



            grdSecGroup.ItemsSource = dtblTemp;

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
            frm_SecurityGroupDlg frm_SecurityGroupDlg = new frm_SecurityGroupDlg();
            try
            {
                frm_SecurityGroupDlg.ID = 0;
                frm_SecurityGroupDlg.BrowseForm = this;
                frm_SecurityGroupDlg.ShowDialog();
                intNewRecID = frm_SecurityGroupDlg.NewID;
            }
            finally
            {
                frm_SecurityGroupDlg.Close();
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
            frm_SecurityGroupDlg frm_SecurityGroupDlg = new frm_SecurityGroupDlg();
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            try
            {
                frm_SecurityGroupDlg.ID = await ReturnRowID();
                if (frm_SecurityGroupDlg.ID > 0)
                {
                    frm_SecurityGroupDlg.BrowseForm = this;
                    frm_SecurityGroupDlg.ShowDialog();
                    intNewRecID = frm_SecurityGroupDlg.ID;
                }
            }
            finally
            {
                frm_SecurityGroupDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnAddPermission_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_SecurityPermissionDlg frm_SecurityPermissionDlg = new frm_SecurityPermissionDlg();
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;

            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            try
            {
                frm_SecurityPermissionDlg.GroupID = await ReturnRowID();
                if (frm_SecurityPermissionDlg.GroupID > 0)
                {
                    frm_SecurityPermissionDlg.GroupName = await ReturnProfile();
                    frm_SecurityPermissionDlg.BrowseForm = this;
                    frm_SecurityPermissionDlg.ShowDialog();
                    if (Settings.ActiveAdminPswdForMercury)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event7, "Successful", "");
                        }
                        else
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event7, "Successful", "");
                        }
                    }
                }
            }
            finally
            {
                frm_SecurityPermissionDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            await SetCurrentRow(await ReturnRowID());
        }

        private int IfExistInEmployee(int venID)
        {
            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objSecurity.IsExistProfile(venID);
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
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdSecGroup, colGroup) == "Owner")
                {
                    DocMessage.MsgPermission2();
                    return;
                }
                if (DocMessage.MsgDelete(Properties.Resources.Security_Profile))
                {

                    if (IfExistInEmployee(intRecdID) == 0)
                    {

                        MessageBoxResult dlgr = DocMessage.MsgConfirmation(Properties.Resources.Records_in_the_Group_Permission_will__be_deleted);
                        if (dlgr == MessageBoxResult.Yes)
                        {
                            PosDataObject.Security objSecurity = new PosDataObject.Security();
                            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            string strError = objSecurity.DeleteRecord(intRecdID);
                            PosDataObject.Security objSecurity1 = new PosDataObject.Security();
                            objSecurity1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            string strError1 = objSecurity1.DeleteGroupPermission(intRecdID);
                            if ((strError != "") || (strError1 != ""))
                            {
                                DocMessage.ShowException("Deleting Security Profile", strError);
                            }
                            else
                            {

                            }
                            FetchData();
                            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count > 1)
                            {
                                await SetCurrentRow(intRecdID - 1);
                            }
                        }


                    }
                    else
                    {
                        DocMessage.MsgInformation("This Security Profile already exists in Employee");
                    }

                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                DataTable dtbl = new DataTable();
                dtbl = grdSecGroup.ItemsSource as DataTable;

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.rep1CList rep_list = new OfflineRetailV2.Report.Misc.rep1CList();

                GeneralFunctions.MakeReportWatermark(rep_list);

                rep_list.Report.DataSource = dtbl;

                rep_list.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_list.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_list.rReportCaption.Text = "List of Security Profiles";

                rep_list.rHcol1.Text = "Security Profile";

                rep_list.rCol1.DataBindings.Add("Text", dtbl, "GroupName");

                if (Settings.ReportPrinterName != "") rep_list.PrinterName = Settings.ReportPrinterName;
                rep_list.CreateDocument();
                rep_list.PrintingSystem.ShowMarginsWarning = false;

                //rep_list.ShowPreviewDialog();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                window.PreviewControl.DocumentSource = rep_list;
                window.ShowDialog();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;

                rep_list.Dispose();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Profiles")
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
                txtSearchGrdData.InfoText = "Search Profiles";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Profiles") return;
            if (txtSearchGrdData.Text == "")
            {
                grdSecGroup.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdSecGroup.FilterString = "([GroupName] LIKE '%" + filterValue + "%')";
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
