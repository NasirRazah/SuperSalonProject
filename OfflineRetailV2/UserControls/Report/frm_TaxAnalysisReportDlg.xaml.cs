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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_SalesSummaryReportDlg.xaml
    /// </summary>
    public partial class frm_TaxAnalysisReportDlg : Window
    {
        public frm_TaxAnalysisReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void ClickButton(string eventtype)
        {

            Cursor = Cursors.Wait;
            try
            {
                ExecuteSalesSummary(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteSalesSummary(string eventtype)
        {



            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }

            DataTable dtblRep = new DataTable();
            dtblRep.Columns.Add("ProductID", System.Type.GetType("System.Int32"));
            dtblRep.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblRep.Columns.Add("Qty", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Sales_Tax", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Discount", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Tax", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Sales_PreTax", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Cost", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
            dtblRep.Columns.Add("Profit", System.Type.GetType("System.Double"));

            PosDataObject.Sales objSale99 = new PosDataObject.Sales();
            objSale99.Connection = SystemVariables.Conn;
            DataTable dtbl = objSale99.FetchSaleBreakupData(GeneralFunctions.fnDate(dtFrom.EditValue.ToString()), GeneralFunctions.fnDate(dtTo.EditValue.ToString()), Settings.TaxInclusive);

            DataTable dTemp = dtbl.DefaultView.ToTable(true, "ProductID");

            foreach (DataRow dr in dTemp.Rows)
            {
                int pID = GeneralFunctions.fnInt32(dr["ProductID"].ToString());

                string desc = "";
                double qty = 0;
                double sales_tax = 0;
                double disc = 0;
                double tax = 0;
                double cost = 0;
                double dcost = 0;
                double sales_pretax = 0;
                double profit = 0;

                foreach (DataRow dr1 in dtbl.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr1["ProductID"].ToString()) == pID)
                    {
                        desc = dr1["Description"].ToString();

                        qty = qty + GeneralFunctions.fnDouble(dr1["Qty"].ToString());
                        disc = disc + GeneralFunctions.fnDouble(dr1["Discount"].ToString());
                        cost = cost + GeneralFunctions.fnDouble(dr1["Cost"].ToString());
                        dcost = dcost + GeneralFunctions.fnDouble(dr1["DiscountedCost"].ToString());
                        tax = tax + GeneralFunctions.fnDouble(dr1["Tax"].ToString());
                        sales_tax = sales_tax + GeneralFunctions.fnDouble(dr1["Sales_Tax"].ToString());
                        sales_pretax = sales_pretax + GeneralFunctions.fnDouble(dr1["Sales_PreTax"].ToString());
                        profit = profit + GeneralFunctions.fnDouble(dr1["Profit"].ToString());
                    }
                }

                dtblRep.Rows.Add(new object[] { pID, desc, qty, sales_tax, disc, tax, sales_pretax, cost, dcost, profit });
            }


            if (dtblRep.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Sales.repTaxAnalysis rep_SalesSummary = new OfflineRetailV2.Report.Sales.repTaxAnalysis();
            GeneralFunctions.MakeReportWatermark(rep_SalesSummary);
            rep_SalesSummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_SalesSummary.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_SalesSummary.rHeader.Text = "Tax Analysis Report";
            rep_SalesSummary.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
            rep_SalesSummary.DecimalPlace = Settings.DecimalPlace;
            rep_SalesSummary.Report.DataSource = dtblRep;

            rep_SalesSummary.rName.DataBindings.Add("Text", dtblRep, "Description");
            rep_SalesSummary.rQty.DataBindings.Add("Text", dtblRep, "Qty");
            rep_SalesSummary.rSTx.DataBindings.Add("Text", dtblRep, "Sales_Tax");
            rep_SalesSummary.rDiscount.DataBindings.Add("Text", dtblRep, "Discount");
            rep_SalesSummary.rSPTx.DataBindings.Add("Text", dtblRep, "Sales_PreTax");
            rep_SalesSummary.rCost.DataBindings.Add("Text", dtblRep, "Cost");
            rep_SalesSummary.rDCost.DataBindings.Add("Text", dtblRep, "DiscountedCost");
            rep_SalesSummary.rTax.DataBindings.Add("Text", dtblRep, "Tax");
            rep_SalesSummary.rProfit.DataBindings.Add("Text", dtblRep, "Profit");

            if (Settings.DecimalPlace == 3)
            {
                rep_SalesSummary.rTTot1.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot2.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot3.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot4.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot5.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot6.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot7.Summary.FormatString = "{0:0.000}";
                rep_SalesSummary.rTTot8.Summary.FormatString = "{0:0.000}";
            }
            else
            {
                rep_SalesSummary.rTTot1.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot2.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot3.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot4.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot5.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot6.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot7.Summary.FormatString = "{0:0.00}";
                rep_SalesSummary.rTTot8.Summary.FormatString = "{0:0.00}";
            }

            rep_SalesSummary.rTTot1.DataBindings.Add("Text", dtblRep, "Qty");
            rep_SalesSummary.rTTot2.DataBindings.Add("Text", dtblRep, "Sales_Tax");
            rep_SalesSummary.rTTot3.DataBindings.Add("Text", dtblRep, "Discount");
            rep_SalesSummary.rTTot4.DataBindings.Add("Text", dtblRep, "Sales_PreTax");
            rep_SalesSummary.rTTot5.DataBindings.Add("Text", dtblRep, "Cost");
            rep_SalesSummary.rTTot6.DataBindings.Add("Text", dtblRep, "Tax");
            rep_SalesSummary.rTTot7.DataBindings.Add("Text", dtblRep, "Profit");
            rep_SalesSummary.rTTot8.DataBindings.Add("Text", dtblRep, "DiscountedCost");



            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_SalesSummary.PrinterName = Settings.ReportPrinterName;
                    rep_SalesSummary.CreateDocument();
                    rep_SalesSummary.PrintingSystem.ShowMarginsWarning = false;
                    rep_SalesSummary.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_SalesSummary.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_SalesSummary;
                    window.ShowDialog();

                }
                finally
                {
                    rep_SalesSummary.Dispose();

                    dtbl.Dispose();
                    dtblRep.Dispose();
                }
            }


            if (eventtype == "Print")
            {
                rep_SalesSummary.CreateDocument();
                rep_SalesSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_SalesSummary);
                }
                finally
                {
                    rep_SalesSummary.Dispose();
                    dtbl.Dispose();
                    dtblRep.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_SalesSummary.CreateDocument();
                rep_SalesSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "tax_analysis.pdf";
                    GeneralFunctions.EmailReport(rep_SalesSummary, attachfile, "Tax Analysis");
                }
                finally
                {
                    rep_SalesSummary.Dispose();
                    dtbl.Dispose();
                    dtblRep.Dispose();
                }
            }

        }

        private double LayawaySales(int T, int I)
        {
            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objSales1.GetLayawaySalesPosted(T, I);
        }

        private double FSTender(int T)
        {
            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objSales1.GetFoodStampTendered(T);
        }

        private double GetTaxableSales(string invno, DataTable dt, double TR, double Q, double P, double NP, double discnt)
        {
            double val = 0;
            val = GeneralFunctions.fnDouble((Q * P) - discnt);
            return val;
        }

        private double GetTaxAmount(double TR, double val, double cperc)
        {
            return GeneralFunctions.fnDouble((val * TR / 100) * cperc);
        }

        private double GetInvoiceDiscount(string invno, DataTable dt)
        {
            double val = 0;
            foreach (DataRow drt in dt.Rows)
            {
                if ((drt["InvNo"].ToString() == invno) && (drt["ProductType"].ToString() != "G") && (drt["ProductType"].ToString() != "X"))
                {
                    val = GeneralFunctions.fnDouble(drt["Discount"].ToString());
                    break;
                }
            }
            return val;
        }

        private void GetTaxableSales1(string invno, DataTable dt, double TR, double discnt, ref double tx1, ref double tx2, ref double tx3)
        {
            double val = 0;
            double tot = 0;
            double tot1 = 0;
            double tot2 = 0;
            double tot3 = 0;
            double totn = 0;
            double disntx = 0;
            foreach (DataRow drt in dt.Rows)
            {
                if ((drt["InvNo"].ToString() == invno) && (drt["ProductType"].ToString() != "G") && (drt["ProductType"].ToString() != "X"))
                {
                    if ((drt["Taxable1"].ToString() == "N") && (drt["Taxable2"].ToString() == "N") && (drt["Taxable3"].ToString() == "N"))
                    {
                        totn = totn + GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["Price"].ToString());
                    }
                    if (drt["Taxable1"].ToString() == "Y")
                    {
                        /*if (GeneralFunctions.fnDouble(drt["Qty"].ToString()) != GeneralFunctions.fnDouble(drt["NormalPrice"].ToString()))
                            tot1 = tot1 + GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["Price"].ToString()) -
                                GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["NormalPrice"].ToString());
                        else*/
                        tot1 = tot1 + GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["Price"].ToString());
                    }
                    if (drt["Taxable2"].ToString() == "Y")
                        tot2 = tot2 + GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["Price"].ToString());
                    if (drt["Taxable3"].ToString() == "Y")
                        tot3 = tot3 + GeneralFunctions.fnDouble(drt["Qty"].ToString()) * GeneralFunctions.fnDouble(drt["Price"].ToString());
                }
            }

            if (totn > 0)
            {
                disntx = GeneralFunctions.fnDouble((discnt * totn) / (totn + tot1 + tot2 + tot3));
            }


            if (tot1 > 0)
                tx1 = tot1 - (discnt - disntx);

            if (tot2 > 0)
                tx2 = tot2 - (discnt - disntx);

            if (tot3 > 0)
                tx3 = tot3 - (discnt - disntx);

            /*
            if ((discnt - disntx) > tot1) tx1 = tot1 + (discnt - disntx);
            else tx1 = tot1 - (discnt - disntx);

            if ((discnt - disntx) > tot2) tx2 = tot2 + (discnt - disntx);
            else tx2 = tot2 - (discnt - disntx);

            if ((discnt - disntx) > tot3) tx3 = tot3 + (discnt - disntx);
            else tx3 = tot3 - (discnt - disntx);*/
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Tax Analysis Report";
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
