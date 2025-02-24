/*
 USER CLASS : Report - Receipt (EBT Balance Info)
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
    public partial class repInvEBT : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repInvEBT()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rGCAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rEBTBal.Text = GeneralFunctions.fnDouble(rEBTBal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rEBTBal.Text = GeneralFunctions.fnDouble(rEBTBal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rEBTBal.Text = GeneralFunctions.fnDouble(rEBTBal.Text).ToString("f3");
            }

            if (rEBTBal.Text.StartsWith("-"))
            {
                string strTemp = rEBTBal.Text.Remove(0, 1);
                rEBTBal.Text = "(" + strTemp + ")";
            }
        }

        private void rGCName_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rEBTCard.Text = OfflineRetailV2.Properties.Resources.EBTCard + rEBTCard.Text;
        }

    }
}
