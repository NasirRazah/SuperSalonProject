/*
 USER CLASS : Report - Customer Listing
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
namespace OfflineRetailV2.Report.Customer
{
    public partial class repCustomerGeneral : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;

        public repCustomerGeneral()
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

        private void rTotPurchase_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTotPurchase.Text = GeneralFunctions.fnDouble(rTotPurchase.Text).ToString("f3");
            else rTotPurchase.Text = GeneralFunctions.fnDouble(rTotPurchase.Text).ToString("f");
        }

        private void rLastPurchase_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rLastPurchase.Text = GeneralFunctions.fnDouble(rLastPurchase.Text).ToString("f3");
            else rLastPurchase.Text = GeneralFunctions.fnDouble(rLastPurchase.Text).ToString("f");
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rLastPurchaseDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rLastPurchaseDate.Text != "")
            {
                rLastPurchaseDate.Text = GeneralFunctions.fnDate(rLastPurchaseDate.Text).ToString("d");
            }
            else rLastPurchaseDate.Text = "";
        }

    }
}
