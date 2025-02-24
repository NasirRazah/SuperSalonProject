/*
 purpose : Data for Employee Attendance,Payroll and Card Payment
 */

using System;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Attendance
    {
        private SqlConnection sqlConn;
        private int intID;
        private int intLoginID;
        private int intEMPID;
        private int intShiftID;

        private DateTime dtTaskStartDate;
        private DateTime dtTaskEndDate;

        private DateTime dtShiftStartDate;
        private DateTime dtShiftEndDate;

        private double dblCashTip;
        private double dblCCTip;
        private double dblTotalSales;


        public Attendance()
        {
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public double TotalSales
        {
            get { return dblTotalSales; }
            set { dblTotalSales = value; }
        }

        public double CashTip
        {
            get { return dblCashTip; }
            set { dblCashTip = value; }
        }

        public double CCTip
        {
            get { return dblCCTip; }
            set { dblCCTip = value; }
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

        public int EMPID
        {
            get { return intEMPID; }
            set { intEMPID = value; }
        }

        public int ShiftID
        {
            get { return intShiftID; }
            set { intShiftID = value; }
        }

        public DateTime TaskStartDate
        {
            get { return dtTaskStartDate; }
            set { dtTaskStartDate = value; }

        }
        public DateTime TaskEndDate
        {
            get { return dtTaskEndDate; }
            set { dtTaskEndDate = value; }
        }

        public DateTime ShiftStartDate
        {
            get { return dtShiftStartDate; }
            set { dtShiftStartDate = value; }

        }
        public DateTime ShiftEndDate
        {
            get { return dtShiftEndDate; }
            set { dtShiftEndDate = value; }
        }

        #region Fetchdata

        public DataTable FetchData(string sqlFilter, int pEmp, DateTime pStart, DateTime pEnd, string configDateFormat)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select a.ID as MID, a.EMPID, a.DAYSTART, a.DAYEND,d.ShiftName, d.StartTime, d.EndTime "
                         + " from AttendanceInfo a Left Join  ShiftMaster d  on a.ShiftID = d.ID "
                         + " where ( 1 = 1) " + sqlFilter + " order by a.DAYSTART DESC ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pEmp > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@EID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@EID"].Value = pEmp;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKSTARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKSTARTTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKENDTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALENDARSTARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALENDARENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHIFTDETAILS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHIFTTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WORKTIME", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    string strTime1 = "";
                    string strTime2 = "";
                    string strShift = "";
                    string dtpatrn = configDateFormat + " hh:mm:ss tt";

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");

                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                            int inIndex1 = strDate1.IndexOf(":00 ");
                            int inLength = strDate1.Length;
                            strTime1 = strDate1.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();

                            strTime1 = strTime1 + " " + strDate1.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).ToShortTimeString();
                            strDate1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).Date.ToShortDateString();
                        }
                    }
                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();

                        int inIndex = strDate2.IndexOf(" ");

                        if (inIndex > 0)
                        {
                            strDate2 = Functions.fnDate(strDate2).ToString(dtpatrn);
                            int inLength = strDate2.Length;
                            int inIndex1 = strDate2.IndexOf(":00 ");
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).TimeOfDay.ToString();
                            strTime2 = strDate2.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime2 = strTime2 + " " + strDate2.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).ToShortTimeString();
                            strDate2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).Date.ToShortDateString();
                        }
                    }

                    strShift = objSQLReader["ShiftName"].ToString() + "  -  " + objSQLReader["StartTime"].ToString() + " - " +
                               objSQLReader["EndTime"].ToString();

                    string strShiftDate1 = "";
                    string strShiftDate2 = "";
                    double intCalculateTime = 0;
                    int ShiftTotalInMinute = 0;
                    int strTime = 0;
                    DateTime CalculateFrom = Convert.ToDateTime(null);
                    DateTime CalculateTo = Convert.ToDateTime(null);


                    TimeSpan ts = new TimeSpan();

                    if (objSQLReader["StartTime"].ToString() != "")
                    {
                        strShiftDate1 = objSQLReader["StartTime"].ToString();
                        int inIndex = strShiftDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strShiftDate1 = strShiftDate1.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["StartTime"].ToString()) < FormatStartDate)
                        {
                            DateTime ss = Functions.fnDate(objSQLReader["StartTime"].ToString());
                            CalculateFrom = FormatStartDate.AddSeconds(-1);
                        }
                        else
                        {
                            CalculateFrom = Functions.fnDate(objSQLReader["StartTime"].ToString());
                        }
                    }

                    if (objSQLReader["EndTime"].ToString() != "")
                    {
                        strShiftDate2 = objSQLReader["EndTime"].ToString();
                        int inIndex = strShiftDate2.IndexOf(" ");
                        int inLength = strShiftDate2.Length;
                        if (inIndex > 0)
                        {
                            strShiftDate2 = strShiftDate2.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["EndTime"].ToString()) > FormatEndDate)
                        {
                            CalculateTo = FormatEndDate.AddSeconds(1);
                        }
                        else
                        {
                            CalculateTo = Functions.fnDate(objSQLReader["EndTime"].ToString());
                        }
                    }
                    else
                    {
                        CalculateTo = FormatEndDate.AddSeconds(1);
                    }

                    ts = CalculateTo.Subtract(CalculateFrom);

                    intCalculateTime = (ts.Days * 24 + ts.Hours);
                    strTime = Functions.fnInt32(intCalculateTime * 60);

                    ShiftTotalInMinute = ((ts.Days * 24 + ts.Hours) * 60) + ts.Minutes;


                    int ShiftTotalInMinute1 = 0;

                    if ((objSQLReader["DAYSTART"].ToString() != "") && (objSQLReader["DAYEND"].ToString() != ""))
                    {
                        string strShiftDate3 = "";
                        string strShiftDate4 = "";
                        double intCalculateTime1 = 0;

                        int strTime4 = 0;
                        DateTime CalculateFrom1 = Convert.ToDateTime(null);
                        DateTime CalculateTo1 = Convert.ToDateTime(null);


                        TimeSpan ts1 = new TimeSpan();

                        if (objSQLReader["DAYSTART"].ToString() != "")
                        {
                            strShiftDate3 = objSQLReader["DAYSTART"].ToString();
                            int inIndex = strShiftDate3.IndexOf(" ");
                            if (inIndex > 0)
                            {
                                strShiftDate3 = strShiftDate3.Substring(0, inIndex).Trim();
                            }

                            if (Functions.fnDate(objSQLReader["DAYSTART"].ToString()) < FormatStartDate)
                            {
                                DateTime ss = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                                CalculateFrom1 = FormatStartDate.AddSeconds(-1);
                            }
                            else
                            {
                                CalculateFrom1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                            }
                        }

                        if (objSQLReader["DAYEND"].ToString() != "")
                        {
                            strShiftDate2 = objSQLReader["DAYEND"].ToString();
                            int inIndex = strShiftDate2.IndexOf(" ");
                            int inLength = strShiftDate2.Length;
                            if (inIndex > 0)
                            {
                                strShiftDate2 = strShiftDate2.Substring(0, inIndex).Trim();
                            }

                            if (Functions.fnDate(objSQLReader["DAYEND"].ToString()) > FormatEndDate)
                            {
                                CalculateTo1 = FormatEndDate.AddSeconds(1);
                            }
                            else
                            {
                                CalculateTo1 = Functions.fnDate(objSQLReader["DAYEND"].ToString());
                            }
                        }
                        else
                        {
                            CalculateTo1 = FormatEndDate.AddSeconds(1);
                        }

                        ts1 = CalculateTo1.Subtract(CalculateFrom1);

                        intCalculateTime1 = (ts1.Days * 24 + ts1.Hours);

                        ShiftTotalInMinute1 = ((ts1.Days * 24 + ts1.Hours) * 60) + ts1.Minutes;
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["MID"].ToString(),
                                                objSQLReader["EMPID"].ToString(),
                                                strDate1,strDate2,strTime1,strTime2,
                                                objSQLReader["DAYSTART"].ToString(),
                                                objSQLReader["DAYEND"].ToString(),strShift,
                                                ShiftTotalInMinute,
                                                objSQLReader["DAYSTART"].ToString() != "" && objSQLReader["DAYEND"].ToString() != "" ? ShiftTotalInMinute1.ToString() : ""});
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

        public DataTable FetchRecord()
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " Select a.ID, a.EMPID, a.DAYSTART, a.DAYEND,e.FirstName + ' ' + e.LastName as EMP,a.ShiftID "
                         + " from AttendanceInfo a Left outer Join  Employee e  on a.EMPID = e.ID "
                         + " where a.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;


                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHIFTID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DAYSTART", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DAYEND", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["EMPID"].ToString(),
                                                objSQLReader["EMP"].ToString(),
                                                objSQLReader["ShiftID"].ToString(),
                                                objSQLReader["DAYSTART"].ToString(),
                                                objSQLReader["DAYEND"].ToString()});
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

        public int IsHolidayFound(DateTime pDate)
        {
            int intresult = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from HOLIDAYMASTER where HOLIDAYDATE = @SDT  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = new DateTime(pDate.Year, pDate.Month, pDate.Day);

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

        #region Insert Shift Start Data

        public string InsertShiftStartData()
        {
            string strSQLComm = " insert into AttendanceInfo( EMPID,ShiftID,"
                                + " DayStart,DayEnd,ShiftStartDate,ShiftEndDate,"
                                + " CreatedBy,LastChangedBy,CreatedOn,LastChangedOn) "
                                + " values( @EMPID,@ShiftID,"
                                + " @TaskStartDate,@TaskEndDate,@ShiftStartDate,@ShiftEndDate, "
                                + " @CreatedBy,@LastChangedBy,@CreatedOn,@LastChangedOn ) "
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


                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskEndDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@EMPID"].Value = intEMPID;

                objSQlComm.Parameters["@ShiftID"].Value = intShiftID;
                objSQlComm.Parameters["@TaskStartDate"].Value = dtTaskStartDate;
                if (dtTaskEndDate == Convert.ToDateTime(null))
                    objSQlComm.Parameters["@TaskEndDate"].Value = Convert.DBNull;
                else
                    objSQlComm.Parameters["@TaskEndDate"].Value = dtTaskEndDate;


                objSQlComm.Parameters["@ShiftStartDate"].Value = dtShiftStartDate;
                objSQlComm.Parameters["@ShiftEndDate"].Value = dtShiftEndDate;

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

        #region Update Shift End Task
        public string UpdateShiftEndData(int intFID)
        {
            string strSQLComm = " update AttendanceInfo set DayEnd=@TaskEndDate,LastChangedBy=@LastChangedBy,"
                                + " LastChangedOn=@LastChangedOn,CashTip=@CashTip,CCTip=@CCTip,ExpFlag = 'N' where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@TaskEndDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CashTip", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CCTip", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@TaskEndDate"].Value = dtTaskEndDate;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@CashTip"].Value = dblCashTip;
                objSQlComm.Parameters["@CCTip"].Value = dblCCTip;
                objSQlComm.Parameters["@ID"].Value = intFID;

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

        public string InsertAttendanceData()
        {
            string strSQLComm = " insert into AttendanceInfo( EMPID,ShiftID,DayStart,DayEnd,ShiftStartDate,ShiftEndDate,CreatedBy,LastChangedBy,CreatedOn,LastChangedOn,IsModified) "
                              + " values( @EMPID,@ShiftID,@TaskStartDate,@TaskEndDate,@ShiftStartDate,@ShiftEndDate,@CreatedBy,@LastChangedBy,@CreatedOn,@LastChangedOn,'Y' ) "
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


                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskEndDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@EMPID"].Value = intEMPID;

                objSQlComm.Parameters["@ShiftID"].Value = intShiftID;
                objSQlComm.Parameters["@TaskStartDate"].Value = dtTaskStartDate;
                if (dtTaskEndDate == Convert.ToDateTime(null))
                    objSQlComm.Parameters["@TaskEndDate"].Value = Convert.DBNull;
                else
                    objSQlComm.Parameters["@TaskEndDate"].Value = dtTaskEndDate;


                objSQlComm.Parameters["@ShiftStartDate"].Value = dtShiftStartDate;
                objSQlComm.Parameters["@ShiftEndDate"].Value = dtShiftEndDate;

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

        #region Update Attendance

        public string UpdateAttendanceData()
        {
            string strSQLComm = " update AttendanceInfo set ShiftID=@ShiftID,DayStart=@TaskStartDate, DayEnd=@TaskEndDate, "
                                + " ShiftStartDate=@ShiftStartDate,ShiftEndDate=@ShiftEndDate,LastChangedBy=@LastChangedBy, "
                                + " LastChangedOn=@LastChangedOn,IsModified='Y' where ID=@ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@TaskEndDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftStartDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ShiftEndDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;

                objSQlComm.Parameters["@ShiftID"].Value = intShiftID;
                objSQlComm.Parameters["@TaskStartDate"].Value = dtTaskStartDate;
                if (dtTaskEndDate == Convert.ToDateTime(null))
                    objSQlComm.Parameters["@TaskEndDate"].Value = Convert.DBNull;
                else
                    objSQlComm.Parameters["@TaskEndDate"].Value = dtTaskEndDate;

                objSQlComm.Parameters["@ShiftStartDate"].Value = dtShiftStartDate;
                objSQlComm.Parameters["@ShiftEndDate"].Value = dtShiftEndDate;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginID;
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

        #region Fetch Late Data
        public DataTable FetchLateData(int pID, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLfilter2 = "";
            string strSQLfilter3 = "";

            if (pID > 0)
            {
                strSQLfilter2 = " and b.ID = @pID ";
            }
            strSQLfilter3 = " order by b.LASTNAME, a.DAYSTART ";

            strSQLComm = " Select a.ID as MID, a.EMPID, a.DAYSTART, a.DAYEND, "
                         + " b.EMPLOYEEID, b.LASTNAME,b.FIRSTNAME, "
                         + " e.ShiftName, e.StartTime as ShiftStart, e.EndTime as ShiftEnd, "
                         + " e.ShiftDuration, a.ShiftStartDate, a.ShiftEndDate "
                         + " from AttendanceInfo a Left Join Employee b on a.EMPID = b.ID "
                         + " Left Join ShiftMaster e  on a.ShiftID = e.ID  "
                         + " where ( 1 = 1) "
                         + " and a.DAYSTART > a.ShiftStartDate "
                         + strSQLfilter2
                         + " and a.DAYSTART between @SDT and @FDT " + strSQLfilter3;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@pID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@pID"].Value = pID;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                //objSQlComm.Parameters.Add(new OleDbParameter("@SDT1", System.Data.OleDb.OleDbType.DBTimeStamp));
                //objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                //objSQlComm.Parameters.Add(new OleDbParameter("@FDT1", System.Data.OleDb.OleDbType.DBTimeStamp));
                //objSQlComm.Parameters["@FDT1"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DEPTNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STARTTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ENDTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftStart", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftEnd", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftStartDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShiftEndDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STARTDATETIME", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";

                    string strTime1 = "";
                    string strTime2 = "";
                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                            int inIndex1 = strDate1.IndexOf(":00 ");
                            int inLength = strDate1.Length;
                            strTime1 = strDate1.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime1 = strTime1 + " " + strDate1.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).ToShortTimeString();
                            strDate1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).Date.ToShortDateString();
                        }

                    }

                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();
                        int inIndex = strDate2.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate2 = Functions.fnDate(strDate2).ToString(dtpatrn);
                            int inIndex1 = strDate2.IndexOf(":00 ");
                            int inLength = strDate2.Length;
                            strTime2 = strDate2.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime2 = strTime2 + " " + strDate2.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).ToShortTimeString();
                            strDate2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).Date.ToShortDateString();
                        }

                    }


                    dtbl.Rows.Add(new object[] {objSQLReader["MID"].ToString(),
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["LASTNAME"].ToString() + ", " + objSQLReader["FIRSTNAME"].ToString(),
                                                "",
                                                strDate1,strDate2,strTime1,strTime2,
                                                objSQLReader["ShiftName"].ToString(),
                                                objSQLReader["ShiftStart"].ToString(),
                                                objSQLReader["ShiftEnd"].ToString(),
                                                objSQLReader["ShiftDuration"].ToString(),
                                                objSQLReader["ShiftStartDate"].ToString(),
                                                objSQLReader["ShiftEndDate"].ToString(),
                                                objSQLReader["DAYSTART"].ToString()});

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

        #region Fetch Initial Absent Data

        public DataTable FetchInitialAbsentData(int pID)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLfilter2 = "";
            string strSQLfilter3 = "";

            if (pID > 0)
            {
                strSQLfilter2 = " and a.ID = @pID ";
            }

            strSQLfilter3 = " order by a.LASTNAME ";

            strSQLComm = " Select a.ID as EMPID, a.EMPLOYEEID, a.LASTNAME, a.FIRSTNAME from Employee a  "
                         + " where ( 1 = 1) " + strSQLfilter2 + strSQLfilter3;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@pID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@pID"].Value = pID;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("EMPID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["EMPID"].ToString(),
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["LASTNAME"].ToString() + ", " + objSQLReader["FIRSTNAME"].ToString()});
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

        public int IsTaskFound(int pEmpID, DateTime pFrom, DateTime pTo)
        {
            int intresult = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from ATTENDANCEINFO where EMPID = @EMPID "
                                + " and DAYSTART between @SDT and @FDT ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@EMPID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPID"].Value = pEmpID;
                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = pFrom;
                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = pTo;

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

        public int IsHolidayFoundForAbsent(DateTime pDate)
        {
            int intresult = 0;
            string strSQLComm = " select count(*) AS RECCOUNT from HOLIDAYMASTER where HOLIDAYDATE = @SDT  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = new DateTime(pDate.Year, pDate.Month, pDate.Day);

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

        #region Fetch Attendance Data

        public DataTable FetchAttendanceData(int pID, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLfilter2 = "";
            string strSQLfilter3 = "";

            if (pID > 0)
            {
                strSQLfilter2 = " and b.ID = @pID ";
            }
            strSQLfilter3 = " order by b.LASTNAME, a.DAYSTART ";

            strSQLComm = " select a.ID as MID, a.EMPID, a.DAYSTART, a.DAYEND, "
                         + " b.EMPLOYEEID, b.LASTNAME,b.FIRSTNAME,b.EmpRate, "
                         + " e.ShiftName, e.StartTime as ShiftStart, e.EndTime as ShiftEnd, "
                         + " e.ShiftDuration, a.ShiftStartDate, a.ShiftEndDate,a.IsModified "
                         + " from AttendanceInfo a Left Join Employee b on a.EMPID = b.ID "
                         + " Left Join ShiftMaster e  on a.ShiftID = e.ID  "
                         + " where ( 1 = 1) " + strSQLfilter2
                         + " and a.DAYSTART between @SDT and @FDT " + strSQLfilter3;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@pID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@pID"].Value = pID;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STARTTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ENDTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Shift", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Modified", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Time", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";

                    string strTime1 = "";
                    string strTime2 = "";
                    string strShift = "";
                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";

                    double intCalculateTime = 0;
                    int strTime = 0;
                    DateTime CalculateFrom = Convert.ToDateTime(null);
                    DateTime CalculateTo = Convert.ToDateTime(null);

                    TimeSpan ts = new TimeSpan();

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                            int inIndex1 = strDate1.IndexOf(":00 ");
                            int inLength = strDate1.Length;
                            strTime1 = strDate1.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime1 = strTime1 + " " + strDate1.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).ToShortTimeString();
                            strDate1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).Date.ToShortDateString();
                        }

                        CalculateFrom = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                    }

                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();
                        int inIndex = strDate2.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate2 = Functions.fnDate(strDate2).ToString(dtpatrn);
                            int inIndex1 = strDate2.IndexOf(":00 ");
                            int inLength = strDate2.Length;
                            strTime2 = strDate2.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime2 = strTime2 + " " + strDate2.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).ToShortTimeString();
                            strDate2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).Date.ToShortDateString();
                        }

                        CalculateTo = Functions.fnDate(objSQLReader["DAYEND"].ToString());
                    }


                    if ((objSQLReader["DAYSTART"].ToString() != "") && (objSQLReader["DAYEND"].ToString() != ""))
                    {

                        ts = CalculateTo.Subtract(CalculateFrom);

                        intCalculateTime = (ts.Days * 24 + ts.Hours);
                        if (intCalculateTime > 0) strTime = Functions.fnInt32(intCalculateTime * 60);

                        if (ts.Minutes > 0)
                        {
                            if (strTime > 0) strTime = strTime + ts.Minutes;
                            else strTime = ts.Minutes;
                        }

                    }


                    strShift = objSQLReader["ShiftName"].ToString() + "   ( " +
                               objSQLReader["ShiftStart"].ToString() + " - " +
                               objSQLReader["ShiftEnd"].ToString() + " )";

                    dtbl.Rows.Add(new object[] {objSQLReader["MID"].ToString(),
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["LASTNAME"].ToString() + ", " + objSQLReader["FIRSTNAME"].ToString(),
                                                objSQLReader["FIRSTNAME"].ToString(),
                                                objSQLReader["LASTNAME"].ToString(),
                                                objSQLReader["EMPRATE"].ToString(),
                                                strDate1,strDate2,strTime1,strTime2,
                                                strShift,
                                                objSQLReader["IsModified"].ToString(),
                                                strTime.ToString()});
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

        #region Fetch Payroll Data

        public DataTable FetchPayrollDataForReport(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " Select a.ID as MID, a.EMPID, a.DAYSTART, a.DAYEND,b.EMPLOYEEID, b.LASTNAME,b.FIRSTNAME, b.EMPRATE "
                         + " from AttendanceInfo a Left Join Employee b on a.EMPID = b.ID where ( 1 = 1) "
                         + " and ((a.DAYSTART between @SDT and @FDT) or (a.DAYSTART between @SDT and @FDT)) "
                         + " order by a.EMPID, a.DAYSTART ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT1"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    double intCalculateTime = 0;
                    double dblRate = 0.00;
                    double dblCalculateRate = 0.00;
                    int strTime = 0;
                    DateTime CalculateFrom = Convert.ToDateTime(null);
                    DateTime CalculateTo = Convert.ToDateTime(null);


                    TimeSpan ts = new TimeSpan();

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = strDate1.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYSTART"].ToString()) < FormatStartDate)
                        {
                            DateTime ss = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                            CalculateFrom = FormatStartDate.AddSeconds(-1);
                        }
                        else
                        {
                            CalculateFrom = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                        }
                    }

                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();
                        int inIndex = strDate2.IndexOf(" ");
                        int inLength = strDate2.Length;
                        if (inIndex > 0)
                        {
                            strDate2 = strDate2.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYEND"].ToString()) > FormatEndDate)
                        {
                            CalculateTo = FormatEndDate.AddSeconds(1);
                        }
                        else
                        {
                            CalculateTo = Functions.fnDate(objSQLReader["DAYEND"].ToString());
                        }
                    }
                    else
                    {
                        CalculateTo = FormatEndDate.AddSeconds(1);
                    }

                    ts = CalculateTo.Subtract(CalculateFrom);

                    intCalculateTime = (ts.Days * 24 + ts.Hours);
                    if (intCalculateTime > 0) strTime = Functions.fnInt32(intCalculateTime * 60);

                    dblRate = Functions.fnDouble(objSQLReader["EMPRATE"].ToString());
                    double minRate = 0;
                    if (ts.Minutes > 0)
                    {

                        if (dblRate == 0)
                        {
                            minRate = 0;
                        }
                        else
                        {
                            minRate = dblRate / 60;
                        }
                        if (strTime > 0) strTime = strTime + ts.Minutes;
                        else strTime = ts.Minutes;
                    }

                    dblCalculateRate = Functions.fnDouble(Functions.fnDouble(intCalculateTime * dblRate + Functions.fnDouble(ts.Minutes) * minRate).ToString("f"));

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["LASTNAME"].ToString() + ", " + objSQLReader["FIRSTNAME"].ToString(),
                                                objSQLReader["EMPRATE"].ToString(),
                                                strTime.ToString(), dblCalculateRate.ToString()});

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

        public DataTable FetchPayrollDataForExport(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();

            string strSQLComm = "";

            strSQLComm = " select a.ID as MID,a.EMPID,a.DAYSTART,a.DAYEND,b.EMPLOYEEID,b.LASTNAME,b.FIRSTNAME,b.EMPRATE "
                       + " from AttendanceInfo a Left Join Employee b on a.EMPID = b.ID where ( 1 = 1) "
                       + " and ((a.DAYSTART between @SDT and @FDT) or (a.DAYSTART between @SDT and @FDT)) "
                       + " order by a.EMPID, a.DAYSTART ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT1"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    double intCalculateTime = 0;
                    double dblRate = 0.00;
                    double dblCalculateRate = 0.00;
                    int strTime = 0;
                    DateTime CalculateFrom = Convert.ToDateTime(null);
                    DateTime CalculateTo = Convert.ToDateTime(null);


                    TimeSpan ts = new TimeSpan();

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = strDate1.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYSTART"].ToString()) < FormatStartDate)
                        {
                            DateTime ss = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                            CalculateFrom = FormatStartDate.AddSeconds(-1);
                        }
                        else
                        {
                            CalculateFrom = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                        }
                    }

                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();
                        int inIndex = strDate2.IndexOf(" ");
                        int inLength = strDate2.Length;
                        if (inIndex > 0)
                        {
                            strDate2 = strDate2.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYEND"].ToString()) > FormatEndDate)
                        {
                            CalculateTo = FormatEndDate.AddSeconds(1);
                        }
                        else
                        {
                            CalculateTo = Functions.fnDate(objSQLReader["DAYEND"].ToString());
                        }
                    }
                    else
                    {
                        CalculateTo = FormatEndDate.AddSeconds(1);
                    }

                    ts = CalculateTo.Subtract(CalculateFrom);

                    intCalculateTime = (ts.Days * 24 + ts.Hours);
                    if (intCalculateTime > 0) strTime = Functions.fnInt32(Convert.ToString(intCalculateTime * 60));

                    dblRate = Functions.fnDouble(objSQLReader["EMPRATE"].ToString());
                    double minRate = 0;
                    if (ts.Minutes > 0)
                    {
                        if (dblRate == 0)
                        {
                            minRate = 0;
                        }
                        else
                        {
                            minRate = dblRate / 60;
                        }
                        if (strTime > 0) strTime = strTime + ts.Minutes;
                        else strTime = ts.Minutes;
                    }

                    dblCalculateRate = Functions.fnDouble(Functions.fnDouble(Convert.ToString(intCalculateTime * dblRate + Functions.fnDouble(ts.Minutes.ToString()) * minRate)).ToString("f"));

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["FIRSTNAME"].ToString() + " " + objSQLReader["LASTNAME"].ToString(),
                                                objSQLReader["EMPRATE"].ToString(),
                                                strTime.ToString(), dblCalculateRate.ToString()
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

        public DataTable ReportCardTranData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();

            string strSQLComm = "";
            //strSQLComm = " select c.CardType, c.SaleTranNo,c.SaleOn,c.InvoiceNo,c.InvoiceAmount,isnull(tr.TransactionId,'') TransactionId, isnull(tr.ApprovalCode,'') ApprovalCode, "
            //             + " isnull(e.FirstName,'ADMIN') as Emp from CardAuthorisation c "
            //             + " left outer join employee e on c.SaleBy = e.ID  "
            //               + " left outer join XeConnectTransactions tr on c.InvoiceNo = tr.InvoiceNumber "
            //             + " where c.SaleOn between @SDT and @FDT order by c.SaleOn DESC, c.InvoiceNo ";

            strSQLComm = @"select distinct c.CardType, c.SaleTranNo,c.SaleOn,c.InvoiceNo,c.InvoiceAmount,
                            isnull(ct.RefCardAuthID,'') as ApprovalCode, isnull(ct.RefCardTranID,'') as TransactionId, isnull (RefCardAuthAmount, 0.0) as RefCardAuthAmount,
                            isnull(e.FirstName,'ADMIN') as Emp 
                            from CardAuthorisation c 
                            left outer join CardTrans ct on c.InvoiceNo=ct.Reference
                            left outer join employee e on c.SaleBy = e.ID   
                            where c.SaleOn between @SDT and @FDT 
                            order by c.SaleOn DESC, c.InvoiceNo";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("CardType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AuthorisedTranNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("AuthorisedOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TransactionId", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ApprovalCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardAuthAmount", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";

                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";
                    if (objSQLReader["SaleOn"].ToString() != "")
                    {
                        strDate1 = objSQLReader["SaleOn"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                        }
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["CardType"].ToString(),
                                                objSQLReader["SaleTranNo"].ToString(),
                                                objSQLReader["InvoiceNo"].ToString(),
                                                Functions.fnDouble(objSQLReader["InvoiceAmount"].ToString()),
                                                strDate1,
                                                objSQLReader["Emp"].ToString(),
                                                objSQLReader["TransactionId"].ToString(),
                                                objSQLReader["ApprovalCode"].ToString(),
                                                Functions.fnDouble(objSQLReader["RefCardAuthAmount"].ToString())
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

        public DataTable ReportCardTranData_New(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();

            string strSQLComm = "";
            strSQLComm = " select c.CardType,c.CardAmount,c.TransactionNo,c.PaymentGateway,c.RefCardLogo,c.RefCardAuthID,c.RefCardTranID, "
                         + " c.RefCardAuthAmount,c.TransactionType,c.CreatedOn,isnull(e.FirstName,'ADMIN') as Emp from CardTrans c "
                         + " left outer join employee e on c.EmployeeID = e.ID where c.CreatedOn between @SDT and @FDT "
                         + " and ((c.AuthCode is not null and c.TransactionNo <> 0 and c.PaymentGateway = 1) or "
                         + " (c.TransactionType is not null and c.PaymentGateway = 2) or (c.TransactionType is not null and c.TransactionNo <> 0 and c.PaymentGateway = 3) "
                         + " or (c.TransactionType is not null and c.TransactionNo <> 0 and c.PaymentGateway = 5)) order by c.CreatedOn desc, c.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("CardType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CardAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TransactionDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransactionNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentGateway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardLogo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardAuthID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardTranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardAuthAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TransactionType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";

                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";
                    if (objSQLReader["CreatedOn"].ToString() != "")
                    {
                        strDate1 = objSQLReader["CreatedOn"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                        }
                    }

                    string pmtgtway = "";
                    if (objSQLReader["PaymentGateway"].ToString() == "1") pmtgtway = "Element Express";
                    if (objSQLReader["PaymentGateway"].ToString() == "2") pmtgtway = "Mercury";
                    if (objSQLReader["PaymentGateway"].ToString() == "3") pmtgtway = "Precidia";
                    if (objSQLReader["PaymentGateway"].ToString() == "5") pmtgtway = "Datacap";
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["CardType"].ToString(),
                                                Functions.fnDouble(objSQLReader["CardAmount"].ToString()),
                                                strDate1,
                                                objSQLReader["TransactionNo"].ToString(),
                                                pmtgtway,
                                                objSQLReader["RefCardLogo"].ToString(),
                                                objSQLReader["RefCardAuthID"].ToString(),
                                                objSQLReader["RefCardTranID"].ToString(),
                                                Functions.fnDouble(objSQLReader["RefCardAuthAmount"].ToString()),
                                                objSQLReader["TransactionType"].ToString(),
                                                objSQLReader["Emp"].ToString()});
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


        public DataTable ReportCardTranData_NewCType(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();

            try
            {
                var dtA = ReportCardTranData(pStart, pEnd);
                var dtB = ReportCardTranData_New(pStart, pEnd);

                dtbl = dtB.Clone();
                dtbl = dtB.Copy();

                foreach (DataRow item in dtA.Rows)
                { 
                    dtbl.Rows.Add(new object[] {
                                                item["CardType"].ToString(),
                                                Functions.fnDouble(item["InvoiceAmount"].ToString()),
                                                item["AuthorisedOn"].ToString(),   
                                                item["InvoiceNo"].ToString(),
                                                "", //pmtgtway,
                                                "", // item["RefCardLogo"].ToString(),
                                                item["ApprovalCode"].ToString(), //item["RefCardAuthID"].ToString(),
                                                item["AuthorisedTranNo"].ToString(), // item["AuthorisedTranNo"].ToString(), //item["RefCardTranID"].ToString(),
                                                Functions.fnDouble(item["RefCardAuthAmount"].ToString()),
                                                "", // item["TransactionType"].ToString(),
                                                item["Employee"].ToString()});

                }

                return dtbl;

            }
            catch (Exception ex)
            {
                string strErrMsg = "";
                strErrMsg = ex.Message;


                return null;
            }
        }



        public DateTime GetSystemDateTime()
        {
            DateTime dt = DateTime.Today;
            string strSQLComm = "";
            strSQLComm = " select getdate() as sysdt ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dt = Functions.fnDate(objSQLReader["sysdt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return DateTime.Today;
            }
        }

        public int GetTotalShifts()
        {
            int intresult = 0;
            string strSQLComm = " select count(*) as rcnt from shiftmaster ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

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

        public int GetDefaultShift()
        {
            int intresult = 0;
            string strSQLComm = " select count(*) as rcnt from shiftmaster where shiftname = 'Default Import Shift' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

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

        public DataTable FetchCCAuthData(string sqlFilter, int pEmp, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();

            string strFilter = "";
            if (sqlFilter == "Authorised and Not Completed") strFilter = " and BatchFlag = 'N' ";
            if (sqlFilter == "Authorised and Completed") strFilter = " and BatchFlag = 'Y' ";

            string strSQLComm = "";

            strSQLComm = " select * from CardAuthorisation where InvoiceAmount > 0 and AuthorisedBy = @EID and (AuthorisedTranNo <> '' "
                       + " or AuthorisedTranNo is not null) and AuthorisedOn between @SDT and @FDT " + strFilter + " order by AuthorisedOn desc, InvoiceNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                if (pEmp > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@EID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@EID"].Value = pEmp;
                }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AuthorisedTranNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CompleteTranNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TipAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("AuthorisedOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CompleteOn", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";

                    if (objSQLReader["AuthorisedOn"].ToString() != "")
                    {
                        strDate1 = objSQLReader["AuthorisedOn"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                        }
                    }
                    if (objSQLReader["CompleteOn"].ToString() != "")
                    {
                        strDate2 = objSQLReader["CompleteOn"].ToString();

                        int inIndex = strDate2.IndexOf(" ");

                        if (inIndex > 0)
                        {
                            strDate2 = Functions.fnDate(strDate2).ToString(dtpatrn);
                        }
                    }



                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["AuthorisedTranNo"].ToString(),
                                                objSQLReader["CompleteTranNo"].ToString(),
                                                objSQLReader["InvoiceNo"].ToString(),
                                                Functions.fnDouble(objSQLReader["InvoiceAmount"].ToString()),
                                                Functions.fnDouble(objSQLReader["TipAmount"].ToString()),
                                                strDate1,strDate2});
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

        public DataTable FetchCardTransData(int pTrnID, string pRefID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select * from cardtrans where transactionno = @ID and RefCardAuthID=@TID and RefCardAuthID is not null order by ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pTrnID;
                objSQlComm.Parameters.Add(new SqlParameter("@TID", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@TID"].Value = pRefID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CardType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CardAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AuthCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryAcqRef", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryToken", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentGateway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Reference", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryProcessData", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardTranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardAuthAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryPurchaseAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsDebitCard", System.Type.GetType("System.String"));

                int i = 0;
                while (objSQLReader.Read())
                {
                    i++;
                    if (i > 2) break;
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["CustomerID"].ToString(),
                                                objSQLReader["CardType"].ToString(),
                                                objSQLReader["CardAmount"].ToString(),
                                                objSQLReader["AuthCode"].ToString(),
                                                objSQLReader["MercuryAcqRef"].ToString(),
                                                objSQLReader["MercuryToken"].ToString(),
                                                objSQLReader["PaymentGateway"].ToString(),
                                                objSQLReader["Reference"].ToString(),
                                                objSQLReader["MercuryInvoiceNo"].ToString(),
                                                objSQLReader["MercuryProcessData"].ToString(),
                                                objSQLReader["RefCardTranID"].ToString(),
                                                objSQLReader["RefCardAuthAmount"].ToString(),
                                                objSQLReader["MercuryPurchaseAmount"].ToString(),
                                                objSQLReader["IsDebitCard"].ToString()});
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

        public DataTable FetchCardTransData1(int pTrnID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select * from cardtrans where id = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pTrnID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CardType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CardAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AuthCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryAcqRef", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryToken", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentGateway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Reference", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryInvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryProcessData", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardTranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefCardAuthAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MercuryPurchaseAmount", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["CustomerID"].ToString(),
                                                objSQLReader["CardType"].ToString(),
                                                objSQLReader["CardAmount"].ToString(),
                                                objSQLReader["AuthCode"].ToString(),
                                                objSQLReader["MercuryAcqRef"].ToString(),
                                                objSQLReader["MercuryToken"].ToString(),
                                                objSQLReader["PaymentGateway"].ToString(),
                                                objSQLReader["Reference"].ToString(),
                                                objSQLReader["MercuryInvoiceNo"].ToString(),
                                                objSQLReader["MercuryProcessData"].ToString(),
                                                objSQLReader["RefCardTranID"].ToString(),
                                                objSQLReader["RefCardAuthAmount"].ToString(),
                                                objSQLReader["MercuryPurchaseAmount"].ToString()});
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

        public DataTable FetchClockInData()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select a.ID as MID,a.EMPID,a.DAYSTART,a.DAYEND,d.ShiftName,d.StartTime,d.EndTime  "
                       + " from AttendanceInfo a Left Join ShiftMaster d on a.ShiftID = d.ID where ( 1 = 1) "
                       + " and a.DAYSTART is not null and a.DAYEND is null order by a.DAYSTART desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKSTARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKSTARTTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TASKENDTIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALENDARSTARTDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CALENDARENDDATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SHIFTDETAILS", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    string strTime1 = "";
                    string strTime2 = "";
                    string strShift = "";
                    string dtpatrn = "M/d/yyyy hh:mm:ss tt";

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = Functions.fnDate(strDate1).ToString(dtpatrn);
                            int inIndex1 = strDate1.IndexOf(":00 ");
                            int inLength = strDate1.Length;
                            strTime1 = strDate1.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime1 = strTime1 + " " + strDate1.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).ToShortTimeString();
                            strDate1 = Functions.fnDate(objSQLReader["DAYSTART"].ToString()).Date.ToShortDateString();
                        }
                    }
                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();

                        int inIndex = strDate2.IndexOf(" ");

                        if (inIndex > 0)
                        {
                            strDate2 = Functions.fnDate(strDate2).ToString(dtpatrn);
                            int inLength = strDate2.Length;
                            int inIndex1 = strDate2.IndexOf(":00 ");
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).TimeOfDay.ToString();
                            strTime2 = strDate2.Substring(inIndex + 1, inIndex1 - inIndex - 1).Trim();
                            strTime2 = strTime2 + " " + strDate2.Substring(inIndex1 + 3, inLength - inIndex1 - 3).Trim();
                            strTime2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).ToShortTimeString();
                            strDate2 = Functions.fnDate(objSQLReader["DAYEND"].ToString()).Date.ToShortDateString();
                        }
                    }

                    strShift = objSQLReader["ShiftName"].ToString() + "  -  " + objSQLReader["StartTime"].ToString() + " - " +
                               objSQLReader["EndTime"].ToString();

                    dtbl.Rows.Add(new object[] {objSQLReader["MID"].ToString(),
                                                objSQLReader["EMPID"].ToString(),
                                                strDate1,strDate2,strTime1,strTime2,
                                                objSQLReader["DAYSTART"].ToString(),
                                                objSQLReader["DAYEND"].ToString(),strShift});
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

        public string DeleteAttendanceRecord(int DeleteID)
        {
            string strSQLComm = " Delete from AttendanceInfo Where ID = @ID";

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
    }
}
