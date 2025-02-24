/*
 USER CLASS : Report - Paid In Out
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

namespace OfflineRetailV2.Report.Misc
{
    public partial class repPaidInOut : DevExpress.XtraReports.UI.XtraReport
    {
        public repPaidInOut()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        
        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void grpRepairType_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (grpType.Text == "6") grpType.Text = "Paid Out";
            if (grpType.Text == "66") grpType.Text = "Paid In";
        }

        private void rAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rAmount.Text = GeneralFunctions.fnDouble(rAmount.Text).ToString("f");

            if (rAmount.Text.StartsWith("-"))
            {
                string strTemp = rAmount.Text.Remove(0, 1);
                rAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTotal.Text = GeneralFunctions.fnDouble(rTotal.Text).ToString("f");

            if (rTotal.Text.StartsWith("-"))
            {
                string strTemp = rTotal.Text.Remove(0, 1);
                rTotal.Text = "(" + strTemp + ")";
            }
        }
    }
}
