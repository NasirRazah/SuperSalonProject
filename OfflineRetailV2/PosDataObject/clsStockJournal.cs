/*
 purpose : Data for Item Stock Journal
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;


namespace PosDataObject
{
    public class StockJournal
    {
        private SqlConnection sqlConn;

        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strErrorMsg;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;
        private string strJTranType;
        private string strJTranSubType;
        private string strJTerminalName;
        private string strJNotes;
        private string strJIsUpdateProduct;
        private string strJDocNo;
        private DateTime dtJDocDate;
        private DateTime dtJTranDate;
        private int intJProductID;
        private int intJEmpID;
        private double dblJQty;
        private double dblJCost;
        private DataTable dtblItemDataTable;
        private string strProductType;
        private int intMatrixOptionID;
        private string strMatrixOptionValue1;
        private string strMatrixOptionValue2;
        private string strMatrixOptionValue3;
        private double dblMatrixQty;

        // stocktake

        private string strStkDocNoPrefix;

        private DateTime dtDocDate;
        private string strDocComment;

        private DataTable dtblStkDetail;
        private DataTable dtblStkPLU;

        private int intStkID;
        private int intProductID;
        private string strSKU;
        private string strDescription;
        private double dblBreakPackRatio;
        private double dblPriceA;
        private double dblPriceB;
        private double dblPriceC;
        private double dblCost;
        private double dblQtyOnHand;
        private double dblStkCount;
        private double dblSKUQty;
        private int intLinkSKU;


        private bool blIsViewMode;

        public bool IsViewMode
        {
            get { return blIsViewMode; }
            set { blIsViewMode = value; }
        }

        public string StkDocNoPrefix
        {
            get { return strStkDocNoPrefix; }
            set { strStkDocNoPrefix = value; }
        }

        public string DocComment
        {
            get { return strDocComment; }
            set { strDocComment = value; }
        }

        public DataTable ItemDataTable
        {
            get { return dtblItemDataTable; }
            set { dtblItemDataTable = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DateTime DocDate
        {
            get { return dtDocDate; }
            set { dtDocDate = value; }
        }

        public DataTable dStkDetail
        {
            get { return dtblStkDetail; }
            set { dtblStkDetail = value; }
        }

        public DataTable dStkPLU
        {
            get { return dtblStkPLU; }
            set { dtblStkPLU = value; }
        }

        public string ProductType
        {
            get { return strProductType; }
            set { strProductType = value; }
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

        public int JEmpID
        {
            get { return intJEmpID; }
            set { intJEmpID = value; }
        }

        public int JProductID
        {
            get { return intJProductID; }
            set { intJProductID = value; }
        }

        public double JQty
        {
            get { return dblJQty; }
            set { dblJQty = value; }
        }

        public double JCost
        {
            get { return dblJCost; }
            set { dblJCost = value; }
        }

        public DateTime JDocDate
        {
            get { return dtJDocDate; }
            set { dtJDocDate = value; }
        }

        public DateTime JTranDate
        {
            get { return dtJTranDate; }
            set { dtJTranDate = value; }
        }

        public string JTranType
        {
            get { return strJTranType; }
            set { strJTranType = value; }
        }

        public string JTranSubType
        {
            get { return strJTranSubType; }
            set { strJTranSubType = value; }
        }

        public string JTerminalName
        {
            get { return strJTerminalName; }
            set { strJTerminalName = value; }
        }

        public string JNotes
        {
            get { return strJNotes; }
            set { strJNotes = value; }
        }

        public string JDocNo
        {
            get { return strJDocNo; }
            set { strJDocNo = value; }
        }

        public string JIsUpdateProduct
        {
            get { return strJIsUpdateProduct; }
            set { strJIsUpdateProduct = value; }
        }

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
            if (SaveTran != null)
            {
                SaveTran.Commit();
                SaveTran.Dispose();
                SaveCon.Close();
            }
        }

        public bool AddJournal()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                intNewID = Functions.AddStockJournal(SaveComm, JTranType, JTranSubType, JIsUpdateProduct, JProductID,
                                    JEmpID, JQty, JCost, JDocNo, JTerminalName, JDocDate, JTranDate, JNotes);
                if (strProductType == "M") AdjustMatrixRecords(SaveComm);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                string sss = SQLDBException.ToString();
                return false;
            }
        }

        private void AdjustMatrixRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dtblItemDataTable == null) return;
                foreach (DataRow dr in dtblItemDataTable.Rows)
                {
                    string colMATRIXOID = "";
                    string colMATRIXOV1 = "";
                    string colMATRIXOV2 = "";
                    string colMATRIXOV3 = "";
                    string colQTY = "";

                    if (Functions.fnInt32(dr["QtyOnHand"].ToString()) == 0) continue;


                    if (dr["QtyOnHand"].ToString() != "")
                    {
                        colQTY = dr["QtyOnHand"].ToString();
                    }
                    else
                    {
                        colQTY = "0.00";
                    }

                    if (dr["MatrixOptionID"].ToString() != "")
                    {
                        colMATRIXOID = dr["MatrixOptionID"].ToString();
                    }
                    else
                    {
                        colMATRIXOID = "0";
                    }

                    colMATRIXOV1 = dr["OptionValue1"].ToString();
                    colMATRIXOV2 = dr["OptionValue2"].ToString();
                    colMATRIXOV3 = dr["OptionValue3"].ToString();


                    intMatrixOptionID = Functions.fnInt32(colMATRIXOID);
                    strMatrixOptionValue1 = colMATRIXOV1;
                    strMatrixOptionValue2 = colMATRIXOV2;
                    strMatrixOptionValue3 = colMATRIXOV3;
                    dblMatrixQty = Functions.fnDouble(colQTY);

                    if (strJTranType == "Stock In")
                        Functions.UpdateMatrixItemStock(objSQlComm, intMatrixOptionID, strMatrixOptionValue1, strMatrixOptionValue2,
                                    strMatrixOptionValue3, dblMatrixQty, intLoginUserID);
                    if (strJTranType == "Stock Out")
                        Functions.UpdateMatrixItemStock(objSQlComm, intMatrixOptionID, strMatrixOptionValue1, strMatrixOptionValue2,
                                    strMatrixOptionValue3, -dblMatrixQty, intLoginUserID);

                }
                dtblItemDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public DataTable FetchData(DateTime pFDate, DateTime pTDate, string Filter, int product, string configDateFormat)
        {
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day,0,0,0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day,23,59,59);

            DataTable dtbl = new DataTable();
            string strSQLFilter = "";
            string strSQLFilter1 = "";
            if (Filter != "ALL")
            {
                strSQLFilter = " and j.trantype = '" + Filter + "'";
            }
            if (product != 0)
            {
                strSQLFilter1 = " and j.productid = @p ";
            }
            
            string strSQLComm = "";
            
            strSQLComm = " select j.ID,j.DocNo,j.DocDate,j.ProductID,j.TranType,j.TranSubType,j.Qty,j.Cost,j.StockOnHand,j.TerminalName,j.TranDate,j.Notes, isnull(e.EmployeeID,'ADMIN') as EmployeeID,p.SKU, p.DecimalPlace, isnull(p.DiscountedCost,0) as DCost  "
                        + " from StockJournal j left outer join Product p on j.ProductID = p.ID "
                        + " left outer join Employee e on j.EmpID = e.ID  "
                        + " where j.docdate between @FDT and @TDT " + strSQLFilter + strSQLFilter1 + " order by convert(date,j.docdate) desc, ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = FromDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = ToDate;

            if (product != 0)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@p"].Value = product;
            }

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                //SqlCommand sqlcmd = new SqlCommand("SET ARITHABORT ON", sqlConn);
                //sqlcmd.ExecuteNonQuery();
                //sqlcmd = objSQlComm;
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranSubType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("StockOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DP", System.Type.GetType("System.String"));
                
                string Qty = "";
                string StockQty = "";

                while (objSQLReader.Read())
                {
                    Qty = Functions.fnDouble(objSQLReader["Qty"].ToString()).ToString();
                    StockQty = Functions.fnDouble(objSQLReader["StockOnHand"].ToString()).ToString("f");

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["DocNo"].ToString(),
                                                Functions.fnDate(objSQLReader["DocDate"].ToString()).ToString(configDateFormat),
                                                objSQLReader["ProductID"].ToString(),
                                                objSQLReader["TranType"].ToString(),
                                                objSQLReader["TranSubType"].ToString(),
                                                Qty,
                                                Functions.fnDouble(objSQLReader["Cost"].ToString()),
                                                Functions.fnDouble(objSQLReader["DCost"].ToString()),
                                                StockQty,
                                                objSQLReader["TerminalName"].ToString(),
                                                objSQLReader["EmployeeID"].ToString(),
                                                objSQLReader["TranDate"].ToString(),
                                                objSQLReader["Notes"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["DecimalPlace"].ToString()
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

        public DataTable FetchDataForReport(DateTime pFDate, DateTime pTDate, string Ptran, string Psubtran, string configDateFormat)
        {
            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();

            string strSQLFilter1 = "";
            string strSQLFilter2 = "";

            if (Ptran == "Stock In")
            {
                strSQLFilter1 = " and j.TranType = 'Stock In'";
                if (Psubtran != "ALL")
                {
                    strSQLFilter2 = " and j.TranSubType = '" + Psubtran + "'";
                }
            }

            if (Ptran == "Stock Out")
            {
                strSQLFilter1 = " and j.TranType = 'Stock Out'";
                if (Psubtran != "ALL")
                {
                    strSQLFilter2 = " and j.TranSubType = '" + Psubtran + "'";
                }
            }

            string strSQLComm = "";

            strSQLComm = " Select j.*, p.SKU,p.Description,p.DecimalPlace,isnull(e.EmployeeID,'ADMIN') as EmployeeID, isnull(p.DiscountedCost,0) as DCost, "
                          + " p.SKU2, p.SKU3, isnull(c.Description,'') as CategoryName "
                        + " from StockJournal j left outer join Product p on j.ProductID = p.ID "
                        + " left outer join Employee e on e.ID = j.EmpID "
                        + " left outer join Category c on c.ID = p.CategoryID "
                        + " where j.docdate between @FDT and @TDT " + strSQLFilter1 + strSQLFilter2 
                        + " order by p.SKU,j.docdate desc ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = FromDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = ToDate;
            objSQlComm.CommandType = CommandType.Text;
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranSubType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StockOnHand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TerminalName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DP", System.Type.GetType("System.String"));

                dtbl.Columns.Add("AltSKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Category", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorPart", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SalePrice", System.Type.GetType("System.String"));

                double Qty = 0;
                double StockQty = 0;
                string Tran = "";
                string AltSku = "";
                string cost = "0";
                string sale = "0";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["TranType"].ToString() == "Stock Out")
                    {
                        Qty = -Functions.fnDouble(objSQLReader["Qty"].ToString());
                        Tran = "Out";
                    }
                    if (objSQLReader["TranType"].ToString() == "Stock In")
                    {
                        Qty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                        Tran = "In";
                    }
                    if (objSQLReader["SKU2"].ToString() != "")
                        AltSku = objSQLReader["SKU2"].ToString();
                    if (objSQLReader["SKU3"].ToString() != "")
                        AltSku = AltSku == "" ? objSQLReader["SKU3"].ToString() : AltSku + ", " + objSQLReader["SKU3"].ToString();
                    StockQty = Functions.fnDouble(objSQLReader["StockOnHand"].ToString());

                    

                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["DocNo"].ToString(),
                                                Functions.fnDate(objSQLReader["DocDate"].ToString()).ToString((configDateFormat) + "  hh:mm tt"),
                                                objSQLReader["ProductID"].ToString(),
                                                Tran,
                                                objSQLReader["TranSubType"].ToString(),
                                                Qty.ToString(),
                                                objSQLReader["Cost"].ToString(),
                                                objSQLReader["DCost"].ToString(),
                                                StockQty.ToString(),
                                                objSQLReader["TerminalName"].ToString(),
                                                objSQLReader["EmployeeID"].ToString(),
                                                objSQLReader["TranDate"].ToString(),
                                                objSQLReader["Notes"].ToString(),
                                                objSQLReader["SKU"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                objSQLReader["DecimalPlace"].ToString(),
                                                AltSku,objSQLReader["CategoryName"].ToString(),"","","0"});
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


        public DataTable FetchSupplierDataForReport(int batchId, int prodId, double qty)
        {
           

            DataTable dtbl = new DataTable();

           

            string strSQLComm = "";

            strSQLComm = " Select v.Name as VName, d.VendorPartNo from RecvDetail d left outer join RecvHeader h on d.BatchNo = h.BatchID  "
                          + " left outer join Vendor v on v.ID = h.VendorID where d.BatchNo = @param1 and d.ProductID = @param2 and d.Qty = @param3 ";
                       

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@param1"].Value = batchId;
            objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@param2"].Value = prodId;
            objSQlComm.Parameters.Add(new SqlParameter("@param3", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@param3"].Value = qty;
            objSQlComm.CommandType = CommandType.Text;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Part", System.Type.GetType("System.String"));
               
                while (objSQLReader.Read())
                {
                   

                    dtbl.Rows.Add(new object[] {objSQLReader["VName"].ToString(),
                                                objSQLReader["VendorPartNo"].ToString()});

                    break;
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


        public double FetchSaleDataForReport(int invoiceId, int prodId, double qty, string txf, bool breturn)
        {


            double val = 0;



            string strSQLComm = "";

            strSQLComm = " Select isnull(price,0) as pr,isnull(taxincluderate,0) as prI from  "
                          + " item where invoiceno = @param1 and productid = @param2 and qty = @param3 ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@param1", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@param1"].Value = invoiceId;
            objSQlComm.Parameters.Add(new SqlParameter("@param2", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@param2"].Value = prodId;
            objSQlComm.Parameters.Add(new SqlParameter("@param3", System.Data.SqlDbType.Float));
            objSQlComm.Parameters["@param3"].Value = qty;
            objSQlComm.CommandType = CommandType.Text;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                

                while (objSQLReader.Read())
                {
                    if (txf == "N") val = Functions.fnDouble(objSQLReader["pr"].ToString());
                    if (txf == "Y") val = Functions.fnDouble(objSQLReader["prI"].ToString());

                    if (breturn) val = -val;

                    break;
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


        public void GetRecordCount()
        {
            bool blReturn = false;

            string strSQLComm = "select count(*) as CRec from stockjournal";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
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

        public bool IsExistRecord(int PID)
        {
            bool blReturn = false;

            string strSQLComm = "select count(*) as CRec from stockjournal where ProductID = @ProductID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ProductID"].Value = PID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    if (Functions.fnInt32(objSQLReader["CRec"].ToString()) > 0) blReturn = true;
                    else blReturn = false;
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return blReturn;
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

        public string GetProductType(int PID)
        {
            string strReturn = "";

            string strSQLComm = "select isnull(ProductType,'') as PT from product where ID = @ProductID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ProductID"].Value = PID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strReturn = objSQLReader["PT"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strReturn;
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

        #region Aging

        public DataTable FetchAgingData(int AgingPeriod)
        {
            DataTable dtbl = new DataTable();
            DateTime pFDate = DateTime.Today.Date.AddDays(-AgingPeriod);
            DateTime pTDate = DateTime.Today.Date;
            

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            string strSQLComm = " select p.SKU,p.Description,p.QtyOnHand,s.DocDate,s.Qty from stockjournal s "
                              + " left outer join product p on p.ID = s.ProductID where s.DocDate < @FDT "
                              + " and s.TranType = 'Stock In' and p.QtyOnHand > 0 order by p.SKU,s.DocDate desc  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = FromDate;
           // objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
           // objSQlComm.Parameters["@TDT"].Value = ToDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastReceived", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Aging", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    int agingval = DateTime.Today.Date.Subtract(Functions.fnDate(objSQLReader["DocDate"].ToString()).Date).Days;

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDate(objSQLReader["DocDate"].ToString()),
                                                   agingval,
                                                   Functions.fnDouble(objSQLReader["QtyOnHand"].ToString())});
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

        public DataTable FetchAgingData(DateTime AgingDate)
        {
            DataTable dtbl = new DataTable();
            DateTime pFDate = AgingDate;
            DateTime pTDate = DateTime.Today.Date;


            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            string strSQLComm = " select p.SKU,p.Description,p.QtyOnHand,s.DocDate,s.Qty from stockjournal s "
                              + " left outer join product p on p.ID = s.ProductID where s.DocDate < @FDT "
                              + " and s.TranType = 'Stock In' and p.QtyOnHand > 0 order by p.SKU,s.DocDate desc  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = FromDate;
            // objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            // objSQlComm.Parameters["@TDT"].Value = ToDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastReceived", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Aging", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    int agingval = DateTime.Today.Date.Subtract(Functions.fnDate(objSQLReader["DocDate"].ToString()).Date).Days;

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDate(objSQLReader["DocDate"].ToString()),
                                                   agingval,
                                                   Functions.fnDouble(objSQLReader["QtyOnHand"].ToString())});
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

        #region STOCKTAKE Data

        public DataTable FetchStocktakeBrowseHeader(DateTime pFDate,DateTime pTDate)
        {
            DataTable dtbl = new DataTable();

            string sqlFilter = "";
            string sqlFilter1 = "";

            DateTime FromDate = new DateTime(pFDate.Year, pFDate.Month, pFDate.Day, 0, 0, 0);
            DateTime ToDate = new DateTime(pTDate.Year, pTDate.Month, pTDate.Day, 23, 59, 59);

            string strSQLComm = " select ID,DocNo,DocDate,DocComment,StkStatus from stocktakeheader "
                              + " where DocDate between @FDT and @TDT order by DocDate Desc, ID desc ";
            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@FDT"].Value = FromDate;
            objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@TDT"].Value = ToDate;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocComment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StkStatus", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {

                    string strDateTime = "";
                    string strDateTime1 = "";
                    if (objSQLReader["DocDate"].ToString() != "")
                    {
                        strDateTime = objSQLReader["DocDate"].ToString();
                        int inIndex = strDateTime.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDateTime = strDateTime.Substring(0, inIndex).Trim();
                        }
                    }
                    else
                    {
                        strDateTime = objSQLReader["DocDate"].ToString();
                    }
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DocNo"].ToString(),
                                                   strDateTime,
                                                   objSQLReader["DocComment"].ToString(),
                                                   objSQLReader["StkStatus"].ToString()});
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

        public DataTable FetchStocktakeBrowseDetail(int pHeaderID,int pLinkSKU, int pSKU)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string sqlFilter = "";
            string sqlFilter1 = "";

            if (pLinkSKU == 0)
            {
                strSQLComm = " select ID,ProductID,SKU,Description,Cost,PriceA,PriceB,PriceC,QtyOnHand,StkCount,SKUQty,BreakPackRatio,LinkSKU, "
                           + " (SKUQty - QtyOnHand) as QtyVar,(SKUQty - QtyOnHand)* Cost as CostVar,(SKUQty - QtyOnHand)* PriceA as PriceVar from stocktakedetail "
                           + " where ( 1 = 1 ) and linksku <= 0 and stkid = @HID order by ID ";
            }

            if (pLinkSKU == -1)
            {
                strSQLComm = " select ID,ProductID,SKU,Description,Cost,PriceA,PriceB,PriceC,QtyOnHand,StkCount,SKUQty,BreakPackRatio,LinkSKU, "
                           + " (SKUQty - QtyOnHand) as QtyVar, (SKUQty - QtyOnHand)* Cost as CostVar, (SKUQty - QtyOnHand)* PriceA as PriceVar from stocktakedetail "
                           + " where ( 1 = 1 ) and linksku > 0 and stkid = @HID "
                           + " and SKU in ( select p.SKU from product p where p.linksku > 0 and p.linksku in ( select p1.ID from "
                           + " product p1 where p1.ID = @SKU ) ) order by ID ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@HID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@HID"].Value = pHeaderID;
            if (pLinkSKU == -1)
            {
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@SKU"].Value = pSKU;
            }

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyVar", System.Type.GetType("System.Double"));
                
                //dtbl.Columns.Add("CostVar", System.Type.GetType("System.Double"));
                //dtbl.Columns.Add("PriceVar", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("StkCount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("PriceA", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("PriceB", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("PriceC", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["SKUQty"].ToString()),
                                                   Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()),
                                                   Functions.fnDouble(objSQLReader["QtyVar"].ToString()),
                                                   //Functions.fnDouble(objSQLReader["CostVar"].ToString()),
                                                   //Functions.fnDouble(objSQLReader["PriceVar"].ToString()),
                                                   objSQLReader["SKU"].ToString(),
                                                   Functions.fnDouble(objSQLReader["BreakPackRatio"].ToString()),
                                                   Functions.fnDouble(objSQLReader["StkCount"].ToString()),
                                                   Functions.fnDouble(objSQLReader["PriceA"].ToString()),
                                                   Functions.fnDouble(objSQLReader["PriceB"].ToString()),
                                                   Functions.fnDouble(objSQLReader["PriceC"].ToString()),
                                                   Functions.fnInt32(objSQLReader["LinkSKU"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Cost"].ToString())});
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

        public string GetNewStocktakeDoc()
        {
            string strReturn = "";

            string strSQLComm = " select isnull(max(ID),0) + 1 as MaxDoc from stocktakeheader ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strReturn = objSQLReader["MaxDoc"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return "Not Defined";
            }
        }

        public bool StocktakeTransaction()
        {
            try
            {
                string retval = "";
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;

                if (intID == 0) // insert mode
                {
                    retval = InsertStkHeaderData(SaveComm); // insert header
                    if (retval == "")
                    {
                        string stkdoc = strStkDocNoPrefix + intID.ToString();
                        retval = UpdateStkDoc(SaveComm, stkdoc);
                    }
                }
                else // update mode
                {
                    retval = UpdateStkHeaderData(SaveComm);
                }
                if (!blIsViewMode)
                {
                    if (retval == "") // insert detail
                    {
                        DeleteStkDetails(SaveComm, intID);
                        retval = AdjustStkDetail1(SaveComm);
                        if (retval == "")
                        {
                            retval = AdjustStkDetail2(SaveComm);
                        }
                    }
                }
                SaveComm.Dispose();
                if (retval == "") return true; else return false;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public string AdjustStkDetail1(SqlCommand objSQlComm)
        {
            try
            {
                string val = "";
                if (dtblStkDetail == null) return "";
                foreach (DataRow dr in dtblStkDetail.Rows)
                {
                    string colSKU = "";
                    string colProductID = "";
                    string colDescription = "";
                    string colCost = "";
                    string colQtyOnHand = "";
                    string colBreakPackRatio = "";
                    string colPriceA = "";
                    string colPriceB = "";
                    string colPriceC = "";
                    string colStkCount = "";
                    string colSKUQty = "";
                    string colLinkSKU = "";
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "") colProductID = dr["ProductID"].ToString(); else colProductID = "0";
                    if (dr["Cost"].ToString() != "") colCost = dr["Cost"].ToString(); else colCost = "0";
                    if (dr["QtyOnHand"].ToString() != "") colQtyOnHand = dr["QtyOnHand"].ToString(); else colQtyOnHand = "0";
                    if (dr["BreakPackRatio"].ToString() != "") colBreakPackRatio = dr["BreakPackRatio"].ToString(); else colBreakPackRatio = "0";
                    if (dr["PriceA"].ToString() != "") colPriceA = dr["PriceA"].ToString(); else colPriceA = "0";
                    if (dr["PriceB"].ToString() != "") colPriceB = dr["PriceB"].ToString(); else colPriceB = "0";
                    if (dr["PriceC"].ToString() != "") colPriceC = dr["PriceC"].ToString(); else colPriceC = "0";
                    if (dr["StkCount"].ToString() != "") colStkCount = dr["StkCount"].ToString(); else colStkCount = "0";
                    if (dr["SKUQty"].ToString() != "") colSKUQty = dr["SKUQty"].ToString(); else colSKUQty = "0";
                    if (dr["LinkSKU"].ToString() != "") colLinkSKU = dr["LinkSKU"].ToString(); else colLinkSKU = "0";
                    colSKU = dr["SKU"].ToString();
                    colDescription = dr["Description"].ToString();
                    strSKU = colSKU;
                    strDescription = colDescription;
                    intProductID = Functions.fnInt32(colProductID);
                    intLinkSKU = Functions.fnInt32(colLinkSKU);
                    dblBreakPackRatio = Functions.fnDouble(colBreakPackRatio);
                    dblPriceA = Functions.fnDouble(colPriceA);
                    dblPriceB = Functions.fnDouble(colPriceB);
                    dblPriceC = Functions.fnDouble(colPriceC);
                    dblCost = Functions.fnDouble(colCost);
                    dblQtyOnHand = Functions.fnDouble(colQtyOnHand);
                    dblStkCount = Functions.fnDouble(colStkCount);
                    dblSKUQty = Functions.fnDouble(colSKUQty);

                    val = InsertStkDetailData(objSQlComm);
                    
                }
                return val;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AdjustStkDetail2(SqlCommand objSQlComm)
        {
            try
            {
                string val = "";
                if (dtblStkPLU == null) return "";
                foreach (DataRow dr in dtblStkPLU.Rows)
                {
                    string colSKU = "";
                    string colProductID = "";
                    string colDescription = "";
                    string colCost = "0";
                    string colQtyOnHand = "0";
                    string colBreakPackRatio = "";
                    string colPriceA = "0";
                    string colPriceB = "0";
                    string colPriceC = "0";
                    string colStkCount = "";
                    string colSKUQty = "";
                    string colLinkSKU = "";
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "") colProductID = dr["ProductID"].ToString(); else colProductID = "0";
                    if (dr["BreakPackRatio"].ToString() != "") colBreakPackRatio = dr["BreakPackRatio"].ToString(); else colBreakPackRatio = "0";
                    if (dr["StkCount"].ToString() != "") colStkCount = dr["StkCount"].ToString(); else colStkCount = "0";
                    if (dr["SKUQty"].ToString() != "") colSKUQty = dr["SKUQty"].ToString(); else colSKUQty = "0";
                    if (dr["LinkSKU"].ToString() != "") colLinkSKU = dr["LinkSKU"].ToString(); else colLinkSKU = "0";
                    colSKU = dr["SKU"].ToString();
                    colDescription = dr["Description"].ToString();
                    strSKU = colSKU;
                    strDescription = colDescription;
                    intProductID = Functions.fnInt32(colProductID);
                    intLinkSKU = Functions.fnInt32(colLinkSKU);
                    dblBreakPackRatio = Functions.fnDouble(colBreakPackRatio);
                    dblPriceA = Functions.fnDouble(colPriceA);
                    dblPriceB = Functions.fnDouble(colPriceB);
                    dblPriceC = Functions.fnDouble(colPriceC);
                    dblCost = Functions.fnDouble(colCost);
                    dblQtyOnHand = Functions.fnDouble(colQtyOnHand);
                    dblStkCount = Functions.fnDouble(colStkCount);
                    dblSKUQty = Functions.fnDouble(colSKUQty);

                    val = InsertStkDetailData(objSQlComm);
                    
                }
                return val;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string InsertStkHeaderData(SqlCommand objSQlComm)
        {
            string strSQLComm = "";

            strSQLComm = " insert into stocktakeheader( DocDate,DocComment,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @DocDate,@DocComment,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) "
                       + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@DocDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DocComment", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@DocDate"].Value = dtDocDate;
                objSQlComm.Parameters["@DocComment"].Value = strDocComment;
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

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }

        }

        public string UpdateStkHeaderData(SqlCommand objSQlComm)
        {
            string strSQLComm = "";
            strSQLComm = " update stocktakeheader set DocDate=@DocDate,DocComment=@DocComment, "
                       + " LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn where ID=@ID ";
                       
            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DocDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@DocComment", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                
                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DocDate"].Value = dtDocDate;
                objSQlComm.Parameters["@DocComment"].Value = strDocComment;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

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

        public string GetStocktakeDoc(SqlCommand objSQlComm)
        {
            string strReturn = "";

            string strSQLComm = " select isnull(max(ID),0) + 1 as MaxDoc from stocktakeheader ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strReturn = objSQLReader["MaxDoc"].ToString();
                }
                
                objSQLReader.Close();
                objSQLReader.Dispose();

                return strStkDocNoPrefix+strReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();

                return "Not Defined";
            }
        }

        public string UpdateStkDoc(SqlCommand objSQlComm, string pval)
        {
            string strSQLComm = "";

            strSQLComm = " update stocktakeheader set DocNo=@DocNo where ID=@ID ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DocNo", System.Data.SqlDbType.NVarChar));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DocNo"].Value = pval;

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

        public string UpdateStkDocStatus(SqlCommand objSQlComm)
        {
            string strSQLComm = "";

            strSQLComm = " update stocktakeheader set StkStatus='Completed' where ID=@ID ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;

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

        public bool DeleteStkHeaders(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from stocktakeheader where id = @ID";

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

        public bool DeleteStkDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = " delete from stocktakedetail where stkid = @ID";

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

        public string InsertStkDetailData(SqlCommand objSQlComm)
        {
            string strSQLComm = "";
            strSQLComm = " insert into stocktakedetail( StkID,ProductID,SKU,Description,BreakPackRatio,PriceA,PriceB,PriceC,Cost,"
                       + " QtyOnHand,StkCount,SKUQty,LinkSKU,CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values ( @StkID,@ProductID,@SKU,@Description,@BreakPackRatio,@PriceA,@PriceB,@PriceC,@Cost,"
                       + " @QtyOnHand,@StkCount,@SKUQty,@LinkSKU,@CreatedBy,@CreatedOn,@LastChangedBy,@LastChangedOn) ";
            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@SKU", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@StkID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkSKU", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BreakPackRatio", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceA", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceB", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@PriceC", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@StkCount", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@SKUQty", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@SKU"].Value = strSKU;
                objSQlComm.Parameters["@Description"].Value = strDescription;
                objSQlComm.Parameters["@StkID"].Value = intID;
                objSQlComm.Parameters["@ProductID"].Value = intProductID;
                objSQlComm.Parameters["@LinkSKU"].Value = intLinkSKU;
                objSQlComm.Parameters["@BreakPackRatio"].Value = dblBreakPackRatio;
                objSQlComm.Parameters["@PriceA"].Value = dblPriceA;
                objSQlComm.Parameters["@PriceB"].Value = dblPriceB;
                objSQlComm.Parameters["@PriceC"].Value = dblPriceC;
                objSQlComm.Parameters["@Cost"].Value = dblCost;
                objSQlComm.Parameters["@QtyOnHand"].Value = dblQtyOnHand;
                objSQlComm.Parameters["@StkCount"].Value = dblStkCount;
                objSQlComm.Parameters["@SKUQty"].Value = dblSKUQty;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();
                
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
        
        public DataTable ShowStocktakeHeader(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,DocNo,DocDate,DocComment,StkStatus from stocktakeheader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocComment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StkStatus", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DocNo"].ToString(),
                                                   objSQLReader["DocDate"].ToString(),
                                                   objSQLReader["DocComment"].ToString(),
                                                   objSQLReader["StkStatus"].ToString()});
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

        public DataTable ShowStocktakeDetailPLU(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,ProductID,SKU,Description,BreakPackRatio,StkCount,SKUQty,LinkSKU "
                              + " from stocktakedetail where StkID = @ID and LinkSKU > 0 order by LinkSKU, BreakPackRatio desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("StkCount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["SKUQty"].ToString()),
                                                   objSQLReader["SKU"].ToString(),
                                                   Functions.fnDouble(objSQLReader["BreakPackRatio"].ToString()),
                                                   Functions.fnDouble(objSQLReader["StkCount"].ToString()),
                                                   Functions.fnInt32(objSQLReader["LinkSKU"].ToString())});
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
        
        public bool IfFuturePostedProductIDExists(int DocID, int pID)
        {
            int cnt = 0;

            string strSQLComm = " select count(h.ID) as rcnt from stocktakeheader h left outer join stocktakedetail d on h.ID = d.StkID where  "
                              + " h.ID > @ID and h.StkStatus = 'Completed' and d.ProductID = @PID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = DocID;
            objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@PID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    cnt =    Functions.fnInt32(objSQLReader["rcnt"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                if (cnt > 0) return true; else return false;
            }
            catch (SqlException SQLDBException)
            {
                strErrorMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return false;
            }
        }
        
        public string AdjustStock(SqlCommand objSQlComm)
        {
            try
            {
                string val = "";
                if (dtblStkDetail == null) return "";
                foreach (DataRow dr in dtblStkDetail.Rows)
                {
                    string colProductID = "";
                    string colStkCount = "";
                    string colQtyOnHand = "";
                    if (dr["ProductID"].ToString() == "") continue;

                    if (dr["ProductID"].ToString() != "") colProductID = dr["ProductID"].ToString(); else colProductID = "0";
                    if (dr["StkCount"].ToString() != "") colStkCount = dr["StkCount"].ToString(); else colStkCount = "0";
                    if (dr["QtyOnHand"].ToString() != "") colQtyOnHand = dr["QtyOnHand"].ToString(); else colQtyOnHand = "0";
                    
                    intProductID = Functions.fnInt32(colProductID);
                    //dblQtyOnHand = Functions.fnDouble(colQtyOnHand);
                    dblStkCount = Functions.fnDouble(colStkCount);
                    DataTable dtbl = new DataTable();
                    dtbl = GetProductInfo(objSQlComm, intProductID);
                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        dblCost = Functions.fnDouble(dr1["Cost"].ToString());
                        dblQtyOnHand = Functions.fnDouble(dr1["Qty"].ToString());
                    }
                    double varianceval = 0;
                    varianceval = dblStkCount - dblQtyOnHand;
                    string trntype = "";
                    double qty = 0;
                    if (varianceval != 0)
                    {
                        if (varianceval > 0)
                        {
                            trntype = "Stock In";
                            qty = varianceval;
                        }
                        else
                        {
                            trntype = "Stock Out";
                            qty = -varianceval;
                        }

                        int intNewID = 0;
                        intNewID = Functions.AddStockJournal(SaveComm, trntype, "Inventory Adjustment", "Y", intProductID,
                                        intLoginUserID, qty, dblCost, JDocNo, JTerminalName, DateTime.Now, DateTime.Now, "Inventory Adjustment Post");
                    }
                    
                }
                return val;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool StocktakePost()
        {
            try
            {
                string retval = "";
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;

                retval = AdjustStock(SaveComm);
                if (retval == "")
                {
                    retval = UpdateStkDocStatus(SaveComm);
                }
                SaveComm.Dispose();
                if (retval == "") return true; else return false;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public DataTable GetProductInfo (SqlCommand objSQlComm, int PID)
        {
            string strReturn = "";
            DataTable dtbl = new DataTable();
            string strSQLComm = " select cost,qtyonhand from product where id = @id ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objSQLReader = null;
            try
            {
                objSQlComm.CommandType = CommandType.Text;
                objSQlComm.CommandText = strSQLComm;
                objSQlComm.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@id"].Value = PID;

                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["qtyonhand"].ToString(), objSQLReader["cost"].ToString() });
                }

                objSQLReader.Close();
                objSQLReader.Dispose();

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();

                return null;
            }
        }

        public bool DeleteStockTake()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.sqlConn);
                SaveComm.Transaction = this.SaveTran;
                DeleteStkHeaders(SaveComm, intID);
                DeleteStkDetails(SaveComm, intID);

                SaveComm.Dispose();
                return true;
            }
            catch (SqlException SQLDBException)
            {
                SaveComm.Dispose();
                return false;
            }
        }

        public DataTable ShowStockTakeReportParent(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select h.ID,h.DocNo,h.DocDate,h.DocComment,h.StkStatus,d.ProductID,d.SKU,d.Description, "
                              + " d.StkCount,d.SKUQty,d.LinkSKU,d.QtyOnHand,(d.SKUQty - d.QtyOnHand) as QtyVar "
                              + " from stocktakeheader h left outer join stocktakedetail d on h.ID = d.StkID "
                              + " where h.ID = @ID and d.LinkSKU <= 0 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DocComment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StkStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StkCount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("BreakPackFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("QtyVar", System.Type.GetType("System.Double"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   objSQLReader["DocNo"].ToString(),
                                                   objSQLReader["DocDate"].ToString(),
                                                   objSQLReader["DocComment"].ToString(),
                                                   objSQLReader["StkStatus"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["SKUQty"].ToString()),
                                                   objSQLReader["SKU"].ToString(),
                                                   Functions.fnDouble(objSQLReader["StkCount"].ToString()),
                                                   Functions.fnInt32(objSQLReader["LinkSKU"].ToString()),"0",
                                                   Functions.fnDouble(objSQLReader["QtyOnHand"].ToString()),
                                                   Functions.fnDouble(objSQLReader["QtyVar"].ToString())});
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

        public DataTable ShowStockTakeReportChild(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select d.SKU,d.Description,d.LinkSKU,d.StkCount,d.SKUQty "
                              + " from stocktakeheader h left outer join stocktakedetail d on h.ID = d.StkID "
                              + " where h.ID = @ID and d.LinkSKU > 0 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StkCount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.Int32"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["Description"].ToString(),
                                                   Functions.fnDouble(objSQLReader["SKUQty"].ToString()),
                                                   objSQLReader["SKU"].ToString(),
                                                   Functions.fnDouble(objSQLReader["StkCount"].ToString()),
                                                   Functions.fnInt32(objSQLReader["LinkSKU"].ToString())});
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

        public DataTable ShowStockTakeReportLinkCount(int pID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(d.LinkSKU,0) as LSKU,count(d.LinkSKU) as rcnt "
                              + " from stocktakeheader h left outer join stocktakedetail d on h.ID = d.StkID "
                              + " where h.ID = @ID and d.LinkSKU > 0 group by d.LinkSKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = pID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("LCount", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   Functions.fnInt32(objSQLReader["LSKU"].ToString()),
                                                   objSQLReader["rcnt"].ToString()});
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

    }
}
