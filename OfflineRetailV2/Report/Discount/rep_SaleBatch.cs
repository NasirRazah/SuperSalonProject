/*
 USER CLASS : Report - Sale Batch Record
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
    public partial class repSaleBatch : DevExpress.XtraReports.UI.XtraReport
    {
        public repSaleBatch()
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
            rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");


            if (rPrice.Text.StartsWith("-"))
            {
                string strTemp = rPrice.Text.Remove(0, 1);
                rPrice.Text = "(" + strTemp + ")";
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

    }
}
