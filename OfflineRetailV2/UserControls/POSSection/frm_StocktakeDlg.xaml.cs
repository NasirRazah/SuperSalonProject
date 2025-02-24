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
using DevExpress.Xpf.Editors;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_StocktakeDlg.xaml
    /// </summary>
    public partial class frm_StocktakeDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_StocktakeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }
        private void OnClose(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private DataTable dtblPLU;
        private frm_StocktakeBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool blIsViewMode;
        public bool IsViewMode
        {
            get { return blIsViewMode; }
            set { blIsViewMode = value; }
        }
        public DataTable dPLU
        {
            get { return dtblPLU; }
            set { dtblPLU = value; }
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

        public frm_StocktakeBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_StocktakeBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }



        private int intPOID = 0;
        private bool boolControlChanged;

        private int intTimes = 1;
        private int intPrevRow;
        private bool blPOLeave = false;

        private double dblMinOrderAmount = 0;
        private int PrevColPID = 0;

        public void PopulateProduct(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchLookupData3(intOption);

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

            PART_Editor.ItemsSource = dtblTemp;

            PART_Editor.NullText = "";
            dbtblSKU.Dispose();
        }

        private void ShowHeader()
        {
            PosDataObject.StockJournal objstkj = new PosDataObject.StockJournal();
            objstkj.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objstkj.ShowStocktakeHeader(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                txtDoc.Text = dr["DocNo"].ToString();
                dtStk.DateTime = GeneralFunctions.fnDate(dr["DocDate"].ToString());
                txtNotes.Text = dr["DocComment"].ToString();
                tmDoc.DateTime = GeneralFunctions.fnDate(dr["DocDate"].ToString());
            }
        }

        private void ShowPLU()
        {
            PosDataObject.StockJournal objstkj = new PosDataObject.StockJournal();
            objstkj.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objstkj.ShowStocktakeDetailPLU(intID);
            int prevlsku = 0;
            int currlsku = 0;
            int slno = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                currlsku = GeneralFunctions.fnInt32(dr["LinkSKU"].ToString());
                if (currlsku == prevlsku) slno++;
                else
                {
                    slno = 0;
                    slno++;
                }
                dtblPLU.Rows.Add(new object[]{
                                    slno.ToString(),
                                    dr["ID"].ToString(),
                                    dr["ProductID"].ToString(),
                                    dr["SKU"].ToString(),
                                    dr["Description"].ToString(),
                                    GeneralFunctions.fnDouble(dr["BreakPackRatio"].ToString()),
                                    GeneralFunctions.fnDouble(dr["StkCount"].ToString()),
                                    GeneralFunctions.fnDouble(dr["SKUQty"].ToString()),
                                    dr["LinkSKU"].ToString()});
                prevlsku = currlsku;
            }
        }

        private void PopulateBatchGrid()
        {
            PosDataObject.StockJournal objRecv = new PosDataObject.StockJournal();
            objRecv.Connection = SystemVariables.Conn;

            DataTable dbtbl = new DataTable();
            dbtbl = objRecv.FetchStocktakeBrowseDetail(intID, 0, 0);
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

            grdDept.ItemsSource = dtblTemp;
            FillRowinGrid(((grdDept.ItemsSource) as DataTable).Rows.Count);
            gridView1.MoveFirstRow();
        }

        private void FillRowinGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = SystemVariables.FIXEDGRIDROWS - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdDept.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, strip });
                }

                grdDept.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private string GetAutoDocumentNo()
        {
            PosDataObject.StockJournal objstkj = new PosDataObject.StockJournal();
            objstkj.Connection = SystemVariables.Conn;
            return Settings.StocktakeDocPrefix + objstkj.GetNewStocktakeDoc();
        }

        private void PART_Editor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private async void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colProductID")
            {
                if (IsDuplicateItem(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation("This SKU has already been entered");

                    await Dispatcher.BeginInvoke(new Action(() => gridView1.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    boolControlChanged = true;
                    PrevColPID = GeneralFunctions.fnInt32(e.OldValue);
                    //e.Handled = true;
                    //gridView1.FocusedView.ShowEditor();
                    //if (gridView1.ActiveEditor != null) gridView1.ActiveEditor.Focus();


                    //await Dispatcher.BeginInvoke(new Action(() => gridView1.ShowEditor()));

                    /*PosDataObject.Tax objGroup = new PosDataObject.Tax();
                    objGroup.Connection = SystemVariables.Conn;
                    grdTax.SetCellValue(e.RowHandle, colTaxName, objGroup.GetTaxDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                    boolControlChangedItem = true;*/
                }

                
            }

            if (e.Column.Name == "colQty")
            {
                boolControlChanged = true;
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                if (e.Value.ToString() == "")
                {
                    e.Handled = true;
                    return;
                }

                double dblSKUQty = 0.00;

                double dblQtyVar = 0.00;
                double dblCostVar = 0.0;
                double dblPriceVar = 0.0;
                double dblCost = 0.00;
                double dblPriceA = 0.00;
                double dblQtyOnHand = 0.00;

                if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQtyOnHand) != "")
                    dblQtyOnHand = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQtyOnHand));
                if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA) != "")
                    dblPriceA = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA));

                dblSKUQty = GeneralFunctions.fnDouble(e.Value);

                dblQtyVar = dblSKUQty - dblQtyOnHand;
                dblCostVar = (dblSKUQty - dblQtyOnHand) * dblCost;
                dblPriceVar = (dblSKUQty - dblQtyOnHand) * dblPriceA;
                grdDept.SetCellValue(intRowNum, colStkCount, dblSKUQty);
                grdDept.SetCellValue(intRowNum, colQtyVar, dblQtyVar);
            }

            if (e.Column.Name == "colQtyOnHand")
            {
                boolControlChanged = true;
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                if (e.Value.ToString() == "")
                {
                    e.Handled = true;
                    return;
                }

                double dblSKUQty = 0.00;

                double dblQtyVar = 0.00;
                double dblCostVar = 0.0;
                double dblPriceVar = 0.0;
                double dblCost = 0.00;
                double dblPriceA = 0.00;
                double dblQtyOnHand = 0.00;

                if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                    dblSKUQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));
                if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA) != "")
                    dblPriceA = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA));

                dblQtyOnHand = GeneralFunctions.fnDouble(e.Value);

                dblQtyVar = dblSKUQty - dblQtyOnHand;
                dblCostVar = (dblSKUQty - dblQtyOnHand) * dblCost;
                dblPriceVar = (dblSKUQty - dblQtyOnHand) * dblPriceA;
                grdDept.SetCellValue(intRowNum, colStkCount, dblSKUQty);
                grdDept.SetCellValue(intRowNum, colQtyVar, dblQtyVar);
            }
        }

        private bool IsDuplicateItem(int refItem)
        {
            bool bDuplicate = false;
            DataTable dsource = grdDept.ItemsSource as DataTable;
            int intc = 0;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ProductID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["ProductID"].ToString()) == refItem)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private string GetSKU(string skutxt)
        {
            bool bActive = false;
            bool blFindBySKU = false;
            bool blFindByAltSKU = false;
            bool blFindByAltSKU2 = false;
            bool blFindByUPC = false;
            string SKU = "";

            if (IfExistsSKU(skutxt) == 1)
            {
                blFindBySKU = true;
                SKU = skutxt;
                if (IfActiveProduct(SKU) == 0)
                {

                    bActive = false;
                }
                else
                {
                    bActive = true;
                }
            }
            else
            {
                blFindBySKU = false;
            }
            if (!blFindBySKU)
            {
                if (IfExistsAltSKU(skutxt) == 1)
                {
                    blFindByAltSKU = true;
                    SKU = SKUfromAltSKU(skutxt);
                    if (IfActiveProduct(SKU) == 0)
                    {
                        bActive = false;
                    }
                    else
                    {
                        bActive = true;
                    }
                }
                else
                {
                    blFindByAltSKU = false;
                }

                if (!blFindByAltSKU)
                {
                    if (IfExistsAltSKU2(skutxt) == 1)
                    {
                        blFindByAltSKU2 = true;
                        SKU = SKUfromAltSKU2(skutxt);
                        if (IfActiveProduct(SKU) == 0)
                        {
                            bActive = false;
                        }
                        else
                        {
                            bActive = true;
                        }
                    }
                    else
                    {
                        blFindByAltSKU2 = false;
                    }

                    if (!blFindByAltSKU2)
                    {
                        if (IfExistsUPC(skutxt) == 1)
                        {
                            blFindByUPC = true;
                            SKU = SKUfromUPC(skutxt);
                            if (IfActiveProduct(SKU) == 0)
                            {
                                bActive = false;
                            }
                            else
                            {
                                bActive = true;
                            }
                        }
                        else
                        {
                            blFindByUPC = false;
                        }
                    }
                }


            }

            if (bActive)
            {
                return SKU;
            }
            else
            {
                return "";
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            PopulateProduct(0);
            dtStk.EditValue = DateTime.Now;
            tmDoc.DateTime = DateTime.Now;
            dtblPLU = new DataTable();

            dtblPLU.Columns.Add("SLNO", System.Type.GetType("System.String"));
            dtblPLU.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblPLU.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtblPLU.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblPLU.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblPLU.Columns.Add("BreakPackRatio", System.Type.GetType("System.Double"));
            dtblPLU.Columns.Add("StkCount", System.Type.GetType("System.Double"));
            dtblPLU.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
            dtblPLU.Columns.Add("LinkSKU", System.Type.GetType("System.String"));

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Inventory_Adjustment;
                txtDoc.Text = GetAutoDocumentNo();
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Inventory_Adjustment;
                ShowHeader();
                ShowPLU();
            }
            PopulateBatchGrid();

            if (blIsViewMode)
            {
                Title.Text = Properties.Resources.View_Inventory_Adjustment;
                btnPost.Visibility = Visibility.Hidden;
                dtStk.IsReadOnly = true;
                tmDoc.IsReadOnly = true;
                txtNotes.IsReadOnly = true;
                btnDelete.IsEnabled = false;
                colProductID.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                colQty.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            }

            boolControlChanged = false;
        }

        private int IfExistsSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateCount(SKU);
        }
        private int IfActiveProduct(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.IfActiveProduct(SKU);
        }
        private int IfExistsAltSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateAltSKUCount(SKU);
        }
        /// Check if Alt SKU 2 Exists
        private int IfExistsAltSKU2(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateAltSKU2Count(SKU);
        }
        private string SKUfromAltSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromAltSKU(SKU);
        }
        /// Get SKU from Alt SKU 2
        private string SKUfromAltSKU2(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromAltSKU2(SKU);
        }
        private int IfExistsUPC(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateUPCCount(SKU);
        }
        private string SKUfromUPC(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromUPC(SKU);
        }
        private int GetProductID(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetProductID(SKU);
        }

        private async void GrdDept_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                if (grdDept.CurrentColumn.Name == "colProductID")
                {
                    if (gridView1.ActiveEditor != null)
                    {
                        
                        string strscantxt = (gridView1.ActiveEditor as DevExpress.Xpf.Grid.LookUp.LookUpEdit).EditText;
                        if (strscantxt != "")
                        {
                            //string sku = GetSKU(strscantxt);
                            if (IfExistsSKU(strscantxt)  == 1)
                            {
                                if (IsDuplicateItem(GetProductID(strscantxt)))
                                {
                                    (gridView1.ActiveEditor as DevExpress.Xpf.Grid.LookUp.LookUpEdit).Text = "";
                                    await Dispatcher.BeginInvoke(new Action(() => gridView1.HideEditor()));
                                    grdDept.CurrentColumn = grdDept.Columns["ProductID"];
                                    e.Handled = true;
                                    return;
                                }
                                else
                                {
                                    grdDept.CurrentColumn = grdDept.Columns["Description"];
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else
                            {
                                grdDept.CurrentColumn = grdDept.Columns["ProductID"];
                                e.Handled = true;
                                return;
                            }
                        }
                        else
                        {
                            grdDept.CurrentColumn = grdDept.Columns["ProductID"];
                            e.Handled = true;
                            return;

                        }
                    }

                    //DocMessage.MsgInformation(grdDept.GetCellDisplayText(gridView1.FocusedRowHandle, colProductID));

                    /*if ( GeneralFunctions.fnInt32(grdDept.GetCellValue(gridView1.FocusedRowHandle,colProductID)) == 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        grdDept.CurrentColumn = grdDept.Columns["Description"];
                        e.Handled = true;
                        return;
                    }*/
                }
                


                if (grdDept.CurrentColumn.Name == "colQty")
                {
                    grdDept.CurrentColumn = grdDept.Columns["QtyOnHand"];
                    e.Handled = true;
                    return;
                }

                if (grdDept.CurrentColumn.Name == "colQtyOnHand")
                {
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
                    grdDept.CurrentColumn = grdDept.Columns["ProductID"];
                    e.Handled = true;
                    return;
                }
                

            }
        }

        private async void GridView1_FocusedColumnChanged(object sender, DevExpress.Xpf.Grid.FocusedColumnChangedEventArgs e)
        {
            try
            {
                bool isfocusedcost = false;
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;

                /*TOdo:if (intRowNum < 0)
                {
                    if (DevExpress.XtraGrid.GridControl.NewItemRowHandle == intRowNum)
                    {
                        if (e.FocusedColumn == colQty)
                        {
                            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                            gridView1.FocusedColumn = colProductID;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }*/


                if ((e.NewColumn.VisibleIndex == 3) ||
                    ((e.NewColumn.VisibleIndex == 4) && (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID)) != 0)
                    && (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colDesc)) == ""))
                {
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID) != "")
                    {
                        string strSKU = "";
                        string strDesciption = "";
                        double dblCost = 0.00;
                        double dblPriceA = 0.00;
                        double dblPriceB = 0.00;
                        double dblPriceC = 0.00;
                        double dblQtyOnHand = 0.00;
                        double dblBreakPackRatio = 0.00;
                        int intLinkSKU = 0;

                        if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID) != "")
                            GeneralFunctions.GetStockTakeSKUDetails(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID)),
                            ref strSKU, ref strDesciption, ref dblCost, ref dblPriceA, ref dblPriceB, ref dblPriceC, ref dblQtyOnHand, ref dblBreakPackRatio, ref intLinkSKU);
                        if (strDesciption.Trim() == "")	// Check for item code
                        {
                            if (intTimes == 1)
                                DocMessage.MsgInformation(Properties.Resources.Invalid_SKU);
                            grdDept.CurrentColumn = colProductID;
                            intTimes = 2;
                            return;
                        }

                        /*if (intLinkSKU == -1)
                        {
                            btnPLU.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            btnPLU.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }*/

                        if (PrevColPID != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID)))
                        {
                            grdDept.SetCellValue(intRowNum, colQty, dblQtyOnHand);
                            grdDept.SetCellValue(intRowNum, colStkCount, dblQtyOnHand);
                            grdDept.SetCellValue(intRowNum, colQtyOnHand, dblQtyOnHand);
                        }
                        else
                        {
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) == "")
                            {
                                grdDept.SetCellValue(intRowNum, colQty, dblQtyOnHand);
                                grdDept.SetCellValue(intRowNum, colStkCount, dblQtyOnHand);
                                grdDept.SetCellValue(intRowNum, colQtyOnHand, dblQtyOnHand);
                            }
                        }
                        grdDept.SetCellValue(intRowNum, colSKU, strSKU);
                        grdDept.SetCellValue(intRowNum, colDesc, strDesciption);
                        grdDept.SetCellValue(intRowNum, colPriceA, dblPriceA);
                        grdDept.SetCellValue(intRowNum, colPriceB, dblPriceB);
                        grdDept.SetCellValue(intRowNum, colPriceC, dblPriceC);
                        grdDept.SetCellValue(intRowNum, colBreakPackRatio, dblBreakPackRatio);
                        grdDept.SetCellValue(intRowNum, colLinkSKU, intLinkSKU);

                        double dblSKUQty = 0.00;

                        double dblQtyVar = 0.00;
                        double dblCostVar = 0.0;
                        double dblPriceVar = 0.0;

                        if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                            dblSKUQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));
                        if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQtyOnHand) != "")
                            dblQtyOnHand = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQtyOnHand));
                        if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA) != "")
                            dblPriceA = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPriceA));

                        dblQtyVar = dblSKUQty - dblQtyOnHand;
                        dblCostVar = (dblSKUQty - dblQtyOnHand) * dblCost;
                        dblPriceVar = (dblSKUQty - dblQtyOnHand) * dblPriceA;

                        grdDept.SetCellValue(intRowNum, colQtyVar, dblQtyVar);
                        grdDept.CurrentColumn = colQty;
                        isfocusedcost = true;

                        await Dispatcher.BeginInvoke(new Action(() => {
                            gridView1.ShowEditor();
                        }), System.Windows.Threading.DispatcherPriority.Loaded);
                        if (gridView1.ActiveEditor != null)
                        {
                            gridView1.ActiveEditor.SelectAll();
                        }
                    }
                }

            }
            finally
            {
                await Dispatcher.BeginInvoke(new Action(() => {
                    gridView1.ShowEditor();
                }), System.Windows.Threading.DispatcherPriority.Loaded);
                if (gridView1.ActiveEditor != null)
                {
                    gridView1.ActiveEditor.SelectAll();
                }
            }
        }

        private void InitializePLUDataTable(int lnksku)
        {
            bool findlnksku = false;

            foreach (DataRow dr in dtblPLU.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["LinkSKU"].ToString()) == lnksku)
                {
                    findlnksku = true;
                    break;
                }
            }
            if (!findlnksku)
            {
                PosDataObject.Product objp = new PosDataObject.Product();
                objp.Connection = SystemVariables.Conn;
                DataTable pdtbl = new DataTable();
                pdtbl = objp.GetBreakPackItemFromSKU(lnksku);
                int slno = 0;
                foreach (DataRow dr in pdtbl.Rows)
                {
                    slno++;
                    dtblPLU.Rows.Add(new object[] { slno.ToString(), "0", dr["ID"].ToString(), dr["SKU"].ToString(),
                                                    dr["Description"].ToString(),GeneralFunctions.fnDouble(dr["BreakPackRatio"].ToString()),0,0,lnksku.ToString()});
                }
            }
        }

        private DataTable FilterPLUTable(int lnksku)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("SLNO", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
            dtbl.Columns.Add("BreakPackRatio", System.Type.GetType("System.Double"));
            dtbl.Columns.Add("StkCount", System.Type.GetType("System.Double"));
            dtbl.Columns.Add("SKUQty", System.Type.GetType("System.Double"));
            dtbl.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
            foreach (DataRow dr in dtblPLU.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["LinkSKU"].ToString()) == lnksku)
                {
                    dtbl.Rows.Add(new object[] { dr["SLNO"].ToString(),
                                                    dr["ID"].ToString(),
                                                    dr["ProductID"].ToString(),
                                                    dr["SKU"].ToString(),
                                                    dr["Description"].ToString(),
                                                    GeneralFunctions.fnDouble(dr["BreakPackRatio"].ToString()),
                                                    GeneralFunctions.fnDouble(dr["StkCount"].ToString()),
                                                    GeneralFunctions.fnDouble(dr["SKUQty"].ToString()),
                                                    dr["LinkSKU"].ToString()});
                }
            }
            return dtbl;
        }

        private void AdjustPLUTable(int lnksku, DataTable filterdata, ref double rqty)
        {
            double qty = 0;
            foreach (DataRow dr in dtblPLU.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["LinkSKU"].ToString()) == lnksku)
                {
                    foreach (DataRow dr1 in filterdata.Rows)
                    {
                        if ((GeneralFunctions.fnInt32(dr["LinkSKU"].ToString()) == GeneralFunctions.fnInt32(dr1["LinkSKU"].ToString()))
                            && (GeneralFunctions.fnInt32(dr["ProductID"].ToString()) == GeneralFunctions.fnInt32(dr1["ProductID"].ToString())))
                        {
                            dr["StkCount"] = dr1["StkCount"].ToString();
                            dr["SKUQty"] = dr1["SKUQty"].ToString();
                            if (GeneralFunctions.fnDouble(dr1["SKUQty"].ToString()) != 0)
                                qty = qty + GeneralFunctions.fnDouble(dr1["SKUQty"].ToString());
                        }
                    }
                }
            }
            rqty = qty;
        }


        private bool IsValidFooter()
        {
            DataTable dtbl1 = new DataTable();
            dtbl1 = grdDept.ItemsSource as DataTable;

            int intcount = 0;
            foreach (DataRow dr in dtbl1.Rows)
            {

                if ((dr["ProductID"].ToString() == "") && (dr["SKUQty"].ToString() == "") && (dr["Description"].ToString() == "")
                    && (dr["QtyOnHand"].ToString() == "") && (dr["QtyVar"].ToString() == ""))
                {
                    intcount++;
                }

            }
            if (intcount == dtbl1.Rows.Count)
            {
                DocMessage.MsgEnter("Item");
                GeneralFunctions.SetFocus(grdDept);
                gridView1.FocusedRowHandle = 0;
                grdDept.CurrentColumn = colProductID;
                return false;
            }
            dtbl1.Dispose();

            DataTable dtbl = new DataTable();
            dtbl = grdDept.ItemsSource as DataTable;

            int introwhandle = -1;
            foreach (DataRow dr in dtbl.Rows)
            {
                introwhandle++;
                if (dr["ProductID"].ToString() == "") continue;
                if (dr["ProductID"].ToString() != "")
                {
                    if (dr["SKUQty"].ToString() == "")
                    {
                        DocMessage.MsgEnter("Count");
                        gridView1.FocusedRowHandle = introwhandle;
                        grdDept.CurrentColumn = colQty;
                        return false;
                    }
                }

            }
            dtbl.Dispose();
            return true;
        }

        private bool IsValidHeader()
        {
            if (dtStk.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Inventory_Adjustment_Date);
                GeneralFunctions.SetFocus(dtStk);
                return false;
            }

            if (tmDoc.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Time);
                GeneralFunctions.SetFocus(tmDoc);
                return false;
            }

            if (dtStk.EditValue != null)
            {
                if (dtStk.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Inventory_Adjustment_Date_can_not_be_after_today);
                    GeneralFunctions.SetFocus(dtStk);
                    return false;
                }
            }



            return true;
        }

        private bool ValidAllFields()
        {
            if (IsValidHeader() && IsValidFooter())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private DateTime GetPostDateTime()
        {
            DateTime dt = new DateTime(dtStk.DateTime.Year, dtStk.DateTime.Month, dtStk.DateTime.Day, GeneralFunctions.fnDate(tmDoc.EditValue).Hour,
                GeneralFunctions.fnDate(tmDoc.EditValue).Minute, 0);
            return dt;
        }

        private bool SaveData()
        {
            if (ValidAllFields())
            {
                string ErrMsg = "";
                bool blUpdate = false;
                gridView1.PostEditor();

                PosDataObject.StockJournal objjr = new PosDataObject.StockJournal();
                objjr.Connection = SystemVariables.Conn;
                objjr.ID = intID;
                objjr.IsViewMode = blIsViewMode;
                objjr.DocDate = GetPostDateTime();
                objjr.DocComment = txtNotes.Text.Trim();
                objjr.StkDocNoPrefix = Settings.StocktakeDocPrefix;
                objjr.dStkDetail = grdDept.ItemsSource as DataTable;
                CheckPLUDataTable();
                objjr.dStkPLU = dtblPLU;
                objjr.LoginUserID = SystemVariables.CurrentUserID;

                //B e g i n   T r a n s a ct i o n
                bool istrans = false;
                objjr.BeginTransaction();
                if (objjr.StocktakeTransaction())
                {
                    intNewID = objjr.ID;
                    istrans = true;
                }
                objjr.EndTransaction();
                //E n d  T r a n s a ct i o n

                if (!istrans)
                {
                    DocMessage.ShowException("Saving Inventory Adjustment data", ErrMsg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }

        private void CheckPLUDataTable()
        {
            DataTable dtbl = new DataTable();
            dtbl = grdDept.ItemsSource as DataTable;
            if (dtblPLU.Rows.Count > 0)
            {
                DataTable dtblT1 = new DataTable();
                DataTable dtblT2 = new DataTable();
                dtblT1.Columns.Add("LinkSKU", System.Type.GetType("System.String"));
                dtblT2 = dtblT1;
                foreach (DataRow dr in dtblPLU.Rows)
                {
                    bool bflg = true;
                    foreach (DataRow dr1 in dtblT2.Rows)
                    {
                        if (dr["LinkSKU"].ToString() != dr1["LinkSKU"].ToString())
                        {
                            bflg = false;
                            break;
                        }
                    }
                    if ((!bflg) || (dtblT2.Rows.Count == 0))
                    {
                        dtblT1.Rows.Add(new object[] { dr["LinkSKU"].ToString() });
                        dtblT2 = dtblT1;
                    }
                }

                foreach (DataRow dr2 in dtblT1.Rows)
                {
                    bool bflg1 = false;
                    foreach (DataRow dr3 in dtbl.Rows)
                    {
                        if (dr3["ProductID"].ToString() == "") continue;
                        if (dr2["LinkSKU"].ToString() == dr3["ProductID"].ToString())
                        {
                            bflg1 = true;
                            break;
                        }
                    }
                    if (!bflg1)
                    {
                        string filtr = "LinkSKU = " + dr2["LinkSKU"].ToString();
                        DataRow[] d = dtblPLU.Select(filtr);
                        foreach (DataRow r in d)
                        {
                            r.Delete();
                        }
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
            objj.dStkDetail = grdDept.ItemsSource as DataTable;
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
            dtbl = grdDept.ItemsSource as DataTable;
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

        private void BtnPost_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                if (ValidForPost(intNewID))
                {
                    if (PostDocument(intNewID, Settings.StocktakeDocPrefix + intNewID.ToString()))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Successfully_posted);
                    }
                    else
                    {
                        DocMessage.MsgInformation(Properties.Resources.Error_posting);
                        return;
                    }
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources.One_more_products_posted_other_Inventory_Adjustment);
                    return;
                }
                boolControlChanged = false;
                BrowseForm.Flag = false;
                BrowseForm.FDate.EditValue = dtStk.DateTime;
                BrowseForm.TDate.EditValue = dtStk.DateTime;
                BrowseForm.Flag = true;
                BrowseForm.FetchData(dtStk.DateTime, dtStk.DateTime);
                CloseKeyboards();
                Close();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                BrowseForm.Flag = false;
                BrowseForm.FDate.EditValue = dtStk.DateTime;
                BrowseForm.TDate.EditValue = dtStk.DateTime;
                BrowseForm.Flag = true;
                BrowseForm.FetchData(dtStk.DateTime, dtStk.DateTime);
                CloseKeyboards();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDept.ItemsSource as DataTable).Rows.Count <= 1) return;
            int intRowNum = 0;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colProductID)) != 0)
            {
                if (DocMessage.MsgDelete())
                {
                    boolControlChanged = true;
                    DataTable dtbl = new DataTable();
                    dtbl = (grdDept.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdDept.ItemsSource = dtbl;
                    FillRowinGrid(((grdDept.ItemsSource) as DataTable).Rows.Count);
                    dtbl.Dispose();
                }
            }
        }

        private void DtStk_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TmDoc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtNotes_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void GrdDept_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdDept.CurrentColumn.Name == "colProductID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdDept.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (SaveData())
                    {

                        boolControlChanged = false;
                        BrowseForm.Flag = false;
                        BrowseForm.FDate.EditValue = dtStk.DateTime;
                        BrowseForm.TDate.EditValue = dtStk.DateTime;
                        BrowseForm.Flag = true;
                        BrowseForm.FetchData(dtStk.DateTime, dtStk.DateTime);
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void GridView1_FocusedRowHandleChanged(object sender, DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs e)
        {
            PrevColPID = 0;
        }

        private void PART_Editor_GotFocus(object sender, RoutedEventArgs e)
        {
            DocMessage.MsgInformation(gridView1.FocusedRowHandle.ToString());
        }

        private async void GridView1_ShownEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e)
        {
            if (e.Column.Name == "colProductID")
            {
                PrevColPID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(e.RowHandle, grdDept, colProductID));
                CloseKeyboards();
            }

            if (e.Column.FieldName == "SKUQty")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0.00";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "StockTake";
                    nkybrd.GridColumnName = "SKUQty";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0));
                    nkybrd.Left = location.X - 385;
                    if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                    {
                        nkybrd.Top = location.Y - 270;
                    }
                    else
                    {
                        nkybrd.Top = location.Y + 35;
                    }

                    nkybrd.Height = 270;
                    nkybrd.Width = 385;
                    nkybrd.IsWindow = true;
                    nkybrd.CalledForm = this;
                    nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                    nkybrd.Show();
                    IsAboutNumKybrdOpen = true;
                }
            }

            if (e.Column.FieldName == "QtyOnHand")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0.00";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "StockTake";
                    nkybrd.GridColumnName = "QtyOnHand";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0));
                    nkybrd.Left = location.X - 385;
                    if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                    {
                        nkybrd.Top = location.Y - 270;
                    }
                    else
                    {
                        nkybrd.Top = location.Y + 35;
                    }

                    nkybrd.Height = 270;
                    nkybrd.Width = 385;
                    nkybrd.IsWindow = true;
                    nkybrd.CalledForm = this;
                    nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                    nkybrd.Show();
                    IsAboutNumKybrdOpen = true;
                }
            }
        }

        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void DtStk_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }


        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X - 385;
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }


        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        public void UpdateGridValueByOnscreenKeyboard(string strgridname, string gridcol, int rindx, string val)
        {


            if (gridcol == "SKUQty")
            {
                double dblCostVar = 0.0;
                double dblPriceVar = 0.0;
                double dblCost = 0.00;
                double dblPriceA = 0.00;

                grdDept.SetCellValue(rindx, colQty, val);

                double dblSKUQty = GeneralFunctions.fnDouble(grdDept.GetCellValue(rindx, colQty));
                double dblQtyOnHand = GeneralFunctions.fnDouble(grdDept.GetCellValue(rindx, colQtyOnHand));

                //dblPriceA = GeneralFunctions.fnDouble(grdDept.GetCellValue(rindx, colPriceA));

                //dblCostVar = (dblSKUQty - dblQtyOnHand) * dblCost;
                //dblPriceVar = (dblSKUQty - dblQtyOnHand) * dblPriceA;

                double dblQtyVar = dblSKUQty - dblQtyOnHand;
                
                grdDept.SetCellValue(rindx, colQtyVar, dblQtyVar);
            }
            if (gridcol == "QtyOnHand")
            {
                grdDept.SetCellValue(rindx, colQtyOnHand, val);
                double dblSKUQty = GeneralFunctions.fnDouble(grdDept.GetCellValue(rindx, colQty));
                double dblQtyOnHand = GeneralFunctions.fnDouble(grdDept.GetCellValue(rindx, colQtyOnHand));


                double dblQtyVar = dblSKUQty - dblQtyOnHand;

                grdDept.SetCellValue(rindx, colQtyVar, dblQtyVar);
            }


        }

        private void PART_Editor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor.Text != "")
            {
                if (e.Key == Key.Return)
                DocMessage.MsgInformation(editor.Text);
            }
        }
    }
}
