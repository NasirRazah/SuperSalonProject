/*
 purpose : Data for Emails ( Messages between employees)
*/

using System;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Messages
    {
        private SqlConnection sqlConn;
        private int intID;
        private int intNewID;
        private string strErrorMsg;
        private string strMsgType;

        private int intSenderID;
        private int intRecipientID;
        private string strEmailSubject;
        private string strEmailBody;
        private string strPriority;
        private string strEmailStatus;
        private string strEmailRead;
        private DateTime dtEMailTime;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        public Messages()
        {
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
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

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
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

        public string MsgType
        {
            get { return strMsgType; }
            set { strMsgType = value; }
        }

        public string EmailSubject
        {
            get { return strEmailSubject; }
            set { strEmailSubject = value; }
        }

        public string EmailBody
        {
            get { return strEmailBody; }
            set { strEmailBody = value; }
        }

        public string EmailStatus
        {
            get { return strEmailStatus; }
            set { strEmailStatus = value; }
        }

        public string Priority
        {
            get { return strPriority; }
            set { strPriority = value; }
        }

        public string EmailRead
        {
            get { return strEmailRead; }
            set { strEmailRead = value; }
        }

        public int SenderID
        {
            get { return intSenderID; }
            set { intSenderID = value; }
        }

        public int RecipientID
        {
            get { return intRecipientID; }
            set { intRecipientID = value; }
        }

        public DateTime EMailTime
        {
            get { return dtEMailTime; }
            set { dtEMailTime = value; }
        }


        #region Fetch data
        public DataTable FetchData(int pRecipient)
        {
            
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select top(50) a.ID, a.EmailSubject, a.EmailBody, a.EMailTime, a.Priority, b.EmailRead, "
                       + " e.LastName, e.FirstName, a.SenderID, b.RecipientID, b.ID as RRowID "
                       + " from MailItems a Right outer join Recipients b  on a.ID = b.MailID "
                       + " and a.SenderID = b.SenderID Left outer join Employee e on a.SenderID = e.ID "
                       + " where b.RecipientID = @RID order by a.EMailTime desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RID"].Value = pRecipient;
                

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsRead", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMailSubject", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sender", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RRowID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["Priority"].ToString(),
                                                objSQLReader["EmailRead"].ToString(),
                                                objSQLReader["EmailSubject"].ToString(),
                                                objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                objSQLReader["EMailTime"].ToString(),
                                                objSQLReader["SenderID"].ToString(),
                                                objSQLReader["RecipientID"].ToString(),
                                                objSQLReader["RRowID"].ToString()});
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

        #region Fetch Sent Items
        public DataTable FetchSentItems(int pSender)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select top(50) a.ID, a.EmailSubject, a.EmailBody, a.EMailTime, a.Priority,e.LastName, e.FirstName, a.SenderID "
                       + " from MailItems a Right outer join  Recipients b  on a.ID = b.MailID and a.SenderID = b.SenderID "
                       + " left outer join Employee e on b.RecipientID = e.ID where a.SenderID = @SID order by a.EMailTime desc, a.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SID"].Value = pSender;
                
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMailSubject", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SID", System.Type.GetType("System.String"));

                int PrevID = 0;
                int CurrID = 0;

                while (objSQLReader.Read())
                {
                    CurrID = Functions.fnInt32(objSQLReader["ID"].ToString());
                    if ((CurrID != PrevID) || (PrevID == 0))
                    {
                        dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["Priority"].ToString(),
                                                objSQLReader["EmailSubject"].ToString(),
                                                objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                objSQLReader["EMailTime"].ToString(),
                                                objSQLReader["SenderID"].ToString()
                                                });
                    }

                    PrevID = CurrID;
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

        #region Fetch Recipients

        public DataTable FetchRecipients(int pSender)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            strSQLComm = " select ID,FirstName,LastName from Employee order by FirstName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                                
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                false});
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

        public bool SendMail()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                
                InsertMailHeader(SaveComm);
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

        #region Insert Mail Header

        public bool InsertMailHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert Into MailItems (SenderID,EmailSubject,EmailBody,Priority,EMailTime) "
                                  + " values ( @SenderID,@EmailSubject,@EmailBody,@Priority,@EMailTime ) select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@SenderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EmailSubject", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmailBody", System.Data.SqlDbType.NText));
                objSQlComm.Parameters.Add(new SqlParameter("@Priority", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMailTime", System.Data.SqlDbType.DateTime));
                
                objSQlComm.Parameters["@SenderID"].Value = intSenderID;
                objSQlComm.Parameters["@EmailSubject"].Value = strEmailSubject;
                objSQlComm.Parameters["@EmailBody"].Value = strEmailBody;
                objSQlComm.Parameters["@Priority"].Value = strPriority;
                objSQlComm.Parameters["@EMailTime"].Value = dtEMailTime;
                
                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intID = Functions.fnInt32(sqlDataReader["ID"]);

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

        #endregion

        private void AdjustSplitGridRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colRecipientID = "";

                    if (dr["ID"].ToString() == "") continue;

                    if (dr["ID"].ToString() != "")
                    {
                        colRecipientID = dr["ID"].ToString();
                    }
                    intRecipientID = Functions.fnInt32(colRecipientID);

                    InsertRecipients(objSQlComm, intID);

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertRecipients(SqlCommand objSQlComm, int intMailID)
        {
            int intRecpID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into Recipients (MailID,SenderID,RecipientID,EMailRead,CreatedOn,LastChangedOn) "
                                  + " values (@MailID,@SenderID,@RecipientID,@EMailRead,@CreatedOn,@LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MailID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SenderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RecipientID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EMailRead", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@MailID"].Value = intMailID;
                objSQlComm.Parameters["@SenderID"].Value = intSenderID;
                objSQlComm.Parameters["@RecipientID"].Value = intRecipientID;
                objSQlComm.Parameters["@EMailRead"].Value = strEmailRead;

                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intRecpID = Functions.fnInt32(sqlDataReader["ID"]);

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

        #region Show Mail based on ID

        public DataTable ShowMail(int intMailID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.*, e.LastName, e.FirstName from MailItems a left outer join Employee e "
                              + " on a.SenderID = e.ID where a.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intMailID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SenderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmailSubject", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmailBody", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMailTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Sender", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SenderID"].ToString(),
                                                objSQLReader["EmailSubject"].ToString(),
                                                objSQLReader["Priority"].ToString(),
                                                objSQLReader["EmailBody"].ToString(),
                                                objSQLReader["EMailTime"].ToString(),
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

        #endregion

        #region Show Recipient based on ID

        public DataTable ShowRecipients(int intMailID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.SenderID, a.RecipientID, e.LastName, e.FirstName "
                              + " from Recipients a left outer join Employee e "
                              + " On a.RecipientID = e.ID where a.MailID = @MailID order by a.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@MailID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@MailID"].Value = intMailID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SenderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecipientID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Recipient", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {
												objSQLReader["SenderID"].ToString(),
                                                objSQLReader["RecipientID"].ToString(),
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

        #endregion

        #region Update Read Status

        public string UpdateReadStatus(int intRID)
        {
            string strSQLComm = " update Recipients set EMailRead='Yes', LastChangedOn= @LastChangedOn Where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intRID;
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

        #region Delete Data

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " delete from Recipients Where ID = @ID";

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

        #region Count UnRead EMail

        public int CountUnreadMail(int pRID)
        {
            int intRecCount = 0;
            string strSQLComm = "";

            strSQLComm = " Select count(*) as RecCount from Recipients where RecipientID=@RID "
                       + " and RecipientID <> SenderID and EmailRead = 'No'  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RID"].Value = pRID;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intRecCount = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                
                return intRecCount;
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
    }
}
