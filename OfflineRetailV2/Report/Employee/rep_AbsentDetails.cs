/*
 USER CLASS : Report - Employee Absent Listing
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
    public partial class repAbsentDetails : DevExpress.XtraReports.UI.XtraReport
    {
        private int TotalTime1 = 0;
        public repAbsentDetails()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rLT_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            TotalTime1 = TotalTime1 + 1;
        }

        private void rCount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //rCount.Text = Convert.ToString(TotalTime1);
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            TotalTime1 = 0;
        }

        private void rCount_AfterPrint(object sender, EventArgs e)
        {

        }

    }
}
