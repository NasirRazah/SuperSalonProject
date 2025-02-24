/*
 purpose : Various common functions used in POSDATAOBJECT
 */

using System;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Functions
    {
        public Functions()
        { }

        #region Update Item Stock Balances

        public static void UpdateItemStockBalance(SqlCommand objCommand, int ItemID, double ItemQuantity, int LogonUser)
        {
            objCommand.Parameters.Clear();
            double refqty = 0;
            int refid = 0;
            GetStockInfo(objCommand, ItemID, ItemQuantity, ref refid, ref refqty);
            objCommand.Parameters.Clear();
            UpdateCurrentStock(objCommand,refid,refqty, LogonUser);
        }

        public static int IfStockDetailsAvailable(SqlCommand objCommand, int ItemID)
        {
            if (ItemID == 0) return -1;

            objCommand.Parameters.Clear();
            int intResult = -1;
            string strSQLComm = "";

            strSQLComm = "select ID from PRODUCT where ID=@ITEMID ";

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
                    intResult = Functions.fnInt32(objSQLReader["ID"]);
                }
                objSQLReader.Close();
                return intResult;
            }
            catch
            {
                return -1;
            }
        }

        public static void UpdateCurrentStock(SqlCommand objCommand, int ID, double ItemQuantity, int LogonUser)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = " update product set QtyOnHand=QtyOnHand+@QtyOnHand,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn, "
                              + " expflag = 'N' where ID = @ID";
            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objCommand.Parameters["@QtyOnHand"].Value = ItemQuantity;
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

        #endregion

        #region Update Store Credit Balances

        public static void UpdateStoreCreditBalance(SqlCommand objCommand, int CustID, double Amount, int LogonUser)
        {
            objCommand.Parameters.Clear();
            UpdateStoreCredit(objCommand, CustID, Amount, LogonUser);
        }

        public static void UpdateCustomerPurchaseRecord(SqlCommand objCommand, int CustID, double Amount, DateTime dtNow, int LogonUser,string opstore)
        {
            objCommand.Parameters.Clear();
            UpdateCustomerPurchase(objCommand, CustID, Amount, dtNow, LogonUser, opstore);
        }

        public static void UpdateCustomerPurchase(SqlCommand objCommand, int ID, double Amount, DateTime dtLastPurchase, int LogonUser, string opstore)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = " update Customer set TotalPurchases = TotalPurchases + @TotalPurchases,DateLastPurchase=@DateLastPurchase, "
                              + " AmountLastPurchase=@AmountLastPurchase,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                              + " OperateStore=@OperateStore,ExpFlag='Y' where ID = @ID";
            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@TotalPurchases", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@DateLastPurchase", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@AmountLastPurchase", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objCommand.Parameters["@TotalPurchases"].Value = Amount;
                objCommand.Parameters["@DateLastPurchase"].Value = dtLastPurchase;
                objCommand.Parameters["@AmountLastPurchase"].Value = Amount;
                objCommand.Parameters["@LastChangedBy"].Value = LogonUser;
                objCommand.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objCommand.Parameters["@ID"].Value = ID;
                objCommand.Parameters["@OperateStore"].Value = opstore;

                objCommand.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                objCommand.Dispose();
            }
        }

        public static void UpdateCustomerPointRecord(SqlCommand objCommand, int CustID, int iPoints, int LogonUser, string opstore)
        {
            objCommand.Parameters.Clear();
            UpdateCustomerPoint(objCommand, CustID, iPoints, LogonUser, opstore);
        }

        public static void UpdateCustomerPoint(SqlCommand objCommand, int ID, int iPoints, int LogonUser, string opstore)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = " update Customer set points = points + @points,LastChangedBy=@LastChangedBy,LastChangedOn=@LastChangedOn, "
                              + " OperateStore=@OperateStore,ExpFlag='N' where ID = @ID";

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@points", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@OperateStore", System.Data.SqlDbType.NVarChar));

                objCommand.Parameters["@points"].Value = iPoints;
                objCommand.Parameters["@LastChangedBy"].Value = LogonUser;
                objCommand.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objCommand.Parameters["@ID"].Value = ID;
                objCommand.Parameters["@OperateStore"].Value = opstore;

                objCommand.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                objCommand.Dispose();
            }
        }

        public static void UpdateStoreCredit(SqlCommand objCommand, int ID, double Amount, int LogonUser)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = "update Customer set StoreCredit = StoreCredit + @StoreCredit, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID";

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@StoreCredit", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objCommand.Parameters["@StoreCredit"].Value = Amount;
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

        #endregion

        #region Update Matrix Item Stock
        public static void UpdateMatrixItemStock(SqlCommand objCommand, int MOID, string MOV1, string MOV2, string MOV3,
                                                    double ItemQuantity, int LogonUser)
        {
            objCommand.Parameters.Clear();
            UpdateMatrixCurrentStock(objCommand,MOID,MOV1,MOV2,MOV3,ItemQuantity,LogonUser);
        }

        public static void UpdateMatrixCurrentStock(SqlCommand objCommand, int ID, string MOV1, string MOV2, string MOV3,
                                                double ItemQuantity, int LogonUser)
        {
            objCommand.Parameters.Clear();
            string strSQLComm = "UPDATE MATRIX set QtyOnHand = QtyOnHand + @QtyOnHand,  "
                + "LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn "
                + "Where MatrixOptionID = @ID and OptionValue1=@OV1 and OptionValue2=@OV2 and OptionValue3=@OV3 ";

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@QtyOnHand", System.Data.SqlDbType.Float));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@OV1", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters.Add(new SqlParameter("@OV2", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters.Add(new SqlParameter("@OV3", System.Data.SqlDbType.NVarChar));

                objCommand.Parameters["@QtyOnHand"].Value = ItemQuantity;
                objCommand.Parameters["@LastChangedBy"].Value = LogonUser;
                objCommand.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objCommand.Parameters["@ID"].Value = ID;
                objCommand.Parameters["@OV1"].Value = MOV1;
                objCommand.Parameters["@OV2"].Value = MOV2;
                objCommand.Parameters["@OV3"].Value = MOV3;

                objCommand.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                objCommand.Dispose();
            }
        }

        #endregion
        
        #region Stock Journal

        public static int AddStockJournal(SqlCommand objCommand, string TranType, string TranSubType, string IsUpdateProduct,
                                            int ItemID,int EmpID,double ItemQuantity,
                                            double ItemCost, string DocNo, string Terminal, DateTime DocDate,
                                            DateTime TranDate, string jNotes)
        {
            objCommand.Parameters.Clear();
            int ret = AddJournal(objCommand, TranType, TranSubType, IsUpdateProduct,
                                            ItemID,EmpID,ItemQuantity,
                                            ItemCost, DocNo, Terminal, DocDate,
                                            TranDate, jNotes);
            return ret;
        }

        public static int AddJournal(SqlCommand objCommand, string TranType, string TranSubType, string IsUpdateProduct,
                                            int ItemID, int EmpID, double ItemQuantity,
                                            double ItemCost, string DocNo, string Terminal, DateTime DocDate,
                                            DateTime TranDate, string jNotes)
        {
            if (ItemID == 0) return -1;

            objCommand.Parameters.Clear();
            int intResult = -1;
            string strSQLComm = "";

            strSQLComm = "sp_AddJournal";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.Add(new SqlParameter("@DocNo", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters["@DocNo"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@DocNo"].Value = DocNo;

                objCommand.Parameters.Add(new SqlParameter("@DocDate", System.Data.SqlDbType.DateTime));
                objCommand.Parameters["@DocDate"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@DocDate"].Value = DocDate;

                objCommand.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@ProductID"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@ProductID"].Value = ItemID;

                objCommand.Parameters.Add(new SqlParameter("@TranType", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters["@TranType"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@TranType"].Value = TranType;

                objCommand.Parameters.Add(new SqlParameter("@TranSubType", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters["@TranSubType"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@TranSubType"].Value = TranSubType;

                objCommand.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objCommand.Parameters["@Qty"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@Qty"].Value = ItemQuantity;

                objCommand.Parameters.Add(new SqlParameter("@Cost", System.Data.SqlDbType.Float));
                objCommand.Parameters["@Cost"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@Cost"].Value = ItemCost;

                objCommand.Parameters.Add(new SqlParameter("@TerminalName", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters["@TerminalName"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@TerminalName"].Value = Terminal;

                objCommand.Parameters.Add(new SqlParameter("@EmpID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@EmpID"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@EmpID"].Value = EmpID;

                objCommand.Parameters.Add(new SqlParameter("@TranDate", System.Data.SqlDbType.DateTime));
                objCommand.Parameters["@TranDate"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@TranDate"].Value = TranDate;

                objCommand.Parameters.Add(new SqlParameter("@Notes", System.Data.SqlDbType.NVarChar));
                objCommand.Parameters["@Notes"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@Notes"].Value = jNotes;

                objCommand.Parameters.Add(new SqlParameter("@IsUpdateProduct", System.Data.SqlDbType.Char));
                objCommand.Parameters["@IsUpdateProduct"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@IsUpdateProduct"].Value = IsUpdateProduct;

                objCommand.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                intResult = Functions.fnInt32(objCommand.Parameters["@ReturnID"].Value);
                
                return intResult;
            }
            catch
            {
                objCommand.Dispose();
                return -1;
            }
        }

        #endregion

        public static int GetDecimalLength(string mVal)
        {
            if (!mVal.Contains("."))
            {
                return 0;
            }
            else
            {
                string strDecimal = mVal.Substring(mVal.IndexOf(".") + 1);
                if ((strDecimal == "0") || (strDecimal == "00") || (strDecimal == "000"))
                {
                    return 0;
                }
                else
                {
                    return strDecimal.Length;
                }
            }

        }

        public static int fnInt32(object pval)
        {
            int rval = 0;
            bool blret = false;
            blret = Int32.TryParse(pval.ToString(), out rval);
            if (blret) rval = Convert.ToInt32((object)pval); else rval = 0;
            return rval;
        }

        public static decimal fnDecimal(object pval)
        {
            decimal rval = 0.0m;
            bool blret = false;
            blret = decimal.TryParse(pval.ToString(), out rval);
            if (blret) rval = Convert.ToDecimal((object)pval); else rval = 0;
            return rval;
        }

        public static double fnDouble(object pval)
        {
            double rval = 0.0;

            bool blret = false;
            blret = double.TryParse(pval.ToString(), out rval);
            if (blret) rval = Convert.ToDouble((object)pval); else rval = 0;
            return rval;
        }

        public static DateTime fnDate(object pval)
        {
            DateTime rval = DateTime.Now;
            bool blret = false;
            blret = DateTime.TryParse(pval.ToString(), out rval);
            if (blret) rval = Convert.ToDateTime((object)pval); else rval = DateTime.Now;
            return rval;
        }


        public static void GetStockInfo(SqlCommand objCommand, int ItemID,double ItemQuantity , ref int rfID, ref double rfQty)
        {
            objCommand.Parameters.Clear();
            double intResult = -1;
            string strSQLComm = "";

            strSQLComm = "sp_getstockinfoforupdate";

            objCommand.CommandText = strSQLComm;
            objCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                objCommand.CommandText = strSQLComm;
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@ProductID"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@ProductID"].Value = ItemID;

                objCommand.Parameters.Add(new SqlParameter("@Qty", System.Data.SqlDbType.Float));
                objCommand.Parameters["@Qty"].Direction = ParameterDirection.Input;
                objCommand.Parameters["@Qty"].Value = ItemQuantity;

                objCommand.Parameters.Add(new SqlParameter("@ReturnQty", System.Data.SqlDbType.Float));
                objCommand.Parameters["@ReturnQty"].Direction = ParameterDirection.Output;

                objCommand.Parameters.Add(new SqlParameter("@ReturnID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@ReturnID"].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                rfQty = Functions.fnDouble(objCommand.Parameters["@ReturnQty"].Value);
                rfID = Functions.fnInt32(objCommand.Parameters["@ReturnID"].Value);

            }
            catch
            {
                objCommand.Dispose();
            }
        }
        
    }
}
