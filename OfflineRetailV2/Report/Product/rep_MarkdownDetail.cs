/*
 USER CLASS : Report - Markdown (Detail)
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
    public partial class repMarkdownDetail : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private int intPDecimalPlace;
        private double rTot1Amt = 0;
        private double rTTot1Amt = 0;
        private bool blPrint = false;
        public repMarkdownDetail()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public string GroupType
        {
            get { return strGroupType; }
            set { strGroupType = value; }
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

        private void rMargin_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        

        private void rGroup_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        

        private void rVendor_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void xrTableCell7_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void rQty_BeforePrint(object sender, CancelEventArgs e)
        {
            /*if (intDecimalPlace == 3) rQty.Text = GeneralFunctions.fnDouble(rQty.Text).ToString("f3");
            else rQty.Text = GeneralFunctions.fnDouble(rQty.Text).ToString("f");

            int intStr = rQty.Text.IndexOf(".000");
            rQty.Text = rQty.Text.Remove(intStr);*/

            
        }

        private void rUnitCost_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void rUnitRetailCost_BeforePrint(object sender, CancelEventArgs e)
        {
            
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

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rUnitCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rExtRetail_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void rTot1_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTot1_AfterPrint(object sender, EventArgs e)
        {
           
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rTTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rType_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rPrice.Text).ToString("0.0#"));
        }

        private void rNewPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rNewPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rNewPrice.Text).ToString("0.0#"));
        }

        private void rSavings_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rSavings.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rSavings.Text).ToString("0.0#"));
        }

        private void rGPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rGPrice.Text).ToString("0.0#"));
        }

        private void rGNewPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGNewPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rGNewPrice.Text).ToString("0.0#"));
        }

        private void rGSavings_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGSavings.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rGSavings.Text).ToString("0.0#"));
        }

        private void rTPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rTPrice.Text).ToString("0.0#"));
        }

        private void rTNewPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTNewPrice.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rTNewPrice.Text).ToString("0.0#"));
        }

        private void rTSavings_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTSavings.Text = GeneralFunctions.PriceSmartDisplay() + GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(rTSavings.Text).ToString("0.0#"));
        }
    }
}
