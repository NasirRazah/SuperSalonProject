/*
 USER CLASS : Pre Printed Receipt (Tax)
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
    public partial class repPPInvTax : DevExpress.XtraReports.UI.XtraReport
    {
        public repPPInvTax()
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

        private void rDTax2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDTax2.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rDTax2.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rDTax2.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rDTax2.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            }

            if (rDTax2.Text.StartsWith("-"))
            {
                string strTemp = rDTax2.Text.Remove(0, 1);
                rDTax2.Text = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
        }

    }
}
