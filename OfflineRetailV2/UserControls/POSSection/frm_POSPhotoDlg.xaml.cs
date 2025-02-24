using OfflineRetailV2.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSPhotoDlg.xaml
    /// </summary>
    public partial class frm_POSPhotoDlg : Window
    {
        public frm_POSPhotoDlg()
        {
            InitializeComponent();
            Loaded += Frm_POSPhotoDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            Close();
        }

        private void Frm_POSPhotoDlg_Loaded(object sender, RoutedEventArgs e)
        {
            //if (strPhotoType == "Customer") btnHelp.Tag = "Help on poscphoto";--Sam
            //if (strPhotoType == "Product") btnHelp.Tag = "Help on pospphoto";
            //if (strPhotoType == "Employee") btnHelp.Tag = "Help on posephoto";
            ShowPhoto();
        }

        private int intImageWidth;
        private int intImageHeight;
        private string csStorePath = "";

        private string strPhotoType;
        private string strPhotoFile;
        private int intPhotoID;

        public string PhotoType
        {
            get { return strPhotoType; }
            set { strPhotoType = value; }
        }


        public string PhotoFile
        {
            get { return strPhotoFile; }
            set { strPhotoFile = value; }
        }

        public int PhotoID
        {
            get { return intPhotoID; }
            set { intPhotoID = value; }
        }

        private void ShowPhoto()
        {
            try
            {
                GeneralFunctions.LoadPhotofromDB(strPhotoType, intPhotoID, picPhoto);
            }
            catch
            {

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
