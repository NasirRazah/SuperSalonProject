/*
 USER CLASS : Report - Receipt (Invoice Total)
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Native.Presenters;
using System.Globalization;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Sales
{
    public partial class repInvSubtotal : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private string strDR;
        public string DR
        {
            get { return strDR; }
            set { strDR = value; }
        }

        public repInvSubtotal()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rSubTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rSubTotal.Text = GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rSubTotal.Text = GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rSubTotal.Text = GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f3");
            }

            if (rSubTotal.Text.StartsWith("-"))
            {
                string strTemp = rSubTotal.Text.Remove(0, 1);
                rSubTotal.Text = "(" + strTemp + ")";
            }
        }

        private void rDiscount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDiscount.Text = GeneralFunctions.fnDouble(rDiscount.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rDiscount.Text = GeneralFunctions.fnDouble(rDiscount.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rDiscount.Text = GeneralFunctions.fnDouble(rDiscount.Text).ToString("f3");
            }

            if (rDiscount.Text.StartsWith("-"))
            {
                string strTemp = rDiscount.Text.Remove(0, 1);
                rDiscount.Text = "(" + strTemp + ")";
            }
        }

        private void rSurcharge_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f3");
            }

            if (rTax.Text.StartsWith("-"))
            {
                string strTemp = rTax.Text.Remove(0, 1);
                rTax.Text = "(" + strTemp + ")";
            }
        }

        private void rlDiscount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strDR != "")
            {
                rlDiscount.Text = OfflineRetailV2.Properties.Resources.TotalDiscount + "  " + "( " + strDR + " )";
            }
        }

    }
}
