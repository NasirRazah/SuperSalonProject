/*
 USER CLASS : Report - Vendor Part No.
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
namespace OfflineRetailV2.Report.Vendor
{
    public partial class repVendorPart : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;

        public repVendorPart()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f3");
            else rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f");
        }

    }
}
