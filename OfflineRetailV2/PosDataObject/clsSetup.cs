/*
 purpose : Data for General Setup of the Application
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;


namespace PosDataObject
{
    public class Setup
    {
        #region definining private variables

        private int intItemExpiryAlertDay;
        public int ItemExpiryAlertDay
        {
            get { return intItemExpiryAlertDay; }
            set { intItemExpiryAlertDay = value; }
        }

        private string strAppointmentEmailBody;
        public string AppointmentEmailBody
        {
            get { return strAppointmentEmailBody; }
            set { strAppointmentEmailBody = value; }
        }

        private int intSMTPServer;
        private string strPrintDuplicateGiftCertSaleReceipt;
        public string PrintDuplicateGiftCertSaleReceipt
        {
            get { return strPrintDuplicateGiftCertSaleReceipt; }
            set { strPrintDuplicateGiftCertSaleReceipt = value; }
        }

        private int intPrinterTypeID;
        public int PrinterTypeID
        {
            get { return intPrinterTypeID; }
            set { intPrinterTypeID = value; }
        }

        private string strPrinterType;
        public string PrinterType
        {
            get { return strPrinterType; }
            set { strPrinterType = value; }
        }

        private string strPrintLogoInReceipt;
        public string PrintLogoInReceipt
        {
            get { return strPrintLogoInReceipt; }
            set { strPrintLogoInReceipt = value; }
        }

        private string strWCommConsumerKey;
        private string strWCommConsumerSecret;
        private string strWCommStoreAddress;
        private int intWooCommRecordCount;

        private string strXeroCompanyName;
        private string strXeroClientId;
        private string strXeroCallbackUrl;
        private string strXeroAccessToken;
        private string strXeroRefreshToken;
        private string strXeroTenantId;

        private string strXeroInventoryAssetAccountCode;
        private string strXeroCOGSAccountCodePurchase;
        private string strXeroCOGSAccountCodeSale;
        private string strXeroAccountCodeSale;
        private string strXeroAccountCodePurchase;
        private string strXeroInventoryAdjustmentAccountCode;

        private int intXERORecordCount;

        private int intBackupType;
        private int intBackupStorageType;


        public int BackupType
        {
            get { return intBackupType; }
            set { intBackupType = value; }
        }

        public int BackupStorageType
        {
            get { return intBackupStorageType; }
            set { intBackupStorageType = value; }
        }

        public string WCommConsumerKey
        {
            get { return strWCommConsumerKey; }
            set { strWCommConsumerKey = value; }
        }

        public string WCommConsumerSecret
        {
            get { return strWCommConsumerSecret; }
            set { strWCommConsumerSecret = value; }
        }

        public string WCommStoreAddress
        {
            get { return strWCommStoreAddress; }
            set { strWCommStoreAddress = value; }
        }

        public string XeroInventoryAssetAccountCode
        {
            get { return strXeroInventoryAssetAccountCode; }
            set { strXeroInventoryAssetAccountCode = value; }
        }

        public string XeroCOGSAccountCodePurchase
        {
            get { return strXeroCOGSAccountCodePurchase; }
            set { strXeroCOGSAccountCodePurchase = value; }
        }

        public string XeroCOGSAccountCodeSale
        {
            get { return strXeroCOGSAccountCodeSale; }
            set { strXeroCOGSAccountCodeSale = value; }
        }

        public string XeroAccountCodeSale
        {
            get { return strXeroAccountCodeSale; }
            set { strXeroAccountCodeSale = value; }
        }

        public string XeroAccountCodePurchase
        {
            get { return strXeroAccountCodePurchase; }
            set { strXeroAccountCodePurchase = value; }
        }

        public string XeroInventoryAdjustmentAccountCode
        {
            get { return strXeroInventoryAdjustmentAccountCode; }
            set { strXeroInventoryAdjustmentAccountCode = value; }
        }

        public string XeroCompanyName
        {
            get { return strXeroCompanyName; }
            set { strXeroCompanyName = value; }
        }

        public string XeroClientId
        {
            get { return strXeroClientId; }
            set { strXeroClientId = value; }
        }

        public string XeroCallbackUrl
        {
            get { return strXeroCallbackUrl; }
            set { strXeroCallbackUrl = value; }
        }

        public string XeroAccessToken
        {
            get { return strXeroAccessToken; }
            set { strXeroAccessToken = value; }
        }


        public string XeroRefreshToken
        {
            get { return strXeroRefreshToken; }
            set { strXeroRefreshToken = value; }
        }

        public string XeroTenantId
        {
            get { return strXeroTenantId; }
            set { strXeroTenantId = value; }
        }





        

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strMode;
        private string strErrorMsg;

        private string strTax1Name;
        private string strTax2Name;
        private string strTax3Name;

        private int intTax1Type;
        private int intTax2Type;
        private int intTax3Type;

        private double dblTax1Rate;
        private double dblTax2Rate;
        private double dblTax3Rate;

        private string strClientParam1;
        private string strClientParam2;
        private string strClientParam3;
        private string strClientParam4;
        private string strClientParam5;

        private string strCompany;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strState;
        private string strPostalCode;
        private string strPhone;
        private string strFax;
        private string strEMail;
        private string strWebAddress;

        private string strUse4Decimal;
        private int intDecimalPlace;
        private int intCustomerInfo;
        private int intVendorUpdate;
        private int intCloseoutOption;
        private string strOtherTerminalCloseout;
        private int intSignINOption;
        private int intMonthsInvJrnl;
        private string strBlindCountPreview;
        private string strSalesByHour;
        private string strSalesByDept;
        private string strPOSAcceptCheck;
        private string strPOSDisplayChangeDue;
        private string strPOSIDRequired;
        private string strPOSRedirectToChangeDue;
        private string strPOSShowGiftCertBalance;
        private string strNoPriceOnLabelDefault;
        private string strQuantityRequired;
        private string strUseInvJrnl;
        private double dblGiftCertMaxChange;
        private string strFormSkin;
        private string strAddGallon;

        private double dblBottleRefund;

        private double dblMaxScaleWeight;

        private string strAutoPO;
        private string strAutoCustomer;
        private string strAutoGC;
        private string strSingleGC;

        private string strReceiptHeader;
        private string strReceiptFooter;
        private string strReceiptLayawayPolicy;
        private string strPoleScreen;
        private double dblLayawayDepositPercent;
        private int intLayawaysDue;
        private int intLayawayReceipts;
        private int intPOSMoreFunctionAlignment;

        private	SqlTransaction objSQLTran;
		private DataTable dblSplitDataTable;
        private DataTable dblSplitDataTable1;
		private DataTable dblInitialDataTable;
        private DataTable dblPrinterDataTable;

		public SqlTransaction SaveTran;
		public SqlCommand SaveComm;
		public SqlConnection SaveCon;

        private int intOtherID;
        private int intStoreID;
        private int intExpImpID;

        private int intPOSFunctionID;
        private int intPOSDisplayOrder;
        private string strPOSIsVisible;
        private bool blPOSFunctionUpdate;
        private bool blScaleModule;
        private string strPhotoFilePath;
        private string strStoreInfo;
        private string strPOSDisplayProductImage;

        private int intUsePriceLevel;
        private int intChangedByAdmin;
        private bool blFunctionButtonAccess;

        private string strForcedLogin;
        
        private string strGeneralReceiptPrint;
        private string strCloseoutExport;

        private string strCardPublisherName;
        private string strCardPublisherEMail;

        private string strPayWAREUserID;
        private string strPayWAREUserPW;
        private string strPayWAREClientID;
        private string strPayWAREDevice;
        private string strPayWARECOMPort;
        private string strPayWAREServerIP;
        private string strPayWAREPort;
        private string strCentralExportImport;
        private string strStoreCode;
        private string strStoreName;
        private string strExportFolderPath;

        private string strSMTPHost;
        private string strSMTPUser;
        private string strSMTPPassword;
        private string strFromAddress;
        private string strToAddress;

        private string strIsDuplicateInvoice;

        private string strElementHPAccountID;
        private string strElementHPAccountToken;
        private string strElementHPApplicationID;
        private string strElementHPAcceptorID;
        private int intElementHPMode;

        private string strReceiptPrintOnReturn;

        private int intDisplayLogoOrWeb;
        private string strDisplayURL;
        private string strBrowsePermission;

        private int intPaymentGateway;
        private string strMercuryHPMerchantID;
        private string strMercuryHPUserID;

        private string strHostName;
        private string strPrinterName;

        private string strParamName;
        private string strParamValue;

        private int intPOSPrintTender;
        private int intPurgeCutOffDay;

        private string strSalesService;
        private string strRentService;
        private string strRepairService;
        private string strPOSDefaultService;

        private string strBarTenderLabelPrint;

        private string strFTPhost;
        private string strFTPuser;
        private string strFTPpswd;

        private string strPreprintedReceipt;

        private string strUseCustomerNameInLabelPrint;

        private string strCustomerRequiredOnRent;
        private int intPOSPrintInvoice;
        
        private string strCloudServer;
        private string strCloudDB;
        private string strCloudUser;
        private string strCloudPassword;

        private string strBookingServer;
        private string strBookingDB;
        private string strBookingDBUser;
        private string strBookingDBPassword;

        private string strAcceptTips;
        private string strShowTipsInReceipt;

        private string strShowFoodStampTotal;

        private string strEasyTendering;
        private string strShowFeesInReceipt;
        private string strCalculatorStyleKeyboard;

        private string  strReportEmail;
        private string  strREHost;
        private string  strREUser;
        private string  strREPassword;
        private string  strRESSL;
        private int     intREPort;
        private string  strREFromAddress;
        private string  strREFromName;
        private string  strREReplyTo;

        private string  strCOMFormatNo;
        private string  strCOMFormatName;
        private string  strCOMPortName;
        private string  strCOMPrinterCommand;
        private int     intCOMQty;
        private int     intCOMWrap;
        private string  strCOMFont;
        private string  strShowSaleSaveInReceipt;

        private string  strPrint2TicketsForRepair;

        private int     intLayawayPaymentOption;

        private int intLinkGL1;
        private int intLinkGL2;
        private int intLinkGL3;
        private int intLinkGL4;
        private int intLinkGL5;
        private int intLinkGL6;

        private string strNoSaleReceipt;
        private string strHouseAccountBalanceInReceipt;

        private string strPhotoFilePath1;

        private DataTable dtblGradData;

        private string strFocusOnEditProduct;

        private string strScaleDevice;
        private string strNotReadScaleBarcodeCheckDigit;
        private string strDatalogicScanner8200;
        private string strNTEPCert;

        private string strGraduation;
        private string strScaleType;
        private double dblS_Range1;
        private double dblS_Range2;
        private double dblS_Graduation;
        private double dblD_Range1;
        private double dblD_Range2;
        private double dblD_Graduation;

        private string strS_Check2Digit;
        private string strD_Check2Digit;

        private string strPrintTrainingMode;

        private string strCheckSmartGrocer;
        private int intSmartGrocerStore;

        private string strCheckPriceSmart;
        private string strPriceSmartDirectoryPath;
        private string strPriceSmartItemFile;
        private string strPriceSmartIngredientFile;
        private string strPriceSmartScaleCat;
        private int intPriceSmartDeptID;
        private int intPriceSmartCatID;
        private int intPriceSmartScaleLabel;
        private string strPrintPriceSmartScaleLabel;
        private string strPriceSmartCurrency;


        private string strPriceSmartFtpHost;
        private string strPriceSmartFtpUser;
        private string strPriceSmartFtpPassword;
        private string strPriceSmartFtpFolder;

        private int intPriceSmartLogClearDays;

        private string  strDutchValleyVendorUser;
        private string  strDutchValleyVendorPassword;
        private string  strDutchValleyClientUser;
        private string  strDutchValleyClientPassword;
        private int     intDutchValleyDeptID;
        private string  strDutchValleyScaleCat;
        private int     intDutchValleyScaleLabel;
        private int     intDutchValleyFamily;
        private int     intDutchValleyVendor;


        private string strSendCheckDigitForLabelsDotNet;


        private string  strEnableBooking;
        private string  strBooking_CustomerSiteURL;
        private string  strBooking_BusinessDetails;
        private string  strBooking_PrivacyNotice;
        private string  strBooking_Phone;
        private string  strBooking_Email;
        private string  strBooking_LinkedIn_Link;
        private string  strBooking_Twitter_Link;
        private string  strBooking_Facebook_Link;
        private int     intBooking_Lead_Day;
        private int     intBooking_Lead_Hour;
        private int     intBooking_Lead_Minute;
        private int     intBooking_Slot_Hour;
        private int     intBooking_Slot_Minute;
        private string  strBooking_Scheduling_NoLimit;
        private string  strBooking_Mon_Check;
        private int     intBooking_Mon_Start;
        private int     intBooking_Mon_End;
        private string  strBooking_Tue_Check;
        private int     intBooking_Tue_Start;
        private int     intBooking_Tue_End;
        private string  strBooking_Wed_Check;
        private int     intBooking_Wed_Start;
        private int     intBooking_Wed_End;
        private string  strBooking_Thu_Check;
        private int     intBooking_Thu_Start;
        private int     intBooking_Thu_End;
        private string  strBooking_Fri_Check;
        private int     intBooking_Fri_Start;
        private int     intBooking_Fri_End;
        private string  strBooking_Sat_Check;
        private int     intBooking_Sat_Start;
        private int     intBooking_Sat_End;
        private string  strBooking_Sun_Check;
        private int     intBooking_Sun_Start;
        private int intBooking_Sun_End;

        private int intBookingScheduleWindowDay;
        private int intBooking_Reminder_Hour;
        private int intBooking_Reminder_Minute;

        private string strPrintBlindDropCloseout;
        private string strPrintCloseoutInReceiptPrinter;

        private string strAllowNegativeStoreCredit;

        private string strTaxInclusive;

        private string strUseTouchKeyboardInAdmin;
        private string strUseTouchKeyboardInPOS;

        // Internationalization

        private string strCountryName;
        private string strCurrencySymbol;
        private string strDateFormat;


        private string strSmallCurrencyName;
        private string strBigCurrencyName;

        private string strCoin1Name;
        private string strCoin2Name;
        private string strCoin3Name;
        private string strCoin4Name;
        private string strCoin5Name;
        private string strCoin6Name;
        private string strCoin7Name;

        private string strCurrency1Name;
        private string strCurrency2Name;
        private string strCurrency3Name;
        private string strCurrency4Name;
        private string strCurrency5Name;
        private string strCurrency6Name;
        private string strCurrency7Name;
        private string strCurrency8Name;
        private string strCurrency9Name;
        private string strCurrency10Name;

        private string strCurrency1QuickTender;
        private string strCurrency2QuickTender;
        private string strCurrency3QuickTender;
        private string strCurrency4QuickTender;
        private string strCurrency5QuickTender;
        private string strCurrency6QuickTender;
        private string strCurrency7QuickTender;
        private string strCurrency8QuickTender;
        private string strCurrency9QuickTender;
        private string strCurrency10QuickTender;

        private string strShopifyAPIKey;
        private string strShopifyPassword;
        private string strShopifyStoreAddress;

        private int intShopifyRecordCount;

        private byte[] bytLogo;

        private string strEvoApiBaseAddress;

        private string strPaymentsense_AccountName;
        private string strPaymentsense_ApiKey;
        private string strPaymentsense_SoftwareHouseId;
        private string strPaymentsense_InstallerId;


        private string strQBCompanyFilePath;
        private int intQuickBooksRecordCount;

        private string strItemIncomeAccount;
        private string strItemCOGSAccount;
        private string strSalesTaxZero;
        private string strSalesTaxNonZero;

        #endregion

        #region definining public variables

        public int SMTPServer
        {
            get { return intSMTPServer; }
            set { intSMTPServer = value; }
        }

        public string QBCompanyFilePath
        {
            get { return strQBCompanyFilePath; }
            set { strQBCompanyFilePath = value; }
        }

        public string ItemIncomeAccount
        {
            get { return strItemIncomeAccount; }
            set { strItemIncomeAccount = value; }
        }

        public string ItemCOGSAccount
        {
            get { return strItemCOGSAccount; }
            set { strItemCOGSAccount = value; }
        }

        public string SalesTaxZero
        {
            get { return strSalesTaxZero; }
            set { strSalesTaxZero = value; }
        }

        public string SalesTaxNonZero
        {
            get { return strSalesTaxNonZero; }
            set { strSalesTaxNonZero = value; }
        }

        public string Paymentsense_AccountName
        {
            get { return strPaymentsense_AccountName; }
            set { strPaymentsense_AccountName = value; }
        }

        public string Paymentsense_ApiKey
        {
            get { return strPaymentsense_ApiKey; }
            set { strPaymentsense_ApiKey = value; }
        }

        public string Paymentsense_SoftwareHouseId
        {
            get { return strPaymentsense_SoftwareHouseId; }
            set { strPaymentsense_SoftwareHouseId = value; }
        }

        public string Paymentsense_InstallerId
        {
            get { return strPaymentsense_InstallerId; }
            set { strPaymentsense_InstallerId = value; }
        }

        public string EvoApiBaseAddress
        {
            get { return strEvoApiBaseAddress; }
            set { strEvoApiBaseAddress = value; }
        }

        public string UseTouchKeyboardInAdmin
        {
            get { return strUseTouchKeyboardInAdmin; }
            set { strUseTouchKeyboardInAdmin = value; }
        }

        public string UseTouchKeyboardInPOS
        {
            get { return strUseTouchKeyboardInPOS; }
            set { strUseTouchKeyboardInPOS = value; }
        }

        public byte[] Logo
        {
            get { return bytLogo; }
            set { bytLogo = value; }
        }

        public string ShopifyAPIKey
        {
            get { return strShopifyAPIKey; }
            set { strShopifyAPIKey = value; }
        }

        public string ShopifyPassword
        {
            get { return strShopifyPassword; }
            set { strShopifyPassword = value; }
        }

        public string ShopifyStoreAddress
        {
            get { return strShopifyStoreAddress; }
            set { strShopifyStoreAddress = value; }
        }
        private string strPremiumPlan;
        public string PremiumPlan
        {
            get { return strPremiumPlan; }
            set { strPremiumPlan = value; }
        }

        public string CountryName
        {
            get { return strCountryName; }
            set { strCountryName = value; }
        }

        public string CurrencySymbol
        {
            get { return strCurrencySymbol; }
            set { strCurrencySymbol = value; }
        }

        public string DateFormat
        {
            get { return strDateFormat; }
            set { strDateFormat = value; }
        }



        public string SmallCurrencyName
        {
            get { return strSmallCurrencyName; }
            set { strSmallCurrencyName = value; }
        }

        public string BigCurrencyName
        {
            get { return strBigCurrencyName; }
            set { strBigCurrencyName = value; }
        }

        public string Coin1Name
        {
            get { return strCoin1Name; }
            set { strCoin1Name = value; }
        }

        public string Coin2Name
        {
            get { return strCoin2Name; }
            set { strCoin2Name = value; }
        }

        public string Coin3Name
        {
            get { return strCoin3Name; }
            set { strCoin3Name = value; }
        }

        public string Coin4Name
        {
            get { return strCoin4Name; }
            set { strCoin4Name = value; }
        }

        public string Coin5Name
        {
            get { return strCoin5Name; }
            set { strCoin5Name = value; }
        }

        public string Coin6Name
        {
            get { return strCoin6Name; }
            set { strCoin6Name = value; }
        }

        public string Coin7Name
        {
            get { return strCoin7Name; }
            set { strCoin7Name = value; }
        }

        public string Currency1Name
        {
            get { return strCurrency1Name; }
            set { strCurrency1Name = value; }
        }

        public string Currency2Name
        {
            get { return strCurrency2Name; }
            set { strCurrency2Name = value; }
        }

        public string Currency3Name
        {
            get { return strCurrency3Name; }
            set { strCurrency3Name = value; }
        }

        public string Currency4Name
        {
            get { return strCurrency4Name; }
            set { strCurrency4Name = value; }
        }

        public string Currency5Name
        {
            get { return strCurrency5Name; }
            set { strCurrency5Name = value; }
        }

        public string Currency6Name
        {
            get { return strCurrency6Name; }
            set { strCurrency6Name = value; }
        }

        public string Currency7Name
        {
            get { return strCurrency7Name; }
            set { strCurrency7Name = value; }
        }

        public string Currency8Name
        {
            get { return strCurrency8Name; }
            set { strCurrency8Name = value; }
        }

        public string Currency9Name
        {
            get { return strCurrency9Name; }
            set { strCurrency9Name = value; }
        }

        public string Currency10Name
        {
            get { return strCurrency10Name; }
            set { strCurrency10Name = value; }
        }

        public string Currency1QuickTender
        {
            get { return strCurrency1QuickTender; }
            set { strCurrency1QuickTender = value; }
        }

        public string Currency2QuickTender
        {
            get { return strCurrency2QuickTender; }
            set { strCurrency2QuickTender = value; }
        }

        public string Currency3QuickTender
        {
            get { return strCurrency3QuickTender; }
            set { strCurrency3QuickTender = value; }
        }

        public string Currency4QuickTender
        {
            get { return strCurrency4QuickTender; }
            set { strCurrency4QuickTender = value; }
        }

        public string Currency5QuickTender
        {
            get { return strCurrency5QuickTender; }
            set { strCurrency5QuickTender = value; }
        }

        public string Currency6QuickTender
        {
            get { return strCurrency6QuickTender; }
            set { strCurrency6QuickTender = value; }
        }

        public string Currency7QuickTender
        {
            get { return strCurrency7QuickTender; }
            set { strCurrency7QuickTender = value; }
        }

        public string Currency8QuickTender
        {
            get { return strCurrency8QuickTender; }
            set { strCurrency8QuickTender = value; }
        }

        public string Currency9QuickTender
        {
            get { return strCurrency9QuickTender; }
            set { strCurrency9QuickTender = value; }
        }

        public string Currency10QuickTender
        {
            get { return strCurrency10QuickTender; }
            set { strCurrency10QuickTender = value; }
        }

        public string TaxInclusive
        {
            get { return strTaxInclusive; }
            set { strTaxInclusive = value; }
        }

        public string AllowNegativeStoreCredit
        {
            get { return strAllowNegativeStoreCredit; }
            set { strAllowNegativeStoreCredit = value; }
        }

        public string PrintBlindDropCloseout
        {
            get { return strPrintBlindDropCloseout; }
            set { strPrintBlindDropCloseout = value; }
        }

        public string PrintCloseoutInReceiptPrinter
        {
            get { return strPrintCloseoutInReceiptPrinter; }
            set { strPrintCloseoutInReceiptPrinter = value; }
        }

        public int BookingScheduleWindowDay
        {
            get { return intBookingScheduleWindowDay; }
            set { intBookingScheduleWindowDay = value; }
        }

        public int Booking_Reminder_Hour
        {
            get { return intBooking_Reminder_Hour; }
            set { intBooking_Reminder_Hour = value; }
        }

        public int Booking_Reminder_Minute
        {
            get { return intBooking_Reminder_Minute; }
            set { intBooking_Reminder_Minute = value; }
        }
       
        public string EnableBooking
        {
            get { return strEnableBooking; }
            set { strEnableBooking = value; }
        }
        public string Booking_CustomerSiteURL
        {
            get { return strBooking_CustomerSiteURL; }
            set { strBooking_CustomerSiteURL = value; }
        }
        public string Booking_BusinessDetails
        {
            get { return strBooking_BusinessDetails; }
            set { strBooking_BusinessDetails = value; }
        }
        public string Booking_PrivacyNotice
        {
            get { return strBooking_PrivacyNotice; }
            set { strBooking_PrivacyNotice = value; }
        }
        public string Booking_Phone
        {
            get { return strBooking_Phone; }
            set { strBooking_Phone = value; }
        }
        public string Booking_Email
        {
            get { return strBooking_Email; }
            set { strBooking_Email = value; }
        }
        public string Booking_LinkedIn_Link
        {
            get { return strBooking_LinkedIn_Link; }
            set { strBooking_LinkedIn_Link = value; }
        }
        public string Booking_Twitter_Link
        {
            get { return strBooking_Twitter_Link; }
            set { strBooking_Twitter_Link = value; }
        }
        public string Booking_Scheduling_NoLimit
        {
            get { return strBooking_Scheduling_NoLimit; }
            set { strBooking_Scheduling_NoLimit = value; }
        }
        public string Booking_Facebook_Link
        {
            get { return strBooking_Facebook_Link; }
            set { strBooking_Facebook_Link = value; }
        }
        public string Booking_Mon_Check
        {
            get { return strBooking_Mon_Check; }
            set { strBooking_Mon_Check = value; }
        }
        public string Booking_Tue_Check
        {
            get { return strBooking_Tue_Check; }
            set { strBooking_Tue_Check = value; }
        }
        public string Booking_Wed_Check
        {
            get { return strBooking_Wed_Check; }
            set { strBooking_Wed_Check = value; }
        }
        public string Booking_Thu_Check
        {
            get { return strBooking_Thu_Check; }
            set { strBooking_Thu_Check = value; }
        }
        public string Booking_Fri_Check
        {
            get { return strBooking_Fri_Check; }
            set { strBooking_Fri_Check = value; }
        }
        public string Booking_Sat_Check
        {
            get { return strBooking_Sat_Check; }
            set { strBooking_Sat_Check = value; }
        }
        public string Booking_Sun_Check
        {
            get { return strBooking_Sun_Check; }
            set { strBooking_Sun_Check = value; }
        }

        public int Booking_Lead_Day
        {
            get { return intBooking_Lead_Day; }
            set { intBooking_Lead_Day = value; }
        }

        public int Booking_Lead_Hour
        {
            get { return intBooking_Lead_Hour; }
            set { intBooking_Lead_Hour = value; }
        }

        public int Booking_Lead_Minute
        {
            get { return intBooking_Lead_Minute; }
            set { intBooking_Lead_Minute = value; }
        }

        public int Booking_Slot_Hour
        {
            get { return intBooking_Slot_Hour; }
            set { intBooking_Slot_Hour = value; }
        }

        public int Booking_Slot_Minute
        {
            get { return intBooking_Slot_Minute; }
            set { intBooking_Slot_Minute = value; }
        }

        public int Booking_Mon_Start
        {
            get { return intBooking_Mon_Start; }
            set { intBooking_Mon_Start = value; }
        }
        public int Booking_Mon_End
        {
            get { return intBooking_Mon_End; }
            set { intBooking_Mon_End = value; }
        }


        public int Booking_Tue_Start
        {
            get { return intBooking_Tue_Start; }
            set { intBooking_Tue_Start = value; }
        }
        public int Booking_Tue_End
        {
            get { return intBooking_Tue_End; }
            set { intBooking_Tue_End = value; }
        }


        public int Booking_Wed_Start
        {
            get { return intBooking_Wed_Start; }
            set { intBooking_Wed_Start = value; }
        }
        public int Booking_Wed_End
        {
            get { return intBooking_Wed_End; }
            set { intBooking_Wed_End = value; }
        }


        public int Booking_Thu_Start
        {
            get { return intBooking_Thu_Start; }
            set { intBooking_Thu_Start = value; }
        }
        public int Booking_Thu_End
        {
            get { return intBooking_Thu_End; }
            set { intBooking_Thu_End = value; }
        }


        public int Booking_Fri_Start
        {
            get { return intBooking_Fri_Start; }
            set { intBooking_Fri_Start = value; }
        }
        public int Booking_Fri_End
        {
            get { return intBooking_Fri_End; }
            set { intBooking_Fri_End = value; }
        }


        public int Booking_Sat_Start
        {
            get { return intBooking_Sat_Start; }
            set { intBooking_Sat_Start = value; }
        }
        public int Booking_Sat_End
        {
            get { return intBooking_Sat_End; }
            set { intBooking_Sat_End = value; }
        }


        public int Booking_Sun_Start
        {
            get { return intBooking_Sun_Start; }
            set { intBooking_Sun_Start = value; }
        }
        public int Booking_Sun_End
        {
            get { return intBooking_Sun_End; }
            set { intBooking_Sun_End = value; }
        }

        public string SendCheckDigitForLabelsDotNet
        {
            get { return strSendCheckDigitForLabelsDotNet; }
            set { strSendCheckDigitForLabelsDotNet = value; }
        }

        public string DutchValleyVendorUser
        {
            get { return strDutchValleyVendorUser; }
            set { strDutchValleyVendorUser = value; }
        }

        public string DutchValleyVendorPassword
        {
            get { return strDutchValleyVendorPassword; }
            set { strDutchValleyVendorPassword = value; }
        }

        public string DutchValleyClientUser
        {
            get { return strDutchValleyClientUser; }
            set { strDutchValleyClientUser = value; }
        }

        public string DutchValleyClientPassword
        {
            get { return strDutchValleyClientPassword; }
            set { strDutchValleyClientPassword = value; }
        }

        public string DutchValleyScaleCat
        {
            get { return strDutchValleyScaleCat; }
            set { strDutchValleyScaleCat = value; }
        }

        public int DutchValleyDeptID
        {
            get { return intDutchValleyDeptID; }
            set { intDutchValleyDeptID = value; }
        }

        public int DutchValleyScaleLabel
        {
            get { return intDutchValleyScaleLabel; }
            set { intDutchValleyScaleLabel = value; }
        }

        public int DutchValleyFamily
        {
            get { return intDutchValleyFamily; }
            set { intDutchValleyFamily = value; }
        }

        public int DutchValleyVendor
        {
            get { return intDutchValleyVendor; }
            set { intDutchValleyVendor = value; }
        }

        public string CheckPriceSmart
        {
            get { return strCheckPriceSmart; }
            set { strCheckPriceSmart = value; }
        }

        public string PrintPriceSmartScaleLabel
        {
            get { return strPrintPriceSmartScaleLabel; }
            set { strPrintPriceSmartScaleLabel = value; }
        }

        public string PriceSmartFtpHost
        {
            get { return strPriceSmartFtpHost; }
            set { strPriceSmartFtpHost = value; }
        }

        public string PriceSmartFtpUser
        {
            get { return strPriceSmartFtpUser; }
            set { strPriceSmartFtpUser = value; }
        }

        public string PriceSmartFtpPassword
        {
            get { return strPriceSmartFtpPassword; }
            set { strPriceSmartFtpPassword = value; }
        }

        public string PriceSmartFtpFolder
        {
            get { return strPriceSmartFtpFolder; }
            set { strPriceSmartFtpFolder = value; }
        }

        public string PriceSmartDirectoryPath
        {
            get { return strPriceSmartDirectoryPath; }
            set { strPriceSmartDirectoryPath = value; }
        }

        public string PriceSmartItemFile
        {
            get { return strPriceSmartItemFile; }
            set { strPriceSmartItemFile = value; }
        }

        public string PriceSmartIngredientFile
        {
            get { return strPriceSmartIngredientFile; }
            set { strPriceSmartIngredientFile = value; }
        }

        public string PriceSmartScaleCat
        {
            get { return strPriceSmartScaleCat; }
            set { strPriceSmartScaleCat = value; }
        }

        public int PriceSmartDeptID
        {
            get { return intPriceSmartDeptID; }
            set { intPriceSmartDeptID = value; }
        }

        public int PriceSmartCatID
        {
            get { return intPriceSmartCatID; }
            set { intPriceSmartCatID = value; }
        }

        public int PriceSmartScaleLabel
        {
            get { return intPriceSmartScaleLabel; }
            set { intPriceSmartScaleLabel = value; }
        }

        public int PriceSmartLogClearDays
        {
            get { return intPriceSmartLogClearDays; }
            set { intPriceSmartLogClearDays = value; }
        }



        public string DataObjectCulture_All
        {
            get { return strDataObjectCulture_All; }
            set { strDataObjectCulture_All = value; }
        }

        public string DataObjectCulture_None
        {
            get { return strDataObjectCulture_None; }
            set { strDataObjectCulture_None = value; }
        }
        public string CheckSmartGrocer
        {
            get { return strCheckSmartGrocer; }
            set { strCheckSmartGrocer = value; }
        }

        public string PrintTrainingMode
        {
            get { return strPrintTrainingMode; }
            set { strPrintTrainingMode = value; }
        }

        public string Graduation
        {
            get { return strGraduation; }
            set { strGraduation = value; }
        }


        public string ScaleType
        {
            get { return strScaleType; }
            set { strScaleType = value; }
        }

        public double S_Range1
        {
            get { return dblS_Range1; }
            set { dblS_Range1 = value; }
        }

        public double S_Range2
        {
            get { return dblS_Range2; }
            set { dblS_Range2 = value; }
        }

        public double S_Graduation
        {
            get { return dblS_Graduation; }
            set { dblS_Graduation = value; }
        }

        public double D_Range1
        {
            get { return dblD_Range1; }
            set { dblD_Range1 = value; }
        }

        public double D_Range2
        {
            get { return dblD_Range2; }
            set { dblD_Range2 = value; }
        }

        public string S_Check2Digit
        {
            get { return strS_Check2Digit; }
            set { strS_Check2Digit = value; }
        }

        public string D_Check2Digit
        {
            get { return strD_Check2Digit; }
            set { strD_Check2Digit = value; }
        }

        public double D_Graduation
        {
            get { return dblD_Graduation; }
            set { dblD_Graduation = value; }
        }

        public string ScaleDevice
        {
            get { return strScaleDevice; }
            set { strScaleDevice = value; }
        }

        public string NotReadScaleBarcodeCheckDigit
        {
            get { return strNotReadScaleBarcodeCheckDigit; }
            set { strNotReadScaleBarcodeCheckDigit = value; }
        }

        public string DatalogicScanner8200
        {
            get { return strDatalogicScanner8200; }
            set { strDatalogicScanner8200 = value; }
        }

        public string NTEPCert
        {
            get { return strNTEPCert; }
            set { strNTEPCert = value; }
        }

        public DataTable GradData
        {
            get { return dtblGradData; }
            set { dtblGradData = value; }
        }

        public bool ScaleModule
        {
            get { return blScaleModule; }
            set { blScaleModule = value; }
        }

        public string FocusOnEditProduct
        {
            get { return strFocusOnEditProduct; }
            set { strFocusOnEditProduct = value; }
        }

        public string NoSaleReceipt
        {
            get { return strNoSaleReceipt; }
            set { strNoSaleReceipt = value; }
        }

        public string HouseAccountBalanceInReceipt
        {
            get { return strHouseAccountBalanceInReceipt; }
            set { strHouseAccountBalanceInReceipt = value; }
        }

        public int LayawayPaymentOption
        {
            get { return intLayawayPaymentOption; }
            set { intLayawayPaymentOption = value; }
        }

        public string Print2TicketsForRepair
        {
            get { return strPrint2TicketsForRepair; }
            set { strPrint2TicketsForRepair = value; }
        }

        public string AddGallon
        {
            get { return strAddGallon; }
            set { strAddGallon = value; }
        }

        public string OtherTerminalCloseout
        {
            get { return strOtherTerminalCloseout; }
            set { strOtherTerminalCloseout = value; }
        }

        public double BottleRefund
        {
            get { return dblBottleRefund; }
            set { dblBottleRefund = value; }
        }

        public double MaxScaleWeight
        {
            get { return dblMaxScaleWeight; }
            set { dblMaxScaleWeight = value; }
        }

        public string ShowSaleSaveInReceipt
        {
            get { return strShowSaleSaveInReceipt; }
            set { strShowSaleSaveInReceipt = value; }
        }

        public string COMFont
        {
            get { return strCOMFont; }
            set { strCOMFont = value; }
        }

        public string COMFormatNo
        {
            get { return strCOMFormatNo; }
            set { strCOMFormatNo = value; }
        }

        public string COMFormatName
        {
            get { return strCOMFormatName; }
            set { strCOMFormatName = value; }
        }

        public string COMPortName
        {
            get { return strCOMPortName; }
            set { strCOMPortName = value; }
        }

        public string COMPrinterCommand
        {
            get { return strCOMPrinterCommand; }
            set { strCOMPrinterCommand = value; }
        }

        public int COMQty
        {
            get { return intCOMQty; }
            set { intCOMQty = value; }
        }

        public int COMWrap
        {
            get { return intCOMWrap; }
            set { intCOMWrap = value; }
        }

        public string REHost
        {
            get { return strREHost; }
            set { strREHost = value; }
        }

        public string REUser
        {
            get { return strREUser; }
            set { strREUser = value; }
        }

        public string REPassword
        {
            get { return strREPassword; }
            set { strREPassword = value; }
        }

        public string RESSL
        {
            get { return strRESSL; }
            set { strRESSL = value; }
        }

        public string REFromAddress
        {
            get { return strREFromAddress; }
            set { strREFromAddress = value; }
        }

        public string REFromName
        {
            get { return strREFromName; }
            set { strREFromName = value; }
        }

        public string REReplyTo
        {
            get { return strREReplyTo; }
            set { strREReplyTo = value; }
        }

        public int REPort
        {
            get { return intREPort; }
            set { intREPort = value; }
        }

        public string ReportEmail
        {
            get { return strReportEmail; }
            set { strReportEmail = value; }
        }

        public string EasyTendering
        {
            get { return strEasyTendering; }
            set { strEasyTendering = value; }
        }

        public string ShowFeesInReceipt
        {
            get { return strShowFeesInReceipt; }
            set { strShowFeesInReceipt = value; }
        }

        public string ShowFoodStampTotal
        {
            get { return strShowFoodStampTotal; }
            set { strShowFoodStampTotal = value; }
        }

        public string AcceptTips
        {
            get { return strAcceptTips; }
            set { strAcceptTips = value; }
        }

        public string ShowTipsInReceipt
        {
            get { return strShowTipsInReceipt; }
            set { strShowTipsInReceipt = value; }
        }

        public int POSPrintInvoice
        {
            get { return intPOSPrintInvoice; }
            set { intPOSPrintInvoice = value; }
        }

        public string PreprintedReceipt
        {
            get { return strPreprintedReceipt; }
            set { strPreprintedReceipt = value; }
        }

        public string CustomerRequiredOnRent
        {
            get { return strCustomerRequiredOnRent; }
            set { strCustomerRequiredOnRent = value; }
        }

        public string UseCustomerNameInLabelPrint
        {
            get { return strUseCustomerNameInLabelPrint; }
            set { strUseCustomerNameInLabelPrint = value; }
        }
        
        public string CloudServer
        {
            get { return strCloudServer; }
            set { strCloudServer = value; }
        }

        public string CloudDB
        {
            get { return strCloudDB; }
            set { strCloudDB = value; }
        }

        public string CloudUser
        {
            get { return strCloudUser; }
            set { strCloudUser = value; }
        }

        public string CloudPassword
        {
            get { return strCloudPassword; }
            set { strCloudPassword = value; }
        }

        public string BookingServer
        {
            get { return strBookingServer; }
            set { strBookingServer = value; }
        }

        public string BookingDB
        {
            get { return strBookingDB; }
            set { strBookingDB = value; }
        }

        public string BookingDBUser
        {
            get { return strBookingDBUser; }
            set { strBookingDBUser = value; }
        }

        public string BookingDBPassword
        {
            get { return strBookingDBPassword; }
            set { strBookingDBPassword = value; }
        }

        public string FTPhost
        {
            get { return strFTPhost; }
            set { strFTPhost = value; }
        }

        public string FTPuser
        {
            get { return strFTPuser; }
            set { strFTPuser = value; }
        }

        public string FTPpswd
        {
            get { return strFTPpswd; }
            set { strFTPpswd = value; }
        }

        public string ExportFolderPath
        {
            get { return strExportFolderPath; }
            set { strExportFolderPath = value; }
        }

        public string SalesService
        {
            get { return strSalesService; }
            set { strSalesService = value; }
        }

        public string RentService
        {
            get { return strRentService; }
            set { strRentService = value; }
        }

        public string RepairService
        {
            get { return strRepairService; }
            set { strRepairService = value; }
        }

        public string POSDefaultService
        {
            get { return strPOSDefaultService; }
            set { strPOSDefaultService = value; }
        }

        public int PurgeCutOffDay
        {
            get { return intPurgeCutOffDay; }
            set { intPurgeCutOffDay = value; }
        }

        public int POSPrintTender
        {
            get { return intPOSPrintTender; }
            set { intPOSPrintTender = value; }
        }

        public DataTable PrinterDataTable
        {
            get { return dblPrinterDataTable; }
            set { dblPrinterDataTable = value; }
        }

        public string PrinterName
        {
            get { return strPrinterName; }
            set { strPrinterName = value; }
        }

        public string HostName
        {
            get { return strHostName; }
            set { strHostName = value; }
        }

        public int PaymentGateway
        {
            get { return intPaymentGateway; }
            set { intPaymentGateway = value; }
        }

        public string MercuryHPMerchantID
        {
            get { return strMercuryHPMerchantID; }
            set { strMercuryHPMerchantID = value; }
        }

        public string MercuryHPUserID
        {
            get { return strMercuryHPUserID; }
            set { strMercuryHPUserID = value; }
        }

        public int DisplayLogoOrWeb
        {
            get { return intDisplayLogoOrWeb; }
            set { intDisplayLogoOrWeb = value; }
        }

        public string DisplayURL
        {
            get { return strDisplayURL; }
            set { strDisplayURL = value; }
        }

        public string BrowsePermission
        {
            get { return strBrowsePermission; }
            set { strBrowsePermission = value; }
        }

        public string ElementHPAcceptorID
        {
            get { return strElementHPAcceptorID; }
            set { strElementHPAcceptorID = value; }
        }

        public string ElementHPAccountID
        {
            get { return strElementHPAccountID; }
            set { strElementHPAccountID = value; }
        }

        public string ElementHPAccountToken
        {
            get { return strElementHPAccountToken; }
            set { strElementHPAccountToken = value; }
        }

        public string ElementHPApplicationID
        {
            get { return strElementHPApplicationID; }
            set { strElementHPApplicationID = value; }
        }

        public int ElementHPMode
        {
            get { return intElementHPMode; }
            set { intElementHPMode = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public SqlTransaction SQLTran
        {
            get { return objSQLTran; }
            set { objSQLTran = value; }
        }

        public DataTable SplitDataTable
        {
            get { return dblSplitDataTable; }
            set { dblSplitDataTable = value; }
        }

        public DataTable SplitDataTable1
        {
            get { return dblSplitDataTable1; }
            set { dblSplitDataTable1 = value; }
        }

        public string IsDuplicateInvoice
        {
            get { return strIsDuplicateInvoice; }
            set { strIsDuplicateInvoice = value; }
        }

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
        }

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
        }

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }

        public int OtherID
        {
            get { return intOtherID; }
            set { intOtherID = value; }
        }

        public int StoreID
        {
            get { return intStoreID; }
            set { intStoreID = value; }
        }

        public int ExpImpID
        {
            get { return intExpImpID; }
            set { intExpImpID = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public string ForcedLogin
        {
            get { return strForcedLogin; }
            set { strForcedLogin = value; }
        }

        public string Tax1Name
        {
            get { return strTax1Name; }
            set { strTax1Name = value; }
        }

        public string Tax2Name
        {
            get { return strTax2Name; }
            set { strTax2Name = value; }
        }

        public string Tax3Name
        {
            get { return strTax3Name; }
            set { strTax3Name = value; }
        }

        public int Tax1Type
        {
            get { return intTax1Type; }
            set { intTax1Type = value; }
        }

        public int Tax2Type
        {
            get { return intTax2Type; }
            set { intTax2Type = value; }
        }

        public int Tax3Type
        {
            get { return intTax3Type; }
            set { intTax3Type = value; }
        }

        public double Tax1Rate
        {
            get { return dblTax1Rate; }
            set { dblTax1Rate = value; }
        }

        public double Tax2Rate
        {
            get { return dblTax2Rate; }
            set { dblTax2Rate = value; }
        }

        public double Tax3Rate
        {
            get { return dblTax3Rate; }
            set { dblTax3Rate = value; }
        }

        public string ClientParam1
        {
            get { return strClientParam1; }
            set { strClientParam1 = value; }
        }

        public string ClientParam2
        {
            get { return strClientParam2; }
            set { strClientParam2 = value; }
        }

        public string ClientParam3
        {
            get { return strClientParam3; }
            set { strClientParam3 = value; }
        }

        public string ClientParam4
        {
            get { return strClientParam4; }
            set { strClientParam4 = value; }
        }

        public string ClientParam5
        {
            get { return strClientParam5; }
            set { strClientParam5 = value; }
        }

        public string Company
        {
            get { return strCompany; }
            set { strCompany = value; }
        }

        public string Address1
        {
            get { return strAddress1; }
            set { strAddress1 = value; }
        }

        public string Address2
        {
            get { return strAddress2; }
            set { strAddress2 = value; }
        }

        public string City
        {
            get { return strCity; }
            set { strCity = value; }
        }

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        public string PostalCode
        {
            get { return strPostalCode; }
            set { strPostalCode = value; }
        }

        public string Phone
        {
            get { return strPhone; }
            set { strPhone = value; }
        }

        public string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }

        public string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public string WebAddress
        {
            get { return strWebAddress; }
            set { strWebAddress = value; }
        }

        public string Use4Decimal
        {
            get { return strUse4Decimal; }
            set { strUse4Decimal = value; }
        }

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public int CustomerInfo
        {
            get { return intCustomerInfo; }
            set { intCustomerInfo = value; }
        }

        public int VendorUpdate
        {
            get { return intVendorUpdate; }
            set { intVendorUpdate = value; }
        }

        public int CloseoutOption
        {
            get { return intCloseoutOption; }
            set { intCloseoutOption = value; }
        }

        public int SignINOption
        {
            get { return intSignINOption; }
            set { intSignINOption = value; }
        }

        public int MonthsInvJrnl
        {
            get { return intMonthsInvJrnl; }
            set { intMonthsInvJrnl = value; }

        }

        public string BlindCountPreview
        {
            get { return strBlindCountPreview; }
            set { strBlindCountPreview = value; }
        }

        public string SalesByHour
        {
            get { return strSalesByHour; }
            set { strSalesByHour = value; }
        }

        public string SalesByDept
        {
            get { return strSalesByDept; }
            set { strSalesByDept = value; }
        }

        public string POSAcceptCheck
        {
            get { return strPOSAcceptCheck; }
            set { strPOSAcceptCheck = value; }
        }

        public string POSDisplayChangeDue
        {
            get { return strPOSDisplayChangeDue; }
            set { strPOSDisplayChangeDue = value; }
        }

        public string POSIDRequired
        {
            get { return strPOSIDRequired; }
            set { strPOSIDRequired = value; }
        }

        public string POSRedirectToChangeDue
        {
            get { return strPOSRedirectToChangeDue; }
            set { strPOSRedirectToChangeDue = value; }
        }

        public string POSShowGiftCertBalance
        {
            get { return strPOSShowGiftCertBalance; }
            set { strPOSShowGiftCertBalance = value; }
        }

        public string NoPriceOnLabelDefault
        {
            get { return strNoPriceOnLabelDefault; }
            set { strNoPriceOnLabelDefault = value; }
        }

        public string QuantityRequired
        {
            get { return strQuantityRequired; }
            set { strQuantityRequired = value; }
        }

        public string UseInvJrnl
        {
            get { return strUseInvJrnl; }
            set { strUseInvJrnl = value; }
        }

        public double GiftCertMaxChange
        {
            get { return dblGiftCertMaxChange; }
            set { dblGiftCertMaxChange = value; }
        }

        public string ReceiptHeader
        {
            get { return strReceiptHeader; }
            set { strReceiptHeader = value; }
        }

        public string ReceiptFooter
        {
            get { return strReceiptFooter; }
            set { strReceiptFooter = value; }
        }

        public string ReceiptLayawayPolicy
        {
            get { return strReceiptLayawayPolicy; }
            set { strReceiptLayawayPolicy = value; }
        }

        public string PoleScreen
        {
            get { return strPoleScreen; }
            set { strPoleScreen = value; }
        }

        public string FormSkin
        {
            get { return strFormSkin; }
            set { strFormSkin = value; }
        }

        public double LayawayDepositPercent
        {
            get { return dblLayawayDepositPercent; }
            set { dblLayawayDepositPercent = value; }
        }

        public int LayawaysDue
        {
            get { return intLayawaysDue; }
            set { intLayawaysDue = value; }
        }

        public int LayawayReceipts
        {
            get { return intLayawayReceipts; }
            set { intLayawayReceipts = value; }
        }

        public bool POSFunctionUpdate
        {
            get { return blPOSFunctionUpdate; }
            set { blPOSFunctionUpdate = value; }
        }

        public string PhotoFilePath
        {
            get { return strPhotoFilePath; }
            set { strPhotoFilePath = value; }
        }

        public string PhotoFilePath1
        {
            get { return strPhotoFilePath1; }
            set { strPhotoFilePath1 = value; }
        }

        public string StoreInfo
        {
            get { return strStoreInfo; }
            set { strStoreInfo = value; }
        }

        public string POSDisplayProductImage
        {
            get { return strPOSDisplayProductImage; }
            set { strPOSDisplayProductImage = value; }
        }

        public int POSMoreFunctionAlignment
        {
            get { return intPOSMoreFunctionAlignment; }
            set { intPOSMoreFunctionAlignment = value; }
        }

        public int UsePriceLevel
        {
            get { return intUsePriceLevel; }
            set { intUsePriceLevel = value; }
        }

        public int ChangedByAdmin
        {
            get { return intChangedByAdmin; }
            set { intChangedByAdmin = value; }
        }

        public bool FunctionButtonAccess
        {
            get { return blFunctionButtonAccess; }
            set { blFunctionButtonAccess = value; }
        }

        public string GeneralReceiptPrint
        {
            get { return strGeneralReceiptPrint; }
            set { strGeneralReceiptPrint = value; }
        }

        

        public string CloseoutExport
        {
            get { return strCloseoutExport; }
            set { strCloseoutExport = value; }
        }

        public string CardPublisherName
        {
            get { return strCardPublisherName; }
            set { strCardPublisherName = value; }
        }

        public string CardPublisherEMail
        {
            get { return strCardPublisherEMail; }
            set { strCardPublisherEMail = value; }
        }

        public string PayWAREUserID
        {
            get { return strPayWAREUserID; }
            set { strPayWAREUserID = value; }
        }
        public string PayWAREUserPW
        {
            get { return strPayWAREUserPW; }
            set { strPayWAREUserPW = value; }
        }
        public string PayWAREClientID
        {
            get { return strPayWAREClientID; }
            set { strPayWAREClientID = value; }
        }
        public string PayWAREDevice
        {
            get { return strPayWAREDevice; }
            set { strPayWAREDevice = value; }
        }
        public string PayWARECOMPort
        {
            get { return strPayWARECOMPort; }
            set { strPayWARECOMPort = value; }
        }
        public string PayWAREServerIP
        {
            get { return strPayWAREServerIP; }
            set { strPayWAREServerIP = value; }
        }

        public string PayWAREPort
        {
            get { return strPayWAREPort; }
            set { strPayWAREPort = value; }
        }

        public string AutoPO
        {
            get { return strAutoPO; }
            set { strAutoPO = value; }
        }

        public string AutoCustomer
        {
            get { return strAutoCustomer; }
            set { strAutoCustomer = value; }
        }

        public string AutoGC
        {
            get { return strAutoGC; }
            set { strAutoGC = value; }
        }

        public string SingleGC
        {
            get { return strSingleGC; }
            set { strSingleGC = value; }
        }

        public string CentralExportImport
        {
            get { return strCentralExportImport; }
            set { strCentralExportImport = value; }
        }

        public string StoreCode
        {
            get { return strStoreCode; }
            set { strStoreCode = value; }
        }

        public string StoreName
        {
            get { return strStoreName; }
            set { strStoreName = value; }
        }
        
        public string SMTPHost
        {
            get { return strSMTPHost; }
            set { strSMTPHost = value; }
        }

        public string SMTPPassword
        {
            get { return strSMTPPassword; }
            set { strSMTPPassword = value; }
        }

        public string SMTPUser
        {
            get { return strSMTPUser; }
            set { strSMTPUser = value; }
        }

        public string FromAddress
        {
            get { return strFromAddress; }
            set { strFromAddress = value; }
        }

        public string ToAddress
        {
            get { return strToAddress; }
            set { strToAddress = value; }
        }

        public string ReceiptPrintOnReturn
        {
            get { return strReceiptPrintOnReturn; }
            set { strReceiptPrintOnReturn = value; }
        }

        public string BarTenderLabelPrint
        {
            get { return strBarTenderLabelPrint; }
            set { strBarTenderLabelPrint = value; }
        }

        public string CalculatorStyleKeyboard
        {
            get { return strCalculatorStyleKeyboard; }
            set { strCalculatorStyleKeyboard = value; }
        }

        public int LinkGL1
        {
            get { return intLinkGL1; }
            set { intLinkGL1 = value; }
        }

        public int LinkGL2
        {
            get { return intLinkGL2; }
            set { intLinkGL2 = value; }
        }

        public int LinkGL3
        {
            get { return intLinkGL3; }
            set { intLinkGL3 = value; }
        }

        public int LinkGL4
        {
            get { return intLinkGL4; }
            set { intLinkGL4 = value; }
        }

        public int LinkGL5
        {
            get { return intLinkGL5; }
            set { intLinkGL5 = value; }
        }

        public int LinkGL6
        {
            get { return intLinkGL6; }
            set { intLinkGL6 = value; }
        }

        public int SmartGrocerStore
        {
            get { return intSmartGrocerStore; }
            set { intSmartGrocerStore = value; }
        }

        public string PriceSmartCurrency
        {
            get { return strPriceSmartCurrency; }
            set { strPriceSmartCurrency = value; }
        }

        #endregion

        private byte[] GetPhoto(string filePath)
        {
            if (filePath.Trim() == "") return null;
            if (filePath.Trim() == "null") return null;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] photo = br.ReadBytes((int)fs.Length);
            
            br.Close();
            fs.Close();
            return photo;
        }

        private void AdjustLinkPrinters(SqlCommand objSQlComm)
        {
            try
            {
                if (dblPrinterDataTable == null) return;
                foreach (DataRow dr in dblPrinterDataTable.Rows)
                {
                    string colID = "";
                    string colLinkPrinter = "";

                    if (dr["ID"].ToString() == "") continue;

                    colID = dr["ID"].ToString();
                    colLinkPrinter = dr["LinkPrinter"].ToString();

                    strParamName = "All Printers - " + colID.ToString();
                    strParamValue = colLinkPrinter;
                    InsertLinkPrinters(objSQlComm);

                }
                //dblPrinterDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustLinkTemplates(SqlCommand objSQlComm)
        {
            try
            {
                if (dblPrinterDataTable == null) return;
                foreach (DataRow dr in dblPrinterDataTable.Rows)
                {
                    string colID = "";
                    string colLinkTemplate = "";

                    if (dr["ID"].ToString() == "") continue;

                    colID = dr["ID"].ToString();
                    colLinkTemplate = dr["LinkTemplate"].ToString();

                    strParamName = "All Templates - " + colID.ToString();
                    strParamValue = GetLinkTemplateID(objSQlComm,colLinkTemplate);
                    InsertLinkTemplates(objSQlComm);

                }
                //dblPrinterDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }


        public string GetLinkTemplateID(SqlCommand objSQlComm, string colLinkTemplate)
        {
            string val = "";
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " select isnull(ID,0) as rID from ReceiptTemplateMaster where TemplateType = 'Item' and TemplateName = '" + colLinkTemplate + "'";
                      
            objSQlComm.CommandText = strSQLComm;

            try
            {
                

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    try
                    {
                        val = objSQLReader["rID"].ToString();
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                return "";
            }
        }

        public string InsertLinkPrinters(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup ( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = strParamName;
                objSQlComm.Parameters["@ParamValue"].Value = strParamValue;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                return strErrMsg;
            }
        }

        public string InsertLinkTemplates(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup ( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = strParamName;
                objSQlComm.Parameters["@ParamValue"].Value = strParamValue;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                return strErrMsg;
            }
        }

        public void BeginTransaction()
        {
            //SaveComm = new SqlCommand(); 
            SaveTran = null;
            SaveCon = this.sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();
        }

        public void EndTransaction()
        {
            SaveTran.Commit();
            SaveTran.Dispose();
        }

        public bool InsertTax()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                // Add or Edit Invoice Data //
                if (strMode == "Add")
                {
                    //InsertData(SaveComm);
                    //UpdateInvoiceNo(SaveComm);
                }
                else
                {
                    UpdateTaxSetup(SaveComm);
                }
                // Add Item details after deleting existing records //
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Update Tax Setup Data

        public string UpdateTaxSetup(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update Setup set Tax1Type=@Tax1Type,Tax2Type=@Tax2Type,Tax3Type=@Tax3Type,"
                                  + " Tax1Rate=@Tax1Rate,Tax2Rate=@Tax2Rate,Tax3Rate=@Tax3Rate, "
                                  + " Tax1Name=@Tax1Name,Tax2Name=@Tax2Name,Tax3Name=@Tax3Name, "
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";

                objSQlComm.CommandText = strSQLComm;

                
                objSQlComm.Parameters.Add(new SqlParameter("@Tax1Rate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax2Rate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax3Rate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax1Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax2Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax3Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@Tax1Rate"].Value = dblTax1Rate;
                objSQlComm.Parameters["@Tax2Rate"].Value = dblTax2Rate;
                objSQlComm.Parameters["@Tax3Rate"].Value = dblTax3Rate;
                objSQlComm.Parameters["@Tax1Type"].Value = intTax1Type;
                objSQlComm.Parameters["@Tax2Type"].Value = intTax2Type;
                objSQlComm.Parameters["@Tax3Type"].Value = intTax3Type;
                objSQlComm.Parameters["@Tax1Name"].Value = strTax1Name;
                objSQlComm.Parameters["@Tax2Name"].Value = strTax2Name;
                objSQlComm.Parameters["@Tax3Name"].Value = strTax3Name;
                
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                
                return "";
            }
            catch (SqlException SQLDBException)
            {
                objSQLTran.Rollback();
                objSQlComm.Dispose();
                strErrorMsg = SQLDBException.Message;
                return strErrorMsg;
            }
            
        }

        #endregion

        #region FetchTaxSetupdata

        public DataTable FetchTaxSetup()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select Tax1Type,Tax2Type,Tax3Type, Tax1Rate,Tax2Rate,Tax3Rate, "
                              + " Tax1Name,Tax2Name,Tax3Name from Setup ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Tax1Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Rate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Rate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Rate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Tax1Type"].ToString(),
												   objSQLReader["Tax2Type"].ToString(),
												   objSQLReader["Tax3Type"].ToString(),
												   objSQLReader["Tax1Rate"].ToString(),
                                                   objSQLReader["Tax2Rate"].ToString(),
                                                   objSQLReader["Tax3Rate"].ToString(),
                                                   objSQLReader["Tax1Name"].ToString(),
                                                   objSQLReader["Tax2Name"].ToString(),
                                                   objSQLReader["Tax3Name"].ToString()});
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return null;
            }
        }

        #endregion

        #region Checking Setup Entry
        public int IsExistSetupData()
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Setup ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["RECCOUNT"]);
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }
        #endregion 

        #region Dynamic Parameters Data

        #region Insert Data
        public string InsertClientParamData()
        {
            string strSQLComm = " insert into Setup( ClientParam1,ClientParam2,ClientParam3,ClientParam4,ClientParam5, "
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @ClientParam1,@ClientParam2,@ClientParam3,@ClientParam4,@ClientParam5, "
                                + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam5", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ClientParam1"].Value = strClientParam1;
                objSQlComm.Parameters["@ClientParam2"].Value = strClientParam2;
                objSQlComm.Parameters["@ClientParam3"].Value = strClientParam3;
                objSQlComm.Parameters["@ClientParam4"].Value = strClientParam4;
                objSQlComm.Parameters["@ClientParam5"].Value = strClientParam5;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    
                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }
        #endregion

        #region Update Data

        public string UpdateClientParamData()
        {
            string strSQLComm = " update Setup set ClientParam1=@ClientParam1,ClientParam2=@ClientParam2,"
                                + " ClientParam3=@ClientParam3,ClientParam4=@ClientParam4,ClientParam5=@ClientParam5,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientParam5", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ClientParam1"].Value = strClientParam1;
                objSQlComm.Parameters["@ClientParam2"].Value = strClientParam2;
                objSQlComm.Parameters["@ClientParam3"].Value = strClientParam3;
                objSQlComm.Parameters["@ClientParam4"].Value = strClientParam4;
                objSQlComm.Parameters["@ClientParam5"].Value = strClientParam5;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        #endregion

        #region Fetch data

        public DataTable FetchClientParamData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ClientParam1,ClientParam2,ClientParam3,ClientParam4,ClientParam5 from Setup ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ClientParam1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClientParam2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClientParam3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClientParam4", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClientParam5", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ClientParam1"].ToString(),
												   objSQLReader["ClientParam2"].ToString(),
                                                   objSQLReader["ClientParam3"].ToString(),
                                                   objSQLReader["ClientParam4"].ToString(),
                                                   objSQLReader["ClientParam5"].ToString()});
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

        #endregion

        #endregion

        # region only FTP insert /update


        #region Insert Data
        public string InsertFTPData()
        {
            string strSQLComm = " insert into setup( FTPhost,FTPuser,FTPpswd ) values( @FTPhost,@FTPuser,@FTPpswd ) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FTPhost", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FTPuser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FTPpswd", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@FTPhost"].Value = strFTPhost;
                objSQlComm.Parameters["@FTPuser"].Value = strFTPuser;
                objSQlComm.Parameters["@FTPpswd"].Value = strFTPpswd;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }
        #endregion

        #region Update Data
        public string UpdateFTPData()
        {
            string strSQLComm = " update setup set FTPhost=@FTPhost,FTPuser=@FTPuser,FTPpswd=@FTPpswd ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FTPhost", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FTPuser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FTPpswd", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@FTPhost"].Value = strFTPhost;
                objSQlComm.Parameters["@FTPuser"].Value = strFTPuser;
                objSQlComm.Parameters["@FTPpswd"].Value = strFTPpswd;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }
        #endregion

        #endregion

        #region General Settings Data

        #region Insert Other Data

        public string InsertGSData1(SqlCommand objSQlComm)
        {


            SqlDataReader objsqlReader = null;
            objSQlComm.Parameters.Clear();
            string strSQLComm = " insert into Setup( Use4Decimal,DecimalPlace,CustomerInfo,VendorUpdate,CloseoutOption,MonthsInvJrnl,BlindCountPreview,"
                               + " SalesByHour,SalesByDept,POSAcceptCheck,POSDisplayChangeDue,POSIDRequired,NoPriceOnLabelDefault,"
                               + " QuantityRequired,UseInvJrnl,GiftCertMaxChange,LayawayDepositPercent,LayawaysDue,LayawayReceipts,"
                               + " ReceiptLayawayPolicy,ReceiptHeader,ReceiptFooter,PoleScreen,FormSkin,POSDisplayProductImage,"
                               + " POSMoreFunctionAlignment,POSRedirectToChangeDue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,"
                               + " SignInOption,PriceLevelApplicableDate,POSShowGiftCertBalance,ForcedLogin,"
                               + " GeneralReceiptPrint,CloseoutExport,CardPublisherName,CardPublisherEMail,AutoPO,"

                               //+ " POSCardPayment,ElementHPMode,ElementHPAccountID,ElementHPAccountToken,ElementHPApplicationID,"
                               //+ " ElementHPAcceptorID,PaymentGateway,MercuryHPMerchantID,MercuryHPUserID, "
                               + " AutoCustomer,IsDuplicateInvoice,ReceiptPrintOnReturn,DisplayLogoOrWeb,DisplayURL,"
                               + " POSRemotePrint,PurgeCutOffDay,BarTenderLabelPrint,"
                               + " UseCustomerNameInLabelPrint,PreprintedReceipt,"
                               + " POSPrintInvoice,AcceptTips,ShowTipsInReceipt,ShowFoodStampTotal,EasyTendering,ShowFeesInReceipt,ReportEmail,"
                               + " REHost,REPort,REUser,REPassword,RESSL,REFromAddress,REFromName,REReplyTo,ShowSaleSaveInReceipt,"
                               + " BottleDeposit,AllowTerminalCloseout,AddGallon,Print2TicketsForRepair,CalculatorStyleKeyboard,LayawayPaymentOption,"
                               + " LinkGL1,LinkGL2,LinkGL3,LinkGL4,LinkGL5,LinkGL6,NoSaleReceipt,HouseAccountBalanceInReceipt,FocusOnEditProduct,MaxScaleWeight, "
                               + " ScaleDevice, NotReadScaleBarcodeCheckDigit, DatalogicScanner8200, NTEPCert, PrintTrainingMode, "
                               + " EnableBooking,Booking_CustomerSiteURL,Booking_BusinessDetails,Booking_PrivacyNotice,Booking_Phone,"
                               + " Booking_Email, Booking_LinkedIn_Link, Booking_Twitter_Link, Booking_Facebook_Link, Booking_Lead_Day, Booking_Lead_Hour, "
                               + " Booking_Lead_Minute, Booking_Slot_Hour, Booking_Slot_Minute, Booking_Scheduling_NoLimit, "
                               + " Booking_Mon_Check,Booking_Mon_Start,Booking_Mon_End,Booking_Tue_Check,Booking_Tue_Start,Booking_Tue_End,"
                               + " Booking_Wed_Check, Booking_Wed_Start, Booking_Wed_End, Booking_Thu_Check, Booking_Thu_Start, Booking_Thu_End, "
                               + " Booking_Fri_Check,Booking_Fri_Start, Booking_Fri_End, Booking_Sat_Check, Booking_Sat_Start, Booking_Sat_End, "
                               + " Booking_Sun_Check, Booking_Sun_Start, Booking_Sun_End, BookingServer,BookingDB,BookingDBUser,BookingDBPassword, "
                               + " BookingScheduleWindowDay, Booking_Reminder_Hour, Booking_Reminder_Minute, PrintBlindDropCloseout, AllowNegativeStoreCredit, TaxInclusive, "
                               + " CountryName,CurrencySymbol,DateFormat,SmallCurrencyName,BigCurrencyName,"
                               + " Coin1Name,Coin2Name,Coin3Name,Coin4Name,Coin5Name,Coin6Name,Coin7Name,"
                               + " Currency1Name,Currency2Name,Currency3Name,Currency4Name,Currency5Name,Currency6Name,Currency7Name,Currency8Name,Currency9Name,Currency10Name,"
                               + " Currency1QuickTender,Currency2QuickTender,Currency3QuickTender,Currency4QuickTender,Currency5QuickTender,"
                               + " Currency6QuickTender,Currency7QuickTender,Currency8QuickTender,Currency9QuickTender,Currency10QuickTender,DefaultInternationalization, "
                               + " UseTouchKeyboardInAdmin,UseTouchKeyboardInPOS,EvoApiBaseAddress,"
                               + " Paymentsense_AccountName,Paymentsense_ApiKey,Paymentsense_SoftwareHouseId,"
                               + " Paymentsense_InstallerId,BackupType,BackupStorageType,PrintLogoInReceipt,AutoGC,SingleGC,PrintDuplicateGiftCertSaleReceipt,SMPTServer,AppointmentEmailBody,ItemExpiryAlertDay) "
                               //+ " SalesService,RentService,RepairService,POSDefaultService ) "
                               + " values ( @Use4Decimal,@DecimalPlace,@CustomerInfo,@VendorUpdate,@CloseoutOption,@MonthsInvJrnl, "
                               + " @BlindCountPreview,@SalesByHour,@SalesByDept,@POSAcceptCheck, "
                               + " @POSDisplayChangeDue,@POSIDRequired,@NoPriceOnLabelDefault,"
                               + " @QuantityRequired,@UseInvJrnl,@GiftCertMaxChange,"
                               + " @LayawayDepositPercent,@LayawaysDue,@LayawayReceipts,@ReceiptLayawayPolicy,"
                               + " @ReceiptHeader,@ReceiptFooter,@PoleScreen,@FormSkin,@POSDisplayProductImage,@POSMoreFunctionAlignment,"
                               + " @POSRedirectToChangeDue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn, "
                               + " @SignInOption,@PriceLevelApplicableDate,@POSShowGiftCertBalance,@ForcedLogin, "
                               + " @GeneralReceiptPrint,@CloseoutExport,@CardPublisherName,@CardPublisherEMail,@AutoPO,"

                               //+ " @POSCardPayment,@ElementHPMode,@ElementHPAccountID,@ElementHPAccountToken,@ElementHPApplicationID,"
                               //+ " @ElementHPAcceptorID,@PaymentGateway,@MercuryHPMerchantID,@MercuryHPUserID, "
                               + " @AutoCustomer,@IsDuplicateInvoice,@ReceiptPrintOnReturn,@DisplayLogoOrWeb,@DisplayURL,"
                               + " @POSRemotePrint,@PurgeCutOffDay,@BarTenderLabelPrint,"
                               + " @UseCustomerNameInLabelPrint,@PreprintedReceipt,"
                               + " @POSPrintInvoice,@AcceptTips,@ShowTipsInReceipt,@ShowFoodStampTotal,"
                               + " @EasyTendering,@ShowFeesInReceipt,@ReportEmail,@REHost,@REPort,@REUser,@REPassword,"
                               + " @RESSL,@REFromAddress,@REFromName,@REReplyTo, @ShowSaleSaveInReceipt,@BottleRefund,@AllowTerminalCloseout,"
                               + " @AddGallon,@Print2TicketsForRepair,@CalculatorStyleKeyboard,@LayawayPaymentOption,"
                               + " @LinkGL1,@LinkGL2,@LinkGL3,@LinkGL4,@LinkGL5,@LinkGL6,@NoSaleReceipt,@HouseAccountBalanceInReceipt,@FocusOnEditProduct,@MaxScaleWeight, "
                               + " @ScaleDevice, @NotReadScaleBarcodeCheckDigit, @DatalogicScanner8200, @NTEPCert, @PrintTrainingMode, "
                               + " @EnableBooking,@Booking_CustomerSiteURL,@Booking_BusinessDetails,@Booking_PrivacyNotice,@Booking_Phone,"
                               + " @Booking_Email, @Booking_LinkedIn_Link, @Booking_Twitter_Link, @Booking_Facebook_Link, @Booking_Lead_Day, @Booking_Lead_Hour, "
                               + " @Booking_Lead_Minute, @Booking_Slot_Hour, @Booking_Slot_Minute, @Booking_Scheduling_NoLimit, "
                               + " @Booking_Mon_Check, @Booking_Mon_Start, @Booking_Mon_End, @Booking_Tue_Check, @Booking_Tue_Start, @Booking_Tue_End,"
                               + " @Booking_Wed_Check, @Booking_Wed_Start, @Booking_Wed_End, @Booking_Thu_Check, @Booking_Thu_Start, @Booking_Thu_End, "
                               + " @Booking_Fri_Check, @Booking_Fri_Start, @Booking_Fri_End, @Booking_Sat_Check, @Booking_Sat_Start, @Booking_Sat_End, "
                               + " @Booking_Sun_Check, @Booking_Sun_Start, @Booking_Sun_End, @BookingServer,@BookingDB,@BookingDBUser,@BookingDBPassword, "
                               + " @BookingScheduleWindowDay, @Booking_Reminder_Hour, @Booking_Reminder_Minute, @PrintBlindDropCloseout, @AllowNegativeStoreCredit, @TaxInclusive,"
                               + " @CountryName,@CurrencySymbol,@DateFormat,@SmallCurrencyName,@BigCurrencyName,"
                               + " @Coin1Name,@Coin2Name,@Coin3Name,@Coin4Name,@Coin5Name,@Coin6Name,@Coin7Name,"
                               + " @Currency1Name,@Currency2Name,@Currency3Name,@Currency4Name,@Currency5Name,@Currency6Name,@Currency7Name,@Currency8Name,@Currency9Name,@Currency10Name,"
                               + " @Currency1QuickTender,@Currency2QuickTender,@Currency3QuickTender,@Currency4QuickTender,@Currency5QuickTender,@Currency6QuickTender,"
                               + " @Currency7QuickTender,@Currency8QuickTender,@Currency9QuickTender,@Currency10QuickTender,@DefaultInternationalization,@UseTouchKeyboardInAdmin,@UseTouchKeyboardInPOS,@EvoApiBaseAddress, "
                               + " @Paymentsense_AccountName,@Paymentsense_ApiKey,@Paymentsense_SoftwareHouseId,"
                               + " @Paymentsense_InstallerId,@BackupType,@BackupStorageType,@PrintLogoInReceipt,@AutoGC,@SingleGC,@PrintDuplicateGiftCertSaleReceipt,@SMPTServer,@AppointmentEmailBody,@ItemExpiryAlertDay) "
                               //+ " @SalesService,@RentService,@RepairService,@POSDefaultService ) "
                               + " select @@IDENTITY as ID ";


            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Use4Decimal", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerInfo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorUpdate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MonthsInvJrnl", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BlindCountPreview", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SalesByHour", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SalesByDept", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSAcceptCheck", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayChangeDue", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSIDRequired", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSRedirectToChangeDue", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabelDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@QuantityRequired", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@UseInvJrnl", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftCertMaxChange", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@LayawayDepositPercent", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawaysDue", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawayReceipts", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptLayawayPolicy", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptHeader", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptFooter", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PoleScreen", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FormSkin", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayProductImage", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSMoreFunctionAlignment", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSShowGiftCertBalance", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ForcedLogin", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SignInOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceLevelApplicableDate", System.Data.SqlDbType.DateTime));

                //objSQlComm.Parameters.Add(new SqlParameter("@POSCardPayment", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralReceiptPrint", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutExport", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CardPublisherName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CardPublisherEMail", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@AutoPO", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AutoCustomer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@AutoGC", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SingleGC", System.Data.SqlDbType.Char));

                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPMode", System.Data.SqlDbType.Int));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAccountID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAccountToken", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPApplicationID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAcceptorID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@IsDuplicateInvoice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptPrintOnReturn", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayLogoOrWeb", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayURL", System.Data.SqlDbType.NVarChar));

                //objSQlComm.Parameters.Add(new SqlParameter("@PaymentGateway", System.Data.SqlDbType.Int));
                //objSQlComm.Parameters.Add(new SqlParameter("@MercuryHPMerchantID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@MercuryHPUserID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSRemotePrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PurgeCutOffDay", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@BarTenderLabelPrint", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@UseCustomerNameInLabelPrint", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PreprintedReceipt", System.Data.SqlDbType.Char));


                objSQlComm.Parameters.Add(new SqlParameter("@POSPrintInvoice", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AcceptTips", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ShowTipsInReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowFoodStampTotal", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@EasyTendering", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowFeesInReceipt", System.Data.SqlDbType.Char));


                objSQlComm.Parameters.Add(new SqlParameter("@ReportEmail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REHost", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REPort", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@REUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RESSL", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@REFromAddress", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REFromName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REReplyTo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CalculatorStyleKeyboard", System.Data.SqlDbType.Char));


                objSQlComm.Parameters.Add(new SqlParameter("@AllowTerminalCloseout", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AllowTerminalCloseout"].Value = strOtherTerminalCloseout;


                objSQlComm.Parameters.Add(new SqlParameter("@ShowSaleSaveInReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ShowSaleSaveInReceipt"].Value = strShowSaleSaveInReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@BottleRefund", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@BottleRefund"].Value = dblBottleRefund;

                objSQlComm.Parameters.Add(new SqlParameter("@AddGallon", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddGallon"].Value = strAddGallon;

                objSQlComm.Parameters.Add(new SqlParameter("@Print2TicketsForRepair", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Print2TicketsForRepair"].Value = strPrint2TicketsForRepair;

                objSQlComm.Parameters.Add(new SqlParameter("@LayawayPaymentOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LayawayPaymentOption"].Value = intLayawayPaymentOption;

                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL3", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL4", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL5", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL6", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@NoSaleReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@HouseAccountBalanceInReceipt", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@FocusOnEditProduct", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@LinkGL1"].Value = intLinkGL1;
                objSQlComm.Parameters["@LinkGL2"].Value = intLinkGL2;
                objSQlComm.Parameters["@LinkGL3"].Value = intLinkGL3;
                objSQlComm.Parameters["@LinkGL4"].Value = intLinkGL4;
                objSQlComm.Parameters["@LinkGL5"].Value = intLinkGL5;
                objSQlComm.Parameters["@LinkGL6"].Value = intLinkGL6;

                objSQlComm.Parameters["@REHost"].Value = strREHost;
                objSQlComm.Parameters["@REPort"].Value = intREPort;
                objSQlComm.Parameters["@REUser"].Value = strREUser;
                objSQlComm.Parameters["@REPassword"].Value = strREPassword;
                objSQlComm.Parameters["@RESSL"].Value = strRESSL;
                objSQlComm.Parameters["@REFromAddress"].Value = strREFromAddress;
                objSQlComm.Parameters["@REFromName"].Value = strREFromName;
                objSQlComm.Parameters["@REReplyTo"].Value = strREReplyTo;
                objSQlComm.Parameters["@ReportEmail"].Value = strReportEmail;


                objSQlComm.Parameters["@EasyTendering"].Value = strEasyTendering;
                objSQlComm.Parameters["@ShowFeesInReceipt"].Value = strShowFeesInReceipt;

                objSQlComm.Parameters["@AcceptTips"].Value = strAcceptTips;
                objSQlComm.Parameters["@ShowTipsInReceipt"].Value = strShowTipsInReceipt;
                objSQlComm.Parameters["@ShowFoodStampTotal"].Value = strShowFoodStampTotal;

                objSQlComm.Parameters["@Use4Decimal"].Value = strUse4Decimal;

                objSQlComm.Parameters["@DecimalPlace"].Value = intDecimalPlace;
                objSQlComm.Parameters["@CustomerInfo"].Value = intCustomerInfo;
                objSQlComm.Parameters["@VendorUpdate"].Value = intVendorUpdate;
                objSQlComm.Parameters["@CloseoutOption"].Value = intCloseoutOption;
                objSQlComm.Parameters["@MonthsInvJrnl"].Value = intMonthsInvJrnl;
                objSQlComm.Parameters["@BlindCountPreview"].Value = strBlindCountPreview;
                objSQlComm.Parameters["@SalesByHour"].Value = strSalesByHour;
                objSQlComm.Parameters["@SalesByDept"].Value = strSalesByDept;
                objSQlComm.Parameters["@POSAcceptCheck"].Value = strPOSAcceptCheck;
                objSQlComm.Parameters["@POSDisplayChangeDue"].Value = strPOSDisplayChangeDue;
                objSQlComm.Parameters["@POSIDRequired"].Value = strPOSIDRequired;
                objSQlComm.Parameters["@POSRedirectToChangeDue"].Value = strPOSRedirectToChangeDue;
                objSQlComm.Parameters["@NoPriceOnLabelDefault"].Value = strNoPriceOnLabelDefault;
                objSQlComm.Parameters["@QuantityRequired"].Value = strQuantityRequired;
                objSQlComm.Parameters["@UseInvJrnl"].Value = strUseInvJrnl;
                objSQlComm.Parameters["@GiftCertMaxChange"].Value = dblGiftCertMaxChange;

                objSQlComm.Parameters["@LayawayDepositPercent"].Value = dblLayawayDepositPercent;
                objSQlComm.Parameters["@LayawaysDue"].Value = intLayawaysDue;
                objSQlComm.Parameters["@LayawayReceipts"].Value = intLayawayReceipts;
                objSQlComm.Parameters["@ReceiptLayawayPolicy"].Value = strReceiptLayawayPolicy;
                objSQlComm.Parameters["@ReceiptHeader"].Value = strReceiptHeader;
                objSQlComm.Parameters["@ReceiptFooter"].Value = strReceiptFooter;
                objSQlComm.Parameters["@PoleScreen"].Value = strPoleScreen;
                objSQlComm.Parameters["@FormSkin"].Value = strFormSkin;
                objSQlComm.Parameters["@POSDisplayProductImage"].Value = strPOSDisplayProductImage;
                objSQlComm.Parameters["@POSMoreFunctionAlignment"].Value = intPOSMoreFunctionAlignment;
                objSQlComm.Parameters["@SignInOption"].Value = intSignINOption;
                objSQlComm.Parameters["@PriceLevelApplicableDate"].Value = DateTime.Today;
                objSQlComm.Parameters["@POSShowGiftCertBalance"].Value = strPOSShowGiftCertBalance;

                objSQlComm.Parameters["@ForcedLogin"].Value = strForcedLogin;


                objSQlComm.Parameters["@GeneralReceiptPrint"].Value = strGeneralReceiptPrint;
                objSQlComm.Parameters["@CloseoutExport"].Value = strCloseoutExport;

                objSQlComm.Parameters["@CardPublisherName"].Value = strCardPublisherName;
                objSQlComm.Parameters["@CardPublisherEMail"].Value = strCardPublisherEMail;

                objSQlComm.Parameters["@AutoPO"].Value = strAutoPO;
                objSQlComm.Parameters["@AutoCustomer"].Value = strAutoCustomer;

                objSQlComm.Parameters["@AutoGC"].Value = strAutoGC;
                objSQlComm.Parameters["@SingleGC"].Value = strSingleGC;

                //objSQlComm.Parameters["@ElementHPMode"].Value = intElementHPMode;
                //objSQlComm.Parameters["@ElementHPAccountID"].Value = strElementHPAccountID;
                //objSQlComm.Parameters["@ElementHPAccountToken"].Value = strElementHPAccountToken;
                //objSQlComm.Parameters["@ElementHPApplicationID"].Value = strElementHPApplicationID;
                //objSQlComm.Parameters["@ElementHPAcceptorID"].Value = strElementHPAcceptorID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IsDuplicateInvoice"].Value = strIsDuplicateInvoice;
                objSQlComm.Parameters["@ReceiptPrintOnReturn"].Value = strReceiptPrintOnReturn;

                objSQlComm.Parameters["@DisplayLogoOrWeb"].Value = intDisplayLogoOrWeb;
                objSQlComm.Parameters["@DisplayURL"].Value = strDisplayURL;

                //objSQlComm.Parameters["@PaymentGateway"].Value = intPaymentGateway;
                //objSQlComm.Parameters["@MercuryHPMerchantID"].Value = strMercuryHPMerchantID;
                //objSQlComm.Parameters["@MercuryHPUserID"].Value = strMercuryHPUserID;
                objSQlComm.Parameters["@POSRemotePrint"].Value = intPOSPrintTender;
                objSQlComm.Parameters["@PurgeCutOffDay"].Value = intPurgeCutOffDay;

                objSQlComm.Parameters["@BarTenderLabelPrint"].Value = strBarTenderLabelPrint;


                objSQlComm.Parameters["@UseCustomerNameInLabelPrint"].Value = strUseCustomerNameInLabelPrint;
                objSQlComm.Parameters["@PreprintedReceipt"].Value = strPreprintedReceipt;


                objSQlComm.Parameters["@POSPrintInvoice"].Value = intPOSPrintInvoice;
                objSQlComm.Parameters["@CalculatorStyleKeyboard"].Value = strCalculatorStyleKeyboard;

                objSQlComm.Parameters["@NoSaleReceipt"].Value = strNoSaleReceipt;
                objSQlComm.Parameters["@HouseAccountBalanceInReceipt"].Value = strHouseAccountBalanceInReceipt;
                objSQlComm.Parameters["@FocusOnEditProduct"].Value = strFocusOnEditProduct;

                objSQlComm.Parameters.Add(new SqlParameter("@MaxScaleWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@MaxScaleWeight"].Value = dblMaxScaleWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDevice", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ScaleDevice"].Value = strScaleDevice;

                objSQlComm.Parameters.Add(new SqlParameter("@NotReadScaleBarcodeCheckDigit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NotReadScaleBarcodeCheckDigit"].Value = strNotReadScaleBarcodeCheckDigit;

                objSQlComm.Parameters.Add(new SqlParameter("@DatalogicScanner8200", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@DatalogicScanner8200"].Value = strDatalogicScanner8200;

                objSQlComm.Parameters.Add(new SqlParameter("@NTEPCert", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@NTEPCert"].Value = strNTEPCert;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintTrainingMode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintTrainingMode"].Value = strPrintTrainingMode;


                objSQlComm.Parameters.Add(new SqlParameter("@EnableBooking", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@EnableBooking"].Value = strEnableBooking;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Scheduling_NoLimit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Scheduling_NoLimit"].Value = strBooking_Scheduling_NoLimit;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Mon_Check"].Value = strBooking_Mon_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Tue_Check"].Value = strBooking_Tue_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Wed_Check"].Value = strBooking_Wed_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Thu_Check"].Value = strBooking_Thu_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Fri_Check"].Value = strBooking_Fri_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Sat_Check"].Value = strBooking_Sat_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Sun_Check"].Value = strBooking_Sun_Check;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_CustomerSiteURL", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_CustomerSiteURL"].Value = strBooking_CustomerSiteURL;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_BusinessDetails", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_BusinessDetails"].Value = strBooking_BusinessDetails;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_PrivacyNotice", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_PrivacyNotice"].Value = strBooking_PrivacyNotice;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Phone"].Value = strBooking_Phone;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Email", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Email"].Value = strBooking_Email;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_LinkedIn_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_LinkedIn_Link"].Value = strBooking_LinkedIn_Link;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Twitter_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Twitter_Link"].Value = strBooking_Twitter_Link;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Facebook_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Facebook_Link"].Value = strBooking_Facebook_Link;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Day", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Day"].Value = intBooking_Lead_Day;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Hour"].Value = intBooking_Lead_Hour;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Minute"].Value = intBooking_Lead_Minute;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Slot_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Slot_Hour"].Value = intBooking_Slot_Hour;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Slot_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Slot_Minute"].Value = intBooking_Slot_Minute;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Mon_Start"].Value = intBooking_Mon_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Mon_End"].Value = intBooking_Mon_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Tue_Start"].Value = intBooking_Tue_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Tue_End"].Value = intBooking_Tue_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Wed_Start"].Value = intBooking_Wed_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Wed_End"].Value = intBooking_Wed_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Thu_Start"].Value = intBooking_Thu_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Thu_End"].Value = intBooking_Thu_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Fri_Start"].Value = intBooking_Fri_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Fri_End"].Value = intBooking_Fri_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sat_Start"].Value = intBooking_Sat_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sat_End"].Value = intBooking_Sat_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sun_Start"].Value = intBooking_Sun_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sun_End"].Value = intBooking_Sun_End;


                objSQlComm.Parameters.Add(new SqlParameter("@BookingServer", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingServer"].Value = strBookingServer;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDB", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDB"].Value = strBookingDB;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDBUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDBUser"].Value = strBookingDBUser;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDBPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDBPassword"].Value = strBookingDBPassword;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingScheduleWindowDay", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BookingScheduleWindowDay"].Value = intBookingScheduleWindowDay;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Reminder_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Reminder_Hour"].Value = intBooking_Reminder_Hour;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Reminder_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Reminder_Minute"].Value = intBooking_Reminder_Minute;


                objSQlComm.Parameters.Add(new SqlParameter("@PrintBlindDropCloseout", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintBlindDropCloseout"].Value = strPrintBlindDropCloseout;

                objSQlComm.Parameters.Add(new SqlParameter("@AllowNegativeStoreCredit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AllowNegativeStoreCredit"].Value = strAllowNegativeStoreCredit;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxInclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@TaxInclusive"].Value = strTaxInclusive;

                objSQlComm.Parameters.Add(new SqlParameter("@CountryName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CountryName"].Value = strCountryName;

                objSQlComm.Parameters.Add(new SqlParameter("@CurrencySymbol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CurrencySymbol"].Value = strCurrencySymbol;

                objSQlComm.Parameters.Add(new SqlParameter("@DateFormat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DateFormat"].Value = strDateFormat;

                objSQlComm.Parameters.Add(new SqlParameter("@SmallCurrencyName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SmallCurrencyName"].Value = strSmallCurrencyName;

                objSQlComm.Parameters.Add(new SqlParameter("@BigCurrencyName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BigCurrencyName"].Value = strBigCurrencyName;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin1Name"].Value = strCoin1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin2Name"].Value = strCoin2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin3Name"].Value = strCoin3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin4Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin4Name"].Value = strCoin4Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin5Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin5Name"].Value = strCoin5Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin6Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin6Name"].Value = strCoin6Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin7Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin7Name"].Value = strCoin7Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency1Name"].Value = strCurrency1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency2Name"].Value = strCurrency2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency3Name"].Value = strCurrency3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency4Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency4Name"].Value = strCurrency4Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency5Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency5Name"].Value = strCurrency5Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency6Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency6Name"].Value = strCurrency6Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency7Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency7Name"].Value = strCurrency7Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency8Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency8Name"].Value = strCurrency8Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency9Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency9Name"].Value = strCurrency9Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency10Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency10Name"].Value = strCurrency10Name;


                objSQlComm.Parameters.Add(new SqlParameter("@Currency1QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency1QuickTender"].Value = strCurrency1QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency2QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency2QuickTender"].Value = strCurrency2QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency3QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency3QuickTender"].Value = strCurrency3QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency4QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency4QuickTender"].Value = strCurrency4QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency5QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency5QuickTender"].Value = strCurrency5QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency6QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency6QuickTender"].Value = strCurrency6QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency7QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency7QuickTender"].Value = strCurrency7QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency8QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency8QuickTender"].Value = strCurrency8QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency9QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency9QuickTender"].Value = strCurrency9QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency10QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency10QuickTender"].Value = strCurrency10QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@DefaultInternationalization", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@DefaultInternationalization"].Value = "N";

                objSQlComm.Parameters.Add(new SqlParameter("@UseTouchKeyboardInAdmin", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@UseTouchKeyboardInAdmin"].Value = strUseTouchKeyboardInAdmin;

                objSQlComm.Parameters.Add(new SqlParameter("@UseTouchKeyboardInPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@UseTouchKeyboardInPOS"].Value = strUseTouchKeyboardInPOS;

                objSQlComm.Parameters.Add(new SqlParameter("@EvoApiBaseAddress", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@EvoApiBaseAddress"].Value = strEvoApiBaseAddress;


                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_AccountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_AccountName"].Value = strPaymentsense_AccountName;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_ApiKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_ApiKey"].Value = strPaymentsense_ApiKey;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_SoftwareHouseId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_SoftwareHouseId"].Value = strPaymentsense_SoftwareHouseId;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_InstallerId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_InstallerId"].Value = strPaymentsense_InstallerId;


                objSQlComm.Parameters.Add(new SqlParameter("@BackupType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BackupType"].Value = intBackupType;

                objSQlComm.Parameters.Add(new SqlParameter("@BackupStorageType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BackupStorageType"].Value = intBackupStorageType;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintLogoInReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintLogoInReceipt"].Value = strPrintLogoInReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintDuplicateGiftCertSaleReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintDuplicateGiftCertSaleReceipt"].Value = strPrintDuplicateGiftCertSaleReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@SMPTServer", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SMPTServer"].Value = intSMTPServer;

                objSQlComm.Parameters.Add(new SqlParameter("@AppointmentEmailBody", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@AppointmentEmailBody"].Value = strAppointmentEmailBody;


                objSQlComm.Parameters.Add(new SqlParameter("@ItemExpiryAlertDay", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemExpiryAlertDay"].Value = intItemExpiryAlertDay;


                

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";

                objsqlReader.Close();
                objsqlReader.Dispose();

                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion

        #region Update Other Data

        public string UpdateGSData1(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = " update Setup set Use4Decimal=@Use4Decimal,DecimalPlace=@DecimalPlace,CustomerInfo=@CustomerInfo,VendorUpdate=@VendorUpdate,"
                              + " CloseoutOption=@CloseoutOption,MonthsInvJrnl=@MonthsInvJrnl,BlindCountPreview=@BlindCountPreview,"
                              + " SalesByHour=@SalesByHour,SalesByDept=@SalesByDept,POSAcceptCheck=@POSAcceptCheck,"
                              + " POSDisplayChangeDue=@POSDisplayChangeDue,POSIDRequired=@POSIDRequired,FormSkin=@FormSkin,"
                              + " NoPriceOnLabelDefault=@NoPriceOnLabelDefault,QuantityRequired=@QuantityRequired,UseInvJrnl=@UseInvJrnl,"
                              + " GiftCertMaxChange=@GiftCertMaxChange,LayawayDepositPercent=@LayawayDepositPercent,LayawaysDue=@LayawaysDue,"
                              + " LayawayReceipts=@LayawayReceipts,ReceiptLayawayPolicy=@ReceiptLayawayPolicy,ReceiptHeader=@ReceiptHeader,"
                              + " ReceiptFooter=@ReceiptFooter,PoleScreen=@PoleScreen,POSDisplayProductImage=@POSDisplayProductImage,"
                              + " POSMoreFunctionAlignment=@POSMoreFunctionAlignment,POSRedirectToChangeDue=@POSRedirectToChangeDue,"
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, SignInOption=@SignInOption,ForcedLogin=@ForcedLogin,"
                              + " POSShowGiftCertBalance=@POSShowGiftCertBalance,CloseoutExport=@CloseoutExport,"
                              + " GeneralReceiptPrint=@GeneralReceiptPrint,CardPublisherName=@CardPublisherName,AutoPO=@AutoPO,"
                              + " CardPublisherEMail=@CardPublisherEMail,"
                              + " AutoCustomer=@AutoCustomer,IsDuplicateInvoice=@IsDuplicateInvoice,ReceiptPrintOnReturn=@ReceiptPrintOnReturn, "
                              + " DisplayLogoOrWeb=@DisplayLogoOrWeb,DisplayURL=@DisplayURL,PreprintedReceipt=@PreprintedReceipt,"
                              + " POSRemotePrint = @POSRemotePrint,PurgeCutOffDay = @PurgeCutOffDay,BarTenderLabelPrint=@BarTenderLabelPrint, "
                              + " UseCustomerNameInLabelPrint=@UseCustomerNameInLabelPrint,"
                              + " POSPrintInvoice=@POSPrintInvoice,AcceptTips=@AcceptTips,ShowTipsInReceipt=@ShowTipsInReceipt,"
                              + " ShowFoodStampTotal=@ShowFoodStampTotal,EasyTendering=@EasyTendering,ShowFeesInReceipt=@ShowFeesInReceipt, "
                              + " ReportEmail=@ReportEmail,REHost=@REHost,REPort=@REPort,REUser=@REUser,REPassword=@REPassword,RESSL=@RESSL,"
                              + " REFromAddress=@REFromAddress,REFromName=@REFromName,REReplyTo=@REReplyTo,"
                              + " ShowSaleSaveInReceipt=@ShowSaleSaveInReceipt,BottleDeposit=@BottleRefund,"
                              + " AllowTerminalCloseout=@AllowTerminalCloseout,AddGallon=@AddGallon, Print2TicketsForRepair=@Print2TicketsForRepair, "
                              + " CalculatorStyleKeyboard=@CalculatorStyleKeyboard,LayawayPaymentOption=@LayawayPaymentOption, "
                              + " LinkGL1=@LinkGL1,LinkGL2=@LinkGL2,LinkGL3=@LinkGL3,LinkGL4=@LinkGL4,LinkGL5=@LinkGL5,LinkGL6=@LinkGL6, "
                              + " NoSaleReceipt=@NoSaleReceipt,HouseAccountBalanceInReceipt=@HouseAccountBalanceInReceipt,FocusOnEditProduct=@FocusOnEditProduct,"
                              + " MaxScaleWeight=@MaxScaleWeight, ScaleDevice=@ScaleDevice, NotReadScaleBarcodeCheckDigit=@NotReadScaleBarcodeCheckDigit, "
                              + " DatalogicScanner8200=@DatalogicScanner8200, NTEPCert=@NTEPCert, PrintTrainingMode=@PrintTrainingMode, "
                              + " EnableBooking=@EnableBooking,Booking_CustomerSiteURL=@Booking_CustomerSiteURL,Booking_BusinessDetails=@Booking_BusinessDetails,"
                              + " Booking_PrivacyNotice=@Booking_PrivacyNotice,Booking_Phone=@Booking_Phone,Booking_Email=@Booking_Email, Booking_LinkedIn_Link=@Booking_LinkedIn_Link,"
                              + " Booking_Twitter_Link=@Booking_Twitter_Link, Booking_Facebook_Link=@Booking_Facebook_Link, Booking_Lead_Day=@Booking_Lead_Day, "
                              + " Booking_Lead_Hour=@Booking_Lead_Hour,Booking_Lead_Minute=@Booking_Lead_Minute, "
                              + " Booking_Slot_Hour=@Booking_Slot_Hour, Booking_Slot_Minute=@Booking_Slot_Minute, Booking_Scheduling_NoLimit=@Booking_Scheduling_NoLimit, "
                              + " Booking_Mon_Check=@Booking_Mon_Check,Booking_Mon_Start=@Booking_Mon_Start,Booking_Mon_End=@Booking_Mon_End,"
                              + " Booking_Tue_Check=@Booking_Tue_Check,Booking_Tue_Start=@Booking_Tue_Start,Booking_Tue_End=@Booking_Tue_End,"
                              + " Booking_Wed_Check=@Booking_Wed_Check, Booking_Wed_Start=@Booking_Wed_Start, Booking_Wed_End=@Booking_Wed_End,"
                              + " Booking_Thu_Check=@Booking_Thu_Check, Booking_Thu_Start=@Booking_Thu_Start, Booking_Thu_End=@Booking_Thu_End, "
                              + " Booking_Fri_Check=@Booking_Fri_Check,Booking_Fri_Start=@Booking_Fri_Start, Booking_Fri_End=@Booking_Fri_End, "
                              + " Booking_Sat_Check=@Booking_Sat_Check, Booking_Sat_Start=@Booking_Sat_Start, Booking_Sat_End=@Booking_Sat_End, "
                              + " Booking_Sun_Check=@Booking_Sun_Check, Booking_Sun_Start=@Booking_Sun_Start, Booking_Sun_End=@Booking_Sun_End, "
                              + " BookingServer=@BookingServer,BookingDB=@BookingDB,BookingDBUser=@BookingDBUser,BookingDBPassword=@BookingDBPassword, "
                              + " BookingScheduleWindowDay=@BookingScheduleWindowDay, Booking_Reminder_Hour=@Booking_Reminder_Hour, Booking_Reminder_Minute = @Booking_Reminder_Minute, "
                              + " PrintBlindDropCloseout=@PrintBlindDropCloseout, AllowNegativeStoreCredit=@AllowNegativeStoreCredit, TaxInclusive=@TaxInclusive, "
                              + " CountryName=@CountryName,CurrencySymbol=@CurrencySymbol,DateFormat=@DateFormat,"
                              + " SmallCurrencyName=@SmallCurrencyName,BigCurrencyName=@BigCurrencyName,"
                              + " Coin1Name=@Coin1Name,Coin2Name=@Coin2Name,Coin3Name=@Coin3Name,Coin4Name=@Coin4Name,Coin5Name=@Coin5Name,"
                              + " Coin6Name=@Coin6Name,Coin7Name=@Coin7Name,Currency1Name=@Currency1Name,Currency2Name=@Currency2Name,"
                              + " Currency3Name=@Currency3Name,Currency4Name=@Currency4Name,Currency5Name=@Currency5Name,Currency6Name=@Currency6Name,"
                              + " Currency7Name=@Currency7Name,Currency8Name=@Currency8Name,Currency9Name=@Currency9Name,Currency10Name=@Currency10Name,"
                              + " Currency1QuickTender=@Currency1QuickTender,Currency2QuickTender=@Currency2QuickTender,"
                              + " Currency3QuickTender=@Currency3QuickTender,Currency4QuickTender=@Currency4QuickTender,"
                              + " Currency5QuickTender=@Currency5QuickTender,Currency6QuickTender=@Currency6QuickTender,"
                              + " Currency7QuickTender=@Currency7QuickTender,Currency8QuickTender=@Currency8QuickTender,Currency9QuickTender=@Currency9QuickTender,Currency10QuickTender=@Currency10QuickTender,"
                              + " DefaultInternationalization=@DefaultInternationalization,"
                              + " UseTouchKeyboardInAdmin=@UseTouchKeyboardInAdmin,UseTouchKeyboardInPOS=@UseTouchKeyboardInPOS,EvoApiBaseAddress=@EvoApiBaseAddress,"
                              + " Paymentsense_AccountName=@Paymentsense_AccountName,Paymentsense_ApiKey=@Paymentsense_ApiKey, "
                              + " Paymentsense_SoftwareHouseId=@Paymentsense_SoftwareHouseId, Paymentsense_InstallerId=@Paymentsense_InstallerId, "
                              + " BackupType=@BackupType, BackupStorageType=@BackupStorageType,PrintLogoInReceipt=@PrintLogoInReceipt, "
                              + " AutoGC=@AutoGC, SingleGC=@SingleGC,PrintDuplicateGiftCertSaleReceipt=@PrintDuplicateGiftCertSaleReceipt, "
                              + " SMPTServer=@SMPTServer, AppointmentEmailBody= @AppointmentEmailBody, ItemExpiryAlertDay=@ItemExpiryAlertDay";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@ItemExpiryAlertDay", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemExpiryAlertDay"].Value = intItemExpiryAlertDay;

                objSQlComm.Parameters.Add(new SqlParameter("@AppointmentEmailBody", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@AppointmentEmailBody"].Value = strAppointmentEmailBody;

                objSQlComm.Parameters.Add(new SqlParameter("@BackupType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BackupType"].Value = intBackupType;

                objSQlComm.Parameters.Add(new SqlParameter("@BackupStorageType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BackupStorageType"].Value = intBackupStorageType;

                objSQlComm.Parameters.Add(new SqlParameter("@EvoApiBaseAddress", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@EvoApiBaseAddress"].Value = strEvoApiBaseAddress;

                objSQlComm.Parameters.Add(new SqlParameter("@Use4Decimal", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerInfo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorUpdate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MonthsInvJrnl", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BlindCountPreview", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SalesByHour", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SalesByDept", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSAcceptCheck", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayChangeDue", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSIDRequired", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabelDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@QuantityRequired", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@UseInvJrnl", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftCertMaxChange", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@LayawayDepositPercent", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawaysDue", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawayReceipts", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptLayawayPolicy", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptHeader", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptFooter", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PoleScreen", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FormSkin", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayProductImage", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@POSMoreFunctionAlignment", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSRedirectToChangeDue", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SignInOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSShowGiftCertBalance", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ForcedLogin", System.Data.SqlDbType.Char));

                //objSQlComm.Parameters.Add(new SqlParameter("@POSCardPayment", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralReceiptPrint", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutExport", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CardPublisherName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CardPublisherEMail", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@AutoPO", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AutoCustomer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@AutoGC", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SingleGC", System.Data.SqlDbType.Char));

                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPMode", System.Data.SqlDbType.Int));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAccountID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAccountToken", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPApplicationID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@ElementHPAcceptorID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@IsDuplicateInvoice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptPrintOnReturn", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@DisplayLogoOrWeb", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayURL", System.Data.SqlDbType.NVarChar));

                //objSQlComm.Parameters.Add(new SqlParameter("@PaymentGateway", System.Data.SqlDbType.Int));
                //objSQlComm.Parameters.Add(new SqlParameter("@MercuryHPMerchantID", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@MercuryHPUserID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSRemotePrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PurgeCutOffDay", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@BarTenderLabelPrint", System.Data.SqlDbType.Char));


                objSQlComm.Parameters.Add(new SqlParameter("@UseCustomerNameInLabelPrint", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@PreprintedReceipt", System.Data.SqlDbType.Char));



                objSQlComm.Parameters.Add(new SqlParameter("@POSPrintInvoice", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@AcceptTips", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ShowTipsInReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowFoodStampTotal", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@EasyTendering", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowFeesInReceipt", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ReportEmail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REHost", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REPort", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@REUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RESSL", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@REFromAddress", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REFromName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@REReplyTo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowSaleSaveInReceipt", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LayawayPaymentOption", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LayawayPaymentOption"].Value = intLayawayPaymentOption;

                objSQlComm.Parameters.Add(new SqlParameter("@CalculatorStyleKeyboard", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CalculatorStyleKeyboard"].Value = strCalculatorStyleKeyboard;

                objSQlComm.Parameters.Add(new SqlParameter("@AllowTerminalCloseout", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AllowTerminalCloseout"].Value = strOtherTerminalCloseout;

                objSQlComm.Parameters.Add(new SqlParameter("@BottleRefund", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@BottleRefund"].Value = dblBottleRefund;

                objSQlComm.Parameters.Add(new SqlParameter("@AddGallon", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddGallon"].Value = strAddGallon;

                objSQlComm.Parameters.Add(new SqlParameter("@Print2TicketsForRepair", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Print2TicketsForRepair"].Value = strPrint2TicketsForRepair;

                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL3", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL4", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL5", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL6", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@NoSaleReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@HouseAccountBalanceInReceipt", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@NoSaleReceipt"].Value = strNoSaleReceipt;
                objSQlComm.Parameters["@HouseAccountBalanceInReceipt"].Value = strHouseAccountBalanceInReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@FocusOnEditProduct", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FocusOnEditProduct"].Value = strFocusOnEditProduct;

                objSQlComm.Parameters["@LinkGL1"].Value = intLinkGL1;
                objSQlComm.Parameters["@LinkGL2"].Value = intLinkGL2;
                objSQlComm.Parameters["@LinkGL3"].Value = intLinkGL3;
                objSQlComm.Parameters["@LinkGL4"].Value = intLinkGL4;
                objSQlComm.Parameters["@LinkGL5"].Value = intLinkGL5;
                objSQlComm.Parameters["@LinkGL6"].Value = intLinkGL6;

                objSQlComm.Parameters["@ShowSaleSaveInReceipt"].Value = strShowSaleSaveInReceipt;

                objSQlComm.Parameters["@REHost"].Value = strREHost;
                objSQlComm.Parameters["@REPort"].Value = intREPort;
                objSQlComm.Parameters["@REUser"].Value = strREUser;
                objSQlComm.Parameters["@REPassword"].Value = strREPassword;
                objSQlComm.Parameters["@RESSL"].Value = strRESSL;
                objSQlComm.Parameters["@REFromAddress"].Value = strREFromAddress;
                objSQlComm.Parameters["@REFromName"].Value = strREFromName;
                objSQlComm.Parameters["@REReplyTo"].Value = strREReplyTo;
                objSQlComm.Parameters["@ReportEmail"].Value = strReportEmail;

                objSQlComm.Parameters["@EasyTendering"].Value = strEasyTendering;
                objSQlComm.Parameters["@ShowFeesInReceipt"].Value = strShowFeesInReceipt;

                objSQlComm.Parameters["@AcceptTips"].Value = strAcceptTips;
                objSQlComm.Parameters["@ShowTipsInReceipt"].Value = strShowTipsInReceipt;
                objSQlComm.Parameters["@ShowFoodStampTotal"].Value = strShowFoodStampTotal;
                objSQlComm.Parameters["@Use4Decimal"].Value = strUse4Decimal;
                objSQlComm.Parameters["@DecimalPlace"].Value = intDecimalPlace;
                objSQlComm.Parameters["@CustomerInfo"].Value = intCustomerInfo;
                objSQlComm.Parameters["@VendorUpdate"].Value = intVendorUpdate;
                objSQlComm.Parameters["@CloseoutOption"].Value = intCloseoutOption;
                objSQlComm.Parameters["@MonthsInvJrnl"].Value = intMonthsInvJrnl;
                objSQlComm.Parameters["@BlindCountPreview"].Value = strBlindCountPreview;
                objSQlComm.Parameters["@SalesByHour"].Value = strSalesByHour;
                objSQlComm.Parameters["@SalesByDept"].Value = strSalesByDept;
                objSQlComm.Parameters["@POSAcceptCheck"].Value = strPOSAcceptCheck;
                objSQlComm.Parameters["@POSDisplayChangeDue"].Value = strPOSDisplayChangeDue;
                objSQlComm.Parameters["@POSIDRequired"].Value = strPOSIDRequired;
                objSQlComm.Parameters["@NoPriceOnLabelDefault"].Value = strNoPriceOnLabelDefault;
                objSQlComm.Parameters["@QuantityRequired"].Value = strQuantityRequired;
                objSQlComm.Parameters["@UseInvJrnl"].Value = strUseInvJrnl;
                objSQlComm.Parameters["@GiftCertMaxChange"].Value = dblGiftCertMaxChange;

                objSQlComm.Parameters["@LayawayDepositPercent"].Value = dblLayawayDepositPercent;
                objSQlComm.Parameters["@LayawaysDue"].Value = intLayawaysDue;
                objSQlComm.Parameters["@LayawayReceipts"].Value = intLayawayReceipts;
                objSQlComm.Parameters["@ReceiptLayawayPolicy"].Value = strReceiptLayawayPolicy;
                objSQlComm.Parameters["@ReceiptHeader"].Value = strReceiptHeader;
                objSQlComm.Parameters["@ReceiptFooter"].Value = strReceiptFooter;
                objSQlComm.Parameters["@PoleScreen"].Value = strPoleScreen;
                objSQlComm.Parameters["@FormSkin"].Value = strFormSkin;
                objSQlComm.Parameters["@POSDisplayProductImage"].Value = strPOSDisplayProductImage;
                objSQlComm.Parameters["@POSMoreFunctionAlignment"].Value = intPOSMoreFunctionAlignment;
                objSQlComm.Parameters["@POSRedirectToChangeDue"].Value = strPOSRedirectToChangeDue;
                objSQlComm.Parameters["@ForcedLogin"].Value = strForcedLogin;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@POSShowGiftCertBalance"].Value = strPOSShowGiftCertBalance;
                objSQlComm.Parameters["@SignInOption"].Value = intSignINOption;


                objSQlComm.Parameters["@GeneralReceiptPrint"].Value = strGeneralReceiptPrint;
                objSQlComm.Parameters["@CloseoutExport"].Value = strCloseoutExport;

                objSQlComm.Parameters["@CardPublisherName"].Value = strCardPublisherName;
                objSQlComm.Parameters["@CardPublisherEMail"].Value = strCardPublisherEMail;

                objSQlComm.Parameters["@AutoPO"].Value = strAutoPO;
                objSQlComm.Parameters["@AutoCustomer"].Value = strAutoCustomer;
                objSQlComm.Parameters["@AutoGC"].Value = strAutoGC;
                objSQlComm.Parameters["@SingleGC"].Value = strSingleGC;

                //objSQlComm.Parameters["@ElementHPMode"].Value = intElementHPMode;
                //objSQlComm.Parameters["@ElementHPAccountID"].Value = strElementHPAccountID;
                //objSQlComm.Parameters["@ElementHPAccountToken"].Value = strElementHPAccountToken;
                //objSQlComm.Parameters["@ElementHPApplicationID"].Value = strElementHPApplicationID;
                //objSQlComm.Parameters["@ElementHPAcceptorID"].Value = strElementHPAcceptorID;

                objSQlComm.Parameters["@IsDuplicateInvoice"].Value = strIsDuplicateInvoice;
                objSQlComm.Parameters["@ReceiptPrintOnReturn"].Value = strReceiptPrintOnReturn;

                objSQlComm.Parameters["@DisplayLogoOrWeb"].Value = intDisplayLogoOrWeb;
                objSQlComm.Parameters["@DisplayURL"].Value = strDisplayURL;

                //objSQlComm.Parameters["@PaymentGateway"].Value = intPaymentGateway;
                //objSQlComm.Parameters["@MercuryHPMerchantID"].Value = strMercuryHPMerchantID;
                //objSQlComm.Parameters["@MercuryHPUserID"].Value = strMercuryHPUserID;

                objSQlComm.Parameters["@POSRemotePrint"].Value = intPOSPrintTender;
                objSQlComm.Parameters["@PurgeCutOffDay"].Value = intPurgeCutOffDay;
                objSQlComm.Parameters["@BarTenderLabelPrint"].Value = strBarTenderLabelPrint;


                objSQlComm.Parameters["@UseCustomerNameInLabelPrint"].Value = strUseCustomerNameInLabelPrint;
                objSQlComm.Parameters["@PreprintedReceipt"].Value = strPreprintedReceipt;


                objSQlComm.Parameters["@POSPrintInvoice"].Value = intPOSPrintInvoice;

                objSQlComm.Parameters.Add(new SqlParameter("@MaxScaleWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@MaxScaleWeight"].Value = dblMaxScaleWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDevice", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ScaleDevice"].Value = strScaleDevice;

                objSQlComm.Parameters.Add(new SqlParameter("@NotReadScaleBarcodeCheckDigit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NotReadScaleBarcodeCheckDigit"].Value = strNotReadScaleBarcodeCheckDigit;

                objSQlComm.Parameters.Add(new SqlParameter("@DatalogicScanner8200", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@DatalogicScanner8200"].Value = strDatalogicScanner8200;

                objSQlComm.Parameters.Add(new SqlParameter("@NTEPCert", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@NTEPCert"].Value = strNTEPCert;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintTrainingMode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintTrainingMode"].Value = strPrintTrainingMode;


                objSQlComm.Parameters.Add(new SqlParameter("@EnableBooking", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@EnableBooking"].Value = strEnableBooking;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Scheduling_NoLimit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Scheduling_NoLimit"].Value = strBooking_Scheduling_NoLimit;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Mon_Check"].Value = strBooking_Mon_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Tue_Check"].Value = strBooking_Tue_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Wed_Check"].Value = strBooking_Wed_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Thu_Check"].Value = strBooking_Thu_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Fri_Check"].Value = strBooking_Fri_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Sat_Check"].Value = strBooking_Sat_Check;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_Check", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Booking_Sun_Check"].Value = strBooking_Sun_Check;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_CustomerSiteURL", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_CustomerSiteURL"].Value = strBooking_CustomerSiteURL;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_BusinessDetails", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_BusinessDetails"].Value = strBooking_BusinessDetails;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_PrivacyNotice", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_PrivacyNotice"].Value = strBooking_PrivacyNotice;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Phone"].Value = strBooking_Phone;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Email", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Email"].Value = strBooking_Email;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_LinkedIn_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_LinkedIn_Link"].Value = strBooking_LinkedIn_Link;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Twitter_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Twitter_Link"].Value = strBooking_Twitter_Link;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Facebook_Link", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Booking_Facebook_Link"].Value = strBooking_Facebook_Link;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Day", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Day"].Value = intBooking_Lead_Day;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Hour"].Value = intBooking_Lead_Hour;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Lead_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Lead_Minute"].Value = intBooking_Lead_Minute;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Slot_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Slot_Hour"].Value = intBooking_Slot_Hour;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Slot_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Slot_Minute"].Value = intBooking_Slot_Minute;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Mon_Start"].Value = intBooking_Mon_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Mon_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Mon_End"].Value = intBooking_Mon_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Tue_Start"].Value = intBooking_Tue_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Tue_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Tue_End"].Value = intBooking_Tue_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Wed_Start"].Value = intBooking_Wed_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Wed_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Wed_End"].Value = intBooking_Wed_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Thu_Start"].Value = intBooking_Thu_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Thu_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Thu_End"].Value = intBooking_Thu_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Fri_Start"].Value = intBooking_Fri_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Fri_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Fri_End"].Value = intBooking_Fri_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sat_Start"].Value = intBooking_Sat_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sat_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sat_End"].Value = intBooking_Sat_End;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_Start", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sun_Start"].Value = intBooking_Sun_Start;
                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Sun_End", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Sun_End"].Value = intBooking_Sun_End;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingServer", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingServer"].Value = strBookingServer;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDB", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDB"].Value = strBookingDB;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDBUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDBUser"].Value = strBookingDBUser;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingDBPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BookingDBPassword"].Value = strBookingDBPassword;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingScheduleWindowDay", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BookingScheduleWindowDay"].Value = intBookingScheduleWindowDay;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Reminder_Hour", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Reminder_Hour"].Value = intBooking_Reminder_Hour;

                objSQlComm.Parameters.Add(new SqlParameter("@Booking_Reminder_Minute", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Booking_Reminder_Minute"].Value = intBooking_Reminder_Minute;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintBlindDropCloseout", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintBlindDropCloseout"].Value = strPrintBlindDropCloseout;

                objSQlComm.Parameters.Add(new SqlParameter("@AllowNegativeStoreCredit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AllowNegativeStoreCredit"].Value = strAllowNegativeStoreCredit;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxInclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@TaxInclusive"].Value = strTaxInclusive;

                objSQlComm.Parameters.Add(new SqlParameter("@CountryName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CountryName"].Value = strCountryName;

                objSQlComm.Parameters.Add(new SqlParameter("@CurrencySymbol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CurrencySymbol"].Value = strCurrencySymbol;

                objSQlComm.Parameters.Add(new SqlParameter("@DateFormat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DateFormat"].Value = strDateFormat;

                objSQlComm.Parameters.Add(new SqlParameter("@SmallCurrencyName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SmallCurrencyName"].Value = strSmallCurrencyName;

                objSQlComm.Parameters.Add(new SqlParameter("@BigCurrencyName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BigCurrencyName"].Value = strBigCurrencyName;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin1Name"].Value = strCoin1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin2Name"].Value = strCoin2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin3Name"].Value = strCoin3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin4Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin4Name"].Value = strCoin4Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin5Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin5Name"].Value = strCoin5Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin6Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin6Name"].Value = strCoin6Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Coin7Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Coin7Name"].Value = strCoin7Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency1Name"].Value = strCurrency1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency2Name"].Value = strCurrency2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency3Name"].Value = strCurrency3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency4Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency4Name"].Value = strCurrency4Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency5Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency5Name"].Value = strCurrency5Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency6Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency6Name"].Value = strCurrency6Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency7Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency7Name"].Value = strCurrency7Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency8Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency8Name"].Value = strCurrency8Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency9Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency9Name"].Value = strCurrency9Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency10Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Currency10Name"].Value = strCurrency10Name;


                objSQlComm.Parameters.Add(new SqlParameter("@Currency1QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency1QuickTender"].Value = strCurrency1QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency2QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency2QuickTender"].Value = strCurrency2QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency3QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency3QuickTender"].Value = strCurrency3QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency4QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency4QuickTender"].Value = strCurrency4QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency5QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency5QuickTender"].Value = strCurrency5QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency6QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency6QuickTender"].Value = strCurrency6QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency7QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency7QuickTender"].Value = strCurrency7QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency8QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency8QuickTender"].Value = strCurrency8QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency9QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency9QuickTender"].Value = strCurrency9QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@Currency10QuickTender", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Currency10QuickTender"].Value = strCurrency10QuickTender;

                objSQlComm.Parameters.Add(new SqlParameter("@DefaultInternationalization", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@DefaultInternationalization"].Value = "N";

                objSQlComm.Parameters.Add(new SqlParameter("@UseTouchKeyboardInAdmin", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@UseTouchKeyboardInAdmin"].Value = strUseTouchKeyboardInAdmin;

                objSQlComm.Parameters.Add(new SqlParameter("@UseTouchKeyboardInPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@UseTouchKeyboardInPOS"].Value = strUseTouchKeyboardInPOS;


                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_AccountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_AccountName"].Value = strPaymentsense_AccountName;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_ApiKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_ApiKey"].Value = strPaymentsense_ApiKey;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_SoftwareHouseId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_SoftwareHouseId"].Value = strPaymentsense_SoftwareHouseId;

                objSQlComm.Parameters.Add(new SqlParameter("@Paymentsense_InstallerId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Paymentsense_InstallerId"].Value = strPaymentsense_InstallerId;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintLogoInReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintLogoInReceipt"].Value = strPrintLogoInReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintDuplicateGiftCertSaleReceipt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PrintDuplicateGiftCertSaleReceipt"].Value = strPrintDuplicateGiftCertSaleReceipt;

                objSQlComm.Parameters.Add(new SqlParameter("@SMPTServer", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SMPTServer"].Value = intSMTPServer;


                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                //objSQlComm.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion

        #region Insert Store Data

        public string InsertGSData(SqlCommand objSQlComm)
        {

            SqlDataReader objsqlReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into StoreInfo( Company,Address1,Address2,City,State, "
                                    + " PostalCode,Phone,Fax,EMail,WebAddress,StoreInfo,CompanyLogo, "
                                    + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                                    + " values ( @Company,@Address1,@Address2,@City,@State, "
                                    + " @PostalCode,@Phone,@Fax,@EMail,@WebAddress,@StoreInfo,@CompanyLogo, "
                                    + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                                    + " select @@IDENTITY as ID ";

            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WebAddress", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreInfo", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CompanyLogo", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@PostalCode"].Value = strPostalCode;
                objSQlComm.Parameters["@Phone"].Value = strPhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@WebAddress"].Value = strWebAddress;
                objSQlComm.Parameters["@StoreInfo"].Value = strStoreInfo;

                objSQlComm.Parameters["@CompanyLogo"].Value = bytLogo == null ? Convert.DBNull : bytLogo;


                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objsqlReader.Close();
                objsqlReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion

        #region Insert Central Export/Import Data

        public string InsertExpData(SqlCommand objSQlComm)
        {
            SqlDataReader objsqlReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " insert into CentralExportImport( StoreCode,StoreName,ExportFolderPath ) "
                       + " values ( @StoreCode,@StoreName,@ExportFolderPath ) "
                       + " select @@IDENTITY as ID ";

            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ExportFolderPath", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = strStoreCode;
                objSQlComm.Parameters["@StoreName"].Value = strStoreName;
                objSQlComm.Parameters["@ExportFolderPath"].Value = strExportFolderPath;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                }
                objsqlReader.Close();
                objsqlReader.Dispose();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();

                return strErrMsg;
            }
        }

        #endregion

        #region Update Store Data

        public string UpdateGSData(SqlCommand objSQlComm)
        {
            
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update StoreInfo set Company=@Company,Address1=@Address1,Address2=@Address2, "
                           + " City=@City,State=@State,PostalCode=@PostalCode, "
                           + " Phone=@Phone,Fax=@Fax,EMail=@EMail,WebAddress=@WebAddress,StoreInfo=@StoreInfo, "
                           + " CompanyLogo=@CompanyLogo,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WebAddress", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreInfo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CompanyLogo", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@PostalCode"].Value = strPostalCode;
                objSQlComm.Parameters["@Phone"].Value = strPhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@WebAddress"].Value = strWebAddress;
                objSQlComm.Parameters["@StoreInfo"].Value = strStoreInfo;

                objSQlComm.Parameters["@CompanyLogo"].Value = bytLogo == null ? Convert.DBNull : bytLogo;


                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion

        public string UpdateScaleLogo(SqlCommand objSQlComm)
        {
            byte[] Photo = GetPhoto(strPhotoFilePath1);
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";

            strSQLComm = " update StoreInfo set ScaleLogo = @ScaleLogo ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleLogo", System.Data.SqlDbType.Image));
                if (strPhotoFilePath1 == "")
                {
                    objSQlComm.Parameters["@ScaleLogo"].Value = Convert.DBNull;
                }
                else
                {
                    if (strPhotoFilePath1 == "null")
                    {
                        objSQlComm.Parameters["@ScaleLogo"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@ScaleLogo"].Value = Photo;
                    }
                    
                }

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #region Update Central Exp/Imp Data

        public string UpdateExpData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update CentralExportImport set StoreCode=@StoreCode,StoreName=@StoreName,ExportFolderPath=@ExportFolderPath ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ExportFolderPath", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = strStoreCode;
                objSQlComm.Parameters["@StoreName"].Value = strStoreName;
                objSQlComm.Parameters["@ExportFolderPath"].Value = strExportFolderPath;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion

        #endregion

        private void AdjustPOSFunctions(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colID = "";
                    string colDisplayOrder = "";
                    string colIsVisible = "";

                    if (dr["ID"].ToString() == "") continue;

                    colID = dr["ID"].ToString();
                    colDisplayOrder = dr["DisplayOrder"].ToString();
                    colIsVisible = dr["IsVisible"].ToString();

                    intPOSFunctionID = Functions.fnInt32(colID);
                    intPOSDisplayOrder = Functions.fnInt32(colDisplayOrder);
                    if (colIsVisible == "Yes") strPOSIsVisible = "Y";
                    else strPOSIsVisible = "N";

                    UpdatePOSFunctionButton(objSQlComm);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustScaleFunctions(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable1 == null) return;
                foreach (DataRow dr in dblSplitDataTable1.Rows)
                {
                    string colID = "";
                    string colDisplayOrder = "";
                    string colIsVisible = "";

                    if (dr["ID"].ToString() == "") continue;

                    colID = dr["ID"].ToString();
                    colDisplayOrder = dr["DisplayOrder"].ToString();
                    colIsVisible = dr["IsVisible"].ToString();

                    intPOSFunctionID = Functions.fnInt32(colID);
                    intPOSDisplayOrder = Functions.fnInt32(colDisplayOrder);
                    if (colIsVisible == "Yes") strPOSIsVisible = "Y";
                    else strPOSIsVisible = "N";

                    UpdateScaleFunctionButton(objSQlComm);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        #region Update POS Function Buttons

        public string UpdatePOSFunctionButton(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = "";
                if (blFunctionButtonAccess)
                {
                    strSQLComm = " Update POSFunctionSetup set DisplayOrder=@DisplayOrder,IsVisible=@IsVisible, "
                                        + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";
                }
                else
                {
                    strSQLComm = " Update POSFunctionSetup set DisplayOrder=@DisplayOrder,IsVisible=@IsVisible, "
                                        + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                                        + " ChangedByAdmin=@ChangedByAdmin, ChangedOnAdmin=@ChangedOnAdmin where ID = @ID ";
                }

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IsVisible", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intPOSFunctionID;
                objSQlComm.Parameters["@DisplayOrder"].Value = intPOSDisplayOrder;
                objSQlComm.Parameters["@IsVisible"].Value = strPOSIsVisible;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                if (!blFunctionButtonAccess)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@ChangedByAdmin", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters.Add(new SqlParameter("@ChangedOnAdmin", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@ChangedByAdmin"].Value = intChangedByAdmin;
                    objSQlComm.Parameters["@ChangedOnAdmin"].Value = DateTime.Now;
                }

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return strErrorMsg;
            }
        }

        public string UpdateScaleFunctionButton(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = "";
                strSQLComm = " Update POSFunctionSetup set DisplayOrder=@DisplayOrder,IsVisible=@IsVisible, "
                                        + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IsVisible", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intPOSFunctionID;
                objSQlComm.Parameters["@DisplayOrder"].Value = intPOSDisplayOrder;
                objSQlComm.Parameters["@IsVisible"].Value = strPOSIsVisible;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return strErrorMsg;
            }
        }

        #endregion

        public string PostPOSFunctionUpdate()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {
                AdjustPOSFunctions(SaveComm);
                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }

        }
    
        public string PostSetupData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {
                if (intOtherID == 0)
                {
                    strError = InsertGSData1(SaveComm);
                }
                else
                {
                    strError = UpdateGSData1(SaveComm);
                }

                if (intStoreID == 0)
                {
                    strError = InsertGSData(SaveComm);
                }
                else
                {
                    strError = UpdateGSData(SaveComm);
                }

                intShopifyRecordCount = ShopifyRecordCount(SaveComm);

                if (intShopifyRecordCount == 0)
                {
                    strError = InsertShopifyInformation(SaveComm);
                }
                else
                {
                    strError = UpdateShopifyInformation(SaveComm);
                }

                intQuickBooksRecordCount = QBInfoRecordCount(SaveComm);

                if (intQuickBooksRecordCount == 0)
                {
                    strError = InsertQBInfo(SaveComm);
                }
                else
                {
                    strError = UpdateQBInfo(SaveComm);
                }

                intXERORecordCount = XEROInfoRecordCount(SaveComm);
                if (intXERORecordCount == 0)
                {
                    strError = InsertXEROInfo(SaveComm);
                }
                else
                {
                    strError = UpdateXEROInfo(SaveComm);
                }

                if (blPOSFunctionUpdate)
                    AdjustPOSFunctions(SaveComm);

                

                DeleteLinkPrinters(SaveComm);
                AdjustLinkPrinters(SaveComm);


                DeleteLinkTemplates(SaveComm);
                AdjustLinkTemplates(SaveComm);

                DeleteDefaultService(SaveComm);
                InsertDefaultService(SaveComm);

                DeleteSalesService(SaveComm);
                DeleteRentService(SaveComm);
                DeleteRepairService(SaveComm);

                InsertSalesService(SaveComm);
                InsertRentService(SaveComm);
                InsertRepairService(SaveComm);

                DeleteScaleGraduationData(SaveComm);
                InsertScaleGraduationData(SaveComm);

                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        public string InsertDefaultService(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = "Default Service";
                objSQlComm.Parameters["@ParamValue"].Value = strPOSDefaultService;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string InsertSalesService(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = "Sales";
                objSQlComm.Parameters["@ParamValue"].Value = strSalesService;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string InsertRentService(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = "Rent";
                objSQlComm.Parameters["@ParamValue"].Value = strRentService;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLReader.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                objSQlComm.Dispose();
                objSQLReader.Close();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string InsertRepairService(SqlCommand objSQlComm)
        {
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into LocalSetup( HostName,ParamName,ParamValue,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                       + " select @@IDENTITY as ID ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = "Repair";
                objSQlComm.Parameters["@ParamValue"].Value = strRepairService;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLReader.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                objSQlComm.Dispose();
                objSQLReader.Close();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public bool DeleteDefaultService(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from localsetup where hostname = @hostname and paramname = 'Default Service' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool DeleteSalesService(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from localsetup where hostname = @hostname and paramname = 'Sales' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool DeleteRentService(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from localsetup Where hostname = @hostname and paramname = 'Rent' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool DeleteRepairService(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from localsetup Where hostname = @hostname and paramname = 'Repair' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        #region Update Use Price Level (POS)
        public string UpdateUsePriceLevel()
        {
            string strSQLComm = "";
            if (blFunctionButtonAccess)
            {
                strSQLComm = " update Setup set UsePriceLevel=@UsePriceLevel,PriceLevelApplicableDate=@PriceLevelApplicableDate, "
                                    + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            }
            else
            {
                strSQLComm = " update Setup set UsePriceLevel=@UsePriceLevel,PriceLevelApplicableDate=@PriceLevelApplicableDate, "
                                    + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                                    + " ChangedByAdmin=@ChangedByAdmin, ChangedOnAdmin=@ChangedOnAdmin ";
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQlComm.Parameters.Add(new SqlParameter("@UsePriceLevel", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceLevelApplicableDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@UsePriceLevel"].Value = intUsePriceLevel;
                objSQlComm.Parameters["@PriceLevelApplicableDate"].Value = DateTime.Today;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;


                if (!blFunctionButtonAccess)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@ChangedByAdmin", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters.Add(new SqlParameter("@ChangedOnAdmin", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@ChangedByAdmin"].Value = intChangedByAdmin;
                    objSQlComm.Parameters["@ChangedOnAdmin"].Value = DateTime.Now;
                }
                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }
        #endregion

        #region Data Purging

        public int PurgeData()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_purged";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;
                objSQlComm.CommandTimeout = 5000;
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
        #endregion

        #region Initialize Transaction

        public int InitializeTran()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_initializereceipts";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value);
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

        #endregion

        #region Change Terminal
        public void ChangeTerminal(string OldTerminal, string NewTerminal)
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ChangeTerminalName";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@OldName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@OldName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OldName"].Value = OldTerminal;

                objSQlComm.Parameters.Add(new SqlParameter("@NewName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@NewName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@NewName"].Value = NewTerminal;

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

        public int AdjustPrice( int intitemmod, int catgrpval, int prinde, int prtype, double prval,
                                string chkprall, string chkprA, string chkprB, string chkprC, 
                                string chknonzero)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_AdjustPrice";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@Option2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Option2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option2"].Value = intitemmod;

                objSQlComm.Parameters.Add(new SqlParameter("@Option21", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Option21"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option21"].Value = catgrpval;

                objSQlComm.Parameters.Add(new SqlParameter("@Option3", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Option3"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option3"].Value = prinde;

                objSQlComm.Parameters.Add(new SqlParameter("@Option4", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Option4"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option4"].Value = prtype;

                objSQlComm.Parameters.Add(new SqlParameter("@Option41", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Option41"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option41"].Value = prval;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckALL", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckALL"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CheckALL"].Value = chkprall;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckPrA", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckPrA"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CheckPrA"].Value = chkprA;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckPrB", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckPrB"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CheckPrB"].Value = chkprB;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckPrC", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckPrC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CheckPrC"].Value = chkprC;


                objSQlComm.Parameters.Add(new SqlParameter("@CheckNonZero", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckNonZero"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CheckNonZero"].Value = chknonzero;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return 0;
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


        public bool AdjustWebHistoryData(DataTable dtbl, string Terml)
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                DeleteWebHistoryData(SaveComm, Terml);
                AdjustWebHtry(SaveComm, dtbl, Terml);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteWebHistoryData(SqlCommand objSQlComm,string Terml)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from WebHistory where TerminalName = @Terml";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@Terml", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terml"].Value = Terml;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        private void AdjustWebHtry(SqlCommand objSQlComm, DataTable dtbl, string Terml)
        {
            try
            {
                if (dtbl == null) return;
                int cnt = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    string colID = "";
                    string colURL = "";

                    if (dr["SLNO"].ToString() == "") continue;

                    colID = dr["SLNO"].ToString();
                    colURL = dr["URL"].ToString();
                    cnt++;
                    if (cnt > 20) break;
                    InsertWebHistoryData(objSQlComm, Functions.fnInt32(colID), colURL, Terml);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertWebHistoryData(SqlCommand objSQlComm,int pslno,string purl, string pterminal)
        {
            string strSQLComm = "";
            objSQlComm.Parameters.Clear();
            try
            {
                strSQLComm = " insert into WebHistory (slno,url,terminalname) values(@slno,@url,@terminalname)";
                           
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@slno", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@url", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@terminalname", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@slno"].Value = pslno;
                objSQlComm.Parameters["@url"].Value = purl;
                objSQlComm.Parameters["@terminalname"].Value = pterminal;
                objSQlComm.ExecuteNonQuery();
                
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public DataTable FetchWebHistoryData(string pterminal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from webhistory where terminalname = @trrml order by slno ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@trrml", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@trrml"].Value = pterminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SLNO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("URL", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["SLNO"].ToString(),objSQLReader["URL"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {

                strErrorMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public int DuplicatePrinterCount(string DeptID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Printers where PrinterName=@DEPARTMENTID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@DEPARTMENTID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DEPARTMENTID"].Value = DeptID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["RECCOUNT"].ToString());
                    }
                    catch { objSQLReader.Close(); }
                    
                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable ShowPrinterRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from Printers where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {    objSQLReader["ID"].ToString(),
												    objSQLReader["PrinterName"].ToString()
                                                    });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchPrinterData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,PrinterName from Printers order by PrinterName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkPrinter", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkTemplate", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["PrinterName"].ToString(),strDataObjectCulture_None,"" });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchLookupPrinterData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,PrinterName from Printers order by PrinterName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkPrinter", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["PrinterName"].ToString(),"" });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_None, "" });
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string GetAttachedItemPrinter(string pParamName, string term)
        {
            string strprinter = "";
            string strSQLComm = " select ParamValue from LocalSetup where HostName = @HostName and ParamName=@ParamName ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@HostName"].Value = term;
                objSQlComm.Parameters["@ParamName"].Value = pParamName;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    try
                    {
                        strprinter = objSQLReader["ParamValue"].ToString();
                    }
                    catch
                    { }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strprinter;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return "";
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable FetchLinkPrinterData(string terml)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,ParamName,ParamValue from LocalSetup where HostName = @HostName and ParamName like 'All Printers%' order by ParamName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@HostName"].Value = terml;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkPrinter", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["ParamName"].ToString(),
                                                   objSQLReader["ParamValue"].ToString() });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }


        public DataTable FetchLinkTemplateData(string terml)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,ParamName,ParamValue from LocalSetup where HostName = @HostName and ParamName like 'All Templates%' order by ParamName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@HostName"].Value = terml;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkTemplate", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ParamName"].ToString(),
                                                   objSQLReader["ParamValue"].ToString() });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string FetchLinkTemplateName(int rID)
        {
            string val = "";

            string strSQLComm = " select TemplateName from ReceiptTemplateMaster where TemplateType = 'Item' and ID = " + rID;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
           
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                
                while (objSQLReader.Read())
                {
                    val = objSQLReader["TemplateName"].ToString();
                  
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return "";
            }
        }

        public int DeletePrinter(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deleteprinter";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ID"].Value = DeleteID;

                objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value.ToString());
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

        public string GetPrinterName(int PrintID)
        {
            string strprinter = "";
            string strSQLComm = " select isnull(PrinterName,'') as PrinterName from Printers where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = PrintID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    try
                    {
                        strprinter = objSQLReader["PrinterName"].ToString();
                    }
                    catch
                    { }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strprinter;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return "";
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public bool DeleteLinkTemplates(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from localsetup where hostname = @hostname and paramname like  'All Templates%' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool DeleteLinkPrinters(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from localsetup where hostname = @hostname and paramname like  'All Printers%' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@hostname", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@hostname"].Value = strHostName;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public string InsertPrinterData()
        {
            string strSQLComm = " insert into Printers( PrinterName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @PrinterName,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@PrinterName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@PrinterName"].Value = strPrinterName;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

   

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdatePrinterData()
        {
            string strSQLComm = " update Printers set PrinterName=@PrinterName,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn "
                              + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@PrinterName"].Value = strPrinterName;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                
                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string GetPrinterDesc(int TaxID)
        {
            string strresult = "";
            string strSQLComm = " select PrinterName from Printers where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = TaxID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = objSQLReader["PrinterName"].ToString();
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strresult;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return "";
            }
        }

        public string UpdateGiftCertData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update giftcert set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = strStoreCode;
                objSQlComm.Parameters["@StoreCode2"].Value = strStoreCode;
                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateCustomerData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update customer set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = strStoreCode;
                objSQlComm.Parameters["@StoreCode2"].Value = strStoreCode;
                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateCustomerAcRecvData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update acctrecv set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = strStoreCode;
                objSQlComm.Parameters["@StoreCode2"].Value = strStoreCode;
                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateGeneralMappingData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update generalmapping set IssueStore=@StoreCode1 where (IssueStore = '' or IssueStore is null)  ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode1"].Value = strStoreCode;
                
                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateEmployeeData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update employee set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where (IssueStore = '' or IssueStore is null) ";

            objSQlComm.CommandText = strSQLComm;
            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode1"].Value = strStoreCode;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode2"].Value = strStoreCode;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public DataTable ViewLog(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();

            string strSQLComm = "";
            strSQLComm = " Select UserName,LogTime,LogTerminal,LogEvent,EventStatus,EventIdentity "
                        + " from applicationlogs where LogTime between @SDT and @FDT "
                        + " order by LogTime desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }



                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("UserName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupDate", System.Type.GetType("System.DateTime"));
                dtbl.Columns.Add("LogDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LogTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LogTerminal", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LogEvent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EventStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EventIdentity", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate = "";
                    string strTime = "";

                    strDate = Functions.fnDate(objSQLReader["LogTime"].ToString()).ToString("d MMM yyyy");
                    strTime = Functions.fnDate(objSQLReader["LogTime"].ToString()).ToString("hh:mm:ss tt");

                    dtbl.Rows.Add(new object[] {objSQLReader["UserName"].ToString(),
                                                Functions.fnDate(Functions.fnDate(objSQLReader["LogTime"].ToString()).ToString("d")),
                                                strDate,
                                                strTime,
                                                objSQLReader["LogTerminal"].ToString(),
                                                objSQLReader["LogEvent"].ToString(),
                                                objSQLReader["EventStatus"].ToString(),
                                                objSQLReader["EventIdentity"].ToString()});
                }
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        #region COM Printer Command Setup

        public string InsertCOMPrinterCommand()
        {
            string strSQLComm = " insert into PortCommand (FormatNo,FormatName,PortName,PrinterCommand,Qty,WordWrap,FontType,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values (@FormatNo,@FormatName,@PortName,@PrinterCommand,@Qty,@WordWrap,@FontType,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatNo", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FormatName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PortName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterCommand", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@WordWrap", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FormatNo"].Value = strCOMFormatNo;
                objSQlComm.Parameters["@FormatName"].Value = strCOMFormatName;
                objSQlComm.Parameters["@PortName"].Value = strCOMPortName;
                objSQlComm.Parameters["@PrinterCommand"].Value = strCOMPrinterCommand;
                objSQlComm.Parameters["@Qty"].Value = intCOMQty;
                objSQlComm.Parameters["@WordWrap"].Value = intCOMWrap;
                objSQlComm.Parameters["@FontType"].Value = strCOMFont;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdateCOMPrinterCommand()
        {
            string strSQLComm = " update PortCommand set FormatNo=@FormatNo,FormatName=@FormatName,PortName=@PortName,PrinterCommand=@PrinterCommand,"
                              + " Qty=@Qty,WordWrap=@WordWrap,FontType=@FontType,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FormatNo", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FormatName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PortName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterCommand", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@WordWrap", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@FormatNo"].Value = strCOMFormatNo;
                objSQlComm.Parameters["@FormatName"].Value = strCOMFormatName;
                objSQlComm.Parameters["@PortName"].Value = strCOMPortName;
                objSQlComm.Parameters["@PrinterCommand"].Value = strCOMPrinterCommand;
                objSQlComm.Parameters["@Qty"].Value = intCOMQty;
                objSQlComm.Parameters["@WordWrap"].Value = intCOMWrap;
                objSQlComm.Parameters["@FontType"].Value = strCOMFont;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int DeleteCOMPrinterCommand(int DeleteID)
        {
            string strSQLComm = " delete from PortCommand where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = DeleteID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return -1;
            }
        }

        public DataTable ShowCOMPrinterCommand(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from PortCommand where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PortName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterCommand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WordWrap", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FontType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
												objSQLReader["FormatNo"].ToString(),
                                                objSQLReader["FormatName"].ToString(),
                                                objSQLReader["PortName"].ToString(),
                                                objSQLReader["PrinterCommand"].ToString(),
                                                objSQLReader["Qty"].ToString(),
                                                objSQLReader["WordWrap"].ToString(),
                                                objSQLReader["FontType"].ToString()
                                                });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchCOMPrinterCommand()
        {
            DataTable dtbl = new DataTable();


            string strSQLComm = " select ID,FormatNo,FormatName,PortName,PrinterCommand from PortCommand order by FormatNo,PortName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PortName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterCommand", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["FormatNo"].ToString(),
                                                    objSQLReader["FormatName"].ToString(),
                                                    objSQLReader["PortName"].ToString(),
                                                    objSQLReader["PrinterCommand"].ToString() });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchCOMPrinterLookup()
        {
            DataTable dtbl = new DataTable();


            string strSQLComm = " select ID,FormatNo,FormatName,PortName from PortCommand order by FormatNo,PortName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Printer", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["FormatNo"].ToString() + (objSQLReader["FormatName"].ToString() != "" ? " - " + objSQLReader["FormatName"].ToString() : "" )+ 
                                                       " ( " + objSQLReader["PortName"].ToString() + " ) " });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }
        
        public int DuplicateCOMFormatNo(string prmval)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from PortCommand where FormatNo=@prm ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@prm"].Value = prmval;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int DuplicateCOMFormatName(string prmval)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from PortCommand where FormatName=@prm ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@prm"].Value = prmval;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable GetCOMPrinterVendorData(string prm)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(v.VendorID,'') as VID, isnull(v.Name,'') as VNAME, isnull(vp.PartNumber,'') as VPART "
                              + " from product p left outer join vendpart vp on vp.ProductID = p.ID "
                              + " left outer join vendor v on vp.vendorid = v.id "
                              + " where p.ID in (select ID from product where sku = @p) and vp.isprimary = 'Y'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p"].Value = prm;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("VID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VPART", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["VID"].ToString(),
												objSQLReader["VNAME"].ToString(),
                                                objSQLReader["VPART"].ToString()
                                                });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable GetCOMPrinterScaleData(string prm)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(sc.SCALE_DESCRIPTION_1,'') as D1, isnull(sc.SCALE_DESCRIPTION_2,'') as D2, "
                              + " isnull(sc.INGRED_STATEMENT,'') as ING,isnull(sc.SPECIAL_MESSAGE,'') as SPL, "
                              + " isnull(sc.BYCOUNT,0) as BC, isnull(sc.WEIGHT,0.00) as WHT from scale_product sc left outer join product p on  "
                              + " sc.ProductID = p.ID where p.ID in (select ID from product where sku = @p ) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p"].Value = prm;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("LD", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LD2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("INGD", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPLM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WHT", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["D1"].ToString(),
												objSQLReader["D2"].ToString(),
                                                objSQLReader["ING"].ToString(),
                                                objSQLReader["SPL"].ToString(),
                                                objSQLReader["BC"].ToString(),
                                                objSQLReader["WHT"].ToString()
                                                });
                    break;
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        #endregion

        #region reset Item Display

        public int ResetItemOrdering()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ResetItemDisplayOrder";

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

        public int ResetItemCategoryOrdering()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ResetItemCategoryOrder";

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

        public int ResetScaleCategoryOrdering()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ResetScaleCategoryOrder";

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

        public int ResetTenderOrdering()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_ResetTenderOrder";

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

        #endregion


        public bool IsExistStoreCode()
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from CentralExportImport ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcnt"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount > 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }

        public string InsertStoreOnFirstTimeWebExport(string pscode,string psname)
        {
            string strSQLComm = " insert into CentralExportImport( StoreCode, StoreName ) "
                              + " values ( @StoreCode, @StoreName) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = pscode;
                objSQlComm.Parameters["@StoreName"].Value = psname;

                objSQlComm.ExecuteNonQuery();
                
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateGiftCertOnFirstTimeWebExport(string pscode)
        {
            string strSQLComm = " update giftcert set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = pscode;
                objSQlComm.Parameters["@StoreCode2"].Value = pscode;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCustomerOnFirstTimeWebExport(string pscode)
        {
            string strSQLComm = " update customer set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = pscode;
                objSQlComm.Parameters["@StoreCode2"].Value = pscode;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCustomerAcRecvOnFirstTimeWebExport(string pscode)
        {
            string strSQLComm = " update acctrecv set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = pscode;
                objSQlComm.Parameters["@StoreCode2"].Value = pscode;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateGeneralMappingOnFirstTimeWebExport(string pscode)
        {
            string strSQLComm = " update generalmapping set IssueStore=@StoreCode1 where (IssueStore = '' or IssueStore is null)  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode1"].Value = pscode;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateEmployeeOnFirstTimeWebExport(string pscode)
        {
            string strSQLComm = " update employee set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where (IssueStore = '' or IssueStore is null) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = pscode;
                objSQlComm.Parameters["@StoreCode2"].Value = pscode;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public bool IsExistsImportedPrecidiaGiftCard(string pGC)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from GiftCert where GiftCertID = @prm ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@prm"].Value = pGC;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcnt"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount > 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }

        public int GetImportedPrecidiaGiftCardRecordCount(string pGC)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from GiftCert where GiftCertID = @prm ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@prm"].Value = pGC;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcnt"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public string InsertImportedPrecidiaGiftCard(string pGC, double pAmt, string iStore, string oStore)
        {
            string strSQLComm = " insert into GiftCert( GiftCertID, Amount, IssueStore, OperateStore, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                              + " values ( @GiftCertID, @Amount, @IssueStore, @OperateStore, @CreatedBy, getdate(), @LastChangedBy, getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@GiftCertID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@GiftCertID"].Value = pGC;
                objSQlComm.Parameters["@Amount"].Value = pAmt;
                objSQlComm.Parameters["@IssueStore"].Value = iStore;
                objSQlComm.Parameters["@OperateStore"].Value = oStore;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateImportedPrecidiaGiftCard(string pGC, double pAmt)
        {
            string strSQLComm = " update GiftCert set Amount = @Amount,LastChangedBy = @LastChangedBy,   LastChangedOn = getdate() where GiftCertID = @GiftCertID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@GiftCertID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@GiftCertID"].Value = pGC;
                objSQlComm.Parameters["@Amount"].Value = pAmt;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        public DataTable GetScaleGraduationData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from ScaleGraduation1 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ScaleType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Graduation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Range1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Range2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Graduation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("D_Range1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("D_Range2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Check2Digit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("D_Check2Digit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("D_Graduation", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ScaleType"].ToString(),
                                                   objSQLReader["Graduation"].ToString(),
                                                   objSQLReader["S_Range1"].ToString(),
                                                   objSQLReader["S_Range2"].ToString(),
                                                   objSQLReader["S_Graduation"].ToString(),
                                                   objSQLReader["D_Range1"].ToString(),
                                                   objSQLReader["D_Range2"].ToString(),
                                                   objSQLReader["S_Check2Digit"].ToString(),
                                                   objSQLReader["D_Check2Digit"].ToString(),
                                                   objSQLReader["D_Graduation"].ToString()
                                               });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public bool DeleteScaleGraduationData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from ScaleGraduation1 ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public string InsertScaleGraduationData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " insert into ScaleGraduation1( Graduation, ScaleType, S_Range1, S_Range2, S_Graduation, D_Range1, D_Range2, D_Graduation, S_Check2Digit,D_Check2Digit ) "
                       + " values ( @Graduation, @ScaleType, @S_Range1, @S_Range2, @S_Graduation, @D_Range1, @D_Range2, @D_Graduation,  @S_Check2Digit, @D_Check2Digit ) ";

            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Graduation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@S_Range1", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@S_Range2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@S_Graduation", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@D_Range1", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@D_Range2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@S_Check2Digit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@D_Check2Digit", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@D_Graduation", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@Graduation"].Value = strGraduation;
                objSQlComm.Parameters["@ScaleType"].Value = strScaleType;
                objSQlComm.Parameters["@S_Range1"].Value = dblS_Range1;
                objSQlComm.Parameters["@S_Range2"].Value = dblS_Range2;
                objSQlComm.Parameters["@S_Graduation"].Value = dblS_Graduation;
                objSQlComm.Parameters["@D_Range1"].Value = dblD_Range1;
                objSQlComm.Parameters["@D_Range2"].Value = dblD_Range2;
                objSQlComm.Parameters["@S_Check2Digit"].Value = strS_Check2Digit;
                objSQlComm.Parameters["@D_Check2Digit"].Value = strD_Check2Digit;
                objSQlComm.Parameters["@D_Graduation"].Value = dblD_Graduation;

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                return strErrMsg;
            }
        }

        public string UpdateCheckSmartGrocer()
        {
            string strSQLComm = " update Setup set CheckSmartGrocer = @CheckSmartGrocer, SmartGrocerStore = @SmartGrocerStore ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckSmartGrocer", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckSmartGrocer"].Value = strCheckSmartGrocer;

                objSQlComm.Parameters.Add(new SqlParameter("@SmartGrocerStore", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SmartGrocerStore"].Value = intSmartGrocerStore;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCheckPriceSmart()
        {
            string strSQLComm = " update Setup set CheckPriceSmart = @CheckPriceSmart, PriceSmartDirectoryPath = @PriceSmartDirectoryPath, "
                              + " PriceSmartItemFile=@PriceSmartItemFile,PriceSmartIngredientFile=@PriceSmartIngredientFile,"
                              + " PriceSmartScaleCat=@PriceSmartScaleCat,PriceSmartDeptID=@PriceSmartDeptID,PriceSmartCatID=@PriceSmartCatID,"
                              + " PriceSmartScaleLabel=@PriceSmartScaleLabel,PriceSmartFtpHost=@PriceSmartFtpHost,PriceSmartFtpUser=@PriceSmartFtpUser,"
                              + " PriceSmartFtpPassword=@PriceSmartFtpPassword, PriceSmartFtpFolder=@PriceSmartFtpFolder,PriceSmartLogClearDays = @PriceSmartLogClearDays ";


            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CheckPriceSmart", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CheckPriceSmart"].Value = strCheckPriceSmart;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartDirectoryPath", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartDirectoryPath"].Value = strPriceSmartDirectoryPath;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartItemFile", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartItemFile"].Value = strPriceSmartItemFile;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartIngredientFile", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartIngredientFile"].Value = strPriceSmartIngredientFile;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartScaleCat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartScaleCat"].Value = strPriceSmartScaleCat;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartDeptID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PriceSmartDeptID"].Value = intPriceSmartDeptID;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartCatID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PriceSmartCatID"].Value = intPriceSmartCatID;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartScaleLabel", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PriceSmartScaleLabel"].Value = intPriceSmartScaleLabel;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartFtpHost", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartFtpHost"].Value = strPriceSmartFtpHost;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartFtpUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartFtpUser"].Value = strPriceSmartFtpUser;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartFtpPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartFtpPassword"].Value = strPriceSmartFtpPassword;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartFtpFolder", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PriceSmartFtpFolder"].Value = strPriceSmartFtpFolder;

                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartLogClearDays", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PriceSmartLogClearDays"].Value = intPriceSmartLogClearDays;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        public string SaveDutchValleySetup()
        {
            string strSQLComm = " update Setup set DutchValleyVendorUser = @DutchValleyVendorUser, DutchValleyVendorPassword = @DutchValleyVendorPassword, "
                              + " DutchValleyClientUser=@DutchValleyClientUser,DutchValleyClientPassword=@DutchValleyClientPassword,"
                              + " DutchValleyDeptID=@DutchValleyDeptID,DutchValleyScaleCat=@DutchValleyScaleCat,DutchValleyScaleLabel=@DutchValleyScaleLabel,"
                              + " DutchValleyFamily=@DutchValleyFamily, DutchValleyVendor = @DutchValleyVendor ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyVendorUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DutchValleyVendorUser"].Value = strDutchValleyVendorUser;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyVendorPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DutchValleyVendorPassword"].Value = strDutchValleyVendorPassword;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyClientUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DutchValleyClientUser"].Value = strDutchValleyClientUser;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyClientPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DutchValleyClientPassword"].Value = strDutchValleyClientPassword;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyDeptID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DutchValleyDeptID"].Value = intDutchValleyDeptID;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyScaleCat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DutchValleyScaleCat"].Value = strDutchValleyScaleCat;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyScaleLabel", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DutchValleyScaleLabel"].Value = intDutchValleyScaleLabel;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyFamily", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DutchValleyFamily"].Value = intDutchValleyFamily;

                objSQlComm.Parameters.Add(new SqlParameter("@DutchValleyVendor", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DutchValleyVendor"].Value = intDutchValleyVendor;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        #region Report Schedule Entry

        public bool IsExistScheduledReport(string Rtag, string RType)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from reportentry where ReportTag = @T and ReportType = @TY ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@T"].Value = Rtag;
                objSQlComm.Parameters.Add(new SqlParameter("@TY", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@TY"].Value = RType;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcnt"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount > 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }

        public int GetScheduledReportCount(string Rtag, string RType)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(max(ReportCount),0) + 1 as rcnt from reportentry where ReportTag = @T and ReportType = @TY ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@T"].Value = Rtag;
                objSQlComm.Parameters.Add(new SqlParameter("@TY", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@TY"].Value = RType;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcnt"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public DataTable FetchReportEntries()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from ReportEntry order by ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportTag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateRange", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_1_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_2_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_3_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportTitle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportCount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ReportTag"].ToString(),
                                                   objSQLReader["ReportName"].ToString(),
                                                   objSQLReader["DateRange"].ToString(),
                                                   objSQLReader["ReportType"].ToString(),
                                                   objSQLReader["ReportSortOption_1"].ToString(),
                                                   objSQLReader["ReportSortOption_1_Type"].ToString(),
                                                   objSQLReader["ReportSortOption_2"].ToString(),
                                                   objSQLReader["ReportSortOption_2_Type"].ToString(),
                                                   objSQLReader["ReportSortOption_3"].ToString(),
                                                   objSQLReader["ReportSortOption_3_Type"].ToString(),
                                                   objSQLReader["ReportTitle"].ToString(),
                                                   objSQLReader["ReportCount"].ToString()
                                               });
                }

                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable GetReportEntryRecord(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from ReportEntry where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportTag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateRange", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_1_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_2_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReportSortOption_3_Type", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ReportTag"].ToString(),
                                                   objSQLReader["ReportName"].ToString(),
                                                   objSQLReader["DateRange"].ToString(),
                                                   objSQLReader["ReportType"].ToString(),
                                                   objSQLReader["ReportSortOption_1"].ToString(),
                                                   objSQLReader["ReportSortOption_1_Type"].ToString(),
                                                   objSQLReader["ReportSortOption_2"].ToString(),
                                                   objSQLReader["ReportSortOption_2_Type"].ToString(),
                                                   objSQLReader["ReportSortOption_3"].ToString(),
                                                   objSQLReader["ReportSortOption_3_Type"].ToString()
                                               });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string DeleteReportEntryRecord(int DeleteID)
        {
            string strSQLComm = " Delete from ReportEntry Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = DeleteID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string InsertSimpleReportEntry(string repName, string repTag, string repDate, string repTitle, int repCount)
        {
            string strSQLComm = " insert into ReportEntry ( ReportName, ReportTag, ReportType, DateRange, ReportTitle, ReportCount, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                              + " values ( @ReportName, @ReportTag, @ReportType, @DateRange,  @ReportTitle, @ReportCount, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ReportName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportTag", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DateRange", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportTitle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ReportName"].Value = repName;
                objSQlComm.Parameters["@ReportTag"].Value = repTag;
                objSQlComm.Parameters["@ReportType"].Value = "";
                objSQlComm.Parameters["@DateRange"].Value = repDate;
                objSQlComm.Parameters["@ReportTitle"].Value = repTitle;
                objSQlComm.Parameters["@ReportCount"].Value = repCount;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }

                    objSQLReader.Close();

                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string InsertComplexReportEntry(string repName, string repTag, string repDate, string repType, string sortOp_1, string sortOp_1_ty,
            string sortOp_2, string sortOp_2_ty, string sortOp_3, string sortOp_3_ty, string repTitle, int repCount)
        {
            string strSQLComm = " insert into ReportEntry ( ReportName, ReportTag, DateRange, ReportType, "
                              + " ReportSortOption_1, ReportSortOption_1_Type, ReportSortOption_2, ReportSortOption_2_Type, ReportSortOption_3, ReportSortOption_3_Type, "
                              + " ReportTitle, ReportCount, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                              + " values ( @ReportName, @ReportTag, @DateRange, @ReportType, "
                              + " @ReportSortOption_1, @ReportSortOption_1_Type, @ReportSortOption_2, @ReportSortOption_2_Type, @ReportSortOption_3, @ReportSortOption_3_Type, "
                              + " @ReportTitle, @ReportCount, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ReportName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportTag", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DateRange", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_1_Type", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_2", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_2_Type", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_3", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_3_Type", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportTitle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportCount", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ReportName"].Value = repName;
                objSQlComm.Parameters["@ReportTag"].Value = repTag;
                objSQlComm.Parameters["@DateRange"].Value = repDate;
                objSQlComm.Parameters["@ReportType"].Value = repType;

                objSQlComm.Parameters["@ReportSortOption_1"].Value = sortOp_1;
                objSQlComm.Parameters["@ReportSortOption_1_Type"].Value = sortOp_1_ty;

                objSQlComm.Parameters["@ReportSortOption_2"].Value = sortOp_2;
                objSQlComm.Parameters["@ReportSortOption_2_Type"].Value = sortOp_2_ty;

                objSQlComm.Parameters["@ReportSortOption_3"].Value = sortOp_3;
                objSQlComm.Parameters["@ReportSortOption_3_Type"].Value = sortOp_3_ty;
                objSQlComm.Parameters["@ReportTitle"].Value = repTitle;
                objSQlComm.Parameters["@ReportCount"].Value = repCount;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdateSimpleReportEntry(string repDate)
        {
            string strSQLComm = " update ReportEntry set DateRange=@DateRange,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DateRange", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DateRange"].Value = repDate;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdateComplexReportEntry(string repDate, string repType, string sortOp_1, string sortOp_1_ty, string sortOp_2, string sortOp_2_ty, string sortOp_3, string sortOp_3_ty)
        {
            string strSQLComm = " update ReportEntry set DateRange = @DateRange, ReportType = @ReportType, "
                              + " ReportSortOption_1 = @ReportSortOption_1, ReportSortOption_1_Type = @ReportSortOption_1_Type, "
                              + " ReportSortOption_2 = @ReportSortOption_2, ReportSortOption_2_Type = @ReportSortOption_2_Type, "
                              + " ReportSortOption_3 = @ReportSortOption_3, ReportSortOption_3_Type = @ReportSortOption_3_Type, "
                              + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DateRange", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_1_Type", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_2", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_2_Type", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_3", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReportSortOption_3_Type", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DateRange"].Value = repDate;
                objSQlComm.Parameters["@ReportType"].Value = repType;

                objSQlComm.Parameters["@ReportSortOption_1"].Value = sortOp_1;
                objSQlComm.Parameters["@ReportSortOption_1_Type"].Value = sortOp_1_ty;

                objSQlComm.Parameters["@ReportSortOption_2"].Value = sortOp_2;
                objSQlComm.Parameters["@ReportSortOption_2_Type"].Value = sortOp_2_ty;

                objSQlComm.Parameters["@ReportSortOption_3"].Value = sortOp_3;
                objSQlComm.Parameters["@ReportSortOption_3_Type"].Value = sortOp_3_ty;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion

        public string AddStoreCodeForGwareHost(string pscode, string psname)
        {
            string strSQLComm = " insert into CentralExportImport (StoreCode,StoreName) values ( @StoreCode, @StoreName) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = pscode;
                objSQlComm.Parameters["@StoreName"].Value = psname;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateStoreCodeForGwareHost(string pscode, string psname)
        {
            string strSQLComm = " update CentralExportImport set StoreCode=@StoreCode, StoreName=@StoreName  ";
                             
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = pscode;
                objSQlComm.Parameters["@StoreName"].Value = psname;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateStoreCodeForGwareHostWithId(int storeId, string pscode, string psname)
        {
            string strSQLComm = " update CentralExportImport set StoreCode=@StoreCode, StoreName=@StoreName where ID=" + storeId ;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode"].Value = pscode;
                objSQlComm.Parameters["@StoreName"].Value = psname;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateGwareHostSetup(string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string strSQLComm = " update setup set CentralExportImport=@CentralExportImport,CloudServer=@CloudServer,CloudDB=@CloudDB,CloudUser=@CloudUser,CloudPassword=@CloudPassword,CheckRunScaleControllerOnHostImport=@CheckRunScaleControllerOnHostImport ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CentralExportImport", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CloudServer", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CloudDB", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CloudUser", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CloudPassword", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckRunScaleControllerOnHostImport", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@CentralExportImport"].Value = p1;
                objSQlComm.Parameters["@CloudServer"].Value = p2;
                objSQlComm.Parameters["@CloudDB"].Value = p3;
                objSQlComm.Parameters["@CloudUser"].Value = p4;
                objSQlComm.Parameters["@CloudPassword"].Value = p5;
                objSQlComm.Parameters["@CheckRunScaleControllerOnHostImport"].Value = p6;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateGiftCertStore(string p)
        {
            string strSQLComm = " update giftcert set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = p;
                objSQlComm.Parameters["@StoreCode2"].Value = p;
                
                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCustomerStore(string p)
        {
            string strSQLComm = " update customer set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = p;
                objSQlComm.Parameters["@StoreCode2"].Value = p;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCustomerAcRecvStore(string p)
        {
            string strSQLComm = " update acctrecv set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where ((IssueStore = '' and OperateStore = '') or (IssueStore is null and OperateStore is null)) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = p;
                objSQlComm.Parameters["@StoreCode2"].Value = p;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateGeneralMappingStore(string p)
        {
            string strSQLComm = " update generalmapping set IssueStore=@StoreCode1 where ((IssueStore = '') or (IssueStore is null))  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode1"].Value = p;
                
                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateEmployeeStore(string p)
        {
            string strSQLComm = " update employee set IssueStore=@StoreCode1,OperateStore=@StoreCode2 where (IssueStore is '' or IssueStore is null) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@StoreCode1"].Value = p;
                objSQlComm.Parameters["@StoreCode2"].Value = p;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateScaleSetup(string p1, string p2, string p3, string p4)
        {

            string strSQLComm = " update setup set PrintPriceSmartScaleLabel=@PrintPriceSmartScaleLabel,PriceSmartCurrency=@PriceSmartCurrency,ShelfLifeDateExtend=@ShelfLifeDateExtend, SendCheckDigitForLabelsDotNet = @SendCheckDigitForLabelsDotNet ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@PrintPriceSmartScaleLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceSmartCurrency", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShelfLifeDateExtend", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SendCheckDigitForLabelsDotNet", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@PrintPriceSmartScaleLabel"].Value = p1;
                objSQlComm.Parameters["@PriceSmartCurrency"].Value = p2;
                objSQlComm.Parameters["@ShelfLifeDateExtend"].Value = p3;
                objSQlComm.Parameters["@SendCheckDigitForLabelsDotNet"].Value = p4;
                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateScaleLogo()
        {
            byte[] Photo = GetPhoto(strPhotoFilePath1);
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "";

            strSQLComm = " update StoreInfo set ScaleLogo = @ScaleLogo ";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleLogo", System.Data.SqlDbType.Image));
                if (strPhotoFilePath1 == "")
                {
                    objSQlComm.Parameters["@ScaleLogo"].Value = Convert.DBNull;
                }
                else
                {
                    if (strPhotoFilePath1 == "null")
                    {
                        objSQlComm.Parameters["@ScaleLogo"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@ScaleLogo"].Value = Photo;
                    }

                }
                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        public string UpdateScaleFunctionButtonDisplayOrder(int p1, int p2, string p3)
        {

            string strSQLComm = " update POSFunctionSetup set DisplayOrder=@DisplayOrder,IsVisible=@IsVisible where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@DisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IsVisible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@DisplayOrder"].Value = p2;
                objSQlComm.Parameters["@IsVisible"].Value = p3;
                objSQlComm.Parameters["@ID"].Value = p1;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        public DataTable FetchStoreData()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select StoreCode,StoreName,ExportFolderPath from CentralExportImport ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExportFolderPath", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                
                                                objSQLReader["StoreCode"].ToString(),
                                                objSQLReader["StoreName"].ToString(),
                                                objSQLReader["ExportFolderPath"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchCloudParameters()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select CloudServer,CloudDB,CloudUser,CloudPassword from setup ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CloudServer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloudDB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloudUser", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloudPassword", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                
                                                objSQLReader["CloudServer"].ToString(),
                                                objSQLReader["CloudDB"].ToString(),
                                                objSQLReader["CloudUser"].ToString(),
                                                objSQLReader["CloudPassword"].ToString()
                                            });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string PostShopifySetupData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {

                intShopifyRecordCount = ShopifyRecordCount(SaveComm);

                if (intShopifyRecordCount == 0)
                {
                    strError = InsertShopifyInformation(SaveComm);
                }
                else
                {
                    strError = UpdateShopifyInformation(SaveComm);
                }


                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        private int ShopifyRecordCount(SqlCommand objSQlComm)
        {
            int val = 0;
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " select count(*) as rcnt from shopify ";

            objSQlComm.CommandText = strSQLComm;

            try
            {

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
        }

        public string InsertShopifyInformation(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into Shopify( ShopifyAPIKey,ShopifyPassword,ShopifyStoreAddress,PremiumPlan) "
                       + " values ( @ShopifyAPIKey,@ShopifyPassword,@ShopifyStoreAddress,@PremiumPlan) ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyAPIKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyPassword", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyStoreAddress", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PremiumPlan", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PremiumPlan"].Value = strPremiumPlan;

                objSQlComm.Parameters["@ShopifyAPIKey"].Value = strShopifyAPIKey;
                objSQlComm.Parameters["@ShopifyPassword"].Value = strShopifyPassword;
                objSQlComm.Parameters["@ShopifyStoreAddress"].Value = strShopifyStoreAddress;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateShopifyInformation(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " update Shopify set ShopifyAPIKey = @ShopifyAPIKey,ShopifyPassword=@ShopifyPassword,ShopifyStoreAddress=@ShopifyStoreAddress,PremiumPlan=@PremiumPlan ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyAPIKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyPassword", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyStoreAddress", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ShopifyAPIKey"].Value = strShopifyAPIKey;
                objSQlComm.Parameters["@ShopifyPassword"].Value = strShopifyPassword;
                objSQlComm.Parameters["@ShopifyStoreAddress"].Value = strShopifyStoreAddress;

                objSQlComm.Parameters.Add(new SqlParameter("@PremiumPlan", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PremiumPlan"].Value = strPremiumPlan;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateCentralCustomerID(int refCustID)
        {
            string strSQLComm = " update setup set CentralCustomerID=@CID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CID"].Value = refCustID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string UpdateCentralExportImportFlag(string refFlag)
        {
            string strSQLComm = " update setup set CentralExportImport=@F ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@F", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@F"].Value = refFlag;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        #region QuickBooks Setup

        public string PostQuickBooksSetupData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {

                intQuickBooksRecordCount = QBInfoRecordCount(SaveComm);

                if (intQuickBooksRecordCount == 0)
                {
                    strError = InsertQBInfo(SaveComm);
                }
                else
                {
                    strError = UpdateQBInfo(SaveComm);
                }


                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        private int QBInfoRecordCount(SqlCommand objSQlComm)
        {
            int val = 0;
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " select count(*) as rcnt from QuickBooksInfo ";

            objSQlComm.CommandText = strSQLComm;

            try
            {

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
        }

        public string InsertQBInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into QuickBooksInfo( QuickBooksWindowsCompanyFilePath, ItemIncomeAccount, ItemCOGSAccount) "
                       + " values ( @QuickBooksWindowsCompanyFilePath, @ItemIncomeAccount,@ItemCOGSAccount) ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@QuickBooksWindowsCompanyFilePath", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@QuickBooksWindowsCompanyFilePath"].Value = strQBCompanyFilePath;

                objSQlComm.Parameters.Add(new SqlParameter("@ItemIncomeAccount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ItemIncomeAccount"].Value = strItemIncomeAccount;

                objSQlComm.Parameters.Add(new SqlParameter("@ItemCOGSAccount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ItemCOGSAccount"].Value = strItemCOGSAccount;


                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateQBInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " update QuickBooksInfo set QuickBooksWindowsCompanyFilePath = @QuickBooksWindowsCompanyFilePath,"
                       + " ItemIncomeAccount=@ItemIncomeAccount,ItemCOGSAccount=@ItemCOGSAccount ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@QuickBooksWindowsCompanyFilePath", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@QuickBooksWindowsCompanyFilePath"].Value = strQBCompanyFilePath;

                objSQlComm.Parameters.Add(new SqlParameter("@ItemIncomeAccount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ItemIncomeAccount"].Value = strItemIncomeAccount;

                objSQlComm.Parameters.Add(new SqlParameter("@ItemCOGSAccount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ItemCOGSAccount"].Value = strItemCOGSAccount;


                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }


        public void AddQBAutoData()
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_AddAutoDataForQuickBooksWindows";

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

        #endregion

        #region WooCommerce Setup

        public string PostWooCommerceSetupData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {

                intWooCommRecordCount = WooCommInfoRecordCount(SaveComm);

                if (intWooCommRecordCount == 0)
                {
                    strError = InsertWooCommInfo(SaveComm);
                }
                else
                {
                    strError = UpdateWooCommInfo(SaveComm);
                }


                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        private int WooCommInfoRecordCount(SqlCommand objSQlComm)
        {
            int val = 0;
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " select count(*) as rcnt from WooCommerceInfo ";

            objSQlComm.CommandText = strSQLComm;

            try
            {

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
        }

        public string InsertWooCommInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into WooCommerceInfo( WCommConsumerKey,WCommConsumerSecret,WCommStoreAddress) "
                       + " values ( @WCommConsumerKey,@WCommConsumerSecret,@WCommStoreAddress) ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@WCommConsumerKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WCommConsumerSecret", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WCommStoreAddress", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@WCommConsumerKey"].Value = strWCommConsumerKey;
                objSQlComm.Parameters["@WCommConsumerSecret"].Value = strWCommConsumerSecret;
                objSQlComm.Parameters["@WCommStoreAddress"].Value = strWCommStoreAddress;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateWooCommInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " update WooCommerceInfo set WCommConsumerKey = @WCommConsumerKey,WCommConsumerSecret=@WCommConsumerSecret,WCommStoreAddress=@WCommStoreAddress ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@WCommConsumerKey", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WCommConsumerSecret", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WCommStoreAddress", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@WCommConsumerKey"].Value = strWCommConsumerKey;
                objSQlComm.Parameters["@WCommConsumerSecret"].Value = strWCommConsumerSecret;
                objSQlComm.Parameters["@WCommStoreAddress"].Value = strWCommStoreAddress;

                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion


        #region XERO Setup

        public string PostXEROSetupData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {

                intXERORecordCount = XEROInfoRecordCount(SaveComm);

                if (intXERORecordCount == 0)
                {
                    strError = InsertXEROInfo(SaveComm);
                }
                else
                {
                    strError = UpdateXEROInfo(SaveComm);
                }


                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        private int XEROInfoRecordCount(SqlCommand objSQlComm)
        {
            int val = 0;
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Clear();

            string strSQLComm = "";

            strSQLComm = " select count(*) as rcnt from XeroInfo ";

            objSQlComm.CommandText = strSQLComm;

            try
            {

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    }
                    catch
                    {
                    }
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLReader.Close();
                objSQLReader.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
        }

        public string InsertXEROInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " insert into XeroInfo( XeroCompanyName, XeroClientId, XeroCallbackUrl, XeroAccessToken, XeroRefreshToken, XeroTenantId, "
                       + " XeroInventoryAssetAccountCode,XeroCOGSAccountCodePurchase,XeroCOGSAccountCodeSale,XeroAccountCodeSale,XeroAccountCodePurchase,XeroInventoryAdjustmentAccountCode) "
                       + " values ( @XeroCompanyName, @XeroClientId, @XeroCallbackUrl, @XeroAccessToken, @XeroRefreshToken, @XeroTenantId, "
                       + " @XeroInventoryAssetAccountCode,@XeroCOGSAccountCodePurchase,@XeroCOGSAccountCodeSale,@XeroAccountCodeSale,@XeroAccountCodePurchase,@XeroInventoryAdjustmentAccountCode) ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@XeroCompanyName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCompanyName"].Value = strXeroCompanyName;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroClientId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroClientId"].Value = strXeroClientId;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCallbackUrl", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroCallbackUrl"].Value = strXeroCallbackUrl;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccessToken", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroAccessToken"].Value = strXeroAccessToken;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroRefreshToken", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroRefreshToken"].Value = strXeroRefreshToken;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroTenantId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroTenantId"].Value = strXeroTenantId;


                objSQlComm.Parameters.Add(new SqlParameter("@XeroInventoryAssetAccountCode", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroInventoryAssetAccountCode"].Value = strXeroInventoryAssetAccountCode;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCOGSAccountCodePurchase", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCOGSAccountCodePurchase"].Value = strXeroCOGSAccountCodePurchase;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCOGSAccountCodeSale", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCOGSAccountCodeSale"].Value = strXeroCOGSAccountCodeSale;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccountCodeSale", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroAccountCodeSale"].Value = strXeroAccountCodeSale;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccountCodePurchase", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroAccountCodePurchase"].Value = strXeroAccountCodePurchase;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroInventoryAdjustmentAccountCode", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroInventoryAdjustmentAccountCode"].Value = strXeroInventoryAdjustmentAccountCode;



                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateXEROInfo(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();

            string strSQLComm = "";
            strSQLComm = " update XeroInfo set XeroCompanyName=@XeroCompanyName, XeroClientId=@XeroClientId, XeroCallbackUrl=@XeroCallbackUrl, "
                      + " XeroAccessToken=@XeroAccessToken, XeroRefreshToken=@XeroRefreshToken, XeroTenantId=@XeroTenantId, "
                      + " XeroInventoryAssetAccountCode=@XeroInventoryAssetAccountCode,XeroCOGSAccountCodePurchase=@XeroCOGSAccountCodePurchase,"
                      + " XeroCOGSAccountCodeSale=@XeroCOGSAccountCodeSale,XeroAccountCodeSale=@XeroAccountCodeSale,"
                      + " XeroAccountCodePurchase=@XeroAccountCodePurchase,XeroInventoryAdjustmentAccountCode=@XeroInventoryAdjustmentAccountCode ";
            objSQlComm.CommandText = strSQLComm;

            try
            {
                objSQlComm.Parameters.Add(new SqlParameter("@XeroCompanyName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCompanyName"].Value = strXeroCompanyName;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroClientId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroClientId"].Value = strXeroClientId;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCallbackUrl", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroCallbackUrl"].Value = strXeroCallbackUrl;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccessToken", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroAccessToken"].Value = strXeroAccessToken;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroRefreshToken", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroRefreshToken"].Value = strXeroRefreshToken;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroTenantId", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@XeroTenantId"].Value = strXeroTenantId;


                objSQlComm.Parameters.Add(new SqlParameter("@XeroInventoryAssetAccountCode", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroInventoryAssetAccountCode"].Value = strXeroInventoryAssetAccountCode;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCOGSAccountCodePurchase", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCOGSAccountCodePurchase"].Value = strXeroCOGSAccountCodePurchase;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroCOGSAccountCodeSale", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroCOGSAccountCodeSale"].Value = strXeroCOGSAccountCodeSale;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccountCodeSale", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroAccountCodeSale"].Value = strXeroAccountCodeSale;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroAccountCodePurchase", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroAccountCodePurchase"].Value = strXeroAccountCodePurchase;

                objSQlComm.Parameters.Add(new SqlParameter("@XeroInventoryAdjustmentAccountCode", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@XeroInventoryAdjustmentAccountCode"].Value = strXeroInventoryAdjustmentAccountCode;


                objSQlComm.ExecuteNonQuery();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        #endregion


        public DataTable FetchSalesDataForQuickBooksOnline()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @"select inv.ID, inv.CreatedOn,  trim(i.Description) as ItemName, i.Qty, (i.TaxTotal1 + i.TaxTotal2 + i.TaxTotal3) as Tax, 
(i.Price*i.Qty) - i.Discount as TS, (i.TaxIncludeRate*i.Qty) as TS1,  i.Price, i.TaxIncludeRate, 
i.Taxable1,i.Taxable2,i.Taxable3,i.TaxID1,i.TaxID2,i.TaxID3,
Case when inv.CustomerID = 0 then '' else (select trim(FirstName + ' ' + LastName) from Customer where ID = inv.CustomerID) end as Cust,
(select TaxInclusive from Setup) as TxF
from invoice inv left outer join item i on i.InvoiceNo = inv.ID 
where inv.Status = 3 and inv.ServiceType = 'Sales' and inv.QuickBooksCloudFlag = 'N' and i.ProductType in ('P','B')
order by inv.ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Item", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Rate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Total", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxLogic", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FileNo", System.Type.GetType("System.Int32"));



                double dQty = 0;
                double dRate = 0;
                double dTotal = 0;
                double dTax = 0;
                string txlogic = "";
                while (objSQLReader.Read())
                {
                    if (Functions.fnDouble(objSQLReader["Qty"].ToString()) < 0) continue;
                    if (objSQLReader["TxF"].ToString() == "N")
                    {
                        if (Functions.fnDouble(objSQLReader["TS"].ToString()) < 0) continue;
                    }
                    else
                    {
                        if (Functions.fnDouble(objSQLReader["TS1"].ToString()) < 0) continue;
                    }

                    txlogic = "";
                    if (objSQLReader["Taxable1"].ToString() == "Y")
                    {
                        if (Functions.fnInt32(objSQLReader["TaxID1"].ToString()) > 1)
                        {
                            txlogic = txlogic + objSQLReader["TaxID1"].ToString() + ",";
                        }
                    }
                    if (objSQLReader["Taxable2"].ToString() == "Y")
                    {
                        if (Functions.fnInt32(objSQLReader["TaxID2"].ToString()) > 1)
                        {
                            txlogic = txlogic + objSQLReader["TaxID2"].ToString() + ",";
                        }
                    }
                    if (objSQLReader["Taxable3"].ToString() == "Y")
                    {
                        if (Functions.fnInt32(objSQLReader["TaxID3"].ToString()) > 1)
                        {
                            txlogic = txlogic + objSQLReader["TaxID3"].ToString() + ",";
                        }
                    }
                    dQty = 0;
                    dRate = 0;
                    dTotal = 0;
                    dTax = 0;

                    dQty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    dTax = Functions.fnDouble(objSQLReader["Tax"].ToString());
                    dRate = objSQLReader["TxF"].ToString() == "N" ? Functions.fnDouble(objSQLReader["Price"].ToString()) : Functions.fnDouble(objSQLReader["TaxIncludeRate"].ToString());
                    dTotal = objSQLReader["TxF"].ToString() == "N" ? Functions.fnDouble(objSQLReader["TS"].ToString()) : Functions.fnDouble(objSQLReader["TS1"].ToString());

                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["ID"].ToString(),
                                                   Functions.fnDate(objSQLReader["CreatedOn"].ToString()).ToString("dd-MM-yyyy"),
                                                   objSQLReader["Cust"].ToString(),
                                                   objSQLReader["ItemName"].ToString(),
                                                   dQty <=1 ? String.Format("{0:#,#}", dQty) : String.Format("{0:#,#}", dQty),
                                                   dRate <= 1 ? String.Format("{0:0.00}", dRate) : String.Format("{0:#,#.00}", dRate),
                                                   dTotal <=1 ? String.Format("{0:0.00}", dTotal) : String.Format("{0:#,#.00}", dTotal),
                                                   dTax <= 1 ? String.Format("{0:0.00}", dTax) : String.Format("{0:#,#.00}", dTax),
                                                   txlogic,
                                                   "0%",0});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["Rate"].ToString().EndsWith("0"))
                    {
                        dr["Rate"] = dr["Rate"].ToString().Substring(0, dr["Rate"].ToString().Length - 1);
                    }
                    if (dr["Total"].ToString().EndsWith("0"))
                    {
                        dr["Total"] = dr["Total"].ToString().Substring(0, dr["Total"].ToString().Length - 1);
                    }
                    if (dr["Tax"].ToString().EndsWith("0"))
                    {
                        dr["Tax"] = dr["Tax"].ToString().Substring(0, dr["Tax"].ToString().Length - 1);
                    }
                }

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["Rate"].ToString().EndsWith(".0"))
                    {
                        dr["Rate"] = dr["Rate"].ToString().Replace(".0", "");
                    }
                    if (dr["Total"].ToString().EndsWith(".0"))
                    {
                        dr["Total"] = dr["Total"].ToString().Replace(".0", "");
                    }
                    if (dr["Tax"].ToString().EndsWith(".0"))
                    {
                        dr["Tax"] = dr["Tax"].ToString().Replace(".0", "");
                    }
                }

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchTaxCodeForQuickBooksOnline()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @"select id, taxrate from taxheader";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("id", System.Type.GetType("System.String"));
                dtbl.Columns.Add("rate", System.Type.GetType("System.String"));

                double dRate = 0;


                while (objSQLReader.Read())
                {

                    dRate = 0;

                    dRate = Functions.fnDouble(objSQLReader["taxrate"].ToString());

                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["id"].ToString(),
                                                  dRate <= 1 ? String.Format("{0:0.00}", dRate) : String.Format("{0:#,#.00}", dRate)
 });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["Rate"].ToString().EndsWith("0"))
                    {
                        dr["Rate"] = dr["Rate"].ToString().Substring(0, dr["Rate"].ToString().Length - 1);
                    }

                }

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["Rate"].ToString().EndsWith(".0"))
                    {
                        dr["Rate"] = dr["Rate"].ToString().Replace(".0", "");
                    }

                }

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }


        public void UpdateQuickBooksOnlineFlag(int invID)
        {
            string strSQLComm = "";

            strSQLComm = " update Invoice set QuickBooksCloudFlag = 'Y' where ID = " + invID.ToString();


            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
        }

        public string UpdateBackupTypeSetup()
        {
            string strSQLComm = "";
            strSQLComm = " UPDATE Setup SET BackupType=@BackupType,BackupStorageType=@BackupStorageType ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQlComm.Parameters.Add(new SqlParameter("@BackupType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BackupStorageType", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@BackupType"].Value = intBackupType;
                objSQlComm.Parameters["@BackupStorageType"].Value = intBackupStorageType;


                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }


        #region Printer Type

        public string InsertPrinterTypes()
        {
            string strSQLComm = " insert into PrinterTypes( PrinterType, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                                + " VALUES ( @PrinterType,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@PrinterType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@PrinterType"].Value = strPrinterType;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdatePrinterTypes()
        {
            string strSQLComm = " update PrinterTypes set PrinterType=@PrinterType, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@PrinterType"].Value = strPrinterType;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable FetchPrinterTypes()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, PrinterType from PrinterTypes order by PrinterType ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["PrinterType"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable ShowPrinterTypesRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from PrinterTypes where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["PrinterType"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public int DuplicatePrinterTypesCount(string ptypedesc)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as reccnt from PrinterTypes where PrinterType=@PrinterType ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PrinterType"].Value = ptypedesc;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["reccnt"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string DeletePrinterTypesRecord(int DeleteID)
        {
            string strSQLComm = " delete from PrinterTypes where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = DeleteID;


                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int IsReceiptTemplateExists(int tempID)
        {
            string strSQLComm = "";
            int intCount = 0;
            if (tempID == 0) strSQLComm = " select count(*) as reccnt from PrintTemplate where IsReceiptTemplate = 'Y' ";
            if (tempID > 0) strSQLComm = " select count(*) as reccnt from PrintTemplate where IsReceiptTemplate = 'Y' and ID <> " + tempID.ToString();

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["reccnt"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int GetPrinterTemplateID(bool bReceipt, int PrinterID)
        {
            string strSQLComm = "";
            int intCount = 0;
            if (bReceipt) strSQLComm = " select isnull(ID, 0) as TID from PrintTemplate where IsReceiptTemplate = 'Y' ";
            if (!bReceipt) strSQLComm = " select TemplateID as TID from Printers where ID = " + PrinterID.ToString();

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["TID"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }



        public int GetReceiptPrintCopy()
        {
            string strSQLComm = "";
            int intCount = 0;
            strSQLComm = " select isnull(ReceiptPrintCopy, 1) as cnt from PrintTemplate where IsReceiptTemplate = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objSQLReader["cnt"].ToString());
                    }
                    catch { objSQLReader.Close(); }

                }
                objSQLReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }


        public DataTable FetchSpecificPrinters(int PrinterTypeID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "SELECT ID, PrinterName from Printers where PrinterTypeID = " + PrinterTypeID.ToString();

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {

                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["PrinterName"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        #endregion
    }
}
