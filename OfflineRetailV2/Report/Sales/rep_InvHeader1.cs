/*
 USER CLASS : Report - Receipt (Header 1)
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
    public partial class repInvHeader1 : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvHeader1()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rOrderDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rOrderDate.Text = GeneralFunctions.fnDate(rOrderDate.Text).ToShortDateString();
        }

    }
}
