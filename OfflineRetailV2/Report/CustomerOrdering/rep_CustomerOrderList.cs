/*
 USER CLASS : Report - Customer Order Record
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
    public partial class repCustomerOrderList : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repCustomerOrderList()
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
            
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTotPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rQty.Text = GeneralFunctions.fnDouble( rQty.Text).ToString("0.##");
        }

        private void rSubtatal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

           
        }

        private void rTotFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
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
