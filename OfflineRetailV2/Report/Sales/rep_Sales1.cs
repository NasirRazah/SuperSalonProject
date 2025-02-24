/*
 USER CLASS : Report - Sales (1)
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
    public partial class repSales1 : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private double dblRev = 0;
        private double dblPft = 0;
        private double dblTRev = 0;
        private double dblTPft = 0;
        private double rTot1Amt = 0;
        private bool blPrint = false;



        public repSales1()
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
            /*if (blPrint)
            {
                blPrint = false;
                rTot1Amt = 0;
            }
            rTot1Amt = rTot1Amt + d;*/
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

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((rMargin.Text == "") || (rMargin.Text.Contains("%")) || (rMargin.Text.Contains("("))) return;
            if (GeneralFunctions.fnDouble(rMargin.Text) != -99999)
            {
                if (intDecimalPlace == 3) rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f3");
                else rMargin.Text = GeneralFunctions.fnDouble(rMargin.Text).ToString("f");
                rMargin.Text = rMargin.Text + "%";
                if (rMargin.Text.StartsWith("-"))
                {
                    string strTemp = rMargin.Text.Remove(0, 1);
                    rMargin.Text = "(" + strTemp + ")";
                }
            }
            else
            {
                rMargin.Text = "";
            }
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((rTot1.Text.EndsWith(".00")) || (rTot1.Text.EndsWith(".000")))
            {
                string temp = "";
                if (intDecimalPlace == 3) temp = rTot1.Text.Substring(0, rTot1.Text.Length - 4);
                if (intDecimalPlace == 2) temp = rTot1.Text.Substring(0, rTot1.Text.Length - 3);
                if (temp.StartsWith("-"))
                {
                    string strTemp = temp.Remove(0, 1);
                    rTot1.Text = "(" + strTemp + ")";
                }
                else
                {
                    rTot1.Text = temp;
                }
            }
            else
            {
                if (rTot1.Text.StartsWith("-"))
                {
                    string strTemp1 = rTot1.Text.Remove(0, 1);
                    rTot1.Text = "(" + strTemp1 + ")";
                }
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
            dblRev = GeneralFunctions.fnDouble(rTot3.Text);
            if (rTot3.Text.StartsWith("-"))
            {
                string strTemp = rTot3.Text.Remove(0, 1);
                rTot3.Text = "(" + strTemp + ")";
            }
        }

        private void rTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            dblPft = GeneralFunctions.fnDouble(rTot41.Text);
            if (rTot41.Text.StartsWith("-"))
            {
                string strTemp = rTot41.Text.Remove(0, 1);
                rTot41.Text = "(" + strTemp + ")";
            }
        }

        private void rTot5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (dblRev != 0)
            {
                double dblMargin = dblPft * 100 / dblRev;

                if (intDecimalPlace == 3) rTot5.Text = dblMargin.ToString("f3");
                else rTot5.Text = dblMargin.ToString("f");
                rTot5.Text = rTot5.Text + "%";
                if (rTot5.Text.StartsWith("-"))
                {
                    string strTemp = rTot5.Text.Remove(0, 1);
                    rTot5.Text = "(" + strTemp + ")";
                }
            }
            else rTot5.Text = "";
            dblRev = 0;
            dblPft = 0;
        }

        private void rTTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((rTTot1.Text.EndsWith(".00")) || (rTTot1.Text.EndsWith(".000")))
            {
                string temp = "";
                if (intDecimalPlace == 3) temp = rTTot1.Text.Substring(0, rTTot1.Text.Length - 4);
                if (intDecimalPlace == 2) temp = rTTot1.Text.Substring(0, rTTot1.Text.Length - 3);
                if (temp.StartsWith("-"))
                {
                    string strTemp = temp.Remove(0, 1);
                    rTTot1.Text = "(" + strTemp + ")";
                }
                else
                {
                    rTTot1.Text = temp;
                }
            }
            else
            {
                if (rTTot1.Text.StartsWith("-"))
                {
                    string strTemp1 = rTTot1.Text.Remove(0, 1);
                    rTTot1.Text = "(" + strTemp1 + ")";
                }
            }
        }

        private void rTTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTTot2.Text.StartsWith("-"))
            {
                string strTemp = rTTot2.Text.Remove(0, 1);
                rTTot2.Text = "(" + strTemp + ")";
            }

        }

        private void rTTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            dblTRev = GeneralFunctions.fnDouble(rTTot3.Text);
            if (rTTot3.Text.StartsWith("-"))
            {
                string strTemp = rTTot3.Text.Remove(0, 1);
                rTTot3.Text = "(" + strTemp + ")";
            }
        }

        private void rTTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            dblTPft = GeneralFunctions.fnDouble(rTTot4.Text);
            if (rTTot4.Text.StartsWith("-"))
            {
                string strTemp = rTTot4.Text.Remove(0, 1);
                rTTot4.Text = "(" + strTemp + ")";
            }
        }

        private void rTTot5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rGroupIDCaption_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rGroupDesc_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strGroupType == "F") && (rGroupDesc.Text == ""))
            {
                rGroupDesc.Text = OfflineRetailV2.Properties.Resources.None;
            }
        }

        private void xrTableCell3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTot41_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void xrTableCell2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strGroupType == "F") && (rGroupDesc1.Text == ""))
            {
                rGroupDesc1.Text = "(None)";
            }
        }

        private void rTot5_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void rTot5_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            if (dblRev != 0)
            {
                double dblMargin = dblPft * 100 / dblRev;

                if (intDecimalPlace == 3) rTot5.Text = dblMargin.ToString("f3");
                else rTot5.Text = dblMargin.ToString("f");
                rTot5.Text = rTot5.Text + "%";
                if (rTot5.Text.StartsWith("-"))
                {
                    string strTemp = rTot5.Text.Remove(0, 1);
                    rTot5.Text = "(" + strTemp + ")";
                }
            }
            else rTot5.Text = "";
            dblRev = 0;
            dblPft = 0;
        }

        private void rTTot5_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            if (dblTRev != 0)
            {
                double dblMargin = dblTPft * 100 / dblTRev;

                if (intDecimalPlace == 3) rTTot5.Text = dblMargin.ToString("f3");
                else rTTot5.Text = dblMargin.ToString("f");
                rTTot5.Text = rTTot5.Text + "%";
                if (rTTot5.Text.StartsWith("-"))
                {
                    string strTemp = rTTot5.Text.Remove(0, 1);
                    rTTot5.Text = "(" + strTemp + ")";
                }
            }
            else rTTot5.Text = "";
            dblTRev = 0;
            dblTPft = 0;
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

        private void rTot6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot6.Text.StartsWith("-"))
            {
                string strTemp = rTot6.Text.Remove(0, 1);
                rTot6.Text = "(" + strTemp + ")";
            }
        }

        private void rTTot6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTTot6.Text.StartsWith("-"))
            {
                string strTemp = rTTot6.Text.Remove(0, 1);
                rTTot6.Text = "(" + strTemp + ")";
            }
        }
    }
}
