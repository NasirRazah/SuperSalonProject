using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OfflineRetailV2.ReceiptTemplateControls
{
    /// <summary>
    /// Interaction logic for rt_Image.xaml
    /// </summary>
    public partial class rt_Image : UserControl
    {
        public rt_Image()
        {
            InitializeComponent();
        }

        private byte[] bytdata;

        public byte[] byt
        {
            get { return bytdata; }
            set { bytdata = value; }
        }

        private string strTextAlign;

        public string TextAlign
        {
            get { return strTextAlign; }
            set { strTextAlign = value; }
        }

       


        private int intCtrlWidth;

        public int CtrlWidth
        {
            get { return intCtrlWidth; }
            set { intCtrlWidth = value; }
        }

        private int intCtrlHeight;

        public int CtrlHeight
        {
            get { return intCtrlHeight; }
            set { intCtrlHeight = value; SetValue(); }
        }

        private void SetValue()
        {


            byte[] content = bytdata;
            try
            {
                // assign byte array data into memory stream 
                MemoryStream stream = new MemoryStream(content);

                // set transparent bitmap with 32 X 32 size by memory stream data 
                Bitmap b = new Bitmap(stream);
                Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
                output.MakeTransparent();

                System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                bi.BeginInit();
                System.Drawing.Image tempImage = (System.Drawing.Image)output;
                MemoryStream ms = new MemoryStream();
                tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Seek(0, SeekOrigin.Begin);

                bi.StreamSource = stream;

                bi.EndInit();

                imglogo.Source = bi;

            }
            catch (Exception ex)
            {
                imglogo.Source = null;
            }

            if (strTextAlign == "left")
            {
                imglogo.HorizontalAlignment = HorizontalAlignment.Left;
            }
            if (strTextAlign == "center")
            {
                imglogo.HorizontalAlignment = HorizontalAlignment.Center;
            }
            if (strTextAlign == "right")
            {
                imglogo.HorizontalAlignment = HorizontalAlignment.Right;
            }

            imglogo.Width = (float)intCtrlWidth;
            imglogo.Height = (float)intCtrlHeight;


        }
    }
}
