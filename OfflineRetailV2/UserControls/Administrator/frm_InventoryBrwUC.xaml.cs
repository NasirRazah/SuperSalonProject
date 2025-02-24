using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.ComponentModel;
using DevExpress.Xpf.Grid.LookUp;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_InventoryBrwUC.xaml
    /// </summary>
    public partial class frm_InventoryBrwUC : UserControl
    {
        public frm_InventoryBrwUC()
        {
            InitializeComponent();
        }

        private bool blPOS;
        private int rowPos = 0;
        private int intSelectedRowHandle;
        private GridColumn _searchcol;
        public bool bPOS
        {
            get { return blPOS; }
            set { blPOS = value; }
        }

        private string strcd;

        public string storecd
        {
            get { return strcd; }
            set { strcd = value; }
        }

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Host");
        }


        public void PopulateStore()
        {

            PosDataObject.Central objcout = new PosDataObject.Central();
            objcout.Connection = SystemVariables.Conn;
            objcout.DataObjectCulture_All = "All";
            DataTable dtbl = new DataTable();
            dtbl = objcout.FetchOtherStoreForInventory(Settings.StoreCode);
            cmbFilter.ItemsSource = dtbl;
            cmbFilter.EditValue = "All";

        }

        public void PopulateSKU()
        {
            PosDataObject.Central objcout = new PosDataObject.Central();
            objcout.Connection = SystemVariables.Conn;
            objcout.DataObjectCulture_All = "All";
            objcout.DataObjectCulture_None = "(None)";
            DataTable dbtblLKUP = new DataTable();
            dbtblLKUP = objcout.FetchSKU();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblLKUP.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            lkupSKU.ItemsSource = dtblTemp;

            lkupSKU.EditValue = "All";
            dbtblLKUP.Dispose();

        }

        public void FetchData()
        {
            if ((cmbFilter.EditValue != null) && (lkupSKU.EditValue != null))
            {
                PosDataObject.Central objcout = new PosDataObject.Central();
                objcout.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = objcout.ShowInventory(cmbFilter.EditValue.ToString(), lkupSKU.EditValue.ToString());


                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtbl.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }



                grdInv.ItemsSource = dtblTemp;

                dtblTemp.Dispose();
                dtbl.Dispose();


                
            }

        }







        private void cmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((cmbFilter.EditValue != null) && (lkupSKU.EditValue != null))
            {
                FetchData();
            }
        }


        private void GrdCustomer_CustomGroupDisplayText(object sender, CustomGroupDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colStore")
            {
                e.DisplayText = "Store" + " : " + e.Value.ToString();
            }
        }

        private void LkupSKU_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((cmbFilter.EditValue != null) && (lkupSKU.EditValue != null))
            {
                FetchData();
            }
        }

        private async void PART_Editor_RequestNavigation(object sender, DevExpress.Xpf.Editors.HyperlinkEditRequestNavigationEventArgs e)
        {
            string tSKU = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdInv, colSKU);
            string tDesc = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdInv, colDescription);
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_MatrixQty frm_dld = new frm_MatrixQty();
            try
            {
                frm_dld.Store = cmbFilter.EditValue.ToString();
                frm_dld.SKU = tSKU;
                frm_dld.Consolidated = cmbFilter.EditValue.ToString() == "All" ? "Y" : "N";
                frm_dld.Title.Text = tDesc + " (" +
                                tSKU + ")";
                frm_dld.ShowDialog();
            }
            finally
            {
                frm_dld.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void LkupSKU_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

    public class EditorTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            GridCellData data = (GridCellData)item;
            PropertyDescriptor property = TypeDescriptor.GetProperties(data.Data)["ProductType"];
            FrameworkElement fe = container as FrameworkElement;
            EditGridCellData row = item as EditGridCellData;

            if (row == null)
                return null;

            string ptype = (row.RowData.Row as DataRowView).Row.ItemArray[7].ToString();


            if (ptype == "M")
            {
                return (DataTemplate)fe.FindResource("hyperlinkEditor");
            }
            else
            {
                return (DataTemplate)fe.FindResource("textEditor");
            }

        }
    }
}
