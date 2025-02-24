/*
 USER CLASS : Report - Layaway by Customer Listing
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
    public partial class repLayawayCust : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private double dblGroupPrice;
        private double dblGroupPayment;
        private double dblCustPrice;
        private double dblCustPayment;
        public repLayawayCust()
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
            if (intDecimalPlace == 3) rExtPrice.Text = GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f3");
            else rExtPrice.Text = GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f");

            dblGroupPrice = dblGroupPrice + GeneralFunctions.fnDouble(rExtPrice.Text);

            if (rExtPrice.Text.StartsWith("-"))
            {
                string strTemp = rExtPrice.Text.Remove(0, 1);
                rExtPrice.Text = "(" + strTemp + ")";
            }
        }

        private void xrTableCell12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell12.Text = GeneralFunctions.fnDouble(xrTableCell12.Text).ToString("f3");
            else xrTableCell12.Text = GeneralFunctions.fnDouble(xrTableCell12.Text).ToString("f");
            dblGroupPayment = dblGroupPayment + GeneralFunctions.fnDouble(xrTableCell12.Text);
            if (xrTableCell12.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell12.Text.Remove(0, 1);
                xrTableCell12.Text = "(" + strTemp + ")";
            }
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
            if (intDecimalPlace == 3) xrTableCell19.Text = GeneralFunctions.fnDouble(xrTableCell19.Text).ToString("f3");
            else xrTableCell19.Text = GeneralFunctions.fnDouble(xrTableCell19.Text).ToString("f");
            dblCustPrice = dblCustPrice + GeneralFunctions.fnDouble(xrTableCell19.Text);
            if (xrTableCell19.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell19.Text.Remove(0, 1);
                xrTableCell19.Text = "(" + strTemp + ")";
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            dblGroupPrice = 0;
            dblGroupPayment = 0;
            
        }

        private void xrTableCell30_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell30.Text = GeneralFunctions.fnDouble(xrTableCell30.Text).ToString("f3");
            else xrTableCell30.Text = GeneralFunctions.fnDouble(xrTableCell30.Text).ToString("f");
            if (xrTableCell30.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell30.Text.Remove(0, 1);
                xrTableCell30.Text = "(" + strTemp + ")";
            }
        }

        private void GroupFooter2_AfterPrint(object sender, EventArgs e)
        {
            dblCustPrice = 0;
            dblCustPayment = 0;
            
        }

        private void xrTableCell25_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell25.Text = GeneralFunctions.fnDouble(xrTableCell25.Text).ToString("f3");
            else xrTableCell25.Text = GeneralFunctions.fnDouble(xrTableCell25.Text).ToString("f");
            
            if (xrTableCell25.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell25.Text.Remove(0, 1);
                xrTableCell25.Text = "(" + strTemp + ")";
            }
            
        }

        private void xrTableCell26_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell26.Text = GeneralFunctions.fnDouble(xrTableCell26.Text).ToString("f3");
            else xrTableCell26.Text = GeneralFunctions.fnDouble(xrTableCell26.Text).ToString("f");
            if (xrTableCell26.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell26.Text.Remove(0, 1);
                xrTableCell26.Text = "(" + strTemp + ")";
            }
            //dblGroupPrice = 0;
            //dblGroupPayment = 0;
        }

        private void xrTableCell32_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell32.Text = GeneralFunctions.fnDouble(xrTableCell32.Text).ToString("f3");
            else xrTableCell32.Text = GeneralFunctions.fnDouble(xrTableCell32.Text).ToString("f");
            if (xrTableCell32.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell32.Text.Remove(0, 1);
                xrTableCell32.Text = "(" + strTemp + ")";
            }
        }

        private void xrTableCell33_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) xrTableCell33.Text = GeneralFunctions.fnDouble(xrTableCell33.Text).ToString("f3");
            else xrTableCell33.Text = GeneralFunctions.fnDouble(xrTableCell33.Text).ToString("f");
            if (xrTableCell33.Text.StartsWith("-"))
            {
                string strTemp = xrTableCell33.Text.Remove(0, 1);
                xrTableCell33.Text = "(" + strTemp + ")";
            }
            dblCustPrice = 0;
            dblCustPayment = 0;
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
        }

        private void rCEml_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void xrTableCell23_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell23.Text = GeneralFunctions.fnDate(xrTableCell23.Text).ToShortDateString();
        }

    }
}
