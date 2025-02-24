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
    /// Interaction logic for frm_TenderTypesDlg.xaml
    /// </summary>
    public partial class frm_TenderTypesDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_TenderTypesBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string BFlag = "N";

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

        public frm_TenderTypesBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_TenderTypesBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_TenderTypesDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateGL()
        {
            PosDataObject.GLedger objEmployee = new PosDataObject.GLedger();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objEmployee.LookupGL("");

            cmbGL.ItemsSource = dbtbl;
            cmbGL.DisplayMember = "Lookup";
            cmbGL.ValueMember = "ID";
            dbtbl.Dispose();
            cmbGL.EditValue = null;
        }

        public void PopulateTenderName()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Name");
            dtbl.Rows.Add(new object[] { "Cash" });
            dtbl.Rows.Add(new object[] { "Check" });
            dtbl.Rows.Add(new object[] { "Visa" });
            dtbl.Rows.Add(new object[] { "MasterCard" });
            dtbl.Rows.Add(new object[] { "American Express" });
            dtbl.Rows.Add(new object[] { "Discover" });
            dtbl.Rows.Add(new object[] { "Credit Card" });
            dtbl.Rows.Add(new object[] { "Credit Card - Voice Auth" });
            dtbl.Rows.Add(new object[] { "Credit Card (STAND-IN)" });
            dtbl.Rows.Add(new object[] { "Credit Card - Voice Auth (STAND-IN)" });
            dtbl.Rows.Add(new object[] { "Debit Card" });
            dtbl.Rows.Add(new object[] { "Mercury Gift Card" });
            dtbl.Rows.Add(new object[] { "Precidia Gift Card" });
            dtbl.Rows.Add(new object[] { "Gift Certificate" });
            dtbl.Rows.Add(new object[] { "Store Credit" });
            dtbl.Rows.Add(new object[] { "House Account" });
            dtbl.Rows.Add(new object[] { "Food Stamps" });
            dtbl.Rows.Add(new object[] { "Card" });

            txtName.ItemsSource = dtbl;
            txtName.DisplayMember = "Name";
            txtName.ValueMember = "Name";
            dtbl.Dispose();
            txtName.EditValue = "Cash";
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Tender_Name);
                GeneralFunctions.SetFocus(txtName);
                return false;
            }
            if (txtDisplayAs.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Display_As);
                GeneralFunctions.SetFocus(txtDisplayAs);
                return false;
            }
            if (Settings.CloseoutExport == "Y")
            {
                if (cmbGL.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources.Select_GL_Account);
                    GeneralFunctions.SetFocus(cmbGL);
                    return false;
                }
            }
            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Tender);
                    GeneralFunctions.SetFocus(txtName);
                    return false;
                }
            }
            if ((intID > 0) && (chkEnabled.IsChecked == false))
            {
                if (GeneralFunctions.GetRecordCountForDelete("Tender", "TenderType", intID) > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Can_not_disable_this_tender_type_as_transaction_exists_against_this);
                    GeneralFunctions.SetFocus(chkEnabled);
                    return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            objTenderTypes.LoginUserID = SystemVariables.CurrentUserID;
            objTenderTypes.TypeName = txtName.Text.Trim();
            objTenderTypes.DisplayAs = txtDisplayAs.Text.Trim();
            objTenderTypes.PaymentOrder = GetMaxPaymentOrder() + 1;
            objTenderTypes.ID = intID;
            if (txtName.Text.Trim() == "Cash")
            {
                objTenderTypes.IsOpenCashDrawer = "Y";
            }
            else
            {
                if (chkCashdrawer.IsChecked == true)
                {
                    objTenderTypes.IsOpenCashDrawer = "Y";
                }
                else
                {
                    objTenderTypes.IsOpenCashDrawer = "N";
                }
            }
            if (chkEnabled.IsChecked == true)
            {
                objTenderTypes.TypeEnabled = "Y";
            }
            else
            {
                objTenderTypes.TypeEnabled = "N";
            }


            if (Settings.CloseoutExport == "Y")
            {
                objTenderTypes.LinkGL = GeneralFunctions.fnInt32(cmbGL.EditValue);
            }
            else
            {
                objTenderTypes.LinkGL = 0;
            }

            if (intID == 0)
            {
                strError = objTenderTypes.InsertData();
                NewID = objTenderTypes.ID;
            }
            else
            {
                strError = BFlag == "Y" ? objTenderTypes.UpdateBlankData() : objTenderTypes.UpdateData();
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

        private int GetMaxPaymentOrder()
        {
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            return objTenderTypes.MaxPaymentOrder();
        }

        public void ShowData()
        {
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objTenderTypes.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtName.Text = dr["Name"].ToString();
                txtDisplayAs.Text = dr["DisplayAs"].ToString();
                if (dr["Enabled"].ToString() == "Y")
                {
                    chkEnabled.IsChecked = true;
                }
                else
                {
                    chkEnabled.IsChecked = false;
                }
                if (dr["IsOpenCashdrawer"].ToString() == "Y")
                {
                    chkCashdrawer.IsChecked = true;
                }
                else
                {
                    chkCashdrawer.IsChecked = false;
                }

                BFlag = dr["IsBlank"].ToString();

                if (Settings.CloseoutExport == "Y")
                {
                    if (GeneralFunctions.fnInt32(dr["LinkGL"].ToString()) > 0)
                    {
                        cmbGL.EditValue = dr["LinkGL"].ToString();
                    }
                }
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objTenderTypes.DuplicateCount(txtName.Text.Trim());
        }

        private void TxtName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            txtDisplayAs.Text = txtName.Text;
            if (txtName.Text == "Cash")
            {
                chkCashdrawer.IsEnabled = false;
                chkCashdrawer.IsChecked = true;
            }
            else
            {
                chkCashdrawer.IsEnabled = true;
            }
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
            PopulateTenderName();
            if (Settings.CloseoutExport == "Y")
            {
                cmbGL.Visibility = lbGL.Visibility = Visibility.Visible;
                PopulateGL();
            }
            else
            {
                cmbGL.Visibility = lbGL.Visibility = Visibility.Hidden;
            }
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Tender_Type;
                txtName.IsEnabled = true;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Tender_Type;
                txtName.IsEnabled = false;
                ShowData();
            }
            if (txtName.Text == "Cash")
            {
                chkCashdrawer.IsEnabled = false;
                chkCashdrawer.IsChecked = true;
            }
            else chkCashdrawer.IsEnabled = true;

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

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkEnabled_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
