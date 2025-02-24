/*
 USER CLASS : Report - Receipt (Rent Item Return)
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
    public partial class repInvRentReturnLine : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvRentReturnLine()
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

        private void rlqty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rlqty.Text);
            rlqty.Text = d.ToString();
            if (rlqty.Text.StartsWith("-"))
            {
                string strTemp = rlqty.Text.Remove(0, 1);
                rlqty.Text = strTemp;
            }
        }

    }
}
