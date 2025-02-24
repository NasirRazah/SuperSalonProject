using OfflineRetailV2.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frm_dbUpdation.xaml
    /// </summary>
    /// 
    public static class ExtensionMethods
    {
        private static readonly Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

    public partial class frm_dbUpdation : Window
    {
        private SqlConnection sqlConn;
        private string strVersion = "0";
        public string strDbVersionResult = "OK";
        private SqlConnection Connection
        {
            set { sqlConn = value; }
        }

        public frm_dbUpdation()
        {
            InitializeComponent();

            this.Connection = SystemVariables.Conn;
            marqueeProgressBarControl1.Visibility = Visibility.Collapsed;

            Loaded += frmdbUpdation_Load;

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            Close();
        }

        private bool CheckMultilingualUpdate()
        {
            string strSQL = "select isnull(SetMultilingual,'N') as CheckMultilingualUpdate from DbVersion ";
            string strError;
            string val = "";
            SqlCommand sqlComm = null;
            SqlDataReader sqlReader = null;
            this.Connection = new SqlConnection(SystemVariables.ConnectionString);

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) sqlConn.Open();
                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.Read())
                {
                    val = sqlReader["CheckMultilingualUpdate"].ToString();
                }
            }
            catch (SqlException SQLDBException)
            {
                val = "N";
                strError = SQLDBException.Message;
            }
            return val == "Y";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        #region Update Database with Script

        private void UpdateDatabase(ArrayList arlTables, ArrayList arlLanguageTables)
        {
            string[] arColDtl = new string[4];
            string AppPath = Environment.CurrentDirectory;
            if (AppPath.EndsWith("\\")) AppPath = AppPath + "scripts\\";
            else AppPath = AppPath + "\\scripts\\";
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;


            ArrayList WorkArray = new ArrayList();


            /*if (CheckMultilingualUpdate())
            {
                bool f = false;
                for (int intTableCount = 0; intTableCount <= (arlTables.Count - 1); intTableCount++)
                {
                    if (arlTables[intTableCount].ToString().StartsWith("M"))
                    {
                        f = true;
                    }

                    if (!f) continue;

                    if ((!arlTables[intTableCount].ToString().StartsWith("M")) && f)
                    {
                        WorkArray.Add(arlTables[intTableCount].ToString());
                    }
                }
            }
            else
            {
                bool f = false;
                for (int intTableCount = 0; intTableCount <= (arlTables.Count - 1); intTableCount++)
                {
                    if (arlTables[intTableCount].ToString().StartsWith("M"))
                    {
                        f = true;
                    }

                    WorkArray.Add(arlTables[intTableCount].ToString());

                    if (!f)
                    {
                        continue;
                    }

                    if (arlTables[intTableCount].ToString().StartsWith("M"))
                    {
                        for (int intTableCount1 = 0; intTableCount1 <= (arlLanguageTables.Count - 1); intTableCount1++)
                        {
                            WorkArray.Add(arlLanguageTables[intTableCount1].ToString());
                        }
                    }
                }
            }*/


            for (int intTableCount1 = 0; intTableCount1 <= (arlTables.Count - 1); intTableCount1++)
            {
                WorkArray.Add(arlTables[intTableCount1].ToString());
            }

            for (int intTableCount = 0; intTableCount <= (WorkArray.Count - 1); intTableCount++)
            {
                //Application.DoEvents();
                txtfile.Text = WorkArray[intTableCount].ToString();
                this.txtfile.Refresh();
                marqueeProgressBarControl1.Value = Convert.ToDouble(intTableCount + 1);
                Thread.Sleep(1);
                if (File.Exists(AppPath + WorkArray[intTableCount].ToString()))
                {

                    try
                    {
                        ExecuteSql(SystemVariables.Conn, AppPath + WorkArray[intTableCount].ToString());
                    }
                    catch
                    {
                    }
                }

                try
                {
                    double dblValue = (intTableCount + 1 / WorkArray.Count);

                }
                catch
                { }


            }



            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public void ExecuteSql(SqlConnection connection, string sqlFile)
        {
            string sql = "";

            using (FileStream strm = File.OpenRead(sqlFile))
            {
                StreamReader reader = new StreamReader(strm);
                sql = reader.ReadToEnd();
            }


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
                        }
                    }
                }
            }

            transaction.Commit();
        }

        #endregion

        private void frmdbUpdation_Load(object sender, EventArgs e)
        {
            Title.Text =Properties.Resources.Update_Database;
        }
        public void CheckDbVersions()
        {
            CheckDbVersion();
        }

        private void CheckDbVersion()
        {
            bool boolFound = true;
            string strSQL = "select isnull(CurrentVersion,'0') as CurrentVersion from DbVersion ";
            string strError;

            SqlCommand sqlComm = null;
            SqlDataReader sqlReader = null;
            this.Connection = new SqlConnection(SystemVariables.ConnectionString);

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed) sqlConn.Open();
                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.Read())
                {
                    strVersion = sqlReader["CurrentVersion"].ToString();

                }
            }
            catch (SqlException SQLDBException)
            {
                // DbVersion Table not found
                boolFound = false;
                strError = SQLDBException.Message;
            }

            if (sqlReader != null)
                sqlReader.Close();
            sqlConn.Close();
            sqlComm.Dispose();

            if (boolFound)
            {
                if (strVersion == "")
                {
                    strVersion = DbDefination.strDbVersion;
                    InsertDbVersionData();
                }
                try
                {
                    if (GeneralFunctions.fnDouble(strVersion) < GeneralFunctions.fnDouble(DbDefination.strDbVersion))
                    {
                        strDbVersionResult = "Old";
                        if (GeneralFunctions.fnDouble(strVersion) != 0) lbAdvise.Visibility = Visibility.Visible;
                    }
                    else if (GeneralFunctions.fnDouble(strVersion) > GeneralFunctions.fnDouble(DbDefination.strDbVersion))
                        strDbVersionResult = "New";
                    else
                        strDbVersionResult = "OK";
                }
                catch
                {
                    strDbVersionResult = "Old";
                }
            }
            else
                strDbVersionResult = "";
        }

        // Create DB Version Table

        public bool CreateDbVersionTable()
        {
            bool boolResult = true;
            string strSQL = "CREATE TABLE [DbVersion] ([CurrentVersion][varchar](50) NULL) ON [PRIMARY]";

            string strError;
            SqlCommand sqlComm = null;
            this.Connection = SystemVariables.Conn;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                boolResult = false;
                strError = SQLDBException.Message.ToString();
            }

            sqlConn.Close();
            sqlComm.Dispose();

            return boolResult;
        }

        // Add Default Version 1.000

        public void InsertDefaltDbVersionData()
        {
            string strSQL = "insert into DbVersion (CurrentVersion) values ('1.000')";
            string strError;
            SqlCommand sqlComm = null;
            this.Connection = SystemVariables.Conn;
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strError = SQLDBException.Message.ToString();
            }
            sqlConn.Close();
            sqlComm.Dispose();
        }

        // insert Version 

        public void InsertDbVersionData()
        {
            string strSQL = "insert into DbVersion (CurrentVersion) values (" +
                DbDefination.strDbVersion + ")";
            string strError;
            SqlCommand sqlComm = null;
            this.Connection = SystemVariables.Conn;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strError = SQLDBException.Message.ToString();
            }

            sqlConn.Close();
            sqlComm.Dispose();
        }

        // Update Version

        public void UpdateDbVersionData()
        {
            string strSQL = "UPDATE DbVersion set CurrentVersion = " + DbDefination.strDbVersion;
            string strError;
            SqlCommand sqlComm = null;
            this.Connection = SystemVariables.Conn;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlComm.ExecuteNonQuery();
            }
            catch (SqlException SQLDBException)
            {
                strError = SQLDBException.Message.ToString();
            }

            sqlConn.Close();
            sqlComm.Dispose();
        }

        // Check Record Count of Db Version 

        private bool HasDbVersionData()
        {
            string strSQL = "select Count(*) AS RECCNT from DbVersion ";
            string strError;
            int intRecCnt = 0;

            SqlCommand sqlComm = null;
            SqlDataReader sqlReader = null;
            this.Connection = SystemVariables.Conn;

            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                sqlComm = new SqlCommand(strSQL, sqlConn);
                sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.Read())
                    intRecCnt = GeneralFunctions.fnInt32(sqlReader["RECCNT"].ToString());
            }
            catch (SqlException SQLDBException)
            {
                // Table not found
                strError = SQLDBException.Message.ToString();
            }

            if (sqlReader != null)
                sqlReader.Close();
            sqlConn.Close();
            sqlComm.Dispose();

            if (intRecCnt > 0)
                return true;
            else
                return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            marqueeProgressBarControl1.Visibility = Visibility.Visible;
            
            btnUpdate.IsEnabled = false;
            btnCancel.IsEnabled = false;
            
            labelControl2.Visibility = Visibility.Visible;
            marqueeProgressBarControl1.Visibility = Visibility.Visible;

            //this.marqueeProgressBarControl1.Refresh();
            Cursor = Cursors.Wait;
            try
            {

                DbDefination.SetAllArrayList(GeneralFunctions.fnDouble(strVersion));
                DbDefination.SetMultilingualArrayList();

                if (this.strDbVersionResult.ToUpper() == "OLD")
                {

                    UpdateDatabase(DbDefination.arlTables, DbDefination.arlLanguageTables);
                }

                if (HasDbVersionData())
                    UpdateDbVersionData();
                else
                    InsertDbVersionData();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }


            DialogResult = true;
        }
    }
}
