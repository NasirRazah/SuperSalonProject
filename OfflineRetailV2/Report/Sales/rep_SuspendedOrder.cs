/*
 USER CLASS : Report - Suspended Transaction Listing
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
    public partial class repSuspendedOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public repSuspendedOrder()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void rData3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rData3.Text = GeneralFunctions.fnDate(rData3.Text).ToString((SystemVariables.DateFormat.Replace("/","-")) + "  hh:mm tt");
        }

    }
}
