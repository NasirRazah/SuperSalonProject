/*
 USER CLASS : Report - Closeout (Sub Report 2-Tax Include)
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
    public partial class repCloseoutMain1_tx : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private double dblTotalSaleTax = 0;
        private string tx1E = "N";
        private string tx2E = "N";
        private string tx3E = "N";
        private string flg = "N";
        private string flg1 = "N";
        private string flg2 = "N";
        private string flg3 = "N";
        private string flg4 = "N";
        private string flgt = "N";
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repCloseoutMain1_tx()
        {
            InitializeComponent();
            ////Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void GetLessTotalBeforePrint(object sender)
        {
            dblTotal = dblTotal - GeneralFunctions.fnDouble((sender as XRTableCell).Text);
        }


        private void GetTotalBeforePrint(object sender)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
        }

        private void GetTotalBeforePrintTax1(object sender)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
            if (tx1E == "N") (sender as XRTableCell).Text = "";
        }

        private void GetTotalBeforePrintTax2(object sender)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
            if (tx2E == "N") (sender as XRTableCell).Text = "";
        }

        private void GetTotalBeforePrintTax3(object sender)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
            if (tx3E == "N") (sender as XRTableCell).Text = "";
        }

        private void GetTotalBeforePrintDTax(object sender)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
            
        }

        private void xrTableCell29_BeforePrint(object sender, CancelEventArgs e)
        {
            dblTotal = dblTotal;
        }

        private void xrTableCell25_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell15_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rctx1_BeforePrint(object sender, CancelEventArgs e)
        {
            tx1E = rctx1.Text;
        }

        private void rctx2_BeforePrint(object sender, CancelEventArgs e)
        {
            tx2E = rctx2.Text;
        }

        private void rctx3_BeforePrint(object sender, CancelEventArgs e)
        {
            tx3E = rctx3.Text;
        }

        private void rTotal_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg == "N")
            {
                rTotal.Text = dblTotal.ToString();
                flg = "Y";
            }
        }

        private void FormatDoubleHeader(object sender)
        {
            if ((sender as XRTableCell).Text != "")
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

        private void FormatInt(object sender)
        {
            if ((sender as XRTableCell).Text != "")
            {
                if (GeneralFunctions.fnInt32((sender as XRTableCell).Text) == 0) (sender as XRTableCell).Text = "";
                else
                {
                    (sender as XRTableCell).Text = GeneralFunctions.fnInt32((sender as XRTableCell).Text).ToString();
                }
            }
        }

        private void FormatDouble(object sender)
        {
            if ((sender as XRTableCell).Text != "")
            {
                if (GeneralFunctions.fnDouble((sender as XRTableCell).Text) == 0) (sender as XRTableCell).Text = "";
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

        private void FormatDouble1(object sender)
        {
            if ((sender as XRTableCell).Text != "")
            {
                if (GeneralFunctions.fnDouble((sender as XRTableCell).Text) == 0) (sender as XRTableCell).Text = "";
                else
                {
                    if (intDecimalPlace == 3) (sender as XRTableCell).Text = GeneralFunctions.fnDouble((sender as XRTableCell).Text).ToString("f3");
                    else (sender as XRTableCell).Text = GeneralFunctions.fnDouble((sender as XRTableCell).Text).ToString("f");

                    if ((sender as XRTableCell).Text.StartsWith("-"))
                    {
                        string strTemp = (sender as XRTableCell).Text.Remove(0, 1);
                        (sender as XRTableCell).Text = strTemp;
                    }
                    else
                    {
                        (sender as XRTableCell).Text = "(" + (sender as XRTableCell).Text + ")";
                    }
                }
            }
        }

        private void FormatDouble(XRTableCell tabcl)
        {
            if (tabcl.Text != "")
            {
                if (intDecimalPlace == 3) tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f3");
                else tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f");

                if (tabcl.Text.StartsWith("-"))
                {
                    string strTemp = tabcl.Text.Remove(0, 1);
                    tabcl.Text = "(" + strTemp + ")";
                }
            }
        }

        private void FormatDouble1(XRTableCell tabcl)
        {
            if (tabcl.Text != "")
            {
                if (intDecimalPlace == 3) tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f3");
                else tabcl.Text = GeneralFunctions.fnDouble(tabcl.Text).ToString("f");

                if (tabcl.Text.StartsWith("-"))
                {
                    string strTemp = tabcl.Text.Remove(0, 1);
                    tabcl.Text = strTemp;
                }
                else
                {
                    tabcl.Text = "(" + tabcl.Text + ")";
                }
            }
        }

        private void xrTableCell33_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(xrTableCell33);
        }

        private void xrTableCell29_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell25_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void xrTableCell15_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell15);
        }

        private void xrTableCell2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(rTotal);
        }

        private void xrTableCell3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell3);
        }

        private void xrTableCell6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell6);
        }

        private void xrTableCell12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell12);
        }

        private void xrTableCell18_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void xrTableCell24_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        }

        private void xrTableCell32_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble1(sender);
        }

        private void xrTableCell43_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void xrTableCell32_BeforePrint(object sender, CancelEventArgs e)
        {
            //GetLessTotalBeforePrint(sender);
        }

        private void xrTableCell43_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell13_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrint(sender);
        }

        private void xrTableCell18_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax1(sender);
        }

        private void xrTableCell29_BeforePrint_1(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax2(sender);
        }

        private void xrTableCell45_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax3(sender);
        }

        private void xrTableCell13_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrTableCell25_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell39_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTableCell39_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatInt(sender);
        }

        private void xrTableCell4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell4.Text = " ( " + xrTableCell4.Text + " )";
        }

        private void xrpntx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrptx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrsntx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrstx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrontx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrotx_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrTableCell73_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell69_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell73_BeforePrint(object sender, CancelEventArgs e)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell73.Text);
        }

        private void xrTableCell69_BeforePrint(object sender, CancelEventArgs e)
        {
            dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell69.Text);
        }

        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintDTax(sender);
        }

        private void xrTableCell23_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatInt(sender);
        }

        private void xrTableCell24_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble1(sender);
        }

        private void GetTaxTotalBeforePrint(object sender)
        {
            dblTotalSaleTax = dblTotalSaleTax + GeneralFunctions.fnDouble((sender as XRTableCell).Text);
        }

        private void xrTableCell52_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg1 == "N")
            {
                dblTotalSaleTax = dblTotalSaleTax + GeneralFunctions.fnDouble(xrTableCell52.Text);
                flg1 = "Y";
            }
        }

        private void xrTableCell56_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrTableCell47_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg2 == "N")
            {
                dblTotalSaleTax = dblTotalSaleTax + GeneralFunctions.fnDouble(xrTableCell47.Text);
                flg2 = "Y";
            }
        }

        private void xrTableCell35_BeforePrint(object sender, CancelEventArgs e)
        {
            if (flg3 == "N")
            {
                dblTotalSaleTax = dblTotalSaleTax + GeneralFunctions.fnDouble(xrTableCell35.Text);
                flg3 = "Y";
            }
        }

        private void xrTableCell45_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (flg4 == "N")
            {
                dblTotalSaleTax = dblTotalSaleTax + GeneralFunctions.fnDouble(xrTableCell45.Text);
                flg4 = "Y";
                flgt = "Y";
            }
        }

        private void xrTableCell29_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
        {
            if (flgt == "Y") xrTableCell29.Text = GeneralFunctions.fnDouble(dblTotalSaleTax).ToString("f");
        }

        

    }
}
