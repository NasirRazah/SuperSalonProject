/*
 purpose : Data for Local Setup ( Terminal wise Data )
*/


using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class LocalSetup
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private int intID;
        private string strHostName;
        private string strParamName;
        private string strParamValue;
        private int intLoginUserID;

        #endregion

        #region definining public variables

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

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }

        public string HostName
        {
            get { return strHostName; }
            set { strHostName = value; }
        }

        public string ParamName
        {
            get { return strParamName; }
            set { strParamName = value; }
        }

        public string ParamValue
        {
            get { return strParamValue; }
            set { strParamValue = value; }
        }

        #endregion

        #region Insert Data

        public string InsertParamData()
        {
            string strSQLComm = " insert into LocalSetup( HostName,ParamName,ParamValue,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                              + " values ( @HostName,@ParamName,@ParamValue,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

        public string UpdateParamData()
        {
            string strSQLComm = " update LocalSetup set ParamValue=@ParamValue,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn "
                              + " where HostName=@HostName and ParamName=@ParamName ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@HostName"].Value = strHostName;
                objSQlComm.Parameters["@ParamName"].Value = strParamName;
                objSQlComm.Parameters["@ParamValue"].Value = strParamValue;
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

        #region Checking Local Setup Entry

        public int IsExistLocalSetupData(string pHost, string pIP, string pParam)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from LocalSetup where HostName=@HostName " 
                              + " and HostIPAddress=@HostIPAddress and ParamName=@ParamName ";
                              

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HostIPAddress", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@HostName"].Value = pHost;
                objSQlComm.Parameters["@HostIPAddress"].Value = pIP;
                objSQlComm.Parameters["@ParamName"].Value = pParam;

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
    }
}
