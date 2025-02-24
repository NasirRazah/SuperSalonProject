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
    /// Interaction logic for frm_ServiceBrwUC.xaml
    /// </summary>
    public partial class frm_ServiceBrwUC : UserControl
    {
        public frm_ServiceBrwUC()
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

        public bool IsPOS
        {
            get { return blIsPOS; }
            set { blIsPOS = value; }
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

            for (intColCtr = 0; intColCtr < (grdProduct.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdProduct, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
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

        public void PopulateServiceStatus()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("FilterText");
            dtbl.Columns.Add("DisplayText");
            dtbl.Rows.Add(new object[] { "Active Services", "Active Services" });
            dtbl.Rows.Add(new object[] { "Inactive Services", "Inactive Services" });
            dtbl.Rows.Add(new object[] { "All Services", "All Services" });
            cmbFilter.ItemsSource = dtbl;
            cmbFilter.EditValue = "Active Services";
        }

        public void FetchData(bool blPOS, string Status)
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            dbtbl = objProduct.FetchServiceData(blPOS, Status);

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

        private void CmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData(blIsPOS, cmbFilter.EditValue.ToString());
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
            frm_ServiceDlg frm_ProductDlg = new frm_ServiceDlg();
            try
            {
                frm_ProductDlg.AddFromPOS = false;
                frm_ProductDlg.Duplicate = false;
                frm_ProductDlg.ID = 0;
                //frm_ProductDlg.BrowseForm = this;
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
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_ServiceDlg frm_ProductDlg = new frm_ServiceDlg();
            try
            {
                frm_ProductDlg.ID = await ReturnRowID();
                if (frm_ProductDlg.ID > 0)
                {
                    frm_ProductDlg.Duplicate = false;
                    //frm_ProductDlg.BrowseForm = this;
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
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_ServiceDlg frm_ProductDlg = new frm_ServiceDlg();
            try
            {
                frm_ProductDlg.ID = await ReturnRowID();
                if (frm_ProductDlg.ID > 0)
                {
                    frm_ProductDlg.Duplicate = true;
                    //frm_ProductDlg.BrowseForm = this;
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
            if ((grdProduct.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = await ReturnRowID();
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Service))
                {
                    PosDataObject.Product objProduct = new PosDataObject.Product();
                    objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intReturn = objProduct.DeleteRecord(intRecdID);

                    if (intReturn != 0)
                    {
                        if (intReturn == 1)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.Purchase_Order_items_against_it);
                        if (intReturn == 2)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.Received_items_against_it);
                        if (intReturn == 3)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.Invoice_items_against_it);
                        if (intReturn == 6)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_it_is_used_as_tagged_item);
                        if ((intReturn == 4) || (intReturn == 5))
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.Journal_items_against_it);
                        if (intReturn == 7)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.break_pack_items_against_it);
                        if (intReturn == 8)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_there_exists_  + Properties.Resources.open_Inventory_Adjustment_documents_with_this_service_only);
                        if (intReturn == 9)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_it_is_  + Properties.Resources.assigned_in_1_or_more_employees);
                        if (intReturn == 10)
                            DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_service_as_it_is_  + Properties.Resources.assigned_in_1_or_more_appointments);
                    }
                    else
                    {
                        FetchData(blIsPOS, cmbFilter.EditValue.ToString());
                        if ((grdProduct.ItemsSource as DataTable).Rows.Count > 1)
                        {
                            await SetCurrentRow(intRecdID - 1);
                        }
                    }
                }
            }
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

                rep_ProductSnap.lbSKU.Text = "Code";
                rep_ProductSnap.lb11.Text = "Cost";
                rep_ProductSnap.lb7.Text = "Min. Age";
                rep_ProductSnap.lb9.Text = "Min. Time";
                rep_ProductSnap.lb1.Visible = false;
                rep_ProductSnap.lb100.Visible = false;
                rep_ProductSnap.rDCost.Visible = false;
                rep_ProductSnap.lb2.Visible = false;
                rep_ProductSnap.lb3.Visible = false;
                rep_ProductSnap.lb8.Visible = false;
                rep_ProductSnap.lb10.Visible = false;
                rep_ProductSnap.lb12.Visible = false;
                rep_ProductSnap.lb13.Visible = false;
                rep_ProductSnap.lb14.Visible = false;
                rep_ProductSnap.lb15.Visible = false;
                rep_ProductSnap.lb16.Visible = false;
                rep_ProductSnap.lb17.Visible = false;
                rep_ProductSnap.lb18.Visible = false;
                rep_ProductSnap.lb21.Visible = false;
                rep_ProductSnap.lb22.Visible = false;
                rep_ProductSnap.lb23.Visible = false;
                rep_ProductSnap.lb24.Visible = false;
                rep_ProductSnap.lb25.Visible = false;
                rep_ProductSnap.lb26.Visible = false;
                rep_ProductSnap.lb27.Visible = false;


                rep_ProductSnap.rAltSKU.Visible = false;
                rep_ProductSnap.rAltSKU2.Visible = false;
                rep_ProductSnap.rPT.Text = "";
                rep_ProductSnap.rMinAge.Visible = false;
                rep_ProductSnap.chkBar.Visible = false;
                rep_ProductSnap.chkFood.Visible = false;
                rep_ProductSnap.chkNoPL.Visible = false;
                rep_ProductSnap.chkPL.Visible = false;
                rep_ProductSnap.chkZero.Visible = false;
                rep_ProductSnap.chkDispStk.Visible = false;
                rep_ProductSnap.chkDigit.Visible = false;
                rep_ProductSnap.rUPC.Visible = false;
                rep_ProductSnap.rSeason.Visible = false;
                rep_ProductSnap.rLastPurchase.Visible = false;
                rep_ProductSnap.rPoints.Visible = false;
                rep_ProductSnap.rNoPrint.Visible = false;
                rep_ProductSnap.rNormalQty.Visible = false;
                rep_ProductSnap.rOnLawaway.Visible = false;
                rep_ProductSnap.rBinLocation.Visible = false;
                rep_ProductSnap.rPrefferedType.Visible = false;

                rep_ProductSnap.rPic.DataBindings.Add("Text", dtbl, "ID");
                rep_ProductSnap.rSKU.DataBindings.Add("Text", dtbl, "SKU");
                rep_ProductSnap.rDesc.DataBindings.Add("Text", dtbl, "Description");
                rep_ProductSnap.rDept.DataBindings.Add("Text", dtbl, "Department");
                rep_ProductSnap.rCategory.DataBindings.Add("Text", dtbl, "Category");
                rep_ProductSnap.chkPOS.DataBindings.Add("Text", dtbl, "AddtoPOSScreen");
                rep_ProductSnap.chkPrice.DataBindings.Add("Text", dtbl, "PromptForPrice");
                rep_ProductSnap.chkActive.DataBindings.Add("Text", dtbl, "ProductStatus");
                rep_ProductSnap.rBrand.DataBindings.Add("Text", dtbl, "Brand");
                rep_ProductSnap.rCurrentCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_ProductSnap.rPriceA.DataBindings.Add("Text", dtbl, "PriceA");
                rep_ProductSnap.rPriceB.DataBindings.Add("Text", dtbl, "PriceB");
                rep_ProductSnap.rPriceC.DataBindings.Add("Text", dtbl, "PriceC");
                rep_ProductSnap.rOnHandQty.DataBindings.Add("Text", dtbl, "MinimumAge");
                rep_ProductSnap.rReorderQty.DataBindings.Add("Text", dtbl, "MinimumServiceTime");
                rep_ProductSnap.rNotes.DataBindings.Add("Text", dtbl, "ProductNotes");

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
            if (txtSearchGrdData.InfoText == "Search Services")
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
                txtSearchGrdData.InfoText = "Search Services";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Services") return;
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
