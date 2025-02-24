/*
 USER CLASS : Report - Reorder Item Listing
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
namespace OfflineRetailV2.Report
{
    public partial class repReorder : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private int intPDecimalPlace;
        public repReorder()
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
            else
            {
                if (intPDecimalPlace == 2)
                    rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f");
                if (intPDecimalPlace == 3)
                    rCost.Text = GeneralFunctions.fnDouble(rCost.Text).ToString("f3");
            }

            if (rCost.Text.StartsWith("-"))
            {
                string strTemp = rCost.Text.Remove(0, 1);
                rCost.Text = "(" + strTemp + ")";
            }
        }

        private void rExtCost_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f3");
            else rExtCost.Text = GeneralFunctions.fnDouble(rExtCost.Text).ToString("f");

            //if (GeneralFunctions.fnDouble(rExtCost.Text) <= 0) rExtCost.Text = "";
        }

        private void rSQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rSQty.Text);
            rSQty.Text = d.ToString();
            //if (GeneralFunctions.fnDouble(rSQty.Text) <= 0) rSQty.Text = "";
        }

        private void rHQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double d = GeneralFunctions.fnDouble(rHQty.Text);
            rHQty.Text = d.ToString();
        }

        private void rRQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

            double d = GeneralFunctions.fnDouble(rRQty.Text);
            rRQty.Text = d.ToString();
            //if (GeneralFunctions.fnDouble(rRQty.Text) == 0) rRQty.Text = "";
        }

        private void rNQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            double d = GeneralFunctions.fnDouble(rNQty.Text);
            rNQty.Text = d.ToString();
            //if (GeneralFunctions.fnDouble(rNQty.Text) == 0) rNQty.Text = "";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (strGroupType == "V") rCheck1.Visible = true;
            else rCheck1.Visible = false;
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            intPDecimalPlace = GeneralFunctions.fnInt32(rDP.Text);
        }

    }
}
