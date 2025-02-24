/*
 USER CLASS : Report - Item Price, Tax Listing
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
    public partial class repProductPriceTax : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private int intPDecimalPlace;

        public repProductPriceTax()
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

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            intPDecimalPlace = GeneralFunctions.fnInt32(rDP.Text);
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f");
                else
                    rPriceA.Text = GeneralFunctions.fnDouble(rPriceA.Text).ToString("f3");
            }
            if (rPriceA.Text.StartsWith("-"))
            {
                string strTemp = rPriceA.Text.Remove(0, 1);
                rPriceA.Text = "(" + strTemp + ")";
            }
        }

        private void rPriceB_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rPriceB.Text = GeneralFunctions.fnDouble(rPriceB.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rPriceB.Text = GeneralFunctions.fnDouble(rPriceB.Text).ToString("f");
                else
                    rPriceB.Text = GeneralFunctions.fnDouble(rPriceB.Text).ToString("f3");
            }
            if (rPriceB.Text.StartsWith("-"))
            {
                string strTemp = rPriceB.Text.Remove(0, 1);
                rPriceB.Text = "(" + strTemp + ")";
            }
        }

        private void rPriceC_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rPriceC.Text = GeneralFunctions.fnDouble(rPriceC.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rPriceC.Text = GeneralFunctions.fnDouble(rPriceC.Text).ToString("f");
                else
                    rPriceC.Text = GeneralFunctions.fnDouble(rPriceC.Text).ToString("f3");
            }
            if (rPriceC.Text.StartsWith("-"))
            {
                string strTemp = rPriceC.Text.Remove(0, 1);
                rPriceC.Text = "(" + strTemp + ")";
            }
        }

        private void rFS_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rFS.Text == "Y") rFS.Text = OfflineRetailV2.Properties.Resources.Yes;
            if (rFS.Text == "N") rFS.Text = OfflineRetailV2.Properties.Resources.No;
        }

    }
}
