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
    /// Interaction logic for frm_POSPointConversionDlg.xaml
    /// </summary>
    public partial class frm_POSPointConversionDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_POSPointConversionDlg()
        {
            InitializeComponent();
            Loaded += Frm_POSPointConversionDlg_Loaded;
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


        private void Frm_POSPointConversionDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Points_to_Store_Credit;
            PosDataObject.Customer objcust = new PosDataObject.Customer();
            objcust.Connection = SystemVariables.Conn;
            lbCustomer.Text = objcust.FetchCustomerNameFromID(intID);
            lbPoints.Text = objcust.FetchCustomerAccumulatedPoints(intID).ToString();
            Button_Click(btn1, new RoutedEventArgs());
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private int intID;
        private bool boolControlChanged = false;
        private double dblTranStoreCreditAmout;
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public double TranStoreCreditAmout
        {
            get { return dblTranStoreCreditAmout; }
            set { dblTranStoreCreditAmout = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            numC.Text = GeneralFunctions.fnDouble((sender as Button).Content).ToString();
            double storecreditvalue = Convert.ToDouble(numC.Text) * GeneralFunctions.fnInt32(lbPoints.Text);
            if (storecreditvalue != 0)
            {
                btnOK.Content = Properties.Resources.Apply + " " + SystemVariables.CurrencySymbol + GeneralFunctions.FormatDouble1(storecreditvalue) + " " + Properties.Resources.STORE_CREDIT;
            }
            else
            {
                btnOK.Content = Properties.Resources.Apply_Store_Credit;
            }
        }

        private void numC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (GeneralFunctions.fnDouble(numC.Text) == 0)
            {
                btnOK.Content = Properties.Resources.Apply_Store_Credit;
            }
            else
            {
                double storecreditvalue = Convert.ToDouble(numC.Text) * GeneralFunctions.fnInt32(lbPoints.Text);
                if (storecreditvalue != 0)
                {
                    btnOK.Content = Properties.Resources.Apply + " " + SystemVariables.CurrencySymbol + GeneralFunctions.FormatDouble1(storecreditvalue) + " " + Properties.Resources.STORE_CREDIT;
                }
                else
                {
                    btnOK.Content = Properties.Resources.Apply_Store_Credit;
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dblTranStoreCreditAmout = Convert.ToDouble(numC.Text)* GeneralFunctions.fnInt32(lbPoints.Text);
            DialogResult = true;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
    }
}
