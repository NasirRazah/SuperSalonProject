using Microsoft.PointOfService;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frmPrintBarcodeDlg.xaml
    /// </summary>
    public partial class frmPrintBarcodeDlg : Window
    {
        SerialPort _sport = null;
        PosExplorer m_posExplorer = null;
        PosCommon m_posCommon = null;

        private string strProductDesc;
        private string strSKU;
        private string strProductPrice;
        private int intProductDecimalPlace;
        private int intQty;
        private int intskip = 0;
        private int intLabelType;


        private string VID = "";
        private string VNAME = "";
        private string VPART = "";
        private string LD = "";
        private string LD2 = "";
        private string INGD = "";
        private string SPLM = "";
        private string BC = "";
        private string WHT = "";

        private string comport = "";
        private string comprintcommand = "";
        private int comdefaultqty = 0;
        private int comwrap = 0;
        private string comparafont = "";

        private bool blisbatchprint;

        public bool isbatchprint
        {
            get { return blisbatchprint; }
            set { blisbatchprint = value; }
        }

        public int LabelType
        {
            get { return intLabelType; }
            set { intLabelType = value; }
        }

        public int Qty
        {
            get { return intQty; }
            set { intQty = value; }
        }

        public string SKU
        {
            get { return strSKU; }
            set { strSKU = value; }
        }

        public string ProductDesc
        {
            get { return strProductDesc; }
            set { strProductDesc = value; }
        }

        public string ProductPrice
        {
            get { return strProductPrice; }
            set { strProductPrice = value; }
        }

        public int ProductDecimalPlace
        {
            get { return intProductDecimalPlace; }
            set { intProductDecimalPlace = value; }
        }
        public frmPrintBarcodeDlg()
        {
            InitializeComponent();
        }
    }
}
