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
    /// Interaction logic for frm_POSPaidInDlg.xaml
    /// </summary>
    public partial class frm_POSPaidInDlg : Window
    {
        NumKeyboard nkybrd;
        FullKeyboard fkybrd;
        bool IsAboutNumKybrdOpen = false;
        bool IsAboutFullKybrdOpen = false;

        public frm_POSPaidInDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSPaidInDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSPaidInDlg_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Paid_In ;
            SetDecimalPlace();
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();

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
            if (Settings.DecimalPlace == 3)GeneralFunctions.SetDecimal( numAmount, 3);
            else GeneralFunctions.SetDecimal(numAmount, 2);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string srterrmsg = "Error";
            if (IsValidAll())
            {
                strPaidOutDesc = txtExplanation.Text;
                dblPaidOutAmount = Convert.ToDouble(numAmount.Text);
                CloseKeyboards();
                ResMan.closeKeyboard();
                DialogResult = true;

                /*PosDataObject.POS objpos = new PosDataObject.POS();
                objpos.Connection = SystemVariables.Conn;
                objpos.EmployeeID = SystemVariables.CurrentUserID;
                objpos.TransType = 66; // Paid In
                objpos.CustomerID = 0;
                objpos.Notes = txtExplanation.Text;
                objpos.PaidOutAmount =Convert.ToDouble( numAmount.Text);
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
                    CloseKeyboards();
                    ResMan.closeKeyboard();
                    Close();
                }*/
            }
        }
        private bool IsValidAll()
        {
            if (txtExplanation.Text.Trim() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Paid_In_explanation  , Properties.Resources.Paid_In_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(txtExplanation);
                return false;
            }

            if (Convert.ToDouble(numAmount.Text) == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Paid_In_amount, Properties.Resources.Paid_In_Validation, MessageBoxButton.OK, MessageBoxImage.Information);

                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
    }
}
