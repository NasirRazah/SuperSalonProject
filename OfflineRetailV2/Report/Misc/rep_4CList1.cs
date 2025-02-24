/*
 USER CLASS : Report - Common Listing with 4 Columns (2)
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
    public partial class rep4CList1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep4CList1()
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

        private void rCol4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rCol4.Text == "Yes")
            {
                rCol4.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                rCol4.Text = "P";
            }
            if (rCol4.Text == "No")
            {
                rCol4.Text = "";
            }
        }

    }
}
