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
using Microsoft.Win32;
using System.IO;
using System.Collections;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustomerDlg.xaml
    /// </summary>
    public partial class frm_CustomerDlg : Window
    {

        NumKeyboard nkybrd;
        FullKeyboard fkybrd;
        bool IsAboutNumKybrdOpen = false;
        bool IsAboutFullKybrdOpen = false;

        private frm_CustomerBrw frmBrowse;
        private frm_CustomerBrwUC frmBrowseUC;
        public frm_CustomerNoteCalender frm_CustomerNoteCalender;
        //Todo: private frmKeyboardDlg frm_KeyboardDlg;
        private int intID;
        private int intNewID;
        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool boolControlChanged;

        private int intPrevClassRow = 0;
        private int intPrevGroupRow = 0;
        private int intSelectedRowHandle;
        private bool lbDeleteMode;
        private bool blDuplicate;
        private bool blfocus = false;
        private int intNoteYear;
        private int intNoteMonth;
        private bool blNotesSetting = false;
        private bool blNotesFiring = false;

        private double dblAL = 0;
        private bool blAddFromPOS;
        private bool blPOS;
        private bool IsOtherStoreRecord;

        private bool boolPopulatePurchaseData = false;
        private bool boolPopulateHouseAccountData = false;
        private bool boolPopulateStoreCreditData = false;

        private bool blLoading = false;

        private double prevAROpeningBalance = 0;

        private bool boolBookingExportFlagChanged;

        private string prevBookingExpFlag = "N";

        private int OldTabIndex = 0;

        public bool bPOS
        {
            get { return blPOS; }
            set { blPOS = value; }
        }

        public bool OtherStoreRecord
        {
            get { return IsOtherStoreRecord; }
            set { IsOtherStoreRecord = value; }
        }

        public bool AddFromPOS
        {
            get { return blAddFromPOS; }
            set { blAddFromPOS = value; }
        }

        public bool DeleteMode
        {
            get { return lbDeleteMode; }
            set { lbDeleteMode = value; }
        }

        public bool Duplicate
        {
            get { return blDuplicate; }
            set { blDuplicate = value; }
        }

        public int SelectedRowHandle
        {
            get { return intSelectedRowHandle; }
            set { intSelectedRowHandle = value; }
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

        public int NoteYear
        {
            get { return intNoteYear; }
            set { intNoteYear = value; }
        }

        public int NoteMonth
        {
            get { return intNoteMonth; }
            set { intNoteMonth = value; }
        }

        public frm_CustomerBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_CustomerBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_CustomerBrwUC BrowseFormUC
        {
            get
            {
                return frmBrowseUC;
            }
            set
            {
                frmBrowseUC = value;
            }
        }


        public class NoteData
        {
            public string ID { get; set; }
            public string Note { get; set; }
            public string DateTime { get; set; }
            public string SpecialEvent { get; set; }
            public string SEDateTime { get; set; }
            public DateTime CommonDateTime { get; set; }
            public ImageSource AttachMark { get; set; }
            public ImageSource Image { get; set; }
        }
        ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }



        public frm_CustomerDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
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

        private void OnClose(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void PopulateHouseAccountData()
        {
            PosDataObject.Sales objCustomer = new PosDataObject.Sales();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objCustomer.FetchCustomerHouseAccount(intID);

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

            grdHouseAccount.ItemsSource = dtbl;
        }

        private void PopulateStoreCreditData()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objCustomer.FetchCustomerStoreCreditTransaction(intID);

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

            grdStoreCredit.ItemsSource = dtbl;
        }

        public void PopulateServiceTypes()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "Sales", Properties.Resources.SALES });
            dtbl.Rows.Add(new object[] { "Rent", Properties.Resources.RENTS });
            dtbl.Rows.Add(new object[] { "Repair", Properties.Resources.REPAIRS });

            cmbService1.ItemsSource = dtbl;

            cmbService1.EditValue = "Sales";
        }

        private void PopulatePurchaseDetails()
        {
            if (!blLoading) return;
            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            dtbl = objSales.FetchCustomerPurchaseInvoice(intID, cmbService1.EditValue.ToString());

            if (cmbService1.EditValue.ToString() == "Sales")
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                    objSales1.Connection = SystemVariables.Conn;
                    double ha = objSales1.FetchCustomerHAPayment(GeneralFunctions.fnInt32(dr["InvNo"].ToString()));
                    dr["TotalSale"] = GeneralFunctions.fnDouble(dr["TotalSale"].ToString()) - ha;
                    dr["SubTotal"] = GeneralFunctions.fnDouble(dr["SubTotal"].ToString()) - ha;
                }
            }

            DataTable dtblNew = dtbl.Clone();
            DataRow[] d = dtbl.Select("TotalSale <> 0", "InvNo desc");
            foreach (DataRow dr in d)
            {
                dtblNew.ImportRow(dr);
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblNew.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdPurchase.ItemsSource = dtblNew;
        }

        private void PopulateDiscountLookup()
        {
            PosDataObject.Discounts objCustomer = new PosDataObject.Discounts();
            objCustomer.Connection = SystemVariables.Conn;
            objCustomer.DataObjectCulture_None = "(None)";
            DataTable dbtblDisc = new DataTable();
            dbtblDisc = objCustomer.FetchCustomerDiscountLookup();
            lkupDiscount.ItemsSource = dbtblDisc;
        }


        public void PopulateGroupSource()
        {
            DataTable dbtblgrp = new DataTable();
            dbtblgrp.Columns.Add("ID");
            dbtblgrp.Columns.Add("GroupID");
            dbtblgrp.Columns.Add("Description");

            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objCustomer.ShowGroup(intID);

            foreach (DataRow dr in dtbl.Rows)
            {
                dbtblgrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["GroupID"].ToString(), dr["Description"].ToString() });
            }



            for (int i = dbtblgrp.Rows.Count; i < 20; i++)
            {
                dbtblgrp.Rows.Add(new object[]
                {
                    "0","0",""
                });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblgrp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }


            grdGroup.ItemsSource = dtblTemp;

        }

        public void PopulateGroupPopup()
        {
            DataTable dbtblgrp = new DataTable();
            dbtblgrp.Columns.Add("ID");
            dbtblgrp.Columns.Add("GroupID");
            dbtblgrp.Columns.Add("Description");

            PosDataObject.Group objGroup = new PosDataObject.Group();
            objGroup.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objGroup.FetchData();

            foreach (DataRow dr in dtbl.Rows)
            {
                dbtblgrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["GroupID"].ToString(), dr["Description"].ToString() });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblgrp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            PART_Editor.AutoPopulateColumns = false;
            PART_Editor.ItemsSource = dtblTemp;

        }


        public void PopulateClassSource()
        {
            DataTable dbtblclass = new DataTable();
            dbtblclass.Columns.Add("ID");
            dbtblclass.Columns.Add("ClassID");
            dbtblclass.Columns.Add("Description");

            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objCustomer.ShowClass(intID);

            foreach (DataRow dr in dtbl.Rows)
            {
                dbtblclass.Rows.Add(new object[] { dr["ID"].ToString(), dr["ClassID"].ToString(), dr["Description"].ToString() });
            }

            for (int i = dbtblclass.Rows.Count; i < 20; i++)
            {
                dbtblclass.Rows.Add(new object[]
                {
                    "0","0",""
                });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblclass.DefaultView.ToTable();
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

        }

        public void PopulateClassPopup()
        {
            DataTable dbtblc = new DataTable();
            dbtblc.Columns.Add("ID");
            dbtblc.Columns.Add("ClassID");
            dbtblc.Columns.Add("Description");

            PosDataObject.Class objGroup = new PosDataObject.Class();
            objGroup.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objGroup.FetchData();

            foreach (DataRow dr in dtbl.Rows)
            {
                dbtblc.Rows.Add(new object[] { dr["ID"].ToString(), dr["ClassID"].ToString(), dr["Description"].ToString() });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblc.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            PART_Editor_C.AutoPopulateColumns = false;
            PART_Editor_C.ItemsSource = dtblTemp;

        }

        private void LoadClientDefinedParameters()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dtblParam = new DataTable();
            dtblParam = objSetup.FetchClientParamData();
            foreach (DataRow dr in dtblParam.Rows)
            {
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam1"].ToString().Trim() == ""))
                    lbParam1.Text = "1. Undefined";
                else lbParam1.Text = "1. " + dr["ClientParam1"].ToString();
                if ((dr["ClientParam2"].ToString() == null) || (dr["ClientParam2"].ToString().Trim() == ""))
                    lbParam2.Text = "2. Undefined";
                else lbParam2.Text = "2. " + dr["ClientParam2"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam3"].ToString().Trim() == ""))
                    lbParam3.Text = "3. Undefined";
                else lbParam3.Text = "3. " + dr["ClientParam3"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam4"].ToString().Trim() == ""))
                    lbParam4.Text = "4. Undefined";
                else lbParam4.Text = "4. " + dr["ClientParam4"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam5"].ToString().Trim() == ""))
                    lbParam5.Text = "5. Undefined";
                else lbParam5.Text = "5. " + dr["ClientParam5"].ToString();
            }

        }

        private void PermissionSettings()
        {
            if ((!SecurityPermission.AcssCustomerPriceLevel) && (SystemVariables.CurrentUserID > 0))
            {
                cmbPrice.IsReadOnly = true;
            }
            if ((!SecurityPermission.AcssCustomerTaxExempt) && (SystemVariables.CurrentUserID > 0))
            {
                chkTax.IsEnabled = false;
            }
            if ((!SecurityPermission.AcssCustomerShipAddress) && (SystemVariables.CurrentUserID > 0))
            {
                txtSadd1.IsReadOnly = true;
                txtSadd2.IsReadOnly = true;
                txtScity.IsReadOnly = true;
                txtSstate.IsReadOnly = true;
                cmbSzip.IsReadOnly = true;
                txtScountry.IsReadOnly = true;
            }
            if ((!SecurityPermission.AcssCustomerIssueCredit) && (SystemVariables.CurrentUserID > 0))
            {
                numCL.IsReadOnly = true;
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objCustomer.DuplicateCount(txtCustID.Text.Trim(), Settings.StoreCode);
        }

        private Double FetchAcctOpeningBalance()
        {
            PosDataObject.Customer objCust = new PosDataObject.Customer();
            objCust.Connection = SystemVariables.Conn;
            return objCust.FetchCustomerAcctOpeningBalance(intID);
        }

        private Double FetchAcctBalance()
        {
            PosDataObject.Customer objCust = new PosDataObject.Customer();
            objCust.Connection = SystemVariables.Conn;
            return objCust.FetchCustomerAcctBalance(intID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateServiceTypes();
            PopulateGroupPopup();
            PopulateClassPopup();

            PopulateDiscountLookup();

            LoadClientDefinedParameters();

            PermissionSettings();

            lbDate.Text = "";

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Customer;
                txtCustID.IsEnabled = true;
                cmbPrice.SelectedIndex = 0;
                if (Settings.AutoCustomer == "Y")
                {
                    txtCustID.Text = "AUTO";
                    txtCustID.IsReadOnly = true;
                    txtCustID.IsTabStop = false;
                }
            }
            else
            {
                if (!blDuplicate)
                {
                    Title.Text = Properties.Resources.Edit_Customer;
                    txtCustID.IsEnabled = false;
                }
                else
                {
                    Title.Text = Properties.Resources.Add_Customer;
                    txtCustID.IsEnabled = true;
                }
                ShowData();
                numOB.Text = FetchAcctOpeningBalance().ToString("f2");
                numCB.Text = FetchAcctBalance().ToString("f2");
                prevAROpeningBalance = GeneralFunctions.fnDouble(numOB.Text);
            }
            PopulateGroupSource();
            PopulateClassSource();

            if (blDuplicate)
            {
                intID = 0;
                txtCustID.Text = "";
                prevBookingExpFlag = "N";
            }

            if (intID == 0)
            {
                chkActive.IsChecked = true;
                chkActive.Visibility = Visibility.Hidden;
            }

            if (blAddFromPOS) btnKeyboard.Visibility = Visibility.Visible; else btnKeyboard.Visibility = Visibility.Hidden;

            if (IsOtherStoreRecord)
            {
                Title.Text = "View Customer Record";
                tpNotes.Visibility = Visibility.Collapsed;
                tpPurchase.Visibility = Visibility.Collapsed;
                tpHouseAccount.Visibility = Visibility.Collapsed;
                tpStoreCredit.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Hidden;
                btnKeyboard.Visibility = Visibility.Hidden;
                btnCancel.Content = "Close";
                btnAdjustment.Visibility = Visibility.Hidden;

                btnScanImage.IsEnabled = false;
                btnClearImage.IsEnabled = false;
                btnOpen.IsEnabled = false;

                txtCustID.IsReadOnly = true;
                chkActive.IsEnabled = false;
                txtCompany.IsReadOnly = true;
                txtFName.IsReadOnly = true;
                txtLName.IsReadOnly = true;
                txtSpouse.IsReadOnly = true;
                txtSalutation.IsReadOnly = true;

                txtBadd1.IsReadOnly = true;
                txtBadd2.IsReadOnly = true;
                txtBcity.IsReadOnly = true;
                txtBstate.IsReadOnly = true;
                txtBcountry.IsReadOnly = true;
                cmbBzip.IsReadOnly = true;

                txtSadd1.IsReadOnly = true;
                txtSadd2.IsReadOnly = true;
                txtScity.IsReadOnly = true;
                txtSstate.IsReadOnly = true;
                txtScountry.IsReadOnly = true;
                cmbSzip.IsReadOnly = true;

                txtWphone.IsReadOnly = true;
                txtHphone.IsReadOnly = true;
                txtMphone.IsReadOnly = true;
                txtFax.IsReadOnly = true;
                txtEmail.IsReadOnly = true;

                chkTax.IsEnabled = false;
                txtTaxID.IsReadOnly = true;
                cmbPrice.IsReadOnly = true;
                numCL.IsReadOnly = true;
                dtDOB.IsReadOnly = true;
                dtDOM.IsReadOnly = true;
                txtPoints.IsReadOnly = true;
                numSC.IsReadOnly = true;
                numOB.IsReadOnly = true;
                chkCard.IsEnabled = false;

                txtParamVal1.IsReadOnly = true;
                txtParamVal2.IsReadOnly = true;
                txtParamVal3.IsReadOnly = true;
                txtParamVal4.IsReadOnly = true;
                txtParamVal5.IsReadOnly = true;
                txtNotes.IsReadOnly = true;
                lkupDiscount.IsReadOnly = true;

                linkBZip.IsEnabled = false;
                linkSZip.IsEnabled = false;

                simpleButton3.IsEnabled = false;
                simpleButton2.IsEnabled = false;

                grdClass.IsEnabled = false;
                grdGroup.IsEnabled = false;
            }
            else
            {
                btnAdjustment.Visibility = intID > 0 ? Visibility.Visible : Visibility.Hidden;
            }

            /*if (intID == 0)
            {
                if (Settings.AutoCustomer == "N")
                {
                    GeneralFunctions.SetFocus(txtCustID);
                }
            }
            else
            {
                GeneralFunctions.SetFocus(txtCompany);
            }*/
            blLoading = true;
            boolControlChanged = false;
            boolBookingExportFlagChanged = false;
        }

        private bool IsValidAll()
        {
            if (txtCustID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Customer_ID_);
                tcCustomer.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtCustID);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtCustID); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (txtFName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.First_Name);
                tcCustomer.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtFName);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtFName); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }
            /*
            if (txtLName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Translation.SetMultilingualTextInCodes("Last Name","frmCustomerDlg_msg_LastName"));
                tcCustomer.SelectedTabPage = tpGeneral;
                GeneralFunctions.SetFocus(txtLName);
                return false;
            }*/
            if (dtDOB.EditValue != null)
            {
                if (dtDOB.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Date_of_Birth_after_today);
                    tcCustomer.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(dtDOB);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(dtDOB); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }
            if (dtDOM.EditValue != null)
            {
                if (dtDOM.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Date_of_Marriage_after_today);
                    tcCustomer.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(dtDOM);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(dtDOM); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if ((dtDOB.EditValue != null) && (dtDOM.EditValue != null))
            {
                if (dtDOM.DateTime.Date <= dtDOB.DateTime.Date)
                {
                    DocMessage.MsgInformation(Properties.Resources.Marrige_before_Birth);
                    tcCustomer.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(dtDOM);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(dtDOM); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (txtEmail.Text != "")
            {
                if (!GeneralFunctions.isEmail(txtEmail.Text))
                {
                    DocMessage.MsgEnter(Properties.Resources.Vaild_Email);
                    tcCustomer.SelectedIndex = 0;
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
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Customer_ID);
                    tcCustomer.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtCustID);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtCustID); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }
            return true;
        }



        public void ShowData()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objCustomer.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                prevBookingExpFlag = dr["BookingExpFlag"].ToString();

                txtCustID.Text = dr["CustomerID"].ToString();
                txtCompany.Text = dr["Company"].ToString();
                txtFName.Text = dr["FirstName"].ToString();
                txtLName.Text = dr["LastName"].ToString();
                txtSpouse.Text = dr["Spouse"].ToString();
                txtSalutation.Text = dr["Salutation"].ToString();

                txtBadd1.Text = dr["Address1"].ToString();
                txtBadd2.Text = dr["Address2"].ToString();
                txtBcity.Text = dr["City"].ToString();
                txtBstate.Text = dr["State"].ToString();
                txtBcountry.Text = dr["Country"].ToString();
                cmbBzip.Text = dr["Zip"].ToString();

                txtSadd1.Text = dr["ShipAddress1"].ToString();
                txtSadd2.Text = dr["ShipAddress2"].ToString();
                txtScity.Text = dr["ShipCity"].ToString();
                txtSstate.Text = dr["ShipState"].ToString();
                txtScountry.Text = dr["ShipCountry"].ToString();
                cmbSzip.Text = dr["ShipZip"].ToString();

                txtWphone.Text = dr["WorkPhone"].ToString();
                txtHphone.Text = dr["HomePhone"].ToString();
                txtMphone.Text = dr["MobilePhone"].ToString();
                txtFax.Text = dr["Fax"].ToString();
                txtEmail.Text = dr["EMail"].ToString();

                chkActive.IsChecked = (dr["ActiveStatus"].ToString() == "Y");

                /*if (dr["StoreCreditCard"].ToString() == "Y")
                {
                    chkCard.Checked = true;
                }
                else
                {
                    chkCard.Checked = false;
                }*/

                if (dr["TaxExempt"].ToString() == "Y")
                {
                    chkTax.IsChecked = true;
                }
                else
                {
                    chkTax.IsChecked = false;
                }
                txtTaxID.Text = dr["TaxID"].ToString();

                if (dr["DiscountLevel"].ToString() == "A") cmbPrice.SelectedIndex = 0;
                if (dr["DiscountLevel"].ToString() == "B") cmbPrice.SelectedIndex = 1;
                if (dr["DiscountLevel"].ToString() == "C") cmbPrice.SelectedIndex = 2;

                numCL.Text = GeneralFunctions.fnDouble(dr["ARCreditLimit"].ToString()).ToString("f2");

                if (dr["DateOfBirth"].ToString() == "")
                {
                    dtDOB.EditValue = null;
                }
                else
                {
                    dtDOB.EditValue = GeneralFunctions.fnDate(dr["DateOfBirth"].ToString());
                }
                if (dr["DateOfMarriage"].ToString() == "")
                {
                    dtDOM.EditValue = null;
                }
                else
                {
                    dtDOM.EditValue = GeneralFunctions.fnDate(dr["DateOfMarriage"].ToString());
                }
                txtPoints.Text = dr["Points"].ToString();
                numSC.Text = GeneralFunctions.fnDouble(dr["StoreCredit"].ToString()).ToString("f2");

                txtParamVal1.Text = dr["ParamValue1"].ToString();
                txtParamVal2.Text = dr["ParamValue2"].ToString();
                txtParamVal3.Text = dr["ParamValue3"].ToString();
                txtParamVal4.Text = dr["ParamValue4"].ToString();
                txtParamVal5.Text = dr["ParamValue5"].ToString();

                txtNotes.Text = dr["POSNotes"].ToString();

                //lkupTax.EditValue = dr["DTaxID"].ToString();
                lkupDiscount.EditValue = dr["DiscountID"].ToString();
            }

            if (!blDuplicate)
            {
                GeneralFunctions.LoadPhotofromDB("Customer", intID, pictPhoto);
            }

            dblAL = GeneralFunctions.fnDouble(numCL.Text);
        }

        private bool SaveData()
        {
            if (!boolControlChanged) return true;
            else
            {
                string strError = "";
                PosDataObject.Customer objCustomer = new PosDataObject.Customer();
                objCustomer.Connection = SystemVariables.Conn;
                objCustomer.LoginUserID = SystemVariables.CurrentUserID;
                objCustomer.ThisStoreCode = Settings.StoreCode;
                objCustomer.CustomerID = txtCustID.Text.Trim();
                objCustomer.AccountNo = "";
                objCustomer.LastName = txtLName.Text.Trim();
                objCustomer.FirstName = txtFName.Text.Trim();
                objCustomer.Spouse = txtSpouse.Text.Trim();
                objCustomer.Company = txtCompany.Text.Trim();
                objCustomer.Salutation = txtSalutation.Text.Trim();

                objCustomer.Address1 = txtBadd1.Text.Trim();
                objCustomer.Address2 = txtBadd2.Text.Trim();
                objCustomer.City = txtBcity.Text.Trim();
                objCustomer.State = txtBstate.Text.Trim();
                objCustomer.Country = txtBcountry.Text.Trim();
                objCustomer.Zip = cmbBzip.Text.Trim();

                objCustomer.ShipAddress1 = txtSadd1.Text.Trim();
                objCustomer.ShipAddress2 = txtSadd2.Text.Trim();
                objCustomer.ShipCity = txtScity.Text.Trim();
                objCustomer.ShipState = txtSstate.Text.Trim();
                objCustomer.ShipCountry = txtScountry.Text.Trim();
                objCustomer.ShipZip = cmbSzip.Text.Trim();

                objCustomer.WorkPhone = txtWphone.Text.Trim();
                objCustomer.HomePhone = txtHphone.Text.Trim();
                objCustomer.Fax = txtFax.Text.Trim();
                objCustomer.MobilePhone = txtMphone.Text.Trim();
                objCustomer.EMail = txtEmail.Text.Trim();

                objCustomer.Discount = "";

                if (chkTax.IsChecked == true)
                {
                    objCustomer.TaxExempt = "Y";
                }
                else
                {
                    objCustomer.TaxExempt = "N";
                }

                objCustomer.StoreCard = "N";

                objCustomer.ActiveStatus = (chkActive.IsChecked == true) ? "Y" : "N";

                objCustomer.TaxID = txtTaxID.Text.Trim();

                objCustomer.Category = 0;
                if (cmbPrice.SelectedIndex == 0) objCustomer.DiscountLevel = "A";
                if (cmbPrice.SelectedIndex == 1) objCustomer.DiscountLevel = "B";
                if (cmbPrice.SelectedIndex == 2) objCustomer.DiscountLevel = "C";
                if (cmbPrice.SelectedIndex == -1) objCustomer.DiscountLevel = "A";
                objCustomer.AmountLastPurchase = 0;
                objCustomer.StoreCredit = GeneralFunctions.fnDouble(numSC.Text.Trim());
                objCustomer.ARCreditLimit = GeneralFunctions.fnDouble(numCL.Text.Trim());
                objCustomer.AROpeningBalance = GeneralFunctions.fnDouble(numOB.Text.Trim());

                objCustomer.ID = intID;

                if (pictPhoto.Source != null)
                {
                    objCustomer.CustomerPhoto = GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source);
                }
                else
                {
                    objCustomer.CustomerPhoto = null;
                }

                if (dtDOB.EditValue == null)
                {
                    objCustomer.DateOfBirth = Convert.ToDateTime(null);
                }
                else
                {
                    objCustomer.DateOfBirth = dtDOB.DateTime.Date;
                }

                if (dtDOM.EditValue == null)
                {
                    objCustomer.DateOfMarriage = Convert.ToDateTime(null);
                }
                else
                {
                    objCustomer.DateOfMarriage = dtDOM.DateTime.Date;
                }
                objCustomer.ClosingBalance = GeneralFunctions.fnDouble(numCB.Text);
                objCustomer.Points = GeneralFunctions.fnInt32(txtPoints.Text);

                objCustomer.ParamValue1 = txtParamVal1.Text.Trim();
                objCustomer.ParamValue2 = txtParamVal2.Text.Trim();
                objCustomer.ParamValue3 = txtParamVal3.Text.Trim();
                objCustomer.ParamValue4 = txtParamVal4.Text.Trim();
                objCustomer.ParamValue5 = txtParamVal5.Text.Trim();
                objCustomer.POSNotes = txtNotes.Text.Trim();
                objCustomer.DTaxID = 0; //GeneralFunctions.fnInt32(lkupTax.EditValue);
                objCustomer.DiscountID = GeneralFunctions.fnInt32(lkupDiscount.EditValue);

                gridView3.PostEditor();
                gridView4.PostEditor();
                objCustomer.GroupDataTable = GetAttachedGroup();
                objCustomer.ClassDataTable = GetAttachedClass();

                objCustomer.AutoCustomer = Settings.AutoCustomer;

                if (intID > 0) // only in update mode
                {
                    if (prevAROpeningBalance == GeneralFunctions.fnDouble(numOB.Text)) objCustomer.UpdateAcOpeningBalanceFlag = false;
                    else objCustomer.UpdateAcOpeningBalanceFlag = true;
                }
                objCustomer.ThisStoreCode = Settings.StoreCode;
                objCustomer.BookingExpFlag = boolBookingExportFlagChanged ? "N" : prevBookingExpFlag;
                strError = objCustomer.PostData_WPF();
                intNewID = objCustomer.ID;

                if (strError == "") return true; else return false;
            }
        }

        private DataTable GetAttachedGroup()
        {
            DataTable dtblG = new DataTable();
            dtblG.Columns.Add("GroupID", System.Type.GetType("System.String"));

            DataTable dsource = grdGroup.ItemsSource as DataTable;

            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["GroupID"].ToString()) > 0)
                {
                    dtblG.Rows.Add(new object[] { dr["GroupID"].ToString() });
                }
            }

            return dtblG;
        }

        private DataTable GetAttachedClass()
        {
            DataTable dtblC = new DataTable();
            dtblC.Columns.Add("ClassID", System.Type.GetType("System.String"));

            DataTable dsource = grdClass.ItemsSource as DataTable;

            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ClassID"].ToString()) > 0)
                {
                    dtblC.Rows.Add(new object[] { dr["ClassID"].ToString() });
                }
            }
            return dtblC;
        }

        private bool IsDuplicateGroup(int refGroupID)
        {
            bool bDuplicate = false;
            DataTable dsource = grdGroup.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["GroupID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["GroupID"].ToString()) == refGroupID)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private bool IsDuplicateClass(int refClassID)
        {
            bool bDuplicate = false;
            DataTable dsource = grdClass.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ClassID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["ClassID"].ToString()) == refClassID)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private void GridView3_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colGroupID")
            {
                if (IsDuplicateGroup(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation("This Group has already been entered");

                    Dispatcher.BeginInvoke(new Action(() => gridView3.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Group objGroup = new PosDataObject.Group();
                    objGroup.Connection = SystemVariables.Conn;
                    grdGroup.SetCellValue(e.RowHandle, colGroupName, objGroup.GetGroupDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                }


            }
        }

        private void GridView4_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colClassID")
            {
                if (IsDuplicateClass(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation("This Class has already been entered");

                    Dispatcher.BeginInvoke(new Action(() => gridView4.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Class objGroup = new PosDataObject.Class();
                    objGroup.Connection = SystemVariables.Conn;
                    grdClass.SetCellValue(e.RowHandle, colClassName, objGroup.GetClassDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                }


            }
        }

        private async void SimpleButton3_Click(object sender, RoutedEventArgs e)
        {
            int GroupID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView3.FocusedRowHandle, grdGroup, colGroupID));
            if (GroupID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView3.DeleteRow(gridView3.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private async void SimpleButton2_Click(object sender, RoutedEventArgs e)
        {
            int ClassID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView4.FocusedRowHandle, grdClass, colClassID));
            if (ClassID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView4.DeleteRow(gridView4.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private void GrdGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdGroup.CurrentColumn.Name == "colGroupID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdGroup.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void GrdClass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdClass.CurrentColumn.Name == "colClassID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdClass.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void PART_Editor_C_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void PART_Editor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                boolControlChanged = true;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (!blPOS)
                    {
                        BrowseFormUC.storecd = Settings.StoreCode;
                        BrowseFormUC.FetchData(BrowseFormUC.cmbFilter.EditValue.ToString());
                    }
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

        private void TcCustomer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = tcCustomer.SelectedIndex;



            //if (i == 0) GeneralFunctions.SetFocus(txtCustID);
            if (i == 1) GeneralFunctions.SetFocus(txtParamVal1);

            if (i == 2)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            intID = NewID;
                        }
                    }
                }
                if (!blNotesSetting)
                {
                    blNotesSetting = true;
                    blNotesFiring = true;
                    PopulateNoteView();
                    cmbView.EditValue = "List View";
                    FetchNote("ALL", "Customer", intID);
                }
            }

            if ((i == 3) || (i == 4))
            {
                if (i == 3)
                {
                    if (intID != 0)
                    {

                        if (!boolPopulatePurchaseData)
                        {
                            PopulatePurchaseDetails();
                            boolPopulatePurchaseData = true;
                        }
                        btnPrint.Visibility = Visibility.Visible;
                    }
                }
                if (i == 4)
                {
                    if (intID != 0)
                    {
                        btnPrint.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                btnPrint.Visibility = Visibility.Hidden;
            }


            if (i == 4)
            {
                if (intID != 0)
                {
                    if (!boolPopulateHouseAccountData)
                    {
                        PopulateHouseAccountData();
                        boolPopulateHouseAccountData = true;
                    }
                    GetBalance();
                    GetLastPayment();
                    GetHADateRange();
                    numAL.Text = numCL.Text;
                    grpHA.Visibility = Visibility.Visible;
                }
            }
            else
            {
                grpHA.Visibility = Visibility.Collapsed;
            }


            if (i == 5)
            {
                if (intID != 0)
                {
                    if (!boolPopulateStoreCreditData)
                    {
                        PopulateStoreCreditData();
                        boolPopulateStoreCreditData = true;
                    }
                }
            }



        }

        private void GetHADateRange()
        {
            string mindt = "";
            string maxdt = "";
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl = objPOS.FetchAcctPayDateRange(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                mindt = dr["MinDate"].ToString();
                maxdt = dr["MaxDate"].ToString();
            }
            if (mindt != "")
            {
                dtF.EditValue = GeneralFunctions.fnDate(mindt);
            }
            else
            {
                dtF.EditValue = null;
            }
            if (maxdt != "")
            {
                dtT.EditValue = GeneralFunctions.fnDate(maxdt);
            }
            else
            {
                dtT.EditValue = null;
            }
        }

        private void GetBalance()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            numB.Text = objPOS.FetchCustomerAcctPayBalance(intID).ToString("f2");
        }

        private void GetLastPayment()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl = objPOS.FetchCustomerLastAcctPay(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                numLP.Text = GeneralFunctions.fnDouble(dr["Amount"].ToString()).ToString("f2");
                lbDate.Text = "( " + GeneralFunctions.fnDate(dr["Date"].ToString()).ToString("d") + " )";
            }
        }

        private void TpStoreCredit_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (intID == 0)
            {
                tcCustomer.SelectedIndex = OldTabIndex;
                return;
            }
            else
            {
                OldTabIndex = 5;
            }
        }

        private void TpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TpOthers_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TpNotes_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void TpPurchase_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 3;
        }

        private void TpHouseAccount_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 4;
        }

        private void CmbService1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (!blLoading) return;
            if (intID != 0)
            {
                PopulatePurchaseDetails();
                btnPrint.Visibility = Visibility.Visible;
            }
        }

        private void TcCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddCredit_Click(object sender, RoutedEventArgs e)
        {
            if ((SecurityPermission.AcssStoreCredit) || (SystemVariables.CurrentUserID <= 0))
            {
            }
            else
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_CustomerCreditAddDlg frm = new frm_CustomerCreditAddDlg();
            try
            {
                frm.ID = intID;
                frm.ShowDialog();

                if (frm.IsSave == true)
                {
                    PopulateStoreCreditData();
                    FetchStoreCreditAmount();
                }
            }
            finally
            {
                frm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void FetchStoreCreditAmount()
        {
            PosDataObject.Customer objc = new PosDataObject.Customer();
            objc.Connection = SystemVariables.Conn;
            bool btempchage = boolControlChanged;
            numSC.Text = objc.FetchCustomerStoreCreditAmount(intID).ToString("f2");
            if (!btempchage)
            {
                boolControlChanged = false;
            }
        }

        private void BtnAdjustment_Click(object sender, RoutedEventArgs e)
        {
            if ((SecurityPermission.AcssHouseAcountAdjust) || (SystemVariables.CurrentUserID <= 0))
            {
                blurGrid.Visibility = Visibility.Visible;
                frm_CustHAcAdjustment frm = new frm_CustHAcAdjustment();
                try
                {
                    frm.CustomerID = intID;
                    frm.ShowDialog();
                    if (frm.IsSave == true)
                    {
                        PopulateHouseAccountData();
                        GetLastPayment();
                        GetBalance();
                        numCB.Text = FetchAcctBalance().ToString("f2");
                    }
                }
                finally
                {
                    frm.Close();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                DocMessage.MsgPermission();
            }
        }




        public void PopulateNoteView()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "List View", "List View" });
            dtbl.Rows.Add(new object[] { "Calendar View", "Calendar View" });

            cmbView.ItemsSource = dtbl;
            cmbView.EditValue = "List View";
        }


        public void FetchNote(string sqlCriteria, string pRefType, int pRef)
        {
            if (blNotesFiring)
            {
                PosDataObject.Notes objNotes = new PosDataObject.Notes();
                objNotes.Connection = SystemVariables.Conn;
                DataTable dbtbl = new DataTable();
                dbtbl = objNotes.FetchNoteData(sqlCriteria, pRefType, pRef, SystemVariables.DateFormat);

                List<NoteData> dsource = new List<NoteData>();

                foreach (DataRow dr in dbtbl.Rows)
                {
                    dsource.Add(new NoteData()
                    {
                        ID = dr["ID"].ToString(),
                        Note = dr["Note"].ToString(),
                        SpecialEvent = dr["SpecialEvent"].ToString(),
                        DateTime = dr["DateTime"].ToString(),
                        CommonDateTime = GeneralFunctions.fnDate(dr["CommonDateTime"].ToString()),
                        SEDateTime = dr["DateTime"].ToString(),
                        AttachMark = dr["Attach"].ToString() == "Y" ? GetImage("/Resources/Icons/Pen.png") : null,
                        Image = GetImage("/Resources/Image/indicator.png")
                    });
                }

                foreach (DataRow dr in dbtbl.Rows)
                {


                    //MemoryStream ms = new MemoryStream();

                    //ImageSource image = new BitmapImage(new Uri("/Resources/Icons/Pen.png", UriKind.Relative));

                    //if (dr["Attach"].ToString() == "Y") dr["AttachMark"] = ms.ToArray();
                }

                grdNotes.ItemsSource = dsource;


                //dbtbl.Dispose();
            }

        }

        private void CmbView_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbView.EditValue.ToString() == "List View")
            {
                tcNotes.SelectedIndex = 0;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;

                tcNotes.SelectedIndex = 1;

                CallCalendar();
            }
        }

        public void CallCalendar()
        {
            GetCalendarData(intID, "Customer", CalenderStartDate(cbView.EditValue.ToString()));
        }

        private DateTime CalenderStartDate(string ViewType)
        {
            DateTime rdt = DateTime.Now;
            string strrtn = "";
            PosDataObject.Notes objnotes = new PosDataObject.Notes();
            objnotes.Connection = SystemVariables.Conn;
            objnotes.RefType = "Customer";
            objnotes.RefID = intID;
            strrtn = objnotes.GetMinSpecialEventDate();
            if (strrtn == "")
            {
                rdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                if ((ViewType == "Month View") || (ViewType == "Week View"))
                {
                    rdt = new DateTime(GeneralFunctions.fnDate(strrtn).Year, GeneralFunctions.fnDate(strrtn).Month, 1);
                }
                else
                {
                    rdt = new DateTime(GeneralFunctions.fnDate(strrtn).Year, GeneralFunctions.fnDate(strrtn).Month, GeneralFunctions.fnDate(strrtn).Day);
                }

            }
            return rdt;
        }

        public void GetCalendarData(int intRefID, string strRefType, DateTime CalenderSDate)
        {

            DateTime dtCalendarEnd = Convert.ToDateTime(null);

            DataTable dtblFetch = new DataTable();
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            dtblFetch = objNotes.FetchNoteData("S", strRefType, intRefID, SystemVariables.DateFormat);
            foreach (DataRow dr in dtblFetch.Rows)
            {
                /*MemoryStream ms = new MemoryStream();
                pos.Properties.Resources.attach.Save(ms, pos.Properties.Resources.attach.RawFormat);
                if (dr["Attach"].ToString() == "Y") dr["AttachMark"] = ms.ToArray();*/
            }
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTEID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTES", System.Type.GetType("System.String"));
            dtbl.Columns.Add("NOTESDTL", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("EDATE", System.Type.GetType("System.DateTime"));
            dtbl.Columns.Add("LABEL", System.Type.GetType("System.String"));
            //dtbl.Columns.Add("ATTACH", System.Type.GetType("System.Byte[]"));
            foreach (DataRow dr in dtblFetch.Rows)
            {
                string strLabel = "2";
                string strEvent = dr["SpecialEvent"].ToString();
                if (strEvent == "Yes") strLabel = "11";
                else strLabel = "12";
                dtbl.Rows.Add(new object[] {
                                            dr["ID"].ToString(),
                                            dr["ID"].ToString(),
                                            dr["Note"].ToString(),
                                            dr["Note"].ToString(),
                                            GeneralFunctions.fnDate(dr["SDateTime"].ToString()),
                                            Convert.ToDateTime(null),strLabel });
                //dr["AttachMark"] });
            }





            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("NOTEID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CAPTION", System.Type.GetType("System.String"));
            //dtbl1.Columns.Add("ATTACH", System.Type.GetType("System.Byte[]"));
            foreach (DataRow dr in dtblFetch.Rows)
            {

                dtbl1.Rows.Add(new object[] {
                                            dr["ID"].ToString(),
                                            "Note" });
                //dr["AttachMark"]});
            }
            schedulerControl1.DataSource.AppointmentsSource = dtbl;
            schedulerControl1.DataSource.ResourcesSource = dtbl1;

            dtbl.Dispose();
            dtbl1.Dispose();
            dtblFetch.Dispose();

            schedulerControl1.Start = CalenderSDate;

            if (cbView.EditValue.ToString() == "Month View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.MonthView;
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            if (cbView.EditValue.ToString() == "Week View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.WeekView;
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            if (cbView.EditValue.ToString() == "Day View")
            {
                schedulerControl1.ActiveViewType = DevExpress.Xpf.Scheduling.ViewType.DayView;
                schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.None;
            }

            schedulerControl1.UpdateLayout();

        }





        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdNotes.ItemsSource as ICollection).Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdNotes, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView5.FocusedRowHandle = intColCtr;
        }


        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdNotes.ItemsSource as ICollection).Count == 0) return intRecID;
            if (gridView5.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView5.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdNotes, colID)));
            return intRecID;
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int RecordID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_CustNotesDlg frm_CustNotesDlg = new frm_CustNotesDlg();
            try
            {
                frm_CustNotesDlg.ID = 0;
                frm_CustNotesDlg.RefID = intID;
                frm_CustNotesDlg.RefType = "Customer";
                frm_CustNotesDlg.BrowseFormC = this;
                frm_CustNotesDlg.ShowDialog();
            }
            finally
            {
                if (frm_CustNotesDlg.NewID > 0) RecordID = frm_CustNotesDlg.NewID;
                //frm_CustNotesDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (cmbView.EditValue.ToString() == "Calender View")
            {
                CallCalendar();
            }
            if (RecordID > 0) await SetCurrentRow(RecordID);
        }


        private async Task EditNote()
        {
            int intRowID = -1;
            int RecordID = 0;
            intRowID = gridView5.FocusedRowHandle;

            if ((grdNotes.ItemsSource as ICollection).Count == 0) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_CustNotesDlg frm_CustNotesDlg = new frm_CustNotesDlg();
            try
            {
                RecordID = await ReturnRowID();
                frm_CustNotesDlg.ID = RecordID;

                if (frm_CustNotesDlg.ID > 0)
                {
                    frm_CustNotesDlg.RefID = intID;
                    frm_CustNotesDlg.RefType = "Customer";
                    frm_CustNotesDlg.BrowseFormC = this;
                    frm_CustNotesDlg.ShowDialog();
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }

            await SetCurrentRow(RecordID);

            if (cmbView.EditValue.ToString() == "Calender View")
            {
                CallCalendar();
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditNote();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView5.FocusedRowHandle;
            if ((grdNotes.ItemsSource as ICollection).Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Customer Note"))
                {

                    PosDataObject.Notes objNotes = new PosDataObject.Notes();
                    objNotes.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objNotes.DeleteNote(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Deleting Customer Note", strError);
                    }
                    else
                    {
                        try
                        {
                            string attachpath = "";
                            attachpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                            if (attachpath.EndsWith("\\")) attachpath = attachpath + "XEPOS\\RetailV2\\Customer\\Notes\\";
                            else attachpath = attachpath + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
                            string docpath = "";
                            string picpath = "";
                            DirectoryInfo di = new DirectoryInfo(attachpath);
                            FileInfo[] fi = di.GetFiles();
                            foreach (FileInfo f in fi)
                            {
                                if (f.Name.Contains(intRecdID.ToString() + "_d"))
                                {
                                    docpath = attachpath + f.Name;
                                    break;
                                }
                            }
                            if (docpath != "")
                            {
                                FileInfo fd = new FileInfo(docpath);
                                fd.Delete();
                            }

                            foreach (FileInfo f in fi)
                            {
                                if (f.Name.Contains(intRecdID.ToString() + "_s"))
                                {
                                    picpath = attachpath + f.Name;
                                    break;
                                }
                            }
                            if (picpath != "")
                            {
                                FileInfo fd1 = new FileInfo(picpath);
                                fd1.Delete();
                            }
                        }
                        catch
                        {
                        }
                    }
                    FetchNote("", "Customer", intID);
                    if ((grdNotes.ItemsSource as ICollection).Count > 1)
                    {
                        await SetCurrentRow((intRecdID - 1));
                    }
                }

                /*if (tcNotes.SelectedTabPage == tpCalenderNotes)
                {
                    if (frm_CustomerNoteCalender != null)
                    {
                        frm_CustomerNoteCalender.pnlBody.Parent = frm_CustomerNoteCalender;
                        frm_CustomerNoteCalender.Dispose();
                        frm_CustomerNoteCalender = null;
                    }
                    tcNotes.SelectedTabPage = tpCalenderNotes;
                    CallCalendar();
                }*/
            }
        }

        private async void GridView5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditNote();
        }

        private void CbView_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            CallCalendar();
        }

        private void GridView5_CustomCellAppearance(object sender, DevExpress.Xpf.Grid.CustomCellAppearanceEventArgs e)
        {

        }

        private void TxtCustID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
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
                            if (!blPOS)
                            {
                                BrowseFormUC.storecd = Settings.StoreCode;
                                BrowseFormUC.FetchData(BrowseFormUC.cmbFilter.EditValue.ToString());
                            }
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

        private async void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frmZipLookup frm_ZipLookup = new frmZipLookup();
            try
            {
                frm_ZipLookup.ShowDialog();
                if (frm_ZipLookup.DialogResult == true)
                {
                    if (await frm_ZipLookup.GetColoum1Value() != "")
                    {
                        cmbBzip.Text = await frm_ZipLookup.GetColoum1Value();
                        if (IsOtherStoreRecord) return;
                        if (cmbBzip.Text.Trim() != "")
                        {
                            if (CheckZip(cmbBzip.Text.Trim()) == 0)
                            {
                                frmZipCodeDlg frm_ZipCodeDlg = new frmZipCodeDlg();
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

        private void CmbBzip_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            if (IsOtherStoreRecord) return;
            if (cmbBzip.Text.Trim() != "")
            {
                if (CheckZip(cmbBzip.Text.Trim()) == 0)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frmZipCodeDlg frm_ZipCodeDlg = new frmZipCodeDlg();
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

        private async void LinkSZip_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frmZipLookup frm_ZipLookup = new frmZipLookup();
            try
            {
                frm_ZipLookup.ShowDialog();
                if (frm_ZipLookup.DialogResult == true)
                {
                    if (await frm_ZipLookup.GetColoum1Value() != "")
                    {
                        cmbSzip.Text = await frm_ZipLookup.GetColoum1Value();
                        if (IsOtherStoreRecord) return;
                        if (cmbSzip.Text.Trim() != "")
                        {
                            if (CheckZip(cmbSzip.Text.Trim()) == 0)
                            {
                                frmZipCodeDlg frm_ZipCodeDlg = new frmZipCodeDlg();
                                try
                                {
                                    frm_ZipCodeDlg.ID = 0;
                                    frm_ZipCodeDlg.ForceAdd = true;
                                    frm_ZipCodeDlg.ForceZip = cmbSzip.Text.Trim();
                                    frm_ZipCodeDlg.ShowDialog();
                                    if (frm_ZipCodeDlg.DialogResult == true)
                                    {
                                        txtScity.Text = frm_ZipCodeDlg.ForceCity;
                                        txtSstate.Text = frm_ZipCodeDlg.ForceState;
                                        GeneralFunctions.SetFocus(txtScountry);
                                    }
                                    if (frm_ZipCodeDlg.DialogResult == false)
                                    {
                                        cmbSzip.Text = "";
                                        txtScity.Text = "";
                                        txtSstate.Text = "";
                                    }
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                GetZipData(cmbSzip, txtSstate, txtScity);
                                GeneralFunctions.SetFocus(txtScountry);
                            }
                        }
                        else
                        {
                            txtScity.Text = "";
                            txtSstate.Text = "";
                        }
                    }
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbSzip_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            if (IsOtherStoreRecord) return;
            if (cmbSzip.Text.Trim() != "")
            {
                if (CheckZip(cmbSzip.Text.Trim()) == 0)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frmZipCodeDlg frm_ZipCodeDlg = new frmZipCodeDlg();
                    try
                    {
                        frm_ZipCodeDlg.ID = 0;
                        frm_ZipCodeDlg.ForceAdd = true;
                        frm_ZipCodeDlg.ForceZip = cmbSzip.Text.Trim();
                        frm_ZipCodeDlg.ShowDialog();
                        if (frm_ZipCodeDlg.DialogResult == true)
                        {
                            txtScity.Text = frm_ZipCodeDlg.ForceCity;
                            txtSstate.Text = frm_ZipCodeDlg.ForceState;
                            GeneralFunctions.SetFocus(txtScountry);
                        }
                        if (frm_ZipCodeDlg.DialogResult == false)
                        {
                            cmbSzip.Text = "";
                            txtScity.Text = "";
                            txtSstate.Text = "";
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    GetZipData(cmbSzip, txtSstate, txtScity);
                    GeneralFunctions.SetFocus(txtBcountry);
                }
            }
            else
            {
                txtScity.Text = "";
                txtSstate.Text = "";
            }
        }

        private void LkupDiscount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbPrice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtDOB_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }




        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {
            bool pf = false;
            if ((blPOS) || (blAddFromPOS)) pf = true;
            if (!pf)
            {
                if (Settings.UseTouchKeyboardInAdmin == "N") return;
            }
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
            bool pf = false;
            if ((blPOS) || (blAddFromPOS)) pf = true;
            if (!pf)
            {
                if (Settings.UseTouchKeyboardInAdmin == "N") return;
            }
            else
            {
                if (Settings.UseTouchKeyboardInPOS == "N") return;
            }

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

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (tcCustomer.SelectedItem == tpPurchase)
            {
                if (gridView1.FocusedRowHandle < 0)
                    return;
                if (grdPurchase.SelectedItem == null)
                    return;
                //var RecordID = ReturnRowID().GetAwaiter().GetResult();

                DataRowView dataRowView = grdPurchase.SelectedItem as DataRowView;
                int RecordID = Convert.ToInt32(dataRowView.Row["InvNo"].ToString());
                UpdateReceiptCnt(RecordID);

                frmPOSInvoicePrintDlg _frmPOSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    _frmPOSInvoicePrintDlg.InvNo = RecordID;
                    _frmPOSInvoicePrintDlg.PrintType = "Reprint Receipt";
                    _frmPOSInvoicePrintDlg.ReprintCnt = this.GetReceiptCnt(RecordID);
                    _frmPOSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    _frmPOSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    //_frmPOSInvoicePrintDlg.Dispose();
                }
            }

            if (tcCustomer.SelectedItem == tpHouseAccount)
            {
                if (gridView7.FocusedRowHandle < 0)
                    return;
                if (grdHouseAccount.SelectedItem == null)
                    return;

                DataRowView dataRowView = grdHouseAccount.SelectedItem as DataRowView;
                string transType = dataRowView.Row["Description"].ToString();
                if (transType == "Opening Balance" || transType == "Adjustment")
                    return;
                int RecordID = Convert.ToInt32(dataRowView.Row["InvoiceNo"].ToString());
                UpdateReceiptCnt(RecordID);

                frmPOSInvoicePrintDlg _frmPOSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    _frmPOSInvoicePrintDlg.InvNo = RecordID;
                    _frmPOSInvoicePrintDlg.PrintType = "Reprint Receipt";
                    _frmPOSInvoicePrintDlg.ReprintCnt = GetReceiptCnt(RecordID);
                    _frmPOSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    _frmPOSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    //_frmPOSInvoicePrintDlg.Dispose();
                }
            }
    
        }

        #region MyRegion

        private int GetReceiptCnt(int invno)
        {
            var pO = new PosDataObject.POS();
            pO.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return pO.GetReceiptCount(invno);
        }

        private void UpdateReceiptCnt(int invno)
        {
            var pO = new PosDataObject.POS();
            pO.Connection = new SqlConnection(SystemVariables.ConnectionString);
            pO.LoginUserID = (SystemVariables.CurrentUserID);

            pO.FunctionButtonAccess = false;
            pO.ChangedByAdmin = (0);
            pO.UpdateReceiptCount(invno, 0);
        }

        public async Task<int> ReturnRowID(DevExpress.Xpf.Grid.GridControl gView, GridColumn gCol)
        {
            //int intRowID = -1;
            //int intRecID = -1;
            //if ((grdNotes.ItemsSource as ICollection).Count == 0) return intRecID;
            //if (gridView5.FocusedRowHandle < 0) return intRecID;
            //intRowID = gridView5.FocusedRowHandle;
            //intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdNotes, colID)));
            //return intRecID;

            int intRowID = -1;
            int intRecID = -1;
            //if ((grdNotes.ItemsSource as ICollection).Count > 1)
            //{

            //}
            if ((grdPurchase.ItemsSource as ICollection).Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdNotes, gCol)));
            return intRecID;


            //int num;
            //int focusedRowHandle = -1;
            //int num1 = -1;
            //if ((gView. as ICollection).Count == 0)
            //{
            //    num = num1;
            //}
            //else if (gView.FocusedRowHandle >= 0)
            //{
            //    focusedRowHandle = gView.FocusedRowHandle;
            //    num1 = GeneralFunctions.fnInt32(GeneralFunctions.GetCellValue(focusedRowHandle, gView, gCol));
            //    num = num1;
            //}
            //else
            //{
            //    num = num1;
            //}
            //return num;

            //return 0;
        }

        #endregion

        private bool IsValidStatement()
        {

            if (dtF.EditValue == null)
            {
                DocMessage.MsgInformation("Enter From Date");
                tcCustomer.SelectedIndex = 4;
                GeneralFunctions.SetFocus(dtF);
                return false;
            }
            if (dtT.EditValue == null)
            {
                DocMessage.MsgInformation("Enter To Date");
                tcCustomer.SelectedIndex = 4;
                GeneralFunctions.SetFocus(dtT);
                return false;
            }

            return true;
        }

        private void BtnStatement_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidStatement()) ExecuteStatement("Preview");
        }

        private void ExecuteStatement(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }

            
            DataSet ds = new DataSet();
            DataSet dsF = new DataSet();
            DataTable dtblgrp = new DataTable();
            dtblgrp.Columns.Add("HID", System.Type.GetType("System.Int32"));
            dtblgrp.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Account", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Amount", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Date", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("OB", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("LP", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("CB", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("OBDate", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("LPDate", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("TranType", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Qty", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Price", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("ExtPrice", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("TotalSale", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Type", System.Type.GetType("System.String"));



            //foreach (DataRow drC in dtblcust.Rows)
            ///{
                //int intID = 0;
                //if (Convert.ToBoolean(drC["Check"].ToString()))

                //{
                    //intID = GeneralFunctions.fnInt32(drC["ID"].ToString());
                    DataTable dtbl = new DataTable();
                    PosDataObject.Sales objSales = new PosDataObject.Sales();
                    objSales.Connection = SystemVariables.Conn;
                    dtbl = objSales.FetchCustomerHAHeaderData(intID, GeneralFunctions.fnDate(dtF.EditValue.ToString()), GeneralFunctions.fnDate(dtT.EditValue.ToString()));

                    if (dtbl.Rows.Count == 0)
                    {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl.Dispose();
                return;
                    }

                    DataTable dtblOP = new DataTable();
                    PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                    objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtblOP = objSales1.FetchCustomerHAOpening(intID);
                    string dtOpening = "";
                    string BalOpening = "0";
                    foreach (DataRow dr in dtblOP.Rows)
                    {
                        BalOpening = dr["Amount"].ToString();
                        if (GeneralFunctions.fnDouble(BalOpening) != 0)
                            dtOpening = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                    }
                    dtblOP.Dispose();

                    string CurBal = "0";

                    PosDataObject.POS objPOS = new PosDataObject.POS();
                    objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    CurBal = Convert.ToString(objPOS.FetchCustomerAcctPayBalance(intID));

                    DataTable dtblLP = new DataTable();
                    PosDataObject.POS objSales2 = new PosDataObject.POS();
                    objSales2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtblLP = objSales2.FetchCustomerLastAcctPay(intID);
                    string dtLP = "";
                    string BalLP = "0";
                    foreach (DataRow dr in dtblLP.Rows)
                    {
                        BalLP = dr["Amount"].ToString();
                        if (GeneralFunctions.fnDouble(BalLP) != 0)
                            dtLP = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                    }
                    dtblLP.Dispose();

                    OfflineRetailV2.Report.Misc.repHAStatement rep_HAStatement = new OfflineRetailV2.Report.Misc.repHAStatement();

                    GeneralFunctions.MakeReportWatermark(rep_HAStatement);
                    rep_HAStatement.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_HAStatement.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_HAStatement.DecimalPlace = Settings.DecimalPlace;

                    DataTable p = new DataTable("Parent");
                    p.Columns.Add("HID", System.Type.GetType("System.Int32"));
                    p.Columns.Add("CustID", System.Type.GetType("System.String"));
                    p.Columns.Add("Account", System.Type.GetType("System.String"));
                    p.Columns.Add("Amount", System.Type.GetType("System.String"));
                    p.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                    p.Columns.Add("Date", System.Type.GetType("System.String"));
                    p.Columns.Add("OB", System.Type.GetType("System.String"));
                    p.Columns.Add("LP", System.Type.GetType("System.String"));
                    p.Columns.Add("CB", System.Type.GetType("System.String"));
                    p.Columns.Add("OBDate", System.Type.GetType("System.String"));
                    p.Columns.Add("LPDate", System.Type.GetType("System.String"));
                    p.Columns.Add("TranType", System.Type.GetType("System.String"));

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        string strAcct = "";
                        if (dr["Company"].ToString().Trim() != "")
                        {
                            strAcct = dr["Company"].ToString().Trim();
                        }
                        if (dr["Customer"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Customer"].ToString().Trim();
                            }
                            else strAcct = dr["Customer"].ToString().Trim();
                        }

                        if (dr["Address1"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Address1"].ToString().Trim();
                            }
                            else strAcct = dr["Address1"].ToString().Trim();
                        }

                        if (dr["Address2"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Address2"].ToString().Trim();
                            }
                            else strAcct = dr["Address2"].ToString().Trim();
                        }
                        string CSZ = "";
                        if (dr["City"].ToString().Trim() != "") CSZ = dr["City"].ToString().Trim();
                        if (dr["State"].ToString().Trim() != "")
                        {
                            if (CSZ != "")
                            {
                                CSZ = CSZ + ", " + dr["State"].ToString().Trim() + "  ";
                            }
                            else CSZ = dr["State"].ToString().Trim() + "  ";
                        }

                        if (dr["Zip"].ToString().Trim() != "")
                        {
                            if (CSZ != "")
                            {
                                CSZ = CSZ + dr["Zip"].ToString().Trim();
                            }
                            else CSZ = dr["Zip"].ToString().Trim();
                        }
                        if (CSZ != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + CSZ;
                            }
                            else strAcct = CSZ;
                        }

                        if (dr["Email"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + "Email: " + dr["Email"].ToString().Trim();
                            }
                            else strAcct = "Email: " + dr["Email"].ToString().Trim();
                        }

                        DataRow r1 = p.NewRow();
                        r1["HID"] = dr["HID"].ToString();
                        r1["CustID"] = dr["CustID"].ToString();
                        r1["Account"] = strAcct;
                        r1["Amount"] = dr["Amount"].ToString();
                        r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                        r1["Date"] = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                        r1["OB"] = BalOpening;
                        r1["LP"] = BalLP;
                        r1["CB"] = CurBal;
                        r1["OBDate"] = dtOpening;
                        r1["LPDate"] = dtLP;
                        r1["TranType"] = dr["TranType"].ToString();
                        p.Rows.Add(r1);
                    }

                    DataTable dtbl1 = new DataTable();
                    PosDataObject.Sales objProduct1 = new PosDataObject.Sales();
                    objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl1 = objProduct1.FetchCustomerHADetailData(intID, GeneralFunctions.fnDate(dtF.EditValue.ToString()), GeneralFunctions.fnDate(dtT.EditValue.ToString()));

                    DataTable c = new DataTable("Child");
                    c.Columns.Add("HID", System.Type.GetType("System.Int32"));
                    c.Columns.Add("CustID", System.Type.GetType("System.String"));
                    c.Columns.Add("Account", System.Type.GetType("System.String"));
                    c.Columns.Add("Amount", System.Type.GetType("System.String"));
                    c.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                    c.Columns.Add("Date", System.Type.GetType("System.String"));
                    c.Columns.Add("OB", System.Type.GetType("System.String"));
                    c.Columns.Add("LP", System.Type.GetType("System.String"));
                    c.Columns.Add("CB", System.Type.GetType("System.String"));
                    c.Columns.Add("OBDate", System.Type.GetType("System.String"));
                    c.Columns.Add("LPDate", System.Type.GetType("System.String"));
                    c.Columns.Add("TranType", System.Type.GetType("System.String"));
                    c.Columns.Add("SKU", System.Type.GetType("System.String"));
                    c.Columns.Add("Description", System.Type.GetType("System.String"));
                    c.Columns.Add("Qty", System.Type.GetType("System.String"));
                    c.Columns.Add("Price", System.Type.GetType("System.String"));
                    c.Columns.Add("ExtPrice", System.Type.GetType("System.String"));
                    c.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                    c.Columns.Add("Type", System.Type.GetType("System.String"));


                    foreach (DataRow dr in dtbl1.Rows)
                    {
                        double crgamt = 0;

                        DataRow r1 = c.NewRow();
                        string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                        foreach (DataRow dr1 in p.Rows)
                        {

                            if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                            {
                                a1 = dr1["CustID"].ToString();
                                a2 = dr1["Account"].ToString();
                                a3 = dr1["Amount"].ToString();
                                a4 = dr1["Date"].ToString();
                                a5 = dr1["OB"].ToString();
                                a6 = dr1["LP"].ToString();
                                a7 = dr1["CB"].ToString();
                                a8 = dr1["OBDate"].ToString();
                                a9 = dr1["LPDate"].ToString();
                                a10 = dr1["TranType"].ToString();
                                a11 = dr1["HID"].ToString();
                                break;
                            }
                        }

                        crgamt = GeneralFunctions.fnDouble(a3);

                        r1["CustID"] = a1;
                        r1["Account"] = a2;
                        r1["Amount"] = crgamt.ToString();
                        r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                        r1["Date"] = a4;
                        r1["OB"] = a5;
                        r1["LP"] = a6;
                        r1["CB"] = a7;
                        r1["OBDate"] = a8;
                        r1["LPDate"] = a9;

                        r1["SKU"] = dr["SKU"].ToString();
                        r1["Description"] = dr["Description"].ToString();
                        r1["Qty"] = dr["Qty"].ToString();
                        r1["Price"] = dr["Price"].ToString();
                        r1["ExtPrice"] = dr["ExtPrice"].ToString();
                        r1["TotalSale"] = dr["TotalSale"].ToString();
                        r1["Type"] = dr["ProductType"].ToString() == "W" ? "W" : "N"; // comment // "N";
                        r1["HID"] = a11;
                        c.Rows.Add(r1);

                        dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r1["CustID"].ToString(),
                                                    r1["Account"].ToString(),
                                                    r1["Amount"].ToString(),
                                                    r1["InvoiceNo"].ToString(),
                                                    r1["Date"].ToString(),
                                                    r1["OB"].ToString(),
                                                    r1["LP"].ToString(),
                                                    r1["CB"].ToString(),
                                                    r1["OBDate"].ToString(),
                                                    r1["LPDate"].ToString(),
                                                    a10,
                                                    r1["SKU"].ToString(),
                                                    r1["Description"].ToString(),
                                                    r1["Qty"].ToString(),
                                                    r1["Price"].ToString(),
                                                    r1["ExtPrice"].ToString(),
                                                    r1["TotalSale"].ToString(),
                                                    r1["Type"].ToString()});

                    }


                    string previnv = "";
                    foreach (DataRow dr in dtbl1.Rows)
                    {

                        double crgamt = 0;

                        DataRow r1 = c.NewRow();
                        string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                        foreach (DataRow dr1 in p.Rows)
                        {

                            if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                            {
                                a1 = dr1["CustID"].ToString();
                                a2 = dr1["Account"].ToString();
                                a3 = dr1["Amount"].ToString();
                                a4 = dr1["Date"].ToString();
                                a5 = dr1["OB"].ToString();
                                a6 = dr1["LP"].ToString();
                                a7 = dr1["CB"].ToString();
                                a8 = dr1["OBDate"].ToString();
                                a9 = dr1["LPDate"].ToString();
                                a10 = dr1["TranType"].ToString();
                                a11 = dr1["HID"].ToString();
                                break;
                            }
                        }

                        crgamt = GeneralFunctions.fnDouble(a3);

                        if ((previnv == "") || (previnv != dr["InvoiceNo"].ToString()))
                        {
                            previnv = dr["InvoiceNo"].ToString();
                            if (GeneralFunctions.fnDouble(dr["Tax1"].ToString()) != 0)
                            {

                                DataRow r2 = c.NewRow();

                                r2["CustID"] = a1;
                                r2["Account"] = a2;
                                r2["Amount"] = crgamt.ToString();
                                r2["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r2["Date"] = a4;
                                r2["OB"] = a5;
                                r2["LP"] = a6;
                                r2["CB"] = a7;
                                r2["OBDate"] = a8;
                                r2["LPDate"] = a9;
                                r2["SKU"] = "";
                                r2["Description"] = dr["TaxName1"].ToString();
                                r2["Qty"] = "0";
                                r2["Price"] = "0";
                                r2["ExtPrice"] = dr["Tax1"].ToString();
                                r2["TotalSale"] = dr["TotalSale"].ToString();
                                r2["Type"] = "A";
                                r2["HID"] = a11;
                                c.Rows.Add(r2);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r2["CustID"].ToString(),
                                                    r2["Account"].ToString(),
                                                    r2["Amount"].ToString(),
                                                    r2["InvoiceNo"].ToString(),
                                                    r2["Date"].ToString(),
                                                    r2["OB"].ToString(),
                                                    r2["LP"].ToString(),
                                                    r2["CB"].ToString(),
                                                    r2["OBDate"].ToString(),
                                                    r2["LPDate"].ToString(),
                                                    a10,
                                                    r2["SKU"].ToString(),
                                                    r2["Description"].ToString(),
                                                    r2["Qty"].ToString(),
                                                    r2["Price"].ToString(),
                                                    r2["ExtPrice"].ToString(),
                                                    r2["TotalSale"].ToString(),
                                                    r2["Type"].ToString()});
                            }

                            if (GeneralFunctions.fnDouble(dr["Tax2"].ToString()) != 0)
                            {

                                DataRow r3 = c.NewRow();

                                r3["CustID"] = a1;
                                r3["Account"] = a2;
                                r3["Amount"] = crgamt.ToString();
                                r3["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r3["Date"] = a4;
                                r3["OB"] = a5;
                                r3["LP"] = a6;
                                r3["CB"] = a7;
                                r3["OBDate"] = a8;
                                r3["LPDate"] = a9;
                                r3["SKU"] = "";
                                r3["Description"] = dr["TaxName2"].ToString();
                                r3["Qty"] = "0";
                                r3["Price"] = "0";
                                r3["ExtPrice"] = dr["Tax2"].ToString();
                                r3["TotalSale"] = dr["TotalSale"].ToString();
                                r3["Type"] = "A";
                                r3["HID"] = a11;
                                c.Rows.Add(r3);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r3["CustID"].ToString(),
                                                    r3["Account"].ToString(),
                                                    r3["Amount"].ToString(),
                                                    r3["InvoiceNo"].ToString(),
                                                    r3["Date"].ToString(),
                                                    r3["OB"].ToString(),
                                                    r3["LP"].ToString(),
                                                    r3["CB"].ToString(),
                                                    r3["OBDate"].ToString(),
                                                    r3["LPDate"].ToString(),
                                                    a10,
                                                    r3["SKU"].ToString(),
                                                    r3["Description"].ToString(),
                                                    r3["Qty"].ToString(),
                                                    r3["Price"].ToString(),
                                                    r3["ExtPrice"].ToString(),
                                                    r3["TotalSale"].ToString(),
                                                    r3["Type"].ToString()});

                            }

                            if (GeneralFunctions.fnDouble(dr["Tax3"].ToString()) != 0)
                            {

                                DataRow r4 = c.NewRow();

                                r4["CustID"] = a1;
                                r4["Account"] = a2;
                                r4["Amount"] = crgamt.ToString();
                                r4["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r4["Date"] = a4;
                                r4["OB"] = a5;
                                r4["LP"] = a6;
                                r4["CB"] = a7;
                                r4["OBDate"] = a8;
                                r4["LPDate"] = a9;
                                r4["SKU"] = "";
                                r4["Description"] = dr["TaxName3"].ToString();
                                r4["Qty"] = "0";
                                r4["Price"] = "0";
                                r4["ExtPrice"] = dr["Tax3"].ToString();
                                r4["TotalSale"] = dr["TotalSale"].ToString();
                                r4["Type"] = "A";
                                r4["HID"] = a11;
                                c.Rows.Add(r4);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r4["CustID"].ToString(),
                                                    r4["Account"].ToString(),
                                                    r4["Amount"].ToString(),
                                                    r4["InvoiceNo"].ToString(),
                                                    r4["Date"].ToString(),
                                                    r4["OB"].ToString(),
                                                    r4["LP"].ToString(),
                                                    r4["CB"].ToString(),
                                                    r4["OBDate"].ToString(),
                                                    r4["LPDate"].ToString(),
                                                    a10,
                                                    r4["SKU"].ToString(),
                                                    r4["Description"].ToString(),
                                                    r4["Qty"].ToString(),
                                                    r4["Price"].ToString(),
                                                    r4["ExtPrice"].ToString(),
                                                    r4["TotalSale"].ToString(),
                                                    r4["Type"].ToString()});
                            }

                            if (GeneralFunctions.fnDouble(dr["Discount"].ToString()) != 0)
                            {
                                DataRow r5 = c.NewRow();
                                r5["CustID"] = a1;
                                r5["Account"] = a2;
                                r5["Amount"] = crgamt.ToString();
                                r5["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r5["Date"] = a4;
                                r5["OB"] = a5;
                                r5["LP"] = a6;
                                r5["CB"] = a7;
                                r5["OBDate"] = a8;
                                r5["LPDate"] = a9;
                                r5["SKU"] = "";
                                if (dr["DiscountReason"].ToString() != "")
                                    r5["Description"] = "Discount : " + dr["DiscountReason"].ToString();
                                else
                                    r5["Description"] = "Discount : ";
                                r5["Qty"] = "0";
                                r5["Price"] = "0";
                                r5["ExtPrice"] = dr["Discount"].ToString();
                                r5["TotalSale"] = dr["TotalSale"].ToString();
                                r5["Type"] = "A";
                                r5["HID"] = a11;
                                c.Rows.Add(r5);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r5["CustID"].ToString(),
                                                    r5["Account"].ToString(),
                                                    r5["Amount"].ToString(),
                                                    r5["InvoiceNo"].ToString(),
                                                    r5["Date"].ToString(),
                                                    r5["OB"].ToString(),
                                                    r5["LP"].ToString(),
                                                    r5["CB"].ToString(),
                                                    r5["OBDate"].ToString(),
                                                    r5["LPDate"].ToString(),
                                                    a10,
                                                    r5["SKU"].ToString(),
                                                    r5["Description"].ToString(),
                                                    r5["Qty"].ToString(),
                                                    r5["Price"].ToString(),
                                                    r5["ExtPrice"].ToString(),
                                                    r5["TotalSale"].ToString(),
                                                    r5["Type"].ToString()});
                            }
                        }

                    }


                    foreach (DataRow dr1 in p.Rows)
                    {

                        if ((dr1["InvoiceNo"].ToString() == "0") && (dr1["TranType"].ToString() == "1"))
                        {
                            DataRow r6 = c.NewRow();
                            string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                            a1 = dr1["CustID"].ToString();
                            a2 = dr1["Account"].ToString();
                            a3 = dr1["Amount"].ToString();
                            a4 = dr1["Date"].ToString();
                            a5 = dr1["OB"].ToString();
                            a6 = dr1["LP"].ToString();
                            a7 = dr1["CB"].ToString();
                            a8 = dr1["OBDate"].ToString();
                            a9 = dr1["LPDate"].ToString();
                            a10 = dr1["TranType"].ToString();
                            a11 = dr1["HID"].ToString();

                            r6["HID"] = a11;
                            r6["CustID"] = a1;
                            r6["Account"] = a2;
                            r6["Amount"] = a3;
                            r6["InvoiceNo"] = "";
                            r6["Date"] = a4;
                            r6["OB"] = a5;
                            r6["LP"] = a6;
                            r6["CB"] = a7;
                            r6["OBDate"] = a8;
                            r6["LPDate"] = a9;
                            r6["SKU"] = "Adjustment";
                            r6["Description"] = "";
                            r6["Qty"] = "";
                            r6["Price"] = "";
                            r6["ExtPrice"] = a3;
                            r6["TotalSale"] = a3;
                            r6["Type"] = "A";
                            c.Rows.Add(r6);

                            dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r6["CustID"].ToString(),
                                                    r6["Account"].ToString(),
                                                    r6["Amount"].ToString(),
                                                    r6["InvoiceNo"].ToString(),
                                                    r6["Date"].ToString(),
                                                    r6["OB"].ToString(),
                                                    r6["LP"].ToString(),
                                                    r6["CB"].ToString(),
                                                    r6["OBDate"].ToString(),
                                                    r6["LPDate"].ToString(),
                                                    a10,
                                                    r6["SKU"].ToString(),
                                                    r6["Description"].ToString(),
                                                    r6["Qty"].ToString(),
                                                    r6["Price"].ToString(),
                                                    r6["ExtPrice"].ToString(),
                                                    r6["TotalSale"].ToString(),
                                                    r6["Type"].ToString()});
                        }
                    }


                    //ds.Tables.Add(p);
                    //ds.Tables.Add(c);


                //}





            //}





            if (dtblgrp.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtblgrp.Dispose();
                return;
            }

            /*DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["InvoiceNo"],
            ds.Tables["Child"].Columns["InvoiceNo"]);
            ds.Relations.Add(relation);*/
            //relation.Nested = true;

            //rep_HAStatement.GroupHeader1.GroupFields.Add(rep_HAStatement.CreateGroupField("Parent.CustID"));
            OfflineRetailV2.Report.Misc.repHAGroupStatement rep_HAGroupStatement = new OfflineRetailV2.Report.Misc.repHAGroupStatement();
            GeneralFunctions.MakeReportWatermark(rep_HAGroupStatement);
            rep_HAGroupStatement.Report.DataSource = dtblgrp;
            rep_HAGroupStatement.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_HAGroupStatement.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_HAGroupStatement.rHDate.Text = "from" + " " + GeneralFunctions.fnDate(dtF.EditValue.ToString()).ToString(Settings.DateFormat) + " " + "to" + " " + GeneralFunctions.fnDate(dtT.EditValue.ToString()).ToString(Settings.DateFormat);
            rep_HAGroupStatement.GroupHeader3.GroupFields.Add(rep_HAGroupStatement.CreateGroupField("CustID"));

            rep_HAGroupStatement.rAccount.DataBindings.Add("Text", dtblgrp, "CustID");
            rep_HAGroupStatement.rAddress.DataBindings.Add("Text", dtblgrp, "Account");


            rep_HAGroupStatement.rOBDate.DataBindings.Add("Text", dtblgrp, "OBDate");
            rep_HAGroupStatement.rOBAmt.DataBindings.Add("Text", dtblgrp, "OB");
            rep_HAGroupStatement.rLPDate.DataBindings.Add("Text", dtblgrp, "LPDate");
            rep_HAGroupStatement.rLPAmt.DataBindings.Add("Text", dtblgrp, "LP");
            rep_HAGroupStatement.rCBAmt.DataBindings.Add("Text", dtblgrp, "CB");

            rep_HAGroupStatement.GroupHeader2.GroupFields.Add(rep_HAGroupStatement.CreateGroupField("HID"));
            rep_HAGroupStatement.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            rep_HAGroupStatement.rInvoice.DataBindings.Add("Text", dtblgrp, "InvoiceNo");
            rep_HAGroupStatement.rDate.DataBindings.Add("Text", dtblgrp, "Date");

            rep_HAGroupStatement.rID.DataBindings.Add("Text", dtblgrp, "Type");
            rep_HAGroupStatement.rSKU.DataBindings.Add("Text", dtblgrp, "SKU");
            rep_HAGroupStatement.rProduct.DataBindings.Add("Text", dtblgrp, "Description");
            rep_HAGroupStatement.rQty.DataBindings.Add("Text", dtblgrp, "Qty");
            rep_HAGroupStatement.rPrice.DataBindings.Add("Text", dtblgrp, "Price");
            rep_HAGroupStatement.rExtPrice.DataBindings.Add("Text", dtblgrp, "ExtPrice");

            rep_HAGroupStatement.rTot1.DataBindings.Add("Text", dtblgrp, "TotalSale");
            rep_HAGroupStatement.rTot2.DataBindings.Add("Text", dtblgrp, "Amount");
            rep_HAGroupStatement.rCB.DataBindings.Add("Text", dtblgrp, "CB");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_HAGroupStatement.PrinterName = Settings.ReportPrinterName;
                    rep_HAGroupStatement.CreateDocument();
                    rep_HAGroupStatement.PrintingSystem.ShowMarginsWarning = false;
                    rep_HAGroupStatement.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_HAGroupStatement.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_HAGroupStatement;
                    window.ShowDialog();
                }
                finally
                {
                    rep_HAGroupStatement.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();
                    dtblgrp.Dispose();
                }

            }

            
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
