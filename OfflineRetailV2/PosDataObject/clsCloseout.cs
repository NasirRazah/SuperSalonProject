/*
 purpose : Data for Closeout
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Closeout
    {
        private SqlConnection sqlConn;
        private int intLoginUserID;
        private int intCloseOutID;
        private string strCloseoutType;
        private string strNotes;
        private int intTenderID;
        private double dblTenderAmount;
        private string strErrorMsg;
        private string strTerminalName;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;
        private DataTable dtblTenderDataTable;
        private DataTable dtblCurrencyCalculateDataTable;
        private string strFileType;
        private string strFileName;
        private string strFilePath;
        private DateTime dtFileCreatedDate;
        private int intFileID;

        private string strPennyCount;
        private string strNickelCount;
        private string strDimeCount;
        private string strQuarterCount;
        private string strHalveCount;
        private string strOneCount;
        private string strFiveCount;
        private string strTenCount;
        private string strTwentyCount;
        private string strFiftyCount;
        private string strHundredCount;

        private string strTwoCount;
        private string strTwoHundredCount;
        private string strFiveHundredCount;
        private string strTwoPennyCount;
        private string strTwentyPennyCount;
        private string strOneThousandCount;

        public int FetchCashFloatCount(int closeoutId)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from CashFloat where CloseoutID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = closeoutId;

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
        public int FetchTransactionCount(int closeoutId)
        {
            int intCount = 0;
            string strSQLComm = " select TransactionCnt from CloseOut where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = closeoutId;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intCount = Functions.fnInt32(objsqlReader["TransactionCnt"]);
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
        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }
        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }

        public DataTable CurrencyCalculateDataTable
        {
            get { return dtblCurrencyCalculateDataTable; }
            set { dtblCurrencyCalculateDataTable = value; }
        }

        public DataTable TenderDataTable
        {
            get { return dtblTenderDataTable; }
            set { dtblTenderDataTable = value; }
        }

        public int CloseOutID
        {
            get { return intCloseOutID; }
            set { intCloseOutID = value; }
        }

        public string CloseoutType
        {
            get { return strCloseoutType; }
            set { strCloseoutType = value; }
        }

        public string Notes
        {
            get { return strNotes; }
            set { strNotes = value; }
        }

        public string TerminalName
        {
            get { return strTerminalName; }
            set { strTerminalName = value; }
        }

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
        }

        public string FileType
        {
            get { return strFileType; }
            set { strFileType = value; }
        }

        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        public string FilePath
        {
            get { return strFilePath; }
            set { strFilePath = value; }
        }

        public DateTime FileCreatedDate
        {
            get { return dtFileCreatedDate; }
            set { dtFileCreatedDate = value; }
        }

        public int FileID
        {
            get { return intFileID; }
            set { intFileID = value; }
        }


        #region Fetch Close Out ID
        public int FetchCloseoutID()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_FetchCloseoutID";

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
                objSQlComm.Parameters["@User"].Value = intLoginUserID;

                objSQlComm.Parameters.Add(new SqlParameter("@TerminalName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@TerminalName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TerminalName"].Value = strTerminalName;

                objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();

                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value);

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

        #region Execute Closeout Report Procedure

        public void ExecuteCloseoutReportProc(string CType, int CID, string Terminal, string taxinclude, string syncf)
        {

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

                string strSQLComm = "sp_CloseoutReportHeader";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;
                objSQlComm.CommandTimeout = 500;

                objSQlComm.Parameters.Add(new SqlParameter("@tax_inclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@tax_inclusive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@tax_inclusive"].Value = taxinclude;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CloseoutType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutType"].Value = CType;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CloseoutID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutID"].Value = CID;

                objSQlComm.Parameters.Add(new SqlParameter("@Terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Terminal"].Value = Terminal;

                objSQlComm.Parameters.Add(new SqlParameter("@Sync", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Sync"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Sync"].Value = syncf;

                objSQlComm.ExecuteNonQuery();
                
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
        }

        #endregion

        #region Fetch Current Close Out Data

        public int FetchCurrentCloseoutData(string CType, int CID, ref int NoOfSale,ref int NoOfNoSale,
                                            ref int NoOfPaidouts,ref int NoOfLayawaypmt )
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_CurrentCloseoutData";

            try
            {

                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CloseoutType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutType"].Value = CType;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CloseoutID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutID"].Value = CID;

                objSQlComm.Parameters.Add(new SqlParameter("@NoOfSale", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NoOfSale"].Direction = ParameterDirection.Output;

                objSQlComm.Parameters.Add(new SqlParameter("@NoOfNoSale", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NoOfNoSale"].Direction = ParameterDirection.Output;

                objSQlComm.Parameters.Add(new SqlParameter("@NoOfPaidouts", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NoOfPaidouts"].Direction = ParameterDirection.Output;

                objSQlComm.Parameters.Add(new SqlParameter("@NoOfLayawaypmt", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@NoOfLayawaypmt"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                
                NoOfSale = Functions.fnInt32(objSQlComm.Parameters["@NoOfSale"].Value);
                NoOfNoSale = Functions.fnInt32(objSQlComm.Parameters["@NoOfNoSale"].Value);
                NoOfPaidouts = Functions.fnInt32(objSQlComm.Parameters["@NoOfPaidouts"].Value);
                NoOfLayawaypmt = Functions.fnInt32(objSQlComm.Parameters["@NoOfLayawaypmt"].Value);
                
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

        #region Fetch Closeout Tender Record 

        public DataTable ShowTenderRecord(string strRecordType, int intCloseOutID, string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutReportTender where CloseOutID = @CloseOutID and "
                                + " RecordType = @RecordType and reportterminalname=@T order by TenderID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CloseOutID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CloseOutID"].Value = intCloseOutID;
            objSQlComm.Parameters.Add(new SqlParameter("@RecordType", System.Data.SqlDbType.Char));
            objSQlComm.Parameters["@RecordType"].Value = strRecordType;
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TenderName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["TenderName"].ToString(),
                                                objSQLReader["TenderCount"].ToString() == "0" ? "" : objSQLReader["TenderCount"].ToString(),
                                                Functions.fnDouble(objSQLReader["TenderAmount"].ToString()) == -929292 ? "" :
                                                objSQLReader["TenderAmount"].ToString()});
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

        public DataTable ShowSalesByDept_QuickExport(int intCloseOutID, string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutSalesDept where CloseOutID = @CloseOutID and reportterminalname=@T order by DeptID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CloseOutID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CloseOutID"].Value = intCloseOutID;
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalesAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["DeptID"].ToString(),
												objSQLReader["DeptDesc"].ToString(),
												objSQLReader["SalesAmount"].ToString()});
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

        #region Fetch Closeout Header Record for Report
        
        public DataTable ShowHeaderRecord(string Terminal, string dateformat)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CloseOutReportMain where reportterminalname=@T order by CloseoutID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TaxedSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonTaxedSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OtherSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountItemAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountInvoiceAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayDeposits", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayRefund", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayPayment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawaySalesPosted", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaidOuts", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCissued", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCredeemed", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HACharged", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HApayments", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoOfSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSales_PreTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CostOfGoods", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoSaleCount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RentSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDeposit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDepositReturned", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairSales", System.Type.GetType("System.String"));

                dtbl.Columns.Add("STax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STax3Amount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("BTax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BTax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BTax3Amount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RntTax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RntTax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RntTax3Amount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RTax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RTax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RTax3Amount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("SDiscountItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SDiscountItemAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BDiscountItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BDiscountItemAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RDiscountItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RDiscountItemAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RntDiscountInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RntDiscountInvoiceAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RDiscountInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RDiscountInvoiceAmount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("SalesInvoiceCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentInvoiceCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairInvoiceCount", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ProductTx", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductNTx", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ServiceTx", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceNTx", System.Type.GetType("System.String"));

                dtbl.Columns.Add("OtherTx", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OtherNTx", System.Type.GetType("System.String"));

                dtbl.Columns.Add("CashTip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CCTip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tip", System.Type.GetType("System.String"));

                dtbl.Columns.Add("SalesFees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalesFeesTax", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RentFees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentFeesTax", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RepairFees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairFeesTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTaxText", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MGCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PGCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DGCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLGCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BottleRefund", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentGCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairDeposit", System.Type.GetType("System.String"));

                dtbl.Columns.Add("FreeQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FreeAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LottoPayout", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GiftAid", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["TaxedSales"].ToString(),
												objSQLReader["NonTaxedSales"].ToString(),
												objSQLReader["Tax1Exist"].ToString(),
                                                objSQLReader["Tax1Name"].ToString(),
												objSQLReader["Tax1Amount"].ToString(),
												objSQLReader["Tax2Exist"].ToString(),
                                                objSQLReader["Tax2Name"].ToString(),
												objSQLReader["Tax2Amount"].ToString(),
												objSQLReader["Tax3Exist"].ToString(),
                                                objSQLReader["Tax3Name"].ToString(),
												objSQLReader["Tax3Amount"].ToString(),
												objSQLReader["ServiceSales"].ToString(),
                                                objSQLReader["ProductSales"].ToString(),
												objSQLReader["OtherSales"].ToString(),
												objSQLReader["DiscountItemNo"].ToString(),
                                                objSQLReader["DiscountItemAmount"].ToString(),
												objSQLReader["DiscountInvoiceNo"].ToString(),
												objSQLReader["DiscountInvoiceAmount"].ToString(),
                                                objSQLReader["LayawayDeposits"].ToString(),
												objSQLReader["LayawayRefund"].ToString(),
												objSQLReader["LayawayPayment"].ToString(),
                                                objSQLReader["LayawaySalesPosted"].ToString(),
												objSQLReader["PaidOuts"].ToString(),
												objSQLReader["GCsold"].ToString(),
                                                objSQLReader["SCissued"].ToString(),
												objSQLReader["SCredeemed"].ToString(),
												objSQLReader["HACharged"].ToString(),
                                                objSQLReader["HApayments"].ToString(),
                                                objSQLReader["NoOfSales"].ToString(),
                                                objSQLReader["CloseoutID"].ToString(),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToString(dateformat),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToString(dateformat),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["CloseoutType"].ToString(),
                                                objSQLReader["Notes"].ToString(),objSQLReader["EmpID"].ToString(),
                                                objSQLReader["TerminalName"].ToString(),
                                                objSQLReader["TotalSales_PreTax"].ToString(),
                                                objSQLReader["CostOfGoods"].ToString(),
                                                objSQLReader["NoSaleCount"].ToString(),
                    
                                                objSQLReader["RentSales"].ToString(),
                                                objSQLReader["RentDeposit"].ToString(),
                                                objSQLReader["RentDepositReturned"].ToString(),
                                                objSQLReader["RepairSales"].ToString(),
                    
                                                objSQLReader["ServiceTax1Amount"].ToString(),
                                                objSQLReader["ServiceTax2Amount"].ToString(),
                                                objSQLReader["ServiceTax3Amount"].ToString(),
                    
                                                objSQLReader["OtherTax1Amount"].ToString(),
                                                objSQLReader["OtherTax2Amount"].ToString(),
                                                objSQLReader["OtherTax3Amount"].ToString(),
                    
                                                objSQLReader["RentTax1Amount"].ToString(),
                                                objSQLReader["RentTax2Amount"].ToString(),
                                                objSQLReader["RentTax3Amount"].ToString(),
                            
                                                objSQLReader["RepairTax1Amount"].ToString(),
                                                objSQLReader["RepairTax2Amount"].ToString(),
                                                objSQLReader["RepairTax3Amount"].ToString(),

                                                objSQLReader["ServiceDiscountItemNo"].ToString(),
                                                objSQLReader["ServiceDiscountItemAmount"].ToString(),
                                                objSQLReader["OtherDiscountItemNo"].ToString(),
                                                objSQLReader["OtherDiscountItemAmount"].ToString(),
                                                objSQLReader["RepairDiscountItemNo"].ToString(),
                                                objSQLReader["RepairDiscountItemAmount"].ToString(),
                                                objSQLReader["RentDiscountInvoiceNo"].ToString(),
                                                objSQLReader["RentDiscountInvoiceAmount"].ToString(),
                                                objSQLReader["RepairDiscountInvoiceNo"].ToString(),
                                                objSQLReader["RepairDiscountInvoiceAmount"].ToString(),

                                                objSQLReader["SalesInvoiceCount"].ToString(),
                                                objSQLReader["RentInvoiceCount"].ToString(),
                                                objSQLReader["RepairInvoiceCount"].ToString(),

                                                objSQLReader["ProductTx"].ToString(),
                                                objSQLReader["ProductNTx"].ToString(),

                                                objSQLReader["ServiceTx"].ToString(),
                                                objSQLReader["ServiceNTx"].ToString(),

                                                objSQLReader["OtherTx"].ToString(),
                                                objSQLReader["OtherNTx"].ToString(),
                                                Functions.fnDouble(objSQLReader["CashTip"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["CCTip"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["CashTip"].ToString()).ToString("f") + " / " +  Functions.fnDouble(objSQLReader["CCTip"].ToString()).ToString("f"),

                                                objSQLReader["SalesFees"].ToString(),
                                                objSQLReader["SalesFeesTax"].ToString(),
                                                objSQLReader["RentFees"].ToString(),
                                                objSQLReader["RentFeesTax"].ToString(),
                                                objSQLReader["RepairFees"].ToString(),
                                                objSQLReader["RepairFeesTax"].ToString(),
                                                Functions.fnDouble(objSQLReader["DTax"].ToString()) == 0 ? "" : "Destination Tax",
                                                objSQLReader["DTax"].ToString(),
                                                objSQLReader["MGCsold"].ToString(),
                                                objSQLReader["PGCsold"].ToString(),
                                                objSQLReader["DGCsold"].ToString(),
                                                objSQLReader["PLGCsold"].ToString(),
                                                objSQLReader["BottleRefund"].ToString(),
                                                (Functions.fnDouble(objSQLReader["MGCsold"].ToString()) + Functions.fnDouble(objSQLReader["PGCsold"].ToString())  + Functions.fnDouble(objSQLReader["DGCsold"].ToString()) + Functions.fnDouble(objSQLReader["PLGCsold"].ToString())).ToString("f"),
                                                objSQLReader["RepairDeposit"].ToString(),
                                                objSQLReader["Free_Qty"].ToString(),
                                                Functions.fnDouble(objSQLReader["Free_Amount"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["LottoPayout"].ToString()).ToString("f"),
                                                Functions.fnDouble(objSQLReader["GiftAid"].ToString()).ToString("f")
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

        #region Fetch Closeout Return Record

        public DataTable ShowReturnedRecord(string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutReturn where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ReturnSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReturnInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReturnAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ReturnSKU"].ToString(),
												objSQLReader["ReturnInvoiceNo"].ToString(),
												objSQLReader["ReturnAmount"].ToString()});
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

        #region Fetch Closeout Sales By Hour Record

        public DataTable ShowSalesByHourRecord(string Terminal, string dateformat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutSalesHour where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Timeinterval", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalesAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoOfSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["Timeinterval"].ToString(),
												objSQLReader["SalesAmount"].ToString(),
                                                objSQLReader["NoOfSales"].ToString(),
                                                objSQLReader["CloseoutID"].ToString(),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToString(dateformat),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToShortTimeString(),
                                               objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToString(dateformat),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["CloseoutType"].ToString(),
                                                objSQLReader["Notes"].ToString(),objSQLReader["EmpID"].ToString(),
                                                objSQLReader["TerminalName"].ToString()});
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

        #region Fetch Closeout Sales By Department Record

        public DataTable ShowSalesByDeptRecord(string Terminal, string dateformat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutSalesDept where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalesAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoOfSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["DeptID"].ToString(),
                                                objSQLReader["DeptDesc"].ToString(),
												objSQLReader["SalesAmount"].ToString(),
                                                objSQLReader["NoOfSales"].ToString(),
                                                objSQLReader["CloseoutID"].ToString(),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToString(dateformat),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToString(dateformat),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["CloseoutType"].ToString(),
                                                objSQLReader["Notes"].ToString(),objSQLReader["EmpID"].ToString(),
                                                objSQLReader["TerminalName"].ToString()});
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

        #region Fetch Closeout Count Tender

        public DataTable FetchBlindTender(string CType, int CID, int TID )
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (CType == "C")
            {
                strSQLComm =        " select count(*) as CountRec, isnull(sum(t.tenderamount),0) as SAmount "
                                  + " from tender t left outer join trans tr "
                                  + " on tr.ID = t.TransactionNo where t.TenderType = @TID and tr.CloseoutID in "
                                  + " ( select c.ID from Closeout c where c.consolidatedID = @CID ) "
                                  + " and tr.ID  not in (select i.transactionno from invoice i where  i.TransactionNo = tr.ID "
                                  + " and i.id in ( select v.InvoiceNo from VoidInv v)) and tr.TransType not in (91,92) ";
            }

            if ((CType == "E") || (CType == "T"))
            {
                strSQLComm = " select count(*) as CountRec, isnull(sum(t.tenderamount),0) as SAmount "
                                  + " from tender t left outer join trans tr "
                                  + " on tr.ID = t.TransactionNo where t.TenderType = @TID and tr.CloseoutID = @CID "
                                  + " and tr.ID  not in (select i.transactionno from invoice i where  i.TransactionNo = tr.ID "
                                  + " and i.id in ( select v.InvoiceNo from VoidInv v)) and tr.TransType not in (91,92) ";
            }
            

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CID"].Value = CID;
            objSQlComm.Parameters.Add(new SqlParameter("@TID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@TID"].Value = TID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ECount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EReceipt", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["CountRec"].ToString(),
                                                objSQLReader["SAmount"].ToString()});
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

        #region Fetch Valid Closeout ID

        public int GetCloseOutID(string cat, string CType, int UID, string TNM)
        {
            int intCID = 0;
            string strSQLComm = "";
            if (CType == "C")
            {
                if (cat == "T")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'C' and enddatetime is null and TransactionCnt > 0 ";
                if (cat == "G")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'C' and enddatetime is null ";
            }
            if (CType == "E")
            {
                if (cat == "T")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'E' and enddatetime is null and createdby = @UID and TransactionCnt > 0 ";
                if (cat == "A")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'E' and enddatetime is null and createdby = @UID ";
            }

            if (CType == "T")
            {
                if (cat == "T")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'T' and enddatetime is null and TerminalName = @T and TransactionCnt > 0 ";
                if (cat == "A")
                    strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'T' and enddatetime is null and TerminalName = @T ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (CType == "E")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@UID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@UID"].Value = UID;
            }
            if (CType == "T")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@T"].Value = TNM;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCID = Functions.fnInt32(objSQLReader["clsID"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCID;
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

        public int OpenSuspndItem(int CID)
        {
            int intCID = 0;
            string strSQLComm = "";

            strSQLComm = " select count(distinct InvoiceNo) as RecCount from suspnded where closeoutid = @cid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@cid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@cid"].Value = CID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCID = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCID;
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

        public bool CloseOutTransaction()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                AdjustTenderGridRecords(SaveComm);
                AdjustCurrencyCount(SaveComm);
                UpdateCloseout(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                SaveComm.Dispose();
                return false;
            }
        }

        public bool CloseOutTransaction_Editing()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                AdjustTenderGridRecords_Editing(SaveComm);
                DeleteCurrencyCount(SaveComm);
                AdjustCurrencyCount(SaveComm);
                UpdateCloseout_Editing(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                SaveComm.Dispose();
                return false;
            }
        }

        private void AdjustTenderGridRecords_Editing(SqlCommand objSQlComm)
        {
            try
            {
                if (dtblTenderDataTable == null) return;
                foreach (DataRow dr in dtblTenderDataTable.Rows)
                {
                    string colID = "";
                    string colAMOUNT = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }

                    if (dr["ActualReceipt"].ToString() != "")
                    {
                        colAMOUNT = dr["ActualReceipt"].ToString();
                    }
                    else
                    {
                        colAMOUNT = "0.00";
                    }
                    dblTenderAmount = Functions.fnDouble(colAMOUNT);
                    intTenderID = Functions.fnInt32(colID);
                    bool blchk = IfTenderRecordExists(objSQlComm);
                    if (blchk)
                    {
                        UpdateTenderDetails(objSQlComm);
                    }
                    else
                    {
                        InsertTenderDetails(objSQlComm);
                    }
                }
                dtblTenderDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void AdjustTenderGridRecords(SqlCommand objSQlComm)
        {
            try
            {
                double tottenderamt = 0;
                if (dtblTenderDataTable == null) return;
                foreach (DataRow dr in dtblTenderDataTable.Rows)
                {
                    string colID = "";
                    string colAMOUNT = "";
                    
                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colID = dr["ID"].ToString();
                    }

                    if (dr["ActualReceipt"].ToString() != "")
                    {
                        colAMOUNT = dr["ActualReceipt"].ToString();
                    }
                    else
                    {
                        colAMOUNT = "0.00";
                    }
                    dblTenderAmount = Functions.fnDouble(colAMOUNT);
                    intTenderID = Functions.fnInt32(colID);

                    InsertTenderDetails(objSQlComm);
                }
                dtblTenderDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertTenderDetails(SqlCommand objSQlComm)
        {
            
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into CloseoutTender (CloseoutID,TenderID,TenderAmount) values (@CloseoutID,@TenderID,@TenderAmount) ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TenderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TenderAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@CloseoutID"].Value = intCloseOutID;
                objSQlComm.Parameters["@TenderID"].Value = intTenderID;
                objSQlComm.Parameters["@TenderAmount"].Value = dblTenderAmount;

                sqlDataReader = objSQlComm.ExecuteReader();
                
                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }

        

        public bool UpdateTenderDetails(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " update CloseoutTender set TenderAmount = @TenderAmount where CloseoutID=@CloseoutID and TenderID=@TenderID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TenderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TenderAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@CloseoutID"].Value = intCloseOutID;
                objSQlComm.Parameters["@TenderID"].Value = intTenderID;
                objSQlComm.Parameters["@TenderAmount"].Value = dblTenderAmount;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }

        public bool UpdateCloseout_Editing(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = " update Closeout set Notes = @Notes where ID = @CloseoutID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@CloseoutID"].Value = intCloseOutID;
                objSQlComm.Parameters["@Notes"].Value = strNotes;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }




        private void AdjustCurrencyCount(SqlCommand objSQlComm)
        {
            try
            {

                if (dtblCurrencyCalculateDataTable == null) return;
                foreach (DataRow dr in dtblCurrencyCalculateDataTable.Rows)
                {

                    strPennyCount = dr["Penny"].ToString();
                    strNickelCount = dr["Nickel"].ToString();
                    strDimeCount = dr["Dime"].ToString();
                    strQuarterCount = dr["Quarter"].ToString();
                    strHalveCount = dr["Halve"].ToString();
                    strOneCount = dr["One"].ToString();
                    strFiveCount = dr["Five"].ToString();
                    strTenCount = dr["Ten"].ToString();
                    strTwentyCount = dr["Twenty"].ToString();
                    strFiftyCount = dr["Fifty"].ToString();
                    strHundredCount = dr["Hundred"].ToString();

                    strTwoCount = dr["Two"].ToString();
                    strTwoHundredCount = dr["TwoHundred"].ToString();
                    strFiveHundredCount = dr["FiveHundred"].ToString();
                    strTwoPennyCount = dr["TwoPenny"].ToString();
                    strTwentyPennyCount = dr["TwentyPenny"].ToString();
                    strOneThousandCount = dr["OneThousand"].ToString();
                    InsertCurrencyCount(objSQlComm);
                }
                dtblTenderDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }



        public bool DeleteCurrencyCount(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " delete from CloseoutCurrencyCalculator where RefID = @RefID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@RefID"].Value = intCloseOutID;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }

        public bool InsertCurrencyCount(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into CloseoutCurrencyCalculator (RefID,Penny,Nickel,Dime,Quarter,Halve,One,Five,Ten,Twenty,Fifty,Hundred,Two,TwoHundred,FiveHundred,TwoPenny,TwentyPenny,OneThousand) "
                                  + " values (@RefID,@Penny,@Nickel,@Dime,@Quarter,@Halve,@One,@Five,@Ten,@Twenty,@Fifty,@Hundred,@Two,@TwoHundred,@FiveHundred,@TwoPenny,@TwentyPenny,@OneThousand) ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Penny", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Nickel", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Dime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Quarter", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Halve", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@One", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Five", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Ten", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Twenty", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fifty", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Hundred", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@Two", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TwoHundred", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FiveHundred", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TwoPenny", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TwentyPenny", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@OneThousand", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@RefID"].Value = intCloseOutID;
                objSQlComm.Parameters["@Penny"].Value = strPennyCount;
                objSQlComm.Parameters["@Nickel"].Value = strNickelCount;
                objSQlComm.Parameters["@Dime"].Value = strDimeCount;
                objSQlComm.Parameters["@Quarter"].Value = strQuarterCount;
                objSQlComm.Parameters["@Halve"].Value = strHalveCount;
                objSQlComm.Parameters["@One"].Value = strOneCount;
                objSQlComm.Parameters["@Five"].Value = strFiveCount;
                objSQlComm.Parameters["@Ten"].Value = strTenCount;
                objSQlComm.Parameters["@Twenty"].Value = strTwentyCount;
                objSQlComm.Parameters["@Fifty"].Value = strFiftyCount;
                objSQlComm.Parameters["@Hundred"].Value = strHundredCount;

                objSQlComm.Parameters["@Two"].Value = strTwoCount;
                objSQlComm.Parameters["@TwoHundred"].Value = strTwoHundredCount;
                objSQlComm.Parameters["@FiveHundred"].Value = strFiveHundredCount;
                objSQlComm.Parameters["@TwoPenny"].Value = strTwoPennyCount;
                objSQlComm.Parameters["@TwentyPenny"].Value = strTwentyPennyCount;
                objSQlComm.Parameters["@OneThousand"].Value = strOneThousandCount;

                sqlDataReader = objSQlComm.ExecuteReader();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }



        #region Fetch Close Out ID
        public void UpdateCloseout(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            string strSQLComm = "sp_CloseCloseoutID";
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@CloseoutType"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutType"].Value = strCloseoutType;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CloseoutID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CloseoutID"].Value = intCloseOutID;

                objSQlComm.Parameters.Add(new SqlParameter("@User", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@User"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@User"].Value = intLoginUserID;

                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Notes"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Notes"].Value = strNotes;

                objSQlComm.Parameters.Add(new SqlParameter("@TerminalName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@TerminalName"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TerminalName"].Value = strTerminalName;

                objSQlComm.ExecuteNonQuery();
                objSQlComm.Dispose();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQlComm.Dispose();
            }
            finally
            {
                objSQlComm.Dispose();
            }
        }
        #endregion

        public bool IfTenderRecordExists(SqlCommand objSQlComm)
        {
            int val = 0;

            objSQlComm.Parameters.Clear();
            SqlDataReader objSQLReader = null;
            try
            {
                string strSQLComm = " select count(*) as rcnt from CloseoutTender where CloseoutID=@CloseoutID and TenderID=@TenderID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TenderID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@CloseoutID"].Value = intCloseOutID;
                objSQlComm.Parameters["@TenderID"].Value = intTenderID;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();

                return val > 0;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                objSQLReader.Close();
                objSQLReader.Dispose();
                if (SaveTran != null)
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                objSQlComm.Dispose();

                return false;
            }
        }

        public DataTable GetActiveTerminals()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select ID,TerminalName from closeout where closeouttype = 'T' and enddatetime is null order by  TerminalName";
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["TerminalName"].ToString()});
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

        public DataTable FetchCloseoutCurrencyCount(int CID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select Penny, Nickel, Dime, Quarter, Halve, One, Five, Ten, Twenty, Fifty, Hundred, Two, TwoHundred, FiveHundred, TwoPenny, TwentyPenny, OneThousand from CloseoutCurrencyCalculator where RefID = @CID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CID"].Value = CID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Penny", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Nickel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Dime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Quarter", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Halve", System.Type.GetType("System.String"));
                dtbl.Columns.Add("One", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Five", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Ten", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Twenty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fifty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Hundred", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Two", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TwoHundred", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FiveHundred", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TwoPenny", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TwentyPenny", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OneThousand", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["Penny"].ToString(),
                                                objSQLReader["Nickel"].ToString(),
                                                objSQLReader["Dime"].ToString(),
                                                objSQLReader["Quarter"].ToString(),
                                                objSQLReader["Halve"].ToString(),
                                                objSQLReader["One"].ToString(),
                                                objSQLReader["Five"].ToString(),
                                                objSQLReader["Ten"].ToString(),
                                                objSQLReader["Twenty"].ToString(),
                                                objSQLReader["Fifty"].ToString(),
                                                objSQLReader["Hundred"].ToString(),

                                                objSQLReader["Two"].ToString(),
                                                objSQLReader["TwoHundred"].ToString(),
                                                objSQLReader["FiveHundred"].ToString(),
                                                objSQLReader["TwoPenny"].ToString(),
                                                objSQLReader["TwentyPenny"].ToString(),
                                                objSQLReader["OneThousand"].ToString()
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

        public DataTable FetchCloseoutTenderRecord(int CID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select TenderID, TenderAmount from CloseoutTender where CloseoutID = @CID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CID"].Value = CID;
           
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TenderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["TenderID"].ToString(),
                                                objSQLReader["TenderAmount"].ToString()});
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

        public string FetchCloseoutNotes(int CID)
        {
            string val = "";
            string strSQLComm = "";

            strSQLComm = " select Notes from Closeout where ID = @CID ";

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
                    val = objSQLReader["Notes"].ToString();
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

        #region Fetch Closeout Header Record

        public DataTable ShowCloseoutHeader(int CID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CloseOut where ID=@ID ";
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = CID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["StartDateTime"].ToString(),
												objSQLReader["EndDateTime"].ToString(),
                                                objSQLReader["Notes"].ToString()});
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

        #region Fetch Closeout Record

        public DataTable ShowRecordForEdit(string Callfor)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select c.*, isnull(e.EmployeeID,'ADMIN') as EmpID from CloseOut c "
                       + " left outer join employee e on e.ID=c.CreatedBy where c.enddatetime is not null and c.CloseoutType = @param order by c.ID desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));
            objSQlComm.Parameters["@param"].Value = Callfor;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ConsolidatedID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDatetime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDatetime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransactionCnt", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["CloseoutType"].ToString(),
												objSQLReader["ConsolidatedID"].ToString(),
                                                objSQLReader["StartDatetime"].ToString(),
                                                objSQLReader["EndDatetime"].ToString(),
                                                objSQLReader["TransactionCnt"].ToString(),
                                                objSQLReader["EmpID"].ToString()});
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

        public DataTable ShowRecord( string Callfor, string dateformat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (Callfor == "Reprint")
            {
                strSQLComm = " select c.*, isnull(e.EmployeeID,'ADMIN') as EmpID from CloseOut c "
                           + " left outer join employee e on e.ID=c.CreatedBy where c.enddatetime is not null order by c.ID desc ";
            }

            if (Callfor == "Report")
            {
                strSQLComm = " select c.*, isnull(e.EmployeeID,'ADMIN') as EmpID from CloseOut c "
                           + " left outer join employee e on e.ID=c.CreatedBy order by c.ID desc ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ConsolidatedID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartDatetime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndDatetime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransactionCnt", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["CloseoutType"].ToString(),
												objSQLReader["ConsolidatedID"].ToString(),
                                                Functions.fnDate(objSQLReader["StartDatetime"].ToString()).ToString(dateformat + " hh:mm tt"),
                                                objSQLReader["EndDatetime"].ToString() == "" ? "" : Functions.fnDate(objSQLReader["EndDatetime"].ToString()).ToString(dateformat + " hh:mm tt"),
                                                objSQLReader["TransactionCnt"].ToString(),
                                                objSQLReader["EmpID"].ToString()});
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

        #region Execute Closeout Export Procedure

        public void ExecuteCloseoutExport(DateTime dtFrom, DateTime dtTo, string Terminal)
        {
            DateTime FormatStartDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day, 23, 59, 59);

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_CloseoutExport";
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.CommandTimeout = 30000;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@DateFrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@DateFrom"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@DateFrom"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@DateTo", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@DateTo"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@DateTo"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@Terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Terminal"].Value = Terminal;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

            }
        }

        #endregion

        #region Execute Central Export Procedure

        public void ExecuteCentralExport()
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_CentralExport";

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
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
        }

        #endregion

        #region Fetch Central Export Record

        public DataTable ShowSalesExportHeader()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CentralExportSalesHeader ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TaxedSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonTaxedSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Exist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OtherSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountItemAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountInvoiceAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayDeposits", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayRefund", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayPayment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawaySalesPosted", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaidOuts", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GCsold", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCissued", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCredeemed", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HACharged", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HApayments", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoOfSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSales_PreTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CostOfGoods", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReturnItemNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReturnItemAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["TaxedSales"].ToString(),
												objSQLReader["NonTaxedSales"].ToString(),
												objSQLReader["Tax1Exist"].ToString(),
                                                objSQLReader["Tax1Name"].ToString(),
												objSQLReader["Tax1Amount"].ToString(),
												objSQLReader["Tax2Exist"].ToString(),
                                                objSQLReader["Tax2Name"].ToString(),
												objSQLReader["Tax2Amount"].ToString(),
												objSQLReader["Tax3Exist"].ToString(),
                                                objSQLReader["Tax3Name"].ToString(),
												objSQLReader["Tax3Amount"].ToString(),
												objSQLReader["ServiceSales"].ToString(),
                                                objSQLReader["ProductSales"].ToString(),
												objSQLReader["OtherSales"].ToString(),
												objSQLReader["DiscountItemNo"].ToString(),
                                                objSQLReader["DiscountItemAmount"].ToString(),
												objSQLReader["DiscountInvoiceNo"].ToString(),
												objSQLReader["DiscountInvoiceAmount"].ToString(),
                                                objSQLReader["LayawayDeposits"].ToString(),
												objSQLReader["LayawayRefund"].ToString(),
												objSQLReader["LayawayPayment"].ToString(),
                                                objSQLReader["LayawaySalesPosted"].ToString(),
												objSQLReader["PaidOuts"].ToString(),
												objSQLReader["GCsold"].ToString(),
                                                objSQLReader["SCissued"].ToString(),
												objSQLReader["SCredeemed"].ToString(),
												objSQLReader["HACharged"].ToString(),
                                                objSQLReader["HApayments"].ToString(),
                                                objSQLReader["NoOfSales"].ToString(),
                                                objSQLReader["TotalSales_PreTax"].ToString(),
                                                objSQLReader["CostOfGoods"].ToString(),
                                                objSQLReader["ReturnItemNo"].ToString(),
                                                objSQLReader["ReturnItemAmount"].ToString()});
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

        public DataTable ShowSalesExportTender()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CentralExportSalesTender ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TenderName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderCount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["TenderName"].ToString(),
												objSQLReader["TenderAmount"].ToString(),
												objSQLReader["TenderCount"].ToString()});
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

        public DataTable ShowInventoryExport()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CentralExportInventory ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["SKU"].ToString(),
												objSQLReader["Description"].ToString(),
												objSQLReader["ProductType"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString(),
												objSQLReader["QtyOnLayaway"].ToString(),
												objSQLReader["ReorderQty"].ToString(),
                                                objSQLReader["NormalQty"].ToString()});
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

        public DataTable ShowEmployeeExport()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CentralExportEmp ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("EmpID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DayStart", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DayEnd", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftStartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftEndDate", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["EmpID"].ToString(),
												objSQLReader["EmployeeID"].ToString(),
												objSQLReader["LastName"].ToString(),
                                                objSQLReader["FirstName"].ToString(),
												objSQLReader["ShiftID"].ToString(),
												objSQLReader["ShiftName"].ToString(),
                                                objSQLReader["StartTime"].ToString(),
                                                objSQLReader["EndTime"].ToString(),
												objSQLReader["ShiftDuration"].ToString(),
												objSQLReader["DayStart"].ToString(),
                                                objSQLReader["DayEnd"].ToString(),
												objSQLReader["ShiftStartDate"].ToString(),
                                                objSQLReader["ShiftEndDate"].ToString()});
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

        #region Insert Export/Import Log

        public string InsertExpImpLog()
        {
            string strSQLComm = " insert into CentralExportImportLog( FileType, FileName,FilePath,FileCreatedDate, LastUsedBy, LastUsedOn) "
                                + " values ( @FileType, @FileName,@FilePath,@FileCreatedDate, @LastUsedBy, @LastUsedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@FileType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@FileName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FilePath", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FileCreatedDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastUsedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastUsedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@FileType"].Value = strFileType;
                objSQlComm.Parameters["@FileName"].Value = strFileName;
                objSQlComm.Parameters["@FilePath"].Value = strFilePath;
                objSQlComm.Parameters["@FileCreatedDate"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastUsedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastUsedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                
                if (objsqlReader.Read())
                {
                }

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

        #region Update Export Tag

        public string UpdateSalesExportTag()
        {
            string strSQLComm = " update trans set expflag='Y' where expflag = 'N' ";       

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

        public string UpdateEmpAttnExportTag()
        {
            string strSQLComm = " update attendanceinfo set expflag='Y' where expflag = 'N' ";

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

        #endregion

        public DataTable ShowExportImportLog(string datefilter)
        {
            DataTable dtbl = new DataTable();
            DateTime FromDt = DateTime.Now;
            string sqlfilter = "";
            if (datefilter == "Last 7 days")
            {
                FromDt = FromDt.AddDays(-7);
            }
            if (datefilter == "Last 15 days")
            {
                FromDt = FromDt.AddDays(-15);
            }
            if (datefilter == "Last 1 month")
            {
                FromDt = FromDt.AddMonths(-1);
            }
            if (datefilter != "All")
            {
                FromDt = new DateTime(FromDt.Year, FromDt.Month, FromDt.Day, 0, 0, 1);
                sqlfilter = " and FileCreatedDate >= @sdt ";
            }
            string strSQLComm = " select ID, FileName, FilePath, FileCreatedDate from CentralExportImportLog where 1 = 1  "
                                + sqlfilter + " order by FileCreatedDate desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (datefilter != "All")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@sdt", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@sdt"].Value = FromDt;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilePath", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FileCreatedDate", System.Type.GetType("System.String"));



                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["FileName"].ToString(),
												objSQLReader["FilePath"].ToString(),
                                                Functions.fnDate(objSQLReader["FileCreatedDate"].ToString()).ToShortDateString()});
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

        public string UpdateExportLog()
        {
            string strSQLComm = " update CentralExportImportLog set LastUsedBy = @LastUsedBy, LastUsedOn = @LastUsedOn where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@LastUsedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastUsedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LastUsedBy"].Value = LoginUserID;
                objSQlComm.Parameters["@LastUsedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ID"].Value = intFileID;
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


        #region Dashboard

        public DataTable ShowAutoProcessRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from AutoProcessInfo where ID in ( select Max(a.ID) from AutoProcessInfo a) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StatusMessage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BackupFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DataPurgingFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShrinkFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloseoutFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpClockoutFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CloudBackupFlag", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["StartTime"].ToString(),
												objSQLReader["EndTime"].ToString(),
                                                objSQLReader["StatusMessage"].ToString(),
                                                objSQLReader["BackupFlag"].ToString(),
                                                objSQLReader["DataPurgingFlag"].ToString(),
                                                objSQLReader["ShrinkFlag"].ToString(),
                                                objSQLReader["CloseoutFlag"].ToString(),
                                                objSQLReader["EmpClockoutFlag"].ToString(),
                                                 objSQLReader["CloudBackupFlag"].ToString()});
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

        public int GetLastConsolidatedCloseoutID()
        {
            int intCID = 0;
            string strSQLComm = "";
            strSQLComm = " select isnull(ID,0) as clsID from Closeout where closeouttype = 'C' and enddatetime is not null and  "
                       + " ID in (select Max(c.ID) from Closeout c where c.closeouttype = 'C'and c.enddatetime is not null )";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCID = Functions.fnInt32(objSQLReader["clsID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCID;
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

        public void ExecuteDashBoard(string Terminal, string pTaxInclude)
        {

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_dashboard";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;
                objSQlComm.CommandTimeout = 500;

                objSQlComm.Parameters.Add(new SqlParameter("@Terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Terminal"].Value = Terminal;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxInclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@TaxInclusive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TaxInclusive"].Value = pTaxInclude;

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

        public DataTable ShowDashBoardHeaderRecord(string Terminal)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from CloseOutReportMain where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Voids", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discounts", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HoursWorked", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                Functions.fnInt32(objSQLReader["NoOfSales"].ToString()) > 0 ? Functions.fnDouble(objSQLReader["TaxedSales"].ToString()).ToString("f") + " (" +objSQLReader["NoOfSales"].ToString() + ")" : "",
                                                Functions.fnInt32(objSQLReader["DiscountItemNo"].ToString()) > 0 ? Functions.fnDouble(objSQLReader["DiscountItemAmount"].ToString()).ToString("f") + " (" +objSQLReader["DiscountItemNo"].ToString() + ")" : "",
                                                Functions.fnInt32(objSQLReader["DiscountInvoiceNo"].ToString()) > 0 ? Functions.fnDouble(objSQLReader["DiscountInvoiceAmount"].ToString()).ToString("f") + " (" +objSQLReader["DiscountInvoiceNo"].ToString() + ")" : "",
                                                objSQLReader["Notes"].ToString()});
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

        public DataTable ShowDashBoardSalesByHourRecord(string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutSalesHour where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Timeinterval", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalesAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["Timeinterval"].ToString(),
												Functions.fnDouble(objSQLReader["SalesAmount"].ToString()) == 0 ? "-  " :
                    Functions.fnDouble(objSQLReader["SalesAmount"].ToString()).ToString("f") + "  ",
                                                objSQLReader["TerminalName"].ToString()
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

        public DataTable ShowDashBoardEmpClockInRecord(string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from CloseOutSalesDept where reportterminalname = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar ));
            objSQlComm.Parameters["@T"].Value = Terminal;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToShortDateString(),
                                                Functions.fnDate(objSQLReader["StartDateTime"].ToString()).ToShortTimeString(),
                                                objSQLReader["Notes"].ToString()});
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


        public string AddCashFloat(int intCloseoutID, string strTerminal, double dblCashfloat, string strNotes, int intUser)
        {
            string strSQLComm = " insert into CashFloat( CloseoutID, TerminalName, CashFloat, Notes, CreatedBy, CreatedOn ) "
                              + " values ( @CloseoutID, @TerminalName, @CashFloat, @Notes,@CreatedBy, @CreatedOn) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TerminalName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CashFloat", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@CloseoutID"].Value = intCloseoutID;
                objSQlComm.Parameters["@TerminalName"].Value = strTerminal;
                objSQlComm.Parameters["@CashFloat"].Value = dblCashfloat;
                objSQlComm.Parameters["@Notes"].Value = strNotes;
                objSQlComm.Parameters["@CreatedBy"].Value = intUser;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;

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

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strErrMsg;
            }
        }


        public bool IsTerminalConsolidatedCloseout(int closeoutId)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from Closeout where ConsolidatedID = @ID and CloseoutType = 'T' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = closeoutId;

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

        public double FetchCashFloatAmount(string closeouttype, int closeoutId)
        {
            double val = 0;
            string strSQLComm = "";

            if (closeouttype == "T")
            {
                strSQLComm = " select isnull(cashfloat,0) as amt from CashFloat where CloseoutID = @ID ";
            }

            if (closeouttype == "C")
            {
                strSQLComm = " select isnull(sum(cashfloat),0) as amt from CashFloat where CloseoutID in (select ID from closeout where CloseoutType = 'T' and ConsolidatedID = @ID) ";
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = closeoutId;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnDouble(objsqlReader["amt"]);
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
                return val;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }


        public int OpenSuspndItem(string closeouttype, int closeoutId)
        {
            int intCID = 0;
            string strSQLComm = "";

            if (closeouttype == "C")
            {
                strSQLComm = " select count(distinct InvoiceNo) as RecCount from suspnded where closeoutid = @cid ";
            }
            else
            {
                strSQLComm = " select count(distinct InvoiceNo) as RecCount from suspnded where  CloseoutID in (select ID from closeout where CloseoutType = 'T' and ConsolidatedID = @cid) ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@cid", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@cid"].Value = closeoutId;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intCID = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCID;
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

        public DataTable GetCloseoutInfo(int pID)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select c.*, isnull(e.FirstName,'ADMIN') as EName from CloseOut c left outer join employee e on e.ID=c.CreatedBy "
                       + " where c.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;


            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TRANCNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TERMINAL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TIME", System.Type.GetType("System.String"));

                string sd = "";
                string st = "";
                string ed = "";
                string et = "";
                while (objSQLReader.Read())
                {
                    sd = "";
                    st = "";
                    ed = "";
                    et = "";
                    sd = Functions.fnDate(objSQLReader["StartDatetime"].ToString()).ToShortDateString();
                    st = Functions.fnDate(objSQLReader["StartDatetime"].ToString()).ToShortTimeString();


                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["TransactionCnt"].ToString(),
                                                objSQLReader["TerminalName"].ToString(),
                                                objSQLReader["EName"].ToString(),
                                                sd,st});
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


        public DataTable FetchTerminals()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select distinct terminalname from closeout where CloseoutType = 'T' ";
         
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["terminalname"].ToString() });
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

    }

}
