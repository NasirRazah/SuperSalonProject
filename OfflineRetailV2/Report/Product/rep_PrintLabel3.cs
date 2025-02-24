using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Product
{
    public partial class repPrintLabel3 : DevExpress.XtraReports.UI.XtraReport
    {
        int skipnumber;
        int printnumber;
        int pageno;
        int i = 0;
        bool blprint = true;
        bool firstpos = false;

        public repPrintLabel3(int skip, int qty)
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
            skipnumber = skip;
            printnumber = qty;
            if (skipnumber == 0) firstpos = true;
            pageno = 1;
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
            if (skipnumber > 0)
            {
                DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
                browser.PositionChanged += new EventHandler(browser_PositionChanged);
            }
            skipnumber--;
        }

        private void lbSKU_BeforePrint(object sender, CancelEventArgs e)
        {
            DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
            if (!browser.HasLastPosition)
            {
                if (skipnumber >= 0)
                {
                    e.Cancel = true;
                }
                
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
