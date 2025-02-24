using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSShopFromImageDlg.xaml
    /// </summary>
    public partial class frm_POSShopFromImageDlg : Window
    {
        public frm_POSShopFromImageDlg()
        {
            InitializeComponent();
        }

        private int intImageWidth;
        private int intImageHeight;

        private int intPhotoID;


        public int PhotoID
        {
            get { return intPhotoID; }
            set { intPhotoID = value; }
        }
        public void ShowPhoto()
        {

            intImageWidth = 550;
            intImageHeight = 550;

            if (!GeneralFunctions.GetPhotoFromTable(picPhoto, intPhotoID, "Product"))
            {
                picPhoto.Source = null;
            }
            else
            {
                if ((picPhoto.Width <= intImageWidth) && (picPhoto.Height <= intImageHeight))
                {
                    picPhoto.Stretch = Stretch.None;
                }
                else
                {
                    /*double AspectRatio = 0.00;
                    double intWidth, intHeight;
                    AspectRatio = GeneralFunctions.fnDouble(picPhoto.Width) /
                        GeneralFunctions.fnDouble(picPhoto.Height);
                    intHeight = picPhoto.Height;
                    intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

                    if (intWidth > intImageWidth)
                    {
                        intWidth = intImageWidth;
                        intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
                    }
                    picPhoto.Width = intWidth;
                    picPhoto.Height = intHeight;*/
                }


            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPhoto();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
