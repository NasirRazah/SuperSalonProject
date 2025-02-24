/*
 USER CLASS : Report - Sales Dashboard (Gift Card Sale)
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
    public partial class repDashboard_GC : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_GC()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
       
        private void PageHeader_AfterPrint(object sender, EventArgs e)
        {
           
           
        }

        private void xrLabel1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrLabel2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        //private void xrChart2_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        //{
            
            
        //}

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTotal.Text = GeneralFunctions.FormatDoubleForPrint(rTotal.Text);
        }

        private void rAvg_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rAvg.Text = GeneralFunctions.FormatDoubleForPrint(rAvg.Text);
        }

    }
}
