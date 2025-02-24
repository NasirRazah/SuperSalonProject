/*
 purpose : Data for Fees & Charges
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Fees
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private int intNoOfChoices;
        private int intAnswerLevel;


        private int intAnswerID;
        private int intQuestionID;

        private string strFeesName;
        private string strFeesDescription;
        private string strFeesStatus;
        private string strFeesType;
        
        private string strChkRestrictedItems;
        private string strChkTax;
        private string strChkDiscount;
        private string strChkFoodStamp;
        private string strChkInclude;
        private string strChkAutoApply;
        private string strChkItemQty;
        private string strChkApplyItemTicket;
        private string strItemType;
        private int intItemID;
        private int intFeesID;

        private double dblFeesAmount;
        private double dblFeesPercentage;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        private DataTable dblSplitDataTableTax;
        private DataTable dblSplitDataTableRIG;
        private DataTable dblSplitDataTableRI;

        private DataTable dblSplitDataTableRF;
        private DataTable dblSplitDataTableRD;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string strErrorMsg;

        private int intDiscountFamilyID;
        private string strDiscountCategory;
        private int intDiscountPlusQty;


        #endregion

        #region definining public variables

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DataTable SplitDataTable
        {
            get { return dblSplitDataTable; }
            set { dblSplitDataTable = value; }
        }

        public DataTable SplitDataTableTax
        {
            get { return dblSplitDataTableTax; }
            set { dblSplitDataTableTax = value; }
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

        public DataTable SplitDataTableRF
        {
            get { return dblSplitDataTableRF; }
            set { dblSplitDataTableRF = value; }
        }

        public DataTable SplitDataTableRD
        {
            get { return dblSplitDataTableRD; }
            set { dblSplitDataTableRD = value; }
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

        public string FeesName
        {
            get { return strFeesName; }
            set { strFeesName = value; }
        }

        public string FeesDescription
        {
            get { return strFeesDescription; }
            set { strFeesDescription = value; }
        }

        public string FeesStatus
        {
            get { return strFeesStatus; }
            set { strFeesStatus = value; }
        }

        public string FeesType
        {
            get { return strFeesType; }
            set { strFeesType = value; }
        }              

        public string ChkTax
        {
            get { return strChkTax; }
            set { strChkTax = value; }
        }

        public string ChkItemQty
        {
            get { return strChkItemQty; }
            set { strChkItemQty = value; }
        }

        public string ChkDiscount
        {
            get { return strChkDiscount; }
            set { strChkDiscount = value; }
        }

        public string ChkRestrictedItems
        {
            get { return strChkRestrictedItems; }
            set { strChkRestrictedItems = value; }
        }

        public string ChkAutoApply
        {
            get { return strChkAutoApply; }
            set { strChkAutoApply = value; }
        }

        public string ChkApplyItemTicket
        {
            get { return strChkApplyItemTicket; }
            set { strChkApplyItemTicket = value; }
        }

        public double FeesAmount
        {
            get { return dblFeesAmount; }
            set { dblFeesAmount = value; }
        }

        public double FeesPercentage
        {
            get { return dblFeesPercentage; }
            set { dblFeesPercentage = value; }
        }

        public string ChkFoodStamp
        {
            get { return strChkFoodStamp; }
            set { strChkFoodStamp = value; }
        }

        public string ChkInclude
        {
            get { return strChkInclude; }
            set { strChkInclude = value; }
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

        public bool AddEditFees()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                if (intID == 0)
                {
                    InsertFeesMasterData(SaveComm);
                }
                else
                {
                    intFeesID = intID;
                    UpdateFeesMasterData(SaveComm);
                }

                DeleteTaxParts(SaveComm);
                SaveTaxParts(SaveComm);
                                
                DeleteFeesRestrictedItems(SaveComm, intFeesID);
                AdjustSplitGridRecordFamily(SaveComm);
                AdjustSplitGridRecordDept(SaveComm);
                AdjustSplitGridRecordGroupItem(SaveComm);
                AdjustSplitGridRecordItem(SaveComm);
                
                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }
        
        public string SaveTaxParts(SqlCommand objSQlComm)
        {
            if (dblSplitDataTableTax == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblSplitDataTableTax.Rows)
            {
                if (dr["TaxID"].ToString() == "") continue;
                if (dr["TaxID"].ToString() == "0") continue;
                int intTaxID = 0;
                intTaxID = Functions.fnInt32(dr["TaxID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertFeesTax(objSQlComm, intTaxID);
                }
            }
            return strResult;
        }

        public string InsertFeesTax(SqlCommand objSQlComm, int TaxID)
        {
            string strSQLComm = " insert into TaxMapping(TaxID, MappingType, MappingID,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                              + " values(@TaxID,@MappingType,@MappingID,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@TaxID"].Value = TaxID;
                objSQlComm.Parameters["@MappingType"].Value = "Fees";
                objSQlComm.Parameters["@MappingID"].Value = intFeesID;

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
            string strSQLComm = " delete from TaxMapping where MappingType = @MappingType and MappingID = @MappingID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@MappingType"].Value = "Fees";
                objSQlComm.Parameters["@MappingID"].Value = intFeesID;

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

        private void AdjustSplitGridRecordFamily(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableRF == null) return;
                foreach (DataRow dr in dblSplitDataTableRF.Rows)
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

                    InsertRestrictedDiscount(objSQlComm, "F");
                }
                dblSplitDataTableRF.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustSplitGridRecordDept(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTableRD == null) return;
                foreach (DataRow dr in dblSplitDataTableRD.Rows)
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

                    InsertRestrictedDiscount(objSQlComm, "D");
                }
                dblSplitDataTableRD.Dispose();
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

                    InsertRestrictedDiscount(objSQlComm, "G");
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

        public bool DeleteFeesRestrictedItems(SqlCommand objSQlComm, int intDiscID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "delete from FeesRestrictedItems where FeesID = @DID ";

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

        public bool InsertFeesMasterData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into FeesMaster ( FeesName,FeesDescription,FeesStatus,FeesType,FeesAmount,FeesPercentage,"
                                  + " ChkRestrictedItems,ChkTax,ChkDiscount,ChkFoodStamp,ChkInclude,ChkAutoApply,ChkItemQty,ChkApplyItemTicket,"
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                                  + " values( @FeesName,@FeesDescription,@FeesStatus,@FeesType,@FeesAmount,@FeesPercentage,"
                                  + " @ChkRestrictedItems,@ChkTax,@ChkDiscount,@ChkFoodStamp,@ChkInclude,@ChkAutoApply,@ChkItemQty,@ChkApplyItemTicket,"
                                  + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)"
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@FeesName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkRestrictedItems", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTax", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDiscount", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFoodStamp", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkInclude", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAutoApply", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkItemQty", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyItemTicket", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FeesName"].Value = strFeesName;
                objSQlComm.Parameters["@FeesDescription"].Value = strFeesDescription;
                objSQlComm.Parameters["@FeesAmount"].Value = dblFeesAmount;
                objSQlComm.Parameters["@FeesPercentage"].Value = dblFeesPercentage;
                objSQlComm.Parameters["@FeesStatus"].Value = strFeesStatus;
                objSQlComm.Parameters["@FeesType"].Value = strFeesType;
                objSQlComm.Parameters["@ChkRestrictedItems"].Value = strChkRestrictedItems;
                objSQlComm.Parameters["@ChkTax"].Value = strChkTax;
                objSQlComm.Parameters["@ChkDiscount"].Value = strChkDiscount;
                objSQlComm.Parameters["@ChkFoodStamp"].Value = strChkFoodStamp;
                objSQlComm.Parameters["@ChkInclude"].Value = strChkInclude;
                objSQlComm.Parameters["@ChkAutoApply"].Value = strChkAutoApply;
                objSQlComm.Parameters["@ChkItemQty"].Value = strChkItemQty;
                objSQlComm.Parameters["@ChkApplyItemTicket"].Value = strChkApplyItemTicket;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intFeesID = Functions.fnInt32(sqlDataReader["ID"].ToString());

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

        public bool UpdateFeesMasterData(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update FeesMaster set FeesName=@FeesName,FeesDescription=@FeesDescription, "
                                  + " FeesStatus=@FeesStatus,FeesType=@FeesType,FeesAmount=@FeesAmount,FeesPercentage=@FeesPercentage,"
                                  + " ChkRestrictedItems=@ChkRestrictedItems,ChkTax=@ChkTax,ChkDiscount=@ChkDiscount,"
                                  + " ChkFoodStamp=@ChkFoodStamp,ChkInclude=@ChkInclude,ChkAutoApply=@ChkAutoApply,ChkApplyItemTicket=@ChkApplyItemTicket,"
                                  + " ChkItemQty=@ChkItemQty,LastChangedBy=@LastChangedBy, LastChangedOn= @LastChangedOn where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesPercentage", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FeesType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkRestrictedItems", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkTax", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkDiscount", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkFoodStamp", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkInclude", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkAutoApply", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkItemQty", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ChkApplyItemTicket", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@FeesName"].Value = strFeesName;
                objSQlComm.Parameters["@FeesDescription"].Value = strFeesDescription;
                objSQlComm.Parameters["@FeesAmount"].Value = dblFeesAmount;
                objSQlComm.Parameters["@FeesPercentage"].Value = dblFeesPercentage;
                objSQlComm.Parameters["@FeesStatus"].Value = strFeesStatus;
                objSQlComm.Parameters["@FeesType"].Value = strFeesType;
                objSQlComm.Parameters["@ChkRestrictedItems"].Value = strChkRestrictedItems;
                objSQlComm.Parameters["@ChkTax"].Value = strChkTax;
                objSQlComm.Parameters["@ChkDiscount"].Value = strChkDiscount;
                objSQlComm.Parameters["@ChkFoodStamp"].Value = strChkFoodStamp;
                objSQlComm.Parameters["@ChkInclude"].Value = strChkInclude;
                objSQlComm.Parameters["@ChkAutoApply"].Value = strChkAutoApply;
                objSQlComm.Parameters["@ChkItemQty"].Value = strChkItemQty;
                objSQlComm.Parameters["@ChkApplyItemTicket"].Value = strChkApplyItemTicket;

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

        public bool InsertRestrictedDiscount(SqlCommand objSQlComm, string pType)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into FeesRestrictedItems(FeesID,ItemType,ItemID,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                                  + " values ( @FeesID,@ItemType,@ItemID,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@FeesID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FeesID"].Value = intFeesID;
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

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,FeesName,FeesStatus,FeesType,FeesAmount,FeesPercentage,"
                              + " ChkTax,ChkFoodStamp,ChkDiscount,ChkInclude,ChkAutoApply,ChkItemQty,ChkApplyItemTicket from FeesMaster order by FeesName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ApplyTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyFoodStamp", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyInclude", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyAuto", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApplyItemQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkApplyItemTicket", System.Type.GetType("System.String"));
                string chk = "";
                string chk1 = "";
                string chk2 = "";
                string chk3 = "";
                string chk4 = "";
                string chk5 = "";
                string chk6 = "";
                string chk7 = "";
                string val = "";
                string chk8 = "";
                while (objSQLReader.Read())
                {
                    string dtxt = "";
                    chk1 = "";
                    chk2 = "";


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
                        chk = "Amount add";
                        val = objSQLReader["FeesAmount"].ToString();
                    }
                    else
                    {
                        chk = "% add";
                        val = objSQLReader["FeesPercentage"].ToString();
                    }

                    if (objSQLReader["ChkApplyItemTicket"].ToString() == "I")
                    {
                        chk8 = "Item";
                    }
                    if (objSQLReader["ChkApplyItemTicket"].ToString() == "T")
                    {
                        chk8 = "Ticket";
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["FeesName"].ToString(), 
												   chk1,chk,Functions.fnDouble(val),
                                                   chk2,chk3,chk4,chk5 ,chk6,chk7,chk8});
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
                strSQLComm = " select a.ItemID as ID, c.Description as Description from FeesRestrictedItems a left outer join "
                           + " Category c on a.ItemID = c.ID where a.FeesID = @QID and a.ItemType = 'G' ";
            if (cat == "I")
                strSQLComm = " select a.ItemID as ID, c.Description as Description from FeesRestrictedItems a left outer join "
                           + " Product c on a.ItemID = c.ID where a.FeesID = @QID and a.ItemType = 'I' ";

            if (cat == "F")
                strSQLComm = " select a.ItemID as ID, c.BrandDescription as Description from FeesRestrictedItems a left outer join "
                           + " BrandMaster c on a.ItemID = c.ID where a.FeesID = @QID and a.ItemType = 'F' ";

            if (cat == "D")
                strSQLComm = " select a.ItemID as ID, c.Description as Description from FeesRestrictedItems a left outer join "
                           + " Dept c on a.ItemID = c.ID where a.FeesID = @QID and a.ItemType = 'D' ";

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

        #endregion

        #region Show Record based on ID

        public DataTable ShowRecord(int PID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from FeesMaster where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

            objSQlComm.Parameters["@ID"].Value = PID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FeesName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesPercentage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkFoodStamp", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkInclude", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkRestrictedItems", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkAutoApply", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkItemQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ChkApplyItemTicket", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["FeesName"].ToString(),
												objSQLReader["FeesDescription"].ToString(),
                                                objSQLReader["FeesStatus"].ToString(),
                                                objSQLReader["FeesType"].ToString(),
                                                objSQLReader["FeesAmount"].ToString(),
                                                objSQLReader["FeesPercentage"].ToString(),
                                                objSQLReader["ChkTax"].ToString(),
                                                objSQLReader["ChkDiscount"].ToString(),
                                                objSQLReader["ChkFoodStamp"].ToString(),
                                                objSQLReader["ChkInclude"].ToString(),
                                                objSQLReader["ChkRestrictedItems"].ToString(),
                                                objSQLReader["ChkAutoApply"].ToString(),
                                                objSQLReader["ChkItemQty"].ToString(),
                                                objSQLReader["ChkApplyItemTicket"].ToString()
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

        public int DeleteFees(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deletefees";

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
            string strSQLComm = " select count(*) AS rcnt from FeesMaster where FeesName = @ClassID ";

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


        #endregion

        public DataTable LookupProductORCategory(int optionID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (optionID == 1) strSQLComm = " select ID,Description from product where productstatus = 'Y' order by Description ";
            if (optionID == 0) strSQLComm = " select ID,Description from category order by Description ";

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


    }
}



