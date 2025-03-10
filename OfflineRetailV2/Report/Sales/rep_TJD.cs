using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.Report.Sales
{
    public partial class repTJD : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private string iTag = "N";
        private bool blDetail;
        public repTJD()
        {
            InitializeComponent();
            
        }
        
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public bool ShowDetail
        {
            get { return blDetail; }
            set { blDetail = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
           
        }

        private void rTotPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rSubtatal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

            
        }

        private void rTotFreight_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void rTotTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rGrandTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rPriceA_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void rTag_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rTag_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //iTag = rTag.Text;
        }

        private void DetailReport_BeforePrint(object sender, CancelEventArgs e)
        {
            iTag = rTag.Text;
            if (blDetail)
            {
                if (iTag == "N")
                {
                    DetailReport.Visible = false;
                }
                else
                {
                    DetailReport.Visible = true;
                }
            }
            else
            {
                DetailReport.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }


        private void FormatDouble(object sender)
        {
            if ((sender as XRTableCell).Text != "")
            {
                if (GeneralFunctions.fnDouble((sender as XRTableCell).Text) == 0) (sender as XRTableCell).Text = "0.00";
                else
                {
                    if (intDecimalPlace == 3) (sender as XRTableCell).Text = GeneralFunctions.fnDouble((sender as XRTableCell).Text).ToString("f3");
                    else (sender as XRTableCell).Text = GeneralFunctions.fnDouble((sender as XRTableCell).Text).ToString("f");

                    if ((sender as XRTableCell).Text.StartsWith("-"))
                    {
                        string strTemp = (sender as XRTableCell).Text.Remove(0, 1);
                        (sender as XRTableCell).Text = "(" + strTemp + ")";
                    }
                }
            }
        }

        private void rTotalBill_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void rTotalTax_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void rQty_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void rT_Bill_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void tHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
