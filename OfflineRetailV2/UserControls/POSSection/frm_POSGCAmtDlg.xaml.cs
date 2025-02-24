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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSGCAmtDlg.xaml
    /// </summary>
    public partial class frm_POSGCAmtDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSGCAmtDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSGCAmtDlg_Loaded;
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

        private void Frm_POSGCAmtDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text =Properties.Resources.Gift_Cert__Balance;
            lbGC.Text = intGiftCertNo;
            txtAmt.Text = dblGiftCertAmt.ToString();
            txtAmt.Focus();
            txtAmt.SelectAll();
        }

        private string intGiftCertNo;
        private double dblGiftCertAmt;

        public string GiftCertNo
        {
            get { return intGiftCertNo; }
            set { intGiftCertNo = value; }
        }

        public double GiftCertAmt
        {
            get { return dblGiftCertAmt; }
            set { dblGiftCertAmt = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse( txtAmt.Text) <= 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Gift_Certificate_balance ,Properties.Resources.Gift_Certificate_Validation , MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtAmt);
                return;
            }
            dblGiftCertAmt =double.Parse( txtAmt.Text);
            DialogResult = true;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
