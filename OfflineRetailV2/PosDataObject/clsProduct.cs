/*
 purpose : Data for Item
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Globalization;
namespace PosDataObject
{
    public class Product
    {
        #region definining private variables

        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;

        private SqlConnection sqlConn;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strSKU;
        private string strSKU2;
        private string strSKU3;
        private string strDescription;
        private string strBinLocation;
        private string strProductType;
        private string strPromptForPrice;
        private string strAddtoPOSScreen;
        private string strAddtoPOSCategoryScreen;
        private string strAddtoScaleScreen;
        private string strScaleBarCode;
        private string strPrimaryVendorID;
        private int    intDepartmentID;
        private int    intCategoryID;
        private int intBrandID;
        private string strPrintBarCode;
        private string strNoPriceOnLabel;
        private string strAux;
        private string strFoodStampEligible;
        private string strAllowZeroStock;
        private string strDisplayStockinPOS;

        private string strProductStatus;

        private int    intClassID;
        private int    intQtyToPrint;
        private int    intLabelType;
        private int    intMinimumAge;

        private double dblPriceA;
        private double dblPriceB;
        private double dblPriceC;
        private double dblLastCost;
        private double dblDiscountedCost;
        private double dblCost;
        private double dblQtyOnHand;
        private double dblQtyOnLayaway;
        private double dblReorderQty;
        private double dblNormalQty;
        private string strProductNotes;
        private string strNotes2;
        private int intPoints;

        private double dblTare;
        private double dblTare2;

        private int intLinkSKU;
        private double dblBreakPackRatio;

        private double dblRentalPerMinute;
        private double dblRentalPerHour;
        private double dblRentalPerHalfDay;
        private double dblRentalPerDay;
        private double dblRentalPerWeek;
        private double dblRentalPerMonth;
        private double dblRentalDeposit;
        private double dblRentalMinHour;
        private double dblRentalMinAmount;
        private string strRentalPrompt;

        private int intMinimumServiceTime;

        private double dblRepairCharge;
        private string strRepairPromptForCharge;
        private string strRepairPromptForTag;

        private int intChangedByAdmin;
        private bool blFunctionButtonAccess;

        private DataTable dblVendorDataTable;
        private DataTable dblTaxDataTable;
        private DataTable dblRentTaxDataTable;
        private DataTable dblRepairTaxDataTable;
        private DataTable dblProductModifierDataTable;
        private DataTable dblPrinterDataTable;
        private string strPhotoFilePath;

        private string strTerminalName;
        private bool blAddJournal;
        private bool blChangeInventory;
        // for Kit
        private int intKitID;
        private int intItemID;
        private int intItemCount;

        // for UOM
        private int intUOMProductID;
        private int intUOMPackageCount;
        private double dblUOMUnitPrice;
        private string strUOMDescription;
        private string strUOMIsDefault;

        // for Serialization
        private int intSerialHeaderID;
        private int intSerialProductID;
        private string strSerial1;
        private string strSerial2;
        private string strSerial3;
        private string strAllowPOSNew;

        // Settings Data
        private int intDecimalPlace;
        private int intProductDecimalPlace;

        private string strPOSBackground;
        private string strPOSScreenStyle;
        private string strPOSFontType;
        private string strPOSFontColor;
        private int intPOSFontSize;
        private string strIsBold;
        private string strIsItalics;
        private string strPOSScreenColor;

        private string strScaleBackground;
        private string strScaleScreenStyle;
        private string strScaleFontType;
        private string strScaleFontColor;
        private int intScaleFontSize;
        private string strScaleIsBold;
        private string strScaleIsItalics;
        private string strScaleScreenColor;

        private double dblJournalQty;
        private string strJournalType;
        private string strJournalSubType;
        private string strTaggedInInvoice;

        private string strCategory;

        private string strNonDiscountable;

        private string strUPC;
        private int intCaseQty;
        private string strSeason;
        private string strCaseUPC;
        private string strChangeProductType;
        private int intPrintLabelQty;

        private double dblPrevPriceA;

        private double dblSplitWeight;

        private string strUOM;
        private string strShopifyImageExportFlag;
        private string strImportFrom="";
        private string strBookerProductID = "";

        private DateTime dtExpiryDate;

        public DateTime ExpiryDate
        {
            get { return dtExpiryDate; }
            set { dtExpiryDate = value; }
        }

        // 03-12-2013    Reorder Items in POS – Retail 

        private bool blIsNewCategory;
        private int intItemDisplayOrder = 0;

        // 03-12-2013    Reorder Items in POS – Retail 

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

        private string strBookingExpFlag;

        public string BookingExpFlag
        {
            get { return strBookingExpFlag; }
            set { strBookingExpFlag = value; }
        }

        private byte[] bytProductPhoto; //change for wpf

        #endregion

        #region definining public variables

        public string UOM
        {
            get { return strUOM; }
            set { strUOM = value; }
        }

        public string ShopifyImageExportFlag
        {
            get { return strShopifyImageExportFlag; }
            set { strShopifyImageExportFlag = value; }
        }

        public string ImportFrom
        {
            get { return strImportFrom; }
            set { strImportFrom = value; }
        }
        public string BookerProductID
        {
            get { return strBookerProductID; }
            set { strBookerProductID = value; }
        }
        //change for wpf
        public byte[] ProductPhoto
        {
            get { return bytProductPhoto; }
            set { bytProductPhoto = value; }
        }

        // 19-10-2016

        public double SplitWeight
        {
            get { return dblSplitWeight; }
            set { dblSplitWeight = value; }
        }
        
        // 03-12-2013    Reorder Items in POS – Retail 

        public string ChangeProductType
        {
            get { return strChangeProductType; }
            set { strChangeProductType = value; }
        }

        public string NonDiscountable
        {
            get { return strNonDiscountable; }
            set { strNonDiscountable = value; }
        }

        public bool IsNewCategory
        {
            get { return blIsNewCategory; }
            set { blIsNewCategory = value; }
        }

        public double PrevPriceA
        {
            get { return dblPrevPriceA; }
            set { dblPrevPriceA = value; }
        }

        public int PrintLabelQty
        {
            get { return intPrintLabelQty; }
            set { intPrintLabelQty = value; }
        }

        public bool ChangeInventory
        {
            get { return blChangeInventory; }
            set { blChangeInventory = value; }
        }

        public string CategoryDesc
        {
            get { return strCategory; }
            set { strCategory = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public int MinimumServiceTime
        {
            get { return intMinimumServiceTime; }
            set { intMinimumServiceTime = value; }
        }

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public int ProductDecimalPlace
        {
            get { return intProductDecimalPlace; }
            set { intProductDecimalPlace = value; }
        }

        public double Tare
        {
            get { return dblTare; }
            set { dblTare = value; }
        }

        public double Tare2
        {
            get { return dblTare2; }
            set { dblTare2 = value; }
        }

        public double BreakPackRatio
        {
            get { return dblBreakPackRatio; }
            set { dblBreakPackRatio = value; }
        }

        public int LinkSKU
        {
            get { return intLinkSKU; }
            set { intLinkSKU = value; }
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
               
        public string SKU
        {
            get { return strSKU; }
            set { strSKU = value; }
        }

        public string SKU2
        {
            get { return strSKU2; }
            set { strSKU2 = value; }
        }

        public string SKU3
        {
            get { return strSKU3; }
            set { strSKU3 = value; }
        }

        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }

        public string BinLocation
        {
            get { return strBinLocation; }
            set { strBinLocation = value; }
        }

        public string ProductType
        {
            get { return strProductType; }
            set { strProductType = value; }
        }

        public string AddtoPOSScreen
        {
            get { return strAddtoPOSScreen; }
            set { strAddtoPOSScreen = value; }
        }

        public string AddtoPOSCategoryScreen
        {
            get { return strAddtoPOSCategoryScreen; }
            set { strAddtoPOSCategoryScreen = value; }
        }

        public string AddtoScaleScreen
        {
            get { return strAddtoScaleScreen; }
            set { strAddtoScaleScreen = value; }
        }

        public string ScaleBarCode
        {
            get { return strScaleBarCode; }
            set { strScaleBarCode = value; }
        }

        public string PromptForPrice
        {
            get { return strPromptForPrice; }
            set { strPromptForPrice = value; }
        }

        public string ProductStatus
        {
            get { return strProductStatus; }
            set { strProductStatus = value; }
        }

        public string PrimaryVendorID
        {
            get { return strPrimaryVendorID; }
            set { strPrimaryVendorID = value; }
        }

        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }

        public int CategoryID
        {
            get { return intCategoryID; }
            set { intCategoryID = value; }
        }

        public int BrandID
        {
            get { return intBrandID; }
            set { intBrandID = value; }
        }

        public string PrintBarCode
        {
            get { return strPrintBarCode; }
            set { strPrintBarCode = value; }
        }

        public string NoPriceOnLabel
        {
            get { return strNoPriceOnLabel; }
            set { strNoPriceOnLabel = value; }
        }

        public string Aux
        {
            get { return strAux; }
            set { strAux = value; }
        }

        public string FoodStampEligible
        {
            get { return strFoodStampEligible; }
            set { strFoodStampEligible = value; }
        }

        public string AllowZeroStock
        {
            get { return strAllowZeroStock; }
            set { strAllowZeroStock = value; }
        }

        public string DisplayStockinPOS
        {
            get { return strDisplayStockinPOS; }
            set { strDisplayStockinPOS = value; }
        }

        public int ClassID
        {
            get { return intClassID; }
            set { intClassID = value; }
        }

        public int QtyToPrint
        {
            get { return intQtyToPrint; }
            set { intQtyToPrint = value; }
        }

        public int LabelType
        {
            get { return intLabelType; }
            set { intLabelType = value; }
        }

        public int MinimumAge
        {
            get { return intMinimumAge; }
            set { intMinimumAge = value; }
        }

        public double PriceA
        {
            get { return dblPriceA; }
            set { dblPriceA = value; }
        }

        public double PriceB
        {
            get { return dblPriceB; }
            set { dblPriceB = value; }
        }

        public double PriceC
        {
            get { return dblPriceC; }
            set { dblPriceC = value; }
        }

        public double LastCost
        {
            get { return dblLastCost; }
            set { dblLastCost = value; }
        }

        public double DiscountedCost
        {
            get { return dblDiscountedCost; }
            set { dblDiscountedCost = value; }
        }

        public double Cost
        {
            get { return dblCost; }
            set { dblCost = value; }
        }

        public double QtyOnHand
        {
            get { return dblQtyOnHand; }
            set { dblQtyOnHand = value; }
        }

        public double QtyOnLayaway
        {
            get { return dblQtyOnLayaway; }
            set { dblQtyOnLayaway = value; }
        }

        public double ReorderQty
        {
            get { return dblReorderQty; }
            set { dblReorderQty = value; }
        }

        public double NormalQty
        {
            get { return dblNormalQty; }
            set { dblNormalQty = value; }
        }

        public string ProductNotes
        {
            get { return strProductNotes; }
            set { strProductNotes = value; }
        }

        public string Notes2
        {
            get { return strNotes2; }
            set { strNotes2 = value; }
        }

        public DataTable VendorDataTable
        {
            get { return dblVendorDataTable; }
            set { dblVendorDataTable = value; }
        }

        public DataTable TaxDataTable
        {
            get { return dblTaxDataTable; }
            set { dblTaxDataTable = value; }
        }

        public DataTable ProductModifierDataTable
        {
            get { return dblProductModifierDataTable; }
            set { dblProductModifierDataTable = value; }
        }

        public DataTable PrinterDataTable
        {
            get { return dblPrinterDataTable; }
            set { dblPrinterDataTable = value; }
        }

        public DataTable RentTaxDataTable
        {
            get { return dblRentTaxDataTable; }
            set { dblRentTaxDataTable = value; }
        }

        public DataTable RepairTaxDataTable
        {
            get { return dblRepairTaxDataTable; }
            set { dblRepairTaxDataTable = value; }
        }

        public string PhotoFilePath
        {
            get { return strPhotoFilePath; }
            set { strPhotoFilePath = value; }
        }

        public int Points
        {
            get { return intPoints; }
            set { intPoints = value; }
        }

        // for Kit
        
        public int KitID
        {
            get { return intKitID; }
            set { intKitID = value; }
        }

        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }

        public int ItemCount
        {
            get { return intItemCount; }
            set { intItemCount = value; }
        }

        // for UOM

        public int UOMProductID
        {
            get { return intUOMProductID; }
            set { intUOMProductID = value; }
        }

        public int UOMPackageCount
        {
            get { return intUOMPackageCount; }
            set { intUOMPackageCount = value; }
        }

        public double UOMUnitPrice
        {
            get { return dblUOMUnitPrice; }
            set { dblUOMUnitPrice = value; }
        }

        public string UOMDescription
        {
            get { return strUOMDescription; }
            set { strUOMDescription = value; }
        }

        public string UOMIsDefault
        {
            get { return strUOMIsDefault; }
            set { strUOMIsDefault = value; }
        }

        // for Serialization

        public int SerialHeaderID
        {
            get { return intSerialHeaderID; }
            set { intSerialHeaderID = value; }
        }

        public int SerialProductID
        {
            get { return intSerialProductID; }
            set { intSerialProductID = value; }
        }

        public string Serial1
        {
            get { return strSerial1; }
            set { strSerial1 = value; }
        }

        public string Serial2
        {
            get { return strSerial2; }
            set { strSerial2 = value; }
        }

        public string Serial3
        {
            get { return strSerial3; }
            set { strSerial3 = value; }
        }

        public string AllowPOSNew
        {
            get { return strAllowPOSNew; }
            set { strAllowPOSNew = value; }
        }

        public string POSScreenColor
        {
            get { return strPOSScreenColor; }
            set { strPOSScreenColor = value; }
        }

        public string POSScreenStyle
        {
            get { return strPOSScreenStyle; }
            set { strPOSScreenStyle = value; }
        }

        public string POSBackground
        {
            get { return strPOSBackground; }
            set { strPOSBackground = value; }
        }

        public string POSFontType
        {
            get { return strPOSFontType; }
            set { strPOSFontType = value; }
        }

        public int POSFontSize
        {
            get { return intPOSFontSize; }
            set { intPOSFontSize = value; }
        }

        public string POSFontColor
        {
            get { return strPOSFontColor; }
            set { strPOSFontColor = value; }
        }

        public string IsBold
        {
            get { return strIsBold; }
            set { strIsBold = value; }
        }

        public string IsItalics
        {
            get { return strIsItalics; }
            set { strIsItalics = value; }
        }


        public string ScaleScreenColor
        {
            get { return strScaleScreenColor; }
            set { strScaleScreenColor = value; }
        }

        public string ScaleScreenStyle
        {
            get { return strScaleScreenStyle; }
            set { strScaleScreenStyle = value; }
        }

        public string ScaleBackground
        {
            get { return strScaleBackground; }
            set { strScaleBackground = value; }
        }

        public string ScaleFontType
        {
            get { return strScaleFontType; }
            set { strScaleFontType = value; }
        }

        public int ScaleFontSize
        {
            get { return intScaleFontSize; }
            set { intScaleFontSize = value; }
        }

        public string ScaleFontColor
        {
            get { return strScaleFontColor; }
            set { strScaleFontColor = value; }
        }

        public string ScaleIsBold
        {
            get { return strScaleIsBold; }
            set { strScaleIsBold = value; }
        }

        public string ScaleIsItalics
        {
            get { return strScaleIsItalics; }
            set { strScaleIsItalics = value; }
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

        public string TerminalName
        {
            get { return strTerminalName; }
            set { strTerminalName = value; }
        }

        public bool AddJournal
        {
            get { return blAddJournal; }
            set { blAddJournal = value; }
        }

        public double JournalQty
        {
            get { return dblJournalQty; }
            set { dblJournalQty = value; }
        }

        public string JournalType
        {
            get { return strJournalType; }
            set { strJournalType = value; }
        }

        public string JournalSubType
        {
            get { return strJournalSubType; }
            set { strJournalSubType = value; }
        }

        public string TaggedInInvoice
        {
            get { return strTaggedInInvoice; }
            set { strTaggedInInvoice = value; }
        }

        public string Season
        {
            get { return strSeason; }
            set { strSeason = value; }
        }

        public string CaseUPC
        {
            get { return strCaseUPC; }
            set { strCaseUPC = value; }
        }

        public int CaseQty
        {
            get { return intCaseQty; }
            set { intCaseQty = value; }
        }

        public string UPC
        {
            get { return strUPC; }
            set { strUPC = value; }
        }

        public double RentalPerMinute
        {
            get { return dblRentalPerMinute; }
            set { dblRentalPerMinute = value; }
        }

        public double RentalPerHour
        {
            get { return dblRentalPerHour; }
            set { dblRentalPerHour = value; }
        }

        public double RentalPerHalfDay
        {
            get { return dblRentalPerHalfDay; }
            set { dblRentalPerHalfDay = value; }
        }

        public double RentalPerDay
        {
            get { return dblRentalPerDay; }
            set { dblRentalPerDay = value; }
        }

        public double RentalPerWeek
        {
            get { return dblRentalPerWeek; }
            set { dblRentalPerWeek = value; }
        }

        public double RentalPerMonth
        {
            get { return dblRentalPerMonth; }
            set { dblRentalPerMonth = value; }
        }

        public double RentalDeposit
        {
            get { return dblRentalDeposit; }
            set { dblRentalDeposit = value; }
        }

        public double RentalMinHour
        {
            get { return dblRentalMinHour; }
            set { dblRentalMinHour = value; }
        }

        public double RentalMinAmount
        {
            get { return dblRentalMinAmount; }
            set { dblRentalMinAmount = value; }
        }

        public double RepairCharge
        {
            get { return dblRepairCharge; }
            set { dblRepairCharge = value; }
        }

        public string RentalPrompt
        {
            get { return strRentalPrompt; }
            set { strRentalPrompt = value; }
        }

        public string RepairPromptForCharge
        {
            get { return strRepairPromptForCharge; }
            set { strRepairPromptForCharge = value; }
        }

        public string RepairPromptForTag
        {
            get { return strRepairPromptForTag; }
            set { strRepairPromptForTag = value; }
        }

        #endregion

        #region Insert Data

        public string InsertData(SqlCommand objSQlComm)
        {
            byte[] Photo = GetPhoto(strPhotoFilePath);
            string strSQLComm = "";
            if (strPhotoFilePath == "")
            {
                strSQLComm = " insert into Product( SKU,SKU2,SKU3,Description,BinLocation,ProductType, "
                           + " PriceA,PriceB,PriceC,PromptForPrice,AddtoPOSScreen,ScaleBarCode,LastCost,Cost,"
                           + " QtyOnHand,QtyOnLayaway,ReorderQty,NormalQty,PrimaryVendorID,DepartmentID,CategoryID,"
                           + " PrintBarCode,NoPriceOnLabel,MinimumAge,QtyToPrint,LabelType,FoodStampEligible, "
                           + " ProductNotes,Points,AllowZeroStock,DisplayStockinPOS,"
                           + " POSBackground,POSScreenStyle,POSScreenColor,POSFontType,POSFontSize,POSFontColor,IsBold,IsItalics,"
                           + " ScaleBackground,ScaleScreenStyle,ScaleScreenColor,ScaleFontType,ScaleFontSize,ScaleFontColor,ScaleIsBold,ScaleIsItalics,"
                           + " DecimalPlace,ProductStatus,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,"
                           + " TaggedInInvoice,BrandID,Season,CaseUPC,CaseQty,UPC,LinkSKU,BreakPackRatio,"
                           + " RentalPerMinute,RentalPerHour,RentalPerHalfDay,RentalPerDay,RentalPerWeek,RentalPerMonth,"
                           + " RentalDeposit,MinimumServiceTime,RepairCharge,RentalMinHour,RentalMinAmount,RentalPrompt,RepairPromptForCharge,"
                           + " RepairPromptForTag,ExpFlag,POSDisplayOrder,NonDiscountable,Tare,AddToScaleScreen,Notes2,Tare2,SplitWeight,AddToPOSCategoryScreen )"

                           + " values ( @SKU,@SKU2,@SKU3,@Description,@BinLocation,@ProductType, "
                           + " @PriceA,@PriceB,@PriceC,@PromptForPrice,@AddtoPOSScreen,@ScaleBarCode,@LastCost,@Cost,"
                           + " @QtyOnHand,@QtyOnLayaway,@ReorderQty,@NormalQty,@PrimaryVendorID,@DepartmentID,@CategoryID,"
                           + " @PrintBarCode,@NoPriceOnLabel,@MinimumAge,@QtyToPrint,@LabelType,@FoodStampEligible, "
                           + " @ProductNotes,@Points,@AllowZeroStock,@DisplayStockinPOS,"
                           + " @POSBackground, @POSScreenStyle, @POSScreenColor,@POSFontType,@POSFontSize,@POSFontColor,@IsBold,@IsItalics,"
                           + " @ScaleBackground, @ScaleScreenStyle, @ScaleScreenColor,@ScaleFontType,@ScaleFontSize,@ScaleFontColor,@ScaleIsBold,@ScaleIsItalics,"
                           + " @DecimalPlace,@ProductStatus,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,"
                           + " @TaggedInInvoice,@BrandID,@Season,@CaseUPC,@CaseQty,@UPC,@LinkSKU,@BreakPackRatio, "
                           + " @RentalPerMinute,@RentalPerHour,@RentalPerHalfDay,@RentalPerDay,@RentalPerWeek,@RentalPerMonth,"
                           + " @RentalDeposit,@MinimumServiceTime,@RepairCharge,@RentalMinHour,@RentalMinAmount,@RentalPrompt,@RepairPromptForCharge,"
                           + " @RepairPromptForTag,'N',@POSDisplayOrder,@NonDiscountable,@Tare,@AddToScaleScreen,@Notes2,@Tare2,@SplitWeight,@AddToPOSCategoryScreen )"
                           + " select @@IDENTITY as ID ";
            }
            else
            {
                strSQLComm = " insert into Product( SKU,SKU2,SKU3,Description,BinLocation,ProductType,PriceA,PriceB,PriceC,"
                            + " PromptForPrice,AddtoPOSScreen,ScaleBarCode,LastCost,Cost,QtyOnHand,QtyOnLayaway,ReorderQty,"
                            + " NormalQty,PrimaryVendorID,DepartmentID,CategoryID,PrintBarCode,NoPriceOnLabel,MinimumAge, "
                            + " QtyToPrint,LabelType,FoodStampEligible,ProductNotes,ProductPhoto,Points,AllowZeroStock,DisplayStockinPOS,"
                            + " POSBackground,POSScreenStyle,POSScreenColor,POSFontType,POSFontSize,POSFontColor,IsBold,IsItalics,"
                            + " ScaleBackground,ScaleScreenStyle,ScaleScreenColor,ScaleFontType,ScaleFontSize,ScaleFontColor,ScaleIsBold,ScaleIsItalics,"
                            + " DecimalPlace,ProductStatus,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,TaggedInInvoice,"
                            + " BrandID,Season,CaseUPC,CaseQty,UPC,LinkSKU,BreakPackRatio, "
                            + " RentalPerMinute,RentalPerHour,RentalPerHalfDay,RentalPerDay,RentalPerWeek,RentalPerMonth,"
                            + " RentalDeposit,MinimumServiceTime,RepairCharge,RentalMinHour,RentalMinAmount,RentalPrompt,RepairPromptForCharge,"
                            + " RepairPromptForTag,ExpFlag,POSDisplayOrder,NonDiscountable,Tare,AddToScaleScreen,Notes2,Tare2,SplitWeight,AddToPOSCategoryScreen )"

                            + " values ( @SKU,@SKU2,@SKU3,@Description,@BinLocation,@ProductType,@PriceA,@PriceB,@PriceC,"
                            + " @PromptForPrice,@AddtoPOSScreen,@ScaleBarCode,@LastCost,@Cost,@QtyOnHand,@QtyOnLayaway,@ReorderQty,"
                            + " @NormalQty,@PrimaryVendorID,@DepartmentID,@CategoryID,@PrintBarCode,@NoPriceOnLabel,@MinimumAge,"
                            + " @QtyToPrint,@LabelType,@FoodStampEligible,@ProductNotes,@ProductPhoto,@Points,@AllowZeroStock,@DisplayStockinPOS,"
                            + " @POSBackground,@POSScreenStyle,@POSScreenColor,@POSFontType,@POSFontSize,@POSFontColor,@IsBold,@IsItalics,"
                            + " @ScaleBackground, @ScaleScreenStyle, @ScaleScreenColor,@ScaleFontType,@ScaleFontSize,@ScaleFontColor,@ScaleIsBold,@ScaleIsItalics,"
                            + " @DecimalPlace,@ProductStatus,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,"
                            + " @TaggedInInvoice,@BrandID,@Season,@CaseUPC,@CaseQty,@UPC,@LinkSKU,@BreakPackRatio, "
                            + " @RentalPerMinute,@RentalPerHour,@RentalPerHalfDay,@RentalPerDay,@RentalPerWeek,@RentalPerMonth,"
                            + " @RentalDeposit,@MinimumServiceTime,@RepairCharge,@RentalMinHour,@RentalMinAmount,@RentalPrompt,@RepairPromptForCharge,"
                            + " @RepairPromptForTag,'N',@POSDisplayOrder,@NonDiscountable,@Tare,@AddToScaleScreen,@Notes2,@Tare2,@SplitWeight,@AddToPOSCategoryScreen )"
                            + " select @@IDENTITY as ID ";
            }

            
            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@BinLocation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PromptForPrice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnLayaway", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ReorderQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NormalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrimaryVendorID", System.Data.SqlDbType.VarChar));// ????
                objSQlComm.Parameters.Add(new SqlParameter("@BrandID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductNotes", System.Data.SqlDbType.NVarChar));
                if (strPhotoFilePath != "")
                    objSQlComm.Parameters.Add(new SqlParameter("@ProductPhoto", System.Data.SqlDbType.Image));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@TaggedInInvoice", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Season", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@UPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BreakPackRatio", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMinute", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHalfDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerWeek", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMonth", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalDeposit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@MinimumServiceTime", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairCharge", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPrompt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForCharge", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare"].Value = dblTare;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare2"].Value = dblTare2;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToScaleScreen", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Notes2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@SKU2"].Value = strSKU2;
                objSQlComm.Parameters["@SKU3"].Value = strSKU3;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@BinLocation"].Value = strBinLocation;
                objSQlComm.Parameters["@ProductType"].Value = strProductType;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                objSQlComm.Parameters["@PromptForPrice"].Value = strPromptForPrice;
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = strAddtoPOSScreen;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@LastCost"].Value = dblLastCost;
                objSQlComm.Parameters["@Cost"].Value = dblCost;
                objSQlComm.Parameters["@QtyOnHand"].Value = dblQtyOnHand;
                objSQlComm.Parameters["@QtyOnLayaway"].Value = dblQtyOnLayaway;
                objSQlComm.Parameters["@ReorderQty"].Value = dblReorderQty;
                objSQlComm.Parameters["@NormalQty"].Value = dblNormalQty;
                objSQlComm.Parameters["@PrimaryVendorID"].Value = strPrimaryVendorID;
                objSQlComm.Parameters["@BrandID"].Value = intBrandID;
                objSQlComm.Parameters["@DepartmentID"].Value = intDepartmentID;
                objSQlComm.Parameters["@CategoryID"].Value = intCategoryID;
                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStampEligible;
                objSQlComm.Parameters["@ProductNotes"].Value = strProductNotes;

                objSQlComm.Parameters["@DecimalPlace"].Value = intProductDecimalPlace;

                if (strPhotoFilePath.Trim() != "")
                {
                    if (strPhotoFilePath.Trim() == "null")
                    {
                        objSQlComm.Parameters["@ProductPhoto"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@ProductPhoto"].Value = Photo;
                    }
                }

                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;

                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@ScaleScreenColor"].Value = strScaleScreenColor;
                objSQlComm.Parameters["@ScaleBackground"].Value = strScaleBackground;
                objSQlComm.Parameters["@ScaleScreenStyle"].Value = strScaleScreenStyle;
                objSQlComm.Parameters["@ScaleFontType"].Value = strScaleFontType;
                objSQlComm.Parameters["@ScaleFontSize"].Value = intScaleFontSize;
                objSQlComm.Parameters["@ScaleFontColor"].Value = strScaleFontColor;
                objSQlComm.Parameters["@ScaleIsBold"].Value = strScaleIsBold;
                objSQlComm.Parameters["@ScaleIsItalics"].Value = strScaleIsItalics;

                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@TaggedInInvoice"].Value = strTaggedInInvoice;

                objSQlComm.Parameters["@Season"].Value = strSeason;
                objSQlComm.Parameters["@CaseUPC"].Value = strCaseUPC;
                objSQlComm.Parameters["@CaseQty"].Value = intCaseQty;
                objSQlComm.Parameters["@UPC"].Value = strUPC;

                objSQlComm.Parameters["@LinkSKU"].Value = intLinkSKU;
                objSQlComm.Parameters["@BreakPackRatio"].Value = dblBreakPackRatio;

                objSQlComm.Parameters["@RentalPerMinute"].Value = dblRentalPerMinute;
                objSQlComm.Parameters["@RentalPerHour"].Value = dblRentalPerHour;
                objSQlComm.Parameters["@RentalPerHalfDay"].Value = dblRentalPerHalfDay;
                objSQlComm.Parameters["@RentalPerDay"].Value = dblRentalPerDay;
                objSQlComm.Parameters["@RentalPerWeek"].Value = dblRentalPerWeek;
                objSQlComm.Parameters["@RentalPerMonth"].Value = dblRentalPerMonth;
                objSQlComm.Parameters["@RentalDeposit"].Value = dblRentalDeposit;
                objSQlComm.Parameters["@RentalMinHour"].Value = dblRentalMinHour;
                objSQlComm.Parameters["@RentalMinAmount"].Value = dblRentalMinAmount;

                objSQlComm.Parameters["@MinimumServiceTime"].Value = intMinimumServiceTime;
                objSQlComm.Parameters["@RepairCharge"].Value = dblRepairCharge;
                
                objSQlComm.Parameters["@RentalPrompt"].Value = strRentalPrompt;
                objSQlComm.Parameters["@RepairPromptForCharge"].Value = strRepairPromptForCharge;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;

                objSQlComm.Parameters["@POSDisplayOrder"].Value = intItemDisplayOrder ;
                objSQlComm.Parameters["@AddToScaleScreen"].Value = strAddtoScaleScreen;

                objSQlComm.Parameters["@Notes2"].Value = strNotes2;

                objSQlComm.Parameters.Add(new SqlParameter("@SplitWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@SplitWeight"].Value = dblSplitWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSCategoryScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToPOSCategoryScreen"].Value = strAddtoPOSCategoryScreen;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string PostData()
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
                if (intID == 0)
                {
                    intItemDisplayOrder = GetItemDisplayOrder(SaveComm, intCategoryID);
                    strError = InsertData(SaveComm);
                    if (strError == "")
                    {
                        if (intLinkSKU > 0) strError = UpdateLinkSKU(SaveComm);
                    }
                }
                else
                {
                    // 03-12-2013    Reorder Items in POS – Retail 
                    if (blIsNewCategory)
                    {
                        intItemDisplayOrder = GetItemDisplayOrder(SaveComm, intCategoryID);
                        strError = UpdateItemDisplayOrder(SaveComm);
                    }

                    strError = UpdateData(SaveComm);

                    if (dblPriceA != dblPrevPriceA)
                    {
                        ScaleConnOnPriceChange(SaveComm);
                    }

                    if (strChangeProductType != "")
                    {
                        UpdateProductTypeInExistingRecord(SaveComm, "item", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                        UpdateProductTypeInExistingRecord(SaveComm, "suspnded", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                        UpdateProductTypeInExistingRecord(SaveComm, "workorder", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                    }
                }

                if (strError == "")
                    strError = DeleteVendorparts(SaveComm);
                if (strError == "")
                    strError = SaveVendorParts(SaveComm);

                if (strError == "")
                    DeleteTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveTaxParts(SaveComm);

                if (strError == "")
                    DeleteRentTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveRentTaxParts(SaveComm);

                if (strError == "")
                    DeleteRepairTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveRepairTaxParts(SaveComm);

                if (strError == "")
                    DeleteProductModifier(SaveComm);
                if (strError == "")
                    strError = SaveProductModifier(SaveComm);

                if (strError == "")
                    DeletePrinters(SaveComm);
                if (strError == "")
                    strError = SavePrinters(SaveComm);

                if (strError == "")
                {
                    if (blAddJournal)
                    {
                        if (intLinkSKU == 0)
                        {
                            int ret = Functions.AddStockJournal(SaveComm, strJournalType, strJournalSubType, "N", intID,
                                        intLoginUserID, dblJournalQty, dblCost, intID.ToString(), strTerminalName, DateTime.Now, DateTime.Now, "");
                            if (ret > 0) strError = "";
                            else strError = "error";
                        }
                    }
                }

                if (strError == "")
                {
                    if (blChangeInventory)
                    {
                        strError = UpdateExpFlagForInventoryChange(SaveComm);
                    }
                }

                if (strError == "")
                {
                    //if (strPrintBarCode == "Y")
                    //{
                        bool blIfExistInPrintLabel = IfExistInPrintLabel(SaveComm, intID);
                        if (blIfExistInPrintLabel)
                        {
                            UpdatePrintLabel(SaveComm);
                        }
                        else
                        {
                            if (intPrintLabelQty > 0) InsertPrintLabel(SaveComm);
                        }
                    //}
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

        #endregion

        // 03-12-2013    Reorder Items in POS – Retail 
        public DataTable FetchMatrixOptionForInvoice(int intpid)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,Option1Name,Option2Name,Option3Name from MatrixOptions where id = @pid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@pid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@pid"].Value = intpid;
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option3Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {  objSQLReader["ID"].ToString(),
                                                   objSQLReader["Option1Name"].ToString(),
                                                   objSQLReader["Option2Name"].ToString(),
                                                   objSQLReader["Option3Name"].ToString()
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
        public string UpdateItemDisplayOrder(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";

            strSQLComm = "sp_UpdateItemDisplayOrder";

            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters.Add(new SqlParameter("@NewGroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NewGroupID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@NewGroupID"].Value = intCategoryID;

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

        // 03-12-2013    Reorder Items in POS – Retail 

        public int GetItemDisplayOrder(SqlCommand objSQlComm, int grp)
        {
            int retval = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select isnull(Max(POSDisplayOrder),0) + 1 as RCNT from product where categoryid = @gparam ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@gparam", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@gparam"].Value = grp;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    retval = Functions.fnInt32(sqlDataReader["RCNT"].ToString());
                }
                sqlDataReader.Close();
                return retval;
            }
            catch (SqlException SQLDBException)
            {
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return 0;
            }
        }

        public bool IfExistInPrintLabel(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = " select count(*) as RC from PrintLabel where ProductID=@ITEMID ";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.Text;

            objCommand.Parameters.Add(new SqlParameter("@ITEMID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ITEMID"].Value = intID;

            try
            {
                SqlDataReader objSQLReader = null;
                objSQLReader = objCommand.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = Functions.fnInt32(objSQLReader["RC"].ToString());
                }
                objSQLReader.Close();
                if (intResult > 0) return true; else return false;
            }
            catch
            {
                return false;
            }
        }

        public void InsertPrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into PrintLabel ( ProductID,Qty,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " Values ( @ProductID,@Qty,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = intID;
                objSQlComm.Parameters["@Qty"].Value = intPrintLabelQty;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                sqlDataReader = objSQlComm.ExecuteReader();
                sqlDataReader.Close();
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
            }
        }

        private void UpdatePrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Update PrintLabel set Qty = Qty + @Qty where ProductID=@ID  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = intPrintLabelQty;
                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }
        
        #region Update Data

        public string UpdateData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            byte[] Photo = GetPhoto(strPhotoFilePath);
            string strSQLComm = "";
            if (strPhotoFilePath == "")
            {
                strSQLComm = " update Product set SKU=@SKU,SKU2=@SKU2,SKU3=@SKU3,Description=@Description, "
                           + " BinLocation=@BinLocation, AddtoPOSScreen=@AddtoPOSScreen, ScaleBarCode=@ScaleBarCode,"
                           + " ProductType=@ProductType,PriceA=@PriceA,PriceB=@PriceB,PriceC=@PriceC, "
                           + " PromptForPrice=@PromptForPrice,LastCost=@LastCost,Cost=@Cost,"
                           + " QtyOnHand=@QtyOnHand,QtyOnLayaway=@QtyOnLayaway,ReorderQty=@ReorderQty, "
                           + " NormalQty=@NormalQty,PrimaryVendorID=@PrimaryVendorID,BrandID=@BrandID, "
                           + " DepartmentID=@DepartmentID,CategoryID=@CategoryID, Points = @Points, "
                           + " AllowZeroStock=@AllowZeroStock,DisplayStockinPOS=@DisplayStockinPOS, "
                           + " PrintBarCode=@PrintBarCode,NoPriceOnLabel=@NoPriceOnLabel,MinimumAge=@MinimumAge, "
                           + " QtyToPrint=@QtyToPrint,LabelType=@LabelType,FoodStampEligible=@FoodStampEligible,ProductNotes=@ProductNotes,"
                           + " POSBackground=@POSBackground, POSScreenStyle=@POSScreenStyle,POSScreenColor=@POSScreenColor, "
                           + " POSFontType=@POSFontType,POSFontSize=@POSFontSize,TaggedInInvoice=@TaggedInInvoice, "
                           + " POSFontColor=@POSFontColor, IsBold=@IsBold, IsItalics=@IsItalics,ProductStatus=@ProductStatus,"
                           + " ScaleBackground=@ScaleBackground, ScaleScreenStyle=@ScaleScreenStyle,ScaleScreenColor=@ScaleScreenColor, "
                           + " ScaleFontType=@ScaleFontType,ScaleFontSize=@ScaleFontSize,ScaleFontColor=@ScaleFontColor,ScaleIsBold=@ScaleIsBold, ScaleIsItalics=@ScaleIsItalics,"
                           + " Season=@Season,CaseUPC=@CaseUPC,CaseQty=@CaseQty,UPC=@UPC,LinkSKU=@LinkSKU,BreakPackRatio=@BreakPackRatio," 
                           + " DecimalPlace=@DecimalPlace,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                           + " RentalPerMinute=@RentalPerMinute,RentalPerHour=@RentalPerHour,RentalPerDay=@RentalPerDay,"
                           + " RentalPerHalfDay=@RentalPerHalfDay,RentalPerWeek=@RentalPerWeek,RentalPerMonth=@RentalPerMonth,RentalDeposit=@RentalDeposit, "
                           + " MinimumServiceTime=@MinimumServiceTime,RepairCharge=@RepairCharge,"
                           + " RentalMinHour=@RentalMinHour,RentalMinAmount=@RentalMinAmount,"
                           + " RentalPrompt=@RentalPrompt,RepairPromptForCharge=@RepairPromptForCharge,Tare2=@Tare2,"
                           + " RepairPromptForTag=@RepairPromptForTag,NonDiscountable=@NonDiscountable,Tare=@Tare,AddToScaleScreen=@AddToScaleScreen,"
                           + " Notes2=@Notes2,SplitWeight=@SplitWeight, BookingExpFlag=@BookingExpFlag, AddToPOSCategoryScreen=@AddToPOSCategoryScreen where ID = @ID";
            }
            else
            {
                strSQLComm = " update Product set SKU=@SKU,SKU2=@SKU2,SKU3=@SKU3,Description=@Description, "
                           + " BinLocation=@BinLocation, AddtoPOSScreen=@AddtoPOSScreen, ScaleBarCode=@ScaleBarCode,"
                           + " ProductType=@ProductType,PriceA=@PriceA,PriceB=@PriceB,PriceC=@PriceC, "
                           + " PromptForPrice=@PromptForPrice,LastCost=@LastCost,Cost=@Cost,"
                           + " QtyOnHand=@QtyOnHand,QtyOnLayaway=@QtyOnLayaway,ReorderQty=@ReorderQty, "
                           + " NormalQty=@NormalQty,PrimaryVendorID=@PrimaryVendorID,BrandID=@BrandID, "
                           + " DepartmentID=@DepartmentID,CategoryID=@CategoryID, Points = @Points, "
                           + " AllowZeroStock=@AllowZeroStock,DisplayStockinPOS=@DisplayStockinPOS,"
                           + " PrintBarCode=@PrintBarCode,NoPriceOnLabel=@NoPriceOnLabel,MinimumAge=@MinimumAge, "
                           + " QtyToPrint=@QtyToPrint,LabelType=@LabelType,FoodStampEligible=@FoodStampEligible,ProductNotes=@ProductNotes,"
                           + " POSBackground=@POSBackground, POSScreenStyle=@POSScreenStyle,POSScreenColor=@POSScreenColor,"
                           + " POSFontType=@POSFontType,POSFontSize=@POSFontSize,TaggedInInvoice=@TaggedInInvoice,"
                           + " POSFontColor=@POSFontColor, IsBold=@IsBold, IsItalics=@IsItalics,ProductStatus=@ProductStatus,"
                           + " ScaleBackground=@ScaleBackground, ScaleScreenStyle=@ScaleScreenStyle,ScaleScreenColor=@ScaleScreenColor, "
                           + " ScaleFontType=@ScaleFontType,ScaleFontSize=@ScaleFontSize,ScaleFontColor=@ScaleFontColor,ScaleIsBold=@ScaleIsBold, ScaleIsItalics=@ScaleIsItalics,"
                           + " Season=@Season,CaseUPC=@CaseUPC,CaseQty=@CaseQty,UPC=@UPC,LinkSKU=@LinkSKU,BreakPackRatio=@BreakPackRatio," 
                           + " DecimalPlace=@DecimalPlace,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,ProductPhoto=@ProductPhoto,"
                           + " RentalPerMinute=@RentalPerMinute,RentalPerHour=@RentalPerHour,RentalPerDay=@RentalPerDay,RentalPerHalfDay=@RentalPerHalfDay,"
                           + " RentalPerWeek=@RentalPerWeek,RentalPerMonth=@RentalPerMonth,RentalDeposit=@RentalDeposit, "
                           + " MinimumServiceTime=@MinimumServiceTime,RepairCharge=@RepairCharge,"
                           + " RentalMinHour=@RentalMinHour,RentalMinAmount=@RentalMinAmount,"
                           + " RentalPrompt=@RentalPrompt,RepairPromptForCharge=@RepairPromptForCharge,Tare2=@Tare2,"
                           + " RepairPromptForTag=@RepairPromptForTag,NonDiscountable=@NonDiscountable,Tare=@Tare,AddToScaleScreen=@AddToScaleScreen,"
                           + " Notes2=@Notes2,SplitWeight=@SplitWeight, BookingExpFlag=@BookingExpFlag, AddToPOSCategoryScreen=@AddToPOSCategoryScreen where ID = @ID";
            }

            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@BinLocation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PromptForPrice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnLayaway", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ReorderQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NormalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrimaryVendorID", System.Data.SqlDbType.VarChar)); // ????
                objSQlComm.Parameters.Add(new SqlParameter("@BrandID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                if (strPhotoFilePath != "")
                    objSQlComm.Parameters.Add(new SqlParameter("@ProductPhoto", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@TaggedInInvoice", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Season", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@UPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BreakPackRatio", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMinute", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHalfDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerWeek", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMonth", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalDeposit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumServiceTime", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RepairCharge", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPrompt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForCharge", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare"].Value = dblTare;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare2"].Value = dblTare2;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToScaleScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToScaleScreen"].Value = strAddtoScaleScreen;

                objSQlComm.Parameters.Add(new SqlParameter("@Notes2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Notes2"].Value = strNotes2;

                objSQlComm.Parameters.Add(new SqlParameter("@SplitWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@SplitWeight"].Value = dblSplitWeight;

                //objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@SKU2"].Value = strSKU2;
                objSQlComm.Parameters["@SKU3"].Value = strSKU3;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@BinLocation"].Value = strBinLocation;
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = strAddtoPOSScreen;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@ProductType"].Value = strProductType;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                objSQlComm.Parameters["@PromptForPrice"].Value = strPromptForPrice;
                objSQlComm.Parameters["@LastCost"].Value = dblLastCost;
                objSQlComm.Parameters["@Cost"].Value = dblCost;
                objSQlComm.Parameters["@QtyOnHand"].Value = dblQtyOnHand;
                objSQlComm.Parameters["@QtyOnLayaway"].Value = dblQtyOnLayaway;
                objSQlComm.Parameters["@ReorderQty"].Value = dblReorderQty;
                objSQlComm.Parameters["@NormalQty"].Value = dblNormalQty;
                objSQlComm.Parameters["@PrimaryVendorID"].Value = strPrimaryVendorID;
                objSQlComm.Parameters["@BrandID"].Value = intBrandID;
                objSQlComm.Parameters["@DepartmentID"].Value = intDepartmentID;
                objSQlComm.Parameters["@CategoryID"].Value = intCategoryID;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@ProductNotes"].Value = ProductNotes;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStampEligible;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@ScaleScreenColor"].Value = strScaleScreenColor;
                objSQlComm.Parameters["@ScaleBackground"].Value = strScaleBackground;
                objSQlComm.Parameters["@ScaleScreenStyle"].Value = strScaleScreenStyle;
                objSQlComm.Parameters["@ScaleFontType"].Value = strScaleFontType;
                objSQlComm.Parameters["@ScaleFontSize"].Value = intScaleFontSize;
                objSQlComm.Parameters["@ScaleFontColor"].Value = strScaleFontColor;
                objSQlComm.Parameters["@ScaleIsBold"].Value = strScaleIsBold;
                objSQlComm.Parameters["@ScaleIsItalics"].Value = strScaleIsItalics;

                objSQlComm.Parameters["@DecimalPlace"].Value = intProductDecimalPlace;
                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;
                objSQlComm.Parameters["@TaggedInInvoice"].Value = strTaggedInInvoice;

                objSQlComm.Parameters["@Season"].Value = strSeason;
                objSQlComm.Parameters["@CaseUPC"].Value = strCaseUPC;
                objSQlComm.Parameters["@CaseQty"].Value = intCaseQty;
                objSQlComm.Parameters["@UPC"].Value = strUPC;
                objSQlComm.Parameters["@LinkSKU"].Value = intLinkSKU;
                objSQlComm.Parameters["@BreakPackRatio"].Value = dblBreakPackRatio;

                objSQlComm.Parameters["@RentalPerMinute"].Value = dblRentalPerMinute;
                objSQlComm.Parameters["@RentalPerHour"].Value = dblRentalPerHour;
                objSQlComm.Parameters["@RentalPerHalfDay"].Value = dblRentalPerHalfDay;
                objSQlComm.Parameters["@RentalPerDay"].Value = dblRentalPerDay;
                objSQlComm.Parameters["@RentalPerWeek"].Value = dblRentalPerWeek;
                objSQlComm.Parameters["@RentalPerMonth"].Value = dblRentalPerMonth;
                objSQlComm.Parameters["@RentalDeposit"].Value = dblRentalDeposit;
                objSQlComm.Parameters["@MinimumServiceTime"].Value = intMinimumServiceTime;
                objSQlComm.Parameters["@RepairCharge"].Value = dblRepairCharge;
                objSQlComm.Parameters["@RentalMinHour"].Value = dblRentalMinHour;
                objSQlComm.Parameters["@RentalMinAmount"].Value = dblRentalMinAmount;
                objSQlComm.Parameters["@RentalPrompt"].Value = strRentalPrompt;
                objSQlComm.Parameters["@RepairPromptForCharge"].Value = strRepairPromptForCharge;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;

                if (strPhotoFilePath.Trim() != "")
                {
                    if (strPhotoFilePath.Trim() == "null")
                    {
                        objSQlComm.Parameters["@ProductPhoto"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@ProductPhoto"].Value = Photo;
                    }
                }

                objSQlComm.Parameters.Add(new SqlParameter("@BookingExpFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@BookingExpFlag"].Value = strBookingExpFlag;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSCategoryScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToPOSCategoryScreen"].Value = strAddtoPOSCategoryScreen;

                //objSQlComm.Parameters["@POSDisplayOrder"].Value = intItemDisplayOrder;

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

        public string UpdateLinkSKU(SqlCommand objSQlComm)
        {
            string strSQLComm = " update product set linksku = -1 where ID = @ID ";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intLinkSKU;

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

        public string UpdateExpFlagForInventoryChange(SqlCommand objSQlComm)
        {
            string strSQLComm = " update Product set expflag = 'N' where ID = @ID";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;
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

        #region Save Vendor Parts

        public string UpdateVendorpart(SqlCommand objSQlComm, int VPID, int VendID, string Part, double pric, string Primary)
        {
            string strSQLComm = " update VendPart set ProductID=@ProductID, VendorID=@VendorID, PartNumber=@PartNumber, "
                                + " Price=@Price, IsPrimary=@IsPrimary,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PartNumber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@IsPrimary", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = VPID;
                objSQlComm.Parameters["@ProductID"].Value = intID;
                objSQlComm.Parameters["@VendorID"].Value = VendID;
                objSQlComm.Parameters["@PartNumber"].Value = Part;
                objSQlComm.Parameters["@Price"].Value = pric;
                objSQlComm.Parameters["@IsPrimary"].Value = Primary;
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

        public string UpdateProductTypeInExistingRecord(SqlCommand objSQlComm, string sTable, string pType_N, string pType_O)
        {
            string strSQLComm = " update " + sTable + "  set producttype = '" + pType_N + "' where ProductID = @ProductID and producttype = '" + pType_O + "'";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ProductID"].Value = intID;

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

        public string DeleteVendorparts(SqlCommand objSQlComm)
        {
            string strSQLComm = " Delete from VendPart Where ProductID = @ProductID";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ProductID"].Value = intID;

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

        public string InsertVendorpart(SqlCommand objSQlComm, int VendID, string Part, double pric, int qty, string Primary, double shrk)
        {
            string strSQLComm = " insert into VendPart(ProductID, VendorID, PartNumber, Price,PackQty, IsPrimary, Shrink, "
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @ProductID, @VendorID, @PartNumber, @Price,@PackQty, @IsPrimary, @Shrink, "
                                + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PartNumber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PackQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IsPrimary", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@Shrink", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@ProductID"].Value = intID;
                objSQlComm.Parameters["@VendorID"].Value = VendID;
                objSQlComm.Parameters["@PartNumber"].Value = Part;
                objSQlComm.Parameters["@Price"].Value = pric;
                objSQlComm.Parameters["@PackQty"].Value = qty;
                objSQlComm.Parameters["@IsPrimary"].Value = Primary;
                objSQlComm.Parameters["@Shrink"].Value = shrk;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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

        public string SaveVendorParts(SqlCommand objSQlComm)
        {
            if (dblVendorDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblVendorDataTable.Rows)
            {
                if (dr["VendorID"].ToString() == "") continue;
                int intVPID = 0;
                if (dr["ID"].ToString() != "")
                    intVPID = Functions.fnInt32(dr["ID"].ToString());
                int intVendID = Functions.fnInt32(dr["VendorID"].ToString());
                string strPart = dr["PartNumber"].ToString();
                double dblpric = 0.00;
                if (dr["Price"].ToString() != "")
                    dblpric = Functions.fnDouble(dr["Price"].ToString());
                string strPrimary = dr["IsPrimary"].ToString();

                int intPackQty = 1;
                if (dr["PackQty"].ToString() != "")
                    intPackQty = Functions.fnInt32(dr["PackQty"].ToString());

                if (strPrimary.ToUpper() == "TRUE")
                {
                    strPrimary = "Y";
                }
                else
                {
                    strPrimary = "N";
                }
                double shrkperc = 0;
                if (strProductType == "W")
                {
                    if (dr["Shrink"].ToString() != "")
                        shrkperc = Functions.fnDouble(dr["Shrink"].ToString());
                }
                else
                {
                    shrkperc = 0;
                }
                string strNewOld = dr["NewOld"].ToString();

                if (strResult == "")
                {
                    strResult = InsertVendorpart(objSQlComm, intVendID, strPart, dblpric, intPackQty, strPrimary, shrkperc);
                }
            }
            return strResult;
        }

        #endregion

        #region Fetchdata

        public DataTable FetchBreakPackData(bool blIsPOS, string sts)
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            if (sts == "Active Break packs")
            {
                SQLFilter = " and P.ProductStatus = 'Y' ";
            }
            if (sts == "Inactive Break packs")
            {
                SQLFilter = " and P.ProductStatus = 'N' ";
            }
            string strSQLComm = " select P.ID,P.SKU,P.Description as ProductName,P.PriceA,P.QtyOnHand,P.ProductType,P.ProductStatus,P.UPC,P.Season,"
                                + " P.BreakPackRatio,L.SKU as LikSKU, L.Description as LikDescription,P.LinkSKU as LinkID, "
                                + " D.Description as Department,CAT.Description as Category,BRAD.BrandDescription as Brand,P.DisplayStockinPOS, "
                                + " P.QtyToPrint,P.LabelType,P.NoPriceOnLabel,P.PrintBarCode,P.DecimalPlace, p.FoodStampEligible from Product P "
                                + " left outer join Dept D on P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                                + " left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                                + " left outer join Product L on L.ID = P.LinkSKU "
                                + " where (1 = 1) and P.LinkSKU > 0 " + SQLFilter + " order By P.SKU,P.BreakPackRatio ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKUDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    string qty = "";
                    string ptype = "";
                    string fs = "";
                    double bprat = 0;

                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        fs = "Yes";
                    }
                    else
                    {
                        fs = "No";
                    }

                    if (objSQLReader["ProductType"].ToString() == "P")
                    {
                        ptype = "Product";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "S")
                    {
                        ptype = "Service";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "U")
                    {
                        ptype = "Unit of Measure";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "M")
                    {
                        ptype = "Matrix";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "E")
                    {
                        ptype = "Serialized";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "K")
                    {
                        ptype = "Kit";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "F")
                    {
                        ptype = "Fuel";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "T")
                    {
                        ptype = "Tagged";
                    }
                    else
                    {
                        ptype = "Weighted";
                    }
                    qty = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()).ToString();
                    if (blIsPOS)
                    {
                        if (objSQLReader["DisplayStockinPOS"].ToString() == "N") qty = "";
                    }

                    int intQtyToPrint = 0;
                    if (objSQLReader["QtyToPrint"].ToString() != "")
                        intQtyToPrint = Functions.fnInt32(objSQLReader["QtyToPrint"].ToString());

                    if (objSQLReader["BreakPackRatio"].ToString().EndsWith(".000"))
                    {
                        bprat = Math.Ceiling(Functions.fnDouble(objSQLReader["BreakPackRatio"]));
                    }
                    else
                    {
                        bprat = Functions.fnDouble(objSQLReader["BreakPackRatio"]);
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["ProductName"].ToString(),
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()),
                                                   qty,
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["Category"].ToString(),
                                                   ptype,objSQLReader["NoPriceOnLabel"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),fs,
                                                   objSQLReader["ProductStatus"].ToString(), intQtyToPrint,
                                                   Functions.fnInt32(objSQLReader["LabelType"].ToString()),
                                                   objSQLReader["UPC"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   objSQLReader["LikSKU"].ToString(),
                                                   objSQLReader["LikSKU"].ToString() + " - " + objSQLReader["LikDescription"].ToString(),
                                                   bprat.ToString(),objSQLReader["LinkID"].ToString()});
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

        public DataTable FetchServiceData(bool blIsPOS, string sts)
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            if (sts == "Active Services")
            {
                SQLFilter = " and P.ProductStatus = 'Y' ";
            }
            if (sts == "Inactive Services")
            {
                SQLFilter = " and P.ProductStatus = 'N' ";
            }
            string strSQLComm = "";
            if (!blIsPOS)
            {
                strSQLComm = " select P.ID,SKU,P.Description as ProductName,PriceA,QtyOnHand,ProductType,P.ProductStatus,UPC,Season,"
                           + " D.Description as Department,CAT.Description as Category,BRAD.BrandDescription as Brand,P.DisplayStockinPOS,"
                           + " P.QtyToPrint,P.LabelType,P.NoPriceOnLabel,P.PrintBarCode,DecimalPlace,p.FoodStampEligible,p.MinimumServiceTime from Product P  "
                           + " left outer join Dept D on P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                           + " left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                           + " where (1 = 1) and (P.LinkSKU = 0 or P.LinkSKU = -1) and ProductType = 'S' " + SQLFilter + " order By SKU ";
            }
            if (blIsPOS)
            {
                strSQLComm = " select P.ID,SKU,P.Description as ProductName,PriceA,QtyOnHand,ProductType,P.ProductStatus,UPC,Season,"
                           + " D.Description as Department,CAT.Description as Category,BRAD.BrandDescription as Brand,P.DisplayStockinPOS,"
                           + " P.QtyToPrint,P.LabelType,P.NoPriceOnLabel,P.PrintBarCode,DecimalPlace,p.FoodStampEligible,p.MinimumServiceTime from Product P  "
                           + " left outer join Dept D on P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                           + " left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                           + " where (1 = 1) and P.LinkSKU >= 0 and ProductType = 'S' " + SQLFilter + " order By SKU ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string qty = "";
                    string ptype = "";
                    string fs = "";

                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        fs = "Yes";
                    }
                    else
                    {
                        fs = "No";
                    }

                    if (objSQLReader["ProductType"].ToString() == "P")
                    {
                        ptype = "Product";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "S")
                    {
                        ptype = "Service";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "U")
                    {
                        ptype = "Unit of Measure";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "M")
                    {
                        ptype = "Matrix";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "E")
                    {
                        ptype = "Serialized";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "K")
                    {
                        ptype = "Kit";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "F")
                    {
                        ptype = "Fuel";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "T")
                    {
                        ptype = "Tagged";
                    }
                    else
                    {
                        ptype = "Weighted";
                    }
                    qty = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()).ToString();
                    qty = Functions.fnDouble(qty).ToString("f");
                    if (blIsPOS)
                    {
                        if (objSQLReader["DisplayStockinPOS"].ToString() == "N") qty = "";
                    }

                    int intQtyToPrint = 0;
                    if (objSQLReader["QtyToPrint"].ToString() != "")
                        intQtyToPrint = Functions.fnInt32(objSQLReader["QtyToPrint"].ToString());

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["ProductName"].ToString(),
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()),
                                                   qty,
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["Category"].ToString(),
                                                   ptype,objSQLReader["NoPriceOnLabel"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),fs,
                                                   objSQLReader["ProductStatus"].ToString(), intQtyToPrint,
                                                   Functions.fnInt32(objSQLReader["LabelType"].ToString()),
                                                   objSQLReader["UPC"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   objSQLReader["MinimumServiceTime"].ToString()});
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

        public DataTable FetchData(bool blIsPOS, string sts)
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            if (sts == "Active Products")
            {
                SQLFilter = " and P.ProductStatus = 'Y' ";
            }
            if (sts == "Inactive Products")
            {
                SQLFilter = " and P.ProductStatus = 'N' ";
            }
            string strSQLComm = "";
            if (!blIsPOS)
            {
                strSQLComm = " select P.ID,SKU,P.Description as ProductName,PriceA,QtyOnHand,ProductType,P.ProductStatus,UPC,Season,"
                           + " D.Description as Department,CAT.Description as Category,BRAD.BrandDescription as Brand,P.DisplayStockinPOS,"
                           + " P.QtyToPrint,P.LabelType,P.NoPriceOnLabel,P.PrintBarCode,DecimalPlace,p.FoodStampEligible, "
                           + " isnull(V.VendorID,'') as VID,isnull(V.Name,'') as VName,isnull(VP.PartNumber,'') as VPart, P.SKU2, P.SKU3 "
                           + " from Product P left outer join Dept D on P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                           + " left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                           + " left outer join VendPart VP on P.ID = VP.ProductID and VP.IsPrimary = 'Y' "
                           + " left outer join Vendor V on V.ID = VP.VendorID "
                           + " where (1 = 1) and (P.LinkSKU = 0 or P.LinkSKU = -1) and ProductType <> 'S' " + SQLFilter + " order By SKU ";
            }
            if (blIsPOS)
            {
                strSQLComm = " select P.ID,SKU,P.Description as ProductName,PriceA,QtyOnHand,ProductType,P.ProductStatus,UPC,Season,"
                           + " D.Description as Department,CAT.Description as Category,BRAD.BrandDescription as Brand,P.DisplayStockinPOS,"
                           + " P.QtyToPrint,P.LabelType,P.NoPriceOnLabel,P.PrintBarCode,DecimalPlace, p.FoodStampEligible, "
                           + " isnull(V.VendorID,'') as VID,isnull(V.Name,'') as VName,isnull(VP.PartNumber,'') as VPart, P.SKU2, P.SKU3 "
                           + " from Product P left outer join Dept D on P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                           + " left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                           + " left outer join VendPart VP on P.ID = VP.ProductID and VP.IsPrimary = 'Y' "
                           + " left outer join Vendor V on V.ID = VP.VendorID "
                           + " where (1 = 1) and P.LinkSKU >= 0  " + SQLFilter + " order By SKU ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 300;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPart", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU3", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string qty = "";
                    string ptype = "";
                    string fs = "";

                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        fs = "Yes";
                    }
                    else
                    {
                        fs = "No";
                    }

                    if (objSQLReader["ProductType"].ToString() == "P")
                    {
                        ptype = "Product";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "S")
                    {
                        ptype = "Service";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "U")
                    {
                        ptype = "Unit of Measure";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "M")
                    {
                        ptype = "Matrix";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "E")
                    {
                        ptype = "Serialized";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "K")
                    {
                        ptype = "Kit";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "F")
                    {
                        ptype = "Fuel";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "T")
                    {
                        ptype = "Tagged";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "D")
                    {
                        ptype = "Donation";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "Q")
                    {
                        ptype = "Entry Ticket";
                    }
                    else 
                    {
                        ptype = "Weighted";
                    }
                    qty = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()).ToString();
                    qty = Functions.fnDouble(qty).ToString("f");
                    if (blIsPOS)
                    {
                        if (objSQLReader["DisplayStockinPOS"].ToString() == "N") qty = "";
                    }

                    int intQtyToPrint = 0;
                    if (objSQLReader["QtyToPrint"].ToString() != "")
                        intQtyToPrint = Functions.fnInt32(objSQLReader["QtyToPrint"].ToString());

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["ProductName"].ToString(),
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()),
                                                   qty,
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["Category"].ToString(),
                                                   ptype,objSQLReader["NoPriceOnLabel"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),fs,
                                                   objSQLReader["ProductStatus"].ToString(), intQtyToPrint,
                                                   Functions.fnInt32(objSQLReader["LabelType"].ToString()),
                                                   objSQLReader["UPC"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                    objSQLReader["VID"].ToString(),
                                                    objSQLReader["VName"].ToString(),
                                                    objSQLReader["VPart"].ToString(),
                                                    objSQLReader["SKU2"].ToString(),
                                                    objSQLReader["SKU3"].ToString()});
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

        public DataTable FetchData1(bool blIsPOS, string sts)
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            if (sts == "Active Products")
            {
                SQLFilter = " and ProductStatus = 'Y' ";
            }
            if (sts == "Inactive Products")
            {
                SQLFilter = " and ProductStatus = 'N' ";
            }
            string strSQLComm = " seletc P.ID,SKU,P.Description as ProductName,PriceA,QtyOnHand,ProductType,ProductStatus,P.CategoryID, "
                                + " D.Description as Department,CAT.Description as Category, B.BrandDescription as Brand,DisplayStockinPOS,QtyToPrint,LabelType, "
                                + " NoPriceOnLabel,PrintBarCode,DecimalPlace, p.FoodStampEligible from Product P left outer join Dept D on  "
                                + " P.DepartmentID = D.ID left outer join Category CAT on P.CategoryID = CAT.ID "
                                + " left outer join Category B on P.BrandID = B.ID "
                                + " where (1 = 1) " + SQLFilter + " order By SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.Int32"));


                while (objSQLReader.Read())
                {
                    string qty = "";
                    string ptype = "";
                    string fs = "";

                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        fs = "Yes";
                    }
                    else
                    {
                        fs = "No";
                    }

                    if (objSQLReader["ProductType"].ToString() == "P")
                    {
                        ptype = "Product";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "S")
                    {
                        ptype = "Service";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "U")
                    {
                        ptype = "Unit of Measure";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "M")
                    {
                        ptype = "Matrix";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "E")
                    {
                        ptype = "Serialized";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "K")
                    {
                        ptype = "Kit";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "F")
                    {
                        ptype = "Fuel";
                    }
                    else if (objSQLReader["ProductType"].ToString() == "T")
                    {
                        ptype = "Tagged";
                    }
                    else
                    {
                        ptype = "Weighted";
                    }
                    qty = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()).ToString();
                    qty = Functions.fnDouble(qty).ToString("f");
                    if (blIsPOS)
                    {
                        if (objSQLReader["DisplayStockinPOS"].ToString() == "N") qty = "";
                    }

                    int intQtyToPrint = 0;
                    if (objSQLReader["QtyToPrint"].ToString() != "")
                        intQtyToPrint = Functions.fnInt32(objSQLReader["QtyToPrint"].ToString());

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["ProductName"].ToString(),
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()),
                                                   qty,
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["CategoryID"].ToString(),
                                                   objSQLReader["Category"].ToString(),
                                                   ptype,objSQLReader["NoPriceOnLabel"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),fs,
                                                   objSQLReader["ProductStatus"].ToString(), intQtyToPrint,
                                                   Functions.fnInt32(objSQLReader["LabelType"].ToString()) });
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

        public DataTable FetchLookupData()
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            string strSQLComm = "";

            strSQLComm = " select ID,SKU,Description from Product where LinkSKU >= 0 order By SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString()});
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

        public DataTable FetchMLookupData(int intOption)
        {
            DataTable dtbl = new DataTable();
            string SQLFilter = "";
            string strSQLComm = "";
            if (intOption == 0)
            {
                SQLFilter = " order by SKU ";
            }
            else
            {
                SQLFilter = " order by Description ";
            }
            strSQLComm = " select ID,Description,SKU,Cost,PrintBarCode from Product where LinkSKU >= 0 " + SQLFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString()});
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

        #region Show Record based on ID

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from product where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BinLocation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromptForPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddtoPOSScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSCategoryScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrimaryVendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AllowZeroStock", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayStockinPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaggedInInvoice", System.Type.GetType("System.String"));

                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RentalPerMinute", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerHour", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerHalfDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerWeek", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerMonth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalDeposit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalMinHour", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalMinAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairCharge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPrompt", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RepairPromptForCharge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairPromptForTag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonDiscountable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToScaleScreen", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ScaleBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleIsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleIsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SplitWeight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BookingExpFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UOM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShopifyImageExportFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpiryDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["SKU2"].ToString(),
                                                objSQLReader["SKU3"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["BinLocation"].ToString(),
                                                objSQLReader["ProductType"].ToString(),
                                                objSQLReader["PriceA"].ToString(),
                                                objSQLReader["PriceB"].ToString(),
                                                objSQLReader["PriceC"].ToString(),
                                                objSQLReader["PromptForPrice"].ToString(),
                                                objSQLReader["AddtoPOSScreen"].ToString(),
                                                objSQLReader["AddToPOSCategoryScreen"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString(),
                                                objSQLReader["LastCost"].ToString(),
                                                objSQLReader["Cost"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString(),
                                                objSQLReader["QtyOnLayaway"].ToString(),
                                                objSQLReader["ReorderQty"].ToString(),
                                                objSQLReader["NormalQty"].ToString(),
                                                objSQLReader["PrimaryVendorID"].ToString(),
                                                objSQLReader["DepartmentID"].ToString(),
                                                objSQLReader["CategoryID"].ToString(),
                                                objSQLReader["Points"].ToString(),
                                                objSQLReader["PrintBarCode"].ToString(),
                                                objSQLReader["NoPriceOnLabel"].ToString(),
                                                objSQLReader["QtyToPrint"].ToString(),
                                                objSQLReader["LabelType"].ToString(),
                                                objSQLReader["FoodStampEligible"].ToString(),
                                                objSQLReader["ProductNotes"].ToString(),
                                                objSQLReader["Notes2"].ToString(),
                                                objSQLReader["MinimumAge"].ToString(),
                                                objSQLReader["AllowZeroStock"].ToString(),
                                                objSQLReader["DisplayStockinPOS"].ToString(),
                                                objSQLReader["POSBackground"].ToString(),
                                                objSQLReader["POSScreenStyle"].ToString(),
                                                objSQLReader["POSScreenColor"].ToString(),
                                                objSQLReader["POSFontType"].ToString(),
                                                objSQLReader["POSFontSize"].ToString(),
                                                objSQLReader["POSFontColor"].ToString(),
                                                objSQLReader["IsBold"].ToString(),
                                                objSQLReader["IsItalics"].ToString(),
                                                objSQLReader["DecimalPlace"].ToString(),
                                                objSQLReader["ProductStatus"].ToString(),
                                                objSQLReader["TaggedInInvoice"].ToString(),
                                                objSQLReader["BrandID"].ToString(),
                                                objSQLReader["Season"].ToString(),
                                                objSQLReader["CaseUPC"].ToString(),
                                                objSQLReader["CaseQty"].ToString(),
                                                objSQLReader["UPC"].ToString(),
                                                objSQLReader["LinkSKU"].ToString(),
                                                objSQLReader["BreakPackRatio"].ToString(),
                                                objSQLReader["RentalPerMinute"].ToString(),
                                                objSQLReader["RentalPerHour"].ToString(),
                                                objSQLReader["RentalPerHalfDay"].ToString(),
                                                objSQLReader["RentalPerDay"].ToString(),
                                                objSQLReader["RentalPerWeek"].ToString(),
                                                objSQLReader["RentalPerMonth"].ToString(),
                                                objSQLReader["RentalDeposit"].ToString(),
                                                objSQLReader["RentalMinHour"].ToString(),
                                                objSQLReader["RentalMinAmount"].ToString(),
                                                objSQLReader["MinimumServiceTime"].ToString(),
                                                objSQLReader["RepairCharge"].ToString(),
                                                objSQLReader["RentalPrompt"].ToString(),
                                                objSQLReader["RepairPromptForCharge"].ToString(),
                                                objSQLReader["RepairPromptForTag"].ToString(),
                                                objSQLReader["NonDiscountable"].ToString(),
                                                objSQLReader["Tare"].ToString(),
                                                objSQLReader["Tare2"].ToString(),
                                                objSQLReader["AddToScaleScreen"].ToString(),
                                                objSQLReader["ScaleBackground"].ToString(),
                                                objSQLReader["ScaleScreenStyle"].ToString(),
                                                objSQLReader["ScaleScreenColor"].ToString(),
                                                objSQLReader["ScaleFontType"].ToString(),
                                                objSQLReader["ScaleFontSize"].ToString(),
                                                objSQLReader["ScaleFontColor"].ToString(),
                                                objSQLReader["ScaleIsBold"].ToString(),
                                                objSQLReader["ScaleIsItalics"].ToString(),
                                                objSQLReader["SplitWeight"].ToString(),
                                                objSQLReader["BookingExpFlag"].ToString(),
                                                objSQLReader["UOM"].ToString(),
                                                objSQLReader["ShopifyImageExportFlag"].ToString(),
                                                objSQLReader["DiscountedCost"].ToString(),
                                                objSQLReader["ExpiryDate"].ToString()
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

        public DataTable ShowRecordOfFamily(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from product where BrandID = @ID order by Description";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BinLocation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromptForPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddtoPOSScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSCategoryScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrimaryVendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AllowZeroStock", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayStockinPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaggedInInvoice", System.Type.GetType("System.String"));

                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RentalPerMinute", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerHour", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerHalfDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerWeek", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerMonth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalDeposit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalMinHour", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalMinAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairCharge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPrompt", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RepairPromptForCharge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairPromptForTag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonDiscountable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare2", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["SKU2"].ToString(),
                                                objSQLReader["SKU3"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["BinLocation"].ToString(),
                                                objSQLReader["ProductType"].ToString(),
                                                objSQLReader["PriceA"].ToString(),
                                                objSQLReader["PriceB"].ToString(),
                                                objSQLReader["PriceC"].ToString(),
                                                objSQLReader["PromptForPrice"].ToString(),
                                                objSQLReader["AddtoPOSScreen"].ToString(),
                                                objSQLReader["AddToPOSCategoryScreen"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString(),
                                                objSQLReader["LastCost"].ToString(),
                                                objSQLReader["Cost"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString(),
                                                objSQLReader["QtyOnLayaway"].ToString(),
                                                objSQLReader["ReorderQty"].ToString(),
                                                objSQLReader["NormalQty"].ToString(),
                                                objSQLReader["PrimaryVendorID"].ToString(),
                                                objSQLReader["DepartmentID"].ToString(),
                                                objSQLReader["CategoryID"].ToString(),
                                                objSQLReader["Points"].ToString(),
                                                objSQLReader["PrintBarCode"].ToString(),
                                                objSQLReader["NoPriceOnLabel"].ToString(),
                                                objSQLReader["QtyToPrint"].ToString(),
                                                objSQLReader["LabelType"].ToString(),
                                                objSQLReader["FoodStampEligible"].ToString(),
                                                objSQLReader["ProductNotes"].ToString(),
                                                objSQLReader["Notes2"].ToString(),
                                                objSQLReader["MinimumAge"].ToString(),
                                                objSQLReader["AllowZeroStock"].ToString(),
                                                objSQLReader["DisplayStockinPOS"].ToString(),
                                                objSQLReader["POSBackground"].ToString(),
                                                objSQLReader["POSScreenStyle"].ToString(),
                                                objSQLReader["POSScreenColor"].ToString(),
                                                objSQLReader["POSFontType"].ToString(),
                                                objSQLReader["POSFontSize"].ToString(),
                                                objSQLReader["POSFontColor"].ToString(),
                                                objSQLReader["IsBold"].ToString(),
                                                objSQLReader["IsItalics"].ToString(),
                                                objSQLReader["DecimalPlace"].ToString(),
                                                objSQLReader["ProductStatus"].ToString(),
                                                objSQLReader["TaggedInInvoice"].ToString(),
                                                objSQLReader["BrandID"].ToString(),
                                                objSQLReader["Season"].ToString(),
                                                objSQLReader["CaseUPC"].ToString(),
                                                objSQLReader["CaseQty"].ToString(),
                                                objSQLReader["UPC"].ToString(),
                                                objSQLReader["LinkSKU"].ToString(),
                                                objSQLReader["BreakPackRatio"].ToString(),
                                                objSQLReader["RentalPerMinute"].ToString(),
                                                objSQLReader["RentalPerHour"].ToString(),
                                                objSQLReader["RentalPerHalfDay"].ToString(),
                                                objSQLReader["RentalPerDay"].ToString(),
                                                objSQLReader["RentalPerWeek"].ToString(),
                                                objSQLReader["RentalPerMonth"].ToString(),
                                                objSQLReader["RentalDeposit"].ToString(),
                                                objSQLReader["RentalMinHour"].ToString(),
                                                objSQLReader["RentalMinAmount"].ToString(),
                                                objSQLReader["MinimumServiceTime"].ToString(),
                                                objSQLReader["RepairCharge"].ToString(),
                                                objSQLReader["RentalPrompt"].ToString(),
                                                objSQLReader["RepairPromptForCharge"].ToString(),
                                                objSQLReader["RepairPromptForTag"].ToString(),
                                                objSQLReader["NonDiscountable"].ToString(),
                                                objSQLReader["Tare"].ToString(),
                                                objSQLReader["Tare2"].ToString()
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

        #endregion

        #region Get SKU

        public string GetProductSKU(int intpID)
        {
            string strpSKU = "";
            string strSQLComm = " select SKU from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strpSKU = objsqlReader["SKU"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strpSKU;
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

        #endregion

        #region Get UPC

        public string GetProductUPC(int intpID)
        {
            string strpSKU = "";
            string strSQLComm = " select UPC from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strpSKU = objsqlReader["UPC"].ToString();
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strpSKU;
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

        #endregion

        #region Get Notes

        public string GetProductNotes(int intpID)
        {
            string strpSKU = "";
            string strSQLComm = " select ProductNotes from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strpSKU = objsqlReader["ProductNotes"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strpSKU;
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
        #endregion

        #region Show Record based on SKU
        public DataTable ShowSKURecord( string strSKU)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from Product where SKU = @SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@SKU"].Value = strSKU;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["SKU2"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["ProductType"].ToString(),
                                                objSQLReader["PriceA"].ToString(),
                                                objSQLReader["PriceB"].ToString(),
                                                objSQLReader["PriceC"].ToString(),
                                                objSQLReader["Cost"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString(),
                                                objSQLReader["QtyOnLayaway"].ToString(),
                                                objSQLReader["ReorderQty"].ToString(),
                                                objSQLReader["NormalQty"].ToString()});
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

        #region Show Record based on BookerProductID
        public DataTable ShowBookerRecord(string strSKU1)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select ID, SKU, BookerProductID from Product where SKU = @SKU and isnull (IsDeleted,0)=0";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@SKU"].Value = strSKU1;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BookerProductID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["BookerProductID"].ToString()});
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

        public DataTable ShowBookerRecordByBookersId(string strBookerID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select ID, SKU, BookerProductID from Product where BookerProductID = @BookerProductID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@BookerProductID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@BookerProductID"].Value = strBookerProductID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BookerProductID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["BookerProductID"].ToString()});
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

        #region Show Record based on ID (Report)
        public DataTable FetchRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.*,d.Description as Dept,c.Description as Category,b.BrandDescription as Brand, "
                              + " l.SKU as LSKU, l.Description as LDesc from Product a left outer join Dept d on a.DepartmentID = d.ID "
                              + " left outer join Category c on a.CategoryID = c.ID left outer join brandmaster b on b.ID = a.BrandID "
                              + " left outer join product l on l.ID = a.LinkSKU where a.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BinLocation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromptForPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddtoPOSScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSCategoryScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrimaryVendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AllowZeroStock", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayStockinPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKUID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkSKUDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    int DP = Functions.fnInt32(objSQLReader["DecimalPlace"].ToString());
                    string strProductType = objSQLReader["ProductType"].ToString();
                    if (strProductType == "P")
                    {
                        strProductType = "Product";
                    }
                    else if (strProductType == "S")
                    {
                        strProductType = "Service";
                    }
                    else if (strProductType == "U")
                    {
                        strProductType = "Unit Of Measure";
                    }
                    else if (strProductType == "M")
                    {
                        strProductType = "Matrix";
                    }
                    else if (strProductType == "E")
                    {
                        strProductType = "Serialized";
                    }
                    else if (strProductType == "K")
                    {
                        strProductType = "Kit";
                    }
                    else if (strProductType == "F")
                    {
                        strProductType = "Fuel";
                    }
                    else
                    {
                        strProductType = "Weighted";
                    }

                    string chkPriceLabel = objSQLReader["PrintBarCode"].ToString();
                    if (chkPriceLabel == "Y")
                    {
                        chkPriceLabel = "Yes";
                    }
                    else
                    {
                        chkPriceLabel = "No";
                    }

                    string chkNoPriceOnLabel = objSQLReader["NoPriceOnLabel"].ToString();
                    if (chkNoPriceOnLabel == "Y")
                    {
                        chkNoPriceOnLabel = "Yes";
                    }
                    else
                    {
                        chkNoPriceOnLabel = "No";
                    }

                    string chkFoodStampEligible = objSQLReader["FoodStampEligible"].ToString();
                    if (chkFoodStampEligible == "Y")
                    {
                        chkFoodStampEligible = "Yes";
                    }
                    else
                    {
                        chkFoodStampEligible = "No";
                    }

                    string chkPromptForPrice = objSQLReader["PromptForPrice"].ToString();
                    if (chkPromptForPrice == "Y")
                    {
                        chkPromptForPrice = "Yes";
                    }
                    else
                    {
                        chkPromptForPrice = "No";
                    }

                    string chkAddtoPOSScreen = objSQLReader["AddtoPOSScreen"].ToString();
                    if (chkAddtoPOSScreen == "Y")
                    {
                        chkAddtoPOSScreen = "Yes";
                    }
                    else
                    {
                        chkAddtoPOSScreen = "No";
                    }

                    string chkAddToPOSCategoryScreen = objSQLReader["AddToPOSCategoryScreen"].ToString();
                    if (chkAddToPOSCategoryScreen == "Y")
                    {
                        chkAddToPOSCategoryScreen = "Yes";
                    }
                    else
                    {
                        chkAddToPOSCategoryScreen = "No";
                    }

                    string chkScaleBarCode = objSQLReader["ScaleBarCode"].ToString();
                    if (chkScaleBarCode == "Y")
                    {
                        chkScaleBarCode = "Yes";
                    }
                    else
                    {
                        chkScaleBarCode = "No";
                    }

                    

                    string chkAllowZeroStock = objSQLReader["AllowZeroStock"].ToString();
                    if (chkAllowZeroStock == "Y")
                    {
                        chkAllowZeroStock = "Yes";
                    }
                    else
                    {
                        chkAllowZeroStock = "No";
                    }

                    string chkDisplayStockinPOS = objSQLReader["DisplayStockinPOS"].ToString();
                    if (chkDisplayStockinPOS == "Y")
                    {
                        chkDisplayStockinPOS = "Yes";
                    }
                    else
                    {
                        chkDisplayStockinPOS = "No";
                    }

                    string chkProductStatus = objSQLReader["ProductStatus"].ToString();
                    if (chkProductStatus == "Y")
                    {
                        chkProductStatus = "Yes";
                    }
                    else
                    {
                        chkProductStatus = "No";
                    }

                    string chkDecimalPlace = objSQLReader["DecimalPlace"].ToString();
                    if (chkDecimalPlace == "Y")
                    {
                        chkDecimalPlace = "Yes";
                    }
                    else
                    {
                        chkDecimalPlace = "No";
                    }


                    string dblvalue1 = "";
                    string dblvalue2 = "";
                    string dblvalue3 = "";
                    string dblvalue4 = "";
                    string dblvalue5 = "";
                    string dblvalue6 = "";

                    string qty1 = "";
                    string qty2 = "";
                    string qty3 = "";
                    string qty4 = "";

                    if (intDecimalPlace == 3)
                    {
                        dblvalue1 = Functions.fnDouble(objSQLReader["PriceA"].ToString()).ToString("f3");
                        dblvalue2 = Functions.fnDouble(objSQLReader["PriceB"].ToString()).ToString("f3");
                        dblvalue3 = Functions.fnDouble(objSQLReader["PriceC"].ToString()).ToString("f3");
                        dblvalue4 = Functions.fnDouble(objSQLReader["LastCost"].ToString()).ToString("f3");
                        dblvalue5 = Functions.fnDouble(objSQLReader["Cost"].ToString()).ToString("f3");
                        dblvalue6 = Functions.fnDouble(objSQLReader["DiscountedCost"].ToString()).ToString("f3");
                    }
                    else
                    {
                        if (DP == 2)
                        {
                            dblvalue1 = Functions.fnDouble(objSQLReader["PriceA"].ToString()).ToString("f");
                            dblvalue2 = Functions.fnDouble(objSQLReader["PriceB"].ToString()).ToString("f");
                            dblvalue3 = Functions.fnDouble(objSQLReader["PriceC"].ToString()).ToString("f");
                            dblvalue4 = Functions.fnDouble(objSQLReader["LastCost"].ToString()).ToString("f");
                            dblvalue5 = Functions.fnDouble(objSQLReader["Cost"].ToString()).ToString("f");
                            dblvalue6 = Functions.fnDouble(objSQLReader["DiscountedCost"].ToString()).ToString("f2");
                        }
                        else
                        {
                            dblvalue1 = Functions.fnDouble(objSQLReader["PriceA"].ToString()).ToString("f3");
                            dblvalue2 = Functions.fnDouble(objSQLReader["PriceB"].ToString()).ToString("f3");
                            dblvalue3 = Functions.fnDouble(objSQLReader["PriceC"].ToString()).ToString("f3");
                            dblvalue4 = Functions.fnDouble(objSQLReader["LastCost"].ToString()).ToString("f3");
                            dblvalue5 = Functions.fnDouble(objSQLReader["Cost"].ToString()).ToString("f3");
                            dblvalue6 = Functions.fnDouble(objSQLReader["DiscountedCost"].ToString()).ToString("f2");
                        }
                        
                    }

                    string srvtime = objSQLReader["MinimumServiceTime"].ToString() + " min";

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["SKU2"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["BinLocation"].ToString(),
                                                strProductType,
                                                dblvalue1,
                                                dblvalue2,
                                                dblvalue3,
                                                chkPromptForPrice,
                                                chkAddtoPOSScreen,
                                                chkAddToPOSCategoryScreen,
                                                chkScaleBarCode,
                                                dblvalue4,
                                                dblvalue5,
                                                dblvalue6,
                                                Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()).ToString("f"),//n0
                                                Functions.fnDouble(objSQLReader["QtyOnLayaway"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["ReorderQty"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["NormalQty"].ToString()).ToString("f"),
                                                objSQLReader["PrimaryVendorID"].ToString(),
                                                objSQLReader["Dept"].ToString(),
                                                objSQLReader["Category"].ToString(),
                                                objSQLReader["Points"].ToString(),
                                                chkPriceLabel,
                                                chkNoPriceOnLabel,
                                                objSQLReader["QtyToPrint"].ToString(),
                                                objSQLReader["LabelType"].ToString(),
                                                chkFoodStampEligible,
                                                objSQLReader["ProductNotes"].ToString(),
                                                objSQLReader["MinimumAge"].ToString(),
                                                chkAllowZeroStock,chkDisplayStockinPOS,chkProductStatus,chkDecimalPlace,
                                                objSQLReader["LinkSKU"].ToString(),
                                                objSQLReader["LSKU"].ToString(),
                                                objSQLReader["LDesc"].ToString(),
                                                Functions.fnDouble(objSQLReader["BreakPackRatio"]).ToString("f"),
                                                objSQLReader["UPC"].ToString(),
                                                objSQLReader["Brand"].ToString(),
                                                objSQLReader["Season"].ToString(),
                                                srvtime});
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

        #region Delete Data
        public int DeleteRecord(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_DeleteProduct";

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
        public int DeleteBookerRecord(string DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_DeleteBookerProduct";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SKU"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU"].Value = DeleteID;

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

        #region Duplicate Checking

        public int DuplicateBreakpack(int SKUrecd, double bpval)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as reccnt from product where LinkSKU = @LSKU and BreakPackRatio = @ratio ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@LSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LSKU"].Value = SKUrecd;

                objSQlComm.Parameters.Add(new SqlParameter("@ratio", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@ratio"].Value = bpval;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["reccnt"]);
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
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

        public string GetSKUFromID(int pID)
        {
            string strSKU = "";
            string strSQLComm = " select SKU from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strSKU = objsqlReader["SKU"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strSKU;
            }
            catch (SqlException SQLDBException)
            {
                objsqlReader.Close();
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strSKU;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int DuplicateCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where SKU=@SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        public int GetProductID(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select ID from PRODUCT where SKU = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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

        public string GetProductExpiryFromSKU(string strSKUrecd)
        {
            string intCount = "";
            string strSQLComm = " select case when ExpiryDate is null then 0 else 1 end as dtFlag, isnull(ExpiryDate,'') as ExpDt from PRODUCT where SKU = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["dtFlag"].ToString()) == 0 ? "" : objsqlReader["ExpDt"].ToString();
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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

        public string GetProductExpiryFromProductID(int ipId)
        {
            string intCount = "";
            string strSQLComm = " select case when ExpiryDate is null then 0 else 1 end as dtFlag, isnull(ExpiryDate,'') as ExpDt from product where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = ipId;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["dtFlag"].ToString()) == 0 ? "" : objsqlReader["ExpDt"].ToString();
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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

        public string GetProductExpiryForSerialisedItem(int HId)
        {
            string intCount = "";
            string strSQLComm = " select case when ExpiryDate is null then 0 else 1 end as dtFlag, isnull(ExpiryDate,'') as ExpDt from SerialDetail where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = HId;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["dtFlag"].ToString()) == 0 ? "" : objsqlReader["ExpDt"].ToString();
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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

        public int DuplicateAltSKUCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where SKU2 = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        public int DuplicateAltSKU2Count(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where SKU3 = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        public int DuplicateUPCCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where UPC = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        public string GetSKUFromAltSKU(string strSKUrecd)
        {
            string strSKU = "";
            string strSQLComm = " select SKU from PRODUCT where SKU2 = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strSKU = objsqlReader["SKU"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strSKU;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strSKU;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string GetSKUFromAltSKU2(string strSKUrecd)
        {
            string strSKU = "";
            string strSQLComm = " select SKU from PRODUCT where SKU3 = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strSKU = objsqlReader["SKU"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strSKU;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strSKU;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string GetSKUFromUPC(string strSKUrecd)
        {
            string strSKU = "";
            string strSQLComm = " select SKU from PRODUCT where UPC = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strSKU = objsqlReader["SKU"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strSKU;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strSKU;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int IfActiveProduct(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select ProductStatus from PRODUCT where SKU = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        string strS = objsqlReader["ProductStatus"].ToString();
                        if (strS == "Y") intCount = 1;
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
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

        public int DuplicateWeightedCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as RECCOUNT from PRODUCT where SKU = @SKU and scalebarcode = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        #endregion

        #region Duplicate Alter SKU

        public int AltSKUCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where SKU2 = @SKU and SKU2 <> '' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        #endregion

        #region Duplicate Alter SKU

        public int AltSKU2Count(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where SKU3=@SKU and SKU3 <> '' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        #endregion

        #region Duplicate UPC

        public int UPCCount(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PRODUCT where UPC=@SKU and UPC <> '' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

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

        #endregion

        #region Fetch Lookup Data

        public DataTable FetchJournalLookup()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,Description,SKU from Product where productstatus = 'Y' order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["Description"].ToString(),
												objSQLReader["SKU"].ToString() });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, "NA" });
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

        public DataTable FetchLookupData2(int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID,Description,SKU,Cost,PrintBarCode from Product where LinkSKU <= 0 "
                              + " and producttype = 'P' and productstatus = 'Y' " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString()});
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

        public DataTable FetchLookupData( int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID,Description,SKU,Cost,PrintBarCode from Product where productstatus = 'Y' " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString()});
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

        public DataTable FetchLookupData1(int intOption,int vid)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by p.SKU ";
            }
            else
            {
                strFilter = " order by p.Description ";
            }
            string strSQLComm = " select p.ID,p.Description,p.SKU,p.Cost,p.PrintBarCode from Product p left outer join vendpart v "
                              + " on v.ProductID = p.ID where v.VendorID = @VID and p.LinkSKU <= 0 " + strFilter;
                                
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@VID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VID"].Value = vid;
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString()

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

        public DataTable FetchLookupData3(int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID,Description,SKU,Cost,PriceA,PriceB,PriceC from Product where LinkSKU <= 0 " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
               
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["PriceA"].ToString(),
                                                   objSQLReader["PriceB"].ToString(),
                                                   objSQLReader["PriceC"].ToString()});
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

        public DataTable FetchMatrixLookupData1(int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID,Description,SKU from Product where producttype = 'M' and "
                              + " productstatus = 'Y' " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString()});
                }
                dtbl.Rows.Add(new object[] { "999999","All Items","" });
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

        public DataTable FetchMatrixLookupData(int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID,Description,SKU from Product where producttype = 'M' and "
                              + " productstatus = 'Y' " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString()});
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

        public DataTable FetchMatrixOptionData(int intpid)
        {
            DataTable dtbl = new DataTable();
            
            string strSQLComm = " select ID,Option1Name,Option2Name,Option3Name from MatrixOptions where productid = @pid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@pid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@pid"].Value = intpid;
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option3Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {  objSQLReader["ID"].ToString(),
                                                   objSQLReader["Option1Name"].ToString(),
                                                   objSQLReader["Option2Name"].ToString(),
                                                   objSQLReader["Option3Name"].ToString()
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

        public DataTable FetchMatrixValueData(int intoid,int intvid)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select OptionValue from MatrixValues where matrixoptionid = @mid "
                              + " and valueid = @vid order by OptionValue ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@mid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@mid"].Value = intoid;
                objSQlComm.Parameters.Add(new SqlParameter("@vid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@vid"].Value = intvid;

                objSQLReader = objSQlComm.ExecuteReader();
               
                dtbl.Columns.Add("OptionValue", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {  objSQLReader["OptionValue"].ToString() });
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

        public DataTable LookupItemForTransfer(int intOption)
        {
            DataTable dtbl = new DataTable();
            string strFilter = "";
            if (intOption == 0)
            {
                strFilter = " order by SKU ";
            }
            else
            {
                strFilter = " order by Description ";
            }
            string strSQLComm = " select ID, Description, SKU from Product where LinkSKU <= 0 and productstatus = 'Y' and ProductType in ('P', 'W') " + strFilter;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
												   objSQLReader["SKU"].ToString() });
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

        #region Show Primary Vendor
        public DataTable ShowPrimayVendor(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select VP.*, V.NAME as VendorName from VendPart VP left outer join Vendor V on VP.VendorID = V.ID where VP.PRODUCTID = @ID and VP.PRODUCTID > 0 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("IsPrimary", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("NewOld", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PackQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Shrink", System.Type.GetType("System.Double"));
                bool blLabel = false;
                double dblDetCost = 0;
                double dblDetShrink = 0;
                while (objSQLReader.Read())
                {
                    dblDetCost = 0;
                    if (objSQLReader["IsPrimary"].ToString() == "Y")
                    {
                        blLabel = true;
                    }
                    else
                    {
                        blLabel = false;
                    }

                    if (objSQLReader["Price"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["Price"].ToString());
                    }

                    if (objSQLReader["Shrink"].ToString() != "")
                    {
                        dblDetShrink = Functions.fnDouble(objSQLReader["Shrink"].ToString());
                    }


                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["VendorID"].ToString(),
                                                objSQLReader["VendorName"].ToString(),
                                                objSQLReader["PartNumber"].ToString(),
                                                dblDetCost,
                                                blLabel,
                                                "O",
                                                objSQLReader["PackQty"].ToString(),
                                                dblDetShrink});
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

        #region Show Taxes

        public DataTable ShowRentTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from TaxMapping TM left outer join TaxHeader T on TM.TaxID = T.ID "
                              + " where TM.MappingID = @ID AND TM.MappingType = 'Rent'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString()});
                    }

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

        public DataTable ShowRepairTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from TaxMapping TM left outer join TaxHeader T on TM.TaxID = T.ID "
                              + " where TM.MappingID = @ID AND TM.MappingType = 'Repair'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString()});
                    }

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

        public DataTable ShowTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select TM.*, T.TaxName, T.TaxRate from TaxMapping TM left outer join TaxHeader T on TM.TaxID = T.ID "
                                + " where TM.MappingID = @ID AND TM.MappingType = 'Product'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));


                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    //if (dblRate != 0) //  this check disabled to show 0% Tax for Bookers Import Data
                    //{
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString()});
                    //}
                                                
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

        public DataTable ShowActiveTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate,T.TaxType from TaxMapping TM left outer join TaxHeader T "
                              + " on TM.TaxID = T.ID where TM.MappingID = @ID and TM.MappingType = 'Product' and T.Active = 'Yes' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.CommandTimeout = 30000;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString(),
                                                objSQLReader["TaxType"].ToString()});
                    }

                }
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
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
        
        public DataTable ShowProductTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate,T.TaxType from TaxMapping TM left outer join TaxHeader T "
                              + " on TM.TaxID = T.ID  where TM.MappingID = @ID AND TM.MappingType = 'Product' order by T.TaxName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString(),
                                                objSQLReader["TaxType"].ToString()});
                    }                        
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

        public DataTable ShowActiveRentTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate,TaxType from TaxMapping TM left outer join TaxHeader T "
                              + " on TM.TaxID = T.ID where TM.MappingID = @ID and TM.MappingType = 'Rent' and T.Active = 'Yes' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.CommandTimeout = 30000;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));

                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString(),
                                                objSQLReader["TaxType"].ToString()});
                    }

                }
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
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

        public DataTable ShowActiveRepairTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate,T.TaxType from TaxMapping TM left outer join TaxHeader T "
                              + " on TM.TaxID = T.ID where TM.MappingID = @ID and TM.MappingType = 'Repair' and T.Active = 'Yes' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.CommandTimeout = 30000;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    if (dblRate != 0)
                    {
                        dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString(),
                                                objSQLReader["TaxType"].ToString()});
                    }

                }
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
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


        public DataTable ShowBookersTax(string bookerID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select top 1 ID, TaxName, TaxRate from TaxHeader where BookerID=@BookerID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@BookerID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@BookerID"].Value = bookerID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                
                string strTaxName = "";
                while (objSQLReader.Read())
                {
                    //if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    //else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    
                    dtbl.Rows.Add(new object[] {
                                            objSQLReader["ID"].ToString(),
                                            strTaxName,
                                            objSQLReader["TaxRate"].ToString()});
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

        #region Save Taxes

        public string SaveTaxParts(SqlCommand objSQlComm)
        {
            if (dblTaxDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblTaxDataTable.Rows)
            {
                if (dr["TaxID"].ToString() == "") continue;
                int intTaxID = 0;
                if (dr["TaxID"].ToString() != "")
                    intTaxID = Functions.fnInt32(dr["TaxID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertProductTax(objSQlComm, intTaxID);
                }
            }
            return strResult;
        }

        public string SaveRentTaxParts(SqlCommand objSQlComm)
        {
            if (dblRentTaxDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblRentTaxDataTable.Rows)
            {
                if (dr["TaxID"].ToString() == "") continue;
                int intTaxID = 0;
                if (dr["TaxID"].ToString() != "")
                    intTaxID = Functions.fnInt32(dr["TaxID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertRentTax(objSQlComm, intTaxID);
                }
            }
            return strResult;
        }

        public string SaveRepairTaxParts(SqlCommand objSQlComm)
        {
            if (dblRepairTaxDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblRepairTaxDataTable.Rows)
            {
                if (dr["TaxID"].ToString() == "") continue;
                int intTaxID = 0;
                if (dr["TaxID"].ToString() != "")
                    intTaxID = Functions.fnInt32(dr["TaxID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertRepairTax(objSQlComm, intTaxID);
                }
            }
            return strResult;
        }

        public string InsertProductTax(SqlCommand objSQlComm, int TaxID)
        {
            string strSQLComm = " insert into TaxMapping(TaxID, MappingType, MappingID,"
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @TaxID, @MappingType, @MappingID, "
                                + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@TaxID"].Value = TaxID;
                objSQlComm.Parameters["@MappingType"].Value = "Product";
                objSQlComm.Parameters["@MappingID"].Value = intID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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

        public string InsertRentTax(SqlCommand objSQlComm, int TaxID)
        {
            string strSQLComm = " insert into TaxMapping(TaxID,MappingType,MappingID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @TaxID, @MappingType, @MappingID, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@TaxID"].Value = TaxID;
                objSQlComm.Parameters["@MappingType"].Value = "Rent";
                objSQlComm.Parameters["@MappingID"].Value = intID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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

        public string InsertRepairTax(SqlCommand objSQlComm, int TaxID)
        {
            string strSQLComm = " insert into TaxMapping(TaxID,MappingType,MappingID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @TaxID, @MappingType, @MappingID, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@TaxID"].Value = TaxID;
                objSQlComm.Parameters["@MappingType"].Value = "Repair";
                objSQlComm.Parameters["@MappingID"].Value = intID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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


        public string DeleteTaxParts(SqlCommand objSQlComm)
        {
            string strSQLComm = " Delete from TaxMapping where MappingType = @MappingType and MappingID = @MappingID";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@MappingType"].Value = "Product";
                objSQlComm.Parameters["@MappingID"].Value = intID;

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

        public string DeleteRentTaxParts(SqlCommand objSQlComm)
        {
            string strSQLComm = " Delete from TaxMapping where MappingType = @MappingType and MappingID = @MappingID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@MappingType"].Value = "Rent";
                objSQlComm.Parameters["@MappingID"].Value = intID;

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

        public string DeleteRepairTaxParts(SqlCommand objSQlComm)
        {
            string strSQLComm = " delete from TaxMapping where MappingType = @MappingType and MappingID = @MappingID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@MappingType"].Value = "Repair";
                objSQlComm.Parameters["@MappingID"].Value = intID;

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

        #region Show Product Modifier
        public DataTable ShowProductModifier(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select m.ID, m.ModifierText from ProductModifier m left outer join Product p  "
                                + " on m.ProductID = p.ID where p.ID = @ID order by m.ModifierText ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ModifierText", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["ModifierText"].ToString()});

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

        #region Save Product Modifier

        public string SaveProductModifier(SqlCommand objSQlComm)
        {
            if (dblProductModifierDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblProductModifierDataTable.Rows)
            {
                if (dr["ModifierText"].ToString() == "") continue;
                string mText = "";
                if (dr["ModifierText"].ToString() != "")
                    mText = dr["ModifierText"].ToString();

                if (strResult == "")
                {
                    strResult = InsertProductModifier(objSQlComm, mText);
                }
            }
            return strResult;
        }

        public string InsertProductModifier(SqlCommand objSQlComm, string MText)
        {
            string strSQLComm = " insert into ProductModifier(ProductID,ModifierText, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @ProductID,@ModifierText,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ModifierText", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@ProductID"].Value = intID;
                objSQlComm.Parameters["@ModifierText"].Value = MText;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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

        public string DeleteProductModifier(SqlCommand objSQlComm)
        {
            string strSQLComm = " Delete from ProductModifier where ProductID = @ID";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;

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

        #region Fetch Purchase History data
        public DataTable FetchPurchaseData(int pProduct, DateTime pFDate, DateTime pTDate)
        {
            DataTable dtbl = new DataTable();

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);



            string strSQLComm = " select a.ID,BatchID,PurchaseOrder,D.Qty as Qty,D.Cost as Cost, D.Tax as Tax, "
                                + " D.Freight as Freight, DateOrdered, ReceiveDate,b.Name as VendorName "
                                + " From RecvHeader a left outer join Vendor b on a.VendorID = b.ID "
                                + " left outer join RecvDetail D on a.ID = D.BatchNo "
                                + " where (1 = 1) "
                                + " and ReceiveDate between @FDT and @TDT and D.ProductID = @PID "
                                + " order by ReceiveDate Desc, BatchID desc ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = pProduct;

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BatchID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PurchaseOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOrdered", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceiveDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TotalPrice", System.Type.GetType("System.Double"));

                double dblTotal = 0;

                while (objSQLReader.Read())
                {
                    dblTotal = 0;
                    string strDateTime = "";
                    string strDateTime1 = "";
                    if (objSQLReader["DateOrdered"].ToString() != "")
                    {
                        strDateTime = objSQLReader["DateOrdered"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["DateOrdered"].ToString();
                    }

                    if (objSQLReader["ReceiveDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["ReceiveDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["ReceiveDate"].ToString();
                    }
                    string qty = Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("#0");
                    dblTotal = (Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnDouble(objSQLReader["Cost"].ToString()))
                                + Functions.fnDouble(objSQLReader["Tax"].ToString()) + Functions.fnDouble(objSQLReader["Freight"].ToString());

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["BatchID"].ToString(),
                                                   objSQLReader["VendorName"].ToString(),
                                                   objSQLReader["PurchaseOrder"].ToString(),
                                                   strDateTime,strDateTime1,
                                                   qty,
                                                   Functions.fnDouble(objSQLReader["Cost"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Freight"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Tax"].ToString()),
                                                   dblTotal});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrorMsg = "";
                strErrorMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        #endregion

        #region Insert Kit Data
        public string InsertKitData()
        {
            string strSQLComm = " insert into Kit( KitID, ItemID, ItemCount, "
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @KitID, @ItemID, @ItemCount, "
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

                objSQlComm.Parameters.Add(new SqlParameter("@KitID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@KitID"].Value = intKitID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ItemCount"].Value = intItemCount;
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
                    objsqlReader.Close();
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
        #endregion

        #region Update Kit Data
        public string UpdateKitData()
        {
            string strSQLComm = " update Kit set ItemID=@ItemID,ItemCount=@ItemCount,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ItemCount"].Value = intItemCount;
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

        #region Fetch Kit data
        public DataTable FetchKitData(int pKit)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.ID,KitID,ItemID,ItemCount, b.SKU, b.Description from Kit a "
                              + " left outer join Product b on a.ItemID = b.ID "
                              + " where KitID=@KitID ";
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@KitID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@KitID"].Value = pKit;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("KitID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["KitID"].ToString(),
												   objSQLReader["ItemID"].ToString(),
                                                   objSQLReader["ItemCount"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString()});
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

        #region Show Kit Record based on ID
        public DataTable ShowKitRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from Kit where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("KitID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemCount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["KitID"].ToString(),
												objSQLReader["ItemID"].ToString(),
                                                objSQLReader["ItemCount"].ToString()});
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

        #region Duplicate Kit Checking

        public int DuplicateKitCount(int pKit, int pItem)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Kit where  "
                                + " KitID=@KitID and ItemID=@ItemID  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@KitID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@KitID"].Value = pKit;
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemID"].Value = pItem;

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
        #endregion 

        #region Delete Kit Data
        public string DeleteKitRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Kit Where ID = @ID";

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

        #region Insert UOM Data
        public string InsertUOMData()
        {
            string strSQLComm = " insert into UOM( ProductID,Description,PackageCount,UnitPrice,IsDefault, "
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @ProductID,@Description,@PackageCount,@UnitPrice,@IsDefault, "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PackageCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@UnitPrice", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@IsDefault", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = intUOMProductID;
                objSQlComm.Parameters["@Description"].Value = strUOMDescription;
                objSQlComm.Parameters["@PackageCount"].Value = intUOMPackageCount;
                objSQlComm.Parameters["@UnitPrice"].Value = dblUOMUnitPrice;
                objSQlComm.Parameters["@IsDefault"].Value = strUOMIsDefault;

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
                    objsqlReader.Close();
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
        #endregion

        #region Update UOM Data
        public string UpdateUOMData()
        {
            string strSQLComm = " update UOM set Description=@Description,PackageCount=@PackageCount,"
                                + " UnitPrice=@UnitPrice,IsDefault=@IsDefault,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PackageCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@UnitPrice", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@IsDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Description"].Value = strUOMDescription;
                objSQlComm.Parameters["@PackageCount"].Value = intUOMPackageCount;
                objSQlComm.Parameters["@UnitPrice"].Value = dblUOMUnitPrice;
                objSQlComm.Parameters["@IsDefault"].Value = strUOMIsDefault;

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
        
        #region Reset All Other Default Flag
        public string ResetDefaultFlag(int UOMID)
        {
            string strSQLComm = " update UOM set IsDefault= 'N' Where ID <> @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = UOMID;

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

        #region Fetch UOM data
        public DataTable FetchUOMData(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.ID,a.ProductID,a.Description,PackageCount,UnitPrice,IsDefault from UOM a "
                              + " left outer join Product b on a.ProductID = b.ID "
                              + " where a.ProductID=@PID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = pID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PackageCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitPrice", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("IsDefault", System.Type.GetType("System.String"));
                
                string Defaultstring = "";
                while (objSQLReader.Read())
                {
                    Defaultstring = "";
                    if (objSQLReader["IsDefault"].ToString() == "Y" )
                    {
                        Defaultstring = "Yes";
                    }
                    else
                    {
                        Defaultstring = "No";
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["ProductID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["PackageCount"].ToString(),
                                                   objSQLReader["UnitPrice"].ToString(),
                                                   Defaultstring});
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

        #region Show UOM Record based on ID
        public DataTable ShowUOMRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from UOM where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PackageCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsDefault", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["ProductID"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["PackageCount"].ToString(),
                                                objSQLReader["UnitPrice"].ToString(),
                                                objSQLReader["IsDefault"].ToString()});
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

        #region Duplicate UOM Checking

        public int DuplicateUOMCount(int pID, int pCount)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from UOM where  "
                                + " ProductID=@ProductID and PackageCount=@PackageCount  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ProductID"].Value = pID;
                objSQlComm.Parameters.Add(new SqlParameter("@PackageCount", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PackageCount"].Value = pCount;

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
        #endregion

        #region Delete UOM Data
        public string DeleteUOMRecord(int DeleteID)
        {
            string strSQLComm = " Delete from UOM Where ID = @ID";

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

        #region Fetch Data For General Report

        public DataTable FetchDataForGeneralReport(string refSKU, string refProductDesc, DataTable refDepartment, DataTable refVendor, DataTable refCategory, string refOrderBy, string refGroupBy)
        {
            DataTable dtbl = new DataTable();
            string strSQLSKU = "";
            string strSQLProductDesc = "";
            string strSQLDepartment = "";
            string strSQLVendor = "";
            string strSQLCat = "";
            string strSQLOrderBy = "";

            string getDept = "";
            string getVendor = "";
            string getCategory = "";

            if (refSKU != "")
            {
                strSQLSKU = " and a.SKU like '%" + refSKU + "%' ";
            }

            if (refProductDesc != "")
            {
                strSQLProductDesc = " and a.Description like '%" + refProductDesc + "%' ";
            }


            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }


            foreach (DataRow drG in refCategory.Rows)
            {

                if (Convert.ToBoolean(drG["CheckCategory"].ToString()))
                {
                    if (getCategory == "")
                    {
                        getCategory = drG["ID"].ToString();
                    }
                    else
                    {
                        getCategory = getCategory + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getCategory != "")
            {
                strSQLCat = " and a.CategoryID in ( " + getCategory + " )";
            }

            foreach (DataRow drC in refVendor.Rows)
            {
                if (Convert.ToBoolean(drC["CheckVendor"].ToString()))
                {
                    if (getVendor == "")
                    {
                        getVendor = drC["ID"].ToString();
                    }
                    else
                    {
                        getVendor = getVendor + "," + drC["ID"].ToString();
                    }
                }
            }
            if (getVendor != "")
            {
                strSQLVendor = " and c.ID in ( " + getVendor + " )";
            }

            if (refGroupBy == "0")
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by a.SKU ";
                }
                else if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by a.Description ";
                }
                else if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by Department ";
                }
                else if (refOrderBy == "4")
                {
                    strSQLOrderBy = " Order by VendorID ";
                }
                else if (refOrderBy == "5")
                {
                    strSQLOrderBy = " Order by Cat ";
                }
                else
                {
                    strSQLOrderBy = " Order by a.SKU ";
                }
            }

            if (refGroupBy == "1")  // Department
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by Department, a.SKU ";
                }
                else if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by Department, a.Description ";
                }
                else if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by Department ";
                }
                else if (refOrderBy == "4")
                {
                    strSQLOrderBy = " Order by Department, VendorID ";
                }
                else if (refOrderBy == "5")
                {
                    strSQLOrderBy = " Order by Department, Cat ";
                }
                else
                {
                    strSQLOrderBy = " Order by Department, a.SKU ";
                }
            }


            if (refGroupBy == "2")  // Vendor
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by VendorID, a.SKU ";
                }
                else if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by VendorID, a.Description ";
                }
                else if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by VendorID, Department ";
                }
                else if (refOrderBy == "4")
                {
                    strSQLOrderBy = " Order by VendorID ";
                }
                else if (refOrderBy == "5")
                {
                    strSQLOrderBy = " Order by VendorID, Cat ";
                }
                else
                {
                    strSQLOrderBy = " Order by VendorID, a.SKU ";
                }
            }

            if (refGroupBy == "3")  // POS Screen Category
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by Cat, a.SKU ";
                }
                else if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by Cat, a.Description ";
                }
                else if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by Cat, Department ";
                }
                else if (refOrderBy == "4")
                {
                    strSQLOrderBy = " Order by Cat, VendorID ";
                }
                else if (refOrderBy == "5")
                {
                    strSQLOrderBy = " Order by Cat ";
                }
                else
                {
                    strSQLOrderBy = " Order by Cat, a.SKU ";
                }
            }

            string strSQLComm = " select DISTINCT a.ID,a.SKU,a.DecimalPlace,a.QtyOnHand, a.Description,a.ProductType,a.Cost as UnitCost,a.PriceA as UnitRetailValue, "
                                + " Cast((a.QtyOnHand * a.Cost) as Numeric(15,2)) AS ExtCost, Cast((a.QtyOnHand * a.PriceA) as Numeric(15,2)) AS ExtRetailValue, "
                                + " b.DepartmentID, b.Description AS Department, "
                                + " ct.CategoryID, ct.Description AS Cat, "
                                + " ISNULL(c.VendorID, '') AS VendorID, ISNULL(c.Name, '') AS Vendor, isnull(a.DiscountedCost,0) as DCost "
                                + " from Product AS a LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID "
                                + " LEFT OUTER JOIN  Category AS ct ON a.CategoryID = ct.ID "
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID AND d.IsPrimary = 'Y' "
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID "  + " Where (1 = 1) "
                                + strSQLSKU + strSQLProductDesc + strSQLDepartment + strSQLVendor + strSQLCat
                                + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                double dblCurrentMargin = 0;
                while (objSQLReader.Read())
                {
                    dblCurrentMargin = 0;
                    if (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString()) == 0)
                    {
                        dblCurrentMargin = -99999;
                    }
                    else
                    {
                        dblCurrentMargin = (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString())
                                        - Functions.fnDouble(objSQLReader["UnitCost"].ToString())) * 100 / Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString());
                    }

                    string strPtype = "";
                    if (objSQLReader["ProductType"].ToString().Trim() == "P")
                    {
                        strPtype = "Product";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "S")
                    {
                        strPtype = "Service";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "U")
                    {
                        strPtype = "Unit Of Measure";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "M")
                    {
                        strPtype = "Matrix";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "E")
                    {
                        strPtype = "Serialized";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "K")
                    {
                        strPtype = "Kit";
                    }
                    else if (objSQLReader["ProductType"].ToString().Trim() == "F")
                    {
                        strPtype = "Fuel";
                    }
                    else 
                    {
                        strPtype = "Weighed";
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   strPtype,
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["UnitCost"].ToString(),
                                                   objSQLReader["UnitRetailValue"].ToString(),
                                                   objSQLReader["ExtCost"].ToString(),
                                                   objSQLReader["ExtRetailValue"].ToString(),
                                                   dblCurrentMargin.ToString(), 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["CategoryID"].ToString(),
                                                   objSQLReader["Cat"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),
                                                    objSQLReader["DCost"].ToString()});
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

        public DataTable FetchDataForPriceTaxReport(DataTable refDepartment, string refOrderBy)
        {
            DataTable dtbl = new DataTable();
            
            string strSQLDepartment = "";
            string strSQLOrderBy = "";

            string getDept = "";
           

            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }

            if (refOrderBy == "1")
            {
                strSQLOrderBy = " Order by Department, a.SKU ";
            }
            else if (refOrderBy == "2")
            {
                strSQLOrderBy = " Order by Department, a.Description ";
            }
            else if (refOrderBy == "3")
            {
                strSQLOrderBy = " Order by Department ";
            }
            else
            {
                strSQLOrderBy = " Order by Department, a.SKU ";
            }

            string strSQLComm =   " select distinct a.ID,a.SKU,a.DecimalPlace,a.PriceA,a.PriceB,a.PriceC,a.FoodStampEligible,a.Description, "
                                + " b.DepartmentID, b.Description AS Department from Product as a left outer join Dept as b on a.DepartmentID = b.ID "
                                + " where (1 = 1) and a.productstatus = 'Y' " + strSQLDepartment + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["PriceA"].ToString(), 
                                                   objSQLReader["PriceB"].ToString(),
                                                   objSQLReader["PriceC"].ToString(),
                                                   objSQLReader["FoodStampEligible"].ToString(),
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
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

        public DataTable FetchDataForProductFamilyReport(DataTable refFamily, string refOrderBy)
        {
            DataTable dtbl = new DataTable();

            string strSQLDepartment = "";
            string strSQLOrderBy = "";

            string getDept = "";


            foreach (DataRow drG in refFamily.Rows)
            {

                if (Convert.ToBoolean(drG["Check"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.BrandID in ( " + getDept + " )";
            }

            if (refOrderBy == "1")
            {
                strSQLOrderBy = " Order by Family, a.SKU ";
            }
            else if (refOrderBy == "2")
            {
                strSQLOrderBy = " Order by Family, a.Description ";
            }
            else
            {
                strSQLOrderBy = " Order by b.BrandDescription, a.SKU ";
            }

            string strSQLComm = " select distinct a.ID,a.SKU,a.DecimalPlace,a.PriceA,a.PriceB,a.PriceC,a.FoodStampEligible,a.Description, "
                                + " isnull(b.BrandID,'') as FamilyID, isnull(b.BrandDescription,'') AS Family from Product as a left outer join BrandMaster as b on a.BrandID = b.ID "
                                + " where (1 = 1) and a.productstatus = 'Y' " + strSQLDepartment + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["PriceA"].ToString(), 
                                                   objSQLReader["PriceB"].ToString(),
                                                   objSQLReader["PriceC"].ToString(),
                                                   objSQLReader["FoodStampEligible"].ToString(),
                                                   objSQLReader["FamilyID"].ToString(),
                                                   objSQLReader["Family"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
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
        
        #region Fetch Data For Kit Product Report
        public DataTable FetchDataForKitProductReport(DataTable refDepartment )
        {
            DataTable dtbl = new DataTable();
            
            string strSQLDepartment = "";

            string getDept = "";

            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }

            
            string strSQLComm =   " select DISTINCT a.ID,a.SKU,a.DecimalPlace,a.QtyOnHand, a.Description,a.ProductType, "
                                + " a.Cost as UnitCost,a.PriceA as UnitRetailValue, "
                                + " Cast((a.QtyOnHand * a.Cost) as Numeric(15,2)) AS ExtCost, "
                                + " Cast((a.QtyOnHand * a.PriceA) as Numeric(15,2)) AS ExtRetailValue, "
                                + " b.DepartmentID, b.Description AS Department, "
                                + " ISNULL(c.VendorID, '') AS VendorID, ISNULL(c.Name, '') AS Vendor, ISNULL(a.DiscountedCost, 0) AS DCost "
                                + " from Product AS a LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID " 
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID AND d.IsPrimary = 'Y' "
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID "
                                + " where (1 = 1) and a.ProductType = 'K' and a.productstatus = 'Y' " + strSQLDepartment
                                + " order by Department ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));

                double dblCurrentMargin = 0;
                while (objSQLReader.Read())
                {
                    dblCurrentMargin = 0;
                    if (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString()) == 0)
                    {
                        dblCurrentMargin = -99999;
                    }
                    else
                    {
                        dblCurrentMargin = (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString())
                                        - Functions.fnDouble(objSQLReader["UnitCost"].ToString())) * 100 / Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString());
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   "Kit",
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["UnitCost"].ToString(),
                                                   objSQLReader["UnitRetailValue"].ToString(),
                                                   objSQLReader["ExtCost"].ToString(),
                                                   objSQLReader["ExtRetailValue"].ToString(),
                                                   dblCurrentMargin.ToString(), 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),
                                                   objSQLReader["DCost"].ToString()});
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

        #region Fetch Kit Item
        public DataTable FetchKitItem(DataTable refDepartment)
        {
            DataTable dtbl = new DataTable();

            string strSQLDepartment = "";

            string getDept = "";

            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }



            string strSQLComm = 
                
                " select DISTINCT a.ID,a.SKU,a.DecimalPlace,a.QtyOnHand, a.Description,a.ProductType,a.Cost as UnitCost,a.PriceA as UnitRetailValue, "
                                + " Cast((a.QtyOnHand * a.Cost) as Numeric(15,2)) AS ExtCost, Cast((a.QtyOnHand * a.PriceA) as Numeric(15,2)) AS ExtRetailValue, "
                                + " b.DepartmentID, b.Description AS Department, "
                                + " ISNULL(c.VendorID, '') AS VendorID, ISNULL(c.Name, '') AS Vendor, "
                                + " ISNULL(k.KitID,a.ID) as KitID, k.ItemID, k.ItemCount, pk.SKU as KitSKU, pk.Description as KitDesc, ISNULL(pk.DiscountedCost, 0) AS DCost "
                                + " from Product AS a LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID " 
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID AND d.IsPrimary = 'Y' "
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID  "
                                + " LEFT OUTER JOIN  Kit k ON a.ID = k.KitID "
                                + " LEFT OUTER JOIN  Product pk ON k.ItemID = pk.ID "
                                + " Where a.ProductType = 'K' " + strSQLDepartment 
                                + " order by Department ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("KitID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("KitSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("KitDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));

                double dblCurrentMargin = 0;
                while (objSQLReader.Read())
                {
                    dblCurrentMargin = 0;
                    if (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString()) == 0)
                    {
                        dblCurrentMargin = -99999;
                    }
                    else
                    {
                        dblCurrentMargin = (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString())
                                        - Functions.fnDouble(objSQLReader["UnitCost"].ToString())) * 100 / Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString());
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   "Kit",
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["UnitCost"].ToString(),
                                                   objSQLReader["UnitRetailValue"].ToString(),
                                                   objSQLReader["ExtCost"].ToString(),
                                                   objSQLReader["ExtRetailValue"].ToString(),
                                                   dblCurrentMargin.ToString(), 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["KitID"].ToString(),
                                                   objSQLReader["KitSKU"].ToString(),
                                                   objSQLReader["KitDesc"].ToString(),
                                                   objSQLReader["ItemCount"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),
                                                    objSQLReader["DCost"].ToString()});
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

        #region Fetch Data For Matrix Product Report
        public DataTable FetchDataForMatrixProductReport(DataTable refDepartment)
        {
            DataTable dtbl = new DataTable();

            string strSQLDepartment = "";

            string getDept = "";

            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }

            string strSQLComm = " select DISTINCT a.ID,a.SKU,a.DecimalPlace,a.QtyOnHand, a.Description,a.ProductType, "
                                + " a.Cost as UnitCost,a.PriceA as UnitRetailValue, "
                                + " Cast((a.QtyOnHand * a.Cost) as Numeric(15,2)) AS ExtCost, "
                                + " Cast((a.QtyOnHand * a.PriceA) as Numeric(15,2)) AS ExtRetailValue, "
                                + " b.DepartmentID, b.Description AS Department, "
                                + " ISNULL(c.VendorID, '') AS VendorID, ISNULL(c.Name, '') AS Vendor "
                                + " from Product AS a LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID "
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID AND d.IsPrimary = 'Y' "
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID "
                                + " where (1 = 1) and a.ProductType = 'M' and a.productstatus = 'Y' " + strSQLDepartment
                                + " order by Department ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));

                double dblCurrentMargin = 0;
                while (objSQLReader.Read())
                {
                    dblCurrentMargin = 0;
                    if (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString()) == 0)
                    {
                        dblCurrentMargin = -99999;
                    }
                    else
                    {
                        dblCurrentMargin = (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString())
                                        - Functions.fnDouble(objSQLReader["UnitCost"].ToString())) * 100 / Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString());
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   "Matrix",
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["UnitCost"].ToString(),
                                                   objSQLReader["UnitRetailValue"].ToString(),
                                                   objSQLReader["ExtCost"].ToString(),
                                                   objSQLReader["ExtRetailValue"].ToString(),
                                                   dblCurrentMargin.ToString(), 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString()});
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

        #region Fetch Matrix Item
        public DataTable FetchMatrixItem(DataTable refDepartment)
        {
            DataTable dtbl = new DataTable();

            string strSQLDepartment = "";

            string getDept = "";

            foreach (DataRow drG in refDepartment.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and a.DepartmentID in ( " + getDept + " )";
            }



            string strSQLComm =

                " select DISTINCT a.ID,a.SKU,a.QtyOnHand,a.DecimalPlace,a.Description,a.ProductType,a.Cost as UnitCost,a.PriceA as UnitRetailValue, "
                                + " Cast((a.QtyOnHand * a.Cost) as Numeric(15,2)) AS ExtCost, Cast((a.QtyOnHand * a.PriceA) as Numeric(15,2)) AS ExtRetailValue, "
                                + " b.DepartmentID, b.Description AS Department, "
                                + " ISNULL(c.VendorID, '') AS VendorID, ISNULL(c.Name, '') AS Vendor, "
                                + " ISNULL(mo.ProductID,a.ID) as MatrixPID, ISNULL(mo.Option1Name,'') as Option1Name, "
                                + " ISNULL(mo.Option2Name,'') as Option2Name, ISNULL(mo.Option3Name,'') as Option3Name, "
                                + " ISNULL(m.OptionValue1,'') as OptionValue1, ISNULL(m.OptionValue2,'') as OptionValue2, "
                                + " ISNULL(m.OptionValue3,'') as OptionValue3,ISNULL(m.QtyOnHand,'-99999') as Qty "
                                + " from Product AS a LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID "
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID AND d.IsPrimary = 'Y' "
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID  "
                                + " LEFT OUTER JOIN  MatrixOptions mo ON a.ID = mo.ProductID "
                                + " LEFT OUTER JOIN  Matrix m ON m.MatrixOptionID = mo.ID "
                                + " Where a.ProductType = 'M' " + strSQLDepartment
                                + " order by Department ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtRetailValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CurrentMargin", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MatrixID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option3Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));

                double dblCurrentMargin = 0;
                while (objSQLReader.Read())
                {
                    dblCurrentMargin = 0;
                    if (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString()) == 0)
                    {
                        dblCurrentMargin = -99999;
                    }
                    else
                    {
                        dblCurrentMargin = (Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString())
                                        - Functions.fnDouble(objSQLReader["UnitCost"].ToString())) * 100 / Functions.fnDouble(objSQLReader["UnitRetailValue"].ToString());
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   "Matrix",
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["UnitCost"].ToString(),
                                                   objSQLReader["UnitRetailValue"].ToString(),
                                                   objSQLReader["ExtCost"].ToString(),
                                                   objSQLReader["ExtRetailValue"].ToString(),
                                                   dblCurrentMargin.ToString(), 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["MatrixPID"].ToString(),
                                                   objSQLReader["Option1Name"].ToString(),
                                                   objSQLReader["Option2Name"].ToString(),
                                                   objSQLReader["Option3Name"].ToString(),
                                                   objSQLReader["OptionValue1"].ToString(),
                                                   objSQLReader["OptionValue2"].ToString(),
                                                   objSQLReader["OptionValue3"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString()});
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

        #region Fetch Data For Reorder Report
        public DataTable FetchDataForReorderReport(string refMainCriteria, string refSubCriteria1, string refSubCriteria2, 
                                                    DataTable refDataTable, string refOrderBy,bool activeitem)
        {
            DataTable dtbl = new DataTable();
            string strSQLactiveFilter = "";
            string strSQLvendorFilter = "";
            string strSQLdeptFilter = "";
            string strSQLcatFilter = "";
            string strSQLOrderBy = "";
            string strSQLPrimaryVendorFilter = "";
            string strSQLReorderFilter = "";
            string getValueFromdtbl = "";

            if (activeitem) strSQLactiveFilter = " and a.productstatus = 'Y' ";

            foreach (DataRow dr in refDataTable.Rows)
            {

                if (Convert.ToBoolean(dr["CheckFlag"].ToString()))
                {
                    if (getValueFromdtbl == "")
                    {
                        getValueFromdtbl = dr["ID"].ToString();
                    }
                    else
                    {
                        getValueFromdtbl = getValueFromdtbl + "," + dr["ID"].ToString();
                    }
                }
            }
            if (getValueFromdtbl != "")
            {
                if (refMainCriteria == "0")
                {
                    strSQLvendorFilter = " and c.ID in ( " + getValueFromdtbl + " )";
                }
                if (refMainCriteria == "1")
                {
                    strSQLdeptFilter = " and a.DepartmentID in ( " + getValueFromdtbl + " )";
                }
                if (refMainCriteria == "2")
                {
                    strSQLcatFilter = " and a.CategoryID in ( " + getValueFromdtbl + " )";
                }
            }

            if (refMainCriteria == "0") // Vendor
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by VendorID,a.SKU ";
                }
                if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by VendorID,a.Description ";
                }
                if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by VendorID,VendorPartNumber ";
                }
            }

            if (refMainCriteria == "1") // Department
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by DepartmentID,a.SKU ";
                }
                if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by DepartmentID,a.Description ";
                }
                if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by DepartmentID,VendorPartNumber ";
                }
            }

            if (refMainCriteria == "2") // Category
            {
                if (refOrderBy == "1")
                {
                    strSQLOrderBy = " Order by CategoryID,a.SKU ";
                }
                if (refOrderBy == "2")
                {
                    strSQLOrderBy = " Order by CategoryID,a.Description ";
                }
                if (refOrderBy == "3")
                {
                    strSQLOrderBy = " Order by CategoryID,VendorPartNumber ";
                }
            }

            if (refMainCriteria == "0")
            {
                if (refSubCriteria1 == "Y")
                {
                    strSQLPrimaryVendorFilter = " and d.IsPrimary = 'Y' ";
                }
            }

            if (refSubCriteria2 == "Y")
            {
                strSQLReorderFilter = " and a.QtyOnHand < a.ReorderQty ";
            }

            string strSQLComm =   " select DISTINCT a.ID,a.SKU,a.Description,a.QtyOnHand,a.ReorderQty, a.NormalQty, "
                                + " a.Cost as UnitCost, a.DecimalPlace as DP,a.CaseQty,a.CaseUPC,a.PrintBarCode, "
                                + " dpt.DepartmentID as DepartmentID, dpt.Description AS Department, "
                                + " ISNULL(cat.CategoryID,'') as CategoryID, ISNULL(cat.Description, '') AS Category, "
                                + " ISNULL(d.PartNumber,'') as VendorPartNumber, ISNULL(c.VendorID, '') AS VendorID, "
                                + " ISNULL(c.Name, '') AS Vendor, ISNULL(c.Contact,'') as VContact, "
                                + " ISNULL(c.Phone,'') as VPhone, ISNULL(c.Fax,'') as VFax "
                                + " from Product AS a "
                                + " LEFT OUTER JOIN  Dept AS dpt ON a.DepartmentID = dpt.ID " 
                                + " LEFT OUTER JOIN  Category AS cat ON a.CategoryID = cat.ID " 
                                + " LEFT OUTER JOIN  VendPart AS d ON a.ID = d.ProductID " 
                                + " LEFT OUTER JOIN  Vendor AS c ON d.VendorID = c.ID "
                                + "  Where (1 = 1) " + strSQLactiveFilter + strSQLdeptFilter + strSQLcatFilter + strSQLvendorFilter 
                                + strSQLPrimaryVendorFilter + strSQLReorderFilter + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPartNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SuggOrderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ActualOrderQty", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VContact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VFax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    double dblQtyOnHand = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString());
                    double dblReorderQty = Functions.fnDouble(objSQLReader["ReorderQty"].ToString());
                    double dblNormalQty = Functions.fnDouble(objSQLReader["NormalQty"].ToString());
                    double dblUnitCost = Functions.fnDouble(objSQLReader["UnitCost"].ToString());
                    double dblExtCost = 0;
                    double dblSuggOrderQty = 0;
                    if (dblQtyOnHand < dblReorderQty) dblSuggOrderQty = dblNormalQty - dblQtyOnHand;
                    dblExtCost = (dblNormalQty - dblQtyOnHand) * dblUnitCost;


                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["VendorPartNumber"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["QtyOnHand"].ToString(), 
                                                   objSQLReader["ReorderQty"].ToString(),
                                                   objSQLReader["NormalQty"].ToString(),
                                                   objSQLReader["UnitCost"].ToString(),
                                                   dblExtCost.ToString("n"),
                                                   dblSuggOrderQty.ToString("n"), 
                                                   "", 
                                                   objSQLReader["DepartmentID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["CategoryID"].ToString(),
                                                   objSQLReader["Category"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["VContact"].ToString(),
                                                   objSQLReader["VPhone"].ToString(),
                                                   objSQLReader["VFax"].ToString(),
                                                   objSQLReader["DP"].ToString(),
                                                   objSQLReader["CaseQty"].ToString(),
                                                   objSQLReader["CaseUPC"].ToString(),
                                                   objSQLReader["PrintBarCode"].ToString() });
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

        #region Display Item in POS

        public int GetPOSProductsforCategory(int CatID)
        {
            int intCount = 0;
            string strSQLComm =   " select COUNT(*) AS RECCOUNT from PRODUCT where AddtoPOSScreen = @AddtoPOSScreen AND CATEGORYID = @CATEGORYID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = "Y";

                objSQlComm.Parameters.Add(new SqlParameter("@CATEGORYID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CATEGORYID"].Value = CatID;

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

        public int GetMaxPOSforCategory(int CatID)
        {
            int intCount = 0;
            string strSQLComm = " select MAXITEMSFORPOS from CATEGORY where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = CatID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["MAXITEMSFORPOS"]);
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
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

        #endregion

        #region Update Product Price
        public string UpdateProductPrice()
        {
            string strSQLComm = "";
            if (blFunctionButtonAccess)
            {
                strSQLComm = " update product set PriceA=@PriceA,PriceB=@PriceB,PriceC=@PriceC,"
                           + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn Where ID = @ID";
            }
            else
            {
                strSQLComm = " update product set PriceA=@PriceA,PriceB=@PriceB,PriceC=@PriceC,"
                           + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                           + " ChangedByAdmin=@ChangedByAdmin, ChangedOnAdmin=@ChangedOnAdmin Where ID = @ID";
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                
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


        public int UpdateBookerProductPrice(string UpdateID, double UpdatedPrice)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_UpdateBookerProductPrice";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SKU"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU"].Value = UpdateID;

                objSQlComm.Parameters.Add(new SqlParameter("@UpdatedPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@UpdatedPrice"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@UpdatedPrice"].Value = UpdatedPrice;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LastChangedBy"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;

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

        #region Get Product Cost

        public double GetProductCost(int intpID)
        {
            double dblCost = 0;
            string strSQLComm = " select cost from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        dblCost = Functions.fnDouble(objsqlReader["cost"].ToString());
                    }
                    catch 
                    { 
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblCost;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }
        #endregion

        public double GetProductPrice(int intpID)
        {
            double dblCost = 0;
            string strSQLComm = " select priceA from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        dblCost = Functions.fnDouble(objsqlReader["priceA"].ToString());
                    }
                    catch
                    {
                    }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblCost;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public double GetSplitPrice(int intpID)
        {
            double dblCost = 0;
            string strSQLComm = " select priceB from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        dblCost = Functions.fnDouble(objsqlReader["priceB"].ToString());
                    }
                    catch
                    {
                    }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblCost;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public double GetSplitWeight(int intpID)
        {
            double dblCost = 0;
            string strSQLComm = " select SplitWeight from PRODUCT where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        dblCost = Functions.fnDouble(objsqlReader["SplitWeight"].ToString());
                    }
                    catch
                    {
                    }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblCost;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string GetProductName(int intpID)
        {
            string val = "";
            string strSQLComm = " select description from product where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        val = objsqlReader["description"].ToString();
                    }
                    catch
                    {
                    }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
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

       


        #region Insert Serialized Header Data
        public string InsertSerializedHeaderData()
        {
            string strSQLComm =   " insert into SerialHeader( ProductID, AllowPOSNew, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @ProductID, @AllowPOSNew, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowPOSNew", System.Data.SqlDbType.Char));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = intSerialProductID;
                objSQlComm.Parameters["@AllowPOSNew"].Value = strAllowPOSNew;
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
                    objsqlReader.Close();
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
        #endregion

        #region Update Serialized Header Data
        public string UpdateSerializedHeaderData()
        {
            string strSQLComm = " update SerialHeader set AllowPOSNew=@AllowPOSNew,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowPOSNew", System.Data.SqlDbType.VarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@AllowPOSNew"].Value = strAllowPOSNew;
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
        
        #region Insert Serialized Data

        public string InsertSerializedData()
        {
            string strSQLComm = " insert into SerialDetail( SerialHeaderID, Serial1, Serial2, Serial3, ExpiryDate,"
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @SerialHeaderID, @Serial1, @Serial2, @Serial3, @ExpiryDate, "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime));
                if (dtExpiryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = dtExpiryDate;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SerialHeaderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SerialHeaderID"].Value = intSerialHeaderID;
                objSQlComm.Parameters["@Serial1"].Value = strSerial1;
                objSQlComm.Parameters["@Serial2"].Value = strSerial2;
                objSQlComm.Parameters["@Serial3"].Value = strSerial3;
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
                    objsqlReader.Close();
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

        #endregion

        #region Update Serialized Data
        public string UpdateSerializedData()
        {
            string strSQLComm = " update SerialDetail set Serial1=@Serial1, Serial2=@Serial2, Serial3=@Serial3, ExpiryDate = @ExpiryDate,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime));
                if (dtExpiryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = dtExpiryDate;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Serial3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Serial1"].Value = strSerial1;
                objSQlComm.Parameters["@Serial2"].Value = strSerial2;
                objSQlComm.Parameters["@Serial3"].Value = strSerial3;
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

        #region Fetch Serialized data
        public DataTable FetchSerializedData(string Cat, int pSID, string dtFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (Cat == "ALL")
            {
                strSQLComm = " select d.ID, d.SerialHeaderID,d.Serial1,d.Serial2,d.Serial3,d.ItemID, d.ExpiryDate from SerialDetail d "
                                  + " left outer join SerialHeader h on d.SerialHeaderID = h.ID "
                                  + " where h.ProductID=@SID order by d.Serial1 ";
            }

            if (Cat == "POS")
            {
                strSQLComm = " select d.ID, d.SerialHeaderID,d.Serial1,d.Serial2,d.Serial3,d.ItemID, d.ExpiryDate from SerialDetail d "
                                  + " left outer join SerialHeader h on d.SerialHeaderID = h.ID "
                                  + " where h.ProductID=@SID and d.ItemID = 0 order by d.Serial1 ";
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@SID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@SID"].Value = pSID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SerialHeaderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sold", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("ExpiryDate", System.Type.GetType("System.String"));
                bool blsold = false;
                while (objSQLReader.Read())
                {
                    blsold = false;
                    if (Functions.fnInt32(objSQLReader["ItemID"].ToString()) > 0) blsold = true;
                    else blsold = false;
                        dtbl.Rows.Add(new object[] {  objSQLReader["ID"].ToString(),
												      objSQLReader["SerialHeaderID"].ToString(),
												      objSQLReader["Serial1"].ToString(),
                                                      objSQLReader["Serial2"].ToString(),
                                                      objSQLReader["Serial3"].ToString(),
                                                      blsold,
                                                      objSQLReader["ExpiryDate"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["ExpiryDate"].ToString()).ToString(dtFormat)});
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

        #region Fetch Serialized header data
        public DataTable FetchSerializedHeaderData(int pSID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, AllowPOSNew from SerialHeader "
                              + " where ProductID=@SID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@SID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@SID"].Value = pSID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AllowPOSNew", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["ID"].ToString(),
                                                 objSQLReader["AllowPOSNew"].ToString() });
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

        #region Show Serialized Record based on ID
        public DataTable ShowSerializedRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from SerialDetail where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Serial3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpiryDate", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["Serial1"].ToString(),
												objSQLReader["Serial2"].ToString(),
                                                objSQLReader["Serial3"].ToString(),
                                                objSQLReader["ExpiryDate"].ToString()});
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

        #region Duplicate Serialized Checking

        public int DuplicateSerializedCount(string sr1)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from SerialDetail where Serial1=@Serial1 ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@Serial1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Serial1"].Value = sr1;
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
        #endregion
        
        #region Serialized Recrord Checking

        public int GetSerializedCount(int PID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from SerialHeader where  "
                                + " ProductID=@PID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = PID;
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
        #endregion

        #region Serialized Serialized POS Add Flag

        public string SerializedPOSAdd(int PID)
        {
            string intCount = "";
            string strSQLComm = " select AllowPOSNew from SerialHeader where  "
                                + " ProductID=@PID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = PID;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = objsqlReader["AllowPOSNew"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
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
        #endregion

        #region Delete Serialized Data
  
        public string DeleteSerializedRecord(int DeleteID)
        {
            string strSQLComm = " Delete from SerialDetail Where ID = @ID";

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
        
        #region Food Stamp Flag

        public string GetFoodStampFlag(int PID)
        {
            string intCount = "";
            string strSQLComm = " select isnull(FoodStampEligible,'N') as FS from Product where ID=@PID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = PID;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = objsqlReader["FS"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
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
        #endregion
        
        #region Fetch Data For Serialized Product Report

        public DataTable FetchDataForSerializedProductReport(DataTable refProduct)
        {
            DataTable dtbl = new DataTable();

            string strSQLDepartment = "";

            string getDept = "";

            foreach (DataRow drG in refProduct.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getDept == "")
                    {
                        getDept = drG["ID"].ToString();
                    }
                    else
                    {
                        getDept = getDept + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getDept != "")
            {
                strSQLDepartment = " and s.ProductID in ( " + getDept + " )";
            }


            string strSQLComm =   " select DISTINCT a.ID,a.SKU,a.Description,isnull(b.Serial1,'') as SL1,isnull(b.Serial2,'') as SL2,"
                                + " isnull(b.Serial3,'') as SL3,i.InvoiceNo  "
                                + " from SerialHeader s LEFT OUTER JOIN  Product AS a ON s.ProductID = a.ID "
                                + " LEFT OUTER JOIN  SerialDetail AS b ON s.ID = b.SerialHeaderID "
                                + " LEFT OUTER JOIN item i ON b.ItemID = i.ID "
                                + " where (1 = 1) and a.ProductType = 'E' and a.productstatus = 'Y' " + strSQLDepartment
                                + " order by a.SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["SL1"].ToString(), 
                                                   objSQLReader["SL2"].ToString(),
                                                   objSQLReader["SL3"].ToString(),
                                                   objSQLReader["InvoiceNo"].ToString()});
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

        #region Fetch Serial Product data
        public DataTable FetchSerialProductData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,SKU,Description  "
                               + " from Product where producttype = 'E' order by sku  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString()});
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
        
        #region Fetch Print Label data
        public DataTable FetchPrintlabelData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select PL.ID as PLID, P.ID,P.SKU,P.Description,P.NoPriceOnLabel,P.DecimalPlace,P.PriceA,PL.Qty,P.LabelType "
                              + " from PrintLabel PL left outer join Product P on  "
                              + " P.ID = PL.ProductID order by P.SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("PLID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    int indx = objSQLReader["Qty"].ToString().IndexOf(".");
                    string qty = objSQLReader["Qty"].ToString().Remove(indx);
                    dtbl.Rows.Add(new object[] {   objSQLReader["PLID"].ToString(),
                                                   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   qty,objSQLReader["NoPriceOnLabel"].ToString(),
                                                   objSQLReader["DecimalPlace"].ToString(),
                                                   objSQLReader["PriceA"].ToString(),
                                                   objSQLReader["LabelType"].ToString()});
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

        public string DeletePrintLabel(int DeleteID)
        {
            string strSQLComm = " Delete from PrintLabel Where ID = @ID";

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

        public string ClearPrintLabel()
        {
            string strSQLComm = " update PrintLabel set Qty = 0 ";

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

        public int CountValidPrintLabelRecord()
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from PrintLabel where qty > 0   ";

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

        public string UpdatePrintLabelQty( int pID, double pQty )
        {
            string strSQLComm = " update PrintLabel set Qty=@Qty,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@Qty"].Value = pQty;
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
        
        #region Insert Tagged Data
        public string InsertTaggedData()
        {
            string strSQLComm = " insert into AttachTag( TagID, ItemID, ItemQty,ItemPrice, "
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @TagID, @ItemID, @ItemQty,@ItemPrice, "
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

                objSQlComm.Parameters.Add(new SqlParameter("@TagID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@TagID"].Value = intKitID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ItemQty"].Value = intItemCount;
                objSQlComm.Parameters["@ItemPrice"].Value = dblPriceA;
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
                    objsqlReader.Close();
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
        #endregion

        #region Update Tagged Data
        public string UpdateTaggedData()
        {
            string strSQLComm = " update AttachTag set ItemID=@ItemID,ItemQty=@ItemQty,ItemPrice=@ItemPrice, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn ";
            strSQLComm = strSQLComm + " Where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ItemQty"].Value = intItemCount;
                objSQlComm.Parameters["@ItemPrice"].Value = dblPriceA;
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

        #region Fetch Tagged data
        public DataTable FetchTaggedData(int pTag)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.ID,TagID,ItemID,ItemQty,ItemPrice, b.SKU, b.Description from AttachTag a "
                              + " left outer join Product b on a.ItemID = b.ID "
                              + " where TagID=@TagID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@TagID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@TagID"].Value = pTag;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TagID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemPrice", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ItemName", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["TagID"].ToString(),
												   objSQLReader["ItemID"].ToString(),
                                                   objSQLReader["ItemQty"].ToString(),
                                                   Functions.fnDouble(objSQLReader["ItemPrice"].ToString()),
                                                   objSQLReader["Description"].ToString()});
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

        #region Show Tagged Record based on ID
        public DataTable ShowTaggedRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from AttachTag where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TagID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemPrice", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["TagID"].ToString(),
												objSQLReader["ItemID"].ToString(),
                                                objSQLReader["ItemQty"].ToString(),
                                                objSQLReader["ItemPrice"].ToString() });
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

        #region Duplicate Tagged Checking

        public int DuplicateTaggedCount(int pTag, int pItem)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from AttachTag where  "
                                + " TagID=@TagID and ItemID=@ItemID  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@TagID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@TagID"].Value = pTag;
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemID"].Value = pItem;

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

        #endregion
        
        public int DeleteTaggedCount(int pTag, int pItem)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Item i where i.productid = @TagID "
                                + " and i.producttype = 'T' and i.id in ( select i1.descid from item i1 where i1.productid = @ItemID ) ";
                               
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@TagID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@TagID"].Value = pTag;
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemID"].Value = pItem;

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

        #region Delete Tagged Data
        public string DeleteTaggedRecord(int DeleteID)
        {
            string strSQLComm = " Delete from AttachTag Where ID = @ID";

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

        public DataTable ShowBreakPacks(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select l.SKU as bpsku, l.Description as bpdesc, l.BreakPackRatio as bpratio from "
                              + " product l left outer join product p on l.LinkSKU = p.ID where p.ID = @ID order by l.BreakPackRatio asc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Ratio", System.Type.GetType("System.Double"));
                double ratio = 0;

                while (objSQLReader.Read())
                {
                    ratio = 0;
                    if (objSQLReader["bpratio"].ToString() != "")
                    {
                        ratio = Functions.fnDouble(objSQLReader["bpratio"].ToString());
                    }


                    dtbl.Rows.Add(new object[] {
												objSQLReader["bpsku"].ToString(),
                                                objSQLReader["bpdesc"].ToString(),
                                                ratio});
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

        public DataTable ShowPrintersForRemotePrint(int ProdId, string MType, double pval)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.PrinterName from PrinterMapping TM left outer join Printers T on TM.PrinterID = T.ID "
                              + " where TM.MappingID = @ID and TM.MappingType = @MappingType and TM.CutOffValue < @Val ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@MappingType"].Value = MType;
            objSQlComm.Parameters.Add(new SqlParameter("@Val", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@Val"].Value = pval;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CutOffValue", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["PrinterID"].ToString(),
                                                objSQLReader["PrinterName"].ToString(),
                                                objSQLReader["CutOffValue"].ToString()});

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

        public DataTable ShowPrinters(int ProdId, string MType)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.PrinterName from PrinterMapping TM left outer join Printers T on TM.PrinterID = T.ID "
                              + " where TM.MappingID = @ID and TM.MappingType = @MappingType ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@MappingType"].Value = MType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CutOffValue", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["PrinterID"].ToString(),
                                                objSQLReader["PrinterName"].ToString(),
                                                objSQLReader["CutOffValue"].ToString()});

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


        public DataTable ShowPrinters1(int ProdId, string MType)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.PrinterName from PrinterMapping TM left outer join Printers T on TM.PrinterID = T.ID "
                              + " where TM.MappingID = @ID and TM.MappingType = @MappingType ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@MappingType"].Value = MType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterID", System.Type.GetType("System.String"));
                //dtbl.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CutOffValue", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["PrinterID"].ToString(),
                                                //objSQLReader["PrinterName"].ToString(),
                                                objSQLReader["CutOffValue"].ToString()});

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

        public string SavePrinters(SqlCommand objSQlComm)
        {
            if (dblPrinterDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblPrinterDataTable.Rows)
            {
                if (dr["PrinterID"].ToString() == "") continue;
                int intPrinterID = 0;
                int intcutoffval = 0;
                if (dr["PrinterID"].ToString() != "")
                    intPrinterID = Functions.fnInt32(dr["PrinterID"].ToString());
                intcutoffval = Functions.fnInt32(dr["CutOffValue"].ToString());
                if (strResult == "")
                {
                    strResult = InsertProductPrinters(objSQlComm, intPrinterID, intcutoffval);
                }
            }
            return strResult;
        }

        public string InsertProductPrinters(SqlCommand objSQlComm, int PrinterID, int COval)
        {
            string strSQLComm = " insert into PrinterMapping (PrinterID,MappingID,MappingType,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,CutOffValue)"
                              + " values(@PrinterID,@MappingID,@MappingType,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@CutOffValue)";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CutOffValue", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PrinterID"].Value = PrinterID;
                objSQlComm.Parameters["@MappingID"].Value = intID;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@MappingType"].Value = "Product";
                objSQlComm.Parameters["@CutOffValue"].Value = COval;
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

        public string DeletePrinters(SqlCommand objSQlComm)
        {
            string strSQLComm = " delete from PrinterMapping where MappingID = @MappingID and MappingType = 'Product' ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MappingID"].Value = intID;

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

        public DataTable GetBreakPackItemFromSKU(int pSKU)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from product where linksku = @SKU order by BreakPackRatio desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@SKU"].Value = pSKU;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["Description"].ToString(),
												Functions.fnDouble(objSQLReader["BreakPackRatio"].ToString())});
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

        public DataTable GetActiveAssignedServices(int pEmpID,int pCatID, int pDeptID)
        {
            DataTable dtbl = new DataTable();
            string sqlCatFilter = "";
            string sqlDeptFilter = "";
            if (pCatID > 0)
            {
                sqlCatFilter = " and p.CategoryID = @CatID ";
            }
            if (pDeptID > 0)
            {
                sqlDeptFilter = " and p.DepartmentID = @DeptID ";
            }
            string strSQLComm = " select p.ID, p.SKU, p.Description, p.MinimumServiceTime from product p left outer join servicemapping s "
                              + " on s.ServiceID = p.ID where p.ProductType = 'S' and p.ProductStatus = 'Y' "
                              + " and s.MappingType = 'Employee' and s.MappingID = @MID " +sqlCatFilter + sqlDeptFilter 
                              + "order by p.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@MID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@MID"].Value = pEmpID;
            if (pCatID > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@CatID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CatID"].Value = pCatID;
            }
            if (pDeptID > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@DeptID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DeptID"].Value = pDeptID;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceCheck", System.Type.GetType("System.Boolean"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["Description"].ToString(),
												objSQLReader["MinimumServiceTime"].ToString(),false});
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

        public double GetProductRetailPrice(int intpID)
        {
            double val = 0;
            string strSQLComm = " select PriceA from product where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnDouble(objsqlReader["PriceA"].ToString());
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable ShowTaxes(int ProdId, string MType)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from taxmapping TM Left Outer Join TaxHeader T on TM.TaxID = T.ID "
                                + " where TM.MappingID = @ID and TM.MappingType = @MappingType ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@MappingType"].Value = MType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MAPPING", System.Type.GetType("System.String"));

                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["TAXID"].ToString(),
                                                strTaxName,
                                                objSQLReader["TaxRate"].ToString(),
                                                MType});

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


        // 03-12-2013    Reorder Items in POS – Retail 

        public int GetItemCountInGroup(int gid)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from product where categoryid = @grp ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@grp", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@grp"].Value = gid;
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

        public int GetScaleItemCountInGroup(string gid)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from scale_product where cat_id = @grp ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@grp", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@grp"].Value = gid;
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


        // 03-12-2013    Reorder Items in POS – Retail 

        public DataTable FetchPOSDisplayOrderData(int pgrp)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,SKU,Description,POSDisplayOrder,POSVisible from product where categoryid = @grp order by POSDisplayOrder,Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@grp", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@grp"].Value = pgrp;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSVisible", System.Type.GetType("System.String"));
                int i = 0;
                while (objSQLReader.Read())
                {
                    i++;
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   Functions.fnInt32(objSQLReader["POSDisplayOrder"].ToString()) == 0 ? i.ToString() : objSQLReader["POSDisplayOrder"].ToString(),
                                                   objSQLReader["POSVisible"].ToString() == "Y" ? "Yes" : "No"
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

        public DataTable FetchScaleDisplayOrderData(string pgrp)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select Distinct s.ProductID,p.SKU,p.Description,s.ScaleDisplayOrder,p.AddToScaleScreen from scale_product s "
                              + " left outer join product p on p.id = s.productID where s.cat_id = @grp order by s.ScaleDisplayOrder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@grp", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@grp"].Value = pgrp;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSVisible", System.Type.GetType("System.String"));
                int i = 0;
                while (objSQLReader.Read())
                {
                    i++;
                    dtbl.Rows.Add(new object[] {   objSQLReader["ProductID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   Functions.fnInt32(objSQLReader["ScaleDisplayOrder"].ToString()) == 0 ? i.ToString() : objSQLReader["ScaleDisplayOrder"].ToString(),
                                                   objSQLReader["AddToScaleScreen"].ToString() == "Y" ? "Yes" : "No"
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

        // 03-12-2013    Reorder Items in POS – Retail 

        #region  Update POS Display Order

        public int UpdatePosDisplayOrder(int pID, int pGroupID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_RearrangeItemDisplayOrder";

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
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@GroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@GroupID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@GroupID"].Value = pGroupID;

                objSQlComm.Parameters.Add(new SqlParameter("@ChangedOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ChangedOrder"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ChangedOrder"].Value = pOrder;

                objSQlComm.Parameters.Add(new SqlParameter("@Position", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Position"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Position"].Value = pType;

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

        public int UpdateScaleDisplayOrder(int pID, string pGroupID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_RearrangeItemScaleDisplayOrder";

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
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@GroupID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@GroupID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@GroupID"].Value = pGroupID;

                objSQlComm.Parameters.Add(new SqlParameter("@ChangedOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ChangedOrder"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ChangedOrder"].Value = pOrder;

                objSQlComm.Parameters.Add(new SqlParameter("@Position", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Position"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Position"].Value = pType;

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
        
        public string UpdateItemPOSVisible(int pID,string pVisible)
        {
            string strSQLComm = " update product set POSVisible=@param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pVisible;
               
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

        public string UpdateItemScaleVisible(int pID, string pVisible)
        {
            string strSQLComm = " update product set AddToScaleScreen=@param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pVisible;

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

        public int GetProductBrandID(int intpID)
        {
            int val = 0;
            string strSQLComm = " select BrandID from product where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["BrandID"].ToString());
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public void ScaleConnOnPriceChange(SqlCommand objSQlComm)
        {
            string strSQLComm = "sp_scale_comm_on_price_change";

            try
            {
                objSQlComm.Parameters.Clear();
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PID"].Value = intID;

                objSQlComm.Parameters.Add(new SqlParameter("@PR", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PR"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PR"].Value = dblPriceA;

                objSQlComm.ExecuteNonQuery();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return;
            }
        }

        public void ActiveSale(int pItem, ref int rid, ref string rbatch, ref string rapply, ref string rperiod, ref double rprice, string configDateFormat)
        {
            string strSQLComm = "";

            strSQLComm = " select h.SaleBatchName,h.EffectiveFrom,h.EffectiveTo,h.ID,d.SalePrice,d.ApplyFamily from salepriceheader h "
                       + " left outer join salepricedetails d on h.ID = d.SaleBatchID where (1 = 1) "
                       + " and ((d.ItemID = @id1 and d.ApplyFamily = 'N') or (d.ItemID = (select BrandID from Product where ID = @id2) and d.ApplyFamily = 'Y')) "
                       + " and d.salebatchid in ( select a.id from salepriceheader a where a.salestatus = 'Y' and getdate() between a.effectivefrom and a.effectiveto) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@id1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id1"].Value = pItem;
                objSQlComm.Parameters.Add(new SqlParameter("@id2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id2"].Value = pItem;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    rid = Functions.fnInt32(objSQLReader["ID"].ToString());
                    rbatch = objSQLReader["SaleBatchName"].ToString();
                    rapply = objSQLReader["ApplyFamily"].ToString() == "Y" ? "Family" : "Item";
                    rperiod = Functions.fnDate(objSQLReader["EffectiveFrom"].ToString()).ToString((configDateFormat) + "  hh:mm tt") + "   to   " +
                        Functions.fnDate(objSQLReader["EffectiveTo"].ToString()).ToString((configDateFormat) + "  hh:mm tt");
                    rprice = Functions.fnDouble(objSQLReader["SalePrice"].ToString());
                    break;
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

        public DataTable ActiveFuturePrice(int pItem, string configDateFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select h.FutureBatchName,h.EffectiveFrom,h.ID,d.FuturePriceA,d.FuturePriceB,d.FuturePriceC,d.ApplyFamily from futurepriceheader h "
                       + " left outer join futurepricedetails d on h.ID = d.FutureBatchID where (1 = 1) "
                       + " and ((d.ItemID = @id1 and d.ApplyFamily = 'N') or (d.ItemID = (select BrandID from Product where ID = @id2) and d.ApplyFamily = 'Y')) "
                       + " and d.futurebatchid in ( select a.id from futurepriceheader a where a.futurebatchstatus = 'Y' and a.isset = 'N' and getdate() < a.effectivefrom) order by h.EffectiveFrom ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@id1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id1"].Value = pItem;
                objSQlComm.Parameters.Add(new SqlParameter("@id2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id2"].Value = pItem;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Batch", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["ID"].ToString(), 
                                                 objSQLReader["FutureBatchName"].ToString(),
                                                 Functions.fnDate(objSQLReader["EffectiveFrom"].ToString()).ToString((configDateFormat) + "  hh:mm tt"), 
                                                 Functions.fnDouble(objSQLReader["FuturePriceA"].ToString()).ToString("f"),
                                                 Functions.fnDouble(objSQLReader["FuturePriceB"].ToString()).ToString("f"),
                                                 Functions.fnDouble(objSQLReader["FuturePriceC"].ToString()).ToString("f") });
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

        public DataTable ActiveMixMatch(int pItem, string configDateFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select h.ID,h.DiscountName,h.OfferStartOn,h.OfferEndOn,h.DiscountCategory,h.DiscountType,h.DiscountAmount,"
                       + " h.DiscountPercentage,h.DiscountPlusQty,h.AbsolutePrice,h.LimitedPeriodCheck "
                       + " from MixNmatchHeader h left outer join MixNmatchDetails d on h.ID = d.HeaderID where (1 = 1) and h.DiscountStatus = 'Y' and d.ItemID = @id "
                       + " and ((h.LimitedPeriodCheck = 'N') or ((h.LimitedPeriodCheck = 'Y') and (getdate() between h.OfferStartOn and h.OfferEndOn ))) order by h.OfferStartOn ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id"].Value = pItem;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MixMatch", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Value", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Count", System.Type.GetType("System.String"));

                string cat = "";
                string valtype = "";
                string val = "";
                while (objSQLReader.Read())
                {
                    cat = "";
                    valtype = "";
                    val = "";
                    if (objSQLReader["DiscountCategory"].ToString() == "A")
                    {
                        cat = "Absolute Pricing";
                        valtype = "Absolute";
                        val = Functions.fnDouble(objSQLReader["AbsolutePrice"].ToString()).ToString("f");
                    }
                    else
                    {
                        cat = objSQLReader["DiscountCategory"].ToString() == "N" ? "Normal Pricing" : "+ Pricing";
                        valtype = objSQLReader["DiscountType"].ToString() == "A" ? "Amount off" : "% off";
                        val = objSQLReader["DiscountType"].ToString() == "A" ? Functions.fnDouble(objSQLReader["DiscountAmount"].ToString()).ToString("f")
                                                 : Functions.fnDouble(objSQLReader["DiscountPercentage"].ToString()).ToString("f");
                    }
                    dtbl.Rows.Add(new object[] { objSQLReader["ID"].ToString(), 
                                                 objSQLReader["DiscountName"].ToString(),
                                                 objSQLReader["LimitedPeriodCheck"].ToString() == "Y"?
                                                 Functions.fnDate(objSQLReader["OfferStartOn"].ToString()).ToString((configDateFormat)) + "  to  " +
                                                 Functions.fnDate(objSQLReader["OfferEndOn"].ToString()).ToString((configDateFormat)) :
                                                 "Unlimited", 
                                                 cat,
                                                 valtype,
                                                 val,
                                                 objSQLReader["DiscountPlusQty"].ToString() });
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


        public int GetActiveProductsforCategory(int CatID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Product where CATEGORYID = @CATEGORYID and ProductStatus = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@CATEGORYID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CATEGORYID"].Value = CatID;

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

        public int GetProductIDFromScale(int clsID)
        {
            int intCount = 0;
            string strSQLComm = " select productid from Scale_Product where Row_ID = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["productid"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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


        public bool IfExistInPrintLabel1(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = " select count(*) as RC from PrintLabel where ProductID=@ITEMID ";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.Text;

            objCommand.Parameters.Add(new SqlParameter("@ITEMID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ITEMID"].Value = ItemID;

            try
            {
                SqlDataReader objSQLReader = null;
                objSQLReader = objCommand.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = Functions.fnInt32(objSQLReader["RC"].ToString());
                }
                objSQLReader.Close();
                if (intResult > 0) return true; else return false;
            }
            catch
            {
                return false;
            }
        }

        public void InsertPrintLabel(SqlCommand objSQlComm, int ItemID, int iQty)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = "Insert Into PrintLabel ( ProductID,Qty,"
                    + "CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                    + "Values ( @ProductID,@Qty,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn )";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = ItemID;
                objSQlComm.Parameters["@Qty"].Value = iQty;//PrintLabelQty
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                sqlDataReader = objSQlComm.ExecuteReader();
                sqlDataReader.Close();
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
            }
        }

        private void UpdatePrintLabel(SqlCommand objSQlComm, int ItemID, int iQty)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Update PrintLabel set Qty = Qty + @Qty where ProductID=@ID  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = ItemID;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = iQty;
                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        private void UpdatePrintQty(SqlCommand objSQlComm, int ItemID, int iQty)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Update product set QtyToPrint = QtyToPrint + @Qty where ID=@ID  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = ItemID;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = iQty;
                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        public bool TransferToPrintLabel(int pCat)
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            try
            {
                DataTable dtbl = ProductsAgainstCat(SaveComm,pCat);
                foreach (DataRow dr in dtbl.Rows)
                {
                    int intItemID = Functions.fnInt32(dr["ItemID"].ToString());
                    bool blIfExistInPrintLabel = IfExistInPrintLabel1(SaveComm, intItemID);
                    if (blIfExistInPrintLabel)
                    {
                        UpdatePrintLabel(SaveComm, intItemID,1);
                    }
                    else InsertPrintLabel(SaveComm, intItemID, 1);

                    UpdatePrintQty(SaveComm, intItemID,1);
                }
               
                SaveTran.Commit();
                SaveTran.Dispose();
                return true;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();
            }
        }

        public DataTable ProductsAgainstCat(SqlCommand objSQlComm, int PID)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
            

            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select ID from product Where CategoryID=@ID and ProductStatus = 'Y' order by SKU";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = PID;

                sqlDataReader = objSQlComm.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    dtbl.Rows.Add(new object[] { sqlDataReader["ID"].ToString() });
                }
                sqlDataReader.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return null;
            }
        }

        #region Import Inventory From DAT

        public bool IfExistDATSKU(string pSKU)
        {
            int intresult = 0;
            string strSQLComm = " select count(*) as rcnt from product where sku = @p ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@p"].Value = pSKU;


                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intresult > 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return false;
            }
        }

        public int IsExistVendorID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from Vendor where VendorID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistVendPart(int pHID, int pPID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from VendPart where VendorID = @HID and ProductID = @PID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@HID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@HID"].Value = pHID;
            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = pPID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistDepartmentID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from Dept where DepartmentID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistCategoryID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from Category where CategoryID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }
        
        public int IsExistFutureBatch(DateTime pDT)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from FuturePriceHeader where EffectiveFrom = @ID and FutureBatchName like 'Spartan Price Change #%' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@ID"].Value = pDT;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int GetFutureBatchSpartanSL()
        {
            int intCNo = 0;

            string strSQLComm = " select isnull(max(CAST(substring(FutureBatchName,23,len(FutureBatchName) - 22) as int)),0) + 1 as SL from FuturePriceHeader where FutureBatchName like 'Spartan Price Change #%' "
                              + " and ISNUMERIC(substring(FutureBatchName,23,len(FutureBatchName) - 22)) = 1 ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["SL"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistFuturePrice(int HID, int IID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from FuturePriceDetails where FutureBatchID = @ID and ItemID = @PID and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = HID;
            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = IID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistSalePrice(int HID, int IID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from SalePriceDetails where SaleBatchID = @ID and ItemID = @PID and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = HID;
            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = IID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int IsExistSaleBatch(DateTime pDT1, DateTime pDT2)
        {
            int intCNo = 0;

            string strSQLComm = " select count(ID) as RecCount from SalePriceHeader where EffectiveFrom = @F and EffectiveTo = @T and SaleBatchName like 'Spartan TPR #%' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            objSQlComm.Parameters.Add(new SqlParameter("@F", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@F"].Value = pDT1;
            
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@T"].Value = pDT2;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["RecCount"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchProductID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from Product where SKU = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchCategoryID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from Category where CategoryID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchDepartmentID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from dept where DepartmentID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchVendorID(string pID)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from vendor where VendorID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchFutureBatchID(DateTime pDT)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from FuturePriceHeader where EffectiveFrom = @DT and FutureBatchName like 'Spartan Price Change #%' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@DT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@DT"].Value = pDT;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int GetSaleBatchSpartanSL()
        {
            int intCNo = 0;

            string strSQLComm = " select isnull(max(CAST(substring(SaleBatchName,14,len(SaleBatchName) - 13) as int)),0) + 1 as SL from SalePriceHeader where SaleBatchName like 'Spartan TPR #%' "
                              + " and ISNUMERIC(substring(SaleBatchName,14,len(SaleBatchName) - 13)) = 1 ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["SL"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchSaleBatchID(DateTime pdt1, DateTime pdt2)
        {
            int intCNo = 0;

            string strSQLComm = " select ID from SalePriceHeader where EffectiveFrom = @F and EffectiveTo = @T and SaleBatchName like 'Spartan TPR #%' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@F", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@F"].Value = pdt1;
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@T"].Value = pdt2;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int FetchMaxCatDisplayOrder()
        {
            int intCNo = 0;

            string strSQLComm = " select isnull(Max(POSDisplayOrder),0) + 1 as MaxVal from Category";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["MaxVal"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public string InsertDepartment(string strDeptID, string strDeptName, string strFS, ref int DID)
        {
            string strSQLComm = " insert into dept(DepartmentID,Description,Selected,FoodStampEligible,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @DepartmentID,@Description,@Selected,@FoodStampEligible,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Selected", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DepartmentID"].Value = strDeptID;
                objSQlComm.Parameters["@Description"].Value = strDeptName;
                objSQlComm.Parameters["@Selected"].Value = "N";
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFS;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                DID = intID;
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
        }

        public string InsertCategory(   string strCatID, string strCatName, int disporder, string strFoodStamp,
                                        string strPOSScreenColor, string strPOSBackground, string strPOSScreenStyle, string strPOSFontType,
                                        int intPOSFontSize, string strPOSFontColor, string strPOSItemFontColor, string strIsBold, string strIsItalics,
                                        ref int CID)
        {
            string strSQLComm = " insert into category(CategoryID,Description,MaxItemsforPOS,POSDisplayOrder,POSBackground,"
                              + " POSFontType,POSFontSize,POSFontColor,POSItemFontColor,FoodStampEligible,POSScreenColor,POSScreenStyle,"
                              + " IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @CategoryID,@Description,@MaxItemsforPOS,@POSDisplayOrder,"
                              + " @POSBackground,@POSFontType,@POSFontSize,@POSFontColor,@POSItemFontColor,@FoodStampEligible,"
                              + " @POSScreenColor,@POSScreenStyle,@IsBold,@IsItalics,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@MaxItemsforPOS", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSItemFontColor", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@CategoryID"].Value = strCatID;
                objSQlComm.Parameters["@Description"].Value = strCatName;
                objSQlComm.Parameters["@MaxItemsforPOS"].Value = 10;
                objSQlComm.Parameters["@POSDisplayOrder"].Value = disporder;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@POSItemFontColor"].Value = strPOSItemFontColor;

                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStamp;
                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                CID = intID;
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
        }

        public string InsertVendor(string strID, string strName, ref int DID)
        {
            string strSQLComm = " insert into vendor(VendorID,Name,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @VendorID,@Name,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@VendorID"].Value = strID;
                objSQlComm.Parameters["@Name"].Value = strName;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                DID = intID;
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
        }
        
        public int FetchItemDisplayOrder(int CID)
        {
            int intCNo = 0;

            string strSQLComm = " select isnull(Max(POSDisplayOrder),0) + 1 as MaxVal from Product where CategoryID = @CID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CID"].Value = CID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["MaxVal"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public string UpdateItemDisplayOrderOnImport(int iCat, int iItem)
        {
            
            string strSQLComm = "";

            strSQLComm = "sp_UpdateItemDisplayOrder";
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ID"].Value = iItem;
                objSQlComm.Parameters.Add(new SqlParameter("@NewGroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NewGroupID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@NewGroupID"].Value = iCat;

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

        public string UpdateItemForDepartment(int pID, int pDept)
        {
            string strSQLComm = " update product set DepartmentID=@param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pDept;

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

        public string UpdateItemForCategory(int pID, int pCat, int pDisp)
        {
            string strSQLComm = " update product set CategoryID=@param, POSDisplayOrder = @param2 where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pCat;
                objSQlComm.Parameters["@param2"].Value = pDisp;

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

        public string UpdateItemForCategory(int pID, int pCat)
        {
            string strSQLComm = " update product set CategoryID=@param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pCat;

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

        public string UpdateItemForPrice(int pID, double pVal)
        {
            string strSQLComm = " update product set PriceA = @param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pVal;

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

        public string UpdateItemForFoodStamp(int pID, string pVal)
        {
            string strSQLComm = " update product set FoodStampEligible = @param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pVal;

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

        public string UpdateItemForPOSVisible(int pID, string pVal)
        {
            string strSQLComm = " update product set AddtoPOSScreen = @param where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pVal;

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


        public string InsertSaleBatchHeader(string strBatch, DateTime DtF, DateTime DtT, ref int DID)
        {
            string strSQLComm = " insert into salepriceheader(SaleBatchName,EffectiveFrom,EffectiveTo,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @SaleBatchName,@EffectiveFrom,@EffectiveTo,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveTo", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SaleBatchName"].Value = strBatch;
                objSQlComm.Parameters["@EffectiveFrom"].Value = DtF;
                objSQlComm.Parameters["@EffectiveTo"].Value = DtT;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                DID = intID;
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
        }


        public string InsertFutureBatchHeader(string strBatch, DateTime EfftDate, ref int DID)
        {
            string strSQLComm = " insert into futurepriceheader(FutureBatchName,EffectiveFrom,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @FutureBatchName,@EffectiveFrom,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FutureBatchName"].Value = strBatch;
                objSQlComm.Parameters["@EffectiveFrom"].Value = EfftDate;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                DID = intID;
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
        }

        public string UpdateFuturePrice(int HID, int pID, double pPrice)
        {
            string strSQLComm = " update futurepricedetails set FuturePriceA=@param where FutureBatchID = @iH and ItemID = @iI and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pPrice;

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

        public string InsertFuturePrice(int HID, int pID, double pPrice)
        {
            string strSQLComm = " insert into futurepricedetails(FutureBatchID,ItemID,FuturePriceA,CreatedOn,LastChangedOn) "
                              + " values(  @iH,@iI,@param,getdate(),getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pPrice;

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

        public string DeleteFuturePrice(int HID, int pID)
        {
            string strSQLComm = " delete from futurepricedetails where FutureBatchID = @iH and ItemID = @iI and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;


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

        public string UpdateSalePrice(int HID, int pID, double pPrice)
        {
            string strSQLComm = " update salepricedetails set SalePrice=@param where SaleBatchID = @iH and ItemID = @iI and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pPrice;

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

        public string InsertSalePrice(int HID, int pID, double pPrice)
        {
            string strSQLComm = " insert into salepricedetails(SaleBatchID,ItemID,SalePrice,CreatedOn,LastChangedOn) "
                              + " values(  @iH,@iI,@param,getdate(),getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@param"].Value = pPrice;

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

        public string DeleteSalePrice(int HID, int pID)
        {
            string strSQLComm = " delete from salepricedetails where saleBatchID = @iH and ItemID = @iI and ApplyFamily = 'N' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;


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

        public int GetFutureBatchRecordCount(int pID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(*) as Rcnt from FuturePriceDetails where FutureBatchID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["Rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int GetSaleBatchRecordCount(int pID)
        {
            int intCNo = 0;

            string strSQLComm = " select count(*) as Rcnt from SalePriceDetails where SaleBatchID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCNo = Convert.ToInt32(objSQLReader["Rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCNo;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public string DeleteFutureBatch(int HID)
        {
            string strSQLComm = " delete from futurepriceheader where FutureBatchID = @iH ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;

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

        public string DeleteSaleBatch(int HID)
        {
            string strSQLComm = " delete from salepriceheader where saleBatchID = @iH ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;

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

        public string InsertVendPart(int HID, int pID)
        {
            string strSQLComm = " insert into vendpart(VendorID,ProductID,IsPrimary,PackQty,CreatedOn,LastChangedOn) "
                              + " values(@iH,@iI,'N',1,getdate(),getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;

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

        public string InsertVendPartWithPack(int HID, int pID,int Pk)
        {
            string strSQLComm = " insert into vendpart(VendorID,ProductID,IsPrimary,PackQty,CreatedOn,LastChangedOn) "
                              + " values(@iH,@iI,'Y',@iP,getdate(),getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iP", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@iP"].Value = Pk;

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

        public string InsertVendPackQty(int HID, int pID,int PK)
        {
            string strSQLComm = " insert into vendpart(VendorID,ProductID,PackQty,IsPrimary,CreatedOn,LastChangedOn) "
                              + " values(@iH,@iI,@PackQty,'N',getdate(),getdate()) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PackQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@PackQty"].Value = PK;

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

        public string UpdateVendPackQty(int HID, int pID, int PK)
        {
            string strSQLComm = " update vendpart set PackQty = @PackQty, LastChangedOn = getdate(), LastChangedBy = 0 "
                              + " where VendorID = @iH and ProductID = @iI ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@iH", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@iI", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PackQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@iH"].Value = HID;
                objSQlComm.Parameters["@iI"].Value = pID;
                objSQlComm.Parameters["@PackQty"].Value = PK;

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


        public string InsertProductFromDATImport(string strSKU, string strDescription,string strProductType,double dblPriceA,string strFS, string strPOS,
                                       int intDptID,int intCatID,int intDisp,
                                       string strPOSScreenColor, string strPOSBackground, string strPOSScreenStyle, string strPOSFontType,
                                       int intPOSFontSize, string strPOSFontColor,  string strIsBold, string strIsItalics,
                                       ref int CID)
        {
            string strSQLComm = " insert into product(SKU,Description,ProductType,PriceA,FoodStampEligible,AddtoPOSScreen,DepartmentID,CategoryID,"
                              + " POSDisplayOrder,POSBackground,POSFontType,POSFontSize,POSFontColor,POSScreenColor,POSScreenStyle,"
                              + " IsBold,IsItalics,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values ( @SKU,@Description,@ProductType,@PriceA,@FoodStampEligible,@AddtoPOSScreen,@DepartmentID,@CategoryID,@POSDisplayOrder,"
                              + " @POSBackground,@POSFontType,@POSFontSize,@POSFontColor,"
                              + " @POSScreenColor,@POSScreenStyle,@IsBold,@IsItalics,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ProductType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@ProductType"].Value = strProductType;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFS;
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = strPOS;
                objSQlComm.Parameters["@DepartmentID"].Value = intDptID;
                objSQlComm.Parameters["@CategoryID"].Value = intCatID;
                objSQlComm.Parameters["@POSDisplayOrder"].Value = intDisp;

                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;

                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@CreatedBy"].Value = 0;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = 0;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Convert.ToInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                CID = intID;
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
        }

        #endregion

        #region Report for List


        public DataTable GetListData(int ListType, string configDateFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (ListType == 0) // List of item families
            {
                strSQLComm = " select BrandID as ReturnCode, BrandDescription as ReturnDescription, ''  as ReturnDescription2 from BrandMaster order by BrandDescription";
            }

            if (ListType == 1) // List of departments
            {
                strSQLComm = " select DepartmentID as ReturnCode, Description as ReturnDescription, ScaleFlag  as ReturnDescription2 from dept order by Description";
            }

            if (ListType == 2) // List of pos screen categories
            {
                strSQLComm = " select CategoryID as ReturnCode, Description as ReturnDescription, ''  as ReturnDescription2 from category order by Description";
            }

            if (ListType == 3) // List of Taxes
            {
                strSQLComm = " select TaxName as ReturnCode, TaxRate as ReturnDescription, Active  as ReturnDescription2 from TaxHeader order by TaxName";
            }
            
            if (ListType == 4) // List of Tender Types
            {
                strSQLComm = " select Name as ReturnCode, DisplayAs as ReturnDescription, Enabled  as ReturnDescription2 from tendertypes order by Name";
            }

            /*
            if (ListType == 5) // List of Security Profiles
            {
                strSQLComm = " select GroupName as ReturnCode, '' as ReturnDescription, ''  as ReturnDescription2 from securitygroup order by GroupName";
            }

            if (ListType == 6) // List of Zip Codes
            {
                strSQLComm = " select Zip as ReturnCode, City as ReturnDescription, state  as ReturnDescription2 from zipcode order by Zip";
            }
             */

            if (ListType == 5) // List of Customer Classes
            {
                strSQLComm = " select ClassID as ReturnCode, Description as ReturnDescription, ''  as ReturnDescription2 from classmaster order by Description";
            }

            if (ListType == 6) // List of Customer Groups
            {
                strSQLComm = " select GroupID as ReturnCode, Description as ReturnDescription, ''  as ReturnDescription2 from groupmaster order by Description";
            }


            if (ListType == 7) // List of Client Parameters
            {
                strSQLComm = " select ClientParam1, ClientParam2, ClientParam3, ClientParam4, ClientParam5 from setup ";
            }

            if (ListType == 8) // List of Employee Shifts
            {
                strSQLComm = " select ShiftName as ReturnCode, StartTime + ' - ' + EndTime as ReturnDescription, ''  as ReturnDescription2 from shiftmaster order by ShiftName ";
            }

            if (ListType == 9) // List of Holidays
            {
                strSQLComm = " select HolidayDate as ReturnCode, HolidayDesc as ReturnDescription, ''  as ReturnDescription2 from holidaymaster order by HolidayDate ";
            }

            if (ListType == 10) // List of Discounts
            {
                strSQLComm = " select ID,DiscountName,DiscountStatus,DiscountType,DiscountAmount,DiscountPercentage,"
                              + " ChkApplyOnItem,ChkApplyOnTicket,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate "
                              + " from DiscountMaster order by DiscountName ";
            }

            if (ListType == 11) // List of Mix n Match
            {
                strSQLComm = " select m.ID,m.DiscountName,m.DiscountStatus,m.DiscountType,m.DiscountAmount,m.DiscountPercentage,"
                              + " m.DiscountCategory,m.DiscountFamilyID,m.DiscountPlusQty,isnull(b.BrandDescription,'') as Family "
                              + " from MixNmatchHeader m left outer join BrandMaster b on m.DiscountFamilyID = b.ID order by m.DiscountName ";
            }

            if (ListType == 12) // List of Fees
            {
                strSQLComm = " select ID,FeesName,FeesStatus,FeesType,FeesAmount,FeesPercentage,"
                              + " ChkTax,ChkFoodStamp,ChkDiscount,ChkInclude,ChkAutoApply,ChkItemQty from FeesMaster order by FeesName ";
            }

            if (ListType == 13) // Sale Batch
            {
                strSQLComm = " select ID,SaleBatchName,SaleBatchDesc,SaleStatus,EffectiveFrom,EffectiveTo "
                              + " from SalePriceHeader order by SaleBatchName ";
            }

            if (ListType == 14) // Future Batch
            {
                strSQLComm = " select ID,FutureBatchName,FutureBatchDesc,FutureBatchStatus,EffectiveFrom,IsSet "
                              + " from FuturePriceHeader order by FutureBatchName ";
            }

            if (ListType == 15) // Scale Category
            {
                strSQLComm = " select s.Cat_ID as ReturnCode,s.Name as ReturnDescription,isnull(d.Description,'') as ReturnDescription2 from Scale_Category s "
                              + " left outer join Dept d on d.ID = s.Department_ID order by s.Cat_ID ";
            }

            if (ListType == 16) // Scale Address
            {
                strSQLComm = " select s.SCALE_IP as ReturnCode,s.SCALE_LOCATION as ReturnDescription,isnull(t.Scale_Type,'') as ReturnDescription2 "
                             + " from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE ";
            }

            if (ListType == 17) // Scale Label
            {
                strSQLComm = " select s.Label_ID as ReturnCode,s.Description as ReturnDescription,isnull(d.Scale_IP,'') as ReturnDescription2 from Label_Types s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type order by s.Label_ID ";
            }

            if (ListType == 18) // Scale Second Label
            {
                strSQLComm = " select s.Label_ID as ReturnCode,s.Description as ReturnDescription,isnull(d.Scale_IP,'') as ReturnDescription2 from Second_Label_Types s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type order by s.Label_ID ";
            }

            if (ListType == 19) // Scale Graphics
            {
                strSQLComm = "  select s.Graphic_ID as ReturnCode,s.Description as ReturnDescription,isnull(d.Scale_IP,'') as ReturnDescription2 from Scale_Graphics s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type order by s.Graphic_ID ";
            }

            


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description2", System.Type.GetType("System.String"));

                string v1 = "";
                string v2 = "";
                string v3 = "";

                while (objSQLReader.Read())
                {
                    if (ListType == 7)  // client parameter
                    {
                        if (objSQLReader["ClientParam1"].ToString().Trim() != "") dtbl.Rows.Add(new object[] { objSQLReader["ClientParam1"].ToString(), "","" });
                        if (objSQLReader["ClientParam2"].ToString().Trim() != "") dtbl.Rows.Add(new object[] { objSQLReader["ClientParam2"].ToString(), "", "" });
                        if (objSQLReader["ClientParam3"].ToString().Trim() != "") dtbl.Rows.Add(new object[] { objSQLReader["ClientParam3"].ToString(), "", "" });
                        if (objSQLReader["ClientParam4"].ToString().Trim() != "") dtbl.Rows.Add(new object[] { objSQLReader["ClientParam4"].ToString(), "", "" });
                        if (objSQLReader["ClientParam5"].ToString().Trim() != "") dtbl.Rows.Add(new object[] { objSQLReader["ClientParam5"].ToString(), "", "" });
                    }
                    else if (ListType == 10)  // Discount
                    {
                        string chk1 = "";
                        string chk2 = "";
                        string val = "";

                        string appitm = "";
                        string apptkt = "";

                        string dtxt = "";
                        chk1 = "";
                        chk2 = "";
                        appitm = "";
                        apptkt = "";
                        appitm = objSQLReader["ChkApplyOnItem"].ToString();
                        apptkt = objSQLReader["ChkApplyOnTicket"].ToString();
                        if ((appitm == "Y") && (apptkt == "Y"))
                        {
                            dtxt = "Item & Ticket";
                        }
                        if ((appitm == "Y") && (apptkt == "N"))
                        {
                            dtxt = "Item";
                        }
                        if ((appitm == "N") && (apptkt == "Y"))
                        {
                            dtxt = "Ticket";
                        }

                        if (objSQLReader["DiscountStatus"].ToString() == "Y")
                        {
                            chk1 = "Yes";
                        }
                        else
                        {
                            chk1 = "No";
                        }
                        if (objSQLReader["ChkLimitedPeriod"].ToString() == "Y")
                        {
                            chk1 = chk1 + "    ("
                                 + Convert.ToDateTime(objSQLReader["LimitedStartDate"].ToString()).ToString("d") + " - " + Convert.ToDateTime(objSQLReader["LimitedEndDate"].ToString()).ToString("d") + "  )";
                        }
                        if (objSQLReader["DiscountType"].ToString() == "A")
                        {
                            chk2 = " off";
                            val = Functions.fnDouble(objSQLReader["DiscountAmount"].ToString()).ToString("f");
                        }
                        else
                        {
                            chk2 = " % off";
                            val = Functions.fnDouble(objSQLReader["DiscountPercentage"].ToString()).ToString("f");
                        }

                        v1 = objSQLReader["DiscountName"].ToString();
                        v2 = val + chk2 + " on " + dtxt;
                        v3 = chk1;

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
                    else if (ListType == 11) // Mix Match
                    {
                        string chk1 = "";
                        string chk2 = "";
                        string chk3 = "";
                        string val = "";

                        if (objSQLReader["DiscountStatus"].ToString() == "Y") chk1 = "Yes";
                        else chk1 = "No";


                        if (objSQLReader["DiscountType"].ToString() == "A")
                        {
                            chk2 = " off";
                            val = objSQLReader["DiscountAmount"].ToString();
                        }
                        else
                        {
                            chk2 = "% off";
                            val = objSQLReader["DiscountPercentage"].ToString();
                        }

                        if (objSQLReader["DiscountCategory"].ToString() == "N")
                        {
                            chk3 = "Normal Pricing";
                        }
                        else if (objSQLReader["DiscountCategory"].ToString() == "P")
                        {
                            chk3 = "Plus Pricing";
                        }
                        else
                        {
                            chk3 = "Absolute Pricing";
                        }

                        v1 = objSQLReader["DiscountName"].ToString();
                        v2 = chk3;
                        v3 = chk1;

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
                    else if (ListType == 12) // Fees
                    {
                        string chk = "";
                        string chk1 = "";
                        string chk2 = "";
                        string chk3 = "";
                        string chk4 = "";
                        string chk5 = "";
                        string chk6 = "";
                        string chk7 = "";
                        string val = "";
                        string dtxt = "";

                        if (objSQLReader["FeesStatus"].ToString() == "Y")
                        {
                            chk1 = "Yes";
                        }
                        else
                        {
                            chk1 = "No";
                        }

                        if (objSQLReader["ChkTax"].ToString() == "Y")
                        {
                            chk2 = "Yes";
                        }
                        else
                        {
                            chk2 = "No";
                        }

                        if (objSQLReader["ChkDiscount"].ToString() == "Y")
                        {
                            chk3 = "Yes";
                        }
                        else
                        {
                            chk3 = "No";
                        }

                        if (objSQLReader["ChkFoodStamp"].ToString() == "Y")
                        {
                            chk4 = "Yes";
                        }
                        else
                        {
                            chk4 = "No";
                        }

                        if (objSQLReader["ChkInclude"].ToString() == "Y")
                        {
                            chk5 = "Yes";
                        }
                        else
                        {
                            chk5 = "No";
                        }

                        if (objSQLReader["ChkAutoApply"].ToString() == "Y")
                        {
                            chk6 = "Yes";
                        }
                        else
                        {
                            chk6 = "No";
                        }

                        if (objSQLReader["ChkItemQty"].ToString() == "Y")
                        {
                            chk7 = "Yes";
                        }
                        else
                        {
                            chk7 = "No";
                        }


                        if (objSQLReader["FeesType"].ToString() == "A")
                        {
                            chk = " add";
                            val = objSQLReader["FeesAmount"].ToString();
                        }
                        else
                        {
                            chk = " % add";
                            val = objSQLReader["FeesPercentage"].ToString();
                        }

                        v1 = objSQLReader["FeesName"].ToString();
                        v2 = Functions.fnDouble(val).ToString("f") + chk;
                        v3 = chk1;

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
                    else if (ListType == 13)        // sale batch
                    {
                        string val = "";
                        string chk1 = "";

                        if (objSQLReader["SaleStatus"].ToString() == "Y")
                        {
                            chk1 = "Yes";
                        }
                        else
                        {
                            chk1 = "No";
                        }
                        val = Convert.ToDateTime(objSQLReader["EffectiveFrom"].ToString()).ToString((configDateFormat) + "     hh:mm tt") + "  -  " + Convert.ToDateTime(objSQLReader["EffectiveTo"].ToString()).ToString((configDateFormat) + "     hh:mm tt");
                        
                        v1 = objSQLReader["SaleBatchName"].ToString();
                        v3 = chk1;
                        v2 = val;

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
                    else if (ListType == 14) // future batch
                    {
                        string val = "";
                        string chk1 = "";

                        if (objSQLReader["FutureBatchStatus"].ToString() == "Y")
                        {
                            chk1 = "Yes";
                        }
                        else
                        {
                            chk1 = "No";
                        }

                        val = Convert.ToDateTime(objSQLReader["EffectiveFrom"].ToString()).ToString((configDateFormat));
                        v1 = objSQLReader["FutureBatchName"].ToString();
                        v3 = chk1;
                        v2 = val;

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
                    else
                    {
                        v1 = objSQLReader["ReturnCode"].ToString();

                        if (ListType == 9) v1 = Functions.fnDate(v1).ToString("MMM dd, yyyy");

                        v2 = objSQLReader["ReturnDescription"].ToString();

                        if (ListType == 3) v2 = Functions.fnDouble(objSQLReader["ReturnDescription"].ToString()).ToString("f") + " %";

                        v3 = objSQLReader["ReturnDescription2"].ToString();

                        if (v3 == "N") v3 = "No";
                        if (v3 == "Y") v3 = "Yes";

                        dtbl.Rows.Add(new object[] { v1, v2, v3 });
                    }
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

        public DataTable FetchFastItemRecords(string searchtxt, int Dpt, int SKUorName, bool bMarkdown)
        {
            DataTable dtbl = new DataTable();

            string strSQLFilter = "";
            string strSQLDeptFilter = "";

            if (SKUorName == 0)
            {
                if (Dpt > 0) strSQLFilter = " and s.PLU_Number like '" + searchtxt + "%' ";
                else strSQLFilter = " and p.SKU like '" + searchtxt + "%' ";
            }

            if (SKUorName == 1)
            {
                strSQLFilter = " and p.description like '%" + searchtxt + "%' ";
            }

            if (Dpt > 0)
            {
                strSQLDeptFilter = " and p.DepartmentID = @D ";
            }

            string strSQLComm = "";

            if (Dpt == 0)
            {
                strSQLComm = " select distinct 0 as ScaleRefID, p.ID as ProductID,p.SKU,p.Description from Product p left outer join Dept d on p.DepartmentID = d.ID where (1 = 1) " + strSQLFilter + " order by " + (SKUorName == 0 ? "p.SKU" : "p.Description"); // and d.ScaleFlag = 'Y'
            }

            if (Dpt > 0)
            {
                if (!bMarkdown)
                    strSQLComm = " select distinct s.Row_ID as ScaleRefID, s.ProductID, s.PLU_Number as SKU, p.Description from Product p left outer join scale_product s on p.ID = s.ProductID where (1 = 1) and s.Item_Type in ('By Count', 'Fixed Weight') " + strSQLFilter + strSQLDeptFilter + " order by " + (SKUorName == 0 ? "s.PLU_Number" : "p.Description");
                else
                    strSQLComm = " select distinct s.Row_ID as ScaleRefID, s.ProductID, s.PLU_Number as SKU, p.Description from Product p left outer join scale_product s on p.ID = s.ProductID where (1 = 1) and s.Item_Type in ('By Count', 'Fixed Weight', 'Random Weight') " + strSQLFilter + strSQLDeptFilter + " order by " + (SKUorName == 0 ? "s.PLU_Number" : "p.Description");
            }
                                
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (Dpt > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
               
                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {   objSQLReader["ScaleRefID"].ToString(),
                                                   objSQLReader["ProductID"].ToString(), 
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString()});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
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

        #region Production List

        public int GetProductID(string strSKUrecd, int Dpt)
        {
            int intCount = 0;
            string strSQLComm = " select ID from product where SKU=@SKU and DepartmentID = @D and ProductStatus = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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


        public int GetScaleRefID(string strSKUrecd, int Dpt)
        {
            int intCount = 0;
            string strSQLComm = " select s.row_id from scale_product s left outer join product p on p.ID = s.ProductID where s.plu_number = @SKU "
                              + " and s.Item_Type in ('By Count', 'Fixed Weight') and p.DepartmentID = @D and p.ProductStatus = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["row_id"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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


        

        public int GetScaleRefIDWithSKU(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(s.row_id,0) as rcdid from scale_product s left outer join product p on p.ID = s.ProductID where p.sku = @SKU "
                              + " and s.Item_Type in ('By Count', 'Fixed Weight') ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["rcdid"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
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

        public string GetPLU_Number(int RecordID)
        {
            string plu = "";
            string strSQLComm = " select plu_number from scale_product where row_id = @param ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@param"].Value = RecordID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        plu = objsqlReader["plu_number"].ToString();
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return plu;
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


        public int DuplicateCount(string strSKUrecd, int Dpt)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from product p left outer join scale_product s on p.ID = s.ProductID "
                              + " where p.SKU = @SKU and p.DepartmentID = @D and p.ProductStatus = 'Y' and s.Item_Type in ('Fixed Weight', 'By Count') ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;

                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;

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

        public int DuplicateAltSKUCount(string strSKUrecd, int Dpt)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from product p left outer join scale_product s on p.ID = s.ProductID "
                              + " where p.SKU2 = @SKU and p.DepartmentID = @D and p.ProductStatus = 'Y' and s.Item_Type in ('Fixed Weight', 'By Count') ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;
                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;
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

        public int DuplicateAltSKU2Count(string strSKUrecd, int Dpt)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from product p left outer join scale_product s on p.ID = s.ProductID "
                              + " where p.SKU3 = @SKU and p.DepartmentID = @D and p.ProductStatus = 'Y' and s.Item_Type in ('Fixed Weight', 'By Count') ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;
                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;
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


        public int DuplicateWeightedCount(string strSKUrecd, int Dpt)
        {
            int intCount = 0;

            string strSQLComm = " select count(*) as rcnt from product p left outer join scale_product s on p.ID = s.ProductID "
                              + " where p.SKU = @SKU and p.DepartmentID = @D and p.scalebarcode = 'Y' and s.Item_Type in ('Fixed Weight', 'By Count') ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = strSKUrecd;
                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dpt;
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

        public int GetProductionListID(int pID)
        {
            int intCount = 0;

            string strSQLComm = " select RefID from ProductionPrintBatch where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["RefID"]);
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
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


        public void GetProductionListHeaderInfo(int pID, ref string RefNo, ref DateTime RefDate)
        {
            int intCount = 0;

            string strSQLComm = " select RefNo, RefDate from ProductionListHeader where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        RefNo = objsqlReader["RefNo"].ToString();
                        RefDate = Functions.fnDate(objsqlReader["RefDate"].ToString());
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion



        public DataTable FetchProductInfoForShelfTagLabel(int pID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select P.ID,SKU,P.Description as ProductName,P.BinLocation,P.Points,P.ProductNotes,P.Notes2,"
                            + " isnull(BRAD.BrandDescription,'') as Brand,"
                            + " isnull(V.VendorID,'') as VID,isnull(V.Name,'') as VName,isnull(VP.PartNumber,'') as VPart, isnull(VP.PackQty,'') as VPack "
                            + " from Product P left outer join BrandMaster BRAD on P.BrandID = BRAD.ID "
                            + " left outer join VendPart VP on P.ID = VP.ProductID and VP.IsPrimary = 'Y' "
                            + " left outer join Vendor V on V.ID = VP.VendorID "
                            + " where (1 = 1) and P.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Location", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPart", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPack", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["ProductName"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["ProductNotes"].ToString(),
                                                   objSQLReader["Notes2"].ToString(),
                                                   objSQLReader["BinLocation"].ToString(),
                                                   objSQLReader["Points"].ToString(),
                                                    objSQLReader["VID"].ToString(),
                                                    objSQLReader["VName"].ToString(),
                                                    objSQLReader["VPart"].ToString(),
                                                    objSQLReader["VPack"].ToString()});
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

        public byte[] GetProductPhoto(int pID)
        {

            byte[] b = null;

            string strSQLComm = " select ProductPhoto from Product where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                while (objSQLReader.Read())
                {
                    try
                    {
                        b = (byte[])objSQLReader["ProductPhoto"];
                    }
                    catch
                    {
                    }
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return b;
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

        public int GetScaleRecordCountForWeightedItem(int pID)
        {
            int rcnt = 0;
            string strSQLComm = " select count(*) as rcnt from scale_product where productid = @pid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@pid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@pid"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    rcnt = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return rcnt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public string GetWeightedItemType(int pID)
        {
            string val = "";
            string strSQLComm = " select item_type from scale_product where productid = @pid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@pid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@pid"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["item_type"].ToString();
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


        public DataTable FetchProductInfoFromImportedSKU(string pSKU)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(ID,0) as ProductID, isnull(Description,'') as ProductName from product where SKU = @SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@SKU"].Value = pSKU;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ProductID"].ToString(),
												   objSQLReader["ProductName"].ToString()
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

        public bool GetPromptForPriceTageOfItem(int intpID)
        {
            bool val = false;
            string strSQLComm = " select PromptForPrice from product where ID in ( select productid from scale_product where row_id = @ID) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = objsqlReader["PromptForPrice"].ToString() == "Y" ? true : false;
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return false;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable GetProductInfoForMarkdown(int intpID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select SKU, description, PriceA from product where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                dtbl.Columns.Add("ProductSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RetailPrice", System.Type.GetType("System.String"));

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        dtbl.Rows.Add(new object[] {  
                                                   objsqlReader["SKU"].ToString(),
                                                   objsqlReader["description"].ToString(),
												   objsqlReader["PriceA"].ToString()
                                                   });
                    }
                    catch
                    {
                    }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return null;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        // Product Valuation Report

        public DataTable FetchInventoryValuationReportData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @" select p.ID, p.SKU, isnull(p.SKU2,'') as AltSKU, isnull(p.SKU3,'') as AltSKU2, 
                                    p.Description, isnull(c.Description,'') as CatDesc,isnull(p.PriceA,0) as saleprice, 
                                    isnull(p.QtyOnHand,0) as qty, isnull(p.Cost,0) as cost, 
                                    isnull(p.DiscountedCost,0) as DCost from product p left outer join
                                    Category c on p.CategoryID = c.ID order by p.SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("StockValue", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("AltSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalePrice", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Part", System.Type.GetType("System.String"));

                string altsku = "";
                while (objSQLReader.Read())
                {
                    altsku = "";
                    if (objSQLReader["AltSKU"].ToString().Trim() != "")
                    {
                        altsku = objSQLReader["AltSKU"].ToString().Trim();
                    }
                    if (objSQLReader["AltSKU2"].ToString().Trim() != "")
                    {
                        altsku = altsku == "" ? objSQLReader["AltSKU2"].ToString().Trim() : altsku + ", " + objSQLReader["AltSKU2"].ToString().Trim();
                    }
                    double dblStockValue = Functions.fnDouble(objSQLReader["qty"].ToString()) * Functions.fnDouble(objSQLReader["cost"].ToString());
                    
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["cost"].ToString()),
                                                   Functions.fnDouble(objSQLReader["DCost"].ToString()),
                                                   Functions.fnDouble(objSQLReader["qty"].ToString()),
                                                   dblStockValue,altsku,objSQLReader["CatDesc"].ToString(),
                                                   Functions.fnDouble(objSQLReader["saleprice"].ToString()),
                                                   "",""});
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return null;
            }
        }

        public DataTable GetSupplierForInventoryValuationReport(int intpID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = @" select isnull(vp.PartNumber,'') as partcode, isnull(v.Name,'') as supplier
                                   from vendpart vp left outer join vendor v on v.ID = vp.VendorID where vp.ProductID = @ID and vp.IsPrimary = 'Y'";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intpID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Part", System.Type.GetType("System.String"));
                

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        dtbl.Rows.Add(new object[] {
                                                   objsqlReader["supplier"].ToString(),
                                                   objsqlReader["partcode"].ToString()
                                                   });
                    }
                    catch
                    {
                    }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return null;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #region WPF Posting

        public string PostData_WPF(bool isBookers=false)
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
                if (intID == 0)
                {
                    intItemDisplayOrder = GetItemDisplayOrder(SaveComm, intCategoryID);
                    strError = InsertData_WPF(SaveComm);
                    if (strError == "")
                    {
                        if (intLinkSKU > 0) strError = UpdateLinkSKU(SaveComm);
                    }
                }
                else
                {
                    // 03-12-2013    Reorder Items in POS – Retail 
                    if (blIsNewCategory)
                    {
                        intItemDisplayOrder = GetItemDisplayOrder(SaveComm, intCategoryID);
                        strError = UpdateItemDisplayOrder(SaveComm);
                    }

                    strError = UpdateData_WPF(SaveComm);

                    if (dblPriceA != dblPrevPriceA)
                    {
                        ScaleConnOnPriceChange(SaveComm);
                    }

                    if (strChangeProductType != "")
                    {
                        UpdateProductTypeInExistingRecord(SaveComm, "item", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                        UpdateProductTypeInExistingRecord(SaveComm, "suspnded", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                        UpdateProductTypeInExistingRecord(SaveComm, "workorder", strChangeProductType, strChangeProductType == "P" ? "W" : "P");
                    }
                }

                if (strError == "")
                    strError = DeleteVendorparts(SaveComm);
                if (strError == "")
                    strError = SaveVendorParts(SaveComm);

                if (strError == "")
                    DeleteTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveTaxParts(SaveComm);

                if (strError == "")
                    DeleteRentTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveRentTaxParts(SaveComm);

                if (strError == "")
                    DeleteRepairTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveRepairTaxParts(SaveComm);

                if (strError == "")
                    DeleteProductModifier(SaveComm);
                if (strError == "")
                    strError = SaveProductModifier(SaveComm);

                if (strError == "")
                    DeletePrinters(SaveComm);
                if (strError == "")
                    strError = SavePrinters(SaveComm);

                if (strError == "")
                {
                    if (blAddJournal)
                    {
                        if (intLinkSKU == 0)
                        {
                            int ret = Functions.AddStockJournal(SaveComm, strJournalType, strJournalSubType, "N", intID,
                                        intLoginUserID, dblJournalQty, dblCost, intID.ToString(), strTerminalName, DateTime.Now, DateTime.Now, "");
                            if (ret > 0) strError = "";
                            else strError = "error";
                        }
                    }
                }

                if (strError == "")
                {
                    if (blChangeInventory)
                    {
                        strError = UpdateExpFlagForInventoryChange(SaveComm);
                    }
                }

                if (strError == "")
                {
                    //if (strPrintBarCode == "Y")
                    //{
                    bool blIfExistInPrintLabel = IfExistInPrintLabel(SaveComm, intID);
                    if (blIfExistInPrintLabel)
                    {
                        UpdatePrintLabel(SaveComm);
                    }
                    else
                    {
                        if (intPrintLabelQty > 0) InsertPrintLabel(SaveComm);
                    }
                    //}
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

        public string InsertData_WPF(SqlCommand objSQlComm)
        {
            
            string strSQLComm = "";
            strSQLComm = " insert into Product( SKU,SKU2,SKU3,Description,BinLocation,ProductType,PriceA,PriceB,PriceC,"
                            + " PromptForPrice,AddtoPOSScreen,ScaleBarCode,LastCost,Cost,QtyOnHand,QtyOnLayaway,ReorderQty,"
                            + " NormalQty,PrimaryVendorID,DepartmentID,CategoryID,PrintBarCode,NoPriceOnLabel,MinimumAge, "
                            + " QtyToPrint,LabelType,FoodStampEligible,ProductNotes,ProductPhoto,Points,AllowZeroStock,DisplayStockinPOS,"
                            + " POSBackground,POSScreenStyle,POSScreenColor,POSFontType,POSFontSize,POSFontColor,IsBold,IsItalics,"
                            + " ScaleBackground,ScaleScreenStyle,ScaleScreenColor,ScaleFontType,ScaleFontSize,ScaleFontColor,ScaleIsBold,ScaleIsItalics,"
                            + " DecimalPlace,ProductStatus,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,TaggedInInvoice,"
                            + " BrandID,Season,CaseUPC,CaseQty,UPC,LinkSKU,BreakPackRatio, "
                            + " RentalPerMinute,RentalPerHour,RentalPerHalfDay,RentalPerDay,RentalPerWeek,RentalPerMonth,"
                            + " RentalDeposit,MinimumServiceTime,RepairCharge,RentalMinHour,RentalMinAmount,RentalPrompt,RepairPromptForCharge,"
                            + " RepairPromptForTag,ExpFlag,POSDisplayOrder,NonDiscountable,Tare,AddToScaleScreen,Notes2,Tare2,SplitWeight,UOM,ShopifyImageExportFlag,"
                            + " ImportFrom, ImportDate, BookerProductID, AddToPOSCategoryScreen,DiscountedCost,ExpiryDate)"

                            + " values ( @SKU,@SKU2,@SKU3,@Description,@BinLocation,@ProductType,@PriceA,@PriceB,@PriceC,"
                            + " @PromptForPrice,@AddtoPOSScreen,@ScaleBarCode,@LastCost,@Cost,@QtyOnHand,@QtyOnLayaway,@ReorderQty,"
                            + " @NormalQty,@PrimaryVendorID,@DepartmentID,@CategoryID,@PrintBarCode,@NoPriceOnLabel,@MinimumAge,"
                            + " @QtyToPrint,@LabelType,@FoodStampEligible,@ProductNotes,@ProductPhoto,@Points,@AllowZeroStock,@DisplayStockinPOS,"
                            + " @POSBackground,@POSScreenStyle,@POSScreenColor,@POSFontType,@POSFontSize,@POSFontColor,@IsBold,@IsItalics,"
                            + " @ScaleBackground, @ScaleScreenStyle, @ScaleScreenColor,@ScaleFontType,@ScaleFontSize,@ScaleFontColor,@ScaleIsBold,@ScaleIsItalics,"
                            + " @DecimalPlace,@ProductStatus,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,"
                            + " @TaggedInInvoice,@BrandID,@Season,@CaseUPC,@CaseQty,@UPC,@LinkSKU,@BreakPackRatio, "
                            + " @RentalPerMinute,@RentalPerHour,@RentalPerHalfDay,@RentalPerDay,@RentalPerWeek,@RentalPerMonth,"
                            + " @RentalDeposit,@MinimumServiceTime,@RepairCharge,@RentalMinHour,@RentalMinAmount,@RentalPrompt,@RepairPromptForCharge,"
                            + " @RepairPromptForTag,'N',@POSDisplayOrder,@NonDiscountable,@Tare,@AddToScaleScreen,@Notes2,@Tare2,@SplitWeight,@UOM,@ShopifyImageExportFlag, "
                            + " @ImportFrom, @ImportDate, @BookerProductID, @AddToPOSCategoryScreen,@DiscountedCost,@ExpiryDate)"
                            + " select @@IDENTITY as ID ";


            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime));
                if (dtExpiryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = dtExpiryDate;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@BinLocation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PromptForPrice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountedCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnLayaway", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ReorderQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NormalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrimaryVendorID", System.Data.SqlDbType.VarChar));// ????
                objSQlComm.Parameters.Add(new SqlParameter("@BrandID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductPhoto", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@TaggedInInvoice", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Season", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@UPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BreakPackRatio", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMinute", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHalfDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerWeek", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMonth", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalDeposit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@MinimumServiceTime", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairCharge", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPrompt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForCharge", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare"].Value = dblTare;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare2"].Value = dblTare2;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToScaleScreen", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Notes2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@SKU2"].Value = strSKU2;
                objSQlComm.Parameters["@SKU3"].Value = strSKU3;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@BinLocation"].Value = strBinLocation;
                objSQlComm.Parameters["@ProductType"].Value = strProductType;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                objSQlComm.Parameters["@PromptForPrice"].Value = strPromptForPrice;
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = strAddtoPOSScreen;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@LastCost"].Value = dblLastCost;
                objSQlComm.Parameters["@Cost"].Value = dblCost;
                objSQlComm.Parameters["@DiscountedCost"].Value = dblDiscountedCost;
                objSQlComm.Parameters["@QtyOnHand"].Value = dblQtyOnHand;
                objSQlComm.Parameters["@QtyOnLayaway"].Value = dblQtyOnLayaway;
                objSQlComm.Parameters["@ReorderQty"].Value = dblReorderQty;
                objSQlComm.Parameters["@NormalQty"].Value = dblNormalQty;
                objSQlComm.Parameters["@PrimaryVendorID"].Value = strPrimaryVendorID;
                objSQlComm.Parameters["@BrandID"].Value = intBrandID;
                objSQlComm.Parameters["@DepartmentID"].Value = intDepartmentID;
                objSQlComm.Parameters["@CategoryID"].Value = intCategoryID;
                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStampEligible;
                objSQlComm.Parameters["@ProductNotes"].Value = strProductNotes;

                objSQlComm.Parameters["@DecimalPlace"].Value = intProductDecimalPlace;

                objSQlComm.Parameters["@ProductPhoto"].Value = bytProductPhoto == null ? Convert.DBNull : bytProductPhoto;

                

                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;

                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@ScaleScreenColor"].Value = strScaleScreenColor;
                objSQlComm.Parameters["@ScaleBackground"].Value = strScaleBackground;
                objSQlComm.Parameters["@ScaleScreenStyle"].Value = strScaleScreenStyle;
                objSQlComm.Parameters["@ScaleFontType"].Value = strScaleFontType;
                objSQlComm.Parameters["@ScaleFontSize"].Value = intScaleFontSize;
                objSQlComm.Parameters["@ScaleFontColor"].Value = strScaleFontColor;
                objSQlComm.Parameters["@ScaleIsBold"].Value = strScaleIsBold;
                objSQlComm.Parameters["@ScaleIsItalics"].Value = strScaleIsItalics;

                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@TaggedInInvoice"].Value = strTaggedInInvoice;

                objSQlComm.Parameters["@Season"].Value = strSeason;
                objSQlComm.Parameters["@CaseUPC"].Value = strCaseUPC;
                objSQlComm.Parameters["@CaseQty"].Value = intCaseQty;
                objSQlComm.Parameters["@UPC"].Value = strUPC;

                objSQlComm.Parameters["@LinkSKU"].Value = intLinkSKU;
                objSQlComm.Parameters["@BreakPackRatio"].Value = dblBreakPackRatio;

                objSQlComm.Parameters["@RentalPerMinute"].Value = dblRentalPerMinute;
                objSQlComm.Parameters["@RentalPerHour"].Value = dblRentalPerHour;
                objSQlComm.Parameters["@RentalPerHalfDay"].Value = dblRentalPerHalfDay;
                objSQlComm.Parameters["@RentalPerDay"].Value = dblRentalPerDay;
                objSQlComm.Parameters["@RentalPerWeek"].Value = dblRentalPerWeek;
                objSQlComm.Parameters["@RentalPerMonth"].Value = dblRentalPerMonth;
                objSQlComm.Parameters["@RentalDeposit"].Value = dblRentalDeposit;
                objSQlComm.Parameters["@RentalMinHour"].Value = dblRentalMinHour;
                objSQlComm.Parameters["@RentalMinAmount"].Value = dblRentalMinAmount;

                objSQlComm.Parameters["@MinimumServiceTime"].Value = intMinimumServiceTime;
                objSQlComm.Parameters["@RepairCharge"].Value = dblRepairCharge;

                objSQlComm.Parameters["@RentalPrompt"].Value = strRentalPrompt;
                objSQlComm.Parameters["@RepairPromptForCharge"].Value = strRepairPromptForCharge;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;

                objSQlComm.Parameters["@POSDisplayOrder"].Value = intItemDisplayOrder;
                objSQlComm.Parameters["@AddToScaleScreen"].Value = strAddtoScaleScreen;

                objSQlComm.Parameters["@Notes2"].Value = strNotes2;

                objSQlComm.Parameters.Add(new SqlParameter("@SplitWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@SplitWeight"].Value = dblSplitWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@UOM", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@UOM"].Value = strUOM;

                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyImageExportFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ShopifyImageExportFlag"].Value = strShopifyImageExportFlag;

                objSQlComm.Parameters.Add(new SqlParameter("@ImportFrom", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ImportFrom"].Value = ImportFrom;

                objSQlComm.Parameters.Add(new SqlParameter("@ImportDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@ImportDate"].Value = String.IsNullOrEmpty(ImportFrom) ? Convert.ToDateTime("01-01-1990") : DateTime.Now;

                //if (String.IsNullOrEmpty(ImportFrom))
                //    objSQlComm.Parameters["@ImportDate"].Value = Convert.ToDateTime("01-01-1990");
                //else
                //    objSQlComm.Parameters["@ImportDate"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@BookerProductID", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@BookerProductID"].Value = BookerProductID;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSCategoryScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToPOSCategoryScreen"].Value = strAddtoPOSCategoryScreen;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch
                    {
                    }
                    objsqlReader.Close();
                }
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateData_WPF(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "";
            strSQLComm = " update Product set SKU=@SKU,SKU2=@SKU2,SKU3=@SKU3,Description=@Description, "
                           + " BinLocation=@BinLocation, AddtoPOSScreen=@AddtoPOSScreen, ScaleBarCode=@ScaleBarCode,"
                           + " ProductType=@ProductType,PriceA=@PriceA,PriceB=@PriceB,PriceC=@PriceC, "
                           + " PromptForPrice=@PromptForPrice,LastCost=@LastCost,Cost=@Cost,"
                           + " QtyOnHand=@QtyOnHand,QtyOnLayaway=@QtyOnLayaway,ReorderQty=@ReorderQty, "
                           + " NormalQty=@NormalQty,PrimaryVendorID=@PrimaryVendorID,BrandID=@BrandID, "
                           + " DepartmentID=@DepartmentID,CategoryID=@CategoryID, Points = @Points, "
                           + " AllowZeroStock=@AllowZeroStock,DisplayStockinPOS=@DisplayStockinPOS,"
                           + " PrintBarCode=@PrintBarCode,NoPriceOnLabel=@NoPriceOnLabel,MinimumAge=@MinimumAge, "
                           + " QtyToPrint=@QtyToPrint,LabelType=@LabelType,FoodStampEligible=@FoodStampEligible,ProductNotes=@ProductNotes,"
                           + " POSBackground=@POSBackground, POSScreenStyle=@POSScreenStyle,POSScreenColor=@POSScreenColor,"
                           + " POSFontType=@POSFontType,POSFontSize=@POSFontSize,TaggedInInvoice=@TaggedInInvoice,"
                           + " POSFontColor=@POSFontColor, IsBold=@IsBold, IsItalics=@IsItalics,ProductStatus=@ProductStatus,"
                           + " ScaleBackground=@ScaleBackground, ScaleScreenStyle=@ScaleScreenStyle,ScaleScreenColor=@ScaleScreenColor, "
                           + " ScaleFontType=@ScaleFontType,ScaleFontSize=@ScaleFontSize,ScaleFontColor=@ScaleFontColor,ScaleIsBold=@ScaleIsBold, ScaleIsItalics=@ScaleIsItalics,"
                           + " Season=@Season,CaseUPC=@CaseUPC,CaseQty=@CaseQty,UPC=@UPC,LinkSKU=@LinkSKU,BreakPackRatio=@BreakPackRatio,"
                           + " DecimalPlace=@DecimalPlace,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,ProductPhoto=@ProductPhoto,"
                           + " RentalPerMinute=@RentalPerMinute,RentalPerHour=@RentalPerHour,RentalPerDay=@RentalPerDay,RentalPerHalfDay=@RentalPerHalfDay,"
                           + " RentalPerWeek=@RentalPerWeek,RentalPerMonth=@RentalPerMonth,RentalDeposit=@RentalDeposit, "
                           + " MinimumServiceTime=@MinimumServiceTime,RepairCharge=@RepairCharge,"
                           + " RentalMinHour=@RentalMinHour,RentalMinAmount=@RentalMinAmount,"
                           + " RentalPrompt=@RentalPrompt,RepairPromptForCharge=@RepairPromptForCharge,Tare2=@Tare2,"
                           + " RepairPromptForTag=@RepairPromptForTag,NonDiscountable=@NonDiscountable,Tare=@Tare,AddToScaleScreen=@AddToScaleScreen,"
                           + " Notes2=@Notes2,SplitWeight=@SplitWeight, BookingExpFlag=@BookingExpFlag, UOM=@UOM, "
                           + " ShopifyImageExportFlag=@ShopifyImageExportFlag, AddToPOSCategoryScreen = @AddToPOSCategoryScreen,DiscountedCost=@DiscountedCost,ExpiryDate=@ExpiryDate where ID = @ID";

            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime));
                if (dtExpiryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpiryDate"].Value = dtExpiryDate;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@BinLocation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AddtoPOSScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PromptForPrice", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountedCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnLayaway", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ReorderQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NormalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrimaryVendorID", System.Data.SqlDbType.VarChar)); // ????
                objSQlComm.Parameters.Add(new SqlParameter("@BrandID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductPhoto", System.Data.SqlDbType.Image));


                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleIsItalics", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@DecimalPlace", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@TaggedInInvoice", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Season", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@UPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BreakPackRatio", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMinute", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerHalfDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerWeek", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPerMonth", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalDeposit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumServiceTime", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalMinAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@RepairCharge", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@RentalPrompt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForCharge", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare"].Value = dblTare;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare2"].Value = dblTare2;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToScaleScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToScaleScreen"].Value = strAddtoScaleScreen;

                objSQlComm.Parameters.Add(new SqlParameter("@Notes2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Notes2"].Value = strNotes2;

                objSQlComm.Parameters.Add(new SqlParameter("@SplitWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@SplitWeight"].Value = dblSplitWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@UOM", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@UOM"].Value = strUOM;

                objSQlComm.Parameters.Add(new SqlParameter("@ShopifyImageExportFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ShopifyImageExportFlag"].Value = strShopifyImageExportFlag;

                //objSQlComm.Parameters.Add(new SqlParameter("@POSDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@SKU2"].Value = strSKU2;
                objSQlComm.Parameters["@SKU3"].Value = strSKU3;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@BinLocation"].Value = strBinLocation;
                objSQlComm.Parameters["@AddtoPOSScreen"].Value = strAddtoPOSScreen;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@ProductType"].Value = strProductType;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                objSQlComm.Parameters["@PromptForPrice"].Value = strPromptForPrice;
                objSQlComm.Parameters["@LastCost"].Value = dblLastCost;
                objSQlComm.Parameters["@Cost"].Value = dblCost;
                objSQlComm.Parameters["@DiscountedCost"].Value = dblDiscountedCost;
                objSQlComm.Parameters["@QtyOnHand"].Value = dblQtyOnHand;
                objSQlComm.Parameters["@QtyOnLayaway"].Value = dblQtyOnLayaway;
                objSQlComm.Parameters["@ReorderQty"].Value = dblReorderQty;
                objSQlComm.Parameters["@NormalQty"].Value = dblNormalQty;
                objSQlComm.Parameters["@PrimaryVendorID"].Value = strPrimaryVendorID;
                objSQlComm.Parameters["@BrandID"].Value = intBrandID;
                objSQlComm.Parameters["@DepartmentID"].Value = intDepartmentID;
                objSQlComm.Parameters["@CategoryID"].Value = intCategoryID;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@ProductNotes"].Value = ProductNotes;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStampEligible;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;

                objSQlComm.Parameters["@ScaleScreenColor"].Value = strScaleScreenColor;
                objSQlComm.Parameters["@ScaleBackground"].Value = strScaleBackground;
                objSQlComm.Parameters["@ScaleScreenStyle"].Value = strScaleScreenStyle;
                objSQlComm.Parameters["@ScaleFontType"].Value = strScaleFontType;
                objSQlComm.Parameters["@ScaleFontSize"].Value = intScaleFontSize;
                objSQlComm.Parameters["@ScaleFontColor"].Value = strScaleFontColor;
                objSQlComm.Parameters["@ScaleIsBold"].Value = strScaleIsBold;
                objSQlComm.Parameters["@ScaleIsItalics"].Value = strScaleIsItalics;

                objSQlComm.Parameters["@DecimalPlace"].Value = intProductDecimalPlace;
                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;
                objSQlComm.Parameters["@TaggedInInvoice"].Value = strTaggedInInvoice;

                objSQlComm.Parameters["@Season"].Value = strSeason;
                objSQlComm.Parameters["@CaseUPC"].Value = strCaseUPC;
                objSQlComm.Parameters["@CaseQty"].Value = intCaseQty;
                objSQlComm.Parameters["@UPC"].Value = strUPC;
                objSQlComm.Parameters["@LinkSKU"].Value = intLinkSKU;
                objSQlComm.Parameters["@BreakPackRatio"].Value = dblBreakPackRatio;

                objSQlComm.Parameters["@RentalPerMinute"].Value = dblRentalPerMinute;
                objSQlComm.Parameters["@RentalPerHour"].Value = dblRentalPerHour;
                objSQlComm.Parameters["@RentalPerHalfDay"].Value = dblRentalPerHalfDay;
                objSQlComm.Parameters["@RentalPerDay"].Value = dblRentalPerDay;
                objSQlComm.Parameters["@RentalPerWeek"].Value = dblRentalPerWeek;
                objSQlComm.Parameters["@RentalPerMonth"].Value = dblRentalPerMonth;
                objSQlComm.Parameters["@RentalDeposit"].Value = dblRentalDeposit;
                objSQlComm.Parameters["@MinimumServiceTime"].Value = intMinimumServiceTime;
                objSQlComm.Parameters["@RepairCharge"].Value = dblRepairCharge;
                objSQlComm.Parameters["@RentalMinHour"].Value = dblRentalMinHour;
                objSQlComm.Parameters["@RentalMinAmount"].Value = dblRentalMinAmount;
                objSQlComm.Parameters["@RentalPrompt"].Value = strRentalPrompt;
                objSQlComm.Parameters["@RepairPromptForCharge"].Value = strRepairPromptForCharge;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;

                objSQlComm.Parameters["@ProductPhoto"].Value = bytProductPhoto == null ? Convert.DBNull : bytProductPhoto;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingExpFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@BookingExpFlag"].Value = strBookingExpFlag;

                //objSQlComm.Parameters["@POSDisplayOrder"].Value = intItemDisplayOrder;

                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSCategoryScreen", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AddToPOSCategoryScreen"].Value = strAddtoPOSCategoryScreen;

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

        #region Product Expiry Due Report

        public DataTable GetProductExpiryDueReport(int dueday, string dtFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = @" select p.SKU as ProductSKU, p.[Description] as ProductName, p.ProductType,
                                  dp.[Description] as RefDept, cat.[Description] as RefCat, isnull(bnd.BrandDescription,'') as refBrand, 
                                    p.ExpiryDate as ExpDate, '' as SN1, '' as SN2, '' as SN3 from product p
                                    left outer join Category cat on cat.ID = p.CategoryID
                                    left outer join Dept dp on dp.ID = p.DepartmentID
                                    left outer join BrandMaster bnd on bnd.ID = p.BrandID
                                    where p.ProductStatus = 'Y' 
                                and p.ExpiryDate is not null and  CAST(p.ExpiryDate as Date) >= CAST(GETDATE() As Date) 
                                and p.ProductType <> 'E'
                                and DATEDIFF(day,CAST(GETDATE() As Date),CAST(p.ExpiryDate as Date)) <= @dueby1
                                union
                                select p.SKU as ProductSKU, p.[Description] as ProductName, p.ProductType,
                                dp.[Description] as RefDept, cat.[Description] as RefCat, isnull(bnd.BrandDescription,'') as refBrand, 
                                d.ExpiryDate as ExpDate, isnull(d.Serial1,'') as SN1,isnull(d.Serial2,'') as SN2,
                                isnull(d.Serial3,'') as SN3   from product p
                                left outer join Category cat on cat.ID = p.CategoryID
                                left outer join Dept dp on dp.ID = p.DepartmentID
                                left outer join BrandMaster bnd on bnd.ID = p.BrandID
                                left outer join SerialHeader h on h.ProductID = p.ID
                                left outer join SerialDetail d on h.ID = d.SerialHeaderID and d.ItemID = 0 
                                where p.ProductType = 'E'  and p.ProductStatus = 'Y' and  CAST(d.ExpiryDate as Date) >= CAST(GETDATE() As Date) 
                                and DATEDIFF(day,CAST(GETDATE() As Date),CAST(d.ExpiryDate as Date)) <= @dueby2
                                Order by ExpDate, ProductSKU";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@dueby1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@dueby1"].Value = dueday;
                objSQlComm.Parameters.Add(new SqlParameter("@dueby2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@dueby2"].Value = dueday;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SeriaNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Expiry", System.Type.GetType("System.String"));


                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();


                while (objsqlReader.Read())
                {
                    try
                    {
                        string ptype = "";
                        if (objsqlReader["ProductType"].ToString() == "P") ptype = "Product";
                        if (objsqlReader["ProductType"].ToString() == "M") ptype = "Matrix";
                        if (objsqlReader["ProductType"].ToString() == "U") ptype = "Unit Of Measure";
                        if (objsqlReader["ProductType"].ToString() == "K") ptype = "Kit";
                        if (objsqlReader["ProductType"].ToString() == "E") ptype = "Serialized";
                        if (objsqlReader["ProductType"].ToString() == "F") ptype = "Fuel";
                        if (objsqlReader["ProductType"].ToString() == "W") ptype = "Weighted";
                        if (objsqlReader["ProductType"].ToString() == "D") ptype = "Donation";
                        if (objsqlReader["ProductType"].ToString() == "Q") ptype = "Entry Ticket";
                        dtbl.Rows.Add(new object[] {

                                                   objsqlReader["ProductSKU"].ToString(),
                                                   objsqlReader["ProductName"].ToString(),
                                                   ptype,
                                                   objsqlReader["RefDept"].ToString(),
                                                   objsqlReader["RefCat"].ToString(),
                                                   objsqlReader["refBrand"].ToString(),
                                                   objsqlReader["SN1"].ToString() + "  " + objsqlReader["SN2"].ToString() + "  " +
                                                   objsqlReader["SN3"].ToString(),
                                                   Functions.fnDate(objsqlReader["ExpDate"].ToString()).ToString(dtFormat)

                        });
                    }
                    catch
                    {
                    }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return null;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion
    }
}
