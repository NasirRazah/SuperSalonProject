/*
 USER CLASS : Report - Employee Late Attendance (Summary)
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
namespace OfflineRetailV2.Report.Employee
{
    public partial class repLateSummary : DevExpress.XtraReports.UI.XtraReport
    {
        private int TotalCount = 0;
        public repLateSummary()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void rLT_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rLT.Text != "")
            {
                TotalCount = TotalCount + GeneralFunctions.fnInt32(rLT.Text);
            }
        }

        private void rCount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rCount.Text = Convert.ToString(TotalCount);
        }

    }
}
