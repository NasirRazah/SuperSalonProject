// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
// Sam Park @ Mental Code Master 2019
//--
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Xpf.Grid;
using Microsoft.PointOfService;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

using OfflineRetailV2.Data;
using OfflineRetailV2.UserControls;
using OfflineRetailV2.UserControls.POSSection;

using System.IO;
using DevExpress.Xpf.Core;
using Settings = OfflineRetailV2.Data.Settings;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Net;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Timers;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// /// </summary>
    public partial class MainWindow : Window
    {
        UserControls.POSControl POSControl = null;
        UserControls.frm_MainAdmin frm_MainAdmin = null;
        UserControls.ClockInClockOutEmployeeControl ClockInClockOutEmployeeControl = null;
        UserControls.frmCloseoutDlg_CashOut frmCloseoutDlg_CashOut = null;
        UserControls.frm_DashboardDlg_SalesSnapshot frm_DashboardDlg_SalesSnapshot = null;

        private bool IsNewVerion = false;
        private bool IsNewVerionCheck = false;
        private long MsiSize = 0;
        private readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private string strSelectedUserID = "";
        private bool blAttachToLWTimer = false;
        SerialPort _slport;
        private WinMediaPlayer winplay;
        //  MessageBoxLoadingWindow MBLW = null;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private DispatcherTimer _versionTimer;

        private DispatcherTimer _versionTimerNet;

        private bool booldownloadCompleted = false;
        private bool booldownloadStarted = false;

        private delegate void SetTextDeleg(string text);

        private double s_wght = 0;
        private string s_wght_u = "";
        private string weightstring = "";
        private bool blGetWeight = false;
        #region Other Variables

        private DataTable dS = null;
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private string strPhotoFile = "";

        private static string usercountConn = "";

        private bool invalidurl = false;
        private bool blinserturl = false;
        private bool isdropdown = false;
        private DataTable dtblurl = null;

        #endregion Other Variables
        private static bool boolFindConfig = false;
        private static readonly object padlock = new object();
        private static MainWindow instance = null;
        public static MainWindow Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainWindow();
                    }
                    return instance;
                }
            }
        }


        #region ProcessMonitor
        private static System.Timers.Timer checkProcessTimer;
        private const string processName = "Launch Till";
        private const string logFilePath = "process_monitor.log"; // Path to the log file

        public void ProcessMonitor()
        {

            checkProcessTimer = new System.Timers.Timer(900000);
            checkProcessTimer.Elapsed += CheckProcessStatus;
            checkProcessTimer.AutoReset = true;
            checkProcessTimer.Enabled = true;
            LogMessage("Monitoring started.");
        }
        private static void CheckProcessStatus(object sender, ElapsedEventArgs e)
        {
            // Get the list of processes by name
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                string message = "Target process not found. Exiting application.";
                Console.WriteLine(message);
                LogMessage(message);
                Environment.Exit(0);
            }
            else
            {
                string message = "Target process is running.";
                Console.WriteLine(message);
                LogMessage(message);
            }
        }

        private static void LogMessage(string message)
        {
            // Append the message to the log file with a timestamp
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now: yyyy-MM-dd HH:mm:ss}-{message}");
            }
        }

        #endregion


        private void CallMain()
        {
            GetProjectAssemblies("devexpress");
            Main(new string[] { });
        }

        public MainWindow()
        {
            DataControlBase.AllowInfiniteGridSize = true;
            InitializeComponent();
            ProcessMonitor();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MinHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            this.ResizeMode = ResizeMode.NoResize;
            this.Visibility = Visibility.Collapsed;
            // this.Topmost = true;

            CallMain();
            ////// re-show the window after changing style
            //this.Visibility = Visibility.Visible;
            MaxHeight = SystemParameters.VirtualScreenHeight; MaxWidth = SystemParameters.VirtualScreenWidth;

            if (boolFindConfig)
            {
                try
                {
                    SystemVariables.BrandName = ConfigSettings.GetBrandName();
                    SystemVariables.BrandShortName = ConfigSettings.GetShortBrandName();
                    SystemVariables.PageAdjustmentForPrint = ConfigSettings.GetPrintPageAdjustment();
                    SystemVariables.Country = ConfigSettings.GetCountry();
                    GeneralFunctions.SetCurrencyNDateformat();
                    SystemVariables.SelectedTheme = ConfigSettings.GetTheme();
                    //SystemVariables.CurrencySymbol = ConfigOfflineRetailV2.Data.Settings.GetCurrencySymbol();
                    //SystemVariables.DateFormat = ConfigOfflineRetailV2.Data.Settings.GetDateFormat();
                }
                catch
                {
                    SystemVariables.BrandName = "XEPOS Retail 2024";
                    SystemVariables.PageAdjustmentForPrint = 0;
                    SystemVariables.Country = "UK";
                    GeneralFunctions.SetCurrencyNDateformat();
                    SystemVariables.SelectedTheme = "Dark";
                }
            }

            double tempWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double tempHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            /*
            if ((tempWidth < 1024) || (tempHeight < 768))
            {
                new MessageBoxWindow().Show(Properties.Resources.Screen_resolution_must_be_set_to_1024_x_768_or_higher, Properties.Resources.Application_Validation, MessageBoxButton.OK, MessageBoxImage.Information);

                Application.Current.Shutdown();
            }*/

            DateTimeTimer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = new TimeSpan(0, 1, 0)
            };
            SetDateTime();
            DateTimeTimer.Tick += (s, e) =>
            {
                SetDateTime();
            };
            DateTimeTimer.Start();
            Closed += MainWindow_Closed;

            //LoginControl.txtUser.TextChanged += TxtUser_TextChanged;
            //LoginControl.txtPswd.PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
            //LoginControl.LoginButton.Click += LoginButton_Click;


            //  LoadAssemblies();

            //  26 July 22 
            //GetProjectAssemblies("devexpress");
            //Main(new string[] { });

            /*
            try
            {
                XeConnectIntegration.RunXeConnect();
            }
            catch
            {

            }*/
        }

        public static IEnumerable<Assembly> GetProjectAssemblies(string prefixName)
        {
            var assemblies = new HashSet<Assembly> {
                    Assembly.GetEntryAssembly()
                };
            try
            {
                for (int i = 0; i < assemblies.Count; i++)
                {
                    try
                    {
                        var assembly = assemblies.ElementAt(i);

                        var referencedProjectAssemblies = assembly.GetReferencedAssemblies()
                            .Where(assemblyName => assemblyName.FullName.ToLower().StartsWith(prefixName))
                            .Select(assemblyName => Assembly.Load(assemblyName));

                        assemblies.UnionWith(referencedProjectAssemblies);

                    }
                    catch (Exception ee)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return assemblies;
        }
        private static String[] GetLocalServers()
        {
            String[] Local = new String[100];
            string sval = "";
            string ival = "";
            RegistryKey RegRootKey = Registry.LocalMachine;

            sval = System.Environment.MachineName;

            RegistryKey RegInstanceKey = RegRootKey.OpenSubKey("Software\\Microsoft\\Microsoft SQL Server\\");

            if (RegInstanceKey != null)
            {
                foreach (String iarry in RegInstanceKey.GetValueNames())
                {
                    if (iarry.ToString() == "InstalledInstances")
                    {
                        int i = 0;
                        foreach (String ival1 in (String[])(RegInstanceKey.GetValue(iarry)))
                        {
                            ival = ival1.ToString();
                            string svr = sval + "\\" + ival;
                            Local.SetValue(svr, i);
                            i++;
                        }
                    }
                }
            }

            RegistryKey RegInstanceKey64 = RegRootKey.OpenSubKey("Software\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\");

            if (RegInstanceKey64 != null)
            {
                int i = 0;
                foreach (String iarry1 in RegInstanceKey64.GetSubKeyNames())
                {
                    if (iarry1.ToString() == "XEPOSRETAIL2020")
                    {
                        string svr = sval + "\\" + iarry1.ToString();
                        Local.SetValue(svr, i);
                        break;
                    }
                    i++;
                }
            }

            return Local;
        }
        private static string GetPOSServer()
        {
            bool ret = false;
            string strServer = "";

            String[] localinstance = GetLocalServers();
            foreach (string li in localinstance)
            {
                //if (li.ToString() == "") continue;
                if (li == null) continue;
                if (li.ToString().Contains("\\XEPOSRETAIL2020"))
                {
                    strServer = li.ToString().Remove(li.ToString().IndexOf("\\XEPOSRETAIL2020"));
                    ret = true;
                    break;
                }
            }

            if (!ret)
            {
                DataTable dtbl = SmoApplication.EnumAvailableSqlServers(false);

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["Instance"].ToString().ToUpper() == "XEPOSRETAIL2020")
                    {
                        strServer = dr["Server"].ToString();
                        ret = true;
                        break;
                    }
                }
            }
            return strServer;
        }
        public static bool SetGlobalConn()
        {
            string strconn = "";
            String ConnStr = "";
            System.Data.SqlClient.SqlConnection Conn;
            try
            {
                DataTable dtbl = new DataTable();
                dtbl = ConfigSettings.GetConnectionString();
                foreach (DataRow dr in dtbl.Rows)
                {
                    strconn = dr["conn"].ToString();
                }
                //string strconn = ConfigurationOfflineRetailV2.Data.Settings.AppOfflineRetailV2.Data.Settings.Get("ConnString").ToString();
                dtbl.Dispose();
                if (strconn == "server=%xy!z%\\XEPOSRETAIL2020;database=Retail2020DB;uid=sa;pwd=anymouse786")
                {
                    string servr = GetPOSServer();
                    if (servr != "")
                    {
                        ConfigSettings.RemoveConnection();
                        ConfigSettings.AddConnection("server=" + servr + "\\XEPOSRETAIL2020;database=Retail2020DB;uid=sa;pwd=anymouse786");
                        strconn = "server=" + servr + "\\XEPOSRETAIL2020;database=Retail2020DB;uid=sa;pwd=anymouse786";
                    }
                }
                ConnStr = strconn;
                usercountConn = ConnStr;
                Conn = new System.Data.SqlClient.SqlConnection(ConnStr);
                Conn.Open();
                Conn.Close();
                SystemVariables.ConnectionString = ConnStr;
                SystemVariables.Conn = Conn;
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public void Main(string[] args)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            btnFullScreen.Source = this.FindResource("FullScreenIn") as System.Windows.Media.ImageSource;

            PopulateThemes();

            boolFindConfig = GeneralFunctions.ExecuteCopyConfigFile();
            if (boolFindConfig)
            {
                GeneralFunctions.ExecuteCopyBackground();
                GeneralFunctions.ExecuteCopyLabelDesign();
                string strfilename = GeneralFunctions.GetUserBackground();
                if (!File.Exists(strfilename))
                {
                    var imgBrush = new ImageBrush();
                    imgBrush.ImageSource = this.FindResource("MainBackground") as ImageSource;
                    imgBrush.Stretch = Stretch.Fill;
                    LoginGrid.Background = imgBrush;
                }
                else
                {
                    var img = new System.Windows.Controls.Image();
                    img.Source = new BitmapImage(new Uri(strfilename, UriKind.Absolute));
                    var imgBrush = new ImageBrush();
                    imgBrush.ImageSource = GeneralFunctions.ConvertImageToImageSource(img);
                    imgBrush.Stretch = Stretch.Fill;
                    LoginGrid.Background = imgBrush;
                }
                string arg = "";
                try
                {
                    arg = args[0].ToString();
                }
                catch
                {
                    arg = "";
                }

                if (arg == "t")
                {
                    OfflineRetailV2.Data.Settings.disableresource = "Y";
                }
                else
                {
                    OfflineRetailV2.Data.Settings.disableresource = "N";
                }

                

                //if (CheckLatestVersion(false))
                //return;

                if (SetGlobalConn()) // connected to database
                {
                    //string textstr = Convert.ToDateTime("04-24-2024").ToString();

                    Dispatcher.BeginInvoke(new System.Action(() => SetApplicationThemeFromSetup()));

                    if (OfflineRetailV2.Data.Settings.disableresource == "Y")
                    {
                        OfflineRetailV2.Data.Settings.default_culture = "en-US";
                    }
                    else
                    {
                        OfflineRetailV2.Data.Settings.GetLanguage();
                    }
                    // Set Culture of the Application
                    OfflineRetailV2.Data.Settings.POS_Culture = OfflineRetailV2.Data.Settings.SetCulture();
                    Thread.CurrentThread.CurrentUICulture = OfflineRetailV2.Data.Settings.POS_Culture;
                    Thread.CurrentThread.CurrentCulture = OfflineRetailV2.Data.Settings.POS_Culture;
                    // Set Culture of the Application

                    AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);

                    // Check DB Script

                    frm_dbUpdation frmDbUpdation = new frm_dbUpdation();
                    frmDbUpdation.CheckDbVersions();

                    // If DbVersion Table not found, then Create it ... //

                    if (frmDbUpdation.strDbVersionResult.ToUpper() == "")
                    {
                        if (frmDbUpdation.CreateDbVersionTable())
                        {
                            frmDbUpdation.InsertDefaltDbVersionData();
                            frmDbUpdation.CheckDbVersions();
                        }
                    }
                    // End Create //

                    if ((frmDbUpdation.strDbVersionResult.ToUpper() == "OLD"))
                    {
                        if (frmDbUpdation.ShowDialog() == false)
                        {
                            Application.Current.Shutdown();
                            return;
                        }
                    }
                    else if (frmDbUpdation.strDbVersionResult.ToUpper() == "NEW")
                    {
                        if (new MessageBoxWindow().Show(Properties.Resources.The_database_format_is_newer_than_the_software_version_ + "\n" + Properties.Resources.the_system_may_not_function_properly__Continue__, Properties.Resources.Database_Update_Validation, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                        {
                            Application.Current.Shutdown();
                            return;
                        }
                    }
                    // Invalid Database //
                    else if (frmDbUpdation.strDbVersionResult.ToUpper() == "")
                    {
                        new MessageBoxWindow().Show(Properties.Resources.This_is_not_a_valid + " " + SystemVariables.BrandName + " " + Properties.Resources.Database, Properties.Resources.Database_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    // Check GwarePOS Registration

                    PosDataObject.Login objLogin = new PosDataObject.Login();
                    objLogin.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    OfflineRetailV2.Data.Settings.LoadRegistrationVariables();
                    OfflineRetailV2.Data.Settings.LoadMainHeader();
                    string KeyCode = "";
                    KeyCode = GeneralFunctions.CalculateKey();
                    int intRecCount = GeneralFunctions.GetRecordCount("REGISTRATION");
                    string RegModule = "";
                    string RegLabelDesigner = "";
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "Y") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "N") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "N")) RegModule = "";
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "N") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "Y") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "N")) RegModule = "10"; //10
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "Y") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "Y") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "N")) RegModule = "1000"; // 1000

                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "N") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "N") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "Y")) RegModule = "100000";
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "Y") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "N") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "Y")) RegModule = "10000000";
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "N") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "Y") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "Y")) RegModule = "1000000000";
                    if ((OfflineRetailV2.Data.Settings.RegPOSAccess == "Y") && (OfflineRetailV2.Data.Settings.RegScaleAccess == "Y") && (OfflineRetailV2.Data.Settings.RegOrderingAccess == "Y")) RegModule = "100000000000";

                    if (OfflineRetailV2.Data.Settings.RegLabelDesigner == "Y") RegLabelDesigner = ""; else RegLabelDesigner = "100";

                    int intReturnCode = 0;
                    intReturnCode = GeneralFunctions.IsRegistrationValid(KeyCode, OfflineRetailV2.Data.Settings.RegCompanyName, OfflineRetailV2.Data.Settings.RegAddress1,
                        OfflineRetailV2.Data.Settings.RegAddress2, OfflineRetailV2.Data.Settings.RegCity, OfflineRetailV2.Data.Settings.RegZip, OfflineRetailV2.Data.Settings.RegState, OfflineRetailV2.Data.Settings.RegEMail, OfflineRetailV2.Data.Settings.NoUsers, OfflineRetailV2.Data.Settings.ActivationKey, intRecCount, RegModule, RegLabelDesigner);

                    if (intReturnCode <= 0)
                    {
                        GeneralFunctions.SetRegStatus(SystemVariables.InvalidLogon);
                        if (intReturnCode == -1)
                            new MessageBoxWindow().Show(Properties.Resources.Invalid_Registration_Key____, Properties.Resources.Registration_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                        frmRegistrationDlg frm_RegistrationDlg = new frmRegistrationDlg();
                        frm_RegistrationDlg.FirstTimeCall = true;
                        if (frm_RegistrationDlg.ShowDialog() != true)
                        {
                            Application.Current.Shutdown();
                            return;
                        }
                    }

                    // Proceed the followings after successful verification

                    // Register Devexpress Additional Skins

                    //DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.OfficeSkins).Assembly);
                    //DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.BonusSkins).Assembly);
                    //DevExpress.UserSkins.BonusSkins.Register();
                    //DevExpress.UserSkins.OfficeSkins.Register();

                    // Assign Spell Check Path

                    SystemVariables.DIRpath = GeneralFunctions.GetSpellChkDirectoryPath();
                    SystemVariables.GMRpath = GeneralFunctions.GetSpellChkGrammarPath();
                    SystemVariables.APHpath = GeneralFunctions.GetSpellChkAlphabetPath();

                    OfflineRetailV2.Data.Settings.UpdateStoreCodeForCentralExport(); // update customer, gift certificate data with Web Office Store Code
                    OfflineRetailV2.Data.Settings.InsertInitialPOSFunctionButtons(); // Add non existing POS functions
                    OfflineRetailV2.Data.Settings.LoadSettingsVariables(); // Assign general setup parameters
                    OfflineRetailV2.Data.Settings.GetSoftwareVersion();
                    if (Settings.DefautInternational == "N")
                        GeneralFunctions.SetCurrencyNDateformat();
                    OfflineRetailV2.Data.Settings.GetDataobjectCulture();

                    OfflineRetailV2.Data.Settings.LoadScaleGraduation();
                    OfflineRetailV2.Data.Settings.LoadStoreInfoVariables(); // Assign Store Info.
                    OfflineRetailV2.Data.Settings.LoadCentralStoreInfo(); // Assign Web Office Store Info
                    OfflineRetailV2.Data.Settings.LoadDisplayAgreement(); // Assign License Agreement
                    OfflineRetailV2.Data.Settings.GetLocalSetup(); // Assign local setup parameters ( for this terminal only )
                    OfflineRetailV2.Data.Settings.GetCustomTemplatePrinters();

                    OfflineRetailV2.Data.Settings.LoadMainHeader(); // Assign Receipt Header

                    OfflineRetailV2.Data.Settings.OSVersion = GeneralFunctions.GetOSVersion();



                    // Scale setup from sale and future price    ( *** This part transfered to Scale Scheduler *** )

                    /*
                    int scl1 = OfflineRetailV2.Data.Settings.Get_Scale_Com_Count();

                    OfflineRetailV2.Data.Settings.SetFuturePrice();

                    int scl2 = OfflineRetailV2.Data.Settings.Get_Scale_Com_Count();

                    if (scl1 != scl2) GeneralFunctions.Call_SendToScales_Routine();

                    int scl3 = OfflineRetailV2.Data.Settings.Get_Scale_Com_Count();

                    OfflineRetailV2.Data.Settings.SetScaleComOnSalePrice();

                    int scl4 = OfflineRetailV2.Data.Settings.Get_Scale_Com_Count();

                    if (scl3 != scl4) GeneralFunctions.Call_SendToScales_Routine();
                    */
                    OfflineRetailV2.Data.Settings.TerminalName = GeneralFunctions.GetHostName();

                    OfflineRetailV2.Data.Settings.TerminalTcpIp = GeneralFunctions.GetTCPIp();

                    //bool createdNew = true;
                    //using (Mutex mutex = new Mutex(true, "MyApplicationName", out createdNew))
                    //{
                    //    if (createdNew)
                    //    {
                    //        //Application.EnableVisualStyles();
                    //        //Application.SetCompatibleTextRenderingDefault(false);
                    //        Application.Run(new frmLogin());
                    //    }
                    //    else
                    //    {
                    //        Process current = Process.GetCurrentProcess();
                    //        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    //        {
                    //            if (process.Id != current.Id)
                    //            {
                    //                SetForegroundWindow(process.MainWindowHandle);
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}


                    if (Settings.POSCardPayment == "Y")
                    {
                        if (Settings.PaymentGateway == 8)
                        {
                            XeposExternal.XeConnectIntegration.RunXeConnect();
                        }
                    }

                    if (Settings.DemoVersion == "Y")
                    {
                        SoftwareNameTextBlock.Text = SystemVariables.BrandName + " - Demo Version";
                        this.Title = SystemVariables.BrandName + " - Demo Version";

                    }
                    else
                    {
                        SoftwareNameTextBlock.Text = SystemVariables.BrandName;
                        this.Title = SystemVariables.BrandName;
                    }

                    TillTextBlock.Text = GeneralFunctions.GetHostName();

                    PointOfSaleButton.Visibility = Settings.RegPOSAccess == "Y" ? Visibility.Visible : Visibility.Collapsed;


                    pnlMenuHeader.Width = 50.0;
                    imghide.Visibility = Visibility.Collapsed;
                    imgshow.Visibility = Visibility.Visible;
                    lbShowHideH.Text = "Show";
                    btnECHeader.SetValue(Canvas.LeftProperty, -15.0);

                    Settings.LoadInitialMenu();


                    _versionTimer = new DispatcherTimer();
                    _versionTimer.Interval = TimeSpan.Parse("0:5:0");
                    _versionTimer.Tick += delegate (object obj, EventArgs ea) { ExecuteVersionUpdate(); };

                    //_versionTimer.IsEnabled = true;
                    _versionTimer.Start();

                    _versionTimerNet = new DispatcherTimer();
                    _versionTimerNet.Interval = TimeSpan.Parse("0:0:1");
                    _versionTimerNet.Tick += delegate (object obj, EventArgs ea) { CheckInternet(); };

                    


                }
                else
                {
                    OfflineRetailV2.Data.Settings.default_culture = "en-US";
                    OfflineRetailV2.Data.Settings.POS_Culture = OfflineRetailV2.Data.Settings.SetCulture();
                    Thread.CurrentThread.CurrentUICulture = OfflineRetailV2.Data.Settings.POS_Culture;
                    Thread.CurrentThread.CurrentCulture = OfflineRetailV2.Data.Settings.POS_Culture;
                    // Set Culture of the Application

                    AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);

                    new MessageBoxWindow().Show(Properties.Resources.Could_not_connect_to_Database_ + "\n" + Properties.Resources.Terminating_Application___, Properties.Resources.Database_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Shutdown();
                    return;
                }
            }
            else
            {
                new MessageBoxWindow().Show("No configuration file found, Temnating Application...", "Application Startup Error", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
                return;
            }
        }


        private void SetApplicationThemeFromSetup()
        {
            if (SystemVariables.SelectedTheme == "Dark")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019BlackName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowWCStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Dark/DarkTheme.xaml", UriKind.Relative)
                });
            }

            if (SystemVariables.SelectedTheme == "Light")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericLightStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowWCLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Light/LightTheme.xaml", UriKind.Relative)
                });
            }
        }

        private void ExecuteVersionUpdate()
        {
            
            string strNewVersion = IsUpdateAvailable();
            IsNewVerionCheck = true;
            if (strNewVersion != "")
            {
                _versionTimer.Stop();
                string str = "A new version of " + SystemVariables.BrandName + " (" + strNewVersion + ") is available for download." + '\n' +
                                "Please save your Incomplete Work and Restart Apliacation to Update";


                DocMessage.MsgInformationForVersionUpdate(str, SystemVariables.BrandName + " Version Update");
                // MessageBox.Show(str, "PowerSource Auto Update ", MessageBoxButtons.OK, MessageBoxIcon.None,
                //   MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                //Application.Restart();
                _versionTimer.Start();
                IsNewVerion = true;
            }
        }

        private bool inProcess = false;

        public void ProcessDone(Object s, EventArgs e)
        {
            inProcess = false;
            Console.WriteLine("Exit time:    {0}\r\n" +
            "Exit code:    {1}\r\n", (s as Process).ExitTime, (s as Process).ExitCode);
        }



        private void ExecuteSecondMonitorApplication()
        {
            if ((Settings.CheckSecondMonitor == "Y") && (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor))
            {
                if (Settings.ApplicationSecondMonitor == "") return;
                if (Settings.ApplicationSecondMonitor != "") if (!File.Exists(Settings.ApplicationSecondMonitor)) return;

                if (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor)
                {
                    string filetype = GetFileType(Settings.ApplicationSecondMonitor);

                    Process exeProcess = new Process();

                    if (filetype == "PowerPoint")
                    {
                        exeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        exeProcess.StartInfo.FileName = "powerpnt.exe";
                        exeProcess.StartInfo.Arguments = "/S " + Settings.ApplicationSecondMonitor;
                        exeProcess.EnableRaisingEvents = true;
                        exeProcess.StartInfo.CreateNoWindow = true;
                        exeProcess.Start();
                        //exeProcess.WaitForInputIdle();
                    }

                    if (filetype == "OpenPowerPoint")
                    {
                        exeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        exeProcess.StartInfo.FileName = "soffice.exe";
                        exeProcess.StartInfo.Arguments = "-show " + Settings.ApplicationSecondMonitor;
                        exeProcess.EnableRaisingEvents = true;
                        exeProcess.StartInfo.CreateNoWindow = true;
                        exeProcess.Start();
                        //exeProcess.WaitForInputIdle();
                    }

                    if (filetype == "Other")
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = Settings.ApplicationSecondMonitor;
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;
                        startInfo.FileName = Settings.ApplicationSecondMonitor;
                        startInfo.WorkingDirectory = Path.GetDirectoryName(Settings.ApplicationSecondMonitor);
                        startInfo.CreateNoWindow = true;
                        exeProcess.StartInfo = startInfo;
                        exeProcess.EnableRaisingEvents = true;
                        exeProcess.Exited += ProcessDone;
                        //exeProcess.Start(startInfo);
                        //exeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        //exeProcess.StartInfo.FileName = Settings.ApplicationSecondMonitor;
                        //exeProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Settings.ApplicationSecondMonitor);

                        //exeProcess.StartInfo.CreateNoWindow = true;

                        exeProcess.Start();
                        inProcess = true;
                        //Dispatcher.BeginInvoke(new Action(() => exeProcess.Start()));

                        //exeProcess.WaitForInputIdle();

                        /*while (inProcess)
                        {
                            exeProcess.Refresh();
                            System.Threading.Thread.Sleep(10);
                            if (exeProcess.HasExited)
                            {
                                inProcess = false;
                            }
                        }*/



                    }

                    if (filetype != "Media")
                    {
                        if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                        {
                            if (System.Windows.Forms.Screen.AllScreens[1] != null)
                            {
                                Rectangle secondMonitor = System.Windows.Forms.Screen.AllScreens[1].WorkingArea;


                                //SetWindowPos(new WindowInteropHelper(Application.Current.MainWindow).Handle, new IntPtr(0), secondMonitor.Left, secondMonitor.Top, secondMonitor.Width, secondMonitor.Height, 0);
                            }
                        }
                        else
                        {
                            //SetWindowPos(new WindowInteropHelper(Application.Current.MainWindow).Handle, new IntPtr(0), 0, 0, 800, 600, 0);
                        }

                        //SystemVariables.SecondMonitorAppID = exeProcess.Id;
                    }
                    else
                    {
                        if ((winplay as WinMediaPlayer) == null)
                        {
                            winplay = new WinMediaPlayer();
                            WinMediaPlayer.playfile(Settings.ApplicationSecondMonitor);
                        }
                    }
                }
            }
        }

        private string GetFileType(string filepath)
        {
            string fext = Path.GetExtension(filepath).ToLower();
            if ((fext == ".ppt") || (fext == ".pps"))
            {
                return "PowerPoint";
            }
            else if ((fext == ".odp"))
            {
                return "OpenPowerPoint";
            }
            else if ((fext == ".asf") || (fext == ".wma") || (fext == ".wmv") || (fext == ".wm") || (fext == ".asx") || (fext == ".wax")
                    || (fext == ".wvx") || (fext == ".wpl") || (fext == ".wmx") || (fext == ".wmd") || (fext == ".mpg") || (fext == ".avi")
                || (fext == ".mpeg") || (fext == ".m1v") || (fext == ".mp2") || (fext == ".mp3") || (fext == ".mpa") || (fext == ".mpe")
                || (fext == ".mpv2") || (fext == ".m3u") || (fext == ".mid") || (fext == ".midi") || (fext == ".rmi") || (fext == ".aif")
                || (fext == ".aifc") || (fext == ".aiff") || (fext == ".au") || (fext == ".snd") || (fext == ".wav") || (fext == ".cda")
                || (fext == ".ivf") || (fext == ".wmz") || (fext == ".wms") || (fext == ".mov") || (fext == ".qt"))
            {
                return "Media";
            }
            else
            {
                return "Other";
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            frm_CustomErrorDlg frm_CustomErrorDlg = new frm_CustomErrorDlg();
            frm_CustomErrorDlg.Title.Text = Properties.Resources.Fatal_Error;
            frm_CustomErrorDlg.ErrorMsg = ex.Message;
            frm_CustomErrorDlg.StackTrace = ex.StackTrace;
            frm_CustomErrorDlg.ShowDialog();
        }

        // Unexpected Error Dialog

        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            frm_CustomErrorDlg frm_CustomErrorDlg = new frm_CustomErrorDlg();
            frm_CustomErrorDlg.Title.Text = Properties.Resources.Application_Error;
            frm_CustomErrorDlg.ErrorMsg = e.Exception.Message;
            frm_CustomErrorDlg.StackTrace = e.Exception.StackTrace;
            frm_CustomErrorDlg.ShowDialog();
        }

        private DispatcherTimer DateTimeTimer { get; set; }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            DateTimeTimer.Stop();

            Application.Current.Shutdown();
        }

        private void SetDateTime()
        {
            int hour = DateTime.Now.TimeOfDay.Hours;
            int minute = DateTime.Now.TimeOfDay.Minutes;
            TimeTextBlock.Text = (hour > 9 ? hour.ToString() : "0" + hour) + ":" + (minute > 9 ? minute.ToString() : "0" + minute);
            DateTextBlock.Text = DateTime.Now.ToString("d MMM yy");
        }

        #region LoginMenuBorder

        public LoginSection LoginSection { get; private set; }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (POSControl != null)
                POSControl.blInitAutoSignOutAfterTender = false;
            LoginSection = LoginSection.Admin;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginBorder.Visibility = Visibility.Visible;
            pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;

            //this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() =>
            //{
            //  GeneralFunctions.SetDetailedTransactionLog("AdminButton_Click", "LoginSection = LoginSection.Admin;", (++idCounter).ToString());
            //    pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;
            //}));
        }

        private void CashOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (POSControl != null)
                POSControl.blInitAutoSignOutAfterTender = false;
            LoginSection = LoginSection.CashOut_CloseOut;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginBorder.Visibility = Visibility.Visible;
            pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void ClockButton_Click(object sender, RoutedEventArgs e)
        {
            if (POSControl == null)
            {
                POSControl = new UserControls.POSControl();
                POSControl.Name = "POSControl";
                ControlsGrid.Children.Add(POSControl);
                POSControl.Visibility = Visibility.Collapsed;
            }

            POSControl.blInitAutoSignOutAfterTender = false;
            LoginSection = LoginSection.ClickInClockOut_Employee;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginBorder.Visibility = Visibility.Visible;
            pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private async void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!booldownloadCompleted)
            {
                if (IsNewVerionCheck)
                {
                    if (!IsNewVerion)
                    {
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        if (CheckLatestVersion(false))
                        {
                            return;
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    
                    

                    if (CheckLatestVersion(false))
                    {
                        return;
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void PointOfSaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Settings.SpeedUpPOS)
            {
                Settings.SpeedUpPOS = true;
                try
                {
                    Thread newWindowThread = new Thread(new ThreadStart(() =>
                    {
                        UserControls.frm_SpeedPOS fakeWindow = new UserControls.frm_SpeedPOS();
                        System.Windows.Threading.Dispatcher.Run();
                    }));
                    newWindowThread.SetApartmentState(ApartmentState.STA);
                    newWindowThread.IsBackground = true;
                    newWindowThread.Start();
                }
                catch
                {

                }
            }

            if (POSControl != null)
                POSControl.blInitAutoSignOutAfterTender = false;
            LoginSection = LoginSection.POS;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginBorder.Visibility = Visibility.Visible;
            //  POSControl.txtSearch.Focus();
            pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void SalesSnapShotButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (POSControl != null)
                POSControl.blInitAutoSignOutAfterTender = false;
            LoginSection = LoginSection.SalesSnapShot_Dashboard;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginBorder.Visibility = Visibility.Visible;
            pnllogininfo.Visibility = GeneralFunctions.GetRecordCount("Employee") == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        #endregion LoginMenuBorder

        #region LoginBorder


        private void BackLinkButton_Click(object sender, RoutedEventArgs e)
        {
            LoginSection = LoginSection.None;
            LoginBorder.Visibility = Visibility.Collapsed;
            LoginMenuBorder.Visibility = Visibility.Visible;
        }

        private void KeyPadPasswordControl_PasswordChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(KeyPadPasswordControl.Password))
            {
                bool bProcced = false;
                if (KeyPadPasswordControl.Password.Length == 4)
                {
                    if (KeyPadPasswordControl.Password == Settings.ADMINPASSWORD) bProcced = true;
                    else if ((KeyPadPasswordControl.Password == "1111") && (strSelectedUserID == ""))
                    {
                        if (GeneralFunctions.GetRecordCount("Employee") == 0)
                        {
                            bProcced = true;
                        }
                    }
                    else if ((KeyPadPasswordControl.Password != Settings.ADMINPASSWORD) && (strSelectedUserID != ""))
                    {
                        bProcced = true;
                    }
                    else
                    {

                    }
                }



                System.Threading.Thread.Sleep(10);
                if (bProcced)
                {
                    KeyPadPasswordControl.Refresh();
                    try
                    {
                        Cursor = Cursors.Wait;
                        int intLogID = 0;
                        string strLogCode = "";
                        string strLogName = "";
                        bool blNew = false;
                        bool bllocked = false;
                        bool blExpired = false;
                        string strExpireDue = "";
                        if (((strSelectedUserID == "1") || (strSelectedUserID == "")) && (KeyPadPasswordControl.Password == "1111"))
                        {
                            if (LoginSection != LoginSection.ClickInClockOut_Employee)
                            {
                                if (GeneralFunctions.GetRecordCount("Employee") == 0) blNew = true;
                            }
                        }

                        if (IsValid() || blNew)
                        {
                            if (blNew)
                            {
                                SystemVariables.CurrentUserCode = "";
                                SystemVariables.CurrentUserName = "STORE ADMIN";
                                SystemVariables.CurrentUserID = -1;
                            }
                            else if (KeyPadPasswordControl.Password == Settings.ADMINPASSWORD)
                            {
                                SystemVariables.CurrentUserCode = "";
                                SystemVariables.CurrentUserName = "ADMIN";
                                SystemVariables.CurrentUserID = 0;
                            }
                            else
                            {
                                string apswd = "";
                                bool freqd = false;

                                PosDataObject.Login objLogin = new PosDataObject.Login();
                                objLogin.Connection = SystemVariables.Conn;
                                int intreturn = objLogin.GetLoginDetails(0, strSelectedUserID, KeyPadPasswordControl.Password,
                                                                         freqd, apswd, ref intLogID, ref strLogCode, ref strLogName,
                                                                         ref bllocked, ref strExpireDue, ref blExpired);
                                if (intreturn == 0)
                                {
                                    new MessageBoxWindow().Show(Properties.Resources.Invalid_Login_ID___Password, Properties.Resources.Login_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }

                                SystemVariables.CurrentUserCode = strLogCode;
                                SystemVariables.CurrentUserName = strLogName;
                                SystemVariables.CurrentUserID = intLogID;

                            }
                            Settings.GetUserCustomizationParameters();
                            // MBLW = new MessageBoxLoadingWindow();
                            // MBLW.Show();
                            try
                            {
                                FormAccessor.Forms.MsgLoadingBox.Show();
                            }
                            catch (Exception aex)
                            {

                            }
                            //FormAccessor.ThreadSafeObjectProvider<FormAccessor>.
                            DoLogin();
                        }
                        else
                        {
                            new MessageBoxWindow().Show(Properties.Resources.Invalid_Login_ID___Password, Properties.Resources.Login_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }


                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                    }
                }
            }
        }
        private int SignINOption = 0;
        private bool IsValid()
        {
            if (LoginSection == LoginSection.ClickInClockOut_Employee)
            {
                if (KeyPadPasswordControl.Password == Settings.ADMINPASSWORD)
                {
                    new MessageBoxWindow().Show(Properties.Resources.Invalid_Module, Properties.Resources.Module_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            /*if ((KeyPadPasswordControl.Password != Settings.ADMINPASSWORD) || ((KeyPadPasswordControl.Password != "1111") && (strSelectedUserID == "")))
            {
                return false;
            }*/

            return true;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginSection == LoginSection.Admin) && (Settings.ActiveAdminPswdForMercury))
            {
                SignINOption = 0;
            }
            else
            {
                SignINOption = Settings.SignINOption;
            }






        }


        public void GoToFrontOffice()
        {
            LoginSection = LoginSection.POS;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            GoToPOSControl();
        }

        private void DoLogin()
        {
            if (LoginSection == LoginSection.None)
                return;

            LoginGrid.Visibility = Visibility.Collapsed;
            pnllogininfo.Visibility = Visibility.Collapsed;
            this.UpdateLayout();
            switch (LoginSection)
            {
                case LoginSection.POS:
                    GoToPOSControl();
                    //MBLW.Close has been done in POSControl.Load func after all the loading

                    break;
                case LoginSection.Admin:
                    GotoAdmin();
                    FormAccessor.Forms.MsgLoadingBox.Hide();
                    break;
                case LoginSection.ClickInClockOut_Employee:
                    GotoClickInClockOut_Employee();
                    FormAccessor.Forms.MsgLoadingBox.Hide();
                    break;
                case LoginSection.CashOut_CloseOut:
                    GotoCashOut_CloseOut();
                    FormAccessor.Forms.MsgLoadingBox.Hide();
                    break;
                case LoginSection.SalesSnapShot_Dashboard:
                    GotoSalesSnapShot_Dashboard();
                    FormAccessor.Forms.MsgLoadingBox.Hide();
                    break;
            }
            LoggedInUserTextBlock.Text = SystemVariables.CurrentUserName;
            //if (mblw != null)
            //{
            //    //mblw.Close();
            //    //mblw.Close();
            //}
        }
        private void PopupUnreadMessage(int UID)
        {
            PosDataObject.Messages objMsg = new PosDataObject.Messages();
            objMsg.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int cnt = objMsg.CountUnreadMail(UID);
            if (cnt == 0) return;
            else
            {
                if (cnt == 1)
                {
                    new MessageBoxWindow().Show(Properties.Resources.You_have_1_unread_message_, Properties.Resources.Message, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    new MessageBoxWindow().Show(Properties.Resources.You_have + cnt.ToString() + Properties.Resources.unread_messages_, Properties.Resources.Message, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }
        private async void GotoAdmin()
        {
            if (GeneralFunctions.GetAppVersionFromDB() != GeneralFunctions.AppVersionInfo())
            {
                /*frmLicenceDlg frm_LicenceDlg = new frmLicenceDlg();
                try
                {
                    frm_LicenceDlg.ShowDialog();
                    if (frm_LicenceDlg.DialogResult == DialogResult.Cancel) return;
                }
                finally
                {
                    frm_LicenceDlg.Dispose();
                }*/

                GeneralFunctions.UpdateAppVersion();
            }

            PopupUnreadMessage(SystemVariables.CurrentUserID);

            SecurityPermission.LoadSecurityPermissionVariables();
            DefaultSecurityCheck();
            Settings.IsPOSSelected = false;

            CheckNoOfUser();

            if (Settings.ScaleDevice == "Live Weight")
            {
                if (_slport != null)
                {
                    if (_slport.IsOpen)
                    {
                        _slport.Close();
                        blAttachToLWTimer = false;
                    }
                }
            }



            try
            {

                if (frm_MainAdmin == null)
                {
                    frm_MainAdmin = new UserControls.frm_MainAdmin();
                    frm_MainAdmin.Name = "frm_MainAdmin";
                    ControlsGrid.Children.Add(frm_MainAdmin);
                    frm_MainAdmin.Visibility = Visibility.Visible;
                }
                else
                {
                    frm_MainAdmin.Visibility = Visibility.Visible;
                }

                //frm_MainAdmin.navControlMain.ActiveGroup = frm_MainAdmin.navGroupCustomer;
                //frm_MainAdmin.Visibility = Visibility.Visible;
                frm_MainAdmin.AdminCloseCommand = new CommandBase(OnAdminCloseCommand);
                btnFrontOffice.Visibility = Visibility.Visible;
                btnFrontOffice.Content = "FRONT OFFICE";
                frm_MainAdmin.LoadAdmin();
            }
            finally
            {

            }
        }

        private void OnAdminCloseCommand(object obj)
        {
            /*
                SystemVariables.DIRpath = GeneralFunctions.GetSpellChkDirectoryPath();
                SystemVariables.GMRpath = GeneralFunctions.GetSpellChkGrammarPath();
                SystemVariables.APHpath = GeneralFunctions.GetSpellChkAlphabetPath();
                //Settings.InsertInitialPOSFunctionButtons();
                Settings.LoadSettingsVariables();
                Settings.LoadStoreInfoVariables();
                Settings.LoadCentralStoreInfo();
                Settings.GetLocalSetup();
                Settings.LoadMainHeader();*/

            if ((Settings.CheckSecondMonitor == "Y") && (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor))
            {
                if ((sm as SecondMonitor) != null)
                {
                    SecondMonitor.ClearWeightInfo();
                    if (Settings.DisplayLaneClosed == "Y") SecondMonitor.SetupDisplayOnLoad();
                    else SecondMonitor.InsertItem(null, -1, "", 0, 0, 0, 0, 0, 0, 0);
                }
            }
        }

        private async void GotoClickInClockOut_Employee()
        {
            if (GeneralFunctions.GetAppVersionFromDB() != GeneralFunctions.AppVersionInfo())
            {
                /*frmLicenceDlg frm_LicenceDlg = new frmLicenceDlg();
                try
                {
                    frm_LicenceDlg.ShowDialog();
                    if (frm_LicenceDlg.DialogResult == DialogResult.Cancel) return;
                }
                finally
                {
                    frm_LicenceDlg.Dispose();
                }*/

                GeneralFunctions.UpdateAppVersion();
            }

            PopupUnreadMessage(SystemVariables.CurrentUserID);
            //this.Visible = false;

            SecurityPermission.LoadSecurityPermissionVariables();
            DefaultSecurityCheck();

            Settings.IsPOSSelected = false;

            //  checking no. of user
            CheckNoOfUser();

            if (ClockInClockOutEmployeeControl == null)
            {
                ClockInClockOutEmployeeControl = new UserControls.ClockInClockOutEmployeeControl();
                ClockInClockOutEmployeeControl.Name = "ClockInClockOutEmployeeControl";
                ControlsGrid.Children.Add(ClockInClockOutEmployeeControl);
                ClockInClockOutEmployeeControl.Visibility = Visibility.Visible;
            }
            else
            {
                ClockInClockOutEmployeeControl.Visibility = Visibility.Visible;
            }

            //ClockInClockOutEmployeeControl.Visibility = Visibility.Visible;
            ClockInClockOutEmployeeControl.ClockInClockOutEmployeeControlClosed = new CommandBase(OnClockInClockOutEmployeeControlClosed);
            btnFrontOffice.Visibility = Visibility.Visible;
            btnFrontOffice.Content = "FRONT OFFICE";
            await ClockInClockOutEmployeeControl.Load();
        }

        private void OnClockInClockOutEmployeeControlClosed(object obj)
        {

        }

        private void GotoCashOut_CloseOut()
        {
            if (GeneralFunctions.GetAppVersionFromDB() != GeneralFunctions.AppVersionInfo())
            {
                /*frmLicenceDlg frm_LicenceDlg = new frmLicenceDlg();
                try
                {
                    frm_LicenceDlg.ShowDialog();
                    if (frm_LicenceDlg.DialogResult == DialogResult.Cancel) return;
                }
                finally
                {
                    frm_LicenceDlg.Dispose();
                }*/

                GeneralFunctions.UpdateAppVersion();
            }

            SecurityPermission.LoadSecurityPermissionVariables();
            DefaultSecurityCheck();

            Settings.IsPOSSelected = false;
            //SystemVariables.CloseOutID = GeneralFunctions.GetCloseOutID();
            //  checking no. of user
            CheckNoOfUser();
            btnFrontOffice.Visibility = Visibility.Visible;
            btnFrontOffice.Content = "FRONT OFFICE";

            if (frmCloseoutDlg_CashOut == null)
            {
                frmCloseoutDlg_CashOut = new UserControls.frmCloseoutDlg_CashOut();
                frmCloseoutDlg_CashOut.Name = "frmCloseoutDlg_CashOut";
                ControlsGrid.Children.Add(frmCloseoutDlg_CashOut);
                frmCloseoutDlg_CashOut.Visibility = Visibility.Visible;
            }
            else
            {
                frmCloseoutDlg_CashOut.Visibility = Visibility.Visible;
            }

            frmCloseoutDlg_CashOut.Visibility = Visibility.Visible;
            frmCloseoutDlg_CashOut.frmCloseoutDlg_CashOutClosed = new CommandBase(OnfrmCloseoutDlg_CashOutClosed);
            frmCloseoutDlg_CashOut.Load();
        }

        private void OnfrmCloseoutDlg_CashOutClosed(object obj)
        {

        }

        private async void GotoSalesSnapShot_Dashboard()
        {
            if (GeneralFunctions.GetRecordCount("StoreInfo") == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.You_must_login_to_ADMIN_module_and_complete_General_Setup_first_,
                    Properties.Resources.Application_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (GeneralFunctions.GetAppVersionFromDB() != GeneralFunctions.AppVersionInfo())
            {
                /*frmLicenceDlg frm_LicenceDlg = new frmLicenceDlg();
                try
                {
                    frm_LicenceDlg.ShowDialog();
                    if (frm_LicenceDlg.DialogResult == DialogResult.Cancel) return;
                }
                finally
                {
                    frm_LicenceDlg.Dispose();
                }*/

                GeneralFunctions.UpdateAppVersion();
            }

            SecurityPermission.LoadSecurityPermissionVariables();
            DefaultSecurityCheck();

            Settings.IsPOSSelected = false;

            CheckNoOfUser();

            if (frm_DashboardDlg_SalesSnapshot == null)
            {
                frm_DashboardDlg_SalesSnapshot = new UserControls.frm_DashboardDlg_SalesSnapshot();
                frm_DashboardDlg_SalesSnapshot.Name = "frm_DashboardDlg_SalesSnapshot";
                ControlsGrid.Children.Add(frm_DashboardDlg_SalesSnapshot);
                //frm_Dashboard.Visibility = Visibility.Visible;
            }
            else
            {
                //frm_Dashboard.Visibility = Visibility.Visible;
            }

            if ((SecurityPermission.AccessDashboard) || (SystemVariables.CurrentUserID <= 0))
            {
                frm_DashboardDlg_SalesSnapshot.Visibility = Visibility.Visible;
                frm_DashboardDlg_SalesSnapshot.frm_DashboardDlg_SalesSnapshotClosed = new CommandBase(Onfrm_DashboardDlg_SalesSnapshotClosed);
                btnFrontOffice.Visibility = Visibility.Visible;
                btnFrontOffice.Content = "FRONT OFFICE";
                await frm_DashboardDlg_SalesSnapshot.Load();
            }
            else
            {
                DocMessage.MsgPermission();
                LoginMenuBorder.Visibility = Visibility.Visible;
                LoginBorder.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;
                btnFrontOffice.Visibility = Visibility.Hidden;
                LoginSection = LoginSection.None;
            }
        }

        private void Onfrm_DashboardDlg_SalesSnapshotClosed(object obj)
        {

        }

        private bool IsValidLoginID()
        {
            /*PosDataObject.Employee oemp = new PosDataObject.Employee();
            oemp.Connection = SystemVariables.Conn;
            return (oemp.DuplicateCount(LoginControl.txtUser.Text) == 1);*/
            return true;
        }
        private int CheckForClosedRegister(out bool isLoadingWindowClosed)
        {
            isLoadingWindowClosed = false;
            int closeoutId = GeneralFunctions.GetCloseOutID();
            PosDataObject.Closeout objc = new PosDataObject.Closeout();
            objc.Connection = SystemVariables.Conn;
            int trncnt = objc.FetchTransactionCount(closeoutId);
            if (trncnt == 0)
            {
                int cashfloatcnt = objc.FetchCashFloatCount(closeoutId);
                if (cashfloatcnt == 0)
                {

                    if ((SystemVariables.CurrentUserID > 0) && (!SecurityPermission.AccessCashFloat))
                    {
                        DocMessage.MsgPermission();
                        return 0;
                    }
                    else
                    {
                        bool bFlag = false;
                        blurGrid.Visibility = Visibility.Visible;
                        frm_OpenRegisterDlg frm = new frm_OpenRegisterDlg();
                        try
                        {
                            frm.CloseoutID = closeoutId;
                            FormAccessor.Forms.MsgLoadingBox.Close();
                            isLoadingWindowClosed = true;
                            frm.ShowDialog();
                            bFlag = frm.FinalFlag;
                        }
                        finally
                        {
                            frm.Close();
                            blurGrid.Visibility = Visibility.Collapsed;
                        }

                        if (bFlag)
                        {
                            return closeoutId;
                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
                else
                {
                    return closeoutId;
                }
            }
            else
            {
                return closeoutId;
            }
        }
        private void DefaultSecurityCheck()
        {
            PosDataObject.Security objSec2 = new PosDataObject.Security();
            objSec2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int ret1 = objSec2.InsertSecurityPermissionData();
            PosDataObject.Security objSec3 = new PosDataObject.Security();
            objSec3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objSec3.LoginUserID = SystemVariables.CurrentUserID;
            int ret2 = objSec3.InsertDefaultSecurity();
            PosDataObject.Security objSec4 = new PosDataObject.Security();
            objSec4.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objSec4.LoginUserID = SystemVariables.CurrentUserID;
            int ret4 = objSec4.SetOwnerSecurity();
        }
        private void CheckNoOfUser()
        {
            Settings.LoadRegistrationVariables();
            Settings.LoadMainHeader();
            bool chkuser = true;
            if (Settings.NoUsers != 0)
            {
                int intactiveuser = GetActiveUser();
                if (intactiveuser > Settings.NoUsers) chkuser = false;
            }

            if (!chkuser)
            {
                new MessageBoxWindow().Show(Properties.Resources.No__of_users_exceed_the_registration_limit_ + "\n" + Properties.Resources.Terminating_Application___, Properties.Resources.Application_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        private int GetActiveUser()
        {
            PosDataObject.Login objlog = new PosDataObject.Login();
            objlog.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objlog.GetActiveUser();
        }
        private bool ContinueWithDemoVersion()
        {
            PosDataObject.POS clsPOS = new PosDataObject.POS();
            clsPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return clsPOS.ContinueWithDemoInvNo(Settings.MaxDemoInvoiceNo);
        }
        private bool IsPOSInstalled()
        {
            bool blf = false;
            try
            {
                PosExplorer psexp = new PosExplorer();
                blf = true;
            }
            catch (Exception ex)
            {
                blf = false;
            }
            return blf;
        }

        public void SetDefaultWindow()
        {
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;
        }

        public void GoToPOSControl()
        {
            if (GeneralFunctions.GetAppVersionFromDB() != GeneralFunctions.AppVersionInfo())
            {
                GeneralFunctions.UpdateAppVersion();
            }

            SecurityPermission.LoadSecurityPermissionVariables();
            DefaultSecurityCheck();

            bool isLoadingWindowClosed = false;
            if (Settings.CloseoutOption == 1)
            {
                if (CheckForClosedRegister(out isLoadingWindowClosed) == 0)
                {
                    LoginMenuBorder.Visibility = Visibility.Visible;
                    LoginBorder.Visibility = Visibility.Collapsed;
                    LoginGrid.Visibility = Visibility.Visible;
                    LoginSection = LoginSection.None;
                    return;
                }
            }

            if (isLoadingWindowClosed)
            {
                //  MBLW = new MessageBoxLoadingWindow();
                try
                {
                    if (FormAccessor.Forms != null && FormAccessor.Forms.MsgLoadingBox != null)
                    {
                        FormAccessor.Forms.MsgLoadingBox.Show();
                    }

                }
                catch (Exception)
                {

                }
            }

            Settings.IsPOSSelected = true;

            //  checking no. of user
            CheckNoOfUser();

            if (Settings.DemoVersion == "Y")
            {
                if (!ContinueWithDemoVersion())
                {
                    FormAccessor.Forms.MsgLoadingBox.Close();
                    new MessageBoxWindow().Show(Properties.Resources.Maximum_no__of_invoices_exceeded_for_DEMO_Version_, Properties.Resources.DEMO_Version_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginMenuBorder.Visibility = Visibility.Visible;
                    LoginBorder.Visibility = Visibility.Collapsed;
                    LoginGrid.Visibility = Visibility.Visible;
                    LoginSection = LoginSection.None;
                    return;
                }
            }

            //if (IsPOSInstalled())
            //{

            if (Settings.ScaleDevice == "Live Weight")
            {
                if (_slport != null)
                {
                    if (_slport.IsOpen)
                    {
                        _slport.Close();
                        blAttachToLWTimer = false;
                    }
                }
            }



            try
            {
                if (POSControl == null)
                {
                    POSControl = new UserControls.POSControl();
                    ControlsGrid.Children.Add(POSControl);
                    POSControl.Visibility = Visibility.Visible;
                }
                else
                {
                    POSControl.Visibility = Visibility.Visible;
                }

                POSControl.StartServiceType = Settings.POSDefaultService;
                SetPriceLevelToday();
                POSControl.pLastLoggedTime = GetTickCount();

                POSControl.OnPOSCloseCommand = new CommandBase(OnOnPOSCloseCommand);
                //POSControl.Visibility = Visibility.Visible;
                SearchMemberTextBox.Visibility = Visibility.Hidden;
                btnFrontOffice.Visibility = Visibility.Visible;
                btnFrontOffice.Content = "SELECT CUSTOMER";
                POSControl.blfetchCustomer = false;
                POSControl.Load(FormAccessor.Forms.MsgLoadingBox);
                // GeneralFunctions.SetFocus( POSControl.txtSearch);

                try
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        POSControl.txtSearch.Focusable = true;
                        POSControl.txtSearch.Focus();
                    }), System.Windows.Threading.DispatcherPriority.Render);

                }
                catch
                {


                }

                Console.WriteLine("MainWindow POS Loaded");
            }
            finally
            {

            }
            //}
            //else
            //{
            //DocMessage.MsgInformation(Properties.Resources.You_must_install_POS_for__Net);
            //}


        }

        private void OnOnPOSCloseCommand(object obj)
        {
            if (obj is string param)
            {
                if (param == "timeout")
                {
                    LoginMenuBorder.Visibility = Visibility.Visible;
                    LoginBorder.Visibility = Visibility.Collapsed;
                    LoginGrid.Visibility = Visibility.Visible;
                    LoginSection = LoginSection.None;
                    return;
                }
            }

            if ((Settings.CheckSecondMonitor == "Y") && (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor))
            {
                if ((sm as SecondMonitor) == null) sm = new SecondMonitor(true);
                SecondMonitor.ClearWeightInfo();
                if (GeneralFunctions.RegisteredModules().Contains("POS"))
                {
                    if (Settings.DisplayLaneClosed == "Y")
                    {
                        SecondMonitor.ClearWeightInfo();
                        SecondMonitor.SetupDisplayOnLoad();
                    }
                    else
                    {
                        if (Settings.ScaleDevice == "Live Weight")
                        {
                            DummyCall_LiveWeightScale();
                        }
                        else
                        {
                            SecondMonitor.ClearWeightInfo();
                        }
                        SecondMonitor.InsertItem(null, -1, "", 0, 0, 0, 0, 0, 0, 0);
                    }
                }
            }
        }

        public void DummyCall_LiveWeightScale()
        {
            try
            {
                if (Settings.COMPort != "(None)")
                {
                    int bd = GeneralFunctions.fnInt32(Settings.BaudRate);
                    int dbit = GeneralFunctions.fnInt32(Settings.DataBits);
                    Parity p = Parity.None;
                    StopBits sb = StopBits.One;
                    Handshake hshk = Handshake.None;
                    if (Settings.Parity == "Even") p = Parity.Even;
                    if (Settings.Parity == "Mark") p = Parity.Mark;
                    if (Settings.Parity == "None") p = Parity.None;
                    if (Settings.Parity == "Odd") p = Parity.Odd;
                    if (Settings.Parity == "Space") p = Parity.Space;

                    if (Settings.StopBits == "None") sb = StopBits.None;
                    if (Settings.StopBits == "One") sb = StopBits.One;
                    if (Settings.StopBits == "OnePointFive") sb = StopBits.OnePointFive;
                    if (Settings.StopBits == "Two") sb = StopBits.Two;

                    if (Settings.Handshake == "None") hshk = Handshake.None;
                    if (Settings.Handshake == "RequstToSend") hshk = Handshake.RequestToSend;
                    if (Settings.Handshake == "RequstToSendXOnXOff") hshk = Handshake.RequestToSendXOnXOff;
                    if (Settings.Handshake == "XOnXOff") hshk = Handshake.XOnXOff;

                    _slport = new SerialPort(Settings.COMPort, bd, p, dbit, sb);
                    _slport.Handshake = hshk;
                    _slport.ReadTimeout = GeneralFunctions.fnInt32(Settings.Timeout) < 3000 ? 3000 : GeneralFunctions.fnInt32(Settings.Timeout);
                    _slport.WriteTimeout = GeneralFunctions.fnInt32(Settings.Timeout) < 3000 ? 3000 : GeneralFunctions.fnInt32(Settings.Timeout);
                    try
                    {
                        if (!(_slport.IsOpen)) _slport.Open();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        _slport.Close();
                        _slport.Dispose();
                    }
                }
                else
                {
                }
            }
            catch
            {
            }
        }
        private SecondMonitor sm;

        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();
        private void SetPriceLevelToday()
        {
            string dtPL = GetPLDate();
            bool updt = false;
            if (dtPL == "")
            {
                updt = true;
            }
            else
            {
                if (GeneralFunctions.fnDate(dtPL).Date != DateTime.Now.Date) updt = true;
                else updt = false;
            }
            if (updt)
            {
                string updtstr = UpdatePLDate();
                Settings.LoadSettingsVariables();
                Settings.LoadScaleGraduation();
            }
        }
        private string GetPLDate()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objPOS.GetPriceLevelApplicableDate();
        }

        private string UpdatePLDate()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objPOS.LoginUserID = SystemVariables.CurrentUserID;
            return objPOS.UpdatePriceLevelApplicableDate();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /*if (string.IsNullOrEmpty(LoginControl.txtUser.Text) && (!string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password) && LoginControl.txtPswd.PasswordBox.Password == Settings.ADMINPASSWORD))
            {
                LoginControl.LoginButton.IsEnabled = true;
            }
            else
            {
                if (SignINOption == 0)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text) && !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
                }

                if (SignINOption == 1)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text);
                }

                if (SignINOption == 2)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
                }
            }*/
            //LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text) && !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
        }

        private void ShowTouchKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void TxtUser_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*if (string.IsNullOrEmpty(LoginControl.txtUser.Text) && (!string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password) && LoginControl.txtPswd.PasswordBox.Password == Settings.ADMINPASSWORD))
            {
                LoginControl.LoginButton.IsEnabled = true;
            }
            else
            {
                if (SignINOption == 0)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text) && !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
                }

                if (SignINOption == 1)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text);
                }

                if (SignINOption == 2)
                {
                    LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
                }
            }*/
            //LoginControl.LoginButton.IsEnabled = !string.IsNullOrEmpty(LoginControl.txtUser.Text) && !string.IsNullOrEmpty(LoginControl.txtPswd.PasswordBox.Password);
        }

        #endregion LoginBorder

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((Settings.CheckSecondMonitor == "Y") && (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor))
            {
                bool blsm = false;
                if (GeneralFunctions.RegisteredModules().Contains("POS")) blsm = true;
                if (GeneralFunctions.RegisteredModules().Contains("SCALE")) blsm = true; // && (Settings.AdvtScale == "Y")

                if (blsm)
                {
                    if ((sm as SecondMonitor) == null) sm = new SecondMonitor(true);
                    SecondMonitor.ClearWeightInfo();
                    if (GeneralFunctions.RegisteredModules().Contains("POS"))
                    {
                        if (Settings.DisplayLaneClosed == "Y")
                        {
                            SecondMonitor.ClearWeightInfo();
                            SecondMonitor.SetupDisplayOnLoad();
                        }
                        else
                        {
                            if (Settings.ScaleDevice == "Live Weight")
                            {
                                DummyCall_LiveWeightScale();
                                //tmrLW.Enabled = true;

                            }
                            else
                            {
                                SecondMonitor.ClearWeightInfo();
                            }
                            SecondMonitor.InsertItem(null, -1, "", 0, 0, 0, 0, 0, 0, 0);
                        }
                    }
                    if (GeneralFunctions.RegisteredModules().Contains("SCALE") && !GeneralFunctions.RegisteredModules().Contains("POS"))
                    {
                        if (Settings.ScaleDevice == "Live Weight")
                        {
                            DummyCall_LiveWeightScale();
                            //tmrLW.Enabled = true;
                        }
                        else
                        {
                            SecondMonitor.ClearWeightInfo();
                        }
                        SecondMonitor.SetupDisplayOnScaleLoad();

                        if ((Settings.ScaleDevice == "Live Weight") || (Settings.ScaleDevice == "Datalogic Scale"))
                        {
                            SecondMonitor.AddScaleInfo("", "", "0.00", false, "", "", false);
                        }
                    }
                    ExecuteSecondMonitorApplication();
                }
            }
        }





        public void LoadEmployees()
        {
            strSelectedUserID = "";
            KeyPadPasswordControl.ResetPassCode();
            pnlEmp.Children.Clear();
            PosDataObject.Login employee = new PosDataObject.Login();
            employee.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = employee.FetchAllEmployees();
            foreach (DataRow dr in dtbl.Rows)
            {
                Grid grd = new Grid();
                grd.Margin = new Thickness(10, 10, 10, 20);
                grd.Width = 120;
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(106.03) });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16) });


                DockPanel dockPanel = new DockPanel();
                dockPanel.Width = 116.03;
                dockPanel.Height = 116.03;
                dockPanel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                dockPanel.PreviewMouseLeftButtonDown += ButtonEmpId_Click;
                dockPanel.Tag = dr["EmployeeID"].ToString();

                Button button = new Button();
                button.Width = 96.03;
                button.Height = 96.03;
                //button.Margin = new Thickness(0,-20,0,0);
                button.Content = dr["DisplayName"].ToString();
                button.Tag = dr["EmployeeID"].ToString();
                //button.Click += ButtonEmpId_Click; 
                button.Style = this.FindResource("CustomEmp" + GetStyleIndex(dr["EmployeeID"].ToString().Substring(0, 1))) as Style;

                if (SystemVariables.SelectedTheme == "Light")
                {
                    button.BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)button.Background).Color) { Opacity = 0.2 };
                }
                if (SystemVariables.SelectedTheme == "Dark")
                {
                    button.BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)button.Background).Color) { Opacity = 0.5 };
                }

                Grid grd1 = new Grid();
                grd1.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                grd1.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                Grid grd2 = new Grid();
                grd2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grd2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });


                bool blflg1 = false;

                PosDataObject.Login objLogin3 = new PosDataObject.Login();
                objLogin3.Connection = SystemVariables.Conn;
                blflg1 = objLogin3.IsNotClockoutEmployee(dr["EmployeeID"].ToString());

                if (blflg1)
                {
                    System.Windows.Controls.Image img1 = new System.Windows.Controls.Image();
                    img1.Source = this.FindResource("Online") as System.Windows.Media.ImageSource;
                    img1.Width = 13.0;
                    img1.Height = 13.0;
                    img1.VerticalAlignment = VerticalAlignment.Top;
                    img1.Margin = new Thickness(-10, 5, 0, 0);
                    Grid.SetRow(img1, 0);
                    grd2.Children.Add(img1);
                }


                bool blflg2 = false;
                if (LoginSection == LoginSection.POS)
                {
                    PosDataObject.Login objLogin = new PosDataObject.Login();
                    objLogin.Connection = SystemVariables.Conn;
                    blflg2 = objLogin.IsNotForcedPasscode(dr["EmployeeID"].ToString());
                }

                if (!blflg2)
                {
                    System.Windows.Controls.Image img2 = new System.Windows.Controls.Image();
                    img2.Source = this.FindResource("Lock") as System.Windows.Media.ImageSource;
                    img2.Width = 11.0;
                    img2.Height = 11.0;
                    img2.VerticalAlignment = VerticalAlignment.Bottom;
                    img2.Margin = new Thickness(-10, 5, 0, 5);
                    Grid.SetRow(img2, 1);
                    grd2.Children.Add(img2);
                }

                Grid.SetColumn(grd2, 1);
                grd1.Children.Add(grd2);


                dockPanel.Children.Add(button);
                Grid.SetColumn(dockPanel, 0);
                grd1.Children.Add(dockPanel);

                grd1.HorizontalAlignment = HorizontalAlignment.Center;

                grd.Children.Add(grd1);

                TextBlock tb = new TextBlock();
                tb.Text = dr["UserName"].ToString();
                tb.Style = this.FindResource("LoginUser") as Style;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontFamily = this.FindResource("OSSemiBold") as System.Windows.Media.FontFamily;
                tb.FontSize = 13f;
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Padding = new Thickness(5);
                tb.VerticalAlignment = VerticalAlignment.Top;

                Grid.SetRow(tb, 1);
                grd.Children.Add(tb);

                TextBlock tb1 = new TextBlock();
                tb1.Text = dr["Email"].ToString();
                tb1.Style = this.FindResource("ThemeTextColor") as Style;
                tb1.FontSize = 8f;
                tb1.TextAlignment = TextAlignment.Center;
                tb1.TextWrapping = TextWrapping.Wrap;
                tb1.TextTrimming = TextTrimming.CharacterEllipsis;
                tb1.Padding = new Thickness(5);
                tb1.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(tb1, 2);
                grd.Children.Add(tb1);


                pnlEmp.Children.Add(grd);

            }
        }

        private void InitializeUserControls()
        {
            if (LoginSection == LoginSection.POS)
            {
                if (POSControl == null)
                {
                    POSControl = new UserControls.POSControl();
                    POSControl.Name = "POSControl";
                    ControlsGrid.Children.Add(POSControl);
                    POSControl.Visibility = Visibility.Collapsed;
                }
            }
            if (LoginSection == LoginSection.Admin)
            {
                if (frm_MainAdmin == null)
                {
                    frm_MainAdmin = new UserControls.frm_MainAdmin();
                    frm_MainAdmin.Name = "frm_MainAdmin";
                    ControlsGrid.Children.Add(frm_MainAdmin);
                    frm_MainAdmin.Visibility = Visibility.Collapsed;
                }
            }
            if (LoginSection == LoginSection.ClickInClockOut_Employee)
            {
                if (ClockInClockOutEmployeeControl == null)
                {
                    ClockInClockOutEmployeeControl = new UserControls.ClockInClockOutEmployeeControl();
                    ClockInClockOutEmployeeControl.Name = "ClockInClockOutEmployeeControl";
                    ControlsGrid.Children.Add(ClockInClockOutEmployeeControl);
                    ClockInClockOutEmployeeControl.Visibility = Visibility.Collapsed;
                }
            }
            if (LoginSection == LoginSection.CashOut_CloseOut)
            {
                if (frmCloseoutDlg_CashOut == null)
                {
                    frmCloseoutDlg_CashOut = new UserControls.frmCloseoutDlg_CashOut();
                    frmCloseoutDlg_CashOut.Name = "frmCloseoutDlg_CashOut";
                    ControlsGrid.Children.Add(frmCloseoutDlg_CashOut);
                    frmCloseoutDlg_CashOut.Visibility = Visibility.Collapsed;
                }
            }
            if (LoginSection == LoginSection.SalesSnapShot_Dashboard)
            {
                if (frm_DashboardDlg_SalesSnapshot == null)
                {
                    frm_DashboardDlg_SalesSnapshot = new UserControls.frm_DashboardDlg_SalesSnapshot();
                    frm_DashboardDlg_SalesSnapshot.Name = "frm_DashboardDlg_SalesSnapshot";
                    ControlsGrid.Children.Add(frm_DashboardDlg_SalesSnapshot);
                    frm_DashboardDlg_SalesSnapshot.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ButtonEmpId_Click(object sender, RoutedEventArgs e)
        {
            string selectedempId = (sender as System.Windows.Controls.DockPanel).Tag.ToString();
            Dispatcher.BeginInvoke(new System.Action(() => SetSeletionStyle(selectedempId)));
            strSelectedUserID = selectedempId;
            if (!string.IsNullOrEmpty(KeyPadPasswordControl.Password)) KeyPadPasswordControl.ResetPassCode();

            Dispatcher.BeginInvoke(new System.Action(() => InitializeUserControls()));

            if (LoginSection == LoginSection.POS)
            {

                PosDataObject.Login objLogin = new PosDataObject.Login();
                objLogin.Connection = SystemVariables.Conn;
                bool blflg = objLogin.IsNotForcedPasscode(selectedempId);

                if (blflg)
                {
                    //Cursor = Cursors.Wait;
                    int intLogID = 0;
                    string strLogCode = "";
                    string strLogName = "";
                    bool blNew = false;
                    bool bllocked = false;
                    bool blExpired = false;
                    string strExpireDue = "";
                    if (((strSelectedUserID == "1") || (strSelectedUserID == "")) && (KeyPadPasswordControl.Password == "1111"))
                    {
                        if (LoginSection != LoginSection.ClickInClockOut_Employee)
                        {
                            if (GeneralFunctions.GetRecordCount("Employee") == 0) blNew = true;
                        }
                    }

                    if (IsValid() || blNew)
                    {
                        if (blNew)
                        {
                            SystemVariables.CurrentUserCode = "";
                            SystemVariables.CurrentUserName = "STORE ADMIN";
                            SystemVariables.CurrentUserID = -1;
                        }
                        else if (KeyPadPasswordControl.Password == Settings.ADMINPASSWORD)
                        {
                            SystemVariables.CurrentUserCode = "";
                            SystemVariables.CurrentUserName = "ADMIN";
                            SystemVariables.CurrentUserID = 0;
                        }
                        else
                        {
                            string apswd = "";
                            bool freqd = false;

                            PosDataObject.Login objLogin1 = new PosDataObject.Login();
                            objLogin1.Connection = SystemVariables.Conn;
                            int intreturn = objLogin1.GetLoginDetails(1, strSelectedUserID, KeyPadPasswordControl.Password,
                                                                     freqd, apswd, ref intLogID, ref strLogCode, ref strLogName,
                                                                     ref bllocked, ref strExpireDue, ref blExpired);
                            if (intreturn == 0)
                            {
                                new MessageBoxWindow().Show(Properties.Resources.Invalid_Login_ID___Password, Properties.Resources.Login_Validation, MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }

                            SystemVariables.CurrentUserCode = strLogCode;
                            SystemVariables.CurrentUserName = strLogName;
                            SystemVariables.CurrentUserID = intLogID;

                        }
                        Settings.GetUserCustomizationParameters();
                        DoLogin();
                    }
                }
            }
        }

        public void ResetSeletionStyle()
        {
            strSelectedUserID = "";
            foreach (UIElement c in pnlEmp.Children)
            {
                if (c is Grid)
                {



                    foreach (UIElement c1 in (c as Grid).Children)
                    {

                        if (c1 is Grid)
                        {
                            foreach (UIElement c2 in (c1 as Grid).Children)
                            {
                                if (c2 is System.Windows.Controls.Button)
                                {
                                    if (SystemVariables.SelectedTheme == "Light")
                                    {
                                        (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.2 };
                                    }
                                    if (SystemVariables.SelectedTheme == "Dark")
                                    {
                                        (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.5 };
                                    }
                                }
                            }
                        }


                    }
                }
            }
        }

        private void SetSeletionStyle(string seletedTag)
        {
            foreach (UIElement c in pnlEmp.Children)
            {
                if (c is Grid)
                {
                    foreach (UIElement c1 in (c as Grid).Children)
                    {
                        if (c1 is Grid)
                        {
                            foreach (UIElement c2 in (c1 as Grid).Children)
                            {
                                if (c2 is System.Windows.Controls.Button)
                                {
                                    if ((c2 as System.Windows.Controls.Button).Tag.ToString() != seletedTag)
                                    {
                                        if (SystemVariables.SelectedTheme == "Light")
                                        {
                                            (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.2 };
                                            (c2 as System.Windows.Controls.Button).Refresh();
                                        }
                                        if (SystemVariables.SelectedTheme == "Dark")
                                        {
                                            (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.5 };
                                            (c2 as System.Windows.Controls.Button).Refresh();
                                        }
                                    }
                                    else
                                    {
                                        if (SystemVariables.SelectedTheme == "Light")
                                        {
                                            (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.5 };
                                            (c2 as System.Windows.Controls.Button).Refresh();
                                        }
                                        if (SystemVariables.SelectedTheme == "Dark")
                                        {
                                            (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.2 };
                                            (c2 as System.Windows.Controls.Button).Refresh();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        ///     Replica of SetSeletionStyle with additional dockpanel handled.
        ///     As button is added in dockpanel to make border/side area clickable too
        ///     Tanzeel.Ali
        /// </summary>
        /// <param name="seletedTag"></param>
        private void SetSeletionStyleNew(string seletedTag)
        {
            foreach (UIElement c in pnlEmp.Children)
            {
                if (c is Grid)
                {
                    foreach (UIElement c1 in (c as Grid).Children)
                    {
                        if (c1 is Grid)
                        {
                            foreach (UIElement c2Grid in (c1 as Grid).Children)
                            {
                                if (c2Grid is DockPanel)
                                {
                                    foreach (UIElement c2 in (c2Grid as DockPanel).Children)
                                    {
                                        if (c2 is System.Windows.Controls.Button)
                                        {
                                            if ((c2 as System.Windows.Controls.Button).Tag.ToString() != seletedTag)
                                            {
                                                if (SystemVariables.SelectedTheme == "Light")
                                                {
                                                    (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.2 };
                                                }
                                                if (SystemVariables.SelectedTheme == "Dark")
                                                {
                                                    (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.5 };
                                                }
                                            }
                                            else
                                            {
                                                if (SystemVariables.SelectedTheme == "Light")
                                                {
                                                    (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.5 };
                                                }
                                                if (SystemVariables.SelectedTheme == "Dark")
                                                {
                                                    (c2 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(((System.Windows.Media.SolidColorBrush)(c2 as System.Windows.Controls.Button).Background).Color) { Opacity = 0.2 };
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private string GetStyleIndex(string strch)
        {
            string indx = "";
            if ((strch.ToUpper() == "A") || (strch.ToUpper() == "B") || (strch == "0"))
            {
                indx = "1";
            }
            if ((strch.ToUpper() == "C") || (strch.ToUpper() == "D") || (strch == "1"))
            {
                indx = "2";
            }
            if ((strch.ToUpper() == "E") || (strch.ToUpper() == "F") || (strch == "2"))
            {
                indx = "3";
            }
            if ((strch.ToUpper() == "G") || (strch.ToUpper() == "H") || (strch == "3"))
            {
                indx = "4";
            }
            if ((strch.ToUpper() == "I") || (strch.ToUpper() == "J") || (strch == "4"))
            {
                indx = "5";
            }
            if ((strch.ToUpper() == "K") || (strch.ToUpper() == "L") || (strch == "5"))
            {
                indx = "6";
            }
            if ((strch.ToUpper() == "M") || (strch.ToUpper() == "N") || (strch == "6"))
            {
                indx = "7";
            }
            if ((strch.ToUpper() == "O") || (strch.ToUpper() == "P") || (strch == "7"))
            {
                indx = "8";
            }
            if ((strch.ToUpper() == "Q") || (strch.ToUpper() == "R") || (strch.ToUpper() == "S") || (strch == "8"))
            {
                indx = "9";
            }
            if ((strch.ToUpper() == "T") || (strch.ToUpper() == "U") || (strch.ToUpper() == "V") || (strch.ToUpper() == "W") || (strch.ToUpper() == "X") || (strch.ToUpper() == "Y") || (strch.ToUpper() == "Z") || (strch == "9"))
            {
                indx = "10";
            }

            if (indx == "") indx = "10";

            return indx;
        }

        private void BtnFullScreen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Topmost = false;
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;

                btnFullScreen.Source = this.FindResource("FullScreenIn") as System.Windows.Media.ImageSource;

                this.ResizeMode = ResizeMode.NoResize;
                this.Visibility = Visibility.Collapsed;
                // this.Topmost = true;

                //// re-show the window after changing style
                this.Visibility = Visibility.Visible;
                MaxHeight = SystemParameters.VirtualScreenHeight; MaxWidth = SystemParameters.VirtualScreenWidth;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
                ResizeMode = ResizeMode.CanResize;

                //WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Top = 0.0;
                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                btnFullScreen.Source = this.FindResource("FullScreenOut") as System.Windows.Media.ImageSource;

            }
        }

        private void CmbTheme_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void PopulateThemes()
        {
            /* DataTable dtbl = new DataTable();
             dtbl.Columns.Add("Name");

             dtbl.Rows.Add(new object[] { "Dark" });
             dtbl.Rows.Add(new object[] { "Light" });


             cmbTheme.ItemsSource = dtbl;*/
        }

        private void CmbTheme_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            /*
            SystemVariables.SelectedTheme = cmbTheme.EditValue.ToString();
            if (SystemVariables.SelectedTheme == "Dark")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019BlackName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Dark/DarkTheme.xaml", UriKind.Relative)
                });
            }

            if (SystemVariables.SelectedTheme == "Light")
            {
                ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Images.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Icons.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Fonts.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/GenericLightStyles.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomButtonLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ListButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/LinkButtonStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/CustomTextBoxStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Controls/ModalWindowLightStyle.xaml", UriKind.Relative)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("OfflineRetailV2;component/Resources/Themes/Light/LightTheme.xaml", UriKind.Relative)
                });
            }*/
        }

        private void BtnECHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pnlMenuHeader.Width > 50.0)
            {
                pnlMenuHeader.Width = 50.0;
                imghide.Visibility = Visibility.Collapsed;
                imgshow.Visibility = Visibility.Visible;
                lbShowHideH.Text = "Show";
                btnECHeader.SetValue(Canvas.LeftProperty, -15.0);
            }
            else
            {
                pnlMenuHeader.Width = 100.0;
                imghide.Visibility = Visibility.Visible;
                imgshow.Visibility = Visibility.Collapsed;
                lbShowHideH.Text = "Hide";
                btnECHeader.SetValue(Canvas.LeftProperty, -15.0);
            }
        }

        private void NavGroupExit1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginSection = LoginSection.None;
            LoginBorder.Visibility = Visibility.Collapsed;
            LoginMenuBorder.Visibility = Visibility.Visible;
        }

        public void InitializeLoginScreenAfterReset()
        {
            LoginSection = LoginSection.POS;
            LoginMenuBorder.Visibility = Visibility.Collapsed;
            LoginBorder.Visibility = Visibility.Visible;
            Dispatcher.BeginInvoke(new System.Action(() => LoadEmployees()));
            LoginGrid.Visibility = Visibility.Visible;

        }

        public bool IsUserSelected()
        {
            if (GeneralFunctions.GetRecordCount("Employee") == 0)
            {
                return true;
            }
            else
            {
                if (strSelectedUserID == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }




        #region Version Update

        private bool CheckLatestVersion(bool silent)
        {
            try
            {
                bool boolResult = false;
                Cursor = Cursors.Wait;

                string strresult = ""; string strCurrentVersion = "";

                if (!IsNewVerionCheck)
                {
                    LoginMenuBorder.Visibility = Visibility.Collapsed;
                    pnlAutoUpdate.Visibility = Visibility.Collapsed;

                    pnlAutoUpdateCheck.Visibility = Visibility.Visible;
                    this.Refresh();
                }

                long versiontxtfile = GetFtpFileSizeVersionText(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Version.txt"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));
                long msiFile = GetFtpFileSizeMsi(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));

                if ((versiontxtfile.ToString() == "0") || (msiFile.ToString() == "0"))
                {
                    
                }
                else
                {

                    using (var client = new WebClient())
                    {


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
                            _versionTimer.Stop();

                            if (!(silent))
                            {
                                pnlAutoUpdateCheck.Visibility = Visibility.Collapsed;
                                DocMessage.MsgInformation(SystemVariables.BrandName + " Application will be updated");
                                //CustMessage.MsgInformation("PowerSource Application will be updated");


                            }

                            _versionTimerNet.Start();

                            MsiSize = GetFtpFileSize(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"), new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@"));

                            // MsiSize = GetFtpFileSize(new Uri(@"ftp://ftp.ennovativetechnologies.com//rajib/MyProducts/XePos%20WPF%20Retail/Retail2020Setup.msi"), new NetworkCredential("rajib", "rajib123"));



                            booldownloadStarted = true;
                            WebClient DownloadClient = new WebClient();
                            DownloadClient.Credentials = new NetworkCredential("0141654|xeposhqcom0", "ddrN4C3YnxY&Mbt@");
                            DownloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadClient_DownloadProgress);
                            DownloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadClient_DownloadDataCompleted);


                            DownloadClient.DownloadFileAsync(new Uri(@"ftp://ftp.eweb803.discountasp.net/VersionUpdate/Retail/Retail2020Setup.msi"),
                                GeneralFunctions.DownloadPath() + "Retail2020Setup.msi");
                            // DownloadClient.DownloadFileAsync(new Uri(@"ftp://ftp.ennovativetechnologies.com//rajib/MyProducts/XePos%20WPF%20Retail/Retail2020Setup.msi"),
                            //GeneralFunctions.DownloadPath() + "Retail2020Setup.msi");

                            LoginMenuBorder.Visibility = Visibility.Collapsed;
                            pnlAutoUpdateCheck.Visibility = Visibility.Collapsed;
                            pnlAutoUpdate.Visibility = Visibility.Visible;
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
                string xxx = ex.Message;
                DocMessage.MsgError("Can not Connect to Server");
                LoginMenuBorder.Visibility = Visibility.Visible;
                pnlAutoUpdate.Visibility = Visibility.Collapsed;
                pnlAutoUpdateCheck.Visibility = Visibility.Collapsed;
                _versionTimer.Start();
                Cursor = Cursors.Arrow;
                booldownloadCompleted = true;
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
                        string NewVersion = strresult;
                        strCurrentVersion = GeneralFunctions.VersionInfoForUpdate();
                        strresult = strresult.Replace(".", "");
                        strCurrentVersion = strCurrentVersion.Replace(".", "");
                        if (Convert.ToInt16(strCurrentVersion) < Convert.ToInt16(strresult))
                        {
                            boolResult = NewVersion;
                        }
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
                System.Diagnostics.Process.Start(GeneralFunctions.DownloadPath() + "Retail2020Setup.msi");
                booldownloadCompleted = true;
                _versionTimer.Stop();
                Close();
            }
            catch
            {
                DocMessage.MsgError("Can not Connect to Server");
                _versionTimer.Stop();
                Close();
            }

        }

        private void DownloadClient_DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
           
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


        private void CheckInternet()
        {
            
            //_versionTimerNet.Start();
            if (InternetChecker.IsConnectedToInternet())
            {
               
            }
            else
            {
                _versionTimerNet.Stop();
                _versionTimer.Stop();
                DocMessage.MsgError("Can not Connect to Server");
                Close();
            }
        }

        public void StopVersionTimer()
        {
            _versionTimer.Stop();
        }

        public void RestartVersionTimer()
        {
            _versionTimer.Start();
        }

        public void ShutDown()
        {
            Application.Current.Shutdown();
        }
        

        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
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