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
    public partial class rt_OrderHeader : UserControl
    {
        private string strShowHeader1;
        private string strShowHeader2;
        private string strShowHeader3;

        private string strHeader1Caption;
        private string strHeader2Caption;
        private string strHeader3Caption;

        public string Header1Caption
        {
            get { return strHeader1Caption; }
            set { strHeader1Caption = value; }
        }

        public string Header2Caption
        {
            get { return strHeader2Caption; }
            set { strHeader2Caption = value; }
        }

        public string Header3Caption
        {
            get { return strHeader3Caption; }
            set { strHeader3Caption = value; }
        }



        public string ShowHeader1
        {
            get { return strShowHeader1; }
            set { strShowHeader1 = value; }
        }

        public string ShowHeader2
        {
            get { return strShowHeader2; }
            set { strShowHeader2 = value; }
        }

        public string ShowHeader3
        {
            get { return strShowHeader3; }
            set { strShowHeader3 = value; }
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

        public rt_OrderHeader()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            if (strShowHeader1 == "N")
            {
                txtData1.Visibility = Visibility.Collapsed;
            }
            if (strShowHeader2 == "N")
            {
                txtData2.Visibility = Visibility.Collapsed;
            }
            if (strShowHeader3 == "N")
            {
                txtData3.Visibility = Visibility.Collapsed;
            }

            txtData1.Text = strHeader1Caption;
            txtData2.Text = strHeader2Caption;
            txtData3.Text = strHeader3Caption;

            txtData1.FontSize = (float)intFontSize;
            txtData2.FontSize = (float)intFontSize;
            txtData3.FontSize = (float)intFontSize;

           

            if (strTextStyle == "normal")
            {
                txtData1.Style = this.FindResource("rtNormal") as Style;
                txtData2.Style = this.FindResource("rtNormal") as Style;
                txtData3.Style = this.FindResource("rtNormal") as Style;
                //txtData.FontStyle = FontStyles.Normal;
                //txtData.FontWeight = FontWeights.Regular;
            }
            if (strTextStyle == "italic")
            {
                txtData1.Style = this.FindResource("rtItalic") as Style;
                txtData2.Style = this.FindResource("rtItalic") as Style;
                txtData3.Style = this.FindResource("rtItalic") as Style;
                //txtData.FontStyle = FontStyles.Italic;
                //txtData.FontWeight = FontWeights.Regular;
            }
            if (strTextStyle == "bold")
            {
                txtData1.Style = this.FindResource("rtBold") as Style;
                txtData2.Style = this.FindResource("rtBold") as Style;
                txtData3.Style = this.FindResource("rtBold") as Style;
                //txtData.FontWeight = FontWeights.Bold;
            }
        }
    }
}
