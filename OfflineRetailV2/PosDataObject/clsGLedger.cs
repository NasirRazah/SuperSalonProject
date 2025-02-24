using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class GLedger
    {
        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strGLCode;
        private string strGLDescription;

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

        public string GLCode
        {
            get { return strGLCode; }
            set { strGLCode = value; }
        }

        public string GLDescription
        {
            get { return strGLDescription; }
            set { strGLDescription = value; }
        }

        public string InsertGL()
        {
            string strSQLComm = " insert into GLedger( GLCode, GLDescription, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                              + " values ( @GLCode, @GLDescription, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@GLCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GLDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@GLCode"].Value = strGLCode;
                objSQlComm.Parameters["@GLDescription"].Value = strGLDescription;
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

        public string UpdateGL()
        {
            string strSQLComm = " update GLedger set GLCode=@GLCode, GLDescription=@GLDescription, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn "
                              + " where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GLCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GLDescription", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@GLCode"].Value = strGLCode;
                objSQlComm.Parameters["@GLDescription"].Value = strGLDescription;
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

        public DataTable FetchGL()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,GLCode,GLDescription from GLedger order by GLCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLDescription", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["GLCode"].ToString(),
												   objSQLReader["GLDescription"].ToString()});
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

        public DataTable ShowGLRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Gledger where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLDescription", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
												objSQLReader["GLCode"].ToString(),
												objSQLReader["GLDescription"].ToString()
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

        public string DeleteGL(int DeleteID)
        {
            string strSQLComm = " Delete from GLedger where ID = @ID";

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

        public int DuplicateCount(string pCode)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from GLedger where GLCode = @GLCode ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@GLCode", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@GLCode"].Value = pCode;

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

        public DataTable LookupGL(string pType)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select ID,GLCode,GLDescription from GLedger order by GLCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GLDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Lookup", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["GLCode"].ToString(),
												   objSQLReader["GLDescription"].ToString(),
                                                   objSQLReader["GLCode"].ToString() + "  |  " + objSQLReader["GLDescription"].ToString() });
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


        public void ExecuteGLAccounting(DateTime dtF, DateTime dtT, DateTime dtF_t, DateTime dtT_t, string Terminal)
        {

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_GL_Accounting";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;
                objSQlComm.CommandTimeout = 500;

                objSQlComm.Parameters.Add(new SqlParameter("@datefrom", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@datefrom"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@datefrom"].Value = dtF;

                objSQlComm.Parameters.Add(new SqlParameter("@dateto", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@dateto"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@dateto"].Value = dtT;

                objSQlComm.Parameters.Add(new SqlParameter("@datefrom_t", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@datefrom_t"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@datefrom_t"].Value = dtF_t;

                objSQlComm.Parameters.Add(new SqlParameter("@dateto_t", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@dateto_t"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@dateto_t"].Value = dtT_t;

                objSQlComm.Parameters.Add(new SqlParameter("@terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@terminal"].Value = Terminal;

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

        public DataTable FetchGLExportData(string Terminal)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select GLCode,GLDescription,ExportAmount from GLAccounting where TerminalName = @T ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@T"].Value = Terminal;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Account Code", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Account Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
               

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["GLCode"].ToString(),
												   objSQLReader["GLDescription"].ToString(),
                                                   objSQLReader["ExportAmount"].ToString() });
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
