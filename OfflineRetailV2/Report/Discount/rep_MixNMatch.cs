/*
 USER CLASS : Report - Mix n Match Batch Record
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
    public partial class repMixNMatch : DevExpress.XtraReports.UI.XtraReport
    {
        public repMixNMatch()
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
            
        }

        private void rApplicable_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

    }
}
