using OfflineRetailV2.Data;
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
using System.Data;
using System.Data.SqlClient;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSPaidInDlg.xaml
    /// </summary>
    public partial class frm_POSGiftAidDlg : Window
    {
        NumKeyboard nkybrd;
        FullKeyboard fkybrd;
        bool IsAboutNumKybrdOpen = false;
        bool IsAboutFullKybrdOpen = false;

        private int intCustomerID;

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        private string strCustomerName;
        private string strCustomerAddress;

        public string CustomerName
        {
            get { return strCustomerName; }
            set { strCustomerName = value; }
        }

        public string CustomerAddress
        {
            get { return strCustomerAddress; }
            set { strCustomerAddress = value; }
        }

        private double dblPrice;
        public double Price
        {
            get { return dblPrice; }
            set { dblPrice = value; }
        }

        public frm_POSGiftAidDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSPaidInDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSPaidInDlg_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerDetails();
            SetDecimalPlace();
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();

        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void LoadCustomerDetails()
        {
            if (intCustomerID > 0)
            {
                PosDataObject.Customer obj = new PosDataObject.Customer();
                obj.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = obj.ShowRecord(intCustomerID);

                string cust_firstname = "";
                string cust_lastname = "";

                string cust_company = "";
                string cust_add1 = "";
                string cust_add2 = "";
                string cust_city = "";
                string cust_state = "";
                string cust_zip = "";
                string cust_country = "";

                string strReportHeader = "";
                string strcitystatezip = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    cust_firstname = dr["FirstName"].ToString().Trim();
                    cust_lastname = dr["LastName"].ToString().Trim();
                    cust_company = dr["Company"].ToString().Trim();
                    cust_add1 = dr["Address1"].ToString().Trim();
                    cust_add2 = dr["Address2"].ToString().Trim();
                    cust_city = dr["City"].ToString().Trim();
                    cust_state = dr["State"].ToString().Trim();
                    cust_zip = dr["Zip"].ToString().Trim();
                    cust_country = dr["Country"].ToString().Trim();
                }

                if (cust_lastname == "")
                {
                    txtName.Text = cust_firstname;
                }
                else
                {
                    txtName.Text = cust_firstname + " " + cust_lastname;
                }

                if (cust_company != "")
                {
                    strReportHeader = strReportHeader + cust_company + "\r\n";
                }
                if (cust_add1 != "")
                {
                    strReportHeader = strReportHeader + cust_add1 + "\r\n";
                    
                }

                if (cust_add2 != null)
                {
                    if (cust_add2 != "")
                    {
                        strReportHeader = strReportHeader + cust_add2 + "\r\n";
                        
                    }

                }

                if (cust_city != null)
                {
                    if (cust_city != "")
                        strcitystatezip = strcitystatezip + cust_city;
                }
                if (cust_state != null)
                {
                    if (cust_state != "")
                    {
                        if (strcitystatezip == "") strcitystatezip = strcitystatezip + cust_state;
                        else strcitystatezip = strcitystatezip + ", " + cust_state;
                    }
                }
                if (cust_zip != null)
                {
                    if (cust_zip != "")
                    {
                        if (strcitystatezip == "") strcitystatezip = strcitystatezip + cust_zip;
                        else strcitystatezip = strcitystatezip + " " + cust_zip;
                    }
                }
                if (strcitystatezip != "")
                {
                    strReportHeader = strReportHeader + strcitystatezip + "\r\n";
                }

                if (cust_country != "")
                {
                    strReportHeader = strReportHeader + cust_country;
                }

                txtAddress.Text = strReportHeader;

            }

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

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private bool blFinalFlag;
        private string strPaidOutDesc;
        private double dblPaidOutAmount;
        private int intTranNo;
        private int intInvNo;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;

        public bool FinalFlag
        {
            get { return blFinalFlag; }
            set { blFinalFlag = value; }
        }

        public string PaidOutDesc
        {
            get { return strPaidOutDesc; }
            set { strPaidOutDesc = value; }
        }

        public double PaidOutAmount
        {
            get { return dblPaidOutAmount; }
            set { dblPaidOutAmount = value; }
        }

        public int TranNo
        {
            get { return intTranNo; }
            set { intTranNo = value; }
        }

        public int InvNo
        {
            get { return intInvNo; }
            set { intInvNo = value; }
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
        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3)GeneralFunctions.SetDecimal( numAmount, 3);
            else GeneralFunctions.SetDecimal(numAmount, 2);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidAll()) return;
            strCustomerName = txtName.Text.Trim();
            strCustomerAddress = txtAddress.Text.Trim();
            dblPrice = double.Parse(numAmount.Text);
            DialogResult = true;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }
        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Name");
                GeneralFunctions.SetFocus(txtName);
                return false;
            }

            if (Convert.ToDouble(numAmount.Text) <= 0)
            {
                DocMessage.MsgEnter("Valid Amount");
                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void ChkItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ChkAid_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
