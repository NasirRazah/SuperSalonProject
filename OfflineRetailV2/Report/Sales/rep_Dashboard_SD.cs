/*
 USER CLASS : Report - Sales Dashboard (Sale by Item Department)
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
    public partial class repDashboard_SD : DevExpress.XtraReports.UI.XtraReport
    {
        public repDashboard_SD()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

    }
}
