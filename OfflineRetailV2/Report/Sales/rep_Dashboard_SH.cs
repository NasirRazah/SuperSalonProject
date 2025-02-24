/*
 USER CLASS : Report - Sales Dashboard (Hourly Sales)
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
    public partial class repDashboard_SH : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_SH()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private double TSale = 0;
        private int TCount = 0;
       
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
            
            if (e.SeriesPoint.Argument == "Fake")
            {
                double sum = 0;
                for (int i = 0; i < e.Series.Points.Count; i++)
                {
                    sum = sum + e.Series.Points[i].Values[0];
                }
                e.LegendText = OfflineRetailV2.Properties.Resources.Total + " : " + SystemVariables.CurrencySymbol + GeneralFunctions.FormatDouble1(sum);
            }
            else
            {
                e.LegendText = e.SeriesPoint.Argument + "  " + GeneralFunctions.FormatDouble1(e.SeriesPoint.Values[0]) + "   (" + e.LegendText.Substring(e.LegendText.IndexOf(":") + 2) + ")";
            }
        }

        private void rTSales_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTSales.Text = GeneralFunctions.FormatDoubleForPrint(rTSales.Text);
            TSale = GeneralFunctions.fnDouble(rTSales.Text);
        }

        private void rTInv_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            TCount = GeneralFunctions.fnInt32(rTInv.Text);
        }

        private void rTAvg_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTAvg.Text = GeneralFunctions.FormatDouble1(TSale / TCount);
        }

        private void rSales_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rSales.Text = GeneralFunctions.FormatDoubleForPrint(rSales.Text);
        }

        private void rAvg_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            rAvg.Text = GeneralFunctions.FormatDoubleForPrint(rAvg.Text);
        }

    }
}
