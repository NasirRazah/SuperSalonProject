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
    /// Interaction logic for frm_POSSafeDropDlg.xaml
    /// </summary>
    public partial class frm_POSSafeDropDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_POSSafeDropDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSSafeDropDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();

            Close();
        }

        private void Frm_POSSafeDropDlg_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Safe_Drop;
            nkybrd = new NumKeyboard();
            SetDecimalPlace();
            numAmount.Focus();
            numAmount.SelectAll();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private bool blFinalFlag;
        private string strPaidOutDesc;
        private double dblPaidOutAmount;
        private int intTranNo;
        private int intInvNo;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;

        public bool FinalFlag
        {
            get { return blFinalFlag; }
            set { blFinalFlag = value; }
        }

        public string PaidOutDesc
        {
            get { return strPaidOutDesc; }
            set { strPaidOutDesc = value; }
        }

        public double PaidOutAmount
        {
            get { return dblPaidOutAmount; }
            set { dblPaidOutAmount = value; }
        }

        public int TranNo
        {
            get { return intTranNo; }
            set { intTranNo = value; }
        }

        public int InvNo
        {
            get { return intInvNo; }
            set { intInvNo = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();

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

        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3) GeneralFunctions.SetDecimal(numAmount, 3);
            else GeneralFunctions.SetDecimal(numAmount, 2);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string srterrmsg = "Error";
            if (IsValidAll())
            {
                dblPaidOutAmount = Convert.ToDouble(numAmount.Text);
                CloseKeyboards();
                ResMan.closeKeyboard();
                DialogResult = true;

                /*
                PosDataObject.POS objpos = new PosDataObject.POS();
                objpos.Connection = SystemVariables.Conn;
                objpos.EmployeeID = SystemVariables.CurrentUserID;
                objpos.TransType = 67; // Safe Drop
                objpos.CustomerID = 0;

                objpos.PaidOutAmount = -Convert.ToDouble(numAmount.Text);
                objpos.ChangedByAdmin = intSuperUserID;
                objpos.FunctionButtonAccess = blFunctionBtnAccess;
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

                objpos.BeginTransaction();
                if (objpos.CreateInvoice())
                {
                    intTranNo = objpos.TransactionNo;
                    dblPaidOutAmount = Convert.ToDouble(numAmount.Text);
                    intInvNo = objpos.ID;
                    srterrmsg = "";
                }
                objpos.EndTransaction();
                if (srterrmsg == "")
                {
                    blFinalFlag = true;
                    CloseKeyboards();
                    ResMan.closeKeyboard();
                    Close();
                }*/
            }
        }

        private bool IsValidAll()
        {

            if (Convert.ToDouble(numAmount.Text) == 0)
            {
               new MessageBoxWindow().Show(Properties.Resources.Enter_Amount ,Properties.Resources.Safe_Drop_Validation , MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            blFinalFlag = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
