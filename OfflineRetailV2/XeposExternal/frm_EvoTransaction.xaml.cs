using Dapper;
using OfflineRetailV2.Data;
using OfflineRetailV2.XeposExternal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OfflineRetailV2.XeposExternal
{
    /// <summary>
    /// Interaction logic for frm_EvoTransaction.xaml
    /// </summary>
    public partial class frm_EvoTransaction : Window
    {
        private DataTable pdtblForStockUpdate = null;
        private DataTable dtblTender = null;
        private bool bl100percinvdiscount = false;
        private double dblSubtotal = 0;
        private double dblDiscount = 0;
        private int intDTxID = 0;
        private double dblDTx = 0;
        private double dblFees = 0;
        private double dblFeesTax = 0;
        private double dblSpecialMixnMatch = 0;
        private double dblCoupon = 0;
        private double dblCouponPerc = 0;
        private double dblCouponApplicableTotal = 0;

        private double dblFeesCouponAmount = 0;
        private double dblFeesCouponPerc = 0;
        private double dblFeesCouponApplicableTotal = 0;
        private double dblFeesCouponTaxAmount = 0;

        private double dblNewSubtotal = 0;
        private double dblTax = 0;
        private double dblTotalsale = 0;
        private double dblTender = 0;
        private double dblBalance = 0;
        private double dblChange = 0;
        private string strDiscountReason = "";
        private double dblDiscountPercent = 0;
        private int intCustID;
        private string strTaxExempt;
        private int intTaxID1 = 0;
        private int intTaxID2 = 0;
        private int intTaxID3 = 0;
        private double dblTax1 = 0;
        private double dblTax2 = 0;
        private double dblTax3 = 0;
        private double dblGiftOldAmt = 0;
        private int intINV = 0;
        private int intSuspendInvoiceNo;
        private double dblLayawayAmt;
        private double dblLayawayTotalSale;
        private DateTime dtLayawayDateDue;
        private int intLAYNO = 0;
        private int intLAYTRAN = 0;
        private int intCardTranID = 0;
        private int intEBTCardTranID = 0;
        private double dblStoreCr;
        private double dblCustAcctBalance;
        private double dblCustAcctLimit;
        private int intMaxInvNo;
        private int intSuperUserID;
        private string PrevPayTypeID;
        private string PrevPayTypeName;
        private int CustDTaxID;
        private string CustDTaxName;
        private double CustDTaxRate;
        private int CustDTaxType;
        private int intRepairInvoiceForDeposit = 0;
        private double dblBottleRefund = 0;
        private int tempMercuryGCCardID = 0;
        private double CustDTaxValue = 0;
        private string GCNO = "";
        private string GCSTORE = "";
        private string GCOPSTORE = "";
        private int CashTID = 0;
        private string CashTName = "";
        private string CashTDisplay = "";
        private string strServiceType = "Sales";
        private string sRentCalcFlag = "N";
        private double dblRentSecurityDeposit;
        private double dblRepairAdvance;
        private double dblRepairAmount;
        private double dblRepairDue;
        private double dblRepairTender;
        private int intIssueRentInvNo;
        private int intIssueRepairInvNo;


        public frm_EvoTransaction()
        {
            InitializeComponent();
        }
        private readonly DispatcherTimer _timer;
        private readonly decimal _amount;
        private readonly int _invoiceNo;
        private int _LayawayInvoiceNo;
        private readonly bool _isRefundOrVoid;
        private readonly bool _isExecuteVoid;
        private bool _isEService;
        private bool _isLayAway;
        private DataTable dtblPOSDatatbl = null;
        private bool isExecuteRefund = false;

        private List<TransactionModel> transactionInfoCollection = new List<TransactionModel>();
        public UserControls.POSSection.frm_POSReturnReprintDlg ReturnReprintDlg { get; set; }
        //private readonly UserControls.POSSection.frm_POSReturnReprintDlg _calledfrom;

        OfflineRetailV2.UserControls.POSSection.frm_POSReturnReprintDlg reprintdlg;

        public bool TransactionCompleted { get; set; } = true;

        /*public UserControls.POS.frm_POSOpenOrderDlg calledfrom
        {
            get { return frm_called; }
            set { frm_called = value; }
        }*/

        public frm_EvoTransaction(decimal amount, int invoiceNo, bool isRefundOrVoid, bool isExecuteVoid, DataTable dtblPart, OfflineRetailV2.UserControls.POSSection.frm_POSReturnReprintDlg calledfrom, bool isLayAway = false)
        {

            // ModalWindow.
            //ReturnReprintDlg = obj;
            InitializeComponent();
            //ModalWindow.CloseButton.Visibility = Visibility.Collapsed;
            //GeneralFunctions.ExecuteXeconnectLog("--------------------------------------------", 0);
            
            ContentTextBlock.Text = "Starting transaction, Please wait ...";
            ResizeMode = ResizeMode.NoResize;
            reprintdlg = calledfrom;
            CloseButton.Visibility = Visibility.Hidden;
            CloseButton.Content = "Cancel";

            _amount = amount;

            _isRefundOrVoid = isRefundOrVoid;

            _isExecuteVoid = isExecuteVoid;

            _invoiceNo = invoiceNo;
            _isLayAway = isLayAway;

            dtblPOSDatatbl = dtblPart;


            /*if ((_isRefundOrVoid) && (_isExecuteVoid))
            {
                if (!_isLayAway)
                {
                    if (dtblPOSDatatbl == null)
                    {
                        PosDataObject.POS objp = new PosDataObject.POS();
                        objp.Connection = SystemVariables.Conn;
                        objp.LoginUserID = SystemVariables.CurrentUserID;
                        objp.VoidTransaction(_isLayAway ? _LayawayInvoiceNo : _invoiceNo, Settings.TerminalName);
                    }
                    else
                    {
                        ExecutePartRefund();
                    }
                }
            }*/

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            _timer.Tick += (sender, args) => OnTimerTick();


            ModalWindowWC.CloseCommand = new CommandBase(OnCloseCommand);
        }
        private void OnCloseCommand(object obj)
        {
            Close();
        }

        private async void frm_EvoTransaction_Load(object sender, EventArgs e)
        {


            if (_amount > 0)
                await StartPayment();
            else if (_amount < 0 && _isRefundOrVoid)
                await StartRefund(_invoiceNo);
            else
                await StartVoid(_invoiceNo);
        }

        private async Task StartPayment()
        {
            try
            {
                bool isServerUp = await XeConnectIntegration.IsServerUp();
                
                if (!isServerUp) XeConnectIntegration.RunXeConnect();

                string clearResponseResult = await EvoIntegration.ClearResponse();
                if (clearResponseResult == "Ok")
                {
                    var request = new AuthorizeCaptureDto
                    {
                        Amount = _amount,
                        CashbackAmount = 0,
                        EmployeeId = SystemVariables.CurrentUserID.ToString(),
                        LaneId = SystemVariables.CurrentUserID.ToString(),
                        OrderNo = _invoiceNo.ToString(),
                        RefrenceNo = _invoiceNo.ToString()
                    };
                    var authorizeCaptureResult = await EvoIntegration.AuthorizeCaptureAsync(request);

                    // add in the Logs Table 
                    try
                    {
                        var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                        var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(authorizeCaptureResult);

                        // Creae a new extended function and add the Refnum param we will send the param in Ref Num =  _invoiceNo.ToString()
                        // set instance = "EvoTransactionLogs"
                        //GeneralFunctions.ExecuteXeconnectLog("frm_EvoTransaction.StartPayment : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);
                        GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.StartPayment", "Request --> " + reqDTO + "\nResponseResult --> " + resDTO, _invoiceNo.ToString());
                    }
                    catch (Exception ex)
                    {
                        var err = ex;
                        //GeneralFunctions.ExecuteXeconnectLog("frm_EvoTransaction.StartPayment :" + err, 1);
                    }

                    if (_isEService)
                    {
                        CloseButton.Content = "Please wait...";
                        CloseButton.IsEnabled = false;
                    }
                    else
                    {
                        CloseButton.Content = "Cancel";
                    }

                    CloseButton.Visibility = Visibility.Visible;

                    ContentTextBlock.Text = authorizeCaptureResult;

                    _timer.IsEnabled = true;
                }
                else
                {
                    //GeneralFunctions.ExecuteXeconnectLog(clearResponseResult, 1);

                    ResMan.MessageBox.Show(clearResponseResult, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    _timer.IsEnabled = false;

                    Close();
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.ToString(), 1);

                ResMan.MessageBox.Show("Something went wrong!\nPlease contact support team.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                _timer.IsEnabled = false;

                Close();
            }
        }

        private List<TranscationInfo> GetTranscationInfo(int invoiceNo)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            List<TranscationInfo> transcationInfoList = new List<TranscationInfo>();

            if (_isLayAway)
            {
                DataTable dtbl = objPOS.FetchInvoiceInfoFromLayAwayNum(invoiceNo);
                foreach (DataRow dr in dtbl.Rows)
                    transcationInfoList.Add(new TranscationInfo(Convert.ToInt32(dr["ID"].ToString()), Convert.ToDecimal(dr["TotalSale"].ToString())
                         , Convert.ToString(dr["TransactionId"].ToString())
                         ));
            }
            else
            {

                var transactionId = GetTransactionId(invoiceNo);
                transcationInfoList.Add(new TranscationInfo(invoiceNo, _amount, transactionId));


            }

            return transcationInfoList;
        }

        private async Task StartRefund(int invoiceNo)
        {

            List<TranscationInfo> transcationInfoList = GetTranscationInfo(invoiceNo);

            try
            {

                foreach (var transcationInfo in transcationInfoList)
                {
                    string transactionId = "";

                    if (!_isRefundOrVoid)
                    {
                        transactionId = GetTransactionId(transcationInfo.InvoiceID);
                    }
                    else
                    {
                        transactionId = transcationInfo.TransactionId;
                        _LayawayInvoiceNo = transcationInfo.InvoiceID;
                    }
                    if (string.IsNullOrEmpty(transactionId))
                    {
                        ContentTextBlock.Text = "Transaction Id not found!";

                        CloseButton.Content = "Ok";
                        CloseButton.Visibility = Visibility.Visible;
                        return;
                    }

                    ContentTextBlock.Text = "Refund is in progress...";

                    if (_isEService)
                    {
                        CloseButton.Content = "Please wait...";
                        CloseButton.IsEnabled = false;
                    }
                    else
                    {
                        CloseButton.Content = "Cancel";
                    }

                    CloseButton.Visibility = Visibility.Visible;


                    decimal refAmt = _amount;
                    if (_isLayAway)
                    {
                        refAmt = -transcationInfo.TotalSale;
                    }

                    var refundResponse = await EvoIntegration.RefundAsync(
                        new RefundDto
                        {
                            Amount = decimal.Negate(refAmt),
                            TransactionId = transactionId
                        });

                    // add in the Logs Table 
                    try
                    {
                        var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(refundResponse);
                        GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.EvoIntegration.RefundAsync -> RefundAsync"," Amount ="+ decimal.Negate(refAmt).ToString()  + " => transactionId =>" + transactionId + "  ResponseResult --> " + resDTO, _invoiceNo.ToString());
                    }
                    catch (Exception ex)
                    {
                        var err = ex;

                    }

                    if (refundResponse != "Ok")
                        CloseButton.Content = "Ok";
                    else
                    {
                        OnTimerTick();
                        //   _timer.IsEnabled = true;
                    }

                 
                }

            }
            catch (Exception ex)
            {


            }
        

            try
            {
                if (_isLayAway)
                {
                    GeneralFunctions.SetTransactionLog("Starting Executing Layaway Refund Ledger", DateTime.Now.ToString());
                    if (isExecuteRefund && _isLayAway)
                    {
                        GeneralFunctions.SetTransactionLog("Executing Layaway Refund Ledger", DateTime.Now.ToString());
                        Thread.Sleep(4000);
                        PosDataObject.POS objPOS = new PosDataObject.POS();
                        objPOS.Connection = SystemVariables.Conn;
                        objPOS.RefundStart(invoiceNo, 0);
                        GeneralFunctions.SetTransactionLog("Ending Executing Layaway Refund Ledger", DateTime.Now.ToString());
                    }
                }
            }
            catch (Exception ee)
            {

                GeneralFunctions.SetTransactionLog("Error Executing Layaway Refund Ledger", DateTime.Now.ToString()+"  -- " + ee.Message.ToString());
            }

            foreach (var transactionInfo in transactionInfoCollection)
            {
            //     SaveTransactionDetail(transactionInfo);

            }

        }


        private async Task StartRefund1(int invoiceNo)
        {
            string transactionId = GetTransactionId(invoiceNo);
            if (string.IsNullOrEmpty(transactionId))
            {
                ContentTextBlock.Text = "Transaction Id not found!";

                CloseButton.Content = "Ok";
                CloseButton.Visibility = Visibility.Visible;
                return;
            }

            ContentTextBlock.Text = "Refund is in progress...";

            if (_isEService)
            {
                CloseButton.Content = "Please wait...";
                CloseButton.IsEnabled = false;
            }
            else
            {
                CloseButton.Content = "Cancel";
            }

            CloseButton.Visibility = Visibility.Visible;

            var refundResponse = await EvoIntegration.RefundAsync(
                new RefundDto
                {
                    Amount = decimal.Negate(_amount),
                    TransactionId = transactionId
                });

            if (refundResponse != "Ok")
                CloseButton.Content = "Ok";
            else
                _timer.IsEnabled = true;
        }
        private async Task StartVoid(int invoiceNo)
        {
            string transactionId = GetTransactionId(invoiceNo);
            if (string.IsNullOrEmpty(transactionId))
            {
                ContentTextBlock.Text = "Transaction Id not found!";

                CloseButton.Content = "Ok";
                CloseButton.Visibility = Visibility.Visible;
                return;
            }

            ContentTextBlock.Text = "Void/Undo is in progress...";

            CloseButton.Content = "Cancel";

            var voidResult = await EvoIntegration.VoidAsync(
                new VoidDto
                {
                    Reason = 0,
                    TransactionId = transactionId
                });


            // add in the Logs Table 
            try
            {
                var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(voidResult);
                GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.EvoIntegration.VoidAsync -> VoidAsync", "transactionId =>"+  transactionId + "  ResponseResult --> " + resDTO, _invoiceNo.ToString());
            }
            catch (Exception ex)
            {
                var err = ex;

            }


            if (voidResult != "Ok")
                CloseButton.Content = "Ok";
            else
                _timer.IsEnabled = true;
        }

        private async void OnTimerTick()
        {
            string getTransactionInfoResponse = await EvoIntegration.GetTransactionInfo();

            if (!getTransactionInfoResponse.Equals("\"Request not Completed yet\""))
            {
                XeResponse transactionInfoResult =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<XeResponse>(getTransactionInfoResponse);

                ContentTextBlock.Text = transactionInfoResult.Message;

                // add in the Logs Table 
                try
                {
                    var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(getTransactionInfoResponse);
                    GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.StartPayment -> OnTimerTick",  " ResponseResult --> " + resDTO, _invoiceNo.ToString());
                }
                catch (Exception ex)
                {
                    var err = ex;

                }

                _timer.IsEnabled = false;

                CloseButton.IsEnabled = true;

                CloseButton.Content = "OK";

                if (transactionInfoResult.StatusCode == 0 || transactionInfoResult.StatusCode == 3)
                {
                    isExecuteRefund = true;

                    if ((_isRefundOrVoid) && (_isExecuteVoid))
                    {
                        if (!_isLayAway)
                        {
                            if (dtblPOSDatatbl == null)
                            {
                                PosDataObject.POS objp = new PosDataObject.POS();
                                objp.Connection = SystemVariables.Conn;
                                objp.LoginUserID = SystemVariables.CurrentUserID;
                                objp.VoidTransaction(_isLayAway ? _LayawayInvoiceNo : _invoiceNo, Settings.TerminalName);
                            }
                            else
                            {
                                ExecutePartRefund();
                            }
                        }
                    }
                    var transactionInfo = new TransactionModel
                    {
                        Id = Guid.NewGuid(),
                        Amount = transactionInfoResult.Amount,
                        StatusCode = transactionInfoResult.StatusCode.ToString(),
                        ApprovalCode = transactionInfoResult.ApprovalCode,
                        AVSResult_ActualResult = transactionInfoResult.AvsResultActualResult,
                        AVSResult_PostalCodeResult = transactionInfoResult.AvsResultPostalCodeResult,
                        ProfileId = "",
                        BatchId = transactionInfoResult.BatchId,
                        CVResult = transactionInfoResult.CvResult,
                        StatusMessage = transactionInfoResult.StatusMessage,
                        TransactionId = transactionInfoResult.TransactionId,
                        InvoiceNumber = _isLayAway ? 0 : _invoiceNo,
                        CreatedOn = DateTime.Now,
                        TransType = _isLayAway ? "Refund" : "Trans",
                        LayAwayNo = _isLayAway ? _invoiceNo : 0,
                        LayAwayInvoiceNo = _isLayAway ? _LayawayInvoiceNo : _invoiceNo

                    };
                       SaveTransactionDetail(transactionInfo);
                    transactionInfoCollection.Add(transactionInfo);

                    if (_isLayAway)
                    {
                        //   InservseEntriesRefund();
                    }
                }
            }
        }

        private void InservseEntriesRefund()
        {
            //RefundProcessCenteral RPC = new RefundProcessCenteral();


        }

        private async void CloseButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveTransactionDetail(TransactionModel transactionInfo)
        {
            DataTable connectionStringDataTable = ConfigSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionStringDataTable.Rows[0].ItemArray[0].ToString()))
            {
                string insertIntoXeConnectTransactionQuery = @"INSERT INTO [XeConnectTransactions]([Id],[InvoiceNumber],[TransactionId],[ProfileId],[StatusCode],[StatusMessage],[ApprovalCode],[Amount],[AVSResult_ActualResult],[AVSResult_PostalCodeResult],[BatchId],[CVResult], [CreatedOn], [TransType], [LayAwayNo],[LayAwayInvoiceNo]) VALUES (@Id,@InvoiceNumber,@TransactionId,@ProfileId,@StatusCode,@StatusMessage,@ApprovalCode,@Amount,@AVSResult_ActualResult,@AVSResult_PostalCodeResult,@BatchId,@CVResult,getdate(),@TransType,@LayAwayNo,@LayAwayInvoiceNo)";

                connection.Execute(insertIntoXeConnectTransactionQuery, transactionInfo);
            }
        }

        private string GetTransactionId(int invoiceNo)
        {
            DataTable connectionStringDataTable = ConfigSettings.GetConnectionString();

            using (SqlConnection cn = new SqlConnection(connectionStringDataTable.Rows[0].ItemArray[0].ToString()))
            {
                string selectTransactionIdQuery = $"SELECT TransactionId FROM XeConnectTransactions where InvoiceNumber={invoiceNo}";

                string transactionId = cn.Query<string>(selectTransactionIdQuery).FirstOrDefault();

                return transactionId;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isEService = await EvoIntegration.IsEService();

            await Task.Delay(2000);


            if (_amount > 0)
                await StartPayment();
            else if (_amount < 0 && _isRefundOrVoid)
                await StartRefund(_invoiceNo);
            else
                await StartVoid(_invoiceNo);
        }

        private async void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Content.ToString() == "Cancel")
                {
                    TransactionCompleted = false;
                }
                GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.Cancel -> First_Click_1", " Sender.Content --> " + ((Button)sender).Content.ToString(), _invoiceNo.ToString());
            }
            catch (Exception)
            {
            }
            
            // add in the Logs Table 
            try
            {
                var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(" First Cancel Clicked " + _isEService.ToString());
                GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.Cancel -> First_Click_1", " ResponseResult --> " + resDTO, _invoiceNo.ToString());
            }
            catch (Exception ex)
            {
                var err = ex;

            }

            if (_isEService && ((Button)sender).Content.ToString() == "Cancel")
            {
                // add in the Logs Table 
                try
                {
                    var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject("Sender Cancel Clicked " + _isEService.ToString());
                    GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.Sender Cancel -> CloseButton_Click_1", " ResponseResult --> " + resDTO, _invoiceNo.ToString());
                }
                catch (Exception ex)
                {
                    var err = ex;

                }
                return;
            }
            if (((Button)sender).Content.ToString() == "Cancel")
            {
                string cancelTransactionResult = await EvoIntegration.CancelTransaction();

                // add in the Logs Table 
                try
                {
                    var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(cancelTransactionResult);
                    GeneralFunctions.SetDetailedTransactionLog("frm_EvoTransaction.Cancel -> CancelTransaction()", " ResponseResult --> " + resDTO, _invoiceNo.ToString());
                }
                catch (Exception ex)
                {
                    var err = ex;

                }


                if (cancelTransactionResult != "\"Ok\"")
                    ResMan.MessageBox.Show(cancelTransactionResult, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (reprintdlg != null)
            {
                await reprintdlg.RefreshData();
            }
            Close();
        }

        public async void CloseButton1_Click(object sender, RoutedEventArgs e)
        {
            if (_isEService && CloseButton.Content.ToString() == "Cancel")
                return;

            if (CloseButton.Content.ToString() == "Cancel")
            {
                string cancelTransactionResult = await EvoIntegration.CancelTransaction();

                if (cancelTransactionResult != "\"Ok\"")
                    ResMan.MessageBox.Show(cancelTransactionResult, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Close();
        }

        #region Part Refund

        // Get Tax of a item

        private double GetTaxRate(int ProductID, double ApplicableAmount, string Qty)
        {
            DataTable dtblTax = new DataTable();
            double dblTax = 0;
            PosDataObject.Product objTax = new PosDataObject.Product();
            objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);

            if (strServiceType == "Sales") dtblTax = objTax.ShowActiveTaxes(ProductID);
            if (strServiceType == "Rent") dtblTax = objTax.ShowActiveRentTaxes(ProductID);

            double tqty = 0;
            if (Qty == "")
            {
                tqty = 1;
            }
            else
            {
                tqty = GeneralFunctions.fnDouble(Qty);
            }

            foreach (DataRow drT in dtblTax.Rows)
            {

                if (drT["TaxRate"].ToString() != "")
                {
                    double tx = 0;
                    if (drT["TaxType"].ToString() == "0")
                    {
                        if (Settings.TaxInclusive == "N")
                        {
                            tx = (GeneralFunctions.fnDouble(drT["TaxRate"].ToString()) * ApplicableAmount) / 100;
                        }
                        else
                        {
                            //ApplicableAmount = GeneralFunctions.FormatDouble(ApplicableAmount / tqty);

                            double tempApplicableAmount = ApplicableAmount / ((100 + GeneralFunctions.fnDouble(drT["TaxRate"].ToString())) / 100);

                            tx = GeneralFunctions.FormatDouble(ApplicableAmount - tempApplicableAmount);
                        }
                    }
                    else
                    {
                        tx = GeneralFunctions.GetTaxFromTaxTable(GeneralFunctions.fnInt32(drT["TaxID"].ToString()), GeneralFunctions.fnDouble(drT["TaxRate"].ToString()), ApplicableAmount);
                    }
                    dblTax = dblTax + tx;
                }
            }
            dtblTax.Dispose();
            return dblTax;
        }

        // Check if nondiscountable item or not

        private bool IsNonDiscountableItem(int pID)
        {
            PosDataObject.POS ops = new PosDataObject.POS();
            ops.Connection = SystemVariables.Conn;
            return ops.IsNonDiscountableItem(pID);
        }

        // calcualte ticket discount

        private void CouponCalculation(DataTable dtbl, ref double resultAmount, ref double resultPerc, ref double TotAmount)
        {
            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if ((dr["BUYNGETFREECATEGORY"].ToString() == "B") || (dr["BUYNGETFREECATEGORY"].ToString() == "F")) continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                TotAmount = TotAmount + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            double dpp = 0;
            double dAA = 0;
            double dp = 0;
            double dA = 0;
            double retamt = 0;
            resultAmount = 0;
            double AppAmount = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                dp = 0;
                dA = 0;
                AppAmount = 0;
                if (dr["PRODUCTTYPE"].ToString() != "C") continue;

                if (dr["DISCLOGIC"].ToString() == "P") dp = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());
                if (dr["DISCLOGIC"].ToString() == "A") dA = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());

                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;

                if (CheckIfRestrictedItem(GeneralFunctions.fnInt32(dr["ID"].ToString())))
                {
                    // discount calculation on restricted items
                    AppAmount = RestrictItemApplicableAmount(GeneralFunctions.fnInt32(dr["ID"].ToString()));

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    resultAmount = resultAmount + retamt;
                }
                else
                {
                    // discount calculation on all items

                    AppAmount = AllItemApplicableAmount();

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    resultAmount = resultAmount + retamt;
                }
            }

            if (TotAmount != 0) resultPerc = dpp + GeneralFunctions.fnDouble(dAA * 100 / TotAmount);
        }

        // calcualte Fees

        private void FeesCouponCalculation(DataTable dtbl, ref double resultAmount, ref double TxAmount)
        {
            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if (dr["PRODUCTTYPE"].ToString() != "H") continue;
                resultAmount = resultAmount + GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["FEES"].ToString()));
                TxAmount = TxAmount + GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["FEESTAX"].ToString()));
            }

        }

        // check if Item is associated with any fees & charge

        private bool CheckIfRestrictedItem_Fees(int pID)
        {
            bool ret = false;
            PosDataObject.POS objps1 = new PosDataObject.POS();
            objps1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            ret = objps1.IsRestrictedFees(pID);
            return ret;
        }

        // Get fees & charge amount

        private double RestrictItemApplicableAmount_Fees(int DID)
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["FEESID"].ToString() != "0") continue;

                string chr = "N";
                PosDataObject.POS objp = new PosDataObject.POS();
                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objp.IsApplicableForRestrictedFees(GeneralFunctions.fnInt32(dr["ID"].ToString()), DID, ref chr);
                if (chr == "Y") ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        // Get discount amount

        private double RestrictItemApplicableAmount(int DID)
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["DISCOUNTID"].ToString() != "0") continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                string chr = "N";
                PosDataObject.POS objp = new PosDataObject.POS();
                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objp.IsApplicableForRestrictedDiscount(GeneralFunctions.fnInt32(dr["ID"].ToString()), DID, ref chr);
                if (chr == "Y") ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        // Mix and Match Calculation

        private void SpecialMixnMatchCalculation(DataTable dtbl, ref double resultAmount)
        {

            double dpp = 0;
            double dAA = 0;
            double dp = 0;
            double dA = 0;
            double retamt = 0;
            resultAmount = 0;
            double AppAmount = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                dp = 0;
                dA = 0;
                AppAmount = 0;
                if (dr["PRODUCTTYPE"].ToString() != "Z") continue;

                if (dr["DISCLOGIC"].ToString() == "P") dp = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());
                if (dr["DISCLOGIC"].ToString() == "A") dA = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());

                AppAmount = GeneralFunctions.fnDouble(dr["DTXRATE"].ToString());

                if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                {
                    retamt = (AppAmount * dp / 100) + dA;
                    dAA = dAA + dA;
                    dpp = dpp + dp;
                }
                resultAmount = resultAmount + retamt;
            }

        }

        /// get all discount applicable to the item

        private double AllItemApplicableAmount()
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["DISCOUNTID"].ToString() != "0") continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        // check if restricted discount applicable to the item or not 

        private bool CheckIfRestrictedItem(int pID)
        {
            bool ret = false;
            PosDataObject.POS objps1 = new PosDataObject.POS();
            objps1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            ret = objps1.IsRestrictedDiscount(pID);
            return ret;
        }

        private double GetDTaxAmount(int pTaxID, double pRate, int pType, double AppAmount)
        {
            double tx = 0;

            if (pType == 0)
            {
                tx = (pRate * AppAmount) / 100;
            }
            else
            {
                tx = GeneralFunctions.GetTaxFromTaxTable(pTaxID, pRate, AppAmount);
            }
            return tx;
        }

        private bool IsFoodStampableFees(int FID)
        {
            PosDataObject.POS opos = new PosDataObject.POS();
            opos.Connection = SystemVariables.Conn;
            return opos.IsFoodStampableFees(FID);
        }

        // Check if an Item is Food Stampable

        private string GetFStamp(int PID)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.GetFoodStampFlag(PID);
        }

        private void GetValue()
        {
            FeesCouponCalculation(dtblPOSDatatbl, ref dblFeesCouponAmount, ref dblFeesCouponTaxAmount);
            CouponCalculation(dtblPOSDatatbl, ref dblCoupon, ref dblCouponPerc, ref dblCouponApplicableTotal);
            SpecialMixnMatchCalculation(dtblPOSDatatbl, ref dblSpecialMixnMatch);
            double dblST = 0;
            double dblTX = 0;
            double dblDTax = 0;
            double dblTXR = 0;
            double dblITEMDISC = 0;
            double rate = 0;
            double qty = 0;
            double renttime = 1;
            double dblFC = 0;
            double dblFCTx = 0;
            string strFQty = "N";
            foreach (DataRow drc in dtblPOSDatatbl.Rows)
            {
                bl100percinvdiscount = false;
                if (drc["PRODUCTTYPE"].ToString() != "C") continue;
                if (drc["PRODUCTTYPE"].ToString() != "H") continue;
                if (drc["PRODUCTTYPE"].ToString() == "Z") continue;
                if ((drc["DISCLOGIC"].ToString() == "P") && (GeneralFunctions.fnDouble(drc["DISCVALUE"].ToString()) == 100))
                {
                    bl100percinvdiscount = true;
                    break;
                }
            }

            foreach (DataRow dr in dtblPOSDatatbl.Rows)
            {
                if (dr["PRODUCTTYPE"].ToString() == "C") continue;
                if (dr["PRODUCTTYPE"].ToString() == "H") continue;
                if (dr["PRODUCTTYPE"].ToString() == "Z") continue;
                if (dr["RATE"].ToString() == "") rate = 0; else rate = GeneralFunctions.fnDouble(dr["RATE"].ToString());
                if (dr["QTY"].ToString() == "") qty = 1; else qty = GeneralFunctions.fnDouble(dr["QTY"].ToString());
                if ((dr["RENTDURATION"].ToString() == "") || (dr["RENTDURATION"].ToString() == "0"))
                {
                    renttime = 1;
                }
                else renttime = GeneralFunctions.fnDouble(dr["RENTDURATION"].ToString());

                if (dr["PRODUCTTYPE"].ToString() != "O")
                {
                    if (Settings.TaxInclusive == "N")
                    {
                        dblST = GeneralFunctions.FormatDouble(dblST + rate * qty * renttime);
                    }
                    else
                    {
                        dblST = GeneralFunctions.FormatDouble(dblST + rate * qty);
                    }
                }
                else
                {
                    dblST = GeneralFunctions.FormatDouble(dblST + GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                    
                }

                strFQty = dr["FEESQTY"].ToString();

                if (strFQty == "Y")
                {
                    dblFC = GeneralFunctions.FormatDouble(dblFC + GeneralFunctions.FormatDouble(-qty * GeneralFunctions.fnDouble(dr["FEES"].ToString())));
                    dblFCTx = GeneralFunctions.FormatDouble(dblFCTx + GeneralFunctions.FormatDouble(-qty * GeneralFunctions.fnDouble(dr["FEESTAX"].ToString())));
                }

                if (strFQty == "N")
                {
                    dblFC = GeneralFunctions.FormatDouble(dblFC - GeneralFunctions.fnDouble(dr["FEES"].ToString()));
                    dblFCTx = GeneralFunctions.FormatDouble(dblFCTx - GeneralFunctions.fnDouble(dr["FEESTAX"].ToString()));
                }

                if ((dr["PRODUCTTYPE"].ToString() != "G") && (dr["PRODUCTTYPE"].ToString() != "A") && (dr["PRODUCTTYPE"].ToString() != "C")
                    && (dr["PRODUCTTYPE"].ToString() != "Z") && (dr["PRODUCTTYPE"].ToString() != "H")
                    && (dr["PRODUCTTYPE"].ToString() != "X") && (dr["PRODUCTTYPE"].ToString() != "O"))
                {
                    if (strTaxExempt == "N")
                    {
                        if (!bl100percinvdiscount)
                        {
                            if (dr["PRODUCTTYPE"].ToString() != "B")
                            {
                                dblTX = dblTX + GetTaxRate(GeneralFunctions.fnInt32(dr["ID"].ToString()), Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString()), dr["QTY"].ToString()) * ((100 - dblCouponPerc) / 100);

                                if (strServiceType == "Sales")
                                {
                                    if (CustDTaxID > 0)
                                    {
                                        double tempDTax = 0;
                                        tempDTax = GetDTaxAmount(CustDTaxID, CustDTaxRate, CustDTaxType, GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                                        dblDTax = dblDTax + tempDTax;
                                        dblTX = dblTX + tempDTax;
                                    }
                                }
                            }
                            else
                            {
                                dblTXR = 0;
                                if (dr["TAXABLE1"].ToString() == "Y")
                                {
                                    double txtemp = 0;
                                    if (Settings.TaxInclusive == "N")
                                    {
                                        txtemp = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                    }
                                    else
                                    {
                                        //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                        double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString())) / 100);
                                        txtemp = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);

                                    }


                                    dblTXR = dblTXR + GeneralFunctions.FormatDouble(txtemp) * ((100 - dblCouponPerc) / 100);
                                }

                                if (dr["TAXABLE2"].ToString() == "Y")
                                {
                                    double txtemp = 0;
                                    if (Settings.TaxInclusive == "N")
                                    {
                                        txtemp = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                    }
                                    else
                                    {
                                        //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                        double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString())) / 100);
                                        txtemp = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                    }


                                    dblTXR = dblTXR + GeneralFunctions.FormatDouble(txtemp) * ((100 - dblCouponPerc) / 100);
                                }

                                if (dr["TAXABLE3"].ToString() == "Y")
                                {
                                    double txtemp = 0;
                                    if (Settings.TaxInclusive == "N")
                                    {
                                        txtemp = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                    }
                                    else
                                    {
                                        //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                        double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString())) / 100);
                                        txtemp = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                    }


                                    dblTXR = dblTXR + GeneralFunctions.FormatDouble(txtemp) * ((100 - dblCouponPerc) / 100);
                                }

                                /*
                                if (dr["TAXABLE2"].ToString() == "Y") dblTXR = dblTXR + GeneralFunctions.FormatDouble((GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100) * ((100 - dblCouponPerc) / 100);
                                if (dr["TAXABLE3"].ToString() == "Y") dblTXR = dblTXR + GeneralFunctions.FormatDouble((GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100) * ((100 - dblCouponPerc) / 100);
                                 */
                                dblTX = dblTX + dblTXR;

                                if (strServiceType == "Sales")
                                {
                                    if (CustDTaxID > 0)
                                    {
                                        double tempDTax = 0;
                                        tempDTax = GetDTaxAmount(CustDTaxID, CustDTaxRate, CustDTaxType, GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                                        dblDTax = dblDTax + tempDTax;
                                        dblTX = dblTX + tempDTax;
                                    }
                                }
                            }

                        }

                    }
                    dblITEMDISC = GeneralFunctions.FormatDouble(dblITEMDISC + GeneralFunctions.fnDouble(dr["DISCOUNT"].ToString()));
                }
            }

            if (dblST >= 0)
            {
                dblST = -dblST;
            }

            dblSubtotal = dblST;

            dblDiscount = dblITEMDISC;
            dblTax = dblTX;
            dblFees = dblFC;
            dblFeesTax = dblFCTx;
            dblDTx = dblDTax;
        }


        private void ArrangeSettings()
        {
            dblBalance = 0;
            dblTender = 0;
            dblChange = 0;

            numRepairAdvance.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblRepairAdvance));

            ShowTax();
            numTax.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblTax));
            numRentDeposit.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblRentSecurityDeposit));
            numSubtotal.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblSubtotal));
            numDiscount.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblDiscount));
            dblNewSubtotal = Settings.TaxInclusive == "N" ? GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numSubtotal.Text.Substring(numSubtotal.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numDiscount.Text.Substring(numDiscount.Text.IndexOf(" ") + 1))) : GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numSubtotal.Text.Substring(numSubtotal.Text.IndexOf(" ") + 1)));

            numNewSubtotal.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblNewSubtotal));
            numFee.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblFees + dblFeesCouponAmount));
            numFeeTax.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblFeesTax + dblFeesCouponTaxAmount));
            numCoupon.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblCoupon + dblSpecialMixnMatch));

            dblTotalsale = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numNewSubtotal.Text.Substring(numNewSubtotal.Text.IndexOf(" ") + 1)) + GeneralFunctions.fnDouble(numFee.Text.Substring(numFee.Text.IndexOf(" ") + 1)) + GeneralFunctions.fnDouble(numFeeTax.Text.Substring(numFeeTax.Text.IndexOf(" ") + 1)) + GeneralFunctions.fnDouble(numTax.Text.Substring(numTax.Text.IndexOf(" ") + 1)) + GeneralFunctions.fnDouble(numRentDeposit.Text.Substring(numRentDeposit.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numRepairAdvance.Text.Substring(numRepairAdvance.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numCoupon.Text.Substring(numCoupon.Text.IndexOf(" ") + 1)) + dblBottleRefund);


            //if (blRepairRecall) dblTotalsale = GeneralFunctions.FormatDouble(dblRepairTender); 

            numTotalsale.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblTotalsale));
            numTender.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblTender));

            if (GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numTender.Text.Substring(numTender.Text.IndexOf(" ") + 1))) >= 0)
            {
                dblBalance = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numTender.Text.Substring(numTender.Text.IndexOf(" ") + 1)));
            }
            else
            {
                dblBalance = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numTender.Text.Substring(numTender.Text.IndexOf(" ") + 1)));
               
            }

            numBalanceDue.Text = SystemVariables.CurrencySymbol + " " + GeneralFunctions.FormatDouble1(GeneralFunctions.FormatDouble(dblBalance));

            if (GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTender.Text.Substring(numTender.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1))) >= 0)
            {
                dblChange = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTender.Text.Substring(numTender.Text.IndexOf(" ") + 1)) - GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1)));
            }
            else
            {
                dblChange = 0;
            }


            
        }

        private bool IsFSTenderApplicable()
        {
            bool f = false;
            int i = 0;
            foreach (DataRow dr in dtblTender.Rows)
            {
                i++;
                if (i == 1)
                {
                    if ((dr["TENDER"].ToString() == "Food Stamps") || (dr["TENDER"].ToString() == "EBT Voucher") || (dr["TENDER"].ToString() == "EBT Cash"))
                    {
                        f = true;
                    }
                    break;
                }
            }
            return f;
        }

        private void ShowTax()
        {

            if (strTaxExempt == "N")
            {
                DataTable dtblTax = new DataTable();
                DataTable dtblTax1 = new DataTable();
                dtblTax.Columns.Add("TAXID", System.Type.GetType("System.String"));
                dtblTax.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtblTax.Columns.Add("Amount", System.Type.GetType("System.Double"));
                dtblTax.Columns.Add("DTax", System.Type.GetType("System.String"));

                double dblChangeTax = 0;
                double dblDiscountSum = 0;
                int intCountRow = dtblPOSDatatbl.Rows.Count;
                int intCountCRow = dtblPOSDatatbl.Rows.Count;

                foreach (DataRow dr in dtblPOSDatatbl.Rows)
                {
                    intCountCRow++;
                    if ((dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                        || (dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "X")
                        || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                        || (dr["PRODUCTTYPE"].ToString() == "H")) continue;


                    int intPID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    double dblPrice = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(Settings.TaxInclusive == "N" ? dr["PRICE"].ToString() : dr["GPRICE"].ToString()));
                    if (intCountCRow != intCountRow)
                    {
                        dblPrice = GeneralFunctions.FormatDouble(dblPrice - (dblPrice * dblDiscountPercent / 100));
                        dblDiscountSum = GeneralFunctions.FormatDouble(dblDiscountSum + (dblPrice * dblDiscountPercent / 100));
                    }
                    else
                    {
                        dblPrice = GeneralFunctions.FormatDouble(dblDiscount - dblDiscountSum);
                    }

                    double dblQty = 0;

                    if (dr["QTY"].ToString() == "")
                    {
                        dblQty = 1;
                    }
                    else
                    {
                        dblQty = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["QTY"].ToString()));
                    }

                    if (GetFStamp(intPID) == "Y")
                    {
                        if (IsFSTenderApplicable()) continue;
                    }

                    double dblTaxR = 0;
                    PosDataObject.Product objTax = new PosDataObject.Product();
                    objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    if ((dr["PRODUCTTYPE"].ToString() != "B") && (dr["EDITF"].ToString() == "N"))
                    {
                        if ((strServiceType == "Sales") || (strServiceType == "Repair")) dtblTax1 = objTax.ShowActiveTaxes(intPID);
                        if (strServiceType == "Rent") dtblTax1 = objTax.ShowActiveRentTaxes(intPID);
                        //if (strServiceType == "Repair") dtblTax1 = objTax.ShowActiveRepairTaxes(intPID);
                    }
                    else
                    {
                        DataTable dtblTemp = new DataTable();
                        dtblTemp.Columns.Add("ID", System.Type.GetType("System.String"));
                        dtblTemp.Columns.Add("TaxID", System.Type.GetType("System.String"));
                        dtblTemp.Columns.Add("TaxType", System.Type.GetType("System.String"));
                        dtblTemp.Columns.Add("TaxName", System.Type.GetType("System.String"));
                        dtblTemp.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                        double dblRate = 0.00;
                        string strTaxName = "";

                        if (dr["TAXABLE1"].ToString() == "Y")
                        {
                            if (dr["TAXRATE1"].ToString() != "")
                            {
                                dblRate = GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString());
                            }
                            if (dr["TAXNAME1"].ToString() != "")
                            {
                                if (Settings.DecimalPlace == 3) strTaxName = dr["TAXNAME1"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = dr["TAXNAME1"].ToString() + " (" + dblRate.ToString("f") + "%)";
                            }
                            else
                            {
                                string txnm = GetTaxName(GeneralFunctions.fnInt32(dr["TAXID1"].ToString()));
                                if (Settings.DecimalPlace == 3) strTaxName = txnm + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = txnm + " (" + dblRate.ToString("f") + "%)";
                            }

                            dtblTemp.Rows.Add(new object[] {
                                                "1",
                                                dr["TAXID1"].ToString(),
                                                dr["TX1TYPE"].ToString(),
                                                strTaxName,
                                                dr["TAXRATE1"].ToString()});
                        }

                        if (dr["TAXABLE2"].ToString() == "Y")
                        {
                            if (dr["TAXRATE2"].ToString() != "")
                            {
                                dblRate = GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString());
                            }

                            if (dr["TAXNAME2"].ToString() != "")
                            {
                                if (Settings.DecimalPlace == 3) strTaxName = dr["TAXNAME2"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = dr["TAXNAME2"].ToString() + " (" + dblRate.ToString("f") + "%)";
                            }
                            else
                            {
                                string txnm = GetTaxName(GeneralFunctions.fnInt32(dr["TAXID2"].ToString()));
                                if (Settings.DecimalPlace == 3) strTaxName = txnm + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = txnm + " (" + dblRate.ToString("f") + "%)";
                            }

                            dtblTemp.Rows.Add(new object[] {
                                                "2",
                                                dr["TAXID2"].ToString(),
                                                dr["TX2TYPE"].ToString(),
                                                strTaxName,
                                                dr["TAXRATE2"].ToString()});
                        }

                        if (dr["TAXABLE3"].ToString() == "Y")
                        {
                            if (dr["TAXRATE3"].ToString() != "")
                            {
                                dblRate = GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString());
                            }

                            if (dr["TAXNAME3"].ToString() != "")
                            {
                                if (Settings.DecimalPlace == 3) strTaxName = dr["TAXNAME3"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = dr["TAXNAME3"].ToString() + " (" + dblRate.ToString("f") + "%)";
                            }
                            else
                            {
                                string txnm = GetTaxName(GeneralFunctions.fnInt32(dr["TAXID3"].ToString()));
                                if (Settings.DecimalPlace == 3) strTaxName = txnm + " (" + dblRate.ToString("f3") + "%)";
                                else strTaxName = txnm + " (" + dblRate.ToString("f") + "%)";
                            }

                            dtblTemp.Rows.Add(new object[] {
                                                "3",
                                                dr["TAXID3"].ToString(),
                                                dr["TX3TYPE"].ToString(),
                                                strTaxName,
                                                dr["TAXRATE3"].ToString()});
                        }
                        dtblTax1 = dtblTemp;
                        dtblTemp.Dispose();
                    }


                    DataTable dtblF = new DataTable();

                    dtblF.Columns.Add("ID", System.Type.GetType("System.String"));
                    dtblF.Columns.Add("TaxID", System.Type.GetType("System.String"));
                    dtblF.Columns.Add("TaxType", System.Type.GetType("System.String"));
                    dtblF.Columns.Add("TaxName", System.Type.GetType("System.String"));
                    dtblF.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                    dtblF.Columns.Add("DTax", System.Type.GetType("System.String"));




                    foreach (DataRow drt in dtblTax1.Rows)
                    {
                        dtblF.Rows.Add(new object[] { drt["ID"].ToString(),drt["TaxID"].ToString(),drt["TaxType"].ToString(),
                                                      drt["TaxName"].ToString(),drt["TaxRate"].ToString(),"N" });
                    }

                    if (strServiceType == "Sales")
                    {
                        if (CustDTaxID > 0)
                        {
                            dtblF.Rows.Add(new object[] { "4",CustDTaxID.ToString(),CustDTaxType.ToString(),
                                                      CustDTaxName.ToString(),CustDTaxRate.ToString(),"Y" });
                        }
                    }


                    foreach (DataRow drt in dtblF.Rows)
                    {
                        if (bl100percinvdiscount) continue;
                        bool blfinddata = false;
                        foreach (DataRow dr1 in dtblTax.Rows)
                        {
                            if ((dr1["TAXID"].ToString() == drt["TAXID"].ToString()) && (dr1["DTax"].ToString() == drt["DTax"].ToString()))
                            {
                                double tx = 0;
                                if (drt["TaxType"].ToString() == "0")
                                {
                                    if (Settings.TaxInclusive == "N")
                                    {
                                        tx = (GeneralFunctions.fnDouble(drt["TaxRate"].ToString()) * dblPrice) / 100;
                                    }
                                    else
                                    {
                                        //dblPrice = GeneralFunctions.FormatDouble(dblPrice / dblQty);
                                        double tempApplicableAmount = dblPrice / ((100 + GeneralFunctions.fnDouble(drt["TaxRate"].ToString())) / 100);
                                        tx = GeneralFunctions.FormatDouble(dblPrice - tempApplicableAmount);
                                    }

                                }
                                else
                                {
                                    tx = GeneralFunctions.GetTaxFromTaxTable(GeneralFunctions.fnInt32(drt["TaxID"].ToString()), GeneralFunctions.fnDouble(drt["TaxRate"].ToString()), dblPrice);
                                }
                                dblChangeTax = dblChangeTax + GeneralFunctions.fnDouble(tx * ((100 - dblCouponPerc) / 100));
                                double prevtax = GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                                double dblRate = tx * ((100 - dblCouponPerc) / 100);
                                dr1["Amount"] = Convert.ToString(prevtax + dblRate);
                                blfinddata = true;
                                break;
                            }
                        }
                        if (!blfinddata)
                        {
                            double tx = 0;
                            if (drt["TaxType"].ToString() == "0")
                            {
                                if (Settings.TaxInclusive == "N")
                                {
                                    tx = (GeneralFunctions.fnDouble(drt["TaxRate"].ToString()) * dblPrice) / 100;
                                }
                                else
                                {
                                    //dblPrice = GeneralFunctions.FormatDouble(dblPrice / dblQty);

                                    double tempApplicableAmount = dblPrice / ((100 + GeneralFunctions.fnDouble(drt["TaxRate"].ToString())) / 100);
                                    tx = GeneralFunctions.FormatDouble(dblPrice - tempApplicableAmount);
                                }
                            }
                            else
                            {
                                tx = GeneralFunctions.GetTaxFromTaxTable(GeneralFunctions.fnInt32(drt["TaxID"].ToString()), GeneralFunctions.fnDouble(drt["TaxRate"].ToString()), dblPrice);
                            }

                            dtblTax.Rows.Add(new object[] { drt["TAXID"].ToString(), drt["TaxName"].ToString(), GeneralFunctions.fnDouble(tx * (100 - dblCouponPerc) / 100),
                                                            drt["DTax"].ToString()});
                            dblChangeTax = dblChangeTax + (tx * ((100 - dblCouponPerc) / 100));
                        }
                    }
                }
                dtblTax.DefaultView.Sort = "DTax asc";
                dtblTax.DefaultView.ApplyDefaultSort = true;

                grdTax.ItemsSource = dtblTax;
                dtblTax1.Dispose();
                dtblTax.Dispose();
                dblTax = dblChangeTax;


                /* Adjust decimal */

                double tgrdval = 0;

                foreach (DataRow dr in (grdTax.ItemsSource as DataTable).Rows)
                {
                    tgrdval = tgrdval + GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString()));
                }

                if (tgrdval != GeneralFunctions.FormatDouble(dblTax))
                {
                    double divval = GeneralFunctions.FormatDouble(dblTax) - tgrdval;

                    DataTable dtb = grdTax.ItemsSource as DataTable;
                    int cnt = dtb.Rows.Count;
                    int i = 0;

                    foreach (DataRow dr in dtb.Rows)
                    {
                        i++;
                        if (i == cnt)
                        {
                            dr["Amount"] = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString())) + divval;
                        }
                    }

                    grdTax.ItemsSource = dtb;

                    dtb.Dispose();
                }
            }
        }

        private string GetTaxName(int pTxID)
        {
            PosDataObject.Tax objtx = new PosDataObject.Tax();
            objtx.Connection = SystemVariables.Conn;
            return objtx.GetTaxName(pTxID);
        }

        private DataTable FinalDataTable()
        {
            DataTable dtblFinal = new DataTable();
            dtblFinal.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRODUCT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ONHANDQTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("NORMALQTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("COST", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("QTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("RATE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("NRATE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRICE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DEPT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("CAT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMPRICE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMDESC", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMID", System.Type.GetType("System.String"));
            // add for Layaway Invoice

            dtblFinal.Columns.Add("TAX1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TOTALSALE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTREASON", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("LAYAWAYAMOUNT", System.Type.GetType("System.String"));

            // add for invoice notes
            dtblFinal.Columns.Add("NOTES", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("DISCLOGIC", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCVALUE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMDISCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTTEXT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMINDEX", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("RENTTYPE", System.Type.GetType("System.String"));//34
            dtblFinal.Columns.Add("RENTDURATION", System.Type.GetType("System.Double"));//35
            dtblFinal.Columns.Add("RENTAMOUNT", System.Type.GetType("System.Double"));//36
            dtblFinal.Columns.Add("RENTDEPOSIT", System.Type.GetType("System.Double"));//37

            dtblFinal.Columns.Add("TX1TYPE", System.Type.GetType("System.Int32"));//38
            dtblFinal.Columns.Add("TX1", System.Type.GetType("System.Double"));//39
            dtblFinal.Columns.Add("TX2TYPE", System.Type.GetType("System.Int32"));//40
            dtblFinal.Columns.Add("TX2", System.Type.GetType("System.Double"));//41
            dtblFinal.Columns.Add("TX3TYPE", System.Type.GetType("System.Int32"));//42
            dtblFinal.Columns.Add("TX3", System.Type.GetType("System.Double"));//43

            dtblFinal.Columns.Add("MIXMATCHID", System.Type.GetType("System.Int32"));//46
            dtblFinal.Columns.Add("MIXMATCHFLAG", System.Type.GetType("System.String"));//47
            dtblFinal.Columns.Add("MIXMATCHTYPE", System.Type.GetType("System.String"));//48
            dtblFinal.Columns.Add("MIXMATCHVALUE", System.Type.GetType("System.Double"));//49
            dtblFinal.Columns.Add("MIXMATCHQTY", System.Type.GetType("System.Int32"));//50
            dtblFinal.Columns.Add("MIXMATCHUNIQUE", System.Type.GetType("System.Int32"));//51
            dtblFinal.Columns.Add("MIXMATCHLAST", System.Type.GetType("System.String"));//52

            // for Fees & Charges
            dtblFinal.Columns.Add("FEESID", System.Type.GetType("System.Int32"));//62
            dtblFinal.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//63
            dtblFinal.Columns.Add("FEESVALUE", System.Type.GetType("System.Double"));//64
            dtblFinal.Columns.Add("FEESTAXRATE", System.Type.GetType("System.Double"));//65
            dtblFinal.Columns.Add("FEES", System.Type.GetType("System.Double"));//66
            dtblFinal.Columns.Add("FEESTAX", System.Type.GetType("System.Double"));//67
            dtblFinal.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//68
            dtblFinal.Columns.Add("FEESQTY", System.Type.GetType("System.String"));//68

            dtblFinal.Columns.Add("SALEPRICEID", System.Type.GetType("System.Int32"));//69

            // customer Destination Tax
            dtblFinal.Columns.Add("DTXID", System.Type.GetType("System.Int32"));//71
            dtblFinal.Columns.Add("DTXTYPE", System.Type.GetType("System.Int32"));//72
            dtblFinal.Columns.Add("DTXRATE", System.Type.GetType("System.Double"));//73
            dtblFinal.Columns.Add("DTX", System.Type.GetType("System.Double"));//74

            dtblFinal.Columns.Add("EDITF", System.Type.GetType("System.String"));//75
            dtblFinal.Columns.Add("PROMPTPRICE", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("BUYNGETFREEHEADERID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("BUYNGETFREECATEGORY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("BUYNGETFREENAME", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("AGE", System.Type.GetType("System.Int32"));

            // Add for Tax Inclusive
            dtblFinal.Columns.Add("GRATE", System.Type.GetType("System.Double"));
            dtblFinal.Columns.Add("GPRICE", System.Type.GetType("System.Double"));
            dtblFinal.Columns.Add("UOM", System.Type.GetType("System.String"));
            // fetch TaxID, TaxName first

            string strTaxID1 = "0";
            string strTaxID2 = "0";
            string strTaxID3 = "0";
            string strTaxName1 = "";
            string strTaxName2 = "";
            string strTaxName3 = "";
            string strTaxRate1 = "0";
            string strTaxRate2 = "0";
            string strTaxRate3 = "0";
            int iTaxType1 = 0;
            int iTaxType2 = 0;
            int iTaxType3 = 0;
            double TaxVal1 = 0;
            double TaxVal2 = 0;
            double TaxVal3 = 0;
            int intCount = 0;

            //Mix n Match

            int iMixID = 0;
            int iMixQty = 0;
            int iMixUnique = 0;
            double dMixVal = 0;
            string sMixType = "";
            string sMixFlag = "N";
            string sMixLast = "N";


            // Fees & Charges

            int iFeeID = 0;
            string sFeeLogic = "";
            double dFeeVal = 0;
            double dFeeTxRate = 0;
            double dFee = 0;
            double dFeeTx = 0;
            string sFeeText = "";
            string sFeeQty = "N";
            // Sale price

            int iSalePriceID = 0;

            int idtxid = 0;
            int idtxtype = 0;
            double ddtxrate = 0;
            double ddtx = 0;

            int iAge = 0;

            double dGRate = 0;
            double dGPrice = 0;

            DataTable dtblTaxHeader = new DataTable();
            PosDataObject.Tax objTax = new PosDataObject.Tax();
            objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblTaxHeader = objTax.FetchActiveTax();
            foreach (DataRow dr in dtblTaxHeader.Rows)
            {
                intCount++;
                if (intCount == 1)
                {
                    strTaxID1 = dr["ID"].ToString();
                    strTaxName1 = dr["TaxName"].ToString();
                    strTaxRate1 = dr["TaxRate"].ToString();
                }
                if (intCount == 2)
                {
                    strTaxID2 = dr["ID"].ToString();
                    strTaxName2 = dr["TaxName"].ToString();
                    strTaxRate2 = dr["TaxRate"].ToString();
                }
                if (intCount == 3)
                {
                    strTaxID3 = dr["ID"].ToString();
                    strTaxName3 = dr["TaxName"].ToString();
                    strTaxRate3 = dr["TaxRate"].ToString();
                    break;
                }
            }
            dtblTaxHeader.Dispose();

            foreach (DataRow drR in dtblPOSDatatbl.Rows)
            {
                iMixID = GeneralFunctions.fnInt32(drR["MIXMATCHID"].ToString());
                iMixQty = GeneralFunctions.fnInt32(drR["MIXMATCHQTY"].ToString());
                iMixUnique = GeneralFunctions.fnInt32(drR["MIXMATCHUNIQUE"].ToString());
                dMixVal = GeneralFunctions.fnDouble(drR["MIXMATCHVALUE"].ToString());

                sMixFlag = drR["MIXMATCHFLAG"].ToString();
                sMixLast = drR["MIXMATCHLAST"].ToString();
                sMixType = drR["MIXMATCHTYPE"].ToString();

                iFeeID = GeneralFunctions.fnInt32(drR["FEESID"].ToString());
                sFeeLogic = drR["FEESLOGIC"].ToString();
                dFeeVal = GeneralFunctions.fnDouble(drR["FEESVALUE"].ToString());
                dFeeTxRate = GeneralFunctions.fnDouble(drR["FEESTAXRATE"].ToString());
                dFee = GeneralFunctions.fnDouble(drR["FEES"].ToString());
                dFeeTx = GeneralFunctions.fnDouble(drR["FEESTAX"].ToString());
                sFeeText = drR["FEESTEXT"].ToString();
                sFeeQty = drR["FEESQTY"].ToString();

                iSalePriceID = GeneralFunctions.fnInt32(drR["SALEPRICEID"].ToString());

                idtxid = GeneralFunctions.fnInt32(drR["DTXID"].ToString());
                idtxtype = GeneralFunctions.fnInt32(drR["DTXTYPE"].ToString());
                ddtxrate = GeneralFunctions.fnDouble(drR["DTXRATE"].ToString());
                ddtx = GeneralFunctions.fnDouble(drR["DTX"].ToString());

                iAge = GeneralFunctions.fnInt32(drR["AGE"].ToString());

                dGRate = GeneralFunctions.fnDouble(drR["GRATE"].ToString());
                dGPrice = GeneralFunctions.fnDouble(drR["GPRICE"].ToString());


                DataTable dtblR = new DataTable();
                PosDataObject.POS objR = new PosDataObject.POS();
                objR.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int d = GeneralFunctions.fnInt32(drR["MATRIXOID"].ToString());
                dtblR = objR.FetchItemDetails(GeneralFunctions.fnInt32(drR["MATRIXOID"].ToString()));

                foreach (DataRow drR1 in dtblR.Rows)
                {
                    dtblFinal.Rows.Add(new object[] {
                                        drR1["ProductID"].ToString(),
                                        drR1["Description"].ToString(),
                                        drR1["ProductType"].ToString(),
                                        "0",
                                        "0",
                                        drR1["Cost"].ToString(),
                                        drR["Qty"].ToString(),
                                        drR1["Price"].ToString(),
                                        drR1["NormalPrice"].ToString(),
                                        drR1["Price"].ToString(),
                                        drR1["TaxID1"].ToString(),
                                        drR1["TaxID2"].ToString(),
                                        drR1["TaxID3"].ToString(),
                                        drR1["Taxable1"].ToString(),
                                        drR1["Taxable2"].ToString(),
                                        drR1["Taxable3"].ToString(),
                                        drR1["TaxRate1"].ToString(),
                                        drR1["TaxRate2"].ToString(),
                                        drR1["TaxRate3"].ToString(),
                                        drR1["SKU"].ToString(),
                                        drR1["DepartmentID"].ToString(),
                                        drR1["CategoryID"].ToString(),
                                        drR1["UOMCount"].ToString(),
                                        drR1["UOMPrice"].ToString(),
                                        drR1["UOMDesc"].ToString(),
                                        drR1["MatrixOptionID"].ToString(),
                                        drR1["OptionValue1"].ToString(),
                                        drR1["OptionValue2"].ToString(),
                                        drR1["OptionValue3"].ToString(),
                                        drR["MATRIXOID"].ToString(),
                                        "0","0","0","0","0","0","","0",
                                        drR["NOTES"].ToString(),
                                        drR1["DiscLogic"].ToString(),
                                        drR1["DiscValue"].ToString(),
                                        drR1["Discount"].ToString(),
                                        drR1["DiscountID"].ToString(),
                                        drR1["DiscountText"].ToString(),"1",
                                        drR["RENTTYPE"].ToString(),
                                        drR["RENTDURATION"].ToString(),
                                        drR["RENTAMOUNT"].ToString(),
                                        drR["RENTDEPOSIT"].ToString(),
                                        iTaxType1,TaxVal1,iTaxType2,TaxVal2,iTaxType3,TaxVal3,
                                        iMixID,sMixFlag,sMixType,dMixVal,iMixQty,iMixUnique,sMixLast,
                                        iFeeID,sFeeLogic,dFeeVal,dFeeTxRate,dFee,dFeeTx,sFeeText,sFeeQty,
                                        iSalePriceID,idtxid,idtxtype,ddtxrate,ddtx,
                                        drR["EDITF"].ToString(),
                                        drR["PROMPTPRICE"].ToString(),
                                        drR["BUYNGETFREEHEADERID"].ToString(),
                                        drR["BUYNGETFREECATEGORY"].ToString(),
                                        drR["BUYNGETFREENAME"].ToString(),iAge,
                                        dGRate,dGPrice,
                                        drR["UOM"].ToString()});
                }
                dtblR.Dispose();
            }

            intTaxID1 = GeneralFunctions.fnInt32(strTaxID1);
            intTaxID2 = GeneralFunctions.fnInt32(strTaxID2);
            intTaxID3 = GeneralFunctions.fnInt32(strTaxID3);

            if (strTaxExempt == "N")
            {
                DataTable dtblTax = grdTax.ItemsSource as DataTable;
                foreach (DataRow drTax in dtblTax.Rows)
                {
                    if (drTax["DTAX"].ToString() == "Y") continue;
                    if (drTax["TAXID"].ToString() == strTaxID1) dblTax1 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(drTax["Amount"].ToString()));
                    if (drTax["TAXID"].ToString() == strTaxID2) dblTax2 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(drTax["Amount"].ToString()));
                    if (drTax["TAXID"].ToString() == strTaxID3) dblTax3 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(drTax["Amount"].ToString()));
                }
                dtblTax.Dispose();
            }

            return dtblFinal;
        }

        private void ExecutePartRefund()
        {
            GetValue();
            ArrangeSettings();
            dtblTender = new DataTable();
            dtblTender.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("TENDER", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("DISPLAY", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("AMOUNT", System.Type.GetType("System.Double"));
            dtblTender.Columns.Add("GIFTCERTIFICATE", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("NEWGC", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("OLDGC", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("OLDGCAMT", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("CCTRANNO", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("GCSTORE", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("MANUAL", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("PROCESSCARD", System.Type.GetType("System.String"));
            dtblTender.Columns.Add("XeConnectID", System.Type.GetType("System.String"));

            PosDataObject.POS objpos1 = new PosDataObject.POS();
            objpos1.Connection = SystemVariables.Conn;
            DataTable dtblT = new DataTable();
            dtblT = objpos1.GetTenderInfoForPartRefund(_invoiceNo);

            foreach(DataRow dr in dtblT.Rows)
            {
                dtblTender.Rows.Add(new object[] { dr["TID"].ToString(), dr["TName"].ToString(), dr["TName"].ToString(),
                _amount,
                "","","","","","","N","N","0"});
            }

            if (dtblTender.Rows.Count == 0)
            {
                PosDataObject.POS objpos2 = new PosDataObject.POS();
                objpos2.Connection = SystemVariables.Conn;
                DataTable dtblT2 = new DataTable();
                dtblT2 = objpos2.GetTenderInfoForPartRefund1(_invoiceNo);

                foreach (DataRow dr in dtblT2.Rows)
                {
                    dtblTender.Rows.Add(new object[] { dr["TID"].ToString(), dr["TName"].ToString(), dr["TName"].ToString(),
                _amount,
                "","","","","","","N","N","0"});
                }
            }

            intCustID = objpos1.GetCustomerIDForPartRefund(_invoiceNo);

            string srterrmsg = "";
            PosDataObject.POS objpos = new PosDataObject.POS();
            objpos.Connection = SystemVariables.Conn;
            objpos.EmployeeID = SystemVariables.CurrentUserID;
            objpos.LoginUserID = SystemVariables.CurrentUserID;
            objpos.CustomerID = intCustID;
            objpos.Return = true;
            objpos.ParentReturnInvNo = _invoiceNo;
            objpos.NewLayaway = false;
            objpos.Layaway = false;
            objpos.LayawayRefund = false;
            objpos.RentReturn = false;
            objpos.ServiceType = "Sales";
            objpos.MGCIssue = false;
            objpos.MercuryGCIssueCardID = tempMercuryGCCardID;

            objpos.TransType = 1; // sale
            objpos.Status = 3;

            objpos.RentalSecurityDeposit = dblRentSecurityDeposit;
            objpos.IssueRentInvNo = intIssueRentInvNo;
            objpos.IsRentCalculated = Settings.CalculateRentLater;

            DateTime rpin = Convert.ToDateTime(null);
            DateTime rpdelvy = Convert.ToDateTime(null);
            DateTime rpnotf = Convert.ToDateTime(null);
            string strrpnotes1 = "";
            string strrpnotes2 = "";
            string strrpnotes3 = "";
            string strrpritm = "";
            string strrprsl = "";
            string strrpfind = "";

            

            objpos.RepairFindUs = strrpfind;
            objpos.RepairItemName = strrpritm;
            objpos.RepairItemSL = strrprsl;
            objpos.RepairDateIn = rpin;
            objpos.RepairDeliveryDate = rpdelvy;
            objpos.RepairNotifiedDate = rpnotf;
            objpos.RepairProblem = strrpnotes1;
            objpos.RepairNotes = strrpnotes2;
            objpos.RepairRemarks = strrpnotes3;

            objpos.RepairAmount = dblRepairAmount;
            objpos.RepairAdvanceAmount = dblRepairAdvance;

            objpos.RepairTenderAmount = dblRepairTender;
            objpos.IssueRepairInvNo = intIssueRepairInvNo;

            objpos.ReceiptCnt = 1;

            objpos.TotalSale = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numTotalsale.Text.Substring(numTotalsale.Text.IndexOf(" ") + 1)));

            

            objpos.Discount = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numDiscount.Text.Substring(numDiscount.Text.IndexOf(" ") + 1)));
            objpos.Coupon = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(numCoupon.Text.Substring(numCoupon.Text.IndexOf(" ") + 1)));
            objpos.CouponPerc = dblCouponPerc;
            objpos.DiscountReason = strDiscountReason;

            objpos.TotalFees = dblFees;
            objpos.TotalFeesTax = dblFeesTax;

            objpos.TotalFeesCoupon = dblFeesCouponAmount;
            objpos.TotalFeesCouponTax = dblFeesCouponTaxAmount;

            objpos.DTaxID = CustDTaxID;
            objpos.DTax = dblDTx;


            pdtblForStockUpdate = FinalDataTable();
            objpos.ItemDataTable = pdtblForStockUpdate;

            objpos.TenderDataTable = dtblTender;
            objpos.TaxID1 = intTaxID1;
            objpos.TaxID2 = intTaxID2;
            objpos.TaxID3 = intTaxID3;
            objpos.Tax1 = GeneralFunctions.FormatDouble(dblTax1);
            objpos.Tax2 = GeneralFunctions.FormatDouble(dblTax2);
            objpos.Tax3 = GeneralFunctions.FormatDouble(dblTax3);

            objpos.ChangeAmount = 0;
            objpos.ApptDataTable = null;
            objpos.ErrorMsg = "";

            // static value
            objpos.StoreID = 1;
            objpos.RegisterID = 1;
            objpos.CloseoutID = GeneralFunctions.GetCloseOutID();
            objpos.TransNoteNo = 0;
            objpos.LayawayNo = 0;
            objpos.TransMSeconds = 0;
            // static value

            objpos.TerminalName = Settings.TerminalName;

            objpos.CardTranID = intCardTranID;

            objpos.ChangedByAdmin = 0;
            objpos.FunctionButtonAccess = false;

            if (Settings.AcceptTips == "Y")
            {
                objpos.AuthorisedTranNo = "";
                objpos.SaleTranNo = "";
            }
            else
            {
                objpos.AuthorisedTranNo = "";
                objpos.SaleTranNo = "";
            }

            objpos.tblCardID = null;
            objpos.MercuryGiftCardDataTable = null;

            objpos.GCCentralFlag = Settings.CentralExportImport;
            objpos.GCOPStore = GCOPSTORE;

            objpos.OperateStore = Settings.StoreCode;

            objpos.BeginTransaction();

            if (objpos.CreateInvoice())
            {
                intINV = objpos.ID;
            }
            objpos.EndTransaction();
            srterrmsg = objpos.ErrorMsg;
        }

        #endregion
    }
}
