/*
 purpose : Data for Employee Shift
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace PosDataObject
{
    public class Shift
    {
        private SqlConnection sqlConn;

        private int intID;
        private int intLoginID;
        private string strShiftName;
        private string dtStartTime;
        private string dtEndTime;
        private int intShiftDuration;
        private string strShiftStatus;
        public Shift()
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

        public int ShiftDuration
        {
            get { return intShiftDuration; }
            set { intShiftDuration = value; }
        }

        public string ShiftName
        {
            get { return strShiftName; }
            set { strShiftName = value; }
        }

        public string StartTime
        {
            get { return dtStartTime; }
            set { dtStartTime = value; }
        }

        public string EndTime
        {
            get { return dtEndTime; }
            set { dtEndTime = value; }
        }

        public string ShiftStatus
        {
            get { return strShiftStatus; }
            set { strShiftStatus = value; }
        }

        #region Insert Data

        public string InsertData()
        {
            string strSQLComm = " insert into ShiftMaster( ShiftName, StartTime, EndTime, ShiftDuration, "
                              + " CreatedBy, LastChangedBy, CreatedOn, LastChangedOn) "
                              + " values( @ShiftName,@StartTime, @EndTime, @ShiftDuration, "
                              + " @CreatedBy, @LastChangedBy, @CreatedOn, @LastChangedOn ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ShiftName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StartTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EndTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftDuration", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ShiftName"].Value = strShiftName;
                objSQlComm.Parameters["@StartTime"].Value = dtStartTime;
                objSQlComm.Parameters["@EndTime"].Value = dtEndTime;
                objSQlComm.Parameters["@ShiftDuration"].Value = intShiftDuration;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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
        
        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select ID,ShiftName,StartTime,EndTime,ShiftDuration,ShiftStatus from ShiftMaster order by StartTime,ShiftName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftStatus", System.Type.GetType("System.String"));

                int intTime = 0;
                int intHH = 0;
                string strDuration = "";
                string strStatus = "";

                while (objSQLReader.Read())
                {
                    intTime = Functions.fnInt32(objSQLReader["ShiftDuration"].ToString());
                    if (intTime > 0)
                    {
                        if (intTime < 60)
                        {
                            strDuration = Convert.ToString(intTime) + " min";
                        }
                        else
                        {
                            intHH = Functions.fnInt32(Math.Ceiling(Functions.fnDouble(intTime / 60)));
                            if (intTime - intHH * 60 == 0)
                            {
                                strDuration = Convert.ToString(intHH) + " hr";
                            }
                            else
                            {
                                strDuration = Convert.ToString(intHH) + " hr " + Convert.ToString(intTime - intHH * 60) + " min";
                            }
                        }
                    }

                    if (objSQLReader["ShiftStatus"].ToString() == "Start")
                    {
                        strStatus = "Started";
                    }
                    if (objSQLReader["ShiftStatus"].ToString() == "Stop")
                    {
                        strStatus = "Stopped";
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["ShiftName"].ToString(),
                                                objSQLReader["StartTime"].ToString(),
                                                objSQLReader["EndTime"].ToString(),strDuration,
                                                strStatus});
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

        public int DuplicateShift()
        {
            int intresult = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from ShiftMaster where ShiftName = @ShiftName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ShiftName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ShiftName"].Value = strShiftName;

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

        #region Update Data

        public string UpdateData()
        {
            string strSQLComm = " update ShiftMaster set ShiftName=@ShiftName, StartTime=@StartTime, "
                              + " EndTime=@EndTime, ShiftDuration=@ShiftDuration, ShiftStatus=@ShiftStatus, "
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

                objSQlComm.Parameters.Add(new SqlParameter("@ShiftName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StartTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EndTime", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftDuration", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftStatus", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ShiftName"].Value = strShiftName;
                objSQlComm.Parameters["@StartTime"].Value = dtStartTime;
                objSQlComm.Parameters["@EndTime"].Value = dtEndTime;
                objSQlComm.Parameters["@ShiftDuration"].Value = intShiftDuration;
                objSQlComm.Parameters["@ShiftStatus"].Value = strShiftStatus;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ID"].Value = intID;

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

        #region Delete Data

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = "DELETE from ShiftMaster where ID = @ID";

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

        public DataTable FetchRecord()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " Select * from ShiftMaster where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StartTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EndTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftStatus", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["ShiftName"].ToString(),
                                                objSQLReader["StartTime"].ToString(),
                                                objSQLReader["EndTime"].ToString(),
                                                objSQLReader["ShiftDuration"].ToString(),
                                                objSQLReader["ShiftStatus"].ToString()});
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

        public int MaxID()
        {
            int intresult = 0;
            string strSQLComm = "select MAX(ID) AS MAXID from ShiftMaster ";

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

        public int GetEmpShiftID(int pEmp)
        {
            int intresult = 0;
            string strSQLComm = " select EMPSHIFT from Employee where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pEmp;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["EMPSHIFT"].ToString());
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
        
        public int GetOpenShift(int pShift)
        {
            int intresult = 0;
            string strSQLComm = " select count(*) as RecInfo from AttendanceInfo where ShiftID = @ID and DayEnd is null ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pShift;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["RecInfo"].ToString());
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

        public int GetEmpShiftStatus(int pEmp)
        {
            int intresult = 0;
            string strSQLComm = " select count(*) as RECCOUNT from AttendanceInfo where EMPID = @EMPID and DayEnd is null ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPID"].Value = pEmp;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["RECCOUNT"].ToString());
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
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public int GetStartShift(int pEmp)
        {
            int intresult = 0;
            string strSQLComm = " select ID from AttendanceInfo where EMPID = @EMPID and DayEnd is null ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPID"].Value = pEmp;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["ID"].ToString());
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
        
        public int IfExistShift(int pShift)
        {
            int intresult = 0;

            string strSQLComm = " select COUNT(*) AS RECCOUNT from AttendanceInfo where ShiftID = @ShiftID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ShiftID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ShiftID"].Value = pShift;

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

        public string GetShiftStatus(int pShift)
        {
            string strresult = "";
            string strSQLComm = " select ShiftStatus from ShiftMaster where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pShift;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = objSQLReader["ShiftStatus"].ToString();
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

        public DateTime GetStartDate(int pEmp)
        {
            DateTime intresult = DateTime.Now;

            string strSQLComm = " select DayStart from AttendanceInfo where EMPID = @EMPID and DayEnd is null ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPID"].Value = pEmp;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnDate(objSQLReader["DayStart"].ToString());
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
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return DateTime.Now;
            }
        }

        public int GetClockIN(int pEmp)
        {
            int intresult = 0;
            string strSQLComm = " SELECT count(*) as RecCount FROM AttendanceInfo "
                                + " WHERE EMPID = @EMPID and DayEnd is null ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPID"].Value = pEmp;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = Functions.fnInt32(objSQLReader["RecCount"].ToString());
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
                objSQLReader.Close();
                sqlConn.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        #region Update Shift Status

        public string UpdateShiftStatus(string strValue)
        {
            string strSQLComm = " update ShiftMaster set ShiftStatus=@ShiftStatus,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ShiftStatus", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ShiftStatus"].Value = strValue;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ID"].Value = intID;

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
