/*
 USER CLASS : Report - Closeout (Repair)
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
    public partial class repCloseoutRepair : DevExpress.XtraReports.UI.XtraReport
    {
        private double dblTotal = 0;
        private string tx1E = "N";
        private string tx2E = "N";
        private string tx3E = "N";
        private string flg = "N";
        private int intDecimalPlace;
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
        public repCloseoutRepair()
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

        private void xrTableCell33_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell33.Text);
        }

        private void xrTableCell29_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell29.Text);
        }

        private void xrTableCell25_BeforePrint(object sender, CancelEventArgs e)
        {
           // dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell25.Text);
            //if (tx1E == "N") xrTableCell25.Text = "";
        }

        private void xrTableCell15_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell15.Text);
            //if (tx2E == "N") xrTableCell15.Text = "";
        }

        private void xrTableCell2_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal + GeneralFunctions.fnDouble(xrTableCell2.Text);
            //if (tx3E == "N") xrTableCell2.Text = "";
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
                //rTotal.Text = dblTotal.ToString();
                flg = "Y";
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
            //FormatDouble(xrTableCell33);
        }

        private void xrTableCell29_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell29);
        }

        private void xrTableCell25_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell25);
        }

        private void xrTableCell15_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell15);
        }

        private void xrTableCell2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(xrTableCell2);
        }

        private void rTotal_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble(rTotal);
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
            //FormatDouble1(xrTableCell32);
        }

        private void xrTableCell43_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //FormatDouble1(xrTableCell43);
        }

        private void xrTableCell32_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal - GeneralFunctions.fnDouble(xrTableCell32.Text);
        }

        private void xrTableCell43_BeforePrint(object sender, CancelEventArgs e)
        {
            //dblTotal = dblTotal - GeneralFunctions.fnDouble(xrTableCell43.Text);
        }

        private void xrTableCell93_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrint(sender);
        }

        private void xrTableCell96_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax1(sender);
        }

        private void xrTableCell100_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax2(sender);
        }

        private void xrTableCell103_BeforePrint(object sender, CancelEventArgs e)
        {
            GetTotalBeforePrintTax3(sender);
        }

        private void xrTableCell106_BeforePrint(object sender, CancelEventArgs e)
        {
            GetLessTotalBeforePrint(sender);
        }

        private void xrTableCell111_BeforePrint(object sender, CancelEventArgs e)
        {
            GetLessTotalBeforePrint(sender);
        }

        private void xrTableCell93_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDoubleHeader(sender);
        }

        private void xrTableCell96_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell106_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble1(sender);
        }

        private void xrTableCell112_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatInt(sender);
        }

        private void xrTableCell1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            xrTableCell1.Text = OfflineRetailV2.Properties.Resources.NoofInvoices + " : " + xrTableCell1.Text;
        }

        private void xrTableCell10_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell14_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

        private void xrTableCell19_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatDouble(sender);
        }

    }
}
