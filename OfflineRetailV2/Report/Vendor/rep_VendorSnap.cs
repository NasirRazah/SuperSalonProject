/*
 USER CLASS : Report - General Details of a Vendor
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
    public partial class repVendorSnap : DevExpress.XtraReports.UI.XtraReport
    {
        public repVendorSnap()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

    }
}
