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
    /// Interaction logic for frm_PrinterNameDlg.xaml
    /// </summary>
    public partial class frm_PrinterNameDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_PrinterNameDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string strGroupType;
        private string strPrev;

        public string GroupType
        {
            get { return strGroupType; }
            set { strGroupType = value; }
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

        private void PolulatePrinterType()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dtbl = objSetup.FetchPrinterTypes();
            //cmbPrinterType.ItemsSource = dtbl;
        }


        private bool IsValidAll()
        {

            if (txtDescription.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Printer Name");
                GeneralFunctions.SetFocus(txtDescription);
                return false;
            }

            /*if (cmbPrinterType.EditValue == null)
            {
                DocMessage.MsgInformation("Select Printer Type");
                GeneralFunctions.SetFocus(cmbPrinterType);
                return false;
            }*/


            if ((intID == 0) || ((intID > 0) && (strPrev != txtDescription.Text.Trim())))
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation("Duplicate Printer Name");
                    GeneralFunctions.SetFocus(txtDescription);
                    return false;
                }
            }



            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Setup objDepartment = new PosDataObject.Setup();
            objDepartment.Connection = SystemVariables.Conn;
            objDepartment.LoginUserID = SystemVariables.CurrentUserID;
            objDepartment.PrinterName = txtDescription.Text.Trim();
            //objDepartment.PrinterTypeID = GeneralFunctions.fnInt32(cmbPrinterType.EditValue);
            objDepartment.ID = intID;

            if (intID == 0)
            {
                strError = objDepartment.InsertPrinterData();
                NewID = objDepartment.ID;
            }
            else
            {
                strError = objDepartment.UpdatePrinterData();
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
            PosDataObject.Setup objDepartment = new PosDataObject.Setup();
            objDepartment.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objDepartment.ShowPrinterRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtDescription.Text = dr["PrinterName"].ToString();
                strPrev = txtDescription.Text;
                
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Setup objDepartment = new PosDataObject.Setup();
            objDepartment.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objDepartment.DuplicatePrinterCount(txtDescription.Text.Trim());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PolulatePrinterType();
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = "Add Printer Name";
            }
            else
            {
                Title.Text = "Edit Printer Name";
                ShowData();
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
                    //BrowseForm.FetchData();
                    CloseKeyboards();
                    DialogResult = true;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            DialogResult = false;
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

        private void cmbPrinterType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void cmbPrinterType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
