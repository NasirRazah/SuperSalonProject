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
    /// Interaction logic for frm_ProductTypeDlg.xaml
    /// </summary>
    public partial class frm_PrinterTypeDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private frm_PrinterTypeBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string PrevProfile = "";

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

        public frm_PrinterTypeBrwUC BrowseForm
        {
            get
            {
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_PrinterTypeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private bool IsValidAll()
        {
            if (txtProfile.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Printer Type");
                GeneralFunctions.SetFocus(txtProfile);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation("Duplicate Printer Type");
                    GeneralFunctions.SetFocus(txtProfile);
                    return false;
                }
            }
            else
            {
                if (PrevProfile != txtProfile.Text.Trim())
                {
                    if (DuplicateCount() == 1)
                    {
                        DocMessage.MsgInformation("Duplicate Printer Type");
                        GeneralFunctions.SetFocus(txtProfile);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Setup objSecurity = new PosDataObject.Setup();
            objSecurity.Connection = SystemVariables.Conn;
            objSecurity.LoginUserID = SystemVariables.CurrentUserID;
            objSecurity.PrinterType = txtProfile.Text.Trim();
            objSecurity.ID = intID;
            if (intID == 0)
            {
                strError = objSecurity.InsertPrinterTypes();
                NewID = objSecurity.ID;
            }
            else
            {
                strError = objSecurity.UpdatePrinterTypes();
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
            PosDataObject.Setup objSecurity = new PosDataObject.Setup();
            objSecurity.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objSecurity.ShowPrinterTypesRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtProfile.Text = dr["PrinterType"].ToString();
            }
            PrevProfile = txtProfile.Text.Trim();
        }

        private int DuplicateCount()
        {
            PosDataObject.Setup objSecurity = new PosDataObject.Setup();
            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objSecurity.DuplicatePrinterTypesCount(txtProfile.Text.Trim());
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
                Title.Text = "Add Printer Type";
            }
            else
            {
                Title.Text = "Edit Printer Type";
                ShowData();
            }
            GeneralFunctions.SetFocus(txtProfile);
            boolControlChanged = false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
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

        private void TxtProfile_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
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

        private void Full_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if ((!IsAboutFullKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
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
    }
}
