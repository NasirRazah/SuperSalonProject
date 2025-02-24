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
using System.Data;
using System.Data.SqlClient;
using OfflineRetailV2;
using OfflineRetailV2.Data;
namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSRentalSelectDlg.xaml
    /// </summary>
    public partial class frmPOSRentalSelectDlg : Window
    {
        private int intID;
        private string CurrentRentType;
        private string strRentType;
        private double dblRentValue;
        private double dblRentDuration;
        private double dblRentDeposit;
        private double mival = 0;
        private double hrval = 0;
        private double hfdyval = 0;
        private double dyval = 0;
        private double wkval = 0;
        private double mtval = 0;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        private bool blcallforrentsetbeforereturn;
        public bool bcallforrentsetbeforereturn
        {
            get { return blcallforrentsetbeforereturn; }
            set { blcallforrentsetbeforereturn = value; }
        }


        public int PID
        {
            get { return intID; }
            set { intID = value; }
        }

        public string RentType
        {
            get { return strRentType; }
            set { strRentType = value; }
        }

        public double RentValue
        {
            get { return dblRentValue; }
            set { dblRentValue = value; }
        }

        public double RentDuration
        {
            get { return dblRentDuration; }
            set { dblRentDuration = value; }
        }

        public double RentDeposit
        {
            get { return dblRentDeposit; }
            set { dblRentDeposit = value; }
        }
        public frmPOSRentalSelectDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            CloseKeyboards();
            ResMan.closeKeyboard();
            Close();
        }

        private void CloseKeyboards()
        {
    
            if (nkybrd != null)
            {
                nkybrd.Close();

            }
        }

        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }






        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }


        private void LoadData()
        {
            

            PosDataObject.POS objps = new PosDataObject.POS();
            objps.Connection = SystemVariables.Conn;
            DataTable dtbl = objps.FetchProductRentalData(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                lbprod.Text = dr["SKU"].ToString() + " - " + dr["Description"].ToString();
                numRentalMinHour.Text = GeneralFunctions.fnDouble(dr["RentalMinHour"].ToString()).ToString("f2");
                numRentalMinAmt.Text = GeneralFunctions.fnDouble(dr["RentalMinAmount"].ToString()).ToString("f2");
                numRentalDeposit.Text = GeneralFunctions.fnDouble(dr["RentalDeposit"].ToString()).ToString("f2");
                mival = GeneralFunctions.fnDouble(dr["RentalPerMinute"].ToString());
                hrval = GeneralFunctions.fnDouble(dr["RentalPerHour"].ToString());
                hfdyval = GeneralFunctions.fnDouble(dr["RentalPerHalfDay"].ToString());
                dyval = GeneralFunctions.fnDouble(dr["RentalPerDay"].ToString());
                wkval = GeneralFunctions.fnDouble(dr["RentalPerWeek"].ToString());
                mtval = GeneralFunctions.fnDouble(dr["RentalPerMonth"].ToString());
                dblRentDeposit = GeneralFunctions.fnDouble(numRentalDeposit.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = "Rental Option";
            LoadData();
            if (!blcallforrentsetbeforereturn)
            {
                CurrentRentType = "day";
                lbduration.Text = CurrentRentType;
                strRentType = "DY";
                numDuration.Text = "1";
                numRent.Text = dyval.ToString("f2");

                numDuration.Focus();
                numDuration.SelectAll();
            }
            btnday.Style = this.FindResource("GeneralButtonStyle2") as Style;

            if (Settings.CalculateRentLater == "Y")
            {
                if (!blcallforrentsetbeforereturn)
                {
                    label1.Visibility = label2.Visibility = label3.Visibility = lbduration.Visibility = numDuration.Visibility
                        = numRent.Visibility = numTotal.Visibility = Visibility.Hidden;
                    btnOK.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (strRentType == "MI")
                    {
                        CurrentRentType = "min.";
                    }

                    if (strRentType == "HR")
                    {
                        CurrentRentType = "hr.";
                    }

                    if (strRentType == "HD")
                    {
                        CurrentRentType = "half day";
                    }

                    if (strRentType == "DY")
                    {
                        CurrentRentType = "day";
                    }

                    if (strRentType == "WK")
                    {
                        CurrentRentType = "week";
                    }
                    if (strRentType == "MN")
                    {
                        CurrentRentType = "month";
                    }
                    lbduration.Text = CurrentRentType;
                    SetRentTypeButtonColor();
                    grpctrl.IsEnabled = false;

                    numDuration.Text = dblRentDuration.ToString("f");
                    numRent.Text = dblRentValue.ToString("f2");

                    numDuration.Focus();
                    numDuration.SelectAll();
                }
            }
        }

        private void SetRentTypeButtonColor()
        {
            if (CurrentRentType == "") return;
            foreach (Control cr in grpctrl.Children)
            {
                if (cr is Button)
                {
                    if (cr.Tag.ToString() == CurrentRentType)
                    {
                        (cr as Button).Style = this.FindResource("GeneralButtonStyle2") as Style; 
                    }
                    else
                    {
                        (cr as Button).Style = this.FindResource("GeneralButtonStyle") as Style;
                    }
                }
            }
        }

        private void Btnmin_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                CurrentRentType = (sender as Button).Tag.ToString();
                lbduration.Text = CurrentRentType;
                SetRentTypeButtonColor();
                if (CurrentRentType == "min.")
                {
                    strRentType = "MI";
                    numDuration.Text = "30";
                    numRent.Text = mival.ToString("f2");

                }
                else if (CurrentRentType == "hr.")
                {
                    strRentType = "HR";
                    numDuration.Text = "1";
                    numRent.Text = hrval.ToString("f2");
                }
                else if (CurrentRentType == "half day")
                {
                    strRentType = "HD";
                    numDuration.Text = "1";
                    numRent.Text = hfdyval.ToString("f2");
                }
                else if (CurrentRentType == "day")
                {
                    strRentType = "DY";
                    numDuration.Text = "1";
                    numRent.Text = dyval.ToString("f2");
                }
                else if (CurrentRentType == "week")
                {
                    strRentType = "WK";
                    numDuration.Text = "1";
                    numRent.Text = wkval.ToString("f2");
                }
                else
                {
                    strRentType = "MN";
                    numDuration.Text = "1";
                    numRent.Text = mtval.ToString("f2"); ;
                }
            }

            if (Settings.CalculateRentLater == "Y")
            {
                numDuration.Text = "0";
                if (ValidSelection())
                {
                    dblRentDuration = GeneralFunctions.fnDouble(numDuration.Text);
                    dblRentValue = GeneralFunctions.fnDouble(numRent.Text);
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private bool ValidSelection()
        {
            if ((blcallforrentsetbeforereturn) || (Settings.CalculateRentLater == "N"))
            {
                if (GeneralFunctions.fnDouble(numDuration.Text) <= 0)
                {
                    DocMessage.MsgInformation("Invalid Duration");
                    GeneralFunctions.SetFocus(numDuration);
                    return false;
                }
            }
            /*if (blcallforrentsetbeforereturn)
            {
                if (numRent.Value <= 0)
                {
                    DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Invalid Rent");
                    GeneralFunctions.SetFocus(numRent);
                    return false;
                }
            }*/

            if ((blcallforrentsetbeforereturn) || (Settings.CalculateRentLater == "N"))
            {
                if ((GeneralFunctions.fnDouble(numRentalMinAmt.Text) == 0) && (GeneralFunctions.fnDouble(numRentalMinHour.Text) == 0)) return true;
                else if ((GeneralFunctions.fnDouble(numRentalMinAmt.Text) == 0) && (GeneralFunctions.fnDouble(numRentalMinHour.Text) != 0))
                {
                    if (!SatisfyMinHour())
                    {
                        DocMessage.MsgInformation("Minimum rent (in Hours) not satisfied");
                        GeneralFunctions.SetFocus(numDuration);
                        return false;
                    }
                }
                else if ((GeneralFunctions.fnDouble(numRentalMinAmt.Text) != 0) && (GeneralFunctions.fnDouble(numRentalMinHour.Text) == 0))
                {
                    if (!SatisfyMinAmount())
                    {
                        DocMessage.MsgInformation("Minimum rent (in Amt.) not satisfied");
                        GeneralFunctions.SetFocus(numDuration);
                        return false;
                    }
                }
                else
                {
                    if (SatisfyMinHour() && SatisfyMinAmount())
                    {
                        return true;
                    }
                    else
                    {
                        DocMessage.MsgInformation("Both minimum rent (in hr.) and minimum rent (in amt.) are not satisfied");
                        GeneralFunctions.SetFocus(numDuration);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool SatisfyMinHour()
        {
            if (strRentType == "MI") if (GeneralFunctions.fnDouble(numDuration.Text) < GeneralFunctions.fnDouble(numRentalMinHour.Text) * 60) return false;
            if (strRentType == "HR") if (GeneralFunctions.fnDouble(numDuration.Text) < GeneralFunctions.fnDouble(numRentalMinHour.Text)) return false;
            if (strRentType == "HD") if (GeneralFunctions.fnDouble(numDuration.Text) * 12 < GeneralFunctions.fnDouble(numRentalMinHour.Text)) return false;
            if (strRentType == "DY") if (GeneralFunctions.fnDouble(numDuration.Text) * 24 < GeneralFunctions.fnDouble(numRentalMinHour.Text)) return false;
            if (strRentType == "WK") if (GeneralFunctions.fnDouble(numDuration.Text) * 24 * 7 < GeneralFunctions.fnDouble(numRentalMinHour.Text)) return false;
            if (strRentType == "MN") if (GeneralFunctions.fnDouble(numDuration.Text) * 24 * 7 * 30 < GeneralFunctions.fnDouble(numRentalMinHour.Text)) return false;
            return true;
        }

        private bool SatisfyMinAmount()
        {
            if (GeneralFunctions.fnDouble(numDuration.Text) * GeneralFunctions.fnDouble(numRent.Text) < GeneralFunctions.fnDouble(numRentalMinAmt.Text)) return false;
            return true;
        }

        private void NumDuration_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((numDuration.EditValue != null) || (numRent.EditValue != null))
                numTotal.Text = (GeneralFunctions.fnDouble(numDuration.Text) * GeneralFunctions.fnDouble(numRent.Text)).ToString("f2");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            Close();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidSelection())
            {
                dblRentDuration = GeneralFunctions.fnDouble(numDuration.Text);
                dblRentValue = GeneralFunctions.fnDouble(numRent.Text);
                DialogResult = true;
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
        }
    }
}
