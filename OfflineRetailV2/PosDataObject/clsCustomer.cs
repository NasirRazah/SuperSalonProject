/*
 purpose : Data for Customer
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;


namespace PosDataObject
{
    public class Customer
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private int intID;
        private int intNewID;
        private int intLoginUserID;

        

        private string strCustomerID;
        private string strAccountNo;
        private string strLastName;
        private string strFirstName;
        private string strSpouse;
        private string strCompany;
        private string strSalutation;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strState;
        private string strCountry;
        private string strZip;
        private string strShipAddress1;
        private string strShipAddress2;
        private string strShipCity;
        private string strShipState;
        private string strShipCountry;
        private string strShipZip;
        private string strWorkPhone;
        private string strHomePhone;
        private string strFax;
        private string strMobilePhone;
        private string strEMail;
        private string strDiscount;
        private string strTaxExempt;
        private string strStoreCard;
        private string strTaxID;
        private string strPOSNotes;
        
        private string strDiscountLevel;
        private int    intCategory;
        private double dblStoreCredit;
        private double dblAmountLastPurchase;
        private double dblTotalPurchases;
        private double dblARCreditLimit;

        private string strParamValue1;
        private string strParamValue2;
        private string strParamValue3;
        private string strParamValue4;
        private string strParamValue5;

        private DateTime dtDateOfBirth;
        private DateTime dtDateOfMarriage;
        private double dblClosingBalance;
        private double dblAROpeningBalance;
        private int intPoints;

        private int intDTaxID;
        private int intDiscountID;

        private string strPhotoFilePath;

        private string strNote;
        private string strSpecialEvent;
        private int intCustomerID;
        private DateTime dtDateTime;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;
        private DataTable dblGroupDataTable;
        private DataTable dblClassDataTable;

        private string ACID = "";
        private string AutoCustNo = "";
        private string strAutoCustomer;

        // Settings Data
        private int intDecimalPlace;

        private string strThisStoreCode;
        private bool blUpdateAcOpeningBalanceFlag;

        private string strActiveStatus;

        private DateTime dtRefHAcAdjustment;
        private double dblHAcAdjustmentAmount;

        private int intRefCustomer;
        private int intRefInvoice;
        private double dblCreditAmount;
        private string strTranType;
        private string strIssueStore;
        private string strOperateStore;
        private DateTime dtTranDate;

        private string strBookingExpFlag;

        private byte[] bytCustomerPhoto; //change for wpf

        #endregion

        #region definining public variables

        //change for wpf
        public byte[] CustomerPhoto
        {
            get { return bytCustomerPhoto; }
            set { bytCustomerPhoto = value; }
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

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public string BookingExpFlag
        {
            get { return strBookingExpFlag; }
            set { strBookingExpFlag = value; }
        }

        public DataTable GroupDataTable
        {
            get { return dblGroupDataTable; }
            set { dblGroupDataTable = value; }
        }

        public DataTable ClassDataTable
        {
            get { return dblClassDataTable; }
            set { dblClassDataTable = value; }
        }

        public DateTime RefHAcAdjustment
        {
            get { return dtRefHAcAdjustment; }
            set { dtRefHAcAdjustment = value; }
        }

        public double HAcAdjustmentAmount
        {
            get { return dblHAcAdjustmentAmount; }
            set { dblHAcAdjustmentAmount = value; }
        }

        public bool UpdateAcOpeningBalanceFlag
        {
            get { return blUpdateAcOpeningBalanceFlag; }
            set { blUpdateAcOpeningBalanceFlag = value; }
        }

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }

        public int DiscountID
        {
            get { return intDiscountID; }
            set { intDiscountID = value; }
        }

        public int DTaxID
        {
            get { return intDTaxID; }
            set { intDTaxID = value; }
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

        public string CustomerID
        {
            get { return strCustomerID; }
            set { strCustomerID = value; }
        }

        public string AccountNo
        {
            get { return strAccountNo; }
            set { strAccountNo = value; }
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

        public string Spouse
        {
            get { return strSpouse; }
            set { strSpouse = value; }
        }

        public string Company
        {
            get { return strCompany; }
            set { strCompany = value; }
        }

        public string Salutation
        {
            get { return strSalutation; }
            set { strSalutation = value; }
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

        public string ShipAddress1
        {
            get { return strShipAddress1; }
            set { strShipAddress1 = value; }
        }

        public string ShipAddress2
        {
            get { return strShipAddress2; }
            set { strShipAddress2 = value; }
        }

        public string ShipCity
        {
            get { return strShipCity; }
            set { strShipCity = value; }
        }

        public string ShipState
        {
            get { return strShipState; }
            set { strShipState = value; }
        }

        public string ShipCountry
        {
            get { return strShipCountry; }
            set { strShipCountry = value; }
        }

        public string ShipZip
        {
            get { return strShipZip; }
            set { strShipZip = value; }
        }

        public string WorkPhone
        {
            get { return strWorkPhone; }
            set { strWorkPhone = value; }
        }

        public string HomePhone
        {
            get { return strHomePhone; }
            set { strHomePhone = value; }
        }

        public string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }

        public string MobilePhone
        {
            get { return strMobilePhone; }
            set { strMobilePhone = value; }
        }

        public string EMail
        {
            get { return strEMail; }
            set { strEMail = value; }
        }

        public string Discount
        {
            get { return strDiscount; }
            set { strDiscount = value; }
        }

        public string TaxExempt
        {
            get { return strTaxExempt; }
            set { strTaxExempt = value; }
        }

        public string TaxID
        {
            get { return strTaxID; }
            set { strTaxID = value; }
        }

        public string POSNotes
        {
            get { return strPOSNotes; }
            set { strPOSNotes = value; }
        }

        public string DiscountLevel
        {
            get { return strDiscountLevel; }
            set { strDiscountLevel = value; }
        }

        public int Category
        {
            get { return intCategory; }
            set { intCategory = value; }
        }

        public double StoreCredit
        {
            get { return dblStoreCredit; }
            set { dblStoreCredit = value; }
        }

        public double AmountLastPurchase
        {
            get { return dblAmountLastPurchase; }
            set { dblAmountLastPurchase = value; }
        }

        public double TotalPurchases
        {
            get { return dblTotalPurchases; }
            set { dblTotalPurchases = value; }
        }

        public double ARCreditLimit
        {
            get { return dblARCreditLimit; }
            set { dblARCreditLimit = value; }
        }

        public double AROpeningBalance
        {
            get { return dblAROpeningBalance; }
            set { dblAROpeningBalance = value; }
        }


        public string PhotoFilePath
        {
            get { return strPhotoFilePath; }
            set { strPhotoFilePath = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dtDateOfBirth; }
            set { dtDateOfBirth = value; }
        }

        public DateTime DateOfMarriage
        {
            get { return dtDateOfMarriage; }
            set { dtDateOfMarriage = value; }
        }

        public double ClosingBalance
        {
            get { return dblClosingBalance; }
            set { dblClosingBalance = value; }
        }

        public int Points
        {
            get { return intPoints; }
            set { intPoints = value; }
        }

        public string StoreCard
        {
            get { return strStoreCard; }
            set { strStoreCard = value; }
        }

        public string ParamValue1
        {
            get { return strParamValue1; }
            set { strParamValue1 = value; }
        }

        public string ParamValue2
        {
            get { return strParamValue2; }
            set { strParamValue2 = value; }
        }

        public string ParamValue3
        {
            get { return strParamValue3; }
            set { strParamValue3 = value; }
        }

        public string ParamValue4
        {
            get { return strParamValue4; }
            set { strParamValue4 = value; }
        }

        public string ParamValue5
        {
            get { return strParamValue5; }
            set { strParamValue5 = value; }
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

        public int ICustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public DateTime DateTime
        {
            get { return dtDateTime; }
            set { dtDateTime = value; }
        }

        public string AutoCustomer
        {
            get { return strAutoCustomer; }
            set { strAutoCustomer = value; }
        }

        public string ThisStoreCode
        {
            get { return strThisStoreCode; }
            set { strThisStoreCode = value; }
        }

        public string ActiveStatus
        {
            get { return strActiveStatus; }
            set { strActiveStatus = value; }
        }

        public int RefCustomer
        {
            get { return intRefCustomer; }
            set { intRefCustomer = value; }
        }

        public double CreditAmount
        {
            get { return dblCreditAmount; }
            set { dblCreditAmount = value; }
        }

        public int RefInvoice
        {
            get { return intRefInvoice; }
            set { intRefInvoice = value; }
        }

        public string TranType
        {
            get { return strTranType; }
            set { strTranType = value; }
        }

        public string IssueStore
        {
            get { return strIssueStore; }
            set { strIssueStore = value; }
        }

        public string OperateStore
        {
            get { return strOperateStore; }
            set { strOperateStore = value; }
        }

        public DateTime TranDate
        {
            get { return dtTranDate; }
            set { dtTranDate = value; }
        }

        #endregion

        public string InsertHouseAccountAdjustmentData()
        {
            string strSQLComm = " insert into AcctRecv( CustomerID, Amount,TranType,Date,IssueStore,OperateStore,ExpFlag, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                              + " values ( @CustomerID,@Amount,1,@Date,@IssueStore,@OperateStore,'N',@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Date", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@CustomerID"].Value = intID;
                objSQlComm.Parameters["@Amount"].Value = dblHAcAdjustmentAmount;
                objSQlComm.Parameters["@Date"].Value = dtRefHAcAdjustment;
                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
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
                    if (strAutoCustomer == "Y")
                    {
                        AutoCustNo = GetAutoCustomer(SaveComm);
                    }
                    strError = InsertData(SaveComm);
                    strError = InsertCustomerAcctOpeningBal(SaveComm);
                }
                else
                {
                    strError = UpdateData(SaveComm);
                    if (blUpdateAcOpeningBalanceFlag) 
                        strError = UpdateCustomerAcctOpeningBal(SaveComm);
                }

                if (strError == "")
                    strError = DeleteGroup(SaveComm);
                if (strError == "")
                    strError = SaveGroup(SaveComm);

                if (strError == "")
                    strError = DeleteClass(SaveComm);
                if (strError == "")
                    strError = SaveClass(SaveComm);

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

        public string InsertData(SqlCommand objSQlComm)
        {
            byte[] Photo = GetPhoto(strPhotoFilePath);
            string strSQLComm = "";

            if (strPhotoFilePath == "")
            {
                strSQLComm = " insert into Customer( CustomerID,AccountNo,LastName,FirstName,Spouse,Company,Salutation, "
                                    + " Address1,Address2,City,State,Country,Zip,ShipAddress1,ShipAddress2,ShipCity,ShipState,"
                                    + " ShipCountry,ShipZip,WorkPhone,HomePhone,Fax,MobilePhone,EMail,Discount,TaxExempt,TaxID,"
                                    + " Category,DiscountLevel,StoreCredit,AmountLastPurchase,TotalPurchases,ARCreditLimit,"
                                    + " DateOfBirth,DateOfMarriage,ClosingBalance,Points,StoreCreditCard,"
                                    + " ParamValue1,ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,"
                                    + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,IssueStore,OperateStore,ActiveStatus,DTaxID,DiscountID) "
                                    + " values ( @CustomerID,@AccountNo,@LastName,@FirstName,@Spouse,@Company,@Salutation, "
                                    + " @Address1,@Address2,@City,@State,@Country,@Zip,@ShipAddress1,@ShipAddress2,@ShipCity,@ShipState,"
                                    + " @ShipCountry,@ShipZip,@WorkPhone,@HomePhone,@Fax,@MobilePhone,@EMail,@Discount,@TaxExempt,@TaxID,"
                                    + " @Category,@DiscountLevel,@StoreCredit,@AmountLastPurchase,@TotalPurchases,@ARCreditLimit,"
                                    + " @DateOfBirth,@DateOfMarriage,@ClosingBalance,@Points,@StoreCreditCard,"
                                    + " @ParamValue1,@ParamValue2,@ParamValue3,@ParamValue4,@ParamValue5,@POSNotes,"
                                    + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@IssueStore,@OperateStore,@ActiveStatus,@DTaxID,@DiscountID ) "
                                    + " select @@IDENTITY as ID ";
            }
            else
            {
                strSQLComm =      " insert into Customer( CustomerID,AccountNo,LastName, "
                                + " FirstName,Spouse,Company,Salutation, "
                                + " Address1,Address2,City,State,Country,"
                                + " Zip,ShipAddress1,ShipAddress2,ShipCity,ShipState,"
                                + " ShipCountry,ShipZip,WorkPhone,HomePhone,Fax,MobilePhone,"
                                + " EMail,Discount,TaxExempt,TaxID,"
                                + " Category,DiscountLevel,StoreCredit,"
                                + " AmountLastPurchase,TotalPurchases,ARCreditLimit,CustomerPhoto,"
                                + " DateOfBirth,DateOfMarriage,ClosingBalance,Points,StoreCreditCard,"
                                + " ParamValue1,ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,"
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,IssueStore,OperateStore,ActiveStatus,DTaxID,DiscountID) "
                                + " values ( @CustomerID,@AccountNo,@LastName, "
                                + " @FirstName, @Spouse, @Company, @Salutation, "
                                + " @Address1,@Address2,@City,@State,@Country,"
                                + " @Zip,@ShipAddress1,@ShipAddress2,@ShipCity,@ShipState,"
                                + " @ShipCountry,@ShipZip,@WorkPhone,@HomePhone,@Fax,@MobilePhone,"
                                + " @EMail,@Discount,@TaxExempt,@TaxID,"
                                + " @Category,@DiscountLevel,@StoreCredit,"
                                + " @AmountLastPurchase,@TotalPurchases,@ARCreditLimit,@CustomerPhoto,"
                                + " @DateOfBirth,@DateOfMarriage,@ClosingBalance,@Points,@StoreCreditCard,"
                                + " @ParamValue1,@ParamValue2,@ParamValue3,@ParamValue4,@ParamValue5,@POSNotes,"
                                + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@IssueStore,@OperateStore,@ActiveStatus,@DTaxID,@DiscountID) "
                                + " select @@IDENTITY as ID ";
            }

            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Spouse", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Salutation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MobilePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Discount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxExempt", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCredit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AmountLastPurchase", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalPurchases", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ARCreditLimit", System.Data.SqlDbType.Float));
                if (strPhotoFilePath != "")
                    objSQlComm.Parameters.Add(new SqlParameter("@CustomerPhoto", System.Data.SqlDbType.Image));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMarriage", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ClosingBalance", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCreditCard", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue5", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSNotes", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ActiveStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DTaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));

                if (strAutoCustomer == "Y")             
                    objSQlComm.Parameters["@CustomerID"].Value = AutoCustNo;
                else
                    objSQlComm.Parameters["@CustomerID"].Value = strCustomerID; 
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Spouse"].Value = strSpouse;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Salutation"].Value = strSalutation;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@MobilePhone"].Value = strMobilePhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Discount"].Value = strDiscount;
                objSQlComm.Parameters["@TaxExempt"].Value = strTaxExempt;
                objSQlComm.Parameters["@TaxID"].Value = strTaxID;
                
                objSQlComm.Parameters["@Category"].Value = intCategory;
                objSQlComm.Parameters["@DiscountLevel"].Value = strDiscountLevel;
                objSQlComm.Parameters["@StoreCredit"].Value = dblStoreCredit;
                objSQlComm.Parameters["@AmountLastPurchase"].Value = dblAmountLastPurchase;
                objSQlComm.Parameters["@TotalPurchases"].Value = dblTotalPurchases;
                objSQlComm.Parameters["@ARCreditLimit"].Value = dblARCreditLimit;

                if (strPhotoFilePath.Trim() != "")
                {
                    if (strPhotoFilePath.Trim() == "null")
                    {
                        objSQlComm.Parameters["@CustomerPhoto"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@CustomerPhoto"].Value = Photo;
                    }
                }

                if (dtDateOfBirth == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = dtDateOfBirth;
                }

                if (dtDateOfMarriage == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = dtDateOfMarriage;
                }

                objSQlComm.Parameters["@ClosingBalance"].Value = dblClosingBalance;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@StoreCreditCard"].Value = strStoreCard;

                objSQlComm.Parameters["@ParamValue1"].Value = strParamValue1;
                objSQlComm.Parameters["@ParamValue2"].Value = strParamValue2;
                objSQlComm.Parameters["@ParamValue3"].Value = strParamValue3;
                objSQlComm.Parameters["@ParamValue4"].Value = strParamValue4;
                objSQlComm.Parameters["@ParamValue5"].Value = strParamValue5;

                objSQlComm.Parameters["@POSNotes"].Value = strPOSNotes;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@ActiveStatus"].Value = strActiveStatus;
                objSQlComm.Parameters["@DTaxID"].Value = intDTaxID;
                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
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

        public string InsertDataFromPOS(SqlCommand objSQlComm)
        {
            string strSQLComm = "";

            strSQLComm = " insert into Customer( CustomerID,LastName,FirstName,Company,Address1,Address2,City,pState,"
                       + " Country,Zip,WorkPhone,HomePhone,TaxExempt,DiscountLevel,EMail,"
                       + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn,IssueStore,OperateStore ) "
                       + " values(@CustomerID,@LastName,@FirstName,@Company,@ShipAddress1,@ShipAddress2,@ShipCity,@ShipState,"
                       + " @ShipCountry,@ShipZip,@WorkPhone,@HomePhone,@TaxExempt,@DiscountLevel,@EMail,"
                       + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@IssueStore,@OperateStore ) "
                       + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxExempt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                if (strAutoCustomer == "Y") objSQlComm.Parameters["@CustomerID"].Value = ACID;
                else objSQlComm.Parameters["@CustomerID"].Value = strCustomerID;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@TaxExempt"].Value = strTaxExempt;
                objSQlComm.Parameters["@DiscountLevel"].Value = strDiscountLevel;
                objSQlComm.Parameters["@EMail"].Value = strEMail;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;

                objSQLReader = objSQlComm.ExecuteReader();
                if (objSQLReader.Read())
                {
                    this.ID = Functions.fnInt32(objSQLReader["ID"].ToString());
                }
                objSQLReader.Close();
                objSQLReader.Dispose();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                return strErrMsg;
            }
        }
        
        #endregion

        #region Update Data

        public string UpdateData(SqlCommand objSQlComm)
        {
            byte[] Photo = GetPhoto(strPhotoFilePath);
            string strSQLComm = "";
            if (strPhotoFilePath == "")
            {
                strSQLComm =      " update Customer set CustomerID=@CustomerID, AccountNo=@AccountNo, "
                                + " LastName=@LastName,FirstName=@FirstName,Spouse=@Spouse, "
                                + " Company=@Company, Salutation=@Salutation, "
                                + " Address1=@Address1,Address2=@Address2,City=@City,State=@State,Country=@Country,"
                                + " Zip=@Zip,ShipAddress1=@ShipAddress1,ShipAddress2=@ShipAddress2, "
                                + " ShipCity=@ShipCity,ShipState=@ShipState,ShipCountry=@ShipCountry,"
                                + " ShipZip=@ShipZip,WorkPhone=@WorkPhone,HomePhone=@HomePhone,Fax=@Fax,MobilePhone=@MobilePhone,"
                                + " EMail=@EMail,Discount=@Discount,TaxExempt=@TaxExempt,TaxID=@TaxID,"
                                + " Category=@Category,DiscountLevel=@DiscountLevel,StoreCredit=@StoreCredit,"
                                + " AmountLastPurchase=@AmountLastPurchase,TotalPurchases=@TotalPurchases,ARCreditLimit=@ARCreditLimit,"
                                + " DateOfBirth=@DateOfBirth,DateOfMarriage=@DateOfMarriage,ClosingBalance=@ClosingBalance,Points=@Points,"
                                + " StoreCreditCard=@StoreCreditCard, "
                                + " ParamValue1=@ParamValue1,ParamValue2=@ParamValue2,ParamValue3=@ParamValue3, "
                                + " ParamValue4=@ParamValue4,ParamValue5=@ParamValue5,POSNotes=@POSNotes,"
                                + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn,OperateStore=@OperateStore,ExpFlag = 'N', "
                                + " ActiveStatus=@ActiveStatus,DTaxID=@DTaxID,DiscountID=@DiscountID, BookingExpFlag=@BookingExpFlag where ID = @ID";
            }
            else
            {
                strSQLComm =      " update Customer set CustomerID=@CustomerID, AccountNo=@AccountNo, "
                                + " LastName=@LastName,FirstName=@FirstName,Spouse=@Spouse, "
                                + " Company=@Company, Salutation=@Salutation, "
                                + " Address1=@Address1,Address2=@Address2,City=@City,State=@State,Country=@Country,"
                                + " Zip=@Zip,ShipAddress1=@ShipAddress1,ShipAddress2=@ShipAddress2, "
                                + " ShipCity=@ShipCity,ShipState=@ShipState,ShipCountry=@ShipCountry, "
                                + " ShipZip=@ShipZip,WorkPhone=@WorkPhone,HomePhone=@HomePhone,Fax=@Fax,MobilePhone=@MobilePhone,"
                                + " EMail=@EMail,Discount=@Discount,TaxExempt=@TaxExempt,TaxID=@TaxID, "
                                + " Category=@Category,DiscountLevel=@DiscountLevel,StoreCredit=@StoreCredit,"
                                + " AmountLastPurchase=@AmountLastPurchase,TotalPurchases=@TotalPurchases,ARCreditLimit=@ARCreditLimit,"
                                + " DateOfBirth=@DateOfBirth,DateOfMarriage=@DateOfMarriage,ClosingBalance=@ClosingBalance,Points=@Points,"
                                + " StoreCreditCard=@StoreCreditCard, "
                                + " ParamValue1=@ParamValue1,ParamValue2=@ParamValue2,ParamValue3=@ParamValue3, "
                                + " ParamValue4=@ParamValue4,ParamValue5=@ParamValue5,POSNotes=@POSNotes,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,CustomerPhoto=@CustomerPhoto, "
                                + " OperateStore=@OperateStore, ExpFlag = 'N',ActiveStatus=@ActiveStatus,DTaxID=@DTaxID,DiscountID=@DiscountID, BookingExpFlag=@BookingExpFlag where ID = @ID";
            }

            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Spouse", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Salutation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MobilePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Discount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxExempt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCredit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AmountLastPurchase", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalPurchases", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ARCreditLimit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                if (strPhotoFilePath != "")
                    objSQlComm.Parameters.Add(new SqlParameter("@CustomerPhoto", System.Data.SqlDbType.Image));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMarriage", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ClosingBalance", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCreditCard", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue5", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSNotes", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ActiveStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DTaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));

                

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@CustomerID"].Value = strCustomerID;
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Spouse"].Value = strSpouse;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Salutation"].Value = strSalutation;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@MobilePhone"].Value = strMobilePhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Discount"].Value = strDiscount;
                objSQlComm.Parameters["@TaxExempt"].Value = strTaxExempt;
                objSQlComm.Parameters["@TaxID"].Value = strTaxID;
                
                objSQlComm.Parameters["@Category"].Value = intCategory;
                objSQlComm.Parameters["@DiscountLevel"].Value = strDiscountLevel;
                objSQlComm.Parameters["@StoreCredit"].Value = dblStoreCredit;
                objSQlComm.Parameters["@AmountLastPurchase"].Value = dblAmountLastPurchase;
                objSQlComm.Parameters["@TotalPurchases"].Value = dblTotalPurchases;
                objSQlComm.Parameters["@ARCreditLimit"].Value = dblARCreditLimit;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                if (strPhotoFilePath.Trim() != "")
                {
                    if (strPhotoFilePath.Trim() == "null")
                    {
                        objSQlComm.Parameters["@CustomerPhoto"].Value = Convert.DBNull;
                    }
                    else
                    {
                        objSQlComm.Parameters["@CustomerPhoto"].Value = Photo;
                    }
                }

                if (dtDateOfBirth == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = dtDateOfBirth;
                }

                if (dtDateOfMarriage == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = dtDateOfMarriage;
                }

                objSQlComm.Parameters["@ClosingBalance"].Value = dblClosingBalance;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@StoreCreditCard"].Value = strStoreCard;

                objSQlComm.Parameters["@ParamValue1"].Value = strParamValue1;
                objSQlComm.Parameters["@ParamValue2"].Value = strParamValue2;
                objSQlComm.Parameters["@ParamValue3"].Value = strParamValue3;
                objSQlComm.Parameters["@ParamValue4"].Value = strParamValue4;
                objSQlComm.Parameters["@ParamValue5"].Value = strParamValue5;

                objSQlComm.Parameters["@POSNotes"].Value = strPOSNotes;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@ActiveStatus"].Value = strActiveStatus;
                objSQlComm.Parameters["@DTaxID"].Value = intDTaxID;
                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingExpFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@BookingExpFlag"].Value = strBookingExpFlag;

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

        public string UpdateDataFromPOS(SqlCommand objSQlComm)
        {

            string strSQLComm = " update Customer set CustomerID=@CustomerID,LastName=@LastName,FirstName=@FirstName,Company=@Company, "
                              + " Address1=@ShipAddress1,Address2=@ShipAddress2,City=@ShipCity,State=@ShipState,Country=@ShipCountry,"
                              + " Zip=@ShipZip,WorkPhone=@WorkPhone,HomePhone=@HomePhone,Email=@Email,"
                              + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,OperateStore=@OperateStore,ExpFlag='N' "
                              + " where ID = @ID";

            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@CustomerID"].Value = strCustomerID;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@Email"].Value = strEMail;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
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

        #endregion

        public string InsertCustomerAcctOpeningBal(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into AcctRecv (CustomerID,InvoiceNo,Amount,TranType,Date,IssueStore,OperateStore,"
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) values (@CustomerID,@InvoiceNo,@Amount,@TranType,"
                                  + " @Date,@IssueStore,@OperateStore,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn)";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@InvoiceNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TranType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Date", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@CustomerID"].Value = intID;
                objSQlComm.Parameters["@Amount"].Value = dblAROpeningBalance;
                objSQlComm.Parameters["@InvoiceNo"].Value = 0;
                objSQlComm.Parameters["@TranType"].Value = 6;
                objSQlComm.Parameters["@Date"].Value = DateTime.Now;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;

                sqlDataReader = objSQlComm.ExecuteReader();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.ToString();
                
                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return strErrMsg;
            }
        }

        public string UpdateCustomerAcctOpeningBal(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update AcctRecv set Amount=@Amount,Date=@Date,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn, "
                                  + " ExpFlag = 'N' where CustomerID = @CustomerID and TranType = @TranType ";
                                    
                objSQlComm.CommandText = strSQLComm;


                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Amount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TranType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Date", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@CustomerID"].Value = intID;
                objSQlComm.Parameters["@Amount"].Value = dblAROpeningBalance;
                objSQlComm.Parameters["@TranType"].Value = 6;
                objSQlComm.Parameters["@Date"].Value = DateTime.Now;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.ToString();
                return strErrMsg;
            }
        }

        #region Fetch Customer House Account 

        public double FetchCustomerAcctBalance(int pCust)
        {
            double dblResult = 0;

            string strSQLComm = " select isnull(sum(Amount),0) as Balance from AcctRecv where CustomerID = @CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustomerID"].Value = pCust;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dblResult = Functions.fnDouble(objSQLReader["Balance"].ToString());
                }
                
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dblResult;
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

        public double FetchCustomerAcctOpeningBalance(int pCust)
        {
            double dblResult = 0;
            string strSQLComm = " select isnull(Amount,0) as Balance from AcctRecv where CustomerID = @CustomerID and TranType = 6 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustomerID"].Value = pCust;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dblResult = Functions.fnDouble(objSQLReader["Balance"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dblResult;
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

        #region Fetch Store Credit, Account Credit based on ID

        public DataTable FetchStoreCrAcctCr(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select StoreCredit,ARCreditLimit from Customer where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("StoreCredit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ARCreditLimit", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["StoreCredit"].ToString(),
                                                objSQLReader["ARCreditLimit"].ToString()});
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

        #endregion

        #region Fetchdata

        public DataTable FetchData(string StoreC,string filters)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string sqlactive = "";
            
            if (filters == "Active Customers") sqlactive = " and ActiveStatus = 'Y'";
            if (filters == "Inactive Customers") sqlactive = " and ActiveStatus = 'N'";

            strSQLComm = " select ID,CustomerID,FirstName,LastName,Company,Address1,WorkPhone,City,Salutation from Customer " +
                         " where (1=1) " + sqlactive + " and issuestore='" + StoreC + "' order by issuestore";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["LastName"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["Salutation"].ToString()});
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

        public DataTable FetchLookup(string thisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CustomerID,FirstName + ' ' + LastName as Customer, "
                              + " Company from Customer where issuestore='" + thisstore + "' order by customerid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["Customer"].ToString(),
                                                   objSQLReader["Company"].ToString()});
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

        #region Fetch Lookup Data

        public DataTable FetchLookupCustomerStore()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select distinct IssueStore from Customer order by IssueStore ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("IssueStore", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["IssueStore"].ToString()});
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

        public DataTable FetchLookupData(string opstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CustomerID,FirstName,LastName from Customer where issuestore='" + opstore + "' order by CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["LastName"].ToString()});
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All, strDataObjectCulture_All });

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

        #region Fetch Lookup Data for House Account

        public DataTable FetchLookupData1(string thisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CustomerID,FirstName + ' ' + LastName as Customer from "
                              + " Customer where issuestore='" + thisstore + "' order by FirstName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["Customer"].ToString()});
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

        public DataTable FetchLookupData2()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,CustomerID,FirstName + ' ' + LastName as Customer from Customer order by FirstName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["Customer"].ToString()});
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

        public DataTable FetchLookupData3(string thisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, CustomerID, FirstName + ' ' + LastName as Customer from Customer where issuestore='" 
                              + thisstore + "' " + " order by FirstName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["Customer"].ToString()});
                }

                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All });

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
            string strSQLComm = " select * from customer where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AccountNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Spouse", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCity", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipState", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCountry", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipZip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HomePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxExempt", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                
                               
                dtbl.Columns.Add("DiscountLevel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCredit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ARCreditLimit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfBirth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfMarriage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClosingBalance", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerPhoto", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCreditCard", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue4", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue5", System.Type.GetType("System.String"));
                dtbl.Columns.Add("POSNotes", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountID", System.Type.GetType("System.String"));

                dtbl.Columns.Add("BookingExpFlag", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["CustomerID"].ToString(),
                                                objSQLReader["FirstName"].ToString(),
												objSQLReader["LastName"].ToString(),
                                                objSQLReader["Company"].ToString(),
                                                objSQLReader["AccountNo"].ToString(),
                                                objSQLReader["Spouse"].ToString(),
                                                objSQLReader["Salutation"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Country"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["ShipAddress1"].ToString(),
                                                objSQLReader["ShipAddress2"].ToString(),
                                                objSQLReader["ShipCity"].ToString(),
                                                objSQLReader["ShipState"].ToString(),
                                                objSQLReader["ShipCountry"].ToString(),
                                                objSQLReader["ShipZip"].ToString(),
                                                objSQLReader["WorkPhone"].ToString(),
                                                objSQLReader["HomePhone"].ToString(),
                                                objSQLReader["Fax"].ToString(),
                                                objSQLReader["MobilePhone"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["Discount"].ToString(),
                                                objSQLReader["TaxExempt"].ToString(),
                                                objSQLReader["TaxID"].ToString(),
                                                objSQLReader["DiscountLevel"].ToString(),
                                                objSQLReader["StoreCredit"].ToString(),
                                                objSQLReader["DateLastPurchase"].ToString(),
                                                objSQLReader["AmountLastPurchase"].ToString(),
                                                objSQLReader["TotalPurchases"].ToString(),
                                                objSQLReader["ARCreditLimit"].ToString(),
                                                objSQLReader["DateOfBirth"].ToString(),
                                                objSQLReader["DateOfMarriage"].ToString(),
                                                objSQLReader["ClosingBalance"].ToString(),
                                                objSQLReader["Points"].ToString(),
                                                objSQLReader["CustomerPhoto"].ToString(),
                                                objSQLReader["StoreCreditCard"].ToString(),
                                                objSQLReader["ParamValue1"].ToString(),
                                                objSQLReader["ParamValue2"].ToString(),
                                                objSQLReader["ParamValue3"].ToString(),
                                                objSQLReader["ParamValue4"].ToString(),
                                                objSQLReader["ParamValue5"].ToString(),
                                                objSQLReader["POSNotes"].ToString(),
                                                objSQLReader["ActiveStatus"].ToString(),
                                                objSQLReader["DTaxID"].ToString(),
                                                objSQLReader["DiscountID"].ToString(),
                                                objSQLReader["BookingExpFlag"].ToString()
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

        #region Show Record based on ID (Report)

        public DataTable FetchRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from Customer where ID = @ID ";
                              

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AccountNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Spouse", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCity", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipState", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCountry", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipZip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HomePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxExempt", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                
                dtbl.Columns.Add("DiscountLevel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCredit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ARCreditLimit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfBirth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfMarriage", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClosingBalance", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCreditCard", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue4", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ParamValue5", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string chkTax = objSQLReader["TaxExempt"].ToString();
                    if (chkTax == "Y")
                    {
                        chkTax = "YES";
                    }
                    else
                    {
                        chkTax = "NO";
                    }
                    string chkCard = objSQLReader["StoreCreditCard"].ToString();
                    if (chkCard == "Y")
                    {
                        chkCard = "YES";
                    }
                    else
                    {
                        chkCard = "NO";
                    }
                    
                    string strDateTime1 = "";
                    if (objSQLReader["DateOfBirth"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["DateOfBirth"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    
                    string strDateTime2 = "";
                    if (objSQLReader["DateOfMarriage"].ToString() != "")
                    {
                        strDateTime2 = objSQLReader["DateOfMarriage"].ToString();
                        int inIndex = strDateTime2.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime2 = strDateTime2.Substring(0, inIndex).Trim();
                        }
                    }

                    string dblvalue1 = "";
                    string dblvalue2 = "";
                    string dblvalue3 = "";
                    string dblvalue4 = "";
                    string dblvalue5 = "";

                    if (intDecimalPlace == 3)
                    {
                        dblvalue1 = Functions.fnDouble(objSQLReader["StoreCredit"].ToString()).ToString("f3");
                        dblvalue2 = Functions.fnDouble(objSQLReader["AmountLastPurchase"].ToString()).ToString("f3");
                        dblvalue3 = Functions.fnDouble(objSQLReader["TotalPurchases"].ToString()).ToString("f3");
                        dblvalue4 = Functions.fnDouble(objSQLReader["ARCreditLimit"].ToString()).ToString("f3");
                        dblvalue5 = Functions.fnDouble(objSQLReader["ClosingBalance"].ToString()).ToString("f3");
                    }
                    else
                    {
                        dblvalue1 = Functions.fnDouble(objSQLReader["StoreCredit"].ToString()).ToString("f");
                        dblvalue2 = Functions.fnDouble(objSQLReader["AmountLastPurchase"].ToString()).ToString("f");
                        dblvalue3 = Functions.fnDouble(objSQLReader["TotalPurchases"].ToString()).ToString("f");
                        dblvalue4 = Functions.fnDouble(objSQLReader["ARCreditLimit"].ToString()).ToString("f");
                        dblvalue5 = Functions.fnDouble(objSQLReader["ClosingBalance"].ToString()).ToString("f");
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["CustomerID"].ToString(),
                                                objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                objSQLReader["Company"].ToString(),
                                                objSQLReader["AccountNo"].ToString(),
                                                objSQLReader["Spouse"].ToString(),
                                                objSQLReader["Salutation"].ToString(),
                                                objSQLReader["Address1"].ToString(),
                                                objSQLReader["Address2"].ToString(),
                                                objSQLReader["City"].ToString(),
                                                objSQLReader["State"].ToString(),
                                                objSQLReader["Country"].ToString(),
                                                objSQLReader["Zip"].ToString(),
                                                objSQLReader["ShipAddress1"].ToString(),
                                                objSQLReader["ShipAddress2"].ToString(),
                                                objSQLReader["ShipCity"].ToString(),
                                                objSQLReader["ShipState"].ToString(),
                                                objSQLReader["ShipCountry"].ToString(),
                                                objSQLReader["ShipZip"].ToString(),
                                                objSQLReader["WorkPhone"].ToString(),
                                                objSQLReader["HomePhone"].ToString(),
                                                objSQLReader["Fax"].ToString(),
                                                objSQLReader["MobilePhone"].ToString(),
                                                objSQLReader["EMail"].ToString(),
                                                objSQLReader["Discount"].ToString(),
                                                chkTax,
                                                objSQLReader["TaxID"].ToString(),
                                                objSQLReader["DiscountLevel"].ToString(),
                                                dblvalue1,
                                                objSQLReader["DateLastPurchase"].ToString(),
                                                dblvalue2,
                                                dblvalue3,
                                                dblvalue4,
                                                strDateTime1,
                                                strDateTime2,
                                                dblvalue5,
                                                objSQLReader["Points"].ToString(),chkCard,
                                                objSQLReader["ParamValue1"].ToString(),
                                                objSQLReader["ParamValue2"].ToString(),
                                                objSQLReader["ParamValue3"].ToString(),
                                                objSQLReader["ParamValue4"].ToString(),
                                                objSQLReader["ParamValue5"].ToString()});
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
            string strSQLComm = "sp_DeleteCustomer";

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

        public int DuplicateCount(string CustID,string IssueST)
        {
            int intCount = 0;

            string strSQLComm = " select count(*) as reccnt from customer where customerid=@prm1 and issuestore = @prm2 ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@prm1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@prm1"].Value = CustID;

                objSQlComm.Parameters.Add(new SqlParameter("@prm2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@prm2"].Value = IssueST;
                
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    intCount = Functions.fnInt32(objsqlReader["reccnt"]);
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

        #region Fetch Data For General Report

        public DataTable FetchDataForGeneralReport(string refCustID, string refCustName, string refCompany, string refZip, 
                                                   DataTable refGroup, DataTable refClass, string refOrderBy, string thisstore)
        {
            DataTable dtbl = new DataTable();
            string strSQLCustID = "";
            string strSQLCustName = "";
            string strSQLCompany = "";
            string strSQLZip = "";
            string strSQLGroup = "";
            string strSQLClass = "";
            string strSQLOrderBy = "";

            string getGroup = "";
            string getClass = "";

            if (refCustID != "")
            {
                strSQLCustID = " and a.CustomerID like '%" + refCustID + "%' ";
            }

            if (refCustName != "")
            {
                strSQLCustName = " and (( a.LastName like '%" + refCustName + "%' ) or (a.FirstName like '%" + refCustName + "%')) ";
            }

            if (refCompany != "")
            {
                strSQLCompany = " and a.Company like '%" + refCompany + "%' ";
            }

            if (refZip != "")
            {
                strSQLZip = " and a.Zip like '%" + refZip + "%' ";
            }

            foreach (DataRow drG in refGroup.Rows)
            {

                if (Convert.ToBoolean(drG["CheckGroup"].ToString()))
                {
                    if (getGroup == "")
                    {
                        getGroup = drG["ID"].ToString();
                    }
                    else
                    {
                        getGroup = getGroup + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getGroup != "")
            {
                strSQLGroup = " and gmG.ReferenceID in ( " + getGroup + " )";
            }


            foreach (DataRow drC in refClass.Rows)
            {
                if (Convert.ToBoolean(drC["CheckClass"].ToString()))
                {
                    if (getClass == "")
                    {
                        getClass = drC["ID"].ToString();
                    }
                    else
                    {
                        getClass = getClass + "," + drC["ID"].ToString();
                    }
                }
            }
            if (getClass != "")
            {
                strSQLClass = " and gmC.ReferenceID in ( " + getClass + " )";
            }


            if (refOrderBy == "1")
            {
                strSQLOrderBy = " Order by a.CustomerID ";
            }
            else if (refOrderBy == "2")
            {
                strSQLOrderBy = " Order by a.LastName ";
            }
            else if (refOrderBy == "3")
            {
                strSQLOrderBy = " Order by a.Company ";
            }
            else if (refOrderBy == "4")
            {
                strSQLOrderBy = " Order by GroupName ";
            }
            else if (refOrderBy == "5")
            {
                strSQLOrderBy = " Order by ClassName ";
            }
            else
            {
                strSQLOrderBy = " Order by a.CustomerID ";
            }

            string strSQLComm = " select Distinct a.ID,a.CustomerID,a.FirstName,a.LastName,a.Company,a.Address1,a.Address2,a.City,a.State,"
                              + " a.Zip,a.Country,a.ShipAddress1,a.ShipAddress2,a.ShipCity,a.ShipState,a.ShipZip,a.ShipCountry,a.DateLastPurchase, "
                              + " a.AmountLastPurchase,a.TotalPurchases,a.WorkPhone,a.ActiveStatus,a.EMail,isnull(gr.Description,'') as CustGroup, "
                              + " isnull(cl.Description,'') as CustClass from Customer a left outer join GeneralMapping gmG "
                              + " on a.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                              + " left outer join GeneralMapping gmC on a.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                              + " gmC.ReferenceType = 'Class' "
                              + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                              + " gmG.ReferenceType = 'Group' "
                              + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                              + " gmC.ReferenceType = 'Class' "
                              + " where (1 = 1) and a.issuestore='" + thisstore + "' " 
                              + strSQLCustID + strSQLCustName + strSQLCompany + strSQLZip 
                              + strSQLGroup + strSQLClass + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));

                dtbl.Columns.Add("ShipAddress1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCity", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipState", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipZip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipCountry", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailingLabelCompany", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailingLabelAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailingLabelAttn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShippingLabelCompany", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShippingLabelAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShippingLabelAttn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    // Mailing Label Data
                    string strMailAddress = "";
                    string strMailCityLine = "";

                    if (objSQLReader["Address1"].ToString() != "")
                    {
                        strMailAddress = objSQLReader["Address1"].ToString() + "\n";
                    }
                    if (objSQLReader["Address2"].ToString() != "")
                    {
                        strMailAddress = strMailAddress + objSQLReader["Address2"].ToString() + "\n";
                    }
                    if (objSQLReader["City"].ToString() != "")
                    {
                        strMailCityLine = objSQLReader["City"].ToString();
                    }
                    if (objSQLReader["State"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + ", " + objSQLReader["State"].ToString();
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["State"].ToString();
                        }
                    }

                    if (objSQLReader["Zip"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + " " + objSQLReader["Zip"].ToString() + "\n";
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["Zip"].ToString() + "\n";
                        }
                    }

                    strMailAddress = strMailAddress + strMailCityLine;

                    if (objSQLReader["Country"].ToString() != "")
                    {
                        strMailAddress = strMailAddress + objSQLReader["Country"].ToString();
                    }

                    // Shipping Label Data

                    string strShipAddress = "";
                    string strShipCityLine = "";

                    if (objSQLReader["ShipAddress1"].ToString() != "")
                    {
                        strShipAddress = objSQLReader["ShipAddress1"].ToString() + "\n";
                    }
                    if (objSQLReader["ShipAddress2"].ToString() != "")
                    {
                        strShipAddress = strShipAddress + objSQLReader["ShipAddress2"].ToString() + "\n";
                    }
                    if (objSQLReader["ShipCity"].ToString() != "")
                    {
                        strShipCityLine = objSQLReader["ShipCity"].ToString();
                    }
                    if (objSQLReader["ShipState"].ToString() != "")
                    {
                        if (strShipCityLine != "")
                        {
                            strShipCityLine = strShipCityLine + ", " + objSQLReader["ShipState"].ToString();
                        }
                        else
                        {
                            strShipCityLine = objSQLReader["ShipState"].ToString();
                        }
                    }

                    if (objSQLReader["ShipZip"].ToString() != "")
                    {
                        if (strShipCityLine != "")
                        {
                            strShipCityLine = strShipCityLine + " " + objSQLReader["ShipZip"].ToString() + "\n";
                        }
                        else
                        {
                            strShipCityLine = objSQLReader["ShipZip"].ToString() + "\n";
                        }
                    }

                    strShipAddress = strShipAddress + strShipCityLine;

                    if (objSQLReader["ShipCountry"].ToString() != "")
                    {
                        strShipAddress = strShipAddress + objSQLReader["ShipCountry"].ToString();
                    }

                    string strDate1 = "";
                    if (objSQLReader["DateLastPurchase"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DateLastPurchase"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = strDate1.Substring(0, inIndex).Trim();
                        }
                    }
                    else strDate1 = "";

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
												   objSQLReader["LastName"].ToString() + ", "+objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["Address2"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["Country"].ToString(), 
                                                   objSQLReader["ShipAddress1"].ToString(),
                                                   objSQLReader["ShipAddress2"].ToString(),
                                                   objSQLReader["ShipCity"].ToString(),
                                                   objSQLReader["ShipState"].ToString(),
                                                   objSQLReader["ShipZip"].ToString(),
                                                   objSQLReader["ShipCountry"].ToString(),  
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["EMail"].ToString(),
                                                   strDate1,
                                                   objSQLReader["AmountLastPurchase"].ToString(),
                                                   objSQLReader["TotalPurchases"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   strMailAddress,
                                                   objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   strShipAddress,
                                                   objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString(),
                                                   objSQLReader["ActiveStatus"].ToString(),
                    objSQLReader["CustGroup"].ToString(),
                    objSQLReader["CustClass"].ToString()});
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

        #region Show Customer Group

        public DataTable ShowGroup(int CustId)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm =   " select GM.*, G.GroupID as GroupIDName, G.Description as GroupName from GeneralMapping GM "
                                + " left outer join GroupMaster G on GM.ReferenceID = G.ID "
                                + " where GM.MappingID = @ID and GM.ReferenceType = 'Group' "
                                + " and GM.MappingType = 'Customer' order by GroupIDName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = CustId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GroupIDName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["ReferenceID"].ToString(),
                                                objSQLReader["GroupIDName"].ToString(),
                                                objSQLReader["GroupName"].ToString()});
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

        #region Save Customer Group

        public string SaveGroup(SqlCommand objSQlComm)
        {
            if (dblGroupDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblGroupDataTable.Rows)
            {
                if (dr["GroupID"].ToString() == "") continue;
                int intGroupID = 0;
                if (dr["GroupID"].ToString() != "")
                    intGroupID = Functions.fnInt32(dr["GroupID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertCustomerGroup(objSQlComm, intGroupID);
                }
            }
            return strResult;
        }

        public string InsertCustomerGroup(SqlCommand objSQlComm, int pID)
        {
            string strSQLComm = " insert into GeneralMapping(ReferenceType,ReferenceID, MappingType, MappingID,"
                              + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,IssueStore ) "
                              + " values ( @ReferenceType,@ReferenceID, @MappingType, @MappingID, "
                              + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn,@IssueStore)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@ReferenceType"].Value = "Group";
                objSQlComm.Parameters["@ReferenceID"].Value = pID;
                objSQlComm.Parameters["@MappingType"].Value = "Customer";
                objSQlComm.Parameters["@MappingID"].Value = intID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;

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

        public string DeleteGroup(SqlCommand objSQlComm)
        {
            string strSQLComm = " delete from GeneralMapping where ReferenceType= @ReferenceType "
                              + " and MappingType = @MappingType and MappingID = @MappingID and IssueStore = @IssueStore ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.VarChar));
                
                objSQlComm.Parameters["@ReferenceType"].Value = "Group";
                objSQlComm.Parameters["@MappingType"].Value = "Customer";
                objSQlComm.Parameters["@MappingID"].Value = intID;
                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;

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

        #endregion

        #region Show Customer Class

        public DataTable ShowClass(int CustId)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select GM.*, G.ClassID as ClassIDName, G.Description as ClassName from GeneralMapping GM left outer join "
                              + " ClassMaster G on GM.ReferenceID = G.ID where GM.MappingID = @ID and GM.ReferenceType = 'Class' "
                              + " and GM.MappingType = 'Customer' Order By ClassIDName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = CustId;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClassID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ClassIDName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                objSQLReader["ReferenceID"].ToString(),
                                                objSQLReader["ClassIDName"].ToString(),
                                                objSQLReader["ClassName"].ToString()});
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

        #region Save Customer Class

        public string SaveClass(SqlCommand objSQlComm)
        {
            if (dblClassDataTable == null) return "";
            string strResult = "";
            foreach (DataRow dr in dblClassDataTable.Rows)
            {
                if (dr["ClassID"].ToString() == "") continue;
                int intClassID = 0;
                if (dr["ClassID"].ToString() != "")
                    intClassID = Functions.fnInt32(dr["ClassID"].ToString());

                if (strResult == "")
                {
                    strResult = InsertCustomerClass(objSQlComm, intClassID);
                }
            }
            return strResult;
        }

        public string InsertCustomerClass(SqlCommand objSQlComm, int pID)
        {
            string strSQLComm = " insert into GeneralMapping(ReferenceType, ReferenceID, MappingType, MappingID,"
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,IssueStore) "
                                + " values ( @ReferenceType,@ReferenceID, @MappingType, @MappingID, "
                                + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn,@IssueStore)";


            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@ReferenceType"].Value = "Class";
                objSQlComm.Parameters["@ReferenceID"].Value = pID;
                objSQlComm.Parameters["@MappingType"].Value = "Customer";
                objSQlComm.Parameters["@MappingID"].Value = intID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;

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

        public string DeleteClass(SqlCommand objSQlComm)
        {
            string strSQLComm = " delete from GeneralMapping where MappingType = @MappingType "
                              + " and MappingID = @MappingID and ReferenceType = @ReferenceType and IssueStore = @IssueStore ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ReferenceType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MappingID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@ReferenceType"].Value = "Class";
                objSQlComm.Parameters["@MappingType"].Value = "Customer";
                objSQlComm.Parameters["@MappingID"].Value = intID;
                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;

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

        #endregion

        #region Fetch Special Event Data

        public DataTable FetchSpecialEvent(string pType, DateTime pStart, DateTime pEnd, int SortOption,string thisstore)
        {
            DataTable dtbl = new DataTable();
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
                        
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and n.DateTime between @SDT and @TDT ";

            if (pType == "Customer")
            {
                if (SortOption == 0)
                {
                    strSQLOrder = " order by c.CustomerID ";
                }
                if (SortOption == 1)
                {
                    strSQLOrder = " order by n.DateTime ";
                }
            }

            if (pType == "Employee")
            {
                if (SortOption == 0)
                {
                    strSQLOrder = " order by c.EmployeeID ";
                }
                if (SortOption == 1)
                {
                    strSQLOrder = " order by n.DateTime ";
                }
            }


            if (pType == "Customer")
            {
                strSQLComm = " select n.Note, n.DateTime, c.CustomerID as ID, c.LastName+', '+c.FirstName as Name,c.workphone as PH1,"
                           + " c.homephone as PH2,c.EMail as EM,isnull(gr.Description,'') as CustGroup,isnull(cl.Description,'') as CustClass "
                           + " from Notes n left outer join customer c on c.ID = n.RefID "
                           + " left outer join GeneralMapping gmG "
                           + " on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                           + " left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                           + " gmC.ReferenceType = 'Class' "
                           + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                           + " gmG.ReferenceType = 'Group' "
                           + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                           + " gmC.ReferenceType = 'Class' "
                           + " where (1 = 1) and "
                           + " n.RefType = 'Customer' and n.specialevent = 'Y' and c.issuestore = '" + thisstore + "' " + strSQLDate;
            }

            if (pType == "Employee")
            {
                strSQLComm = " select n.Note, n.DateTime, c.EmployeeID as ID, c.LastName+', '+c.FirstName as Name, c.phone1 as PH1,"
                           + " c.phone2 as PH2,c.EMail as EM, '' as CustGroup, '' as  CustClass from Notes n left outer join employee "
                           + " c on c.ID = n.RefID Where (1 = 1) and n.RefType = 'Employee' and n.specialevent = 'Y' " + strSQLDate;
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Event", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("G", System.Type.GetType("System.String"));
                dtbl.Columns.Add("C", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    if (objSQLReader["DateTime"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DateTime"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = strDate1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDate1 = "";
                    }

                        dtbl.Rows.Add(new object[] {    objSQLReader["ID"].ToString(),
												        objSQLReader["Name"].ToString(),
												        objSQLReader["PH1"].ToString(),
                                                        objSQLReader["PH2"].ToString(),
                                                        objSQLReader["EM"].ToString(),
                                                        objSQLReader["Note"].ToString(),
                                                        strDate1,
                                                        objSQLReader["CustGroup"].ToString(),
                                                        objSQLReader["CustClass"].ToString()});
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

        public string GetAutoCustomer(SqlCommand objSQlComm)
        {
            string strAutoPO = "";
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select isnull(max(cast(id as int)),0) + 1 as rcnt from customer where issuestore = @prm ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@prm"].Value = strThisStoreCode;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    strAutoPO = sqlDataReader["rcnt"].ToString();
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return strAutoPO;
            }
            catch (SqlException SQLDBException)
            {
                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return strAutoPO;
            }
        }

        public DataTable FetchCustomerInfo(int prm)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "";

            strSQLComm = " select *, FirstName + ' ' + LastName as CustomerName from customer where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = prm;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }



                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOfBirth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("HomePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Field", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strMailAddress = "";
                    string strMailCityLine = "";

                    if (objSQLReader["Address1"].ToString() != "")
                    {
                        strMailAddress = objSQLReader["Address1"].ToString();
                    }
                    if (objSQLReader["Address2"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + objSQLReader["Address2"].ToString();
                        else
                            strMailAddress = objSQLReader["Address2"].ToString();
                    }
                    if (objSQLReader["City"].ToString() != "")
                    {
                        strMailCityLine = objSQLReader["City"].ToString();
                    }
                    if (objSQLReader["State"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + ", " + objSQLReader["State"].ToString();
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["State"].ToString();
                        }
                    }

                    if (objSQLReader["Zip"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + " " + objSQLReader["Zip"].ToString();
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["Zip"].ToString();
                        }
                    }
                    if (strMailCityLine != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + strMailCityLine;
                        else
                            strMailAddress = strMailCityLine;
                    }
                    if (objSQLReader["Country"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + objSQLReader["Country"].ToString();
                        else
                            strMailAddress = objSQLReader["Country"].ToString();
                    }


                    // Shipping Label Data


                    string strShipAddress = "";
                    string strShipCityLine = "";

                    if (objSQLReader["ShipAddress1"].ToString() != "")
                    {
                        strShipAddress = objSQLReader["ShipAddress1"].ToString() + "\n";
                    }
                    if (objSQLReader["ShipAddress2"].ToString() != "")
                    {
                        strShipAddress = strShipAddress + objSQLReader["ShipAddress2"].ToString() + "\n";
                    }
                    if (objSQLReader["ShipCity"].ToString() != "")
                    {
                        strShipCityLine = objSQLReader["ShipCity"].ToString();
                    }
                    if (objSQLReader["ShipState"].ToString() != "")
                    {
                        if (strShipCityLine != "")
                        {
                            strShipCityLine = strShipCityLine + ", " + objSQLReader["ShipState"].ToString();
                        }
                        else
                        {
                            strShipCityLine = objSQLReader["ShipState"].ToString();
                        }
                    }

                    if (objSQLReader["ShipZip"].ToString() != "")
                    {
                        if (strShipCityLine != "")
                        {
                            strShipCityLine = strShipCityLine + " " + objSQLReader["ShipZip"].ToString() + "\n";
                        }
                        else
                        {
                            strShipCityLine = objSQLReader["ShipZip"].ToString() + "\n";
                        }
                    }

                    strShipAddress = strShipAddress + strShipCityLine;

                    if (objSQLReader["ShipCountry"].ToString() != "")
                    {
                        strShipAddress = strShipAddress + objSQLReader["ShipCountry"].ToString();
                    }


                    string strDateTime = "";
                    if (objSQLReader["DateOfBirth"].ToString() != "")
                    {
                        strDateTime = objSQLReader["DateOfBirth"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["DateOfBirth"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {    objSQLReader["ID"].ToString(),
												    objSQLReader["CustomerID"].ToString(),
                                                    objSQLReader["CustomerName"].ToString(),
                                                    strMailAddress,
                                                    strShipAddress,  
                                                    objSQLReader["Company"].ToString(),
                                                    objSQLReader["Salutation"].ToString(),
                                                    objSQLReader["FirstName"].ToString(),
                                                    objSQLReader["LastName"].ToString(),
                                                    strDateTime,
                                                    objSQLReader["Address1"].ToString(),
                                                    objSQLReader["Address2"].ToString(),
                                                    objSQLReader["City"].ToString(),
                                                    objSQLReader["State"].ToString(),
                                                    objSQLReader["Country"].ToString(),
                                                    objSQLReader["Zip"].ToString(),
                                                    objSQLReader["HomePhone"].ToString(),
                                                    objSQLReader["WorkPhone"].ToString(),
                                                    objSQLReader["MobilePhone"].ToString(),
                                                    objSQLReader["Fax"].ToString(),
                                                    objSQLReader["EMail"].ToString(),
                                                    "0"});

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

        public DataTable FetchFastCustomerRecords(string searchtxt, int Dpt, int FilterOption)
        {
            DataTable dtbl = new DataTable();

            string strSQLFilter = "";
            string strSQLOrderBy = "";
            if (FilterOption == 0)
            {
                strSQLFilter = " and MobilePhone like '%" + searchtxt + "%' ";
                strSQLOrderBy = " MobilePhone";
            }

            if (FilterOption == 1)
            {
                strSQLFilter = " and FirstName like '" + searchtxt + "%' ";
                strSQLOrderBy = " FirstName";
            }

            if (FilterOption == 2)
            {
                strSQLFilter = " and LastName like '" + searchtxt + "%' ";
                strSQLOrderBy = " LastName";
            }

            if (FilterOption == 3)
            {
                strSQLFilter = " and Company like '%" + searchtxt + "%' ";
                strSQLOrderBy = " Company";
            }


            string strSQLComm = " select distinct ID,FirstName,LastName,Company,MobilePhone,Address1,Address2,City,State,Country,Zip,FirstName + ' ' + LastName as CustomerName from Customer where (1 = 1) " + strSQLFilter
                + " order by " + strSQLOrderBy;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strMailAddress = "";
                    string strMailCityLine = "";

                    if (objSQLReader["Address1"].ToString() != "")
                    {
                        strMailAddress = objSQLReader["Address1"].ToString();
                    }
                    if (objSQLReader["Address2"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + objSQLReader["Address2"].ToString();
                        else
                            strMailAddress = objSQLReader["Address2"].ToString();
                    }
                    if (objSQLReader["City"].ToString() != "")
                    {
                        strMailCityLine = objSQLReader["City"].ToString();
                    }
                    if (objSQLReader["State"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + ", " + objSQLReader["State"].ToString();
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["State"].ToString();
                        }
                    }

                    if (objSQLReader["Zip"].ToString() != "")
                    {
                        if (strMailCityLine != "")
                        {
                            strMailCityLine = strMailCityLine + " " + objSQLReader["Zip"].ToString();
                        }
                        else
                        {
                            strMailCityLine = objSQLReader["Zip"].ToString();
                        }
                    }
                    if (strMailCityLine != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + strMailCityLine;
                        else
                            strMailAddress = strMailCityLine;
                    }
                    if (objSQLReader["Country"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + objSQLReader["Country"].ToString();
                        else
                            strMailAddress = objSQLReader["Country"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerName"].ToString(),
                                                   objSQLReader["MobilePhone"].ToString(),
                                                   strMailAddress});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string PostDataFromPOS()
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
                    if (strAutoCustomer == "Y")
                    {
                        ACID = GetAutoCustomer(SaveComm);
                    }
                    strError = InsertDataFromPOS(SaveComm);
                    strError = InsertCustomerAcctOpeningBal(SaveComm);
                }
                else
                {
                    strError = UpdateDataFromPOS(SaveComm);
                }

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

        public DataTable GetCustomersToReplacedWith(int intRecID,string strFind, string strAutoC, string thisstore)
        {
            DataTable dtbl = new DataTable();
            string sqlf = "";
            if (strFind != "")
            {
                sqlf = " and (( LastName like '%" + strFind + "%' ) or (FirstName like '%" + strFind
                     + "%') or (Address1 like '%" + strFind + "%') or (City like '%" + strFind + "%') ) ";
            }
            string strSQLComm = " select * from Customer where ID <> @ID and issuestore = @store " + sqlf + " order by LastName,FirstName,Address1";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@store"].Value = thisstore;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                
                
                while (objSQLReader.Read())
                {
                    string strname = "";
                    string strid = "";
                    string stradd = "";

                    string strAddress1 = "";
                    string strAddress2 = "";
                    string strCity = "";
                    string strState = "";
                    string strPostalCode = "";
                    string strPhone = "";
                    string strcitystatezip = "";

                    strAddress1 = objSQLReader["Address1"].ToString();
                    strAddress2 = objSQLReader["Address2"].ToString();
                    strCity = objSQLReader["City"].ToString();
                    strState = objSQLReader["State"].ToString();
                    strPostalCode = objSQLReader["Zip"].ToString();
                    strPhone = objSQLReader["WorkPhone"].ToString();

                    strname = objSQLReader["LastName"].ToString() + ", " + objSQLReader["FirstName"].ToString();
                    if (strAutoC == "N") strid = "ID: " + objSQLReader["CustomerID"].ToString();

                    if (strAddress1 != "") stradd = stradd + strAddress1 + "\n";

                    if (strAddress2 != null)
                    {
                        if (strAddress2 != "")
                        {
                            if (stradd != "") stradd = stradd + strAddress2 + "\n"; else stradd = strAddress2 + "\n";
                        }
                    }

                    if (strCity != null)
                    {
                        if (strCity != "") strcitystatezip = strcitystatezip + strCity;
                    }
                    if (strState != null)
                    {
                        if (strState != "")
                        {
                            if (strcitystatezip == "") strcitystatezip = strcitystatezip + strState;
                            else strcitystatezip = strcitystatezip + ", " + strState;
                        }
                    }
                    if (strPostalCode != null)
                    {
                        if (strPostalCode != "")
                        {
                            if (strcitystatezip == "") strcitystatezip = strcitystatezip + strPostalCode;
                            else strcitystatezip = strcitystatezip + " " + strPostalCode;
                        }
                    }
                    if (strcitystatezip != "")
                    {
                        if (stradd != "") stradd = stradd + strcitystatezip + "\n"; else stradd = strcitystatezip + "\n";
                    }

                    if (strPhone != null)
                    {
                        if (strPhone != "")
                        {
                            //if (stradd != "") stradd = stradd + "Ph. :" + strPhone + "\n"; else stradd = "Ph. :" +  strPhone + "\n";
                        }
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												strname,
                                                strid,
                                                stradd,
												strPhone});
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
        
        public int ReplaceCustomer(int SID,int RID,int Luser)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;

            string strSQLComm = "sp_ReplaceCustomer";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@CID"].Value = SID;

                objSQlComm.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@RID"].Value = RID;

                objSQlComm.Parameters.Add(new SqlParameter("@UserID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@UserID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@UserID"].Value = Luser;

                objSQlComm.ExecuteNonQuery();
                
                objSQLTran.Commit();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLTran.Rollback();
                objSQLTran.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return -1;
            }
        }

        public int ActiveAppointmentCount(int grpID)
        {
            int intCount = 0;

            string strSQLComm = " select count(*) as rcnt from appointments where customerid = @CID and apptstatus != 'Closed'  ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CID"].Value = grpID;

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


        public DataTable FetchRepairList(int DateFilter,DateTime FDT, DateTime TDT,int InvFilter)
        {
            DateTime FormatStartDate = DateTime.Now;
            DateTime FormatEndDate = DateTime.Now;

            DataTable dtbl = new DataTable();
            string SKUfilter1 = "";
            string SKUfilter2 = "";
            string Customerfilter = "";
            string strSQLComm = "";
            string Datefilter1 = "";
            string RepairNoFilter = "";
            string RepairItemFilter = "";
            string sqlInvFilter = "";

            if (InvFilter == 0)
            {
                sqlInvFilter = " and i.Status = 17 and i.RepairStatus <> 'Delivered' ";
            }

            if (InvFilter == 1)
            {
                sqlInvFilter = " and i.Status = 18 ";
            }

            if (InvFilter == 2)
            {
                sqlInvFilter = " and ((i.Status = 17 and i.RepairStatus <> 'Delivered') or (i.Status = 18)) ";
            }

            FormatStartDate = new DateTime(FDT.Year, FDT.Month, FDT.Day, 0, 0, 1);
            FormatEndDate = new DateTime(TDT.Year, TDT.Month, TDT.Day, 23, 59, 59);
            if (DateFilter == 0)
            {
                Datefilter1 = " and i.RepairDateIn between @DT1 and @DT2 ";
            }
            if (DateFilter == 1)
            {
                Datefilter1 = " and i.RepairDeliveryDate between @DT1 and @DT2 ";
            }
            if (DateFilter == 2)
            {
                Datefilter1 = " and i.RepairNotifiedDate between @DT1 and @DT2 ";
            }

            strSQLComm = " select i.ID as InvoiceNo,i.TotalSale as Amount,c.LastName + ', ' + c.FirstName AS Customer,"
                       + " i.Status as Status,i.RepairAmount,i.RepairAdvanceAmount,i.RepairDueAmount,i.RepairStatus,i.RepairDateIn,"
                       + " i.RepairParentID,isnull(i.RepairItemName,'') as RItem,isnull(i.RepairItemSlNo,'') as RSl, "
                       + " i.CreatedOn as DeliveryDate from trans t left outer join invoice as i "
                       + " on t.ID = i.TransactionNo left outer join customer as c on c.ID = i.CustomerID "
                       + " where (1 = 1) and i.ServiceType = 'Repair' " + Datefilter1 + sqlInvFilter
                       + " order by i.Status,i.CreatedOn ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@DT1", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@DT1"].Value = FormatStartDate;
            objSQlComm.Parameters.Add(new SqlParameter("@DT2", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@DT2"].Value = FormatEndDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Status", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("RepairStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeliveryDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairItem", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairItemSL", System.Type.GetType("System.String"));
                
                string strPrevInv = "";
                string strCustID = "";
                string strCustName = "";
                string strCustCompany = "";
                string strVoid = "Y";
                string strVoidNo = "0";

                while (objSQLReader.Read())
                {
                    strCustName = objSQLReader["Customer"].ToString();

                    if ((strPrevInv == "") || (strPrevInv != objSQLReader["InvoiceNo"].ToString()))
                    {
                        dtbl.Rows.Add(new object[] {
                                                        Functions.fnInt32(objSQLReader["Status"].ToString()) == 18 ? objSQLReader["RepairParentID"].ToString() : objSQLReader["InvoiceNo"].ToString(),
                                                        strCustName,
                                                        objSQLReader["Status"].ToString(),
                                                        Functions.fnDouble(objSQLReader["RepairAmount"].ToString()),
                                                        objSQLReader["RepairStatus"].ToString(),
                                                        Functions.fnDate(objSQLReader["RepairDateIn"].ToString()).ToString("d"),
                                                        Functions.fnInt32(objSQLReader["Status"].ToString()) == 18 ? Functions.fnDate(objSQLReader["DeliveryDate"].ToString()).ToString("d") : "",
                                                        objSQLReader["RItem"].ToString(),
                                                        objSQLReader["RSl"].ToString()
                                                });
                    }
                    strPrevInv = objSQLReader["InvoiceNo"].ToString();
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
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string FetchCustomerAddress(int intRecID)
        {
            string addr = "";

            string strSQLComm = " select * from Customer where ID = @ID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                
                string strcitystatezip = "";
                while (objSQLReader.Read())
                {
                    strCompany = objSQLReader["Company"].ToString();
                    strAddress1 = objSQLReader["Address1"].ToString();
                    strAddress2 = objSQLReader["Address2"].ToString();
                    strCity = objSQLReader["City"].ToString();
                    strState = objSQLReader["State"].ToString();
                    strZip = objSQLReader["Zip"].ToString();
                    strWorkPhone = objSQLReader["WorkPhone"].ToString();


                    if (strCompany != "") addr = strCompany + "\r\n";
                    if (strAddress1 != "") addr = addr + strAddress1 + "\r\n";
                    //if (strAddress1 != "") strReportHeader = strReportHeader + "San Patricio Town Center # 500" + "\r\n";

                    if (strAddress2 != null)
                    {
                        if (strAddress2 != "") addr = addr + strAddress2 + "\r\n";
                    }

                    if (strCity != null)
                    {
                        if (strCity != "") strcitystatezip = strcitystatezip + strCity;
                    }
                    if (strState != null)
                    {
                        if (strState != "")
                        {
                            if (strcitystatezip == "") strcitystatezip = strcitystatezip + strState;
                            else strcitystatezip = strcitystatezip + ", " + strState;
                        }
                    }
                    if (strZip != null)
                    {
                        if (strZip != "")
                        {
                            if (strcitystatezip == "") strcitystatezip = strcitystatezip + strZip;
                            else strcitystatezip = strcitystatezip + " " + strZip;
                        }
                    }
                    if (strcitystatezip != "") addr = addr + strcitystatezip + "\r\n";

                    if (strWorkPhone != null)
                    {
                        if (strWorkPhone != "") addr = addr + strWorkPhone + "\r\n";
                    }

                    
                    
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return addr;
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

        #region Store Credit Transaction

        public string PostAddStoreCredit()
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
                strError = AddStoreCredit(SaveComm);
                strError = UpdateStoreCreditInCustomerRecord(SaveComm);
                return strError;
            }
            finally
            {
                SaveComm.Dispose();
                SaveCon.Close();
                //SaveCon.Dispose();

            }
        }

        public string AddStoreCredit(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                string strSQLComm = " insert into StoreCreditTransaction (RefCustomer,TranType,TranDate,TranAmount,IssueStore,OperateStore,"
                                  + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) values (@RefCustomer,@TranType,@TranDate,@TranAmount,"
                                  + " @IssueStore,@OperateStore,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) select @@IDENTITY as ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefCustomer", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TranAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TranType", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TranDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@RefCustomer"].Value = intRefCustomer;
                objSQlComm.Parameters["@TranType"].Value = "Add Credit";
                objSQlComm.Parameters["@TranAmount"].Value = dblCreditAmount;
                objSQlComm.Parameters["@TranDate"].Value = DateTime.Now;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;

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
                strErrMsg = SQLDBException.ToString();
                objsqlReader.Close();
                objsqlReader.Dispose();

                return strErrMsg;
            }
        }

        public string UpdateStoreCreditInCustomerRecord(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " update customer set StoreCredit = StoreCredit + @TranAmount, LastChangedBy=@LastChangedBy, "
                                  + " LastChangedOn = @LastChangedOn where ID = @RefCustomer";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefCustomer", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TranAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefCustomer"].Value = intRefCustomer;
                objSQlComm.Parameters["@TranAmount"].Value = dblCreditAmount;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.ToString();
                return strErrMsg;
            }
        }

        public DataTable FetchCustomerStoreCreditTransaction(int CustID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select TranType, TranDate, TranAmount, RefInvoice from StoreCreditTransaction where RefCustomer = @CustID order by ID desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustID"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("RefInvoice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranAmount", System.Type.GetType("System.Double"));
                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {   objSQLReader["RefInvoice"].ToString(),
												   Functions.fnDate(objSQLReader["TranDate"].ToString()).ToShortDateString(),
                                                   objSQLReader["TranType"].ToString(),
                                                   Functions.fnDouble(objSQLReader["TranAmount"].ToString())
                                                   });
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
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return null;
            }
        }

        public string FetchCustomerNameFromID(int intRecID)
        {
            string name = "";

            string strSQLComm = " select FirstName, LastName, Salutation from Customer where ID = @ID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                string strcitystatezip = "";
                while (objSQLReader.Read())
                {
                    name = (objSQLReader["Salutation"].ToString() != "" ? objSQLReader["Salutation"].ToString() + " " : "")
                        + objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString();

                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return name;
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

        public int FetchCustomerAccumulatedPoints(int intRecID)
        {
            int ipoint = 0;

            string strSQLComm = " select Points from Customer where ID = @ID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                string strcitystatezip = "";
                while (objSQLReader.Read())
                {
                    ipoint = Functions.fnInt32(objSQLReader["Points"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return ipoint;
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

        public double FetchCustomerStoreCreditAmount(int pCust)
        {
            double dblResult = 0;

            string strSQLComm = " select StoreCredit from Customer where CustomerID = @CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustomerID"].Value = pCust;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dblResult = Functions.fnDouble(objSQLReader["StoreCredit"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return dblResult;
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


        public int PostStoreCreditTransaction()
        {
            int ReturnVal = -1;
            string strSQLComm = "sp_StoreCreditTransaction";
            SqlCommand objSQlComm = null;

            objSQlComm = new SqlCommand();
            if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
            objSQlComm.Connection = sqlConn;

            objSQlComm.CommandText = strSQLComm;
            objSQlComm.CommandType = CommandType.StoredProcedure;

            objSQlComm.Parameters.Add(new SqlParameter("@RefCustomer", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefCustomer"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@RefCustomer"].Value = intRefCustomer;

            objSQlComm.Parameters.Add(new SqlParameter("@RefInvoice", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@RefInvoice"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@RefInvoice"].Value = intRefInvoice;

            objSQlComm.Parameters.Add(new SqlParameter("@TranType", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@TranType"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@TranType"].Value = strTranType;

            objSQlComm.Parameters.Add(new SqlParameter("@TranAmount", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@TranAmount"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@TranAmount"].Value = dblCreditAmount;

            objSQlComm.Parameters.Add(new SqlParameter("@TranDate", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TranDate"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@TranDate"].Value = dtTranDate;

            objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@IssueStore"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@IssueStore"].Value = strIssueStore;

            objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@OperateStore"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@OperateStore"].Value = strOperateStore;

            objSQlComm.Parameters.Add(new SqlParameter("@User", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@User"].Direction = ParameterDirection.Input;
            objSQlComm.Parameters["@User"].Value = intLoginUserID;

            objSQlComm.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

            try
            {
                objSQlComm.ExecuteNonQuery();
                ReturnVal = Functions.fnInt32(objSQlComm.Parameters["@ReturnID"].Value.ToString());
                objSQlComm.Dispose();
                sqlConn.Close();
                return ReturnVal;
            }
            catch (SqlException SQLDBException)
            {
                objSQlComm.Dispose();
                sqlConn.Close();
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return -1;
            }
            finally
            {
            }
        }

        #endregion


        #region change for wpf

        public string PostData_WPF()
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
                    if (strAutoCustomer == "Y")
                    {
                        AutoCustNo = GetAutoCustomer(SaveComm);
                    }
                    strError = InsertData_WPF(SaveComm);
                    strError = InsertCustomerAcctOpeningBal(SaveComm);
                }
                else
                {
                    strError = UpdateData_WPF(SaveComm);
                    if (blUpdateAcOpeningBalanceFlag)
                        strError = UpdateCustomerAcctOpeningBal(SaveComm);
                }

                if (strError == "")
                    strError = DeleteGroup(SaveComm);
                if (strError == "")
                    strError = SaveGroup(SaveComm);

                if (strError == "")
                    strError = DeleteClass(SaveComm);
                if (strError == "")
                    strError = SaveClass(SaveComm);

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

        public string InsertData_WPF(SqlCommand objSQlComm)
        {

            string strSQLComm = "";

            strSQLComm = " insert into Customer( CustomerID,AccountNo,LastName, "
                                + " FirstName,Spouse,Company,Salutation, "
                                + " Address1,Address2,City,State,Country,"
                                + " Zip,ShipAddress1,ShipAddress2,ShipCity,ShipState,"
                                + " ShipCountry,ShipZip,WorkPhone,HomePhone,Fax,MobilePhone,"
                                + " EMail,Discount,TaxExempt,TaxID,"
                                + " Category,DiscountLevel,StoreCredit,"
                                + " AmountLastPurchase,TotalPurchases,ARCreditLimit,CustomerPhoto,"
                                + " DateOfBirth,DateOfMarriage,ClosingBalance,Points,StoreCreditCard,"
                                + " ParamValue1,ParamValue2,ParamValue3,ParamValue4,ParamValue5,POSNotes,"
                                + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,IssueStore,OperateStore,ActiveStatus,DTaxID,DiscountID) "
                                + " values ( @CustomerID,@AccountNo,@LastName, "
                                + " @FirstName, @Spouse, @Company, @Salutation, "
                                + " @Address1,@Address2,@City,@State,@Country,"
                                + " @Zip,@ShipAddress1,@ShipAddress2,@ShipCity,@ShipState,"
                                + " @ShipCountry,@ShipZip,@WorkPhone,@HomePhone,@Fax,@MobilePhone,"
                                + " @EMail,@Discount,@TaxExempt,@TaxID,"
                                + " @Category,@DiscountLevel,@StoreCredit,"
                                + " @AmountLastPurchase,@TotalPurchases,@ARCreditLimit,@CustomerPhoto,"
                                + " @DateOfBirth,@DateOfMarriage,@ClosingBalance,@Points,@StoreCreditCard,"
                                + " @ParamValue1,@ParamValue2,@ParamValue3,@ParamValue4,@ParamValue5,@POSNotes,"
                                + " @CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn,@IssueStore,@OperateStore,@ActiveStatus,@DTaxID,@DiscountID) "
                                + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Spouse", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Salutation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MobilePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Discount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxExempt", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCredit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AmountLastPurchase", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalPurchases", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ARCreditLimit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerPhoto", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMarriage", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ClosingBalance", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCreditCard", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue5", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSNotes", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@IssueStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ActiveStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DTaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));

                if (strAutoCustomer == "Y")
                    objSQlComm.Parameters["@CustomerID"].Value = AutoCustNo;
                else
                    objSQlComm.Parameters["@CustomerID"].Value = strCustomerID;
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Spouse"].Value = strSpouse;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Salutation"].Value = strSalutation;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@MobilePhone"].Value = strMobilePhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Discount"].Value = strDiscount;
                objSQlComm.Parameters["@TaxExempt"].Value = strTaxExempt;
                objSQlComm.Parameters["@TaxID"].Value = strTaxID;

                objSQlComm.Parameters["@Category"].Value = intCategory;
                objSQlComm.Parameters["@DiscountLevel"].Value = strDiscountLevel;
                objSQlComm.Parameters["@StoreCredit"].Value = dblStoreCredit;
                objSQlComm.Parameters["@AmountLastPurchase"].Value = dblAmountLastPurchase;
                objSQlComm.Parameters["@TotalPurchases"].Value = dblTotalPurchases;
                objSQlComm.Parameters["@ARCreditLimit"].Value = dblARCreditLimit;

                objSQlComm.Parameters["@CustomerPhoto"].Value = bytCustomerPhoto == null ? Convert.DBNull : bytCustomerPhoto;

                if (dtDateOfBirth == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = dtDateOfBirth;
                }

                if (dtDateOfMarriage == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = dtDateOfMarriage;
                }

                objSQlComm.Parameters["@ClosingBalance"].Value = dblClosingBalance;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@StoreCreditCard"].Value = strStoreCard;

                objSQlComm.Parameters["@ParamValue1"].Value = strParamValue1;
                objSQlComm.Parameters["@ParamValue2"].Value = strParamValue2;
                objSQlComm.Parameters["@ParamValue3"].Value = strParamValue3;
                objSQlComm.Parameters["@ParamValue4"].Value = strParamValue4;
                objSQlComm.Parameters["@ParamValue5"].Value = strParamValue5;

                objSQlComm.Parameters["@POSNotes"].Value = strPOSNotes;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@IssueStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@ActiveStatus"].Value = strActiveStatus;
                objSQlComm.Parameters["@DTaxID"].Value = intDTaxID;
                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;
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

        public string UpdateData_WPF(SqlCommand objSQlComm)
        {

            string strSQLComm = "";

            strSQLComm = " update Customer set CustomerID=@CustomerID, AccountNo=@AccountNo, "
                                + " LastName=@LastName,FirstName=@FirstName,Spouse=@Spouse, "
                                + " Company=@Company, Salutation=@Salutation, "
                                + " Address1=@Address1,Address2=@Address2,City=@City,State=@State,Country=@Country,"
                                + " Zip=@Zip,ShipAddress1=@ShipAddress1,ShipAddress2=@ShipAddress2, "
                                + " ShipCity=@ShipCity,ShipState=@ShipState,ShipCountry=@ShipCountry, "
                                + " ShipZip=@ShipZip,WorkPhone=@WorkPhone,HomePhone=@HomePhone,Fax=@Fax,MobilePhone=@MobilePhone,"
                                + " EMail=@EMail,Discount=@Discount,TaxExempt=@TaxExempt,TaxID=@TaxID, "
                                + " Category=@Category,DiscountLevel=@DiscountLevel,StoreCredit=@StoreCredit,"
                                + " AmountLastPurchase=@AmountLastPurchase,TotalPurchases=@TotalPurchases,ARCreditLimit=@ARCreditLimit,"
                                + " DateOfBirth=@DateOfBirth,DateOfMarriage=@DateOfMarriage,ClosingBalance=@ClosingBalance,Points=@Points,"
                                + " StoreCreditCard=@StoreCreditCard, "
                                + " ParamValue1=@ParamValue1,ParamValue2=@ParamValue2,ParamValue3=@ParamValue3, "
                                + " ParamValue4=@ParamValue4,ParamValue5=@ParamValue5,POSNotes=@POSNotes,"
                                + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn,CustomerPhoto=@CustomerPhoto, "
                                + " OperateStore=@OperateStore, ExpFlag = 'N',ActiveStatus=@ActiveStatus,DTaxID=@DTaxID,DiscountID=@DiscountID, BookingExpFlag=@BookingExpFlag where ID = @ID";

            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Spouse", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Company", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Salutation", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Address2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@State", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Zip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipAddress2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipState", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ShipZip", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@WorkPhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@HomePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Fax", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@MobilePhone", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EMail", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Discount", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxExempt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCredit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@AmountLastPurchase", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalPurchases", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@ARCreditLimit", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerPhoto", System.Data.SqlDbType.Image));

                objSQlComm.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOfMarriage", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ClosingBalance", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Points", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreCreditCard", System.Data.SqlDbType.Char));

                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue4", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ParamValue5", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@POSNotes", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ActiveStatus", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@DTaxID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));



                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@CustomerID"].Value = strCustomerID;
                objSQlComm.Parameters["@AccountNo"].Value = strAccountNo;
                objSQlComm.Parameters["@LastName"].Value = strLastName;
                objSQlComm.Parameters["@FirstName"].Value = strFirstName;
                objSQlComm.Parameters["@Spouse"].Value = strSpouse;
                objSQlComm.Parameters["@Company"].Value = strCompany;
                objSQlComm.Parameters["@Salutation"].Value = strSalutation;
                objSQlComm.Parameters["@Address1"].Value = strAddress1;
                objSQlComm.Parameters["@Address2"].Value = strAddress2;
                objSQlComm.Parameters["@City"].Value = strCity;
                objSQlComm.Parameters["@State"].Value = strState;
                objSQlComm.Parameters["@Country"].Value = strCountry;
                objSQlComm.Parameters["@Zip"].Value = strZip;
                objSQlComm.Parameters["@ShipAddress1"].Value = strShipAddress1;
                objSQlComm.Parameters["@ShipAddress2"].Value = strShipAddress2;
                objSQlComm.Parameters["@ShipCity"].Value = strShipCity;
                objSQlComm.Parameters["@ShipState"].Value = strShipState;
                objSQlComm.Parameters["@ShipCountry"].Value = strShipCountry;
                objSQlComm.Parameters["@ShipZip"].Value = strShipZip;
                objSQlComm.Parameters["@WorkPhone"].Value = strWorkPhone;
                objSQlComm.Parameters["@HomePhone"].Value = strHomePhone;
                objSQlComm.Parameters["@Fax"].Value = strFax;
                objSQlComm.Parameters["@MobilePhone"].Value = strMobilePhone;
                objSQlComm.Parameters["@EMail"].Value = strEMail;
                objSQlComm.Parameters["@Discount"].Value = strDiscount;
                objSQlComm.Parameters["@TaxExempt"].Value = strTaxExempt;
                objSQlComm.Parameters["@TaxID"].Value = strTaxID;

                objSQlComm.Parameters["@Category"].Value = intCategory;
                objSQlComm.Parameters["@DiscountLevel"].Value = strDiscountLevel;
                objSQlComm.Parameters["@StoreCredit"].Value = dblStoreCredit;
                objSQlComm.Parameters["@AmountLastPurchase"].Value = dblAmountLastPurchase;
                objSQlComm.Parameters["@TotalPurchases"].Value = dblTotalPurchases;
                objSQlComm.Parameters["@ARCreditLimit"].Value = dblARCreditLimit;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@CustomerPhoto"].Value = bytCustomerPhoto == null ? Convert.DBNull : bytCustomerPhoto;

                if (dtDateOfBirth == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfBirth"].Value = dtDateOfBirth;
                }

                if (dtDateOfMarriage == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOfMarriage"].Value = dtDateOfMarriage;
                }

                objSQlComm.Parameters["@ClosingBalance"].Value = dblClosingBalance;
                objSQlComm.Parameters["@Points"].Value = intPoints;
                objSQlComm.Parameters["@StoreCreditCard"].Value = strStoreCard;

                objSQlComm.Parameters["@ParamValue1"].Value = strParamValue1;
                objSQlComm.Parameters["@ParamValue2"].Value = strParamValue2;
                objSQlComm.Parameters["@ParamValue3"].Value = strParamValue3;
                objSQlComm.Parameters["@ParamValue4"].Value = strParamValue4;
                objSQlComm.Parameters["@ParamValue5"].Value = strParamValue5;

                objSQlComm.Parameters["@POSNotes"].Value = strPOSNotes;
                objSQlComm.Parameters["@OperateStore"].Value = strThisStoreCode;
                objSQlComm.Parameters["@ActiveStatus"].Value = strActiveStatus;
                objSQlComm.Parameters["@DTaxID"].Value = intDTaxID;
                objSQlComm.Parameters["@DiscountID"].Value = intDiscountID;

                objSQlComm.Parameters.Add(new SqlParameter("@BookingExpFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@BookingExpFlag"].Value = strBookingExpFlag;

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

        #endregion
    }
}
