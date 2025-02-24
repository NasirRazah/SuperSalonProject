/*
 USER CLASS : Report - Layaway Receipt (Tender Info)
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
namespace OfflineRetailV2.Report.Invoice
{
    public partial class repReceiptTender : DevExpress.XtraReports.UI.XtraReport
    {
        public repReceiptTender()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rTenderName_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTenderName.Text = OfflineRetailV2.Properties.Resources.Paidin + " " + rTenderName.Text;
        }

        private void rTenderAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTenderAmount.Text = GeneralFunctions.fnDouble(rTenderAmount.Text).ToString("f3");
            else rTenderAmount.Text = GeneralFunctions.fnDouble(rTenderAmount.Text).ToString("f");

            if (rTenderAmount.Text.StartsWith("-"))
            {
                string strTemp = rTenderAmount.Text.Remove(0, 1);
                rTenderAmount.Text = "(" + strTemp + ")";
            }
        }

    }
}
