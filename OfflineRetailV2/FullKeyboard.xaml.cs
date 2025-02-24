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
using System.Reflection;
using DevExpress.Xpf.Editors;


namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for FullKeyboard.xaml
    /// </summary>
    public partial class FullKeyboard : Window
    {
        private bool blexecsearchonreturn;
        public bool execsearchonreturn
        {
            get { return blexecsearchonreturn; }
            set { blexecsearchonreturn = value; }
        }

        private bool bCallFromGrid;
        public bool CallFromGrid
        {
            get { return bCallFromGrid; }
            set { bCallFromGrid = value; }
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

        private IInputElement staticFocusedElement;
        private Window fcalledform;
        private UserControl fcalledusrctrl;
        private bool bDirectKey = false;
        private bool bOnTop = false;

        private bool boolIsWindow;

        public bool IsWindow
        {
            get { return boolIsWindow; }
            set { boolIsWindow = value; }
        }

        public bool DirectKey
        {
            get { return bDirectKey; }
            set { bDirectKey = value; }
        }

        public bool OnTop
        {
            get { return bOnTop; }
            set { bOnTop = value; }
        }


        public Window calledform
        {
            get { return fcalledform; }
            set { fcalledform = value; }
        }

        public UserControl calledusercontrol
        {
            get { return fcalledusrctrl; }
            set { fcalledusrctrl = value; }
        }


        private TextEdit txtEditG;

        public TextEdit EditControlG
        {
            get { return txtEditG; }
            set { txtEditG = value; }
        }

        private TextBox txtEdit;
        private PasswordBox txtEditP;
        private bool blPasswordFocused;

        public bool PasswordFocused
        {
            get { return blPasswordFocused; }
            set { blPasswordFocused = value; }
        }

        private TextBox txtEditUCNext;
        private TextBox txtEditUC;

        public TextBox UCEdit
        {
            get { return txtEditUC; }
            set { txtEditUC = value; }
        }

        public TextBox UCEditNext
        {
            get { return txtEditUCNext; }
            set { txtEditUCNext = value; }
        }

        private bool bAlphaKyBrd;

        public Window CalledForm
        {
            get { return fcalledform; }
            set { fcalledform = value; }
        }



        public PasswordBox EditControlP
        {
            get { return txtEditP; }
            set { txtEditP = value; }
        }

        public TextBox EditControl
        {
            get { return txtEdit; }
            set { txtEdit = value; }
        }


        public FullKeyboard()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private void ChangeKeyBoardLayOut()
        {
            if (bAlphaKyBrd)
            {
                btnA.SetValue(Canvas.LeftProperty, Convert.ToDouble(31));
                btnB.SetValue(Canvas.LeftProperty, Convert.ToDouble(88));
                btnC.SetValue(Canvas.LeftProperty, Convert.ToDouble(145));
                btnD.SetValue(Canvas.LeftProperty, Convert.ToDouble(202));
                btnE.SetValue(Canvas.LeftProperty, Convert.ToDouble(259));
                btnF.SetValue(Canvas.LeftProperty, Convert.ToDouble(316));
                btnG.SetValue(Canvas.LeftProperty, Convert.ToDouble(373));
                btnH.SetValue(Canvas.LeftProperty, Convert.ToDouble(430));
                btnI.SetValue(Canvas.LeftProperty, Convert.ToDouble(487));
                btnJ.SetValue(Canvas.LeftProperty, Convert.ToDouble(544));

                btnA.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnB.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnC.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnD.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnE.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnF.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnG.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnH.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnI.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnJ.SetValue(Canvas.TopProperty, Convert.ToDouble(57));

                btnK.SetValue(Canvas.LeftProperty, Convert.ToDouble(88));
                btnL.SetValue(Canvas.LeftProperty, Convert.ToDouble(145));
                btnM.SetValue(Canvas.LeftProperty, Convert.ToDouble(202));
                btnN.SetValue(Canvas.LeftProperty, Convert.ToDouble(259));
                btnO.SetValue(Canvas.LeftProperty, Convert.ToDouble(316));
                btnP.SetValue(Canvas.LeftProperty, Convert.ToDouble(373));
                btnQ.SetValue(Canvas.LeftProperty, Convert.ToDouble(430));
                btnR.SetValue(Canvas.LeftProperty, Convert.ToDouble(487));
                btnS.SetValue(Canvas.LeftProperty, Convert.ToDouble(544));

                btnK.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnL.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnM.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnN.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnO.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnP.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnQ.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnR.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnS.SetValue(Canvas.TopProperty, Convert.ToDouble(114));

                btnT.SetValue(Canvas.LeftProperty, Convert.ToDouble(116));
                btnU.SetValue(Canvas.LeftProperty, Convert.ToDouble(173));
                btnV.SetValue(Canvas.LeftProperty, Convert.ToDouble(230));
                btnW.SetValue(Canvas.LeftProperty, Convert.ToDouble(287));
                btnX.SetValue(Canvas.LeftProperty, Convert.ToDouble(344));
                btnY.SetValue(Canvas.LeftProperty, Convert.ToDouble(401));
                btnZ.SetValue(Canvas.LeftProperty, Convert.ToDouble(458));

                btnT.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnU.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnV.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnW.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnX.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnY.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnZ.SetValue(Canvas.TopProperty, Convert.ToDouble(171));

            }
            else
            {

                btnQ.SetValue(Canvas.LeftProperty, Convert.ToDouble(31));
                btnW.SetValue(Canvas.LeftProperty, Convert.ToDouble(88));
                btnE.SetValue(Canvas.LeftProperty, Convert.ToDouble(145));
                btnR.SetValue(Canvas.LeftProperty, Convert.ToDouble(202));
                btnT.SetValue(Canvas.LeftProperty, Convert.ToDouble(259));
                btnY.SetValue(Canvas.LeftProperty, Convert.ToDouble(316));
                btnU.SetValue(Canvas.LeftProperty, Convert.ToDouble(373));
                btnI.SetValue(Canvas.LeftProperty, Convert.ToDouble(430));
                btnO.SetValue(Canvas.LeftProperty, Convert.ToDouble(487));
                btnP.SetValue(Canvas.LeftProperty, Convert.ToDouble(544));

                btnQ.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnW.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnE.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnR.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnT.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnY.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnU.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnI.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnO.SetValue(Canvas.TopProperty, Convert.ToDouble(57));
                btnP.SetValue(Canvas.TopProperty, Convert.ToDouble(57));

                btnA.SetValue(Canvas.LeftProperty, Convert.ToDouble(88));
                btnS.SetValue(Canvas.LeftProperty, Convert.ToDouble(145));
                btnD.SetValue(Canvas.LeftProperty, Convert.ToDouble(202));
                btnF.SetValue(Canvas.LeftProperty, Convert.ToDouble(259));
                btnG.SetValue(Canvas.LeftProperty, Convert.ToDouble(316));
                btnH.SetValue(Canvas.LeftProperty, Convert.ToDouble(373));
                btnJ.SetValue(Canvas.LeftProperty, Convert.ToDouble(430));
                btnK.SetValue(Canvas.LeftProperty, Convert.ToDouble(487));
                btnL.SetValue(Canvas.LeftProperty, Convert.ToDouble(544));

                btnA.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnS.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnD.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnF.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnG.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnH.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnJ.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnK.SetValue(Canvas.TopProperty, Convert.ToDouble(114));
                btnL.SetValue(Canvas.TopProperty, Convert.ToDouble(114));

                btnZ.SetValue(Canvas.LeftProperty, Convert.ToDouble(116));
                btnX.SetValue(Canvas.LeftProperty, Convert.ToDouble(173));
                btnC.SetValue(Canvas.LeftProperty, Convert.ToDouble(230));
                btnV.SetValue(Canvas.LeftProperty, Convert.ToDouble(287));
                btnB.SetValue(Canvas.LeftProperty, Convert.ToDouble(344));
                btnN.SetValue(Canvas.LeftProperty, Convert.ToDouble(401));
                btnM.SetValue(Canvas.LeftProperty, Convert.ToDouble(458));

                btnZ.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnX.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnC.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnV.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnB.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnN.SetValue(Canvas.TopProperty, Convert.ToDouble(171));
                btnM.SetValue(Canvas.TopProperty, Convert.ToDouble(171));


            }
        }


        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }


        private void WriteCharacter(string Ch, RoutedEventArgs e)
        {
            if (boolIsWindow)
            {
                if (!bDirectKey)
                {

                    if (!bCallFromGrid)
                    {
                        IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledform);
                        if (focusedControl == null)
                        {
                            if (!bCallFromGrid) return;
                            else
                            {
                                focusedControl = staticFocusedElement;
                            }
                        }
                        if (!((focusedControl is TextBox) || (focusedControl is PasswordBox))) return;

                        if (focusedControl is TextBox)
                        {
                            if ((focusedControl as TextBox).IsReadOnly) return;
                            txtEdit = (focusedControl as TextBox);
                            txtEdit.Text = (focusedControl as TextBox).Text;

                            txtEdit.Focus();

                            if (txtEdit.Text.Length > 0)
                            {
                                txtEdit.SelectionLength = 0;
                                txtEdit.SelectionStart = txtEdit.Text.Length;
                            }
                            blPasswordFocused = false;
                        }
                        IInputElement inpute3 = Keyboard.FocusedElement;
                        if (inpute3 != null)
                        {
                            staticFocusedElement = inpute3;
                        }
                        if (focusedControl is PasswordBox)
                        {
                            txtEditP = (focusedControl as PasswordBox);
                            txtEditP.Password = (focusedControl as PasswordBox).Password;

                            txtEditP.Focus();
                            if (txtEditP.Password.Length > 0)
                            {
                                SetSelection(txtEditP, txtEditP.Password.Length, 0);
                            }
                            blPasswordFocused = true;
                        }

                        if (fcalledform.Name == "frmPOSM")
                        {
                            if (Ch == "Enter")
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (Ch == "Enter")
                            {

                                if (fcalledform == null) return;

                                try
                                {
                                    if ((focusedControl as TextBox).AcceptsReturn == true)
                                    {
                                        (focusedControl as TextBox).Text += "\r\n";
                                        (focusedControl as TextBox).SelectionStart = (focusedControl as TextBox).Text.Length + 1;
                                        return;
                                    }
                                    else
                                    {
                                        if (fcalledform.ToString() == "OfflineRetailV2.UserControls.POSSection.frm_POSApptBookDlg")
                                        {
                                            if (focusedControl is TextBox)
                                            {
                                               if ((focusedControl as TextBox).Text.Length > 0)
                                                {
                                                    if (blexecsearchonreturn)
                                                    (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_POSApptBookDlg).ExecuteOnScreenSearch = true;
                                                }
                                            }
                                        }
                                        

                                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                        if (keyboardFocus != null)
                                        {
                                           
                                            keyboardFocus.MoveFocus(tRequest);
                                            IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                            /*if (focusedControlNew != null)
                                            {
                                                if ((focusedControlNew is TextBox))
                                                {
                                                    txtEdit = (focusedControlNew as TextBox);
                                                    txtEdit.Text = (focusedControlNew as TextBox).Text;
                                                    txtEdit.Focus();
                                                }
                                            }*/
                                            e.Handled = true;
                                        }
                                        return;
                                    }
                                }
                                catch
                                {
                                    TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                    UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                    if (keyboardFocus != null)
                                    {
                                        keyboardFocus.MoveFocus(tRequest);
                                        IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                        /*if (focusedControlNew != null)
                                        {
                                            if ((focusedControlNew is TextBox))
                                            {
                                                txtEdit = (focusedControlNew as TextBox);
                                                txtEdit.Text = (focusedControlNew as TextBox).Text;
                                                txtEdit.Focus();
                                            }
                                        }*/
                                        e.Handled = true;
                                    }
                                    return;
                                }
                            }
                        }

                        if (focusedControl == null) return;

                        if (!((focusedControl is TextBox) || (focusedControl is PasswordBox))) return;

                        if (focusedControl is TextBox)
                        {
                            if ((focusedControl as TextBox).IsReadOnly) return;

                            if (((focusedControl as TextBox).MaxLength == (focusedControl as TextBox).Text.Length) && ((focusedControl as TextBox).MaxLength > 0))
                            {
                                if (Ch != "Back") return;
                            }
                        }

                        if (focusedControl is PasswordBox)
                        {

                            if (((focusedControl as PasswordBox).MaxLength == (focusedControl as PasswordBox).Password.Length) && ((focusedControl as PasswordBox).MaxLength > 0))
                            {
                                if (Ch != "Back") return;
                            }
                        }

                        if (focusedControl is TextBox)
                        {
                            /*if (fcalledform.ActiveControl.AccessibleName != null)
                            {
                                if (fcalledform.ActiveControl.AccessibleName.ToString() == "floatctrl")
                                {
                                    if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                        (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                        (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                        (Ch == "0") || (Ch == ".") || (Ch == "{BACKSPACE}"))) return;
                                    if (Ch == ".")
                                    {
                                        if (fcalledform.ActiveControl.Text.Length > 0)
                                        {
                                            int n = fcalledform.ActiveControl.Text.IndexOf(".");
                                            if (n > 0)
                                            {
                                                return;
                                            }

                                        }
                                    }
                                }

                                if (fcalledform.ActiveControl.AccessibleName.ToString() == "intctrl")
                                {
                                    if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                        (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                        (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                        (Ch == "0") || (Ch == "{BACKSPACE}"))) return;
                                }

                            }*/
                        }

                        SendKeys(Ch);


                        e.Handled = true;
                    }
                    else
                    {
                        IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledform);
                        if (focusedControl == null)
                        {
                            focusedControl = staticFocusedElement;
                        }

                        //if (!((focusedControl is TextBox) || (focusedControl is PasswordBox))) return;

                        if (tbGridInputControl.IsReadOnly) return;
                        txtEditG = (tbGridInputControl as TextEdit);
                        txtEditG.Text = (tbGridInputControl as TextEdit).Text;

                        txtEditG.Focus();

                        if (txtEditG.Text.Length > 0)
                        {
                            txtEditG.SelectionLength = 0;
                            txtEditG.SelectionStart = txtEditG.Text.Length;
                        }
                        blPasswordFocused = false;

                        IInputElement inpute3 = Keyboard.FocusedElement;
                        if (inpute3 != null)
                        {
                            staticFocusedElement = inpute3;
                        }


                        if (Ch == "Enter")
                        {

                            if (fcalledform == null) return;

                            try
                            {
                                if ((focusedControl as TextBox).AcceptsReturn == true)
                                {
                                    (focusedControl as TextBox).Text += "\r\n";
                                    (focusedControl as TextBox).SelectionStart = (focusedControl as TextBox).Text.Length + 1;
                                    return;
                                }
                                else
                                {
                                    TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                    UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                    if (keyboardFocus != null)
                                    {
                                        keyboardFocus.MoveFocus(tRequest);
                                        IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                        /*if (focusedControlNew != null)
                                        {
                                            if ((focusedControlNew is TextBox))
                                            {
                                                txtEdit = (focusedControlNew as TextBox);
                                                txtEdit.Text = (focusedControlNew as TextBox).Text;
                                                txtEdit.Focus();
                                            }
                                        }*/
                                        e.Handled = true;
                                    }
                                    return;
                                }
                            }
                            catch
                            {
                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                if (keyboardFocus != null)
                                {
                                    keyboardFocus.MoveFocus(tRequest);
                                    IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                    /*if (focusedControlNew != null)
                                    {
                                        if ((focusedControlNew is TextBox))
                                        {
                                            txtEdit = (focusedControlNew as TextBox);
                                            txtEdit.Text = (focusedControlNew as TextBox).Text;
                                            txtEdit.Focus();
                                        }
                                    }*/
                                    e.Handled = true;
                                }
                                return;
                            }
                        }

                        if (focusedControl == null) return;

                        if (!((focusedControl is TextBox) || (focusedControl is PasswordBox))) return;

                       
                        if ((tbGridInputControl as TextEdit).IsReadOnly) return;

                        if (((tbGridInputControl as TextEdit).MaxLength == (tbGridInputControl as TextEdit).Text.Length) && ((tbGridInputControl as TextEdit).MaxLength > 0))
                        {
                            if (Ch != "Back") return;
                        }


                        SendKeys1(Ch, staticFocusedElement);


                        e.Handled = true;
                    }

                }
                else
                {
                    IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledform);
                    if (focusedControl == null) return;
                    if (Ch == "Enter")
                    {

                        if (fcalledform == null) return;

                        try
                        {
                            if ((focusedControl as TextBox).AcceptsReturn == true)
                            {
                                (focusedControl as TextBox).Text += "\r\n";
                                (focusedControl as TextBox).SelectionStart = (focusedControl as TextBox).Text.Length + 1;
                                return;
                            }
                            else
                            {
                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                if (keyboardFocus != null)
                                {
                                    keyboardFocus.MoveFocus(tRequest);
                                    IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                    /*if (focusedControlNew != null)
                                    {
                                        if ((focusedControlNew is TextBox))
                                        {
                                            txtEdit = (focusedControlNew as TextBox);
                                            txtEdit.Text = (focusedControlNew as TextBox).Text;
                                            txtEdit.Focus();
                                        }
                                    }*/
                                    e.Handled = true;
                                }


                                //bool bActivo = fcalledform.SelectNextControl(fcalledform.ActiveControl, true, false, true, true);
                                //txtEdit.Text = "";
                            }

                            return;
                        }
                        catch
                        {
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                /*if (focusedControlNew != null)
                                {
                                    if ((focusedControlNew is TextBox))
                                    {
                                        txtEdit = (focusedControlNew as TextBox);
                                        txtEdit.Text = (focusedControlNew as TextBox).Text;
                                        txtEdit.Focus();
                                    }
                                }*/
                                e.Handled = true;
                            }
                            return;
                        }
                    }


                    if (!(focusedControl is TextBox)) return;
                    if ((focusedControl as TextBox).IsReadOnly) return;
                    txtEdit = (focusedControl as TextBox);
                    txtEdit.Text = (focusedControl as TextBox).Text;

                    txtEdit.Focus();
                    if (txtEdit.Text.Length > 0)
                    {
                        txtEdit.SelectionLength = 0;
                        txtEdit.SelectionStart = txtEdit.Text.Length;
                    }


                    if (focusedControl == null) return;
                    if (!(focusedControl is TextBox)) return;
                    if ((focusedControl as TextBox).IsReadOnly) return;

                    if (((focusedControl as TextBox).MaxLength == (focusedControl as TextBox).Text.Length) && ((focusedControl as TextBox).MaxLength > 0))
                    {
                        if (Ch != "Back") return;
                    }


                    if (focusedControl is TextBox)
                    {
                        /*if (fcalledform.ActiveControl.AccessibleName != null)
                        {
                            if (fcalledform.ActiveControl.AccessibleName.ToString() == "floatctrl")
                            {
                                if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                    (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                    (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                    (Ch == "0") || (Ch == ".") || (Ch == "{BACKSPACE}"))) return;
                                if (Ch == ".")
                                {
                                    if (fcalledform.ActiveControl.Text.Length > 0)
                                    {
                                        int n = fcalledform.ActiveControl.Text.IndexOf(".");
                                        if (n > 0)
                                        {
                                            return;
                                        }

                                    }
                                }
                            }

                            if (fcalledform.ActiveControl.AccessibleName.ToString() == "intctrl")
                            {
                                if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                    (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                    (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                    (Ch == "0") || (Ch == "{BACKSPACE}"))) return;
                            }

                        }*/
                    }

                    SendKeys(Ch);


                }
            }
            else
            {
                if (!bDirectKey)
                {

                    if (txtEditUC is TextBox)
                    {
                        if ((txtEditUC as TextBox).IsReadOnly) return;
                        txtEdit = (txtEditUC as TextBox);
                        txtEdit.Text = (txtEditUC as TextBox).Text;

                        txtEdit.Focus();
                        if (txtEdit.Text.Length > 0)
                        {
                            txtEdit.SelectionLength = 0;
                            txtEdit.SelectionStart = txtEdit.Text.Length;
                        }
                        blPasswordFocused = false;
                    }



                    if (txtEditUC is TextBox)
                    {
                        if ((txtEditUC as TextBox).IsReadOnly) return;

                        if (((txtEditUC as TextBox).MaxLength == (txtEditUC as TextBox).Text.Length) && ((txtEditUC as TextBox).MaxLength > 0))
                        {
                            if (Ch != "Back") return;
                        }
                    }


                    if (Ch == "Enter")
                    {


                        try
                        {
                            if ((txtEditUC as TextBox).AcceptsReturn == true)
                            {
                                (txtEditUC as TextBox).Text += "\r\n";
                                (txtEditUC as TextBox).SelectionStart = (txtEditUC as TextBox).Text.Length + 1;
                                return;
                            }
                            else
                            {
                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                if (keyboardFocus != null)
                                {
                                    bool blf = keyboardFocus.MoveFocus(tRequest);

                                    //txtEditUCNext.Focus();

                                    /*keyboardFocus.MoveFocus(tRequest);
                                    IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledusrctrl);
                                    /*if (focusedControlNew != null)
                                    {
                                        if ((focusedControlNew is TextBox))
                                        {
                                            txtEdit = (focusedControlNew as TextBox);
                                            txtEdit.Text = (focusedControlNew as TextBox).Text;
                                            txtEdit.Focus();
                                        }
                                    }*/
                                    e.Handled = true;
                                }
                                return;
                            }
                        }
                        catch
                        {
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledform);
                                /*if (focusedControlNew != null)
                                {
                                    if ((focusedControlNew is TextBox))
                                    {
                                        txtEdit = (focusedControlNew as TextBox);
                                        txtEdit.Text = (focusedControlNew as TextBox).Text;
                                        txtEdit.Focus();
                                    }
                                }*/
                                e.Handled = true;
                            }
                            return;
                        }
                    }

                    SendKeys(Ch);
                }
                else
                {
                    IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledusrctrl);
                    if (focusedControl == null) return;
                    if (Ch == "Enter")
                    {

                        if (fcalledusrctrl == null) return;

                        try
                        {
                            if ((focusedControl as TextBox).AcceptsReturn == true)
                            {
                                (focusedControl as TextBox).Text += "\r\n";
                            }
                            else
                            {
                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                                if (keyboardFocus != null)
                                {
                                    keyboardFocus.MoveFocus(tRequest);
                                    IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledusrctrl);
                                    /*if (focusedControlNew != null)
                                    {
                                        if ((focusedControlNew is TextBox))
                                        {
                                            txtEdit = (focusedControlNew as TextBox);
                                            txtEdit.Text = (focusedControlNew as TextBox).Text;
                                            txtEdit.Focus();
                                        }
                                    }*/
                                    e.Handled = true;
                                }


                                //bool bActivo = fcalledform.SelectNextControl(fcalledform.ActiveControl, true, false, true, true);
                                //txtEdit.Text = "";
                            }

                            return;
                        }
                        catch
                        {
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                IInputElement focusedControlNew = FocusManager.GetFocusedElement(fcalledusrctrl);
                                /*if (focusedControlNew != null)
                                {
                                    if ((focusedControlNew is TextBox))
                                    {
                                        txtEdit = (focusedControlNew as TextBox);
                                        txtEdit.Text = (focusedControlNew as TextBox).Text;
                                        txtEdit.Focus();
                                    }
                                }*/
                                e.Handled = true;
                            }
                            return;
                        }
                    }


                    if (!(focusedControl is TextBox)) return;
                    if ((focusedControl as TextBox).IsReadOnly) return;
                    txtEdit = (focusedControl as TextBox);
                    txtEdit.Text = (focusedControl as TextBox).Text;

                    txtEdit.Focus();
                    if (txtEdit.Text.Length > 0)
                    {
                        txtEdit.SelectionLength = 0;
                        txtEdit.SelectionStart = txtEdit.Text.Length;
                    }

                    IInputElement inpute2 = Keyboard.FocusedElement;
                    if (focusedControl == null) return;
                    if (!(focusedControl is TextBox)) return;
                    if ((focusedControl as TextBox).IsReadOnly) return;

                    if (((focusedControl as TextBox).MaxLength == (focusedControl as TextBox).Text.Length) && ((focusedControl as TextBox).MaxLength > 0))
                    {
                        if (Ch != "Back") return;
                    }


                    if (focusedControl is TextBox)
                    {
                        /*if (fcalledform.ActiveControl.AccessibleName != null)
                        {
                            if (fcalledform.ActiveControl.AccessibleName.ToString() == "floatctrl")
                            {
                                if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                    (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                    (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                    (Ch == "0") || (Ch == ".") || (Ch == "{BACKSPACE}"))) return;
                                if (Ch == ".")
                                {
                                    if (fcalledform.ActiveControl.Text.Length > 0)
                                    {
                                        int n = fcalledform.ActiveControl.Text.IndexOf(".");
                                        if (n > 0)
                                        {
                                            return;
                                        }

                                    }
                                }
                            }

                            if (fcalledform.ActiveControl.AccessibleName.ToString() == "intctrl")
                            {
                                if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                    (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                    (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                    (Ch == "0") || (Ch == "{BACKSPACE}"))) return;
                            }

                        }*/
                    }
                    IInputElement inpute3 = Keyboard.FocusedElement;
                    SendKeys(Ch);


                }
            }

        }

        private void SendKeys(string ch)
        {
            KeyConverter k = new KeyConverter();
            Key mykey;
            if (ch == ".")
            {
                mykey = Key.Decimal;
            }
            else if (ch == "-")
            {
                mykey = Key.OemMinus;
            }
            else if (ch == "Clear")
            {
                mykey = Key.Delete;
            }
            else if (ch == "Back")
            {
                mykey = Key.Back;
            }
            else if (ch == " ")
            {
                mykey = Key.Space;
            }
            else if (ch == "~")
            {
                mykey = Key.OemTilde;
            }
            else if (ch == "`")
            {
                mykey = Key.OemBackTab;
            }
            else if (ch == "+")
            {
                mykey = Key.OemPlus;
            }
            else if (ch == ";")
            {
                mykey = Key.OemSemicolon;
            }
            else if (ch == "'")
            {
                mykey = Key.OemQuotes;
            }
            else if (ch == "|")
            {
                mykey = Key.OemPipe;
            }
            else if (ch == "/")
            {
                mykey = Key.OemBackslash;
            }
            else if (ch == "?")
            {
                mykey = Key.OemQuestion;
            }
            else if (ch == "[")
            {
                mykey = Key.OemOpenBrackets;
            }
            else if (ch == "]")
            {
                mykey = Key.OemCloseBrackets;
            }
            else if (ch == ",")
            {
                mykey = Key.OemComma;
            }
            else if (ch == "=")
            {
                mykey = Key.OemEnlw;
            }
            else if (ch == "*")
            {
                mykey = Key.Multiply;
            }
            else if (ch == "%")
            {
                mykey = Key.Divide;
            }
            else if ((ch == "%") || (ch == SystemVariables.CurrencySymbol) || (ch == "\\") || (ch == "#") || (ch == "@") || (ch == "!")
                || (ch == "^") || (ch == "&") || (ch == "!") || (ch == "(") || (ch == ")") || (ch == "_") || (ch == "\"")
                || (ch == ":") || (ch == "{") || (ch == "}")
                || (ch == "<") || (ch == ">"))
            {
                mykey = Key.Divide;
            }
            else
            {
                mykey = (Key)k.ConvertFromString(ch);
            }
            GeneralFunctions.SendKeysFullKybd.Send(mykey, ch, blPasswordFocused);


        }


        private void SendKeys1(string ch, IInputElement element)
        {
            KeyConverter k = new KeyConverter();
            Key mykey;
            if (ch == ".")
            {
                mykey = Key.Decimal;
            }
            else if (ch == "-")
            {
                mykey = Key.OemMinus;
            }
            else if (ch == "Clear")
            {
                mykey = Key.Delete;
            }
            else if (ch == "Back")
            {
                mykey = Key.Back;
            }
            else if (ch == " ")
            {
                mykey = Key.Space;
            }
            else if (ch == "~")
            {
                mykey = Key.OemTilde;
            }
            else if (ch == "`")
            {
                mykey = Key.OemBackTab;
            }
            else if (ch == "+")
            {
                mykey = Key.OemPlus;
            }
            else if (ch == ";")
            {
                mykey = Key.OemSemicolon;
            }
            else if (ch == "'")
            {
                mykey = Key.OemQuotes;
            }
            else if (ch == "|")
            {
                mykey = Key.OemPipe;
            }
            else if (ch == "/")
            {
                mykey = Key.OemBackslash;
            }
            else if (ch == "?")
            {
                mykey = Key.OemQuestion;
            }
            else if (ch == "[")
            {
                mykey = Key.OemOpenBrackets;
            }
            else if (ch == "]")
            {
                mykey = Key.OemCloseBrackets;
            }
            else if (ch == ",")
            {
                mykey = Key.OemComma;
            }
            else if (ch == "=")
            {
                mykey = Key.OemEnlw;
            }
            else if (ch == "*")
            {
                mykey = Key.Multiply;
            }
            else if (ch == "%")
            {
                mykey = Key.Divide;
            }
            else if ((ch == "%") || (ch == SystemVariables.CurrencySymbol) || (ch == "\\") || (ch == "#") || (ch == "@") || (ch == "!")
                || (ch == "^") || (ch == "&") || (ch == "!") || (ch == "(") || (ch == ")") || (ch == "_") || (ch == "\"")
                || (ch == ":") || (ch == "{") || (ch == "}")
                || (ch == "<") || (ch == ">"))
            {
                mykey = Key.Divide;
            }
            else
            {
                mykey = (Key)k.ConvertFromString(ch);
            }

            if (ch == "Clear")
            {
                txtEditG.Text = txtEditG.Text + ch;
            }
            else if (ch == "Back")
            {
                if (txtEditG.Text == "")
                {
                    return;
                }
                else
                {
                    txtEditG.Text = txtEditG.Text.Substring(0, txtEditG.Text.Length - 1);
                }
            }
            else
            {
                txtEditG.Text = txtEditG.Text + ch;
            }

            if (strWindowName == "Product Vendor")
            {
                (fcalledform as OfflineRetailV2.UserControls.POSSection.AddProductWindow).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
            }

            if (strWindowName == "PO")
            {
                (fcalledform as OfflineRetailV2.UserControls.POSSection.frm_PODlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
            }

            if (strWindowName == "Recv")
            {
                (fcalledform as OfflineRetailV2.UserControls.frm_ReceivingDlg).UpdateGridValueByOnscreenKeyboard(strWindowName, strGridColumnName, intGridRowIndex, txtEditG.Text);
            }

            //GeneralFunctions.SendKeysFullKybd.SendGrid(mykey, ch, blPasswordFocused, element, tbGridInputControl, txtEdit.Text);


        }

        private void BtnA_Click(object sender, RoutedEventArgs e)
        {
            WriteCharacter((sender as Button).Content.ToString(), e);

            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
        }

        private void ChkCapsLock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChkCapsLock_Checked(object sender, RoutedEventArgs e)
        {
            if (chkCapsLock.IsChecked == true)
            {
                chkCapsLock.Foreground = Brushes.LightSkyBlue;
                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
                else
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
            }
            else
            {
                chkCapsLock.Foreground = Brushes.White;
                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
                else
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
            }
        }

        private void ChkShiftL_Checked(object sender, RoutedEventArgs e)
        {
            if (chkShiftL.IsChecked == true)
            {
                chkShiftR.IsChecked = true;
            }
            else
            {
                chkShiftR.IsChecked = false;
            }

            if (chkShiftL.IsChecked == true)
            {
                chkShiftL.Foreground = Brushes.LightSkyBlue;
                chkShiftR.Foreground = Brushes.LightSkyBlue;

            }
            else
            {
                chkShiftL.Foreground = Brushes.White;
                chkShiftR.Foreground = Brushes.White;
            }

            if (chkCapsLock.IsChecked == true)
            {
                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
                else
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
            }
            else
            {

                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
                else
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
            }
        }

        private void ChkShiftR_Checked(object sender, RoutedEventArgs e)
        {
            if (chkShiftR.IsChecked == true)
            {
                chkShiftL.IsChecked = true;
            }
            else
            {
                chkShiftL.IsChecked = false;
            }

            if (chkShiftR.IsChecked == true)
            {
                chkShiftL.Foreground = Brushes.LightSkyBlue;
                chkShiftR.Foreground = Brushes.LightSkyBlue;

            }
            else
            {
                chkShiftL.Foreground = Brushes.White;
                chkShiftR.Foreground = Brushes.White;
            }

            if (chkCapsLock.IsChecked == true)
            {
                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
                else
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
            }
            else
            {

                if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
                {
                    btnA.Content = "A";
                    btnB.Content = "B";
                    btnC.Content = "C";
                    btnD.Content = "D";
                    btnE.Content = "E";
                    btnF.Content = "F";
                    btnG.Content = "G";
                    btnH.Content = "H";
                    btnI.Content = "I";
                    btnJ.Content = "J";

                    btnK.Content = "K";
                    btnL.Content = "L";
                    btnM.Content = "M";
                    btnN.Content = "N";
                    btnO.Content = "O";
                    btnP.Content = "P";
                    btnQ.Content = "Q";
                    btnR.Content = "R";
                    btnS.Content = "S";

                    btnT.Content = "T";
                    btnU.Content = "U";
                    btnV.Content = "V";
                    btnW.Content = "W";
                    btnX.Content = "X";
                    btnY.Content = "Y";
                    btnZ.Content = "Z";
                }
                else
                {
                    btnA.Content = "a";
                    btnB.Content = "b";
                    btnC.Content = "c";
                    btnD.Content = "d";
                    btnE.Content = "e";
                    btnF.Content = "f";
                    btnG.Content = "g";
                    btnH.Content = "h";
                    btnI.Content = "i";
                    btnJ.Content = "j";

                    btnK.Content = "k";
                    btnL.Content = "l";
                    btnM.Content = "m";
                    btnN.Content = "n";
                    btnO.Content = "o";
                    btnP.Content = "p";
                    btnQ.Content = "q";
                    btnR.Content = "r";
                    btnS.Content = "s";

                    btnT.Content = "t";
                    btnU.Content = "u";
                    btnV.Content = "v";
                    btnW.Content = "w";
                    btnX.Content = "x";
                    btnY.Content = "y";
                    btnZ.Content = "z";
                }
            }
        }

        private void BtnBkSp_Click(object sender, RoutedEventArgs e)
        {
            WriteCharacter("Back", e);
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            WriteCharacter("Enter", e);
        }

        private void BtnSp_Click(object sender, RoutedEventArgs e)
        {
            WriteCharacter(" ", e);
        }

        private void Btnchr1_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("~", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("`", e);
            }
        }

        private void Btnchr2_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("_", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("-", e);
            }
        }

        private void Btnchr3_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("+", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("=", e);
            }
        }

        private void Btnchr4_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("{", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("[", e);
            }
        }

        private void Btnchr5_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("}", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("]", e);
            }
        }

        private void Btnchr11_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("|", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("\\", e);
            }
        }

        private void Btnchr6_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter(":", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter(";", e);
            }
        }

        private void Btnchr7_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("\"", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("'", e);
            }
        }

        private void Btnchr8_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("<", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;

            }
            else
            {
                WriteCharacter(",", e);
            }
        }

        private void Btnchr9_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter(">", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter(".", e);
            }
        }

        private void Btnchr10_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("?", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("/", e);
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("!", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("1", e);
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("@", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("2", e);
            }
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("#", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("3", e);
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter(SystemVariables.CurrencySymbol, e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("4", e);
            }
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("%", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("5", e);
            }
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("^", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("6", e);
            }
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("&", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("7", e);
            }
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("*", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("8", e);
            }
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter("(", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("9", e);
            }
        }

        private void Btn10_Click(object sender, RoutedEventArgs e)
        {
            if ((chkShiftL.IsChecked == true) || (chkShiftR.IsChecked == true))
            {
                WriteCharacter(")", e);
                chkShiftL.IsChecked = false;
                chkShiftR.IsChecked = false;
            }
            else
            {
                WriteCharacter("0", e);
            }
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
                if (!bDirectKey)
                {
                    //IInputElement focusedControl = FocusManager.GetFocusedElement(fcalledform);


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
                            Close();
                            return;
                        }
                    }
                    //txtEdit.Text = (focusedControl as TextBox).Text;
                }

                focusedControl.Focus();

                btn4.Content = btn4.Content.ToString().Replace("$", SystemVariables.CurrencySymbol);

                if (focusedControl is TextBox)
                {
                    EditControl = focusedControl as TextBox;
                    PasswordFocused = false;
                }
                if (bCallFromGrid)
                {
                    EditControlG = focusedControl as TextEdit;
                    EditControlG.Focusable = true;
                    PasswordFocused = false;
                }
                if (focusedControl is PasswordBox)
                {
                    EditControlP = focusedControl as PasswordBox;
                    PasswordFocused = true;
                }
            }
            else
            {
                if (fcalledusrctrl == null)
                {
                    Close();
                    return;
                }




                btn4.Content = btn4.Content.ToString().Replace("$", SystemVariables.CurrencySymbol);

                if (txtEditUC is TextBox)
                {
                    EditControl = txtEditUC as TextBox;
                    PasswordFocused = false;
                }
                txtEditUC.Focus();
            }




            this.Topmost = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!boolIsWindow)
            {
                this.Topmost = false;
                this.Topmost = true;
            }
        }
    }
}
