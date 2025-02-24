/*
 USER CLASS : Report - Stock Take
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
    public partial class repStockTake : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public repStockTake()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.DetailReport.GetCurrentColumnValue("BreakPackFlag").ToString() == "0") e.Cancel = true;
        }

        private void rExtCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f3");
            else
            {
                rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f");
            }

            if (rExtCost.Text.StartsWith("-"))
            {
                string strTemp = rExtCost.Text.Remove(0, 1);
                rExtCost.Text = "(" + strTemp + ")";
            }
        }

        private void rExtRetail_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtRetail.Text = GeneralFunctions.fnDouble(rExtRetail.Text).ToString("f3");
            else
            {
                rExtRetail.Text = GeneralFunctions.fnDouble(rExtRetail.Text).ToString("f");
            }

            if (rExtRetail.Text.StartsWith("-"))
            {
                string strTemp = rExtRetail.Text.Remove(0, 1);
                rExtRetail.Text = "(" + strTemp + ")";
            }
        }

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f3");
            else
            {
                rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f");
            }

            if (rMargin.Text.StartsWith("-"))
            {
                string strTemp = rMargin.Text.Remove(0, 1);
                rMargin.Text = "(" + strTemp + ")";
            }
        }

        private void xrTableCell17_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell17.Text = GeneralFunctions.fnDouble(xrTableCell17.Text).ToString("f3");
            else
            {
                xrTableCell17.Text = GeneralFunctions.fnDouble(xrTableCell17.Text).ToString("f");
            }

            if (xrTableCell17.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell17.Text.Remove(0, 1);
                xrTableCell17.Text = "(" + strTemp + ")";
            }
        }

        private void xrTableCell19_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell19.Text = GeneralFunctions.fnDouble(xrTableCell19.Text).ToString("f3");
            else
            {
                xrTableCell19.Text = GeneralFunctions.fnDouble(xrTableCell19.Text).ToString("f");
            }

            if (xrTableCell19.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell19.Text.Remove(0, 1);
                xrTableCell19.Text = "(" + strTemp + ")";
            }
        }

        private void Detail1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.DetailReport.GetCurrentColumnValue("BreakPackFlag").ToString() == "0") e.Cancel = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.DetailReport.GetCurrentColumnValue("DocComment").ToString() == "") e.Cancel = true;
        }

        private void repStockTake_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
