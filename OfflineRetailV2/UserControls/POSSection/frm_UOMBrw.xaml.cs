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
    /// Interaction logic for frm_UOMBrw.xaml
    /// </summary>
    public partial class frm_UOMBrw : Window
    {
        public frm_UOMBrw()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }
        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private int intPID;

        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;

            for (intColCtr = 0; intColCtr < (frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, frm_UOMBrwUC.grdUOM, frm_UOMBrwUC.colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) frm_UOMBrwUC.gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task FocusDefault()
        {
            string sDefault = "";
            int intColCtr = 0;
            
            for (intColCtr = 0; intColCtr < (frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                sDefault = await GeneralFunctions.GetCellValue1(intColCtr, frm_UOMBrwUC.grdUOM, frm_UOMBrwUC.colIsDefault);
                if (sDefault == "Yes") break;
            }
            if (intColCtr >= 0) frm_UOMBrwUC.gridView1.FocusedRowHandle = intColCtr;
        }


        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (frm_UOMBrwUC.gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = frm_UOMBrwUC.gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, frm_UOMBrwUC.grdUOM, frm_UOMBrwUC.colID)));
            return intRecID;
        }


        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchUOMData(intPID);

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



            frm_UOMBrwUC.grdUOM.ItemsSource = dtblTemp;
            GeneralFunctions.SetRecordCountStatus(dbtbl.Rows.Count);
            dtblTemp.Dispose();
            dbtbl.Dispose();


        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*Todo:if (Settings.DecimalPlace == 3) frm_UOMBrwUC.colUnitPrice.DisplayFormat.FormatString = "f3";
            else colUnitPrice.DisplayFormat.FormatString = "f";*/

            frm_UOMBrwUC.btnAdd.Click += new RoutedEventHandler(BtnAddClick);
            frm_UOMBrwUC.btnEdit.Click += new RoutedEventHandler(BtnEditClick);
            frm_UOMBrwUC.btnDelete.Click += new RoutedEventHandler(BtnDeleteClick);
            frm_UOMBrwUC.gridView1.MouseDoubleClick += GridView1_MouseDoubleClick;


            FetchData();
            DataTable dtbl = new DataTable();
            dtbl = frm_UOMBrwUC.grdUOM.ItemsSource as DataTable;

            // code for focus default data

            int focusrowc = -1;
            bool isdefault = false;
            foreach (DataRow dr in dtbl.Rows)
            {
                focusrowc++;
                if (dr["IsDefault"].ToString() == "Yes")
                {
                    isdefault = true;
                    break;
                }
            }
            if (isdefault) frm_UOMBrwUC.gridView1.FocusedRowHandle = focusrowc;
            else frm_UOMBrwUC.gridView1.FocusedRowHandle = 0;
        }


        async void BtnAddClick(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_UOMDlg frm_Dlg = new frm_UOMDlg();
            try
            {
                frm_Dlg.ID = 0;
                frm_Dlg.PID = intPID;
                frm_Dlg.BrowseForm = this;
                frm_Dlg.ShowDialog();
                intNewRecID = frm_Dlg.NewID;
            }
            finally
            {
                frm_Dlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = frm_UOMBrwUC.gridView1.FocusedRowHandle;
            if ((frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_UOMDlg frm_dlg = new frm_UOMDlg();
            try
            {
                frm_dlg.ID = await ReturnRowID();
                if (frm_dlg.ID > 0)
                {
                    frm_dlg.PID = intPID;
                    frm_dlg.BrowseForm = this;
                    frm_dlg.ShowDialog();
                    intNewRecID = frm_dlg.ID;
                }
            }
            finally
            {
                frm_dlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        async void BtnEditClick(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        async void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = frm_UOMBrwUC.gridView1.FocusedRowHandle;
            if ((frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Unit_of_Measure))
                {
                    PosDataObject.Product objProduct = new PosDataObject.Product();
                    objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string strError = objProduct.DeleteUOMRecord(intRecdID);
                    if (strError != "")
                    {
                        DocMessage.ShowException("Unit of Measure", strError);
                    }
                    FetchData();
                    if ((frm_UOMBrwUC.grdUOM.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        frm_UOMBrwUC.gridView1.FocusedRowHandle = intRowID - 1;
                    }
                }
            }
        }
    }
}
