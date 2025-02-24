using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using OfflineRetailV2;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.XeposExternal
{
    public class XeConnectIntegration
    {
        private static string XeConnectFilePath = OfflineRetailV2.Data.Settings.EvoConnectFileLocation; //@"C:\localprinters\XeposExternal.exe";

        private static string BaseAddress = OfflineRetailV2.Data.Settings.EvoApi; //"http://localhost:5050/api/";

        private static bool IsXeConnectFileExist()
        {
            return File.Exists(XeConnectFilePath);
        }

        public static void RunXeConnect()
        {
            try
            {
                if (IsXeConnectFileExist())
                {
                    ProcessStartInfo start = new ProcessStartInfo { FileName = XeConnectFilePath };
                    Process.Start(start);
                }
                else
                {
                    //GeneralFunctions.ExecuteXeconnectLog("Try to run XeposExternal.exe : XeposExternal.exe not found", 1);
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
                ResMan.MessageBox.Show("XeposExternal.exe can't be executed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async void CloseXeConnect()
        {
            try
            {
                string closeUrl = BaseAddress + "Xepos/Close";
                using (HttpClient client = new HttpClient())
                {
                    await client.GetAsync(closeUrl);
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
                ResMan.MessageBox.Show("XeposExternal.exe can't be closed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void RestartXeConnect()
        {
            CloseXeConnect();
            RunXeConnect();
        }

        public static async Task<bool> IsServerUp()
        {
            try
            {
                string serverUpUrl = BaseAddress + "Xepos/IsServerUp";

                //GeneralFunctions.ExecuteXeconnectLog(serverUpUrl, 0);

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(serverUpUrl))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    var responsejson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                    //GeneralFunctions.ExecuteXeconnectLog(responsejson, 0);
                    if (result != null)
                    {
                        return Convert.ToBoolean(result);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                //GeneralFunctions.ExecuteXeconnectLog(ex.Message, 1);
                return false;
            }
        }
    }
}