/*
 USER CLASS : Report - Receipt (Rent Return Deposit)
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
    public partial class repInvRentReturnSubTotal : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvRentReturnSubTotal()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        private void rReturnDeposit_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rReturnDeposit.Text = GeneralFunctions.fnDouble(rReturnDeposit.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2) rReturnDeposit.Text = GeneralFunctions.fnDouble(rReturnDeposit.Text).ToString("f");
                if (intDecimalPlace == 3) rReturnDeposit.Text = GeneralFunctions.fnDouble(rReturnDeposit.Text).ToString("f3");
            }

            if (rReturnDeposit.Text.StartsWith("-"))
            {
                string strTemp = rReturnDeposit.Text.Remove(0, 1);
                rReturnDeposit.Text = "(" + strTemp + ")";
            }
        }

    }
}
