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
    /// Interaction logic for frm_POSCashBackPopup.xaml
    /// </summary>
    public partial class frm_POSCashBackPopup : Window
    {
        public frm_POSCashBackPopup()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);

            ResMan.SetDecimal(numAmount, 2);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private double dblCashBackAmount;

        public double CashBackAmount
        {
            get { return dblCashBackAmount; }
            set { dblCashBackAmount = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dblCashBackAmount = double.Parse(numAmount.EditValue.ToString());
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
