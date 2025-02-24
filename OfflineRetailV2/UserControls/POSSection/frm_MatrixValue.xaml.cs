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
    /// Interaction logic for frm_MatrixValue.xaml
    /// </summary>
    public partial class frm_MatrixValue : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_MatrixValue()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        
        private string strMatrixValue;
        private bool lbMatrixDefault;
        private DataTable dbtblMatrixData;
        private bool boolControlChanged;

        private bool boolOK;

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public string MatrixValue
        {
            get { return strMatrixValue; }
            set { strMatrixValue = value; }
        }

        public bool MatrixDefault
        {
            get { return lbMatrixDefault; }
            set { lbMatrixDefault = value; }
        }

        public DataTable MatrixData
        {
            get { return dbtblMatrixData; }
            set { dbtblMatrixData = value; }
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            txtValue.Text = strMatrixValue;
            chkDefault.IsChecked = lbMatrixDefault;
            boolControlChanged = false;
        }

        private bool IsValidAll()
        {
            bool lbMatch = false;
            if (txtValue.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Value);
                GeneralFunctions.SetFocus(txtValue);
                return false;
            }

            if (txtValue.Text.Trim() != strMatrixValue)
            {
                if (dbtblMatrixData != null)
                {
                    foreach (DataRow dr in dbtblMatrixData.Rows)
                    {
                        if ((dr["OptionValue"].ToString().ToUpper()) == (txtValue.Text.Trim().ToUpper()))
                        {
                            lbMatch = true;
                            DocMessage.MsgInformation(Properties.Resources.Duplicate_Value);
                            break;
                        }
                    }
                }
            }

            if (lbMatch)
            {
                GeneralFunctions.SetFocus(txtValue);
                return false;
            }

            return true;
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
                        boolControlChanged = false;
                        boolOK = true;
                    }
                    
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtValue_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkDefault_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                boolControlChanged = false;
                boolOK = true;
                CloseKeyboards();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            boolOK = false;
            CloseKeyboards();
            Close();
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
