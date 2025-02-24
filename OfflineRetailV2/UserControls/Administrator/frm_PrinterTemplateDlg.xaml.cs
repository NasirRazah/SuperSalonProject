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
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_TenderTypesDlg.xaml
    /// </summary>
    public partial class frm_PrinterTemplateDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        private frm_PrinterTemplateBrwUC frmBrowse;

       

        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private string BFlag = "N";

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

        public frm_PrinterTemplateBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_PrinterTemplateBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        public frm_PrinterTemplateDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }


        private void PolulatePrinterType()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dtbl = objSetup.FetchPrinterTypes();
            cmbPrinterType.ItemsSource = dtbl;
        }


        public void PopulateTenderName()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Name");
            dtbl.Rows.Add(new object[] { "Receipt" });
            dtbl.Rows.Add(new object[] { "Layaway" });
            dtbl.Rows.Add(new object[] { "Rent Issue" });
            dtbl.Rows.Add(new object[] { "Return Rent Item" });
            dtbl.Rows.Add(new object[] { "Repair In" });
            dtbl.Rows.Add(new object[] { "Repair Deliver" });
            dtbl.Rows.Add(new object[] { "WorkOrder" });
            dtbl.Rows.Add(new object[] { "Suspend Receipt" });
            dtbl.Rows.Add(new object[] { "Closeout" });
            dtbl.Rows.Add(new object[] { "No Sale" });
            dtbl.Rows.Add(new object[] { "Paid Out" });
            dtbl.Rows.Add(new object[] { "Paid In" });
            dtbl.Rows.Add(new object[] { "Safe Drop" });
            dtbl.Rows.Add(new object[] { "Lotto Payout" });
            dtbl.Rows.Add(new object[] { "Customer Label" });
            

            txtName.ItemsSource = dtbl;
            txtName.DisplayMember = "Name";
            txtName.ValueMember = "Name";
            dtbl.Dispose();
            txtName.EditValue = "Receipt";
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Template Type");
                GeneralFunctions.SetFocus(txtName);
                return false;
            }
            if (txtDisplayAs.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Template Name");
                GeneralFunctions.SetFocus(txtDisplayAs);
                return false;
            }

            if (cmbTemplate.EditValue == null)
            {
                DocMessage.MsgInformation("Select Printer");
                GeneralFunctions.SetFocus(cmbTemplate);
                return false;
            }

            if (cmbPrintCopy.SelectedIndex < 0)
            {
                DocMessage.MsgInformation("Select Number of Copies");
                GeneralFunctions.SetFocus(cmbPrintCopy);
                return false;
            }

            if (DuplicateCount() == 1)
            {
                DocMessage.MsgInformation("Duplicate Printer Template");
                GeneralFunctions.SetFocus(txtName);
                return false;
            }

            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
            objTenderTypes.Connection = SystemVariables.Conn;
            objTenderTypes.LoginUserID = SystemVariables.CurrentUserID;
            objTenderTypes.TemplateType = txtName.Text.Trim();
            objTenderTypes.TemplateName = txtDisplayAs.Text.Trim();
            objTenderTypes.TemplateSize = cmbSize.Text;
            objTenderTypes.PrinterTypeID = GeneralFunctions.fnInt32(cmbPrinterType.EditValue);
            objTenderTypes.AttachedPrinterID = GeneralFunctions.fnInt32(cmbTemplate.EditValue);
            objTenderTypes.PrintCopy = cmbPrintCopy.SelectedIndex + 1;
            objTenderTypes.ID = intID;
            

            if (intID == 0)
            {
                strError = objTenderTypes.InsertData();
                NewID = objTenderTypes.ID;
            }
            else
            {
                strError = objTenderTypes.UpdateData();
            }
            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int GetMaxPaymentOrder()
        {
            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            return objTenderTypes.MaxPaymentOrder();
        }

        public void ShowData()
        {
            PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
            objTenderTypes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objTenderTypes.ShowData(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtName.Text = dr["TemplateType"].ToString();
                txtDisplayAs.Text = dr["TemplateName"].ToString();
                cmbSize.Text = dr["TemplateSize"].ToString();
                cmbPrinterType.EditValue = dr["PrinterTypeID"].ToString();
                cmbTemplate.EditValue = dr["AttachedPrinterID"].ToString();
                cmbPrintCopy.SelectedIndex = GeneralFunctions.fnInt32(dr["PrintCopy"].ToString()) - 1;
            }
            dbtbl.Dispose();
        }

        private int DuplicateCount()
        {
            PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
            objTenderTypes.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objTenderTypes.DuplicateCount(intID, txtName.Text.Trim(),txtDisplayAs.Text.Trim());
        }

        private void TxtName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            txtDisplayAs.Text = txtName.Text + " Template";
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            BrowseForm.FetchData();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PolulatePrinterType();

            fkybrd = new FullKeyboard();
            PopulateTenderName();
            
            if (intID == 0)
            {
                Title.Text = "Add Printer Template";
                txtName.IsEnabled = true;
            }
            else
            {
                Title.Text = "Edit Printer Template";
                txtName.IsEnabled = false;
                ShowData();
            }
            
            boolControlChanged = false;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
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

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkEnabled_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void TxtName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void PolulatePrinters()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dtbl = objSetup.FetchSpecificPrinters(GeneralFunctions.fnInt32(cmbPrinterType.EditValue));
            cmbTemplate.ItemsSource = dtbl;
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void cmbPrinterType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void cmbPrinterType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            PolulatePrinters();
            boolControlChanged = true;
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
