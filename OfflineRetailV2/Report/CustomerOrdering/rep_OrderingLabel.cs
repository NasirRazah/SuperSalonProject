/*
 USER CLASS : Report - Customer Order Label
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
namespace OfflineRetailV2.Report.CustomerOrdering
{
    public partial class repOrderingLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public repOrderingLabel()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        private void rpgCategory_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rpgCategory.Text == "I")
            {
                rInfo2.SizeF = new SizeF(150, 50);
                rInfo3.LocationF = new PointF(10, 80);
            }
        }

        private void rInfo3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

    }
}
