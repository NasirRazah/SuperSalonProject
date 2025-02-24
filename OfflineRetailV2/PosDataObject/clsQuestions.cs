/*
 NOT USED
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Questions
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private int intRefID;
        private int intHeaderID;
        private int intLevelID;

        private string strQType;
        private string strQDesc;

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

        public string QType
        {
            get { return strQType; }
            set { strQType = value; }
        }

        public string QDesc
        {
            get { return strQDesc; }
            set { strQDesc = value; }
        }

        public int RefID
        {
            get { return intRefID; }
            set { intRefID = value; }
        }

        public int HeaderID
        {
            get { return intHeaderID; }
            set { intHeaderID = value; }
        }

        public int LevelID
        {
            get { return intLevelID; }
            set { intLevelID = value; }
        }
        
        #endregion

        #region Insert Data

        public string InsertData()
        {
            string strSQLComm = " insert into Questions( RefID, HeaderID,LevelID,QType,QDesc,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                              + " values ( @RefID,@HeaderID,@LevelID,@QType,@QDesc,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@HeaderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LevelID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@QDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@HeaderID"].Value = intHeaderID;
                objSQlComm.Parameters["@LevelID"].Value = intLevelID;
                objSQlComm.Parameters["@QType"].Value = strQType;
                objSQlComm.Parameters["@QDesc"].Value = strQDesc;

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
            string strSQLComm = " update Questions set RefID=@RefID,HeaderID=@HeaderID,LevelID=@LevelID,QType=@QType,QDesc=@QDesc,"
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
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@HeaderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LevelID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@QDesc", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@HeaderID"].Value = intHeaderID;
                objSQlComm.Parameters["@LevelID"].Value = intLevelID;
                objSQlComm.Parameters["@QType"].Value = strQType;
                objSQlComm.Parameters["@QDesc"].Value = strQDesc;
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

        #region Fetch Root Data

        public DataTable FetchRootData(int intRef, string strType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,RefID,HeaderID,LevelID,QDesc from Questions where "
                              + " QType=@QType and RefID=@RefID and HeaderID = 0 and LevelID = 0 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@QType"].Value = strType;
            objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefID"].Value = intRef;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HeaderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LevelID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Question", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["RefID"].ToString(),
												   objSQLReader["HeaderID"].ToString(),
                                                   objSQLReader["LevelID"].ToString(),
                                                   objSQLReader["QDesc"].ToString()});
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

        #region Fetch Next Level Data

        public DataTable FetchNextLevelData(int intRef, string strType, int HID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,RefID,HeaderID,LevelID,QDesc from Questions where "
                              + " QType=@QType and RefID=@RefID and HeaderID = @HID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@QType"].Value = strType;
            objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefID"].Value = intRef;
            objSQlComm.Parameters.Add(new SqlParameter("@HID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@HID"].Value = HID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HeaderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LevelID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Question", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["RefID"].ToString(),
												   objSQLReader["HeaderID"].ToString(),
                                                   objSQLReader["LevelID"].ToString(),
                                                   objSQLReader["QDesc"].ToString()});
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

        #region If Exists Next Level Data

        public int IsExistsNextLevelData(int intRef, string strType, int HID)
        {
            int intReturn = 0;

            string strSQLComm = " select count(*) as RecCount from Questions where "
                              + " QType=@QType and RefID=@RefID and HeaderID = @HID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@QType"].Value = strType;
            objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefID"].Value = intRef;
            objSQlComm.Parameters.Add(new SqlParameter("@HID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@HID"].Value = HID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intReturn = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                return intReturn;
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

        #region Show Record based on ID

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from Questions where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HeaderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LevelID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Question", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["RefID"].ToString(),
												objSQLReader["HeaderID"].ToString(),
                                                objSQLReader["LevelID"].ToString(),
                                                objSQLReader["QDesc"].ToString()});
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

        public string DeleteRecord(DataTable dtbldel, string qtype)
        {
            string sqlFilter = "";
            string delid = "";
            foreach (DataRow dr in dtbldel.Rows)
            {
                if (delid == "") delid = dr["ID"].ToString();
                else delid = delid + "," + dr["ID"].ToString();
            }
            sqlFilter = " and id in ( " + delid + ")";

            string strSQLComm = " Delete from Questions Where QType = @QType " + sqlFilter; 

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@QType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@QType"].Value = qtype;
                
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
    }
}
