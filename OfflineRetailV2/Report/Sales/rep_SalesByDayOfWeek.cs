/*
 USER CLASS : Report - Sales (Day of Week)
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
    public partial class repSalesByDayOfWeek : DevExpress.XtraReports.UI.XtraReport
    {
        public repSalesByDayOfWeek()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter,Settings.POS_Culture, this);
        }

        
    }
}
