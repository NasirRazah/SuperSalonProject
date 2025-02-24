/*
 USER CLASS : Report - Receipt (Signature)
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
    public partial class repInvSign : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvSign()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

    }
}
