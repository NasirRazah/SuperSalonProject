/*
 purpose : Data for Tax
*/

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Tax
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private int intID;
        private int intNewID;
        private int intLoginUserID;
        private string strMode;
        private string strErrorMsg;

        private string strTaxName;
        private int intTaxType;
        private double dblTaxRate;
        private string strActive;
        private string strBookerID = "";

        private double dblTax;
        private double dblBreakpoints;

        private SqlTransaction objSQLTran;
        private DataTable dblSplitDataTable;
        private DataTable dblInitialDataTable;

        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        // Settings Data
        private int intDecimalPlace;

        private int intLinkGL;

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

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        } 

        public string Mode
        {
            get { return strMode; }
            set { strMode = value; }
        }

        public string Active
        {
            get { return strActive; }
            set { strActive = value; }
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

        public string TaxName
        {
            get { return strTaxName; }
            set { strTaxName = value; }
        }

        public string BookerID
        {
            get { return strBookerID; }
            set { strBookerID = value; }
        }

        public int TaxType
        {
            get { return intTaxType; }
            set { intTaxType = value; }
        }

        public double TaxRate
        {
            get { return dblTaxRate; }
            set { dblTaxRate = value; }
        }

        public int LinkGL
        {
            get { return intLinkGL; }
            set { intLinkGL = value; }
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
            if (SaveTran != null)
            {
                SaveTran.Commit();
                SaveTran.Dispose();
                SaveCon.Close();
            }
        }

        public bool InsertTax()
        {
            try
            {
                SaveComm = new SqlCommand(" ", this.Connection);
                SaveComm.Transaction = this.SaveTran;
                // Add or Edit Tax Data //
                if (strMode == "Add")
                {
                    InsertTaxHeader(SaveComm);
                }
                else
                {
                    UpdateTaxHeader(SaveComm);
                }
                // Add Tax details after deleting existing records //
                DeleteTaxDetails(SaveComm, intID);
                if (intTaxType == 1)
                {
                    AdjustSplitGridRecords(SaveComm);
                }
                SaveComm.Dispose();
                return true;
            }
            catch
            {
                SaveComm.Dispose();
                return false;
            }
        }

        #region Insert Tax Header Data

        public bool InsertTaxHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert Into TaxHeader ( TaxType,TaxRate,TaxName,Active,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn, LinkGL, BookerID)"
                                  + " values (@TaxType,@TaxRate, @TaxName, @Active,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn , @LinkGL, @BookerID)"
                                  + " select @@IDENTITY AS ID";

                objSQlComm.CommandText = strSQLComm;
                                
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Active", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@BookerID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@TaxRate"].Value = dblTaxRate;
                objSQlComm.Parameters["@TaxType"].Value = intTaxType;
                objSQlComm.Parameters["@TaxName"].Value = strTaxName;
                objSQlComm.Parameters["@Active"].Value = strActive;
                objSQlComm.Parameters["@LinkGL"].Value = intLinkGL;
                objSQlComm.Parameters["@BookerID"].Value = strBookerID;

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
        #endregion

        #region Update Tax Header Data

        public string UpdateTaxHeader(SqlCommand objSQlComm)
        {
            objSQlComm.Parameters.Clear();

            try
            {
                string strSQLComm = " update TaxHeader set TaxType=@TaxType,TaxRate=@TaxRate,TaxName=@TaxName,LinkGL=@LinkGL, "
                                  + " Active=@Active,LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn where ID = @ID ";

                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxRate", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxType", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@TaxName", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Active", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@TaxRate"].Value = dblTaxRate;
                objSQlComm.Parameters["@TaxType"].Value = intTaxType;
                objSQlComm.Parameters["@TaxName"].Value = strTaxName;
                objSQlComm.Parameters["@Active"].Value = strActive;
                objSQlComm.Parameters["@LinkGL"].Value = intLinkGL;
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

        #region Fetch Tax Header data

        public DataTable FetchTaxHeader()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,TaxType,TaxRate,TaxName,Active From TaxHeader ";
                               

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Active", System.Type.GetType("System.String"));
                
                string strType = "";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["TaxType"].ToString() == "0")
                    {
                        strType = "Percent";
                    }
                    else
                    {
                        strType = "Table";
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   strType,
                                                   Functions.fnDouble(objSQLReader["TaxRate"].ToString()),
                                                   objSQLReader["TaxName"].ToString(),
                                                   objSQLReader["Active"].ToString()});
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
        
        #region Fetch Active Tax Header data

        public DataTable FetchActiveTax()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,TaxType,TaxRate,TaxName From TaxHeader where Active='Yes' ";


            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                

                string strType = "";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["TaxType"].ToString() == "0")
                    {
                        strType = "Percent";
                    }
                    else
                    {
                        strType = "Table";
                    }
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                   strType,
                                                   Functions.fnDouble(objSQLReader["TaxRate"].ToString()),
                                                   objSQLReader["TaxName"].ToString()
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

        private void AdjustSplitGridRecords(SqlCommand objSQlComm)
        {
            try
            {
                if (dblSplitDataTable == null) return;
                foreach (DataRow dr in dblSplitDataTable.Rows)
                {
                    string colTax = "";
                    string colBreakpoints = "";
                   
                    if (dr["Breakpoints"].ToString() == "") continue;
                    if (dr["Breakpoints"].ToString() != "")
                    {
                       if ( Functions.fnDouble(dr["Breakpoints"].ToString()) == 0) continue;
                    }
                    if (dr["Breakpoints"].ToString() != "")
                    {
                        colBreakpoints = dr["Breakpoints"].ToString();
                    }
                    else
                    {
                        colBreakpoints = "0.00";
                    }

                    if (dr["Tax"].ToString() != "")
                    {
                        colTax = dr["Tax"].ToString();
                    }
                    else
                    {
                        colTax = "0.00";
                    }
                    dblTax = Functions.fnDouble(colTax);
                    dblBreakpoints = Functions.fnDouble(colBreakpoints);
                    InsertTaxDetails(objSQlComm, intID);

                }
                dblSplitDataTable.Dispose();
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message;
            }
        }

        public bool DeleteTaxDetails(SqlCommand objSQlComm, int intRefID)
        {
            objSQlComm.Parameters.Clear();
            try
            {
                string strSQLComm = "Delete from TaxDetail Where RefID = @ID";

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

        public bool InsertTaxDetails(SqlCommand objSQlComm, int intRefID)
        {
            int intTaxDetailID = 0;
            objSQlComm.Parameters.Clear();
            SqlDataReader sqlDataReader = null;
            try
            {
                string strSQLComm = " insert into TaxDetail (RefID, Breakpoints,Tax,CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                                  + " values (@RefID,@Breakpoints,@Tax,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                                  + " select @@IDENTITY AS ID ";

                objSQlComm.CommandText = strSQLComm;


                objSQlComm.Parameters.Add(new SqlParameter("@RefID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Breakpoints", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@Tax", System.Data.SqlDbType.Decimal));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@RefID"].Value = intRefID;
                objSQlComm.Parameters["@Breakpoints"].Value = dblBreakpoints;
                objSQlComm.Parameters["@Tax"].Value = dblTax;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                sqlDataReader = objSQlComm.ExecuteReader();

                if (sqlDataReader.Read()) intTaxDetailID = Functions.fnInt32(sqlDataReader["ID"]);

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


        #region Show Header Record based on ID

        public DataTable ShowHeaderRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select * from TaxHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxRate", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Active", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkGL", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
												objSQLReader["TaxType"].ToString(),
                                                objSQLReader["TaxRate"].ToString(),
                                                objSQLReader["TaxName"].ToString(),
                                                objSQLReader["Active"].ToString(),
                                                objSQLReader["LinkGL"].ToString()});
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

            string strSQLComm = " select * from TaxDetail where RefID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                
                dtbl.Columns.Add("BreakPoints", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));

                double dblbp; double dblTax;
                while (objSQLReader.Read())
                {
                    dblbp = 0.00; dblTax = 0.00;
                    if (objSQLReader["BreakPoints"].ToString() != "")
                        dblbp = Functions.fnDouble(objSQLReader["BreakPoints"].ToString());
                    if (objSQLReader["Tax"].ToString() != "")
                        dblTax = Functions.fnDouble(objSQLReader["Tax"].ToString());

                    dtbl.Rows.Add(new object[] { dblbp, dblTax });
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

        public bool IsActiveDTax(int TaxID)
        {
            string val = "";
            string strSQLComm = "select Active from TaxHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = TaxID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                double dblRate = 0;
                while (objSQLReader.Read())
                {
                    val = objSQLReader["Active"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return val == "Yes";
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

        public int GetDTaxType(int TaxID)
        {
            int val = 0;
            string strSQLComm = "select TaxType from TaxHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = TaxID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                double dblRate = 0;
                while (objSQLReader.Read())
                {
                    val = Functions.fnInt32(objSQLReader["TaxType"].ToString());
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

        public void GetDTaxDetails(int pID,ref string TxName, ref double TxVal, ref int TxType)
        {
            string strSQLComm = "select ID, TaxName,TaxRate,TaxType from TaxHeader where ID = @ID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;
                objSQLReader = objSQlComm.ExecuteReader();

                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }

                    strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("n") + "%)";
                    TxName = strTaxName;
                    TxVal = dblRate;
                    TxType = Functions.fnInt32(objSQLReader["TaxType"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();
            }
        }

        #region Show Taxes in a Combo

        public DataTable FetchDestinationTaxCombo()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select ID, TaxName, TaxRate from TaxHeader";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));


                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }

                    strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("n") + "%)";
                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                strTaxName});

                }

                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_None });

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

        public DataTable ShowTaxCombo()
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "select ID, TaxName, TaxRate from TaxHeader";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();


                dtbl.Columns.Add("TaxID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("TaxName", System.Type.GetType("System.String"));


                double dblRate = 0;
                string strTaxName = "";
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }

                    strTaxName = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("n") + "%)";
                    dtbl.Rows.Add(new object[] {
												objSQLReader["ID"].ToString(),
                                                strTaxName});

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

        public string GetTaxDesc(int TaxID)
        {
            string strresult = "";
            string strSQLComm = "select TaxName, TaxRate from TaxHeader where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = TaxID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                double dblRate = 0;
                while (objSQLReader.Read())
                {

                    dblRate = 0.00;
                    if (objSQLReader["TaxRate"].ToString() != "")
                    {
                        dblRate = Functions.fnDouble(objSQLReader["TaxRate"].ToString());
                    }
                    if (intDecimalPlace == 3) strresult = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f3") + "%)";
                    else strresult = objSQLReader["TaxName"].ToString() + " (" + dblRate.ToString("f") + "%)";
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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
            string strSQLComm = " Delete from TaxHeader Where ID = @ID";

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

        #region Delete Detail

        public string DeleteDetail(int DeleteID)
        {
            string strSQLComm = " Delete from TaxDetail Where REFID = @ID";

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

        #region Delete Mapping

        public string DeleteMapping(int DeleteID,string mType)
        {
            string strSQLComm = " Delete from TaxMapping Where MappingID = @ID and MappingType = @MTYPE ";

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
                objSQlComm.Parameters.Add(new SqlParameter("@MTYPE", System.Data.SqlDbType.VarChar));
                objSQlComm.Parameters["@MTYPE"].Value = mType;
                
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

        public int GetNoOfActiveTaxes(int pID)
        {
            int strresult = 0;
            string strSQLComm = " select count(*) as RecCount from TaxHeader where Active = 'Yes' and ID <> @ID  ";

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
                    strresult = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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

        public int GetNoOfActiveTaxes()
        {
            int strresult = 0;
            string strSQLComm = " select count(*) as RecCount from TaxHeader where Active = 'Yes' ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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

        public int IsExistsTaxName(string pval)
        {
            int strresult = 0;

            string strSQLComm = " select count(*) as RecCount from TaxHeader where upper(TaxName) = @val  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@val", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@val"].Value = pval.ToUpper();
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = Functions.fnInt32(objSQLReader["RecCount"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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

        public int GetTaxType(int ptxid)
        {
            int strresult = 0;
            string strSQLComm = " select TaxType from TaxHeader where ID = @val  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@val", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@val"].Value = ptxid;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = Functions.fnInt32(objSQLReader["TaxType"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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

        public string GetTaxName(int ptxid)
        {
            string strresult = "";
            string strSQLComm = " select TaxName from TaxHeader where ID = @val  ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);

            objSQlComm.Parameters.Add(new SqlParameter("@val", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@val"].Value = ptxid;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                while (objSQLReader.Read())
                {
                    strresult = objSQLReader["TaxName"].ToString();
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return strresult;
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


    }
}
