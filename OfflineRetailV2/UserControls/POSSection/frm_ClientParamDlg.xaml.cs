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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_ClientParamDlg.xaml
    /// </summary>
    public partial class frm_ClientParamDlg : Window
    {
        private bool boolControlChanged;
        private int intID;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_ClientParamDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void ShowData()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objSetup.FetchClientParamData();
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtParam1.Text = dr["ClientParam1"].ToString();
                txtParam2.Text = dr["ClientParam2"].ToString();
                txtParam3.Text = dr["ClientParam3"].ToString();
                txtParam4.Text = dr["ClientParam4"].ToString();
                txtParam5.Text = dr["ClientParam5"].ToString();
            }
            dbtbl.Dispose();
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            objSetup.LoginUserID = SystemVariables.CurrentUserID;
            objSetup.ClientParam1 = txtParam1.Text.Trim();
            objSetup.ClientParam2 = txtParam2.Text.Trim();
            objSetup.ClientParam3 = txtParam3.Text.Trim();
            objSetup.ClientParam4 = txtParam4.Text.Trim();
            objSetup.ClientParam5 = txtParam5.Text.Trim();

            if (intID == 0)
            {
                strError = objSetup.InsertClientParamData();
            }
            else
            {
                strError = objSetup.UpdateClientParamData();
            }
            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IsExistSetupRecord()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            intID = objSetup.IsExistSetupData();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {

                    if (SaveData())
                    {
                        boolControlChanged = false;
                    }
                    else
                        e.Cancel = true;

                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();

            Title.Text = Properties.Resources.Dynamic_CRM_Parameters;
            IsExistSetupRecord();
            if (intID == 1)
            {
                ShowData();
            }
            GeneralFunctions.SetFocus(txtParam1);
            boolControlChanged = false;
        }

        private void TxtParam1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                CloseKeyboards();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
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

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }
    }
}
