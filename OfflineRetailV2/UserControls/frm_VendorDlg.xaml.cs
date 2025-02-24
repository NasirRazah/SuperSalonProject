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
using DevExpress.Xpf.Editors;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_VendorDlg.xaml
    /// </summary>
    public partial class frm_VendorDlg : Window
    {

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_VendorBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private int OldTabIndex = 0;
        private bool blPurchase = false;
        private bool blReceive = false;
        private bool blPartNo = false;
        private bool blfocus = false;
        public frm_VendorDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
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

        public frm_VendorBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_VendorBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private async void LbZip_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frmZipLookup frm_ZipLookup = new POSSection.frmZipLookup();
            try
            {
                frm_ZipLookup.ShowDialog();
                if (frm_ZipLookup.DialogResult == true)
                {
                    if (await frm_ZipLookup.GetColoum1Value() != "")
                    {
                        cmbBzip.Text = await frm_ZipLookup.GetColoum1Value();
                        //if (IsOtherStoreRecord) return;
                        if (cmbBzip.Text.Trim() != "")
                        {
                            if (CheckZip(cmbBzip.Text.Trim()) == 0)
                            {
                                POSSection.frmZipCodeDlg frm_ZipCodeDlg = new POSSection.frmZipCodeDlg();
                                try
                                {
                                    frm_ZipCodeDlg.ID = 0;
                                    frm_ZipCodeDlg.ForceAdd = true;
                                    frm_ZipCodeDlg.ForceZip = cmbBzip.Text.Trim();
                                    frm_ZipCodeDlg.ShowDialog();
                                    if (frm_ZipCodeDlg.DialogResult == true)
                                    {
                                        txtBcity.Text = frm_ZipCodeDlg.ForceCity;
                                        txtBstate.Text = frm_ZipCodeDlg.ForceState;
                                        GeneralFunctions.SetFocus(txtBcountry);
                                    }
                                    if (frm_ZipCodeDlg.DialogResult == false)
                                    {
                                        cmbBzip.Text = "";
                                        txtBcity.Text = "";
                                        txtBstate.Text = "";
                                    }
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                GetZipData(cmbBzip, txtBstate, txtBcity);
                                GeneralFunctions.SetFocus(txtBcountry);
                            }
                        }
                        else
                        {
                            txtBcity.Text = "";
                            txtBstate.Text = "";
                        }
                    }
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbBzip_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (IsOtherStoreRecord) return;
            if (cmbBzip.Text.Trim() != "")
            {
                if (CheckZip(cmbBzip.Text.Trim()) == 0)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    POSSection.frmZipCodeDlg frm_ZipCodeDlg = new POSSection.frmZipCodeDlg();
                    try
                    {
                        frm_ZipCodeDlg.ID = 0;
                        frm_ZipCodeDlg.ForceAdd = true;
                        frm_ZipCodeDlg.ForceZip = cmbBzip.Text.Trim();
                        frm_ZipCodeDlg.ShowDialog();
                        if (frm_ZipCodeDlg.DialogResult == true)
                        {
                            txtBcity.Text = frm_ZipCodeDlg.ForceCity;
                            txtBstate.Text = frm_ZipCodeDlg.ForceState;
                            GeneralFunctions.SetFocus(txtBcountry);
                        }
                        if (frm_ZipCodeDlg.DialogResult == false)
                        {
                            cmbBzip.Text = "";
                            txtBcity.Text = "";
                            txtBstate.Text = "";
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    GetZipData(cmbBzip, txtBstate, txtBcity);
                    GeneralFunctions.SetFocus(txtBcountry);
                }
            }
            else
            {
                txtBcity.Text = "";
                txtBstate.Text = "";
            }
        }

        private int CheckZip(string zip)
        {
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            return objZip.DuplicateCount(zip);
        }

        private void GetZipData(TextEdit zip, TextEdit state, TextEdit city)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            dtbl = objZip.ZIPData(zip.Text.Trim());
            foreach (DataRow dr in dtbl.Rows)
            {
                state.Text = dr["STATE"].ToString();
                city.Text = dr["CITY"].ToString();
            }
            dtbl.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Vendor;
                txtVendorID.IsEnabled = true;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Vendor;
                txtVendorID.IsEnabled = false;
                ShowData();
                //FetchPartNumber(intID);
            }
            boolControlChanged = false;
        }

        private bool IsValidAll()
        {
            if (txtVendorID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Vendor_ID);
                tcVendor.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtVendorID);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtVendorID); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }
            if (txtCompany.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Company);
                tcVendor.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtCompany);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtCompany); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (txtEmail.Text != "")
            {
                if (!GeneralFunctions.isEmail(txtEmail.Text))
                {
                    DocMessage.MsgEnter(Properties.Resources.Vaild_Email);
                    tcVendor.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtEmail);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtEmail); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Vendor_ID);
                    tcVendor.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtVendorID);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtVendorID); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            return true;
        }

        private int DuplicateCount()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objVendor.DuplicateCount(txtVendorID.Text.Trim());
        }

        public void ShowData()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objVendor.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtVendorID.Text = dr["VendorID"].ToString();
                txtCompany.Text = dr["Name"].ToString();
                txtContact.Text = dr["Contact"].ToString();
                txtAccountNo.Text = dr["AccountNo"].ToString();

                txtBadd1.Text = dr["Address1"].ToString();
                txtBadd2.Text = dr["Address2"].ToString();
                txtBcity.Text = dr["City"].ToString();
                txtBstate.Text = dr["State"].ToString();
                txtBcountry.Text = dr["Country"].ToString();
                cmbBzip.Text = dr["Zip"].ToString();

                txtPhone.Text = dr["Phone"].ToString();
                txtFax.Text = dr["Fax"].ToString();
                txtEmail.Text = dr["EMail"].ToString();
                txtNotes.Text = dr["Notes"].ToString();
                numMinAmount.Text = GeneralFunctions.fnDouble(dr["MinimumOrderAmount"].ToString()).ToString("f2");
            }
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            objVendor.LoginUserID = SystemVariables.CurrentUserID;

            objVendor.VendorID = txtVendorID.Text.Trim();
            objVendor.AccountNo = txtAccountNo.Text.Trim();
            objVendor.Name = txtCompany.Text.Trim();
            objVendor.Contact = txtContact.Text.Trim();

            objVendor.Address1 = txtBadd1.Text.Trim();
            objVendor.Address2 = txtBadd2.Text.Trim();
            objVendor.City = txtBcity.Text.Trim();
            objVendor.State = txtBstate.Text.Trim();
            objVendor.Country = txtBcountry.Text.Trim();
            objVendor.Zip = cmbBzip.Text.Trim();

            objVendor.Fax = txtFax.Text.Trim();
            objVendor.Phone = txtPhone.Text.Trim();
            objVendor.EMail = txtEmail.Text.Trim();
            objVendor.Notes = txtNotes.Text.Trim();
            objVendor.MinimumOrderAmount = GeneralFunctions.fnDouble(numMinAmount.Text);
            objVendor.ID = intID;

            if (intID == 0)
            {
                strError = objVendor.InsertData();
                NewID = objVendor.ID;
            }
            else
            {
                strError = objVendor.UpdateData();
            }
            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.FetchData();
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TabpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TabItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TabItem_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void TabItem_PreviewMouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 3;
        }

        private void TcVendor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = tcVendor.SelectedIndex;

            if (i == 1)
            {
                if (!blPartNo)
                {
                    FetchPartNumber(intID);
                    blPartNo = true;
                }
            }
            if (i == 2)
            {
                if (!blPurchase)
                {
                    POFDate.EditValue = DateTime.Today.Date.AddDays(-7);
                    POToDate.EditValue = DateTime.Today.Date;
                    blPurchase = true;
                }
            }
            if (i == 3)
            {
                if (!blReceive)
                {
                    RFDate.EditValue = DateTime.Today.Date.AddDays(-7);
                    RToDate.EditValue = DateTime.Today.Date;
                    blReceive = true;
                }
            }
        }

        private void FetchPartNumber(int fVendor)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            dbtbl = objVendor.ShowPartNumber(fVendor);
            grdPartNo.ItemsSource = dbtbl;
            dbtbl.Dispose();
        }

        private void FetchPurchase(int fVendor, DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            dbtbl = objPO.FetchPOHeader(fVendor, fFrmDate, fToDate, "");
            grdPO.ItemsSource = dbtbl;
            dbtbl.Dispose();
        }

        public void FetchReceive(int fVendor, DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Recv objRecv = new PosDataObject.Recv();
            objRecv.Connection = SystemVariables.Conn;
            dbtbl = objRecv.FetchRecvHeader(fVendor, fFrmDate, fToDate, "");
            grdRecv.ItemsSource = dbtbl;
            dbtbl.Dispose();
        }

        private void POFDate_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if ((POFDate.EditValue != null) && (POToDate.EditValue != null))
                FetchPurchase(intID, GeneralFunctions.fnDate(POFDate.EditValue.ToString()), GeneralFunctions.fnDate(POToDate.EditValue.ToString()));
        }

        private void RFDate_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if ((RFDate.EditValue != null) && (RToDate.EditValue != null))
                FetchReceive(intID, GeneralFunctions.fnDate(RFDate.EditValue.ToString()), GeneralFunctions.fnDate(RToDate.EditValue.ToString()));
        }

        private void TxtVendorID_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void POFDate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
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

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
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




        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

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



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }
    }

        
    }
