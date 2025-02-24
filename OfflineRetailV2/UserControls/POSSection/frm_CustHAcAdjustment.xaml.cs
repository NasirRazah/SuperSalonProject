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
using System.Data;
using System.Data.SqlClient;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustHAcAdjustment.xaml
    /// </summary>
    public partial class frm_CustHAcAdjustment : Window
    {
        public frm_CustHAcAdjustment()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        private int intCustomerID;

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        private bool blSave;
        private bool boolControlChanged = false;
        public bool IsSave
        {
            get { return blSave; }
            set { blSave = value; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            blSave = true;
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = true;
                        blSave = false;
                    }
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private bool IsValidAll()
        {
            if (dtRef.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Ref. Date");
                GeneralFunctions.SetFocus(dtRef);
                return false;
            }

            if (dtRef.Text.Trim() != "")
            {
                if (dtRef.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation("Ref. Date can not be after today");
                    GeneralFunctions.SetFocus(dtRef);
                    return false;
                }
            }

            if (GeneralFunctions.fnDouble(numAmt.Text) == 0)
            {
                DocMessage.MsgEnter("Adjustment Amount");
                GeneralFunctions.SetFocus(numAmt);
                return false;
            }

            return true;
        }

        private bool SaveData()
        {
            PosDataObject.Customer objC = new PosDataObject.Customer();
            objC.Connection = SystemVariables.Conn;
            objC.ID = intCustomerID;
            objC.RefHAcAdjustment = dtRef.DateTime.Date;
            objC.HAcAdjustmentAmount = cmbTrantype.SelectedIndex == 0 ? GeneralFunctions.fnDouble(numAmt.Text) : GeneralFunctions.fnDouble(numAmt.Text) * (-1);
            objC.ThisStoreCode = Settings.StoreCode;
            string err = objC.InsertHouseAccountAdjustmentData();
            if (err == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            Title.Text = "House Account Adjustment";
            dtRef.DateTime = DateTime.Today.Date;
            boolControlChanged = false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    blSave = true;
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            blSave = false;
            CloseKeyboards();
            Close();
        }

        private void DtRef_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbTrantype_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtRef_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
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

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
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
    }
}
