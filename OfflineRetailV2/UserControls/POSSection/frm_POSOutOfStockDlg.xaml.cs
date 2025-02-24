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
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSOutOfStockDlg.xaml
    /// </summary>
    public partial class frm_POSOutOfStockDlg : Window
    {
        private DataTable dtblStock;

        public DataTable Stock
        {
            get { return dtblStock; }
            set { dtblStock = value; }
        }

        public frm_POSOutOfStockDlg()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Out_Of_Stock;
            lBox.ItemsSource = dtblStock;
            lBox.DisplayMember = "PRODUCT";
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
               new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_ , "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_ , "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
