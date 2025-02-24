/*
 purpose : Data for Imported Information ( WEB OFFICE )
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Windows;

namespace PosDataObject
{
    public class Central
    {
        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private string strpSKU;
        private string strpSKU2;
        private string strpSKU3;
        private string strpDescription;
        private string strpBinLocation;
        private string strpProductNotes;
        private string strpPOSScreenColor;
        private string strpPOSScreenStyle;
        private string strpPOSBackground;
        private string strpPOSFontType;
        private string strpPOSFontColor;
        private string strpIsBold;
        private string strpIsItalics;
        private string strpCaseUPC;
        private string strpUPC;
        private string strpSeason;
        private string strpProductType;
        private string strpPromptForPrice;
        private string strpPrintBarCode;
        private string strpNoPriceOnLabel;
        private string strpFoodStampEligible;
        private string strpAddtoPOSScreen;
        private string strpAddToPOSCategoryScreen;
        private string strpScaleBarCode;
        private string strpAllowZeroStock;
        private string strpDisplayStockinPOS;
        private string strpProductStatus;
        private string strpRentalPrompt;

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

        public string pProductType
        {
            get { return strpProductType; }
            set { strpProductType = value; }
        }

        public string pPromptForPrice
        {
            get { return strpPromptForPrice; }
            set { strpPromptForPrice = value; }
        }

        public string pPrintBarCode
        {
            get { return strpPrintBarCode; }
            set { strpPrintBarCode = value; }
        }

        public string pNoPriceOnLabel
        {
            get { return strpNoPriceOnLabel; }
            set { strpNoPriceOnLabel = value; }
        }

        public string pFoodStampEligible
        {
            get { return strpFoodStampEligible; }
            set { strpFoodStampEligible = value; }
        }

        public string pAddtoPOSScreen
        {
            get { return strpAddtoPOSScreen; }
            set { strpAddtoPOSScreen = value; }
        }

        public string pAddToPOSCategoryScreen
        {
            get { return strpAddToPOSCategoryScreen; }
            set { strpAddToPOSCategoryScreen = value; }
        }

        public string pScaleBarCode
        {
            get { return strpScaleBarCode; }
            set { strpScaleBarCode = value; }
        }

        public string pAllowZeroStock
        {
            get { return strpAllowZeroStock; }
            set { strpAllowZeroStock = value; }
        }

        public string pDisplayStockinPOS
        {
            get { return strpDisplayStockinPOS; }
            set { strpDisplayStockinPOS = value; }
        }

        public string pProductStatus
        {
            get { return strpProductStatus; }
            set { strpProductStatus = value; }
        }

        public string pRentalPrompt
        {
            get { return strpRentalPrompt; }
            set { strpRentalPrompt = value; }
        }


        private string strpRepairPromptForCharge;
        private string strpRepairPromptForTag;
        private string strpDepartmentCode;       
        private string strpCategoryCode;



        public string pRepairPromptForCharge
        {
            get { return strpRepairPromptForCharge; }
            set { strpRepairPromptForCharge = value; }
        }

        public string pRepairPromptForTag
        {
            get { return strpRepairPromptForTag; }
            set { strpRepairPromptForTag = value; }
        }

        public string pDepartmentCode
        {
            get { return strpDepartmentCode; }
            set { strpDepartmentCode = value; }
        }


        public string pCategoryCode
        {
            get { return strpCategoryCode; }
            set { strpCategoryCode = value; }
        }

        private string strpTax1Name;
        private string strpTax2Name;
        private string strpTax3Name;
        private string strpBrandCode;


        public string pTax1Name
        {
            get { return strpTax1Name; }
            set { strpTax1Name = value; }
        }

        public string pTax2Name
        {
            get { return strpTax2Name; }
            set { strpTax2Name = value; }
        }

        public string pTax3Name
        {
            get { return strpTax3Name; }
            set { strpTax3Name = value; }
        }

        public string pBrandCode
        {
            get { return strpBrandCode; }
            set { strpBrandCode = value; }
        }


        private int intpPOSFontSize;
        private int intpScaleFontSize;
        private int intpQtyToPrint;
        private int intpLabelType;
        private int intpPoints;
        private int intpMinimumAge;
        private int intpDecimalPlace;
        private int intpCaseQty;
        private int intpLinkSKU;
        private int intpMinimumServiceTime;
        private int intpCatPOSFontSize;

        public int pPOSFontSize
        {
            get { return intpPOSFontSize; }
            set { intpPOSFontSize = value; }
        }

        public int pQtyToPrint
        {
            get { return intpQtyToPrint; }
            set { intpQtyToPrint = value; }
        }

        public int pLabelType
        {
            get { return intpLabelType; }
            set { intpLabelType = value; }
        }

        public int pPoints
        {
            get { return intpPoints; }
            set { intpPoints = value; }
        }

        public int pMinimumAge
        {
            get { return intpMinimumAge; }
            set { intpMinimumAge = value; }
        }

        public int pDecimalPlace
        {
            get { return intpDecimalPlace; }
            set { intpDecimalPlace = value; }
        }

        public int pCaseQty
        {
            get { return intpCaseQty; }
            set { intpCaseQty = value; }
        }

        public int pLinkSKU
        {
            get { return intpLinkSKU; }
            set { intpLinkSKU = value; }
        }

        public int pMinimumServiceTime
        {
            get { return intpMinimumServiceTime; }
            set { intpMinimumServiceTime = value; }
        }

        public int pCatPOSFontSize
        {
            get { return intpCatPOSFontSize; }
            set { intpCatPOSFontSize = value; }
        }


        private double dblpPriceA;
        private double dblpPriceB;
        private double dblpPriceC;
        private double dblpLastCost;
        private double dblpCost;
        private double dblpDiscountedCost;
        private double dblpQtyOnHand;
        private double dblpQtyOnLayaway;
        private double dblpReorderQty;
        private double dblpNormalQty;
        private double dblpBreakPackRatio;

        public double pPriceA
        {
            get { return dblpPriceA; }
            set { dblpPriceA = value; }
        }

        public double pPriceB
        {
            get { return dblpPriceB; }
            set { dblpPriceB = value; }
        }

        public double pPriceC
        {
            get { return dblpPriceC; }
            set { dblpPriceC = value; }
        }

        public double pLastCost
        {
            get { return dblpLastCost; }
            set { dblpLastCost = value; }
        }

        public double pCost
        {
            get { return dblpCost; }
            set { dblpCost = value; }
        }

        public double pDCost
        {
            get { return dblpDiscountedCost; }
            set { dblpDiscountedCost = value; }
        }

        public double pQtyOnHand
        {
            get { return dblpQtyOnHand; }
            set { dblpQtyOnHand = value; }
        }

        public double pQtyOnLayaway
        {
            get { return dblpQtyOnLayaway; }
            set { dblpQtyOnLayaway = value; }
        }

        public double pReorderQty
        {
            get { return dblpReorderQty; }
            set { dblpReorderQty = value; }
        }

        public double pNormalQty
        {
            get { return dblpNormalQty; }
            set { dblpNormalQty = value; }
        }

        public double pBreakPackRatio
        {
            get { return dblpBreakPackRatio; }
            set { dblpBreakPackRatio = value; }
        }


        private double dblpRentalPerMinute;
        private double dblpRentalPerHour;
        private double dblpRentalPerHalfDay; 
        private double dblpRentalPerDay;
        private double dblpRentalPerWeek;
        private double dblpRentalPerMonth;
        private double dblpRentalDeposit;
        private double dblpRentalMinHour;
        private double dblpRentalMinAmount;
        private double dblpRepairCharge;
        private double dblpTax1Rate;
        private double dblpTax2Rate;
        private double dblpTax3Rate;

        public double pRentalPerMinute
        {
            get { return dblpRentalPerMinute; }
            set { dblpRentalPerMinute = value; }
        }

        public double pRentalPerHour
        {
            get { return dblpRentalPerHour; }
            set { dblpRentalPerHour = value; }
        }

        public double pRentalPerDay
        {
            get { return dblpRentalPerDay; }
            set { dblpRentalPerDay = value; }
        }

        public double pRentalPerWeek
        {
            get { return dblpRentalPerWeek; }
            set { dblpRentalPerWeek = value; }
        }

        public double pRentalPerMonth
        {
            get { return dblpRentalPerMonth; }
            set { dblpRentalPerMonth = value; }
        }

        public double pRentalDeposit
        {
            get { return dblpRentalDeposit; }
            set { dblpRentalDeposit = value; }
        }

        public double pRentalMinHour
        {
            get { return dblpRentalMinHour; }
            set { dblpRentalMinHour = value; }
        }

        public double pRentalMinAmount
        {
            get { return dblpRentalMinAmount; }
            set { dblpRentalMinAmount = value; }
        }

        public double pRepairCharge
        {
            get { return dblpRepairCharge; }
            set { dblpRepairCharge = value; }
        }

        public double pTax1Rate
        {
            get { return dblpTax1Rate; }
            set { dblpTax1Rate = value; }
        }

        public double pTax2Rate
        {
            get { return dblpTax2Rate; }
            set { dblpTax2Rate = value; }
        }

        public double pTax3Rate
        {
            get { return dblpTax3Rate; }
            set { dblpTax3Rate = value; }
        }

        private int intpPID;

        public int pPID
        {
            get { return intpPID; }
            set { intpPID = value; }
        }

        public string pSKU
        {
            get { return strpSKU; }
            set { strpSKU = value; }
        }

        public string pSKU2
        {
            get { return strpSKU2; }
            set { strpSKU2 = value; }
        }
        public string pSKU3
        {
            get { return strpSKU3; }
            set { strpSKU3 = value; }
        }

        public string pDescription
        {
            get { return strpDescription; }
            set { strpDescription = value; }
        }

        public string pBinLocation
        {
            get { return strpBinLocation; }
            set { strpBinLocation = value; }
        }

        public string pProductNotes
        {
            get { return strpProductNotes; }
            set { strpProductNotes = value; }
        }

        public string pPOSScreenColor
        {
            get { return strpPOSScreenColor; }
            set { strpPOSScreenColor = value; }
        }

        public string pPOSScreenStyle
        {
            get { return strpPOSScreenStyle; }
            set { strpPOSScreenStyle = value; }
        }

        public string pPOSBackground
        {
            get { return strpPOSBackground; }
            set { strpPOSBackground = value; }
        }

        public string pPOSFontType
        {
            get { return strpPOSFontType; }
            set { strpPOSFontType = value; }
        }

        public string pPOSFontColor
        {
            get { return strpPOSFontColor; }
            set { strpPOSFontColor = value; }
        }

        public string pIsBold
        {
            get { return strpIsBold; }
            set { strpIsBold = value; }
        }

        public string pIsItalics
        {
            get { return strpIsItalics; }
            set { strpIsItalics = value; }
        }

        public string pCaseUPC
        {
            get { return strpCaseUPC; }
            set { strpCaseUPC = value; }
        }

        public string pUPC
        {
            get { return strpUPC; }
            set { strpUPC = value; }
        }

        public string pSeason
        {
            get { return strpSeason; }
            set { strpSeason = value; }
        }

        private string strpRntTax1Name;

        private string strpRntTax2Name;

        private string strpRntTax3Name;

        private double dblpRntTax1Rate;

        private double dblpRntTax2Rate;

        private double dblpRntTax3Rate;

        public string pRntTax1Name
        {
            get { return strpRntTax1Name; }
            set { strpRntTax1Name = value; }
        }

        public string pRntTax2Name
        {
            get { return strpRntTax2Name; }
            set { strpRntTax2Name = value; }
        }

        public string pRntTax3Name
        {
            get { return strpRntTax3Name; }
            set { strpRntTax3Name = value; }
        }

        public double pRntTax1Rate
        {
            get { return dblpRntTax1Rate; }
            set { dblpRntTax1Rate = value; }
        }

        public double pRntTax2Rate
        {
            get { return dblpRntTax2Rate; }
            set { dblpRntTax2Rate = value; }
        }

        public double pRntTax3Rate
        {
            get { return dblpRntTax3Rate; }
            set { dblpRntTax3Rate = value; }
        }

        private double pdblTare;

        public double pTare
        {
            get { return pdblTare; }
            set { pdblTare = value; }
        }

        private string strUOM;

        public string UOM
        {
            get { return strUOM; }
            set { strUOM = value; }
        }

        private string strpAddtoScaleScreen;
        private string strpNonDiscountable;
        private string strpScaleScreenColor;
        private string strpScaleScreenStyle;
        private string strpScaleBackground;
        private string strpScaleFontType;
        private string strpScaleFontColor;
        private string strpScaleIsBold;
        private string strpScaleIsItalics;

        private double pdblTare2;
        public double pTare2
        {
            get { return pdblTare2; }
            set { pdblTare2 = value; }
        }

        private string strpPNotes2;
        public string pPNotes2
        {
            get { return strpPNotes2; }
            set { strpPNotes2 = value; }
        }

        public string pScaleScreenColor
        {
            get { return strpScaleScreenColor; }
            set { strpScaleScreenColor = value; }
        }

        public string pScaleScreenStyle
        {
            get { return strpScaleScreenStyle; }
            set { strpScaleScreenStyle = value; }
        }

        public string pScaleBackground
        {
            get { return strpScaleBackground; }
            set { strpScaleBackground = value; }
        }

        public string pScaleFontType
        {
            get { return strpScaleFontType; }
            set { strpScaleFontType = value; }
        }

        public string pScaleFontColor
        {
            get { return strpScaleFontColor; }
            set { strpScaleFontColor = value; }
        }

        public string pScaleIsBold
        {
            get { return strpScaleIsBold; }
            set { strpScaleIsBold = value; }
        }

        public string pScaleIsItalics
        {
            get { return strpScaleIsItalics; }
            set { strpScaleIsItalics = value; }
        }

        public double pRentalPerHalfDay
        {
            get { return dblpRentalPerHalfDay; }
            set { dblpRentalPerHalfDay = value; }
        }

        public int pScaleFontSize
        {
            get { return intpScaleFontSize; }
            set { intpScaleFontSize = value; }
        }

        public string pAddtoScaleScreen
        {
            get { return strpAddtoScaleScreen; }
            set { strpAddtoScaleScreen = value; }
        }

        public string pNonDiscountable
        {
            get { return strpNonDiscountable; }
            set { strpNonDiscountable = value; }
        }


        private double dblSplitWeight;

        public double SplitWeight
        {
            get { return dblSplitWeight; }
            set { dblSplitWeight = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DataTable FetchOtherStoreForInventory(string pthisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select distinct StoreCode from importinventory where storecode <> @store order by StoreCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@store"].Value = pthisstore;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCodeGeneral", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["StoreCode"].ToString(), objSQLReader["StoreCode"].ToString() });
                }
                dtbl.Rows.Add(new object[] { DataObjectCulture_All,"All" });

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

        public DataTable FetchOtherStoreForMatrixInventory(string pthisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select distinct StoreCode from importmatrixinventory where storecode <> @store order by StoreCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@store"].Value = pthisstore;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCodeGeneral", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["StoreCode"].ToString(), objSQLReader["StoreCode"].ToString() });
                }
                dtbl.Rows.Add(new object[] { DataObjectCulture_All, "All" });

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

        public DataTable FetchOtherStoreForCustomers(string pthisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select distinct issuestore from customer where issuestore <> @store order by issuestore ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@store"].Value = pthisstore;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("issuestore", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["issuestore"].ToString() });
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

        public DataTable FetchSKU()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select distinct SKU,Description from ImportInventory order by SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["SKU"].ToString(), objSQLReader["Description"].ToString(), objSQLReader["SKU"].ToString() });
                }

                dtbl.Rows.Add(new object[] { strDataObjectCulture_All, strDataObjectCulture_All, "All" });

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

        public DataTable ShowInventory(string pstorecode, string pSKU)
        {
            DataTable dtbl = new DataTable();

            string sqlfilter = "";
            string sqlfilter1 = "";
            if (pstorecode != "All")
            {
                sqlfilter = " and StoreCode = '" + pstorecode + "'";
            }
            if (pSKU != "All")
            {
                sqlfilter1 = " and SKU = '" + pSKU + "'";
            }

            string strSQLComm = " select distinct SKU,StoreCode,Description,QtyOnHand,QtyOnLayaway,NormalQty,ReorderQty,ProductType  "
                              + " from ImportInventory where 1 =1 " + sqlfilter + sqlfilter1 + " order by storecode,SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string qty = "";
                    string qtyl = "";
                    string qtyn = "";
                    string qtyr = "";

                    int rval1 = 0;
                    bool blret1 = false;
                    blret1 = Int32.TryParse(objSQLReader["QtyOnHand"].ToString(), out rval1);
                    if (blret1) qty = Convert.ToInt32(objSQLReader["QtyOnHand"].ToString()).ToString();
                    else qty = Convert.ToDouble(objSQLReader["QtyOnHand"].ToString()).ToString();

                    int rval2 = 0;
                    bool blret2 = false;
                    blret2 = Int32.TryParse(objSQLReader["QtyOnLayaway"].ToString(), out rval2);
                    if (blret2) qtyl = Convert.ToInt32(objSQLReader["QtyOnLayaway"].ToString()).ToString();
                    else qtyl = Convert.ToDouble(objSQLReader["QtyOnLayaway"].ToString()).ToString();

                    int rval3 = 0;
                    bool blret3 = false;
                    blret3 = Int32.TryParse(objSQLReader["NormalQty"].ToString(), out rval3);
                    if (blret3) qtyn = Convert.ToInt32(objSQLReader["NormalQty"].ToString()).ToString();
                    else qtyn = Convert.ToDouble(objSQLReader["NormalQty"].ToString()).ToString();

                    int rval4 = 0;
                    bool blret4 = false;
                    blret4 = Int32.TryParse(objSQLReader["ReorderQty"].ToString(), out rval4);
                    if (blret4) qtyr = Convert.ToInt32(objSQLReader["ReorderQty"].ToString()).ToString();
                    else qtyr = Convert.ToDouble(objSQLReader["ReorderQty"].ToString()).ToString();

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["StoreCode"].ToString(),
                                                objSQLReader["SKU"].ToString(),
												objSQLReader["Description"].ToString(),
                                                qty,
    											qtyl,
                                                qtyn,
                                                qtyr,
                                                objSQLReader["ProductType"].ToString()});
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

        public DataTable ShowMatrixInventory(string pstorecode, string pSKU)
        {
            DataTable dtbl = new DataTable();

            string sqlfilter = "";
            string sqlfilter1 = "";
            if (pstorecode != "All")
            {
                sqlfilter = " and i.StoreCode = '" + pstorecode + "'";
            }
            if (pSKU != "All")
            {
                sqlfilter1 = " and i.SKU = '" + pSKU + "'";
            }

            string strSQLComm = " select distinct i.SKU,isnull(p.Description,'') as ProductName, i.storecode,isnull(m.Option1Name,'') as Cat1,isnull(m.Option2Name,'') as Cat2,"
                              + " isnull(m.Option3Name,'') as Cat3,i.OptionValue1,i.OptionValue2,i.OptionValue3,i.QtyOnHand "
                              + " from ImportMatrixInventory i left outer join product p on i.SKU = p.SKU "
                              + " left outer join Matrixoptions m on m.productid = p.ID "
                              + " where 1 =1 " + sqlfilter + sqlfilter1 + " order by i.storecode,i.SKU ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("F", System.Type.GetType("System.String"));
                dtbl.Columns.Add("V", System.Type.GetType("System.String"));
                dtbl.Columns.Add("H", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU1", System.Type.GetType("System.String"));
                string psku = "";
                string pstore = "";


                while (objSQLReader.Read())
                {
                    string c1 = "";
                    string c2 = "";
                    string c3 = "";

                    string cv1 = "";
                    string cv2 = "";
                    string cv3 = "";

                    string q = "";

                    string CC = "";
                    string CV = "";
                    string sep = "     ";
                    if ((objSQLReader["Cat1"].ToString() == "") && (objSQLReader["Cat1"].ToString() == "") && (objSQLReader["Cat1"].ToString() == "")) continue;
                    if (objSQLReader["Cat1"].ToString() != "")
                    {
                        c1 = objSQLReader["Cat1"].ToString();
                        cv1 = objSQLReader["OptionValue1"].ToString();

                        CC = c1.PadRight(30 - c1.Length) + sep;
                        CV = cv1.PadRight(30 - cv1.Length) + sep;

                        if (objSQLReader["Cat2"].ToString() != "")
                        {
                            c2 = objSQLReader["Cat2"].ToString();
                            cv2 = objSQLReader["OptionValue2"].ToString();

                            CC = CC + c2.PadRight(30 - c2.Length) + sep;
                            CV = CV + cv2.PadRight(30 - cv2.Length) + sep;

                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c3 = objSQLReader["Cat3"].ToString();
                                cv3 = objSQLReader["OptionValue3"].ToString();

                                CC = CC + c3.PadRight(30 - c3.Length) + sep;
                                CV = CV + cv3.PadRight(30 - cv3.Length) + sep;
                            }
                        }
                        else
                        {
                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c2 = objSQLReader["Cat3"].ToString();
                                cv2 = objSQLReader["OptionValue3"].ToString();

                                CC = CC + c2.PadRight(30 - c2.Length) + sep;
                                CV = CV + cv2.PadRight(30 - cv2.Length) + sep;
                            }
                        }
                    }
                    else
                    {
                        if (objSQLReader["Cat2"].ToString() != "")
                        {
                            c1 = objSQLReader["Cat2"].ToString();
                            cv1 = objSQLReader["OptionValue2"].ToString();

                            CC = CC + c1.PadRight(30 - c1.Length) + sep;
                            CV = CV + cv1.PadRight(30 - cv1.Length) + sep;

                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c2 = objSQLReader["Cat3"].ToString();
                                cv2 = objSQLReader["OptionValue3"].ToString();

                                CC = CC + c2.PadRight(30 - c2.Length) + sep;
                                CV = CV + cv2.PadRight(30 - cv2.Length) + sep;
                            }
                        }
                        else
                        {
                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c1 = objSQLReader["Cat3"].ToString();
                                cv1 = objSQLReader["OptionValue3"].ToString();

                                CC = CC + c1.PadRight(30 - c1.Length) + sep;
                                CV = CV + cv1.PadRight(30 - cv1.Length) + sep;
                            }
                        }
                    }

                    if ((objSQLReader["SKU"].ToString() != psku) || (objSQLReader["StoreCode"].ToString() != pstore))
                    {
                        dtbl.Rows.Add(new object[] {
                                                objSQLReader["StoreCode"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["ProductName"].ToString(),
												CC,
                                                "",
                                                "Y",
                                                 objSQLReader["SKU"].ToString()});
                    }


                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["StoreCode"].ToString(),
                                                "",
                                                "",
												CV,
                                                objSQLReader["QtyOnHand"].ToString(),
                                                "N",
                                                 objSQLReader["SKU"].ToString()});

                    psku = objSQLReader["SKU"].ToString();
                    pstore = objSQLReader["StoreCode"].ToString();
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
        
        public DataTable ShowMatrixQty(string pstorecode, string pskuval, string cons)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string sqlfilter = "";
            string sqlfilter1 = "";
            string sql1 = "";
            if (cons != "Y")
            {
                sqlfilter = " and i.SKU = '" + pskuval + "'" + " and i.storecode = '" + pstorecode + "'";

                strSQLComm =    " select isnull(m.Option1Name,'') as Cat1,isnull(m.Option2Name,'') as Cat2,"
                              + " isnull(m.Option3Name,'') as Cat3,i.OptionValue1,i.OptionValue2,i.OptionValue3,i.QtyOnHand as qty "
                              + " from ImportMatrixInventory i left outer join ImportMatrixoptions m on i.SKU = m.SKU "
                              + " where (1 = 1) "
                              + sqlfilter;
            }
            else
            {
                sqlfilter = " and i.SKU = '" + pskuval + "'";

                strSQLComm =    " select isnull(m.Option1Name,'') as Cat1,isnull(m.Option2Name,'') as Cat2,"
                              + " isnull(m.Option3Name,'') as Cat3,i.OptionValue1,i.OptionValue2,i.OptionValue3,sum(i.QtyOnHand) as qty "
                              + " from ImportMatrixInventory i left outer join ImportMatrixoptions m on i.SKU = m.SKU "
                              + " where (1 = 1) "
                              + sqlfilter + " group by m.Option1Name,m.Option2Name,m.Option3Name,i.OptionValue1,i.OptionValue2,i.OptionValue3 ";
            }
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, Connection);

            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { Connection.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("F1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("F2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("F3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("V1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("V2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("V3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Q", System.Type.GetType("System.String"));
                string psku = "";


                while (objSQLReader.Read())
                {
                    string c1 = "";
                    string c2 = "";
                    string c3 = "";

                    string cv1 = "";
                    string cv2 = "";
                    string cv3 = "";


                    if ((objSQLReader["Cat1"].ToString() == "") && (objSQLReader["Cat1"].ToString() == "") && (objSQLReader["Cat1"].ToString() == "")) continue;
                    if (objSQLReader["Cat1"].ToString() != "")
                    {
                        c1 = objSQLReader["Cat1"].ToString();
                        cv1 = objSQLReader["OptionValue1"].ToString();

                        if (objSQLReader["Cat2"].ToString() != "")
                        {
                            c2 = objSQLReader["Cat2"].ToString();
                            cv2 = objSQLReader["OptionValue2"].ToString();

                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c3 = objSQLReader["Cat3"].ToString();
                                cv3 = objSQLReader["OptionValue3"].ToString();

                            }
                        }
                        else
                        {
                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c2 = objSQLReader["Cat3"].ToString();
                                cv2 = objSQLReader["OptionValue3"].ToString();

                            }
                        }
                    }
                    else
                    {
                        if (objSQLReader["Cat2"].ToString() != "")
                        {
                            c1 = objSQLReader["Cat2"].ToString();
                            cv1 = objSQLReader["OptionValue2"].ToString();


                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c2 = objSQLReader["Cat3"].ToString();
                                cv2 = objSQLReader["OptionValue3"].ToString();

                            }
                        }
                        else
                        {
                            if (objSQLReader["Cat3"].ToString() != "")
                            {
                                c1 = objSQLReader["Cat3"].ToString();
                                cv1 = objSQLReader["OptionValue3"].ToString();

                            }
                        }
                    }
                    string qty = "";
                    int rval1 = 0;
                    bool blret1 = false;
                    blret1 = Int32.TryParse(objSQLReader["qty"].ToString(), out rval1);
                    if (blret1) qty = Convert.ToInt32(objSQLReader["qty"].ToString()).ToString() + " ";
                    else qty = Convert.ToDouble(objSQLReader["qty"].ToString()).ToString() + " ";

                    dtbl.Rows.Add(new object[] {
                                                
                                                c1,c2,c3,cv1,cv2,cv3,
                                                qty
                                               });


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
        }
        
        public DataTable FetchCustomers(string StoreC, string FilterCode)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            string sql = "";
                if (FilterCode != "All") sql = " and issuestore = '" + FilterCode + "' ";

                strSQLComm = " select issuestore,ID,CustomerID,FirstName,LastName,Company,Address1,WorkPhone,City,Salutation from Customer " +
                             " where (1=1) and issuestore <> '" + StoreC + "' " + sql + " order by issuestore";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("issuestore", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["issuestore"].ToString(),
                                                   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["LastName"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["Salutation"].ToString()});
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


        #region First Time Export

        public DataTable FetchDepartment()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select DepartmentID,Description,ScaleFlag from Dept order by DepartmentID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Desc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["DepartmentID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["ScaleFlag"].ToString()});
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

        public DataTable FetchCategory()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Category order by CategoryID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Desc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSItemFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MaxItemsforPOS", System.Type.GetType("System.String"));
                


                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
												objSQLReader["CategoryID"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["POSBackground"].ToString(),
                                                objSQLReader["POSScreenStyle"].ToString(),
                                                objSQLReader["POSScreenColor"].ToString(),
                                                objSQLReader["POSFontType"].ToString(),
                                                objSQLReader["POSFontSize"].ToString(),
                                                objSQLReader["POSFontColor"].ToString(),
                                                objSQLReader["IsBold"].ToString(),
                                                objSQLReader["IsItalics"].ToString(),
                                                objSQLReader["POSItemFontColor"].ToString(),
                                                objSQLReader["MaxItemsforPOS"].ToString()
                                                });
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

        public DataTable FetchBrand()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select BrandID,BrandDescription from BrandMaster order by BrandID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Desc", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["BrandID"].ToString(),
												   objSQLReader["BrandDescription"].ToString()
                                                   });
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

        public DataTable FetchVendor()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from Vendor order by VendorID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();
                
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AccountNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumOrderAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
												objSQLReader["VendorID"].ToString(),
                                                objSQLReader["Name"].ToString(),
												objSQLReader["Contact"].ToString(),
                                                objSQLReader["AccountNo"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Country"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["Fax"].ToString(),
                                                objSQLReader["Phone"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["Notes"].ToString(),
                                                objSQLReader["MinimumOrderAmount"].ToString()});
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

        public DataTable FetchActiveTax()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,TaxType,TaxRate,TaxName From TaxHeader where Active='Yes' ";
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TaxType"].ToString(),
                                                   Functions.fnDouble(objSQLReader["TaxRate"].ToString()),
                                                   objSQLReader["TaxName"].ToString()
                                                   });
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

        public DataTable FetchTaxBreakpoints(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from TaxDetail where RefID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                
                dtbl.Columns.Add("BreakPoints", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));

                double dblbp; double dblTax;
                while (objSQLReader.Read())
                {
                    dblbp = 0.00; dblTax = 0.00;
                    if (objSQLReader["BreakPoints"].ToString() != "")
                        dblbp = Functions.fnDouble(objSQLReader["BreakPoints"].ToString());
                    if (objSQLReader["Tax"].ToString() != "")
                        dblTax = Functions.fnDouble(objSQLReader["Tax"].ToString());

                    dtbl.Rows.Add(new object[] { dblbp, dblTax });
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

        public DataTable FetchProduct()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select * from product where ProductType <> 'S'  and ProductStatus = 'Y' ";
            //strSQLComm = " select * from product";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

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
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
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
                dtbl.Columns.Add("Tare", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonDiscountable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToScaleScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentalPerHalfDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleIsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleIsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SplitWeight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UOM", System.Type.GetType("System.String"));
                
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
                                                objSQLReader["DiscountedCost"].ToString(),
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
                                                objSQLReader["Tare"].ToString(),
                                                objSQLReader["Tare2"].ToString(),
                                                objSQLReader["NonDiscountable"].ToString(),
                                                objSQLReader["AddToScaleScreen"].ToString(),
                                                objSQLReader["RentalPerHalfDay"].ToString(),
                                                objSQLReader["ScaleBackground"].ToString(),
                                                objSQLReader["ScaleScreenStyle"].ToString(),
                                                objSQLReader["ScaleScreenColor"].ToString(),
                                                objSQLReader["ScaleFontType"].ToString(),
                                                objSQLReader["ScaleFontSize"].ToString(),
                                                objSQLReader["ScaleFontColor"].ToString(),
                                                objSQLReader["ScaleIsBold"].ToString(),
                                                objSQLReader["ScaleIsItalics"].ToString(),
                                                objSQLReader["Notes2"].ToString(),
                                                objSQLReader["SplitWeight"].ToString(),
                                                objSQLReader["UOM"].ToString()});
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

        public DataTable FetchProductBrand(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from BrandMaster where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
  
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["BrandID"].ToString() });
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductCategory(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from category where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["CategoryID"].ToString()});
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductDepartment(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from dept where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
               
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["DepartmentID"].ToString()});
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from TaxMapping TM Left Outer Join TaxHeader T on TM.TaxID = T.ID "
                                + " where TM.MappingID = @ID AND TM.MappingType = 'Product'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["TaxName"].ToString(),
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductRentTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from TaxMapping TM Left Outer Join TaxHeader T on TM.TaxID = T.ID "
                                + " where TM.MappingID = @ID AND TM.MappingType = 'Rent'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["TaxName"].ToString(),
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductPrimayVendor(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select vp.*,v.VendorID as VCode,v.name as VendorName,v.Contact,v.Address1,v.Address2,v.City,v.State,v.Country,v.Zip,"
                              + " v.Phone,v.Fax,v.EMail,v.AccountNo,v.Notes,v.MinimumOrderAmount,p.SKU from VendPart vp Left Outer Join Vendor v "
                              + " on vp.VendorID = v.ID left outer join product p on p.id = vp.productid where vp.productid = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsPrimary", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PackQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Shrink", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["VCode"].ToString(),
                                                objSQLReader["PartNumber"].ToString(),
                                                objSQLReader["Price"].ToString(),
                                                objSQLReader["IsPrimary"].ToString(),
                                                objSQLReader["PackQty"].ToString(),
                                                objSQLReader["Shrink"].ToString()
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
                MessageBox.Show(strErrMsg);
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchProductMatrixOptions(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select p.SKU,m.Option1Name,m.Option2Name,m.Option3Name from matrixoptions m left outer join product p on p.id = m.productid "
                              + " where m.productid = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option3Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["SKU"].ToString(),
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

        public DataTable FetchProductMatrixValues(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select p.SKU,v.ValueID,v.OptionValue,v.OptionDefault from matrixvalues v "
                              + " left outer join matrixoptions m on v.matrixoptionid = m.id "
                              + " left outer join product p on p.id = m.productid where m.productid = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ValueID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionDefault", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["ValueID"].ToString(),
                                                objSQLReader["OptionValue"].ToString(),
                                                objSQLReader["OptionDefault"].ToString()
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

        public DataTable FetchProductMatrix(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select p.SKU,v.OptionValue1,v.OptionValue2,v.OptionValue3,v.QtyOnHand from matrix v "
                              + " left outer join matrixoptions m on v.matrixoptionid = m.id "
                              + " left outer join product p on p.id = m.productid where m.productid = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            //objSQlComm.CommandTimeout = 90;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["OptionValue1"].ToString(),
                                                objSQLReader["OptionValue2"].ToString(),
                                                objSQLReader["OptionValue3"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString()
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

        public bool IfExistTaxInWebOffice(string pCode, int pclient)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from TaxHeader where upper(TaxName) = @prm and clientid = @prm2  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm2"].Value = pclient;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistStoreInWebOffice(string pCode, int pClicntId)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from stores where storecode = @prm and clientId = @prm2  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm2"].Value = pClicntId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistZipInWebOffice(string pZip, int pClientId)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from zipcode where zip = @prm and clientid = @prm2  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pZip;
            objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm2"].Value = pClientId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistDepartmentInWebOffice(string pCode)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from dept where DepartmentID = @prm  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistCategoryInWebOffice(string pCode)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from category where CategoryID = @prm  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistBrandInWebOffice(string pCode)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from brandmaster where BrandID = @prm  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }

        public bool IfExistVendorInWebOffice(string pCode)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from vendor where VendorID = @prm  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }
        
        public bool IfExistTaxInWebOffice(string pCode)
        {
            bool f = false;

            string strSQLComm = " select count(*) as cnt from TaxHeader where upper(TaxName) = @prm  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["cnt"].ToString()) > 0) f = true;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return f;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }


        public string UpdateStoreForWebExport(string pscode, string psname)
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


        public string UpdateStoreInWebOffice(string pcode, string pname, int puser, int pClient, string pcuurency, int HostID)
        {
            string strSQLComm = @"
                    begin	
	                    update Stores set StoreCode = @StoreCode, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where clientId = @ClientId and ID = @ID
                    end";

            /*
            string strSQLComm = @"
                    begin	
	                    if exists(select  1  from stores where syncID = @syncID and clientId = @ClientId)
		                    begin
			                    update Stores set StoreCode = @StoreCode, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where syncID = @syncID and clientId = @ClientId
		                    end

	                    else if exists(select  1  from stores where storecode = @StoreCode and clientId = @ClientId)
		                    begin
			                    update Stores set syncID = @syncID, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where storecode = @StoreCode and clientId = @ClientId
		                    end
	                    else
		                    begin
			                        insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID)  
			                        values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID)
		                    end
                    end";
            */

            //  string strSQLComm = " insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID) "
            //                  + " values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID) ";
            //dddd

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CurrencySymbol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Value = pClient;

                objSQlComm.Parameters["@StoreCode"].Value = pcode;
                objSQlComm.Parameters["@StoreName"].Value = pname;
                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@CurrencySymbol"].Value = pcuurency;
                objSQlComm.Parameters["@ID"].Value = HostID;

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


        public string InsertStoreInWebOfficeNew(string pcode, string pname, int puser, int pClient, string pcuurency, string dtStoreId)
        {
            string strSQLComm = @"
                    begin	
	                     insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID)  
			                        values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID)
                    end";

            /*
            string strSQLComm = @"
                    begin	
	                    if exists(select  1  from stores where syncID = @syncID and clientId = @ClientId)
		                    begin
			                    update Stores set StoreCode = @StoreCode, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where syncID = @syncID and clientId = @ClientId
		                    end

	                    else if exists(select  1  from stores where storecode = @StoreCode and clientId = @ClientId)
		                    begin
			                    update Stores set syncID = @syncID, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where storecode = @StoreCode and clientId = @ClientId
		                    end
	                    else
		                    begin
			                        insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID)  
			                        values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID)
		                    end
                    end";
            */

            //  string strSQLComm = " insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID) "
            //                  + " values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID) ";
            //dddd

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CurrencySymbol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@syncID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Value = pClient;

                objSQlComm.Parameters["@StoreCode"].Value = pcode;
                objSQlComm.Parameters["@StoreName"].Value = pname;
                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@CurrencySymbol"].Value = pcuurency;
                objSQlComm.Parameters["@syncID"].Value = Functions.fnInt32(dtStoreId);

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



        public string InsertStoreInWebOffice(string pcode, string pname, int puser, int pClient, string pcuurency, string dtStoreId)
        {
            string strSQLComm = @"
                    begin	
	                    if exists(select  1  from stores where syncID = @syncID and clientId = @ClientId)
		                    begin
			                    update Stores set StoreCode = @StoreCode, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where syncID = @syncID and clientId = @ClientId
		                    end

	                    else if exists(select  1  from stores where storecode = @StoreCode and clientId = @ClientId)
		                    begin
			                    update Stores set syncID = @syncID, StoreName = @StoreName, LastChangedOn = @LastChangedOn, CurrencySymbol = @CurrencySymbol
			                    where storecode = @StoreCode and clientId = @ClientId
		                    end
	                    else
		                    begin
			                        insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID)  
			                        values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID)
		                    end
                    end";


          //  string strSQLComm = " insert into Stores( StoreCode,StoreName,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientId, CurrencySymbol,syncID) "
            //                  + " values( @StoreCode,@StoreName,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId, @CurrencySymbol,@syncID) ";
            //dddd

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CurrencySymbol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@syncID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Value = pClient;

                objSQlComm.Parameters["@StoreCode"].Value = pcode;
                objSQlComm.Parameters["@StoreName"].Value = pname;
                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@CurrencySymbol"].Value = pcuurency;
                objSQlComm.Parameters["@syncID"].Value = Convert.ToInt32(dtStoreId);

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


        public string UpdateStoreHostID(int pHostID)
        {
            string strSQLComm = " update CentralExportImport set HostID = @param ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@param"].Value = pHostID;

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

        public int GetStoreHostID(string pCode, int pClicntId)
        {
            int val = 0;

            string strSQLComm = " select isnull(ID,0) as rID from stores where storecode = @prm and clientId = @prm2  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = pCode;
            objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm2"].Value = pClicntId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rID"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public string InsertDepartmentInWebOffice(int prefclient, string pcode, string pdesc, string pscale_f, int puser)
        {

            string strSQLComm = "sp_co_imp_dept";

            SqlCommand objSQlComm = null;
            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;

                //objSQLTran = sqlConn.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@d_code", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@d_code"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@d_code"].Value = pcode;

                objSQlComm.Parameters.Add(new SqlParameter("@d_name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@d_name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@d_name"].Value = pdesc;

                objSQlComm.Parameters.Add(new SqlParameter("@d_scaleF", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@d_scaleF"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@d_scaleF"].Value = pscale_f;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
               
                //objSQLTran.Dispose();
                return strErrMsg;
            }
        }

        public int GetCategoryDisplayOrderinWebOffice()
        {
            int val = 0;
            string strSQLComm = " select isnull(Max(POSDisplayOrder),0) + 1 as cnt from Category ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["cnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public string InsertCategoryInWebOffice(int prefclient, string pcode, string pdesc, int pdisp, string pposscreenclr, string pposbkgrd, string pposscreensty,
                                                string pposfontty, int pposfontsz, string pposfontclr, string ppositemfontclr, string pbold,
                                                string pitalic, int puser, int pmaxitemsforpos)
        {
            string strSQLComm = "sp_co_imp_category";

            SqlCommand objSQlComm = null;
            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;

                //objSQLTran = sqlConn.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@c_code", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@c_code"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_code"].Value = pcode;

                objSQlComm.Parameters.Add(new SqlParameter("@c_name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@c_name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_name"].Value = pdesc;

                objSQlComm.Parameters.Add(new SqlParameter("@c_posscreen", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_posscreen"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_posscreen"].Value = pposscreenclr;

                objSQlComm.Parameters.Add(new SqlParameter("@c_posbkground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_posbkground"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_posbkground"].Value = pposbkgrd;

                objSQlComm.Parameters.Add(new SqlParameter("@c_posscreenstyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_posscreenstyle"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_posscreenstyle"].Value = pposscreensty;

                objSQlComm.Parameters.Add(new SqlParameter("@c_fonttype", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_fonttype"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_fonttype"].Value = pposfontty;

                objSQlComm.Parameters.Add(new SqlParameter("@c_fontsize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@c_fontsize"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_fontsize"].Value = pposfontsz;

                objSQlComm.Parameters.Add(new SqlParameter("@c_fontcolor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_fontcolor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_fontcolor"].Value = pposfontclr;

                objSQlComm.Parameters.Add(new SqlParameter("@c_bold", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@c_bold"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_bold"].Value = pbold;

                objSQlComm.Parameters.Add(new SqlParameter("@c_italic", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@c_italic"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_italic"].Value = pitalic;

                objSQlComm.Parameters.Add(new SqlParameter("@c_positemfontcolor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@c_positemfontcolor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_positemfontcolor"].Value = ppositemfontclr;

                objSQlComm.Parameters.Add(new SqlParameter("@c_maxQuantity", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@c_maxQuantity"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@c_maxQuantity"].Value = pmaxitemsforpos;


                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                return strErrMsg;
            }
        }

        public string InsertBrandInWebOffice(int prefclient, string pcode, string pdesc, int puser)
        {
            string strSQLComm = "sp_co_imp_brand";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;

                //objSQLTran = sqlConn.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@b_code", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@b_code"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@b_code"].Value = pcode;

                objSQlComm.Parameters.Add(new SqlParameter("@b_name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@b_name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@b_name"].Value = pdesc;


                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                return strErrMsg;
            }
        }

        public string InsertVendorInWebOffice(int prefclient, string pcode, string paccno, string pname, string pcontact, string padd1, string padd2, string pcity,
                                                string pstate, string pcountry, string pzip, string pfax, string pph, string pmail, string pnote, double minamt, int puser)
        {
            string strSQLComm = "sp_co_imp_vendor";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                //objSQLTran = sqlConn.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@v_code", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_code"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_code"].Value = pcode;

                objSQlComm.Parameters.Add(new SqlParameter("@v_name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_name"].Value = pname;

                objSQlComm.Parameters.Add(new SqlParameter("@v_contact", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_contact"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_contact"].Value = pcontact;

                objSQlComm.Parameters.Add(new SqlParameter("@v_ac", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_ac"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_ac"].Value = paccno;

                objSQlComm.Parameters.Add(new SqlParameter("@v_add1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_add1"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_add1"].Value = padd1;

                objSQlComm.Parameters.Add(new SqlParameter("@v_add2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_add2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_add2"].Value = padd2;

                objSQlComm.Parameters.Add(new SqlParameter("@v_city", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_city"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_city"].Value = pcity;

                objSQlComm.Parameters.Add(new SqlParameter("@v_state", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_state"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_state"].Value = pstate;

                objSQlComm.Parameters.Add(new SqlParameter("@v_country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_country"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_country"].Value = pcountry;

                objSQlComm.Parameters.Add(new SqlParameter("@v_zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_zip"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_zip"].Value = pzip;

                objSQlComm.Parameters.Add(new SqlParameter("@v_phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_phone"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_phone"].Value = pph;

                objSQlComm.Parameters.Add(new SqlParameter("@v_fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_fax"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_fax"].Value = pfax;

                objSQlComm.Parameters.Add(new SqlParameter("@v_email", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_email"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_email"].Value = pmail;

                objSQlComm.Parameters.Add(new SqlParameter("@v_notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@v_notes"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_notes"].Value = pnote;

                objSQlComm.Parameters.Add(new SqlParameter("@v_minorder", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@v_minorder"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@v_minorder"].Value = minamt;


                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                return strErrMsg;
            }
        }

        public string InsertZipInWebOffice(int prefclient, string pzip, string pcity, string pstate, int puser)
        {
            string strSQLComm = " insert into ZipCode( zip,city,state,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,ClientID) "
                              + " values( @zip,@city,@state,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ClientId) ";


            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@city", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@state", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@zip"].Value = pzip;
                objSQlComm.Parameters["@city"].Value = pcity;
                objSQlComm.Parameters["@state"].Value = pstate;
                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                //objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                //objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public int GetTaxCountInWebOffice()
        {
            int val = 0;
            string strSQLComm = " select count(*) as cnt from TaxHeader where active = 'Yes' ";

            SqlCommand objSQlComm = null;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["cnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public string InsertTaxInWebOffice(int prefclient, string ptxname, int ptxtype, double ptxrate, string pactive, int puser, ref int refH)
        {
            string strSQLComm = " insert into TaxHeader( ClientID, TaxName,TaxRate,TaxType,Active,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values( @ClientId,@TaxName,@TaxRate,@TaxType,@Active,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            //SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Active", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ClientId"].Value = prefclient;
                objSQlComm.Parameters["@TaxName"].Value = ptxname;
                objSQlComm.Parameters["@Active"].Value = pactive;
                objSQlComm.Parameters["@TaxRate"].Value = ptxrate;
                objSQlComm.Parameters["@TaxType"].Value = ptxtype;
                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    refH = Functions.fnInt32(objsqlReader["ID"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                //objSQLTran.Commit();
                //objSQLTran.Dispose();
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
                //objSQLTran.Rollback();
                //objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string InsertTaxBreakPointInWebOffice(int prefclient, int refH, double pbreakp, double ptx, int puser)
        {
            string strSQLComm = " insert into TaxDetail( ClientID, RefID,Breakpoints,Tax,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                              + " values( @ClientId,@RefID,@Breakpoints,@Tax,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) ";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;
                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Breakpoints", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ClientId"].Value = prefclient;
                objSQlComm.Parameters["@RefID"].Value = refH;
                objSQlComm.Parameters["@Breakpoints"].Value = pbreakp;
                objSQlComm.Parameters["@Tax"].Value = ptx;

                objSQlComm.Parameters["@CreatedBy"].Value = puser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = puser;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                //objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                //objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }

        public string InsertProductInWebOffice(int prefclient, string prefStore)
        {

            string strSQLComm = "sp_co_imp_product";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand();

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                
                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StoreCode"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@StoreCode"].Value = prefStore;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU"].Value = strpSKU;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU2"].Value = strpSKU2;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU3"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU3"].Value = strpSKU3;

                objSQlComm.Parameters.Add(new SqlParameter("@PDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PDesc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PDesc"].Value = strpDescription;

                objSQlComm.Parameters.Add(new SqlParameter("@PBinL", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PBinL"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PBinL"].Value = strpBinLocation;

                objSQlComm.Parameters.Add(new SqlParameter("@PNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PNotes"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PNotes"].Value = strpProductNotes;

                objSQlComm.Parameters.Add(new SqlParameter("@PScrnColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PScrnColor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PScrnColor"].Value = strpPOSScreenColor;

                objSQlComm.Parameters.Add(new SqlParameter("@PScrnStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PScrnStyle"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PScrnStyle"].Value = pPOSScreenStyle;

                objSQlComm.Parameters.Add(new SqlParameter("@PBkGrnd", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PBkGrnd"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PBkGrnd"].Value = strpPOSBackground;

                objSQlComm.Parameters.Add(new SqlParameter("@PFont", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PFont"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PFont"].Value = strpPOSFontType;

                objSQlComm.Parameters.Add(new SqlParameter("@PFontS", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PFontS"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PFontS"].Value = intpPOSFontSize;

                objSQlComm.Parameters.Add(new SqlParameter("@PFontC", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@PFontC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PFontC"].Value = strpPOSFontColor;

                objSQlComm.Parameters.Add(new SqlParameter("@PBold", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PBold"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PBold"].Value = strpIsBold;

                objSQlComm.Parameters.Add(new SqlParameter("@PItalics", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PItalics"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PItalics"].Value = strpIsItalics;

                objSQlComm.Parameters.Add(new SqlParameter("@PCUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PCUPC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PCUPC"].Value = strpCaseUPC;

                objSQlComm.Parameters.Add(new SqlParameter("@PUPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PUPC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PUPC"].Value = strpUPC;

                objSQlComm.Parameters.Add(new SqlParameter("@PSeason", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PSeason"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PSeason"].Value = strpSeason;

                objSQlComm.Parameters.Add(new SqlParameter("@PType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PType"].Value = strpProductType;

                objSQlComm.Parameters.Add(new SqlParameter("@PPrompt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PPrompt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPrompt"].Value = strpPromptForPrice;

                objSQlComm.Parameters.Add(new SqlParameter("@PPrintBrCd", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PPrintBrCd"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPrintBrCd"].Value = strpPrintBarCode;

                objSQlComm.Parameters.Add(new SqlParameter("@PNoPriceLbl", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PNoPriceLbl"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PNoPriceLbl"].Value = strpNoPriceOnLabel;

                objSQlComm.Parameters.Add(new SqlParameter("@PFS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PFS"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PFS"].Value = strpFoodStampEligible;

                objSQlComm.Parameters.Add(new SqlParameter("@PAddPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PAddPOS"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PAddPOS"].Value = strpAddtoPOSScreen;

                objSQlComm.Parameters.Add(new SqlParameter("@PAddPOSCat", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PAddPOSCat"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PAddPOSCat"].Value = strpAddToPOSCategoryScreen;

                objSQlComm.Parameters.Add(new SqlParameter("@PScaleBrCd", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PScaleBrCd"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PScaleBrCd"].Value = strpScaleBarCode;

                objSQlComm.Parameters.Add(new SqlParameter("@PAllowZeroStk", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PAllowZeroStk"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PAllowZeroStk"].Value = strpAllowZeroStock;

                objSQlComm.Parameters.Add(new SqlParameter("@PDispStk", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PDispStk"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PDispStk"].Value = strpDisplayStockinPOS;

                objSQlComm.Parameters.Add(new SqlParameter("@PActive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PActive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PActive"].Value = strpProductStatus;

                objSQlComm.Parameters.Add(new SqlParameter("@PRental", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PRental"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRental"].Value = strpRentalPrompt;

                objSQlComm.Parameters.Add(new SqlParameter("@PReprCrg", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PReprCrg"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PReprCrg"].Value = strpRepairPromptForCharge;

                objSQlComm.Parameters.Add(new SqlParameter("@PReprTag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PReprTag"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PReprTag"].Value = strpRepairPromptForTag;

                objSQlComm.Parameters.Add(new SqlParameter("@PBrkFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PBrkFlag"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PBrkFlag"].Value = "N";

                objSQlComm.Parameters.Add(new SqlParameter("@PPriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PPriceA"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPriceA"].Value = dblpPriceA;

                objSQlComm.Parameters.Add(new SqlParameter("@PPriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PPriceB"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPriceB"].Value = dblpPriceB;

                objSQlComm.Parameters.Add(new SqlParameter("@PPriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PPriceC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPriceC"].Value = dblpPriceC;

                objSQlComm.Parameters.Add(new SqlParameter("@PLastCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PLastCost"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PLastCost"].Value = dblpLastCost;

                objSQlComm.Parameters.Add(new SqlParameter("@PCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PCost"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PCost"].Value = dblpCost;

                objSQlComm.Parameters.Add(new SqlParameter("@PDCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PDCost"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PDCost"].Value = dblpDiscountedCost;

                objSQlComm.Parameters.Add(new SqlParameter("@POnHandQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@POnHandQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@POnHandQty"].Value = dblpQtyOnHand;

                objSQlComm.Parameters.Add(new SqlParameter("@PLayawayQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PLayawayQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PLayawayQty"].Value = dblpQtyOnLayaway;

                objSQlComm.Parameters.Add(new SqlParameter("@PReorderQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PReorderQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PReorderQty"].Value = dblpReorderQty;

                objSQlComm.Parameters.Add(new SqlParameter("@PNormalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PNormalQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PNormalQty"].Value = dblpNormalQty;

                objSQlComm.Parameters.Add(new SqlParameter("@PBrkRatio", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PBrkRatio"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PBrkRatio"].Value = dblpBreakPackRatio;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerMinute", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerMinute"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerMinute"].Value = dblpRentalPerMinute;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerHour"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerHour"].Value = dblpRentalPerHour;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerDay"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerDay"].Value = dblpRentalPerDay;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerWeek", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerWeek"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerWeek"].Value = dblpRentalPerWeek;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerMonth", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerMonth"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerMonth"].Value = pRentalPerMonth;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalDeposit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalDeposit"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalDeposit"].Value = dblpRentalDeposit;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalMinHour", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalMinHour"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalMinHour"].Value = dblpRentalMinHour;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalMinAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalMinAmount"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalMinAmount"].Value = dblpRentalMinAmount;

                objSQlComm.Parameters.Add(new SqlParameter("@PRepairCharge", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRepairCharge"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRepairCharge"].Value = dblpRepairCharge;

                objSQlComm.Parameters.Add(new SqlParameter("@PPrntQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PPrntQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPrntQty"].Value = intpQtyToPrint;

                objSQlComm.Parameters.Add(new SqlParameter("@PLbl", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PLbl"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PLbl"].Value = intpLabelType;

                objSQlComm.Parameters.Add(new SqlParameter("@PPoints", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PPoints"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PPoints"].Value = intpPoints;

                objSQlComm.Parameters.Add(new SqlParameter("@PMinAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PMinAge"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PMinAge"].Value = intpMinimumAge;

                objSQlComm.Parameters.Add(new SqlParameter("@PDecimal", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PDecimal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PDecimal"].Value = intpDecimalPlace;

                objSQlComm.Parameters.Add(new SqlParameter("@PCaseQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PCaseQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PCaseQty"].Value = intpCaseQty;

                objSQlComm.Parameters.Add(new SqlParameter("@PLinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PLinkSKU"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PLinkSKU"].Value = intpLinkSKU;

                objSQlComm.Parameters.Add(new SqlParameter("@PMinSrv", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PMinSrv"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PMinSrv"].Value = intpMinimumServiceTime;

                objSQlComm.Parameters.Add(new SqlParameter("@DeptID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DeptID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@DeptID"].Value = strpDepartmentCode;

                objSQlComm.Parameters.Add(new SqlParameter("@CatID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CatID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CatID"].Value = strpCategoryCode;


                objSQlComm.Parameters.Add(new SqlParameter("@Tx1nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Tx1nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx1nm"].Value = strpTax1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Tx1rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tx1rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx1rt"].Value = dblpTax1Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@Tx2nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Tx2nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx2nm"].Value = strpTax2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Tx2rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tx2rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx2rt"].Value = dblpTax2Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@Tx3nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Tx3nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx3nm"].Value = strpTax3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@Tx3rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tx3rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tx3rt"].Value = dblpTax3Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@BrndID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@BrndID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@BrndID"].Value = strpBrandCode;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx1nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@rntTx1nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx1nm"].Value = strpRntTax1Name;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx1rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@rntTx1rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx1rt"].Value = dblpRntTax1Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx2nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@rntTx2nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx2nm"].Value = strpRntTax2Name;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx2rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@rntTx2rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx2rt"].Value = dblpRntTax2Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx3nm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@rntTx3nm"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx3nm"].Value = strpRntTax3Name;

                objSQlComm.Parameters.Add(new SqlParameter("@rntTx3rt", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@rntTx3rt"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@rntTx3rt"].Value = dblpRntTax3Rate;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tare"].Value = pdblTare;

                objSQlComm.Parameters.Add(new SqlParameter("@PRentalPerHalfDay", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@PRentalPerHalfDay"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRentalPerHalfDay"].Value = dblpRentalPerHalfDay;

                objSQlComm.Parameters.Add(new SqlParameter("@PAddScale", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PAddScale"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PAddScale"].Value = strpAddtoScaleScreen;

                objSQlComm.Parameters.Add(new SqlParameter("@PNonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PNonDiscountable"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PNonDiscountable"].Value = strpNonDiscountable;

                objSQlComm.Parameters.Add(new SqlParameter("@SScrnColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SScrnColor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SScrnColor"].Value = strpScaleScreenColor;

                objSQlComm.Parameters.Add(new SqlParameter("@SScrnStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SScrnStyle"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SScrnStyle"].Value = pScaleScreenStyle;

                objSQlComm.Parameters.Add(new SqlParameter("@SBkGrnd", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SBkGrnd"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SBkGrnd"].Value = strpScaleBackground;

                objSQlComm.Parameters.Add(new SqlParameter("@SFont", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SFont"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SFont"].Value = strpScaleFontType;

                objSQlComm.Parameters.Add(new SqlParameter("@SFontS", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SFontS"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SFontS"].Value = intpScaleFontSize;

                objSQlComm.Parameters.Add(new SqlParameter("@SFontC", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SFontC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SFontC"].Value = strpScaleFontColor;

                objSQlComm.Parameters.Add(new SqlParameter("@SBold", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@SBold"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SBold"].Value = strpScaleIsBold;

                objSQlComm.Parameters.Add(new SqlParameter("@SItalics", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@SItalics"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SItalics"].Value = strpScaleIsItalics;

                objSQlComm.Parameters.Add(new SqlParameter("@Tare2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Tare2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Tare2"].Value = pdblTare2;

                objSQlComm.Parameters.Add(new SqlParameter("@PNotes2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PNotes2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PNotes2"].Value = strpPNotes2;

                objSQlComm.Parameters.Add(new SqlParameter("@SplitWeight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@SplitWeight"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SplitWeight"].Value = dblSplitWeight;

                objSQlComm.Parameters.Add(new SqlParameter("@UOM", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@UOM"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@UOM"].Value = strUOM;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                intpPID = Functions.fnInt32(objSQlComm.Parameters["@ID"].Value);
                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                MessageBox.Show(strErrMsg);
                return strErrMsg;
            }
        }

        public string InsertPrimaryVendorInWebOffice(int prefclient, int pPID, string pVCd, string pVPart, double pVPrice, string pVPrimary, int pPackQty, double pShrink)
        {

            string strSQLComm = "sp_co_imp_vendpart";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand();
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                //objSQlComm.CommandTimeout = 90;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PID"].Value = pPID;

                objSQlComm.Parameters.Add(new SqlParameter("@VCd", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@VCd"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VCd"].Value = pVCd;

                objSQlComm.Parameters.Add(new SqlParameter("@VPart", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@VPart"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VPart"].Value = pVPart;

                objSQlComm.Parameters.Add(new SqlParameter("@VPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@VPrice"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VPrice"].Value = pVPrice;

                objSQlComm.Parameters.Add(new SqlParameter("@VPrimary", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@VPrimary"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VPrimary"].Value = pVPrimary;

                objSQlComm.Parameters.Add(new SqlParameter("@VPackQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VPackQty"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VPackQty"].Value = pPackQty;

                objSQlComm.Parameters.Add(new SqlParameter("@VShrink", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@VShrink"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VShrink"].Value = pShrink;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                MessageBox.Show(strErrMsg);
                return strErrMsg;
            }
        }

        public string InsertProductMatrixOptionsInWebOffice(int prefclient, int pPID, string pOP1, string pOP2, string pOP3, ref int pOID)
        {
            string strSQLComm = "sp_co_imp_product_mtrx_op";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                //objSQlComm.CommandTimeout = 90;
                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PID"].Value = pPID;

                objSQlComm.Parameters.Add(new SqlParameter("@Option1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Option1Name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option1Name"].Value = pOP1;

                objSQlComm.Parameters.Add(new SqlParameter("@Option2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Option2Name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option2Name"].Value = pOP2;

                objSQlComm.Parameters.Add(new SqlParameter("@Option3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Option3Name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Option3Name"].Value = pOP3;

                objSQlComm.Parameters.Add(new SqlParameter("@OID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@OID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                pOID = Convert.ToInt32(objSQlComm.Parameters["@OID"].Value);
                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                MessageBox.Show(strErrMsg);
                return strErrMsg;
            }
        }

        public string InsertProductMatrixValuesInWebOffice(int prefclient, int pOID, int pValueID, string pOP, string pOPD)
        {
            string strSQLComm = "sp_co_imp_product_mtrx_val";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                //objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                //objSQlComm.CommandTimeout = 90;
                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@OID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@OID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OID"].Value = pOID;

                objSQlComm.Parameters.Add(new SqlParameter("@ValueID", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters["@ValueID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ValueID"].Value = pValueID;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@OptionValue"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OptionValue"].Value = pOP;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@OptionDefault"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OptionDefault"].Value = pOPD;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                MessageBox.Show(strErrMsg);
                return strErrMsg;
            }
        }

        public string InsertProductMatrixInWebOffice(int prefclient, int pOID, string pOP1, string pOP2, string pOP3, double pQTY)
        {
            string strSQLComm = "sp_co_imp_product_mtrx";

            SqlCommand objSQlComm = null;

            //SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                //objSQlComm.CommandTimeout = 90;
                objSQlComm = new SqlCommand();
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Connection = sqlConn;
                //objSQLTran = Connection.BeginTransaction();
                //objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@OID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@OID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OID"].Value = pOID;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@OptionValue1"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OptionValue1"].Value = pOP1;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@OptionValue2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OptionValue2"].Value = pOP2;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@OptionValue3"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@OptionValue3"].Value = pOP3;

                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@QtyOnHand"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@QtyOnHand"].Value = pQTY;

                objSQlComm.ExecuteNonQuery();

                //objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = SQLDBException.Message;
                //objSQLTran.Rollback();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                //objSQLTran.Dispose();
                MessageBox.Show(strErrMsg);
                return strErrMsg;
            }
        }


        public DataTable FetchScaleCategories()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.Cat_ID,s.Name,s.Department_ID, isnull(d.DepartmentID,'') as Dept,s.POSBackground,s.POSScreenStyle,"
                              + " s.POSScreenColor, s.POSFontType, s.POSFontSize, s.POSFontColor, s.IsBold, s.IsItalics, s.POSItemFontColor from Scale_Category s "
                              + " left outer join Dept d on d.ID = s.Department_ID order by s.Name ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dept_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dept", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSItemFontColor", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Cat_ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Dept"].ToString(),
                                                   objSQLReader["Dept"].ToString(),
                                                   objSQLReader["POSBackground"].ToString(),
                                                   objSQLReader["POSScreenStyle"].ToString(),
                                                   objSQLReader["POSScreenColor"].ToString(),
                                                   objSQLReader["POSFontType"].ToString(),
                                                   objSQLReader["POSFontSize"].ToString(),
                                                   objSQLReader["POSFontColor"].ToString(),
                                                   objSQLReader["IsBold"].ToString(),
                                                   objSQLReader["IsItalics"].ToString(),
                                                   objSQLReader["POSItemFontColor"].ToString() });
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

        public string InsertScaleCategoryInWebOffice(int prefclient, string pCat_ID, string pScaleCat_Name, string pScaleCat_Ref_Department_ID,
            string pScaleCat_POSBackground, string pScaleCat_POSScreenStyle,
            string pScaleCat_POSScreenColor, string pScaleCat_POSFontType,
            int pScaleCat_POSFontSize, string pScaleCat_POSFontColor,
            string pScaleCat_IsBold, string pScaleCat_IsItalics, string pScaleCat_POSItemFontColor)
        {



            string strSQLComm = "sp_co_imp_scalecategory";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Cat_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Cat_ID"].Value = pCat_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ScaleCat_Name"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_Name"].Value = pScaleCat_Name;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_Ref_Department_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ScaleCat_Ref_Department_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_Ref_Department_ID"].Value = pScaleCat_Ref_Department_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSBackground"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSBackground"].Value = pScaleCat_POSBackground;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSScreenStyle"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSScreenStyle"].Value = pScaleCat_POSScreenStyle;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSScreenColor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSScreenColor"].Value = pScaleCat_POSScreenColor;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSFontType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSFontType"].Value = pScaleCat_POSFontType;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ScaleCat_POSFontSize"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSFontSize"].Value = pScaleCat_POSFontSize;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSFontColor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSFontColor"].Value = pScaleCat_POSFontColor;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_IsBold", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ScaleCat_IsBold"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_IsBold"].Value = pScaleCat_IsBold;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_IsItalics", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ScaleCat_IsItalics"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_IsItalics"].Value = pScaleCat_IsItalics;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleCat_POSItemFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleCat_POSItemFontColor"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleCat_POSItemFontColor"].Value = pScaleCat_POSItemFontColor;


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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleAddresses()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.SCALE_IP,s.SCALE_LOCATION,s.SCALE_TYPE,s.PORT,isnull(t.Scale_Type,'') as ManuF, "
                              + " s.FILE_LOCATION,s.SCALE_ID_NO,s.SCALE_NOTES,s.Mil_Sec,"
                              + " s.DEPARTMENT_ID, isnull(d.DepartmentID,'') as Dept from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE "
                              + " left outer join dept d on d.ID = s.DEPARTMENT_ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_LOCATION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PORT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FILE_LOCATION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_ID_NO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_NOTES", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DEPARTMENT_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Mil_Sec", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["SCALE_LOCATION"].ToString(),
												   objSQLReader["SCALE_TYPE"].ToString(),
                                                   objSQLReader["PORT"].ToString(),
												   objSQLReader["FILE_LOCATION"].ToString(),
                                                   objSQLReader["SCALE_ID_NO"].ToString(),
												   objSQLReader["SCALE_NOTES"].ToString(),
                                                   objSQLReader["Dept"].ToString(),
                                                   objSQLReader["Mil_Sec"].ToString() });
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

        public string InsertScaleAddressInWebOffice(int prefclient, string pSCALE_IP, string pSCALE_LOCATION, int pSCALE_TYPE_A, string pPORT,
            string pFILE_LOCATION, int pSCALE_ID_NO, string pSCALE_NOTES,
            int pMil_Sec, string pRef_Department_ID)
        {

            string strSQLComm = "sp_co_imp_scaleaddress";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_IP"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_IP"].Value = pSCALE_IP;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_LOCATION"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_LOCATION"].Value = pSCALE_LOCATION;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE_A", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SCALE_TYPE_A"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_TYPE_A"].Value = pSCALE_TYPE_A;

                objSQlComm.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PORT"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PORT"].Value = pPORT;

                objSQlComm.Parameters.Add(new SqlParameter("@FILE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FILE_LOCATION"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FILE_LOCATION"].Value = pFILE_LOCATION;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_ID_NO", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SCALE_ID_NO"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_ID_NO"].Value = pSCALE_ID_NO;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_NOTES", System.Data.SqlDbType.Text));
                objSQlComm.Parameters["@SCALE_NOTES"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_NOTES"].Value = pSCALE_NOTES;

                objSQlComm.Parameters.Add(new SqlParameter("@Mil_Sec", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Mil_Sec"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Mil_Sec"].Value = pMil_Sec;

                objSQlComm.Parameters.Add(new SqlParameter("@Ref_Department_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Ref_Department_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Ref_Department_ID"].Value = pRef_Department_ID;


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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleLabelFormatRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from labelformats ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsDefaultFormat", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelFormat", typeof(byte[]));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["FormatName"].ToString(),
                                                   objSQLReader["IsDefaultFormat"].ToString(),
                                                   objSQLReader["FormatType"].ToString(),
                                                   (byte[])objSQLReader["LabelFormat"]
                                                });
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

        public string InsertScaleLabelFormatInWebOffice(int prefclient, string pLabelFormatName, string pIsDefaultFormat, string pFormatType, byte[] pLabelFormat_RawData)
        {
            string strSQLComm = "sp_co_imp_LabelFormat";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormatName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@LabelFormatName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@LabelFormatName"].Value = pLabelFormatName;

                objSQlComm.Parameters.Add(new SqlParameter("@IsDefaultFormat", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@IsDefaultFormat"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@IsDefaultFormat"].Value = pIsDefaultFormat;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@FormatType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FormatType"].Value = pFormatType;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat_RawData", System.Data.SqlDbType.VarBinary, pLabelFormat_RawData.Length));
                objSQlComm.Parameters["@LabelFormat_RawData"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@LabelFormat_RawData"].Value = pLabelFormat_RawData;



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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleGraphicArtRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from GraphicArts ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);


            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ArtNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArt", typeof(byte[]));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["ArtNo"].ToString(),
                                                   (byte[])objSQLReader["GraphicArt"]
                                                });
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

        public string InsertGraphicArtInWebOffice(int prefclient, int pGraphic_ArtNo, byte[] pGraphic_GraphicArt_RawData)
        {
            string strSQLComm = "sp_co_imp_GraphicArt";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_ArtNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Graphic_ArtNo"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_ArtNo"].Value = pGraphic_ArtNo;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_GraphicArt_RawData", System.Data.SqlDbType.Image));
                objSQlComm.Parameters["@Graphic_GraphicArt_RawData"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_GraphicArt_RawData"].Value = pGraphic_GraphicArt_RawData;



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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleSecondLabelTypeRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.*, isnull(i.SCALE_IP,'') as Scale_Ip, isnull(l.FormatName,'') as FormatNm, isnull(l.FormatType,'') as FormatTy "
                              + " from Second_Label_Types s left outer join Scale_Addresses i on i.ID = s.Scale_Type left outer join LabelFormats l on l.FormatID = s.LabelFormat ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelFormat", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Ip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelFormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelFormatType", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["Label_ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                   objSQLReader["LabelFormat"].ToString(),
                                                   objSQLReader["Scale_Ip"].ToString(),
                                                   objSQLReader["FormatNm"].ToString(),
                                                   objSQLReader["FormatTy"].ToString()
                                                });
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

        public string InsertSecondScaleTypeInWebOffice(int prefclient, int pLabel_ID, string pDescription, int pScale_Type, int pLabelFormat, string pSCALE_IP,
            string pLabelFormatName, string pFormatType)
        {



            string strSQLComm = "sp_co_imp_secondlabeltype";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@Label_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Label_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Label_ID"].Value = pLabel_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Description"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Description"].Value = pDescription;

                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Scale_Type"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Scale_Type"].Value = pScale_Type;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LabelFormat"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@LabelFormat"].Value = pLabelFormat;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_IP"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_IP"].Value = pSCALE_IP;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormatName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@LabelFormatName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@LabelFormatName"].Value = pLabelFormatName;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@FormatType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FormatType"].Value = pFormatType;



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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleLabelTypeRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select l.*, isnull(i.SCALE_IP,'') as Scale_Ip "
                              + " from Label_Types l left outer join Scale_Addresses i on i.ID = l.Scale_Type ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_Id", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dp_Id", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Ip", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["Label_Id"].ToString(),
                                                    objSQLReader["Dp_Id"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                   objSQLReader["Scale_Ip"].ToString()
                                                });
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

        public string InsertScaleTypeInWebOffice(int prefclient, int pLabel_Id, int pDp_ID, string pDescription, int pScale_Type, string pSCALE_IP)
        {
            string strSQLComm = "sp_co_imp_labeltype";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@Label_Id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Label_Id"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Label_Id"].Value = pLabel_Id;

                objSQlComm.Parameters.Add(new SqlParameter("@Dp_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Dp_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Dp_ID"].Value = pDp_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Description"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Description"].Value = pDescription;

                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Scale_Type"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Scale_Type"].Value = pScale_Type;



                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_IP"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_IP"].Value = pSCALE_IP;



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
                return strErrMsg;
            }
        }

        public DataTable FetchScaleGraphicsRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select g.*, isnull(i.SCALE_IP,'') as Scale_Ip, isnull(a.ArtNo,'0') as ArtNo "
                              + " from Scale_Graphics g left outer join Scale_Addresses i on i.ID = g.Scale_Type left outer join GraphicArts a on a.ID = g.GraphicArtID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Graphic_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Ip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArtNo", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["Graphic_ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                   objSQLReader["Scale_Ip"].ToString(),
                                                   objSQLReader["ArtNo"].ToString()
                                                });
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

        public string InsertScaleGraphicsInWebOffice(int prefclient, int pGraphic_Graphic_ID, string pGraphic_Description, int pGraphic_Scale_Type, int pGraphic_GraphicArtNo, string pGraphic_SCALE_IP)
        {
            string strSQLComm = "sp_co_imp_ScaleGraphics";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_Graphic_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Graphic_Graphic_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_Graphic_ID"].Value = pGraphic_Graphic_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Graphic_Description"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_Description"].Value = pGraphic_Description;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Graphic_Scale_Type"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_Scale_Type"].Value = pGraphic_Scale_Type;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_GraphicArtNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Graphic_GraphicArtNo"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_GraphicArtNo"].Value = pGraphic_GraphicArtNo;


                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Graphic_SCALE_IP"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Graphic_SCALE_IP"].Value = pGraphic_SCALE_IP;

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
                return strErrMsg;
            }
        }




        public DataTable GetScaleProduct(int ProdId)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Product where productid = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("row_id", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_DESCRIPTION_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_DESCRIPTION_2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ITEM_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BYCOUNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WEIGHT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHELF_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCT_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("INGRED_STATEMENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPECIAL_MESSAGE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GRAPHIC_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SEC_LABEL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FORCE_TARE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE_1_S", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE_2_O", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TempNum", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Calors", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FatCalors", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotFat", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FatPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SatFat", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SatFatPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Chol", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CholPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sodium", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SodPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Carbs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CarbPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fiber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FiberPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sugar", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Proteins", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CalcPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IronPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VitDPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VitEPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VitCPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VitAPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransFattyAcid", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EXT1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EXT2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PL_JulianDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL_JulianDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SugarAlcoh", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SugarAlcohPerc", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["row_id"].ToString(), 
                                                   objSQLReader["PLU_NUMBER"].ToString(), 
                                                   objSQLReader["SCALE_DESCRIPTION_1"].ToString(),
												   objSQLReader["SCALE_DESCRIPTION_2"].ToString(),
												   objSQLReader["ITEM_TYPE"].ToString(),
                                                   objSQLReader["BYCOUNT"].ToString(),
												   objSQLReader["WEIGHT"].ToString(),
                                                   objSQLReader["SHELF_LIFE"].ToString(),
												   objSQLReader["PRODUCT_LIFE"].ToString(),
                                                   objSQLReader["LABEL_1"].ToString(),
                                                   objSQLReader["INGRED_STATEMENT"].ToString(), 
                                                   objSQLReader["SPECIAL_MESSAGE"].ToString(),
												   objSQLReader["GRAPHIC_ID"].ToString(),
												   objSQLReader["SEC_LABEL"].ToString(),
                                                   objSQLReader["FORCE_TARE"].ToString(),
												   objSQLReader["TARE_1_S"].ToString(),
                                                   objSQLReader["TARE_2_O"].ToString(),
												   objSQLReader["TempNum"].ToString(),
                                                   objSQLReader["ServSize"].ToString(),
                                                   objSQLReader["ServPC"].ToString(), 
                                                   objSQLReader["Calors"].ToString(),
												   objSQLReader["FatCalors"].ToString(),
												   objSQLReader["TotFat"].ToString(),
                                                   objSQLReader["FatPerc"].ToString(),
												   objSQLReader["SatFat"].ToString(),
                                                   objSQLReader["SatFatPerc"].ToString(),
												   objSQLReader["Chol"].ToString(),
                                                   objSQLReader["CholPerc"].ToString(),
                                                   objSQLReader["Sodium"].ToString(), 
                                                   objSQLReader["SodPerc"].ToString(),
												   objSQLReader["Carbs"].ToString(),
												   objSQLReader["CarbPerc"].ToString(),
                                                   objSQLReader["Fiber"].ToString(),
												   objSQLReader["FiberPerc"].ToString(),
                                                   objSQLReader["Sugar"].ToString(),
												   objSQLReader["Proteins"].ToString(),
                                                   objSQLReader["CalcPerc"].ToString(),
												   objSQLReader["IronPerc"].ToString(),
                                                   objSQLReader["VitDPerc"].ToString(),
												   objSQLReader["VitEPerc"].ToString(),
                                                   objSQLReader["VitCPerc"].ToString(),
												   objSQLReader["VitAPerc"].ToString(), 
                                                   objSQLReader["TransFattyAcid"].ToString(),
                                                   objSQLReader["Cat_ID"].ToString(),
                                                   objSQLReader["EXT1"].ToString(),
                                                   objSQLReader["EXT2"].ToString(),
                                                   objSQLReader["PL_JulianDate"].ToString(),
                                                   objSQLReader["SL_JulianDate"].ToString(),
                                                   objSQLReader["ScaleDisplayOrder"].ToString(),
                                                   objSQLReader["SugarAlcoh"].ToString(),
                                                   objSQLReader["SugarAlcohPerc"].ToString() });
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
                MessageBox.Show(strErrMsg);
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return null;
            }
        }


        public DataTable GetScaleMappingRecord(int ProdId)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Mapping where productid = @ID order by ScaleMappingType ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ProdId;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Value", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ScaleMappingType"].ToString(), 
                                                   objSQLReader["MappingID"].ToString(),
                                                   objSQLReader["ID"].ToString()
                                                });
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


        public string GetScaleLabelLinkIp(int RefLink)
        {
            string val = "";

            string strSQLComm = " select isnull(Scale_Ip,'') as IP from Scale_Addresses where ID in (Select Scale_Type from Label_Types where ID = @ID) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = RefLink;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["IP"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "";
            }
        }







       
        public string InsertScaleDetailsInWebOffice(int prefclient, int sdProductID, string sdPLU_NUMBER, string sdSCALE_DESCRIPTION_1, string sdSCALE_DESCRIPTION_2, string sdITEM_TYPE,
                           int sdBYCOUNT, double sdWEIGHT, int sdSHELF_LIFE, int sdPRODUCT_LIFE, string sdINGRED_STATEMENT, string sdSPECIAL_MESSAGE,
                           string sdFORCE_TARE, double sdTARE_1_S, double sdTARE_2_O,
                           int sdTempNum, string sdServSize, string sdServPC, string sdCalors,
                           string sdFatCalors, string sdTotFat, int sdFatPerc, string sdSatFat, int sdSatFatPerc, string sdChol, int sdCholPerc, string sdSodium,
                           int sdSodPerc, string sdCarbs, int sdCarbPerc, string sdFiber, int sdFiberPerc, string sdSugar, string sdProteins, int sdCalcPerc,
                           int sdIronPerc, int sdVitDPerc, int sdVitEPerc, int sdVitCPerc, int sdVitAPerc, string sdTransFattyAcid, string sdCat_ID,
                           string sdEXT1, string sdEXT2, string sdPL_JulianDate, string sdSL_JulianDate, string sdSugarAlcoh, int sdSugarAlcohPerc, ref int sdRef_ID
                           )
        {
            string strSQLComm = "sp_co_imp_scaledetails";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                //objSQlComm.CommandTimeout = 90;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ProductID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ProductID"].Value = sdProductID;

                objSQlComm.Parameters.Add(new SqlParameter("@PLU_NUMBER", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PLU_NUMBER"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PLU_NUMBER"].Value = sdPLU_NUMBER;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Value = sdSCALE_DESCRIPTION_1;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Value = sdSCALE_DESCRIPTION_2;

                objSQlComm.Parameters.Add(new SqlParameter("@ITEM_TYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ITEM_TYPE"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ITEM_TYPE"].Value = sdITEM_TYPE;

                objSQlComm.Parameters.Add(new SqlParameter("@BYCOUNT", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BYCOUNT"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@BYCOUNT"].Value = sdBYCOUNT;

                objSQlComm.Parameters.Add(new SqlParameter("@WEIGHT", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@WEIGHT"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@WEIGHT"].Value = sdWEIGHT;

                objSQlComm.Parameters.Add(new SqlParameter("@SHELF_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SHELF_LIFE"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SHELF_LIFE"].Value = sdSHELF_LIFE;

                objSQlComm.Parameters.Add(new SqlParameter("@PRODUCT_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PRODUCT_LIFE"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PRODUCT_LIFE"].Value = sdPRODUCT_LIFE;

                objSQlComm.Parameters.Add(new SqlParameter("@INGRED_STATEMENT", System.Data.SqlDbType.NText));
                objSQlComm.Parameters["@INGRED_STATEMENT"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@INGRED_STATEMENT"].Value = sdINGRED_STATEMENT;

                objSQlComm.Parameters.Add(new SqlParameter("@SPECIAL_MESSAGE", System.Data.SqlDbType.NText));
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Value = sdSPECIAL_MESSAGE;

                objSQlComm.Parameters.Add(new SqlParameter("@FORCE_TARE", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@FORCE_TARE"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FORCE_TARE"].Value = sdFORCE_TARE;

                objSQlComm.Parameters.Add(new SqlParameter("@TARE_1_S", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@TARE_1_S"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TARE_1_S"].Value = sdTARE_1_S;

                objSQlComm.Parameters.Add(new SqlParameter("@TARE_2_O", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@TARE_2_O"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TARE_2_O"].Value = sdTARE_2_O;

                objSQlComm.Parameters.Add(new SqlParameter("@TempNum", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@TempNum"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TempNum"].Value = sdTempNum;

                objSQlComm.Parameters.Add(new SqlParameter("@ServSize", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ServSize"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ServSize"].Value = sdServSize;

                objSQlComm.Parameters.Add(new SqlParameter("@ServPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ServPC"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ServPC"].Value = sdServPC;

                objSQlComm.Parameters.Add(new SqlParameter("@Calors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Calors"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Calors"].Value = sdCalors;

                objSQlComm.Parameters.Add(new SqlParameter("@FatCalors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FatCalors"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FatCalors"].Value = sdFatCalors;

                objSQlComm.Parameters.Add(new SqlParameter("@TotFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@TotFat"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TotFat"].Value = sdTotFat;

                objSQlComm.Parameters.Add(new SqlParameter("@FatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@FatPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FatPerc"].Value = sdFatPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@SatFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SatFat"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SatFat"].Value = sdSatFat;

                objSQlComm.Parameters.Add(new SqlParameter("@SatFatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SatFatPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SatFatPerc"].Value = sdSatFatPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@Chol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Chol"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Chol"].Value = sdChol;

                objSQlComm.Parameters.Add(new SqlParameter("@CholPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CholPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CholPerc"].Value = sdCholPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@Sodium", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Sodium"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Sodium"].Value = sdSodium;

                objSQlComm.Parameters.Add(new SqlParameter("@SodPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SodPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SodPerc"].Value = sdSodPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@Carbs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Carbs"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Carbs"].Value = sdCarbs;

                objSQlComm.Parameters.Add(new SqlParameter("@CarbPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CarbPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CarbPerc"].Value = sdCarbPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@Fiber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Fiber"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Fiber"].Value = sdFiber;

                objSQlComm.Parameters.Add(new SqlParameter("@FiberPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@FiberPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FiberPerc"].Value = sdFiberPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@Sugar", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Sugar"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Sugar"].Value = sdSugar;

                objSQlComm.Parameters.Add(new SqlParameter("@Proteins", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Proteins"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Proteins"].Value = sdProteins;


                objSQlComm.Parameters.Add(new SqlParameter("@CalcPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CalcPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CalcPerc"].Value = sdCalcPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@IronPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@IronPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@IronPerc"].Value = sdIronPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@VitDPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VitDPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VitDPerc"].Value = sdVitDPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@VitEPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VitEPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VitEPerc"].Value = sdVitEPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@VitCPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VitCPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VitCPerc"].Value = sdVitCPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@VitAPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VitAPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VitAPerc"].Value = sdVitAPerc;

                objSQlComm.Parameters.Add(new SqlParameter("@TransFattyAcid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@TransFattyAcid"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TransFattyAcid"].Value = sdTransFattyAcid;

                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Cat_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Cat_ID"].Value = sdCat_ID;

                objSQlComm.Parameters.Add(new SqlParameter("@EXT1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@EXT1"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@EXT1"].Value = sdEXT1;

                objSQlComm.Parameters.Add(new SqlParameter("@EXT2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@EXT2"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@EXT2"].Value = sdEXT2;

                objSQlComm.Parameters.Add(new SqlParameter("@PL_JulianDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@PL_JulianDate"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PL_JulianDate"].Value = sdPL_JulianDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SL_JulianDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@SL_JulianDate"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SL_JulianDate"].Value = sdSL_JulianDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcoh", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SugarAlcoh"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SugarAlcoh"].Value = sdSugarAlcoh;

                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcohPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SugarAlcohPerc"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SugarAlcohPerc"].Value = sdSugarAlcohPerc;


                objSQlComm.Parameters.Add(new SqlParameter("@Ref_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref_ID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                sdRef_ID = Convert.ToInt32(objSQlComm.Parameters["@Ref_ID"].Value);
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

                return strErrMsg;
            }
        }




        public string InsertScaleMappingInWebOffice(int prefclient, int pRefProductID, string pScaleMappingType, int pMappingID, string pIp)
        {
            string strSQLComm = "sp_co_imp_scalemapping";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ClientId", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ClientId"].Value = prefclient;

                objSQlComm.Parameters.Add(new SqlParameter("@RefProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefProductID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@RefProductID"].Value = pRefProductID;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleMappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ScaleMappingType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ScaleMappingType"].Value = pScaleMappingType;

                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MappingID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@MappingID"].Value = pMappingID;

                objSQlComm.Parameters.Add(new SqlParameter("@RefIP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@RefIP"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@RefIP"].Value = pIp;

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
                return strErrMsg;
            }
        }

        #endregion

        public DataTable FetchOtherStores(string ignorestore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select StoreCode,  StoreCode + ' - ' + StoreName as LookupName from WebStores where IsActive = 'Y' and StoreCode != '" + ignorestore + "' order by StoreCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Lookup", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["StoreCode"].ToString(), objSQLReader["LookupName"].ToString() });
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



        public string AddActionInWebOffice(string pcode, string pAction, int pClientId)
        {
            string strSQLComm = " insert into StoreActions (StoreCode,ActionOn,ActionDetails,ClientID ) "
                              + " values( @StoreCode,getdate(), @Action, @ClientID) ";


            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Action", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ClientID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@StoreCode"].Value = pcode;
                objSQlComm.Parameters["@Action"].Value = pAction;
                objSQlComm.Parameters["@ClientID"].Value = pClientId;

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

        public int GetCustomerInformationFromWeb(string pCode, string pPassword)
        {
            int val = 0;

            string strSQLComm = " select isnull(ID,0) as recId from customerinfo where CustomerCode = @prm1 and LoginPassword = @prm2 and ActiveStatus = 'Y' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm1", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm1"].Value = pCode;
            objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm2"].Value = pPassword;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["recId"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return 0;
            }
        }

        public bool IsActiveCustomer(int refID)
        {
            string val = "N";

            string strSQLComm = " select isnull(ActiveStatus,'N') as sts from customerinfo where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = refID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["sts"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val == "Y";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }

        public DataTable GetCustomerInformationDetailFromWeb(int refID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CustomerInfo where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = refID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Password", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["CustomerCode"].ToString(),
                                                   objSQLReader["LoginPassword"].ToString()
                                                });
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

    }
}
