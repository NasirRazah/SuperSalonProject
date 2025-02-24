using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Product
{
    public partial class repPrintLabel2 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPrintLabel2()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private int intcnt = 0;

        private void lbSKU_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            /*intcnt++;
            if (intcnt == 1)
            {
                Margins.Top = 40;
            }
            else
            {
                Margins.Top = 0;
            }*/
        }

    }
}
