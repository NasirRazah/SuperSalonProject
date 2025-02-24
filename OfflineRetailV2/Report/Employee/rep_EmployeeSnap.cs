/*
 USER CLASS : Report - Employee Detail Info
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
namespace OfflineRetailV2.Report.Employee
{
    public partial class repEmployeeSnap : DevExpress.XtraReports.UI.XtraReport
    {
        private string strPicID;
        private int intImageWidth = 128;
        private int intImageHeight = 112;

        public string PicID
        {
            get { return strPicID; }
            set { strPicID = value; }
        }

        public repEmployeeSnap()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }

        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }

        private void rPic_BeforePrint(object sender, CancelEventArgs e)
        {
            strPicID = rPic.Text;
        }

        private void picCustomer_BeforePrint(object sender, CancelEventArgs e)
        {
            if (!GeneralFunctions.GetPhotoFromTableForReport(picCustomer, GeneralFunctions.fnInt32(strPicID), "Employee"))
            {
                picCustomer.Image = null;
            }
            else
            {

                double AspectRatio = 0.00;
                int intWidth, intHeight;
                AspectRatio = GeneralFunctions.fnDouble(picCustomer.Image.Width) /
                    GeneralFunctions.fnDouble(picCustomer.Image.Height);
                intHeight = picCustomer.Height;
                intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

                if (intWidth > intImageWidth)
                {
                    intWidth = intImageWidth;
                    intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
                }
                picCustomer.Width = intWidth;
                picCustomer.Height = intHeight;
            }
        }

    }
}
