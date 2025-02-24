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
    /// Interaction logic for frm_SelectTimeDlg.xaml
    /// </summary>
    public partial class frm_SelectTimeDlg : Window
    {
        public frm_SelectTimeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private string strSTIME;
        private string strETIME;
        private bool boolControlChanged;
        private bool boolOK;

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public string STIME
        {
            get { return strSTIME; }
            set { strSTIME = value; }
        }

        public string ETIME
        {
            get { return strETIME; }
            set { strETIME = value; }
        }

        private bool validtime()
        {
            DateTime s = GeneralFunctions.fnDate(txtStart.EditValue);
            DateTime e = GeneralFunctions.fnDate(txtEnd.EditValue);
            if (e < s)
            {
                DocMessage.MsgInformation(Properties.Resources.End_Time_should_be_greater_than_Start_Time);
                GeneralFunctions.SetFocus(txtStart);
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtStart.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 9, 0, 0);
            txtEnd.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
            boolControlChanged = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {

                    if (validtime())
                    {
                        boolControlChanged = false;
                        strSTIME = txtStart.Text;
                        strETIME = txtEnd.Text;
                        boolOK = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (validtime())
            {
                strSTIME = txtStart.EditText;
                strETIME = txtEnd.EditText;
                boolControlChanged = false;
                boolOK = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            Close();
        }

        private void TxtStart_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }
    }
}
