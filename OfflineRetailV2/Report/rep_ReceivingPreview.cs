/*
 USER CLASS : Report - Receiving Order (Before Final Post)
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
namespace OfflineRetailV2.Report
{
    public partial class repReceivingPreview : DevExpress.XtraReports.UI.XtraReport
    {
        public repReceivingPreview()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
            else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f4");
            else if (intDecimalPlace == 3) rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f3");
            else rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f");
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f3");
            else rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f");
        }

        private void rTotPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f3");
            else rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f");
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f4");
            else if (intDecimalPlace == 3) rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f3");
            else rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f");
        }
    }
}
