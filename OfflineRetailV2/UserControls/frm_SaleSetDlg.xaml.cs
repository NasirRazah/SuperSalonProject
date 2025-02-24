using System;
using System.Collections.Generic;
using System.Data;
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
using DevExpress.XtraEditors;
using OfflineRetailV2.Data;
using pos;

namespace OfflineRetailV2.UserControls
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

            ModalWindow.CloseCommand = new CommandBase(OnClose);

            Loaded += Frm_SaleSetDlg_Loaded;
        }
        List<System.Windows.Forms.Control> Controls = new List<System.Windows.Forms.Control>();
        private void Frm_SaleSetDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Set_Sale_Price;
            if (strITMFAMILY == "") populatelkup();
            else
            {
                if (strITMFAMILY == "I")
                    rbItem.IsChecked = true;
                else
                    rbFamily.IsChecked = true;


                if (strITMFAMILY == "I") populatelkup();
                if (strITMFAMILY == "F") populatebrand();
            }
            if (intITMID > 0)
            {
                LookUpEdit plkup = new LookUpEdit();
                foreach (System.Windows.Forms.Control c in this.Controls)
                {
                    if (c is LookUpEdit)
                    {
                        if (strITMFAMILY == "I") if (c.Name == "cmbItem") plkup = (LookUpEdit)c;
                        if (strITMFAMILY == "F") if (c.Name == "cmbFamily") plkup = (LookUpEdit)c;
                        break;
                    }
                }
                plkup.EditValue = intITMID.ToString();
                numSale.Text = dblITMPRICE.ToString();
            }
        }

        private void OnClose(object obj)
        {
            CloseKeyboards();
            DialogResult = false;
            Close();
        }
        private string strITMFAMILY;
        private int intITMID;
        private string strITMTXT;
        private string strITMSKU;
        private double dblITMPRICE;
        private bool boolControlChanged;

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

            SortAndSearchLookUpEdit sortAndSearchLookUpEditP = new SortAndSearchLookUpEdit();
            sortAndSearchLookUpEditP.Bounds = new System.Drawing.Rectangle(120, 60, 250, 20);
            sortAndSearchLookUpEditP.Properties.DataSource = dbtblCat;
            sortAndSearchLookUpEditP.Name = "cmbFamily";
            sortAndSearchLookUpEditP.Font = new System.Drawing.Font("Tahoma", 8);
            sortAndSearchLookUpEditP.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 8);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col0 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col0);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col1);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col2);

            sortAndSearchLookUpEditP.Properties.Columns[0].Caption = Properties.Resources.ID;
            sortAndSearchLookUpEditP.Properties.Columns[0].FieldName = "ID";
            sortAndSearchLookUpEditP.Properties.Columns[0].Visible = false;

            sortAndSearchLookUpEditP.Properties.Columns[1].Caption = Properties.Resources.Description;
            sortAndSearchLookUpEditP.Properties.Columns[1].FieldName = "BrandDescription";
            sortAndSearchLookUpEditP.Properties.Columns[1].Visible = true;
            sortAndSearchLookUpEditP.Properties.Columns[2].Caption = Properties.Resources.Code;
            sortAndSearchLookUpEditP.Properties.Columns[2].FieldName = "BrandID";
            sortAndSearchLookUpEditP.Properties.Columns[2].Visible = true;

            sortAndSearchLookUpEditP.Properties.DisplayMember = "BrandDescription";
            sortAndSearchLookUpEditP.Properties.ValueMember = "ID";
            sortAndSearchLookUpEditP.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            sortAndSearchLookUpEditP.Properties.PopupWidth = 400;
            sortAndSearchLookUpEditP.EditValue = 0;
            sortAndSearchLookUpEditP.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbItem_KeyDown);
            sortAndSearchLookUpEditP.EditValueChanged += new EventHandler(LookupBrand_EditValueChanged);
            this.Controls.Add(sortAndSearchLookUpEditP);

            dbtblCat.Dispose();
        }


        public void populatelkup()
        {
            PosDataObject.Discounts objCategory = new PosDataObject.Discounts();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.LookupProduct();

            SortAndSearchLookUpEdit sortAndSearchLookUpEditP = new SortAndSearchLookUpEdit();
            sortAndSearchLookUpEditP.Bounds = new System.Drawing.Rectangle(120, 60, 250, 20);
            sortAndSearchLookUpEditP.Properties.DataSource = dbtblCat;
            sortAndSearchLookUpEditP.Name = "cmbItem";
            sortAndSearchLookUpEditP.Font = new System.Drawing.Font("Tahoma", 8);
            sortAndSearchLookUpEditP.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 8);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col0 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col0);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col1);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            sortAndSearchLookUpEditP.Properties.Columns.Add(col2);

            sortAndSearchLookUpEditP.Properties.Columns[0].Caption = Properties.Resources.ID;
            sortAndSearchLookUpEditP.Properties.Columns[0].FieldName = "ID";
            sortAndSearchLookUpEditP.Properties.Columns[0].Visible = false;

            sortAndSearchLookUpEditP.Properties.Columns[1].Caption = Properties.Resources.Description;
            sortAndSearchLookUpEditP.Properties.Columns[1].FieldName = "Description";
            sortAndSearchLookUpEditP.Properties.Columns[1].Visible = true;
            sortAndSearchLookUpEditP.Properties.Columns[2].Caption = Properties.Resources.SKU;
            sortAndSearchLookUpEditP.Properties.Columns[2].FieldName = "SKU";
            sortAndSearchLookUpEditP.Properties.Columns[2].Visible = true;

            sortAndSearchLookUpEditP.Properties.DisplayMember = "Description";
            sortAndSearchLookUpEditP.Properties.ValueMember = "ID";
            sortAndSearchLookUpEditP.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            sortAndSearchLookUpEditP.Properties.AutoSearchColumnIndex = 2;
            sortAndSearchLookUpEditP.Properties.PopupWidth = 400;
            sortAndSearchLookUpEditP.EditValue = 0;
            sortAndSearchLookUpEditP.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbItem_KeyDown);
            sortAndSearchLookUpEditP.EditValueChanged += new EventHandler(LookupEdit_EditValueChanged);
            sortAndSearchLookUpEditP.Enter += new EventHandler(cmbItem_Enter);
            this.Controls.Add(sortAndSearchLookUpEditP);

            dbtblCat.Dispose();

            sortAndSearchLookUpEditP.Focus();
        }
        private void LookupBrand_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit plkup = new LookUpEdit();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is LookUpEdit)
                {
                    if (c.Name == "cmbFamily")
                    {
                        plkup = (LookUpEdit)c;
                        break;
                    }
                }
            }

            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            DataTable dtbl = objp.ShowRecordOfFamily(GeneralFunctions.fnInt32(plkup.EditValue.ToString()));
            foreach (DataRow dr in dtbl.Rows)
            {
                numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();
                break;
            }
            dtbl.Dispose();

            label1.Visibility = label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Visible;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Visible;
            label5.Visibility = numSale.Visibility = Visibility.Visible;
        }

        private void LookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit plkup = new LookUpEdit();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is LookUpEdit)
                {
                    if (c.Name == "cmbItem")
                    {
                        plkup = (LookUpEdit)c;
                        break;
                    }
                }
            }

            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            DataTable dtbl = objp.ShowRecord(GeneralFunctions.fnInt32(plkup.EditValue.ToString()));
            foreach (DataRow dr in dtbl.Rows)
            {
                numA.Text = GeneralFunctions.fnDouble(dr["PriceA"].ToString()).ToString();
                numB.Text = GeneralFunctions.fnDouble(dr["PriceB"].ToString()).ToString();
                numC.Text = GeneralFunctions.fnDouble(dr["PriceC"].ToString()).ToString();
            }
            dtbl.Dispose();

            label1.Visibility = label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Visible;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Visible;
            label5.Visibility = numSale.Visibility = Visibility.Visible;
        }
        private void cmbItem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void cmbItem_Enter(object sender, EventArgs e)
        {
            LookUpEdit edit = (LookUpEdit)sender;
            if (edit.EditValue.ToString() == "0")
            {
                if (!edit.IsPopupOpen)
                {
                    edit.ShowPopup();
                    edit.Focus();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            DialogResult = false;
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LookUpEdit plkup = new LookUpEdit();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is LookUpEdit)
                {

                    if (rbItem.IsChecked == true) if (c.Name == "cmbItem") plkup = (LookUpEdit)c;
                    if (rbFamily.IsChecked == true) if (c.Name == "cmbFamily") plkup = (LookUpEdit)c;
                    break;
                }
            }

            if (plkup.EditValue.ToString() != "")
            {
                if (double.Parse(numSale.Text) == 0)
                {
                    if (DocMessage.MsgConfirmation(Properties.Resources.You_have_set_sale_price_to + " 0.00." + "\n" + Properties.Resources.Do_you_want_to_continue_) == MessageBoxResult.No) return;
                }
                intITMID = GeneralFunctions.fnInt32(plkup.EditValue.ToString());
                strITMTXT = plkup.Text;
                strITMFAMILY = rbItem.IsChecked == true ? "I" : "F";
                GeneralFunctions.GetOtherColumnValuesOfLookup(plkup, strITMFAMILY == "I" ? "SKU" : "BrandID", ref strITMSKU);
                dblITMPRICE = double.Parse(numSale.Text);
                CloseKeyboards();
                DialogResult = true;
                Close();
            }
        }

        private void rbItem_Checked(object sender, RoutedEventArgs e)
        {
            label1.Visibility = label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Collapsed;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Collapsed;
            label5.Visibility = numSale.Visibility = Visibility.Collapsed;

            lbText.Text = Properties.Resources.Select_Item;

            LookUpEdit plkupF = new LookUpEdit();
            LookUpEdit plkupM = new LookUpEdit();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is LookUpEdit)
                {
                    if (c.Name == "cmbFamily")
                    {
                        plkupF = (LookUpEdit)c;
                        plkupF.Dispose();
                    }
                }
            }

            populatelkup();

        }

        private void rbFamily_Checked(object sender, RoutedEventArgs e)
        {
            label1.Visibility = label2.Visibility = label3.Visibility = label4.Visibility = Visibility.Collapsed;
            numA.Visibility = numB.Visibility = numC.Visibility = Visibility.Collapsed;
            label5.Visibility = numSale.Visibility = Visibility.Collapsed;

            lbText.Text = Properties.Resources.Select_Family;

            LookUpEdit plkupF = new LookUpEdit();
            LookUpEdit plkupM = new LookUpEdit();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is LookUpEdit)
                {
                    if (c.Name == "cmbItem")
                    {
                        plkupM = (LookUpEdit)c;
                        plkupM.Visible = false;
                        plkupM.Dispose();
                    }
                }
            }

            populatebrand();
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
    }
}
