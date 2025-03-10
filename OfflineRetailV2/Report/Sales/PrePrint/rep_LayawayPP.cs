/*
 USER CLASS : Pre Printed Layaway Receipt
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
namespace OfflineRetailV2.Report.Layaway
{
    public partial class repLayawayPP : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private double dblGroupPrice;
        private double dblGroupPayment;
        private double dblCustPrice;
        private double dblCustPayment;
        public repLayawayPP()
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

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void Detail2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rExtPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtPrice.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f3");
            else rExtPrice.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f");

            dblGroupPrice = dblGroupPrice + GeneralFunctions.fnDouble(rExtPrice.Text);

            if (rExtPrice.Text.StartsWith("-"))
            {
                string strTemp = rExtPrice.Text.Remove(0, 1);
                rExtPrice.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

        private void xrTableCell12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            dblCustPrice = 0;
            dblCustPayment = 0;
            
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            dblGroupPrice = 0;
            dblGroupPayment = 0;
            
        }

        private void xrTableCell19_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            dblGroupPrice = 0;
            dblGroupPayment = 0;
            
        }

        private void xrTableCell30_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void GroupFooter2_AfterPrint(object sender, EventArgs e)
        {
            dblCustPrice = 0;
            dblCustPayment = 0;
            
        }

        private void xrTableCell25_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            
        }

        private void xrTableCell26_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell32_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell33_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell33.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell33.Text).ToString("f3");
            else xrTableCell33.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell33.Text).ToString("f");
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
        }

        private void xrTableCell2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell2.Text = GeneralFunctions.fnDate(xrTableCell2.Text).ToString(SystemVariables.DateFormat);
        }

        private void xrTableCell16_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell16.Text = GeneralFunctions.fnDate(xrTableCell16.Text).ToString("hh:mm tt");
        }

        private void xrTableCell12_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell12.Text = "Invoice Number: " + xrTableCell12.Text;
        }

        private void xrTableCell10_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void xrTableCell14_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell14.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell14.Text).ToString("f3");
            else xrTableCell14.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell14.Text).ToString("f");
        }

        private void xrTableCell13_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell13.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell13.Text).ToString("f3");
            else xrTableCell13.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(xrTableCell13.Text).ToString("f");
        }

    }
}
