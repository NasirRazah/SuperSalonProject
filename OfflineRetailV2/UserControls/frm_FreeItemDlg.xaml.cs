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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_FreeItemDlg.xaml
    /// </summary>
    public partial class frm_FreeItemDlg : Window
    {

        private int intID;
        private int intBuyItemID;
        private int intITMID;
        private string strITMTXT;
        private int intITMQTY;
        private bool boolControlChanged;

        private bool boolOK;

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int BuyItemID
        {
            get { return intBuyItemID; }
            set { intBuyItemID = value; }
        }

        public int ITMID
        {
            get { return intITMID; }
            set { intITMID = value; }
        }

        public string ITMTXT
        {
            get { return strITMTXT; }
            set { strITMTXT = value; }
        }

        public int ITMQTY
        {
            get { return intITMQTY; }
            set { intITMQTY = value; }
        }

        public frm_FreeItemDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        public void PopulateProduct()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupFreeItem(intBuyItemID);

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

            cmbFreeItem.ItemsSource = dtblTemp;
            cmbFreeItem.Text = "";
            dbtblCat.Dispose();
        }

        

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFreeItem.EditValue == null) return;
            if (cmbFreeItem.EditValue.ToString() != "0")
            {
                boolControlChanged = false;
                intITMID = GeneralFunctions.fnInt32(cmbFreeItem.EditValue.ToString());
                strITMTXT = cmbFreeItem.Text;
                intITMQTY = GeneralFunctions.fnInt32(txtQty.Text);
                boolOK = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateProduct();

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Free_Item;
                cmbFreeItem.Focus();
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Free_Item;
                cmbFreeItem.EditValue = intITMID.ToString();
                txtQty.Text = intITMQTY.ToString();
            }

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
                    if (cmbFreeItem.EditValue != null)
                    {
                        if (cmbFreeItem.EditValue.ToString() != "0")
                        {
                            boolControlChanged = false;
                            intITMID = GeneralFunctions.fnInt32(cmbFreeItem.EditValue.ToString());
                            strITMTXT = cmbFreeItem.Text;
                            intITMQTY = GeneralFunctions.fnInt32(txtQty.Text);
                            boolOK = true;
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

        private void CmbProduct_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbProduct_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
