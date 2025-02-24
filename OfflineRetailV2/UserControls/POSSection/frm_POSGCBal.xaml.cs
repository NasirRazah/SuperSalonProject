using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System;
using DevExpress.XtraEditors;

using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSGCBal.xaml
    /// </summary>
    public partial class frm_POSGCBal : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private int intCustomerID;
        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }
        public frm_POSGCBal()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);

            Loaded += Frm_POSGCBal_Loaded;
        }

        private void Frm_POSGCBal_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.Gift_Certificate_Balance;
            lbBal.Text = "";
            if (Settings.CentralExportImport == "Y")
            {
                lb1.Visibility = txtStoreCode.Visibility = Visibility.Visible;
                txtStoreCode.Text = Settings.StoreCode;
            }
            else lb1.Visibility = txtStoreCode.Visibility = Visibility.Collapsed;
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
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

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
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
        private double GCBal()
        {
            string store = txtStoreCode.Text.Trim();
            if (Settings.CentralExportImport == "Y")
            {
                if (store == "") store = Settings.StoreCode;
            }
            PosDataObject.POS objGC = new PosDataObject.POS();
            objGC.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objGC.GiftCertBalance(txtGiftCertNo.Text.Trim(), store, Settings.CentralExportImport);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            lbBal.Text = "";
            if (CheckGC())
            {
                if (IfExistGC() != 0)
                {
                    if (Settings.DecimalPlace == 3)
                        lbBal.Text = Properties.Resources.Current_balance__;
                    else
                        lbBal.Text = Properties.Resources.Current_balance__ + GCBal().ToString("#0.00");
                }
                else
                {
                    lbBal.Text = Properties.Resources.Invalid_Gift_Certificate;
                }
            }
        }

        private void txtGiftCertNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GeneralFunctions.MaskIntegerTextEdit(sender as TextEdit, e);
        }

        private int IfExistGC()
        {
            string store = txtStoreCode.Text.Trim();
            if (Settings.CentralExportImport == "Y")
            {
                if (store == "") store = Settings.StoreCode;
            }
            PosDataObject.POS objGC = new PosDataObject.POS();
            objGC.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objGC.IfExistGiftCert(txtGiftCertNo.Text.Trim(), store, Settings.CentralExportImport);
        }

        private bool CheckGC()
        {
            if ((txtGiftCertNo.Text.Trim() == "") || (txtGiftCertNo.Text.Trim() == "0"))
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Gift_Certificate__, Properties.Resources.Gift_Certificate_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtGiftCertNo);
                return false;
            }

            return true;
        }
    }
}
