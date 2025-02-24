/*
 USER CLASS : Pre Printed Receipt (Main)
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
    public partial class repPPInv : DevExpress.XtraReports.UI.XtraReport
    {
        public repPPInv()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

    }
}
