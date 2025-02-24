using OfflineRetailV2.Data;
using OfflineRetailV2.UserControls;
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
using System.Windows.Shapes;

namespace OfflineRetailV2.ProductsLabel
{
    /// <summary>
    /// Interaction logic for frm_ProductsLabel.xaml
    /// </summary>
    public partial class frm_ProductsLabel : Window
    {

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }



        public frm_ProductsLabel()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {

            LoadProducts();
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
            if (String.IsNullOrWhiteSpace(itemCode.Text))
                return ;

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dt = objProduct.ShowSKURecord(itemCode.Text);
            if (dt.Rows.Count == 0)
                MessageBox.Show("Wrong Item Code");


        }

        DataTable ProductTble = new DataTable();
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtProductCode.Text) || string.IsNullOrWhiteSpace(txtNoOFLabel.Text))
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

            if (ProductTble.Rows.Count == 0)
            {

                ProductTble.Columns.Add("ProductCode", System.Type.GetType("System.String"));
                ProductTble.Columns.Add("ItemCode", System.Type.GetType("System.String"));
                ProductTble.Columns.Add("Price", System.Type.GetType("System.String"));
                ProductTble.Columns.Add("Description", System.Type.GetType("System.String"));
                ProductTble.Columns.Add("NoOfLabel", System.Type.GetType("System.String"));
            }

            ProductTble.Rows.Add(dt.Rows[0]["SKU"], dt.Rows[0]["SKU"], dt.Rows[0]["PriceA"], dt.Rows[0]["Description"], txtNoOFLabel.Text);

          
                datagrid1.DataContext = ProductTble.DefaultView;
          

            datagrid1.CanUserAddRows = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductTble = new DataTable();
            datagrid1.DataContext = null;
        }
    }
}
