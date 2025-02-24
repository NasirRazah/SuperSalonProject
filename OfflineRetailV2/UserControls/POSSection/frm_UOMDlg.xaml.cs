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
    /// Interaction logic for frm_UOMDlg.xaml
    /// </summary>
    public partial class frm_UOMDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private frm_UOMBrw frmBrowse;
        private int intID;
        private int intNewID;
        private int intPID;
        private bool boolControlChanged;
        private int intPrevCount = 0;

        public frm_UOMDlg()
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
        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }

        public frm_UOMBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_UOMBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void ShowData()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowUOMRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtDesc.Text = dr["Description"].ToString();
                spnCount.Text = dr["PackageCount"].ToString();
                txtPrice.Text = GeneralFunctions.fnDouble(dr["UnitPrice"].ToString()).ToString();
                intPrevCount = GeneralFunctions.fnInt32(spnCount.Text);
                if (dr["IsDefault"].ToString() == "N")
                {
                    chkDefault.IsChecked = false;
                }
                else
                {
                    chkDefault.IsChecked = true;
                }
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateUOMCount(intPID, GeneralFunctions.fnInt32(spnCount.Text));
        }

        private bool IsValidAll()
        {
            if (txtDesc.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Description);
                GeneralFunctions.SetFocus(txtDesc);
                return false;
            }

            if (GeneralFunctions.fnDouble(txtPrice.Text) <= 0.00)
            {
                DocMessage.MsgEnter(Properties.Resources.Unit_Price);
                GeneralFunctions.SetFocus(txtPrice);
                return false;
            }

            if (spnCount.Text == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Pieces_Per_Unit);
                GeneralFunctions.SetFocus(spnCount);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (intPrevCount != GeneralFunctions.fnInt32(spnCount.Text))))
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Unit_of_Measure);
                    GeneralFunctions.SetFocus(spnCount);
                    return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;
            objProduct.UOMProductID = intPID;
            objProduct.UOMDescription = txtDesc.Text.Trim();
            objProduct.UOMUnitPrice =  GeneralFunctions.fnDouble(txtPrice.Text);
            objProduct.UOMPackageCount = GeneralFunctions.fnInt32(spnCount.Text);
            if (chkDefault.IsChecked == true)
            {
                objProduct.UOMIsDefault = "Y";
            }
            else
            {
                objProduct.UOMIsDefault = "N";
            }
            objProduct.ID = intID;
            if (intID == 0)
            {
                strError = objProduct.InsertUOMData();
                NewID = objProduct.ID;
            }
            else
            {
                strError = objProduct.UpdateUOMData();
            }
            if (strError == "")
            {
                if (objProduct.UOMIsDefault == "Y")
                {
                    if (intID == 0) ResetDefault(NewID);
                    else ResetDefault(intID);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResetDefault(int UOMID)
        {
            PosDataObject.Product objProduct1 = new PosDataObject.Product();
            objProduct1.Connection = SystemVariables.Conn;
            string strtemp = objProduct1.ResetDefaultFlag(UOMID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();

            if (intID == 0)
            {
                Title.Text = "Add Unit of Measure";
            }
            else
            {
                Title.Text = "Edit Unit of Measure";
                ShowData();
            }
            boolControlChanged = false;
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

        private void TxtDesc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkDefault_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }





        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }






        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
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
