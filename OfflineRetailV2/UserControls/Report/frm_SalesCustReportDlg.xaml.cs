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
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_SalesCustReportDlg.xaml
    /// </summary>
    public partial class frm_SalesCustReportDlg : Window
    {
        public frm_SalesCustReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private DataTable dtblCustDT;
        private DateTime dtFromDT;
        private DateTime dtToDT;

        private int intTranType;

        

        public int iTranType
        {
            get { return intTranType; }
            set { intTranType = value; }
        }

        public DataTable CustDT
        {
            get { return dtblCustDT; }
            set { dtblCustDT = value; }
        }

        public DateTime FromDT
        {
            get { return dtFromDT; }
            set { dtFromDT = value; }
        }

        public DateTime ToDT
        {
            get { return dtToDT; }
            set { dtToDT = value; }
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void RgRep_1_Checked(object sender, RoutedEventArgs e)
        {
            if (rgRep_1.IsChecked == true)
            {
                lbsort1.Visibility = Visibility.Visible;
                rgsortdept_1.Visibility = rgsortdept_1.Visibility = Visibility.Visible;
            }
            else
            {
                lbsort1.Visibility = Visibility.Hidden;
                rgsortdept_1.Visibility = rgsortdept_1.Visibility = Visibility.Hidden;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (rgRep_4.IsChecked == false)
                    ExecuteReport();
                else
                    ExecuteSummary();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ChkSelectGrid_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSelectGrid.IsChecked == true) grdCustomer.SelectAll();
            else
            {
                grdCustomer.UnselectAll();
                gridView1.FocusedRowHandle = -1;
            }
        }

        private void ExecuteReport()
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }
            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();



            DataTable dtblS = new DataTable();
            dtblS = (grdCustomer.ItemsSource as DataTable).Clone();
            foreach(DataRowView dv in grdCustomer.SelectedItems)
            {
                dtblS.Rows.Add(new object[] { dv["ID"].ToString(), dv["CustomerID"].ToString(), dv["Company"].ToString(), dv["LastName"].ToString(),
                        dv["Name"].ToString(), dv["WorkPhone"].ToString(), dv["Tot"].ToString(), dv["Group"].ToString(),
                        dv["Class"].ToString()});

            }
            
            string strCustID = "";
            foreach (DataRow drS in dtblS.Rows)
            {
                if (strCustID == "") strCustID = strCustID + drS["ID"].ToString();
                else strCustID = strCustID + "," + drS["ID"].ToString();
            }
            dtblS.Dispose();

            // 
            string strType = "";
            if (rgRep_1.IsChecked == true) strType = "Department";
            if (rgRep_2.IsChecked == true) strType = "SKU";
            if (rgRep_3.IsChecked == true) strType = "Date";

            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            if (intTranType == 0) dtbl = objSales.FetchCustomerSaleReportData(strType, strCustID, dtFromDT, dtToDT);
            if (intTranType == 1) dtbl = objSales.FetchCustomerRentReportData(strType, strCustID, dtFromDT, dtToDT);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                dtbl.Dispose();
                return;
            }

            DataTable repdtbl = new DataTable();

            repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Season", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Department", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("CustomerAddress", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));

            double dblRev = 0;
            double dblCost = 0;
            double dblProfit = 0;

            foreach (DataRow dr in dtbl.Rows)
            {
                dblRev = 0;
                dblCost = 0;
                dblProfit = 0;
                dblRev = Settings.TaxInclusive == "N" ? (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString())) -
                           GeneralFunctions.fnDouble(dr["Discount"].ToString()) : (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["PriceI"].ToString()));
                dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());
                dblProfit = dblRev - dblCost;

                repdtbl.Rows.Add(new object[] {
                                                   dr["SKU"].ToString(),
                                                   dr["Description"].ToString(),
                                                   dr["Brand"].ToString(),
                                                   dr["UPC"].ToString(),
                                                   dr["Season"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   dblProfit,
                                                   dr["DeptID"].ToString(),
                                                   dr["Department"].ToString(),
                                                   dr["CustID"].ToString(),
                                                   dr["Customer"].ToString(),
                                                   dr["CustomerAddress"].ToString(),
                                                   dr["InvoiceNo"].ToString(),
                                                   dr["TransDate"].ToString()});
            }

            DataTable frepdtbl = new DataTable();

            frepdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("Season", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
            frepdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
            frepdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
            frepdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
            frepdtbl.Columns.Add("DeptID", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("Department", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("CustID", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("Customer", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("CustomerAddress", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            frepdtbl.Columns.Add("TransDate", System.Type.GetType("System.String"));

            string fsort1 = "";

            if (cmbsort.EditValue.ToString() == "SKU") if (rgsort1_1.IsChecked == true) fsort1 = "SKU asc"; else fsort1 = "SKU desc";
            if (cmbsort.EditValue.ToString() == "Description") if (rgsort1_1.IsChecked == true) fsort1 = "Description asc"; else fsort1 = "Description desc";
            if (cmbsort.EditValue.ToString() == "Family") if (rgsort1_1.IsChecked == true) fsort1 = "Brand asc"; else fsort1 = "Brand desc";
            if (cmbsort.EditValue.ToString() == "UPC") if (rgsort1_1.IsChecked == true) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
            if (cmbsort.EditValue.ToString() == "Season") if (rgsort1_1.IsChecked == true) fsort1 = "Season asc"; else fsort1 = "Season desc";
            if (cmbsort.EditValue.ToString() == "Date") if (rgsort1_1.IsChecked == true) fsort1 = "TransDate asc"; else fsort1 = "TransDate desc";
            if (cmbsort.EditValue.ToString() == "Qty Sold") if (rgsort1_1.IsChecked == true) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
            if (cmbsort.EditValue.ToString() == "Cost") if (rgsort1_1.IsChecked == true) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
            if (cmbsort.EditValue.ToString() == "Revenue") if (rgsort1_1.IsChecked == true) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
            if (cmbsort.EditValue.ToString() == "Profit") if (rgsort1_1.IsChecked == true) fsort1 = "Profit asc"; else fsort1 = "Profit desc";


            repdtbl.DefaultView.Sort = fsort1;
            repdtbl.DefaultView.ApplyDefaultSort = true;


            foreach (DataRowView dr in repdtbl.DefaultView)
            {
                frepdtbl.Rows.Add(new object[] {     dr["SKU"].ToString(),
                                                     dr["Description"].ToString(),
                                                     dr["Brand"].ToString(),
                                                     dr["UPC"].ToString(),
                                                     dr["Season"].ToString(),
                                                     dr["QtySold"].ToString(),
                                                     dr["Revenue"].ToString(),
                                                     dr["Cost"].ToString(),
                                                     dr["Profit"].ToString(),
                                                     dr["DeptID"].ToString(),
                                                     dr["Department"].ToString(),
                                                     dr["CustID"].ToString(),
                                                     dr["Customer"].ToString(),
                                                     dr["CustomerAddress"].ToString(),
                                                     dr["InvoiceNo"].ToString(),
                                                     dr["TransDate"].ToString()});
            }

            if (strType != "Department")
            {

                OfflineRetailV2.Report.Sales.repCustSales1 rep_CustSales1 = new OfflineRetailV2.Report.Sales.repCustSales1();

                rep_CustSales1.Report.DataSource = frepdtbl;

                GeneralFunctions.MakeReportWatermark(rep_CustSales1);
                rep_CustSales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CustSales1.rReportHeader.Text = Settings.ReportHeader_Address;
                if (intTranType == 0) rep_CustSales1.rHeader.Text = "Customer Sales";
                if (intTranType == 1) rep_CustSales1.rHeader.Text = "Customer Rent";
                rep_CustSales1.rDate.Text = "from" + " " + dtFromDT.ToString("d") + " " + "to"+ " " + dtToDT.ToString("d");
                rep_CustSales1.rCustomer.DataBindings.Add("Text", frepdtbl, "Customer");
                rep_CustSales1.rCustomerAddress.DataBindings.Add("Text", frepdtbl, "CustomerAddress");
                rep_CustSales1.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_CustSales1.rTot2.Summary.FormatString = "{0:0.000}";
                    rep_CustSales1.rTot3.Summary.FormatString = "{0:0.000}";
                    rep_CustSales1.rTot4.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_CustSales1.rTot2.Summary.FormatString = "{0:0.00}";
                    rep_CustSales1.rTot3.Summary.FormatString = "{0:0.00}";
                    rep_CustSales1.rTot4.Summary.FormatString = "{0:0.00}";
                }

                if (strType == "SKU") // SKU
                {
                    rep_CustSales1.rHeader.Text = rep_CustSales1.rHeader.Text + " - By SKU";
                    rep_CustSales1.GroupType = "S";
                }

                if (strType == "Date") // Date Time
                {
                    rep_CustSales1.rHeader.Text = rep_CustSales1.rHeader.Text + " - By Date";
                    rep_CustSales1.GroupType = "DT";
                }

                rep_CustSales1.GroupHeader1.GroupFields.Add(rep_CustSales1.CreateGroupField("CustID"));
                if (rgsort_1.IsChecked == true) rep_CustSales1.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                if (rgsort_2.IsChecked == true) rep_CustSales1.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
                rep_CustSales1.rSKU.DataBindings.Add("Text", frepdtbl, "SKU");
                rep_CustSales1.rProduct.DataBindings.Add("Text", frepdtbl, "Description");
                rep_CustSales1.rBrand.DataBindings.Add("Text", frepdtbl, "Brand");
                //rep_CustSales1.rUPC.DataBindings.Add("Text", frepdtbl, "UPC");
                //rep_CustSales1.rSeason.DataBindings.Add("Text", frepdtbl, "Season");
                if (intTranType == 1)
                {
                    rep_CustSales1.xrlbQ.Text = "Qty Rented";
                    rep_CustSales1.xrlbC.Visible = false;
                    rep_CustSales1.xrlbDC.Visible = false;
                    rep_CustSales1.xrlbR.Visible = false;
                    rep_CustSales1.xrlbP.Visible = false;
                    rep_CustSales1.xrlbQ.Width = 250;
                    rep_CustSales1.rCost.Visible = false;
                    rep_CustSales1.rDCost.Visible = false;
                    rep_CustSales1.rRevenue.Visible = false;
                    rep_CustSales1.rProfit.Visible = false;
                    rep_CustSales1.rQty.Width = 250;
                    rep_CustSales1.rTot2.Visible = false;
                    rep_CustSales1.rTot3.Visible = false;
                    rep_CustSales1.rTot4.Visible = false;
                    rep_CustSales1.rTot6.Visible = false;
                    rep_CustSales1.rTot1.Width = 250;
                }
                rep_CustSales1.rInvoice.DataBindings.Add("Text", frepdtbl, "InvoiceNo");
                rep_CustSales1.rDateTime.DataBindings.Add("Text", frepdtbl, "TransDate");
                rep_CustSales1.rQty.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_CustSales1.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_CustSales1.rDCost.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_CustSales1.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_CustSales1.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");

                rep_CustSales1.rTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_CustSales1.rTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_CustSales1.rTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_CustSales1.rTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_CustSales1.rTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                //rep_Sales1.rTot5.DataBindings.Add("Text", repdtbl, "Margin");

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CustSales1.PrinterName = Settings.ReportPrinterName;
                    rep_CustSales1.CreateDocument();
                    rep_CustSales1.PrintingSystem.ShowMarginsWarning = false;
                    rep_CustSales1.PrintingSystem.ShowPrintStatusDialog = false;
                    rep_CustSales1.ShowPreviewDialog();

                }
                finally
                {
                    rep_CustSales1.Dispose();

                    dtbl.Dispose();
                    repdtbl.Dispose();
                }
            }
            else
            {

                OfflineRetailV2.Report.Sales.repCustSales2 rep_CustSales2 = new OfflineRetailV2.Report.Sales.repCustSales2();

                rep_CustSales2.Report.DataSource = frepdtbl;
                GeneralFunctions.MakeReportWatermark(rep_CustSales2);
                rep_CustSales2.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CustSales2.rReportHeader.Text = Settings.ReportHeader_Address;
                if (intTranType == 0) rep_CustSales2.rHeader.Text = "Customer Sales";
                if (intTranType == 1) rep_CustSales2.rHeader.Text = "Customer Rent";
                rep_CustSales2.rDate.Text = dtFromDT.ToString("d") + "  " + "to" + "  " + dtToDT.ToString("d");
                rep_CustSales2.rCustomer.DataBindings.Add("Text", frepdtbl, "Customer");
                rep_CustSales2.rCustomerAddress.DataBindings.Add("Text", frepdtbl, "CustomerAddress");
                rep_CustSales2.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_CustSales2.rTot2.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rTot3.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rTot4.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rsTot2.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rsTot3.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rsTot4.Summary.FormatString = "{0:0.000}";
                    rep_CustSales2.rsTot6.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_CustSales2.rTot2.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rTot3.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rTot4.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rsTot2.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rsTot3.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rsTot4.Summary.FormatString = "{0:0.00}";
                    rep_CustSales2.rsTot6.Summary.FormatString = "{0:0.00}";
                }

                if (intTranType == 1)
                {
                    rep_CustSales2.xrlbQ.Text = "Qty Rented";
                    rep_CustSales2.xrlbC.Visible = false;
                    rep_CustSales2.xrlbDC.Visible = false;
                    rep_CustSales2.xrlbR.Visible = false;
                    rep_CustSales2.xrlbP.Visible = false;
                    rep_CustSales2.xrlbQ.Width = 250;
                    rep_CustSales2.rCost.Visible = false;
                    rep_CustSales2.rDCost.Visible = false;
                    rep_CustSales2.rRevenue.Visible = false;
                    rep_CustSales2.rProfit.Visible = false;
                    rep_CustSales2.rQty.Width = 250;
                    rep_CustSales2.rTot2.Visible = false;
                    rep_CustSales2.rTot3.Visible = false;
                    rep_CustSales2.rTot4.Visible = false;
                    rep_CustSales2.rTot6.Visible = false;
                    rep_CustSales2.rTot1.Width = 250;
                    rep_CustSales2.rsTot2.Visible = false;
                    rep_CustSales2.rsTot3.Visible = false;
                    rep_CustSales2.rsTot4.Visible = false;
                    rep_CustSales2.rsTot6.Visible = false;
                    rep_CustSales2.rsTot1.Width = 250;
                }

                rep_CustSales2.rDetSum.Text = "By Department";
                rep_CustSales2.GroupType = "DP";

                rep_CustSales2.GroupHeader1.GroupFields.Add(rep_CustSales2.CreateGroupField("CustID"));
                if (rgsort_1.IsChecked == true) rep_CustSales2.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                if (rgsort_2.IsChecked == true) rep_CustSales2.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

                rep_CustSales2.GroupHeader2.GroupFields.Add(rep_CustSales2.CreateGroupField("DeptID"));
                if (rgsortdept_1.IsChecked == true) rep_CustSales2.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
                if (rgsortdept_2.IsChecked == true) rep_CustSales2.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

                rep_CustSales2.rGroupID.DataBindings.Add("Text", frepdtbl, "DeptID");
                rep_CustSales2.rGroupDesc.DataBindings.Add("Text", frepdtbl, "Department");

                rep_CustSales2.rSKU.DataBindings.Add("Text", frepdtbl, "SKU");
                rep_CustSales2.rProduct.DataBindings.Add("Text", frepdtbl, "Description");
                rep_CustSales2.rDateTime.DataBindings.Add("Text", frepdtbl, "TransDate");
                rep_CustSales2.rQty.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_CustSales2.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_CustSales2.rDCost.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_CustSales2.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_CustSales2.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");

                rep_CustSales2.rTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_CustSales2.rTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_CustSales2.rTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_CustSales2.rTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_CustSales2.rTot4.DataBindings.Add("Text", frepdtbl, "Profit");

                rep_CustSales2.rsTot1.DataBindings.Add("Text", frepdtbl, "QtySold");
                rep_CustSales2.rsTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_CustSales2.rsTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_CustSales2.rsTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_CustSales2.rsTot4.DataBindings.Add("Text", frepdtbl, "Profit");

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CustSales2.PrinterName = Settings.ReportPrinterName;
                    rep_CustSales2.CreateDocument();
                    rep_CustSales2.PrintingSystem.ShowMarginsWarning = false;
                    rep_CustSales2.PrintingSystem.ShowPrintStatusDialog = false;
                    rep_CustSales2.ShowPreviewDialog();

                }
                finally
                {
                    rep_CustSales2.Dispose();

                    dtbl.Dispose();
                    repdtbl.Dispose();
                }

            }

        }


        private void ExecuteSummary()
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }
            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();


            DataTable repdtbl = new DataTable();
            repdtbl = CustDT;

            OfflineRetailV2.Report.Sales.repCustSalesSummary rep_CustSalesSummary = new OfflineRetailV2.Report.Sales.repCustSalesSummary();

            rep_CustSalesSummary.Report.DataSource = repdtbl;
            GeneralFunctions.MakeReportWatermark(rep_CustSalesSummary);
            rep_CustSalesSummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CustSalesSummary.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CustSalesSummary.rHeader.Text = "Customer Sales - Summary";
            rep_CustSalesSummary.rDate.Text = dtFromDT.ToString("d") + "  " + "To" + "  " + dtToDT.ToString("d");

            rep_CustSalesSummary.DecimalPlace = Settings.DecimalPlace;
            if (Settings.DecimalPlace == 3)
            {
                rep_CustSalesSummary.rTot3.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_CustSalesSummary.rTot3.Summary.FormatString = "{0:0.00}";
            }

            rep_CustSalesSummary.rCID.DataBindings.Add("Text", repdtbl, "CustomerID");
            rep_CustSalesSummary.rCompany.DataBindings.Add("Text", repdtbl, "Company");
            rep_CustSalesSummary.rName.DataBindings.Add("Text", repdtbl, "Name");
            rep_CustSalesSummary.rPhone.DataBindings.Add("Text", repdtbl, "WorkPhone");
            rep_CustSalesSummary.rSales.DataBindings.Add("Text", repdtbl, "Tot");
            rep_CustSalesSummary.rGroup.DataBindings.Add("Text", repdtbl, "Group");
            rep_CustSalesSummary.rClass.DataBindings.Add("Text", repdtbl, "Class");

            rep_CustSalesSummary.rTot3.DataBindings.Add("Text", repdtbl, "Tot");

            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                if (Settings.ReportPrinterName != "") rep_CustSalesSummary.PrinterName = Settings.ReportPrinterName;
                rep_CustSalesSummary.CreateDocument();
                rep_CustSalesSummary.PrintingSystem.ShowMarginsWarning = false;
                rep_CustSalesSummary.PrintingSystem.ShowPrintStatusDialog = false;
                rep_CustSalesSummary.ShowPreviewDialog();

            }
            finally
            {
                rep_CustSalesSummary.Dispose();


                repdtbl.Dispose();
            }

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Sales By Customer";
            

            DataTable dtblsort = new DataTable();
            dtblsort.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblsort.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblsort.Rows.Add(new object[] { "SKU","SKU" });
            dtblsort.Rows.Add(new object[] { "Description", "Description" });
            dtblsort.Rows.Add(new object[] { "Family", "Family" });
            dtblsort.Rows.Add(new object[] { "UPC", "UPC" });
            dtblsort.Rows.Add(new object[] { "Season","Season" });
            dtblsort.Rows.Add(new object[] { "Date", "Date" });
            dtblsort.Rows.Add(new object[] { "Qty Sold", "Qty Sold" });
            dtblsort.Rows.Add(new object[] { "Cost", "Cost" });
            dtblsort.Rows.Add(new object[] { "Revenue", "Revenue" });
            dtblsort.Rows.Add(new object[] { "Profit", "Profit" });
            cmbsort.ItemsSource = dtblsort;

            dtblsort.Dispose();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblCustDT.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdCustomer.ItemsSource = dtblTemp;
            rgRep_1.IsChecked = true;
            rgsort_1.IsChecked = true;
            lbsort1.Visibility = Visibility.Visible;
            rgsortdept_1.Visibility = rgsortdept_1.Visibility = Visibility.Visible;
            rgsortdept_1.IsChecked = true;
            cmbsort.EditValue = "SKU";
            rgsort1_1.IsChecked = true;
        }

        private void Cmbsort_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
