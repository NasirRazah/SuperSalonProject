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
    /// Interaction logic for frm_POSDiscountDlg.xaml
    /// </summary>
    public partial class frm_POSDiscountDlg : Window
    {
        private double dblDiscountAmount;
        private double dblDiscountPercentage;
        private double dblSubtotal;
        private double dblNewSubtotal;
        private string strDiscountReason;
        private string strlastchanged = "";
        public frm_POSDiscountDlg()
        {
            InitializeComponent();
        }
        public double DiscountAmount
        {
            get { return dblDiscountAmount; }
            set { dblDiscountAmount = value; }
        }

        public double DiscountPercentage
        {
            get { return dblDiscountPercentage; }
            set { dblDiscountPercentage = value; }
        }

        public double Subtotal
        {
            get { return dblSubtotal; }
            set { dblSubtotal = value; }
        }

        public double NewSubtotal
        {
            get { return dblNewSubtotal; }
            set { dblNewSubtotal = value; }
        }

        public string DiscountReason
        {
            get { return strDiscountReason; }
            set { strDiscountReason = value; }
        }



        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3)
            {
                ResMan.SetDecimal(numSubtotal, 3);
                ResMan.SetDecimal(numNewSubtotal, 3);
                ResMan.SetDecimal(numDiscount, 3);
                ResMan.SetDecimal(numDiscountPerc, 3);
            }
            else
            {
                ResMan.SetDecimal(numSubtotal, 2);
                ResMan.SetDecimal(numNewSubtotal, 2);
                ResMan.SetDecimal(numDiscount, 2);
                ResMan.SetDecimal(numDiscountPerc, 2);
            }
        }

        private void RearrangeValue()
        {
            numSubtotal.EditValue = GeneralFunctions.FormatDouble(dblSubtotal);
            dblNewSubtotal = GeneralFunctions.FormatDouble(dblSubtotal - dblDiscountAmount);
            numNewSubtotal.EditValue = GeneralFunctions.FormatDouble(dblNewSubtotal);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (strlastchanged == "Amount")
            {
                if (double.Parse(numDiscount.EditValue.ToString()) == 0) numDiscountPerc.EditValue = 0;
                else numDiscountPerc.EditValue = GeneralFunctions.FormatDouble((100 * double.Parse(numDiscount.EditValue.ToString())) / double.Parse(numSubtotal.EditValue.ToString()));
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }

            if (strlastchanged == "Percent")
            {
                numDiscount.EditValue = GeneralFunctions.FormatDouble((double.Parse(numSubtotal.EditValue.ToString()) * double.Parse(numDiscountPerc.EditValue.ToString())) / 100);
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }
            strDiscountReason = txtReason.Text.Trim();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, Properties.Resources.POS_Help, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private void Kbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void numDiscount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (strlastchanged == "Amount")
            {
                if (double.Parse(numDiscount.EditValue.ToString()) == 0) numDiscountPerc.EditValue = 0;
                else numDiscountPerc.EditValue = GeneralFunctions.FormatDouble((100 * double.Parse(numDiscount.EditValue.ToString())) / double.Parse(numSubtotal.EditValue.ToString() ));
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }
        }

        private void numDiscountPerc_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (strlastchanged == "Percent")
            {
                numDiscount.EditValue = GeneralFunctions.FormatDouble((double.Parse(numSubtotal.EditValue.ToString() )* double.Parse(numDiscountPerc.EditValue.ToString())) / 100);
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }
        }

        private void numDiscountPerc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            strlastchanged = "Percent";
        }

        private void numDiscount_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            strlastchanged = "Amount";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDecimalPlace();
            RearrangeValue();
        }

        private void numDiscountPerc_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                numDiscount.EditValue = GeneralFunctions.FormatDouble((double.Parse(numSubtotal.EditValue.ToString()) * double.Parse(numDiscountPerc.EditValue.ToString())) / 100);
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }
        }

        private void numDiscount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            {
                if (double.Parse(numDiscount.EditValue.ToString()) == 0) numDiscountPerc.EditValue = 0;
                else numDiscountPerc.EditValue = GeneralFunctions.FormatDouble((100 * double.Parse(numDiscount.EditValue.ToString())) / double.Parse(numSubtotal.EditValue.ToString()));
                dblDiscountAmount = GeneralFunctions.FormatDouble(double.Parse(numDiscount.EditValue.ToString()));
                dblDiscountPercentage = GeneralFunctions.FormatDouble(double.Parse(numDiscountPerc.EditValue.ToString()));
                RearrangeValue();
                strlastchanged = "";
            }
        }
    }
}
