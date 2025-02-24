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
using System.Data.SqlClient;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_VendorUpdateDlg.xaml
    /// </summary>
    public partial class frm_VendorUpdateDlg : Window
    {
        private int strPrevVendorID;
        private int strCurrVendorID;
        private string strSKU;
        private string strReturnValue;

        public frm_VendorUpdateDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public int PrevVendorID
        {
            get { return strPrevVendorID; }
            set { strPrevVendorID = value; }
        }
        public int CurrVendorID
        {
            get { return strCurrVendorID; }
            set { strCurrVendorID = value; }
        }
        public string SKU
        {
            get { return strSKU; }
            set { strSKU = value; }
        }
        public string ReturnValue
        {
            get { return strReturnValue; }
            set { strReturnValue = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Vendor_Update_Confirmation;
            if (strPrevVendorID == 0)
            {
                lbText.Text =
                       Properties.Resources.Click_Yes_to_set_Primary_Vendor_to_ + strCurrVendorID
                        + "\n" + Properties.Resources.Click_No_to_leave_Current_Primary_Vendor
                        + "\n" + Properties.Resources._Click_Yes_to_All_to_update_Primary_Vendor_for_rest_of_the_items
                        + "\n" + Properties.Resources.Click_No_to_All_to_leave_Primary_Vendor_for_rest_of_the_items;
            }
            else
            {
                lbText.Text =  Properties.Resources.Current_Primary_Vendor_for + strSKU + " is " + strPrevVendorID + " ."
                        + "\n" + Properties.Resources.Click_Yes_to_set_Primary_Vendor_to_ + strCurrVendorID
                         + "\n" + Properties.Resources.Click_No_to_leave_Current_Primary_Vendor
                        + "\n" + Properties.Resources._Click_Yes_to_All_to_update_Primary_Vendor_for_rest_of_the_items
                        + "\n" + Properties.Resources.Click_No_to_All_to_leave_Primary_Vendor_for_rest_of_the_items;
            }
            
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            strReturnValue = "Y";
            Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            strReturnValue = "N";
            Close();
        }

        private void BtnYesAll_Click(object sender, RoutedEventArgs e)
        {
            strReturnValue = "YA";
            Close();
        }

        private void BtnNoAll_Click(object sender, RoutedEventArgs e)
        {
            strReturnValue = "NA";
            Close();
        }
    }
}
