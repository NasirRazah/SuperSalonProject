/*
 purpose : Data for Customer Class
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Class
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strGeneralCode;
        private string strGeneralDesc;

        #endregion

        #region definining public variables

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

        #endregion

        #region Insert Data
        public string InsertData()
        {
            string strSQLComm = " insert into ClassMaster( ClassID, Description,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                              + " values ( @ClassID,@Description,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ClassID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
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
        public string UpdateData()
        {
            string strSQLComm = " update ClassMaster set ClassID=@ClassID,Description=@Description,"
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ClassID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@ClassID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
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

        #region Fetchdata

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,ClassID,Description from ClassMaster ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClassID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["ClassID"].ToString(),
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

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from ClassMaster where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClassID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
												objSQLReader["ClassID"].ToString(),
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
        #endregion

        #region Duplicate Checking

        public int DuplicateCount(string clsID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from ClassMaster where ClassID = @ClassID ";

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
        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " Delete from ClassMaster Where ID = @ID";

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

        #region Get Class Desc

        public string GetClassDesc(int ClassID)
        {
            string strresult = "";
            string strSQLComm = " select Description from ClassMaster where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = ClassID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = objSQLReader["Description"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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
    }
}
