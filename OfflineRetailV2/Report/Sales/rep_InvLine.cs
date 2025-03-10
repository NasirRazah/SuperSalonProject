/*
 USER CLASS : Report - Receipt (Line Item)
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
    public partial class repInvLine : DevExpress.XtraReports.UI.XtraReport
    {
        private string IM = "";
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repInvLine()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rlPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            /*if (rlPrice.Text == "") return;
            if (intDecimalPlace == 3) rlPrice.Text = GeneralFunctions.fnDouble(rlPrice.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rlPrice.Text = GeneralFunctions.fnDouble(rlPrice.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rlPrice.Text = GeneralFunctions.fnDouble(rlPrice.Text).ToString("f3");
            }

            if (rlPrice.Text.StartsWith("-"))
            {
                string strTemp = rlPrice.Text.Remove(0, 1);
                rlPrice.Text = "(" + strTemp + ")";
            }*/

        }

        private void rlDiscount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rlDiscount.Text == "") return;
            if (intDecimalPlace == 3) rlDiscount.Text = GeneralFunctions.fnDouble(rlDiscount.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rlDiscount.Text = GeneralFunctions.fnDouble(rlDiscount.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rlDiscount.Text = GeneralFunctions.fnDouble(rlDiscount.Text).ToString("f3");
            }

            if (rlDiscount.Text.StartsWith("-"))
            {
                string strTemp = rlDiscount.Text.Remove(0, 1);
                rlDiscount.Text = "(" + strTemp + ")";
            }

        }

        private void rlSurcharge_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            /*if (rlSurcharge.Text == "") return;
            if (intDecimalPlace == 3) rlSurcharge.Text = GeneralFunctions.fnDouble(rlSurcharge.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rlSurcharge.Text = GeneralFunctions.fnDouble(rlSurcharge.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rlSurcharge.Text = GeneralFunctions.fnDouble(rlSurcharge.Text).ToString("f3");
            }

            if (rlSurcharge.Text.StartsWith("-"))
            {
                string strTemp = rlSurcharge.Text.Remove(0, 1);
                rlSurcharge.Text = "(" + strTemp + ")";
            }*/

        }

        private void rlTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rlTotal.Text == "") return;
            if (intDecimalPlace == 3) rlTotal.Text = GeneralFunctions.fnDouble(rlTotal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rlTotal.Text = GeneralFunctions.fnDouble(rlTotal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rlTotal.Text = GeneralFunctions.fnDouble(rlTotal.Text).ToString("f3");
            }

            if (rlSurcharge.Text.StartsWith("-"))
            {
                string strTemp = rlTotal.Text.Remove(0, 1);
                rlTotal.Text = "(" + strTemp + ")";
            }

        }

        private void rM_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            IM = rM.Text;
        }

        private void rlIem_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void rlqty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //double d = GeneralFunctions.fnDouble(rlqty.Text);
            //rlqty.Text = d.ToString();
        }

        private void rDiscTxt_BeforePrint(object sender, CancelEventArgs e)
        {
            if (rDiscTxt.Text == "") ((XRLabel)sender).Text = String.Empty;
        }

        private void rTY_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

    }
}
