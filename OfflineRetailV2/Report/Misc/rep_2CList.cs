/*
 USER CLASS : Report - Common Listing with 2 Columns (1)
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Native.Presenters;
using System.Globalization;
using System.Collections.Generic;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Misc
{
    public partial class rep2CList : DevExpress.XtraReports.UI.XtraReport
    {
        public rep2CList()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();
            
            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

    }
}
