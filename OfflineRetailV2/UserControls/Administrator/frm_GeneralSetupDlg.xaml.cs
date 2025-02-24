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
using Microsoft.Win32;
using System.IO;
using System.IO.Ports;
using System.Net.Mail;
using System.Drawing.Printing;
using Microsoft.PointOfService;

using DevExpress.Xpf.Editors;
using System.Diagnostics;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using TaskScheduler;
using Microsoft.Win32.TaskScheduler;
using System.Security.Cryptography;
using static OfflineRetailV2.XeroConstants.XeroUrls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_GeneralSetupDlg.xaml
    /// </summary>
    public partial class frm_GeneralSetupDlg : Window
    {
        PosExplorer m_posExplorer = null; // Variables to get Device Drivers from Local ComputerC:\MyProducts\XEPOS\RetailV2\src\SuperSalonProject\OfflineRetailV2\Data\Translation.cs

        private string xerocodeverifier = "";
        private string xeroauthlink = "";
        private string xeroscopes = "";
        private string xerostate = "";
        private string xerotenantId = "";

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool boolChangeStyle = false;

        private bool boolIsThemeCHanged = false;

             
        public frm_GeneralSetupDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
            Loaded += Window_Loaded;
        }

        private string _authorisationCode;
        public string AuthorisationCode
        {
            get => _authorisationCode;

            set
            {
                txtXeroReturnCode.Text = value;
                _authorisationCode = value;
            }
        }

        private string _state;
        public string State
        {
            get => _state;

            set
            {
                txtXeroReturnState.Text = value;
                _state = value;
            }
        }

        private void OnCloseCommand(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            Close();
        }

        #region Variables
        private bool boolLoad;
        private int intID;
        private bool boolControlChanged;
        private string strPrevFormStyle;
        private bool blSetStyle = false;
        private DataTable dtblPOSFunc = null;
        private DataTable dtblScaleFunc = null;
        private DataTable dtblScaleDept = null;
        private bool blSetPOSFunction = false;
        private bool blSetScaleFunction = false;
        private string PrevProductImgDisplay;
        private bool blChangeProductImgDisplay = false;
        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool blfocus = false;
        private bool blFunctionBtnAccess;
        private bool blFunctionOrderChangeAccess;
        private bool blCloseout = false;
        private int intSuperUserID;
        private bool blRH = false;
        private bool blPrevPO;
        private bool blPrevCustomer;
        private bool blPrevGC;

        private string strExportDir = "";
        private string strExportPath = "";
        private string strExportFile = "";
        private DataTable dtblLink = null;
        private bool blViewPrevFile = false;
        private bool blcallinkprinter = false;

        private string strAdvtDispArea = "";
        private string strAdvtScale = "";

        private string strAdvtDispNo = "";
        private string strAdvtDispTime = "";
        private string strAdvtDispOrder = "";
        private string strAdvtFont = "";
        private string strAdvtColor = "";
        private string strAdvtBackground = "";
        private string strAdvtDir = "";

        private string strPrevAdvtBG = "";
        private string strAdvtBG = "";

        private string pCOMPort = "";
        private string pBaudRate = "";
        private string pDataBits = "";
        private string pStopBits = "";
        private string pParity = "";
        private string pHandshake = "";
        private string pTimeout = "";
        private bool isloadcomplete = false;

        private bool isaccesspaymentconfig = false;
        private bool ischangepaymentconfig = false;

        private bool blchangeskudisplay = false;

        private bool bCallFromAdmin = false;


        private int ReScan1 = 0;
        private string strPhotoFile1 = "";
        private int intImageWidth1;
        private int intImageHeight1;
        private string csStorePath1 = "";


        private frm_MainAdmin pMainF;

        private DataTable dtblGraduation = null;

        private bool bActivateSecondMonitor = false;
        private string bInitialCulture = "";
        private string s_Dual;
        private string s_GrdText;
        private double d_FromWt1;
        private double d_FromWt2;
        private double d_ToWt1;
        private double d_ToWt2;
        private double d_Grd1;
        private double d_Grd2;

        private string s_SChk;
        private string s_DChk;

        private bool blCheckGrocer = false;

        private bool blCheckPriceSmart = false;

        private bool boolBookingExportFlagChanged;
        private string prevBookingExpFlag = "N";

        private int OldTabIndex = 0;

        public bool bLoad
        {
            get { return boolLoad; }
            set { boolLoad = value; }
        }

        public bool ThemeCHanged
        {
            get { return boolIsThemeCHanged; }
            set { boolIsThemeCHanged = value; }
        }

        public frm_MainAdmin MainF
        {
            get { return pMainF; }
            set { pMainF = value; }
        }

        public bool CallFromAdmin
        {
            get { return bCallFromAdmin; }
            set { bCallFromAdmin = value; }
        }

        public bool changeskudisplay
        {
            get { return blchangeskudisplay; }
            set { blchangeskudisplay = value; }
        }

        public bool SetPOSFunction
        {
            get { return blSetPOSFunction; }
            set { blSetPOSFunction = value; }
        }

        public bool ChangeProductImgDisplay
        {
            get { return blChangeProductImgDisplay; }
            set { blChangeProductImgDisplay = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        public bool FunctionOrderChangeAccess
        {
            get { return blFunctionOrderChangeAccess; }
            set { blFunctionOrderChangeAccess = value; }
        }

        public string Dual
        {
            get { return s_Dual; }
            set { s_Dual = value; }
        }

        public string GrdText
        {
            get { return s_GrdText; }
            set { s_GrdText = value; }
        }

        public double FromWt1
        {
            get { return d_FromWt1; }
            set { d_FromWt1 = value; }
        }

        public double FromWt2
        {
            get { return d_FromWt2; }
            set { d_FromWt2 = value; }
        }

        public double ToWt1
        {
            get { return d_ToWt1; }
            set { d_ToWt1 = value; }
        }

        public double ToWt2
        {
            get { return d_ToWt2; }
            set { d_ToWt2 = value; }
        }

        public double Grd1
        {
            get { return d_Grd1; }
            set { d_Grd1 = value; }
        }

        public double Grd2
        {
            get { return d_Grd2; }
            set { d_Grd2 = value; }
        }

        public string SChk
        {
            get { return s_SChk; }
            set { s_SChk = value; }
        }

        public string DChk
        {
            get { return s_DChk; }
            set { s_DChk = value; }
        }

        #endregion

        private void Label47_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All Files(*.*)|*.*";

            if (op.ShowDialog() == true)
            {
                txtSecondMonitorApp.Text = op.FileName;
                boolControlChanged = true;
            }
        }

        private void Label52_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_CustomerDisplaySetupDlg frm_CustomerDisplaySetupDlg = new POSSection.frm_CustomerDisplaySetupDlg();
            try
            {
                frm_CustomerDisplaySetupDlg.AdvtDispArea = strAdvtDispArea;
                frm_CustomerDisplaySetupDlg.AdvtScale = strAdvtScale;
                frm_CustomerDisplaySetupDlg.AdvtDispNo = strAdvtDispNo;
                frm_CustomerDisplaySetupDlg.AdvtDispTime = strAdvtDispTime;
                frm_CustomerDisplaySetupDlg.AdvtDispOrder = strAdvtDispOrder;
                frm_CustomerDisplaySetupDlg.AdvtFont = strAdvtFont;
                frm_CustomerDisplaySetupDlg.AdvtColor = strAdvtColor;
                frm_CustomerDisplaySetupDlg.AdvtBackground = strAdvtBackground;
                frm_CustomerDisplaySetupDlg.AdvtDir = strAdvtDir;
                frm_CustomerDisplaySetupDlg.ShowDialog();
                if (frm_CustomerDisplaySetupDlg.DialogResult == true)
                {
                    strAdvtDispArea = frm_CustomerDisplaySetupDlg.AdvtDispArea;
                    strAdvtScale = frm_CustomerDisplaySetupDlg.AdvtScale;
                    strAdvtDispNo = frm_CustomerDisplaySetupDlg.AdvtDispNo;
                    strAdvtDispTime = frm_CustomerDisplaySetupDlg.AdvtDispTime;
                    strAdvtDispOrder = frm_CustomerDisplaySetupDlg.AdvtDispOrder;
                    strAdvtFont = frm_CustomerDisplaySetupDlg.AdvtFont;
                    strAdvtColor = frm_CustomerDisplaySetupDlg.AdvtColor;
                    strAdvtBackground = frm_CustomerDisplaySetupDlg.AdvtBackground;
                    strAdvtDir = frm_CustomerDisplaySetupDlg.AdvtDir;
                    strAdvtBG = frm_CustomerDisplaySetupDlg.AdvtBGImage;
                    strPrevAdvtBG = frm_CustomerDisplaySetupDlg.PrevAdvtBGImage;
                }
            }
            finally
            {
                frm_CustomerDisplaySetupDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnTestEmail_Click(object sender, RoutedEventArgs e)
        {
            if ((txtREHost.Text.Trim() == "") || (txtREUser.Text.Trim() == "") ||
                (txtREPort.Text.Trim() == "") || (txtREFromEmail.Text.Trim() == ""))
            {
                DocMessage.MsgInformation("Please check if any of the following is empty" + "\r\n" + "   " + "SMTP Host" + "\r\n" + "   " + "SMTP Port" + "\r\n" + "   " + "SMTP User ID" + "\r\n" + "   " + "From Address");
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_EmailTestDlg frm = new frm_EmailTestDlg();
            try
            {
                frm.SMTP_Host = txtREHost.Text.Trim();
                frm.SMTP_Port = txtREPort.Text;
                frm.SMTP_User = txtREUser.Text.Trim();
                frm.SMTP_Pswd = txtREPassword.Text.Trim();
                if (chkRESSL.IsChecked == true)
                    frm.SMTP_SSL = "Y";
                else
                    frm.SMTP_SSL = "N";

                frm.From_Addr = txtREFromEmail.Text.Trim();
                frm.To_Addr = txtReportEmail.Text.Trim();
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Rglogo_0_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (rglogo_0.IsChecked == true)
            {
                tabLogo.SelectedIndex = 0;
            }
            if (rglogo_1.IsChecked == true)
            {
                tabLogo.SelectedIndex = 1;
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                boolControlChanged = true;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
        }

        private void BtnAcctExp_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_AcctExpDlg fdlg = new frm_AcctExpDlg();
            try
            {
                fdlg.ShowDialog();
            }
            finally
            {
                fdlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_QuickBooksExportDlg frm_QuickBooksExportDlg = new frm_QuickBooksExportDlg();
            try
            {
                frm_QuickBooksExportDlg.ShowDialog();
            }
            finally
            {
                frm_QuickBooksExportDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void ChkAutoSignout_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (chkAutoSignout.IsChecked == true)
            {
                txtLoginTime.Visibility = Visibility.Visible;
                lbLoginTime.Visibility = Visibility.Visible;
            }
            else
            {
                txtLoginTime.Visibility = Visibility.Hidden;
                lbLoginTime.Visibility = Visibility.Hidden;
            }
        }

        private bool IsCloseout()
        {
            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int COUTID = objCloseout.GetCloseOutID("G", "C", SystemVariables.CurrentUserID, Settings.TerminalName);
            if (COUTID == 0) return false;
            else return true;
        }

        private void RgCloseout_0_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            /*if (!blCloseout) return;
            if (IsCloseout())
            {
                int selectedIndx = -1;
                if (rgCloseout_0.IsChecked == true) selectedIndx = 0;
                if (rgCloseout_1.IsChecked == true) selectedIndx = 1;
                if (rgCloseout_2.IsChecked == true) selectedIndx = 2;
                if (selectedIndx == Settings.CloseoutOption) return;
                DocMessage.MsgInformation("This option can only be changed after a consolidated close out");
                blCloseout = false;
                if (Settings.CloseoutOption == 0) rgCloseout_0.IsChecked = true;
                else if (Settings.CloseoutOption == 1) rgCloseout_1.IsChecked = true;
                else rgCloseout_2.IsChecked = true;
                blCloseout = true;

            }
            else
            {
                boolControlChanged = true;
            }*/
        }


        private void PopulateLabelPrinter()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("PrinterName");

            
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                dtbl.Rows.Add(new object[] { strPrinter });
            }
            cmbLabelPrinter.ItemsSource = dtbl;
            dtbl.Dispose();
            cmbLabelPrinter.SelectedIndex = 0;
        }

        public void PopulateCOMPrinter()
        {
            PosDataObject.Setup objShift = new PosDataObject.Setup();
            objShift.Connection = SystemVariables.Conn;
            DataTable dbtblShift = new DataTable();
            dbtblShift = objShift.FetchCOMPrinterLookup();

            cmbCOMPrinter.ItemsSource = dbtblShift;
            cmbCOMPrinter.EditValue = null;
            dbtblShift.Dispose();
        }

        private void RgLabelPrinter_0_Checked(object sender, RoutedEventArgs e)
        {
            if (rgLabelPrinter_0.IsChecked == true)
            {
                cmbLabelPrinter.Visibility = Visibility.Visible;
                cmbCOMPrinter.Visibility = Visibility.Collapsed;
                //chkBartender.Visibility = Visibility.Visible;
            }
            else
            {
                cmbLabelPrinter.Visibility = Visibility.Collapsed;
                cmbCOMPrinter.Visibility = Visibility.Visible;
                //chkBartender.Visibility = Visibility.Collapsed;
            }
            boolControlChanged = true;
        }


        private void PopulateMercuryPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            pay2_pinpadport.Items.Clear();
            foreach (string port in ports)
            {
                pay2_pinpadport.Items.Add(port);
            }
            pay2_pinpadport.SelectedIndex = 0;
        }

        private void PopulateReportPrinter()
        {
            cmbReportPrinter.Items.Clear();
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbReportPrinter.Items.Add(strPrinter);
            }
            cmbReportPrinter.SelectedIndex = 0;
        }

        // get attached printers ( Receipt Printing )

        private void PopulateReceiptPrinter()
        {
            cmbReceiptPrinter.Items.Clear();
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbReceiptPrinter.Items.Add(strPrinter);
            }
            cmbReceiptPrinter.SelectedIndex = 0;
        }

        private void PopulatePorts()
        {
            string[] ports = SerialPort.GetPortNames();

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("port");

            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                dtbl.Rows.Add(new object[] { strPrinter });
            }

            foreach (string port in ports)
            {
                dtbl.Rows.Add(new object[] { port });
            }
            dtbl.Rows.Add(new object[] { "(None)" });
            cmbport.ItemsSource = dtbl;
            cmbport.EditValue = "(None)";
            dtbl.Dispose();
        }

        private void PopulateLineDisplayDevice()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("device");
            dtbl.Rows.Add(new object[] { "(None)" });

            m_posExplorer = new PosExplorer();
            Microsoft.PointOfService.DeviceInfo deviceInfo = null;
            DeviceCollection deviceCollection = m_posExplorer.GetDevices();
            for (int i = 0; i < deviceCollection.Count; i++)
            {
                deviceInfo = deviceCollection[i];
                dtbl.Rows.Add(new object[] { deviceInfo.ServiceObjectName });
                
            }
            cmbLineDisplay.ItemsSource = dtbl;
            cmbLineDisplay.SelectedIndex = 0;
            dtbl.Dispose();
        }

        private void PopulatePoleDisplaySerialPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("port");


            foreach (string port in ports)
            {
                dtbl.Rows.Add(new object[] { port });
            }
            
            dtbl.Rows.Add(new object[] { "(None)" });
            cmbLineDisplaySP.ItemsSource = dtbl;
            cmbLineDisplaySP.EditValue = "(None)";
            dtbl.Dispose();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cmbLineDisplay.ItemsSource = null;

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("device");
            dtbl.Rows.Add(new object[] { "(None)" });

            m_posExplorer = new PosExplorer();

            Microsoft.PointOfService.DeviceInfo deviceInfo = null;
            DeviceCollection deviceCollection = m_posExplorer.GetDevices();

            for (int i = 0; i < deviceCollection.Count; i++)
            {
                deviceInfo = deviceCollection[i];
                dtbl.Rows.Add(new object[] { deviceInfo.ServiceObjectName });
            }
            cmbLineDisplay.ItemsSource = dtbl;
            cmbLineDisplay.EditValue = Settings.LineDisplayDevice;
            if (cmbLineDisplay.SelectedIndex == -1) cmbLineDisplay.SelectedIndex = 0;
        }

        private void CmbScaleType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void CmbScaleType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            lbscaleport.Visibility = cmbport.Visibility = btnportsetup.Visibility = (cmbScaleType.SelectedIndex == 0
                || cmbScaleType.SelectedIndex == 2) ? Visibility.Visible : Visibility.Hidden;
            chk8200Scanner.Visibility = cmbScaleType.SelectedIndex == 1 ? Visibility.Visible : Visibility.Hidden;
            if (cmbScaleType.SelectedIndex == 0 || cmbScaleType.SelectedIndex == 2 || cmbScaleType.SelectedIndex == 3)
            {
                chk8200Scanner.IsChecked = false;
            }
            

            cmbGRS.Visibility = cmbScaleType.SelectedIndex == 2 ? Visibility.Visible : Visibility.Hidden;

            boolControlChanged = true;
        }

        private void CmbGRS_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbGRS.SelectedIndex == 0)
            {
                txtMaxWeight.Text = "60";
            }
            else
            {
                txtMaxWeight.Text = "30";
            }
            boolControlChanged = true;
        }

        private void Btnportsetup_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_PortSetupDlg frm_PortSetupDlg = new frm_PortSetupDlg();
            try
            {
                frm_PortSetupDlg.COMPort = cmbport.Text;
                frm_PortSetupDlg.BaudRate = pBaudRate;
                frm_PortSetupDlg.DataBits = pDataBits;
                frm_PortSetupDlg.StopBits = pStopBits;
                frm_PortSetupDlg.Parity = pParity;
                frm_PortSetupDlg.Handshake = pHandshake;
                frm_PortSetupDlg.Timeout = pTimeout;
                frm_PortSetupDlg.ShowDialog();
                if (frm_PortSetupDlg.DialogResult == true)
                {
                    pBaudRate = frm_PortSetupDlg.BaudRate;
                    pDataBits = frm_PortSetupDlg.DataBits;
                    pStopBits = frm_PortSetupDlg.StopBits;
                    pParity = frm_PortSetupDlg.Parity;
                    pHandshake = frm_PortSetupDlg.Handshake;
                    pTimeout = frm_PortSetupDlg.Timeout;
                }
            }
            finally
            {
                frm_PortSetupDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void ChkPOS7_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (chkPOS7.IsChecked == true)
            {
                gcPub.IsEnabled = true;
            }
            else
            {
                gcPub.IsEnabled = false;
            }
        }

        private void RgPaymentGateway_0_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (rgPaymentGateway_0.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Visible;
                rgPaymentMode_1.Visibility = Visibility.Visible;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 0;
            }
            if (rgPaymentGateway_1.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Visible;
                rgPaymentMode_1.Visibility = Visibility.Visible;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 1;
            }
            if (rgPaymentGateway_2.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Hidden;
                rgPaymentMode_1.Visibility = Visibility.Hidden;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 2;
            }
            if (rgPaymentGateway_3.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Hidden;
                rgPaymentMode_1.Visibility = Visibility.Hidden;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 3;
            }
            if (rgPaymentGateway_4.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Visible;
                rgPaymentMode_1.Visibility = Visibility.Visible;
                rgCardEntryMode_0.Visibility = Visibility.Visible;
                rgCardEntryMode_1.Visibility = Visibility.Visible;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 4;
            }
            if (rgPaymentGateway_5.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Visible;
                rgPaymentMode_1.Visibility = Visibility.Visible;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Visible;
                tcPayment.SelectedIndex = 5;
            }

            if (rgPaymentGateway_6.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Hidden;
                rgPaymentMode_1.Visibility = Visibility.Hidden;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 6;
            }

            if (rgPaymentGateway_7.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Hidden;
                rgPaymentMode_1.Visibility = Visibility.Hidden;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 7;
            }

            if (rgPaymentGateway_8.IsChecked == true)
            {
                rgPaymentMode_0.Visibility = Visibility.Hidden;
                rgPaymentMode_1.Visibility = Visibility.Hidden;
                rgCardEntryMode_0.Visibility = Visibility.Hidden;
                rgCardEntryMode_1.Visibility = Visibility.Hidden;
                btnDEMV.Visibility = Visibility.Hidden;
                tcPayment.SelectedIndex = 8;
            }
        }

        private void ChkOnlineBooking_Checked(object sender, RoutedEventArgs e)
        {
            if (chkOnlineBooking.IsChecked == true)
            {
                sectionBk.IsEnabled = sectionBk1.IsEnabled = sectionBk2.IsEnabled = sectionBk3.IsEnabled = true;

            }
            else
            {
                sectionBk.IsEnabled = sectionBk1.IsEnabled = sectionBk2.IsEnabled = sectionBk3.IsEnabled = false;
            }
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private bool SetCloudConn()
        {
            string strconn = "server=" + txtcldSvr.Text.Trim() + ";database=" + txtcldDb.Text.Trim() + ";uid=" + txtcldUsr.Text.Trim() + ";pwd=" + txtcldPswd.Text.Trim();
            String ConnStr = "";
            System.Data.SqlClient.SqlConnection Conn;
            try
            {
                ConnStr = strconn;
                Conn = new System.Data.SqlClient.SqlConnection(ConnStr);
                SystemVariables.rmBookingConn = Conn;
                SystemVariables.rmBookingConnectionString = strconn;
                Conn.Open();
                Conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnRemoteConnect1_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (SetCloudConn())
                {
                    DocMessage.MsgInformation("Online Booking database connected successfully");
                }
                else
                    DocMessage.MsgInformation("Can not connect to Online Booking database. Please check the parameters.");
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        private bool CheckAndSetParameterBeforeTaskSchedulerActivity()
        {

            if (!SetCloudConn())
            {
                DocMessage.MsgInformation("Cannot connect to Online Booking database. Please check the parameters.");
                return false;
            }
            else
            {
                Settings.CloudServer = txtcldSvr.Text;
                Settings.CloudDB = txtcldDb.Text;
                Settings.CloudUser = txtcldUsr.Text;
                Settings.CloudPassword = txtcldPswd.Text;
                return true;
            }

        }

        private void SetScheduleEndTime(DateEdit Stime, DateEdit Etime)
        {
            bool nextday = false;
            TimeSpan ets = new TimeSpan(Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            TimeSpan sts = new TimeSpan(Stime.DateTime.Hour, Stime.DateTime.Minute, Stime.DateTime.Second);
            TimeSpan rts = ets.Subtract(sts);

            if ((Stime.DateTime.Hour < 12) && (Etime.DateTime.Hour < 12))
            {
                if (rts.TotalMinutes < 0) nextday = true; else nextday = false;
            }

            if ((Stime.DateTime.Hour < 12) && (Etime.DateTime.Hour >= 12))
            {
                nextday = false;
            }

            if ((Stime.DateTime.Hour >= 12) && (Etime.DateTime.Hour < 12))
            {
                nextday = true;
            }

            if ((Stime.DateTime.Hour >= 12) && (Etime.DateTime.Hour >= 12))
            {
                if (rts.TotalMinutes < 0) nextday = true; else nextday = false;
            }
            if (nextday)
            {
                DateTime dt = Stime.DateTime.AddDays(1);
                Etime.DateTime = new DateTime(dt.Year, dt.Month, dt.Day, Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            }
            else
            {
                DateTime dt = Stime.DateTime;
                Etime.DateTime = new DateTime(dt.Year, dt.Month, dt.Day, Etime.DateTime.Hour, Etime.DateTime.Minute, Etime.DateTime.Second);
            }
        }

        private void BtnGCSchedule_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckAndSetParameterBeforeTaskSchedulerActivity()) return;
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "BookingImportExport.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\BookingImportExport.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  ALL";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtBookSchdTime.DateTime.Hour, (short)txtBookSchdTime.DateTime.Minute, 1);

                    t.Triggers.Add(dt);
                    SetScheduleEndTime(txtBookSchdTime, rep3);
                    int days = 0;
                    if (rep3.DateTime.Date > txtBookSchdTime.DateTime.Date) days = 1;

                    TimeSpan ets = new TimeSpan(days, rep3.DateTime.Hour, rep3.DateTime.Minute, rep3.DateTime.Second);
                    TimeSpan sts = new TimeSpan(txtBookSchdTime.DateTime.Hour, txtBookSchdTime.DateTime.Minute, txtBookSchdTime.DateTime.Second);
                    TimeSpan rts = ets.Subtract(sts);


                    dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                    if (rep2.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep1.Value.ToString());
                    else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep1.Value.ToString()) * 60;

                    t.Save();
                    t.Close();

                    if (btnGCSchedule.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnGCSchedule.Content = "Modify Scheduler";
                        btnGCSchedule.Tag = "Modify Scheduler";
                        btnGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }


            if (Settings.OSVersion == "Win 11")
            {

                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtBookSchdTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtBookSchdTime.DateTime.Minute);
                    daily.DaysInterval = 1;


                    SetScheduleEndTime(txtBookSchdTime, rep3);
                    int days = 0;
                    if (rep3.DateTime.Date > txtBookSchdTime.DateTime.Date) days = 1;

                    TimeSpan ets = new TimeSpan(days, rep3.DateTime.Hour, rep3.DateTime.Minute, rep3.DateTime.Second);
                    TimeSpan sts = new TimeSpan(txtBookSchdTime.DateTime.Hour, txtBookSchdTime.DateTime.Minute, txtBookSchdTime.DateTime.Second);
                    TimeSpan rts = ets.Subtract(sts);

                    RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep2.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep1.Value.ToString()) : GeneralFunctions.fnInt32(rep1.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                    daily.Repetition = repetition;

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  ALL", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    if (btnGCSchedule.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnGCSchedule.Content = "Modify Scheduler";
                        btnGCSchedule.Tag = "Modify Scheduler";
                        btnGCRunSchedule.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated.");
                    }
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }
        }

        private void BtnGCRunSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAndSetParameterBeforeTaskSchedulerActivity()) return;

            /*try
            {
                ScheduledTasks st1 = new ScheduledTasks();
                TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName));
                if (t1 != null)
                {
                    Dispatcher.BeginInvoke(new Action(() => t1.Run()));
                }
            }
            catch
            {
            }*/

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(100);
            string schdName = SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName);
            try
            {
                Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();
                bool boolFindTask = false;
                boolFindTask = ts.GetTask(@"" + schdName) != null;
                if (boolFindTask) ts.GetTask(@"" + schdName).Run();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Rep2_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (rep2.SelectedIndex == 0)
            {
                rep1.MaxValue = 60;
            }
            if (rep2.SelectedIndex == 1)
            {
                rep1.MaxValue = 23;
            }
        }

        private void DisplaySelectedTime(DevExpress.Xpf.Editors.TrackBarEdit trkB, TextBlock lbC)
        {
            string strDisplayText = "";
            int iMinVal = (int)trkB.SelectionStart;
            int iMaxVal = (int)trkB.SelectionEnd;

            int minHour = iMinVal / 2;
            int minMin = iMinVal % 2;

            int maxHour = iMaxVal / 2;
            int maxMin = iMaxVal % 2;

            if (minHour == 0)
            {
                if (minMin == 0)
                {
                    strDisplayText = "12 AM";
                }
                else
                {
                    strDisplayText = "12:30 AM";
                }
            }
            if (maxHour == 0)
            {
                if (maxMin == 0)
                {
                    strDisplayText = strDisplayText + " - " + "12 AM";
                }
                else
                {
                    strDisplayText = strDisplayText + " - " + "12:30 AM";
                }
            }
            if (minHour > 0)
            {
                string sAMPM = "";
                if (minHour <= 11)
                {
                    sAMPM = "AM";
                }
                else
                {
                    sAMPM = "PM";
                    if (minHour > 12)
                    {
                        minHour = minHour - 12;
                    }
                }

                if (minMin == 0)
                {
                    strDisplayText = minHour.ToString() + " " + sAMPM;
                }
                else
                {
                    strDisplayText = minHour.ToString() + ":30 " + sAMPM;
                }
            }

            if (maxHour > 0)
            {
                string sAMPM = "";
                if (maxHour <= 11)
                {
                    sAMPM = "AM";
                }
                else
                {
                    sAMPM = "PM";
                    if (maxHour > 12)
                    {
                        maxHour = maxHour - 12;
                    }
                }

                if (maxMin == 0)
                {
                    strDisplayText = strDisplayText + " - " + maxHour.ToString() + " " + sAMPM;
                }
                else
                {
                    strDisplayText = strDisplayText + " - " + maxHour.ToString() + ":30 " + sAMPM;
                }
            }

            lbC.Text = strDisplayText;
        }

        private void TrkbarMon_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarMon, lbtimeMon);
            lbtimeMon.Visibility = chkMon.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void ChkMon_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarMon.IsEnabled = chkMon.IsChecked == true ? true : false;
            lbtimeMon.Visibility = chkMon.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkMon.IsChecked == true)
            {
                DisplaySelectedTime(trkbarMon, lbtimeMon);
            }
        }

        private void ChkTue_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarTue.IsEnabled = chkTue.IsChecked == true ? true : false;
            lbtimeTue.Visibility = chkTue.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkTue.IsChecked == true)
            {
                DisplaySelectedTime(trkbarTue, lbtimeTue);
            }
        }

        private void ChkWed_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarWed.IsEnabled = chkWed.IsChecked == true ? true : false;
            lbtimeWed.Visibility = chkWed.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkWed.IsChecked == true)
            {
                DisplaySelectedTime(trkbarWed, lbtimeWed);
            }
        }

        private void ChkThu_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarThu.IsEnabled = chkThu.IsChecked == true ? true : false;
            lbtimeThu.Visibility = chkThu.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkThu.IsChecked == true)
            {
                DisplaySelectedTime(trkbarThu, lbtimeThu);
            }
        }

        private void ChkFri_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarFri.IsEnabled = chkFri.IsChecked == true ? true : false;
            lbtimeFri.Visibility = chkFri.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkFri.IsChecked == true)
            {
                DisplaySelectedTime(trkbarFri, lbtimeFri);
            }
        }

        private void ChkSat_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarSat.IsEnabled = chkSat.IsChecked == true ? true : false;
            lbtimeSat.Visibility = chkSat.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkSat.IsChecked == true)
            {
                DisplaySelectedTime(trkbarSat, lbtimeSat);
            }
        }

        private void ChkSun_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            trkbarSun.IsEnabled = chkSun.IsChecked == true ? true : false;
            lbtimeSun.Visibility = chkSun.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            if (chkSun.IsChecked == true)
            {
                DisplaySelectedTime(trkbarSun, lbtimeSun);
            }
        }

        private void TrkbarTue_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarTue, lbtimeTue);
            lbtimeTue.Visibility = chkTue.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void TrkbarWed_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarWed, lbtimeWed);
            lbtimeWed.Visibility = chkWed.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void TrkbarThu_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarThu, lbtimeThu);
            lbtimeThu.Visibility = chkThu.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void TrkbarFri_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarFri, lbtimeFri);
            lbtimeFri.Visibility = chkFri.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void TrkbarSat_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarSat, lbtimeSat);
            lbtimeSat.Visibility = chkSat.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void TrkbarSun_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
            DisplaySelectedTime(trkbarSun, lbtimeSun);
            lbtimeSun.Visibility = chkSun.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }

        private void ChkSchdWindow_Checked(object sender, RoutedEventArgs e)
        {
            txtSchdWindow.Visibility = lbSchdWinDays.Visibility = chkSchdWindow.IsChecked == false ? Visibility.Visible : Visibility.Hidden;
            boolControlChanged = true;
        }

        private void TxtcldSvr_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbLeadDay_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbLeadHour_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbLeadMinute_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbSlotHour_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbSlotMinute_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbReminderHour_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void CmbReminderMinute_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void TxtSchdWindow_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolBookingExportFlagChanged = true;
        }

        private void FetchPrinterName()
        {
            PosDataObject.Setup objstup = new PosDataObject.Setup();
            objstup.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objstup.DataObjectCulture_None = Settings.DataObjectCulture_None;
            DataTable dtbl = new DataTable();
            dtbl = objstup.FetchPrinterData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdPrinter1.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();

        }

        public void FetchPortCommand()
        {
            PosDataObject.Setup objstup = new PosDataObject.Setup();
            objstup.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtbl = new DataTable();
            dtbl = objstup.FetchCOMPrinterCommand();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdCOMprinter.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void InitialLoadLinkPrinters()
        {
            DataTable dtbl = new DataTable();
            DataTable dtblTL = new DataTable();
            if (!blcallinkprinter)
            {
                dtblLink = new DataTable();
                dtblLink.Columns.Add("ID", System.Type.GetType("System.String"));
                dtblLink.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtblLink.Columns.Add("LinkPrinter", System.Type.GetType("System.String"));
                dtblLink.Columns.Add("LinkTemplate", System.Type.GetType("System.String"));

                PosDataObject.Setup objstup = new PosDataObject.Setup();
                objstup.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objstup.FetchLinkPrinterData(Settings.TerminalName);

                dtblTL = objstup.FetchLinkTemplateData(Settings.TerminalName);

                if (dtbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        int indx = dr["ParamName"].ToString().IndexOf("All Printers - ");
                        int PrniterID = 0;
                        PrniterID = GeneralFunctions.fnInt32(dr["ParamName"].ToString().Substring(15, dr["ParamName"].ToString().Length - 15));
                        string template = "";
                        foreach (DataRow dr1 in dtblTL.Rows)
                        {
                            int PrniterID1 = 0;
                            PrniterID1 = GeneralFunctions.fnInt32(dr1["ParamName"].ToString().Substring(16, dr1["ParamName"].ToString().Length - 16));
                            template = objstup.FetchLinkTemplateName(GeneralFunctions.fnInt32(dr1["LinkTemplate"].ToString()));
                            if (PrniterID1 == PrniterID) break;
                        }

                            dtblLink.Rows.Add(new object[] {   PrniterID.ToString(),
                                                   PrniterID.ToString(),
                                                   dr["LinkPrinter"].ToString(),template });
                    }
                }
            }
            else
            {
                dtblLink = grdPrinter2.ItemsSource as DataTable;
            }

            PosDataObject.Setup objstup1 = new PosDataObject.Setup();
            objstup1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objstup1.DataObjectCulture_None = Settings.DataObjectCulture_None;
            DataTable dtbl1 = new DataTable();
            dtbl1 = objstup1.FetchPrinterData();

            FetchLocalPrinter();
            FetchItemTemplates();
            if (dtblLink.Rows.Count == 0)
            {
                dtblLink = dtbl1;
            }
            else
            {
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    foreach (DataRow dr2 in dtblLink.Rows)
                    {
                        if (dr1["ID"].ToString() != dr2["ID"].ToString()) continue;
                        if (dr1["ID"].ToString() == dr2["ID"].ToString())
                        {
                            dr1["LinkPrinter"] = dr2["LinkPrinter"].ToString();
                            dr1["LinkTemplate"] = dr2["LinkTemplate"].ToString();
                            break;
                        }
                    }
                }
                dtblLink = dtbl1;
            }


            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblLink.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdPrinter2.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtbl1.Dispose();
            dtblTemp.Dispose();
        }

        private void FetchLocalPrinter()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Printer");
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                dtbl.Rows.Add(new object[] { strPrinter });
            }
            dtbl.Rows.Add(new object[] { "(None)" });
            PART_Editor_LinkPrinter.ItemsSource = dtbl;

        }

        private void FetchItemTemplates()
        {
            
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("TemplateName");
            
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl1 = obj.FetchActiveTemplateList("Item");

            foreach(DataRow dr in dtbl1.Rows)
            {
                dtbl.Rows.Add(new object[] { dr["TemplateName"].ToString() });
            }

            PART_Editor_LinkTemplate.ItemsSource = dtbl;
            

        }

        private void BarButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_PrinterNameDlg frm_PrinterNameDlg = new frm_PrinterNameDlg();
            try
            {
                frm_PrinterNameDlg.ID = 0;
                frm_PrinterNameDlg.ShowDialog();
                if (frm_PrinterNameDlg.DialogResult == true)
                {
                    FetchPrinterName();
                    InitialLoadLinkPrinters();
                    boolControlChanged = true;
                }
            }
            finally
            {
                frm_PrinterNameDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem2_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView3.FocusedRowHandle;
            if ((grdPrinter1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_PrinterNameDlg frm_PrinterNameDlg = new frm_PrinterNameDlg();
            try
            {
                frm_PrinterNameDlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPrinter1, colP1));
                if (frm_PrinterNameDlg.ID > 0)
                {
                    frm_PrinterNameDlg.ShowDialog();
                    intNewRecID = frm_PrinterNameDlg.ID;
                    if (frm_PrinterNameDlg.DialogResult == true)
                    {
                        FetchPrinterName();
                        InitialLoadLinkPrinters();
                        boolControlChanged = true;
                    }
                }
            }
            finally
            {
                frm_PrinterNameDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void GridView3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView3.FocusedRowHandle;
            if ((grdPrinter1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_PrinterNameDlg frm_PrinterNameDlg = new frm_PrinterNameDlg();
            try
            {
                frm_PrinterNameDlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPrinter1, colP1));
                if (frm_PrinterNameDlg.ID > 0)
                {
                    frm_PrinterNameDlg.ShowDialog();
                    intNewRecID = frm_PrinterNameDlg.ID;
                    if (frm_PrinterNameDlg.DialogResult == true)
                    {
                        FetchPrinterName();
                        InitialLoadLinkPrinters();
                        boolControlChanged = true;
                    }
                }
            }
            finally
            {
                frm_PrinterNameDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem3_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView3.FocusedRowHandle;
            if ((grdPrinter1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPrinter1, colP1));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Printer"))
                {
                    if (DocMessage.MsgConfirmation("All the terminal settings against this printer will be deleted." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
                    {
                        PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                        objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        int intrError = objCustomer.DeletePrinter(intRecdID);
                        if (intrError != 0)
                        {

                        }
                        FetchPrinterName();
                        InitialLoadLinkPrinters();
                    }
                }
            }
        }

        private void GridView4_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colPL3")
            {
                boolControlChanged = true;
                grdPrinter2.SetCellValue(gridView4.FocusedRowHandle, colPL3, e.Value);
                gridView4.ShowEditor();
                DataTable dtbl = grdPrinter2.ItemsSource as DataTable;
                grdPrinter2.ItemsSource = dtbl;
            }

            if (e.Column.Name == "colPL4")
            {
                boolControlChanged = true;
                grdPrinter2.SetCellValue(gridView4.FocusedRowHandle, colPL4, e.Value);
                gridView4.ShowEditor();
                DataTable dtbl = grdPrinter2.ItemsSource as DataTable;
                grdPrinter2.ItemsSource = dtbl;
            }

        }

        private void BtnPurged_Click(object sender, RoutedEventArgs e)
        {
            if ((SystemVariables.CurrentUserID == 0) || (SecurityPermission.AccessDataPurging))
            {
                if (DocMessage.MsgConfirmation("Are you sure you want to purging data ?") == MessageBoxResult.Yes)
                {
                    PosDataObject.Setup objS = new PosDataObject.Setup();
                    objS.Connection = SystemVariables.Conn;
                    int RID = objS.PurgeData();
                    if (RID == 0)
                    {
                        DocMessage.MsgInformation("Data deleted successfully");
                        if (Settings.ActiveAdminPswdForMercury)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event12, "Successful", "Purging");
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event12, "Successful", "Purging");
                        }
                        Settings.AddInGeneralLog(SystemVariables.CurrentUserID == 0 ? SystemVariables.CurrentUserName : SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(),
                            "Data Purging", "Successful");
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        DocMessage.MsgError("Error while purging data.");
                        if (Settings.ActiveAdminPswdForMercury)
                        {
                            if (SystemVariables.CurrentUserID == 0)
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event12, "Failed", "Purging");
                            else
                                Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event12, "Failed", "Purging");
                        }
                        Settings.AddInGeneralLog(SystemVariables.CurrentUserID == 0 ? SystemVariables.CurrentUserName : SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(),
                            "Data Purging", "Failed");
                    }
                }
            }
            else
            {
                DocMessage.MsgPermission();
                return;
            }
        }

        private void SimpleButton7_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Tender display order will be reset in alphabetical order." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
            {
                PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int intrError = objCustomer.ResetTenderOrdering();
                if (intrError == 0)
                {
                    DocMessage.MsgInformation("Tender display order reset successfully");
                }
                else
                {
                    DocMessage.MsgInformation("Error while trying to reset Tender display order...");
                }
            }
        }

        private void SimpleButton9_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Item display order will be reset in alphabetical order." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
            {
                PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int intrError = objCustomer.ResetItemOrdering();
                if (intrError == 0)
                {
                    DocMessage.MsgInformation("Item display order reset successfully");
                }
                else
                {
                    DocMessage.MsgInformation("Error while trying to reset Item display order...");
                }
            }
        }

        private void SimpleButton8_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Item Category display order will be reset in alphabetical order." + "\n" +"Do you wish to continue?") == MessageBoxResult.Yes)
            {
                PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int intrError = objCustomer.ResetItemCategoryOrdering();
                if (intrError == 0)
                {
                    DocMessage.MsgInformation("Item Category display order reset successfully");
                }
                else
                {
                    DocMessage.MsgInformation("Error while trying to reset Item Category display order...");
                }
            }
        }

        private void SimpleButton5_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Scale Category display order will be reset in alphabetical order." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
            {
                PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int intrError = objCustomer.ResetScaleCategoryOrdering();
                if (intrError == 0)
                {
                    DocMessage.MsgInformation("Scale Category display order reset successfully");
                }
                else
                {
                    DocMessage.MsgInformation("Error while trying to reset Scale Category display order...");
                }
            }
        }

        private void BtnRunScript_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_SQLScript frm_SQLScript = new frm_SQLScript();
            try
            {
                frm_SQLScript.ShowDialog();
            }
            finally
            {
                frm_SQLScript.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void SimpleButton11_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_SQLView frm_SQLScript = new frm_SQLView();
            try
            {
                frm_SQLScript.ShowDialog();
            }
            finally
            {
                frm_SQLScript.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnChangeTerminal_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_ChangeTerminal frm_ChangeTerminal = new frm_ChangeTerminal();
            try
            {
                frm_ChangeTerminal.ShowDialog();
            }
            finally
            {
                frm_ChangeTerminal.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation("Are you sure you want to reset training data ?") == MessageBoxResult.Yes)
            {
                PosDataObject.Setup objS = new PosDataObject.Setup();
                objS.Connection = SystemVariables.Conn;
                int RID = objS.InitializeTran();
                if (RID == 0)
                {
                    DocMessage.MsgInformation("Training data has successfully been reset." + "\n" + SystemVariables.BrandName + " " + "will now close.");
                    Application.Current.Shutdown();
                }
                else
                {
                    DocMessage.MsgError("Error while reset training data.");
                }
            }
        }

        private void BtnTemplate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSmallCurrecyName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSmallCurrecyName.Text.Trim() != "")
            {
                txtCoin1.IsEnabled = txtCoin2.IsEnabled = txtCoin3.IsEnabled = txtCoin4.IsEnabled = txtCoin5.IsEnabled = txtCoin6.IsEnabled = txtCoin7.IsEnabled = true;
                lbEquivalentCoin1.Text = "= 1 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin2.Text = "= 2 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin3.Text = "= 5 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin4.Text = "= 10 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin5.Text = "= 20 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin6.Text = "= 25 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin7.Text = "= 50 " + txtSmallCurrecyName.Text.Trim();
            }
            else
            {
                txtCoin1.IsEnabled = txtCoin2.IsEnabled = txtCoin3.IsEnabled = txtCoin4.IsEnabled = txtCoin5.IsEnabled = txtCoin6.IsEnabled = txtCoin7.IsEnabled = false;
                lbEquivalentCoin1.Text = lbEquivalentCoin2.Text = lbEquivalentCoin3.Text = lbEquivalentCoin4.Text = lbEquivalentCoin5.Text = lbEquivalentCoin6.Text = lbEquivalentCoin7.Text = "";
            }
            CloseKeyboards();
            boolControlChanged = true;
        }

        private void TxtBigCurrecyName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBigCurrecyName.Text.Trim() != "")
            {
                txtCurrency1.IsEnabled = txtCurrency2.IsEnabled = txtCurrency3.IsEnabled = txtCurrency4.IsEnabled = txtCurrency5.IsEnabled = txtCurrency6.IsEnabled = txtCurrency7.IsEnabled = txtCurrency8.IsEnabled = txtCurrency9.IsEnabled = txtCurrency10.IsEnabled = true;
                lbEquivalentCurrency1.Text = "= 1 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency2.Text = "= 2 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency3.Text = "= 5 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency4.Text = "= 10 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency5.Text = "= 20 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency6.Text = "= 50 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency7.Text = "= 100 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency8.Text = "= 200 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency9.Text = "= 500 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency10.Text = "= 1000 " + txtBigCurrecyName.Text.Trim();
            }
            else
            {
                txtCurrency1.IsEnabled = txtCurrency2.IsEnabled = txtCurrency3.IsEnabled = txtCurrency4.IsEnabled = txtCurrency5.IsEnabled = txtCurrency6.IsEnabled = txtCurrency7.IsEnabled = txtCurrency8.IsEnabled = txtCurrency9.IsEnabled = txtCurrency10.IsEnabled = false;

                lbEquivalentCurrency1.Text = lbEquivalentCurrency2.Text = lbEquivalentCurrency3.Text = lbEquivalentCurrency4.Text = lbEquivalentCurrency5.Text = lbEquivalentCurrency6.Text = lbEquivalentCurrency7.Text = lbEquivalentCurrency8.Text = lbEquivalentCurrency9.Text = lbEquivalentCurrency10.Text = "";
            }
            CloseKeyboards();
            boolControlChanged = true;
        }

        private void BtnApplySeetings_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPredefined.Text == "Custom")
            {
                txtICountry.Text = "";
                txtICurrencySymbol.Text = "";
                cmbDateSeparator.SelectedIndex = -1;
                txtIDateFormat.Text = "";


                txtSmallCurrecyName.Text = "";
                txtBigCurrecyName.Text = "";

                txtCoin1.Text = "";
                txtCoin2.Text = "";
                txtCoin3.Text = "";
                txtCoin4.Text = "";
                txtCoin5.Text = "";
                txtCoin6.Text = "";
                txtCoin7.Text = "";


                txtCurrency1.Text = "";
                txtCurrency2.Text = "";
                txtCurrency3.Text = "";
                txtCurrency4.Text = "";
                txtCurrency5.Text = "";
                txtCurrency6.Text = "";
                txtCurrency7.Text = "";
                txtCurrency8.Text = "";
                txtCurrency9.Text = "";
                txtCurrency10.Text = "";

            }

            if (cmbPredefined.Text == "USA")
            {
                txtICountry.Text = "USA";
                txtICurrencySymbol.Text = "$";
                cmbDateSeparator.SelectedIndex = 0;
                txtIDateFormat.Text = "MM/dd/yyyy";


                txtSmallCurrecyName.Text = "cent";
                txtBigCurrecyName.Text = "dollar";

                txtCoin1.Text = "Penny";
                txtCoin2.Text = "";
                txtCoin3.Text = "Nickel";
                txtCoin4.Text = "Dime";
                txtCoin5.Text = "";
                txtCoin6.Text = "Quarter";
                txtCoin7.Text = "Halves";


                txtCurrency1.Text = "One Dollar";
                txtCurrency2.Text = "";
                txtCurrency3.Text = "Five Dollar";
                txtCurrency4.Text = "Ten Dollar";
                txtCurrency5.Text = "Twenty Dollar";
                txtCurrency6.Text = "Fifty Dollar";
                txtCurrency7.Text = "One Hundred Dollar";
                txtCurrency8.Text = "";
                txtCurrency9.Text = "";
                txtCurrency10.Text = "";
            }

            if (cmbPredefined.Text == "UK")
            {
                txtICountry.Text = "UK";
                txtICurrencySymbol.Text = "£";
                cmbDateSeparator.SelectedIndex = 0;
                txtIDateFormat.Text = "dd/MM/yyyy";


                txtSmallCurrecyName.Text = "penny";
                txtBigCurrecyName.Text = "pound";

                txtCoin1.Text = "One penny";
                txtCoin2.Text = "Two pence";
                txtCoin3.Text = "Five pence";
                txtCoin4.Text = "Ten pence";
                txtCoin5.Text = "Twenty pence";
                txtCoin6.Text = "";
                txtCoin7.Text = "Fifty pence";


                txtCurrency1.Text = "£ 1";
                txtCurrency2.Text = "£ 2";
                txtCurrency3.Text = "£ 5";
                txtCurrency4.Text = "£ 10";
                txtCurrency5.Text = "£ 20";
                txtCurrency6.Text = "£ 50";
                txtCurrency7.Text = "";
                txtCurrency8.Text = "";
                txtCurrency9.Text = "";
                txtCurrency10.Text = "";
            }

            if (cmbPredefined.Text == "Europe")
            {
                txtICountry.Text = "Europe";
                txtICurrencySymbol.Text = "€";
                cmbDateSeparator.SelectedIndex = 0;
                txtIDateFormat.Text = "dd/MM/yyyy";

                txtSmallCurrecyName.Text = "cent";
                txtBigCurrecyName.Text = "euro";

                txtCoin1.Text = "1 cent";
                txtCoin2.Text = "2 cent";
                txtCoin3.Text = "5 cent";
                txtCoin4.Text = "10 cent";
                txtCoin5.Text = "20 cent";
                txtCoin6.Text = "";
                txtCoin7.Text = "50 cent";


                txtCurrency1.Text = "€ 1";
                txtCurrency2.Text = "€ 2";
                txtCurrency3.Text = "€ 5";
                txtCurrency4.Text = "€ 10";
                txtCurrency5.Text = "€ 20";
                txtCurrency6.Text = "€ 50";
                txtCurrency7.Text = "€ 100";
                txtCurrency8.Text = "€ 200";
                txtCurrency9.Text = "€ 500";
                txtCurrency10.Text = "";
            }

            if (cmbPredefined.Text == "Canada")
            {
                txtICountry.Text = "Canada";
                txtICurrencySymbol.Text = "$";
                cmbDateSeparator.SelectedIndex = 1;
                txtIDateFormat.Text = "yyyy-MM-dd";

                txtSmallCurrecyName.Text = "cent";
                txtBigCurrecyName.Text = "dollar";

                txtCoin1.Text = "Penny";
                txtCoin2.Text = "";
                txtCoin3.Text = "Nickel";
                txtCoin4.Text = "Dime";
                txtCoin5.Text = "";
                txtCoin6.Text = "Quarter";
                txtCoin7.Text = "";


                txtCurrency1.Text = "Loonie";
                txtCurrency2.Text = "Toonie";
                txtCurrency3.Text = "Five Dollar";
                txtCurrency4.Text = "Ten Dollar";
                txtCurrency5.Text = "Twenty Dollar";
                txtCurrency6.Text = "Fifty Dollar";
                txtCurrency7.Text = "One Hundred Dollar";
                txtCurrency8.Text = "";
                txtCurrency9.Text = "";
                txtCurrency10.Text = "";
            }

            if (txtSmallCurrecyName.Text != "")
            {
                txtCoin1.IsEnabled = txtCoin2.IsEnabled = txtCoin3.IsEnabled = txtCoin4.IsEnabled = txtCoin5.IsEnabled = txtCoin6.IsEnabled = txtCoin7.IsEnabled = true;
                lbEquivalentCoin1.Text = "= 1 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin2.Text = "= 2 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin3.Text = "= 5 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin4.Text = "= 10 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin5.Text = "= 20 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin6.Text = "= 25 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin7.Text = "= 50 " + txtSmallCurrecyName.Text.Trim();
            }
            else
            {
                txtCoin1.IsEnabled = txtCoin2.IsEnabled = txtCoin3.IsEnabled = txtCoin4.IsEnabled = txtCoin5.IsEnabled = txtCoin6.IsEnabled = txtCoin7.IsEnabled = false;
                lbEquivalentCoin1.Text = "";
                lbEquivalentCoin2.Text = "";
                lbEquivalentCoin3.Text = "";
                lbEquivalentCoin4.Text = "";
                lbEquivalentCoin5.Text = "";
                lbEquivalentCoin6.Text = "";
                lbEquivalentCoin7.Text = "";
            }
            if (txtBigCurrecyName.Text != "")
            {
                txtCurrency1.IsEnabled = txtCurrency2.IsEnabled = txtCurrency3.IsEnabled = txtCurrency4.IsEnabled = txtCurrency5.IsEnabled = txtCurrency6.IsEnabled = txtCurrency7.IsEnabled = txtCurrency8.IsEnabled = txtCurrency9.IsEnabled = txtCurrency10.IsEnabled = true;
                lbEquivalentCurrency1.Text = "= 1 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency2.Text = "= 2 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency3.Text = "= 5 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency4.Text = "= 10 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency5.Text = "= 20 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency6.Text = "= 50 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency7.Text = "= 100 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency8.Text = "= 200 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency9.Text = "= 500 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency10.Text = "= 1000 " + txtBigCurrecyName.Text.Trim();
            }
            else
            {
                txtCurrency1.IsEnabled = txtCurrency2.IsEnabled = txtCurrency3.IsEnabled = txtCurrency4.IsEnabled = txtCurrency5.IsEnabled = txtCurrency6.IsEnabled = txtCurrency7.IsEnabled = txtCurrency8.IsEnabled = txtCurrency9.IsEnabled = txtCurrency10.IsEnabled = false;
                lbEquivalentCurrency1.Text = "";
                lbEquivalentCurrency2.Text = "";
                lbEquivalentCurrency3.Text = "";
                lbEquivalentCurrency4.Text = "";
                lbEquivalentCurrency5.Text = "";
                lbEquivalentCurrency6.Text = "";
                lbEquivalentCurrency7.Text = "";
                lbEquivalentCurrency8.Text = "";
                lbEquivalentCurrency9.Text = "";
                lbEquivalentCurrency10.Text = "";
            }

            boolControlChanged = true;
        }

        private void CmbDateSeparator_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            int prevIndx = txtIDateFormat.SelectedIndex;
            if (cmbDateSeparator.SelectedIndex == 0)
            {
                txtIDateFormat.Items.Clear();
                txtIDateFormat.Items.Add("MM/dd/yyyy");
                txtIDateFormat.Items.Add("dd/MM/yyyy");
                txtIDateFormat.Items.Add("yyyy/MM/dd");
                txtIDateFormat.Items.Add("yyyy/dd/MM");
            }
            if (cmbDateSeparator.SelectedIndex == 1)
            {
                txtIDateFormat.Items.Clear();
                txtIDateFormat.Items.Add("MM-dd-yyyy");
                txtIDateFormat.Items.Add("dd-MM-yyyy");
                txtIDateFormat.Items.Add("yyyy-MM-dd");
                txtIDateFormat.Items.Add("yyyy-dd-MM");
            }
            if (cmbDateSeparator.SelectedIndex == 2)
            {
                txtIDateFormat.Items.Clear();
                txtIDateFormat.Items.Add("MM.dd.yyyy");
                txtIDateFormat.Items.Add("dd.MM.yyyy");
                txtIDateFormat.Items.Add("yyyy.MM.dd");
                txtIDateFormat.Items.Add("yyyy.dd.MM");
            }
            if (cmbDateSeparator.SelectedIndex == -1)
            {
                txtIDateFormat.Items.Clear();
            }

            txtIDateFormat.SelectedIndex = prevIndx;

            boolControlChanged = true;
        }

        private void TxtCurrency1_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency1.Text.Trim() != "")
            {
                chkQuickTender1.IsEnabled = true;
            }
            else
            {
                chkQuickTender1.IsEnabled = false;
                chkQuickTender1.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency2_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency2.Text.Trim() != "")
            {
                chkQuickTender2.IsEnabled = true;
            }
            else
            {
                chkQuickTender2.IsEnabled = false;
                chkQuickTender2.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency3_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency3.Text.Trim() != "")
            {
                chkQuickTender3.IsEnabled = true;
            }
            else
            {
                chkQuickTender3.IsEnabled = false;
                chkQuickTender3.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency4_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency4.Text.Trim() != "")
            {
                chkQuickTender4.IsEnabled = true;
            }
            else
            {
                chkQuickTender4.IsEnabled = false;
                chkQuickTender4.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency5_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency5.Text.Trim() != "")
            {
                chkQuickTender5.IsEnabled = true;
            }
            else
            {
                chkQuickTender5.IsEnabled = false;
                chkQuickTender5.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency6_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency6.Text.Trim() != "")
            {
                chkQuickTender6.IsEnabled = true;
            }
            else
            {
                chkQuickTender6.IsEnabled = false;
                chkQuickTender6.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency7_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency7.Text.Trim() != "")
            {
                chkQuickTender7.IsEnabled = true;
            }
            else
            {
                chkQuickTender7.IsEnabled = false;
                chkQuickTender7.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency8_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency8.Text.Trim() != "")
            {
                chkQuickTender8.IsEnabled = true;
            }
            else
            {
                chkQuickTender8.IsEnabled = false;
                chkQuickTender8.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency9_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency9.Text.Trim() != "")
            {
                chkQuickTender9.IsEnabled = true;
            }
            else
            {
                chkQuickTender9.IsEnabled = false;
                chkQuickTender9.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void TxtCurrency10_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtCurrency10.Text.Trim() != "")
            {
                chkQuickTender10.IsEnabled = true;
            }
            else
            {
                chkQuickTender10.IsEnabled = false;
                chkQuickTender10.IsChecked = false;
            }
            boolControlChanged = true;
        }

        private void BtnReportSchd_Click(object sender, RoutedEventArgs e)
        {
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }

            /*
            ScheduledTasks st = new ScheduledTasks();
            st.DeleteTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
            try
            {
                ScheduledTasks st1 = new ScheduledTasks();

                TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));

                t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                t.ApplicationName = csConnPath;
                t.Parameters = "  R";
                t.SetAccountInformation(Environment.UserName, (String)null);
                DailyTrigger dt = new DailyTrigger((short)txtReportTime.DateTime.Hour, (short)txtReportTime.DateTime.Minute, 1);
                t.Triggers.Add(dt);

                t.Save();
                t.Close();
                DocMessage.MsgInformation("Scheduler successfully set");
                btnReportSchd.Content = "Modify Scheduler";
                btnReportSchd.Tag = "Modify Scheduler";
                btnRunReportSchd.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                DocMessage.MsgInformation("Error while scheduling ...");
            }
            */
        }

        private void BtnRunReportSchd_Click(object sender, RoutedEventArgs e)
        {
           /* Cursor = Cursors.Wait;
            try
            {
                ScheduledTasks st1 = new ScheduledTasks();
                TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
                if (t1 != null)
                {
                    Dispatcher.BeginInvoke(new Action(() => t1.Run()));
                }
            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }*/

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(100);
            string schdName = SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName);
            try
            {
                Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();
                bool boolFindTask = false;
                boolFindTask = ts.GetTask(@"" + schdName) != null;
                if (boolFindTask) ts.GetTask(@"" + schdName).Run();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {
            string reportpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            reportpath = directory.Parent.FullName;

            if (reportpath.EndsWith("\\")) reportpath = reportpath + SystemVariables.BrandName + "\\Report\\";
            else reportpath = reportpath + "\\" + SystemVariables.BrandName + "\\Report\\";
            if (Directory.Exists(reportpath))
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add("ReportName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportPath", System.Type.GetType("System.String"));

                foreach (string f in Directory.GetFiles(reportpath))
                {
                    FileInfo fi = new FileInfo(f);
                    string tempf = fi.Name.Replace(fi.Extension, "");
                    string rpt = "";
                    if (tempf == "Dashboard_Sales") rpt = "Dashboard-Sales";
                    if (tempf == "Sales_Summary") rpt = "Sales Summary";
                    if (tempf == "Sales_SKU") rpt = "Sales By SKU";
                    if (tempf == "Sales_Department") rpt = "Sales By Department";
                    if (tempf == "Sales_Category") rpt = "Sales By POS Screen Category";
                    if (tempf == "Sales_Family") rpt = "Sales By Family";
                    if (tempf == "Sales_Employee") rpt = "Sales By Employee";
                    if (tempf == "Sales_Vendor") rpt = "Sales By Vendor";
                    if (tempf == "Sales_Day_Of_Week") rpt = "Sales By Day of Week";
                    if (tempf == "Sales_Month") rpt = "Monthly Sales";
                    if (tempf == "Sales_Customer_SKU") rpt = "Customer Sales (By SKU)";
                    if (tempf == "Sales_Customer_Date") rpt = "Customer Sales (By Date)";
                    if (tempf == "Sales_Customer_Department") rpt = "Customer Sales (By Department)";
                    if (tempf == "Sales_Customer_Summary") rpt = "Customer Sales Summary";

                    dtbl.Rows.Add(new object[] { rpt, fi.FullName });
                }

                /*if (dtbl.Rows.Count > 0)
                {
                    frmViewFile frmf = new frmViewFile();
                    try
                    {
                        frmf.Files = dtbl;
                        frmf.ShowDialog();
                    }
                    finally
                    {
                        frmf.Dispose();
                    }
                }*/
                dtbl.Dispose();
            }
        }

        private void BtnImportPrecidiaGC_Click(object sender, RoutedEventArgs e)
        {
            bool f = false;
            string fle = "";
            string sep = "";
            bool fHeader = false;
            blurGrid.Visibility = Visibility.Visible;
            frm_SelectImportFile fselect = new frm_SelectImportFile();
            try
            {
                fselect.ShowDialog();
                if (fselect.DialogResult == true)
                {
                    fle = fselect.ImportFile;
                    sep = fselect.ImportFileSeparator;
                    fHeader = fselect.ImportFileColumnHeader;
                    f = true;
                }
            }
            finally
            {
                fselect.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (f)
            {
                string[] sp = new string[] { sep };
                StreamReader sr = new StreamReader(fle);
                DataTable dt = new DataTable();
                DataRow row;
                string[] value = null;
                string line = "";

                if (fHeader)
                {
                    line = sr.ReadLine();
                    value = line.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                }
                dt.Columns.Add(new DataColumn("C1"));
                dt.Columns.Add(new DataColumn("C2"));

                while (!sr.EndOfStream)
                {
                    value = sr.ReadLine().Split(sp, StringSplitOptions.RemoveEmptyEntries);
                    if (value.Length == dt.Columns.Count)
                    {
                        row = dt.NewRow();
                        row.ItemArray = value;
                        dt.Rows.Add(row);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    Cursor = Cursors.Wait;
                    int newcount = 0;
                    try
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string pGC = dr["C1"].ToString().Trim('\'');
                            double pAmt = GeneralFunctions.fnDouble(dr["C2"].ToString());
                            if (pAmt == 0) continue;
                            PosDataObject.Setup os1 = new PosDataObject.Setup();
                            os1.Connection = SystemVariables.Conn;
                            bool extGC = os1.IsExistsImportedPrecidiaGiftCard(pGC);
                            if (extGC)
                            {
                                PosDataObject.Setup os2 = new PosDataObject.Setup();
                                os2.Connection = SystemVariables.Conn;
                                int cnt = os2.GetImportedPrecidiaGiftCardRecordCount(pGC);
                                if (cnt == 1)
                                {
                                    os2.LoginUserID = SystemVariables.CurrentUserID;
                                    os2.UpdateImportedPrecidiaGiftCard(pGC, pAmt);
                                }
                            }
                            else
                            {
                                PosDataObject.Setup os3 = new PosDataObject.Setup();
                                os3.Connection = SystemVariables.Conn;
                                os3.LoginUserID = SystemVariables.CurrentUserID;
                                os3.InsertImportedPrecidiaGiftCard(pGC, pAmt, Settings.StoreCode, Settings.StoreCode);
                                newcount++;
                            }
                        }
                        if (newcount == 0)
                        {
                            DocMessage.MsgInformation("Precidia gift cards imported succussfully.");
                        }
                        else
                        {
                            DocMessage.MsgInformation("Precidia gift cards imported succussfully." + "\n" + "" + newcount.ToString() + " " + "new gift certificate" + (newcount == 1 ? "" : "s") + " " + "created.");
                        }
                    }
                    catch
                    {
                        DocMessage.MsgInformation("Error while importing...");
                    }

                }
                else
                {
                    DocMessage.MsgInformation("No record found for import");
                }
               Cursor = Cursors.Arrow;
            }
        }

        private void ChkgcRepeat5_Checked(object sender, RoutedEventArgs e)
        {
            if (chkgcRepeat5.IsChecked == true)
            {
                rep41.Visibility = Visibility.Visible;
                rep42.Visibility = Visibility.Visible;
                rep43.Visibility = Visibility.Visible;
                rep43.SelectedIndex = 1;
                rep44.Visibility = Visibility.Visible;
                rep45.Visibility = Visibility.Visible;
                rep45.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, txtTimeO.DateTime.Hour,
                    txtTimeO.DateTime.Minute, 0).AddHours(23);
            }
            else
            {
                rep41.Visibility = Visibility.Hidden;
                rep42.Visibility = Visibility.Hidden;
                rep43.Visibility = Visibility.Hidden;
                rep44.Visibility = Visibility.Hidden;
                rep45.Visibility = Visibility.Hidden;
            }
        }

        private bool ValidAll()
        {
            /*if (txtShopifyAPI.Text.Trim() == "")
            {
                DocMessage.MsgEnter("API Key");
                GeneralFunctions.SetFocus(txtShopifyAPI);
                return false;
            }*/

            if (txtShopifyPswd.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Access Token");
                GeneralFunctions.SetFocus(txtShopifyPswd);
                return false;
            }

            if (txtShopifyStoreAddress.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Store Address");
                GeneralFunctions.SetFocus(txtShopifyStoreAddress);
                return false;
            }

            return true;
        }

        private bool AddShopifySetupBeforeSetOrRunSched()
        {
            bool blProceed = false;
            if (ValidAll())
            {
                PosDataObject.Setup objsetup = new PosDataObject.Setup();
                objsetup.Connection = SystemVariables.Conn;

                objsetup.ShopifyAPIKey = ""; // txtShopifyAPI.Text.Trim();
                objsetup.ShopifyPassword = txtShopifyPswd.Text.Trim();
                objsetup.ShopifyStoreAddress = txtShopifyStoreAddress.Text.Trim();
                if (chkShopifyPremium.IsChecked == true)
                {
                    objsetup.PremiumPlan = "Y";
                }
                else
                {
                    objsetup.PremiumPlan = "N";
                }
                string error = objsetup.PostShopifySetupData();

                if (error == "")
                {
                    Settings.LoadShopifySettings();
                    blProceed = true;
                }
                else
                {
                }
            }

            return blProceed;
        }

        private void BtnAutoSchedulrO_Click(object sender, RoutedEventArgs e)
        {
            if (!AddShopifySetupBeforeSetOrRunSched()) return;
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Shopify Integration".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Shopify Integration".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  Shopify";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtTimeO.DateTime.Hour, (short)txtTimeO.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    if (chkgcRepeat5.IsChecked == true)
                    {
                        SetScheduleEndTime(txtTimeO, rep45);
                        int days = 0;
                        if (rep45.DateTime.Date > txtTimeO.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep45.DateTime.Hour, rep45.DateTime.Minute, rep45.DateTime.Second);
                        TimeSpan sts = new TimeSpan(txtTimeO.DateTime.Hour, txtTimeO.DateTime.Minute, txtTimeO.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);
                        dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                        if (rep43.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep42.Value.ToString());
                        else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep42.Value.ToString()) * 60;
                    }


                    t.Save();
                    t.Close();
                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrO.Content = "Modify Scheduler";
                    btnAutoSchedulrO.Tag = "Modify Scheduler";


                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Shopify Integration".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtTimeO.DateTime.Hour) + TimeSpan.FromMinutes((short)txtTimeO.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkgcRepeat5.IsChecked == true)
                    {
                        SetScheduleEndTime(txtTimeO, rep45);
                        int days = 0;
                        if (rep45.DateTime.Date > txtTimeO.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep45.DateTime.Hour, rep45.DateTime.Minute, rep45.DateTime.Second);
                        TimeSpan sts = new TimeSpan(txtTimeO.DateTime.Hour, txtTimeO.DateTime.Minute, txtTimeO.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep43.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep42.Value.ToString()) : GeneralFunctions.fnInt32(rep42.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  Shopify", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrO.Content = "Modify Scheduler";
                    btnAutoSchedulrO.Tag = "Modify Scheduler";
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }
        }

        private void BtnRunSchedulerO_Click(object sender, RoutedEventArgs e)
        {
            if (!AddShopifySetupBeforeSetOrRunSched()) return;

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "AutoExe_Retail.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\AutoExe_Retail.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"ShopifyManual";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }



        public void PopulateGL(DevExpress.Xpf.Editors.ComboBoxEdit cmbGL)
        {
            PosDataObject.GLedger objEmployee = new PosDataObject.GLedger();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objEmployee.LookupGL("");
            cmbGL.ItemsSource = dbtbl;
            cmbGL.DisplayMember = "Lookup";
            cmbGL.ValueMember = "ID";
            dbtbl.Dispose();
            cmbGL.EditValue = null;
        }

        private void ArrangePoleDisplayText()
        {
            if (Settings.PoleScreen != null)
            {
                if (Settings.PoleScreen != "")
                {
                    if (Settings.PoleScreen.Length <= 20)
                    {
                        txtPole1.Text = Settings.PoleScreen.Trim();
                        txtPole2.Text = "";
                    }
                    else
                    {
                        txtPole1.Text = Settings.PoleScreen.Substring(0, 20).Trim();
                        txtPole2.Text = Settings.PoleScreen.Substring(20, 20).Trim();
                    }
                }
            }
        }

        public void ShowData()
        {
            numAlert.Text = Settings.ItemExpiryAlertDay.ToString();
            memEmail.Text = Settings.AppointmentEmailBody;

            cmbSMTPType.SelectedIndex = Settings.SMTPServer;

            cmbstyle.Text = SystemVariables.SelectedTheme;

            // Backup
            cmbBkupType.SelectedIndex = Settings.BackupType;
            cmbBkupStorageType.SelectedIndex = Settings.BackupStorageType;

            //Shopify
            txtShopifyAPI.Text = ""; //Settings.ShopifyAPIKey;
            txtShopifyPswd.Text = Settings.ShopifyPassword;
            txtShopifyStoreAddress.Text = Settings.ShopifyStoreAddress;
            chkShopifyPremium.IsChecked = Settings.PremiumPlan == "Y";
            

            txtQuickBookCompanyPath.Text = Settings.QuickBooksWindowsCompanyFilePath;
            txtQuickBookIncomeAc.Text = Settings.QuickBooksWindowsIncomeAccount;
            txtQuickBookCOGSAc.Text = Settings.QuickBooksWindowsCOGSAccount;

            txtWooConsumerKey.Text = Settings.WCommConsumerKey;
            txtWooConsumerSecret.Text = Settings.WCommConsumerSecret;
            txtWooStoreAddress.Text = Settings.WCommStoreAddress;

            txtXeroCompany.Text = Settings.XeroCompanyName;
            txtXeroClientID.Text = Settings.XeroClientId;
            txtXeroCallbackUri.Text = Settings.XeroCallbackUrl;
            txtXeroAccessToken.Text = Settings.XeroAccessToken;
            txtXeroRefreshToken.Text = Settings.XeroRefreshToken;
            xerotenantId = Settings.XeroTenantId;

            txtXeroAccount1.Text = Settings.XeroInventoryAssetAccountCode;
            txtXeroAccount2.Text = Settings.XeroInventoryAdjustmentAccountCode;
            txtXeroAccount3.Text = Settings.XeroAccountCodePurchase;
            txtXeroAccount4.Text = Settings.XeroAccountCodeSale;
            txtXeroAccount5.Text = Settings.XeroCOGSAccountCodePurchase;
            txtXeroAccount6.Text = Settings.XeroCOGSAccountCodeSale;

            // office info
            txtCompany.Text = Settings.RegCompanyName;
            txtAdd1.Text = Settings.RegAddress1;
            txtAdd2.Text = Settings.RegAddress2;
            txtCity.Text = Settings.RegCity;
            txtState.Text = Settings.RegState;
            cmbZip.Text = Settings.RegZip;
            txtPhone.Text = Settings.Phone;
            txtFax.Text = Settings.Fax;
            txtEmail.Text = Settings.RegEMail;
            txtWebAddress.Text = Settings.WebAddress;
            txtMsg.Text = Settings.StoreInfo;
            // general
            if (Settings.CustomerInfo == 1)
            {
                rgCustInfo_0.IsChecked = true;
            }
            if (Settings.CustomerInfo == 2)
            {
                rgCustInfo_1.IsChecked = true;
            }
            if (Settings.POSPrintInvoice == 0) rgPrintInvoice_0.IsChecked = true;
            if (Settings.POSPrintInvoice == 1) rgPrintInvoice_1.IsChecked = true;
            if (Settings.POSPrintInvoice == 2) rgPrintInvoice_2.IsChecked = true;

            if (Settings.VendorUpdate == 0) rgVendUpdt_0.IsChecked = true;
            if (Settings.VendorUpdate == 1) rgVendUpdt_1.IsChecked = true;
            if (Settings.VendorUpdate == 2) rgVendUpdt_2.IsChecked = true;

            if (Settings.CloseoutOption == 0) rgCloseout_0.IsChecked = true;
            if (Settings.CloseoutOption == 1) rgCloseout_1.IsChecked = true;
            if (Settings.CloseoutOption == 2) rgCloseout_2.IsChecked = true;

            if (Settings.SignINOption == 0) rgSignin_0.IsChecked = true;
            if (Settings.SignINOption == 1) rgSignin_1.IsChecked = true;
            if (Settings.SignINOption == 2) rgSignin_2.IsChecked = true;

            if (Settings.Use4Decimal == "Y") chk4digit.IsChecked = true;
            else chk4digit.IsChecked = false;


            if (Settings.PrintBlindDropCloseout == "Y") chkPrintBlindDrop.IsChecked = true;
            else chkPrintBlindDrop.IsChecked = false;


            if (Settings.PrintDuplicateGiftCertSaleReceipt == "Y") chkPOS745.IsChecked = true;
            else chkPOS745.IsChecked = false;

            if (Settings.OtherTerminalCloseout == "Y") chkCloseout5.IsChecked = true;
            else chkCloseout5.IsChecked = false;

            if (Settings.SalesByDept == "Y") chkCloseout1.IsChecked = true;
            else chkCloseout1.IsChecked = false;
            if (Settings.BlindCountPreview == "Y") chkCloseout2.IsChecked = true;
            else chkCloseout2.IsChecked = false;
            if (Settings.SalesByHour == "Y") chkCloseout3.IsChecked = true;
            else chkCloseout3.IsChecked = false;
            if (Settings.CloseoutExport == "Y") chkCloseout4.IsChecked = true;
            else chkCloseout4.IsChecked = false;
            if (Settings.POSAcceptCheck == "Y") chkPOS1.IsChecked = true;
            else chkPOS1.IsChecked = false;
            if (Settings.POSDisplayChangeDue == "Y") chkPOS2.IsChecked = true;
            else chkPOS2.IsChecked = false;
            if (Settings.POSIDRequired == "Y") chkPOS3.IsChecked = true;
            else chkPOS3.IsChecked = false;
            /*if (Settings.POSDisplayProductImage == "Y") chkPOS4.IsChecked = true;
            else chkPOS4.IsChecked = false;
            if (Settings.POSRedirectToChangeDue == "Y") chkPOS5.IsChecked = true;
            else chkPOS5.IsChecked = false;*/
            if (Settings.POSShowGiftCertBalance == "Y") chkPOS6.IsChecked = true;
            else chkPOS6.IsChecked = false;
            if (Settings.POSCardPayment == "Y") chkPOS7.IsChecked = true;
            else chkPOS7.IsChecked = false;

            if (Settings.AutoDisplayItemImage == "Y") chkPOS87.IsChecked = true;
            else chkPOS87.IsChecked = false;

            if (Settings.IsDuplicateInvoice == "Y") chkPOS8.IsChecked = true;
            else chkPOS8.IsChecked = false;

            /*if (Settings.ReceiptPrintOnReturn == "Y") chkPOS10.IsChecked = true;
            else chkPOS10.IsChecked = false;*/

            if (Settings.CloseCategoryAfterRinging == "Y") chkPOS90.IsChecked = true;
            else chkPOS90.IsChecked = false;

            if (Settings.ShowFoodStampTotal == "Y") chkPOS39.IsChecked = true;
            else chkPOS39.IsChecked = false;

            if (Settings.AddGallon == "Y") chkPOS110.IsChecked = true;
            else chkPOS110.IsChecked = false;

            if (Settings.Print2TicketsForRepair == "Y") chkPOS150.IsChecked = true;
            else chkPOS150.IsChecked = false;

            chkPOS33.IsChecked = (Settings.AcceptTips == "Y");
            chkPOS34.IsChecked = (Settings.ShowTipsInReceipt == "Y");

            chkUseCustNameInLabelPrint.IsChecked = (Settings.UseCustomerNameInLabelPrint == "Y");

            chkPOS250.IsChecked = (Settings.NoSaleReceipt == "Y");
            chkPOS251.IsChecked = (Settings.HouseAccountBalanceInReceipt == "Y");

            PrevProductImgDisplay = Settings.POSDisplayProductImage;

            /*if (Settings.UseInvJrnl == "Y") chkGeneral1.IsChecked = true;
            else chkGeneral1.IsChecked = false;
            if (Settings.NoPriceOnLabelDefault == "Y") chkGeneral2.IsChecked = true;
            else chkGeneral2.IsChecked = false;
            if (Settings.QuantityRequired == "Y") chkGeneral3.IsChecked = true;
            else chkGeneral3.IsChecked = false;*/

            if (Settings.ForcedLogin == "Y") chkForcedlogin.IsChecked = true;
            else chkForcedlogin.IsChecked = false;

            if (Settings.GeneralReceiptPrint == "Y") chkA4.IsChecked = true;
            else chkA4.IsChecked = false;

            if (Settings.PreprintedReceipt == "Y") chkPrePrint.IsChecked = true;
            else chkPrePrint.IsChecked = false;

            if (Settings.AutoPO == "Y") chkAutoPO.IsChecked = true;
            else chkAutoPO.IsChecked = false;

            if (Settings.AutoCustomer == "Y") chkAutoCustomer.IsChecked = true;
            else chkAutoCustomer.IsChecked = false;

            if (Settings.AutoGC == "Y") chkAutoGC.IsChecked = true;
            else chkAutoGC.IsChecked = false;

            if (Settings.SingleGC == "Y") chkSingleGC.IsChecked = true;
            else chkSingleGC.IsChecked = false;


            if (Settings.BarTenderLabelPrint == "Y") chkBartender.IsChecked = true;
            else chkBartender.IsChecked = false;

            txtLoginTime.Text = Settings.AutoSignoutTime.ToString();

            if (Settings.AutoSignout == "Y") chkAutoSignout.IsChecked = true;
            else chkAutoSignout.IsChecked = false;

            if (Settings.AutoSignoutTender == "Y") chkAutoSignoutAfterTender.IsChecked = true;
            else chkAutoSignoutAfterTender.IsChecked = false;


            if (Settings.DefaultCloseoutPrinter == "Receipt Printer") chkDefCloseoutPrinter.IsChecked = true;
            else chkDefCloseoutPrinter.IsChecked = false;

            /*if (Settings.EasyTendering == "Y") chkPOS81.IsChecked = true; else chkPOS81.IsChecked = false;*/
            if (Settings.ShowFeesInReceipt == "Y") chkPOS82.IsChecked = true; else chkPOS82.IsChecked = false;

            if (Settings.ShowSaleSaveInReceipt == "Y") chkPOS69.IsChecked = true;
            else chkPOS69.IsChecked = false;

            if (Settings.DisplayLaneClosed == "Y") chkPOS120.IsChecked = true; else chkPOS120.IsChecked = false;
            if (Settings.CalculatorStyleKeyboard == "Y") chkPOS225.IsChecked = true; else chkPOS225.IsChecked = false;

            blPrevPO = chkAutoPO.IsChecked == true ? true : false;

            blPrevCustomer = chkAutoCustomer.IsChecked == true ? true : false;

            blPrevGC = chkAutoGC.IsChecked == true ? true : false;

            cmbAlign.SelectedIndex = Settings.POSMoreFunctionAlignment;

            //spnInvMonth.Value = Settings.MonthsInvJrnl;
            numGC.Text = Settings.GiftCertMaxChange.ToString("f2");

            cmbReportPrinter.SelectedIndex = cmbReportPrinter.Items.IndexOf(Settings.ReportPrinterName);
            cmbReceiptPrinter.SelectedIndex = cmbReceiptPrinter.Items.IndexOf(Settings.ReceiptPrinterName);

            if (GeneralFunctions.fnInt32(Settings.LabelPrinterType) - 1 == 0) rgLabelPrinter_0.IsChecked = true;
            else rgLabelPrinter_1.IsChecked = true;

            cmbLabelPrinter.Visibility = rgLabelPrinter_0.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            cmbCOMPrinter.Visibility = rgLabelPrinter_1.IsChecked == true ? Visibility.Visible : Visibility.Hidden;

            if (rgLabelPrinter_0.IsChecked == true)
                cmbLabelPrinter.EditValue = Settings.LabelPrinterName;
            if (rgLabelPrinter_1.IsChecked == true)
                cmbCOMPrinter.EditValue = Settings.LabelPrinterName;

            //chkBartender.Visibility = rgLabelPrinter_0.IsChecked == true ? Visibility.Visible : Visibility.Hidden;

            if (Settings.LineDisplayDeviceType == "OPOS") rgPDType1.IsChecked = true;
            if (Settings.LineDisplayDeviceType == "SERIAL") rgPDType2.IsChecked = true;

            cmbLineDisplay.EditValue = Settings.LineDisplayDevice;
            cmbLineDisplaySP.EditValue = Settings.LineDisplayDeviceSerial;

            txtDrawerCode.Text = Settings.CashDrawerCode;


            numLDue.Text = Settings.LayawaysDue.ToString();
            numLPrint.Text = Settings.LayawayReceipts.ToString();
            numLDepPerc.Text = Settings.LayawayDepositPercent.ToString("f2");

            memLP.Text = Settings.ReceiptLayawayPolicy;
            memReceiptH.Text = Settings.ReceiptHeader;
            memReceiptF.Text = Settings.ReceiptFooter;

            ArrangePoleDisplayText();

            if (Settings.PaymentGateway == 1) rgPaymentGateway_0.IsChecked = true;
            if (Settings.PaymentGateway == 2) rgPaymentGateway_1.IsChecked = true;
            if (Settings.PaymentGateway == 3) rgPaymentGateway_2.IsChecked = true;
            if (Settings.PaymentGateway == 4) rgPaymentGateway_3.IsChecked = true;
            if (Settings.PaymentGateway == 5) rgPaymentGateway_4.IsChecked = true;
            if (Settings.PaymentGateway == 6) rgPaymentGateway_5.IsChecked = true;
            if (Settings.PaymentGateway == 7) rgPaymentGateway_6.IsChecked = true;

            if (Settings.PaymentGateway == 8) rgPaymentGateway_7.IsChecked = true;
            if (Settings.PaymentGateway == 9) rgPaymentGateway_8.IsChecked = true;

            pay8_XEConnectPath.Text = Settings.EvoConnectFileLocation;
            pay8_Api.Text = Settings.EvoApi;

            pay9_psaccountname.Text = Settings.Paymentsense_AccountName;
            pay9_psapikey.Text = Settings.Paymentsense_ApiKey;
            pay9_pssoftwareid.Text = Settings.Paymentsense_SoftwareHouseId;
            pay9_psinstallerid.Text = Settings.Paymentsense_InstallerId;

            pay9_psterminal.Text = Settings.Paymentsense_Terminal;


            pay3_clientMac.Text = Settings.PrecidiaClientMAC;
            pay3_posLynx.Text = Settings.PrecidiaPOSLynxMAC;
            pay3_portnumber.Text = Settings.PrecidiaPort == 0 ? "" : Settings.PrecidiaPort.ToString();

            if (Settings.PrecidiaUsePINPad == "N") pay3_usepinpad.IsChecked = false; else pay3_usepinpad.IsChecked = true;

            //if (Settings.PrecidiaLaneOpen == "N") chkPrecidiaOpenTerminal.IsChecked = false; else chkPrecidiaOpenTerminal.IsChecked = true;
            //if (Settings.PrecidiaRRLog == "N") chkPrecidiaRRLog.IsChecked = false; else chkPrecidiaRRLog.IsChecked = true;
            if (Settings.PrecidiaSign == "N") chkPrecidiaSign.IsChecked = false; else chkPrecidiaSign.IsChecked = true;

            //txtPrecidiaSignatureAmout.Value = Settings.PrecidiaSignAmount;

            pay5_server.Text = Settings.DatacapServer;
            pay5_mid.Text = Settings.DatacapMID;
            pay5_securedevice.Text = Settings.DatacapSecureDeviceID;
            pay5_comport.Text = Settings.DatacapCOMPort;
            pay5_pinpadtype.Text = Settings.DatacapPINPad;
            pay5_signatureAmt.Text = Settings.DatacapSignAmount.ToString("f2");

            if (Settings.DatacapCardEntryMode == 0) rgCardEntryMode_0.IsChecked = true;
            if (Settings.DatacapCardEntryMode == 1) rgCardEntryMode_1.IsChecked = true;

            pay5_signaturedevice.Text = Settings.DatacapSignatureDevice;
            pay5_signaturecomport.Text = Settings.DatacapSignatureDeviceCOMPort;


            pay6_serverIp.Text = Settings.DatacapEMVServerIP;
            pay6_serverport.Text = Settings.DatacapEMVServerPort;
            pay6_merchantId.Text = Settings.DatacapEMVMID;
            pay6_securitydevice.Text = Settings.DatacapEMVSecurityDevice;
            pay6_comport.Text = Settings.DatacapEMVCOMPort;
            pay6_usertrace.Text = Settings.DatacapEMVUserTrace;
            pay6_terminalId.Text = Settings.DatacapEMVTerminalID;
            pay6_operatorId.Text = Settings.DatacapEMVOperatorID;
            pay6_manualEntry.IsChecked = Settings.DatacapEMVManual == "Y";
            pay6_tokenReq.IsChecked = Settings.DatacapEMVToken == "Y";



            pay7_commtype.Text = Settings.POSLinkCommType;
            pay7_baud.Text = Settings.POSLinkBaudRate;
            pay7_destIp.Text = Settings.POSLinkDestIP;
            pay7_destport.Text = Settings.POSLinkDestPort;
            pay7_serialport.Text = Settings.POSLinkSerialPort;
            pay7_timeout.Text = Settings.POSLinkTimeout;

            pay2_merchantId.Text = Settings.MercuryHPMerchantID;
            pay2_userId.Text = Settings.MercuryHPUserID;

            pay2_pinpadport.SelectedIndex = pay2_pinpadport.Items.IndexOf("COM" + Settings.MercuryHPPort);
            pay2_signatureAmt.Text = Settings.MercurySignAmount.ToString("f2");

            if (Settings.ElementHPMode == 0) rgPaymentMode_0.IsChecked = true;
            else rgPaymentMode_1.IsChecked = true;

            pay1_accountId.Text = Settings.ElementHPAccountID;
            //pay.Text = Settings.ElementHPAccountToken;
            pay1_applicationId.Text = Settings.ElementHPApplicationID;
            pay1_acceptorId.Text = Settings.ElementHPAcceptorID;
            pay1_terminal.Text = Settings.ElementHPTerminalID;
            if (Settings.ElementZipProcessing == "N") pay1_check.IsChecked = false; else pay1_check.IsChecked = true;

            if (Settings.POSPrintTender == 0) rgPrintTender_0.IsChecked = true;
            if (Settings.POSPrintTender == 1) rgPrintTender_1.IsChecked = true;
            if (Settings.POSPrintTender == 2) rgPrintTender_2.IsChecked = true;


            if (Settings.DisplayLogoOrWeb == 0) rglogo_0.IsChecked = true;
            else rglogo_1.IsChecked = true;

            if (rglogo_0.IsChecked == true) tabLogo.SelectedIndex = 0;
            else
            {
                tabLogo.SelectedIndex = 1;
                txtFixedWeb.Text = Settings.DisplayURL;
                if (Settings.BrowsePermission == "F") rgwebsec_0.IsChecked = true;
                if (Settings.BrowsePermission == "A") rgwebsec_1.IsChecked = true;
                if (Settings.BrowsePermission == "N") rgwebsec_2.IsChecked = true;
            }

            spnpurge.Value = Settings.PurgeCutOffDay;

            if (Settings.CheckSecondMonitor == "Y") chkSecondMonitor.IsChecked = true; else chkSecondMonitor.IsChecked = false;
            if (Settings.CheckSecondMonitorWithPOS == "Y") chkSMWithPOS.IsChecked = true; else chkSMWithPOS.IsChecked = false;

            chkSMWithPOS.Visibility = chkSecondMonitor.IsChecked == true ? Visibility.Visible : Visibility.Hidden;

            txtSecondMonitorApp.Text = Settings.ApplicationSecondMonitor;

            if (Settings.SalesService == "Y") chksrvsales.IsChecked = true; else chksrvsales.IsChecked = false;
            if (Settings.RentService == "Y") chksrvrent.IsChecked = true; else chksrvrent.IsChecked = false;
            if (Settings.RepairService == "Y") chksrvrepair.IsChecked = true; else chksrvrepair.IsChecked = false;

            if (Settings.POSDefaultService == "Sales") cmbDefaultService.SelectedIndex = 0;
            else if (Settings.POSDefaultService == "Rent") cmbDefaultService.SelectedIndex = 1;
            else if (Settings.POSDefaultService == "Repair") cmbDefaultService.SelectedIndex = 2;
            else cmbDefaultService.SelectedIndex = 0;

            chkcustomeronrent.IsChecked = (Settings.CustomerRequiredOnRent == "Y") ? true : false;
            chkrentcalclater.IsChecked = (Settings.CalculateRentLater == "Y") ? true : false;
            chkPOS20.IsChecked = (Settings.ShowSKUOnPOSButton == "Y") ? true : false;

            //chkPOS50.IsChecked = (Settings.DirectReceiptPrinting == "Y") ? true : false;

            chkNotReadBarCodeCheck.IsChecked = (Settings.NotReadBarcodeCheckDigit == "Y") ? true : false;
            chk8200Scanner.IsChecked = (Settings.Scanner_8200 == "Y") ? true : false;
            chkDisplay4DigitWeight.IsChecked = Settings.Display4DigitWeight == "Y";

            chkPOSNegativeStoreCdt.IsChecked = (Settings.AllowNegativeStoreCredit == "Y") ? true : false;

            chkBCF1.IsChecked = Settings.RandomWeightUPCCheckDigit == "2";
            chkBCF2.IsChecked = Settings.RandomWeightUPCCheckDigit == "20";
            chkBCF3.IsChecked = Settings.RandomWeightUPCCheckDigit == "02";

            //GetItemGridDimensions();
            //GetCustomerGridDimensions();
            //GetVendorGridDimensions();


            txtREHost.Text = Settings.REHost;
            txtREUser.Text = Settings.REUser;
            txtREPassword.Text = Settings.REPassword;
            txtREPort.Value = Settings.REPort;
            chkRESSL.IsChecked = Settings.RESSL == "Y";
            txtReportEmail.Text = Settings.ReportEmail;
            txtREFromEmail.Text = Settings.REFromAddress;
            txtREFromName.Text = Settings.REFromName;
            txtREReply.Text = Settings.REReplyTo;

            txtBottleDepositAmt.Text = Settings.BottleDeposit.ToString("f2");
            txtMaxWeight.Text = Settings.MaxScaleWeight.ToString("f2");

            cmbLayawayPayment.SelectedIndex = Settings.LayawayPaymentOption;




            txtNTEP.Text = Settings.NTEPCert;

            cmbFocus.SelectedIndex = Settings.FocusOnEditProduct == "Description" ? 0 : 1;

            if (Settings.PrintTrainingMode == "Y") chkPrintTrainingMode.IsChecked = true;
            else chkPrintTrainingMode.IsChecked = false;

            if (Settings.default_culture == "en-US") cmbLanguage.SelectedIndex = 0;
            if (Settings.default_culture == "es-ES") cmbLanguage.SelectedIndex = 1;

            GeneralFunctions.LoadPhotofromDB("Logo", 0, pictPhoto);

            if (Settings.PrintLogoInReceipt == "Y") chkPrintLogo.IsChecked = true; else chkPrintLogo.IsChecked = false;




            strAdvtDispArea = Settings.AdvtDispArea;
            strAdvtScale = Settings.AdvtScale;

            strAdvtDispNo = Settings.AdvtDispNo;
            strAdvtDispTime = Settings.AdvtDispTime;
            strAdvtDispOrder = Settings.AdvtDispOrder;
            strAdvtFont = Settings.AdvtFont;
            strAdvtColor = Settings.AdvtColor;
            strAdvtBackground = Settings.AdvtBackground;
            strAdvtDir = Settings.AdvtDir;

            if (Settings.ScaleDevice == "XEPOS") cmbScaleType.SelectedIndex = 0;
            if (Settings.ScaleDevice == "Datalogic Scale") cmbScaleType.SelectedIndex = 1;
            if (Settings.ScaleDevice == "Live Weight") cmbScaleType.SelectedIndex = 2;
            if (Settings.ScaleDevice == "(None)") cmbScaleType.SelectedIndex = 3;
            cmbport.EditValue = Settings.COMPort1;
            //cmbport.SelectedIndex = cmbport.Items.IndexOf(Settings.COMPort);
            pBaudRate = Settings.BaudRate;
            pDataBits = Settings.DataBits;
            pStopBits = Settings.StopBits;
            pParity = Settings.Parity;
            pHandshake = Settings.Handshake;
            pTimeout = Settings.Timeout;

            if (Settings.LinkGL1 > 0) cmbGL1.EditValue = Settings.LinkGL1.ToString();
            if (Settings.LinkGL2 > 0) cmbGL2.EditValue = Settings.LinkGL2.ToString();
            if (Settings.LinkGL3 > 0) cmbGL3.EditValue = Settings.LinkGL3.ToString();
            if (Settings.LinkGL4 > 0) cmbGL4.EditValue = Settings.LinkGL4.ToString();
            if (Settings.LinkGL5 > 0) cmbGL5.EditValue = Settings.LinkGL5.ToString();
            if (Settings.LinkGL6 > 0) cmbGL6.EditValue = Settings.LinkGL6.ToString();

            if (Settings.TaxInclusive == "Y") chkTaxInclusive.IsChecked = true;
            else chkTaxInclusive.IsChecked = false;

            if (Settings.UseTouchKeyboardInAdmin == "Y") chkTouchKybrdInAdmin.IsChecked = true;
            else chkTouchKybrdInAdmin.IsChecked = false;

            if (Settings.UseTouchKeyboardInPOS == "Y") chkTouchKybrdInPOS.IsChecked = true;
            else chkTouchKybrdInPOS.IsChecked = false;

            // Online Booking

            if (Settings.EnableBooking == "Y") chkOnlineBooking.IsChecked = true; else chkOnlineBooking.IsChecked = false;
            if (Settings.Booking_Scheduling_NoLimit == "Y") chkSchdWindow.IsChecked = true; else chkSchdWindow.IsChecked = false;

            if (Settings.Booking_Mon_Check == "Y") chkMon.IsChecked = true; else chkMon.IsChecked = false;
            if (Settings.Booking_Tue_Check == "Y") chkTue.IsChecked = true; else chkTue.IsChecked = false;
            if (Settings.Booking_Wed_Check == "Y") chkWed.IsChecked = true; else chkWed.IsChecked = false;
            if (Settings.Booking_Thu_Check == "Y") chkThu.IsChecked = true; else chkThu.IsChecked = false;
            if (Settings.Booking_Fri_Check == "Y") chkFri.IsChecked = true; else chkFri.IsChecked = false;
            if (Settings.Booking_Sat_Check == "Y") chkSat.IsChecked = true; else chkSat.IsChecked = false;
            if (Settings.Booking_Sun_Check == "Y") chkSun.IsChecked = true; else chkSun.IsChecked = false;

            trkbarMon.SelectionStart = Settings.Booking_Mon_Start;
            trkbarTue.SelectionStart = Settings.Booking_Tue_Start;
            trkbarWed.SelectionStart = Settings.Booking_Wed_Start;
            trkbarThu.SelectionStart = Settings.Booking_Thu_Start;
            trkbarFri.SelectionStart = Settings.Booking_Fri_Start;
            trkbarSat.SelectionStart = Settings.Booking_Sat_Start;
            trkbarSun.SelectionStart = Settings.Booking_Sun_Start;

            trkbarMon.SelectionEnd = Settings.Booking_Mon_End;
            trkbarTue.SelectionEnd = Settings.Booking_Tue_End;
            trkbarWed.SelectionEnd = Settings.Booking_Wed_End;
            trkbarThu.SelectionEnd = Settings.Booking_Thu_End;
            trkbarFri.SelectionEnd = Settings.Booking_Fri_End;
            trkbarSat.SelectionEnd = Settings.Booking_Sat_End;
            trkbarSun.SelectionEnd = Settings.Booking_Sun_End;

            /* trkbarMon.Value = new DevExpress.Xpf.Editors.TrackBarEdit(Settings.Booking_Mon_Start, Settings.Booking_Mon_End);
             trkbarTue.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Tue_Start, Settings.Booking_Tue_End);
             trkbarWed.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Wed_Start, Settings.Booking_Wed_End);
             trkbarThu.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Thu_Start, Settings.Booking_Thu_End);
             trkbarFri.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Fri_Start, Settings.Booking_Fri_End);
             trkbarSat.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Sat_Start, Settings.Booking_Sat_End);
             trkbarSun.Value = new DevExpress.XtraEditors.Repository.TrackBarRange(Settings.Booking_Sun_Start, Settings.Booking_Sun_End);*/

            txtCustomerSiteURL.Text = Settings.Booking_CustomerSiteURL;
            txtBusinessDescription.Text = Settings.Booking_BusinessDetails;
            txtPrivacyNotice.Text = Settings.Booking_PrivacyNotice;

            txtbkPhone.Text = Settings.Booking_Phone;
            txtbkEmail.Text = Settings.Booking_Email;
            txtLinkedin.Text = Settings.Booking_LinkedIn_Link;
            txtTwitter.Text = Settings.Booking_Twitter_Link;
            txtFacebook.Text = Settings.Booking_Facebook_Link;

            cmbLeadDay.Text = Settings.Booking_Lead_Day.ToString();
            cmbLeadHour.Text = Settings.Booking_Lead_Hour.ToString();
            cmbLeadMinute.Text = Settings.Booking_Lead_Minute.ToString();
            cmbSlotHour.Text = Settings.Booking_Slot_Hour.ToString();
            cmbSlotMinute.Text = Settings.Booking_Slot_Minute.ToString();

            txtcldSvr.Text = Settings.BookingServer;
            txtcldDb.Text = Settings.BookingDB;
            txtcldUsr.Text = Settings.BookingDBUser;
            txtcldPswd.Text = Settings.BookingDBPassword;

            txtSchdWindow.Text = Settings.BookingScheduleWindowDay.ToString();
            cmbReminderHour.Text = Settings.Booking_Reminder_Hour.ToString();
            cmbReminderMinute.Text = Settings.Booking_Reminder_Minute.ToString();


            txtICountry.Text = Settings.CountryName;
            txtICurrencySymbol.Text = Settings.CurrencySymbol;
            if (Settings.DateFormat == "")
            {
                cmbDateSeparator.SelectedIndex = -1;
                txtIDateFormat.Text = Settings.DateFormat;
            }
            else
            {
                if (txtIDateFormat.Text.Contains("/"))
                {
                    cmbDateSeparator.SelectedIndex = 0;
                }
                if (txtIDateFormat.Text.Contains("-"))
                {
                    cmbDateSeparator.SelectedIndex = 1;
                }
                if (txtIDateFormat.Text.Contains("2"))
                {
                    cmbDateSeparator.SelectedIndex = 0;
                }
                txtIDateFormat.Text = Settings.DateFormat;
            }

            txtSmallCurrecyName.Text = Settings.SmallCurrencyName;
            txtBigCurrecyName.Text = Settings.BigCurrencyName;

            txtCoin1.Text = Settings.Coin1Name;
            txtCoin2.Text = Settings.Coin2Name;
            txtCoin3.Text = Settings.Coin3Name;
            txtCoin4.Text = Settings.Coin4Name;
            txtCoin5.Text = Settings.Coin5Name;
            txtCoin6.Text = Settings.Coin6Name;
            txtCoin7.Text = Settings.Coin7Name;

            txtCurrency1.Text = Settings.Currency1Name;
            txtCurrency2.Text = Settings.Currency2Name;
            txtCurrency3.Text = Settings.Currency3Name;
            txtCurrency4.Text = Settings.Currency4Name;
            txtCurrency5.Text = Settings.Currency5Name;
            txtCurrency6.Text = Settings.Currency6Name;
            txtCurrency7.Text = Settings.Currency7Name;
            txtCurrency8.Text = Settings.Currency8Name;
            txtCurrency9.Text = Settings.Currency9Name;
            txtCurrency10.Text = Settings.Currency10Name;

            chkQuickTender1.IsChecked = Settings.Currency1QuickTender == "Y";
            chkQuickTender2.IsChecked = Settings.Currency2QuickTender == "Y";
            chkQuickTender3.IsChecked = Settings.Currency3QuickTender == "Y";
            chkQuickTender4.IsChecked = Settings.Currency4QuickTender == "Y";
            chkQuickTender5.IsChecked = Settings.Currency5QuickTender == "Y";
            chkQuickTender6.IsChecked = Settings.Currency6QuickTender == "Y";
            chkQuickTender7.IsChecked = Settings.Currency7QuickTender == "Y";
            chkQuickTender8.IsChecked = Settings.Currency8QuickTender == "Y";
            chkQuickTender9.IsChecked = Settings.Currency9QuickTender == "Y";
            chkQuickTender10.IsChecked = Settings.Currency10QuickTender == "Y";

            if (Settings.SmallCurrencyName != "")
            {
                txtCoin1.IsEnabled = txtCoin2.IsEnabled = txtCoin3.IsEnabled = txtCoin4.IsEnabled = txtCoin5.IsEnabled = txtCoin6.IsEnabled = txtCoin7.IsEnabled = true;

                lbEquivalentCoin1.Text = "= 1 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin2.Text = "= 2 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin3.Text = "= 5 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin4.Text = "= 10 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin5.Text = "= 20 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin6.Text = "= 25 " + txtSmallCurrecyName.Text.Trim();
                lbEquivalentCoin7.Text = "= 50 " + txtSmallCurrecyName.Text.Trim();
            }

            if (Settings.BigCurrencyName != "")
            {
                txtCurrency1.IsEnabled = txtCurrency2.IsEnabled = txtCurrency3.IsEnabled = txtCurrency4.IsEnabled = txtCurrency5.IsEnabled = txtCurrency6.IsEnabled = txtCurrency7.IsEnabled = txtCurrency8.IsEnabled = txtCurrency9.IsEnabled = txtCurrency10.IsEnabled = true;
                lbEquivalentCurrency1.Text = "= 1 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency2.Text = "= 2 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency3.Text = "= 5 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency4.Text = "= 10 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency5.Text = "= 20 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency6.Text = "= 50 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency7.Text = "= 100 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency8.Text = "= 200 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency9.Text = "= 500 " + txtBigCurrecyName.Text.Trim();
                lbEquivalentCurrency10.Text = "= 1000 " + txtBigCurrecyName.Text.Trim();
            }
        }

        private void PopulateScaleGraduation()
        {
            try
            {
                PosDataObject.Setup objPO = new PosDataObject.Setup();
                objPO.Connection = SystemVariables.Conn;
                DataTable dtbl = objPO.GetScaleGraduationData();
                foreach (DataRow dr in dtbl.Rows)
                {
                    s_Dual = dr["ScaleType"].ToString();
                    s_GrdText = dr["Graduation"].ToString();
                    d_FromWt1 = GeneralFunctions.fnDouble(dr["S_Range1"].ToString());
                    d_ToWt1 = GeneralFunctions.fnDouble(dr["S_Range2"].ToString());
                    d_Grd1 = GeneralFunctions.fnDouble(dr["S_Graduation"].ToString());
                    d_FromWt2 = GeneralFunctions.fnDouble(dr["D_Range1"].ToString());
                    d_ToWt2 = GeneralFunctions.fnDouble(dr["D_Range2"].ToString());
                    d_Grd2 = GeneralFunctions.fnDouble(dr["D_Graduation"].ToString());
                    s_SChk = dr["S_Check2Digit"].ToString();
                    s_DChk = dr["D_Check2Digit"].ToString();
                }

                dtbl.Dispose();
            }
            finally
            {

            }
        }

        private void PopulatePOSFunctions()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchPOSFunctionData();
            dtblPOSFunc = dtbl;
            //SetPOSFunctionResource(dtblPOSFunc);
            //grdFuncBtn.ite = dtblPOSFunc;
            dtbl.Dispose();
        }


        private void GetTaskScheduler()
        {
            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st2 = new ScheduledTasks();
                    TaskScheduler.Task t2 = st2.OpenTask(SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName));
                    foreach (TaskScheduler.DailyTrigger dt2 in t2.Triggers)
                    {
                        txtBookSchdTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                        if (dt2.IntervalMinutes > 0)
                        {
                            rep1.Visibility = Visibility.Visible;
                            rep2.Visibility = Visibility.Visible;
                            rep3.Visibility = Visibility.Visible;
                            rep4.Visibility = Visibility.Visible;
                            rep5.Visibility = Visibility.Visible;
                            int durmin = dt2.DurationMinutes;
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                            rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                            if (val == 0)
                            {
                                rep1.Value = quotient;
                                rep2.SelectedIndex = 1;
                            }
                            else
                            {
                                rep1.Value = dt2.IntervalMinutes;
                                rep2.SelectedIndex = 0;
                            }
                        }
                    }
                    btnGCSchedule.Content = "Modify Scheduler";
                    btnGCRunSchedule.Visibility = Visibility.Visible;
                    btnGCSchedule.Tag = "Modify Scheduler";
                }
                catch
                {
                    btnGCSchedule.Content = "Add Scheduler";
                    btnGCSchedule.Tag = "Add Scheduler";
                }

            }


            if (Settings.OSVersion == "Win 11")
            {

                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Online Booking Service".Replace("XEPOS", SystemVariables.BrandName));


                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        txtBookSchdTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                        RepetitionPattern repetition = dt.Repetition;
                        if (repetition.Interval.TotalMinutes > 0)
                        {

                            rep1.Visibility = Visibility.Visible;
                            rep2.Visibility = Visibility.Visible;
                            rep3.Visibility = Visibility.Visible;
                            rep4.Visibility = Visibility.Visible;
                            rep5.Visibility = Visibility.Visible;
                            int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                            rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                            if (val == 0)
                            {
                                rep1.Value = quotient;
                                rep2.SelectedIndex = 1;
                            }
                            else
                            {
                                rep1.Value = repetition.Interval.Minutes;
                                rep2.SelectedIndex = 0;
                            }
                        }

                        btnGCSchedule.Content = "Modify Scheduler";
                        btnGCRunSchedule.Visibility = Visibility.Visible;
                        btnGCSchedule.Tag = "Modify Scheduler";
                    }



                }
                catch
                {
                    btnGCSchedule.Content = "Add Scheduler";
                    btnGCSchedule.Tag = "Add Scheduler";
                }
            }
        }


        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            objSetup.LoginUserID = SystemVariables.CurrentUserID;

            //Backup
            objSetup.BackupType = cmbBkupType.SelectedIndex;
            objSetup.BackupStorageType = cmbBkupStorageType.SelectedIndex;
            // General Setup
            if (rgCustInfo_0.IsChecked == true)
            {
                objSetup.CustomerInfo = 1;
            }
            if (rgCustInfo_1.IsChecked == true)
            {
                objSetup.CustomerInfo = 2;
            }

            if (rgVendUpdt_0.IsChecked == true)
            {
                objSetup.VendorUpdate = 0;
            }
            else if (rgVendUpdt_1.IsChecked == true)
            {
                objSetup.VendorUpdate = 1;
            }
            else
            {
                objSetup.VendorUpdate = 2;
            }

            if (rgCloseout_0.IsChecked == true)
            {
                objSetup.CloseoutOption = 0;
            }
            else if (rgCloseout_1.IsChecked == true)
            {
                objSetup.CloseoutOption = 1;
            }
            else
            {
                objSetup.CloseoutOption = 2;
            }

            if (rgSignin_0.IsChecked == true)
            {
                objSetup.SignINOption = 0;
            }
            else if (rgSignin_1.IsChecked == true)
            {
                objSetup.SignINOption = 1;
            }
            else
            {
                objSetup.SignINOption = 2;
            }

            if (rgPrintInvoice_0.IsChecked == true)
            {
                objSetup.POSPrintInvoice = 0;
            }
            else if (rgPrintInvoice_1.IsChecked == true)
            {
                objSetup.POSPrintInvoice = 1;
            }
            else
            {
                objSetup.POSPrintInvoice = 2;
            }

            objSetup.MonthsInvJrnl = 0;
            objSetup.GiftCertMaxChange = GeneralFunctions.fnDouble(numGC.Text);

            if (chkTaxInclusive.IsChecked == true) objSetup.TaxInclusive = "Y";
            else objSetup.TaxInclusive = "N";

            if (chkTouchKybrdInAdmin.IsChecked == true) objSetup.UseTouchKeyboardInAdmin = "Y";
            else objSetup.UseTouchKeyboardInAdmin = "N";

            if (chkTouchKybrdInPOS.IsChecked == true) objSetup.UseTouchKeyboardInPOS = "Y";
            else objSetup.UseTouchKeyboardInPOS = "N";

            objSetup.Use4Decimal = (chk4digit.IsChecked == true) ? "Y" : "N";

            objSetup.DecimalPlace = 2;

            if (chkPrintBlindDrop.IsChecked == true) objSetup.PrintBlindDropCloseout = "Y";
            else objSetup.PrintBlindDropCloseout = "N";


            if (chkCloseout1.IsChecked == true) objSetup.SalesByDept = "Y";
            else objSetup.SalesByDept = "N";

            if (chkCloseout2.IsChecked == true) objSetup.BlindCountPreview = "Y";
            else objSetup.BlindCountPreview = "N";

            if (chkCloseout3.IsChecked == true) objSetup.SalesByHour = "Y";
            else objSetup.SalesByHour = "N";

            if (chkCloseout4.IsChecked == true) objSetup.CloseoutExport = "Y";
            else objSetup.CloseoutExport = "N";

            if (chkCloseout5.IsChecked == true) objSetup.OtherTerminalCloseout = "Y";
            else objSetup.OtherTerminalCloseout = "N";

            if (chkPOS745.IsChecked == true) objSetup.PrintDuplicateGiftCertSaleReceipt = "Y";
            else objSetup.PrintDuplicateGiftCertSaleReceipt = "N";

            if (chkPOS1.IsChecked == true) objSetup.POSAcceptCheck = "Y";
            else objSetup.POSAcceptCheck = "N";

            if (chkPOS2.IsChecked == true) objSetup.POSDisplayChangeDue = "Y";
            else objSetup.POSDisplayChangeDue = "N";

            if (chkPOS3.IsChecked == true) objSetup.POSIDRequired = "Y";
            else objSetup.POSIDRequired = "N";

            /*if (chkPOS5.IsChecked == true) objSetup.POSRedirectToChangeDue = "Y";
            else objSetup.POSRedirectToChangeDue = "N";*/

            objSetup.POSRedirectToChangeDue = "N";

            if (chkPOS6.IsChecked == true) objSetup.POSShowGiftCertBalance = "Y";
            else objSetup.POSShowGiftCertBalance = "N";

            /*
            if (chkPOS10.IsChecked == true) objSetup.ReceiptPrintOnReturn = "Y";
            else objSetup.ReceiptPrintOnReturn = "N";
            */

            objSetup.ReceiptPrintOnReturn = "N";

            if (chkPOS33.IsChecked == true) objSetup.AcceptTips = "Y";
            else objSetup.AcceptTips = "N";

            if (chkPOS34.IsChecked == true) objSetup.ShowTipsInReceipt = "Y";
            else objSetup.ShowTipsInReceipt = "N";

            if (chkPrePrint.IsChecked == true) objSetup.PreprintedReceipt = "Y";
            else objSetup.PreprintedReceipt = "N";

            if (chkPOS39.IsChecked == true) objSetup.ShowFoodStampTotal = "Y";
            else objSetup.ShowFoodStampTotal = "N";

            if (chkPOS69.IsChecked == true) objSetup.ShowSaleSaveInReceipt = "Y";
            else objSetup.ShowSaleSaveInReceipt = "N";

            if (chkPOS110.IsChecked == true) objSetup.AddGallon = "Y";
            else objSetup.AddGallon = "N";

            if (chkPOS150.IsChecked == true) objSetup.Print2TicketsForRepair = "Y";
            else objSetup.Print2TicketsForRepair = "N";

            if (chkPOS250.IsChecked == true) objSetup.NoSaleReceipt = "Y";
            else objSetup.NoSaleReceipt = "N";

            if (chkPOS251.IsChecked == true) objSetup.HouseAccountBalanceInReceipt = "Y";
            else objSetup.HouseAccountBalanceInReceipt = "N";

            if (chkPOSNegativeStoreCdt.IsChecked == true) objSetup.AllowNegativeStoreCredit = "Y";
            else objSetup.AllowNegativeStoreCredit = "N";

            if (rgPrintTender_0.IsChecked == true)
            {
                objSetup.POSPrintTender = 0;
            }
            else if (rgPrintTender_1.IsChecked == true)
            {
                objSetup.POSPrintTender = 1;
            }
            else
            {
                objSetup.POSPrintTender = 2;
            }


            objSetup.CardPublisherName = "";
            objSetup.CardPublisherEMail = "";



            if (chkPOS8.IsChecked == true) objSetup.IsDuplicateInvoice = "Y";
            else objSetup.IsDuplicateInvoice = "N";

            /*
            if (chkGeneral1.IsChecked) objSetup.UseInvJrnl = "Y";
            else objSetup.UseInvJrnl = "N";

            if (chkGeneral2.IsChecked) objSetup.NoPriceOnLabelDefault = "Y";
            else objSetup.NoPriceOnLabelDefault = "N";

            if (chkGeneral3.IsChecked) objSetup.QuantityRequired = "Y";
            else objSetup.QuantityRequired = "N";
            */

            objSetup.UseInvJrnl = "N";
            objSetup.NoPriceOnLabelDefault = "N";
            objSetup.QuantityRequired = "N";

            if (chkForcedlogin.IsChecked == true) objSetup.ForcedLogin = "Y";
            else objSetup.ForcedLogin = "N";

            if (chkA4.IsChecked == true) objSetup.GeneralReceiptPrint = "Y";
            else objSetup.GeneralReceiptPrint = "N";

            if (chkAutoPO.IsChecked == true) objSetup.AutoPO = "Y";
            else objSetup.AutoPO = "N";

            if (chkAutoCustomer.IsChecked == true) objSetup.AutoCustomer = "Y";
            else objSetup.AutoCustomer = "N";

            if (chkAutoGC.IsChecked == true) objSetup.AutoGC = "Y";
            else objSetup.AutoGC = "N";

            if (chkSingleGC.IsChecked == true) objSetup.SingleGC = "Y";
            else objSetup.SingleGC = "N";

            if (chkBartender.IsChecked == true) objSetup.BarTenderLabelPrint = "N";
            else objSetup.BarTenderLabelPrint = "N";

            objSetup.UseCustomerNameInLabelPrint = (chkUseCustNameInLabelPrint.IsChecked == true) ? "Y" : "N";

            //objSetup.EasyTendering = (chkPOS81.IsChecked == true) ? "Y" : "N";
            objSetup.EasyTendering = "Y";
            objSetup.ShowFeesInReceipt = (chkPOS82.IsChecked == true) ? "Y" : "N";

            objSetup.LayawaysDue = GeneralFunctions.fnInt32(numLDue.Text);
            objSetup.LayawayReceipts = GeneralFunctions.fnInt32(numLPrint.Text);
            objSetup.LayawayDepositPercent =  GeneralFunctions.fnDouble(numLDepPerc.Text);
            objSetup.ReceiptLayawayPolicy = memLP.Text.Trim();


            objSetup.ReceiptHeader = memReceiptH.Text.Trim();
            objSetup.ReceiptFooter = memReceiptF.Text.Trim();
            objSetup.AppointmentEmailBody = memEmail.Text == null ? "" : memEmail.Text.Trim();

            string strPole = "";
            if (txtPole1.Text.Trim() == "") strPole = strPole.PadRight(20);
            else strPole = txtPole1.Text.Trim().PadRight(20);
            int len = strPole.Length;

            if (txtPole2.Text.Trim() == "") strPole = strPole.PadRight(40);
            else strPole = strPole + txtPole2.Text.Trim().PadRight(20);

            objSetup.PoleScreen = strPole;

            objSetup.FormSkin = cmbFormSkin.Text.Trim();
            objSetup.POSMoreFunctionAlignment = cmbAlign.SelectedIndex;



            // Office Info
            objSetup.Company = txtCompany.Text.Trim();
            objSetup.Address1 = txtAdd1.Text.Trim();
            objSetup.Address2 = txtAdd2.Text.Trim();
            objSetup.City = txtCity.Text.Trim();
            objSetup.State = txtState.Text.Trim();
            objSetup.PostalCode = cmbZip.Text.Trim();
            objSetup.Phone = txtPhone.Text.Trim();
            objSetup.Fax = txtFax.Text.Trim();
            objSetup.EMail = txtEmail.Text.Trim();
            objSetup.WebAddress = txtWebAddress.Text.Trim();

            objSetup.StoreID = GeneralFunctions.GetRecordCount("StoreInfo");
            objSetup.OtherID = GeneralFunctions.GetRecordCount("Setup");
            objSetup.ExpImpID = GeneralFunctions.GetRecordCount("CentralExportImport");

            objSetup.POSFunctionUpdate = blSetPOSFunction;
            objSetup.SplitDataTable = dtblPOSFunc;



            objSetup.GradData = dtblGraduation;

            

            objSetup.StoreInfo = txtMsg.Text.Trim();

            /*if (chkPOS4.IsChecked == true) objSetup.POSDisplayProductImage = "Y";
            else objSetup.POSDisplayProductImage = "N";*/

            objSetup.POSDisplayProductImage = "N";

            if (chkPOS225.IsChecked == true) objSetup.CalculatorStyleKeyboard = "Y";
            else objSetup.CalculatorStyleKeyboard = "N";

            objSetup.FunctionButtonAccess = blFunctionBtnAccess;
            objSetup.ChangedByAdmin = intSuperUserID;

            objSetup.DisplayLogoOrWeb = rglogo_0.IsChecked == true ? 0 : 1;

            if (rglogo_0.IsChecked == true)
            {
                objSetup.DisplayURL = "";
            }
            else 
            {
                objSetup.DisplayURL = txtFixedWeb.Text.Trim();
            }



            if (chksrvsales.IsChecked == true) objSetup.SalesService = "Y"; else objSetup.SalesService = "N";
            if (chksrvrent.IsChecked == true) objSetup.RentService = "Y"; else objSetup.RentService = "N";
            if (chksrvrepair.IsChecked == true) objSetup.RepairService = "Y"; else objSetup.RepairService = "N";

            if (cmbDefaultService.SelectedIndex == 0) objSetup.POSDefaultService = "Sales";
            if (cmbDefaultService.SelectedIndex == 1) objSetup.POSDefaultService = "Rent";
            if (cmbDefaultService.SelectedIndex == 2) objSetup.POSDefaultService = "Repair";

            objSetup.SMTPServer = cmbSMTPType.SelectedIndex;
            objSetup.ReportEmail = txtReportEmail.Text.Trim();
            objSetup.REHost = txtREHost.Text.Trim();
            objSetup.REUser = txtREUser.Text.Trim();
            objSetup.REPassword = txtREPassword.Text.Trim();
            objSetup.REPort = GeneralFunctions.fnInt32(txtREPort.Value);
            objSetup.RESSL = chkRESSL.IsChecked == true ? "Y" : "N";
            objSetup.REFromAddress = txtREFromEmail.Text.Trim();
            objSetup.REFromName = txtREFromName.Text.Trim();
            objSetup.REReplyTo = txtREReply.Text.Trim();

            objSetup.BottleRefund = GeneralFunctions.fnDouble(txtBottleDepositAmt.Text);
            objSetup.MaxScaleWeight = GeneralFunctions.fnDouble(txtMaxWeight.Text);
            objSetup.PurgeCutOffDay = Convert.ToInt32(spnpurge.Value);
            objSetup.PrinterDataTable = grdPrinter2.ItemsSource as DataTable;
            objSetup.HostName = Settings.TerminalName;

            objSetup.LayawayPaymentOption = cmbLayawayPayment.SelectedIndex;

            if (chkCloseout4.IsChecked == true)
            {
                objSetup.LinkGL1 = cmbGL1.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL1.EditValue);
                objSetup.LinkGL2 = cmbGL2.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL2.EditValue);
                objSetup.LinkGL3 = cmbGL3.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL3.EditValue);
                objSetup.LinkGL4 = cmbGL4.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL4.EditValue);
                objSetup.LinkGL5 = cmbGL5.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL5.EditValue);
                objSetup.LinkGL6 = cmbGL6.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbGL6.EditValue);
            }
            else
            {
                objSetup.LinkGL1 = 0;
                objSetup.LinkGL2 = 0;
                objSetup.LinkGL3 = 0;
                objSetup.LinkGL4 = 0;
                objSetup.LinkGL5 = 0;
                objSetup.LinkGL6 = 0;
            }

            objSetup.FocusOnEditProduct = cmbFocus.SelectedIndex == 0 ? "Description" : "Price";

            if (cmbScaleType.SelectedIndex == 0)
            {
                objSetup.ScaleDevice = "XEPOS";
            }
            else if (cmbScaleType.SelectedIndex == 1)
            {
                objSetup.ScaleDevice = "Datalogic Scale";
            }
            else if (cmbScaleType.SelectedIndex == 2)
            {
                objSetup.ScaleDevice = "Live Weight";
            }
            else if (cmbScaleType.SelectedIndex == 3)
            {
                objSetup.ScaleDevice = "(None)";
            }
            else
            {
                objSetup.ScaleDevice = "";
            }

            objSetup.NotReadScaleBarcodeCheckDigit = chkNotReadBarCodeCheck.IsChecked == true ? "Y" : "N";
            objSetup.DatalogicScanner8200 = chk8200Scanner.IsChecked == true ? "Y" : "N";
            objSetup.NTEPCert = txtNTEP.Text.Trim();

            objSetup.Graduation = s_GrdText;
            objSetup.ScaleType = s_Dual;
            objSetup.S_Range1 = d_FromWt1;
            objSetup.S_Range2 = d_ToWt1;
            objSetup.S_Graduation = d_Grd1;
            objSetup.D_Range1 = d_FromWt2;
            objSetup.D_Range2 = d_ToWt2;
            objSetup.S_Check2Digit = s_SChk;
            objSetup.D_Check2Digit = s_DChk;
            objSetup.D_Graduation = d_Grd2;

            objSetup.PrintTrainingMode = chkPrintTrainingMode.IsChecked == true ? "Y" : "N";


            // Online Booking

            objSetup.EnableBooking = chkOnlineBooking.IsChecked == true ? "Y" : "N";

            objSetup.Booking_CustomerSiteURL = txtCustomerSiteURL.Text.Trim();
            objSetup.Booking_BusinessDetails = txtBusinessDescription.Text.Trim();
            objSetup.Booking_PrivacyNotice = txtPrivacyNotice.Text.Trim();
            objSetup.Booking_Phone = txtbkPhone.Text.Trim();
            objSetup.Booking_Email = txtbkEmail.Text.Trim();
            objSetup.Booking_LinkedIn_Link = txtLinkedin.Text.Trim();
            objSetup.Booking_Twitter_Link = txtTwitter.Text.Trim();
            objSetup.Booking_Facebook_Link = txtFacebook.Text.Trim();

            objSetup.Booking_Lead_Day = GeneralFunctions.fnInt32(cmbLeadDay.Text);
            objSetup.Booking_Lead_Hour = GeneralFunctions.fnInt32(cmbLeadHour.Text);
            objSetup.Booking_Lead_Minute = GeneralFunctions.fnInt32(cmbLeadMinute.Text);

            objSetup.Booking_Slot_Hour = GeneralFunctions.fnInt32(cmbSlotHour.Text);
            objSetup.Booking_Slot_Minute = GeneralFunctions.fnInt32(cmbSlotMinute.Text);

            objSetup.Booking_Scheduling_NoLimit = chkSchdWindow.IsChecked == true ? "Y" : "N";

            objSetup.BookingScheduleWindowDay = GeneralFunctions.fnInt32(txtSchdWindow.Text);
            objSetup.Booking_Reminder_Hour = GeneralFunctions.fnInt32(cmbReminderHour.Text);
            objSetup.Booking_Reminder_Minute = GeneralFunctions.fnInt32(cmbReminderMinute.Text);

            objSetup.Booking_Mon_Check = chkMon.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Tue_Check = chkTue.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Wed_Check = chkWed.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Thu_Check = chkThu.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Fri_Check = chkFri.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Sat_Check = chkSat.IsChecked == true ? "Y" : "N";
            objSetup.Booking_Sun_Check = chkSun.IsChecked == true ? "Y" : "N";

            objSetup.Booking_Mon_Start = (int)trkbarMon.SelectionStart;
            objSetup.Booking_Mon_End = (int)trkbarMon.SelectionEnd;

            objSetup.Booking_Tue_Start = (int)trkbarTue.SelectionStart;
            objSetup.Booking_Tue_End = (int)trkbarTue.SelectionEnd;

            objSetup.Booking_Wed_Start = (int)trkbarWed.SelectionStart;
            objSetup.Booking_Wed_End = (int)trkbarWed.SelectionEnd;

            objSetup.Booking_Thu_Start = (int)trkbarThu.SelectionStart;
            objSetup.Booking_Thu_End = (int)trkbarThu.SelectionEnd;

            objSetup.Booking_Fri_Start = (int)trkbarFri.SelectionStart;
            objSetup.Booking_Fri_End = (int)trkbarFri.SelectionEnd;

            objSetup.Booking_Sat_Start = (int)trkbarSat.SelectionStart;
            objSetup.Booking_Sat_End = (int)trkbarSat.SelectionEnd;

            objSetup.Booking_Sun_Start = (int)trkbarSun.SelectionStart;
            objSetup.Booking_Sun_End = (int)trkbarSun.SelectionEnd;

            objSetup.BookingServer = txtcldSvr.Text.Trim();
            objSetup.BookingDB = txtcldDb.Text.Trim();
            objSetup.BookingDBUser = txtcldUsr.Text.Trim();
            objSetup.BookingDBPassword = txtcldPswd.Text.Trim();


            // Internationalization

            objSetup.CountryName = txtICountry.Text.Trim();
            objSetup.CurrencySymbol = txtICurrencySymbol.Text.Trim();
            objSetup.DateFormat = txtIDateFormat.Text.Trim();


            objSetup.SmallCurrencyName = txtSmallCurrecyName.Text.Trim();
            objSetup.BigCurrencyName = txtBigCurrecyName.Text.Trim();

            objSetup.Coin1Name = txtCoin1.Text.Trim();
            objSetup.Coin2Name = txtCoin2.Text.Trim();
            objSetup.Coin3Name = txtCoin3.Text.Trim();
            objSetup.Coin4Name = txtCoin4.Text.Trim();
            objSetup.Coin5Name = txtCoin5.Text.Trim();
            objSetup.Coin6Name = txtCoin6.Text.Trim();
            objSetup.Coin7Name = txtCoin7.Text.Trim();

            objSetup.Currency1Name = txtCurrency1.Text.Trim();
            objSetup.Currency2Name = txtCurrency2.Text.Trim();
            objSetup.Currency3Name = txtCurrency3.Text.Trim();
            objSetup.Currency4Name = txtCurrency4.Text.Trim();
            objSetup.Currency5Name = txtCurrency5.Text.Trim();
            objSetup.Currency6Name = txtCurrency6.Text.Trim();
            objSetup.Currency7Name = txtCurrency7.Text.Trim();
            objSetup.Currency8Name = txtCurrency8.Text.Trim();
            objSetup.Currency9Name = txtCurrency9.Text.Trim();
            objSetup.Currency10Name = txtCurrency10.Text.Trim();

            objSetup.Currency1QuickTender = chkQuickTender1.IsChecked == true ? "Y" : "N";
            objSetup.Currency2QuickTender = chkQuickTender2.IsChecked == true ? "Y" : "N";
            objSetup.Currency3QuickTender = chkQuickTender3.IsChecked == true ? "Y" : "N";
            objSetup.Currency4QuickTender = chkQuickTender4.IsChecked == true ? "Y" : "N";
            objSetup.Currency5QuickTender = chkQuickTender5.IsChecked == true ? "Y" : "N";
            objSetup.Currency6QuickTender = chkQuickTender6.IsChecked == true ? "Y" : "N";
            objSetup.Currency7QuickTender = chkQuickTender7.IsChecked == true ? "Y" : "N";
            objSetup.Currency8QuickTender = chkQuickTender8.IsChecked == true ? "Y" : "N";
            objSetup.Currency9QuickTender = chkQuickTender9.IsChecked == true ? "Y" : "N";
            objSetup.Currency10QuickTender = chkQuickTender10.IsChecked == true  ? "Y" : "N";

            objSetup.ShopifyAPIKey = ""; // txtShopifyAPI.Text.Trim();
            objSetup.ShopifyPassword = txtShopifyPswd.Text.Trim();
            objSetup.ShopifyStoreAddress = txtShopifyStoreAddress.Text.Trim();
            objSetup.PremiumPlan = chkShopifyPremium.IsChecked == true ? "Y" : "N";

            objSetup.QBCompanyFilePath = txtQuickBookCompanyPath.Text == null ? "" : txtQuickBookCompanyPath.Text.Trim();
            objSetup.ItemIncomeAccount = txtQuickBookIncomeAc.Text == null ? "" : txtQuickBookIncomeAc.Text.Trim();
            objSetup.ItemCOGSAccount = txtQuickBookCOGSAc.Text == null ? "" : txtQuickBookCOGSAc.Text.Trim();


            objSetup.WCommConsumerKey = txtWooConsumerKey.Text == null ? "" : txtWooConsumerKey.Text.Trim();
            objSetup.WCommConsumerSecret = txtWooConsumerSecret.Text == null ? "" : txtWooConsumerSecret.Text.Trim();
            objSetup.WCommStoreAddress = txtWooStoreAddress.Text == null ? "" : txtWooStoreAddress.Text.Trim();


            objSetup.XeroCompanyName = txtXeroCompany.Text == null ? "" : txtXeroCompany.Text.Trim();
            objSetup.XeroClientId = txtXeroClientID.Text == null ? "" : txtXeroClientID.Text.Trim();
            objSetup.XeroCallbackUrl = txtXeroCallbackUri.Text == null ? "" : txtXeroCallbackUri.Text.Trim();
            objSetup.XeroAccessToken = txtXeroAccessToken.Text == null ? "" : txtXeroAccessToken.Text.Trim();
            objSetup.XeroRefreshToken = txtXeroRefreshToken.Text == null ? "" : txtXeroRefreshToken.Text.Trim();
            objSetup.XeroTenantId = xerotenantId == null ? "" : xerotenantId;

            objSetup.XeroInventoryAssetAccountCode = txtXeroAccount1.Text == null ? "" : txtXeroAccount1.Text.Trim();
            objSetup.XeroInventoryAdjustmentAccountCode = txtXeroAccount2.Text == null ? "" : txtXeroAccount2.Text.Trim();
            objSetup.XeroAccountCodePurchase = txtXeroAccount3.Text == null ? "" : txtXeroAccount3.Text.Trim();
            objSetup.XeroAccountCodeSale = txtXeroAccount4.Text == null ? "" : txtXeroAccount4.Text.Trim();
            objSetup.XeroCOGSAccountCodePurchase = txtXeroAccount5.Text == null ? "" : txtXeroAccount5.Text.Trim();
            objSetup.XeroCOGSAccountCodeSale = txtXeroAccount6.Text == null ? "" : txtXeroAccount6.Text.Trim();

            if (pictPhoto.Source != null)
            {
                objSetup.Logo = GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source);
            }
            else
            {
                objSetup.Logo = null;
            }

            objSetup.PrintLogoInReceipt = chkPrintLogo.IsChecked == true ? "Y" : "N";

            objSetup.EvoApiBaseAddress = pay8_Api.Text == null ? "" : pay8_Api.Text.Trim();

            objSetup.Paymentsense_AccountName = pay9_psaccountname.Text == null ? "" : pay9_psaccountname.Text.Trim();
            objSetup.Paymentsense_ApiKey = pay9_psapikey.Text == null ? "" : pay9_psapikey.Text.Trim();
            objSetup.Paymentsense_SoftwareHouseId = pay9_pssoftwareid.Text == null ? "" : pay9_pssoftwareid.Text.Trim();
            objSetup.Paymentsense_InstallerId = pay9_psinstallerid.Text == null ? "" : pay9_psinstallerid.Text.Trim();

            objSetup.ItemExpiryAlertDay = GeneralFunctions.fnInt32(numAlert.Text);

            strError = objSetup.PostSetupData();

            if (strError == "")
            {
                if (Settings.ActiveAdminPswdForMercury)
                {
                    if (isaccesspaymentconfig)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event9, "Successful", "");
                        }
                        else
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event9, "Successful", "");
                        }
                    }
                    if (ischangepaymentconfig)
                    {
                        if (SystemVariables.CurrentUserID == 0)
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event10, "Successful", "");
                        }
                        else
                        {
                            Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event10, "Successful", "");
                        }
                    }
                }
                return true;
            }
            else
            {
                if (isaccesspaymentconfig)
                {
                    if (SystemVariables.CurrentUserID == 0)
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event9, "Failed", "");
                    }
                    else
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event9, "Failed", "");
                    }
                }
                if (ischangepaymentconfig)
                {
                    if (SystemVariables.CurrentUserID == 0)
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserName, GeneralFunctions.GetHostName(), Settings.Event10, "Failed", "");
                    }
                    else
                    {
                        Settings.AddInApplicationLogs(SystemVariables.CurrentUserCode, GeneralFunctions.GetHostName(), Settings.Event10, "Failed", "");
                    }
                }
                return false;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txtDBVersion.Text = "DB Version : " + DbDefination.strDbVersion + "  "
                + "Product Version : " + GeneralFunctions.VersionInfo() + "\n" +
                "DB Path : " + GeneralFunctions.GetDBFilePath();

            string csvFilePath = GeneralFunctions.GetQBCCSVPath();
            txtQBC.Text = csvFilePath + " folder.";

            tcSetup.NextButton.Style = this.FindResource("TabNext") as Style;
            tcSetup.PrevButton.Style = this.FindResource("TabPrev") as Style;
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            boolLoad = false;

            Settings.LoadShopifySettings();
            Settings.LoadQuickBooksSettings();
            Settings.LoadWooCommerceSettings();
            Settings.LoadXEROSettings();
            //Shopify scheduler

            txtTimeN.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);

            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Shopify Integration".Replace("XEPOS", SystemVariables.BrandName));
                    if (t1 != null)
                    {
                        foreach (TaskScheduler.DailyTrigger dt2 in t1.Triggers)
                        {
                            txtTimeO.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                            if (dt2.IntervalMinutes > 0)
                            {
                                chkgcRepeat5.IsChecked = true;
                                rep41.Visibility = Visibility.Visible;
                                rep42.Visibility = Visibility.Visible;
                                rep43.Visibility = Visibility.Visible;
                                rep44.Visibility = Visibility.Visible;
                                rep45.Visibility = Visibility.Visible;
                                int durmin = dt2.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                                rep45.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    rep42.Value = quotient;
                                    rep43.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep42.Value = dt2.IntervalMinutes;
                                    rep43.SelectedIndex = 0;
                                }
                            }
                        }
                        btnAutoSchedulrO.Content = "Modify Scheduler";
                        btnAutoSchedulrO.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        txtTimeO.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }
                }
                catch
                {
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Shopify Integration".Replace("XEPOS", SystemVariables.BrandName));


                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        txtTimeO.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                        RepetitionPattern repetition = dt.Repetition;
                        if (repetition.Interval.TotalMinutes > 0)
                        {
                            chkgcRepeat5.IsChecked = true;
                            rep41.Visibility = Visibility.Visible;
                            rep42.Visibility = Visibility.Visible;
                            rep43.Visibility = Visibility.Visible;
                            rep44.Visibility = Visibility.Visible;
                            rep45.Visibility = Visibility.Visible;
                            int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                            rep45.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                            if (val == 0)
                            {
                                rep42.Value = quotient;
                                rep43.SelectedIndex = 1;
                            }
                            else
                            {
                                rep42.Value = repetition.Interval.Minutes;
                                rep43.SelectedIndex = 0;
                            }
                        }

                        btnAutoSchedulrO.Content = "Modify Scheduler";
                        btnAutoSchedulrO.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        txtTimeO.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }


                }
                catch
                {

                }
            }


            // Woo Commerce SCheduler

            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Woo Commerce Integration".Replace("XEPOS", SystemVariables.BrandName));
                    if (t1 != null)
                    {
                        foreach (TaskScheduler.DailyTrigger dt2 in t1.Triggers)
                        {
                            tmWoo.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                            if (dt2.IntervalMinutes > 0)
                            {
                                chkRepeatWoo.IsChecked = true;
                                rep51.Visibility = Visibility.Visible;
                                rep52.Visibility = Visibility.Visible;
                                rep53.Visibility = Visibility.Visible;
                                rep54.Visibility = Visibility.Visible;
                                rep55.Visibility = Visibility.Visible;
                                int durmin = dt2.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                                rep55.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    rep52.Value = quotient;
                                    rep53.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep52.Value = dt2.IntervalMinutes;
                                    rep53.SelectedIndex = 0;
                                }
                            }
                        }
                        btnAutoSchedulrWoo.Content = "Modify Scheduler";
                        btnAutoSchedulrWoo.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmWoo.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }
                }
                catch
                {
                }

            }


            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Woo Commerce Integration".Replace("XEPOS", SystemVariables.BrandName));


                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        tmWoo.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                        RepetitionPattern repetition = dt.Repetition;
                        if (repetition.Interval.TotalMinutes > 0)
                        {
                            chkRepeatWoo.IsChecked = true;
                            rep51.Visibility = Visibility.Visible;
                            rep52.Visibility = Visibility.Visible;
                            rep53.Visibility = Visibility.Visible;
                            rep54.Visibility = Visibility.Visible;
                            rep55.Visibility = Visibility.Visible;
                            int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                            rep55.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                            if (val == 0)
                            {
                                rep52.Value = quotient;
                                rep53.SelectedIndex = 1;
                            }
                            else
                            {
                                rep52.Value = repetition.Interval.Minutes;
                                rep53.SelectedIndex = 0;
                            }
                        }

                        btnAutoSchedulrWoo.Content = "Modify Scheduler";
                        btnAutoSchedulrWoo.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmWoo.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }


                }
                catch
                {

                }
            }


            // QBD SCheduler

            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " QuickBooks Windows Integration".Replace("XEPOS", SystemVariables.BrandName));
                    if (t1 != null)
                    {
                        foreach (TaskScheduler.DailyTrigger dt2 in t1.Triggers)
                        {
                            tmQuk.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                            if (dt2.IntervalMinutes > 0)
                            {
                                chkRepeatQuickBooks.IsChecked = true;
                                rep61.Visibility = Visibility.Visible;
                                rep62.Visibility = Visibility.Visible;
                                rep63.Visibility = Visibility.Visible;
                                rep64.Visibility = Visibility.Visible;
                                rep65.Visibility = Visibility.Visible;
                                int durmin = dt2.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                                rep65.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    rep62.Value = quotient;
                                    rep63.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep62.Value = dt2.IntervalMinutes;
                                    rep63.SelectedIndex = 0;
                                }
                            }
                        }
                        btnAutoSchedulrQuickBooks.Content = "Modify Scheduler";
                        btnAutoSchedulrQuickBooks.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmQuk.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }
                }
                catch
                {
                }
            }


            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " QuickBooks Windows Integration".Replace("XEPOS", SystemVariables.BrandName));


                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        tmQuk.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                        RepetitionPattern repetition = dt.Repetition;
                        if (repetition.Interval.TotalMinutes > 0)
                        {
                            chkRepeatQuickBooks.IsChecked = true;
                            rep61.Visibility = Visibility.Visible;
                            rep62.Visibility = Visibility.Visible;
                            rep63.Visibility = Visibility.Visible;
                            rep64.Visibility = Visibility.Visible;
                            rep65.Visibility = Visibility.Visible;
                            int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                            rep65.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                            if (val == 0)
                            {
                                rep62.Value = quotient;
                                rep63.SelectedIndex = 1;
                            }
                            else
                            {
                                rep62.Value = repetition.Interval.Minutes;
                                rep63.SelectedIndex = 0;
                            }
                        }

                        btnAutoSchedulrQuickBooks.Content = "Modify Scheduler";
                        btnAutoSchedulrQuickBooks.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmQuk.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }


                }
                catch
                {

                }
            }


            // XERO SCheduler

            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " XERO Integration".Replace("XEPOS", SystemVariables.BrandName));
                    if (t1 != null)
                    {
                        foreach (TaskScheduler.DailyTrigger dt2 in t1.Triggers)
                        {
                            tmXero.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt2.StartHour, dt2.StartMinute, 0);
                            if (dt2.IntervalMinutes > 0)
                            {
                                chkRepeatXero.IsChecked = true;
                                rep71.Visibility = Visibility.Visible;
                                rep72.Visibility = Visibility.Visible;
                                rep73.Visibility = Visibility.Visible;
                                rep74.Visibility = Visibility.Visible;
                                rep75.Visibility = Visibility.Visible;
                                int durmin = dt2.DurationMinutes;
                                int h = 0;
                                int mi = 0;
                                int val = 0;
                                int quotient = Math.DivRem(durmin, 60, out val);
                                if (val == 0)
                                {
                                    h = quotient;
                                    mi = 0;
                                }
                                else
                                {
                                    h = quotient;
                                    mi = val;
                                }
                                TimeSpan tt = new TimeSpan(dt2.StartHour + h, dt2.StartMinute + mi, 0);
                                rep75.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                                h = 0;
                                mi = 0;
                                val = 0;
                                quotient = 0;
                                quotient = Math.DivRem(dt2.IntervalMinutes, 60, out val);
                                if (val == 0)
                                {
                                    rep72.Value = quotient;
                                    rep73.SelectedIndex = 1;
                                }
                                else
                                {
                                    rep72.Value = dt2.IntervalMinutes;
                                    rep73.SelectedIndex = 0;
                                }
                            }
                        }
                        btnAutoSchedulrXero.Content = "Modify Scheduler";
                        btnAutoSchedulrXero.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmXero.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }
                }
                catch
                {
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " XERO Integration".Replace("XEPOS", SystemVariables.BrandName));


                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        tmXero.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);
                        RepetitionPattern repetition = dt.Repetition;
                        if (repetition.Interval.TotalMinutes > 0)
                        {
                            chkRepeatXero.IsChecked = true;
                            rep71.Visibility = Visibility.Visible;
                            rep72.Visibility = Visibility.Visible;
                            rep73.Visibility = Visibility.Visible;
                            rep74.Visibility = Visibility.Visible;
                            rep75.Visibility = Visibility.Visible;
                            int durmin = repetition.Duration.Minutes + (repetition.Duration.Hours * 60);
                            int h = 0;
                            int mi = 0;
                            int val = 0;
                            int quotient = Math.DivRem(durmin, 60, out val);
                            if (val == 0)
                            {
                                h = quotient;
                                mi = 0;
                            }
                            else
                            {
                                h = quotient;
                                mi = val;
                            }
                            TimeSpan tt = new TimeSpan(dt.StartBoundary.Hour + h, dt.StartBoundary.Minute + mi, 0);
                            rep75.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tt.Hours, tt.Minutes, 0);
                            h = 0;
                            mi = 0;
                            val = 0;
                            quotient = 0;
                            quotient = Math.DivRem(repetition.Interval.Minutes + (repetition.Interval.Hours * 60), 60, out val);
                            if (val == 0)
                            {
                                rep72.Value = quotient;
                                rep73.SelectedIndex = 1;
                            }
                            else
                            {
                                rep72.Value = repetition.Interval.Minutes;
                                rep73.SelectedIndex = 0;
                            }
                        }

                        btnAutoSchedulrXero.Content = "Modify Scheduler";
                        btnAutoSchedulrXero.Tag = "Modify Scheduler";
                    }
                    else
                    {
                        tmXero.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
                    }


                }
                catch
                {

                }
            }

            rgPaymentGateway_7.IsChecked = true;

            GeneralFunctions.PhoneMaskNew(txtPhone);
            GeneralFunctions.PhoneMaskNew(txtbkPhone);

            Title.Text = "General Settings";

            lbLanguage.Visibility = cmbLanguage.Visibility = Settings.disableresource == "Y" ? Visibility.Visible : Visibility.Hidden;

            dtblGraduation = new DataTable();
            string ModulesRegistered = GeneralFunctions.RegisteredModules();

            /*if (ModulesRegistered.Contains("POS") && !ModulesRegistered.Contains("SCALE"))
            {
                btnTemplate.Visible = label93.Visible = false;
            }
            if (ModulesRegistered.Contains("SCALE"))
            {
                simpleButton5.Visible = label25.Visible = true;
            }*/

            tpAccounting.Visibility = Settings.CloseoutExport == "Y" ? Visibility.Visible : Visibility.Collapsed;

            if (Settings.CloseoutExport == "Y")
            {
                PopulateGL(cmbGL1);
                PopulateGL(cmbGL2);
                PopulateGL(cmbGL3);
                PopulateGL(cmbGL4);
                PopulateGL(cmbGL5);
                PopulateGL(cmbGL6);
            }

            PopulateCOMPrinter();

            
            chkSMWithPOS.Content = "  " + "Use Customer Display when" + " " + SystemVariables.BrandName + " " + "opens";


            txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);

            txtBackupTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 0, 0);

            //cmbExportFileFilter.EditValue = "Last 7 days";

            // if (Settings.UseStyle == "NO") groupControl11.Visibility = Visibility.Hidden;

            /*lbFuncName.Text = "";
            lbFuncDesc.Text = "";
            if (Settings.IsPOSSelected)
            {
                btnUP1.Visible = true;
                btnDown1.Visible = true;
                pnlgrdbottm.Height = 100;
            }
            else
            {
                btnUP1.Visible = false;
                btnDown1.Visible = false;
                pnlgrdbottm.Height = 0;
            }*/
            if (!blFunctionOrderChangeAccess)
            {
                cmbAlign.IsEnabled = false;
            }
            else
            {
                cmbAlign.IsEnabled = true;
            }

            chk8200Scanner.Visibility = cmbScaleType.SelectedIndex == 1 ? Visibility.Visible : Visibility.Hidden;

            rgPaymentGateway_0.IsChecked = true;

            dtblPOSFunc = new DataTable();
            dtblPOSFunc.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionName", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("IsVisible", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionDescription", System.Type.GetType("System.String"));

            dtblScaleFunc = new DataTable();
            dtblScaleFunc.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblScaleFunc.Columns.Add("FunctionName", System.Type.GetType("System.String"));
            dtblScaleFunc.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtblScaleFunc.Columns.Add("IsVisible", System.Type.GetType("System.String"));
            dtblScaleFunc.Columns.Add("FunctionDescription", System.Type.GetType("System.String"));

            dtblScaleDept = new DataTable();
            dtblScaleDept.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblScaleDept.Columns.Add("Department", System.Type.GetType("System.String"));
            dtblScaleDept.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtblScaleDept.Columns.Add("AddToScale", System.Type.GetType("System.String"));

            PopulatePoleDisplaySerialPorts();
            PopulatePorts();
            PopulateMercuryPorts();
            PopulateReportPrinter();
            PopulateReceiptPrinter();
            PopulateLabelPrinter();
            PopulateLineDisplayDevice();



            ShowData();
            bActivateSecondMonitor = Settings.CheckSecondMonitor == "Y";
            PopulatePOSFunctions();

            //PopulateAndSetFormSkin();
            //LookAndFeel.SetSkinStyle(cmbFormSkin.Text.Trim());
            //EnableDisableButton();
            
            boolControlChanged = false;
            blCloseout = true;

            if (chkAutoSignout.IsChecked ==true)
            {
                txtLoginTime.Visibility = Visibility.Visible;
                lbLoginTime.Visibility = Visibility.Visible;
            }
            else
            {
                txtLoginTime.Visibility = Visibility.Hidden;
                lbLoginTime.Visibility = Visibility.Hidden;
            }


            if (Settings.DemoVersion == "Y")
            {
                chkCloseout4.Visibility = Visibility.Hidden;
                chkCloseout4.IsChecked = false;
                btnExport.Visibility = Visibility.Hidden;
                memReceiptH.IsEnabled = false;
                //lbRHtxt.Visible = false;
            }

            if (chkPOS7.IsChecked == true)
            {
                gcPub.IsEnabled = true;
            }
            else
            {
                gcPub.IsEnabled = false;
            }

            FetchPrinterName();
            InitialLoadLinkPrinters();

            txtBookSchdTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 9, 0, 0);
            rep2.SelectedIndex = 0;
            rep3.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 0, 0);


            GetTaskScheduler();

            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));

                    foreach (TaskScheduler.DailyTrigger dt1 in t1.Triggers)
                    {
                        txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                    }
                    btnReportSchd.Content = "Modify Scheduler";
                    btnReportSchd.Tag = "Modify Scheduler";
                    btnRunReportSchd.Visibility = Visibility.Visible;
                }
                catch
                {
                    btnReportSchd.Content = "Add Scheduler";
                    btnReportSchd.Tag = "Add Scheduler";
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail Report Scheduler".Replace("XEPOS", SystemVariables.BrandName));
                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        txtReportTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                        btnReportSchd.Content = "Modify Scheduler";
                        btnReportSchd.Tag = "Modify Scheduler";
                        btnRunReportSchd.Visibility = Visibility.Visible;
                    }


                }
                catch
                {
                    btnReportSchd.Content = "Add Scheduler";
                    btnReportSchd.Tag = "Add Scheduler";
                }
            }







            if (Settings.OSVersion == "Win 10")
            {
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();
                    TaskScheduler.Task t1 = st1.OpenTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));

                    foreach (TaskScheduler.DailyTrigger dt1 in t1.Triggers)
                    {
                        txtBackupTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt1.StartHour, dt1.StartMinute, 0);
                    }
                    
                    btnBkupSchd.Tag = "Modify Scheduler";
                    btnRunBkupSchd.Visibility = Visibility.Visible;
                }
                catch
                {
                    
                    btnBkupSchd.Tag = "Add Scheduler";
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();


                    Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(@"" + SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));
                    if (t != null)
                    {
                        Microsoft.Win32.TaskScheduler.DailyTrigger dt = t.Definition.Triggers[0] as Microsoft.Win32.TaskScheduler.DailyTrigger;
                        txtBackupTime.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dt.StartBoundary.Hour, dt.StartBoundary.Minute, 0);

                       
                        btnBkupSchd.Tag = "Modify Scheduler";
                        btnRunBkupSchd.Visibility = Visibility.Visible;
                    }


                }
                catch
                {
                   
                    btnBkupSchd.Tag = "Add Scheduler";
                }
            }


            FetchPortCommand();

            PopulateScaleGraduation();

            blcallinkprinter = true;
            isloadcomplete = true;


            blCheckGrocer = false;
            blCheckPriceSmart = false;

            if (cmbLanguage.SelectedIndex == 0) bInitialCulture = "en-US";
            if (cmbLanguage.SelectedIndex == 1) bInitialCulture = "es-ES";

            boolControlChanged = false;
            boolBookingExportFlagChanged = false;

            boolLoad = true;
        }

        private void BtnSetPOSFunctionBtn_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_POSFunctionButtonSetup frm = new POSSection.frm_POSFunctionButtonSetup();

            try
            {
                frm.FunctionBtnAccess = true; //blFunctionBtnAccess;
                frm.FunctionOrderChangeAccess = true; //blFunctionOrderChangeAccess;
                frm.SetPOSFunction = blSetPOSFunction;
                frm.ControlChanged = boolControlChanged;
                frm.ShowDialog();
                if (frm.DialogResult == true)
                {
                    blFunctionBtnAccess = frm.FunctionBtnAccess;
                    blFunctionOrderChangeAccess = frm.FunctionOrderChangeAccess;
                    blSetPOSFunction = frm.SetPOSFunction;
                    //boolControlChanged = frm.ControlChanged;
                    dtblPOSFunc = frm.grdFuncBtn.ItemsSource as DataTable;
                }
            }
            finally
            {
                frm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void TpOffice_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TpOthers_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TpDevices_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void TpPOS_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 3;
        }

        private void TpOnlineBooking_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 4;
        }

        private void TpSupport_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 5;
        }

        private void TpPrinter_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 6;
        }

        private void TpCOMPrinter_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 7;
        }

        private void TpGridSetup_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 8;
        }

        private void TpAccounting_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 9;
        }

        private void TpInternational_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 10;
        }

        private void TpIntegration_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 11;
        }

        private bool IsValidDefaultService()
        {
            string stype = "";
            string Salestype = "N";
            string Renttype = "N";
            string Repairtype = "N";

            if (cmbDefaultService.SelectedIndex == 0) stype = "Sales";
            if (cmbDefaultService.SelectedIndex == 1) stype = "Rent";
            if (cmbDefaultService.SelectedIndex == 2) stype = "Repair";

            if (chksrvsales.IsChecked == true) Salestype = "Y";
            if (chksrvrent.IsChecked == true) Renttype = "Y";
            if (chksrvrepair.IsChecked == true) Repairtype = "Y";

            if (stype == "Sales")
            {
                if (Salestype == "Y") return true; else return false;
            }
            else if (stype == "Rent")
            {
                if (Renttype == "Y") return true; else return false;
            }
            else
            {
                if (Repairtype == "Y") return true; else return false;
            }
        }
        
        private bool IsValidAll()
        {
            if (txtCompany.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Company);
                tcSetup.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtCompany);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtCompany); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }
            if (txtEmail.Text != "")
            {
                if (!GeneralFunctions.isEmail(txtEmail.Text))
                {
                    DocMessage.MsgEnter(Properties.Resources.Vaild_Email);
                    tcSetup.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtEmail);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtEmail); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }
            if (rglogo_1.IsChecked == true)
            {
                if (txtFixedWeb.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Web_page);
                    tcSetup.SelectedIndex = 0;
                    tabLogo.SelectedIndex = 1;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtFixedWeb);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtFixedWeb); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (cmbport.Visibility == Visibility.Visible)
            {
                if (cmbport.Text == "")
                {
                    DocMessage.MsgInformation(Properties.Resources.Select_Port);
                    tcSetup.SelectedIndex = 1;
                    if (OldTabIndex == 1)
                    {
                        GeneralFunctions.SetFocus(cmbport);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(cmbport); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }
            if (chkAutoSignout.IsChecked == true)
            {
                if (txtLoginTime.Text == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.sign_out_time);
                    tcSetup.SelectedIndex = 1;
                    if (OldTabIndex == 1)
                    {
                        GeneralFunctions.SetFocus(txtLoginTime);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtLoginTime); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if (GeneralFunctions.fnInt32(txtLoginTime.Text) <= 0)
                {
                    DocMessage.MsgEnter(Properties.Resources.sign_out_time);
                    tcSetup.SelectedIndex = 1;
                    if (OldTabIndex == 1)
                    {
                        GeneralFunctions.SetFocus(txtLoginTime);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtLoginTime); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (GeneralFunctions.fnInt32(numAlert.Text) <= 0)
            {
                DocMessage.MsgInformation("Invalid Product Expiry Alert Day");
                tcSetup.SelectedIndex = 3;
                if (OldTabIndex == 3)
                {
                    GeneralFunctions.SetFocus(numAlert);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(numAlert); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (chkPOS7.IsChecked == true)
            {
                if (rgPaymentGateway_0.IsChecked == true)
                {
                    if (pay1_accountId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Account_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay1_accountId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay1_accountId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                    if (pay1_applicationId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Application_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay1_applicationId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay1_applicationId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay1_acceptorId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Acceptor_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay1_acceptorId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay1_acceptorId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    /*if (pa.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Account Token");
                        tcSetup.SelectedIndex = 3;
                        GeneralFunctions.SetFocus(txtHPAccountToken);
                        return false;
                    }*/

                    if (pay1_terminal.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Terminal_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay1_terminal);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay1_terminal); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else if (rgPaymentGateway_1.IsChecked == true)
                {
                    if (pay2_merchantId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Merchant_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay2_merchantId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay2_merchantId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay2_userId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.User_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay2_userId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay2_userId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                }
                else if (rgPaymentGateway_2.IsChecked == true)
                {
                    if (pay3_clientMac.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Client_MAC);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay3_clientMac);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay3_clientMac); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                    if (pay3_posLynx.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Last_6_digits_of_POSLynx_MAC);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay3_posLynx);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay3_posLynx); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay3_posLynx.Text.Trim().Length < 6)
                    {
                        DocMessage.MsgEnter(Properties.Resources.Last_6_digits_of_POSLynx_MAC);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay3_posLynx);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay3_posLynx); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay3_portnumber.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Port_Number);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay3_portnumber);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay3_portnumber); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else if (rgPaymentGateway_4.IsChecked == true)
                {
                    if (pay5_server.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_Server);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay5_server);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay5_server); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay5_mid.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_MID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay5_mid);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay5_mid); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else if (rgPaymentGateway_5.IsChecked == true)
                {
                    if (pay6_serverIp.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_EMV_Server_IP);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(pay6_serverIp);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay6_serverIp); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay6_merchantId.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_EMV_Merchant_ID);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay6_merchantId);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay6_merchantId); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay6_securitydevice.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_EMV_Security_Device);
                        tcSetup.SelectedIndex = 3;

                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay6_securitydevice);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay6_securitydevice); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay6_comport.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Datacap_EMV_COM_Port);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay6_comport);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay6_comport); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else if (rgPaymentGateway_6.IsChecked == true)
                {
                    /*if (!File.Exists("commsetting.ini"))
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("commsetting.ini Missing", "frmGeneralSetupDlg_msg_commsettinginiMissing"));
                        return false;
                    }

                    String buf;
                    FileStream ini = new FileStream("commsetting.ini", FileMode.OpenOrCreate);
                    StreamReader fs = new StreamReader(ini);
                    int index;
                    String content = "";
                    String contentValue = "";
                    String ipPort = "";
                    String ip = "";
                    String serialPort = "";
                    String timeout = "";
                    String commType = "";
                    String baudRate = "";
                    int len;
                    buf = fs.ReadLine();
                    while (buf != null)
                    {
                        index = buf.IndexOf("=");
                        if (index == -1)
                        {
                            buf = fs.ReadLine();
                            continue;
                        }
                        content = buf.Substring(0, index);
                        len = buf.Length - index;
                        if (len == 1)
                        {
                            buf = fs.ReadLine();
                            continue;
                        }
                        contentValue = buf.Substring(index + 1, len - 1);
                        if (content == "PORT")
                        {
                            ipPort = contentValue.Trim();
                        }
                        else if (content == "IP")
                        {
                            ip = contentValue.Trim();
                        }
                        else if (content == "SERIALPORT")
                        {
                            serialPort = contentValue.Trim();
                        }
                        else if (content == "TIMEOUT")
                        {
                            timeout = contentValue.Trim();
                        }
                        else if (content == "CommType")
                        {
                            commType = contentValue.Trim();
                        }
                        else if (content == "BAUDRATE")
                        {
                            baudRate = contentValue.Trim();
                        }
                        buf = fs.ReadLine();
                    }

                    ini.Close();
                    fs.Close();
                    */
                    if (pay7_commtype.Text == "")
                    {
                        DocMessage.MsgInformation(Properties.Resources.Please_setup_POSLink_Communication_in_General_Settings);
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay7_commtype);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay7_commtype); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else if (rgPaymentGateway_7.IsChecked == true)
                {

                    if (pay8_XEConnectPath.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("External exe location");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay8_XEConnectPath);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay8_XEConnectPath); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay8_Api.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Api Base Address");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay8_Api);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay8_Api); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }


                }
                else if (rgPaymentGateway_8.IsChecked == true)
                {

                    if (pay9_psaccountname.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Account Name");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay9_psaccountname);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay9_psaccountname); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay9_psapikey.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Api key");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay9_psapikey);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay9_psapikey); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay9_pssoftwareid.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Software House Id");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay9_pssoftwareid);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay9_pssoftwareid); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay9_psinstallerid.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Installer Id");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay9_psinstallerid);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay9_psinstallerid); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }

                    if (pay9_psterminal.Text.Trim() == "")
                    {
                        DocMessage.MsgEnter("Terminal");
                        tcSetup.SelectedIndex = 3;
                        if (OldTabIndex == 3)
                        {
                            GeneralFunctions.SetFocus(pay9_psterminal);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(pay9_psterminal); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }


                }
                else
                {
                }
            }


            if ((!chksrvsales.IsChecked == true) && (!chksrvrent.IsChecked == true) && (!chksrvrepair.IsChecked == true))
            {
                DocMessage.MsgInformation(Properties.Resources.You_should_select_atleast_1_service);
                tcSetup.SelectedItem = tpPOS;
                if (OldTabIndex == 3)
                {
                    GeneralFunctions.SetFocus(chksrvsales);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(chksrvsales); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (!IsValidDefaultService())
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Default_Service);
                tcSetup.SelectedItem = tpPOS;
                if (OldTabIndex == 3)
                {
                    GeneralFunctions.SetFocus(cmbDefaultService);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(cmbDefaultService); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if ((chkAutoPO.IsChecked == true) && (blPrevPO != chkAutoPO.IsChecked == true ? true : false))
            {
                if (GeneralFunctions.GetRecordCount("POHeader") > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Purchase_Order_data_already_exists + " " + Properties.Resources.Auto_PO_Number_can_not_be_set);
                    chkAutoPO.IsChecked = false;
                    return false;
                }
            }

            if ((chkAutoCustomer.IsChecked == true) && (blPrevCustomer != chkAutoCustomer.IsChecked == true ? true : false))
            {
                if (GeneralFunctions.GetRecordCount("Customer") > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Customer_data_already_exists + " " + Properties.Resources.Auto_Customer_Number_can_not_be_set);
                    chkAutoCustomer.IsChecked = false;
                    return false;
                }
            }



            if ((chkAutoGC.IsChecked == true) && (blPrevGC != chkAutoGC.IsChecked == true ? true : false))
            {
                if (GeneralFunctions.GetRecordCountGC(Settings.CentralExportImport,Settings.StoreCode) > 0)
                {
                    DocMessage.MsgInformation("Gift Certificates already exists. Auto Gift Certificate Number can not be set.");
                    chkAutoGC.IsChecked = false;
                    return false;
                }
            }



            /*if (chkSecondMonitor.Checked)
            {
                if (txtSecondMonitorApp.Text.Trim() == "")
                {
                    if (!File.Exists(txtSecondMonitorApp.Text.Trim()))
                    {
                        DocMessage.MsgEnter(Translation.SetMultilingualTextInCodes("Application in Second Monitor.");
                        tcSetup.SelectedTabPage = tpOffice;
                        GeneralFunctions.SetFocus(txtSecondMonitorApp);
                        return false;
                    }
                }
            }*/

            if (txtSecondMonitorApp.Text.Trim() != "")
            {
                bool blfile = false;
                string fname = "";
                try
                {
                    FileInfo fi = new FileInfo(txtSecondMonitorApp.Text);
                    fname = fi.Name.Remove(fi.Name.Length - fi.Extension.Length);
                    blfile = true;
                }
                catch
                {
                    blfile = false;
                }
                if (blfile)
                {
                    if (fname.Contains("."))
                    {
                        DocMessage.MsgInformation(Properties.Resources._not_allowed_in_second_monitor_application_name);
                        tcSetup.SelectedItem = tpOffice;

                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(txtSecondMonitorApp);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtSecondMonitorApp); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                    if (fname.Contains(" "))
                    {
                        DocMessage.MsgInformation(Properties.Resources.space_not_allowed_in_second_monitor_application_name);
                        tcSetup.SelectedItem = tpOffice;

                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(txtSecondMonitorApp);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtSecondMonitorApp); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources.file_name_cannot_contain_any_of_the_following_characters + " : " + "\n\"/ \\ : * ? < > |");
                    tcSetup.SelectedItem = tpOffice;

                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtSecondMonitorApp);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtSecondMonitorApp); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (txtSecondMonitorApp.Text.Trim() != "")
            {
                if (!File.Exists(txtSecondMonitorApp.Text.Trim()))
                {
                    DocMessage.MsgInformation("Invalid application path in second monitor");
                    tcSetup.SelectedItem = tpOffice;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtSecondMonitorApp);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtSecondMonitorApp); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if ((GeneralFunctions.fnInt32(spnpurge.Value) >= 500) && (GeneralFunctions.fnInt32(spnpurge.Value) <= 5000))
            {
            }
            else
            {
                DocMessage.MsgInformation(Properties.Resources.Data_purging_days_should_be_between_500_5000);
                tcSetup.SelectedItem = tpSupport;
                GeneralFunctions.SetFocus(spnpurge);
                if (OldTabIndex == 5)
                {
                    GeneralFunctions.SetFocus(spnpurge);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(spnpurge); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }





            if (txtICountry.Text.Trim() == "")
            {
                //DocMessage.MsgEnter(Translation.SetMultilingualTextInCodes("Company", "frmGeneralSetupDlg_msg_Company"));

                DocMessage.MsgEnter(Properties.Resources.Country_Name);
                tcSetup.SelectedItem = tpInternational;

                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtICountry);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtICountry); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            /*
            if (txtICurrencySymbol.Text.Trim() == "")
            {

                DocMessage.MsgEnter(Properties.Resources.Currency_Symbol);
                tcSetup.SelectedItem = tpInternational;
                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtICurrencySymbol);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtICurrencySymbol); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }*/

            if (txtIDateFormat.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Date Format");
                tcSetup.SelectedItem = tpInternational;
                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtIDateFormat);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtIDateFormat); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }


            if (txtSmallCurrecyName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Coin_Unit_Name);
                tcSetup.SelectedItem = tpInternational;
                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtSmallCurrecyName);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtSmallCurrecyName); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (txtBigCurrecyName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Currency_Unit_Name);
                tcSetup.SelectedItem = tpInternational;
                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtBigCurrecyName);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtBigCurrecyName); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if ((txtCoin1.Text.Trim() == "") && (txtCoin2.Text.Trim() == "") && (txtCoin3.Text.Trim() == "") && (txtCoin4.Text.Trim() == "") && (txtCoin5.Text.Trim() == "") && (txtCoin6.Text.Trim() == "") && (txtCoin7.Text.Trim() == "")
                && (txtCurrency1.Text.Trim() == "") && (txtCurrency2.Text.Trim() == "") && (txtCurrency3.Text.Trim() == "") && (txtCurrency4.Text.Trim() == "")
                && (txtCurrency5.Text.Trim() == "") && (txtCurrency6.Text.Trim() == "") && (txtCurrency7.Text.Trim() == "") && (txtCurrency8.Text.Trim() == "")
                && (txtCurrency9.Text.Trim() == "") && (txtCurrency10.Text.Trim() == ""))
            {
                DocMessage.MsgInformation(Properties.Resources.No_CoinCurrency_Entered);
                tcSetup.SelectedItem = tpInternational;
                if (OldTabIndex == 11)
                {
                    GeneralFunctions.SetFocus(txtCoin1);
                }
                else
                {
                    Dispatcher.BeginInvoke(new System.Action(() => { GeneralFunctions.SetFocus(txtCoin1); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            int iqtcheck = 0;

            if (chkQuickTender1.IsChecked == true) iqtcheck++;
            if (chkQuickTender2.IsChecked == true) iqtcheck++;
            if (chkQuickTender3.IsChecked == true) iqtcheck++;
            if (chkQuickTender4.IsChecked == true) iqtcheck++;
            if (chkQuickTender5.IsChecked == true) iqtcheck++;
            if (chkQuickTender6.IsChecked == true) iqtcheck++;
            if (chkQuickTender7.IsChecked == true) iqtcheck++;
            if (chkQuickTender8.IsChecked == true) iqtcheck++;
            if (chkQuickTender9.IsChecked == true) iqtcheck++;
            if (chkQuickTender10.IsChecked == true) iqtcheck++;

            if (iqtcheck > 6)
            {
                DocMessage.MsgInformation(Properties.Resources.Maximum_6_Currency_types_are_allowed_for_quick_tendering);
                tcSetup.SelectedItem = tpInternational;
                return false;
            }

            return true;
        }

        #region Assign Local Settings

        // Element Payment Zip Processing
        private void SetElementPaymentZipProcessing()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Element Payment Zip Processing");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Element Payment Zip Processing";
            if (pay1_check.IsChecked == true) objLsetup.ParamValue = "Y"; else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Cash Drawer Code
        private void SetCashdrawerCode()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Cash Drawer Code");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Cash Drawer Code";
            objLsetup.ParamValue = txtDrawerCode.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Default Closeout Printer
        private void SetLocalDefaultCloseoutPrinter()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Default Closeout Printer");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Default Closeout Printer";
            if (chkDefCloseoutPrinter.IsChecked == true) objLsetup.ParamValue = "Receipt Printer";
            else objLsetup.ParamValue = "Report Printer";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Report Printer Name
        private void SetLocalReportPrinter()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Report Printer Name");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Report Printer Name";
            objLsetup.ParamValue = cmbReportPrinter.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Receipt Printer Name
        private void SetLocalReceiptPrinter()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Receipt Printer Name");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Receipt Printer Name";
            objLsetup.ParamValue = cmbReceiptPrinter.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Label Printer Type
        private void SetLocalLabelPrinterType()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Label Printer Type");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Label Printer Type";
            objLsetup.ParamValue = rgLabelPrinter_0.IsChecked == true ? "1" : "2";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Label Printer Name
        private void SetLocalLabelPrinter()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Label Printer Name");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Label Printer Name";
            if (rgLabelPrinter_0.IsChecked == true)
            {
                objLsetup.ParamValue = cmbLabelPrinter.EditValue == null ? "" : cmbLabelPrinter.Text.Trim();
            }
            else
            {
                objLsetup.ParamValue = cmbCOMPrinter.EditValue == null ? "" : cmbCOMPrinter.EditValue.ToString();
            }
            
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Line Display Device
        private void SetLineDisplayDevice()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Line Display Device");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Line Display Device";
            objLsetup.ParamValue = cmbLineDisplay.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        private void SetLineDisplaySerialDevice()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Line Display Serial Device");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Line Display Serial Device";
            objLsetup.ParamValue = cmbLineDisplaySP.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetLineDisplayDeviceType()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Line Display Device Type");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Line Display Device Type";
            objLsetup.ParamValue = rgPDType1.IsChecked == true ? "OPOS" : "SERIAL";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Element Payment Account ID
        private void SetElementPaymentAccountID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Element Payment Account ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Element Payment Account ID";
            objLsetup.ParamValue = pay1_accountId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Element Payment Account Token
        private void SetElementPaymentAccountToken()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Element Payment Account Token");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Element Payment Account Token";
            objLsetup.ParamValue = "0";//txtHPAccountToken.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Element Payment Application ID
        private void SetElementPaymentApplicationID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Element Payment Application ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Element Payment Application ID";
            objLsetup.ParamValue = pay1_applicationId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Element Payment Acceptor ID
        private void SetElementPaymentAcceptorID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Element Payment Acceptor ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Element Payment Acceptor ID";
            objLsetup.ParamValue = pay1_acceptorId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Mercury Payment Merchant ID
        private void SetMercuryPaymentMerchantID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Mercury Payment Merchant ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Mercury Payment Merchant ID";
            objLsetup.ParamValue = pay2_merchantId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Mercury Payment User ID
        private void SetMercuryPaymentUserID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Mercury Payment User ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Mercury Payment User ID";
            objLsetup.ParamValue = pay2_userId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Mercury Payment Port
        private void SetMercuryPaymentPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Mercury Payment Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Mercury Payment Port";
            objLsetup.ParamValue = pay2_pinpadport.Text.Trim() != "" ? pay2_pinpadport.Text.Trim().Remove(0, 3) : "";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Mercury Signature Amount
        private void SetMercurySignatureAmount()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Mercury Signature Amount");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Mercury Signature Amount";
            if (rgPaymentGateway_1.IsChecked == true)
                objLsetup.ParamValue = pay2_signatureAmt.Text;
            else
                objLsetup.ParamValue = "0";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Payment Gateway
        private void SetPaymentGateway()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Payment Gateway");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Payment Gateway";
            if (rgPaymentGateway_0.IsChecked == true) objLsetup.ParamValue = "1";
            if (rgPaymentGateway_1.IsChecked == true) objLsetup.ParamValue = "2";
            if (rgPaymentGateway_2.IsChecked == true) objLsetup.ParamValue = "3";
            if (rgPaymentGateway_3.IsChecked == true) objLsetup.ParamValue = "4";
            if (rgPaymentGateway_4.IsChecked == true) objLsetup.ParamValue = "5";
            if (rgPaymentGateway_5.IsChecked == true) objLsetup.ParamValue = "6";
            if (rgPaymentGateway_6.IsChecked == true) objLsetup.ParamValue = "7";
            if (rgPaymentGateway_7.IsChecked == true) objLsetup.ParamValue = "8";
            if (rgPaymentGateway_8.IsChecked == true) objLsetup.ParamValue = "9";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPaymentsenseTerminal()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Paymentsense Terminal");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Paymentsense Terminal";
            objLsetup.ParamValue = pay9_psterminal == null ? "" : pay9_psterminal.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetEVOConnectFile()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Evo Connect");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Evo Connect";
            objLsetup.ParamValue = pay8_XEConnectPath.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Payment Mode(Test/Live)
        private void SetPaymentMode()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Payment Mode(Test/Live)");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Payment Mode(Test/Live)";
            objLsetup.ParamValue = rgPaymentMode_0.IsChecked == true ? "0" : "1";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Use Kiss Retail as Card Payment Processor
        private void SetUsePaymentService()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Use Kiss Retail as Card Payment Processor");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Use Kiss Retail as Card Payment Processor";
            if (chkPOS7.IsChecked == true) objLsetup.ParamValue = "Y"; else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia ClientMAC
        private void SetPrecidiaClientMAC()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia ClientMAC");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia ClientMAC";
            objLsetup.ParamValue = pay3_clientMac.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia POSLynxMAC
        private void SetPrecidiaPOSLynxMAC()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia POSLynxMAC");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia POSLynxMAC";
            objLsetup.ParamValue = pay3_posLynx.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Port
        private void SetPrecidiaPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Port";
            objLsetup.ParamValue = pay3_portnumber.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Use PIN Pad
        private void SetPrecidiaUsePINPad()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Use PIN Pad");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Use PIN Pad";
            objLsetup.ParamValue = pay3_usepinpad.IsChecked == true ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Lane Open
        private void SetPrecidiaLaneOpen()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Lane Open");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Lane Open";
            objLsetup.ParamValue = "N"; //chkPrecidiaOpenTerminal.Checked ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Add Log
        private void SetPrecidiaRRLog()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Add Log");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Add Log";
            objLsetup.ParamValue = "N"; //chkPrecidiaRRLog.Checked ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Signature
        private void SetPrecidiaSignature()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Signature");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Signature";
            /*if ((rgPaymentGateway.SelectedIndex == 2) && (chkPrecidiaPINpad.Checked))
                objLsetup.ParamValue = chkPrecidiaSign.Checked ? "Y" : "N";
            else
                objLsetup.ParamValue = "N";*/
            objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Precidia Signature Amount
        private void SetPrecidiaSignatureAmount()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Precidia Signature Amount");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Precidia Signature Amount";
            if ((rgPaymentGateway_2.IsChecked == true) && (pay3_usepinpad.IsChecked == true))
                objLsetup.ParamValue = "0";
            else
                objLsetup.ParamValue = "0";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Payment Gateway Terminal ID
        private void SetPaymentGatewayTerminalID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Payment Gateway Terminal ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Payment Gateway Terminal ID";
            objLsetup.ParamValue = pay1_terminal.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkCommType()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Comm Type");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Comm Type";
            objLsetup.ParamValue = pay7_commtype.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkSerialPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Serial Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Serial Port";
            objLsetup.ParamValue = pay7_serialport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkDestIP()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Dest IP");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Dest IP";
            objLsetup.ParamValue = pay7_destIp.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkDestPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Dest Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Dest Port";
            objLsetup.ParamValue = pay7_destport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkBaudRate()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Baud Rate");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Baud Rate";
            objLsetup.ParamValue = pay7_baud.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        private void SetPOSLinkTimeout()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "POSLink Timeout");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "POSLink Timeout";
            objLsetup.ParamValue = pay7_timeout.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap Server or IP 
        private void SetDatacapServer()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Server");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Server";
            objLsetup.ParamValue = pay5_server.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap MID
        private void SetDatacapMID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap MID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap MID";
            objLsetup.ParamValue = pay5_mid.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap Sercure Device ID
        private void SetDatacapSecureDeviceID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Secure Device ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Secure Device ID";
            objLsetup.ParamValue = pay5_securedevice.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap COM Port
        private void SetDatacapCOMPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap COM Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap COM Port";
            objLsetup.ParamValue = pay5_comport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap PIN Pad Type
        private void SetDatacapPINPadType()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap PIN Pad Type");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap PIN Pad Type";
            objLsetup.ParamValue = pay5_pinpadtype.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap Signature Amount
        private void SetDatacapSignatureAmount()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Signature Amount");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Signature Amount";
            objLsetup.ParamValue = pay5_signatureAmt.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Card Entry Mode (Swipe/Manual)
        private void SetDatacapCardEntryMode()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Card Entry Mode");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Card Entry Mode";
            objLsetup.ParamValue = rgCardEntryMode_0.IsChecked == true ? "0" : "1";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap Signature Sercure Device ID
        private void SetDatacapSignatureSecureDeviceID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Signature Device");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Signature Device";
            objLsetup.ParamValue = pay5_signaturedevice.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap Signature Device COM Port
        private void SetDatacapSignatureDeviceCOMPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datacap Signature Device COM Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datacap Signature Device COM Port";
            objLsetup.ParamValue = pay5_signaturecomport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        // Datacap EMV Server IP 
        private void SetDatacapEMVServerIP()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Server IP");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Server IP";
            objLsetup.ParamValue = pay6_serverIp.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV Server Port 
        private void SetDatacapEMVServerPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Server Port");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Server Port";
            objLsetup.ParamValue = pay6_serverport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV MID 
        private void SetDatacapEMVMID()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Merchant ID");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Merchant ID";
            objLsetup.ParamValue = pay6_merchantId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV Security Device 
        private void SetDatacapEMVSecurityDevice()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Security Device");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Security Device";
            objLsetup.ParamValue = pay6_securitydevice.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV Security Device's COM Port
        private void SetDatacapEMVCOMPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV COM");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV COM";
            objLsetup.ParamValue = pay6_comport.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV User Trace
        private void SetDatacapEMVUserTrace()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV User Trace");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV User Trace";
            objLsetup.ParamValue = pay6_usertrace.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        // Datacap EMV Terminal
        private void SetDatacapEMVTerminal()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Terminal");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Terminal";
            objLsetup.ParamValue = pay6_terminalId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV Operator
        private void SetDatacapEMVOperator()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Operator");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Operator";
            objLsetup.ParamValue = pay6_operatorId.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        // Datacap EMV Card Entry
        private void SetDatacapEMVManual()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Manual");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Manual";
            objLsetup.ParamValue = pay6_manualEntry.IsChecked == true ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Datacap EMV Token Request
        private void SetDatacapEMVToken()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DatacapEMV Token");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DatacapEMV Token";
            objLsetup.ParamValue = pay6_tokenReq.IsChecked == true ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        // Browse Permission
        private void SetBrowsePermission()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Browse Permission");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Browse Permission";
            if (rgwebsec_0.IsChecked == true) objLsetup.ParamValue = "F";
            else if (rgwebsec_1.IsChecked == true) objLsetup.ParamValue = "A";
            else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Run Second Monitor Application
        private void SetSecondMonitorCheck()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Run Second Monitor Application");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Run Second Monitor Application";
            if (chkSecondMonitor.IsChecked == true) objLsetup.ParamValue = "Y";
            else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Active Second Monitor with POS
        private void SetSecondMonitorWithPOS()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Active Second Monitor with POS");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Active Second Monitor with POS";
            if (chkSMWithPOS.IsChecked == true) objLsetup.ParamValue = "Y";
            else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Second Monitor Application Path
        private void SetSecondMonitorAppPath()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Second Monitor Application Path");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Second Monitor Application Path";
            objLsetup.ParamValue = txtSecondMonitorApp.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Display Area
        private void SetAdvtDisplayArea()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Display Area");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Display Area";
            objLsetup.ParamValue = strAdvtDispArea;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Display Area - Scale
        private void SetAdvtDisplayAreaScale()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Display - Scale");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Display - Scale";
            objLsetup.ParamValue = strAdvtScale;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Count
        private void SetAdvtDisplayCount()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Count");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Count";
            objLsetup.ParamValue = strAdvtDispNo;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Display Time
        private void SetAdvtDisplayTime()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Display Time");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Display Time";
            objLsetup.ParamValue = strAdvtDispTime;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Display Order
        private void SetAdvtDisplayOrder()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Display Order");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Display Order";
            objLsetup.ParamValue = strAdvtDispOrder;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Font Size
        private void SetAdvtFontSize()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Font Size");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Font Size";
            objLsetup.ParamValue = strAdvtFont;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Background Color
        private void SetAdvtColor()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Background Color");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Background Color";
            objLsetup.ParamValue = strAdvtColor;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Background Image
        private void SetAdvtBackground()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Background Image");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Background Image";
            objLsetup.ParamValue = strAdvtBackground;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Advertisement Directory
        private void SetAdvtDir()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Advertisement Directory");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Advertisement Directory";
            objLsetup.ParamValue = strAdvtDir;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Scale Device
        private void SetScaleDevice()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Device");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Device";
            if (cmbScaleType.SelectedIndex == 0)
            {
                objLsetup.ParamValue = "XEPOS";
            }
            else if (cmbScaleType.SelectedIndex == 1)
            {
                objLsetup.ParamValue = "Datalogic Scale";
            }
            else if (cmbScaleType.SelectedIndex == 2)
            {
                objLsetup.ParamValue = "Live Weight";
            }
            else if (cmbScaleType.SelectedIndex == 3)
            {
                objLsetup.ParamValue = "(None)";
            }
            else
            {
                objLsetup.ParamValue = "";
            }

            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }


        // Scale Category
        private void SetScaleCategory()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Category");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Category";
            if (cmbScaleType.SelectedIndex == 2)
            {
                if (cmbGRS.SelectedIndex == 0)
                {
                    objLsetup.ParamValue = "GRS 60";
                }
                else
                {
                    objLsetup.ParamValue = "GRS 100";
                }
            }
            else
            {
                objLsetup.ParamValue = "NA";
            }

            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }



        // Scale Port Setup - COMPort
        private void SetScaleCOMPort()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - COMPort");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - COMPort";
            objLsetup.ParamValue = cmbport.Text;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - BaudRate
        private void SetScaleBaudRate()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - BaudRate");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - BaudRate";
            objLsetup.ParamValue = pBaudRate;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - DataBits
        private void SetScaleDataBits()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - DataBits");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - DataBits";
            objLsetup.ParamValue = pDataBits;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - StopBits
        private void SetScaleStopBits()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - StopBits");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - StopBits";
            objLsetup.ParamValue = pStopBits;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - Parity
        private void SetScaleParity()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - Parity");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - Parity";
            objLsetup.ParamValue = pParity;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - HandShake
        private void SetScaleHandShake()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - HandShake");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - HandShake";
            objLsetup.ParamValue = pHandshake;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Scale Port Setup - Timeout
        private void SetScaleTimeout()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Scale Port Setup - Timeout");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Scale Port Setup - Timeout";
            objLsetup.ParamValue = pTimeout;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Item Grid Setup - Visible
        private void SetItemGridDimension1(string itemvisible)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Item Grid Setup - Visible");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Item Grid Setup - Visible";
            objLsetup.ParamValue = itemvisible;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Item Grid Setup - Width
        private void SetItemGridDimension2(string itemwidth)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Item Grid Setup - Width");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Item Grid Setup - Width";
            objLsetup.ParamValue = itemwidth;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Item Vendor Grid Setup - Visible
        private void SetItemVendorGridDimension1(string itemvisible)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Item Vendor Grid Setup - Visible");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Item Vendor Grid Setup - Visible";
            objLsetup.ParamValue = itemvisible;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Item Vendor Grid Setup - Width
        private void SetItemVendorGridDimension2(string itemwidth)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Item Vendor Grid Setup - Width");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Item Vendor Grid Setup - Width";
            objLsetup.ParamValue = itemwidth;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Customer Grid Setup - Visible
        private void SetCustomerGridDimension1(string custvisible)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Customer Grid Setup - Visible");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Customer Grid Setup - Visible";
            objLsetup.ParamValue = custvisible;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Customer Grid Setup - Width
        private void SetCustomerGridDimension2(string custwidth)
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Customer Grid Setup - Width");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Customer Grid Setup - Width";
            objLsetup.ParamValue = custwidth;
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Auto Signout After Tender
        private void SetAutoSignoutAfterTender()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Auto Signout After Tender");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Auto Signout After Tender";
            if (chkAutoSignoutAfterTender.IsChecked == true) objLsetup.ParamValue = "Y"; else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Auto Signout
        private void SetAutoSignout()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Auto Signout");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Auto Signout";
            if (chkAutoSignout.IsChecked == true) objLsetup.ParamValue = "Y"; else objLsetup.ParamValue = "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Auto Signout Time
        private void SetAutoSignoutTime()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Auto Signout Time");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Auto Signout Time";
            objLsetup.ParamValue = txtLoginTime.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }

        // Customer Required On Rental
        private void SetCustomerRequiredOnRental()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Customer Required On Rental");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Customer Required On Rental";
            objLsetup.ParamValue = (chksrvrent.IsChecked == true) ? ((chkcustomeronrent.IsChecked == true) ? "Y" : "N") : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Rent Calculate Later
        private void SetRentCalculateLater()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Rent Calculate Later");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Rent Calculate Later";
            objLsetup.ParamValue = (chksrvrent.IsChecked == true) ? ((chkrentcalclater.IsChecked == true) ? "Y" : "N") : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // SKU on POS Button
        private void SetShowSKUonPOSButton()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "SKU on POS Button");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "SKU on POS Button";
            objLsetup.ParamValue = (chkPOS20.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Receipt Print Directly
        private void SetDirectReceiptPrintPOSButton()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Receipt Print Directly");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Receipt Print Directly";
            objLsetup.ParamValue = "N";//(chkPOS50.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Auto Display Product Image while Add to Cart
        private void SetAutoDisplayItemImage()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Auto Display Product Image while Add to Cart");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Auto Display Product Image while Add to Cart";
            objLsetup.ParamValue = (chkPOS87.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Display Lane Closed
        private void SetDisplayLaneClosed()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Display Lane Closed");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Display Lane Closed";
            objLsetup.ParamValue = (chkPOS120.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Close Category After Ringing
        private void SetCloseCategoryAfterRinging()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Close Category After Ringing");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Close Category After Ringing";
            objLsetup.ParamValue = (chkPOS90.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // DO not Read Scale Bar Code check Digits
        private void SetNotReadScaleBarcodeCheck()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "DO not Read Scale Bar Code check Digits");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "DO not Read Scale Bar Code check Digits";
            objLsetup.ParamValue = (chkNotReadBarCodeCheck.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        private void SetRandomWeightUPCCheckDigit()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Random Weight UPC Check Digit");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Random Weight UPC Check Digit";
            if (chkBCF1.IsChecked == true) objLsetup.ParamValue = "2";
            if (chkBCF2.IsChecked == true) objLsetup.ParamValue = "20";
            if (chkBCF3.IsChecked == true) objLsetup.ParamValue = "02";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }

        // Datalogic Scanner Model 8200 Exists
        private void Set8200ScannerCheck()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Datalogic Scanner Model 8200");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Datalogic Scanner Model 8200";
            objLsetup.ParamValue = (chk8200Scanner.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }


        private void SetNTEPCert()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "NTEP Cert");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "NTEP Cert";
            objLsetup.ParamValue = cmbScaleType.SelectedIndex == 3 ? "" : txtNTEP.Text.Trim();
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }
        }


        // Display 4 Digit Decimal Weight in Scale Module
        private void Set4DigitWeightInScale()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Display 4 Digit Decimal Weight");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Display 4 Digit Decimal Weight";
            objLsetup.ParamValue = (chkDisplay4DigitWeight.IsChecked == true) ? "Y" : "N";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0) strret = objLsetup.InsertParamData(); else strret = objLsetup.UpdateParamData();
        }


        private void SetLocalLanguage()
        {
            string strret = "";
            string pHostName = GeneralFunctions.GetHostName();
            int intCount = GeneralFunctions.IsExistLocalSetupData(pHostName, "Language");
            PosDataObject.LocalSetup objLsetup = new PosDataObject.LocalSetup();
            objLsetup.Connection = SystemVariables.Conn;
            objLsetup.HostName = pHostName;
            objLsetup.ParamName = "Language";
            if (cmbLanguage.SelectedIndex == 0) objLsetup.ParamValue = "en-US";
            if (cmbLanguage.SelectedIndex == 1) objLsetup.ParamValue = "es-ES";
            objLsetup.LoginUserID = SystemVariables.CurrentUserID;
            if (intCount == 0)
            {
                strret = objLsetup.InsertParamData();
            }
            else
            {
                strret = objLsetup.UpdateParamData();
            }

        }


        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    if ((boolChangeStyle) && (SystemVariables.SelectedTheme != cmbstyle.Text))
                    {
                        ConfigSettings.RemoveTheme();
                        ConfigSettings.AddTheme(cmbstyle.Text);
                        SystemVariables.SelectedTheme = cmbstyle.Text;
                        new MessageBoxWindow().Show("Please restart the application for better visuals", "Theme Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (boolChangeStyle) boolIsThemeCHanged = true;
                    Settings.ReportPrinterName = cmbReportPrinter.Text;
                    Settings.ReceiptPrinterName = cmbReceiptPrinter.Text;
                    Settings.LabelPrinterName = cmbLabelPrinter.Text;
                    SetLocalDefaultCloseoutPrinter();
                    SetLocalReportPrinter();
                    SetLocalReceiptPrinter();
                    SetLocalLabelPrinterType();
                    SetLocalLabelPrinter();
                    SetCashdrawerCode();
                    SetLineDisplayDeviceType();
                    SetLineDisplayDevice();
                    SetLineDisplaySerialDevice();

                    SetUsePaymentService();
                    SetElementPaymentAccountID();
                    SetElementPaymentAccountToken();
                    SetElementPaymentApplicationID();
                    SetElementPaymentAcceptorID();
                    SetElementPaymentZipProcessing();
                    SetMercuryPaymentMerchantID();
                    SetMercuryPaymentUserID();
                    SetMercuryPaymentPort();
                    SetMercurySignatureAmount();
                    SetEVOConnectFile();
                    SetPaymentsenseTerminal();
                    SetPaymentGateway();
                    SetPaymentMode();
                    SetPrecidiaClientMAC();
                    SetPrecidiaPOSLynxMAC();
                    SetPrecidiaPort();
                    SetPrecidiaUsePINPad();
                    SetPrecidiaLaneOpen();
                    SetPrecidiaRRLog();
                    SetPrecidiaSignature();
                    SetPrecidiaSignatureAmount();
                    SetDatacapServer();
                    SetDatacapMID();
                    SetDatacapSecureDeviceID();
                    SetDatacapCOMPort();
                    SetDatacapPINPadType();
                    SetDatacapSignatureAmount();
                    SetDatacapSignatureSecureDeviceID();
                    SetDatacapSignatureDeviceCOMPort();
                    SetDatacapCardEntryMode();

                    SetDatacapEMVCOMPort();
                    SetDatacapEMVManual();
                    SetDatacapEMVToken();
                    SetDatacapEMVMID();
                    SetDatacapEMVSecurityDevice();
                    SetDatacapEMVServerIP();
                    SetDatacapEMVServerPort();
                    SetDatacapEMVTerminal();
                    SetDatacapEMVOperator();
                    SetDatacapEMVUserTrace();

                    SetPOSLinkCommType();
                    SetPOSLinkSerialPort();
                    SetPOSLinkDestPort();
                    SetPOSLinkDestIP();
                    SetPOSLinkBaudRate();
                    SetPOSLinkTimeout();

                    SetPaymentGatewayTerminalID();
                    SetBrowsePermission();
                    SetSecondMonitorCheck();
                    SetSecondMonitorWithPOS();
                    SetSecondMonitorAppPath();
                    SetAdvtDisplayArea();
                    SetAdvtDisplayAreaScale();
                    SetAdvtDisplayCount();
                    SetAdvtDisplayTime();
                    SetAdvtDisplayOrder();
                    SetAdvtFontSize();
                    SetAdvtColor();
                    SetAdvtBackground();
                    SetAdvtDir();

                    SetScaleDevice();
                    SetScaleCategory();
                    SetScaleCOMPort();
                    SetScaleBaudRate();
                    SetScaleDataBits();
                    SetScaleStopBits();
                    SetScaleParity();
                    SetScaleHandShake();
                    SetScaleTimeout();

                    //SetGridDimensions();

                    SetAutoSignout();
                    SetAutoSignoutAfterTender();
                    SetAutoSignoutTime();

                    SetCustomerRequiredOnRental();
                    SetRentCalculateLater();
                    SetShowSKUonPOSButton();
                    SetDirectReceiptPrintPOSButton();
                    SetAutoDisplayItemImage();
                    SetDisplayLaneClosed();
                    SetCloseCategoryAfterRinging();
                    SetNotReadScaleBarcodeCheck();
                    SetRandomWeightUPCCheckDigit();
                    Set8200ScannerCheck();


                    SetNTEPCert();
                    Set4DigitWeightInScale();
                    SetLocalLanguage();



                    Settings.LoadSettingsVariables();
                    Settings.LoadShopifySettings();

                    if (Settings.DefautInternational == "N")
                        GeneralFunctions.SetCurrencyNDateformat();
                    Settings.LoadScaleGraduation();
                    Settings.LoadStoreInfoVariables();
                    Settings.LoadCentralStoreInfo();
                    Settings.GetLocalSetup();
                    Settings.GetLanguage();
                    Settings.LoadMainHeader();
                    boolControlChanged = false;
                    if (Settings.POSDisplayProductImage != PrevProductImgDisplay) blChangeProductImgDisplay = true;

                    if ((chkSecondMonitor.IsChecked == true) && (!bActivateSecondMonitor))
                    {
                        DocMessage.MsgInformation("Restart" + " " + SystemVariables.BrandName + " " + "to receive outputs to second monitor");
                    }

                    string currCulture = "";
                    if (cmbLanguage.SelectedIndex == 0) currCulture = "en-US";
                    if (cmbLanguage.SelectedIndex == 1) currCulture = "es-ES";

                    if (bInitialCulture != currCulture)
                    {
                        DocMessage.MsgInformation("Language changes will be effective when you restart the application.");
                    }
                    boolControlChanged = false;
                    //blCloseout = false;
                    DialogResult = true;
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((boolChangeStyle) && (SystemVariables.SelectedTheme != cmbstyle.Text))
            {
                ResetTheme();
            }
            if (boolChangeStyle) boolIsThemeCHanged = true;
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            if ((boolChangeStyle) && (SystemVariables.SelectedTheme != cmbstyle.Text))
                            {
                                ConfigSettings.RemoveTheme();
                                ConfigSettings.AddTheme(cmbstyle.Text);
                                SystemVariables.SelectedTheme = cmbstyle.Text;
                            }
                            if (boolChangeStyle) boolIsThemeCHanged = true;
                            Settings.ReportPrinterName = cmbReportPrinter.Text;
                            Settings.ReceiptPrinterName = cmbReceiptPrinter.Text;
                            Settings.LabelPrinterName = cmbLabelPrinter.Text;

                            SetLocalDefaultCloseoutPrinter();
                            SetLocalReportPrinter();
                            SetLocalReceiptPrinter();
                            SetLocalLabelPrinterType();
                            SetLocalLabelPrinter();
                            SetCashdrawerCode();
                            SetLineDisplayDeviceType();
                            SetLineDisplayDevice();
                            SetLineDisplaySerialDevice();

                            SetUsePaymentService();
                            SetElementPaymentAccountID();
                            SetElementPaymentAccountToken();
                            SetElementPaymentApplicationID();
                            SetElementPaymentAcceptorID();
                            SetElementPaymentZipProcessing();
                            SetMercuryPaymentMerchantID();
                            SetMercuryPaymentUserID();
                            SetMercuryPaymentPort();
                            SetMercurySignatureAmount();
                            SetEVOConnectFile();
                            SetPaymentsenseTerminal();
                            SetPaymentGateway();
                            SetPaymentMode();
                            SetPrecidiaClientMAC();
                            SetPrecidiaPOSLynxMAC();
                            SetPrecidiaPort();
                            SetPrecidiaUsePINPad();
                            SetPrecidiaLaneOpen();
                            SetPrecidiaRRLog();
                            SetPrecidiaSignature();
                            SetPrecidiaSignatureAmount();
                            SetDatacapServer();
                            SetDatacapMID();
                            SetDatacapSecureDeviceID();
                            SetDatacapCOMPort();
                            SetDatacapPINPadType();
                            SetDatacapSignatureAmount();
                            SetDatacapSignatureSecureDeviceID();
                            SetDatacapSignatureDeviceCOMPort();
                            SetDatacapCardEntryMode();

                            SetDatacapEMVCOMPort();
                            SetDatacapEMVManual();
                            SetDatacapEMVToken();
                            SetDatacapEMVMID();
                            SetDatacapEMVSecurityDevice();
                            SetDatacapEMVServerIP();
                            SetDatacapEMVServerPort();
                            SetDatacapEMVTerminal();
                            SetDatacapEMVOperator();
                            SetDatacapEMVUserTrace();

                            SetPOSLinkCommType();
                            SetPOSLinkSerialPort();
                            SetPOSLinkDestPort();
                            SetPOSLinkDestIP();
                            SetPOSLinkBaudRate();
                            SetPOSLinkTimeout();

                            SetPaymentGatewayTerminalID();
                            SetBrowsePermission();
                            SetSecondMonitorCheck();
                            SetSecondMonitorWithPOS();
                            SetSecondMonitorAppPath();
                            SetAdvtDisplayArea();
                            SetAdvtDisplayAreaScale();
                            SetAdvtDisplayCount();
                            SetAdvtDisplayTime();
                            SetAdvtDisplayOrder();
                            SetAdvtFontSize();
                            SetAdvtColor();
                            SetAdvtBackground();
                            SetAdvtDir();

                            SetScaleDevice();
                            SetScaleCategory();
                            SetScaleCOMPort();
                            SetScaleBaudRate();
                            SetScaleDataBits();
                            SetScaleStopBits();
                            SetScaleParity();
                            SetScaleHandShake();
                            SetScaleTimeout();
                            SetAutoDisplayItemImage();

                            //SetGridDimensions();

                            SetAutoSignout();
                            SetAutoSignoutAfterTender();
                            SetAutoSignoutTime();

                            SetCustomerRequiredOnRental();
                            SetRentCalculateLater();
                            SetShowSKUonPOSButton();
                            SetDirectReceiptPrintPOSButton();

                            SetDisplayLaneClosed();
                            SetCloseCategoryAfterRinging();
                            SetNotReadScaleBarcodeCheck();
                            SetRandomWeightUPCCheckDigit();
                            Set8200ScannerCheck();

                            SetNTEPCert();
                            Set4DigitWeightInScale();
                            SetLocalLanguage();

                            Settings.LoadSettingsVariables();
                            Settings.LoadShopifySettings();
                            if (Settings.DefautInternational == "N")
                                GeneralFunctions.SetCurrencyNDateformat();
                            Settings.LoadScaleGraduation();
                            Settings.LoadStoreInfoVariables();
                            Settings.LoadCentralStoreInfo();
                            Settings.GetLocalSetup();
                            Settings.GetLanguage();
                            Settings.LoadMainHeader();

                            boolControlChanged = false;
                            //blCloseout = false;
                            if (Settings.POSDisplayProductImage != PrevProductImgDisplay) blChangeProductImgDisplay = true;

                            if ((chkSecondMonitor.IsChecked == true) && (!bActivateSecondMonitor))
                            {
                                DocMessage.MsgInformation("Restart" + " " + SystemVariables.BrandName + " " + "to receive outputs to second monitor");
                            }

                        }
                        else
                        { e.Cancel = true; }
                    }
                    else
                        e.Cancel = true;
                }
                if (DlgResult == MessageBoxResult.No)
                {
                    if ((boolChangeStyle) && (SystemVariables.SelectedTheme != cmbstyle.Text))
                    {
                        ResetTheme();
                    }
                    //blCloseout = false;
                }
                if (DlgResult == MessageBoxResult.Cancel) e.Cancel = true;

                
            }
        }


        

        private void GrdCustomer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TableViewHitInfo hi = ((TableView)grdCustomer.View).CalcHitInfo(e.OriginalSource as DependencyObject);
                //textBlock.Text = hi.HitTest.ToString();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigate_1(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void TxtPhone_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void PopupContainerEdit1_Click(object sender, RoutedEventArgs e)
        {
            
            flyoutControl.PlacementTarget = sender as FrameworkElement;
            flyoutControl.Content = "Following lines will be printed as Receipt Header:" + "\r\n" + Settings.MainReceiptHeader + memReceiptH.Text;
            
            flyoutControl.IsOpen = true;
        }

        private void ChkSecondMonitor_Checked(object sender, RoutedEventArgs e)
        {
            chkSMWithPOS.Visibility = chkSecondMonitor.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            boolControlChanged = true;
        }

        private void RgCustInfo_0_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkPrintBlindDrop_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TcSetup_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {

        }

        private void RearrangeCOMPrinterLookup()
        {
            string prevvalue = cmbCOMPrinter.EditValue.ToString();
            PopulateCOMPrinter();
            try
            {
                cmbCOMPrinter.EditValue = prevvalue;
            }
            catch
            {
            }
        }

        private void BarButtonItem4_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_COMPrinterDlg fcom = new frm_COMPrinterDlg();
            try
            {
                fcom.ID = 0;
                fcom.BrwF = this;
                fcom.ShowDialog();
                if (fcom.DialogResult == true)
                {
                    FetchPortCommand();
                    boolControlChanged = true;
                    RearrangeCOMPrinterLookup();
                }
            }
            finally
            {
                fcom.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem5_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView7.FocusedRowHandle;
            if ((grdCOMprinter.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_COMPrinterDlg frm_PrinterNameDlg = new frm_COMPrinterDlg();
            try
            {
                frm_PrinterNameDlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdCOMprinter, colcomid));
                if (frm_PrinterNameDlg.ID > 0)
                {
                    frm_PrinterNameDlg.BrwF = this;
                    frm_PrinterNameDlg.ShowDialog();
                    intNewRecID = frm_PrinterNameDlg.ID;
                    if (frm_PrinterNameDlg.DialogResult == true)
                    {
                        boolControlChanged = true;
                        RearrangeCOMPrinterLookup();
                    }
                }
            }
            finally
            {
                frm_PrinterNameDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void GridView7_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView7.FocusedRowHandle;
            if ((grdCOMprinter.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_COMPrinterDlg frm_PrinterNameDlg = new frm_COMPrinterDlg();
            try
            {
                frm_PrinterNameDlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdCOMprinter, colcomid));
                if (frm_PrinterNameDlg.ID > 0)
                {
                    frm_PrinterNameDlg.BrwF = this;
                    frm_PrinterNameDlg.ShowDialog();
                    intNewRecID = frm_PrinterNameDlg.ID;
                    if (frm_PrinterNameDlg.DialogResult == true)
                    {
                        boolControlChanged = true;
                        RearrangeCOMPrinterLookup();
                    }
                }
            }
            finally
            {
                frm_PrinterNameDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BarButtonItem6_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView7.FocusedRowHandle;
            if ((grdCOMprinter.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdCOMprinter, colcomid));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("COM Printer"))
                {
                    if (DocMessage.MsgConfirmation("All the terminal settings against this printer will be deleted." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
                    {
                        PosDataObject.Setup objCustomer = new PosDataObject.Setup();
                        objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        int intrError = objCustomer.DeleteCOMPrinterCommand(intRecdID);
                        if (intrError == 0)
                        {
                            FetchPortCommand();
                            RearrangeCOMPrinterLookup();
                            boolControlChanged = true;
                        }

                    }
                }
            }
        }

        private void CmbLanguage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void PART_Editor_LinkPrinter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void GrdPrinter2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdPrinter2.CurrentColumn.Name == "colPL3")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdPrinter2.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }

            if (grdPrinter2.CurrentColumn.Name == "colPL4")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdPrinter2.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }





        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new System.Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }






        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }




        private void Full_GotFocusPswd(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }


        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (bCallFromAdmin ? Settings.UseTouchKeyboardInAdmin == "N" : Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Cmbstyle_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (boolLoad)
            {
                boolChangeStyle = true;

                if (cmbstyle.Text == "Dark")
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

                if (cmbstyle.Text == "Light")
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
        }

        private void ResetTheme()
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

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select External Connection File";
            op.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            op.FileName = "";
            op.CheckFileExists = false;
            op.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (op.ShowDialog() == true)
            {
                string strFilename = op.FileName;
                pay8_XEConnectPath.Text = strFilename;
            }
            blurGrid.Visibility = Visibility.Collapsed;
        }

        private void Cmbport_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void rgPDType1_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (rgPDType1.IsChecked == true)
            {
                lbPD.Text = "Display Device";
                cmbLineDisplay.Visibility = Visibility.Visible;
                cmbLineDisplaySP.Visibility = Visibility.Collapsed;
                btnRefresh.Visibility = Visibility.Visible;
            }
            else
            {
                lbPD.Text = "Display Port";
                cmbLineDisplay.Visibility = Visibility.Collapsed;
                cmbLineDisplaySP.Visibility = Visibility.Visible;
                btnRefresh.Visibility = Visibility.Collapsed;
            }
        }

        private void MnuShopify_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mnuShopify.Background = this.FindResource("NavGroupSelected") as Brush;
            
            mnuWooQuickBooks.Background = this.FindResource("NavGroup") as Brush;
            mnuWooCommerce.Background = this.FindResource("NavGroup") as Brush;
            mnuXero.Background = this.FindResource("NavGroup") as Brush;

            mnuNav2013.Background = this.FindResource("NavGroup") as Brush;

            mnuShopify.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
            
            mnuWooQuickBooks.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuXero.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooCommerce.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuNav2013.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            pnlShopify.Visibility = Visibility.Visible;
            
            pnlQuickBooks.Visibility = Visibility.Collapsed;
            pnlXero.Visibility = Visibility.Collapsed;
            pnlWooCommerce.Visibility = Visibility.Collapsed;
            pnlNAV2013.Visibility = Visibility.Collapsed;
        }

        private void MnuWooQuickBooks_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mnuShopify.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuNav2013.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooQuickBooks.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
            mnuXero.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooCommerce.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            pnlShopify.Visibility = Visibility.Collapsed;
            
            pnlQuickBooks.Visibility = Visibility.Visible;
            pnlXero.Visibility = Visibility.Collapsed;
            pnlWooCommerce.Visibility = Visibility.Collapsed;
            pnlNAV2013.Visibility = Visibility.Collapsed;
        }

        private void btnBrowseQB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select File";
            op.Filter = "qbw files (*.qbw)|*.qbw|All files (*.*)|*.*";
            op.DefaultExt = "qbw";
            op.CheckFileExists = false;
            op.FilterIndex = 1;

            if (op.ShowDialog() == true)
            {
                string strFilename = "";
                int intIndex = 0;
                strFilename = op.FileName;
                intIndex = strFilename.IndexOf(".");
                if (intIndex <= 0)
                    strFilename = strFilename + ".qbw";
                txtQuickBookCompanyPath.Text = strFilename;
            }
        }

        private void chkRepeatQuickBooks_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRepeatQuickBooks.IsChecked == true)
            {
                rep61.Visibility = Visibility.Visible;
                rep62.Visibility = Visibility.Visible;
                rep63.Visibility = Visibility.Visible;
                rep63.SelectedIndex = 1;
                rep64.Visibility = Visibility.Visible;
                rep65.Visibility = Visibility.Visible;
                rep65.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tmQuk.DateTime.Hour,
                    tmQuk.DateTime.Minute, 0).AddHours(23);
            }
            else
            {
                rep61.Visibility = Visibility.Hidden;
                rep62.Visibility = Visibility.Hidden;
                rep63.Visibility = Visibility.Hidden;
                rep64.Visibility = Visibility.Hidden;
                rep65.Visibility = Visibility.Hidden;
            }
        }


        private bool ValidQBW()
        {
            if (txtQuickBookCompanyPath.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Company File path");
                GeneralFunctions.SetFocus(txtQuickBookCompanyPath);
                return false;
            }

            if (txtQuickBookIncomeAc.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Income Account Name");
                GeneralFunctions.SetFocus(txtQuickBookIncomeAc);
                return false;
            }

            if (txtQuickBookCOGSAc.Text.Trim() == "")
            {
                DocMessage.MsgEnter("COGS Account Name");
                GeneralFunctions.SetFocus(txtQuickBookIncomeAc);
                return false;
            }

            return true;
        }

        private bool AddQBWSetupBeforeSetOrRunSched()
        {
            bool blProceed = false;
            if (ValidQBW())
            {
                PosDataObject.Setup objsetup = new PosDataObject.Setup();
                objsetup.Connection = SystemVariables.Conn;

                objsetup.QBCompanyFilePath = txtQuickBookCompanyPath.Text.Trim();

                objsetup.ItemIncomeAccount = txtQuickBookIncomeAc.Text.Trim();
                objsetup.ItemCOGSAccount = txtQuickBookCOGSAc.Text.Trim();
                //objsetup.SalesTaxZero = txtQBWZeroTax.Text.Trim();
                //objsetup.SalesTaxNonZero = txtQBWNonZeroTax.Text.Trim();

                string error = objsetup.PostQuickBooksSetupData();

                if (error == "")
                {
                    Settings.LoadQuickBooksSettings();
                    blProceed = true;
                }
                else
                {
                }
            }

            if (blProceed)
            {
                if (Settings.QuickBooksWindowsZeroSalesTax == "")
                {
                    PosDataObject.Setup objsetup = new PosDataObject.Setup();
                    objsetup.Connection = SystemVariables.Conn;
                    objsetup.AddQBAutoData();
                    Settings.LoadQuickBooksSettings();
                }
            }


            return blProceed;
        }

        private void btnAutoSchedulrQuickBooks_Click(object sender, RoutedEventArgs e)
        {
            if (!AddQBWSetupBeforeSetOrRunSched()) return;
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " QuickBooks Windows Integration".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " QuickBooks Windows Integration".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  QB";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)tmQuk.DateTime.Hour, (short)tmQuk.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    if (chkRepeatQuickBooks.IsChecked == true)
                    {
                        SetScheduleEndTime(tmQuk, rep65);
                        int days = 0;
                        if (rep65.DateTime.Date > tmQuk.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep65.DateTime.Hour, rep65.DateTime.Minute, rep65.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmQuk.DateTime.Hour, tmQuk.DateTime.Minute, tmQuk.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);
                        dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                        if (rep63.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep62.Value.ToString());
                        else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep62.Value.ToString()) * 60;
                    }


                    t.Save();
                    t.Close();
                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrQuickBooks.Content = "Modify Scheduler";
                    btnAutoSchedulrQuickBooks.Tag = "Modify Scheduler";


                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " QuickBooks Windows Integration".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)tmQuk.DateTime.Hour) + TimeSpan.FromMinutes((short)tmQuk.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkRepeatQuickBooks.IsChecked == true)
                    {
                        SetScheduleEndTime(tmQuk, rep65);
                        int days = 0;
                        if (rep65.DateTime.Date > tmQuk.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep65.DateTime.Hour, rep65.DateTime.Minute, rep65.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmQuk.DateTime.Hour, tmQuk.DateTime.Minute, tmQuk.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep63.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep62.Value.ToString()) : GeneralFunctions.fnInt32(rep62.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  QB", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrQuickBooks.Content = "Modify Scheduler";
                    btnAutoSchedulrQuickBooks.Tag = "Modify Scheduler";
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }
        }

        private void btnRunSchedulerQuickBooks_Click(object sender, RoutedEventArgs e)
        {
            if (!AddQBWSetupBeforeSetOrRunSched()) return;

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "AutoExe_Retail.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\AutoExe_Retail.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"QBManual";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnQBC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                PosDataObject.Setup objqb = new PosDataObject.Setup();
                objqb.Connection = SystemVariables.Conn;

                DataTable dtbl = objqb.FetchSalesDataForQuickBooksOnline();

                if (dtbl.Rows.Count > 0)
                {
                    DataTable dtbltx = objqb.FetchTaxCodeForQuickBooksOnline();
                    if (dtbltx.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtbl.Rows)
                        {
                            if (dr["TaxLogic"].ToString() != "")
                            {
                                string txcode = "";
                                String[] tx = dr["TaxLogic"].ToString().Split(',');

                                foreach (string s in tx)
                                {
                                    foreach (DataRow dr1 in dtbltx.Rows)
                                    {
                                        if (dr1["id"].ToString() == s)
                                        {
                                            txcode = dr1["rate"].ToString();
                                            dr["TaxCode"] = txcode + "%";
                                            break;
                                        }
                                    }


                                    break;
                                }
                            }
                        }
                    }

                    DataTable dtblClone = dtbl.Clone();

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        dtblClone.ImportRow(dr);
                    }

                    int prevCnt = 0;
                    int prevInv = 0;
                    int currInv = 0;
                    bool bNextFile = false;
                    int invLine = 0;
                    int TotalFile = 1;
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        currInv = GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString());
                        if (prevInv != currInv)
                        {
                            invLine = 0;
                            DataRow[] filterRow = dtblClone.Select("InvoiceNo = '" + currInv.ToString() + "'");
                            invLine = filterRow.Length;
                            if (prevCnt + invLine <= 1000)
                            {

                            }
                            else
                            {

                                bNextFile = true;
                            }


                        }
                        else
                        {
                            invLine = 0;
                        }

                        if (prevCnt == 0)
                        {
                            dr["FileNo"] = TotalFile;
                            // Create 
                        }

                        if (bNextFile)
                        {
                            TotalFile++;
                            prevCnt = 0;
                            bNextFile = false;
                            dr["FileNo"] = TotalFile;
                        }

                        if (prevInv != currInv)
                        {
                            prevCnt = prevCnt + invLine;
                        }

                        dr["FileNo"] = TotalFile;
                        prevInv = currInv;
                    }


                    DataTable dtblFile = dtbl.DefaultView.ToTable(true, new String[] { "FileNo" });

                    int totalfile = dtblFile.Rows.Count;

                    DataTable dtbloutput = new DataTable();
                    dtbloutput.Columns.Add("FileName", System.Type.GetType("System.String"));
                    int i = 0;
                    foreach (DataRow dr in dtblFile.Rows)
                    {
                        string csvFilePath = GeneralFunctions.GetQBCCSVPath();

                        i++;
                        if (i == 1)
                        {

                        }


                        string csvFileName = csvFilePath + "salesreceipt_" + DateTime.Now.ToString("dd-MM-yy_HH_mm") + (totalfile == 1 ? "" : "_" + dr["FileNo"].ToString()) + ".csv";

                        StreamWriter writer = new StreamWriter(csvFileName);
                        StringBuilder builder = new StringBuilder();
                        try
                        {
                            int prevlength = 0;
                            string sepChar = ",";

                            string sep = "";
                            builder.Append(sep).Append("*SalesReceiptNo");
                            sep = sepChar;
                            builder.Append(sep).Append("Customer");
                            sep = sepChar;
                            builder.Append(sep).Append("*SalesReceiptDate");
                            sep = sepChar;
                            builder.Append(sep).Append("*DepositAccount");
                            sep = sepChar;
                            builder.Append(sep).Append("Location");
                            sep = sepChar;
                            builder.Append(sep).Append("Memo");
                            sep = sepChar;
                            builder.Append(sep).Append("Item(Product/Service)");
                            sep = sepChar;
                            builder.Append(sep).Append("ItemDescription");
                            sep = sepChar;
                            builder.Append(sep).Append("ItemQuantity");
                            sep = sepChar;
                            builder.Append(sep).Append("ItemRate");
                            sep = sepChar;
                            builder.Append(sep).Append("*ItemAmount");
                            sep = sepChar;
                            builder.Append(sep).Append("*ItemTaxCode");
                            sep = sepChar;
                            builder.Append(sep).Append("ItemTaxAmount");
                            sep = sepChar;
                            builder.Append(sep).Append("ServiceDate");
                            sep = sepChar;
                            writer.WriteLine(builder.ToString());
                            prevlength = builder.Length;

                            DataRow[] filterRow1 = dtbl.Select("FileNo = " + dr["FileNo"].ToString());

                            prevInv = 0;
                            currInv = 0;
                            foreach (DataRow dr1 in filterRow1)
                            {
                                currInv = GeneralFunctions.fnInt32(dr1["InvoiceNo"].ToString());
                                sep = "";

                                if (prevInv != currInv)
                                {
                                    builder.Append(sep).Append(dr1["InvoiceNo"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Customer"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["InvoiceDate"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("Undeposited Funds");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Item"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Qty"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Rate"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Total"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["TaxCode"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Tax"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                }
                                else
                                {
                                    builder.Append(sep).Append(dr1["InvoiceNo"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Item"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Qty"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Rate"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Total"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["TaxCode"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append(dr1["Tax"].ToString());
                                    sep = sepChar;
                                    builder.Append(sep).Append("");
                                    sep = sepChar;
                                }
                                writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                                prevlength = builder.Length;
                                prevInv = currInv;
                            }


                            writer.Close();

                            DataTable dtblUpdate = dtbl.Clone();
                            foreach (DataRow r in filterRow1)
                            {
                                dtblUpdate.ImportRow(r);
                            }

                            DataTable dtblUpdate2 = dtblUpdate.DefaultView.ToTable(true, new String[] { "InvoiceNo" });

                            foreach (DataRow udr in dtblUpdate2.Rows)
                            {
                                objqb.UpdateQuickBooksOnlineFlag(GeneralFunctions.fnInt32(udr["InvoiceNo"].ToString()));
                            }

                            dtbloutput.Rows.Add(new object[] { "salesreceipt_" + DateTime.Now.ToString("dd-MM-yy_HH_mm") + (totalfile == 1 ? "" : "_" + dr["FileNo"].ToString()) + ".csv" });

                        }
                        catch
                        {
                            Cursor = Cursors.Arrow;
                            DocMessage.MsgError("Failed to Export Sales Data");

                        }

                        if (dtbloutput.Rows.Count > 0)
                        {
                            string outfiles = "";
                            foreach (DataRow dro in dtbloutput.Rows)
                            {
                                outfiles = outfiles == "" ? dro["FileName"].ToString() : outfiles + ", " + dro["FileName"].ToString();
                            }
                            Cursor = Cursors.Arrow;
                            DocMessage.MsgInformation("Following file(s) successfully genarated :\r\n" + outfiles);
                        }
                    }


                }
                else
                {
                    Cursor = Cursors.Arrow;
                    DocMessage.MsgInformation("No Sales Data Found to Export");

                }

            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void MnuWooCommerce_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mnuNav2013.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuShopify.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooCommerce.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
            mnuWooQuickBooks.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuXero.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            pnlShopify.Visibility = Visibility.Collapsed;
            pnlWooCommerce.Visibility = Visibility.Visible;
            pnlQuickBooks.Visibility = Visibility.Collapsed;
            pnlXero.Visibility = Visibility.Collapsed;
            pnlNAV2013.Visibility = Visibility.Collapsed;
        }

        private void MnuXero_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mnuNav2013.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuShopify.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooCommerce.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooQuickBooks.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuXero.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));
            pnlShopify.Visibility = Visibility.Collapsed;
            pnlWooCommerce.Visibility = Visibility.Collapsed;
            pnlQuickBooks.Visibility = Visibility.Collapsed;
            pnlXero.Visibility = Visibility.Visible;
            pnlNAV2013.Visibility = Visibility.Collapsed;
        }

        private bool ValidXERO()
        {
            if (txtXeroCompany.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Compay Name");
                GeneralFunctions.SetFocus(txtXeroCompany);
                return false;
            }

            if (txtXeroClientID.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Client ID");
                GeneralFunctions.SetFocus(txtXeroClientID);
                return false;
            }

            if (txtXeroCallbackUri.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Redirect URIs ");
                GeneralFunctions.SetFocus(txtXeroCallbackUri);
                return false;
            }

            if (txtXeroAccount1.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Inventory Asset A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount1);
                return false;
            }

            if (txtXeroAccount2.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Inventory Adjustment A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount2);
                return false;
            }

            if (txtXeroAccount3.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Purchase A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount3);
                return false;
            }

            if (txtXeroAccount4.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Sales A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount4);
                return false;
            }

            if (txtXeroAccount5.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Purchase COGS A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount5);
                return false;
            }

            if (txtXeroAccount6.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Sales COGS A/C Code ");
                GeneralFunctions.SetFocus(txtXeroAccount6);
                return false;
            }

            if ((txtXeroAccessToken.Text.Trim() == "") || (txtXeroRefreshToken.Text.Trim() == ""))
            {
                DocMessage.MsgInformation("Tokens not found, Please Authorise and Get Tokens");
                return false;
            }

            return true;
        }

        private bool ValidWoo()
        {
            if (txtWooStoreAddress.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Store Address");
                GeneralFunctions.SetFocus(txtWooStoreAddress);
                return false;
            }

            if (txtWooConsumerKey.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Consumer Key");
                GeneralFunctions.SetFocus(txtWooConsumerKey);
                return false;
            }

            if (txtWooConsumerSecret.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Consumer Secret");
                GeneralFunctions.SetFocus(txtWooConsumerSecret);
                return false;
            }



            return true;
        }

        private bool AddWooCommerceSetupBeforeSetOrRunSched()
        {
            bool blProceed = false;
            if (ValidWoo())
            {
                PosDataObject.Setup objsetup = new PosDataObject.Setup();
                objsetup.Connection = SystemVariables.Conn;

                objsetup.WCommConsumerKey = txtWooConsumerKey.Text.Trim();
                objsetup.WCommConsumerSecret = txtWooConsumerSecret.Text.Trim();
                objsetup.WCommStoreAddress = txtWooStoreAddress.Text.Trim();

                string error = objsetup.PostWooCommerceSetupData();

                if (error == "")
                {
                    Settings.LoadWooCommerceSettings();
                    blProceed = true;
                }
                else
                {
                }
            }

            return blProceed;
        }

        private void BtnAutoSchedulrWoo_Click(object sender, RoutedEventArgs e)
        {
            if (!AddWooCommerceSetupBeforeSetOrRunSched()) return;
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }

            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Woo Commerce Integration".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Woo Commerce Integration".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  WooComm";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)tmWoo.DateTime.Hour, (short)tmWoo.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    if (chkRepeatWoo.IsChecked == true)
                    {
                        SetScheduleEndTime(tmWoo, rep55);
                        int days = 0;
                        if (rep55.DateTime.Date > tmWoo.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep55.DateTime.Hour, rep55.DateTime.Minute, rep55.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmWoo.DateTime.Hour, tmWoo.DateTime.Minute, tmWoo.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);
                        dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                        if (rep53.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep52.Value.ToString());
                        else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep52.Value.ToString()) * 60;
                    }


                    t.Save();
                    t.Close();
                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrWoo.Content = "Modify Scheduler";
                    btnAutoSchedulrWoo.Tag = "Modify Scheduler";


                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }

            if (Settings.OSVersion == "Win 11")
            {
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Woo Commerce Integration".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)tmWoo.DateTime.Hour) + TimeSpan.FromMinutes((short)tmWoo.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkRepeatWoo.IsChecked == true)
                    {
                        SetScheduleEndTime(tmWoo, rep55);
                        int days = 0;
                        if (rep55.DateTime.Date > tmWoo.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep55.DateTime.Hour, rep55.DateTime.Minute, rep55.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmWoo.DateTime.Hour, tmWoo.DateTime.Minute, tmWoo.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep53.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep52.Value.ToString()) : GeneralFunctions.fnInt32(rep52.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  WooComm", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrWoo.Content = "Modify Scheduler";
                    btnAutoSchedulrWoo.Tag = "Modify Scheduler";
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }
        }

        private void BtnRunSchedulerWoo_Click(object sender, RoutedEventArgs e)
        {
            if (!AddWooCommerceSetupBeforeSetOrRunSched()) return;

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "AutoExe_Retail.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\AutoExe_Retail.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"WooCommManual";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }


        private static string GenerateCodeVerifier()
        {
            //Generate a random string for our code verifier
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);

            var codeVerifier = Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
            return codeVerifier;
        }

        private async void BtnXeroAutorize_Click(object sender, RoutedEventArgs e)
        {
            if (txtXeroCompany.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Compay Name");
                GeneralFunctions.SetFocus(txtXeroCompany);
                return;
            }

            if (txtXeroClientID.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Client ID");
                GeneralFunctions.SetFocus(txtXeroClientID);
                return;
            }

            if (txtXeroCallbackUri.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Redirect URIs ");
                GeneralFunctions.SetFocus(txtXeroCallbackUri);
                return;
            }

            btnXeroAutorize.IsEnabled = false;
            try
            {
                btnXeroGetToken.IsEnabled = false;
                txtXeroReturnCode.Text = "";
                txtXeroReturnState.Text = "";

                xeroscopes = "offline_access accounting.transactions openid profile email accounting.contacts accounting.settings";
                xerostate = "345217689";
                xerocodeverifier = GenerateCodeVerifier();


                var clientId = txtXeroClientID.Text;
                var scopes = Uri.EscapeUriString(xeroscopes);
                var redirectUri = txtXeroCallbackUri.Text;
                var state = xerostate;

                //generate the code challenge based on the verifier
                string codeChallenge;
                using (var sha256 = SHA256.Create())
                {
                    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(xerocodeverifier));
                    codeChallenge = Convert.ToBase64String(challengeBytes)
                        .TrimEnd('=')
                        .Replace('+', '-')
                        .Replace('/', '_');
                }

                var authLinkInitial = $"{AuthorisationUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope={scopes}&state={state}&code_challenge={codeChallenge}&code_challenge_method=S256";
                xeroauthlink = authLinkInitial;


                var authLink = xeroauthlink;

                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = authLink;
                System.Diagnostics.Process process1 = new System.Diagnostics.Process();
                process1.StartInfo = psi;
                process1.Start();

                // System.Diagnostics.Process.Start(authLink);

                //start webserver to listen for the callback
                LocalHttpListener.StartWebServer(this);
            }
            catch
            {

            }
            finally
            {
                btnXeroAutorize.IsEnabled = true;
            }
        }

        private async void BtnXeroGetToken_Click(object sender, RoutedEventArgs e)
        {
            txtXeroAccessToken.Text = "";
            txtXeroRefreshToken.Text = "";

            const string url = "https://identity.xero.com/connect/token";

            var client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", txtXeroClientID.Text),
                new KeyValuePair<string, string>("code", txtXeroReturnCode.Text),
                new KeyValuePair<string, string>("redirect_uri", txtXeroCallbackUri.Text),
                new KeyValuePair<string, string>("code_verifier", xerocodeverifier),
            });

            var response = await client.PostAsync(url, formContent);

            //read the response and populate the boxes for each token
            //could also parse the expiry here if required
            var content = await response.Content.ReadAsStringAsync();
            var tokens = JObject.Parse(content);

            //txtBoxIDToken.Text = tokens["id_token"]?.ToString();
            txtXeroAccessToken.Text = tokens["access_token"]?.ToString();
            txtXeroRefreshToken.Text = tokens["refresh_token"]?.ToString();

            if (txtXeroAccessToken.Text != "")
            {
                var client1 = new HttpClient();
                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", txtXeroAccessToken.Text);

                var response1 = await client1.GetAsync(ConnectionsUrl);
                var content1 = await response1.Content.ReadAsStringAsync();

                //fill the dropdown box based on the results 
                var tenants = JsonConvert.DeserializeObject<List<Tenant>>(content1);

                xerotenantId = "";
                foreach (Tenant t in tenants)
                {
                    if (t.Name.Equals(txtXeroCompany.Text))
                    {
                        xerotenantId = t.Id;
                        break;
                    }
                }
                if (xerotenantId != "")
                {
                    btnXeroGetToken.IsEnabled = false;
                }
            }
        }

        private void TxtXeroReturnState_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtXeroReturnState.Text == xerostate)
            {
                if (!btnXeroGetToken.IsEnabled) btnXeroGetToken.IsEnabled = true;
            }
        }

        private void ChkRepeatXero_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRepeatXero.IsChecked == true)
            {
                rep71.Visibility = Visibility.Visible;
                rep72.Visibility = Visibility.Visible;
                rep73.Visibility = Visibility.Visible;
                rep73.SelectedIndex = 1;
                rep74.Visibility = Visibility.Visible;
                rep75.Visibility = Visibility.Visible;
                rep75.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tmQuk.DateTime.Hour,
                    tmQuk.DateTime.Minute, 0).AddHours(23);
            }
            else
            {
                rep71.Visibility = Visibility.Hidden;
                rep72.Visibility = Visibility.Hidden;
                rep73.Visibility = Visibility.Hidden;
                rep74.Visibility = Visibility.Hidden;
                rep75.Visibility = Visibility.Hidden;
            }
        }

        private bool AddXEROSetupBeforeSetOrRunSched()
        {
            bool blProceed = false;
            if (ValidXERO())
            {
                PosDataObject.Setup objsetup = new PosDataObject.Setup();
                objsetup.Connection = SystemVariables.Conn;

                objsetup.XeroCompanyName = txtXeroCompany.Text.Trim();
                objsetup.XeroClientId = txtXeroClientID.Text.Trim();
                objsetup.XeroCallbackUrl = txtXeroCallbackUri.Text.Trim();
                objsetup.XeroAccessToken = txtXeroAccessToken.Text.Trim();
                objsetup.XeroRefreshToken = txtXeroRefreshToken.Text.Trim();
                objsetup.XeroTenantId = xerotenantId;

                objsetup.XeroInventoryAssetAccountCode = txtXeroAccount1.Text.Trim();
                objsetup.XeroInventoryAdjustmentAccountCode = txtXeroAccount2.Text.Trim();
                objsetup.XeroAccountCodePurchase = txtXeroAccount3.Text.Trim();
                objsetup.XeroAccountCodeSale = txtXeroAccount4.Text.Trim();
                objsetup.XeroCOGSAccountCodePurchase = txtXeroAccount5.Text.Trim();
                objsetup.XeroCOGSAccountCodeSale = txtXeroAccount6.Text.Trim();

                string error = objsetup.PostXEROSetupData();

                if (error == "")
                {
                    Settings.LoadXEROSettings();
                    blProceed = true;
                }
                else
                {
                }
            }




            return blProceed;
        }

        private void BtnAutoSchedulrXero_Click(object sender, RoutedEventArgs e)
        {
            if (!AddXEROSetupBeforeSetOrRunSched()) return;
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "Xero\\XeXero.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\Xero\\XeXero.exe";
            }


            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " XERO Integration".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " XERO Integration".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  XERO";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)tmXero.DateTime.Hour, (short)tmXero.DateTime.Minute, 1);
                    t.Triggers.Add(dt);
                    if (chkRepeatXero.IsChecked == true)
                    {
                        SetScheduleEndTime(tmXero, rep75);
                        int days = 0;
                        if (rep75.DateTime.Date > tmXero.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep75.DateTime.Hour, rep75.DateTime.Minute, rep75.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmXero.DateTime.Hour, tmXero.DateTime.Minute, tmXero.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);
                        dt.DurationMinutes = rts.Hours * 60 + rts.Minutes;
                        if (rep73.SelectedIndex == 0) dt.IntervalMinutes = GeneralFunctions.fnInt32(rep72.Value.ToString());
                        else dt.IntervalMinutes = GeneralFunctions.fnInt32(rep72.Value.ToString()) * 60;
                    }


                    t.Save();
                    t.Close();
                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrQuickBooks.Content = "Modify Scheduler";
                    btnAutoSchedulrQuickBooks.Tag = "Modify Scheduler";


                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }

            }


            if (Settings.OSVersion == "Win 11")
            {
                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " XERO Integration".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)tmXero.DateTime.Hour) + TimeSpan.FromMinutes((short)tmXero.DateTime.Minute);
                    daily.DaysInterval = 1;


                    if (chkRepeatXero.IsChecked == true)
                    {
                        SetScheduleEndTime(tmXero, rep75);
                        int days = 0;
                        if (rep75.DateTime.Date > tmXero.DateTime.Date) days = 1;

                        TimeSpan ets = new TimeSpan(days, rep75.DateTime.Hour, rep75.DateTime.Minute, rep75.DateTime.Second);
                        TimeSpan sts = new TimeSpan(tmXero.DateTime.Hour, tmXero.DateTime.Minute, tmXero.DateTime.Second);
                        TimeSpan rts = ets.Subtract(sts);

                        RepetitionPattern repetition = new RepetitionPattern(TimeSpan.FromMinutes(rep73.SelectedIndex == 0 ? GeneralFunctions.fnInt32(rep72.Value.ToString()) : GeneralFunctions.fnInt32(rep72.Value.ToString()) * 60), TimeSpan.FromMinutes(rts.Hours * 60 + rts.Minutes), true);
                        daily.Repetition = repetition;
                    }

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  XERO", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    DocMessage.MsgInformation("Scheduler successfully set");
                    btnAutoSchedulrXero.Content = "Modify Scheduler";
                    btnAutoSchedulrXero.Tag = "Modify Scheduler";
                }
                catch
                {
                    DocMessage.MsgInformation("Error while scheduling ...");
                }
            }
        }

        private void BtnRunSchedulerXero_Click(object sender, RoutedEventArgs e)
        {
            if (!AddXEROSetupBeforeSetOrRunSched()) return;

            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "Xero\\XeXero.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\Xero\\XeXero.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"XEROManual";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void chkRepeatWoo_Checked(object sender, RoutedEventArgs e)
        {
            if (chkRepeatWoo.IsChecked == true)
            {
                rep51.Visibility = Visibility.Visible;
                rep52.Visibility = Visibility.Visible;
                rep53.Visibility = Visibility.Visible;
                rep53.SelectedIndex = 1;
                rep54.Visibility = Visibility.Visible;
                rep55.Visibility = Visibility.Visible;
                rep55.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, tmWoo.DateTime.Hour,
                    tmWoo.DateTime.Minute, 0).AddHours(23);
            }
            else
            {
                rep51.Visibility = Visibility.Hidden;
                rep52.Visibility = Visibility.Hidden;
                rep53.Visibility = Visibility.Hidden;
                rep54.Visibility = Visibility.Hidden;
                rep55.Visibility = Visibility.Hidden;
            }
        }

        private void CmbBkupType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnBkupSchd_Click(object sender, RoutedEventArgs e)
        {
            UpdateDBBackupParameter();
            Settings.LoadSettingsVariables();
            string csConnPath = "";
            csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (csConnPath.EndsWith("\\"))
            {
                csConnPath = csConnPath + "AutoExe_Retail.exe";
            }
            else
            {
                csConnPath = csConnPath + "\\AutoExe_Retail.exe";
            }


            if (Settings.OSVersion == "Win 10")
            {
                ScheduledTasks st = new ScheduledTasks();
                st.DeleteTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));
                try
                {
                    ScheduledTasks st1 = new ScheduledTasks();

                    TaskScheduler.Task t = st1.CreateTask(SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName));

                    t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                    t.ApplicationName = csConnPath;
                    t.Parameters = "  D";
                    t.SetAccountInformation(Environment.UserName, (String)null);
                    TaskScheduler.DailyTrigger dt = new TaskScheduler.DailyTrigger((short)txtBackupTime.DateTime.Hour, (short)txtBackupTime.DateTime.Minute, 1);
                    t.Triggers.Add(dt);

                    t.Save();
                    t.Close();
                    if (btnBkupSchd.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnBkupSchd.Tag = "Modify Scheduler";
                        btnRunBkupSchd.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated");
                    }
                }
                catch (Exception ex)
                {
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }
            }

            if (Settings.OSVersion == "Win 11")
            {

                try

                {

                    Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService();

                    string TaskName = SystemVariables.BrandName + " Retail DashBoard".Replace("XEPOS", SystemVariables.BrandName);
                    bool boolFindTask = false;
                    boolFindTask = ts.GetTask(@"" + TaskName) != null;
                    if (boolFindTask)
                    {
                        ts.RootFolder.DeleteTask(@"" + TaskName);
                    }

                    Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = TaskName;

                    Microsoft.Win32.TaskScheduler.DailyTrigger daily = new Microsoft.Win32.TaskScheduler.DailyTrigger();

                    daily.StartBoundary = DateTime.Today + TimeSpan.FromHours((short)txtBackupTime.DateTime.Hour) + TimeSpan.FromMinutes((short)txtBackupTime.DateTime.Minute);
                    daily.DaysInterval = 1;

                    td.Triggers.Add(daily);

                    td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(csConnPath, "  D", null));

                    ts.RootFolder.RegisterTaskDefinition(@"" + TaskName, td);

                    if (btnBkupSchd.Tag.ToString() == "Add Scheduler")
                    {
                        DocMessage.MsgInformation("Scheduler successfully created");
                        btnBkupSchd.Tag = "Modify Scheduler";
                        btnRunBkupSchd.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Scheduler successfully updated");
                    }
                }
                catch
                {
                    DocMessage.MsgInformation(Properties.Resources.Error_while_scheduling_tasks);
                }
            }
        }

        private void BtnRunBkupSchd_Click(object sender, RoutedEventArgs e)
        {
            UpdateDBBackupParameter();
            Settings.LoadSettingsVariables();
            Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(5000);
            try
            {
                string csConnPath = "";
                csConnPath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (csConnPath.EndsWith("\\"))
                {
                    csConnPath = csConnPath + "AutoExe_Retail.exe";
                }
                else
                {
                    csConnPath = csConnPath + "\\AutoExe_Retail.exe";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @csConnPath;
                startInfo.Arguments = @"D";
                Process.Start(startInfo);


            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void UpdateDBBackupParameter()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            objSetup.LoginUserID = SystemVariables.CurrentUserID;

            //Backup
            objSetup.BackupType = cmbBkupType.SelectedIndex;
            objSetup.BackupStorageType = cmbBkupStorageType.SelectedIndex;
            string err = objSetup.UpdateBackupTypeSetup();
        }

        private void MnuNav2013_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mnuNav2013.Background = this.FindResource("NavGroupSelected") as Brush;

            mnuWooQuickBooks.Background = this.FindResource("NavGroup") as Brush;
            mnuWooCommerce.Background = this.FindResource("NavGroup") as Brush;
            mnuXero.Background = this.FindResource("NavGroup") as Brush;

            mnuShopify.Background = this.FindResource("NavGroup") as Brush;

            mnuNav2013.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64C0D9"));

            mnuWooQuickBooks.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuXero.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuWooCommerce.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            mnuShopify.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4F9DAD"));
            pnlShopify.Visibility = Visibility.Collapsed;

            pnlQuickBooks.Visibility = Visibility.Collapsed;
            pnlXero.Visibility = Visibility.Collapsed;
            pnlWooCommerce.Visibility = Visibility.Collapsed;
            pnlNAV2013.Visibility = Visibility.Visible;
        }

        private void BtnAutoSchedulrO5_Click(object sender, RoutedEventArgs e)
        {
            DocMessage.MsgInformation("Scheduler successfully set");
        }

        private void ChkgcRepeat55_Checked(object sender, RoutedEventArgs e)
        {
            if (chkgcRepeat55.IsChecked == true)
            {
                rep415.Visibility = Visibility.Visible;
                rep425.Visibility = Visibility.Visible;
                rep435.Visibility = Visibility.Visible;
                rep435.SelectedIndex = 1;
                rep445.Visibility = Visibility.Visible;
                rep455.Visibility = Visibility.Visible;
                rep455.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, txtTimeN.DateTime.Hour,
                    txtTimeN.DateTime.Minute, 0).AddHours(23);
            }
            else
            {
                rep415.Visibility = Visibility.Hidden;
                rep425.Visibility = Visibility.Hidden;
                rep435.Visibility = Visibility.Hidden;
                rep445.Visibility = Visibility.Hidden;
                rep455.Visibility = Visibility.Hidden;
            }
        }

        private void ChkPrintLogo_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void Full_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutFullKybrdOpen) && (bCallFromAdmin ? Settings.UseTouchKeyboardInAdmin == "Y" : Settings.UseTouchKeyboardInPOS == "Y"))
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }


        private void Full_GotFocusP(object sender, RoutedEventArgs e)
        {
            if (bCallFromAdmin ? Settings.UseTouchKeyboardInAdmin == "N" : Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.PasswordBoxEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void PART_Editor_LinkTemplate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbSMTPType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbSMTPType.SelectedIndex == 0)
            {
                
            }
            if (cmbSMTPType.SelectedIndex == 1)
            {

                if (txtREHost.Text != "smtp.gmail.com")
                {
                    txtREHost.Text = "smtp.gmail.com";
                    txtREPort.Text = "587";
                    chkRESSL.IsChecked = true;
                }
            }
        }

        private void TxtREHost_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (txtREHost.Text == "")
            {
                if (cmbSMTPType.SelectedIndex == 1)
                {
                    txtREHost.Text = "smtp.gmail.com";
                    txtREPort.Text = "587";
                    chkRESSL.IsChecked = true;
                }
            }
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ChkShopifyPremium_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }
    }
}
