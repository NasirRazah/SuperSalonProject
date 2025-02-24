/*
 USER CLASS : Report - Fees and Charges Record
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
namespace OfflineRetailV2.Report.Misc
{
    public partial class repFees : DevExpress.XtraReports.UI.XtraReport
    {
        public repFees()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void MakeCheckmark(XRLabel rLabel)
        {
            if (rLabel.Text == "Y")
            {
                rLabel.Font = new Font("Wingdings 2", 18.25f, FontStyle.Regular);
                rLabel.Text = "P";
            }
            else
            {
                rLabel.Text = "";
            }
        }

        private void rActive_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            MakeCheckmark(sender as XRLabel);
        }

        private void MakeDateHighlighted(XRLabel rLabel)
        {
            if (rLabel.Text == "1")
            {
                rLabel.ForeColor = Color.Black;
                rLabel.Text = rLabel.Tag.ToString();
            }
            else
            {
                rLabel.Text = rLabel.Tag.ToString();
            }
        }
        private void r1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            MakeDateHighlighted(sender as XRLabel);
        }
    }
}
