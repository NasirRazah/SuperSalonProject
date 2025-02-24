/*
 USER CLASS : Report - Layaway Receipt (Line Item)
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
namespace OfflineRetailV2.Report.Invoice
{
    public partial class repReceiptItem : DevExpress.XtraReports.UI.XtraReport
    {
        
        public repReceiptItem()
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
        private string strDP;
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


        private void rRate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rRate.Text = GeneralFunctions.fnDouble(rRate.Text).ToString("f3");
            else
            {
                if (strDP == "2")
                    rRate.Text = GeneralFunctions.fnDouble(rRate.Text).ToString("f");
                if (strDP == "3")
                    rRate.Text = GeneralFunctions.fnDouble(rRate.Text).ToString("f3");
            }

            if (rRate.Text.StartsWith("-"))
            {
                string strTemp = rRate.Text.Remove(0, 1);
                rRate.Text = "(" + strTemp + ")";
            }
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rQty.Text = GeneralFunctions.fnDouble(rQty.Text).ToString("f3");
            else
            {
                rQty.Text = GeneralFunctions.fnDouble(rQty.Text).ToString("f");
            }
            if (rQty.Text.StartsWith("-"))
            {
                string strTemp = rQty.Text.Remove(0, 1);
                rQty.Text = "(" + strTemp + ")";
            }

        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
            else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");

            if (rPrice.Text.StartsWith("-"))
            {
                string strTemp = rPrice.Text.Remove(0, 1);
                rPrice.Text = "(" + strTemp + ")";
            }
        }

        private void rDP_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            strDP = rDP.Text;
        }


        
    }
}
