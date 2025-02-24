/* NOT USED */


using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;


namespace PosDataObject
{
	/// <summary>
	/// Summary description for clsLoggedUsersNo.
	/// </summary>
	public class LoggedUsersNo
	{
		
		#region definining private variables
		
		private SqlConnection sqlConn;

		#endregion

		public LoggedUsersNo()
		{
			
		}


		#region definining public properties

		//General
		public SqlConnection Connection
		{
			get {return sqlConn;}
			set {sqlConn = value;}
		}
		#endregion


		
		// Execute stored procedure for Logged on users //
		/*public string UpdateCustomerDetails(string CustomerCode, string Address1, string Address2,
			string Address3, string City, string PinCode, string Country, string OfficeTel,
			string ResidenceTel, string MobileTel, string EmailID, string MaritalStatus,DateTime DOB, 
			string Qualification, string Occupation, string Income, ref int ReturnValue)*/
		public string ExecStoredProc()
		{
			string strSQLComm = "DocUserCount";
				
			SqlCommand objSQlComm	= null;
			
			objSQlComm = new SqlCommand();
			if (Connection.State == System.Data.ConnectionState.Closed){Connection.Open();}
			objSQlComm.Connection = this.Connection;
			
			objSQlComm.CommandText = strSQLComm;
			objSQlComm.CommandType = CommandType.StoredProcedure;

			try
			{	
				objSQlComm.ExecuteNonQuery(); 
				
                objSQlComm.Dispose();
				sqlConn.Close();

				return "";
			}

			catch (SqlException SQLDBException)
			{
                objSQlComm.Dispose();
				sqlConn.Close();
				string strErrMsg = "";
				strErrMsg = SQLDBException.Message;
				return strErrMsg;				
			}
		}
	}
}
