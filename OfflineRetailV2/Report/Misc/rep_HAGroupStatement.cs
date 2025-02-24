/*
 USER CLASS : Report - Customer Wise House Account Statement
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
namespace OfflineRetailV2.Report.Misc
{
    public partial class repHAGroupStatement : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private string strtype = "";
        private bool blShowHeader = true;
        public repHAGroupStatement()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rAccount_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            rAccount.Text = OfflineRetailV2.Properties.Resources.Account + " : " + rAccount.Text;
        }

        private void rTot1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTot1.Text = GeneralFunctions.fnDouble(rTot1.Text).ToString("f3");
            else rTot1.Text = GeneralFunctions.fnDouble(rTot1.Text).ToString("f");

            if (rTot1.Text.StartsWith("-"))
            {
                string strTemp = rTot1.Text.Remove(0, 1);
                rTot1.Text = "(" + strTemp + ")";
            }
        }

        private void xrTableCell15_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void rID_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            strtype = rID.Text;
        }

        private void rProduct_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strtype == "N") || (strtype == "W"))
            {
                rProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                rQty.Size = new Size(42, 25);
                rPrice.Size = new Size(67, 25);
                rProduct.Size = new Size(175, 25);
            }
            if (strtype == "A")
            {
                rProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                rQty.Size = new Size(2, 25);
                rPrice.Size = new Size(2, 25);
                rProduct.Size = new Size(280, 25);
            }
        }

        private void rQty_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strtype == "N") || (strtype == "W"))
            {
                rQty.Text = GeneralFunctions.GetDisplayQty1(rQty.Text, GeneralFunctions.GetDecimalLength1(rQty.Text).ToString());
                if (strtype == "W") rQty.Text = rQty.Text + " (lb)";
                //double d = Math.Truncate(GeneralFunctions.fnDouble(rQty.Text));
                //rQty.Text = d.ToString();
            }
            if (strtype == "A")
            {
                rQty.Text = "";
            }
        }

        private void rPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if ((strtype == "N") || (strtype == "W"))
            {
                if (intDecimalPlace == 3) rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f3");
                else rPrice.Text = GeneralFunctions.fnDouble(rPrice.Text).ToString("f");

                if (rPrice.Text.StartsWith("-"))
                {
                    string strTemp = rPrice.Text.Remove(0, 1);
                    rPrice.Text = "(" + strTemp + ")";
                }
            }
            if (strtype == "A")
            {
                rPrice.Text = "";
            }
        }

        private void rExtPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rExtPrice.Text = GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f3");
            else rExtPrice.Text = GeneralFunctions.fnDouble(rExtPrice.Text).ToString("f");

            if (rExtPrice.Text.StartsWith("-"))
            {
                string strTemp = rExtPrice.Text.Remove(0, 1);
                rExtPrice.Text = "(" + strTemp + ")";
            }
        }

        private void rTot2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTot2.Text = GeneralFunctions.fnDouble(rTot2.Text).ToString("f3");
            else rTot2.Text = GeneralFunctions.fnDouble(rTot2.Text).ToString("f");

            if (rTot2.Text.StartsWith("-"))
            {
                string strTemp = rTot2.Text.Remove(0, 1);
                rTot2.Text = "(" + strTemp + ")";
            }
        }

        private void rCB_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rCB.Text = GeneralFunctions.fnDouble(rCB.Text).ToString("f3");
            else rCB.Text = GeneralFunctions.fnDouble(rCB.Text).ToString("f");

            if (rCB.Text.StartsWith("-"))
            {
                string strTemp = rCB.Text.Remove(0, 1);
                rCB.Text = "(" + strTemp + ")";
            }
        }

        private void rOBAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rOBAmt.Text = GeneralFunctions.fnDouble(rOBAmt.Text).ToString("f3");
            else rOBAmt.Text = GeneralFunctions.fnDouble(rOBAmt.Text).ToString("f");

            if (rOBAmt.Text.StartsWith("-"))
            {
                string strTemp = rOBAmt.Text.Remove(0, 1);
                rOBAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rLPAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rLPAmt.Text = GeneralFunctions.fnDouble(rLPAmt.Text).ToString("f3");
            else rLPAmt.Text = GeneralFunctions.fnDouble(rLPAmt.Text).ToString("f");

            if (rLPAmt.Text.StartsWith("-"))
            {
                string strTemp = rLPAmt.Text.Remove(0, 1);
                rLPAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rCBAmt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rLPAmt.Text = GeneralFunctions.fnDouble(rCBAmt.Text).ToString("f3");
            else rCBAmt.Text = GeneralFunctions.fnDouble(rCBAmt.Text).ToString("f");

            if (rCBAmt.Text.StartsWith("-"))
            {
                string strTemp = rCBAmt.Text.Remove(0, 1);
                rCBAmt.Text = "(" + strTemp + ")";
            }
        }

        private void rInvoice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rInvoice.Text == "")
            {
                rInvoice.Text = "NA";
                blShowHeader = false;
            }
            else blShowHeader = true;
        }

        private void xrTableHeader_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (blShowHeader)
            {
                xrTableHeader.WidthF = 30;
                xrLine3.Visible = false;
            }
            else
            {
                xrTableHeader.Visible = false;
                xrTableHeader.WidthF = 0;
                xrLine3.Visible = true;
                blShowHeader = true;
            }
        }

        private void GroupHeader2_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void xrLine3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (blShowHeader)
            {

                xrLine3.Visible = false;
            }
            else
            {
                xrLine3.Visible = true;
            }
        }

    }
}
