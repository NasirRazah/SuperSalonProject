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
using System.IO.Ports;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_COMPrinterDlg.xaml
    /// </summary>
    public partial class frm_COMPrinterDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_COMPrinterDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private frm_GeneralSetupDlg fBrw;

        private int intID;
        private int intNewID;
        private bool boolControlChanged;

        private string strPrevNo;
        private string strPrevName;

        public frm_GeneralSetupDlg BrwF
        {
            get { return fBrw; }
            set { fBrw = value; }
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

        private void LoadAvailablePorts()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbPort.Items.Add(s);
            }

        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Setup objport = new PosDataObject.Setup();
            objport.Connection = SystemVariables.Conn;
            objport.LoginUserID = SystemVariables.CurrentUserID;
            objport.COMFormatNo = txtFNo.Text.Trim();
            objport.COMFormatName = txtFName.Text.Trim();
            objport.COMPortName = cmbPort.Text.Trim();
            objport.COMPrinterCommand = memTemplate.Text;
            objport.COMQty = GeneralFunctions.fnInt32(numQty.Text);
            objport.COMWrap = GeneralFunctions.fnInt32(numWrap.Text);
            objport.COMFont = cmbFont.Text.Trim();

            objport.ID = intID;

            if (intID == 0)
            {
                strError = objport.InsertCOMPrinterCommand();
                intNewID = objport.ID;
            }
            else
            {
                strError = objport.UpdateCOMPrinterCommand();
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
            dbtbl = objDepartment.ShowCOMPrinterCommand(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtFNo.Text = dr["FormatNo"].ToString();
                txtFName.Text = dr["FormatName"].ToString();
                cmbPort.Text = dr["PortName"].ToString();
                memTemplate.Text = dr["PrinterCommand"].ToString();
                numQty.Text = dr["Qty"].ToString();
                numWrap.Text = dr["WordWrap"].ToString();
                cmbFont.Text = dr["FontType"].ToString();
            }
            dbtbl.Dispose();
            strPrevNo = txtFNo.Text;
            strPrevName = txtFName.Text;
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            LoadAvailablePorts();

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_COM_Label_Printer;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_COM_Label_Printer;
                ShowData();
            }

            boolControlChanged = false;
        }

        private bool IsValidAll()
        {
            if (txtFNo.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Format_No);
                GeneralFunctions.SetFocus(txtFNo);
                return false;
            }

            if (cmbPort.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Port);
                GeneralFunctions.SetFocus(cmbPort);
                return false;
            }

            if (memTemplate.Text == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Printer_Command);
                GeneralFunctions.SetFocus(memTemplate);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (strPrevNo != txtFNo.Text.Trim())))
            {
                if (DuplicateFormatNo() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Format_No);
                    GeneralFunctions.SetFocus(txtFNo);
                    return false;
                }
            }

            if (txtFName.Text.Trim() != "")
            {
                if ((intID == 0) || ((intID > 0) && (strPrevName != txtFName.Text.Trim())))
                {
                    if (DuplicateFormatName() == 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Duplicate_Format_Name);
                        GeneralFunctions.SetFocus(txtFName);
                        return false;
                    }
                }
            }

            return true;
        }

        private int DuplicateFormatNo()
        {
            PosDataObject.Setup objDepartment = new PosDataObject.Setup();
            objDepartment.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objDepartment.DuplicateCOMFormatNo(txtFNo.Text.Trim());
        }

        private int DuplicateFormatName()
        {
            PosDataObject.Setup objDepartment = new PosDataObject.Setup();
            objDepartment.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objDepartment.DuplicateCOMFormatName(txtFName.Text.Trim());
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    fBrw.FetchPortCommand();
                    CloseKeyboards();
                    DialogResult = true;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            DialogResult = false;
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
                            fBrw.FetchPortCommand();
                            DialogResult = true;
                            boolControlChanged = false;
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

        private void TxtFNo_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbPort_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
