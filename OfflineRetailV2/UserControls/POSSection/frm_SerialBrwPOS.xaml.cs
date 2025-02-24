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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_SerialBrw.xaml
    /// </summary>
    public partial class frm_SerialBrwPOS : UserControl
    {
        private FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool bOpenKeyBrd = false;

        double posActionListBoxVerticalUpOffset = 0;
        double posActionListBoxVerticalDownOffset = 0;

        public frm_SerialBrwPOS()
        {
            InitializeComponent();
            
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

        public void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private void OnCloseCOmmand(object obj)
        {
            
        }

        private int intSelectedRowHandle;
        private int intPID;
        private int intSerialHeaderID;
        private DataTable dtblSL;

        public DataTable dtblS
        {
            get { return dtblSL; }
            set { dtblSL = value; }
        }
        private string strCAT;
        public string CAT
        {
            get { return strCAT; }
            set { strCAT = value; }
        }
        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            
            for (intColCtr = 0; intColCtr < (frm_SerialBrwUC.grdSerialized.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, frm_SerialBrwUC.grdSerialized, frm_SerialBrwUC.colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) frm_SerialBrwUC.gridView1.FocusedRowHandle = intColCtr;
        }


        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((frm_SerialBrwUC.grdSerialized.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (frm_SerialBrwUC.gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = frm_SerialBrwUC.gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, frm_SerialBrwUC.grdSerialized, frm_SerialBrwUC.colID)));
            return intRecID;
        }

        

        public void FetchData()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SerialHeaderID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Serial1", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Serial2", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Serial3", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ExpiryDate", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Sold", System.Type.GetType("System.Boolean"));

            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchSerializedData(strCAT, intPID, SystemVariables.DateFormat);

            if (strCAT == "POS")
            {
                bool blfind = false;
                foreach (DataRow dr in dbtbl.Rows)
                {

                    if (dtblSL.Rows.Count > 0)
                    {
                        blfind = false;
                        foreach (DataRow dr1 in dtblSL.Rows)
                        {
                            if (dr1["PRODUCTTYPE"].ToString() != "E") continue;
                            if ((dr1["ID"].ToString() == intPID.ToString()) && (dr1["UOMCOUNT"].ToString() == dr["ID"].ToString()))
                            {
                                blfind = true;
                                break;
                            }
                        }
                        if (!blfind)
                        {
                            dtbl.Rows.Add(new object[] {  dr["ID"].ToString(),
                                                      dr["SerialHeaderID"].ToString(),
                                                      dr["Serial1"].ToString(),
                                                      dr["Serial2"].ToString(),
                                                      dr["Serial3"].ToString(),
                                                      dr["ExpiryDate"].ToString(),
                                                      Convert.ToBoolean(dr["Sold"].ToString())});
                        }

                    }
                    else
                    {
                        dtbl.Rows.Add(new object[] {  dr["ID"].ToString(),
                                                      dr["SerialHeaderID"].ToString(),
                                                      dr["Serial1"].ToString(),
                                                      dr["Serial2"].ToString(),
                                                      dr["Serial3"].ToString(),
                                                      dr["ExpiryDate"].ToString(),
                                                      Convert.ToBoolean(dr["Sold"].ToString())});
                    }
                }

                frm_SerialBrwUC.grdSerialized.ItemsSource = dtbl;
                
            }
            else
            {
                frm_SerialBrwUC.grdSerialized.ItemsSource = dbtbl;
            }
            dbtbl.Dispose();
            dtbl.Dispose();

        }

        public int FetchRecordCount()
        {
            PosDataObject.Product objProduct1 = new PosDataObject.Product();
            objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct1.GetSerializedCount(intPID);
        }

        public string FetchPOSFlag()
        {
            PosDataObject.Product objProduct2 = new PosDataObject.Product();
            objProduct2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct2.SerializedPOSAdd(intPID);
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GeneralFunctions.SetFocus(frm_SerialBrwUC.txtSearchGrdData);

            frm_SerialBrwUC.btnAdd.Click += new RoutedEventHandler(BtnAddClick);
            frm_SerialBrwUC.btnEdit.Click += new RoutedEventHandler(BtnEditClick);
            frm_SerialBrwUC.btnDelete.Click += new RoutedEventHandler(BtnDeleteClick);
            frm_SerialBrwUC.gridView1.MouseDoubleClick += GridView1_MouseDoubleClick;
            frm_SerialBrwUC.txtSearchGrdData.TextChanged += TxtSearchGrdData_TextChanged;
            frm_SerialBrwUC.txtSearchGrdData.GotFocus += TxtSearchGrdData_GotFocus;
            frm_SerialBrwUC.txtSearchGrdData.LostFocus += TxtSearchGrdData_LostFocus;
            frm_SerialBrwUC.txtSearchGrdData.PreviewMouseDown += TxtSearchGrdData_MouseDown;
            

            if (strCAT == "POS") frm_SerialBrwUC.colSold.Visible = false;
            if (FetchRecordCount() == 0)
            {
                intSerialHeaderID = 0;
                chkPOS.IsChecked = true;
            }
            else
            {
                PosDataObject.Product objProduct = new PosDataObject.Product();
                objProduct.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = objProduct.FetchSerializedHeaderData(intPID);
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["AllowPOSNew"].ToString() == "Y") chkPOS.IsChecked = true;
                    else chkPOS.IsChecked = false;
                    intSerialHeaderID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                }
                dtbl.Dispose();
            }
            FetchData();
        }

       

        public void AddHeader()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;
            objProduct.SerialProductID = intPID;
            if (chkPOS.IsChecked == true) objProduct.AllowPOSNew = "Y";
            else objProduct.AllowPOSNew = "N";

            string ret = objProduct.InsertSerializedHeaderData();
            if (ret == "")
            {
                intSerialHeaderID = objProduct.ID;
            }
            else intSerialHeaderID = 0;

        }

        public void UpdateHeader()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.ID = intSerialHeaderID;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;
            if (chkPOS.IsChecked == true) objProduct.AllowPOSNew = "Y";
            else objProduct.AllowPOSNew = "N";

            string ret = objProduct.UpdateSerializedHeaderData();

        }
        
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (intSerialHeaderID == 0) AddHeader();
            else UpdateHeader();
        }

        async void BtnAddClick(object sender, RoutedEventArgs e)
        {
            
            int intNewRecID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_SerialDlg frm_SerialDlg = new frm_SerialDlg();
            try
            {
                frm_SerialDlg.ID = 0;
                frm_SerialDlg.CAT = strCAT;
                if (strCAT == "ALL")
                {
                    if (intSerialHeaderID == 0) AddHeader();
                    frm_SerialDlg.PID = intSerialHeaderID;
                }
                if (strCAT == "POS")
                {
                    PosDataObject.Product objProduct = new PosDataObject.Product();
                    objProduct.Connection = SystemVariables.Conn;
                    DataTable dtbl = new DataTable();
                    dtbl = objProduct.FetchSerializedHeaderData(intPID);
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        intSerialHeaderID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    }
                    frm_SerialDlg.PID = intSerialHeaderID;
                }

                frm_SerialDlg.BrowseFormPOS = this;
                frm_SerialDlg.ShowDialog();
                intNewRecID = frm_SerialDlg.NewID;
            }
            finally
            {
                frm_SerialDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                if (strCAT == "POS")
                {
                    FetchData();
                }
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);

            
        }

        private async Task EditProcess()
        {
            
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = frm_SerialBrwUC.gridView1.FocusedRowHandle;
            if ((frm_SerialBrwUC.grdSerialized.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            blurGrid.Visibility = Visibility.Visible;
            frm_SerialDlg frm_SerialDlg = new frm_SerialDlg();
            try
            {
                frm_SerialDlg.ID = await ReturnRowID();
                if (frm_SerialDlg.ID > 0)
                {
                    if ((await GeneralFunctions.GetCellValue1(frm_SerialBrwUC.gridView1.FocusedRowHandle, frm_SerialBrwUC.grdSerialized, frm_SerialBrwUC.colSold)).ToUpper() == "FALSE")
                    {
                        frm_SerialDlg.CAT = strCAT;
                        frm_SerialDlg.PID = intSerialHeaderID;
                        //frm_SerialDlg.BrowseForm = this;
                        frm_SerialDlg.ShowDialog();
                        intNewRecID = frm_SerialDlg.ID;
                    }
                    else
                    {
                        DocMessage.MsgInformation("This serial number has already been sold and can not be modified");
                    }
                }
            }
            finally
            {
                frm_SerialDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);

            
        }

        async void BtnEditClick(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (strCAT != "POS")
            {
                await EditProcess();
            }
            else
            {
                (((((this.Parent as Grid).Parent as Grid).Parent as Grid).Parent as OfflineRetailV2.Controls.ModalWindow).Parent as frm_POSProductAddnDlg).SelectSerialProduct();
            }
        }

        async void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
         
            int intRowID = -1;
            intRowID = frm_SerialBrwUC.gridView1.FocusedRowHandle;
            if ((frm_SerialBrwUC.grdSerialized.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if ((await GeneralFunctions.GetCellValue1(frm_SerialBrwUC.gridView1.FocusedRowHandle, frm_SerialBrwUC.grdSerialized, frm_SerialBrwUC.colSold)).ToUpper() == "FALSE")
                {
                    if (DocMessage.MsgDelete(Properties.Resources.Serialization))
                    {
                        PosDataObject.Product objProduct = new PosDataObject.Product();
                        objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        string strError = objProduct.DeleteSerializedRecord(intRecdID);
                        if (strError != "")
                        {
                            DocMessage.ShowException("Deleting Serialized Definition", strError);
                        }
                        FetchData();
                        if ((frm_SerialBrwUC.grdSerialized.ItemsSource as DataTable).Rows.Count > 1)
                        {
                            frm_SerialBrwUC.gridView1.FocusedRowHandle = intRowID - 1;
                        }
                    }
                }
                else
                {
                    DocMessage.MsgInformation("This serial number has already been sold and can not be deleted");
                }
            }

            
        }


        async void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (frm_SerialBrwUC.txtSearchGrdData.Text == "Search with Serial no.") return;
            if (frm_SerialBrwUC.txtSearchGrdData.Text == "")
            {
                frm_SerialBrwUC.grdSerialized.FilterString = "";
                return;
            }
            string filterValue = frm_SerialBrwUC.txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                frm_SerialBrwUC.grdSerialized.FilterString = "([Serial1] LIKE '%" + filterValue + "%' OR [Serial2] LIKE '%" + filterValue + "%' OR [Serial3] LIKE '%" + filterValue + "%')";
            }
        }

        async void TxtSearchGrdData_MouseDown(object sender, RoutedEventArgs e)
        {
            
        }


        async void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (frm_SerialBrwUC.txtSearchGrdData.InfoText == "Search with Serial No.")
            {
                frm_SerialBrwUC.txtSearchGrdData.Text = "";
            }

            if (Settings.UseTouchKeyboardInPOS == "N") return;
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
                fkybrd.calledusercontrol = frm_SerialBrwUC;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }

        async void TxtSearchGrdData_LostFocus(object sender, RoutedEventArgs e)
        {
            if (frm_SerialBrwUC.txtSearchGrdData.Text == "")
            {
                frm_SerialBrwUC.txtSearchGrdData.InfoText = "Search with Serial No.";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        public async Task UpScroll()
        {
            if (sv is null) return;



            posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = sv.VerticalOffset - 30;
            if (posActionListBoxVerticalDownOffset < 0)
            {
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = 0;
            }
            sv.ScrollToVerticalOffset(posActionListBoxVerticalDownOffset);

        }

        public async Task DownScroll()
        {
            if (sv is null) return;

            if (posActionListBoxVerticalUpOffset < sv.ExtentHeight)
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = sv.VerticalOffset + 30;
            sv.ScrollToVerticalOffset(posActionListBoxVerticalUpOffset);


        }
    }
}
