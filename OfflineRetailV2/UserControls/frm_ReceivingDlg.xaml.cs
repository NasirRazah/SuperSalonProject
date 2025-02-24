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
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_ReceivingDlg.xaml
    /// </summary>
    public partial class frm_ReceivingDlg : Window
    {

        double TaxPercentage = 20.0;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_ReceivingBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private int intPOID = 0;
        private bool boolControlChanged;

        private int intTimes = 1;
        private int intPrevRow;
        private bool blPOLeave = false;
        private bool boolLoading = false;
        private double dblMinOrderAmount = 0;

        public frm_ReceivingDlg()
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

        public frm_ReceivingBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_ReceivingBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateVendor()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dbtblVendor = new DataTable();
            dbtblVendor = objVendor.FetchLookupData("D");

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblVendor.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbVendor.ItemsSource = dtblTemp;
            cmbVendor.EditValue = null;
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
            txtRecvClerk.ItemsSource = dtblTemp;

            txtCheckClerk.EditValue = null;
            txtRecvClerk.EditValue = null;
        }

        private void SetDecimalPlace()
        {
            /*
            if (Settings.Use4Decimal == "N")
            {
                //numGrossAmt.Decimals = Settings.DecimalPlace;
                //numFreight.Decimals = Settings.DecimalPlace;
                //numTax.Decimals = Settings.DecimalPlace;
                //numNetAmt.Decimals = Settings.DecimalPlace;

                if (Settings.DecimalPlace == 3)
                {
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
                    colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
                    colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
                    colTotal.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
                    //ScolFreight.DisplayFormat = "{0:0.000}";
                    //ScolTax.DisplayFormat = "{0:0.000}";
                    //ScolTotal.DisplayFormat = "{0:0.000}";
                }
                else
                {
                    colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };
                    colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };
                    colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };
                    colTotal.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };
                    //ScolFreight.DisplayFormat = "{0:0.00}";
                    //ScolTax.DisplayFormat = "{0:0.00}";
                    //ScolTotal.DisplayFormat = "{0:0.00}";
                }
            }
            else
            {
                //numGrossAmt.Decimals = 4;
                //numFreight.Decimals = 4;
                //numTax.Decimals = 4;
                //numNetAmt.Decimals = 4;

                colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = MaskType.Numeric };
                colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = MaskType.Numeric };
                colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = MaskType.Numeric };
                colTotal.EditSettings = new TextEditSettings() { DisplayFormat = "f4", MaskType = MaskType.Numeric };
                //ScolFreight.DisplayFormat = "{0:0.0000}";
                //ScolTax.DisplayFormat = "{0:0.0000}";
                //ScolTotal.DisplayFormat = "{0:0.0000}";
            }
            */


            colCost.EditSettings = new TextEditSettings() { DisplayFormat = "f2", MaskType = MaskType.Numeric };
            colFreight.EditSettings = new TextEditSettings() { DisplayFormat = "f2", MaskType = MaskType.Numeric };
            colTax.EditSettings = new TextEditSettings() { DisplayFormat = "f2", MaskType = MaskType.Numeric };
            colTotal.EditSettings = new TextEditSettings() { DisplayFormat = "f2", MaskType = MaskType.Numeric };
        }

        private bool IsEmptyGrid()
        {
            DataTable dtbl1 = new DataTable();
            dtbl1 = grdDeatil.ItemsSource as DataTable;
            int intcount = 0;
            foreach (DataRow dr in dtbl1.Rows)
            {

                if ((dr["ProductID"].ToString() == "0") && (dr["VendorPartNo"].ToString() == "") && (dr["Description"].ToString() == "")
                    && (dr["Cost"].ToString() == "") && (dr["Qty"].ToString() == "") && (dr["Freight"].ToString() == "") &&
                    (dr["ExtCost"].ToString() == ""))
                {
                    intcount++;
                }

            }

            if (intcount == dtbl1.Rows.Count)
            {
                dtbl1.Dispose();
                return true;
            }
            else
            {
                dtbl1.Dispose();
                return false;
            }
        }

        private void GetAllAmount()
        {
            if (grdDeatil.ItemsSource as DataTable == null) return;
            DataTable TempTbl = new DataTable();
            gridView1.PostEditor();
            grdDeatil.UpdateTotalSummary();
            TempTbl = grdDeatil.ItemsSource as DataTable;
            double dblGross = 0;
            double dblFreight = 0;
            double dblTax = 0;
            double dblNet = 0;
            int intCaseQty = 0;
            foreach (DataRow dr in TempTbl.Rows)
            {
                if ((dr["ProductID"].ToString() != "0") && (dr["Cost"].ToString() != "") && (dr["Qty"].ToString() != "") && (dr["CaseQty"].ToString() != ""))
                {
                    intCaseQty = GeneralFunctions.fnInt32(dr["CaseQty"].ToString());
                    if (dr["Cost"].ToString() != "")
                    {
                        dblGross = dblGross + GeneralFunctions.fnDouble(dr["Cost"].ToString()) * GeneralFunctions.fnDouble(dr["Qty"].ToString());
                        //if (intCaseQty == 0) dblGross = dblGross + GeneralFunctions.fnDouble(dr["Cost"].ToString()) * GeneralFunctions.fnDouble(dr["Qty"].ToString());
                        //if (intCaseQty > 0) dblGross = dblGross + GeneralFunctions.fnDouble(dr["Cost"].ToString()) * intCaseQty * GeneralFunctions.fnDouble(dr["Qty"].ToString());
                    }

                    if (dr["Freight"].ToString() != "")
                    {
                        dblFreight = dblFreight + GeneralFunctions.fnDouble(dr["Freight"].ToString());
                    }

                    if (dr["Tax"].ToString() != "")
                    {
                        dblTax = dblTax + GeneralFunctions.fnDouble(dr["Tax"].ToString());
                    }
                    if (dr["ExtCost"].ToString() != "")
                    {
                        dblNet = dblNet + GeneralFunctions.fnDouble(dr["ExtCost"].ToString());
                    }

                }

            }
            numGrossAmt.Text = dblGross.ToString();
            numFreight.Text = dblFreight.ToString();
            numTax.Text = dblTax.ToString();
            numINVtot.Text = dblNet.ToString();
            TempTbl.Dispose();
        }

        private void PopulatePOBatchGrid()
        {
            bool blPopulate = false;
            try
            {
                int refVendor = 0;
                string strDt = "";
                //grdDeatil.
                PosDataObject.PO objPO1 = new PosDataObject.PO();
                objPO1.Connection = SystemVariables.Conn;
                int intExists = objPO1.IfExistsPONO(txtPOno.Text.Trim(), ref refVendor, ref strDt);
                if (intExists == 1)
                {
                    cmbVendor.EditValue = refVendor.ToString();
                    if (strDt == "")
                    {
                        dtOrder.EditValue = null;
                    }
                    else
                    {
                        dtOrder.EditValue = GeneralFunctions.fnDate(strDt);
                    }
                    PosDataObject.PO objPO3 = new PosDataObject.PO();
                    objPO3.Connection = SystemVariables.Conn;
                    int intRefID = objPO3.FetchIDfromPONO(txtPOno.Text.Trim(), GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()));
                    if (intRefID > 0)
                    {
                        intPOID = intRefID;
                        if (!IsEmptyGrid())
                        {
                            if (DocMessage.MsgGridStatus())
                            {
                                blPopulate = true;
                            }
                            else
                            {
                                blPopulate = false;
                            }
                        }
                        else
                        {
                            blPopulate = true;
                        }

                        if (blPopulate)
                        {
                            PosDataObject.PO objPO2 = new PosDataObject.PO();
                            objPO2.Connection = SystemVariables.Conn;
                            RearrangePOItemLeftForReceiving(objPO2.ShowDetailRecord(intRefID), intRefID);
                            if (intRefID == 0) FillRowinGrid(((grdDeatil.ItemsSource) as DataTable).Rows.Count);
                            GetAllAmount();
                            gridView1.FocusedRowHandle = 0;

                        }
                    }
                    else
                    {
                        intPOID = 0;
                    }
                }
                else
                {
                    intPOID = 0;
                }
            }
            finally
            {
                //grdDeatil.ResumeLayout();
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
                    dtbl.Rows.Add(new object[] { null, null, 0, null, null, null, null, null, null, null, false, null, null, null, null, null, strip });
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

        private void RearrangePOItemLeftForReceiving(DataTable dtbl, int RefPO)
        {
            DataTable dClone = dtbl.Clone();

            DataTable dUnique = dtbl.DefaultView.ToTable(true, new String[] { "ProductID", "VendorPartNo", "Cost" });

            foreach (DataRow dr1 in dUnique.Rows)
            {
                int iLine = 0;
                int iProductID = 0;
                string sVendPart = "";
                double dCost = 0;
                double dQty = 0;
                double dTax = 0;
                double dFreight = 0;

                string sSKU = "";
                string sDesc = "";
                int iCaseQty = 0;
                string sCaseUPC = "";
                string sProdType = "";
                string sScaleBarCode = "";
                string sDecimal = "";
                string sNotes = "";

                double dRecvQty = 0;

                foreach (DataRow dr2 in dtbl.Rows)
                {
                    if ((dr1["ProductID"].ToString() == dr2["ProductID"].ToString()) && (dr1["VendorPartNo"].ToString() == dr2["VendorPartNo"].ToString())
                        && (dr1["Cost"].ToString() == dr2["Cost"].ToString()))
                    {
                        iLine = GeneralFunctions.fnInt32(dr2["ID"].ToString());
                        iProductID = GeneralFunctions.fnInt32(dr2["ProductID"].ToString());
                        sVendPart = dr2["VendorPartNo"].ToString();
                        dCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString());
                        dQty = dQty + GeneralFunctions.fnDouble(dr2["Qty"].ToString());
                        dTax = dTax + GeneralFunctions.fnDouble(dr2["Tax"].ToString());
                        dFreight = dFreight + GeneralFunctions.fnDouble(dr2["Freight"].ToString());

                        sSKU = dr2["SKU"].ToString();
                        sDesc = dr2["Description"].ToString();
                        iCaseQty = GeneralFunctions.fnInt32(dr2["CaseQty"].ToString());
                        sCaseUPC = dr2["CaseUPC"].ToString();
                        sProdType = dr2["ProductType"].ToString();
                        sScaleBarCode = dr2["ScaleBarCode"].ToString();

                        sDecimal = dr2["DecimalPlace"].ToString();
                        sNotes = dr2["Notes"].ToString();
                    }
                }

                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;
                dRecvQty = objPO.GetReceiptQtyForPerticularCombination(RefPO, iProductID, sVendPart, dCost);


                if (dQty - dRecvQty > 0)
                {
                    double UnitTax = 0;
                    double UnitFreight = 0;
                    UnitTax = dTax != 0 ? (dTax / dQty) : 0;
                    UnitFreight = dFreight != 0 ? (dFreight / dQty) : 0;

                    double TCost = ((dQty - dRecvQty) * dCost) + (UnitTax * (dQty - dRecvQty)) + (UnitFreight * (dQty - dRecvQty));
                    dClone.Rows.Add(new object[] { iLine,iProductID, sSKU, sVendPart, sDesc, dQty - dRecvQty, dCost, (UnitFreight * (dQty - dRecvQty)), (UnitTax * (dQty - dRecvQty)), TCost.ToString("n"),
                    sNotes, "", true, sDecimal, iCaseQty, sCaseUPC, sProdType, sScaleBarCode });
                }

            }

            grdDeatil.ItemsSource = dClone;
            gridView1.NewItemRowPosition = NewItemRowPosition.None;
            colSKU.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            colPartNo.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            colCost.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            colDesc.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            colTotal.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            //repProduct.Buttons[0].Visible = false;
            //repProduct.Buttons[1].Visible = false;
            //repProduct.Buttons[2].Visible = false;
            //repProduct.ReadOnly = true;
            //repPartNo.ReadOnly = true;
            //repCost.ReadOnly = true;
            //repDesc.ReadOnly = true;
            //repExtCost.ReadOnly = true;
        }

        private void TxtPOno_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((intID > 0) && (intPOID > 0)) return;
            if (txtPOno.Text.Trim() != "")
            {
                PopulatePOBatchGrid();
            }
        }

        public void PopulateProduct(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchLookupData1(intOption, GeneralFunctions.fnInt32(cmbVendor.EditValue));
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

        public void GetMinOrderAmount()
        {
            if (GeneralFunctions.fnInt32(cmbVendor.EditValue) > 0)
            {
                PosDataObject.Vendor objProduct = new PosDataObject.Vendor();
                objProduct.Connection = SystemVariables.Conn;
                dblMinOrderAmount = objProduct.GetVendorMinimumOrderAmount(GeneralFunctions.fnInt32(cmbVendor.EditValue));
            }
        }

        private void CmbVendor_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            GetMinOrderAmount();
            if (dblMinOrderAmount > 0)
            {
                if (Settings.DecimalPlace == 3)
                {
                    lbminval.Text = "Min. Order Amount : " + dblMinOrderAmount.ToString("f3");
                }
                else
                {
                    lbminval.Text = "Min. Order Amount : " + dblMinOrderAmount.ToString("f");
                }
            }
            else
            {
                lbminval.Text = "";
            }

            PopulateProduct(0);
            boolControlChanged = true;
        }

        public void ShowRecvHeaderData()
        {
            PosDataObject.Recv objRecv = new PosDataObject.Recv();
            objRecv.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objRecv.ShowHeaderRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtBatch.Text = dr["BatchID"].ToString();
                cmbVendor.EditValue = dr["VendorID"].ToString();
                txtCheckClerk.EditValue = dr["CheckInClerk"].ToString();
                txtRecvClerk.EditValue = dr["ReceivingClerk"].ToString();
                txtPOno.Text = dr["PurchaseOrder"].ToString();
                intPOID = GeneralFunctions.fnInt32(dr["PurchaseOrderID"].ToString());
                txtINVno.Text = dr["InvoiceNo"].ToString();
                numINVtot.Text = GeneralFunctions.fnDouble(dr["InvoiceTotal"].ToString()).ToString("f2");
                numFreight.Text = GeneralFunctions.fnDouble(dr["Freight"].ToString()).ToString("f2");
                numGrossAmt.Text = GeneralFunctions.fnDouble(dr["GrossAmount"].ToString()).ToString("f2");
                numTax.Text = GeneralFunctions.fnDouble(dr["Tax"].ToString()).ToString("f2");
                txtNotes.Text = dr["Note"].ToString();

                dtReceive.EditValue = GeneralFunctions.fnDate(dr["ReceiveDate"].ToString());

                if (dr["DateOrdered"].ToString() == "")
                {
                    dtOrder.EditValue = null;
                }
                else
                {
                    dtOrder.EditValue = GeneralFunctions.fnDate(dr["DateOrdered"].ToString());
                }
            }
            dbtbl.Dispose();
        }

        private void PopulateBatchGrid()
        {
            try
            {
                //grdDeatil.SuspendLayout();
                PosDataObject.Recv objRecv = new PosDataObject.Recv();
                objRecv.Connection = SystemVariables.Conn;
                DataTable dtbl = objRecv.ShowDetailRecord(intID);

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


                grdDeatil.ItemsSource = dtblTemp;
                if (intPOID > 0)
                {
                    gridView1.NewItemRowPosition = NewItemRowPosition.None;
                    colSKU.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    colPartNo.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    colCost.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    colDesc.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    colTotal.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
                    txtPOno.IsReadOnly = true;
                    cmbVendor.IsReadOnly = true;
                }
                if (intPOID == 0) FillRowinGrid(((grdDeatil.ItemsSource) as DataTable).Rows.Count);
                //gridView1.MoveFirstRow();
            }
            finally
            {
                //grdDeatil.ResumeLayout();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            PopulateVendor();
            PopulateEmployee();
            SetDecimalPlace();
            dtReceive.EditValue = DateTime.Today.Date;
            if (Settings.VendorUpdate == 0) rbvupdtYes.IsChecked = true;
            if (Settings.VendorUpdate == 1) rbvupdtNo.IsChecked = true;
            if (Settings.VendorUpdate == 2) rbvupdtPrompt.IsChecked = true;
            if (intID == 0)
            {
                txtBatch.Text = "0";
                Title.Text = Properties.Resources.Add_Receiving_Items;
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Receiving_Items;
                ShowRecvHeaderData();
                txtINVno.IsEnabled = false;
            }
            PopulateBatchGrid();
            boolControlChanged = false;
            boolLoading = true;
        }

        private async void LnkPO_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (intID > 0) return;
            blurGrid.Visibility = Visibility.Visible;
            Report.frm_OrderLookUpDlg frm_OrderLookUpDlg = new Report.frm_OrderLookUpDlg();
            try
            {
                frm_OrderLookUpDlg.SearchType = "Purchase Order";
                frm_OrderLookUpDlg.Print = "N";
                frm_OrderLookUpDlg.ShowDialog();
                if (frm_OrderLookUpDlg.boolSelected == true)
                {
                    txtPOno.Text = await frm_OrderLookUpDlg.GetPO();
                    if (txtPOno.Text.Trim() != "")
                    {
                        PopulatePOBatchGrid();
                        GeneralFunctions.SetFocus(txtINVno);
                        blPOLeave = false;
                    }
                }

            }
            finally
            {
                blPOLeave = true;
                frm_OrderLookUpDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void GridView1_ShowingEditor(object sender, ShowingEditorEventArgs e)
        {
            if (!boolLoading) return;
            if (e.Column == colSKU)
            {
                if (cmbVendor.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources.Select_Vendor);
                    GeneralFunctions.SetFocus(cmbVendor);
                    e.Cancel = true;
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
                            string strLabel = "N";
                            string strPartNo = "";
                            string DP = "2";
                            int intcaseqty = 0;
                            string strcaseupc = "";
                            string strscalebarcode = "N";
                            string strptype = "P";
                            if (cmbVendor.EditValue == null)
                                return;
                            GeneralFunctions.GetSKUDetails(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colSKU)), GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()),
                                ref strSKU, ref strDesciption, ref dblCost, ref strLabel, ref strPartNo, ref DP, ref intcaseqty, ref strcaseupc, ref strptype, ref strscalebarcode);
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
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight) == "") grdDeatil.SetCellValue(intRowNum, colFreight, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax) == "") grdDeatil.SetCellValue(intRowNum, colTax, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCaseQty) == "") grdDeatil.SetCellValue(intRowNum, colCaseQty, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) == "") grdDeatil.SetCellValue(intRowNum, colCost, dblCost);

                            grdDeatil.SetCellValue(intRowNum, colSKU1, strSKU);
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colDesc) == "") grdDeatil.SetCellValue(intRowNum, colDesc, strDesciption);
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colPartNo) == "") grdDeatil.SetCellValue(intRowNum, colPartNo, strPartNo);
                            grdDeatil.SetCellValue(intRowNum, colCaseQty, intcaseqty);
                            grdDeatil.SetCellValue(intRowNum, colCaseUPC, strcaseupc);

                            grdDeatil.SetCellValue(intRowNum, colProductType, strptype);
                            grdDeatil.SetCellValue(intRowNum, colScaleBarcode, strscalebarcode);

                            double dblQty = 0.00;
                            double dblAmount = 0.00;
                            double dblRate = 0.0;
                            double dblFreight = 0.0;
                            double dblTax = 0.0;

                            double dblCsQty = 0.00;


                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) != "")
                                dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost));
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight) != "")
                                dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight));
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax) != "")
                                dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax));

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) != "")
                                dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty));

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCaseQty) != "")
                                dblCsQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCaseQty));

                            try
                            {
                                dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                                //if (dblCsQty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                                //if (dblCsQty > 0) dblAmount = (dblQty * dblCsQty * dblRate) + (dblFreight + dblTax);
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

                if (grdDeatil.CurrentColumn.Name == "colPrintLabel")
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
            if (e.Column.Name == "colSKU")
            {

            }

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
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax));

                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCaseQty) != "")
                        intcaseqty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCaseQty));

                    dblQty = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        if (Settings.TaxInclusive == "Y")
                        {
                            double calculatedTaxAmount = TaxPercentage / 100 * (dblQty * dblRate);
                            dblAmount = (dblQty * dblRate) + (dblFreight - calculatedTaxAmount);
                            dblTax = calculatedTaxAmount;
                        }
                        else
                            dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
                    }
                    catch
                    {
                        dblAmount = 0.00;
                    }
                    dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    grdDeatil.SetCellValue(intRowNum, colTotal, dblAmount);

                    if (Settings.TaxInclusive == "Y")
                    {
                        dblTax = GeneralFunctions.fnDouble(dblTax.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                        grdDeatil.SetCellValue(intRowNum, colTax, dblTax);
                    }
                    GetAllAmount();
                }
            }

            if (e.Column.Name == "colCost")
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



                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax));



                    dblRate = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        if (Settings.TaxInclusive == "Y")
                        {
                            double calculatedTaxAmount = TaxPercentage / 100 * (dblQty * dblRate);
                            dblAmount = (dblQty * dblRate) + (dblFreight - calculatedTaxAmount);
                            dblTax = calculatedTaxAmount;
                        }
                        else
                            dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
                    }
                    catch
                    {
                        dblAmount = 0.00;
                    }
                    dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    grdDeatil.SetCellValue(intRowNum, colTotal, dblAmount);

                    if (Settings.TaxInclusive == "Y")
                    {
                        dblTax = GeneralFunctions.fnDouble(dblTax.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                        grdDeatil.SetCellValue(intRowNum, colTax, dblTax);
                    }
                    GetAllAmount();
                }
            }

            if (e.Column.Name == "colFreight")
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



                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colTax));



                    dblFreight = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
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

            if (e.Column.Name == "colTax")
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



                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colCost));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDeatil, colFreight));



                    dblTax = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
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

        private void CmbVendor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void TxtRecvClerk_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private bool IsValidFooter()
        {
            DataTable dtbl1 = new DataTable();
            dtbl1 = grdDeatil.ItemsSource as DataTable;

            int intcount = 0;
            foreach (DataRow dr in dtbl1.Rows)
            {

                if ((dr["ProductID"].ToString() == "0") && (dr["VendorPartNo"].ToString() == "") && (dr["Description"].ToString() == "")
                    && (dr["Cost"].ToString() == "") && (dr["Qty"].ToString() == "") && (dr["Freight"].ToString() == "") &&
                    (dr["ExtCost"].ToString() == ""))
                {
                    intcount++;
                }

            }
            if (intcount == dtbl1.Rows.Count)
            {
                DocMessage.MsgEnter("Item");
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
                    if (dr["Qty"].ToString() == "")
                    {
                        DocMessage.MsgEnter("Quantity");
                        gridView1.FocusedRowHandle = introwhandle;
                        grdDeatil.CurrentColumn = colQty;
                        return false;
                    }

                    if (intPOID > 0)
                    {
                        double TotalPOItem = 0;
                        double TotalReceivedItem = 0;

                        PosDataObject.PO objPO = new PosDataObject.PO();
                        objPO.Connection = SystemVariables.Conn;

                        TotalPOItem = objPO.GetPOQtyForPerticularCombination(intPOID, GeneralFunctions.fnInt32(dr["ProductID"].ToString()),
                            dr["VendorPartNo"].ToString(), GeneralFunctions.fnDouble(dr["Cost"].ToString()));

                        TotalReceivedItem = objPO.GetReceiptQtyForPerticularCombination(intPOID, GeneralFunctions.fnInt32(dr["ProductID"].ToString()),
                        dr["VendorPartNo"].ToString(), GeneralFunctions.fnDouble(dr["Cost"].ToString()), GeneralFunctions.fnInt32(dr["ID"].ToString()));

                        if (GeneralFunctions.fnDouble(dr["Qty"].ToString()) > (TotalPOItem - TotalReceivedItem))
                        {
                            DocMessage.MsgInvalid(Properties.Resources.Quantity);
                            gridView1.Focus();
                            gridView1.FocusedRowHandle = introwhandle;
                            grdDeatil.CurrentColumn = colQty;
                            return false;
                        }


                    }
                }

            }
            dtbl.Dispose();
            return true;
        }

        private bool IsValidHeader()
        {
            if (cmbVendor.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Vendor);
                GeneralFunctions.SetFocus(cmbVendor);
                return false;
            }

            

            if (dtReceive.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Receive_Date);
                GeneralFunctions.SetFocus(dtReceive);
                return false;
            }

            if (dtReceive.Text.Trim() != "")
            {
                if (dtReceive.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Receive_Date_can_not_be_after_today);
                    GeneralFunctions.SetFocus(dtReceive);
                    return false;
                }
            }

            if ((dtOrder.Text.Trim() != "") && (dtReceive.Text.Trim() != ""))
            {
                if (dtOrder.DateTime.Date > dtReceive.DateTime.Date)
                {
                    DocMessage.MsgInformation(Properties.Resources.Receive_Date_can_not_be_before_Order_Date);
                    GeneralFunctions.SetFocus(dtOrder);
                    return false;
                }
            }

            /*if (GeneralFunctions.fnDouble(numINVtot.Text) < dblMinOrderAmount)
            {
                DocMessage.MsgInformation("Order amount must be equal or exceeds to\nminimum order amount set for this vendor.");
                GeneralFunctions.SetFocus(grdDeatil);
                gridView1.FocusedRowHandle = 0;
                grdDeatil.CurrentColumn = colSKU;
                return false;
            }*/

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

        private bool SaveData()
        {
            if (ValidAllFields())
            {
                string ErrMsg = "";
                bool blUpdate = false;

                gridView1.PostEditor();
                GetAllAmount();
                PosDataObject.Recv objRecv = new PosDataObject.Recv();
                objRecv.Connection = SystemVariables.Conn;
                objRecv.ConnString = SystemVariables.ConnectionString;

                objRecv.BatchID = GeneralFunctions.fnInt32(txtBatch.Text.Trim());
                objRecv.VendorID = GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString());
                objRecv.PurchaseOrder = txtPOno.Text.Trim();
                objRecv.PurchaseOrderID = intPOID;
                objRecv.InvoiceNo = txtINVno.Text.Trim();
                objRecv.InvoiceTotal = GeneralFunctions.fnDouble(numINVtot.Text);
                objRecv.Freight = GeneralFunctions.fnDouble(numFreight.Text);
                objRecv.GrossAmount = GeneralFunctions.fnDouble(numGrossAmt.Text);
                objRecv.Tax = GeneralFunctions.fnDouble(numTax.Text);
                if (txtCheckClerk.Text.Trim() != "") objRecv.CheckInClerk = GeneralFunctions.fnInt32(txtCheckClerk.EditValue.ToString());
                else objRecv.CheckInClerk = 0;
                if (txtRecvClerk.Text.Trim() != "") objRecv.ReceivingClerk = GeneralFunctions.fnInt32(txtRecvClerk.EditValue.ToString());
                else objRecv.ReceivingClerk = 0;
                objRecv.Note = txtNotes.Text.Trim();
                objRecv.EmployeeID = SystemVariables.CurrentUserID;
                objRecv.RegisterID = 1;
                objRecv.StoreID = 1;

                if (dtOrder.EditValue == null)
                {
                    objRecv.DateOrdered = Convert.ToDateTime(null);
                }
                else
                {
                    objRecv.DateOrdered = dtOrder.DateTime.Date;
                }

                objRecv.ReceiveDate = dtReceive.DateTime.Date;

                DataTable dtblRearrangeDetail = grdDeatil.ItemsSource as DataTable;
                foreach (DataRow dr in dtblRearrangeDetail.Rows)
                {
                    if (dr["ProductID"].ToString() == "0")
                    {
                        dr["ProductID"] = "";
                    }
                }

                objRecv.SplitDataTable = dtblRearrangeDetail;
                objRecv.LoginUserID = SystemVariables.CurrentUserID;
                objRecv.TerminalName = Settings.TerminalName;
                objRecv.ErrorMsg = "";
                objRecv.ID = intID;


                if (intID == 0)
                {
                    objRecv.Mode = "Add";
                }
                else
                {
                    objRecv.Mode = "Edit";
                }

                //B e g i n   T r a n s a ct i o n
                objRecv.BeginTransaction();
                if (objRecv.InsertRecv())
                {
                    int intNewBatchID = objRecv.ID;
                }
                objRecv.EndTransaction();
                //E n d  T r a n s a ct i o n

                ErrMsg = objRecv.ErrorMsg;
                if (ErrMsg != "")
                {
                    DocMessage.ShowException("Saving Receiving Data", ErrMsg);
                    return false;
                }
                else
                {
                    if (rbvupdtYes.IsChecked == true)
                    {
                        blUpdate = true;
                    }
                    if (rbvupdtNo.IsChecked == true)
                    {
                        blUpdate = false;
                    }
                    if (rbvupdtPrompt.IsChecked == true)
                    {
                        blUpdate = true;
                    }
                    if (blUpdate)
                    {
                        DataTable dtblVU = grdDeatil.ItemsSource as DataTable;
                        try
                        {
                            string msgErr = "";
                            string rv = "";
                            bool blExecUpdate = false;

                            PosDataObject.Recv objRecv1 = new PosDataObject.Recv();
                            SqlConnection sConn = new SqlConnection(SystemVariables.ConnectionString);

                            foreach (DataRow dr in dtblVU.Rows)
                            {
                                if (dr["ProductID"].ToString() == "0") continue;

                                int strgetPrimaryVendor = objRecv1.FetchPrimaryVendor(sConn, GeneralFunctions.fnInt32(dr["ProductID"].ToString()), GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()));

                                if (rbvupdtYes.IsChecked == true)
                                {
                                    blExecUpdate = true;
                                }
                                if ((rbvupdtPrompt.IsChecked == true)
                                    && ((rv == "") || (rv == "Y") || (rv == "N")))
                                {
                                    blurGrid.Visibility = Visibility.Visible;
                                    frm_VendorUpdateDlg frm_VendorUpdateDlg = new frm_VendorUpdateDlg();
                                    try
                                    {
                                        frm_VendorUpdateDlg.PrevVendorID = strgetPrimaryVendor;
                                        frm_VendorUpdateDlg.CurrVendorID = GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString());
                                        frm_VendorUpdateDlg.ShowDialog();
                                    }

                                    finally
                                    {
                                        rv = frm_VendorUpdateDlg.ReturnValue;
                                        frm_VendorUpdateDlg.Close();
                                        blurGrid.Visibility = Visibility.Collapsed;
                                    }
                                    if (rv == "Y")
                                    {
                                        blExecUpdate = true;
                                    }
                                    else if (rv == "N")
                                    {
                                        blExecUpdate = false;
                                    }
                                    else if (rv == "YA")
                                    {
                                        blExecUpdate = true;
                                    }
                                    else
                                    {
                                        blExecUpdate = false;
                                    }

                                }
                                if (blExecUpdate)
                                    msgErr = objRecv1.UpdateVendor(sConn, GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), GeneralFunctions.fnInt32(dr["ProductID"].ToString()), dr["VendorPartNo"].ToString(), GeneralFunctions.fnDouble(dr["Cost"].ToString()));

                                dtblVU.Dispose();
                            }
                        }
                        catch
                        {

                        }

                    }

                    return true;
                }
            }
            else
                return false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                BrowseForm.Flag = false;
                //BrowseForm.lkupVendor.EditValue = cmbVendor.EditValue.ToString();
                //BrowseForm.lkupVendor.EditValue = "0";
                //BrowseForm.FDate.EditValue = dtReceive.DateTime;
                //BrowseForm.TDate.EditValue = dtReceive.DateTime;
                BrowseForm.Flag = true;
                //BrowseForm.FetchData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), dtReceive.DateTime, dtReceive.DateTime);
                frmBrowse.FetchData(GeneralFunctions.fnInt32(frmBrowse.lkupVendor.EditValue), GeneralFunctions.fnDate(frmBrowse.FDate.EditValue), GeneralFunctions.fnDate(frmBrowse.TDate.EditValue));
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
                        //BrowseForm.lkupVendor.EditValue = cmbVendor.EditValue.ToString();
                        //BrowseForm.lkupVendor.EditValue = "0";
                        //BrowseForm.FDate.EditValue = dtReceive.DateTime;
                        //BrowseForm.TDate.EditValue = dtReceive.DateTime;
                        BrowseForm.Flag = true;
                        //BrowseForm.FetchData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), dtReceive.DateTime, dtReceive.DateTime);
                        frmBrowse.FetchData(GeneralFunctions.fnInt32(frmBrowse.lkupVendor.EditValue), GeneralFunctions.fnDate(frmBrowse.FDate.EditValue), GeneralFunctions.fnDate(frmBrowse.TDate.EditValue));
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtBatch_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void RbvupdtYes_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void RbvupdtYes_Unchecked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtPOno_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GeneralFunctions.SetFocus(cmbVendor);
            }
        }

        private async void LnkVendor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (intID > 0) return;
            blurGrid.Visibility = Visibility.Visible;
            Report.frm_Lookups frm_Lookups = new Report.frm_Lookups();
            try
            {
                frm_Lookups.Print = "N";
                frm_Lookups.SearchType = "Vendor";
                frm_Lookups.ShowDialog();
                if (await frm_Lookups.GetID() > 0)
                {
                    cmbVendor.EditValue = (await frm_Lookups.GetID()).ToString();
                }
            }
            finally
            {
                frm_Lookups.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void DtOrder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private int GetDecimalPlace()
        {
            /* if (Settings.Use4Decimal == "N")
             {

                 if (Settings.DecimalPlace == 3)
                 {
                     return 3;
                 }
                 else
                 {
                     return 2;
                 }
             }
             else
             {
                 return 4;
             }*/

            return 2;
        }

        private void GridView1_ShownEditor(object sender, EditorEventArgs e)
        {
            if (e.Column.FieldName == "VendorPartNo")
            {
                TextEdit editor = (TextEdit)e.Editor;
                editor.Focusable = true;

                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (!IsAboutFullKybrdOpen)
                {

                    fkybrd = new FullKeyboard();

                    fkybrd.CallFromGrid = true;
                    fkybrd.GridInputControl = editor;
                    fkybrd.WindowName = "Recv";
                    fkybrd.GridColumnName = "VendorPartNo";
                    fkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0));
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

                    e.Handled = true;
                }
            }

            if (e.Column.FieldName == "Description")
            {
                TextEdit editor = (TextEdit)e.Editor;
                editor.Focusable = true;

                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (!IsAboutFullKybrdOpen)
                {

                    fkybrd = new FullKeyboard();

                    fkybrd.CallFromGrid = true;
                    fkybrd.GridInputControl = editor;
                    fkybrd.WindowName = "Recv";
                    fkybrd.GridColumnName = "Description" +
                        "";
                    fkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0));
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

                    e.Handled = true;
                }
            }

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
                    nkybrd.WindowName = "Recv";
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


            


            if (e.Column.FieldName == "Cost")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                int dp = GetDecimalPlace();
                if (editor.Text == "")
                {
                    if (dp == 2) editor.Text = "0.00";
                    if (dp == 3) editor.Text = "0.000";
                    if (dp == 4) editor.Text = "0.0000";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Recv";
                    nkybrd.GridColumnName = "Cost";
                    nkybrd.GridDecimal = dp;
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

            if (e.Column.FieldName == "Freight")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                int dp = GetDecimalPlace();
                if (editor.Text == "")
                {
                    if (dp == 2) editor.Text = "0.00";
                    if (dp == 3) editor.Text = "0.000";
                    if (dp == 4) editor.Text = "0.0000";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Recv";
                    nkybrd.GridColumnName = "Freight";
                    nkybrd.GridDecimal = dp;
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

            if (e.Column.FieldName == "Tax")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                int dp = GetDecimalPlace();
                if (editor.Text == "")
                {
                    if (dp == 2) editor.Text = "0.00";
                    if (dp == 3) editor.Text = "0.000";
                    if (dp == 4) editor.Text = "0.0000";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Recv";
                    nkybrd.GridColumnName = "Tax";
                    nkybrd.GridDecimal = dp;
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
            if (gridcol == "VendorPartNo") grdDeatil.SetCellValue(rindx, colPartNo, val);
            if (gridcol == "Description") grdDeatil.SetCellValue(rindx, colDesc, val);
            if (gridcol == "Qty") grdDeatil.SetCellValue(rindx, colQty, val);
            if (gridcol == "Cost") grdDeatil.SetCellValue(rindx, colCost, val);
            if (gridcol == "Freight") grdDeatil.SetCellValue(rindx, colFreight, val);
            if (gridcol == "Tax") grdDeatil.SetCellValue(rindx, colTax, val);

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
