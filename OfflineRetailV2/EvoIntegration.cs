using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace pos.XeposExternal
{
    public class AuthorizeCaptureDto
    {
        public decimal Amount { get; set; }

        public string OrderNo { get; set; }

        public decimal TipAmount { get; set; }

        public decimal CashbackAmount { get; set; }

        public string EmployeeId { get; set; }

        public string RefrenceNo { get; set; }

        public string LaneId { get; set; }
    }

    public class RefundDto
    {
        public decimal Amount { get; set; }

        public string TransactionId { get; set; }
    }

    public class VoidDto
    {
        public int Reason { get; set; }

        public string TransactionId { get; set; }
    }

    public class XeResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string ApprovalCode { get; set; }

        public string AvsResultActualResult { get; set; }

        public string AvsResultPostalCodeResult { get; set; }

        public string TransactionStatusCode { get; set; }

        public string BatchId { get; set; }

        public string CvResult { get; set; }

        public string StatusMessage { get; set; }

        public string TransactionId { get; set; }
    }

    public class PrinterSetting
    {
        public string DefaultPrinterName { get; set; }

        public int PageSize { get; set; }
    }

    public class ReceiptHeader
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string VatNumber { get; set; }
    }

    public class EvoCommerceDriverConfigDto
    {
        public string ServiceKey
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ServiceId
        {
            get;
            set;
        }

        public string MerchantProfileId
        {
            get;
            set;
        }

        public string ApplicationProfileId
        {
            get;
            set;
        }

        public string TerminalName
        {
            get;
            set;
        }

        public string ComPort
        {
            get;
            set;
        }

        public bool TestMode
        {
            get;
            set;
        }
    }

    class EvoIntegration
    {
        private const string BaseAddress = "http://localhost:5050/api/";

        public static async Task<string> AuthorizeCaptureAsync(AuthorizeCaptureDto authorizeCaptureInput)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/AuthorizeCapture";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(authorizeCaptureInput), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string result = await content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(result))
                            {
                                return result;
                            }
                            return "Waiting for payment";
                        }
                        return $"Connection Failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<string> VoidAsync(VoidDto voidInput)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/Void";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(voidInput), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string result = await content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(result))
                            {
                                return result;
                            }
                            return "Ok";
                        }

                        return $"Void Failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<string> RefundAsync(RefundDto refundInput)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/Refund";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(refundInput), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string result = await content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(result))
                            {
                                return result;
                            }
                            return "Ok";
                        }
                        return $"Void Failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<string> GetTransactionInfo()
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/GetTransactionInfo";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage getTransactionInfoResponse = client.GetAsync(url).Result)
                    using (HttpContent content = getTransactionInfoResponse.Content)
                    {
                        if (getTransactionInfoResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return await content.ReadAsStringAsync();
                        }

                        return "\"Request not Completed yet\"";
                    }
                }
            }
            catch (Exception)
            {
                return "\"Request not Completed yet\"";
            }
        }

        public static async Task<string> SetDefaultPrinter(PrinterSetting printingDtoInput)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/SetDefaultPrinter";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(printingDtoInput), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"Setting default printer failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<ReceiptHeader> GetRecipetHeaders()
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/GetRecipetHeaders";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.PostAsync(url, new StringContent("")))
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var a = await content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<ReceiptHeader>(a);
                        }

                        return new ReceiptHeader();
                    }
                }
            }
            catch (Exception)
            {
                return new ReceiptHeader();
            }
        }

        public static async Task<string> SetRecipetHeaders(ReceiptHeader receiptHeaderInput)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/SetRecipetHeaders";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(receiptHeaderInput), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"Setting default printer failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<string> SetConfiguration(EvoCommerceDriverConfigDto input)
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/SetConfiguration";
                using (HttpClient client = new HttpClient())
                {
                    var requestParams = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(url, requestParams))
                    using (response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"set configuration failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<EvoCommerceDriverConfigDto> GetConfiguration()
        {
            try
            {
                string url = BaseAddress + "Xepos/Evo/GetConfiguration";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    using (HttpContent content = response.Content)
                    {
                        var model = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<EvoCommerceDriverConfigDto>(model);
                    }
                }
            }
            catch (Exception)
            {
                return new EvoCommerceDriverConfigDto();
            }
        }

        public static async Task<string> ClearResponse()
        {
            try
            {
                string clearResponseUrl = BaseAddress + "Xepos/ClearResponse";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage clearResponseResponse = await client.GetAsync(clearResponseUrl))
                    using (clearResponseResponse.Content)
                    {
                        if (clearResponseResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }
                        return $"Clear Failed! Error: {clearResponseResponse.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }

        public static async Task<string> CancelTransaction()
        {
            try
            {
                string cancelTransactionUrl = BaseAddress + "Xepos/Evo/CancelTransaction";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage cancelTransactionResponse = await client
                        .PostAsync(cancelTransactionUrl, new StringContent(string.Empty)))
                    using (HttpContent cancelTransactionContent = cancelTransactionResponse.Content)
                    {
                        if (cancelTransactionResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return cancelTransactionContent.ReadAsStringAsync().Result;
                        }

                        return $"Clear Failed! Error: {cancelTransactionResponse.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Connect XEConnect";
            }
        }
    }
}