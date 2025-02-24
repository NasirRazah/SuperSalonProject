/*
 purpose : Data for Access Permission 
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Security
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strGroupName;

        private int intGroupID;

        private string strMode;
        private string strErrorMsg;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        private DataTable dblInitialDataTable;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string detCode;
        private string detCheck;


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

        public int GroupID
        {
            get { return intGroupID; }
            set { intGroupID = value; }
        }

        public string GroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
        }
        public SqlTransaction SQLTran
        {
            get { return objSQLTran; }
            set { objSQLTran = value; }
        }

        public DataTable SplitDataTable
        {
            get { return dblSplitDataTable; }
            set { dblSplitDataTable = value; }
        }

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
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

        public bool InsertPermission()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                
                DeletePermissionDetails(SaveComm, intGroupID);
                AdjustSplitGridRecords(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }


        private void AdjustSplitGridRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSecurityCode = "";
                    string colSecurityCheck = "";

                    if (dr["SecurityCode"].ToString() == "") continue;

                    if (dr["SecurityCode"].ToString() != "")
                    {
                        colSecurityCode = dr["SecurityCode"].ToString();
                    }

                    if ( Convert.ToBoolean(dr["SecurityCheck"].ToString())== true)
                    {
                        colSecurityCheck = "Y";
                    }
                    else
                    {
                        colSecurityCheck = "N";
                    }
                    detCode = colSecurityCode;
                    detCheck = colSecurityCheck;

                    InsertPermissionDetails(objSQlComm);

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeletePermissionDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from GroupPermission Where GroupID = @GroupID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@GroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@GroupID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertPermissionDetails(SqlCommand objSQlComm)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into GroupPermission(GroupID, SecurityCode, PermissionFlag, "
                                  + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @GroupID, @SecurityCode, @PermissionFlag, "
                                  + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@GroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SecurityCode", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PermissionFlag", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@GroupID"].Value = intGroupID;
                objSQlComm.Parameters["@SecurityCode"].Value = detCode;
                objSQlComm.Parameters["@PermissionFlag"].Value = detCheck;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);
                
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


        #region Insert Data

        public string InsertData()
        {
            string strSQLComm = " insert into SecurityGroup( GroupName, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                                + " values ( @GroupName,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@GroupName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@GroupName"].Value = strGroupName;
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
            string strSQLComm = " update SecurityGroup set GroupName=@GroupName,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GroupName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@GroupName"].Value = strGroupName;
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

        #region Fetch Data

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,GroupName from SecurityGroup order by GroupName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["GroupName"].ToString()});
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
            string strSQLComm = "select * from SecurityGroup where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["GroupName"].ToString()});
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

        public int DuplicateCount(string profilename)
        {
            int intCount = 0;

            string strSQLComm = " select COUNT(*) AS RECCOUNT from SecurityGroup where GroupName=@GroupName ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@GroupName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@GroupName"].Value = profilename;

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
        
        #region Fetch Permission Data ( New )

        public DataTable FetchNewPermissionData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select SecurityCode,SecurityGroup, SecurityDesc from SecurityPermission order by GroupSlNo, SlNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SecurityGroup", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityCheck", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("SecurityVisible", System.Type.GetType("System.Boolean"));

                

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["SecurityGroup"].ToString() == "Scales") || (objSQLReader["SecurityGroup"].ToString() == "Labels and Signs") ||
                    (objSQLReader["SecurityGroup"].ToString() == "Tablet")) continue;

                    if ((objSQLReader["SecurityCode"].ToString() == "1q") || (objSQLReader["SecurityCode"].ToString() == "20f") ||
                    (objSQLReader["SecurityCode"].ToString() == "20g") || (objSQLReader["SecurityCode"].ToString() == "21a") || (objSQLReader["SecurityCode"].ToString() == "21b")) continue;

                    if (objSQLReader["SecurityCode"].ToString() == "20e")
                    {
                        dtbl.Rows.Add(new object[] {   objSQLReader["SecurityGroup"].ToString(),
                                                   objSQLReader["SecurityCode"].ToString(),
                                                   "XEPOS Host",
                                                   false,true});
                    }
                    else if (objSQLReader["SecurityCode"].ToString() == "21c")
                    {
                        dtbl.Rows.Add(new object[] {   objSQLReader["SecurityGroup"].ToString(),
                                                        objSQLReader["SecurityCode"].ToString(),
                                                   "Allow Closeout",
                                                   false,true});
                    }
                    else
                    {
                        dtbl.Rows.Add(new object[] {   objSQLReader["SecurityGroup"].ToString(),
                                                   objSQLReader["SecurityCode"].ToString(),
                                                   objSQLReader["SecurityDesc"].ToString(),
                                                   false,true});
                    }
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

        #region Fetch Assigned Permission Data (  )

        public DataTable FetchAssignedPermissionData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select SecurityCode,PermissionFlag from GroupPermission where GroupID = @GRID order by SecurityCode ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@GRID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@GRID"].Value = intGroupID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SecurityCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityCheck", System.Type.GetType("System.Boolean"));

                while (objSQLReader.Read())
                {
                    bool chk = false;
                    if (objSQLReader["PermissionFlag"].ToString() == "Y")
                    {
                        chk = true;
                    }
                    else
                    {
                        chk = false;
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["SecurityCode"].ToString(),
                                                   chk});
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

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " Delete from SecurityGroup Where ID = @ID";

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

        #region Delete Group Permission

        public string DeleteGroupPermission(int DeleteID)
        {
            string strSQLComm = " Delete from GroupPermission Where GroupID = @ID";

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

        public int IsExistProfile(int GroupID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Employee where ProfileID = @PID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = GroupID;

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

        public int IsExistSecuriryData()
        {
            int intCount = 0;

            string strSQLComm = " select COUNT(*) AS RECCOUNT from SecurityPermission  ";

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

        #region Insert Security Permission Data

        public int InsertSecurityPermissionData()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_addsecuritymodule";

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

        #endregion

        #region Load Default Security

        public int InsertDefaultSecurity()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_LoadDefaultSecurity";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@intUser", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@intUser"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@intUser"].Value = intLoginUserID;

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

        #endregion

        #region Set owner Security

        public int SetOwnerSecurity()
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_SetSuperUserPermission";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@intUser", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@intUser"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@intUser"].Value = intLoginUserID;

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

        #endregion

        #region POS

        public int IsExistsPOSAccess(int usrID,string scode)
        {
            int intreturn = 0;

            string strSQLComm = " select count(gp.ID) as RecCount from GroupPermission gp  "
                              + " Left outer join Employee emp on emp.ProfileID = gp.GroupID "
                              + " where emp.ID = @LID and gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LID"].Value = usrID;
                objSQlComm.Parameters.Add(new SqlParameter("@SCODE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SCODE"].Value = scode;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                
                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    intreturn = Functions.fnInt32(objsqlReader["RecCount"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intreturn;
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

                return intreturn;
            }
        }

        public int IsExistsPOSSuperUserID(string usrID, string passwrd, string scode, int signinoption)
        {
            int intreturn = 0;
            string sqlFilter = "";

            string strSQLComm = "";

            
            if (signinoption == 0)
            {
                strSQLComm = " select count(emp.ID) as RecCount from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.EmployeeID = @LID and emp.Password = @PWD ";
            }
            if (signinoption == 1)
            {
                strSQLComm = " select count(emp.ID) as RecCount from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.EmployeeID = @LID ";
            }
            if (signinoption == 2)
            {
                strSQLComm = " select count(emp.ID) as RecCount from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.Password = @PWD ";
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                
                if ((signinoption == 0) || (signinoption == 1))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@LID"].Value = usrID;
                }
                if ((signinoption == 0) || (signinoption == 2))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@PWD", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@PWD"].Value = passwrd;
                }
                objSQlComm.Parameters.Add(new SqlParameter("@SCODE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SCODE"].Value = scode;
                                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intreturn = Functions.fnInt32(objsqlReader["RecCount"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intreturn;
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
                return intreturn;
            }
        }

        public int FetchPOSSuperUserID(string usrID, string passwrd, string scode, int signinoption)
        {
            int intreturn = 0;
            string sqlFilter = "";

            string strSQLComm = " ";
                        
            if (signinoption == 0)
            {
                strSQLComm = " select isnull(emp.ID,0) as ID from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where  gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.EmployeeID = @LID and emp.Password = @PWD ";
            }

            if (signinoption == 1)
            {
                strSQLComm = " select isnull(emp.ID,0) as ID from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where  gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.EmployeeID = @LID ";
            }

            if (signinoption == 2)
            {
                strSQLComm = " select isnull(emp.ID,0) as ID from Employee emp "
                                  + " Left outer join GroupPermission gp on emp.ProfileID = gp.GroupID "
                                  + " where  gp.PermissionFlag = 'Y' and gp.SecurityCode = @SCODE "
                                  + " and emp.Password = @PWD ";
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                
                if ((signinoption == 0) || (signinoption == 1))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@LID"].Value = usrID;
                }
               
                if ((signinoption == 0) || (signinoption == 2))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@PWD", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@PWD"].Value = passwrd;
                }

                
                objSQlComm.Parameters.Add(new SqlParameter("@SCODE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@SCODE"].Value = scode;
                

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intreturn = Functions.fnInt32(objsqlReader["ID"].ToString());
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intreturn;
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

                return intreturn;
            }
        }

        public bool GetAccessCashdrawerForSuperuser(int usrID)
        {
            string sv = "";
            string strSQLComm = " select isnull(gp.PermissionFlag, 'N') as pflag from GroupPermission gp  "
                              + " Left outer join Employee emp on emp.ProfileID = gp.GroupID "
                              + " where emp.ID = @LID and gp.SecurityCode = '31xx1' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@LID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LID"].Value = usrID;
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    sv = objsqlReader["pflag"].ToString();
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                if (sv == "Y") return true; else return false;
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
    }
}
