/*
 USER CLASS : Report - Customer Repair Listing
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
namespace OfflineRetailV2.Report.Customer
{
    public partial class repCustomerRepairList : DevExpress.XtraReports.UI.XtraReport
    {
        public repCustomerRepairList()
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

        private void grpRepairType_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (grpRepairType.Text == "17") grpRepairType.Text = OfflineRetailV2.Properties.Resources.ActiveRepairs;
            if (grpRepairType.Text == "18") grpRepairType.Text = OfflineRetailV2.Properties.Resources.CompletedRepairs;
        }

        private void rAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rAmount.Text = GeneralFunctions.fnDouble(rAmount.Text).ToString("f");
        }

    }
}
