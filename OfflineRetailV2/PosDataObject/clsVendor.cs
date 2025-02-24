/*
 purpose : Data for Vendor
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Vendor
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private int intID;
        private int intNewID;
        private int intLoginUserID;


        private string strVendorID;
        private string strAccountNo;
        private string strName;
        private string strContact;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strState;
        private string strCountry;
        private string strZip;
        private string strFax;
        private string strPhone;
        private string strEMail;
        private string strNotes;

        private double dblMinimumOrderAmount;

        #endregion

        #region definining public variables
        public string DataObjectCulture_All
        {
            get { return strDataObjectCulture_All; }
            set { strDataObjectCulture_All = value; }
        }

        public string DataObjectCulture_None
        {
            get { return strDataObjectCulture_None; }
            set { strDataObjectCulture_None = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public double MinimumOrderAmount
        {
            get { return dblMinimumOrderAmount; }
            set { dblMinimumOrderAmount = value; }
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

        public string VendorID
        {
            get { return strVendorID; }
            set { strVendorID = value; }
        }

        public string AccountNo
        {
            get { return strAccountNo; }
            set { strAccountNo = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public string Contact
        {
            get { return strContact; }
            set { strContact = value; }
        }

        public string Address1
        {
            get { return strAddress1; }
            set { strAddress1 = value; }
        }

        public string Address2
        {
            get { return strAddress2; }
            set { strAddress2 = value; }
        }

        public string City
        {
            get { return strCity; }
            set { strCity = value; }
        }

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        public string Country
        {
            get { return strCountry; }
            set { strCountry = value; }
        }

        public string Zip
        {
            get { return strZip; }
            set { strZip = value; }
        }

        public string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }

        public string Phone
        {
            get { return strPhone; }
            set { strPhone = value; }
        }

        public string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public string Notes
        {
            get { return strNotes; }
            set { strNotes = value; }
        }

        #endregion

        #region Insert Data

        public string InsertData()
        {
            string strSQLComm = " insert into Vendor( VendorID,AccountNo,Name,Contact,Address1,Address2,City,State,Country,Zip, "
                              + " Fax,Phone,EMail,Notes,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,MinimumOrderAmount) "
                              + " values ( @VendorID,@AccountNo,@Name,@Contact,@Address1,@Address2,@City,@State,@Country, "
                              + " @Zip,@Fax,@Phone,@EMail,@Notes,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@MinimumOrderAmount) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Contact", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@MinimumOrderAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@VendorID"].Value = strVendorID;
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@Name"].Value = strName;
                objSQlComm.Parameters["@Contact"].Value = strContact;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@Phone"].Value = strPhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Notes"].Value = strNotes;
                
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@MinimumOrderAmount"].Value = dblMinimumOrderAmount;

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
            string strSQLComm = " update Vendor set VendorID=@VendorID,AccountNo=@AccountNo,Name=@Name,Contact=@Contact,Address1=@Address1, "
                              + " Address2=@Address2,City=@City,State=@State,Country=@Country,Zip=@Zip,Fax=@Fax,Phone=@Phone, "
                              + " EMail=@EMail,Notes=@Notes,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,"
                              + " MinimumOrderAmount=@MinimumOrderAmount where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Contact", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@MinimumOrderAmount", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@VendorID"].Value = strVendorID;
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@Name"].Value = strName;
                objSQlComm.Parameters["@Contact"].Value = strContact;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@Phone"].Value = strPhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Notes"].Value = strNotes;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@MinimumOrderAmount"].Value = dblMinimumOrderAmount;
                
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

            string strSQLComm = " select ID,VendorID,Name,Contact,Address1,City, Phone from Vendor ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["VendorID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Contact"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(), 
                                                   objSQLReader["Phone"].ToString()});
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

            string strSQLComm = "select * from Vendor where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AccountNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumOrderAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["VendorID"].ToString(),
                                                objSQLReader["Name"].ToString(),
												objSQLReader["Contact"].ToString(),
                                                objSQLReader["AccountNo"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Country"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["Fax"].ToString(),
                                                objSQLReader["Phone"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["Notes"].ToString(),
                                                objSQLReader["MinimumOrderAmount"].ToString()});
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

        public string GetVendorName(int intVID)
        {
            string strResult = "";

            string strSQLComm = "select NAME from Vendor where ID = @VendorID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@VendorID"].Value = intVID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strResult = objSQLReader["NAME"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strResult;
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

        #region Delete Data

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Vendor Where ID = @ID";

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

        #region Delete Part Number

        public string DeletePartNumber(int DeleteID)
        {
            string strSQLComm = " Delete from VendPart Where VendorID = @ID";

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

        #region Duplicate Checking

        public int DuplicateCount(string VendorID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Vendor where VENDORID=@VENDORID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@VENDORID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@VENDORID"].Value = VendorID;

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
        
        public int IsExistProduct(int VendorID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from Product where PRIMARYVENDORID in ( select ID from VENDOR where  ID = @VID) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@VID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VID"].Value = VendorID;

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

        #region Fetch Lookup data

        public DataTable FetchLookupData(string pType)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, VendorID,Name,Contact from Vendor ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["VendorID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Contact"].ToString() }); //
                }
                if (pType == "B")
                {
                    dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All, "" });
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

        #region Show Vendor Part Number

        public DataTable ShowPartNumber(int pVendor)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select VP.*, P.Description as ProductName from VendPart VP left outer join Product P on VP.ProductID = P.ID "
                              + " where VP.VendorID = @ID order by P.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pVendor;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Primary", System.Type.GetType("System.String"));

                double dblDetCost = 0;
                string strpmry = "";
                while (objSQLReader.Read())
                {
                    dblDetCost = 0;
                    strpmry = "";
                    if (objSQLReader["IsPrimary"].ToString() == "Y")
                    {
                        strpmry = "Yes";
                    }
                    else
                    {
                        strpmry = "No";
                    }

                    if (objSQLReader["Price"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["Price"].ToString());
                    }
                    
                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["ProductName"].ToString(),
                                                objSQLReader["PartNumber"].ToString(),
                                                dblDetCost,
                                                strpmry });
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

        #region Fetch Data For General Report

        public DataTable FetchDataForGeneralReport(string refVID, string refVName, string refContact, string refZip, string refOrderBy)
        {
            DataTable dtbl = new DataTable();
            string strSQLVID = "";
            string strSQLVName = "";
            string strSQLContact = "";
            string strSQLZip = "";
            string strSQLOrderBy = "";

            if (refVID != "")
            {
                strSQLVID = " and VendorID like '%" + refVID + "%' ";
            }

            if (refVName != "")
            {
                strSQLVName = " and Name like '%" + refVName + "%' ";
            }

            if (refContact != "")
            {
                strSQLContact = " and Contact like '%" + refContact + "%' ";
            }

            if (refZip != "")
            {
                strSQLZip = " and Zip like '%" + refZip + "%' ";
            }

            if (refOrderBy == "1")
            {
                strSQLOrderBy = " Order by VendorID ";
            }
            else if (refOrderBy == "2")
            {
                strSQLOrderBy = " Order by Name ";
            }
            else if (refOrderBy == "3")
            {
                strSQLOrderBy = " Order by Contact ";
            }
            else if (refOrderBy == "4")
            {
                strSQLOrderBy = " Order by Zip ";
            }
            else
            {
                strSQLOrderBy = " Order by VendorID ";
            }
            
            string strSQLComm = " select ID,VendorID,Name,Contact,Address1, Address2, City, State, Zip, Phone from Vendor Where (1 = 1) "
                                + strSQLVID + strSQLVName + strSQLContact + strSQLZip + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["VendorID"].ToString(),
												   objSQLReader["Name"].ToString(),
                                                   objSQLReader["Contact"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["Address2"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["Phone"].ToString()});
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

        public double GetVendorMinimumOrderAmount(int VendorID)
        {
            double val = 0;

            string strSQLComm = " select MinimumOrderAmount from vendor where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = VendorID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    val = Functions.fnDouble(objsqlReader["MinimumOrderAmount"]);
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val;
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

                return val;
            }
        }
    }
}
