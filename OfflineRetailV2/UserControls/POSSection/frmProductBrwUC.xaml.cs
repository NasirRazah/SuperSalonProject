using System;
using System.Collections;
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
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    
    /// </summary>
    public partial class frmProductBrwUC : UserControl
    {
        public frmProductBrwUC()
        {
            InitializeComponent();
        }

        private FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool bOpenKeyBrd = false;




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

        public void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private int rowPos = 0;
        private int intSelectedRowHandle;
        private bool blIsPOS;
        private GridColumn _searchcol;
        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        } 
        public bool IsPOS
        {
            get { return blIsPOS; }
            set { blIsPOS = value; }
        }

        public class Data
        {
            public string ID { get; set; }
            public string SKU { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string UPC { get; set; }
            public double PriceA { get; set; }
            public string QtyOnHand { get; set; }
            public string Department { get; set; }
            public string Category { get; set; }
            public string ProductType { get; set; }
            public string FS { get; set; }
            public string Season { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPart { get; set; }
            public string NoPriceOnLabel { get; set; }
            public string PrintBarCode { get; set; }
            public string DecimalPlace { get; set; }
            public string QtyToPrint { get; set; }
            public string LabelType { get; set; }
            public string ProductStatus { get; set; }
            public ImageSource Image { get; set; }
        }

        

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;

            for (intColCtr = 0; intColCtr < (gridControl1.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, gridControl1, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public void SetDecimalPlace()
        {

            if (Settings.DecimalPlace == 3) colPrice.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
            else colPrice.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };

            /*CultureInfo culture = new CultureInfo("en-US", true);
            culture.NumberFormat.CurrencySymbol = "€";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            DevExpress.Utils.FormatInfo.AlwaysUseThreadFormat = true;

            repPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            if (Settings.DecimalPlace == 3)
            {
                repPrice.DisplayFormat.FormatString = "#.000;[#.0];Zero";
                repPrice.Mask.EditMask = "c3";
            }
            else
            {
                repPrice.DisplayFormat.FormatString = "#.00;[#.0];Zero";
                repPrice.Mask.EditMask = "c2";
            }

            repPrice.Mask.Culture = culture;
            repPrice.Mask.UseMaskAsDisplayFormat = true;*/
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, gridControl1, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, gridControl1, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((gridControl1.ItemsSource as ICollection).Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void PopulateProductStatus()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "Active Products", Properties.Resources.Active_Products });
            dtbl.Rows.Add(new object[] { "Inactive Products", Properties.Resources.Inactive_Products });
            dtbl.Rows.Add(new object[] { "All Products", Properties.Resources.All_Products });

            cmbFilter.ItemsSource = dtbl;
            cmbFilter.EditValue = "Active Products";
            
        }

        public void FetchData(bool blPOS, string Status)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchData(blPOS, Status);

            

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
            


            gridControl1.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();
        }

        private void CmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData(blIsPOS, cmbFilter.EditValue.ToString());
        }

        public void GetItemGridDimensions()
        {
            try
            {
                //gridView5.BeginUpdate();

                colSKU.VisibleIndex = -1;
                colDesc.VisibleIndex = -1;
                colBrand.VisibleIndex = -1;
                colUPC.VisibleIndex = -1;
                colPrice.VisibleIndex = -1;
                colQty.VisibleIndex = -1;
                colDept.VisibleIndex = -1;
                colCat.VisibleIndex = -1;
                colType.VisibleIndex = -1;
                colFS.VisibleIndex = -1;
                colSeason.VisibleIndex = -1;
                colVID.VisibleIndex = -1;
                colVName.VisibleIndex = -1;
                colVPart.VisibleIndex = -1;

                colSKU.Width = 90;
                colDesc.Width = 140;
                colBrand.Width = 60;
                colUPC.Width = 60;
                colPrice.Width = 55;
                colQty.Width = 35;
                colDept.Width = 75;
                colCat.Width = 75;
                colType.Width = 60;
                colFS.Width = 70;
                colSeason.Width = 50;

                colVID.Width = 55;
                colVName.Width = 75;
                colVPart.Width = 80;

                string[] itemvisible = Settings.GridItemParam1.Split('*');
                int[] visibleindex = new int[itemvisible.Length];
                for (int x = 0; x < itemvisible.Length; x++)
                {
                    visibleindex[x] = GeneralFunctions.fnInt32(itemvisible[x].ToString());
                }
                Array.Sort(visibleindex);
                string[] itemwidth = Settings.GridItemParam2.Split('*');
                string[] ItemGrid = Settings.ItemGrid.Split('*'); ;
                int i = -1;

                gridView1.BeginInit();

                foreach (int val in visibleindex)
                {
                    if (val < 0) continue;
                    int findindex = -1;
                    foreach (string sval in itemvisible)
                    {
                        findindex++;
                        if (sval == val.ToString())
                        {
                            break;
                        }

                    }
                    if (findindex == 0) colSKU.VisibleIndex = val;
                    if (findindex == 1) colDesc.VisibleIndex = val;
                    if (findindex == 2) colBrand.VisibleIndex = val;
                    if (findindex == 3) colUPC.VisibleIndex = val;
                    if (findindex == 4) colPrice.VisibleIndex = val;
                    if (findindex == 5) colQty.VisibleIndex = val;
                    if (findindex == 6) colDept.VisibleIndex = val;
                    if (findindex == 7) colCat.VisibleIndex = val;
                    if (findindex == 8) colType.VisibleIndex = val;
                    if (findindex == 9) colFS.VisibleIndex = val;
                    if (findindex == 10) colSeason.VisibleIndex = val;
                    if (findindex == 11) colVID.VisibleIndex = val;
                    if (findindex == 12) colVName.VisibleIndex = val;
                    if (findindex == 13) colVPart.VisibleIndex = val;
                }

                foreach (string s in ItemGrid)
                {
                    i++;
                    if (s == "SK")
                    {
                        //colSKU.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colSKU.VisibleIndex != -1) colSKU.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "PN")
                    {
                        //colProductName.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colDesc.VisibleIndex != -1) colDesc.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "BR")
                    {
                        //colBrand.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colBrand.VisibleIndex != -1) colBrand.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "UP")
                    {
                        //colUPC.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colUPC.VisibleIndex != -1) colUPC.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "PR")
                    {
                        //colPrice.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colPrice.VisibleIndex != -1) colPrice.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "QT")
                    {
                        //colQty.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colQty.VisibleIndex != -1) colQty.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "DP")
                    {
                        //colDept.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colDept.VisibleIndex != -1) colDept.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "CT")
                    {
                        //colCat.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colCat.VisibleIndex != -1) colCat.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "TY")
                    {
                        colType.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colType.VisibleIndex != -1) colType.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "FS")
                    {
                        //colFS.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colFS.VisibleIndex != -1) colFS.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "SN")
                    {
                        //colSeason.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colSeason.VisibleIndex != -1) colSeason.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "VI")
                    {
                        //colSeason.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colVID.VisibleIndex != -1) colVID.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "VN")
                    {
                        //colSeason.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colVName.VisibleIndex != -1) colVName.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                    if (s == "VP")
                    {
                        //colSeason.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colVPart.VisibleIndex != -1) colVPart.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.Width - 5) / 100));
                    }
                }
            }
            finally
            {
                gridView1.EndInit();
            }
        }

        private async void BarButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            //(Window.GetWindow(this) as MainWindow).Grid.Effect = new  System.Windows.Media.Effects.ShaderEffect();
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            int intNewRecID = 0;
            AddProductWindow frm_ProductDlg = new AddProductWindow();
            try
            {
                frm_ProductDlg.AddFromPOS = false;
                frm_ProductDlg.Duplicate = false;
                frm_ProductDlg.ID = 0;
                
                frm_ProductDlg.ShowDialog();
                intNewRecID = frm_ProductDlg.NewID;

                if (frm_ProductDlg.PostData)
                {
                    FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                    
                }
            }
            finally
            {
                frm_ProductDlg.Close();


                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }


        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            if ((blIsPOS) && (GeneralFunctions.GetCellValue(intRowID, gridControl1, colType) == "Service"))
            {
                /*Todo:frm_ServiceDlg frm_ProductDlg = new frm_ServiceDlg();
                try
                {
                    frm_ProductDlg.ID = ReturnRowID();
                    if (frm_ProductDlg.ID > 0)
                    {
                        frm_ProductDlg.Duplicate = false;
                        frm_ProductDlg.BrowseFormPOS = this;
                        frm_ProductDlg.ShowDialog();
                        intNewRecID = frm_ProductDlg.ID;
                    }
                }
                finally
                {
                    frm_ProductDlg.Dispose();
                }*/
            }
            else
            {
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                AddProductWindow frm_ProductDlg = new AddProductWindow();
                try
                {
                    frm_ProductDlg.ID = await ReturnRowID();
                    if (frm_ProductDlg.ID > 0)
                    {
                        frm_ProductDlg.Duplicate = false;
                        frm_ProductDlg.ShowDialog();
                        intNewRecID = frm_ProductDlg.ID;
                        if (frm_ProductDlg.PostData)
                        {
                            FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                        }
                    }
                }
                finally
                {
                    frm_ProductDlg.Close();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            await EditProcess();
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((!SecurityPermission.AcssProductEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            await EditProcess();
        }

        private async void BtnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            AddProductWindow frm_ProductDlg = new AddProductWindow();
            try
            {
                frm_ProductDlg.ID = await ReturnRowID();
                if (frm_ProductDlg.ID > 0)
                {
                    frm_ProductDlg.Duplicate = true;
                    frm_ProductDlg.ShowDialog();
                    intNewRecID = frm_ProductDlg.NewID;
                    if (frm_ProductDlg.PostData)
                    {
                        FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                    }
                }
            }
            finally
            {
                frm_ProductDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductDelete) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            //SetRowIndex();

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Product))
                {
                    PosDataObject.Product objProduct = new PosDataObject.Product();
                    objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intReturn = objProduct.DeleteRecord(intRecdID);

                    if (intReturn != 0)
                    {
                        if (intReturn == 1)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.Purchase_Order_items_against_it);
                        if (intReturn == 2)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.Received_items_against_it);
                        if (intReturn == 3)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.Invoice_items_against_it);
                        if (intReturn == 6)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_it_is_used_as_tagged_item);
                        if ((intReturn == 4) || (intReturn == 5))
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.Journal_items_against_it);
                        if (intReturn == 7)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.break_pack_items_against_it);
                        if (intReturn == 8)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_ + Properties.Resources.open_Inventory_Adjustment_documents_with_this_product_only);
                        if (intReturn == 15)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_product_as_there_exists_  + Properties.Resources.Buy_N_Get_Free_records_against_it);
                        if (intReturn == 11)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_record_as_it_is_already_exported);
                    }
                    else
                    {
                        FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                        if ((gridControl1.ItemsSource as DataTable).Rows.Count > 1)
                        {
                            await SetCurrentRow(intRecdID - 1);
                        }
                    }
                    
                    
                }
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductPrint) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                return;
            }
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();
                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();
                DataTable dtbl3 = new DataTable();
                PosDataObject.Product objProduct = new PosDataObject.Product();
                objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objProduct.DecimalPlace = Settings.DecimalPlace;
                dtbl = objProduct.FetchRecordForReport(await ReturnRowID());

                int LinkSKU = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    LinkSKU = GeneralFunctions.fnInt32(dr["LinkSKUID"].ToString());
                    break;
                }

                OfflineRetailV2.Report.Product.repProductSnapDetail rep_ProductSnapDetail = new OfflineRetailV2.Report.Product.repProductSnapDetail();
                OfflineRetailV2.Report.Product.repProductSnap1 rep_ProductSnap = new OfflineRetailV2.Report.Product.repProductSnap1();
                OfflineRetailV2.Report.Product.repProductVendor rep_ProductVendor = new OfflineRetailV2.Report.Product.repProductVendor();
                OfflineRetailV2.Report.Product.repProductTax rep_ProductTax = new OfflineRetailV2.Report.Product.repProductTax();
                OfflineRetailV2.Report.Product.repProductBreakPack rep_ProductBreakPack = new OfflineRetailV2.Report.Product.repProductBreakPack();

                rep_ProductSnapDetail.subrepGeneral.ReportSource = rep_ProductSnap;
                rep_ProductSnap.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_ProductSnapDetail);
                rep_ProductSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_ProductSnap.rReportHeader.Text = Settings.ReportHeader_Address;

                rep_ProductSnap.rPic.DataBindings.Add("Text", dtbl, "ID");
                rep_ProductSnap.rSKU.DataBindings.Add("Text", dtbl, "SKU");
                rep_ProductSnap.rAltSKU.DataBindings.Add("Text", dtbl, "SKU2");
                rep_ProductSnap.rDesc.DataBindings.Add("Text", dtbl, "Description");
                rep_ProductSnap.rDept.DataBindings.Add("Text", dtbl, "Department");
                rep_ProductSnap.rCategory.DataBindings.Add("Text", dtbl, "Category");
                rep_ProductSnap.rPT.DataBindings.Add("Text", dtbl, "ProductType");
                rep_ProductSnap.rMinAge.DataBindings.Add("Text", dtbl, "MinimumAge");

                rep_ProductSnap.chkBar.DataBindings.Add("Text", dtbl, "ScaleBarCode");
                rep_ProductSnap.chkFood.DataBindings.Add("Text", dtbl, "FoodStampEligible");
                rep_ProductSnap.chkNoPL.DataBindings.Add("Text", dtbl, "NoPriceOnLabel");
                rep_ProductSnap.chkPL.DataBindings.Add("Text", dtbl, "PrintBarCode");
                rep_ProductSnap.chkPOS.DataBindings.Add("Text", dtbl, "AddtoPOSScreen");
                rep_ProductSnap.chkPrice.DataBindings.Add("Text", dtbl, "PromptForPrice");

                rep_ProductSnap.chkZero.DataBindings.Add("Text", dtbl, "AllowZeroStock");
                rep_ProductSnap.chkDispStk.DataBindings.Add("Text", dtbl, "DisplayStockinPOS");
                rep_ProductSnap.chkActive.DataBindings.Add("Text", dtbl, "ProductStatus");
                rep_ProductSnap.chkDigit.DataBindings.Add("Text", dtbl, "DecimalPlace");

                rep_ProductSnap.rUPC.DataBindings.Add("Text", dtbl, "UPC");
                rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "Brand");
                rep_ProductSnap.rSeason.DataBindings.Add("Text", dtbl, "Season");

                rep_ProductSnap.rDCost.DataBindings.Add("Text", dtbl, "DCost");
                rep_ProductSnap.rCurrentCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_ProductSnap.rLastPurchase.DataBindings.Add("Text", dtbl, "LastCost");
                rep_ProductSnap.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");
                rep_ProductSnap.rPriceB.DataBindings.Add("Text", dtbl, "PriceB");
                rep_ProductSnap.rPriceC.DataBindings.Add("Text", dtbl, "PriceC");
                rep_ProductSnap.rPoints.DataBindings.Add("Text", dtbl, "Points");

                //rep_ProductSnap.rPrefferedType.DataBindings.Add("Text", dtbl, "ShipState");
                rep_ProductSnap.rNoPrint.DataBindings.Add("Text", dtbl, "QtyToPrint");
                rep_ProductSnap.rOnHandQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
                rep_ProductSnap.rReorderQty.DataBindings.Add("Text", dtbl, "ReorderQty");
                rep_ProductSnap.rNormalQty.DataBindings.Add("Text", dtbl, "NormalQty");
                rep_ProductSnap.rOnLawaway.DataBindings.Add("Text", dtbl, "QtyOnLayaway");
                rep_ProductSnap.rNotes.DataBindings.Add("Text", dtbl, "ProductNotes");
                rep_ProductSnap.rBinLocation.DataBindings.Add("Text", dtbl, "BinLocation");


                PosDataObject.Product objProduct1 = new PosDataObject.Product();
                objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objProduct1.ShowPrimayVendor(await ReturnRowID());

                if (dtbl1.Rows.Count > 0)
                {
                    rep_ProductSnapDetail.subrepVendor.ReportSource = rep_ProductVendor;
                    rep_ProductVendor.Report.DataSource = dtbl1;
                    rep_ProductVendor.DecimalPlace = Settings.DecimalPlace;
                    rep_ProductVendor.rVendor.DataBindings.Add("Text", dtbl1, "VendorName");
                    rep_ProductVendor.rPart.DataBindings.Add("Text", dtbl1, "PartNumber");
                    rep_ProductVendor.rCost.DataBindings.Add("Text", dtbl1, "Price");
                    rep_ProductVendor.rPrimary.DataBindings.Add("Text", dtbl1, "IsPrimary");
                }

                PosDataObject.Product objProduct2 = new PosDataObject.Product();
                objProduct2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objProduct2.DecimalPlace = Settings.DecimalPlace;
                dtbl2 = objProduct2.ShowTaxes(await ReturnRowID());

                if (dtbl2.Rows.Count > 0)
                {
                    rep_ProductSnapDetail.subrepTax.ReportSource = rep_ProductTax;
                    rep_ProductTax.Report.DataSource = dtbl2;
                    rep_ProductTax.rTax.DataBindings.Add("Text", dtbl2, "TaxName");
                }

                if (LinkSKU == -1)
                {
                    PosDataObject.Product objProduct3 = new PosDataObject.Product();
                    objProduct3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objProduct3.DecimalPlace = Settings.DecimalPlace;
                    dtbl3 = objProduct3.ShowBreakPacks(await ReturnRowID());

                    if (dtbl3.Rows.Count > 0)
                    {
                        rep_ProductSnapDetail.subrepbreakpack.ReportSource = rep_ProductBreakPack;
                        rep_ProductBreakPack.Report.DataSource = dtbl3;
                        rep_ProductBreakPack.rSKU.DataBindings.Add("Text", dtbl3, "SKU");
                        rep_ProductBreakPack.rDesc.DataBindings.Add("Text", dtbl3, "Description");
                        rep_ProductBreakPack.rRatio.DataBindings.Add("Text", dtbl3, "Ratio");
                    }
                }
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_ProductSnapDetail.PrinterName = Settings.ReportPrinterName;
                    rep_ProductSnapDetail.CreateDocument();
                    rep_ProductSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    rep_ProductSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_ProductSnapDetail.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_ProductSnapDetail;
                    window.ShowDialog();

                }
                finally
                {
                    rep_ProductSnap.Dispose();
                    rep_ProductVendor.Dispose();
                    rep_ProductTax.Dispose();
                    rep_ProductSnapDetail.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void BtnPrintBarCode_Click(object sender, RoutedEventArgs e)
        {
            await GeneralFunctions.PrintItemLabel(this, false);
        }

        private async void BarButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            //await GeneralFunctions.PrintItemLabel(this, false);

            //PosDataObject.BookerPreIntegeration bookerIntegeration = new PosDataObject.BookerPreIntegeration();
            //bookerIntegeration.ImportPreMigrationData();


            //PosDataObject.BookerService bookerService = new PosDataObject.BookerService();
            //bookerService.AddProductsData();
            //bookerService.UpdatePriceChangeData();
            //bookerService.DeleteProductData();

        }

        private void CmbFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Items")
            {
                txtSearchGrdData.Text = "";
            }

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            if (bOpenKeyBrd) return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }

        private void TxtSearchGrdData_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.Text == "")
            {
                txtSearchGrdData.InfoText = "Search Items";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Items") return;
            if (txtSearchGrdData.Text == "")
            {
                gridControl1.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                gridControl1.FilterString = "([SKU] LIKE '%" + filterValue + "%' OR [ProductName] LIKE '%" + filterValue + "%' OR [SKU2] LIKE '%" + filterValue + "%' OR [SKU3] LIKE '%" + filterValue + "%')";
            }
        }

        private void BtnBackMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Items");
        }

        private void TxtSearchGrdData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutFullKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                bOpenKeyBrd = true;
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }
    }
}
