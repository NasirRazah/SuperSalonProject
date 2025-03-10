/*
 USER CLASS : Report - Closeout (Return)
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
    public partial class repReturn : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private string flg = "N";
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repReturn()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void xrTableCell6_BeforePrint(object sender, CancelEventArgs e)
        {
            if (xrTableCell6.Text != "")
            {
                dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell6.Text);
                if (GeneralFunctions.fnDouble(xrTableCell6.Text) != 0) flg = "Y";
            }
        }

        private void xrTableCell8_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg == "N")
            {
                //xrTableCell8.Text = "";
            }
            else
            {
                //xrTableCell8.Text = "_______________";
            }
        }

        private void xrTableCell9_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg == "N")
            {
                xrTableCell9.Text = OfflineRetailV2.Properties.Resources.NoReturns;
            }
            else
            {
                xrTableCell9.Text = OfflineRetailV2.Properties.Resources.Total;
            }
        }

        private void rTotal_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg == "N")
            {
                rTotal.Text = "";
            }
            else
            {
                rTotal.Text = dblTotal.ToString();
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

        private void xrTableCell6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (flg == "Y")
                FormatDouble(xrTableCell6);
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (flg == "Y")
                FormatDouble(rTotal);
        }

    }
}
