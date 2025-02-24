using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSProductPriceDlg.xaml
    /// </summary>
    public partial class frm_POSProductPriceDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSProductPriceDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSProductPriceDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
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

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void Frm_POSProductPriceDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            blchange = false;
            if (StrFormType == "View")
            {
                
                numPriceA.IsEnabled = false;
                numPriceB.IsEnabled = false;
                numPriceC.IsEnabled = false;
                btnUpdate.Visibility = Visibility.Collapsed;
            }

            if (StrFormType == "Change")
            {

                numPriceA.IsEnabled = true;
                numPriceB.IsEnabled = true;
                numPriceC.IsEnabled = true;
                btnUpdate.Visibility = Visibility.Visible;
            }

            SetDecimalPlace();
            ShowData();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private int intID;
        private string StrFormType;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blchange;

        public bool ChangeFlag
        {
            get { return blchange; }
            set { blchange = value; }
        }

        public string FormType
        {
            get { return StrFormType; }
            set { StrFormType = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3)
            {
                GeneralFunctions.SetDecimal( numPriceA, 3);
                GeneralFunctions.SetDecimal(numPriceB, 3);
                GeneralFunctions.SetDecimal(numPriceC, 3);
            }
            else
            {
                GeneralFunctions.SetDecimal(numPriceA,2);
                GeneralFunctions.SetDecimal(numPriceB,2);
                GeneralFunctions.SetDecimal(numPriceC,2);
            }
        }

        private void ShowData()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Product objProd = new PosDataObject.Product();
            objProd.Connection = SystemVariables.Conn;
            dtbl = objProd.ShowRecord(intID);
            foreach (DataRow dr in dtbl.Rows)
            {
                numPriceA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numPriceB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numPriceC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (blchange)
            {
                PosDataObject.Product objProd = new PosDataObject.Product();
                objProd.Connection = SystemVariables.Conn;
                objProd.ID = intID;
                objProd.PriceA = Convert.ToDouble(numPriceA.Text);
                objProd.PriceB = Convert.ToDouble(numPriceB.Text);
                objProd.PriceC = Convert.ToDouble(numPriceC.Text);
                objProd.LoginUserID = SystemVariables.CurrentUserID;
                objProd.ChangedByAdmin = intSuperUserID;
                objProd.FunctionButtonAccess = blFunctionBtnAccess;
                string strErr = objProd.UpdateProductPrice();
                if (strErr == "")
                {
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    Close();
                }
            }
            else
            {
                DialogResult = true;
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
        }

        private void numPriceA_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            blchange = true;
        }
    }
}
