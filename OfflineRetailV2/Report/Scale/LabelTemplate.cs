/*
 USER CLASS : Report - Scale label Template
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
namespace OfflineRetailV2.Report.Scale
{
    public partial class LabelTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public LabelTemplate()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        private void xrPictureBox1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            string rotate = "";
            string tagval = xrPictureBox1.Tag.ToString();
            if ((tagval == "") || (tagval == "0"))
            {
                rotate = "";
            }
            else if (tagval == "1")
            {
                rotate = "90";
            }
            else if (tagval == "2")
            {
                rotate = "180";
            }
            else
            {
                rotate = "270";
            }

            Image i = xrPictureBox1.Image;
            Bitmap b = new Bitmap(i);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.DrawImage(i, 0, 0, i.Width, i.Height);
            if (rotate == "90") b.RotateFlip(RotateFlipType.Rotate90FlipXY);
            if (rotate == "180")
            {
                b.RotateFlip(RotateFlipType.Rotate90FlipXY);
                b.RotateFlip(RotateFlipType.Rotate90FlipXY);
            }
            if (rotate == "270")
            {
                b.RotateFlip(RotateFlipType.Rotate90FlipXY);
                b.RotateFlip(RotateFlipType.Rotate90FlipXY);
                b.RotateFlip(RotateFlipType.Rotate90FlipXY);
            }
            xrPictureBox1.Image = b;
        }

        private void xrPictureBox2_BeforePrint(object sender, CancelEventArgs e)
        {
            (sender as XRPictureBox).Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        }

    }
}
