using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Win32;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSEmpList.xaml
    /// </summary>
    public partial class frm_POSEmpList : Window
    {
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";

        private string strPhotoType;
        private string strPhotoFile;
        private int intPhotoID;

        public frm_POSEmpList()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            dbtbl = objEmployee.FetchData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdEmployee.ItemsSource = dtblTemp;
            dbtbl.Dispose();
            dtblTemp.Dispose();
            GeneralFunctions.SetRecordCountStatus(dbtbl.Rows.Count);
        }
        OpenFileDialog opendlg_PicFile = new OpenFileDialog()
        {
            Filter = "All Picture Files (*.bmp,*.jpg,*.jpeg,*.gif,*.png,*.ico,*.emf,*.wmf)|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.ico;*.emf;*.wmf",
            AddExtension = true,
            CheckFileExists = true,
            CheckPathExists = true,
            DereferenceLinks = true,
            Multiselect = false,
            ReadOnlyChecked = false,
            RestoreDirectory = false,
            ShowReadOnly = false,
            ValidateNames = true
        };
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Employee_Photo;
            FetchData();
            
        }

        public void ShowPhoto()
        {
            try
            {
                GeneralFunctions.LoadPhotofromDB(strPhotoType,  intPhotoID, picPhoto);
            }
            catch (Exception ex)
            {

                BitmapImage bi = new BitmapImage();
                picPhoto.Source = bi;
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void gridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            strPhotoType = "Employee";
            strPhotoFile = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdEmployee, colEmpID);
            intPhotoID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdEmployee, colID));
            ShowPhoto();
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == 0) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdEmployee.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == (grdEmployee.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }
    }
}
