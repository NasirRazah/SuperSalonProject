using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_ClassDlg.xaml
    /// </summary>
    public partial class frm_ClassDlg : Window
    {
        private frm_ClassBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_ClassDlg()
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

        public frm_ClassBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_ClassBrwUC();
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
                DocMessage.MsgEnter(Properties.Resources.Class_ID);
                GeneralFunctions.SetFocus(txtID);
                return false;
            }
            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Class_ID);
                    GeneralFunctions.SetFocus(txtID);
                    return false;
                }
            }
            return true;
        }

        private int DuplicateCount()
        {
            PosDataObject.Class objClass = new PosDataObject.Class();
            objClass.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objClass.DuplicateCount(txtID.Text.Trim());
        }

        public void ShowData()
        {
            PosDataObject.Class objClass = new PosDataObject.Class();
            objClass.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objClass.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtID.Text = dr["ClassID"].ToString();
                txtDescription.Text = dr["Description"].ToString();
            }

            dbtbl.Dispose();
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Class objClass = new PosDataObject.Class();
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

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtval.Select(txtval.Text.Length + 1, 0);
            fkybrd = new FullKeyboard();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Class;
                txtID.IsEnabled = true;
                GeneralFunctions.SetFocus(txtID);
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Class;
                txtID.IsEnabled = false;
                GeneralFunctions.SetFocus(txtDescription);
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

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
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

        private void Txtval_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            object oldval = e.OldValue;
            object newval = e.NewValue;

            int positiondot = newval.ToString().IndexOf(".");
            string digitbeforedot = newval.ToString().Substring(0, positiondot);
            if (positiondot != -1)
            {
                if (newval.ToString().Substring(positiondot + 1).Length == 1)
                {
                    
                    //txtval.Text = oldval.ToString();
                    txtval.EditValue = oldval.ToString();
                   
                }
            }

        }

        private void Txtval_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Txtval_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key enterKey = e.Key;
            string val = (sender as DevExpress.Xpf.Editors.TextEdit).EditText.ToString();
            //(sender as DevExpress.Xpf.Editors.TextEdit).Text = SetNumericText(enterKey, val);
            (sender as DevExpress.Xpf.Editors.TextEdit).EditValue = SetNumericText(enterKey, val);
        }

        private void Txtval_PreviewKeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private string SetNumericText(Key ky, string prevval)
        {
            string newvalue = "";
            string kyval = "";
            if (ky == Key.D0)
            {
                kyval = "0";
            }
            if (ky == Key.D1)
            {
                kyval = "1";
            }
            if (ky == Key.D2)
            {
                kyval = "2";
            }
            if (ky == Key.D3)
            {
                kyval = "3";
            }
            if (ky == Key.D4)
            {
                kyval = "4";
            }
            if (ky == Key.D5)
            {
                kyval = "5";
            }
            if (ky == Key.D6)
            {
                kyval = "6";
            }
            if (ky == Key.D7)
            {
                kyval = "7";
            }
            if (ky == Key.D8)
            {
                kyval = "8";
            }
            if (ky == Key.D9)
            {
                kyval = "9";
            }

            if (ky == Key.Back)
            {
                kyval = "back";
            }

            if (ky == Key.OemMinus)
            {
                kyval = "-";
            }

            int positiondot = prevval.IndexOf(".");
            if (positiondot != -1)
            {
                string digitbeforedot = prevval.Substring(0, positiondot);
                string decimal1 = prevval.Substring(positiondot + 1, 1);
                string decimal2 = prevval.Substring(positiondot + 2, 1);

                if (kyval == "-")
                {
                    if (prevval.Contains("-"))
                    {
                        newvalue = prevval;
                    }
                    else
                    {
                        newvalue = "-" + prevval;
                    }
                }
                else if (kyval == "back")
                {
                    if ((digitbeforedot == "-0") || (digitbeforedot == "0"))
                    {
                        decimal2 = decimal1;
                        decimal1 = "0";
                        newvalue = digitbeforedot + "." + decimal1 + decimal2;
                    }
                    else
                    {
                        decimal2 = decimal1;
                        decimal1 = digitbeforedot.Substring(digitbeforedot.Length - 1);
                        digitbeforedot = digitbeforedot.Substring(0, digitbeforedot.Length - 1);
                        if ((digitbeforedot == "-") || (digitbeforedot == ""))
                        {
                            digitbeforedot = digitbeforedot + "0";
                        }
                            newvalue = digitbeforedot + "." + decimal1 + decimal2;
                    }
                }
                else
                {
                    if ((digitbeforedot == "-0") || (digitbeforedot == "0"))
                    {
                        digitbeforedot = digitbeforedot.Replace("0", decimal1);
                    }
                    else
                    {
                        digitbeforedot = digitbeforedot + decimal1;
                    }
                    decimal1 = decimal2;
                    decimal2 = kyval;
                    newvalue = digitbeforedot + "." + decimal1 + decimal2;
                }
            }


            return newvalue;
        }

        private void Txtval_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            int positiondot = txtval.Text.ToString().IndexOf(".");
            string digitbeforedot = txtval.Text.ToString().Substring(0, positiondot);
            if (positiondot != -1)
            {
                if (txtval.Text.ToString().Substring(positiondot + 1).Length == 3)
                {
                    txtval.Text = txtval.Text.Substring(0, txtval.Text.Length - 1);
                    txtval.Refresh();
                }
            }
        }
    }
}
