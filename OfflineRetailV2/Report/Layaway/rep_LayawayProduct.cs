/*
 USER CLASS : Report - Layaway by Item Listing
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
namespace OfflineRetailV2.Report.Layaway
{
    public partial class repLayawayProduct : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repLayawayProduct()
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

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rPrice.Text.Trim() != "")
            {
                if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
                else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");

                if (rPrice.Text.StartsWith("-"))
                {
                    string strTemp = rPrice.Text.Remove(0, 1);
                    rPrice.Text = "(" + strTemp + ")";
                }
            }
        }

        private void rohQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rohQty.Text.Trim() != "")
            {
                double d = GeneralFunctions.fnDouble(rohQty.Text);
                rohQty.Text = d.ToString();
            }
        }

        private void rlQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rlQty.Text.Trim() != "")
            {
                double d = GeneralFunctions.fnDouble(rlQty.Text);
                rlQty.Text = d.ToString();
            }
        }

        private void raQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (raQty.Text.Trim() != "")
            {
                double d = GeneralFunctions.fnDouble(raQty.Text);
                raQty.Text = d.ToString();
            }
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rQty.Text.Trim() != "")
            {
                double d = GeneralFunctions.fnDouble(rQty.Text);
                rQty.Text = d.ToString();
            }
        }

        private void raQty_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            if (raQty.Text.Trim() != "")
            {
                double d = GeneralFunctions.fnDouble(raQty.Text);
                raQty.Text = d.ToString();
            }
        }

        private void rEmail_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rEmail.Text.Trim() != "")
            {
                rEmail.Text = OfflineRetailV2.Properties.Resources.Email;
            }
        }

    }
}
