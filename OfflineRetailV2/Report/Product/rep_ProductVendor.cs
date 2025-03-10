/*
 USER CLASS : Report - Item Vendor Info.
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
namespace OfflineRetailV2.Report.Product
{
    public partial class repProductVendor : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;

        public repProductVendor()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rPrimary_BeforePrint(object sender, CancelEventArgs e)
        {
            if (rPrimary.Text == "True")
            {
                rPrimary.Text = OfflineRetailV2.Properties.Resources.Yes;
            }
            else
            {
                rPrimary.Text = OfflineRetailV2.Properties.Resources.No;
            }
        }

        private void rCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f3");
            else rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f");
        }

    }
}
