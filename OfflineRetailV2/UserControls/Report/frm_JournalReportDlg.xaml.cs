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

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_JournalReportDlg.xaml
    /// </summary>
    public partial class frm_JournalReportDlg : Window
    {
        public frm_JournalReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void GetReportData(string eventtype)
        {
            PosDataObject.StockJournal objStockJournal = new PosDataObject.StockJournal();
            objStockJournal.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objStockJournal.FetchDataForReport(dtFrom.DateTime, dtTo.DateTime, cmbtrantype.EditValue.ToString(), cmbAction.Text, SystemVariables.DateFormat);

            foreach(DataRow dr in dbtbl.Rows)
            {
                if (dr["TranSubType"].ToString()== "Receiving")
                {
                    DataTable dtb = objStockJournal.FetchSupplierDataForReport(
                        GeneralFunctions.fnInt32(dr["DocNo"].ToString()),
                        GeneralFunctions.fnInt32(dr["ProductID"].ToString()),
                        GeneralFunctions.fnDouble(dr["Qty"].ToString()));

                    foreach (DataRow dr1 in dtb.Rows)
                    {
                        dr["Vendor"] = dr1["Vendor"].ToString();
                            dr["VendorPart"] = dr1["Part"].ToString();
                    }
                }

                if (dr["TranSubType"].ToString() == "Sale")
                {
                    double val = objStockJournal.FetchSaleDataForReport(
                        GeneralFunctions.fnInt32(dr["DocNo"].ToString()),
                        GeneralFunctions.fnInt32(dr["ProductID"].ToString()),
                        -GeneralFunctions.fnDouble(dr["Qty"].ToString()),Settings.TaxInclusive,false);

                    dr["SalePrice"] = val.ToString();
                }

                if (dr["TranSubType"].ToString() == "Return")
                {
                    double val = objStockJournal.FetchSaleDataForReport(
                        GeneralFunctions.fnInt32(dr["DocNo"].ToString()),
                        GeneralFunctions.fnInt32(dr["ProductID"].ToString()),
                        -GeneralFunctions.fnDouble(dr["Qty"].ToString()), Settings.TaxInclusive, true);

                    dr["SalePrice"] = val.ToString();
                }
            }

            if (dbtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Product.repInvJournal rep_InvJournal = new OfflineRetailV2.Report.Product.repInvJournal();

            rep_InvJournal.rRange.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_InvJournal);
            rep_InvJournal.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_InvJournal.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_InvJournal.DecimalPlace = Settings.DecimalPlace;
            rep_InvJournal.Report.DataSource = dbtbl;
            rep_InvJournal.GroupHeader2.GroupFields.Add(rep_InvJournal.CreateGroupField("SKU"));
            rep_InvJournal.GroupHeader1.GroupFields.Add(rep_InvJournal.CreateGroupField("ID"));
            rep_InvJournal.rSKU.DataBindings.Add("Text", dbtbl, "SKU");
            rep_InvJournal.rProduct.DataBindings.Add("Text", dbtbl, "ProductName");
            rep_InvJournal.rN.DataBindings.Add("Text", dbtbl, "DocNo");
            rep_InvJournal.rD.DataBindings.Add("Text", dbtbl, "DocDate");
            rep_InvJournal.rS.DataBindings.Add("Text", dbtbl, "TranType");
            rep_InvJournal.rT.DataBindings.Add("Text", dbtbl, "TranSubType");
            rep_InvJournal.rQ.DataBindings.Add("Text", dbtbl, "Qty");
            rep_InvJournal.rHQ.DataBindings.Add("Text", dbtbl, "StockOnHand");
            rep_InvJournal.rC.DataBindings.Add("Text", dbtbl, "Cost");
            rep_InvJournal.rDC.DataBindings.Add("Text", dbtbl, "DicountedCost");
            rep_InvJournal.rDP.DataBindings.Add("Text", dbtbl, "DP");
            rep_InvJournal.rNotes.DataBindings.Add("Text", dbtbl, "Notes");
            rep_InvJournal.rAlt.DataBindings.Add("Text", dbtbl, "AltSKU");
            rep_InvJournal.rVendor.DataBindings.Add("Text", dbtbl, "Vendor");
            rep_InvJournal.rPart.DataBindings.Add("Text", dbtbl, "VendorPart");
            rep_InvJournal.rSale.DataBindings.Add("Text", dbtbl, "SalePrice");
            rep_InvJournal.rCat.DataBindings.Add("Text", dbtbl, "Category");

            if (chkNotes.IsChecked == false)
            {
                rep_InvJournal.Notes = "N";
            }
            else
            {
                rep_InvJournal.Notes = "Y";
            }

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_InvJournal.PrinterName = Settings.ReportPrinterName;
                    rep_InvJournal.CreateDocument();
                    rep_InvJournal.PrintingSystem.ShowMarginsWarning = false;
                    rep_InvJournal.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_InvJournal.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_InvJournal;
                    window.ShowDialog();
                }
                finally
                {
                    rep_InvJournal.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dbtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_InvJournal.CreateDocument();
                rep_InvJournal.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_InvJournal);
                }
                finally
                {
                    rep_InvJournal.Dispose();
                    dbtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_InvJournal.CreateDocument();
                rep_InvJournal.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "journal.pdf";
                    GeneralFunctions.EmailReport(rep_InvJournal, attachfile, "Stock Journal");
                }
                finally
                {
                    rep_InvJournal.Dispose();
                    dbtbl.Dispose();
                }
            }

        }

        private bool CheckBlankDate()
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }
            return true;
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (CheckBlankDate())
                {
                    GetReportData(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void PopulateTranTypes()
        {
            DataTable dtblCategory = new DataTable();
            dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblCategory.Rows.Add(new object[] { "Stock In", "Stock In" });
            dtblCategory.Rows.Add(new object[] { "Stock Out", "Stock Out" });
            dtblCategory.Rows.Add(new object[] { "ALL", "All" });
            cmbtrantype.ItemsSource = dtblCategory;

            cmbtrantype.EditValue = "ALL";
            dtblCategory.Dispose();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Inventory Journal Report";
            PopulateTranTypes();
            dtFrom.EditValue = DateTime.Today.Date;
            dtTo.EditValue = DateTime.Today.Date;
            cmbAction.Visibility = Visibility.Collapsed;
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

        private void Cmbtrantype_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbtrantype.EditValue.ToString() == "ALL")
            {
                cmbAction.Visibility = Visibility.Collapsed;
            }
            if (cmbtrantype.EditValue.ToString() == "Stock In")
            {
                cmbAction.Visibility = Visibility.Visible;
                cmbAction.ItemsSource = null;
                DataTable dtblCategory = new DataTable();
                dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
                dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));

                dtblCategory.Rows.Add(new object[] { "Adjustment-WO","Adjustment-WO" });
                dtblCategory.Rows.Add(new object[] { "Inventory Adjustment", "Inventory Adjustment" });
                dtblCategory.Rows.Add(new object[] { "Manual Adjustment", "Manual Adjustment" });
                dtblCategory.Rows.Add(new object[] { "Opening Stock", "Opening Stock" });
                dtblCategory.Rows.Add(new object[] { "Receiving", "Receiving" });
                dtblCategory.Rows.Add(new object[] { "Return", "Return" });
                dtblCategory.Rows.Add(new object[] { "Transfer", "Transfer" });
                dtblCategory.Rows.Add(new object[] { "Production", "Production" });
                dtblCategory.Rows.Add(new object[] { "ALL", "All" });
                cmbAction.ItemsSource = dtblCategory;
                cmbAction.EditValue = "ALL";
                dtblCategory.Dispose();
            }
            if (cmbtrantype.EditValue.ToString() == "Stock Out")
            {
                cmbAction.Visibility = Visibility.Visible;
                cmbAction.ItemsSource = null;
                DataTable dtblCategory = new DataTable();
                dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
                dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));

                dtblCategory.Rows.Add(new object[] { "Adjustment-Recv","Adjustment-Recv" });
                dtblCategory.Rows.Add(new object[] { "Damage","Damage" });
                dtblCategory.Rows.Add(new object[] { "Manual Adjustment","Manual Adjustment" });
                dtblCategory.Rows.Add(new object[] { "Inventory Adjustment", "Inventory Adjustment" });
                dtblCategory.Rows.Add(new object[] { "Sale", "Receiving" });
                dtblCategory.Rows.Add(new object[] { "Transfer", "Transfer" });
                dtblCategory.Rows.Add(new object[] { "Work Order", "Return" });
                dtblCategory.Rows.Add(new object[] { "Adjustment-Production","Production" });
                dtblCategory.Rows.Add(new object[] { "ALL", "All" });
                cmbAction.ItemsSource = dtblCategory;
                cmbAction.EditValue = "ALL";
                dtblCategory.Dispose();
            }

        }

        private void Cmbtrantype_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
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

        private void CmbAction_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
