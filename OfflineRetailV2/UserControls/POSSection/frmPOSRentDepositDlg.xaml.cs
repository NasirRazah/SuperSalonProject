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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSRentDepositDlg.xaml
    /// </summary>
    public partial class frmPOSRentDepositDlg : Window
    {
        private bool isnewrent;
        private string strInv;
        private double dblDeposit1;
        private double dblDeposit2;
        double dblRentDeposit = 0;
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public double RentDeposit
        {
            get { return dblRentDeposit; }
            set { dblRentDeposit = value; }
        }
        public double Deposit1
        {
            get { return dblDeposit1; }
            set { dblDeposit1 = value; }
        }
        public double Deposit2
        {
            get { return dblDeposit2; }
            set { dblDeposit2 = value; }
        }
        public string Inv
        {
            get { return strInv; }
            set { strInv = value; }
        }
        public bool blisnewrent
        {
            get { return isnewrent; }
            set { isnewrent = value; }
        }
        public frmPOSRentDepositDlg()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        

        private void CloseKeyboards()
        {

            if (nkybrd != null)
            {
                nkybrd.Close();

            }

        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!blisnewrent)
            {
                if (double.Parse(txtAmt.Text) > dblRentDeposit)
                {
                    new MessageBoxWindow().Show(Properties.Resources.Security_Deposit_Amt_  + dblRentDeposit +Properties.Resources.to_be_returned_ ,Properties.Resources.Validation ,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    GeneralFunctions.SetFocus(txtAmt);
                    return;
                }
            }
            dblRentDeposit =double.Parse( txtAmt.Text);
            DialogResult =true;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text =Properties.Resources.Rental_Deposit;
            //posNumKeyBoard1.RearrangeForCalculatorStyle(Settings.CalculatorStyleKeyboard == "Y"); --Sam

            if (isnewrent)
            {
                //row1.Height = row2.Height = new GridLength(0);
            }
            else
            {
                lbInv.Text = strInv;
                if (dblDeposit2 > 0) lbamt.Visibility = Visibility.Visible; else lbamt.Visibility = Visibility.Collapsed;
                lbamt.Text =Properties.Resources.Security_Deposit_Amt_  + dblDeposit2.ToString("f") +Properties.Resources.already_returned ;
                lbDeposit.Text =Properties.Resources.Return_Deposit;
            }
            txtAmt.Text = dblRentDeposit.ToString();
            txtAmt.Focus();
            txtAmt.SelectAll();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
