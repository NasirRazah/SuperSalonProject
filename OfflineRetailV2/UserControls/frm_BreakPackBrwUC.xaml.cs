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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_BreakPackBrwUC.xaml
    /// </summary>
    public partial class frm_BreakPackBrwUC : UserControl
    {
        public frm_BreakPackBrwUC()
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

        private bool blIsPOS;
        public bool IsPOS
        {
            get { return blIsPOS; }
            set { blIsPOS = value; }
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
            fParent.RedirectToSubmenu("Discounts");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdProduct.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdProduct, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0)
            {
                gridView1.FocusedRowHandle = intColCtr;
            }
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void PopulateBreakPackStatus()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "All Break packs", "All Break Packs" });
            dtbl.Rows.Add(new object[] { "Active Break packs", "Active Break Packs" });
            dtbl.Rows.Add(new object[] { "Inactive Break packs", "Inactive Break Packs" });

            cmbFilter.ItemsSource = dtbl;
            dtbl.Dispose();
            cmbFilter.EditValue = "Active Break packs";
        }


        public void FetchData(bool blPOS, string Status)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchBreakPackData(blPOS, Status);

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



            grdProduct.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();

            
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssProductAdd) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BreakPackDlg frm_ProductDlg = new frm_BreakPackDlg();
            try
            {
                frm_ProductDlg.AddFromPOS = false;
                frm_ProductDlg.Duplicate = false;
                frm_ProductDlg.ID = 0;
                frm_ProductDlg.BrowseForm = this;
                frm_ProductDlg.ShowDialog();
                intNewRecID = frm_ProductDlg.NewID;
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
            if ((!SecurityPermission.AcssProductEdit) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BreakPackDlg frm_ProductDlg = new frm_BreakPackDlg();
            try
            {
                frm_ProductDlg.ID = await ReturnRowID();
                if (frm_ProductDlg.ID > 0)
                {
                    frm_ProductDlg.Duplicate = false;
                    frm_ProductDlg.BrowseForm = this;
                    frm_ProductDlg.ShowDialog();
                    intNewRecID = frm_ProductDlg.ID;
                }
            }
            finally
            {
                frm_ProductDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
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
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();
                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();
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

                rep_ProductSnap.rCurrentCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_ProductSnap.rDCost.DataBindings.Add("Text", dtbl, "DCost");
                rep_ProductSnap.rLastPurchase.DataBindings.Add("Text", dtbl, "LastCost");
                rep_ProductSnap.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");
                rep_ProductSnap.rPriceB.DataBindings.Add("Text", dtbl, "PriceB");
                rep_ProductSnap.rPriceC.DataBindings.Add("Text", dtbl, "PriceC");
                rep_ProductSnap.rPoints.DataBindings.Add("Text", dtbl, "Points");

                rep_ProductSnap.chkZero.DataBindings.Add("Text", dtbl, "AllowZeroStock");
                rep_ProductSnap.chkDispStk.DataBindings.Add("Text", dtbl, "DisplayStockinPOS");
                rep_ProductSnap.chkActive.DataBindings.Add("Text", dtbl, "ProductStatus");
                rep_ProductSnap.chkDigit.DataBindings.Add("Text", dtbl, "DecimalPlace");

                rep_ProductSnap.rUPC.DataBindings.Add("Text", dtbl, "UPC");
                rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "Brand");
                rep_ProductSnap.rSeason.DataBindings.Add("Text", dtbl, "Season");


                if (LinkSKU > 0) // Breack pack
                {
                    rep_ProductSnap.lbSKU.Text = "UPC";
                    rep_ProductSnap.lb1.Text = "Ratio";
                    rep_ProductSnap.rAltSKU.DataBindings.Add("Text", dtbl, "BreakPackRatio");
                    rep_ProductSnap.lb4.Text = "Link SKU";
                    rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "LinkSKU");
                    rep_ProductSnap.rDept.DataBindings.Add("Text", dtbl, "LinkSKUDesc");

                    rep_ProductSnap.lb2.Visible = false;
                    rep_ProductSnap.lb3.Visible = false;
                    rep_ProductSnap.lb5.Visible = false;
                    rep_ProductSnap.lb6.Visible = false;
                    rep_ProductSnap.lb7.Visible = false;
                    rep_ProductSnap.lb8.Visible = false;
                    rep_ProductSnap.lb9.Visible = false;
                    rep_ProductSnap.lb10.Visible = false;
                    rep_ProductSnap.rPT.Visible = false;
                    rep_ProductSnap.rUPC.Visible = false;
                    rep_ProductSnap.rCategory.Visible = false;
                    rep_ProductSnap.rOnHandQty.Visible = false;
                    rep_ProductSnap.rReorderQty.Visible = false;
                    rep_ProductSnap.rNormalQty.Visible = false;
                    rep_ProductSnap.rOnLawaway.Visible = false;
                }
                else
                {

                    rep_ProductSnap.rOnHandQty.DataBindings.Add("Text", dtbl, "QtyOnHand");
                    rep_ProductSnap.rReorderQty.DataBindings.Add("Text", dtbl, "ReorderQty");
                    rep_ProductSnap.rNormalQty.DataBindings.Add("Text", dtbl, "NormalQty");
                    rep_ProductSnap.rOnLawaway.DataBindings.Add("Text", dtbl, "QtyOnLayaway");
                }
                rep_ProductSnap.rNoPrint.DataBindings.Add("Text", dtbl, "QtyToPrint");
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



                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_ProductSnapDetail.PrinterName = Settings.ReportPrinterName;
                    rep_ProductSnapDetail.CreateDocument();
                    rep_ProductSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    rep_ProductSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_ProductSnapDetail.ShowPreviewDialog();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
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
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
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
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_BreakPackDlg frm_ProductDlg = new frm_BreakPackDlg();
            try
            {
                frm_ProductDlg.ID = await ReturnRowID();
                if (frm_ProductDlg.ID > 0)
                {
                    //frm_ProductDlg.ShowData();
                    frm_ProductDlg.Duplicate = true;
                    frm_ProductDlg.BrowseForm = this;
                    frm_ProductDlg.ShowDialog();
                    intNewRecID = frm_ProductDlg.ID;
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
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete("Break Pack Item"))
                {
                    int linkID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colLinkID));
                    PosDataObject.Product objProduct = new PosDataObject.Product();
                    objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intReturn = objProduct.DeleteRecord(intRecdID);

                    if (intReturn != 0)
                    {
                        if (intReturn == 1)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_break_pack_item_as_ + Properties.Resources.there_exists_Purchase_Order_items_against_it);
                        if (intReturn == 2)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_break_pack_item_as_ + Properties.Resources.there_exists_Received_items_against_it);
                        if (intReturn == 3)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_break_pack_item_as_ + Properties.Resources.there_exists_Invoice_items_against_it);
                        if (intReturn == 6)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_break_pack_item_as_ + Properties.Resources.it_is_used_as_tagged_item);
                        if ((intReturn == 4) || (intReturn == 5))
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_break_pack_item_as_ + Properties.Resources.there_exists_Journal_items_against_it);
                        return;
                    }

                    FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                    gridView1.FocusedRowHandle = intRecdID - 1;
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void CmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData(blIsPOS, cmbFilter.EditValue.ToString());
        }

        private async void BtnPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID == -1) return;
            if ((grdProduct.ItemsSource as DataTable).Rows.Count  == 0) return;
            if (await GeneralFunctions.GetCellValue1(intRowID,  grdProduct, colPrintLabel) == "N") return;
            int intQty = 0;
            intQty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colQtyToPrint));
            if (intQty == 0) return;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg frm_PrintBarcodeDlg = new OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg();
            try
            {
                frm_PrintBarcodeDlg.isbatchprint = false;
                frm_PrintBarcodeDlg.SKU = await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colSKU);
                frm_PrintBarcodeDlg.ProductDesc = await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colDesc);
                frm_PrintBarcodeDlg.ProductDecimalPlace = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colDecimal));
                if (await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colNoPriceLabel) == "Y")
                    frm_PrintBarcodeDlg.ProductPrice = "";
                else
                    frm_PrintBarcodeDlg.ProductPrice = await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colPrice);
                //if (GeneralFunctions.GetCellValue(intRowID, gridView1, colQtyToPrint).Trim() != "")
                //intQty = GeneralFunctions.fnInt32(GeneralFunctions.GetCellValue(intRowID, gridView1, colQtyToPrint));
                //if(intQty == 0) intQty = 1;
                frm_PrintBarcodeDlg.Qty = intQty;
                frm_PrintBarcodeDlg.LabelType = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdProduct, colLabelType));
                frm_PrintBarcodeDlg.ShowDialog();
            }
            finally
            {
                frm_PrintBarcodeDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Breakpacks")
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
                txtSearchGrdData.InfoText = "Search Breakpacks";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Breakpacks") return;
            if (txtSearchGrdData.Text == "")
            {
                grdProduct.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdProduct.FilterString = "([SKU] LIKE '%" + filterValue + "%' OR [ProductName] LIKE '%" + filterValue + "%')";
            }
        }
    }
}
