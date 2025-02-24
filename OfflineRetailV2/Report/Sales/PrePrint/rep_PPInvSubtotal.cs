/*
 USER CLASS : Pre Printed Receipt (Total)
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
    public partial class repPPInvSubtotal : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public repPPInvSubtotal()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rSubTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rSubTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rSubTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rSubTotal.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rSubTotal.Text).ToString("f3");
            }

            if (rSubTotal.Text.StartsWith("-"))
            {
                string strTemp = rSubTotal.Text.Remove(0, 1);
                rSubTotal.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

    }
}
