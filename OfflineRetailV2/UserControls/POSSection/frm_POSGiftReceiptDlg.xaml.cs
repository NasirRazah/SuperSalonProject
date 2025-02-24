using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using pos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSGiftReceiptDlg.xaml
    /// </summary>
    public partial class frm_POSGiftReceiptDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_POSGiftReceiptDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSGiftReceiptDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
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
            if (Settings.UseTouchKeyboardInPOS == "N") return;
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


        private async void Frm_POSGiftReceiptDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Print_Gift_Receipt;
            
            if (Settings.DecimalPlace == 3)
            {
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                GeneralFunctions.SetDecimal(numF, 3);
                GeneralFunctions.SetDecimal(numT, 3);
            }
            else
            {
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                GeneralFunctions.SetDecimal(numF, 2);
                GeneralFunctions.SetDecimal(numT, 2);
            }
            dtF.EditValue = DateTime.Today.Date;
            dtT.EditValue = DateTime.Today.Date;

            lbReceipt.Text = "";
            lbCustCompany.Text = "";
            lbCustName.Text = "";
            lbCustID.Text = "";

            cmbDate.SelectedIndex = 0;
            cmbAmount.SelectedIndex = 0;
            //pictureBox1.Visible = false;
            //pictureBox2.Visible = false;
            dtF.Visibility = Visibility.Collapsed;
            dtT.Visibility = Visibility.Collapsed;
            lbDate.Visibility = Visibility.Collapsed;

            numF.Visibility = Visibility.Collapsed;
            numT.Visibility = Visibility.Collapsed;
            lbAmt.Visibility = Visibility.Collapsed;
            if (Settings.CentralExportImport == "N")
            {
                cmbStore.Visibility = Visibility.Collapsed;
            }
            else
            {
                cmbStore.Visibility = Visibility.Visible;
                PopulateCustomerStores();
            }
            PopulateCustomer();
            PopulateSKU(0);
            PopulateEmployee();
            await FetchHeaderData();
            blFetch = true;

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }

            if (intMainScreenCustID != 0)
            {
                LookUpEdit clkup = new LookUpEdit();
                //foreach (Control c in panel1.Controls)
                //{
                //    if (c is LookUpEdit)
                //    {
                //        if (c.Name == "cmbC") clkup = (LookUpEdit)c;
                //    }
                //}
                if (Settings.CentralExportImport == "Y")
                {
                    PosDataObject.POS objPOS = new PosDataObject.POS();
                    objPOS.Connection = SystemVariables.Conn;
                    string tstr = objPOS.FetchCustomerIssueStore(intMainScreenCustID);
                    if (tstr != Settings.StoreCode) cmbStore.Text = tstr;
                }
                clkup.EditValue = intMainScreenCustID.ToString();
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        

        private int intCustID;
        private bool blFetch = false;
        private int PrevInv = 0;
        private int CurrInv = 0;
        private int intMainScreenCustID;
        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }
        public int MainScreenCustID
        {
            get { return intMainScreenCustID; }
            set { intMainScreenCustID = value; }
        }

        public void PopulateCustomerStores()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupCustomerStore();
            cmbStore.Items.Clear();
            foreach (DataRow dr in dbtblCust.Rows)
            {
                cmbStore.Items.Add(dr["IssueStore"].ToString());
            }
            dbtblCust.Dispose();
            cmbStore.Text = Settings.StoreCode;
        }


        public void PopulateCustomer()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            objCustomer.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupData(cmbStore.Text);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCust.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbC.ItemsSource = dtblTemp;

            cmbC.EditValue = "0";

            dbtblCust.Dispose();
        }

        public void PopulateSKU(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchMLookupData(intOption);
            dbtblSKU.Rows.Add(new object[] { "0", Properties.Resources.All, Properties.Resources.All, "", "" });

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

            cmbP.ItemsSource = dtblTemp;
            cmbP.EditValue = "0";

            dbtblSKU.Dispose();
        }

        public void PopulateEmployee()
        {
            PosDataObject.Employee objProduct = new PosDataObject.Employee();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.DataObjectCulture_All = Settings.DataObjectCulture_All;
            objProduct.DataObjectCulture_ADMIN = Settings.DataObjectCulture_ADMIN;
            objProduct.DataObjectCulture_Administrator = Settings.DataObjectCulture_Administrator;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.LookupEmployee();

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

            cmbE.ItemsSource = dtblTemp;
            cmbE.EditValue = "0";
            dbtblSKU.Dispose();
        }

        private async void LookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void LookupEditE_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }


        private void cmbC_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void cmbP_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void cmbE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private async Task FetchHeaderData()
        {
            

            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchReprintData(0, GeneralFunctions.fnInt32(cmbP.EditValue.ToString()),
                   GeneralFunctions.fnInt32(cmbC.EditValue.ToString()), cmbDate.SelectedIndex, dtF.DateTime, dtT.DateTime,
                   cmbAmount.SelectedIndex, Convert.ToDouble(numF.Text), Convert.ToDouble(numT.Text), GeneralFunctions.GetCloseOutID(), cmbStore.Text, GeneralFunctions.fnInt32(cmbE.EditValue.ToString()));

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

            grdHeader.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }
        }

        private async Task FetchDetailData(int INV)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchGCReceiptRecord(INV);

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

            grdDetail.ItemsSource = dtblTemp;

            colchk.Visible = dtbl.Rows.Count > 1;

            string RNO = "";
            string CC = "";
            string CI = "";
            string CN = "";
            string CID = "";
            RNO = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv);
            CC = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCC);
            CI = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCustID);
            CN = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCust);
            CID = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID);
            lbReceipt.Text = Properties.Resources.Receipt_Number + RNO;

            if (CID != "0")
            {
                lbCustCompany.Text = Properties.Resources.Company + CC;
                lbCustName.Text = Properties.Resources.Customer + CN;
                lbCustID.Text = Properties.Resources.Customer_ID + CI;
            }
            else
            {
                lbCustCompany.Text = "";
                lbCustName.Text = "";
                lbCustID.Text = "";
            }
            dtbl.Dispose();
            dtblTemp.Dispose();
            bool flag = false;
            foreach (DataRow drt in dtbl.Rows)
            {
                if (drt["ProductType"].ToString() == "T")
                {
                    flag = true;
                    break;
                }
            }
        }

        private async void cmbDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbDate.SelectedIndex == 0)
            {
                //pictureBox1.Visible = false;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Collapsed;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if ((cmbDate.SelectedIndex == 1) || (cmbDate.SelectedIndex == 2))
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if (cmbDate.SelectedIndex == 3)
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = true;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Visible;
                lbDate.Visibility = Visibility.Visible;
            }
            if (blFetch) await FetchHeaderData();
        }

        private async void dtF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtF.EditValue == null) return;
            if (blFetch) await FetchHeaderData();
        }

        private async void dtT_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtT.EditValue.ToString() == "") return;
            if (blFetch) await FetchHeaderData();
        }

        private async void cmbAmount_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbAmount.SelectedIndex == 0)
            {
                numF.Visibility = Visibility.Collapsed;
                numT.Visibility = Visibility.Collapsed;
                lbAmt.Visibility = Visibility.Collapsed;
            }
            if ((cmbAmount.SelectedIndex == 1) || (cmbAmount.SelectedIndex == 2))
            {
                numF.Visibility = Visibility.Visible;
                numT.Visibility = Visibility.Collapsed;
                lbAmt.Visibility = Visibility.Collapsed;
            }
            if (cmbAmount.SelectedIndex == 3)
            {
                numF.Visibility = Visibility.Visible;
                numT.Visibility = Visibility.Visible;
                lbAmt.Visibility = Visibility.Visible;
            }
            if (blFetch) await FetchHeaderData();
        }

        private async void numF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void gridView2_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                if (blFetch)
                    await FetchDetailData(GeneralFunctions.fnInt32(
                        await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));


                CurrInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
                if (PrevInv == 0)
                {
                    PrevInv = CurrInv;
                }
            }
        }

        private async void gridView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                int INV = CurrInv;
                int GCID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colID));

                /*
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/

                PrintInvoice(INV, GCID);
            }
        }
        private void PrintInvoice(int intINV, int intGCID)
        {
            if (Settings.GeneralReceiptPrint == "N")
            {
                /*
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/
                blurGrid.Visibility = Visibility.Visible;
                frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.PrintType = "Gift Receipt";
                    frm_POSInvoicePrintDlg.InvNo = intINV;
                    frm_POSInvoicePrintDlg.GCID = intGCID;
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                DataTable dtbl = new DataTable();
                PosDataObject.POS objPOS1 = new PosDataObject.POS();
                objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                DataTable dlogo = new DataTable();
                objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dlogo = objPOS1.FetchStoreLogo();
                bool boolnulllogo = false;
                foreach (DataRow drl1 in dtbl.Rows)
                {
                    foreach (DataRow drl2 in dlogo.Rows)
                    {
                        if (drl2["logo"] == null) boolnulllogo = true;
                        drl1["Logo"] = drl2["logo"];
                    }
                }

                int intTranNo = 0;
                double dblOrderTotal = 0;
                double dblOrderSubtotal = 0;
                double dblDiscount = 0;
                double dblTax = 0;
                double dblSurcharge = 0;
                int intCID = 0;
                string strDiscountReason = "";
                double dblTax1 = 0;
                double dblTax2 = 0;
                double dblTax3 = 0;
                string strTaxNM1 = "";
                string strTaxNM2 = "";
                string strTaxNM3 = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                    intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                    dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                    dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                    dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                    dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                    dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                    dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                    strTaxNM1 = dr["TaxNM1"].ToString();
                    strTaxNM2 = dr["TaxNM2"].ToString();
                    strTaxNM3 = dr["TaxNM3"].ToString();

                    strDiscountReason = dr["DiscountReason"].ToString();
                }

                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();
                DataTable dtbl3 = new DataTable();
                DataTable dtbl4 = new DataTable();

                OfflineRetailV2.Report.Sales.repInvMain rep_InvMain = new OfflineRetailV2.Report.Sales.repInvMain();
                OfflineRetailV2.Report.Sales.repInvHeader1 rep_InvHeader1 = new OfflineRetailV2.Report.Sales.repInvHeader1();
                OfflineRetailV2.Report.Sales.repInvHeader2 rep_InvHeader2 = new OfflineRetailV2.Report.Sales.repInvHeader2();
                OfflineRetailV2.Report.Sales.repInvLine rep_InvLine = new OfflineRetailV2.Report.Sales.repInvLine();

                rep_InvMain.rReprint.Text = "**** " + Properties.Resources.Gift_Receipt + " ****";

                if (Settings.ReceiptFooter == "")
                {
                    rep_InvMain.rReportFooter.HeightF = 1.0f;
                    rep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 2);
                    rep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 5);
                    rep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 5);

                    rep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 25);
                    rep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 25);
                    rep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 25);
                    rep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 25);

                    rep_InvMain.ReportFooter.Height = 60;
                    rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                }
                else
                {
                    rep_InvMain.ReportFooter.Height = 91;
                    rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                }
                rep_InvMain.ReportFooter.PrintAtBottom = false;
                rep_InvMain.subrepH1.ReportSource = rep_InvHeader1;
                rep_InvHeader1.Report.DataSource = dtbl;
                rep_InvHeader1.rReprint.Text = "";
                GeneralFunctions.MakeReportWatermark(rep_InvMain);
                rep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                rep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;


                rep_InvHeader1.rType.Text = "";
                rep_InvHeader1.rOrderNo.Text = intINV.ToString();
                if (Settings.PrintLogoInReceipt == "Y")
                {
                    if (!boolnulllogo) rep_InvHeader1.rPic.DataBindings.Add("Image", dtbl, "Logo");
                }
                rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "TransDate");

                if (intCID > 0)
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                    rep_InvHeader2.rCustName.DataBindings.Add("Text", dtbl, "CustName");
                    rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "CustCompany");
                }
                else
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustName.Text = "";
                    rep_InvHeader2.rCustID.Text = "";
                    rep_InvHeader2.rCompany.Text = "";
                    rep_InvHeader2.rlCustName.Text = "";
                    rep_InvHeader2.rlCustID.Text = "";
                    rep_InvHeader2.rlCompany.Text = "";
                }

                PosDataObject.POS objPOS2 = new PosDataObject.POS();
                objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objPOS2.FetchGCReceiptData(intGCID);
                rep_InvMain.subrepLine.ReportSource = rep_InvLine;
                rep_InvLine.xrTable1.Visible = false;
                rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                rep_InvLine.Report.DataSource = dtbl1;
                rep_InvLine.rlSKU.DataBindings.Add("Text", dtbl1, "Qty");
                rep_InvLine.rlIem.DataBindings.Add("Text", dtbl1, "SKU");
                rep_InvLine.rlIem.Width = 150;
                rep_InvLine.rlqty.Width = 600;
                rep_InvLine.rlqty.DataBindings.Add("Text", dtbl1, "Description");
                rep_InvLine.rlPrice.Visible = false;
                rep_InvLine.rlDiscount.Visible = false;
                rep_InvLine.rlSurcharge.Visible = false;
                rep_InvLine.rlTotal.Visible = false;
                rep_InvLine.rDiscTxt.Text = "";

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_InvMain.PrinterName = Settings.ReportPrinterName;
                    rep_InvMain.CreateDocument();
                    rep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                    rep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_InvMain.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_InvMain;
                    window.ShowDialog();

                }
                finally
                {
                    rep_InvHeader1.Dispose();
                    rep_InvHeader2.Dispose();
                    rep_InvLine.Dispose();
                    rep_InvMain.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                }

            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            gridView1.PostEditor();
            foreach (DataRow dr in (grdDetail.ItemsSource as DataTable).Rows)
            {
                if (!Convert.ToBoolean(dr["PrintChecked"].ToString())) return;
                int INV = CurrInv;
                int GCID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                /*
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/
                PrintInvoice(INV, GCID);
            }
        }

        private async void cmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            PopulateCustomer();
            if (blFetch) await FetchHeaderData();
        }

        private async void CmbE_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void CmbC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void CmbP_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void CmbStore_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbP_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtF_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdHeader.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView2.FocusedRowHandle == 0) return;
            gridView2.FocusedRowHandle = gridView2.FocusedRowHandle - 1;
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdHeader.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView2.FocusedRowHandle == (grdHeader.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView2.FocusedRowHandle = gridView2.FocusedRowHandle + 1;
        }

        private void BtnUpDetail_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDetail.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == 0) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        private void BtnDownDetail_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDetail.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == (grdDetail.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
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
