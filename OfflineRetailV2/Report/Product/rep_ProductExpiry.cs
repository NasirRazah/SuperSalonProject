/*
 USER CLASS : Report - Item Listing
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
    public partial class repProductExpiry : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private int intPDecimalPlace;
        private double rTot1Amt = 0;
        private bool blPrint = false;
        public repProductExpiry()
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

        public int PDecimalPlace
        {
            get { return intPDecimalPlace; }
            set { intPDecimalPlace = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rQty_BeforePrint(object sender, CancelEventArgs e)
        {
           
            double d = Math.Truncate(GeneralFunctions.fnDouble(rBrand.Text));
            rBrand.Text = d.ToString();
        }

        private void rUnitCost_BeforePrint(object sender, CancelEventArgs e)
        {
            if (intDecimalPlace == 3) rSerialNo.Text = GeneralFunctions.fnDouble(rSerialNo.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rSerialNo.Text = GeneralFunctions.fnDouble(rSerialNo.Text).ToString("f");
                else
                    rSerialNo.Text = GeneralFunctions.fnDouble(rSerialNo.Text).ToString("f3");
            }
            if (rSerialNo.Text.StartsWith("-"))
            {
                string strTemp = rSerialNo.Text.Remove(0, 1);
                rSerialNo.Text = "(" + strTemp + ")";
            }
        }

        private void rUnitRetailCost_BeforePrint(object sender, CancelEventArgs e)
        {
            if (intDecimalPlace == 3) rEexpiry.Text = GeneralFunctions.fnDouble(rEexpiry.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rEexpiry.Text = GeneralFunctions.fnDouble(rEexpiry.Text).ToString("f");
                else
                    rEexpiry.Text = GeneralFunctions.fnDouble(rEexpiry.Text).ToString("f3");
            }

            if (rEexpiry.Text.StartsWith("-"))
            {
                string strTemp = rEexpiry.Text.Remove(0, 1);
                rEexpiry.Text = "(" + strTemp + ")";
            }
        }

        private void rExtCost_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rExtRetail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rUnitCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rUnitRetailCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rExtCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rExtRetail_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            intPDecimalPlace = GeneralFunctions.fnInt32(rDP.Text);
        }

        private void rDCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }
    }
}
