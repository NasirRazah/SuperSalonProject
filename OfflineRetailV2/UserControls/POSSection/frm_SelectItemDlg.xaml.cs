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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_SelectItemDlg.xaml
    /// </summary>
    public partial class frm_SelectItemDlg : Window
    {
        public frm_SelectItemDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }
        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private int intLevelID;
        private int intITMID;
        private string strITMPLU;
        private string strITMTXT;
        private bool boolControlChanged;
        private bool boolOK;

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public int LevelID
        {
            get { return intLevelID; }
            set { intLevelID = value; }
        }

        public int ITMID
        {
            get { return intITMID; }
            set { intITMID = value; }
        }

        public string ITMPLU
        {
            get { return strITMPLU; }
            set { strITMPLU = value; }
        }

        public string ITMTXT
        {
            get { return strITMTXT; }
            set { strITMTXT = value; }
        }


        public void populatelkup()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            if (intLevelID == 0) dbtblCat = objCategory.LookupProductORCategory(0);
            if (intLevelID == 1) dbtblCat = objCategory.LookupProductORCategory(1);
            if (intLevelID == 11) dbtblCat = objCategory.LookupProductORCategory(11);
            if (intLevelID == 12) dbtblCat = objCategory.LookupProductORCategory(12);
            if (intLevelID == 10) dbtblCat = objCategory.LookupProductORCategory(10);
            if (intLevelID == 2) dbtblCat = objCategory.LookupProductORCategory(2);
            if (intLevelID == 3) dbtblCat = objCategory.LookupProductORCategory(3);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbAnswer.ItemsSource = dtblTemp;

            dbtblCat.Dispose();

            
        }

        private void CmbAnswer_PopupOpened(object sender, RoutedEventArgs e)
        {
            LookUpEdit edt = (LookUpEdit)sender;
            Dispatcher.BeginInvoke((Action)(() =>
            {
                GridControl t = edt.GetGridControl();
                if ((intLevelID == 1) || (intLevelID == 10) || (intLevelID == 11))
                {
                    t.Columns[2].Header = "SKU";
                }
                if ((intLevelID == 0) || (intLevelID == 2) || (intLevelID == 3))
                {
                    t.Columns[1].Header = "Code";
                }
                if ((intLevelID == 12))
                {
                    t.Columns[1].Header = "PLU";
                }
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (intLevelID == 0)
            {
                lbText.Text = "Item Categories";
                Title.Text = "Select Item Category";
            }
            if ((intLevelID == 1) || (intLevelID == 10) || (intLevelID == 11))
            {
                lbText.Text = "Items";
                Title.Text = "Select Item";
            }

            if (intLevelID == 2)
            {
                lbText.Text = "Families";
                Title.Text = "Select Item Family";
            }

            if (intLevelID == 3)
            {
                lbText.Text = "Departments";
                Title.Text = "Select Item Department";
            }

            populatelkup();
            boolControlChanged = false;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAnswer.EditValue != null)
            {
                boolControlChanged = false;
                if (intLevelID != 12) intITMID = GeneralFunctions.fnInt32(cmbAnswer.EditValue.ToString());
                else strITMPLU = cmbAnswer.EditValue.ToString();
                strITMTXT = cmbAnswer.Text;
                boolOK = true;
                Close();
            }
        }

        private void CmbAnswer_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
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

                    if (cmbAnswer.EditValue != null)
                    {
                        boolControlChanged = false;
                        if (intLevelID != 12) intITMID = GeneralFunctions.fnInt32(cmbAnswer.EditValue.ToString());
                        else strITMPLU = cmbAnswer.EditValue.ToString();
                        strITMTXT = cmbAnswer.Text;
                        boolOK = true;
                        
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void CmbAnswer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
