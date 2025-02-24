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
    /// Interaction logic for frm_BuynGetFreeBrwUC.xaml
    /// </summary>
    public partial class frm_BuynGetFreeBrwUC : UserControl
    {
        public frm_BuynGetFreeBrwUC()
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
            fParent.RedirectToSubmenu("Discounts");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdBuyGetFree.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdBuyGetFree, colID)));
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
            if ((grdBuyGetFree.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdBuyGetFree, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdBuyGetFree.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdBuyGetFree, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdBuyGetFree.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Discounts objClass = new PosDataObject.Discounts();
            objClass.Connection = SystemVariables.Conn;
            dbtbl = objClass.FetchBuyNGetFreeData();
            foreach (DataRow dr in dbtbl.Rows)
            {
                dr["Free"] = objClass.FetchFreeData(GeneralFunctions.fnInt32(dr["ID"].ToString()));
            }


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



            grdBuyGetFree.ItemsSource = dtblTemp;

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
            frm_BuynGetFreeDlg frm_BuynGetFree = new frm_BuynGetFreeDlg();
            try
            {
                frm_BuynGetFree.ID = 0;
                frm_BuynGetFree.ViewMode = false;
                frm_BuynGetFree.BrowseForm = this;
                frm_BuynGetFree.ShowDialog();
                intNewRecID = frm_BuynGetFree.NewID;
            }
            finally
            {
                frm_BuynGetFree.Close();
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
            if ((grdBuyGetFree.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BuynGetFreeDlg frm_BuynGetFree = new frm_BuynGetFreeDlg();
            try
            {
                frm_BuynGetFree.ID = await ReturnRowID();
                if (frm_BuynGetFree.ID > 0)
                {
                    frm_BuynGetFree.ViewMode = false;
                    frm_BuynGetFree.BrowseForm = this;
                    frm_BuynGetFree.ShowDialog();
                    intNewRecID = frm_BuynGetFree.ID;
                }
            }
            finally
            {
                frm_BuynGetFree.Close();
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
            if ((grdBuyGetFree.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdBuyGetFree, colID));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Buy_n_Get_Free))
                {

                    PosDataObject.Discounts objCustomer = new PosDataObject.Discounts();
                    objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intrError = objCustomer.DeleteBuyNGetFree(intRecdID);
                    if (intrError != 0)
                    {
                        if (intrError == 1) DocMessage.MsgInformation(Properties.Resources.Sales_transactions_exists_against_it);
                        if (intrError == 2) DocMessage.MsgInformation(Properties.Resources.Suspend_transactions_exists_against_it);
                        if (intrError == 3) DocMessage.MsgInformation(Properties.Resources.Work_Order_transactions_exists_against_it);
                        return;
                    }
                    FetchData();
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Buy and Free")
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
                txtSearchGrdData.InfoText = "Search Buy and Free";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Buy and Free") return;
            if (txtSearchGrdData.Text == "")
            {
                grdBuyGetFree.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdBuyGetFree.FilterString = "([Buy] LIKE '%" + filterValue + "%' OR [Free] LIKE '%" + filterValue + "%')";
            }
        }
    }
}
