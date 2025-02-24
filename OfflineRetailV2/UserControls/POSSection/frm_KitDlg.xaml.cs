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
    /// Interaction logic for frm_KitDlg.xaml
    /// </summary>
    public partial class frm_KitDlg : Window
    {

        private frm_KitBrw frmBrowse;
        private int intID;
        private int intNewID;
        private int intPID;
        private int intPrevItemID;
        private bool boolControlChanged;

        public frm_KitDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
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
        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }

        public frm_KitBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_KitBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateProduct()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchMLookupData(0);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSKU.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbProduct.ItemsSource = dtblTemp;
            cmbProduct.Text = "";
            dbtblSKU.Dispose();
        }

        public void ShowData()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowKitRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                cmbProduct.EditValue = dr["ItemID"].ToString();
                spnCount.Text = dr["ItemCount"].ToString();
                intPrevItemID = GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString());
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateKitCount(intPID, GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString()));
        }

        private bool IsValidAll()
        {
            if (cmbProduct.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Kit_Item_SKU);
                GeneralFunctions.SetFocus(cmbProduct);
                return false;
            }
            if (spnCount.Text == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Quantity);
                GeneralFunctions.SetFocus(spnCount);
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (intPrevItemID != GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString()))))
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Kit_Item);
                    GeneralFunctions.SetFocus(cmbProduct);
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
            objProduct.KitID = intPID;
            objProduct.ItemID = GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString());
            objProduct.ItemCount = GeneralFunctions.fnInt32(spnCount.Text);
            objProduct.ID = intID;
            if (intID == 0)
            {
                strError = objProduct.InsertKitData();
                NewID = objProduct.ID;
            }
            else
            {
                strError = objProduct.UpdateKitData();
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

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
                    Close();
                }
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
                Title.Text = Properties.Resources.Add_Kit;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Kit;
                ShowData();
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
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
