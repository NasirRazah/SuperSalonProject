using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraEditors;
using Microsoft.PointOfService;
using OfflineRetailV2.Data;
using pos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSWorkOrderDlg.xaml
    /// </summary>
    public partial class frm_POSWorkOrderDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSWorkOrderDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSWorkOrderDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
            
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
            if (Settings.UseTouchKeyboardInPOS == "N") return;
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

        private void Frm_POSWorkOrderDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            Title. Text = Properties.Resources.Work_Order_Transaction;
            

            if (Settings.DecimalPlace == 3)
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
            else
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };

            dtF.DateTime = DateTime.Today.Date;
            dtT.DateTime = DateTime.Today.Date;

            cmbDate.SelectedIndex = 3;

            if (Settings.CentralExportImport == "N")
            {
                cmbStore.Visibility = Visibility.Collapsed;
            }
            else
            {
                cmbStore.Visibility = Visibility.Visible;
                PopulateCustomerStores();
            }

            PopulateCustomer();

            FetchData();

            blFetch = true;

            if (intMainScreenCustID != 0)
            {
                LookUpEdit clkup = new LookUpEdit();
                //foreach (Control c in this.Controls) --Sam
                //{
                //    if (c is LookUpEdit)
                //    {
                //        if (c.Name == "cmbC")
                //        {
                //            clkup = (LookUpEdit)c;
                //            break;
                //        }
                //    }
                //}
                if (Settings.CentralExportImport == "Y")
                {
                    PosDataObject.POS objPOS = new PosDataObject.POS();
                    objPOS.Connection = SystemVariables.Conn;
                    string tstr = objPOS.FetchCustomerIssueStore(intMainScreenCustID);
                    if (tstr != Settings.StoreCode) cmbStore.Text = tstr;
                }
                clkup.EditValue = intMainScreenCustID.ToString();
               grdTran.ItemsSource = null;
                FetchData();
            }

            if (Settings.ScaleDevice == "Datalogic Scale")
            {
                PrepareDatalogicScanner();
            }
        }

        private string SCAN = "";
        PosExplorer m_posExplorer = null;
        Scanner m_posScanner = null;
        SortAndSearchLookUpEdit sortAndSearchLookUpEditC;
        private int intSuspendInv;
        private bool blFetch = false;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blAllowByAdmin = false;
        private int intMainScreenCustID;
        public int MainScreenCustID
        {
            get { return intMainScreenCustID; }
            set { intMainScreenCustID = value; }
        }
        public int SuspendInv
        {
            get { return intSuspendInv; }
            set { intSuspendInv = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        private void cmbC_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        public void PopulateCustomerStores()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupCustomerStore();
            cmbStore.Items.Clear();
            foreach (DataRow dr in dbtblCust.Rows)
            {
                cmbStore.Items.Add(dr["IssueStore"].ToString());
            }
            dbtblCust.Dispose();
            cmbStore.Text = Settings.StoreCode;
        }


        public void PopulateCustomer()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            objCustomer.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupData(cmbStore.Text);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCust.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbC.ItemsSource = dtblTemp;
            cmbC.EditValue = "0";
            dbtblCust.Dispose();
        }

        private void LookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch) FetchData();
        }

        private void PrepareDatalogicScanner()
        {
            SCAN = "";
            bool blFind = false;
            m_posExplorer = new PosExplorer();

            DeviceInfo deviceInfo = null;
            DeviceCollection deviceCollection = m_posExplorer.GetDevices();
            string deviceName = Settings.Datalogic_Scanner;
            for (int i = 0; i < deviceCollection.Count; i++)
            {
                deviceInfo = deviceCollection[i];
                if (deviceInfo.ServiceObjectName == deviceName)
                {
                    blFind = true;
                    break;
                }
            }

            if (blFind)
            {
                if (deviceInfo != null)
                {
                    if (m_posScanner != null) { m_posScanner.Release(); m_posScanner.Close(); }

                    try
                    {
                        m_posScanner = (Scanner)m_posExplorer.CreateInstance(deviceInfo);

                        m_posScanner.Open();
                        m_posScanner.Claim(20000);

                        m_posScanner.DeviceEnabled = true;
                        m_posScanner.DataEventEnabled = true;
                        m_posScanner.DecodeData = true;
                        m_posScanner.DataEvent += new DataEventHandler(_Scanner_DataEvent);
                    }
                    catch
                    {
                    }
                }

            }

        }

        void _Scanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();

                byte[] b = m_posScanner.ScanData;

                string str = "";

                b = m_posScanner.ScanDataLabel;
                for (int i = 0; i < b.Length; i++)
                    str += (char)b[i];

                m_posScanner.DataEventEnabled = true;
                m_posScanner.DeviceEnabled = true;

                if (Settings.Scanner_8200 == "Y")
                {
                    try
                    {
                        str = str.Remove(0, 3);
                    }
                    catch
                    {
                    }
                }

                SCAN = str;
                txtInv.Text = SCAN;
                try
                {
                    m_posScanner.DeviceEnabled = false;
                    FetchSpecificData();
                    SCAN = "";
                }
                finally
                {
                    m_posScanner.DeviceEnabled = true;
                }
            }
            catch (PosControlException)
            {

            }
            finally
            {

            }
        }

        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if ((await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colPaymentDate)).ToString() == "")
                {
                    intSuspendInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                    DialogResult =true;
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private async void gridView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if ((await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colPaymentDate)).ToString() == "")
                {
                    intSuspendInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    Close();
                }
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

            if (btnHelp.Tag.ToString() == "")
            {
               new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private void FetchData()
        {
            
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = objPOS.FetchWorkOrderData(cmbType.SelectedIndex, GeneralFunctions.fnInt32(cmbC.EditValue.ToString()),
                            cmbDate.SelectedIndex, dtF.DateTime, dtT.DateTime, cmbStore.Text);

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

            grdTran.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void FetchSpecificData()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = objPOS.FetchWorkOrderData_Specific(GeneralFunctions.fnInt32(txtInv.Text.Trim()));
            if (dtbl.Rows.Count == 1)
            {
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
                grdTran.ItemsSource = dtblTemp;
                dtblTemp.Dispose();
            }
            dtbl.Dispose();
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 1)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
            txtInv.Text = "";
            txtInv.Focus();
        }

        private void cmbDate_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbDate.SelectedIndex == 0)
            {
                //pictureBox1.Visible = false;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Collapsed;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if ((cmbDate.SelectedIndex == 1) || (cmbDate.SelectedIndex == 2))
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if (cmbDate.SelectedIndex == 3)
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = true;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Visible;
                lbDate.Visibility = Visibility.Visible;
            }
            if (blFetch) FetchData();
        }

        private void dtF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtF.EditValue == null) return;
            if (blFetch) FetchData();
        }

        private void dtT_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtT.EditValue.ToString() == "") return;
            if (blFetch) FetchData();
        }

        private void cmbType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (blFetch) FetchData();
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (!CheckFunctionButton("31i")) return;

                /*if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/

                if ((await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colPaymentDate)).ToString() == "")
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                    try
                    {
                        frm_POSInvoicePrintDlg.InvNo = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv));
                        frm_POSInvoicePrintDlg.PrintType = "WorkOrder";
                        frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                        frm_POSInvoicePrintDlg.ShowDialog();
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }

                }
                else
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                    try
                    {
                        int INV = GetInvoiceNo(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grdTran, colInv)));
                        UpdateReceiptCnt(INV);
                        frm_POSInvoicePrintDlg.InvNo = INV;
                        frm_POSInvoicePrintDlg.PrintType = "Reprint Receipt";
                        frm_POSInvoicePrintDlg.ReprintCnt = GetReceiptCnt(INV);
                        frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                        frm_POSInvoicePrintDlg.ShowDialog();
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }

                }
            }
        }

        private int GetInvoiceNo(int WNO)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.FetchINVNoFromWONo(WNO);
        }

        private int GetReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.GetReceiptCount(invno);
        }

        private void UpdateReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            objPOS.LoginUserID = SystemVariables.CurrentUserID;
            objPOS.FunctionButtonAccess = blFunctionBtnAccess;
            objPOS.ChangedByAdmin = intSuperUserID;
            string ret = objPOS.UpdateReceiptCount(invno, 0);
        }

        private bool CheckFunctionButton(string scode)
        {
            blFunctionBtnAccess = false;
            if (SystemVariables.CurrentUserID <= 0)
            {
                blFunctionBtnAccess = true;
                return true;
            }

            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int result = objSecurity.IsExistsPOSAccess(SystemVariables.CurrentUserID, scode);
            if (result == 0)
            {
                if (blAllowByAdmin)
                {
                    return true;
                }
                else
                {
                    bool bl2 = false;
                    blurGrid.Visibility = Visibility.Visible;
                    frm_POSLoginDlg frm_POSLoginDlg2 = new frm_POSLoginDlg();
                    try
                    {
                        frm_POSLoginDlg2.SecurityCode = scode;
                        frm_POSLoginDlg2.ShowDialog();
                        if (frm_POSLoginDlg2.DialogResult == true)
                        {
                            bl2 = true;
                            blAllowByAdmin = frm_POSLoginDlg2.AllowByAdmin;
                            intSuperUserID = frm_POSLoginDlg2.SuperUserID;
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                    if (!bl2) return false;
                    else return true;
                }
            }
            else
            {
                blFunctionBtnAccess = true;
                return true;
            }
        }

        private void cmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            PopulateCustomer();
            if (blFetch) FetchData();
        }

        private void txtInv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtInv.Text.Trim() == "") e.Handled = false;
                FetchSpecificData();
                e.Handled = true;
            }
        }

        private void CmbC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) FetchData();
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == 0) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdTran.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == (grdTran.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CmbStore_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbC_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtF_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
