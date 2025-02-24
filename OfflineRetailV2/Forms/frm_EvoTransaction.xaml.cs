using Dapper;
using OfflineRetailV2.Data;
using pos.XeposExternal;
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
using System.Windows.Threading;

namespace OfflineRetailV2.Forms
{
    /// <summary>
    /// Interaction logic for frm_EvoTransaction.xaml
    /// </summary>
    public partial class frm_EvoTransaction : Window
    {
        public frm_EvoTransaction()
        {
            InitializeComponent();
        }
        private readonly DispatcherTimer _timer;
        private readonly decimal _amount;
        private readonly int _invoiceNo;
        private readonly bool _isRefundOrVoid;

        public frm_EvoTransaction(decimal amount, int invoiceNo, bool isRefundOrVoid)
        {
            InitializeComponent();

            ContentTextBlock.Text = "Starting transaction, Please wait ...";
            ResizeMode = ResizeMode.NoResize;
            CloseButton.Content = "Cancel";

            _amount = amount;

            _isRefundOrVoid = isRefundOrVoid;

            _invoiceNo = invoiceNo;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            _timer.Tick += (sender, args) => OnTimerTick();
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
                    var authorizeCaptureResult = await EvoIntegration.AuthorizeCaptureAsync(
                        new AuthorizeCaptureDto
                        {
                            Amount = _amount,
                            CashbackAmount = 0,
                            EmployeeId = SystemVariables.CurrentUserID.ToString(),
                            LaneId = SystemVariables.CurrentUserID.ToString(),
                            OrderNo = _invoiceNo.ToString(),
                            RefrenceNo = _invoiceNo.ToString()
                        });

                    ContentTextBlock.Text = authorizeCaptureResult;

                    _timer.IsEnabled = true;
                }
                else
                {
                    ResMan.MessageBox.Show(clearResponseResult, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    _timer.IsEnabled = false;

                    Close();
                }
            }
            catch (Exception ex)
            {
                ResMan.MessageBox.Show("Something went wrong!\nPlease contact support team.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                _timer.IsEnabled = false;

                Close();
            }
        }

        private async Task StartRefund(int invoiceNo)
        {
            string transactionId = GetTransactionId(invoiceNo);
            if (string.IsNullOrEmpty(transactionId))
            {
                ContentTextBlock.Text = "Transaction Id not found!";

                CloseButton.Content = "Ok";
                return;
            }

            ContentTextBlock.Text = "Refund is in progress...";

            CloseButton.Content = "Cancel";

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

                _timer.IsEnabled = false;

                CloseButton.Content = "OK";

                if (transactionInfoResult.StatusCode == 0 || transactionInfoResult.StatusCode == 3)
                {
                    if (_isRefundOrVoid)
                    {
                        PosDataObject.POS objp = new PosDataObject.POS();
                        objp.Connection = SystemVariables.Conn;
                        objp.LoginUserID = SystemVariables.CurrentUserID;
                        objp.VoidTransaction(_invoiceNo, Settings.TerminalName);
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
                        InvoiceNumber = _invoiceNo
                    };

                    SaveTransactionDetail(transactionInfo);
                }
            }
        }

        private async void CloseButton_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Content == "Cancel")
            {
                string cancelTransactionResult = await EvoIntegration.CancelTransaction();

                if (cancelTransactionResult != "\"Ok\"")
                    ResMan.MessageBox.Show(cancelTransactionResult, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Close();
        }

        private void SaveTransactionDetail(TransactionModel transactionInfo)
        {
            DataTable connectionStringDataTable = ConfigSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionStringDataTable.Rows[0].ItemArray[0].ToString()))
            {
                string insertIntoXeConnectTransactionQuery = @"INSERT INTO [XeConnectTransactions]([Id],[InvoiceNumber],[TransactionId],[ProfileId],[StatusCode],[StatusMessage],[ApprovalCode],[Amount],[AVSResult_ActualResult],[AVSResult_PostalCodeResult],[BatchId],[CVResult]) VALUES (@Id,@InvoiceNumber,@TransactionId,@ProfileId,@StatusCode,@StatusMessage,@ApprovalCode,@Amount,@AVSResult_ActualResult,@AVSResult_PostalCodeResult,@BatchId,@CVResult)";

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
    }
}
