/*
 USER CLASS : Report - Vendor Minimum Order Listing
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
namespace OfflineRetailV2.Report
{
    public partial class repMinOrderReport : DevExpress.XtraReports.UI.XtraReport
    {
        public repMinOrderReport()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private int intDecimalPlace;

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rGAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rGAmt.Text = GeneralFunctions.fnDouble(rGAmt.Text).ToString("f3");
            else rGAmt.Text = GeneralFunctions.fnDouble(rGAmt.Text).ToString("f");

            if (rGAmt.Text.StartsWith("-"))
            {
                string strTemp = rGAmt.Text.Remove(0, 1);
                rGAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f3");
            else rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f");

            if (rFreight.Text.StartsWith("-"))
            {
                string strTemp = rFreight.Text.Remove(0, 1);
                rFreight.Text = "(" + strTemp + ")";
            }
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f3");
            else rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f");

            if (rTax.Text.StartsWith("-"))
            {
                string strTemp = rTax.Text.Remove(0, 1);
                rTax.Text = "(" + strTemp + ")";
            }
        }

        private void rNetAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rNetAmt.Text = GeneralFunctions.fnDouble(rNetAmt.Text).ToString("f3");
            else rNetAmt.Text = GeneralFunctions.fnDouble(rNetAmt.Text).ToString("f");

            if (rNetAmt.Text.StartsWith("-"))
            {
                string strTemp = rNetAmt.Text.Remove(0, 1);
                rNetAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rMinAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rMinAmt.Text = GeneralFunctions.fnDouble(rMinAmt.Text).ToString("f3");
            else rMinAmt.Text = GeneralFunctions.fnDouble(rMinAmt.Text).ToString("f");

            if (rMinAmt.Text.StartsWith("-"))
            {
                string strTemp = rMinAmt.Text.Remove(0, 1);
                rMinAmt.Text = "(" + strTemp + ")";
            }
        }
    }
}
