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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_GLDlg.xaml
    /// </summary>
    public partial class frm_GLDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_GLBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;

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

        public frm_GLBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_GLBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_GLDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_GL_Account;
                txtID.IsEnabled = true;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_GL_Account;
                ShowData();
                txtID.IsEnabled = false;
            }
            boolControlChanged = false;
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

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private bool IsValidAll()
        {
            if (txtID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.GL_Code);
                GeneralFunctions.SetFocus(txtID);
                return false;
            }

            if (txtDescription.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.GL_Description);
                GeneralFunctions.SetFocus(txtDescription);
                return false;
            }

            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_GL_Code);
                    GeneralFunctions.SetFocus(txtID);
                    return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.GLedger objClass = new PosDataObject.GLedger();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.GLCode = txtID.Text.Trim();
            objClass.GLDescription = txtDescription.Text.Trim();
            objClass.ID = intID;
            if (intID == 0)
            {
                strError = objClass.InsertGL();
                NewID = objClass.ID;
            }
            else
            {
                strError = objClass.UpdateGL();
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

        public void ShowData()
        {
            PosDataObject.GLedger objClass = new PosDataObject.GLedger();
            objClass.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objClass.ShowGLRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtID.Text = dr["GlCode"].ToString();
                txtDescription.Text = dr["GLDescription"].ToString();
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.GLedger objClass = new PosDataObject.GLedger();
            objClass.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objClass.DuplicateCount(txtID.Text.Trim());
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
