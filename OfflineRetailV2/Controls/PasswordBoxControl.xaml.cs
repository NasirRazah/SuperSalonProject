// Sam Park @ Mental Code Master 2019
//--
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfflineRetailV2.Controls
{
    /// <summary>
    /// Interaction logic for PasswordBoxControl.xaml
    /// </summary>
    public partial class PasswordBoxControl : UserControl
    {
        public PasswordBoxControl()
        {
            InitializeComponent();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.SelectAll();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
                PasswordWaterMarkText.Visibility = Visibility.Visible;
            else
                PasswordWaterMarkText.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
                PasswordWaterMarkText.Visibility = Visibility.Visible;
            else
                PasswordWaterMarkText.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
                PasswordWaterMarkText.Visibility = Visibility.Collapsed;
        }
    }
}