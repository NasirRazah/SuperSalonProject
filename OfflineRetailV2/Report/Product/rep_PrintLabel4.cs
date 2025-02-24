using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Product
{
    public partial class repPrintLabel4 : DevExpress.XtraReports.UI.XtraReport
    {
        int skipnumber;
        int printnumber;
        int pageno = 1;
        int i = 0;
        bool blprint = true;
        bool firstpos = false;

        public int skpno
        {
            get { return skipnumber; }
            set { skipnumber = value; }
        }

        public int prtno
        {
            get { return printnumber; }
            set { printnumber = value; }
        }

        public bool firstp
        {
            get { return firstpos; }
            set { firstpos = value; }
        }

        public repPrintLabel4()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
            
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

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (skipnumber > 0)
            {
                DevExpress.Data.Browsing.DataBrowser browser = this.fDataContext[this.DataSource, this.DataMember];
                browser.PositionChanged += new EventHandler(browser_PositionChanged);
            }
            skipnumber--;
        }

        private void repPrintLabel4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
