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
using DevExpress.Xpf.Editors;
using System.Reflection;
using System.Reflection;
using DevExpress.Xpf.Editors;
using System.Data;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for NumKeyboard.xaml
    /// </summary>
    public partial class NumKeyboard : Window
    {
        private bool bCallFromGrid;
        public bool CallFromGrid
        {
            get { return bCallFromGrid; }
            set { bCallFromGrid = value; }
        }


        private int intGridDecimal;
        public int GridDecimal
        {
            get { return intGridDecimal; }
            set { intGridDecimal = value; }
        }

        private int intGridRowIndex;
        public int GridRowIndex
        {
            get { return intGridRowIndex; }
            set { intGridRowIndex = value; }
        }

        private string strGridColumnName;
        public string GridColumnName
        {
            get { return strGridColumnName; }
            set { strGridColumnName = value; }
        }

        private string strWindowName;
        public string WindowName
        {
            get { return strWindowName; }
            set { strWindowName = value; }
        }

        private TextEdit tbGridInputControl;
        public TextEdit GridInputControl
        {
            get { return tbGridInputControl; }
            set { tbGridInputControl = value; }
        }

        private TextEdit txtEditG;

        public TextEdit EditControlG
        {
            get { return txtEditG; }
            set { txtEditG = value; }
        }

        private int CurrentSelectionStart = 0;
        private Window fcalledform;
        private UserControl fcalledusrctrl;
        private TextBox txtEdit;
        private bool blIsFloat;
        private int intDecimals;
        private bool boolIsWindow;
        private PasswordBox txtEditP;
        private bool blPasswordFocused;

        private bool blInitialPassword = false;

        public bool PasswordFocused
        {
            get { return blPasswordFocused; }
            set { blPasswordFocused = value; }
        }

        public PasswordBox EditControlP
        {
            get { return txtEditP; }
            set { txtEditP = value; }
        }

        public UserControl calledusercontrol
        {
            get { return fcalledusrctrl; }
            set { fcalledusrctrl = value; }
        }

        public bool IsWindow
        {
            get { return boolIsWindow; }
            set { boolIsWindow = value; }
        }

        public Window CalledForm
        {
            get { return fcalledform; }
            set { fcalledform = value; }
        }

        public TextBox EditControl
        {
            get { return txtEdit; }
            set { txtEdit = value; }
        }

        public bool IsFloat
        {
            get { return blIsFloat; }
            set { blIsFloat = value; }
        }

        public int Decimals
        {
            get { return intDecimals; }
            set { intDecimals = value; }
        }

        public NumKeyboard()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }



        private void OnCloseCOmmand(object obj)
        {
            try
            {
                txtEdit.Tag = null;
            }
            catch
            {

            }

            try
            {
                txtEditG.Tag = null;
            }
            catch
            {

            }
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }

        private void SetSelectionTextBox(TextBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }

        private void BtnKey1_Click(object sender, RoutedEventArgs e)
        {
            KeyConverter k = new KeyConverter();
            Key mykey;
            string keyval = "";
            if ((sender as System.Windows.Controls.Button).Content.ToString() == ".")
            {
                mykey = Key.Decimal;
                keyval = ".";
            }
            else if ((sender as System.Windows.Controls.Button).Content.ToString() == "-")
            {
                mykey = Key.OemMinus;
                keyval = "-";
            }
            else if ((sender as System.Windows.Controls.Button).Content.ToString() == "System.Windows.Controls.Image")
            {
                if ((sender as System.Windows.Controls.Button).Tag.ToString() == "Delete")
                {
                    mykey = Key.Delete;
                    keyval = "Delete";
                }
                else
                {
                    mykey = Key.Back;
                    keyval = "Back";
                }
            }

            else
            {
                mykey = (Key)k.ConvertFromString((sender as System.Windows.Controls.Button).Content.ToString());
                keyval = (sender as System.Windows.Controls.Button).Content.ToString();
            }

            if (!bCallFromGrid)
            {
                IInputElement focusedControl = boolIsWindow ? FocusManager.GetFocusedElement(fcalledform) : FocusManager.GetFocusedElement(fcalledusrctrl);
                if (!((focusedControl is TextBox) || (focusedControl is PasswordBox)))
                {
                    Close();
                    return;
                }

                if (focusedControl is TextBox)
                {
                    if ((focusedControl as TextBox).IsReadOnly) return;
                    txtEdit = (focusedControl as TextBox);
                    txtEdit.Text = (focusedControl as TextBox).Text;
                    int iStart = txtEdit.SelectionStart;



                    txtEdit.Focus();

                    if (CurrentSelectionStart > 0)
                    {
                        txtEdit.SelectionLength = 0;
                        txtEdit.SelectionStart = CurrentSelectionStart;
                        e.Handled = true;
                    }



                    blPasswordFocused = false;



                    if (keyval == "Delete")
                    {
                        GeneralFunctions.SendKeys.SendNumeric(mykey, keyval);
                        txtEdit.Text = "0";
                        txtEdit.SelectAll();
                        txtEdit.Tag = null;
                        CurrentSelectionStart = 0;
                    }
                    else if (keyval == "Back")
                    {
                        string returnNumber = "";
                        object tg = txtEdit.Tag;
                        bool decimalpress = false;
                        if (tg != null)
                        {
                            decimalpress = tg.ToString() == ".";
                        }
                        int ReturnCursorPos = InputNumberRightLoLeft(keyval, CurrentSelectionStart, txtEdit.Text, decimalpress, ref returnNumber);
                        GeneralFunctions.SendKeys.SendNumeric(mykey, returnNumber);
                        txtEdit.Focus();
                        txtEdit.Select(txtEdit.Text.Length + 1, 0);
                        /*
                        txtEdit.SelectionLength = 0;
                        txtEdit.SelectionStart = ReturnCursorPos;
                        CurrentSelectionStart = txtEdit.SelectionStart;*/
                    }
                    else
                    {

                        string returnNumber = "";
                        object tg = txtEdit.Tag;
                        bool decimalpress = false;
                        if (tg != null)
                        {
                            decimalpress = tg.ToString() == ".";
                        }
                        int ReturnCursorPos = InputNumberRightLoLeft(keyval, CurrentSelectionStart, txtEdit.Text, decimalpress, ref returnNumber);
                        GeneralFunctions.SendKeys.SendNumeric(mykey, returnNumber);
                        if (keyval == ".")
                        {
                            txtEdit.Tag = ".";
                        }
                        txtEdit.Focus();

                        txtEdit.Select(txtEdit.Text.Length + 1, 0);

                        /*txtEdit.SelectionLength = 0;
                        txtEdit.SelectionStart = ReturnCursorPos;
                        CurrentSelectionStart = txtEdit.SelectionStart;*/


                    }



                }


                if (focusedControl is PasswordBox)
                {
                    if ((focusedControl as PasswordBox).IsEnabled == false) return;
                    txtEditP = (focusedControl as PasswordBox);
                    txtEditP.Password = (focusedControl as PasswordBox).Password;
                    txtEditP.Focus();
                    if (txtEditP.Password.Length > 0)
                    {
                        SetSelection(txtEditP, txtEditP.Password.Length, 0);
                    }
                    blPasswordFocused = true;

                    if (blInitialPassword)
                    {
                        if (txtEditP.Password.Length == txtEditP.MaxLength)
                        {
                            if ((keyval == "0") || (keyval == "1") || (keyval == "2") || (keyval == "3") || (keyval == "4") || (keyval == "5") || (keyval == "6") || (keyval == "7") ||
                                (keyval == "8") || (keyval == "9"))
                            {
                                txtEditP.Password = "";
                                txtEditP.SelectAll();
                            }
                        }
                        blInitialPassword = false;
                    }

                    GeneralFunctions.SendKeys.SendSpecial(mykey, keyval);

                    if ((keyval == "Delete") || (keyval == "Back"))
                    {

                    }
                    else
                    {
                        //txtEdit.SelectionStart = iStart + 1;
                    }

                    if (keyval == "Delete")
                    {
                        txtEditP.Password = "";
                        txtEditP.SelectAll();
                        //Dispatcher.BeginInvoke(new Action(() => (txtEdit).SelectAll()));
                    }

                }

            }
            else
            {

                if (tbGridInputControl.IsReadOnly) return;
                txtEditG = (tbGridInputControl as TextEdit);
                if (intGridDecimal > 0)
                {
                    txtEditG.Text = GeneralFunctions.fnDouble((tbGridInputControl as TextEdit).Text).ToString("f" + intGridDecimal.ToString());
                }
                else txtEditG.Text = (tbGridInputControl as TextEdit).Text;
                int iStart = txtEditG.SelectionStart;



                txtEditG.Focus();

                if (CurrentSelectionStart > 0)
                {
                    txtEditG.SelectionLength = 0;
                    txtEditG.SelectionStart = CurrentSelectionStart;
                    e.Handled = true;
                }



                blPasswordFocused = false;



                if (keyval == "Delete")
                {
                    //GeneralFunctions.SendKeys.SendNumeric(mykey, keyval);
                    txtEditG.Text = "0";
                    txtEditG.Tag = null;
                    txtEditG.SelectAll();
                    CurrentSelectionStart = 0;
                    if (strWindowName == "Product Vendor")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "Product Printer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "PO")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_PODlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Tax")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.Administrator.frm_TaxDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "Recv")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_ReceivingDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "StockTake")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_StocktakeDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Closeout")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_CloseoutCountDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Transfer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_TransferDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                }
                else if (keyval == "Back")
                {
                    string returnNumber = "";
                    object tg = txtEditG.Tag;
                    bool decimalpress = false;
                    if (tg != null)
                    {
                        decimalpress = tg.ToString() == ".";
                    }
                    int ReturnCursorPos = InputNumberGrdRightLoLeft(keyval, CurrentSelectionStart, txtEditG.Text, decimalpress, ref returnNumber);
                    //GeneralFunctions.SendKeys.SendNumeric(mykey, returnNumber);
                    txtEditG.Text = returnNumber;
                    if (strWindowName == "Product Vendor")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "Product Printer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "PO")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_PODlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Tax")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.Administrator.frm_TaxDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "Recv")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_ReceivingDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "StockTake")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_StocktakeDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Closeout")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_CloseoutCountDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Transfer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_TransferDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (keyval == ".")
                    {
                        txtEditG.Tag = ".";
                    }
                    txtEditG.Focus();

                    txtEditG.Select(txtEditG.Text.Length + 1, 0);
                }
                else if (keyval == "-")
                {
                    string prevval = txtEditG.Text;

                    if (prevval.Contains("-"))
                    {
                        return;
                    }
                    else
                    {
                        string returnNumber = "-" + prevval;
                        CurrentSelectionStart++;
                        //GeneralFunctions.SendKeys.SendNumeric(mykey, returnNumber);

                        txtEditG.Text = returnNumber;
                        if (strWindowName == "Product Vendor")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }
                        if (strWindowName == "Product Printer")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }
                        if (strWindowName == "PO")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_PODlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        if (strWindowName == "Tax")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.Administrator.frm_TaxDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        if (strWindowName == "Recv")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.frm_ReceivingDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        if (strWindowName == "StockTake")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_StocktakeDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        if (strWindowName == "Closeout")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.frm_CloseoutCountDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        if (strWindowName == "Transfer")
                        {
                            (fcalledform as OfflineRetailV2.UserControls.frm_TransferDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                        }

                        txtEditG.Focus();
                        txtEditG.SelectionLength = 0;
                        txtEditG.SelectionStart = CurrentSelectionStart;
                        CurrentSelectionStart = txtEditG.SelectionStart;


                    }
                }
                else
                {

                    string returnNumber = "";
                    object tg = txtEditG.Tag;
                    bool decimalpress = false;
                    if (tg != null)
                    {
                        decimalpress = tg.ToString() == ".";
                    }
                    int ReturnCursorPos = InputNumberGrdRightLoLeft(keyval, CurrentSelectionStart, txtEditG.Text, decimalpress, ref returnNumber);
                    //GeneralFunctions.SendKeys.SendNumeric(mykey, returnNumber);
                    if (returnNumber != "")
                        txtEditG.Text = returnNumber;
                    
                    if (strWindowName == "Product Vendor")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "Product Printer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }
                    if (strWindowName == "PO")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_PODlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Tax")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.Administrator.frm_TaxDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Recv")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_ReceivingDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "StockTake")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_StocktakeDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Closeout")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_CloseoutCountDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Transfer")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.frm_TransferDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (strWindowName == "Matrix")
                    {
                        (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_MatrixProduct).frm_MatrixProductUC.UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
                    }

                    if (keyval == ".")
                    {
                        txtEditG.Tag = ".";
                    }
                    txtEditG.Focus();

                    txtEditG.Select(txtEditG.Text.Length + 1, 0);

                }


            }

        }


        private int GetHowManyTimeOccurenceCharInString(string text, char c)
        {
            int count = 0;
            foreach (char ch in text)
            {
                if (ch.Equals(c))
                {
                    count++;
                }

            }
            return count;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (boolIsWindow)
            {
                if (fcalledform == null)
                {
                    Close();
                    return;
                }

                IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledform);
                if (focusedControl == null)
                {
                    Close();
                    return;
                }

                if (!bCallFromGrid)
                {

                    if (!((focusedControl is TextBox) || (focusedControl is PasswordBox)))
                    {
                        Close();
                        return;
                    }
                }

                if (focusedControl is TextBox)
                {
                    if ((focusedControl as TextBox).IsReadOnly)
                    {
                        SetupQuickTendering();
                        Close();
                        return;
                    }
                }

                focusedControl.Focus();


                if (focusedControl is TextBox)
                {
                    EditControl = focusedControl as TextBox;
                    PasswordFocused = false;
                    CurrentSelectionStart = 0;
                    SetupQuickTendering();

                }

                /*if (focusedControl is DevExpress.Xpf.Editors.TextEdit)
                {
                    EditControl = focusedControl as TextBox;
                    PasswordFocused = false;
                    CurrentSelectionStart = 0;

                }*/

                if (bCallFromGrid)
                {
                    EditControlG = focusedControl as TextEdit;
                    EditControlG.Focusable = true;
                    PasswordFocused = false;
                    CurrentSelectionStart = 0;
                }

                if (focusedControl is PasswordBox)
                {
                    EditControlP = focusedControl as PasswordBox;
                    PasswordFocused = true;
                    blInitialPassword = true;
                }
            }
            else
            {
                if (fcalledusrctrl == null)
                {
                    Close();
                    return;
                }


            }

            try
            {
                txtEdit.Tag = null;
            }
            catch
            {

            }

            try
            {
                txtEditG.Tag = null;
            }
            catch
            {

            }
            this.Topmost = true;
        }




        private int InputNumber(string ival, int currCursorPos, string presentnumber, ref string returnNumber)
        {
            int retcursorpos = currCursorPos;



            string prevval = presentnumber;
            int positiondot = presentnumber.IndexOf(".");
            if (positiondot != -1) // Decimal value
            {
                string digitsbeforedecimal = presentnumber.Substring(0, positiondot);
                string decimal1 = presentnumber.Substring(positiondot + 1, 1);
                string decimal2 = presentnumber.Substring(positiondot + 2, 1);
                if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                    || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                {
                    if (currCursorPos == 0)
                    {
                        if (prevval.Contains("-"))
                        {
                            returnNumber = "-" + ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            returnNumber = ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }

                        /*
                        if (digitsbeforedecimal.Length == 1)
                        {
                            if (digitsbeforedecimal == "0")
                            {
                                if (ival == "0")
                                {
                                    digitsbeforedecimal = "0";
                                    numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                    retcursorpos = currCursorPos + 1;
                                }
                                else
                                {
                                    digitsbeforedecimal = ival;
                                    numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                    retcursorpos = currCursorPos + 1;
                                }

                            }
                            else
                            {
                                digitsbeforedecimal = ival;
                                numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;

                            }
                        }
                        else
                        {

                        }*/
                    }
                    else
                    {
                        if (currCursorPos == positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            if (Convert.ToInt32(digitsbeforecursor) == 0)
                            {
                                returnNumber = ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos;
                            }
                            else
                            {
                                returnNumber = digitsbeforecursor + ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            string digitsaftercursor = presentnumber.Substring(currCursorPos + 1);
                            returnNumber = digitsbeforecursor + ival + digitsaftercursor;
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            if (currCursorPos == positiondot + 1)
                            {
                                returnNumber = digitsbeforedecimal + "." + ival + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos == positiondot + 2)
                            {
                                returnNumber = digitsbeforedecimal + "." + decimal1 + ival;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos > positiondot + 2)
                            {
                                returnNumber = prevval;
                                retcursorpos = currCursorPos;
                            }
                        }
                    }
                }
                if (ival == ".")
                {
                    if (currCursorPos <= positiondot)
                    {

                        retcursorpos = positiondot + 1;
                    }
                }

                if (ival == "Back") // backspace
                {
                    if (currCursorPos == 0)
                    {
                        returnNumber = prevval;
                        retcursorpos = 0;
                    }
                    else
                    {
                        if (currCursorPos == positiondot + 3)
                        {
                            returnNumber = digitsbeforedecimal + "." + decimal1 + "0";
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 2)
                        {
                            returnNumber = digitsbeforedecimal + "." + "0" + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 1)
                        {
                            retcursorpos = positiondot;
                        }
                        else if (currCursorPos == positiondot)
                        {
                            if (digitsbeforedecimal.Length == 1)
                            {
                                returnNumber = "0" + "." + decimal1 + decimal2;
                                retcursorpos = 0;
                            }
                            else
                            {
                                digitsbeforedecimal = digitsbeforedecimal.Substring(0, currCursorPos - 1);
                                returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string partdigitafter = digitsbeforedecimal.Substring(currCursorPos, positiondot - currCursorPos);
                            string partdigitbefore = digitsbeforedecimal.Substring(0, currCursorPos - 1);

                            returnNumber = partdigitbefore + partdigitafter + "." + decimal1 + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }
            }
            else  // Integer Value
            {

                if (txtEdit.Text.Length == txtEdit.MaxLength)
                {
                    if ((ival == "0") || (ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5") || (ival == "6") || (ival == "7") ||
                        (ival == "8") || (ival == "9"))
                    {
                        returnNumber = prevval;
                        retcursorpos = currCursorPos;
                    }

                    if (ival == "Back") // backspace
                    {
                        if (currCursorPos == 0)
                        {
                            returnNumber = prevval;
                            retcursorpos = 0;
                        }
                        else
                        {
                            if (prevval.Length == 1)
                            {
                                returnNumber = "0";
                                retcursorpos = 0;
                            }
                            else
                            {
                                prevval = prevval.Substring(0, currCursorPos - 1);
                                returnNumber = prevval;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                    }
                }
                else
                {
                    if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                        || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                    {
                        if (currCursorPos == 0)
                        {
                            if (prevval.Contains("-"))
                            {
                                returnNumber = "-" + ival;
                                retcursorpos = currCursorPos + 1;
                            }
                            else
                            {
                                returnNumber = ival;
                                retcursorpos = currCursorPos + 1;
                            }


                        }
                        else
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            returnNumber = digitsbeforecursor + ival;
                            retcursorpos = currCursorPos + 1;
                        }
                    }


                    if (ival == "Back") // backspace
                    {
                        if (currCursorPos == 0)
                        {
                            returnNumber = prevval;
                            retcursorpos = 0;
                        }
                        else
                        {
                            if (prevval.Length == 1)
                            {
                                returnNumber = "0";
                                retcursorpos = 0;
                            }
                            else
                            {
                                prevval = prevval.Substring(0, currCursorPos - 1);
                                returnNumber = prevval;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                    }
                }
            }


            return retcursorpos;
        }


        private int InputNumberGrd(string ival, int currCursorPos, string presentnumber, ref string returnNumber)
        {
            int retcursorpos = currCursorPos;



            string prevval = presentnumber;
            int positiondot = presentnumber.IndexOf(".");
            if (positiondot != -1) // Decimal value
            {
                string digitsbeforedecimal = presentnumber.Substring(0, positiondot);
                string decimal1 = presentnumber.Substring(positiondot + 1, 1);
                string decimal2 = presentnumber.Substring(positiondot + 2, 1);
                if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                    || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                {
                    if (currCursorPos == 0)
                    {
                        if (prevval.Contains("-"))
                        {
                            returnNumber = "-" + ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            returnNumber = ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }

                        /*
                        if (digitsbeforedecimal.Length == 1)
                        {
                            if (digitsbeforedecimal == "0")
                            {
                                if (ival == "0")
                                {
                                    digitsbeforedecimal = "0";
                                    numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                    retcursorpos = currCursorPos + 1;
                                }
                                else
                                {
                                    digitsbeforedecimal = ival;
                                    numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                    retcursorpos = currCursorPos + 1;
                                }

                            }
                            else
                            {
                                digitsbeforedecimal = ival;
                                numAmount.Text = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;

                            }
                        }
                        else
                        {

                        }*/
                    }
                    else
                    {
                        if (currCursorPos == positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            if (Convert.ToInt32(digitsbeforecursor) == 0)
                            {
                                returnNumber = ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos;
                            }
                            else
                            {
                                returnNumber = digitsbeforecursor + ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            string digitsaftercursor = presentnumber.Substring(currCursorPos + 1);
                            returnNumber = digitsbeforecursor + ival + digitsaftercursor;
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            if (currCursorPos == positiondot + 1)
                            {
                                returnNumber = digitsbeforedecimal + "." + ival + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos == positiondot + 2)
                            {
                                returnNumber = digitsbeforedecimal + "." + decimal1 + ival;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos > positiondot + 2)
                            {
                                returnNumber = prevval;
                                retcursorpos = currCursorPos;
                            }
                        }
                    }
                }
                if (ival == ".")
                {
                    if (currCursorPos <= positiondot)
                    {

                        retcursorpos = positiondot + 1;
                    }
                }

                if (ival == "Back") // backspace
                {
                    if (currCursorPos == 0)
                    {
                        returnNumber = prevval;
                        retcursorpos = 0;
                    }
                    else
                    {
                        if (currCursorPos == positiondot + 3)
                        {
                            returnNumber = digitsbeforedecimal + "." + decimal1 + "0";
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 2)
                        {
                            returnNumber = digitsbeforedecimal + "." + "0" + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 1)
                        {
                            retcursorpos = positiondot;
                        }
                        else if (currCursorPos == positiondot)
                        {
                            if (digitsbeforedecimal.Length == 1)
                            {
                                returnNumber = "0" + "." + decimal1 + decimal2;
                                retcursorpos = 0;
                            }
                            else
                            {
                                digitsbeforedecimal = digitsbeforedecimal.Substring(0, currCursorPos - 1);
                                returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string partdigitafter = digitsbeforedecimal.Substring(currCursorPos, positiondot - currCursorPos);
                            string partdigitbefore = digitsbeforedecimal.Substring(0, currCursorPos - 1);

                            returnNumber = partdigitbefore + partdigitafter + "." + decimal1 + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }
            }
            else  // Integer Value
            {

                if (txtEditG.Text.Length == txtEditG.MaxLength)
                {
                    if ((ival == "0") || (ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5") || (ival == "6") || (ival == "7") ||
                        (ival == "8") || (ival == "9"))
                    {
                        returnNumber = prevval;
                        retcursorpos = currCursorPos;
                    }

                    if (ival == "Back") // backspace
                    {
                        if (currCursorPos == 0)
                        {
                            returnNumber = prevval;
                            retcursorpos = 0;
                        }
                        else
                        {
                            if (prevval.Length == 1)
                            {
                                returnNumber = "0";
                                retcursorpos = 0;
                            }
                            else
                            {
                                prevval = prevval.Substring(0, currCursorPos - 1);
                                returnNumber = prevval;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                    }
                }
                else
                {
                    if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                        || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                    {
                        if (currCursorPos == 0)
                        {
                            if (prevval.Contains("-"))
                            {
                                returnNumber = "-" + ival;
                                retcursorpos = currCursorPos + 1;
                            }
                            else
                            {
                                returnNumber = ival;
                                retcursorpos = currCursorPos + 1;
                            }


                        }
                        else
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            returnNumber = digitsbeforecursor + ival;
                            retcursorpos = currCursorPos + 1;
                        }
                    }


                    if (ival == "Back") // backspace
                    {
                        if (currCursorPos == 0)
                        {
                            returnNumber = prevval;
                            retcursorpos = 0;
                        }
                        else
                        {
                            if (prevval.Length == 1)
                            {
                                returnNumber = "0";
                                retcursorpos = 0;
                            }
                            else
                            {
                                prevval = prevval.Substring(0, currCursorPos - 1);
                                returnNumber = prevval;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                    }
                }
            }


            return retcursorpos;
        }



        private int InputNumberGrdRightLoLeft(string ival, int currCursorPos, string presentnumber, bool booldecimalpress, ref string returnNumber)
        {
            int retcursorpos = currCursorPos;



            string prevval = presentnumber;
            int positiondot = presentnumber.IndexOf(".");
            if (positiondot != -1) // Decimal value
            {
                string digitsbeforedecimal = presentnumber.Substring(0, positiondot);
                string decimal1 = presentnumber.Substring(positiondot + 1, 1);
                string decimal2 = presentnumber.Substring(positiondot + 2, 1);

                /*
                if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                    || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                {
                    if (currCursorPos == 0)
                    {
                        if (prevval.Contains("-"))
                        {
                            returnNumber = "-" + ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            returnNumber = ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }

                        
                    }
                    else
                    {
                        if (currCursorPos == positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            if (Convert.ToInt32(digitsbeforecursor) == 0)
                            {
                                returnNumber = ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos;
                            }
                            else
                            {
                                returnNumber = digitsbeforecursor + ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            string digitsaftercursor = presentnumber.Substring(currCursorPos + 1);
                            returnNumber = digitsbeforecursor + ival + digitsaftercursor;
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            if (currCursorPos == positiondot + 1)
                            {
                                returnNumber = digitsbeforedecimal + "." + ival + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos == positiondot + 2)
                            {
                                returnNumber = digitsbeforedecimal + "." + decimal1 + ival;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos > positiondot + 2)
                            {
                                returnNumber = prevval;
                                retcursorpos = currCursorPos;
                            }
                        }
                    }
                }
                if (ival == ".")
                {
                    if (currCursorPos <= positiondot)
                    {

                        retcursorpos = positiondot + 1;
                    }
                }

                if (ival == "Back") // backspace
                {
                    if (currCursorPos == 0)
                    {
                        returnNumber = prevval;
                        retcursorpos = 0;
                    }
                    else
                    {
                        if (currCursorPos == positiondot + 3)
                        {
                            returnNumber = digitsbeforedecimal + "." + decimal1 + "0";
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 2)
                        {
                            returnNumber = digitsbeforedecimal + "." + "0" + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 1)
                        {
                            retcursorpos = positiondot;
                        }
                        else if (currCursorPos == positiondot)
                        {
                            if (digitsbeforedecimal.Length == 1)
                            {
                                returnNumber = "0" + "." + decimal1 + decimal2;
                                retcursorpos = 0;
                            }
                            else
                            {
                                digitsbeforedecimal = digitsbeforedecimal.Substring(0, currCursorPos - 1);
                                returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string partdigitafter = digitsbeforedecimal.Substring(currCursorPos, positiondot - currCursorPos);
                            string partdigitbefore = digitsbeforedecimal.Substring(0, currCursorPos - 1);

                            returnNumber = partdigitbefore + partdigitafter + "." + decimal1 + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }

                */


                if (ival == "-")
                {
                    if (prevval.Contains("-"))
                    {
                        returnNumber = prevval;
                    }
                    else
                    {
                        returnNumber = "-" + prevval;
                    }
                }

                if (ival == "Back")
                {
                    if (!booldecimalpress)
                    {
                        if ((digitsbeforedecimal == "-0") || (digitsbeforedecimal == "0"))
                        {
                            decimal2 = decimal1;
                            decimal1 = "0";
                            returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                        }
                        else
                        {
                            decimal2 = decimal1;
                            decimal1 = digitsbeforedecimal.Substring(digitsbeforedecimal.Length - 1);
                            digitsbeforedecimal = digitsbeforedecimal.Substring(0, digitsbeforedecimal.Length - 1);
                            if ((digitsbeforedecimal == "-") || (digitsbeforedecimal == ""))
                            {
                                digitsbeforedecimal = digitsbeforedecimal + "0";
                            }
                            returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                        }
                    }
                    else
                    {
                        decimal2 = decimal1;
                        decimal1 = "0";
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;

                    }
                }

                if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                    || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                {
                    if (!booldecimalpress)
                    {
                        if ((digitsbeforedecimal == "-0") || (digitsbeforedecimal == "0"))
                        {
                            digitsbeforedecimal = digitsbeforedecimal.Replace("0", decimal1);
                        }
                        else
                        {
                            digitsbeforedecimal = digitsbeforedecimal + decimal1;
                        }
                        decimal1 = decimal2;
                        decimal2 = ival;
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                    }
                    else
                    {
                        decimal1 = decimal2;
                        decimal2 = ival;
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                    }
                }
                }

                else
                {

                if (txtEditG.Text.Length == txtEditG.MaxLength)
                {
                    if ((ival == "0") || (ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5") || (ival == "6") || (ival == "7") ||
                        (ival == "8") || (ival == "9"))
                    {
                        returnNumber = prevval;
                        retcursorpos = currCursorPos;
                    }

                    if (ival == "Back") // backspace
                    {
                        if (prevval.Length == 1)
                        {
                            returnNumber = "0";
                            retcursorpos = 0;
                        }
                        else
                        {
                            prevval = prevval.Substring(0, prevval.Length - 1);

                            returnNumber = prevval;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }
                else
                {
                    if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                        || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                    {
                        if (currCursorPos == 0)
                        {
                            if (prevval.Contains("-"))
                            {
                                string actualval = prevval.Substring(1);
                                if (actualval == "0")
                                {
                                    actualval = ival;
                                }
                                else
                                {
                                    actualval = actualval + ival;
                                }
                                returnNumber = "-" + actualval;

                                retcursorpos = currCursorPos + 1;
                            }
                            else
                            {
                                string actualval = prevval;
                                if (actualval == "0")
                                {
                                    actualval = ival;
                                }
                                else
                                {
                                    actualval = actualval + ival;
                                }

                                returnNumber = actualval;
                                retcursorpos = currCursorPos + 1;
                            }

                        }
                        else
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            returnNumber = digitsbeforecursor + ival;
                            retcursorpos = currCursorPos + 1;

                        }
                    }


                    if (ival == "Back") // backspace
                    {
                        if (prevval.Length == 1)
                        {
                            returnNumber = "0";
                            retcursorpos = 0;
                        }
                        else
                        {
                            prevval = prevval.Substring(0, prevval.Length - 1);
                            returnNumber = prevval;
                            retcursorpos = currCursorPos - 1;
                        }

                    }
                }
            }


            return retcursorpos;
        }



        private int InputNumberRightLoLeft(string ival, int currCursorPos, string presentnumber, bool booldecimalpress, ref string returnNumber)
        {
            int retcursorpos = currCursorPos;



            string prevval = presentnumber;
            int positiondot = presentnumber.IndexOf(".");
            if (positiondot != -1) // Decimal value
            {
                string digitsbeforedecimal = presentnumber.Substring(0, positiondot);
                string decimal1 = presentnumber.Substring(positiondot + 1, 1);
                string decimal2 = presentnumber.Substring(positiondot + 2, 1);

                if (ival == "-")
                {
                    if (prevval.Contains("-"))
                    {
                        returnNumber = prevval;
                    }
                    else
                    {
                        returnNumber = "-" + prevval;
                    }
                }

                if (ival == "Back")
                {
                    if (!booldecimalpress)
                    {
                        if ((digitsbeforedecimal == "-0") || (digitsbeforedecimal == "0"))
                        {
                            decimal2 = decimal1;
                            decimal1 = "0";
                            returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                        }
                        else
                        {
                            decimal2 = decimal1;
                            decimal1 = digitsbeforedecimal.Substring(digitsbeforedecimal.Length - 1);
                            digitsbeforedecimal = digitsbeforedecimal.Substring(0, digitsbeforedecimal.Length - 1);
                            if ((digitsbeforedecimal == "-") || (digitsbeforedecimal == ""))
                            {
                                digitsbeforedecimal = digitsbeforedecimal + "0";
                            }
                            returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                        }
                    }
                    else
                    {
                        decimal2 = decimal1;
                        decimal1 = "0";
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;

                    }
                }

                if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                    || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                {
                    if (!booldecimalpress)
                    {
                        if ((digitsbeforedecimal == "-0") || (digitsbeforedecimal == "0"))
                        {
                            digitsbeforedecimal = digitsbeforedecimal.Replace("0", decimal1);
                        }
                        else
                        {
                            digitsbeforedecimal = digitsbeforedecimal + decimal1;
                        }
                        decimal1 = decimal2;
                        decimal2 = ival;
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                    }
                    else
                    {
                        decimal1 = decimal2;
                        decimal2 = ival;
                        returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                    }

                    /*
                    if (currCursorPos == 0)
                    {
                        if (prevval.Contains("-"))
                        {
                            returnNumber = "-" + ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            returnNumber = ival + ".00";
                            retcursorpos = currCursorPos + 1;
                        }

                       
                    }
                    else
                    {
                        if (currCursorPos == positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            if (Convert.ToInt32(digitsbeforecursor) == 0)
                            {
                                returnNumber = ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos;
                            }
                            else
                            {
                                returnNumber = digitsbeforecursor + ival + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            string digitsaftercursor = presentnumber.Substring(currCursorPos + 1);
                            returnNumber = digitsbeforecursor + ival + digitsaftercursor;
                            retcursorpos = currCursorPos + 1;
                        }
                        else
                        {
                            if (currCursorPos == positiondot + 1)
                            {
                                returnNumber = digitsbeforedecimal + "." + ival + decimal2;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos == positiondot + 2)
                            {
                                returnNumber = digitsbeforedecimal + "." + decimal1 + ival;
                                retcursorpos = currCursorPos + 1;
                            }

                            if (currCursorPos > positiondot + 2)
                            {
                                returnNumber = prevval;
                                retcursorpos = currCursorPos;
                            }
                        }
                    }
                    */
                }



                /*
                if (ival == ".")
                {
                    if (currCursorPos <= positiondot)
                    {

                        retcursorpos = positiondot + 1;
                    }
                }

                if (ival == "Back") // backspace
                {
                    if (currCursorPos == 0)
                    {
                        returnNumber = prevval;
                        retcursorpos = 0;
                    }
                    else
                    {
                        if (currCursorPos == positiondot + 3)
                        {
                            returnNumber = digitsbeforedecimal + "." + decimal1 + "0";
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 2)
                        {
                            returnNumber = digitsbeforedecimal + "." + "0" + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                        else if (currCursorPos == positiondot + 1)
                        {
                            retcursorpos = positiondot;
                        }
                        else if (currCursorPos == positiondot)
                        {
                            if (digitsbeforedecimal.Length == 1)
                            {
                                returnNumber = "0" + "." + decimal1 + decimal2;
                                retcursorpos = 0;
                            }
                            else
                            {
                                digitsbeforedecimal = digitsbeforedecimal.Substring(0, currCursorPos - 1);
                                returnNumber = digitsbeforedecimal + "." + decimal1 + decimal2;
                                retcursorpos = currCursorPos - 1;
                            }
                        }
                        else if (currCursorPos < positiondot)
                        {
                            string partdigitafter = digitsbeforedecimal.Substring(currCursorPos, positiondot - currCursorPos);
                            string partdigitbefore = digitsbeforedecimal.Substring(0, currCursorPos - 1);

                            returnNumber = partdigitbefore + partdigitafter + "." + decimal1 + decimal2;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }*/


            }
            else  // Integer Value
            {

                if (txtEdit.Text.Length == txtEdit.MaxLength)
                {
                    if ((ival == "0") || (ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5") || (ival == "6") || (ival == "7") ||
                        (ival == "8") || (ival == "9"))
                    {
                        returnNumber = prevval;
                        retcursorpos = currCursorPos;
                    }

                    if (ival == "Back") // backspace
                    {
                        if (prevval.Length == 1)
                        {
                            returnNumber = "0";
                            retcursorpos = 0;
                        }
                        else
                        {
                            prevval = prevval.Substring(0,prevval.Length - 1);
                            returnNumber = prevval;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }
                else
                {
                    if ((ival == "1") || (ival == "2") || (ival == "3") || (ival == "4") || (ival == "5")
                        || (ival == "6") || (ival == "7") || (ival == "8") || (ival == "9") || (ival == "0"))
                    {
                        if (currCursorPos == 0)
                        {
                            if (prevval.Contains("-"))
                            {
                                string actualval = prevval.Substring(1);
                                if (actualval  == "0")
                                {
                                    actualval = ival;
                                }
                                else
                                {
                                    actualval = actualval + ival;
                                }
                                returnNumber = "-" + actualval;

                                retcursorpos = currCursorPos + 1;
                            }
                            else
                            {
                                string actualval = prevval;
                                if (actualval == "0")
                                {
                                    actualval = ival;
                                }
                                else
                                {
                                    actualval = actualval + ival;
                                }

                                returnNumber = actualval;
                                retcursorpos = currCursorPos + 1;
                            }


                        }
                        else
                        {
                            string digitsbeforecursor = presentnumber.Substring(0, currCursorPos);
                            returnNumber = digitsbeforecursor + ival;
                            retcursorpos = currCursorPos + 1;
                        }
                    }


                    if (ival == "Back") // backspace
                    {
                        if (prevval.Length == 1)
                        {
                            returnNumber = "0";
                            retcursorpos = 0;
                        }
                        else
                        {
                            prevval = prevval.Substring(0, prevval.Length - 1);
                            returnNumber = prevval;
                            retcursorpos = currCursorPos - 1;
                        }
                    }
                }
            }


            return retcursorpos;
        }
        private void SetupQuickTendering()
        {
            DataTable dtblQT = GeneralFunctions.GetQuickTenderingCurrencies_TenderScreen();

            if (dtblQT.Rows.Count == 0)
            {
                btnC1.Visibility = btnC2.Visibility = btnC3.Visibility = btnC4.Visibility = btnC5.Visibility = btnC6.Visibility = Visibility.Hidden;
            }
            else
            {
                int i = 0;
                foreach (DataRow dr in dtblQT.Rows)
                {
                    i++;
                    if (i == 1)
                    {
                        btnC1.Content = dr["CurrencyName"].ToString();
                        btnC1.Tag = dr["CurrencyValue"].ToString();
                    }

                    if (i == 2)
                    {
                        btnC2.Content = dr["CurrencyName"].ToString();
                        btnC2.Tag = dr["CurrencyValue"].ToString();
                    }

                    if (i == 3)
                    {
                        btnC3.Content = dr["CurrencyName"].ToString();
                        btnC3.Tag = dr["CurrencyValue"].ToString();
                    }

                    if (i == 4)
                    {
                        btnC4.Content = dr["CurrencyName"].ToString();
                        btnC4.Tag = dr["CurrencyValue"].ToString();
                    }

                    if (i == 5)
                    {
                        btnC5.Content = dr["CurrencyName"].ToString();
                        btnC5.Tag = dr["CurrencyValue"].ToString();
                    }

                    if (i == 6)
                    {
                        btnC6.Content = dr["CurrencyName"].ToString();
                        btnC6.Tag = dr["CurrencyValue"].ToString();
                    }

                }

                if (i == 1)
                {
                    btnC2.Visibility = btnC3.Visibility = btnC4.Visibility = btnC5.Visibility = btnC6.Visibility = Visibility.Hidden;
                }

                if (i == 2)
                {
                    btnC3.Visibility = btnC4.Visibility = btnC5.Visibility = btnC6.Visibility = Visibility.Hidden;
                }

                if (i == 3)
                {
                    btnC4.Visibility = btnC5.Visibility = btnC6.Visibility = Visibility.Hidden;
                }

                if (i == 4)
                {
                    btnC5.Visibility = btnC6.Visibility = Visibility.Hidden;
                }

                if (i == 5)
                {
                    btnC6.Visibility = Visibility.Hidden;
                }
            }


        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            double val = GeneralFunctions.fnDouble((sender as System.Windows.Controls.Button).Tag);


            if (!bCallFromGrid)
            {
                IInputElement focusedControl = boolIsWindow ? FocusManager.GetFocusedElement(fcalledform) : FocusManager.GetFocusedElement(fcalledusrctrl);
                if (!(focusedControl is TextBox))
                {
                    Close();
                    return;
                }

                if (focusedControl is TextBox)
                {
                    if ((focusedControl as TextBox).IsReadOnly) return;
                    txtEdit = (focusedControl as TextBox);
                    txtEdit.Text = (focusedControl as TextBox).Text;
                    int iStart = txtEdit.SelectionStart;



                    txtEdit.Focus();
                    txtEdit.Text = "0.00";
                    txtEdit.Text = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(val)).ToString();












                }
            }


                //


            }
    }
}
