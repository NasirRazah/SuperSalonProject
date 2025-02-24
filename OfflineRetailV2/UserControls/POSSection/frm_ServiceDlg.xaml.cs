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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_ServiceDlg.xaml
    /// </summary>
    public partial class frm_ServiceDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private int ReScan = 0;
        private string strPhotoFile = "";
        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";
        private bool boolControlChanged;

        private frm_ProductBrw frmBrowsePOS;
        private frm_ServiceBrw frmBrowse;
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
        private bool blVendor = false;

        private bool blDuplicate;
        private bool blAddFromPOS;
        public bool blCallFromLookup = false;
        private string strAddSKU;
        private int intAddCategory;
        private string strAddDisplayItemInPOS;
        private DataTable dtblDelete;
        private int ProductDecialPlace = 2;

        private double dblCurC = 0;
        private double dblLastC = 0;
        private double dblPrA = 0;
        private double dblPrB = 0;
        private double dblPrC = 0;

        private bool blfocus = false;

        private double prevOnHand = 0;

        private int linksku = 0;

        private bool boolBookingExportFlagChanged;
        private string prevBookingExpFlag = "N";

        private bool boolloading = false;


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

        public frm_ProductBrw BrowseFormPOS
        {
            get
            {
                return frmBrowsePOS;
            }
            set
            {
                frmBrowsePOS = value;
            }
        }

        public frm_ServiceBrw BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_ServiceBrw();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private int OldTabIndex = 0;

        private bool boolPostData;

        public bool PostData
        {
            get { return boolPostData; }
            set { boolPostData = value; }
        }

        public frm_ServiceDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
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

        }

        private void IsUseStyle()
        {
            if (Settings.UseStyle == "NO")
            {
                rgStyle2.IsChecked = true;
                chkFontBold.Visibility = chkFontItalics.Visibility = txtFontType.Visibility = txtFontSize.Visibility = lbFontSize.Visibility = lbFontSize.Visibility = Visibility.Hidden;
            }
        }

        private void BtnDAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnCAdd_Click(object sender, RoutedEventArgs e)
        {
            
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
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            boolControlChanged = true;
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

        private void RgStyle1_Checked(object sender, RoutedEventArgs e)
        {
            if (!boolloading) return;

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

        private async void SimpleButton3_Click(object sender, RoutedEventArgs e)
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

        private void SetDecimalPlace()
        {
            if (ProductDecialPlace == 3)
            {
                numCcost.Mask = "f3";
                numPriceA.Mask = "f3";
                numPriceB.Mask = "f3";
                numPriceC.Mask = "f3";
            }
            else
            {
                numCcost.Mask = "f2";
                numPriceA.Mask = "f2";
                numPriceB.Mask = "f2";
                numPriceC.Mask = "f2";

            }
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

        private int DuplicateCount()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.DuplicateCount(txtSKU.Text.Trim());
        }

        private bool IsValidAll()
        {
            if (!IsValidGrid()) return false;
            if (txtSKU.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Code);
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
                        DocMessage.MsgInformation(Properties.Resources.Duplicate_Code);
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

            /*if (txtCaseQty.Value == 0)
            {
                DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Non Zero Pack Qty required");
                tcProduct.SelectedTabPage = tpGeneral;
                GeneralFunctions.SetFocus(txtCaseQty);
                return false;
            }*/

            if (chkPOSScreen.IsChecked == true)
            {
                PosDataObject.Product objProd = new PosDataObject.Product();
                objProd.Connection = SystemVariables.Conn;
                int intProd = objProd.GetPOSProductsforCategory(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));
                int intAllow = objProd.GetMaxPOSforCategory(GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString()));

                if (intProd >= intAllow)
                {
                    DocMessage.MsgInformation(Properties.Resources.Cannot_add_more_services_in_POS_Screen_for_this_Category + "\n" + Properties.Resources.Please_increase_this_count_in_Category);
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



            return true;
        }

        private bool IsValidGrid()
        {
            return true;
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
                prevBookingExpFlag = dr["BookingExpFlag"].ToString();
                linksku = GeneralFunctions.fnInt32(dr["LinkSKU"].ToString());
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


                cmbDept.EditValue = dr["DepartmentID"].ToString();
                cmbCategory.EditValue = dr["CategoryID"].ToString();
                numMinAge.Text = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString()).ToString();
                

                if (dr["DecimalPlace"].ToString() == "3")
                {
                    ProductDecialPlace = 3;
                }
                else
                {
                    ProductDecialPlace = 2;
                }
                SetDecimalPlace();

                numCcost.Text = GeneralFunctions.fnDouble(dr["Cost"].ToString()).ToString();

                numPriceA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numPriceB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numPriceC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();

                dblCurC = GeneralFunctions.fnDouble(numCcost.Text);
                dblPrA = GeneralFunctions.fnDouble(numPriceA.Text);
                dblPrB = GeneralFunctions.fnDouble(numPriceB.Text);
                dblPrC = GeneralFunctions.fnDouble(numPriceC.Text);

                spnMinTime.Value = GeneralFunctions.fnInt32(dr["MinimumServiceTime"].ToString());

                

                txtPoints.Text = GeneralFunctions.fnDouble(dr["Points"].ToString()).ToString();



                txtNotes.Text = dr["ProductNotes"].ToString();


                

                if (dr["ProductStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
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

                if (dr["POSBackground"].ToString() == "Skin")
                {
                    rgStyle1.IsChecked = true;
                    cmbSkin.Text = dr["POSScreenStyle"].ToString();
                }
                if (dr["POSBackground"].ToString() == "Color") rgStyle2.IsChecked = true;
            }
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
            objProduct.BinLocation = "";
            objProduct.PrimaryVendorID = "";
            objProduct.BrandID = 0;

            if (cmbDept.EditValue !=null)
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


            objProduct.ScaleBarCode = "N";
            objProduct.FoodStampEligible = "N";
            objProduct.PrintBarCode = "N";
            objProduct.NoPriceOnLabel = "N";
            objProduct.AllowZeroStock = "Y";
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

            objProduct.Cost = GeneralFunctions.fnDouble(numCcost.Text.Trim());
            objProduct.LastCost = 0;
            objProduct.PriceA = GeneralFunctions.fnDouble(numPriceA.Text.Trim());
            objProduct.PriceB = GeneralFunctions.fnDouble(numPriceB.Text.Trim());
            objProduct.PriceC = GeneralFunctions.fnDouble(numPriceC.Text.Trim());
            objProduct.QtyOnHand = 0;
            objProduct.ReorderQty = 0;
            objProduct.NormalQty = 0;
            objProduct.QtyOnLayaway = 0;
            objProduct.MinimumAge = GeneralFunctions.fnInt32(numMinAge.Text.Trim());
            objProduct.QtyToPrint = 0;
            objProduct.ProductNotes = txtNotes.Text.Trim();
            objProduct.Notes2 = "";
            objProduct.Points = GeneralFunctions.fnInt32(txtPoints.Text.Trim());
            objProduct.CaseQty = 1;
            objProduct.Season = "";
            objProduct.CaseUPC = "";

            objProduct.RentalPerMinute = 0;

            objProduct.RentalPerHour = 0;
            objProduct.RentalPerHalfDay = 0;
            objProduct.RentalPerDay = 0;
            objProduct.RentalPerWeek = 0;
            objProduct.RentalPerMonth = 0;
            objProduct.RentalDeposit = 0;
            objProduct.RentalPrompt = "N";

            objProduct.RentalMinHour = 0;
            objProduct.RentalMinAmount = 0;

            objProduct.RepairCharge = 0;
            objProduct.RepairPromptForCharge = "N";
            objProduct.RepairPromptForTag = "N";

            objProduct.MinimumServiceTime = GeneralFunctions.fnInt32(spnMinTime.Value);

            objProduct.ProductType = "S";

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

            objProduct.ScaleBackground = "Skin";
            objProduct.ScaleScreenColor = "0";
            objProduct.ScaleScreenStyle = "DevExpress Skin";
            objProduct.ScaleFontType = "Arial";
            objProduct.ScaleFontSize = 8;
            objProduct.ScaleFontColor = "0";
            objProduct.ScaleIsBold = "N";
            objProduct.ScaleIsItalics = "N";

            objProduct.LabelType = -1;

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

            gridView3.PostEditor();
            objProduct.VendorDataTable = null;
            objProduct.TaxDataTable = (grdTax.ItemsSource as DataTable);
            objProduct.RentTaxDataTable = null;
            objProduct.RepairTaxDataTable = null;
            objProduct.ProductModifierDataTable = null;

            objProduct.TerminalName = Settings.TerminalName;


            objProduct.AddJournal = false;
            objProduct.ProductDecimalPlace = 2;

            objProduct.PrinterDataTable = null;
            objProduct.NonDiscountable = "N";
            objProduct.Tare = 0;
            objProduct.Tare2 = 0;
            objProduct.SplitWeight = 0;
            objProduct.BookingExpFlag = boolBookingExportFlagChanged ? "N" : prevBookingExpFlag;
            objProduct.UOM = "";
            objProduct.ShopifyImageExportFlag = "N";
            objProduct.DiscountedCost = 0;
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

        private void TpGeneral_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 0;
        }

        private void TpStyle_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 1;
        }

        private void TpSales_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OldTabIndex = 2;
        }

        private void SetDefaultStyle()
        {

            txtFontType.Text = Settings.DefaultItemFontType;
            txtFontSize.Text = Settings.DefaultItemFontSize;
        }

        private void FetchSalesData()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Period", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Qty", System.Type.GetType("System.Double"));
            double dblQty = 0;
            dblQty = GetSalesQty(intID, "Today");
            dtbl.Rows.Add(new object[] { Properties.Resources.Today, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 7 Days");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_7_Days, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 14 Days");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_14_Days, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 1 Month");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Month, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 3 Months");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_3_Months, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 6 Months");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_6_Months, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "Last 1 Year");
            dtbl.Rows.Add(new object[] { Properties.Resources.Last_1_Year, dblQty });
            dblQty = 0;
            dblQty = GetSalesQty(intID, "To Date");
            dtbl.Rows.Add(new object[] { Properties.Resources.To_Date, dblQty });

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
        }

        private double GetSalesQty(int pID, string cat)
        {
            PosDataObject.Sales objsale = new PosDataObject.Sales();
            objsale.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objsale.FetchProductSalesRecord(pID, cat, DateTime.Today);
        }

        private void TcProduct_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                        if (cmbCategory.EditValue == null)
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
                        {
                            try
                            {
                                BrowseForm.FetchData(BrowseForm.IsPOS, BrowseForm.cmbFilter.EditValue.ToString());
                            }
                            catch
                            {
                                BrowseFormPOS.FetchData(BrowseFormPOS.IsPOS, BrowseFormPOS.cmbFilter.EditValue.ToString());
                            }
                        }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();

            PopulateCategory();
            DeptLookup();
            PopulateTax();

            PopulateAndSetFormSkin();

            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Service;
                txtSKU.IsEnabled = true;
                
                if (blAddFromPOS)
                {
                    txtSKU.Text = strAddSKU;
                    txtSKU.IsEnabled = false;
                }
                SetDecimalPlace();
                prevOnHand = 0;
            }
            else
            {
                if (!blDuplicate)
                {
                    Title.Text = Properties.Resources.Edit_Service;
                    txtSKU.IsEnabled = false;
                }
                else
                {
                    Title.Text = Properties.Resources.New_Service;
                    txtSKU.IsEnabled = true;
                    SetDecimalPlace();
                }
                ShowData();
            }
            tcProduct.TabIndex = 0;
            GeneralFunctions.SetFocus(txtSKU);

            IsUseStyle();
            
            PopulateBatchGrid();

            if (blDuplicate)
            {
                intID = 0;
                txtSKU.Text = "";
                prevBookingExpFlag = "N";
            }

            rgStyle2.IsChecked = true;
            lbColor.Text = Properties.Resources.Color;
            cmbSkin.Visibility = Visibility.Collapsed;
            txtColor.Visibility = Visibility.Visible;
            lbdelaultcolor.Text = Properties.Resources.Leave_blank_default_color;
            boolloading = true;
            boolControlChanged = false;
            boolBookingExportFlagChanged = false;
        }

        private void TxtSKU_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
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

        private void CmbSkin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
