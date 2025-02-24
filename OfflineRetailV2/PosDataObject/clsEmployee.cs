/*
 purpose : Data for Employee 
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace PosDataObject
{
    public class Employee
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private string strDataObjectCulture_ADMIN;
        private string strDataObjectCulture_Administrator;
        private int intID;
        private int intNewID;
        private int intLoginUserID;


        private string strEmployeeID;
        private string strEmpPassword;
        private int intProfileID;
        private int intProfileCRC;
        private int intEmpShift;

        private string strLastName;
        private string strFirstName;
        private string strEmergencyContact;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strState;
        private string strZip;
        private string strPhone1;
        private string strPhone2;
        private string strEmergencyPhone;
        private string strEMail;
        private string strSSNumber;
        private string strLocked;

        private double dblEmpRate;
        private int intDecimalPlace;

        private string strAdminPassword;
        private DateTime dtAdminPasswordModifiedOn;
        private string strPhotoFilePath;

        private string strIncludeInAppointmentBook;

        private string strThisStoreCode;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string strBookingExpFlag;

        private byte[] bytEmployeePhoto; //change for wpf

        private string strForcedPasscode;

        #endregion

        #region definining public variables

        public byte[] EmployeePhoto
        {
            get { return bytEmployeePhoto; }
            set { bytEmployeePhoto = value; }
        }

        public string ForcedPasscode
        {
            get { return strForcedPasscode; }
            set { strForcedPasscode = value; }
        }

        public string BookingExpFlag
        {
            get { return strBookingExpFlag; }
            set { strBookingExpFlag = value; }
        }

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

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public string AdminPassword
        {
            get { return strAdminPassword; }
            set { strAdminPassword = value; }
        }

        public DateTime AdminPasswordModifiedOn
        {
            get { return dtAdminPasswordModifiedOn; }
            set { dtAdminPasswordModifiedOn = value; }
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

        public string Locked
        {
            get { return strLocked; }
            set { strLocked = value; }
        }

        public string EmployeeID
        {
            get { return strEmployeeID; }
            set { strEmployeeID = value; }
        }

        public string EmpPassword
        {
            get { return strEmpPassword; }
            set { strEmpPassword = value; }
        }

        public int ProfileID
        {
            get { return intProfileID; }
            set { intProfileID = value; }
        }

        public int ProfileCRC
        {
            get { return intProfileCRC; }
            set { intProfileCRC = value; }
        }

        public string LastName
        {
            get { return strLastName; }
            set { strLastName = value; }
        }

        public string FirstName
        {
            get { return strFirstName; }
            set { strFirstName = value; }
        }

        public string EmergencyContact
        {
            get { return strEmergencyContact; }
            set { strEmergencyContact = value; }
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

        public string Phone1
        {
            get { return strPhone1; }
            set { strPhone1 = value; }
        }

        public string Phone2
        {
            get { return strPhone2; }
            set { strPhone2 = value; }
        }

        public string EmergencyPhone
        {
            get { return strEmergencyPhone; }
            set { strEmergencyPhone = value; }
        }

        public string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public string SSNumber
        {
            get { return strSSNumber; }
            set { strSSNumber = value; }
        }

        public string PhotoFilePath
        {
            get { return strPhotoFilePath; }
            set { strPhotoFilePath = value; }
        }

        public int EmpShift
        {
            get { return intEmpShift; }
            set { intEmpShift = value; }
        }

        public double EmpRate
        {
            get { return dblEmpRate; }
            set { dblEmpRate = value; }
        }

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public string IncludeInAppointmentBook
        {
            get { return strIncludeInAppointmentBook; }
            set { strIncludeInAppointmentBook = value; }
        }

        private bool bIsChangingAdminPassword;
        private int intAppEmpID;

        public int AppEmpID
        {
            get { return intAppEmpID; }
            set { intAppEmpID = value; }
        }

        public bool IsChangingAdminPassword
        {
            get { return bIsChangingAdminPassword; }
            set { bIsChangingAdminPassword = value; }
        }

        public string ThisStoreCode
        {
            get { return strThisStoreCode; }
            set { strThisStoreCode = value; }
        }

        public string DataObjectCulture_ADMIN
        {
            get { return strDataObjectCulture_ADMIN; }
            set { strDataObjectCulture_ADMIN = value; }
        }

        public string DataObjectCulture_Administrator
        {
            get { return strDataObjectCulture_Administrator; }
            set { strDataObjectCulture_Administrator = value; }
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

        #region Insert Data

        public string InsertData()
        {
            
            string strSQLComm = "";

            strSQLComm = " insert into Employee( EmployeeID,LastName,FirstName,EmergencyContact, "
                            + " Address1,Address2,City,State,Zip,Phone1,Phone2, EmergencyPhone,EMail,"
                            + " SSNumber,Password,ProfileID,ProfileCRC,EmployeePhoto,EmpShift,EmpRate,IncludeInAppointmentBook, "
                            + " AdminPassword,AdminPasswordModifiedOn,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,"
                            + " Locked,IssueStore,OperateStore,ForcedPasscode ) "
                            + " values ( @EmployeeID, @LastName, @FirstName,@EmergencyContact, "
                            + " @Address1,@Address2,@City,@State,@Zip,@Phone1,@Phone2, @EmergencyPhone,@EMail,"
                            + " @SSNumber,@Password,@ProfileID,@ProfileCRC,@EmployeePhoto,@EmpShift,@EmpRate,@IncludeInAppointmentBook, "
                            + " @AdminPassword,@AdminPasswordModifiedOn,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,"
                            + " @Locked,@IssueStore,@OperateStore,@ForcedPasscode ) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmergencyContact", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmergencyPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SSNumber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProfileID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProfileCRC", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EmpShift", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EmpRate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@EmployeePhoto", System.Data.SqlDbType.Image));
                objSQlComm.Parameters.Add(new SqlParameter("@IncludeInAppointmentBook", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@AdminPassword", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AdminPasswordModifiedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@Locked", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@EmployeeID"].Value = strEmployeeID;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@EmergencyContact"].Value = strEmergencyContact;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@EmergencyPhone"].Value = strEmergencyPhone;
                objSQlComm.Parameters["@Phone1"].Value = strPhone1;
                objSQlComm.Parameters["@Phone2"].Value = strPhone2;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@SSNumber"].Value = strSSNumber;
                objSQlComm.Parameters["@Password"].Value = strEmpPassword;
                objSQlComm.Parameters["@ProfileID"].Value = intProfileID;
                objSQlComm.Parameters["@ProfileCRC"].Value = intProfileCRC;
                objSQlComm.Parameters["@EmpShift"].Value = intEmpShift;
                objSQlComm.Parameters["@EmpRate"].Value = dblEmpRate;

                objSQlComm.Parameters["@EmployeePhoto"].Value = bytEmployeePhoto == null ? Convert.DBNull : bytEmployeePhoto;

                objSQlComm.Parameters["@IncludeInAppointmentBook"].Value = strIncludeInAppointmentBook;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@AdminPassword"].Value = strAdminPassword;
                if (dtAdminPasswordModifiedOn == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@AdminPasswordModifiedOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@AdminPasswordModifiedOn"].Value = dtAdminPasswordModifiedOn;
                }

                objSQlComm.Parameters["@Locked"].Value = strLocked;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;

                objSQlComm.Parameters.Add(new SqlParameter("@ForcedPasscode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ForcedPasscode"].Value = strForcedPasscode;

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

            string strSQLComm = "";
            strSQLComm =     " update Employee set EmployeeID=@EmployeeID, "
                           + " LastName=@LastName,FirstName=@FirstName,EmergencyContact=@EmergencyContact, "
                           + " Address1=@Address1,Address2=@Address2,City=@City,State=@State,Zip=@Zip,"
                           + " Phone1=@Phone1,Phone2=@Phone2,EmergencyPhone=@EmergencyPhone,EMail=@EMail,SSNumber=@SSNumber, "
                           + " Password=@Password,ProfileID=@ProfileID,ProfileCRC=@ProfileCRC,EmployeePhoto=@EmployeePhoto, "
                           + " EmpShift=@EmpShift,EmpRate=@EmpRate,IncludeInAppointmentBook=@IncludeInAppointmentBook,"
                           + " AdminPassword=@AdminPassword,AdminPasswordModifiedOn=@AdminPasswordModifiedOn,"
                           + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,Locked=@Locked,"
                           + " OperateStore=@OperateStore,ExpFlag='N', BookingExpFlag=@BookingExpFlag,ForcedPasscode=@ForcedPasscode where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmergencyContact", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Phone2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmergencyPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@SSNumber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ProfileID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProfileCRC", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EmployeePhoto", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@EmpShift", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@EmpRate", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@IncludeInAppointmentBook", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@AdminPassword", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AdminPasswordModifiedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@Locked", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@EmployeeID"].Value = strEmployeeID;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@EmergencyContact"].Value = strEmergencyContact;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@EmergencyPhone"].Value = strEmergencyPhone;
                objSQlComm.Parameters["@Phone1"].Value = strPhone1;
                objSQlComm.Parameters["@Phone2"].Value = strPhone2;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@SSNumber"].Value = strSSNumber;
                objSQlComm.Parameters["@Password"].Value = strEmpPassword;
                objSQlComm.Parameters["@ProfileID"].Value = intProfileID;
                objSQlComm.Parameters["@ProfileCRC"].Value = intProfileCRC;
                objSQlComm.Parameters["@EmpShift"].Value = intEmpShift;
                objSQlComm.Parameters["@EmpRate"].Value = dblEmpRate;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IncludeInAppointmentBook"].Value = strIncludeInAppointmentBook;

                objSQlComm.Parameters["@AdminPassword"].Value = strAdminPassword;
                if (dtAdminPasswordModifiedOn == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@AdminPasswordModifiedOn"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@AdminPasswordModifiedOn"].Value = dtAdminPasswordModifiedOn;
                }

                objSQlComm.Parameters["@EmployeePhoto"].Value = bytEmployeePhoto == null ? Convert.DBNull : bytEmployeePhoto;

                objSQlComm.Parameters["@Locked"].Value = strLocked;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingExpFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@BookingExpFlag"].Value = strBookingExpFlag;

                objSQlComm.Parameters.Add(new SqlParameter("@ForcedPasscode", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@ForcedPasscode"].Value = strForcedPasscode;

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

            string strSQLComm = " select ID,EmployeeID,LastName,FirstName,EmergencyContact,EmergencyPhone, Phone1 from Employee ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyContact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone1", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["EmployeeID"].ToString(),
												   objSQLReader["LastName"].ToString(),
                                                   objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["EmergencyContact"].ToString(),
                                                   objSQLReader["EmergencyPhone"].ToString(), 
                                                   objSQLReader["Phone1"].ToString()});
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

        public DataTable GetAppointmentEmployee(string option)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,EmployeeID,FirstName + ' ' + LastName as EmployeeName from employee "
                              + " where IncludeInAppointmentBook = 'Y' order by FirstName + ' ' + LastName " ;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeName", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["ID"].ToString(),
												 objSQLReader["EmployeeID"].ToString(),
												 objSQLReader["EmployeeName"].ToString()
                                               });
                }

                if (option == "ALL") dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All });

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

        #region Fetch  Lookup Data

        public DataTable FetchLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, EmployeeID,LastName,FirstName from Employee ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["EmployeeID"].ToString(),
												   objSQLReader["LastName"].ToString(),
                                                   objSQLReader["FirstName"].ToString()});
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


        public DataTable FetchLookupData2(string searchtxt)
        {
            string sqlF = "";

            if (searchtxt != "")
            {
                sqlF = " and (( a.EmployeeID like '%" + searchtxt + "%') " + " or ( a.FirstName like '%" + searchtxt + "%') " + " or ( a.LastName like '%" + searchtxt + "%') " +
                    " or ( g.GroupName like '%" + searchtxt + "%')) ";
            }

            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.ID, a.EmployeeID, a.FirstName, a.LastName, a.FirstName + ' ' + a.LastName as EmployeeName, g.GroupName as Profile from Employee a "
                              + " left outer join SecurityGroup g on a.ProfileID=g.ID where (1 = 1) " + sqlF + " order by a.FirstName, a.LastName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Designation", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
												objSQLReader["EmployeeID"].ToString(),
                                                objSQLReader["EmployeeName"].ToString(),
                                                objSQLReader["Profile"].ToString()});
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

        #region Fetch Modified Lookup Data

        public DataTable FetchModifiedLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, EmployeeID,LastName,FirstName from Employee ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["EmployeeID"].ToString(),
												   objSQLReader["LastName"].ToString() + ", " + objSQLReader["FirstName"].ToString()});
                }

                dtbl.Rows.Add(new object[] { "0", "ALL", "ALL" });

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

            string strSQLComm = " select * from employee where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyContact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SSNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Password", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProfileID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProfileCRC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpShift", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmpRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IncludeInAppointmentBook", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AdminPassword", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AdminPasswordModifiedOn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Locked", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BookingExpFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ForcedPasscode", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    string dblvalue1 = "";
                    if (intDecimalPlace == 3)
                    {
                        dblvalue1 = Functions.fnDouble(objSQLReader["EmpRate"].ToString()).ToString("f3");
                    }
                    else
                    {
                        dblvalue1 = Functions.fnDouble(objSQLReader["EmpRate"].ToString()).ToString("f");
                    }
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["EmployeeID"].ToString(),
                                                objSQLReader["LastName"].ToString(),
												objSQLReader["FirstName"].ToString(),
                                                objSQLReader["EmergencyContact"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["EmergencyPhone"].ToString(),
                                                objSQLReader["Phone1"].ToString(),
                                                objSQLReader["Phone2"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["SSNumber"].ToString(),
                                                objSQLReader["Password"].ToString(),
                                                objSQLReader["ProfileID"].ToString(),
                                                objSQLReader["ProfileCRC"].ToString(),
                                                objSQLReader["EmpShift"].ToString(),dblvalue1,
                                                objSQLReader["IncludeInAppointmentBook"].ToString(),
                                                objSQLReader["AdminPassword"].ToString(),
                                                objSQLReader["AdminPasswordModifiedOn"].ToString(),
                                                objSQLReader["Locked"].ToString(),
                                                objSQLReader["BookingExpFlag"].ToString(),
                                                objSQLReader["ForcedPasscode"].ToString()});
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

        #region Show Record based on ID (Report)

        public DataTable FetchRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.*, s.ShiftName, s.StartTime, s.EndTime, g.GroupName as Profile from Employee a " 
                              + " left outer join ShiftMaster s on a.EmpShift=s.ID left outer join SecurityGroup g on a.ProfileID=g.ID "
                              + " where a.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyContact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmergencyPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SSNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Password", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProfileID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProfileCRC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Shift", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SecurityProfile", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strShift = objSQLReader["ShiftName"].ToString() + " ( " + objSQLReader["StartTime"].ToString() 
                                        + " - " + objSQLReader["EndTime"].ToString() + " )";
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["EmployeeID"].ToString(),
                                                objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                objSQLReader["EmergencyContact"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["EmergencyPhone"].ToString(),
                                                objSQLReader["Phone1"].ToString(),
                                                objSQLReader["Phone2"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["SSNumber"].ToString(),
                                                objSQLReader["Password"].ToString(),
                                                objSQLReader["ProfileID"].ToString(),
                                                objSQLReader["ProfileCRC"].ToString(),
                                                strShift,objSQLReader["Profile"].ToString()});
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

        public int DeleteRecord(int DeleteID)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_DeleteEmployee";

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
                intReturn = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value);

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
        #endregion

        #region Duplicate Checking

        public int DuplicateCount(string EmpID)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from EMPLOYEE where EMPLOYEEID = @EMPLOYEEID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@EMPLOYEEID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@EMPLOYEEID"].Value = EmpID;

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

        public bool IsDuplicatePassword()
        {
            int intCount = 0;

            string strSQLComm = " select count(*) as rcnt from EmpPassword where empid=@empid and pswd=@pswd ";
            
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@empid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@empid"].Value = intID;
                objSQlComm.Parameters.Add(new SqlParameter("@pswd", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@pswd"].Value = strAdminPassword;

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

                return (intCount != 0);
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

                return true;
            }
        }

        public int DuplicatePassword(int EID, string PWD)
        {
            int intCount = 0;
            string strSQLComm = " select COUNT(*) AS RECCOUNT from EMPLOYEE where password = @P and ID <> @EMPLOYEEID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@EMPLOYEEID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPLOYEEID"].Value = EID;

                objSQlComm.Parameters.Add(new SqlParameter("@P", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@P"].Value = PWD;

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

        public DataTable FetchAllServices()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,SKU,Description from product where producttype = 'S' order by Description";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceCheck", System.Type.GetType("System.Boolean"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),false});
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

        public DataTable FetchAssignedServices(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ServiceID from servicemapping where mappingtype = 'Employee' and MappingID = @MID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@MID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MID"].Value = pID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["ServiceID"].ToString() });
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

        public bool AddServiceMapping()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                DeleteServiceMapping(SaveComm, intID);
                AdjustSplitGridRecords(SaveComm);

                if (bIsChangingAdminPassword) InsertEmpPassword(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }
        
        private void AdjustSplitGridRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colServiceID = "";

                    if (dr["SKU"].ToString() == "") continue;

                    if (Convert.ToBoolean(dr["ServiceCheck"].ToString()) == true)
                    {
                        colServiceID = dr["ID"].ToString();
                        InsertServiceMapping(objSQlComm, Functions.fnInt32(colServiceID));
                    }
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
            }
        }

        public bool DeleteServiceMapping(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from servicemapping where mappingtype = 'Employee' and MappingID = @MID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public bool InsertServiceMapping(SqlCommand objSQlComm, int srvid)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into servicemapping (ServiceID,MappingID,MappingType, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values( @ServiceID,@MappingID,@MappingType, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ServiceID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ServiceID"].Value = srvid;
                objSQlComm.Parameters["@MappingID"].Value = intID;
                objSQlComm.Parameters["@MappingType"].Value = "Employee";

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return false;
            }
        }

        public bool InsertEmpPassword(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " insert into EmpPassword (EmpID,Pswd) values(@e,@p) ";
                
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@e", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@e"].Value = intAppEmpID;
                objSQlComm.Parameters["@p"].Value = strAdminPassword;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public string CanOperateonOthersAppointment(int EID)
        {
            string intCount = "N";

            string strSQLComm =   " select p.permissionflag from grouppermission p left outer join employee e on e.profileid = p.groupid "
                                + " where p.securitycode = '31a2' and e.ID = @EMPLOYEEID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@EMPLOYEEID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPLOYEEID"].Value = EID;


                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;
                
                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    intCount = objsqlReader["permissionflag"].ToString();
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
        
        public void LockEmployee()
        {
            string strSQLComm = "";

            strSQLComm = " update Employee set Locked= 'Y' where EmployeeID = @userid";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@userid", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@userid"].Value = strEmployeeID;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return;
            }
        }

        public DataTable LookupEmployee()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,EmployeeID,LastName,FirstName from Employee order by EmployeeID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["EmployeeID"].ToString(),
												   objSQLReader["LastName"].ToString() + ", " + objSQLReader["FirstName"].ToString()});
                }

                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_ADMIN, strDataObjectCulture_Administrator });
                dtbl.Rows.Add(new object[] { "9999", strDataObjectCulture_All, strDataObjectCulture_All });

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

        public string GetEmployeeName(int EID)
        {
            string strval = "";

            string strSQLComm = " select firstname + ' ' + lastname as empname  from employee where ID = @EMPLOYEEID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@EMPLOYEEID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@EMPLOYEEID"].Value = EID;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    strval = objsqlReader["empname"].ToString();
                }

                objsqlReader.Close();
                objsqlReader.Dispose();
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strval;
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

                return "";
            }
        }

    }
}
