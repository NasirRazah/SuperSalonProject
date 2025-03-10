/*
 USER CLASS : Report - Customer Order Listing
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
namespace OfflineRetailV2.Report.CustomerOrdering
{
    public partial class repOrderItem : DevExpress.XtraReports.UI.XtraReport
    {
        private string strGroupType;
        private int intDecimalPlace;
        private double dblRev = 0;
        private double dblPft = 0;
        private double dblTRev = 0;
        private double dblTPft = 0;
        private double rTot1Amt = 0;
        private bool blPrint = false;

        private int TotItem = 0;


        public repOrderItem()
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
           
        }

        private void rRevenue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rProfit_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rMargin_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
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

        private void rTTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTTot3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTTot4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTTot5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rGroupIDCaption_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rGroupDesc_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            TotItem = TotItem + 1;
        }

        private void xrTableCell8_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell8.Text = TotItem.ToString();
        }

        

    }
}
