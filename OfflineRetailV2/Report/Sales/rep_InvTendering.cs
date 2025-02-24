/*
 USER CLASS : Report - Receipt (Tendering)
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
    public partial class repPPInvTendering : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private bool blChangeDue;
        public bool ChangeDue
        {
            get { return blChangeDue; }
            set { blChangeDue = value; }
        }

        public repPPInvTendering()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTotal.Text = GeneralFunctions.fnDouble(rTotal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTotal.Text = GeneralFunctions.fnDouble(rTotal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTotal.Text = GeneralFunctions.fnDouble(rTotal.Text).ToString("f3");
            }

            if (rTotal.Text.StartsWith("-"))
            {
                string strTemp = rTotal.Text.Remove(0, 1);
                rTotal.Text = "(" + strTemp + ")";
            }
        }

        private void rTenderAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTenderAmt.Text == "") return;
            if (intDecimalPlace == 3) rTenderAmt.Text = GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTenderAmt.Text = GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTenderAmt.Text = GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f3");
            }

            if (rTenderAmt.Text.StartsWith("-"))
            {
                string strTemp = rTenderAmt.Text.Remove(0, 1);
                rTenderAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rChangeDue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (blChangeDue)
            {
                if (intDecimalPlace == 3) rChangeDue.Text = "(" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f3") + " )";
                else
                {
                    if (intDecimalPlace == 2)
                        rChangeDue.Text = "(" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f") + " )";
                    if (intDecimalPlace == 3)
                        rChangeDue.Text = "(" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f3") + " )";
                }
            }
        }

        private void rAdvance_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rAdvance.Text == "") return;
            if (intDecimalPlace == 3) rAdvance.Text = GeneralFunctions.fnDouble(rAdvance.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rAdvance.Text = GeneralFunctions.fnDouble(rAdvance.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rAdvance.Text = GeneralFunctions.fnDouble(rAdvance.Text).ToString("f3");
            }

            if (rAdvance.Text.StartsWith("-"))
            {
                string strTemp = rAdvance.Text.Remove(0, 1);
                rAdvance.Text = "(" + strTemp + ")";
            }
        }

        private void rFees_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rFees.Text = GeneralFunctions.fnDouble(rFees.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rFees.Text = GeneralFunctions.fnDouble(rFees.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rFees.Text = GeneralFunctions.fnDouble(rFees.Text).ToString("f3");
            }

            if (rTotal.Text.StartsWith("-"))
            {
                string strTemp = rTotal.Text.Remove(0, 1);
                rTotal.Text = "(" + strTemp + ")";
            }
        }

        private void rFeeTx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rFeeTx.Text = GeneralFunctions.fnDouble(rFeeTx.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rFeeTx.Text = GeneralFunctions.fnDouble(rFeeTx.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rFeeTx.Text = GeneralFunctions.fnDouble(rFeeTx.Text).ToString("f3");
            }

            if (rFeeTx.Text.StartsWith("-"))
            {
                string strTemp = rFeeTx.Text.Remove(0, 1);
                rFeeTx.Text = "(" + strTemp + ")";
            }
        }

        private void rDue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rDue.Text == "") return;
            if (intDecimalPlace == 3) rDue.Text = GeneralFunctions.fnDouble(rDue.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rDue.Text = GeneralFunctions.fnDouble(rDue.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rDue.Text = GeneralFunctions.fnDouble(rDue.Text).ToString("f3");
            }

            if (rDue.Text.StartsWith("-"))
            {
                string strTemp = rDue.Text.Remove(0, 1);
                rDue.Text = "(" + strTemp + ")";
            }
        }

    }
}
