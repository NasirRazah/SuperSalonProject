/*
 USER CLASS : Report - Matrix Item Listing
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
namespace OfflineRetailV2.Report.Product
{
    public partial class repMatrixReport : DevExpress.XtraReports.UI.XtraReport
    {
        private bool vis = true;
        private int intDecimalPlace;
        private int intPDecimalPlace;
        private double rTot1Amt = 0;
        private double rAmt = 0;
        private bool blPrint = false;
        public repMatrixReport()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rQty.Text);
            rQty.Text = d.ToString();
            if (blPrint)
            {
                blPrint = false;
                rTot1Amt = 0;
            }
            rTot1Amt = rTot1Amt + d;
            rAmt = rAmt + d;
            
        }

        private void rmQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (GeneralFunctions.fnDouble(rmQty.Text) != -99999)
            {
                double d = Math.Truncate(GeneralFunctions.fnDouble(rmQty.Text));
                rmQty.Text = d.ToString();
            }
            else
            {
                rmQty.Text = "";
            }
        }

        private void rUnitCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rUnitCost.Text = GeneralFunctions.fnDouble(rUnitCost.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rUnitCost.Text = GeneralFunctions.fnDouble(rUnitCost.Text).ToString("f");
                else
                    rUnitCost.Text = GeneralFunctions.fnDouble(rUnitCost.Text).ToString("f3");
            }

            if (rUnitCost.Text.StartsWith("-"))
            {
                string strTemp = rUnitCost.Text.Remove(0, 1);
                rUnitCost.Text = "(" + strTemp + ")";
            }
        }

        private void rUnitRetailCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rUnitRetailCost.Text = GeneralFunctions.fnDouble(rUnitRetailCost.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rUnitRetailCost.Text = GeneralFunctions.fnDouble(rUnitRetailCost.Text).ToString("f");
                else
                    rUnitRetailCost.Text = GeneralFunctions.fnDouble(rUnitRetailCost.Text).ToString("f3");
            }


            if (rUnitRetailCost.Text.StartsWith("-"))
            {
                string strTemp = rUnitRetailCost.Text.Remove(0, 1);
                rUnitRetailCost.Text = "(" + strTemp + ")";
            }
        }

        private void rExtCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f3");
            else
            {
                rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f");
            }

            if (rExtCost.Text.StartsWith("-"))
            {
                string strTemp = rExtCost.Text.Remove(0, 1);
                rExtCost.Text = "(" + strTemp + ")";
            }
        }

        private void rExtRetail_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtRetail.Text = GeneralFunctions.fnDouble(rExtRetail.Text).ToString("f3");
            else
            {
                rExtRetail.Text = GeneralFunctions.fnDouble(rExtRetail.Text).ToString("f");
            }

            if (rExtRetail.Text.StartsWith("-"))
            {
                string strTemp = rExtRetail.Text.Remove(0, 1);
                rExtRetail.Text = "(" + strTemp + ")";
            }
        }

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((rMargin.Text == "") || (rMargin.Text.Contains("%")) || (rMargin.Text.Contains("("))) return;
            if (GeneralFunctions.fnDouble(rMargin.Text) != -99999)
            {
                if (intDecimalPlace == 3) rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f3");
                else
                {
                    rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f");
                }
                rMargin.Text = rMargin.Text + "%";
            }
            else
            {
                rMargin.Text = "";
            }
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTot1.Text = rTot1Amt.ToString();
            blPrint = true;
        }

        private void rTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot2.Text.StartsWith("-"))
            {
                string strTemp = rTot2.Text.Remove(0, 1);
                rTot2.Text = "(" + strTemp + ")";
            }
        }

        private void rTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot3.Text.StartsWith("-"))
            {
                string strTemp = rTot3.Text.Remove(0, 1);
                rTot3.Text = "(" + strTemp + ")";
            }
        }

        private void rRTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rRTot1.Text = rAmt.ToString();
        }

        private void rRTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rRTot2.Text.StartsWith("-"))
            {
                string strTemp = rRTot2.Text.Remove(0, 1);
                rRTot2.Text = "(" + strTemp + ")";
            }
        }

        private void rRTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rRTot3.Text.StartsWith("-"))
            {
                string strTemp = rRTot3.Text.Remove(0, 1);
                rRTot3.Text = "(" + strTemp + ")";
            }
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            intPDecimalPlace = GeneralFunctions.fnInt32(rDP.Text);
        }

        private void rSKU_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void DetailReport_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
