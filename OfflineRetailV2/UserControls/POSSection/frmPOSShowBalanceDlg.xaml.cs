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
using OfflineRetailV2;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSShowBalanceDlg.xaml
    /// </summary>
    public partial class frmPOSShowBalanceDlg : Window
    {
        private double dblTotalsale;
        private double dblTender;
        private double dblChange;

        public double Totalsale
        {
            get { return dblTotalsale; }
            set { dblTotalsale = value; }
        }

        public double Tender
        {
            get { return dblTender; }
            set { dblTender = value; }
        }

        public double Change
        {
            get { return dblChange; }
            set { dblChange = value; }
        }

        public frmPOSShowBalanceDlg()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            Close();
        }

        private void SetDecimalPlace()
        {
            if (Data.Settings.DecimalPlace == 3)
            {
                //numTotalsale.Decimals = 3;
                //numTender.Decimals = 3;
                //numChangeDue.Decimals = 3;
            }
            else
            {
                //numTotalsale.Decimals = 2;
                //numTender.Decimals = 2;
                //numChangeDue.Decimals = 2;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Sale;
            SetDecimalPlace();
            numTotalsale.Text =  SystemVariables.CurrencySymbol + " " +  dblTotalsale.ToString(Settings.DecimalPlace == 3 ? "f3" : "f2");
            numTender.Text = SystemVariables.CurrencySymbol + " " + dblTender.ToString(Settings.DecimalPlace == 3 ? "f3" : "f2");
            numChangeDue.Text = SystemVariables.CurrencySymbol + " " + dblChange.ToString(Settings.DecimalPlace == 3 ? "f3" : "f2");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
