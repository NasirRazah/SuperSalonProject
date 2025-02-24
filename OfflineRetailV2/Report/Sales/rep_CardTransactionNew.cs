/*
 USER CLASS : Report - Card Transaction Listing (1)
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
    public partial class repCardTransactionNew : DevExpress.XtraReports.UI.XtraReport
    {
        public repCardTransactionNew()
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

        private void rlCardAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rAuthAmount.Text = GeneralFunctions.fnDouble(rAuthAmount.Text).ToString("f3");
            else rAuthAmount.Text = GeneralFunctions.fnDouble(rAuthAmount.Text).ToString("f");

            if (rAuthAmount.Text.StartsWith("-"))
            {
                string strTemp = rAuthAmount.Text.Remove(0, 1);
                rAuthAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rCardAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rAuthAmount.Text = GeneralFunctions.fnDouble(rCardAmount.Text).ToString("f3");
            else rCardAmount.Text = GeneralFunctions.fnDouble(rCardAmount.Text).ToString("f");

            if (rCardAmount.Text.StartsWith("-"))
            {
                string strTemp = rCardAmount.Text.Remove(0, 1);
                rCardAmount.Text = "(" + strTemp + ")";
            }
        }

    }
}
