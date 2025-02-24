using System;
using System.Collections;
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
using System.Text.RegularExpressions;
using System.IO;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_SQLView.xaml
    /// </summary>
    public partial class frm_SQLView : Window
    {
        private SqlConnection sqlConn;
        public SqlConnection Connection
        {
            set { sqlConn = value; }
        }

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_SQLView()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public bool ExecuteSql(SqlConnection connection, string sql)
        {
            bool f = true;
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds, "ss");

                dataGridView1.ItemsSource = ds.Tables["ss"] as IEnumerable;
            }
            catch
            {
                f = false;
            }
            return f;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (memSQL.Text.Trim() == "")
                {
                    DocMessage.MsgInformation(Properties.Resources.No_SQL_script_found_to_execute);
                    return;
                }
                dataGridView1.ItemsSource = null;
                dataGridView1.Columns.Clear();


                if (ExecuteSql(SystemVariables.Conn, memSQL.Text.Trim()))
                {

                }
                else
                {
                    DocMessage.MsgError(Properties.Resources.Error_while_execute_SQL_script);
                    return;
                }
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }
    }
}
