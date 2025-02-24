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

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_EmpMain.xaml
    /// </summary>
    public partial class frm_EmpMain : Window
    {
        public frm_EmpMain()
        {
            InitializeComponent();
        }

        private bool callfrompos;
        public bool bcallfrompos
        {
            get { return callfrompos; }
            set { callfrompos = value; }
        }
    }
}
