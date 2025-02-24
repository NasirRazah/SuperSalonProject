/*
 USER CLASS : Pre Printed Layaway Receipt (Item)
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
    public partial class repPPLayLine : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public repPPLayLine()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
        }

        private void rRate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rRate.Text == "") return;
            if (intDecimalPlace == 3) rRate.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rRate.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rRate.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rRate.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rRate.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rRate.Text).ToString("f3");
            }

            if (rRate.Text.StartsWith("-"))
            {
                string strTemp = rRate.Text.Remove(0, 1);
                rRate.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

        private void rTot_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot.Text == "") return;
            if (intDecimalPlace == 3) rTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTot.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTot.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTot.Text).ToString("f3");
            }

            if (rRate.Text.StartsWith("-"))
            {
                string strTemp = rTot.Text.Remove(0, 1);
                rTot.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }

        }

    }
}
