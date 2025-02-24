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
using System.Text.RegularExpressions;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustomerCreditAddDlg.xaml
    /// </summary>
    public partial class frm_CustomerCreditAddDlg : Window
    {
        public frm_CustomerCreditAddDlg()
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
        private int intID;
        private bool boolControlChanged = false;
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        private bool blSave;

        public bool IsSave
        {
            get { return blSave; }
            set { blSave = value; }
        }

        private bool IsValidAll()
        {
            if (GeneralFunctions.fnDouble(numAmount.Text) == 0)
            {
                DocMessage.MsgEnter("Valid Amount");
                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;
        }

        private bool SaveData()
        {

            PosDataObject.POS objpos = new PosDataObject.POS();
            objpos.Connection = SystemVariables.Conn;
            objpos.EmployeeID = SystemVariables.CurrentUserID;
            objpos.TransType = 50; // Point to Store Credit
            objpos.CustomerID = intID;
            objpos.FunctionButtonAccess = false;
            objpos.ChangedByAdmin = 0;
            // static value
            objpos.StoreID = 1;
            objpos.RegisterID = 1;
            objpos.CloseoutID = GeneralFunctions.GetCloseOutID();
            objpos.TransNoteNo = 0;
            // static value
            objpos.TerminalName = Settings.TerminalName;
            objpos.GCCentralFlag = Settings.CentralExportImport;
            objpos.GCOPStore = Settings.StoreCode;
            objpos.OperateStore = Settings.StoreCode;
            objpos.StoreCreditPointEquivalent = GeneralFunctions.fnDouble(numAmount.Text);
            objpos.BeginTransaction();
            string srterrmsg = "error";
            if (objpos.CreateInvoice())
            {
                srterrmsg = "";
            }
            objpos.EndTransaction();
            return srterrmsg == "";
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
                        if (DocMessage.MsgConfirmation("Do you want to add " + SystemVariables.CurrencySymbol + "" + GeneralFunctions.fnDouble(numAmount.Text).ToString("f2") + " " + "to Store Credit?") == MessageBoxResult.Yes)
                        {
                            if (SaveData())
                            {
                                boolControlChanged = false;
                                blSave = true;
                            }
                            else
                                e.Cancel = true;
                        }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = "Add Credit";
            PosDataObject.Customer objcust = new PosDataObject.Customer();
            objcust.Connection = SystemVariables.Conn;
            lbCustomerName.Text = objcust.FetchCustomerNameFromID(intID);
            boolControlChanged = false;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (DocMessage.MsgConfirmation("Do you want to add " + SystemVariables.CurrencySymbol + "" + GeneralFunctions.fnDouble(numAmount.Text).ToString("f2") + " " + "to Store Credit?") == MessageBoxResult.Yes)
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
        }



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            blSave = false;
            CloseKeyboards();
            Close();
        }

        private void NumAmount_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void NumAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.-]+").IsMatch(e.Text);
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
