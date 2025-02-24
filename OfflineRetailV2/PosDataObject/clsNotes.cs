using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;

/*
 purpose : Data for Employee / Customer Notes, Documents , attachments
*/

namespace PosDataObject
{
    public class Notes
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strNote;
        private string strSpecialEvent;
        private int intRefID;
        private string strRefType;
        private DateTime dtDateTime;
        private string strAttachFilePath;
        private string strAttachFileExt;

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

        public int RefID 
        {
            get { return intRefID; }
            set { intRefID = value; }
        }

        public string Note
        {
            get { return strNote; }
            set { strNote = value; }
        }

        public string SpecialEvent
        {
            get { return strSpecialEvent; }
            set { strSpecialEvent = value; }
        }

        public string RefType
        {
            get { return strRefType; }
            set { strRefType = value; }
        }

        public DateTime DateTime
        {
            get { return dtDateTime; }
            set { dtDateTime = value; }
        }

        public string AttachFilePath
        {
            get { return strAttachFilePath; }
            set { strAttachFilePath = value; }
        }

        public string AttachFileExt
        {
            get { return strAttachFileExt; }
            set { strAttachFileExt = value; }
        }

        #endregion

        private byte[] GetPhoto(string filePath)
        {
            if (filePath.Trim() == "") return null;
            if (filePath.Trim() == "null") return null;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] photo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return photo;
        }

        #region Insert Note Data

        public string InsertNoteData()
        {
            string strSQLComm = "";

            strSQLComm = " insert into Notes(RefType,RefID,Note,DateTime,SpecialEvent,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                       + " values (@RefType,@RefID,@Note,@DateTime,@SpecialEvent,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@RefType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Note", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DateTime", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@SpecialEvent", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefType"].Value = strRefType;
                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@Note"].Value = strNote;
                
                objSQlComm.Parameters["@SpecialEvent"].Value = strSpecialEvent;
                if (dtDateTime == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateTime"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateTime"].Value = dtDateTime;
                }
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

        #region Update Note Data

        public string UpdateNoteData()
        {
            string strSQLComm = "";

            strSQLComm = " update notes set Note=@Note,DateTime=@DateTime,SpecialEvent=@SpecialEvent,LastChangedBy=@LastChangedBy, "
                       + " LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Note", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DateTime", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@SpecialEvent", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Note"].Value = strNote;
                
                objSQlComm.Parameters["@SpecialEvent"].Value = strSpecialEvent;
                if (dtDateTime == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateTime"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateTime"].Value = dtDateTime;
                }
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

        #region Fetch Note data

        public DataTable FetchNoteData(string strCriteria, string pRefType, int pRef, string configDateFormat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (strCriteria == "S")
            {
                strSQLComm = " select ID,Note,DateTime,SpecialEvent,LastChangedOn,DocumentFile,ScanFile from Notes "
                           + " where RefID=@RefID and RefType=@RefType and SpecialEvent = 'Y' order by DateTime desc  ";
            }
            else
            {
                strSQLComm = " select ID,Note,DateTime,SpecialEvent,LastChangedOn,DocumentFile,ScanFile from Notes "
                           + " where RefID=@RefID and RefType=@RefType ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefID"].Value = pRef;

            objSQlComm.Parameters.Add(new SqlParameter("@RefType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@RefType"].Value = pRefType;
           
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Note", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SpecialEvent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SEDateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CommonDateTime", System.Type.GetType("System.DateTime"));
                dtbl.Columns.Add("Attach", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AttachMark", System.Type.GetType("System.Byte[]"));
                dtbl.Columns.Add("SDateTime", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    string strSEvent = "";
                    string strNoteDate = "";
                    string strSEDate = "";
                    string strSDate = "";
                    string strCommonDate = "";
                    string attachfile = "N";

                    if (objSQLReader["SpecialEvent"].ToString() == "Y")
                    {
                        strSEvent = "Yes";
                        strNoteDate = Functions.fnDate(objSQLReader["LastChangedOn"].ToString()).ToString(configDateFormat +  " hh:mm:ss tt");
                        strSEDate = Functions.fnDate(objSQLReader["DateTime"].ToString()).ToString(configDateFormat + " hh:mm:ss tt");
                        strCommonDate = Functions.fnDate(objSQLReader["DateTime"].ToString()).ToString(configDateFormat + " hh:mm:ss tt");
                        strSDate = Functions.fnDate(objSQLReader["DateTime"].ToString()).ToString();
                    }
                    else
                    {
                        strSEvent = "No";
                        strSEDate = "";
                        strSDate = "";
                        strNoteDate = Functions.fnDate(objSQLReader["LastChangedOn"].ToString()).ToString(configDateFormat + " hh:mm:ss tt");
                        strCommonDate = Functions.fnDate(objSQLReader["LastChangedOn"].ToString()).ToString(configDateFormat +  " hh:mm:ss tt");
                    }
                    if ((objSQLReader["DocumentFile"].ToString() == "") && (objSQLReader["ScanFile"].ToString() == "")) attachfile = "N";
                    else attachfile = "Y";
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Note"].ToString(),
												   strNoteDate,strSEvent,strSEDate,Functions.fnDate(strCommonDate),attachfile,null,strSDate });
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

        #region Show Record based on ID for Note

        public DataTable ShowNoteRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from Notes where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Note", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateTime", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SpecialEvent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocumentFile", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScanFile", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["Note"].ToString(),
												objSQLReader["DateTime"].ToString(),
                                                objSQLReader["SpecialEvent"].ToString(),
                                                objSQLReader["DocumentFile"].ToString(),
                                                objSQLReader["ScanFile"].ToString()});
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

        #region Note Count

        public int NoteCount(int pRefID, string pRefType)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from CUSTNOTE where RefID=@RefID and RefType=@RefType ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefID"].Value = pRefID;

                objSQlComm.Parameters.Add(new SqlParameter("@RefType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@RefType"].Value = pRefType;

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
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }
        #endregion

        #region Get Min/Max Date from Note

        public DataTable GetMinMaxNote()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select Min(DateTime) as MinDate, Max(DateTime) as MaxDate "
                              + " from Notes where RefID = @ID and RefType = @Type ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intID;
            objSQlComm.Parameters.Add(new SqlParameter("@Type", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@Type"].Value = strRefType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MinDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MaxDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["MinDate"].ToString(),
												objSQLReader["MaxDate"].ToString()});
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

        public string GetMinSpecialEventDate()
        {
            string strSDate = "";

            string strSQLComm = " select Min(DateTime) as MinDate from Notes where RefID = @ID and RefType = @Type and SpecialEvent = 'Y' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRefID;
            objSQlComm.Parameters.Add(new SqlParameter("@Type", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@Type"].Value = strRefType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strSDate =  objSQLReader["MinDate"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strSDate;
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

        #region Delete Note

        public string DeleteNote(int DeleteID)
        {
            string strSQLComm = " Delete from Notes Where ID = @ID";

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

        public string UpdateDocumentFile(int pID, string pFile)
        {
            
            string strSQLComm = "";

            strSQLComm = " update notes set DocumentFile = @DocumentFile where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DocumentFile", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@DocumentFile"].Value = pFile;

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

        public string UpdateScanFile(int pID, string pFile)
        {
            string strSQLComm = "";

            strSQLComm = " update notes set ScanFile = @ScanFile where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ScanFile", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@ScanFile"].Value = pFile;

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
