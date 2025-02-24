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

namespace OfflineRetailV2.Controls
{
    /// <summary>
    /// Interaction logic for LoginControl1.xaml
    /// </summary>
    public partial class LoginControl1 : UserControl
    {
        public LoginControl1()
        {
            InitializeComponent();
            txtUser.TextChanged += TxtUser_TextChanged;
            txtPswd.PasswordBox.PasswordChanged += LoginPasswordBox_PasswordChanged;
        }

        public Grid BlurGrid { get; set; }

        /// <summary>
        /// Command to run if Login actions confirmed.
        /// </summary>
        public Action ConfirmCommand { get; set; }

        private void TxtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        #region UILogic

        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }



        #endregion UILogic
    }
}
