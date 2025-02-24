// Sam Park @ Mental Code Master 2019
//--
using System;
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.Controls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
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