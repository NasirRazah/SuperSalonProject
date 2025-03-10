/*
 USER CLASS : Report - Item Detail Record
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
namespace OfflineRetailV2.Report.Product
{
    public partial class repProductSnap1 : DevExpress.XtraReports.UI.XtraReport
    {
        private string strPicID;
        private int intImageWidth = 128;
        private int intImageHeight = 112;

        public repProductSnap1()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public string PicID
        {
            get { return strPicID; }
            set { strPicID = value; }
        }

        private void rPic_BeforePrint(object sender, CancelEventArgs e)
        {
            strPicID = rPic.Text;
        }

        private void picCustomer_BeforePrint(object sender, CancelEventArgs e)
        {
            if (!GeneralFunctions.GetPhotoFromTableForReport(picCustomer, Convert.ToInt32(strPicID), "Product"))
            {
                picCustomer.Image = null;
            }
            else
            {
                double AspectRatio = 0.00;
                int intWidth, intHeight;
                AspectRatio = Convert.ToDouble(picCustomer.Image.Width) /
                    Convert.ToDouble(picCustomer.Image.Height);
                intHeight = picCustomer.Height;
                intWidth = Convert.ToInt16(Convert.ToDouble(intHeight) * AspectRatio);

                if (intWidth > intImageWidth)
                {
                    intWidth = intImageWidth;
                    intHeight = Convert.ToInt16(Convert.ToDouble(intWidth) / AspectRatio);
                }
                picCustomer.Width = intWidth;
                picCustomer.Height = intHeight;
            }
        }

        private void chkActive_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkActive.Text == "Yes")
            {
                chkActive.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkActive.Text = "P";
            }
            else
            {
                //chkActive.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkActive.Text = "O";
                chkActive.Text = "";
            }
        }

        private void chkPOS_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkPOS.Text == "Yes")
            {
                chkPOS.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkPOS.Text = "P";
            }
            else
            {
                //chkPOS.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkPOS.Text = "O";
                chkPOS.Text = "";
            }
        }

        private void chkPrice_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkPrice.Text == "Yes")
            {
                chkPrice.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkPrice.Text = "P";
            }
            else
            {
                ////chkPrice.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkPrice.Text = "O";
                chkPrice.Text = "";
            }
        }

        private void chkZero_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkZero.Text == "Yes")
            {
                chkZero.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkZero.Text = "P";
            }
            else
            {
                ////chkZero.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkZero.Text = "O";
                chkZero.Text = "";
            }
        }

        private void chkDispStk_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkDispStk.Text == "Yes")
            {
                chkDispStk.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkDispStk.Text = "P";
            }
            else
            {
                //chkDispStk.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkDispStk.Text = "O";
                chkDispStk.Text = "";
            }
        }

        private void chkFood_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkFood.Text == "Yes")
            {
                chkFood.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkFood.Text = "P";
            }
            else
            {
                //chkFood.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkFood.Text = "O";
                chkFood.Text = "";
            }
        }

        private void chkBar_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkBar.Text == "Yes")
            {
                chkBar.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkBar.Text = "P";
            }
            else
            {
                //chkBar.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkBar.Text = "O";
                chkBar.Text = "";
            }
        }

        private void chkPL_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkPL.Text == "Yes")
            {
                chkPL.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkPL.Text = "P";
            }
            else
            {
                //chkPL.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkPL.Text = "O";
                chkPL.Text = "";
            }
        }

        private void chkNoPL_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkNoPL.Text == "Yes")
            {
                chkNoPL.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkNoPL.Text = "P";
            }
            else
            {
                //chkNoPL.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkNoPL.Text = "O";
                chkNoPL.Text = "";
            }
        }

        private void chkDigit_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (chkDigit.Text == "Yes")
            {
                chkDigit.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                chkDigit.Text = "P";
            }
            else
            {
                //chkDigit.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                //chkDigit.Text = "O";
                chkDigit.Text = "";
            }
        }

    }
}
