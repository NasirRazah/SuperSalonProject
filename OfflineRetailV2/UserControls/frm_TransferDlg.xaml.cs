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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using OfflineRetailV2.Data;
using System.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_TransferDlg.xaml
    /// </summary>
    public partial class frm_TransferDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_TransferBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private int intTimes = 1;
        private int intPrevRow;
        private string TransferFrom = "";
        private string PrevState = "Pending";
        bool blReady = false;

        public frm_TransferDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
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

        public frm_TransferBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_TransferBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateWebStores()
        {
            PosDataObject.Central objwb = new PosDataObject.Central();
            objwb.Connection = SystemVariables.Conn;
            DataTable dbtblstore = new DataTable();
            dbtblstore = objwb.FetchOtherStores(Settings.StoreCode);

            cmbToStore.ItemsSource = dbtblstore;
            cmbToStore.SelectedIndex = -1;
            dbtblstore.Dispose();
        }

        public void PopulateEmployee()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtblEmp = new DataTable();
            dbtblEmp = objEmployee.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblEmp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            txtCheckClerk.ItemsSource = dtblTemp;
            txtCheckClerk.EditValue = null;
        }

        public void PopulateProduct(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.LookupItemForTransfer(intOption);
            if (intOption == 0)
            {


            }
            if (intOption == 1)
            {


            }

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

            PART_Editor_SKU.ItemsSource = dtblTemp;

        }

        private void PopulateBatchGrid()
        {
            try
            {
                //grdDept.SuspendLayout();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;

                DataTable dbtbl = objPO.ShowTransferDetailRecord(intID);

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

                grdDeatil.ItemsSource = dtblTemp;
                FillRowinGrid(((grdDeatil.ItemsSource) as DataTable).Rows.Count);
                gridView1.MoveFirstRow();
            }
            finally
            {
                //grdDept.ResumeLayout();
            }
        }

        private void FillRowinGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = SystemVariables.FIXEDGRIDROWS - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdDeatil.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { 0, null, null, null, null, null, null, null, strip });
                }
                grdDeatil.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        public void ShowHeaderData()
        {
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objPO.ShowTransferHeaderRecord(intID);

            foreach (DataRow dr in dbtbl.Rows)
            {
                cmbToStore.EditValue = dr["ToStore"].ToString();
                txtPO.Text = dr["TransferNo"].ToString();
                txtNotes.Text = dr["GeneralNotes"].ToString();
                txtCheckClerk.EditValue = dr["CheckInClerk"].ToString();
                dtOrderDate.EditValue = GeneralFunctions.fnDate(dr["TransferDate"].ToString());
                if (dr["Status"].ToString() == "Pending") cmbStatus.SelectedIndex = 0;
                if (dr["Status"].ToString() == "Ready") cmbStatus.SelectedIndex = 1;
                numQty.Text = GeneralFunctions.fnDouble(dr["TotalQty"].ToString()).ToString("f2");
                numTotal.Text = GeneralFunctions.fnDouble(dr["TotalCost"].ToString()).ToString("f2");
            }
            dbtbl.Dispose();
            PrevState = cmbStatus.SelectedIndex == 0 ? "Pending" : "Ready";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            PopulateProduct(0);
            PopulateWebStores();
            PopulateEmployee();
            
            if (intID == 0)
            {
                Title.Text = Properties.Resources.New_Transfer;
                dtOrderDate.EditValue = DateTime.Today.Date;
                txtPO.Text = "AUTO";
                lbThisStore.Text = Settings.StoreCode + " - " + Settings.StoreName;
                TransferFrom = Settings.StoreCode;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Transfer;
                ShowHeaderData();
                cmbToStore.IsReadOnly = true;
                lbThisStore.Text = Settings.StoreCode + " - " + Settings.StoreName;
                TransferFrom = Settings.StoreCode;

                if (cmbStatus.Text == "Sent")
                {
                    blReady = true;
                    Title.Text = Properties.Resources.View_Transfer;
                    /*panel1.Enabled = false;
                    chkSKUVariarion.Enabled = false;
                    Text = Translation.SetMultilingualTextInCodes(" View Transfer", "frmTransferDlg_ViewTransfer");
                    colSKU.AppearanceCell.BackColor = SystemColors.Window;
                    colSKU.AppearanceCell.ForeColor = SystemColors.ControlText;
                    colSKU.AppearanceCell.Options.UseBackColor = true;
                    colSKU.AppearanceCell.Options.UseForeColor = true;
                    colSKU.OptionsColumn.AllowEdit = false;
                    colQty.OptionsColumn.AllowEdit = false;*/

                }
            }
            PopulateBatchGrid();
            boolControlChanged = false;
        }

        private void GetAllAmount()
        {
            if (grdDeatil.ItemsSource as DataTable == null) return;
            DataTable TempTbl = new DataTable();
            gridView1.PostEditor();
            grdDeatil.UpdateTotalSummary();
            TempTbl = grdDeatil.ItemsSource as DataTable;
            double dblQty = 0;
            double dblNet = 0;
            foreach (DataRow dr in TempTbl.Rows)
            {
                if ((dr["ProductID"].ToString() != "0") && (dr["Cost"].ToString() != "") && (dr["Qty"].ToString() != ""))
                {
                    if (dr["Qty"].ToString() != "")
                    {
                        dblQty = dblQty + GeneralFunctions.fnDouble(dr["Qty"].ToString());

                    }

                    if (dr["Total"].ToString() != "")
                    {
                        dblNet = dblNet + GeneralFunctions.fnDouble(dr["Total"].ToString());
                    }

                }

            }

            TempTbl.Dispose();

            numQty.Text = dblQty.ToString();
            numTotal.Text = dblNet.ToString();
        }

        private bool IsValidFooter()
        {
            DataTable dtbl1 = new DataTable();
            dtbl1 = grdDeatil.ItemsSource as DataTable;

            int intcount = 0;
            foreach (DataRow dr in dtbl1.Rows)
            {
                if ((dr["ProductID"].ToString() == "0") && (dr["Description"].ToString() == "")
                    && (dr["Cost"].ToString() == "") && (dr["Qty"].ToString() == "") && (dr["Total"].ToString() == ""))
                {
                    intcount++;
                }
            }
            if (intcount == dtbl1.Rows.Count)
            {
                DocMessage.MsgEnter(Properties.Resources.Item);
                GeneralFunctions.SetFocus(grdDeatil);
                gridView1.FocusedRowHandle = 0;
                grdDeatil.CurrentColumn = colSKU;
                return false;
            }
            dtbl1.Dispose();

            DataTable dtbl = new DataTable();
            dtbl = grdDeatil.ItemsSource as DataTable;

            int introwhandle = -1;
            foreach (DataRow dr in dtbl.Rows)
            {
                introwhandle++;
                if (dr["ProductID"].ToString() == "0") continue;
                if (dr["ProductID"].ToString() != "0")
                {
                    if (GeneralFunctions.fnDouble(dr["Qty"].ToString()) <= 0)
                    {
                        DocMessage.MsgEnter(Properties.Resources.Quantity);
                        gridView1.FocusedRowHandle = introwhandle;
                        grdDeatil.CurrentColumn = colQty;
                        return false;
                    }
                }
            }
            dtbl.Dispose();

            return true;
        }

        private bool IsValidHeader()
        {

            if (dtOrderDate.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Transfer_Date);
                GeneralFunctions.SetFocus(dtOrderDate);
                return false;
            }

            if (cmbToStore.Text.Trim() == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Select_Store_to_transfer);
                GeneralFunctions.SetFocus(cmbToStore);
                return false;
            }

            if (dtOrderDate.Text.Trim() != "")
            {
                if (dtOrderDate.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Transfer_Date_can_not_be_after_today);
                    GeneralFunctions.SetFocus(dtOrderDate);
                    return false;
                }
            }

            if (cmbStatus.SelectedIndex == 1)
            {
                string invalidstock = "";

                DataTable dtbl1 = new DataTable();
                dtbl1 = grdDeatil.ItemsSource as DataTable;

                foreach (DataRow dr in dtbl1.Rows)
                {
                    if ((dr["ProductID"].ToString() != "0") && (GeneralFunctions.fnDouble(dr["Qty"].ToString()) > 0))
                    {
                        string prodname = "";
                        double onhandqty = 0;
                        bool allowzerostock = false;

                        int prd = GeneralFunctions.fnInt32(dr["ProductID"].ToString());
                        DataTable dTemp = new DataTable();
                        PosDataObject.Product objProd = new PosDataObject.Product();
                        objProd.Connection = SystemVariables.Conn;
                        dTemp = objProd.ShowRecord(prd);
                        foreach (DataRow drt in dTemp.Rows)
                        {
                            allowzerostock = drt["AllowZeroStock"].ToString() == "Y";
                            onhandqty = GeneralFunctions.fnDouble(drt["QtyOnHand"].ToString());
                            prodname = drt["Description"].ToString();
                        }
                        dTemp.Dispose();
                        if (!allowzerostock)
                        {
                            if (onhandqty - (GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["PQty"].ToString())) < 0)
                            {
                                invalidstock = invalidstock == "" ? "Inadequate Stock\n\n" + prodname + " ( qty - " + GeneralFunctions.fnDouble(dr["Qty"].ToString()).ToString("0.##") + " )"
                                    : invalidstock + "\n" + prodname + " ( qty - " + GeneralFunctions.fnDouble(dr["Qty"].ToString()).ToString("0.##") + " )";
                            }
                        }
                    }

                }

                if (invalidstock != "")
                {
                    DocMessage.MsgInformation(invalidstock);
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

        private void GrdDeatil_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (grdDeatil.CurrentColumn.Name == "colSKU")
                {
                    if (GeneralFunctions.fnInt32(grdDeatil.GetCellValue(gridView1.FocusedRowHandle, colSKU)) == 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        grdDeatil.CurrentColumn = grdDeatil.Columns["Qty"];
                        e.Handled = true;
                        return;
                    }
                }

                if (grdDeatil.CurrentColumn.Name == "colQty")
                {
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
                    grdDeatil.CurrentColumn = grdDeatil.Columns["ProductID"];
                    e.Handled = true;
                    return;
                }
            }

            
        }

        private async void GridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colQty")
            {
                boolControlChanged = false;
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                if (e.Value.ToString() == "")
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    double dblQty = 0.00;
                    double dblAmount = 0.00;
                    double dblRate = 0.0;
                    double dblFreight = 0.0;
                    double dblTax = 0.0;

                    int intcaseqty = 0;

                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost));
                    
                    dblQty = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        dblAmount = (dblQty * dblRate);
                    }
                    catch
                    {
                        dblAmount = 0.00;
                    }
                    dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    grdDeatil.SetCellValue(intRowNum, colTotal, dblAmount);
                    GetAllAmount();
                }
            }
        }

        private async void GridView1_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            try
            {
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                /*Todo:if (intRowNum < 0)
                {
                    if (DevExpress.XtraGrid.GridControl.NewItemRowHandle == intRowNum)
                    {
                        if (e.FocusedColumn == colTotal)
                        {
                            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                            gridView1.FocusedColumn = colSKU;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }*/

                //if (e.FocusedColumn == null) return;

                if (e.NewColumn.VisibleIndex == 3)
                {
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colSKU) == "")
                    {
                        return;
                    }
                    else
                    {
                        if ((await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colSKU) != "") && (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) == ""))
                        {
                            string strSKU = "";
                            string strDesciption = "";
                            double dblCost = 0.00;
                            double dblPriceA = 0;
                            double dblPriceB = 0;
                            double dblPriceC = 0;
                            double OnHandQty = 0;
                            double BrkPk = 0;
                            int Link = 0;
                            GeneralFunctions.GetStockTakeSKUDetails(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colSKU)),
                                        ref strSKU, ref strDesciption, ref dblCost, ref dblPriceA, ref dblPriceB, ref dblPriceC, ref OnHandQty, ref BrkPk, ref Link);
                            if (strDesciption.Trim() == "")	// Check for item code
                            {
                                if (intTimes == 1)
                                    DocMessage.MsgInformation(Properties.Resources.Invalid_SKU);
                                grdDeatil.CurrentColumn = colSKU;
                                intTimes = 2;
                                return;
                            }
                            //grdDeatil.SetCellValue(intRowNum, colDecimalPlace, DP);

                            /*if (Settings.Use4Decimal == "N")
                            {
                                if (DP == "2")
                                {
                                    repCost.EditFormat.FormatString = "f";
                                    repCost.DisplayFormat.FormatString = "f";
                                    //colCost.ColumnEdit.Assign(repCost as DevExpress.XtraEditors.Repository.RepositoryItem);
                                }
                                else
                                {
                                    repCost.EditFormat.FormatString = "f3";
                                    repCost.DisplayFormat.FormatString = "f3";
                                    //colCost.ColumnEdit.Assign(repCost3 as DevExpress.XtraEditors.Repository.RepositoryItem);
                                }
                            }
                            else
                            {
                                repCost.EditFormat.FormatString = "f4";
                                repCost.DisplayFormat.FormatString = "f4";
                            }*/

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) == "") grdDeatil.SetCellValue(intRowNum, colQty, "1");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) == "") grdDeatil.SetCellValue(intRowNum, colCost, dblCost);

                            grdDeatil.SetCellValue(intRowNum, colSKU1, strSKU);
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colDesc) == "") grdDeatil.SetCellValue(intRowNum, colDesc, strDesciption);
                            

                            double dblQty = 0.00;
                            double dblAmount = 0.00;
                            double dblRate = 0.0;
                            double dblFreight = 0.0;
                            double dblTax = 0.0;

                            double dblCsQty = 0.00;


                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) != "")
                                dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost));
                            
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) != "")
                                dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty));

                            
                            try
                            {
                                dblAmount = (dblQty * dblRate);
                                
                            }
                            catch
                            {
                                dblAmount = 0.00;
                            }
                            dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                            grdDeatil.SetCellValue(intRowNum, colTotal, dblAmount);

                            grdDeatil.CurrentColumn = colQty;

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
                GetAllAmount();
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

        private void PART_Editor_SKU_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtCheckClerk_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private async void BtnNotes_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_NotesDlg frm_NotesDlg = new frm_NotesDlg();
            try
            {
                frm_NotesDlg.Title.Text = Properties.Resources.Notes;
                frm_NotesDlg.Notes = (await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDeatil, colNotes)).ToString();
                frm_NotesDlg.ShowDialog();
                if (frm_NotesDlg.OK)
                {
                    boolControlChanged = true;
                    grdDeatil.SetCellValue(gridView1.FocusedRowHandle, colNotes, frm_NotesDlg.Notes);
                }

            }
            finally
            {
                frm_NotesDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDeatil.ItemsSource as DataTable).Rows.Count <= 1) return;
            int intRowNum = 0;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colSKU) != "")
            {
                if (DocMessage.MsgDelete())
                {
                    boolControlChanged = true;
                    DataTable dtbl = new DataTable();
                    dtbl = (grdDeatil.ItemsSource) as DataTable;
                    dtbl.Rows[intRowNum].Delete();
                    grdDeatil.ItemsSource = dtbl;
                    FillRowinGrid(((grdDeatil.ItemsSource) as DataTable).Rows.Count);
                    dtbl.Dispose();
                }
            }
            GetAllAmount();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private bool SaveData()
        {
            if (blReady) return true;
            else
            {
                if (ValidAllFields())
                {
                    string ErrMsg = "";
                    gridView1.PostEditor();
                    GetAllAmount();
                    PosDataObject.PO objPO = new PosDataObject.PO();
                    objPO.Connection = SystemVariables.Conn;
                    objPO.TransferFrom = TransferFrom;
                    objPO.TransferTo = cmbToStore.EditValue.ToString();
                    objPO.OrderNo = txtPO.Text.Trim();
                    objPO.TransferStatus = cmbStatus.SelectedIndex == 0 ? "Pending" : "Ready";
                    objPO.GeneralNotes = txtNotes.Text.Trim();
                    if (txtCheckClerk.EditValue != null) objPO.CheckInClerk = GeneralFunctions.fnInt32(txtCheckClerk.EditValue.ToString());
                    else objPO.CheckInClerk = 0;

                    objPO.OrderDate = dtOrderDate.DateTime.Date;
                    objPO.GrossAmount = GeneralFunctions.fnDouble(numQty.Text);
                    objPO.NetAmount = GeneralFunctions.fnDouble(numTotal.Text);

                    DataTable dtblRearrangeDetail = grdDeatil.ItemsSource as DataTable;
                    foreach (DataRow dr in dtblRearrangeDetail.Rows)
                    {
                        if (dr["ProductID"].ToString() == "0")
                        {
                            dr["ProductID"] = "";
                        }
                    }

                    objPO.SplitDataTable = dtblRearrangeDetail;
                    objPO.LoginUserID = SystemVariables.CurrentUserID;
                    objPO.ErrorMsg = "";
                    objPO.ID = intID;
                    objPO.TerminalName = Settings.TerminalName;
                    if (PrevState == "Ready")
                    {
                        objPO.bAdjustStockForReadyState = true;
                    }
                    else
                    {
                        objPO.bAdjustStockForReadyState = false;
                    }
                    if (intID == 0)
                    {
                        objPO.Mode = "Add";
                    }
                    else
                    {
                        objPO.Mode = "Edit";
                    }

                    //B e g i n   T r a n s a ct i o n
                    objPO.BeginTransaction();
                    if (objPO.PostTransfer())
                    {
                        int intNewBatchID = objPO.ID;
                    }
                    objPO.EndTransaction();
                    //E n d  T r a n s a ct i o n

                    ErrMsg = objPO.ErrorMsg;
                    if (ErrMsg != "")
                    {
                        DocMessage.ShowException("Saving Transfer", ErrMsg);
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
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                BrowseForm.Flag = false;

                BrowseForm.FDate.EditValue = dtOrderDate.DateTime;
                BrowseForm.TDate.EditValue = dtOrderDate.DateTime;
                BrowseForm.Flag = true;
                BrowseForm.FetchData(dtOrderDate.DateTime, dtOrderDate.DateTime);
                CloseKeyboards();
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (SaveData())
                    {
                        boolControlChanged = false;
                        BrowseForm.Flag = false;

                        BrowseForm.FDate.EditValue = dtOrderDate.DateTime;
                        BrowseForm.TDate.EditValue = dtOrderDate.DateTime;
                        BrowseForm.Flag = true;
                        BrowseForm.FetchData(dtOrderDate.DateTime, dtOrderDate.DateTime);
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtPO_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbStatus_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtOrderDate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
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

        private void GridView1_ShownEditor(object sender, EditorEventArgs e)
        {
            if (e.Column.FieldName == "Qty")
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
                    nkybrd.WindowName = "Transfer";
                    nkybrd.GridColumnName = "Qty";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0)); nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
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

        public void UpdateGridValueByOnscreenKeyboard(string strgridname, string gridcol, int rindx, string val)
        {
            if (gridcol == "Qty") grdDeatil.SetCellValue(rindx, colQty, val);
           
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
