/*
 USER CLASS : Pre Printed Receipt (Tendering)
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
    public partial class repPPInvTendering : DevExpress.XtraReports.UI.XtraReport
    {
        public repPPInvTendering()
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

        private bool blChangeDue;
        public bool ChangeDue
        {
            get { return blChangeDue; }
            set { blChangeDue = value; }
        }

        private void rChangeDue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            /*if (blChangeDue)
            {
                if (intDecimalPlace == 3) rChangeDue.Text = "($" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f3") + " )";
                else
                {
                    if (intDecimalPlace == 2)
                        rChangeDue.Text = "($" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f") + " )";
                    if (intDecimalPlace == 3)
                        rChangeDue.Text = "($" + GeneralFunctions.fnDouble(rChangeDue.Text).ToString("f3") + " )";
                }
            }*/
        }

        private void rTenderAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTenderAmt.Text == "") return;
            if (intDecimalPlace == 3) rTenderAmt.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTenderAmt.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTenderAmt.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTenderAmt.Text).ToString("f3");
            }

            if (rTenderAmt.Text.StartsWith("-"))
            {
                string strTemp = rTenderAmt.Text.Remove(0, 1);
                rTenderAmt.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTotal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTotal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTotal.Text).ToString("f3");
            }

            if (rTotal.Text.StartsWith("-"))
            {
                string strTemp = rTotal.Text.Remove(0, 1);
                rTotal.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

    }
}
