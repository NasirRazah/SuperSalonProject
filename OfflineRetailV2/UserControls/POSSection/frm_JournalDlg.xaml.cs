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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_JournalDlg.xaml
    /// </summary>
    public partial class frm_JournalDlg : Window
    {
        public frm_JournalDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_JournalBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string pDesc;
        private DataTable Mdtbl = null;
        private string ptype = "";

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public frm_JournalBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_JournalBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public void PopulateProduct(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchLookupData2(intOption);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSKU.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbProduct.ItemsSource = dtblTemp;

            cmbProduct.Text = "";
            dbtblSKU.Dispose();
        }

        private void PopulateTranTypes()
        {
            DataTable dtblCategory = new DataTable();
            dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblCategory.Rows.Add(new object[] { "Stock In", Properties.Resources.Stock_In });
            dtblCategory.Rows.Add(new object[] { "Stock Out", Properties.Resources.Stock_Out });
            cmbtrantype.ItemsSource = dtblCategory;
            cmbtrantype.EditValue = "Stock In";
            dtblCategory.Dispose();
        }

        private void Cmbtrantype_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbtrantype.EditValue.ToString() == "Stock In")
            {
                numCost.IsReadOnly = false;
                cmbAction.ItemsSource = null;
                DataTable dtblCategory = new DataTable();
                dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
                dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
                dtblCategory.Rows.Add(new object[] { "Manual Adjustment", Properties.Resources.Manual_Adjustment });
                dtblCategory.Rows.Add(new object[] { "Transfer", Properties.Resources.Transfer });
                cmbAction.ItemsSource = dtblCategory;
                cmbAction.EditValue = "Manual Adjustment";
                dtblCategory.Dispose();
            }
            else
            {
                numCost.IsReadOnly = true;

                cmbAction.ItemsSource = null;
                DataTable dtblCategory = new DataTable();
                dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
                dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
                dtblCategory.Rows.Add(new object[] { "Manual Adjustment", Properties.Resources.Manual_Adjustment });
                dtblCategory.Rows.Add(new object[] { "Transfer", Properties.Resources.Transfer });
                dtblCategory.Rows.Add(new object[] { "Damage", Properties.Resources.Damage });
                cmbAction.ItemsSource = dtblCategory;
                cmbAction.EditValue = "Manual Adjustment";
                dtblCategory.Dispose();
            }
            boolControlChanged = true;
        }

        private void CmbProduct_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            if (cmbProduct.EditValue == null)
            {
                numQty.IsReadOnly = false;
                lbCost.Visibility = Visibility.Hidden;
                numCost.Visibility = Visibility.Hidden;
                btnMatrix.Visibility = Visibility.Hidden;
                numCost.Text = "0.00";
                numQty.Text = "0.00";
            }
            else
            {
                numQty.IsReadOnly = false;
                lbCost.Visibility = Visibility.Visible;
                numCost.Visibility = Visibility.Visible;
                btnMatrix.Visibility = Visibility.Hidden;
                DataTable dtbl = new DataTable();
                PosDataObject.Product objp = new PosDataObject.Product();
                objp.Connection = SystemVariables.Conn;
                dtbl = objp.ShowRecord(GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString()));
                foreach (DataRow dr in dtbl.Rows)
                {
                    ptype = dr["ProductType"].ToString();
                    if (Settings.DecimalPlace == 2)
                    {
                        if (GeneralFunctions.fnInt32(dr["DecimalPlace"].ToString()) == 2)
                            numCost.Mask = "f2";
                        else
                            numCost.Mask = "f3";
                    }
                    numCost.Text = GeneralFunctions.fnDouble(dr["Cost"].ToString()).ToString();
                    pDesc = dr["Description"].ToString();
                }
                dtbl.Dispose();
                if (ptype == "M")
                {
                    btnMatrix.Visibility = Visibility.Visible;
                    numQty.IsReadOnly = true;
                }

                if (cmbtrantype.EditValue.ToString() == "Stock In")
                {
                    numCost.IsReadOnly = false;
                }
                else
                {
                    numCost.IsReadOnly = true;
                }
            }
        }

        private double GetCost(int pid)
        {
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            return objp.GetProductCost(pid);
        }

        private void BtnMatrix_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_MatrixProduct frmMatrixProduct = new frm_MatrixProduct();
            try
            {
                frmMatrixProduct.CalledFrom = "Stock Journal";
                frmMatrixProduct.FID = GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString());
                frmMatrixProduct.PID = GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString());
                frmMatrixProduct.Title.Text = "Matrix Definition for : " + pDesc;
                frmMatrixProduct.ShowDialog();
                if (frmMatrixProduct.OK == true)
                {
                    numQty.Text = frmMatrixProduct.GetStockTotal().ToString();
                    Mdtbl = frmMatrixProduct.frm_MatrixProductUC.grdQty.ItemsSource as DataTable;
                }
            }
            finally
            {
                frmMatrixProduct.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.New_Journal;
            Mdtbl = new DataTable();
            Mdtbl.Columns.Add("MatrixOptionID", System.Type.GetType("System.String"));
            Mdtbl.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
            Mdtbl.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
            Mdtbl.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
            Mdtbl.Columns.Add("QtyonHand", System.Type.GetType("System.Int32"));
            PopulateTranTypes();
            if (Settings.DecimalPlace == 3) numCost.Mask = "f3";
            else numCost.Mask = "f2";
            dtDocDate.EditValue = DateTime.Today.Date;
            numQty.IsReadOnly = false;
            lbCost.Visibility = Visibility.Hidden;
            numCost.Visibility = Visibility.Hidden;
            btnMatrix.Visibility = Visibility.Hidden;
            PopulateProduct(0);
            GeneralFunctions.SetFocus(txtDoc);
            boolControlChanged = false;
        }

        private bool IsValidAll()
        {
            if (dtDocDate.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Ref_Date);
                GeneralFunctions.SetFocus(dtDocDate);
                return false;
            }

            if (dtDocDate.EditValue != null)
            {
                if (dtDocDate.DateTime.Date > DateTime.Today)
                {
                    DocMessage.MsgInformation(Properties.Resources.Ref_Date_after_today);
                    GeneralFunctions.SetFocus(dtDocDate);
                    return false;
                }
            }

            if (cmbProduct.EditValue == null)
            {
                DocMessage.MsgEnter(Properties.Resources.Item);
                GeneralFunctions.SetFocus(cmbProduct);
                return false;
            }

            if (GeneralFunctions.fnDouble(numQty.Text) == 0)
            {
                DocMessage.MsgEnter(Properties.Resources.Qty);
                GeneralFunctions.SetFocus(numQty);
                return false;
            }

            

            return true;
        }

        private bool SaveData()
        {
            bool flag = false;
            PosDataObject.StockJournal sj = new PosDataObject.StockJournal();
            sj.Connection = SystemVariables.Conn;
            sj.JIsUpdateProduct = "Y";
            sj.JEmpID = SystemVariables.CurrentUserID;
            sj.JTerminalName = Settings.TerminalName;
            sj.JTranType = cmbtrantype.EditValue.ToString();
            sj.JTranSubType = cmbAction.EditValue.ToString();
            sj.JProductID = GeneralFunctions.fnInt32(cmbProduct.EditValue.ToString());
            sj.JQty = GeneralFunctions.fnDouble(numQty.Text);
            sj.JCost = GeneralFunctions.fnDouble(numCost.Text);
            sj.JDocNo = txtDoc.Text.Trim();
            sj.JDocDate = new DateTime(dtDocDate.DateTime.Date.Year, dtDocDate.DateTime.Date.Month,
                                            dtDocDate.DateTime.Date.Day, DateTime.Now.Hour, DateTime.Now.Minute,
                                            DateTime.Now.Second);
            sj.JTranDate = DateTime.Now;
            sj.JNotes = txtNotes.Text.Trim();
            sj.LoginUserID = SystemVariables.CurrentUserID;
            sj.ProductType = ptype;
            sj.ItemDataTable = Mdtbl;
            sj.BeginTransaction();
            if (sj.AddJournal())
            {
                flag = true;
                intNewID = sj.NewID;
            }
            sj.EndTransaction();
            if (flag) return true;
            else return false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.Flag = false;
                    BrowseForm.FDate.EditValue = dtDocDate.DateTime;
                    BrowseForm.TDate.EditValue = dtDocDate.DateTime;

                    BrowseForm.cmbAction.EditValue = cmbtrantype.EditValue.ToString();
                    BrowseForm.cmbItem.EditValue = "0";
                    BrowseForm.Flag = true;
                    BrowseForm.FetchData(dtDocDate.DateTime, dtDocDate.DateTime, cmbtrantype.EditValue.ToString(), 0);
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void TxtDoc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.Flag = false;
                            BrowseForm.FDate.EditValue = dtDocDate.DateTime;
                            BrowseForm.TDate.EditValue = dtDocDate.DateTime;
                            BrowseForm.cmbItem.EditValue = "0";
                            BrowseForm.Flag = true;
                            BrowseForm.FetchData(dtDocDate.DateTime, dtDocDate.DateTime, cmbtrantype.EditValue.ToString(), 0);
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void CmbAction_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;

            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Cmbtrantype_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtDocDate_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
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




        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private int IfExistsSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateCount(SKU);
        }
        private int IfActiveProduct(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.IfActiveProduct(SKU);
        }
        private int IfExistsAltSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateAltSKUCount(SKU);
        }
        /// Check if Alt SKU 2 Exists
        private int IfExistsAltSKU2(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateAltSKU2Count(SKU);
        }
        private string SKUfromAltSKU(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromAltSKU(SKU);
        }
        /// Get SKU from Alt SKU 2
        private string SKUfromAltSKU2(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromAltSKU2(SKU);
        }
        private int IfExistsUPC(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.DuplicateUPCCount(SKU);
        }
        private string SKUfromUPC(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetSKUFromUPC(SKU);
        }
        private int GetProductID(string SKU)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            return objProduct.GetProductID(SKU);
        }

        private void CmbProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (cmbProduct.Text != "")
            {
                if (e.Key == Key.Return)
                {
                    //string sku = GetSKU(cmbProduct.Text);
                    if (IfExistsSKU(cmbProduct.Text) == 1)
                    {
                        int pID = GetProductID(cmbProduct.Text);
                        numQty.IsReadOnly = false;
                        lbCost.Visibility = Visibility.Visible;
                        numCost.Visibility = Visibility.Visible;
                        btnMatrix.Visibility = Visibility.Hidden;
                        DataTable dtbl = new DataTable();
                        PosDataObject.Product objp = new PosDataObject.Product();
                        objp.Connection = SystemVariables.Conn;
                        dtbl = objp.ShowRecord(pID);
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            ptype = dr["ProductType"].ToString();
                            if (Settings.DecimalPlace == 2)
                            {
                                if (GeneralFunctions.fnInt32(dr["DecimalPlace"].ToString()) == 2)
                                    numCost.Mask = "f2";
                                else
                                    numCost.Mask = "f3";
                            }
                            numCost.Text = GeneralFunctions.fnDouble(dr["Cost"].ToString()).ToString();
                            pDesc = dr["Description"].ToString();
                        }
                        dtbl.Dispose();
                        if (ptype == "M")
                        {
                            btnMatrix.Visibility = Visibility.Visible;
                            numQty.IsReadOnly = true;
                        }

                        if (cmbtrantype.EditValue.ToString() == "Stock In")
                        {
                            numCost.IsReadOnly = false;
                        }
                        else
                        {
                            numCost.IsReadOnly = true;
                        }
                    }
                }
            
            }
        }

        private string GetSKU(string skutxt)
        {
            bool bActive = false;
            bool blFindBySKU = false;
            bool blFindByAltSKU = false;
            bool blFindByAltSKU2 = false;
            bool blFindByUPC = false;
            string SKU = "";

            if (IfExistsSKU(skutxt) == 1)
            {
                blFindBySKU = true;
                SKU = skutxt;
                if (IfActiveProduct(SKU) == 0)
                {

                    bActive = false;
                }
                else
                {
                    bActive = true;
                }
            }
            else
            {
                blFindBySKU = false;
            }
            if (!blFindBySKU)
            {
                if (IfExistsAltSKU(skutxt) == 1)
                {
                    blFindByAltSKU = true;
                    SKU = SKUfromAltSKU(skutxt);
                    if (IfActiveProduct(SKU) == 0)
                    {
                        bActive = false;
                    }
                    else
                    {
                        bActive = true;
                    }
                }
                else
                {
                    blFindByAltSKU = false;
                }

                if (!blFindByAltSKU)
                {
                    if (IfExistsAltSKU2(skutxt) == 1)
                    {
                        blFindByAltSKU2 = true;
                        SKU = SKUfromAltSKU2(skutxt);
                        if (IfActiveProduct(SKU) == 0)
                        {
                            bActive = false;
                        }
                        else
                        {
                            bActive = true;
                        }
                    }
                    else
                    {
                        blFindByAltSKU2 = false;
                    }

                    if (!blFindByAltSKU2)
                    {
                        if (IfExistsUPC(skutxt) == 1)
                        {
                            blFindByUPC = true;
                            SKU = SKUfromUPC(skutxt);
                            if (IfActiveProduct(SKU) == 0)
                            {
                                bActive = false;
                            }
                            else
                            {
                                bActive = true;
                            }
                        }
                        else
                        {
                            blFindByUPC = false;
                        }
                    }
                }
               

            }

            if (bActive)
            {
                return SKU;
            }
            else
            {
                return "";
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
