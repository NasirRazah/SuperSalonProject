/*
 USER CLASS : Report - Inventory Valuation
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
namespace OfflineRetailV2.Report.Product
{
    public partial class repInventoryValuation : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public repInventoryValuation()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        private void rCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f3");
            else rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f");

            if (rCost.Text.StartsWith("-"))
            {
                string strTemp = rCost.Text.Remove(0, 1);
                rCost.Text = "(" + strTemp + ")";
            }
        }

        private void rValue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rValue.Text = GeneralFunctions.fnDouble(rValue.Text).ToString("f3");
            else rValue.Text = GeneralFunctions.fnDouble(rValue.Text).ToString("f");

            if (rValue.Text.StartsWith("-"))
            {
                string strTemp = rValue.Text.Remove(0, 1);
                rValue.Text = "(" + strTemp + ")";
            }
        }

        private void rTQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

            if ((rTQty.Text.EndsWith(".00")) || (rTQty.Text.EndsWith(".000")))
            {
                string temp = "";
                if (intDecimalPlace == 3) temp = rTQty.Text.Substring(0, rTQty.Text.Length - 4);
                if (intDecimalPlace == 2) temp = rTQty.Text.Substring(0, rTQty.Text.Length - 3);
                if (temp.StartsWith("-"))
                {
                    string strTemp = temp.Remove(0, 1);
                    rTQty.Text = "(" + strTemp + ")";
                }
                else
                {
                    rTQty.Text = temp;
                }
            }
            else
            {
                if (rTQty.Text.StartsWith("-"))
                {
                    string strTemp1 = rTQty.Text.Remove(0, 1);
                    rTQty.Text = "(" + strTemp1 + ")";
                }
            }

        }

        private void rTValue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTValue.Text = GeneralFunctions.fnDouble(rTValue.Text).ToString("f3");
            else rTValue.Text = GeneralFunctions.fnDouble(rTValue.Text).ToString("f");

            if (rTValue.Text.StartsWith("-"))
            {
                string strTemp = rTValue.Text.Remove(0, 1);
                rTValue.Text = "(" + strTemp + ")";
            }
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rQty.Text.StartsWith("-"))
            {
                string strTemp = rQty.Text.Remove(0, 1);
                rQty.Text = "(" + strTemp + ")";
            }
        }

        private void rDCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDCost.Text = GeneralFunctions.fnDouble(rDCost.Text).ToString("f3");
            else rDCost.Text = GeneralFunctions.fnDouble(rDCost.Text).ToString("f");

            if (rDCost.Text.StartsWith("-"))
            {
                string strTemp = rDCost.Text.Remove(0, 1);
                rDCost.Text = "(" + strTemp + ")";
            }
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f3");
            else rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f");

            if (rPriceA.Text.StartsWith("-"))
            {
                string strTemp = rPriceA.Text.Remove(0, 1);
                rPriceA.Text = "(" + strTemp + ")";
            }
        }
    }
}
