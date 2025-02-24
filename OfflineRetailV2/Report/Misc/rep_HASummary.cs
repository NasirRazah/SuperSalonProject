/*
 USER CLASS : Report - House Account Summary Listing
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
namespace OfflineRetailV2.Report.Misc
{
    public partial class repHASummary : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repHASummary()
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

        private void rLP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rLP.Text.Trim() != "")
            {
                if (intDecimalPlace == 3) rLP.Text = GeneralFunctions.fnDouble(rLP.Text).ToString("f3");
                else rLP.Text = GeneralFunctions.fnDouble(rLP.Text).ToString("f");

                if (rLP.Text.StartsWith("-"))
                {
                    string strTemp = rLP.Text.Remove(0, 1);
                    rLP.Text = "(" + strTemp + ")";
                }
            }
        }

        private void rB_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rB.Text.Trim() != "")
            {
                if (intDecimalPlace == 3) rB.Text = GeneralFunctions.fnDouble(rB.Text).ToString("f3");
                else rB.Text = GeneralFunctions.fnDouble(rB.Text).ToString("f");

                if (rB.Text.StartsWith("-"))
                {
                    string strTemp = rB.Text.Remove(0, 1);
                    rB.Text = "(" + strTemp + ")";
                }
            }
        }

        private void rDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rDate.Text != "")
            {
                rDate.Text = GeneralFunctions.fnDate(rDate.Text).ToString("d");
            }
            else rDate.Text = "";
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTotal.Text.StartsWith("-"))
            {
                string strTemp = rTotal.Text.Remove(0, 1);
                rTotal.Text = "(" + strTemp + ")";
            }
        }

    }
}
