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


namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_CloseoutEditDlg.xaml
    /// </summary>
    public partial class frm_CloseoutEditDlg : Window
    {

        private string strCloseoutType;

        public string CloseoutType
        {
            get { return strCloseoutType; }
            set { strCloseoutType = value; }
        }

        private bool boolOK;
        public frm_CloseoutEditDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        private void FetchRecord()
        {
            PosDataObject.Closeout objco = new PosDataObject.Closeout();
            objco.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objco.ShowRecordForEdit(strCloseoutType);

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

            grdCloseout.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = Properties.Resources.Edit_Close_Out;
            FetchRecord();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtNotes_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private async void BtnOK_Click_1(object sender, RoutedEventArgs e)
        {
            int RowID = 0;
            RowID = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            int intCloseoutID = 0;
            string strCloseoutType = "";
            intCloseoutID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colID));
            strCloseoutType = await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colType);
            blurGrid.Visibility = Visibility.Visible;
            frm_CloseoutCountDlg frm_CloseoutCountDlg = new frm_CloseoutCountDlg();
            try
            {
                frm_CloseoutCountDlg.CloseoutID = intCloseoutID;
                frm_CloseoutCountDlg.CloseoutType = strCloseoutType;
                frm_CloseoutCountDlg.CountType = "R";
                frm_CloseoutCountDlg.EditTendering = true;
                frm_CloseoutCountDlg.ShowDialog();
            }
            finally
            {
                frm_CloseoutCountDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int RowID = 0;
            RowID = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            int intCloseoutID = 0;
            string strCloseoutType = "";
            intCloseoutID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colID));
            strCloseoutType = await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colType);

            blurGrid.Visibility = Visibility.Visible;
            frm_CloseoutCountDlg frm_CloseoutCountDlg = new frm_CloseoutCountDlg();
            try
            {
                frm_CloseoutCountDlg.CloseoutID = intCloseoutID;
                frm_CloseoutCountDlg.CloseoutType = strCloseoutType;
                frm_CloseoutCountDlg.CountType = "R";
                frm_CloseoutCountDlg.EditTendering = true;
                frm_CloseoutCountDlg.ShowDialog();
            }
            finally
            {
                frm_CloseoutCountDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnUp1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdCloseout.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle > 0))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            }
        }

        private void BtnDown1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdCloseout.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle != (grdCloseout.ItemsSource as DataTable).Rows.Count - 1))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            }
        }
    }
}
