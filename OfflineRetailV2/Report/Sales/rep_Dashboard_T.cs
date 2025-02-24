/*
 USER CLASS : Report - Sales Dashboard (Tender Reconciliation)
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
    public partial class repDashboard_T : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_T()
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

    }
}
