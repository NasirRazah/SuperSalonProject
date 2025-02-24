/*
 USER CLASS : Report - Gift Cert. Redeem Listing
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
    public partial class repGCRedeem : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public repGCRedeem()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }
        private void rAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rAmount.Text = GeneralFunctions.fnDouble(rAmount.Text).ToString("f3");
            else rAmount.Text = GeneralFunctions.fnDouble(rAmount.Text).ToString("f");

            if (rAmount.Text.StartsWith("-"))
            {
                string strTemp = rAmount.Text.Remove(0, 1);
                rAmount.Text = "(" + strTemp + ")";
            }
        }

        private void rTrDate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rTrDate.Text = GeneralFunctions.fnDate(rTrDate.Text).ToString(SystemVariables.DateFormat);
        }

    }
}
