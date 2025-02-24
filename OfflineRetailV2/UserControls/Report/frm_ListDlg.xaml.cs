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
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_ListDlg.xaml
    /// </summary>
    public partial class frm_ListDlg : Window
    {
        public frm_ListDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Miscellaneous Lists";

            DataTable dtblCriteria = new DataTable();
            dtblCriteria.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCriteria.Columns.Add("FilterText", System.Type.GetType("System.String"));


            dtblCriteria.Rows.Add(new object[] { "0", "Item Families" });
            dtblCriteria.Rows.Add(new object[] { "1", "Departments" });
            dtblCriteria.Rows.Add(new object[] { "2", "POS Screen Categories" });
            dtblCriteria.Rows.Add(new object[] { "3", "Taxes" });
            dtblCriteria.Rows.Add(new object[] { "4", "Tender Types" });
            dtblCriteria.Rows.Add(new object[] { "5", "Customer Classes" });
            dtblCriteria.Rows.Add(new object[] { "6", "Customer Groups" });
            dtblCriteria.Rows.Add(new object[] { "7", "Client Parameters" });
            dtblCriteria.Rows.Add(new object[] { "8", "Shifts" });
            dtblCriteria.Rows.Add(new object[] { "9", "Holidays" });
            dtblCriteria.Rows.Add(new object[] { "10", "Discounts" });
            dtblCriteria.Rows.Add(new object[] { "11", "Mix & Matches" });
            dtblCriteria.Rows.Add(new object[] { "12", "Fees & Charges" });
            dtblCriteria.Rows.Add(new object[] { "13", "Sale Prices" });
            dtblCriteria.Rows.Add(new object[] { "14", "Future Prices" });

            cmbList.ItemsSource = dtblCriteria;
            cmbList.EditValue = "0";
            dtblCriteria.Dispose();
        }

        private void ExecuteReport(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                DataTable dtbl = new DataTable();
                PosDataObject.Product objSales = new PosDataObject.Product();
                objSales.Connection = SystemVariables.Conn;
                dtbl = objSales.GetListData(GeneralFunctions.fnInt32(cmbList.EditValue.ToString()), SystemVariables.DateFormat);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                OfflineRetailV2.Report.Misc.repList rep_Sales1 = new OfflineRetailV2.Report.Misc.repList();

                GeneralFunctions.MakeReportWatermark(rep_Sales1);

                rep_Sales1.Report.DataSource = dtbl;

                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rReportCaption.Text = "List of" + " " + cmbList.Text;

                if (cmbList.EditValue == "0")
                {
                    rep_Sales1.rHCode.Text = "Code";
                    rep_Sales1.rHDesc.Text = "Item Family";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "1")
                {
                    rep_Sales1.rHCode.Text = "Code";
                    rep_Sales1.rHDesc.Text = "Department";
                    rep_Sales1.rHDesc2.Text = "Scale?";
                }

                if (cmbList.EditValue == "2")
                {
                    rep_Sales1.rHCode.Text = "Code";
                    rep_Sales1.rHDesc.Text = "POS Screen Category";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "3")
                {
                    rep_Sales1.rHCode.Text = "Tax";
                    rep_Sales1.rHDesc.Text = "Tax Rate";
                    rep_Sales1.rHDesc2.Text = "Active?";
                }

                if (cmbList.EditValue == "4")
                {
                    rep_Sales1.rHCode.Text = "Tender Type";
                    rep_Sales1.rHDesc.Text = "Display As";
                    rep_Sales1.rHDesc2.Text = "Enabled?";
                }

                /*if (cmbList.SelectedIndex == 5)
                {
                    rep_Sales1.rHCode.Text = "Security Profile";
                    rep_Sales1.rHDesc.Text = "";
                    rep_Sales1.rHDesc2.Text = "";
                }

                if (cmbList.SelectedIndex == 6)
                {
                    rep_Sales1.rHCode.Text = "Zip Code";
                    rep_Sales1.rHDesc.Text = "City";
                    rep_Sales1.rHDesc2.Text = "State";
                }*/

                if (cmbList.EditValue == "5")
                {
                    rep_Sales1.rHCode.Text = "Code";
                    rep_Sales1.rHDesc.Text = "Customer Class";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "6")
                {
                    rep_Sales1.rHCode.Text = "Code";
                    rep_Sales1.rHDesc.Text = "Customer Group";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "7")
                {
                    rep_Sales1.rHCode.Text = "Client Parameter";
                    rep_Sales1.rHDesc.Text = "";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc.BorderColor = Color.SlateGray;
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "8")
                {
                    rep_Sales1.rHCode.Text = "Shift";
                    rep_Sales1.rHDesc.Text = "Duration";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "9")
                {
                    rep_Sales1.rHCode.Text = "Holiday Date";
                    rep_Sales1.rHDesc.Text = "Occasion";
                    rep_Sales1.rHDesc2.Text = "";
                    //rep_Sales1.rHDesc2.BorderColor = Color.SlateGray;
                }

                if (cmbList.EditValue == "10")
                {
                    rep_Sales1.rHCode.Text = "Discount Name";
                    rep_Sales1.rHDesc.Text = "Applicable Info.";
                    rep_Sales1.rHDesc2.Text = "Active?";
                }

                if (cmbList.EditValue == "11")
                {
                    rep_Sales1.rHCode.Text = "Mix and Match";
                    rep_Sales1.rHDesc.Text = "Category";
                    rep_Sales1.rHDesc2.Text = "Active?";
                }

                if (cmbList.EditValue == "12")
                {
                    rep_Sales1.rHCode.Text = "Fees";
                    rep_Sales1.rHDesc.Text = "Applicable Info.";
                    rep_Sales1.rHDesc2.Text = "Active?";
                }

                if (cmbList.EditValue == "13")
                {
                    rep_Sales1.rHCode.Text = "Sale Batch";
                    rep_Sales1.rHDesc.Text = "Effective Period";
                    rep_Sales1.rHDesc2.Text = "Active?";
                }

                if (cmbList.EditValue == "14")
                {
                    rep_Sales1.rHCode.Text = "Future Batch";
                    rep_Sales1.rHDesc.Text = "Effective From";
                    rep_Sales1.rHDesc2.Text ="Active?";
                }

                

                rep_Sales1.rCode.DataBindings.Add("Text", dtbl, "Code");
                rep_Sales1.rDesc.DataBindings.Add("Text", dtbl, "Description");
                rep_Sales1.rDesc2.DataBindings.Add("Text", dtbl, "Description2");

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_Sales1.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Sales1;
                        window.ShowDialog();
                        

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
                        if (cmbList.EditValue == "0") attachfile = "list_item_family.pdf";
                        if (cmbList.EditValue == "1") attachfile = "list_departments.pdf";
                        if (cmbList.EditValue == "2") attachfile = "list_categoriess.pdf";
                        if (cmbList.EditValue == "3") attachfile = "list_tax.pdf";
                        if (cmbList.EditValue == "4") attachfile = "list_tender.pdf";
                        if (cmbList.EditValue == "5") attachfile = "list_class.pdf";
                        if (cmbList.EditValue == "6") attachfile = "list_group.pdf";
                        if (cmbList.EditValue == "7") attachfile = "list_crm.pdf";
                        if (cmbList.EditValue == "8") attachfile = "list_shift.pdf";
                        if (cmbList.EditValue == "9") attachfile = "list_holiday.pdf";
                        if (cmbList.EditValue == "10") attachfile = "list_discount.pdf";
                        if (cmbList.EditValue == "11") attachfile = "list_mixmatch.pdf";
                        if (cmbList.EditValue == "12") attachfile = "list_fees.pdf";
                        if (cmbList.EditValue == "13") attachfile = "list_salebatch.pdf";
                        if (cmbList.EditValue == "14") attachfile = "list_futurebatch.pdf";
                        if (cmbList.EditValue == "15") attachfile = "list_scale_cat.pdf";
                        if (cmbList.EditValue == "16") attachfile = "list_scale_addr.pdf";
                        if (cmbList.EditValue == "17") attachfile = "list_scale_label.pdf";
                        if (cmbList.EditValue == "18") attachfile = "list_scale_slabel.pdf";
                        if (cmbList.EditValue == "19") attachfile = "list_scale_grap.pdf";

                        GeneralFunctions.EmailReport(rep_Sales1, attachfile, "List");
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

        private void ClickButton(string EventType)
        {
            if (cmbList.EditValue == null) return;
            ExecuteReport(EventType);
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void CmbList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
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
