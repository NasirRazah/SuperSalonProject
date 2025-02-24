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
using Microsoft.Win32;
using System.IO;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_SelectImportFile.xaml
    /// </summary>
    public partial class frm_SelectImportFile : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_SelectImportFile()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private string strImportFile;
        private string strImportFileSeparator;
        private bool blImportFileColumnHeader;
        public string ImportFile
        {
            get { return strImportFile; }
            set { strImportFile = value; }
        }

        public string ImportFileSeparator
        {
            get { return strImportFileSeparator; }
            set { strImportFileSeparator = value; }
        }

        public bool ImportFileColumnHeader
        {
            get { return blImportFileColumnHeader; }
            set { blImportFileColumnHeader = value; }
        }

        private void OnCloseCOmmand(object obj)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            rbTab.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidAll())
            {
                strImportFile = mem_location.Text.Trim();
                blImportFileColumnHeader = chkColumn.IsChecked == true? true : false;
                strImportFileSeparator = GetSeparator();
                CloseKeyboards();
                DialogResult = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            DialogResult = false;
        }

        private bool ValidAll()
        {
            if (mem_location.Text.Trim().Length == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Select_Import_File);
                GeneralFunctions.SetFocus(btnBrowse);
                return false;
            }
            if (mem_location.Text.Trim().Length > 0)
            {
                if (!Directory.GetParent(mem_location.Text.Trim()).Exists)
                {
                    DocMessage.MsgInformation(Properties.Resources.Invalid_File_Path);
                    GeneralFunctions.SetFocus(mem_location);
                    return false;
                }
            }

            return true;
        }

        private string GetSeparator()
        {
            if (rbComma.IsChecked == true) return ",";
            else if (rbTab.IsChecked == true) return "\t";
            else if (rbSpace.IsChecked == true) return " ";
            else return txtChar.Text;
        }

        private void RbComma_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCustom.IsChecked == true)
            {
                txtChar.IsEnabled = true;
            }
            else
            {
                txtChar.IsEnabled = false;
            }
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Export File";
            op.Filter = "txt files(*.csv) | (*.txt) | All files(*.*) | *.* ";
            op.DefaultExt = "csv";
            op.CheckFileExists = false;
            op.FilterIndex = 1;
            
            if (op.ShowDialog() == true)
            {
                string strFilename = "";
                strFilename = op.FileName;
                mem_location.Text = strFilename;
            }
        }
    }
}
