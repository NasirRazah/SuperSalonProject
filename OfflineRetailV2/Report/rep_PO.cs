/*
 USER CLASS : Report - Purchase Order Record
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
    public partial class repPO : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;

        public repPO()
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

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rTax_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
            else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f4");
            else if (intDecimalPlace == 3) rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f3");
            else rFreight.Text = GeneralFunctions.fnDouble(rFreight.Text).ToString("f");
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f3");
            else rTax.Text = GeneralFunctions.fnDouble(rTax.Text).ToString("f");
        }

        private void rTotPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f3");
            else rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f");
        }

        private void rSubtatal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rSubtatal.Text = GeneralFunctions.fnDouble(rSubtatal.Text).ToString("f4");
            else if (intDecimalPlace == 3) rSubtatal.Text = GeneralFunctions.fnDouble(rSubtatal.Text).ToString("f3");
            else rSubtatal.Text = GeneralFunctions.fnDouble(rSubtatal.Text).ToString("f");
        }

        private void rTotFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTotFreight.Text = GeneralFunctions.fnDouble(rTotFreight.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTotFreight.Text = GeneralFunctions.fnDouble(rTotFreight.Text).ToString("f3");
            else rTotFreight.Text = GeneralFunctions.fnDouble(rTotFreight.Text).ToString("f");
        }

        private void rTotTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTotTax.Text = GeneralFunctions.fnDouble(rTotTax.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTotTax.Text = GeneralFunctions.fnDouble(rTotTax.Text).ToString("f3");
            else rTotTax.Text = GeneralFunctions.fnDouble(rTotTax.Text).ToString("f");
        }

        private void rGrandTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rGrandTotal.Text = GeneralFunctions.fnDouble(rGrandTotal.Text).ToString("f4");
            else if (intDecimalPlace == 3) rGrandTotal.Text = GeneralFunctions.fnDouble(rGrandTotal.Text).ToString("f3");
            else rGrandTotal.Text = GeneralFunctions.fnDouble(rGrandTotal.Text).ToString("f");
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
        }
    }
}
