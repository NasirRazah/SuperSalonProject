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

namespace pos.XeposExternal
{
    public class XeConnectIntegration
    {
        private const string XeConnectFilePath = @"C:\localprinters\XeposExternal.exe";

        private const string BaseAddress = "http://localhost:5050/api/";

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
            }
            catch (Exception)
            {
                ResMan.MessageBox.Show(OfflineRetailV2.Properties.Resources.XeposExternalCanNotExecute, OfflineRetailV2.Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
            catch (Exception)
            {
                ResMan.MessageBox.Show(OfflineRetailV2.Properties.Resources.XeposExternalClosed, OfflineRetailV2.Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(serverUpUrl))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    if (result != null)
                    {
                        return Convert.ToBoolean(result);
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}