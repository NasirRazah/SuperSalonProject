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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_PrintLabelBrwUC.xaml
    /// </summary>
    public partial class frm_PrintLabelBrwUC : UserControl
    {
        public frm_PrintLabelBrwUC()
        {
            InitializeComponent();
        }

        private int intSelectedRowHandle;

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Ordering");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdPL.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdPL, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task SetCurrentRow1(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdPL.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdPL, colPLID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPL, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPL, colID)));
            return intRecID;
        }

        public async Task<int> ReturnRowID1()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdPL, colPLID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objPO = new PosDataObject.Product();
            objPO.Connection = SystemVariables.Conn;
            dbtbl = objPO.FetchPrintlabelData();

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



            grdPL.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }

        

        

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_EditPrintLabelDlg frm_EditPrintLabelDlg = new frm_EditPrintLabelDlg();
            try
            {
                frm_EditPrintLabelDlg.ID = await ReturnRowID1();
                if (frm_EditPrintLabelDlg.ID > 0)
                {
                    frm_EditPrintLabelDlg.lbText.Text = await GeneralFunctions.GetCellValue1(intRowID, grdPL, colProduct) + "\nSKU : " +
                        await GeneralFunctions.GetCellValue1(intRowID, grdPL, colSKU);
                    frm_EditPrintLabelDlg.txtQty.Text = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowID, grdPL, colQty)).ToString();
                    frm_EditPrintLabelDlg.BrowseForm = this;
                    frm_EditPrintLabelDlg.ShowDialog();
                    intNewRecID = frm_EditPrintLabelDlg.ID;
                }
            }
            finally
            {
                frm_EditPrintLabelDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow1(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        

        private void BtnBatchPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return;
            int ValidRecordCount = 0;
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            ValidRecordCount = objProduct.CountValidPrintLabelRecord();
            if (ValidRecordCount > 0)
            {
                if (DocMessage.MsgConfirmation("Do you want to start Batch Print ?") == MessageBoxResult.Yes)
                {
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                    frm_BatchPrintDlg frm_BatchPrintDlg = new frm_BatchPrintDlg();
                    try
                    {
                        frm_BatchPrintDlg.ShowDialog();
                    }
                    finally
                    {
                        frm_BatchPrintDlg.Close();
                        (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                        FetchData();
                    }
                }
            }
        }

        private async void BtnPrint_Click_1(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID == -1) return;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return;
            frm_PrintBarcodeDlg frm_PrintBarcodeDlg = new frm_PrintBarcodeDlg();
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            try
            {
                frm_PrintBarcodeDlg.isbatchprint = true;
                frm_PrintBarcodeDlg.SKU = await GeneralFunctions.GetCellValue1(intRowID, grdPL, colSKU);
                frm_PrintBarcodeDlg.ProductDesc = await GeneralFunctions.GetCellValue1(intRowID, grdPL, colProduct);
                frm_PrintBarcodeDlg.ProductDecimalPlace = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPL, colDecimal));
                if (await GeneralFunctions.GetCellValue1(intRowID, grdPL, colNoPriceOnLabel) == "Y")
                    frm_PrintBarcodeDlg.ProductPrice = "";
                else
                    frm_PrintBarcodeDlg.ProductPrice = await GeneralFunctions.GetCellValue1(intRowID, grdPL, colPrice);

                frm_PrintBarcodeDlg.Qty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPL, colQty));
                frm_PrintBarcodeDlg.LabelType = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPL, colLabelType));

                frm_PrintBarcodeDlg.ShowDialog();
            }
            finally
            {
                frm_PrintBarcodeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                FetchData();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0) return;
            if (DocMessage.MsgConfirmation("This will initialize Qty to Print to Zero of all products." + "\n" + "Do you wish to continue?") == MessageBoxResult.Yes)
            {
                PosDataObject.Product objPO = new PosDataObject.Product();
                objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                string ErrMsg = objPO.ClearPrintLabel();
                if (ErrMsg != "")
                {
                    DocMessage.ShowException("Clearing Print Label", ErrMsg);
                }
                FetchData();
            }
        }

        private async void BtnEdit_Click_1(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private void BtnBatchDelete_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtbl = new DataTable();
            dtbl = grdPL.ItemsSource as DataTable;
            if (dtbl.Rows.Count > 0)
            {
                if (DocMessage.MsgDelete("This Batch Data"))
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        int intRowID = GeneralFunctions.fnInt32(dr["PLID"].ToString());
                        PosDataObject.Product objPO = new PosDataObject.Product();
                        objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        string ErrMsg = objPO.DeletePrintLabel(intRowID);


                        PosDataObject.Recv objPO1 = new PosDataObject.Recv();
                        objPO1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        string ErrMsg1 = objPO1.UpdatePrintQty(dr["SKU"].ToString(), GeneralFunctions.fnInt32(dr["Qty"].ToString()));
                    }
                    FetchData();
                }
            }
        }

        private async void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdPL.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID1();
            string psku = "";
            int pqty = 0;
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Print Label"))
                {
                    psku = await GeneralFunctions.GetCellValue1(intRowID, grdPL, colSKU);
                    pqty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdPL, colQty));

                    PosDataObject.Product objPO = new PosDataObject.Product();
                    objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    string ErrMsg = objPO.DeletePrintLabel(intRecdID);
                    if (ErrMsg != "")
                    {
                        DocMessage.ShowException("Deleting Print Label", ErrMsg);
                    }
                    else
                    {
                        PosDataObject.Recv objPO1 = new PosDataObject.Recv();
                        objPO1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        string ErrMsg1 = objPO1.UpdatePrintQty(psku, pqty);
                    }

                    FetchData();

                    if ((grdPL.ItemsSource as DataTable).Rows.Count > 0)
                    {
                        await SetCurrentRow1(intRecdID - 1);
                    }
                }
            }
        }
    }
}
