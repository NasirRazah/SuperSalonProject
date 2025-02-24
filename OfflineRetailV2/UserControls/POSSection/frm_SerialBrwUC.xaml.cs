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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_SerialBrwUC.xaml
    /// </summary>
    public partial class frm_SerialBrwUC : UserControl
    {
        public frm_SerialBrwUC()
        {
            InitializeComponent();
        }

        private void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private async void HandleKeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            GeneralFunctions.SetFocus(txtSearchGrdData);
            //if (tcPOS == null) return;
            //if (txtSearch.Text == "Search Items") return;
            //if (txtSearch.Text == "") return;
            //if (e.Key == Key.Return)
            //{
            //    await AddtoCartFromFind((sender as OfflineRetailV2.Controls.CustomTextBox).Text.Trim());
            //}
        }
    }
}
