/*
 USER CLASS : Report - Sales Dashboard (Tax Summary)
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
    public partial class repDashboard_TX : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_TX()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
 
       
        private void PageHeader_AfterPrint(object sender, EventArgs e)
        {
           
           
        }

        private void xrLabel1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrLabel2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrChart2_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {
            
            
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTaxable.Text = GeneralFunctions.FormatDoubleForPrint(rTaxable.Text);
        }

        private void rAvg_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTax.Text = GeneralFunctions.FormatDoubleForPrint(rTax.Text);
        }

    }
}
