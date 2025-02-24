using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_BrandDlg.xaml
    /// </summary>
    public partial class frm_VersionUpdateDlg : Window
    {
        private bool boolUpdateComplete = false;
        public bool UpdateComplete
        {
            get { return boolUpdateComplete; }
            set { boolUpdateComplete = value; }
        }
        private DispatcherTimer _versionTimerNet;

        private long MsiSize = 0;
        private readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private bool booldownloadCompleted = false;
        private bool booldownloadStarted = false;

        public frm_VersionUpdateDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            if (booldownloadCompleted)
            //CloseKeyboards();
            Close();
        }

       

      

       
        
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #region Version Update

        private bool CheckLatestVersion(bool silent)
        {
            try
            {
                bool boolResult = false;
                Cursor = Cursors.Wait;

                string strresult = ""; string strCurrentVersion = "";

                long versiontxtfile = GetFtpFileSizeVersionText(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));
                long msiFile = GetFtpFileSizeMsi(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));

                if ((versiontxtfile.ToString() == "0") || (msiFile.ToString() == "0"))
                {

                }
                else
                {
                    using (var client = new WebClient())
                    {
                        //strresult = client.DownloadString("http://www.photosurfer.com/PowerSourceUpdate/version.txt");

                        client.Credentials = new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@");
                        strresult = client.DownloadString("ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt");


                    }



                    if (strresult != "")
                    {
                        strCurrentVersion = GeneralFunctions.VersionInfoForUpdate();
                        strresult = strresult.Replace(".", "");
                        strCurrentVersion = strCurrentVersion.Replace(".", "");
                        if (Convert.ToInt16(strCurrentVersion) < Convert.ToInt16(strresult))
                        {
                            //_versionTimer.Stop();

                            if (!(silent))
                            {
                                DocMessage.MsgInformation(SystemVariables.BrandName + " Application will be updated");
                                //CustMessage.MsgInformation("PowerSource Application will be updated");


                            }
                            _versionTimerNet.Start();
                            MsiSize = GetFtpFileSize(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));

                            booldownloadStarted = true;
                            WebClient DownloadClient = new WebClient();
                            DownloadClient.Credentials = new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@");
                            DownloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadClient_DownloadProgress);
                            DownloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadClient_DownloadDataCompleted);


                            DownloadClient.DownloadFileAsync(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"),
                            GeneralFunctions.DownloadPath() + "Retail2020Setup.msi");



                            txtVersionUpdateCaption.Text = "Downloading Version Update ...";
                            /*tcSearch.SelectedTabPage = tpLookup;
                            tcSearch.Enabled = false;
                            pnlBottom.Enabled = false;
                            barProgress.Visible = true;
                            lblProgress.Visible = true;
                            lblUpdate.Visible = true;*/
                            boolResult = true;
                        }
                    }
                }
                return boolResult;

            }
            catch (Exception ex)
            {
                booldownloadCompleted = true;
                //pnlAutoUpdate.Visibility = Visibility.Collapsed;
                DocMessage.MsgError("Download fails.");
                //_versionTimer.Start();
                Cursor = Cursors.Arrow;
                return false;
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }


        private long GetFtpFileSizeVersionText(Uri requestUri, NetworkCredential networkCredential)
        {

            var ftpWebRequest = GetFtpWebRequest(requestUri, networkCredential, WebRequestMethods.Ftp.GetFileSize);

            try { return ((FtpWebResponse)ftpWebRequest.GetResponse()).ContentLength; } //Incase of success it'll return the File Size.
            catch (Exception) { return default(long); } //Incase of fail it'll return default value to check it later.
        }

        private long GetFtpFileSizeMsi(Uri requestUri, NetworkCredential networkCredential)
        {

            var ftpWebRequest = GetFtpWebRequest(requestUri, networkCredential, WebRequestMethods.Ftp.GetFileSize);

            try { return ((FtpWebResponse)ftpWebRequest.GetResponse()).ContentLength; } //Incase of success it'll return the File Size.
            catch (Exception) { return default(long); } //Incase of fail it'll return default value to check it later.
        }

        private long GetFtpFileSize(Uri requestUri, NetworkCredential networkCredential)
        {
            //Create ftpWebRequest object with given options to get the File Size. 
            var ftpWebRequest = GetFtpWebRequest(requestUri, networkCredential, WebRequestMethods.Ftp.GetFileSize);

            try { return ((FtpWebResponse)ftpWebRequest.GetResponse()).ContentLength; } //Incase of success it'll return the File Size.
            catch (Exception) { return default(long); } //Incase of fail it'll return default value to check it later.
        }
        private FtpWebRequest GetFtpWebRequest(Uri requestUri, NetworkCredential networkCredential, string method = null)
        {
            var ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUri); //Create FtpWebRequest with given Request Uri.
            ftpWebRequest.Credentials = networkCredential; //Set the Credentials of current FtpWebRequest.

            if (!string.IsNullOrEmpty(method))
                ftpWebRequest.Method = method; //Set the Method of FtpWebRequest incase it has a value.
            return ftpWebRequest; //Return the configured FtpWebRequest.
        }

        private string IsUpdateAvailable()
        {
            try
            {
                string boolResult = "";
                string strresult = ""; string strCurrentVersion = "";
                using (var client = new WebClient())
                {
                    //strresult = client.DownloadString("http://www.photosurfer.com/PowerSourceUpdate/version.txt");



                    client.Credentials = new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@");

                    strresult = client.DownloadString("ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt");
                }

                if (strresult != "")
                {
                    string NewVersion = strresult;
                    strCurrentVersion = GeneralFunctions.VersionInfoForUpdate();
                    strresult = strresult.Replace(".", "");
                    strCurrentVersion = strCurrentVersion.Replace(".", "");
                    if (Convert.ToInt16(strCurrentVersion) < Convert.ToInt16(strresult))
                    {
                        boolResult = NewVersion;
                    }
                }

                return boolResult;

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                return "";
            }
        }


        private void DownloadClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                pnlAutoUpdate.Visibility = Visibility.Collapsed;
                //barProgress.Visible = false;
                //lblProgress.Visible = false;


                //byte[] raw = e.Result;

                //CustMessage.MsgInformation("Download Complete");
                System.Diagnostics.Process.Start(GeneralFunctions.DownloadPath() + "Retail2020Setup.msi");
                booldownloadCompleted = true;
                boolUpdateComplete = true;
                //_versionTimer.Stop();
                Close();
            }
            catch
            {
                DocMessage.MsgError("Can not Connect to Server");
                Close();
            }

        }

        private void DownloadClient_DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
           
            //barProgress.Value = e.ProgressPercentage;
            Duration duration = new Duration(TimeSpan.FromSeconds(20));
            double pval = Math.Ceiling(((double)e.BytesReceived / (double)MsiSize) * 100);
            barProgress.Value = pval;
            barProgress.Refresh();
            lblProgress.Text = SizeSuffix(e.BytesReceived) + " of " + SizeSuffix(MsiSize);

        }


        private string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _versionTimerNet = new DispatcherTimer();
            _versionTimerNet.Interval = TimeSpan.Parse("0:0:1");
            _versionTimerNet.Tick += delegate (object obj, EventArgs ea) { CheckInternet(); };

            if (CheckLatestVersion(true))
            {
                return;
            }
        }

        private void CheckInternet()
        {

            //_versionTimerNet.Start();
            if (InternetChecker.IsConnectedToInternet())
            {

            }
            else
            {
                _versionTimerNet.Stop();
                DocMessage.MsgError("Can not Connect to Server");

                Close();
            }
        }
    }

    public class InternetChecker
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

    }
}
