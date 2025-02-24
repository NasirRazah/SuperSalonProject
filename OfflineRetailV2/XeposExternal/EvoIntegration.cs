using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.XeposExternal
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
        private static string BaseAddress = OfflineRetailV2.Data.Settings.EvoApi; //  "http://localhost:5050/api/";

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

                        //var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(authorizeCaptureInput);
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);

                        // Creae a new extended function and add the Refnum param we will send the param in Ref Num =  _invoiceNo.ToString()
                        // set instance = "EvoTransactionLogs"
                        //GeneralFunctions.ExecuteXeconnectLog("AuthorizeCapture : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);

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
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);

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
                        //var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(refundInput);
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);

                        
                        //GeneralFunctions.ExecuteXeconnectLog("Refund : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);

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
                       
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(getTransactionInfoResponse);


                        //GeneralFunctions.ExecuteXeconnectLog("GetTransactionInfo : ResponseResult --> " + resDTO, 0);

                        if (getTransactionInfoResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return await content.ReadAsStringAsync();
                        }

                        return "\"Request not Completed yet\"";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                        //var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(printingDtoInput);
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);


                        //GeneralFunctions.ExecuteXeconnectLog("SetDefaultPrinter : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"Setting default printer failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                       
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);

                        //GeneralFunctions.ExecuteXeconnectLog("GetRecipetHeaders : " + url + "\nResponseResult --> " + resDTO, 0);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var a = await content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<ReceiptHeader>(a);
                        }

                        return new ReceiptHeader();
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                        //var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(receiptHeaderInput);
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                        //GeneralFunctions.ExecuteXeconnectLog("SetRecipetHeaders : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"Setting default printer failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                        //var reqDTO = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);

                        //GeneralFunctions.ExecuteXeconnectLog("SetConfiguration : Request --> " + reqDTO + "\nResponseResult --> " + resDTO, 0);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }

                        return $"set configuration failed! Error: {response.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                        
                        //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(response);


                        //GeneralFunctions.ExecuteXeconnectLog("GetConfiguration : Request --> " + url + "\nResponseResult --> " + resDTO, 0);

                        var model = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<EvoCommerceDriverConfigDto>(model);
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
                return new EvoCommerceDriverConfigDto();
            }
        }

        public static async Task<string> ClearResponse()
        {
            try
            {
                string clearResponseUrl = BaseAddress + "Xepos/ClearResponse";
                //GeneralFunctions.ExecuteXeconnectLog(clearResponseUrl, 0);
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage clearResponseResponse = await client.GetAsync(clearResponseUrl))
                    using (clearResponseResponse.Content)
                    {
                        //var responsejson = Newtonsoft.Json.JsonConvert.SerializeObject(clearResponseResponse);
                        //GeneralFunctions.ExecuteXeconnectLog(responsejson, 0);

                        if (clearResponseResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return "Ok";
                        }
                        return $"Clear Failed! Error: {clearResponseResponse.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
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
                       //var resDTO = Newtonsoft.Json.JsonConvert.SerializeObject(cancelTransactionResponse);
                       // GeneralFunctions.ExecuteXeconnectLog("CancelTransaction : Request --> " + cancelTransactionUrl + "\nResponseResult --> " + resDTO, 0);

                        if (cancelTransactionResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return cancelTransactionContent.ReadAsStringAsync().Result;
                        }

                        return $"Clear Failed! Error: {cancelTransactionResponse.StatusCode.ToString()}";
                    }
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
                return "Failed to Connect XEConnect";
            }
        }


        public static async Task<bool> IsEService()
        {
            try
            {
                string cancelTransactionUrl = BaseAddress + "Xepos/Evo/IsEService";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage cancelTransactionResponse = await client
                        .GetAsync(cancelTransactionUrl))
                    using (HttpContent cancelTransactionContent = cancelTransactionResponse.Content)
                    {
                        if (cancelTransactionResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}