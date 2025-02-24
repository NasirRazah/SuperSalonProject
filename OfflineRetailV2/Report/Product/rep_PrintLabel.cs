using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Product
{
    public partial class repPrintLabel : DevExpress.XtraReports.UI.XtraReport
    {
        int skipnumber;
        const int skip = 3;
        public repPrintLabel()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
            //skipnumber = skip;
        }

        private void lbBarCode_BeforePrint(object sender, CancelEventArgs e)
        {
            /*if (skipnumber >= 0)
            {
                e.Cancel = true;
            }*/
        }

        bool stop = false;
        void browser_PositionChanged(object sender, EventArgs e)
        {
            ((DevExpress.Data.Browsing.ListBrowser)(sender)).PositionChanged -= browser_PositionChanged;
            if (!stop)
                ((DevExpress.Data.Browsing.ListBrowser)(sender)).Position = 0;
            if (skipnumber <= 0)
            {
                stop = true;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            /*if (skipnumber > 0)
            {
                DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
                browser.PositionChanged += new EventHandler(browser_PositionChanged);
            }
            skipnumber--;*/
        }

    }
}
