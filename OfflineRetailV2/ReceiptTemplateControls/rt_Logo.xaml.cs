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
    /// Interaction logic for rt_Logo.xaml
    /// </summary>
    public partial class rt_Logo : UserControl
    {
        public rt_Logo()
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
