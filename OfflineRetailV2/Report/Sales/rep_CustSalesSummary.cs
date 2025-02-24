/*
 USER CLASS : Report - Customer Sale Listing (Summary)
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
    public partial class repCustSalesSummary : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;

        public repCustSalesSummary()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rSales_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rSales.Text = GeneralFunctions.fnDouble(rSales.Text).ToString("f3");
            else rSales.Text = GeneralFunctions.fnDouble(rSales.Text).ToString("f");

            if (rSales.Text.StartsWith("-"))
            {
                string strTemp = rSales.Text.Remove(0, 1);
                rSales.Text = "(" + strTemp + ")";
            }
        }

        private void rTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot3.Text.StartsWith("-"))
            {
                string strTemp = rTot3.Text.Remove(0, 1);
                rTot3.Text = "(" + strTemp + ")";
            }
        }

    }
}
