//-- Sam Park @ MentalCodeMaster 2020
//----------------------------------------------------
using OfflineRetailV2.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSAcctPayDlg.xaml
    /// </summary>
    public partial class frm_POSAcctPayDlg : Window
    {
        private double dblPayment;
        private int intCustomerID;
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSAcctPayDlg()
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

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public double Payment
        {
            get { return dblPayment; }
            set { dblPayment = value; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void btnHelp_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnOK_Click_1(object sender, RoutedEventArgs e)
        {
            dblPayment = double.Parse(numPayment.Text);
            DialogResult = true;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void GetBalance()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            numBalance.Text = objPOS.FetchCustomerAcctPayBalance(intCustomerID).ToString();
        }

        private void GetLastPayment()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl = objPOS.FetchCustomerLastAcctPay(intCustomerID);
            foreach (DataRow dr in dtbl.Rows)
            {
                numLastPayment.Text = GeneralFunctions.fnDouble(dr["Amount"].ToString()).ToString();
                lbDate.Text = "( " + GeneralFunctions.fnDate(dr["Date"].ToString()).ToString("d") + " )";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Account_Payment;

            lbDate.Text = "";
            
            GetBalance();
            GetLastPayment();

            numPayment.Focus();
            numPayment.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void numBalance_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResMan.DelimitCurrency((sender as TextBox));
        }



        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}