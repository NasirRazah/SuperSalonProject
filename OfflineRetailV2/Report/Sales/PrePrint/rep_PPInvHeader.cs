/*
 USER CLASS : Pre Printed Receipt (Header)
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Sales.PrePrint
{
    public partial class repPPInvHeader : DevExpress.XtraReports.UI.XtraReport
    {
        public repPPInvHeader()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rDate.Text = GeneralFunctions.fnDate(rDate.Text).ToString(SystemVariables.DateFormat);
        }

        private void rTime_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTime.Text = GeneralFunctions.fnDate(rTime.Text).ToString("hh:mm tt");
        }

    }
}
