/*
 USER CLASS : Report - Item Transfer Record
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
    public partial class repTransfer : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repTransfer()
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

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString("0.##");
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
            else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTotPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f3");
            else rTotPrice.Text = GeneralFunctions.fnDouble(rTotPrice.Text).ToString("f");
        }

        private void rSubtatal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

            double d = GeneralFunctions.fnDouble(rTQty.Text);
            rTQty.Text = d.ToString("0.##");
        }

        private void rTotFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 4) rTCost.Text = GeneralFunctions.fnDouble(rTCost.Text).ToString("f4");
            else if (intDecimalPlace == 3) rTCost.Text = GeneralFunctions.fnDouble(rTCost.Text).ToString("f3");
            else rTCost.Text = GeneralFunctions.fnDouble(rTCost.Text).ToString("f");
        }

        private void rTotTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rGrandTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }
    }
}
