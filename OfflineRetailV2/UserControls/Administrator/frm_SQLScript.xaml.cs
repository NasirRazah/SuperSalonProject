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
using System.Text.RegularExpressions;
using System.IO;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_SQLScript.xaml
    /// </summary>
    public partial class frm_SQLScript : Window
    {
        public frm_SQLScript()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private SqlConnection sqlConn;
        public SqlConnection Connection
        {
            set { sqlConn = value; }
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
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
                if (ExecuteSql(SystemVariables.Conn, memSQL.Text.Trim()))
                {
                    DocMessage.MsgInformation(Properties.Resources.SQL_script_successfully_executed);
                    DialogResult = true;
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
            DialogResult = false;
        }

        public bool ExecuteSql(SqlConnection connection, string sql)
        {
            bool f = true;
            Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string[] lines = regex.Split(sql);

            if (connection.State == ConnectionState.Closed) connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                foreach (string line in lines)
                {
                    if (!f) break;
                    if (line.Length > 0)
                    {
                        cmd.CommandText = line;
                        cmd.CommandType = CommandType.Text;

                        try
                        {
                            cmd.CommandTimeout = 5000;
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            throw;
                            f = false;
                        }
                    }
                }
            }
            transaction.Commit();
            return f;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
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
