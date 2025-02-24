using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using DevExpress.Xpf.Core;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls
{
    public partial class WeightedItemsWindow : ThemedWindow
    {
        string connectionString = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
                                   .Descendants("conn").FirstOrDefault()?.Value;

        private DataTable cartTable;
        private CartWindow cartWindow;
        private Scale scale;
        private string weightstring = "";
        private int weightitemid;
        private string weightitemname;

        public WeightedItemsWindow()
        {
            InitializeComponent();
            InitializeCartTable();
            cartWindow = new CartWindow(cartTable);  // Initialize the CartWindow
            InitializeScale();
            LoadProducts();
        }

        private void InitializeScale()
        {
            // Initialize the scale (you might need to adjust this based on your actual scale implementation)
            scale = new MyScale();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ProductsListBox.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void InitializeCartTable()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("Description", typeof(string));
            cartTable.Columns.Add("Quantity", typeof(double));
            cartTable.Columns.Add("Price", typeof(double));
            cartTable.Columns.Add("TotalAmount", typeof(double));
        }

        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddSelectedItemToCart();
        }

        private void WeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddSelectedItemToCart();
        }

        private async void AddSelectedItemToCart()
        {
            try
            {
                DataRowView selectedRow = (DataRowView)ProductsListBox.SelectedItem;
                if (selectedRow != null)
                {
                    weightitemid = Convert.ToInt32(selectedRow["ProductId"]);
                    weightitemname = selectedRow["ProductName"].ToString();

                    // Read the weight from the scale
                    weightstring = ReadWeightFromScale();
                    if (string.IsNullOrEmpty(weightstring)) return;

                    // Display the weight in the WeightLabel
                    WeightLabel.Content = weightstring;

                    // Add the item and weight to the cart
                    await AddWeightedItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item to cart: " + ex.Message);
            }
        }

        private string ReadWeightFromScale()
        {
            try
            {
                // Implement the logic to read weight from the scale
                decimal weight = scale.ReadWeight(5000); // 5 seconds timeout
                return weight.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading weight: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        private async Task AddWeightedItem()
        {
            try
            {
                if (!GeneralFunctions.IsValidScaleWeight_POS(weightstring, true)) return;

                string line1 = "";
                string line2 = "";
                bool blfinddata = false;
                string strselectid = weightitemid.ToString();
                string refType = "";
                string refOHQty = "";
                string refNQty = "";
                string refPrice = "";
                string refNPrice = "";
                string refCost = "";
                string refPrompt = "";
                string refDP = "";
                string refAge = "";
                string refRentMI = "";
                string refRentHR = "";
                string refRentHDY = "";
                string refRentDY = "";
                string refRentWK = "";
                string refRentMN = "";
                string refRentDeposit = "";
                string refRentMinHR = "";
                string refRentMinAmt = "";
                string refRepairAmt = "";
                string refRepairPromptAmt = "";
                string refRepairPromptTag = "";
                string refRentPrompt = "";
                int SaleID = 0;
                double SalePrice = 0;
                double Tare = 0;

                string refUOM = "";
                string refExpiry = "";
                FetchProduct(GeneralFunctions.fnInt32(strselectid), strDiscountLevel.Trim(), intUsePriceLevel, ref refType, ref refOHQty, ref refNQty,
                             ref refPrice, ref refNPrice, ref refCost, ref refPrompt, ref refDP, ref refAge, ref refRentMI, ref refRentHR, ref refRentHDY, ref refRentDY,
                             ref refRentWK, ref refRentMN, ref refRentDeposit, ref refRentMinHR, ref refRentMinAmt, ref refRentPrompt, ref refRepairAmt,
                             ref refRepairPromptAmt, ref refRepairPromptTag, ref refUOM, ref refExpiry);

                FetchActiveSale(GeneralFunctions.fnInt32(strselectid), ref SaleID, ref SalePrice);

                double Tr1 = 0;
                double Tr2 = 0;
                FetchTare(GeneralFunctions.fnInt32(strselectid), ref Tr1, ref Tr2);

                Tare = GetTareOnWeight(GeneralFunctions.fnDouble(weightstring), Tr1, Tr2);

                if (blTareEntry)
                {
                    Tare = dblTareEntryValue;
                    dblTareEntryValue = 0;
                    blTareEntry = false;
                }

                if (SaleID > 0)
                {
                    refPrompt = "N";
                    refPrice = SalePrice.ToString();
                }

                if (GeneralFunctions.fnDouble(weightstring) - Tare > 0)
                {
                    if (refPrompt == "Y")
                    {
                        double promptPrice = GetPromptPrice(weightitemname, GeneralFunctions.fnDouble(refPrice));
                        if (promptPrice == -99999) return;
                        DataRow cartRow = cartTable.NewRow();
                        cartRow["Description"] = weightitemname;
                        cartRow["Quantity"] = GeneralFunctions.fnDouble(weightstring) - Tare;
                        cartRow["Price"] = promptPrice;
                        cartRow["TotalAmount"] = (GeneralFunctions.fnDouble(weightstring) - Tare) * promptPrice;
                        cartTable.Rows.Add(cartRow);
                    }
                    else
                    {
                        DataRow cartRow = cartTable.NewRow();
                        cartRow["Description"] = weightitemname;
                        cartRow["Quantity"] = GeneralFunctions.fnDouble(weightstring) - Tare;
                        cartRow["Price"] = GeneralFunctions.fnDouble(refPrice);
                        cartRow["TotalAmount"] = (GeneralFunctions.fnDouble(weightstring) - Tare) * GeneralFunctions.fnDouble(refPrice);
                        cartTable.Rows.Add(cartRow);
                    }
                }
                else
                {
                    if (Tare > GeneralFunctions.fnDouble(weightstring))
                    {
                        MessageBox.Show("Tare Weight exceeds Gross Weight");
                    }
                }

                weightstring = "";
                if (Settings.PriceLevelForOneTime == "Y")
                {
                    Settings.PriceLevelForOneTime = "N";
                    intUsePriceLevel = Settings.UsePriceLevel;
                }
                cartWindow.UpdateCartTable(cartTable);
                cartWindow.Show();
                cartWindow.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding weighted item to cart: " + ex.Message);
            }
        }

        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic for generating an invoice here
            MessageBox.Show("Invoice generated!");
        }

        private void ShowWeightedItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Showing weighted items!");
        }
    }
}