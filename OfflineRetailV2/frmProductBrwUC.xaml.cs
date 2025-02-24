using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors;
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


namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frmProductBrwUC.xaml
    /// </summary>
    public partial class frmProductBrwUC : UserControl
    {
        private int rowPos = 0;
        private int intSelectedRowHandle;
        private bool blIsPOS;
        private GridColumn _searchcol;
        public bool IsPOS
        {
            get { return blIsPOS; }
            set { blIsPOS = value; }
        }
        private XtraForm fCalledMainform;
        public XtraForm CalledMainform
        {
            get { return fCalledMainform; }
            set { fCalledMainform = value; }
        }
        public frmProductBrwUC()
        {
            InitializeComponent();
        }

        public void SetDecimalPlace()
        {

            if (Data.Settings.DecimalPlace == 3) colPrice.EditSettings = new TextEditSettings() { DisplayFormat = "f3", MaskType = MaskType.Numeric };
            else colPrice.EditSettings = new TextEditSettings() { DisplayFormat = "f", MaskType = MaskType.Numeric };

            /*CultureInfo culture = new CultureInfo("en-US", true);
            culture.NumberFormat.CurrencySymbol = "€";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            DevExpress.Utils.FormatInfo.AlwaysUseThreadFormat = true;

            repPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            if (Settings.DecimalPlace == 3)
            {
                repPrice.DisplayFormat.FormatString = "#.000;[#.0];Zero";
                repPrice.Mask.EditMask = "c3";
            }
            else
            {
                repPrice.DisplayFormat.FormatString = "#.00;[#.0];Zero";
                repPrice.Mask.EditMask = "c2";
            }

            repPrice.Mask.Culture = culture;
            repPrice.Mask.UseMaskAsDisplayFormat = true;*/
        }
    }
}
