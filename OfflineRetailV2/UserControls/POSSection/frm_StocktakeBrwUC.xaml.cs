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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_StocktakeBrwUC.xaml
    /// </summary>
    public partial class frm_StocktakeBrwUC : UserControl
    {
        private bool blFlag;
        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }

        public frm_StocktakeBrwUC()
        {
            InitializeComponent();
        }

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Items");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;

            for (intColCtr = 0; intColCtr < (grdstkheader.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdstkheader, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colID)));
            return intRecID;
        }

        public async Task<int> ReturnBatchID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colID)));
            return intRecID;
        }

        public void FetchData(DateTime fFrmDate, DateTime fToDate)
        {

            grdstkheader.ItemsSource = null;
            grdstkline.ItemsSource = null;
            DataTable dbtbl = new DataTable();
            PosDataObject.StockJournal objj = new PosDataObject.StockJournal();
            objj.Connection = SystemVariables.Conn;
            dbtbl = objj.FetchStocktakeBrowseHeader(fFrmDate, fToDate);

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



            grdstkheader.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            GeneralFunctions.SetRecordCountStatus(dbtbl.Rows.Count);
            gridView1.FocusedRowHandle = -1;
            gridView1.FocusedRowHandle = 0;
        }

        private async void GridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (grdstkheader.ItemsSource == null) return;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return;
            int rwhandle = 0;
            rwhandle = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            PosDataObject.StockJournal ojstk = new PosDataObject.StockJournal();
            ojstk.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = ojstk.FetchStocktakeBrowseDetail(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(rwhandle, grdstkheader, colID)), 0, 0);

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



            grdstkline.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dtbl.Dispose();


        }

        private void FDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFlag)
            {
                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                {
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                }
            }
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_StocktakeDlg frm_StocktakeDlg = new frm_StocktakeDlg();
            try
            {
                frm_StocktakeDlg.ID = 0;
                frm_StocktakeDlg.BrowseForm = this;
                frm_StocktakeDlg.IsViewMode = false;
                frm_StocktakeDlg.ShowDialog();
                intNewRecID = frm_StocktakeDlg.NewID;
            }
            finally
            {
                
                frm_StocktakeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_StocktakeDlg frm_StocktakeDlg = new frm_StocktakeDlg();
            try
            {
                frm_StocktakeDlg.ID = await ReturnRowID();
                if (frm_StocktakeDlg.ID > 0)
                {
                    if (await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colStatus) == "Completed") frm_StocktakeDlg.IsViewMode = true;
                    else frm_StocktakeDlg.IsViewMode = false;
                    frm_StocktakeDlg.BrowseForm = this;
                    frm_StocktakeDlg.ShowDialog();
                    intNewRecID = frm_StocktakeDlg.ID;
                }
            }
            finally
            {
                frm_StocktakeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnPost_Click(object sender, RoutedEventArgs e)
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return;
            if (await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colStatus) == "Completed") return;
            else
            {
                if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_post_document) == MessageBoxResult.Yes)
                {
                    if (ValidForPost(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colID))))
                    {
                        if (PostDocument(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colID)), await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colDoc)))
                        {
                            DocMessage.MsgInformation(Properties.Resources.Successfully_posted);
                            if (blFlag)
                            {
                                if ((FDate.EditValue != null) && (TDate.EditValue != null))
                                {
                                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                                }
                            }
                        }
                        else
                        {
                            DocMessage.MsgInformation(Properties.Resources.Error_posting);
                        }
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.One_more_products_posted_other_Inventory_Adjustment);
                    }
                }
            }
        }

        private bool PostDocument(int DocID, string DocNo)
        {
            bool bl = false;
            PosDataObject.StockJournal objj = new PosDataObject.StockJournal();
            objj.Connection = SystemVariables.Conn;
            objj.LoginUserID = SystemVariables.CurrentUserID;
            objj.JDocNo = DocNo;
            objj.JTerminalName = Settings.TerminalName;
            objj.ID = DocID;
            objj.dStkDetail = grdstkline.ItemsSource as DataTable;
            objj.BeginTransaction();
            if (objj.StocktakePost())
            {
                bl = true;
            }
            objj.EndTransaction();
            return bl;


        }

        private bool ValidForPost(int DocID)
        {
            bool bl = false;
            DataTable dtbl = new DataTable();
            dtbl = grdstkline.ItemsSource as DataTable;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (DataCompare(DocID, GeneralFunctions.fnInt32(dr["ProductID"].ToString())))
                {
                    bl = true;
                    break;
                }
            }
            return !bl;
        }

        private bool DataCompare(int DocID, int PID)
        {
            PosDataObject.StockJournal oj = new PosDataObject.StockJournal();
            oj.Connection = SystemVariables.Conn;
            return oj.IfFuturePostedProductIDExists(DocID, PID);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return;
            
            int intRecdID = await ReturnRowID();

            if (intRecdID > 0)
            {
                if (await GeneralFunctions.GetCellValue1(intRowID, grdstkheader, colStatus) == "Completed") return;

                if (DocMessage.MsgDelete(Properties.Resources.Inventory_Adjustment_document))
                {
                    PosDataObject.StockJournal objPO = new PosDataObject.StockJournal();
                    objPO.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objPO.ID = intRecdID;
                    objPO.BeginTransaction();
                    bool blFlag = objPO.DeleteStockTake();
                    objPO.EndTransaction();
                    string ErrMsg = objPO.ErrorMsg;
                    if (!blFlag)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Error_deleting_Inventory_Adjustment_document);
                    }
                    FetchData(GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
                    if ((grdstkheader.ItemsSource as DataTable).Rows.Count > 1)
                    {
                        await SetCurrentRow(intRecdID - 1);
                    }
                }
            }
        }

        private async void BarButtonItem5_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdstkheader.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                ExecuteGroupReport(intRecdID);
            }
        }

        private void ExecuteGroupReport(int pID)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }

            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();


            DataTable dtbllink = new DataTable();
            PosDataObject.StockJournal objsj2 = new PosDataObject.StockJournal();
            objsj2.Connection = SystemVariables.Conn;
            dtbllink = objsj2.ShowStockTakeReportLinkCount(pID);

            DataTable dtbl = new DataTable();
            PosDataObject.StockJournal objsj1 = new PosDataObject.StockJournal();
            objsj1.Connection = SystemVariables.Conn;
            dtbl = objsj1.ShowStockTakeReportParent(pID);

            foreach (DataRow drl1 in dtbllink.Rows)
            {
                foreach (DataRow drh in dtbl.Rows)
                {
                    if (drl1["LinkSKU"].ToString() == drh["ProductID"].ToString())
                    {
                        drh["BreakPackFlag"] = drl1["LCount"].ToString();
                    }
                }
            }

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Product.repStockTake rep_MatrixReport = new OfflineRetailV2.Report.Product.repStockTake();
            GeneralFunctions.MakeReportWatermark(rep_MatrixReport);
            rep_MatrixReport.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_MatrixReport.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_MatrixReport.DecimalPlace = Settings.DecimalPlace;
            /*if (Settings.DecimalPlace == 3)
            {
                rep_MatrixReport.rTot2.Summary.FormatString = "{0:0.000}";
                rep_MatrixReport.rTot3.Summary.FormatString = "{0:0.000}";
                rep_MatrixReport.rRTot2.Summary.FormatString = "{0:0.000}";
                rep_MatrixReport.rRTot3.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_MatrixReport.rTot2.Summary.FormatString = "{0:0.00}";
                rep_MatrixReport.rTot3.Summary.FormatString = "{0:0.00}";
                rep_MatrixReport.rRTot2.Summary.FormatString = "{0:0.00}";
                rep_MatrixReport.rRTot3.Summary.FormatString = "{0:0.00}";
            }*/

            DataTable p = new DataTable("Parent");

            p.Columns.Add("ID", System.Type.GetType("System.String"));
            p.Columns.Add("DocNo", System.Type.GetType("System.String"));
            p.Columns.Add("DocDate", System.Type.GetType("System.String"));
            p.Columns.Add("DocComment", System.Type.GetType("System.String"));
            p.Columns.Add("StkStatus", System.Type.GetType("System.String"));
            p.Columns.Add("ProductID", System.Type.GetType("System.String"));
            p.Columns.Add("Description", System.Type.GetType("System.String"));
            p.Columns.Add("SKUQty", System.Type.GetType("System.String"));
            p.Columns.Add("SKU", System.Type.GetType("System.String"));
            p.Columns.Add("StkCount", System.Type.GetType("System.String"));
            p.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
            p.Columns.Add("BreakPackFlag", System.Type.GetType("System.String"));
            p.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
            p.Columns.Add("QtyVar", System.Type.GetType("System.String"));


            foreach (DataRow dr in dtbl.Rows)
            {
                DataRow r1 = p.NewRow();
                r1["ID"] = dr["ID"].ToString();
                r1["DocNo"] = dr["DocNo"].ToString();
                r1["DocDate"] = dr["DocDate"].ToString();
                r1["DocComment"] = dr["DocComment"].ToString();
                r1["StkStatus"] = dr["StkStatus"].ToString();
                r1["ProductID"] = dr["ProductID"].ToString();
                r1["Description"] = dr["Description"].ToString();
                r1["SKUQty"] = dr["SKUQty"].ToString();
                r1["SKU"] = dr["SKU"].ToString();
                r1["StkCount"] = dr["StkCount"].ToString();
                r1["LinkSKU"] = dr["LinkSKU"].ToString();
                r1["BreakPackFlag"] = dr["BreakPackFlag"].ToString();
                r1["QtyOnHand"] = dr["QtyOnHand"].ToString();
                r1["QtyVar"] = dr["QtyVar"].ToString();

                p.Rows.Add(r1);
            }

            DataTable dtbl1 = new DataTable();
            PosDataObject.StockJournal objProduct1 = new PosDataObject.StockJournal();
            objProduct1.Connection = SystemVariables.Conn;
            dtbl1 = objProduct1.ShowStockTakeReportChild(pID);

            DataTable dtbl2 = new DataTable();
            dtbl2.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("DocNo", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("DocDate", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("DocComment", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("StkStatus", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("Description", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("StkCount", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("LinkSKU", System.Type.GetType("System.Int32"));
            dtbl2.Columns.Add("BreakPackFlag", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("QtyOnHand", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("QtyVar", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("StkID", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("BPSKU", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("BPDescription", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("BPStkCount", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("BPSKUQty", System.Type.GetType("System.Double"));

            bool comments = false;

            foreach (DataRow dr7 in dtbl.Rows)
            {
                if (dr7["DocComment"].ToString().Trim() != "") comments = true;
                bool foundflg = false;
                foreach (DataRow dr8 in dtbl1.Rows)
                {
                    if (dr7["ProductID"].ToString() == dr8["LinkSKU"].ToString())
                    {
                        foundflg = true;
                        break;
                    }
                }
                if (!foundflg)
                {
                    dtbl2.Rows.Add(new object[] {  dr7["ID"].ToString(),
                                                   dr7["DocNo"].ToString(),
                                                   dr7["DocDate"].ToString(),
                                                   dr7["DocComment"].ToString(),
                                                   dr7["StkStatus"].ToString(),
                                                   dr7["ProductID"].ToString(),
                                                   dr7["Description"].ToString(),
                                                   GeneralFunctions.fnDouble(dr7["SKUQty"].ToString()),
                                                   dr7["SKU"].ToString(),
                                                   GeneralFunctions.fnDouble(dr7["StkCount"].ToString()),
                                                   GeneralFunctions.fnInt32(dr7["LinkSKU"].ToString()),"0",
                                                   GeneralFunctions.fnDouble(dr7["QtyOnHand"].ToString()),
                                                   GeneralFunctions.fnDouble(dr7["QtyVar"].ToString()),pID,
                                                   "","",0,0});
                }
                if (foundflg)
                {
                    foreach (DataRow dr9 in dtbl1.Rows)
                    {
                        if (dr7["ProductID"].ToString() == dr9["LinkSKU"].ToString())
                        {
                            dtbl2.Rows.Add(new object[] {  dr7["ID"].ToString(),
                                                   dr7["DocNo"].ToString(),
                                                   dr7["DocDate"].ToString(),
                                                   dr7["DocComment"].ToString(),
                                                   dr7["StkStatus"].ToString(),
                                                   dr7["ProductID"].ToString(),
                                                   dr7["Description"].ToString(),
                                                   GeneralFunctions.fnDouble(dr7["SKUQty"].ToString()),
                                                   dr7["SKU"].ToString(),
                                                   GeneralFunctions.fnDouble(dr7["StkCount"].ToString()),
                                                   GeneralFunctions.fnInt32(dr7["LinkSKU"].ToString()),"0",
                                                   GeneralFunctions.fnDouble(dr7["QtyOnHand"].ToString()),
                                                   GeneralFunctions.fnDouble(dr7["QtyVar"].ToString()),
                                                   pID,dr9["SKU"].ToString(),
                                                   dr9["Description"].ToString(),
                                                   GeneralFunctions.fnDouble(dr9["StkCount"].ToString()),
                                                   GeneralFunctions.fnDouble(dr9["SKUQty"].ToString())});
                        }
                    }
                }
            }

            foreach (DataRow drl2 in dtbllink.Rows)
            {
                foreach (DataRow drh1 in dtbl2.Rows)
                {
                    if (drl2["LinkSKU"].ToString() == drh1["ProductID"].ToString())
                    {
                        drh1["BreakPackFlag"] = drl2["LCount"].ToString();
                    }
                }
            }

            DataTable c = new DataTable("Child");

            c.Columns.Add("ID", System.Type.GetType("System.String"));
            c.Columns.Add("DocNo", System.Type.GetType("System.String"));
            c.Columns.Add("DocDate", System.Type.GetType("System.String"));
            c.Columns.Add("DocComment", System.Type.GetType("System.String"));
            c.Columns.Add("StkStatus", System.Type.GetType("System.String"));
            c.Columns.Add("ProductID", System.Type.GetType("System.String"));
            c.Columns.Add("Description", System.Type.GetType("System.String"));
            c.Columns.Add("SKUQty", System.Type.GetType("System.String"));
            c.Columns.Add("SKU", System.Type.GetType("System.String"));
            c.Columns.Add("StkCount", System.Type.GetType("System.String"));
            c.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
            c.Columns.Add("BreakPackFlag", System.Type.GetType("System.String"));
            c.Columns.Add("QtyOnHand", System.Type.GetType("System.String"));
            c.Columns.Add("QtyVar", System.Type.GetType("System.String"));
            c.Columns.Add("StkID", System.Type.GetType("System.String"));
            c.Columns.Add("BPSKU", System.Type.GetType("System.String"));
            c.Columns.Add("BPDescription", System.Type.GetType("System.String"));
            c.Columns.Add("BPStkCount", System.Type.GetType("System.String"));
            c.Columns.Add("BPSKUQty", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl2.Rows)
            {
                DataRow r1 = c.NewRow();
                r1["ID"] = dr["ID"].ToString();
                r1["DocNo"] = dr["DocNo"].ToString();
                r1["DocDate"] = dr["DocDate"].ToString();
                r1["DocComment"] = dr["DocComment"].ToString();
                r1["StkStatus"] = dr["StkStatus"].ToString();
                r1["ProductID"] = dr["ProductID"].ToString();
                r1["Description"] = dr["Description"].ToString();
                r1["SKUQty"] = dr["SKUQty"].ToString();
                r1["SKU"] = dr["SKU"].ToString();
                r1["StkCount"] = dr["StkCount"].ToString();
                r1["LinkSKU"] = dr["LinkSKU"].ToString();
                r1["BreakPackFlag"] = dr["BreakPackFlag"].ToString();
                r1["QtyOnHand"] = dr["QtyOnHand"].ToString();
                r1["QtyVar"] = dr["QtyVar"].ToString();
                r1["StkID"] = dr["StkID"].ToString();
                r1["BPSKU"] = dr["BPSKU"].ToString();
                r1["BPDescription"] = dr["BPDescription"].ToString();
                r1["BPStkCount"] = dr["BPStkCount"].ToString();
                r1["BPSKUQty"] = dr["BPSKUQty"].ToString();
                c.Rows.Add(r1);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(p);
            ds.Tables.Add(c);

            DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["ProductID"],
            ds.Tables["Child"].Columns["ProductID"]);
            //relation.Nested = true;
            ds.Relations.Add(relation);
            rep_MatrixReport.lbcomments.Visible = comments;
            rep_MatrixReport.DataSource = ds;

            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                if (Settings.ReportPrinterName != "") rep_MatrixReport.PrinterName = Settings.ReportPrinterName;
                rep_MatrixReport.CreateDocument();
                rep_MatrixReport.PrintingSystem.ShowMarginsWarning = false;
                rep_MatrixReport.PrintingSystem.ShowPrintStatusDialog = false;

                //rep_MatrixReport.ShowPreviewDialog();

                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                window.PreviewControl.DocumentSource = rep_MatrixReport;
                window.ShowDialog();

            }
            finally
            {
                rep_MatrixReport.Dispose();

                dtbl.Dispose();
            }
        }

        private void FDate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
