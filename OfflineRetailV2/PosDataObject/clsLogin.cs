/*
 purpose : Data for Login
*/
using System;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Login
    {
        private SqlConnection sqlConn;

        public Login()
        {
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public int GetLoginDetails( int SigninOption, string logUser, string logPassword,bool pswdrqd,string apswd,
                                    ref int rfUserID, ref string rfUserCode, ref string rfUserName,ref bool blLocked,
                                    ref string MsgExpiry, ref bool blExpired )
        {
            int intresult = 0;
            string strSQLComm = "";
            
            string sqlsqladd = "";
            if (pswdrqd) sqlsqladd = " and adminpassword = @pswd";

            if (SigninOption == 0)
            {
                strSQLComm = " select ID,EmployeeID,Lastname,FirstName,Locked,isnull(AdminPasswordModifiedOn,'') as LastModified "
                           + " from Employee where EmployeeID=@EmployeeID and Password=@Password " + sqlsqladd;
            }

            if (SigninOption == 1)
            {
                strSQLComm = " select ID,EmployeeID,Lastname,FirstName from Employee where EmployeeID=@EmployeeID " + sqlsqladd;
            }

            if (SigninOption == 2)
            {
                strSQLComm = " select ID,EmployeeID,Lastname,FirstName from Employee where Password=@Password " + sqlsqladd;
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if ((SigninOption == 0) || (SigninOption == 1))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@EmployeeID"].Value = logUser;
                }
                if ((SigninOption == 0) || (SigninOption == 2))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@Password"].Value = logPassword;
                }

                if (pswdrqd)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@pswd", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@pswd"].Value = apswd;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    rfUserID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    rfUserCode = objSQLReader["EmployeeID"].ToString();
                    rfUserName = objSQLReader["FirstName"].ToString() + " " + objSQLReader["Lastname"].ToString();
                    intresult = 1;
                    if (pswdrqd)
                    {
                        if (objSQLReader["Locked"].ToString() == "Y")
                        {
                            blLocked = true;
                            intresult = -1;
                        }
                        if (DateTime.Today.Date > Functions.fnDate(objSQLReader["LastModified"].ToString()).AddDays(90).Date)
                        {
                            blExpired = true;
                            intresult = -2;
                            MsgExpiry = "Password has expired";
                        }
                        else
                        {
                            TimeSpan ts = Functions.fnDate(objSQLReader["LastModified"].ToString()).Date.AddDays(90).Subtract(DateTime.Today.Date);
                            if (ts.Days == 0) MsgExpiry = "Admin Password is due to expire from tomorrow";
                            else if (ts.Days == 1) MsgExpiry = "Admin Password is due to expire in 1 day";
                            else MsgExpiry = "Admin Password is due to expire in " + ts.Days.ToString() + " days";
                        }
                    }
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


        


        public bool IsNotForcedPasscode(string EmployeeID)
        {
            string val = "";
            string strSQLComm = " select ForcedPasscode from Employee where employeeid=@empid ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@empid", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@empid"].Value = EmployeeID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = objsqlReader["ForcedPasscode"].ToString();
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return (val == "N");
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


        public bool IsNotClockoutEmployee(string EmployeeID)
        {
            int val = 0;
            string strSQLComm = " select count(*) as rcnt from AttendanceInfo where EmpId in (select Id from Employee where employeeid=@empid) and DayStart is not null and DayEnd is null ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@empid", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@empid"].Value = EmployeeID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        val = Functions.fnInt32(objsqlReader["rcnt"].ToString());
                    }
                    catch { objsqlReader.Close(); }
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return (val == 1);
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

        public int GetLoginDetails(string logUser, string logPassword, int SigninOption)
        {
            int intresult = 0;
            string strSQLComm = "";

            if (SigninOption == 0)
            {
                strSQLComm = " select count(*) as rcnt from Employee where EmployeeID=@EmployeeID and Password=@Password ";
            }

            if (SigninOption == 1)
            {
                strSQLComm = " select count(*) as rcnt from Employee where EmployeeID=@EmployeeID ";
            }

            if (SigninOption == 2)
            {
                strSQLComm = " select count(*) as rcnt from Employee where Password=@Password ";
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if ((SigninOption == 0) || (SigninOption == 1))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@EmployeeID"].Value = logUser;
                }
                if ((SigninOption == 0) || (SigninOption == 2))
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@Password"].Value = logPassword;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["rcnt"].ToString());
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

        public int GetActiveUser()
        {
            int intresult = 0;
            string strSQLComm = "";

            strSQLComm = " select isnull(count(distinct(hostname)),0) as NoOfConnections from sys.sysprocesses "
                       + " where dbid > 0 and db_name(dbid) = 'PosDB' and hostname <> '' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["NoOfConnections"].ToString());
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


        public int GetActiveUser1()
        {
            int intresult = 0;
            string strSQLComm = "";

            strSQLComm = " select count(*) as NoOfConnections from sys.sysprocesses where dbid > 0 and db_name(dbid) = 'PosDB' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                SqlDataReader objSQLReader = null;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["NoOfConnections"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intresult;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public DataTable FetchAllEmployees()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, EmployeeID, FirstName, LastName, Email from Employee ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UserName", System.Type.GetType("System.String"));

                string strDisplay = "";
                while (objSQLReader.Read())
                {
                    
                    strDisplay = objSQLReader["FirstName"].ToString().Substring(0,1).ToUpper() + objSQLReader["LastName"].ToString().Substring(0, 1).ToUpper();
                    dtbl.Rows.Add(new object[] {
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["EmployeeID"].ToString(),
                                                   strDisplay,
                                                   objSQLReader["Email"].ToString(),
                    objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString()});
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


        #region Auto Process

        public int InsertAutoInfo(DateTime sdt)
        {
            int rval = -1;
            string strSQLComm = " insert into AutoProcessInfo( StartTime,StatusMessage) values( @StartTime,@StatusMessage) "
                              + " select @@IDENTITY as ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@StartTime", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@StatusMessage", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@StartTime"].Value = sdt;
                objSQlComm.Parameters["@StatusMessage"].Value = "Start";

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    try
                    {
                        rval = Functions.fnInt32(objSQLReader["ID"].ToString());
                    }
                    catch
                    {
                    }
                    objSQLReader.Close();
                }
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return rval;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return -1;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string UpdateAutoInfoDate(int pID, DateTime edt)
        {
            string strSQLComm = " update AutoProcessInfo set EndTime=@EndTime where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EndTime", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@EndTime"].Value = edt;
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

        public string UpdateAutoInfoParameters(int pID, string FName, string FVal, string msgVal)
        {
            string strSQLComm = " update AutoProcessInfo set " + FName + " =@Val, StatusMessage = @msg where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Val", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@msg", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@Val"].Value = FVal;
                objSQlComm.Parameters["@msg"].Value = msgVal;
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

        public string UpdateAutoInfoStatus(int pID, string msgVal)
        {
            string strSQLComm = " update AutoProcessInfo set StatusMessage = @msg where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = sqlConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@msg", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@msg"].Value = msgVal;
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
    }
}
