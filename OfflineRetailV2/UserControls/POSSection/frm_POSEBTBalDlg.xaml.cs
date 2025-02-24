using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSEBTBalDlg.xaml
    /// </summary>
    public partial class frm_POSEBTBalDlg : Window
    {
        private volatile int watchr = 0;
        private FileSystemWatcher watcher;
        private bool blCG = false;
        private string CGresp = "";
        private string CGresptxt = "";
        private string CGmonitor = "";
        private string CGrequestfile = "";
        private string CGanswerfile = "";
        private string CGtrantype = "";
        private double CGamt = 0;
        private int CGinv = 0;

        private string CNO = "";


        private string GBAL = "";
        private string FBAL = "";
        private string CBAL = "";

        private string PrecidiaRequestDisplayFile = "";
        private string PrecidiaResponseDisplayFile = "";
        private string PrecidiaDisplayResponse = "";
        private string PrecidiaDisplayResult = "";
        private string PrecidiaDisplayResultText = "";

        private int PrecidiaResponse = -1;

        private string PrecidiaLogFile = "";
        private string PrecidiaLogPath = "";

        #region POSLink payment variables

        private double POSLink_CashBack = 0;
        private string POSLink_ResultCode = "";
        private string POSLink_ResultTxt = "";
        private string POSLink_RefNum = "";
        private string POSLink_AuthCode = "";
        private string POSLink_CardType = "";
        private string POSLink_BogusAccountNum = "";
        private double POSLink_RequestedAmt = 0;
        private double POSLink_ApprovedAmt = 0;
        private double POSLink_RemainingBalance = 0;
        private double POSLink_ExtraBalance = 0;

        private string POSLinkLogFile = "";
        private string POSLinkLogPath = "";

        #endregion

        #region Datacap payment variables

        private string Dcap_TranType = "";
        private string Dcap_TranCode = "";
        private string Dcap_CardType = "";
        private double Dcap_CashBkAmt = 0;
        private double Dcap_TranAmt = 0;
        private double Dcap_AuthAmt = 0;
        private double Dcap_BalAmt = 0;
        private string Dcap_Sign = "";

        private string Dcap_CmdStatus = "";
        private string Dcap_TextResponse = "";
        private string Dcap_AcctNo = "";
        private string Dcap_Merchant = "";
        private string Dcap_AuthCode = "";
        private string Dcap_RefNo = "";
        private string Dcap_AcqRefData = "";
        private string Dcap_RecordNo = "";
        private string Dcap_InvoiceNo = "";

        #endregion

        public frm_POSEBTBalDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void simpleButton1_Click(object sender, RoutedEventArgs e)
        {
            /*
           MercuryPayment.clsMercuryPymnt mp = new MercuryPayment.clsMercuryPymnt();
           mp.MerchantID = Settings.MercuryHPMerchantID;
           mp.UserID = Settings.MercuryHPUserID;
           mp.COMPort = GeneralFunctions.fnInt32(Settings.MercuryHPPort);
           mp.InvNo = "1";
           mp.PurchaseAmount = 20;

           string msg1 = "";

           if (Settings.ElementHPMode == 1) mp.TestGiftCardReload(ref msg1);

           GeneralFunctions.CreateMercuryTransactionXML(mp.MercuryXmlResponse, mp.Token);*/
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            PrecidiaResponse = -1;
            if ((rbReload.IsChecked == true) && (GeneralFunctions.fnDouble(txtAmt.EditValue.ToString()) == 0))
            {
                DocMessage.MsgInformation(Properties.Resources.Enter_Valid_Reload_Amount);
                GeneralFunctions.SetFocus(txtAmt);
                return;
            }

            /*
            if (Settings.PaymentGateway == 2)
            {
                MercuryPayment.clsMercuryPymnt mp = new MercuryPayment.clsMercuryPymnt();
                mp.MerchantID = Settings.MercuryHPMerchantID;
                mp.UserID = Settings.MercuryHPUserID;
                mp.COMPort = GeneralFunctions.fnInt32(Settings.MercuryHPPort);
                mp.InvNo = "1";
                mp.PurchaseAmount = rgTran.SelectedIndex == 0 ? 1 : GeneralFunctions.fnDouble(txtAmt.Value);

                string msg1 = "";

                if (cmbType.SelectedIndex == 0)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        if (Settings.ElementHPMode == 0) mp.EBTBalance(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestEBTBalance(ref msg1);
                    }
                    if (rgTran.SelectedIndex == 1)
                    {
                        if (Settings.ElementHPMode == 0) mp.EBTReload(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestEBTReload(ref msg1);
                    }

                }

                if (cmbType.SelectedIndex == 1)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        if (Settings.ElementHPMode == 0) mp.EBTCashBalance(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestEBTCashBalance(ref msg1);
                    }
                    if (rgTran.SelectedIndex == 1)
                    {
                        if (Settings.ElementHPMode == 0) mp.EBTReload(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestEBTReload(ref msg1);
                    }
                }

                if (cmbType.SelectedIndex == 2)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        if (Settings.ElementHPMode == 0) mp.GiftCardBalance(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestGiftCardBalance(ref msg1);
                    }
                    if (rgTran.SelectedIndex == 1)
                    {
                        if (Settings.ElementHPMode == 0) mp.GiftCardReload(ref msg1);
                        if (Settings.ElementHPMode == 1) mp.TestGiftCardReload(ref msg1);
                    }
                }

                GeneralFunctions.CreateMercuryTransactionXML(mp.MercuryXmlResponse, mp.Token);

                if (msg1.ToUpper().Trim() == "APPROVED")
                {
                    if (cmbType.SelectedIndex == 0)
                    {
                        lbAc.Text = Properties.Resources."EBT Card : ","frmPOSEBTBalDlg_EBTCard");
                        lbAcVal.Text = mp.CardNumber;
                        lbBal.Text = Properties.Resources."Balance : ","frmPOSEBTBalDlg_Balance");
                        lbBalVal.Text = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(mp.ApprovedAmt));
                    }

                    if (cmbType.SelectedIndex == 1)
                    {
                        lbAc.Text = Properties.Resources."EBT Cash Card : ","frmPOSEBTBalDlg_EBTCashCard");
                        lbAcVal.Text = mp.CardNumber;
                        lbBal.Text = Properties.Resources."Cash Balance : ","frmPOSEBTBalDlg_CashBalance");
                        lbBalVal.Text = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(mp.ApprovedAmt));
                    }


                    if (cmbType.SelectedIndex == 2)
                    {
                        lbAc.Text = Properties.Resources."Gift Card : ","frmPOSEBTBalDlg_GiftCard");
                        lbAcVal.Text = mp.CardNumber;
                        lbBal.Text = Properties.Resources."Balance : ","frmPOSEBTBalDlg_Balance");
                        lbBalVal.Text = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(mp.ApprovedAmt));
                    }
                    lbAc.Visibility = lbAcVal.Visibility = Visibility.Visible;
                    lbBal.Visibility = lbBalVal.Visibility = Visibility.Visible;
                    lbErr.Visibility = Visibility.Collapsed;
                }
                else
                {
                    lbAc.Visibility = lbAcVal.Visibility = lbBal.Visibility = lbBalVal.Visibility = Visibility.Collapsed;
                    lbErr.Text = Properties.Resources."Error occured during transaction...","frmPOSEBTBalDlg_Erroroccuredduringtransaction") + mp.MercuryTextResponse;
                    lbErr.Visibility = Visibility.Visible;
                }
            }

            if (Settings.PaymentGateway == 3)
            {


                string resp = "";
                string resptxt = "";


                if (cmbType.SelectedIndex == 0)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        CGtrantype = "EBTFOODBALANCE";
                    }
                    if (rgTran.SelectedIndex == 1)
                    {

                    }

                }

                if (cmbType.SelectedIndex == 1)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        CGtrantype = "EBTCASHBALANCE";
                    }
                    if (rgTran.SelectedIndex == 1)
                    {

                    }
                }

                if (cmbType.SelectedIndex == 2)
                {
                    btnOK.Enabled = false;
                    if (rgTran.SelectedIndex == 0)
                    {
                        CGtrantype = "GCBALANCE";
                    }
                    if (rgTran.SelectedIndex == 1)
                    {
                        CGtrantype = "GCRELOAD";
                    }
                }

                PrecidiaLogFile = "";
                PrecidiaLogFile = CGtrantype + "_" + DateTime.Now.ToString("MMddyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".txt";

                PrecidiaLogPath = PrecidiaLogFilePath();

                WriteToPrecidiaLogFile("Start: " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));


                PosDataObject.POS objPOS = new PosDataObject.POS();
                objPOS.Connection = SystemVariables.Conn;
                CGinv = objPOS.FetchMaxInvoiceNo();


                XmlDocument XDoc = new XmlDocument();

                // Create root node.
                XmlElement XElemRoot = XDoc.CreateElement("PLRequest");

                XDoc.AppendChild(XElemRoot);

                XmlElement XTemp = XDoc.CreateElement("Command");
                XTemp.InnerText = CGtrantype;
                XElemRoot.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Id");
                XTemp.InnerText = CGinv.ToString();
                XElemRoot.AppendChild(XTemp);

                if (((cmbType.SelectedIndex == 0) || (cmbType.SelectedIndex == 1)) && (rgTran.SelectedIndex == 0))
                {
                    XTemp = XDoc.CreateElement("Input");
                    XTemp.InnerText = "EXTERNAL";
                    XElemRoot.AppendChild(XTemp);
                }

                XmlDocument XmlResponse = new XmlDocument();

                XTemp = XDoc.CreateElement("KeepAlive");
                XTemp.InnerText = "N";
                XElemRoot.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("ClientMAC");
                XTemp.InnerText = Settings.PrecidiaClientMAC;
                XElemRoot.AppendChild(XTemp);

                if (Settings.PrecidiaRRLog == "Y") WriteToPrecidiaLogFile("Request XML : \n" + XDoc.OuterXml);

                bool bTelnet = false;
                try
                {
                    SslTcpClient.RunClient(Settings.PrecidiaPOSLynxMAC, Settings.PrecidiaPort, XDoc, ref XmlResponse);
                    bTelnet = true;
                }
                catch (Exception ex)
                {
                    WriteToPrecidiaLogFile("Socket Error: " + ex.ToString());
                    bTelnet = false;
                }

                if (bTelnet)
                {
                    if (XmlResponse.InnerXml != "")
                    {
                        if (Settings.PrecidiaRRLog == "Y") WriteToPrecidiaLogFile("Response XML : \n" + XmlResponse.InnerXml);

                        SocketResponse_General(XmlResponse);


                        resp = CGresp;
                        resptxt = CGresptxt;

                        WriteToPrecidiaLogFile("Response: " + resp);

                        if (resp == "APPROVED")
                        {
                            PrecidiaResponse = -1;
                            if (cmbType.SelectedIndex == 0)
                            {
                                lbAc.Text = Properties.Resources."EBT Card : ","frmPOSEBTBalDlg_EBTCard");
                                lbAcVal.Text = CNO;
                                lbBal.Text = Properties.Resources."Balance(Food / Cash) : ","frmPOSEBTBalDlg_BalanceFoodCash");
                                lbBalVal.Text = FBAL + " / " + CBAL;
                            }

                            if (cmbType.SelectedIndex == 1)
                            {
                                lbAc.Text = Properties.Resources."EBT Cash Card : ","frmPOSEBTBalDlg_EBTCashCard");
                                lbAcVal.Text = CNO;
                                lbBal.Text = Properties.Resources."Balance(Food / Cash) : ","frmPOSEBTBalDlg_GCBALANCE");
                                lbBalVal.Text = FBAL + " / " + CBAL;
                            }


                            if (cmbType.SelectedIndex == 2)
                            {
                                lbAc.Text = Properties.Resources."Gift Card : ","frmPOSEBTBalDlg_GiftCard");
                                lbAcVal.Text = CNO;
                                lbBal.Text = Properties.Resources."Balance : ","frmPOSEBTBalDlg_Balance");
                                lbBalVal.Text = GBAL;
                            }
                            lbAc.Visibility = lbAcVal.Visibility = Visibility.Visible;
                            lbBal.Visibility = lbBalVal.Visibility = Visibility.Visible;
                            lbErr.Visibility = Visibility.Collapsed;

                            WriteToPrecidiaLogFile("Precidia Display Balance : start");
                            PrecidiaDisplayBalance();
                        }
                        else
                        {
                            lbAc.Visibility = lbAcVal.Visibility = lbBal.Visibility = lbBalVal.Visibility = Visibility.Collapsed;
                            lbErr.Text = Properties.Resources."Error occured during transaction...","frmPOSEBTBalDlg_Erroroccuredduringtransaction") + resp;
                            lbErr.Visibility = Visibility.Visible;
                        }
                    }
                }
            }




            if (Settings.PaymentGateway == 5)
            {
                Dcap_CmdStatus = "";
                Dcap_TextResponse = "";
                Dcap_AcctNo = "";
                Dcap_Merchant = "";
                Dcap_TranCode = "";
                Dcap_CardType = "";
                Dcap_AuthCode = "";
                Dcap_RefNo = "";
                Dcap_AcqRefData = "";
                Dcap_RecordNo = "";
                Dcap_InvoiceNo = "";
                Dcap_TranAmt = 0;
                Dcap_AuthAmt = 0;
                Dcap_CashBkAmt = 0;
                Dcap_BalAmt = 0;

                bool bproceed = true;
                string request_xml = "";
                string response_xml = "";

                DSIPDCXLib.DsiPDCX dsipdx = new DSIPDCXLib.DsiPDCX();

                dsipdx.ServerIPConfig(Settings.DatacapServer, 1);

                PosDataObject.POS objPOS = new PosDataObject.POS();
                objPOS.Connection = SystemVariables.Conn;
                int dcap_max_inv = objPOS.FetchMaxInvoiceNo();

                if (cmbType.SelectedIndex == 0)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        request_xml = GeneralFunctions.Datacap_EBTBalance_Request_XML(dcap_max_inv.ToString());
                        try
                        {
                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                        }
                        catch
                        {
                            bproceed = false;
                        }
                    }
                    if (rgTran.SelectedIndex == 1)
                    {

                    }

                }

                if (cmbType.SelectedIndex == 1)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        request_xml = GeneralFunctions.Datacap_EBTCashBalance_Request_XML(dcap_max_inv.ToString());
                        try
                        {
                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                        }
                        catch
                        {
                            bproceed = false;
                        }
                    }
                    if (rgTran.SelectedIndex == 1)
                    {

                    }
                }

                if (cmbType.SelectedIndex == 2)
                {
                    if (rgTran.SelectedIndex == 0)
                    {
                        request_xml = GeneralFunctions.Datacap_PrePaidBalance_Request_XML(dcap_max_inv);
                        try
                        {
                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                        }
                        catch
                        {
                            bproceed = false;
                        }
                    }
                    if (rgTran.SelectedIndex == 1)
                    {
                        request_xml = GeneralFunctions.Datacap_PrePaidReload_Request_XML(txtAmt.Value,dcap_max_inv);
                        try
                        {
                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                        }
                        catch
                        {
                            bproceed = false;
                        }
                    }
                }

                if (bproceed)
                {
                     GeneralFunctions.Datacap_General_Response(response_xml, ref Dcap_CmdStatus, ref Dcap_TextResponse, ref Dcap_AcctNo, ref Dcap_Merchant, ref Dcap_TranCode,
                                ref Dcap_CardType, ref Dcap_AuthCode, ref Dcap_InvoiceNo, ref Dcap_RefNo, ref Dcap_AcqRefData, ref Dcap_RecordNo,
                                ref Dcap_TranAmt, ref Dcap_AuthAmt, ref Dcap_CashBkAmt, ref Dcap_BalAmt);
                     if (Dcap_CmdStatus == "Approved")
                     {
                         if (cmbType.SelectedIndex == 0)
                         {
                             lbAc.Text = "EBT Card : ";
                             lbAcVal.Text = Dcap_AcctNo;
                             lbBal.Text = "Balance : ";
                             lbBalVal.Text = Dcap_BalAmt.ToString("f");
                         }

                         if (cmbType.SelectedIndex == 1)
                         {
                             lbAc.Text = Properties.Resources."EBT Cash Card : ", "frmPOSEBTBalDlg_EBTCashCard");
                             lbAcVal.Text = Dcap_AcctNo;
                             lbBal.Text = Properties.Resources."Balance : ","frmPOSEBTBalDlg_Balance");
                             lbBalVal.Text = Dcap_BalAmt.ToString("f");
                         }

                         if (cmbType.SelectedIndex == 2)
                         {
                             lbAc.Text = Properties.Resources."Gift Card : ","frmPOSEBTBalDlg_GiftCard");
                             lbAcVal.Text = Dcap_AcctNo;
                             lbBal.Text = Properties.Resources."Balance : ","frmPOSEBTBalDlg_Balance");
                             lbBalVal.Text = Dcap_BalAmt.ToString("f");
                         }
                         lbAc.Visibility = lbAcVal.Visibility = Visibility.Visible;
                         lbBal.Visibility = lbBalVal.Visibility = Visibility.Visible;
                         lbErr.Visibility = Visibility.Collapsed;
                     }
                     else
                     {
                         lbErr.Text = Properties.Resources."Error occured during transaction...","frmPOSEBTBalDlg_Erroroccuredduringtransaction") + Dcap_TextResponse;
                         lbErr.Visibility = Visibility.Visible;
                     }
                    
                }
                
            }

            */


            /*

            if (Settings.PaymentGateway == 7) // POSLink
            {
                POSLinkLogPath = POSLinkLogFilePath();
                POSLink_ResultCode = "";
                POSLink_ResultTxt = "";
                POSLink_RefNum = "";
                POSLink_AuthCode = "";
                POSLink_CardType = "";
                POSLink_BogusAccountNum = "";
                POSLink_CashBack = 0;
                POSLink_RequestedAmt = 0;
                POSLink_ApprovedAmt = 0;
                POSLink_RemainingBalance = 0;
                POSLink_ExtraBalance = 0;

                POSLink.PosLink pg = new POSLink.PosLink();

                pg.CommSetting = GeneralFunctions.GetPOSLinkCommSetup();

                POSLink.LogManagement plog = new POSLink.LogManagement();
                plog.LogLevel = 1;
                plog.LogFilePath = System.IO.Path.GetDirectoryName(POSLinkLogPath);
                pg.LogManageMent = plog;

                POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

                if (mgcrb.IsChecked == true)
                {
                    paymentRequest.TenderType = paymentRequest.ParseTenderType("GIFT");
                }

                if (ebtcrb.IsChecked==true)
                {
                    paymentRequest.TenderType = paymentRequest.ParseTenderType("EBT_CASHBENEFIT");
                }

                if (ebtfrb.IsChecked==true)
                {
                    paymentRequest.TenderType = paymentRequest.ParseTenderType("EBT_FOODSTAMP");
                }

                paymentRequest.TransType = paymentRequest.ParseTransType("INQUIRY");

                paymentRequest.Amount = "0";

                paymentRequest.OrigRefNum = "";
                paymentRequest.InvNum = "";
                paymentRequest.UserID = "";
                paymentRequest.PassWord = "";
                paymentRequest.ClerkID = "";
                paymentRequest.ServerID = "";
                paymentRequest.ECRRefNum = "1";

                pg.PaymentRequest = paymentRequest;

                POSLink.ProcessTransResult result = new POSLink.ProcessTransResult();

                result = pg.ProcessTrans();

                if (result.Code == POSLink.ProcessTransResultCode.OK)
                {
                    POSLink.PaymentResponse paymentResponse = pg.PaymentResponse;
                    if (paymentResponse != null && paymentResponse.ResultCode != null)
                    {
                        POSLink_ResultCode = paymentResponse.ResultCode;
                        POSLink_ResultTxt = paymentResponse.ResultTxt;
                        POSLink_RefNum = paymentResponse.RefNum;

                        POSLink_RequestedAmt = GeneralFunctions.fnDouble(paymentResponse.RequestedAmount) / 100;
                        POSLink_ApprovedAmt = GeneralFunctions.fnDouble(paymentResponse.ApprovedAmount) / 100;
                        POSLink_RemainingBalance = GeneralFunctions.fnDouble(paymentResponse.RemainingBalance) / 100;
                        POSLink_ExtraBalance = GeneralFunctions.fnDouble(paymentResponse.ExtraBalance) / 100;

                        POSLink_BogusAccountNum = paymentResponse.BogusAccountNum;
                        POSLink_CardType = paymentResponse.CardType;


                        POSLink_AuthCode = paymentResponse.AuthCode;

                        if (POSLink_ResultCode == "000000") // Approved
                        {
                            if (rbBalance.IsChecked==true)
                            {
                                lbAc.Text = "EBT Card : ";
                                lbAcVal.Text = paymentResponse.BogusAccountNum;
                                lbBal.Text = Properties.Resources.Balance__;
                                lbBalVal.Text = POSLink_ExtraBalance.ToString("f");
                            }

                            if (rbReload.IsChecked==true)
                            {
                                lbAc.Text = Properties.Resources.EBT_Cash_Card__;
                                lbAcVal.Text = paymentResponse.BogusAccountNum;
                                lbBal.Text = Properties.Resources.Balance__;
                                lbBalVal.Text = POSLink_RemainingBalance.ToString("f");
                            }

                            if (mgcrb.IsChecked==true)
                            {
                                lbAc.Text = Properties.Resources.Gift_Card__;
                                lbAcVal.Text = paymentResponse.BogusAccountNum;
                                lbBal.Text = Properties.Resources.Balance__;
                                lbBalVal.Text = POSLink_RemainingBalance.ToString("f");
                            }
                            lbAc.Visibility = lbAcVal.Visibility = Visibility.Visible;
                            lbBal.Visibility = lbBalVal.Visibility = Visibility.Visible;
                            lbErr.Visibility = Visibility.Collapsed;

                        }
                        else
                        {
                            lbErr.Text = Properties.Resources.Error_occured_during_transaction +Dcap_TextResponse;
                            lbErr.Visibility = Visibility.Visible;
                        }

                    }
                    else
                    {
                        lbErr.Text = Properties.Resources.Error_occured_during_transaction +Dcap_TextResponse;
                        lbErr.Visibility = Visibility.Visible;
                    }

                }
                else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
                {
                    lbErr.Text = Properties.Resources.Error_occured_during_transaction +Dcap_TextResponse;
                    lbErr.Visibility = Visibility.Visible;

                }
                else
                {
                    lbErr.Text = Properties.Resources.Error_occured_during_transaction +Dcap_TextResponse;
                    lbErr.Visibility = Visibility.Visible;
                }
            }

            */
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.EBT__Mercury_Gift_Card_Balance;

            if (Settings.PaymentGateway == 3)
            {
                Title.Text = Properties.Resources.EBT__Precidia_Gift_Card_Balance;
                mgcrb.ToolTip = Properties.Resources.Precidia_Gift_Card;
            }
            if (Settings.PaymentGateway == 5)
            {
                Title.Text = Properties.Resources.EBT__Datacap_Gift_Card_Balance;
                mgcrb.ToolTip = Properties.Resources.Datacap_Gift_Card;
            }

            if (Settings.PaymentGateway == 7)
            {
                Title.Text = Properties.Resources.EBT__POSLink_Gift_Card_Balance;
                mgcrb.ToolTip = Properties.Resources.POSLink_Gift_Card;
            }

            lbAc.Visibility = lbAcVal.Visibility = lbBal.Visibility = lbBalVal.Visibility = lbErr.Visibility = Visibility.Collapsed; ;
            pnlReload.Visibility = Visibility.Collapsed;
            this.Height = 225;
            rbBalance.IsChecked = true;
        }
        TextBlock lbAc = new TextBlock();
        TextBlock lbAcVal = new TextBlock();
        TextBlock lbBal = new TextBlock();
        TextBlock lbBalVal = new TextBlock();

        private void rbBalance_Checked(object sender, RoutedEventArgs e)
        {
            pnlReload.Visibility = Visibility.Collapsed;
            this.Height = 225;
        }

        private void rbReload_Checked(object sender, RoutedEventArgs e)
        {
            pnlReload.Visibility = Visibility.Visible;
            txtAmt.Focus();
            this.Height = 500;
        }

        private void PrecidiaLaneOpen()
        {
            PrecidiaDisplayResult = "";
            PrecidiaDisplayResultText = "";
            PrecidiaDisplayResponse = "";

            XmlDocument XDoc = new XmlDocument();

            // Create root node.
            XmlElement XElemRoot = XDoc.CreateElement("PLRequest");

            XDoc.AppendChild(XElemRoot);

            XmlElement XTemp = XDoc.CreateElement("Command");
            XTemp.InnerText = "PPDISPLAY";
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Lines");
            XTemp.InnerText = "2";
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Text");
            XTemp.InnerText = "***Lane Open***";
            XElemRoot.AppendChild(XTemp);

            XmlDocument XmlResponse = new XmlDocument();

            XTemp = XDoc.CreateElement("KeepAlive");
            XTemp.InnerText = "N";
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ClientMAC");
            XTemp.InnerText = Settings.PrecidiaClientMAC;
            XElemRoot.AppendChild(XTemp);

            bool bTelnet = false;
            try
            {
                SslTcpClient.RunClient(Settings.PrecidiaPOSLynxMAC, Settings.PrecidiaPort, XDoc, ref XmlResponse);
                bTelnet = true;
            }
            catch (Exception ex)
            {
                WriteToPrecidiaLogFile("Socket Error: " + ex.ToString());
                bTelnet = false;
            }

            if (bTelnet)
            {
                if (XmlResponse.InnerXml != "")
                {
                    SocketResponse_Display(XmlResponse);


                    string DisplayResult = PrecidiaDisplayResult;
                    string DisplayResultText = PrecidiaDisplayResultText;
                    string DisplayResponse = PrecidiaDisplayResponse;
                }
            }
        }
        private void SocketResponse_Display(XmlDocument XDoc1)
        {
            //XDoc1.Load(CGmonitor + PrecidiaResponseDisplayFile);
            XmlNodeList nd = XDoc1.GetElementsByTagName("Result");
            for (int i = 0; i < nd.Count; ++i)
            {
                PrecidiaDisplayResult = nd[i].InnerText.ToUpper();
            }

            XmlNodeList nd1 = XDoc1.GetElementsByTagName("ResultText");
            for (int i = 0; i < nd1.Count; ++i)
            {
                PrecidiaDisplayResultText = nd1[i].InnerText.ToUpper();
            }

            XmlNodeList nd2 = XDoc1.GetElementsByTagName("Response");
            for (int i = 0; i < nd2.Count; ++i)
            {
                PrecidiaDisplayResponse = nd2[i].InnerText.ToUpper();
            }
        }
        #region Precidia Log

        private string PrecidiaLogFilePath()
        {
            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";

            //csConnPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Precidia Logs";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Precidia Logs";


            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + PrecidiaLogFile;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + PrecidiaLogFile;
            }
            return strfilename;
        }

        private void WriteToPrecidiaLogFile(string txt)
        {
            FileStream fileStrm;
            if (File.Exists(PrecidiaLogPath)) fileStrm = new FileStream(PrecidiaLogPath, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(PrecidiaLogPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }

        #endregion

        #region POSLink Log

        private string POSLinkLogFilePath()
        {

            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\POSLink Logs";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\POSLink Logs";

            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + POSLinkLogFile;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + POSLinkLogFile;
            }
            return strfilename;
        }

        private void WriteToPOSLinkLogFile(string txt)
        {
            FileStream fileStrm;
            if (File.Exists(POSLinkLogPath)) fileStrm = new FileStream(POSLinkLogPath, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(POSLinkLogPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }

        #endregion

        private void ebtfrb_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void mgcrb_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ebtcrb_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
