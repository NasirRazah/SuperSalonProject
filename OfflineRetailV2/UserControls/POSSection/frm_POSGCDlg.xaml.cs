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
using DevExpress.XtraEditors;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSGCDlg.xaml
    /// </summary>
    public partial class frm_POSGCDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private string strStoreCode;
        private string intGiftCertNo;
        private int intCustomerID;
        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }
        public frm_POSGCDlg()
        {
            InitializeComponent();
            Loaded += Frm_POSGCDlg_Loaded;
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
            if (fkybrd != null)
            {
                fkybrd.Close();

            }
            if (nkybrd != null)
            {
                nkybrd.Close();

            }
        }


        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
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

        private int IfExistGC()
        {
            PosDataObject.POS objGC = new PosDataObject.POS();
            objGC.Connection = SystemVariables.Conn;
            return objGC.IfExistGiftCert(txtGiftCertNo.Text.Trim(), txtStoreCode.Text.Trim(), Settings.CentralExportImport);
        }
        private void Frm_POSGCDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.Gift_Certificate;
            if (Settings.CentralExportImport == "Y")
            {
                lb1.Visibility = txtStoreCode.Visibility = Visibility.Visible;
                txtStoreCode.Text = Settings.StoreCode;
            }
            else lb1.Visibility = txtStoreCode.Visibility = Visibility.Collapsed;
        }

        public string GiftCertNo
        {
            get { return intGiftCertNo; }
            set { intGiftCertNo = value; }
        }
        public string StoreCode
        {
            get { return strStoreCode; }
            set { strStoreCode = value; }
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckGC())
            {
                strStoreCode = txtStoreCode.Text.Trim();
                if ( Settings.CentralExportImport == "Y")
                {
                    if (strStoreCode == "") strStoreCode = Settings.StoreCode;
                }
                intGiftCertNo = txtGiftCertNo.Text;
                ResMan.closeKeyboard();
                CloseKeyboards();
                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
        private bool CheckGC()
        {
            if ((txtGiftCertNo.Text.Trim() == "") || (txtGiftCertNo.Text.Trim() == "0"))
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Gift_Certificate__, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtGiftCertNo);
                return false;
            }

            /*if (IfExistGC() == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources."Invalid/Inactive Gift Certificate", "Gift Certificate Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtGiftCertNo);
                return false;
            }*/

            return true;
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
    }
}
