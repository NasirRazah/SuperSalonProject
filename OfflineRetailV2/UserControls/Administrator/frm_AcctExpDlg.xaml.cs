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
using System.IO;
using System.Net;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_AcctExpDlg.xaml
    /// </summary>
    public partial class frm_AcctExpDlg : Window
    {
        public frm_AcctExpDlg()
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
            Title.Text = Properties.Resources.Accounting_Export;
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
        }

        private bool ValidAll()
        {
            if ((dtFrom.EditText == "") || (dtTo.EditText == ""))
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Date);
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }

            return true;
        }

        private string GLExportFilePath()
        {
            string Fnmae = "GL_Export_" + dtFrom.DateTime.ToString("MMddyyyy") + "_" + dtTo.DateTime.ToString("MMddyyyy") + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                    DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                    DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".csv";
            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\GLExport";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\GLExport";

            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + Fnmae;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + Fnmae;
            }
            return strfilename;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fl = "";
                Cursor = Cursors.Wait;
                if (ValidAll())
                {
                    DateTime d1 = new DateTime(dtFrom.DateTime.Year, dtFrom.DateTime.Month, dtFrom.DateTime.Day, 0, 0, 0);
                    DateTime d2 = new DateTime(dtTo.DateTime.Year, dtTo.DateTime.Month, dtTo.DateTime.Day, 0, 0, 0);

                    DateTime d3 = new DateTime(dtFrom.DateTime.Year, dtFrom.DateTime.Month, dtFrom.DateTime.Day, 0, 0, 1);
                    DateTime d4 = new DateTime(dtTo.DateTime.Year, dtTo.DateTime.Month, dtTo.DateTime.Day, 23, 59, 59);

                    PosDataObject.GLedger objGL = new PosDataObject.GLedger();
                    objGL.Connection = SystemVariables.Conn;
                    objGL.ExecuteGLAccounting(d1, d2, d3, d4, Settings.TerminalName);

                    DataTable dtbl = objGL.FetchGLExportData(Settings.TerminalName);

                    if (dtbl.Rows.Count > 0)
                    {
                        fl = GLExportFilePath();

                        StreamWriter writer = new StreamWriter(fl);
                        StringBuilder builder = new StringBuilder();
                        try
                        {
                            int prevlength = 0;
                            string sepChar = ",";

                            string sep = "";

                            foreach (DataColumn dc in dtbl.Columns)
                            {
                                builder.Append(sep).Append(dc.ColumnName);
                                sep = sepChar;
                            }
                            writer.WriteLine(builder.ToString());
                            prevlength = builder.Length;


                            foreach (DataRow drE in dtbl.Rows)
                            {
                                sep = "";
                                //builder.Remove(0, builder.Length);
                                foreach (DataColumn dc1 in dtbl.Columns)
                                {
                                    builder.Append(sep).Append(drE[dc1.ColumnName]);
                                    sep = sepChar;

                                }
                                writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                                prevlength = builder.Length;

                            }
                            writer.Close();

                        }
                        catch
                        {

                        }

                        DocMessage.MsgInformation(Properties.Resources.GL_Accounting_exported_successfully + "." + "Export File" + " : " + fl);
                        try
                        {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            process.StartInfo.FileName = fl;
                            //process.StartInfo.Verb = "Open";
                            //process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                            process.Start();
                        }
                        catch
                        {
                            DocMessage.MsgError(Properties.Resources.Cannot_find_an_application_on_your_system_suitable_for_openning_the_file_with_exported_data);
                        }
                    }
                }
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
