
/*
 USER CLASS : Report - Sales Discount on Ticket Listing
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
    public partial class repInvoiceDiscount : DevExpress.XtraReports.UI.XtraReport
    {
        public repInvoiceDiscount()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private int intDecimalPlace;
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

        private void rDiscAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rDiscAmt.Text = GeneralFunctions.fnDouble(rDiscAmt.Text).ToString("f3");
            else rDiscAmt.Text = GeneralFunctions.fnDouble(rDiscAmt.Text).ToString("f");

            if (rDiscAmt.Text.StartsWith("-"))
            {
                string strTemp = rDiscAmt.Text.Remove(0, 1);
                rDiscAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rGroupDateAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rGroupDateAmount.Text = GeneralFunctions.fnDouble(rGroupDateAmount.Text).ToString("f3");
            else rGroupDateAmount.Text = GeneralFunctions.fnDouble(rGroupDateAmount.Text).ToString("f");

            if (rGroupDateAmount.Text.StartsWith("-"))
            {
                string strTemp = rGroupDateAmount.Text.Remove(0, 1);
                rGroupDateAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rGroupInvAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void rTot_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot.Text.StartsWith("-"))
            {
                string strTemp = rTot.Text.Remove(0, 1);
                rTot.Text = "(" + strTemp + ")";
            }
        }

        private void rGroupDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGroupDate.Text = Convert.ToDateTime(rGroupDate.Text).ToShortDateString();
        }

        private void rInvAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rInvAmt.Text == "") return;
            if (intDecimalPlace == 3) rInvAmt.Text = GeneralFunctions.fnDouble(rInvAmt.Text).ToString("f3");
            else rInvAmt.Text = GeneralFunctions.fnDouble(rInvAmt.Text).ToString("f");

            if (rInvAmt.Text.StartsWith("-"))
            {
                string strTemp = rInvAmt.Text.Remove(0, 1);
                rInvAmt.Text = "(" + strTemp + ")";
            }
        }


    }
}
