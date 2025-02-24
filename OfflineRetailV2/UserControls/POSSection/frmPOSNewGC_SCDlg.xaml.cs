using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for frmPOSNewGC_SCDlg.xaml
    /// </summary>
    public partial class frmPOSNewGC_SCDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        private string strGCno;
        private double dblAmount;
        private bool blNewGC;
        private int intCustID;
        private DataTable dtblTenderDT;
        private DataTable dtblPOSDT;
        private int intCustomerID;
        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }
        public double Amount
        {
            get { return dblAmount; }
            set { dblAmount = value; }
        }

        public bool NewGC
        {
            get { return blNewGC; }
            set { blNewGC = value; }
        }

        public string GCno
        {
            get { return strGCno; }
            set { strGCno = value; }
        }

        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }

        public DataTable TenderDT
        {
            get { return dtblTenderDT; }
            set { dtblTenderDT = value; }
        }

        public DataTable POSDT
        {
            get { return dtblPOSDT; }
            set { dtblPOSDT = value; }
        }
        public frmPOSNewGC_SCDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
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

        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3)
            {
                //numSC.Decimals = 3;
                //numGC.Decimals = 3;
                lbNote.Text = Properties.Resources.Change_is_greater_than + Settings.GiftCertMaxChange.ToString("f3") + Properties.Resources.__that_is_maximum_for_Gift_Certificate_;
            }
            else
            {
                //numSC.Decimals = 2;
                //numGC.Decimals = 2;
                lbNote.Text = Properties.Resources.Change_is_greater_than + Settings.GiftCertMaxChange.ToString("f") + Properties.Resources.__that_is_maximum_for_Gift_Certificate_;
            }
        }

        private bool CheckNewGC()
        {
            if (txtGiftCertNo.Text.Trim() == "")
            {
               new MessageBoxWindow().Show( Properties.Resources.Enter_Gift_Certificate__, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtGiftCertNo);
                return false;
            }
            if (IfExistGC() != 0)
            {
               new MessageBoxWindow().Show(Properties.Resources.This_Gift_Certificate_already_exists_, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtGiftCertNo);
                return false;
            }
            else
            {
                bool blF = false;
                foreach (DataRow dr in dtblTenderDT.Rows)
                {
                    if ((dr["NEWGC"].ToString() == "Y") && (dr["GIFTCERTIFICATE"].ToString() == txtGiftCertNo.Text))
                    {
                       new MessageBoxWindow().Show(Properties.Resources.This_Gift_Certificate___already_exists_in_this_transaction, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                        blF = true;
                        break;

                    }
                }

                foreach (DataRow dr in dtblPOSDT.Rows)
                {
                    if ((dr["PRODUCTTYPE"].ToString() == "G") && (dr["ID"].ToString() == txtGiftCertNo.Text))
                    {
                       new MessageBoxWindow().Show(Properties.Resources.This_Gift_Certificate___already_exists_in_this_transaction, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                        blF = true;
                        break;

                    }
                }

                if (blF)
                {
                    GeneralFunctions.SetFocus(txtGiftCertNo);
                    return false;
                }
            }
            return true;
        }

        private int IfExistGC()
        {
            PosDataObject.POS objGC = new PosDataObject.POS();
            objGC.Connection = SystemVariables.Conn;
            return objGC.IfExistGiftCert(txtGiftCertNo.Text.Trim(), Settings.StoreCode, Settings.CentralExportImport);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Issue_Store_Credit___New_Gift_Certificate;
            SetDecimalPlace();
            numSC.Text = dblAmount.ToString();
            numGC.Text = dblAmount.ToString();
        }

        private void btnSC_Click(object sender, RoutedEventArgs e)
        {
            if (intCustID == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Must_designate_customer_for_Store_Credit, Properties.Resources.Store_Credit_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (intCustID != 0)
            {
                blNewGC = false;
                DialogResult = true;
                CloseKeyboards();
            }
        }

        private void btnGC_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNewGC())
            {
                strGCno = txtGiftCertNo.Text;
                blNewGC = true;
                string strmsg = Properties.Resources.New_Gift_Certificate__ + strGCno + Properties.Resources.with_amount + dblAmount.ToString("n") + Properties.Resources.issued_;
                new MessageBoxWindow().Show(strmsg, Properties.Resources.New_Gift_Certificate__, MessageBoxButton.OK, MessageBoxImage.Information);
                CloseKeyboards();
                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            CloseKeyboards();
            Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
