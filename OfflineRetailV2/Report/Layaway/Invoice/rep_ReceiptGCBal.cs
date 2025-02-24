/*
 USER CLASS : Report - Layaway Receipt (Gift Cert Balance)
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
    public partial class repReceiptGCBal : DevExpress.XtraReports.UI.XtraReport
    {
        public repReceiptGCBal()
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

        private void rGCAMount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rGCAMount.Text = GeneralFunctions.fnDouble(rGCAMount.Text).ToString("f3");
            else rGCAMount.Text = GeneralFunctions.fnDouble(rGCAMount.Text).ToString("f");

            if (rGCAMount.Text.StartsWith("-"))
            {
                string strTemp = rGCAMount.Text.Remove(0, 1);
                rGCAMount.Text = "(" + strTemp + ")";
            }
        }

        private void rGC_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGC.Text = OfflineRetailV2.Properties.Resources.GC + " : " + rGC.Text;
        }
    }
}
