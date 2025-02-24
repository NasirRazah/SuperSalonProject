/*
 purpose : Data for Item Department
 */

using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PosDataObject
{
    public class Department
    {
        #region definining private variables

        private SqlConnection sqlConn;
        private string strDataObjectCulture_All;
        private string strDataObjectCulture_None;
        private string strDataObjectCulture_AllDepartments;
        private int intID;
        private int intNewID;
        private int intLoginUserID;

        private string strGeneralCode;
        private string strGeneralDesc;
        private string strFoodStamp;
        private string strScaleFlag;

        private string strScaleScreenVisible;

        private int intScaleDisplayOrder;

        private int intLinkSalesGL;
        private int intLinkCostGL;
        private int intLinkPayableGL;
        private int intLinkInventoryGL;
        private string strBookerDeptID = "";


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

        public string DataObjectCulture_AllDepartments
        {
            get { return strDataObjectCulture_AllDepartments; }
            set { strDataObjectCulture_AllDepartments = value; }
        }

        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public int ScaleDisplayOrder
        {
            get { return intScaleDisplayOrder; }
            set { intScaleDisplayOrder = value; }
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


        public int LinkSalesGL
        {
            get { return intLinkSalesGL; }
            set { intLinkSalesGL = value; }
        }

        public int LinkCostGL
        {
            get { return intLinkCostGL; }
            set { intLinkCostGL = value; }
        }

        public int LinkPayableGL
        {
            get { return intLinkPayableGL; }
            set { intLinkPayableGL = value; }
        }

        public int LinkInventoryGL
        {
            get { return intLinkInventoryGL; }
            set { intLinkInventoryGL = value; }
        }

        public string BookerDeptID
        {
            get { return strBookerDeptID; }
            set { strBookerDeptID = value; }
        }

        public string GeneralCode
        {
            get { return strGeneralCode; }
            set { strGeneralCode = value; }
        }

        public string GeneralDesc
        {
            get { return strGeneralDesc; }
            set { strGeneralDesc = value; }
        }

        public string FoodStamp
        {
            get { return strFoodStamp; }
            set { strFoodStamp = value; }
        }

        public string ScaleFlag
        {
            get { return strScaleFlag; }
            set { strScaleFlag = value; }
        }

        public string ScaleScreenVisible
        {
            get { return strScaleScreenVisible; }
            set { strScaleScreenVisible = value; }
        }

        #endregion

        #region Insert Data
        public string InsertData()
        {
            string strSQLComm =   " insert into Dept( DepartmentID,Description,FoodStampEligible,ScaleFlag,ScaleScreenVisible,ScaleDisplayOrder,"
                                + " LinkSalesGL,LinkCostGL,LinkPayableGL,LinkInventoryGL, CreatedBy, CreatedOn, LastChangedBy, LastChangedOn, BookerDeptID) "
                                + " values ( @DepartmentID,@Description, @FoodStampEligible,@ScaleFlag,@ScaleScreenVisible,@ScaleDisplayOrder,"
                                + " @LinkSalesGL,@LinkCostGL,@LinkPayableGL,@LinkInventoryGL,@CreatedBy, @CreatedOn, @LastChangedBy, @LastChangedOn, @BookerDeptID) "
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

                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenVisible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDisplayOrder", System.Data.SqlDbType.Int));

                objSQlComm.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@CreatedOn", System.Data.SqlDbType.DateTime));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSalesGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkCostGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkPayableGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkInventoryGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@BookerDeptID", System.Data.SqlDbType.VarChar));

                objSQlComm.Parameters["@DepartmentID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStamp;
                objSQlComm.Parameters["@ScaleFlag"].Value = strScaleFlag;
                objSQlComm.Parameters["@ScaleScreenVisible"].Value = strScaleScreenVisible;
                objSQlComm.Parameters["@ScaleDisplayOrder"].Value = intScaleDisplayOrder;
                objSQlComm.Parameters["@BookerDeptID"].Value = strBookerDeptID;

                objSQlComm.Parameters["@CreatedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@CreatedOn"].Value = DateTime.Now;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@LinkSalesGL"].Value = intLinkSalesGL;
                objSQlComm.Parameters["@LinkCostGL"].Value = intLinkCostGL;
                objSQlComm.Parameters["@LinkPayableGL"].Value = intLinkPayableGL;
                objSQlComm.Parameters["@LinkInventoryGL"].Value = intLinkInventoryGL;

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
            string strSQLComm = " update Dept set DepartmentID=@DepartmentID,Description=@Description,FoodStampEligible=@FoodStampEligible,ScaleDisplayOrder=@ScaleDisplayOrder, "
                              + " ScaleFlag=@ScaleFlag,ScaleScreenVisible=@ScaleScreenVisible, LastChangedBy=@LastChangedBy, LastChangedOn=@LastChangedOn, "
                              + " LinkSalesGL=@LinkSalesGL,LinkCostGL=@LinkCostGL,LinkPayableGL=@LinkPayableGL,LinkInventoryGL=@LinkInventoryGL where ID = @ID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@DepartmentID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@Description", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters.Add(new SqlParameter("@FoodStampEligible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleFlag", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleScreenVisible", System.Data.SqlDbType.Char));
                objSQlComm.Parameters.Add(new SqlParameter("@ScaleDisplayOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedBy", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LastChangedOn", System.Data.SqlDbType.DateTime));

                objSQlComm.Parameters.Add(new SqlParameter("@LinkSalesGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkCostGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkPayableGL", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@LinkInventoryGL", System.Data.SqlDbType.Int));

                objSQlComm.Parameters["@ID"].Value = intID;
                objSQlComm.Parameters["@DepartmentID"].Value = strGeneralCode;
                objSQlComm.Parameters["@Description"].Value = strGeneralDesc;
                objSQlComm.Parameters["@FoodStampEligible"].Value = strFoodStamp;
                objSQlComm.Parameters["@ScaleFlag"].Value = strScaleFlag;
                objSQlComm.Parameters["@ScaleScreenVisible"].Value = strScaleScreenVisible;
                objSQlComm.Parameters["@ScaleDisplayOrder"].Value = intScaleDisplayOrder;
                objSQlComm.Parameters["@LastChangedBy"].Value = intLoginUserID;
                objSQlComm.Parameters["@LastChangedOn"].Value = DateTime.Now;

                objSQlComm.Parameters["@LinkSalesGL"].Value = intLinkSalesGL;
                objSQlComm.Parameters["@LinkCostGL"].Value = intLinkCostGL;
                objSQlComm.Parameters["@LinkPayableGL"].Value = intLinkPayableGL;
                objSQlComm.Parameters["@LinkInventoryGL"].Value = intLinkInventoryGL;

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

            string strSQLComm = " select ID,DepartmentID,Description,FoodStampEligible,ScaleFlag,ScaleDisplayOrder from Dept order by ScaleDisplayOrder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleDisplayOrder", System.Type.GetType("System.String"));

                string strFood = "";
                string strScale = "";
                while (objSQLReader.Read())
                {
                    if (objSQLReader["FoodStampEligible"].ToString() == "Y")
                    {
                        strFood = "Yes";
                    }
                    else
                    {
                        strFood = "No";
                    }

                    strScale = objSQLReader["ScaleFlag"].ToString() == "Y" ? "Yes" : "No";

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["DepartmentID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   strFood,strScale,objSQLReader["ScaleDisplayOrder"].ToString()});
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

        #region Show Record based on ID

        public DataTable ShowRecord(int intRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select * from Dept where ID = @ID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@ID"].Value = intRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FoodStampEligible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleFlag", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleScreenVisible", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleDisplayOrder", System.Type.GetType("System.String"));

                dtbl.Columns.Add("LinkSalesGL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkCostGL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkPayableGL", System.Type.GetType("System.String"));
                dtbl.Columns.Add("LinkInventoryGL", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["DepartmentID"].ToString(),
                                                objSQLReader["Description"].ToString(),
                                                objSQLReader["FoodStampEligible"].ToString(),
                                                objSQLReader["ScaleFlag"].ToString(),
                                                objSQLReader["ScaleScreenVisible"].ToString(),
                                                objSQLReader["ScaleDisplayOrder"].ToString(),
                                                objSQLReader["LinkSalesGL"].ToString(),
                                                objSQLReader["LinkCostGL"].ToString(),
                                                objSQLReader["LinkPayableGL"].ToString(),
                                                objSQLReader["LinkInventoryGL"].ToString()});
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
        public DataTable ShowBookerRecord(string intBookerRecID)
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = "select top 1 ID from Dept where BookerDeptID = @BookerDeptID";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            objSQlComm.Parameters.Add(new SqlParameter("@BookerDeptID", System.Data.SqlDbType.VarChar));
            objSQlComm.Parameters["@BookerDeptID"].Value = intBookerRecID;
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString()});
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

        #region Duplicate Checking

        public int DuplicateCount(string DeptID)
        {
            int intCount = 0;
            string strSQLComm = " select count(*) as rcnt from dept where DEPARTMENTID = @DEPARTMENTID ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;
            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

                objSQlComm.Parameters.Add(new SqlParameter("@DEPARTMENTID", System.Data.SqlDbType.NVarChar));
                objSQlComm.Parameters["@DEPARTMENTID"].Value = DeptID;

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
        #endregion 

        #region Fetch Lookup Data

        public DataTable FetchLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, DepartmentID, Description from Dept ";
                                
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["DepartmentID"].ToString(),
												   objSQLReader["Description"].ToString()
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

        public DataTable FetchLookupData1()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, DepartmentID, Description from dept order by description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["DepartmentID"].ToString(),
												   objSQLReader["Description"].ToString()
                                                   });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_All, strDataObjectCulture_All });
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

        public DataTable FetchScaleLookupData()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, DepartmentID, Description from Dept where ScaleFlag='Y' order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DepartmentID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["DepartmentID"].ToString(),
												   objSQLReader["Description"].ToString()
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

        public DataTable FetchLookupData2()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, Description from dept order by description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Description"].ToString()
                                                   });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_AllDepartments, });
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

        public DataTable FetchLookupData3()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, Description from dept where ScaleFlag = 'Y' order by description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));

                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Description"].ToString()
                                                   });
                }
                dtbl.Rows.Add(new object[] { "0", strDataObjectCulture_AllDepartments });
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

        #region Delete Data

        public string DeleteRecord(int DeleteID)
        {
            string strSQLComm = " Delete from Dept Where ID = @ID";

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

        public DataTable FetchScaleDepartments()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID,Description,ScaleDisplayOrder,ScaleScreenVisible from Dept where ScaleFlag = 'Y' order by ScaleDisplayOrder ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
                dtbl.Columns.Add("AddToScale", System.Type.GetType("System.String"));
                
                
                string strScale = "";
                while (objSQLReader.Read())
                {
                    strScale = objSQLReader["ScaleScreenVisible"].ToString() == "Y" ? "Yes" : "No";

                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Description"].ToString(),
                                                   objSQLReader["ScaleDisplayOrder"].ToString(),
                                                   strScale});
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

        public int MaxScaleDisplayOrder()
        {
            int intCount = 0;

            string strSQLComm = " select max(ScaleDisplayOrder) as rcnt from dept where ScaleFlag = 'Y' ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlDataReader objsqlReader = null;

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, Connection);

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

        public int UpdateDepartmentDisplayOrder(int pID, int pOrder, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "sp_RearrangeDept_Delete";

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
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@ChangedOrder", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ChangedOrder"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@ChangedOrder"].Value = pOrder;

                objSQlComm.Parameters.Add(new SqlParameter("@Position", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@Position"].Direction = ParameterDirection.Input;
                objSQlComm.Parameters["@Position"].Value = pType;

                objSQlComm.ExecuteNonQuery();
                intReturn = 0;
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intReturn;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public int UpdateDepartmentScaleScreenVisible(int pID, string pType)
        {
            int intReturn = -1;

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            string strSQLComm = "update dept set ScaleScreenVisible = @p1 where ID = @ID ";

            try
            {
                objSQlComm = new SqlCommand(strSQLComm, sqlConn);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLTran = Connection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.CommandText = strSQLComm;
                objSQlComm.CommandType = CommandType.Text;

                objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@ID"].Value = pID;

                objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.Char));
                objSQlComm.Parameters["@p1"].Value = pType;
                
                objSQlComm.ExecuteNonQuery();
                intReturn = 0;
                objSQLTran.Commit();
                objSQlComm.Dispose();
                sqlConn.Close();
                return intReturn;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return intReturn;
            }
            finally
            {
                sqlConn.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public DataTable FetchScaleDepartmentsForMarkdown()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select ID, Description from Dept where ScaleFlag = 'Y' order by Description ";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Department", System.Type.GetType("System.String"));
               
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {   objSQLReader["ID"].ToString(),
												   objSQLReader["Description"].ToString()});
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

    }
}
