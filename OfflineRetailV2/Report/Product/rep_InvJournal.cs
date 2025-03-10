/*
 USER CLASS : Report - Inventory Journal Listing
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
    public partial class repInvJournal : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private int intPDecimalPlace;
        private string strNotes;
        private string strN = "N";
        public repInvJournal()
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

        public string Notes
        {
            get { return strNotes; }
            set { strNotes = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rC_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rC.Text = GeneralFunctions.fnDouble(rC.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rC.Text = GeneralFunctions.fnDouble(rC.Text).ToString("f");
                if (intPDecimalPlace == 3)
                    rC.Text = GeneralFunctions.fnDouble(rC.Text).ToString("f3");
            }

            if (rC.Text.StartsWith("-"))
            {
                string strTemp = rC.Text.Remove(0, 1);
                rC.Text = "(" + strTemp + ")";
            }
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            intPDecimalPlace = GeneralFunctions.fnInt32(rDP.Text);
        }

        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (strNotes == "N")
            {
                xrTableRow3.Height = 2;
                rNotes.Height = 2;
                rNotes.Visible = false;
                xrTableRow3.Visible = false;
                tabNotes.Visible = false;
                Detail.Height = 2;
            }
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
           
        }

        private void rNotes_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strNotes == "Y") && (rNotes.Text.Trim() != "") && (rNotes.Text.Trim() != "From Data Import"))
            {
                rNotes.Text = "  " + OfflineRetailV2.Properties.Resources.Notes + " : " + rNotes.Text.Trim();
            }
            else
            {
                rNotes.Text = "";
            }
        }

        private void tabNotes_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rID_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rDC_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDC.Text = GeneralFunctions.fnDouble(rDC.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rDC.Text = GeneralFunctions.fnDouble(rDC.Text).ToString("f");
                if (intPDecimalPlace == 3)
                    rDC.Text = GeneralFunctions.fnDouble(rDC.Text).ToString("f3");
            }

            if (rDC.Text.StartsWith("-"))
            {
                string strTemp = rDC.Text.Remove(0, 1);
                rDC.Text = "(" + strTemp + ")";
            }
        }

        private void rSale_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rSale.Text = GeneralFunctions.fnDouble(rSale.Text).ToString("f3");
            else
            {
                if (intPDecimalPlace == 2)
                    rSale.Text = GeneralFunctions.fnDouble(rSale.Text).ToString("f");
                if (intPDecimalPlace == 3)
                    rSale.Text = GeneralFunctions.fnDouble(rSale.Text).ToString("f3");
            }

            if (rSale.Text.StartsWith("-"))
            {
                string strTemp = rSale.Text.Remove(0, 1);
                rSale.Text = "(" + strTemp + ")";
            }
        }
    }
}
