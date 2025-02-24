/*
 purpose : Data for Discount, Mix N Match, Sale Price, Future Price
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Discounts
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private string strDataObjectCulture_SelectSKU;
        private string strDataObjectCulture_SelectPLU;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private int intNoOfChoices;
        private int intAnswerLevel;


        private int intAnswerID;
        private int intQuestionID;

        private string strDiscountName;
        private string strDiscountDescription;
        private string strDiscountStatus;
        private string strDiscountType;
        private string strRequiredReason;
        private string strChkDaysAvailable;
        private string strChkMonday;
        private string strChkTuesday;
        private string strChkWednesday;
        private string strChkThursday;
        private string strChkFriday;
        private string strChkSaturday;
        private string strChkSunday;
        private string strChkAllDay;
        private string strChkAllDate;
        private string strChkRestrictedItems;
        private string strChkLimitedPeriod;    
        private DateTime dtLimitedStartDate;
        private DateTime dtLimitedEndDate;

        private string strChkAutoApply;
        private string strChkApplyOnItem;
        private string strChkApplyOnTicket;

        private string strChkApplyOnCustomer;

        private string strStartTime;
        private string strEndTime;
        private DateTime dtStartDate;
        private DateTime dtEndDate;
        private string strItemType;
        private int intItemID;
        private int intDiscountID;
        private int intDateOfMonth;

        private double dblDiscountAmount;
        private double dblDiscountPercentage;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;

        private DataTable dblSplitDataTableTime;
        private DataTable dblSplitDataTableDate;
        private DataTable dblSplitDataTableRIG;
        private DataTable dblSplitDataTableRI;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string strErrorMsg;

        private int intDiscountFamilyID;
        private string strDiscountCategory;
        private int intDiscountPlusQty;

        private string strSaleBatchName;
        private string strSaleBatchDesc;
        private string strSaleStatus;
        private DateTime dtEffectiveFrom;
        private DateTime dtEffectiveTo;

        private double dblSalePrice;
        private string strApplyFamily;

        private DataTable dtblSalePriceData;


        private string strFutureBatchName;
        private string strFutureBatchDesc;
        private string strFutureBatchStatus;


        private double dblFuturePriceA;
        private double dblFuturePriceB;
        private double dblFuturePriceC;

        private DataTable dtblFuturePriceData;

        private string strLimitedPeriodCheck;
        private double dblAbsolutePrice;
        private int intBasedOn;

        private int intBuyProductID;
        private int intBuyQty;

        private int intFreeProductID;
        private int intFreeQty;
        private string strDeleteFreeItem;
        private int intFreeRef;

        #endregion

        #region definining public variables

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }
        public string DataObjectCulture_All
        {
            get { return strDataObjectCulture_All; }
            set { strDataObjectCulture_All = value; }
        }

        public string DataObjectCulture_SelectPLU
        {
            get { return strDataObjectCulture_SelectPLU; }
            set { strDataObjectCulture_SelectPLU = value; }
        }

        public string DataObjectCulture_SelectSKU
        {
            get { return strDataObjectCulture_SelectSKU; }
            set { strDataObjectCulture_SelectSKU = value; }
        }

        public string DataObjectCulture_None
        {
            get { return strDataObjectCulture_None; }
            set { strDataObjectCulture_None = value; }
        }
        public DataTable FuturePriceData
        {
            get { return dtblFuturePriceData; }
            set { dtblFuturePriceData = value; }
        }

        public DataTable SalePriceData
        {
            get { return dtblSalePriceData; }
            set { dtblSalePriceData = value; }
        }

        public DataTable SplitDataTable
        {
            get { return dblSplitDataTable; }
            set { dblSplitDataTable = value; }
        }

        public DataTable SplitDataTableTime
        {
            get { return dblSplitDataTableTime; }
            set { dblSplitDataTableTime = value; }
        }

        public DataTable SplitDataTableDate
        {
            get { return dblSplitDataTableDate; }
            set { dblSplitDataTableDate = value; }
        }

        public DataTable SplitDataTableRIG
        {
            get { return dblSplitDataTableRIG; }
            set { dblSplitDataTableRIG = value; }
        }

        public DataTable SplitDataTableRI
        {
            get { return dblSplitDataTableRI; }
            set { dblSplitDataTableRI = value; }
        }

        public string ChkLimitedPeriod
        {
            get { return strChkLimitedPeriod; }
            set { strChkLimitedPeriod = value; }
        }

        public DateTime LimitedStartDate
        {
            get { return dtLimitedStartDate; }
            set { dtLimitedStartDate = value; }
        }

        public DateTime LimitedEndDate
        {
            get { return dtLimitedEndDate; }
            set { dtLimitedEndDate = value; }
        }

        public int NoOfChoices
        {
            get { return intNoOfChoices; }
            set { intNoOfChoices = value; }
        }

        public int AnswerLevel
        {
            get { return intAnswerLevel; }
            set { intAnswerLevel = value; }
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
        
        public string DiscountName
        {
            get { return strDiscountName; }
            set { strDiscountName = value; }
        }
        
        public string DiscountDescription
        {
            get { return strDiscountDescription; }
            set { strDiscountDescription = value; }
        }

        public string DiscountStatus
        {
            get { return strDiscountStatus; }
            set { strDiscountStatus = value; }
        }
        
        public string DiscountType
        {
            get { return strDiscountType; }
            set { strDiscountType = value; }
        }
        
        public string RequiredReason
        {
            get { return strRequiredReason; }
            set { strRequiredReason = value; }
        }
        
        public string ChkDaysAvailable
        {
            get { return strChkDaysAvailable; }
            set { strChkDaysAvailable = value; }
        }
        
        public string ChkMonday
        {
            get { return strChkMonday; }
            set { strChkMonday = value; }
        }
        
        public string ChkTuesday
        {
            get { return strChkTuesday; }
            set { strChkTuesday = value; }
        }
        
        public string ChkWednesday
        {
            get { return strChkWednesday; }
            set { strChkWednesday = value; }
        }
        
        public string ChkThursday
        {
            get { return strChkThursday; }
            set { strChkThursday = value; }
        }
        
        public string ChkFriday
        {
            get { return strChkFriday; }
            set { strChkFriday = value; }
        }
        
        public string ChkSaturday
        {
            get { return strChkSaturday; }
            set { strChkSaturday = value; }
        }
        
        public string ChkSunday
        {
            get { return strChkSunday; }
            set { strChkSunday = value; }
        }
        
        public string ChkAllDay
        {
            get { return strChkAllDay; }
            set { strChkAllDay = value; }
        }
        
        public string ChkAllDate
        {
            get { return strChkAllDate; }
            set { strChkAllDate = value; }
        }
        
        public string ChkRestrictedItems
        {
            get { return strChkRestrictedItems; }
            set { strChkRestrictedItems = value; }
        }

        public double DiscountAmount
        {
            get { return dblDiscountAmount; }
            set { dblDiscountAmount = value; }
        }
        
        public double DiscountPercentage
        {
            get { return dblDiscountPercentage; }
            set { dblDiscountPercentage = value; }
        }

        public string ChkAutoApply
        {
            get { return strChkAutoApply; }
            set { strChkAutoApply = value; }
        }

        public string ChkApplyOnItem
        {
            get { return strChkApplyOnItem; }
            set { strChkApplyOnItem = value; }
        }

        public string ChkApplyOnTicket
        {
            get { return strChkApplyOnTicket; }
            set { strChkApplyOnTicket = value; }
        }

        public string ChkApplyOnCustomer
        {
            get { return strChkApplyOnCustomer; }
            set { strChkApplyOnCustomer = value; }
        }

        public string DiscountCategory
        {
            get { return strDiscountCategory; }
            set { strDiscountCategory = value; }
        }

        public int DiscountFamilyID
        {
            get { return intDiscountFamilyID; }
            set { intDiscountFamilyID = value; }
        }

        public int DiscountPlusQty
        {
            get { return intDiscountPlusQty; }
            set { intDiscountPlusQty = value; }
        }

        public string SaleBatchName
        {
            get { return strSaleBatchName; }
            set { strSaleBatchName = value; }
        }

        public string SaleBatchDesc
        {
            get { return strSaleBatchDesc; }
            set { strSaleBatchDesc = value; }
        }

        public string SaleStatus
        {
            get { return strSaleStatus; }
            set { strSaleStatus = value; }
        }

        public DateTime EffectiveFrom
        {
            get { return dtEffectiveFrom; }
            set { dtEffectiveFrom = value; }
        }

        public DateTime EffectiveTo
        {
            get { return dtEffectiveTo; }
            set { dtEffectiveTo = value; }
        }

        public string FutureBatchName
        {
            get { return strFutureBatchName; }
            set { strFutureBatchName = value; }
        }

        public string FutureBatchDesc
        {
            get { return strFutureBatchDesc; }
            set { strFutureBatchDesc = value; }
        }

        public string FutureBatchStatus
        {
            get { return strFutureBatchStatus; }
            set { strFutureBatchStatus = value; }
        }

        public string LimitedPeriodCheck
        {
            get { return strLimitedPeriodCheck; }
            set { strLimitedPeriodCheck = value; }
        }

        public double AbsolutePrice
        {
            get { return dblAbsolutePrice; }
            set { dblAbsolutePrice = value; }
        }

        public int BasedOn
        {
            get { return intBasedOn; }
            set { intBasedOn = value; }
        }

        public int BuyProductID
        {
            get { return intBuyProductID; }
            set { intBuyProductID = value; }
        }

        public int BuyQty
        {
            get { return intBuyQty; }
            set { intBuyQty = value; }
        }

        #endregion
        
        public void BeginTransaction()
        {
            SaveTran = null;
            SaveCon = this.sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();
        }

        public void EndTransaction()
        {
            if (SaveTran != null)
            {
                SaveTran.Commit();
                SaveTran.Dispose();
                SaveCon.Close();
            }
        }

        public bool AddEditDiscount()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertDiscountMasterData(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateDiscountMasterData(SaveComm);
                }
                //if (strChkAllDay == "N")
                //{
                DeleteDiscountTime(SaveComm, intDiscountID);
                AdjustSplitGridRecordTime(SaveComm);
                //}

                //if (strChkAllDate == "N")
                //{
                DeleteDiscountDate(SaveComm, intDiscountID);
                AdjustSplitGridRecordDate(SaveComm);
                //}

                //if (strChkRestrictedItems == "Y")
                //{
                DeleteDiscountRestrictedItems(SaveComm, intDiscountID);
                AdjustSplitGridRecordGroupItem(SaveComm);
                AdjustSplitGridRecordItem(SaveComm);
                //}

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        private void AdjustSplitGridRecordTime(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableTime == null) return;
                foreach (DataRow dr in dblSplitDataTableTime.Rows)
                {
                    string colID = "";
                    string colSTime = "";
                    string colETime = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }
                    colSTime = dr["StartTime"].ToString();
                    colETime = dr["EndTime"].ToString();

                    strStartTime = colSTime;
                    strEndTime = colETime;

                    InsertDiscountTime(objSQlComm);
                }

                dblSplitDataTableTime.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustSplitGridRecordDate(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableDate == null) return;
                foreach (DataRow dr in dblSplitDataTableDate.Rows)
                {
                    string colID = "";
                    
                    if (dr["DateValue"].ToString() == "") continue;

                    if (dr["DateValue"].ToString() != "")
                    {
                        colID = dr["DateValue"].ToString();
                    }
                    intDateOfMonth = Functions.fnInt32(colID);
                    InsertDiscountDate(objSQlComm);
                }
                dblSplitDataTableDate.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustSplitGridRecordGroupItem(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableRIG == null) return;
                foreach (DataRow dr in dblSplitDataTableRIG.Rows)
                {
                    string colID = "";
                    string colItemID = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }
                    colItemID = dr["ID"].ToString();

                    intItemID = Functions.fnInt32(colItemID);

                    InsertRestrictedDiscount(objSQlComm,"G");
                }
                dblSplitDataTableRIG.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustSplitGridRecordItem(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableRI == null) return;
                foreach (DataRow dr in dblSplitDataTableRI.Rows)
                {
                    string colID = "";
                    string colItemID = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }
                    colItemID = dr["ID"].ToString();

                    intItemID = Functions.fnInt32(colItemID);

                    InsertRestrictedDiscount(objSQlComm, "I");
                }
                dblSplitDataTableRI.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeleteDiscountTime(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from discountTime where DiscountID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;

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

        public bool DeleteDiscountDate(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from DiscountDate where DiscountID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool DeleteDiscountRestrictedItems(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "delete from DiscountRestrictedItems where DiscountID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertDiscountMasterData(SqlCommand objSQlComm)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into DiscountMaster(DiscountName,DiscountDescription,DiscountStatus,DiscountType,"
                                  + " DiscountAmount,DiscountPercentage,RequiredReason,ChkDaysAvailable,ChkMonday,"
                                  + " ChkTuesday,ChkWednesday,ChkThursday,ChkFriday,"
                                  + " ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,ChkRestrictedItems,"
                                  + " CreatedBy, CreatedOn,LastChangedBy,LastChangedOn,ChkAutoApply,ChkApplyOnItem,ChkApplyOnTicket,"
                                  + " ChkLimitedPeriod,LimitedStartDate,LimitedEndDate,ChkApplyOnCustomer )"
                                  + " values( @DiscountName,@DiscountDescription,@DiscountStatus, "
                                  + " @DiscountType,@DiscountAmount,@DiscountPercentage,"
                                  + " @RequiredReason,@ChkDaysAvailable,@ChkMonday,"
                                  + " @ChkTuesday,@ChkWednesday,@ChkThursday,@ChkFriday,"
                                  + " @ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@ChkRestrictedItems,"
                                  + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@ChkAutoApply,@ChkApplyOnItem,@ChkApplyOnTicket, "
                                  + " @ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate,@ChkApplyOnCustomer )"
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RequiredReason", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDaysAvailable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkMonday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTuesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkWednesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkThursday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFriday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSaturday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSunday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDay", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkRestrictedItems", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkAutoApply", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnItem", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnTicket", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnCustomer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkLimitedPeriod", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@RequiredReason"].Value = strRequiredReason;
                objSQlComm.Parameters["@ChkDaysAvailable"].Value = strChkDaysAvailable;
                objSQlComm.Parameters["@ChkMonday"].Value = strChkMonday;
                objSQlComm.Parameters["@ChkTuesday"].Value = strChkTuesday;
                objSQlComm.Parameters["@ChkWednesday"].Value = strChkWednesday;
                objSQlComm.Parameters["@ChkThursday"].Value = strChkThursday;
                objSQlComm.Parameters["@ChkFriday"].Value = strChkFriday;
                objSQlComm.Parameters["@ChkSaturday"].Value = strChkSaturday;
                objSQlComm.Parameters["@ChkSunday"].Value = strChkSunday;
                objSQlComm.Parameters["@ChkAllDay"].Value = strChkAllDay;
                objSQlComm.Parameters["@ChkAllDate"].Value = strChkAllDate;
                objSQlComm.Parameters["@ChkRestrictedItems"].Value = strChkRestrictedItems;

                objSQlComm.Parameters["@ChkAutoApply"].Value = strChkAutoApply;
                objSQlComm.Parameters["@ChkApplyOnItem"].Value = strChkApplyOnItem;
                objSQlComm.Parameters["@ChkApplyOnTicket"].Value = strChkApplyOnTicket;
                objSQlComm.Parameters["@ChkApplyOnCustomer"].Value = strChkApplyOnCustomer;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@ChkLimitedPeriod"].Value = strChkLimitedPeriod;
                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = dtLimitedStartDate;
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = dtLimitedEndDate;
                }

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }

        public bool UpdateDiscountMasterData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update DiscountMaster set DiscountName=@DiscountName,DiscountDescription=@DiscountDescription, "
                                  + " DiscountStatus=@DiscountStatus,DiscountType=@DiscountType,DiscountAmount=@DiscountAmount,"
                                  + " DiscountPercentage=@DiscountPercentage,RequiredReason=@RequiredReason,ChkDaysAvailable=@ChkDaysAvailable,"
                                  + " ChkMonday=@ChkMonday,ChkTuesday=@ChkTuesday,ChkWednesday=@ChkWednesday,"
                                  + " ChkThursday=@ChkThursday,ChkFriday=@ChkFriday,ChkSaturday=@ChkSaturday,ChkSunday=@ChkSunday,"
                                  + " ChkAllDay=@ChkAllDay,ChkAllDate=@ChkAllDate,ChkRestrictedItems=@ChkRestrictedItems,"
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn= @LastChangedOn,"
                                  + " ChkAutoApply=@ChkAutoApply,ChkApplyOnItem=@ChkApplyOnItem,ChkApplyOnTicket=@ChkApplyOnTicket, "
                                  + " ChkLimitedPeriod=@ChkLimitedPeriod,LimitedStartDate=@LimitedStartDate,LimitedEndDate=@LimitedEndDate, "
                                  + " ChkApplyOnCustomer = @ChkApplyOnCustomer where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@RequiredReason", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDaysAvailable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkMonday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTuesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkWednesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkThursday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFriday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSaturday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSunday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDay", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDate", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkRestrictedItems", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkAutoApply", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnItem", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnTicket", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyOnCustomer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkLimitedPeriod", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@RequiredReason"].Value = strRequiredReason;
                objSQlComm.Parameters["@ChkDaysAvailable"].Value = strChkDaysAvailable;
                objSQlComm.Parameters["@ChkMonday"].Value = strChkMonday;
                objSQlComm.Parameters["@ChkTuesday"].Value = strChkTuesday;
                objSQlComm.Parameters["@ChkWednesday"].Value = strChkWednesday;
                objSQlComm.Parameters["@ChkThursday"].Value = strChkThursday;
                objSQlComm.Parameters["@ChkFriday"].Value = strChkFriday;
                objSQlComm.Parameters["@ChkSaturday"].Value = strChkSaturday;
                objSQlComm.Parameters["@ChkSunday"].Value = strChkSunday;
                objSQlComm.Parameters["@ChkAllDay"].Value = strChkAllDay;
                objSQlComm.Parameters["@ChkAllDate"].Value = strChkAllDate;
                objSQlComm.Parameters["@ChkRestrictedItems"].Value = strChkRestrictedItems;

                objSQlComm.Parameters["@ChkAutoApply"].Value = strChkAutoApply;
                objSQlComm.Parameters["@ChkApplyOnItem"].Value = strChkApplyOnItem;
                objSQlComm.Parameters["@ChkApplyOnTicket"].Value = strChkApplyOnTicket;
                objSQlComm.Parameters["@ChkApplyOnCustomer"].Value = strChkApplyOnCustomer;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@ChkLimitedPeriod"].Value = strChkLimitedPeriod;
                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = dtLimitedStartDate;
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = dtLimitedEndDate;
                }

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertDiscountTime(SqlCommand objSQlComm)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into DiscountTime(DiscountID,StartTime,EndTime,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @DiscountID,@StartTime,@EndTime,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StartTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EndTime", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@StartTime"].Value = strStartTime;
                objSQlComm.Parameters["@EndTime"].Value = strEndTime;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool InsertDiscountDate(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into DiscountDate(DiscountID,DateOfMonth,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @DiscountID,@DateOfMonth,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)  "
                                  + " select @@IDENTITY as ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMonth", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@DateOfMonth"].Value = intDateOfMonth;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool InsertRestrictedDiscount(SqlCommand objSQlComm,string pType)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into DiscountRestrictedItems(DiscountID,ItemType,ItemID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @DiscountID,@ItemType,@ItemID,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@ItemType"].Value = pType;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                
                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        #region Fetchdata

        public DataTable FetchModifierGroup(int MNUgrp)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm =  " select distinct p.ModifierGroupID, m.ModifierGroupDesc from Product p Left outer join ModifierGroup m "
                               + " on p.ModifierGroupID = m.ID where p.MenuGroupID = @MGID and p.IsModifier = 'Y' order by m.ModifierGroupDesc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@MGID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@MGID"].Value = MNUgrp;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Modifier", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ModifierGroupID"].ToString(),
												   objSQLReader["ModifierGroupDesc"].ToString() });
                }
                
                dtbl.Rows.Add(new object[] { "0", "All Modifiers" });

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

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,DiscountName,DiscountStatus,DiscountType,DiscountAmount,DiscountPercentage,"
                              + " ChkApplyOnItem,ChkApplyOnTicket,ChkLimitedPeriod,LimitedStartDate,LimitedEndDate "
                              + " from DiscountMaster order by DiscountName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ApplyOn", System.Type.GetType("System.String"));

                string chk1 = "";
                string chk2 = "";
                string val = "";

                string appitm = "";
                string apptkt = "";

                while (objSQLReader.Read())
                {
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
                        chk1 = "     Yes";
                    }
                    else
                    {
                        chk1 = "     No";
                    }
                    if (objSQLReader["ChkLimitedPeriod"].ToString() == "Y")
                    {
                        chk1 = chk1 + "    ("
                             + Convert.ToDateTime(objSQLReader["LimitedStartDate"].ToString()).ToString("d") + " - " + Convert.ToDateTime(objSQLReader["LimitedEndDate"].ToString()).ToString("d") + "  )";
                    }
                    if (objSQLReader["DiscountType"].ToString() == "A")
                    {
                        chk2 = "Amount off";
                        val = objSQLReader["DiscountAmount"].ToString();
                    }
                    else
                    {
                        chk2 = "% off";
                        val = objSQLReader["DiscountPercentage"].ToString();
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DiscountName"].ToString(), 
												   chk1,chk2,Functions.fnDouble(val),dtxt });
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
        
        public DataTable FetchRestrictedItem(int intDID, string cat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (cat == "G")
                strSQLComm = " select a.ItemID as ID, c.Description as Description from DiscountRestrictedItems a left outer join "
                           + " Category c on a.ItemID = c.ID where a.DiscountID = @QID and a.ItemType = 'G' ";
            if (cat == "I")
                strSQLComm = " select a.ItemID as ID, c.Description as Description from DiscountRestrictedItems a left outer join "
                           + " Product c on a.ItemID = c.ID where a.DiscountID = @QID and a.ItemType = 'I' ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Item", System.Type.GetType("System.String"));

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

        public DataTable FetchDiscountDate(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,DateOfMonth from DiscountDate where DiscountID = @QID order by DateOfMonth ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfMonth", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DateOfMonth"].ToString()});
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

        public DataTable FetchDiscountTime(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,StartTime,EndTime from DiscountTime where DiscountID = @QID order by startTime ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["StartTime"].ToString(),
                                                   objSQLReader["EndTime"].ToString()});
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

        public DataTable FetchCustomerDiscountLookup()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,DiscountName from DiscountMaster where DiscountStatus = 'Y' "
                              + " and ChkApplyOnCustomer = 'Y' order by DiscountName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DiscountName"].ToString() });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_None });
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

        public DataTable ShowRecord(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from DiscountMaster where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountPercentage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RequiredReason", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkDaysAvailable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkMonday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkTuesday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkWednesday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkThursday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkFriday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkSaturday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkSunday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkAllDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkAllDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkRestrictedItems", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ChkAutoApply", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkApplyOnItem", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkApplyOnTicket", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkLimitedPeriod", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LimitedStartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LimitedEndDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkApplyOnCustomer", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["DiscountName"].ToString(),
												objSQLReader["DiscountDescription"].ToString(),
                                                objSQLReader["DiscountStatus"].ToString(),
                                                objSQLReader["DiscountType"].ToString(),
                                                objSQLReader["DiscountAmount"].ToString(),
                                                objSQLReader["DiscountPercentage"].ToString(),
                                                objSQLReader["RequiredReason"].ToString(),
                                                objSQLReader["ChkDaysAvailable"].ToString(),
                                                objSQLReader["ChkMonday"].ToString(),
                                                objSQLReader["ChkTuesday"].ToString(),
                                                objSQLReader["ChkWednesday"].ToString(),
                                                objSQLReader["ChkThursday"].ToString(),
                                                objSQLReader["ChkFriday"].ToString(),
                                                objSQLReader["ChkSaturday"].ToString(),
                                                objSQLReader["ChkSunday"].ToString(),
                                                objSQLReader["ChkAllDay"].ToString(),
                                                objSQLReader["ChkAllDate"].ToString(),
                                                objSQLReader["ChkRestrictedItems"].ToString(),
                                                objSQLReader["ChkAutoApply"].ToString(),
                                                objSQLReader["ChkApplyOnItem"].ToString(),
                                                objSQLReader["ChkApplyOnTicket"].ToString(),
                                                objSQLReader["ChkLimitedPeriod"].ToString(),
                                                objSQLReader["LimitedStartDate"].ToString(),
                                                objSQLReader["LimitedEndDate"].ToString(),
                                                objSQLReader["ChkApplyOnCustomer"].ToString()
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
        
        #region Delete Data

        public int DeleteDiscount(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deletediscount";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        #endregion
                
        #region Fetch Answer Lookup data

        public DataTable FetchCatLookup()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CategoryID,Description from Category order by Description ";

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

        public DataTable FetchItemLookup()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,MenuID,MenuName from Product where productstatus = 'Y' order by MenuName ";

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
												   objSQLReader["MenuName"].ToString()});
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

        #region Duplicate Checking

        public int DuplicateCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as RECCOUNT from DiscountMaster where  " + " DiscountName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["RECCOUNT"].ToString());
                }
                
                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        #endregion 

        #endregion


        public int GetCustomerDiscountCount(int DID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from customer where discountid=@did ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@did"].Value = DID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public DataTable LookupProductORCategory(int optionID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (optionID == 3) strSQLComm = " select ID,DepartmentID as CD,Description from dept where enabled = 'Y' order by Description ";
            if (optionID == 2) strSQLComm = " select ID,BrandID as CD,BrandDescription as Description from brandmaster order by BrandDescription ";
            if (optionID == 1) strSQLComm = " select ID,SKU as CD,Description from product where productstatus = 'Y' order by Description ";
            if (optionID == 11) strSQLComm = " select ID,SKU as CD,Description from product where productstatus = 'Y' and BrandID = 0 order by Description ";
            if (optionID == 10) strSQLComm = " select ID,SKU as CD,Description from product where productstatus = 'Y' and NonDiscountable = 'N' order by Description ";
            if (optionID == 0) strSQLComm = " select ID,CategoryID as CD,Description from category order by Description ";
            if (optionID == 12) strSQLComm = " select distinct s.plu_number as ID, s.plu_number as CD, p.Description  as Description from scale_product s left outer join product p on p.ID = s.ProductID order by p.Description ";
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["CD"].ToString(), 
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

        public DataTable LookupProduct()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,SKU,Description from product where productstatus = 'Y' order by Description ";

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

        public DataTable LookupBrand()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,BrandID,BrandDescription from brandmaster order by BrandDescription ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BrandDescription", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["BrandID"].ToString(), 
                                                   objSQLReader["BrandDescription"].ToString()});
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

        public DataTable LookupProduct1()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,SKU,Description from product where productstatus = 'Y' order by Description ";

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

        public DataTable LookupProduct2()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,SKU,Description from product where productstatus = 'Y' order by Description ";

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

                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All });
                dtbl.Rows.Add(new object[] { "-1", strDataObjectCulture_SelectSKU, strDataObjectCulture_SelectSKU });

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

        

        #region Mix N Match

        public bool isDuplicateBookersPromotion()
        {

            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = @"select MH.ID
                                from MixNmatchHeader MH
                                join MixNmatchDetails MD on MH.ID=MD.HeaderID
                                where OfferStartOn=@OfferStartOn and OfferEndOn=@OfferEndOn and ItemID=@ItemID";

                SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.CommandText = strSQLComm;


                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OfferStartOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@OfferEndOn", System.Data.SqlDbType.DateTime));



                objSQlComm.Parameters["@ItemID"].Value = intID;
                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = new DateTime(dtLimitedStartDate.Year, dtLimitedStartDate.Month, dtLimitedStartDate.Day, 0, 0, 0);
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = new DateTime(dtLimitedEndDate.Year, dtLimitedEndDate.Month, dtLimitedEndDate.Day, 23, 59, 59);
                }

                bool isExists = false;
                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                    isExists = true;
                
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return isExists;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }

        }
        public DataTable FetchItemsOfFamily(int pBID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,SKU,Description,BrandID from Product where BrandID = @ID and ProductStatus = 'Y' "
                              // " union select ID,SKU,Description,BrandID from Product where BrandID = 0 and ProductStatus = 'Y' "
                              + " order by BrandID desc, Description desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pBID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("chk", System.Type.GetType("System.Boolean"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                objSQLReader["BrandID"].ToString(),
                                                false
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

        public bool ProcessMixNmatchSetup()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertMixNmatchHeader(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateMixNmatchHeader(SaveComm);
                }

                DeleteMixNmatchDetails(SaveComm, intDiscountID);
                AdjustMixNmatchDetails(SaveComm);
                
                intID = intDiscountID;
                SaveComm.Dispose();
                
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public bool InsertMixNmatchHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into MixNmatchHeader(DiscountName,DiscountDescription,DiscountStatus,DiscountType, "
                                  + " DiscountAmount,DiscountPercentage,DiscountFamilyID,DiscountCategory,DiscountPlusQty, "
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,OfferStartOn,OfferEndOn,LimitedPeriodCheck,AbsolutePrice,BasedOn ) "
                                  + " values( @DiscountName,@DiscountDescription,@DiscountStatus,@DiscountType,@DiscountAmount,"
                                  + " @DiscountPercentage,@DiscountFamilyID,@DiscountCategory,@DiscountPlusQty,@CreatedBy,@CreatedOn,"
                                  + " @LastChangedBy,@LastChangedOn,@OfferStartOn,@OfferEndOn,@LimitedPeriodCheck,@AbsolutePrice,@BasedOn ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountCategory", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountFamilyID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPlusQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@OfferStartOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@OfferEndOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LimitedPeriodCheck", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AbsolutePrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@BasedOn", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@DiscountCategory"].Value = strDiscountCategory;
                objSQlComm.Parameters["@DiscountFamilyID"].Value = intDiscountFamilyID;
                objSQlComm.Parameters["@DiscountPlusQty"].Value = intDiscountPlusQty;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = new DateTime(dtLimitedStartDate.Year, dtLimitedStartDate.Month, dtLimitedStartDate.Day, 0, 0, 0);
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = new DateTime(dtLimitedEndDate.Year, dtLimitedEndDate.Month, dtLimitedEndDate.Day, 23, 59, 59);
                }

                objSQlComm.Parameters["@LimitedPeriodCheck"].Value = strLimitedPeriodCheck;
                objSQlComm.Parameters["@AbsolutePrice"].Value = dblAbsolutePrice;
                objSQlComm.Parameters["@BasedOn"].Value = intBasedOn;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }
        
        public bool UpdateMixNmatchHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update MixNmatchHeader set DiscountName=@DiscountName,DiscountDescription=@DiscountDescription, "
                                  + " DiscountStatus=@DiscountStatus,DiscountType=@DiscountType,DiscountAmount=@DiscountAmount,"
                                  + " DiscountPercentage=@DiscountPercentage,DiscountFamilyID=@DiscountFamilyID,"
                                  + " DiscountCategory=@DiscountCategory,DiscountPlusQty=@DiscountPlusQty,BasedOn=@BasedOn, "
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn = @LastChangedOn,OfferStartOn=@OfferStartOn, "
                                  + " OfferEndOn=@OfferEndOn,LimitedPeriodCheck=@LimitedPeriodCheck,AbsolutePrice=@AbsolutePrice where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountCategory", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountFamilyID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPlusQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OfferStartOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@OfferEndOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LimitedPeriodCheck", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@AbsolutePrice", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@BasedOn", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@DiscountCategory"].Value = strDiscountCategory;
                objSQlComm.Parameters["@DiscountFamilyID"].Value = intDiscountFamilyID;
                objSQlComm.Parameters["@DiscountPlusQty"].Value = intDiscountPlusQty;

                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferStartOn"].Value = new DateTime(dtLimitedStartDate.Year, dtLimitedStartDate.Month, dtLimitedStartDate.Day, 0, 0, 0);
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@OfferEndOn"].Value = new DateTime(dtLimitedEndDate.Year, dtLimitedEndDate.Month, dtLimitedEndDate.Day, 23, 59, 59);
                }

                objSQlComm.Parameters["@LimitedPeriodCheck"].Value = strLimitedPeriodCheck;
                objSQlComm.Parameters["@AbsolutePrice"].Value = dblAbsolutePrice;
                objSQlComm.Parameters["@BasedOn"].Value = intBasedOn;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustMixNmatchDetails(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;

                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    if (!Convert.ToBoolean(dr["chk"].ToString())) continue;
                    
                    string colItemID = "";

                    if (dr["ID"].ToString() != "")
                    {
                        colItemID = dr["ID"].ToString();
                    }
                    
                    intItemID = Functions.fnInt32(colItemID);
                    InsertMixNmatchDetails(objSQlComm);
                }
                dblSplitDataTableDate.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertMixNmatchDetails(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into MixNmatchDetails(HeaderID,ItemID) values ( @DiscountID,@ItemID ) "
                                  + " select @@IDENTITY as ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool DeleteMixNmatchDetails(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from MixNmatchDetails where HeaderID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public DataTable FetchMixNmatchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select m.ID,m.DiscountName,m.DiscountStatus,m.DiscountType,m.DiscountAmount,m.DiscountPercentage,m.AbsolutePrice,m.BasedOn, "
                              + " m.DiscountCategory,m.DiscountFamilyID,m.DiscountPlusQty,isnull(b.BrandDescription,'') as Family "
                              + " from MixNmatchHeader m left outer join BrandMaster b on m.DiscountFamilyID = b.ID order by m.DiscountName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BasedOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DiscountCategory", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Family", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountPlusQty", System.Type.GetType("System.String"));

                string chk1 = "";
                string chk2 = "";
                string chk3 = "";
                string val = "";

                while (objSQLReader.Read())
                {
                    chk1 = "";
                    chk2 = "";
                    chk3 = "";

                    if (objSQLReader["DiscountStatus"].ToString() == "Y") chk1 = "Yes";
                    else chk1 = "No";

                    if (objSQLReader["DiscountCategory"].ToString() != "A")
                    {
                        if (objSQLReader["DiscountType"].ToString() == "A")
                        {
                            chk2 = "Amount off";
                            val = objSQLReader["DiscountAmount"].ToString();
                        }
                        else
                        {
                            chk2 = "% off";
                            val = objSQLReader["DiscountPercentage"].ToString();
                        }
                    }
                    else
                    {
                        chk2 = "Absolute";
                        val = objSQLReader["AbsolutePrice"].ToString();
                    }

                    if (objSQLReader["DiscountCategory"].ToString() == "N")
                    {
                        chk3 = "Normal Pricing";
                    }
                    else if (objSQLReader["DiscountCategory"].ToString() == "P")
                    {
                        chk3 = "Plus Pricing";
                    }
                    else if (objSQLReader["DiscountCategory"].ToString() == "A")
                    {
                        chk3 = "Absolute Pricing";
                    }
                    else
                    {
                        chk3 = "NA";
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DiscountName"].ToString(), 
												   chk1,
                                                   Functions.fnInt32(objSQLReader["BasedOn"].ToString())  == 0 ? "Item Qty" : "Amount",
                                                   chk2,Functions.fnDouble(val),
                                                   chk3,objSQLReader["Family"].ToString(),
                                                   Functions.fnInt32(objSQLReader["BasedOn"].ToString())  == 0 ? objSQLReader["DiscountPlusQty"].ToString() :
                                                   "Min. amount :" + Functions.fnDouble(objSQLReader["AbsolutePrice"].ToString()).ToString("f2") });
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

        public DataTable ShowMixNmatchHeader(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from MixNmatchHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountPercentage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountCategory", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountFamilyID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountPlusQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OfferStartOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OfferEndOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LimitedPeriodCheck", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AbsolutePrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BasedOn", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] 
                                            {
                                                objSQLReader["DiscountName"].ToString(),
												objSQLReader["DiscountDescription"].ToString(),
                                                objSQLReader["DiscountStatus"].ToString(),
                                                objSQLReader["DiscountType"].ToString(),
                                                objSQLReader["DiscountAmount"].ToString(),
                                                objSQLReader["DiscountPercentage"].ToString(),
                                                objSQLReader["DiscountCategory"].ToString(),
                                                objSQLReader["DiscountFamilyID"].ToString(),
                                                objSQLReader["DiscountPlusQty"].ToString(),
                                                objSQLReader["OfferStartOn"].ToString(),
                                                objSQLReader["OfferEndOn"].ToString(),
                                                objSQLReader["LimitedPeriodCheck"].ToString(),
                                                objSQLReader["AbsolutePrice"].ToString(),
                                                objSQLReader["BasedOn"].ToString()
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

        public DataTable FetchMixNmatchDetails(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ItemID from MixNmatchDetails where HeaderID = @H ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@H", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@H"].Value = intDID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ItemID"].ToString() });
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

        public int DuplicateMixNmatchCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from MixNmatchHeader where  " + " DiscountName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public int DeleteMixNmatch(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deletemixnmatch";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;
                objSQlComm.CommandTimeout = 500;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ID"].Value = DeleteID;

                objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();

                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value.ToString());

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        #endregion

        #region Sale Price Setup

        public DataTable FetchSalePriceData(string status, string configDateFormat)
        {
            DataTable dtbl = new DataTable();
            string sql1 = "";
            if (status == "Active")
            {
                sql1 = " and SaleStatus = 'Y' ";
            }
            if (status == "Inactive")
            {
                sql1 = " and SaleStatus = 'N' ";
            }
            string strSQLComm = " select ID,SaleBatchName,SaleBatchDesc,SaleStatus,EffectiveFrom,EffectiveTo "
                              + " from SalePriceHeader where 1 = 1 " + sql1 + " order by SaleBatchName ";

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

                    val = Convert.ToDateTime(objSQLReader["EffectiveFrom"].ToString()).ToString(configDateFormat + "     hh:mm tt") + "  -  " + Convert.ToDateTime(objSQLReader["EffectiveTo"].ToString()).ToString((configDateFormat) + "     hh:mm tt");

                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["SaleBatchName"].ToString(), 
                                                   objSQLReader["SaleBatchDesc"].ToString(), 
												   chk1,val });
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

        public DataTable FetchSalePriceDataForReport(DateTime FDT, DateTime TDT)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select h.SaleBatchName as batch,p.SKU as item_code,p.Description as item_name,p.PriceA as normal_price,d.SalePrice, 'I' as item_type "
                              + " from SalePriceHeader h left outer join SalePriceDetails d on h.ID = d.SaleBatchID "
                              + " left outer join product p on (p.ID = d.ItemID and d.ApplyFamily = 'N') where (1 = 1) "
                              + " and h.SaleStatus = 'Y' and p.ProductStatus = 'Y' and ((h.EffectiveFrom between @d1 and @d2) or "
                              + " (h.EffectiveTo between @d3 and @d4)) "
                              + " union "
                              + " select h.SaleBatchName as batch,b.BrandID as item_code,b.BrandDescription as item_name,0 as normal_price,d.SalePrice, 'F' as item_type "
                              + " from SalePriceHeader h left outer join SalePriceDetails d on h.ID = d.SaleBatchID "
                              + " left outer join brandmaster b on (b.ID = d.ItemID and d.ApplyFamily = 'Y') where (1 = 1) "
                              + " and h.SaleStatus = 'Y' and ((h.EffectiveFrom between @d1 and @d2) or "
                              + " (h.EffectiveTo between @d3 and @d4)) "
                              + "  order by item_type ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@d1"].Value = FDT;

            objSQlComm.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@d3"].Value = FDT;

            objSQlComm.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@d2"].Value = TDT;

            objSQlComm.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@d4"].Value = TDT;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Batch", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["item_code"].ToString() == "") && (objSQLReader["item_name"].ToString() == "")) continue;
                    dtbl.Rows.Add(new object[] {   objSQLReader["item_code"].ToString(),
                                                   objSQLReader["item_type"].ToString() == "I" ? objSQLReader["item_name"].ToString() : objSQLReader["item_name"].ToString() + " [Family]", 
                                                   objSQLReader["SalePrice"].ToString(), 
                                                   objSQLReader["normal_price"].ToString(),
												   //objSQLReader["item_type"].ToString() == "I" ? objSQLReader["normal_price"].ToString() : "NA",
                                                   objSQLReader["batch"].ToString() });
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

        public DataTable ShowSalePriceHeader(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from SalePriceHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SaleBatchName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SaleBatchDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SaleStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EffectiveFrom", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EffectiveTo", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["SaleBatchName"].ToString(),
												objSQLReader["SaleBatchDesc"].ToString(),
                                                objSQLReader["SaleStatus"].ToString(),
                                                objSQLReader["EffectiveFrom"].ToString(),
                                                objSQLReader["EffectiveTo"].ToString()
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

        public DataTable FetchSalePriceDetails(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select * from SalePriceDetails where SaleBatchID = @QID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyFamily", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalePrice", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {   objSQLReader["ID"].ToString(),
                                                    objSQLReader["ItemID"].ToString(),
                                                    "",
                                                    objSQLReader["ApplyFamily"].ToString(),
                                                    Functions.fnDouble(objSQLReader["SalePrice"].ToString())
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

        public bool InsertSalePriceHeaderData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into SalePriceHeader(SaleBatchName,SaleBatchDesc,SaleStatus,EffectiveFrom,EffectiveTo,"
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn )"
                                  + " values( @SaleBatchName,@SaleBatchDesc,@SaleStatus,@EffectiveFrom,@EffectiveTo,"
                                  + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)"
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SaleStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveTo", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SaleBatchName"].Value = strSaleBatchName;
                objSQlComm.Parameters["@SaleBatchDesc"].Value = strSaleBatchDesc;
                objSQlComm.Parameters["@SaleStatus"].Value = strSaleStatus;
                objSQlComm.Parameters["@EffectiveFrom"].Value = dtEffectiveFrom;
                objSQlComm.Parameters["@EffectiveTo"].Value = dtEffectiveTo;
                
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                
                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }

        public bool UpdateSalePriceHeaderData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update SalePriceHeader set SaleBatchName=@SaleBatchName,SaleBatchDesc=@SaleBatchDesc,"
                                  + " SaleStatus=@SaleStatus,EffectiveFrom=@EffectiveFrom,EffectiveTo=@EffectiveTo,"
                                  + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";
                                  

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SaleStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveTo", System.Data.SqlDbType.DateTime));
                
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@SaleBatchName"].Value = strSaleBatchName;
                objSQlComm.Parameters["@SaleBatchDesc"].Value = strSaleBatchDesc;
                objSQlComm.Parameters["@SaleStatus"].Value = strSaleStatus;
                objSQlComm.Parameters["@EffectiveFrom"].Value = dtEffectiveFrom;
                objSQlComm.Parameters["@EffectiveTo"].Value = dtEffectiveTo;

                
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;


                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustSalePriceDetails(SqlCommand objSQlComm)
        {
            try
            {
                if (dtblSalePriceData == null) return;
                foreach (DataRow dr in dtblSalePriceData.Rows)
                {
                    string colID = "";
                    string colItemID = "";
                    string colSalePrice = "";
                    string colApplyFamily = "";

                    if (dr["ItemID"].ToString() == "") continue;

                    colItemID = dr["ItemID"].ToString();
                    colApplyFamily = dr["ApplyFamily"].ToString();
                    colSalePrice = dr["SalePrice"].ToString();


                    intItemID = Functions.fnInt32(colItemID);
                    strApplyFamily = colApplyFamily;
                    dblSalePrice = Functions.fnDouble(colSalePrice);
                    
                    InsertSalePriceDetails(objSQlComm);
                }
                dtblSalePriceData.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }
        
        public bool InsertSalePriceDetails(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into SalePriceDetails(SaleBatchID,ItemID,ApplyFamily,SalePrice,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @SaleBatchID,@ItemID,@ApplyFamily,@SalePrice,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@SaleBatchID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ApplyFamily", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@SalePrice", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SaleBatchID"].Value = intDiscountID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ApplyFamily"].Value = strApplyFamily;
                objSQlComm.Parameters["@SalePrice"].Value = dblSalePrice;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool DeleteSalePriceDetails(SqlCommand objSQlComm, int intpID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "delete from SalePriceDetails where SaleBatchID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intpID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool ProcessSalePrice()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertSalePriceHeaderData(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateSalePriceHeaderData(SaveComm);
                }
               
                DeleteSalePriceDetails(SaveComm, intDiscountID);
                AdjustSalePriceDetails(SaveComm);
                
                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public int DuplicateSaleBatch(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from SalePriceHeader where SaleBatchName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public int DeleteSaleBatch(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deletesalebatch";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        public int OverlappingSaleBatch(int refID, DateTime df, DateTime dt)
        {
            int intCount = 0;
            string strSQLComm = "";
            if (refID == 0) strSQLComm = " select count(*) as rcnt from SalePriceHeader where salestatus = 'Y' and (@df between EffectiveFrom and EffectiveTo  "
                                       + " or @dt between EffectiveFrom and EffectiveTo) ";

            if (refID > 0) strSQLComm = " select count(*) as rcnt from SalePriceHeader where salestatus = 'Y' and (@df between EffectiveFrom and EffectiveTo  "
                                       + " or @dt between EffectiveFrom and EffectiveTo) and ID <> @refID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@df", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@df"].Value = df;
                objSQlComm.Parameters.Add(new SqlParameter("@dt", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@dt"].Value = dt;

                if (refID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@refID"].Value = refID;
                }

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }


        public int OverlappingSaleItemInBatch(int refID, DateTime df, DateTime dt, int refItem, string refFamily)
        {
            int intCount = 0;
            string strSQLComm = "";
            if (refID == 0) strSQLComm = " select count(*) as rcnt from SalePriceHeader h left outer join SalePriceDetails d "
                                       + " on h.ID = d.SaleBatchID where h.salestatus = 'Y' and (@df between h.EffectiveFrom and h.EffectiveTo  "
                                       + " or @dt between h.EffectiveFrom and h.EffectiveTo) and d.ItemID = @ItemID and d.ApplyFamily = @AFamily ";

            if (refID > 0) strSQLComm = " select count(*) as rcnt from SalePriceHeader h left outer join SalePriceDetails d "
                                       + " on h.ID = d.SaleBatchID where h.salestatus = 'Y' and (@df between h.EffectiveFrom and h.EffectiveTo  "
                                       + " or @dt between h.EffectiveFrom and h.EffectiveTo) and d.ItemID = @ItemID and d.ApplyFamily = @AFamily and h.ID <> @refID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@df", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@df"].Value = df;
                objSQlComm.Parameters.Add(new SqlParameter("@dt", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@dt"].Value = dt;

                if (refID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@refID"].Value = refID;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ItemID"].Value = refItem;

                objSQlComm.Parameters.Add(new SqlParameter("@AFamily", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@AFamily"].Value = refFamily;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }


        #endregion
        
        #region Future Price Setup

        public DataTable FetchFuturePriceData(string configDateFormat)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,FutureBatchName,FutureBatchDesc,FutureBatchStatus,EffectiveFrom,IsSet "
                              + " from FuturePriceHeader order by FutureBatchName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FutureBatchName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FutureBatchDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FutureBatchStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
                dtbl.Columns.Add("isset", System.Type.GetType("System.String"));
                string chk1 = "";
                string val = "";

                while (objSQLReader.Read())
                {
                    val = "";
                    chk1 = "";

                    if (objSQLReader["FutureBatchStatus"].ToString() == "Y")
                    {
                        chk1 = "     Yes";
                    }
                    else
                    {
                        chk1 = "     No";
                    }

                    val = Convert.ToDateTime(objSQLReader["EffectiveFrom"].ToString()).ToString((configDateFormat) + "     hh:mm tt");


                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["FutureBatchName"].ToString(), 
                                                   objSQLReader["FutureBatchDesc"].ToString(), 
												   chk1,val,
                                                   objSQLReader["IsSet"].ToString() });
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

        public DataTable ShowFuturePriceHeader(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from FuturePriceHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FutureBatchName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FutureBatchDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FutureBatchStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EffectiveFrom", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["FutureBatchName"].ToString(),
												objSQLReader["FutureBatchDesc"].ToString(),
                                                objSQLReader["FutureBatchStatus"].ToString(),
                                                objSQLReader["EffectiveFrom"].ToString()
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

        public DataTable FetchFuturePriceDetails(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select * from FuturePriceDetails where FutureBatchID = @QID ";
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyFamily", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FuturePriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("FuturePriceB", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("FuturePriceC", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[]  {   objSQLReader["ID"].ToString(),
                                                    objSQLReader["ItemID"].ToString(),
                                                    "",
                                                    objSQLReader["ApplyFamily"].ToString(),
                                                    Functions.fnDouble(objSQLReader["FuturePriceA"].ToString()),
                                                    Functions.fnDouble(objSQLReader["FuturePriceB"].ToString()),
                                                    Functions.fnDouble(objSQLReader["FuturePriceC"].ToString())
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

        public bool InsertFuturePriceHeaderData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into FuturePriceHeader(FutureBatchName,FutureBatchDesc,FutureBatchStatus,EffectiveFrom,"
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn )"
                                  + " values( @FutureBatchName,@FutureBatchDesc,@FutureBatchStatus,@EffectiveFrom,"
                                  + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)"
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FutureBatchName"].Value = strFutureBatchName;
                objSQlComm.Parameters["@FutureBatchDesc"].Value = strFutureBatchDesc;
                objSQlComm.Parameters["@FutureBatchStatus"].Value = strFutureBatchStatus;
                objSQlComm.Parameters["@EffectiveFrom"].Value = dtEffectiveFrom;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;


                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }
        
        public bool UpdateFuturePriceHeaderData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update FuturePriceHeader set FutureBatchName=@FutureBatchName,FutureBatchDesc=@FutureBatchDesc,"
                                  + " FutureBatchStatus=@FutureBatchStatus,EffectiveFrom=@EffectiveFrom,"
                                  + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";


                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@EffectiveFrom", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@FutureBatchName"].Value = strFutureBatchName;
                objSQlComm.Parameters["@FutureBatchDesc"].Value = strFutureBatchDesc;
                objSQlComm.Parameters["@FutureBatchStatus"].Value = strFutureBatchStatus;
                objSQlComm.Parameters["@EffectiveFrom"].Value = dtEffectiveFrom;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;


                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustFuturePriceDetails(SqlCommand objSQlComm)
        {
            try
            {
                if (dtblFuturePriceData == null) return;
                foreach (DataRow dr in dtblFuturePriceData.Rows)
                {
                    string colID = "";
                    string colItemID = "";
                    string colFuturePriceA = "";
                    string colFuturePriceB = "";
                    string colFuturePriceC = "";
                    string colApplyFamily = "";

                    if (dr["ItemID"].ToString() == "") continue;

                    colItemID = dr["ItemID"].ToString();
                    colApplyFamily = dr["ApplyFamily"].ToString();
                    colFuturePriceA = dr["FuturePriceA"].ToString();
                    colFuturePriceB = dr["FuturePriceB"].ToString();
                    colFuturePriceC = dr["FuturePriceC"].ToString();


                    intItemID = Functions.fnInt32(colItemID);
                    strApplyFamily = colApplyFamily;
                    dblFuturePriceA = Functions.fnDouble(colFuturePriceA);
                    dblFuturePriceB = Functions.fnDouble(colFuturePriceB);
                    dblFuturePriceC = Functions.fnDouble(colFuturePriceC);

                    InsertFuturePriceDetails(objSQlComm);
                }
                dtblSalePriceData.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertFuturePriceDetails(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into FuturePriceDetails(FutureBatchID,ItemID,ApplyFamily,FuturePriceA,FuturePriceB,FuturePriceC,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @FutureBatchID,@ItemID,@ApplyFamily,@FuturePriceA,@FuturePriceB,@FuturePriceC,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@FutureBatchID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ApplyFamily", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FuturePriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FuturePriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FuturePriceC", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FutureBatchID"].Value = intDiscountID;
                objSQlComm.Parameters["@ItemID"].Value = intItemID;
                objSQlComm.Parameters["@ApplyFamily"].Value = strApplyFamily;
                objSQlComm.Parameters["@FuturePriceA"].Value = dblFuturePriceA;
                objSQlComm.Parameters["@FuturePriceB"].Value = dblFuturePriceB;
                objSQlComm.Parameters["@FuturePriceC"].Value = dblFuturePriceC;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool DeleteFuturePriceDetails(SqlCommand objSQlComm, int intpID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "delete from FuturePriceDetails where FutureBatchID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intpID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool ProcessFuturePrice()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertFuturePriceHeaderData(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateFuturePriceHeaderData(SaveComm);
                }

                DeleteFuturePriceDetails(SaveComm, intDiscountID);
                AdjustFuturePriceDetails(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public int DuplicateFutureBatch(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from FuturePriceHeader where  " + " FutureBatchName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                
                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public int DeleteFutureBatch(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deletefuturebatch";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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


        #endregion

        #region Buy N Get free

        public DataTable FetchBuyNGetFreeData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select h.ID,h.PromotionName,p.Description as BuyItem,h.BuyQty,h.PromotionStatus,"
                              + " h.ForLimitedPeriod,h.PromotionStartDate,h.PromotionEndDate "
                              + " from BuyNGetFreeHeader h left outer join Product p on p.ID = h.BuyProductID order by p.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Buy", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Free", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Status", System.Type.GetType("System.String"));

                string chk1 = "";
                string buyitem = "";
               
                while (objSQLReader.Read())
                {
                    buyitem = objSQLReader["BuyItem"].ToString() + "  (" + objSQLReader["BuyQty"].ToString() + ")";

                    if (objSQLReader["PromotionStatus"].ToString() == "Y")
                    {
                        chk1 = "Yes";
                    }
                    else
                    {
                        chk1 = "No";
                    }
                    if (objSQLReader["ForLimitedPeriod"].ToString() == "Y")
                    {
                        chk1 = chk1 + " ( "
                             + Convert.ToDateTime(objSQLReader["PromotionStartDate"].ToString()).ToString("d") + " - " + Convert.ToDateTime(objSQLReader["PromotionEndDate"].ToString()).ToString("d") + " )";
                    }
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   buyitem, "",
												   chk1 });
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

        public string FetchFreeData(int RefHeader)
        {
            string freeitem = "";

            string strSQLComm = " select p.Description as FreeItem,h.FreeQty "
                              + " from BuyNGetFreeDetails h left outer join Product p on p.ID = h.FreeProductID where h.HeaderID = @HID order by p.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@HID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@HID"].Value = RefHeader;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (freeitem != "") freeitem = freeitem + "\r\n";
                    freeitem = freeitem + objSQLReader["FreeItem"].ToString() + "  (" + objSQLReader["FreeQty"].ToString() + ")";
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return freeitem;
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

        public DataTable LookupBuyItem()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,SKU as CD,Description from product where productstatus = 'Y' and ProductType in ('P','M','U','K','E') order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["CD"].ToString(), 
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

        public DataTable LookupFreeItem(int BuyItem)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,SKU as CD,Description from product where productstatus = 'Y' and ProductType in ('P','M','U','K','E') and id <> @BuyID order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@BuyID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@BuyID"].Value = BuyItem;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["CD"].ToString(), 
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

        public int DuplicateBuynGetFreeCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from BuyNGetFreeHeader where PromotionName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public bool ProcessBuyNGetFree()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertBuyNGetFreeHeader(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateBuyNGetFreeHeader(SaveComm);
                }

                AdjustBuyNGetFreeDetails(SaveComm);

                intID = intDiscountID;
                SaveComm.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public bool InsertBuyNGetFreeHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into BuyNGetFreeHeader(PromotionName,PromotionDescription,PromotionStatus,BuyProductID,BuyQty, "
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,PromotionStartDate,PromotionEndDate,ForLimitedPeriod ) "
                                  + " values( @PromotionName,@PromotionDescription,@PromotionStatus,@BuyProductID,@BuyQty,"
                                  + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@PromotionStartDate,@PromotionEndDate,@ForLimitedPeriod ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@PromotionName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@BuyProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BuyQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@PromotionStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ForLimitedPeriod", System.Data.SqlDbType.Char));


                objSQlComm.Parameters["@PromotionName"].Value = strDiscountName;
                objSQlComm.Parameters["@PromotionDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@PromotionStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@BuyProductID"].Value = intBuyProductID;
                objSQlComm.Parameters["@BuyQty"].Value = intBuyQty;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@PromotionStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@PromotionStartDate"].Value = new DateTime(dtLimitedStartDate.Year, dtLimitedStartDate.Month, dtLimitedStartDate.Day, 0, 0, 0);
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@PromotionEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@PromotionEndDate"].Value = new DateTime(dtLimitedEndDate.Year, dtLimitedEndDate.Month, dtLimitedEndDate.Day, 23, 59, 59);
                }

                objSQlComm.Parameters["@ForLimitedPeriod"].Value = strLimitedPeriodCheck;
                
                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }

        public bool UpdateBuyNGetFreeHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update BuyNGetFreeHeader set PromotionName=@PromotionName,PromotionDescription=@PromotionDescription, "
                                  + " PromotionStatus=@PromotionStatus,BuyProductID=@BuyProductID,BuyQty=@BuyQty,"
                                  + " PromotionStartDate=@PromotionStartDate,PromotionEndDate=@PromotionEndDate,"
                                  + " ForLimitedPeriod=@ForLimitedPeriod where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@BuyProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BuyQty", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@PromotionStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@PromotionEndDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ForLimitedPeriod", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@PromotionName"].Value = strDiscountName;
                objSQlComm.Parameters["@PromotionDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@PromotionStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@BuyProductID"].Value = intBuyProductID;
                objSQlComm.Parameters["@BuyQty"].Value = intBuyQty;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@PromotionStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@PromotionStartDate"].Value = new DateTime(dtLimitedStartDate.Year, dtLimitedStartDate.Month, dtLimitedStartDate.Day, 0, 0, 0);
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@PromotionEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@PromotionEndDate"].Value = new DateTime(dtLimitedEndDate.Year, dtLimitedEndDate.Month, dtLimitedEndDate.Day, 23, 59, 59);
                }

                objSQlComm.Parameters["@ForLimitedPeriod"].Value = strLimitedPeriodCheck;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustBuyNGetFreeDetails(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;

                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    strDeleteFreeItem = dr["Del"].ToString();
                    intFreeProductID = Functions.fnInt32(dr["ItemID"].ToString());
                    intFreeQty = Functions.fnInt32(dr["Qty"].ToString());

                    intFreeRef = Functions.fnInt32(dr["ID"].ToString());

                    if ((intFreeRef == 0) && (strDeleteFreeItem == "Y")) continue;

                    if (intFreeRef == 0)
                    {
                        InsertFreeItems(objSQlComm);
                    }

                    if ((intFreeRef > 0) && (strDeleteFreeItem == "N"))
                    {
                        EditFreeItems(objSQlComm);
                    }

                    if ((intFreeRef > 0) && (strDeleteFreeItem == "Y"))
                    {
                        DeleteFreeItems(objSQlComm);
                    }
                }
                dblSplitDataTableDate.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertFreeItems(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " insert into BuyNGetFreeDetails(HeaderID,FreeProductID,FreeQty,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                    + " values ( @HeaderID,@FreeProductID,@FreeQty,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) ";
                

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@HeaderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FreeProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FreeQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@HeaderID"].Value = intDiscountID;
                objSQlComm.Parameters["@FreeProductID"].Value = intFreeProductID;
                objSQlComm.Parameters["@FreeQty"].Value = intFreeQty;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool EditFreeItems(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = " update BuyNGetFreeDetails set FreeProductID=FreeProductID,FreeQty=@FreeQty, "
                                  + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";


                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FreeProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FreeQty", System.Data.SqlDbType.Int));
                
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@ID"].Value = intFreeRef;
                objSQlComm.Parameters["@FreeProductID"].Value = intFreeProductID;
                objSQlComm.Parameters["@FreeQty"].Value = intFreeQty;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteFreeItems(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from BuyNGetFreeDetails where ID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intFreeRef;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public DataTable ShowBuyNGetFreeHeader(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from BuyNGetFreeHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("PromotionName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromotionDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BuyProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BuyQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromotionStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ForLimitedPeriod", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromotionStartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromotionEndDate", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] 
                                            {
                                                objSQLReader["PromotionName"].ToString(),
												objSQLReader["PromotionDescription"].ToString(),
                                                objSQLReader["BuyProductID"].ToString(),
                                                objSQLReader["BuyQty"].ToString(),
                                                objSQLReader["PromotionStatus"].ToString(),
                                                objSQLReader["ForLimitedPeriod"].ToString(),
                                                objSQLReader["PromotionStartDate"].ToString(),
                                                objSQLReader["PromotionEndDate"].ToString()
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

        public DataTable FetchFreeItemDetails(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select h.ID, h.FreeProductID, h.FreeQty, p.Description from BuyNGetFreeDetails h left outer join Product p "
                       + " on p.ID = h.FreeProductID where HeaderID = @H ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@H", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@H"].Value = intDID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Item", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Del", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SL", System.Type.GetType("System.String"));
                int i = 0;
                while (objSQLReader.Read())
                {
                    i++;
                    dtbl.Rows.Add(new object[] { objSQLReader["ID"].ToString(), objSQLReader["Description"].ToString(), objSQLReader["FreeQty"].ToString(), objSQLReader["FreeProductID"].ToString(), "N", i.ToString() });
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

        public bool IsOverlappingPromotion(int refID, int refBuyItem, int refBuyQty, bool refAppPeriod, DateTime df, DateTime dt)
        {
            bool bOverlap = true;
            string strSQLComm = "";
            if (refID == 0)
            {
                if (!refAppPeriod) strSQLComm = " select count(*) as rcnt from BuyNGetFreeHeader where promotionstatus = 'Y' and BuyProductID = @buyitem and BuyQty = @buyqty ";

                if (refAppPeriod) strSQLComm = " select count(*) as rcnt from BuyNGetFreeHeader where promotionstatus = 'Y' and BuyProductID = @buyitem and BuyQty = @buyqty "
                                             + " and ((@df between PromotionStartDate and PromotionEndDate) or (@dt between PromotionStartDate and PromotionEndDate)) ";
            }

            if (refID > 0)
            {
                if (!refAppPeriod) strSQLComm = " select count(*) as rcnt from BuyNGetFreeHeader where promotionstatus = 'Y' and BuyProductID = @buyitem and BuyQty = @buyqty and ID <> @refID ";
                if (refAppPeriod) strSQLComm =  " select count(*) as rcnt from BuyNGetFreeHeader where promotionstatus = 'Y' and BuyProductID = @buyitem and BuyQty = @buyqty "
                                              + " and ((@df between PromotionStartDate and PromotionEndDate) or (@dt between PromotionStartDate and PromotionEndDate)) and ID <> @refID ";

            }
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                objSQlComm.Parameters.Add(new SqlParameter("@buyitem", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@buyitem"].Value = refBuyItem;

                objSQlComm.Parameters.Add(new SqlParameter("@buyqty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@buyqty"].Value = refBuyQty;
                
                if (refAppPeriod)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@df", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@df"].Value = df;
                    objSQlComm.Parameters.Add(new SqlParameter("@dt", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@dt"].Value = dt;
                }


                if (refID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@refID"].Value = refID;
                }

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    bOverlap = Functions.fnInt32(objSQLReader["rcnt"].ToString()) > 0;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return bOverlap;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return true;
            }
        }


        public int DeleteBuyNGetFree(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_DeleteBuynGetFree";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        #endregion

        #region Markdown

        public DataTable FetchMarkdownData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,DiscountName,DiscountStatus,DiscountType,DiscountAmount,DiscountPercentage,"
                              + " ChkLimitedPeriod,LimitedStartDate,LimitedEndDate "
                              + " from Scale_Markdown order by DiscountName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.Double"));

                string chk1 = "";
                string chk2 = "";
                string val = "";



                while (objSQLReader.Read())
                {

                    chk1 = "";
                    chk2 = "";
                   
                   

                    if (objSQLReader["DiscountStatus"].ToString() == "Y")
                    {
                        chk1 = "     Yes";
                    }
                    else
                    {
                        chk1 = "     No";
                    }
                    if (objSQLReader["ChkLimitedPeriod"].ToString() == "Y")
                    {
                        chk1 = chk1 + "    ("
                             + Convert.ToDateTime(objSQLReader["LimitedStartDate"].ToString()).ToString("d") + " - " + Convert.ToDateTime(objSQLReader["LimitedEndDate"].ToString()).ToString("d") + "  )";
                    }
                    if (objSQLReader["DiscountType"].ToString() == "A")
                    {
                        chk2 = "Amount off";
                        val = objSQLReader["DiscountAmount"].ToString();
                    }
                    else
                    {
                        chk2 = "% off";
                        val = objSQLReader["DiscountPercentage"].ToString();
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DiscountName"].ToString(), 
												   chk1,chk2,Functions.fnDouble(val) });
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

        public DataTable FetchMarkdownDate(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,DateOfMonth from Scale_MarkdownDate where DiscountID = @QID order by DateOfMonth ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfMonth", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DateOfMonth"].ToString()});
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

        public DataTable FetchMarkdownTime(int intDID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,StartTime,EndTime from Scale_MarkdownTime where DiscountID = @QID order by startTime ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@QID"].Value = intDID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["StartTime"].ToString(),
                                                   objSQLReader["EndTime"].ToString()});
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

        public DataTable ShowMarkdownRecord(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Scale_Markdown where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("DiscountName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountPercentage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkDaysAvailable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkMonday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkTuesday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkWednesday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkThursday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkFriday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkSaturday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkSunday", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkAllDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkAllDate", System.Type.GetType("System.String"));
                
                dtbl.Columns.Add("ChkLimitedPeriod", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LimitedStartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LimitedEndDate", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["DiscountName"].ToString(),
												objSQLReader["DiscountDescription"].ToString(),
                                                objSQLReader["DiscountStatus"].ToString(),
                                                objSQLReader["DiscountType"].ToString(),
                                                objSQLReader["DiscountAmount"].ToString(),
                                                objSQLReader["DiscountPercentage"].ToString(),
                                               
                                                objSQLReader["ChkDaysAvailable"].ToString(),
                                                objSQLReader["ChkMonday"].ToString(),
                                                objSQLReader["ChkTuesday"].ToString(),
                                                objSQLReader["ChkWednesday"].ToString(),
                                                objSQLReader["ChkThursday"].ToString(),
                                                objSQLReader["ChkFriday"].ToString(),
                                                objSQLReader["ChkSaturday"].ToString(),
                                                objSQLReader["ChkSunday"].ToString(),
                                                objSQLReader["ChkAllDay"].ToString(),
                                                objSQLReader["ChkAllDate"].ToString(),
                                                
                                                objSQLReader["ChkLimitedPeriod"].ToString(),
                                                objSQLReader["LimitedStartDate"].ToString(),
                                                objSQLReader["LimitedEndDate"].ToString()
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

        public bool AddEditMarkdown()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertMarkdownHeaderData(SaveComm);
                }
                else
                {
                    intDiscountID = intID;
                    UpdateMarkdownHeaderData(SaveComm);
                }

                DeleteMarkdownTime(SaveComm, intDiscountID);
                AdjustSplitGridRecordMarkdownTime(SaveComm);

                DeleteMarkdownDate(SaveComm, intDiscountID);
                AdjustSplitGridRecordMarkdownDate(SaveComm);
                               

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        private void AdjustSplitGridRecordMarkdownTime(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableTime == null) return;
                foreach (DataRow dr in dblSplitDataTableTime.Rows)
                {
                    string colID = "";
                    string colSTime = "";
                    string colETime = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }
                    colSTime = dr["StartTime"].ToString();
                    colETime = dr["EndTime"].ToString();

                    strStartTime = colSTime;
                    strEndTime = colETime;

                    InsertMarkdownTime(objSQlComm);
                }

                dblSplitDataTableTime.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustSplitGridRecordMarkdownDate(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableDate == null) return;
                foreach (DataRow dr in dblSplitDataTableDate.Rows)
                {
                    string colID = "";

                    if (dr["DateValue"].ToString() == "") continue;

                    if (dr["DateValue"].ToString() != "")
                    {
                        colID = dr["DateValue"].ToString();
                    }
                    intDateOfMonth = Functions.fnInt32(colID);
                    InsertMarkdownDate(objSQlComm);
                }
                dblSplitDataTableDate.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeleteMarkdownTime(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from Scale_MarkdownTime where DiscountID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;

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

        public bool DeleteMarkdownDate(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from Scale_MarkdownDate where DiscountID = @DID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@DID"].Value = intDiscID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertMarkdownHeaderData(SqlCommand objSQlComm)
        {
            
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into Scale_Markdown(DiscountName,DiscountDescription,DiscountStatus,DiscountType,"
                                  + " DiscountAmount,DiscountPercentage,ChkDaysAvailable,ChkMonday,ChkTuesday,ChkWednesday,ChkThursday,ChkFriday,"
                                  + " ChkSaturday,ChkSunday,ChkAllDay,ChkAllDate,CreatedBy, CreatedOn,LastChangedBy,LastChangedOn, "
                                  + " ChkLimitedPeriod,LimitedStartDate,LimitedEndDate )"
                                  + " values( @DiscountName,@DiscountDescription,@DiscountStatus,@DiscountType,@DiscountAmount,@DiscountPercentage,"
                                  + " @ChkDaysAvailable,@ChkMonday,@ChkTuesday,@ChkWednesday,@ChkThursday,@ChkFriday,"
                                  + " @ChkSaturday,@ChkSunday,@ChkAllDay,@ChkAllDate,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn, "
                                  + " @ChkLimitedPeriod,@LimitedStartDate,@LimitedEndDate )"
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDaysAvailable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkMonday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTuesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkWednesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkThursday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFriday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSaturday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSunday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDay", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDate", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkLimitedPeriod", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@ChkDaysAvailable"].Value = strChkDaysAvailable;
                objSQlComm.Parameters["@ChkMonday"].Value = strChkMonday;
                objSQlComm.Parameters["@ChkTuesday"].Value = strChkTuesday;
                objSQlComm.Parameters["@ChkWednesday"].Value = strChkWednesday;
                objSQlComm.Parameters["@ChkThursday"].Value = strChkThursday;
                objSQlComm.Parameters["@ChkFriday"].Value = strChkFriday;
                objSQlComm.Parameters["@ChkSaturday"].Value = strChkSaturday;
                objSQlComm.Parameters["@ChkSunday"].Value = strChkSunday;
                objSQlComm.Parameters["@ChkAllDay"].Value = strChkAllDay;
                objSQlComm.Parameters["@ChkAllDate"].Value = strChkAllDate;
                
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@ChkLimitedPeriod"].Value = strChkLimitedPeriod;
                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = dtLimitedStartDate;
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = dtLimitedEndDate;
                }

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intDiscountID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }

        public bool UpdateMarkdownHeaderData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update Scale_Markdown set DiscountName=@DiscountName,DiscountDescription=@DiscountDescription, "
                                  + " DiscountStatus=@DiscountStatus,DiscountType=@DiscountType,DiscountAmount=@DiscountAmount,"
                                  + " DiscountPercentage=@DiscountPercentage,ChkDaysAvailable=@ChkDaysAvailable,"
                                  + " ChkMonday=@ChkMonday,ChkTuesday=@ChkTuesday,ChkWednesday=@ChkWednesday,"
                                  + " ChkThursday=@ChkThursday,ChkFriday=@ChkFriday,ChkSaturday=@ChkSaturday,ChkSunday=@ChkSunday,"
                                  + " ChkAllDay=@ChkAllDay,ChkAllDate=@ChkAllDate,"
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,"
                                  + " ChkLimitedPeriod=@ChkLimitedPeriod,LimitedStartDate=@LimitedStartDate,LimitedEndDate=@LimitedEndDate "
                                  + " where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDaysAvailable", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkMonday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTuesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkWednesday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkThursday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFriday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSaturday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkSunday", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDay", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAllDate", System.Data.SqlDbType.Char));
                

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ChkLimitedPeriod", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LimitedEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DiscountName"].Value = strDiscountName;
                objSQlComm.Parameters["@DiscountDescription"].Value = strDiscountDescription;
                objSQlComm.Parameters["@DiscountAmount"].Value = dblDiscountAmount;
                objSQlComm.Parameters["@DiscountPercentage"].Value = dblDiscountPercentage;
                objSQlComm.Parameters["@DiscountStatus"].Value = strDiscountStatus;
                objSQlComm.Parameters["@DiscountType"].Value = strDiscountType;
                objSQlComm.Parameters["@ChkDaysAvailable"].Value = strChkDaysAvailable;
                objSQlComm.Parameters["@ChkMonday"].Value = strChkMonday;
                objSQlComm.Parameters["@ChkTuesday"].Value = strChkTuesday;
                objSQlComm.Parameters["@ChkWednesday"].Value = strChkWednesday;
                objSQlComm.Parameters["@ChkThursday"].Value = strChkThursday;
                objSQlComm.Parameters["@ChkFriday"].Value = strChkFriday;
                objSQlComm.Parameters["@ChkSaturday"].Value = strChkSaturday;
                objSQlComm.Parameters["@ChkSunday"].Value = strChkSunday;
                objSQlComm.Parameters["@ChkAllDay"].Value = strChkAllDay;
                objSQlComm.Parameters["@ChkAllDate"].Value = strChkAllDate;
                

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@ChkLimitedPeriod"].Value = strChkLimitedPeriod;
                if (dtLimitedStartDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedStartDate"].Value = dtLimitedStartDate;
                }

                if (dtLimitedEndDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@LimitedEndDate"].Value = dtLimitedEndDate;
                }

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertMarkdownTime(SqlCommand objSQlComm)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into Scale_MarkdownTime(DiscountID,StartTime,EndTime,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @DiscountID,@StartTime,@EndTime,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StartTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EndTime", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@StartTime"].Value = strStartTime;
                objSQlComm.Parameters["@EndTime"].Value = strEndTime;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public bool InsertMarkdownDate(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into Scale_MarkdownDate(DiscountID,DateOfMonth,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @DiscountID,@DateOfMonth,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)  "
                                  + " select @@IDENTITY as ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMonth", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
                objSQlComm.Parameters["@DateOfMonth"].Value = intDateOfMonth;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intNewID = Functions.fnInt32(sqlDataReader["ID"].ToString());

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public int DuplicateMarkdownCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as RECCOUNT from Scale_Markdown where  " + " DiscountName = @ClassID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ClassID"].Value = clsID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["RECCOUNT"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public DataTable LookupMarkdown()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, DiscountName from scale_markdown where DiscountStatus = 'Y' and ((ChkLimitedPeriod = 'N') "
                              + " or ((ChkLimitedPeriod = 'Y') and getdate() between LimitedStartDate and LimitedEndDate)) order by DiscountName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                
                dtbl.Columns.Add("Markdown", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["DiscountName"].ToString(),
                                                   objSQLReader["ID"].ToString()});
                }
                dtbl.Rows.Add(new object[] { "(None)", "0" });
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


        public int GetMarkdownMap(int refscaledept)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(Markdown,0) as mapval from Scale_Markdown_Map where ScaleDepartment = @dept ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@dept", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@dept"].Value = refscaledept;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["mapval"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public int DeleteMarkdown(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_DeleteMarkdown";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        public int GetMarkdownLabelFormatMap(int refscaledept)
        {
            int intCount = 0;
            string strSQLComm = " select isnull(LabelFormat,0) as mapval from Scale_Markdown_Map where ScaleDepartment = @dept ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.Parameters.Add(new SqlParameter("@dept", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@dept"].Value = refscaledept;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["mapval"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
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

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        public bool ProceedMarkdownMap(string pHost)
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                
                DeleteMarkdownMap(SaveComm);
                DeleteMarkdownMappingForPrinter(SaveComm, pHost);
                AdjustSplitGridRecordMarkdownMap(SaveComm, pHost);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }


        public bool DeleteMarkdownMap(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from Scale_Markdown_Map ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool DeleteMarkdownMappingForPrinter(SqlCommand objSQlComm, string pHost)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from LocalSetup where HostName='" + pHost + "' and ParamName = 'Markdown Printer' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustSplitGridRecordMarkdownMap(SqlCommand objSQlComm, string pHost)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    InsertMarkdownMap(objSQlComm, Functions.fnInt32(dr["DepartmentID"].ToString()), Functions.fnInt32(dr["Markdown"].ToString()), Functions.fnInt32(dr["LabelFormat"].ToString()));
                    InsertMarkdownPrinterMap(objSQlComm, dr["DepartmentID"].ToString(), dr["Printer"].ToString(), pHost);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }


        public bool InsertMarkdownMap(SqlCommand objSQlComm, int mapDept, int mapMarkdown,int intmapMarkdownLabel)
        {
            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " insert into Scale_Markdown_Map(ScaleDepartment,Markdown,LabelFormat,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @ScaleDepartment,@Markdown,@LabelFormat, @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)  ";
                                  

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDepartment", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Markdown", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelFormat", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ScaleDepartment"].Value = mapDept;
                objSQlComm.Parameters["@Markdown"].Value = mapMarkdown;
                objSQlComm.Parameters["@LabelFormat"].Value = intmapMarkdownLabel;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();



                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertMarkdownPrinterMap(SqlCommand objSQlComm, string mapDept, string mapPrinter, string pHost)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = " insert into LocalSetup(HostName,ParamName,ParamValue, CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @HostName,@ParamName,@ParamValue, @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)  ";


                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = pHost;
                objSQlComm.Parameters["@ParamName"].Value = "Markdown Printer";
                objSQlComm.Parameters["@ParamValue"].Value = mapDept + "|" + mapPrinter;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID; 
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();



                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }



        public DataTable LookupPLU()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select distinct s.plu_number, p.Description  from scale_product s left outer join product p on p.ID = s.ProductID order by s.plu_number ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["plu_number"].ToString(), 
                                                   objSQLReader["plu_number"].ToString(), 
                                                   objSQLReader["Description"].ToString()});
                }

                dtbl.Rows.Add(new object[] {"0",strDataObjectCulture_All, strDataObjectCulture_All });
                dtbl.Rows.Add(new object[] { "-1",strDataObjectCulture_SelectPLU, strDataObjectCulture_SelectPLU });

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



        public DataTable FetchMarkdownDetailReportData(string refType, string refPLU, DataTable refdtblPLU, DataTable refdtblDept, DateTime pStart, DateTime pEnd, string configDateFormat)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 0);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 0);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;

            if (refType == "0") // Department
            {
                foreach (DataRow dr in refdtblDept.Rows)
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

            if ((refType == "1")) 
            {
                if (refPLU == "-1") // Selected PLU
                {
                    foreach (DataRow dr in refdtblPLU.Rows)
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["PLU"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + "'" + dr["PLU"].ToString() + "'";
                        }
                    }
                }
                else if (refPLU == "0") // All PLU
                {
                }
                else
                {
                    getdtblID = "'" + refPLU + "'"; // Specific PLU
                }
            }

            


            if (getdtblID != "")
            {
                if (refType == "0") strSQL1 = " and DeptRefID in ( " + getdtblID + " )";
                if (refType == "1")
                {
                    if (refPLU == "-1") // Selected PLU
                    {
                        strSQL1 = " and PLU in ( " + getdtblID + " )";
                    }
                    else if (refPLU == "0") // All PLU
                    {
                    }
                    else
                    {
                        if (refPLU != "0") strSQL1 = " and PLU = " + getdtblID;
                    }
                }
            }

            strSQLDate = " and ApplicableDate between @SDT and @TDT ";
            

            if (refType == "0")
            {
                strSQLComm = " select * from Scale_Markdown_Print where (1 = 1) " + strSQLDate + strSQL1
                           + " Order by ScaleDepartment, ApplicableDate ";
            }


            if (refType == "1")
            {
                strSQLComm = " select * from Scale_Markdown_Print where (1 = 1) " + strSQLDate + strSQL1
                           + " Order by PLU, ApplicableDate ";
            }

            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleDepartment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PreparedBy", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RetailPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MarkdownPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NewPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Savings", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {  Functions.fnDate(objSQLReader["ApplicableDate"].ToString()).ToString((configDateFormat)),
                                                   objSQLReader["PLU"].ToString(),
                                                   objSQLReader["ProductDescription"].ToString(),
												   objSQLReader["ScaleDepartment"].ToString(),
                                                   objSQLReader["Prepared_By"].ToString(),
                                                   objSQLReader["RetailPrice"].ToString(),
                                                   objSQLReader["MarkdownPrice"].ToString(),
                                                   objSQLReader["NewPrice"].ToString(), 
                                                   objSQLReader["Savings"].ToString()});
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


        public DataTable FetchMarkdownSummaryReportData(string refType, string refPLU, DataTable refdtblPLU, DataTable refdtblDept, DateTime pStart, DateTime pEnd, string configDateFormat)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 0);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 0);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;

            if (refType == "0") // Department
            {
                foreach (DataRow dr in refdtblDept.Rows)
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

            if ((refType == "1"))
            {
                if (refPLU == "-1") // Selected PLU
                {
                    foreach (DataRow dr in refdtblPLU.Rows)
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["PLU"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + "'" + dr["PLU"].ToString() + "'";
                        }
                    }
                }
                else if (refPLU == "0") // All PLU
                {
                }
                else
                {
                    getdtblID = "'" + refPLU + "'"; // Specific PLU
                }
            }




            if (getdtblID != "")
            {
                if (refType == "0") strSQL1 = " and DeptRefID in ( " + getdtblID + " )";
                if (refType == "1")
                {
                    if (refPLU == "-1") // Selected PLU
                    {
                        strSQL1 = " and PLU in ( " + getdtblID + " )";
                    }
                    else if (refPLU == "0") // All PLU
                    {
                    }
                    else
                    {
                        if (refPLU != "0") strSQL1 = " and PLU = " + getdtblID;
                    }
                }
            }

            strSQLDate = " and ApplicableDate between @SDT and @TDT ";


            if (refType == "0")
            {
                strSQLComm = " select ApplicableDate, DeptRefID as GroupPLU, ScaleDepartment as GroupDesc, count(PLU) as LabelCount, sum(savings) as TotalSave from scale_markdown_print where (1 = 1) " + strSQLDate + strSQL1
                           + " group  by ApplicableDate, DeptRefID, ScaleDepartment Order by ScaleDepartment, ApplicableDate ";
            }


            if (refType == "1")
            {
                strSQLComm = " select ApplicableDate, PLU as GroupPLU, ProductDescription as GroupDesc, count(PLU) as LabelCount, sum(savings) as TotalSave from scale_markdown_print where (1 = 1) " + strSQLDate + strSQL1
                           + " group  by ApplicableDate, PLU, ProductDescription  Order by PLU, ApplicableDate ";
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupPLU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSave", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {  Functions.fnDate(objSQLReader["ApplicableDate"].ToString()).ToString((configDateFormat)),
                                                   objSQLReader["GroupPLU"].ToString(),
                                                   objSQLReader["GroupDesc"].ToString(),
												   objSQLReader["LabelCount"].ToString(),
                                                   objSQLReader["TotalSave"].ToString()});
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


