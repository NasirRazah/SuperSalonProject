﻿using System;
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
    /// Interaction logic for frm_SerialDlg.xaml
    /// </summary>
    public partial class frm_SerialDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_SerialDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private frm_SerialBrw frmBrowse;
        private frm_SerialBrwPOS frmBrowsePOS;
        private int intID;
        private int intNewID;
        private int intPID;
        private string strPrevSL;
        private bool boolControlChanged;

        private string strCAT;
        public string CAT
        {
            get { return strCAT; }
            set { strCAT = value; }
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

        public frm_SerialBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_SerialBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_SerialBrwPOS BrowseFormPOS
        {
            get
            {
                return frmBrowsePOS;
            }
            set
            {
                frmBrowsePOS = value;
            }
        }

        public void ShowData()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowSerializedRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtSL1.Text = dr["Serial1"].ToString();
                txtSL2.Text = dr["Serial2"].ToString();
                txtSL3.Text = dr["Serial3"].ToString();

                if (dr["ExpiryDate"].ToString() == "")
                {
                    dtExpiry.EditValue = null;
                }
                else
                {
                    dtExpiry.EditValue = GeneralFunctions.fnDate(dr["ExpiryDate"].ToString());
                }
                strPrevSL = txtSL1.Text;
            }
            dbtbl.Dispose();
        }

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateSerializedCount(txtSL1.Text.Trim());
        }

        private bool IsValidAll()
        {

            if (txtSL1.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Primaty_Serial_No);
                GeneralFunctions.SetFocus(txtSL1);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (strPrevSL != txtSL1.Text.Trim())))
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Primary_Serial_No);
                    GeneralFunctions.SetFocus(txtSL1);
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
            objProduct.SerialHeaderID = intPID;
            objProduct.Serial1 = txtSL1.Text.Trim();
            objProduct.Serial2 = txtSL2.Text.Trim();
            objProduct.Serial3 = txtSL3.Text.Trim();

            if (dtExpiry.EditValue == null)
            {
                objProduct.ExpiryDate = Convert.ToDateTime(null);
            }
            else
            {
                objProduct.ExpiryDate = dtExpiry.DateTime.Date;
            }


            objProduct.ID = intID;

            if (intID == 0)
            {
                strError = objProduct.InsertSerializedData();
                NewID = objProduct.ID;
            }
            else
            {
                strError = objProduct.UpdateSerializedData();
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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (strCAT == "ALL") BrowseForm.FetchData();
                    if (strCAT == "POS") BrowseFormPOS.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
           
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Serial_No;
                if (strCAT == "POS")
                {
                    lbslno2.Visibility = lbslno3.Visibility = txtSL2.Visibility = txtSL3.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Serial_No;
                ShowData();
            }
            txtSL1.Focus();
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
                            if (strCAT == "ALL") BrowseForm.FetchData();
                            if (strCAT == "POS") BrowseFormPOS.FetchData();
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

        private void TxtSL1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
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

        private void DtExpiry_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
