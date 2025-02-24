/*
 USER CLASS : Report - Closeout (Sub Report 1)
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
    public partial class repCloseoutMain : DevExpress.XtraReports.UI.XtraReport
    {
        private string strCT;
        public string COType
        {
            get { return strCT; }
            set { strCT = value; }
        }

        public repCloseoutMain()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void rTAmount_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell21_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            if (strCT == "C")
                xrTableCell21.Text = OfflineRetailV2.Properties.Resources.Requestor;
            if (strCT == "T")
                xrTableCell21.Text = OfflineRetailV2.Properties.Resources.Terminal;
            if (strCT == "E")
                xrTableCell21.Text = OfflineRetailV2.Properties.Resources.Employee;

        }

        private void xrTableCell22_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "C")
            {
                if (SystemVariables.CurrentUserID <= 0)
                    xrTableCell22.Text = SystemVariables.CurrentUserName;
                else
                    xrTableCell22.Text = SystemVariables.CurrentUserCode;
            }

            if (strCT == "E")
            {
                xrTableCell23.Width = 0;
                xrTableCell23.Visible = false;
                xrTableCell22.Width = 192;
                xrTableCell22.Visible = true;
                xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            }

            if (strCT == "T")
            {
                xrTableCell22.Width = 0;
                xrTableCell22.Visible = false;
                xrTableCell23.Width = 192;
                xrTableCell23.Visible = true;
            }
        }

        private void xrTableCell23_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "C")
            {
                xrTableCell23.Width = 0;
                xrTableCell23.Visible = false;
                xrTableCell22.Width = 192;
                xrTableCell22.Visible = true;
            }
            if (strCT == "E")
            {
                xrTableCell23.Width = 0;
                xrTableCell23.Visible = false;
                xrTableCell22.Width = 192;
                xrTableCell22.Visible = true;
            }
            if (strCT == "T")
            {
                xrTableCell22.Width = 0;
                xrTableCell22.Visible = false;
                xrTableCell23.Width = 192;
                xrTableCell23.Visible = true;
            }
        }

        private void xrTableRow9_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (strCT == "T")
            {
                xrTableRow9.Height = 0;
            }
        }

    }
}
