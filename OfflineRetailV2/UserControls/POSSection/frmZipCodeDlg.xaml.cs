using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for frmZipCodeDlg.xaml
    /// </summary>
    public partial class frmZipCodeDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frmZipCodeBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private string strPrevZip;
        private bool boolControlChanged;
        private bool blForceAdd;
        private string strForceZip;
        private string strForceCity;
        private string strForceState;
        public frmZipCodeDlg()
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

        public bool ForceAdd
        {
            get { return blForceAdd; }
            set { blForceAdd = value; }
        }

        public string ForceZip
        {
            get { return strForceZip; }
            set { strForceZip = value; }
        }

        public string ForceCity
        {
            get { return strForceCity; }
            set { strForceCity = value; }
        }

        public string ForceState
        {
            get { return strForceState; }
            set { strForceState = value; }
        }

        public frmZipCodeBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frmZipCodeBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private bool IsValidAll()
        {


            if (!blForceAdd)
            {
                if (txtZip.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Zip_Code);
                    GeneralFunctions.SetFocus(txtZip);
                    return false;
                }

                if (txtCity.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.City );
                    GeneralFunctions.SetFocus(txtCity);
                    return false;
                }

                if (txtState.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.State );
                    GeneralFunctions.SetFocus(txtState);
                    return false;
                }

                if (intID == 0)
                {
                    if (DuplicateCount() == 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Duplicate_Zip_Code );
                        GeneralFunctions.SetFocus(txtZip);
                        return false;
                    }
                }
                else
                {
                    if (strPrevZip != txtZip.Text.Trim())
                    {
                        if (DuplicateCount() == 1)
                        {
                            DocMessage.MsgInformation(Properties.Resources.Duplicate_Zip_Code );
                            GeneralFunctions.SetFocus(txtZip);
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (txtCity.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.City );
                    GeneralFunctions.SetFocus(txtCity);
                    return false;
                }

                if (cmbState.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.State );
                    GeneralFunctions.SetFocus(cmbState);
                    return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            objZip.LoginUserID = SystemVariables.CurrentUserID;

            objZip.City = txtCity.Text.Trim();
            if (!blForceAdd)
            {
                objZip.ZipCode = txtZip.Text.Trim();
                objZip.State = txtState.Text.Trim();
            }
            else
            {
                objZip.ZipCode = strForceZip;
                objZip.State = cmbState.Text.Trim();
            }
            objZip.ID = intID;
            if (intID == 0)
            {
                strError = objZip.InsertData();
                NewID = objZip.ID;
            }
            else
            {
                strError = objZip.UpdateData();

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



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }
        public void ShowData()
        {
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objZip.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtZip.Text = dr["ZIP"].ToString();
                txtCity.Text = dr["CITY"].ToString();
                txtState.Text = dr["STATE"].ToString();
            }
            dbtbl.Dispose();
            strPrevZip = txtZip.Text;
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (!blForceAdd)
                    {
                        BrowseForm.FetchData();
                        Close();
                    }
                    else
                    {
                        strForceCity = txtCity.Text.Trim();
                        strForceState = cmbState.Text.Trim();
                        DialogResult = true;
                        CloseKeyboards();
                        Close();
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Zip_code;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Zip_code ;
            }
            if (blForceAdd)
            {
                label1.Visibility = Visibility.Collapsed;
                txtZip.Visibility = Visibility.Collapsed;
                txtState.Visibility = Visibility.Collapsed;
                lbForceZip.Visibility = Visibility.Visible;
                cmbState.Visibility = Visibility.Visible;
                lbForceZip.Text = Properties.Resources.Zipcode  + " '" + strForceZip + "' " + Properties.Resources.has_not_been_stored_yet ;
            }
            else
            {
                label1.Visibility = Visibility.Visible;
                txtZip.Visibility = Visibility.Visible;
                txtState.Visibility = Visibility.Visible;
                lbForceZip.Visibility = Visibility.Collapsed;
                cmbState.Visibility = Visibility.Collapsed;
            }
            boolControlChanged = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            boolControlChanged = true;
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
                            if (!blForceAdd)
                            {
                                BrowseForm.FetchData();
                                Close();
                            }
                            else
                            {
                                strForceCity = txtCity.Text.Trim();
                                strForceState = cmbState.Text.Trim();
                                DialogResult = true;
                                Close();
                            }
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
        private int DuplicateCount()
        {
            PosDataObject.Zip objZip = new PosDataObject.Zip();
            objZip.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objZip.DuplicateCount(txtZip.Text.Trim());
        }

        private void CmbState_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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

                var location = (sender as System.Windows.Controls.TextBox /*DevExpress.Xpf.Editors.TextEdit*/).PointToScreen(new Point(0, 0));
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
