/*
 USER CLASS : Report - Receipt (Tax Info)
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
    public partial class repInvTax : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repInvTax()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void rDTax2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            else
            {
                if (intDecimalPlace == 2)
                    rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f");
                if (intDecimalPlace == 3)
                    rDTax2.Text = GeneralFunctions.fnDouble(rDTax2.Text).ToString("f3");
            }

            if (rDTax2.Text.StartsWith("-"))
            {
                string strTemp = rDTax2.Text.Remove(0, 1);
                rDTax2.Text = "(" + strTemp + ")";
            }
        }

    }
}
