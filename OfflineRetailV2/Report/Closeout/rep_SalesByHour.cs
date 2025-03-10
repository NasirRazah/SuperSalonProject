/*
 USER CLASS : Report - Closeout (Sales by Hour)
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
    public partial class repSalesByHour : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        private string strCT;
        public string COType
        {
            get { return strCT; }
            set { strCT = value; }
        }
        public repSalesByHour()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
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

        private void rTotal_BeforePrint(object sender, CancelEventArgs e)
        {
            rTotal.Text = dblTotal.ToString();
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

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(rTotal);
        }

        private void xrTableCell24_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "C")
                xrTableCell24.Text = OfflineRetailV2.Properties.Resources.Requestor;
            if (strCT == "T")
                xrTableCell24.Text = OfflineRetailV2.Properties.Resources.Terminal;
            if (strCT == "E")
                xrTableCell24.Text = OfflineRetailV2.Properties.Resources.Employee;
        }

        private void xrTableCell25_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "C")
            {
                if (SystemVariables.CurrentUserID <= 0)
                    xrTableCell25.Text = SystemVariables.CurrentUserName;
                else
                    xrTableCell25.Text = SystemVariables.CurrentUserCode;
            }
            if (strCT == "E")
            {
                xrTableCell26.Width = 0;
                xrTableCell26.Visible = false;
                xrTableCell25.Width = 192;
                xrTableCell25.Visible = true;
                xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            }

            if (strCT == "T")
            {
                xrTableCell25.Width = 0;
                xrTableCell25.Visible = false;
                xrTableCell26.Width = 192;
                xrTableCell26.Visible = true;
            }
        }

        private void xrTableCell26_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "C")
            {
                xrTableCell26.Width = 0;
                xrTableCell26.Visible = false;
                xrTableCell25.Width = 192;
                xrTableCell25.Visible = true;
            }
            if (strCT == "E")
            {
                xrTableCell26.Width = 0;
                xrTableCell26.Visible = false;
                xrTableCell25.Width = 192;
                xrTableCell25.Visible = true;
            }
            if (strCT == "T")
            {
                xrTableCell25.Width = 0;
                xrTableCell25.Visible = false;
                xrTableCell26.Width = 192;
                xrTableCell26.Visible = true;
            }
        }
    }
}
