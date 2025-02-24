/*
 USER CLASS : Report - Customer Label (1)
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
namespace OfflineRetailV2.Report.Customer
{
    public partial class repLabel : DevExpress.XtraReports.UI.XtraReport
    {
        int skipnumber;
        int printnumber;
        int pageno;
        int i = 0;
        bool blprint = true;
        bool firstpos = false;
        //private int skip;

        

        public repLabel(int skip,int qty)
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
            skipnumber = skip;
            printnumber = qty;
            if (skipnumber == 0) firstpos = true;
            pageno = 1;
        }

        private void xrPanel1_BeforePrint(object sender, CancelEventArgs e)
        {
            
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

        private void lbCompany_BeforePrint(object sender, CancelEventArgs e)
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

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void repPrintLabel1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

    }
}
