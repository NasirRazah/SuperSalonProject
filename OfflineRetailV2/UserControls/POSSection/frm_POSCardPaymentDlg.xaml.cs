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
    /// Interaction logic for frm_POSCardPaymentDlg.xaml
    /// </summary>
    public partial class frm_POSCardPaymentDlg : Window
    {
        private string strCardType;
        private string strCustName;
        private double dblAmount;
        private int intCardTranID;
        private string strreturnmasg;
        private string strAuthCode;
        private string strReference;
        private string strCardData;
        public frm_POSCardPaymentDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        public int CardTranID
        {
            get { return intCardTranID; }
            set { intCardTranID = value; }
        }

        public string CardType
        {
            get { return strCardType; }
            set { strCardType = value; }
        }

        public string CustName
        {
            get { return strCustName; }
            set { strCustName = value; }
        }

        public double Amount
        {
            get { return dblAmount; }
            set { dblAmount = value; }
        }

        public string returnmasg
        {
            get { return strreturnmasg; }
            set { strreturnmasg = value; }
        }

        public string AuthCode
        {
            get { return strAuthCode; }
            set { strAuthCode = value; }
        }

        public string Reference
        {
            get { return strReference; }
            set { strReference = value; }
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private bool IsValidInformation()
        {
            /*if (txtCust.Text.Trim() == "")
            {
                MyMessageBox.ShowBox(Properties.Resources."Enter Customer Name (as in Card)", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtCust);
                return false;
            }

            if (txtCust.Text.Trim().Length > 70)
            {
                MyMessageBox.ShowBox(Properties.Resources."Customer Name too Long", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtCust);
                return false;
            }

            if (txtCard.Text.Trim() == "")
            {
                MyMessageBox.ShowBox(Properties.Resources."Enter Card Number", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtCard);
                return false;
            }*/
            /*if (txtCVV.Text.Trim() == "")
           {
               MyMessageBox.ShowBox(Properties.Resources."Enter CVV Number", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
               GeneralFunctions.SetFocus(txtCust);
               return false;
           }*/
            /*if (cmbMonth.SelectedIndex == -1)
            {
                MyMessageBox.ShowBox(Properties.Resources."Select Expiry Month", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(cmbMonth);
                return false;
            }

            if (cmbYear.SelectedIndex == -1)
            {
                MyMessageBox.ShowBox(Properties.Resources."Select Expiry Year", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(cmbYear);
                return false;
            }

            if (!IsValidExpiry())
            {
                MyMessageBox.ShowBox(Properties.Resources."Invalid Card Expiry", "Credit Card Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtCust);
                return false;
            }*/
            return true;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInformation())
            {
                // element temp out
                /*
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    ElementExpress.ElementPS pg = new ElementExpress.ElementPS();
                    pg.ElementApplicationID = Settings.ElementHPApplicationID;
                    pg.ElementAccountID = Settings.ElementHPAccountID;
                    pg.ElementAccountToken = Settings.ElementHPAccountToken;
                    pg.ElementAcceptorID = Settings.ElementHPAcceptorID;

                    pg.TranAmount = numAmount.Text.Trim();
                    
                    pg.CardCVV = txtCVV.Text.Trim();
                    pg.CardNumber = txtCard.Text.Trim();
                    pg.CardHoldername = txtCust.Text.Trim();
                    pg.CardExpirationMonth = (cmbMonth.SelectedIndex + 1).ToString().PadLeft(2, '0');
                    pg.CardExpirationYear = cmbYear.Text.ToString().Substring(2, 2);
                    string msg1 = "";
                    string msg2 = "";

                    if (Settings.ElementHPMode == 0) pg.POSCreditSale(ref msg1, ref msg2, ref strAuthCode);
                    if (Settings.ElementHPMode == 1) pg.TestPOSCreditSale(ref msg1, ref msg2, ref strAuthCode);

                    if (msg1 == "0")
                    {
                        strreturnmasg = msg1;

                        frmPaymentWindow frm_PaymentWindow = new frmPaymentWindow();
                        try
                        {
                            frm_PaymentWindow.TranID = strAuthCode;
                            if (Settings.ElementHPMode == 0) frm_PaymentWindow.IsTestMode = false;
                            if (Settings.ElementHPMode == 1) frm_PaymentWindow.IsTestMode = true;
                            frm_PaymentWindow.ShowDialog();
                        }
                        finally
                        {
                            frm_PaymentWindow.Dispose();
                        }
                        if (frm_KeyboardDlg != null)
                        {
                            frm_KeyboardDlg.Dispose();
                        }
                        Cursor.Current = Cursors.Default;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        new MessageBoxWindow().Show (msg2, "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                        Cursor.Current = Cursors.Default;
                        DialogResult = DialogResult.None;
                    }
                }
                catch
                {
                    new MessageBoxWindow().Show (Properties.Resources."Error occured during transaction.",""), "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                    Cursor.Current = Cursors.Default;
                    DialogResult = DialogResult.None;
                }
                */

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
               new MessageBoxWindow().Show (Properties.Resources.This_help_topic_is_currently_not_available_ , Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show (Properties.Resources.This_help_topic_is_currently_not_available_, Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void ProcessCard()
        {
            if (strCardData != "")
            {
                string strCardMICR = strCardData;
                string strHolderName = "";
                string strdatestring = "";
                int inFirstCaretPOS = strCardMICR.IndexOf('^');
                int inSecondCaretPOS = strCardMICR.IndexOf('^', inFirstCaretPOS + 1);
                int inThirdCaretPOS = strCardMICR.IndexOf('^', inSecondCaretPOS + 1);

                if (inFirstCaretPOS <= 0) return;
                if (inSecondCaretPOS <= 0) return;


                txtCard.Text = strCardMICR.Substring(2, inFirstCaretPOS - 2);

                if (!txtCard.Text.StartsWith("59")) //if currency is stored then ignore it.
                {
                    strHolderName = strCardMICR.Substring(inFirstCaretPOS + 1, inSecondCaretPOS - inFirstCaretPOS - 1);
                    strHolderName = strHolderName.Replace('/', ' ');
                    txtCust.Text = strHolderName.Trim();
                    if ((inThirdCaretPOS > 0) && ((inThirdCaretPOS - inSecondCaretPOS) <= 3)) return; //check for separator for infinite expiry

                    strdatestring = strCardMICR.Substring(inSecondCaretPOS + 1, 4);
                    int inMonth = GeneralFunctions.fnInt32(strdatestring.Substring(2, 2));
                    cmbMonth.SelectedIndex = inMonth - 1;

                    int inYear = GeneralFunctions.fnInt32("20" + strdatestring.Substring(0, 2));
                    cmbYear.SelectedIndex = cmbYear.Items.IndexOf(inYear.ToString());
                }
            }
        }

        private void txtCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCust.Text.Length > 40)
            {
                e.Handled = true;
            }
        }

        private void txtSwipe_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (e.NewValue.ToString().Length >= 110)
            {
                strCardData = e.NewValue.ToString();
                e.Handled = true;
                txtSwipe.IsReadOnly = true;
                ProcessCard();
                txtSwipe.Text = "";
                this.Focus();
            }
        }

        private void txtSwipe_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtSwipe.IsReadOnly = false;
            txtSwipe.Text = "";
        }


        private bool IsValidExpiry()
        {
            if (DateTime.Today.Year > GeneralFunctions.fnInt32(cmbYear.Text.Trim())) return false;
            if ((DateTime.Today.Year == GeneralFunctions.fnInt32(cmbYear.Text.Trim())) && (DateTime.Today.Month > (cmbMonth.SelectedIndex + 1))) return false;
            if (DateTime.Today.Year < GeneralFunctions.fnInt32(cmbYear.Text.Trim())) return true;
            if ((DateTime.Today.Year == GeneralFunctions.fnInt32(cmbYear.Text.Trim())) && (DateTime.Today.Month <= (cmbMonth.SelectedIndex + 1))) return true;
            return true;
        }
 
    }
}
