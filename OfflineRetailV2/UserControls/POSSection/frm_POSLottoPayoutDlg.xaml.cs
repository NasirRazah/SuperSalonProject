using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;


using System.Windows;


namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSLottoPayoutDlg.xaml
    /// </summary>
    public partial class frm_POSLottoPayoutDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_POSLottoPayoutDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSLottoPayoutDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSLottoPayoutDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Lotto_Payout;
            SetDecimalPlace();
            SetupQuickTendering();
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
            else GeneralFunctions.SetDecimal(numAmount, 2);
        }

        private void SetupQuickTendering()
        {

            DataTable dInfo = GeneralFunctions.GetQuickTenderingCurrencies_TenderScreen();

            if (dInfo.Rows.Count == 0)
            {
                //pnlcurrency.Height = 0; --Sam
            }
            else
            {
                int intseparator = 1;

                if (dInfo.Rows.Count == 6)
                {
                    intseparator = 1;
                }
                else
                {
                    intseparator = 2;
                }

                


                //dInfo.DefaultView.Sort = "DisplayOrder desc";
                //dInfo.DefaultView.ApplyDefaultSort = true;

                foreach (DataRow dv in dInfo.Rows)
                {
                    System.Windows.Controls.Button qtbtn = new System.Windows.Controls.Button();
                    qtbtn.Width = 100;
                    qtbtn.Height = 50;
                    qtbtn.Margin = new Thickness(10, 5,5,5);
                    qtbtn.Style = this.FindResource("GeneralButtonStyle") as Style;
                   
                    qtbtn.Content = dv["CurrencyName"].ToString();
                    qtbtn.Tag = dv["CurrencyValue"].ToString();
                    qtbtn.Click += Qtbtn_Click;
                    pnlcurrency.Children.Add(qtbtn);
                }
            }


        }

        private void Qtbtn_Click(object sender, RoutedEventArgs e)
        {
            double val = GeneralFunctions.fnDouble((sender as System.Windows.Controls.Button).Tag);
            numAmount.Text = GeneralFunctions.FormatDouble1(val);
            numAmount.Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string srterrmsg = "Error";
            if (IsValidAll())
            {
                PosDataObject.POS objpos = new PosDataObject.POS();
                objpos.Connection = SystemVariables.Conn;
                objpos.EmployeeID = SystemVariables.CurrentUserID;
                objpos.TransType = 60; // Lotto Payout
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
                    dblPaidOutAmount =Convert.ToDouble( numAmount.Text);
                    intInvNo = objpos.ID;
                    srterrmsg = "";
                }
                objpos.EndTransaction();
                if (srterrmsg == "")
                {
                    blFinalFlag = true;
                    Close();
                }
            }
        }
        private bool IsValidAll()
        {
            if (Convert.ToDouble( numAmount.Text)== 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Enter_Payout_Amount, Properties.Resources.Lotto_Payout_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                GeneralFunctions.SetFocus(numAmount);
                return false;
            }
            return true;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
