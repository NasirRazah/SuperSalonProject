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
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;

namespace OfflineRetailV2.ReceiptTemplateControls
{
    /// <summary>
    /// Interaction logic for rt_GroupAll.xaml
    /// </summary>
    public partial class rt_GroupAll : UserControl
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

        private bool boolSelected;

        public bool Selected
        {
            get { return boolSelected; }
            set { boolSelected = value; SetSelection(); }
        }

        private int intSL;

        public int SL
        {
            get { return intSL; }
            set { intSL = value; }
        }

        private int intID;

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        private string strGroupName;

        public string GroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }

        private int intGroupSL;

        public int GroupSL
        {
            get { return intGroupSL; }
            set { intGroupSL = value; }
        }

        private int intGroupSubSL;

        public int GroupSubSL
        {
            get { return intGroupSubSL; }
            set { intGroupSubSL = value; }
        }




        private string strGroupData;

        public string GroupData
        {
            get { return strGroupData; }
            set { strGroupData = value; SetValue(); }
        }


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


        private int intCtrlPositionTop;

        public int CtrlPositionTop
        {
            get { return intCtrlPositionTop; }
            set { intCtrlPositionTop = value; }
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
            set { intCtrlHeight = value; }
        }
        
        private byte[] bytdata;

        public byte[] byt
        {
            get { return bytdata; }
            set { bytdata = value; }
        }

        public rt_GroupAll()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            if (strGroupName == "Separator")
            {
                rt_Separator ctrl = new rt_Separator();

                ctrl.GroupData = strGroupData;
                pnlData.Children.Add(ctrl);
            }
            else if (strGroupName == "Item/Price Header")
            {
                rt_OrderHeader ctrl = new rt_OrderHeader();
                ctrl.TextStyle = strTextStyle;
                ctrl.FontSize = intFontSize;
                ctrl.ShowHeader1 = strShowHeader1;
                ctrl.ShowHeader2 = strShowHeader2;
                ctrl.ShowHeader3 = strShowHeader3;

                ctrl.Header1Caption = strHeader1Caption;
                ctrl.Header2Caption = strHeader2Caption;
                ctrl.Header3Caption = strHeader3Caption;

                ctrl.GroupData = strGroupData;
                pnlData.Children.Add(ctrl);
            }
            else if (strGroupName == "Logo")
            {
                rt_Logo ctrl = new rt_Logo();
                ctrl.TextAlign = strTextAlign;
                ctrl.CtrlWidth = intCtrlWidth;
                ctrl.CtrlHeight = intCtrlWidth;
                pnlData.Children.Add(ctrl);
            }
            else if (strGroupName == "Barcode")
            {
                rt_Barcode ctrl = new rt_Barcode();
                ctrl.TextAlign = strTextAlign;
                ctrl.CtrlWidth = intCtrlWidth;
                ctrl.CtrlHeight = intCtrlWidth;
                pnlData.Children.Add(ctrl);
            }
            else if (strGroupName == "Image")
            { 
                rt_Image ctrl = new rt_Image();
                ctrl.byt = bytdata;
                ctrl.TextAlign = strTextAlign;
                ctrl.CtrlWidth = intCtrlWidth;
                ctrl.CtrlHeight = intCtrlWidth;
                pnlData.Children.Add(ctrl);
            }
            else
            {
                rt_DataGeneral ctrl = new rt_DataGeneral();

                ctrl.TextAlign = strTextAlign;
                ctrl.TextStyle = strTextStyle;
                ctrl.FontSize = intFontSize;
                ctrl.GroupData = strGroupData;
                pnlData.Children.Add(ctrl);

            }
        }

        private void SetSelection()
        {
            if (boolSelected)
            {
                imgUp.Visibility = Visibility.Visible;
                imgDown.Visibility = Visibility.Visible;
                imgAdd.Visibility = Visibility.Hidden;
                //imgAdd.Source = this.FindResource("Collapse") as ImageSource;
            }
            else
            {
                imgUp.Visibility = Visibility.Collapsed;
                imgDown.Visibility = Visibility.Collapsed;
                imgAdd.Visibility = Visibility.Visible;
                //imgAdd.Source = this.FindResource("Expand") as ImageSource;
            }
        }
    }
}
