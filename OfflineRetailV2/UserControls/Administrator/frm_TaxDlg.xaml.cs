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
using DevExpress.Xpf.Editors;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_TaxDlg.xaml
    /// </summary>
    public partial class frm_TaxDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private frm_TaxBrwUC frmBrowse;
        private int intID;
        private int intNewID;
        private int intPrevRow;
        private int intTimes;
        private bool boolControlChanged;
        private string prevtx = "";

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

        public frm_TaxBrwUC BrowseForm
        {
            get
            {

                if (frmBrowse == null)
                    frmBrowse = new frm_TaxBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_TaxDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateGL()
        {
            PosDataObject.GLedger objEmployee = new PosDataObject.GLedger();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objEmployee.LookupGL("");

            cmbGL.ItemsSource = dbtbl;
            cmbGL.DisplayMember = "Lookup";
            cmbGL.ValueMember = "ID";
            dbtbl.Dispose();
            cmbGL.EditValue = null;
        }

        private bool ValidAllFields()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Tax_Name);
                txtName.Focus();
                return false;
            }

            if ((intID == 0) || ((intID > 0) && (prevtx != txtName.Text.Trim())))
            {
                PosDataObject.Tax objtx = new PosDataObject.Tax();
                objtx.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int vl = objtx.IsExistsTaxName(txtName.Text.Trim());
                if (vl > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Tax_Name_already_exists);
                    txtName.Focus();
                    return false;
                }
            }

            if (intID > 0)
            {
                if (chkActive.IsChecked == true)
                {
                    if (!IsExecute())
                    {
                        DocMessage.MsgInformation(SystemVariables.BrandName + " " + Properties.Resources.support_only_3_Active_Taxes + " " + Properties.Resources.You_can_not_defined_more_active_taxes_than_that_limit);
                        return false;
                    }
                }
            }

            if (GeneralFunctions.fnDouble(txtRate.Text) == 0)
            {
                DocMessage.MsgEnter("Tax Rate");
                txtRate.Focus();
                return false;
            }

            if (Settings.CloseoutExport == "Y")
            {
                if (cmbGL.EditValue == null)
                {
                    DocMessage.MsgInformation("Select G/L Account");
                    GeneralFunctions.SetFocus(cmbGL);
                    return false;
                }
            }

            return true;
        }

        private bool IsExecute()
        {
            PosDataObject.Tax objd = new PosDataObject.Tax();
            objd.Connection = SystemVariables.Conn;
            if (objd.GetNoOfActiveTaxes(intID) >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ShowTaxHaederData()
        {
            PosDataObject.Tax objTax = new PosDataObject.Tax();
            objTax.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objTax.ShowHeaderRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtName.Text = dr["TaxName"].ToString();
                txtRate.Text = GeneralFunctions.fnDouble(dr["TaxRate"].ToString()).ToString("f2");
                if (GeneralFunctions.fnInt32(dr["TaxType"].ToString()) == 0)
                {
                    rbP.IsChecked = true;
                }
                else
                {
                    rbT.IsChecked = true;
                }
                
                if (dr["Active"].ToString() == "Yes")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }

                if (Settings.CloseoutExport == "Y")
                {
                    if (GeneralFunctions.fnInt32(dr["LinkGL"].ToString()) > 0)
                    {
                        cmbGL.EditValue = dr["LinkGL"].ToString();
                    }
                }
            }
            prevtx = txtName.Text;
            dbtbl.Dispose();
        }

        private void SetDecimalPlace()
        {
            txtRate.Mask = Settings.DecimalPlace == 2 ? "N2": "N3";
            if (Settings.DecimalPlace == 3)
            {
                PART_Editor_B.DisplayFormat = "f3";
                PART_Editor_T.DisplayFormat = "f3";
            }
            else
            {
                PART_Editor_B.DisplayFormat = "f";
                PART_Editor_T.DisplayFormat = "f";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();
            SetDecimalPlace();

            if (Settings.CloseoutExport == "Y")
            {
                cmbGL.Visibility = lbGL.Visibility = Visibility.Visible;
                PopulateGL();
            }
            else
            {
                cmbGL.Visibility = lbGL.Visibility = Visibility.Collapsed;
            }

            grdTax.Visibility = Visibility.Collapsed;
            if (intID == 0)
            {
                Title.Text = "Add Tax";
                rbP.IsChecked = true;
                chkActive.IsChecked = true;
                grdTax.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title.Text = "Edit Tax";
                ShowTaxHaederData();

                if (rbT.IsChecked == true)
                {
                    grdTax.Visibility = Visibility.Visible;
                }
                else
                {
                    grdTax.Visibility = Visibility.Collapsed;
                }
            }
            PopulateBatchGrid();
            boolControlChanged = false;
        }

        private void PopulateBatchGrid()
        {
            try
            {
                PosDataObject.Tax objTax = new PosDataObject.Tax();
                objTax.Connection = SystemVariables.Conn;
                grdTax.ItemsSource = objTax.ShowDetailRecord(intID);
                FillRowinGrid(((grdTax.ItemsSource) as DataTable).Rows.Count);
                gridView1.MoveFirstRow();
                grdTax.CurrentColumn = colBreakPoints;
            }
            finally
            {
                
            }
        }

        private void FillRowinGrid(int FilledupRow)
        {
            try
            {
                int intRowsToFill = SystemVariables.FIXEDGRIDROWS - FilledupRow;
                if (intRowsToFill <= 0) return;
                DataTable dtbl = new DataTable();
                dtbl = (grdTax.ItemsSource) as DataTable;
                for (int intCount = 0; intCount <= intRowsToFill; intCount++)
                {
                    dtbl.Rows.Add(new object[] { null, null });
                }

                grdTax.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message.Trim();
                DocMessage.MsgInformation(ErrMsg);
            }
        }

        private void RbP_Checked(object sender, RoutedEventArgs e)
        {
            if (rbT.IsChecked == true)
            {
                grdTax.Visibility = Visibility.Visible;
            }
            else
            {
                grdTax.Visibility = Visibility.Collapsed;
            }
        }

        private bool SaveData()
        {
            if (ValidAllFields())
            {

                string ErrMsg = "";

                gridView1.PostEditor();

                PosDataObject.Tax objTax = new PosDataObject.Tax();
                objTax.Connection = SystemVariables.Conn;

                objTax.TaxName = txtName.Text.Trim();
                objTax.TaxType = rbP.IsChecked == true? 0 : 1;
                objTax.TaxRate = GeneralFunctions.fnDouble(txtRate.Text.Trim());
                objTax.SplitDataTable = grdTax.ItemsSource as DataTable;
                objTax.LoginUserID = SystemVariables.CurrentUserID;
                objTax.ErrorMsg = "";
                objTax.ID = intID;
                if (chkActive.IsChecked == true)
                {
                    objTax.Active = "Yes";
                }
                else
                {
                    objTax.Active = "No";
                }

                if (Settings.CloseoutExport == "Y")
                {
                    objTax.LinkGL = GeneralFunctions.fnInt32(cmbGL.EditValue);
                }
                else
                {
                    objTax.LinkGL = 0;
                }

                if (intID == 0)
                {
                    objTax.Mode = "Add";
                }
                else
                {
                    objTax.Mode = "Edit";
                }

                //B e g i n   T r a n s a ct i o n
                objTax.BeginTransaction();
                if (objTax.InsertTax())
                {
                    intNewID = objTax.ID;
                }
                objTax.EndTransaction();
                //E n d  T r a n s a ct i o n

                ErrMsg = objTax.ErrorMsg;
                if (ErrMsg != "")
                {
                    DocMessage.ShowException("Saving Tax Data", ErrMsg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (SaveData())
                    {
                        boolControlChanged = false;
                        BrowseForm.FetchData();
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (SaveData())
            {
                boolControlChanged = false;
                BrowseForm.FetchData();
                CloseKeyboards();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colBreakPoints")
            {
                boolControlChanged =  true;
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                if (e.Value.ToString() == "")
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    /*if (gridView1.focu == intRowNum)
                    {
                        intRowNum = gridView1.RowCount - 1;
                        gridView1.AddNewRow();
                        gridView1.FocusedRowHandle = intRowNum;
                    }*/

                    double dblTax = 0.01 * (intRowNum + 1);
                    grdTax.SetCellValue(intRowNum, colBreakPoints, GeneralFunctions.fnDouble(e.Value));
                    grdTax.SetCellValue(intRowNum, colTax, dblTax);
                }
            }
        }

        private void GrdTax_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            int visibleIndex = grdTax.GetRowVisibleIndexByHandle(gridView1.FocusedRowHandle);
            if (e.Key == Key.Enter && visibleIndex == (grdTax.VisibleRowCount - 1))
            {
                gridView1.AddNewRow();
                int intRowNum = (grdTax.ItemsSource as DataTable).Rows.Count - 1;
                gridView1.FocusedRowHandle = intRowNum;
                grdTax.CurrentColumn = colBreakPoints;
            }
            if (e.Key == Key.Tab && visibleIndex == (grdTax.VisibleRowCount - 1) && grdTax.CurrentColumn.VisibleIndex == (gridView1.VisibleColumns.Count - 1))
            {
                gridView1.AddNewRow();
                int intRowNum = (grdTax.ItemsSource as DataTable).Rows.Count - 1;
                gridView1.FocusedRowHandle = intRowNum;
                grdTax.CurrentColumn = colBreakPoints;
            }
        }

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbGL_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
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

        private void GridView1_ShownEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e)
        {
            if (e.Column.FieldName == "BreakPoints")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                int dp = 2;
                if (editor.Text == "")
                {
                    if (dp == 2) editor.Text = "0.00";
                    
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Tax";
                    nkybrd.GridColumnName = "BreakPoints";
                    nkybrd.GridDecimal = dp;
                    nkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0)); nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
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
        }


        public void UpdateGridValueByOnscreenKeyboard(string strgridname, string gridcol, int rindx, string val)
        {
            if (gridcol == "BreakPoints") grdTax.SetCellValue(rindx, colBreakPoints, val);
            

        }
    }
}
