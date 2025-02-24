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
    /// Interaction logic for rt_Separator.xaml
    /// </summary>
    public partial class rt_Separator : UserControl
    {
        
        private string strGroupData;

        public string GroupData
        {
            get { return strGroupData; }
            set { strGroupData = value; SetValue(); }
        }

        public rt_Separator()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            txtData.Text = strGroupData;
            
        }
    }
}
