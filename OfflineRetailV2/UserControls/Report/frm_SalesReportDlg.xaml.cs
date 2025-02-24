using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_SalesReportDlg.xaml
    /// </summary>
    public partial class frm_SalesReportDlg : Window
    {
        private DataTable dtblCust;
        private DataTable dtblSelectSKU = null;
        private DataTable CSVTbl = new DataTable();


        public frm_SalesReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }


        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void populatelkup()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.DataObjectCulture_All = Settings.DataObjectCulture_All;
            objCategory.DataObjectCulture_SelectSKU = Settings.DataObjectCulture_SelectSKU;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupProduct2();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbItem.ItemsSource = dtblTemp;
            cmbItem.EditValue = "0";

            dbtblCat.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Sales Matrix Report";

            dtblSelectSKU = new DataTable();
            dtblSelectSKU.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblSelectSKU.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblSelectSKU.Columns.Add("Description", System.Type.GetType("System.String"));

            dtblSelectSKU.Columns.Add("chk", System.Type.GetType("System.Boolean"));

            PopulateSaleRentCategory();
            populatelkup();
            lbText.Visibility = Visibility.Visible;
            cmbtran.Visibility = (Settings.RentService == "Y") ? Visibility.Visible : Visibility.Collapsed;
            PopulateGroupBy();
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            lbsort.Text = "SKU";
            grd.Visibility = Visibility.Collapsed;
            chkgrd.Visibility = Visibility.Collapsed;
            rgRepType1.IsChecked = true;
            rgsort_1.IsChecked = true;
            rgsort1_1.IsChecked = true;
            dtblCust = new DataTable();
            dtblCust.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("CustomerID", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("Company", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("LastName", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("Name", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("Tot", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("Group", System.Type.GetType("System.String"));
            dtblCust.Columns.Add("Class", System.Type.GetType("System.String"));
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void GridView5_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {

        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                grd.SetCellValue(e.RowHandle, colCheck, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grd.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["Check"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grd.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkgrd.IsChecked = true;
                    chkgrd.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkgrd.IsChecked = false;
                    chkgrd.Content = "Check All";
                }
                dtbl.Dispose();
            }
        }

        private void Chkgrd_Checked(object sender, RoutedEventArgs e)
        {
            if (lkupGroupBy.EditValue.ToString() == "0") return;
            if (chkgrd.IsChecked == true)
            {
                chkgrd.Content = "Uncheck All";
                DataTable dtbl = grd.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["Check"] = true;
                }
                grd.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkgrd.Content = "Check All";
                DataTable dtbl1 = grd.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["Check"] = false;
                }
                grd.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void LkupGroupBy_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            chkgrd.IsChecked = false;
            if (lkupGroupBy.EditValue.ToString() == "0")
            {
                if (cmbtran.EditValue.ToString() == "Sales")
                {

                    //cmbItem.Enabled = true;
                    //plkup.EditValue = "0";
                }

                //btnImport.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Visible;
                //grdItem.Visibility = Visibility.Visible;
                dtblSelectSKU.Rows.Clear();
            }
            else
            {
                //btnImport.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Visible;
                //grdItem.Visibility = Visibility.Visible;
                dtblSelectSKU.Rows.Clear();
            }
            if (lkupGroupBy.EditValue.ToString() == "0")
            {
                grd.Visibility = Visibility.Collapsed;
                chkgrd.Visibility = Visibility.Collapsed;

                lbsort.Text = "SKU";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Visible;
                    lbText.Visibility = Visibility.Visible;
                }
            }
            if (lkupGroupBy.EditValue.ToString() == "1")
            {

                grd.Visibility = Visibility.Visible;
                chkgrd.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                PopulateDepartment();
                lbsort.Text = "Department";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Collapsed;
                    lbText.Visibility = Visibility.Collapsed;
                }
            }
            if (lkupGroupBy.EditValue.ToString() == "2")
            {
                grd.Visibility = Visibility.Visible;
                chkgrd.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                PopulateCategory();
                lbsort.Text = "POS Screen Category";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Collapsed;
                    lbText.Visibility = Visibility.Collapsed;
                }
            }

            if (lkupGroupBy.EditValue.ToString() == "3")
            {
                grd.Visibility = Visibility.Visible;
                chkgrd.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                PopulateFamily();
                lbsort.Text = "Family";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Collapsed;
                    lbText.Visibility = Visibility.Collapsed;
                }
            }

            if (lkupGroupBy.EditValue.ToString() == "4")
            {
                grd.Visibility = Visibility.Visible;
                chkgrd.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                PopulateEmployee();
                lbsort.Text = "Employee";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Collapsed;
                    lbText.Visibility = Visibility.Collapsed;
                }
            }
            if (lkupGroupBy.EditValue.ToString() == "5")
            {
                grd.Visibility = Visibility.Visible;
                chkgrd.Visibility = Visibility.Visible;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                PopulateVendor();
                lbsort.Text = "Vendor";
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    cmbItem.Visibility = Visibility.Collapsed;
                    lbText.Visibility = Visibility.Collapsed;
                }
            }

            bool allsku = true;
            if (lkupGroupBy.EditValue.ToString() == "0")
            {
                if (cmbtran.EditValue.ToString() == "Sales")
                {

                    if (cmbItem.EditValue.ToString() != "0")
                    {
                        allsku = false;
                    }
                }
            }
            if (((lkupGroupBy.EditValue.ToString() == "0") && allsku) || (lkupGroupBy.EditValue.ToString() == "1") || (lkupGroupBy.EditValue.ToString() == "2")
                || (lkupGroupBy.EditValue.ToString() == "3") || (lkupGroupBy.EditValue.ToString() == "4") || (lkupGroupBy.EditValue.ToString() == "5"))
            {
                grpsort.Visibility = Visibility.Visible;
                cmbsort.Visibility = Visibility.Visible;
                rgsort1_1.Visibility = Visibility.Visible;
                rgsort1_2.Visibility = Visibility.Visible;
                if ((lkupGroupBy.EditValue.ToString() == "0") || (lkupGroupBy.EditValue.ToString() == "1") || (lkupGroupBy.EditValue.ToString() == "2")
                    || (lkupGroupBy.EditValue.ToString() == "3") || (lkupGroupBy.EditValue.ToString() == "4"))
                {
                    DataTable dtblsort = new DataTable();
                    dtblsort.Columns.Add("Filter", System.Type.GetType("System.String"));
                    dtblsort.Columns.Add("FilterText", System.Type.GetType("System.String"));

                    cmbsort.ItemsSource = null;

                    if (lkupGroupBy.EditValue.ToString() != "0")
                    {
                        dtblsort.Rows.Add(new object[] { "SKU", "SKU" });
                    }

                    dtblsort.Rows.Add(new object[] { "Description", "Description" });

                    if (lkupGroupBy.EditValue.ToString() != "3")
                    {
                        dtblsort.Rows.Add(new object[] { "Family", "Family" });
                    }
                    dtblsort.Rows.Add(new object[] { "UPC", "UPC" });
                    dtblsort.Rows.Add(new object[] { "Season", "Season" });
                    if (cmbtran.EditValue.ToString() == "Sales")
                    {
                        dtblsort.Rows.Add(new object[] { "Qty Sold", "Qty Sold" });
                        dtblsort.Rows.Add(new object[] { "Cost", "Cost" });
                        dtblsort.Rows.Add(new object[] { "Revenue", "Revenue" });
                        dtblsort.Rows.Add(new object[] { "Profit", "Profit" });
                        dtblsort.Rows.Add(new object[] { "Margin %", "Margin %" });
                    }
                    if (cmbtran.EditValue.ToString() == "Rent")
                    {
                        dtblsort.Rows.Add(new object[] { "Qty Rented", "Qty Rented" });
                    }
                    string selectedvalue = "";
                    foreach (DataRow dr in dtblsort.Rows)
                    {
                        selectedvalue = dr["Filter"].ToString();
                        break;
                    }
                    cmbsort.ItemsSource = dtblsort;
                    cmbsort.DisplayMember = "FilterText";
                    cmbsort.ValueMember = "Filter";
                    cmbsort.EditValue = selectedvalue;
                    dtblsort.Dispose();
                }

                if (lkupGroupBy.EditValue.ToString() == "5")
                {
                    DataTable dtblsort = new DataTable();
                    dtblsort.Columns.Add("Filter", System.Type.GetType("System.String"));
                    dtblsort.Columns.Add("FilterText", System.Type.GetType("System.String"));

                    cmbsort.ItemsSource = null;
                    dtblsort.Rows.Add(new object[] { "SKU", "SKU" });
                    dtblsort.Rows.Add(new object[] { "Description", "Description" });
                    dtblsort.Rows.Add(new object[] { "Vendor PN", "Vendor PN" });
                    dtblsort.Rows.Add(new object[] { "On Hand Qty", "On Hand Qty" });

                    if (cmbtran.EditValue.ToString() == "Sales")
                    {
                        dtblsort.Rows.Add(new object[] { "Qty Sold", "Qty Sold" });
                        dtblsort.Rows.Add(new object[] { "Cost", "Cost" });
                        dtblsort.Rows.Add(new object[] { "Revenue", "Revenue" });
                        dtblsort.Rows.Add(new object[] { "Profit", "Profit" });
                        dtblsort.Rows.Add(new object[] { "Margin %", "Margin %" });
                    }
                    if (cmbtran.EditValue.ToString() == "Rent")
                    {
                        dtblsort.Rows.Add(new object[] { "Qty Rented", "Qty Rented" });
                    }
                    string selectedvalue = "";
                    foreach (DataRow dr in dtblsort.Rows)
                    {
                        selectedvalue = dr["Filter"].ToString();
                        break;
                    }
                    cmbsort.ItemsSource = dtblsort;
                    cmbsort.DisplayMember = "FilterText";
                    cmbsort.ValueMember = "Filter";
                    cmbsort.EditValue = selectedvalue;
                    dtblsort.Dispose();
                }
            }
            else
            {
                grd.Visibility = Visibility.Collapsed;
                chkgrd.Visibility = Visibility.Collapsed;
                grpsort.Visibility = Visibility.Collapsed;
            }

            if ((lkupGroupBy.EditValue.ToString() == "1") || (lkupGroupBy.EditValue.ToString() == "2") || (lkupGroupBy.EditValue.ToString() == "3") || (lkupGroupBy.EditValue.ToString() == "4")
                || (lkupGroupBy.EditValue.ToString() == "5")) chkgrd.IsChecked = true;
        }

        private void CmbItem_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbItem.EditValue.ToString() == "0")
            {
                grpsort.Visibility = Visibility.Visible;
                cmbsort.Visibility = Visibility.Visible;
                rgsort1_1.Visibility = Visibility.Visible;
                rgsort1_2.Visibility = Visibility.Visible;
                DataTable dtblsort = new DataTable();
                dtblsort.Columns.Add("Filter", System.Type.GetType("System.String"));
                dtblsort.Columns.Add("FilterText", System.Type.GetType("System.String"));

                cmbsort.ItemsSource = null;


                if (cmbItem.EditValue.ToString() == "0")
                {
                    dtblsort.Rows.Add(new object[] { "Description", "Description" });
                    dtblsort.Rows.Add(new object[] { "Family", "Family" });
                    dtblsort.Rows.Add(new object[] { "UPC", "UPC" });
                    dtblsort.Rows.Add(new object[] { "Season", "Season" });
                }
                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    dtblsort.Rows.Add(new object[] { "Qty Sold", "Qty Sold" });
                    dtblsort.Rows.Add(new object[] { "Cost", "Cost" });
                    dtblsort.Rows.Add(new object[] { "Revenue", "Revenue" });
                    dtblsort.Rows.Add(new object[] { "Profit", "Profit" });
                    dtblsort.Rows.Add(new object[] { "Margin %", "Margin %" });
                }
                if (cmbtran.EditValue.ToString() == "Rent")
                {
                    dtblsort.Rows.Add(new object[] { "Qty Rented", "Qty Rented" });
                }
                string selectedvalue = "";
                foreach (DataRow dr in dtblsort.Rows)
                {
                    selectedvalue = dr["Filter"].ToString();
                    break;
                }
                cmbsort.ItemsSource = dtblsort;
                cmbsort.DisplayMember = "FilterText";
                cmbsort.ValueMember = "Filter";
                cmbsort.EditValue = selectedvalue;
                dtblsort.Dispose();
                //btnSelectItem.Visibility = Visibility.Collapsed;
                //btnImport.Visibility = Visibility.Collapsed;
                //grdItem.Visibility = Visibility.Collapsed;
                dtblSelectSKU.Rows.Clear();
            }
            else if (cmbItem.EditValue.ToString() == "-1")
            {
                //btnSelectItem.Visibility = Visibility.Visible;
                //btnImport.Visibility = Visibility.Visible;
                grpsort.Visibility = Visibility.Visible;

                cmbsort.Visibility = Visibility.Collapsed;
                rgsort1_1.Visibility = Visibility.Collapsed;
                rgsort1_2.Visibility = Visibility.Collapsed;
            }
            else
            {
                grpsort.Visibility = Visibility.Collapsed;
                //btnSelectItem.Visibility = Visibility.Collapsed;
                //btnImport.Visibility = Visibility.Collapsed;
                //grdItem.Visibility = Visibility.Collapsed;
                dtblSelectSKU.Rows.Clear();
            }
        }


        private void PopulateSaleRentCategory()
        {
            DataTable dtblCategory = new DataTable();
            dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblCategory.Rows.Add(new object[] { "Sales", "Sales" });
            dtblCategory.Rows.Add(new object[] { "Rent", "Rent" });
            cmbtran.ItemsSource = dtblCategory;
            cmbtran.DisplayMember = "FilterText";
            cmbtran.ValueMember = "Filter";
            cmbtran.EditValue = "Sales";
            dtblCategory.Dispose();
        }

        private void PopulateGroupBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "0", "SKU" });
            dtblOrderBy.Rows.Add(new object[] { "1", "Department" });
            dtblOrderBy.Rows.Add(new object[] { "2", "POS Screen Category" });
            dtblOrderBy.Rows.Add(new object[] { "3", "Family" });
            dtblOrderBy.Rows.Add(new object[] { "4", "Employee" });
            dtblOrderBy.Rows.Add(new object[] { "5", "Vendor" });
            dtblOrderBy.Rows.Add(new object[] { "6", "Monthly" });
            dtblOrderBy.Rows.Add(new object[] { "7", "Day of Week" });
            dtblOrderBy.Rows.Add(new object[] { "8", "Customer" });
            lkupGroupBy.ItemsSource = dtblOrderBy;
            lkupGroupBy.DisplayMember = "Desc";
            lkupGroupBy.ValueMember = "ID";
            lkupGroupBy.EditValue = "0";
            dtblOrderBy.Dispose();
        }

        private void PopulateDepartment()
        {
            colDesc.Header = "Departments";
            PosDataObject.Department objDept = new PosDataObject.Department();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false, strip });
            }
            grd.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void PopulateCategory()
        {
            colDesc.Header = "POS Screen Categories";
            PosDataObject.Category objCat = new PosDataObject.Category();
            objCat.Connection = SystemVariables.Conn;
            DataTable dtbl = objCat.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false, strip });
            }
            grd.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void PopulateFamily()
        {
            colDesc.Header = "Families";
            PosDataObject.Brand objDept = new PosDataObject.Brand();
            objDept.Connection = SystemVariables.Conn;
            objDept.DataObjectCulture_None = Settings.DataObjectCulture_None;
            DataTable dtbl = objDept.FetchData1();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["BrandDescription"].ToString(), false, strip });
            }
            grd.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void PopulateEmployee()
        {
            colDesc.Header = "Employees";
            PosDataObject.Employee objEmp = new PosDataObject.Employee();
            objEmp.Connection = SystemVariables.Conn;
            DataTable dtbl = objEmp.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["FirstName"].ToString() + " " + dr["LastName"].ToString(), false, strip });
            }
            grd.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void PopulateVendor()
        {
            colDesc.Header = "Vendors";
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dtbl = objVendor.FetchData();
            DataTable dtblCls = new DataTable();
            dtblCls.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtblCls.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblCls.Rows.Add(new object[] { dr["ID"].ToString(), dr["Name"].ToString(), false, strip });
            }
            grd.ItemsSource = dtblCls;
            dtbl.Dispose();
            dtblCls.Dispose();
        }

        private void ClickButton(string EventType)
        {
            if (lkupGroupBy.EditValue.ToString() == "8")
            {
                InsertCustomerDataTable();
                if (dtblCust.Rows.Count > 0)
                {
                    frm_SalesCustReportDlg frm_SalesCustReportDlg = new frm_SalesCustReportDlg();
                    try
                    {
                        frm_SalesCustReportDlg.iTranType = cmbtran.EditValue.ToString() == "Sales" ? 0 : 1;
                        frm_SalesCustReportDlg.CustDT = dtblCust;
                        frm_SalesCustReportDlg.FromDT = GeneralFunctions.fnDate(dtFrom.EditValue);
                        frm_SalesCustReportDlg.ToDT = GeneralFunctions.fnDate(dtTo.EditValue);
                        frm_SalesCustReportDlg.ShowDialog();
                    }
                    finally
                    {
                        frm_SalesCustReportDlg.Close();
                    }
                }
                else
                {
                    DocMessage.MsgInformation("No record found in the specified date range");
                    return;
                }
            }
            else if (lkupGroupBy.EditValue.ToString() == "0")
            {


                int PSKU = GeneralFunctions.fnInt32(cmbItem.EditValue.ToString());

                if (PSKU == -1)
                {
                    gridView5.PostEditor();
                    DataTable tblCompare = grdItem.ItemsSource as DataTable;
                    int iSelectedRecord = 0;
                    foreach (DataRow dr in tblCompare.Rows)
                    {
                        if (Convert.ToBoolean(dr["chk"].ToString()) == true) iSelectedRecord++;
                    }
                    if (iSelectedRecord == 0)
                    {
                        DocMessage.MsgInformation("No SKU selected");
                        return;
                    }
                }

                ExecuteReport(EventType);
            }
            else
            {
                int i = 0;
                int j = 0;
                DataTable dG = grd.ItemsSource as DataTable;
                if (dG != null)
                {
                    foreach (DataRow drG in dG.Rows)
                    {
                        if (Convert.ToBoolean(drG["Check"].ToString()))
                        {
                            i++;
                        }
                    }
                    dG.Dispose();
                }

                if (i == 0)
                {
                    if ((lkupGroupBy.EditValue.ToString() == "6") || (lkupGroupBy.EditValue.ToString() == "7"))
                    {
                        if (lkupGroupBy.EditValue.ToString() == "6") ExecuteMonthReport(EventType);
                        if (lkupGroupBy.EditValue.ToString() == "7") ExecuteDOWReport(EventType);
                    }
                    else
                    {
                        if (DocMessage.MsgConfirmation("No Criteria Selected." + "\n" + "Do you print all records?") == MessageBoxResult.Yes)
                        {
                            if (lkupGroupBy.Text == "Vendor") ExecuteVendorReport(EventType);
                            else ExecuteReport(EventType);
                        }
                    }
                }
                else
                {
                    if (lkupGroupBy.EditValue.ToString() == "7") ExecuteDOWReport(EventType);
                    else if (lkupGroupBy.EditValue.ToString() == "6") ExecuteMonthReport(EventType);
                    else if (lkupGroupBy.EditValue.ToString() == "5") ExecuteVendorReport(EventType);
                    else ExecuteReport(EventType);
                }
            }
        }

        private void ExecuteReport(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                string srt = "";
                if (rgsort_1.IsChecked == true) srt = "asc"; else srt = "desc";

                int PSKU = 0;
                string p_sku = "";
                string p_desc = "";
                if (lkupGroupBy.EditValue.ToString() == "0")
                {
                    if (cmbtran.Text == "Sales")
                    {
                        PSKU = GeneralFunctions.fnInt32(cmbItem.EditValue.ToString());
                        PosDataObject.Product objP = new PosDataObject.Product();
                        objP.Connection = SystemVariables.Conn;
                        DataTable dtblT = objP.ShowRecord(PSKU);
                        foreach (DataRow dr in dtblT.Rows)
                        {
                            p_sku = dr["SKU"].ToString();
                            p_desc = dr["Description"].ToString();
                        }
                        dtblT.Dispose();


                    }
                }


                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;

                if (cmbtran.EditValue.ToString() == "Sales")
                {
                    if (PSKU == 0)
                    {
                        dtbl = objSales.FetchSaleReportData(lkupGroupBy.EditValue.ToString(), grd.ItemsSource as DataTable, GeneralFunctions.fnDate(dtFrom.EditValue),
                                                    GeneralFunctions.fnDate(dtTo.EditValue), srt);
                    }
                    else
                    {
                        if (PSKU > 0) dtbl = objSales.FetchSaleReportDataForSpecificSKU(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), PSKU, SystemVariables.DateFormat);
                        if (PSKU == -1)
                        {
                            DataTable dtblSpl = new DataTable();
                            dtblSpl.Columns.Add("SKU", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("Description", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("Qty", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("Price", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("Discount", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("Cost", System.Type.GetType("System.String"));
                            dtblSpl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));

                            DataTable tblCompare = grdItem.ItemsSource as DataTable;
                            tblCompare.DefaultView.Sort = "SKU " + (rgsort_1.IsChecked == true ? "asc" : "desc");
                            tblCompare.DefaultView.ApplyDefaultSort = true;
                            foreach (DataRowView dr in tblCompare.DefaultView)
                            {
                                if (Convert.ToBoolean(dr["chk"].ToString()) == true)
                                {
                                    dtbl = objSales.FetchSaleReportDataForSpecificSKU(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), GeneralFunctions.fnInt32(dr["ID"].ToString()), SystemVariables.DateFormat);
                                    foreach (DataRow dr1 in dtbl.Rows)
                                    {
                                        dtblSpl.Rows.Add(new object[] { dr["SKU"].ToString(), dr["Description"].ToString(),
                                        dr1["InvoiceNo"].ToString(), dr1["InvoiceDate"].ToString(), dr1["Qty"].ToString(), Settings.TaxInclusive == "N" ? dr1["Price"].ToString() : dr1["PriceI"].ToString(),
                                        dr1["Discount"].ToString(), dr1["Cost"].ToString(), dr1["DiscountedCost"].ToString()});
                                    }
                                }
                            }

                            tblCompare.Dispose();

                            dtbl = dtblSpl;

                        }
                    }
                }
                if (cmbtran.EditValue.ToString() == "Rent") dtbl = objSales.FetchRentReportData(lkupGroupBy.EditValue.ToString(), grd.ItemsSource as DataTable, GeneralFunctions.fnDate(dtFrom.EditValue),
                                                    GeneralFunctions.fnDate(dtTo.EditValue), srt);
                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                DataTable repdtbl = new DataTable();

                if ((cmbtran.EditValue.ToString() == "Sales") && (PSKU != 0))
                {
                    repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));

                    double dblRev = 0;
                    double dblCost = 0;
                    double dblDCost = 0;
                    double dblPRev = 0;
                    double dblPCost = 0;
                    double dblProfit = 0;
                    double dblMargin = 0;
                    double dblPQty = 0;
                    double dblRD = 0;

                    if (PSKU > 0)
                    {
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            dblRev = 0;
                            dblCost = 0;
                            dblDCost = 0;
                            dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                            - GeneralFunctions.fnDouble(dr["Discount"].ToString()) :
                                            (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));

                            dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            dblDCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());

                            repdtbl.Rows.Add(new object[] {
                                                   p_sku,p_desc,
                                                   dr["InvoiceNo"].ToString(),
                                                   dr["InvoiceDate"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   dblDCost,
                                                   0,0 });
                        }
                    }


                    if (PSKU == -1)
                    {
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            dblRev = 0;
                            dblCost = 0;
                            dblDCost = 0;
                            dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                            - GeneralFunctions.fnDouble(dr["Discount"].ToString()) :
                                            (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));
                            dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            dblDCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());
                            repdtbl.Rows.Add(new object[] {
                                                   dr["SKU"].ToString(),
                                                   dr["Description"].ToString(),
                                                   dr["InvoiceNo"].ToString(),
                                                   dr["InvoiceDate"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   dblDCost,
                                                   0,0 });
                        }
                    }

                    foreach (DataRow dr3 in repdtbl.Rows)
                    {
                        dblProfit = 0;
                        dblMargin = 0;
                        if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                        {
                            dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                            dblMargin = -99999;
                        }
                        else
                        {
                            dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                            dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                        }

                        dr3["Profit"] = dblProfit;
                        dr3["Margin"] = dblMargin;
                    }

                }
                else
                {
                    repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("Season", System.Type.GetType("System.String"));

                    repdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                    repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));

                    repdtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                    repdtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));

                    double dblRev = 0;
                    double dblCost = 0;
                    double dblDCost = 0;
                    double dblPRev = 0;
                    double dblPCost = 0;
                    double dblPDCost = 0;
                    double dblProfit = 0;
                    double dblMargin = 0;
                    double dblPQty = 0;
                    double dblRD = 0;

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        bool blExists = false;
                        foreach (DataRow dr1 in repdtbl.Rows)
                        {
                            if ((dr1["SKU"].ToString() == dr["SKU"].ToString()) &&
                                (dr1["Description"].ToString() == dr["Description"].ToString()) &&
                                (dr1["FilterID"].ToString() == dr["FilterID"].ToString()))
                            {
                                blExists = true;
                                break;
                            }
                        }

                        if (!blExists)
                        {
                            dblRev = 0;
                            dblCost = 0;
                            dblDCost = 0;
                            dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                            - GeneralFunctions.fnDouble(dr["Discount"].ToString()) :
                                            (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));
                            dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            dblDCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());
                            if (cmbtran.EditValue.ToString() == "Rent")
                            {
                            }
                            repdtbl.Rows.Add(new object[] {dr["SKU"].ToString(),
                                                   dr["Description"].ToString(),
                                                   dr["Brand"].ToString(),
                                                   dr["UPC"].ToString(),
                                                   dr["Season"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   dblDCost,
                                                   0,0,
                                                   dr["FilterID"].ToString(),
                                                   dr["FilterDesc"].ToString()});
                        }
                        else
                        {
                            foreach (DataRow dr2 in repdtbl.Rows)
                            {
                                if ((dr2["SKU"].ToString() == dr["SKU"].ToString()) &&
                                    (dr2["Description"].ToString() == dr["Description"].ToString()) &&
                                    (dr2["FilterID"].ToString() == dr["FilterID"].ToString()))
                                {
                                    dblRev = 0;
                                    dblCost = 0;
                                    dblDCost = 0;

                                    dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                        - GeneralFunctions.fnDouble(dr["Discount"].ToString())
                                        : (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));

                                    dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                                    dblDCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());

                                    dblPRev = GeneralFunctions.fnDouble(dr2["Revenue"].ToString()) + dblRev;
                                    dblPCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString()) + dblCost;
                                    dblPDCost = GeneralFunctions.fnDouble(dr2["DiscountedCost"].ToString()) + dblDCost;
                                    dblPQty = GeneralFunctions.fnDouble(dr2["QtySold"].ToString()) + GeneralFunctions.fnDouble(dr["Qty"].ToString());
                                    dr2["Revenue"] = dblPRev;
                                    dr2["Cost"] = dblPCost;
                                    dr2["DiscountedCost"] = dblPDCost;
                                    dr2["QtySold"] = dblPQty;
                                }
                            }
                        }
                    }

                    foreach (DataRow dr3 in repdtbl.Rows)
                    {
                        dblProfit = 0;
                        dblMargin = 0;
                        if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                        {
                            dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                            dblMargin = -99999;
                        }
                        else
                        {
                            dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                            dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                        }

                        dr3["Profit"] = dblProfit;
                        dr3["Margin"] = dblMargin;
                    }
                }


                OfflineRetailV2.Report.Sales.repSales1 rep_Sales1 = new OfflineRetailV2.Report.Sales.repSales1();
                OfflineRetailV2.Report.Sales.repSales3 rep_Sales3 = new OfflineRetailV2.Report.Sales.repSales3();

                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                GeneralFunctions.MakeReportWatermark(rep_Sales3);

                DataTable frepdtbl = new DataTable();

                if ((cmbtran.EditValue.ToString() == "Sales") && (PSKU != 0))
                {

                    rep_Sales3.Report.DataSource = repdtbl;
                    GeneralFunctions.MakeReportWatermark(rep_Sales3);
                    rep_Sales3.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_Sales3.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_Sales3.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                    rep_Sales3.DecimalPlace = Settings.DecimalPlace;
                    if (Settings.DecimalPlace == 3)
                    {
                        rep_Sales3.rTTot1.Summary.FormatString = "{0:0.000}";
                        rep_Sales3.rTTot2.Summary.FormatString = "{0:0.000}";
                        rep_Sales3.rTTot3.Summary.FormatString = "{0:0.000}";
                        rep_Sales3.rTTot4.Summary.FormatString = "{0:0.000}";
                        rep_Sales3.rTTot6.Summary.FormatString = "{0:0.000}";
                    }
                    else
                    {
                        rep_Sales3.rTTot1.Summary.FormatString = "{0:0.00}";
                        rep_Sales3.rTTot2.Summary.FormatString = "{0:0.00}";
                        rep_Sales3.rTTot3.Summary.FormatString = "{0:0.00}";
                        rep_Sales3.rTTot4.Summary.FormatString = "{0:0.00}";
                        rep_Sales3.rTTot6.Summary.FormatString = "{0:0.00}";
                    }

                    rep_Sales3.GroupHeader1.GroupFields.Add(rep_Sales1.CreateGroupField("SKU"));
                    if (rgsort_1.IsChecked == true) rep_Sales3.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                    if (rgsort_2.IsChecked == true) rep_Sales3.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

                    rep_Sales3.rSKU.DataBindings.Add("Text", repdtbl, "SKU");
                    rep_Sales3.rDesc.DataBindings.Add("Text", repdtbl, "Description");
                    rep_Sales3.rHeader.Text = "Sales (By SKU)"; //+ " " +p_desc + "( " + p_sku + " )";

                    rep_Sales3.rInvoice.DataBindings.Add("Text", repdtbl, "InvoiceNo");
                    rep_Sales3.rDate1.DataBindings.Add("Text", repdtbl, "InvoiceDate");

                    rep_Sales3.rQty.DataBindings.Add("Text", repdtbl, "QtySold");
                    rep_Sales3.rCost.DataBindings.Add("Text", repdtbl, "Cost");
                    rep_Sales3.rDCost.DataBindings.Add("Text", repdtbl, "DiscountedCost");
                    rep_Sales3.rRevenue.DataBindings.Add("Text", repdtbl, "Revenue");
                    rep_Sales3.rProfit.DataBindings.Add("Text", repdtbl, "Profit");
                    rep_Sales3.rMargin.DataBindings.Add("Text", repdtbl, "Margin");

                    rep_Sales3.rTTot1.DataBindings.Add("Text", repdtbl, "QtySold");
                    rep_Sales3.rTTot2.DataBindings.Add("Text", repdtbl, "Cost");
                    rep_Sales3.rTTot6.DataBindings.Add("Text", repdtbl, "DiscountedCost");
                    rep_Sales3.rTTot3.DataBindings.Add("Text", repdtbl, "Revenue");
                    rep_Sales3.rTTot4.DataBindings.Add("Text", repdtbl, "Profit");
                    rep_Sales3.rTTot5.DataBindings.Add("Text", repdtbl, "Margin");

                    if (rgRepType1.IsChecked == true)
                    {
                        rep_Sales3.Detail.Visible = true;
                    }
                    else
                    {
                        rep_Sales3.Detail.Visible = false;
                    }
                }

                else
                {
                    string fsort1 = "";

                    if (lkupGroupBy.EditValue.ToString() != "0")
                    {
                        if (cmbsort.EditValue.ToString() == "SKU") if (rgsort1_1.IsChecked == true) fsort1 = "SKU asc"; else fsort1 = "SKU desc";
                        if (cmbsort.EditValue.ToString() == "Description") if (rgsort1_1.IsChecked == true) fsort1 = "Description asc"; else fsort1 = "Description desc";
                        if (lkupGroupBy.EditValue.ToString() != "3")
                        {
                            if (cmbsort.EditValue.ToString() == "Family") if (rgsort1_1.IsChecked == true) fsort1 = "Brand asc"; else fsort1 = "Brand desc";
                            if (cmbsort.EditValue.ToString() == "UPC") if (rgsort1_1.IsChecked == true) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
                            if (cmbsort.EditValue.ToString() == "Season") if (rgsort1_1.IsChecked == true) fsort1 = "Season asc"; else fsort1 = "Season desc";
                            if (cmbsort.EditValue.ToString() == "Qty Sold") if (rgsort1_1.IsChecked == true) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                            if (cmbsort.EditValue.ToString() == "Cost") if (rgsort1_1.IsChecked == true) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                            if (cmbsort.EditValue.ToString() == "Revenue") if (rgsort1_1.IsChecked == true) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                            if (cmbsort.EditValue.ToString() == "Profit") if (rgsort1_1.IsChecked == true) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                            if (cmbsort.EditValue.ToString() == "Margin %") if (rgsort1_1.IsChecked == true) fsort1 = "Margin asc"; else fsort1 = "Margin desc";
                        }
                        else
                        {
                            if (cmbsort.EditValue.ToString() == "UPC") if (rgsort1_1.IsChecked == true) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
                            if (cmbsort.EditValue.ToString() == "Season") if (rgsort1_1.IsChecked == true) fsort1 = "Season asc"; else fsort1 = "Season desc";
                            if (cmbsort.EditValue.ToString() == "Qty Sold") if (rgsort1_1.IsChecked == true) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                            if (cmbsort.EditValue.ToString() == "Cost") if (rgsort1_1.IsChecked == true) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                            if (cmbsort.EditValue.ToString() == "Revenue") if (rgsort1_1.IsChecked == true) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                            if (cmbsort.EditValue.ToString() == "Profit") if (rgsort1_1.IsChecked == true) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                            if (cmbsort.EditValue.ToString() == "Margin %") if (rgsort1_1.IsChecked == true) fsort1 = "Margin asc"; else fsort1 = "Margin desc";
                        }
                    }
                    else
                    {
                        if (cmbsort.EditValue.ToString() == "Description") if (rgsort1_1.IsChecked == true) fsort1 = "Description asc"; else fsort1 = "Description desc";
                        if (cmbsort.EditValue.ToString() == "Family") if (rgsort1_1.IsChecked == true) fsort1 = "Brand asc"; else fsort1 = "Brand desc";
                        if (cmbsort.EditValue.ToString() == "UPC") if (rgsort1_1.IsChecked == true) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
                        if (cmbsort.EditValue.ToString() == "Season") if (rgsort1_1.IsChecked == true) fsort1 = "Season asc"; else fsort1 = "Season desc";
                        if (cmbsort.EditValue.ToString() == "Qty Sold") if (rgsort1_1.IsChecked == true) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                        if (cmbsort.EditValue.ToString() == "Cost") if (rgsort1_1.IsChecked == true) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                        if (cmbsort.EditValue.ToString() == "Revenue") if (rgsort1_1.IsChecked == true) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                        if (cmbsort.EditValue.ToString() == "Profit") if (rgsort1_1.IsChecked == true) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                        if (cmbsort.EditValue.ToString() == "Margin %") if (rgsort1_1.IsChecked == true) fsort1 = "Margin asc"; else fsort1 = "Margin desc";
                    }
                    repdtbl.DefaultView.Sort = fsort1;
                    repdtbl.DefaultView.ApplyDefaultSort = true;


                    frepdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("Season", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                    frepdtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                    frepdtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));
                    foreach (DataRowView dr in repdtbl.DefaultView)
                    {
                        frepdtbl.Rows.Add(new object[] {
                                                     dr["SKU"].ToString(),
                                                     dr["Description"].ToString(),
                                                     dr["Brand"].ToString(),
                                                     dr["UPC"].ToString(),
                                                     dr["Season"].ToString(),
                                                     dr["QtySold"].ToString(),
                                                     dr["Revenue"].ToString(),
                                                     dr["Cost"].ToString(),
                                                     dr["DiscountedCost"].ToString(),
                                                     dr["Profit"].ToString(),
                                                     dr["Margin"].ToString(),
                                                     dr["FilterID"].ToString(),
                                                     dr["FilterDesc"].ToString()});
                    }
                    rep_Sales1.Report.DataSource = frepdtbl;
                    GeneralFunctions.MakeReportWatermark(rep_Sales1);
                    rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_Sales1.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                    rep_Sales1.DecimalPlace = Settings.DecimalPlace;
                    if (Settings.DecimalPlace == 3)
                    {
                        rep_Sales1.rTot1.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTot2.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTot3.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTot41.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTot6.Summary.FormatString = "{0:0.000}";

                        rep_Sales1.rTTot1.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTTot2.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTTot3.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTTot4.Summary.FormatString = "{0:0.000}";
                        rep_Sales1.rTTot6.Summary.FormatString = "{0:0.000}";
                    }
                    else
                    {
                        rep_Sales1.rTot1.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTot2.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTot3.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTot41.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTot6.Summary.FormatString = "{0:0.00}";

                        rep_Sales1.rTTot1.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTTot2.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTTot3.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTTot4.Summary.FormatString = "{0:0.00}";
                        rep_Sales1.rTTot6.Summary.FormatString = "{0:0.00}";
                    }

                    if (lkupGroupBy.EditValue.ToString() == "0") // SKU
                    {
                        if (cmbtran.EditValue.ToString() == "Sales") rep_Sales1.rHeader.Text = "Sales Details (by SKU)";
                        if (cmbtran.EditValue.ToString() == "Rent") rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Rent Details (by SKU)" : "Rent Summary (by SKU)";
                        rep_Sales1.rGroupIDCaption.Visible = false;
                        rep_Sales1.rGroupDescCaption.Visible = false;
                        rep_Sales1.rGroupID.Visible = false;
                        rep_Sales1.rGroupDesc.Visible = false;
                        rep_Sales1.GroupType = "D";
                    }

                    if (lkupGroupBy.EditValue.ToString() == "1") // Department
                    {
                        if (cmbtran.EditValue.ToString() == "Sales") rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Sales Details (by Department)" : "Sales Summary (by Department)";
                        if (cmbtran.EditValue.ToString() == "Rent") rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Rent Details (by Department)" : "Rent Summary (by Department)";
                        rep_Sales1.rGroupIDCaption.Text = "Department ID" + " : ";
                        rep_Sales1.rGroupDescCaption.Text = "Department" + " : ";
                        if (rgRepType2.IsChecked == true)
                        {
                            rep_Sales1.rGroupIDCaption1.Text = "Department ID";
                            rep_Sales1.rGroupDescCaption1.Text = "Department";
                        }
                        rep_Sales1.GroupType = "D";
                    }

                    if (lkupGroupBy.EditValue.ToString() == "2") // Category
                    {
                        rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Sales Details (by POS Screen Category)" : "Sales Summary (by POS Screen Category)";
                        rep_Sales1.rGroupIDCaption.Text = "Category ID" + " : ";
                        rep_Sales1.rGroupDescCaption.Text = "POS Screen Category" + " : ";
                        if (rgRepType2.IsChecked == true)
                        {
                            rep_Sales1.rGroupIDCaption1.Text = "Category ID";
                            rep_Sales1.rGroupDescCaption1.Text = "POS Screen Category";
                        }
                        rep_Sales1.GroupType = "C";
                    }

                    if (lkupGroupBy.EditValue.ToString() == "3") // Family
                    {
                        rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Sales Details (by Family)" : "Sales Summary (by Family)";
                        rep_Sales1.rGroupIDCaption.Text = "Family ID" + " : ";
                        rep_Sales1.rGroupDescCaption.Text = "Family" + " : ";
                        rep_Sales1.rHFamily.Text = "";
                        rep_Sales1.rHFamily.WidthF = 0;
                        rep_Sales1.rHFamily.BorderColor = System.Drawing.Color.SlateGray;
                        rep_Sales1.rBrand.WidthF = 0;
                        if (rgRepType2.IsChecked == true)
                        {
                            rep_Sales1.rGroupIDCaption1.Text = "Family ID";
                            rep_Sales1.rGroupDescCaption1.Text = "Family";
                        }
                        rep_Sales1.rBrand.Visible = false;
                        rep_Sales1.GroupType = "F";
                    }

                    if (lkupGroupBy.EditValue.ToString() == "4") // Employee
                    {
                        rep_Sales1.rHeader.Text = rgRepType1.IsChecked == true ? "Sales Details (by Employee)" : "Sales Summary (by Employee)";
                        rep_Sales1.rGroupIDCaption.Text = "Employee ID" + " : ";
                        rep_Sales1.rGroupDescCaption.Text = "Employee" + " : ";
                        {
                            rep_Sales1.rGroupIDCaption1.Text = "Employee ID";
                            rep_Sales1.rGroupDescCaption1.Text = "Employee";
                        }
                        rep_Sales1.GroupType = "E";
                    }



                    rep_Sales1.GroupHeader1.GroupFields.Add(rep_Sales1.CreateGroupField("FilterID"));
                    if (rgsort_1.IsChecked == true) rep_Sales1.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                    if (rgsort_2.IsChecked == true) rep_Sales1.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
                    rep_Sales1.rGroupID.DataBindings.Add("Text", frepdtbl, "FilterID");
                    rep_Sales1.rGroupDesc.DataBindings.Add("Text", frepdtbl, "FilterDesc");

                    rep_Sales1.rSKU.DataBindings.Add("Text", frepdtbl, "SKU");
                    rep_Sales1.rProduct.DataBindings.Add("Text", frepdtbl, "Description");

                    rep_Sales1.rUPC.DataBindings.Add("Text", frepdtbl, "UPC");
                    rep_Sales1.rBrand.DataBindings.Add("Text", frepdtbl, "Brand");
                    rep_Sales1.rSeason.DataBindings.Add("Text", frepdtbl, "Season");

                    rep_Sales1.rQty.DataBindings.Add("Text", frepdtbl, "QtySold");
                    rep_Sales1.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                    rep_Sales1.rDCost.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                    rep_Sales1.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                    rep_Sales1.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");
                    rep_Sales1.rMargin.DataBindings.Add("Text", frepdtbl, "Margin");

                    rep_Sales1.rTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                    rep_Sales1.rTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                    rep_Sales1.rTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                    rep_Sales1.rTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                    rep_Sales1.rTot41.DataBindings.Add("Text", frepdtbl, "Profit");
                    //rep_Sales1.rTot5.DataBindings.Add("Text", repdtbl, "Margin");

                    rep_Sales1.rTTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                    rep_Sales1.rTTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                    rep_Sales1.rTTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                    rep_Sales1.rTTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                    rep_Sales1.rTTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                    //rep_Sales1.rTTot5.DataBindings.Add("Text", repdtbl, "Margin");

                    if (rgRepType2.IsChecked == true)
                    {
                        rep_Sales1.rGroupID1.DataBindings.Add("Text", frepdtbl, "FilterID");
                        rep_Sales1.rGroupDesc1.DataBindings.Add("Text", frepdtbl, "FilterDesc");
                    }

                    if (lkupGroupBy.EditValue.ToString() == "0")
                    {
                        rep_Sales1.PageHeader.HeightF = 0;
                        rep_Sales1.rTableHeaderD.Visible = true;
                        rep_Sales1.rTableHeaderS.Visible = false;
                        rep_Sales1.Detail.Visible = true;
                    }
                    else
                    {
                        if (rgRepType1.IsChecked == true)
                        {
                            rep_Sales1.PageHeader.HeightF = 0;
                            rep_Sales1.rTableHeaderD.Visible = true;
                            rep_Sales1.rTableHeaderS.Visible = false;
                            rep_Sales1.Detail.Visible = true;
                        }
                        else
                        {
                            rep_Sales1.rTableHeaderD.Visible = false;
                            rep_Sales1.rTableHeaderS.Visible = true;

                            rep_Sales1.rGroupTable.HeightF = 0;
                            rep_Sales1.rGroupTable.Visible = false;
                            rep_Sales1.GroupHeader1.HeightF = 1;
                            rep_Sales1.GroupFooter1.HeightF = 45;
                            rep_Sales1.xrLine1.Visible = rep_Sales1.xrLine3.Visible = false;
                            rep_Sales1.Detail.Visible = false;
                        }
                    }

                    if (cmbtran.EditValue.ToString() == "Rent")
                    {
                        rep_Sales1.xrlbqty.Text = "Qty Rented";
                        rep_Sales1.xrlbqty.Width = 250;
                        rep_Sales1.xrlbrev.Visible = false;
                        rep_Sales1.xrlbcost.Visible = false;
                        rep_Sales1.xrlbdcost.Visible = false;
                        rep_Sales1.xrlbprofit.Visible = false;
                        rep_Sales1.xrlbmargin.Visible = false;
                        rep_Sales1.rQty.Width = 250;
                        rep_Sales1.rCost.Visible = false;
                        rep_Sales1.rDCost.Visible = false;
                        rep_Sales1.rDCost.Visible = false;
                        rep_Sales1.rRevenue.Visible = false;
                        rep_Sales1.rProfit.Visible = false;
                        rep_Sales1.rMargin.Visible = false;

                        rep_Sales1.rTot1.Width = 250;
                        rep_Sales1.rTot2.Visible = false;
                        rep_Sales1.rTot3.Visible = false;
                        rep_Sales1.rTot4.Visible = false;
                        rep_Sales1.rTot5.Visible = false;
                        rep_Sales1.rTot6.Visible = false;
                        rep_Sales1.rTTot1.Width = 250;
                        rep_Sales1.rTTot2.Visible = false;
                        rep_Sales1.rTTot3.Visible = false;
                        rep_Sales1.rTTot4.Visible = false;
                        rep_Sales1.rTTot5.Visible = false;
                        rep_Sales1.rTTot6.Visible = false;
                    }
                }



                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {

                        if ((cmbtran.EditValue.ToString() == "Sales") && (PSKU != 0))
                        {
                            if (Settings.ReportPrinterName != "") rep_Sales3.PrinterName = Settings.ReportPrinterName;
                            rep_Sales3.CreateDocument();
                            rep_Sales3.PrintingSystem.ShowMarginsWarning = false;
                            rep_Sales3.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_Sales3.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_Sales3;
                            window.ShowDialog();
                        }
                        else
                        {
                            if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                            rep_Sales1.CreateDocument();
                            rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                            rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_Sales1.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_Sales1;
                            window.ShowDialog();
                        }



                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        rep_Sales3.Dispose();


                        dtbl.Dispose();
                        repdtbl.Dispose();
                        frepdtbl.Dispose();
                    }
                }


                if (eventtype == "Print")
                {
                    if ((cmbtran.EditValue.ToString() == "Sales") && (PSKU > 0))
                    {
                        rep_Sales3.CreateDocument();
                        rep_Sales3.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_Sales3);
                        }
                        finally
                        {
                            rep_Sales3.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                    else
                    {
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_Sales1);
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                }


                if (eventtype == "Email")
                {
                    if ((cmbtran.EditValue.ToString() == "Sales") && (PSKU > 0))
                    {
                        rep_Sales3.CreateDocument();
                        rep_Sales3.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            attachfile = "sales.pdf";
                            GeneralFunctions.EmailReport(rep_Sales3, attachfile, "Sales");
                        }
                        finally
                        {
                            rep_Sales3.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                    else
                    {
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            attachfile = "sales.pdf";
                            GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Sales");
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void InsertCustomerDataTable()
        {
            dtblCust.Rows.Clear();
            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
            if (cmbtran.EditValue.ToString() == "Sales") dtbl = objSales.FetchCustomer(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
            if (cmbtran.EditValue.ToString() == "Rent") dtbl = objSales.FetchCustomerForRent(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));

            foreach (DataRow dr in dtbl.Rows)
            {
                string strG = GetCustomerGroup(GeneralFunctions.fnInt32(dr["ID"].ToString()));
                string strC = GetCustomerClass(GeneralFunctions.fnInt32(dr["ID"].ToString()));
                dtblCust.Rows.Add(new object[] {
                                                    dr["ID"].ToString(),
                                                    dr["CustomerID"].ToString(),
                                                    dr["Company"].ToString(),
                                                    dr["LastName"].ToString(),
                                                    dr["Name"].ToString(),
                                                    dr["WorkPhone"].ToString(),
                                                    dr["Total"].ToString(),strG,strC });
            }
            dtbl.Dispose();
        }

        private string GetCustomerGroup(int CID)
        {
            string rets = "";
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCG = new PosDataObject.Customer();
            objCG.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objCG.ShowGroup(CID);
            foreach (DataRow dr in dtbl.Rows)
            {
                if (rets == "") rets = dr["GroupIDName"].ToString();
                else rets = rets + ", " + dr["GroupIDName"].ToString();
            }
            dtbl.Dispose();
            return rets;
        }

        private string GetCustomerClass(int CID)
        {
            string rets = "";
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCC = new PosDataObject.Customer();
            objCC.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objCC.ShowClass(CID);
            foreach (DataRow dr in dtbl.Rows)
            {
                if (rets == "") rets = dr["ClassIDName"].ToString();
                else rets = rets + ", " + dr["ClassIDName"].ToString();
            }
            dtbl.Dispose();
            return rets;
        }

        private void ExecuteVendorReport(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                string srt = "";
                if (rgsort_1.IsChecked == true) srt = "asc"; else srt = "desc";
                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;
                dtbl = objSales.FetchVendorSaleReportData(
                                   grd.ItemsSource as DataTable, GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), srt);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));

                double dblRev = 0;
                double dblCost = 0;
                double dblPRev = 0;
                double dblPCost = 0;
                double dblProfit = 0;
                double dblMargin = 0;
                double dblPQty = 0;

                foreach (DataRow dr in dtbl.Rows)
                {
                    bool blExists = false;
                    foreach (DataRow dr1 in repdtbl.Rows)
                    {
                        if ((dr1["SKU"].ToString() == dr["SKU"].ToString()) &&
                            (dr1["Description"].ToString() == dr["Description"].ToString())
                            && (dr1["VendorID"].ToString() == dr["VendorID"].ToString())
                            )
                        {
                            blExists = true;
                            break;
                        }
                    }

                    if (!blExists)
                    {
                        dblRev = 0;
                        dblCost = 0;
                        dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                            - GeneralFunctions.fnDouble(dr["Discount"].ToString()) :
                            (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));
                        dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());

                        repdtbl.Rows.Add(new object[] {dr["SKU"].ToString(),
                                                   dr["Description"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   0,0,
                                                   dr["FilterID"].ToString(),
                                                   dr["FilterDesc"].ToString(),
                                                   dr["VendorID"].ToString(),
                                                   dr["Vendor"].ToString(),
                                                   dr["PartNumber"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["QtyOnHand"].ToString())});
                    }
                    else
                    {
                        foreach (DataRow dr2 in repdtbl.Rows)
                        {
                            if ((dr2["SKU"].ToString() == dr["SKU"].ToString()) &&
                                (dr2["Description"].ToString() == dr["Description"].ToString()))
                            {
                                dblRev = 0;
                                dblCost = 0;
                                dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                    - GeneralFunctions.fnDouble(dr["Discount"].ToString()) :
                                    (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()));

                                dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                                dblPRev = GeneralFunctions.fnDouble(dr2["Revenue"].ToString()) + dblRev;
                                dblPCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString()) + dblCost;
                                dblPQty = GeneralFunctions.fnDouble(dr2["QtySold"].ToString()) + GeneralFunctions.fnDouble(dr["Qty"].ToString());
                                dr2["Revenue"] = dblPRev;
                                dr2["Cost"] = dblPCost;
                                dr2["QtySold"] = dblPQty;
                            }
                        }
                    }
                }

                foreach (DataRow dr3 in repdtbl.Rows)
                {
                    dblProfit = 0;
                    dblMargin = 0;
                    if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = -99999;
                    }
                    else
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                    }

                    dr3["Profit"] = dblProfit;
                    dr3["Margin"] = dblMargin;
                }

                OfflineRetailV2.Report.Sales.repSales2 rep_Sales2 = new OfflineRetailV2.Report.Sales.repSales2();

                string fsort1 = "";

                if (cmbsort.EditValue.ToString() == "SKU") if (rgsort1_1.IsChecked == true) fsort1 = "SKU asc"; else fsort1 = "SKU desc";
                if (cmbsort.EditValue.ToString() == "Description") if (rgsort1_1.IsChecked == true) fsort1 = "Description asc"; else fsort1 = "Description desc";
                if (cmbsort.EditValue.ToString() == "Vendor PN") if (rgsort1_1.IsChecked == true) fsort1 = "PartNumber asc"; else fsort1 = "PartNumber desc";
                if (cmbsort.EditValue.ToString() == "On Hand Qty") if (rgsort1_1.IsChecked == true) fsort1 = "QtyOnHand asc"; else fsort1 = "QtyOnHand desc";
                if (cmbsort.EditValue.ToString() == "Qty Sold") if (rgsort1_1.IsChecked == true) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                if (cmbsort.EditValue.ToString() == "Cost") if (rgsort1_1.IsChecked == true) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                if (cmbsort.EditValue.ToString() == "Revenue") if (rgsort1_1.IsChecked == true) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                if (cmbsort.EditValue.ToString() == "Profit") if (rgsort1_1.IsChecked == true) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                if (cmbsort.EditValue.ToString() == "Margin %") if (rgsort1_1.IsChecked == true) fsort1 = "Margin asc"; else fsort1 = "Margin desc";

                repdtbl.DefaultView.Sort = fsort1;
                repdtbl.DefaultView.ApplyDefaultSort = true;

                DataTable frepdtbl = new DataTable();
                frepdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("VendorID", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("Vendor", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("PartNumber", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));

                foreach (DataRowView dr in repdtbl.DefaultView)
                {
                    frepdtbl.Rows.Add(new object[] { dr["SKU"].ToString(),
                                                     dr["Description"].ToString(),
                                                     GeneralFunctions.fnDouble(dr["QtySold"].ToString()),
                                                     GeneralFunctions.fnDouble(dr["Revenue"].ToString()),
                                                     GeneralFunctions.fnDouble(dr["Cost"].ToString()),
                                                     GeneralFunctions.fnDouble(dr["Profit"].ToString()),
                                                     GeneralFunctions.fnDouble(dr["Margin"].ToString()),
                                                     dr["FilterID"].ToString(),
                                                     dr["FilterDesc"].ToString(),
                                                     dr["VendorID"].ToString(),
                                                     dr["Vendor"].ToString(),
                                                     dr["PartNumber"].ToString(),
                                                     GeneralFunctions.fnDouble(dr["QtyOnHand"].ToString())});
                }


                rep_Sales2.Report.DataSource = frepdtbl;
                GeneralFunctions.MakeReportWatermark(rep_Sales2);
                rep_Sales2.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales2.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales2.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_Sales2.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Sales2.rTot1.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rsTot1.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rsTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rsTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rsTot4.Summary.FormatString = "{0:0.000}";
                    //rep_Sales1.rTot5.Summary.FormatString = "{0:0.000}";

                    rep_Sales2.rTTot1.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot4.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Sales2.rTot1.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rsTot1.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rsTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rsTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rsTot4.Summary.FormatString = "{0:0.00}";
                    //rep_Sales1.rTot5.Summary.FormatString = "{0:0.00}";

                    rep_Sales2.rTTot1.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot4.Summary.FormatString = "{0:0.00}";
                }

                rep_Sales2.rHeader.Text = rgRepType1.IsChecked == true ? "Sales Details (by Vendor)" : "Sales Summary (by Vendor)";

                rep_Sales2.GroupHeader1.GroupFields.Add(rep_Sales2.CreateGroupField("VendorID"));
                if (rgsort_1.IsChecked == true) rep_Sales2.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                if (rgsort_2.IsChecked == true) rep_Sales2.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
                rep_Sales2.rVendorGID.DataBindings.Add("Text", frepdtbl, "VendorID");
                rep_Sales2.rVendor.DataBindings.Add("Text", frepdtbl, "Vendor");

                rep_Sales2.GroupHeader2.GroupFields.Add(rep_Sales2.CreateGroupField("FilterID"));
                //if (rgsort_1.IsChecked == true) rep_Sales2.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                //if (rgsort_2.IsChecked == true) rep_Sales2.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

                rep_Sales2.rGroupID.DataBindings.Add("Text", frepdtbl, "FilterID");
                rep_Sales2.rGroupDesc.DataBindings.Add("Text", frepdtbl, "FilterDesc");

                rep_Sales2.rSKU.DataBindings.Add("Text", frepdtbl, "SKU");
                rep_Sales2.rPN.DataBindings.Add("Text", frepdtbl, "PartNumber");
                rep_Sales2.rProduct.DataBindings.Add("Text", frepdtbl, "Description");
                rep_Sales2.rQtyOH.DataBindings.Add("Text", frepdtbl, "QtyOnHand");
                rep_Sales2.rQty.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_Sales2.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales2.rMargin.DataBindings.Add("Text", frepdtbl, "Margin");

                rep_Sales2.rTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_Sales2.rTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales2.rsTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_Sales2.rsTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rsTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rsTot4.DataBindings.Add("Text", frepdtbl, "Profit");

                rep_Sales2.rTTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_Sales2.rTTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rTTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rTTot4.DataBindings.Add("Text", frepdtbl, "Profit");

                if (rgRepType1.IsChecked == true)
                {
                    rep_Sales2.Detail.Visible = true;
                    rep_Sales2.rGroupTableS.HeightF = 0;
                    rep_Sales2.rGroupTableS.Visible = false;
                    rep_Sales2.GroupHeader1.HeightF = 42;
                }
                else
                {
                    rep_Sales2.rGroupID1.DataBindings.Add("Text", frepdtbl, "FilterID");
                    rep_Sales2.rGroupDesc1.DataBindings.Add("Text", frepdtbl, "FilterDesc");

                    rep_Sales2.rGroupID1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular);
                    rep_Sales2.rGroupDesc1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular);
                    rep_Sales2.rGroupTable1.Visible = false;
                    rep_Sales2.rGroupTable2.Visible = false;
                    rep_Sales2.rGroupTable1.HeightF = 0;
                    rep_Sales2.rGroupTable2.HeightF = 0;
                    rep_Sales2.GroupHeader2.HeightF = 0;
                    rep_Sales2.xrLine1.Visible = rep_Sales2.xrLine2.Visible = false;
                    rep_Sales2.rHSKU.WidthF = 0;
                    rep_Sales2.rHSKU.BorderColor = System.Drawing.Color.SlateGray;

                    rep_Sales2.Detail.Visible = false;
                }

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales2.PrinterName = Settings.ReportPrinterName;
                        rep_Sales2.CreateDocument();
                        rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales2.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_Sales2.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Sales2;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_Sales2.Dispose();

                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }


                if (eventtype == "Print")
                {
                    rep_Sales2.CreateDocument();
                    rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Sales2);
                    }
                    finally
                    {
                        rep_Sales2.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Sales2.CreateDocument();
                    rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "sales_by_vendor.pdf";
                        GeneralFunctions.EmailReport(rep_Sales2, attachfile, "Sales by Vendor");
                    }
                    finally
                    {
                        rep_Sales2.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }

            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteDOWReport(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }


                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;
                if (cmbtran.EditValue.ToString() == "Sales") dtbl1 = objSales.FetchDayOfWeekSaleReportData(grd.ItemsSource as DataTable,
                                                            GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.TaxInclusive);
                if (cmbtran.EditValue.ToString() == "Rent") dtbl1 = objSales.FetchDayOfWeekRentReportData(grd.ItemsSource as DataTable,
                                                           GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
                dtbl = GetFinalDayOfWeekData(dtbl1);
                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("WeekDay", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                repdtbl.Rows.Add(new object[] { "Sunday", 0 });
                repdtbl.Rows.Add(new object[] { "Monday", 0 });
                repdtbl.Rows.Add(new object[] { "Tuesday", 0 });
                repdtbl.Rows.Add(new object[] { "Wednesday", 0 });
                repdtbl.Rows.Add(new object[] { "Thursday", 0 });
                repdtbl.Rows.Add(new object[] { "Friday", 0 });
                repdtbl.Rows.Add(new object[] { "Saturday", 0 });

                foreach (DataRow dr in repdtbl.Rows)
                {
                    bool blf = false;
                    double dblval = 0;
                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        if (dr["WeekDay"].ToString() == dr1["WeekDay"].ToString())
                        {
                            dblval = GeneralFunctions.fnDouble(GeneralFunctions.fnDouble(dr1["TotSale"].ToString()).ToString("f2"));
                            blf = true;
                            break;
                        }
                    }
                    if (blf)
                    {
                        dr["TotSale"] = dblval.ToString();
                    }
                }

                foreach (DataRow dr in repdtbl.Rows)
                {
                    if (dr["WeekDay"].ToString() == "Sunday") dr["WeekDay"] = "Sunday";
                    else if (dr["WeekDay"].ToString() == "Monday") dr["WeekDay"] = "Monday";
                    else if (dr["WeekDay"].ToString() == "Tuesday") dr["WeekDay"] = "Tuesday";
                    else if (dr["WeekDay"].ToString() == "Wednesday") dr["WeekDay"] = "Wednesday";
                    else if (dr["WeekDay"].ToString() == "Thursday") dr["WeekDay"] = "Thursday";
                    else if (dr["WeekDay"].ToString() == "Friday") dr["WeekDay"] = "Friday";
                    else dr["WeekDay"] = "Saturday";
                }

                OfflineRetailV2.Report.Sales.repSalesByDayOfWeek rep_SalesByDayOfWeek = new OfflineRetailV2.Report.Sales.repSalesByDayOfWeek();
                GeneralFunctions.MakeReportWatermark(rep_SalesByDayOfWeek);
                rep_SalesByDayOfWeek.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_SalesByDayOfWeek.rReportHeader.Text = Settings.ReportHeader_Address;
                (rep_SalesByDayOfWeek.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Day of Week";
                (rep_SalesByDayOfWeek.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Sales";
                if (cmbtran.EditValue.ToString() == "Sales") rep_SalesByDayOfWeek.rHeader.Text = "Sales by Day of Week";
                if (cmbtran.EditValue.ToString() == "Rent") rep_SalesByDayOfWeek.rHeader.Text = "Rent by Day of Week";
                rep_SalesByDayOfWeek.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                /*
                int i = 0;
                string dept = "";
                DataTable dg = grd.ItemsSource as DataTable;
                foreach (DataRow drG in dg.Rows)
                {
                    if (Convert.ToBoolean(drG["Check"].ToString()))
                    {
                        if (dept == "") dept = drG["Description"].ToString();
                        else dept = dept + ", " + drG["Description"].ToString();
                        i++;
                    }
                }

                if (dg.Rows.Count == i) rep_SalesByDayOfWeek.rDetSum.Text = "For All Departments";
                else rep_SalesByDayOfWeek.rDetSum.Text = "For Selected Departments : " + dept;
                dg.Dispose();
                */
                rep_SalesByDayOfWeek.rDetSum.Text = "";
                rep_SalesByDayOfWeek.xrChart1.DataSource = repdtbl;
                rep_SalesByDayOfWeek.xrChart1.SeriesDataMember = "WeekDay";
                rep_SalesByDayOfWeek.xrChart1.SeriesTemplate.ArgumentDataMember = "WeekDay";
                rep_SalesByDayOfWeek.xrChart1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "TotSale" });

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_SalesByDayOfWeek.PrinterName = Settings.ReportPrinterName;
                        rep_SalesByDayOfWeek.CreateDocument();
                        rep_SalesByDayOfWeek.PrintingSystem.ShowMarginsWarning = false;
                        rep_SalesByDayOfWeek.PrintingSystem.ShowPrintStatusDialog = false;


                        //rep_SalesByDayOfWeek.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_SalesByDayOfWeek;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_SalesByDayOfWeek.Dispose();

                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_SalesByDayOfWeek.CreateDocument();
                    rep_SalesByDayOfWeek.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_SalesByDayOfWeek);
                    }
                    finally
                    {
                        rep_SalesByDayOfWeek.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_SalesByDayOfWeek.CreateDocument();
                    rep_SalesByDayOfWeek.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "sales_by_day_of_week.pdf";
                        GeneralFunctions.EmailReport(rep_SalesByDayOfWeek, attachfile, "Sales by Day of Week");
                    }
                    finally
                    {
                        rep_SalesByDayOfWeek.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }

            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private string GetMonthName(int mno)
        {
            string ret = "";
            if (mno == 1) ret = "Jan";
            else if (mno == 2) ret = "Feb";
            else if (mno == 3) ret = "Mar";
            else if (mno == 4) ret = "Apr";
            else if (mno == 5) ret = "May";
            else if (mno == 6) ret = "Jun";
            else if (mno == 7) ret = "Jul";
            else if (mno == 8) ret = "Aug";
            else if (mno == 9) ret = "Sep";
            else if (mno == 10) ret = "Oct";
            else if (mno == 11) ret = "Nov";
            else ret = "Dec";
            return ret;
        }

        private void ExecuteMonthReport(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                DataTable dtbl1 = new DataTable();
                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;
                if (cmbtran.EditValue.ToString() == "Sales") dtbl1 = objSales.FetchMonthlySaleReportData(grd.ItemsSource as DataTable,
                                                            GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.TaxInclusive);
                else dtbl1 = objSales.FetchMonthlyRentReportData(grd.ItemsSource as DataTable,
                                                            GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
                dtbl = GetFinalMonthData(dtbl1);
                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("YearMonth", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));

                int intsm = dtFrom.DateTime.Month;
                int intsy = dtFrom.DateTime.Year;
                int inttm = dtTo.DateTime.Month;
                int intty = dtTo.DateTime.Year;

                int ii = 1 + (inttm - intsm) + (intty - intsy) * 12;

                for (int j = 1; j <= ii; j++)
                {
                    repdtbl.Rows.Add(new object[] { GetMonthName(intsm) + intsy.ToString().Substring(2, 2), 0 });
                    if (intsm == 12)
                    {
                        intsm = 1;
                        intsy++;
                    }
                    else intsm++;

                }

                foreach (DataRow dr in repdtbl.Rows)
                {
                    bool blf = false;
                    double dblval = 0;
                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        if (dr["YearMonth"].ToString() == dr1["YearMonth"].ToString())
                        {
                            dblval = GeneralFunctions.fnDouble(GeneralFunctions.fnDouble(dr1["TotSale"].ToString()).ToString("f2"));
                            blf = true;
                            break;
                        }
                    }
                    if (blf)
                    {
                        dr["TotSale"] = dblval.ToString();
                    }
                }

                foreach (DataRow dr in repdtbl.Rows)
                {
                    if (dr["YearMonth"].ToString().StartsWith("Jan")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Jan", "Jan");
                    else if (dr["YearMonth"].ToString().StartsWith("Feb")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Feb", "Feb");
                    else if (dr["YearMonth"].ToString().StartsWith("Mar")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Mar", "Mar");
                    else if (dr["YearMonth"].ToString().StartsWith("Apr")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Apr", "Apr");
                    else if (dr["YearMonth"].ToString().StartsWith("May")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("May", "May");
                    else if (dr["YearMonth"].ToString().StartsWith("Jun")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Jun", "Jun");
                    else if (dr["YearMonth"].ToString().StartsWith("Jul")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Jul", "Jul");
                    else if (dr["YearMonth"].ToString().StartsWith("Aug")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Aug", "Aug");
                    else if (dr["YearMonth"].ToString().StartsWith("Sep")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Sep", "Sep");
                    else if (dr["YearMonth"].ToString().StartsWith("Oct")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Oct", "Oct");
                    else if (dr["YearMonth"].ToString().StartsWith("Nov")) dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Nov", "Nov");
                    else dr["YearMonth"] = dr["YearMonth"].ToString().Replace("Dec", "Dec");

                }

                OfflineRetailV2.Report.Sales.repSalesByMonth rep_SalesByMonth = new OfflineRetailV2.Report.Sales.repSalesByMonth();
                GeneralFunctions.MakeReportWatermark(rep_SalesByMonth);
                rep_SalesByMonth.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_SalesByMonth.rReportHeader.Text = Settings.ReportHeader_Address;
                (rep_SalesByMonth.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Month";
                (rep_SalesByMonth.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Sales";
                if (cmbtran.EditValue.ToString() == "Sales") rep_SalesByMonth.rHeader.Text = "Sales by Month";
                if (cmbtran.EditValue.ToString() == "Rent") rep_SalesByMonth.rHeader.Text = "Rent by Month";
                rep_SalesByMonth.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

                /*
                int i = 0;
                string dept = "";
                DataTable dg = grd.ItemsSource as DataTable;
                foreach (DataRow drG in dg.Rows)
                {
                    if (Convert.ToBoolean(drG["Check"].ToString()))
                    {
                        if (dept == "") dept = drG["Description"].ToString();
                        else dept = dept + ", " + drG["Description"].ToString();
                        i++;
                    }
                }

                if (dg.Rows.Count == i) rep_SalesByMonth.rDetSum.Text = "For All Departments";
                else rep_SalesByMonth.rDetSum.Text = "For Selected Departments : " + dept;
                dg.Dispose();
                */
                rep_SalesByMonth.rDetSum.Text = "";

                rep_SalesByMonth.xrChart1.DataSource = repdtbl;
                rep_SalesByMonth.xrChart1.SeriesDataMember = "YearMonth";
                rep_SalesByMonth.xrChart1.SeriesTemplate.ArgumentDataMember = "YearMonth";
                rep_SalesByMonth.xrChart1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "TotSale" });

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_SalesByMonth.PrinterName = Settings.ReportPrinterName;
                        rep_SalesByMonth.CreateDocument();
                        rep_SalesByMonth.PrintingSystem.ShowMarginsWarning = false;
                        rep_SalesByMonth.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_SalesByMonth.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_SalesByMonth;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_SalesByMonth.Dispose();

                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_SalesByMonth.CreateDocument();
                    rep_SalesByMonth.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_SalesByMonth);
                    }
                    finally
                    {
                        rep_SalesByMonth.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_SalesByMonth.CreateDocument();
                    rep_SalesByMonth.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "sales_mobthly.pdf";
                        GeneralFunctions.EmailReport(rep_SalesByMonth, attachfile, "Month wise Sales");
                    }
                    finally
                    {
                        rep_SalesByMonth.Dispose();
                        dtbl.Dispose();
                        repdtbl.Dispose();
                    }
                }

            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private DataTable GetFinalMonthData(DataTable rftbl)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("YearMonth", System.Type.GetType("System.String"));
            dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));
            int cnt = 0;
            int i = 0;
            string currval = "";
            string prevval = "";
            double cval = 0;
            double pval = 0;
            cnt = rftbl.Rows.Count;
            foreach (DataRow dr in rftbl.Rows)
            {
                currval = dr["YearMonth"].ToString();
                cval = GeneralFunctions.fnDouble(dr["TotSale"].ToString());
                if (cnt == 1)
                {
                    dtbl.Rows.Add(new object[] { currval, cval });
                }
                else
                {
                    i++;
                    if (i == cnt)
                    {
                        if (currval == prevval)
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval + cval });
                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval });
                            dtbl.Rows.Add(new object[] { currval, cval });
                        }
                    }
                    else
                    {
                        if ((currval != prevval) && (prevval != ""))
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval });
                            pval = cval;
                            prevval = currval;
                        }
                        else
                        {
                            pval = pval + cval;
                            prevval = currval;
                        }
                    }
                }
            }
            return dtbl;

        }

        private DataTable GetFinalDayOfWeekData(DataTable rftbl)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("WeekDay", System.Type.GetType("System.String"));
            dtbl.Columns.Add("TotSale", System.Type.GetType("System.Double"));
            int cnt = 0;
            int i = 0;
            string currval = "";
            string prevval = "";
            double cval = 0;
            double pval = 0;
            cnt = rftbl.Rows.Count;
            foreach (DataRow dr in rftbl.Rows)
            {
                currval = dr["WeekDay"].ToString();
                cval = GeneralFunctions.fnDouble(dr["TotSale"].ToString());
                if (cnt == 1)
                {
                    dtbl.Rows.Add(new object[] { currval, cval });
                }
                else
                {
                    i++;
                    if (i == cnt)
                    {
                        if (currval == prevval)
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval + cval });
                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval });
                            dtbl.Rows.Add(new object[] { currval, cval });
                        }
                    }
                    else
                    {
                        if ((currval != prevval) && (prevval != ""))
                        {
                            dtbl.Rows.Add(new object[] { prevval, pval });
                            pval = cval;
                            prevval = currval;
                        }
                        else
                        {
                            pval = pval + cval;
                            prevval = currval;
                        }
                    }
                }
            }
            return dtbl;

        }

        private void Cmbtran_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
