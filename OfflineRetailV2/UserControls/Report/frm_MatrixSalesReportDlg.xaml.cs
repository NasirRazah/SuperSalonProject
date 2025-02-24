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
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using System.Collections.ObjectModel;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_MatrixSalesReportDlg.xaml
    /// </summary>
    public partial class frm_MatrixSalesReportDlg : Window
    {
        public frm_MatrixSalesReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        
        public List<Object> SelectedValues { get; set; }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void PopulateProduct(int intOption)
        {

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchMatrixLookupData1(intOption);

            cmbProduct.ItemsSource = dbtblSKU;

            dbtblSKU.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Matrix Item Sales Report";
            chkoption1.Visibility = chkoption2.Visibility = chkoption3.Visibility = Visibility.Collapsed;
            chkoption1.IsChecked = chkoption2.IsChecked = chkoption3.IsChecked = false;
            cmboption1.Visibility = cmboption2.Visibility = cmboption3.Visibility = Visibility.Collapsed;
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            PopulateProduct(0);
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void CmbProduct_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (GeneralFunctions.fnInt32(cmbProduct.EditValue) == 0) return;

            chkoption1.Visibility = chkoption2.Visibility = chkoption3.Visibility = Visibility.Collapsed;
            chkoption1.IsChecked = chkoption2.IsChecked = chkoption3.IsChecked = false;
            cmboption1.Visibility = cmboption2.Visibility = cmboption3.Visibility = Visibility.Collapsed;

            if (GeneralFunctions.fnInt32(cmbProduct.EditValue) == 999999) return;

            DataTable dtblO = new DataTable();
            PosDataObject.Product objprod = new PosDataObject.Product();
            objprod.Connection = SystemVariables.Conn;
            dtblO = objprod.FetchMatrixOptionData(GeneralFunctions.fnInt32(cmbProduct.EditValue));
            foreach (DataRow dr in dtblO.Rows)
            {
                if (dr["Option1Name"].ToString().Trim() != "")
                {
                    chkoption1.Content = " " + dr["Option1Name"].ToString().Trim();
                    chkoption1.Tag = dr["ID"].ToString();
                    chkoption1.Visibility = Visibility.Visible;
                }
                else
                {
                    DataTable d1 = new DataTable();
                    PosDataObject.Product objprod11 = new PosDataObject.Product();
                    objprod11.Connection = SystemVariables.Conn;
                    d1 = objprod11.FetchMatrixValueData(GeneralFunctions.fnInt32(dr["ID"].ToString()), 1);

                    if (d1.Rows.Count > 0)
                    {
                        chkoption1.Content = " " + dr["Option1Name"].ToString().Trim();
                        chkoption1.Tag = dr["ID"].ToString();
                        chkoption1.Visibility = Visibility.Visible;
                    }
                }

                if (dr["Option2Name"].ToString().Trim() != "")
                {
                    chkoption2.Content = " " + dr["Option2Name"].ToString().Trim();
                    chkoption2.Tag = dr["ID"].ToString();
                    chkoption2.Visibility = Visibility.Visible;
                }
                else
                {
                    DataTable d1 = new DataTable();
                    PosDataObject.Product objprod12 = new PosDataObject.Product();
                    objprod12.Connection = SystemVariables.Conn;
                    d1 = objprod12.FetchMatrixValueData(GeneralFunctions.fnInt32(dr["ID"].ToString()), 2);

                    if (d1.Rows.Count > 0)
                    {
                        chkoption2.Content = "";
                        chkoption2.Tag = dr["ID"].ToString();
                        chkoption2.Visibility = Visibility.Visible;
                    }
                }


                if (dr["Option3Name"].ToString().Trim() != "")
                {
                    chkoption3.Content = " " + dr["Option3Name"].ToString().Trim();
                    chkoption3.Tag = dr["ID"].ToString();
                    chkoption3.Visibility = Visibility.Visible;
                }
                else
                {
                    DataTable d3 = new DataTable();
                    PosDataObject.Product objprod13 = new PosDataObject.Product();
                    objprod13.Connection = SystemVariables.Conn;
                    d3 = objprod13.FetchMatrixValueData(GeneralFunctions.fnInt32(dr["ID"].ToString()), 3);

                    if (d3.Rows.Count > 0)
                    {
                        chkoption3.Content = "";
                        chkoption3.Tag = dr["ID"].ToString();
                        chkoption3.Visibility = Visibility.Visible;
                    }
                }

            }
            dtblO.Dispose();
        }

        private void Chkoption1_Checked(object sender, RoutedEventArgs e)
        {
            cmboption1.Items.Clear();
            if (chkoption1.IsChecked == true)
            {
                DataTable dtblV = new DataTable();
                PosDataObject.Product objprod = new PosDataObject.Product();
                objprod.Connection = SystemVariables.Conn;
                dtblV = objprod.FetchMatrixValueData(GeneralFunctions.fnInt32(chkoption1.Tag), 1);
                cmboption1.ItemsSource = dtblV;
                cmboption1.DisplayMember = "OptionValue";
                cmboption1.ValueMember = "OptionValue";

                cmboption1.Visibility = dtblV.Rows.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

                dtblV.Dispose();
            }
            else
            {
                cmboption1.Visibility = Visibility.Collapsed;
            }
        }

        private void Chkoption2_Checked(object sender, RoutedEventArgs e)
        {
            cmboption2.Items.Clear();
            if (chkoption2.IsChecked == true)
            {
                DataTable dtblV = new DataTable();
                PosDataObject.Product objprod = new PosDataObject.Product();
                objprod.Connection = SystemVariables.Conn;
                dtblV = objprod.FetchMatrixValueData(GeneralFunctions.fnInt32(chkoption2.Tag), 2);
                cmboption2.ItemsSource = dtblV;
                cmboption2.DisplayMember = "OptionValue";
                cmboption2.ValueMember = "OptionValue";

                cmboption2.Visibility = dtblV.Rows.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

                dtblV.Dispose();
            }
            else
            {
                cmboption2.Visibility = Visibility.Collapsed;
            }
        }

        private void Chkoption3_Checked(object sender, RoutedEventArgs e)
        {
            cmboption3.Items.Clear();
            if (chkoption3.IsChecked == true)
            {
                DataTable dtblV = new DataTable();
                PosDataObject.Product objprod = new PosDataObject.Product();
                objprod.Connection = SystemVariables.Conn;
                dtblV = objprod.FetchMatrixValueData(GeneralFunctions.fnInt32(chkoption3.Tag), 3);
                cmboption3.ItemsSource = dtblV;
                cmboption3.DisplayMember = "OptionValue";
                cmboption3.ValueMember = "OptionValue";

                cmboption3.Visibility = dtblV.Rows.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

                dtblV.Dispose();
            }
            else
            {
                cmboption3.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (ValidAll())
                {
                    if (GeneralFunctions.fnInt32(cmbProduct.EditValue) != 999999) ExecutePrintProcess(eventtype);
                    else ExecutePrintProcess1(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private bool ValidAll()
        {
            if (GeneralFunctions.fnInt32(cmbProduct.EditValue) == 0)
            {
                DocMessage.MsgInformation("Select an Item");
                GeneralFunctions.SetFocus(cmbProduct);
                return false;
            }

            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }

            if (GeneralFunctions.fnInt32(cmbProduct.EditValue) != 999999)
            {

                int cnt1 = 0;
                int cnt2 = 0;
                int cnt3 = 0;

                if (chkoption1.IsChecked == true)
                {

                    foreach (DevExpress.Xpf.Editors.ComboBoxEditItem item1 in cmboption1.Items)
                    {
                        if (item1.IsSelected == true)
                        {
                            cnt1++;
                        }
                    }
                }

                if (chkoption2.IsChecked == true)
                {
                    foreach (DevExpress.Xpf.Editors.ComboBoxEditItem item2 in cmboption2.Items)
                    {
                        cnt2++;
                        if (item2.IsSelected == true)
                        {
                            cnt2++;
                        }
                    }
                }

                if (chkoption3.IsChecked == true)
                {
                    foreach (DevExpress.Xpf.Editors.ComboBoxEditItem item3 in cmboption3.Items)
                    {
                        cnt3++;
                        if (item3.IsSelected == true)
                        {
                            cnt3++;
                        }
                    }
                }

                if ((cnt1 == 0) && (cnt2 == 0) && (cnt3 == 0))
                {
                    DocMessage.MsgInformation("No Criteria Selected");
                    return false;
                }
            }

            return true;
        }

        private void CmbProduct_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Cmboption1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void ExecutePrintProcess(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                int cnt1 = 0;
                int cnt2 = 0;
                int cnt3 = 0;
                string repoption1 = "";
                string repoption2 = "";
                string repoption3 = "";

                string op1 = "";
                string op2 = "";
                string op3 = "";
                int pid = 0;
                int moid = 0;
                DataTable dtblo1 = new DataTable();
                dtblo1.Columns.Add("value", System.Type.GetType("System.String"));

                DataTable dtblo2 = new DataTable();
                dtblo2.Columns.Add("value", System.Type.GetType("System.String"));

                DataTable dtblo3 = new DataTable();
                dtblo3.Columns.Add("value", System.Type.GetType("System.String"));

                if (chkoption1.IsChecked == true)
                {
                    moid = GeneralFunctions.fnInt32(chkoption1.Tag);
                    op1 = chkoption1.Content.ToString().Trim();
                    
                     
                    object obj = cmboption1.SelectedItems;

                    /*foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item1 in cmboption1.Properties.Items)
                    {
                        cnt1++;
                        if (item1.CheckState == CheckState.Checked)
                        {
                            repoption1 = repoption1 == "" ? op1 + " : " + item1.Value.ToString() : repoption1 + ", " + item1.Value.ToString();
                            dtblo1.Rows.Add(new object[] { item1.Value.ToString() });
                        }
                    }*/
                }

                if (chkoption2.IsChecked == true)
                {
                    moid = GeneralFunctions.fnInt32(chkoption2.Tag);
                    op2 = chkoption2.Content.ToString().Trim();

                    /*foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item2 in cmboption2.Properties.Items)
                    {
                        cnt2++;
                        if (item2.CheckState == CheckState.Checked)
                        {
                            repoption2 = repoption2 == "" ? op2 + " : " + item2.Value.ToString() : repoption2 + ", " + item2.Value.ToString();
                            dtblo2.Rows.Add(new object[] { item2.Value.ToString() });
                        }
                    }*/
                }

                if (chkoption3.IsChecked == true)
                {
                    moid = GeneralFunctions.fnInt32(chkoption2.Tag);
                    op3 = chkoption3.Content.ToString().Trim();

                    /*
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item3 in cmboption3.Properties.Items)
                    {
                        cnt3++;
                        if (item3.CheckState == CheckState.Checked)
                        {
                            repoption3 = repoption3 == "" ? op3 + " : " + item3.Value.ToString() : repoption3 + ", " + item3.Value.ToString();
                            dtblo3.Rows.Add(new object[] { item3.Value.ToString() });
                        }
                    }*/
                }

                pid = GeneralFunctions.fnInt32(cmbProduct.EditValue);

                DataTable dtbl = new DataTable();
                PosDataObject.Sales os1 = new PosDataObject.Sales();
                os1.Connection = SystemVariables.Conn;
                dtbl = os1.FetchMatrixItemSaleReportData(pid, moid, op1, op2, op3, dtblo1, dtblo2, dtblo3, GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.TaxInclusive);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No record found in the selected date range");
                    dtbl.Dispose();
                    return;
                }


                PosDataObject.Product objprod = new PosDataObject.Product();
                objprod.Connection = SystemVariables.Conn;
                string psku = objprod.GetProductSKU(pid);

                OfflineRetailV2.Report.Sales.repMatrixSales rep_Sales1 = new OfflineRetailV2.Report.Sales.repMatrixSales();
                rep_Sales1.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rDate.Text = "from" + " " + dtFrom.Text + " " + "to" + " " + dtTo.Text;
                rep_Sales1.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Sales1.rTTot1.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.000}";
                    //rep_Sales1.rTTot5.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Sales1.rTTot1.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.00}";
                    //rep_Sales1.rTTot5.Summary.FormatString = "{0:0.00}";
                }

                rep_Sales1.rHeader.Text = cmbProduct.Text + " (" + psku + ")";

                string tstr = "";

                if (op1 != "")
                {
                    if (dtblo1.Rows.Count == cnt1) repoption1 = op1 + ": All";
                    tstr = repoption1;
                }
                if (op2 != "")
                {
                    if (dtblo2.Rows.Count == cnt2) repoption2 = op2 + ": All";
                    tstr = tstr == "" ? repoption2 : tstr + "\r\n" + repoption2;
                }

                if (op3 != "")
                {
                    if (dtblo3.Rows.Count == cnt3) repoption3 = op3 + ": All";
                    tstr = tstr == "" ? repoption3 : tstr + "\r\n" + repoption3;
                }
                rep_Sales1.rOption.Text = tstr;

                rep_Sales1.rHOP1.Text = chkoption1.Visibility == Visibility.Visible ? chkoption1.Content.ToString() : "";
                rep_Sales1.rHOP2.Text = chkoption2.Visibility == Visibility.Visible ? chkoption2.Content.ToString() : "";
                rep_Sales1.rHOP3.Text = chkoption3.Visibility == Visibility.Visible ? chkoption3.Content.ToString() : "";



                rep_Sales1.rOP1.DataBindings.Add("Text", dtbl, "MatrixOption1");
                rep_Sales1.rOP2.DataBindings.Add("Text", dtbl, "MatrixOption2");
                rep_Sales1.rOP3.DataBindings.Add("Text", dtbl, "MatrixOption3");

                rep_Sales1.rQty.DataBindings.Add("Text", dtbl, "Qty");
                rep_Sales1.rCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_Sales1.rDCost.DataBindings.Add("Text", dtbl, "DiscountedCost");
                rep_Sales1.rRevenue.DataBindings.Add("Text", dtbl, "Revenue");
                rep_Sales1.rProfit.DataBindings.Add("Text", dtbl, "Profit");
                rep_Sales1.rMargin.DataBindings.Add("Text", dtbl, "Margin");

                rep_Sales1.rTTot1.DataBindings.Add("Text", dtbl, "Qty");
                rep_Sales1.rTTot2.DataBindings.Add("Text", dtbl, "Cost");
                rep_Sales1.rTTot6.DataBindings.Add("Text", dtbl, "DiscountedCost");
                rep_Sales1.rTTot3.DataBindings.Add("Text", dtbl, "Revenue");
                rep_Sales1.rTTot4.DataBindings.Add("Text", dtbl, "Profit");
                rep_Sales1.rTTot5.DataBindings.Add("Text", dtbl, "Margin");


                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;
                        rep_Sales1.ShowPreviewDialog();

                    }
                    finally
                    {
                        rep_Sales1.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Sales1);
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "sales_matrix.pdf";
                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Matrix Item Sales");
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        dtbl.Dispose();
                    }
                }


            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void ExecutePrintProcess1(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }

                DataTable dtbl = new DataTable();
                PosDataObject.Sales os1 = new PosDataObject.Sales();
                os1.Connection = SystemVariables.Conn;
                dtbl = os1.FetchAllMatrixItemSaleReportData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), SystemVariables.DateFormat, Settings.TaxInclusive);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No record found in the selected date range");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Sales.repMatrixAllSales rep_Sales1 = new OfflineRetailV2.Report.Sales.repMatrixAllSales();
                rep_Sales1.Report.DataSource = dtbl;
                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rDate.Text = "from" + " " + dtFrom.Text + " " + "to" + " " + dtTo.Text;
                rep_Sales1.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Sales1.rTTot1.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.000}";
                    //rep_Sales1.rTTot5.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Sales1.rTTot1.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.00}";
                    //rep_Sales1.rTTot5.Summary.FormatString = "{0:0.00}";
                }

                string tstr = "";



                rep_Sales1.rOP1.DataBindings.Add("Text", dtbl, "Date");
                rep_Sales1.rOP2.DataBindings.Add("Text", dtbl, "SKU");
                rep_Sales1.rOP3.DataBindings.Add("Text", dtbl, "ProductName");
                rep_Sales1.rOP4.DataBindings.Add("Text", dtbl, "ProductDetails");

                rep_Sales1.rQty.DataBindings.Add("Text", dtbl, "Qty");
                rep_Sales1.rCost.DataBindings.Add("Text", dtbl, "Cost");
                rep_Sales1.rDCost.DataBindings.Add("Text", dtbl, "DiscountedCost");
                rep_Sales1.rRevenue.DataBindings.Add("Text", dtbl, "Revenue");
                rep_Sales1.rProfit.DataBindings.Add("Text", dtbl, "Profit");
                rep_Sales1.rMargin.DataBindings.Add("Text", dtbl, "Margin");

                rep_Sales1.rTTot1.DataBindings.Add("Text", dtbl, "Qty");
                rep_Sales1.rTTot2.DataBindings.Add("Text", dtbl, "Cost");
                rep_Sales1.rTTot3.DataBindings.Add("Text", dtbl, "Revenue");
                rep_Sales1.rTTot4.DataBindings.Add("Text", dtbl, "Profit");
                rep_Sales1.rTTot5.DataBindings.Add("Text", dtbl, "Margin");
                rep_Sales1.rTTot6.DataBindings.Add("Text", dtbl, "DiscountedCost");


                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;
                        rep_Sales1.ShowPreviewDialog();

                    }
                    finally
                    {
                        rep_Sales1.Dispose();

                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Sales1);
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Sales1.CreateDocument();
                    rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "sales_matrix_all.pdf";
                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Matrix All Items Sales");
                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        dtbl.Dispose();
                    }
                }


            }
            finally
            {
                Cursor = Cursors.Arrow;
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
