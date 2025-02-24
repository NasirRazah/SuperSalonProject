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
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSCardAdjustBrw.xaml
    /// </summary>
    public partial class frm_POSCardAdjustBrw : Window
    {
        public frm_POSCardAdjustBrw()
        {
            InitializeComponent();
        }

        bool blload = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            blload = false;
            dtF.EditValue = DateTime.Today;
            dtT.EditValue = DateTime.Today;
            blload = true;
            FetchData();
        }

        public void FetchData()
        {
            if (!blload) return;
            DataTable dtbl = new DataTable();
            PosDataObject.POS objps = new PosDataObject.POS();
            objps.Connection = SystemVariables.Conn;
            dtbl = objps.FetchCardDataForAdjust(cmbType.EditValue.ToString(), GeneralFunctions.fnDate(dtF.EditValue.ToString()), GeneralFunctions.fnDate(dtT.EditValue.ToString()));
            grdDetail.ItemsSource = dtbl;
        }

        private async void btnAdjust_Click(object sender, RoutedEventArgs e)
        {
            int findx = gridView1.FocusedRowHandle;
            if (findx < 0) return;
            if (new MessageBoxWindow().Show(Properties.Resources.Transaction_against_this_record_will_be_reverted_  + "\n" + Properties.Resources.Do_you_want_to_continue_, Properties.Resources.Credit_Card_Reversal , MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            int fid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(findx, grdDetail, colID));
            string trnid = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF1);
            string trntype = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF3);
            string trnamt = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF4);
            string gateway = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF5);

            string mcauth = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF6);
            string mcref = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF7);
            string mcacq = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF9);
            string mctoken = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF10);
            string mcinv = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF11);
            string mcprocssd = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF12);
            string mccard = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF13);
            string mcresponse = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF14);
            string dbt = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF15);
            string cid = await GeneralFunctions.GetCellValue1(findx, grdDetail, colF16);

            if (gateway == "Element")
            {
                // element temp out
                /*
                ElementExpress.ElementPS pg = new ElementExpress.ElementPS();
                pg.ElementApplicationID = Settings.ElementHPApplicationID;
                pg.ElementAccountID = Settings.ElementHPAccountID;
                pg.ElementAccountToken = Settings.ElementHPAccountToken;
                pg.ElementAcceptorID = Settings.ElementHPAcceptorID;
                pg.TranAmount = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(trnamt));
                pg.ElementTerminalID = Settings.ElementHPTerminalID.PadLeft(4, '0');
                pg.TranID = trnid;
                pg.RefNo = await GeneralFunctions.GetCellValue1(findx, gridView1, colF7);
                pg.ApprovalNo = await GeneralFunctions.GetCellValue1(findx, gridView1, colF6);
                //pg.TktNo = intCardTranID.ToString();
                pg.ApplicationVersion = GeneralFunctions.PaymentGatewayApplicationVersion();

                string msg1 = "";
                string msg2 = "";

                //if (Settings.ElementHPMode == 0) pg.CreditVoidSale(ref msg1, ref msg2);
                if (Settings.ElementHPMode == 1) pg.TestCreditAdjust(ref msg1, ref msg2);
                 */
            }

            /*
            if (gateway == "Mercury")
            {
                MercuryPayment.clsMercuryPymnt mp = new MercuryPayment.clsMercuryPymnt();
                mp.MerchantID = Settings.MercuryHPMerchantID;
                mp.UserID = Settings.MercuryHPUserID;
                mp.COMPort = GeneralFunctions.fnInt32(Settings.MercuryHPPort);
                mp.InvNo = mcinv;
                mp.AuthID = mcauth;
                mp.RefNo = mcref;
                mp.AcqRefData = mcacq;
                mp.Token = mctoken;
                mp.PurchaseAmount = GeneralFunctions.fnDouble(trnamt);
                mp.ApprovedAmt = "0"; 
                mp.MercuryProcessData = mcprocssd;
                string msg1 = "";
                string msg2 = "";

                if (mcresponse != "PARTIAL AP")
                {
                    if (mccard == "Mercury Gift Card")
                    {
                        if (trntype == "Sale")
                        {
                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidSales(ref msg1);
                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidSales(ref msg1);
                        }

                        if (trntype == "Issue")
                        {
                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidIssue(ref msg1);
                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidIssue(ref msg1);
                        }

                        if (trntype == "Reload")
                        {
                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidReload(ref msg1);
                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidReload(ref msg1);
                        }
                    }
                    if (mccard == "Credit Card")
                    {
                        if (Settings.ElementHPMode == 0) mp.CreditVoidSale(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestCreditVoidSale(ref msg1);
                    }
                    if ((mccard == "Credit Card (STAND-IN)") || (mccard == "Credit Card - Voice Auth (STAND-IN)"))
                    {
                        if (Settings.ElementHPMode == 0) mp.StandInCreditVoidSale(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestStandInCreditVoidSale(ref msg1);
                    }
                    if (mccard == "Debit Card")
                    {
                        if (Settings.ElementHPMode == 0) mp.DebitVoidSale(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestDebitVoidSale(ref msg1);
                    }
                }
                else
                {
                    if (mccard == "Credit Card")
                    {
                        if (Settings.ElementHPMode == 0) mp.CreditReversal(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestCreditReversal(ref msg1);
                    }
                    if ((mccard == "Credit Card (STAND-IN)") || (mccard == "Credit Card - Voice Auth (STAND-IN)"))
                    {
                        if (Settings.ElementHPMode == 0) mp.StandInCreditReversal(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestStandInCreditReversal(ref msg1);
                    }
                    if (mccard == "Debit Card")
                    {
                        if (Settings.ElementHPMode == 0) mp.DebitReversal(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestDebitReversal(ref msg1);
                    }
                }

                string AuthCode = mp.AuthID;
                string TranID = mp.TranID;
                string CardNum = mp.CardNumber;
                string CardExMM = mp.CardExMM;
                string CardExYY = mp.CardExYY;
                string CardLogo = mp.CardLogo;
                string CardType = mp.CardType;
                string ApprovedAmt = mp.ApprovedAmt;
                string RefNo = mp.RefNo;
                string CardEntry = mp.CardEntry;
                string Token = mp.Token;
                string AcqRef = mp.AcqRefData;
                string strMercuryMerchantID = mp.MerchantID;
                string MercuryProcessData = mp.MercuryProcessData;
                double MercuryPurchaseAmount = mp.PurchaseAmount;
                string MercuryTranCode = mp.MercuryTranCode;
                string MercuryRecordNo = mp.MercuryRecordNo;
                string MercuryResponseOrigin = mp.MercuryResponseOrigin;
                string MercuryResponseReturnCode = mp.MercuryResponseReturnCode;
                string MercuryTextResponse = mp.MercuryTextResponse;
                string MercuryGiftCardBalance = mp.BalanceAmt;

                if (AuthCode == null) AuthCode = "";
                if (TranID == null) TranID = "";
                if (CardNum == null) CardNum = "";
                if (CardExMM == null) CardExMM = "";
                if (CardExYY == null) CardExYY = "";
                if (CardLogo == null) CardLogo = "";
                if (CardType == null) CardType = "";
                if (ApprovedAmt == null) ApprovedAmt = "0";
                if (RefNo == null) RefNo = "";
                if (CardEntry == null) CardEntry = "";
                if (Token == null) Token = "";
                if (AcqRef == null) AcqRef = "";


                if (MercuryTranCode == null) MercuryTranCode = "";
                if (MercuryRecordNo == null) MercuryRecordNo = "";
                if (MercuryResponseOrigin == null) MercuryResponseOrigin = "";
                if (MercuryResponseReturnCode == null) MercuryResponseReturnCode = "";
                if (MercuryTextResponse == null) MercuryTextResponse = "";
                if (MercuryProcessData == null) MercuryProcessData = "";
                if (MercuryGiftCardBalance == null) MercuryGiftCardBalance = "0";

                GeneralFunctions.CreateMercuryTransactionXML(mp.MercuryXmlResponse, mctoken);

                PosDataObject.POS objcard = new PosDataObject.POS();
                objcard.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objcard.CustomerID = GeneralFunctions.fnInt32(cid);
                objcard.LoginUserID = SystemVariables.CurrentUserID;
                objcard.EmployeeID = SystemVariables.CurrentUserID;
                objcard.CardType = CardLogo;
                objcard.IsDebit = dbt;
                objcard.CardAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                objcard.PaymentGateway = 2;
                objcard.MercuryInvNo = TranID;
                objcard.MercuryProcessData = MercuryProcessData;
                objcard.MercuryTranCode = MercuryTranCode;
                objcard.MercuryPurchaseAmount = MercuryPurchaseAmount;
                objcard.AuthCode = AuthCode;
                objcard.Reference = RefNo;
                objcard.AcqRefData = AcqRef;
                objcard.TokenData = Token;
                objcard.MercuryRecordNo = MercuryRecordNo;
                objcard.MercuryResponseOrigin = MercuryResponseOrigin;
                objcard.MercuryResponseReturnCode = MercuryResponseReturnCode;
                objcard.MercuryTextResponse = MercuryTextResponse;

                objcard.RefCardAct = CardNum;
                objcard.RefCardLogo = CardLogo;
                objcard.RefCardEntry = CardEntry;
                objcard.RefCardAuthID = AuthCode;
                objcard.RefCardTranID = TranID;
                objcard.RefCardMerchID = strMercuryMerchantID;
                objcard.RefCardAuthAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                objcard.CardTranType = "Void";
                objcard.TerminalName = Settings.TerminalName;
                objcard.LogFileName = "";
                string strerr = objcard.InsertCardTrans1();
                
                int intCardTranID = objcard.CardTranID;

                if ((mcresponse.ToUpper() != "PARTIAL AP") && (msg1.ToUpper() == "APPROVED"))
                {
                    MyMessageBox.ShowBox(Translation.SetMultilingualTextInCodes("Credit Card reversal done successfully", "frmPOSCardAdjustBrw_msg_CreditCardreversaldonesuccessf"), Translation.SetMultilingualTextInCodes("Credit Card Reversal", "frmPOSCardAdjustBrw_msg_CreditCardReversal"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PosDataObject.POS ob = new PosDataObject.POS();
                    ob.Connection = SystemVariables.Conn;
                    ob.LoginUserID = SystemVariables.CurrentUserID;
                    ob.CardTranID = fid;
                    string s = ob.UpdateCardAdjustment();
                    if (s == "") FetchData();
                }

                if ((mcresponse.ToUpper().Trim() == "PARTIAL AP"))
                {
                    if (msg1.ToUpper().Trim() == "APPROVED")
                    {
                        MyMessageBox.ShowBox(Translation.SetMultilingualTextInCodes("Credit Card reversal done successfully", "frmPOSCardAdjustBrw_msg_CreditCardreversaldonesuccessf"), Translation.SetMultilingualTextInCodes("Credit Card Reversal", "frmPOSCardAdjustBrw_msg_CreditCardReversal"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PosDataObject.POS ob = new PosDataObject.POS();
                        ob.Connection = SystemVariables.Conn;
                        ob.LoginUserID = SystemVariables.CurrentUserID;
                        ob.CardTranID = fid;
                        string s = ob.UpdateCardAdjustment();
                        if (s == "") FetchData();
                    }
                    else
                    {
                        MercuryPayment.clsMercuryPymnt mp1 = new MercuryPayment.clsMercuryPymnt();
                        mp1.MerchantID = Settings.MercuryHPMerchantID;
                        mp1.UserID = Settings.MercuryHPUserID;
                        mp1.COMPort = GeneralFunctions.fnInt32(Settings.MercuryHPPort);
                        mp1.InvNo = TranID;
                        mp1.AuthID = AuthCode;
                        mp1.RefNo = RefNo;
                        mp1.AcqRefData = AcqRef;
                        mp1.Token = Token;
                        mp1.PurchaseAmount = GeneralFunctions.fnDouble(trnamt);
                        mp1.ApprovedAmt = "0";
                        mp1.MercuryProcessData = MercuryProcessData;
                        string msg11 = "";
                        string msg21 = "";
                        if (CardType == "Credit")
                        {
                            if (Settings.ElementHPMode == 0) mp1.CreditVoidSale(ref msg11);
                            if (Settings.ElementHPMode == 1) mp1.TestCreditVoidSale(ref msg11);
                        }
                        
                        if (CardType == "Debit")
                        {
                            if (Settings.ElementHPMode == 0) mp1.DebitVoidSale(ref msg11);
                            if (Settings.ElementHPMode == 1) mp1.TestDebitVoidSale(ref msg11);
                        }

                        GeneralFunctions.CreateMercuryTransactionXML(mp1.MercuryXmlResponse, Token);

                        AuthCode = mp.AuthID;
                        TranID = mp.TranID;
                        CardNum = mp.CardNumber;
                        CardExMM = mp.CardExMM;
                        CardExYY = mp.CardExYY;
                        CardLogo = mp.CardLogo;
                        CardType = mp.CardType;
                        ApprovedAmt = mp.ApprovedAmt;
                        RefNo = mp.RefNo;
                        CardEntry = mp.CardEntry;
                        Token = mp.Token;
                        AcqRef = mp.AcqRefData;
                        strMercuryMerchantID = mp.MerchantID;
                        MercuryProcessData = mp.MercuryProcessData;
                        MercuryPurchaseAmount = GeneralFunctions.fnDouble(mp.ApprovedAmt);
                        MercuryTranCode = mp.MercuryTranCode;
                        MercuryRecordNo = mp.MercuryRecordNo;
                        MercuryResponseOrigin = mp.MercuryResponseOrigin;
                        MercuryResponseReturnCode = mp.MercuryResponseReturnCode;
                        MercuryTextResponse = mp.MercuryTextResponse;

                        if (AuthCode == null) AuthCode = "";
                        if (TranID == null) TranID = "";
                        if (CardNum == null) CardNum = "";
                        if (CardExMM == null) CardExMM = "";
                        if (CardExYY == null) CardExYY = "";
                        if (CardLogo == null) CardLogo = "";
                        if (CardType == null) CardType = "";
                        if (ApprovedAmt == null) ApprovedAmt = "0";
                        if (RefNo == null) RefNo = "";
                        if (CardEntry == null) CardEntry = "";
                        if (Token == null) Token = "";
                        if (AcqRef == null) AcqRef = "";


                        if (MercuryTranCode == null) MercuryTranCode = "";
                        if (MercuryRecordNo == null) MercuryRecordNo = "";
                        if (MercuryResponseOrigin == null) MercuryResponseOrigin = "";
                        if (MercuryResponseReturnCode == null) MercuryResponseReturnCode = "";
                        if (MercuryTextResponse == null) MercuryTextResponse = "";
                        if (MercuryProcessData == null) MercuryProcessData = "";

                        GeneralFunctions.CreateMercuryTransactionXML(mp.MercuryXmlResponse, Token);

                        PosDataObject.POS objcard1 = new PosDataObject.POS();
                        objcard1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        objcard1.CustomerID = GeneralFunctions.fnInt32(cid);
                        objcard1.LoginUserID = SystemVariables.CurrentUserID;
                        objcard1.EmployeeID = SystemVariables.CurrentUserID;
                        objcard1.CardType = CardLogo;
                        objcard1.IsDebit = dbt;
                        objcard1.CardAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                        objcard1.PaymentGateway = 2;
                        objcard1.MercuryInvNo = TranID;
                        objcard1.MercuryProcessData = MercuryProcessData;
                        objcard1.MercuryTranCode = MercuryTranCode;
                        objcard1.MercuryPurchaseAmount = MercuryPurchaseAmount;
                        objcard1.AuthCode = AuthCode;
                        objcard1.Reference = RefNo;
                        objcard1.AcqRefData = AcqRef;
                        objcard1.TokenData = Token;
                        objcard1.MercuryRecordNo = MercuryRecordNo;
                        objcard1.MercuryResponseOrigin = MercuryResponseOrigin;
                        objcard1.MercuryResponseReturnCode = MercuryResponseReturnCode;
                        objcard1.MercuryTextResponse = MercuryTextResponse;

                        objcard1.RefCardAct = CardNum;
                        objcard1.RefCardLogo = CardLogo;
                        objcard1.RefCardEntry = CardEntry;
                        objcard1.RefCardAuthID = AuthCode;
                        objcard1.RefCardTranID = TranID;
                        objcard1.RefCardMerchID = strMercuryMerchantID;
                        objcard1.RefCardAuthAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                        objcard1.CardTranType = "Void";
                        objcard.TerminalName = Settings.TerminalName;
                        objcard.LogFileName = "";
                        strerr = objcard.InsertCardTrans1();

                        if (msg11.ToUpper().Trim() == "APPROVED")
                        {
                            PosDataObject.POS ob = new PosDataObject.POS();
                            ob.Connection = SystemVariables.Conn;
                            ob.LoginUserID = SystemVariables.CurrentUserID;
                            ob.CardTranID = intCardTranID;
                            string s = ob.UpdateCardAdjustment();
                            if (s == "") FetchData();
                        }
                    }
                }
            }*/
        }

        private void dtF_EditValueChanged(object sender, RoutedEventArgs e)
        {
            blload = false;
            if ((dtF.EditValue != null) && (dtT.EditValue != null) && (cmbType.EditValue != null))
            {
                blload = true;
                FetchData();
            }
        }

        private void dtT_EditValueChanged(object sender, RoutedEventArgs e)
        {
            blload = false;
            if ((dtF.EditValue != null) && (dtT.EditValue != null) && (cmbType.EditValue != null))
            {
                blload = true;
                FetchData();
            }
        }

        private void cmbType_EditValueChanged(object sender, RoutedEventArgs e)
        {
            blload = false;
            if ((dtF.EditValue != null) && (dtT.EditValue != null) && (cmbType.EditValue != null))
            {
                blload = true;
                FetchData();
            }
        }
    }
}
