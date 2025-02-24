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
using OfflineRetailV2;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSPaidOutDlg.xaml
    /// </summary>
    public partial class frm_POSPaidOutDlg : Window
    {
        NumKeyboard nkybrd;
        FullKeyboard fkybrd;
        bool IsAboutNumKybrdOpen = false;
        bool IsAboutFullKybrdOpen = false;

        private POSControl fParentWindow;

        public POSControl calledfrm
        {
            get { return fParentWindow; }
            set { fParentWindow = value; }
        }

        public frm_POSPaidOutDlg()
        {
            InitializeComponent();
            Loaded += Frm_POSPaidOutDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSPaidOutDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            Title.Text =Properties.Resources.Paid_Out;
            SetDecimalPlace();
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

        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3) GeneralFunctions.SetDecimal(numAmount, 3);
            else GeneralFunctions.SetDecimal(numAmount,2);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string srterrmsg = "Error";
            if (IsValidAll())
            {
                strPaidOutDesc = txtExplanation.Text;
                dblPaidOutAmount = Convert.ToDouble(numAmount.Text);
                ResMan.closeKeyboard();
                CloseKeyboards();
                DialogResult = true;
                /*PosDataObject.POS objpos = new PosDataObject.POS();
                objpos.Connection = SystemVariables.Conn;
                objpos.EmployeeID = SystemVariables.CurrentUserID;
                objpos.TransType = 6; // Paid Out
                objpos.CustomerID = 0;
                objpos.Notes = txtExplanation.Text;
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
                    strPaidOutDesc = txtExplanation.Text;
                    dblPaidOutAmount = Convert.ToDouble(numAmount.Text);
                    intInvNo = objpos.ID;
                    srterrmsg = "";
                }
                objpos.EndTransaction();
                if (srterrmsg == "")
                {
                    blFinalFlag = true;
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    Close();
                }*/
            }
        }

        private bool IsValidAll()
        {
            if (txtExplanation.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Paid_Out_explanation ,Properties.Resources.Paid_Out_Validation , MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtExplanation);
                return false;
            }

            if (Convert.ToDouble(numAmount.Text) == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Paid_Out_amount, Properties.Resources.Paid_Out_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void TxtExplanation_GotTouchCapture(object sender, TouchEventArgs e)
        {
            
        }

        private void TxtExplanation_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void NumAmount_GotTouchCapture(object sender, TouchEventArgs e)
        {
            
        }

        private void NumAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void TxtExplanation_GotFocus(object sender, RoutedEventArgs e)
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


        private  void NumAmount_GotFocus(object sender, RoutedEventArgs e)
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
                    nkybrd.Top = location.Y  - 270;
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

        private void NumAmount_MouseEnter(object sender, MouseEventArgs e)
        {
            numAmount.SelectAll();
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
