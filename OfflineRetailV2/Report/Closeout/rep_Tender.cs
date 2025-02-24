/*
 USER CLASS : Report - Closeout (Drawer Reconciliation)
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
namespace OfflineRetailV2.Report.Closeout
{
    public partial class repTender : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }



        private bool blCardTotal = false;

        private string sCC1 = "";
        private string sCC2 = "";
        private string sCC3 = "";
        public repTender()
        {
            InitializeComponent();
           // //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void rTAmount_BeforePrint(object sender, CancelEventArgs e)
        {
            if (blCardTotal)
            {
                sCC3 = GeneralFunctions.fnDouble(rTAmount.Text).ToString();
                e.Cancel = true;
            }
            else
            {
                if (rTAmount.Text != "")
                    dblTotal = dblTotal + GeneralFunctions.fnDouble(rTAmount.Text);
            }
        }

        private void rTotal_BeforePrint(object sender, CancelEventArgs e)
        {
            rTotal.Text = dblTotal.ToString();
        }

        private void FormatDouble(XRTableCell tabcl)
        {
            if (tabcl.Text != "")
            {
                if (intDecimalPlace == 3) tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f3");
                else tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f");

                if (tabcl.Text.StartsWith("-"))
                {
                    string strTemp = tabcl.Text.Remove(0, 1);
                    tabcl.Text = "(" + strTemp + ")";
                }
            }
        }

        private void rTAmount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(rTAmount);
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(rTotal);
        }

        private void rTName_BeforePrint(object sender, CancelEventArgs e)
        {
            if (rTName.Text == "Card Processing Total")
            {
                sCC1 = OfflineRetailV2.Properties.Resources.CardProcessingTotal;
                blCardTotal = true;
                e.Cancel = true;
            }
            else
            {
                blCardTotal = false;
            }
        }

        private void rTCount_BeforePrint(object sender, CancelEventArgs e)
        {
            if (blCardTotal)
            {
                sCC2 = rTCount.Text.ToString();
                e.Cancel = true;
            }
        }

        private void rCC1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rCC1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rCC1.Text = sCC1;
        }

        private void rCC2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rCC2.Text = sCC2;
        }

        private void tCC3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rCC3.Text = sCC3;
        }
    }
}
