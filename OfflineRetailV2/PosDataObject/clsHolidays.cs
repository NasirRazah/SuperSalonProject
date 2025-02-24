/*
 purpose : Data for Holiday
*/


using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Holidays
    {
        private SqlConnection sqlConn;
        private int intID;
        private int intLoginID;
        private string strHolidayDesc;
        private DateTime dtHolidayDate;

        public Holidays()
        {
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int LoginID
        {
            get { return intLoginID; }
            set { intLoginID = value; }
        }

        public string HolidayDesc
        {
            get { return strHolidayDesc; }
            set { strHolidayDesc = value; }
        }

        public DateTime HolidayDate
        {
            get { return dtHolidayDate; }
            set { dtHolidayDate = value; }
        }


        #region Insert Data
        public string InsertData()
        {
            string strSQLComm = " insert into HolidayMaster( HolidayDesc, HolidayDate, CreatedBy, LastChangedBy, CreatedOn, LastChangedOn) "
                              + " values( @HolidayDesc,@HolidayDate, @CreatedBy, @LastChangedBy, @CreatedOn, @LastChangedOn ) "
                              +" select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@HolidayDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HolidayDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HolidayDesc"].Value = strHolidayDesc;
                objSQlComm.Parameters["@HolidayDate"].Value = dtHolidayDate;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        this.ID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch { objsqlReader.Close();  }
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

        #region Update Data
        public string UpdateData()
        {
            string strSQLComm = " update HolidayMaster set HolidayDesc=@HolidayDesc, HolidayDate=@HolidayDate, "
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn   "
                                + " where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@HolidayDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HolidayDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@HolidayDesc"].Value = strHolidayDesc;
                objSQlComm.Parameters["@HolidayDate"].Value = dtHolidayDate;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ID"].Value = intID;

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

        #region Delete Data
        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = "DELETE from HolidayMaster where ID = @ID";

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

        #region Fetchdata
        public DataTable FetchData(int intYear)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " Select ID, HolidayDesc, HolidayDate from HolidayMaster where  "
                         + " year(HolidayDate) = @Year order by HolidayDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@Year", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@Year"].Value = intYear;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HolidayDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HolidayDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["HolidayDesc"].ToString(),
                                                Functions.fnDate(objSQLReader["HolidayDate"].ToString()).ToString("MMMM dd, yyyy")});
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
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public DataTable FetchRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " Select * from HolidayMaster where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HolidayDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HolidayDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["HolidayDesc"].ToString(),
                                                objSQLReader["HolidayDate"].ToString()});

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
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        #endregion

        public int DuplicateHoliday()
        {
            int intresult = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from HolidayMaster where HolidayDate = @HolidayDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@HolidayDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@HolidayDate"].Value = dtHolidayDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["RECCOUNT"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                return intresult;
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

        public int MaxID()
        {
            int intresult = 0;

            string strSQLComm = "select MAX(ID) AS MAXID from HolidayMaster ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["MAXID"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intresult;
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

    }
}
