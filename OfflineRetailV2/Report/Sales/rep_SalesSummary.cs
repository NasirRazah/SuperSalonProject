/*
 USER CLASS : Report - Sales Summary
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
namespace OfflineRetailV2.Report.Sales
{
    public partial class repSalesSummary : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        private int sty = 0;
        public repSalesSummary()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private void FormatDoubleHeader(object sender)
        {
            if ((sender as XRLabel).Text != "")
            {
                if ((sender as XRLabel).Text != "______________")
                {
                    if (intDecimalPlace == 3) (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f3");
                    else (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f");

                    if ((sender as XRLabel).Text.StartsWith("-"))
                    {
                        string strTemp = (sender as XRLabel).Text.Remove(0, 1);
                        (sender as XRLabel).Text = "(" + strTemp + ")";
                    }
                }
            }
        }

        private void FormatInt(object sender)
        {
            if ((sender as XRLabel).Text != "")
            {
                if (GeneralFunctions.fnInt32((sender as XRLabel).Text) == 0) (sender as XRLabel).Text = "";
                else
                {
                    (sender as XRLabel).Text = GeneralFunctions.fnInt32((sender as XRLabel).Text).ToString();
                }
            }
        }

        private void FormatDouble(object sender)
        {
            if ((sender as XRLabel).Text != "")
            {
                if (GeneralFunctions.fnDouble((sender as XRLabel).Text) == 0) (sender as XRLabel).Text = "";
                else
                {
                    if (intDecimalPlace == 3) (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f3");
                    else (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f");

                    if ((sender as XRLabel).Text.StartsWith("-"))
                    {
                        string strTemp = (sender as XRLabel).Text.Remove(0, 1);
                        (sender as XRLabel).Text = "(" + strTemp + ")";
                    }
                }
            }
        }

        private void FormatDouble1(object sender)
        {
            if ((sender as XRLabel).Text != "")
            {
                if (GeneralFunctions.fnDouble((sender as XRLabel).Text) == 0) (sender as XRLabel).Text = "";
                else
                {
                    if (intDecimalPlace == 3) (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f3");
                    else (sender as XRLabel).Text = GeneralFunctions.fnDouble((sender as XRLabel).Text).ToString("f");

                    if ((sender as XRLabel).Text.StartsWith("-"))
                    {
                        string strTemp = (sender as XRLabel).Text.Remove(0, 1);
                        (sender as XRLabel).Text = strTemp;
                    }
                    else
                    {
                        (sender as XRLabel).Text = "(" + (sender as XRLabel).Text + ")";
                    }
                }
            }
        }


        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void rs_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            sty = GeneralFunctions.fnInt32(rs.Text);
        }

        private void GetFontStype(object sender)
        {
            (sender as XRLabel).Font = (sty == 1) ? new Font("Segoe UI", 11.25f, FontStyle.Regular) : (sty == 2) ? new Font("Segoe UI", 9.75f, FontStyle.Regular) : new Font("Segoe UI", 9.0f, FontStyle.Regular);
        }

        private void xrLabel1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            GetFontStype(sender);
        }

        private void rv_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (sty != 9) FormatDoubleHeader(sender);
        }

        

        
    }
}
