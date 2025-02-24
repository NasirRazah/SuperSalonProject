/*
 USER CLASS : Report - Card Transaction Listing
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
    public partial class repCardTransaction : DevExpress.XtraReports.UI.XtraReport
    {
        public repCardTransaction()
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
            if (intDecimalPlace == 3) rlCardAmount.Text = GeneralFunctions.fnDouble(rlCardAmount.Text).ToString("f3");
            else rlCardAmount.Text = GeneralFunctions.fnDouble(rlCardAmount.Text).ToString("f");

            if (rlCardAmount.Text.StartsWith("-"))
            {
                string strTemp = rlCardAmount.Text.Remove(0, 1);
                rlCardAmount.Text = "(" + strTemp + ")";
            }
        }

    }
}
