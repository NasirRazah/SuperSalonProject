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
    /// Interaction logic for frmPOSPromptPriceDlg.xaml
    /// </summary>
    public partial class frmPOSPromptPriceDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        private double dblPrice;
        public double Price
        {
            get { return dblPrice; }
            set { dblPrice = value; }
        }
        public frmPOSPromptPriceDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);

            Loaded += FrmPOSPromptPriceDlg_Loaded;
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

        private void FrmPOSPromptPriceDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            //txtPrice.Decimals =OfflineRetailV2.Data.Settings.DecimalPlace;--sam
            txtPrice.Text = dblPrice.ToString();
            txtPrice.SelectionLength = 0;
            GeneralFunctions.SetFocus(txtPrice);
            txtPrice.SelectAll();
            //double dblP = GeneralFunctions.fnDouble(dblPrice.ToString("f"));
            //txtPrice.Text = GeneralFunctions.fnInt32(dblP * 100).ToString();
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dblPrice = double.Parse(txtPrice.Text);
            //double dblP = GeneralFunctions.fnDouble(txtPrice.Value) / 100;
            //dblPrice = GeneralFunctions.fnDouble(dblP.ToString("f"));
            DialogResult = true;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void txtPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*if ((Char.IsDigit((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key))) || 
                ((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) == 8))
                e.Handled = false;
            else
                e.Handled = true;*/
        }
    }
}
