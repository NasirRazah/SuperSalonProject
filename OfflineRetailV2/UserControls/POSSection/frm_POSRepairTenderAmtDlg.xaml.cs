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
    /// Interaction logic for frm_POSRepairTenderAmtDlg.xaml
    /// </summary>
    public partial class frm_POSRepairTenderAmtDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        private string strInv;
        private double dblDueAmt;
        private double dblTenderAmt;
        public double DueAmt
        {
            get { return dblDueAmt; }
            set { dblDueAmt = value; }
        }
        public double TenderAmt
        {
            get { return dblTenderAmt; }
            set { dblTenderAmt = value; }
        }
        public string Inv
        {
            get { return strInv; }
            set { strInv = value; }
        }

        public frm_POSRepairTenderAmtDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSRepairTenderAmtDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }


        private void OnCloseCommandExecute(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            Close();
        }

        private void Frm_POSRepairTenderAmtDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text =Properties.Resources.Repair_Tender_Amount;
            lbInv.Text = strInv;
            txtDue.Text = dblDueAmt.ToString();
            txtAmt.Text = dblDueAmt.ToString();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtAmt.Text) <= dblDueAmt)
            {
                dblTenderAmt = Convert.ToDouble(txtAmt.Text);
                CloseKeyboards();
                DialogResult = true;
            }
        }
    }
}
