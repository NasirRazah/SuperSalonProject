/*
 USER CLASS : Report - Receipt (Ticket Dicount Info)
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
namespace OfflineRetailV2.Report.Sales
{
    public partial class repInvCoupon : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public repInvCoupon()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rDTax2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            }

            if (rDTax2.Text.StartsWith("-"))
            {
                string strTemp = rDTax2.Text.Remove(0, 1);
                rDTax2.Text = "(" + strTemp + ")";
            }
        }

        private void rAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rAmt.Text = GeneralFunctions.fnDouble(rAmt.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rAmt.Text = GeneralFunctions.fnDouble(rAmt.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rAmt.Text = GeneralFunctions.fnDouble(rAmt.Text).ToString("f3");
            }

            if (rAmt.Text.StartsWith("-"))
            {
                string strTemp = rAmt.Text.Remove(0, 1);
                rAmt.Text = "(" + strTemp + ")";
            }
        }

        

    }
}
