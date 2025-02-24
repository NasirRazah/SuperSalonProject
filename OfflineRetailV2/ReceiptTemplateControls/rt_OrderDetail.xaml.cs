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
    /// Interaction logic for rt_DataGeneral.xaml
    /// </summary>
    public partial class rt_OrderDetail : UserControl
    {
        private string strTextAlign;

        public string TextAlign
        {
            get { return strTextAlign; }
            set { strTextAlign = value; }
        }

        private string strTextStyle;

        public string TextStyle
        {
            get { return strTextStyle; }
            set { strTextStyle = value; }
        }

        private int intFontSize;

        public int FontSize
        {
            get { return intFontSize; }
            set { intFontSize = value; }
        }

        private string strGroupData;

        public string GroupData
        {
            get { return strGroupData; }
            set { strGroupData = value; SetValue(); }
        }

        public rt_OrderDetail()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            txtData.Text = strGroupData;
            txtData.FontSize = (float)intFontSize;
            

            if (strTextStyle == "normal")
            {
                txtData.Style = this.FindResource("rtNormal") as Style;
                //txtData.FontStyle = FontStyles.Normal;
                //txtData.FontWeight = FontWeights.Regular;
            }
            if (strTextStyle == "italic")
            {
                txtData.Style = this.FindResource("rtItalic") as Style;
                //txtData.FontStyle = FontStyles.Italic;
                //txtData.FontWeight = FontWeights.Regular;
            }
            if (strTextStyle == "bold")
            {
                txtData.Style = this.FindResource("rtBold") as Style;
                //txtData.FontWeight = FontWeights.Bold;
            }
        }
    }
}
