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
    /// Interaction logic for frm_TaxDlg.xaml
    /// </summary>
    public partial class frm_POSApptServiceDlg : Window
    {
        private int intEmployeeID;

        public int EmployeeID
        {
            get { return intEmployeeID; }
            set { intEmployeeID = value; }
        }

        private DataTable dtblSelectedService;

        public DataTable SelectedService
        {
            get { return dtblSelectedService; }
            set { dtblSelectedService = value; }
        }

        private bool IsPopulate = false;

        public frm_POSApptServiceDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        public void PopulateCategory()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchLookupData1();

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

            catlkup.ItemsSource = dtblTemp;
            catlkup.EditValue = "0";
            dbtblCat.Dispose();
        }

        public void PopulateDepartment()
        {
            PosDataObject.Department objCategory = new PosDataObject.Department();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchLookupData1();

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

            deptlkup.ItemsSource = dtblTemp;
            deptlkup.EditValue = "0";
            dbtblCat.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Select Services";
            dtblSelectedService = new DataTable();
            dtblSelectedService.Columns.Add("LineNo", System.Type.GetType("System.Int32"));
            dtblSelectedService.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblSelectedService.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblSelectedService.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblSelectedService.Columns.Add("MinimumServiceTime", System.Type.GetType("System.String"));

            PopulateCategory();
            PopulateDepartment();
            IsPopulate = true;
            LoadServices();
        }

        private void PopulateBatchGrid()
        {
            
        }

        private void LoadServices()
        {
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            DataTable dtbl = objp.GetActiveAssignedServices(intEmployeeID, GeneralFunctions.fnInt32(catlkup.EditValue), GeneralFunctions.fnInt32(deptlkup.EditValue));
            grdService.ItemsSource = dtbl;
        }


        private void AddSelectedServices()
        {
            gridView1.PostEditor();
            DataTable dtbl = new DataTable();
            dtbl = grdService.ItemsSource as DataTable;
            int ln = 0;
            if (dtbl.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["ServiceCheck"].ToString()) == true)
                    {
                        ln++;
                        dtblSelectedService.Rows.Add(new object[] {
                                    ln,
                                    dr["ID"].ToString(),
                                    dr["SKU"].ToString(),
                                    dr["Description"].ToString(),
                                    dr["MinimumServiceTime"].ToString()});
                    }
                }
            }

        }


        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedServices();
            DialogResult = true;
            ResMan.closeKeyboard();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        private void Catlkup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (IsPopulate) LoadServices();
        }

        private void Catlkup_PopupOpening(object sender, DevExpress.Xpf.Editors.OpenPopupEventArgs e)
        {
            DevExpress.Utils.Win.IPopupControl popup = sender as DevExpress.Utils.Win.IPopupControl;
            if (popup != null)
            {
                DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm popupForm = popup.PopupWindow as DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm;
                popupForm.Width = (int)(sender as Control).Width;
            }
        }

        private void Deptlkup_PopupOpening(object sender, DevExpress.Xpf.Editors.OpenPopupEventArgs e)
        {
            DevExpress.Utils.Win.IPopupControl popup = sender as DevExpress.Utils.Win.IPopupControl;
            if (popup != null)
            {
                DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm popupForm = popup.PopupWindow as DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm;
                popupForm.Width = (int)(sender as Control).Width;
            }
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void Catlkup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Deptlkup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
