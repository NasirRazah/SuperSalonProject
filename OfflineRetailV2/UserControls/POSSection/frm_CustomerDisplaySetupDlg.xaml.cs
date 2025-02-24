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
using System.IO;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;


namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustomerDisplaySetupDlg.xaml
    /// </summary>
    public partial class frm_CustomerDisplaySetupDlg : Window
    {
        public frm_CustomerDisplaySetupDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private string strAdvtDispArea;
        private string strAdvtScale;
        private string strAdvtDispNo;
        private string strAdvtDispTime;
        private string strAdvtDispOrder;
        private string strAdvtFont;
        private string strAdvtColor;
        private string strAdvtBackground;
        private string strPrevAdvtBGImage;
        private string strAdvtBGImage;
        private string strAdvtDir;

        private string strPhotoFile = "";
        private string strPrevPhotoFile = "";
        private double intImageWidth;
        private double intImageHeight;
        private string csStorePath = "";

        public string AdvtDispArea
        {
            get { return strAdvtDispArea; }
            set { strAdvtDispArea = value; }
        }

        public string AdvtScale
        {
            get { return strAdvtScale; }
            set { strAdvtScale = value; }
        }

        public string AdvtDispNo
        {
            get { return strAdvtDispNo; }
            set { strAdvtDispNo = value; }
        }
        public string AdvtDispTime
        {
            get { return strAdvtDispTime; }
            set { strAdvtDispTime = value; }
        }
        public string AdvtDispOrder
        {
            get { return strAdvtDispOrder; }
            set { strAdvtDispOrder = value; }
        }
        public string AdvtFont
        {
            get { return strAdvtFont; }
            set { strAdvtFont = value; }
        }
        public string AdvtColor
        {
            get { return strAdvtColor; }
            set { strAdvtColor = value; }
        }
        public string AdvtBackground
        {
            get { return strAdvtBackground; }
            set { strAdvtBackground = value; }
        }
        public string AdvtDir
        {
            get { return strAdvtDir; }
            set { strAdvtDir = value; }
        }

        public string PrevAdvtBGImage
        {
            get { return strPrevAdvtBGImage; }
            set { strPrevAdvtBGImage = value; }
        }

        public string AdvtBGImage
        {
            get { return strAdvtBGImage; }
            set { strAdvtBGImage = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Customer Display Setup";
            string RegisterModule = GeneralFunctions.RegisteredModules();

            if (RegisterModule.Contains("SCALE") && !RegisterModule.Contains("POS"))
            {
                //chkScaleAdvertisement.Visible = true;
                //chkScaleAdvertisement.Location = new Point(17, 48);
                txtAdvDisplayArea.Value = 50;
                strAdvtDispArea = "50";
                label1.Visibility = label3.Visibility = txtAdvDisplayArea.Visibility = Visibility.Hidden;
            }
            if (RegisterModule.Contains("POS") && !RegisterModule.Contains("SCALE"))
            {
                chkScaleAdvertisement.Visibility = Visibility.Collapsed;
                chkScaleAdvertisement.IsChecked = false;
                label1.Visibility = label3.Visibility = txtAdvDisplayArea.Visibility = Visibility.Visible;
                label1.Text = "Advertisement Display Area";
            }


            txtAdvDisplayArea.Value = Convert.ToDecimal(strAdvtDispArea);
            chkScaleAdvertisement.IsChecked = false; // strAdvtScale == "Y";

            txtAdvDisplayNo.Value = Convert.ToDecimal(strAdvtDispNo);
            txtAdvDisplayTime.Value = Convert.ToDecimal(strAdvtDispTime);
            cmbDisplayOrder.SelectedIndex = (strAdvtDispOrder == "Display Randomly" ? 0 : 1);
            txtFontSize.Text = strAdvtFont;


            if (!((strAdvtColor == "") || (strAdvtColor == "0") || (strAdvtColor.Contains("#00000000"))))
            {
                txtBGColor.Color = (Color)ColorConverter.ConvertFromString(strAdvtColor);
            }
            txtAdvFileDir.Text = strAdvtDir;

            csStorePath = "";
            intImageWidth = 128;
            intImageHeight = 112;

            if (strAdvtBackground != "")
            {
                csStorePath = System.IO.Path.GetPathRoot(strAdvtBackground);
                DirectoryInfo di = new DirectoryInfo(strAdvtBackground);
                if (File.Exists(strAdvtBackground))
                {
                    strPhotoFile = strAdvtBackground;
                    strPrevPhotoFile = strPhotoFile;
                }

                
                if (!File.Exists(strPhotoFile))
                {
                    pictPhoto.Source = null;
                }
                else
                {
                    pictPhoto.Source = new BitmapImage(new Uri(strPhotoFile));
                    double AspectRatio = 0.00;
                    double intWidth, intHeight;
                    AspectRatio = GeneralFunctions.fnDouble(pictPhoto.Source.Width) /
                        GeneralFunctions.fnDouble(pictPhoto.Source.Height);
                    intHeight = pictPhoto.Source.Height;
                    intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

                    if (intWidth > intImageWidth)
                    {
                        intWidth = intImageWidth;
                        intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
                    }
                    pictPhoto.Width = intWidth;
                    pictPhoto.Height = intHeight;
                }
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                strPhotoFile = op.FileName;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            strPhotoFile = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdvFileDir.Text.Trim() != "")
            {
                if (Directory.Exists(txtAdvFileDir.Text.Trim()))
                {
                    if (Directory.GetFiles(txtAdvFileDir.Text.Trim()).Length == 0)
                    {
                        DocMessage.MsgInformation("Empty advertisement file directory");
                    }
                }
                else
                {
                    DocMessage.MsgInformation("Invalid advertisement file directory path");
                    GeneralFunctions.SetFocus(txtAdvFileDir);
                    return;
                }
            }
            strAdvtDispArea = txtAdvDisplayArea.Text;
            strAdvtScale = chkScaleAdvertisement.IsChecked == true ? "Y" : "N";

            strAdvtDispNo = txtAdvDisplayNo.Text;
            strAdvtDispTime = txtAdvDisplayTime.Text;
            strAdvtDispOrder = cmbDisplayOrder.SelectedIndex == 0 ? "Display Randomly" : "Display File Name Alphabetically";
            strAdvtFont = txtFontSize.Text;
            strAdvtColor = txtBGColor.Text;
            if (strPhotoFile != "")
            {
                strAdvtBackground = strPhotoFile;

            }
            else strAdvtBackground = "";

            strPrevAdvtBGImage = strPrevPhotoFile;
            strAdvtBGImage = strPhotoFile;
            strAdvtDir = txtAdvFileDir.Text;
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Label47_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtAdvFileDir.Text = dialog.SelectedPath;
            }
            
        }

        private void CmbDisplayOrder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtBGColor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.PopupColorEdit editor = sender as DevExpress.Xpf.Editors.PopupColorEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
