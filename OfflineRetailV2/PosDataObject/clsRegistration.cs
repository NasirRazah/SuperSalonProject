/*
 purpose : Data for Registration
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
	/// <summary>
	/// Summary description for clsRegistration.
	/// </summary>
	public class Registration
	{
		#region definining private variables
		private SqlConnection sqlConn;
		private int		intID;
		private int		intNoUsers;
		private DateTime  dtFirstLoggedOn;
		private string  strSerialNo;
		private string  strActivationKey;
		private string  strCompanyName;
        private string strEMail;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strState;
        private string strZip;
		private DateTime dtLastChangedOn;

        private string strPOSAccess;
        private string strScaleAccess;
        private string strOrderingAccess;

        private string strLabelDesigner;

		#endregion

		public Registration()
		{
			
		}

		#region definining public properties

		public SqlConnection Connection
		{
			get {return sqlConn;}
			set {sqlConn = value;}
		}

		public int ID
		{
			get {return intID;}
			set {intID = value;}
		}

		public int NoUsers
		{
			get {return intNoUsers;}
			set {intNoUsers = value;}
		}

		public DateTime FirstLoggedOn
		{
			get {return dtFirstLoggedOn;}
			set {dtFirstLoggedOn = value;}
		}
		
		public string SerialNo
		{
			get {return strSerialNo;}
			set {strSerialNo = value;}
		}

        public string CompanyName
        {
            get { return strCompanyName; }
            set { strCompanyName = value; }
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

        public string Zip
        {
            get { return strZip; }
            set { strZip = value; }
        }

        public string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public string POSAccess
        {
            get { return strPOSAccess; }
            set { strPOSAccess = value; }
        }

        public string ScaleAccess
        {
            get { return strScaleAccess; }
            set { strScaleAccess = value; }
        }

        public string OrderingAccess
        {
            get { return strOrderingAccess; }
            set { strOrderingAccess = value; }
        }

		public string ActivationKey
		{
			get {return strActivationKey;}
			set {strActivationKey = value;}
		}

		public DateTime LastChangedOn
		{
			get {return dtLastChangedOn;}
			set {dtLastChangedOn = value;}
		}

        public string LabelDesigner
        {
            get { return strLabelDesigner; }
            set { strLabelDesigner = value; }
        }

		#endregion

		#region Fetch data 

		public DataTable FetchData()
		{
			DataTable dtbl = new DataTable();

			string strSQLComm = " select * from registration ";

			SqlCommand objSQlComm = new SqlCommand(strSQLComm,Connection);
			SqlDataReader objSQLReader = null;
			try
			{
				if (Connection.State == System.Data.ConnectionState.Closed){Connection.Open();}
				objSQLReader = objSQlComm.ExecuteReader();

				dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
				dtbl.Columns.Add("COMPANYNAME", System.Type.GetType("System.String"));
				dtbl.Columns.Add("SERIALNO", System.Type.GetType("System.String"));
				dtbl.Columns.Add("ACTIVATIONKEY", System.Type.GetType("System.String"));
				dtbl.Columns.Add("ADDRESS1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ADDRESS2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CITY", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtbl.Columns.Add("STATE", System.Type.GetType("System.String"));

				dtbl.Columns.Add("EMAIL", System.Type.GetType("System.String"));
				dtbl.Columns.Add("NOUSERS", System.Type.GetType("System.String"));
				dtbl.Columns.Add("FIRSTLOGGEDON", System.Type.GetType("System.String"));
				dtbl.Columns.Add("LASTCHANGEDON", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSACCESS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SCALEACCESS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ORDERINGACCESS", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LABELDESIGNER", System.Type.GetType("System.String"));
				while (objSQLReader.Read())
				{
					dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["COMPANYNAME"].ToString(),
												   objSQLReader["SERIALNO"].ToString(),
												   objSQLReader["ACTIVATIONKEY"].ToString(),
												   objSQLReader["ADDRESS1"].ToString(),
                                                   objSQLReader["ADDRESS2"].ToString(),
                                                   objSQLReader["CITY"].ToString(), 
                                                   objSQLReader["ZIP"].ToString(), 
                                                   objSQLReader["STATE"].ToString(), 
												   objSQLReader["EMAIL"].ToString(),
												   objSQLReader["NOUSERS"].ToString(),
												   objSQLReader["FIRSTLOGGEDON"].ToString(),
												   objSQLReader["LASTCHANGEDON"].ToString(),
                                                   objSQLReader["POSACCESS"].ToString(),
                                                   objSQLReader["SCALEACCESS"].ToString(),
                                                   objSQLReader["ORDERINGACCESS"].ToString(),
                                                   objSQLReader["LABELDESIGNER"].ToString() });
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

		#region Insert Data

		public string InsertData()
		{
            string strSQLComm = " insert into registration (SerialNo, CompanyName, ActivationKey,Address1,Address2,City, " 
				              + " Zip,State,EMail, NoUsers, FirstLoggedOn, LastChangedOn,POSAccess,ScaleAccess,OrderingAccess,LabelDesigner) "
                              + " values (@SerialNo, @CompanyName, @ActivationKey,@Address1,@Address2,@City,"
                              + " @Zip,@State,@EMail, @NoUsers, @FirstLoggedOn, @LastChangedOn,@POSAccess,@ScaleAccess,@OrderingAccess,@LabelDesigner)  "
				              + " select @@IDENTITY as ID ";

			SqlCommand objSQlComm	= null;
			SqlTransaction objSQLTran	= null;
			SqlDataReader objsqlReader = null;
			try
			{
				objSQlComm = new SqlCommand(strSQLComm,Connection);
				if (Connection.State == System.Data.ConnectionState.Closed){Connection.Open();}

				objSQLTran = Connection.BeginTransaction();
				objSQlComm.Transaction = objSQLTran;

				objSQlComm.Parameters.Add(new SqlParameter("@SerialNo",System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar));
				objSQlComm.Parameters.Add(new SqlParameter("@ActivationKey",System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
				objSQlComm.Parameters.Add(new SqlParameter("@NoUsers",System.Data.SqlDbType.Int));
				objSQlComm.Parameters.Add(new SqlParameter("@FirstLoggedOn",System.Data.SqlDbType.DateTime));
				objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn",System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@POSAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderingAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelDesigner", System.Data.SqlDbType.Char));

				objSQlComm.Parameters["@SerialNo"].Value		 = strSerialNo;
				objSQlComm.Parameters["@CompanyName"].Value		 = strCompanyName;
				objSQlComm.Parameters["@ActivationKey"].Value	 = strActivationKey;
                objSQlComm.Parameters["@Address1"].Value         = strAddress1;
                objSQlComm.Parameters["@Address2"].Value         = strAddress2;
                objSQlComm.Parameters["@City"].Value             = strCity;
                objSQlComm.Parameters["@Zip"].Value              = strZip;
                objSQlComm.Parameters["@State"].Value            = strState;
                objSQlComm.Parameters["@EMail"].Value            = strEMail;
				objSQlComm.Parameters["@NoUsers"].Value			 = intNoUsers;
				objSQlComm.Parameters["@FirstLoggedOn"].Value	 = dtFirstLoggedOn;
				objSQlComm.Parameters["@LastChangedOn"].Value	 = dtLastChangedOn;
                objSQlComm.Parameters["@POSAccess"].Value        = strPOSAccess;
                objSQlComm.Parameters["@ScaleAccess"].Value      = strScaleAccess;
                objSQlComm.Parameters["@OrderingAccess"].Value = strOrderingAccess;
                objSQlComm.Parameters["@LabelDesigner"].Value = strLabelDesigner;

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
            string strSQLComm = " update registration set SerialNo=@SerialNo,CompanyName=@CompanyName,ActivationKey=@ActivationKey, "
                              + " Address1=@Address1,Address2=@Address2,City=@City,Zip=@Zip,State=@State,EMail=@EMail,NoUsers=@NoUsers, "
                              + " FirstLoggedOn=@FirstLoggedOn, LastChangedOn=@LastChangedOn,POSAccess=@POSAccess,"
                              + " ScaleAccess=@ScaleAccess,OrderingAccess=@OrderingAccess,LabelDesigner=@LabelDesigner where ID = @ID";

			SqlCommand objSQlComm	= null;
			SqlTransaction objSQLTran	= null;
			SqlDataReader objsqlReader = null;
			try
			{
				objSQlComm = new SqlCommand(strSQLComm,Connection);
				if (Connection.State == System.Data.ConnectionState.Closed){Connection.Open();}

				objSQLTran = Connection.BeginTransaction();
				objSQlComm.Transaction = objSQLTran;

				objSQlComm.Parameters.Add(new SqlParameter("@ID",System.Data.SqlDbType.Int));
				objSQlComm.Parameters.Add(new SqlParameter("@SerialNo",System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar));
				objSQlComm.Parameters.Add(new SqlParameter("@ActivationKey",System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
				objSQlComm.Parameters.Add(new SqlParameter("@NoUsers",System.Data.SqlDbType.Int));
				objSQlComm.Parameters.Add(new SqlParameter("@FirstLoggedOn",System.Data.SqlDbType.DateTime));
				objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn",System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@POSAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderingAccess", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LabelDesigner", System.Data.SqlDbType.Char));

				objSQlComm.Parameters["@ID"].Value			 	 = intID;
				objSQlComm.Parameters["@SerialNo"].Value		 = strSerialNo;
				objSQlComm.Parameters["@CompanyName"].Value		 = strCompanyName;
				objSQlComm.Parameters["@ActivationKey"].Value	 = strActivationKey;
                objSQlComm.Parameters["@Address1"].Value         = strAddress1;
                objSQlComm.Parameters["@Address2"].Value         = strAddress2;
                objSQlComm.Parameters["@City"].Value             = strCity;
                objSQlComm.Parameters["@Zip"].Value              = strZip;
                objSQlComm.Parameters["@State"].Value            = strState;
                objSQlComm.Parameters["@EMail"].Value            = strEMail;
				objSQlComm.Parameters["@NoUsers"].Value			 = intNoUsers;
				objSQlComm.Parameters["@FirstLoggedOn"].Value	 = dtFirstLoggedOn;
				objSQlComm.Parameters["@LastChangedOn"].Value	 = dtLastChangedOn;
                objSQlComm.Parameters["@POSAccess"].Value        = strPOSAccess;
                objSQlComm.Parameters["@ScaleAccess"].Value      = strScaleAccess;
                objSQlComm.Parameters["@OrderingAccess"].Value   = strOrderingAccess;
                objSQlComm.Parameters["@LabelDesigner"].Value = strLabelDesigner;
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

        #region Insert License Agreement

        public string InsertLicenseAgreement()
        {
            string strSQLComm = "insert into setup (AgreeLicense,AgreeLicenseFirstTime) values('0',@dt)";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                objSQlComm.Parameters.Add(new SqlParameter("@dt", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@dt"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
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

        #region Update License Agreement

        public string UpdateLicenseAgreement(string flg)
        {
            string strSQLComm = "";
            if (flg == "F")
                strSQLComm = " update Setup set AgreeLicense='0',AgreeLicenseFirstTime = @dt  ";
            if (flg == "S")
                strSQLComm = " update Setup set AgreeLicense='2',AgreeLicenseSecondTime = 'Y'  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                if (flg == "F")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@dt", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@dt"].Value = DateTime.Now;
                }

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
