/*
 USER CLASS : Report - Customer General Details 
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
namespace OfflineRetailV2.Report.Customer
{
    public partial class repCustomerSnap : DevExpress.XtraReports.UI.XtraReport
    {
        private string strPicID;
        private string chkTax;
        private int intImageWidth = 128;
        private int intImageHeight = 112;
        private int intDecimalPlace;

        public repCustomerSnap()
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
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }

        private void picCustomer_BeforePrint(object sender, CancelEventArgs e)
        {
            if (!GeneralFunctions.GetPhotoFromTableForReport(picCustomer, GeneralFunctions.fnInt32(strPicID), "Customer"))
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

        private void rPic_BeforePrint(object sender, CancelEventArgs e)
        {
            strPicID = rPic.Text;
        }

        

        

    }
}
