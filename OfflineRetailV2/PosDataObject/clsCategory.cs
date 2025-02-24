/*
 purpose : Data for Item Category
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Category
    {
        #region definining private variables

        private SqlConnection sqlConn;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strGeneralCode;
        private string strGeneralDesc;
        private string strFoodStamp;
        private int intPosDisplayOrder;
        private int intMaxItemsforPOS;
        private string strPOSBackground;
        private string strPOSScreenStyle;
        private string strPOSFontType;
        private string strPOSFontColor;
        private string strPOSItemFontColor;
        private int intPOSFontSize;
        private string strIsBold;
        private string strIsItalics;
        private string strPOSScreenColor;

        private int intParentCategory;
        private int intParentCategoryLevel;

        private string strAddToPOSScreen;
       

        private DataTable dblTaxDataTable;

        private string strPrintBarCode;
        private string strNoPriceOnLabel;
        private string strFoodStampEligibleForProduct;
        private string strScaleBarCode;
        private string strAllowZeroStock;
        private string strDisplayStockinPOS;
        private string strProductStatus;
        private string strRepairPromptForTag;
        private string strNonDiscountable;
        private int intQtyToPrint;
        private int intLabelType;
        private int intMinimumAge;
        private string strBookerCategoryID = "";
        private string strBookerDeptID = "";
        private string strImportFrom = "";

        #endregion

        #region definining public variables

        public int ParentCategory
        {
            get { return intParentCategory; }
            set { intParentCategory = value; }
        }

        public int ParentCategoryLevel
        {
            get { return intParentCategoryLevel; }
            set { intParentCategoryLevel = value; }
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

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DataTable TaxDataTable
        {
            get { return dblTaxDataTable; }
            set { dblTaxDataTable = value; }
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

        public string FoodStamp
        {
            get { return strFoodStamp; }
            set { strFoodStamp = value; }
        }

        public int PosDisplayOrder
        {
            get { return intPosDisplayOrder; }
            set { intPosDisplayOrder = value; }
        }

        public int MaxItemsforPOS
        {
            get { return intMaxItemsforPOS; }
            set { intMaxItemsforPOS = value; }
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

        public string BookerCategoryID
        {
            get { return strBookerCategoryID; }
            set { strBookerCategoryID = value; }
        }
        public string BookerDeptID
        {
            get { return strBookerDeptID; }
            set { strBookerDeptID = value; }
        }
        public string ImportFrom
        {
            get { return strImportFrom; }
            set { strImportFrom = value; }
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

        public string FoodStampEligibleForProduct
        {
            get { return strFoodStampEligibleForProduct; }
            set { strFoodStampEligibleForProduct = value; }
        }

        public string ScaleBarCode
        {
            get { return strScaleBarCode; }
            set { strScaleBarCode = value; }
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

        public string ProductStatus
        {
            get { return strProductStatus; }
            set { strProductStatus = value; }
        }

        public string RepairPromptForTag
        {
            get { return strRepairPromptForTag; }
            set { strRepairPromptForTag = value; }
        }

        public string NonDiscountable
        {
            get { return strNonDiscountable; }
            set { strNonDiscountable = value; }
        }

        #endregion

        public int GetParentCategoryLevel(int gid)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(ParentCategoryLevel,0) as lvl from Category where id = @grp ";

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
                        intCount = Functions.fnInt32(objSQLReader["lvl"].ToString());
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

        public DataTable LookupCategory( int NoRefCategory, bool bActive)
        {
            string sqlFilter = "";
            string sqlFilter1 = "";
            

            DataTable dtbl = new DataTable();
            if (NoRefCategory != 0)
            {
                sqlFilter = " and c.ID <> " + NoRefCategory.ToString();
            }
            if (bActive) sqlFilter1 = " and c.Enabled = 'Y' ";

          

            string strSQLComm = " select c.ID,c.ParentCategory,c.CategoryID,c.Description from Category c "
                              + " left outer join Category p on c.ParentCategory = p.ID where (1=1)  " + sqlFilter + sqlFilter1 + " order by c.ParentCategoryLevel, c.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("ParentCategory", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   Convert.ToInt32(objSQLReader["ID"].ToString()),
                                                   Convert.ToInt32(objSQLReader["ParentCategory"].ToString()),
                                                   objSQLReader["CategoryID"].ToString(),
                                                   objSQLReader["Description"].ToString()
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
                    strError = InsertData(SaveComm);
                }
                else
                {
                    strError = UpdateData(SaveComm);
                }

                if (strError == "")
                    DeleteTaxParts(SaveComm);
                if (strError == "")
                    strError = SaveTaxParts(SaveComm);

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

        #region Insert Data

        public string InsertData(SqlCommand objSQlComm)
        {
            string strSQLComm = " insert into Category( CategoryID, Description, FoodStampEligible,PosDisplayOrder,POSBackground, "
                              + " POSScreenStyle, POSFontType, POSFontSize, POSFontColor, IsBold, IsItalics,MaxItemsforPOS, POSScreenColor,"
                              + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,POSItemFontColor,AddToPOSScreen, "
                              + " PrintBarCode,NoPriceOnLabel,FoodStampEligibleForProduct,ScaleBarCode,AllowZeroStock,DisplayStockinPOS,"
                              + " ProductStatus,RepairPromptForTag,NonDiscountable,QtyToPrint,LabelType,MinimumAge, BookerCategoryID, BookerDeptID, ImportFrom,ParentCategory,ParentCategoryLevel) "
                              + " values ( @CategoryID,@Description, @FoodStampEligible,@PosDisplayOrder,@POSBackground, @POSScreenStyle, "
                              + " @POSFontType, @POSFontSize, @POSFontColor, @IsBold, @IsItalics,@MaxItemsforPOS, @POSScreenColor,"
                              + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn,@POSItemFontColor,@AddToPOSScreen, "
                              + " @PrintBarCode,@NoPriceOnLabel,@FoodStampEligibleForProduct,@ScaleBarCode,@AllowZeroStock,@DisplayStockinPOS,"
                              + " @ProductStatus,@RepairPromptForTag,@NonDiscountable,@QtyToPrint,@LabelType,@MinimumAge, @BookerCategoryID, @BookerDeptID, @ImportFrom,@ParentCategory,@ParentCategoryLevel) "
                              + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PosDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MaxItemsforPOS", System.Data.SqlDbType.Int));
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
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligibleForProduct", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BookerCategoryID", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@BookerDeptID", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ImportFrom", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParentCategory", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ParentCategory"].Value = intParentCategory;

                objSQlComm.Parameters.Add(new SqlParameter("@ParentCategoryLevel", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ParentCategoryLevel"].Value = intParentCategoryLevel;

                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@FoodStampEligibleForProduct"].Value = strFoodStampEligibleForProduct;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;
                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;

                objSQlComm.Parameters["@CategoryID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStamp;
                objSQlComm.Parameters["@PosDisplayOrder"].Value = intPosDisplayOrder;
                objSQlComm.Parameters["@MaxItemsforPOS"].Value = intMaxItemsforPOS;
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
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@BookerCategoryID"].Value = strBookerCategoryID;
                objSQlComm.Parameters["@BookerDeptID"].Value = strBookerDeptID;
                objSQlComm.Parameters["@ImportFrom"].Value = strImportFrom;

                
                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
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

        #region Update Data

        public string UpdateData(SqlCommand objSQlComm)
        {
            string strSQLComm = " update Category set CategoryID=@CategoryID,Description=@Description,FoodStampEligible=@FoodStampEligible,"
                              + " PosDisplayOrder = @PosDisplayOrder,MaxItemsforPOS = @MaxItemsforPOS, POSScreenColor = @POSScreenColor,"
                              + " POSBackground=@POSBackground, POSScreenStyle=@POSScreenStyle, POSItemFontColor=@POSItemFontColor,"
                              + " AddToPOSScreen=@AddToPOSScreen,POSFontType=@POSFontType,POSFontSize=@POSFontSize, "
                              + " POSFontColor=@POSFontColor, IsBold=@IsBold, IsItalics=@IsItalics,"
                              + " PrintBarCode=@PrintBarCode,NoPriceOnLabel=@NoPriceOnLabel,FoodStampEligibleForProduct=@FoodStampEligibleForProduct,"
                              + " ScaleBarCode=@ScaleBarCode,AllowZeroStock=@AllowZeroStock,DisplayStockinPOS=@DisplayStockinPOS,"
                              + " ProductStatus=@ProductStatus,RepairPromptForTag=@RepairPromptForTag,NonDiscountable=@NonDiscountable,"
                              + " QtyToPrint=@QtyToPrint,LabelType=@LabelType,MinimumAge=@MinimumAge, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,ParentCategory=@ParentCategory,ParentCategoryLevel=@ParentCategoryLevel where ID = @ID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PosDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MaxItemsforPOS", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSBackground", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSScreenStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsBold", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsItalics", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@POSItemFontColor", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@AddToPOSScreen", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@PrintBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NoPriceOnLabel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligibleForProduct", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleBarCode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AllowZeroStock", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayStockinPOS", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairPromptForTag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@NonDiscountable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyToPrint", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumAge", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@ParentCategory", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ParentCategory"].Value = intParentCategory;

                objSQlComm.Parameters.Add(new SqlParameter("@ParentCategoryLevel", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ParentCategoryLevel"].Value = intParentCategoryLevel;

                objSQlComm.Parameters["@PrintBarCode"].Value = strPrintBarCode;
                objSQlComm.Parameters["@NoPriceOnLabel"].Value = strNoPriceOnLabel;
                objSQlComm.Parameters["@FoodStampEligibleForProduct"].Value = strFoodStampEligibleForProduct;
                objSQlComm.Parameters["@ScaleBarCode"].Value = strScaleBarCode;
                objSQlComm.Parameters["@AllowZeroStock"].Value = strAllowZeroStock;
                objSQlComm.Parameters["@DisplayStockinPOS"].Value = strDisplayStockinPOS;
                objSQlComm.Parameters["@ProductStatus"].Value = strProductStatus;
                objSQlComm.Parameters["@RepairPromptForTag"].Value = strRepairPromptForTag;
                objSQlComm.Parameters["@NonDiscountable"].Value = strNonDiscountable;
                objSQlComm.Parameters["@QtyToPrint"].Value = intQtyToPrint;
                objSQlComm.Parameters["@LabelType"].Value = intLabelType;
                objSQlComm.Parameters["@MinimumAge"].Value = intMinimumAge;

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@CategoryID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStamp;
                objSQlComm.Parameters["@PosDisplayOrder"].Value = intPosDisplayOrder;
                objSQlComm.Parameters["@MaxItemsforPOS"].Value = intMaxItemsforPOS;
                objSQlComm.Parameters["@POSScreenColor"].Value = strPOSScreenColor;
                objSQlComm.Parameters["@POSBackground"].Value = strPOSBackground;
                objSQlComm.Parameters["@POSScreenStyle"].Value = strPOSScreenStyle;
                objSQlComm.Parameters["@POSFontType"].Value = strPOSFontType;
                objSQlComm.Parameters["@POSFontSize"].Value = intPOSFontSize;
                objSQlComm.Parameters["@POSFontColor"].Value = strPOSFontColor;
                objSQlComm.Parameters["@IsBold"].Value = strIsBold;
                objSQlComm.Parameters["@IsItalics"].Value = strIsItalics;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@POSItemFontColor"].Value = strPOSItemFontColor;
                objSQlComm.Parameters["@AddToPOSScreen"].Value = strAddToPOSScreen;


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

        public string UpdateItemFontColor()
        {
            string strSQLComm = " update Product set POSFontColor=@POSItemFontColor where CategoryID=@CategoryID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@POSItemFontColor", System.Data.SqlDbType.VarChar));
                
                objSQlComm.Parameters["@CategoryID"].Value = intID;
                objSQlComm.Parameters["@POSItemFontColor"].Value = strPOSItemFontColor;

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

        public string DeleteTaxParts(SqlCommand objSQlComm)
        {
            string strSQLComm = " Delete from TaxMapping where MappingType = @MappingType and MappingID = @MappingID";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@MappingType"].Value = "Category";
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

        public string InsertProductTax(SqlCommand objSQlComm, int TaxID)
        {
            string strSQLComm = " insert into TaxMapping(TaxID, MappingType, MappingID,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                + " values ( @TaxID, @MappingType, @MappingID,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";


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
                objSQlComm.Parameters["@MappingType"].Value = "Category";
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

        #region Fetchdata

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            //string strSQLComm = " select ID,CategoryID,Description,PosDisplayOrder, MaxItemsforPOS, "
            //                  + " POSScreenColor, POSBackground, POSScreenStyle,AddToPOSScreen from Category order by PosDisplayOrder ";

            string strSQLComm = " select c.ID,c.CategoryID,c.Description,c.FoodStampEligible, c.PosDisplayOrder,c.MaxItemsforPOS, c.ParentCategory,"
                             + " isnull(p.Description,'') as ParentCategoryName,c.POSScreenColor, c.POSBackground, c.POSScreenStyle,c.AddToPOSScreen  " 
                             + "from Category c left outer join Category p on c.ParentCategory = p.ID  order by c.ParentCategoryLevel, c.PosDisplayOrder ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParentCategory", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PosDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MaxItemsforPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSScreenColor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParentCategoryName", System.Type.GetType("System.String"));

                string strAddPOS = "";
                string strFood = "";
                string strStyle = "";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        strFood = "Yes";
                    }
                    else
                    {
                        strFood = "No";
                    }

                    if (objSQLReader["AddToPOSScreen"].ToString() == "Y")
                    {
                        strAddPOS = "Yes";
                    }
                    else
                    {
                        strAddPOS = "No";
                    }

                    if (objSQLReader["POSBackground"].ToString() == "Color")
                    {
                        if ((objSQLReader["POSScreenColor"].ToString() == "0") ||
                            (objSQLReader["POSScreenColor"].ToString() == ""))
                        {
                            strStyle = "Color - Default";
                        }
                        else
                        {
                            strStyle = "Color - " + objSQLReader["POSScreenColor"].ToString();
                        }
                    }
                    else
                    {
                        if (objSQLReader["POSScreenStyle"].ToString() == "")
                        {
                            strStyle = "Skin  - Default";
                        }
                        else
                        {
                            strStyle = "Skin  - " + objSQLReader["POSScreenStyle"].ToString();
                        }
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                        Convert.ToInt32(objSQLReader["ParentCategory"].ToString()),

                                                   objSQLReader["CategoryID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   strFood,
                                                   objSQLReader["PosDisplayOrder"].ToString(),
                                                   objSQLReader["MaxItemsforPOS"].ToString(),
                                                   objSQLReader["POSScreenColor"].ToString(),
                                                   
                                                   strStyle,strAddPOS,objSQLReader["ParentCategoryName"].ToString()});
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

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from category where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PosDisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MaxItemsforPOS", System.Type.GetType("System.String"));
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

                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligibleForProduct", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AllowZeroStock", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayStockinPOS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairPromptForTag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonDiscountable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParentCategory", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["CategoryID"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                objSQLReader["FoodStampEligible"].ToString(),
                                                objSQLReader["PosDisplayOrder"].ToString(),
                                                objSQLReader["MaxItemsforPOS"].ToString(),
                                                objSQLReader["POSBackground"].ToString(),
                                                objSQLReader["POSScreenStyle"].ToString(),
                                                objSQLReader["POSScreenColor"].ToString(),
                                                objSQLReader["POSFontType"].ToString(),
                                                objSQLReader["POSFontSize"].ToString(),
                                                objSQLReader["POSFontColor"].ToString(),
                                                objSQLReader["IsBold"].ToString(),
                                                objSQLReader["IsItalics"].ToString(),
                                                objSQLReader["POSItemFontColor"].ToString(),
                                                objSQLReader["AddToPOSScreen"].ToString(),
                                                objSQLReader["PrintBarCode"].ToString(),
                                                objSQLReader["NoPriceOnLabel"].ToString(),
                                                objSQLReader["FoodStampEligibleForProduct"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString(),
                                                objSQLReader["AllowZeroStock"].ToString(),
                                                objSQLReader["DisplayStockinPOS"].ToString(),
                                                objSQLReader["ProductStatus"].ToString(),
                                                objSQLReader["RepairPromptForTag"].ToString(),
                                                objSQLReader["NonDiscountable"].ToString(),
                                                objSQLReader["QtyToPrint"].ToString(),
                                                objSQLReader["LabelType"].ToString(),
                                                objSQLReader["MinimumAge"].ToString(),
                                                objSQLReader["ParentCategory"].ToString()
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
        public DataTable ShowBookerRecord(string intBookerRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select top 1 ID from category where BookerCategoryID = @BookerCategoryID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@BookerCategoryID", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@BookerCategoryID"].Value = intBookerRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString()});
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

        public int DuplicateCount(string CatID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Category where CategoryID=@CategoryID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@CategoryID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@CategoryID"].Value = CatID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["RECCOUNT"]);
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

        public int GetPOSOrder(int pID)
        {
            string strSQLComm = " select PosDisplayOrder from Category where ID = @ID ";

            int boolResult = 0;

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
                    boolResult = Functions.fnInt32(objsqlReader["PosDisplayOrder"].ToString());
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return boolResult;
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

        #region Fetch Maximum Pos Display Order
        public int MaxPosDisplayOrder(int refcategory)
        {
            int intCount = 0;

            string strSQLComm = " select isnull(Max(PosDisplayOrder),0) AS rcnt from Category where ParentCategory = " + refcategory.ToString();

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
        public int MaxPosDisplayOrder()
        {
            int intCount = 0;

            string strSQLComm = " select max(PosDisplayOrder) as rcnt from Category  ";

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

        public bool IsDuplicatePOSOrder(int POSOrder)
        {
            string strSQLComm = " select PosDisplayOrder from Category where PosDisplayOrder = @PosDisplayOrder ";

            bool boolResult = false;
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@PosDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PosDisplayOrder"].Value = POSOrder;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    boolResult = true;
                }
                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return boolResult;
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

        #endregion 

        #region Fetch Lookup Data

        public DataTable FetchLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CategoryID,Description from Category";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CategoryID"].ToString(),
												   objSQLReader["Description"].ToString()
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

        public DataTable FetchLookupData1()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CategoryID,Description from category order by Description";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CategoryID"].ToString(),
												   objSQLReader["Description"].ToString()
                                                   });
                }

                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All });

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

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Category Where ID = @ID";

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
        
        #region  Update POS Display Order

        public int UpdatePosDisplayOrder(int pID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_RearrangeCategory_Delete";

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

        public DataTable ShowTaxes(int ProdId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select TM.*, T.TaxName, T.TaxRate from TaxMapping TM left outer join TaxHeader T on TM.TaxID = T.ID "
                              + " where TM.MappingID = @ID AND TM.MappingType = 'Category'";

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
                    strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
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

        public int UpdateProductOnCategorySettingChange(int iID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_updateproductfromcategory";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@catid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@catid"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@catid"].Value = iID;

                objSQlComm.Parameters.Add(new SqlParameter("@user", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@user"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@user"].Value = intLoginUserID;

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

                return intReturn;
            }
        }

        public int GetItemCount(int pID)
        {
            string strSQLComm = " select count(*) as rcnt from product where categoryID = @ID ";

            int boolResult = 0;

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
                    boolResult = Functions.fnInt32(objsqlReader["rcnt"].ToString());
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return boolResult;
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
    }
}
