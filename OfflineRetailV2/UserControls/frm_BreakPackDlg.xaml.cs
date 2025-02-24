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
using Microsoft.Win32;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_BreakPackDlg.xaml
    /// </summary>
    public partial class frm_BreakPackDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_BreakPackDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool boolControlChanged;

        private frm_BreakPackBrwUC frmBrowse;
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
        private bool blSetStyle = false;
        private bool blSetStyleS = false;
        private bool blVendor = false;

        private bool blDuplicate;
        private bool blAddFromPOS;
        public bool blCallFromLookup = false;
        private string strAddSKU;
        private int intAddCategory;
        private string strAddDisplayItemInPOS;
        private string strAddDisplayItemInPOSCat;
        private DataTable dtblDelete;
        private int ProductDecialPlace = 2;

        private double dblRatio = 0;
        private double dblDisC = 0;
        private double dblCurC = 0;
        private double dblLastC = 0;
        private double dblPrA = 0;
        private double dblPrB = 0;
        private double dblPrC = 0;

        private bool blfocus = false;

        private double prevOnHand = 0;

        private double pckqty = 0;
        private double prevratio = 0;

        private double ratiobeforechange = 0;

        private int OldTabIndex = 0;                                              

        private int bpk_Department = 0;
        private int bpk_Category = 0;
        private int bpk_Brand = 0;
        private int bpk_ProductType = 0;

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


        public frm_BreakPackBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_BreakPackBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateProduct(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchLookupData2(intOption);

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

            cmbLinkSKU.ItemsSource = dtblTemp;
            dbtblSKU.Dispose();
        }

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

        }

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

                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { "0", "0", "" });
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

        private void PopulateBatchGrid()
        {
            

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
                gridView3.MoveFirstRow();
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

                grdrenttax.ItemsSource = dtblF_Tx;
                FillRowinRentTaxGrid(((grdrenttax.ItemsSource) as DataTable).Rows.Count);
                gridView9.MoveFirstRow();
            }
            finally
            {

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
                numRatio.Mask = "f3";
            }
            else
            {
                numDcost.Mask = "f2";
                numCcost.Mask = "f2";
                numLPcost.Mask = "f2";
                numPriceA.Mask = "f2";
                numPriceB.Mask = "f2";
                numPriceC.Mask = "f2";
                numRatio.Mask = "f2";
            }
        }



        private async void LbLinkSKU_RequestNavigation(object sender, DevExpress.Xpf.Editors.HyperlinkEditRequestNavigationEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            Report.frm_Lookups frm_Lookups = new Report.frm_Lookups();
            try
            {
                frm_Lookups.Print = "N";
                frm_Lookups.SearchType = "Product (For Break Pack)";
                frm_Lookups.ShowDialog();
                if (await frm_Lookups.GetID() > 0)
                {
                    cmbLinkSKU.EditValue = (await frm_Lookups.GetID()).ToString();
                }
            }
            finally
            {
                frm_Lookups.Close();
                blurGrid.Visibility = Visibility.Collapsed;
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
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
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

        private void PART_Editor_RentTax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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
            
            if ((!SecurityPermission.AcssProductCost) && (SystemVariables.CurrentUserID > 0))
            {
                numDcost.IsReadOnly = true;
                numCcost.IsReadOnly = true;
                numLPcost.IsReadOnly = true;
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
            PopulateTax();
            PopulateProduct(0);
            
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Break_Pack;
                
                txtSKU.IsEnabled = true;
                chkZeroStock.IsChecked = true;
                
                if (blAddFromPOS)
                {
                    txtSKU.Text = strAddSKU;
                    txtSKU.IsEnabled = false;
                }
                SetDecimalPlace();
                numRatio.Text = "1.00";
            }
            else
            {
                if (!blDuplicate)
                {
                    Title.Text = Properties.Resources.Edit_Break_Pack;
                    txtSKU.IsEnabled = false;
                    lbLinkSKU.IsEnabled = false;
                    cmbLinkSKU.IsEnabled = false;
                }
                else
                {
                    Title.Text = Properties.Resources.New_Break_Pack;
                    txtSKU.IsEnabled = true;
                    SetDecimalPlace();
                    numRatio.Text = "1.00";
                }

                ShowData();
            }
            tcProduct.SelectedIndex = 0;
            GeneralFunctions.SetFocus(txtSKU);
            IsUseStyle();

            PermissionSettings();
            
            PopulateBatchGrid();

            if (blDuplicate)
            {
                intID = 0;
                txtSKU.Text = "";
            }

            //SetItemOnHand();

            //prevOnHand = numOnHand.Value;

            boolControlChanged = false;
        }

        public void ShowData()
        {
            string strColor = "";
            string strFontColor = "";

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtSKU.Text = dr["SKU"].ToString();
                txtDesc.Text = dr["Description"].ToString();
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

                
                cmbLinkSKU.EditValue = dr["LinkSKU"].ToString();
                bpk_Brand = GeneralFunctions.fnInt32(dr["BrandID"].ToString());
                bpk_Department = GeneralFunctions.fnInt32(dr["DepartmentID"].ToString());
                bpk_Category = GeneralFunctions.fnInt32(dr["CategoryID"].ToString());
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

                if (dr["DecimalPlace"].ToString() == "3")
                {
                    ProductDecialPlace = 3;
                }
                else
                {
                    ProductDecialPlace = 2;
                }
                SetDecimalPlace();

                numRatio.Text = GeneralFunctions.fnDouble(dr["BreakPackRatio"].ToString()).ToString();
                prevratio = GeneralFunctions.fnDouble(numRatio.Text);
                numDcost.Text = GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString()).ToString();
                numCcost.Text = GeneralFunctions.fnDouble(dr["Cost"].ToString()).ToString();
                numLPcost.Text = GeneralFunctions.fnDouble(dr["LastCost"].ToString()).ToString();

                numPriceA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numPriceB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numPriceC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();



                numRentalMinuteAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerMinute"].ToString()).ToString("f2");
                numRentalHourAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerHour"].ToString()).ToString("f2");
                numRentalHalfDayAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerHalfDay"].ToString()).ToString("f2");
                numRentalDayAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerDay"].ToString()).ToString("f2");
                numRentalWeekAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerWeek"].ToString()).ToString("f2");
                numRentalMonthAmt.Text = GeneralFunctions.fnDouble(dr["RentalPerMonth"].ToString()).ToString("f2");
                numRentalDeposit.Text = GeneralFunctions.fnDouble(dr["RentalDeposit"].ToString()).ToString("f2");
                numRentalMinHour.Text = GeneralFunctions.fnDouble(dr["RentalMinHour"].ToString()).ToString("f2");
                numRentalMinAmt.Text = GeneralFunctions.fnDouble(dr["RentalMinAmount"].ToString()).ToString("f2");
                if (dr["RentalPrompt"].ToString() == "Y")
                {
                    chkRentPrompt.IsChecked = true;
                }
                else
                {
                    chkRentPrompt.IsChecked = false;
                }


                if (dr["RepairPromptForTag"].ToString() == "Y") chkRepairPromptTag.IsChecked = true;
                else chkRepairPromptTag.IsChecked = false;

                dblDisC = GeneralFunctions.fnDouble(numDcost.Text);
                dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
                dblLastC = GeneralFunctions.fnDouble(numLPcost.Text);
                dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
                dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
                dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);

                txtPoints.Text = GeneralFunctions.fnDouble(dr["Points"].ToString()).ToString("f");


                txtNotes.Text = dr["ProductNotes"].ToString();

                pckqty = GeneralFunctions.fnDouble(dr["CaseQty"].ToString());
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

                if (dr["ProductType"].ToString() == "P")
                {
                    bpk_ProductType = 0;
                }
                else if (dr["ProductType"].ToString() == "U")
                {
                    bpk_ProductType = 1;
                }
                else if (dr["ProductType"].ToString() == "M")
                {
                    bpk_ProductType = 2;
                }
                else if (dr["ProductType"].ToString() == "E")
                {
                    bpk_ProductType = 3;
                }
                else if (dr["ProductType"].ToString() == "K")
                {
                    bpk_ProductType = 4;
                }
                else if (dr["ProductType"].ToString() == "F")
                {
                    bpk_ProductType = 5;
                }
                else if (dr["ProductType"].ToString() == "W")
                {
                    bpk_ProductType = 6;
                }
                else
                {
                    bpk_ProductType = 7;
                }

                if (!blDuplicate)
                {
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
                
            }
        }

        private void CmbLinkSKU_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        public void FetchLinkSKUDataForPrice(bool blv)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowRecord(GeneralFunctions.fnInt32(cmbLinkSKU.EditValue));
            foreach (DataRow dr in dbtbl.Rows)
            {
                pckqty = GeneralFunctions.fnDouble(dr["CaseQty"].ToString());

                if (blv)
                {
                    numDcost.Text = (GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numCcost.Text = (GeneralFunctions.fnDouble(dr["Cost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numLPcost.Text = (GeneralFunctions.fnDouble(dr["LastCost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();

                    numPriceA.Text = (GeneralFunctions.fnDouble(dr["PriceA"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numPriceB.Text = (GeneralFunctions.fnDouble(dr["PriceB"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numPriceC.Text = (GeneralFunctions.fnDouble(dr["PriceC"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();

                    txtPoints.Text = (Math.Floor(GeneralFunctions.fnDouble(dr["Points"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text))).ToString();

                    dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
                    dblLastC = GeneralFunctions.fnDouble(numLPcost.Text);
                    dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
                    dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
                    dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);
                    dblRatio = GeneralFunctions.fnDouble(numRatio.Text);
                }
            }
            dbtbl.Dispose();

            /*if (blv)
            {
                numCcost.Value = numCcost.Value / GeneralFunctions.fnDouble(numRatio.Value);
                numLPcost.Value = numLPcost.Value / GeneralFunctions.fnDouble(numRatio.Value);

                numPriceA.Value = numPriceA.Value / GeneralFunctions.fnDouble(numRatio.Value);
                numPriceB.Value = numPriceB.Value / GeneralFunctions.fnDouble(numRatio.Value);
                numPriceC.Value = numPriceC.Value / GeneralFunctions.fnDouble(numRatio.Value);

                txtPoints.Value = Math.Ceiling(txtPoints.Value) / GeneralFunctions.fnDouble(numRatio.Value);

                dblCurC = numCcost.Value;
                dblLastC = numLPcost.Value;
                dblPrA = numPriceA.Value;
                dblPrB = numPriceB.Value;
                dblPrC = numPriceC.Value;
                dblRatio = numRatio.Value;
            }*/
        }
        public void FetchLinkSKUData(int pID, bool blval)
        {
            string strColor = "";
            string strFontColor = "";

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objProduct.ShowRecord(pID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                bpk_Brand = GeneralFunctions.fnInt32(dr["BrandID"].ToString());
                bpk_Department = GeneralFunctions.fnInt32(dr["DepartmentID"].ToString());
                bpk_Category = GeneralFunctions.fnInt32(dr["CategoryID"].ToString());
                pckqty = GeneralFunctions.fnDouble(dr["CaseQty"].ToString());

                if (dr["ProductType"].ToString() == "P")
                {
                    bpk_ProductType = 0;
                }
                else if (dr["ProductType"].ToString() == "S")
                {
                    bpk_ProductType = 1;
                }
                else if (dr["ProductType"].ToString() == "U")
                {
                    bpk_ProductType = 2;
                }
                else if (dr["ProductType"].ToString() == "M")
                {
                    bpk_ProductType = 3;
                }
                else if (dr["ProductType"].ToString() == "E")
                {
                    bpk_ProductType = 4;
                }
                else if (dr["ProductType"].ToString() == "K")
                {
                    bpk_ProductType = 5;
                }
                else if (dr["ProductType"].ToString() == "F")
                {
                    bpk_ProductType = 6;
                }
                else if (dr["ProductType"].ToString() == "W")
                {
                    bpk_ProductType = 7;
                }
                else
                {
                    bpk_ProductType = 8;
                }



                if (blval)
                {
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

                    

                    if (dr["FoodStampEligible"].ToString() == "Y")
                    {
                        chkFoodStamp.IsChecked = true;
                    }
                    else
                    {
                        chkFoodStamp.IsChecked = false;
                    }
                    txtBinLocation.Text = dr["BinLocation"].ToString();

                    if (dr["DecimalPlace"].ToString() == "3")
                    {
                        ProductDecialPlace = 3;
                    }
                    else
                    {
                        ProductDecialPlace = 2;
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

                    txtSeason.Text = dr["Season"].ToString();

                    numDcost.Text = (GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numCcost.Text = (GeneralFunctions.fnDouble(dr["Cost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numLPcost.Text = (GeneralFunctions.fnDouble(dr["LastCost"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();

                    numPriceA.Text = (GeneralFunctions.fnDouble(dr["PriceA"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numPriceB.Text = (GeneralFunctions.fnDouble(dr["PriceB"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();
                    numPriceC.Text = (GeneralFunctions.fnDouble(dr["PriceC"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text)).ToString();

                    txtPoints.Text = (Math.Floor(GeneralFunctions.fnDouble(dr["Points"].ToString()) / GeneralFunctions.fnDouble(numRatio.Text))).ToString();

                    dblDisC = GeneralFunctions.fnDouble(numDcost.Text);
                    dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
                    dblLastC = GeneralFunctions.fnDouble(numLPcost.Text);
                    dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
                    dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
                    dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);
                    dblRatio = GeneralFunctions.fnDouble(numRatio.Text);


                    numMinAge.Text = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString()).ToString();
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
            }

            if (blval)
            {

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
                    dtblTx = objTaxProduct.ShowTaxes(GeneralFunctions.fnInt32(cmbLinkSKU.EditValue));

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
                    gridView3.MoveFirstRow();
                }
                finally
                {
                }

                

            }
        }

        private void NumRatio_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbLinkSKU.EditValue != null)
            {
                if (numRatio.EditValue != null)
                {
                    if (numRatio.EditValue.ToString() != "")
                    {
                        if (GeneralFunctions.fnDouble(numRatio.EditValue.ToString()) != 0)
                        {
                            FetchLinkSKUDataForPrice(true);
                        }
                    }
                }
            }

            boolControlChanged = true;
        }

        private void TcProduct_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int i = tcProduct.SelectedIndex;

            if (i == 2)
            {
                if (!blSales)
                {
                    FetchSalesData();

                    blSales = true;
                }
            }

            if (i == 1)
            {
                if ((intID == 0) && (!blDuplicate))
                {
                    if (!blVendor)
                    {
                        if (bpk_Category == 0)
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
            }
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
            dbtbl = objCategory.ShowRecord(bpk_Category);
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

        private void FetchSalesData()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
            
            double dblQty = 0;
            double dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Today");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Today, dblQty });
            dblQty = 0;
            dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Last 7 Days");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty });
            dblQty = 0;
            dblRevenue = 0;

            dblQty = GetSalesQty(intID, "Last 14 Days");
            
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 1 Month");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 3 Months");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 6 Months");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty });
            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "Last 1 Year");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty });

            dblQty = 0;
            dblRevenue = 0;
            dblQty = GetSalesQty(intID, "To Date");
            

            dtbl.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty });

            grdSales.ItemsSource = dtbl;
            dtbl.Dispose();

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Qty", System.Type.GetType("System.Double"));


            double dblQty1 = 0;
            double dblRevenue1 = 0;

            dblQty1 = GetRentQty(intID, "Today");
            

            dtbl1.Rows.Add(new object[] { Properties.Resources.Today, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 7 Days");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 14 Days");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 1 Month");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 3 Months");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 6 Months");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty1 });
            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "Last 1 Year");
            dtbl1.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty1 });

            dblQty1 = 0;
            dblRevenue1 = 0;
            
            dblQty1 = GetRentQty(intID, "To Date");
            dtbl1.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty1 });

            grdrent.ItemsSource = dtbl1;
            dtbl1.Dispose();

            DataTable dtbl2 = new DataTable();
            dtbl2.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl2.Columns.Add("Qty", System.Type.GetType("System.Double"));

            double dblQty2 = 0;
            double dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Today");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Today, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 7 Days");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 14 Days");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 1 Month");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 3 Months");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 6 Months");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty2 });
            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "Last 1 Year");
            dtbl2.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty2 });

            dblQty2 = 0;
            dblRevenue2 = 0;
            
            dblQty2 = GetRepairQty(intID, "To Date");
            dtbl2.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty2 });

            grdrepair.ItemsSource = dtbl2;
            dtbl2.Dispose();
        }

        private double GetSalesQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductSalesRecord(pID, cat, DateTime.Today);
        }

        

        private double GetRentQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRentRecord(pID, cat, DateTime.Today);
        }

        

        private double GetRepairQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductRepairRecord(pID, cat, DateTime.Today);
        }

        private void TpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TabItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TpLinkedWith_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void TabItem_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 3;
        }

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateCount(txtSKU.Text.Trim());
        }

        private int DuplicateBPCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateBreakpack(GeneralFunctions.fnInt32(cmbLinkSKU.EditValue), GeneralFunctions.fnDouble(numRatio.Text));
        }

        private bool IsValidAll()
        {
            
            if (txtSKU.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.UPC);
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
                        DocMessage.MsgInformation(Properties.Resources.Duplicate_UPC);
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

            /*if (bpk_Department == 0)
            {
                DocMessage.MsgEnter(Properties.Resources.Department);
                tcProduct.SelectedIndex = 0;
                return false;
            }

            if (bpk_Category == 0)
            {
                DocMessage.MsgEnter("Category");
                tcProduct.SelectedIndex = 0;
                return false;
            }*/

            if (cmbLinkSKU.EditText == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Link_SKU);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(cmbLinkSKU);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbLinkSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if (chkPOSScreen.IsChecked == true)
            {
                PosDataObject.Product objProd = new PosDataObject.Product();
                objProd.Connection = SystemVariables.Conn;
                int intProd = objProd.GetPOSProductsforCategory(bpk_Category);
                int intAllow = objProd.GetMaxPOSforCategory(bpk_Category);

                if (intProd >= intAllow)
                {
                    DocMessage.MsgInformation(Properties.Resources.Cannot_add_more_breakpacks_in_POS_Screen_for_this_Category + " " + Properties.Resources.Please_increase_this_count_in_Category);
                    tcProduct.SelectedIndex = 0;
                    return false;
                }
            }

            

            if (GeneralFunctions.fnDouble(numRatio.Text) == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Invalid_Ratio);
                tcProduct.SelectedIndex = 0;
                if (OldTabIndex == 0)
                {
                    GeneralFunctions.SetFocus(numRatio);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(numRatio); }), System.Windows.Threading.DispatcherPriority.Render);
                }
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (prevratio != GeneralFunctions.fnDouble(numRatio.Text))))
            {
                if (DuplicateBPCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Break_Pack_Ratio_Link_SKU);
                    tcProduct.SelectedIndex = 0;
                    if (OldTabIndex == 0)
                    {
                        GeneralFunctions.SetFocus(cmbLinkSKU);
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => { GeneralFunctions.SetFocus(cmbLinkSKU); }), System.Windows.Threading.DispatcherPriority.Render);
                    }
                    return false;
                }
            }

            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;
            objProduct.UPC = "";
            objProduct.SKU = txtSKU.Text.Trim();
            objProduct.SKU2 = "";
            objProduct.SKU3 = "";
            objProduct.Description = txtDesc.Text.Trim();
            objProduct.BinLocation = txtBinLocation.Text.Trim();
            objProduct.PrimaryVendorID = "";
            if (bpk_Brand != 0)
            {
                objProduct.BrandID = bpk_Brand;
            }
            else
            {
                objProduct.BrandID = 0;
            }

            if (bpk_Department != 0)
            {
                objProduct.DepartmentID = bpk_Department;
            }
            else
            {
                objProduct.DepartmentID = 0;
            }
            if (bpk_Category != 0)
            {
                objProduct.CategoryID = bpk_Category;
            }
            else
            {
                objProduct.CategoryID = 0;
            }

            if (cmbLinkSKU.EditValue.ToString() != "")
            {
                objProduct.LinkSKU = GeneralFunctions.fnInt32(cmbLinkSKU.EditValue.ToString());
            }
            else
            {
                objProduct.LinkSKU = 0;
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

            objProduct.DisplayStockinPOS = "N";

            if (chkActive.IsChecked == true)
            {
                objProduct.ProductStatus = "Y";
            }
            else
            {
                objProduct.ProductStatus = "N";
            }

            objProduct.TaggedInInvoice = "N";

            objProduct.AddtoScaleScreen = "N";
            objProduct.Notes2 = "";

            objProduct.BreakPackRatio = GeneralFunctions.fnDouble(numRatio.Text.Trim());

            objProduct.DiscountedCost = GeneralFunctions.fnDouble(numDcost.Text.Trim());
            objProduct.Cost = GeneralFunctions.fnDouble(numCcost.Text.Trim());
            objProduct.LastCost = GeneralFunctions.fnDouble(numLPcost.Text.Trim());
            objProduct.PriceA = GeneralFunctions.fnDouble(numPriceA.Text.Trim());
            objProduct.PriceB = GeneralFunctions.fnDouble(numPriceB.Text.Trim());
            objProduct.PriceC = GeneralFunctions.fnDouble(numPriceC.Text.Trim());
            objProduct.QtyOnHand = 0;
            objProduct.ReorderQty = 0;
            objProduct.NormalQty = 0;
            objProduct.QtyOnLayaway = 0;
            objProduct.MinimumAge = GeneralFunctions.fnInt32(numMinAge.Text.Trim());
            objProduct.QtyToPrint = GeneralFunctions.fnInt32(numLabel.Text.Trim());
            objProduct.ProductNotes = txtNotes.Text.Trim();
            objProduct.Notes2 = "";
            objProduct.Points = GeneralFunctions.fnInt32(txtPoints.Text.Trim());
            objProduct.CaseQty = GeneralFunctions.fnInt32(pckqty);
            objProduct.Season = txtSeason.Text.Trim();
            objProduct.CaseUPC = txtCaseUPC.Text.Trim();

            objProduct.RentalPerMinute = GeneralFunctions.fnDouble(numRentalMinuteAmt.Text.Trim());
            objProduct.RentalPerHour = GeneralFunctions.fnDouble(numRentalHourAmt.Text.Trim());
            objProduct.RentalPerHalfDay = GeneralFunctions.fnDouble(numRentalHalfDayAmt.Text.Trim());
            objProduct.RentalPerDay = GeneralFunctions.fnDouble(numRentalDayAmt.Text.Trim());
            objProduct.RentalPerWeek = GeneralFunctions.fnDouble(numRentalWeekAmt.Text.Trim());
            objProduct.RentalPerMonth = GeneralFunctions.fnDouble(numRentalMonthAmt.Text.Trim());
            objProduct.RentalDeposit = GeneralFunctions.fnDouble(numRentalDeposit.Text.Trim());

            if (chkRentPrompt.IsChecked == true)
            {
                objProduct.RentalPrompt = "Y";
            }
            else
            {
                objProduct.RentalPrompt = "N";
            }
            objProduct.RentalMinHour = GeneralFunctions.fnDouble(numRentalMinHour.Text.Trim());
            objProduct.RentalMinAmount = GeneralFunctions.fnDouble(numRentalMinAmt.Text.Trim());

            objProduct.RepairCharge = 0;
            objProduct.RepairPromptForCharge = "N";
            objProduct.RepairPromptForTag = "N";

            objProduct.MinimumServiceTime = 0;

            if (bpk_ProductType == 0)
            {
                objProduct.ProductType = "P";
            }
            else if (bpk_ProductType == 1)
            {
                objProduct.ProductType = "U";
            }
            else if (bpk_ProductType == 2)
            {
                objProduct.ProductType = "M";
            }
            else if (bpk_ProductType == 3)
            {
                objProduct.ProductType = "E";
            }
            else if (bpk_ProductType == 4)
            {
                objProduct.ProductType = "K";
            }
            else if (bpk_ProductType == 5)
            {
                objProduct.ProductType = "F";
            }
            else if (bpk_ProductType == 6)
            {
                objProduct.ProductType = "W";
            }
            else
            {
                objProduct.ProductType = "T";
            }

            // Style

            if ((intID == 0) && (!blVendor) && (!blDuplicate)) SetCategoryStyle();  // set style in add mode if it is not set

            objProduct.POSBackground = "Color";
            objProduct.POSScreenColor = txtColor.Color.ToString();
            objProduct.POSScreenStyle = "";

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

            gridView3.PostEditor();
            objProduct.VendorDataTable = null;
            objProduct.TaxDataTable = (grdTax.ItemsSource as DataTable);
            objProduct.RentTaxDataTable = (grdrenttax.ItemsSource as DataTable);
            objProduct.RepairTaxDataTable = null;
            objProduct.ProductModifierDataTable = null;

            objProduct.TerminalName = Settings.TerminalName;


            /*if (numOnHand.Value != prevOnHand)
            {
                objProduct.AddJournal = true;
                bool blPExists = IfExistsInJournal();
                if (numOnHand.Value > prevOnHand)
                {

                    objProduct.JournalQty = numOnHand.Value - prevOnHand;
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
                        objProduct.JournalQty = numOnHand.Value - prevOnHand;
                        objProduct.JournalSubType = "Opening Stock";
                        objProduct.JournalType = "Stock In";
                    }
                    else
                    {
                        objProduct.JournalQty = prevOnHand - numOnHand.Value;
                        objProduct.JournalSubType = "Manual Adjustment";
                        objProduct.JournalType = "Stock Out";
                    }
                }
            }
            else
            {
                objProduct.AddJournal = false;
            }*/

            objProduct.AddJournal = false;

            objProduct.ProductDecimalPlace = ProductDecialPlace;

            
            objProduct.NonDiscountable = "N";
            objProduct.Tare = 0;
            objProduct.Tare2 = 0;
            objProduct.SplitWeight = 0;
            objProduct.BookingExpFlag = "N";
            objProduct.UOM = "";
            objProduct.ShopifyImageExportFlag = "N";
            objProduct.ExpiryDate = Convert.ToDateTime(null);
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    if (!blAddFromPOS)
                    {
                        if (!blCallFromLookup)
                            BrowseForm.FetchData(BrowseForm.IsPOS, BrowseForm.cmbFilter.EditValue.ToString());
                    }
                    CloseKeyboards();
                    Close();
                }
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
                    if (IsValidAll())
                    {
                        
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.FetchData(BrowseForm.IsPOS, BrowseForm.cmbFilter.EditValue.ToString());
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

        private void TxtSKU_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbLinkSKU_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbLinkSKU_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbLinkSKU.EditValue != null)
            {
                if (intID == 0)
                {
                    bool blval = false;
                    if (DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_set_Link_SKU_data_as_default_data) == MessageBoxResult.Yes) blval = true;
                    FetchLinkSKUData(GeneralFunctions.fnInt32(cmbLinkSKU.EditValue.ToString()), blval);
                }
            }
        }

        private void CmbLabelType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void Lbdelaultcolor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtColor.Clear();
        }

        private void Label24_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void btnCancel_Click_1(object sender, RoutedEventArgs e)
        {

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
