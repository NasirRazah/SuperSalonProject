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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frmHolidayDlg.xaml
    /// </summary>
    public partial class frmHolidayDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frmHolidayDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frmHolidayBrwUC frmHolidayBrw;

        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private DateTime initDate = Convert.ToDateTime(null);
        private String strAddEdit;

        public String AddEdit
        {
            get { return strAddEdit; }
            set { strAddEdit = value; }
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

        public frmHolidayBrwUC BrowseForm
        {
            get
            {
                return frmHolidayBrw;
            }
            set
            {
                frmHolidayBrw = value;
            }
        }

        private void ShowData()
        {
            PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
            objHolidays.Connection = SystemVariables.Conn;
            objHolidays.ID = intID;
            DataTable dbtbl = new DataTable();
            dbtbl = objHolidays.FetchRecord();
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtOccation.Text = dr["HolidayDesc"].ToString();
                dtHoliday.EditValue = GeneralFunctions.fnDate(dr["HolidayDate"].ToString());
            }
            initDate = dtHoliday.DateTime.Date;
            dbtbl.Dispose();
        }

        private bool ValidAllFields()
        {
            if (dtHoliday.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Date);
                GeneralFunctions.SetFocus(dtHoliday);
                return false;
            }
            if (txtOccation.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Occasion);
                GeneralFunctions.SetFocus(txtOccation);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateHoliday())
                {
                    DocMessage.MsgInformation(Properties.Resources.This_Holiday_already_exists);
                    GeneralFunctions.SetFocus(dtHoliday);
                    return false;
                }
            }

            if (intID != 0)
            {
                if (initDate != dtHoliday.DateTime.Date)
                {
                    if (DuplicateHoliday())
                    {
                        DocMessage.MsgInformation(Properties.Resources.This_Holiday_already_exists);
                        GeneralFunctions.SetFocus(dtHoliday);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            if (ValidAllFields())
            {
                boolControlChanged = false;
                PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
                objHolidays.Connection = SystemVariables.Conn;
                objHolidays.ID = intID;
                objHolidays.HolidayDesc = txtOccation.Text.Trim();
                objHolidays.HolidayDate = dtHoliday.DateTime.Date;
                objHolidays.LoginID = SystemVariables.CurrentUserID;

                if (intID == 0)
                {
                    strError = objHolidays.InsertData();
                    intNewID = GetMaxID();
                }
                else
                {
                    strError = objHolidays.UpdateData();
                    intNewID = intID;
                }

                if (strError != "")
                {
                    return false;
                }
                else
                    return true;
            }
            else
                return false;
        }

        private bool DuplicateHoliday()
        {
            PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
            objHolidays.Connection = SystemVariables.Conn;
            objHolidays.HolidayDate = dtHoliday.DateTime.Date;
            int RecCount = objHolidays.DuplicateHoliday();
            if (RecCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetMaxID()
        {
            PosDataObject.Holidays objHolidays = new PosDataObject.Holidays();
            objHolidays.Connection = SystemVariables.Conn;
            return objHolidays.MaxID();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Holiday;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Holiday;
            }
            dtHoliday.EditValue = DateTime.Today.Date;
            if (intID > 0)
            {
                ShowData();
            }
            boolControlChanged = false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                frmHolidayBrw.Year = GeneralFunctions.fnDate(dtHoliday.EditValue.ToString()).Year;
                frmHolidayBrw.PopulateYear();
                frmHolidayBrw.FetchGridData();
                CloseKeyboards();
                Close();
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
                    if (SaveData())
                    {
                        boolControlChanged = false;
                        frmHolidayBrw.Year = GeneralFunctions.fnDate(dtHoliday.EditValue.ToString()).Year;
                        frmHolidayBrw.PopulateYear();
                        frmHolidayBrw.FetchGridData();
                    }
                    else e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void DtHoliday_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtOccation_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void DtHoliday_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
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
