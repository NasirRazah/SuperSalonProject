/*
 purpose : Data for Scales ( Scale Communication )
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using System.IO;
namespace PosDataObject
{
    public class Scales
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string  strGeneralCode;
        private string  strGeneralDesc;
        private int     intDeptID;

        private int int_LabelFormat_ID;
        private int int_GraphicArt_ID;

        private string str_SCALE_IP;
        private string str_SCALE_LOCATION;
        private string str_PORT;
        private string str_FILE_LOCATION;
        private string str_SCALE_NOTES;

        private int int_SCALE_TYPE;
        private int int_EXP_TEXT_MAX;
        private int int_SPM_TEXT_MAX;
        private int int_SCALE_ID_NO;
        private int int_LABEL_MAX;
        private int int_Mil_Sec;

        private int int_Label_Id;
        private int int_Dp_ID;
        private int int_Graphic_ID;

        private string str_Cat_ID;
        private string str_SKU;
        private double dbl_PriceA;
        private double dbl_AD_Price;
        private int int_row;




        private string str_PLU_NUMBER;
        private string str_SCALE_DESCRIPTION_1;
        private string str_SCALE_DESCRIPTION_2;
        private string str_ITEM_TYPE;
        private string str_FORCE_TARE;
        private string str_INGRED_STATEMENT;
        private string str_SPECIAL_MESSAGE;
        private string str_ServSize;
        private string str_ServPC;
        private string str_Calors;
        private string str_FatCalors;
        private string str_TotFat;
        private string str_SatFat;
        private string str_Chol;
        private string str_Sodium;
        private string str_Carbs;
        private string str_Fiber;
        private string str_Sugar;
        private string str_Proteins;
        private string str_TransFattyAcid;

        private int int_ProductID;
        private int int_BYCOUNT;
        private int int_SHELF_LIFE;
        private int int_PRODUCT_LIFE;
        private int int_SEC_LABEL;
        private int int_TempNum;
        private int int_FatPerc;
        private int int_SatFatPerc;
        private int int_CholPerc;
        private int int_SodPerc;
        private int int_CarbPerc;
        private int int_FiberPerc;
        private int int_CalcPerc;
        private int int_IronPerc;
        private int int_VitDPerc;
        private int int_VitEPerc;
        private int int_VitCPerc;
        private int int_VitAPerc;

        private double dbl_WEIGHT;
        private double dbl_TARE_1_S;
        private double dbl_TARE_2_O;

        private string str_SugarAlcoh;
        private int int_SugarAlcohPerc;

        private int intPosDisplayOrder;
        private string strPOSBackground;
        private string strPOSScreenStyle;
        private string strPOSFontType;
        private string strPOSFontColor;
        private string strPOSItemFontColor;
        private int intPOSFontSize;
        private string strIsBold;
        private string strIsItalics;
        private string strPOSScreenColor;

        private string strAddToPOSScreen;

        private string strReasonText;

        private string strEXT1;
        private string strEXT2;

        private string strPL_JulianDate;
        private string strSL_JulianDate;

        private DataTable dtblStockData;

        private int intScaleDisplayOrder;

        #endregion

        #region definining public variables

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DataTable StockData
        {
            get { return dtblStockData; }
            set { dtblStockData = value; }
        }

        public int ScaleDisplayOrder
        {
            get { return intScaleDisplayOrder; }
            set { intScaleDisplayOrder = value; }
        }

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
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

        public int row
        {
            get { return int_row; }
            set { int_row = value; }
        }

        public int DeptID
        {
            get { return intDeptID; }
            set { intDeptID = value; }
        }

        public int Mil_Sec
        {
            get { return int_Mil_Sec; }
            set { int_Mil_Sec = value; }
        }

        public string GeneralCode
        {
            get { return strGeneralCode; }
            set { strGeneralCode = value; }
        }

        public string GeneralDesc
        {
            get { return strGeneralDesc; }
            set { strGeneralDesc = value; }
        }

        public string SCALE_IP
        {
            get { return str_SCALE_IP; }
            set { str_SCALE_IP = value; }
        }

        public string SCALE_LOCATION
        {
            get { return str_SCALE_LOCATION; }
            set { str_SCALE_LOCATION = value; }
        }

        public string PORT
        {
            get { return str_PORT; }
            set { str_PORT = value; }
        }

        public string FILE_LOCATION
        {
            get { return str_FILE_LOCATION; }
            set { str_FILE_LOCATION = value; }
        }

        public string SCALE_NOTES
        {
            get { return str_SCALE_NOTES; }
            set { str_SCALE_NOTES = value; }
        }

        public int SCALE_TYPE
        {
            get { return int_SCALE_TYPE; }
            set { int_SCALE_TYPE = value; }
        }

        public int EXP_TEXT_MAX
        {
            get { return int_EXP_TEXT_MAX; }
            set { int_EXP_TEXT_MAX = value; }
        }

        public int SPM_TEXT_MAX
        {
            get { return int_SPM_TEXT_MAX; }
            set { int_SPM_TEXT_MAX = value; }
        }

        public int SCALE_ID_NO
        {
            get { return int_SCALE_ID_NO; }
            set { int_SCALE_ID_NO = value; }
        }

        public int LABEL_MAX
        {
            get { return int_LABEL_MAX; }
            set { int_LABEL_MAX = value; }
        }

        public int LabelFormat_ID
        {
            get { return int_LabelFormat_ID; }
            set { int_LabelFormat_ID = value; }
        }

        public int GraphicArt_ID
        {
            get { return int_GraphicArt_ID; }
            set { int_GraphicArt_ID = value; }
        }

        public int Graphic_ID
        {
            get { return int_Graphic_ID; }
            set { int_Graphic_ID = value; }
        }

        public int Label_Id
        {
            get { return int_Label_Id; }
            set { int_Label_Id = value; }
        }

        public int Dp_ID
        {
            get { return int_Dp_ID; }
            set { int_Dp_ID = value; }
        }

        public string PLU_NUMBER
        {
            get { return str_PLU_NUMBER; }
            set { str_PLU_NUMBER = value; }
        }

        public string SCALE_DESCRIPTION_1
        {
            get { return str_SCALE_DESCRIPTION_1; }
            set { str_SCALE_DESCRIPTION_1 = value; }
        }

        public string SCALE_DESCRIPTION_2
        {
            get { return str_SCALE_DESCRIPTION_2; }
            set { str_SCALE_DESCRIPTION_2 = value; }
        }

        public string ITEM_TYPE
        {
            get { return str_ITEM_TYPE; }
            set { str_ITEM_TYPE = value; }
        }

        public string FORCE_TARE
        {
            get { return str_FORCE_TARE; }
            set { str_FORCE_TARE = value; }
        }

        public string INGRED_STATEMENT
        {
            get { return str_INGRED_STATEMENT; }
            set { str_INGRED_STATEMENT = value; }
        }

        public string SPECIAL_MESSAGE
        {
            get { return str_SPECIAL_MESSAGE; }
            set { str_SPECIAL_MESSAGE = value; }
        }

        public string ServSize
        {
            get { return str_ServSize; }
            set { str_ServSize = value; }
        }

        public string ServPC
        {
            get { return str_ServPC; }
            set { str_ServPC = value; }
        }

        public string Calors
        {
            get { return str_Calors; }
            set { str_Calors = value; }
        }

        public string FatCalors
        {
            get { return str_FatCalors; }
            set { str_FatCalors = value; }
        }

        public string TotFat
        {
            get { return str_TotFat; }
            set { str_TotFat = value; }
        }

        public string SatFat
        {
            get { return str_SatFat; }
            set { str_SatFat = value; }
        }

        public string Chol
        {
            get { return str_Chol; }
            set { str_Chol = value; }
        }

        public string Sodium
        {
            get { return str_Sodium; }
            set { str_Sodium = value; }
        }

        public string Carbs
        {
            get { return str_Carbs; }
            set { str_Carbs = value; }
        }

        public string Fiber
        {
            get { return str_Fiber; }
            set { str_Fiber = value; }
        }

        public string Sugar
        {
            get { return str_Sugar; }
            set { str_Sugar = value; }
        }

        public string Proteins
        {
            get { return str_Proteins; }
            set { str_Proteins = value; }
        }

        public string TransFattyAcid
        {
            get { return str_TransFattyAcid; }
            set { str_TransFattyAcid = value; }
        }

        public double WEIGHT
        {
            get { return dbl_WEIGHT; }
            set { dbl_WEIGHT = value; }
        }

        public double TARE_1_S
        {
            get { return dbl_TARE_1_S; }
            set { dbl_TARE_1_S = value; }
        }

        public double TARE_2_O
        {
            get { return dbl_TARE_2_O; }
            set { dbl_TARE_2_O = value; }
        }
        
        public int ProductID
        {
            get { return int_ProductID; }
            set { int_ProductID = value; }
        }

        public int BYCOUNT
        {
            get { return int_BYCOUNT; }
            set { int_BYCOUNT = value; }
        }

        public int SHELF_LIFE
        {
            get { return int_SHELF_LIFE; }
            set { int_SHELF_LIFE = value; }
        }

        public int PRODUCT_LIFE
        {
            get { return int_PRODUCT_LIFE; }
            set { int_PRODUCT_LIFE = value; }
        }

        public int SEC_LABEL
        {
            get { return int_SEC_LABEL; }
            set { int_SEC_LABEL = value; }
        }

        public int TempNum
        {
            get { return int_TempNum; }
            set { int_TempNum = value; }
        }

        public int FatPerc
        {
            get { return int_FatPerc; }
            set { int_FatPerc = value; }
        }

        public int SatFatPerc
        {
            get { return int_SatFatPerc; }
            set { int_SatFatPerc = value; }
        }

        public int CholPerc
        {
            get { return int_CholPerc; }
            set { int_CholPerc = value; }
        }

        public int SodPerc
        {
            get { return int_SodPerc; }
            set { int_SodPerc = value; }
        }

        public int CarbPerc
        {
            get { return int_CarbPerc; }
            set { int_CarbPerc = value; }
        }

        public int FiberPerc
        {
            get { return int_FiberPerc; }
            set { int_FiberPerc = value; }
        }

        public int CalcPerc
        {
            get { return int_CalcPerc; }
            set { int_CalcPerc = value; }
        }

        public int IronPerc
        {
            get { return int_IronPerc; }
            set { int_IronPerc = value; }
        }

        public int VitDPerc
        {
            get { return int_VitDPerc; }
            set { int_VitDPerc = value; }
        }

        public int VitEPerc
        {
            get { return int_VitEPerc; }
            set { int_VitEPerc = value; }
        }

        public int VitCPerc
        {
            get { return int_VitCPerc; }
            set { int_VitCPerc = value; }
        }

        public int VitAPerc
        {
            get { return int_VitAPerc; }
            set { int_VitAPerc = value; }
        }

        public int SugarAlcohPerc
        {
            get { return int_SugarAlcohPerc; }
            set { int_SugarAlcohPerc = value; }
        }

        public string SugarAlcoh
        {
            get { return str_SugarAlcoh; }
            set { str_SugarAlcoh = value; }
        }

        public string Cat_ID
        {
            get { return str_Cat_ID; }
            set { str_Cat_ID = value; }
        }

        public string PL_JulianDate
        {
            get { return strPL_JulianDate; }
            set { strPL_JulianDate = value; }
        }

        public string SL_JulianDate
        {
            get { return strSL_JulianDate; }
            set { strSL_JulianDate = value; }
        }

        public string SKU
        {
            get { return str_SKU; }
            set { str_SKU = value; }
        }


        public double PriceA
        {
            get { return dbl_PriceA; }
            set { dbl_PriceA = value; }
        }


        public double AD_Price
        {
            get { return dbl_AD_Price; }
            set { dbl_AD_Price = value; }
        }


        public int PosDisplayOrder
        {
            get { return intPosDisplayOrder; }
            set { intPosDisplayOrder = value; }
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

        public string POSItemFontColor
        {
            get { return strPOSItemFontColor; }
            set { strPOSItemFontColor = value; }
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

        public string AddToPOSScreen
        {
            get { return strAddToPOSScreen; }
            set { strAddToPOSScreen = value; }
        }

        public string ReasonText
        {
            get { return strReasonText; }
            set { strReasonText = value; }
        }

        public string EXT1
        {
            get { return strEXT1; }
            set { strEXT1 = value; }
        }

        public string EXT2
        {
            get { return strEXT2; }
            set { strEXT2 = value; }
        }

        
        #endregion

        #region SCALE TYPE

        public DataTable FetchLookupScaleType()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,Scale_Type from Scale_Types order by Scale_Type ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Scale_Type"].ToString()
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

        #endregion

        #region SCALE CATEGORY

        #region Insert Data

        public string InsertScaleCategoryData()
        {
            string strSQLComm = " insert into Scale_Category( Cat_ID, Name, Department_ID,PosDisplayOrder,POSBackground,POSScreenStyle, "
                              + " POSFontType, POSFontSize, POSFontColor, IsBold, IsItalics,POSScreenColor,POSItemFontColor,AddToPOSScreen)"
                              + " values ( @Cat_ID, @Name, @Department_ID,@PosDisplayOrder,@POSBackground, @POSScreenStyle,"
                              + "  @POSFontType, @POSFontSize, @POSFontColor, @IsBold, @IsItalics, @POSScreenColor,@POSItemFontColor,@AddToPOSScreen) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Department_ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@PosDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSItemFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSScreen", System.Data.SqlDbType.Char)); 

                objSQlComm.Parameters["@Cat_ID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Name"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Department_ID"].Value = intDeptID;

                objSQlComm.Parameters["@PosDisplayOrder"].Value = intPosDisplayOrder;
                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@POSItemFontColor"].Value = strPOSItemFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;
                objSQlComm.Parameters["@AddToPOSScreen"].Value = strAddToPOSScreen;

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

        public string UpdateScaleCategoryData()
        {
            string strSQLComm = " update Scale_Category set Name=@Name,Department_ID=@Department_ID,PosDisplayOrder=@PosDisplayOrder,"
                              + " POSBackground=@POSBackground,POSScreenStyle=@POSScreenStyle,POSFontType=@POSFontType, POSFontSize=@POSFontSize, "
                              + " POSFontColor=@POSFontColor, IsBold=@IsBold, IsItalics=@IsItalics,POSScreenColor=@POSScreenColor,POSItemFontColor=@POSItemFontColor,"
                              + " AddToPOSScreen=@AddToPOSScreen where Cat_ID = @Cat_ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Department_ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@PosDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSItemFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSScreen", System.Data.SqlDbType.Char)); 

                objSQlComm.Parameters["@Cat_ID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Name"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Department_ID"].Value = intDeptID;

                objSQlComm.Parameters["@PosDisplayOrder"].Value = intPosDisplayOrder;
                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@POSItemFontColor"].Value = strPOSItemFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;
                objSQlComm.Parameters["@AddToPOSScreen"].Value = strAddToPOSScreen;

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

        #region Fetchdata

        public DataTable LookupScaleCategoryData(string searchtxt)
        {
            DataTable dtbl = new DataTable();

            string sqlF = "";

            if (searchtxt != "")
            {
                sqlF = " and (( Cat_ID like '%" + searchtxt + "%') " + " or ( Name like '%" + searchtxt + "%')) ";
            }

            string strSQLComm = " select Cat_ID,Name from Scale_Category where ( 1= 1) " + sqlF + " order by Name ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Cat_ID"].ToString(),
												   objSQLReader["Name"].ToString()});
                }
                //dtbl.Rows.Add(new object[] { "","All Categories" });
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

        public DataTable FetchScaleCategoryData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.Cat_ID,s.Name,s.Department_ID, isnull(d.Description,'') as Dept,s.POSDisplayOrder from Scale_Category s "
                              + " left outer join Dept d on d.ID = s.Department_ID order by isnull(d.Description,''), s.POSDisplayOrder ";

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
                dtbl.Columns.Add("POSDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S", System.Type.GetType("System.String"));
                dtbl.Columns.Add("E", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Cat_ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Department_ID"].ToString(),
                                                   objSQLReader["Dept"].ToString(),
                                                   objSQLReader["POSDisplayOrder"].ToString(),
                                                   "","" });
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

        public DataTable FetchScaleCategoryData1()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.Cat_ID,s.Name,s.Department_ID, isnull(d.Description,'') as Dept,s.POSDisplayOrder from Scale_Category s "
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
                dtbl.Columns.Add("POSDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S", System.Type.GetType("System.String"));
                dtbl.Columns.Add("E", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Cat_ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Department_ID"].ToString(),
                                                   objSQLReader["Dept"].ToString(),
                                                   objSQLReader["POSDisplayOrder"].ToString(),
                                                   "","" });
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

        #region Show Record based on ID

        public DataTable ShowScaleCategoryRecord(string pVal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Category where Cat_ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@ID"].Value = pVal;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PosDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSItemFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSScreen", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {    objSQLReader["Cat_ID"].ToString(),
												    objSQLReader["Name"].ToString(),
												    objSQLReader["Department_ID"].ToString(),
                                                    objSQLReader["PosDisplayOrder"].ToString(),
                                                    objSQLReader["POSBackground"].ToString(),
                                                    objSQLReader["POSScreenStyle"].ToString(),
                                                    objSQLReader["POSScreenColor"].ToString(),
                                                    objSQLReader["POSFontType"].ToString(),
                                                    objSQLReader["POSFontSize"].ToString(),
                                                    objSQLReader["POSFontColor"].ToString(),
                                                    objSQLReader["IsBold"].ToString(),
                                                    objSQLReader["IsItalics"].ToString(),
                                                    objSQLReader["POSItemFontColor"].ToString(),
                                                    objSQLReader["AddToPOSScreen"].ToString() });
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

        #region Duplicate Checking

        public int DuplicateScaleCategoryID(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_Category where Cat_ID= @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

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
        #endregion

        #region  Update POS Display Order

        public int UpdatePosDisplayOrder(string pCID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_Rearrange_Scale_Category_Delete";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@CAT_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CAT_ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CAT_ID"].Value = pCID;

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

        public int UpdateScaleDisplayOrder(int RowID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_UpdateScaleDisplayOrder";

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
                objSQlComm.Parameters["@ID"].Value = RowID;

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


        #region Delete Data
        public string DeleteScaleCategoryRecord(string DeleteID)
        {
            string strSQLComm = " Delete from Scale_Category where Cat_ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
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
        #endregion

        public int MaxPosDisplayOrder(int Dept)
        {
            int intCount = 0;

            string strSQLComm = " select max(PosDisplayOrder) as rcnt from Scale_Category where Department_ID = @D  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = Dept;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["rcnt"]);
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

        public int MaxScaleDisplayOrder(string pCat_ID)
        {
            int intCount = 0;

            string strSQLComm = " select isnull(max(ScaleDisplayOrder),0) + 1 as dorder from Scale_Product where Cat_ID = @C  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@C", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@C"].Value = pCat_ID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["dorder"]);
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

        public DataTable LookupScaleCategory(int iDeptID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select Cat_ID,Name from Scale_Category where Department_ID = @DID order by Name ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = iDeptID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
               

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Cat_ID"].ToString(),
												   objSQLReader["Name"].ToString()});
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


        public int GetDepartmentFromScaleCategory(string p)
        {
            int val = 0;
            string strSQLComm = " select Department_ID from Scale_Category where cat_id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["Department_ID"].ToString());
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


        public string ApplyScaleCategoryStyleToAllItems(string pCat, string pscalescreencolor, string pscalefontcolor)
        {
            string strSQLComm = " update product set ScaleBackground='Color',POSScreenStyle='',ScaleScreenColor=@ScaleScreenColor,ScaleFontColor=@ScaleFontColor "
                              + " where id in ( select productid from scale_product where cat_id = @Cat_ID ) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFontColor", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@Cat_ID"].Value = pCat;
                objSQlComm.Parameters["@ScaleScreenColor"].Value = pscalescreencolor;
                objSQlComm.Parameters["@ScaleFontColor"].Value = pscalefontcolor;

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

        #region SCALE ADDRESS

        #region Insert Data
        public string InsertScaleAddressData()
        {
            string strSQLComm = " insert into Scale_Addresses(SCALE_IP,SCALE_LOCATION,SCALE_TYPE,PORT,FILE_LOCATION,SCALE_ID_NO,SCALE_NOTES,DEPARTMENT_ID,Mil_Sec) "
                              + " values ( @SCALE_IP,@SCALE_LOCATION,@SCALE_TYPE,@PORT,@FILE_LOCATION,@SCALE_ID_NO,@SCALE_NOTES,@DEPARTMENT_ID,@Mil_Sec) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FILE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_ID_NO", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_NOTES", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@DEPARTMENT_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Mil_Sec", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@SCALE_IP"].Value = str_SCALE_IP;
                objSQlComm.Parameters["@SCALE_LOCATION"].Value = str_SCALE_LOCATION;
                objSQlComm.Parameters["@SCALE_TYPE"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@PORT"].Value = str_PORT;
                objSQlComm.Parameters["@FILE_LOCATION"].Value = str_FILE_LOCATION;
                objSQlComm.Parameters["@SCALE_ID_NO"].Value = int_SCALE_ID_NO;
                objSQlComm.Parameters["@SCALE_NOTES"].Value = str_SCALE_NOTES;
                objSQlComm.Parameters["@DEPARTMENT_ID"].Value = intDeptID;
                objSQlComm.Parameters["@Mil_Sec"].Value = int_Mil_Sec;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
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
        public string UpdateScaleAddressData()
        {
            string strSQLComm = " update Scale_Addresses set SCALE_IP=@SCALE_IP,SCALE_LOCATION=@SCALE_LOCATION,SCALE_TYPE=@SCALE_TYPE,"
                              + " PORT=@PORT,FILE_LOCATION=@FILE_LOCATION,SCALE_ID_NO=@SCALE_ID_NO,"
                              + " SCALE_NOTES=@SCALE_NOTES,DEPARTMENT_ID=@DEPARTMENT_ID,Mil_Sec=@Mil_Sec where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FILE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_ID_NO", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_NOTES", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@DEPARTMENT_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Mil_Sec", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@SCALE_IP"].Value = str_SCALE_IP;
                objSQlComm.Parameters["@SCALE_LOCATION"].Value = str_SCALE_LOCATION;
                objSQlComm.Parameters["@SCALE_TYPE"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@PORT"].Value = str_PORT;
                objSQlComm.Parameters["@FILE_LOCATION"].Value = str_FILE_LOCATION;
                objSQlComm.Parameters["@SCALE_ID_NO"].Value = int_SCALE_ID_NO;
                objSQlComm.Parameters["@SCALE_NOTES"].Value = str_SCALE_NOTES;
                objSQlComm.Parameters["@DEPARTMENT_ID"].Value = intDeptID;
                objSQlComm.Parameters["@Mil_Sec"].Value = int_Mil_Sec;

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

        #region Fetchdata

        public DataTable FetchScaleAddressData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.ID,s.SCALE_IP,s.SCALE_LOCATION,s.SCALE_TYPE,s.PORT,isnull(t.Scale_Type,'') as ManuF, "
                              + " s.FILE_LOCATION,s.SCALE_ID_NO,s.SCALE_NOTES,"
                              + " s.DEPARTMENT_ID from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_LOCATION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PORT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FILE_LOCATION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_ID_NO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_NOTES", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DEPARTMENT_ID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ID"].ToString(), 
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["SCALE_LOCATION"].ToString(),
												   objSQLReader["ManuF"].ToString(),
                                                   objSQLReader["PORT"].ToString(),
												   objSQLReader["FILE_LOCATION"].ToString(),
                                                   objSQLReader["SCALE_ID_NO"].ToString(),
												   objSQLReader["SCALE_NOTES"].ToString(),
                                                   objSQLReader["DEPARTMENT_ID"].ToString()});
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

        public DataTable FetchDepartmentWiseScaleAddressData(int pDept)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.ID,s.SCALE_IP,s.SCALE_LOCATION,isnull(t.Scale_Type,'') as ManuF "
                              + " from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE where s.DEPARTMENT_ID = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = pDept;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_LOCATION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_TYPE", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {   
                                                   objSQLReader["ID"].ToString(), 
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["SCALE_LOCATION"].ToString(),
												   objSQLReader["ManuF"].ToString()
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

        #endregion

        #region Show Record based on ID
        public DataTable ShowScaleAddressRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Addresses where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
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
                                                   objSQLReader["ID"].ToString(), 
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["SCALE_LOCATION"].ToString(),
												   objSQLReader["SCALE_TYPE"].ToString(),
                                                   objSQLReader["PORT"].ToString(),
												   objSQLReader["FILE_LOCATION"].ToString(),
                                                   objSQLReader["SCALE_ID_NO"].ToString(),
												   objSQLReader["SCALE_NOTES"].ToString(),
                                                   objSQLReader["DEPARTMENT_ID"].ToString(),
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

        public DataTable ShowScaleIPFromDepartment(int intRecID, int intRefType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select SCALE_IP from Scale_Addresses where DEPARTMENT_ID = @ID and SCALE_TYPE = @SCALE_TYPE ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@SCALE_TYPE"].Value = intRefType;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["SCALE_IP"].ToString()});
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

        #region Duplicate Checking

        public int DuplicateCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from ClassMaster where ClassID=@ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

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

        #region Delete Data
        public string DeleteScaleAddressRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Scale_Addresses Where ID = @ID";

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
        #endregion

        public DataTable FetchScaleAddressLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.ID,s.SCALE_IP,s.SCALE_LOCATION,s.SCALE_TYPE,s.PORT,isnull(t.Scale_Type,'') as ManuF, "
                              + " s.FILE_LOCATION,s.SCALE_ID_NO,s.SCALE_NOTES,"
                              + " s.DEPARTMENT_ID from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PORT", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ID"].ToString(), 
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["ManuF"].ToString(),
                                                   objSQLReader["PORT"].ToString()});
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

        public DataTable FetchScaleForPLU(int Dept)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.ID,s.SCALE_IP,s.SCALE_LOCATION,s.SCALE_TYPE,s.PORT,isnull(t.Scale_Type,'') as ManuF, "
                              + " s.FILE_LOCATION,s.SCALE_ID_NO,s.SCALE_NOTES,"
                              + " s.DEPARTMENT_ID from Scale_Addresses s left outer join Scale_Types t on t.ID = s.SCALE_TYPE "
                              + " where s.DEPARTMENT_ID = @d ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@d", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@d"].Value = Dept;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_IP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PORT", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["ID"].ToString(), 
                                                   objSQLReader["SCALE_IP"].ToString(),
												   objSQLReader["ManuF"].ToString(),
                                                   objSQLReader["PORT"].ToString()});
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

        #region LABEL TYPE

        #region Insert Data
        public string InsertLabel_TypesData()
        {
            string strSQLComm = " insert into Label_Types( Label_Id, Dp_ID,Description,Scale_Type,EXP_TEXT_MAX,SPM_TEXT_MAX,LABEL_MAX ) "
                              + " values ( @Label_Id, @Dp_ID, @Description, @Scale_Type, @EXP_TEXT_MAX, @SPM_TEXT_MAX, @LABEL_MAX ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@Label_Id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Dp_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@EXP_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SPM_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_MAX", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@EXP_TEXT_MAX"].Value = int_EXP_TEXT_MAX;
                objSQlComm.Parameters["@SPM_TEXT_MAX"].Value = int_SPM_TEXT_MAX;
                objSQlComm.Parameters["@LABEL_MAX"].Value = int_LABEL_MAX;

                objSQlComm.Parameters["@Label_Id"].Value = int_Label_Id;
                objSQlComm.Parameters["@Dp_ID"].Value = int_Dp_ID;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
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

        public string UpdateLabel_TypesData()
        {
            string strSQLComm = " update Label_Types set Dp_ID=@Dp_ID,Description=@Description,Scale_Type=@Scale_Type,"
                              + " EXP_TEXT_MAX=@EXP_TEXT_MAX,SPM_TEXT_MAX=@SPM_TEXT_MAX,LABEL_MAX=@LABEL_MAX where ID = @Id";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Dp_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@EXP_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SPM_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_MAX", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@EXP_TEXT_MAX"].Value = int_EXP_TEXT_MAX;
                objSQlComm.Parameters["@SPM_TEXT_MAX"].Value = int_SPM_TEXT_MAX;
                objSQlComm.Parameters["@LABEL_MAX"].Value = int_LABEL_MAX;

                objSQlComm.Parameters["@Id"].Value = intID;
                objSQlComm.Parameters["@Dp_ID"].Value = int_Dp_ID;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;
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

        #region Fetchdata

        public DataTable FetchLabel_TypesData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.ID, s.Label_ID,s.Dp_ID,s.Description,isnull(d.Scale_IP,'') as Scl,s.EXP_TEXT_MAX,s.SPM_TEXT_MAX,s.LABEL_MAX from Label_Types s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type order by s.Label_ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dp_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EXP_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPM_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL_MAX", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Label_ID"].ToString(),
												   objSQLReader["Dp_ID"].ToString() == "0"? "" : objSQLReader["Dp_ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scl"].ToString(),
                                                   objSQLReader["EXP_TEXT_MAX"].ToString(),
                                                   objSQLReader["SPM_TEXT_MAX"].ToString(),
                                                   objSQLReader["LABEL_MAX"].ToString() });
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

        #region Show Record based on ID

        public DataTable ShowLabel_TypesRecord(int pVal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Label_Types where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pVal;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dp_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EXP_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPM_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL_MAX", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Label_ID"].ToString(),
												   objSQLReader["Dp_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                   objSQLReader["EXP_TEXT_MAX"].ToString(),
                                                   objSQLReader["SPM_TEXT_MAX"].ToString(),
                                                   objSQLReader["LABEL_MAX"].ToString() });
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

        public int GetDp_ID(int pVal)
        {
            int val = 0;

            string strSQLComm = " select Dp_ID from Label_Types where Label_ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pVal;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["Dp_ID"].ToString());
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

        
        #endregion

        #region Duplicate Checking

        public int DuplicateLabel_TypesID(int clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Label_Types where Label_ID=@ClassID ";

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

        #endregion

        #region Delete Data
        public string DeleteLabel_TypesRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Label_Types where ID = @ID";

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
        #endregion

        public int GetScaleAddressIDFromLabel(int p)
        {
            int val = 0;
            string strSQLComm = " select Scale_Type from label_types where ID = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["Scale_Type"].ToString());
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

        public DataTable LookupLabelType()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select l.ID, l.Label_ID,l.Description,a.Scale_IP from Label_Types l left outer join Scale_Addresses a on a.ID = l.Scale_Type order by l.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_IP", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { 
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["Label_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_IP"].ToString() });
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

        #region SCALE PARAMETER

        #region Insert Data
        public string InsertScale_ParameterData()
        {
            string strSQLComm = " insert into Scale_Parameters( EXP_TEXT_MAX,SPM_TEXT_MAX,LABEL_MAX ) "
                              + " values ( @EXP_TEXT_MAX, @SPM_TEXT_MAX, @LABEL_MAX ) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@EXP_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SPM_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_MAX", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@EXP_TEXT_MAX"].Value = int_EXP_TEXT_MAX;
                objSQlComm.Parameters["@SPM_TEXT_MAX"].Value = int_SPM_TEXT_MAX;
                objSQlComm.Parameters["@LABEL_MAX"].Value = int_LABEL_MAX;

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

        public string UpdateScale_ParameterData()
        {
            string strSQLComm = " update Scale_Parameters set EXP_TEXT_MAX=@EXP_TEXT_MAX,SPM_TEXT_MAX=@SPM_TEXT_MAX,LABEL_MAX=@LABEL_MAX ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@EXP_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SPM_TEXT_MAX", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_MAX", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@EXP_TEXT_MAX"].Value = int_EXP_TEXT_MAX;
                objSQlComm.Parameters["@SPM_TEXT_MAX"].Value = int_SPM_TEXT_MAX;
                objSQlComm.Parameters["@LABEL_MAX"].Value = int_LABEL_MAX;

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

        #region Fetchdata

        public DataTable FetchScale_Parameter()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(EXP_TEXT_MAX,0) as param1,isnull(SPM_TEXT_MAX,0) as param2,isnull(LABEL_MAX,0) as param3 from Scale_Parameters ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("EXP_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPM_TEXT_MAX", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL_MAX", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["param1"].ToString(),
                                                   objSQLReader["param2"].ToString(),
                                                   objSQLReader["param3"].ToString() });
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

        public int IsExistScaleParameter()
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_Parameters ";

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

        #endregion

        #region SECOND LABEL TYPE

        #region Insert Data

        public string InsertSecond_Label_TypesData()
        {
            string strSQLComm = " insert into Second_Label_Types( Label_Id,Description,Scale_Type,LabelFormat )values ( @Label_Id,@Description,@Scale_Type,@LabelFormat ) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Label_Id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Label_Id"].Value = int_Label_Id;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@LabelFormat"].Value = int_LabelFormat_ID;

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

        public string UpdateSecond_Label_TypesData()
        {
            string strSQLComm = " update Second_Label_Types set Description=@Description,Scale_Type=@Scale_Type,LabelFormat=@LabelFormat where Label_Id = @Label_Id";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Label_Id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Label_Id"].Value = int_Label_Id;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@LabelFormat"].Value = int_LabelFormat_ID;

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

        #region Fetchdata

        public DataTable FetchSecond_Label_TypesData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.Label_ID,s.Description,isnull(d.Scale_IP,'') as Scl,isnull(l.FormatName,'') as LabelF from Second_Label_Types s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type left outer join LabelFormats l on l.FormatID = s.LabelFormat "
                              + " order by s.Label_ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Label", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Label_ID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scl"].ToString(),
                                                   objSQLReader["LabelF"].ToString() });
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

        #region Show Record based on ID

        public DataTable ShowSecond_Label_TypesRecord(int pVal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Second_Label_Types where Label_ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pVal;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Label_Format", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Label_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                   objSQLReader["LabelFormat"].ToString() });
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

        #region Duplicate Checking

        public int DuplicateSecond_Label_TypesID(int clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Second_Label_Types where Label_ID=@ClassID ";

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

        #endregion

        #region Delete Data
        public string DeleteSecond_Label_TypesRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Second_Label_Types where Label_ID = @ID";

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
        #endregion

        public DataTable LookupSecondLabelType()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select l.Label_ID,l.Description,a.Scale_IP from Second_Label_Types l left outer join Scale_Addresses a on a.ID = l.Scale_Type order by l.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_IP", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Label_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_IP"].ToString() });
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

        #region SCALE GRAPHICS

        #region Insert Data
        public string InsertScale_GraphicsData()
        {
            string strSQLComm = " insert into Scale_Graphics( Graphic_ID,Description,Scale_Type,GraphicArtID )values ( @Graphic_ID,@Description,@Scale_Type, @GraphicArtID ) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GraphicArtID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Graphic_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@GraphicArtID"].Value = int_GraphicArt_ID;

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

        public string UpdateScale_GraphicsData()
        {
            string strSQLComm = " update Scale_Graphics set Description=@Description,Scale_Type=@Scale_Type,GraphicArtID=@GraphicArtID where Graphic_ID = @Graphic_ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Graphic_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Type", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GraphicArtID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Graphic_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@Scale_Type"].Value = int_SCALE_TYPE;
                objSQlComm.Parameters["@GraphicArtID"].Value = int_GraphicArt_ID;

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

        #region Fetchdata

        public DataTable FetchScale_GraphicsData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.Graphic_ID,s.Description,isnull(d.Scale_IP,'') as Scl, s.GraphicArtID, a.GraphicArt from Scale_Graphics s "
                              + " left outer join Scale_Addresses d on d.ID = s.Scale_Type left outer join GraphicArts a on a.ID = s.GraphicArtID order by s.Graphic_ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Graphic_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArtID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArt", typeof(byte[]));

                while (objSQLReader.Read())
                {
                        dtbl.Rows.Add(new object[] {    objSQLReader["Graphic_ID"].ToString(),
                                                        objSQLReader["Description"].ToString(),
                                                        objSQLReader["Scl"].ToString(),
                                                        objSQLReader["GraphicArtID"].ToString(),
                                                        objSQLReader["GraphicArt"].ToString() != "" ? (byte[])objSQLReader["GraphicArt"] : null  });
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

        #region Show Record based on ID

        public DataTable ShowScale_GraphicsRecord(int pVal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Graphics where Graphic_ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pVal;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Graphic_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_Type", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArtID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Graphic_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_Type"].ToString(),
                                                    objSQLReader["GraphicArtID"].ToString()  });
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

        #region Duplicate Checking

        public int DuplicateScale_GraphicsID(int clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_Graphics where Graphic_ID=@ClassID ";

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

        #endregion

        #region Delete Data
        public string DeleteScale_GraphicsRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Scale_Graphics where Graphic_ID = @ID";

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
        #endregion

        public DataTable LookupScaleGraphics()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select Graphic_ID,Description from Scale_Graphics order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Graphic_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Graphic_ID"].ToString(),
												   objSQLReader["Description"].ToString() });
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

        public DataTable LookupScaleGraphics1()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select l.Graphic_ID,l.Description,a.Scale_IP from Scale_Graphics l left outer join Scale_Addresses a on a.ID = l.Scale_Type order by l.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_IP", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["Graphic_ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Scale_IP"].ToString() });
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

        #region SCALE PLU

        

        #region Insert Data
        public string InsertScaleProductData()
        {
            string strSQLComm = " insert into Scale_Product(PLU_NUMBER,SCALE_DESCRIPTION_1,SCALE_DESCRIPTION_2,ITEM_TYPE,BYCOUNT,WEIGHT,"
                              + " SHELF_LIFE,PRODUCT_LIFE,LABEL_1,INGRED_STATEMENT,SPECIAL_MESSAGE,GRAPHIC_ID,SEC_LABEL,FORCE_TARE,"
                              + " TARE_1_S,TARE_2_O,TempNum,ServSize,ServPC,Calors,FatCalors,TotFat,FatPerc,SatFat,SatFatPerc,"
                              + " Chol,CholPerc,Sodium,SodPerc,Carbs,CarbPerc,Fiber,FiberPerc,Sugar,Proteins,CalcPerc,IronPerc,"
                              + " VitDPerc,VitEPerc,VitCPerc,VitAPerc,TransFattyAcid,ProductID,Cat_ID, EXT1, EXT2, "
                              + " PL_JulianDate,SL_JulianDate,ScaleDisplayOrder,SugarAlcoh,SugarAlcohPerc ) "
                              + " values ( @PLU_NUMBER,@SCALE_DESCRIPTION_1,@SCALE_DESCRIPTION_2,@ITEM_TYPE,@BYCOUNT,@WEIGHT,"
                              + " @SHELF_LIFE,@PRODUCT_LIFE,@LABEL_1,@INGRED_STATEMENT,@SPECIAL_MESSAGE,@GRAPHIC_ID,@SEC_LABEL,@FORCE_TARE,"
                              + " @TARE_1_S,@TARE_2_O,@TempNum,@ServSize,@ServPC,@Calors,@FatCalors,@TotFat,@FatPerc,@SatFat,@SatFatPerc,"
                              + " @Chol,@CholPerc,@Sodium,@SodPerc,@Carbs,@CarbPerc,@Fiber,@FiberPerc,@Sugar,@Proteins,@CalcPerc,@IronPerc,"
                              + " @VitDPerc,@VitEPerc,@VitCPerc,@VitAPerc,@TransFattyAcid,@ProductID,@Cat_ID, @EXT1, @EXT2, "
                              + " @PL_JulianDate,@SL_JulianDate,@ScaleDisplayOrder,@SugarAlcoh,@SugarAlcohPerc ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@PLU_NUMBER", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ITEM_TYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FORCE_TARE", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@INGRED_STATEMENT", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@SPECIAL_MESSAGE", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@ServSize", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ServPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Calors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FatCalors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Chol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sodium", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Carbs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fiber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sugar", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Proteins", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TransFattyAcid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WEIGHT", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_1_S", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_2_O", System.Data.SqlDbType.Float));


                objSQlComm.Parameters.Add(new SqlParameter("@BYCOUNT", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SHELF_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PRODUCT_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SEC_LABEL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GRAPHIC_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TempNum", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CholPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SodPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CarbPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FiberPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CalcPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IronPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitDPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitEPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitCPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitAPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@EXT1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EXT2", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@PL_JulianDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SL_JulianDate", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcoh", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcohPerc", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@PLU_NUMBER"].Value = str_PLU_NUMBER;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Value = str_SCALE_DESCRIPTION_1;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Value = str_SCALE_DESCRIPTION_2;
                objSQlComm.Parameters["@ITEM_TYPE"].Value = str_ITEM_TYPE;
                objSQlComm.Parameters["@FORCE_TARE"].Value = str_FORCE_TARE;
                objSQlComm.Parameters["@INGRED_STATEMENT"].Value = str_INGRED_STATEMENT;
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Value = str_SPECIAL_MESSAGE;
                objSQlComm.Parameters["@ServSize"].Value = str_ServSize;
                objSQlComm.Parameters["@ServPC"].Value = str_ServPC;
                objSQlComm.Parameters["@Calors"].Value = str_Calors;
                objSQlComm.Parameters["@FatCalors"].Value = str_FatCalors;
                objSQlComm.Parameters["@TotFat"].Value = str_TotFat;
                objSQlComm.Parameters["@SatFat"].Value = str_SatFat;
                objSQlComm.Parameters["@Chol"].Value = str_Chol;
                objSQlComm.Parameters["@Sodium"].Value = str_Sodium;
                objSQlComm.Parameters["@Carbs"].Value = str_Carbs;
                objSQlComm.Parameters["@Fiber"].Value = str_Fiber;
                objSQlComm.Parameters["@Proteins"].Value = str_Proteins;
                objSQlComm.Parameters["@Sugar"].Value = str_Sugar;
                objSQlComm.Parameters["@TransFattyAcid"].Value = str_TransFattyAcid;

                objSQlComm.Parameters["@WEIGHT"].Value = dbl_WEIGHT;
                objSQlComm.Parameters["@TARE_1_S"].Value = dbl_TARE_1_S;
                objSQlComm.Parameters["@TARE_2_O"].Value = dbl_TARE_2_O;

                objSQlComm.Parameters["@BYCOUNT"].Value = int_BYCOUNT;
                objSQlComm.Parameters["@SHELF_LIFE"].Value = int_SHELF_LIFE;
                objSQlComm.Parameters["@PRODUCT_LIFE"].Value = int_PRODUCT_LIFE;
                objSQlComm.Parameters["@LABEL_1"].Value = int_Label_Id;
                objSQlComm.Parameters["@SEC_LABEL"].Value = int_SEC_LABEL;
                objSQlComm.Parameters["@GRAPHIC_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@TempNum"].Value = int_TempNum;
                objSQlComm.Parameters["@FatPerc"].Value = int_FatPerc;
                objSQlComm.Parameters["@SatFatPerc"].Value = int_SatFatPerc;
                objSQlComm.Parameters["@CholPerc"].Value = int_CholPerc;
                objSQlComm.Parameters["@SodPerc"].Value = int_SodPerc;
                objSQlComm.Parameters["@CarbPerc"].Value = int_CarbPerc;
                objSQlComm.Parameters["@FiberPerc"].Value = int_FiberPerc;
                objSQlComm.Parameters["@CalcPerc"].Value = int_CalcPerc;
                objSQlComm.Parameters["@IronPerc"].Value = int_IronPerc;
                objSQlComm.Parameters["@VitDPerc"].Value = int_VitDPerc;
                objSQlComm.Parameters["@VitEPerc"].Value = int_VitEPerc;
                objSQlComm.Parameters["@VitCPerc"].Value = int_VitCPerc;
                objSQlComm.Parameters["@VitAPerc"].Value = int_VitAPerc;
                objSQlComm.Parameters["@ProductID"].Value = int_ProductID;
                objSQlComm.Parameters["@Cat_ID"].Value = str_Cat_ID;

                objSQlComm.Parameters["@EXT1"].Value = strEXT1;
                objSQlComm.Parameters["@EXT2"].Value = strEXT2;
                objSQlComm.Parameters["@PL_JulianDate"].Value = strPL_JulianDate;
                objSQlComm.Parameters["@SL_JulianDate"].Value = strSL_JulianDate;
                objSQlComm.Parameters["@ScaleDisplayOrder"].Value = intScaleDisplayOrder;

                objSQlComm.Parameters["@SugarAlcoh"].Value = str_SugarAlcoh;
                objSQlComm.Parameters["@SugarAlcohPerc"].Value = int_SugarAlcohPerc;


                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
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
        public string UpdateScaleProductData()
        {
            string strSQLComm = " update Scale_Product set Cat_ID=@Cat_ID, SCALE_DESCRIPTION_1=@SCALE_DESCRIPTION_1,SCALE_DESCRIPTION_2=@SCALE_DESCRIPTION_2,"
                              + " ITEM_TYPE=@ITEM_TYPE,BYCOUNT=@BYCOUNT,WEIGHT=@WEIGHT,SHELF_LIFE=@SHELF_LIFE,PRODUCT_LIFE=@PRODUCT_LIFE,"
                              + " LABEL_1=@LABEL_1,INGRED_STATEMENT=@INGRED_STATEMENT,SPECIAL_MESSAGE=@SPECIAL_MESSAGE,GRAPHIC_ID=@GRAPHIC_ID,"
                              + " SEC_LABEL=@SEC_LABEL,FORCE_TARE=@FORCE_TARE,TARE_1_S=@TARE_1_S,TARE_2_O=@TARE_2_O,TempNum=@TempNum,"
                              + " ServSize=@ServSize,ServPC=@ServPC,Calors=@Calors,FatCalors=@FatCalors,TotFat=@TotFat,FatPerc=@FatPerc,"
                              + " SatFat=@SatFat,SatFatPerc=@SatFatPerc,Chol=@Chol,CholPerc=@CholPerc,Sodium=@Sodium,SodPerc=@SodPerc,"
                              + " Carbs=@Carbs,CarbPerc=@CarbPerc,Fiber=@Fiber,FiberPerc=@FiberPerc,Sugar=@Sugar,Proteins=@Proteins,"
                              + " CalcPerc=@CalcPerc,IronPerc=@IronPerc,VitDPerc=@VitDPerc,VitEPerc=@VitEPerc,VitCPerc=@VitCPerc,"
                              + " VitAPerc=@VitAPerc,TransFattyAcid=@TransFattyAcid, EXT1=@EXT1, EXT2=@EXT2, PL_JulianDate=@PL_JulianDate, "
                              + " SL_JulianDate=@SL_JulianDate, ScaleDisplayOrder = @ScaleDisplayOrder, SugarAlcoh=@SugarAlcoh, SugarAlcohPerc = @SugarAlcohPerc where row_id = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ITEM_TYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FORCE_TARE", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@INGRED_STATEMENT", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@SPECIAL_MESSAGE", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@ServSize", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ServPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Calors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FatCalors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Chol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sodium", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Carbs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fiber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sugar", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Proteins", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TransFattyAcid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WEIGHT", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_1_S", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_2_O", System.Data.SqlDbType.Float));


                objSQlComm.Parameters.Add(new SqlParameter("@BYCOUNT", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SHELF_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PRODUCT_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LABEL_1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SEC_LABEL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GRAPHIC_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TempNum", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CholPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SodPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CarbPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FiberPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CalcPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IronPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitDPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitEPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitCPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitAPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EXT1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EXT2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PL_JulianDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SL_JulianDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcoh", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcohPerc", System.Data.SqlDbType.Int));


                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Value = str_SCALE_DESCRIPTION_1;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Value = str_SCALE_DESCRIPTION_2;
                objSQlComm.Parameters["@ITEM_TYPE"].Value = str_ITEM_TYPE;
                objSQlComm.Parameters["@FORCE_TARE"].Value = str_FORCE_TARE;
                objSQlComm.Parameters["@INGRED_STATEMENT"].Value = str_INGRED_STATEMENT;
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Value = str_SPECIAL_MESSAGE;
                objSQlComm.Parameters["@ServSize"].Value = str_ServSize;
                objSQlComm.Parameters["@ServPC"].Value = str_ServPC;
                objSQlComm.Parameters["@Calors"].Value = str_Calors;
                objSQlComm.Parameters["@FatCalors"].Value = str_FatCalors;
                objSQlComm.Parameters["@TotFat"].Value = str_TotFat;
                objSQlComm.Parameters["@SatFat"].Value = str_SatFat;
                objSQlComm.Parameters["@Chol"].Value = str_Chol;
                objSQlComm.Parameters["@Sodium"].Value = str_Sodium;
                objSQlComm.Parameters["@Carbs"].Value = str_Carbs;
                objSQlComm.Parameters["@Fiber"].Value = str_Fiber;
                objSQlComm.Parameters["@Proteins"].Value = str_Proteins;
                objSQlComm.Parameters["@Sugar"].Value = str_Sugar;
                objSQlComm.Parameters["@TransFattyAcid"].Value = str_TransFattyAcid;

                objSQlComm.Parameters["@WEIGHT"].Value = dbl_WEIGHT;
                objSQlComm.Parameters["@TARE_1_S"].Value = dbl_TARE_1_S;
                objSQlComm.Parameters["@TARE_2_O"].Value = dbl_TARE_2_O;

                objSQlComm.Parameters["@BYCOUNT"].Value = int_BYCOUNT;
                objSQlComm.Parameters["@SHELF_LIFE"].Value = int_SHELF_LIFE;
                objSQlComm.Parameters["@PRODUCT_LIFE"].Value = int_PRODUCT_LIFE;
                objSQlComm.Parameters["@LABEL_1"].Value = int_Label_Id;
                objSQlComm.Parameters["@SEC_LABEL"].Value = int_SEC_LABEL;
                objSQlComm.Parameters["@GRAPHIC_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@TempNum"].Value = int_TempNum;
                objSQlComm.Parameters["@FatPerc"].Value = int_FatPerc;
                objSQlComm.Parameters["@SatFatPerc"].Value = int_SatFatPerc;
                objSQlComm.Parameters["@CholPerc"].Value = int_CholPerc;
                objSQlComm.Parameters["@SodPerc"].Value = int_SodPerc;
                objSQlComm.Parameters["@CarbPerc"].Value = int_CarbPerc;
                objSQlComm.Parameters["@FiberPerc"].Value = int_FiberPerc;
                objSQlComm.Parameters["@CalcPerc"].Value = int_CalcPerc;
                objSQlComm.Parameters["@IronPerc"].Value = int_IronPerc;
                objSQlComm.Parameters["@VitDPerc"].Value = int_VitDPerc;
                objSQlComm.Parameters["@VitEPerc"].Value = int_VitEPerc;
                objSQlComm.Parameters["@VitCPerc"].Value = int_VitCPerc;
                objSQlComm.Parameters["@VitAPerc"].Value = int_VitAPerc;
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Cat_ID"].Value = str_Cat_ID;
                objSQlComm.Parameters["@EXT1"].Value = strEXT1;
                objSQlComm.Parameters["@EXT2"].Value = strEXT2;
                objSQlComm.Parameters["@PL_JulianDate"].Value = strPL_JulianDate;
                objSQlComm.Parameters["@SL_JulianDate"].Value = strSL_JulianDate;
                objSQlComm.Parameters["@ScaleDisplayOrder"].Value = intScaleDisplayOrder;

                objSQlComm.Parameters["@SugarAlcoh"].Value = str_SugarAlcoh;
                objSQlComm.Parameters["@SugarAlcohPerc"].Value = int_SugarAlcohPerc;

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

        #region Show Record based on ID

        public DataTable ShowScaleProductRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Product where row_id = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

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
                    dtbl.Rows.Add(new object[] {   
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return null;
            }
        }

        #endregion

        #region Fetchdata

        public DataTable FetchScaleProductData(int pdid)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select s.row_id,s.PLU_NUMBER,s.ITEM_TYPE,t.Description as LabelText "
                              + " from Scale_Product s left outer join Label_Types t on t.Label_ID = s.LABEL_1 where s.ProductID = @PID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = pdid;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ITEM_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["row_id"].ToString(), 
                                                   objSQLReader["PLU_NUMBER"].ToString().TrimStart('0'),
												   objSQLReader["ITEM_TYPE"].ToString(),
												   objSQLReader["LabelText"].ToString()});
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

        #region Delete Data
        public string DeleteScaleProductRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Scale_Product where row_id = @ID";

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
        #endregion

        public int DuplicateScale_Product_Label(int PID, int LID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_Product where ProductID = @PID and Label_1= @LID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = PID;
                
                objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LID"].Value = LID;

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

        public DataTable FetchScaleMapping(int pdid,string pMapping)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            
            if (pMapping == "Label")
                strSQLComm = " select m.ID as RowID, m.MappingID as Label_ID, l.Description, isnull(i.Scale_IP,'') as IP from Scale_Mapping m Left outer join Label_Types l "
                           + " on m.ID = l.ID left outer join Scale_Addresses i on i.ID = l.Scale_Type where m.ProductID = @PID and ScaleMappingType = 'Label' ";

            if (pMapping == "Second Label")
                strSQLComm = " select 0 as RowID, m.MappingID as Label_ID, l.Description, '' as IP from Scale_Mapping m Left outer join Second_Label_Types l "
                           + " on m.MappingID = l.Label_ID where m.ProductID = @PID and ScaleMappingType = 'Second Label' ";
            
            if (pMapping == "Graphic")
                strSQLComm = " select 0 as RowID, m.MappingID as Label_ID, l.Description, '' as IP from Scale_Mapping m Left outer join Scale_Graphics l "
                           + " on m.MappingID = l.Graphic_ID where m.ProductID = @PID and ScaleMappingType = 'Graphic' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = pdid;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Label_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Scale_IP", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["RowID"].ToString(), objSQLReader["Label_ID"].ToString(), objSQLReader["Description"].ToString(), objSQLReader["IP"].ToString() });
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

        public string DeleteScale_Mapping(int DeleteID)
        {
            string strSQLComm = " Delete from Scale_Mapping where ProductID = @ID";

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

        public string InsertScale_Mapping(string p1, int p2, int p3, int p4)
        {
            string strSQLComm = "insert into Scale_Mapping ( ScaleMappingType,MappingID,ProductID,ID ) values (@ScaleMappingType,@MappingID,@ProductID,@ID )";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleMappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                
                objSQlComm.Parameters["@ScaleMappingType"].Value = p1;
                objSQlComm.Parameters["@MappingID"].Value = p2;
                objSQlComm.Parameters["@ProductID"].Value = p3;
                objSQlComm.Parameters["@ID"].Value = p4;

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

        public string GetLabelId(int p)
        {
            string val = "";
            string strSQLComm = " select Label_Id from label_types where id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["Label_Id"].ToString();
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

        public string GetLabelText(int p)
        {
            string val = "";
            string strSQLComm = " select Description from label_types where id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["Description"].ToString();
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

        public string GetLabelIp(int p)
        {
            string val = "";
            string strSQLComm = " select isnull(i.Scale_IP, '') as Ip from scale_addresses i left outer join label_types l on l.scale_type = i.ID where l.id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["Ip"].ToString();
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

        public string GetSecondLabelText(int p)
        {
            string val = "";
            string strSQLComm = " select Description from second_label_types where label_id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["Description"].ToString();
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

        public string GetGraphicText(int p)
        {
            string val = "";
            string strSQLComm = " select Description from scale_graphics where graphic_id = @prm ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@prm"].Value = p;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["Description"].ToString();
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

        #endregion

        #region SCALE COM

        public bool IfExistsInScaleCom(int PID, int LID, string Scale_IP)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_Com where ProductID = @PID and Label_ID= @LID and Scale_IP = @IP ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = PID;

                objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LID"].Value = LID;

                objSQlComm.Parameters.Add(new SqlParameter("@IP", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@IP"].Value = Scale_IP;

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
                return intCount > 0;
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

        public string InsertScaleComm()
        {
            string strSQLComm = " insert into Scale_Com(SCALE_IP,SCALE_TYPE,PORT,FILE_LOCATION,Label_ID,DepartmentID,Cat_ID,SKU,PriceA,AD_Price,Mil_Sec,"
                              + " PLU_NUMBER,SCALE_DESCRIPTION_1,SCALE_DESCRIPTION_2,ITEM_TYPE,BYCOUNT,WEIGHT,"
                              + " SHELF_LIFE,PRODUCT_LIFE,INGRED_STATEMENT,SPECIAL_MESSAGE,GRAPHIC_ID,SEC_LABEL,FORCE_TARE,"
                              + " TARE_1_S,TARE_2_O,TempNum,ServSize,ServPC,Calors,FatCalors,TotFat,FatPerc,SatFat,SatFatPerc,"
                              + " Chol,CholPerc,Sodium,SodPerc,Carbs,CarbPerc,Fiber,FiberPerc,Sugar,Proteins,CalcPerc,IronPerc,"
                              + " VitDPerc,VitEPerc,VitCPerc,VitAPerc,TransFattyAcid,ProductID, SugarAlcoh, SugarAlcohPerc ) "
                              + " values (@SCALE_IP,@SCALE_TYPE,@PORT,@FILE_LOCATION,@Label_ID,@DepartmentID,@Cat_ID,@SKU,@PriceA,@AD_Price,@Mil_Sec,"
                              + " @PLU_NUMBER,@SCALE_DESCRIPTION_1,@SCALE_DESCRIPTION_2,@ITEM_TYPE,@BYCOUNT,@WEIGHT,"
                              + " @SHELF_LIFE,@PRODUCT_LIFE,@INGRED_STATEMENT,@SPECIAL_MESSAGE,@GRAPHIC_ID,@SEC_LABEL,@FORCE_TARE,"
                              + " @TARE_1_S,@TARE_2_O,@TempNum,@ServSize,@ServPC,@Calors,@FatCalors,@TotFat,@FatPerc,@SatFat,@SatFatPerc,"
                              + " @Chol,@CholPerc,@Sodium,@SodPerc,@Carbs,@CarbPerc,@Fiber,@FiberPerc,@Sugar,@Proteins,@CalcPerc,@IronPerc,"
                              + " @VitDPerc,@VitEPerc,@VitCPerc,@VitAPerc,@TransFattyAcid,@ProductID,@SugarAlcoh, @SugarAlcohPerc) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FILE_LOCATION", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Label_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AD_Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Mil_Sec", System.Data.SqlDbType.Int));


                objSQlComm.Parameters.Add(new SqlParameter("@PLU_NUMBER", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ITEM_TYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FORCE_TARE", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@INGRED_STATEMENT", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@SPECIAL_MESSAGE", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@ServSize", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ServPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Calors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FatCalors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Chol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sodium", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Carbs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fiber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sugar", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Proteins", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TransFattyAcid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WEIGHT", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_1_S", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_2_O", System.Data.SqlDbType.Float));


                objSQlComm.Parameters.Add(new SqlParameter("@BYCOUNT", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SHELF_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PRODUCT_LIFE", System.Data.SqlDbType.Int));
                
                objSQlComm.Parameters.Add(new SqlParameter("@SEC_LABEL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GRAPHIC_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TempNum", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CholPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SodPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CarbPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FiberPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CalcPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IronPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitDPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitEPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitCPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitAPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcoh", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcohPerc", System.Data.SqlDbType.Int));
                

                objSQlComm.Parameters["@PLU_NUMBER"].Value = str_PLU_NUMBER;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Value = str_SCALE_DESCRIPTION_1;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Value = str_SCALE_DESCRIPTION_2;
                objSQlComm.Parameters["@ITEM_TYPE"].Value = str_ITEM_TYPE;
                objSQlComm.Parameters["@FORCE_TARE"].Value = str_FORCE_TARE;
                objSQlComm.Parameters["@INGRED_STATEMENT"].Value = str_INGRED_STATEMENT;
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Value = str_SPECIAL_MESSAGE;
                objSQlComm.Parameters["@ServSize"].Value = str_ServSize;
                objSQlComm.Parameters["@ServPC"].Value = str_ServPC;
                objSQlComm.Parameters["@Calors"].Value = str_Calors;
                objSQlComm.Parameters["@FatCalors"].Value = str_FatCalors;
                objSQlComm.Parameters["@TotFat"].Value = str_TotFat;
                objSQlComm.Parameters["@SatFat"].Value = str_SatFat;
                objSQlComm.Parameters["@Chol"].Value = str_Chol;
                objSQlComm.Parameters["@Sodium"].Value = str_Sodium;
                objSQlComm.Parameters["@Carbs"].Value = str_Carbs;
                objSQlComm.Parameters["@Fiber"].Value = str_Fiber;
                objSQlComm.Parameters["@Proteins"].Value = str_Proteins;
                objSQlComm.Parameters["@Sugar"].Value = str_Sugar;
                objSQlComm.Parameters["@TransFattyAcid"].Value = str_TransFattyAcid;

                objSQlComm.Parameters["@WEIGHT"].Value = dbl_WEIGHT;
                objSQlComm.Parameters["@TARE_1_S"].Value = dbl_TARE_1_S;
                objSQlComm.Parameters["@TARE_2_O"].Value = dbl_TARE_2_O;

                objSQlComm.Parameters["@BYCOUNT"].Value = int_BYCOUNT;
                objSQlComm.Parameters["@SHELF_LIFE"].Value = int_SHELF_LIFE;
                objSQlComm.Parameters["@PRODUCT_LIFE"].Value = int_PRODUCT_LIFE;
                objSQlComm.Parameters["@SEC_LABEL"].Value = int_SEC_LABEL;
                objSQlComm.Parameters["@GRAPHIC_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@TempNum"].Value = int_TempNum;
                objSQlComm.Parameters["@FatPerc"].Value = int_FatPerc;
                objSQlComm.Parameters["@SatFatPerc"].Value = int_SatFatPerc;
                objSQlComm.Parameters["@CholPerc"].Value = int_CholPerc;
                objSQlComm.Parameters["@SodPerc"].Value = int_SodPerc;
                objSQlComm.Parameters["@CarbPerc"].Value = int_CarbPerc;
                objSQlComm.Parameters["@FiberPerc"].Value = int_FiberPerc;
                objSQlComm.Parameters["@CalcPerc"].Value = int_CalcPerc;
                objSQlComm.Parameters["@IronPerc"].Value = int_IronPerc;
                objSQlComm.Parameters["@VitDPerc"].Value = int_VitDPerc;
                objSQlComm.Parameters["@VitEPerc"].Value = int_VitEPerc;
                objSQlComm.Parameters["@VitCPerc"].Value = int_VitCPerc;
                objSQlComm.Parameters["@VitAPerc"].Value = int_VitAPerc;
                objSQlComm.Parameters["@ProductID"].Value = int_ProductID;


                objSQlComm.Parameters["@SCALE_IP"].Value =      str_SCALE_IP;
                objSQlComm.Parameters["@SCALE_TYPE"].Value =    int_SCALE_TYPE;
                objSQlComm.Parameters["@PORT"].Value =          str_PORT;
                objSQlComm.Parameters["@FILE_LOCATION"].Value = str_FILE_LOCATION;
                objSQlComm.Parameters["@Label_ID"].Value = int_Label_Id;
                objSQlComm.Parameters["@DepartmentID"].Value = int_Dp_ID;
                objSQlComm.Parameters["@Cat_ID"].Value = str_Cat_ID;
                objSQlComm.Parameters["@SKU"].Value = str_SKU;
                objSQlComm.Parameters["@PriceA"].Value = dbl_PriceA;
                objSQlComm.Parameters["@AD_Price"].Value = dbl_AD_Price;
                objSQlComm.Parameters["@Mil_Sec"].Value = int_Mil_Sec;
                objSQlComm.Parameters["@SugarAlcoh"].Value = str_SugarAlcoh;
                objSQlComm.Parameters["@SugarAlcohPerc"].Value = int_SugarAlcohPerc;

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

        public string UpdateScaleCommForChangeScalePLU()
        {
            string strSQLComm = " update Scale_Com set "
                              + " DepartmentID=@DepartmentID,Cat_ID=@Cat_ID,PriceA=@PriceA,AD_Price=@AD_Price,"
                              + " PLU_NUMBER=@PLU_NUMBER,SCALE_DESCRIPTION_1=@SCALE_DESCRIPTION_1,SCALE_DESCRIPTION_2=@SCALE_DESCRIPTION_2,"
                              + " ITEM_TYPE=@ITEM_TYPE,BYCOUNT=@BYCOUNT,WEIGHT=@WEIGHT,SHELF_LIFE=@SHELF_LIFE,PRODUCT_LIFE=@PRODUCT_LIFE,"
                              + " INGRED_STATEMENT=@INGRED_STATEMENT,SPECIAL_MESSAGE=@SPECIAL_MESSAGE,GRAPHIC_ID=@GRAPHIC_ID,SEC_LABEL=@SEC_LABEL,"
                              + " FORCE_TARE=@FORCE_TARE,TARE_1_S=@TARE_1_S,TARE_2_O=@TARE_2_O,TempNum=@TempNum,ServSize=@ServSize,"
                              + " ServPC=@ServPC,Calors=@Calors,FatCalors=@FatCalors,TotFat=@TotFat,FatPerc=@FatPerc,SatFat=@SatFat,"
                              + " SatFatPerc=@SatFatPerc,Chol=@Chol,CholPerc=@CholPerc,Sodium=@Sodium,SodPerc=@SodPerc,Carbs=@Carbs,CarbPerc=@CarbPerc,"
                              + " Fiber=@Fiber,FiberPerc=@FiberPerc,Sugar=@Sugar,Proteins=@Proteins,CalcPerc=@CalcPerc,IronPerc=@IronPerc,"
                              + " VitDPerc=@VitDPerc,VitEPerc=@VitEPerc,VitCPerc=@VitCPerc,VitAPerc=@VitAPerc,TransFattyAcid=@TransFattyAcid, SugarAlcoh=@SugarAlcoh, SugarAlcohPerc=@SugarAlcohPerc "
                              + " where ProductID=@ProductID and Label_ID = @Label_ID ";
                             

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Label_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));

                //objSQlComm.Parameters.Add(new SqlParameter("@SCALE_IP", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@SCALE_TYPE", System.Data.SqlDbType.Int));
                //objSQlComm.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.VarChar));
                //objSQlComm.Parameters.Add(new SqlParameter("@FILE_LOCATION", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Cat_ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AD_Price", System.Data.SqlDbType.Float));
                //objSQlComm.Parameters.Add(new SqlParameter("@Mil_Sec", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PLU_NUMBER", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SCALE_DESCRIPTION_2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ITEM_TYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FORCE_TARE", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@INGRED_STATEMENT", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@SPECIAL_MESSAGE", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@ServSize", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ServPC", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Calors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FatCalors", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Chol", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sodium", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Carbs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fiber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Sugar", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Proteins", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TransFattyAcid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WEIGHT", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_1_S", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TARE_2_O", System.Data.SqlDbType.Float));


                objSQlComm.Parameters.Add(new SqlParameter("@BYCOUNT", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SHELF_LIFE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PRODUCT_LIFE", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@SEC_LABEL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GRAPHIC_ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TempNum", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SatFatPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CholPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SodPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CarbPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FiberPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CalcPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IronPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitDPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitEPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitCPerc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VitAPerc", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcoh", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SugarAlcohPerc", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Label_ID"].Value = int_Label_Id;
                objSQlComm.Parameters["@ProductID"].Value = int_ProductID;

                objSQlComm.Parameters["@PLU_NUMBER"].Value = str_PLU_NUMBER;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_1"].Value = str_SCALE_DESCRIPTION_1;
                objSQlComm.Parameters["@SCALE_DESCRIPTION_2"].Value = str_SCALE_DESCRIPTION_2;
                objSQlComm.Parameters["@ITEM_TYPE"].Value = str_ITEM_TYPE;
                objSQlComm.Parameters["@FORCE_TARE"].Value = str_FORCE_TARE;
                objSQlComm.Parameters["@INGRED_STATEMENT"].Value = str_INGRED_STATEMENT;
                objSQlComm.Parameters["@SPECIAL_MESSAGE"].Value = str_SPECIAL_MESSAGE;
                objSQlComm.Parameters["@ServSize"].Value = str_ServSize;
                objSQlComm.Parameters["@ServPC"].Value = str_ServPC;
                objSQlComm.Parameters["@Calors"].Value = str_Calors;
                objSQlComm.Parameters["@FatCalors"].Value = str_FatCalors;
                objSQlComm.Parameters["@TotFat"].Value = str_TotFat;
                objSQlComm.Parameters["@SatFat"].Value = str_SatFat;
                objSQlComm.Parameters["@Chol"].Value = str_Chol;
                objSQlComm.Parameters["@Sodium"].Value = str_Sodium;
                objSQlComm.Parameters["@Carbs"].Value = str_Carbs;
                objSQlComm.Parameters["@Fiber"].Value = str_Fiber;
                objSQlComm.Parameters["@Proteins"].Value = str_Proteins;
                objSQlComm.Parameters["@Sugar"].Value = str_Sugar;
                objSQlComm.Parameters["@TransFattyAcid"].Value = str_TransFattyAcid;
                objSQlComm.Parameters["@WEIGHT"].Value = dbl_WEIGHT;
                objSQlComm.Parameters["@TARE_1_S"].Value = dbl_TARE_1_S;
                objSQlComm.Parameters["@TARE_2_O"].Value = dbl_TARE_2_O;

                objSQlComm.Parameters["@BYCOUNT"].Value = int_BYCOUNT;
                objSQlComm.Parameters["@SHELF_LIFE"].Value = int_SHELF_LIFE;
                objSQlComm.Parameters["@PRODUCT_LIFE"].Value = int_PRODUCT_LIFE;
                objSQlComm.Parameters["@SEC_LABEL"].Value = int_SEC_LABEL;
                objSQlComm.Parameters["@GRAPHIC_ID"].Value = int_Graphic_ID;
                objSQlComm.Parameters["@TempNum"].Value = int_TempNum;
                objSQlComm.Parameters["@FatPerc"].Value = int_FatPerc;
                objSQlComm.Parameters["@SatFatPerc"].Value = int_SatFatPerc;
                objSQlComm.Parameters["@CholPerc"].Value = int_CholPerc;
                objSQlComm.Parameters["@SodPerc"].Value = int_SodPerc;
                objSQlComm.Parameters["@CarbPerc"].Value = int_CarbPerc;
                objSQlComm.Parameters["@FiberPerc"].Value = int_FiberPerc;
                objSQlComm.Parameters["@CalcPerc"].Value = int_CalcPerc;
                objSQlComm.Parameters["@IronPerc"].Value = int_IronPerc;
                objSQlComm.Parameters["@VitDPerc"].Value = int_VitDPerc;
                objSQlComm.Parameters["@VitEPerc"].Value = int_VitEPerc;
                objSQlComm.Parameters["@VitCPerc"].Value = int_VitCPerc;
                objSQlComm.Parameters["@VitAPerc"].Value = int_VitAPerc;
                //objSQlComm.Parameters["@SCALE_IP"].Value = str_SCALE_IP;
                //objSQlComm.Parameters["@SCALE_TYPE"].Value = int_SCALE_TYPE;
                //objSQlComm.Parameters["@PORT"].Value = str_PORT;
                //objSQlComm.Parameters["@FILE_LOCATION"].Value = str_FILE_LOCATION;
                objSQlComm.Parameters["@DepartmentID"].Value = int_Dp_ID;
                objSQlComm.Parameters["@Cat_ID"].Value = str_Cat_ID;
                objSQlComm.Parameters["@PriceA"].Value = dbl_PriceA;
                objSQlComm.Parameters["@AD_Price"].Value = dbl_AD_Price;
                //objSQlComm.Parameters["@Mil_Sec"].Value = int_Mil_Sec;
                objSQlComm.Parameters["@SugarAlcoh"].Value = str_SugarAlcoh;
                objSQlComm.Parameters["@SugarAlcohPerc"].Value = int_SugarAlcohPerc;

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

        #region SCALE REPORT

        public DataTable FetchScaleReportData(  int refType, DataTable refdtbl, string pOption1,DataTable pOption1Value,
                                                string pOption2,string pOption2Value,string pOption3,int pOption33, double pOption3Value1,
                                                double pOption3Value2)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";
            string getdtblID = "";
            string getdtblTaxID = "";

            int chkcnt = 0;

            if (refType == 0)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {

                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + dr["ID"].ToString();
                        }
                        chkcnt++;
                    }
                }
            }

            chkcnt = 0;
            if (refType == 1)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["Cat_ID"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + ",'" + dr["Cat_ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblID != "")
            {
                if (refType == 0) strSQL1 = " and b.ID in ( " + getdtblID + " )";
                if (refType == 1) strSQL1 = " and b.Cat_ID in ( " + getdtblID + " )";
            }


            chkcnt = 0;
            if (pOption1 == "Y")
            {
                foreach (DataRow dr in pOption1Value.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckTax"].ToString()))
                    {
                        if (getdtblTaxID == "")
                        {
                            getdtblTaxID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblTaxID = getdtblTaxID + ",'" + dr["ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblTaxID != "")
            {
                strSQL2 = " right outer join TaxMapping t on t.MappingID = p.ID and t.MappingType = 'Product' and "
                        + " t.TaxID in ( " + getdtblTaxID + " )";
            }

            if (pOption2 == "Y")
            {
                strSQL3 = " and p.FoodStampEligible = @param1 ";
            }

            if (pOption3 == "Y")
            {
                if (pOption33 == 0) strSQL4 = " and p.PriceA between @param2 and @param3 ";
                if (pOption33 == 1) strSQL4 = " and p.PriceA > @param2";
                if (pOption33 == 2) strSQL4 = " and p.PriceA < @param2";
            }
            
            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T','B')";

            if (refType == 0)
            {
                strSQLComm =    " select a.*,p.Description, p.PriceA, b.DepartmentID as FilterCode, b.Description as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join dept b on p.DepartmentID = b.ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            if (refType == 1)
            {
                strSQLComm = " select a.*,p.Description,p.PriceA, b.Cat_ID as FilterCode, isnull(b.Name,'') as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join scale_category b on a.Cat_ID = b.Cat_ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pOption2 == "Y")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.Char));
                    objSQlComm.Parameters["@param1"].Value = pOption2Value;
                }

                if (pOption3 == "Y")
                {
                    if (pOption33 == 0)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                        objSQlComm.Parameters.Add(new SqlParameter("@param3", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param3"].Value = pOption3Value2;
                    }

                    if (pOption33 == 1)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }

                    if (pOption33 == 2)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }
                }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ITEM_TYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BYCOUNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WEIGHT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHELF_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCT_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FORCE_TARE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE_1_S", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE_2_O", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description2", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["FilterCode"].ToString(),
                                                   objSQLReader["FilterName"].ToString(),
												   objSQLReader["PLU_NUMBER"].ToString(),
                                                   objSQLReader["ITEM_TYPE"].ToString(),
                                                   objSQLReader["BYCOUNT"].ToString(), 
                                                   Functions.fnDouble(objSQLReader["WEIGHT"].ToString()).ToString("f"),
                                                   objSQLReader["SHELF_LIFE"].ToString(),
                                                   objSQLReader["PRODUCT_LIFE"].ToString(), 
                                                   objSQLReader["FORCE_TARE"].ToString(),
                                                   objSQLReader["TARE_1_S"].ToString(),
                                                   objSQLReader["TARE_2_O"].ToString(), 
                                                   objSQLReader["Description"].ToString(),             
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()).ToString("f"),
                                                   objSQLReader["Scale_Description_1"].ToString(),
                                                   objSQLReader["Scale_Description_2"].ToString() });
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

        public DataTable FetchScaleReportData1(int refType, DataTable refdtbl, string pOption1, DataTable pOption1Value,
                                                string pOption2, string pOption2Value, string pOption3, int pOption33, double pOption3Value1,
                                                double pOption3Value2,int ReportType)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";
            string strSQL5 = "";
            string getdtblID = "";
            string getdtblTaxID = "";

            int chkcnt = 0;

            if (refType == 0)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {

                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + dr["ID"].ToString();
                        }
                        chkcnt++;
                    }
                }
            }

            chkcnt = 0;
            if (refType == 1)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["Cat_ID"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + ",'" + dr["Cat_ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblID != "")
            {
                if (refType == 0) strSQL1 = " and b.ID in ( " + getdtblID + " )";
                if (refType == 1) strSQL1 = " and b.Cat_ID in ( " + getdtblID + " )";
            }


            chkcnt = 0;
            if (pOption1 == "Y")
            {
                foreach (DataRow dr in pOption1Value.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckTax"].ToString()))
                    {
                        if (getdtblTaxID == "")
                        {
                            getdtblTaxID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblTaxID = getdtblTaxID + ",'" + dr["ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblTaxID != "")
            {
                strSQL2 = " right outer join TaxMapping t on t.MappingID = p.ID and t.MappingType = 'Product' and "
                        + " t.TaxID in ( " + getdtblTaxID + " )";
            }

            if (pOption2 == "Y")
            {
                strSQL3 = " and p.FoodStampEligible = @param1 ";
            }

            if (pOption3 == "Y")
            {
                if (pOption33 == 0) strSQL4 = " and p.PriceA between @param2 and @param3 ";
                if (pOption33 == 1) strSQL4 = " and p.PriceA > @param2";
                if (pOption33 == 2) strSQL4 = " and p.PriceA < @param2";
            }

            if (ReportType == 1) strSQL5 = " and a.INGRED_STATEMENT != '' ";
            if (ReportType == 2) strSQL5 = " and a.SPECIAL_MESSAGE != '' ";

            if (refType == 0)
            {
                strSQLComm = " select a.*,p.Description, p.PriceA, b.DepartmentID as FilterCode, b.Description as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join dept b on p.DepartmentID = b.ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            if (refType == 1)
            {
                strSQLComm = " select a.*,p.Description,p.PriceA, b.Cat_ID as FilterCode, isnull(b.Name,'') as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join scale_category b on a.Cat_ID = b.Cat_ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pOption2 == "Y")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.Char));
                    objSQlComm.Parameters["@param1"].Value = pOption2Value;
                }

                if (pOption3 == "Y")
                {
                    if (pOption33 == 0)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                        objSQlComm.Parameters.Add(new SqlParameter("@param3", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param3"].Value = pOption3Value2;
                    }

                    if (pOption33 == 1)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }

                    if (pOption33 == 2)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }
                }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SText", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    if (ReportType == 1)
                    {
                        if (objSQLReader["INGRED_STATEMENT"].ToString() != "")
                        {
                            dtbl.Rows.Add(new object[] {   objSQLReader["FilterCode"].ToString(),
                                                   objSQLReader["FilterName"].ToString(),
												   objSQLReader["PLU_NUMBER"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["INGRED_STATEMENT"].ToString() });
                        }
                    }

                    if (ReportType == 2)
                    {
                        if (objSQLReader["SPECIAL_MESSAGE"].ToString() != "")
                        {
                            dtbl.Rows.Add(new object[] {   objSQLReader["FilterCode"].ToString(),
                                                   objSQLReader["FilterName"].ToString(),
												   objSQLReader["PLU_NUMBER"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                  objSQLReader["SPECIAL_MESSAGE"].ToString() });
                        }
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


        public DataTable FetchScaleReportData2(int refType, DataTable refdtbl, string pOption1, DataTable pOption1Value,
                                               string pOption2, string pOption2Value, string pOption3, int pOption33, double pOption3Value1,
                                               double pOption3Value2)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";
            string getdtblID = "";
            string getdtblTaxID = "";

            int chkcnt = 0;

            if (refType == 0)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {

                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + dr["ID"].ToString();
                        }
                        chkcnt++;
                    }
                }
            }

            chkcnt = 0;
            if (refType == 1)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["Cat_ID"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + ",'" + dr["Cat_ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblID != "")
            {
                if (refType == 0) strSQL1 = " and b.ID in ( " + getdtblID + " )";
                if (refType == 1) strSQL1 = " and b.Cat_ID in ( " + getdtblID + " )";
            }


            chkcnt = 0;
            if (pOption1 == "Y")
            {
                foreach (DataRow dr in pOption1Value.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckTax"].ToString()))
                    {
                        if (getdtblTaxID == "")
                        {
                            getdtblTaxID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblTaxID = getdtblTaxID + ",'" + dr["ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblTaxID != "")
            {
                strSQL2 = " right outer join TaxMapping t on t.MappingID = p.ID and t.MappingType = 'Product' and "
                        + " t.TaxID in ( " + getdtblTaxID + " )";
            }

            if (pOption2 == "Y")
            {
                strSQL3 = " and p.FoodStampEligible = @param1 ";
            }

            if (pOption3 == "Y")
            {
                if (pOption33 == 0) strSQL4 = " and p.PriceA between @param2 and @param3 ";
                if (pOption33 == 1) strSQL4 = " and p.PriceA > @param2";
                if (pOption33 == 2) strSQL4 = " and p.PriceA < @param2";
            }

            if (refType == 0)
            {
                strSQLComm = " select a.*,p.Description, p.PriceA, b.DepartmentID as FilterCode, b.Description as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join dept b on p.DepartmentID = b.ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            if (refType == 1)
            {
                strSQLComm = " select a.*,p.Description,p.PriceA, b.Cat_ID as FilterCode, isnull(b.Name,'') as FilterName "
                              + " from Scale_Product a left outer join Product p on p.ID = a.ProductID "
                              + " left outer join scale_category b on a.Cat_ID = b.Cat_ID "
                              + strSQL2 + " where (1 = 1) and p.ProductStatus = 'Y' " + strSQL1 + strSQL3 + strSQL4
                              + " order by FilterCode ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pOption2 == "Y")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.Char));
                    objSQlComm.Parameters["@param1"].Value = pOption2Value;
                }

                if (pOption3 == "Y")
                {
                    if (pOption33 == 0)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                        objSQlComm.Parameters.Add(new SqlParameter("@param3", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param3"].Value = pOption3Value2;
                    }

                    if (pOption33 == 1)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }

                    if (pOption33 == 2)
                    {
                        objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Float));
                        objSQlComm.Parameters["@param2"].Value = pOption3Value1;
                    }
                }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU_NUMBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
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
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["FilterCode"].ToString(),
                                                   objSQLReader["FilterName"].ToString(),
												   objSQLReader["PLU_NUMBER"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["TempNum"].ToString(),
                                                   objSQLReader["ServSize"].ToString(), 
                                                   objSQLReader["ServPC"].ToString(),
                                                   objSQLReader["Calors"].ToString(), 
                                                   objSQLReader["FatCalors"].ToString(),
                                                   objSQLReader["TotFat"].ToString(),
                                                   Functions.fnInt32(objSQLReader["FatPerc"].ToString()) == 0 ? "" : objSQLReader["FatPerc"].ToString() + "%", 
                                                   objSQLReader["SatFat"].ToString(),  
                                                   Functions.fnInt32(objSQLReader["SatFatPerc"].ToString()) == 0 ? "" : objSQLReader["SatFatPerc"].ToString() + "%", 
                                                   objSQLReader["Chol"].ToString(),  
                                                   Functions.fnInt32(objSQLReader["CholPerc"].ToString()) == 0 ? "" : objSQLReader["CholPerc"].ToString() + "%", 
                                                   objSQLReader["Sodium"].ToString(),  
                                                   Functions.fnInt32(objSQLReader["SodPerc"].ToString()) == 0 ? "" : objSQLReader["SodPerc"].ToString() + "%", 
                                                   objSQLReader["Carbs"].ToString(),  
                                                   Functions.fnInt32(objSQLReader["CarbPerc"].ToString()) == 0 ? "" : objSQLReader["CarbPerc"].ToString() + "%", 
                                                    objSQLReader["Fiber"].ToString(),  
                                                   Functions.fnInt32(objSQLReader["FiberPerc"].ToString()) == 0 ? "" : objSQLReader["FiberPerc"].ToString() + "%", 
                                                   objSQLReader["Sugar"].ToString(),
                                                   objSQLReader["Proteins"].ToString(),
                                                   Functions.fnInt32(objSQLReader["CalcPerc"].ToString()) == 0 ? "" : objSQLReader["CalcPerc"].ToString() + "%", 
                                                   Functions.fnInt32(objSQLReader["IronPerc"].ToString()) == 0 ? "" : objSQLReader["IronPerc"].ToString() + "%", 
                                                   Functions.fnInt32(objSQLReader["VitDPerc"].ToString()) == 0 ? "" : objSQLReader["VitDPerc"].ToString() + "%", 
                                                   Functions.fnInt32(objSQLReader["VitEPerc"].ToString()) == 0 ? "" : objSQLReader["VitEPerc"].ToString() + "%", 
                                                   Functions.fnInt32(objSQLReader["VitCPerc"].ToString()) == 0 ? "" : objSQLReader["VitCPerc"].ToString() + "%", 
                                                   Functions.fnInt32(objSQLReader["VitAPerc"].ToString()) == 0 ? "" : objSQLReader["VitAPerc"].ToString() + "%", 
                                                   objSQLReader["TransFattyAcid"].ToString()
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

        public DataTable FetchScalePriceChangeExceptionData(int refType, DataTable refdtbl, string configDateFormat)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";
            string getdtblID = "";
            string getdtblTaxID = "";

            int chkcnt = 0;

            if (refType == 0)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {

                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + dr["ID"].ToString();
                        }
                        chkcnt++;
                    }
                }
            }

            chkcnt = 0;
            if (refType == 1)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["Cat_ID"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + ",'" + dr["Cat_ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblID != "")
            {
                if (refType == 0) strSQL1 = " and b.ID in ( " + getdtblID + " )";
                if (refType == 1) strSQL1 = " and b.Cat_ID in ( " + getdtblID + " )";
            }


            
            if (refType == 0)
            {
                strSQLComm =    " select m.PLU,m.CurrentRetailPrice,m.ChangedRetailPrice,r.ReasonText,isnull(e.FirstName,'') as UserF,isnull(e.LastName,'') as UserL,m.LastChangedOn,"
                              + " p.Description,  b.DepartmentID as FilterCode, b.Description as FilterName "
                              + " from Scale_RetailPriceChange m left outer join Scale_Product a on m.PLURecID = a.Row_ID "
                             + " left outer join Scale_PriceChangeReasons r on m.RefReason = r.ID "
                             + " left outer join Product p on p.ID = a.ProductID "
                             + " left outer join Employee e on m.LastChangedBy = e.ID "
                              + " left outer join dept b on p.DepartmentID = b.ID "
                              + " where (1 = 1) " + strSQL1
                              + " order by FilterCode, m.LastChangedOn desc ";
            }

            if (refType == 1)
            {
                strSQLComm = " select m.PLU,m.CurrentRetailPrice,m.ChangedRetailPrice,r.ReasonText,isnull(e.FirstName,'') as UserF,isnull(e.LastName,'') as UserL,m.LastChangedOn,"
                             + " p.Description,  b.Cat_ID as FilterCode, isnull(b.Name,'') as FilterName "
                             + " from Scale_RetailPriceChange m left outer join Scale_Product a on m.PLURecID = a.Row_ID "
							 + " left outer join Scale_PriceChangeReasons r on m.RefReason = r.ID "
                             + " left outer join Product p on p.ID = a.ProductID "
                             + " left outer join Employee e on m.LastChangedBy = e.ID "
                             + " left outer join scale_category b on a.Cat_ID = b.Cat_ID "
                             + " where (1 = 1) " + strSQL1
                             + " order by FilterCode, m.LastChangedOn desc ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

               

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChangeTo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("User", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Reason", System.Type.GetType("System.String"));

                string user = "";
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["UserF"].ToString() == "") && (objSQLReader["UserL"].ToString() == ""))
                    {
                        user = "ADMIN";
                    }
                    else
                    {
                        if (objSQLReader["UserF"].ToString() != "") user = objSQLReader["UserF"].ToString();
                        if (objSQLReader["UserL"].ToString() != "") user = user == "" ? objSQLReader["UserL"].ToString() : user + " " + objSQLReader["UserL"].ToString();
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["FilterCode"].ToString(),
                                                   objSQLReader["FilterName"].ToString(),
												   objSQLReader["PLU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["CurrentRetailPrice"].ToString()).ToString("f"),
                                                   Functions.fnDouble(objSQLReader["ChangedRetailPrice"].ToString()).ToString("f"),
                                                   user,
                                                   Functions.fnDate(objSQLReader["LastChangedOn"].ToString()).ToString(configDateFormat),
                                                   objSQLReader["ReasonText"].ToString()});
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

        public int ScaleLoadReload(int val1,int val2)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_scale_load_reload";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@pDept", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@pDept"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@pDept"].Value = val1;

                objSQlComm.Parameters.Add(new SqlParameter("@pScaleID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@pScaleID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@pScaleID"].Value = val2;

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

        #region SCALE POS

        public DataTable FetchScaleDepartments()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, Description from Dept where ScaleFlag = 'Y' and ScaleScreenVisible = 'Y' order by ScaleDisplayOrder desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Description"].ToString()});
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

        public DataTable FetchScaleCategoryByDepartment(int intDID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Category where Department_ID = @ID and AddToPOSScreen = 'Y' order by PosDisplayOrder desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intDID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Cat_ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PosDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSItemFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSScreen", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["Cat_ID"].ToString(), 
                                                   objSQLReader["Name"].ToString(),
                                                   objSQLReader["PosDisplayOrder"].ToString(),
                                                    objSQLReader["POSBackground"].ToString(),
                                                    objSQLReader["POSScreenStyle"].ToString(),
                                                    objSQLReader["POSFontColor"].ToString(),
                                                    objSQLReader["POSFontType"].ToString(),
                                                    objSQLReader["POSFontSize"].ToString(),
                                                    objSQLReader["POSItemFontColor"].ToString(),
                                                    objSQLReader["IsBold"].ToString(),
                                                    objSQLReader["IsItalics"].ToString(),
                                                    objSQLReader["POSItemFontColor"].ToString(),
                                                    objSQLReader["AddToPOSScreen"].ToString() });
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

        public DataTable FetchScaleItemsforScaleCategory(string CatID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm =      " select distinct p.ID,p.SKU,p.Description,p.ProductType,p.DisplayStockinPOS,p.AllowZeroStock,p.QtyOnHand,p.ScaleBackground,p.ScaleScreenColor, "
                            + " p.ScaleScreenStyle,p.ScaleFontType,p.ScaleFontSize,p.ScaleFontColor,p.ScaleIsBold,p.ScaleIsItalics,p.LinkSKU,p.BreakPackRatio,p.CaseQty,s.ScaleDisplayOrder, "
                            + " c.POSFontColor as ScaleCatBackGround, c.POSItemFontColor as ScaleCatFont from Product p "
                            + " Left Outer Join Scale_Product s on s.ProductID = p.ID Left Outer Join Scale_Category c on c.Cat_ID = s.cat_id where s.cat_id = @CategoryID "
                            + " and p.ProductStatus = 'Y' and p.LinkSKU >= 0 and p.ProductType = 'W' and p.AddtoScaleScreen = 'Y'  order by s.ScaleDisplayOrder desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@CategoryID"].Value = CatID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DESCRIPTION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DISPLAYSTOCKINPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ALLOWZEROSTOCK", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QTYONHAND", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("POSBACKGROUND", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSSCREENCOLOR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSSCREENSTYLE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTSIZE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTCOLOR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ISBOLD", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ISITALICS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LINKSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BREAKPACKRATIO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PACKQTY", System.Type.GetType("System.String"));
                int intItemID = 0;
                int intItemQty = 0;
                double dbQty = 0;

                while (objSQLReader.Read())
                {
                    intItemID = 0;
                    intItemQty = 0;
                    dbQty = 0;

                    if (objSQLReader["ID"].ToString() != "")
                        intItemID = Functions.fnInt32(objSQLReader["ID"].ToString());

                    if (objSQLReader["QTYONHAND"].ToString() != "")
                        dbQty = Functions.fnDouble(objSQLReader["QTYONHAND"].ToString());

                    intItemQty = Functions.fnInt32(dbQty);
                    dtbl.Rows.Add(new object[] {intItemID,
                                                objSQLReader["SKU"].ToString(),
												objSQLReader["DESCRIPTION"].ToString(),
                                                objSQLReader["PRODUCTTYPE"].ToString(),
                                                objSQLReader["DISPLAYSTOCKINPOS"].ToString(),
                                                objSQLReader["ALLOWZEROSTOCK"].ToString(),
                                                intItemQty,
                                                objSQLReader["ScaleBackground"].ToString(),
                                                objSQLReader["ScaleScreenColor"].ToString(),
                                                objSQLReader["ScaleScreenStyle"].ToString(),
                                                objSQLReader["ScaleFontType"].ToString(),
                                                objSQLReader["ScaleFontSize"].ToString(),
                                                objSQLReader["ScaleFontColor"].ToString(),
                                                objSQLReader["ScaleIsBold"].ToString(),
                                                objSQLReader["ScaleIsItalics"].ToString(),
                                                objSQLReader["LINKSKU"].ToString(),
                                                objSQLReader["BREAKPACKRATIO"].ToString(),
                                                objSQLReader["CASEQTY"].ToString()});
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

        public DataTable FetchAllScaleItemsforScaleCategory(string CatID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select distinct p.ID,p.SKU,p.Description,p.ProductType,p.DisplayStockinPOS,p.AllowZeroStock,p.QtyOnHand,p.ScaleBackground,p.ScaleScreenColor, "
                           + " p.ScaleScreenStyle,p.ScaleFontType,p.ScaleFontSize,p.ScaleFontColor,p.ScaleIsBold,p.ScaleIsItalics,p.LinkSKU,p.BreakPackRatio,p.CaseQty,s.ScaleDisplayOrder, "
                           + " c.POSFontColor as ScaleCatBackGround, c.POSItemFontColor as ScaleCatFont from Product p Left Outer Join Scale_Product s on s.ProductID = p.ID "
                           + " Left Outer Join Scale_Category c on c.Cat_ID = s.cat_id where s.cat_id = @CategoryID "
                           + " and p.ProductStatus = 'Y' and p.LinkSKU >= 0 and p.ProductType = 'W' order by s.ScaleDisplayOrder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@CategoryID"].Value = CatID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DESCRIPTION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DISPLAYSTOCKINPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ALLOWZEROSTOCK", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QTYONHAND", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("POSBACKGROUND", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSSCREENCOLOR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSSCREENSTYLE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTSIZE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFONTCOLOR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ISBOLD", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ISITALICS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LINKSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BREAKPACKRATIO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PACKQTY", System.Type.GetType("System.String"));
                int intItemID = 0;
                int intItemQty = 0;
                double dbQty = 0;

                while (objSQLReader.Read())
                {
                    intItemID = 0;
                    intItemQty = 0;
                    dbQty = 0;

                    if (objSQLReader["ID"].ToString() != "")
                        intItemID = Functions.fnInt32(objSQLReader["ID"].ToString());

                    if (objSQLReader["QTYONHAND"].ToString() != "")
                        dbQty = Functions.fnDouble(objSQLReader["QTYONHAND"].ToString());

                    intItemQty = Functions.fnInt32(dbQty);
                    dtbl.Rows.Add(new object[] {intItemID,
                                                objSQLReader["SKU"].ToString(),
												objSQLReader["DESCRIPTION"].ToString(),
                                                objSQLReader["PRODUCTTYPE"].ToString(),
                                                objSQLReader["DISPLAYSTOCKINPOS"].ToString(),
                                                objSQLReader["ALLOWZEROSTOCK"].ToString(),
                                                intItemQty,
                                                objSQLReader["ScaleBackground"].ToString(),
                                                objSQLReader["ScaleScreenColor"].ToString(),
                                                objSQLReader["ScaleScreenStyle"].ToString(),
                                                objSQLReader["ScaleFontType"].ToString(),
                                                objSQLReader["ScaleFontSize"].ToString(),
                                                objSQLReader["ScaleFontColor"].ToString(),
                                                objSQLReader["ScaleIsBold"].ToString(),
                                                objSQLReader["ScaleIsItalics"].ToString(),
                                                objSQLReader["LINKSKU"].ToString(),
                                                objSQLReader["BREAKPACKRATIO"].ToString(),
                                                objSQLReader["CASEQTY"].ToString()});
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

        public DataTable FetchPLUfromScaleItems(int pRefID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select plu_number,row_id from scale_product where productid = @pid order by plu_number ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@pid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@pid"].Value = pRefID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecordID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["plu_number"].ToString(),
                                                objSQLReader["row_id"].ToString()});
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

        public DataTable FetchDetalisFromScalePLU(int pRefID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select s.plu_number,isnull(s.scale_description_1,'') as desc_1,isnull(s.scale_description_2,'') as desc_2, "
                       + " isnull(s.ingred_statement,'') as ingred,isnull(s.special_message,'') as spl, s.product_life, s.shelf_life, "
                       + " s.weight,s.tare_1_s, s.tare_2_o,s.force_tare, s.productid,s.item_type,s.bycount, s.pl_juliandate, s.sl_juliandate, "
                       + " s.TempNum as v1, isnull(s.ServSize,'') as v2, isnull(s.ServPC,'') as v3, isnull(s.Calors,'') as v4,  "
                       + " isnull(s.FatCalors,'') as v5, isnull(s.TotFat,'') as v6, isnull(s.FatPerc,'0') as v7, "
                       + " isnull(s.SatFat,'') as v8, isnull(s.SatFatPerc,'0') as v9, isnull(s.Chol,'') as v10, "
                       + " isnull(s.CholPerc,'0') as v11, isnull(s.Sodium,'') as v12, isnull(s.SodPerc,'0') as v13, "
                       + " isnull(s.Carbs,'') as v14, isnull(s.CarbPerc,'0') as v15, isnull(s.Fiber,'') as v16, "
                       + " isnull(s.FiberPerc,'0') as v17, isnull(s.Sugar,'') as v18, isnull(s.Proteins,'') as v19, "
                       + " isnull(s.CalcPerc,'0') as v20, isnull(s.IronPerc,'0') as v21, isnull(s.VitDPerc,'0') as v22, "
                       + " isnull(s.VitEPerc,'0') as v23, isnull(s.VitCPerc,'0') as v24, isnull(s.VitAPerc,'0') as v25, "
                       + " isnull(s.TransFattyAcid,'') as v26, isnull(s.SugarAlcoh,'') as v27, isnull(s.SugarAlcohPerc,'0') as v28, "
                       + " isnull(p.SKU,'') as PSku, isnull(p.UPC,'') as PUpc, isnull(p.Description,'') as PName,  isnull(p.Cost,0) as PCost, isnull(p.PriceA,0) as PPrice,"
                       + " isnull(m.MappingID,0) as Mapping_Ref, isnull(l.LabelFormat,0) as LFormat_Ref, "
                       + " isnull(m1.MappingID,0) as Mapping_Ref1, isnull(g.GraphicArtID,0) as Graphic_Ref,"
                       + " isnull(s.EXT2,'0') as RecipeMapping_Ref, isnull(l1.LabelFormat,0) as RecpFormat_Ref, "
                       + " isnull(s.EXT1,'0') as ExternPrnMapping_Ref, isnull(l2.LabelFormat,0) as ExternPrnFormat_Ref from scale_product s "
                       + " left outer join Product p on p.ID = s.productid left outer join Scale_Mapping m on m.ProductID = s.row_id and "
                       + " m.ScaleMappingType = 'Second Label' left outer join Second_label_Types l on l.label_Id = m.MappingID "
                       + " left outer join Scale_Mapping m1 on m1.ProductID = s.row_id and m1.ScaleMappingType = 'Graphic' "
                       + " left outer join Scale_Graphics g on g.Graphic_Id = m1.MappingID "
                       + " left outer join Second_label_Types l1 on l1.label_Id = isnull(s.EXT2,0) "
                        + " left outer join Second_label_Types l2 on l2.label_Id = isnull(s.EXT1,0) where s.row_id = @rid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@rid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@rid"].Value = pRefID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("PRODUCTID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTCOST", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTPRICE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DESC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DESC2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("INGREDIENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RECIPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCT_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHELF_LIFE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TARE2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FORCETARE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPRICE", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("WEIGHT", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TPRICE", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("WEIGHTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BYCOUNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PL_JULIAN", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL_JULIAN", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TEMPLATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SERVING_SIZE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SERVINGS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALORIES", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALORIES_FROM_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TOTAL_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TOTAL_FAT_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SATURATED_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SATURATED_FAT_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TRANS_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SUGAR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PROTEIN", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CHOLESTEROL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CHOLESTEROL_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SODIUM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SODIUM_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CARBOHYDRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CARBOHYDRATE_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DIETARY_FIBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DIETARY_FIBER_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_A", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_C", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_D", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_E", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALCIUM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IRON", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SUGAR_ALCOHOL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SUGAR_ALCOHOL_PERCENT", System.Type.GetType("System.String"));

                dtbl.Columns.Add("SECOND_LABEL_REF", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABEL_FORMAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GRAPHICS_REF", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GRAPHICS_ART", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RECIPE_REF", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RECIPE_FORMAT", System.Type.GetType("System.String"));

                dtbl.Columns.Add("EXTERNPRN_REF", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EXTERNPRN_FORMAT", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["productid"].ToString(),
                                                objSQLReader["PSku"].ToString(),
                                                objSQLReader["PUpc"].ToString(),
                                                objSQLReader["PName"].ToString(),
                                                objSQLReader["PCost"].ToString(),
                                                objSQLReader["PPrice"].ToString(),
                                                objSQLReader["plu_number"].ToString(),
                                                objSQLReader["desc_1"].ToString(),
                                                objSQLReader["desc_2"].ToString(),
                                                objSQLReader["ingred"].ToString(),
                                                objSQLReader["spl"].ToString(),
                                                objSQLReader["product_life"].ToString(),
                                                objSQLReader["shelf_life"].ToString(),
                                                objSQLReader["tare_1_s"].ToString(),
                                                objSQLReader["tare_2_o"].ToString(),
                                                objSQLReader["force_tare"].ToString(),
                                                0,Functions.fnDouble(objSQLReader["weight"].ToString()),0,
                                                objSQLReader["item_type"].ToString(),
                                                objSQLReader["bycount"].ToString(),
                                                objSQLReader["pl_juliandate"].ToString(),
                                                objSQLReader["sl_juliandate"].ToString(),
                                                objSQLReader["v1"].ToString(),
                                                objSQLReader["v2"].ToString(),
                                                objSQLReader["v3"].ToString(),
                                                objSQLReader["v4"].ToString(),
                                                objSQLReader["v5"].ToString(),
                                                objSQLReader["v6"].ToString(),
                                                objSQLReader["v7"].ToString(),
                                                objSQLReader["v8"].ToString(),
                                                objSQLReader["v9"].ToString(),
                                                objSQLReader["v26"].ToString(),
                                                objSQLReader["v18"].ToString(),
                                                objSQLReader["v19"].ToString(),
                                                objSQLReader["v10"].ToString(),
                                                objSQLReader["v11"].ToString(),
                                                objSQLReader["v12"].ToString(),
                                                objSQLReader["v13"].ToString(),
                                                objSQLReader["v14"].ToString(),
                                                objSQLReader["v15"].ToString(),
                                                objSQLReader["v16"].ToString(),
                                                objSQLReader["v17"].ToString(),
                                                objSQLReader["v25"].ToString(),
                                                objSQLReader["v24"].ToString(),
                                                objSQLReader["v22"].ToString(),
                                                objSQLReader["v23"].ToString(),
                                                objSQLReader["v20"].ToString(),
                                                objSQLReader["v21"].ToString(),
                                                objSQLReader["v27"].ToString(),
                                                objSQLReader["v28"].ToString(),

                                                objSQLReader["Mapping_Ref"].ToString(),
                                                objSQLReader["LFormat_Ref"].ToString(),
                                                objSQLReader["Mapping_Ref1"].ToString(),
                                                objSQLReader["Graphic_Ref"].ToString(),
                                                objSQLReader["RecipeMapping_Ref"].ToString(),
                                                objSQLReader["RecpFormat_Ref"].ToString(),
                                                objSQLReader["ExternPrnMapping_Ref"].ToString(),
                                                objSQLReader["ExternPrnFormat_Ref"].ToString()
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

        public DataTable FetchNutriDetalisFromScalePLU(int pRefID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select TempNum as v1, isnull(ServSize,'') as v2, isnull(ServPC,'') as v3, isnull(Calors,'') as v4,  "
                       + " isnull(FatCalors,'') as v5, isnull(TotFat,'') as v6, isnull(FatPerc,'0') as v7, "
                       + " isnull(SatFat,'') as v8, isnull(SatFatPerc,'0') as v9, isnull(Chol,'') as v10, "
                       + " isnull(CholPerc,'0') as v11, isnull(Sodium,'') as v12, isnull(SodPerc,'0') as v13, "
                       + " isnull(Carbs,'') as v14, isnull(CarbPerc,'0') as v15, isnull(Fiber,'') as v16, "
                       + " isnull(FiberPerc,'0') as v17, isnull(Sugar,'') as v18, isnull(Proteins,'') as v19, "
                       + " isnull(CalcPerc,'0') as v20, isnull(IronPerc,'0') as v21, isnull(VitDPerc,'0') as v22, "
                       + " isnull(VitEPerc,'0') as v23, isnull(VitCPerc,'0') as v24, isnull(VitAPerc,'0') as v25, "
                       + " isnull(TransFattyAcid,'') as v26, isnull(SugarAlcoh,'') as v27, isnull(SugarAlcohPerc,'0') as v28 "
                       + " from scale_product where row_id = @rid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@rid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@rid"].Value = pRefID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("TEMPLATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SERVING_SIZE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SERVINGS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALORIES", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALORIES_FROM_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TOTAL_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TOTAL_FAT_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SATURATED_FAT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SATURATED_FAT_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TRANS_FAT", System.Type.GetType("System.String"));

                dtbl.Columns.Add("SUGAR", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PROTEIN", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CHOLESTEROL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CHOLESTEROL_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SODIUM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SODIUM_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CARBOHYDRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CARBOHYDRATE_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DIETARY_FIBER", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DIETARY_FIBER_PERCENT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_A", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_C", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_D", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VITAMIN_E", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALCIUM", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IRON", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SUGAR_ALCOHOL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SUGAR_ALCOHOL_PERCENT", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["v1"].ToString(),
                                                objSQLReader["v2"].ToString(),
                                                objSQLReader["v3"].ToString(),
                                                objSQLReader["v4"].ToString(),
                                                objSQLReader["v5"].ToString(),
                                                objSQLReader["v6"].ToString(),
                                                objSQLReader["v7"].ToString(),
                                                objSQLReader["v8"].ToString(),
                                                objSQLReader["v9"].ToString(),
                                                objSQLReader["v26"].ToString(),
                                                objSQLReader["v18"].ToString(),
                                                objSQLReader["v19"].ToString(),
                                                objSQLReader["v10"].ToString(),
                                                objSQLReader["v11"].ToString(),
                                                objSQLReader["v12"].ToString(),
                                                objSQLReader["v13"].ToString(),
                                                objSQLReader["v14"].ToString(),
                                                objSQLReader["v15"].ToString(),
                                                objSQLReader["v16"].ToString(),
                                                objSQLReader["v17"].ToString(),
                                                objSQLReader["v25"].ToString(),
                                                objSQLReader["v24"].ToString(),
                                                objSQLReader["v22"].ToString(),
                                                objSQLReader["v23"].ToString(),
                                                objSQLReader["v20"].ToString(),
                                                objSQLReader["v21"].ToString(),
                                                objSQLReader["v27"].ToString(),
                                                objSQLReader["v28"].ToString()
                                                
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

        public int GetScaleRefIDWithSKU(string strSKUrecd)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(s.row_id,0) as rcdid from scale_product s left outer join product p on p.ID = s.ProductID where p.sku = @SKU "
                              + " and s.Item_Type in ('By Count', 'Fixed Weight', 'Random Weight') ";

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

        public int FetchRowIDFromPLU_Dept(string pPLU, int pDeptID)
        {
            int rval = 0;
            string strSQLComm = "";

            strSQLComm = " select isnull(s.row_id,0) as val from scale_product s left outer join product p on p.ID = s.ProductID and p.ProductStatus = 'Y' "
                       + " where p.DepartmentID = @d and s.plu_number = @p ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@d", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@d"].Value = pDeptID;
            objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p"].Value = pPLU;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                
                while (objSQLReader.Read())
                {
                    rval = Functions.fnInt32(objSQLReader["val"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return rval;
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

        public int FetchRowIDFromPLU(string pPLU)
        {
            int rval = 0;
            string strSQLComm = "";

            strSQLComm = " select isnull(s.row_id,0) as val from scale_product s left outer join product p on p.ID = s.ProductID and p.ProductStatus = 'Y' "
                       + " where s.plu_number = @p ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p"].Value = pPLU;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    rval = Functions.fnInt32(objSQLReader["val"].ToString());
                    break;
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return rval;
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

        public int GetItemNameFromPLU(string pPLU)
        {
            int rval = 0;
            string strSQLComm = "";

            strSQLComm = " select isnull(s.row_id,0) as val from scale_product s left outer join product p on p.ID = s.ProductID and p.ProductStatus = 'Y' "
                       + " where s.plu_number = @p ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p"].Value = pPLU;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    rval = Functions.fnInt32(objSQLReader["val"].ToString());
                    break;
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return rval;
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

        public DataTable FetchScaleItemsFromSearch(string pSearchString)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select distinct p.ID,p.SKU,p.Description,p.ProductType,p.DisplayStockinPOS,p.AllowZeroStock,p.QtyOnHand,p.ScaleBackground,p.ScaleScreenColor, "
                            + " p.ScaleScreenStyle,p.ScaleFontType,p.ScaleFontSize,p.ScaleFontColor,p.ScaleIsBold,p.ScaleIsItalics,p.LinkSKU,p.BreakPackRatio,p.CaseQty from "
                            + " scale_product s left outer join product p on p.id = s.productid where "
                            + " p.ProductStatus = 'Y' and p.DepartmentID in ( select d.ID from Dept d where d.ScaleFlag = 'Y') and p.Description like '%" + pSearchString  + "%' and p.LinkSKU >= 0 "
                            + " and p.ProductType = 'W' order by p.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DESCRIPTION", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DISPLAYSTOCKINPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ALLOWZEROSTOCK", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QTYONHAND", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("POSBackground", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSFontColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsItalics", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LINKSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BREAKPACKRATIO", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PACKQTY", System.Type.GetType("System.String"));
                int intItemID = 0;
                int intItemQty = 0;
                double dbQty = 0;

                while (objSQLReader.Read())
                {
                    intItemID = 0;
                    intItemQty = 0;
                    dbQty = 0;

                    if (objSQLReader["ID"].ToString() != "")
                        intItemID = Functions.fnInt32(objSQLReader["ID"].ToString());

                    if (objSQLReader["QTYONHAND"].ToString() != "")
                        dbQty = Functions.fnDouble(objSQLReader["QTYONHAND"].ToString());

                    intItemQty = Functions.fnInt32(dbQty);
                    dtbl.Rows.Add(new object[] {intItemID,
                                                objSQLReader["SKU"].ToString(),
												objSQLReader["DESCRIPTION"].ToString(),
                                                objSQLReader["PRODUCTTYPE"].ToString(),
                                                objSQLReader["DISPLAYSTOCKINPOS"].ToString(),
                                                objSQLReader["ALLOWZEROSTOCK"].ToString(),
                                                intItemQty,
                                                objSQLReader["ScaleBackground"].ToString(),
                                                objSQLReader["ScaleScreenColor"].ToString(),
                                                objSQLReader["ScaleScreenStyle"].ToString(),
                                                objSQLReader["ScaleFontType"].ToString(),
                                                objSQLReader["ScaleFontSize"].ToString(),
                                                objSQLReader["ScaleFontColor"].ToString(),
                                                objSQLReader["ScaleIsBold"].ToString(),
                                                objSQLReader["ScaleIsItalics"].ToString(),
                                                objSQLReader["LINKSKU"].ToString(),
                                                objSQLReader["BREAKPACKRATIO"].ToString(),
                                                objSQLReader["CASEQTY"].ToString()});
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

        public string InsertLabelLayout(int RefID, string x)
        {
            string strSQLComm = "";
            strSQLComm = " insert into LabelLayout (RecordID,ReportInfo)values(@ID,@R)";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objSQlComm.Parameters.Add(new SqlParameter("@R", System.Data.SqlDbType.Xml));
                objSQlComm.Parameters["@R"].Value = x;

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

        public string UpdateLabelLayout(int RefID, string x)
        {
            string strSQLComm = "";
            strSQLComm = " update LabelLayout set ReportInfo = @R where RecordID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objSQlComm.Parameters.Add(new SqlParameter("@R", System.Data.SqlDbType.Xml));
                objSQlComm.Parameters["@R"].Value = x;

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

        public int IsExistLabelLayout(int RefID)
        {
            int val = 0;
            string strSQLComm = " select count(*) as rcnt from labellayout where recordid = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["rcnt"].ToString());
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

                return val;
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

        public string GetLabelLayout(int RefID)
        {
            string strnm = "";
            string strSQLComm = " select reportinfo from labellayout where recordid = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strnm = objsqlReader["reportinfo"].ToString();
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

                return strnm;
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

                return "";
            }
        }

        public int IsExistSecondLabelMapping(int RefID)
        {
            int val = 0;

            string strSQLComm = " select * from Scale_Mapping where ScaleMappingType = 'Second Label' and productid = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                while (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["MappingID"].ToString());
                    }
                    catch
                    {
                    }
                    break;
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
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

        public int IsExistExternalPrinterMapping(int RefID)
        {
            int val = 0;

            string strSQLComm = " select isnull(EXT1,'') as val from Scale_Product where row_id = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                while (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["val"].ToString());
                    }
                    catch
                    {
                    }
                    break;
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
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

        public int IsExistRecipePrinterMapping(int RefID)
        {
            int val = 0;

            string strSQLComm = " select isnull(EXT2,'') as val from Scale_Product where row_id = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                while (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["val"].ToString());
                    }
                    catch
                    {
                    }
                    break;
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
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

        public int IsExistLabelFormat(int RefID)
        {
            int val = 0;
            string strSQLComm = " select LabelFormat from Second_Label_Types where Label_ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["LabelFormat"].ToString());
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

                return val;
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

        public int IsExistGraphicMapping(int RefID)
        {
            int val = 0;
            string strSQLComm = " select * from Scale_Mapping where ScaleMappingType = 'Graphic' and productid = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                while (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["MappingID"].ToString());
                    }
                    catch
                    {
                    }
                    break;
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
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

        public int IsExistGraphicArt(int RefID)
        {
            int val = 0;
            string strSQLComm = " select GraphicArtID from Scale_Graphics where Graphic_ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["GraphicArtID"].ToString());
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

                return val;
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

        public byte[] GetGraphicArt(int pID)
        {

            byte[] b = null;

            string strSQLComm = " select GraphicArt from GraphicArts where ID = @ID ";

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
                    b = (byte[])objSQLReader["GraphicArt"];
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

        public string InsertScaleRetailPriceChange(int RefPLU, int RefReason,string pPLU, double Price1, double Price2, int pUser)
        {
            string strSQLComm = "";
            strSQLComm = " insert into Scale_RetailPriceChange (PLURecID,PLU,CurrentRetailPrice,ChangedRetailPrice,RefReason,LastChangedBy,LastChangedOn) "
                       + " values(@PLURecID,@PLU,@CurrentRetailPrice,@ChangedRetailPrice,@RefReason,@LastChangedBy,@LastChangedOn)"
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

                objSQlComm.Parameters.Add(new SqlParameter("@PLURecID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PLURecID"].Value = RefPLU;

                objSQlComm.Parameters.Add(new SqlParameter("@PLU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PLU"].Value = pPLU;
                
                objSQlComm.Parameters.Add(new SqlParameter("@CurrentRetailPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@CurrentRetailPrice"].Value = Price1;

                objSQlComm.Parameters.Add(new SqlParameter("@ChangedRetailPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@ChangedRetailPrice"].Value = Price2;

                objSQlComm.Parameters.Add(new SqlParameter("@RefReason", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefReason"].Value = RefReason;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LastChangedBy"].Value = pUser;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
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

        #region Label Format

        public DataTable FetchNonAddedDefaultLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select LabelSlNo, LabelDescription from LabelSL where Added = 'N' order by LabelSlNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("LabelSlNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelDescription", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["LabelSlNo"].ToString(),
                                                   objSQLReader["LabelDescription"].ToString() });
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

        public DataTable FetchLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select FormatID, FormatName, IsDefaultFormat, IsProductionLabel, FormatType from LabelFormats order by FormatName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Default", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsProductionLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sort", System.Type.GetType("System.Int32"));
                while (objSQLReader.Read())
                {
                    int isort = 0;
                    if (objSQLReader["FormatType"].ToString() == "Scale") isort = 1;
                    if (objSQLReader["FormatType"].ToString() == "Recipe") isort = 2;
                    if (objSQLReader["FormatType"].ToString() == "Shelf Tag") isort = 3;
                    if (objSQLReader["FormatType"].ToString() == "Markdown") isort = 4;

                    dtbl.Rows.Add(new object[] {   objSQLReader["FormatID"].ToString(),
                                                   objSQLReader["FormatName"].ToString(),
                                                   objSQLReader["IsDefaultFormat"].ToString() == "Y" ? "Yes" : "No",
                                                   objSQLReader["IsProductionLabel"].ToString(),
                                                   objSQLReader["FormatType"].ToString(),
                                                   isort });
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

        public DataTable FetchScaleNRecipeLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select FormatID, FormatName, IsDefaultFormat, IsProductionLabel, FormatType from LabelFormats where (1 = 1) and (FormatType = 'Scale' or formatType = 'Recipe' ) order by FormatName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Default", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsProductionLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sort", System.Type.GetType("System.Int32"));
                while (objSQLReader.Read())
                {
                    int isort = 0;
                    if (objSQLReader["FormatType"].ToString() == "Scale") isort = 1;
                    if (objSQLReader["FormatType"].ToString() == "Recipe") isort = 2;
                    if (objSQLReader["FormatType"].ToString() == "Production List") isort = 3;

                    dtbl.Rows.Add(new object[] {   objSQLReader["FormatID"].ToString(),
                                                   objSQLReader["FormatName"].ToString(),
                                                   objSQLReader["IsDefaultFormat"].ToString() == "Y" ? "Yes" : "No",
                                                   objSQLReader["IsProductionLabel"].ToString(),
                                                   objSQLReader["FormatType"].ToString(),
                                                   isort });
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

        public DataTable FetchScaleLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select FormatID, FormatName from LabelFormats where FormatType = 'Scale' order by FormatName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["FormatID"].ToString(),
                                                   objSQLReader["FormatName"].ToString()});
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

        public DataTable FetchShelfTagLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select FormatID, FormatName from LabelFormats where FormatType = 'Shelf Tag' order by FormatName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["FormatID"].ToString(),
                                                   objSQLReader["FormatName"].ToString()});
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

        public string InsertLabelFormat(string pName, string pDefault, string pFilePath, string pType)
        {
            byte[] file;
            using (var stream = new FileStream(pFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            string strSQLComm = "";
            strSQLComm = " insert into LabelFormats (FormatName,IsDefaultFormat,LabelFormat,FormatType) values(@FormatName,@IsDefaultFormat,@LabelFormat,@FormatType)"
                       + " select @@IDENTITY as FormatID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FormatName"].Value = pName;

                objSQlComm.Parameters.Add(new SqlParameter("@IsDefaultFormat", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@IsDefaultFormat"].Value = pDefault;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.VarBinary));
                objSQlComm.Parameters["@LabelFormat"].Value = file;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@FormatType"].Value = pType;
               
                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["FormatID"]);
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

        public string UpdateLabelFormat(string pName, string pDefault, string pFilePath, int pID)
        {
            byte[] file;
            using (var stream = new FileStream(pFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            string strSQLComm = "";
            strSQLComm = " update LabelFormats set FormatName=@FormatName,IsDefaultFormat=@IsDefaultFormat,LabelFormat=@LabelFormat where FormatID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
           
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FormatName"].Value = pName;

                objSQlComm.Parameters.Add(new SqlParameter("@IsDefaultFormat", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@IsDefaultFormat"].Value = pDefault;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.VarBinary));
                objSQlComm.Parameters["@LabelFormat"].Value = file;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;

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

        public string UpdateDefaultLabelFormatSL(int pLink)
        {
            string strSQLComm = "";
            strSQLComm = " update LabelSL set Added = 'Y' where LabelSlNo = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pLink;

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

        public string UpdateLabelFormat(string pFilePath, int pLink)
        {
            byte[] file;
            using (var stream = new FileStream(pFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            string strSQLComm = "";
            strSQLComm = " update LabelFormats set LabelFormat = @LabelFormat where FormatID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.VarBinary));
                objSQlComm.Parameters["@LabelFormat"].Value = file;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pLink;

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

        public string UpdateLabelFormatName(string pName, int pLink)
        {
            string strSQLComm = "";
            strSQLComm = " update LabelFormats set FormatName = @FormatName where FormatID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@FormatName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@FormatName"].Value = pName;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pLink;

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

        public DataTable FetchLabelFormatData(int pID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select FormatName, IsDefaultFormat, FormatType from LabelFormats where FormatID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsDefaultFormat", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatType", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["FormatName"].ToString(),
                                                objSQLReader["IsDefaultFormat"].ToString(),
                                                objSQLReader["FormatType"].ToString()});
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

        public string GetLabelFormatLayout(int RefID)
        {
            string strnm = "";
            string strSQLComm = " select LabelFormat from LabelFormats where FormatID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = RefID;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strnm = objsqlReader["LabelFormat"].ToString();
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

                return strnm;
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

                return "";
            }
        }

        public string DeleteLabelFormat(int DeleteID)
        {
            string strSQLComm = " Delete from LabelFormats where FormatID = @ID";

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

        public int DuplicateLabelFormat(string pVal,string pType)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from LabelFormats where FormatName=@P and FormatType = @T ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@P", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@P"].Value = pVal;

                objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@T"].Value = pType;

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


        public string ResetProductionLabelTag()
        {
            string strSQLComm = " update LabelFormats set IsProductionLabel = 'N' ";

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

        public string SetProductionLabelTag(int DeleteID)
        {
            string strSQLComm = " update LabelFormats set IsProductionLabel = 'Y' where FormatID = @ID";

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

        public int IsExistProductionLabelFormat()
        {
            int val = 0;
            string strSQLComm = " select FormatID from LabelFormats where IsProductionLabel = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["FormatID"].ToString());
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

                return val;
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

        public string DeleteProductionBatchPrint()
        {
            string strSQLComm = " Delete from ProductionPrintBatch";

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

        public string DeleteProductionPrintLabel(int DeleteID)
        {
            string strSQLComm = " delete from ProductionPrintBatch where ID = @ID";

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

        public string DeleteShelfTagBatchPrint()
        {
            string strSQLComm = " Delete from ShelfTagPrintBatch";

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

        public string DeleteShelfTagPrintLabel(int DeleteID)
        {
            string strSQLComm = " delete from ShelfTagPrintBatch where ID = @ID";

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

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        public bool UpdateStock(string pTerminal, string pTranType)
        {
            try
            {
                SaveComm = new SqlCommand(" ", sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                SaveComm.Transaction = this.SaveTran;
                AdjustStockRecords(SaveComm, pTerminal, pTranType);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustStockRecords(SqlCommand objSQlComm, string pTerminal, string pTranType)
        {
            try
            {
                if (dtblStockData == null) return;
                foreach (DataRow dr in dtblStockData.Rows)
                {
                    int SJ = Functions.AddStockJournal(objSQlComm, "Stock In", pTranType, "Y", Functions.fnInt32(dr["ProductID"].ToString()), intLoginUserID, Functions.fnDouble(dr["Qty"].ToString()), Functions.fnDouble(dr["Cost"].ToString()),
                                        dr["DocNo"].ToString(), pTerminal, Functions.fnDate(dr["DocDate"].ToString()), DateTime.Now, "");

                }
                dtblStockData.Dispose();
            }
            catch (Exception e)
            {
                
            }
        }


        public bool UpdateStock(string pTerminal, string pTranType, int refProduct, double refCost, double refQty, string refDoc )
        {
            try
            {
                SaveComm = new SqlCommand(" ", sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                SaveComm.Transaction = this.SaveTran;
                AdjustStockRecords(SaveComm, pTerminal, pTranType, refProduct, refCost, refQty, refDoc);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustStockRecords(SqlCommand objSQlComm, string pTerminal, string pTranType, int refProduct, double refCost, double refQty, string refDoc)
        {
            try
            {
                int SJ = Functions.AddStockJournal(objSQlComm, "Stock In", pTranType, "Y", refProduct, intLoginUserID, refQty, refCost,
                                         refDoc, pTerminal, DateTime.Today.Date, DateTime.Now, "");
            }
            catch (Exception e)
            {

            }
        } 

        #endregion

        #region Graphic Art

        public string InsertGraphicArt(int pArtNo,Image pArt)
        {
            string strSQLComm = "";
            strSQLComm = " insert into GraphicArts (ArtNo,GraphicArt) values(@ArtNo,@GraphicArt)"
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

                objSQlComm.Parameters.Add(new SqlParameter("@ArtNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ArtNo"].Value = pArtNo;

                objSQlComm.Parameters.Add(new SqlParameter("@GraphicArt", System.Data.SqlDbType.Image));

                ImageConverter converter = new ImageConverter();
                byte[] newBA = (byte[])converter.ConvertTo(pArt, typeof(byte[]));

                objSQlComm.Parameters["@GraphicArt"].Value = newBA;
                
                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
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

        public string UpdateGraphicArt(int pArtNo, Image pArt, int pID)
        {
            string strSQLComm = "";
            strSQLComm = " update GraphicArts set ArtNo=@ArtNo,GraphicArt=@GraphicArt where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@ArtNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ArtNo"].Value = pArtNo;

                objSQlComm.Parameters.Add(new SqlParameter("@GraphicArt", System.Data.SqlDbType.Image));

                ImageConverter converter = new ImageConverter();
                byte[] newBA = (byte[])converter.ConvertTo(pArt, typeof(byte[]));

                objSQlComm.Parameters["@GraphicArt"].Value = newBA;

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

        public int DuplicateGraphicArt(int clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from GraphicArts where ArtNo=@ClassID ";

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

        public DataTable FetchGraphicArt()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ArtNo, GraphicArt from GraphicArts order by ArtNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ArtNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArt", typeof(byte[]));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ArtNo"].ToString(),
                                                   (byte[])objSQLReader["GraphicArt"] });
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

        public DataTable ShowGraphicArt(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ArtNo, GraphicArt from GraphicArts where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ArtNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GraphicArt", typeof(byte[]));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ArtNo"].ToString(),
                                                   (byte[])objSQLReader["GraphicArt"] });
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

        public int GetGraphicArtNo(int pID)
        {
            int intCount = 0;
            string strSQLComm = " select ArtNo from GraphicArts where ID = @ID ";

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
                        intCount = Functions.fnInt32(objsqlReader["ArtNo"]);
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

        public int GetGraphicArtID(int pArtNo)
        {
            int intCount = 0;
            string strSQLComm = " select ID from GraphicArts where ArtNo = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pArtNo;

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

        public string DeleteGraphicArt(int DeleteID)
        {
            string strSQLComm = " Delete from GraphicArts where ID = @ID";

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

        #endregion

        #region Price Change Reason

        public string InsertReason()
        {
            string strSQLComm = "";
            strSQLComm = " insert into Scale_PriceChangeReasons (ReasonText,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values(@ReasonText,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)"
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

                objSQlComm.Parameters.Add(new SqlParameter("@ReasonText", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ReasonText"].Value = strReasonText;

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
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

        public string UpdateReason()
        {
            string strSQLComm = "";

            strSQLComm = " update Scale_PriceChangeReasons set ReasonText=@ReasonText,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ReasonText", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ReasonText"].Value = strReasonText;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;


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

        public int DuplicateReason(string pVal)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Scale_PriceChangeReasons where ReasonText =@Txt ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@Txt", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Txt"].Value = pVal;

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

        public DataTable FetchReason()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ReasonText from Scale_PriceChangeReasons order by ReasonText ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReasonText", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ReasonText"].ToString()});
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

        public DataTable FetchReasonDesc()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ReasonText from Scale_PriceChangeReasons order by ReasonText desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReasonText", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ReasonText"].ToString()});
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

        public DataTable ShowReason(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ReasonText from Scale_PriceChangeReasons where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReasonText", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ReasonText"].ToString()});
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

        public string DeleteReason(int DeleteID)
        {
            string strSQLComm = " Delete from Scale_PriceChangeReasons where ID = @ID";

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

        #endregion


        #region Markdown

        public DataTable LookupMarkdownLabelFormat()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select FormatID, FormatName from LabelFormats where FormatType = 'Markdown' order by FormatName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FormatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FormatName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["FormatID"].ToString(),
                                                   objSQLReader["FormatName"].ToString()});
                }
                
                dtbl.Rows.Add(new object[] { "0", "(None)" });

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

        public DataTable FetchScaleInfoForMarkdownPrint(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select SCALE_DESCRIPTION_1, SCALE_DESCRIPTION_2 from Scale_Product where row_id = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SCALE_DESCRIPTION_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALE_DESCRIPTION_2", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   
                                                   objSQLReader["SCALE_DESCRIPTION_1"].ToString(),
												   objSQLReader["SCALE_DESCRIPTION_2"].ToString()});
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


        public void GetScaleMarkdownOnPrint(int PID, DateTime PDATE, ref int AD)
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_getscalemarkdown";
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
                objSQlComm.Parameters["@ID"].Value = PID;

                objSQlComm.Parameters.Add(new SqlParameter("@AppDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@AppDate"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@AppDate"].Value = PDATE;

                objSQlComm.Parameters.Add(new SqlParameter("@AppDisc", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@AppDisc"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();

                AD = Functions.fnInt32(objSQlComm.Parameters["@AppDisc"].Value);
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


        public string InsertMarkdownPrintInfo(int pProductRefID, int pScaleRefID, int pDeptRefID, int pMarkdownRefID, int pPrepared_By_RefID, string pSKU,
                                                string pPLU, string pProductDescription, string pScale_Description_1, string pScale_Description_2, string pScaleDepartment,
                                                string pPrepared_By, double pRetailPrice, double pMarkDownPrice, double pNewPrice, double pSavings, DateTime dtApplicableDate)
        {
            string strSQLComm = " insert into Scale_Markdown_Print(ProductRefID,ScaleRefID,DeptRefID,MarkdownRefID,SKU,PLU,ProductDescription,"
                              + " Scale_Description_1,Scale_Description_2,ScaleDepartment,RetailPrice,MarkDownPrice,NewPrice,Savings,Prepared_By_RefID,Prepared_By,ApplicableDate,CreatedOn,CreatedBy ) "
                              + " values ( @ProductRefID,@ScaleRefID,@DeptRefID,@MarkdownRefID,@SKU,@PLU,@ProductDescription, "
                               + " @Scale_Description_1,@Scale_Description_2,@ScaleDepartment,@RetailPrice,@MarkDownPrice,@NewPrice,@Savings,@Prepared_By_RefID,@Prepared_By,@ApplicableDate,getdate(),@CreatedBy ) ";
                             

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductRefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleRefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DeptRefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MarkdownRefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PLU", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductDescription", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Description_1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Scale_Description_2", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDepartment", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RetailPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@MarkDownPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NewPrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Savings", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Prepared_By_RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Prepared_By", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ApplicableDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ProductRefID"].Value = pProductRefID;
                objSQlComm.Parameters["@ScaleRefID"].Value = pScaleRefID;
                objSQlComm.Parameters["@DeptRefID"].Value = pDeptRefID;
                objSQlComm.Parameters["@MarkdownRefID"].Value = pMarkdownRefID;
                objSQlComm.Parameters["@SKU"].Value = pSKU;
                objSQlComm.Parameters["@PLU"].Value = pPLU;
                objSQlComm.Parameters["@ProductDescription"].Value = pProductDescription;
                objSQlComm.Parameters["@Scale_Description_1"].Value = pScale_Description_1;
                objSQlComm.Parameters["@Scale_Description_2"].Value = pScale_Description_2;
                objSQlComm.Parameters["@ScaleDepartment"].Value = pScaleDepartment;
                objSQlComm.Parameters["@RetailPrice"].Value = pRetailPrice;
                objSQlComm.Parameters["@MarkDownPrice"].Value = pMarkDownPrice;
                objSQlComm.Parameters["@NewPrice"].Value = pNewPrice;
                objSQlComm.Parameters["@Savings"].Value = pSavings;
                objSQlComm.Parameters["@Prepared_By_RefID"].Value = pPrepared_By_RefID;
                objSQlComm.Parameters["@Prepared_By"].Value = pPrepared_By;
                objSQlComm.Parameters["@ApplicableDate"].Value = dtApplicableDate;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;

                objSQlComm.ExecuteNonQuery();

                
                objSQLTran.Commit();
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


        #region Digi Import

        public DataTable FetchDigiScaleExport_InitialData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select p.priceA, p.SKU, s.PLU_Number, s.SCALE_DESCRIPTION_1, s.SCALE_DESCRIPTION_2, s.ITEM_TYPE, s.BYCOUNT, s.SHELF_LIFE, "
                              + " s.TARE_1_S from Scale_Product s left outer join Product p on p.ID = s.ProductID left outer join Dept d on p.DepartmentID = d.ID "
                              + " where p.ProductType = 'W' and d.ID in (select DEPARTMENT_ID from Scale_Addresses where scale_type = (select id from Scale_Types "
                              + " where Scale_Type = 'Digi')) order by s.PLU_Number ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SellBy", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Content", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Symbol", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Line1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Line2", System.Type.GetType("System.String"));

                string unitprice = "";
                string tare = "";
                string symbol = "0";
                while (objSQLReader.Read())
                {
                    unitprice = "";
                    if (Functions.fnDouble(objSQLReader["priceA"].ToString()) == 0)
                    {
                        unitprice = "0";
                    }
                    else
                    {
                        unitprice = objSQLReader["priceA"].ToString().Replace(".",String.Empty);
                        unitprice = unitprice.Substring(0, (unitprice.Length - 1));
                        unitprice = unitprice.TrimStart('0');
                    }
                    tare = "";
                    if (Functions.fnDouble(objSQLReader["TARE_1_S"].ToString()) == 0)
                    {
                        tare = "0";
                    }
                    else
                    {
                        tare = objSQLReader["TARE_1_S"].ToString().Replace(".", String.Empty);
                        tare = tare.Trim(new char[] {'0'});
                    }

                    symbol = "0";
                    if (objSQLReader["ITEM_TYPE"].ToString() == "Random Weight")
                    {
                    }

                    if (objSQLReader["ITEM_TYPE"].ToString() == "Fixed Weight")
                    {
                        
                    }

                    if (objSQLReader["ITEM_TYPE"].ToString() == "By Count")
                    {
                        symbol = "2";
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   unitprice,
                                                   objSQLReader["PLU_Number"].ToString(),
                                                   tare,
                                                   objSQLReader["SHELF_LIFE"].ToString(),
                                                   objSQLReader["BYCOUNT"].ToString(),
                                                   symbol,
                                                   objSQLReader["SCALE_DESCRIPTION_1"].ToString(),
                                                   objSQLReader["SCALE_DESCRIPTION_2"].ToString()
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



        public DataTable FetchDigiScaleExport_ChangeData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select priceA, AD_Price, SKU, PLU_Number, SCALE_DESCRIPTION_1, SCALE_DESCRIPTION_2, ITEM_TYPE, BYCOUNT, SHELF_LIFE, "
                              + " TARE_1_S from Scale_com where scale_type = (select id from Scale_Types where Scale_Type = 'Digi') order by PLU_Number ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UnitPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tare", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SellBy", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Content", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Symbol", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Line1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Line2", System.Type.GetType("System.String"));

                string unitprice = "";
                string tare = "";
                string symbol = "0";
                while (objSQLReader.Read())
                {
                    string tempprice = objSQLReader["priceA"].ToString();
                    double ad_price = Functions.fnDouble(objSQLReader["AD_Price"].ToString());
                    if (ad_price > 0) tempprice = objSQLReader["AD_Price"].ToString();
                    unitprice = "";
                    if (Functions.fnDouble(tempprice) == 0)
                    {
                        unitprice = "0";
                    }
                    else
                    {
                        unitprice = tempprice.ToString().Replace(".", String.Empty);
                        unitprice = unitprice.Substring(0, (unitprice.Length - 1));
                        unitprice = unitprice.TrimStart('0');
                    }
                    tare = "";
                    if (Functions.fnDouble(objSQLReader["TARE_1_S"].ToString()) == 0)
                    {
                        tare = "0";
                    }
                    else
                    {
                        tare = objSQLReader["TARE_1_S"].ToString().Replace(".", String.Empty);
                        tare = tare.Trim(new char[] { '0' });
                    }

                    symbol = "0";
                    if (objSQLReader["ITEM_TYPE"].ToString() == "Random Weight")
                    {
                    }

                    if (objSQLReader["ITEM_TYPE"].ToString() == "Fixed Weight")
                    {

                    }

                    if (objSQLReader["ITEM_TYPE"].ToString() == "By Count")
                    {
                        symbol = "2";
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   unitprice,
                                                   objSQLReader["PLU_Number"].ToString(),
                                                   tare,
                                                   objSQLReader["SHELF_LIFE"].ToString(),
                                                   objSQLReader["BYCOUNT"].ToString(),
                                                   symbol,
                                                   objSQLReader["SCALE_DESCRIPTION_1"].ToString(),
                                                   objSQLReader["SCALE_DESCRIPTION_2"].ToString()
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

        public void DigiScaleExport_DeleteChangeData()
        {


            string strSQLComm = " delete from Scale_com where scale_type = (select id from Scale_Types where Scale_Type = 'Digi') ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);


            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.ExecuteNonQuery();


                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
        }

        #endregion
    }
}

