using System;
using System.Data;
using System.Data.SqlClient;

namespace OfflineRetailV2.Data
{
    public class SecurityPermission
    {
        #region definining private variables

        private static SqlConnection sqlConn;

        // Screen Access
        private static bool blAccessCustomerScreen;
        private static bool blAccessProductScreen;
        private static bool blAccessReportsScreen;

        private static bool blAccessEmployeeScreen;
        private static bool blAccessShiftScreen;
        private static bool blAccessHolidayScreen;

        private static bool blAccessTaxScreen;
        private static bool blAccessTenderTypeScreen;
        private static bool blAccessSecurityScreen;

        private static bool blAccessVendorScreen;
        private static bool blAccessReorderReportScreen;
        private static bool blAccessPurchaseOrderScreen;
        private static bool blAccessReceivingScreen;
        private static bool blAccessPrintLabelScreen;

        private static bool blAccessDepartmentScreen;
        private static bool blAccessCategoryScreen;
        private static bool blAccessScales;
        private static bool blAccessGroupScreen;
        private static bool blAccessClassScreen;
        private static bool blAccessCRMParameterScreen;
        private static bool blAccessBrandScreen;

        private static bool blAccessBreakPackScreen;
        private static bool blAccessStockTakeScreen;
        private static bool blAccessDashboard;

        private static bool blAccessLabelNSign;
        private static bool blAccessLabelFormat;
        private static bool blAccessShelfTag;
        private static bool blAccessProductionList;
        private static bool blAccessMarkdown;

        private static bool blAccessCentralOffice;
        //Customer Access
        private static bool blAcssCustomerAdd;
        private static bool blAcssCustomerEdit;
        private static bool blAcssCustomerDelete;
        private static bool blAcssCustomerPrint;
        private static bool blAcssCustomerShipAddress;
        private static bool blAcssCustomerNotes;
        private static bool blAcssCustomerTaxExempt;
        private static bool blAcssCustomerPriceLevel;
        private static bool blAcssCustomerIssueCredit;
        private static bool blAcssGroup;
        private static bool blAcssClass;
        private static bool blAcssStoreCredit;
        private static bool blAcssHouseAcountAdjust;

        //Employee Access
        private static bool blAcssEmployeeAdd;
        private static bool blAcssEmployeeEdit;
        private static bool blAcssEmployeeDelete;
        private static bool blAcssEmployeePrint;
        private static bool blAcssEmployeePassword;
        private static bool blAcssEmployeeAttendanceUpdate;

        //Product Access
        private static bool blAcssProductAdd;
        private static bool blAcssProductEdit;
        private static bool blAcssProductDelete;
        private static bool blAcssProductPrint;
        private static bool blAcssProductCost;
        private static bool blAcssProductPrice;
        private static bool blAcssProductOnHandQty;
        private static bool blAcssDepartment;
        private static bool blAcssCategory;
        private static bool blAcssBrand;

        //Report Access
        private static bool blAcssCustomerDtlRep;
        private static bool blAcssCustomerGenRep;
        private static bool blAcssVendorDtlRep;
        private static bool blAcssVendorGenRep;
        private static bool blAcssProductDtlRep;
        private static bool blAcssProductGenRep;
        private static bool blAcssProductKitRep;
        private static bool blAcssProductMatxRep;
        private static bool blAcssEmployeeDtlRep;
        private static bool blAcssEmployeeAttnRep;
        private static bool blAcssEmployeeLateRep;
        private static bool blAcssEmployeeAbsentRep;
        private static bool blAcssOrderingPORep;
        private static bool blAcssOrderingRecvRep;
        private static bool blAcssOrderingReorderRep;

        private static bool blAcssSalesRep;
        private static bool blAcssSalesSummaryRep;
        private static bool blAcssCustomerOtherRep;

        private static bool blAcssSalesCardTranRep;
        private static bool blAcssMinimumOrderRep;
        private int intLoginUserID;

        //Vendor Access
        private static bool blAcssVendorAdd;
        private static bool blAcssVendorEdit;
        private static bool blAcssVendorDelete;
        private static bool blAcssVendorPrint;

        //PO Access
        private static bool blAcssPOAdd;
        private static bool blAcssPOEdit;
        private static bool blAcssPODelete;
        private static bool blAcssPOPrint;

        //Receiving Access
        private static bool blAcssRecvAdd;
        private static bool blAcssRecvEdit;
        private static bool blAcssRecvDelete;
        private static bool blAcssRecvPrint;

        //Scale Access
        private static bool blAcssScale;
        private static bool blAcssScalePrint;
        private static bool blAcssScaleSetup;

        private static bool blAcssScale_ChangeUnitPrice;
        private static bool blAcssScale_ChangeTare;
        private static bool blAcssScale_ChangeByCount;
        private static bool blAcssScale_ChangeProductLife;
        private static bool blAcssScale_ChangeShelfLife;
        private static bool blAcssScale_ManualWeightForNoScale;

        //Setup Access
        private static bool blAcssSetup;
        private static bool blAcssDataPurging;
        private static bool blAcssReportSchd;
        private static bool blAcssGwareHostSetup;
        private static bool blAcss3rdPartyIntegration;
        private static bool blAcssScaleSettings;
        private static bool blAcssMarkdownSettings;
        //Setup Access
        private static bool blAcssDiscount;

        // POS Access
        private static bool blAcssPOSCustomer;
        private static bool blAcssPOSProduct;
        private static bool blAcssPOSReport;
        private static bool blAcssPOSSetup;

        private static bool blAcssPOSPaidout;
        private static bool blAcssPOSNosale;
        private static bool blAcssPOSAcctpay;
        private static bool blAcssPOSGiftCert;
        private static bool blAcssPOSSuspend;
        private static bool blAcssPOSVoid;
        private static bool blAcssPOSReturnReprint;
        private static bool blAcssPOSCancel;
        private static bool blAcssPOSItemEdit;
        private static bool blAcssPOSTenderAllowType;
        private static bool blAcssPOSTenderExit;
        private static bool blAcssPOSTenderDiscount;
        private static bool blAcssPOSTender;

        private static bool blAcssPOSLayawayTran;
        private static bool blAcssPOSLayawayRefund;
        private static bool blAcssPOSLayawayMinimumDeposit;

        private static bool blAcssPOSFuncSelectFunctions;
        private static bool blAcssPOSFuncUsePriceLevel;
        private static bool blAcssPOSFuncChangePrice;
        private static bool blAcssOpenCashDrawer;

        private static bool blAcssNegativeCardTransaction;

        // access for closeout

        private static bool blAccessBlindDrop;
        private static bool blAccessBlindCount;
        private static bool blAccessReconcileCount;
        private static bool blAccessOtherTerminalCloseout;


        private static bool blAccessExit;
        private static bool blAccessCashFloat;
        private static bool blAccessCashInOut;
        #endregion

        public SecurityPermission()
        {
           
        }

        #region definining public properties

        public static SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public static bool AccessCashFloat
        {
            get { return blAccessCashFloat; }
            set { blAccessCashFloat = value; }
        }

        public static bool AccessCashInOut
        {
            get { return blAccessCashInOut; }
            set { blAccessCashInOut = value; }
        }

        public static bool AccessMarkdown
        {
            get { return blAccessMarkdown; }
            set { blAccessMarkdown = value; }
        }

        public static bool AcssMarkdownSettings
        {
            get { return blAcssMarkdownSettings; }
            set { blAcssMarkdownSettings = value; }
        }


        public static bool AccessScale_ChangeUnitPrice
        {
            get { return blAcssScale_ChangeUnitPrice; }
            set { blAcssScale_ChangeUnitPrice = value; }
        }

        public static bool AccessScale_ChangeTare
        {
            get { return blAcssScale_ChangeTare; }
            set { blAcssScale_ChangeTare = value; }
        }

        public static bool AccessScale_ChangeByCount
        {
            get { return blAcssScale_ChangeByCount; }
            set { blAcssScale_ChangeByCount = value; }
        }

        public static bool AccessScale_ChangeProductLife
        {
            get { return blAcssScale_ChangeProductLife; }
            set { blAcssScale_ChangeProductLife = value; }
        }

        public static bool AccessScale_ChangeShelfLife
        {
            get { return blAcssScale_ChangeShelfLife; }
            set { blAcssScale_ChangeShelfLife = value; }
        }


        public static bool AcssScale_ManualWeightForNoScale
        {
            get { return blAcssScale_ManualWeightForNoScale; }
            set { blAcssScale_ManualWeightForNoScale = value; }
        }


        public static bool AccessDataPurging
        {
            get { return blAcssDataPurging; }
            set { blAcssDataPurging = value; }
        }

        public static bool AccessExit
        {
            get { return blAccessExit; }
            set { blAccessExit = value; }
        }

        public static bool AccessOtherTerminalCloseout
        {
            get { return blAccessOtherTerminalCloseout; }
            set { blAccessOtherTerminalCloseout = value; }
        }


        public static bool AccessBlindDrop
        {
            get { return blAccessBlindDrop; }
            set { blAccessBlindDrop = value; }
        }

        public static bool AccessBlindCount
        {
            get { return blAccessBlindCount; }
            set { blAccessBlindCount = value; }
        }

        public static bool AccessReconcileCount
        {
            get { return blAccessReconcileCount; }
            set { blAccessReconcileCount = value; }
        }


        // Screen Access

        public static bool AccessDashboard
        {
            get { return blAccessDashboard; }
            set { blAccessDashboard = value; }
        }


        public static bool AccessDiscountTab
        {
            get { return blAcssDiscount; }
            set { blAcssDiscount = value; }
        }

        public static bool AccessCentralOffice
        {
            get { return blAccessCentralOffice; }
            set { blAccessCentralOffice = value; }
        }

        public static bool AcssMinimumOrderRep
        {
            get { return blAcssMinimumOrderRep; }
            set { blAcssMinimumOrderRep = value; }
        }

        public static bool AccessStockTakeScreen
        {
            get { return blAccessStockTakeScreen; }
            set { blAccessStockTakeScreen = value; }
        }

        public static bool AccessBreakPackScreen
        {
            get { return blAccessBreakPackScreen; }
            set { blAccessBreakPackScreen = value; }
        }

        public static bool AccessBrandScreen
        {
            get { return blAccessBrandScreen; }
            set { blAccessBrandScreen = value; }
        }
        public static bool AccessCustomerScreen
        {
            get { return blAccessCustomerScreen; }
            set { blAccessCustomerScreen = value; }
        }
        public static bool AccessProductScreen
        {
            get { return blAccessProductScreen; }
            set { blAccessProductScreen = value; }
        }
        public static bool AccessReportsScreen
        {
            get { return blAccessReportsScreen; }
            set { blAccessReportsScreen = value; }
        }

        public static bool AccessEmployeeScreen
        {
            get { return blAccessEmployeeScreen; }
            set { blAccessEmployeeScreen = value; }
        }
        public static bool AccessShiftScreen
        {
            get { return blAccessShiftScreen; }
            set { blAccessShiftScreen = value; }
        }
        public static bool AccessHolidayScreen
        {
            get { return blAccessHolidayScreen; }
            set { blAccessHolidayScreen = value; }
        }

        public static bool AccessTaxScreen
        {
            get { return blAccessTaxScreen; }
            set { blAccessTaxScreen = value; }
        }
        public static bool AccessTenderTypeScreen
        {
            get { return blAccessTenderTypeScreen; }
            set { blAccessTenderTypeScreen = value; }
        }
        public static bool AccessSecurityScreen
        {
            get { return blAccessSecurityScreen; }
            set { blAccessSecurityScreen = value; }
        }

        public static bool AccessVendorScreen
        {
            get { return blAccessVendorScreen; }
            set { blAccessVendorScreen = value; }
        }
        public static bool AccessReorderReportScreen
        {
            get { return blAccessReorderReportScreen; }
            set { blAccessReorderReportScreen = value; }
        }
        public static bool AccessPurchaseOrderScreen
        {
            get { return blAccessPurchaseOrderScreen; }
            set { blAccessPurchaseOrderScreen = value; }
        }
        public static bool AccessReceivingScreen
        {
            get { return blAccessReceivingScreen; }
            set { blAccessReceivingScreen = value; }
        }
        public static bool AccessPrintLabelScreen
        {
            get { return blAccessPrintLabelScreen; }
            set { blAccessPrintLabelScreen = value; }
        }

        public static bool AccessDepartmentScreen
        {
            get { return blAccessDepartmentScreen; }
            set { blAccessDepartmentScreen = value; }
        }
        public static bool AccessCategoryScreen
        {
            get { return blAccessCategoryScreen; }
            set { blAccessCategoryScreen = value; }
        }
        public static bool AccessScales
        {
            get { return blAccessScales; }
            set { blAccessScales = value; }
        }
        public static bool AccessGroupScreen
        {
            get { return blAccessGroupScreen; }
            set { blAccessGroupScreen = value; }
        }
        public static bool AccessClassScreen
        {
            get { return blAccessClassScreen; }
            set { blAccessClassScreen = value; }
        }
        public static bool AccessCRMParameterScreen
        {
            get { return blAccessCRMParameterScreen; }
            set { blAccessCRMParameterScreen = value; }
        }

        public static bool AccessLabelNSign
        {
            get { return blAccessLabelNSign; }
            set { blAccessLabelNSign = value; }
        }

        public static bool AccessLabelFormat
        {
            get { return blAccessLabelFormat; }
            set { blAccessLabelFormat = value; }
        }

        public static bool AccessShelfTag
        {
            get { return blAccessShelfTag; }
            set { blAccessShelfTag = value; }
        }

        public static bool AccessProductionList
        {
            get { return blAccessProductionList; }
            set { blAccessProductionList = value; }
        }


        //Customer Access
        public static bool AcssCustomerAdd
        {
            get { return blAcssCustomerAdd; }
            set { blAcssCustomerAdd = value; }
        }
        public static bool AcssCustomerEdit
        {
            get { return blAcssCustomerEdit; }
            set { blAcssCustomerEdit = value; }
        }
        public static bool AcssCustomerDelete
        {
            get { return blAcssCustomerDelete; }
            set { blAcssCustomerDelete = value; }
        }
        public static bool AcssCustomerPrint
        {
            get { return blAcssCustomerPrint; }
            set { blAcssCustomerPrint = value; }
        }
        public static bool AcssCustomerShipAddress
        {
            get { return blAcssCustomerShipAddress; }
            set { blAcssCustomerShipAddress = value; }
        }
        public static bool AcssCustomerNotes
        {
            get { return blAcssCustomerNotes; }
            set { blAcssCustomerNotes = value; }
        }
        public static bool AcssCustomerTaxExempt
        {
            get { return blAcssCustomerTaxExempt; }
            set { blAcssCustomerTaxExempt = value; }
        }
        public static bool AcssCustomerPriceLevel
        {
            get { return blAcssCustomerPriceLevel; }
            set { blAcssCustomerPriceLevel = value; }
        }
        public static bool AcssCustomerIssueCredit
        {
            get { return blAcssCustomerIssueCredit; }
            set { blAcssCustomerIssueCredit = value; }
        }
        public static bool AcssGroup
        {
            get { return blAcssGroup; }
            set { blAcssGroup = value; }
        }
        public static bool AcssClass
        {
            get { return blAcssClass; }
            set { blAcssClass = value; }
        }

        public static bool AcssStoreCredit
        {
            get { return blAcssStoreCredit; }
            set { blAcssStoreCredit = value; }
        }

        public static bool AcssHouseAcountAdjust
        {
            get { return blAcssHouseAcountAdjust; }
            set { blAcssHouseAcountAdjust = value; }
        }

        //Employee Access
        public static bool AcssEmployeeAdd
        {
            get { return blAcssEmployeeAdd; }
            set { blAcssEmployeeAdd = value; }
        }
        public static bool AcssEmployeeEdit
        {
            get { return blAcssEmployeeEdit; }
            set { blAcssEmployeeEdit = value; }
        }
        public static bool AcssEmployeeDelete
        {
            get { return blAcssEmployeeDelete; }
            set { blAcssEmployeeDelete = value; }
        }
        public static bool AcssEmployeePrint
        {
            get { return blAcssEmployeePrint; }
            set { blAcssEmployeePrint = value; }
        }
        public static bool AcssEmployeePassword
        {
            get { return blAcssEmployeePassword; }
            set { blAcssEmployeePassword = value; }
        }

        public static bool AcssEmployeeAttendanceUpdate
        {
            get { return blAcssEmployeeAttendanceUpdate; }
            set { blAcssEmployeeAttendanceUpdate = value; }
        }

        //Product Access
        public static bool AcssProductAdd
        {
            get { return blAcssProductAdd; }
            set { blAcssProductAdd = value; }
        }
        public static bool AcssProductEdit
        {
            get { return blAcssProductEdit; }
            set { blAcssProductEdit = value; }
        }
        public static bool AcssProductDelete
        {
            get { return blAcssProductDelete; }
            set { blAcssProductDelete = value; }
        }
        public static bool AcssProductPrint
        {
            get { return blAcssProductPrint; }
            set { blAcssProductPrint = value; }
        }
        public static bool AcssProductCost
        {
            get { return blAcssProductCost; }
            set { blAcssProductCost = value; }
        }
        public static bool AcssProductPrice
        {
            get { return blAcssProductPrice; }
            set { blAcssProductPrice = value; }
        }
        public static bool AcssProductOnHandQty
        {
            get { return blAcssProductOnHandQty; }
            set { blAcssProductOnHandQty = value; }
        }
        public static bool AcssDepartment
        {
            get { return blAcssDepartment; }
            set { blAcssDepartment = value; }
        }

        public static bool AcssCategory
        {
            get { return blAcssCategory; }
            set { blAcssCategory = value; }
        }
        public static bool AcssBrand
        {
            get { return blAcssBrand; }
            set { blAcssBrand = value; }
        }
        //Report Access
        public static bool AcssCustomerDtlRep
        {
            get { return blAcssCustomerDtlRep; }
            set { blAcssCustomerDtlRep = value; }
        }
        public static bool AcssCustomerGenRep
        {
            get { return blAcssCustomerGenRep; }
            set { blAcssCustomerGenRep = value; }
        }
        public static bool AcssVendorDtlRep
        {
            get { return blAcssVendorDtlRep; }
            set { blAcssVendorDtlRep = value; }
        }
        public static bool AcssVendorGenRep
        {
            get { return blAcssVendorGenRep; }
            set { blAcssVendorGenRep = value; }
        }
        public static bool AcssProductDtlRep
        {
            get { return blAcssProductDtlRep; }
            set { blAcssProductDtlRep = value; }
        }
        public static bool AcssProductGenRep
        {
            get { return blAcssProductGenRep; }
            set { blAcssProductGenRep = value; }
        }
        public static bool AcssProductKitRep
        {
            get { return blAcssProductKitRep; }
            set { blAcssProductKitRep = value; }
        }
        public static bool AcssProductMatxRep
        {
            get { return blAcssProductMatxRep; }
            set { blAcssProductMatxRep = value; }
        }
        public static bool AcssEmployeeDtlRep
        {
            get { return blAcssEmployeeDtlRep; }
            set { blAcssEmployeeDtlRep = value; }
        }
        public static bool AcssEmployeeAttnRep
        {
            get { return blAcssEmployeeAttnRep; }
            set { blAcssEmployeeAttnRep = value; }
        }
        public static bool AcssEmployeeLateRep
        {
            get { return blAcssEmployeeLateRep; }
            set { blAcssEmployeeLateRep = value; }
        }
        public static bool AcssEmployeeAbsentRep
        {
            get { return blAcssEmployeeAbsentRep; }
            set { blAcssEmployeeAbsentRep = value; }
        }
        public static bool AcssOrderingPORep
        {
            get { return blAcssOrderingPORep; }
            set { blAcssOrderingPORep = value; }
        }
        public static bool AcssOrderingRecvRep
        {
            get { return blAcssOrderingRecvRep; }
            set { blAcssOrderingRecvRep = value; }
        }
        public static bool AcssOrderingReorderRep
        {
            get { return blAcssOrderingReorderRep; }
            set { blAcssOrderingReorderRep = value; }
        }

        public static bool AcssSalesRep
        {
            get { return blAcssSalesRep; }
            set { blAcssSalesRep = value; }
        }

        public static bool AcssSalesSummaryRep
        {
            get { return blAcssSalesSummaryRep; }
            set { blAcssSalesSummaryRep = value; }
        }

        public static bool AcssCustomerOtherRep
        {
            get { return blAcssCustomerOtherRep; }
            set { blAcssCustomerOtherRep = value; }
        }


        public static bool AcssSalesCardTranRep
        {
            get { return blAcssSalesCardTranRep; }
            set { blAcssSalesCardTranRep = value; }
        }



        //Vendor Access
        public static bool AcssVendorAdd
        {
            get { return blAcssVendorAdd; }
            set { blAcssVendorAdd = value; }
        }
        public static bool AcssVendorEdit
        {
            get { return blAcssVendorEdit; }
            set { blAcssVendorEdit = value; }
        }
        public static bool AcssVendorDelete
        {
            get { return blAcssVendorDelete; }
            set { blAcssVendorDelete = value; }
        }
        public static bool AcssVendorPrint
        {
            get { return blAcssVendorPrint; }
            set { blAcssVendorPrint = value; }
        }

        //PO Access
        public static bool AcssPOAdd
        {
            get { return blAcssPOAdd; }
            set { blAcssPOAdd = value; }
        }
        public static bool AcssPOEdit
        {
            get { return blAcssPOEdit; }
            set { blAcssPOEdit = value; }
        }
        public static bool AcssPODelete
        {
            get { return blAcssPODelete; }
            set { blAcssPODelete = value; }
        }
        public static bool AcssPOPrint
        {
            get { return blAcssPOPrint; }
            set { blAcssPOPrint = value; }
        }

        //Receiv Accessing
        public static bool AcssRecvAdd
        {
            get { return blAcssRecvAdd; }
            set { blAcssRecvAdd = value; }
        }
        public static bool AcssRecvEdit
        {
            get { return blAcssRecvEdit; }
            set { blAcssRecvEdit = value; }
        }
        public static bool AcssRecvDelete
        {
            get { return blAcssRecvDelete; }
            set { blAcssRecvDelete = value; }
        }
        public static bool AcssRecvPrint
        {
            get { return blAcssRecvPrint; }
            set { blAcssRecvPrint = value; }
        }

        //Other Accessing
        public static bool AcssScale
        {
            get { return blAcssScale; }
            set { blAcssScale = value; }
        }

        public static bool AcssScalePrint
        {
            get { return blAcssScalePrint; }
            set { blAcssScalePrint = value; }
        }

        public static bool AcssScaleSetup
        {
            get { return blAcssScaleSetup; }
            set { blAcssScaleSetup = value; }
        }

        //Setup Accessing
        public static bool AcssSetup
        {
            get { return blAcssSetup; }
            set { blAcssSetup = value; }
        }

        public static bool AcssReportSchd
        {
            get { return blAcssReportSchd; }
            set { blAcssReportSchd = value; }
        }

        public static bool AcssGwareHostSetup
        {
            get { return blAcssGwareHostSetup; }
            set { blAcssGwareHostSetup = value; }
        }

        public static bool Acss3rdPartyIntegration
        {
            get { return blAcss3rdPartyIntegration; }
            set { blAcss3rdPartyIntegration = value; }
        }

        public static bool AcssScaleSettings
        {
            get { return blAcssScaleSettings; }
            set { blAcssScaleSettings = value; }
        }

        // POS Access  
        public static bool AcssPOSCustomer
        {
            get { return blAcssPOSCustomer; }
            set { blAcssPOSCustomer = value; }
        }
        public static bool AcssPOSProduct
        {
            get { return blAcssPOSProduct; }
            set { blAcssPOSProduct = value; }
        }
        public static bool AcssPOSReport
        {
            get { return blAcssPOSReport; }
            set { blAcssPOSReport = value; }
        }
        public static bool AcssPOSSetup
        {
            get { return blAcssPOSSetup; }
            set { blAcssPOSSetup = value; }
        }

        public static bool AcssPOSPaidout
        {
            get { return blAcssPOSPaidout; }
            set { blAcssPOSPaidout = value; }
        }
        public static bool AcssPOSNosale
        {
            get { return blAcssPOSNosale; }
            set { blAcssPOSNosale = value; }
        }
        public static bool AcssPOSAcctpay
        {
            get { return blAcssPOSAcctpay; }
            set { blAcssPOSAcctpay = value; }
        }
        public static bool AcssPOSGiftCert
        {
            get { return blAcssPOSGiftCert; }
            set { blAcssPOSGiftCert = value; }
        }
        public static bool AcssPOSSuspend
        {
            get { return blAcssPOSSuspend; }
            set { blAcssPOSSuspend = value; }
        }
        public static bool AcssPOSVoid
        {
            get { return blAcssPOSVoid; }
            set { blAcssPOSVoid = value; }
        }
        public static bool AcssPOSReturnReprint
        {
            get { return blAcssPOSReturnReprint; }
            set { blAcssPOSReturnReprint = value; }
        }
        public static bool AcssPOSCancel
        {
            get { return blAcssPOSCancel; }
            set { blAcssPOSCancel = value; }
        }
        public static bool AcssPOSItemEdit
        {
            get { return blAcssPOSItemEdit; }
            set { blAcssPOSItemEdit = value; }
        }

        public static bool AcssPOSTenderAllowType
        {
            get { return blAcssPOSTenderAllowType; }
            set { blAcssPOSTenderAllowType = value; }
        }
        public static bool AcssPOSTenderExit
        {
            get { return blAcssPOSTenderExit; }
            set { blAcssPOSTenderExit = value; }
        }
        public static bool AcssPOSTender
        {
            get { return blAcssPOSTender; }
            set { blAcssPOSTender = value; }
        }

        public static bool AcssPOSTenderDiscount
        {
            get { return blAcssPOSTenderDiscount; }
            set { blAcssPOSTenderDiscount = value; }
        }

        public static bool AcssPOSLayawayTran
        {
            get { return blAcssPOSLayawayTran; }
            set { blAcssPOSLayawayTran = value; }
        }
        public static bool AcssPOSLayawayRefund
        {
            get { return blAcssPOSLayawayRefund; }
            set { blAcssPOSLayawayRefund = value; }
        }
        public static bool AcssPOSLayawayMinimumDeposit
        {
            get { return blAcssPOSLayawayMinimumDeposit; }
            set { blAcssPOSLayawayMinimumDeposit = value; }
        }

        public static bool AcssPOSFuncSelectFunctions
        {
            get { return blAcssPOSFuncSelectFunctions; }
            set { blAcssPOSFuncSelectFunctions = value; }
        }
        public static bool AcssPOSFuncUsePriceLevel
        {
            get { return blAcssPOSFuncUsePriceLevel; }
            set { blAcssPOSFuncUsePriceLevel = value; }
        }
        public static bool AcssPOSFuncChangePrice
        {
            get { return blAcssPOSFuncChangePrice; }
            set { blAcssPOSFuncChangePrice = value; }
        }
        public static bool AcssOpenCashDrawer
        {
            get { return blAcssOpenCashDrawer; }
            set { blAcssOpenCashDrawer = value; }
        }

        public static bool AcssNegativeCardTransaction
        {
            get { return blAcssNegativeCardTransaction; }
            set { blAcssNegativeCardTransaction = value; }
        }

        #endregion

        #region Assign Security Access 

        // Get All Events that needs access permission
        private static DataTable FetchAllPermission()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " Select SecurityCode From SecurityPermission ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SecurityCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityCheck", System.Type.GetType("System.Boolean"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["SecurityCode"].ToString(),
                                                   false});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQlComm.Dispose();
                return null;
            }
            finally
            {
                dtbl.Dispose();
            }
        }

        // Get access permissions of events for a security profile ( ie. access for clerk / manager )
        private static DataTable FetchGroupPermission()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " Select SecurityCode, PermissionFlag From GroupPermission a left outer join Employee b on a.GroupID = b.ProfileID where b.ID = @ID ";
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = SystemVariables.ConnectionString;
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = SystemVariables.CurrentUserID;
            SqlDataReader objSQLReader = null;
            try
            {
                if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }


                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SecurityCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityCheck", System.Type.GetType("System.Boolean"));

                while (objSQLReader.Read())
                {
                    bool chk = false;
                    if (objSQLReader["PermissionFlag"].ToString() == "Y")
                    {
                        chk = true;
                    }
                    else
                    {
                        chk = false;
                    }

                    dtbl.Rows.Add(new object[] { objSQLReader["SecurityCode"].ToString(), chk });
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                Connection.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
            finally
            {
                dtbl.Dispose();
            }
        }

        // Assign Access permission for logged in user
        public static void LoadSecurityPermissionVariables()
        {
            DataTable dtbl = FetchAllPermission();
            DataTable dtbl1 = FetchGroupPermission();
            foreach (DataRow dr in dtbl.Rows)
            {
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    if (dr["SecurityCode"].ToString() == dr1["SecurityCode"].ToString())
                    {
                        dr["SecurityCheck"] = Convert.ToBoolean(dr1["SecurityCheck"].ToString());
                    }
                }
            }

            foreach (DataRow drF in dtbl.Rows)
            {
                if (drF["SecurityCode"].ToString() == "1a") blAccessCustomerScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1b") blAccessProductScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1c") blAccessReportsScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1d") blAccessVendorScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1e") blAccessReorderReportScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1f") blAccessPurchaseOrderScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1g") blAccessReceivingScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1h") blAccessPrintLabelScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1i") blAccessEmployeeScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1j") blAccessShiftScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1k") blAccessHolidayScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1l") blAccessTaxScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1m") blAccessTenderTypeScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1n") blAccessSecurityScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1o") blAccessDepartmentScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1p") blAccessCategoryScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1q") blAccessScales = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1r") blAccessGroupScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1s") blAccessClassScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1t") blAccessCRMParameterScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1u") blAccessBrandScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1v") blAccessBreakPackScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1w") blAccessStockTakeScreen = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1x") blAccessCentralOffice = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1xxd") blAcssDiscount = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "1z1") blAccessDashboard = Convert.ToBoolean(drF["SecurityCheck"].ToString());



                // Customer Access
                if (drF["SecurityCode"].ToString() == "11a") blAcssCustomerAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11b") blAcssCustomerEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11c") blAcssCustomerDelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11d") blAcssCustomerPrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11e") blAcssCustomerNotes = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11f") blAcssCustomerPriceLevel = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11g") blAcssCustomerShipAddress = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11h") blAcssCustomerTaxExempt = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11i") blAcssCustomerIssueCredit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11j") blAcssGroup = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11k") blAcssClass = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11l") blAcssStoreCredit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "11m") blAcssHouseAcountAdjust = Convert.ToBoolean(drF["SecurityCheck"].ToString());


                // Employee Access
                if (drF["SecurityCode"].ToString() == "12a") blAcssEmployeeAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "12b") blAcssEmployeeEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "12c") blAcssEmployeeDelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "12d") blAcssEmployeePrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "12e") blAcssEmployeePassword = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "12f") blAcssEmployeeAttendanceUpdate = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Product Access
                if (drF["SecurityCode"].ToString() == "13a") blAcssProductAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13b") blAcssProductEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13c") blAcssProductDelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13d") blAcssProductPrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13e") blAcssProductCost = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13f") blAcssProductPrice = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13g") blAcssProductOnHandQty = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13h") blAcssDepartment = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13i") blAcssCategory = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "13j") blAcssBrand = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Report Access
                if (drF["SecurityCode"].ToString() == "14a") blAcssCustomerDtlRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14b") blAcssCustomerGenRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14c") blAcssVendorDtlRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14d") blAcssVendorGenRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14e") blAcssProductDtlRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14f") blAcssProductGenRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14g") blAcssProductKitRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14h") blAcssProductMatxRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14i") blAcssEmployeeDtlRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14j") blAcssEmployeeAttnRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14k") blAcssEmployeeLateRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14l") blAcssEmployeeAbsentRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14m") blAcssOrderingPORep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14n") blAcssOrderingRecvRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14o") blAcssOrderingReorderRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14p") blAcssSalesRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14q") blAcssSalesSummaryRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14r") blAcssCustomerOtherRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14x") blAcssSalesCardTranRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "14o1") blAcssMinimumOrderRep = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Vendor Access
                if (drF["SecurityCode"].ToString() == "16a") blAcssVendorAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "16b") blAcssVendorEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "16c") blAcssVendorDelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "16d") blAcssVendorPrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                //  PO Access
                if (drF["SecurityCode"].ToString() == "17a") blAcssPOAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "17b") blAcssPOEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "17c") blAcssPODelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "17d") blAcssPOPrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Recv Access
                if (drF["SecurityCode"].ToString() == "18a") blAcssRecvAdd = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "18b") blAcssRecvEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "18c") blAcssRecvDelete = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "18d") blAcssRecvPrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Other Access
                if (drF["SecurityCode"].ToString() == "15a") blAcssScale = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15b") blAcssScalePrint = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15c") blAcssScaleSetup = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                if (drF["SecurityCode"].ToString() == "15s1") blAcssScale_ChangeUnitPrice = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15s2") blAcssScale_ChangeTare = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15s3") blAcssScale_ChangeByCount = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15s4") blAcssScale_ChangeProductLife = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15s5") blAcssScale_ChangeShelfLife = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15e") blAcssMarkdownSettings = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "15s6") blAcssScale_ManualWeightForNoScale = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Setup Access
                if (drF["SecurityCode"].ToString() == "20a") blAcssSetup = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "20c") blAcssDataPurging = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "20d") blAcssReportSchd = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                if (drF["SecurityCode"].ToString() == "20e") blAcssGwareHostSetup = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "20f") blAcss3rdPartyIntegration = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "20g") blAcssScaleSettings = Convert.ToBoolean(drF["SecurityCheck"].ToString());


                //  Closeout Access
                if (drF["SecurityCode"].ToString() == "21a") blAccessBlindDrop = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "21b") blAccessBlindCount = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "21c") blAccessReconcileCount = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "21d") blAccessOtherTerminalCloseout = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "21e") blAccessCashFloat = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "21f") blAccessCashInOut = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // POS Access

                if (drF["SecurityCode"].ToString() == "31a") blAcssPOSCustomer = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31b") blAcssPOSProduct = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31c") blAcssPOSReport = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31d") blAcssPOSSetup = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                if (drF["SecurityCode"].ToString() == "31e") blAcssPOSPaidout = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31f") blAcssPOSAcctpay = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31g") blAcssPOSGiftCert = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31h") blAcssPOSVoid = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31i") blAcssPOSReturnReprint = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31j") blAcssPOSSuspend = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31k") blAcssPOSCancel = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31l") blAcssPOSItemEdit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31m") blAcssPOSNosale = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31p") blAcssPOSTenderAllowType = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31s") blAcssPOSTender = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31q") blAcssPOSTenderExit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31r") blAcssPOSTenderDiscount = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31u") blAcssPOSLayawayTran = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31v") blAcssPOSLayawayRefund = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31w") blAcssPOSLayawayMinimumDeposit = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31z1") blAcssPOSFuncSelectFunctions = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31z2") blAcssPOSFuncUsePriceLevel = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31z3") blAcssPOSFuncChangePrice = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31xx1") blAcssOpenCashDrawer = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "31xx2") blAcssNegativeCardTransaction = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                // Labels and Signs
                if (drF["SecurityCode"].ToString() == "41a") blAccessLabelFormat = Convert.ToBoolean(drF["SecurityCheck"].ToString());

                //Tablet
                if (drF["SecurityCode"].ToString() == "41b") blAccessShelfTag = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "41c") blAccessProductionList = Convert.ToBoolean(drF["SecurityCheck"].ToString());
                if (drF["SecurityCode"].ToString() == "51a") blAccessMarkdown = Convert.ToBoolean(drF["SecurityCheck"].ToString());

            }
            dtbl.Dispose();
            dtbl1.Dispose();
        }

        #endregion

    }
}
