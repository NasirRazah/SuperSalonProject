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
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PosDataObject.Login employee = new PosDataObject.Login();
            employee.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = employee.FetchAllEmployees();
            foreach(DataRow dr in dtbl.Rows)
            {
                Grid grd = new Grid();
                grd.Margin = new Thickness(20, 10, 10, 20);
                grd.Width = 120;
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(96.03) });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
               

                Button button = new Button();
                button.Width = 96.03;
                button.Height = 96.03;
                button.Content = dr["DisplayName"].ToString();
                button.Tag = dr["EmployeeID"].ToString();
                button.Click += Button_Click;
                button.Style = this.FindResource("CustomEmp" + GetStyleIndex(dr["EmployeeID"].ToString().Substring(0,1))) as Style;

                Grid.SetRow(button, 0);

                grd.Children.Add(button);

                TextBlock tb = new TextBlock();
                tb.Text = dr["EmployeeID"].ToString(); 
                tb.TextAlignment = TextAlignment.Center;
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Padding = new Thickness(5);
                tb.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(tb, 1);
                grd.Children.Add(tb);


                pnlEmp.Children.Add(grd);

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedempId = (sender as System.Windows.Controls.Button).Tag.ToString();
            SetSeletionStyle(selectedempId);

        }

        private void SetSeletionStyle(string seletedTag)
        {
            foreach(UIElement c in pnlEmp.Children)
            {
                if (c is Grid)
                {
                    foreach(UIElement c1 in (c as Grid).Children)
                    {
                        if (c1 is System.Windows.Controls.Button)
                        {
                            if ((c1 as System.Windows.Controls.Button).Tag.ToString() != seletedTag)
                            {
                                (c1 as System.Windows.Controls.Button).BorderBrush = Brushes.White;
                            }
                            else
                            {
                                (c1 as System.Windows.Controls.Button).BorderBrush = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                    }
                }
            }
        }

        private string GetStyleIndex(string strch)
        {
            string indx = "";
            if ((strch.ToUpper() == "A") || (strch.ToUpper() == "B") || (strch == "0"))
            {
                indx = "1";
            }
            if ((strch.ToUpper() == "C") || (strch.ToUpper() == "D") || (strch == "1"))
            {
                indx = "2";
            }
            if ((strch.ToUpper() == "E") || (strch.ToUpper() == "F") || (strch == "2"))
            {
                indx = "3";
            }
            if ((strch.ToUpper() == "G") || (strch.ToUpper() == "H") || (strch == "3"))
            {
                indx = "4";
            }
            if ((strch.ToUpper() == "I") || (strch.ToUpper() == "J") || (strch == "4"))
            {
                indx = "5";
            }
            if ((strch.ToUpper() == "K") || (strch.ToUpper() == "L") || (strch == "5"))
            {
                indx = "6";
            }
            if ((strch.ToUpper() == "M") || (strch.ToUpper() == "N") || (strch == "6"))
            {
                indx = "7";
            }
            if ((strch.ToUpper() == "O") || (strch.ToUpper() == "P") || (strch == "7"))
            {
                indx = "8";
            }
            if ((strch.ToUpper() == "Q") || (strch.ToUpper() == "R") || (strch.ToUpper() == "S") || (strch == "8"))
            {
                indx = "9";
            }
            if ((strch.ToUpper() == "T") || (strch.ToUpper() == "U") || (strch.ToUpper() == "V") || (strch.ToUpper() == "W") || (strch.ToUpper() == "X") || (strch.ToUpper() == "Y") || (strch.ToUpper() == "Z") || (strch == "9"))
            {
                indx = "10";
            }
            return indx;
        }

        
    }
}
