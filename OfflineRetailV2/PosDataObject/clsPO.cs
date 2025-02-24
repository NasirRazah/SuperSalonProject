/*
 purpose : Data for Purchase Order
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class PO
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strMode;
        private string strErrorMsg;

        private int intVendorID;
        private string strOrderNo;
        private string strRefNo;
        private string strPriority;
        private string strSupplierInstructions;
        private string strGeneralNotes;

        private double dblGrossAmount;
        private double dblFreight;
        private double dblDiscount;
        private double dblTax;
        private double dblNetAmount;
        private int intOrderItems;
        private string strCDiscountLevel;
        private string strCTaxExempt;

        private DateTime dtOrderDate;
        private DateTime dtPickupDate;
        private DateTime dtExpectedDeliveryDate;
        private int intCustomerID;

        private double dblItemFreight;
        private double dblItemQty;
        private double dblItemCost;
        private double dblItemTax;
        private string strItemVendorPartNo;
        private string strItemDescription;
        private int intItemSKU;
        private string strItemNotes;
        private int intItemCaseQty;


        private double dblItemRate;
        private int intItemTaxID1;
        private int intItemTaxID2;
        private int intItemTaxID3;
        private string strItemTaxable1;
        private string strItemTaxable2;
        private string strItemTaxable3;
        private double dblItemTaxRate1;
        private double dblItemTaxRate2;
        private double dblItemTaxRate3;
        private int intItemTaxType1;
        private int intItemTaxType2;
        private int intItemTaxType3;
        private double dblItemTaxTotal1;
        private double dblItemTaxTotal2;
        private double dblItemTaxTotal3;


        private double dblItemDiscount;
        private double dblItemDiscValue;
        private int intItemDiscountID;
        private string strItemDiscLogic;
        private string strItemDiscountText;




        private string strItemCaseUPC;
        private string strTerminalName;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        private DataTable dblInitialDataTable;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string strAutoPO;
        private int intCheckInClerk;
        private int intPrintingClerk;

        private string strCreateType;
        private int intCreateID;

        private string AutoPONo = "";
        private double dblMinOrderAmount;

        private string strTransferFrom;
        private string strTransferTo;

        private string strTransferStatus;

        private bool boolAdjustStockForReadyState;

        private int intDeptID;

        private string strItemSKU;
        private bool blDelete;
        private string sPrintlabelFlag = "";

        private DataTable dblDataTableForPrintLabel;

        private bool blAdjustInventoryOnProductionList;
        private string strConnString;

        private bool blIfExistInPrintLabel = false;
        private string strListStatus;

        private bool blbatchprintFlag;

        #endregion

        #region definining public variables

        public bool batchprintFlag
        {
            get { return blbatchprintFlag; }
            set { blbatchprintFlag = value; }
        }

        public string ListStatus
        {
            get { return strListStatus; }
            set { strListStatus = value; }
        }

        public string ConnString
        {
            get { return strConnString; }
            set { strConnString = value; }
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

        public bool AdjustInventoryOnProductionList
        {
            get { return blAdjustInventoryOnProductionList; }
            set { blAdjustInventoryOnProductionList = value; }
        }

        public bool bAdjustStockForReadyState
        {
            get { return boolAdjustStockForReadyState; }
            set { boolAdjustStockForReadyState = value; }
        }

        public double MinOrderAmount
        {
            get { return dblMinOrderAmount; }
            set { dblMinOrderAmount = value; }
        }

        public string TerminalName
        {
            get { return strTerminalName; }
            set { strTerminalName = value; }
        }

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
        }

        public string AutoPO
        {
            get { return strAutoPO; }
            set { strAutoPO = value; }
        }

        public string TransferFrom
        {
            get { return strTransferFrom; }
            set { strTransferFrom = value; }
        }

        public string TransferTo
        {
            get { return strTransferTo; }
            set { strTransferTo = value; }
        }

        public string TransferStatus
        {
            get { return strTransferStatus; }
            set { strTransferStatus = value; }
        }

        public int CheckInClerk
        {
            get { return intCheckInClerk; }
            set { intCheckInClerk = value; }
        }

        public int PrintingClerk
        {
            get { return intPrintingClerk; }
            set { intPrintingClerk = value; }
        }

        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
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

        public int VendorID
        {
            get { return intVendorID; }
            set { intVendorID = value; }
        }

        public int DeptID
        {
            get { return intDeptID; }
            set { intDeptID = value; }
        }

        public string OrderNo
        {
            get { return strOrderNo; }
            set { strOrderNo = value; }
        }

        public string RefNo
        {
            get { return strRefNo; }
            set { strRefNo = value; }
        }

        public string Priority
        {
            get { return strPriority; }
            set { strPriority = value; }
        }

        public string SupplierInstructions
        {
            get { return strSupplierInstructions; }
            set { strSupplierInstructions = value; }
        }

        public string GeneralNotes
        {
            get { return strGeneralNotes; }
            set { strGeneralNotes = value; }
        }

        public double GrossAmount
        {
            get { return dblGrossAmount; }
            set { dblGrossAmount = value; }
        }

        public double Freight
        {
            get { return dblFreight; }
            set { dblFreight = value; }
        }

        public double Tax
        {
            get { return dblTax; }
            set { dblTax = value; }
        }

        public double NetAmount
        {
            get { return dblNetAmount; }
            set { dblNetAmount = value; }
        }

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public DateTime PickupDate
        {
            get { return dtPickupDate; }
            set { dtPickupDate = value; }
        }

        public DateTime OrderDate
        {
            get { return dtOrderDate; }
            set { dtOrderDate = value; }
        }

        public DateTime ExpectedDeliveryDate
        {
            get { return dtExpectedDeliveryDate; }
            set { dtExpectedDeliveryDate = value; }
        }

        public double Discount
        {
            get { return dblDiscount; }
            set { dblDiscount = value; }
        }

        public int OrderItems
        {
            get { return intOrderItems; }
            set { intOrderItems = value; }
        }

        public string CDiscountLevel
        {
            get { return strCDiscountLevel; }
            set { strCDiscountLevel = value; }
        }

        public string CTaxExempt
        {
            get { return strCTaxExempt; }
            set { strCTaxExempt = value; }
        }

        public int CreateID
        {
            get { return intCreateID; }
            set { intCreateID = value; }
        }

        public string CreateType
        {
            get { return strCreateType; }
            set { strCreateType = value; }
        }

        #endregion

        public void BeginTransaction()
        {
            //SaveComm = new SqlCommand(); 
            SaveTran = null;
            SaveCon = this.sqlConn;
            if (SaveCon.State == System.Data.ConnectionState.Open) SaveCon.Close();
            SaveCon.Open();
            SaveTran = SaveCon.BeginTransaction();
        }

        public void EndTransaction()
        {
            SaveTran.Commit();
            SaveTran.Dispose();
        }

        public bool InsertPO()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

               

                if (strMode == "Add")
                {
                    if (strAutoPO == "Y")
                    {
                        AutoPONo = GetAutoPO(SaveComm);
                    }
                    InsertPOHeader(SaveComm);
                }
                else
                {
                    UpdatePOHeader(SaveComm);
                }

                DeletePODetails(SaveComm, intID);
                AdjustSplitGridRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        #region Insert PO Header Data

        public bool InsertPOHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into POHeader ( OrderNo,OrderDate,VendorID,RefNo, "
                    + " ExpectedDeliveryDate, Priority, GrossAmount, Freight, "
                    + " Tax, NetAmount, SupplierInstructions, GeneralNotes, "
                    + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn,CheckInClerk,VendorMinOrderAmount ) "
                    + " Values ( @OrderNo,@OrderDate,@VendorID,@RefNo, "
                    + " @ExpectedDeliveryDate, @Priority, @GrossAmount, @Freight, "
                    + " @Tax, @NetAmount, @SupplierInstructions, @GeneralNotes, "
                    + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn,@CheckInClerk,@VendorMinOrderAmount ) "
                    + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@OrderNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ExpectedDeliveryDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@Priority", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GrossAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NetAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@SupplierInstructions", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorMinOrderAmount", System.Data.SqlDbType.Float));

                if (strAutoPO == "Y")
                {
                    objSQlComm.Parameters["@OrderNo"].Value = AutoPONo;
                }
                else
                {
                    objSQlComm.Parameters["@OrderNo"].Value = strOrderNo;
                }
                objSQlComm.Parameters["@OrderDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@VendorID"].Value = intVendorID;
                objSQlComm.Parameters["@RefNo"].Value = strRefNo;
                if (dtExpectedDeliveryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpectedDeliveryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpectedDeliveryDate"].Value = dtExpectedDeliveryDate;
                }
                objSQlComm.Parameters["@Priority"].Value = strPriority;
                objSQlComm.Parameters["@GrossAmount"].Value = dblGrossAmount;
                objSQlComm.Parameters["@Freight"].Value = dblFreight;
                objSQlComm.Parameters["@Tax"].Value = dblTax;
                objSQlComm.Parameters["@NetAmount"].Value = dblNetAmount;
                objSQlComm.Parameters["@SupplierInstructions"].Value = strSupplierInstructions;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@VendorMinOrderAmount"].Value = dblMinOrderAmount;

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

        #region Update Tax Header Data

        public string UpdatePOHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update POHeader set OrderNo=@OrderNo,OrderDate=@OrderDate,VendorID=@VendorID,RefNo=@RefNo, "
                                  + " ExpectedDeliveryDate=@ExpectedDeliveryDate, Priority=@Priority, GrossAmount=@GrossAmount, Freight=@Freight, "
                                  + " Tax=@Tax, NetAmount=@NetAmount, SupplierInstructions=@SupplierInstructions, "
                                  + " GeneralNotes=@GeneralNotes,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                                  + " CheckInClerk=@CheckInClerk Where ID = @ID ";
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@OrderNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ExpectedDeliveryDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@Priority", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GrossAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@NetAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@SupplierInstructions", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@OrderNo"].Value = strOrderNo;
                objSQlComm.Parameters["@OrderDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@VendorID"].Value = intVendorID;
                objSQlComm.Parameters["@RefNo"].Value = strRefNo;
                if (dtExpectedDeliveryDate == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@ExpectedDeliveryDate"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@ExpectedDeliveryDate"].Value = dtExpectedDeliveryDate;
                }

                
                objSQlComm.Parameters["@Priority"].Value = strPriority;
                objSQlComm.Parameters["@GrossAmount"].Value = dblGrossAmount;
                objSQlComm.Parameters["@Freight"].Value = dblFreight;
                objSQlComm.Parameters["@Tax"].Value = dblTax;
                objSQlComm.Parameters["@NetAmount"].Value = dblNetAmount;
                objSQlComm.Parameters["@SupplierInstructions"].Value = strSupplierInstructions;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return "";
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.Message;
                return strErrorMsg;
            }

        }

        #endregion

        #region Fetch PO Header data

        public DataTable FetchPOHeader(int pVendor, DateTime pFDate, DateTime pTDate , string pPO) //, string pPrint
        {
            string sqlFilter = "";
            string sqlFilter1 = "";
            string sqlFilter2 = "";
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            if (pVendor != 0)
            {
                sqlFilter = " and a.VendorID = @Vendor";
            }
            if (pPO != "")
            {
                sqlFilter1 = " and OrderNo like '%"+pPO+"%' ";
            }
            /*if (pPrint == "N")
            {
                sqlFilter2 = " and a.ID not in ( Select PurchaseOrderID from RecvHeader )";
            }*/

            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.ID,OrderNo,OrderDate,ExpectedDeliveryDate,b.Name as VendorName, "
                                + " GrossAmount, Freight, Tax, NetAmount,Priority  "
                                + " From POHeader a left outer join Vendor b on a.VendorID = b.ID where (1 = 1) "
                                + " and OrderDate between @FDT and @TDT " + sqlFilter + sqlFilter1 
                                + " order by OrderDate desc ";

            

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (pVendor != 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Vendor", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Vendor"].Value = pVendor;
            }
            //objSQlComm.Parameters.Add(new SqlParameter("@PO", System.Data.SqlDbType.VarChar));
            //objSQlComm.Parameters["@PO"].Value = pPO;

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpectedDeliveryDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("NetAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {

                    string strDateTime = "";
                    string strDateTime1 = "";
                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }

                    if (objSQLReader["ExpectedDeliveryDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["ExpectedDeliveryDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["ExpectedDeliveryDate"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["OrderNo"].ToString(),
                                                   strDateTime,
                                                   strDateTime1,
                                                   objSQLReader["VendorName"].ToString(),
                                                   Functions.fnDouble(objSQLReader["GrossAmount"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Freight"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Tax"].ToString()), 
                                                   Functions.fnDouble(objSQLReader["NetAmount"].ToString()),
                                                   objSQLReader["Priority"].ToString()});
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable FetchMinAmountPO(int pVendor, DateTime pFDate, DateTime pTDate)
        {
            string sqlFilter = "";
            string sqlFilter1 = "";
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            if (pVendor != 0)
            {
                sqlFilter = " and a.VendorID = @Vendor";
            }
            
            DataTable dtbl = new DataTable();

            string strSQLComm =   " select b.ID,OrderNo,OrderDate,ExpectedDeliveryDate,b.Name as VendorName,b.Contact,b.Address1, "
                                + " GrossAmount, Freight, Tax, NetAmount,Priority,VendorMinOrderAmount  "
                                + " From POHeader a left outer join Vendor b on a.VendorID = b.ID where (1 = 1) "
                                + " and NetAmount < VendorMinOrderAmount "
                                + " and OrderDate between @FDT and @TDT " + sqlFilter + sqlFilter1
                                + " order by b.Name, OrderDate desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (pVendor != 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@Vendor", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Vendor"].Value = pVendor;
            }
            

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpectedDeliveryDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Contact", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("NetAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("VendorMinOrderAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {

                    string strDateTime = "";
                    string strDateTime1 = "";
                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }

                    if (objSQLReader["ExpectedDeliveryDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["ExpectedDeliveryDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["ExpectedDeliveryDate"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["OrderNo"].ToString(),
                                                   strDateTime,
                                                   strDateTime1,
                                                   objSQLReader["VendorName"].ToString(),
                                                   objSQLReader["Contact"].ToString(),
                                                   objSQLReader["Address1"].ToString(),
                                                   Functions.fnDouble(objSQLReader["GrossAmount"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Freight"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Tax"].ToString()), 
                                                   Functions.fnDouble(objSQLReader["NetAmount"].ToString()),
                                                   Functions.fnDouble(objSQLReader["VendorMinOrderAmount"].ToString()),
                                                   objSQLReader["Priority"].ToString()});
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
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
                    string colSKU = "";
                    string colVendorPartNo = "";
                    string colDescription = "";
                    string colCost = "";
                    string colQty = "";
                    string colFreight = "";
                    string colTax = "";
                    string colCaseQty = "";
                    string colCaseUPC = "";
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }

                    if (dr["Cost"].ToString() != "")
                    {
                        colCost = dr["Cost"].ToString();
                    }
                    else
                    {
                        colCost = "0.00";
                    }

                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }

                    if (dr["Freight"].ToString() != "")
                    {
                        colFreight = dr["Freight"].ToString();
                    }
                    else
                    {
                        colFreight = "0.00";
                    }

                    
                    if (dr["Tax"].ToString() != "")
                    {
                        colTax = dr["Tax"].ToString();
                    }
                    else
                    {
                        colTax = "0.00";
                    }

                    if (dr["CaseQty"].ToString() != "")
                    {
                        colCaseQty = dr["CaseQty"].ToString();
                    }
                    else
                    {
                        colCaseQty = "0";
                    }
                    colCaseUPC = dr["CaseUPC"].ToString();
                    colVendorPartNo = dr["VendorPartNo"].ToString();
                    colDescription = dr["Description"].ToString();
                    strItemNotes = dr["Notes"].ToString();
                    dblItemCost = Functions.fnDouble(colCost);
                    dblItemFreight = Functions.fnDouble(colFreight);
                    dblItemQty = Functions.fnDouble(colQty);
                    dblItemTax = Functions.fnDouble(colTax);
                    intItemCaseQty = Functions.fnInt32(colCaseQty);
                    strItemCaseUPC = colCaseUPC;
                    strItemDescription = colDescription;
                    strItemVendorPartNo = colVendorPartNo;
                    intItemSKU = Functions.fnInt32(colSKU);
                    InsertPODetails(objSQlComm, intID);

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeletePODetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from PODetail Where RefID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertPODetails(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into PODetail ( RefID, ProductID,VendorPartNo,Description, "
                                  + " Cost,Qty,Freight,Tax,Notes,CaseQty,CaseUPC, "
                                  + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @RefID, @SKU,@VendorPartNo,@Description, "
                                  + " @Cost,@Qty,@Freight,@Tax,@Notes,@CaseQty,@CaseUPC, "
                                  + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorPartNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@SKU"].Value = intItemSKU;
                objSQlComm.Parameters["@VendorPartNo"].Value = strItemVendorPartNo;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Cost"].Value = dblItemCost;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@Freight"].Value = dblItemFreight;
                objSQlComm.Parameters["@Tax"].Value = dblItemTax;
                objSQlComm.Parameters["@Notes"].Value = strItemNotes;
                objSQlComm.Parameters["@CaseQty"].Value = intItemCaseQty;
                objSQlComm.Parameters["@CaseUPC"].Value = strItemCaseUPC;

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
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public int DuplicateCount(string pOrder)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from POHeader where OrderNo = @param ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@param"].Value = pOrder;

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
                objSQlComm.Dispose();
                sqlConn.Close();

                return intCount;
            }
        }


        #region Show Header Record based on ID

        public DataTable ShowHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from POHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpectedDeliveryDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NetAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SupplierInstructions", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GeneralNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["OrderNo"].ToString(),
                                                objSQLReader["OrderDate"].ToString(),
                                                objSQLReader["VendorID"].ToString(),
                                                objSQLReader["RefNo"].ToString(),
                                                objSQLReader["ExpectedDeliveryDate"].ToString(),
                                                objSQLReader["Priority"].ToString(),
                                                objSQLReader["GrossAmount"].ToString(),
                                                objSQLReader["Freight"].ToString(),
                                                objSQLReader["Tax"].ToString(),
                                                objSQLReader["NetAmount"].ToString(),
                                                objSQLReader["SupplierInstructions"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["CheckInClerk"].ToString()});
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

        #region Show Detail Record based on ID

        public DataTable ShowDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select POD.*, P.SKU, P.DecimalPlace,P.ProductType,p.ScaleBarCode from PODetail POD "
                              + " left outer join Product P on POD.ProductID = P.ID "
                              + " where POD.RefID = @ID order by POD.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPartNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BatchNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintLabels", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));

                double dblDetTax = 0;
                double dblExtCost = 0;
                double dblDetQty = 0;
                double dblDetCost = 0;
                double dblDetFreight = 0;
                int    intDetCaseQty = 0;
                while (objSQLReader.Read())
                {
                    dblDetTax = 0;
                    dblExtCost = 0;
                    dblDetQty = 0;
                    dblDetCost = 0;
                    dblDetFreight = 0;
                    intDetCaseQty = 0;

                    if (objSQLReader["Qty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    }
                    if (objSQLReader["Cost"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["Cost"].ToString());
                    }
                    if (objSQLReader["Freight"].ToString() != "")
                    {
                        dblDetFreight = Functions.fnDouble(objSQLReader["Freight"].ToString());
                    }
                    if (objSQLReader["Tax"].ToString() != "")
                    {
                        dblDetTax = Functions.fnDouble(objSQLReader["Tax"].ToString());
                    }
                    if (objSQLReader["CaseQty"].ToString() != "")
                    {
                        intDetCaseQty = Functions.fnInt32(objSQLReader["CaseQty"].ToString());
                    }

                    dblExtCost = (dblDetQty * dblDetCost) + dblDetFreight + dblDetTax;
                    //if (intDetCaseQty == 0) dblExtCost = (dblDetQty * dblDetCost) + dblDetFreight + dblDetTax;
                    //if (intDetCaseQty > 0) dblExtCost = (dblDetQty * intDetCaseQty * dblDetCost) + dblDetFreight + dblDetTax;

                    dtbl.Rows.Add(new object[] {
                                                "0",
                                                objSQLReader["ProductID"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["VendorPartNo"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                dblDetQty,
                                                dblDetCost,
                                                dblDetFreight,
                                                dblDetTax,
                                                dblExtCost.ToString("n"),
                                                objSQLReader["Notes"].ToString(),"",true,
                                                objSQLReader["DecimalPlace"].ToString(),
                                                intDetCaseQty,
                                                objSQLReader["CaseUPC"].ToString(),
                                                objSQLReader["ProductType"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString()});
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

        public bool DeletePOHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from POHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                
                return false;
            }
        }

        public bool DeletePO()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                if (IsExistInReceipt(SaveComm, intID) == 0)
                {
                    DeletePOHeader(SaveComm, intID);
                    DeletePODetails(SaveComm, intID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public int IsExistInReceipt(SqlCommand objSQlComm, int intRefID)
        {
            int intCount = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " select count(*) as RecCount from RecvHeader Where PurchaseOrderID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                
                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intCount = Functions.fnInt32(sqlDataReader["RecCount"]);
                
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return intCount;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return intCount;
            }
        }

        #region Fetch ID from PO No.

        public int FetchIDfromPONO(string strpPONO, int  intpVendor)
        {
            int intPOID = 0;
            string strSQLComm = "select ID from POHeader where OrderNo = @OrderNo and VendorID = @VendorID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@OrderNo", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@OrderNo"].Value = strpPONO;
            objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@VendorID"].Value = intpVendor;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    intPOID  = Functions.fnInt32(objSQLReader["ID"].ToString());
                }
                
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return intPOID;
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

        #region If Exists PO No.

        public int IfExistsPONO(string strpPONO, ref int refVendor, ref string refDate)
        {
            int intPOID = 0;
            refVendor = 0;
            string strSQLComm = "select VendorID, OrderDate from POHeader where OrderNo = @OrderNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@OrderNo", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@OrderNo"].Value = strpPONO;
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    string strDateTime = "";
                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }
                    refDate = strDateTime;
                    refVendor = Functions.fnInt32(objSQLReader["VendorID"].ToString());
                    intPOID = 1;

                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intPOID;
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

        public string GetAutoPO(SqlCommand objSQlComm)
        {
            string strAutoPO = "";
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select isnull(Max(cast(orderno as int)),0) + 1 as RCNT from poheader ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    strAutoPO = sqlDataReader["RCNT"].ToString();
                }
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return strAutoPO;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return strAutoPO;
            }
        }


        public int IfExistsInReceipt(int intRefID)
        {
            int intCount = 0;
            string strSQLComm = "select count(*) as RecCount from RecvHeader Where PurchaseOrderID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRefID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    intCount = Functions.fnInt32(objSQLReader["RecCount"]);

                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intCount;
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


        public double GetReceiptQtyForPerticularCombination(int intRefID, int pPID, string pVPart, double pCost )
        {
            double dQty = 0;
            string strSQLComm = " select isnull(sum(qty),0) as TQty from RecvDetail where ProductID = @p1 and VendorPartNo = @p2 and Cost =@p3 and "
                              + " BatchNo in ( select ID from RecvHeader Where PurchaseOrderID = @ID ) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRefID;

            objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@p1"].Value = pPID;

            objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@p2"].Value = pVPart;

            objSQlComm.Parameters.Add(new SqlParameter("@p3", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@p3"].Value = pCost;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    dQty = Functions.fnDouble(objSQLReader["TQty"]);

                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dQty;
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


        public double GetReceiptQtyForPerticularCombination(int intRefID, int pPID, string pVPart, double pCost, int LineID)
        {
            string SqlFilter1 = "";
            if (LineID > 0) SqlFilter1 = " and ID <> @LineID ";
            double dQty = 0;
            string strSQLComm = " select isnull(sum(qty),0) as TQty from RecvDetail where ProductID = @p1 and VendorPartNo = @p2 and Cost =@p3  " + SqlFilter1
                              + " and BatchNo in ( select ID from RecvHeader Where PurchaseOrderID = @ID ) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRefID;

            objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@p1"].Value = pPID;

            objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@p2"].Value = pVPart;

            objSQlComm.Parameters.Add(new SqlParameter("@p3", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@p3"].Value = pCost;

            if (LineID > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@LineID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@LineID"].Value = LineID;
            }

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    dQty = Functions.fnDouble(objSQLReader["TQty"]);

                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dQty;
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

        public double GetPOQtyForPerticularCombination(int intRefID, int pPID, string pVPart, double pCost)
        {
           
            double dQty = 0;
            string strSQLComm = " select isnull(sum(qty),0) as TQty from PODetail where ProductID = @p1 and VendorPartNo = @p2 and Cost =@p3  " 
                              + " and RefID in ( select ID from POHeader Where ID = @ID ) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRefID;

            objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@p1"].Value = pPID;

            objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@p2"].Value = pVPart;

            objSQlComm.Parameters.Add(new SqlParameter("@p3", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@p3"].Value = pCost;

            

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    dQty = Functions.fnDouble(objSQLReader["TQty"]);

                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dQty;
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

        #region Show Header Record based on ID ( Report)
        public DataTable FetchHeaderRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm =   " select a.*,v.Name,v.Address1,v.Address2,v.City,v.Zip,v.State,v.Country, "
                                + " v.Contact, v.Phone, v.Fax, v.EMail, "
                                + " d.Qty as DQty, d.Cost as DCost, d.Freight as DFreight, d.Tax as DTax, "
                                + " d.Description as ProductName, d.VendorPartNo "
                                + " from POHeader a left outer join Vendor v on a.VendorID = v.ID "
                                + " left outer join PODetail d on a.ID = d.RefID " 
                                + " where a.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExpectedDeliveryDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Priority", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NetAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SupplierInstructions", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VOthers", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPartNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DFreight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DExtCost", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    double dblDetTax = 0;
                    double dblExtCost = 0;
                    double dblDetQty = 0;
                    double dblDetCost = 0;
                    double dblDetFreight = 0;

                    string strAddress = "";
                    string strOtherDetails = "";

                    string strccitystatezip = "";

                    if (objSQLReader["DQty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["DQty"].ToString());
                    }
                    if (objSQLReader["DCost"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["DCost"].ToString());
                    }
                    if (objSQLReader["DFreight"].ToString() != "")
                    {
                        dblDetFreight = Functions.fnDouble(objSQLReader["DFreight"].ToString());
                    }
                    if (objSQLReader["DTax"].ToString() != "")
                    {
                        dblDetTax = Functions.fnDouble(objSQLReader["DTax"].ToString());
                    }
                    dblExtCost = (dblDetQty * dblDetCost) + dblDetFreight + dblDetTax;

                    if (objSQLReader["Address1"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["Address1"].ToString() + "\n";
                    }
                    
                    if (objSQLReader["Address2"].ToString() != "")
                    {
                        strAddress = strAddress  + objSQLReader["Address2"].ToString() + "\n";
                    }

                    if (objSQLReader["City"].ToString() != "") strccitystatezip = strccitystatezip + objSQLReader["City"].ToString();
                    if (objSQLReader["State"].ToString() != "")
                    {
                        if (strccitystatezip == "") strccitystatezip = strccitystatezip + objSQLReader["State"].ToString();
                        else strccitystatezip = strccitystatezip + ", " + objSQLReader["State"].ToString();
                    }
                    if (objSQLReader["Zip"].ToString() != "")
                    {
                        if (strccitystatezip == "") strccitystatezip = strccitystatezip + objSQLReader["Zip"].ToString();
                        else strccitystatezip = strccitystatezip + " " + objSQLReader["Zip"].ToString();
                    }
                    if (strccitystatezip != "") strAddress = strAddress + strccitystatezip + "\n";

                    
                    if (objSQLReader["Country"].ToString() != "")
                    {
                        strAddress = strAddress  + objSQLReader["Country"].ToString() + "\n";
                    }

                    if (objSQLReader["Contact"].ToString() != "")
                    {
                        strOtherDetails = strOtherDetails + "Contact : " + objSQLReader["Contact"].ToString() + "\n";

                    }

                    if (objSQLReader["Phone"].ToString() != "")
                    {
                        strOtherDetails = strOtherDetails + "Phone : " + objSQLReader["Phone"].ToString() + "\n";
                    }

                    if (objSQLReader["Fax"].ToString() != "")
                    {
                        strOtherDetails = strOtherDetails + "Fax : " + objSQLReader["Fax"].ToString() + "\n";
                    }

                    if (objSQLReader["EMail"].ToString() != "")
                    {
                        strOtherDetails = strOtherDetails + "EMail : " + objSQLReader["EMail"].ToString() + "\n";
                    }

                    string strDateTime1 = "";
                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    string strDateTime2 = "";
                    if (objSQLReader["ExpectedDeliveryDate"].ToString() != "")
                    {
                        strDateTime2 = objSQLReader["ExpectedDeliveryDate"].ToString();
                        int inIndex = strDateTime2.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime2 = strDateTime2.Substring(0, inIndex).Trim();
                        }
                    }

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["OrderNo"].ToString(),
                                                strDateTime1,
                                                objSQLReader["VendorID"].ToString(),
                                                objSQLReader["RefNo"].ToString(),
                                                strDateTime2,
                                                objSQLReader["Priority"].ToString(),
                                                objSQLReader["GrossAmount"].ToString(),
                                                objSQLReader["Freight"].ToString(),
                                                objSQLReader["Tax"].ToString(),
                                                objSQLReader["NetAmount"].ToString(),
                                                objSQLReader["SupplierInstructions"].ToString(),
                                                objSQLReader["Name"].ToString(),
                                                strAddress,
                                                strOtherDetails,
                                                objSQLReader["ProductName"].ToString(),
                                                objSQLReader["VendorPartNo"].ToString(),
                                                dblDetQty.ToString(),
                                                dblDetCost.ToString(),
                                                dblDetFreight.ToString(),
                                                dblDetTax.ToString(),
                                                dblExtCost.ToString()});
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

        #region Transfer Item

        public DataTable FetchTransferHeader(DateTime pFDate, DateTime pTDate)
        {
            string sqlFilter = "";
            string sqlFilter1 = "";
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            
            DataTable dtbl = new DataTable();

            string strSQLComm =   " select a.ID, TransferNo, TransferDate, Status, w.StoreCode + ' - ' + w.StoreName as TransferTo, "
                                + " TotalQty, TotalCost "
                                + " From TransferHeader a left outer join WebStores w on a.ToStore = w.StoreCode where (1 = 1) "
                                + " and TransferDate between @FDT and @TDT "
                                + " order by TransferDate desc, ToStore ";



            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ToStore", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalQty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TotalCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Status", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    string strDateTime = "";

                    if (objSQLReader["TransferDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["TransferDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["TransferDate"].ToString();
                    }


                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TransferNo"].ToString(),
                                                   strDateTime,
                                                   objSQLReader["TransferTo"].ToString(),
                                                   Functions.fnDouble(objSQLReader["TotalQty"].ToString()),
                                                   Functions.fnDouble(objSQLReader["TotalCost"].ToString()),
                                                   objSQLReader["Status"].ToString() });
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable ShowTransferDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select h.Status, d.*, p.SKU from TransferHeader h left outer join TransferDetail d on h.ID = d.RefID left outer join Product P on d.ProductID = P.ID where d.RefID = @ID order by d.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Total", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PQty", System.Type.GetType("System.Double"));

                double dblTotalCost = 0;
                double dblDetQty = 0;
                double dblDetCost = 0;
                
                while (objSQLReader.Read())
                {
                    dblDetQty = 0;
                    dblDetCost = 0;
                    dblTotalCost = 0;

                    if (objSQLReader["Qty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    }
                    if (objSQLReader["Cost"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["Cost"].ToString());
                    }

                    dblTotalCost = (dblDetQty * dblDetCost);

                    dtbl.Rows.Add(new object[] {objSQLReader["ProductID"].ToString(),
                                               
                                                objSQLReader["Description"].ToString(),
                                                dblDetQty,
                                                dblDetCost,
                                                dblTotalCost.ToString("n"),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["Notes"].ToString(),
                                                objSQLReader["Status"].ToString() == "Pending"? 0 : dblDetQty
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

        public bool PostTransfer()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                // Add or Edit Tax Data //
                if (strMode == "Add")
                {
                    AutoPONo = GetAutoTransferNo(SaveComm);
                    InsertTransferHeader(SaveComm);
                }
                else
                {
                    UpdateTransferHeader(SaveComm);
                }
                if (boolAdjustStockForReadyState)
                {
                    DataTable dtbl = FetchTransferDetailPreviousRecords(SaveComm, intID);
                    AdjustStockOfTransferRecords(SaveComm, dtbl);
                }
                DeleteTransferDetails(SaveComm, intID);
                AdjustTransferRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        public string GetAutoTransferNo(SqlCommand objSQlComm)
        {
            string strAutoPO = "";
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select isnull(Max(cast(transferno as int)),0) + 1 as RCNT from transferheader  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    strAutoPO = sqlDataReader["RCNT"].ToString();
                }
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return strAutoPO;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return strAutoPO;
            }
        }

        public bool InsertTransferHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into TransferHeader ( TransferNo, TransferDate, FromStore, ToStore,  Status, TotalQty, TotalCost, "
                                  + " GeneralNotes, CheckInClerk, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                                  + " values ( @TransferNo, @TransferDate, @FromStore, @ToStore, @Status, @TotalQty, @TotalCost, @GeneralNotes,"
                                  + " @CheckInClerk, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@TransferNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TransferDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@FromStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ToStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                

                objSQlComm.Parameters["@TransferNo"].Value = AutoPONo;
                objSQlComm.Parameters["@TransferDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@FromStore"].Value = strTransferFrom;
                objSQlComm.Parameters["@ToStore"].Value = strTransferTo;
                objSQlComm.Parameters["@Status"].Value = strTransferStatus;
                objSQlComm.Parameters["@TotalQty"].Value = dblGrossAmount;
                objSQlComm.Parameters["@TotalCost"].Value = dblNetAmount;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public bool UpdateTransferHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            
            try
            {
                string strSQLComm = " update TransferHeader set TransferDate=@TransferDate, FromStore=@FromStore, ToStore=@ToStore,  Status=@Status,"
                                  + " TotalQty=@TotalQty, TotalCost=@TotalCost, GeneralNotes=@GeneralNotes, CheckInClerk=@CheckInClerk, "
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID=@ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TransferDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@FromStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ToStore", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TotalCost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@TransferDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@FromStore"].Value = strTransferFrom;
                objSQlComm.Parameters["@ToStore"].Value = strTransferTo;
                objSQlComm.Parameters["@Status"].Value = strTransferStatus;
                objSQlComm.Parameters["@TotalQty"].Value = dblGrossAmount;
                objSQlComm.Parameters["@TotalCost"].Value = dblNetAmount;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteTransferDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from TransferDetail where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustTransferRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSKU = "";
                    string colVendorPartNo = "";
                    string colDescription = "";
                    string colCost = "";
                    string colQty = "";
                    string colFreight = "";
                    string colTax = "";
                    string colCaseQty = "";
                    string colCaseUPC = "";
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }

                    if (dr["Cost"].ToString() != "")
                    {
                        colCost = dr["Cost"].ToString();
                    }
                    else
                    {
                        colCost = "0.00";
                    }

                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }

                    
                    colDescription = dr["Description"].ToString();
                    strItemNotes = dr["Notes"].ToString();
                    dblItemCost = Functions.fnDouble(colCost);
                    dblItemQty = Functions.fnDouble(colQty);
                    strItemDescription = colDescription;
                    intItemSKU = Functions.fnInt32(colSKU);
                    InsertTransferDetail(objSQlComm, intID);

                    if (strTransferStatus == "Ready")
                    {
                        double stkqty = 0;
                        stkqty = dblItemQty;

                        //Functions.UpdateItemStockBalance(objSQlComm, intItemSKU, stkqty, intLoginUserID);

                        int SJ = Functions.AddStockJournal(objSQlComm, "Stock Out", "Web Transfer", "Y", intItemSKU,intLoginUserID, stkqty, dblItemCost, 
                                        intID.ToString(),strTerminalName, dtOrderDate, DateTime.Now, "");

                        double iqty = 0;
                        double icost = 0;
                        iqty = GetItemQty(objSQlComm, intItemSKU);
                        icost = GetItemCost(objSQlComm, intItemSKU);
                        if (iqty != 0)
                            UpdateItemCost(objSQlComm, intItemSKU, (((iqty + dblItemQty) * icost) - (dblItemQty * dblItemCost)) / iqty, dblItemCost, intLoginUserID);
                    }

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertTransferDetail(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into TransferDetail ( RefID, ProductID, Description, Cost, Qty, Notes, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @RefID, @SKU, @Description, @Cost, @Qty, @Notes, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@SKU"].Value = intItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Cost"].Value = dblItemCost;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@Notes"].Value = strItemNotes;
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
                strErrorMsg = SQLDBException.ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                return false;
            }
        }

        public double GetItemQty(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            double intResult = -1;
            string strSQLComm = "";

            strSQLComm = "select QtyOnHand from product where ID = @ITEMID ";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.Text;

            objCommand.Parameters.Add(new SqlParameter("@ITEMID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ITEMID"].Value = ItemID;

            try
            {
                SqlDataReader objSQLReader = null;
                objSQLReader = objCommand.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = Functions.fnDouble(objSQLReader["QtyOnHand"].ToString());
                }
                objSQLReader.Close();
                return intResult;
            }
            catch
            {
                return -1;
            }
        }

        public double GetItemCost(SqlCommand objCommand, int ItemID)
        {

            objCommand.Parameters.Clear();
            double intResult = -1;
            string strSQLComm = "";

            strSQLComm = "select Cost from product where ID = @ITEMID ";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.Text;

            objCommand.Parameters.Add(new SqlParameter("@ITEMID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ITEMID"].Value = ItemID;

            try
            {
                SqlDataReader objSQLReader = null;
                objSQLReader = objCommand.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = Functions.fnDouble(Functions.fnDouble(objSQLReader["Cost"].ToString()).ToString("f"));
                }
                objSQLReader.Close();
                return intResult;
            }
            catch
            {
                return -1;
            }
        }

        public void UpdateItemCost(SqlCommand objCommand, int ID, double ItemCost, double ItemLastCost, int LogonUser)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = " update product set Cost = @Cost,LastCost = @LastCost,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn "
                              + " where ID = @ID";

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastCost", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objCommand.Parameters["@Cost"].Value = Functions.fnDouble(ItemCost.ToString("f"));
                objCommand.Parameters["@LastCost"].Value = Functions.fnDouble(ItemLastCost.ToString("f"));
                objCommand.Parameters["@LastChangedBy"].Value = LogonUser;
                objCommand.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objCommand.Parameters["@ID"].Value = ID;

                objCommand.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                objCommand.Dispose();
            }
        }

        public DataTable ShowTransferHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from TransferHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FromStore", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ToStore", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Status", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GeneralNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["TransferNo"].ToString(),
                                                objSQLReader["TransferDate"].ToString(),
                                                objSQLReader["FromStore"].ToString(),
                                                objSQLReader["ToStore"].ToString(),
                                                objSQLReader["Status"].ToString(),
                                                objSQLReader["TotalQty"].ToString(),
                                                objSQLReader["TotalCost"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["CheckInClerk"].ToString()});
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

        public bool DeleteTransfer()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                string status = GetTransferRecordStatus(SaveComm, intID);
                if (status != "Sent")
                {
                    if (status == "Ready")
                    {
                        DataTable dtbl = FetchTransferDetailPreviousRecords(SaveComm, intID);
                        AdjustStockOfTransferRecords(SaveComm, dtbl);
                    }
                    DeleteTransferHeader(SaveComm, intID);
                    DeleteTransferDetails(SaveComm, intID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public string GetTransferRecordStatus(SqlCommand objSQlComm, int intRefID)
        {
            string val = "";
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " select status from TransferHeader where ID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    val = sqlDataReader["status"].ToString();
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                sqlDataReader.Close();
                sqlDataReader.Dispose();
                return "";
            }
        }

        public bool DeleteTransferHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from TransferHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public DataTable FetchTransferDetailPreviousRecords(SqlCommand objSQlComm, int pRef)
        {
            DataTable dtbl = new DataTable();
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = " select * from TransferDetail where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pRef;

                sqlDataReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));

                while (sqlDataReader.Read())
                {
                    dtbl.Rows.Add(new object[] {sqlDataReader["ProductID"].ToString(),
                                                sqlDataReader["Qty"].ToString(),
                                                sqlDataReader["Cost"].ToString()});
                }
                sqlDataReader.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return dtbl;
            }
        }

        private void AdjustStockOfTransferRecords(SqlCommand objSQlComm, DataTable dtbl)
        {
            try
            {
                if (dtbl == null) return;
                foreach (DataRow dr in dtbl.Rows)
                {
                    string colSKU = "";
                    string colCost = "";
                    string colQty = "";
                    
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }

                    if (dr["Cost"].ToString() != "")
                    {
                        colCost = dr["Cost"].ToString();
                    }
                    else
                    {
                        colCost = "0.00";
                    }

                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }


                    dblItemCost = Functions.fnDouble(colCost);
                    dblItemQty = Functions.fnDouble(colQty);
                    intItemSKU = Functions.fnInt32(colSKU);

                    double stkqty = 0;
                    stkqty = dblItemQty;

                    int SJ = Functions.AddStockJournal(objSQlComm, "Stock In", "Adj-Transfer", "Y", intItemSKU, intLoginUserID, stkqty, dblItemCost,
                                    intID.ToString(), strTerminalName, DateTime.Now, DateTime.Now, "");

                    double iqty = 0;
                    double icost = 0;
                    iqty = GetItemQty(objSQlComm, intItemSKU);
                    icost = GetItemCost(objSQlComm, intItemSKU);
                    if (iqty != 0)
                        UpdateItemCost(objSQlComm, intItemSKU, (((iqty + dblItemQty) * icost) - (dblItemQty * dblItemCost)) / iqty, dblItemCost, intLoginUserID);

                }
                dtbl.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public DataTable FetchTransferRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm =   " select a.*,w.StoreName,d.Qty as DQty, d.Cost as DCost,  "
                                + " d.Description as ProductName, p.SKU as SKU "
                                + " from TransferHeader a left outer join TransferDetail d on a.ID = d.RefID "
                                + " left outer join Product p on p.ID = d.ProductID "
                                + " left outer join WebStores w on w.StoreCode = a.ToStore "
                                + " where a.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferTo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransferStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTCost", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    double dblExtCost = 0;
                    double dblDetQty = 0;
                    double dblDetCost = 0;

                    if (objSQLReader["DQty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["DQty"].ToString());
                    }
                    if (objSQLReader["DCost"].ToString() != "")
                    {
                        dblDetCost = Functions.fnDouble(objSQLReader["DCost"].ToString());
                    }
                    
                    dblExtCost = (dblDetQty * dblDetCost);

                    

                    string strDateTime1 = "";
                    if (objSQLReader["TransferDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["TransferDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["TransferNo"].ToString(),
                                                strDateTime1,
                                                objSQLReader["ToStore"].ToString() + " - " + objSQLReader["StoreName"].ToString(),
                                                
                                                objSQLReader["Status"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["TotalQty"].ToString(),
                                                objSQLReader["TotalCost"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["ProductName"].ToString(),
                                                dblDetQty.ToString(),
                                                dblDetCost.ToString(),
                                                dblExtCost.ToString()});
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

        #endregion

        #region Production List

        public DataTable ShowProductionListDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from ProductionListDetails where RefID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));

                dtbl.Columns.Add("PrintLabelFlag", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("D", System.Type.GetType("System.Boolean"));

                double dblTotalCost = 0;


                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {objSQLReader["SKU"].ToString(),
                                                Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("0.##"),
                                                objSQLReader["Description"].ToString(),
                                                objSQLReader["ProductID"].ToString(),
                                                
                                                objSQLReader["PrintLabelFlag"].ToString() == "Y" ? true : false,
                                                false
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

        public bool PostProductionList()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                dblDataTableForPrintLabel = new DataTable();
                dblDataTableForPrintLabel.Columns.Add("ITEMID", System.Type.GetType("System.String"));

                if (strMode == "Add")
                {
                    InsertProductionListHeader(SaveComm);
                }
                else
                {
                    if (blAdjustInventoryOnProductionList)
                    {
                        string cs = FetchCurrentProductionListStatus(SaveComm);
                        //if (cs == "R") AdjustStockJournal(SaveComm);
                    }
                    UpdateProductionListHeader(SaveComm);
                }

                if (strMode == "Edit")
                {
                    if (blAdjustInventoryOnProductionList)
                    {
                        AdjustPreviousRecord(SaveComm);
                        DeleteProductionPrintBatch(SaveComm);
                    }

                    DeleteProductionListDetails(SaveComm, intID);
                }

                
                AdjustProductionListRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustStockJournal(SqlCommand objSQlComm)
        {
            DataTable dtbltemp = new DataTable();
            dtbltemp = FetchRecordForAdjust(objSQlComm);
            try
            {
                if (dtbltemp == null) return;
                foreach (DataRow dr in dtbltemp.Rows)
                {
                    string colITEMID = "";
                    string colQTY = "";

                    if (dr["ITEMID"].ToString() == "") continue;

                    if (dr["ITEMID"].ToString() != "")
                    {
                        colITEMID = dr["ITEMID"].ToString();
                    }

                    if (dr["QTY"].ToString() != "")
                    {
                        colQTY = dr["QTY"].ToString();
                    }
                    else
                    {
                        colQTY = "0.00";
                    }

                    dblItemCost = 0;
                    dblItemQty = Functions.fnDouble(colQTY);
                    intItemCaseQty = 0;
                    intItemSKU = GetProductIDFromScale(objSQlComm,Functions.fnInt32(colITEMID));

                    double stkqty = 0;
                    if (intItemCaseQty == 0) stkqty = dblItemQty;

                    int SJ = Functions.AddStockJournal(objSQlComm, "Stock Out", "Adjustment-Production", "Y", intItemSKU,
                             intLoginUserID, stkqty, dblItemCost, intID.ToString(), strTerminalName, dtOrderDate, DateTime.Now, "");

                }
                dtbltemp.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }

        }

        public string FetchCurrentProductionListStatus(SqlCommand objSQlComm)
        {
            string val = "";
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = "select ListStatus from ProductionListHeader where ID = @Ref ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref"].Value = intID;

                sqlDataReader = objSQlComm.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    val = sqlDataReader["ListStatus"].ToString();
                }
                sqlDataReader.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return "";
            }
        }

        public DateTime FetchProductionListDate(SqlCommand objSQlComm)
        {
            DateTime val = DateTime.Today.Date;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = "select RefDate from ProductionListHeader where ID = @Ref ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref"].Value = intID;

                sqlDataReader = objSQlComm.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    val = Functions.fnDate(sqlDataReader["RefDate"].ToString());
                }
                sqlDataReader.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return val;
            }
        }

        public int GetProductIDFromScale(SqlCommand objSQlComm, int iRowID)
        {
            int val = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = "select ProductID from Scale_Product where row_id = @Ref ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref"].Value = iRowID;

                sqlDataReader = objSQlComm.ExecuteReader();
                
                while (sqlDataReader.Read())
                {
                    val = Functions.fnInt32(sqlDataReader["ProductID"].ToString());
                }
                sqlDataReader.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return 0;
            }
        }

        public DataTable FetchRecordForAdjust(SqlCommand objSQlComm)
        {
            DataTable dtbl = new DataTable();
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            string strSQLComm = "";
            try
            {
                strSQLComm = "select ProductID as ITEMID, Qty from ProductionListDetails where RefID = @Ref ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref"].Value = intID;

                sqlDataReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ITEMID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QTY", System.Type.GetType("System.String"));
                
                while (sqlDataReader.Read())
                {
                    dtbl.Rows.Add(new object[] {sqlDataReader["ITEMID"].ToString(),
                                                sqlDataReader["Qty"].ToString()});
                }
                sqlDataReader.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return dtbl;
            }
        }

        private bool AdjustPreviousRecord(SqlCommand AdjustSQlComm)
        {

            bool boolResult = false;
            AdjustSQlComm.Parameters.Clear();
            string strSQLComm;

            strSQLComm = " select ProductID as ITEMID,Qty from ProductionListDetails where RefID = @Ref ";

            SqlConnection objSQLCon = new SqlConnection(strConnString);

            if (objSQLCon.State == System.Data.ConnectionState.Open) { objSQLCon.Close(); }
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@Ref"].Value = intID;

            SqlDataReader objSQLReader = null;
            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {

                    /*if (Functions.fnInt32(objSQLReader["CaseQty"].ToString()) == 0)
                    {
                        Functions.UpdateItemStockBalance(AdjustSQlComm,
                                        Functions.fnInt32(objSQLReader["ITEMID"].ToString()),
                                        -Functions.fnDouble(objSQLReader["Qty"].ToString()), intLoginUserID);
                    }
                    if (Functions.fnInt32(objSQLReader["CaseQty"].ToString()) > 0)
                    {
                        Functions.UpdateItemStockBalance(AdjustSQlComm,
                                        Functions.fnInt32(objSQLReader["ITEMID"].ToString()),
                                        -Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnInt32(objSQLReader["CaseQty"].ToString()), intLoginUserID);
                    }*/

                    DeletePrintLabelForProductionListUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()));
                    UpdatePrintQtyForProductionListUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()),
                                                            Functions.fnDouble(objSQLReader["Qty"].ToString()));
                    bool blF = IfExistInPrintLabel(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()));
                    if (blF)
                        dblDataTableForPrintLabel.Rows.Add(new object[] { objSQLReader["ITEMID"].ToString() });

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                return boolResult;
            }
            catch (SqlException SQLDBException)
            {
                string x = SQLDBException.Message;
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool DeletePrintLabelForProductionListUpdate(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from productionprintbatch where productid = @ID and RefID = @Ref";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Ref"].Value = intID;
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool UpdatePrintQtyForProductionListUpdate(SqlCommand objSQlComm, int intRefQty, double intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update product set QtyToPrint = QtyToPrint - @Qty where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefQty;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(intRefID);
                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool IfExistInPrintLabel(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = " select count(*) as RC from productionprintbatch where ProductID=@ITEMID and RefID = @Ref";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.Text;

            objCommand.Parameters.Add(new SqlParameter("@ITEMID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ITEMID"].Value = ItemID;
            objCommand.Parameters.Add(new SqlParameter("@Ref", System.Data.SqlDbType.Int));
            objCommand.Parameters["@Ref"].Value = intID;


            try
            {
                SqlDataReader objSQLReader = null;
                objSQLReader = objCommand.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = Functions.fnInt32(objSQLReader["RC"].ToString());
                }
                objSQLReader.Close();
                if (intResult > 0) return true; else return false;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertProductionListHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into ProductionListHeader ( RefNo, RefDate, RefDept, GeneralNotes, CheckInClerk, PrintingClerk, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                                  + " values ( @RefNo, @RefDate, @RefDept,@GeneralNotes, @CheckInClerk, @PrintingClerk, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDept", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintingClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefNo"].Value = strOrderNo;
                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@RefDept"].Value = intDeptID;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@PrintingClerk"].Value = intPrintingClerk;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public bool UpdateProductionListHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update ProductionListHeader set RefNo=@RefNo, RefDate=@RefDate, RefDept=@RefDept, GeneralNotes=@GeneralNotes, "
                                  + " CheckInClerk=@CheckInClerk, PrintingClerk = @PrintingClerk, ListStatus = @ListStatus, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";
  
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDept", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintingClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@ListStatus", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@RefNo"].Value = strOrderNo;
                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@RefDept"].Value = intDeptID;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@PrintingClerk"].Value = intPrintingClerk;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ListStatus"].Value = strListStatus;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteProductionListDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ProductionListDetails where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustProductionListRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSKU = "";
                    string colDescription = "";
                    string colQty = "";
                    string colF = "";
                    if (dr["ProductID"].ToString() == "") continue;
                    if (Convert.ToBoolean(dr["D"].ToString())) continue;
                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }
                    
                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }

                    sPrintlabelFlag = Convert.ToBoolean(dr["PrintLabelFlag"].ToString()) ? "Y" : "N";

                    colDescription = dr["Description"].ToString();
                    strItemSKU = dr["SKU"].ToString();
                    dblItemQty = Functions.fnDouble(colQty);
                    strItemDescription = colDescription;
                    intItemSKU = Functions.fnInt32(colSKU);

                    InsertProductionListDetail(objSQlComm, intID);

                    if (blAdjustInventoryOnProductionList)
                    {
                        //Functions.UpdateItemStockBalance(objSQlComm, intItemSKU, dblItemQty, intLoginUserID);

                        //int SJ = Functions.AddStockJournal(objSQlComm, dblItemQty >= 0 ? "Stock In" : "Stock Out", dblItemQty >= 0 ? "Production" : "Return", "N", intItemSKU,
                        //intLoginUserID, dblItemQty >= 0 ? dblItemQty : -dblItemQty, dblItemCost, intID.ToString(),
                                //strTerminalName, dtOrderDate, DateTime.Now, "");


                        if (sPrintlabelFlag == "Y")
                        {
                            InsertPrintLabel(objSQlComm);
                        }
                    }
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void DeleteProductionPrintBatch(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from ProductionPrintBatch where RefID = @RefID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefID"].Value = intID;

                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        public void InsertPrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into ProductionPrintBatch ( RefID,ProductID,Qty,SKU,Description,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " Values ( @RefID,@ProductID,@Qty,@SKU, @Description, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(dblItemQty);//PrintLabelQty
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                sqlDataReader = objSQlComm.ExecuteReader();
                sqlDataReader.Close();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
            }
        }

        private void UpdatePrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Update ProductionPrintBatch set Qty = Qty + @Qty where ProductID=@ID and RefID = @RefID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intItemSKU;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(dblItemQty);
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefID"].Value = intID;

                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        private void UpdatePrintQty(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Update product set QtyToPrint = QtyToPrint + @Qty where ID=@ID  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intItemSKU;
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(dblItemQty);
                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        public bool InsertProductionListDetail(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into ProductionListDetails ( RefID, ProductID, Description, SKU, Qty, PrintLabelFlag) "
                                  + " values ( @RefID, @ProductID,  @Description, @SKU,@Qty, @PrintLabelFlag ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintLabelFlag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@PrintLabelFlag"].Value = sPrintlabelFlag;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);

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

        public DataTable FetchProductionListHeader(DateTime pFDate, DateTime pTDate, int pDept)
        {
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            if (pDept > 0)
            {
                sqlFilter1 = " and a.RefDept = @D";
            }

            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.RefNo, a.ID, a.RefDate,d.Description, a.CheckInClerk,a.PrintingClerk, isnull(e.FirstName,'') as FN, isnull(e.LastName,'') as LN , "
                                + " isnull(e1.FirstName,'') as FN1, isnull(e1.LastName,'') as LN1  from ProductionListHeader a left outer join Dept d on a.RefDept = d.ID  "
                                + " left outer join employee e on e.ID = a.CheckInClerk left outer join employee e1 on e1.ID = a.PrintingClerk where (1 = 1) " + sqlFilter1
                                + " and a.RefDate between @FDT and @TDT "
                                + " order by a.RefDate desc, d.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;
            if (pDept > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@D", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@D"].Value = pDept;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee1", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    string strDateTime = "";
                    string emp = "";
                    string emp1 = "";
                    if (objSQLReader["RefDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                    }
                    if (Functions.fnInt32(objSQLReader["CheckInClerk"].ToString()) > 0)
                    {
                        emp = objSQLReader["FN"].ToString() + " " + objSQLReader["LN"].ToString();
                    }
                    if (Functions.fnInt32(objSQLReader["PrintingClerk"].ToString()) > 0)
                    {
                        emp1 = objSQLReader["FN1"].ToString() + " " + objSQLReader["LN1"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["RefNo"].ToString(),
                                                   strDateTime,
                                                   objSQLReader["Description"].ToString(),
                                                   emp,emp1
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable ShowProductionListHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select h.*, e1.FirstName + ' ' + e1.LastName as CClerk, e2.FirstName + ' ' + e2.LastName as PClerk "
                              + " from ProductionListHeader h left outer join Employee e1 on e1.ID = h.CheckInClerk "
                              + " left outer join Employee e2 on e2.ID = h.PrintingClerk where h.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefDept", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GeneralNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintingClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Emp1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Emp2", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["RefNo"].ToString(),
												objSQLReader["RefDept"].ToString(),
                                                objSQLReader["RefDate"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["CheckInClerk"].ToString(),
                                                objSQLReader["PrintingClerk"].ToString(),
                                                Functions.fnInt32(objSQLReader["CheckInClerk"].ToString()) == 0 ? "" : objSQLReader["CClerk"].ToString(),
                                                Functions.fnInt32(objSQLReader["PrintingClerk"].ToString()) == 0 ? "" : objSQLReader["PClerk"].ToString()});
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

        public bool DeleteProductionListHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ProductionListHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteProductionPrintBatch(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ProductionPrintBatch Where RefID = @ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteProductionList()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                /*string cs = FetchCurrentProductionListStatus(SaveComm);
                if (cs == "R")
                {
                    dtOrderDate = FetchProductionListDate(SaveComm);
                    AdjustStockJournal(SaveComm);
                }*/
                DeleteProductionListHeader(SaveComm, intID);
                DeleteProductionListDetails(SaveComm, intID);
                DeleteProductionPrintBatch(SaveComm, intID);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public DataTable FetchProductionListRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.*,isnull(e.FirstName,'') as FN, isnull(e.LastName,'') as LN,d.Qty as DQty,  "
                                + " d.Description as ProductName, d.SKU as SKU, dp.Description as Dpt "
                                + " from ProductionListHeader a left outer join ProductionListDetails d on a.ID = d.RefID "
                                + " left outer join Employee e on e.ID = a.CheckInClerk "
                                + " left outer join Dept dp on dp.ID = a.RefDept "
                                + " where a.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Ref", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DQty", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    double dblDetQty = 0;

                    if (objSQLReader["DQty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["DQty"].ToString());
                    }
                    


                    string strDateTime = "";
                    string emp = "";
                    if (objSQLReader["RefDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                    }
                    if (Functions.fnInt32(objSQLReader["CheckInClerk"].ToString()) > 0)
                    {
                        emp = objSQLReader["FN"].ToString() + " " + objSQLReader["LN"].ToString();
                    }


                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
                                                objSQLReader["RefNo"].ToString(),
												strDateTime,
                                                objSQLReader["Dpt"].ToString(),
                                                emp,
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["ProductName"].ToString(),
                                                dblDetQty.ToString()});
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


        public bool IsDuplicateProductListNo(int intRecID, string pNo, DateTime pDate)
        {
            int val = 0;
            string strSQLComm = "";
            if (intRecID == 0)
            {
                strSQLComm = " select count(*) as rcnt from ProductionListHeader where RefNo = @param1 and RefDate = @param2 ";
            }
            else
            {
                strSQLComm = " select count(*) as rcnt from ProductionListHeader where RefNo = @param1 and RefDate = @param2 and ID != @id ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@param1"].Value = pNo;
            objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@param2"].Value = pDate;
            if (intRecID > 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id"].Value = intRecID;
            }

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                
                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val > 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return true;
            }
        }


        public string GetProductionListStatus(int intRecID)
        {
            string val = "";
            string strSQLComm = "";

            strSQLComm = " select ListStatus from ProductionListHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                while (objSQLReader.Read())
                {
                    val = objSQLReader["ListStatus"].ToString();
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
                return "";
            }
        }

        public DataTable FetchPrintlabelData(string pscalecat)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (pscalecat == "") strSQLComm = " select ID, ProductID,SKU,Description,Qty from ProductionPrintBatch order by SKU ";
            else strSQLComm = " select ID, ProductID,SKU,Description,Qty from ProductionPrintBatch where ProductID in (select row_id from scale_product where cat_id = @cat ) order by SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            if (pscalecat != "")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@cat", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@cat"].Value = pscalecat;
            }
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("PLID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    int indx = objSQLReader["Qty"].ToString().IndexOf(".");
                    string qty = objSQLReader["Qty"].ToString().Remove(indx);
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   qty});
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

        public string UpdatePrintLabelQty(int pID, double pQty)
        {
            string strSQLComm = " update ProductionPrintBatch set Qty=@Qty,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@Qty"].Value = pQty;
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

        #region Random Inventory

        public DataTable ShowEmbeddedInventoryDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from EmbeddedInventoryDetails where RefID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));

                dtbl.Columns.Add("D", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("Embed", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                    objSQLReader["SKU"].ToString(),
                                                    Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("0.###"),
                                                    objSQLReader["Description"].ToString(),
                                                    objSQLReader["ProductID"].ToString(),
                                                    false,"N"
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

        public bool PostEmbeddedInventoryList()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                if (strMode == "Add")
                {
                    InsertEmbeddedInventoryHeader(SaveComm);
                }
                else
                {
                    UpdateEmbeddedInventoryHeader(SaveComm);
                }

                DeleteEmbeddedInventoryDetails(SaveComm, intID);
                AdjustEmbeddedInventoryRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertEmbeddedInventoryHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into EmbeddedInventoryHeader ( RefDate, GeneralNotes, CheckInClerk, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                                  + " values ( @RefDate, @GeneralNotes,@CheckInClerk, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public bool UpdateEmbeddedInventoryHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update EmbeddedInventoryHeader set RefDate=@RefDate, GeneralNotes=@GeneralNotes, "
                                  + " CheckInClerk=@CheckInClerk, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CheckInClerk"].Value = intCheckInClerk;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteEmbeddedInventoryDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from EmbeddedInventoryDetails where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustEmbeddedInventoryRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSKU = "";
                    string colDescription = "";
                    string colQty = "";

                    if (dr["ProductID"].ToString() == "") continue;
                    if (Convert.ToBoolean(dr["D"].ToString())) continue;
                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }

                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }

                    colDescription = dr["Description"].ToString();
                    strItemSKU = dr["SKU"].ToString();
                    dblItemQty = Functions.fnDouble(colQty);
                    strItemDescription = colDescription;
                    intItemSKU = Functions.fnInt32(colSKU);
                    InsertEmbeddedInventoryDetail(objSQlComm, intID);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertEmbeddedInventoryDetail(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into EmbeddedInventoryDetails ( RefID, ProductID, Description, SKU, Qty) "
                                  + " values ( @RefID, @ProductID,  @Description, @SKU,@Qty ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);

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

        public DataTable FetchEmbeddedInventoryHeader(DateTime pFDate, DateTime pTDate )
        {
            string sqlFilter1 = "";
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();

            string strSQLComm = " select a.ID, a.RefDate,  a.CheckInClerk, isnull(e.FirstName,'') as FN, isnull(e.LastName,'') as LN  "
                                + " from EmbeddedInventoryHeader a left outer join employee e on "
                                + " e.ID = a.CheckInClerk where (1 = 1) " + sqlFilter1
                                + " and a.RefDate between @FDT and @TDT "
                                + " order by a.RefDate desc ";



            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    string strDateTime = "";
                    string emp = "";
                    if (objSQLReader["RefDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                    }
                    if (Functions.fnInt32(objSQLReader["CheckInClerk"].ToString()) > 0)
                    {
                        emp = objSQLReader["FN"].ToString() + " " + objSQLReader["LN"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   strDateTime,
                                                   emp
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable ShowEmbeddedInventoryHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select h.*, e.FirstName + ' ' + e.LastName as CClerk from EmbeddedInventoryHeader h left outer join Employee e on e.ID = h.CheckInClerk where h.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GeneralNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Emp1", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["RefDate"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString(),
                                                objSQLReader["CheckInClerk"].ToString(),
                                                objSQLReader["CClerk"].ToString()});
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

        public bool DeleteEmbeddedInventoryHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from EmbeddedInventoryHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteEmbeddedInventory()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                DeleteEmbeddedInventoryHeader(SaveComm, intID);
                DeleteEmbeddedInventoryDetails(SaveComm, intID);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public DataTable FetchEmbeddedInventoryRecordForReport(int refType, DataTable refdtbl, DateTime pFDate, DateTime pTDate)
        {
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            string getdtblID = "";
            string strSQL1 = "";
            
            int chkcnt = 0;

            if (refType == 0)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {

                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = dr["ID"].ToString();
                        }
                        else
                        {
                            getdtblID = getdtblID + "," + dr["ID"].ToString();
                        }
                        chkcnt++;
                    }
                }
            }

            chkcnt = 0;
            if (refType == 1)
            {
                foreach (DataRow dr in refdtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"].ToString()))
                    {
                        if (getdtblID == "")
                        {
                            getdtblID = "'" + dr["Cat_ID"].ToString() + "'";
                        }
                        else
                        {
                            getdtblID = getdtblID + ",'" + dr["Cat_ID"].ToString() + "'";
                        }
                        chkcnt++;
                    }
                }
            }

            if (getdtblID != "")
            {
                if (refType == 0) strSQL1 = " and b.ID in ( " + getdtblID + " )";
                if (refType == 1) strSQL1 = " and b.Cat_ID in ( " + getdtblID + " )";
            }

            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (refType == 0)
            {
                strSQLComm =    " select a.Description, a.SKU, a.Qty, h.RefDate, b.DepartmentID as FilterCode, b.Description as FilterName "
                              + " from EmbeddedInventoryHeader h left outer join EmbeddedInventoryDetails a on h.ID = a.RefID "
                              + " left outer join Scale_Product s on a.ProductID = s.row_ID "
                              + " left outer join Product p on p.ID = s.ProductID "
                              + " left outer join dept b on p.DepartmentID = b.ID "
                              + " where (1 = 1) and h.RefDate between @D1 and @D2 " + strSQL1
                              + " order by FilterCode ";
            }

            if (refType == 1)
            {
                strSQLComm =    " select a.Description, a.SKU, a.Qty, h.RefDate, b.Cat_ID as FilterCode, isnull(b.Name,'') as FilterName "
                              + " from EmbeddedInventoryHeader h left outer join EmbeddedInventoryDetails a on h.ID = a.RefID "
                              + " left outer join Scale_Product s on a.ProductID = s.row_ID "
                              + " left outer join Product p on p.ID = s.ProductID "
                              + " left outer join scale_category b on s.Cat_ID = b.Cat_ID "
                              + " where (1 = 1) and h.RefDate between @D1 and @D2 " + strSQL1 
                              + " order by FilterCode ";
            }


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@D1", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@D1"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@D2", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@D2"].Value = pTDate;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    double dblQty = 0;

                    if (objSQLReader["Qty"].ToString() != "")
                    {
                        dblQty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    }



                    string strDateTime = "";

                    if (objSQLReader["RefDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                    }


                    dtbl.Rows.Add(new object[] {
                                                    objSQLReader["FilterCode"].ToString(),
                                                    objSQLReader["FilterName"].ToString(),
												    strDateTime,
                                                    objSQLReader["SKU"].ToString(),
                                                    objSQLReader["Description"].ToString(),
                                                    dblQty.ToString("0.###")});
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

        #endregion

        #region Customer Ordering

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
                dtbl.Columns.Add("TaxExempt", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountLevel", System.Type.GetType("System.String"));

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
                                                    objSQLReader["TaxExempt"].ToString(),
                                                    objSQLReader["DiscountLevel"].ToString()});

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

        public DataTable FetchProductRecord(int intRecID, string strPriceLevel)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from product where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            string pPrice = "";

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BinLocation", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PromptForPrice", System.Type.GetType("System.String"));

                dtbl.Columns.Add("AddtoPOSScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToPOSCategoryScreen", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnLayaway", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReorderQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrimaryVendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));

                dtbl.Columns.Add("CategoryID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintBarCode", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NoPriceOnLabel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyToPrint", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LabelType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MinimumAge", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {
                    if (strPriceLevel == "A") pPrice = objSQLReader["PriceA"].ToString();
                    if (strPriceLevel == "B") pPrice = objSQLReader["PriceB"].ToString();
                    if (strPriceLevel == "C") pPrice = objSQLReader["PriceC"].ToString();


                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["SKU"].ToString(),
                                                objSQLReader["SKU2"].ToString(),
												objSQLReader["Description"].ToString(),
                                                objSQLReader["BinLocation"].ToString(),
                                                objSQLReader["ProductType"].ToString(),
                                                objSQLReader["PriceA"].ToString(),
                                                objSQLReader["PriceB"].ToString(),
                                                objSQLReader["PriceC"].ToString(),
                                                objSQLReader["PromptForPrice"].ToString(),
                                                
                                                objSQLReader["AddtoPOSScreen"].ToString(),
                                                objSQLReader["AddToPOSCategoryScreen"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString(),
                                                objSQLReader["LastCost"].ToString(),
                                                objSQLReader["Cost"].ToString(),
                                                objSQLReader["QtyOnHand"].ToString(),
                                                objSQLReader["QtyOnLayaway"].ToString(),
                                                objSQLReader["ReorderQty"].ToString(),
                                                objSQLReader["NormalQty"].ToString(),
                                                objSQLReader["PrimaryVendorID"].ToString(),
                                                objSQLReader["DepartmentID"].ToString(),
                                                objSQLReader["CategoryID"].ToString(),
                                                objSQLReader["Points"].ToString(),
                                                objSQLReader["PrintBarCode"].ToString(),
                                                objSQLReader["NoPriceOnLabel"].ToString(),
                                                objSQLReader["QtyToPrint"].ToString(),
                                                objSQLReader["LabelType"].ToString(),
                                                objSQLReader["FoodStampEligible"].ToString(),
                                                objSQLReader["ProductNotes"].ToString(),
                                                objSQLReader["MinimumAge"].ToString(), 
                                                pPrice,
                                                objSQLReader["DecimalPlace"].ToString()

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

        public DataTable ShowOrderingDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from OrderDetails where RefID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("D", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("Rate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Net", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DiscountID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountText", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscLogic", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TaxID1", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("TaxID2", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("TaxID3", System.Type.GetType("System.Int32"));

                dtbl.Columns.Add("TaxName1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TaxRate1", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxRate2", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxRate3", System.Type.GetType("System.Double"));

                dtbl.Columns.Add("Taxable1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Tax1ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Tax2ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Tax3ID", System.Type.GetType("System.Int32"));

                dtbl.Columns.Add("TaxType1", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("TaxType2", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("TaxType3", System.Type.GetType("System.Int32"));

                dtbl.Columns.Add("TaxTotal1", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxTotal2", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxTotal3", System.Type.GetType("System.Double"));

                double dblQty = 0;
                double dblRate = 0;
                double dblDiscount = 0;
                double dblNet = 0;

                while (objSQLReader.Read())
                {
                    dblQty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    dblRate = Functions.fnDouble(objSQLReader["ItemRate"].ToString());
                    dblDiscount = Functions.fnDouble(objSQLReader["Discount"].ToString());
                    dblNet = (dblQty * dblRate) - dblDiscount;
                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                dblQty.ToString("0.##"),
                                                objSQLReader["Notes"].ToString(),
                                                false,
                                                dblRate.ToString("0.##"),
                                                dblDiscount.ToString("0.##"),
                                                dblNet.ToString("0.00"),
                                                objSQLReader["ProductID"].ToString(),
                                                objSQLReader["DiscountID"].ToString(),
                                                objSQLReader["DiscValue"].ToString(),
                                                objSQLReader["DiscountText"].ToString(),
                                                objSQLReader["DiscLogic"].ToString(),
                                                Functions.fnInt32(objSQLReader["TaxID1"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxID2"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxID3"].ToString()),
                                                "","","",
                                                Functions.fnDouble(objSQLReader["TaxRate1"].ToString()),
                                                Functions.fnDouble(objSQLReader["TaxRate2"].ToString()),
                                                Functions.fnDouble(objSQLReader["TaxRate3"].ToString()),
                                                objSQLReader["Taxable1"].ToString(),
                                                objSQLReader["Taxable2"].ToString(),
                                                objSQLReader["Taxable3"].ToString(),
                                                Functions.fnInt32(objSQLReader["TaxID1"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxID2"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxID3"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxType1"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxType2"].ToString()),
                                                Functions.fnInt32(objSQLReader["TaxType3"].ToString()),
                                                Functions.fnDouble(objSQLReader["TaxTotal1"].ToString()),
                                                Functions.fnDouble(objSQLReader["TaxTotal2"].ToString()),
                                                Functions.fnDouble(objSQLReader["TaxTotal3"].ToString())
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

        public bool PostOrderingList()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                if (strMode == "Add")
                {
                    InsertOrderingHeader(SaveComm);
                }
                else
                {
                    UpdateOrderingHeader(SaveComm);
                }

                DeleteOrderingDetails(SaveComm, intID);
                AdjustOrderingRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertOrderingHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into OrderHeader ( OrderDate, PickupDate, CustomerID, CDiscountLevel, CTaxExempt, "
                                  + " OrderItems, OrderGross, OrderDiscount, OrderTax, OrderNet, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                                  + " values ( @OrderDate, @PickupDate, @CustomerID,  @CDiscountLevel, @CTaxExempt, "
                                  + " @OrderItems, @OrderGross, @OrderDiscount, @OrderTax, @OrderNet,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@OrderDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@PickupDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CDiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CTaxExempt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderItems", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderGross", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderDiscount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderTax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderNet", System.Data.SqlDbType.Float));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@OrderDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@PickupDate"].Value = dtPickupDate;
                objSQlComm.Parameters["@CustomerID"].Value = intCustomerID;


                objSQlComm.Parameters["@CDiscountLevel"].Value = strCDiscountLevel;
                objSQlComm.Parameters["@CTaxExempt"].Value = strCTaxExempt;
                objSQlComm.Parameters["@OrderItems"].Value = intOrderItems;

                objSQlComm.Parameters["@OrderGross"].Value = dblGrossAmount;
                objSQlComm.Parameters["@OrderDiscount"].Value = dblDiscount;
                objSQlComm.Parameters["@OrderTax"].Value = dblTax;
                objSQlComm.Parameters["@OrderNet"].Value = dblNetAmount;
                
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public bool UpdateOrderingHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update OrderHeader set OrderDate=@OrderDate, PickupDate=@PickupDate, CustomerID=@CustomerID, "
                                  + " CDiscountLevel=@CDiscountLevel, CTaxExempt=@CTaxExempt, OrderItems=@OrderItems, OrderGross=@OrderGross, "
                                  + " OrderDiscount=@OrderDiscount, OrderTax=@OrderTax, OrderNet=@OrderNet, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@PickupDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CustomerID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CDiscountLevel", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CTaxExempt", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderItems", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderGross", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderDiscount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderTax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@OrderNet", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@OrderDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@PickupDate"].Value = dtPickupDate;
                objSQlComm.Parameters["@CustomerID"].Value = intCustomerID;
                objSQlComm.Parameters["@CDiscountLevel"].Value = strCDiscountLevel;
                objSQlComm.Parameters["@CTaxExempt"].Value = strCTaxExempt;
                objSQlComm.Parameters["@OrderItems"].Value = intOrderItems;
                objSQlComm.Parameters["@OrderGross"].Value = dblGrossAmount;
                objSQlComm.Parameters["@OrderDiscount"].Value = dblDiscount;
                objSQlComm.Parameters["@OrderTax"].Value = dblTax;
                objSQlComm.Parameters["@OrderNet"].Value = dblNetAmount;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteOrderingDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from OrderDetails where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustOrderingRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSKU = "";
                    string colDescription = "";
                    string colQty = "";
                    string colRATE = "";
                    string colTAXID1 = "";
                    string colTAXID2 = "";
                    string colTAXID3 = "";
                    string colTAXABLE1 = "";
                    string colTAXABLE2 = "";
                    string colTAXABLE3 = "";
                    string colTAXRATE1 = "";
                    string colTAXRATE2 = "";
                    string colTAXRATE3 = "";
                    string colDISCLOGIC = "";
                    string colDISCVALUE = "";
                    string colITEMDISCOUNT = "";
                    string colDISCOUNTID = "";
                    string colDISCOUNTTEXT = "";

                    string colTAXTYPE1 = "";
                    string colTAXTYPE2 = "";
                    string colTAXTYPE3 = "";
                    string colTAXTOTAL1 = "";
                    string colTAXTOTAL2 = "";
                    string colTAXTOTAL3 = "";



                    if (dr["ProductID"].ToString() == "") continue;
                    if (Convert.ToBoolean(dr["D"].ToString())) continue;
                    if (Functions.fnDouble(dr["Qty"].ToString()) <= 0) continue;

                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }

                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }

                    if (dr["Rate"].ToString() != "")
                    {
                        colRATE = dr["Rate"].ToString();
                    }
                    else
                    {
                        colRATE = "0.00";
                    }

                    if (dr["DiscValue"].ToString() != "")
                    {
                        colDISCVALUE = dr["DiscValue"].ToString();
                    }
                    else
                    {
                        colDISCVALUE = "0";
                    }

                    if (dr["Discount"].ToString() != "")
                    {
                        colITEMDISCOUNT = dr["Discount"].ToString();
                    }
                    else
                    {
                        colITEMDISCOUNT = "0";
                    }

                    if (dr["DiscountID"].ToString() != "")
                    {
                        colDISCOUNTID = dr["DiscountID"].ToString();
                    }
                    else
                    {
                        colDISCOUNTID = "0";
                    }


                    colDescription = dr["Description"].ToString();
                    colDISCLOGIC = dr["DiscLogic"].ToString();
                    colDISCOUNTTEXT = dr["DiscountText"].ToString();

                    colTAXID1 = dr["TaxID1"].ToString();
                    colTAXID2 = dr["TaxID2"].ToString();
                    colTAXID3 = dr["TaxID3"].ToString();

                    colTAXABLE1 = dr["Taxable1"].ToString();
                    colTAXABLE2 = dr["Taxable2"].ToString();
                    colTAXABLE3 = dr["Taxable3"].ToString();

                    colTAXRATE1 = dr["TaxRate1"].ToString();
                    colTAXRATE2 = dr["TaxRate2"].ToString();
                    colTAXRATE3 = dr["TaxRate3"].ToString();

                    colTAXTYPE1 = dr["TaxType1"].ToString();
                    colTAXTYPE2 = dr["TaxType2"].ToString();
                    colTAXTYPE3 = dr["TaxType3"].ToString();

                    colTAXTOTAL1 = dr["TaxTotal1"].ToString();
                    colTAXTOTAL2 = dr["TaxTotal2"].ToString();
                    colTAXTOTAL3 = dr["TaxTotal3"].ToString();



                    strItemSKU = dr["SKU"].ToString();
                    strItemNotes = dr["Notes"].ToString();
                    dblItemQty = Functions.fnDouble(colQty);
                    strItemDescription = colDescription;
                    intItemSKU = Functions.fnInt32(colSKU);

                    dblItemRate = Functions.fnDouble(colRATE);

                    intItemTaxID1 = Functions.fnInt32(colTAXID1);
                    intItemTaxID2 = Functions.fnInt32(colTAXID2);
                    intItemTaxID3 = Functions.fnInt32(colTAXID3);

                    strItemTaxable1 = colTAXABLE1;
                    strItemTaxable2 = colTAXABLE2;
                    strItemTaxable3 = colTAXABLE3;

                    dblItemTaxRate1 = Functions.fnDouble(colTAXRATE1);
                    dblItemTaxRate2 = Functions.fnDouble(colTAXRATE2);
                    dblItemTaxRate3 = Functions.fnDouble(colTAXRATE3);
                    
                    intItemTaxType1 = Functions.fnInt32(colTAXTYPE1);
                    intItemTaxType2 = Functions.fnInt32(colTAXTYPE2);
                    intItemTaxType3 = Functions.fnInt32(colTAXTYPE3);

                    dblItemTaxTotal1 = Functions.fnDouble(colTAXTOTAL1);
                    dblItemTaxTotal2 = Functions.fnDouble(colTAXTOTAL2);
                    dblItemTaxTotal3 = Functions.fnDouble(colTAXTOTAL3);

                    strItemDiscLogic = colDISCLOGIC;
                    strItemDiscountText = colDISCOUNTTEXT;
                    dblItemDiscount = Functions.fnDouble(colITEMDISCOUNT);
                    dblItemDiscValue = Functions.fnDouble(colDISCVALUE);
                    intItemDiscountID = Functions.fnInt32(colDISCOUNTID);


                    InsertOrderingDetail(objSQlComm, intID);
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool InsertOrderingDetail(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into OrderDetails ( RefID, ProductID, Description, SKU, Qty, ItemRate, Notes, "
                                  + " TaxID1,TaxID2,TaxID3,Taxable1,Taxable2,Taxable3,TaxRate1,TaxRate2,TaxRate3, "
                                  + " TaxType1,TaxType2,TaxType3,TaxTotal1,TaxTotal2,TaxTotal3, "
                                  + " Discount,DiscountID,DiscountText,DiscLogic,DiscValue ) "
                                  + " values ( @RefID, @ProductID,  @Description, @SKU, @Qty, @ItemRate, @Notes, "
                                  + " @TaxID1,@TaxID2,@TaxID3,@Taxable1,@Taxable2,@Taxable3,@TaxRate1,@TaxRate2,@TaxRate3, "
                                  + " @TaxType1,@TaxType2,@TaxType3,@TaxTotal1,@TaxTotal2,@TaxTotal3, "
                                  + " @Discount,@DiscountID,@DiscountText,@DiscLogic,@DiscValue ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@ItemRate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxID3", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Taxable1", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@Taxable2", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@Taxable3", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate1", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate3", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType2", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType3", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxTotal1", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxTotal2", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxTotal3", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscountText", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscLogic", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@Discount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DiscValue", System.Data.SqlDbType.Float));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@Notes"].Value = strItemNotes;
                objSQlComm.Parameters["@ItemRate"].Value = dblItemRate;
                objSQlComm.Parameters["@TaxID1"].Value = intItemTaxID1;
                objSQlComm.Parameters["@TaxID2"].Value = intItemTaxID2;
                objSQlComm.Parameters["@TaxID3"].Value = intItemTaxID3;
                objSQlComm.Parameters["@Taxable1"].Value = strItemTaxable1;
                objSQlComm.Parameters["@Taxable2"].Value = strItemTaxable2;
                objSQlComm.Parameters["@Taxable3"].Value = strItemTaxable3;
                objSQlComm.Parameters["@TaxRate1"].Value = dblItemTaxRate1;
                objSQlComm.Parameters["@TaxRate2"].Value = dblItemTaxRate2;
                objSQlComm.Parameters["@TaxRate3"].Value = dblItemTaxRate3;
                objSQlComm.Parameters["@TaxType1"].Value = intItemTaxType1;
                objSQlComm.Parameters["@TaxType2"].Value = intItemTaxType2;
                objSQlComm.Parameters["@TaxType3"].Value = intItemTaxType3;
                objSQlComm.Parameters["@TaxTotal1"].Value = dblItemTaxTotal1;
                objSQlComm.Parameters["@TaxTotal2"].Value = dblItemTaxTotal2;
                objSQlComm.Parameters["@TaxTotal3"].Value = dblItemTaxTotal3;
                objSQlComm.Parameters["@DiscountID"].Value = intItemDiscountID;
                objSQlComm.Parameters["@DiscountText"].Value = strItemDiscountText;
                objSQlComm.Parameters["@DiscLogic"].Value = strItemDiscLogic;
                objSQlComm.Parameters["@Discount"].Value = dblItemDiscount;
                objSQlComm.Parameters["@DiscValue"].Value = dblItemDiscValue;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);

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

        public DataTable FetchOrders(   string orderDateFilter, string pickupDateFilter, string customerFilter,
                                        DateTime orderFDate, DateTime orderTDate, DateTime pickupFDate, DateTime pickupTDate,
                                        int customerID, string custFNM, string custLNM, string custPhone    )
        {
            string sqlOrderFilter = "";
            string sqlPickupFilter = "";
            string sqlCustomerFilter = "";

            DateTime PckUpFromDate = new DateTime();
            DateTime PckUpToDate = new DateTime();

            DateTime OrdFromDate = new DateTime();
            DateTime OrdToDate = new DateTime();

            if (orderDateFilter == "Y")
            {
                OrdFromDate = new DateTime(orderFDate.Year, orderFDate.Month, orderFDate.Day, 0, 0, 0);
                OrdToDate = new DateTime(orderTDate.Year, orderTDate.Month, orderTDate.Day, 0, 0, 0);

                sqlOrderFilter = " and h.OrderDate between @orddt1 and @orddt2 ";
            }

            if (pickupDateFilter == "Y")
            {
                PckUpFromDate = new DateTime(pickupFDate.Year, pickupFDate.Month, pickupFDate.Day, 0, 0, 0);
                PckUpToDate = new DateTime(pickupTDate.Year, pickupTDate.Month, pickupTDate.Day, 0, 0, 0);

                sqlPickupFilter = " and h.PickupDate between @pkupdt1 and @pkupdt2 ";
            }

            if (customerFilter == "Y")
            {
                if (customerID == 0)
                {
                    if ((custFNM == "") && (custLNM == "") && (custPhone == ""))
                    {

                    }
                    else
                    {
                        if (custFNM != "")
                        {
                            sqlCustomerFilter = " c.FirstName like '%" + custFNM + "%' ";
                        }

                        if (custLNM != "")
                        {
                            sqlCustomerFilter = sqlCustomerFilter == "" ? " c.LastName like '%" + custLNM + "%' " : sqlCustomerFilter + " or c.LastName like '%" + custLNM + "%' ";
                        }

                        if (custPhone != "")
                        {
                            sqlCustomerFilter = sqlCustomerFilter == "" ? " c.MobilePhone like '%" + custPhone + "%' " : sqlCustomerFilter + " or c.MobilePhone like '%" + custPhone + "%' ";
                        }

                        sqlCustomerFilter = " and ( " + sqlCustomerFilter + " ) ";
                    }
                }
                else
                {
                    sqlCustomerFilter = " and h.CustomerID = @CID ";
                }
            }

            DataTable dtbl = new DataTable();

            string strSQLComm =   " select h.ID, h.OrderDate,h.PickupDate,h.OrderItems, h.OrderNet, c.FirstName + ' ' + c.LastName as cname, c.MobilePhone "
                                + " from OrderHeader h left outer join Customer c on c.ID = h.CustomerID where (1 = 1) " 
                                + sqlOrderFilter + sqlPickupFilter + sqlCustomerFilter
                                + " order by h.PickupDate, c.FirstName + ' ' + c.LastName ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            if (orderDateFilter == "Y")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@orddt1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@orddt1"].Value = OrdFromDate;
                objSQlComm.Parameters.Add(new SqlParameter("@orddt2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@orddt2"].Value = OrdToDate;
            }

            if (pickupDateFilter == "Y")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@pkupdt1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@pkupdt1"].Value = PckUpFromDate;
                objSQlComm.Parameters.Add(new SqlParameter("@pkupdt2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@pkupdt2"].Value = PckUpToDate;
            }

            if ((customerFilter == "Y") && (customerID > 0))
            {
                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CID"].Value = customerID;
            }
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PickupDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderItem", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderAmount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strODateTime = "";
                    string strPDateTime = "";

                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strODateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strODateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strODateTime = strODateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strODateTime = objSQLReader["OrderDate"].ToString();
                    }

                    if (objSQLReader["PickupDate"].ToString() != "")
                    {
                        strPDateTime = objSQLReader["PickupDate"].ToString();
                        int inIndex = strPDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strPDateTime = strPDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strPDateTime = objSQLReader["PickupDate"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   strODateTime,strPDateTime,
                                                   objSQLReader["cname"].ToString(),
                                                   objSQLReader["MobilePhone"].ToString(),
                                                   objSQLReader["OrderItems"].ToString(),
                                                   Functions.fnDouble(objSQLReader["OrderNet"].ToString()).ToString("0.##")
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable ShowOrderingHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from OrderHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PickupDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CDiscountLevel", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CTaxExempt", System.Type.GetType("System.String"));



                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["OrderDate"].ToString(),
                                                objSQLReader["PickupDate"].ToString(),
                                                objSQLReader["CustomerID"].ToString(),
                                                objSQLReader["CDiscountLevel"].ToString(),
                                                objSQLReader["CTaxExempt"].ToString()});
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

        public bool DeleteOrderingHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from OrderHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteOrdering()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                DeleteOrderingHeader(SaveComm, intID);
                DeleteOrderingDetails(SaveComm, intID);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public DataTable FetchReportDataForOrderingItems(string DateFilter, DateTime pFDate, DateTime pTDate)
        {
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 0, 0, 0);

            if (DateFilter == "O")
            {
                sqlFilter1 = " and h.OrderDate between @FDT and @TDT ";
            }

            if (DateFilter == "P")
            {
                sqlFilter1 = " and h.PickupDate between @FDT and @TDT ";
            }

            DataTable dtbl = new DataTable();

            string strSQLComm =   " select d.ProductID, d.SKU, d.Description, d.Qty, h.OrderDate, h.PickupDate, c.FirstName + ' ' + c.LastName as cnm "
                                + " from OrderHeader h left outer join OrderDetails d on d.RefID = h.ID left outer join customer c on "
                                + " c.ID = h.CustomerID where (1 = 1) " + sqlFilter1
                                + " order by d.Description, h.PickupDate desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;
           
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PickupDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GQty", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDateTime = "";
                    string strDateTime1 = "";

                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }
                    if (objSQLReader["PickupDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {    
                                                    objSQLReader["ProductID"].ToString(),
                                                    objSQLReader["SKU"].ToString(),
                                                    objSQLReader["Description"].ToString(),
                                                    Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("0.##"),
                                                    strDateTime,strDateTime1,
                                                    objSQLReader["cnm"].ToString(),
                                                    ""
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable FetchCustomerOrderRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select a.*,c.FirstName + ' ' + c.LastName as cnm,d.Qty as DQty,  "
                                + " c.Address1, c.Address2, c.City, c.Zip, c.State, c.Country, c.ShipAddress1, c.ShipAddress2, c.ShipCity, c.ShipZip, c.ShipState, c.ShipCountry, c.MobilePhone, "
                                + " d.Description as ProductName, d.SKU as SKU "
                                + " from OrderHeader a left outer join OrderDetails d on a.ID = d.RefID "
                                + " left outer join Customer c on c.ID = a.CustomerID "
                                + " where a.ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PickupDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerInfo", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    double dblDetQty = 0;

                    if (objSQLReader["DQty"].ToString() != "")
                    {
                        dblDetQty = Functions.fnDouble(objSQLReader["DQty"].ToString());
                    }



                    string strDateTime = "";
                    string strDateTime1 = "";
                    
                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }
                    if (objSQLReader["PickupDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                    }


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

                    if (objSQLReader["MobilePhone"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + "Ph. " + objSQLReader["MobilePhone"].ToString();
                        else
                            strMailAddress ="Ph. " +  objSQLReader["MobilePhone"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["ID"].ToString(),
												strDateTime,
                                                strDateTime1,
                                                objSQLReader["cnm"].ToString(),
                                                objSQLReader["OrderItems"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["ProductName"].ToString(),
                                                dblDetQty.ToString("0.##"),
                                                strMailAddress});
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


        public DataTable FetchReportDataForOrderingList(string DateFilter, DateTime pFDate, DateTime pTDate)
        {
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 0, 0, 0);

            if (DateFilter == "O")
            {
                sqlFilter1 = " and h.OrderDate between @FDT and @TDT ";
            }

            if (DateFilter == "P")
            {
                sqlFilter1 = " and h.PickupDate between @FDT and @TDT ";
            }

            DataTable dtbl = new DataTable();

            string strSQLComm = " select h.ID,  d.SKU, d.Description, d.Qty, h.OrderDate, h.PickupDate, c.FirstName + ' ' + c.LastName as cnm, "
                                + " c.Address1, c.Address2, c.City, c.Zip, c.State, c.Country, c.ShipAddress1, c.ShipAddress2, c.ShipCity, c.ShipZip, c.ShipState, c.ShipCountry, c.MobilePhone "
                                + " from OrderHeader h left outer join OrderDetails d on d.RefID = h.ID left outer join customer c on "
                                + " c.ID = h.CustomerID where (1 = 1) " + sqlFilter1
                                + " order by h.PickupDate desc, h.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OrderDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PickupDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerInfo", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strDateTime = "";
                    string strDateTime1 = "";

                    if (objSQLReader["OrderDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["OrderDate"].ToString();
                    }
                    if (objSQLReader["PickupDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["PickupDate"].ToString();
                    }

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

                    if (objSQLReader["MobilePhone"].ToString() != "")
                    {
                        if (strMailAddress != "")
                            strMailAddress = strMailAddress + "\n" + "Ph. " + objSQLReader["MobilePhone"].ToString();
                        else
                            strMailAddress = "Ph. " + objSQLReader["MobilePhone"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {    
                                                    objSQLReader["ID"].ToString(),
                                                    objSQLReader["SKU"].ToString(),
                                                    objSQLReader["Description"].ToString(),
                                                    Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("0.##"),
                                                    strDateTime,strDateTime1,
                                                    objSQLReader["cnm"].ToString(),
                                                    strMailAddress
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        #endregion

        #region Shelf Tag

        public DataTable ShowShelfTagDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from ShelfTagDetails where RefID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PrintLabelFlag", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("D", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {
                                                    objSQLReader["SKU"].ToString(),
                                                    Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString("0.##"),
                                                    objSQLReader["Description"].ToString(),
                                                    objSQLReader["ProductID"].ToString(),
                                                    objSQLReader["PrintLabelFlag"].ToString() == "Y" ? true : false,
                                                    false,
                                                    objSQLReader["Price"].ToString()
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

        public bool PostShelfTag()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                if (strMode == "Add")
                {
                    InsertShelfTagHeader(SaveComm);
                }
                else
                {
                    UpdateShelfTagHeader(SaveComm);
                }

                if (blbatchprintFlag) DeleteShelfTagBatch(SaveComm);
                DeleteShelfTagDetails(SaveComm, intID);

                AdjustShelfTagRecords(SaveComm);

                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        public bool InsertShelfTagHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " Insert Into ShelfTagHeader ( CreateType, CreateID, RefNo, RefDate, GeneralNotes, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn ) "
                                  + " values ( @CreateType, @CreateID, @RefNo, @RefDate, @GeneralNotes, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) "
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreateType", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CreateID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefNo"].Value = strOrderNo;
                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@CreateType"].Value = strCreateType;
                objSQlComm.Parameters["@CreateID"].Value = intCreateID;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public bool UpdateShelfTagHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update ShelfTagHeader set RefNo=@RefNo, RefDate=@RefDate, GeneralNotes=@GeneralNotes, "
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RefNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@RefDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@GeneralNotes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@RefNo"].Value = strOrderNo;
                objSQlComm.Parameters["@RefDate"].Value = dtOrderDate;
                objSQlComm.Parameters["@GeneralNotes"].Value = strGeneralNotes;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteShelfTagDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ShelfTagDetails where RefID = @ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;

                objSQlComm.ExecuteNonQuery();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustShelfTagRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colSKU = "";
                    string colDescription = "";
                    string colQty = "";
                    string colF = "";
                    string colRate = "";

                    if (dr["ProductID"].ToString() == "") continue;
                    if (Convert.ToBoolean(dr["D"].ToString())) continue;
                    if (dr["ProductID"].ToString() != "")
                    {
                        colSKU = dr["ProductID"].ToString();
                    }



                    if (dr["Qty"].ToString() != "")
                    {
                        colQty = dr["Qty"].ToString();
                    }
                    else
                    {
                        colQty = "0.00";
                    }


                    if (dr["Price"].ToString() != "")
                    {
                        colRate = dr["Price"].ToString();
                    }
                    else
                    {
                        colRate = "0.00";
                    }

                    sPrintlabelFlag = Convert.ToBoolean(dr["PrintLabelFlag"].ToString()) ? "Y" : "N";

                    colDescription = dr["Description"].ToString();
                    strItemSKU = dr["SKU"].ToString();
                    dblItemQty = Functions.fnDouble(colQty);
                    strItemDescription = colDescription;
                    intItemSKU = Functions.fnInt32(colSKU);
                    dblItemRate = Functions.fnDouble(colRate);

                    InsertShelfTagDetail(objSQlComm, intID);

                    if (sPrintlabelFlag == "Y")
                    {
                        if (blbatchprintFlag) InsertShelfTagPrintLabel(objSQlComm);
                    }
                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        private void DeleteShelfTagBatch(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from ShelfTagPrintBatch where RefID = @RefID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@RefID"].Value = intID;

                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        public void InsertShelfTagPrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert Into ShelfTagPrintBatch ( RefID, ProductID, Qty, SKU, Description, Price, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values ( @RefID, @ProductID, @Qty, @SKU, @Description, @Price, @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn ) ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(dblItemQty);//PrintLabelQty
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Price"].Value = dblItemRate;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                sqlDataReader = objSQlComm.ExecuteReader();
                sqlDataReader.Close();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
            }
        }

        public bool InsertShelfTagDetail(SqlCommand objSQlComm, int intRefID)
        {
            int intPODetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into ShelfTagDetails ( RefID, ProductID, Description, SKU, Qty, Price, PrintLabelFlag) "
                                  + " values ( @RefID, @ProductID,  @Description, @SKU,@Qty, @Price, @PrintLabelFlag ) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintLabelFlag", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@SKU"].Value = strItemSKU;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@Price"].Value = dblItemRate;
                objSQlComm.Parameters["@PrintLabelFlag"].Value = sPrintlabelFlag;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intPODetailID = Functions.fnInt32(sqlDataReader["ID"]);

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

        public DataTable FetchShelfTagHeader(DateTime pFDate, DateTime pTDate)
        {
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

           
            DataTable dtbl = new DataTable();

            string strSQLComm = " select RefNo, ID, RefDate, CreateType, CreateID from ShelfTagHeader where (1 = 1) " + sqlFilter1
                                + " and RefDate between @FDT and @TDT order by RefDate desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = pFDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = pTDate;
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CreateType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CreateID", System.Type.GetType("System.String"));
               
                while (objSQLReader.Read())
                {
                    string strDateTime = "";

                    if (objSQLReader["RefDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["RefDate"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["RefNo"].ToString(),
                                                   strDateTime,
                                                   objSQLReader["CreateType"].ToString(),
                                                   objSQLReader["CreateID"].ToString()
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

                strErrorMsg = SQLDBException.Message;
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return null;
            }
        }

        public DataTable ShowShelfTagHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = " select * from ShelfTagHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GeneralNotes", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["RefNo"].ToString(),
                                                objSQLReader["RefDate"].ToString(),
                                                objSQLReader["GeneralNotes"].ToString()});
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

        public bool DeleteShelfTagHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ShelfTagHeader Where ID = @ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteShelfTagPrintBatch(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from ShelfTagPrintBatch Where RefID = @ID";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
                objSQlComm.ExecuteNonQuery();

                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();

                return false;
            }
        }

        public bool DeleteShelfTag()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                
                DeleteShelfTagHeader(SaveComm, intID);
                DeleteShelfTagDetails(SaveComm, intID);
                DeleteShelfTagPrintBatch(SaveComm, intID);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public DataTable FetchShelfTagPrintlabelData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, ProductID,SKU,Description,Qty,Price from ShelfTagPrintBatch order by SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("PLID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    int indx = objSQLReader["Qty"].ToString().IndexOf(".");
                    string qty = objSQLReader["Qty"].ToString().Remove(indx);
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
												   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   qty,
                                                   objSQLReader["Price"].ToString() });
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

        public string UpdateShelfTagPrintLabelQty(int pID, double pQty)
        {
            string strSQLComm = " update ShelfTagPrintBatch set Qty=@Qty,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = pID;
                objSQlComm.Parameters["@Qty"].Value = pQty;
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

        public bool CheckShelfTagHeaderFromSaleBatch(int intRecID)
        {
            int val = 0;
            string strSQLComm = " select count(*) as rcnt from ShelfTagHeader where CreateID = @ID and CreateType = 'S'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val > 0;
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

        public int GetShelfTagHeaderIDFromSaleBatch(int intRecID)
        {
            int val = 0;
            string strSQLComm = " select ID from ShelfTagHeader where CreateID = @ID and CreateType = 'S'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["ID"].ToString());
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
                return 0;
            }
        }

        public bool CheckShelfTagHeaderFromFutureBatch(int intRecID)
        {
            int val = 0;
            string strSQLComm = " select count(*) as rcnt from ShelfTagHeader where CreateID = @ID and CreateType = 'F'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val > 0;
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

        public int GetShelfTagHeaderIDFromFutureBatch(int intRecID)
        {
            int val = 0;
            string strSQLComm = " select ID from ShelfTagHeader where CreateID = @ID and CreateType = 'F'";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["ID"].ToString());
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
                return 0;
            }
        }

        #endregion

    }
}

