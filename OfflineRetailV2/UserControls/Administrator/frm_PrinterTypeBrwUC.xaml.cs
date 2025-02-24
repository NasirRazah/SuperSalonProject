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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_ProductTypeBrwUC.xaml
    /// </summary>
    public partial class frm_PrinterTypeBrwUC : UserControl
    {
        public frm_PrinterTypeBrwUC()
        {
            InitializeComponent();
        }


        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
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

        public FullKeyboard OnscreenKeyboard
        {
            get { return fkybrd; }
            set { fkybrd = value; }
        }




        private void txtSearchGrdData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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






        private void BtnBack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Administrator");
        }

        private int intSelectedRowHandle;

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
            PosDataObject.Setup objSecurity = new PosDataObject.Setup();
            objSecurity.Connection = SystemVariables.Conn;
            dbtbl = objSecurity.FetchPrinterTypes();

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
            dbtbl.Dispose();
            dtblTemp.Dispose();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_PrinterTypeDlg frm_SecurityGroupDlg = new frm_PrinterTypeDlg();
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
            
            int intNewRecID = 0;
            frm_PrinterTypeDlg frm_SecurityGroupDlg = new frm_PrinterTypeDlg();
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

        

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdSecGroup.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                
                if (DocMessage.MsgDelete("Printer Type"))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("PrintTemplate", "PrinterTypeID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation("Can not delete printer type as is is used in print template");
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Printers", "PrinterTypeID", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation("Can not delete printer type as is is used in printer");
                        return;
                    }

                    PosDataObject.Setup objSecurity = new PosDataObject.Setup();
                    objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objSecurity.DeletePrinterTypesRecord(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Deleting printer type", strError);
                    }
                    else
                    {
                        FetchData();
                        if ((grdSecGroup.ItemsSource as DataTable).Rows.Count > 1)
                        {
                            await SetCurrentRow(intRecdID - 1);
                        }
                    }
                    

                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Printer Types")
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
                txtSearchGrdData.InfoText = "Search Printer Types";
            }
            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Printer Types") return;
            if (txtSearchGrdData.Text == "")
            {
                grdSecGroup.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdSecGroup.FilterString = "([PrinterType] LIKE '%" + filterValue + "%')";
            }
        }
    }
}
