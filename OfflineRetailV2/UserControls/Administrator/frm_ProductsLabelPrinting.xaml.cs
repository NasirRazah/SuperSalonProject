using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_ProductsLabelPrinting.xaml
    /// </summary>
    public partial class frm_ProductsLabelPrinting : UserControl
    {

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Ordering");
        }



        public frm_ProductsLabelPrinting()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            // LoadProducts();
            ProductTble = new DataTable();
            ProductTble.Columns.Add("ID", System.Type.GetType("System.String"));
            ProductTble.Columns.Add("ProductCode", System.Type.GetType("System.String"));
            ProductTble.Columns.Add("ItemCode", System.Type.GetType("System.String"));
            ProductTble.Columns.Add("Price", System.Type.GetType("System.String"));
            ProductTble.Columns.Add("Description", System.Type.GetType("System.String"));
            ProductTble.Columns.Add("NoOfLabel", System.Type.GetType("System.String"));

        }

        public void LoadProducts()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchData(false, "All");
            //datagrid1.ItemsSource = dbtbl.DefaultView;
        }

        private void FindProduct(object sender, RoutedEventArgs e)
        {
            //if (String.IsNullOrWhiteSpace(itemCode.Text))
            //    return;

            //PosDataObject.Product objProduct = new PosDataObject.Product();
            //objProduct.Connection = SystemVariables.Conn;
            //DataTable dt = objProduct.ShowSKURecord(itemCode.Text);
            //if (dt.Rows.Count == 0)
            //    MessageBox.Show("Wrong Item Code");


        }

        DataTable ProductTble = new DataTable();
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductCode.Text) || string.IsNullOrWhiteSpace(txtNoOFLabel.Text))
            {
                MessageBox.Show("Please enter Product Code OR No Of Labels");
                return;
            }
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dt = objProduct.ShowSKURecord(txtProductCode.Text);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No product found. Please enter Valid Product Code!");
                return;
            }

            //if (ProductTble.Rows.Count == 0)
            //{
                
            //}

            txtProduct.Text = dt.Rows[0]["Description"].ToString();
            txtSalePrice.Text = dt.Rows[0]["PriceA"].ToString();

            if (ProductTble == null)
                Load();
            ProductTble.Rows.Add(dt.Rows[0]["SKU"], dt.Rows[0]["SKU"], dt.Rows[0]["ID"], dt.Rows[0]["PriceA"], dt.Rows[0]["Description"], txtNoOFLabel.Text);


            datagrid1.DataContext = ProductTble.DefaultView;
            datagrid1.CanUserAddRows = false;


            grdPL.ItemsSource = ProductTble;

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ProductTble = null;
            datagrid1.DataContext = null;
            grdPL.ItemsSource = null;

            txtBarCode.Text = String.Empty;
            txtNoOFLabel.Text = String.Empty;
            txtProduct.Text = String.Empty;
            txtProductCode.Text = String.Empty;
            txtSalePrice.Text = String.Empty;
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowNum = -1;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0 || grdPL.ItemsSource == null || (grdPL.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            DataTable dtbl = new DataTable();
            dtbl = (grdPL.ItemsSource) as DataTable;
            dtbl.Rows[intRowNum].Delete();
            grdPL.ItemsSource = ProductTble = dtbl;
            dtbl.Dispose();

            //ProductTble.Rows[intRowID].Delete();
            //grdPL.ItemsSource = ProductTble;
        }

        private void gridview1_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {

        }

        public async Task<int> ReturnRowID1()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPL, colID)));
            return intRecID;
        }

        private void btn_print(object sender, RoutedEventArgs e)
        {

        }
    }
}
