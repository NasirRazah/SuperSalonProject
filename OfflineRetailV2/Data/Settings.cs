using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRetailV2.Data
{
    public class Settings
    {

        public Settings()
        {

        }

        #region Constants

        public const string socketuri = @"wss://<account-name>.connect.paymentsense.cloud/<integration-type>?token=<api-key>&api-version=<connect-version>&software-house-id=<software-house-id>&installer-id=<installer-id>";

        /* XEPOS CLOUD CONNECTION PARAMETER */

        

        public const string XEPOSCloudConnectionParameter = @"server=esql2k1701.discountasp.net;database=SQL2017_1028416_retail;uid=SQL2017_1028416_retail_user;pwd=express786#";

        //public const string XEPOSCloudConnectionParameter = @"server=RAJIBNEW\POSEXPRESS;database=SQL2017_1028416_retail;uid=sa;pwd=posmaster";

        // Currect Param    
        //public const string XEPOSCloudConnectionParameter = @"server=tcp:esql2k1701.discountasp.net;database= SQL2017_1028416_retail;uid= SQL2017_1028416_retail_user;pwd=express786#";

        //@"server=ENNOVATIVE\XEPOSHOSPITALITY;database=SQL2017_1028416_retail;uid=sa;pwd=anymouse786";


        // @"server=esql2k1701.discountasp.net;database=SQL2017_1018251_xeposonline;uid=SQL2017_1018251_xeposonline_user;pwd=partha786!";


        /* Mercury Actions */
        public const int POSScreenVersion = 2;
        public const string Event1 = "User Login";
        public const string Event2 = "Account lockout";
        public const string Event3 = "Account lockout reset";
        public const string Event4 = "Password or PIN change";
        public const string Event5 = "Account creation";
        public const string Event6 = "Account deletion";

        public const string Event7 = "Access to user account security screens";
        public const string Event8 = "Changes to user account security";

        public const string Event9 = "Access to payment processing configuration screens";
        public const string Event10 = "Changes to payment processing configuration";

        public const string Event11 = "View Log";
        public const string Event12 = "Delete Log";

        public const string UseStyle = "YES";  // responsible for using extensive form styles 
        public const string DefaultFormSkin = "Caramel";
        public const string DefaultCategoryFontType = "Tahoma";
        public const string DefaultCategoryFontSize = "9";
        public const string DefaultItemFontType = "Tahoma";
        public const string DefaultItemFontSize = "9";
        public const string ADMINPASSWORD = "3089";
        public const string PublisherName = "pnpdemo";
        public const string ExportDelimiter = "|";

        //public const int    PrintFactor = -150;      // default 0
        public const int NoOfPrintSpace1 = 1;    // default 2
        public const int NoOfPrintSpace2 = 3;    // default 8
        public const int NoOfPrintSpace3 = 5;    // default 10
        public const int NoOfPrintSign = 20;   // default 28
        public const int MaxDemoInvoiceNo = 1000;   // maximum invoice allowed in demo version
        public const string StocktakeDocPrefix = "DOC - ";
        public const int MaxEmployeeInAppointmentScreen = 10;    // 

        public const int ScaleDecimal = 3; // no. of digits afer decimal considered in weight scale 

        public const int ScreenCountForSecondMonitor = 2; // second monitor screen count  put value 2 for live, 1 for development

        public const string ItemGrid = "SK*PN*BR*UP*PR*QT*DP*CT*TY*FS*SN*VI*VN*VP";
        public const string CustGrid = "CI*FN*LN*CO*WP*AR*CI";
        public const string VendGrid = "VI*VN*VP";

        public const string PasswordCode = "*arp";

        public const string Datalogic_Scale = "SCRS232Scale";
        public const string Datalogic_Scanner = "SCRS232Scanner";

        public const int RegularHoursPerWeek = 40;
        public const double OverTimeFactor = 1.5;

        public const string LabelDotNetSerialNo = "BT62NH44AL3CS1";

        public const int CutoffScreenResolutionForScale = 1200; // Cut off Screen Resolution beyond Scale Button Height will change with the following parameter

        // 50% add in height

        public const int AltScaleCategoryCaptionSize = 57;  // Scale Category Caption Size                  # Default 38
        public const float AltScaleCategoryCaptionFontSize = 36f; // Scale Category Caption Font Size       # Default 24f
        public const int AltScaleCategorySize = 65; // Scale Category Size without item                     # Default 45

        public const int AltScaleItem1ButtonHeight = 60; // Scale Item Button Type 1                        # Default 40
        public const int AltScaleItem2ButtonHeight = 82; // Scale Item Button Type 2                        # Default 55
        public const int AltScaleItem3ButtonHeight = 112; // Scale PLU Button Type 2                        # Default 75 

        public const string TestScaleWeight = "6.04";


        public const string NTEPCert_Default = "16-053";  // Default NTEP Cert No for Live Weight


        public const string EmbeddedBarcodeNumberSystemChar = "1"; // for US 2, 1 for UK


        #endregion

        private static string strSoftwareVersion;
        public static string SoftwareVersion
        {
            get { return strSoftwareVersion; }
            set { strSoftwareVersion = value; }
        }

        private static bool boolSpeedUpPOS;
        public static bool SpeedUpPOS
        {
            get { return boolSpeedUpPOS; }
            set { boolSpeedUpPOS = value; }
        }

        #region Variables

        #region Private Declaration

        private static int intItemExpiryAlertDay;
        public static int ItemExpiryAlertDay
        {
            get { return intItemExpiryAlertDay; }
            set { intItemExpiryAlertDay = value; }
        }


        private static string strAppointmentEmailBody;
        public static string AppointmentEmailBody
        {
            get { return strAppointmentEmailBody; }
            set { strAppointmentEmailBody = value; }
        }

        private static string strPrintLogoInReceipt;
        public static string PrintLogoInReceipt
        {
            get { return strPrintLogoInReceipt; }
            set { strPrintLogoInReceipt = value; }
        }

        private static int intBackupType;
        private static int intBackupStorageType;


        public static int BackupType
        {
            get { return intBackupType; }
            set { intBackupType = value; }
        }

        public static int BackupStorageType
        {
            get { return intBackupStorageType; }
            set { intBackupStorageType = value; }
        }

        private static string strOSVersion;

        public static string OSVersion
        {
            get { return strOSVersion; }
            set { strOSVersion = value; }
        }


        private static string strWCommConsumerKey;
        private static string strWCommConsumerSecret;
        private static string strWCommStoreAddress;

        public static string WCommConsumerKey
        {
            get { return strWCommConsumerKey; }
            set { strWCommConsumerKey = value; }
        }

        public static string WCommConsumerSecret
        {
            get { return strWCommConsumerSecret; }
            set { strWCommConsumerSecret = value; }
        }

        public static string WCommStoreAddress
        {
            get { return strWCommStoreAddress; }
            set { strWCommStoreAddress = value; }
        }

        


        private static string strXeroCompanyName;
        private static string strXeroClientId;
        private static string strXeroCallbackUrl;
        private static string strXeroAccessToken;
        private static string strXeroRefreshToken;
        private static string strXeroTenantId;

        private static string strXeroInventoryAssetAccountCode;
        private static string strXeroCOGSAccountCodePurchase;
        private static string strXeroCOGSAccountCodeSale;
        private static string strXeroAccountCodeSale;
        private static string strXeroAccountCodePurchase;
        private static string strXeroInventoryAdjustmentAccountCode;

        private static string strQuickBooksWindowsCompanyFilePath;
        private static string strQuickBooksWindowsIncomeAccount;
        private static string strQuickBooksWindowsCOGSAccount;
        private static string strQuickBooksWindowsZeroSalesTax;
        private static string strQuickBooksWindowsNonZeroSalesTax;

        public static string XeroInventoryAssetAccountCode
        {
            get { return strXeroInventoryAssetAccountCode; }
            set { strXeroInventoryAssetAccountCode = value; }
        }

        public static string XeroCOGSAccountCodePurchase
        {
            get { return strXeroCOGSAccountCodePurchase; }
            set { strXeroCOGSAccountCodePurchase = value; }
        }

        public static string XeroCOGSAccountCodeSale
        {
            get { return strXeroCOGSAccountCodeSale; }
            set { strXeroCOGSAccountCodeSale = value; }
        }

        public static string QuickBooksWindowsIncomeAccount
        {
            get { return strQuickBooksWindowsIncomeAccount; }
            set { strQuickBooksWindowsIncomeAccount = value; }
        }

        public static string QuickBooksWindowsCOGSAccount
        {
            get { return strQuickBooksWindowsCOGSAccount; }
            set { strQuickBooksWindowsCOGSAccount = value; }
        }

        public static string QuickBooksWindowsZeroSalesTax
        {
            get { return strQuickBooksWindowsZeroSalesTax; }
            set { strQuickBooksWindowsZeroSalesTax = value; }
        }

        public static string QuickBooksWindowsNonZeroSalesTax
        {
            get { return strQuickBooksWindowsNonZeroSalesTax; }
            set { strQuickBooksWindowsNonZeroSalesTax = value; }
        }

        public static string QuickBooksWindowsCompanyFilePath
        {
            get { return strQuickBooksWindowsCompanyFilePath; }
            set { strQuickBooksWindowsCompanyFilePath = value; }
        }

        public static string XeroAccountCodeSale
        {
            get { return strXeroAccountCodeSale; }
            set { strXeroAccountCodeSale = value; }
        }

        public static string XeroAccountCodePurchase
        {
            get { return strXeroAccountCodePurchase; }
            set { strXeroAccountCodePurchase = value; }
        }

        public static string XeroInventoryAdjustmentAccountCode
        {
            get { return strXeroInventoryAdjustmentAccountCode; }
            set { strXeroInventoryAdjustmentAccountCode = value; }
        }

        public static string XeroCompanyName
        {
            get { return strXeroCompanyName; }
            set { strXeroCompanyName = value; }
        }

        public static string XeroClientId
        {
            get { return strXeroClientId; }
            set { strXeroClientId = value; }
        }

        public static string XeroCallbackUrl
        {
            get { return strXeroCallbackUrl; }
            set { strXeroCallbackUrl = value; }
        }

        public static string XeroAccessToken
        {
            get { return strXeroAccessToken; }
            set { strXeroAccessToken = value; }
        }


        public static string XeroRefreshToken
        {
            get { return strXeroRefreshToken; }
            set { strXeroRefreshToken = value; }
        }

        public static string XeroTenantId
        {
            get { return strXeroTenantId; }
            set { strXeroTenantId = value; }
        }

        private static int intCentralCustomerID;

        private static SqlConnection sqlConn;
        private static CultureInfo cultr_POS;

        private static string strdisableresource;
        public static string strdefault_culture;

        private static string strDataObjectCulture_All;
        private static string strDataObjectCulture_None;
        private static string strDataObjectCulture_ADMIN;
        private static string strDataObjectCulture_Administrator;
        private static string strDataObjectCulture_AllDepartments;
        private static string strDataObjectCulture_SelectSKU;
        private static string strDataObjectCulture_SelectPLU;



        private static string strAutoDisplayItemImage;
        private static string strUse4Decimal;
        private static int intPOSPrintInvoice;
        private static int intDecimalPlace;
        private static int intCustomerInfo;
        private static int intVendorUpdate;
        private static int intCloseoutOption;
        private static int intSignINOption;
        private static int intMonthsInvJrnl;
        private static string strBlindCountPreview;
        private static string strOtherTerminalCloseout;
        private static string strSalesByHour;
        private static string strSalesByDept;

        private static string strPrintBlindDropCloseout;
        private static string strPrintCloseoutInReceiptPrinter;

        private static string strPOSAcceptCheck;
        private static string strPOSDisplayChangeDue;
        private static string strPOSIDRequired;
        private static string strPOSRedirectToChangeDue;
        private static string strPOSShowGiftCertBalance;
        private static string strNoPriceOnLabelDefault;
        private static string strQuantityRequired;
        private static string strUseInvJrnl;
        private static double dblGiftCertMaxChange;
        private static string strReceiptHeader;
        private static string strReceiptFooter;
        private static string strReceiptLayawayPolicy;
        private static string strPoleScreen;
        private static string strFormSkin;
        private static double dblLayawayDepositPercent;
        private static int intLayawaysDue;
        private static int intLayawayReceipts;
        private static int intLayawayPaymentOption;
        private static int intPOSMoreFunctionAlignment;
        private static string strLineDisplayDeviceType;
        private static string strLineDisplayDevice;
        private static string strLineDisplayDeviceSerial;
        private static string strCloseoutExport;
        private static double dblBottleDeposit;
        private static double dblMaxScaleWeight;
        private static string strCompany;
        private static string strAddress1;
        private static string strAddress2;
        private static string strCity;
        private static string strState;
        private static string strPostalCode;
        private static string strPhone;
        private static string strFax;
        private static string strEMail;
        private static string strWebAddress;
        private static string strCompanyAddress;
        private static string strTerminalName;
        private static string strTerminalTcpIp;

        private static int intDisplayLogoOrWeb;
        private static string strDisplayURL;
        private static string strBrowsePermission;

        private static string strReportHeader_Company;
        private static string strReportHeader_Address;

        private static string strReceiptHeader_Company;
        private static string strReceiptHeader_Address;

        private static string strReportHeader;
        private static string strDefaultCloseoutPrinter;
        private static string strReportPrinterName;
        private static string strReceiptPrinterName;
        private static string strLabelPrinterType;
        private static string strLabelPrinterName;
        private static string strBarTenderLabelPrint;

        private static string strScalePrinterName;
        private static string strExternalLabelPrinterName;
        private static string strRecipePrinterName;

        private static string strStartWithScaleScreen;
        private static string strStartWithScale_User;
        private static string strStartWithScale_Password;

        private static string strStoreInfo;
        private static string strPOSDisplayProductImage;
        private static string strPOSCardPayment;
        private static string strPOSCardPaymentMerged;

        private static string strGeneralReceiptPrint;
        private static string strPreprintedReceipt;

        private static string strCardPublisherName;
        private static string strCardPublisherEMail;

        private static int intPaymentGateway;
        private static string strRunScaleControllerOnHostImport;
        private static string strDatacapServer;
        private static string strDatacapMID;
        private static string strDatacapSecureDeviceID;
        private static string strDatacapCOMPort;
        private static string strDatacapPINPad;
        private static double dblDatacapSignAmount;

        private static string strDatacapSignatureDevice;
        private static string strDatacapSignatureDeviceCOMPort;
        private static int intDatacapCardEntryMode;

        private static string strDatacapEMVServerIP;
        private static string strDatacapEMVServerPort;
        private static string strDatacapEMVMID;
        private static string strDatacapEMVUserTrace;
        private static string strDatacapEMVTerminalID;
        private static string strDatacapEMVOperatorID;
        private static string strDatacapEMVSecurityDevice;
        private static string strDatacapEMVCOMPort;
        private static string strDatacapEMVManual;
        private static string strDatacapEMVToken;

        private static string strPOSLinkCommType;
        private static string strPOSLinkDestPort;
        private static string strPOSLinkDestIP;
        private static string strPOSLinkSerialPort;
        private static string strPOSLinkBaudRate;
        private static string strPOSLinkTimeout;

        private static string strPrecidiaDataPath;
        private static string strPrecidiaUsePINPad;
        private static string strPrecidiaLaneOpen;
        private static string strPrecidiaSign;
        private static double dblPrecidiaSignAmount;
        private static int intPrecidiaProcessTimeout;
        private static string strPrecidiaRRLog;

        private static string strPrecidiaClientMAC;
        private static string strPrecidiaPOSLynxMAC;
        private static int intPrecidiaPort;

        private static string strMercuryHPMerchantID;
        private static string strMercuryHPUserID;
        private static string strMercuryHPPort;
        private static double dblMercurySignAmount;

        private static string strElementHPAccountID;
        private static string strElementHPAccountToken;
        private static string strElementHPApplicationID;
        private static string strElementHPAcceptorID;
        private static int intElementHPMode;
        private static string strElementHPTerminalID;
        private static bool blIsPOSSelected;

        private static int intUsePriceLevel;

        private static string strForcedLogin;

        public static string PriceLevelForThisSale = "N";
        public static string PriceLevelForOneTime = "N";
        public static int TempPriceLevel = 0;

        private static string strAutoPO;
        private static string strAutoCustomer;

        private static string strAutoGC;
        private static string strSingleGC;

        private static string strPrintDuplicateGiftCertSaleReceipt;

        private static string strIsDuplicateInvoice;
        private static string strReceiptPrintOnReturn;

        private static string dtAgreeLicenseFirstTime;
        private static string strAgreeLicenseSecondTime;

        private static string strCashDrawerCode;

        private static string strUseCustomerNameInLabelPrint;

        private static string strCustomerRequiredOnRent = "N";
        private static string strCalculateRentLater = "N";

        private static string strAcceptTips = "N";
        private static string strShowTipsInReceipt = "N";

        private static string strShowFoodStampTotal = "N";

        private static string strEasyTendering = "N";
        private static string strShowFeesInReceipt = "Y";

        private static string strShowSaleSaveInReceipt = "Y";

        private static string strAddGallon = "N";

        private static string strDisplayLaneClosed = "N";

        private static string strPrint2TicketsForRepair = "N";

        private static string strCloseCategoryAfterRinging = "N";

        private static string strNotReadBarcodeCheckDigit = "N";

        private static string strScanner_8200 = "N";

        private static string strRandomWeightUPCCheckDigit = "2";

        private static string strWeightDisplayType;
        private static string strNTEPCert;

        private static string strFocusOnEditProduct;

        private static string strPrintTrainingMode = "N";

        private static string strCheckSmartGrocer = "N";

        private static string strSmartGrocerStore = "0";

        private static string strGPPrinter_Use = "";
        private static string strGPPrinter_PrinterName = "";
        private static double dblGPPrinter_LabelFooter = 0;



        private static string strCheckPriceSmart = "N";
        private static string strPriceSmartDirectoryPath = "";
        private static string strPriceSmartItemFile = "";
        private static string strPriceSmartIngredientFile = "";
        private static string strPriceSmartScaleCat = "";
        private static int intPriceSmartDeptID = 0;
        private static int intPriceSmartCatID = 0;
        private static int intPriceSmartScaleLabel = 0;
        private static string strPrintPriceSmartScaleLabel;
        private static string strPriceSmartCurrency;
        private static string strPriceSmartFtpHost;
        private static string strPriceSmartFtpUser;
        private static string strPriceSmartFtpPassword;
        private static string strPriceSmartFtpFolder;
        private static int intPriceSmartLogClearDays;

        private static string strDisplay4DigitWeight;


        private static string strDutchValleyVendorUser;
        private static string strDutchValleyVendorPassword;
        private static string strDutchValleyClientUser;
        private static string strDutchValleyClientPassword;
        private static int intDutchValleyDeptID;
        private static string strDutchValleyScaleCat;
        private static int intDutchValleyScaleLabel;
        private static int intDutchValleyFamily;
        private static int intDutchValleyVendor;


        // Online Booking


        private static string strEnableBooking;
        private static string strBooking_CustomerSiteURL;
        private static string strBooking_BusinessDetails;
        private static string strBooking_PrivacyNotice;
        private static string strBooking_Phone;
        private static string strBooking_Email;
        private static string strBooking_LinkedIn_Link;
        private static string strBooking_Twitter_Link;
        private static string strBooking_Facebook_Link;
        private static int intBooking_Lead_Day;
        private static int intBooking_Lead_Hour;
        private static int intBooking_Lead_Minute;
        private static int intBooking_Slot_Hour;
        private static int intBooking_Slot_Minute;
        private static string strBooking_Scheduling_NoLimit;
        private static string strBooking_Mon_Check;
        private static int intBooking_Mon_Start;
        private static int intBooking_Mon_End;
        private static string strBooking_Tue_Check;
        private static int intBooking_Tue_Start;
        private static int intBooking_Tue_End;
        private static string strBooking_Wed_Check;
        private static int intBooking_Wed_Start;
        private static int intBooking_Wed_End;
        private static string strBooking_Thu_Check;
        private static int intBooking_Thu_Start;
        private static int intBooking_Thu_End;
        private static string strBooking_Fri_Check;
        private static int intBooking_Fri_Start;
        private static int intBooking_Fri_End;
        private static string strBooking_Sat_Check;
        private static int intBooking_Sat_Start;
        private static int intBooking_Sat_End;
        private static string strBooking_Sun_Check;
        private static int intBooking_Sun_Start;
        private static int intBooking_Sun_End;

        private static string strBookingServer;
        private static string strBookingDB;
        private static string strBookingDBUser;
        private static string strBookingDBPassword;

        private static int intBookingScheduleWindowDay;
        private static int intBooking_Reminder_Hour;
        private static int intBooking_Reminder_Minute;


        // S T A R T   R E G I S T R A T I O N
        private static int intID;
        private static int intNoUsers;
        private static DateTime dtRegistrationDate;
        private static string strActivationKey;
        private static string strRegCompanyName;

        private static string strRegEMail;

        private static string strRegLicense;
        private static string strRegAddress1;
        private static string strRegAddress2;
        private static string strRegCity;
        private static string strRegZip;
        private static string strRegState;

        private static string strRegPOSAccess;
        private static string strRegScaleAccess;
        private static string strRegOrderingAccess;

        private static string strRegLabelDesigner;

        // No. of Users //
        private static int intUsersLoggedOn;

        private static string strDemoVersion;

        private static string strMainReceiptHeader;
        private static string strTotalReceiptHeader;

        private static string strReceiptHeaderCompany;
        private static string strReceiptHeaderAddress;

        // central export/import

        private static string strCentralExportImport;
        private static string strStoreCode;
        private static string strStoreName;
        private static string strExportFolderPath;

        public static string strStoreId { get; private set; } = "0";
        public static string strStoreHostId { get; private set; } = "0";

        private static string storeID;

        private static string strSMTPHost;
        private static string strSMTPUser;
        private static string strSMTPPassword;
        private static string strFromAddress;
        private static string strToAddress;

        private static string strCheckSecondMonitor;
        private static string strCheckSecondMonitorWithPOS;
        private static string strApplicationSecondMonitor;

        private static string strAdvtDispArea;
        private static string strAdvtScale;

        private static string strAdvtDispNo;
        private static string strAdvtDispTime;
        private static string strAdvtDispOrder;
        private static string strAdvtFont;
        private static string strAdvtColor;
        private static string strAdvtBackground;
        private static string strAdvtDir;

        private static int intPOSPrintTender;

        private static int intPurgeCutOffDay;

        private static string strSalesService;
        private static string strRentService;
        private static string strRepairService;
        private static string strPOSDefaultService;

        private static string pScaleDevice;
        private static string pScaleCategory;
        private static string pCOMPort= "(None)";
        private static string pCOMPort1= "(None)";
        private static string pBaudRate;
        private static string pDataBits;
        private static string pStopBits;
        private static string pParity;
        private static string pHandshake;
        private static string pTimeout;

        private static string strGridItemParam1;
        private static string strGridItemParam2;

        private static string strGridItemVendorParam1;
        private static string strGridItemVendorParam2;

        private static string strGridCustomerParam1;
        private static string strGridCustomerParam2;

        private static string strAutoSignoutTender;
        private static string strAutoSignout;
        private static int intAutoSignoutTime;

        private static string strFTPhost;
        private static string strFTPuser;
        private static string strFTPpswd;

        private static bool blActiveAdminPswdForMercury;

        private static string strElementZipProcessing;

        private static string strLoginExpireDue;

        private static string strShowSKUOnPOSButton;

        private static string strCloudServer;
        private static string strCloudDB;
        private static string strCloudUser;
        private static string strCloudPassword;

        private static string strDirectReceiptPrinting;

        private static string strReportEmail;
        private static int intSMTPServer;
        private static string strREHost;
        private static string strREUser;
        private static string strREPassword;
        private static string strRESSL;
        private static int intREPort;
        private static string strREFromAddress;
        private static string strREFromName;
        private static string strREReplyTo;

        private static string strCalculatorStyleKeyboard;

        private static int intLinkGL1;
        private static int intLinkGL2;
        private static int intLinkGL3;
        private static int intLinkGL4;
        private static int intLinkGL5;
        private static int intLinkGL6;

        private static string strNoSaleReceipt;
        private static string strHouseAccountBalanceInReceipt;

        private static string strGrad_ScaleType;
        private static string strGrad_GraduationText;
        private static double dblGrad_S_Range1;
        private static double dblGrad_S_Range2;
        private static double dblGrad_S_Graduation;
        private static double dblGrad_D_Range1;
        private static double dblGrad_D_Range2;
        private static double dblGrad_D_Graduation;
        private static string strS_Check2Digit;
        private static string strD_Check2Digit;


        private static string strShelfLifeDateExtend;

        private static string strSendCheckDigitForLabelsDotNet;

        private static string strAllowNegativeStoreCredit;



        private static string strShopifyAPIKey;
        private static string strShopifyPassword;
        private static string strShopifyStoreAddress;
        private static string strShopifyUserInstructions;

        private static string strPremiumPlan;

        // Internationalization

        private static string strCountryName;
        private static string strCurrencySymbol;
        private static string strDateFormat;


        private static string strSmallCurrencyName;
        private static string strBigCurrencyName;

        private static string strCoin1Name;
        private static string strCoin2Name;
        private static string strCoin3Name;
        private static string strCoin4Name;
        private static string strCoin5Name;
        private static string strCoin6Name;
        private static string strCoin7Name;

        private static string strCurrency1Name;
        private static string strCurrency2Name;
        private static string strCurrency3Name;
        private static string strCurrency4Name;
        private static string strCurrency5Name;
        private static string strCurrency6Name;
        private static string strCurrency7Name;
        private static string strCurrency8Name;
        private static string strCurrency9Name;
        private static string strCurrency10Name;

        private static string strCurrency1QuickTender;
        private static string strCurrency2QuickTender;
        private static string strCurrency3QuickTender;
        private static string strCurrency4QuickTender;
        private static string strCurrency5QuickTender;
        private static string strCurrency6QuickTender;
        private static string strCurrency7QuickTender;
        private static string strCurrency8QuickTender;
        private static string strCurrency9QuickTender;
        private static string strCurrency10QuickTender;

        private static string strDefautInternational;

        private static string strUseTouchKeyboardInAdmin;
        private static string strUseTouchKeyboardInPOS;


        private static string strPOSFunctionButtonShowHideState_User;

        private static string strEvoConnectFileLocation;
        private static string strEvoApi;

        private static string strPaymentsense_AccountName;
        private static string strPaymentsense_ApiKey;
        private static string strPaymentsense_SoftwareHouseId;
        private static string strPaymentsense_InstallerId;
        private static string strPaymentsense_Terminal;

        private static string strPaymentsense_Uri;

        private static string strCustomTemplatePrinter1;
        private static string strCustomTemplatePrinter2;
        private static string strCustomTemplatePrinter3;
        private static string strCustomTemplatePrinter4;
        private static string strCustomTemplatePrinter5;
        private static string strCustomTemplatePrinter6;
        private static string strCustomTemplatePrinter7;
        private static string strCustomTemplatePrinter8;
        private static string strCustomTemplatePrinter9;
        private static string strCustomTemplatePrinter10;
        private static string strCustomTemplatePrinter11;
        private static string strCustomTemplatePrinter12;
        private static string strCustomTemplatePrinter13;
        private static string strCustomTemplatePrinter14;
        private static string strCustomTemplatePrinter15;
        private static string strCustomTemplatePrinter16;
        private static string strCustomTemplatePrinter17;

        public static string CustomTemplatePrinter1
        {
            get { return strCustomTemplatePrinter1; }
            set { strCustomTemplatePrinter1 = value; }
        }

        public static string CustomTemplatePrinter2
        {
            get { return strCustomTemplatePrinter2; }
            set { strCustomTemplatePrinter2 = value; }
        }

        public static string CustomTemplatePrinter3
        {
            get { return strCustomTemplatePrinter3; }
            set { strCustomTemplatePrinter3 = value; }
        }

        public static string CustomTemplatePrinter4
        {
            get { return strCustomTemplatePrinter4; }
            set { strCustomTemplatePrinter4 = value; }
        }

        public static string CustomTemplatePrinter5
        {
            get { return strCustomTemplatePrinter5; }
            set { strCustomTemplatePrinter5 = value; }
        }

        public static string CustomTemplatePrinter6
        {
            get { return strCustomTemplatePrinter6; }
            set { strCustomTemplatePrinter6 = value; }
        }

        public static string CustomTemplatePrinter7
        {
            get { return strCustomTemplatePrinter7; }
            set { strCustomTemplatePrinter7 = value; }
        }

        public static string CustomTemplatePrinter8
        {
            get { return strCustomTemplatePrinter8; }
            set { strCustomTemplatePrinter8 = value; }
        }

        public static string CustomTemplatePrinter9
        {
            get { return strCustomTemplatePrinter9; }
            set { strCustomTemplatePrinter9 = value; }
        }

        public static string CustomTemplatePrinter10
        {
            get { return strCustomTemplatePrinter10; }
            set { strCustomTemplatePrinter10 = value; }
        }

        public static string CustomTemplatePrinter11
        {
            get { return strCustomTemplatePrinter11; }
            set { strCustomTemplatePrinter11 = value; }
        }

        public static string CustomTemplatePrinter12
        {
            get { return strCustomTemplatePrinter12; }
            set { strCustomTemplatePrinter12 = value; }
        }

        public static string CustomTemplatePrinter13
        {
            get { return strCustomTemplatePrinter13; }
            set { strCustomTemplatePrinter13 = value; }
        }

        public static string CustomTemplatePrinter14
        {
            get { return strCustomTemplatePrinter14; }
            set { strCustomTemplatePrinter14 = value; }
        }

        public static string CustomTemplatePrinter15
        {
            get { return strCustomTemplatePrinter15; }
            set { strCustomTemplatePrinter15 = value; }
        }

        public static string CustomTemplatePrinter16
        {
            get { return strCustomTemplatePrinter16; }
            set { strCustomTemplatePrinter16 = value; }
        }

        public static string CustomTemplatePrinter17
        {
            get { return strCustomTemplatePrinter17; }
            set { strCustomTemplatePrinter17 = value; }
        }


        #endregion

        #region Public Declaration

        public static string Paymentsense_Terminal
        {
            get { return strPaymentsense_Terminal; }
            set { strPaymentsense_Terminal = value; }
        }

        public static string Paymentsense_Uri
        {
            get { return strPaymentsense_Uri; }
            set { strPaymentsense_Uri = value; }
        }

        public static string Paymentsense_AccountName
        {
            get { return strPaymentsense_AccountName; }
            set { strPaymentsense_AccountName = value; }
        }

        public static string Paymentsense_ApiKey
        {
            get { return strPaymentsense_ApiKey; }
            set { strPaymentsense_ApiKey = value; }
        }

        public static string Paymentsense_SoftwareHouseId
        {
            get { return strPaymentsense_SoftwareHouseId; }
            set { strPaymentsense_SoftwareHouseId = value; }
        }

        public static string Paymentsense_InstallerId
        {
            get { return strPaymentsense_InstallerId; }
            set { strPaymentsense_InstallerId = value; }
        }

        public static string EvoConnectFileLocation
        {
            get { return strEvoConnectFileLocation; }
            set { strEvoConnectFileLocation = value; }
        }

        public static string EvoApi
        {
            get { return strEvoApi; }
            set { strEvoApi = value; }
        }

        public static string POSFunctionButtonShowHideState_User
        {
            get { return strPOSFunctionButtonShowHideState_User; }
            set { strPOSFunctionButtonShowHideState_User = value; }
        }

        public static int CentralCustomerID
        {
            get { return intCentralCustomerID; }
            set { intCentralCustomerID = value; }
        }

        public static string UseTouchKeyboardInAdmin
        {
            get { return strUseTouchKeyboardInAdmin; }
            set { strUseTouchKeyboardInAdmin = value; }
        }

        public static string UseTouchKeyboardInPOS
        {
            get { return strUseTouchKeyboardInPOS; }
            set { strUseTouchKeyboardInPOS = value; }
        }

        public static string ShopifyAPIKey
        {
            get { return strShopifyAPIKey; }
            set { strShopifyAPIKey = value; }
        }

        public static string ShopifyPassword
        {
            get { return strShopifyPassword; }
            set { strShopifyPassword = value; }
        }

        public static string ShopifyStoreAddress
        {
            get { return strShopifyStoreAddress; }
            set { strShopifyStoreAddress = value; }
        }

        
        public static string PremiumPlan
        {
            get { return strPremiumPlan; }
            set { strPremiumPlan = value; }
        }

        public static string ShopifyUserInstructions
        {
            get { return strShopifyUserInstructions; }
            set { strShopifyUserInstructions = value; }
        }

        public static string DefautInternational
        {
            get { return strDefautInternational; }
            set { strDefautInternational = value; }
        }

        public static string CountryName
        {
            get { return strCountryName; }
            set { strCountryName = value; }
        }

        public static string CurrencySymbol
        {
            get { return strCurrencySymbol; }
            set { strCurrencySymbol = value; }
        }

        public static string DateFormat
        {
            get { return strDateFormat; }
            set { strDateFormat = value; }
        }



        public static string SmallCurrencyName
        {
            get { return strSmallCurrencyName; }
            set { strSmallCurrencyName = value; }
        }

        public static string BigCurrencyName
        {
            get { return strBigCurrencyName; }
            set { strBigCurrencyName = value; }
        }

        public static string Coin1Name
        {
            get { return strCoin1Name; }
            set { strCoin1Name = value; }
        }

        public static string Coin2Name
        {
            get { return strCoin2Name; }
            set { strCoin2Name = value; }
        }

        public static string Coin3Name
        {
            get { return strCoin3Name; }
            set { strCoin3Name = value; }
        }

        public static string Coin4Name
        {
            get { return strCoin4Name; }
            set { strCoin4Name = value; }
        }

        public static string Coin5Name
        {
            get { return strCoin5Name; }
            set { strCoin5Name = value; }
        }

        public static string Coin6Name
        {
            get { return strCoin6Name; }
            set { strCoin6Name = value; }
        }

        public static string Coin7Name
        {
            get { return strCoin7Name; }
            set { strCoin7Name = value; }
        }

        public static string Currency1Name
        {
            get { return strCurrency1Name; }
            set { strCurrency1Name = value; }
        }

        public static string Currency2Name
        {
            get { return strCurrency2Name; }
            set { strCurrency2Name = value; }
        }

        public static string Currency3Name
        {
            get { return strCurrency3Name; }
            set { strCurrency3Name = value; }
        }

        public static string Currency4Name
        {
            get { return strCurrency4Name; }
            set { strCurrency4Name = value; }
        }

        public static string Currency5Name
        {
            get { return strCurrency5Name; }
            set { strCurrency5Name = value; }
        }

        public static string Currency6Name
        {
            get { return strCurrency6Name; }
            set { strCurrency6Name = value; }
        }

        public static string Currency7Name
        {
            get { return strCurrency7Name; }
            set { strCurrency7Name = value; }
        }

        public static string Currency8Name
        {
            get { return strCurrency8Name; }
            set { strCurrency8Name = value; }
        }

        public static string Currency9Name
        {
            get { return strCurrency9Name; }
            set { strCurrency9Name = value; }
        }

        public static string Currency10Name
        {
            get { return strCurrency10Name; }
            set { strCurrency10Name = value; }
        }

        public static string Currency1QuickTender
        {
            get { return strCurrency1QuickTender; }
            set { strCurrency1QuickTender = value; }
        }

        public static string Currency2QuickTender
        {
            get { return strCurrency2QuickTender; }
            set { strCurrency2QuickTender = value; }
        }

        public static string Currency3QuickTender
        {
            get { return strCurrency3QuickTender; }
            set { strCurrency3QuickTender = value; }
        }

        public static string Currency4QuickTender
        {
            get { return strCurrency4QuickTender; }
            set { strCurrency4QuickTender = value; }
        }

        public static string Currency5QuickTender
        {
            get { return strCurrency5QuickTender; }
            set { strCurrency5QuickTender = value; }
        }

        public static string Currency6QuickTender
        {
            get { return strCurrency6QuickTender; }
            set { strCurrency6QuickTender = value; }
        }

        public static string Currency7QuickTender
        {
            get { return strCurrency7QuickTender; }
            set { strCurrency7QuickTender = value; }
        }

        public static string Currency8QuickTender
        {
            get { return strCurrency8QuickTender; }
            set { strCurrency8QuickTender = value; }
        }

        public static string Currency9QuickTender
        {
            get { return strCurrency9QuickTender; }
            set { strCurrency9QuickTender = value; }
        }

        public static string Currency10QuickTender
        {
            get { return strCurrency10QuickTender; }
            set { strCurrency10QuickTender = value; }
        }

        public static string AllowNegativeStoreCredit
        {
            get { return strAllowNegativeStoreCredit; }
            set { strAllowNegativeStoreCredit = value; }
        }

        public static string PrintBlindDropCloseout
        {
            get { return strPrintBlindDropCloseout; }
            set { strPrintBlindDropCloseout = value; }
        }

        public static string PrintCloseoutInReceiptPrinter
        {
            get { return strPrintCloseoutInReceiptPrinter; }
            set { strPrintCloseoutInReceiptPrinter = value; }
        }

        public static int BookingScheduleWindowDay
        {
            get { return intBookingScheduleWindowDay; }
            set { intBookingScheduleWindowDay = value; }
        }

        public static int Booking_Reminder_Hour
        {
            get { return intBooking_Reminder_Hour; }
            set { intBooking_Reminder_Hour = value; }
        }

        public static int Booking_Reminder_Minute
        {
            get { return intBooking_Reminder_Minute; }
            set { intBooking_Reminder_Minute = value; }
        }

        public static string BookingServer
        {
            get { return strBookingServer; }
            set { strBookingServer = value; }
        }

        public static string BookingDB
        {
            get { return strBookingDB; }
            set { strBookingDB = value; }
        }

        public static string BookingDBUser
        {
            get { return strBookingDBUser; }
            set { strBookingDBUser = value; }
        }

        public static string BookingDBPassword
        {
            get { return strBookingDBPassword; }
            set { strBookingDBPassword = value; }
        }

        public static string EnableBooking
        {
            get { return strEnableBooking; }
            set { strEnableBooking = value; }
        }
        public static string Booking_CustomerSiteURL
        {
            get { return strBooking_CustomerSiteURL; }
            set { strBooking_CustomerSiteURL = value; }
        }
        public static string Booking_BusinessDetails
        {
            get { return strBooking_BusinessDetails; }
            set { strBooking_BusinessDetails = value; }
        }
        public static string Booking_PrivacyNotice
        {
            get { return strBooking_PrivacyNotice; }
            set { strBooking_PrivacyNotice = value; }
        }
        public static string Booking_Phone
        {
            get { return strBooking_Phone; }
            set { strBooking_Phone = value; }
        }
        public static string Booking_Email
        {
            get { return strBooking_Email; }
            set { strBooking_Email = value; }
        }
        public static string Booking_LinkedIn_Link
        {
            get { return strBooking_LinkedIn_Link; }
            set { strBooking_LinkedIn_Link = value; }
        }
        public static string Booking_Twitter_Link
        {
            get { return strBooking_Twitter_Link; }
            set { strBooking_Twitter_Link = value; }
        }
        public static string Booking_Scheduling_NoLimit
        {
            get { return strBooking_Scheduling_NoLimit; }
            set { strBooking_Scheduling_NoLimit = value; }
        }
        public static string Booking_Facebook_Link
        {
            get { return strBooking_Facebook_Link; }
            set { strBooking_Facebook_Link = value; }
        }
        public static string Booking_Mon_Check
        {
            get { return strBooking_Mon_Check; }
            set { strBooking_Mon_Check = value; }
        }
        public static string Booking_Tue_Check
        {
            get { return strBooking_Tue_Check; }
            set { strBooking_Tue_Check = value; }
        }
        public static string Booking_Wed_Check
        {
            get { return strBooking_Wed_Check; }
            set { strBooking_Wed_Check = value; }
        }
        public static string Booking_Thu_Check
        {
            get { return strBooking_Thu_Check; }
            set { strBooking_Thu_Check = value; }
        }
        public static string Booking_Fri_Check
        {
            get { return strBooking_Fri_Check; }
            set { strBooking_Fri_Check = value; }
        }
        public static string Booking_Sat_Check
        {
            get { return strBooking_Sat_Check; }
            set { strBooking_Sat_Check = value; }
        }
        public static string Booking_Sun_Check
        {
            get { return strBooking_Sun_Check; }
            set { strBooking_Sun_Check = value; }
        }

        public static int Booking_Lead_Day
        {
            get { return intBooking_Lead_Day; }
            set { intBooking_Lead_Day = value; }
        }

        public static int Booking_Lead_Hour
        {
            get { return intBooking_Lead_Hour; }
            set { intBooking_Lead_Hour = value; }
        }

        public static int Booking_Lead_Minute
        {
            get { return intBooking_Lead_Minute; }
            set { intBooking_Lead_Minute = value; }
        }

        public static int Booking_Slot_Hour
        {
            get { return intBooking_Slot_Hour; }
            set { intBooking_Slot_Hour = value; }
        }

        public static int Booking_Slot_Minute
        {
            get { return intBooking_Slot_Minute; }
            set { intBooking_Slot_Minute = value; }
        }

        public static int Booking_Mon_Start
        {
            get { return intBooking_Mon_Start; }
            set { intBooking_Mon_Start = value; }
        }
        public static int Booking_Mon_End
        {
            get { return intBooking_Mon_End; }
            set { intBooking_Mon_End = value; }
        }


        public static int Booking_Tue_Start
        {
            get { return intBooking_Tue_Start; }
            set { intBooking_Tue_Start = value; }
        }
        public static int Booking_Tue_End
        {
            get { return intBooking_Tue_End; }
            set { intBooking_Tue_End = value; }
        }


        public static int Booking_Wed_Start
        {
            get { return intBooking_Wed_Start; }
            set { intBooking_Wed_Start = value; }
        }
        public static int Booking_Wed_End
        {
            get { return intBooking_Wed_End; }
            set { intBooking_Wed_End = value; }
        }


        public static int Booking_Thu_Start
        {
            get { return intBooking_Thu_Start; }
            set { intBooking_Thu_Start = value; }
        }
        public static int Booking_Thu_End
        {
            get { return intBooking_Thu_End; }
            set { intBooking_Thu_End = value; }
        }


        public static int Booking_Fri_Start
        {
            get { return intBooking_Fri_Start; }
            set { intBooking_Fri_Start = value; }
        }
        public static int Booking_Fri_End
        {
            get { return intBooking_Fri_End; }
            set { intBooking_Fri_End = value; }
        }


        public static int Booking_Sat_Start
        {
            get { return intBooking_Sat_Start; }
            set { intBooking_Sat_Start = value; }
        }
        public static int Booking_Sat_End
        {
            get { return intBooking_Sat_End; }
            set { intBooking_Sat_End = value; }
        }


        public static int Booking_Sun_Start
        {
            get { return intBooking_Sun_Start; }
            set { intBooking_Sun_Start = value; }
        }
        public static int Booking_Sun_End
        {
            get { return intBooking_Sun_End; }
            set { intBooking_Sun_End = value; }
        }

        public static CultureInfo POS_Culture
        {
            get { return cultr_POS; }
            set { cultr_POS = value; }
        }

        public static string default_culture
        {
            get { return strdefault_culture; }
            set { strdefault_culture = value; }
        }

        public static string disableresource
        {
            get { return strdisableresource; }
            set { strdisableresource = value; }
        }

        public static string DataObjectCulture_SelectPLU
        {
            get { return strDataObjectCulture_SelectPLU; }
            set { strDataObjectCulture_SelectPLU = value; }
        }

        public static string DataObjectCulture_SelectSKU
        {
            get { return strDataObjectCulture_SelectSKU; }
            set { strDataObjectCulture_SelectSKU = value; }
        }

        public static string DataObjectCulture_All
        {
            get { return strDataObjectCulture_All; }
            set { strDataObjectCulture_All = value; }
        }

        public static string DataObjectCulture_None
        {
            get { return strDataObjectCulture_None; }
            set { strDataObjectCulture_None = value; }
        }

        public static string DataObjectCulture_ADMIN
        {
            get { return strDataObjectCulture_ADMIN; }
            set { strDataObjectCulture_ADMIN = value; }
        }

        public static string DataObjectCulture_Administrator
        {
            get { return strDataObjectCulture_Administrator; }
            set { strDataObjectCulture_Administrator = value; }
        }

        public static string DataObjectCulture_AllDepartments
        {
            get { return strDataObjectCulture_AllDepartments; }
            set { strDataObjectCulture_AllDepartments = value; }
        }


        public static void GetDataobjectCulture()
        {
            strDataObjectCulture_SelectSKU = Properties.Resources.SelectSKU;
            strDataObjectCulture_SelectPLU = Properties.Resources.SelectPLU;
            strDataObjectCulture_All = Properties.Resources.All;
            strDataObjectCulture_None = Properties.Resources.None;
            strDataObjectCulture_ADMIN = Properties.Resources.Admin;
            strDataObjectCulture_Administrator = Properties.Resources.Administrator;
            strDataObjectCulture_AllDepartments = Properties.Resources.AllDepartments;
        }

        public static CultureInfo SetCulture()
        {
            CultureInfo customculture = new CultureInfo(strdefault_culture);
            customculture.NumberFormat.PercentDecimalSeparator = ".";
            customculture.NumberFormat.NumberDecimalSeparator = ".";

            return customculture;
        }

        public static string GPPrinter_Use
        {
            get { return strGPPrinter_Use; }
            set { strGPPrinter_Use = value; }
        }

        public static string GPPrinter_PrinterName
        {
            get { return strGPPrinter_PrinterName; }
            set { strGPPrinter_PrinterName = value; }
        }

        public static double GPPrinter_LabelFooter
        {
            get { return dblGPPrinter_LabelFooter; }
            set { dblGPPrinter_LabelFooter = value; }
        }

        public static string Display4DigitWeight
        {
            get { return strDisplay4DigitWeight; }
            set { strDisplay4DigitWeight = value; }
        }

        public static string Grad_ScaleType
        {
            get { return strGrad_ScaleType; }
            set { strGrad_ScaleType = value; }
        }

        public static string PrintTrainingMode
        {
            get { return strPrintTrainingMode; }
            set { strPrintTrainingMode = value; }
        }

        public static string CheckSmartGrocer
        {
            get { return strCheckSmartGrocer; }
            set { strCheckSmartGrocer = value; }
        }

        public static string SmartGrocerStore
        {
            get { return strSmartGrocerStore; }
            set { strSmartGrocerStore = value; }
        }

        public static string Grad_GraduationText
        {
            get { return strGrad_GraduationText; }
            set { strGrad_GraduationText = value; }
        }

        public static string S_Check2Digit
        {
            get { return strS_Check2Digit; }
            set { strS_Check2Digit = value; }
        }

        public static string D_Check2Digit
        {
            get { return strD_Check2Digit; }
            set { strD_Check2Digit = value; }
        }

        public static double Grad_S_Range1
        {
            get { return dblGrad_S_Range1; }
            set { dblGrad_S_Range1 = value; }
        }

        public static double Grad_S_Range2
        {
            get { return dblGrad_S_Range2; }
            set { dblGrad_S_Range2 = value; }
        }

        public static double Grad_S_Graduation
        {
            get { return dblGrad_S_Graduation; }
            set { dblGrad_S_Graduation = value; }
        }

        public static double Grad_D_Range1
        {
            get { return dblGrad_D_Range1; }
            set { dblGrad_D_Range1 = value; }
        }

        public static double Grad_D_Range2
        {
            get { return dblGrad_D_Range2; }
            set { dblGrad_D_Range2 = value; }
        }

        public static double Grad_D_Graduation
        {
            get { return dblGrad_D_Graduation; }
            set { dblGrad_D_Graduation = value; }
        }

        public static int LayawayPaymentOption
        {
            get { return intLayawayPaymentOption; }
            set { intLayawayPaymentOption = value; }
        }

        public static int PrecidiaProcessTimeout
        {
            get { return intPrecidiaProcessTimeout; }
            set { intPrecidiaProcessTimeout = value; }
        }

        public static string FocusOnEditProduct
        {
            get { return strFocusOnEditProduct; }
            set { strFocusOnEditProduct = value; }
        }

        public static string RandomWeightUPCCheckDigit
        {
            get { return strRandomWeightUPCCheckDigit; }
            set { strRandomWeightUPCCheckDigit = value; }
        }

        public static string NotReadBarcodeCheckDigit
        {
            get { return strNotReadBarcodeCheckDigit; }
            set { strNotReadBarcodeCheckDigit = value; }
        }

        public static string Scanner_8200
        {
            get { return strScanner_8200; }
            set { strScanner_8200 = value; }
        }

        public static string CloseCategoryAfterRinging
        {
            get { return strCloseCategoryAfterRinging; }
            set { strCloseCategoryAfterRinging = value; }
        }

        public static string CalculatorStyleKeyboard
        {
            get { return strCalculatorStyleKeyboard; }
            set { strCalculatorStyleKeyboard = value; }
        }

        public static string Print2TicketsForRepair
        {
            get { return strPrint2TicketsForRepair; }
            set { strPrint2TicketsForRepair = value; }
        }

        public static string DisplayLaneClosed
        {
            get { return strDisplayLaneClosed; }
            set { strDisplayLaneClosed = value; }
        }

        public static string AddGallon
        {
            get { return strAddGallon; }
            set { strAddGallon = value; }
        }

        public static string AutoDisplayItemImage
        {
            get { return strAutoDisplayItemImage; }
            set { strAutoDisplayItemImage = value; }
        }

        public static string OtherTerminalCloseout
        {
            get { return strOtherTerminalCloseout; }
            set { strOtherTerminalCloseout = value; }
        }

        public static int SMTPServer
        {
            get { return intSMTPServer; }
            set { intSMTPServer = value; }
        }

        public static string REHost
        {
            get { return strREHost; }
            set { strREHost = value; }
        }

        public static string REUser
        {
            get { return strREUser; }
            set { strREUser = value; }
        }

        public static string REPassword
        {
            get { return strREPassword; }
            set { strREPassword = value; }
        }

        public static string RESSL
        {
            get { return strRESSL; }
            set { strRESSL = value; }
        }

        public static string REFromAddress
        {
            get { return strREFromAddress; }
            set { strREFromAddress = value; }
        }

        public static string REFromName
        {
            get { return strREFromName; }
            set { strREFromName = value; }
        }

        public static string REReplyTo
        {
            get { return strREReplyTo; }
            set { strREReplyTo = value; }
        }

        public static int REPort
        {
            get { return intREPort; }
            set { intREPort = value; }
        }

        public static string ReportEmail
        {
            get { return strReportEmail; }
            set { strReportEmail = value; }
        }

        public static string EasyTendering
        {
            get { return strEasyTendering; }
            set { strEasyTendering = value; }
        }

        public static string ShowSaleSaveInReceipt
        {
            get { return strShowSaleSaveInReceipt; }
            set { strShowSaleSaveInReceipt = value; }
        }

        public static string ShowFeesInReceipt
        {
            get { return strShowFeesInReceipt; }
            set { strShowFeesInReceipt = value; }
        }

        public static string ShowFoodStampTotal
        {
            get { return strShowFoodStampTotal; }
            set { strShowFoodStampTotal = value; }
        }

        public static string AcceptTips
        {
            get { return strAcceptTips; }
            set { strAcceptTips = value; }
        }

        public static string ShowTipsInReceipt
        {
            get { return strShowTipsInReceipt; }
            set { strShowTipsInReceipt = value; }
        }

        public static int POSPrintInvoice
        {
            get { return intPOSPrintInvoice; }
            set { intPOSPrintInvoice = value; }
        }

        public static string DirectReceiptPrinting
        {
            get { return strDirectReceiptPrinting; }
            set { strDirectReceiptPrinting = value; }
        }

        public static string DatacapServer
        {
            get { return strDatacapServer; }
            set { strDatacapServer = value; }
        }

        public static string DatacapMID
        {
            get { return strDatacapMID; }
            set { strDatacapMID = value; }
        }

        public static string DatacapSecureDeviceID
        {


            get { return strDatacapSecureDeviceID; }
            set { strDatacapSecureDeviceID = value; }
        }

        public static string DatacapCOMPort
        {
            get { return strDatacapCOMPort; }
            set { strDatacapCOMPort = value; }
        }

        public static string DatacapPINPad
        {
            get { return strDatacapPINPad; }
            set { strDatacapPINPad = value; }
        }

        public static double DatacapSignAmount
        {
            get { return dblDatacapSignAmount; }
            set { dblDatacapSignAmount = value; }
        }

        public static int DatacapCardEntryMode
        {
            get { return intDatacapCardEntryMode; }
            set { intDatacapCardEntryMode = value; }
        }

        public static string DatacapSignatureDevice
        {
            get { return strDatacapSignatureDevice; }
            set { strDatacapSignatureDevice = value; }
        }

        public static string DatacapSignatureDeviceCOMPort
        {
            get { return strDatacapSignatureDeviceCOMPort; }
            set { strDatacapSignatureDeviceCOMPort = value; }
        }

        public static string PrecidiaClientMAC
        {
            get { return strPrecidiaClientMAC; }
            set { strPrecidiaClientMAC = value; }
        }

        public static string PrecidiaPOSLynxMAC
        {
            get { return strPrecidiaPOSLynxMAC; }
            set { strPrecidiaPOSLynxMAC = value; }
        }


        public static string DatacapEMVServerIP
        {
            get { return strDatacapEMVServerIP; }
            set { strDatacapEMVServerIP = value; }
        }

        public static string DatacapEMVServerPort
        {
            get { return strDatacapEMVServerPort; }
            set { strDatacapEMVServerPort = value; }
        }

        public static string DatacapEMVMID
        {
            get { return strDatacapEMVMID; }
            set { strDatacapEMVMID = value; }
        }

        public static string DatacapEMVUserTrace
        {
            get { return strDatacapEMVUserTrace; }
            set { strDatacapEMVUserTrace = value; }
        }

        public static string DatacapEMVTerminalID
        {
            get { return strDatacapEMVTerminalID; }
            set { strDatacapEMVTerminalID = value; }
        }

        public static string DatacapEMVOperatorID
        {
            get { return strDatacapEMVOperatorID; }
            set { strDatacapEMVOperatorID = value; }
        }

        public static string DatacapEMVSecurityDevice
        {
            get { return strDatacapEMVSecurityDevice; }
            set { strDatacapEMVSecurityDevice = value; }
        }

        public static string DatacapEMVCOMPort
        {
            get { return strDatacapEMVCOMPort; }
            set { strDatacapEMVCOMPort = value; }
        }

        public static string DatacapEMVManual
        {
            get { return strDatacapEMVManual; }
            set { strDatacapEMVManual = value; }
        }

        public static string DatacapEMVToken
        {
            get { return strDatacapEMVToken; }
            set { strDatacapEMVToken = value; }
        }

        public static int PrecidiaPort
        {
            get { return intPrecidiaPort; }
            set { intPrecidiaPort = value; }
        }

        public static string PrecidiaDataPath
        {
            get { return strPrecidiaDataPath; }
            set { strPrecidiaDataPath = value; }
        }

        public static string PrecidiaUsePINPad
        {
            get { return strPrecidiaUsePINPad; }
            set { strPrecidiaUsePINPad = value; }
        }

        public static string PrecidiaLaneOpen
        {
            get { return strPrecidiaLaneOpen; }
            set { strPrecidiaLaneOpen = value; }
        }

        public static string PrecidiaRRLog
        {
            get { return strPrecidiaRRLog; }
            set { strPrecidiaRRLog = value; }
        }

        public static string PrecidiaSign
        {
            get { return strPrecidiaSign; }
            set { strPrecidiaSign = value; }
        }

        public static double PrecidiaSignAmount
        {
            get { return dblPrecidiaSignAmount; }
            set { dblPrecidiaSignAmount = value; }
        }

        public static double MercurySignAmount
        {
            get { return dblMercurySignAmount; }
            set { dblMercurySignAmount = value; }
        }

        public static string CloudServer
        {
            get { return strCloudServer; }
            set { strCloudServer = value; }
        }

        public static string CloudDB
        {
            get { return strCloudDB; }
            set { strCloudDB = value; }
        }

        public static string CloudUser
        {
            get { return strCloudUser; }
            set { strCloudUser = value; }
        }

        public static string CloudPassword
        {
            get { return strCloudPassword; }
            set { strCloudPassword = value; }
        }

        public static string ShowSKUOnPOSButton
        {
            get { return strShowSKUOnPOSButton; }
            set { strShowSKUOnPOSButton = value; }
        }

        public static string UseCustomerNameInLabelPrint
        {
            get { return strUseCustomerNameInLabelPrint; }
            set { strUseCustomerNameInLabelPrint = value; }
        }

        public static string DefaultCloseoutPrinter
        {
            get { return strDefaultCloseoutPrinter; }
            set { strDefaultCloseoutPrinter = value; }
        }

        public static string LoginExpireDue
        {
            get { return strLoginExpireDue; }
            set { strLoginExpireDue = value; }
        }

        public static string ElementZipProcessing
        {
            get { return strElementZipProcessing; }
            set { strElementZipProcessing = value; }
        }

        public static bool ActiveAdminPswdForMercury
        {
            get { return blActiveAdminPswdForMercury; }
            set { blActiveAdminPswdForMercury = value; }
        }

        public static string FTPhost
        {
            get { return strFTPhost; }
            set { strFTPhost = value; }
        }

        public static string FTPuser
        {
            get { return strFTPuser; }
            set { strFTPuser = value; }
        }

        public static string FTPpswd
        {
            get { return strFTPpswd; }
            set { strFTPpswd = value; }
        }

        public static string AutoSignout
        {
            get { return strAutoSignout; }
            set { strAutoSignout = value; }
        }

        public static int AutoSignoutTime
        {
            get { return intAutoSignoutTime; }
            set { intAutoSignoutTime = value; }
        }

        public static string AutoSignoutTender
        {
            get { return strAutoSignoutTender; }
            set { strAutoSignoutTender = value; }
        }

        public static string GridCustomerParam2
        {
            get { return strGridCustomerParam2; }
            set { strGridCustomerParam2 = value; }
        }

        public static string GridCustomerParam1
        {
            get { return strGridCustomerParam1; }
            set { strGridCustomerParam1 = value; }
        }

        public static string GridItemParam2
        {
            get { return strGridItemParam2; }
            set { strGridItemParam2 = value; }
        }

        public static string GridItemParam1
        {
            get { return strGridItemParam1; }
            set { strGridItemParam1 = value; }
        }

        public static string GridItemVendorParam1
        {
            get { return strGridItemVendorParam1; }
            set { strGridItemVendorParam1 = value; }
        }

        public static string GridItemVendorParam2
        {
            get { return strGridItemVendorParam2; }
            set { strGridItemVendorParam2 = value; }
        }

        public static string CashDrawerCode
        {
            get { return strCashDrawerCode; }
            set { strCashDrawerCode = value; }
        }

        public static string BarTenderLabelPrint
        {
            get { return strBarTenderLabelPrint; }
            set { strBarTenderLabelPrint = value; }
        }

        public static string SalesService
        {
            get { return strSalesService; }
            set { strSalesService = value; }
        }

        public static string RentService
        {
            get { return strRentService; }
            set { strRentService = value; }
        }

        public static string RepairService
        {
            get { return strRepairService; }
            set { strRepairService = value; }
        }

        public static string POSDefaultService
        {
            get { return strPOSDefaultService; }
            set { strPOSDefaultService = value; }
        }

        public static int PurgeCutOffDay
        {
            get { return intPurgeCutOffDay; }
            set { intPurgeCutOffDay = value; }
        }

        public static int POSPrintTender
        {
            get { return intPOSPrintTender; }
            set { intPOSPrintTender = value; }
        }

        public static string CheckSecondMonitor
        {
            get { return strCheckSecondMonitor; }
            set { strCheckSecondMonitor = value; }
        }

        public static string CheckSecondMonitorWithPOS
        {
            get { return strCheckSecondMonitorWithPOS; }
            set { strCheckSecondMonitorWithPOS = value; }
        }

        public static string ApplicationSecondMonitor
        {
            get { return strApplicationSecondMonitor; }
            set { strApplicationSecondMonitor = value; }
        }

        public static int PaymentGateway
        {
            get { return intPaymentGateway; }
            set { intPaymentGateway = value; }
        }

        public static string MercuryHPMerchantID
        {
            get { return strMercuryHPMerchantID; }
            set { strMercuryHPMerchantID = value; }
        }

        public static string MercuryHPUserID
        {
            get { return strMercuryHPUserID; }
            set { strMercuryHPUserID = value; }
        }

        public static string MercuryHPPort
        {
            get { return strMercuryHPPort; }
            set { strMercuryHPPort = value; }
        }

        public static int DisplayLogoOrWeb
        {
            get { return intDisplayLogoOrWeb; }
            set { intDisplayLogoOrWeb = value; }
        }

        public static string DisplayURL
        {
            get { return strDisplayURL; }
            set { strDisplayURL = value; }
        }

        public static string BrowsePermission
        {
            get { return strBrowsePermission; }
            set { strBrowsePermission = value; }
        }

        public static string ReceiptPrintOnReturn
        {
            get { return strReceiptPrintOnReturn; }
            set { strReceiptPrintOnReturn = value; }
        }

        public static SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public static string DemoVersion
        {
            get { return strDemoVersion; }
            set { strDemoVersion = value; }
        }

        // Setup
        public static string ForcedLogin
        {
            get { return strForcedLogin; }
            set { strForcedLogin = value; }
        }

        public static string Use4Decimal
        {
            get { return strUse4Decimal; }
            set { strUse4Decimal = value; }
        }

        public static int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public static int CustomerInfo
        {
            get { return intCustomerInfo; }
            set { intCustomerInfo = value; }
        }

        public static int VendorUpdate
        {
            get { return intVendorUpdate; }
            set { intVendorUpdate = value; }
        }

        public static int CloseoutOption
        {
            get { return intCloseoutOption; }
            set { intCloseoutOption = value; }
        }

        public static int SignINOption
        {
            get { return intSignINOption; }
            set { intSignINOption = value; }
        }

        public static int MonthsInvJrnl
        {
            get { return intMonthsInvJrnl; }
            set { intMonthsInvJrnl = value; }
        }

        public static string BlindCountPreview
        {
            get { return strBlindCountPreview; }
            set { strBlindCountPreview = value; }
        }

        public static string SalesByHour
        {
            get { return strSalesByHour; }
            set { strSalesByHour = value; }
        }

        public static string SalesByDept
        {
            get { return strSalesByDept; }
            set { strSalesByDept = value; }
        }

        public static string POSAcceptCheck
        {
            get { return strPOSAcceptCheck; }
            set { strPOSAcceptCheck = value; }
        }

        public static string POSDisplayChangeDue
        {
            get { return strPOSDisplayChangeDue; }
            set { strPOSDisplayChangeDue = value; }
        }

        public static string POSIDRequired
        {
            get { return strPOSIDRequired; }
            set { strPOSIDRequired = value; }
        }

        public static string POSRedirectToChangeDue
        {
            get { return strPOSRedirectToChangeDue; }
            set { strPOSRedirectToChangeDue = value; }
        }

        public static string POSShowGiftCertBalance
        {
            get { return strPOSShowGiftCertBalance; }
            set { strPOSShowGiftCertBalance = value; }
        }

        public static string NoPriceOnLabelDefault
        {
            get { return strNoPriceOnLabelDefault; }
            set { strNoPriceOnLabelDefault = value; }
        }

        public static string QuantityRequired
        {
            get { return strQuantityRequired; }
            set { strQuantityRequired = value; }
        }

        public static string UseInvJrnl
        {
            get { return strUseInvJrnl; }
            set { strUseInvJrnl = value; }
        }

        public static double GiftCertMaxChange
        {
            get { return dblGiftCertMaxChange; }
            set { dblGiftCertMaxChange = value; }
        }

        public static int POSMoreFunctionAlignment
        {
            get { return intPOSMoreFunctionAlignment; }
            set { intPOSMoreFunctionAlignment = value; }
        }

        public static string Company
        {
            get { return strCompany; }
            set { strCompany = value; }
        }

        public static string CompanyAddress
        {
            get { return strCompanyAddress; }
            set { strCompanyAddress = value; }
        }

        public static string Address1
        {
            get { return strAddress1; }
            set { strAddress1 = value; }
        }

        public static string Address2
        {
            get { return strAddress2; }
            set { strAddress2 = value; }
        }

        public static string City
        {
            get { return strCity; }
            set { strCity = value; }
        }

        public static string State
        {
            get { return strState; }
            set { strState = value; }
        }

        public static string PostalCode
        {
            get { return strPostalCode; }
            set { strPostalCode = value; }
        }

        public static string Phone
        {
            get { return strPhone; }
            set { strPhone = value; }
        }

        public static string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }

        public static string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public static string WebAddress
        {
            get { return strWebAddress; }
            set { strWebAddress = value; }
        }

        public static string ReportHeader
        {
            get { return strReportHeader; }
            set { strReportHeader = value; }
        }

        public static string ReportHeader_Company
        {
            get { return strReportHeader_Company; }
            set { strReportHeader_Company = value; }
        }

        public static string ReportHeader_Address
        {
            get { return strReportHeader_Address; }
            set { strReportHeader_Address = value; }
        }

        public static string ReceiptHeader_Company
        {
            get { return strReceiptHeader_Company; }
            set { strReceiptHeader_Company = value; }
        }

        public static string ReceiptHeader_Address
        {
            get { return strReceiptHeader_Address; }
            set { strReceiptHeader_Address = value; }
        }

        public static string ReportPrinterName
        {
            get { return strReportPrinterName; }
            set { strReportPrinterName = value; }
        }

        public static string ReceiptPrinterName
        {
            get { return strReceiptPrinterName; }
            set { strReceiptPrinterName = value; }
        }

        public static string LabelPrinterType
        {
            get { return strLabelPrinterType; }
            set { strLabelPrinterType = value; }
        }

        public static string LabelPrinterName
        {
            get { return strLabelPrinterName; }
            set { strLabelPrinterName = value; }
        }

        public static string ReceiptHeader
        {
            get { return strReceiptHeader; }
            set { strReceiptHeader = value; }
        }

        public static string ReceiptFooter
        {
            get { return strReceiptFooter; }
            set { strReceiptFooter = value; }
        }

        public static string ReceiptLayawayPolicy
        {
            get { return strReceiptLayawayPolicy; }
            set { strReceiptLayawayPolicy = value; }
        }

        public static string PoleScreen
        {
            get { return strPoleScreen; }
            set { strPoleScreen = value; }
        }

        public static double LayawayDepositPercent
        {
            get { return dblLayawayDepositPercent; }
            set { dblLayawayDepositPercent = value; }
        }

        public static double BottleDeposit
        {
            get { return dblBottleDeposit; }
            set { dblBottleDeposit = value; }
        }

        public static double MaxScaleWeight
        {
            get { return dblMaxScaleWeight; }
            set { dblMaxScaleWeight = value; }
        }

        public static int LayawaysDue
        {
            get { return intLayawaysDue; }
            set { intLayawaysDue = value; }
        }

        public static int LayawayReceipts
        {
            get { return intLayawayReceipts; }
            set { intLayawayReceipts = value; }
        }

        public static string FormSkin
        {
            get { return strFormSkin; }
            set { strFormSkin = value; }
        }

        public static string StoreInfo
        {
            get { return strStoreInfo; }
            set { strStoreInfo = value; }
        }

        public static string POSDisplayProductImage
        {
            get { return strPOSDisplayProductImage; }
            set { strPOSDisplayProductImage = value; }
        }

        public static int UsePriceLevel
        {
            get { return intUsePriceLevel; }
            set { intUsePriceLevel = value; }
        }

        public static string TerminalName
        {
            get { return strTerminalName; }
            set { strTerminalName = value; }
        }

        public static string TerminalTcpIp
        {
            get { return strTerminalTcpIp; }
            set { strTerminalTcpIp = value; }
        }

        public static string POSCardPayment
        {
            get { return strPOSCardPayment; }
            set { strPOSCardPayment = value; }
        }

        public static string POSCardPaymentMerged
        {
            get { return strPOSCardPaymentMerged; }
            set { strPOSCardPaymentMerged = value; }
        }

        public static string GeneralReceiptPrint
        {
            get { return strGeneralReceiptPrint; }
            set { strGeneralReceiptPrint = value; }
        }

        public static string PreprintedReceipt
        {
            get { return strPreprintedReceipt; }
            set { strPreprintedReceipt = value; }
        }

        public static string AutoPO
        {
            get { return strAutoPO; }
            set { strAutoPO = value; }
        }

        public static string AutoCustomer
        {
            get { return strAutoCustomer; }
            set { strAutoCustomer = value; }
        }

        public static string AutoGC
        {
            get { return strAutoGC; }
            set { strAutoGC = value; }
        }

        public static string SingleGC
        {
            get { return strSingleGC; }
            set { strSingleGC = value; }
        }

        public static string LineDisplayDevice
        {
            get { return strLineDisplayDevice; }
            set { strLineDisplayDevice = value; }
        }

        public static string LineDisplayDeviceType
        {
            get { return strLineDisplayDeviceType; }
            set { strLineDisplayDeviceType = value; }
        }

        public static string LineDisplayDeviceSerial
        {
            get { return strLineDisplayDeviceSerial; }
            set { strLineDisplayDeviceSerial = value; }
        }

        public static string AgreeLicenseSecondTime
        {
            get { return strAgreeLicenseSecondTime; }
            set { strAgreeLicenseSecondTime = value; }
        }

        public static string AgreeLicenseFirstTime
        {
            get { return dtAgreeLicenseFirstTime; }
            set { dtAgreeLicenseFirstTime = value; }
        }

        // Registration Details

        public static int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public static int NoUsers
        {
            get { return intNoUsers; }
            set { intNoUsers = value; }
        }

        public static DateTime RegistrationDate
        {
            get { return dtRegistrationDate; }
            set { dtRegistrationDate = value; }
        }

        public static string ActivationKey
        {
            get { return strActivationKey; }
            set { strActivationKey = value; }
        }

        public static string RegCompanyName
        {
            get { return strRegCompanyName; }
            set { strRegCompanyName = value; }
        }

        public static string RegAddress1
        {
            get { return strRegAddress1; }
            set { strRegAddress1 = value; }
        }

        public static string RegAddress2
        {
            get { return strRegAddress2; }
            set { strRegAddress2 = value; }
        }

        public static string RegCity
        {
            get { return strRegCity; }
            set { strRegCity = value; }
        }

        public static string RegZip
        {
            get { return strRegZip; }
            set { strRegZip = value; }
        }

        public static string RegState
        {
            get { return strRegState; }
            set { strRegState = value; }
        }

        public static string RegEMail
        {
            get { return strRegEMail; }
            set { strRegEMail = value; }
        }

        public static string RegPOSAccess
        {
            get { return strRegPOSAccess; }
            set { strRegPOSAccess = value; }
        }

        public static string RegScaleAccess
        {
            get { return strRegScaleAccess; }
            set { strRegScaleAccess = value; }
        }

        public static string RegOrderingAccess
        {
            get { return strRegOrderingAccess; }
            set { strRegOrderingAccess = value; }
        }

        public static string RegLabelDesigner
        {
            get { return strRegLabelDesigner; }
            set { strRegLabelDesigner = value; }
        }

        public static int UsersLoggedOn
        {
            get { return intUsersLoggedOn; }
            set { intUsersLoggedOn = value; }
        }

        public static bool IsPOSSelected
        {
            get { return blIsPOSSelected; }
            set { blIsPOSSelected = value; }
        }

        public static string RegLicense
        {
            get { return strRegLicense; }
            set { strRegLicense = value; }
        }

        public static string CloseoutExport
        {
            get { return strCloseoutExport; }
            set { strCloseoutExport = value; }
        }

        public static string CardPublisherName
        {
            get { return strCardPublisherName; }
            set { strCardPublisherName = value; }
        }

        public static string CardPublisherEMail
        {
            get { return strCardPublisherEMail; }
            set { strCardPublisherEMail = value; }
        }

        public static string ElementHPAcceptorID
        {
            get { return strElementHPAcceptorID; }
            set { strElementHPAcceptorID = value; }
        }

        public static string ElementHPAccountID
        {
            get { return strElementHPAccountID; }
            set { strElementHPAccountID = value; }
        }

        public static string ElementHPAccountToken
        {
            get { return strElementHPAccountToken; }
            set { strElementHPAccountToken = value; }
        }

        public static string ElementHPApplicationID
        {
            get { return strElementHPApplicationID; }
            set { strElementHPApplicationID = value; }
        }

        public static int ElementHPMode
        {
            get { return intElementHPMode; }
            set { intElementHPMode = value; }
        }

        public static string ElementHPTerminalID
        {
            get { return strElementHPTerminalID; }
            set { strElementHPTerminalID = value; }
        }

        public static string MainReceiptHeader
        {
            get { return strMainReceiptHeader; }
            set { strMainReceiptHeader = value; }
        }

        public static string TotalReceiptHeader
        {
            get { return strTotalReceiptHeader; }
            set { strTotalReceiptHeader = value; }
        }


        public static string ReceiptHeaderCompany
        {
            get { return strReceiptHeaderCompany; }
            set { strReceiptHeaderCompany = value; }
        }

        public static string ReceiptHeaderAddress
        {
            get { return strReceiptHeaderAddress; }
            set { strReceiptHeaderAddress = value; }
        }

        public static string CentralExportImport
        {
            get { return strCentralExportImport; }
            set { strCentralExportImport = value; }
        }

        public static string RunScaleControllerOnHostImport
        {
            get { return strRunScaleControllerOnHostImport; }
            set { strRunScaleControllerOnHostImport = value; }
        }

        public static string StoreCode
        {
            get { return strStoreCode; }
            set { strStoreCode = value; }
        }

        public static string StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }

        public static string ExportFolderPath
        {
            get { return strExportFolderPath; }
            set { strExportFolderPath = value; }
        }

        public static string StoreName
        {
            get { return strStoreName; }
            set { strStoreName = value; }
        }

        public static string SMTPHost
        {
            get { return strSMTPHost; }
            set { strSMTPHost = value; }
        }

        public static string SMTPPassword
        {
            get { return strSMTPPassword; }
            set { strSMTPPassword = value; }
        }

        public static string SMTPUser
        {
            get { return strSMTPUser; }
            set { strSMTPUser = value; }
        }

        public static string FromAddress
        {
            get { return strFromAddress; }
            set { strFromAddress = value; }
        }

        public static string ToAddress
        {
            get { return strToAddress; }
            set { strToAddress = value; }
        }

        public static string IsDuplicateInvoice
        {
            get { return strIsDuplicateInvoice; }
            set { strIsDuplicateInvoice = value; }
        }

        public static string PrintDuplicateGiftCertSaleReceipt
        {
            get { return strPrintDuplicateGiftCertSaleReceipt; }
            set { strPrintDuplicateGiftCertSaleReceipt = value; }
        }

        public static string AdvtDispArea
        {
            get { return strAdvtDispArea; }
            set { strAdvtDispArea = value; }
        }

        public static string AdvtScale
        {
            get { return strAdvtScale; }
            set { strAdvtScale = value; }
        }

        public static string AdvtDispNo
        {
            get { return strAdvtDispNo; }
            set { strAdvtDispNo = value; }
        }
        public static string AdvtDispTime
        {
            get { return strAdvtDispTime; }
            set { strAdvtDispTime = value; }
        }
        public static string AdvtDispOrder
        {
            get { return strAdvtDispOrder; }
            set { strAdvtDispOrder = value; }
        }
        public static string AdvtFont
        {
            get { return strAdvtFont; }
            set { strAdvtFont = value; }
        }
        public static string AdvtColor
        {
            get { return strAdvtColor; }
            set { strAdvtColor = value; }
        }
        public static string AdvtBackground
        {
            get { return strAdvtBackground; }
            set { strAdvtBackground = value; }
        }
        public static string AdvtDir
        {
            get { return strAdvtDir; }
            set { strAdvtDir = value; }
        }

        public static string ScaleDevice
        {
            get { return pScaleDevice; }
            set { pScaleDevice = value; }
        }

        public static string ScaleCategory
        {
            get { return pScaleCategory; }
            set { pScaleCategory = value; }
        }

        public static string COMPort
        {
            get { return pCOMPort1; }
            set { pCOMPort1 = value; }
        }
        public static string COMPort1
        {
            get { return pCOMPort; }
            set { pCOMPort = value; }
        }
        public static string BaudRate
        {
            get { return pBaudRate; }
            set { pBaudRate = value; }
        }
        public static string DataBits
        {
            get { return pDataBits; }
            set { pDataBits = value; }
        }
        public static string StopBits
        {
            get { return pStopBits; }
            set { pStopBits = value; }
        }
        public static string Parity
        {
            get { return pParity; }
            set { pParity = value; }
        }
        public static string Handshake
        {
            get { return pHandshake; }
            set { pHandshake = value; }
        }
        public static string Timeout
        {
            get { return pTimeout; }
            set { pTimeout = value; }
        }

        public static string CustomerRequiredOnRent
        {
            get { return strCustomerRequiredOnRent; }
            set { strCustomerRequiredOnRent = value; }
        }

        public static string CalculateRentLater
        {
            get { return strCalculateRentLater; }
            set { strCalculateRentLater = value; }
        }

        public static int LinkGL1
        {
            get { return intLinkGL1; }
            set { intLinkGL1 = value; }
        }

        public static int LinkGL2
        {
            get { return intLinkGL2; }
            set { intLinkGL2 = value; }
        }

        public static int LinkGL3
        {
            get { return intLinkGL3; }
            set { intLinkGL3 = value; }
        }

        public static int LinkGL4
        {
            get { return intLinkGL4; }
            set { intLinkGL4 = value; }
        }

        public static int LinkGL5
        {
            get { return intLinkGL5; }
            set { intLinkGL5 = value; }
        }

        public static int LinkGL6
        {
            get { return intLinkGL6; }
            set { intLinkGL6 = value; }
        }

        public static string ScalePrinterName
        {
            get { return strScalePrinterName; }
            set { strScalePrinterName = value; }
        }

        public static string ExternalLabelPrinterName
        {
            get { return strExternalLabelPrinterName; }
            set { strExternalLabelPrinterName = value; }
        }

        public static string RecipePrinterName
        {
            get { return strRecipePrinterName; }
            set { strRecipePrinterName = value; }
        }

        public static string StartWithScaleScreen
        {
            get { return strStartWithScaleScreen; }
            set { strStartWithScaleScreen = value; }
        }

        public static string StartWithScale_User
        {
            get { return strStartWithScale_User; }
            set { strStartWithScale_User = value; }
        }

        public static string StartWithScale_Password
        {
            get { return strStartWithScale_Password; }
            set { strStartWithScale_Password = value; }
        }

        public static string NoSaleReceipt
        {
            get { return strNoSaleReceipt; }
            set { strNoSaleReceipt = value; }
        }

        public static string HouseAccountBalanceInReceipt
        {
            get { return strHouseAccountBalanceInReceipt; }
            set { strHouseAccountBalanceInReceipt = value; }
        }

        public static string WeightDisplayType
        {
            get { return strWeightDisplayType; }
            set { strWeightDisplayType = value; }
        }

        public static string NTEPCert
        {
            get { return strNTEPCert; }
            set { strNTEPCert = value; }
        }

        public static string CheckPriceSmart
        {
            get { return strCheckPriceSmart; }
            set { strCheckPriceSmart = value; }
        }

        public static string PriceSmartDirectoryPath
        {
            get { return strPriceSmartDirectoryPath; }
            set { strPriceSmartDirectoryPath = value; }
        }

        public static string PriceSmartItemFile
        {
            get { return strPriceSmartItemFile; }
            set { strPriceSmartItemFile = value; }
        }

        public static string PriceSmartIngredientFile
        {
            get { return strPriceSmartIngredientFile; }
            set { strPriceSmartIngredientFile = value; }
        }

        public static string PriceSmartScaleCat
        {
            get { return strPriceSmartScaleCat; }
            set { strPriceSmartScaleCat = value; }
        }

        public static int PriceSmartDeptID
        {
            get { return intPriceSmartDeptID; }
            set { intPriceSmartDeptID = value; }
        }

        public static int PriceSmartCatID
        {
            get { return intPriceSmartCatID; }
            set { intPriceSmartCatID = value; }
        }

        public static int PriceSmartScaleLabel
        {
            get { return intPriceSmartScaleLabel; }
            set { intPriceSmartScaleLabel = value; }
        }

        public static string PrintPriceSmartScaleLabel
        {
            get { return strPrintPriceSmartScaleLabel; }
            set { strPrintPriceSmartScaleLabel = value; }
        }

        public static string PriceSmartCurrency
        {
            get { return strPriceSmartCurrency; }
            set { strPriceSmartCurrency = value; }
        }

        public static string PriceSmartFtpHost
        {
            get { return strPriceSmartFtpHost; }
            set { strPriceSmartFtpHost = value; }
        }

        public static string PriceSmartFtpUser
        {
            get { return strPriceSmartFtpUser; }
            set { strPriceSmartFtpUser = value; }
        }

        public static string PriceSmartFtpPassword
        {
            get { return strPriceSmartFtpPassword; }
            set { strPriceSmartFtpPassword = value; }
        }

        public static string PriceSmartFtpFolder
        {
            get { return strPriceSmartFtpFolder; }
            set { strPriceSmartFtpFolder = value; }
        }

        public static int PriceSmartLogClearDays
        {
            get { return intPriceSmartLogClearDays; }
            set { intPriceSmartLogClearDays = value; }
        }


        public static string ShelfLifeDateExtend
        {
            get { return strShelfLifeDateExtend; }
            set { strShelfLifeDateExtend = value; }
        }

        public static string SendCheckDigitForLabelsDotNet
        {
            get { return strSendCheckDigitForLabelsDotNet; }
            set { strSendCheckDigitForLabelsDotNet = value; }
        }

        public static string POSLinkCommType
        {
            get { return strPOSLinkCommType; }
            set { strPOSLinkCommType = value; }
        }

        public static string POSLinkDestPort
        {
            get { return strPOSLinkDestPort; }
            set { strPOSLinkDestPort = value; }
        }

        public static string POSLinkDestIP
        {
            get { return strPOSLinkDestIP; }
            set { strPOSLinkDestIP = value; }
        }

        public static string POSLinkSerialPort
        {
            get { return strPOSLinkSerialPort; }
            set { strPOSLinkSerialPort = value; }
        }

        public static string POSLinkBaudRate
        {
            get { return strPOSLinkBaudRate; }
            set { strPOSLinkBaudRate = value; }
        }

        public static string POSLinkTimeout
        {
            get { return strPOSLinkTimeout; }
            set { strPOSLinkTimeout = value; }
        }






        public static string DutchValleyVendorUser
        {
            get { return strDutchValleyVendorUser; }
            set { strDutchValleyVendorUser = value; }
        }

        public static string DutchValleyVendorPassword
        {
            get { return strDutchValleyVendorPassword; }
            set { strDutchValleyVendorPassword = value; }
        }

        public static string DutchValleyClientUser
        {
            get { return strDutchValleyClientUser; }
            set { strDutchValleyClientUser = value; }
        }

        public static string DutchValleyClientPassword
        {
            get { return strDutchValleyClientPassword; }
            set { strDutchValleyClientPassword = value; }
        }

        public static string DutchValleyScaleCat
        {
            get { return strDutchValleyScaleCat; }
            set { strDutchValleyScaleCat = value; }
        }

        public static int DutchValleyDeptID
        {
            get { return intDutchValleyDeptID; }
            set { intDutchValleyDeptID = value; }
        }

        public static int DutchValleyScaleLabel
        {
            get { return intDutchValleyScaleLabel; }
            set { intDutchValleyScaleLabel = value; }
        }

        public static int DutchValleyFamily
        {
            get { return intDutchValleyFamily; }
            set { intDutchValleyFamily = value; }
        }

        public static int DutchValleyVendor
        {
            get { return intDutchValleyVendor; }
            set { intDutchValleyVendor = value; }
        }

        #endregion

        #endregion


        public static void GetSoftwareVersion()
        {

            strSoftwareVersion = "0000";
            
            string strSQLComm = " select isnull(AppVersion,'0000') as val, isnull(CurrentVersion,'0000') as dbval from DBVersion ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strSoftwareVersion = objSQLReader["val"].ToString() + " , " + objSQLReader["dbval"].ToString();
                
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        public static void GetUserCustomizationParameters()
        {
            strPOSFunctionButtonShowHideState_User = "S";
            string strSQLComm = " select isnull(POSFunctionButtonShowHideState,'S') as Param1 from usercustomization where UserID = @usr ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@usr", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@usr"].Value = SystemVariables.CurrentUserID;

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strPOSFunctionButtonShowHideState_User = objSQLReader["Param1"].ToString();

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        #region Assign Local Settings Parameters ( for this terminal only )

        public static void GetCustomTemplatePrinters()
        {
            string pHost = GeneralFunctions.GetHostName();
            strCustomTemplatePrinter1 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 1");
            strCustomTemplatePrinter2 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 2");
            strCustomTemplatePrinter3 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 3");
            strCustomTemplatePrinter4 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 4");
            strCustomTemplatePrinter5 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 5");
            strCustomTemplatePrinter6 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 6");
            strCustomTemplatePrinter7 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 7");
            strCustomTemplatePrinter8 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 8");
            strCustomTemplatePrinter9 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 9");
            strCustomTemplatePrinter10 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 10");
            strCustomTemplatePrinter11 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 11");
            strCustomTemplatePrinter12 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 12");
            strCustomTemplatePrinter13 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 13");
            strCustomTemplatePrinter14 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 14");
            strCustomTemplatePrinter15 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 15");
            strCustomTemplatePrinter16 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 16");
            strCustomTemplatePrinter17 = GeneralFunctions.GetLocalSetupData(pHost, "Custom Template Printer 17");
        }

        public static void GetLocalSetup()
        {
            blActiveAdminPswdForMercury = false;

            string pHost = GeneralFunctions.GetHostName();

            strPaymentsense_Terminal = GeneralFunctions.GetLocalSetupData(pHost, "Paymentsense Terminal");

            strEvoConnectFileLocation = GeneralFunctions.GetLocalSetupData(pHost, "Evo Connect");

            strDefaultCloseoutPrinter = GeneralFunctions.GetLocalSetupData(pHost, "Default Closeout Printer");
            if (strDefaultCloseoutPrinter == "") strDefaultCloseoutPrinter = "Receipt Printer";
            strReportPrinterName = GeneralFunctions.GetLocalSetupData(pHost, "Report Printer Name");
            strReceiptPrinterName = GeneralFunctions.GetLocalSetupData(pHost, "Receipt Printer Name");
            strLabelPrinterType = GeneralFunctions.GetLocalSetupData(pHost, "Label Printer Type");
            if (strLabelPrinterType == "") strLabelPrinterType = "1";
            strLabelPrinterName = GeneralFunctions.GetLocalSetupData(pHost, "Label Printer Name");
            strLineDisplayDeviceType = GeneralFunctions.GetLocalSetupData(pHost, "Line Display Device Type");
            if (strLineDisplayDeviceType == "") strLineDisplayDeviceType = "OPOS";

            strLineDisplayDevice = GeneralFunctions.GetLocalSetupData(pHost, "Line Display Device");
            strLineDisplayDeviceSerial = GeneralFunctions.GetLocalSetupData(pHost, "Line Display Serial Device");

            strShowSKUOnPOSButton = GeneralFunctions.GetLocalSetupData(pHost, "SKU on POS Button");
            if (strShowSKUOnPOSButton == "") strShowSKUOnPOSButton = "N";

            strCashDrawerCode = GeneralFunctions.GetLocalSetupData(pHost, "Cash Drawer Code");



            strPOSCardPayment = GeneralFunctions.GetLocalSetupData(pHost, "Use Kiss Retail as Card Payment Processor");

            if (strPOSCardPayment == "") strPOSCardPayment = "N";
            if (strPOSCardPayment == "Y")
            {
                strPOSCardPaymentMerged = GeneralFunctions.GetLocalSetupDataSpecial(pHost, "Card Reporting Merged");

            }

            intPaymentGateway = GeneralFunctions.fnInt32(GeneralFunctions.GetLocalSetupData(pHost, "Payment Gateway"));
            strMercuryHPMerchantID = GeneralFunctions.GetLocalSetupData(pHost, "Mercury Payment Merchant ID");
            strMercuryHPUserID = GeneralFunctions.GetLocalSetupData(pHost, "Mercury Payment User ID");
            strMercuryHPPort = GeneralFunctions.GetLocalSetupData(pHost, "Mercury Payment Port");
            dblMercurySignAmount = GeneralFunctions.fnDouble(GeneralFunctions.GetLocalSetupData(pHost, "Mercury Signature Amount"));

            strElementZipProcessing = GeneralFunctions.GetLocalSetupData(pHost, "Element Payment Zip Processing");
            strElementHPAccountID = GeneralFunctions.GetLocalSetupData(pHost, "Element Payment Account ID");
            strElementHPAccountToken = GeneralFunctions.GetLocalSetupData(pHost, "Element Payment Account Token");
            strElementHPApplicationID = GeneralFunctions.GetLocalSetupData(pHost, "Element Payment Application ID");
            strElementHPAcceptorID = GeneralFunctions.GetLocalSetupData(pHost, "Element Payment Acceptor ID");
            intElementHPMode = GeneralFunctions.fnInt32(GeneralFunctions.GetLocalSetupData(pHost, "Payment Mode(Test/Live)"));


            if (strElementZipProcessing == "") strElementZipProcessing = "N";

            strElementHPTerminalID = GeneralFunctions.GetLocalSetupData(pHost, "Payment Gateway Terminal ID");

            strPrecidiaDataPath = GeneralFunctions.GetLocalSetupData(pHost, "Precidia Data Path");

            strPrecidiaUsePINPad = GeneralFunctions.GetLocalSetupData(pHost, "Precidia Use PIN Pad");
            if (strPrecidiaUsePINPad == "") strPrecidiaUsePINPad = "N";

            strPrecidiaLaneOpen = GeneralFunctions.GetLocalSetupData(pHost, "Precidia Lane Open");
            if (strPrecidiaLaneOpen == "") strPrecidiaLaneOpen = "Y";

            strPrecidiaRRLog = GeneralFunctions.GetLocalSetupData(pHost, "Precidia Add Log");
            if (strPrecidiaRRLog == "") strPrecidiaRRLog = "Y";

            strPrecidiaSign = GeneralFunctions.GetLocalSetupData(pHost, "Precidia Signature");
            if (strPrecidiaSign == "") strPrecidiaSign = "N";

            strPrecidiaClientMAC = GeneralFunctions.GetLocalSetupData(pHost, "Precidia ClientMAC");
            strPrecidiaPOSLynxMAC = GeneralFunctions.GetLocalSetupData(pHost, "Precidia POSLynxMAC");
            intPrecidiaPort = GeneralFunctions.fnInt32(GeneralFunctions.GetLocalSetupData(pHost, "Precidia Port"));

            dblPrecidiaSignAmount = GeneralFunctions.fnDouble(GeneralFunctions.GetLocalSetupData(pHost, "Precidia Signature Amount"));

            intPrecidiaProcessTimeout = GeneralFunctions.fnInt32(GeneralFunctions.GetLocalSetupData(pHost, "Precidia Process Timeout"));

            if (intPrecidiaProcessTimeout == 0) intPrecidiaProcessTimeout = 120;

            strBrowsePermission = GeneralFunctions.GetLocalSetupData(pHost, "Browse Permission");

            strCheckSecondMonitor = GeneralFunctions.GetLocalSetupData(pHost, "Run Second Monitor Application");
            strCheckSecondMonitorWithPOS = GeneralFunctions.GetLocalSetupData(pHost, "Active Second Monitor with POS");
            strApplicationSecondMonitor = GeneralFunctions.GetLocalSetupData(pHost, "Second Monitor Application Path");

            if (strCheckSecondMonitor == "") strCheckSecondMonitor = "N";
            if (strCheckSecondMonitorWithPOS == "") strCheckSecondMonitorWithPOS = "N";

            strPOSDefaultService = GeneralFunctions.GetLocalSetupData(pHost, "Default Service");
            strSalesService = GeneralFunctions.GetLocalSetupData(pHost, "Sales");
            strRentService = GeneralFunctions.GetLocalSetupData(pHost, "Rent");
            strRepairService = GeneralFunctions.GetLocalSetupData(pHost, "Repair");
            strCustomerRequiredOnRent = GeneralFunctions.GetLocalSetupData(pHost, "Customer Required On Rental");
            strCalculateRentLater = GeneralFunctions.GetLocalSetupData(pHost, "Rent Calculate Later");
            strAdvtDispArea = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Display Area");
            strAdvtScale = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Display - Scale");

            strAdvtDispNo = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Count");
            strAdvtDispTime = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Display Time");
            strAdvtDispOrder = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Display Order");
            strAdvtFont = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Font Size");
            strAdvtColor = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Background Color");
            strAdvtBackground = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Background Image");
            strAdvtDir = GeneralFunctions.GetLocalSetupData(pHost, "Advertisement Directory");
            strDirectReceiptPrinting = GeneralFunctions.GetLocalSetupData(pHost, "Receipt Print Directly");

            if (strDirectReceiptPrinting == "") strDirectReceiptPrinting = "N";

            strAutoSignout = GeneralFunctions.GetLocalSetupData(pHost, "Auto Signout");
            if (strAutoSignout == "") strAutoSignout = "N";

            string strTemp = GeneralFunctions.GetLocalSetupData(pHost, "Auto Signout Time");
            if (strTemp == "") intAutoSignoutTime = 0; else intAutoSignoutTime = GeneralFunctions.fnInt32(strTemp);

            strAutoSignoutTender = GeneralFunctions.GetLocalSetupData(pHost, "Auto Signout After Tender");
            if (strAutoSignoutTender == "") strAutoSignoutTender = "N";


            if (strPOSDefaultService == "") strPOSDefaultService = "Sales";

            if (strCustomerRequiredOnRent == "") strCustomerRequiredOnRent = "N";

            if (strSalesService == "") strSalesService = "Y";
            if (strRentService == "") strRentService = "N";
            if (strRepairService == "") strRepairService = "N";

            if (strAdvtDispArea == "") strAdvtDispArea = "25";
            if (strAdvtScale == "") strAdvtScale = "N";
            if (strAdvtDispNo == "") strAdvtDispNo = "1";
            if (strAdvtDispTime == "") strAdvtDispTime = "10";
            if (strAdvtDispOrder == "") strAdvtDispOrder = "Display Randomly";
            if (strAdvtFont == "") strAdvtFont = "8";
            if (strAdvtColor == "") strAdvtColor = "";
            if (strCashDrawerCode == "") strCashDrawerCode = "27,112,0,25,250";

            //if (strAdvtBackground == "") strAdvtBackground = "N";

            //pScaleDevice = GeneralFunctions.GetLocalSetupData(pHost, "Scale Device");
            //if (pScaleDevice == "") pScaleDevice = "(None)";

            pCOMPort = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - COMPort");
            pBaudRate = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - BaudRate");
            pDataBits = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - DataBits");
            pStopBits = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - StopBits");
            pParity = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - Parity");
            pHandshake = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - HandShake");
            pTimeout = GeneralFunctions.GetLocalSetupData(pHost, "Scale Port Setup - Timeout");

            if (pCOMPort == "") pCOMPort = "(None)";
            if (pBaudRate == "") pBaudRate = "9600";
            if (pDataBits == "") pDataBits = "8";
            if (pStopBits == "") pStopBits = "One";
            if (pParity == "") pParity = "None";
            if (pHandshake == "") pHandshake = "None";
            if (pTimeout == "") pTimeout = "1000";

            strGridItemParam1 = GeneralFunctions.GetLocalSetupData(pHost, "Item Grid Setup - Visible");
            strGridItemParam2 = GeneralFunctions.GetLocalSetupData(pHost, "Item Grid Setup - Width");

            if (strGridItemParam1 == "") strGridItemParam1 = "0*1*2*3*4*5*6*7*8*9*10*11*12*13";
            if (strGridItemParam2 == "") strGridItemParam2 = "11*18*7*7*7*5*9*14*6*5*11*7*15*15";


            strGridItemVendorParam1 = GeneralFunctions.GetLocalSetupData(pHost, "Item Vendor Grid Setup - Visible");
            strGridItemVendorParam2 = GeneralFunctions.GetLocalSetupData(pHost, "Item Vendor Grid Setup - Width");

            if (strGridItemVendorParam1 == "") strGridItemVendorParam1 = "0*1*2";
            if (strGridItemVendorParam2 == "") strGridItemVendorParam2 = "1*3*2";


            strGridCustomerParam1 = GeneralFunctions.GetLocalSetupData(pHost, "Customer Grid Setup - Visible");
            strGridCustomerParam2 = GeneralFunctions.GetLocalSetupData(pHost, "Customer Grid Setup - Width");

            if (strGridCustomerParam1 == "") strGridCustomerParam1 = "0*1*2*3*4*5*6";
            if (strGridCustomerParam2 == "") strGridCustomerParam2 = "10*20*19*12*10*16*10";

            if ((strPOSCardPayment == "Y") && (intPaymentGateway == 2)) blActiveAdminPswdForMercury = true;

            strAutoDisplayItemImage = GeneralFunctions.GetLocalSetupData(pHost, "Auto Display Product Image while Add to Cart");
            if (strAutoDisplayItemImage == "") strAutoDisplayItemImage = "N";

            strDisplayLaneClosed = GeneralFunctions.GetLocalSetupData(pHost, "Display Lane Closed");
            if (strDisplayLaneClosed == "") strDisplayLaneClosed = "N";

            strCloseCategoryAfterRinging = GeneralFunctions.GetLocalSetupData(pHost, "Close Category After Ringing");
            if (strCloseCategoryAfterRinging == "") strCloseCategoryAfterRinging = "N";

            //strNotReadBarcodeCheckDigit = GeneralFunctions.GetLocalSetupData(pHost, "DO not Read Scale Bar Code check Digits");
            //if (strNotReadBarcodeCheckDigit == "") strNotReadBarcodeCheckDigit = "N";

            //strScanner_8200 = GeneralFunctions.GetLocalSetupData(pHost, "Datalogic Scanner Model 8200");
            //if (strScanner_8200 == "") strScanner_8200 = "N";

            strScalePrinterName = GeneralFunctions.GetLocalSetupData(pHost, "Scale Printer");
            strExternalLabelPrinterName = GeneralFunctions.GetLocalSetupData(pHost, "External Label Printer");
            strRecipePrinterName = GeneralFunctions.GetLocalSetupData(pHost, "Recipe Printer");

            if (strScalePrinterName == "") strScalePrinterName = "(None)";
            if (strExternalLabelPrinterName == "") strExternalLabelPrinterName = "(None)";
            if (strRecipePrinterName == "") strRecipePrinterName = "(None)";

            strStartWithScaleScreen = GeneralFunctions.GetLocalSetupData(pHost, "Application Starts With Scale Screen");
            strStartWithScale_User = GeneralFunctions.GetLocalSetupData(pHost, "Starts With Scale Screen - User");
            strStartWithScale_Password = GeneralFunctions.GetLocalSetupData(pHost, "Starts With Scale Screen - Password");
            if (strStartWithScaleScreen == "") strStartWithScaleScreen = "N";

            strDatacapServer = GeneralFunctions.GetLocalSetupData(pHost, "Datacap Server");
            strDatacapMID = GeneralFunctions.GetLocalSetupData(pHost, "Datacap MID");
            strDatacapSecureDeviceID = GeneralFunctions.GetLocalSetupData(pHost, "Datacap Secure Device ID");
            strDatacapCOMPort = GeneralFunctions.GetLocalSetupData(pHost, "Datacap COM Port");
            strDatacapPINPad = GeneralFunctions.GetLocalSetupData(pHost, "Datacap PIN Pad Type");
            dblDatacapSignAmount = GeneralFunctions.fnDouble(GeneralFunctions.GetLocalSetupData(pHost, "Datacap Signature Amount"));


            intDatacapCardEntryMode = GeneralFunctions.fnInt32(GeneralFunctions.GetLocalSetupData(pHost, "Datacap Card Entry Mode"));
            strDatacapSignatureDevice = GeneralFunctions.GetLocalSetupData(pHost, "Datacap Signature Device");
            strDatacapSignatureDeviceCOMPort = GeneralFunctions.GetLocalSetupData(pHost, "Datacap Signature Device COM Port");

            strDatacapEMVCOMPort = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV COM");
            strDatacapEMVManual = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Manual");
            strDatacapEMVToken = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Token");
            strDatacapEMVSecurityDevice = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Security Device");
            strDatacapEMVServerIP = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Server IP");
            strDatacapEMVServerPort = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Server Port");
            strDatacapEMVTerminalID = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Terminal");
            strDatacapEMVOperatorID = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Operator");
            strDatacapEMVUserTrace = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV User Trace");
            strDatacapEMVMID = GeneralFunctions.GetLocalSetupData(pHost, "DatacapEMV Merchant ID");

            strRandomWeightUPCCheckDigit = GeneralFunctions.GetLocalSetupData(pHost, "Random Weight UPC Check Digit");
            if (strRandomWeightUPCCheckDigit == "") strRandomWeightUPCCheckDigit = "2";

            strWeightDisplayType = GeneralFunctions.GetLocalSetupData(pHost, "Weight Display Type");
            //strNTEPCert = GeneralFunctions.GetLocalSetupData(pHost, "NTEP Cert");

            strDisplay4DigitWeight = GeneralFunctions.GetLocalSetupData(pHost, "Display 4 Digit Decimal Weight");
            if (strDisplay4DigitWeight == "") strDisplay4DigitWeight = "N";

            strPOSLinkCommType = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Comm Type");
            strPOSLinkSerialPort = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Serial Port");
            strPOSLinkDestIP = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Dest IP");
            strPOSLinkDestPort = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Dest Port");
            strPOSLinkBaudRate = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Baud Rate");
            strPOSLinkTimeout = GeneralFunctions.GetLocalSetupData(pHost, "POSLink Timeout");

            strGPPrinter_Use = GeneralFunctions.GetLocalSetupData(pHost, "GPPrinter for Die Cut Printing");
            if (strGPPrinter_Use == "") strGPPrinter_Use = "N";
            strGPPrinter_PrinterName = GeneralFunctions.GetLocalSetupData(pHost, "GPPrinter Name");
            dblGPPrinter_LabelFooter = GeneralFunctions.fnDouble(GeneralFunctions.GetLocalSetupData(pHost, "GPPrinter Label Footer"));

        }

        public static void GetLanguage()
        {
            blActiveAdminPswdForMercury = false;

            string pHost = GeneralFunctions.GetHostName();
            strdefault_culture = GeneralFunctions.GetLocalSetupData(pHost, "Language");
            if (strdefault_culture == "") strdefault_culture = "en-US";
        }

        #endregion

        #region Assign Genearl Settings Parameters

        // License Agreement
        public static void LoadDisplayAgreement()
        {
            string strval = "";

            try
            {
                DataTable dtbl = new DataTable();
                dtbl = ConfigSettings.GetAgreementString();
                foreach (DataRow dr in dtbl.Rows)
                {
                    strval = dr["displayagreement"].ToString().Trim();
                }
                dtbl.Dispose();
                if (strval != "") strRegLicense = strval; else strRegLicense = "";
            }
            catch
            {
                strRegLicense = "";
            }
        }

        // General Settings ( applicable to all terminals )
        public static void LoadSettingsVariables()
        {
            intItemExpiryAlertDay = 0;
            strAppointmentEmailBody = "";
            strPrintLogoInReceipt = "Y";
            intBackupType = 1;
            intBackupStorageType = 0;
            strUse4Decimal = "N";
            intDecimalPlace = 2;
            intMonthsInvJrnl = 1;
            dblGiftCertMaxChange = 0;
            intCustomerInfo = 1;
            intVendorUpdate = 1;
            intCloseoutOption = 0;

            strAddGallon = "N";

            strOtherTerminalCloseout = "N";
            strBlindCountPreview = "N";
            strSalesByHour = "N";
            strSalesByDept = "N";
            strAllowNegativeStoreCredit = "Y";
            strPrintBlindDropCloseout = "Y";
            strPrintCloseoutInReceiptPrinter = "Y";
            strPOSAcceptCheck = "";
            strPOSDisplayChangeDue = "N";
            strPOSRedirectToChangeDue = "N";
            strPOSIDRequired = "N";
            strNoPriceOnLabelDefault = "N";
            strQuantityRequired = "N";
            strUseInvJrnl = "N";

            dblLayawayDepositPercent = 0;
            dblBottleDeposit = 0;
            dblMaxScaleWeight = 0;
            intLayawaysDue = 0;
            intLayawayReceipts = 0;
            strReceiptLayawayPolicy = "";
            strReceiptHeader = "";
            strPoleScreen = "";
            strReceiptFooter = "";
            strFormSkin = "Caramel";
            strPOSDisplayProductImage = "Y";
            strPOSShowGiftCertBalance = "N";
            intUsePriceLevel = 0;
            intPOSMoreFunctionAlignment = 0;
            strForcedLogin = "N";

            strGeneralReceiptPrint = "N";
            strPreprintedReceipt = "N";
            strCardPublisherName = "";
            strCardPublisherEMail = "";
            //strElementHPAcceptorID = "";
            //strElementHPAccountID = "";
            //strElementHPAccountToken = "";
            //strElementHPApplicationID = "";
            //intElementHPMode = 0;
            strCentralExportImport = "N";
            intCentralCustomerID = 0;
            strRunScaleControllerOnHostImport = "N";

            //intPaymentGateway = 0;
            //strMercuryHPMerchantID = "";
            //strMercuryHPUserID = "";

            strAutoPO = "N";
            strAutoCustomer = "N";

            strAutoGC = "N";
            strSingleGC = "N";

            strSMTPHost = "";
            strSMTPUser = "";
            strSMTPPassword = "";
            strFromAddress = "";
            strToAddress = "";
            strIsDuplicateInvoice = "N";
            strPrintDuplicateGiftCertSaleReceipt = "N";
            strReceiptPrintOnReturn = "Y";

            intDisplayLogoOrWeb = 0;
            strDisplayURL = "";

            strAcceptTips = "N";
            strShowFoodStampTotal = "N";

            //intPaymentGateway = 1;
            //strMercuryHPMerchantID = "";


            strShowSaleSaveInReceipt = "Y";

            intPOSPrintTender = 2;

            intPurgeCutOffDay = 500;

            intLinkGL1 = 0;
            intLinkGL2 = 0;
            intLinkGL3 = 0;
            intLinkGL4 = 0;
            intLinkGL5 = 0;
            intLinkGL6 = 0;

            strFocusOnEditProduct = "";

            pScaleDevice = "(None)";
            strNotReadBarcodeCheckDigit = "N";
            strScanner_8200 = "N";
            strNTEPCert = "";

            strPrintTrainingMode = "";


            strCountryName = "";
            strCurrencySymbol = "";
            strDateFormat = "";


            strSmallCurrencyName = "";
            strBigCurrencyName = "";

            strCoin1Name = "";
            strCoin2Name = "";
            strCoin3Name = "";
            strCoin4Name = "";
            strCoin5Name = "";
            strCoin6Name = "";
            strCoin7Name = "";

            strCurrency1Name = "";
            strCurrency2Name = "";
            strCurrency3Name = "";
            strCurrency4Name = "";
            strCurrency5Name = "";
            strCurrency6Name = "";
            strCurrency7Name = "";
            strCurrency8Name = "";
            strCurrency9Name = "";
            strCurrency10Name = "";

            strCurrency1QuickTender = "N";
            strCurrency2QuickTender = "N";
            strCurrency3QuickTender = "N";
            strCurrency4QuickTender = "N";
            strCurrency5QuickTender = "N";
            strCurrency6QuickTender = "N";
            strCurrency7QuickTender = "N";
            strCurrency8QuickTender = "N";
            strCurrency9QuickTender = "N";
            strCurrency10QuickTender = "N";

            strDefautInternational = "N";

            strUseTouchKeyboardInAdmin = "N";
            strUseTouchKeyboardInPOS = "N";

            intSMTPServer = 0;
            strREHost = "";
            intREPort = 0;
            strREUser = "";
            strREPassword = "";
            strRESSL = "Y";
            strREFromAddress = "";
            strREFromName = "";
            strREReplyTo = "";
            strReportEmail = "";
            dblBottleDeposit = 0;

            dblMaxScaleWeight = 0;


            strEnableBooking = "N";
            intBooking_Lead_Day = 0;
            intBooking_Lead_Hour = 1;
            intBooking_Lead_Minute = 0;
            intBooking_Slot_Hour = 0;
            intBooking_Slot_Minute = 30;

            intBookingScheduleWindowDay = 7;
            intBooking_Reminder_Hour = 24;
            intBooking_Reminder_Minute = 0;

            strBooking_CustomerSiteURL = "";
            strBooking_BusinessDetails = "";
            strBooking_PrivacyNotice = "";
            strBooking_Phone = "";
            strBooking_Email = "";
            strBooking_LinkedIn_Link = "";
            strBooking_Twitter_Link = "";
            strBooking_Facebook_Link = "";
            strBooking_Scheduling_NoLimit = "Y";
            strBooking_Scheduling_NoLimit = "N";

            strBooking_Mon_Check = "N";
            strBooking_Mon_Check = "Y";
            intBooking_Mon_Start = 18;
            intBooking_Mon_End = 38;


            strBooking_Tue_Check = "N";
            strBooking_Tue_Check = "Y";
            intBooking_Tue_Start = 18;
            intBooking_Tue_End = 38;

            strBooking_Wed_Check = "N";
            strBooking_Wed_Check = "Y";
            intBooking_Wed_Start = 18;
            intBooking_Wed_End = 38;


            strBooking_Thu_Check = "N";
            strBooking_Thu_Check = "Y";
            intBooking_Thu_Start = 18;
            intBooking_Thu_End = 38;

            strBooking_Fri_Check = "N";
            strBooking_Fri_Check = "Y";
            intBooking_Fri_Start = 18;
            intBooking_Fri_End = 38;

            strBooking_Sat_Check = "N";
            strBooking_Sat_Check = "Y";
            intBooking_Sat_Start = 18;
            intBooking_Sat_End = 38;

            strBooking_Sun_Check = "N";
            strBooking_Sun_Check = "N";
            intBooking_Sun_Start = 18;
            intBooking_Sun_End = 38;

            strBookingServer = "";
            strBookingDB = "";
            strBookingDBUser = "";
            strBookingDBPassword = "";


            strTaxInclusive = "Y";

            strPaymentsense_AccountName = "";
            strPaymentsense_ApiKey = "";
            strPaymentsense_SoftwareHouseId = "";
            strPaymentsense_InstallerId = "";

            strPaymentsense_Uri = "";





            string strSQLComm = " select * from setup ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    try
                    {
                        intSMTPServer = GeneralFunctions.fnInt32(objSQLReader["SMPTServer"].ToString());
                    }
                    catch
                    {
                        intSMTPServer = 0;
                    }

                    try
                    {
                        intItemExpiryAlertDay = GeneralFunctions.fnInt32(objSQLReader["ItemExpiryAlertDay"].ToString());
                    }
                    catch
                    {
                        intItemExpiryAlertDay = 90;
                    }


                    strAppointmentEmailBody = objSQLReader["AppointmentEmailBody"].ToString();

                    strPrintLogoInReceipt = objSQLReader["PrintLogoInReceipt"].ToString();
                    if (strPrintLogoInReceipt == "") strPrintLogoInReceipt = "N";

                    intBackupType = GeneralFunctions.fnInt32(objSQLReader["BackupType"].ToString());
                    intBackupStorageType = GeneralFunctions.fnInt32(objSQLReader["BackupStorageType"].ToString());

                    strPaymentsense_AccountName = objSQLReader["Paymentsense_AccountName"].ToString();
                    strPaymentsense_ApiKey = objSQLReader["Paymentsense_ApiKey"].ToString();
                    strPaymentsense_SoftwareHouseId = objSQLReader["Paymentsense_SoftwareHouseId"].ToString();
                    strPaymentsense_InstallerId = objSQLReader["Paymentsense_InstallerId"].ToString();

                    SetPaymentSenseUri();

                    strEvoApi = objSQLReader["EvoApiBaseAddress"].ToString();

                    strDefautInternational = objSQLReader["DefaultInternationalization"].ToString();
                    if (strDefautInternational == "") strDefautInternational = "Y";

                    if (strDefautInternational == "N")
                    {
                        strCountryName = objSQLReader["CountryName"].ToString();
                        strCurrencySymbol = objSQLReader["CurrencySymbol"].ToString();
                        strDateFormat = objSQLReader["DateFormat"].ToString();


                        strSmallCurrencyName = objSQLReader["SmallCurrencyName"].ToString();
                        strBigCurrencyName = objSQLReader["BigCurrencyName"].ToString();

                        strCoin1Name = objSQLReader["Coin1Name"].ToString();
                        strCoin2Name = objSQLReader["Coin2Name"].ToString();
                        strCoin3Name = objSQLReader["Coin3Name"].ToString();
                        strCoin4Name = objSQLReader["Coin4Name"].ToString();
                        strCoin5Name = objSQLReader["Coin5Name"].ToString();
                        strCoin6Name = objSQLReader["Coin6Name"].ToString();
                        strCoin7Name = objSQLReader["Coin7Name"].ToString();

                        strCurrency1Name = objSQLReader["Currency1Name"].ToString();
                        strCurrency2Name = objSQLReader["Currency2Name"].ToString();
                        strCurrency3Name = objSQLReader["Currency3Name"].ToString();
                        strCurrency4Name = objSQLReader["Currency4Name"].ToString();
                        strCurrency5Name = objSQLReader["Currency5Name"].ToString();
                        strCurrency6Name = objSQLReader["Currency6Name"].ToString();
                        strCurrency7Name = objSQLReader["Currency7Name"].ToString();
                        strCurrency8Name = objSQLReader["Currency8Name"].ToString();
                        strCurrency9Name = objSQLReader["Currency9Name"].ToString();
                        strCurrency10Name = objSQLReader["Currency10Name"].ToString();

                        strCurrency1QuickTender = objSQLReader["Currency1QuickTender"].ToString();
                        strCurrency2QuickTender = objSQLReader["Currency2QuickTender"].ToString();
                        strCurrency3QuickTender = objSQLReader["Currency3QuickTender"].ToString();
                        strCurrency4QuickTender = objSQLReader["Currency4QuickTender"].ToString();
                        strCurrency5QuickTender = objSQLReader["Currency5QuickTender"].ToString();
                        strCurrency6QuickTender = objSQLReader["Currency6QuickTender"].ToString();
                        strCurrency7QuickTender = objSQLReader["Currency7QuickTender"].ToString();
                        strCurrency8QuickTender = objSQLReader["Currency8QuickTender"].ToString();
                        strCurrency9QuickTender = objSQLReader["Currency9QuickTender"].ToString();
                        strCurrency10QuickTender = objSQLReader["Currency10QuickTender"].ToString();
                    }

                    strUse4Decimal = objSQLReader["Use4Decimal"].ToString();
                    strUse4Decimal = "Y";
                    intDecimalPlace = GeneralFunctions.fnInt32(objSQLReader["DecimalPlace"].ToString());
                    intDecimalPlace = 2;
                    intCustomerInfo = GeneralFunctions.fnInt32(objSQLReader["CustomerInfo"].ToString());
                    intVendorUpdate = GeneralFunctions.fnInt32(objSQLReader["VendorUpdate"].ToString());
                    intCloseoutOption = GeneralFunctions.fnInt32(objSQLReader["CloseoutOption"].ToString());
                    intSignINOption = GeneralFunctions.fnInt32(objSQLReader["SignInOption"].ToString());
                    intMonthsInvJrnl = GeneralFunctions.fnInt32(objSQLReader["MonthsInvJrnl"].ToString());

                    strBlindCountPreview = objSQLReader["BlindCountPreview"].ToString();
                    strSalesByHour = objSQLReader["SalesByHour"].ToString();
                    strSalesByDept = objSQLReader["SalesByDept"].ToString();
                    strPOSAcceptCheck = objSQLReader["POSAcceptCheck"].ToString();
                    strPOSDisplayChangeDue = objSQLReader["POSDisplayChangeDue"].ToString();
                    strPOSRedirectToChangeDue = objSQLReader["POSRedirectToChangeDue"].ToString();
                    strPOSIDRequired = objSQLReader["POSIDRequired"].ToString();
                    strNoPriceOnLabelDefault = objSQLReader["NoPriceOnLabelDefault"].ToString();
                    strQuantityRequired = objSQLReader["QuantityRequired"].ToString();
                    strUseInvJrnl = objSQLReader["UseInvJrnl"].ToString();

                    dblGiftCertMaxChange = GeneralFunctions.fnDouble(objSQLReader["GiftCertMaxChange"].ToString());

                    dblLayawayDepositPercent = GeneralFunctions.fnDouble(objSQLReader["LayawayDepositPercent"].ToString());
                    intLayawaysDue = GeneralFunctions.fnInt32(objSQLReader["LayawaysDue"].ToString());
                    intLayawayReceipts = GeneralFunctions.fnInt32(objSQLReader["LayawayReceipts"].ToString());
                    if (intLayawayReceipts == 0) intLayawayReceipts = 1;
                    strReceiptLayawayPolicy = objSQLReader["ReceiptLayawayPolicy"].ToString();
                    strReceiptHeader = objSQLReader["ReceiptHeader"].ToString();
                    strPoleScreen = objSQLReader["PoleScreen"].ToString();
                    strReceiptFooter = objSQLReader["ReceiptFooter"].ToString();
                    strFormSkin = objSQLReader["FormSkin"].ToString();
                    strPOSDisplayProductImage = objSQLReader["POSDisplayProductImage"].ToString();
                    strPOSShowGiftCertBalance = objSQLReader["POSShowGiftCertBalance"].ToString();
                    intUsePriceLevel = GeneralFunctions.fnInt32(objSQLReader["UsePriceLevel"].ToString());
                    intPOSMoreFunctionAlignment = GeneralFunctions.fnInt32(objSQLReader["POSMoreFunctionAlignment"].ToString());
                    strForcedLogin = objSQLReader["ForcedLogin"].ToString();
                    if (strForcedLogin == "") strForcedLogin = "N";
                    strForcedLogin = "N";


                    strAutoGC = objSQLReader["AutoGC"].ToString();
                    if (strAutoGC == "") strAutoGC = "N";

                    strSingleGC = objSQLReader["SingleGC"].ToString();
                    if (strSingleGC == "") strSingleGC = "N";

                    strUseTouchKeyboardInAdmin = objSQLReader["UseTouchKeyboardInAdmin"].ToString();
                    if (strUseTouchKeyboardInAdmin == "") strUseTouchKeyboardInAdmin = "N";

                    strUseTouchKeyboardInPOS = objSQLReader["UseTouchKeyboardInPOS"].ToString();
                    if (strUseTouchKeyboardInPOS == "") strUseTouchKeyboardInPOS = "N";

                    //strRegLicense = objSQLReader["AGREELICENSE"].ToString();

                    strGeneralReceiptPrint = objSQLReader["GeneralReceiptPrint"].ToString();
                    if (strGeneralReceiptPrint == "") strGeneralReceiptPrint = "N";

                    strPreprintedReceipt = objSQLReader["PreprintedReceipt"].ToString();
                    if (strPreprintedReceipt == "") strPreprintedReceipt = "N";

                    strCloseoutExport = objSQLReader["CloseoutExport"].ToString();
                    if (strCloseoutExport == "") strGeneralReceiptPrint = "N";

                    strCardPublisherName = objSQLReader["CardPublisherName"].ToString();
                    strCardPublisherEMail = objSQLReader["CardPublisherEMail"].ToString();

                    //intPaymentGateway = GeneralFunctions.fnInt32(objSQLReader["PaymentGateway"].ToString());
                    //strMercuryHPMerchantID = objSQLReader["MercuryHPMerchantID"].ToString();
                    //strMercuryHPUserID = objSQLReader["MercuryHPUserID"].ToString();

                    //strElementHPAcceptorID = objSQLReader["ElementHPAcceptorID"].ToString();
                    //strElementHPAccountID = objSQLReader["ElementHPAccountID"].ToString();
                    //strElementHPAccountToken = objSQLReader["ElementHPAccountToken"].ToString();
                    //strElementHPApplicationID = objSQLReader["ElementHPApplicationID"].ToString();
                    //intElementHPMode = GeneralFunctions.fnInt32(objSQLReader["ElementHPMode"].ToString());

                    strCentralExportImport = objSQLReader["CentralExportImport"].ToString();
                    if (strCentralExportImport == "") strCentralExportImport = "N";

                    strRunScaleControllerOnHostImport = objSQLReader["CheckRunScaleControllerOnHostImport"].ToString();
                    if (strRunScaleControllerOnHostImport == "") strRunScaleControllerOnHostImport = "N";

                    strShelfLifeDateExtend = objSQLReader["ShelfLifeDateExtend"].ToString();
                    if (strShelfLifeDateExtend == "") strShelfLifeDateExtend = "N";

                    strSendCheckDigitForLabelsDotNet = objSQLReader["SendCheckDigitForLabelsDotNet"].ToString();
                    if (strSendCheckDigitForLabelsDotNet == "") strSendCheckDigitForLabelsDotNet = "N";

                    strAutoPO = objSQLReader["AutoPO"].ToString();
                    strAutoCustomer = objSQLReader["AutoCustomer"].ToString();

                    strSMTPHost = objSQLReader["SMTPHost"].ToString();
                    strSMTPUser = objSQLReader["SMTPUser"].ToString();
                    strSMTPPassword = objSQLReader["SMTPPassword"].ToString();
                    strFromAddress = objSQLReader["FromAddress"].ToString();
                    strToAddress = objSQLReader["ToAddress"].ToString();

                    strIsDuplicateInvoice = objSQLReader["IsDuplicateInvoice"].ToString();
                    if (strIsDuplicateInvoice == "") strIsDuplicateInvoice = "N";

                    strPrintDuplicateGiftCertSaleReceipt = objSQLReader["PrintDuplicateGiftCertSaleReceipt"].ToString();
                    if (strPrintDuplicateGiftCertSaleReceipt == "") strIsDuplicateInvoice = "N";

                    //strReceiptPrintOnReturn = objSQLReader["ReceiptPrintOnReturn"].ToString();
                    //if (strReceiptPrintOnReturn == "") strReceiptPrintOnReturn = "Y";
                    strReceiptPrintOnReturn = "Y";

                    intDisplayLogoOrWeb = GeneralFunctions.fnInt32(objSQLReader["DisplayLogoOrWeb"].ToString());
                    strDisplayURL = objSQLReader["DisplayURL"].ToString();

                    intPOSPrintTender = GeneralFunctions.fnInt32(objSQLReader["POSRemotePrint"].ToString());

                    intPurgeCutOffDay = GeneralFunctions.fnInt32(objSQLReader["PurgeCutOffDay"].ToString());

                    strBarTenderLabelPrint = objSQLReader["BarTenderLabelPrint"].ToString();
                    if (strBarTenderLabelPrint == "") strBarTenderLabelPrint = "N";
                    if (strBarTenderLabelPrint == "Y") strBarTenderLabelPrint = "N";
                    //  strBarTenderLabelPrint = "N";

                    strUseCustomerNameInLabelPrint = objSQLReader["UseCustomerNameInLabelPrint"].ToString();
                    if (strUseCustomerNameInLabelPrint == "") strUseCustomerNameInLabelPrint = "N";
                    strFTPhost = objSQLReader["FTPhost"].ToString();
                    strFTPuser = objSQLReader["FTPuser"].ToString();
                    strFTPpswd = objSQLReader["FTPpswd"].ToString();

                    strCloudServer = objSQLReader["CloudServer"].ToString();
                    strCloudDB = objSQLReader["CloudDB"].ToString();
                    strCloudUser = objSQLReader["CloudUser"].ToString();
                    strCloudPassword = objSQLReader["CloudPassword"].ToString();

                    intPOSPrintInvoice = GeneralFunctions.fnInt32(objSQLReader["POSPrintInvoice"].ToString());

                    strAcceptTips = objSQLReader["AcceptTips"].ToString();
                    if (strAcceptTips == "") strAcceptTips = "N";

                    strShowTipsInReceipt = objSQLReader["ShowTipsInReceipt"].ToString();
                    if (strShowTipsInReceipt == "") strShowTipsInReceipt = "N";

                    strShowFoodStampTotal = objSQLReader["ShowFoodStampTotal"].ToString();
                    if (strShowFoodStampTotal == "") strShowFoodStampTotal = "N";

                    strEasyTendering = objSQLReader["EasyTendering"].ToString();
                    if (strEasyTendering == "") strEasyTendering = "N";
                    strEasyTendering = "N";
                    strCalculatorStyleKeyboard = objSQLReader["CalculatorStyleKeyboard"].ToString();
                    if (strCalculatorStyleKeyboard == "") strCalculatorStyleKeyboard = "N";

                    strShowFeesInReceipt = objSQLReader["ShowFeesInReceipt"].ToString();
                    if (strShowFeesInReceipt == "") strShowFeesInReceipt = "Y";

                    strShowSaleSaveInReceipt = objSQLReader["ShowSaleSaveInReceipt"].ToString();
                    if (strShowSaleSaveInReceipt == "") strShowSaleSaveInReceipt = "Y";

                    strReportEmail = objSQLReader["ReportEmail"].ToString();

                    strREHost = objSQLReader["REHost"].ToString();
                    intREPort = GeneralFunctions.fnInt32(objSQLReader["REPort"].ToString());
                    strREUser = objSQLReader["REUser"].ToString();
                    strREPassword = objSQLReader["REPassword"].ToString();
                    strRESSL = objSQLReader["RESSL"].ToString();
                    strREFromAddress = objSQLReader["REFromAddress"].ToString();
                    strREFromName = objSQLReader["REFromName"].ToString();
                    strREReplyTo = objSQLReader["REReplyTo"].ToString();

                    dblBottleDeposit = GeneralFunctions.fnDouble(objSQLReader["BottleDeposit"].ToString());

                    dblMaxScaleWeight = GeneralFunctions.fnDouble(objSQLReader["MaxScaleWeight"].ToString());

                    strOtherTerminalCloseout = objSQLReader["AllowTerminalCloseout"].ToString();
                    if (strOtherTerminalCloseout == "") strOtherTerminalCloseout = "N";

                    strAddGallon = objSQLReader["AddGallon"].ToString();
                    if (strAddGallon == "") strAddGallon = "N";

                    strPrint2TicketsForRepair = objSQLReader["Print2TicketsForRepair"].ToString();
                    if (strPrint2TicketsForRepair == "") strPrint2TicketsForRepair = "N";

                    intLayawayPaymentOption = GeneralFunctions.fnInt32(objSQLReader["LayawayPaymentOption"].ToString());

                    intLinkGL1 = GeneralFunctions.fnInt32(objSQLReader["LinkGL1"].ToString());
                    intLinkGL2 = GeneralFunctions.fnInt32(objSQLReader["LinkGL2"].ToString());
                    intLinkGL3 = GeneralFunctions.fnInt32(objSQLReader["LinkGL3"].ToString());
                    intLinkGL4 = GeneralFunctions.fnInt32(objSQLReader["LinkGL4"].ToString());
                    intLinkGL5 = GeneralFunctions.fnInt32(objSQLReader["LinkGL5"].ToString());
                    intLinkGL6 = GeneralFunctions.fnInt32(objSQLReader["LinkGL6"].ToString());

                    strNoSaleReceipt = objSQLReader["NoSaleReceipt"].ToString();
                    if (strNoSaleReceipt == "") strNoSaleReceipt = "N";

                    strHouseAccountBalanceInReceipt = objSQLReader["HouseAccountBalanceInReceipt"].ToString();
                    if (strHouseAccountBalanceInReceipt == "") strHouseAccountBalanceInReceipt = "N";

                    strFocusOnEditProduct = objSQLReader["FocusOnEditProduct"].ToString();

                    if (strFocusOnEditProduct == "") strFocusOnEditProduct = "Description";

                    pScaleDevice = objSQLReader["ScaleDevice"].ToString();
                    if (pScaleDevice == "") pScaleDevice = "(None)";
                    strNotReadBarcodeCheckDigit = objSQLReader["NotReadScaleBarcodeCheckDigit"].ToString();
                    if (strNotReadBarcodeCheckDigit == "") strNotReadBarcodeCheckDigit = "N";
                    strScanner_8200 = objSQLReader["DatalogicScanner8200"].ToString();
                    if (strScanner_8200 == "") strScanner_8200 = "N";
                    strNTEPCert = objSQLReader["NTEPCert"].ToString();
                    strPrintTrainingMode = objSQLReader["PrintTrainingMode"].ToString();
                    if (strPrintTrainingMode == "") strPrintTrainingMode = "N";

                    strCheckSmartGrocer = objSQLReader["CheckSmartGrocer"].ToString();
                    if (strCheckSmartGrocer == "") strCheckSmartGrocer = "N";

                    strSmartGrocerStore = objSQLReader["SmartGrocerStore"].ToString();
                    if (strSmartGrocerStore == "") strSmartGrocerStore = "0";


                    strCheckPriceSmart = objSQLReader["CheckPriceSmart"].ToString();
                    if (strCheckPriceSmart == "") strCheckPriceSmart = "N";

                    try
                    {
                        intCentralCustomerID = GeneralFunctions.fnInt32(objSQLReader["CentralCustomerID"].ToString());
                    }
                    catch
                    {
                        intCentralCustomerID = 0;
                    }

                    strPriceSmartDirectoryPath = objSQLReader["PriceSmartDirectoryPath"].ToString();
                    strPriceSmartItemFile = objSQLReader["PriceSmartItemFile"].ToString();
                    strPriceSmartIngredientFile = objSQLReader["PriceSmartIngredientFile"].ToString();
                    strPriceSmartScaleCat = objSQLReader["PriceSmartScaleCat"].ToString();

                    intPriceSmartDeptID = GeneralFunctions.fnInt32(objSQLReader["PriceSmartDeptID"].ToString());
                    intPriceSmartCatID = GeneralFunctions.fnInt32(objSQLReader["PriceSmartCatID"].ToString());
                    intPriceSmartScaleLabel = GeneralFunctions.fnInt32(objSQLReader["PriceSmartScaleLabel"].ToString());

                    strPrintPriceSmartScaleLabel = objSQLReader["PrintPriceSmartScaleLabel"].ToString();
                    if (strPrintPriceSmartScaleLabel == "") strPrintPriceSmartScaleLabel = "N";

                    strPriceSmartCurrency = objSQLReader["PriceSmartCurrency"].ToString();


                    strPriceSmartFtpHost = objSQLReader["PriceSmartFtpHost"].ToString();
                    strPriceSmartFtpUser = objSQLReader["PriceSmartFtpUser"].ToString();
                    strPriceSmartFtpPassword = objSQLReader["PriceSmartFtpPassword"].ToString();
                    strPriceSmartFtpFolder = objSQLReader["PriceSmartFtpFolder"].ToString();

                    intPriceSmartLogClearDays = GeneralFunctions.fnInt32(objSQLReader["PriceSmartLogClearDays"].ToString());
                    if (intPriceSmartLogClearDays == 0) intPriceSmartLogClearDays = 15;




                    strDutchValleyVendorUser = objSQLReader["DutchValleyVendorUser"].ToString();
                    strDutchValleyVendorPassword = objSQLReader["DutchValleyVendorPassword"].ToString();
                    strDutchValleyClientUser = objSQLReader["DutchValleyClientUser"].ToString();
                    strDutchValleyClientPassword = objSQLReader["DutchValleyClientPassword"].ToString();

                    intDutchValleyDeptID = GeneralFunctions.fnInt32(objSQLReader["DutchValleyDeptID"].ToString());
                    strDutchValleyScaleCat = objSQLReader["DutchValleyScaleCat"].ToString();
                    intDutchValleyScaleLabel = GeneralFunctions.fnInt32(objSQLReader["DutchValleyScaleLabel"].ToString());
                    intDutchValleyFamily = GeneralFunctions.fnInt32(objSQLReader["DutchValleyFamily"].ToString());
                    intDutchValleyVendor = GeneralFunctions.fnInt32(objSQLReader["DutchValleyVendor"].ToString());


                    strPrintBlindDropCloseout = objSQLReader["PrintBlindDropCloseout"].ToString();
                    if (strPrintBlindDropCloseout == "") PrintBlindDropCloseout = "Y";

                    strAllowNegativeStoreCredit = objSQLReader["AllowNegativeStoreCredit"].ToString();
                    if (strAllowNegativeStoreCredit == "") AllowNegativeStoreCredit = "Y";

                    strEnableBooking = objSQLReader["EnableBooking"].ToString();
                    if (strEnableBooking == "")
                    {
                        strEnableBooking = "N";
                        intBooking_Lead_Day = 0;
                        intBooking_Lead_Hour = 1;
                        intBooking_Lead_Minute = 0;
                        intBooking_Slot_Hour = 0;
                        intBooking_Slot_Minute = 30;

                        intBookingScheduleWindowDay = 7;
                        intBooking_Reminder_Hour = 24;
                        intBooking_Reminder_Minute = 0;
                    }
                    else
                    {
                        intBooking_Lead_Day = GeneralFunctions.fnInt32(objSQLReader["Booking_Lead_Day"].ToString());
                        intBooking_Lead_Hour = GeneralFunctions.fnInt32(objSQLReader["Booking_Lead_Hour"].ToString());
                        intBooking_Lead_Minute = GeneralFunctions.fnInt32(objSQLReader["Booking_Lead_Minute"].ToString());
                        intBooking_Slot_Hour = GeneralFunctions.fnInt32(objSQLReader["Booking_Slot_Hour"].ToString());
                        intBooking_Slot_Minute = GeneralFunctions.fnInt32(objSQLReader["Booking_Slot_Minute"].ToString());

                        intBookingScheduleWindowDay = GeneralFunctions.fnInt32(objSQLReader["BookingScheduleWindowDay"].ToString());
                        intBooking_Reminder_Hour = GeneralFunctions.fnInt32(objSQLReader["Booking_Reminder_Hour"].ToString());
                        intBooking_Reminder_Minute = GeneralFunctions.fnInt32(objSQLReader["Booking_Reminder_Minute"].ToString());
                    }

                    strBooking_CustomerSiteURL = objSQLReader["Booking_CustomerSiteURL"].ToString();
                    strBooking_BusinessDetails = objSQLReader["Booking_BusinessDetails"].ToString();
                    strBooking_PrivacyNotice = objSQLReader["Booking_PrivacyNotice"].ToString();
                    strBooking_Phone = objSQLReader["Booking_Phone"].ToString();
                    strBooking_Email = objSQLReader["Booking_Email"].ToString();
                    strBooking_LinkedIn_Link = objSQLReader["Booking_LinkedIn_Link"].ToString();
                    strBooking_Twitter_Link = objSQLReader["Booking_Twitter_Link"].ToString();
                    strBooking_Facebook_Link = objSQLReader["Booking_Facebook_Link"].ToString();
                    strBooking_Scheduling_NoLimit = objSQLReader["Booking_Scheduling_NoLimit"].ToString();
                    if (strBooking_Scheduling_NoLimit == "") strBooking_Scheduling_NoLimit = "Y";

                    strBooking_Scheduling_NoLimit = objSQLReader["Booking_Scheduling_NoLimit"].ToString();
                    if (strBooking_Scheduling_NoLimit == "") strBooking_Scheduling_NoLimit = "N";

                    strBooking_Mon_Check = objSQLReader["Booking_Mon_Check"].ToString();
                    if (strBooking_Mon_Check == "")
                    {
                        strBooking_Mon_Check = "Y";
                        intBooking_Mon_Start = 18;
                        intBooking_Mon_End = 38;
                    }
                    else
                    {
                        intBooking_Mon_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Mon_Start"].ToString());
                        intBooking_Mon_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Mon_End"].ToString());
                    }


                    strBooking_Tue_Check = objSQLReader["Booking_Tue_Check"].ToString();
                    if (strBooking_Tue_Check == "")
                    {
                        strBooking_Tue_Check = "Y";
                        intBooking_Tue_Start = 18;
                        intBooking_Tue_End = 38;
                    }
                    else
                    {
                        intBooking_Tue_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Tue_Start"].ToString());
                        intBooking_Tue_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Tue_End"].ToString());
                    }

                    strBooking_Wed_Check = objSQLReader["Booking_Wed_Check"].ToString();
                    if (strBooking_Wed_Check == "")
                    {
                        strBooking_Wed_Check = "Y";
                        intBooking_Wed_Start = 18;
                        intBooking_Wed_End = 38;
                    }
                    else
                    {
                        intBooking_Wed_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Wed_Start"].ToString());
                        intBooking_Wed_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Wed_End"].ToString());
                    }


                    strBooking_Thu_Check = objSQLReader["Booking_Thu_Check"].ToString();
                    if (strBooking_Thu_Check == "")
                    {
                        strBooking_Thu_Check = "Y";
                        intBooking_Thu_Start = 18;
                        intBooking_Thu_End = 38;
                    }
                    else
                    {
                        intBooking_Thu_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Thu_Start"].ToString());
                        intBooking_Thu_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Thu_End"].ToString());
                    }

                    strBooking_Fri_Check = objSQLReader["Booking_Fri_Check"].ToString();
                    if (strBooking_Fri_Check == "")
                    {
                        strBooking_Fri_Check = "Y";
                        intBooking_Fri_Start = 18;
                        intBooking_Fri_End = 38;
                    }
                    else
                    {
                        intBooking_Fri_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Fri_Start"].ToString());
                        intBooking_Fri_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Fri_End"].ToString());
                    }

                    strBooking_Sat_Check = objSQLReader["Booking_Sat_Check"].ToString();
                    if (strBooking_Sat_Check == "")
                    {
                        strBooking_Sat_Check = "Y";
                        intBooking_Sat_Start = 18;
                        intBooking_Sat_End = 38;
                    }
                    else
                    {
                        intBooking_Sat_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Sat_Start"].ToString());
                        intBooking_Sat_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Sat_End"].ToString());
                    }

                    strBooking_Sun_Check = objSQLReader["Booking_Sun_Check"].ToString();
                    if (strBooking_Sun_Check == "")
                    {
                        strBooking_Sun_Check = "N";
                        intBooking_Sun_Start = 18;
                        intBooking_Sun_End = 38;
                    }
                    else
                    {
                        intBooking_Sun_Start = GeneralFunctions.fnInt32(objSQLReader["Booking_Sun_Start"].ToString());
                        intBooking_Sun_End = GeneralFunctions.fnInt32(objSQLReader["Booking_Sun_End"].ToString());
                    }


                    strBookingServer = objSQLReader["BookingServer"].ToString();
                    strBookingDB = objSQLReader["BookingDB"].ToString();
                    strBookingDBUser = objSQLReader["BookingDBUser"].ToString();
                    strBookingDBPassword = objSQLReader["BookingDBPassword"].ToString();


                    strTaxInclusive = objSQLReader["TaxInclusive"].ToString();
                    if (strTaxInclusive == "") strTaxInclusive = "Y";

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }

        }

        public static void SetPaymentSenseUri()
        {
            strPaymentsense_Uri = socketuri.Replace("<account-name>", strPaymentsense_AccountName).Replace("<integration-type>", "pac").Replace("<api-key>", strPaymentsense_ApiKey).Replace("<connect-version>", "v1").Replace("<software-house-id>", strPaymentsense_SoftwareHouseId).Replace("<installer-id>", strPaymentsense_InstallerId);
        }

        public static void LoadScaleGraduation()
        {

            strGrad_ScaleType = "";
            strGrad_GraduationText = "";
            dblGrad_S_Range1 = 0;
            dblGrad_S_Range2 = 0;
            dblGrad_S_Graduation = 0;
            dblGrad_D_Range1 = 0;
            dblGrad_D_Range2 = 0;
            dblGrad_D_Graduation = 0;
            strS_Check2Digit = "N";
            strD_Check2Digit = "N";

            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from ScaleGraduation1 ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                while (objSQLReader.Read())
                {
                    strGrad_ScaleType = objSQLReader["ScaleType"].ToString();
                    strGrad_GraduationText = objSQLReader["Graduation"].ToString();
                    dblGrad_S_Range1 = GeneralFunctions.fnDouble(objSQLReader["S_Range1"].ToString());
                    dblGrad_S_Range2 = GeneralFunctions.fnDouble(objSQLReader["S_Range2"].ToString());
                    dblGrad_D_Range1 = GeneralFunctions.fnDouble(objSQLReader["D_Range1"].ToString());
                    dblGrad_D_Range2 = GeneralFunctions.fnDouble(objSQLReader["D_Range2"].ToString());
                    dblGrad_S_Graduation = GeneralFunctions.fnDouble(objSQLReader["S_Graduation"].ToString());
                    dblGrad_D_Graduation = GeneralFunctions.fnDouble(objSQLReader["D_Graduation"].ToString());
                    strS_Check2Digit = objSQLReader["S_Check2Digit"].ToString();
                    strD_Check2Digit = objSQLReader["D_Check2Digit"].ToString();

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        // Store Info.
        public static void LoadStoreInfoVariables()
        {
            strCompany = "";
            strAddress1 = "";
            strAddress2 = "";
            strCity = "";
            strState = "";
            strPostalCode = "";
            strPhone = "";
            strFax = "";
            strEMail = "";
            strWebAddress = "";
            strStoreInfo = "";
            strReportHeader = "";
            strReportHeader_Company = "";
            strReportHeader_Address = "";

            string strSQLComm = " Select * From StoreInfo ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strCompany = objSQLReader["Company"].ToString();
                    strAddress1 = objSQLReader["Address1"].ToString();
                    strAddress2 = objSQLReader["Address2"].ToString();
                    strCity = objSQLReader["City"].ToString();
                    strState = objSQLReader["State"].ToString();
                    strPostalCode = objSQLReader["PostalCode"].ToString();
                    strPhone = objSQLReader["Phone"].ToString();
                    strFax = objSQLReader["Fax"].ToString();
                    strEMail = objSQLReader["EMail"].ToString();
                    strWebAddress = objSQLReader["WebAddress"].ToString();
                    strStoreInfo = objSQLReader["StoreInfo"].ToString();
                }

                // Get Report Header String;

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return;
            }

        }

        // Store Info. ( for Web Office )
        public static void LoadCentralStoreInfo()
        {
            strStoreCode = "";
            strStoreName = "";
            strExportFolderPath = "";
            strStoreId = "";

            string strSQLComm = " select * from CentralExportImport ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strStoreCode = objSQLReader["StoreCode"].ToString();
                    strStoreName = objSQLReader["StoreName"].ToString();
                    strStoreId = objSQLReader["ID"].ToString();
                    strStoreHostId = objSQLReader["HostID"].ToString(); 
                    strExportFolderPath = objSQLReader["ExportFolderPath"].ToString();
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        // Record count in Scale_Com Tabla
        public static int Get_Scale_Com_Count()
        {
            int val = 0;
            string strSQLComm = " select count(*) as rcnt from scale_com ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = GeneralFunctions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        #endregion

        #region Assign Registration Parameters

        public static void LoadRegistrationVariables()
        {
            strCompanyAddress = "";

            string strcitystatezip = "";
            string strccitystatezip = "";

            string strSQLComm = "select * from registration ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intID = GeneralFunctions.fnInt32(objSQLReader["ID"].ToString());
                    intNoUsers = GeneralFunctions.fnInt32(objSQLReader["NOUSERS"].ToString());
                    strActivationKey = objSQLReader["ACTIVATIONKEY"].ToString();
                    strRegCompanyName = objSQLReader["COMPANYNAME"].ToString();
                    strRegAddress1 = objSQLReader["ADDRESS1"].ToString();
                    strRegAddress2 = objSQLReader["ADDRESS2"].ToString();
                    strRegCity = objSQLReader["CITY"].ToString();
                    strRegZip = objSQLReader["ZIP"].ToString();
                    strRegState = objSQLReader["STATE"].ToString();
                    strRegEMail = objSQLReader["EMAIL"].ToString();
                    strRegPOSAccess = objSQLReader["POSAccess"].ToString();
                    strRegScaleAccess = objSQLReader["ScaleAccess"].ToString();
                    strRegOrderingAccess = objSQLReader["OrderingAccess"].ToString();
                    strRegLabelDesigner = objSQLReader["LabelDesigner"].ToString();
                }
                if ((strRegCompanyName == "demo company") && (strRegAddress1 == "address 1")
                    && (strRegAddress2 == "address 2") && (strRegCity == "city")
                    && (strRegZip == "11111") && (strRegState == "ST")
                    && (strRegEMail == "demo@demo.com") && (strActivationKey == "7870-1574-0031-4878"))
                {
                    DemoVersion = "Y";
                }
                else
                {
                    DemoVersion = "N";
                }

                // Get Company Address String on login page;

                if (strRegAddress1 != "") strCompanyAddress = strCompanyAddress + strRegAddress1 + "\n";
                if (strRegAddress2 != "") strCompanyAddress = strCompanyAddress + strRegAddress2 + "\n";

                if (strRegCity != "") strccitystatezip = strccitystatezip + strRegCity;
                if (strRegState != "")
                {
                    if (strccitystatezip == "") strccitystatezip = strccitystatezip + strRegState;
                    else strccitystatezip = strccitystatezip + ", " + strRegState;
                }
                if (strRegZip != "")
                {
                    if (strccitystatezip == "") strccitystatezip = strccitystatezip + strRegZip;
                    else strccitystatezip = strccitystatezip + " " + strRegZip;
                }
                if (strccitystatezip != "") strCompanyAddress = strCompanyAddress + strccitystatezip + "\n";

                objSQLReader.Close();
                objSQlComm.Dispose();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
            }
        }

        #endregion

        private static string strTaxInclusive;
        public static string TaxInclusive
        {
            get { return strTaxInclusive; }
            set { strTaxInclusive = value; }
        }

        // Add non existing functions used in POS module
        public static int InsertInitialPOSFunctionButtons()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_InsertInitialPOSFunction";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@User", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@User"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@User"].Value = 0;

                objSQlComm.ExecuteNonQuery();
                intReturn = 0;
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intReturn;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        // update customer, gift certificate data with Web Office Store Code  
        public static int UpdateStoreCodeForCentralExport()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_UpdateDataForCentralExport";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.ExecuteNonQuery();
                intReturn = 0;
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intReturn;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        // Not Used
        public static void ModifyAgreement()
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ModifyLicense";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        // Assign Receipt Header
        public static void LoadMainHeader()
        {
            strCompany = "";
            strAddress1 = "";
            strAddress2 = "";
            strCity = "";
            strState = "";
            strPostalCode = "";
            //strPhone = "";
            strReportHeader = "";
            //strWebAddress = "";
            string strcitystatezip = "";
            string strRH = "";

            strReportHeader_Company = "";
            strReportHeader_Address = "";

            strReceiptHeader_Company = "";
            strReceiptHeader_Address = "";

            strCompany = Settings.RegCompanyName; //"Jackson County Sheriff's Office";
            strAddress1 = Settings.RegAddress1;
            strAddress2 = Settings.RegAddress2;
            strCity = Settings.RegCity;
            strState = Settings.RegState;
            strPostalCode = Settings.RegZip;
            strPhone = Settings.Phone;
            strWebAddress = Settings.WebAddress;
            strRH = Settings.ReceiptHeader;
            strReportHeader = "";
            strTotalReceiptHeader = "";

            strReceiptHeaderCompany = "";
            strReceiptHeaderAddress = "";
            // load report header

            if (strCompany != "")
            {
                strReportHeader = strReportHeader + strCompany + "\r\n";
                strReportHeader_Company = strCompany;
                strReceiptHeader_Company = strCompany;
            }
            if (strAddress1 != "")
            {
                strReportHeader = strReportHeader + strAddress1 + "\r\n";
                strReportHeader_Address = strReportHeader_Address + strAddress1 + "\r\n";
                strReceiptHeader_Address = strReceiptHeader_Address + strAddress1 + "\r\n";
            }

            if (strAddress2 != null)
            {
                if (strAddress2 != "")
                {
                    strReportHeader = strReportHeader + strAddress2 + "\r\n";
                    strReportHeader_Address = strReportHeader_Address + strAddress2 + "\r\n";
                    strReceiptHeader_Address = strReceiptHeader_Address + strAddress2 + "\r\n";
                }

            }

            if (strCity != null)
            {
                if (strCity != "")
                    strcitystatezip = strcitystatezip + strCity;
            }
            if (strState != null)
            {
                if (strState != "")
                {
                    if (strcitystatezip == "") strcitystatezip = strcitystatezip + strState;
                    else strcitystatezip = strcitystatezip + ", " + strState;
                }
            }
            if (strPostalCode != null)
            {
                if (strPostalCode != "")
                {
                    if (strcitystatezip == "") strcitystatezip = strcitystatezip + strPostalCode;
                    else strcitystatezip = strcitystatezip + " " + strPostalCode;
                }
            }
            if (strcitystatezip != "")
            {
                strReportHeader = strReportHeader + strcitystatezip + "\r\n";
                strReportHeader_Address = strReportHeader_Address + strcitystatezip + "\r\n";
                strReceiptHeader_Address = strReceiptHeader_Address + strcitystatezip + "\r\n";
            }

            if (strPhone != null)
            {
                if (strPhone != "")
                {
                    strReportHeader = strReportHeader + strPhone + "\r\n";
                    strReportHeader_Address = strReportHeader_Address + strPhone + "\r\n";
                    strReceiptHeader_Address = strReceiptHeader_Address + strPhone + "\r\n";
                }
            }
            if (strWebAddress != null)
            {
                if (strWebAddress != "")
                {
                    strReportHeader = strReportHeader + strWebAddress + "\r\n";
                    strReportHeader_Address = strReportHeader_Address + strWebAddress + "\r\n";
                    strReceiptHeader_Address = strReceiptHeader_Address + strWebAddress + "\r\n";
                }
            }

            // load receipt header



            strMainReceiptHeader = strReportHeader;

            if (strRH != null)
            {
                if (strRH != "")
                    strTotalReceiptHeader = strMainReceiptHeader + strRH;
                else
                    strTotalReceiptHeader = strMainReceiptHeader.Substring(0, strMainReceiptHeader.Length - 2);
            }
            else
            {
                strTotalReceiptHeader = strMainReceiptHeader.Substring(0, strMainReceiptHeader.Length - 2);
            }
            if (strDemoVersion == "Y")
            {
                strReportHeader = "*** DEMO VERSION ***\n\n" + strTotalReceiptHeader;
            }
        }

        // Used to store logs for some specific events if using Mercury Payment gateway
        public static void AddInApplicationLogs(string p1, string p2, string p3, string p4, string p5)
        {
            string strSQLComm = " insert into applicationlogs(username,logtime,logterminal,logevent,eventstatus,eventidentity) "
                              + " values(@username,@logtime,@logterminal,@logevent,@eventstatus,@eventidentity)";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@logtime", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters.Add(new SqlParameter("@logterminal", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@logevent", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@eventstatus", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@eventidentity", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@username"].Value = p1;
            objSQlComm.Parameters["@logtime"].Value = DateTime.Now;
            objSQlComm.Parameters["@logterminal"].Value = p2;
            objSQlComm.Parameters["@logevent"].Value = p3;
            objSQlComm.Parameters["@eventstatus"].Value = p4;
            objSQlComm.Parameters["@eventidentity"].Value = p5;
            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQlComm.ExecuteNonQuery();

                objSQlComm.Dispose();
                sqlConn.Close();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        // General stored event
        public static void AddInGeneralLog(string p1, string p2, string p3, string p4)
        {
            string strSQLComm = " insert into generallog(username,logdatetime,logterminal,logevent,eventstatus) "
                              + " values(@username,@logtime,@logterminal,@logevent,@eventstatus)";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@logtime", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters.Add(new SqlParameter("@logterminal", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@logevent", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters.Add(new SqlParameter("@eventstatus", System.Data.SqlDbType.NVarChar));

            objSQlComm.Parameters["@username"].Value = p1;
            objSQlComm.Parameters["@logtime"].Value = DateTime.Now;
            objSQlComm.Parameters["@logterminal"].Value = p2;
            objSQlComm.Parameters["@logevent"].Value = p3;
            objSQlComm.Parameters["@eventstatus"].Value = p4;
            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.ExecuteNonQuery();

                objSQlComm.Dispose();
                sqlConn.Close();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        #region Set Future Price ( Execute on Application Startup )

        public static void SetFuturePrice()
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_set_futureprice";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public static void SetScaleComOnSalePrice()
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_scale_comm_on_saleprice";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public static int TotalActiveAdPriceToSend()
        {

            int cnt = 0;

            string strSQLComm = " select count(*) as rcnt from salepriceheader where getdate() between effectivefrom and effectiveto "
                              + " and salestatus = 'Y' and scale_com_set = 'N' and scale_com_reset = 'N' ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    cnt = GeneralFunctions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return cnt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public static DataTable FetchAdPriceData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,SaleBatchName,SaleBatchDesc,SaleStatus,EffectiveFrom,EffectiveTo "
                              + " from SalePriceHeader where getdate() between effectivefrom and effectiveto and salestatus = 'Y' and scale_com_reset = 'N' order by SaleBatchName ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SaleBatchName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SaleBatchDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SaleStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Flag", System.Type.GetType("System.Boolean"));

                string chk1 = "";
                string val = "";

                while (objSQLReader.Read())
                {
                    val = "";
                    chk1 = "";

                    if (objSQLReader["SaleStatus"].ToString() == "Y")
                    {
                        chk1 = "     Yes";
                    }
                    else
                    {
                        chk1 = "     No";
                    }

                    val = Convert.ToDateTime(objSQLReader["EffectiveFrom"].ToString()).ToString(SystemVariables.DateFormat + "  hh:mm tt") + "  -  " + Convert.ToDateTime(objSQLReader["EffectiveTo"].ToString()).ToString(SystemVariables.DateFormat + "  hh:mm tt");


                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["SaleBatchName"].ToString(),
                                                   objSQLReader["SaleBatchDesc"].ToString(),
                                                   chk1,val, false });
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return null;
            }
        }

        public static void SetScaleComOnSalePrice_Manual(int BatchID)
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_scale_comm_on_saleprice_Manual";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SaleBatchID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SaleBatchID"].Value = BatchID;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion


        //Shopify
        public static void LoadShopifySettings()
        {

            strShopifyAPIKey = "";
            strShopifyPassword = "";
            strShopifyStoreAddress = "";
            strShopifyUserInstructions = "";
            strPremiumPlan = "N";

            string strSQLComm = " select * from shopify ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strShopifyAPIKey = objSQLReader["ShopifyAPIKey"].ToString();
                    strShopifyPassword = objSQLReader["ShopifyPassword"].ToString();
                    strShopifyStoreAddress = objSQLReader["ShopifyStoreAddress"].ToString();
                    strShopifyUserInstructions = objSQLReader["ShopifyUserInstructions"].ToString();
                    strPremiumPlan = objSQLReader["PremiumPlan"].ToString();
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        //Admin Menu

        public static void LoadInitialMenu()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("MainMenu", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SubMenu", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("Visible", System.Type.GetType("System.String"));

            dtbl.Rows.Add(new object[] { "Customers", "Customers", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Customers", "Groups", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Customers", "Classes", 3, "Y" });
            dtbl.Rows.Add(new object[] { "Customers", "CRM Parameters", 4, "Y" });

            dtbl.Rows.Add(new object[] { "Items", "Products", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Services", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Families", 3, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Departments", 4, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "POS Screen Categories", 5, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Stock Journal", 6, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Inventory Adjustment", 7, "Y" });
            dtbl.Rows.Add(new object[] { "Items", "Import Bookers", 8, "Y" });

            dtbl.Rows.Add(new object[] { "Ordering", "Vendors", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Ordering", "Reorder Report", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Ordering", "Purchase Order", 3, "Y" });
            dtbl.Rows.Add(new object[] { "Ordering", "Receiving", 4, "Y" });
            dtbl.Rows.Add(new object[] { "Ordering", "Print Labels", 5, "Y" });
            dtbl.Rows.Add(new object[] { "Ordering", "Transfer", 6, "Y" });
            //     dtbl.Rows.Add(new object[] { "Ordering", "Products Label Printing", 7, "Y" });

            dtbl.Rows.Add(new object[] { "Employee", "Employees", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Employee", "Shifts", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Employee", "Holidays", 3, "Y" });

            dtbl.Rows.Add(new object[] { "Administrator", "Tax", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Tender Types", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Fees & Charges", 3, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Security", 4, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "General Settings", 5, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "XEPOS Host", 6, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Registration", 7, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Zip Codes", 8, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "G/L Account", 9, "Y" });
            //dtbl.Rows.Add(new object[] { "Administrator", "Printer Types", 10, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Printer Templates", 10, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Printer Template Mapping", 11, "Y" });
            dtbl.Rows.Add(new object[] { "Administrator", "Check New Version", 12, "Y" });


            dtbl.Rows.Add(new object[] { "Discounts", "Discounts", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Buy 'n Get Free", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Mix and Match", 3, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Break Packs", 4, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Price Adjustment", 5, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Sale Price", 6, "Y" });
            dtbl.Rows.Add(new object[] { "Discounts", "Future Price", 7, "Y" });

            dtbl.Rows.Add(new object[] { "Host", "Inventory", 1, "Y" });
            dtbl.Rows.Add(new object[] { "Host", "Gift Certificates", 2, "Y" });
            dtbl.Rows.Add(new object[] { "Host", "Customers", 3, "Y" });

            SystemVariables.Menu = dtbl;
        }

        public static void DisableSpecificSubMenu(string strMainMenu, string strSubMenu)
        {
            foreach (DataRow dr in SystemVariables.Menu.Rows)
            {
                if ((dr["MainMenu"].ToString() == strMainMenu) && (dr["SubMenu"].ToString() == strSubMenu))
                {
                    dr["Visible"] = "N";
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        public static void EnableSpecificSubMenu(string strMainMenu, string strSubMenu)
        {
            foreach (DataRow dr in SystemVariables.Menu.Rows)
            {
                if ((dr["MainMenu"].ToString() == strMainMenu) && (dr["SubMenu"].ToString() == strSubMenu))
                {
                    dr["Visible"] = "Y";
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        public static void DisableSpecificMenuGroup(string strMainMenu)
        {
            foreach (DataRow dr in SystemVariables.Menu.Rows)
            {
                if (dr["MainMenu"].ToString() == strMainMenu)
                {
                    dr["Visible"] = "N";
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        public static void EnableSpecificMenuGroup(string strMainMenu)
        {
            foreach (DataRow dr in SystemVariables.Menu.Rows)
            {
                if (dr["MainMenu"].ToString() == strMainMenu)
                {
                    dr["Visible"] = "Y";
                    break;
                }
                else
                {
                    continue;
                }
            }
        }


        public static void LoadQuickBooksSettings()
        {
            strQuickBooksWindowsIncomeAccount = "Merchandise Sales";
            strQuickBooksWindowsCOGSAccount = "Merchant Account Fees";

            string strSQLComm = " select * from QuickBooksInfo ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strQuickBooksWindowsCompanyFilePath = objSQLReader["QuickBooksWindowsCompanyFilePath"].ToString();
                    strQuickBooksWindowsIncomeAccount = objSQLReader["ItemIncomeAccount"].ToString();
                    strQuickBooksWindowsCOGSAccount = objSQLReader["ItemCOGSAccount"].ToString();
                    strQuickBooksWindowsZeroSalesTax = objSQLReader["SalesTaxZero"].ToString();
                    strQuickBooksWindowsNonZeroSalesTax = objSQLReader["SalesTaxNonZero"].ToString();

                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        //Woo Commerce
        public static void LoadWooCommerceSettings()
        {
            string strSQLComm = " select * from WooCommerceInfo ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strWCommConsumerKey = objSQLReader["WCommConsumerKey"].ToString();
                    strWCommConsumerSecret = objSQLReader["WCommConsumerSecret"].ToString();
                    strWCommStoreAddress = objSQLReader["WCommStoreAddress"].ToString();

                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

        public static void LoadXEROSettings()
        {
            strXeroCompanyName = Settings.Company;
            strXeroClientId = "";
            strXeroTenantId = "";
            strXeroAccessToken = "";
            strXeroRefreshToken = "";
            strXeroInventoryAssetAccountCode = "630";
            strXeroInventoryAdjustmentAccountCode = "400";
            strXeroAccountCodePurchase = "310";
            strXeroAccountCodeSale = "200";
            strXeroCOGSAccountCodePurchase = "310";
            strXeroCOGSAccountCodeSale = "310";

            strXeroCallbackUrl = @"http://localhost:8080/callback";

            string strSQLComm = " select * from XeroInfo ";

            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    strXeroCompanyName = objSQLReader["XeroCompanyName"].ToString();
                    strXeroClientId = objSQLReader["XeroClientId"].ToString();
                    strXeroCallbackUrl = objSQLReader["XeroCallbackUrl"].ToString();
                    strXeroAccessToken = objSQLReader["XeroAccessToken"].ToString();
                    strXeroRefreshToken = objSQLReader["XeroRefreshToken"].ToString();
                    strXeroTenantId = objSQLReader["XeroTenantId"].ToString();

                    strXeroAccountCodePurchase = objSQLReader["XeroAccountCodePurchase"].ToString();
                    strXeroAccountCodeSale = objSQLReader["XeroAccountCodeSale"].ToString();
                    strXeroCOGSAccountCodePurchase = objSQLReader["XeroCOGSAccountCodePurchase"].ToString();
                    strXeroCOGSAccountCodeSale = objSQLReader["XeroCOGSAccountCodeSale"].ToString();

                    strXeroInventoryAssetAccountCode = objSQLReader["XeroInventoryAssetAccountCode"].ToString();
                    strXeroInventoryAdjustmentAccountCode = objSQLReader["XeroInventoryAdjustmentAccountCode"].ToString();

                }

                if(strXeroCompanyName == "")
                {
                    strXeroCompanyName = Settings.Company;
                }
                if (strXeroCallbackUrl == "")
                {
                    strXeroCallbackUrl = @"http://localhost:8080/callback";
                }

                if (strXeroInventoryAssetAccountCode == "")
                {
                    strXeroInventoryAssetAccountCode = "630";
                }

                if (strXeroInventoryAdjustmentAccountCode == "")
                {
                    strXeroInventoryAdjustmentAccountCode = "400";
                }

                if (strXeroAccountCodePurchase == "")
                {
                    strXeroAccountCodePurchase = "310";
                }

                if (strXeroAccountCodeSale == "")
                {
                    strXeroAccountCodeSale = "200";
                }

                if (strXeroCOGSAccountCodePurchase == "")
                {
                    strXeroCOGSAccountCodePurchase = "310";
                }

                if (strXeroCOGSAccountCodeSale == "")
                {
                    strXeroCOGSAccountCodeSale = "310";
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return;
            }
        }

    }
}
