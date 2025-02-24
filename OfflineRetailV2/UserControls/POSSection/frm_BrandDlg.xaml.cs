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
    /// Interaction logic for frm_BrandDlg.xaml
    /// </summary>
    public partial class frm_BrandDlg : Window
    {

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private frm_BrandBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;

        public frm_BrandDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public frm_BrandBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_BrandBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }


        private bool IsValidAll()
        {
            if (txtID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Family_ID);
                GeneralFunctions.SetFocus(txtID);
                return false;
            }
            if (txtDescription.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Family_Name);
                GeneralFunctions.SetFocus(txtDescription);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Family_ID);
                    GeneralFunctions.SetFocus(txtID);
                    return false;
                }
            }
            return true;
        }

        private int DuplicateCount()
        {
            PosDataObject.Brand objClass = new PosDataObject.Brand();
            objClass.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objClass.DuplicateCount(txtID.Text.Trim());
        }

        public void ShowData()
        {
            PosDataObject.Brand objClass = new PosDataObject.Brand();
            objClass.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objClass.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtID.Text = dr["BrandID"].ToString();
                txtDescription.Text = dr["BrandDescription"].ToString();
            }

            dbtbl.Dispose();
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Brand objClass = new PosDataObject.Brand();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.GeneralCode = txtID.Text.Trim();
            objClass.GeneralDesc = txtDescription.Text.Trim();
            objClass.ID = intID;
            if (intID == 0)
            {
                strError = objClass.InsertData();
                NewID = objClass.ID;
            }
            else
            {
                strError = objClass.UpdateData();
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


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.FetchData();
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
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
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Family;
                txtID.IsEnabled = true;
                GeneralFunctions.SetFocus(txtID);
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Family;
                txtID.IsEnabled = false;
                ShowData();
                GeneralFunctions.SetFocus(txtDescription);
            }
            boolControlChanged = false;
        }

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           

            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }
    }
}
