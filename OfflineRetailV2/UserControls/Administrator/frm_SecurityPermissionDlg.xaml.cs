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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_SecurityPermissionDlg.xaml
    /// </summary>
    public partial class frm_SecurityPermissionDlg : Window
    {
        public frm_SecurityPermissionDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        bool isFirst = true;

        private void GrdPermission_AsyncOperationCompleted(object sender, RoutedEventArgs e)
        {
            var gridControl = sender as DevExpress.Xpf.Grid.GridControl;

            if (gridControl.GroupCount > 0 && isFirst)
            {
                int rowHandle = gridControl.GetRowHandleByListIndex(0);
                if (!gridControl.IsGroupRowHandle(rowHandle))
                    rowHandle = gridControl.GetParentRowHandle(rowHandle);

                if (!gridControl.IsGroupRowExpanded(rowHandle))
                {
                    gridControl.ExpandGroupRow(rowHandle);
                }
                isFirst = false;
            }
        }

        private void GrdPermission_EndGrouping(object sender, RoutedEventArgs e)
        {
            isFirst = true;
        }

        private frm_SecurityGroupBrwUC frmBrowse;
        private int intID;
        private int intGroupID;
        private string strGroupName;
        private bool boolControlChanged;
        private bool blChanged = false;
        private bool blChangedLoad = false;

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int GroupID
        {
            get { return intGroupID; }
            set { intGroupID = value; }
        }

        public string GroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }

        public frm_SecurityGroupBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_SecurityGroupBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private DataTable FetchNewPermission()
        {
            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = SystemVariables.Conn;
            return objSecurity.FetchNewPermissionData();
        }


        private DataTable FetchInitAssignPermission()
        {
            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = SystemVariables.Conn;
            objSecurity.GroupID = intGroupID;
            return objSecurity.FetchAssignedPermissionData();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                blChanged = true;
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {

                    if (SaveData())
                    {
                        boolControlChanged = false;
                        if (blChanged)
                            DocMessage.MsgInformation(Properties.Resources.Changes_will_be_effective_from_the_next_login);
                        BrowseForm.FetchData();
                    }
                    else
                        e.Cancel = true;

                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string RegisterModule = GeneralFunctions.RegisteredModules();
            Title.Text = Properties.Resources.Permission + " : " + strGroupName;
            DataTable dtbl = FetchNewPermission();
            DataTable dtbl1 = FetchInitAssignPermission();
            foreach (DataRow dr in dtbl.Rows)
            {
                if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
                {
                    if ((dr["SecurityGroup"].ToString() == "Customer") || (dr["SecurityGroup"].ToString() == "POS"))
                    {
                        dr["SecurityVisible"] = false;
                        continue;
                    }
                    else
                    {
                        if ((dr["SecurityDesc"].ToString() == "Customer Screen") || (dr["SecurityDesc"].ToString() == "Tax Screen")
                            || (dr["SecurityDesc"].ToString() == "Tender Type Screen") || (dr["SecurityDesc"].ToString() == "Group Screen")
                            || (dr["SecurityDesc"].ToString() == "Class Screen") || (dr["SecurityDesc"].ToString() == "Dynamic CRM Screen")
                            || (dr["SecurityDesc"].ToString() == "Inventory Adjustment Screen") || (dr["SecurityDesc"].ToString() == "Central Office")
                            || (dr["SecurityDesc"].ToString() == "Change On-Hand Quantity"))
                        {
                            dr["SecurityVisible"] = false;
                            continue;
                        }
                    }

                    if (dr["SecurityDesc"].ToString() == "Add, Edit, Delete Access of Tax, Security etc.")
                    {
                        dr["SecurityDesc"] = "Add, Edit, Delete Access of Security";
                    }
                }
                if (RegisterModule.Contains("POS") && !RegisterModule.Contains("SCALE"))
                {
                    if ((dr["SecurityDesc"].ToString() == "Production List") || (dr["SecurityDesc"].ToString() == "Markdown"))
                    {
                        dr["SecurityVisible"] = false;
                        continue;
                    }
                }
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    if (dr["SecurityCode"].ToString() == dr1["SecurityCode"].ToString())
                    {
                        dr["SecurityCheck"] = Convert.ToBoolean(dr1["SecurityCheck"].ToString());
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

            grdPermission.ItemsSource = dtblTemp;

            int i = 0;
            while (i < grdPermission.VisibleRowCount)
            {
                int rH = grdPermission.GetRowHandleByVisibleIndex(i);
                if (grdPermission.IsGroupRowHandle(rH))
                {
                    grdPermission.ExpandGroupRow(rH);
                    break;
                }
                i++;
            }

            //grdPermission.ExpandGroupRow(0);
            //grdPermission.ExpandAllGroups();
            boolControlChanged = false;
            dtbl.Dispose();
            dtbl1.Dispose();
        }

        private bool ValidAllFields()
        {
            return true;
        }

        private bool SaveData()
        {
            if (ValidAllFields())
            {
                string ErrMsg = "";
                gridView1.PostEditor();

                PosDataObject.Security objSecurity = new PosDataObject.Security();
                objSecurity.Connection = SystemVariables.Conn;

                objSecurity.SplitDataTable = grdPermission.ItemsSource as DataTable;
                objSecurity.LoginUserID = SystemVariables.CurrentUserID;
                objSecurity.ErrorMsg = "";
                objSecurity.GroupID = intGroupID;

                //B e g i n   T r a n s a ct i o n
                objSecurity.BeginTransaction();
                if (objSecurity.InsertPermission())
                {
                    int intNewBatchID = 0;
                }
                objSecurity.EndTransaction();
                //E n d  T r a n s a ct i o n

                ErrMsg = objSecurity.ErrorMsg;
                if (ErrMsg != "")
                {
                    DocMessage.ShowException("Saving security permission", ErrMsg);
                    return false;
                }
                else
                {
                    if (Settings.ActiveAdminPswdForMercury)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event8, "Successful", strGroupName);
                        }
                        else
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event8, "Successful", strGroupName);
                        }
                    }
                    return true;
                }
            }
            else
            {
                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (SystemVariables.CurrentUserID == 0)
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event8, "Failed", strGroupName);
                    }
                    else
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event8, "Failed", strGroupName);
                    }
                }
                return false;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                if (blChanged) DocMessage.MsgInformation(Properties.Resources.Changes_will_be_effective_from_the_next_login);
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            blChanged = false;
            Close();
        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            blChanged = true;
            if (chkAll.IsChecked == true)
            {
                DataTable dtbl = grdPermission.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["SecurityCheck"] = true;
                }
                grdPermission.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                DataTable dtbl1 = grdPermission.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["SecurityCheck"] = false;
                }
                grdPermission.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                boolControlChanged = true;
                blChanged = true;
            }
        }

        private void GrdPermission_CustomRowFilter(object sender, DevExpress.Xpf.Grid.RowFilterEventArgs e)
        {
            if ((bool)(e.Source.ItemsSource as DataTable).Rows[e.ListSourceRowIndex]["SecurityVisible"] == true)
            {
                e.Visible = true;
                e.Handled = false;
            }
            if ((bool)(e.Source.ItemsSource as DataTable).Rows[e.ListSourceRowIndex]["SecurityVisible"] == false)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        private void GrdPermission_CustomGroupDisplayText(object sender, DevExpress.Xpf.Grid.CustomGroupDisplayTextEventArgs e)
        {

        }
    }
}
