using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Product
{
    public partial class repPrintLabel2up : DevExpress.XtraReports.UI.XtraReport
    {
        public repPrintLabel2up()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void xrBarCode1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (xrBarCode1.Text == "") xrBarCode1.Visible = false;
            else xrBarCode1.Visible = true;
        }

        private void lbBarCode_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            if (lbBarCode.Text == "") lbBarCode.Visible = false;
            else lbBarCode.Visible = true;
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
        }

    }
}
