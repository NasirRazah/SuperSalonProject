/*
 USER CLASS : Report - Receipt (Card Transaction Info)
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
    public partial class repInvCC : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvCC()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
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

        private void rAct_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rAct.Text = GeneralFunctions.GetLast(rAct.Text, 4);
        }
    }
}
