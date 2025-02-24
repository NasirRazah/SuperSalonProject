// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
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
using System.Data;
using System.Data.SqlClient;
using OfflineRetailV2.Data;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        #region Variables

        private bool boolPhotoChanged;
        private bool blFetchScale = false;
        private double dblCurrentCost = 0;
        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool boolControlChanged;

        private frm_ProductBrw frmBrowse;


        private int intPrevRow;
        private int intPrevTaxRow;
        private int intPrevProductModifierRow;
        private int intID;
        private int intNewID;
        private bool blPurchaseHistory = false;
        private bool blQuestion = false;
        private bool blSales = false;
        private string strSaveSKU = "";
        private string strPrevAltSKU = "";
        private string strPrevAltSKU2 = "";
        private string strPrevUPC = "";
        private bool blSetStyle = false;
        private bool blSetStyleScale = false;
        private bool blVendor = false;

        private bool blSalePrice = false;

        private bool blDuplicate;
        private bool blAddFromPOS;
        public bool blCallFromLookup = false;
        private string strAddSKU;
        private string strAddDescription;
        private int intAddCategory;
        private string strAddDisplayItemInPOS;
        private DataTable dtblDelete;
        private int ProductDecialPlace = 2;
        private double dblDisC = 0;
        private double dblCurC = 0;
        private double dblLastC = 0;
        private double dblPrA = 0;
        private double dblPrB = 0;
        private double dblPrC = 0;
        private bool blfocus = false;
        private int prevcategory = 0;
        private double prevOnHand = 0;
        private double prevLaywayQty = 0;
        private double prevReorderQty = 0;
        private double prevNormalQty = 0;
        private string prevProductType = "P";
        private int linksku = 0;
        private double prevpriceA = 0;
        private int prevQtyToPrint = 0;
        private int intSelectedRowHandle;
        private int DuplicateProductID = 0;
        private bool blclicksubitembutton = false;
        private bool boolchkPOSScreen_EditMode = false;

        private string RegisterModule = "";

        private string prevBookingExpFlag = "N";
        private string prevImageExportFlag = "N";

        private int OldTabIndex = 0;

        private bool boolPostData;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;


        public bool PostData
        {
            get { return boolPostData; }
            set { boolPostData = value; }
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

        public bool Duplicate
        {
            get { return blDuplicate; }
            set { blDuplicate = value; }
        }

        public bool AddFromPOS
        {
            get { return blAddFromPOS; }
            set { blAddFromPOS = value; }
        }

        public string AddSKU
        {
            get { return strAddSKU; }
            set { strAddSKU = value; }
        }

        public string AddDescription
        {
            get { return strAddDescription; }
            set { strAddDescription = value; }
        }

        public int AddCategory
        {
            get { return intAddCategory; }
            set { intAddCategory = value; }
        }

        public string AddDisplayItemInPOS
        {
            get { return strAddDisplayItemInPOS; }
            set { strAddDisplayItemInPOS = value; }
        }

        public frm_ProductBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_ProductBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }



        #endregion
        public AddProductWindow()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        #region Generate Lookup Data

        public void BrandLookup()
        {
            PosDataObject.Brand objDepartment = new PosDataObject.Brand();
            objDepartment.Connection = SystemVariables.Conn;
            objDepartment.DataObjectCulture_None = Settings.DataObjectCulture_None;
            DataTable dbtblDept = new DataTable();
            dbtblDept = objDepartment.FetchLookupData();
            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblDept.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }
            cmbBrand.ItemsSource = dtblTemp;
        }

        public void PopulateCategory()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchLookupData();

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

            cmbCategory.ItemsSource = dtblTemp;
        }

        /*public void PopulateProduct()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();

            dbtblSKU = objProduct.FetchMLookupData(0);
            repVendor.Columns[0].Caption = Translation.SetMultilingualTextInCodes("SKU", "frmProductDlg_SKU");
            repVendor.Columns[0].FieldName = "SKU";
            repVendor.Columns[1].Caption = Translation.SetMultilingualTextInCodes("Description", "frmProductDlg_Description");
            repVendor.Columns[1].FieldName = "Description";

            repVendor.DataSource = dbtblSKU;
            repVendor.DisplayMember = "SKU";
            repVendor.ValueMember = "SKU";
            repVendor.NullText = "";
            dbtblSKU.Dispose();
        }*/

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

            PART_Editor_VendorID.AutoPopulateColumns = false;
            PART_Editor_VendorID.ItemsSource = dtblTemp;

        }

        /*public void PopulateVendor1()
        {
            PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
            objVendor.Connection = SystemVariables.Conn;
            DataTable dbtblVendor = new DataTable();
            dbtblVendor = objVendor.FetchLookupData("D");
            repVendor1.DataSource = dbtblVendor;

            repVview.Columns[0].Caption = Translation.SetMultilingualTextInCodes("Vendor", "frmProductDlg_Vendor");
            repVview.Columns[0].FieldName = "Name";
            repVview.Columns[1].Caption = Translation.SetMultilingualTextInCodes("VendorID", "frmProductDlg_VendorID");
            repVview.Columns[1].FieldName = "VendorID";
            repVendor1.DisplayMember = "VendorID";

            repVendor1.ValueMember = "ID";
            repVendor1.NullText = "";
            dbtblVendor.Dispose();
        }*/

        public void PopulateTax()
        {
            PosDataObject.Tax objTax = new PosDataObject.Tax();
            objTax.Connection = SystemVariables.Conn;
            DataTable dbtblTax = new DataTable();
            dbtblTax = objTax.ShowTaxCombo();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblTax.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            PART_Editor_Tax.AutoPopulateColumns = false;
            PART_Editor_Tax.ItemsSource = dtblTemp;

            PART_Editor_RentTax.AutoPopulateColumns = false;
            PART_Editor_RentTax.ItemsSource = dtblTemp;

            /*repRepairTax.Columns[0].Caption = Translation.SetMultilingualTextInCodes("Name", "frmProductDlg_Name");
            repRepairTax.Columns[0].FieldName = "TaxName";
            repRepairTax.DataSource = dbtblTax;
            repRepairTax.DisplayMember = "TaxID";
            repRepairTax.ValueMember = "TaxID";
            repRepairTax.NullText = "";*/

            //dbtblTax.Dispose();
        }

        public void PopulatePrinter()
        {
            PosDataObject.Setup objTax = new PosDataObject.Setup();
            objTax.Connection = SystemVariables.Conn;
            objTax.DataObjectCulture_None = Settings.DataObjectCulture_None;
            DataTable dbtblPrntr = new DataTable();
            dbtblPrntr = objTax.FetchPrinterData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblPrntr.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            PART_Editor_Printer.AutoPopulateColumns = false;
            PART_Editor_Printer.ItemsSource = dtblTemp;
        }


        public void DeptLookup()
        {
            PosDataObject.Department objDepartment = new PosDataObject.Department();
            objDepartment.Connection = SystemVariables.Conn;
            DataTable dbtblDept = new DataTable();
            dbtblDept = objDepartment.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblDept.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbDept.ItemsSource = dtblTemp;
        }

        #endregion

        private void FillRowinTaxGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = 3 - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdTax.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount < intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "0", "", strip });
                }

                grdTax.ItemsSource = dtbl;
                //dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private void FillRowinRentTaxGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = SystemVariables.FIXEDGRIDROWS - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdrenttax.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "0", "", strip });
                }

                grdrenttax.ItemsSource = dtbl;
                //dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private void FillRowinPrinterGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = SystemVariables.FIXEDGRIDROWS - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();

                dtbl = (grdPrinter.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "", "", strip });
                }

                grdPrinter.ItemsSource = dtbl;
                //dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
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
                    dtbl.Rows.Add(new object[] { "0", "", "", "", null, false, "N", "", null, strip });
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

        private void PopulateBatchGrid()
        {
            try
            {
                PosDataObject.Product objProduct = new PosDataObject.Product();
                objProduct.Connection = SystemVariables.Conn;

                DataTable dbtbl = objProduct.ShowPrimayVendor(intID);
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

                dtblTemp.Dispose();
                dbtbl.Dispose();

                FillRowinGrid(((grdDept.ItemsSource) as DataTable).Rows.Count);
                gridView2.MoveFirstRow();

                bool f = false;
                foreach (DataRow dr in ((grdDept.ItemsSource) as DataTable).Rows)
                {
                    if (dr["IsPrimary"].ToString() == "True")
                    {
                        f = true;
                        break;
                    }

                }

                numCcost.IsReadOnly = f ? true : false;
            }
            finally
            {
            }

            try
            {
                DataTable dtblF_Tx = new DataTable();
                dtblF_Tx.Columns.Add("ID");
                dtblF_Tx.Columns.Add("TaxID");
                dtblF_Tx.Columns.Add("TaxName");

                PosDataObject.Product objTaxProduct = new PosDataObject.Product();
                objTaxProduct.Connection = SystemVariables.Conn;
                objTaxProduct.DecimalPlace = Settings.DecimalPlace;
                DataTable dtblTx = new DataTable();
                dtblTx = objTaxProduct.ShowTaxes(intID);
                foreach (DataRow dr in dtblTx.Rows)
                {
                    dtblF_Tx.Rows.Add(new object[] { dr["ID"].ToString(), dr["TaxID"].ToString(), dr["TaxName"].ToString() });
                }

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtblF_Tx.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }

                grdTax.ItemsSource = dtblTemp;
                FillRowinTaxGrid(((grdTax.ItemsSource) as DataTable).Rows.Count);

                dtblTemp.Dispose();

                gridView3.MoveFirstRow();
            }
            finally
            {
            }

            /*try
            {
                grdModifier.SuspendLayout();
                PosDataObject.Product objProductModifier = new PosDataObject.Product();
                objProductModifier.Connection = SystemVariables.Conn;
                grdModifier.DataSource = objProductModifier.ShowProductModifier(intID);
                FillRowinProdModifierGrid(((grdModifier.DataSource) as DataTable).Rows.Count);
                gridView6.MoveFirst();
            }
            finally
            {
                grdModifier.ResumeLayout();
            }
            */

            try
            {

                PosDataObject.Product objPrint = new PosDataObject.Product();
                objPrint.Connection = SystemVariables.Conn;


                DataTable dbtbl = objPrint.ShowPrinters1(intID, "Product");
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

                grdPrinter.ItemsSource = dtblTemp;

                dtblTemp.Dispose();
                dbtbl.Dispose();

                FillRowinPrinterGrid(((grdPrinter.ItemsSource) as DataTable).Rows.Count);
                gridView20.MoveFirstRow();
            }
            finally
            {

            }

            try
            {
                DataTable dtblF_Tx = new DataTable();
                dtblF_Tx.Columns.Add("ID");
                dtblF_Tx.Columns.Add("TaxID");
                dtblF_Tx.Columns.Add("TaxName");

                PosDataObject.Product objTaxProduct = new PosDataObject.Product();
                objTaxProduct.Connection = SystemVariables.Conn;
                objTaxProduct.DecimalPlace = Settings.DecimalPlace;

                DataTable dtblTx = new DataTable();
                dtblTx = objTaxProduct.ShowRentTaxes(intID);
                foreach (DataRow dr in dtblTx.Rows)
                {
                    dtblF_Tx.Rows.Add(new object[] { dr["ID"].ToString(), dr["TaxID"].ToString(), dr["TaxName"].ToString() });
                }

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtblF_Tx.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }

                grdrenttax.ItemsSource = dtblTemp;
                dtblTemp.Dispose();
                FillRowinRentTaxGrid(((grdrenttax.ItemsSource) as DataTable).Rows.Count);
                gridView9.MoveFirstRow();
            }
            finally
            {

            }
            /*
            try
            {
                grdrepairtax.SuspendLayout();
                PosDataObject.Product objTaxProduct = new PosDataObject.Product();
                objTaxProduct.Connection = SystemVariables.Conn;
                objTaxProduct.DecimalPlace = Settings.DecimalPlace;
                grdrepairtax.DataSource = objTaxProduct.ShowRepairTaxes(intID);
                FillRowinRepairTaxGrid(((grdrepairtax.DataSource) as DataTable).Rows.Count);
                gridView10.MoveFirst();
            }
            finally
            {
                grdrepairtax.ResumeLayout();
            }*/
        }

        private void PopulateProductType()
        {

        }

        public void CreateQuestionTree()
        {
            //Todo: trlistQ.ClearNodes();
            //DataTable dtblQ = new DataTable();
            //PosDataObject.Questions objQ = new PosDataObject.Questions();
            //objQ.Connection = new SqlConnection(SystemVariables.ConnectionString);
            //dtblQ = objQ.FetchRootData(intID, "Product");

            //string hid = "";
            //string lid = "";
            //string qid = "";
            //string qstion = "";

            //foreach (DataRow dr in dtblQ.Rows)
            //{
            //    hid = dr["HeaderID"].ToString();
            //    lid = dr["LevelID"].ToString();
            //    qid = dr["ID"].ToString();
            //    qstion = dr["Question"].ToString();

            //    TreeListNode node = null;
            //    node = trlistQ.AppendNode(new object[] { qid, hid, lid, qstion }, null);

            //    if (IsExistsNextLevel(GeneralFunctions.fnInt32(qid)) > 0)
            //    {
            //        RecursiveNextLevel(node, GeneralFunctions.fnInt32(qid));
            //    }

            //}
            //dtblQ.Dispose();
            //trlistQ.ExpandAll();
            //trlistQ.BestFitColumns();
        }

        private void IsUseStyle()
        {
            if (Settings.UseStyle == "NO")
            {
                rgStyle2.IsChecked = true;
                chkFontBold.Visibility = chkFontItalics.Visibility = txtFontType.Visibility = txtFontSize.Visibility = lbFontSize.Visibility = lbFontSize.Visibility = Visibility.Hidden;
                //Scale off
                /*
                rgStyleS.SelectedIndex = 1;
                groupControl11.Visible = false;
                groupControl10.Visible = true;
                txtFontTypeS.Visible = false;
                txtFontSizeS.Visible = false;
                label5.Visible = false;
                label2.Visible = false;
                label6.Top = label6.Top - 10;
                label11.Top = label11.Top - 10;
                txtFontColorS.Top = txtFontColorS.Top - 10;*/
            }
        }

        private void PermissionSettings()
        {
            if ((!SecurityPermission.AcssProductOnHandQty) && (SystemVariables.CurrentUserID > 0))
            {
                numOnHand.IsReadOnly = true;
            }

            if ((!SecurityPermission.AcssProductCost) && (SystemVariables.CurrentUserID > 0))
            {
                numCcost.IsReadOnly = true;
                numLPcost.IsReadOnly = true;
                numDcost.IsReadOnly = true;
            }

            if ((!SecurityPermission.AcssProductPrice) && (SystemVariables.CurrentUserID > 0))
            {
                numPriceA.IsReadOnly = true;
                numPriceB.IsReadOnly = true;
                numPriceC.IsReadOnly = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();


            RegisterModule = GeneralFunctions.RegisteredModules();

            btnProductType.Visibility = Visibility.Hidden;
            PopulateVendor();
            PopulateCategory();
            DeptLookup();
            BrandLookup();


            PopulateTax();
            PopulatePrinter();

            PopulateAndSetFormSkin();
            IsUseStyle();

            /*if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
            {
                RearrangeForScaleOnly();
            }
            */
            if (RegisterModule.Contains("SCALE") && RegisterModule.Contains("POS"))
            {
                //styleScreenStyleS.Visible = true;
                //groupControl9.Visible = true;
                numSplitWeight.Visibility = Visibility.Visible;
                lbSplitWeight.Visibility = Visibility.Visible;
            }

            if (Settings.Grad_ScaleType == "N") numTare2.IsEnabled = false;
            if (Settings.S_Check2Digit == "Y") numTare.Mask = "f2";
            if (Settings.D_Check2Digit == "Y") numTare2.Mask = "f2";

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Product;

                txtSKU.IsEnabled = true;

                lnkType.Visibility = Visibility.Hidden;

                if (RegisterModule.Contains("POS"))
                {
                    cmbProductType.SelectedIndex = 0;
                    chkZeroStock.IsChecked = true;
                    chkDisplayStock.IsChecked = true;
                    //chkPOSScreen.IsChecked = true;
                }

                if (!RegisterModule.Contains("SCALE"))
                {
                    chkScaleScreen.IsChecked = false;
                    chkScaleScreen.Visibility = Visibility.Hidden;
                    numSplitWeight.Visibility = Visibility.Hidden;
                    lbSplitWeight.Visibility = Visibility.Hidden;
                }


                if (blAddFromPOS)
                {
                    txtSKU.Text = strAddSKU;
                    txtSKU.IsEnabled = true;
                }
                if (blCallFromLookup)
                {
                    if (strAddSKU != "")
                    {
                        txtSKU.Text = strAddSKU;
                        txtSKU.IsEnabled = false;
                    }

                    if (strAddDescription != "")
                    {
                        txtDesc.Text = strAddDescription;
                    }
                }

                SetDecimalPlace();
                prevOnHand = 0;
                prevLaywayQty = 0;
                prevNormalQty = 0;
                prevReorderQty = 0;
                prevpriceA = 0;

            }
            else
            {

                if (!blDuplicate)
                {
                    Title.Text = Properties.Resources.Edit_Product;
                    cmbProductType.IsEnabled = false;
                    txtSKU.IsEnabled = false;

                }
                else
                {
                    Title.Text = Properties.Resources.New_Product;
                    if (RegisterModule != "SCALE") cmbProductType.IsEnabled = true;
                    txtSKU.IsEnabled = true;
                    SetDecimalPlace();
                    DuplicateProductID = intID;

                }

                ShowData();

                if (!RegisterModule.Contains("SCALE"))
                {

                    chkScaleScreen.IsChecked = false;
                    chkScaleScreen.Visibility = Visibility.Hidden;
                    numSplitWeight.Visibility = Visibility.Hidden;
                    lbSplitWeight.Visibility = Visibility.Hidden;
                }



                if (RegisterModule != "SCALE")
                {
                    if (!blDuplicate)
                    {
                        lnkType.Visibility = cmbProductType.SelectedIndex == 0 || cmbProductType.SelectedIndex == 6 ? Visibility.Visible : Visibility.Hidden;
                    }
                    else
                    {
                        lnkType.Visibility = Visibility.Hidden;
                    }
                }
            }
            tcProduct.TabIndex = 0;

            //GeneralFunctions.SetFocus(txtSKU);




            PermissionSettings();

            PopulateBatchGrid();

            if (blDuplicate)
            {
                intID = 0;
                txtSKU.Text = "";
                txtAltSKU.Text = "";
                txtAltSKU2.Text = "";
                txtUPC.Text = "";
                prevBookingExpFlag = "N";
            }

            if (intID > 0)
            {
                boolchkPOSScreen_EditMode = chkPOSScreen.IsChecked == true ? true : false;
            }

            SetItemOnHand();

            prevOnHand = GeneralFunctions.fnDouble(numOnHand.Text);
            prevLaywayQty = GeneralFunctions.fnDouble(numLayaway.Text);
            prevNormalQty = GeneralFunctions.fnDouble(numNormal.Text);
            prevReorderQty = GeneralFunctions.fnDouble(numReorder.Text);

            CalculatePriceMargin();

            if (RegisterModule.Contains("POS"))
            {
                rgStyle2.IsChecked = true;
                lbColor.Text = Properties.Resources.Color;
                cmbSkin.Visibility = Visibility.Collapsed;
                txtColor.Visibility = Visibility.Visible;
                lbdelaultcolor.Text = Properties.Resources.Leave_blank_default_color;
            }

            //Scale Off
            /*
            if (RegisterModule.Contains("SCALE"))
            {
                rgStyleS.SelectedIndex = 1;
                lbColorS.Text = Translation.SetMultilingualTextInCodes("Color", "frmProductDlg_Color");
                cmbSkinS.Visible = false;
                txtColorS.Visible = true;
                lbdelaultcolorS.Text = Translation.SetMultilingualTextInCodes("Leave blank for default color.", "frmProductDlg_Leaveblankfordefaultcolor");
            }*/

            boolControlChanged = false;
            boolPhotoChanged = false;
        }

        private void GridView3_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colTaxID")
            {
                if (IsDuplicateTax(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation(Properties.Resources.Tax_already_entered);

                    Dispatcher.BeginInvoke(new Action(() => gridView3.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Tax objGroup = new PosDataObject.Tax();
                    objGroup.Connection = SystemVariables.Conn;
                    grdTax.SetCellValue(e.RowHandle, colTaxName, objGroup.GetTaxDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                }


            }
        }

        private bool IsDuplicateTax(int refTax)
        {
            bool bDuplicate = false;
            DataTable dsource = grdTax.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == refTax)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private bool IsDuplicateRentTax(int refTax)
        {
            bool bDuplicate = false;
            DataTable dsource = grdrenttax.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == refTax)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private void PART_Editor_Tax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void GrdTax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdTax.CurrentColumn.Name == "colTaxID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdTax.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private async void BtnDelTax_Click(object sender, RoutedEventArgs e)
        {
            int TaxID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView3.FocusedRowHandle, grdTax, colTaxID));
            if (TaxID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView3.DeleteRow(gridView3.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private void GridView9_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumn12")
            {
                if (IsDuplicateRentTax(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation(Properties.Resources.Tax_already_entered);

                    Dispatcher.BeginInvoke(new Action(() => gridView9.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Tax objGroup = new PosDataObject.Tax();
                    objGroup.Connection = SystemVariables.Conn;
                    grdrenttax.SetCellValue(e.RowHandle, gridColumn13, objGroup.GetTaxDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                }


            }
        }

        private void Grdrenttax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdrenttax.CurrentColumn.Name == "gridColumn12")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdrenttax.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private async void SimpleButton1_Click(object sender, RoutedEventArgs e)
        {
            int TaxID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView9.FocusedRowHandle, grdrenttax, gridColumn12));
            if (TaxID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView9.DeleteRow(gridView9.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private bool IsDuplicatePrinter(int refPrinter)
        {
            bool bDuplicate = false;
            DataTable dsource = grdPrinter.ItemsSource as DataTable;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["PrinterID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["PrinterID"].ToString()) == refPrinter)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private async void GridView20_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colPrinterID")
            {
                if (IsDuplicatePrinter(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation(Properties.Resources.This_Printer_has_already_been_entered);

                    await Dispatcher.BeginInvoke(new Action(() => gridView20.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Setup objGroup = new PosDataObject.Setup();
                    objGroup.Connection = SystemVariables.Conn;
                    //grdPrinter.SetCellValue(e.RowHandle, colPrinterName, objGroup.GetPrinterDesc(GeneralFunctions.fnInt32(e.Value)));
                    grdPrinter.SetCellValue(e.RowHandle, colCutOffValue, "0");
                    //grdPrinter.CurrentColumn = colCutOffValue;

                    await Dispatcher.BeginInvoke(new Action(() =>
                    {
                        gridView20.ShowEditor();
                    }), System.Windows.Threading.DispatcherPriority.Loaded);
                    if (gridView20.ActiveEditor != null)
                    {
                        gridView20.ActiveEditor.SelectAll();
                    }

                    boolControlChanged = true;
                }


            }
        }

        private async void BtnDelPrinter_Click(object sender, RoutedEventArgs e)
        {
            int PrinterID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView20.FocusedRowHandle, grdPrinter, colPrinterID));
            if (PrinterID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView20.DeleteRow(gridView20.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private void GrdPrinter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (grdPrinter.CurrentColumn.Name == "colPrinterID")
                {
                    if (GeneralFunctions.fnInt32(grdPrinter.GetCellValue(gridView20.FocusedRowHandle, colPrinterID)) == 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        grdPrinter.CurrentColumn = grdPrinter.Columns["CutOffValue"];
                        e.Handled = true;
                        return;
                    }
                }

                if (grdPrinter.CurrentColumn.Name == "colQty")
                {
                    gridView20.FocusedRowHandle = gridView20.FocusedRowHandle + 1;
                    grdPrinter.CurrentColumn = grdPrinter.Columns["PrinterID"];
                    e.Handled = true;
                    return;
                }

            }
        }

        private void BtnDAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmbCategory_Drop(object sender, DragEventArgs e)
        {

        }

        private void FillRowinTaxGridC(int FilledupRow)
        {
            try
            {
                int intRowsToFill = 3 - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdTax.ItemsSource) as DataTable;
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                for (int intCount = 0; intCount < intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "0", "", strip });
                }

                grdTax.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private void PopulateTaxGrid()
        {
            try
            {
                PosDataObject.Category objTaxProduct = new PosDataObject.Category();
                objTaxProduct.Connection = SystemVariables.Conn;
                DataTable dbtbl = objTaxProduct.ShowTaxes(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));

                DataTable dtblF_Tx = new DataTable();
                dtblF_Tx.Columns.Add("ID");
                dtblF_Tx.Columns.Add("TaxID");
                dtblF_Tx.Columns.Add("TaxName");

                foreach (DataRow dr in dbtbl.Rows)
                {
                    dtblF_Tx.Rows.Add(new object[] { dr["ID"].ToString(), dr["TaxID"].ToString(), dr["TaxName"].ToString() });
                }

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                DataTable dtblTemp = dtblF_Tx.DefaultView.ToTable();
                DataColumn column = new DataColumn("Image");
                column.DataType = System.Type.GetType("System.Byte[]");
                column.AllowDBNull = true;
                column.Caption = "Image";
                dtblTemp.Columns.Add(column);

                foreach (DataRow dr in dtblTemp.Rows)
                {
                    dr["Image"] = strip;
                }
                grdTax.ItemsSource = dtblTemp;
                FillRowinTaxGridC(((grdTax.ItemsSource) as DataTable).Rows.Count);
                gridView3.MoveFirstRow();
            }
            finally
            {

            }
        }

        private void CmbCategory_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (cmbCategory.EditValue == null) return;
            string itemcolor = "";
            if (intID == 0)
            {
                string addposscreen = "";
                
                DataTable dtbl = new DataTable();
                try
                {
                    PosDataObject.Category objcat = new PosDataObject.Category();
                    objcat.Connection = SystemVariables.Conn;
                    dtbl = objcat.ShowRecord(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        itemcolor = dr["POSItemFontColor"].ToString();
                        addposscreen = dr["AddToPOSScreen"].ToString();
                        

                        if (dr["ScaleBarCode"].ToString() == "Y")
                        {
                            chkBarCode.IsChecked = true;
                        }
                        else
                        {
                            chkBarCode.IsChecked = false;
                        }

                        if (dr["AllowZeroStock"].ToString() == "Y")
                        {
                            chkZeroStock.IsChecked = true;
                        }
                        else
                        {
                            chkZeroStock.IsChecked = false;
                        }

                        if (dr["DisplayStockinPOS"].ToString() == "Y")
                        {
                            chkDisplayStock.IsChecked = true;
                        }
                        else
                        {
                            chkDisplayStock.IsChecked = false;
                        }

                        if (dr["FoodStampEligibleForProduct"].ToString() == "Y")
                        {
                            chkFoodStamp.IsChecked = true;
                        }
                        else
                        {
                            chkFoodStamp.IsChecked = false;
                        }

                        if (dr["PrintBarCode"].ToString() == "Y")
                        {
                            chkPrintLabel.IsChecked = true;
                        }
                        else
                        {
                            chkPrintLabel.IsChecked = false;
                        }

                        if (dr["NoPriceOnLabel"].ToString() == "Y")
                        {
                            chkNoPrice.IsChecked = true;
                        }
                        else
                        {
                            chkNoPrice.IsChecked = false;
                        }


                        if (dr["ProductStatus"].ToString() == "Y")
                        {
                            chkActive.IsChecked = true;
                        }
                        else
                        {
                            chkActive.IsChecked = false;
                        }
                        numMinAge.Text = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString()).ToString();
                        numLabel.Text = GeneralFunctions.fnInt32(dr["QtyToPrint"].ToString()).ToString();

                        if (dr["LabelType"].ToString() == "0")
                        {
                            cmbLabelType.SelectedIndex = 0;
                        }
                        else if (dr["LabelType"].ToString() == "1")
                        {
                            cmbLabelType.SelectedIndex = 1;
                        }
                        else if (dr["LabelType"].ToString() == "2")
                        {
                            cmbLabelType.SelectedIndex = 2;
                        }
                        else if (dr["LabelType"].ToString() == "3")
                        {
                            cmbLabelType.SelectedIndex = 3;
                        }
                        else
                        {
                            cmbLabelType.SelectedIndex = -1;
                        }

                        if (dr["RepairPromptForTag"].ToString() == "Y") chkRepairPromptTag.IsChecked = true;
                        else chkRepairPromptTag.IsChecked = false;

                        if (dr["NonDiscountable"].ToString() == "Y") chkNoDiscount.IsChecked = true; else chkNoDiscount.IsChecked = false;

                    }
                }
                finally
                {
                    dtbl.Dispose();
                }

                //if (addposscreen == "Y") chkPOSScreen.IsChecked = true;
                //if (addposscreen == "N") chkPOSScreen.IsChecked = false;

               

                if (!((itemcolor == "") || (itemcolor == "0") || (itemcolor.Contains("#00000000"))))
                {
                    txtFontColor.Color = (Color)ColorConverter.ConvertFromString(itemcolor);
                }


                PopulateTaxGrid();
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                boolControlChanged = true;
                boolPhotoChanged = true;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
            boolPhotoChanged = true;
        }

        // Visible additional button for specific product types 

        private void GetProductTypeButton()
        {
            numOnHand.IsEnabled = true;
            if (cmbProductType.SelectedIndex == 1)
            {
                btnProductType.Content = Properties.Resources.Unit_of_Measure;
                btnProductType.Visibility = Visibility.Visible;
                chkTagInInvoice.IsChecked = false;
                chkTagInInvoice.Visibility = Visibility.Hidden;
            }
            else if (cmbProductType.SelectedIndex == 2)
            {
                btnProductType.Content = Properties.Resources.Matrix;

                numOnHand.IsEnabled = false;
                btnProductType.Visibility = Visibility.Visible;
                chkTagInInvoice.IsChecked = false;
                chkTagInInvoice.Visibility = Visibility.Hidden;
            }
            else if (cmbProductType.SelectedIndex == 3)
            {
                btnProductType.Content = Properties.Resources.Serialization;
                btnProductType.Visibility = Visibility.Visible;
                chkTagInInvoice.IsChecked = false;
                chkTagInInvoice.Visibility = Visibility.Hidden;
            }
            else if (cmbProductType.SelectedIndex == 4)
            {
                btnProductType.Content = Properties.Resources.Kit_Contents;
                btnProductType.Visibility = Visibility.Visible;
                chkTagInInvoice.IsChecked = false;
                chkTagInInvoice.Visibility = Visibility.Hidden;
            }
            /*else if (cmbProductType.SelectedIndex == 7)
            {
                btnProductType.Text = Translation.SetMultilingualTextInCodes("Tagged Item", "frmProductDlg_TaggedItem");
                btnProductType.Visible = true;
                chkTagInInvoice.Visible = true;
            }*/
            else
            {
                btnProductType.Visibility = Visibility.Hidden;
                chkTagInInvoice.IsChecked = false;
                chkTagInInvoice.Visibility = Visibility.Hidden;
            }

        }

        private void CmbProductType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            GetProductTypeButton();
            dblDisC = GeneralFunctions.fnDouble(numDcost.Text);
            dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
            dblLastC = GeneralFunctions.fnDouble(numLPcost.Text);
            dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
            dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
            dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);


            if (cmbProductType.SelectedIndex == 5)
            {
                numDcost.Mask = "f3";
                numCcost.Mask = "f3";
                numLPcost.Mask = "f3";
                numPriceA.Mask = "f3";
                numPriceB.Mask = "f3";
                numPriceC.Mask = "f3";
                //////repCost.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //////repCost.EditFormat.FormatString = "f3";
            }
            else
            {
                numDcost.Mask = "f2";
                numCcost.Mask = "f2";
                numLPcost.Mask = "f2";
                numPriceA.Mask = "f2";
                numPriceB.Mask = "f2";
                numPriceC.Mask = "f2";
                //////repCost.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //////repCost.EditFormat.FormatString = "f";
            }
            numDcost.Text = dblDisC.ToString();
            numCcost.Text = dblCurC.ToString();
            numLPcost.Text = dblLastC.ToString();
            numPriceA.Text = dblPrA.ToString();
            numPriceB.Text = dblPrB.ToString();
            numPriceC.Text = dblPrC.ToString();

            if (cmbProductType.SelectedIndex == 6)
            {
                string ResigteredModule = GeneralFunctions.RegisteredModules();
                if (ResigteredModule.Contains("POS"))
                {
                    lbTare.Visibility = numTare.Visibility = Visibility.Visible;
                    lbTare2.Visibility = numTare2.Visibility = Visibility.Visible;
                }
                else
                {
                    lbTare.Visibility = numTare.Visibility = Visibility.Hidden;
                    lbTare2.Visibility = numTare2.Visibility = Visibility.Hidden;
                }
                lnkType.Text = Properties.Resources.Change_to_Product;
            }
            else
            {
                lbTare.Visibility = numTare.Visibility = Visibility.Hidden;
                lbTare2.Visibility = numTare2.Visibility = Visibility.Hidden;
                numTare.Text = "0.000";
                lnkType.Text = Properties.Resources.Change_to_Weighted;
            }

            if (cmbProductType.SelectedIndex == 6)
            {
                wb1.Visibility = wb2.Visibility = Visibility.Visible;
                cmbUOM.Visibility = lbUOM.Visibility = Visibility.Visible;
                cmbUOM.SelectedIndex = 0;
            }
            else
            {
                wb1.Visibility = wb2.Visibility = Visibility.Hidden;
                cmbUOM.Visibility = lbUOM.Visibility = Visibility.Hidden;
                cmbUOM.SelectedIndex = -1;
            }

            if (cmbProductType.SelectedIndex == 3)
            {
                lbExpiry.Visibility = dtExpiry.Visibility = Visibility.Collapsed;
            }
            else
            {
                lbExpiry.Visibility = dtExpiry.Visibility = Visibility.Visible;
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateCount(txtSKU.Text.Trim());
        }

        private int AlterSKUCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.AltSKUCount(txtAltSKU.Text.Trim());
        }

        private int AlterSKU2Count()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.AltSKU2Count(txtAltSKU2.Text.Trim());
        }

        private int UPCCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.UPCCount(txtUPC.Text.Trim());
        }

        // Check validity before posting

        private bool IsValidAll()
        {
            if (!IsValidGrid()) return false;
            if (txtSKU.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.SKU);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtSKU);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                }

                return false;
            }

            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    if (strSaveSKU != txtSKU.Text.Trim())
                    {
                        DocMessage.MsgInformation(Properties.Resources.Duplicate_SKU);
                        tcProduct.SelectedIndex = 0;
                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(txtSKU);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }

                if ((AlterSKUCount() == 1) && (txtAltSKU.Text.Trim() != ""))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Alt_SKU);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtAltSKU);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtAltSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if ((AlterSKU2Count() == 1) && (txtAltSKU2.Text.Trim() != ""))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Alt_SKU_2);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtAltSKU2);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtAltSKU2); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if ((UPCCount() == 1) && (txtUPC.Text.Trim() != ""))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_UPC);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtUPC);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtUPC); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (intID > 0)
            {
                if ((AlterSKUCount() == 1) && (txtAltSKU.Text.Trim() != "") && (txtAltSKU.Text.Trim() != strPrevAltSKU))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Alt_SKU);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtAltSKU);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtAltSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if ((AlterSKU2Count() == 1) && (txtAltSKU2.Text.Trim() != "") && (txtAltSKU2.Text.Trim() != strPrevAltSKU2))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Alt_SKU_2);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtAltSKU2);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtAltSKU2); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }

                if ((UPCCount() == 1) && (txtUPC.Text.Trim() != "") && (txtUPC.Text.Trim() != strPrevUPC))
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_UPC);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(txtUPC);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtUPC); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            if (txtDesc.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Description);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(txtDesc);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(txtDesc); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (cmbDept.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Department);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(cmbDept);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbDept); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (cmbCategory.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.POS_Screen_Category);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(cmbCategory);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbCategory); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }



            if (chkPOSScreen.IsChecked == true)
            {
                if ((intID > 0) && (boolchkPOSScreen_EditMode))
                {
                }
                else
                {
                    PosDataObject.Product objProd = new PosDataObject.Product();
                    objProd.Connection = SystemVariables.Conn;
                    int intProd = objProd.GetPOSProductsforCategory(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));
                    int intAllow = objProd.GetMaxPOSforCategory(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));

                    if (intProd >= intAllow)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Cannot_add_more_products_in_POS_Screen_for_this_Category + "\n" + Properties.Resources.Please_increase_this_count_in_Category);
                        tcProduct.SelectedIndex = 0;
                        if (OldTabIndex == 0)
                        {
                            GeneralFunctions.SetFocus(cmbCategory);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbCategory); }), System.Windows.Threading.DispatcherPriority.Render);
                        }
                        return false;
                    }
                }
            }





            return true;
        }

        private bool IsValidGrid()
        {
            return true;
        }

        private void SetDefaultStyle()
        {

            txtFontType.Text = Settings.DefaultItemFontType;
            txtFontSize.Text = Settings.DefaultItemFontSize;
        }

        private void SetCategoryStyle()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            string strColor = "";
            string strFontColor = "";
            DataTable dbtbl = new DataTable();
            dbtbl = objCategory.ShowRecord(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));
            foreach (DataRow dr in dbtbl.Rows)
            {
                strColor = dr["POSScreenColor"].ToString();

                if (!((strColor == "") || (strColor == "0") || (strColor.Contains("#00000000"))))
                {
                    txtColor.Color = (Color)ColorConverter.ConvertFromString(strColor);
                }
                strFontColor = dr["POSItemFontColor"].ToString();
                if (!((strFontColor == "") || (strFontColor == "0") || (strFontColor.Contains("#00000000"))))
                {
                    txtFontColor.Color = (Color)ColorConverter.ConvertFromString(strFontColor);
                }
                txtFontType.Text = dr["POSFontType"].ToString();
                txtFontSize.Text = dr["POSFontSize"].ToString();
                if (dr["IsBold"].ToString() == "Y")
                {
                    chkFontBold.IsChecked = true;
                }
                else
                {
                    chkFontBold.IsChecked = false;
                }
                if (dr["IsItalics"].ToString() == "Y")
                {
                    chkFontItalics.IsChecked = true;
                }
                else
                {
                    chkFontItalics.IsChecked = false;
                }
            }
        }

        private void SetDecimalPlace()
        {
            if (ProductDecialPlace == 3)
            {
                numDcost.Mask = "f3";
                numCcost.Mask = "f3";
                numLPcost.Mask = "f3";
                numPriceA.Mask = "f3";
                numPriceB.Mask = "f3";
                numPriceC.Mask = "f3";

                PART_EDITOR_Cost.Mask = "f3";
                PART_Editor_Gross.Mask = "f3";
                PART_Editor_Freight.Mask = "f3";
                PART_Editor_PTax.Mask = "f3";
                PART_Editor_Total.Mask = "f3";
            }
            else
            {
                numDcost.Mask = "f2";
                numCcost.Mask = "f2";
                numLPcost.Mask = "f2";
                numPriceA.Mask = "f2";
                numPriceB.Mask = "f2";
                numPriceC.Mask = "f2";

                PART_EDITOR_Cost.Mask = "f2";
                PART_Editor_Gross.Mask = "f2";
                PART_Editor_Freight.Mask = "f2";
                PART_Editor_PTax.Mask = "f2";
                PART_Editor_Total.Mask = "f2";
            }
        }


        // Assign Detail info. of the Item

        public void ShowData()
        {
            string strColor = "";
            string strFontColor = "";

            string strColorS = "";
            string strFontColorS = "";

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                prevBookingExpFlag = dr["BookingExpFlag"].ToString();
                linksku = GeneralFunctions.fnInt32(dr["LinkSKU"].ToString());
                txtUPC.Text = dr["UPC"].ToString();
                txtSKU.Text = dr["SKU"].ToString();
                txtAltSKU.Text = dr["SKU2"].ToString();
                txtAltSKU2.Text = dr["SKU3"].ToString();
                strPrevAltSKU = txtAltSKU.Text;
                strPrevAltSKU2 = txtAltSKU2.Text;
                strPrevUPC = txtUPC.Text;
                txtDesc.Text = dr["Description"].ToString();

                if (dr["ExpiryDate"].ToString() == "")
                {
                    dtExpiry.EditValue = null;
                }
                else
                {
                    dtExpiry.EditValue = GeneralFunctions.fnDate(dr["ExpiryDate"].ToString());
                }

                prevProductType = dr["ProductType"].ToString();

                if (dr["ProductType"].ToString() == "P")
                {
                    cmbProductType.SelectedIndex = 0;
                }
                else if (dr["ProductType"].ToString() == "U")
                {
                    cmbProductType.SelectedIndex = 1;
                }
                else if (dr["ProductType"].ToString() == "M")
                {
                    cmbProductType.SelectedIndex = 2;
                }
                else if (dr["ProductType"].ToString() == "E")
                {
                    cmbProductType.SelectedIndex = 3;
                }
                else if (dr["ProductType"].ToString() == "K")
                {
                    cmbProductType.SelectedIndex = 4;
                }
                else if (dr["ProductType"].ToString() == "F")
                {
                    cmbProductType.SelectedIndex = 5;
                }
                else if (dr["ProductType"].ToString() == "W")
                {
                    cmbProductType.SelectedIndex = 6;
                }
                else if (dr["ProductType"].ToString() == "D")
                {
                    cmbProductType.SelectedIndex = 7;
                }
                else if (dr["ProductType"].ToString() == "Q")
                {
                    cmbProductType.SelectedIndex = 8;
                }
                else
                {
                    cmbProductType.SelectedIndex = 9;
                }

                if (cmbProductType.SelectedIndex == 5)
                {
                    numDcost.Mask = "f3";
                    numCcost.Mask = "f3";
                    numLPcost.Mask = "f3";
                    numPriceA.Mask = "f3";
                    numPriceB.Mask = "f3";
                    numPriceC.Mask = "f3";
                    PART_EDITOR_Cost.Mask = "f3";
                }
                else
                {
                    numDcost.Mask = "f2";
                    numCcost.Mask = "f2";
                    numLPcost.Mask = "f2";
                    numPriceA.Mask = "f2";
                    numPriceB.Mask = "f2";
                    numPriceC.Mask = "f2";
                    PART_EDITOR_Cost.Mask = "f2";
                }

                if (dr["PromptForPrice"].ToString() == "Y")
                {
                    chkPromptPrice.IsChecked = true;
                }
                else
                {
                    chkPromptPrice.IsChecked = false;
                }
                if (dr["AddtoPOSScreen"].ToString() == "Y")
                {
                    chkPOSScreen.IsChecked = true;
                }
                else
                {
                    chkPOSScreen.IsChecked = false;
                }

                if (dr["AddToPOSCategoryScreen"].ToString() == "Y")
                {
                    chkPOSCatScreen.IsChecked = true;
                }
                else
                {
                    chkPOSCatScreen.IsChecked = false;
                }


                if (dr["ScaleBarCode"].ToString() == "Y")
                {
                    chkBarCode.IsChecked = true;
                }
                else
                {
                    chkBarCode.IsChecked = false;
                }

                if (dr["AllowZeroStock"].ToString() == "Y")
                {
                    chkZeroStock.IsChecked = true;
                }
                else
                {
                    chkZeroStock.IsChecked = false;
                }

                if (dr["DisplayStockinPOS"].ToString() == "Y")
                {
                    chkDisplayStock.IsChecked = true;
                }
                else
                {
                    chkDisplayStock.IsChecked = false;
                }

                if (dr["AddToScaleScreen"].ToString() == "Y")
                {
                    chkScaleScreen.IsChecked = true;
                }
                else
                {
                    chkScaleScreen.IsChecked = false;
                }

                cmbBrand.EditValue = dr["BrandID"].ToString();
                cmbDept.EditValue = dr["DepartmentID"].ToString();
                cmbCategory.EditValue = dr["CategoryID"].ToString();
                prevcategory = GeneralFunctions.fnInt32(dr["CategoryID"].ToString());
                numMinAge.Text = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString()).ToString();

                if (dr["FoodStampEligible"].ToString() == "Y")
                {
                    chkFoodStamp.IsChecked = true;
                }
                else
                {
                    chkFoodStamp.IsChecked = false;
                }
                txtBinLocation.Text = dr["BinLocation"].ToString();

                if ((dr["DecimalPlace"].ToString() == "3") || (dr["ProductType"].ToString() == "F"))
                {
                    ProductDecialPlace = 3;
                }
                else
                {
                    ProductDecialPlace = 2;
                }

                if (cmbProductType.SelectedIndex != 5) SetDecimalPlace();

                numSplitWeight.Text = GeneralFunctions.fnDouble(dr["SplitWeight"].ToString()).ToString();
                cmbUOM.Text = dr["UOM"].ToString();

                numTare.Text = GeneralFunctions.fnDouble(dr["Tare"].ToString()).ToString();
                numTare2.Text = GeneralFunctions.fnDouble(dr["Tare2"].ToString()).ToString();

                numDcost.Text = GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString()).ToString();

                numCcost.Text = GeneralFunctions.fnDouble(dr["Cost"].ToString()).ToString();
                numLPcost.Text = GeneralFunctions.fnDouble(dr["LastCost"].ToString()).ToString();

                numPriceA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numPriceB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numPriceC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();

                dblDisC = GeneralFunctions.fnDouble(numDcost.Text);
                dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
                dblLastC = GeneralFunctions.fnDouble(numLPcost.Text);
                dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
                dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
                dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);

                prevpriceA = GeneralFunctions.fnDouble(numPriceA.Text);

                txtPoints.Text = GeneralFunctions.fnDouble(dr["Points"].ToString()).ToString();

                numOnHand.Text = GeneralFunctions.fnDouble(dr["QtyOnHand"].ToString()).ToString();
                numReorder.Text = GeneralFunctions.fnDouble(dr["ReorderQty"].ToString()).ToString();
                numNormal.Text = GeneralFunctions.fnDouble(dr["NormalQty"].ToString()).ToString();

                numLayaway.Text = GeneralFunctions.fnDouble(dr["QtyOnLayaway"].ToString()).ToString();

                txtNotes.Text = dr["ProductNotes"].ToString();
                txtNotes2.Text = dr["Notes2"].ToString();

                txtCaseQty.Text = GeneralFunctions.fnDouble(dr["CaseQty"].ToString()).ToString();
                txtSeason.Text = dr["Season"].ToString();
                txtCaseUPC.Text = dr["CaseUPC"].ToString();

                if (dr["PrintBarCode"].ToString() == "Y")
                {
                    chkPrintLabel.IsChecked = true;
                }
                else
                {
                    chkPrintLabel.IsChecked = false;
                }

                if (dr["NoPriceOnLabel"].ToString() == "Y")
                {
                    chkNoPrice.IsChecked = true;
                }
                else
                {
                    chkNoPrice.IsChecked = false;
                }


                if (dr["ProductStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

                if (dr["TaggedInInvoice"].ToString() == "Y")
                {
                    chkTagInInvoice.IsChecked = true;
                }
                else
                {
                    chkTagInInvoice.IsChecked = false;
                }

                numLabel.Text = GeneralFunctions.fnInt32(dr["QtyToPrint"].ToString()).ToString();
                prevQtyToPrint = GeneralFunctions.fnInt32(numLabel.Text);

                if (dr["LabelType"].ToString() == "0")
                {
                    cmbLabelType.SelectedIndex = 0;
                }
                else if (dr["LabelType"].ToString() == "1")
                {
                    cmbLabelType.SelectedIndex = 1;
                }
                else if (dr["LabelType"].ToString() == "2")
                {
                    cmbLabelType.SelectedIndex = 2;
                }
                else if (dr["LabelType"].ToString() == "3")
                {
                    cmbLabelType.SelectedIndex = 3;
                }
                else
                {
                    cmbLabelType.SelectedIndex = -1;
                }



                numRentalMinuteAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerMinute"].ToString()).ToString();
                numRentalHourAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerHour"].ToString()).ToString();
                numRentalHalfDayAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerHalfDay"].ToString()).ToString();
                numRentalDayAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerDay"].ToString()).ToString();
                numRentalWeekAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerWeek"].ToString()).ToString();
                numRentalMonthAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerMonth"].ToString()).ToString();
                numRentalDeposit.Text = GeneralFunctions.fnDouble(dr["RentalDeposit"].ToString()).ToString();
                numRentalMinHour.Text = GeneralFunctions.fnDouble(dr["RentalMinHour"].ToString()).ToString();
                numRentalMinAmt.Text = GeneralFunctions.fnDouble(dr["RentalMinAmount"].ToString()).ToString();

                if (dr["RentalPrompt"].ToString() == "Y") chkRentPrompt.IsChecked = true; else chkRentPrompt.IsChecked = false;

                if (dr["RepairPromptForCharge"].ToString() == "Y") chkRepairPromptPrice.IsChecked = true;
                else chkRepairPromptPrice.IsChecked = false;

                if (dr["RepairPromptForTag"].ToString() == "Y") chkRepairPromptTag.IsChecked = true;
                else chkRepairPromptTag.IsChecked = false;

                if (dr["NonDiscountable"].ToString() == "Y") chkNoDiscount.IsChecked = true; else chkNoDiscount.IsChecked = false;

                numRepairCrg.Text = GeneralFunctions.fnDouble(dr["RepairCharge"].ToString()).ToString();

                if (!blDuplicate)
                {
                    prevImageExportFlag = dr["ShopifyImageExportFlag"].ToString();

                    GeneralFunctions.LoadPhotofromDB("Product", intID, pictPhoto);
                }

                // display style

                strColor = dr["POSScreenColor"].ToString();

                if (!((strColor == "") || (strColor == "0") || (strColor.Contains("#00000000"))))
                {
                    txtColor.Color = (Color)ColorConverter.ConvertFromString(strColor);
                }

                strFontColor = dr["POSFontColor"].ToString();
                if (!((strFontColor == "") || (strFontColor == "0") || (strFontColor.Contains("#00000000"))))
                {
                    txtFontColor.Color = (Color)ColorConverter.ConvertFromString(strFontColor);
                }

                txtFontType.Text = dr["POSFontType"].ToString();
                txtFontSize.Text = dr["POSFontSize"].ToString();
                if (txtFontType.SelectedIndex == -1)
                {
                    txtFontType.Text = "Tahoma";
                }
                if (dr["IsBold"].ToString() == "Y")
                {
                    chkFontBold.IsChecked = true;
                }
                else
                {
                    chkFontBold.IsChecked = false;
                }
                if (dr["IsItalics"].ToString() == "Y")
                {
                    chkFontItalics.IsChecked = true;
                }
                else
                {
                    chkFontItalics.IsChecked = false;
                }
                if (dr["POSBackground"].ToString() == "Skin")
                {
                    rgStyle1.IsChecked = true;
                    cmbSkin.Text = dr["POSScreenStyle"].ToString();
                }
                if (dr["POSBackground"].ToString() == "Color") rgStyle2.IsChecked = true;


                // Scale Off


                // display style

                /*

                strColorS = dr["ScaleScreenColor"].ToString();

                if (!((strColorS == "") || (strColorS.Contains("0"))))
                {
                    txtColorS.Text = strColorS;
                }

                strFontColorS = dr["ScaleFontColor"].ToString();
                if (!((strFontColorS == "") || (strFontColorS.Contains("0"))))
                {
                    txtFontColorS.Text = strFontColorS;
                }

                txtFontTypeS.SelectedIndex = txtFontTypeS.Properties.Items.IndexOf(dr["ScaleFontType"].ToString());
                txtFontSizeS.SelectedIndex = txtFontSizeS.Properties.Items.IndexOf(dr["ScaleFontSize"].ToString());
                if (txtFontTypeS.SelectedIndex == -1)
                {
                    txtFontTypeS.SelectedIndex = txtFontTypeS.Properties.Items.IndexOf("Tahoma");
                }
                if (dr["ScaleIsBold"].ToString() == "Y")
                {
                    chkFontBoldS.Checked = true;
                }
                else
                {
                    chkFontBoldS.Checked = false;
                }
                if (dr["ScaleIsItalics"].ToString() == "Y")
                {
                    chkFontItalicsS.Checked = true;
                }
                else
                {
                    chkFontItalicsS.Checked = false;
                }
                if (dr["ScaleBackground"].ToString() == "Skin")
                {
                    rgStyleS.SelectedIndex = 0;
                    cmbSkinS.SelectedIndex = cmbSkinS.Properties.Items.IndexOf(dr["ScaleScreenStyle"].ToString());
                }
                if (dr["ScaleBackground"].ToString() == "Color") rgStyleS.SelectedIndex = 1;

                if (rgStyleS.SelectedIndex == -1)
                {
                    rgStyleS.SelectedIndex = 0;
                    cmbSkinS.SelectedIndex = -1;
                }
                */
            }
        }


        private bool SaveData()
        {

            string strError = "";
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;
            objProduct.UPC = txtUPC.Text.Trim();
            objProduct.SKU = txtSKU.Text.Trim();
            objProduct.SKU2 = txtAltSKU.Text.Trim();
            objProduct.SKU3 = txtAltSKU2.Text.Trim();
            objProduct.Description = txtDesc.Text.Trim();
            objProduct.BinLocation = txtBinLocation.Text.Trim();
            objProduct.PrimaryVendorID = "";
            objProduct.BookingExpFlag = prevBookingExpFlag;

            if (cmbProductType.SelectedIndex == 6)
            {
                objProduct.Tare = GeneralFunctions.fnDouble(numTare.Text);
                objProduct.Tare2 = GeneralFunctions.fnDouble(numTare2.Text);
            }
            else
            {
                objProduct.Tare = 0;
                objProduct.Tare2 = 0;
            }
            if (cmbBrand.EditValue != null)
            {
                objProduct.BrandID = GeneralFunctions.fnInt32(cmbBrand.EditValue.ToString());
            }
            else
            {
                objProduct.BrandID = 0;
            }

            if (cmbDept.EditValue != null)
            {
                objProduct.DepartmentID = GeneralFunctions.fnInt32(cmbDept.EditValue.ToString());
            }
            else
            {
                objProduct.DepartmentID = 0;
            }
            if (cmbCategory.EditValue != null)
            {
                objProduct.CategoryID = GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString());
            }
            else
            {
                objProduct.CategoryID = 0;
            }

            if (chkPromptPrice.IsChecked == true)
            {
                objProduct.PromptForPrice = "Y";
            }
            else
            {
                objProduct.PromptForPrice = "N";
            }

            if (chkPOSScreen.IsChecked == true)
            {
                objProduct.AddtoPOSScreen = "Y";
            }
            else
            {
                objProduct.AddtoPOSScreen = "N";
            }

            if (chkPOSCatScreen.IsChecked == true)
            {
                objProduct.AddtoPOSCategoryScreen = "Y";
            }
            else
            {
                objProduct.AddtoPOSCategoryScreen = "N";
            }

            if (chkBarCode.IsChecked == true)
            {
                objProduct.ScaleBarCode = "Y";
            }
            else
            {
                objProduct.ScaleBarCode = "N";
            }

            if (chkFoodStamp.IsChecked == true)
            {
                objProduct.FoodStampEligible = "Y";
            }
            else
            {
                objProduct.FoodStampEligible = "N";
            }

            if (chkPrintLabel.IsChecked == true)
            {
                objProduct.PrintBarCode = "Y";
            }
            else
            {
                objProduct.PrintBarCode = "N";
            }

            if (chkNoPrice.IsChecked == true)
            {
                objProduct.NoPriceOnLabel = "Y";
            }
            else
            {
                objProduct.NoPriceOnLabel = "N";
            }

            if (chkZeroStock.IsChecked == true)
            {
                objProduct.AllowZeroStock = "Y";
            }
            else
            {
                objProduct.AllowZeroStock = "N";
            }

            if (chkDisplayStock.IsChecked == true)
            {
                objProduct.DisplayStockinPOS = "Y";
            }
            else
            {
                objProduct.DisplayStockinPOS = "N";
            }

            if (chkActive.IsChecked == true)
            {
                objProduct.ProductStatus = "Y";
            }
            else
            {
                objProduct.ProductStatus = "N";
            }

            if (chkTagInInvoice.IsChecked == true)
            {
                objProduct.TaggedInInvoice = "Y";
            }
            else
            {
                objProduct.TaggedInInvoice = "N";
            }

            if (chkNoDiscount.IsChecked == true)
            {
                objProduct.NonDiscountable = "Y";
            }
            else
            {
                objProduct.NonDiscountable = "N";
            }

            if (chkScaleScreen.IsChecked == true)
            {
                objProduct.AddtoScaleScreen = "Y";
            }
            else
            {
                objProduct.AddtoScaleScreen = "N";
            }
            objProduct.DiscountedCost = GeneralFunctions.fnDouble(numDcost.Text.Trim());
            objProduct.Cost = GeneralFunctions.fnDouble(numCcost.Text.Trim());
            objProduct.LastCost = GeneralFunctions.fnDouble(numLPcost.Text.Trim());
            objProduct.PriceA = GeneralFunctions.fnDouble(numPriceA.Text.Trim());
            objProduct.PriceB = GeneralFunctions.fnDouble(numPriceB.Text.Trim());
            objProduct.PriceC = GeneralFunctions.fnDouble(numPriceC.Text.Trim());
            objProduct.QtyOnHand = GeneralFunctions.fnDouble(numOnHand.Text.Trim());
            objProduct.ReorderQty = GeneralFunctions.fnDouble(numReorder.Text.Trim());
            objProduct.NormalQty = GeneralFunctions.fnDouble(numNormal.Text.Trim());
            objProduct.QtyOnLayaway = GeneralFunctions.fnDouble(numLayaway.Text.Trim());
            objProduct.MinimumAge = GeneralFunctions.fnInt32(numMinAge.Text.Trim());
            objProduct.QtyToPrint = GeneralFunctions.fnInt32(numLabel.Text.Trim());
            objProduct.ProductNotes = txtNotes.Text.Trim();
            objProduct.Notes2 = txtNotes2.Text.Trim();
            objProduct.Points = GeneralFunctions.fnInt32(txtPoints.Text.Trim());
            objProduct.CaseQty = GeneralFunctions.fnInt32(txtCaseQty.Text.Trim());
            objProduct.Season = txtSeason.Text.Trim();
            objProduct.CaseUPC = txtCaseUPC.Text.Trim();

            objProduct.RentalPerMinute = GeneralFunctions.fnDouble(numRentalMinuteAmt.Text.Trim());
            objProduct.RentalPerHour = GeneralFunctions.fnDouble(numRentalHourAmt.Text.Trim());
            objProduct.RentalPerHalfDay = GeneralFunctions.fnDouble(numRentalHalfDayAmt.Text.Trim());
            objProduct.RentalPerDay = GeneralFunctions.fnDouble(numRentalDayAmt.Text.Trim());
            objProduct.RentalPerWeek = GeneralFunctions.fnDouble(numRentalWeekAmt.Text.Trim());
            objProduct.RentalPerMonth = GeneralFunctions.fnDouble(numRentalMonthAmt.Text.Trim());
            objProduct.RentalDeposit = GeneralFunctions.fnDouble(numRentalDeposit.Text.Trim());
            if (chkRentPrompt.IsChecked == true) objProduct.RentalPrompt = "Y"; else objProduct.RentalPrompt = "N";

            objProduct.RentalMinHour = GeneralFunctions.fnDouble(numRentalMinHour.Text.Trim());
            objProduct.RentalMinAmount = GeneralFunctions.fnDouble(numRentalMinAmt.Text.Trim());

            objProduct.RepairCharge = GeneralFunctions.fnDouble(numRepairCrg.Text.Trim());
            if (chkRepairPromptPrice.IsChecked == true) objProduct.RepairPromptForCharge = "Y"; else objProduct.RepairPromptForCharge = "N";
            if (chkRepairPromptTag.IsChecked == true) objProduct.RepairPromptForTag = "Y"; else objProduct.RepairPromptForTag = "N";

            objProduct.MinimumServiceTime = 0;

            if (cmbProductType.SelectedIndex == 0) objProduct.ProductType = "P";
            else if (cmbProductType.SelectedIndex == 1) objProduct.ProductType = "U";
            else if (cmbProductType.SelectedIndex == 2) objProduct.ProductType = "M";
            else if (cmbProductType.SelectedIndex == 3) objProduct.ProductType = "E";
            else if (cmbProductType.SelectedIndex == 4) objProduct.ProductType = "K";
            else if (cmbProductType.SelectedIndex == 5) objProduct.ProductType = "F";
            else if (cmbProductType.SelectedIndex == 6) objProduct.ProductType = "W";
            else if (cmbProductType.SelectedIndex == 7) objProduct.ProductType = "D";
            else if (cmbProductType.SelectedIndex == 8) objProduct.ProductType = "Q";
            else objProduct.ProductType = "T";

            if (intID > 0)
            {
                if (prevProductType != objProduct.ProductType)
                {
                    objProduct.ChangeProductType = objProduct.ProductType;
                }
                else
                {
                    objProduct.ChangeProductType = "";
                }
            }
            else
            {
                objProduct.ChangeProductType = "";
            }

            // Style

            if ((intID == 0) && (!blVendor) && (!blDuplicate)) SetCategoryStyle();    // set style in add mode if it is not set

            if (rgStyle1.IsChecked == true)
            {
                objProduct.POSBackground = "Skin";
                objProduct.POSScreenColor = "0";
                objProduct.POSScreenStyle = cmbSkin.Text.Trim();

            }
            if (rgStyle2.IsChecked == true)
            {
                objProduct.POSBackground = "Color";
                objProduct.POSScreenColor = txtColor.Color.ToString();
                objProduct.POSScreenStyle = "";
            }
            objProduct.POSFontType = txtFontType.Text.Trim();
            objProduct.POSFontSize = GeneralFunctions.fnInt32(txtFontSize.Text.Trim());
            objProduct.POSFontColor = txtFontColor.Color.ToString();
            if (chkFontBold.IsChecked == true)
            {
                objProduct.IsBold = "Y";
            }
            else
            {
                objProduct.IsBold = "N";
            }
            if (chkFontItalics.IsChecked == true)
            {
                objProduct.IsItalics = "Y";
            }
            else
            {
                objProduct.IsItalics = "N";
            }

            /* Scale not implemented yet
             
            if ((rgStyleS.SelectedIndex == 0) || (rgStyleS.SelectedIndex == -1))
            {
                objProduct.ScaleBackground = "Skin";
                objProduct.ScaleScreenColor = "0";
                objProduct.ScaleScreenStyle = cmbSkinS.Text.Trim();

            }
            if (rgStyleS.SelectedIndex == 1)
            {
                objProduct.ScaleBackground = "Color";
                objProduct.ScaleScreenColor = txtColorS.Color.Name;
                objProduct.ScaleScreenStyle = "";
            }

            objProduct.ScaleFontType = txtFontTypeS.Text.Trim();
            objProduct.ScaleFontSize = GeneralFunctions.fnInt32(txtFontSizeS.Text.Trim());
            objProduct.ScaleFontColor = txtFontColorS.Color.Name;

            if (chkFontBoldS.Checked)
            {
                objProduct.ScaleIsBold = "Y";
            }
            else
            {
                objProduct.ScaleIsBold = "N";
            }
            if (chkFontItalicsS.Checked)
            {
                objProduct.ScaleIsItalics = "Y";
            }
            else
            {
                objProduct.ScaleIsItalics = "N";
            }
            */

            objProduct.ScaleBackground = "Skin";
            objProduct.ScaleScreenColor = "0";
            objProduct.ScaleScreenStyle = "DevExpress Skin";
            objProduct.ScaleFontType = "Arial";
            objProduct.ScaleFontSize = 8;
            objProduct.ScaleFontColor = "0";
            objProduct.ScaleIsBold = "N";
            objProduct.ScaleIsItalics = "N";


            if (cmbLabelType.SelectedIndex == 0)
            {
                objProduct.LabelType = 0;
            }
            else if (cmbLabelType.SelectedIndex == 1)
            {
                objProduct.LabelType = 1;
            }
            else if (cmbLabelType.SelectedIndex == 2)
            {
                objProduct.LabelType = 2;
            }
            else if (cmbLabelType.SelectedIndex == 3)
            {
                objProduct.LabelType = 3;
            }
            else
            {
                objProduct.LabelType = -1;
            }

            if (ReScan == 1)  // Photo Scanned or Selected from Folder but not saved
            {
                objProduct.PhotoFilePath = strPhotoFile;
            }
            else
            {
                objProduct.PhotoFilePath = "";
            }

            if (pictPhoto.Source != null)
            {
                objProduct.ProductPhoto = GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source);
            }
            else
            {
                objProduct.ProductPhoto = null;
            }



            objProduct.ID = intID;
            if (intID == 0)
            {
                if (intNewID > 0) objProduct.ID = intNewID;
            }


            if (blAddFromPOS)
            {
                intAddCategory = objProduct.CategoryID;
                strAddDisplayItemInPOS = objProduct.AddtoPOSScreen;
            }

            objProduct.BreakPackRatio = 0;
            objProduct.LinkSKU = linksku;

            gridView2.PostEditor();
            gridView3.PostEditor();
            objProduct.VendorDataTable = (grdDept.ItemsSource as DataTable);
            objProduct.TaxDataTable = (grdTax.ItemsSource as DataTable);
            objProduct.RentTaxDataTable = (grdrenttax.ItemsSource as DataTable);
            objProduct.RepairTaxDataTable = (grdrepairtax.ItemsSource as DataTable);
            objProduct.ProductModifierDataTable = null; //(grdModifier.DataSource as DataTable);

            objProduct.TerminalName = Settings.TerminalName;


            if (GeneralFunctions.fnDouble(numOnHand.Text) != prevOnHand)
            {
                objProduct.AddJournal = true;
                bool blPExists = IfExistsInJournal();
                if (GeneralFunctions.fnDouble(numOnHand.Text) > prevOnHand)
                {

                    objProduct.JournalQty = GeneralFunctions.fnDouble(numOnHand.Text) - prevOnHand;
                    objProduct.JournalType = "Stock In";
                    if (!blPExists)
                    {
                        objProduct.JournalSubType = "Opening Stock";
                    }
                    else
                    {
                        objProduct.JournalSubType = "Manual Adjustment";
                    }
                }
                else
                {

                    if (!blPExists)
                    {
                        objProduct.JournalQty = GeneralFunctions.fnDouble(numOnHand.Text) - prevOnHand;
                        objProduct.JournalSubType = "Opening Stock";
                        objProduct.JournalType = "Stock In";
                    }
                    else
                    {
                        objProduct.JournalQty = prevOnHand - GeneralFunctions.fnDouble(numOnHand.Text);
                        objProduct.JournalSubType = "Manual Adjustment";
                        objProduct.JournalType = "Stock Out";
                    }
                }
            }
            else
            {
                objProduct.AddJournal = false;
            }

            objProduct.ProductDecimalPlace = 2;



            objProduct.PrinterDataTable = grdPrinter.ItemsSource as DataTable;


            if ((GeneralFunctions.fnDouble(numOnHand.Text) != prevOnHand) || (GeneralFunctions.fnDouble(numLayaway.Text) != prevLaywayQty) || (GeneralFunctions.fnDouble(numNormal.Text) != prevNormalQty)
                || (GeneralFunctions.fnDouble(numReorder.Text) != prevReorderQty)) objProduct.ChangeInventory = true;
            else objProduct.ChangeInventory = false;

            int tpqty = GeneralFunctions.fnInt32(numLabel.Text);
            objProduct.PrintLabelQty = tpqty - prevQtyToPrint;

            // 03-12-2013    Reorder Items in POS – Retail 
            if (intID > 0)
            {
                if (GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()) == prevcategory) objProduct.IsNewCategory = false;
                else objProduct.IsNewCategory = true;
            }

            objProduct.PrevPriceA = prevpriceA;
            objProduct.SplitWeight = cmbProductType.SelectedIndex == 6 ? GeneralFunctions.fnDouble(numSplitWeight.Text) : 0;
            objProduct.UOM = cmbProductType.SelectedIndex == 6 ? cmbUOM.Text : "";

            if (intID == 0)
            {
                objProduct.ShopifyImageExportFlag = "N";
            }
            else
            {
                if (boolPhotoChanged)
                {
                    objProduct.ShopifyImageExportFlag = "N";
                }
                else
                {
                    objProduct.ShopifyImageExportFlag = prevImageExportFlag;
                }
            }

            if (cmbProductType.SelectedIndex != 3)
            {
                if (dtExpiry.EditValue == null)
                {
                    objProduct.ExpiryDate = Convert.ToDateTime(null);
                }
                else
                {
                    objProduct.ExpiryDate = dtExpiry.DateTime.Date;
                }
            }
            else
            {
                objProduct.ExpiryDate = Convert.ToDateTime(null);
            }

            

            strError = objProduct.PostData_WPF();
            intNewID = objProduct.ID;

            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool IfExistsInJournal()
        {
            PosDataObject.StockJournal objsj = new PosDataObject.StockJournal();
            objsj.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsj.IsExistRecord(intID);
        }


        private void SetItemOnHand()
        {
            PosDataObject.StockJournal objsj1 = new PosDataObject.StockJournal();
            objsj1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            string strreturn = objsj1.GetProductType(intID);
            if (strreturn == "M")
                numOnHand.IsReadOnly = true;
            else
                numOnHand.IsReadOnly = false;
        }


        private void BtnProductType_Click(object sender, RoutedEventArgs e)
        {
            bool blexec = false;
            int intGetID = 0;

            if (intID == 0)
            {
                if (IsValidAll())
                {
                    if (SaveData())
                    {
                        blexec = true;

                        boolchkPOSScreen_EditMode = chkPOSScreen.IsChecked == true ? true : false;
                        intGetID = intNewID;
                        intID = intNewID;
                        strSaveSKU = txtSKU.Text.Trim();
                        strPrevAltSKU = txtAltSKU.Text.Trim();
                        strPrevAltSKU2 = txtAltSKU2.Text.Trim();
                        prevcategory = GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString());
                        txtSKU.IsEnabled = false;
                        SetItemOnHand();

                        lnkType.Visibility = cmbProductType.SelectedIndex == 0 || cmbProductType.SelectedIndex == 6 ? Visibility.Visible : Visibility.Hidden;

                        if (cmbProductType.SelectedIndex == 0) prevProductType = "P";
                        else if (cmbProductType.SelectedIndex == 1) prevProductType = "U";
                        else if (cmbProductType.SelectedIndex == 2) prevProductType = "M";
                        else if (cmbProductType.SelectedIndex == 3) prevProductType = "E";
                        else if (cmbProductType.SelectedIndex == 4) prevProductType = "K";
                        else if (cmbProductType.SelectedIndex == 5) prevProductType = "F";
                        else if (cmbProductType.SelectedIndex == 6) prevProductType = "W";
                        else prevProductType = "T";

                    }
                    else
                    {
                        blexec = false;
                        return;
                    }
                }
                else
                {
                    blexec = false;
                    return;
                }
            }
            else
            {
                blexec = true;
                intGetID = intID;
            }

            if (blexec)
            {
                if (cmbProductType.SelectedIndex == 1)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_UOMBrw frm_UOMBrw = new frm_UOMBrw();
                    try
                    {
                        frm_UOMBrw.PID = intGetID;
                        frm_UOMBrw.Title.Text = Properties.Resources.UOM_Definition + " " + txtDesc.Text;
                        frm_UOMBrw.ShowDialog();
                    }
                    finally
                    {
                        frm_UOMBrw.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }

                if (cmbProductType.SelectedIndex == 2)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_MatrixProduct frmMatrixProduct = new frm_MatrixProduct();
                    try
                    {
                        frmMatrixProduct.CalledFrom = "Product";
                        if (!blclicksubitembutton && blDuplicate) frmMatrixProduct.FID = DuplicateProductID;
                        else frmMatrixProduct.FID = intGetID;
                        frmMatrixProduct.PID = intGetID;
                        frmMatrixProduct.Title.Text = Properties.Resources.Matrix_Definition + " " + txtDesc.Text;
                        frmMatrixProduct.ShowDialog();
                        if (frmMatrixProduct.OK == true)
                        {
                            numOnHand.Text = frmMatrixProduct.GetStockTotal().ToString();
                        }
                    }
                    finally
                    {
                        frmMatrixProduct.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }

                if (cmbProductType.SelectedIndex == 4)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_KitBrw frm_KitBrw = new frm_KitBrw();
                    try
                    {
                        frm_KitBrw.PID = intGetID;
                        frm_KitBrw.Title.Text = Properties.Resources.Kit_Definition + " " + txtDesc.Text;
                        frm_KitBrw.ShowDialog();
                    }
                    finally
                    {
                        frm_KitBrw.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                }

                if (cmbProductType.SelectedIndex == 3)
                {
                    blurGrid.Visibility = Visibility.Visible;
                    frm_SerialBrw frm_SerialBrw = new frm_SerialBrw();
                    try
                    {
                        frm_SerialBrw.CAT = "ALL";
                        frm_SerialBrw.PID = intGetID;
                        frm_SerialBrw.Title.Text = Properties.Resources.Serialization_Definition + " " + txtDesc.Text;
                        frm_SerialBrw.ShowDialog();
                    }
                    finally
                    {
                        frm_SerialBrw.Close();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }

                }

                /*if (cmbProductType.SelectedIndex == 7)
                {
                    frmTagBrw frm_TagBrw = new frmTagBrw();
                    try
                    {
                        frm_TagBrw.PID = intGetID;
                        frm_TagBrw.Text = Translation.SetMultilingualTextInCodes("Attach Tag for : ", "frmProductDlg_AttachTagfor") + txtDesc.Text;
                        frm_TagBrw.ShowDialog();
                    }
                    finally
                    {
                        frm_TagBrw.Dispose();
                    }
                }*/
                blclicksubitembutton = true;
            }
        }


        // Calculate & Assign Price with Cost Margin 

        private void CalculatePriceMargin()
        {
            if (GeneralFunctions.fnDouble(numCcost.Text) == 0)
            {
                if (GeneralFunctions.fnDouble(numPriceA.Text) == 0) lbPriceMarginA.Text = "NA";
                else lbPriceMarginA.Text = "100 %";
                if (GeneralFunctions.fnDouble(numPriceB.Text) == 0) lbPriceMarginB.Text = "NA";
                else lbPriceMarginB.Text = "100 %";
                if (GeneralFunctions.fnDouble(numPriceC.Text) == 0) lbPriceMarginC.Text = "NA";
                else lbPriceMarginC.Text = "100 %";
            }
            else
            {
                if (GeneralFunctions.fnDouble(numPriceA.Text) == 0) lbPriceMarginA.Text = "NA";
                else lbPriceMarginA.Text = GeneralFunctions.fnDouble(((GeneralFunctions.fnDouble(numPriceA.Text) - GeneralFunctions.fnDouble(numCcost.Text)) / GeneralFunctions.fnDouble(numPriceA.Text)) * 100).ToString("f").Replace(".00", "") + " %";
                if (GeneralFunctions.fnDouble(numPriceB.Text) == 0) lbPriceMarginB.Text = "NA";
                else lbPriceMarginB.Text = GeneralFunctions.fnDouble(((GeneralFunctions.fnDouble(numPriceB.Text) - GeneralFunctions.fnDouble(numCcost.Text)) / GeneralFunctions.fnDouble(numPriceB.Text)) * 100).ToString("f").Replace(".00", "") + " %";
                if (GeneralFunctions.fnDouble(numPriceC.Text) == 0) lbPriceMarginC.Text = "NA";
                else lbPriceMarginC.Text = GeneralFunctions.fnDouble(((GeneralFunctions.fnDouble(numPriceC.Text) - GeneralFunctions.fnDouble(numCcost.Text)) / GeneralFunctions.fnDouble(numPriceC.Text)) * 100).ToString("f").Replace(".00", "") + " %";
            }
        }

        private void NumCcost_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            CalculatePriceMargin();
            boolControlChanged = true;
        }

        private void PopulateAndSetFormSkin()
        {
            DataTable data = new DataTable();
            data.Columns.Add("skin");
            foreach (DevExpress.Skins.SkinContainer sk in DevExpress.Skins.SkinManager.Default.Skins)
            {
                data.Rows.Add(new object[] { sk.SkinName });
            }
            cmbSkin.ItemsSource = data;
            blSetStyle = true;
        }

        private void RgStyle1_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            if (rgStyle1.IsChecked == true)  // Skin
            {
                lbColor.Text = Properties.Resources.Style;
                cmbSkin.Visibility = Visibility.Visible;
                txtColor.Visibility = Visibility.Collapsed;
                lbdelaultcolor.Text = Properties.Resources.Leave_blank_default_style;

            }
            else    // Absolute color
            {

                lbColor.Text = Properties.Resources.Color;
                cmbSkin.Visibility = Visibility.Collapsed;
                txtColor.Visibility = Visibility.Visible;
                lbdelaultcolor.Text = Properties.Resources.Leave_blank_default_color;
            }
        }

        private void GridView2_CellMerge(object sender, DevExpress.Xpf.Grid.CellMergeEventArgs e)
        {

        }

        private async void GridView2_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colVendor")
            {
                bool lbDuplicateFound = false;
                DataTable dtbl = grdDept.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["VendorID"].ToString() == e.Value.ToString())
                    {
                        lbDuplicateFound = true;
                        break;
                    }
                }

                if (lbDuplicateFound)
                {
                    DocMessage.MsgInformation(Properties.Resources.This_Vendor_has_already_been_entered_modify);
                    await Dispatcher.BeginInvoke(new Action(() => gridView2.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Vendor objVendor = new PosDataObject.Vendor();
                    objVendor.Connection = SystemVariables.Conn;
                    grdDept.SetCellValue(e.RowHandle, colName, objVendor.GetVendorName(GeneralFunctions.fnInt32(e.Value)));
                    grdDept.SetCellValue(e.RowHandle, colCost, cmbProductType.SelectedIndex == 5 ? "0.000" : "0.00");
                    grdDept.SetCellValue(e.RowHandle, colShrink, "0.00");
                    grdDept.SetCellValue(e.RowHandle, colPackQty, "1");

                    //grdDept.CurrentColumn = colPartNo;

                    await Dispatcher.BeginInvoke(new Action(() =>
                    {
                        gridView2.ShowEditor();
                    }), System.Windows.Threading.DispatcherPriority.Loaded);
                    if (gridView2.ActiveEditor != null)
                    {
                        gridView2.ActiveEditor.SelectAll();
                    }

                    boolControlChanged = true;
                }


            }

            if (e.Column.Name == "colCost")
            {
                boolControlChanged = true;
                string strNew = e.Value.ToString();
                int intRow = gridView2.FocusedRowHandle;
                if (intRow == -1) return;

                if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colVendor) == "")
                {
                    e.Handled = true;
                }
                else
                {
                    if (strNew != "")
                    {
                        grdDept.SetCellValue(intRow, colCost, strNew);
                        if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colPrimary) == "True")
                        {
                            dblCurrentCost = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colCost)) /
                                GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colPackQty));

                            if (cmbProductType.SelectedIndex == 6)
                            {
                                double shrinkperc = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colShrink));
                                double addcost = GeneralFunctions.fnDouble(dblCurrentCost * shrinkperc / 100);
                                dblCurrentCost = dblCurrentCost + addcost;
                            }

                            numCcost.Text = dblCurrentCost.ToString();
                            numCcost.IsReadOnly = true;
                        }
                    }
                }
            }

            if (e.Column.Name == "colPackQty")
            {
                boolControlChanged = true;
                string strNew = e.Value.ToString();
                int intRow = gridView2.FocusedRowHandle;
                if (intRow == -1) return;

                if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colVendor) == "")
                {
                    e.Handled = true;
                }
                else
                {
                    if (strNew != "")
                    {
                        if (GeneralFunctions.fnInt32(strNew) == 0) strNew = e.OldValue == null ? "1" : e.OldValue.ToString();

                        grdDept.SetCellValue(intRow, colPackQty, strNew);

                        if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colPrimary) == "True")
                        {
                            dblCurrentCost = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colCost)) /
                                GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colPackQty));

                            if (cmbProductType.SelectedIndex == 6)
                            {
                                double shrinkperc = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colShrink));
                                double addcost = GeneralFunctions.fnDouble(dblCurrentCost * shrinkperc / 100);
                                dblCurrentCost = dblCurrentCost + addcost;
                            }

                            numCcost.Text = dblCurrentCost.ToString();
                            numCcost.IsReadOnly = true;
                        }
                    }
                }
            }


            if (e.Column.Name == "colShrink")
            {
                boolControlChanged = true;
                string strNew = e.Value.ToString();
                int intRow = gridView2.FocusedRowHandle;
                if (intRow == -1) return;

                if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colVendor) == "")
                {
                    e.Handled = true;
                }
                else
                {
                    if (strNew != "")
                    {
                        grdDept.SetCellValue(intRow, colShrink, strNew);
                        if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colPrimary) == "True")
                        {
                            dblCurrentCost = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colCost)) /
                                GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colPackQty));

                            if (cmbProductType.SelectedIndex == 6)
                            {
                                double shrinkperc = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colShrink));
                                double addcost = GeneralFunctions.fnDouble(dblCurrentCost * shrinkperc / 100);
                                dblCurrentCost = dblCurrentCost + addcost;
                            }

                            numCcost.Text = dblCurrentCost.ToString();
                            numCcost.IsReadOnly = true;
                        }
                    }
                }
            }

            if (e.Column.Name == "colPrimary")
            {
                boolControlChanged = true;
                string strNew = e.Value.ToString();
                int intRow = gridView2.FocusedRowHandle;
                if (intRow == -1) return;

                if (e.OldValue.ToString().ToUpper() == "TRUE")
                {
                    e.Handled = true;
                }
                else if (await GeneralFunctions.GetCellValue1(intRow, grdDept, colVendor) == "")
                {
                    e.Handled = true;
                }
                else
                {
                    DataTable dtbl = grdDept.ItemsSource as DataTable;
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        dr["IsPrimary"] = "False";
                    }
                    grdDept.ItemsSource = dtbl;
                    grdDept.SetCellValue(intRow, colPrimary, strNew);
                    if (strNew.ToUpper() == "TRUE")
                    {
                        dblCurrentCost = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colCost)) /
                                GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colPackQty));

                        if (cmbProductType.SelectedIndex == 6)
                        {
                            double shrinkperc = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRow, grdDept, colShrink));
                            double addcost = GeneralFunctions.fnDouble(dblCurrentCost * shrinkperc / 100);
                            dblCurrentCost = dblCurrentCost + addcost;
                        }

                        numCcost.Text = dblCurrentCost.ToString();
                        numCcost.IsReadOnly = true;
                    }
                    else
                    {
                        dblCurrentCost = 0;

                        numCcost.Text = dblCurrentCost.ToString();
                        numCcost.IsReadOnly = false;
                    }
                    dtbl.Dispose();
                }





            }
        }

        private void TpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TabItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TcProduct_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = tcProduct.SelectedIndex;

            //Scale off

            /*if (tcProduct.SelectedTabPageIndex == 3)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolchkPOSScreen_EditMode = chkPOSScreen.Checked;
                            intID = NewID;
                            strSaveSKU = txtSKU.Text.Trim();
                            strPrevAltSKU = txtAltSKU.Text.Trim();
                            strPrevAltSKU2 = txtAltSKU2.Text.Trim();
                            prevcategory = GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString());
                            prevpriceA = numPriceA.Value;
                            txtSKU.Enabled = false;
                            SetItemOnHand();
                            lnkType.Visible = cmbProductType.SelectedIndex == 0 || cmbProductType.SelectedIndex == 6;

                            if (cmbProductType.SelectedIndex == 0) prevProductType = "P";
                            else if (cmbProductType.SelectedIndex == 1) prevProductType = "U";
                            else if (cmbProductType.SelectedIndex == 2) prevProductType = "M";
                            else if (cmbProductType.SelectedIndex == 3) prevProductType = "E";
                            else if (cmbProductType.SelectedIndex == 4) prevProductType = "K";
                            else if (cmbProductType.SelectedIndex == 5) prevProductType = "F";
                            else if (cmbProductType.SelectedIndex == 6) prevProductType = "W";
                            else prevProductType = "T";
                        }
                    }
                }

                if (!blFetchScale)
                {
                    FetchScaleData();
                    blFetchScale = true;
                }
            }*/

            /*if (tcProduct.SelectedTabPageIndex == 6)
            {
                if (!blPurchaseHistory)
                {
                    FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                    TDate.EditValue = DateTime.Today.Date;
                    blPurchaseHistory = true;
                }
            }*/

            /* Question not used now
            
            if (tcProduct.SelectedTabPageIndex == 6)
            {
                if (intID == 0)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            intID = NewID;
                            boolchkPOSScreen_EditMode = chkPOSScreen.Checked;
                            strSaveSKU = txtSKU.Text.Trim();
                            strPrevAltSKU = txtAltSKU.Text.Trim();
                            strPrevAltSKU2 = txtAltSKU2.Text.Trim();
                            prevcategory = GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString());
                            prevpriceA = numPriceA.Value;
                            txtSKU.Enabled = false;
                            SetItemOnHand();
                            lnkType.Visible = cmbProductType.SelectedIndex == 0 || cmbProductType.SelectedIndex == 6;

                            if (cmbProductType.SelectedIndex == 0) prevProductType = "P";
                            else if (cmbProductType.SelectedIndex == 1) prevProductType = "U";
                            else if (cmbProductType.SelectedIndex == 2) prevProductType = "M";
                            else if (cmbProductType.SelectedIndex == 3) prevProductType = "E";
                            else if (cmbProductType.SelectedIndex == 4) prevProductType = "K";
                            else if (cmbProductType.SelectedIndex == 5) prevProductType = "F";
                            else if (cmbProductType.SelectedIndex == 6) prevProductType = "W";
                            else prevProductType = "T";
                        }
                    }
                }
                if (!blQuestion)
                {
                    DataTable dtbl = new DataTable();
                    dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                    dtblDelete = dtbl;
                    dtbl.Dispose();
                    CreateQuestionTree();
                    blQuestion = true;
                }
            }*/

            if (i == 5)
            {
                if (!blSales)
                {
                    FetchSalesData();

                    FDate.EditValue = DateTime.Today.Date.AddDays(-7);
                    TDate.EditValue = DateTime.Today.Date;

                    blSales = true;
                }
            }

            if (i == 2)
            {
                if ((intID == 0) && (!blDuplicate))
                {
                    if (!blVendor)
                    {
                        if (cmbCategory.Text.Trim() == "")
                        {
                            SetDefaultStyle();
                        }
                        else
                        {
                            SetCategoryStyle();
                        }
                        blVendor = true;
                    }
                }

                //Scale Off
                /*
                if ((RegisterModule.Contains("SCALE")) && (cmbProductType.SelectedIndex == 6))
                {
                    styleScreenStyleS.Visible = true;
                    groupControl9.Visible = true;
                }
                else
                {
                    styleScreenStyleS.Visible = false;
                    groupControl9.Visible = false;
                }*/
            }

            if (i == 7)
            {
                if (!blSalePrice)
                {
                    int salebid = 0;
                    string saleapply = "";
                    string salebatch = "";
                    string saleperiod = "";
                    double saleprice = 0;

                    PosDataObject.Product op1 = new PosDataObject.Product();
                    op1.Connection = SystemVariables.Conn;
                    op1.ActiveSale(intID, ref salebid, ref salebatch, ref saleapply, ref saleperiod, ref saleprice, SystemVariables.DateFormat);

                    if (salebid > 0)
                    {
                        lnksb.Text = salebatch;
                        lbsp1.Tag = salebid;
                        lbsp2.Text = saleapply;
                        lbsp3.Text = GeneralFunctions.FormatDouble1(saleprice);
                        lbsp4.Text = saleperiod;

                        pnlSalePrice.Visibility = Visibility.Visible;
                        pnlNoSaleBatch.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        pnlSalePrice.Visibility = Visibility.Collapsed;
                        pnlNoSaleBatch.Visibility = Visibility.Visible;
                    }

                    PosDataObject.Product op2 = new PosDataObject.Product();
                    op2.Connection = SystemVariables.Conn;
                    DataTable dtbl = op2.ActiveFuturePrice(intID, SystemVariables.DateFormat);

                    if (dtbl.Rows.Count > 0)
                    {
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




                        grd.ItemsSource = dtblTemp;
                        grd.Visibility = Visibility.Visible;
                        pnlNoFutureBatch.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        grd.Visibility = Visibility.Collapsed;
                        pnlNoFutureBatch.Visibility = Visibility.Visible;
                    }

                    DataTable dtblmx = op2.ActiveMixMatch(intID, SystemVariables.DateFormat);

                    if (dtblmx.Rows.Count > 0)
                    {
                        byte[] strip = GeneralFunctions.GetImageAsByteArray();

                        DataTable dtblTemp = dtblmx.DefaultView.ToTable();
                        DataColumn column = new DataColumn("Image");
                        column.DataType = System.Type.GetType("System.Byte[]");
                        column.AllowDBNull = true;
                        column.Caption = "Image";
                        dtblTemp.Columns.Add(column);

                        foreach (DataRow dr in dtblTemp.Rows)
                        {
                            dr["Image"] = strip;
                        }

                        grdMx.ItemsSource = dtblTemp;
                        grdMx.Visibility = Visibility.Visible;
                        pnlNoMixMatch.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        grdMx.Visibility = Visibility.Collapsed;
                        pnlNoMixMatch.Visibility = Visibility.Visible;
                    }



                    blSalePrice = true;
                }
            }
        }


        private void FetchSalesData()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
            dtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));

            double dblQty = 0;
            double dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Today");
            dblRevenue = GetSalesRevenue(intID, "Today");

            dtbl.Rows.Add(new object[] { Properties.Resources.Today, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Last 7 Days");
            dblRevenue = GetSalesRevenue(intID, "Last 7 Days");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Last 14 Days");
            dblRevenue = GetSalesRevenue(intID, "Last 14 Days");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 1 Month");
            dblRevenue = GetSalesRevenue(intID, "Last 1 Month");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 3 Months");
            dblRevenue = GetSalesRevenue(intID, "Last 3 Months");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 6 Months");
            dblRevenue = GetSalesRevenue(intID, "Last 6 Months");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty, dblRevenue });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 1 Year");
            dblRevenue = GetSalesRevenue(intID, "Last 1 Year");

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty, dblRevenue });

            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "To Date");
            dblRevenue = GetSalesRevenue(intID, "To Date");

            dtbl.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty, dblRevenue });

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

            grdSales.ItemsSource = dtblTemp;
            dtbl.Dispose();

            dtblTemp.Dispose();

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Qty", System.Type.GetType("System.Double"));
            dtbl1.Columns.Add("Revenue", System.Type.GetType("System.Double"));

            double dblQty1 = 0;
            double dblRevenue1 = 0;

            dblQty1 = GetRentQty(intID, "Today");
            dblRevenue1 = GetRentRevenue(intID, "Today");

            dtbl1.Rows.Add(new object[] { Properties.Resources.Today, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 7 Days");
            dblQty1 = GetRentQty(intID, "Last 7 Days");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 14 Days");
            dblQty1 = GetRentQty(intID, "Last 14 Days");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 1 Month");
            dblQty1 = GetRentQty(intID, "Last 1 Month");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 3 Months");
            dblQty1 = GetRentQty(intID, "Last 3 Months");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 6 Months");
            dblQty1 = GetRentQty(intID, "Last 6 Months");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty1, dblRevenue1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "Last 1 Year");
            dblQty1 = GetRentQty(intID, "Last 1 Year");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty1, dblRevenue1 });

            dblQty1 = 0;
            dblRevenue1 = 0;
            dblRevenue1 = GetRentRevenue(intID, "To Date");
            dblQty1 = GetRentQty(intID, "To Date");
            dtbl1.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty1, dblRevenue1 });


            DataTable dtblTemp1 = dtbl1.DefaultView.ToTable();
            DataColumn column1 = new DataColumn("Image");
            column1.DataType = System.Type.GetType("System.Byte[]");
            column1.AllowDBNull = true;
            column1.Caption = "Image";
            dtblTemp1.Columns.Add(column1);

            foreach (DataRow dr in dtblTemp1.Rows)
            {
                dr["Image"] = strip;
            }

            grdrent.ItemsSource = dtblTemp1;
            dtbl1.Dispose();
            dtblTemp1.Dispose();

            DataTable dtbl2 = new DataTable();
            dtbl2.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("Qty", System.Type.GetType("System.Double"));
            dtbl2.Columns.Add("Revenue", System.Type.GetType("System.Double"));
            double dblQty2 = 0;
            double dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Today");
            dblQty2 = GetRepairQty(intID, "Today");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Today, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 7 Days");
            dblQty2 = GetRepairQty(intID, "Last 7 Days");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 14 Days");
            dblQty2 = GetRepairQty(intID, "Last 14 Days");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 1 Month");
            dblQty2 = GetRepairQty(intID, "Last 1 Month");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 3 Months");
            dblQty2 = GetRepairQty(intID, "Last 3 Months");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 6 Months");
            dblQty2 = GetRepairQty(intID, "Last 6 Months");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty2, dblRevenue2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "Last 1 Year");
            dblQty2 = GetRepairQty(intID, "Last 1 Year");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty2, dblRevenue2 });

            dblQty2 = 0;
            dblRevenue2 = 0;
            dblRevenue2 = GetRepairRevenue(intID, "To Date");
            dblQty2 = GetRepairQty(intID, "To Date");
            dtbl2.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty2, dblRevenue2 });


            DataTable dtblTemp2 = dtbl2.DefaultView.ToTable();
            DataColumn column2 = new DataColumn("Image");
            column2.DataType = System.Type.GetType("System.Byte[]");
            column2.AllowDBNull = true;
            column2.Caption = "Image";
            dtblTemp2.Columns.Add(column2);

            foreach (DataRow dr in dtblTemp2.Rows)
            {
                dr["Image"] = strip;
            }

            grdrepair.ItemsSource = dtblTemp2;
            dtbl2.Dispose();
            dtblTemp2.Dispose();
        }

        private double GetSalesQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductSalesRecord(pID, cat, DateTime.Today);
        }

        private double GetSalesRevenue(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductSalesRevenueRecord(pID, cat, DateTime.Today);
        }

        private double GetRentQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRentRecord(pID, cat, DateTime.Today);
        }

        private double GetRentRevenue(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRentRevenueRecord(pID, cat, DateTime.Today);
        }

        private double GetRepairQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRepairRecord(pID, cat, DateTime.Today);
        }

        private double GetRepairRevenue(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRepairRevenueRecord(pID, cat, DateTime.Today);
        }

        private void FetchPurchaseData(int fProduct, DateTime fFrmDate, DateTime fToDate)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchPurchaseData(fProduct, fFrmDate, fToDate);
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



            grdRecv.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

        }

        private void FDate_EditValueChanged(object sender, RoutedEventArgs e)
        {
            if ((FDate.EditValue != null) && (TDate.EditValue != null))
                FetchPurchaseData(intID, GeneralFunctions.fnDate(FDate.EditValue.ToString()), GeneralFunctions.fnDate(TDate.EditValue.ToString()));
        }

        private void TpOther_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TpVendor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
            if (cmbProductType.SelectedIndex == 6) colShrink.Visible = true; else colShrink.Visible = false;
        }

        private void TpRental_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 4;
        }

        private void TpSale_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 5;
        }

        private void TpLinkedWith_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (intID == 0)
            {
                tcProduct.SelectedIndex = OldTabIndex;
                return;
            }
            else
            {
                OldTabIndex = 7;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {



            if (IsValidAll())
            {

                if (SaveData())
                {
                    boolControlChanged = false;
                    boolPostData = true;
                    /*if (!blAddFromPOS)
                    {
                        if (!blCallFromLookup)
                            BrowseFormUC.FetchData(BrowseFormUC.IsPOS, BrowseFormUC.cmbFilter.EditValue.ToString());
                    }*/
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
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
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {

                        if (SaveData())
                        {
                            boolControlChanged = false;
                            boolPostData = true;
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtSKU_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private async Task SetCurrentRowFP(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;

            for (intColCtr = 0; intColCtr < (grd.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grd, colFPID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView14.FocusedRowHandle = intColCtr;
        }



        private void Lbsp1_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_SalePriceDlg frm_Dlg = new frm_SalePriceDlg();
            try
            {
                frm_Dlg.ID = GeneralFunctions.fnInt32(lbsp1.Tag);
                if (frm_Dlg.ID > 0)
                {
                    frm_Dlg.CallFromProduct = true;
                    frm_Dlg.ShowHeader();
                    frm_Dlg.ShowDialog();
                }
            }
            finally
            {
                frm_Dlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }


            int salebid = 0;
            string saleapply = "";
            string salebatch = "";
            string saleperiod = "";
            double saleprice = 0;

            PosDataObject.Product op1 = new PosDataObject.Product();
            op1.Connection = SystemVariables.Conn;
            op1.ActiveSale(intID, ref salebid, ref salebatch, ref saleapply, ref saleperiod, ref saleprice, SystemVariables.DateFormat);

            if (salebid > 0)
            {
                lnksb.Text = salebatch;
                lbsp1.Tag = salebid;
                lbsp2.Text = saleapply;
                lbsp3.Text = GeneralFunctions.FormatDouble1(saleprice);
                lbsp4.Text = saleperiod;
                pnlSalePrice.Visibility = Visibility.Visible;
                pnlNoSaleBatch.Visibility = Visibility.Collapsed;
            }
            else
            {
                pnlSalePrice.Visibility = Visibility.Collapsed;
                pnlNoSaleBatch.Visibility = Visibility.Visible;
            }
        }


        private void GridView14_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private async void Part_Editor_FLink_RequestNavigation(object sender, HyperlinkEditRequestNavigationEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            int tID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView14.FocusedRowHandle, grd, colFPID));
            frm_FuturePriceDlg frm_Dlg = new frm_FuturePriceDlg();
            try
            {
                frm_Dlg.ID = tID;
                if (frm_Dlg.ID > 0)
                {
                    frm_Dlg.setflag = false;
                    frm_Dlg.ShowHeader();
                    frm_Dlg.CallFromProduct = true;
                    frm_Dlg.ShowDialog();
                }
            }
            finally
            {
                frm_Dlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            PosDataObject.Product op2 = new PosDataObject.Product();
            op2.Connection = SystemVariables.Conn;
            DataTable dtbl = op2.ActiveFuturePrice(intID, SystemVariables.DateFormat);



            if (dtbl.Rows.Count > 0)
            {
                grd.ItemsSource = dtbl;
                await SetCurrentRowFP(tID);
                grd.Visibility = Visibility.Visible;
                pnlNoFutureBatch.Visibility = Visibility.Collapsed;
            }
            else
            {
                grd.Visibility = Visibility.Collapsed;
                pnlNoFutureBatch.Visibility = Visibility.Visible;
            }
        }

        private void CmbDept_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbCategory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbBrand_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void GrdDept_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdDept.CurrentColumn.Name == "colVendor")
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

        private void PART_Editor_VendorID_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void GrdPrinter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdPrinter.CurrentColumn.Name == "colPrinterID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdPrinter.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void PART_Editor_Printer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void LnkType_RequestNavigation(object sender, HyperlinkEditRequestNavigationEventArgs e)
        {
            if (cmbProductType.SelectedIndex == 0)
            {
                cmbProductType.SelectedIndex = 6;
                boolControlChanged = true;
            }
            else
            {
                cmbProductType.SelectedIndex = 0;
                boolControlChanged = true;

            }
        }

        private async void Part_Editor_MixLink_RequestNavigation(object sender, HyperlinkEditRequestNavigationEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            int tID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView16.FocusedRowHandle, grdMx, colMxID));

            frmMix_n_MatchDlg frm_DiscountDlg = new frmMix_n_MatchDlg();
            try
            {
                frm_DiscountDlg.ID = tID;
                frm_DiscountDlg.CallFromProduct = true;
                if (frm_DiscountDlg.ID > 0)
                {
                    frm_DiscountDlg.ViewMode = false;
                    frm_DiscountDlg.ShowDialog();
                }
            }
            finally
            {
                frm_DiscountDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }

            PosDataObject.Product op2 = new PosDataObject.Product();
            op2.Connection = SystemVariables.Conn;
            DataTable dtblmx = op2.ActiveMixMatch(intID, SystemVariables.DateFormat);

            if (dtblmx.Rows.Count > 0)
            {
                grdMx.ItemsSource = dtblmx;
                grdMx.Visibility = Visibility.Visible;
                pnlNoMixMatch.Visibility = Visibility.Collapsed;

            }
            else
            {
                grdMx.Visibility = Visibility.Collapsed;
                pnlNoMixMatch.Visibility = Visibility.Visible;
            }
        }

        private void CmbProductType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtColor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.PopupColorEdit editor = sender as DevExpress.Xpf.Editors.PopupColorEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtFontType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.FontEdit editor = sender as DevExpress.Xpf.Editors.FontEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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

        private void Lbdelaultcolor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtColor.Clear();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtFontColor.Clear();
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
            if (!blAddFromPOS)
            {
                if (Settings.UseTouchKeyboardInAdmin == "N") return;
            }

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
            if (!blAddFromPOS)
            {
                if (Settings.UseTouchKeyboardInAdmin == "N") return;
            }
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

        private void ColPartNo_GotFocus(object sender, RoutedEventArgs e)
        {
            object o = sender;
        }

        private void PART_EDITOR_Part_GotFocus(object sender, RoutedEventArgs e)
        {
            object o = sender;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int VendorID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdDept, colID));
            if (VendorID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView2.DeleteRow(gridView2.FocusedRowHandle);
                    boolControlChanged = true;
                }
            }
        }

        private void GridView2_FocusedColumnChanged(object sender, DevExpress.Xpf.Grid.FocusedColumnChangedEventArgs e)
        {


        }

        private void btnDAdd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssDepartment) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_DepartmentDlg frm_DepartmentDlg = new frm_DepartmentDlg();
            try
            {
                frm_DepartmentDlg.OnScreenCall = true;
                frm_DepartmentDlg.ID = 0;
                frm_DepartmentDlg.ShowDialog();
                intNewRecID = frm_DepartmentDlg.NewID;
            }
            finally
            {
                frm_DepartmentDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                DeptLookup();
                cmbDept.EditValue = intNewRecID.ToString();
            }
        }

        private void btnCAdd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssCategory) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intNewRecID = 0;
            blurGrid.Visibility = Visibility.Visible;
            frm_CategoryDlg frm_CategoryDlg = new frm_CategoryDlg();
            try
            {
                frm_CategoryDlg.OnScreenCall = true;
                frm_CategoryDlg.ID = 0;
                frm_CategoryDlg.ShowDialog();
                intNewRecID = frm_CategoryDlg.NewID;
            }
            finally
            {
                frm_CategoryDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0)
            {
                PopulateCategory();
                cmbCategory.EditValue = intNewRecID.ToString();
            }
        }

        private void GridView2_ShownEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e)
        {
            if (e.Column.FieldName == "PartNumber")
            {
                TextEdit editor = (TextEdit)e.Editor;
                editor.Focusable = true;

                if (!blAddFromPOS)
                {
                    if (Settings.UseTouchKeyboardInAdmin == "N") return;
                }
                CloseKeyboards();

                if (!IsAboutFullKybrdOpen)
                {

                    fkybrd = new FullKeyboard();
                    
                    fkybrd.CallFromGrid = true;
                    fkybrd.GridInputControl = editor;
                    fkybrd.WindowName = "Product Vendor";
                    fkybrd.GridColumnName = "PartNumber";
                    fkybrd.GridRowIndex = gridView2.FocusedRowHandle;
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
            if (e.Column.FieldName == "Price")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (!blAddFromPOS)
                {
                    if (Settings.UseTouchKeyboardInAdmin == "N") return;
                }

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
                    nkybrd.WindowName = "Product Vendor";
                    nkybrd.GridColumnName = "Price";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView2.FocusedRowHandle;
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

            if (e.Column.FieldName == "Shrink")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (!blAddFromPOS)
                {
                    if (Settings.UseTouchKeyboardInAdmin == "N") return;
                }

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
                    nkybrd.WindowName = "Product Vendor";
                    nkybrd.GridColumnName = "Shrink";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView2.FocusedRowHandle;
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

            if (e.Column.FieldName == "PackQty")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (!blAddFromPOS)
                {
                    if (Settings.UseTouchKeyboardInAdmin == "N") return;
                }

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Product Vendor";
                    nkybrd.GridColumnName = "PackQty";
                    nkybrd.GridDecimal = 0;
                    nkybrd.GridRowIndex = gridView2.FocusedRowHandle;
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
            if (strgridname == "Product Vendor")
            {
                if (gridcol == "PartNumber") grdDept.SetCellValue(rindx, colPartNo, val);
                if (gridcol == "Price") grdDept.SetCellValue(rindx, colCost, val);
                if (gridcol == "Shrink") grdDept.SetCellValue(rindx, colShrink, val);
                if (gridcol == "PackQty") grdDept.SetCellValue(rindx, colPackQty, val);
            }

            if (strgridname == "Product Printer")
            {
                if (gridcol == "CutOffValue") grdPrinter.SetCellValue(rindx, colCutOffValue, val);
            }
        }

        private void GridView20_ShownEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e)
        {
            if (e.Column.FieldName == "CutOffValue")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (!blAddFromPOS)
                {
                    if (Settings.UseTouchKeyboardInAdmin == "N") return;
                }

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Product Printer";
                    nkybrd.GridColumnName = "CutOffValue";
                    nkybrd.GridDecimal = 0;
                    nkybrd.GridRowIndex = gridView20.FocusedRowHandle;
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

        private void CmbCategory_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }

        private void TxtFontType_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }

        private void DtExpiry_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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