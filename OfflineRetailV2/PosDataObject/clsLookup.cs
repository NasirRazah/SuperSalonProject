/*
 purpose : Data for Lookups
*/

using System;
using System.Data.SqlClient;
using System.Data;

namespace PosDataObject
{
    public class Lookup
    {
        private SqlConnection sqlConn;
        public Lookup()
        {
        }
        public SqlConnection Connection
        {
            get { return sqlConn; }
            set { sqlConn = value; }
        }

        public DataTable FetchData(string strType, int intCategory, string strSearchText,string thisstore)
        {
            DataTable dtbl = new DataTable();
            string strSQLComm = "";
            if (strType == "Customer")
            {
                if (intCategory == 0)
                {
                    strSQLComm = " Select ID, CustomerID as FLDVAL1, LastName + ', ' + FirstName as "
                         + " FLDVAL2, Company as FLDVAL3, Address1 as FLDVAL4 from Customer "
                         + " where CustomerID like " + "'%" + strSearchText + "%' and issuestore = '" + thisstore + "' "
                         + " order by CustomerID ,LastName ";
                }
                if (intCategory == 1)
                {
                    strSQLComm = " Select ID, CustomerID as FLDVAL1, LastName + ', ' + FirstName as "
                         + " FLDVAL2, Company as FLDVAL3, Address1 as FLDVAL4 from Customer "
                         + " where FirstName like " + "'%" + strSearchText + "%' and issuestore = '" + thisstore + "' "
                         + " order by FirstName ";
                }
                if (intCategory == 2)
                {
                    strSQLComm = " Select ID, CustomerID as FLDVAL1, LastName + ', ' + FirstName as "
                         + " FLDVAL2, Company as FLDVAL3, Address1 as FLDVAL4 from Customer "
                         + " where LastName like " + "'%" + strSearchText + "%' and issuestore = '" + thisstore + "' "
                         + " order by LastName ";
                }
                if (intCategory == 3)
                {
                    strSQLComm = " Select ID, CustomerID as FLDVAL1, LastName + ', ' + FirstName as "
                         + " FLDVAL2, Company as FLDVAL3, Address1 as FLDVAL4 from Customer "
                         + " where Company like " + "'%" + strSearchText + "%' and issuestore = '" + thisstore + "' "
                         + " order by Company ";
                }

            }
            if (strType == "Employee")
            {
                if (intCategory == 0)
                {
                    strSQLComm = " Select a.ID, a.EmployeeID as FLDVAL1, a.LastName + ', ' + a.FirstName as "
                         + " FLDVAL2, a.Address1 as FLDVAL3, b.GroupName as FLDVAL4 from Employee a "
                         + " left outer join SecurityGroup b on a.ProfileID = b.ID "
                         + " where a.EmployeeID like " + "'%" + strSearchText + "%' "
                         + " order by a.EmployeeID ,a.LastName ";
                }
                if (intCategory == 1)
                {
                    strSQLComm = " Select a.ID, a.EmployeeID as FLDVAL1, a.LastName + ', ' + a.FirstName as "
                          + " FLDVAL2, a.Address1 as FLDVAL3, b.GroupName as FLDVAL4 from Employee a "
                          + " left outer join SecurityGroup b on a.ProfileID = b.ID "
                          + " where a.FirstName like " + "'%" + strSearchText + "%' "
                          + " order by a.FirstName  ";
                }
                if (intCategory == 2)
                {
                    strSQLComm = " Select a.ID, a.EmployeeID as FLDVAL1, a.LastName + ', ' + a.FirstName as "
                         + " FLDVAL2, a.Address1 as FLDVAL3, b.GroupName as FLDVAL4 from Employee a "
                         + " left outer join SecurityGroup b on a.ProfileID = b.ID "
                         + " where a.LastName like " + "'%" + strSearchText + "%' "
                         + " order by a.LastName ";
                }
            }

            if (strType == "Product")
            {
                if (intCategory == 0)
                {
                    strSQLComm = " Select a.ID, a.SKU as FLDVAL1, a.Description as FLDVAL2, "
                         + "  b.Description as FLDVAL3, a.ProductType as FLDVAL4 from Product a "
                         + " left outer join dept b on a.DepartmentID = b.ID "
                         + " where a.SKU like " + "'%" + strSearchText + "%' "
                         + " order by a.SKU, a.ProductType ";
                }
                if (intCategory == 1)
                {
                    strSQLComm = " Select a.ID, a.SKU as FLDVAL1, a.Description as FLDVAL2, "
                         + "  b.Description as FLDVAL3, a.ProductType as FLDVAL4 from Product a "
                         + " left outer join dept b on a.DepartmentID = b.ID "
                         + " where a.Description like " + "'%" + strSearchText + "%' "
                         + " order by a.Description, a.ProductType ";
                }
            }

            if (strType == "Product (For Break Pack)")
            {
                if (intCategory == 0)
                {
                    strSQLComm = " Select a.ID, a.SKU as FLDVAL1, a.Description as FLDVAL2, "
                         + "  b.Description as FLDVAL3, a.ProductType as FLDVAL4 from Product a "
                         + " left outer join dept b on a.DepartmentID = b.ID "
                         + " where a.SKU like " + "'%" + strSearchText + "%' "
                         + " and a.LinkSKU <= 0 and a.producttype = 'P' order by a.SKU, a.ProductType ";
                }
                if (intCategory == 1)
                {
                    strSQLComm = " Select a.ID, a.SKU as FLDVAL1, a.Description as FLDVAL2, "
                         + "  b.Description as FLDVAL3, a.ProductType as FLDVAL4 from Product a "
                         + " left outer join dept b on a.DepartmentID = b.ID "
                         + " where a.Description like " + "'%" + strSearchText + "%' "
                         + " and a.LinkSKU <= 0 and a.producttype = 'P' order by a.Description,a.ProductType ";
                }
            }

            if (strType == "Vendor")
            {
                if (intCategory == 0)
                {
                    strSQLComm = " Select ID, VendorID as FLDVAL1, Name  as "
                         + " FLDVAL2, Address1 as FLDVAL3, Contact as FLDVAL4 from Vendor "
                         + " where VendorID like " + "'%" + strSearchText + "%' "
                         + " order by VendorID ,Name ";
                }
                if (intCategory == 1)
                {
                    strSQLComm = " Select ID, VendorID as FLDVAL1, Name  as "
                         + " FLDVAL2, Address1 as FLDVAL3, Contact as FLDVAL4 from Vendor "
                         + " where Name like " + "'%" + strSearchText + "%' "
                         + " order by Name ";
                }
                if (intCategory == 2)
                {
                    strSQLComm = " Select ID, VendorID as FLDVAL1, Name  as "
                         + " FLDVAL2, Address1 as FLDVAL3, Contact as FLDVAL4 from Vendor "
                         + " where Contact like " + "'%" + strSearchText + "%' "
                         + " order by Contact,Name ";
                }
                if (intCategory == 3)
                {
                    strSQLComm = " Select ID, VendorID as FLDVAL1, Name  as "
                         + " FLDVAL2, Address1 as FLDVAL3, Contact as FLDVAL4 from Vendor "
                         + " where Address1 like " + "'%" + strSearchText + "%' "
                         + " order by Address1,Name ";
                }
            }

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlConn);
            SqlDataReader objSQLReader = null;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();

                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FLDVAL1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FLDVAL2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FLDVAL3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("FLDVAL4", System.Type.GetType("System.String"));
                while (objSQLReader.Read())
                {
                    dtbl.Rows.Add(new object[] {objSQLReader["ID"].ToString(),
                                                objSQLReader["FLDVAL1"].ToString(),
                                                objSQLReader["FLDVAL2"].ToString(),
                                                objSQLReader["FLDVAL3"].ToString(),
                                                objSQLReader["FLDVAL4"].ToString()});
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
