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
using DevExpress.XtraEditors;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSGiftCertDlg.xaml
    /// </summary>
    public partial class frm_POSGiftCertDlg : Window
    {
        private bool boolLoad = false;
        private string intGiftCertNo;

        private double dblGiftCertAmount;
        private int intCustomerID;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public string GiftCertNo
        {
            get { return intGiftCertNo; }
            set { intGiftCertNo = value; }
        }

        public double GiftCertAmount
        {
            get { return dblGiftCertAmount; }
            set { dblGiftCertAmount = value; }
        }

        private bool blMercuryGiftCard;

        public bool IsMercuryGiftCard
        {
            get { return blMercuryGiftCard; }
            set { blMercuryGiftCard = value; }
        }

        private string strMgcTranType;

        public string MgcTranType
        {
            get { return strMgcTranType; }
            set { strMgcTranType = value; }
        }
        public frm_POSGiftCertDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSGiftCertDlg_Loaded;
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

        private void Frm_POSGiftCertDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Sell_a_Gift_Certificate;

            if (Settings.SingleGC == "Y")
            {
                rgMGCTran.Visibility = Visibility.Collapsed;
            }
            lbCustomer.Visibility = Visibility.Collapsed;
            lbCustomer1.Visibility = Visibility.Collapsed;
            //rgMGCTran.Visibility = Visibility.Collapsed;
            if (blMercuryGiftCard)
            {
                if (Settings.PaymentGateway == 2) Title.Text = Properties.Resources.Mercury_Gift_Card_Issue_Reload;
                if (Settings.PaymentGateway == 3) Title.Text = Properties.Resources.Precidia_Gift_Card_Issue_Reload;
                if (Settings.PaymentGateway == 5) Title.Text = Properties.Resources.Datacap_Gift_Card_Issue_Reload;
                if (Settings.PaymentGateway == 7) Title.Text = Properties.Resources.POSLink_Gift_Card_Issue_Reload;
                lbNote.Visibility = Visibility.Collapsed;
                lbGC.Visibility = Visibility.Collapsed;
                txtGiftCertNo.Visibility = Visibility.Collapsed;
                rgMGCTran.Visibility = Visibility.Visible;
            }

            if (intCustomerID != 0)
            {
                PosDataObject.Customer objcust = new PosDataObject.Customer();
                objcust.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = objcust.ShowRecord(intCustomerID);
                foreach (DataRow dr in dtbl.Rows)
                {
                    lbCustomer1.Text = dr["FirstName"].ToString() + " " + dr["LastName"].ToString() + " (ID:" + dr["CustomerID"].ToString() + ")";
                }
                lbCustomer.Text = Properties.Resources.Customer;
                dtbl.Dispose();
                lbCustomer.Visibility = Visibility.Visible;
                lbCustomer1.Visibility = Visibility.Visible;
            }

            if (Settings.AutoGC == "Y")
            {
                txtGiftCertNo.Visibility = Visibility.Collapsed;
                lbAuto.Visibility = Visibility.Visible;
            }

            boolLoad = true;
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckGC())
            {
                if (rbIssue.IsChecked == true) intGiftCertNo =  Settings.AutoGC == "Y" ? "AUTO" : txtGiftCertNo.Text;
                if (rbReload.IsChecked == true) intGiftCertNo = txtGiftCertNo.Text;
                dblGiftCertAmount = double.Parse(numAmount.Text);
                if (blMercuryGiftCard) strMgcTranType = rbIssue.IsChecked == true ? "Issue" : "Reload";
                DialogResult = true;
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
        }
        private bool CheckGC()
        {
            if (rbIssue.IsChecked == true)
            {
                if (!blMercuryGiftCard)
                {
                    if (Settings.AutoGC == "N")
                    {
                        if (txtGiftCertNo.Text.Trim() == "")
                        {
                            new MessageBoxWindow().Show(Properties.Resources.Enter_Gift_Certificate__, Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                            GeneralFunctions.SetFocus(txtGiftCertNo);
                            return false;
                        }
                    }
                }

                if (double.Parse(numAmount.Text) <= 0)
                {
                    new MessageBoxWindow().Show(Properties.Resources.Enter_Amount, Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    GeneralFunctions.SetFocus(numAmount);
                    return false;
                }

                if (Settings.AutoGC == "N")
                {
                    if (IfExistGC() > 0)
                    {
                        new MessageBoxWindow().Show("Gift Certificate already issued with this Number", Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                        GeneralFunctions.SetFocus(txtGiftCertNo);
                        return false;
                    }
                }
            }
            if (rbReload.IsChecked == true)
            {
                if (txtGiftCertNo.Text.Trim() == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.Enter_Gift_Certificate__, Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    GeneralFunctions.SetFocus(txtGiftCertNo);
                    return false;
                }

                if (double.Parse(numAmount.Text) <= 0)
                {
                    new MessageBoxWindow().Show(Properties.Resources.Enter_Amount, Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    GeneralFunctions.SetFocus(numAmount);
                    return false;
                }

                if (IfExistGC() == 0)
                {
                    new MessageBoxWindow().Show("Gift Certificate does not exists", Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    GeneralFunctions.SetFocus(txtGiftCertNo);
                    return false;
                }

                if (Settings.SingleGC == "Y")
                {
                    if (IfExistGC() > 0)
                    {
                        new MessageBoxWindow().Show("Gift Certificate already issued with this Number", Properties.Resources.Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                        GeneralFunctions.SetFocus(txtGiftCertNo);
                        return false;
                    }
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void txtGiftCertNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GeneralFunctions.MaskIntegerTextEdit(sender as TextEdit, e);
        }

        private void txtGiftCertNo_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            if (e.NewValue != null)
            {
                if ((e.NewValue.ToString().Contains("-")) || (e.NewValue.ToString().Contains(".")))
                {
                    e.Handled = true;
                }
            }
        }

        private void RbIssue_Checked(object sender, RoutedEventArgs e)
        {
            if (!boolLoad) return;
            if (rbIssue.IsChecked == true)
            {
                if (Settings.AutoGC == "Y")
                {
                    txtGiftCertNo.Visibility = Visibility.Collapsed;
                    lbAuto.Visibility = Visibility.Visible;
                }

            }
            else
            {
                txtGiftCertNo.Visibility = Visibility.Visible;
                lbAuto.Visibility = Visibility.Collapsed;
            }
        }

        private void RbIssue_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
