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
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSQty.xaml
    /// </summary>
    public partial class frm_POSQty : Window
    {
        private int intPressCount = 0;

        private int intPressQty = 1;

        private int iReturnQty;

        public int ReturnQty
        {
            get { return iReturnQty; }
            set { iReturnQty = value; }
        }


        public frm_POSQty()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void PrepareQty(string val)
        {
            if (intPressCount == 0)
            {
                if (val == "0")
                {
                    /*if (pnlQty.Height == 68)
                    {
                        pnlQty.Height = 1;
                        this.Height = 279;
                    }*/
                    lbQty.Text = "1";
                    return;
                }
                else
                {
                    intPressQty = GeneralFunctions.fnInt32(val);
                    intPressCount++;
                    /*if (pnlQty.Height == 1)
                    {
                        pnlQty.Height = 68;
                        this.Height = 346;
                    }*/
                    lbQty.Text = intPressQty.ToString();
                }
            }
            else
            {
                intPressQty = GeneralFunctions.fnInt32(intPressQty.ToString() + val);
                intPressCount++;
                /*if (pnlQty.Height == 1)
                {
                    pnlQty.Height = 68;
                    this.Height = 346;
                }*/
                lbQty.Text = intPressQty.ToString();
            }
        }

        private void BtnKeyCancel_Click(object sender, RoutedEventArgs e)
        {
            iReturnQty = intPressQty;
            DialogResult = true;
            Close();
        }

        private void btnKeyEnter_Click(object sender, RoutedEventArgs e)
        {
            iReturnQty = intPressQty;
            DialogResult = true;
        }

        private void btnKey1_Click(object sender, RoutedEventArgs e)
        {
            PrepareQty((sender as System.Windows.Controls.Button).Content.ToString());
            
        }
    }
}
