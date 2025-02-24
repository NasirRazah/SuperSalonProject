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
    /// Interaction logic for frm_MarkdownReportDlg.xaml
    /// </summary>
    public partial class frm_MarkdownReportDlg : Window
    {
        public frm_MarkdownReportDlg()
        {
            InitializeComponent();
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }
    }
}
