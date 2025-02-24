using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for frm_POSNewLayawayDlg.xaml
    /// </summary>
    public partial class frm_POSNewLayawayDlg : Window
    {

        private POSControl frm_POS;
        public POSControl calledfrm
        {
            get { return frm_POS; }
            set { frm_POS = value; }
        }

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSNewLayawayDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSNewLayawayDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void Frm_POSNewLayawayDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.New_Layaway;
            numTotal.Text = dblTotalAmt.ToString();
            numMinimumP.Text = Settings.LayawayDepositPercent.ToString();
            numMinimumD.Text = ((Convert.ToDouble(numTotal.Text) * Convert.ToDouble(numMinimumP.Text)) / 100).ToString();
            numDeposit.Text = numMinimumD.Text;
            dtDue.EditValue = DateTime.Today.AddDays(Settings.LayawaysDue);

            /*if ((SystemVariables.CurrentUserID >  0) && (!SecurityPermission.AcssPOSLayawayMinimumDeposit))
            {
                numDeposit.Properties.ReadOnly = true;
            }*/

            numDeposit.Focus();
            numDeposit.SelectAll();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private double dblTotalAmt;
        private DataTable dtblPOSdtbl;
        private int intCustID;
        private string strTaxExempt;
        private bool blFinalFlag;

        private double dblStoreCr;
        private double dblCustAcctBalance;
        private double dblCustAcctLimit;

        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blAllowByAdmin = false;

        //private frmPOSN frm_POS; --Sam

        private int CustDTaxID;
        private string CustDTaxName;
        private double CustDTaxRate;
        private int CustDTaxType;
        private double CustDTaxValue;

        public int TCustDTaxID
        {
            get { return CustDTaxID; }
            set { CustDTaxID = value; }
        }

        public int TCustDTaxType
        {
            get { return CustDTaxType; }
            set { CustDTaxType = value; }
        }

        public string TCustDTaxName
        {
            get { return CustDTaxName; }
            set { CustDTaxName = value; }
        }

        public double TCustDTaxRate
        {
            get { return CustDTaxRate; }
            set { CustDTaxRate = value; }
        }

        public double TCustDTaxValue
        {
            get { return CustDTaxValue; }
            set { CustDTaxValue = value; }
        }

        //public frmPOSN calledfrm --Sam
        //{
        //    get { return frm_POS; }
        //    set { frm_POS = value; }
        //}

        public bool FinalFlag
        {
            get { return blFinalFlag; }
            set { blFinalFlag = value; }
        }


        public double TotalAmt
        {
            get { return dblTotalAmt; }
            set { dblTotalAmt = value; }
        }

        public DataTable POSdtbl
        {
            get { return dtblPOSdtbl; }
            set { dtblPOSdtbl = value; }
        }

        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }

        public string TaxExempt
        {
            get { return strTaxExempt; }
            set { strTaxExempt = value; }
        }

        public double StoreCr
        {
            get { return dblStoreCr; }
            set { dblStoreCr = value; }
        }

        public double CustAcctBalance
        {
            get { return dblCustAcctBalance; }
            set { dblCustAcctBalance = value; }
        }

        public double CustAcctLimit
        {
            get { return dblCustAcctLimit; }
            set { dblCustAcctLimit = value; }
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
            if (Settings.DecimalPlace == 3)
            {
                GeneralFunctions.SetDecimal(numTotal, 3);
                GeneralFunctions.SetDecimal(numMinimumP, 3);
                GeneralFunctions.SetDecimal(numMinimumD, 3);
                GeneralFunctions.SetDecimal(numDeposit, 3);
            }
            else
            {
                GeneralFunctions.SetDecimal(numTotal, 2);
                GeneralFunctions.SetDecimal(numMinimumP, 2);
                GeneralFunctions.SetDecimal(numMinimumD, 2);
                GeneralFunctions.SetDecimal(numDeposit, 2);
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


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (GeneralFunctions.FormatDouble(Convert.ToDouble(numMinimumD.Text)) > GeneralFunctions.FormatDouble(Convert.ToDouble(numDeposit.Text)))
            {
                if (!SecurityPermission.AcssPOSLayawayMinimumDeposit)
                {
                    if (!CheckFunctionButton("31w")) return;
                }
            }
            CloseKeyboards();
            ResMan.closeKeyboard();
            blurGrid.Visibility = Visibility.Visible;
            frmPOSTenderDlg frm_POSTenderDlg = new frmPOSTenderDlg();
            try
            {
                frm_POSTenderDlg.ResumeTransaction = false;
                frm_POSTenderDlg.ReturnItem = false;
                frm_POSTenderDlg.NewLayaway = true;
                frm_POSTenderDlg.FinalFlag = false;
                frm_POSTenderDlg.CustID = intCustID;
                frm_POSTenderDlg.TaxExempt = strTaxExempt;
                frm_POSTenderDlg.POSDatatbl = dtblPOSdtbl;
                frm_POSTenderDlg.LayawayAmt = Convert.ToDouble(numDeposit.Text);
                frm_POSTenderDlg.LayawayTotalSale = dblTotalAmt;
                frm_POSTenderDlg.LayawayDateDue = dtDue.DateTime;
                frm_POSTenderDlg.StoreCr = dblStoreCr;
                frm_POSTenderDlg.CustAcctLimit = dblCustAcctLimit;
                frm_POSTenderDlg.CustAcctBalance = dblCustAcctBalance;
                frm_POSTenderDlg.SuperUserID = intSuperUserID;
                frm_POSTenderDlg.FunctionBtnAccess = blFunctionBtnAccess;
                frm_POSTenderDlg.calledfrm = frm_POS;

                frm_POSTenderDlg.TCustDTaxID = CustDTaxID;
                frm_POSTenderDlg.TCustDTaxName = CustDTaxName;
                frm_POSTenderDlg.TCustDTaxType = CustDTaxType;
                frm_POSTenderDlg.TCustDTaxRate = CustDTaxRate;
                frm_POSTenderDlg.TCustDTaxValue = CustDTaxValue;

                frm_POSTenderDlg.ShowDialog();
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
                blFinalFlag = frm_POSTenderDlg.FinalFlag;
                if (blFinalFlag)
                {
                    
                    Close();
                }
            }
        }

        private bool CheckFunctionButton(string scode)
        {
            blFunctionBtnAccess = false;
            if (SystemVariables.CurrentUserID <= 0)
            {
                blFunctionBtnAccess = true;
                return true;
            }

            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int result = objSecurity.IsExistsPOSAccess(SystemVariables.CurrentUserID, scode);
            if (result == 0)
            {
                if (blAllowByAdmin)
                {
                    return true;
                }
                else
                {
                    bool bl2 = false;
                    blurGrid.Visibility = Visibility.Visible;
                    frm_POSLoginDlg frm_POSLoginDlg2 = new frm_POSLoginDlg();
                    try
                    {
                        frm_POSLoginDlg2.SecurityCode = scode;
                        frm_POSLoginDlg2.ShowDialog();
                        if (frm_POSLoginDlg2.DialogResult ==true)
                        {
                            bl2 = true;
                            blAllowByAdmin = frm_POSLoginDlg2.AllowByAdmin;
                            intSuperUserID = frm_POSLoginDlg2.SuperUserID;
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                    if (!bl2) return false;
                    else return true;
                }
            }
            else
            {
                blFunctionBtnAccess = true;
                return true;
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void DtDue_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
