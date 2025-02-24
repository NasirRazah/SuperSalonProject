/*
 purpose : Data for Matrix Product
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PosDataObject
{
    public class Matrix
    {
        private SqlConnection sqlConn;
        public SqlTransaction SaveTran;
        public SqlCommand SaveComm;
        public SqlConnection SaveCon;

        private int intID;
        private int intMatrixOptionID;
        private int intProductID;
        private string strOption1Name;
        private string strOption2Name;
        private string strOption3Name;
        private int intLoginUserID;
        private int intOptionValueID;
        private DateTime dtCreatedOn;
        private DateTime dtLastChangedOn;
        private DataTable dtblMatrixValues1;
        private DataTable dtblMatrixValues2;
        private DataTable dtblMatrixValues3;
        private DataTable dtblMatrixData;

    
        
        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int MatrixOptionID
        {
            get { return intMatrixOptionID; }
            set { intMatrixOptionID = value; }
        }

        public int ProductID
        {
            get { return intProductID; }
            set { intProductID = value; }
        }

        public string Option1Name
        {
            get { return strOption1Name; }
            set { strOption1Name = value; }
        }

        public string Option2Name
        {
            get { return strOption2Name; }
            set { strOption2Name = value; }
        }

        public string Option3Name
        {
            get { return strOption3Name; }
            set { strOption3Name = value; }
        }

        public DateTime CreatedOn
        {
            get { return dtCreatedOn; }
            set { dtCreatedOn = value; }
        }

        public DateTime LastChangedOn
        {
            get { return dtLastChangedOn; }
            set { dtLastChangedOn = value; }
        }

        public DataTable MatrixValues1
        {
            get { return dtblMatrixValues1; }
            set { dtblMatrixValues1 = value; }
        }

        public DataTable MatrixValues2
        {
            get { return dtblMatrixValues2; }
            set { dtblMatrixValues2 = value; }
        }

        public DataTable MatrixValues3
        {
            get { return dtblMatrixValues3; }
            set { dtblMatrixValues3 = value; }
        }

        public DataTable MatrixData
        {
            get { return dtblMatrixData; }
            set { dtblMatrixData = value; }
        }

        public int LoginUserID
        {
            get { return intLoginUserID; }
            set { intLoginUserID = value; }
        }
        
        public int OptionValueID
        {
            get { return intOptionValueID; }
            set { intOptionValueID = value; }
        }

        public DataTable FetchData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * From MatrixOptions where ProductID = @ProductID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ProductID"].Value = intProductID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("Option1Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option2Name", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Option3Name", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    int intMatID = 0;

                    if (objSQLReader["ID"].ToString() != "")
                        intMatID = Functions.fnInt32(objSQLReader["ID"].ToString());

                    dtbl.Rows.Add(new object[] {   intMatID,
                                                   objSQLReader["Option1Name"].ToString(),
                                                   objSQLReader["Option2Name"].ToString(),
                                                   objSQLReader["Option3Name"].ToString()});
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

        public DataTable FetchOptionValues()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, OptionValue, OptionDefault From MatrixValues "
                              + " where MatrixOptionID = @MatrixOptionID and ValueID = @ValueID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@MatrixOptionID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@MatrixOptionID"].Value = intMatrixOptionID;
            objSQlComm.Parameters.Add(new SqlParameter("@ValueID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ValueID"].Value = intOptionValueID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionDefault", System.Type.GetType("System.Boolean"));

                bool lbDefault = false;
                while (objSQLReader.Read())
                {

                    if (objSQLReader["OptionDefault"].ToString() == "Y")
                    {
                        lbDefault = true;
                    }
                    else
                    {
                        lbDefault = false;
                    }

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
                                                objSQLReader["OptionValue"].ToString(),
                                                lbDefault});
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

        public DataTable FetchMatrixData(string CalledFrom)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select MatrixOptionID,OptionValue1,OptionValue2,OptionValue3,QtyonHand "
                              + " From Matrix where MatrixOptionID = @MatrixOptionID ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@MatrixOptionID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@MatrixOptionID"].Value = intMatrixOptionID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("MatrixOptionID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyonHand", System.Type.GetType("System.Double"));

                
                double dblQty = 0;
                while (objSQLReader.Read())
                {
                    dblQty = 0;
                    if (CalledFrom == "Product")
                    {
                        if (objSQLReader["QtyonHand"].ToString() != "")
                        {
                            dblQty = Functions.fnDouble(objSQLReader["QtyonHand"].ToString());
                            
                        }
                    }

                    dtbl.Rows.Add(new object[] {
                                                objSQLReader["MatrixOptionID"].ToString(),
                                                objSQLReader["OptionValue1"].ToString(),
                                                objSQLReader["OptionValue2"].ToString(),
                                                objSQLReader["OptionValue3"].ToString(),
                                                dblQty});
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

        public string InsertData(SqlCommand objSQlComm)
        {
            string strSQLComm = "";
            strSQLComm = " insert into MatrixOptions( ProductID,Option1Name,Option2Name,Option3Name, "
                       + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn) "
                       + " values (@ProductID, @Option1Name, @Option2Name, @Option3Name, "
                       + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn) "
                       + " select @@IDENTITY as ID ";

            objSQlComm.Parameters.Clear();
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@Option1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Option2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Option3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@ProductID"].Value = intProductID;
                objSQlComm.Parameters["@Option1Name"].Value = strOption1Name;
                objSQlComm.Parameters["@Option2Name"].Value = strOption2Name;
                objSQlComm.Parameters["@Option3Name"].Value = strOption3Name;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objsqlReader = objSQlComm.ExecuteReader();

                if (objsqlReader.Read())
                {
                    intID = Functions.fnInt32(objsqlReader["ID"]);
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

        public string UpdateData(SqlCommand objSQlComm)
        {
            string strSQLComm = "";
            strSQLComm = " update MatrixOptions set Option1Name = @Option1Name, Option2Name = @Option2Name,Option3Name = @Option3Name, "
                       + " LastChangedBy = @LastChangedBy, LastChangedOn = @LastChangedOn Where ID = @ID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@Option1Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Option2Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Option3Name", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@Option1Name"].Value = strOption1Name;
                objSQlComm.Parameters["@Option2Name"].Value = strOption2Name;
                objSQlComm.Parameters["@Option3Name"].Value = strOption3Name;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
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


        public string DeleteMatrixOptionValues(SqlCommand objSQlComm, int MatrixOptID)
        {
            string strSQLComm = "";

            strSQLComm = " Delete from MatrixValues where MatrixoptionID = @MatrixoptionID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MatrixoptionID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MatrixoptionID"].Value = MatrixOptID;

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

        public string DeleteMatrixDataValues(SqlCommand objSQlComm, int MatrixOptID)
        {
            string strSQLComm = "";

            strSQLComm = " Delete from Matrix where MatrixoptionID = @MatrixoptionID";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MatrixoptionID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@MatrixoptionID"].Value = MatrixOptID;

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


        public string ProcessMatrixValues(DataTable MatrixValues, int ValueSerialNo, SqlCommand objSQlComm)
        {
            if (MatrixValues == null) return "";
            int intRowID = 0;
            string strOptionValue = "";
            string strOptionDefault = "";

            string strError = "";

            foreach (DataRow dr in MatrixValues.Rows)
            {
                intRowID = 0;
                if (dr["ID"].ToString() != "")
                {
                    intRowID = Functions.fnInt32(dr["ID"].ToString());
                }
                strOptionValue = dr["OptionValue"].ToString();

                if (dr["OptionDefault"].ToString().ToUpper() == "TRUE")
                {
                    strOptionDefault = "Y";
                }
                else
                {
                    strOptionDefault = "N";
                }

                InsertMatrixOptions(objSQlComm, ValueSerialNo, strOptionValue, strOptionDefault);
                /*

                if (intRowID == 0)
                {
                    strError = InsertMatrixOptions(objSQlComm, ValueSerialNo, strOptionValue, strOptionDefault);
                    
                }
                else
                {
                    strError = UpdateMatrixOptions(objSQlComm, intRowID, strOptionValue, strOptionDefault);

                }
                  */
                if (strError != "") break;
            }
            return strError;
        }

        public string InsertMatrixOptions(SqlCommand objSQlComm, int MatValue, string MatOptionValue, string MastOptionDefault)
        {
            string strSQLComm = "";
            strSQLComm = " insert into MatrixValues( MatrixOptionID,ValueID,OptionValue,OptionDefault,"
                       + " CreatedBy,CreatedOn,LastChangedBy,LastChangedOn ) "
                       + " values (@MatrixOptionID, @ValueID, @OptionValue, @OptionDefault, "
                       + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MatrixOptionID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@ValueID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@MatrixOptionID"].Value = intID;
                objSQlComm.Parameters["@ValueID"].Value = MatValue;
                objSQlComm.Parameters["@OptionValue"].Value = MatOptionValue;
                objSQlComm.Parameters["@OptionDefault"].Value = MastOptionDefault;
                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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

        public string UpdateMatrixOptions(SqlCommand objSQlComm, int MatID, string MatOptionValue, string MastOptionDefault)
        {
            string strSQLComm = "";
            strSQLComm = " Update MatrixValues Set OptionValue = @OptionValue, OptionDefault = @OptionDefault, "
                       + " LastChangedBy = @LastChangedBy, LastChangedOn = @LastChangedOn Where ID = @ID ";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionDefault", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@OptionValue"].Value = MatOptionValue;
                objSQlComm.Parameters["@OptionDefault"].Value = MastOptionDefault;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@ID"].Value = MatID;

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


        public string ProcessMatrixData(SqlCommand objSQlComm)
        {
            if (dtblMatrixData == null) return "";
            string strOptionValue1 = "";
            string strOptionValue2 = "";
            string strOptionValue3 = "";
            double dbltQty = 0.00;

            string strError = "";

            foreach (DataRow dr in dtblMatrixData.Rows)
            {
                double intQty = 0;
                if (dr["QtyonHand"].ToString() != "")
                {
                    intQty = Functions.fnDouble(dr["QtyonHand"].ToString());
                }

                strOptionValue1 = dr["OptionValue1"].ToString();
                strOptionValue2 = dr["OptionValue2"].ToString();
                strOptionValue3 = dr["OptionValue3"].ToString();

                InsertMatrixData(objSQlComm, strOptionValue1, strOptionValue2, strOptionValue3, intQty);

                if (strError != "") break;
            }
            return strError;
        }

        public string InsertMatrixData(SqlCommand objSQlComm, string MatOptionValue1, string MatOptionValue2,
                                        string MatOptionValue3, double QtyonHand )
        {
            string strSQLComm = "";
            strSQLComm = " insert into Matrix( MatrixOptionID, OptionValue1, OptionValue2, OptionValue3, QtyonHand, "
                       + " CreatedBy, CreatedOn, LastChangedBy, LastChangedOn) "
                       + " values (@MatrixOptionID, @OptionValue1, @OptionValue2, @OptionValue3, @QtyonHand, "
                       + " @CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn)";

            objSQlComm.Parameters.Clear();
            try
            {
                objSQlComm.CommandText = strSQLComm;

                objSQlComm.Parameters.Add(new SqlParameter("@MatrixOptionID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue1", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue2", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@OptionValue3", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@QtyonHand", System.Data.SqlDbType.Float));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters["@MatrixOptionID"].Value = intID;
                objSQlComm.Parameters["@OptionValue1"].Value = MatOptionValue1;
                objSQlComm.Parameters["@OptionValue2"].Value = MatOptionValue2;
                objSQlComm.Parameters["@OptionValue3"].Value = MatOptionValue3;
                objSQlComm.Parameters["@QtyonHand"].Value = QtyonHand;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
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
                    strError = InsertData(SaveComm);
                }
                else
                {
                    strError = UpdateData(SaveComm);
                    if (strError == "")
                        strError = DeleteMatrixOptionValues(SaveComm, intID);
                    if (strError == "")
                        strError = DeleteMatrixDataValues(SaveComm, intID);
                }

                if (strError == "")
                    strError = ProcessMatrixValues(dtblMatrixValues1, 1, SaveComm);

                if (strError == "")
                    strError = ProcessMatrixValues(dtblMatrixValues2, 2, SaveComm);

                if (strError == "")
                    strError = ProcessMatrixValues(dtblMatrixValues3, 3, SaveComm);

                if (strError == "")
                    strError = ProcessMatrixData(SaveComm);

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

        public int FetchMatrixOptionID(int PID)
        {
            int RMOID = 0;

            string strSQLComm = "select isnull(ID,'0') as ID from MatrixOptions where ProductID = @ProductID";

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
                    RMOID = Functions.fnInt32(objSQLReader["ID"].ToString());
                }

                objSQLReader.Close();
                objSQLReader.Dispose();
                objSQlComm.Dispose();
                sqlConn.Close();

                return RMOID;
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

    }
}
