/*
 USER CLASS : Report - Sales Dashboard (Sale by Item Category)
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
    public partial class repDashboard_SC : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_SC()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private string currcolor = "";

        private void xrLabel1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            currcolor = xrLabel1.Text;
        }

        private void xrLabel2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrLabel2.BackColor = Color.FromArgb(Convert.ToInt32(currcolor));
        }

        private void xrLabel4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrLabel4.Text = GeneralFunctions.FormatDoubleForPrint(xrLabel4.Text);
        }

        private void xrLabel6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrLabel6.Text = GeneralFunctions.FormatDoubleForPrint(xrLabel6.Text) + " %";
        }

        private void rTPerc_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

    }
}
