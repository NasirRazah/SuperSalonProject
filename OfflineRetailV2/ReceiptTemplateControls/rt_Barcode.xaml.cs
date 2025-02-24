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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OfflineRetailV2.ReceiptTemplateControls
{
    /// <summary>
    /// Interaction logic for rt_Barcode.xaml
    /// </summary>
    public partial class rt_Barcode : UserControl
    {
        public rt_Barcode()
        {
            InitializeComponent();
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
            //imgBarcode.Module = 2;

            if (strTextAlign == "left")
            {
                imgBarcode.HorizontalAlignment = HorizontalAlignment.Left;
            }
            if (strTextAlign == "center")
            {
                imgBarcode.HorizontalAlignment = HorizontalAlignment.Center;
            }
            if (strTextAlign == "right")
            {
                imgBarcode.HorizontalAlignment = HorizontalAlignment.Right;
            }

            imgBarcode.Width = (float)intCtrlWidth;
            imgBarcode.Height = (float)intCtrlHeight;
            imgBarcode.Module = (float)2;
        }
    }
}
