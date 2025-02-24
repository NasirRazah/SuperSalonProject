/*
 USER CLASS : Report - Closeout (Sub Report 3)
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
    public partial class repCloseoutMain2 : DevExpress.XtraReports.UI.XtraReport
    {
        public repCloseoutMain2()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
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

        private void xrTableCell27_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell27);
        }

        private void xrTableCell33_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell33);
        }

        private void xrTableCell36_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell36);
        }

        private void xrTableCell42_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell42);
        }

        private void xrTableCell39_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell39);
        }

        private void xrTableCell44_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell44);
        }

        private void xrTableCell26_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell26);
        }

        private void xrTableCell30_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell30);
        }

        private void xrTableCell61_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell61);
        }

        private void xrTableCell67_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell67);
        }

        private void xrTableCell20_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell20);
        }

        private void xrTableCell12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (Settings.AcceptTips == "N") xrTableCell12.Text = "";
            else
            {
                xrTableCell12.Text = OfflineRetailV2.Properties.Resources.TIPS;
            }
        }

        private void xrTableCell10_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (Settings.AcceptTips == "N") xrTableCell10.Text = "";
        }

        private void xrTableCell17_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (Settings.POSCardPayment == "Y")
            {
                if (Settings.PaymentGateway != 1) FormatDouble(xrTableCell17);
                else xrTableCell17.Text = "";
            }
            else
            {
                xrTableCell17.Text = "";
            }
        }

        private void xrTableCell23_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell23);
        }

        private void xrTableCell32_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell16_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (Settings.POSCardPayment == "Y")
            {
                if (Settings.PaymentGateway != 1) 
                {
                    if (Settings.PaymentGateway == 2) xrTableCell16.Text = OfflineRetailV2.Properties.Resources.MercuryGiftCardSales;
                    if (Settings.PaymentGateway == 3) xrTableCell16.Text = OfflineRetailV2.Properties.Resources.PrecidiaGiftCardSales;
                    if (Settings.PaymentGateway == 5) xrTableCell16.Text = OfflineRetailV2.Properties.Resources.DatacapGiftCardSales;
                    if (Settings.PaymentGateway == 7) xrTableCell16.Text = OfflineRetailV2.Properties.Resources.POSLinkGiftCardSales;
                }
                else xrTableCell16.Text = "";
            }
            else
            {
                xrTableCell16.Text = "";
            }
        }

        private void xrTableCell32_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell32);
        }

        private void xrTableCell41_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell41);
        }
    }
}
