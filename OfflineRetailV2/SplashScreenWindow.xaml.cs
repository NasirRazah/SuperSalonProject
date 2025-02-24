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

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
        }

        public double Progress
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*try
            {
                lbBrand.Text = ConfigSettings.GetBrandName();
            }
            catch
            {
                lbBrand.Text = "XEPOS";
            }*/
        }
    }
}
