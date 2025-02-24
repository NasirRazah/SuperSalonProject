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
    /// Interaction logic for frm_POSBottleRefundDlg.xaml
    /// </summary>
    public partial class frm_POSBottleRefundDlg : Window
    {

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSBottleRefundDlg()
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

        private double dblRefundQty;
        private double dblRefundAmount;

        public double RefundQty
        {
            get { return dblRefundQty; }
            set { dblRefundQty = value; }
        }

        public double RefundAmount
        {
            get { return dblRefundAmount; }
            set { dblRefundAmount = value; }
        }

        private bool IsValidAll()
        {
            if (txtQty.Text == "0")
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Quantity, Properties.Resources.Bottle_Refund_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtQty);
                return false;
            }
            return true;

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                dblRefundQty =double.Parse( txtQty.Text);
                dblRefundAmount = double.Parse(numAmount.Text);
                DialogResult = true;
                ResMan.closeKeyboard();
                Close();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Bottle_Refund;
            txtQty.Text = "1";
            numAmount.Text = Data.Settings.BottleDeposit.ToString();

            txtQty.Focus();
            txtQty.SelectAll();

            //ResMan.SetDecimal(numAmount, 3);
            //ResMan.SetDecimal(txtQty, 3);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
