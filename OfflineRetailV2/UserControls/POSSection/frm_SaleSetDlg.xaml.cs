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
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;


namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_SaleSetDlg.xaml
    /// </summary>
    public partial class frm_SaleSetDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_SaleSetDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }
        private GridControl grdInTemplate;
        private string strITMFAMILY;
        private int intITMID;
        private string strITMTXT;
        private string strITMSKU;
        private double dblITMPRICE;
        private bool boolControlChanged;

        private bool boolOK;
        private bool boolLoading = false;
        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public string ITMFAMILY
        {
            get { return strITMFAMILY; }
            set { strITMFAMILY = value; }
        }

        public double ITMPRICE
        {
            get { return dblITMPRICE; }
            set { dblITMPRICE = value; }
        }

        public int ITMID
        {
            get { return intITMID; }
            set { intITMID = value; }
        }

        public string ITMSKU
        {
            get { return strITMSKU; }
            set { strITMSKU = value; }
        }

        public string ITMTXT
        {
            get { return strITMTXT; }
            set { strITMTXT = value; }
        }

        public void populatebrand()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupBrand();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbFamily.ItemsSource = dtblTemp;
            cmbFamily.EditValue = null;
        }


        public void populatelkup()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupProduct();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbItem.ItemsSource = dtblTemp;
            cmbItem.EditValue = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Set_Sale_Price;

            label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Hidden;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Hidden;
            label5.Visibility = numSale.Visibility = Visibility.Hidden;

            if (strITMFAMILY == "I")
            {
                rgTypeI.IsChecked = true;
                populatelkup();
            }
            if (strITMFAMILY == "F")
            {
                rgTypeF.IsChecked = true;
                populatebrand();
            }
            if (intITMID > 0)
            {
                label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Visible;
                numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Visible;
                label5.Visibility = numSale.Visibility = Visibility.Visible;

                numSale.Text = dblITMPRICE.ToString("f2");
                if (strITMFAMILY == "I") cmbItem.EditValue = intITMID.ToString();
                if (strITMFAMILY == "F") cmbFamily.EditValue = intITMID.ToString();

                if (rgTypeI.IsChecked == true)
                {
                    lbText.Text = Properties.Resources.Select_Item;
                    cmbItem.Visibility = Visibility.Visible;
                    cmbFamily.Visibility = Visibility.Hidden;

                    PosDataObject.Product objp = new PosDataObject.Product();
                    objp.Connection = SystemVariables.Conn;
                    DataTable dtbl = objp.ShowRecord(GeneralFunctions.fnInt32(cmbItem.EditValue));
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString("f2");
                        numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString("f2");
                        numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString("f2");
                        break;
                    }
                    dtbl.Dispose();

                }
                else
                {
                    lbText.Text = Properties.Resources.Select_Family;
                    cmbItem.Visibility = Visibility.Hidden;
                    cmbFamily.Visibility = Visibility.Visible;

                    PosDataObject.Product objp = new PosDataObject.Product();
                    objp.Connection = SystemVariables.Conn;
                    DataTable dtbl = objp.ShowRecordOfFamily(GeneralFunctions.fnInt32(cmbFamily.EditValue));
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString("f2");
                        numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString("f2");
                        numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString("f2");
                        break;
                    }
                    dtbl.Dispose();
                }
            }



           

            boolLoading = true;
            
        }

        private void RgTypeI_Checked(object sender, RoutedEventArgs e)
        {
            if (!boolLoading) return;
            label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Hidden;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Hidden;
            label5.Visibility = numSale.Visibility = Visibility.Hidden;

            if (rgTypeI.IsChecked == true)
            {
                lbText.Text = Properties.Resources.Select_Item;
                cmbItem.Visibility = Visibility.Visible;
                cmbFamily.Visibility = Visibility.Hidden;

                populatelkup();

            }
            else
            {
                lbText.Text = Properties.Resources.Select_Family;
                cmbItem.Visibility = Visibility.Hidden;
                cmbFamily.Visibility = Visibility.Visible;
                populatebrand();
            }
        }

        private void RgTypeF_Checked(object sender, RoutedEventArgs e)
        {
            label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Hidden;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Hidden;
            label5.Visibility = numSale.Visibility = Visibility.Hidden;

            if (rgTypeI.IsChecked == true)
            {
                lbText.Text = Properties.Resources.Select_Item;
                cmbItem.Visibility = Visibility.Visible;
                cmbFamily.Visibility = Visibility.Hidden;

            }
            else
            {
                lbText.Text = Properties.Resources.Select_Family;
                cmbItem.Visibility = Visibility.Hidden;
                cmbFamily.Visibility = Visibility.Visible;
            }
        }

        private void CmbItem_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            DataTable dtbl = objp.ShowRecord(GeneralFunctions.fnInt32(cmbItem.EditValue));
            foreach (DataRow dr in dtbl.Rows)
            {
                numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString("f2");
                numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString("f2");
                numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString("f2");
                break;
            }
            dtbl.Dispose();

            if (cmbItem.EditValue != null)
            {
                label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Visible;
                numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Visible;
                label5.Visibility = numSale.Visibility = Visibility.Visible;
            }
        }

        private void CmbFamily_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            DataTable dtbl = objp.ShowRecordOfFamily(GeneralFunctions.fnInt32(cmbFamily.EditValue));
            foreach (DataRow dr in dtbl.Rows)
            {
                numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString("f2");
                numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString("f2");
                numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString("f2");
                break;
            }
            dtbl.Dispose();

            if (cmbFamily.EditValue != null)
            {
                label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Visible;
                numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Visible;
                label5.Visibility = numSale.Visibility = Visibility.Visible;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            int iEditValue = 0;
            if (rgTypeI.IsChecked == true)
            {
                if (cmbItem.EditValue != null) iEditValue = GeneralFunctions.fnInt32(cmbItem.EditValue);
                
            }
            if (rgTypeF.IsChecked == true)
            {
                if (cmbFamily.EditValue != null) iEditValue = GeneralFunctions.fnInt32(cmbFamily.EditValue);
            }
            if (iEditValue > 0)
            {
                if (GeneralFunctions.fnDouble(numSale.Text) == 0)
                {
                    if (DocMessage.MsgConfirmation(Properties.Resources.You_have_set_sale_price_to + " 0.00." + "\n" + Properties.Resources.Do_you_want_to_continue_) == MessageBoxResult.No) return;
                }
                intITMID = iEditValue;
                strITMTXT = rgTypeI.IsChecked == true ? cmbItem.Text : cmbFamily.Text;
                strITMFAMILY = rgTypeI.IsChecked == true ? "I" : "F";
                strITMSKU = GetSKUorBrandCode(strITMFAMILY, iEditValue);
                dblITMPRICE = GeneralFunctions.fnDouble(numSale.Text);
                boolOK = true;
                CloseKeyboards();
                Close();
            }
        }

        private string GetSKUorBrandCode(string GetType, int RefID)
        {
            string val = "";
            if (GetType == "F")
            {
                DataTable dtbl = new DataTable();
                PosDataObject.Brand objp = new PosDataObject.Brand();
                objp.Connection = SystemVariables.Conn;
                dtbl = objp.ShowRecord(RefID);
                foreach(DataRow dr in dtbl.Rows)
                {
                    val = dr["BrandID"].ToString();
                    break;
                }
                dtbl.Dispose();
            }

            if (GetType == "I")
            {
                DataTable dtbl = new DataTable();
                PosDataObject.Product objp = new PosDataObject.Product();
                objp.Connection = SystemVariables.Conn;
                val = objp.GetProductSKU(RefID);
            }

            return val;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolOK = false;
            CloseKeyboards();
            Close();
        }

        private void CmbFamily_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;

            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;

            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbItem_PopupOpened(object sender, RoutedEventArgs e)
        {
            /*LookUpEdit edt = (LookUpEdit)sender;
            Dispatcher.BeginInvoke((Action)(() =>
            {
                GridControl t = edt.GetGridControl();
                if (rgTypeI.IsChecked == true)
                {
                    t.Columns[1].Header = "Rajib";
                }
                if (rgTypeF.IsChecked == true)
                {
                    t.Columns[1].Header = "Adi";
                }
            }), System.Windows.Threading.DispatcherPriority.Input);*/
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

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
