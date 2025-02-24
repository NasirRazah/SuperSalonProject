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
    /// Interaction logic for frm_POSDemoPaymentGateway.xaml
    /// </summary>
    public partial class frm_POSDemoPaymentGateway : Window
    {
        public frm_POSDemoPaymentGateway()
        {
            InitializeComponent();
        }
        private double dblAmount;

        public double Amount
        {
            get { return dblAmount; }
            set { dblAmount = value; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtCVV_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtCVV.Text.Trim() != "")
            {
                DialogResult =true;
            }
        }

        private void textEdit1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

            if (textEdit1.Text.Trim() != "")
            {
                DialogResult = true;
            }
        }

        private void txtCard_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtCard.Text.Trim() != "")
            {
                DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Demo_Payment_Gateway;
            numAmount.Text = dblAmount.ToString();
        }
    }
}
