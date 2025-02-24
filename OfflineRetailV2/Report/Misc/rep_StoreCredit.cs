/*
 USER CLASS : Report - Customer Stote Credit Listing
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
    public partial class repStoreCredit : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repStoreCredit()
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

        private void rCredit_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rCredit.Text != "")
            {
                if (intDecimalPlace == 3) rCredit.Text = GeneralFunctions.fnDouble(rCredit.Text).ToString("f3");
                else rCredit.Text = GeneralFunctions.fnDouble(rCredit.Text).ToString("f");
            }
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }
    }
}
