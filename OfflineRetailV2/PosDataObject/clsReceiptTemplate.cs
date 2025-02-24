using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class ReceiptTemplate
    {
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private DataTable dtblTemplateData;

        private int intpSL;
        private string strpHeader1;
        private string strpHeader2;
        private string strpHeader3;

        private string strpHeaderCaption1;
        private string strpHeaderCaption2;
        private string strpHeaderCaption3;

        private int intpID;
        private int intpGroupSL;
        private int intpGroupSubSL;
        private string strpGroupData;
        private string strpTextAlign;
        private string strpTextStyle;

        private int intpFontSize;
        private int intpCtrlWidth;
        private int intpCtrlHeight;
        private byte[] bytCustomImage;
        private int intpCtrlPositionTop;


        public DataTable TemplateData
        {
            get { return dtblTemplateData; }
            set { dtblTemplateData = value; }
        }

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strTemplateName;
        private string strTemplateType;
        private string strTemplateSize;


        private int intActiveTemplate1;
        private int intActiveTemplate2;
        private int intActiveTemplate3;
        private int intActiveTemplate4;
        private int intActiveTemplate5;
        private int intActiveTemplate6;
        private int intActiveTemplate7;
        private int intActiveTemplate8;
        private int intActiveTemplate9;
        private int intActiveTemplate10;
        private int intActiveTemplate11;
        private int intActiveTemplate12;
        private int intActiveTemplate13;
        private int intActiveTemplate14;
        private int intActiveTemplate15;
        private int intActiveTemplate16;
        private int intActiveTemplate17;


        public int ActiveTemplate1
        {
            get { return intActiveTemplate1; }
            set { intActiveTemplate1 = value; }
        }

        public int ActiveTemplate2
        {
            get { return intActiveTemplate2; }
            set { intActiveTemplate2 = value; }
        }

        public int ActiveTemplate3
        {
            get { return intActiveTemplate3; }
            set { intActiveTemplate3 = value; }
        }

        public int ActiveTemplate4
        {
            get { return intActiveTemplate4; }
            set { intActiveTemplate4 = value; }
        }

        public int ActiveTemplate5
        {
            get { return intActiveTemplate5; }
            set { intActiveTemplate5 = value; }
        }

        public int ActiveTemplate6
        {
            get { return intActiveTemplate6; }
            set { intActiveTemplate6 = value; }
        }

        public int ActiveTemplate7
        {
            get { return intActiveTemplate7; }
            set { intActiveTemplate7 = value; }
        }

        public int ActiveTemplate8
        {
            get { return intActiveTemplate8; }
            set { intActiveTemplate8 = value; }
        }

        public int ActiveTemplate9
        {
            get { return intActiveTemplate9; }
            set { intActiveTemplate9 = value; }
        }

        public int ActiveTemplate10
        {
            get { return intActiveTemplate10; }
            set { intActiveTemplate10 = value; }
        }

        public int ActiveTemplate11
        {
            get { return intActiveTemplate11; }
            set { intActiveTemplate11 = value; }
        }

        public int ActiveTemplate12
        {
            get { return intActiveTemplate12; }
            set { intActiveTemplate12 = value; }
        }

        public int ActiveTemplate13
        {
            get { return intActiveTemplate13; }
            set { intActiveTemplate13 = value; }
        }

        public int ActiveTemplate14
        {
            get { return intActiveTemplate14; }
            set { intActiveTemplate14 = value; }
        }

        public int ActiveTemplate15
        {
            get { return intActiveTemplate15; }
            set { intActiveTemplate15 = value; }
        }

        public int ActiveTemplate16
        {
            get { return intActiveTemplate16; }
            set { intActiveTemplate16 = value; }
        }

        public int ActiveTemplate17
        {
            get { return intActiveTemplate17; }
            set { intActiveTemplate17 = value; }
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

        public string TemplateName
        {
            get { return strTemplateName; }
            set { strTemplateName = value; }
        }

        public string TemplateType
        {
            get { return strTemplateType; }
            set { strTemplateType = value; }
        }

        public string TemplateSize
        {
            get { return strTemplateSize; }
            set { strTemplateSize = value; }
        }


        private int intPrinterTypeID;
        public int PrinterTypeID
        {
            get { return intPrinterTypeID; }
            set { intPrinterTypeID = value; }
        }

        private int intAttachedPrinterID;
        public int AttachedPrinterID
        {
            get { return intAttachedPrinterID; }
            set { intAttachedPrinterID = value; }
        }

        private int intPrintCopy;
        public int PrintCopy
        {
            get { return intPrintCopy; }
            set { intPrintCopy = value; }
        }

        private int intLabelTemplateWidth;
        public int LabelTemplateWidth
        {
            get { return intLabelTemplateWidth; }
            set { intLabelTemplateWidth = value; }
        }

        private int intLabelTemplateHeight;
        public int LabelTemplateHeight
        {
            get { return intLabelTemplateHeight; }
            set { intLabelTemplateHeight = value; }
        }

        private SqlConnection sqlConn;

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        #region Receipt Template Master

        public string InsertData()
        {
            string strSQLComm = " insert into ReceiptTemplateMaster( TemplateName,TemplateType,TemplateSize,"
                              + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,PrinterTypeID,AttachedPrinterID,PrintCopy) "
                              + " values( @TemplateName,@TemplateType,@TemplateSize,@CreatedBy,"
                              + " @CreatedOn,@LastChangedBy,@LastChangedOn,@PrinterTypeID,@AttachedPrinterID,@PrintCopy) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@TemplateName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateSize", System.Data.SqlDbType.VarChar));

                
                objSQlComm.Parameters.Add(new SqlParameter("@PrinterTypeID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AttachedPrinterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintCopy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PrinterTypeID"].Value = intPrinterTypeID;
                objSQlComm.Parameters["@AttachedPrinterID"].Value = intAttachedPrinterID;
                objSQlComm.Parameters["@PrintCopy"].Value = intPrintCopy;

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@TemplateName"].Value = strTemplateName;
                objSQlComm.Parameters["@TemplateType"].Value = strTemplateType;
                objSQlComm.Parameters["@TemplateSize"].Value = strTemplateSize;

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

        public string UpdateData()
        {
            string strSQLComm = " update ReceiptTemplateMaster set TemplateName=@TemplateName, TemplateSize=@TemplateSize, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,PrinterTypeID=@PrinterTypeID,"
                              + " AttachedPrinterID=@AttachedPrinterID,PrintCopy=@PrintCopy where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateSize", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@TemplateName"].Value = strTemplateName;
                objSQlComm.Parameters["@TemplateSize"].Value = strTemplateSize;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@PrinterTypeID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AttachedPrinterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintCopy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PrinterTypeID"].Value = intPrinterTypeID;
                objSQlComm.Parameters["@AttachedPrinterID"].Value = intAttachedPrinterID;
                objSQlComm.Parameters["@PrintCopy"].Value = intPrintCopy;

                

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

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, TemplateName, TemplateType, TemplateSize, LabelTemplateWidth, LabelTemplateHeight from ReceiptTemplateMaster ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateSize", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TemplateName"].ToString(),
                                                   objSQLReader["TemplateType"].ToString(),
                                                   objSQLReader["TemplateType"].ToString() == "1 Up Label" ? objSQLReader["LabelTemplateWidth"].ToString()  + " X "  + objSQLReader["LabelTemplateHeight"].ToString() : objSQLReader["TemplateSize"].ToString()
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

        public DataTable ShowData(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select TemplateName, TemplateType, TemplateSize, PrinterTypeID, AttachedPrinterID, PrintCopy, isnull(LabelTemplateWidth,0) as W, isnull(LabelTemplateHeight,0) as H from ReceiptTemplateMaster where ID=@ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

               
                dtbl.Columns.Add("TemplateName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateSize", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrinterTypeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AttachedPrinterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintCopy", System.Type.GetType("System.String"));

                dtbl.Columns.Add("LabelTemplateWidth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelTemplateHeight", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["TemplateName"].ToString(),
                                                   objSQLReader["TemplateType"].ToString(),
                                                   objSQLReader["TemplateSize"].ToString(),
                                                   objSQLReader["PrinterTypeID"].ToString(),
                                                   objSQLReader["AttachedPrinterID"].ToString(),
                                                   Functions.fnInt32(objSQLReader["PrintCopy"].ToString()) == 0 ? "1" : objSQLReader["PrintCopy"].ToString(),
                                                   objSQLReader["W"].ToString(),
                                                   objSQLReader["H"].ToString()
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

        public int DuplicateCount(int pID, string pTemplateType, string pTemplateSize)
        {
            int intCount = 0;
            string strSQLComm = "";

            if (pID == 0)
            {
                strSQLComm = " select count(*) as rcnt from ReceiptTemplateMaster where TemplateType = @ty and TemplateName = @sz  "; //and TemplateSize = @sz
            }
            else
            {
                strSQLComm = " select count(*) as rcnt from ReceiptTemplateMaster where TemplateType = @ty and TemplateName = @sz and ID <> " + pID.ToString(); // and TemplateSize = @sz
            }

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@ty", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@ty"].Value = pTemplateType;
                objSQlComm.Parameters.Add(new SqlParameter("@sz", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@sz"].Value = pTemplateSize;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["rcnt"]);
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

        public int DeletePrinterTemplate(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_deleteprintertemplate";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ID"].Value = DeleteID;

                objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objSQlComm.ExecuteNonQuery();
                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value.ToString());
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intReturn;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion

        public DataTable FetchDefaultParameterData(string templateType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,GroupName,SlNo from ReceiptTemplateDefaultData where TemplateType = @param order by SlNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@param"].Value = templateType;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SlNo", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["GroupName"].ToString(),
                                                   objSQLReader["SlNo"].ToString()
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

        public DataTable FetchDummyLinkData(string TemplType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @"select  isnull(d.ID,0) as GroupID,isnull(l.GroupName,'') as GroupName,isnull(d.SlNo,0) as GroupSL,
                                0 as GroupSubSL,l.GroupData,
                                l.TextAlign, l.TextStyle, l.FontSize, l.CtrlWidth, l.CtrlHeight, l.SL,
                                l.ShowHeader1,l.ShowHeader2,l.ShowHeader3,
                                l.Header1Caption,l.Header2Caption,l.Header3Caption, isnull(l.CtrlPositionTop,0) as LabelTop
                                from ReceiptTemplateDummyData l left outer join ReceiptTemplateDefaultData d
                                on l.TemplateType = d.TemplateType and l.GroupName = d.GroupName where l.TemplateType = @refID order by  l.SL ";
            //order by l.GroupSL,l.GroupSubSL

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@refID"].Value = TemplType;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupSL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupSubSL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupData", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TextAlign", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TextStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FontSize", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CtrlWidth", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CtrlHeight", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Display", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomImage", typeof(byte[]));
                dtbl.Columns.Add("SL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("ShowHeader1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShowHeader2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShowHeader3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Header1Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Header2Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Header3Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CtrlPositionTop", System.Type.GetType("System.Int32"));

                while (objSQLReader.Read())
                {
                    byte[] bytImage = null;

                    dtbl.Rows.Add(new object[] {   Functions.fnInt32(objSQLReader["GroupID"].ToString()),
                                                   objSQLReader["GroupName"].ToString(),
                                                   Functions.fnInt32(objSQLReader["GroupSL"].ToString()),
                                                   Functions.fnInt32(objSQLReader["GroupSubSL"].ToString()),
                                                   objSQLReader["GroupData"].ToString(),
                                                   objSQLReader["TextAlign"].ToString(),
                                                   objSQLReader["TextStyle"].ToString(),
                                                   Functions.fnInt32(objSQLReader["FontSize"].ToString()),
                                                   Functions.fnInt32(objSQLReader["CtrlWidth"].ToString()),
                                                   Functions.fnInt32(objSQLReader["CtrlHeight"].ToString()),
                                                   "Y",
                                                   bytImage,
                                                   Functions.fnInt32(objSQLReader["SL"].ToString()),

                                                   objSQLReader["ShowHeader1"].ToString(),
                                                   objSQLReader["ShowHeader2"].ToString(),
                                                   objSQLReader["ShowHeader3"].ToString(),
                                                   objSQLReader["Header1Caption"].ToString(),
                                                   objSQLReader["Header2Caption"].ToString(),
                                                   objSQLReader["Header3Caption"].ToString(),
                                                   Functions.fnInt32(objSQLReader["LabelTop"].ToString())
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

        public DataTable FetchLinkData(int refID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @"select l.GroupID,d.GroupName,l.GroupSL,l.GroupSubSL,l.GroupData,
                                l.TextAlign, l.TextStyle, l.FontSize, l.CtrlWidth, l.CtrlHeight, l.CustomImage,l.SL,
                                l.ShowHeader1,l.ShowHeader2,l.ShowHeader3,
                                l.Header1Caption,l.Header2Caption,l.Header3Caption, isnull(l.CtrlPositionTop,0) as LabelTop
                                from ReceiptTemplateLinkData l left outer join ReceiptTemplateDefaultData d
                                on l.GroupID = d.ID where TemplateRefID = @refID order by  l.SL ";
            //order by l.GroupSL,l.GroupSubSL

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@refID"].Value = refID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupSL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupSubSL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("GroupData", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TextAlign", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TextStyle", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FontSize", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CtrlWidth", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CtrlHeight", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Display", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomImage", typeof(byte[]));
                dtbl.Columns.Add("SL", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("ShowHeader1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShowHeader2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShowHeader3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Header1Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Header2Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Header3Caption", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CtrlPositionTop", System.Type.GetType("System.Int32"));

                while (objSQLReader.Read())
                {
                    byte[] bytImage = null;
                    try
                    {
                        bytImage = (byte[])objSQLReader["CustomImage"];
                    }
                    catch
                    {
                        bytImage = null;
                    }

                    dtbl.Rows.Add(new object[] {   Functions.fnInt32(objSQLReader["GroupID"].ToString()),
                                                   objSQLReader["GroupName"].ToString(),
                                                   Functions.fnInt32(objSQLReader["GroupSL"].ToString()),
                                                   Functions.fnInt32(objSQLReader["GroupSubSL"].ToString()),
                                                   objSQLReader["GroupData"].ToString(),
                                                   objSQLReader["TextAlign"].ToString(),
                                                   objSQLReader["TextStyle"].ToString(),
                                                   Functions.fnInt32(objSQLReader["FontSize"].ToString()),
                                                   Functions.fnInt32(objSQLReader["CtrlWidth"].ToString()),
                                                   Functions.fnInt32(objSQLReader["CtrlHeight"].ToString()),
                                                   "Y",
                                                   bytImage,
                                                   Functions.fnInt32(objSQLReader["SL"].ToString()),

                                                   objSQLReader["ShowHeader1"].ToString(),
                                                   objSQLReader["ShowHeader2"].ToString(),
                                                   objSQLReader["ShowHeader3"].ToString(),
                                                   objSQLReader["Header1Caption"].ToString(),
                                                   objSQLReader["Header2Caption"].ToString(),
                                                   objSQLReader["Header3Caption"].ToString(),
                                                   Functions.fnInt32(objSQLReader["LabelTop"].ToString())
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

        public int CheckLinkCount(int refID)
        {
            int TCnt = 0;

            string strSQLComm = @"select count(*) as rcnt from ReceiptTemplateLinkData where TemplateRefID = @refID ";
          

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@refID"].Value = refID;

                objSQLReader = objSQlComm.ExecuteReader();

                
                while (objSQLReader.Read())
                {
                    TCnt = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                    
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return TCnt;
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

        public string FetchPrinterName(string paramname, string termname)
        {
            string retval = "";

            string strSQLComm = @" select isnull(paramvalue,'') as val from localsetup 
                                   where HostName = @p1 and ParamName = @p2 ";
           
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@p1"].Value = termname;

                objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@p2"].Value = paramname;

                objSQLReader = objSQlComm.ExecuteReader();

               

                while (objSQLReader.Read())
                {
                    retval = objSQLReader["val"].ToString();

                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                if (retval == "")
                {
                    retval = "(None)";
                }

                return retval;
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


        public DataTable FetchDefaultParameterData(int linkID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select GroupName,SlNo from ReceiptTemplateDefaultData where ID = " + linkID.ToString();

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("GroupName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SlNo", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {  
                                                   objSQLReader["GroupName"].ToString(),
                                                   objSQLReader["SlNo"].ToString()
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


        public string PostData()
        {
            SaveTran = null;
            SaveCon = sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();

            SaveComm = new SqlCommand(" ", sqlConn);
            SaveComm.Transaction = SaveTran;

            string strError = "";
            try
            {
                if (intID == 0)
                {
                    strError = InsertData(SaveComm);
                }
                else
                {
                    strError = UpdateData(SaveComm);
                }
                if (strError == "")
                    strError = DeleteLinkData(SaveComm);
               
              
                    
                if (strError == "")
                    strError = SaveLinkData(SaveComm);

                
                if (strError == "")
                {
                    SaveTran.Commit();
                    SaveTran.Dispose();
                }
                else
                {
                    SaveTran.Rollback();
                    SaveTran.Dispose();
                }
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        public string SaveLinkData(SqlCommand objSQlComm)
        {
            if (dtblTemplateData == null) return "";
            string strResult = "";



            //dtblTemplateData.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            //dtblTemplateData.DefaultView.ApplyDefaultSort = true;

            //foreach (DataRowView dr in dtblTemplateData.DefaultView)
            foreach (DataRow dr in dtblTemplateData.Rows)
            {
                if (dr["Display"].ToString() == "N") continue;

                 intpSL = 0;
                 strpHeader1 = "N";
                 strpHeader2 = "N";
                 strpHeader3 = "N";

                strpHeaderCaption1 = "";
                strpHeaderCaption2 = "";
                strpHeaderCaption3 = "";

                intpID = 0;
                intpGroupSL = 0;
                intpGroupSubSL = 0;
                intpFontSize = 0;
                intpCtrlWidth = 0;
                intpCtrlPositionTop = 0;
                intpCtrlHeight = 0;
                strpGroupData = "";
                strpTextAlign = "";
                strpTextStyle = "";
                bytCustomImage = null;

                intpSL = Functions.fnInt32(dr["SL"].ToString());

                strpHeader1 = dr["ShowHeader1"].ToString();
                strpHeader2 = dr["ShowHeader2"].ToString();
                strpHeader3 = dr["ShowHeader3"].ToString();

                strpHeaderCaption1 = dr["Header1Caption"].ToString();
                strpHeaderCaption2 = dr["Header2Caption"].ToString();
                strpHeaderCaption3 = dr["Header3Caption"].ToString();

                intpID = Functions.fnInt32(dr["ID"].ToString());
                intpGroupSL = Functions.fnInt32(dr["GroupSL"].ToString());
                intpGroupSubSL = Functions.fnInt32(dr["GroupSubSL"].ToString());

                strpTextAlign = dr["TextAlign"].ToString();
                strpTextStyle = dr["TextStyle"].ToString();
                intpFontSize = Functions.fnInt32(dr["FontSize"].ToString());
                intpCtrlWidth = Functions.fnInt32(dr["CtrlWidth"].ToString());
                intpCtrlHeight = Functions.fnInt32(dr["CtrlHeight"].ToString());
                bytCustomImage = (dr["CustomImage"].GetType().ToString() == "System.DBNull") ? null : (byte[])dr["CustomImage"];

                intpCtrlPositionTop = dr["CtrlPositionTop"].ToString() == null ? 0 : Functions.fnInt32(dr["CtrlPositionTop"].ToString());

                strpGroupData = dr["GroupData"].ToString();


                

                if (strResult == "")
                {
                    strResult = InsertLinkData(objSQlComm);
                }
            }
            return strResult;
        }

        public string InsertLinkData(SqlCommand objSQlComm)
        {
            

            string strSQLComm = " insert into ReceiptTemplateLinkData(GroupID,GroupSL, GroupSubSL, FontSize,"
                              + " CtrlWidth, CtrlHeight, TextAlign, TextStyle,GroupData,CustomImage,TemplateRefID,"
                              + " SL, ShowHeader1,ShowHeader2,ShowHeader3,Header1Caption,Header2Caption,Header3Caption,CtrlPositionTop ) "
                              + " values ( @GroupID, @GroupSL, @GroupSubSL, @FontSize, "
                              + " @CtrlWidth, @CtrlHeight, @TextAlign, @TextStyle,@GroupData,@CustomImage,@TemplateRefID, "
                              + " @SL,@ShowHeader1,@ShowHeader2,@ShowHeader3,@Header1Caption,@Header2Caption,@Header3Caption,@CtrlPositionTop )";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@SL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@GroupID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GroupSL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GroupSubSL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@FontSize", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CtrlWidth", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CtrlHeight", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateRefID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@TextAlign", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TextStyle", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GroupData", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomImage", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@ShowHeader1", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowHeader2", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ShowHeader3", System.Data.SqlDbType.Char));


                objSQlComm.Parameters.Add(new SqlParameter("@Header1Caption", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Header2Caption", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Header3Caption", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CtrlPositionTop", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CtrlPositionTop"].Value = intpCtrlPositionTop;

                objSQlComm.Parameters["@SL"].Value = intpSL;
                objSQlComm.Parameters["@GroupID"].Value = intpID;
                objSQlComm.Parameters["@GroupSL"].Value = intpGroupSL;
                objSQlComm.Parameters["@GroupSubSL"].Value = intpGroupSubSL;
                objSQlComm.Parameters["@FontSize"].Value = intpFontSize;
                objSQlComm.Parameters["@CtrlWidth"].Value = intpCtrlWidth;
                objSQlComm.Parameters["@CtrlHeight"].Value = intpCtrlHeight;
                objSQlComm.Parameters["@TemplateRefID"].Value = intID;

                objSQlComm.Parameters["@TextAlign"].Value = strpTextAlign;
                objSQlComm.Parameters["@TextStyle"].Value = strpTextStyle;
                objSQlComm.Parameters["@GroupData"].Value = strpGroupData;

                objSQlComm.Parameters["@ShowHeader1"].Value = strpHeader1;
                objSQlComm.Parameters["@ShowHeader2"].Value = strpHeader2;
                objSQlComm.Parameters["@ShowHeader3"].Value = strpHeader3;

                objSQlComm.Parameters["@Header1Caption"].Value = strpHeaderCaption1;
                objSQlComm.Parameters["@Header2Caption"].Value = strpHeaderCaption2;
                objSQlComm.Parameters["@Header3Caption"].Value = strpHeaderCaption3;

                if (bytCustomImage == null)
                {
                    objSQlComm.Parameters["@CustomImage"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@CustomImage"].Value = bytCustomImage;
                }

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string DeleteLinkData(SqlCommand objSQlComm)
        {
            string strSQLComm = " delete from ReceiptTemplateLinkData where TemplateRefID = @refID ";
           
            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@refID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@refID"].Value = intID;
                
                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }



        public string InsertData(SqlCommand objSQlComm)
        {
            string strSQLComm = " insert into ReceiptTemplateMaster( TemplateName,TemplateType,TemplateSize,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,PrintCopy,LabelTemplateWidth, LabelTemplateHeight) "
                              + " values( @TemplateName,@TemplateType,@TemplateSize,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@PrintCopy,@LabelTemplateWidth, @LabelTemplateHeight) "
                              + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
          
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@TemplateName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateSize", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintCopy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PrintCopy"].Value = intPrintCopy;

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@TemplateName"].Value = strTemplateName;
                objSQlComm.Parameters["@TemplateType"].Value = strTemplateType;
                objSQlComm.Parameters["@TemplateSize"].Value = strTemplateSize;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelTemplateWidth", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LabelTemplateWidth"].Value = intLabelTemplateWidth;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelTemplateHeight", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LabelTemplateHeight"].Value = intLabelTemplateHeight;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    this.ID = Functions.fnInt32(objsqlReader["ID"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
               
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objsqlReader.Close();
                objsqlReader.Dispose();
               
                return strErrMsg;
            }
        }

        public string UpdateData(SqlCommand objSQlComm)
        {
            string strSQLComm = " update ReceiptTemplateMaster set TemplateName=@TemplateName, TemplateSize=@TemplateSize,LabelTemplateWidth=@LabelTemplateWidth, LabelTemplateHeight=@LabelTemplateHeight, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,PrinterTypeID=@PrinterTypeID,AttachedPrinterID=@AttachedPrinterID,PrintCopy=@PrintCopy where ID = @ID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TemplateSize", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@TemplateName"].Value = strTemplateName;
                objSQlComm.Parameters["@TemplateSize"].Value = strTemplateSize;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters.Add(new SqlParameter("@PrinterTypeID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@AttachedPrinterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintCopy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PrinterTypeID"].Value = intPrinterTypeID;
                objSQlComm.Parameters["@AttachedPrinterID"].Value = intAttachedPrinterID;
                objSQlComm.Parameters["@PrintCopy"].Value = intPrintCopy;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelTemplateWidth", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LabelTemplateWidth"].Value = intLabelTemplateWidth;

                objSQlComm.Parameters.Add(new SqlParameter("@LabelTemplateHeight", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LabelTemplateHeight"].Value = intLabelTemplateHeight;

                objSQlComm.ExecuteNonQuery();

               
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

              
                return strErrMsg;
            }
        }



        #region POS


        public void FetchTemplateHeaderInfoForPOSPrinting(string printtype,ref int templateID, ref string templateSize, ref int printCopy)
        {
            string TemplateType = "";

            if ((printtype == "Invoice") || (printtype == "Reprint Receipt"))
            {
                TemplateType = "Receipt";
            }
            else if ((printtype == "Layaway") || (printtype == "Reprint Layaway"))
            {
                TemplateType = "Layaway";
            }
            else if (printtype == "Rent Issue")
            {
                TemplateType = "Rent Item Issue";
            }
            else if (printtype == "Return Rent Item")
            {
                TemplateType = "Rent Item Return";
            }
            else if (printtype == "Repair In")
            {
                TemplateType = "Repair Item Receive";
            }
            else if (printtype == "Repair Deliver")
            {
                TemplateType = "Repair Item Return";
            }
            else
            {
                TemplateType = printtype;
            }

           

            string strSQLComm = "";

            if (TemplateType == "Receipt")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.ReceiptTemplate=h.ID ";
            }

            if (TemplateType == "Layaway")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.LayawayTemplate=h.ID ";
            }

            if (TemplateType == "Rent Item Issue")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.RentIssueTemplate=h.ID ";
            }

            if (TemplateType == "Rent Item Return")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.RentReturnTemplate=h.ID ";
            }

            if (TemplateType == "Repair Item Receive")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.RepairInTemplate=h.ID ";
            }

            if (TemplateType == "Repair Item Return")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.RepairDeliverTemplate=h.ID ";
            }

            if (TemplateType == "WorkOrder")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.WorkOrderTemplate=h.ID ";
            }

            if (TemplateType == "Suspend Receipt")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.SuspendReceiptTemplate=h.ID ";
            }

            if (TemplateType == "Closeout")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.CloseoutTemplate=h.ID ";
            }

            if (TemplateType == "No Sale")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.NoSaleTemplate=h.ID ";
            }

            if (TemplateType == "Paid Out")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.PaidOutTemplate=h.ID ";
            }

            if (TemplateType == "Paid In")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.PaidInTemplate=h.ID ";
            }

            if (TemplateType == "Safe Drop")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize,
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.SafeDropTemplate=h.ID ";
            }

            if (TemplateType == "Lotto Payout")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.LottoPayoutTemplate=h.ID ";
            }

            if (TemplateType == "Customer Label")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.CustomerLabelTemplate=h.ID ";
            }

            if (TemplateType == "Gift Receipt")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.GiftReceiptTemplate=h.ID ";
            }

            if (TemplateType == "Gift Aid Receipt")
            {
                strSQLComm = @" select isnull(h.ID,0) as rcdID, isnull(h.TemplateSize,'') as TSize, 
                                   isnull(h.PrintCopy,1) as TCopy from ReceiptTemplateActive a left outer join
                                   ReceiptTemplateMaster h on a.GiftAidTemplate=h.ID ";
            }

            


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@param"].Value = TemplateType;
            SqlDataReader objSQLReader = null;

            try
            {
                if (strSQLComm != "")
                {
                    if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                    objSQLReader = objSQlComm.ExecuteReader();



                    while (objSQLReader.Read())
                    {
                        templateID = Functions.fnInt32(objSQLReader["rcdID"].ToString());
                        templateSize = objSQLReader["TSize"].ToString();

                        printCopy = Functions.fnInt32(objSQLReader["TCopy"].ToString());
                        break;
                    }

                    objSQLReader.Close();
                    objSQLReader.Dispose();
                    objSQlComm.Dispose();
                    sqlConn.Close();

                    return;
                }
                else
                {
                    //objSQLReader.Close();
                    //objSQLReader.Dispose();
                    objSQlComm.Dispose();
                    sqlConn.Close();

                    return;
                }
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
        }


        public void FetchItemTemplateDetails(int templateID, ref string templateSize, ref int printCopy)
        {
           

            string strSQLComm = "";

            strSQLComm = @" select isnull(TemplateSize,'') as TSize, 
                                   isnull(PrintCopy,1) as TCopy from
                                   ReceiptTemplateMaster where ID = " + templateID.ToString();



            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
           
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();



                while (objSQLReader.Read())
                {
                    
                    templateSize = objSQLReader["TSize"].ToString();

                    printCopy = Functions.fnInt32(objSQLReader["TCopy"].ToString());
                    break;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
        }

        #endregion


        #region Template Activation

        public DataTable FetchActiveTemplateList(string templateType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @" select distinct h.ID, h.TemplateName from ReceiptTemplateLinkData l left outer join  ReceiptTemplateMaster h
                                   on l.TemplateRefID = h.ID
                                   where h.TemplateType = @param1 order by h.TemplateName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@param1"].Value = templateType;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TemplateName"].ToString()
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

        public DataTable FetchActiveTemplateListForLabel(string templateType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @" select distinct ID, TemplateName from ReceiptTemplateMaster
                                   
                                   where TemplateType = @param1 order by TemplateName";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@param1"].Value = templateType;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TemplateName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TemplateName"].ToString()
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

        public DataTable FetchActiveTemplateValue()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = @"  select isnull(ReceiptTemplate,0) as T1, isnull(LayawayTemplate,0) as T2, isnull(RentIssueTemplate,0) as T3, 
                                    isnull(RentReturnTemplate,0) as T4, isnull(RepairInTemplate,0) as T5, isnull(RepairDeliverTemplate,0) as T6, 
                                    isnull(WorkOrderTemplate,0) as T7, isnull(SuspendReceiptTemplate,0) as T8, isnull(CloseoutTemplate,0) as T9, 
                                    isnull(NoSaleTemplate,0) as T10, isnull(PaidOutTemplate,0) as T11, isnull(PaidInTemplate,0) as T12, 
                                    isnull(SafeDropTemplate,0) as T13, isnull(LottoPayoutTemplate,0) as T14, isnull(CustomerLabelTemplate,0) as T15,
                                    isnull(GiftReceiptTemplate,0) as T16, isnull(GiftAidTemplate,0) as T17
                                    from ReceiptTemplateActive";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
               
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("T1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T4", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T5", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T6", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T7", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T8", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T9", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T10", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T11", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T12", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T13", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T14", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T15", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T16", System.Type.GetType("System.String"));
                dtbl.Columns.Add("T17", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["T1"].ToString(),
                                                  objSQLReader["T2"].ToString(),
                                                  objSQLReader["T3"].ToString(),
                                                  objSQLReader["T4"].ToString(),
                                                  objSQLReader["T5"].ToString(),
                                                  objSQLReader["T6"].ToString(),
                                                  objSQLReader["T7"].ToString(),
                                                  objSQLReader["T8"].ToString(),
                                                  objSQLReader["T9"].ToString(),
                                                  objSQLReader["T10"].ToString(),
                                                  objSQLReader["T11"].ToString(),
                                                  objSQLReader["T12"].ToString(),
                                                  objSQLReader["T13"].ToString(),
                                                  objSQLReader["T14"].ToString(),
                                                  objSQLReader["T15"].ToString(),
                                                   objSQLReader["T16"].ToString(),
                                                   objSQLReader["T17"].ToString()
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

        public bool IfExistsActiveTemplate()
        {
            bool val = false;

            string strSQLComm = @" select count(*) as rcnt from ReceiptTemplateActive";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

               
                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rcnt"].ToString()) > 0;
                    
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }
        
        public string InsertActiveData()
        {
            string strSQLComm = @" insert into ReceiptTemplateActive( ReceiptTemplate, LayawayTemplate,
                                    RentIssueTemplate,RentReturnTemplate,RepairInTemplate,RepairDeliverTemplate,
                                    WorkOrderTemplate,SuspendReceiptTemplate,CloseoutTemplate,NoSaleTemplate,
                                    PaidOutTemplate,PaidInTemplate,SafeDropTemplate,
                                    LottoPayoutTemplate,CustomerLabelTemplate,GiftReceiptTemplate,GiftAidTemplate,
                                    CreatedBy, LastChangedBy, CreatedOn, LastChangedOn)
                                    values ( @ReceiptTemplate, @LayawayTemplate,
                                    @RentIssueTemplate,@RentReturnTemplate,@RepairInTemplate,@RepairDeliverTemplate,
                                    @WorkOrderTemplate,@SuspendReceiptTemplate,@CloseoutTemplate,@NoSaleTemplate,
                                    @PaidOutTemplate,@PaidInTemplate,@SafeDropTemplate,
                                    @LottoPayoutTemplate,@CustomerLabelTemplate,@GiftReceiptTemplate,@GiftAidTemplate,
                                    @CreatedBy, @LastChangedBy, @CreatedOn, @LastChangedOn)
                                    ";
                              


            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawayTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentIssueTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentReturnTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairInTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairDeliverTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkOrderTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SuspendReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@NoSaleTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PaidOutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PaidInTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SafeDropTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LottoPayoutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerLabelTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftAidTemplate", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ReceiptTemplate"].Value = intActiveTemplate1;
                objSQlComm.Parameters["@LayawayTemplate"].Value = intActiveTemplate2;
                objSQlComm.Parameters["@RentIssueTemplate"].Value = intActiveTemplate3;
                objSQlComm.Parameters["@RentReturnTemplate"].Value = intActiveTemplate4;
                objSQlComm.Parameters["@RepairInTemplate"].Value = intActiveTemplate5;
                objSQlComm.Parameters["@RepairDeliverTemplate"].Value = intActiveTemplate6;
                objSQlComm.Parameters["@WorkOrderTemplate"].Value = intActiveTemplate7;
                objSQlComm.Parameters["@SuspendReceiptTemplate"].Value = intActiveTemplate8;
                objSQlComm.Parameters["@CloseoutTemplate"].Value = intActiveTemplate9;
                objSQlComm.Parameters["@NoSaleTemplate"].Value = intActiveTemplate10;
                objSQlComm.Parameters["@PaidOutTemplate"].Value = intActiveTemplate11;
                objSQlComm.Parameters["@PaidInTemplate"].Value = intActiveTemplate12;
                objSQlComm.Parameters["@SafeDropTemplate"].Value = intActiveTemplate13;
                objSQlComm.Parameters["@LottoPayoutTemplate"].Value = intActiveTemplate14;
                objSQlComm.Parameters["@CustomerLabelTemplate"].Value = intActiveTemplate15;

                objSQlComm.Parameters["@GiftReceiptTemplate"].Value = intActiveTemplate16;
                objSQlComm.Parameters["@GiftAidTemplate"].Value = intActiveTemplate17;


                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public string UpdateActiveData()
        {
            string strSQLComm = @"  update ReceiptTemplateActive set ReceiptTemplate=@ReceiptTemplate, LayawayTemplate=@LayawayTemplate,
                                    RentIssueTemplate=@RentIssueTemplate,RentReturnTemplate=@RentReturnTemplate,
                                    RepairInTemplate=@RepairInTemplate,RepairDeliverTemplate=@RepairDeliverTemplate,
                                    WorkOrderTemplate=@WorkOrderTemplate,SuspendReceiptTemplate=@SuspendReceiptTemplate,
                                    CloseoutTemplate=@CloseoutTemplate,NoSaleTemplate=@NoSaleTemplate,
                                    PaidOutTemplate=@PaidOutTemplate,PaidInTemplate=@PaidInTemplate,
                                    SafeDropTemplate=@SafeDropTemplate,LottoPayoutTemplate=@LottoPayoutTemplate,
                                    CustomerLabelTemplate=@CustomerLabelTemplate,GiftReceiptTemplate=@GiftReceiptTemplate,
                                    GiftAidTemplate=@GiftAidTemplate,
                                    LastChangedBy=@LastChangedBy, LastChangedOn=LastChangedOn ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LayawayTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentIssueTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RentReturnTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairInTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RepairDeliverTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkOrderTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SuspendReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CloseoutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@NoSaleTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PaidOutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PaidInTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SafeDropTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LottoPayoutTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerLabelTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftReceiptTemplate", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GiftAidTemplate", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ReceiptTemplate"].Value = intActiveTemplate1;
                objSQlComm.Parameters["@LayawayTemplate"].Value = intActiveTemplate2;
                objSQlComm.Parameters["@RentIssueTemplate"].Value = intActiveTemplate3;
                objSQlComm.Parameters["@RentReturnTemplate"].Value = intActiveTemplate4;
                objSQlComm.Parameters["@RepairInTemplate"].Value = intActiveTemplate5;
                objSQlComm.Parameters["@RepairDeliverTemplate"].Value = intActiveTemplate6;
                objSQlComm.Parameters["@WorkOrderTemplate"].Value = intActiveTemplate7;
                objSQlComm.Parameters["@SuspendReceiptTemplate"].Value = intActiveTemplate8;
                objSQlComm.Parameters["@CloseoutTemplate"].Value = intActiveTemplate9;
                objSQlComm.Parameters["@NoSaleTemplate"].Value = intActiveTemplate10;
                objSQlComm.Parameters["@PaidOutTemplate"].Value = intActiveTemplate11;
                objSQlComm.Parameters["@PaidInTemplate"].Value = intActiveTemplate12;
                objSQlComm.Parameters["@SafeDropTemplate"].Value = intActiveTemplate13;
                objSQlComm.Parameters["@LottoPayoutTemplate"].Value = intActiveTemplate14;
                objSQlComm.Parameters["@CustomerLabelTemplate"].Value = intActiveTemplate15;
                objSQlComm.Parameters["@GiftReceiptTemplate"].Value = intActiveTemplate16;
                objSQlComm.Parameters["@GiftAidTemplate"].Value = intActiveTemplate17;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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
