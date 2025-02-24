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
    /// Interaction logic for frm_GLBrwUC.xaml
    /// </summary>
    public partial class frm_GLBrwUC : UserControl
    {
        public frm_GLBrwUC()
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
            for (intColCtr = 0; intColCtr < (grdClass.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdClass, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdClass.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdClass, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdClass.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdClass, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdClass.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.GLedger objClass = new PosDataObject.GLedger();
            objClass.Connection = SystemVariables.Conn;
            dbtbl = objClass.FetchGL();

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



            grdClass.ItemsSource = dtblTemp;

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
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            int intNewRecID = 0;
            frm_GLDlg frm_ClassDlg = new frm_GLDlg();
            try
            {
                frm_ClassDlg.ID = 0;
                frm_ClassDlg.BrowseForm = this;
                frm_ClassDlg.ShowDialog();
                intNewRecID = frm_ClassDlg.NewID;
            }
            finally
            {
                frm_ClassDlg.Close();
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
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdClass.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_GLDlg frm_ClassDlg = new frm_GLDlg();
            try
            {
                frm_ClassDlg.ID = await ReturnRowID();
                if (frm_ClassDlg.ID > 0)
                {
                    //frm_ClassDlg.ShowData();
                    frm_ClassDlg.BrowseForm = this;
                    frm_ClassDlg.ShowDialog();
                    intNewRecID = frm_ClassDlg.ID;
                }
            }
            finally
            {
                frm_ClassDlg.Close();
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
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdClass.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.GL_Code))
                {
                    if (GeneralFunctions.GetRecordCountForDelete("TenderTypes", "LinkGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.tender_types);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Dept", "LinkSalesGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Department);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Dept", "LinkCostGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Department);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Dept", "LinkPayableGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Department);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Dept", "LinkInventoryGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Department);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("TaxHeader", "LinkGL", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Tax);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL1", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Accounts_Payable_Purchase);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL2", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Tax_Payable_Purchase);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL3", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Freight_Purchase);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL4", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Sales_Discount);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL5", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Customer_ROA);
                        return;
                    }

                    if (GeneralFunctions.GetRecordCountForDelete("Setup", "LinkGL6", intRecdID) > 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_GL_Code_as_it_is_linked_with + " " + Properties.Resources.Fees_and_Charges);
                        return;
                    }

                    PosDataObject.GLedger objClass = new PosDataObject.GLedger();
                    objClass.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objClass.DeleteGL(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Deleting G/L Code", strError);
                    }
                    FetchData();
                    if ((grdClass.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
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
            if (txtSearchGrdData.InfoText == "Search G/L")
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
                txtSearchGrdData.InfoText = "Search G/L";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search G/L") return;
            if (txtSearchGrdData.Text == "")
            {
                grdClass.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdClass.FilterString = "([GLCode] LIKE '%" + filterValue + "%' OR [GLDescription] LIKE '%" + filterValue + "%')";
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
