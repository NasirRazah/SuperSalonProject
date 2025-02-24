/*
 purpose : Data for Tender Type
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class TenderTypes
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strTypeName;
        private string strTypeEnabled;
        private string strDisplayAs;
        private string strIsOpenCashDrawer;

        private int intPaymentOrder;

        private int intLinkGL;

        #endregion

        #region definining public variables

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public int PaymentOrder
        {
            get { return intPaymentOrder; }
            set { intPaymentOrder = value; }
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

        public int LinkGL
        {
            get { return intLinkGL; }
            set { intLinkGL = value; }
        }

        public string TypeName
        {
            get { return strTypeName; }
            set { strTypeName = value; }
        }

        public string TypeEnabled
        {
            get { return strTypeEnabled; }
            set { strTypeEnabled = value; }
        }

        public string DisplayAs
        {
            get { return strDisplayAs; }
            set { strDisplayAs = value; }
        }

        public string IsOpenCashDrawer
        {
            get { return strIsOpenCashDrawer; }
            set { strIsOpenCashDrawer = value; }
        }

        #endregion

        #region Insert Data

        public string InsertData()
        {
            string strSQLComm = " insert into TenderTypes( Name,Enabled,DisplayAs,IsOpenCashdrawer,PaymentOrder,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,LinkGL) "
                              + " values ( @Name,@Enabled,@DisplayAs,@IsOpenCashdrawer,@PaymentOrder,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@LinkGL) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Enabled", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayAs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsOpenCashdrawer", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@PaymentOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Name"].Value = strTypeName;
                objSQlComm.Parameters["@Enabled"].Value = strTypeEnabled;
                objSQlComm.Parameters["@DisplayAs"].Value = strDisplayAs;
                objSQlComm.Parameters["@IsOpenCashdrawer"].Value = strIsOpenCashDrawer;
                objSQlComm.Parameters["@PaymentOrder"].Value = intPaymentOrder;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LinkGL"].Value = intLinkGL;

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
            string strSQLComm = " update TenderTypes set Name=@Name,Enabled=@Enabled,DisplayAs=@DisplayAs,IsOpenCashdrawer=@IsOpenCashdrawer, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, LinkGL=@LinkGL where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayAs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsOpenCashdrawer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Enabled", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Name"].Value = strTypeName;
                objSQlComm.Parameters["@DisplayAs"].Value = strDisplayAs;
                objSQlComm.Parameters["@IsOpenCashdrawer"].Value = strIsOpenCashDrawer;

                objSQlComm.Parameters["@Enabled"].Value = strTypeEnabled;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LinkGL"].Value = intLinkGL;
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

        public string UpdateBlankData()
        {
            string strSQLComm = " update TenderTypes set Name=@Name,Enabled=@Enabled,DisplayAs=@DisplayAs,IsOpenCashdrawer=@IsOpenCashdrawer, "
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, LinkGL=@LinkGL where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DisplayAs", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@IsOpenCashdrawer", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@Enabled", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@Name"].Value = strDisplayAs;
                objSQlComm.Parameters["@DisplayAs"].Value = strDisplayAs;
                objSQlComm.Parameters["@IsOpenCashdrawer"].Value = strIsOpenCashDrawer;

                objSQlComm.Parameters["@Enabled"].Value = strTypeEnabled;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LinkGL"].Value = intLinkGL;

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

            string strSQLComm = " select ID,Name,Enabled,DisplayAs,IsOpenCashdrawer,PaymentOrder from TenderTypes order by PaymentOrder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Enabled", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentOrder", System.Type.GetType("System.String"));
                string strEnabled = "";
                string strcashdrawerflag = "";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["Enabled"].ToString() == "Y") strEnabled = "Yes"; else strEnabled = "No";
                    if (objSQLReader["IsOpenCashdrawer"].ToString() == "Y") strcashdrawerflag = "Yes"; else strcashdrawerflag = "No";

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
												   strEnabled,objSQLReader["DisplayAs"].ToString(),
                                                   strcashdrawerflag,
                                                   objSQLReader["PaymentOrder"].ToString() });
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

        public DataTable FetchPOSData(bool flg)
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            if (!flg) sql = " and Name not in ('Credit Card - Voice Auth', 'Credit Card (STAND-IN)', 'Credit Card - Voice Auth (STAND-IN)','Mercury Gift Card','Precidia Gift Card', 'Datacap Gift Card', 'POSLink Gift Card') ";
            
            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        public DataTable FetchPOSDataForMercury()
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            sql = " and Name not in ('Precidia Gift Card','Datacap Gift Card','POSLink Gift Card') ";

            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        public DataTable FetchPOSDataForPrecidia(bool flg)
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            if (!flg) sql = " and Name not in ('Food Stamps', 'Debit Card', 'Credit Card - Voice Auth (STAND-IN)','Mercury Gift Card','Datacap Gift Card','POSLink Gift Card') ";
            if (flg) sql = " and Name not in ('Credit Card - Voice Auth (STAND-IN)','Mercury Gift Card','Datacap Gift Card','POSLink Gift Card') ";

            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        public DataTable FetchPOSDataForDatacap()
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            sql = " and Name not in ('Precidia Gift Card','Mercury Gift Card','POSLink Gift Card') ";

            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        public DataTable FetchPOSDataForDatacapEMV()
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            sql = " and Name not in ('Precidia Gift Card','Mercury Gift Card', 'Datacap Gift Card', 'POSLink Gift Card', 'Credit Card - Voice Auth (STAND-IN)', 'Credit Card (STAND-IN)', 'EBT Cash', 'EBT Voucher', 'Food Stamps') ";

            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        public DataTable FetchPOSDataForPOSLink()
        {
            DataTable dtbl = new DataTable();
            string sql = "";

            sql = " and Name not in ('Precidia Gift Card','Mercury Gift Card', 'Datacap Gift Card', 'Credit Card - Voice Auth (STAND-IN)', 'Credit Card (STAND-IN)', 'EBT Voucher') ";

            string strSQLComm = " select ID,Name,DisplayAs,IsOpenCashdrawer from TenderTypes where Enabled = 'Y' " + sql + " order by paymentorder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["DisplayAs"].ToString(),
                                                   objSQLReader["IsOpenCashdrawer"].ToString()});
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

        #region Fetch Cash ID for Fast Cash Transaction

        public int FetchCashID()
        {
            int intCount = 0;

            string strSQLComm = " select ID from TenderTypes where Name= 'Cash' and Enabled = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["ID"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
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
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }

        #endregion

        public int FetchCCID()
        {
            int intCount = 0;
            string strSQLComm = " select ID from TenderTypes where Name= 'Credit Card' and Enabled = 'Y' ";

            SqlCommand objSQlComm = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                SqlDataReader objSQLReader = null;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                sqlConn.Close();
                objSQlComm.Dispose();
                strErrMsg = SQLDBException.Message;
                return intCount;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
            }
        }

        #region Show Record based on ID

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from TenderTypes where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Enabled", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayAs", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsOpenCashdrawer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsBlank", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkGL", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["Name"].ToString(),
												objSQLReader["Enabled"].ToString(),
                                                objSQLReader["DisplayAs"].ToString(),
                                                objSQLReader["IsOpenCashdrawer"].ToString(),
                                                objSQLReader["IsBlank"].ToString(),
                                                objSQLReader["LinkGL"].ToString()});
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

        public void GetCashRecord(ref int refID, ref string refName, ref string refDisplay)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select isnull(id,0) as TID, isnull(name,'') as TName, isnull(displayas,'') as TDisplay "
                              + " from TenderTypes where Name = 'Cash' and enabled = 'Y' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    refID = Functions.fnInt32(objSQLReader["TID"].ToString());
                    refName = objSQLReader["TName"].ToString();
                    refDisplay = objSQLReader["TDisplay"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

            }
        }

        #endregion

        #region Duplicate Checking

        public int DuplicateCount(string pTenderName)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from TenderTypes where Name=@Name and Name <> 'Blank'  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Name"].Value = pTenderName;

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

        #endregion

        #region Fetch Lookup Data

        public DataTable FetchLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select Name from TenderTypes ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["Name"].ToString()});
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
            string strSQLComm = " delete from TenderTypes where ID = @ID";

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

        public string GetCashdrawerOpenFlag(int pID)
        {
            string val = "";

            string strSQLComm = " select isnull(IsOpenCashdrawer,'N') as flg from TenderTypes where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = objSQLReader["flg"].ToString();
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

                return "N";
            }
        }

        public int UpdatePaymentOrder(int pID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_RearrangeTenderType_Order";

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
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@ChangedOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ChangedOrder"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ChangedOrder"].Value = pOrder;

                objSQlComm.Parameters.Add(new SqlParameter("@Position", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Position"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Position"].Value = pType;

                objSQlComm.ExecuteNonQuery();

                intReturn = 0;

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intReturn;
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

        public int MaxPaymentOrder()
        {
            int intCount = 0;

            string strSQLComm = " select max(PaymentOrder) as rcnt from TenderTypes  ";

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

        public int FetchCheckID()
        {
            int intCount = 0;

            string strSQLComm = " select ID from TenderTypes where Name= 'Check' and Enabled = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["ID"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
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
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }
    }
}
