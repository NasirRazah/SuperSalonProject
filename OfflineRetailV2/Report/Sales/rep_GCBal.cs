/*
 USER CLASS : Report - Gift Cert. Balance Listing
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
namespace OfflineRetailV2.Report.Sales
{
    public partial class repGCBal : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repGCBal()
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

        private void rBal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rBal.Text = GeneralFunctions.fnDouble(rBal.Text).ToString("f3");
            else rBal.Text = GeneralFunctions.fnDouble(rBal.Text).ToString("f");

            if (rBal.Text.StartsWith("-"))
            {
                string strTemp = rBal.Text.Remove(0, 1);
                rBal.Text = "(" + strTemp + ")";
            }
        }

        private void repGCBal_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
