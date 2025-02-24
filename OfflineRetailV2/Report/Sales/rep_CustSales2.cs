/*
 USER CLASS : Report - Customer Sale Listing (1)
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
    public partial class repCustSales2 : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private double rTot1Amt = 0;
        private double rAmt = 0;
        private bool blPrint = false;
        private bool blPrint1 = false;

        public repCustSales2()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public string GroupType
        {
            get { return strGroupType; }
            set { strGroupType = value; }
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
            if (blPrint1)
            {
                blPrint1 = false;
                rAmt = 0;
            }
            rTot1Amt = rTot1Amt + d;
            rAmt = rAmt + d;
        }

        private void rCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f3");
            else rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f");

            if (rCost.Text.StartsWith("-"))
            {
                string strTemp = rCost.Text.Remove(0, 1);
                rCost.Text = "(" + strTemp + ")";
            }
        }

        private void rRevenue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rRevenue.Text = GeneralFunctions.fnDouble(rRevenue.Text).ToString("f3");
            else rRevenue.Text = GeneralFunctions.fnDouble(rRevenue.Text).ToString("f");
            if (rRevenue.Text.StartsWith("-"))
            {
                string strTemp = rRevenue.Text.Remove(0, 1);
                rRevenue.Text = "(" + strTemp + ")";
            }
        }

        private void rProfit_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rProfit.Text = GeneralFunctions.fnDouble(rProfit.Text).ToString("f3");
            else rProfit.Text = GeneralFunctions.fnDouble(rProfit.Text).ToString("f");

            if (rProfit.Text.StartsWith("-"))
            {
                string strTemp = rProfit.Text.Remove(0, 1);
                rProfit.Text = "(" + strTemp + ")";
            }
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

        private void rTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot4.Text.StartsWith("-"))
            {
                string strTemp = rTot4.Text.Remove(0, 1);
                rTot4.Text = "(" + strTemp + ")";
            }
        }

        private void rsTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rsTot2.Text.StartsWith("-"))
            {
                string strTemp = rsTot2.Text.Remove(0, 1);
                rsTot2.Text = "(" + strTemp + ")";
            }
        }

        private void rsTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rsTot3.Text.StartsWith("-"))
            {
                string strTemp = rsTot3.Text.Remove(0, 1);
                rsTot3.Text = "(" + strTemp + ")";
            }
        }

        private void rsTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rsTot4.Text.StartsWith("-"))
            {
                string strTemp = rsTot4.Text.Remove(0, 1);
                rsTot4.Text = "(" + strTemp + ")";
            }
        }

        private void rsTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rsTot1.Text = rTot1Amt.ToString();
            blPrint = true;
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTot1.Text = rAmt.ToString();
            blPrint1 = true;
        }

        private void rDCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDCost.Text = GeneralFunctions.fnDouble(rDCost.Text).ToString("f3");
            else rDCost.Text = GeneralFunctions.fnDouble(rDCost.Text).ToString("f");

            if (rDCost.Text.StartsWith("-"))
            {
                string strTemp = rDCost.Text.Remove(0, 1);
                rDCost.Text = "(" + strTemp + ")";
            }
        }

        private void rsTot6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rsTot6.Text.StartsWith("-"))
            {
                string strTemp = rsTot6.Text.Remove(0, 1);
                rsTot6.Text = "(" + strTemp + ")";
            }
        }

        private void rTot6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot6.Text.StartsWith("-"))
            {
                string strTemp = rTot6.Text.Remove(0, 1);
                rTot6.Text = "(" + strTemp + ")";
            }
        }
    }
}
