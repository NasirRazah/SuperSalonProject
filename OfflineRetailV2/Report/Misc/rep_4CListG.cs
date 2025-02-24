/*
 USER CLASS : Report - Common Listing with 4 Columns Group wise (4)
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
    public partial class rep4CListG : DevExpress.XtraReports.UI.XtraReport
    {
        public rep4CListG()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        private int intImageWidth = 50;
        private int intImageHeight = 50;

        private string strPicID;

        private void rCol3_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rCol3.Text == "Yes")
            {
                rCol3.Font = new Font("Wingdings 2", 20.25f, FontStyle.Regular);
                rCol3.Text = "P";
            }
            if (rCol3.Text == "No")
            {
                rCol3.Text = "";
            }
        }

        private void xrPictureBox1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (!GeneralFunctions.GetPhotoFromTableForReport(rPic, Convert.ToInt32(strPicID), "Graphic Art"))
            {
                rPic.Image = null;
                rPic.Visible = false;
            }
            else
            {
                rPic.Visible = true;
                double AspectRatio = 0.00;
                int intWidth, intHeight;
                AspectRatio = Convert.ToDouble(rPic.Image.Width) /
                    Convert.ToDouble(rPic.Image.Height);
                intHeight = rCol2.Height;
                intWidth = Convert.ToInt16(Convert.ToDouble(intHeight) * AspectRatio);

                if (intWidth > intImageWidth)
                {
                    intWidth = intImageWidth;
                    intHeight = Convert.ToInt16(Convert.ToDouble(intWidth) / AspectRatio);
                }
                rPic.Width = intWidth;
                rPic.Height = intHeight;
            }
        }

        private void rCol4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            strPicID = rCol4.Text;
        }

    }
}
