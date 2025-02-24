/*
 USER CLASS : Report - Dashboard (Other)
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
    public partial class repdbothers : DevExpress.XtraReports.UI.XtraReport
    {
        public repdbothers()
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

        private void xrTableCell8_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell56_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }
    }
}
