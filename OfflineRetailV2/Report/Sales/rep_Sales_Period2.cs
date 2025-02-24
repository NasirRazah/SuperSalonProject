/*
 USER CLASS : Report - Sales by Period (2)
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
    public partial class repSales_Period2 : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private double dblRev = 0;
        private double dblPft = 0;
        private double dblTRev = 0;
        private double dblTPft = 0;
        private double rTot1Amt = 0;
        private bool blPrint = false;

        private string PrevGroupText = "";

        public repSales_Period2()
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

        private void rTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTot5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
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
            if ((rTTot5.Text == "") || (rTTot5.Text.Contains("%")) || (rTTot5.Text.Contains("("))) return;
            if (GeneralFunctions.fnDouble(rTTot5.Text) != -99999)
            {
                if (intDecimalPlace == 3) rTTot5.Text = GeneralFunctions.fnDouble(rTTot5.Text).ToString("f3");
                else rTTot5.Text = GeneralFunctions.fnDouble(rTTot5.Text).ToString("f");
                rTTot5.Text = rTTot5.Text + "%";
                if (rTTot5.Text.StartsWith("-"))
                {
                    string strTemp = rTTot5.Text.Remove(0, 1);
                    rTTot5.Text = "(" + strTemp + ")";
                }
            }
            else
            {
                rTTot5.Text = "";
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rGroupID_PreviewMouseUp(object sender, PreviewMouseEventArgs e)
        {

        }

        private void rGroupID_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (PrevGroupText == rGroupID.Text)
            {
                rGroupID.Text = "";
            }
            else
            {
                PrevGroupText = rGroupID.Text;
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            
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
