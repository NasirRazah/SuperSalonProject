using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using OfflineRetailV2.Data;
using System;
using System.Collections;
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
using DevExpress.Xpf.Grid;
namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_PODlg.xaml
    /// </summary>
    public partial class frm_PODlg : Window
    {
        double TaxPercentage = 20.0;
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;

        private int intTimes = 1;
        private int intPrevRow;
        private double dblMinOrderAmount = 0;
        private bool blloading = false;
        private frm_POBrwUC frmBrowse;
        public frm_POBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_POBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_PODlg()
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

        public void ShowPOHeaderData()
        {
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objPO.ShowHeaderRecord(intID);

            foreach (DataRow dr in dbtbl.Rows)
            {
                cmbVendor.EditValue = dr["VendorID"].ToString();
                txtPO.Text = dr["OrderNo"].ToString();
                txtRefNo.Text = dr["RefNo"].ToString();
                numGrossAmt.Text = GeneralFunctions.fnDouble(dr["GrossAmount"].ToString()).ToString();
                numFreight.Text = GeneralFunctions.fnDouble(dr["Freight"].ToString()).ToString();
                numTax.Text = GeneralFunctions.fnDouble(dr["Tax"].ToString()).ToString();
                numNetAmt.Text = GeneralFunctions.fnDouble(dr["NetAmount"].ToString()).ToString();
                txtNotes.Text = dr["GeneralNotes"].ToString();
                txtInstructions.Text = dr["SupplierInstructions"].ToString();
                txtCheckClerk.EditValue = dr["CheckInClerk"].ToString();
                dtOrderDate.EditValue = GeneralFunctions.fnDate(dr["OrderDate"].ToString());
                if (dr["ExpectedDeliveryDate"].ToString() == "")
                {
                    dtDelivery.EditValue = null;
                }
                else
                {
                    dtDelivery.EditValue = GeneralFunctions.fnDate(dr["ExpectedDeliveryDate"].ToString());
                }
                cmbPriority.SelectedIndex = dr["Priority"].ToString() == "Normal" ? 0 : 1;
            }
            dbtbl.Dispose();
        }
        private bool IsValidHeader()
        {
            if (txtPO.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Order_No_);
                GeneralFunctions.SetFocus(txtPO);
                return false;
            }

            if (intID == 0)
            {
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;
                int val = objPO.DuplicateCount(txtPO.Text.Trim());
                if (val > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Order_No__Please_Check_);
                    GeneralFunctions.SetFocus(txtPO);
                    return false;
                }
            }

            if (dtOrderDate.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Order_Date);
                GeneralFunctions.SetFocus(dtOrderDate);
                return false;
            }

            if (cmbVendor.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Vendor);
                GeneralFunctions.SetFocus(cmbVendor);
                return false;
            }

            if (dtOrderDate.Text.Trim() != "")
            {
                if (dtOrderDate.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Order_Date_can_not_be_after_today);
                    GeneralFunctions.SetFocus(dtOrderDate);
                    return false;
                }
            }

            if ((dtOrderDate.Text.Trim() != "") && (dtDelivery.Text.Trim() != ""))
            {
                if (dtOrderDate.DateTime.Date > dtDelivery.DateTime.Date)
                {
                    DocMessage.MsgInformation( Properties.Resources.Expected_Delivery_Date_can_not_be_before_Order_Date);
                    GeneralFunctions.SetFocus(dtOrderDate);
                    return false;
                }
            }

            /*if (GeneralFunctions.fnDouble(numNetAmt.Text) < dblMinOrderAmount)
            {
                DocMessage.MsgInformation("Order amount must be equal or exceeds to\nminimum order amount set for this vendor.");
                GeneralFunctions.SetFocus(grdDept);
                gridView1.FocusedRowHandle = 0;
                grdDept.CurrentColumn = colSKU;
                return false;
            }*/

            return true;
        }

        private bool IsValidFooter()
        {
            DataTable dtbl1 = new DataTable();
            dtbl1 = grdDept.ItemsSource as DataTable;

            int intcount = 0;
            foreach (DataRow dr in dtbl1.Rows)
            {
                if ((dr["ProductID"].ToString() == "0") && (dr["VendorPartNo"].ToString() == "") && (dr["Description"].ToString() == "")
                    && (dr["Cost"].ToString() == "") && (dr["Qty"].ToString() == "") && (dr["Freight"].ToString() == "")
                    && (dr["Tax"].ToString() == "") && (dr["ExtCost"].ToString() == ""))
                {
                    intcount++;
                }
            }
            if (intcount == dtbl1.Rows.Count)
            {
                DocMessage.MsgEnter(Properties.Resources.Item);
                GeneralFunctions.SetFocus(grdDept);
                gridView1.FocusedRowHandle = 0;
                grdDept.CurrentColumn = colSKU;
                return false;
            }
            dtbl1.Dispose();

            DataTable dtble = new DataTable();
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = new System.Data.SqlClient.SqlConnection(SystemVariables.ConnectionString);
            dtble = objp.FetchLookupData1(0, GeneralFunctions.fnInt32(cmbVendor.EditValue));
            int rwindx = -1;
            int invalidindx = -1;
            bool flg = true;
            DataTable dtbl2 = new DataTable();
            dtbl2 = grdDept.ItemsSource as DataTable;
            foreach (DataRow dr in dtbl2.Rows)
            {
                rwindx++;
                if (dr["ProductID"].ToString() == "0") continue;
                flg = false;
                if (dr["ProductID"].ToString() != "0")
                {
                    foreach (DataRow drt in dtble.Rows)
                    {
                        if (dr["ProductID"].ToString() == drt["ID"].ToString())
                        {
                            flg = true;
                            break;
                        }
                    }
                    if (!flg)
                    {
                        invalidindx = rwindx;
                    }
                }
            }
            if (!flg)
            {
                DocMessage.MsgInvalid(Properties.Resources.Item);
                GeneralFunctions.SetFocus(grdDept);
                gridView1.FocusedRowHandle = invalidindx;
                grdDept.CurrentColumn = colSKU;
                return false;
            }

            DataTable dtbl = new DataTable();
            dtbl = grdDept.ItemsSource as DataTable;

            int introwhandle = -1;
            foreach (DataRow dr in dtbl.Rows)
            {
                introwhandle++;
                if (dr["ProductID"].ToString() == "0") continue;
                if (dr["ProductID"].ToString() != "0")
                {
                    if (dr["Qty"].ToString() == "")
                    {
                        DocMessage.MsgEnter(Properties.Resources.Quantity);
                        gridView1.FocusedRowHandle = introwhandle;
                        grdDept.CurrentColumn = colQty;
                        return false;
                    }
                }
            }
            dtbl.Dispose();

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
        private void GetAllAmount()
        {
            if (grdDept.ItemsSource == null) return;
            DataTable TempTbl = new DataTable();
            gridView1.PostEditor();
            grdDept.UpdateTotalSummary();
            TempTbl = grdDept.ItemsSource as DataTable;
            double dblGross = 0;
            double dblFreight = 0;
            double dblTax = 0;
            double dblNet = 0;
            int intCaseQty = 0;
            foreach (DataRow dr in TempTbl.Rows)
            {
                if ((dr["ProductID"].ToString() != "0") && (dr["Cost"].ToString() != "") && (dr["Qty"].ToString() != "") /*&& (dr["CaseQty"].ToString() != "")*/)
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
            numNetAmt.Text = dblNet.ToString();
            TempTbl.Dispose();
        }

        private bool SaveData()
        {
            if (ValidAllFields())
            {
                string ErrMsg = "";
                gridView1.PostEditor();
                GetAllAmount();
                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;

                objPO.VendorID = GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString());
                objPO.OrderNo = txtPO.Text.Trim();
                objPO.RefNo = txtRefNo.Text.Trim();
                objPO.GrossAmount = GeneralFunctions.fnDouble(numGrossAmt.Text);
                objPO.Freight = GeneralFunctions.fnDouble(numFreight.Text);
                objPO.Tax = GeneralFunctions.fnDouble(numTax.Text);
                objPO.NetAmount = GeneralFunctions.fnDouble(numNetAmt.Text);
                objPO.MinOrderAmount = dblMinOrderAmount;
                objPO.SupplierInstructions = txtInstructions.Text.Trim();
                objPO.GeneralNotes = txtNotes.Text.Trim();
                objPO.Priority = cmbPriority.Text.Trim();
                if (txtCheckClerk.Text.Trim() != "") objPO.CheckInClerk = GeneralFunctions.fnInt32(txtCheckClerk.EditValue.ToString());
                else objPO.CheckInClerk = 0;
                objPO.AutoPO = Settings.AutoPO;
                objPO.OrderDate = dtOrderDate.DateTime.Date;
                if (dtDelivery.EditValue == null)
                {
                    objPO.ExpectedDeliveryDate = Convert.ToDateTime(null);
                }
                else
                {
                    objPO.ExpectedDeliveryDate = dtDelivery.DateTime.Date;
                }

                DataTable dtblRearrangeDetail = grdDept.ItemsSource as DataTable;
                foreach(DataRow dr in dtblRearrangeDetail.Rows)
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
                if (objPO.InsertPO())
                {
                    intNewID = objPO.ID;
                }
                objPO.EndTransaction();
                //E n d  T r a n s a ct i o n

                ErrMsg = objPO.ErrorMsg;
                if (ErrMsg != "")
                {
                    DocMessage.ShowException("Saving Purchase Order Data", ErrMsg);
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
            PART_Editor_SKU.AutoPopulateColumns = false;
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

        }

        private void PopulateBatchGrid()
        {
            
            
            try
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add("ProductID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("VendorPartNo", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Freight", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Tax", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("ExtCost", System.Type.GetType("System.Double"));
                dtbl.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("DecimalPlace", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("CaseQty", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("CaseUPC", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtbl.Columns.Add("ScaleBarCode", System.Type.GetType("System.String"));

                PosDataObject.PO objPO = new PosDataObject.PO();
                objPO.Connection = SystemVariables.Conn;
                DataTable dtbl1 = new DataTable();
                dtbl1 = objPO.ShowDetailRecord(intID);

                foreach(DataRow dr in dtbl1.Rows)
                {
                    dtbl.Rows.Add(new object[] { dr["ProductID"].ToString(), GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                    dr["VendorPartNo"].ToString(),dr["Description"].ToString(),
                    GeneralFunctions.fnDouble(dr["Cost"].ToString()),GeneralFunctions.fnDouble(dr["Freight"].ToString()),
                    GeneralFunctions.fnDouble(dr["Tax"].ToString()),GeneralFunctions.fnDouble(dr["ExtCost"].ToString()),
                    dr["Notes"].ToString(),dr["DecimalPlace"].ToString(),dr["SKU"].ToString(),
                    GeneralFunctions.fnInt32(dr["CaseQty"].ToString()),dr["CaseUPC"].ToString(),
                    dr["ProductType"].ToString(),dr["ScaleBarCode"].ToString()
                    });
                }

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

                grdDept.ItemsSource = dtblTemp;
                FillRowinGrid(((grdDept.ItemsSource) as DataTable).Rows.Count);
                //gridView1.MoveFirstRow();
            }
            finally
            {
            }
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
                    dtbl.Rows.Add(new object[] { 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, strip });
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

        private bool ProcessTabKeyNew(bool forward)
        {
            DevExpress.Xpf.Grid.GridColumn CurrentCol=null;
            try
            {
                //if (forward == true)
                //    CurrentCol = grdDept.GetVisibleColumn(gridView1.FocusedColumn.VisibleIndex + 1);
                //else
                //    CurrentCol = gridView1.GetVisibleColumn(gridView1.FocusedColumn.VisibleIndex - 1);
            }
            catch
            {
                //CurrentCol = gridView1.GetVisibleColumn(0);
            }

            if (CurrentCol == null)
            {
                if (forward == true)
                {
                    //if (gridView1.IsLastRow == true)
                    //{
                    //    //this.SelectNextControl(this, true, true, false, true);
                    //    return false;
                    //}
                    //else
                    //{
                    //    gridView1.FocusedRowHandle += 1;
                    //    //grdDept.FocusedView.ShowEditor();
                    //    gridView1.ActiveEditor.Focus();
                    //    gridView1.ActiveEditor.SelectAll();
                    //}
                    //CurrentCol = gridView1.GetVisibleColumn(0);
                }
                else
                {
                    //if (gridView1.IsFirstRow == true)
                    //{
                    //    //this.SelectNextControl(this, false, true, false, true);
                    //    return false;
                    //}
                    //else
                    //{
                    //    gridView1.FocusedRowHandle -= 1;
                    //}
                    CurrentCol = grdDept.Columns[grdDept.Columns.Count - 1];
                }
            }
            gridView1.FocusedColumn = CurrentCol;
            return true;
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

        private async void barButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_NotesDlg frm_NotesDlg = new frm_NotesDlg();
            try
            {
                frm_NotesDlg.Title.Text = Properties.Resources.Notes;
                frm_NotesDlg.Notes = (await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDept, colNotes)).ToString();
                frm_NotesDlg.ShowDialog();
                if (frm_NotesDlg.OK)
                {
                    boolControlChanged = true;
                    grdDept.SetCellValue(gridView1.FocusedRowHandle, colNotes, frm_NotesDlg.Notes);
                }

            }
            finally
            {
                frm_NotesDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void barButtonItem2_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDept.ItemsSource as DataTable).Rows.Count <= 1) return;
            int intRowNum = 0;
            intRowNum = gridView1.FocusedRowHandle;
            if (intRowNum < 0) return;

            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colSKU) != "")
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
            GetAllAmount();
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                frmBrowse.Flag = false;
                //frmBrowse.lkupVendor.EditValue = cmbVendor.EditValue.ToString();
                //frmBrowse.lkupVendor.EditValue = "0";
                //frmBrowse.FDate.EditValue = dtOrderDate.DateTime;
                //frmBrowse.TDate.EditValue = dtOrderDate.DateTime;
                frmBrowse.Flag = true;
                //frmBrowse.FetchData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), dtOrderDate.DateTime, dtOrderDate.DateTime);
                frmBrowse.FetchData(GeneralFunctions.fnInt32(frmBrowse.lkupVendor.EditValue), GeneralFunctions.fnDate(frmBrowse.FDate.EditValue), GeneralFunctions.fnDate(frmBrowse.TDate.EditValue));
                CloseKeyboards();
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
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
                        frmBrowse.Flag = false;
                        //frmBrowse.lkupVendor.EditValue = cmbVendor.EditValue.ToString();
                        //frmBrowse.lkupVendor.EditValue = "0";
                        //frmBrowse.FDate.EditValue = dtOrderDate.DateTime;
                        //frmBrowse.TDate.EditValue = dtOrderDate.DateTime;
                        frmBrowse.Flag = true;
                        //frmBrowse.FetchData(GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()), dtOrderDate.DateTime, dtOrderDate.DateTime);
                        frmBrowse.FetchData(GeneralFunctions.fnInt32(frmBrowse.lkupVendor.EditValue), GeneralFunctions.fnDate(frmBrowse.FDate.EditValue), GeneralFunctions.fnDate(frmBrowse.TDate.EditValue));
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            PopulateProduct(0);
            PopulateVendor();
            PopulateEmployee();
            SetDecimalPlace();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Purchase_Order;
                dtOrderDate.EditValue = DateTime.Today.Date;
                if (Settings.AutoPO == "Y")
                {
                    txtPO.Text = "AUTO";
                    txtPO.IsReadOnly = true;
                    txtPO.IsTabStop = false;
                }
            }
            else
            {
                Title.Text = Properties.Resources. Edit_Purchase_Order;
                ShowPOHeaderData();
                txtPO.IsReadOnly = true;
                txtPO.IsTabStop = false;
                cmbVendor.IsReadOnly = true;
            }
            PopulateBatchGrid();
            if (intID > 0) SetReadonlyPOItemIfReceived();
            boolControlChanged = false;
            blloading = true;
        }

        private void SetReadonlyPOItemIfReceived()
        {
            PosDataObject.PO objPO = new PosDataObject.PO();
            objPO.Connection = SystemVariables.Conn;
            int ReceivedCount = objPO.IfExistsInReceipt(intID);
            if (ReceivedCount > 0)
            {
                grdDept.IsEnabled = false;
            }
        }

        private void GridView1_ShowingEditor(object sender, ShowingEditorEventArgs e)
        {
            /*if (e.Column == colSKU)
            {
                if (cmbVendor.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources. Select_Vendor);
                    GeneralFunctions.SetFocus(cmbVendor);
                    e.Cancel = true;
                }
            }*/
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

                if (e.NewColumn.VisibleIndex == 1)
                {
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colSKU) == "")
                    {
                        return;
                    }
                    else
                    {
                       
                        if ((await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colSKU) != "") && (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) == ""))
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
                            GeneralFunctions.GetSKUDetails(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colSKU)), GeneralFunctions.fnInt32(cmbVendor.EditValue.ToString()),
                                ref strSKU, ref strDesciption, ref dblCost, ref strLabel, ref strPartNo, ref DP, ref intcaseqty, ref strcaseupc, ref strptype, ref strscalebarcode);
                            if (strDesciption.Trim() == "")	// Check for item code
                            {
                                if (intTimes == 1)
                                    DocMessage.MsgInformation("Invalid SKU");
                                grdDept.CurrentColumn = colSKU;
                                intTimes = 2;
                                return;
                            }
                            grdDept.SetCellValue(intRowNum, colDecimalPlace, DP);

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

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) == "") grdDept.SetCellValue(intRowNum, colQty, "1");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight) == "") grdDept.SetCellValue(intRowNum, colFreight, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax) == "") grdDept.SetCellValue(intRowNum, colTax, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCaseQty) == "") grdDept.SetCellValue(intRowNum, colCaseQty, "0");
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost) == "") grdDept.SetCellValue(intRowNum, colCost, dblCost);

                            grdDept.SetCellValue(intRowNum, colSKU1, strSKU);
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colDesc) == "") grdDept.SetCellValue(intRowNum, colDesc, strDesciption);
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colPartNo) == "") grdDept.SetCellValue(intRowNum, colPartNo, strPartNo);
                            grdDept.SetCellValue(intRowNum, colCaseQty, intcaseqty);
                            grdDept.SetCellValue(intRowNum, colCaseUPC, strcaseupc);

                            grdDept.SetCellValue(intRowNum, colProductType, strptype);
                            grdDept.SetCellValue(intRowNum, colScaleBarCode, strscalebarcode);

                            double dblQty = 0.00;
                            double dblAmount = 0.00;
                            double dblRate = 0.0;
                            double dblFreight = 0.0;
                            double dblTax = 0.0;

                            double dblCsQty = 0.00;


                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost) != "")
                                dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost));
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight) != "")
                                dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight));
                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax) != "")
                                dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax));

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                                dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));

                            if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCaseQty) != "")
                                dblCsQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCaseQty));

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
                            grdDept.SetCellValue(intRowNum, colTotal, dblAmount);

                            grdDept.CurrentColumn = colQty;

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

        private void GrdDept_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.Key == Key.Enter)
            {
                if (grdDept.CurrentColumn.Name == "colSKU")
                {
                    if (GeneralFunctions.fnInt32(grdDept.GetCellValue(gridView1.FocusedRowHandle, colSKU)) == 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        grdDept.CurrentColumn = grdDept.Columns["Qty"];
                        e.Handled = true;
                        return;
                    }
                }

                if (grdDept.CurrentColumn.Name == "colTax")
                {
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
                    grdDept.CurrentColumn = grdDept.Columns["ProductID"];
                    e.Handled = true;
                    return;
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

                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax));

                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCaseQty) != "")
                        intcaseqty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCaseQty));

                    dblQty = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        if (Settings.TaxInclusive == "Y")
                        {
                            double calculatedTaxAmount = TaxPercentage / 100 * (dblQty * dblRate) ;
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
                    grdDept.SetCellValue(intRowNum, colTotal, dblAmount);

                    if (Settings.TaxInclusive == "Y")
                    {
                        dblTax = GeneralFunctions.fnDouble(dblTax.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                        grdDept.SetCellValue(intRowNum, colTax, dblTax);
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

                    

                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax));



                    dblRate = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        if (Settings.TaxInclusive == "Y")
                        {
                            double calculatedTaxAmount = TaxPercentage / 100 * (dblQty * dblRate);
                            dblAmount = (dblQty * dblRate) + (dblFreight - calculatedTaxAmount);
                            dblTax = calculatedTaxAmount;
                        }
                        dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
                    }
                    catch
                    {
                        dblAmount = 0.00;
                    }
                    dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    grdDept.SetCellValue(intRowNum, colTotal, dblAmount);

                    if (Settings.TaxInclusive == "Y")
                    {
                        dblTax = GeneralFunctions.fnDouble(dblTax.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                        grdDept.SetCellValue(intRowNum, colTax, dblTax);
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



                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax) != "")
                        dblTax = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colTax));



                    dblFreight = GeneralFunctions.fnDouble(e.Value);

                    try
                    {
                        //if (Settings.TaxInclusive == "Y")
                        //{
                        //    double calculatedTaxAmount = TaxPercentage / 100 * (dblQty * dblRate);
                        //    dblAmount = (dblQty * dblRate) + (dblFreight - calculatedTaxAmount);
                        //    dblTax = calculatedTaxAmount;
                        //}
                        //else
                            dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty == 0) dblAmount = (dblQty * dblRate) + (dblFreight + dblTax);
                        //if (intcaseqty > 0) dblAmount = (dblQty * intcaseqty * dblRate) + (dblFreight + dblTax);
                    }
                    catch
                    {
                        dblAmount = 0.00;
                    }
                    dblAmount = GeneralFunctions.fnDouble(dblAmount.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    grdDept.SetCellValue(intRowNum, colTotal, dblAmount);

                    //if (Settings.TaxInclusive == "Y")
                    //{
                    //    dblTax = GeneralFunctions.fnDouble(dblTax.ToString(Settings.Use4Decimal == "Y" ? "f4" : Settings.DecimalPlace == 3 ? "f3" : "f"));
                    //    grdDept.SetCellValue(intRowNum, colTax, dblTax);
                    //}
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



                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty) != "")
                        dblQty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colQty));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost) != "")
                        dblRate = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colCost));
                    if (await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight) != "")
                        dblFreight = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdDept, colFreight));



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
                    grdDept.SetCellValue(intRowNum, colTotal, dblAmount);
                    GetAllAmount();
                }
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
        }

        private void TxtPO_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtPO_EditValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private async void LnkVendor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
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

        private void CmbPriority_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void GrdDept_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdDept.CurrentColumn.Name == "colSKU")
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
                    fkybrd.WindowName = "PO";
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
                    fkybrd.WindowName = "PO";
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
                    nkybrd.WindowName = "PO";
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
                    nkybrd.WindowName = "PO";
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
                    nkybrd.WindowName = "PO";
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
                    nkybrd.WindowName = "PO";
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
            if (gridcol == "VendorPartNo") grdDept.SetCellValue(rindx, colPartNo, val);
            if (gridcol == "Description") grdDept.SetCellValue(rindx, colDesc, val);
            if (gridcol == "Qty") grdDept.SetCellValue(rindx, colQty, val);
            if (gridcol == "Cost") grdDept.SetCellValue(rindx, colCost, val);
            if (gridcol == "Freight") grdDept.SetCellValue(rindx, colFreight, val);
            if (gridcol == "Tax") grdDept.SetCellValue(rindx, colTax, val);

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
