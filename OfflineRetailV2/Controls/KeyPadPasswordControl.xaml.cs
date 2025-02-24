using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace OfflineRetailV2.Controls
{
    /// <summary>
    /// Interaction logic for KeyPadPasswordControl.xaml
    /// </summary>
    public partial class KeyPadPasswordControl : UserControl
    {
        public event EventHandler PasswordChanged;

        private SolidColorBrush PassFillColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B364C2DB"));
        public KeyPadPasswordControl()
        {
            InitializeComponent();
            Password = "";
        }
        public string Password { get; private set; }
        string _pass1 = "";
        string _pass2 = "";
        string _pass3 = "";
        string _pass4 = "";
        private void FillPassEllipse()
        {
            Pass1.Fill = Pass2.Fill = Pass3.Fill = Pass4.Fill = new SolidColorBrush(SystemVariables.SelectedTheme == "Dark" ?Colors.Transparent : (Color)ColorConverter.ConvertFromString("#EFF4F4"));
            Password = "";

            if (_pass1 != "")
            {
                Pass1.Fill = PassFillColor;
                Password += _pass1;
            }
            if (_pass2 != "")
            {
                Pass2.Fill = PassFillColor;
                Password += _pass2;
            }
            if (_pass3 != "")
            {
                Pass3.Fill = PassFillColor;
                Password += _pass3;
            }
            if (_pass4 != "")
            {
                Pass4.Fill = PassFillColor;
                Password += _pass4;
            }

            PasswordChanged?.Invoke(null, null);
        }
        private void KeyPad1_Click(object sender, RoutedEventArgs e)
        {
            //if (!(Window.GetWindow(this) as MainWindow).IsUserSelected()) return;
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "1";
            else if (_pass2 == "") _pass2 = "1";
            else if (_pass3 == "") _pass3 = "1";
            else if (_pass4 == "") _pass4 = "1";
            FillPassEllipse();
        }

        private void KeyPad2_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "2";
            else if (_pass2 == "") _pass2 = "2";
            else if (_pass3 == "") _pass3 = "2";
            else if (_pass4 == "") _pass4 = "2";
            FillPassEllipse();
        }

        private void KeyPad3_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "3";
            else if (_pass2 == "") _pass2 = "3";
            else if (_pass3 == "") _pass3 = "3";
            else if (_pass4 == "") _pass4 = "3";
            FillPassEllipse();
        }

        private void KeyPad4_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "4";
            else if (_pass2 == "") _pass2 = "4";
            else if (_pass3 == "") _pass3 = "4";
            else if (_pass4 == "") _pass4 = "4";
            FillPassEllipse();
        }

        private void KeyPad5_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "5";
            else if (_pass2 == "") _pass2 = "5";
            else if (_pass3 == "") _pass3 = "5";
            else if (_pass4 == "") _pass4 = "5";
            FillPassEllipse();
        }

        private void KeyPad6_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "6";
            else if (_pass2 == "") _pass2 = "6";
            else if (_pass3 == "") _pass3 = "6";
            else if (_pass4 == "") _pass4 = "6";
            FillPassEllipse();
        }

        private void KeyPad7_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "7";
            else if (_pass2 == "") _pass2 = "7";
            else if (_pass3 == "") _pass3 = "7";
            else if (_pass4 == "") _pass4 = "7";
            FillPassEllipse();
        }

        private void KeyPad8_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "8";
            else if (_pass2 == "") _pass2 = "8";
            else if (_pass3 == "") _pass3 = "8";
            else if (_pass4 == "") _pass4 = "8";
            FillPassEllipse();
        }

        private void KeyPad9_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "9";
            else if (_pass2 == "") _pass2 = "9";
            else if (_pass3 == "") _pass3 = "9";
            else if (_pass4 == "") _pass4 = "9";
            FillPassEllipse();
        }

        private void KeyPad0_Click(object sender, RoutedEventArgs e)
        {
            dynamic callingWindow = Window.GetWindow(this) as MainWindow;
            if (callingWindow == null)
                callingWindow = Window.GetWindow(this) as UserControls.POSSection.frm_POSLoginDlg;

            if (!callingWindow.IsUserSelected()) return;
            if (_pass1 == "") _pass1 = "0";
            else if (_pass2 == "") _pass2 = "0";
            else if (_pass3 == "") _pass3 = "0";
            else if (_pass4 == "") _pass4 = "0";
            FillPassEllipse();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_pass4 != "")
            {
                _pass4 = "";
                FillPassEllipse();
                //return;
            }
            if (_pass3 != "")
            {
                _pass3 = "";
                FillPassEllipse();
                //return;
            }
            if (_pass2 != "")
            {
                _pass2 = "";
                FillPassEllipse();
                //return;
            }
            if (_pass1 != "")
            {
                _pass1 = "";
                FillPassEllipse();
                //return;
            }

           (Window.GetWindow(this) as MainWindow).ResetSeletionStyle();
        }


        public void ResetPassCode()
        {
            if (_pass4 != "")
            {
                _pass4 = "";
                FillPassEllipse();
            }
            if (_pass3 != "")
            {
                _pass3 = "";
                FillPassEllipse();
            }
            if (_pass2 != "")
            {
                _pass2 = "";
                FillPassEllipse();
            }
            if (_pass1 != "")
            {
                _pass1 = "";
                FillPassEllipse();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_pass4 != "")
            {
                _pass4 = "";
                FillPassEllipse();
                return;
            }
            if (_pass3 != "")
            {
                _pass3 = "";
                FillPassEllipse();
                return;
            }
            if (_pass2 != "")
            {
                _pass2 = "";
                FillPassEllipse();
                return;
            }
            if (_pass1 != "")
            {
                _pass1 = "";
                FillPassEllipse();
                return;
            }
        }
    }
}
