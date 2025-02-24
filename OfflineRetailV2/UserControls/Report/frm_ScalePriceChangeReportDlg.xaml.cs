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

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ScalePriceChangeReportDlg.xaml
    /// </summary>
    public partial class frm_ScalePriceChangeReportDlg : Window
    {
        public frm_ScalePriceChangeReportDlg()
        {
            InitializeComponent();
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }
    }
}
