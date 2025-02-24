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
    /// Interaction logic for frm_SelectDateDlg.xaml
    /// </summary>
    public partial class frm_SelectDateDlg : Window
    {
        public frm_SelectDateDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private DateTime dtSDate;
        private DateTime dtEDate;
        private bool boolControlChanged;

        private bool boolOK;

        public DateTime SDate
        {
            get { return dtSDate; }
            set { dtSDate = value; }
        }

        public DateTime EDate
        {
            get { return dtEDate; }
            set { dtEDate = value; }
        }

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtStart.DateTime = DateTime.Today;
            dtEnd.DateTime = DateTime.Today.AddDays(1);
            boolControlChanged = false; 
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            dtSDate = GeneralFunctions.fnDate(dtStart.EditValue.ToString());
            dtEDate = GeneralFunctions.fnDate(dtEnd.EditValue.ToString());
            boolOK = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            Close();
        }

        private void DtStart_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void DtEnd_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult DlgResult = new MessageBoxResult();
            DlgResult = DocMessage.MsgSaveChanges();

            if (DlgResult == MessageBoxResult.Yes)
            {
                boolControlChanged = false;
                dtSDate = GeneralFunctions.fnDate(dtStart.EditValue.ToString());
                dtEDate = GeneralFunctions.fnDate(dtEnd.EditValue.ToString());
                boolOK = true;
            }

            if (DlgResult == MessageBoxResult.Cancel)
                e.Cancel = true;
        }

        private void DtStart_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
