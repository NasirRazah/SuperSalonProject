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
    /// Interaction logic for frmShiftDlg.xaml
    /// </summary>
    public partial class frmShiftDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frmShiftDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frmShiftBrwUC frmShiftBrw;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string initShift = "";
        private string strShiftStatus = "";

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

        public frmShiftBrwUC BrowseForm
        {
            get
            {
                return frmShiftBrw;
            }
            set
            {
                frmShiftBrw = value;
            }
        }

        private bool ValidAllFields()
        {
            if (txtShift.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Shift_Name);
                GeneralFunctions.SetFocus(txtShift);
                return false;
            }
            if (timeStart.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Start_Time);
                GeneralFunctions.SetFocus(timeStart);
                return false;
            }
            if (timeEnd.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.End_Time);
                GeneralFunctions.SetFocus(timeEnd);
                return false;
            }
            if ((timeStart.DateTime.Hour == timeEnd.DateTime.Hour)
                && (timeStart.DateTime.Minute == timeEnd.DateTime.Minute))
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Shift_Time_range);
                GeneralFunctions.SetFocus(timeStart);
                return false;
            }

            if (intID == 0)
            {
                if (DuplicateShift())
                {
                    DocMessage.MsgInformation(Properties.Resources.This_Shift_already_exists);
                    GeneralFunctions.SetFocus(txtShift);
                    return false;
                }
            }
            if (intID != 0)
            {
                if (initShift != txtShift.Text.ToUpper())
                {
                    if (DuplicateShift())
                    {
                        DocMessage.MsgInformation(Properties.Resources.This_Shift_already_exists);
                        GeneralFunctions.SetFocus(txtShift);
                        return false;
                    }
                }
            }

            return true;
        }

        private bool DuplicateShift()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            objShift.ShiftName = txtShift.Text.Trim();
            int RecCount = objShift.DuplicateShift();
            if (RecCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool SaveData()
        {

            string strError = "";
            if (ValidAllFields())
            {

                boolControlChanged = false;
                PosDataObject.Shift objShift = new PosDataObject.Shift();
                objShift.Connection = SystemVariables.Conn;
                objShift.ID = intID;
                objShift.ShiftName = txtShift.Text.Trim();

                objShift.StartTime = timeStart.EditText;
                objShift.EndTime = timeEnd.EditText;
                objShift.ShiftDuration = GetShiftDuratin();
                objShift.ShiftStatus = strShiftStatus;

                objShift.LoginID = SystemVariables.CurrentUserID;

                if (intID == 0)
                {
                    strError = objShift.InsertData();
                    intNewID = GetMaxID();
                }
                else
                {
                    strError = objShift.UpdateData();
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

        private int GetShiftDuratin()
        {
            int totalDuration = 0;

            string ShiftStart = timeStart.EditText;
            string ShiftEnd = timeEnd.EditText;

            string strSampm = GeneralFunctions.AMPM(ShiftStart);
            string strEampm = GeneralFunctions.AMPM(ShiftEnd);

            int SShr = 0;
            int SSmin = 0;
            int SEhr = 0;
            int SEmin = 0;

            SShr = GeneralFunctions.GetHour(ShiftStart);
            SEhr = GeneralFunctions.GetHour(ShiftEnd);

            SSmin = GeneralFunctions.GetMinute(ShiftStart);
            SEmin = GeneralFunctions.GetMinute(ShiftEnd);

            if ((strSampm == "PM") && (strEampm == "AM"))
            {
                totalDuration = ((SEhr + 24) * 60 + SEmin) - (SShr * 60 + SSmin);
            }

            if ((strSampm == "AM") && (strEampm == "AM"))
            {
                if (SEhr >= SShr)
                {
                    totalDuration = (SEhr * 60 + SEmin) - (SShr * 60 + SSmin);
                }
                else
                {
                    totalDuration = ((SEhr + 24) * 60 + SEmin) - (SShr * 60 + SSmin);
                }

            }

            if ((strSampm == "PM") && (strEampm == "PM"))
            {
                if (SEhr >= SShr)
                {
                    totalDuration = (SEhr * 60 + SEmin) - (SShr * 60 + SSmin);
                }
                else
                {
                    totalDuration = ((SEhr + 24) * 60 + SEmin) - (SShr * 60 + SSmin);
                }

            }
            if ((strSampm == "AM") && (strEampm == "PM"))
            {
                totalDuration = (SEhr * 60 + SEmin) - (SShr * 60 + SSmin);
            }
            return totalDuration;

        }

        private int GetMaxID()
        {
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            return objShift.MaxID();
        }

        private void ShowData()
        {
            string ShiftStart = "";
            string ShiftEnd = "";
            PosDataObject.Shift objShift = new PosDataObject.Shift();
            objShift.Connection = SystemVariables.Conn;
            objShift.ID = intID;
            DataTable dbtbl = new DataTable();
            dbtbl = objShift.FetchRecord();
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtShift.Text = dr["ShiftName"].ToString();
                ShiftStart = dr["StartTime"].ToString();
                ShiftEnd = dr["EndTime"].ToString();
                strShiftStatus = dr["ShiftStatus"].ToString();
            }
            initShift = txtShift.Text.ToUpper();
            string strSampm = GeneralFunctions.AMPM(ShiftStart);
            string strEampm = GeneralFunctions.AMPM(ShiftEnd);

            int SShr = 0;
            int SSmin = 0;
            int SEhr = 0;
            int SEmin = 0;

            SShr = GeneralFunctions.GetHour(ShiftStart);
            SEhr = GeneralFunctions.GetHour(ShiftEnd);

            SSmin = GeneralFunctions.GetMinute(ShiftStart);
            SEmin = GeneralFunctions.GetMinute(ShiftEnd);

            timeStart.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, SShr, SSmin, 0);
            timeEnd.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, SEhr, SEmin, 0);
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                BrowseForm.FetchGridData();
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
                        BrowseForm.FetchGridData();
                    }
                    else  e.Cancel = true;
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
                Title.Text = Properties.Resources.Add_Shift;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Shift;
            }
            timeStart.EditValue = DateTime.Now;
            timeEnd.EditValue = DateTime.Now;
            if (intID > 0)
            {
                ShowData();
            }
            boolControlChanged = false;
        }

        private void TxtShift_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
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
    }
}
