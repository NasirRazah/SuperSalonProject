using DevExpress.Xpf.Editors;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSRepairInfoDlg.xaml
    /// </summary>
    public partial class frmPOSRepairInfoDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        PopupKeyBoard userkybd;
        PopupKeyBoard_es userkybd_es;
        private string strCalledFor;
        private int intNewID;
        private int intID;
        private int intCustomerID;
        private string strCustName;
        private DataTable retdtbl;
        private double dblRepairAmount;
        frm_CustomerBrw frm_CustomerBrw;
        private bool blDisableDeposit = false;

        private bool bLoading = false;

        public bool DisableDeposit
        {
            get { return blDisableDeposit; }
            set { blDisableDeposit = value; }
        }

        public double RepairAmount
        {
            get { return dblRepairAmount; }
            set { dblRepairAmount = value; }
        }

        public DataTable pdtbl
        {
            get { return retdtbl; }
            set { retdtbl = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public string CustName
        {
            get { return strCustName; }
            set { strCustName = value; }
        }

        public string CalledFor
        {
            get { return strCalledFor; }
            set { strCalledFor = value; }
        }



        private bool blchangebrwfield;

        public bool changebrwfield
        {
            get { return blchangebrwfield; }
            set { blchangebrwfield = value; }
        }

        private string prevmph = "";
        private string prevhph = "";
        private string prevlname = "";
        private string prevfname = "";
        private string prevadd = "";
        public frmPOSRepairInfoDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void dtNotified_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

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


        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
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

        private void simpleButton2_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void simpleButton1_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Settings.POS_Culture.Name == "en-US")
            {
                userkybd.AlphaKyBrd = true;
            }
            if (Data.Settings.POS_Culture.Name == "es-ES")
            {
                userkybd_es.AlphaKyBrd = true;
            }
        }
        DevExpress.Xpf.Editors.TextEdit txtZip = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtCity = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtState = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtCountry = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtPhone = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtPhone1 = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtPhone2 = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtAdd1 = new DevExpress.Xpf.Editors.TextEdit();
        DevExpress.Xpf.Editors.TextEdit txtAdd2 = new DevExpress.Xpf.Editors.TextEdit();

        private async void simpleButton4_Click(object sender, RoutedEventArgs e)
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
                        txtZip.Text = await frm_ZipLookup.GetColoum1Value();
                        txtZip_Leave(sender, e);
                    }
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }
        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                if (CheckZip(txtZip.Text.Trim()) == 0)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frmZipCodeDlg frm_ZipCodeDlg = new frmZipCodeDlg();
                    try
                    {
                        //frm_ZipCodeDlg.CallFromPOS = true;
                        frm_ZipCodeDlg.ID = 0;
                        frm_ZipCodeDlg.ForceAdd = true;
                        frm_ZipCodeDlg.ForceZip = txtZip.Text.Trim();
                        frm_ZipCodeDlg.ShowDialog();
                        if (frm_ZipCodeDlg.DialogResult ==true)
                        {
                            txtCity.Text = frm_ZipCodeDlg.ForceCity;
                            txtState.Text = frm_ZipCodeDlg.ForceState;
                            GeneralFunctions.SetFocus(txtCountry);
                        }
                        if (frm_ZipCodeDlg.DialogResult == false)
                        {
                            txtZip.Text = "";
                            txtCity.Text = "";
                            txtState.Text = "";
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    GetZipData(txtZip, txtState, txtCity);
                }
            }
            else
            {
                txtCity.Text = "";
                txtState.Text = "";
            }
        }
        private void simpleButton3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidAll())
            {
                string dt1 = "";
                string dt2 = "";

                if (dtDelivery.EditValue == null) dt1 = ""; else dt1 = dtDelivery.EditValue.ToString();
                if (dtNotified.EditValue == null) dt2 = ""; else dt2 = dtNotified.EditValue.ToString();

                retdtbl.Rows.Add(new object[] { numAdvance.Text,dtIssued.EditValue.ToString(),dt1,dt2,txtProblem.Text.Trim(),txtRepair.Text.Trim(),txtRemarks.Text.Trim(),
                                                txtItemTag.Text.Trim(),txtItemSl.Text.Trim(),cmbFindUs.Text.Trim(),chkEmail.IsChecked==true ? (txtEmail.Text.Trim() != "" ? "Y" : "N") : "N"});

                if ((prevmph != txtPhone.Text.Trim()) || (prevhph != txtPhone1.Text.Trim()) || (prevlname != txtLastName.Text.Trim())
                    || (prevfname != txtFirstName.Text.Trim()) || (prevadd != txtAdd1.Text.Trim())) blchangebrwfield = true;

                if ((strCalledFor == "Add") || (strCalledFor == "Add with Phone") || (strCalledFor == "Edit Customer") ||
                    (strCalledFor == "Edit") || (strCalledFor == "Issue"))
                {
                    if (strCalledFor == "Edit Customer")
                    {
                        if (SaveData())
                        {
                            if (blchangebrwfield)
                            {
                                frm_CustomerBrw.frm_CustomerBrwUC.storecd = Settings.StoreCode;
                                //Todo: frmCustomerBrw.Instance.FetchData(BrowseForm.cmbFilter.EditValue.ToString());
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    if ((strCalledFor == "Add with Phone") || (strCalledFor == "Add"))
                    {
                        if (SaveData())
                        {
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (strCalledFor == "Edit")
                    {
                        if (SaveData())
                        {
                            if (intID > 0)
                            {
                                PosDataObject.POS objp = new PosDataObject.POS();
                                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                string strval = "";
                                objp.ID = intID;
                                objp.LoginUserID = SystemVariables.CurrentUserID;
                                objp.RepairProblem = txtProblem.Text.Trim();
                                objp.RepairNotes = txtRepair.Text.Trim();
                                objp.RepairRemarks = txtRemarks.Text.Trim();
                                objp.RepairItemName = txtItemTag.Text.Trim();
                                objp.RepairItemSL = txtItemSl.Text.Trim();
                                objp.RepairFindUs = cmbFindUs.Text.Trim();
                                if (dtDelivery.EditValue == null) objp.RepairDeliveryDate = Convert.ToDateTime(null);
                                else objp.RepairDeliveryDate = dtDelivery.DateTime;
                                if (dtNotified.EditValue == null) objp.RepairNotifiedDate = Convert.ToDateTime(null);
                                else objp.RepairNotifiedDate = dtNotified.DateTime;
                                strval = objp.UpdateRepairInfo();
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (strCalledFor == "Issue")
                    {
                        if (SaveData())
                        {
                        }
                    }
                }
                ResMan.closeKeyboard();
                CloseKeyboards();
                DialogResult = true;
            }
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            objCustomer.LoginUserID = SystemVariables.CurrentUserID;

            objCustomer.AutoCustomer = Settings.AutoCustomer;
            objCustomer.CustomerID = txtID.Text.Trim();
            objCustomer.LastName = txtLastName.Text.Trim();
            objCustomer.FirstName = txtFirstName.Text.Trim();
            objCustomer.Company = txtPhone2.Text.Trim();

            objCustomer.ShipAddress1 = txtAdd1.Text.Trim();
            objCustomer.ShipAddress2 = txtAdd2.Text.Trim();
            objCustomer.ShipCity = txtCity.Text.Trim();
            objCustomer.ShipState = txtState.Text.Trim();
            objCustomer.ShipCountry = txtCountry.Text.Trim();
            objCustomer.ShipZip = txtZip.Text.Trim();

            objCustomer.WorkPhone = txtPhone.Text.Trim();
            objCustomer.HomePhone = txtPhone1.Text.Trim();
            objCustomer.EMail = txtEmail.Text.Trim();

            objCustomer.TaxExempt = "N";
            objCustomer.DiscountLevel = "A";
            objCustomer.AROpeningBalance = 0;
            objCustomer.ThisStoreCode = Settings.StoreCode;
            objCustomer.ID = intCustomerID;
            strError = objCustomer.PostDataFromPOS();
            intNewID = objCustomer.ID;

            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidAll()
        {
            if (txtID.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Customer_ID ,Properties.Resources.Repair_information , MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtID);
                return false;
            }

            /*if (txtPhone.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Translation.SetMultilingualTextInCodes("Enter Phone No.", "Repair information", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtPhone);
                return false;
            }*/
            if (txtLastName.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Customer_Last_Name, Properties.Resources.Repair_information, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtLastName);
                return false;
            }



            /*if (txtAdd1.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Translation.SetMultilingualTextInCodes("Enter Address 1", "Repair information", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtAdd1);
                return false;
            }

            if (txtZip.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Translation.SetMultilingualTextInCodes("Enter ZIP Code", "Repair information", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtZip);
                return false;
            }

            if (txtCity.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Translation.SetMultilingualTextInCodes("Enter City", "Repair information", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtCity);
                return false;
            }
            */
            if (intCustomerID == 0)
            {
                if (Settings.AutoCustomer != "Y")
                {
                    if (DuplicateCount() == 1)
                    {
                        new MessageBoxWindow().Show(Properties.Resources.Duplicate_Customer_ID, Properties.Resources.Repair_information, MessageBoxButton.OK, MessageBoxImage.Information);
                        GeneralFunctions.SetFocus(txtID);
                        return false;
                    }
                }
            }

            if (dtDelivery.EditValue != null)
            {
                if (dtDelivery.DateTime.Date < dtIssued.DateTime.Date)
                {
                    new MessageBoxWindow().Show(Properties.Resources.Expected_Delivery_Date_can_not_be_before_Date_In , Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            if (strCalledFor == "Issue")
            {
                if (double.Parse(numRepair.Text )> 0)
                {
                    if (double.Parse(numAdvance.Text) >= double.Parse(numRepair.Text))
                    {
                        new MessageBoxWindow().Show(Properties.Resources.Advance_amount_should_be_less_than_total_repair_value , Properties.Resources.Repair_information, MessageBoxButton.OK, MessageBoxImage.Information);
                        GeneralFunctions.SetFocus(numAdvance);
                        return false;
                    }
                }
            }



            return true;
        }

        private int DuplicateCount()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objCustomer.DuplicateCount(txtID.Text.Trim(), Settings.StoreCode);
        }

        private int CheckZip(string zip)
        {
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            return objZip.DuplicateCount(zip);
        }

        private void GetZipData(DevExpress.Xpf.Editors.TextEdit zip, DevExpress.Xpf.Editors.TextEdit state, DevExpress.Xpf.Editors.TextEdit city)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            dtbl = objZip.ZIPData(txtZip.Text.Trim());
            foreach (DataRow dr in dtbl.Rows)
            {
                txtState.Text = dr["STATE"].ToString();
                txtCity.Text = dr["CITY"].ToString();
            }
            dtbl.Dispose();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
        private void FetchInfoFromRepairInvoice()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            dtbl = objp.FetchRepairInfo(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                if (dr["TransDate"].ToString() != "") dtIssued.DateTime = GeneralFunctions.fnDate(dr["TransDate"].ToString());
                if (dr["ExpectedDeliveryDate"].ToString() != "") dtDelivery.DateTime = GeneralFunctions.fnDate(dr["ExpectedDeliveryDate"].ToString()); else dtDelivery.EditValue = null;
                if (dr["NotifiedDate"].ToString() != "") dtNotified.DateTime = GeneralFunctions.fnDate(dr["NotifiedDate"].ToString()); else dtNotified.EditValue = null;
                txtProblem.Text = dr["RepairProblem"].ToString();
                txtRepair.Text = dr["RepairNotes"].ToString();
                txtRemarks.Text = dr["RepairRemarks"].ToString();
                numRepair.Text = GeneralFunctions.fnDouble(dr["RepairAmount"].ToString()).ToString();
                numAdvance.Text = GeneralFunctions.fnDouble(dr["RepairAdvanceAmount"].ToString()).ToString();
                txtItemTag.Text = dr["RepairItemName"].ToString();
                txtItemSl.Text = dr["RepairItemSlNo"].ToString();
                cmbFindUs.Text = dr["RepairFindUs"].ToString();
            }
            dtbl.Dispose();
        }

        private void FetchAddressFromCustomerData()
        {
            DataTable dtbl = new DataTable();
            dtbl = FetchFromCustomer();
            if (dtbl.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    txtID.Text = dr["CustomerID"].ToString();
                    txtPhone.Text = dr["PhoneNo"].ToString();
                    txtLastName.Text = dr["LastName"].ToString();
                    txtFirstName.Text = dr["FirstName"].ToString();
                    txtAdd1.Text = dr["Address1"].ToString();
                    txtAdd2.Text = dr["Address2"].ToString();
                    txtCity.Text = dr["City"].ToString();
                    txtState.Text = dr["State"].ToString();
                    txtCountry.Text = dr["Country"].ToString();
                    txtZip.Text = dr["Zip"].ToString();
                    txtPhone1.Text = dr["PhoneNo1"].ToString();
                    txtPhone2.Text = dr["PhoneNo2"].ToString();
                    txtMobile.Text = dr["Mobile"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    break;
                }
                prevmph = txtPhone.Text;
                prevhph = txtPhone1.Text;
                prevlname = txtLastName.Text;
                prevfname = txtFirstName.Text;
                prevadd = txtAdd1.Text;
            }
            dtbl.Dispose();
        }

        private DataTable FetchFromCustomer()
        {
            PosDataObject.POS objpos2 = new PosDataObject.POS();
            objpos2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos2.FetchAddressFromCustomerData(intCustomerID);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.Repair_information;
            retdtbl = new DataTable();
            retdtbl.Columns.Add("AdvanceAmount", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("DateIn", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("ExpectedDeliveryDate", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("NotifiedDate", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("ProblemDesc", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("RepairDesc", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("Remarks", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("ItemTag", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("ItemSL", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("RepairFindUs", System.Type.GetType("System.String"));
            retdtbl.Columns.Add("AutoEmail", System.Type.GetType("System.String"));

            if (strCalledFor == "Add")
            {
                Title.Text = "New Customer";
                txtID.IsEnabled = true;
                if (Settings.AutoCustomer == "Y")
                {
                    txtID.Text = "AUTO";
                    txtID.IsReadOnly = true;
                    txtID.Focusable = false;
                }
            }

            if (strCalledFor == "Add with Phone")
            {
                Title.Text = "New Customer";
                txtID.IsEnabled = true;
                if (Settings.AutoCustomer == "Y")
                {
                    txtID.Text = "AUTO";
                    txtID.IsReadOnly = true;
                    txtID.Focusable = false;
                }
            }

            if (strCalledFor == "Edit Customer")
            {
                Title.Text = "Update Customer";
                txtID.IsEnabled = false;
            }

            if (strCalledFor == "Edit")
            {
                Title.Text = "Update Repair Information";
                txtID.IsEnabled = false;
                numRepair.Text = dblRepairAmount.ToString();
            }

            if (strCalledFor == "Issue")
            {
                if (blDisableDeposit)
                {
                    numAdvance.IsReadOnly = true;
                    numAdvance.Focusable = false;
                }
            }
            else
            {
                numAdvance.IsReadOnly = true;
                numAdvance.Focusable = false;
            }

            if (strCalledFor == "Issue")
            {
                Title.Text = "Repair Information";
                txtID.IsEnabled = false;
                GeneralFunctions.SetFocus(numAdvance);
                numRepair.Text = dblRepairAmount.ToString();
                dtIssued.DateTime = DateTime.Now;
                if (Settings.REHost != "")
                {
                    chkEmail.Visibility = Visibility.Visible;
                    chkEmail.IsChecked = true;
                }
            }

            if (strCalledFor == "View")
            {
                Title.Text = "Repair Information";
                txtID.IsEnabled = false;
                txtPhone.IsReadOnly = true;
                txtPhone1.IsReadOnly = true;
                txtPhone2.IsReadOnly = true;
                txtLastName.IsReadOnly = true;
                txtFirstName.IsReadOnly = true;
                txtAdd1.IsReadOnly = true;
                txtAdd2.IsReadOnly = true;
                txtCity.IsReadOnly = true;
                txtZip.IsReadOnly = true;
                txtState.IsReadOnly = true;
                txtCountry.IsReadOnly = true;
                txtPhone.Focusable = false;
                txtPhone1.Focusable = false;
                txtPhone2.Focusable = false;
                txtLastName.Focusable = false;
                txtFirstName.Focusable = false;
                txtAdd1.Focusable = false;
                txtAdd2.Focusable = false;
                txtCity.Focusable = false;
                txtZip.Focusable = false;
                txtState.Focusable = false;
                txtCountry.Focusable = false;

                numAdvance.IsReadOnly = true;
                numAdvance.Focusable = false;
                lbRemarks.Visibility = Visibility.Visible;
                txtRemarks.Visibility = Visibility.Visible;
            }

            if (intID == 0)
            {
                if (intCustomerID > 0)
                {
                    FetchAddressFromCustomerData();
                }
                else
                {
                    txtPhone.Text = strCustName;
                }
            }
            else
            {
                FetchAddressFromCustomerData();
                FetchInfoFromRepairInvoice();
            }

            if (strCalledFor == "Edit")
            {
                if (intID == 0)
                {
                    //txtRemarks.Text = DR();
                    //GeneralFunctions.SetFocus(txtRemarks);
                }
            }

            GeneralFunctions.SetFocus(txtItemTag);


            /*if (Settings.POS_Culture.Name == "en-US")
            {
                userkybd = new PopupKeyBoard();
                userkybd.btn4.Text = userkybd.btn4.Text.Replace("$", SystemVariables.CurrencySymbol);
                userkybd.Name = "keybrd";
                userkybd.DirectKey = true;
                userkybd.Dock = DockStyle.Top;
                panel1.Child = userkybd;
            }

            if (Settings.POS_Culture.Name == "es-ES")
            {
                userkybd_es = new PopupKeyBoard_es();
                userkybd_es.btn4.Text = userkybd_es.btn4.Text.Replace("$", SystemVariables.CurrencySymbol);
                userkybd_es.Name = "keybrd";
                userkybd_es.DirectKey = true;
                userkybd_es.Dock = DockStyle.Top;
                panel1.Child = userkybd_es;
            }*/
        }

        private void dtDelivery_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void NumAdvance_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            numDue.Text = (GeneralFunctions.fnDouble(numRepair.Text) - GeneralFunctions.fnDouble(numAdvance.Text)).ToString();
        }

        private void CmbFindUs_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtIssued_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
