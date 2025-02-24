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
    public partial class frm_SalesSummaryReportDlg : Window
    {
        public frm_SalesSummaryReportDlg()
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

            DataTable dtblRep = new DataTable();
            dtblRep.Columns.Add("Style", System.Type.GetType("System.Int32"));
            dtblRep.Columns.Add("Title", System.Type.GetType("System.String"));
            dtblRep.Columns.Add("Value", System.Type.GetType("System.String"));

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                return;
            }

            double dblSales = 0;
            double dblTaxCollected1 = 0;
            double dblTaxCollected2 = 0;
            double dblTaxCollected3 = 0;

            double dblInvDiscount = 0;
            double dblItemDiscount = 0;
            double dblGCSales = 0;
            double dblMcryGCSales = 0;
            double dblPcdiaGCSales = 0;
            double dblProductSales = 0;
            double dblServiceSales = 0;
            double dblBlankItemSales = 0;    /*****/
            double dblRentSales = 0;
            double dblRepairSales = 0;
            double dblCostOfGoods = 0;
            string lbTx1 = "";
            string lbTx2 = "";
            string lbTx3 = "";

            double dblTaxableSales1 = 0;
            double dblTaxableSales2 = 0;
            double dblTaxableSales3 = 0;
            double dblNonTaxableSales = 0;
            double dblLayawaySalesPosted = 0;
            double dblFoodStampTendered = 0;  /*****/

            double dblFees = 0;

            double dblSaleFees = 0;
            double dblSaleFeesTax = 0;

            double dblRNFees = 0;
            double dblRNFeesTax = 0;

            double dblRPFees = 0;
            double dblRPFeesTax = 0;

            double dblPTax1 = 0;
            double dblPTax2 = 0;
            double dblPTax3 = 0;

            double dblRNInvDiscount = 0;
            double dblRPInvDiscount = 0;

            double dblPItemDiscount = 0;
            double dblSItemDiscount = 0;
            double dblBItemDiscount = 0;
            double dblRNItemDiscount = 0;
            double dblRPItemDiscount = 0;

            double dblSTax1 = 0;
            double dblSTax2 = 0;
            double dblSTax3 = 0;

            double dblSTax11 = 0;
            double dblSTax21 = 0;
            double dblSTax31 = 0;

            double dblBTax1 = 0;
            double dblBTax2 = 0;
            double dblBTax3 = 0;

            double dblRNTax1 = 0;
            double dblRNTax2 = 0;
            double dblRNTax3 = 0;

            double dblRPTax1 = 0;
            double dblRPTax2 = 0;
            double dblRPTax3 = 0;

            double dblTot1 = 0;
            double dblTot2 = 0;

            string strTax1 = "";
            string strTax2 = "";
            string strTax3 = "";

            string strRntTax1 = "";
            string strRntTax2 = "";
            string strRntTax3 = "";

            string strRprTax1 = "";
            string strRprTax2 = "";
            string strRprTax3 = "";

            bool blRentSalesExists = false;
            bool blRepairSalesExists = false;

            double dblRepairDeposit = 0;

            double TotalDTax = 0;

            double MercuryGiftCardSales = 0;
            double PrecidiaGiftCardSales = 0;
            double DatacapGiftCardSales = 0;
            double POSLinkGiftCardSales = 0;

            double dblBottleRefund = 0;

            int intFreeQty = 0;
            double dblFreeAmount = 0;

            double dblPaidout = 0;
            double dblLottoPayout = 0;
            double dblGiftAid = 0;

            PosDataObject.Sales objSale99 = new PosDataObject.Sales();
            objSale99.Connection = SystemVariables.Conn;
            objSale99.ExecSalesSummaryProcedure(GeneralFunctions.fnDate(dtFrom.EditValue.ToString()), GeneralFunctions.fnDate(dtTo.EditValue.ToString()), GeneralFunctions.GetHostName(), Settings.TaxInclusive);

            DataTable dtblNew = objSale99.FetchSalesSummaryData(GeneralFunctions.GetHostName());

            DataTable dtblTender_Sales = objSale99.FetchSalesSummaryDataTender_Sales(GeneralFunctions.GetHostName());
            DataTable dtblTender_Paidout = objSale99.FetchSalesSummaryDataTender_Paidout(GeneralFunctions.GetHostName());

            double dblPSB = 0;
            double dblPSBTax = 0;
            foreach (DataRow dr in dtblNew.Rows)
            {
                dblProductSales = GeneralFunctions.fnDouble(dr["ProductSales"].ToString());
                dblServiceSales = GeneralFunctions.fnDouble(dr["ServiceSales"].ToString());
                dblBlankItemSales = GeneralFunctions.fnDouble(dr["OtherSales"].ToString());

                dblSTax1 = GeneralFunctions.fnDouble(dr["Tax1_Sales"].ToString());
                dblSTax2 = GeneralFunctions.fnDouble(dr["Tax2_Sales"].ToString());
                dblSTax3 = GeneralFunctions.fnDouble(dr["Tax3_Sales"].ToString());

                strTax1 = dr["TaxName1_Sales"].ToString();
                strTax2 = dr["TaxName2_Sales"].ToString();
                strTax3 = dr["TaxName3_Sales"].ToString();

                TotalDTax = GeneralFunctions.fnDouble(dr["DestinationTax"].ToString());

                lbTx1 = dr["TaxableSales_Info_1"].ToString();
                lbTx2 = dr["TaxableSales_Info_2"].ToString();
                lbTx3 = dr["TaxableSales_Info_3"].ToString();

                dblTaxableSales1 = GeneralFunctions.fnDouble(dr["TaxableSales_1"].ToString());
                dblTaxableSales2 = GeneralFunctions.fnDouble(dr["TaxableSales_2"].ToString());
                dblTaxableSales3 = GeneralFunctions.fnDouble(dr["TaxableSales_3"].ToString());

                dblPTax1 = GeneralFunctions.fnDouble(dr["P_Tax1"].ToString());
                dblPTax2 = GeneralFunctions.fnDouble(dr["P_Tax2"].ToString());
                dblPTax3 = GeneralFunctions.fnDouble(dr["P_Tax3"].ToString());

                dblSTax11            = GeneralFunctions.fnDouble(dr["S_Tax1"].ToString());
                dblSTax21            = GeneralFunctions.fnDouble(dr["S_Tax2"].ToString());
                dblSTax31            = GeneralFunctions.fnDouble(dr["S_Tax3"].ToString());

                dblBTax1 = GeneralFunctions.fnDouble(dr["B_Tax1"].ToString());
                dblBTax2 = GeneralFunctions.fnDouble(dr["B_Tax2"].ToString());
                dblBTax3 = GeneralFunctions.fnDouble(dr["B_Tax3"].ToString());

                dblNonTaxableSales = GeneralFunctions.fnDouble(dr["NonTaxableSales"].ToString());




                dblItemDiscount = GeneralFunctions.fnDouble(dr["ProductDiscount"].ToString());
                dblSItemDiscount = GeneralFunctions.fnDouble(dr["ServiceDiscount"].ToString());
                dblBItemDiscount = GeneralFunctions.fnDouble(dr["OtherDiscount"].ToString());
                dblRPItemDiscount = GeneralFunctions.fnDouble(dr["RepairDiscount"].ToString());

                dblInvDiscount = GeneralFunctions.fnDouble(dr["TicketDiscount_Sales"].ToString());
                dblRNInvDiscount = GeneralFunctions.fnDouble(dr["TicketDiscount_Rent"].ToString());
                dblRPInvDiscount = GeneralFunctions.fnDouble(dr["TicketDiscount_Repair"].ToString());

                dblSaleFees = GeneralFunctions.fnDouble(dr["Fees_Sales"].ToString());
                dblSaleFeesTax = GeneralFunctions.fnDouble(dr["TaxOnFees_Sales"].ToString());
                dblRNFees = GeneralFunctions.fnDouble(dr["Fees_Rent"].ToString());
                dblRNFeesTax = GeneralFunctions.fnDouble(dr["TaxOnFees_Rent"].ToString());
                dblRPFees = GeneralFunctions.fnDouble(dr["Fees_Repair"].ToString());
                dblRPFeesTax = GeneralFunctions.fnDouble(dr["TaxOnFees_Repair"].ToString());


                blRentSalesExists = GeneralFunctions.fnInt32(dr["RentExist"].ToString()) > 0;

                dblRentSales = GeneralFunctions.fnDouble(dr["RentSales"].ToString());
                strRntTax1 = dr["TaxName1_Rent"].ToString();
                strRntTax2 = dr["TaxName2_Rent"].ToString();
                strRntTax3 = dr["TaxName3_Rent"].ToString();
                dblRNTax1 = GeneralFunctions.fnDouble(dr["Tax1_Rent"].ToString());
                dblRNTax2 = GeneralFunctions.fnDouble(dr["Tax2_Rent"].ToString());
                dblRNTax3 = GeneralFunctions.fnDouble(dr["Tax3_Rent"].ToString());

                blRepairSalesExists = GeneralFunctions.fnInt32(dr["RepairExist"].ToString()) > 0;
                dblRepairSales = GeneralFunctions.fnDouble(dr["RepairSales"].ToString());
                strRprTax1 = dr["TaxName1_Repair"].ToString();
                strRprTax2 = dr["TaxName2_Repair"].ToString();
                strRprTax3 = dr["TaxName3_Repair"].ToString();
                dblRPTax1 = GeneralFunctions.fnDouble(dr["Tax1_Repair"].ToString());
                dblRPTax2 = GeneralFunctions.fnDouble(dr["Tax2_Repair"].ToString());
                dblRPTax3 = GeneralFunctions.fnDouble(dr["Tax3_Repair"].ToString());

                dblRepairDeposit = GeneralFunctions.fnDouble(dr["Repair_Deposit"].ToString());


                dblFoodStampTendered = GeneralFunctions.fnDouble(dr["FS_Tender"].ToString());
                dblLayawaySalesPosted = GeneralFunctions.fnDouble(dr["Layaway_Sales"].ToString());
                dblGCSales = GeneralFunctions.fnDouble(dr["GC_Sales"].ToString());
                MercuryGiftCardSales = GeneralFunctions.fnDouble(dr["MGC_Sales"].ToString());
                PrecidiaGiftCardSales = GeneralFunctions.fnDouble(dr["PGC_Sales"].ToString());
                DatacapGiftCardSales = GeneralFunctions.fnDouble(dr["DGC_Sales"].ToString());
                POSLinkGiftCardSales = GeneralFunctions.fnDouble(dr["PLGC_Sales"].ToString());
                dblBottleRefund = GeneralFunctions.fnDouble(dr["BootleRefund"].ToString());
                dblCostOfGoods = GeneralFunctions.fnDouble(dr["CostOfGoods"].ToString());
                intFreeQty = GeneralFunctions.fnInt32(dr["Free_Qty"].ToString());
                dblFreeAmount = GeneralFunctions.fnDouble(dr["Free_Amount"].ToString());
                dblPaidout = GeneralFunctions.fnDouble(dr["Paidout"].ToString());
                dblLottoPayout = GeneralFunctions.fnDouble(dr["LottoPayout"].ToString());

                dblGiftAid = GeneralFunctions.fnDouble(dr["GiftAid"].ToString());
            }

            DataTable dtbl = new DataTable();
            DataTable dtbl1 = new DataTable();

            dblPSB = dblProductSales + dblServiceSales + dblBlankItemSales;
            dblPSBTax = dblPTax1 + dblPTax2 + dblPTax3 + dblSTax11 + dblSTax21 + dblSTax31 + dblBTax1 + dblBTax2 + dblBTax3;

            dblTot1 = dblProductSales + dblServiceSales + dblBlankItemSales + dblPaidout + dblLottoPayout + dblGiftAid + (Settings.TaxInclusive == "N" ? (dblSTax1 + dblSTax2 + dblSTax3) : 0);

            double discItem = dblItemDiscount + dblSItemDiscount + dblBItemDiscount + dblRPItemDiscount;

            double discInv = dblInvDiscount + dblRNInvDiscount + dblRPInvDiscount;

            dblFees = dblSaleFees + dblSaleFeesTax + dblRNFees + dblRNFeesTax + dblRPFees + dblRPFeesTax;

            dtblRep.Rows.Add(new object[] { 1, "SALES", "" });
            dtblRep.Rows.Add(new object[] { 2, "Product Sales", dblProductSales.ToString() });
            dtblRep.Rows.Add(new object[] { 2, "Service Sales", dblServiceSales.ToString() });
            dtblRep.Rows.Add(new object[] { 2, "Other Sales", dblBlankItemSales.ToString() });

            dtblRep.Rows.Add(new object[] { 2, "Total Sales", dblPSB.ToString() });
            if (Settings.TaxInclusive == "N")
                dtblRep.Rows.Add(new object[] { 2, "Tax on Total Sales", dblPSBTax.ToString() });

            foreach (DataRow drt1 in dtblTender_Sales.Rows)
            {
                dtblRep.Rows.Add(new object[] { 2, "  " + drt1["TenderName"].ToString(), drt1["TenderAmount"].ToString() });
            }

            if (dblPaidout != 0)
            {
                dtblRep.Rows.Add(new object[] { 2, "Paid Out", dblPaidout.ToString() });

                foreach (DataRow drt1 in dtblTender_Paidout.Rows)
                {
                    dtblRep.Rows.Add(new object[] { 2, "  " + drt1["TenderName"].ToString(), drt1["TenderAmount"].ToString() });
                }

            }

            if (dblLottoPayout != 0)
            {
                dtblRep.Rows.Add(new object[] { 2, "Lotto Payout", dblLottoPayout.ToString() });
            }

            if (Settings.TaxInclusive == "N")
            {
                if (dblSTax1 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strTax1, dblSTax1.ToString() });
                if (dblSTax2 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strTax2, dblSTax2.ToString() });
                if (dblSTax3 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strTax3, dblSTax3.ToString() });
                if (TotalDTax != 0)
                {
                    dtblRep.Rows.Add(new object[] { 9, "", "" });
                    dtblRep.Rows.Add(new object[] { 0, "     " + "Destination Tax", TotalDTax.ToString() });
                }

                dtblRep.Rows.Add(new object[] { 2, "", "______________" });

                dtblRep.Rows.Add(new object[] { 2, "     " + "Total", dblTot1.ToString("f2") });
                dtblRep.Rows.Add(new object[] { 9, "", "" });

                if (dblTaxableSales1 != 0)
                {
                    dtblRep.Rows.Add(new object[] { 2, lbTx1, dblTaxableSales1.ToString("f2") });
                    //dtblRep.Rows.Add(new object[] { 0, "     Tax Collected", (dblPTax1 + dblSTax1 + dblBTax1).ToString() });
                }
                if (dblTaxableSales2 != 0)
                {
                    dtblRep.Rows.Add(new object[] { 2, lbTx2, dblTaxableSales2.ToString("f2") });
                    //dtblRep.Rows.Add(new object[] { 0, "     Tax Collected", (dblPTax2 + dblSTax2 + dblBTax2).ToString() });
                }
                if (dblTaxableSales3 != 0)
                {
                    dtblRep.Rows.Add(new object[] { 2, lbTx3, dblTaxableSales3.ToString("f2") });
                    //dtblRep.Rows.Add(new object[] { 0, "     Tax Collected", (dblPTax3 + dblSTax3 + dblBTax3).ToString() });
                }


                if (dblNonTaxableSales != 0) dtblRep.Rows.Add(new object[] { 2, "Non Taxable Sales", dblNonTaxableSales.ToString("f2") });

                if (dblNonTaxableSales != 0) dtblRep.Rows.Add(new object[] { 9, "", "" });
            }


            if (Settings.TaxInclusive == "Y")
            {
                dtblRep.Rows.Add(new object[] { 2, "", "______________" });

                dtblRep.Rows.Add(new object[] { 2, "     " + "Total", dblTot1.ToString("f2") });

                dtblRep.Rows.Add(new object[] { 9, "", "" });

                dtblRep.Rows.Add(new object[] { 11, "Tax Details", "" });

                if (dblSTax1 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strTax1, dblSTax1.ToString("f2") });
                if (dblSTax2 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strTax2, dblSTax2.ToString("f2") });
                if (dblSTax3 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strTax3, dblSTax3.ToString("f2") });
                if (TotalDTax != 0)
                {
                    dtblRep.Rows.Add(new object[] { 9, "", "" });
                    dtblRep.Rows.Add(new object[] { 2, "Destination Tax", TotalDTax.ToString("f2") });
                }
            }

            if ((!blRentSalesExists) && (!blRepairSalesExists))
            {
                dtblRep.Rows.Add(new object[] { 9, "", "" });
            }

            if (blRentSalesExists)
            {
                dtblRep.Rows.Add(new object[] { 9, "", "" });
                dtblRep.Rows.Add(new object[] { 1, "RENTS", dblRentSales.ToString("f2") });

                if (Settings.TaxInclusive == "N")
                {
                    if (dblRNTax1 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRntTax1, dblRNTax1.ToString("f2") });
                    if (dblRNTax2 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRntTax2, dblRNTax2.ToString("f2") });
                    if (dblRNTax3 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRntTax3, dblRNTax3.ToString("f2") });
                }

                if (Settings.TaxInclusive == "Y")
                {
                    dtblRep.Rows.Add(new object[] { 11, "Tax Details", "" });
                    if (dblRNTax1 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRntTax1, dblRNTax1.ToString("f2") });
                    if (dblRNTax2 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRntTax2, dblRNTax2.ToString("f2") });
                    if (dblRNTax3 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRntTax3, dblRNTax3.ToString("f2") });
                }

                dtblRep.Rows.Add(new object[] { 9, "", "" });
            }

            if (blRepairSalesExists)
            {
                dtblRep.Rows.Add(new object[] { 1, "REPAIRS", dblRepairSales.ToString("f2") });

                if (Settings.TaxInclusive == "N")
                {
                    if (dblRPTax1 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRprTax1, dblRPTax1.ToString("f2") });
                    if (dblRPTax2 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRprTax2, dblRPTax2.ToString("f2") });
                    if (dblRPTax3 != 0) dtblRep.Rows.Add(new object[] { 0, "     " + strRprTax3, dblRPTax3.ToString("f2") });
                }

                if (Settings.TaxInclusive == "Y")
                {
                    dtblRep.Rows.Add(new object[] { 11, "Tax Details", "" });
                    if (dblRPTax1 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRprTax1, dblRPTax1.ToString("f2") });
                    if (dblRPTax2 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRprTax2, dblRPTax2.ToString("f2") });
                    if (dblRPTax3 != 0) dtblRep.Rows.Add(new object[] { 2, "     " + strRprTax3, dblRPTax3.ToString("f2") });
                }

                dtblRep.Rows.Add(new object[] { 2, "Diposit", dblRepairDeposit.ToString("f2") });
                dtblRep.Rows.Add(new object[] { 9, "", "" });
            }

            dtblRep.Rows.Add(new object[] { 2, "Item Discounts", discItem.ToString("f2") });

            if (dblItemDiscount != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Product Sales", dblItemDiscount.ToString("f2") });
            if (dblSItemDiscount != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Service Sales", dblSItemDiscount.ToString("f2") });
            if (dblBItemDiscount != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Other Sales", dblBItemDiscount.ToString("f2") });
            if ((blRepairSalesExists) && (dblRPItemDiscount != 0)) dtblRep.Rows.Add(new object[] { 0, "     " + "Repairs", dblRPItemDiscount.ToString("f2") });

            dtblRep.Rows.Add(new object[] { 9, "", "" });

            dtblRep.Rows.Add(new object[] { 2, "Invoice Discounts", discInv.ToString("f2") });

            if (dblInvDiscount != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Sales", dblInvDiscount.ToString("f2") });
            if ((blRentSalesExists) && (dblRNInvDiscount != 0)) dtblRep.Rows.Add(new object[] { 0, "     " + "Rents", dblRNInvDiscount.ToString("f2") });
            if ((blRepairSalesExists) && (dblRPInvDiscount != 0)) dtblRep.Rows.Add(new object[] { 0, "     " + "Repairs", dblRPInvDiscount.ToString("f2") });

            if (intFreeQty > 0)
            {
                dtblRep.Rows.Add(new object[] { 9, "", "" });
                dtblRep.Rows.Add(new object[] { 2, "Free Items" + " (" + intFreeQty.ToString() + ")", dblFreeAmount.ToString("f2") });
            }

            dtblRep.Rows.Add(new object[] { 9, "", "" });

            if (dblFees != 0)
            {
                dtblRep.Rows.Add(new object[] { 2, "Fees & Charges", dblFees.ToString() });
                if (dblSaleFees != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Sales - Fees", dblSaleFees.ToString() });
                if (dblSaleFeesTax != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Sales - Tax on Fees", dblSaleFeesTax.ToString() });
                if (dblRNFees != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Rents - Fees", dblRNFees.ToString() });
                if (dblRNFeesTax != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Rents - Tax on Fees", dblRNFeesTax.ToString() });
                if (dblRPFees != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Repairs - Fees", dblRPFees.ToString() });
                if (dblRPFeesTax != 0) dtblRep.Rows.Add(new object[] { 0, "     " + "Repairs - Tax on Fees", dblRPFeesTax.ToString() });
            }


            dtblRep.Rows.Add(new object[] { 9, "", "" });

            dtblRep.Rows.Add(new object[] { 2, "Food Stamps Tendered", dblFoodStampTendered.ToString("f2") });
            dtblRep.Rows.Add(new object[] { 2, "Layaway Sales Posted", dblLayawaySalesPosted.ToString("f2") });
            dtblRep.Rows.Add(new object[] { 2, "Gift Cert. Sales", dblGCSales.ToString("f2") });
            if (dblGiftAid != 0) dtblRep.Rows.Add(new object[] { 2, "Gift Aid", dblGiftAid.ToString("f2") });
            if (Settings.PaymentGateway == 2) dtblRep.Rows.Add(new object[] { 2, "Mercury Gift Card Sales", MercuryGiftCardSales.ToString() });
            if (Settings.PaymentGateway == 3) dtblRep.Rows.Add(new object[] { 2, "Precidia Gift Card Sales", PrecidiaGiftCardSales.ToString() });
            if (Settings.PaymentGateway == 5) dtblRep.Rows.Add(new object[] { 2, "Datacap Gift Card Sales", DatacapGiftCardSales.ToString() });
            if (Settings.PaymentGateway == 7) dtblRep.Rows.Add(new object[] { 2, "POSLink Gift Card Sales", POSLinkGiftCardSales.ToString() });
            dtblRep.Rows.Add(new object[] { 2, "Bottle Refund", dblBottleRefund.ToString("f2") });
            dtblRep.Rows.Add(new object[] { 2, "Cost of Goods Sold", dblCostOfGoods.ToString("f2") });

            OfflineRetailV2.Report.Sales.repSalesSummary rep_SalesSummary = new OfflineRetailV2.Report.Sales.repSalesSummary();
            GeneralFunctions.MakeReportWatermark(rep_SalesSummary);
            rep_SalesSummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_SalesSummary.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_SalesSummary.rHeader.Text = "Sales Summary Report";
            rep_SalesSummary.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
            rep_SalesSummary.DecimalPlace = Settings.DecimalPlace;
            rep_SalesSummary.Report.DataSource = dtblRep;
            rep_SalesSummary.rs.DataBindings.Add("Text", dtblRep, "Style");
            rep_SalesSummary.rtxt.DataBindings.Add("Text", dtblRep, "Title");
            rep_SalesSummary.rv.DataBindings.Add("Text", dtblRep, "Value");


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
                    dtbl1.Dispose();
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
                    dtbl1.Dispose();
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
                    attachfile = "sales_summary.pdf";
                    GeneralFunctions.EmailReport(rep_SalesSummary, attachfile, "Sales Summary");
                }
                finally
                {
                    rep_SalesSummary.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
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
            Title.Text = "Sales Summary Report";
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
