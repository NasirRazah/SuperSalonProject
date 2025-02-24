/*
 purpose : Data for Receive Order
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Recv
    {
        #region definining private variables

        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strMode;
        private string strErrorMsg;

        private int    intVendorID;
        private string strPurchaseOrder;
        private string strInvoiceNo;
        private int strCheckInClerk;
        private int strReceivingClerk;
        private string strNote;

        private int intBatchID;
        private int intPurchaseOrderID;
        private int intEmployeeID;
        private int intRegisterID;
        private int intStoreID;

        private double dblInvoiceTotal;
        private double dblFreight;
        private double dblGrossAmount;
        private double dblTax;
        private bool blVendorUpdate;

        private DateTime dtDateOrdered;
        private DateTime dtReceiveDate;

        private double dblItemFreight;
        private double dblItemQty;
        private double dblItemCost;
        private string strItemPrintLabel;
        private string strItemVendorPartNo;
        private string strItemDescription;
        private int intItemSKU;
        private string strItemNotes;
        private double dblItemTax;
        private int intItemCaseQty;
        private string strItemCaseUPC;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        private DataTable dblInitialDataTable;
        private DataTable dblDataTableForPrintLabel;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private string strConnString;

        private string strTerminalName;

        private int PrintLabelQty = 0;
        private bool blIfExistInPrintLabel = false;

       

        #endregion

        #region definining public variables

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

        public string ConnString
        {
            get { return strConnString; }
            set { strConnString = value; }
        }

        public bool VendorUpdate
        {
            get { return blVendorUpdate; }
            set { blVendorUpdate = value; }
        }

        

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
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

        public int  VendorID
        {
            get { return intVendorID; }
            set { intVendorID = value; }
        }

        public string PurchaseOrder
        {
            get { return strPurchaseOrder; }
            set { strPurchaseOrder = value; }
        }

        public string InvoiceNo
        {
            get { return strInvoiceNo; }
            set { strInvoiceNo = value; }
        }

        public int CheckInClerk
        {
            get { return strCheckInClerk; }
            set { strCheckInClerk = value; }
        }

        public int ReceivingClerk
        {
            get { return strReceivingClerk; }
            set { strReceivingClerk = value; }
        }

        public string Note
        {
            get { return strNote; }
            set { strNote = value; }
        }

        public int BatchID
        {
            get { return intBatchID; }
            set { intBatchID = value; }
        }

        public int PurchaseOrderID
        {
            get { return intPurchaseOrderID; }
            set { intPurchaseOrderID = value; }
        }

        public int EmployeeID
        {
            get { return intEmployeeID; }
            set { intEmployeeID = value; }
        }

        public int RegisterID
        {
            get { return intRegisterID; }
            set { intRegisterID = value; }
        }

        public int StoreID
        {
            get { return intStoreID; }
            set { intStoreID = value; }
        }


        public double InvoiceTotal
        {
            get { return dblInvoiceTotal; }
            set { dblInvoiceTotal = value; }
        }

        public double Freight
        {
            get { return dblFreight; }
            set { dblFreight = value; }
        }

        public double GrossAmount
        {
            get { return dblGrossAmount; }
            set { dblGrossAmount = value; }
        }

        public double Tax
        {
            get { return dblTax; }
            set { dblTax = value; }
        }

        public DateTime DateOrdered
        {
            get { return dtDateOrdered; }
            set { dtDateOrdered = value; }
        }

        public DateTime ReceiveDate
        {
            get { return dtReceiveDate; }
            set { dtReceiveDate = value; }
        }


        public string TerminalName
        {
            get { return strTerminalName; }
            set { strTerminalName = value; }
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

        public bool InsertRecv()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;

                dblDataTableForPrintLabel = new DataTable();
                dblDataTableForPrintLabel.Columns.Add("ITEMID", System.Type.GetType("System.String"));
                if (strMode == "Add")
                {
                    InsertRecvHeader(SaveComm);
                    UpdateRecvBatchNo(SaveComm);
                }
                else
                {
                    UpdateRecvHeader(SaveComm);
                }
                if (strMode == "Edit")
                {
                    AdjustStockJournal(SaveComm);
                    AdjustPreviousRecord(SaveComm);
                    DeleteRecvDetails(SaveComm, intID);
                    //DeleteStockJournal(SaveComm, intID);
                }

                AdjustSplitGridRecords(SaveComm);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                string sss = SQLDBException.ToString(); 
                return false;
            }
        }

        #region Insert Recv Header Data
        public bool InsertRecvHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = "Insert Into RecvHeader ( BatchID,DateTimeStamp,VendorID,PurchaseOrder,"
                    + " PurchaseOrderID, InvoiceNo, InvoiceTotal, Freight, GrossAmount, Tax, DateOrdered, "
                    + " CheckInClerk, ReceivingClerk, Note, EmployeeID, RegisterID,StoreID,Posted,ReceiveDate, "
                    + "CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                    + "Values ( @BatchID,@DateTimeStamp,@VendorID,@PurchaseOrder,"
                    + " @PurchaseOrderID, @InvoiceNo, @InvoiceTotal, @Freight,@GrossAmount,@Tax, @DateOrdered, "
                    + " @CheckInClerk, @ReceivingClerk,@Note,@EmployeeID,@RegisterID,@StoreID,@Posted,@ReceiveDate, "
                    + "@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn )"
                    + "select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@BatchID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DateTimeStamp", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PurchaseOrder", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PurchaseOrderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@InvoiceNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@InvoiceTotal", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@GrossAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOrdered", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceivingClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Note", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RegisterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Posted", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiveDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));


                objSQlComm.Parameters["@BatchID"].Value = intBatchID;
                objSQlComm.Parameters["@DateTimeStamp"].Value = DateTime.Now;
                objSQlComm.Parameters["@VendorID"].Value = intVendorID;
                objSQlComm.Parameters["@PurchaseOrder"].Value = strPurchaseOrder;
                objSQlComm.Parameters["@PurchaseOrderID"].Value = intPurchaseOrderID;
                objSQlComm.Parameters["@InvoiceNo"].Value = strInvoiceNo;
                objSQlComm.Parameters["@InvoiceTotal"].Value = dblInvoiceTotal;
                objSQlComm.Parameters["@Freight"].Value = dblFreight;
                objSQlComm.Parameters["@GrossAmount"].Value = dblGrossAmount;
                objSQlComm.Parameters["@Tax"].Value = dblTax;
                if (dtDateOrdered == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOrdered"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOrdered"].Value = dtDateOrdered;
                }
                
                objSQlComm.Parameters["@CheckInClerk"].Value = strCheckInClerk;
                objSQlComm.Parameters["@ReceivingClerk"].Value = strReceivingClerk;
                objSQlComm.Parameters["@Note"].Value = strNote;
                objSQlComm.Parameters["@EmployeeID"].Value = intEmployeeID;
                objSQlComm.Parameters["@RegisterID"].Value = intRegisterID;
                objSQlComm.Parameters["@StoreID"].Value = intStoreID;
                objSQlComm.Parameters["@Posted"].Value = "Y";
                objSQlComm.Parameters["@ReceiveDate"].Value = dtReceiveDate;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;


                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intID = Functions.fnInt32(sqlDataReader["ID"]);
                sqlDataReader.Close();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return false;
            }
        }
        #endregion

        #region Update Tax Header Data
        public string UpdateRecvHeader(SqlCommand objSQlComm)
        {

            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " update RecvHeader set "
                                  + " DateTimeStamp=@DateTimeStamp,VendorID=@VendorID,PurchaseOrder=@PurchaseOrder,"
                                  + " PurchaseOrderID=@PurchaseOrderID, InvoiceNo=@InvoiceNo, "
                                  + " InvoiceTotal=@InvoiceTotal, Freight=@Freight,GrossAmount=@GrossAmount,Tax=@Tax,DateOrdered=@DateOrdered, "
                                  + " CheckInClerk=@CheckInClerk, ReceivingClerk=@ReceivingClerk,ReceiveDate=@ReceiveDate, "
                                  + " Note=@Note, EmployeeID=@EmployeeID, RegisterID=@RegisterID, StoreID=@StoreID, "
                                  + " LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn "
                                  + " Where ID = @ID ";
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@DateTimeStamp", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@PurchaseOrder", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@PurchaseOrderID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@InvoiceNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@InvoiceTotal", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@GrossAmount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@DateOrdered", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@CheckInClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceivingClerk", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Note", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@RegisterID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@StoreID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ReceiveDate", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DateTimeStamp"].Value = DateTime.Now;
                objSQlComm.Parameters["@VendorID"].Value = intVendorID;
                objSQlComm.Parameters["@PurchaseOrder"].Value = strPurchaseOrder;
                objSQlComm.Parameters["@PurchaseOrderID"].Value = intPurchaseOrderID;
                objSQlComm.Parameters["@InvoiceNo"].Value = strInvoiceNo;
                objSQlComm.Parameters["@InvoiceTotal"].Value = dblInvoiceTotal;
                objSQlComm.Parameters["@Freight"].Value = dblFreight;
                objSQlComm.Parameters["@GrossAmount"].Value = dblGrossAmount;
                objSQlComm.Parameters["@Tax"].Value = dblTax;

                if (dtDateOrdered == Convert.ToDateTime(null))
                {
                    objSQlComm.Parameters["@DateOrdered"].Value = Convert.DBNull;
                }
                else
                {
                    objSQlComm.Parameters["@DateOrdered"].Value = dtDateOrdered;
                }
                objSQlComm.Parameters["@CheckInClerk"].Value = strCheckInClerk;
                objSQlComm.Parameters["@ReceivingClerk"].Value = strReceivingClerk;
                objSQlComm.Parameters["@Note"].Value = strNote;
                objSQlComm.Parameters["@EmployeeID"].Value = intEmployeeID;
                objSQlComm.Parameters["@RegisterID"].Value = intRegisterID;
                objSQlComm.Parameters["@StoreID"].Value = intStoreID;
                objSQlComm.Parameters["@ReceiveDate"].Value = dtReceiveDate;

                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.ExecuteNonQuery();
                objSQlComm.Dispose();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                objSQLTran.Rollback();
                objSQlComm.Dispose();
                strErrorMsg = SQLDBException.Message;
                return strErrorMsg;
            }

        }
        #endregion

        #region Fetch Recv Header data

        public DataTable FetchRecvHeader(int pVendor, DateTime pFDate, DateTime pTDate, string pPO)
        {
            DataTable dtbl = new DataTable();

            string sqlFilter = "";
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 1);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            if (pVendor != 0)
            {
                sqlFilter = " and a.VendorID = @Vendor";
            }

            if (pPO != "")
            {
                sqlFilter1 = " and a.PurchaseOrder like '%" + pPO + "%' ";
            }

            string strSQLComm = " select a.ID,BatchID,a.VendorID,PurchaseOrder,InvoiceNo,InvoiceTotal,GrossAmount,Tax, "
                                + " Freight, DateOrdered, ReceiveDate,b.Name as VendorName "
                                + " From RecvHeader a left outer join Vendor b on a.VendorID = b.ID where (1 = 1) "
                                + " and ReceiveDate between @FDT and @TDT " + sqlFilter + sqlFilter1
                                + " order by ReceiveDate Desc, BatchID desc ";


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
                dtbl.Columns.Add("BatchID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PurchaseOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOrdered", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceiveDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("InvoiceTotal", System.Type.GetType("System.Double"));
                
                while (objSQLReader.Read())
                {

                    string strDateTime = "";
                    string strDateTime1 = "";
                    if (objSQLReader["DateOrdered"].ToString() != "")
                    {
                        strDateTime = objSQLReader["DateOrdered"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["DateOrdered"].ToString();
                    }

                    if (objSQLReader["ReceiveDate"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["ReceiveDate"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime1 = objSQLReader["ReceiveDate"].ToString();
                    }
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["BatchID"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["VendorName"].ToString(),
                                                   objSQLReader["PurchaseOrder"].ToString(),
                                                   objSQLReader["InvoiceNo"].ToString(),
                                                   strDateTime,strDateTime1,
                                                   Functions.fnDouble(objSQLReader["GrossAmount"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Freight"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Tax"].ToString()),
                                                   Functions.fnDouble(objSQLReader["InvoiceTotal"].ToString())});
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {

                strErrorMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
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
                    string colPrintLabels = "";
                    string colNotes = "";
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

                    if (Convert.ToBoolean(dr["PrintLabels"].ToString()) == true)
                    {
                        colPrintLabels = "Y";
                    }
                    else
                    {
                        colPrintLabels = "N";
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
                    colNotes = dr["Notes"].ToString();

                    dblItemCost = Functions.fnDouble(colCost);
                    dblItemFreight = Functions.fnDouble(colFreight);
                    dblItemQty = Functions.fnDouble(colQty);
                    dblItemTax = Functions.fnDouble(colTax);
 
                    strItemPrintLabel = colPrintLabels;
                    strItemDescription = colDescription;
                    strItemVendorPartNo = colVendorPartNo;
                    intItemSKU = Functions.fnInt32(colSKU);
                    intItemCaseQty = Functions.fnInt32(colCaseQty);
                    strItemCaseUPC = colCaseUPC;

                    strItemNotes = colNotes;

                    double stkqty = 0;
                    if (intItemCaseQty == 0) stkqty = dblItemQty;
                    if (intItemCaseQty > 0) stkqty = dblItemQty * intItemCaseQty;

                    Functions.UpdateItemStockBalance(objSQlComm, intItemSKU, stkqty, intLoginUserID);
                    InsertRecvDetails(objSQlComm, intID);
                    int SJ = Functions.AddStockJournal(objSQlComm, stkqty >= 0 ? "Stock In" : "Stock Out", stkqty >= 0 ? "Receiving" : "Return", "N", intItemSKU,
                        intLoginUserID, stkqty >= 0 ? stkqty : -stkqty, dblItemCost, intID.ToString(), 
                                strTerminalName, dtReceiveDate, DateTime.Now, "");

                    double iqty = 0;
                    double icost = 0;
                    iqty = GetItemQty(objSQlComm, intItemSKU);
                    icost = GetItemCost(objSQlComm, intItemSKU);
                    if (iqty != 0)
                        UpdateItemCost(objSQlComm, intItemSKU, (((iqty - stkqty) * icost) + (dblItemCost * dblItemQty)) / iqty, intItemCaseQty == 0 ? dblItemCost : dblItemCost / intItemCaseQty, intLoginUserID);

                    if (strItemPrintLabel == "Y")
                    {
                        //PrintLabelQty = FetchQtyToPrint(objSQlComm, intItemSKU);
                        //if (PrintLabelQty > 0)
                        //{
                            bool blProceed = true;
                            foreach (DataRow dr1 in dblDataTableForPrintLabel.Rows)
                            {
                                if (Functions.fnInt32(dr1["ITEMID"].ToString()) != intItemSKU ) continue;
                                if (Functions.fnInt32(dr1["ITEMID"].ToString()) == intItemSKU )
                                {
                                    blProceed = false;
                                    break;
                                }
                            }
                            if (blProceed)
                            {
                                blIfExistInPrintLabel = IfExistInPrintLabel(objSQlComm, intItemSKU);
                                if (blIfExistInPrintLabel)
                                {
                                    UpdatePrintLabel(objSQlComm);
                                }
                                else InsertPrintLabel(objSQlComm);

                                UpdatePrintQty(objSQlComm);
                            }
                        //}
                    }

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeletePrintLabelForRecvUpdate(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from printlabel where productid = @ID ";

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
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool UpdatePrintQtyForRecvUpdate(SqlCommand objSQlComm, int intRefQty, double intRefID)
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

        public bool DeleteStockJournal(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from stockjournal Where docno = @ID and transubtype = 'Receiving' ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@ID"].Value = intRefID.ToString();
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

        public bool DeleteRecvDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from RecvDetail where BatchNo = @ID";

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
                SaveTran.Rollback();
                objSQlComm.Dispose();
                return false;
            }
        }

        public bool InsertRecvDetails(SqlCommand objSQlComm, int intRefID)
        {
            int intRecvDetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = "insert into RecvDetail (BatchNo,ProductID,VendorPartNo,Description, "
                    + " Cost, Qty, Freight, PrintLabels, Tax, Notes,CaseQty,CaseUPC, "
                    + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                    + " values (@BatchNo,@SKU,@VendorPartNo,@Description, "
                    + " @Cost, @Qty, @Freight, @PrintLabels, @Tax, @Notes,@CaseQty,@CaseUPC,"
                    + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                    + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@BatchNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@VendorPartNo", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Freight", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@PrintLabels", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseQty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CaseUPC", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@BatchNo"].Value = intRefID;
                objSQlComm.Parameters["@SKU"].Value = intItemSKU;
                objSQlComm.Parameters["@VendorPartNo"].Value = strItemVendorPartNo;
                objSQlComm.Parameters["@Description"].Value = strItemDescription;
                objSQlComm.Parameters["@Cost"].Value = dblItemCost;
                objSQlComm.Parameters["@Qty"].Value = dblItemQty;
                objSQlComm.Parameters["@Freight"].Value = dblItemFreight;
                objSQlComm.Parameters["@PrintLabels"].Value = strItemPrintLabel;
                objSQlComm.Parameters["@Tax"].Value = dblItemTax;
                objSQlComm.Parameters["@Notes"].Value = strItemNotes;
                objSQlComm.Parameters["@CaseQty"].Value = intItemCaseQty;
                objSQlComm.Parameters["@CaseUPC"].Value = strItemCaseUPC;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();
                if (sqlDataReader.Read()) intRecvDetailID = Functions.fnInt32(sqlDataReader["ID"]);
                sqlDataReader.Close();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
                sqlDataReader.Close();
                return false;
            }
        }

        #region Show Header Record based on ID
        public DataTable ShowHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select * from RecvHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BatchID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PurchaseOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PurchaseOrderID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceTotal", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));

                dtbl.Columns.Add("DateOrdered", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceivingClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Note", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceiveDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["BatchID"].ToString(),
                                                objSQLReader["VendorID"].ToString(),
                                                objSQLReader["PurchaseOrder"].ToString(),
                                                objSQLReader["PurchaseOrderID"].ToString(),
                                                objSQLReader["InvoiceNo"].ToString(),
                                                objSQLReader["InvoiceTotal"].ToString(),
                                                objSQLReader["Freight"].ToString(),
                                                objSQLReader["GrossAmount"].ToString(),
                                                objSQLReader["Tax"].ToString(),
                                                objSQLReader["DateOrdered"].ToString(),
                                                objSQLReader["CheckInClerk"].ToString(),
                                                objSQLReader["ReceivingClerk"].ToString(),
                                                objSQLReader["Note"].ToString(),
                                                objSQLReader["ReceiveDate"].ToString()});
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

        #region Show Detail Record based on ID
        public DataTable ShowDetailRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select RD.*,RD.ID as Line, P.ProductType,p.ScaleBarCode from RecvDetail RD left outer join Product P on RD.ProductID = P.ID where RD.BatchNo = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BatchNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPartNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("PrintLabels", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));

                bool blLabel = false;
                double dblExtCost = 0;
                double dblDetQty = 0;
                double dblDetCost = 0;
                double dblDetTax = 0;
                double dblDetFreight = 0;
                int intDetCaseQty = 0;
                while (objSQLReader.Read())
                {
                    dblExtCost = 0;
                    dblDetQty = 0;
                    dblDetCost = 0;
                    dblDetTax = 0;
                    dblDetFreight = 0;
                    intDetCaseQty = 0;

                    if (objSQLReader["PrintLabels"].ToString() == "Y")
                    {
                        blLabel = true;
                    }
                    else
                    {
                        blLabel = false;
                    }
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
                                                objSQLReader["Line"].ToString(),
												objSQLReader["BatchNo"].ToString(),
                                                objSQLReader["ProductID"].ToString(),
                                                objSQLReader["VendorPartNo"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                dblDetCost,
                                                dblDetQty,
                                                dblDetFreight,
                                                dblDetTax,
                                                dblExtCost,
                                                blLabel,
                                                objSQLReader["Notes"].ToString(),
                                                intDetCaseQty,
                                                objSQLReader["CaseUPC"].ToString(),
                                                 objSQLReader["ProductType"].ToString(),
                                                objSQLReader["ScaleBarCode"].ToString()});
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

        private void UpdateRecvBatchNo(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Update RecvHeader set BatchID = @BatchID where ID=@ID  ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@BatchID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BatchID"].Value = intID;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.ToString();
                SaveTran.Rollback();
                objSQlComm.Dispose();
            }
        }

        public bool DeleteRecv()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                dblDataTableForPrintLabel = new DataTable();
                dblDataTableForPrintLabel.Columns.Add("ITEMID", System.Type.GetType("System.String"));
                DeleteRecvHeader(SaveComm, intID);
                AdjustPreviousStock(SaveComm);
                AdjustStockJournalBeforeDelete(SaveComm);
                DeleteRecvDetails(SaveComm, intID);
                return true;
            }
            catch (SqlException SQLDBException)
            {
                return false;
            }
        }

        public bool DeleteRecvHeader(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " Delete from RecvHeader Where BatchID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = intRefID;
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

        #region Update Vendor
        public string UpdateVendor(SqlConnection pConn,int strpVendor, int strpSKU, string strpPart, double fltPrice)
        {
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_UpdateVendor";

            try
            {

                objSQlComm = new SqlCommand(strSQLComm, pConn);
                if (pConn.State == System.Data.ConnectionState.Closed) { pConn.Open(); }
                objSQLTran = pConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SKU"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@SKU"].Value = strpSKU;

                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VendorID"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@VendorID"].Value = strpVendor;

                objSQlComm.Parameters.Add(new SqlParameter("@PartNumber", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@PartNumber"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@PartNumber"].Value = strpPart;

                objSQlComm.Parameters.Add(new SqlParameter("@Price", System.Data.SqlDbType.Float));
                objSQlComm.Parameters["@Price"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Price"].Value = fltPrice;

                objSQlComm.Parameters.Add(new SqlParameter("@User", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@User"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@User"].Value = intLoginUserID;

                objSQlComm.ExecuteNonQuery();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                pConn.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                pConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                pConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }
        #endregion

        public int FetchPrimaryVendor(SqlConnection pConn, int strpSKU, int strpVendor)
        {
            int strV = 0;
            string strSQLComm =   " select VendorID from VendPart where IsPrimary = 'Y' and ProductID=@SKU and VendorID <> @VendorID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, pConn);
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SKU"].Value = strpSKU;
                objSQlComm.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@VendorID"].Value = strpVendor;

                if (pConn.State == System.Data.ConnectionState.Closed) { pConn.Open(); }

                objSQLTran = pConn.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        strV = Functions.fnInt32(objsqlReader["VendorID"].ToString());
                    }
                    catch { objsqlReader.Close(); }
                    
                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                pConn.Close();
                return strV;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                pConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return 0;
            }
            finally
            {
                pConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #region Show Header Record based on ID ( Report)

        public DataTable FetchHeaderRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm =   " select a.*, v.Name, v.Address1, v.Address2, v.City, v.Zip, v.State, v.Country, "
                                + " v.Contact, v.Phone, v.Fax, v.EMail,isnull(p.PriceA,0) as PriceA, "
                                + " d.Qty as DQty, d.Cost as DCost, d.Freight as DFreight, d.Tax as DTax, "
                                + " d.Description as ProductName, d.VendorPartNo, "
                                + " e.FirstName as CheckClerk1, e.LastName as CheckClerk2, "
                                + " f.FirstName as RecvClerk1, f.LastName as RecvClerk2 "
                                + " from RecvHeader a left outer join Vendor v on a.VendorID = v.ID "
                                + " left outer join RecvDetail d on a.ID = d.BatchNo "
                                + " left outer join Product p on p.ID = d.ProductID "
                                + " left outer join Employee e on a.CheckInClerk = e.ID "
                                + " left outer join Employee f on a.ReceivingClerk = f.ID "
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
                dtbl.Columns.Add("PurchaseOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateOrdered", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceiveDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GrossAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceTotal", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Note", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CheckInClerk", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ReceivingClerk", System.Type.GetType("System.String"));
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
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.String"));

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
                        strAddress = strAddress + objSQLReader["Address2"].ToString() + "\n";
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
                        strAddress = strAddress + objSQLReader["Country"].ToString() + "\n";
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
                    if (objSQLReader["DateOrdered"].ToString() != "")
                    {
                        strDateTime1 = objSQLReader["DateOrdered"].ToString();
                        int inIndex = strDateTime1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime1 = strDateTime1.Substring(0, inIndex).Trim();
                        }
                    }
                    string strDateTime2 = "";
                    if (objSQLReader["ReceiveDate"].ToString() != "")
                    {
                        strDateTime2 = objSQLReader["ReceiveDate"].ToString();
                        int inIndex = strDateTime2.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime2 = strDateTime2.Substring(0, inIndex).Trim();
                        }
                    }


                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["PurchaseOrder"].ToString(),
                                                strDateTime1,
                                                objSQLReader["VendorID"].ToString(),
                                                objSQLReader["InvoiceNo"].ToString(),
                                                strDateTime2,
                                                objSQLReader["GrossAmount"].ToString(),
                                                objSQLReader["Freight"].ToString(),
                                                objSQLReader["Tax"].ToString(),
                                                objSQLReader["InvoiceTotal"].ToString(),
                                                objSQLReader["Note"].ToString(),
                                                objSQLReader["CheckClerk1"].ToString() + " " + objSQLReader["CheckClerk2"].ToString(),
                                                objSQLReader["RecvClerk1"].ToString() + " " + objSQLReader["RecvClerk2"].ToString(),
                                                objSQLReader["Name"].ToString(),
                                                strAddress,
                                                strOtherDetails,
                                                objSQLReader["ProductName"].ToString(),
                                                objSQLReader["VendorPartNo"].ToString(),
                                                dblDetQty.ToString(),
                                                dblDetCost.ToString(),
                                                dblDetFreight.ToString(),
                                                dblDetTax.ToString(),
                                                dblExtCost.ToString(),
                                                objSQLReader["PriceA"].ToString()});
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
        
        public DataTable FetchVendorRecordForReport(int intRecID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm =   " select  v.Name, v.Address1, v.Address2, v.City, v.Zip, v.State, v.Country, "
                                + " v.Contact, v.Phone, v.Fax, v.EMail from Vendor v where v.ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

               
                dtbl.Columns.Add("VAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VOthers", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    string strAddress = "";
                    string strOtherDetails = "";

                    if (objSQLReader["Address1"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["Address1"].ToString() + "\n";
                    }
                    if (objSQLReader["Address2"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["Address2"].ToString() + "\n";
                    }
                    if (objSQLReader["City"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["City"].ToString() + "\n";
                    }
                    if (objSQLReader["Zip"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["Zip"].ToString() + "\n";
                    }
                    if (objSQLReader["State"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["State"].ToString() + "\n";
                    }
                    if (objSQLReader["Country"].ToString() != "")
                    {
                        strAddress = strAddress + objSQLReader["Country"].ToString() + "\n";
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

                    dtbl.Rows.Add(new object[] {
                                                strAddress,
                                                strOtherDetails});
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

        public string GetCustomer(int pID)
        {
            string strresult = "";
            string strSQLComm = "select FirstName, LastName from Employee where ID = @ID";

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
                    strresult = objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString();
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return strresult;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return "";
            }
        }

        #endregion

        private bool AdjustPreviousRecord(SqlCommand AdjustSQlComm)
        {

            bool boolResult = false;
            AdjustSQlComm.Parameters.Clear();
            string strSQLComm;

            strSQLComm = " select ProductID as ITEMID,Qty,Cost,CaseQty from RecvDetail where BatchNo = @BatchNo ";

            SqlConnection objSQLCon = new SqlConnection(strConnString);

            if (objSQLCon.State == System.Data.ConnectionState.Open) { objSQLCon.Close(); }
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@BatchNo", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@BatchNo"].Value = intID;

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

                    DeletePrintLabelForRecvUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()));
                    UpdatePrintQtyForRecvUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()),
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

        private bool AdjustPreviousStock(SqlCommand AdjustSQlComm)
        {
            
            bool boolResult = false;
            AdjustSQlComm.Parameters.Clear();
            string strSQLComm;

            strSQLComm = " select ProductID as ITEMID,Qty,Cost,CaseQty from RecvDetail where BatchNo = @BatchNo ";

            SqlConnection objSQLCon = new SqlConnection(strConnString);

            if (objSQLCon.State == System.Data.ConnectionState.Open) { objSQLCon.Close(); }
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@BatchNo", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@BatchNo"].Value = intID;

            SqlDataReader objSQLReader = null;
            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                
                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {

                    if (Functions.fnInt32(objSQLReader["CaseQty"].ToString()) == 0)
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
                    }
                    DeletePrintLabelForRecvUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()));
                    UpdatePrintQtyForRecvUpdate(AdjustSQlComm, Functions.fnInt32(objSQLReader["ITEMID"].ToString()),
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
                    string colCOST = "";
                    string colCASEQTY = "";

                    if (dr["ITEMID"].ToString() == "") continue;

                    if (dr["ITEMID"].ToString() != "")
                    {
                        colITEMID = dr["ITEMID"].ToString();
                    }

                    if (dr["COST"].ToString() != "")
                    {
                        colCOST = dr["COST"].ToString();
                    }
                    else
                    {
                        colCOST = "0.00";
                    }

                    if (dr["QTY"].ToString() != "")
                    {
                        colQTY = dr["QTY"].ToString();
                    }
                    else
                    {
                        colQTY = "0.00";
                    }

                    if (dr["CASEQTY"].ToString() != "")
                    {
                        colCASEQTY = dr["CASEQTY"].ToString();
                    }
                    else
                    {
                        colCASEQTY = "0.00";
                    }

                    dblItemCost = Functions.fnDouble(colCOST);
                    dblItemQty = Functions.fnDouble(colQTY);
                    intItemCaseQty = Functions.fnInt32(colCASEQTY);
                    intItemSKU = Functions.fnInt32(colITEMID);

                    double stkqty = 0;
                    if (intItemCaseQty == 0) stkqty = dblItemQty;
                    if (intItemCaseQty > 0) stkqty = intItemCaseQty * dblItemQty;

                    int SJ = Functions.AddStockJournal( objSQlComm, "Stock Out", "Adjustment-Recv", "Y", intItemSKU,
                                                        intLoginUserID, stkqty, dblItemCost, intID.ToString(),
                                                        strTerminalName, dtReceiveDate, DateTime.Now, "");

                }
                dtbltemp.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
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
                strSQLComm = "select ProductID as ITEMID, Qty, Cost,CaseQty from RecvDetail where BatchNo = @BatchNo ";

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@BatchNo", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@BatchNo"].Value = intID;

                sqlDataReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ITEMID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QTY", System.Type.GetType("System.String"));
                dtbl.Columns.Add("COST", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CASEQTY", System.Type.GetType("System.String"));
                while (sqlDataReader.Read())
                {
                    dtbl.Rows.Add(new object[] {sqlDataReader["ITEMID"].ToString(),
                                                sqlDataReader["Qty"].ToString(),
                                                sqlDataReader["Cost"].ToString(),
                                                sqlDataReader["CaseQty"].ToString()});
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

        public double GetItemQty(SqlCommand objCommand, int ItemID)
        {

            objCommand.Parameters.Clear();
            double intResult = -1;
            string strSQLComm = "";

            strSQLComm = "select QtyOnHand from PRODUCT where ID=@ITEMID ";

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

            strSQLComm = "select Cost from PRODUCT where ID=@ITEMID ";

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

        private void AdjustStockJournalBeforeDelete(SqlCommand objSQlComm)
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
                    string colCOST = "";

                    if (dr["ITEMID"].ToString() == "") continue;

                    if (dr["ITEMID"].ToString() != "")
                    {
                        colITEMID = dr["ITEMID"].ToString();
                    }

                    if (dr["COST"].ToString() != "")
                    {
                        colCOST = dr["COST"].ToString();
                    }
                    else
                    {
                        colCOST = "0.00";
                    }

                    if (dr["QTY"].ToString() != "")
                    {
                        colQTY = dr["QTY"].ToString();
                    }
                    else
                    {
                        colQTY = "0.00";
                    }


                    dblItemCost = Functions.fnDouble(colCOST);
                    dblItemQty = Functions.fnDouble(colQTY);

                    intItemSKU = Functions.fnInt32(colITEMID);

                    int SJ = Functions.AddStockJournal(objSQlComm, "Stock Out", "Adjustment-Recv", "N", intItemSKU,
                                intLoginUserID, dblItemQty, dblItemCost, intID.ToString(),
                                strTerminalName, DateTime.Now, DateTime.Now, "");

                    /*double iqty = 0;
                    double icost = 0;
                    iqty = GetItemQty(objSQlComm, intItemSKU);
                    icost = GetItemCost(objSQlComm, intItemSKU);
                    UpdateItemCost(objSQlComm, intItemSKU, ((iqty) * icost) / iqty, icost, intLoginUserID);*/

                }
                dtbltemp.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }

        }
        
        public int FetchQtyToPrint(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = "select QtyToPrint from PRODUCT where ID=@ITEMID ";

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
                    intResult = Functions.fnInt32(objSQLReader["QtyToPrint"].ToString());
                }
                objSQLReader.Close();
                return intResult;
            }
            catch
            {
                return 0;
            }
        }

        public bool IfExistInPrintLabel(SqlCommand objCommand, int ItemID)
        {
            objCommand.Parameters.Clear();
            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = " select count(*) as RC from PrintLabel where ProductID=@ITEMID ";

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
        
        public void InsertPrintLabel(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = "Insert Into PrintLabel ( ProductID,Qty,"
                    + "CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                    + "Values ( @ProductID,@Qty,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn )";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = intItemSKU;
                objSQlComm.Parameters["@Qty"].Value = Functions.fnInt32(dblItemQty);//PrintLabelQty
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
                string strSQLComm = "Update PrintLabel set Qty = Qty + @Qty where ProductID=@ID  ";

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

        private void UpdatePrintQty(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Update product set QtyToPrint = QtyToPrint + @Qty where ID=@ID  ";

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

        public string UpdatePrintQty(string pSKU,int pQty)
        {
            string strSQLComm = " update product set QtyToPrint = QtyToPrint - @Qty where SKU=@SKU ";
                                
            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                
                objSQlComm.Parameters["@Qty"].Value = pQty;
                objSQlComm.Parameters["@SKU"].Value = pSKU;
                
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

        public int GetPrintQty(string pSKU)
        {
            int intQty = 0;
            string strSQLComm = " select QtyToPrint from product where SKU = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = pSKU;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intQty = Functions.fnInt32(objsqlReader["QtyToPrint"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intQty;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return -1;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int GetProductID(string pSKU)
        {
            int intPID = 0;
            string strSQLComm = " select ID from product where SKU = @SKU ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = pSKU;

                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;


                objsqlReader = objSQlComm.ExecuteReader();
                if (objsqlReader.Read())
                {
                    try
                    {
                        intPID = Functions.fnInt32(objsqlReader["ID"]);
                    }
                    catch { objsqlReader.Close(); }

                }
                objsqlReader.Close();
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intPID;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return -1;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public string DeletePrintLabel(string pSKU)
        {
            string strSQLComm = " delete from PrintLabel where ProductID in (select ID from product where SKU = @SKU)";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@SKU"].Value = pSKU;
                
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

        public string InsertPrintLabel(int pid, int pqty)
        {
            string strSQLComm = " insert Into PrintLabel ( ProductID,Qty,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn)"
                              + " values ( @ProductID,@Qty,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn )";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.SmallInt));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = pid;
                objSQlComm.Parameters["@Qty"].Value = pqty;
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


    }
}

