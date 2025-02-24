/*
 USER CLASS : Report - Receipt (Mercury Gift Card Balance)
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
    public partial class repInvMGC : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repInvMGC()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rGCAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rGCAmt.Text = GeneralFunctions.fnDouble(rGCAmt.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rGCAmt.Text = GeneralFunctions.fnDouble(rGCAmt.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rGCAmt.Text = GeneralFunctions.fnDouble(rGCAmt.Text).ToString("f3");
            }

            if (rGCAmt.Text.StartsWith("-"))
            {
                string strTemp = rGCAmt.Text.Remove(0, 1);
                rGCAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rGCName_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

    }
}
