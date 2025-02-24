/*
 USER CLASS : Report - Common Listing with 3 Columns
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
    public partial class rep3CList : DevExpress.XtraReports.UI.XtraReport
    {
        public rep3CList()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        private void rCol3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rCol3.Text == "Yes")
            {
                rCol3.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                rCol3.Text = "P";
            }
            if (rCol3.Text == "No")
            {
                rCol3.Text = "";
            }
        }

    }
}
