using OfflineRetailV2.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_SCALE_Tare_Change.xaml
    /// </summary>
    public partial class frm_SCALE_Tare_Change : Window
    {
        public frm_SCALE_Tare_Change()
        {
            InitializeComponent();

            Loaded += Frm_SCALE_Tare_Change_Loaded;
        }

        private void Frm_SCALE_Tare_Change_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Grad_ScaleType != "")
            {
                if ((dblWeight >= Settings.Grad_S_Range1) && (dblWeight <= Settings.Grad_S_Range2))
                {
                    if (Settings.S_Check2Digit == "Y")
                    {
                        GeneralFunctions.SetDecimal(txtTare, 2);
                        bl2DigitTare = true;

                    }
                }

                if ((dblWeight >= Settings.Grad_D_Range1) && (dblWeight <= Settings.Grad_D_Range2))
                {
                    if (Settings.D_Check2Digit == "Y")
                    {
                        GeneralFunctions.SetDecimal(txtTare, 2);
                        bl2DigitTare = true;
                    }
                }
            }
            txtTare.Text = dblTare.ToString();
            txtTare.Text = dblTare.ToString();
        }

        private double dblWeight;
        private double dblTare;
        private bool blCallFromPOS;
        public double Tare
        {
            get { return dblTare; }
            set { dblTare = value; }
        }

        private bool bl2DigitTare = false;

        public bool b2DigitTare
        {
            get { return bl2DigitTare; }
            set { bl2DigitTare = value; }
        }

        public bool bCallFromPOS
        {
            get { return blCallFromPOS; }
            set { blCallFromPOS = value; }
        }

        public double Weight
        {
            get { return dblWeight; }
            set { dblWeight = value; }
        }

        private void txtTare_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Char.IsDigit(new KeyConverter().ConvertToString(e.Key)[0])) || (e.Key == Key.D8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (btnHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show( Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "POS Help", MessageBoxButton.OK, MessageBoxImage.Information);
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
        ErrorProvider errorProvider1 = new ErrorProvider();
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            if (HasError)
            {

                new MessageBoxWindow().Show(Error, "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            dblTare = Convert.ToDouble(txtTare.Text);
            DialogResult = true;
        }
        public bool HasError = false;
        public string Error = "";
        private void txtTare_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            errorProvider1.Clear();
            if (Settings.Grad_ScaleType != "")
            {
                if ((Settings.MaxScaleWeight > 0) && (Convert.ToDouble(txtTare.Text) > Settings.MaxScaleWeight))
                {
                    txtTare.Focus();
                    Error = "Tare can not be greater than Max. Weight";
                    HasError = true;
                }
                else
                {
                    if ((dblWeight >= Settings.Grad_S_Range1) && (dblWeight <= Settings.Grad_S_Range2))
                    {
                        if (Convert.ToDouble(txtTare.Text) <= Settings.Grad_S_Range2)
                        {
                            try
                            {
                                if (Convert.ToDecimal(txtTare.Text) % (decimal)Settings.Grad_S_Graduation == 0)
                                {
                                    Error = "";
                                    HasError = false;
                                }
                                else
                                {
                                    txtTare.Focus();
                                    Error = "Graduation division should be " + GeneralFunctions.GetFormattedScaleGraduation(Settings.S_Check2Digit == "Y" ? true : false, Settings.Grad_S_Graduation);
                                    HasError = true;
                                   
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            txtTare.Focus();
                            Error = "Tare should be between " + String.Format("{0:0.000}", Settings.Grad_S_Range1) + " and " +
                                    String.Format("{0:0.000}", Settings.Grad_S_Range2);
                            HasError = true;
                        }
                    }

                    if (Settings.Grad_ScaleType == "Y")
                    {
                        if ((dblWeight >= Settings.Grad_D_Range1) && (dblWeight <= Settings.Grad_D_Range2))
                        {
                            if (Convert.ToDouble(txtTare.Text) <= Settings.Grad_D_Range1)
                            {
                                try
                                {
                                    if (Convert.ToDecimal(txtTare.Text) % (decimal)Settings.Grad_D_Graduation == 0)
                                    {
                                        Error = "";
                                        HasError = false;
                                    }
                                    else
                                    {
                                        txtTare.Focus();
                                        Error = "Graduation division should be " + GeneralFunctions.GetFormattedScaleGraduation(Settings.D_Check2Digit == "Y" ? true : false, Settings.Grad_D_Graduation);
                                        HasError = true;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                txtTare.Focus();
                                Error = "Tare should be <= " + String.Format("{0:0.000}", Settings.Grad_D_Range1);
                                HasError = true;
                            }
                        }
                    }
                }
            }
        }


        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
