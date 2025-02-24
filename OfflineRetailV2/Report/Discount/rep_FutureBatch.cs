/*
 USER CLASS : Report - Future Batch Record
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
namespace OfflineRetailV2.Report.Discount
{
    public partial class repFutureBatch : DevExpress.XtraReports.UI.XtraReport
    {
        public repFutureBatch()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rPriceC.Text = GeneralFunctions.fnDouble(rPriceC.Text).ToString("f");


            if (rPriceC.Text.StartsWith("-"))
            {
                string strTemp = rPriceC.Text.Remove(0, 1);
                rPriceC.Text = "(" + strTemp + ")";
            }
        }

        private void rApplicable_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rApplicable.Text == "Y")
            {
                rApplicable.Text = OfflineRetailV2.Properties.Resources.Family;
            }
            else
            {
                rApplicable.Text = OfflineRetailV2.Properties.Resources.Item;
            }
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f");


            if (rPriceA.Text.StartsWith("-"))
            {
                string strTemp = rPriceA.Text.Remove(0, 1);
                rPriceA.Text = "(" + strTemp + ")";
            }
        }

        private void rPriceB_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rPriceB.Text = GeneralFunctions.fnDouble(rPriceB.Text).ToString("f");


            if (rPriceB.Text.StartsWith("-"))
            {
                string strTemp = rPriceB.Text.Remove(0, 1);
                rPriceB.Text = "(" + strTemp + ")";
            }
        }

    }
}
