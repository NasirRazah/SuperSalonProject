/*
 USER CLASS : Report - Sales Discount on Item Listing
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
    public partial class repGiftAid : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiftAid()
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
            if (intDecimalPlace == 3) rAmount.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rAmount.Text).ToString("f3");
            else rAmount.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rAmount.Text).ToString("f");
            
            if (rAmount.Text.StartsWith("-"))
            {
                string strTemp = rAmount.Text.Remove(0, 1);
                rAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rGroupInvAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rGTot_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTot_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rTot.Text.StartsWith("-"))
            {
                string strTemp = rTot.Text.Remove(0, 1);
                rTot.Text = "(" + strTemp + ")";
            }

            if (intDecimalPlace == 3) rTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTot.Text).ToString("f3");
            else rTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rTot.Text).ToString("f");

        }

        private void rGroupDateAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rGroupDateAmount.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rGroupDateAmount.Text).ToString("f3");
            else rGroupDateAmount.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rGroupDateAmount.Text).ToString("f");

            if (rGroupDateAmount.Text.StartsWith("-"))
            {
                string strTemp = rGroupDateAmount.Text.Remove(0, 1);
                rGroupDateAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rGroupDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rGroupDate.Text = Convert.ToDateTime(rGroupDate.Text).ToShortDateString();
        }

        private void rInvAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rGTot_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            if (rGTot.Text.StartsWith("-"))
            {
                string strTemp = rGTot.Text.Remove(0, 1);
                rGTot.Text = "(" + strTemp + ")";
            }

            if (intDecimalPlace == 3) rGTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rGTot.Text).ToString("f3");
            else rGTot.Text = SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(rGTot.Text).ToString("f");
        }
    }
}
