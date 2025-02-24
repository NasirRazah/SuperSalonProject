/*
 USER CLASS : Report - Dashboard (Clock In Employee)
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

namespace OfflineRetailV2.Report.Closeout
{
    public partial class repdbempclock : DevExpress.XtraReports.UI.XtraReport
    {
        public repdbempclock()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
    }
}
