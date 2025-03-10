/*
 USER CLASS : Report - Dashboard (Sales by Hour)
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

namespace OfflineRetailV2.Report.Closeout
{
    public partial class repdbsalesbyhour : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repdbsalesbyhour()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        private void xrTableCell21_BeforePrint(object sender, CancelEventArgs e)
        {
            if (xrTableCell21.Text != "")
            {
                dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell21.Text);
            }
        }

        private void FormatDouble(XRTableCell tabcl)
        {
            if (tabcl.Text != "")
            {
                if (intDecimalPlace == 3) tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f3");
                else tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f");

                if (tabcl.Text.StartsWith("-"))
                {
                    string strTemp = tabcl.Text.Remove(0, 1);
                    tabcl.Text = "(" + strTemp + ")";
                }
            }
        }

        private void xrTableCell21_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell21);
        }

        private void rTotal_BeforePrint(object sender, CancelEventArgs e)
        {
            rTotal.Text = dblTotal.ToString();
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(rTotal);
        }

    }
}
