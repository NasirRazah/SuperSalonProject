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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSCCInfo.xaml
    /// </summary>
    public partial class frm_POSCCInfo : Window
    {
        public frm_POSCCInfo()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private double dblAmount;
        public double Amount
        {
            get { return dblAmount; }
            set { dblAmount = value; }
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Credit_Card_Info_ ;
            lbAmt.Text = dblAmount.ToString();
        }
    }
}
