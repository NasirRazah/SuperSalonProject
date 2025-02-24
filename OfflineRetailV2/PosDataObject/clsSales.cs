/*
 purpose : Data for Sales Transaction
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace PosDataObject
{
    public class Sales
    {
        private string strOperateStore;
        private SqlConnection sqlConn;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public string OperateStore
        {
            get { return strOperateStore; }
            set { strOperateStore = value; }
        }

        #region Fetch Data For Employee Wage to Total Sales

        public DataTable FetchEmployeeForWageToSalesReport(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm =   " Select a.EMPID, a.DAYSTART, a.DAYEND,b.EMPLOYEEID, b.LASTNAME,b.FIRSTNAME, b.EMPRATE "
                         + " from AttendanceInfo a Left Join Employee b on a.EMPID = b.ID where ( 1 = 1) "
                         + " and ((a.DAYSTART between @SDT and @FDT) or (a.DAYSTART between @SDT and @FDT)) "
                         + " order by a.EMPID, a.DAYSTART ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@FDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDT1"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Check", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {
                    string strDate1 = "";
                    string strDate2 = "";
                    double intCalculateTime = 0;
                    double dblRate = 0.00;
                    double dblCalculateRate = 0.00;
                    int strTime = 0;
                    DateTime CalculateFrom = Convert.ToDateTime(null);
                    DateTime CalculateTo = Convert.ToDateTime(null);


                    TimeSpan ts = new TimeSpan();

                    if (objSQLReader["DAYSTART"].ToString() != "")
                    {
                        strDate1 = objSQLReader["DAYSTART"].ToString();
                        int inIndex = strDate1.IndexOf(" ");
                        if (inIndex > 0)
                        {
                            strDate1 = strDate1.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYSTART"].ToString()) < FormatStartDate)
                        {
                            DateTime ss = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                            CalculateFrom = FormatStartDate.AddSeconds(-1);
                        }
                        else
                        {
                            CalculateFrom = Functions.fnDate(objSQLReader["DAYSTART"].ToString());
                        }
                    }

                    if (objSQLReader["DAYEND"].ToString() != "")
                    {
                        strDate2 = objSQLReader["DAYEND"].ToString();
                        int inIndex = strDate2.IndexOf(" ");
                        int inLength = strDate2.Length;
                        if (inIndex > 0)
                        {
                            strDate2 = strDate2.Substring(0, inIndex).Trim();
                        }

                        if (Functions.fnDate(objSQLReader["DAYEND"].ToString()) > FormatEndDate)
                        {
                            CalculateTo = FormatEndDate.AddSeconds(1);
                        }
                        else
                        {
                            CalculateTo = Functions.fnDate(objSQLReader["DAYEND"].ToString());
                        }
                    }
                    else
                    {
                        CalculateTo = FormatEndDate.AddSeconds(1);
                    }

                    ts = CalculateTo.Subtract(CalculateFrom);

                    intCalculateTime = (ts.Days * 24 + ts.Hours);
                    if (intCalculateTime > 0) strTime = Functions.fnInt32(intCalculateTime * 60);

                    dblRate = Functions.fnDouble(objSQLReader["EMPRATE"].ToString());
                    double minRate = 0;
                    if (ts.Minutes > 0)
                    {

                        if (dblRate == 0)
                        {
                            minRate = 0;
                        }
                        else
                        {
                            minRate = dblRate / 60;
                        }
                        if (strTime > 0) strTime = strTime + ts.Minutes;
                        else strTime = ts.Minutes;
                    }

                    dblCalculateRate = Functions.fnDouble(Functions.fnDouble(intCalculateTime * dblRate + Functions.fnDouble(ts.Minutes) * minRate).ToString("f"));

                    dtbl.Rows.Add(new object[] {
                                                true,
                                                objSQLReader["EMPID"].ToString(),
                                                objSQLReader["EMPLOYEEID"].ToString(),
                                                objSQLReader["LASTNAME"].ToString() + ", " + objSQLReader["FIRSTNAME"].ToString(),
                                                Functions.fnDouble(objSQLReader["EMPRATE"].ToString()).ToString("f"),
                                                strTime.ToString(), dblCalculateRate.ToString()});

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

        #region Fetch Data For Sale Report

        public DataTable FetchSaleReportData(string refType, DataTable refdtbl, DateTime pStart, DateTime pEnd, string sortby)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;

            if (refType != "0")
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

            if (refType == "4")
            {
                if (getdtblID != "")
                {
                    if (chkcnt == refdtbl.Rows.Count) getdtblID = getdtblID + ",0";
                }
            }

            if (getdtblID != "")
            {
                if (refType == "1") strSQL1 = " and a.DepartmentID in ( " + getdtblID + " )";
                if (refType == "2") strSQL1 = " and a.CategoryID in ( " + getdtblID + " )";
                if (refType == "4") strSQL1 = " and t.EmployeeID in ( " + getdtblID + " )";
                if (refType == "3") strSQL1 = " and p.BrandID in ( " + getdtblID + " )";
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T','B')";

            if (refType == "0")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount,'' as FilterID, '' as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }


            if (refType == "1")
            {
                strSQLComm = " select a.ProductType as PT, a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount,b.DepartmentID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                           + " left outer join  Dept AS b on a.DepartmentID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X'  and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "2")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount,b.CategoryID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X'  and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "3")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount,br.BrandID as FilterID, br.BrandDescription as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X'  and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "4")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount,isnull(b.EmployeeID,0) as FilterID,isnull(b.LastName + ' ' + b.FirstName,'SUPER USER') as FilterDesc,"
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                           + " left outer join Employee AS b ON b.ID = t.EmployeeID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X'  " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "H")
                        || (objSQLReader["PT"].ToString() == "Z")) continue;

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["UPC"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString(),
                                                   objSQLReader["FilterID"].ToString(),
                                                   objSQLReader["FilterDesc"].ToString()});
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

        public DataTable FetchSaleReportDataForSpecificSKU(DateTime pStart, DateTime pEnd, int pSKU, string configDateFormat)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            strSQL1 = " and a.ProductID = " + pSKU.ToString();

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T','B')";

            strSQLComm =    " select a.ProductType as PT,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost, "
                          + " isnull(a.Discount,0) as Discount,i.ID as InvoiceNo, t.TransDate as InvoiceDate, isnull(p.DiscountedCost,0) as DCost "
                          + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                          + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                          + " left outer join  Product p on a.ProductID = p.ID "
                          + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                          + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                          + " Order by i.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                         || (objSQLReader["PT"].ToString() == "H")) continue;

                    dtbl.Rows.Add(new object[] {   objSQLReader["InvoiceNo"].ToString(),
                                                   Functions.fnDate(objSQLReader["InvoiceDate"].ToString()).ToString(configDateFormat),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString()});
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

        public DataTable FetchMatrixItemSaleReportData(int mtxid, int mtxopid, string mop1, string mop2, string mop3, DataTable refdtbl1, DataTable refdtbl2,
                                                        DataTable refdtbl3, DateTime pStart, DateTime pEnd, string pTaxIncludeRate)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";

            string getdtblID1 = "";
            string getdtblID2 = "";
            string getdtblID3 = "";

            foreach (DataRow dr in refdtbl1.Rows)
            {
                if (getdtblID1 == "")
                {
                    getdtblID1 = "'" + dr["value"].ToString() + "'";
                }
                else
                {
                    getdtblID1 = getdtblID1 + "," + "'" + dr["value"].ToString() + "'";
                }
            }


            foreach (DataRow dr in refdtbl2.Rows)
            {
                if (getdtblID2 == "")
                {
                    getdtblID2 = "'" + dr["value"].ToString() + "'";
                }
                else
                {
                    getdtblID2 = getdtblID2 + "," + "'" + dr["value"].ToString() + "'";
                }
            }

            foreach (DataRow dr in refdtbl3.Rows)
            {
                if (getdtblID3 == "")
                {
                    getdtblID3 = "'" + dr["value"].ToString() + "'";
                }
                else
                {
                    getdtblID3 = getdtblID3 + "," + "'" + dr["value"].ToString() + "'";
                }
            }

            if (getdtblID1 != "") strSQL1 = " and im.OptionValue1 in ( " + getdtblID1 + " )";
            if (getdtblID2 != "") strSQL2 = " and im.OptionValue2 in ( " + getdtblID2 + " )";
            if (getdtblID3 != "") strSQL3 = " and im.OptionValue3 in ( " + getdtblID3 + " )";


            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype = 'M' ";

            strSQLComm = " select isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.TaxIncludeRate,0) as PriceI, isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount, "
                       + " im.OptionValue1,im.OptionValue2,im.OptionValue3, isnull(p.DiscountedCost,0) as DCost "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " left outer join Product p on a.ProductID = p.ID "
                       + " left outer join ItemMatrixOptions im on a.ID = im.ItemID "
                       + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                       + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' "
                       + " and p.ProductStatus = 'Y' and im.MatrixOptionID > 0 and a.ProductID = @PID and im.MatrixOptionID = @MOID "
                       + strSQL1 + strSQL2 + strSQL3 + strSQLDate
                       + " order by im.OptionValue1,im.OptionValue2,im.OptionValue3 ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = mtxid;

                objSQlComm.Parameters.Add(new SqlParameter("@MOID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MOID"].Value = mtxopid;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MatrixOption1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MatrixOption2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MatrixOption3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    double rev = 0;
                    double cp = 0;
                    double dcp = 0;
                    double pft = 0;
                    double mrgn = 0;
                    rev = pTaxIncludeRate == "N" ? (Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnDouble(objSQLReader["Price"].ToString())) -
                        Functions.fnDouble(objSQLReader["Discount"].ToString()) : (Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnDouble(objSQLReader["PriceI"].ToString()));

                    cp = (Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnDouble(objSQLReader["Cost"].ToString()));
                    dcp = (Functions.fnDouble(objSQLReader["Qty"].ToString()) * Functions.fnDouble(objSQLReader["DiscountedCost"].ToString()));

                    if (rev == 0)
                    {
                        pft = rev - cp;
                        mrgn = -99999;
                    }
                    else
                    {
                        pft = rev - cp;
                        mrgn = pft * 100 / rev;
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["OptionValue1"].ToString(),
                                                   objSQLReader["OptionValue2"].ToString(),
                                                   objSQLReader["OptionValue3"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   cp,dcp,
                                                   rev,pft,mrgn });
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

        public DataTable FetchPaidInOutReportData(string filter, DateTime pStart, DateTime pEnd, string configDateFormat)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strFilter = "";

            if (filter == "All")
            {
                strFilter = "and tr.TransType in (6, 66)";
            }
            if (filter == "Paid In")
            {
                strFilter = "and tr.TransType = 66";
            }
            if (filter == "Paid Out")
            {
                strFilter = "and tr.TransType = 6";
            }

            strSQLDate = " and tr.TransDate between @SDT and @TDT ";


            strSQLComm = " select tr.TransDate, tr.TransType, td.TenderAmount, isnull(n.Note,'') notetxt, "
                       + " isnull(e.FirstName,'') FN, isnull(e.LastName,'') LN, tr.EmployeeID from Trans tr left outer join Tender td on tr.ID = td.TransactionNo "
                       + " left outer join Notes n on n.ID = tr.TransNoteNo left outer join Employee e on e.ID = tr.EmployeeID"
                       + " where (1 = 1) " + strSQLDate + strFilter
                       + " order by tr.TransType desc, tr.TransDate ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TranType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranAmount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranNotes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Staff", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["TransType"].ToString(),
                        objSQLReader["TransType"].ToString() == "6" ? "Paid Out" : "Paid In",
                        Functions.fnDate(objSQLReader["TransDate"].ToString()).ToString(configDateFormat),
                        objSQLReader["TenderAmount"].ToString(),
                        objSQLReader["notetxt"].ToString(),
                        Functions.fnInt32(objSQLReader["EmployeeID"].ToString()) == 0 ? "Admin" : objSQLReader["FN"].ToString() + " " + objSQLReader["LN"].ToString() });
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


        public DataTable FetchAllMatrixItemSaleReportData(DateTime pStart, DateTime pEnd, string configDateFormat, string pTaxIncludeRate)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype = 'M' ";

            strSQLComm = " select p.SKU,p.Description,DATEADD(D, 0, DATEDIFF(D, 0, t.TransDate)) as TDate,isnull(sum(a.Qty),0) as Qty,"
                       + " isnull(sum(a.Price),0) as Price,  isnull(sum(a.TaxIncludeRate),0) as PriceI, isnull(sum(a.Cost),0) as Cost,isnull(sum(a.Discount),0) as Discount, "
                       + " im.OptionValue1,im.OptionValue2,im.OptionValue3,m.Option1Name,m.Option2Name,m.Option3Name, isnull(sum(p.DiscountedCost),0) as DCost "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " left outer join Product p on a.ProductID = p.ID "
                       + " left outer join ItemMatrixOptions im on a.ID = im.ItemID "
                       + " left outer join MatrixOptions m on p.ID = m.ProductID "
                       + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                       + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' "
                       + " and p.ProductStatus = 'Y' and im.MatrixOptionID > 0 " + strSQLDate
                       + " group by DATEADD(D, 0, DATEDIFF(D, 0, t.TransDate)),p.SKU,p.Description,im.OptionValue1,im.OptionValue2,im.OptionValue3,m.Option1Name,m.Option2Name,m.Option3Name "
                       + " order by TDate desc,p.SKU,p.Description,im.OptionValue1,im.OptionValue2,im.OptionValue3 ";



            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDetails", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));

                while (objSQLReader.Read())
                {
                    double rev = 0;
                    double cp = 0;
                    double dcp = 0;
                    double pft = 0;
                    double mrgn = 0;
                    string dtls = "";
                    rev = pTaxIncludeRate == "N" ? Functions.fnDouble(objSQLReader["Price"].ToString()) - Functions.fnDouble(objSQLReader["Discount"].ToString()) : Functions.fnDouble(objSQLReader["PriceI"].ToString());

                    cp = Functions.fnDouble(objSQLReader["Cost"].ToString());
                    dcp = Functions.fnDouble(objSQLReader["DCost"].ToString());

                    if (rev == 0)
                    {
                        pft = rev - cp;
                        mrgn = -99999;
                    }
                    else
                    {
                        pft = rev - cp;
                        mrgn = pft * 100 / rev;
                    }

                    if (objSQLReader["OptionValue1"].ToString() != "")
                    {
                        dtls = objSQLReader["Option1Name"].ToString() + " - " + objSQLReader["OptionValue1"].ToString();
                    }
                    if (objSQLReader["OptionValue2"].ToString() != "")
                    {
                        dtls = dtls == "" ? objSQLReader["Option2Name"].ToString() + ": " + objSQLReader["OptionValue2"].ToString() : dtls + ", " + objSQLReader["Option2Name"].ToString() + ": " + objSQLReader["OptionValue2"].ToString();
                    }

                    if (objSQLReader["OptionValue3"].ToString() != "")
                    {
                        dtls = dtls == "" ? objSQLReader["Option3Name"].ToString() + ": " + objSQLReader["OptionValue3"].ToString() : dtls + ", " + objSQLReader["Option3Name"].ToString() + ": " + objSQLReader["OptionValue2"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {
                                                   Functions.fnDate(objSQLReader["TDate"].ToString()).ToString(configDateFormat),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   dtls,
                                                   objSQLReader["Qty"].ToString(),
                                                   cp,dcp,
                                                   rev,pft,mrgn });
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

        public DataTable FetchRentReportData(string refType, DataTable refdtbl, DateTime pStart, DateTime pEnd, string sortby)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;
            if (refType != "0")
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

            if (refType == "4")
            {
                if (getdtblID != "")
                {
                    if (chkcnt == refdtbl.Rows.Count) getdtblID = getdtblID + ",0";
                }
            }

            if (getdtblID != "")
            {
                if (refType == "1") strSQL1 = " and a.DepartmentID in ( " + getdtblID + " )";
                if (refType == "2") strSQL1 = " and a.CategoryID in ( " + getdtblID + " )";
                if (refType == "4") strSQL1 = " and t.EmployeeID in ( " + getdtblID + " )";
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            if (refType == "0")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount, isnull(a.RentApplicable,'') as RA, "
                           + " isnull(a.RentDuration,0) as RD,'' as FilterID, '' as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 15 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "1")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount, isnull(a.RentApplicable,'') as RA, "
                           + " isnull(a.RentDuration,0) as RD,b.DepartmentID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join  Dept AS b on a.DepartmentID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 15 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "2")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount,isnull(a.RentApplicable,'') as RA,"
                           + " isnull(a.RentDuration,0) as RD,b.CategoryID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 15 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "4")
            {
                strSQLComm = " select a.ProductType as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount,isnull(a.RentApplicable,'') as RA, "
                           + " isnull(a.RentDuration,0) as RD,isnull(b.EmployeeID,0) as FilterID,isnull(b.LastName + ' ' + b.FirstName,'SUPER USER') as FilterDesc,"
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join Employee AS b ON b.ID = t.EmployeeID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 15 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentApplicable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    double qty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    if (qty < 0) qty = -qty;
                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["UPC"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   qty.ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString(),
                                                   objSQLReader["RA"].ToString(), 
                                                   objSQLReader["RD"].ToString(),
                                                   objSQLReader["FilterID"].ToString(),
                                                   objSQLReader["FilterDesc"].ToString()});
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

        public DataTable FetchRepairReportData(string refType, DataTable refdtbl, DateTime pStart, DateTime pEnd, string sortby)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;
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

            if (refType == "Employee")
            {
                if (getdtblID != "")
                {
                    if (chkcnt == refdtbl.Rows.Count) getdtblID = getdtblID + ",0";
                }
            }

            if (getdtblID != "")
            {
                if (refType == "Department") strSQL1 = " and a.DepartmentID in ( " + getdtblID + " )";
                if (refType == "POS Screen Category") strSQL1 = " and a.CategoryID in ( " + getdtblID + " )";
                if (refType == "Employee") strSQL1 = " and t.EmployeeID in ( " + getdtblID + " )";
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            if (refType == "Department")
            {
                strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount, "
                           + " isnull(a.RepairItemPurchaseDate,'') as RD,b.DepartmentID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join  Dept AS b on a.DepartmentID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 17 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "POS Screen Category")
            {
                strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price, isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount,"
                           + " isnull(a.RentDuration,0) as RD,b.CategoryID as FilterID, b.Description as FilterDesc, "
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 17 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            if (refType == "Employee")
            {
                strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,isnull(a.Cost,0) as Cost,isnull(a.Discount,0) as Discount,"
                           + " isnull(a.RentDuration,0) as RD,isnull(b.EmployeeID,0) as FilterID,isnull(b.LastName + ' ' + b.FirstName,'SUPER USER') as FilterDesc,"
                           + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand,isnull(p.DiscountedCost,0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID " + strSQLProduct
                           + " left outer join Employee AS b ON b.ID = t.EmployeeID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and i.status = 17 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " Order by FilterDesc " + sortby + ",Qty desc, a.SKU, a.Description ";
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentApplicable", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    double qty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    if (qty < 0) qty = -qty;
                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["UPC"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   qty.ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString(),
                                                   objSQLReader["RA"].ToString(), 
                                                   objSQLReader["RD"].ToString(),
                                                   objSQLReader["FilterID"].ToString(),
                                                   objSQLReader["FilterDesc"].ToString()});
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

        #region Fetch Customer For Sale Report

        public DataTable FetchCustomer(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select c.ID, c.CustomerID, c.Company, c.LastName, c.FirstName + ' ' +c.LastName as CustName, "
                       + " c.WorkPhone, sum(i.TotalSale) as TS from Trans t "
                       + " left outer join Invoice i on t.ID = i.TransactionNo  "
                       + " left outer join Customer C on t.CustomerID = c.ID "
                       + " where t.CustomerID > 0 and t.TransType in( 1,4,18 ) and i.status in(3,18) " + strSQLDate
                       + " group by c.ID, c.CustomerID, c.Company,c.FirstName, c.LastName, c.WorkPhone ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
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
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Total", System.Type.GetType("System.String"));

                
                while (objSQLReader.Read())
                {
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["LastName"].ToString(), 
                                                   objSQLReader["CustName"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(), 
                                                   objSQLReader["TS"].ToString()});
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

        public DataTable FetchCustomerForRent(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select c.ID, c.CustomerID, c.Company, c.LastName, c.FirstName + ' ' +c.LastName as CustName, "
                       + " c.WorkPhone, sum(i.TotalSale) as TS from Trans t "
                       + " left outer join Invoice i on t.ID = i.TransactionNo  "
                       + " left outer join Customer C on t.CustomerID = c.ID "
                       + " where t.CustomerID > 0 and i.status = 15 " + strSQLDate
                       + " group by c.ID, c.CustomerID, c.Company,c.FirstName, c.LastName, c.WorkPhone ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
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
                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Total", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["CustomerID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["LastName"].ToString(), 
                                                   objSQLReader["CustName"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(), 
                                                   objSQLReader["TS"].ToString()});
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

        #region Fetch Data For Customer Sale Report

        public DataTable FetchCustomerSaleReportData(string refType, string refCustID, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            if (refCustID != "")
            {
                strSQLCustomer = " and t.CustomerID in (" + refCustID + ")";
            }

            if (refType == "Department") strSQLOrder = " Order By c.CustomerID, b.DepartmentID, t.TransDate ";
            if (refType == "SKU") strSQLOrder = " Order By c.CustomerID, a.SKU, t.TransDate ";
            if (refType == "Date") strSQLOrder = " Order By c.CustomerID, t.TransDate, a.SKU ";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X','B')";

            strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,a.Qty,a.Price,a.TaxIncludeRate as PriceI,a.Cost,a.InvoiceNo,a.Discount, "
                       + " b.DepartmentID as DeptID, b.Description as Department, "
                       + " c.CustomerID as CustID,c.Company, c.FirstName + ' ' + c.LastName as Customer,t.TransDate, "
                       + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand, "
                       + " c.Company,c.Address1,c.Address2,c.City,c.State,c.Zip,c.HomePhone,c.Email,isnull(p.DiscountedCost,0) as DCost "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                     //+ " and t.TransType in (1,4) and i.status = 3 " 
                       + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " left outer join Customer AS c ON c.ID = t.CustomerID "
                       + " left outer join Dept AS b ON a.DepartmentID = b.ID "
                       + " left outer join Product p on a.ProductID = p.ID "
                       + " left outer join BrandMaster br on p.BrandID = br.ID "
                       + " where (1 = 1) and t.CustomerID > 0 "
                       + " and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' " + strSQLCustomer + strSQLDate
                       + strSQLOrder;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PriceI", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));

                string addr = "";
                string strcitystatezip = "";
                string strCompany = "";

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    if (objSQLReader["SKU"].ToString() == "") continue;

                    addr = "";
                    strcitystatezip = "";
                    strCompany = "";
                    strCompany = objSQLReader["Company"].ToString();
                    string strAddress1 = objSQLReader["Address1"].ToString();
                    string strAddress2 = objSQLReader["Address2"].ToString();
                    string strCity = objSQLReader["City"].ToString();
                    string strState = objSQLReader["State"].ToString();
                    string strZip = objSQLReader["Zip"].ToString();
                    string strWorkPhone = objSQLReader["HomePhone"].ToString();


                    if (strCompany != "") addr = strCompany + "\r\n";
                    if (strAddress1 != "") addr = addr + strAddress1 + "\r\n";
                   
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

                    if (objSQLReader["Email"].ToString() != "")
                    {
                        addr = addr + "Email : " + objSQLReader["Email"].ToString();
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["UPC"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(),
                                                    objSQLReader["PriceI"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString(),
                                                   objSQLReader["DeptID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(),
                                                   addr,
                                                   objSQLReader["InvoiceNo"].ToString(), 
                                                   Functions.fnDate(objSQLReader["TransDate"].ToString()).ToShortDateString()});
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

        public DataTable FetchCustomerRentReportData(string refType, string refCustID, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            if (refCustID != "")
            {
                strSQLCustomer = " and t.CustomerID in (" + refCustID + ")";
            }

            if (refType == "Department") strSQLOrder = " Order By c.CustomerID, b.DepartmentID, t.TransDate ";
            if (refType == "SKU") strSQLOrder = " Order By c.CustomerID, a.SKU, t.TransDate ";
            if (refType == "Date") strSQLOrder = " Order By c.CustomerID, t.TransDate, a.SKU ";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";

            strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,a.Qty,a.Price,a.Cost,a.InvoiceNo,a.Discount, "
                       + " b.DepartmentID as DeptID, b.Description as Department, "
                       + " c.CustomerID as CustID,c.Company, c.FirstName + ' ' + c.LastName as Customer,t.TransDate, "
                       + " isnull(p.UPC,'') as UPC,isnull(p.Season,'') as Season,isnull(br.BrandDescription,'') as Brand "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " left outer join item a ON a.InvoiceNo = i.ID " + strSQLProduct
                       + " left outer join Customer AS c ON c.ID = t.CustomerID "
                       + " left outer join Dept AS b ON a.DepartmentID = b.ID "
                       + " left outer join Product p on a.ProductID = p.ID "
                       + " left outer join BrandMaster br on p.BrandID = br.ID "
                       + " where (1 = 1) and t.CustomerID > 0 "
                       + " and i.status = 15 and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQLCustomer + strSQLDate
                       + strSQLOrder;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                dtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Season", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                         || (objSQLReader["PT"].ToString() == "H")) continue;
                    if (objSQLReader["SKU"].ToString() == "") continue;
                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Brand"].ToString(),
                                                   objSQLReader["UPC"].ToString(),
                                                   objSQLReader["Season"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DeptID"].ToString(),
                                                   objSQLReader["Department"].ToString(),
                                                   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(),
                                                   objSQLReader["InvoiceNo"].ToString(), 
                                                   objSQLReader["TransDate"].ToString()});
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

        #region Fetch Data For Vendor Sale Report

        public DataTable FetchVendorSaleReportData(DataTable refdtbl, DateTime pStart, DateTime pEnd, string psort)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";
            try
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
                    }
                }
                if (getdtblID != "")
                {
                    strSQL1 = " and v.ID in ( " + getdtblID + " )";
                }
            }
            catch
            {
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            strSQLComm = " select a.Producttype as PT,a.SKU,a.Description,a.Qty,a.Price,a.Cost,a.Discount, "
                       + " b.DepartmentID as FilterID, b.Description as FilterDesc, "
                       + " isnull(v.VendorID,'') as VendorID,isnull(v.Name,'') as Vendor ,vp.PartNumber,"
                       + " p.QtyOnHand,p.Cost as PCost "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " LEFT OUTER JOIN item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " left outer join Product p on a.ProductID = p.ID "
                       + " LEFT OUTER JOIN  VendPart AS vp ON a.ProductID = vp.ProductID "
                       + " LEFT OUTER JOIN  Vendor AS v ON vp.VendorID = v.ID "
                       + " LEFT OUTER JOIN  Dept AS b ON a.DepartmentID = b.ID "
                       + " where (1 = 1) and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                       + " Order by VendorID " + psort + ", FilterID, a.SKU, a.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["PCost"].ToString(),
                                                   objSQLReader["FilterID"].ToString(),
                                                   objSQLReader["FilterDesc"].ToString(),
                                                   objSQLReader["VendorID"].ToString(),
                                                   objSQLReader["Vendor"].ToString(),
                                                   objSQLReader["PartNumber"].ToString(),
                                                   objSQLReader["QtyOnHand"].ToString()});
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

        #region Fetch Data For Day Of Week Sale Report

        public DataTable FetchDayOfWeekSaleReportData(DataTable refdtbl, DateTime pStart, DateTime pEnd, string pTaxInclusive)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";
            try
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
                    }
                }
                if (getdtblID != "")
                {
                    strSQL1 = " and m.DepartmentID in ( " + getdtblID + " )";
                }
            }
            catch
            {
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F')";

            strSQLComm = " select Distinct i.ID, DatePart(Weekday,t.TransDate) as WeekNo, "
                       + " isnull((i.TotalSale - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale, isnull((i.TotalSale),0) as TotSaleI "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " left outer join item m On m.invoiceno = i.id "
                       + " where (1 = 1) and t.TransType in( 1,4,18 ) and i.status in(3,18) and m.Tagged <> 'X' " + strSQL1 + strSQLDate
                         //+ " Group by DatePart(Weekday,t.TransDate) "
                       + " order by DatePart(Weekday,t.TransDate) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("WeekDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                int intwkno = 0;
                string strwkday = "";
                
                while (objSQLReader.Read())
                {
                    intwkno = 0;
                    strwkday = "";
                    intwkno = Functions.fnInt32(objSQLReader["WeekNo"].ToString());
                    if (intwkno == 1) strwkday = "Sunday";
                    else if (intwkno == 2) strwkday = "Monday";
                    else if (intwkno == 3) strwkday = "Tuesday";
                    else if (intwkno == 4) strwkday = "Wednesday";
                    else if (intwkno == 5) strwkday = "Thursday";
                    else if (intwkno == 6) strwkday = "Friday";
                    else strwkday = "Saturday";
                    dtbl.Rows.Add(new object[] {   strwkday,
                                                   pTaxInclusive == "N" ?  Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSale"].ToString()).ToString("f2")) : Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSaleI"].ToString()).ToString("f2"))});
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

        public DataTable FetchDayOfWeekRentReportData(DataTable refdtbl, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate1 = "";
            string strSQLDate2 = "";
            string strSQL1 = "";
            string getdtblID = "";
            try
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
                    }
                }
                if (getdtblID != "")
                {
                    strSQL1 = " and m.DepartmentID in ( " + getdtblID + " )";
                }
            }
            catch
            {
            }
            strSQLDate1 = " and t.TransDate between @SDT1 and @TDT1 ";
            strSQLDate2 = " and t.TransDate between @SDT2 and @TDT2 ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F')";

            strSQLComm = " select Distinct i.ID, DatePart(Weekday,t.TransDate) as WeekNo, "
                       + " isnull((i.TotalSale - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " left outer join item m On m.invoiceno = i.id "
                       + " where (1 = 1) and i.status = 15 and i.IsRentCalculated = 'N' and m.Tagged <> 'X' " + strSQL1 + strSQLDate1
                       + " union "
                       + " select Distinct i.ID, DatePart(Weekday,t.TransDate) as WeekNo, "
                       + " isnull((i.TotalSale - i.RentDeposit - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale "
                       + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " left outer join item m On m.invoiceno = i.id "
                       + " where (1 = 1) and i.status = 16 and i.IsRentCalculated = 'Y' and m.Tagged <> 'X' " + strSQL1 + strSQLDate2
                       + " order by DatePart(Weekday,t.TransDate) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT1"].Value = FormatEndDate;


                objSQlComm.Parameters.Add(new SqlParameter("@SDT2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT2"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT2"].Value = FormatEndDate;


                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("WeekDay", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                int intwkno = 0;
                string strwkday = "";

                while (objSQLReader.Read())
                {
                    intwkno = 0;
                    strwkday = "";
                    intwkno = Functions.fnInt32(objSQLReader["WeekNo"].ToString());
                    if (intwkno == 1) strwkday = "Sunday";
                    else if (intwkno == 2) strwkday = "Monday";
                    else if (intwkno == 3) strwkday = "Tuesday";
                    else if (intwkno == 4) strwkday = "Wednesday";
                    else if (intwkno == 5) strwkday = "Thursday";
                    else if (intwkno == 6) strwkday = "Friday";
                    else strwkday = "Saturday";
                    dtbl.Rows.Add(new object[] {   strwkday,
												   Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSale"].ToString()).ToString("f2"))});
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

        #region Fetch Data For Monthly Sale Report

        public DataTable FetchMonthlySaleReportData(DataTable refdtbl, DateTime pStart, DateTime pEnd, string pTaxInclusive)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";
            try
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
                    }
                }
                if (getdtblID != "")
                {
                    strSQL1 = " and m.DepartmentID in ( " + getdtblID + " )";
                }
            }
            catch
            {
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F')";

            strSQLComm = " select Distinct i.ID, DatePart(Month,t.TransDate) as Month, DatePart(Year,t.TransDate) as Year, "
                         + " isnull((i.TotalSale - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale, isnull((i.TotalSale),0) as TotSaleI "
                         + " from Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                         + " left outer join item m On m.invoiceno = i.id "
                         + " where (1 = 1) and t.TransType in( 1,4,18 ) and i.status in(3,18) and m.Tagged <> 'X' " + strSQL1 + strSQLDate
                         //+ " Group by DatePart(Month,t.TransDate), DatePart(Year,t.TransDate) "
                         + " order by DatePart(Month,t.TransDate), DatePart(Year,t.TransDate) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("YearMonth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    intmn = 0;
                    intyr = 0;
                    strmn = "";
                    intmn = Functions.fnInt32(objSQLReader["Month"].ToString());
                    intyr = Functions.fnInt32(objSQLReader["Year"].ToString());
                    if (intmn == 1) strmn = "Jan";
                    else if (intmn == 2) strmn = "Feb";
                    else if (intmn == 3) strmn = "Mar";
                    else if (intmn == 4) strmn = "Apr";
                    else if (intmn == 5) strmn = "May";
                    else if (intmn == 6) strmn = "Jun";
                    else if (intmn == 7) strmn = "Jul";
                    else if (intmn == 8) strmn = "Aug";
                    else if (intmn == 9) strmn = "Sep";
                    else if (intmn == 10) strmn = "Oct";
                    else if (intmn == 11) strmn = "Nov";
                    else strmn = "Dec";

                    dtbl.Rows.Add(new object[] {   strmn+intyr.ToString().Substring(2,2),
                                                   pTaxInclusive == "N" ? Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSale"].ToString()).ToString("f2")) :
                                                   Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSaleI"].ToString()).ToString("f2"))});
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

        public DataTable FetchMonthlyRentReportData(DataTable refdtbl, DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate1 = "";
            string strSQLDate2 = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";
            try
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
                    }
                }
                if (getdtblID != "")
                {
                    strSQL1 = " and m.DepartmentID in ( " + getdtblID + " )";
                }
            }
            catch
            {
            }

            strSQLDate1 = " and t.TransDate between @SDT1 and @TDT1 ";

            strSQLDate2 = " and t.TransDate between @SDT2 and @TDT2 ";

            strSQLComm =   " select Distinct i.ID, DatePart(Month,t.TransDate) as Month, DatePart(Year,t.TransDate) as Year, "
                         + " isnull((i.TotalSale - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale "
                         + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                         + " left outer join item m On m.invoiceno = i.id "
                         + " where (1 = 1) and i.status = 15 and i.IsRentCalculated = 'N' and m.Tagged <> 'X' " + strSQL1 + strSQLDate1
                         + " union "
                         + " select Distinct i.ID, DatePart(Month,t.TransDate) as Month, DatePart(Year,t.TransDate) as Year, "
                         + " isnull((i.TotalSale - i.RentDeposit - i.Tax1 - i.Tax2 - i.Tax3 - i.Fees - i.FeesTax - i.FeesCoupon - i.FeesCouponTax),0) as TotSale "
                         + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                         + " left outer join item m On m.invoiceno = i.id "
                         + " where (1 = 1) and i.status = 16 and i.IsRentCalculated = 'Y' and m.Tagged <> 'X' " + strSQL1 + strSQLDate2
                         + " order by DatePart(Month,t.TransDate), DatePart(Year,t.TransDate) ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT1"].Value = FormatEndDate;


                objSQlComm.Parameters.Add(new SqlParameter("@SDT2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT2"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT2", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT2"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("YearMonth", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    intmn = 0;
                    intyr = 0;
                    strmn = "";
                    intmn = Functions.fnInt32(objSQLReader["Month"].ToString());
                    intyr = Functions.fnInt32(objSQLReader["Year"].ToString());
                    if (intmn == 1) strmn = "Jan";
                    else if (intmn == 2) strmn = "Feb";
                    else if (intmn == 3) strmn = "Mar";
                    else if (intmn == 4) strmn = "Apr";
                    else if (intmn == 5) strmn = "May";
                    else if (intmn == 6) strmn = "Jun";
                    else if (intmn == 7) strmn = "Jul";
                    else if (intmn == 8) strmn = "Aug";
                    else if (intmn == 9) strmn = "Sep";
                    else if (intmn == 10) strmn = "Oct";
                    else if (intmn == 11) strmn = "Nov";
                    else strmn = "Dec";

                    dtbl.Rows.Add(new object[] {   strmn+intyr.ToString().Substring(2,2),
												   Functions.fnDouble(Functions.fnDouble(objSQLReader["TotSale"].ToString()).ToString("f2"))});
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

        #region Fetch Data For Customer Purchase Report

        public DataTable FetchCustomerPurchaseReportData(DateTime pStart, DateTime pEnd, string thisstore)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and c.DateLastPurchase between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";

            strSQLComm = " select distinct c.ID as CID,c.CustomerID as CustID,c.Company,c.LastName + ', '+c.FirstName as Customer,"
                       + " c.Address1,c.City,c.State,c.Zip,c.WorkPhone,c.Email,c.TotalPurchases,c.DateLastPurchase,c.AmountLastPurchase, "
                       + " isnull(gr.Description,'') as CustGroup,isnull(cl.Description,'') as CustClass from Customer as c "
                       + " left outer join GeneralMapping gmG "
                       + " on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                       + " left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                       + " gmC.ReferenceType = 'Class' "
                       + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                       + " gmG.ReferenceType = 'Group' and c.ID = gmG.MappingID "
                       + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                       + " gmC.ReferenceType = 'Class' and c.ID = gmC.MappingID "
                       + " where (1 = 1) and c.issuestore=@store " + strSQLDate + " order by c.DateLastPurchase ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@store"].Value = thisstore;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["TotalPurchases"].ToString(),
                                                   objSQLReader["DateLastPurchase"].ToString(), 
                                                   objSQLReader["AmountLastPurchase"].ToString(),
                                                   objSQLReader["Email"].ToString() ,
                                                   objSQLReader["CustGroup"].ToString(),
                                                   objSQLReader["CustClass"].ToString()});
                    
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

        #region Fetch Data For Customer House Account Summary

        public DataTable FetchCustomerHAReportData(string thisstore)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select distinct c.ID as CID, c.CustomerID as CustID,c.Company,c.LastName + ', '+c.FirstName as Customer, "
                       + " c.Email,Sum(a.Amount) as Balance from AcctRecv a left outer join Customer as c on a.CustomerID = c.ID "
                       + " where (1 = 1) and c.ID is not null and c.issuestore = '" + thisstore + "' "
                       + " group by c.ID,c.CustomerID,c.Company,c.LastName,c.FirstName,c.Email order by c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Balance", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Email"].ToString(), 
                                                   objSQLReader["Balance"].ToString()});

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

        public DataTable FetchCustomerHAOpening(int CustID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select isnull(date,'') as date, isnull(amount,0) as amount from acctrecv   "
                        + " where customerid = @custid and TranType = 6 ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@custid", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@custid"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["amount"].ToString(),
                                                   objSQLReader["date"].ToString()});
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

        public DataTable FetchCustomerHALastPay(string thisstore)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select customerid, max(date) as mxdate, amount from acctrecv where TranType = 3 "
                              + " and issuestore = '" + thisstore + "' group by customerid,date,amount order by customerid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["customerid"].ToString(),
                                                   - Functions.fnDouble(objSQLReader["amount"].ToString()),
                                                   objSQLReader["mxdate"].ToString()
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

        public double FetchCustomerHAPayment(int inv)
        {
            double val = 0;

            string strSQLComm = " select isnull(sum(price * qty),0) as samt from item where invoiceno = @i and producttype = 'A' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            objSQlComm.Parameters.Add(new SqlParameter("@i", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@i"].Value = inv;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnDouble(objSQLReader["samt"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        #region Fetch Customer Purchase Amount

        public string FetchCustomerPurchaseAmount(int CustID, DateTime pStart, DateTime pEnd)
        {
            string dblAmt = "0";
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select isnull(sum(i.TotalSale),'0') as TS "
                       + " from Trans t left outer join invoice i on t.ID = i.TransactionNo  "
                       + " Where (1 = 1) and t.CustomerID = @CustID and t.TransType in( 1,4,18 ) and i.status in (3,18) " + strSQLDate;


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@CustID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustID"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dblAmt = objSQLReader["TS"].ToString();

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblAmt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return "0";
            }
        }

        public DataTable FetchAllCustomerPurchaseAmount(DateTime pStart, DateTime pEnd)
        {
            DataTable dtbl = new DataTable();
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
            dtbl.Columns.Add("City", System.Type.GetType("System.String"));
            dtbl.Columns.Add("State", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm =   " select Distinct t.CustomerID as CID,isnull(sum(distinct i.TotalSale),'0') as TS,c.CustomerID as CustID,c.Company, "
                         + " c.LastName + ', '+c.FirstName as Customer,c.Address1, c.City, c.State, c.Zip, c.WorkPhone,c.Email, "
                         + " c.DateLastPurchase, c.AmountLastPurchase,isnull(gr.Description,'') as CustGroup,isnull(cl.Description,'') as CustClass"
                         + " from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                         + " left outer join customer c on c.ID = t.CustomerID  "
                         + " left outer join GeneralMapping gmG "
                         + " on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                         + " left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' "
                         + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                         + " gmG.ReferenceType = 'Group' and c.ID = gmG.MappingID "
                         + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' and c.ID = gmC.MappingID "
                         + " where (1 = 1) and t.CustomerID > 0 and t.TransType in( 1,4,18 ) and i.status in (3,18) " + strSQLDate
                         + " group by t.CustomerID,c.CustomerID,c.Company,c.LastName + ', '+c.FirstName,c.Address1, c.City, "
                         + " c.State, c.Zip,c.WorkPhone,c.Email,c.DateLastPurchase, c.AmountLastPurchase,gr.Description,cl.Description order by c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["TS"].ToString(),
                                                   objSQLReader["DateLastPurchase"].ToString(), 
                                                   objSQLReader["AmountLastPurchase"].ToString(),
                                                   objSQLReader["Email"].ToString(),
                    objSQLReader["CustGroup"].ToString(),
                    objSQLReader["CustClass"].ToString()});

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

        public DataTable FetchAllCustomerPurchaseAmount1(DateTime pStart, DateTime pEnd)
        {
            DataTable dtbl = new DataTable();
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
            dtbl.Columns.Add("City", System.Type.GetType("System.String"));
            dtbl.Columns.Add("State", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select Distinct t.CustomerID as CID,isnull(sum(distinct i.TotalSale),'0') as TS,c.CustomerID as CustID,c.Company, "
                         + " c.LastName + ', '+c.FirstName as Customer,c.Address1, c.City, c.State, c.Zip, c.WorkPhone,c.Email, "
                         + " c.DateLastPurchase, c.AmountLastPurchase,isnull(gr.Description,'') as CustGroup,isnull(cl.Description,'') as CustClass"
                         + " from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                         + " left outer join customer c on c.ID = t.CustomerID  "
                         + " left outer join GeneralMapping gmG "
                         + " on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                         + " left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' "
                         + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                         + " gmG.ReferenceType = 'Group' and c.ID = gmG.MappingID "
                         + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' and c.ID = gmC.MappingID "
                         + " where (1 = 1) and t.CustomerID > 0 and t.TransType in( 1,4 ) and i.status in (3) " + strSQLDate
                         + " group by t.CustomerID,c.CustomerID,c.Company,c.LastName + ', '+c.FirstName,c.Address1, c.City, "
                         + " c.State, c.Zip,c.WorkPhone,c.Email,c.DateLastPurchase, c.AmountLastPurchase,gr.Description,cl.Description order by c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["TS"].ToString(),
                                                   objSQLReader["DateLastPurchase"].ToString(), 
                                                   objSQLReader["AmountLastPurchase"].ToString(),
                                                   objSQLReader["Email"].ToString(),
                    objSQLReader["CustGroup"].ToString(),
                    objSQLReader["CustClass"].ToString()});

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

        public DataTable FetchAllCustomerPurchaseAmount2(DateTime pStart, DateTime pEnd)
        {
            DataTable dtbl = new DataTable();
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
            dtbl.Columns.Add("City", System.Type.GetType("System.String"));
            dtbl.Columns.Add("State", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select Distinct t.CustomerID as CID,isnull(sum(distinct i.TotalSale),'0') as TS,c.CustomerID as CustID,c.Company, "
                         + " c.LastName + ', '+c.FirstName as Customer,c.Address1, c.City, c.State, c.Zip, c.WorkPhone,c.Email, "
                         + " c.DateLastPurchase, c.AmountLastPurchase,isnull(gr.Description,'') as CustGroup,isnull(cl.Description,'') as CustClass"
                         + " from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                         + " left outer join customer c on c.ID = t.CustomerID  "
                         + " left outer join GeneralMapping gmG "
                         + " on c.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                         + " left outer join GeneralMapping gmC on c.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' "
                         + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                         + " gmG.ReferenceType = 'Group' and c.ID = gmG.MappingID "
                         + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                         + " gmC.ReferenceType = 'Class' and c.ID = gmC.MappingID "
                         + " where (1 = 1) and t.CustomerID > 0 and t.TransType in( 18 ) and i.status in (18) " + strSQLDate
                         + " group by t.CustomerID,c.CustomerID,c.Company,c.LastName + ', '+c.FirstName,c.Address1, c.City, "
                         + " c.State, c.Zip,c.WorkPhone,c.Email,c.DateLastPurchase, c.AmountLastPurchase,gr.Description,cl.Description order by c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["TS"].ToString(),
                                                   objSQLReader["DateLastPurchase"].ToString(), 
                                                   objSQLReader["AmountLastPurchase"].ToString(),
                                                   objSQLReader["Email"].ToString(),
                    objSQLReader["CustGroup"].ToString(),
                    objSQLReader["CustClass"].ToString()});

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

        public DataTable FetchAllCustomerPaymentAmount(DateTime pStart, DateTime pEnd)
        {
            DataTable dtbl = new DataTable();
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Payment", System.Type.GetType("System.String"));
            string strSQLComm = "";
            string strSQLDate = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select Distinct t.CustomerID as CID,isnull(sum(itm.Price * itm.Qty),'0') as TP "
                         + " from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                         + " left outer join item itm on i.ID = itm.InvoiceNo "
                         + " where (1 = 1) and t.CustomerID > 0 and t.TransType in( 1,4,18 ) and i.status in (3,18) " + strSQLDate
                         + " and itm.ProductType = 'A' group by t.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["TP"].ToString()});

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

        #region Fetch Data For Customer Store Credit Report

        public DataTable FetchCustomerSCReportData(string thisstore)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select distinct c.ID as CID, c.CustomerID as CustID,c.Company,c.LastName + ', '+c.FirstName as Customer, "
                       + " c.Address1, c.City, c.State, c.Zip,c.WorkPhone,c.StoreCredit,c.Email from Customer as c where (1 = 1) "
                       + " and c.StoreCredit > 0 and c.issuestore='" + thisstore + "' order by c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Phone", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("StoreCredit", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["WorkPhone"].ToString(),
                                                   objSQLReader["Email"].ToString(),
                                                   objSQLReader["StoreCredit"].ToString()});
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

        #region Fetch Main Data For Sales Summary Report

        public DataTable FetchSalesSummaryMainData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";
            
            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";
            
            strSQLComm =  " select t.ID as TranID, t.TransDate ,isnull(iv.ID,'') as InvoiceNo, "
                        + " isnull(iv.Tax,0) as Tax,isnull(iv.Tax1,0) as Tax1,isnull(iv.Tax2,0) as Tax2,isnull(iv.Tax3,0) as Tax3,"
                        + " isnull(iv.Discount,0) as Discount,isnull(iv.TotalSale,0) as TotalSale,isnull(iv.LayawayNo,0) as LayawayNo, "
                        + " i.ProductID,i.SKU,i.Description,i.ProductType,isnull(i.Price,0) as Price,isnull(i.NormalPrice,0) as NormalPrice,isnull(i.Qty,0) as Qty,"
                        + " isnull(i.Cost,0) as Cost,i.Taxable1,i.Taxable2,i.Taxable3,i.TaxRate1,i.TaxRate2,i.TaxRate3,"
                        + " i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,"
                        + " isnull(tx1.TaxName,'') as TaxName1,isnull(tx1.TaxRate,0) as DTaxRate1,isnull(tx2.TaxName,'') as TaxName2,"
                        + " isnull(tx2.TaxRate,0) as DTaxRate2, isnull(tx3.TaxName,'') as TaxName3,isnull(tx3.TaxRate,0) as DTaxRate3,"
                        + " isnull(i.Discount,0) as iDiscount,isnull(iv.Coupon,0) as Coupon,isnull(iv.CouponPerc,0) as CouponPerc,i.FSTender, "
                        + " isnull(iv.Fees,0) as Fees,isnull(iv.FeesTax,0) as FeesTax,"
                        + " isnull(iv.DTaxID,0) as DTaxID,isnull(iv.DTax,0) as DTax  "
                        + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                        + " and t.TransType in( 1,4 ) and iv.Status = 3 left outer join Item i on iv.ID = i.InvoiceNo "
                        + " left outer join TaxHeader tx1 on iv.TaxID1 = tx1.ID left outer join TaxHeader tx2 on iv.TaxID2 = tx2.ID "
                        + " left outer join TaxHeader tx3 on iv.TaxID3 = tx3.ID where (1 = 1) and i.Tagged <> 'X' " + strSQLDate
                        + " order by iv.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Coupon", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CouponPerc", System.Type.GetType("System.String"));
                
                dtbl.Columns.Add("TaxType1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType3", System.Type.GetType("System.String"));
                
                dtbl.Columns.Add("TaxTotal1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("FSTender", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DTax", System.Type.GetType("System.String"));

                dtbl.Columns.Add("I_ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("I_SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("I_Description", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["TranID"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
												   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["Tax"].ToString(),
                                                   objSQLReader["Tax1"].ToString(), 
                                                   objSQLReader["Tax2"].ToString(),
                                                   objSQLReader["Tax3"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["LayawayNo"].ToString(),
                                                   objSQLReader["ProductType"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["NormalPrice"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["Taxable1"].ToString(),
                                                   objSQLReader["Taxable2"].ToString(),
                                                   objSQLReader["Taxable3"].ToString(),
                                                   objSQLReader["TaxRate1"].ToString(),
                                                   objSQLReader["TaxRate2"].ToString(),
                                                   objSQLReader["TaxRate3"].ToString(),
                                                   objSQLReader["TaxName1"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate1"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate1"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName2"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate2"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate2"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName3"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate3"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate3"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["iDiscount"].ToString(),
                                                   objSQLReader["Coupon"].ToString(),
                                                   objSQLReader["CouponPerc"].ToString(),
                                                   objSQLReader["TaxType1"].ToString(),
                                                   objSQLReader["TaxType2"].ToString(),
                                                   objSQLReader["TaxType3"].ToString(),
                                                   objSQLReader["TaxTotal1"].ToString(),
                                                   objSQLReader["TaxTotal2"].ToString(),
                                                   objSQLReader["TaxTotal3"].ToString(),
                                                   objSQLReader["FSTender"].ToString(),
                                                   objSQLReader["Fees"].ToString(),
                                                   objSQLReader["FeesTax"].ToString(),
                                                   objSQLReader["DTaxID"].ToString(),
                                                   objSQLReader["DTax"].ToString(),
                                                   objSQLReader["ProductID"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString()
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

        public DataTable FetchRentSummaryMainData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";


            strSQLComm = " select t.ID as TranID, t.TransDate ,isnull(iv.ID,'') as InvoiceNo, "
                        + " isnull(iv.Tax,0) as Tax,isnull(iv.Tax1,0) as Tax1,isnull(iv.Tax2,0) as Tax2,isnull(iv.Tax3,0) as Tax3,"
                        + " isnull(iv.Discount,0) as Discount,isnull(iv.TotalSale,0) as TotalSale,isnull(iv.LayawayNo,0) as LayawayNo, "
                        + " i.ProductType,isnull(i.Price,0) as Price,isnull(i.NormalPrice,0) as NormalPrice,isnull(i.Qty,0) as Qty,"
                        + " isnull(i.Cost,0) as Cost,i.Taxable1,i.Taxable2,i.Taxable3,i.TaxRate1,i.TaxRate2,i.TaxRate3,"
                        + " i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,"
                        + " isnull(tx1.TaxName,'') as TaxName1,isnull(tx1.TaxRate,0) as DTaxRate1,isnull(tx2.TaxName,'') as TaxName2,"
                        + " isnull(tx2.TaxRate,0) as DTaxRate2, isnull(tx3.TaxName,'') as TaxName3,isnull(tx3.TaxRate,0) as DTaxRate3, "
                        + " isnull(iv.CouponPerc,0) as CouponPerc, isnull(i.Discount,0) as iDiscount,isnull(iv.Coupon,0) as Coupon, "
                        + " isnull(i.RentDuration,0) as RentDuration,iv.RentDeposit,iv.Status,iv.IsRentCalculated,"
                        + " isnull(iv.Fees,0) as Fees,isnull(iv.FeesTax,0) as FeesTax from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                        + " and ((iv.Status = 15 and iv.IsRentCalculated = 'N') or (iv.Status = 16 and iv.IsRentCalculated = 'Y')) left outer join Item i on iv.ID = i.InvoiceNo "
                        + " left outer join TaxHeader tx1 on iv.TaxID1 = tx1.ID left outer join TaxHeader tx2 on iv.TaxID2 = tx2.ID "
                        + " left outer join TaxHeader tx3 on iv.TaxID3 = tx3.ID where (1 = 1) and i.Tagged <> 'X' " + strSQLDate
                        + " order by  iv.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Coupon", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDeposit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Status", System.Type.GetType("System.String"));
                dtbl.Columns.Add("IsRentCalculated", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CouponPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("FSTender", System.Type.GetType("System.String"));

                dtbl.Columns.Add("Fees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesTax", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   objSQLReader["TranID"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
												   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["Tax"].ToString(),
                                                   objSQLReader["Tax1"].ToString(), 
                                                   objSQLReader["Tax2"].ToString(),
                                                   objSQLReader["Tax3"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["LayawayNo"].ToString(),
                                                   objSQLReader["ProductType"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["NormalPrice"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["Taxable1"].ToString(),
                                                   objSQLReader["Taxable2"].ToString(),
                                                   objSQLReader["Taxable3"].ToString(),
                                                   objSQLReader["TaxRate1"].ToString(),
                                                   objSQLReader["TaxRate2"].ToString(),
                                                   objSQLReader["TaxRate3"].ToString(),
                                                   objSQLReader["TaxName1"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate1"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate1"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName2"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate2"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate2"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName3"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate3"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate3"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["iDiscount"].ToString(),
                                                   objSQLReader["Coupon"].ToString(),
                                                   objSQLReader["RentDuration"].ToString(),
                                                   objSQLReader["RentDeposit"].ToString(),
                                                   objSQLReader["Status"].ToString(),
                                                   objSQLReader["IsRentCalculated"].ToString(),
                                                   objSQLReader["CouponPerc"].ToString(),
                                                   objSQLReader["TaxType1"].ToString(),
                                                   objSQLReader["TaxType2"].ToString(),
                                                   objSQLReader["TaxType3"].ToString(),
                                                   objSQLReader["TaxTotal1"].ToString(),
                                                   objSQLReader["TaxTotal2"].ToString(),
                                                   objSQLReader["TaxTotal3"].ToString(),
                                                   objSQLReader["FSTender"].ToString(),
                                                   objSQLReader["Fees"].ToString(),
                                                   objSQLReader["FeesTax"].ToString() });
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

        public double FetchRepairSummaryDepositData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            double val = 0;

            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select isnull(sum(iv.RepairAdvanceAmount),0) as RDeposit from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                       + " and ((iv.Status = 17 and iv.RepairStatus = 'In') or (iv.Status = 4 and iv.servicetype = 'Repair' and iv.repairparentid = 0)) where (1 = 1) " + strSQLDate ;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    val = Functions.fnDouble(objSQLReader["RDeposit"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public DataTable FetchRepairSummaryMainData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select isnull(iv.RepairParentID,0) as RepairParentID,isnull(iv.Coupon,0) as Coupon,"
                       + " isnull(iv.CouponPerc,0) as CouponPerc,isnull(iv.Fees,0) as Fees,isnull(iv.FeesTax,0) as FeesTax, "
                       + " isnull(iv.Tax1,0) as Tax1,isnull(iv.Tax2,0) as Tax2, isnull(iv.Tax3,0) as Tax3 from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                       + " and iv.Status = 18 where (1 = 1) and  iv.RepairParentID > 0 " + strSQLDate + " order by  iv.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("RepairParentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Coupon", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CouponPerc", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FeesTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if (objSQLReader["RepairParentID"].ToString() == "") continue;
                    dtbl.Rows.Add(new object[] { objSQLReader["RepairParentID"].ToString(),
                                                 objSQLReader["Coupon"].ToString(),
                                                 objSQLReader["CouponPerc"].ToString(),
                                                 objSQLReader["Fees"].ToString(),
                                                 objSQLReader["FeesTax"].ToString(),
                                                 objSQLReader["Tax1"].ToString(),
                                                 objSQLReader["Tax2"].ToString(),
                                                 objSQLReader["Tax3"].ToString()});
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

        public DataTable FetchRepairSummarySubData(int rprparent)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select t.ID as TranID, t.TransDate ,isnull(iv.ID,'') as InvoiceNo, "
                        + " isnull(iv.Tax,0) as Tax,isnull(iv.Tax1,0) as Tax1,isnull(iv.Tax2,0) as Tax2,isnull(iv.Tax3,0) as Tax3,"
                        + " isnull(iv.Discount,0) as Discount,isnull(iv.TotalSale,0) as TotalSale,isnull(iv.LayawayNo,0) as LayawayNo, "
                        + " i.ProductType,isnull(i.Price,0) as Price,isnull(i.NormalPrice,0) as NormalPrice,isnull(i.Qty,0) as Qty,"
                        + " isnull(i.Cost,0) as Cost,i.Taxable1,i.Taxable2,i.Taxable3,i.TaxRate1,i.TaxRate2,i.TaxRate3,"
                        + " i.TaxType1,i.TaxType2,i.TaxType3,i.TaxTotal1,i.TaxTotal2,i.TaxTotal3,i.FSTender,"
                        + " isnull(tx1.TaxName,'') as TaxName1,isnull(tx1.TaxRate,0) as DTaxRate1,isnull(tx2.TaxName,'') as TaxName2,"
                        + " isnull(tx2.TaxRate,0) as DTaxRate2, isnull(tx3.TaxName,'') as TaxName3,isnull(tx3.TaxRate,0) as DTaxRate3, "
                        + " isnull(iv.CouponPerc,0) as CouponPerc,isnull(i.Discount,0) as iDiscount,isnull(iv.Coupon,0) as Coupon, "
                        + " isnull(i.RentDuration,0) as RentDuration "
                        + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                        + " and iv.Status = 17 left outer join Item i on iv.ID = i.InvoiceNo "
                        + " left outer join TaxHeader tx1 on iv.TaxID1 = tx1.ID left outer join TaxHeader tx2 on iv.TaxID2 = tx2.ID "
                        + " left outer join TaxHeader tx3 on iv.TaxID3 = tx3.ID where (1 = 1) and i.Tagged <> 'X' and iv.ID=@ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = rprparent;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TranID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Taxable3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Coupon", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentDuration", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CouponPerc", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TaxType1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("TaxTotal1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxTotal3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("FSTender", System.Type.GetType("System.String"));

                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["TranID"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
												   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["Tax"].ToString(),
                                                   objSQLReader["Tax1"].ToString(), 
                                                   objSQLReader["Tax2"].ToString(),
                                                   objSQLReader["Tax3"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["LayawayNo"].ToString(),
                                                   objSQLReader["ProductType"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["NormalPrice"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["Taxable1"].ToString(),
                                                   objSQLReader["Taxable2"].ToString(),
                                                   objSQLReader["Taxable3"].ToString(),
                                                   objSQLReader["TaxRate1"].ToString(),
                                                   objSQLReader["TaxRate2"].ToString(),
                                                   objSQLReader["TaxRate3"].ToString(),
                                                   objSQLReader["TaxName1"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate1"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate1"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName2"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate2"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate2"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["TaxName3"].ToString() + (Functions.fnDouble(objSQLReader["DTaxRate3"].ToString()) == 0 ? "" : " ( "+objSQLReader["DTaxRate3"].ToString().TrimStart('0').TrimEnd('0','.') + "% )"),
                                                   objSQLReader["iDiscount"].ToString(),
                                                   objSQLReader["Coupon"].ToString(),
                                                   objSQLReader["RentDuration"].ToString(),
                                                   objSQLReader["CouponPerc"].ToString(),
                                                   objSQLReader["TaxType1"].ToString(),
                                                   objSQLReader["TaxType2"].ToString(),
                                                   objSQLReader["TaxType3"].ToString(),
                                                   objSQLReader["TaxTotal1"].ToString(),
                                                   objSQLReader["TaxTotal2"].ToString(),
                                                   objSQLReader["TaxTotal3"].ToString(),
                                                   objSQLReader["FSTender"].ToString()
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

        public double GetLayawaySalesPosted(int TranID, int InvNo)
        {
            double dblamount = 0;
            string strSQLComm = " select payment from laypmts where TransactionNo = @T and InvoiceNo = @I ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@T"].Value = TranID;
            objSQlComm.Parameters.Add(new SqlParameter("@I", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@I"].Value = InvNo;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {

                    dblamount = Functions.fnDouble(objSQLReader["payment"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblamount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public double GetFoodStampTendered(int TranID)
        {
            double dblamount = 0;
            string strSQLComm = " select isnull(sum(TenderAmount),0) as fst from tender where TransactionNo = @T "
                              + " and tendertype in (select id from tendertypes where name = 'Food Stamps') ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@T", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@T"].Value = TranID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    dblamount = Functions.fnDouble(objSQLReader["fst"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblamount;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        #endregion

        #region Fetch Customer Purchase Record

        public DataTable FetchCustomerPurchaseInvoice(int CustID,string srv)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";
            string sqlfilter = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";

            if (srv == "Sales")
            {
                sqlfilter = " and t.TransType in (1,4) and iv.status = 3 ";
            }

            if (srv == "Rent")
            {
                sqlfilter = " and t.TransType = 15 and iv.status = 15 ";
            }

            if (srv == "Repair")
            {
                sqlfilter = " and t.TransType = 18 and iv.status = 18 ";
            }

            strSQLComm =  " select distinct iv.ID as InvoiceNo,t.employeeID,t.TransDate,e.lastname + ', ' + e.firstname as Emp,"
                        + " iv.Tax,iv.Discount,iv.TotalSale,iv.RepairAmount from Trans t left outer join Invoice iv "
                        + " on t.ID = iv.TransactionNo left outer join employee e on t.EmployeeID = e.ID  "
                        + " where (1 = 1) " + sqlfilter + " and t.CustomerID = @CustID order by iv.ID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustID"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvNo", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("SubTotal", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Employee", System.Type.GetType("System.String"));

                string stremp = "";
                double subT = 0;
                double T = 0;
                while (objSQLReader.Read())
                {
                    stremp = "";
                    subT = 0;
                    T = 0;
                    if (objSQLReader["employeeID"].ToString() == "0") stremp = "ADMIN"; else stremp = objSQLReader["Emp"].ToString();

                    if (srv == "Sales")
                    {
                        subT = Functions.fnDouble(objSQLReader["TotalSale"].ToString()) - Functions.fnDouble(objSQLReader["Tax"].ToString());
                        T = Functions.fnDouble(objSQLReader["TotalSale"].ToString());
                    }

                    if (srv == "Rent")
                    {
                        subT = Functions.fnDouble(objSQLReader["TotalSale"].ToString()) - Functions.fnDouble(objSQLReader["Tax"].ToString());
                        T = Functions.fnDouble(objSQLReader["TotalSale"].ToString());
                    }

                    if (srv == "Repair")
                    {
                        subT = Functions.fnDouble(objSQLReader["RepairAmount"].ToString()) - Functions.fnDouble(objSQLReader["Tax"].ToString());
                        T = Functions.fnDouble(objSQLReader["RepairAmount"].ToString());
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["TransDate"].ToString(),
												   Functions.fnInt32(objSQLReader["InvoiceNo"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Tax"].ToString()),
                                                   Functions.fnDouble(objSQLReader["Discount"].ToString()),
                                                   T,subT,stremp
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

        public DataTable FetchCustomerHouseAccount(int CustID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLCustomer = "";
            string strSQL1 = "";
            string strSQLOrder = "";


            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','G','T','X')";

            strSQLComm = " select InvoiceNo, Date, TranType, Amount From AcctRecv where CustomerID = @CustID order by ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustID"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Amount", System.Type.GetType("System.Double"));
                

                string strtran = "";
                while (objSQLReader.Read())
                {
                    strtran = "";
                    if (objSQLReader["TranType"].ToString() == "6") strtran = "Opening Balance";
                    if (objSQLReader["TranType"].ToString() == "3") strtran = "Account Payment";
                    if (objSQLReader["TranType"].ToString() == "2") strtran = "Purchase";
                    if (objSQLReader["TranType"].ToString() == "1") strtran = "Adjustment";
                    if (objSQLReader["TranType"].ToString() == "5") strtran = "Account Payment with purchase/return";

                    dtbl.Rows.Add(new object[] {   objSQLReader["InvoiceNo"].ToString(),
												   Functions.fnDate(objSQLReader["Date"].ToString()).ToShortDateString(),
                                                   strtran,
                                                   Functions.fnDouble(objSQLReader["Amount"].ToString())
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
        
        public DataTable FetchCustomerPurchaseRecord(int CustID)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm =  " select t.TransDate ,iv.ID as InvoiceNo,iv.Tax,iv.Discount,iv.TotalSale, "
                        + " i.ProductType,i.SKU, i.Description,i.Price,i.NormalPrice,i.Qty,i.Cost "
                        + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                        + " and iv.Status = 3 left outer join Item i on iv.ID = i.InvoiceNo "
                        + " where (1 = 1) and i.Tagged <> 'X' and t.CustomerID = @CustID "
                        + " order by iv.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CustID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CustID"].Value = CustID;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NormalPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["TransDate"].ToString(),
												   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["Tax"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["ProductType"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["NormalPrice"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString()});

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

        #region Fetch Product sales,rent,repair Record

        public double FetchProductSalesRecord(int refProductID, string refCategory, DateTime pStart )
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            string SQLdate = "";

            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }

            double dblSum = 0;
            string strSQLComm = "";

            strSQLComm =  " select isnull(sum(i.Qty),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype in (1,4) "
                        + " and v.Status = 3 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','S','W','E','F','T') "
                        + SQLdate;
                        
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;

                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                   dblSum =Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public double FetchProductRentRecord(int refProductID, string refCategory, DateTime pStart)
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            double dblSum = 0;
            string strSQLComm = "";

            string SQLdate = "";
            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }


            strSQLComm =  " select isnull(sum(i.Qty),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype = 15 "
                        + " and v.Status = 15 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','W','E','F','T') "
                        + SQLdate;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;

                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    dblSum = Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public double FetchProductRepairRecord(int refProductID, string refCategory, DateTime pStart)
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            double dblSum = 0;
            string strSQLComm = "";

            string SQLdate = "";
            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }

            strSQLComm =  " select isnull(sum(i.Qty),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype = 17 "
                        + " and v.Status = 17 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','W','E','F','T') "
                        +  SQLdate;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;

                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    dblSum = Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }


        public double FetchProductSalesRevenueRecord(int refProductID, string refCategory, DateTime pStart)
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            double dblSum = 0;
            string strSQLComm = "";

            string SQLdate = "";
            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }

            strSQLComm = " select isnull(sum(i.Qty * i.Price),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype in (1,4) "
                        + " and v.Status = 3 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','S','W','E','F','T') "
                        + SQLdate;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;
                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    dblSum = Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public double FetchProductRentRevenueRecord(int refProductID, string refCategory, DateTime pStart)
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            double dblSum = 0;
            string strSQLComm = "";

            string SQLdate = "";
            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }

            strSQLComm = " select isnull(sum(i.Qty * i.Price),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype = 15 "
                        + " and v.Status = 15 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','W','E','F','T') "
                        +  SQLdate;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;

                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    dblSum = Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }

        public double FetchProductRepairRevenueRecord(int refProductID, string refCategory, DateTime pStart)
        {
            DateTime pEnd = Convert.ToDateTime(null);
            if (refCategory == "Today") pEnd = pStart;
            if (refCategory == "Last 7 Days") pEnd = pStart.AddDays(-7);
            if (refCategory == "Last 14 Days") pEnd = pStart.AddDays(-14);
            if (refCategory == "Last 1 Month") pEnd = pStart.AddMonths(-1);
            if (refCategory == "Last 3 Months") pEnd = pStart.AddMonths(-3);
            if (refCategory == "Last 6 Months") pEnd = pStart.AddMonths(-6);
            if (refCategory == "Last 1 Year") pEnd = pStart.AddYears(-1);
            DateTime FormatStartDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 23, 59, 59);

            double dblSum = 0;
            string strSQLComm = "";

            string SQLdate = "";
            if (refCategory != "To Date")
            {
                SQLdate = " and t.TransDate between @SDT and @TDT ";
            }

            strSQLComm = " select isnull(sum(i.Qty * i.Price),0) as CountQty from trans t left outer join invoice v on t.ID = v.TransactionNo "
                        + " left outer join item i on v.ID = i.InvoiceNo where i.ProductID = @PID and t.Transtype = 17 "
                        + " and v.Status = 17 and i.Tagged <> 'X' and i.Producttype in ('P','K','U','M','W','E','F','T') "
                        + SQLdate;

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@PID"].Value = refProductID;
                if (refCategory != "To Date")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                    objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                    objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                }
                objSQLReader = objSQlComm.ExecuteReader();

                int intmn = 0;
                int intyr = 0;
                string strmn = "";

                while (objSQLReader.Read())
                {
                    dblSum = Functions.fnDouble(objSQLReader["CountQty"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                sqlConn.Close();
                return dblSum;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                sqlConn.Close();
                objSQLReader.Close();
                objSQlComm.Dispose();
                return 0;
            }
        }


        #endregion

        #region Fetch Data For CRM 

        public DataTable FetchCRMbyVisitReportData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLDate1 = "";

            string strSQLVisit = "";
            string strSQLVisit1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLDate1 = " and t.LastChangedOn between @SDT1 and @TDT1 ";

            strSQLComm = " select t.CustomerID,cs.ActiveStatus, count(*) as VisitCount from trans t left outer join customer cs on cs.ID = t.customerid where (1 = 1) " + strSQLDate +
                         " and t.customerID > 0 and t.customerid in (select c.id from customer c where c.issuestore = '" + strOperateStore
                         + "') and transtype not in (5,6) group by t.customerID,cs.ActiveStatus " +  
                         " UNION "
                       + " select t.CustomerID,cs.ActiveStatus, count(distinct InvoiceNo) as VisitCount from suspnded t left outer join customer cs on cs.ID = t.customerid "
                       + " where (1 = 1)  " + strSQLDate1 + "and t.customerID > 0 and t.customerid in (select c1.id from customer c1 where c1.issuestore = '" + strOperateStore
                       + "') group by t.customerID,cs.ActiveStatus order by t.customerID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;
                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;
                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;
                objSQlComm.Parameters.Add(new SqlParameter("@TDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT1"].Value = FormatEndDate;


                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Visit", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["CustomerID"].ToString(),
                                                   objSQLReader["ActiveStatus"].ToString(),
												   objSQLReader["VisitCount"].ToString()});
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

        public DataTable FetchCustomer()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "";

            strSQLComm = " select *, FirstName + ' ' + LastName as CustomerName from customer where IssueStore = '" + strOperateStore + "' ";
                        
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
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
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
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
                    if (strMailAddress != "")
                        strMailAddress = strMailAddress + "\n" + strMailCityLine;
                    else
                        strMailAddress = strMailCityLine;

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
                                                    "0",objSQLReader["ActiveStatus"].ToString()});

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

        public DataTable FetchCustomerByParam(bool PT1,bool PT2,bool PT3,bool PT4,bool PT5,
                                              string PV1,string PV2,string PV3,string PV4,string PV5)
        {
            DataTable dtbl = new DataTable();
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";
            string strSQL5 = "";
            string strSQL6 = "";
            if ((!PT1) && (PV1.Trim() != ""))
                strSQL1 = " and ParamValue1 like '%" + PV1 + "%' ";
            if ((!PT2) && (PV2.Trim() != ""))
                strSQL2 = " and ParamValue2 like '%" + PV2 + "%' ";
            if ((!PT3) && (PV3.Trim() != ""))
                strSQL3 = " and ParamValue3 like '%" + PV3 + "%' ";
            if ((!PT4) && (PV4.Trim() != ""))
                strSQL4 = " and ParamValue4 like '%" + PV4 + "%' ";
            if ((!PT5) && (PV5.Trim() != ""))
                strSQL5 = " and ParamValue5 like '%" + PV5 + "%' ";
            strSQL6 = " and IssueStore = '" + strOperateStore + "' ";
            string strSQLComm = "";

            strSQLComm = " select *, FirstName + ' ' + LastName as CustomerName from customer where (1 = 1) " +
                        strSQL1 + strSQL2 + strSQL3 + strSQL4 + strSQL5 + strSQL6 + " order by customerid ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
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
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
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
                    if (strMailAddress != "")
                        strMailAddress = strMailAddress + "\n" + strMailCityLine;
                    else
                        strMailAddress = strMailCityLine;

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
                                                    "0",objSQLReader["ActiveStatus"].ToString()});

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
        
        public DataTable FetchCRMByPointReportData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            strSQLComm = " select a.Producttype as PT,a.ProductID,a.Qty - a.ReturnedItemCnt as PQty,t.CustomerID, p.Points,"
                         + " p.Points*(a.Qty - a.ReturnedItemCnt) as TPoints, c.ActiveStatus "
                         + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                         + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                         + " left outer join  Product p on a.ProductID = p.ID "
                         + " left outer join  Customer c on t.CustomerID = c.ID "
                         + " where (1 = 1) and t.CustomerID > 0 and i.Status = 3 and a.Tagged <> 'X' "
                         + " and a.Qty > 0 and a.Qty - a.ReturnedItemCnt > 0 and c.IssueStore = @Store " + strSQLDate
                         + " Order by t.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@Store", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Store"].Value = strOperateStore;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Points", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TPoints", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    dtbl.Rows.Add(new object[] { objSQLReader["CustomerID"].ToString(),
												 objSQLReader["ProductID"].ToString(),
                                                 objSQLReader["PQty"].ToString(),
                                                 objSQLReader["Points"].ToString(), 
                                                 Functions.fnInt32(Math.Ceiling(Math.Round(Functions.fnDouble(objSQLReader["TPoints"].ToString())))),
                                                 objSQLReader["ActiveStatus"].ToString() });
                                                 
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

        public DataTable FetchAllGiftCertIssueAmount(string CustOption, string BalOption, double BalAmount, 
                                                     string CentralFlag, string StoreCode)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLCust = "";
            string strSQLBal = "";

            if (CustOption == "C")
                strSQLCust = " and g.CustomerID <> 0 ";
            if (BalOption != "A")
            {
                if (BalOption == "G")
                {
                    strSQLBal = " having sum(g.Amount) > " + BalAmount.ToString();
                }

                if (BalOption == "L")
                {
                    strSQLBal = " having sum(g.Amount) < " + BalAmount.ToString();
                }
            }

            if (CentralFlag == "N")
                strSQLComm = " select g.GiftCertID,isnull(sum(g.Amount),0) as IssueAmt, c.CustomerID, c.LastName, c.FirstName "
                           + " from giftcert g left outer join customer c on g.CustomerID = c.ID "
                           + " where tenderno = 0  " + strSQLCust
                           + " group by g.GiftCertID, c.CustomerID, c.LastName, c.FirstName " + strSQLBal
                           + " order by GiftCertID ";

            if (CentralFlag == "Y")
                strSQLComm = " select g.GiftCertID,isnull(sum(g.Amount),0) as IssueAmt, c.CustomerID, c.LastName, c.FirstName "
                           + " from giftcert g left outer join customer c on g.CustomerID = c.ID "
                           + " where tenderno = 0 and g.IssueStore = '" + StoreCode + "'" + strSQLCust
                           + " group by g.GiftCertID, c.CustomerID, c.LastName, c.FirstName " + strSQLBal
                           + " order by GiftCertID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("GC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AMOUNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CUSTOMER", System.Type.GetType("System.String"));
                
                string strcust = "";
                while (objSQLReader.Read())
                {
                    strcust = "";
                    if (objSQLReader["CustomerID"].ToString() == "0") strcust = "";
                    else strcust = objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString();
                    dtbl.Rows.Add(new object[] {   objSQLReader["GiftCertID"].ToString(),
												   objSQLReader["IssueAmt"].ToString(),
                                                   strcust});

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

        public DataTable FetchAllGiftCertTranAmount(string CustOption, string BalOption, double BalAmount, string CentralFlag, string StoreCode)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLCust = "";
            string strSQLBal = "";

            if (CustOption == "C")
                strSQLCust = " and g.CustomerID <> 0 ";
            if (BalOption != "A")
            {
                if (BalOption == "G")
                {
                    strSQLBal = " having sum(g.Amount) > " + BalAmount.ToString();
                }

                if (BalOption == "L")
                {
                    strSQLBal = " having sum(g.Amount) < " + BalAmount.ToString();
                }
            }

            if (CentralFlag == "N")
                strSQLComm = " select g.GiftCertID,isnull(sum(g.Amount),0) as IssueAmt, c.CustomerID, c.LastName, c.FirstName "
                            + " from giftcert g left outer join customer c on g.CustomerID = c.ID "
                            + " where tenderno <> 0  " + strSQLCust
                            + " group by g.GiftCertID, c.CustomerID, c.LastName, c.FirstName " + strSQLBal
                            + " order by GiftCertID ";

            if (CentralFlag == "Y")
                strSQLComm = " select g.GiftCertID,isnull(sum(g.Amount),0) as IssueAmt, c.CustomerID, c.LastName, c.FirstName "
                            + " from giftcert g left outer join customer c on g.CustomerID = c.ID "
                            + " where tenderno <> 0  and g.IssueStore = '" + StoreCode + "'" + strSQLCust
                            + " group by g.GiftCertID, c.CustomerID, c.LastName, c.FirstName " + strSQLBal
                            + " order by GiftCertID ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("GC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AMOUNT", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CUSTOMER", System.Type.GetType("System.String"));

                string strcust = "";
                while (objSQLReader.Read())
                {
                    strcust = "";
                    if (objSQLReader["CustomerID"].ToString() == "0") strcust = "";
                    else strcust = objSQLReader["FirstName"].ToString() + " " + objSQLReader["LastName"].ToString();
                    dtbl.Rows.Add(new object[] {   objSQLReader["GiftCertID"].ToString(),
												   objSQLReader["IssueAmt"].ToString(),
                                                   strcust});

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

        public DataTable FetchGCRedeemedData(string GCNo, DateTime pStart, DateTime pEnd,string CentralF, string storec)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQLDate1 = "";
            string strSQLProduct1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLDate1 = " and t.TransDate between @SDT1 and @TDT1 ";

            if (GCNo != "")
            {
                strSQLProduct =  " and g.GiftCertID = @GID ";
                strSQLProduct1 = " and g.GiftCertID = @GID1 ";
            }

            if (CentralF == "N")
                strSQLComm =  " select t.TransDate ,iv.ID as InvoiceNo, g.GiftCertID , g.Amount, '-' as Type  "
                            + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                            + " left outer join tender td on td.TransactionNo = t.ID "
                            + " left outer join GiftCert g on g.TenderNo = td.ID  "
                            + " Where (1 = 1) and g.TenderNo > 0 " + strSQLDate + strSQLProduct
                            + " union select t.TransDate ,iv.ID as InvoiceNo, g.GiftCertID , g.Amount, '+' as Type "
                            + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                            + " left outer join item i on i.InvoiceNo = iv.ID "
                            + " left outer join GiftCert g on g.ItemID = i.ID  "
                            + " Where (1 = 1) and g.itemid > 0 and i.Tagged <> 'X' " + strSQLDate1 + strSQLProduct1
                            + " order by g.GiftCertID, t.TransDate ";

            if (CentralF == "Y")
                strSQLComm = " select t.TransDate ,iv.ID as InvoiceNo, g.GiftCertID , g.Amount, '-' as Type  "
                            + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                            + " left outer join tender td on td.TransactionNo = t.ID "
                            + " left outer join GiftCert g on g.TenderNo = td.ID  "
                            + " Where (1 = 1) and g.TenderNo > 0 and g.issuestore = '" + storec  + "'" + strSQLDate + strSQLProduct
                            + " union select t.TransDate ,iv.ID as InvoiceNo, g.GiftCertID , g.Amount, '+' as Type "
                            + " from Trans t left outer join Invoice iv on t.ID = iv.TransactionNo "
                            + " left outer join item i on i.InvoiceNo = iv.ID "
                            + " left outer join GiftCert g on g.ItemID = i.ID  "
                            + " Where (1 = 1) and g.itemid > 0 and i.Tagged <> 'X' and g.issuestore = '" + storec + "'" + strSQLDate1 + strSQLProduct1
                            + " order by g.GiftCertID, t.TransDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@SDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT1"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT1", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT1"].Value = FormatEndDate;

                if (GCNo != "")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@GID", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@GID"].Value = GCNo;
                    objSQlComm.Parameters.Add(new SqlParameter("@GID1", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@GID1"].Value = GCNo;
                }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GiftCertID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    double dblamt = 0;
                    if (objSQLReader["Type"].ToString() == "-")
                    {
                        dblamt = -Functions.fnDouble(objSQLReader["Amount"].ToString());
                    }
                    else
                    {
                        dblamt = Functions.fnDouble(objSQLReader["Amount"].ToString());
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
												   objSQLReader["GiftCertID"].ToString(),
                                                   dblamt.ToString()});
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

        #region Fetch Data For Customer House Account Detail

        public DataTable FetchCustomerHAHeaderData(int ACID, DateTime FDT, DateTime TDT)
        {
            DateTime FormatStartDate = new DateTime(FDT.Year, FDT.Month, FDT.Day, 0, 0, 0);
            DateTime FormatEndDate = new DateTime(TDT.Year, TDT.Month, TDT.Day, 23, 59, 59);
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select a.ID as HID, c.ID as CID, c.CustomerID as CustID,c.Company, "
                         + " c.FirstName + ' ' + c.LastName as Customer,c.Address1,c.Address2,c.City,c.State,c.zip,c.Email, "
                         + " a.amount, a.invoiceno, a.date, a.TranType "
                         + " from AcctRecv a left outer join Customer AS c on a.CustomerID = c.ID "
                         + " where (1 = 1) and  a.CustomerID = @CID  and a.trantype in (1,3,2,5) and a.date between @SDT and @EDT "
                         //+ " and a.ID > ( select max(b.ID) from AcctRecv b where b.CustomerID = @CID1 and b.trantype = 3)"
                         + " order by a.ID, a.invoiceno ";
            //
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CID1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@CID"].Value = ACID;
                objSQlComm.Parameters["@CID1"].Value = ACID;
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;
                objSQlComm.Parameters["@EDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("HID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("City", System.Type.GetType("System.String"));
                dtbl.Columns.Add("State", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Date", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TranType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
                                                   Functions.fnInt32(objSQLReader["HID"].ToString()),
                                                   objSQLReader["CID"].ToString(),
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Address1"].ToString(),
                                                   objSQLReader["Address2"].ToString(),
                                                   objSQLReader["City"].ToString(),
                                                   objSQLReader["State"].ToString(),
                                                   objSQLReader["Zip"].ToString(),
                                                   objSQLReader["Email"].ToString(),
                                                   objSQLReader["Amount"].ToString(),
                                                   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["Date"].ToString(),
                                                   objSQLReader["TranType"].ToString()});

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

        public DataTable FetchCustomerHADetailData(int ACID, DateTime FDT, DateTime TDT)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            DateTime FormatStartDate = new DateTime(FDT.Year, FDT.Month, FDT.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(TDT.Year, TDT.Month, TDT.Day, 23, 59, 59);

            strSQLComm = " select a.InvoiceNo, a.SKU,a.Description,a.Qty,a.Price,a.Cost,a.Qty*a.Price as ExtPrice,a.ProductType, "
                        + " i.Tax1,i.Tax2,i.Tax3,i.Discount,i.DiscountReason,i.TotalSale,"
                        + " isnull(tx1.TaxName,'') as TaxName1, isnull(tx2.TaxName,'') as TaxName2, "
                        + " isnull(tx3.TaxName,'') as TaxName3 "
                       + " From Trans t LEFT OUTER JOIN Invoice i ON t.ID = i.TransactionNo "
                       + " LEFT OUTER JOIN item a ON a.InvoiceNo = i.ID "
                        + " left outer join TaxHeader tx1 on i.TaxID1 = tx1.ID "
                        + " left outer join TaxHeader tx2 on i.TaxID2 = tx2.ID "
                        + " left outer join TaxHeader tx3 on i.TaxID3 = tx3.ID "
                       + " Where (1 = 1) and a.Tagged <> 'X' and i.ID in (select act.invoiceno from acctrecv act where act.customerid = @CID and  "
                       + "  act.trantype in (1,3,2,5) and act.date between @SDT and @EDT ) "
                       //+ "  act.ID > ( select max(ID) from acctrecv act1 where act1.customerid = @CID and act1.trantype =  3) "
                       + " order by i.ID ";
            //
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CID1", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@EDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@CID"].Value = ACID;
                objSQlComm.Parameters["@CID1"].Value = ACID;
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;
                objSQlComm.Parameters["@EDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ExtPrice", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountReason", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["InvoiceNo"].ToString(),
												   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Qty"].ToString(), 
                                                   objSQLReader["Price"].ToString(),
                                                   objSQLReader["ExtPrice"].ToString(),
                                                   objSQLReader["Tax1"].ToString(),
                                                   objSQLReader["Tax2"].ToString(),
                                                   objSQLReader["Tax3"].ToString(),
                                                   objSQLReader["TaxName1"].ToString(),
                                                   objSQLReader["TaxName2"].ToString(),
                                                   objSQLReader["TaxName3"].ToString(),
                                                   objSQLReader["Discount"].ToString(),
                                                   objSQLReader["DiscountReason"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["ProductType"].ToString()
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

        #endregion

        #region Fetch Data For Customer Layaway Report

        public DataTable FetchCustomerLayawayHeaderData(DataTable refCustomer, bool ActiveLayaway)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLActive = "";
            string strSQLCustomer = "";

            string getCust = "";

            foreach (DataRow drG in refCustomer.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getCust == "")
                    {
                        getCust = drG["ID"].ToString();
                    }
                    else
                    {
                        getCust = getCust + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getCust != "")
            {
                strSQLCustomer = " and c.ID in ( " + getCust + " )";
            }

            if (ActiveLayaway)
            {
                strSQLActive = " and i.Status in (1) ";
            }
            else
            {
                strSQLActive = " and i.Status in (1,3) ";
            }


            strSQLComm =   " select c.CustomerID as CustID,c.LastName + ', ' + c.FirstName as Customer,c.Email, "
                         + " c.Company,c.Address1,c.Address2,c.City,c.State,c.Zip,c.HomePhone, "
                         + " t.TransDate, i.ID as InvoiceNo,i.LayawayNo,it.SKU, it.Description, it.Qty, i.TotalSale, l.DateDue "
                         + " from trans t left outer join invoice i on i.transactionno = t.ID "
                         + " left outer join item it on it.invoiceno = i.ID "
                         + " left outer join layaway l on i.LayawayNo = l.ID "
                         + " left outer join customer c on i.CustomerID = c.ID "
                         + " where i.LayawayNo > 0 and it.Tagged <> 'X' " + strSQLCustomer + strSQLActive
                         + " order by c.CustomerID, i.LayawayNo, i.ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LayawayNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DateDue", System.Type.GetType("System.String"));

                string addr = "";
                string strcitystatezip = "";
                string strCompany = "";
                while (objSQLReader.Read())
                {
                    addr = "";
                    strcitystatezip = "";
                    strCompany = "";
                    strCompany = objSQLReader["Company"].ToString();
                    string strAddress1 = objSQLReader["Address1"].ToString();
                    string strAddress2 = objSQLReader["Address2"].ToString();
                    string strCity = objSQLReader["City"].ToString();
                    string strState = objSQLReader["State"].ToString();
                    string strZip = objSQLReader["Zip"].ToString();
                    string strWorkPhone = objSQLReader["HomePhone"].ToString();


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

                    if (objSQLReader["Email"].ToString() != "")
                    {
                        addr = addr + strWorkPhone + "Email: " + objSQLReader["Email"].ToString();
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Customer"].ToString(),
                                                   addr,
                                                   objSQLReader["Email"].ToString(),
                                                   objSQLReader["InvoiceNo"].ToString(),
                                                   objSQLReader["LayawayNo"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["TotalSale"].ToString(),
                                                   objSQLReader["DateDue"].ToString()});

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

        public DataTable FetchCustomerLayawayDetailData(DataTable refCustomer, bool ActiveLayaway)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLActive = "";
            string strSQLCustomer = "";

            string getCust = "";

            foreach (DataRow drG in refCustomer.Rows)
            {

                if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                {
                    if (getCust == "")
                    {
                        getCust = drG["ID"].ToString();
                    }
                    else
                    {
                        getCust = getCust + "," + drG["ID"].ToString();
                    }
                }
            }
            if (getCust != "")
            {
                strSQLCustomer = " and c.ID in ( " + getCust + " )";
            }

            if (ActiveLayaway)
            {
                strSQLActive = " and i.Status in (1) ";
            }
            else
            {
                strSQLActive = " and i.Status in (1,3) ";
            }



            strSQLComm =   " select lp.CreatedOn, lp.InvoiceNo, lp.TransactionNo, lp.Payment, "
                         + " tn.tendertype, tt.name from "
                         + " laypmts lp left outer join tender tn on lp.TransactionNo = tn.transactionno "
                         + " left outer join tendertypes tt on tt.id = tn.tendertype "
                         + " where lp.InvoiceNo in "
                         + " ( select i.ID as InvoiceNo "
                         + " from trans t left outer join invoice i on i.transactionno = t.ID "
                         + " left outer join item it on it.invoiceno = i.ID "
                         + " left outer join layaway l on i.LayawayNo = l.ID "
                         + " left outer join customer c on i.CustomerID = c.ID "
                         + " where i.LayawayNo > 0 and it.Tagged <> 'X' " + strSQLCustomer + strSQLActive
                         + "  ) "
                         + " group by lp.CreatedOn, lp.InvoiceNo, lp.TransactionNo, lp.Payment,tn.tendertype, tt.name "
                         + " order by lp.InvoiceNo ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TransactionNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PaymentDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Payment", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderType", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["InvoiceNo"].ToString(),
												   objSQLReader["TransactionNo"].ToString(),
                                                   objSQLReader["CreatedOn"].ToString(),
                                                   objSQLReader["Payment"].ToString(), 
                                                   objSQLReader["name"].ToString()
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
        
        public DataTable FetchProductLayawayData()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select c.CustomerID as CustID,c.FirstName + ' ' + c.LastName as Customer,c.Email, "
                         + " it.SKU,it.Description,isnull(sum(it.Qty),0) as Qty,d.Description as Dept, " 
                         + " p.PriceA,p.QtyOnHand,p.QtyOnLayaway,p.QtyOnHand - p.QtyOnLayaway as AdjQty "
                         + " from invoice i "
                         + " left outer join item it on it.invoiceno = i.ID "
                         + " left outer join product p on it.ProductID = p.ID "
                         + " left outer join customer c on c.ID = i.CustomerID "
                         + " left outer join dept d on d.ID = p.DepartmentID "
                         + " where i.LayawayNo > 0 and i.Status in (1) and it.Tagged <> 'X' "
                         + " group by c.CustomerID,c.FirstName,c.LastName,c.Email,it.SKU, it.Description,"
                         + " d.Description, p.PriceA, p.QtyOnHand, p.QtyOnLayaway order by it.SKU, c.CustomerID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OnHandQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OnLayawayQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AdjQty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["CustID"].ToString(),
                                                   objSQLReader["Customer"].ToString(), 
                                                   objSQLReader["Email"].ToString(), 
                                                   objSQLReader["SKU"].ToString(),
                                                   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["PriceA"].ToString(),
                                                   objSQLReader["QtyOnHand"].ToString(),
                                                   objSQLReader["QtyOnLayaway"].ToString(),
                                                   objSQLReader["AdjQty"].ToString(),
                                                   objSQLReader["Dept"].ToString()});

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

        #region Fetch Data For Packing List

        public DataTable FetchPackingListData(string refType, DateTime pStart, DateTime pEnd, string thisstore)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            if (refType == "Customer")
                strSQLComm = " select a.SKU,a.Description,sum(a.Qty) as SoldQty,t.CustomerID,isnull(c.FirstName + ' ' + c.LastName,'') as CustName, "
                           + " c.Email from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID and a.Producttype in "
                           + " ('P','K','U','M','S','W','E','F','T') left outer join  customer as c on c.ID = t.CustomerID "
                           + " where t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' "
                           + " and t.TransDate between @SDT and @TDT and c.issuestore='" + thisstore + "' "
                           + " group by a.SKU,a.Description,t.CustomerID,c.FirstName + ' ' + c.LastName,c.Email order by c.FirstName + ' ' + c.LastName,a.SKU,a.Description ";

            if (refType == "Item")
                strSQLComm = " select a.SKU,a.Description,sum(a.Qty) as SoldQty,t.CustomerID,isnull(c.FirstName + ' ' + c.LastName,'') as CustName, "
                           + " c.Email from Trans t left outer join invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID and a.Producttype in "
                           + " ('P','K','U','M','S','W','E','F','T') left outer join  customer as c on c.ID = t.CustomerID "
                           + " where  t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' "
                           + " and t.TransDate between @SDT and @TDT "
                           + " group by a.SKU,a.Description,t.CustomerID,c.FirstName + ' ' + c.LastName,c.Email order by a.SKU,a.Description,c.FirstName + ' ' + c.LastName ";
                           
                         
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ItemName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Email", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    if (objSQLReader["SKU"].ToString() == "") continue;
                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["SoldQty"].ToString(),
                                                   objSQLReader["CustomerID"].ToString(), 
                                                   objSQLReader["CustName"].ToString(),
                                                   objSQLReader["Email"].ToString() });
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

        public DataTable FetchItemDiscountReportData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            strSQLComm = " select a.Producttype as PT,i.ID,i.Discount as TDiscount,t.TransDate,convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate,a.SKU,a.Description, "
                       + " a.DiscountText,a.Discount from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " where (1 = 1) and a.Discount is not null and a.Discount <> 0 "
                       + " and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' " + strSQL1 + strSQLDate
                       + " Order by FilterDate,i.ID, a.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountText", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    dtbl.Rows.Add(new object[] {   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["DiscountText"].ToString().Trim(), 
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
                                                   objSQLReader["TDiscount"].ToString(), 
                                                   objSQLReader["FilterDate"].ToString()});
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

        public DataTable FetchItemDiscountReportData1(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            strSQLComm = " select convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate,sum(a.Discount) as Disc "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " where (1 = 1) and a.Discount is not null and a.Discount <> 0 "
                       + " and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' " + strSQL1 + strSQLDate
                       + " group by convert(datetime,convert(char(10),t.TransDate,101)) "
                       + " Order by FilterDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();
                
                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["FilterDate"].ToString(), objSQLReader["Disc"].ToString() });
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

        public DataTable FetchInvoiceDiscountReportData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('C','Z')";

            strSQLComm = " select i.ID,i.Coupon as TDiscount,t.TransDate,convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate, "
                       + " a.Description,a.Discount from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                       + " where (1 = 1) and a.Discount is not null and a.Discount <> 0 " + strSQLProduct
                       + " and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' " + strSQL1 + strSQLDate
                       + " Order by FilterDate,i.ID, a.Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   
												   objSQLReader["Description"].ToString().Trim(),
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["ID"].ToString(),
                                                   objSQLReader["TransDate"].ToString(),
                                                   objSQLReader["TDiscount"].ToString(), 
                                                   objSQLReader["FilterDate"].ToString()});
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

        public DataTable FetchInvoiceDiscountReportData1(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('C','Z')";

            strSQLComm = " select convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate,sum(a.Discount) as Disc "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                       + " where (1 = 1) and a.Discount is not null and a.Discount <> 0 " + strSQLProduct
                       + " and t.TransType in( 1,4,18 ) and i.status in (3,18) and a.Tagged <> 'X' " + strSQL1 + strSQLDate
                       + " group by convert(datetime,convert(char(10),t.TransDate,101)) "
                       + " Order by FilterDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["FilterDate"].ToString(), objSQLReader["Disc"].ToString() });
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

        public DataTable FetchCustomerSalesByProductData(int CID, DataTable refdtbl1,DataTable refdtbl2,DataTable refdtbl3,DateTime pStart,
                                                         DateTime pEnd,string thisstore, bool blDiscardProduct )
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string strSQL3 = "";
            string strSQL4 = "";

            string getdtblID1 = "";
            string getdtblID2 = "";
            string getdtblID3 = "";

            string strSQL5 = "";

            if (thisstore != "") strSQL4 = " and cs.issuestore = @store";
            if (CID > 0) strSQL5 = " and t.CustomerID = @CID ";

            foreach (DataRow dr in refdtbl1.Rows)
            {
                if (Convert.ToBoolean(dr["Check"].ToString())) 
                {
                    if (getdtblID1 == "") getdtblID1 = dr["ID"].ToString(); else getdtblID1 = getdtblID1 + "," + dr["ID"].ToString();
                }
            }

            foreach (DataRow dr in refdtbl2.Rows)
            {
                if (Convert.ToBoolean(dr["Check"].ToString()))
                {
                    if (getdtblID2 == "") getdtblID2 = dr["ID"].ToString(); else getdtblID2 = getdtblID2 + "," + dr["ID"].ToString();
                }
            }

            foreach (DataRow dr in refdtbl3.Rows)
            {
                if (Convert.ToBoolean(dr["Check"].ToString()))
                {
                    if (getdtblID3 == "") getdtblID3 = dr["ID"].ToString(); else getdtblID3 = getdtblID3 + "," + dr["ID"].ToString();
                }
            }

            if (getdtblID1 != "") strSQL1 = " and p.DepartmentID in ( " + getdtblID1 + " )";
            if (getdtblID2 != "") strSQL2 = " and p.CategoryID in ( " + getdtblID2 + " )";
            if ((getdtblID3 != "") && (!blDiscardProduct)) strSQL3 = " and p.ID in ( " + getdtblID3 + " )";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            //strSQLProduct = " and a.Producttype in ('P','K','U','M','S','W','E','F','T')";

            strSQLComm = " select t.CustomerID as CID,a.Producttype as PT,a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price,0) as Price,"
                       + " isnull(a.Cost,0) as Cost, isnull(a.Discount,0) as Discount,b.DepartmentID as DeptID, b.Description as DeptName,"
                       + " c.CategoryID as CatID, c.Description as CatName,cs.FirstName + ' ' + cs.LastName as CustomerName,"
                       + " cs.CustomerID,cs.LastName,cs.FirstName,cs.Company,cs.Address1,cs.Address2,cs.City,cs.State,cs.Country,cs.Zip,"
                       + " cs.ShipAddress1,cs.ShipAddress2,cs.ShipCity,cs.ShipState,cs.ShipCountry,cs.ShipZip,"
                       + " cs.WorkPhone,cs.HomePhone,cs.Fax,cs.MobilePhone,cs.EMail,cs.ActiveStatus,isnull(gr.Description,'') as CustGroup, "
                       + " isnull(cl.Description,'') as CustClass, isnull(p.DiscountedCost,0) as DCost from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID " + strSQLProduct
                       + " left outer join customer cs on cs.ID = t.CustomerID "
                       + " left outer join GeneralMapping gmG "
                       + " on cs.ID = gmG.MappingID and gmG.MappingType = 'Customer' and gmG.ReferenceType = 'Group' "
                       + " left outer join GeneralMapping gmC on cs.ID = gmC.MappingID and gmC.MappingType = 'Customer' and "
                       + " gmC.ReferenceType = 'Class' "
                       + " left outer join GroupMaster gr on gr.ID = gmG.ReferenceID and gmG.MappingType = 'Customer' and "
                       + " gmG.ReferenceType = 'Group' and cs.ID = gmG.MappingID "
                       + " left outer join ClassMaster cl on cl.ID = gmC.ReferenceID and gmC.MappingType = 'Customer' and "
                       + " gmC.ReferenceType = 'Class' and cs.ID = gmC.MappingID "
                       + " left outer join Product p on a.ProductID = p.ID and p.ProductStatus = 'Y' "
                       + " left outer join Dept as b on p.DepartmentID = b.ID "
                       + " left outer join category c on p.CategoryID = c.ID "
                       + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                       + " and t.TransType in( 1,4,18 ) and i.status in(3,18) and a.Tagged <> 'X' and i.CustomerID > 0 " + strSQL4
                       + strSQL5 + strSQL1 + strSQL2 + strSQL3 + strSQLDate
                       + " Order by t.CustomerID,a.SKU ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                if (CID > 0)
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@CID", System.Data.SqlDbType.Int));
                    objSQlComm.Parameters["@CID"].Value = CID;
                }
                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                if (thisstore != "")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@store", System.Data.SqlDbType.NVarChar));
                    objSQlComm.Parameters["@store"].Value = thisstore;
                }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Item", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DCost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DeptName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CatID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CatName", System.Type.GetType("System.String"));

                dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CustomerName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MailAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ShipAddress", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
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
                dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CGroup", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CClass", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;
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
                    if (strMailAddress != "")
                        strMailAddress = strMailAddress + "\n" + strMailCityLine;
                    else
                        strMailAddress = strMailCityLine;

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

                    dtbl.Rows.Add(new object[] {   objSQLReader["CID"].ToString(),
                                                   objSQLReader["SKU"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["Qty"].ToString(),
                                                   objSQLReader["Price"].ToString(), 
                                                   objSQLReader["Discount"].ToString(), 
                                                   objSQLReader["Cost"].ToString(),
                                                    objSQLReader["DCost"].ToString(),
                                                   objSQLReader["DeptID"].ToString(),
                                                   objSQLReader["DeptName"].ToString(),
                                                   objSQLReader["CatID"].ToString(),
                                                   objSQLReader["CatName"].ToString(),
                                                   
                                                   objSQLReader["CustomerID"].ToString(),
                                                   objSQLReader["CustomerName"].ToString(),
                                                   strMailAddress,strShipAddress,
                                                   objSQLReader["Company"].ToString(),
                                                   objSQLReader["FirstName"].ToString(),
                                                   objSQLReader["LastName"].ToString(),
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
                                                   objSQLReader["ActiveStatus"].ToString(),
                                                   objSQLReader["CustGroup"].ToString(),
                                                   objSQLReader["CustClass"].ToString()});
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

        #region Sales By Period

        public DataTable FetchSaleByPeriodReportData(int PeriodIndex, string refMoreFilter, int MoreFilterID,DateTime pStart, DateTime pEnd, string pTaxInclude)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strType = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";
            string strSQL2 = "";
            string getdtblID = "";

            int chkcnt = 0;

            if (refMoreFilter != "")
            {
                if (MoreFilterID != 0)
                {
                    if (refMoreFilter == "Department") strSQL1 = " and a.DepartmentID in ( " + MoreFilterID + " )";
                    if (refMoreFilter == "POS Screen Category") strSQL1 = " and a.CategoryID in ( " + MoreFilterID + " )";
                }
            }

            strSQLDate = " and t.TransDate between @SDT and @TDT ";

            if (PeriodIndex == 1) // Daily Report
            {
                strSQLComm = " select CONVERT(VARCHAR, t.TransDate, 101) as Group1, "
                           + " CONVERT(VARCHAR, t.TransDate, 101) as Group2, t.TransDate as Group3, a.ProductType as PT, "
                           + " isnull(sum(a.Price * a.Qty - a.Discount),0) as Price, isnull(sum(a.TaxIncludeRate * a.Qty),0) as PriceI, isnull(sum(a.Cost *  a.Qty),0) as Cost, isnull(sum(p.DiscountedCost *  a.Qty),0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join Dept AS d ON a.DepartmentID = d.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " group by CONVERT(VARCHAR, t.TransDate, 101), a.ProductType,t.TransDate "
                           + " Order by t.TransDate ";
            }


            if (PeriodIndex == 2) // Weekly Report
            {
                strSQLComm = " select Convert(varchar, DateAdd(dd, -(DatePart(dw, t.TransDate) - 1),t.TransDate), 101) + '  -  ' + Convert(varchar, DateAdd(dd, (7 - DatePart(dw, t.TransDate)), t.TransDate), 101)  as Group1, "
                           + " CONVERT(VARCHAR, t.TransDate, 101) as Group2, t.TransDate as Group3, a.ProductType as PT, "
                           + " isnull(sum(a.Price * a.Qty - a.Discount),0) as Price, isnull(sum(a.TaxIncludeRate * a.Qty),0) as PriceI, isnull(sum(a.Cost *  a.Qty),0) as Cost, isnull(sum(p.DiscountedCost *  a.Qty),0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join Dept AS d ON a.DepartmentID = d.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " group by Convert(varchar, DateAdd(dd, -(DatePart(dw, t.TransDate) - 1), t.TransDate), 101) + '  -  ' + Convert(varchar, DateAdd(dd, (7 - DatePart(dw, t.TransDate)), t.TransDate), 101) , CONVERT(VARCHAR, t.TransDate, 101), a.ProductType,t.TransDate "
                           + " Order by t.TransDate ";
            }
            
            if (PeriodIndex == 3) // Monthly Report
            {
                strSQLComm = " select CONVERT(CHAR(4), t.TransDate, 100) + CONVERT(CHAR(4), t.TransDate, 120) as Group1, "
                           + " CONVERT(VARCHAR, t.TransDate, 101) as Group2, t.TransDate as Group3, a.ProductType as PT, "
                           + " isnull(sum(a.Price * a.Qty - a.Discount),0) as Price, isnull(sum(a.TaxIncludeRate * a.Qty),0) as PriceI, isnull(sum(a.Cost *  a.Qty),0) as Cost, isnull(sum(p.DiscountedCost *  a.Qty),0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join Dept AS d ON a.DepartmentID = d.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " group by CONVERT(CHAR(4), t.TransDate, 100) + CONVERT(CHAR(4), t.TransDate, 120), CONVERT(VARCHAR, t.TransDate, 101), a.ProductType,t.TransDate "
                           + " Order by t.TransDate ";
            }

            if (PeriodIndex == 4) // Yearly Report
            {
                strSQLComm = " select CONVERT(CHAR(4), t.TransDate, 120)  as Group1, "
                           + " CONVERT(CHAR(4), t.TransDate, 100)  as Group2, a.ProductType as PT, datepart(YEAR,t.TransDate),datepart(MONTH,t.TransDate), "
                           + " isnull(sum(a.Price * a.Qty - a.Discount),0) as Price, isnull(sum(a.TaxIncludeRate * a.Qty),0) as PriceI, isnull(sum(a.Cost *  a.Qty),0) as Cost, isnull(sum(p.DiscountedCost *  a.Qty),0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join Dept AS d ON a.DepartmentID = d.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " group by CONVERT(CHAR(4), t.TransDate, 120), CONVERT(CHAR(4), t.TransDate, 100),  a.ProductType, datepart(YEAR,t.TransDate),datepart(MONTH,t.TransDate) "
                           + " Order by datepart(YEAR,t.TransDate),datepart(MONTH,t.TransDate) ";
            }

            if (PeriodIndex == 0) // Hourly Report
            {
                strSQLComm = " select CONVERT(VARCHAR, t.TransDate, 101) as Group1, "
                           + " datepart(HOUR,t.TransDate) as Group2,t.TransDate as Group3, a.ProductType as PT, "
                           + " isnull(sum(a.Price * a.Qty - a.Discount),0) as Price, isnull(sum(a.TaxIncludeRate * a.Qty),0) as PriceI, isnull(sum(a.Cost *  a.Qty),0) as Cost, isnull(sum(p.DiscountedCost *  a.Qty),0) as DCost "
                           + " from Trans t left outer join Invoice i ON t.ID = i.TransactionNo "
                           + " left outer join item a ON a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join Category AS b ON a.CategoryID = b.ID "
                           + " left outer join Dept AS d ON a.DepartmentID = d.ID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and p.ProductStatus = 'Y' " + strSQL1 + strSQLDate
                           + " group by CONVERT(VARCHAR, t.TransDate, 101),  datepart(HOUR,t.TransDate), a.ProductType,t.TransDate "
                           + " Order by t.TransDate, datepart(HOUR,t.TransDate) ";
            }

            
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("Group1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Group2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Price", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));

                string grp2 = "";
                while (objSQLReader.Read())
                {
                    if ((objSQLReader["PT"].ToString() == "G") || (objSQLReader["PT"].ToString() == "A") || (objSQLReader["PT"].ToString() == "C")
                        || (objSQLReader["PT"].ToString() == "O") || (objSQLReader["PT"].ToString() == "X") || (objSQLReader["PT"].ToString() == "Z")
                        || (objSQLReader["PT"].ToString() == "H")) continue;

                    if (PeriodIndex == 0)
                    {
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 0) grp2 = "12:00 M -   1:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 1) grp2 = "1:00 AM -   2:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 2) grp2 = "2:00 AM -   3:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 3) grp2 = "3:00 AM -   4:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 4) grp2 = "4:00 AM -   5:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 5) grp2 = "5:00 AM -   6:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 6) grp2 = "6:00 AM -   7:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 7) grp2 = "7:00 AM -   8:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 8) grp2 = "8:00 AM -   9:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 9) grp2 = "9:00 AM -   10:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 10) grp2 = "10:00 AM -   11:00 AM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 11) grp2 = "11:00 AM -   12:00 N";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 12) grp2 = "12:00 N -   1:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 13) grp2 = "1:00 PM -   2:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 14) grp2 = "2:00 PM -   3:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 15) grp2 = "3:00 PM -   4:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 16) grp2 = "4:00 PM -   5:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 17) grp2 = "5:00 PM -   6:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 18) grp2 = "6:00 PM -   7:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 19) grp2 = "7:00 PM -   8:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 20) grp2 = "8:00 PM -   9:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 21) grp2 = "9:00 PM -   10:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 22) grp2 = "10:00 PM -  11:00 PM";
                        if (Functions.fnInt32(objSQLReader["Group2"].ToString()) == 23) grp2 = "11:00 PM -  12:00 PM";
                    }
                    else if (PeriodIndex == 1)
                    {
                        grp2 = "";
                    }
                    else
                    {
                        grp2 = objSQLReader["Group2"].ToString();
                    }
                    
                    dtbl.Rows.Add(new object[] {   objSQLReader["Group1"].ToString(),
                                                   grp2,
                                                   pTaxInclude == "N" ? objSQLReader["Price"].ToString() : objSQLReader["PriceI"].ToString(),
                                                   objSQLReader["Cost"].ToString(),
                                                   objSQLReader["DCost"].ToString()
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

        #endregion

         #region Sales Summary ( Faster Approach )

        public int ExecSalesSummaryProcedure(DateTime fD, DateTime tD, string Teml, string taxinclude="")
        {
            DateTime FormatStartDate = new DateTime(fD.Year, fD.Month, fD.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(tD.Year, tD.Month, tD.Day, 23, 59, 59);

            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_salessummary";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.CommandTimeout = 1000;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@tax_inclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@tax_inclusive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@tax_inclusive"].Value = taxinclude;

                objSQlComm.Parameters.Add(new SqlParameter("@f_date", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@f_date"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@f_date"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@t_date", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@t_date"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@t_date"].Value = FormatEndDate;

                objSQlComm.Parameters.Add(new SqlParameter("@terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@terminal"].Value = Teml;

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

                return intReturn;
            }
        }

        public DataTable FetchSalesSummaryData(string Teml)
        {
            
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLComm  = " select * from summarydata where terminalname = @param ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@param"].Value = Teml;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ProductSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OtherSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("NonTaxableSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_Info_1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_Info_2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxableSales_Info_3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DestinationTax", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxOnFees_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ServiceDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OtherDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TicketDiscount_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FS_Tender", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Layaway_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GC_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("MGC_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PGC_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DGC_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("PLGC_Sales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CostOfGoods", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BootleRefund", System.Type.GetType("System.String"));
                dtbl.Columns.Add("P_Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("P_Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("P_Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("S_Tax3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("B_Tax1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("B_Tax2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("B_Tax3", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RentExist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RentSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TicketDiscount_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees_Rent", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxOnFees_Rent", System.Type.GetType("System.String"));

                dtbl.Columns.Add("RepairExist", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairSales", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName1_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName2_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName3_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax1_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax2_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Tax3_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RepairDiscount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TicketDiscount_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Fees_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxOnFees_Repair", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Repair_Deposit", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Free_Qty", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Free_Amount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Paidout", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LottoPayout", System.Type.GetType("System.String"));
                dtbl.Columns.Add("GiftAid", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ProductSales"].ToString(),
												   objSQLReader["ServiceSales"].ToString(),
                                                   objSQLReader["OtherSales"].ToString(), 
                                                   objSQLReader["TaxName1_Sales"].ToString().Trim(), 
                                                   objSQLReader["TaxName2_Sales"].ToString(),
                                                   objSQLReader["TaxName3_Sales"].ToString(),
                                                   objSQLReader["Tax1_Sales"].ToString(), 
                                                   objSQLReader["Tax2_Sales"].ToString(),
                                                   objSQLReader["Tax3_Sales"].ToString(),
												   objSQLReader["TaxableSales_1"].ToString(),
                                                   objSQLReader["TaxableSales_2"].ToString(), 
                                                   objSQLReader["TaxableSales_3"].ToString().Trim(), 
                                                   objSQLReader["NonTaxableSales"].ToString(),
                                                   objSQLReader["TaxableSales_Info_1"].ToString(),
                                                   objSQLReader["TaxableSales_Info_2"].ToString(), 
                                                   objSQLReader["TaxableSales_Info_3"].ToString(),
                                                   objSQLReader["DestinationTax"].ToString(),
												   objSQLReader["Fees_Sales"].ToString(),
                                                   objSQLReader["TaxOnFees_Sales"].ToString(), 
                                                   objSQLReader["ProductDiscount"].ToString().Trim(), 
                                                   objSQLReader["ServiceDiscount"].ToString(),
                                                   objSQLReader["OtherDiscount"].ToString(),
                                                   objSQLReader["TicketDiscount_Sales"].ToString(), 
                                                   objSQLReader["FS_Tender"].ToString(),
                                                   objSQLReader["Layaway_Sales"].ToString(),
												   objSQLReader["GC_Sales"].ToString(),
                                                   objSQLReader["MGC_Sales"].ToString(), 
                                                   objSQLReader["PGC_Sales"].ToString(), 
                                                   objSQLReader["DGC_Sales"].ToString().Trim(), 
                                                   objSQLReader["PLGC_Sales"].ToString().Trim(), 
                                                   objSQLReader["CostOfGoods"].ToString(),
                                                   objSQLReader["BootleRefund"].ToString(),
                                                   objSQLReader["P_Tax1"].ToString(),
                                                   objSQLReader["P_Tax2"].ToString(),
                                                   objSQLReader["P_Tax3"].ToString(),
                                                   objSQLReader["S_Tax1"].ToString(),
                                                   objSQLReader["S_Tax2"].ToString(),
                                                   objSQLReader["S_Tax3"].ToString(),
                                                   objSQLReader["B_Tax1"].ToString(),
                                                   objSQLReader["B_Tax2"].ToString(),
                                                   objSQLReader["B_Tax3"].ToString(),
                                                   objSQLReader["RentExist"].ToString(), 
                                                   objSQLReader["RentSales"].ToString(),
                                                   objSQLReader["TaxName1_Rent"].ToString(),
												   objSQLReader["TaxName2_Rent"].ToString(),
                                                   objSQLReader["TaxName3_Rent"].ToString(), 
                                                   objSQLReader["Tax1_Rent"].ToString().Trim(), 
                                                   objSQLReader["Tax2_Rent"].ToString(),
                                                   objSQLReader["Tax3_Rent"].ToString(),
                                                   objSQLReader["TicketDiscount_Rent"].ToString(), 
                                                   objSQLReader["Fees_Rent"].ToString(),
                                                   objSQLReader["TaxOnFees_Rent"].ToString(), 

                                                   objSQLReader["RepairExist"].ToString(),
                                                   objSQLReader["RepairSales"].ToString(),
												   objSQLReader["TaxName1_Repair"].ToString(),
                                                   objSQLReader["TaxName2_Repair"].ToString(), 
                                                   objSQLReader["TaxName3_Repair"].ToString().Trim(), 
                                                   objSQLReader["Tax1_Repair"].ToString(),
                                                   objSQLReader["Tax2_Repair"].ToString(),
                                                   objSQLReader["Tax3_Repair"].ToString(), 
                                                   objSQLReader["RepairDiscount"].ToString(),
                                                   objSQLReader["TicketDiscount_Repair"].ToString(),
                                                   objSQLReader["Fees_Repair"].ToString(), 
                                                   objSQLReader["TaxOnFees_Repair"].ToString(),
                                                   objSQLReader["Repair_Deposit"].ToString(),
                                                   objSQLReader["Free_Qty"].ToString(),
                                                   objSQLReader["Free_Amount"].ToString(),
                                                   objSQLReader["Paidout"].ToString(),
                                                   objSQLReader["LottoPayout"].ToString(),
                                                   objSQLReader["GiftAid"].ToString()});
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


        public DataTable FetchSalesSummaryDataTender_Sales(string Teml)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLComm = @" select isnull(TenderID,0) as tId, isnull(TenderName,'') as tName, isnull(sum(TenderAmount),0) as tAmt 
                            from SummaryDataTender where terminalname = @param and TransType in (1,2) group by TenderID, TenderName order by TenderID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@param"].Value = Teml;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TenderName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["tName"].ToString(),
                                                   objSQLReader["tAmt"].ToString()});
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

        public DataTable FetchSalesSummaryDataTender_Paidout(string Teml)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLComm = @" select isnull(TenderID,0) as tId, isnull(TenderName,'') as tName, isnull(sum(TenderAmount),0) as tAmt 
                            from SummaryDataTender where terminalname = @param and TransType = 6 group by TenderID, TenderName order by TenderID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@param"].Value = Teml;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("TenderName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TenderAmount", System.Type.GetType("System.String"));


                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["tName"].ToString(),
                                                   objSQLReader["tAmt"].ToString()});
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


        public int ExecDashboardReport(string Teml, string TaxI)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_Dashboard_Report";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.CommandTimeout = 1000;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@Terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Terminal"].Value = Teml;

                objSQlComm.Parameters.Add(new SqlParameter("@TaxInclusive", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@TaxInclusive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TaxInclusive"].Value = TaxI;

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

                return intReturn;
            }
        }

        public int ExecDashboardReport_Date(string Teml,DateTime FDate, DateTime TDate, string pTaxI)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_Dashboard_Report_Date";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                objSQlComm.CommandTimeout = 1000;
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.StoredProcedure;

                objSQlComm.Parameters.Add(new SqlParameter("@Terminal", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@Terminal"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Terminal"].Value = Teml;

                objSQlComm.Parameters.Add(new SqlParameter("@FDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@FDate"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@FDate"].Value = new DateTime(FDate.Year,FDate.Month,FDate.Day,0,0,0);

                objSQlComm.Parameters.Add(new SqlParameter("@TDate", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDate"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TDate"].Value = new DateTime(TDate.Year, TDate.Month, TDate.Day, 23, 59, 59);

                objSQlComm.Parameters.Add(new SqlParameter("@TaxInclusive", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@TaxInclusive"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@TaxInclusive"].Value = pTaxI;

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

                return intReturn;
            }
        }



        public DataTable FetchDashboardReportData(string Teml)
        {

            DataTable dtbl = new DataTable();
            string strSQLComm = "";

            strSQLComm = " select * from dashboard where terminalname = @param order by RecordType, RecordSL, RecordDescription ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@param"].Value = Teml;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("RecordType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecordSL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecordDescription", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecordCount", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RecordAmount", System.Type.GetType("System.String"));
                

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["RecordType"].ToString(),
												   objSQLReader["RecordSL"].ToString(),
                                                   objSQLReader["RecordDescription"].ToString(), 
                                                   objSQLReader["RecordCount"].ToString().Trim(), 
                                                   objSQLReader["RecordAmount"].ToString() });
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

        public DataTable FetchSaleBreakupData(DateTime pStart, DateTime pEnd, string TaxIncludeFlag)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";



            string strSQLDate = " and t.TransDate between @SDT and @TDT ";

            strSQLComm = " select a.ProductID, a.SKU,a.Description,isnull(a.Qty,0) as Qty,isnull(a.Price*a.Qty,0) as Price, isnull(a.TaxIncludePrice,0) as PriceI, isnull(a.Cost * a.Qty,0) as Cost, "
                           + " isnull(a.Discount,0) as Discount, isnull(TaxTotal1 + TaxTotal2 + TaxTotal3,0) as Tax, isnull(p.DiscountedCost * a.Qty,0) as DCost "
                           + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                           + " left outer join item a on a.InvoiceNo = i.ID or a.InvoiceNo = i.RepairParentID "
                           + " left outer join  Product p on a.ProductID = p.ID "
                           + " left outer join  BrandMaster br on p.BrandID = br.ID "
                           + " where (1 = 1) and a.Qty is not null and a.Cost is not null "
                           + " and t.TransType in( 1,4,18) and i.status in (3,18) and a.Tagged <> 'X' and a.Producttype in ('P','K','U','M','S','W','E','F','T','B') " + strSQLDate
                           + " Order by a.Description ";



            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ProductID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Sales_Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Discount", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Sales_PreTax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));

                double qty = 0;
                double sales_tax = 0;
                double disc = 0;
                double tax = 0;
                double cost = 0;
                double dcost = 0;
                double sales_pretax = 0;
                double profit = 0;

                while (objSQLReader.Read())
                {
                    qty = Functions.fnDouble(objSQLReader["Qty"].ToString());
                    disc = Functions.fnDouble(objSQLReader["Discount"].ToString());
                    cost = Functions.fnDouble(objSQLReader["Cost"].ToString());
                    dcost = Functions.fnDouble(objSQLReader["DCost"].ToString());
                    tax = Functions.fnDouble(objSQLReader["Tax"].ToString());

                    if (TaxIncludeFlag == "Y")
                    {
                        sales_tax = Functions.fnDouble(objSQLReader["PriceI"].ToString());
                        sales_pretax = sales_tax - tax;
                        profit = sales_pretax - cost;
                    }

                    if (TaxIncludeFlag == "N")
                    {
                        sales_tax = Functions.fnDouble(objSQLReader["Price"].ToString());
                        sales_pretax = sales_tax - disc;
                        profit = sales_pretax - cost - tax;
                    }

                    dtbl.Rows.Add(new object[] {   Functions.fnInt32(objSQLReader["ProductID"].ToString()),
                                                   objSQLReader["Description"].ToString(),
                                                   qty,
                                                   sales_tax,
                                                   disc,
                                                   tax,
                                                   sales_pretax,
                                                   cost,
                                                   dcost,
                                                   profit});
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


        public DataTable FetchGiftAidReportGroupData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('I')";

            strSQLComm = " select convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate,sum(i.TotalSale) as TotalValue "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                       + " where (1 = 1) "
                       + " and t.TransType = 1 and i.status in (3) and isnull(i.GiftAidFlag,'N') = 'Y' " + strSQL1 + strSQLDate
                       + " group by convert(datetime,convert(char(10),t.TransDate,101)) "
                       + " Order by FilterDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Total", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["FilterDate"].ToString(), objSQLReader["TotalValue"].ToString() });
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

        public DataTable FetchGiftAidReportDetailData(DateTime pStart, DateTime pEnd)
        {
            DateTime FormatStartDate = new DateTime(pStart.Year, pStart.Month, pStart.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(pEnd.Year, pEnd.Month, pEnd.Day, 23, 59, 59);

            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            string strSQLDate = "";
            string strSQLProduct = "";
            string strSQL1 = "";

            strSQLDate = " and t.TransDate between @SDT and @TDT ";
            strSQLProduct = " and a.Producttype in ('I')";

            strSQLComm = " select convert(datetime,convert(char(10),t.TransDate,101)) as FilterDate, a.InvoiceNo, a.Price, a.DiscountText as Name, isnull(a.Notes,'') as Address  "
                       + " from Trans t left outer join Invoice i on t.ID = i.TransactionNo "
                       + " left outer join item a on a.InvoiceNo = i.ID " + strSQLProduct
                       + " where (1 = 1) "
                       + " and t.TransType = 1 and i.status in (3) and isnull(i.GiftAidFlag,'N') = 'Y' " + strSQL1 + strSQLDate
                       + " Order by FilterDate ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.CommandTimeout = 500;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }


                objSQlComm.Parameters.Add(new SqlParameter("@SDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@SDT"].Value = FormatStartDate;

                objSQlComm.Parameters.Add(new SqlParameter("@TDT", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters["@TDT"].Value = FormatEndDate;

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Address", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objSQLReader["FilterDate"].ToString(), objSQLReader["InvoiceNo"].ToString(),
                    objSQLReader["Name"].ToString(),objSQLReader["Address"].ToString(),objSQLReader["Price"].ToString()});
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

    }
}
