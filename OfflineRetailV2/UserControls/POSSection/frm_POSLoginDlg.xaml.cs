using OfflineRetailV2.Data;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Interaction logic for frm_POSLoginDlg.xaml
    /// </summary>
    public partial class frm_POSLoginDlg : Window
    {
        private int intSuperUserID;
        private string strSecurityCode;
        private bool blAllowByAdmin;
        private string strSelectedUserID = "";
        public frm_POSLoginDlg()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
            

            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }
        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public string SecurityCode
        {
            get { return strSecurityCode; }
            set { strSecurityCode = value; }
        }

        public bool AllowByAdmin
        {
            get { return blAllowByAdmin; }
            set { blAllowByAdmin = value; }
        }
        private void OnCloseCommand(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }
        private bool IsValid()
        {
           
            return true;
        }
        private void txtUser_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void BackLinkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTouchKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void KeyPadPasswordControl_PasswordChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(KeyPadPasswordControl.Password))
            {
                bool bProcced = false;
                if (KeyPadPasswordControl.Password.Length == 4)
                {
                    bProcced = true;
                }
                if (bProcced)
                {
                    System.Threading.Thread.Sleep(10);
                    KeyPadPasswordControl.Refresh();
                    if ((KeyPadPasswordControl.Password == Settings.ADMINPASSWORD))
                    {
                        intSuperUserID = 0;
                        blAllowByAdmin = true;
                    }
                    else
                    {
                        PosDataObject.Security objSecurity1 = new PosDataObject.Security();
                        objSecurity1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        intSuperUserID = objSecurity1.IsExistsPOSSuperUserID(strSelectedUserID, KeyPadPasswordControl.Password,
                                                                        strSecurityCode, 0);

                        if (intSuperUserID != 0)
                        {
                            PosDataObject.Security objSecurity = new PosDataObject.Security();
                            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            intSuperUserID = objSecurity.FetchPOSSuperUserID(strSelectedUserID, KeyPadPasswordControl.Password,
                                                                            strSecurityCode, Settings.SignINOption);
                            if (intSuperUserID == 0)
                            {
                                new MessageBoxWindow().Show(Properties.Resources.Invalid_Login_ID___Password + "\n" +
                                    Properties.Resources.OR + "\n" + Properties.Resources.This_login_do_not_have_the_access_, Properties.Resources.POS_Access, MessageBoxButton.OK, MessageBoxImage.Information);

                                return;
                            }
                        }
                        else
                        {
                            new MessageBoxWindow().Show(Properties.Resources.Invalid_Login_ID___Password + "\n" + Properties.Resources.OR + "\n" + Properties.Resources.This_login_do_not_have_the_access_, Properties.Resources.POS_Access, MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        blAllowByAdmin = false;
                    }
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Super_User_Login;
            LoadEmployees();
        }


        public void LoadEmployees()
        {
            strSelectedUserID = "";
            pnlEmp.Children.Clear();
            PosDataObject.Login employee = new PosDataObject.Login();
            employee.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = employee.FetchAllEmployees();
            foreach (DataRow dr in dtbl.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == SystemVariables.CurrentUserID) continue;
                Grid grd = new Grid();
                grd.Margin = new Thickness(10, 10, 10, 20);
                grd.Width = 120;
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(96.03) });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });


                Button button = new Button();
                button.Width = 96.03;
                button.Height = 96.03;
                button.Content = dr["DisplayName"].ToString();
                button.Tag = dr["EmployeeID"].ToString();
                button.Click += ButtonEmpId_Click;
                button.Style = this.FindResource("CustomEmp" + GetStyleIndex(dr["EmployeeID"].ToString().Substring(0, 1))) as Style;

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

        private void ButtonEmpId_Click(object sender, RoutedEventArgs e)
        {
            string selectedempId = (sender as System.Windows.Controls.Button).Tag.ToString();
            SetSeletionStyle(selectedempId);
            strSelectedUserID = selectedempId;
            if (!string.IsNullOrEmpty(KeyPadPasswordControl.Password)) KeyPadPasswordControl.ResetPassCode();
        }

        private void SetSeletionStyle(string seletedTag)
        {
            foreach (UIElement c in pnlEmp.Children)
            {
                if (c is Grid)
                {
                    foreach (UIElement c1 in (c as Grid).Children)
                    {
                        if (c1 is System.Windows.Controls.Button)
                        {
                            if ((c1 as System.Windows.Controls.Button).Tag.ToString() != seletedTag)
                            {
                                (c1 as System.Windows.Controls.Button).BorderBrush = System.Windows.Media.Brushes.White;
                            }
                            else
                            {
                                (c1 as System.Windows.Controls.Button).BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
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

        public bool IsUserSelected()
        {
            if (GeneralFunctions.GetRecordCount("Employee") == 0)
            {
                return true;
            }
            else
            {
                if (strSelectedUserID == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
