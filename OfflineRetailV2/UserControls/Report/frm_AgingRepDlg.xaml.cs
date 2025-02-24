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
    /// Interaction logic for frm_AgingRepDlg.xaml
    /// </summary>
    public partial class frm_AgingRepDlg : Window
    {
        public frm_AgingRepDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void Rg1_Checked(object sender, RoutedEventArgs e)
        {
            if (rg1.IsChecked == true)
            {
                label1.Text = "Aging (by Days)";
                dtAging.Visibility = Visibility.Collapsed;
                cmbAging.Visibility = Visibility.Visible;
            }
            else
            {
                label1.Text = "Aging (by Date)";
                cmbAging.Visibility = Visibility.Collapsed;
                dtAging.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Inventory Aging Report";
            rg1.IsChecked = true;
            cmbAging.SelectedIndex = 0;
            dtAging.EditValue = DateTime.Today.AddDays(-90);
        }

        private void GetReportData(string eventtype)
        {

            PosDataObject.StockJournal objStockJournal = new PosDataObject.StockJournal();
            objStockJournal.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            if (rg1.IsChecked == true) dbtbl = objStockJournal.FetchAgingData(GeneralFunctions.fnInt32(cmbAging.Text));
            if (rg2.IsChecked == true) dbtbl = objStockJournal.FetchAgingData(dtAging.DateTime.Date);

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

            OfflineRetailV2.Report.Product.repAging rep = new OfflineRetailV2.Report.Product.repAging();
            dbtbl = RearrangeData(dbtbl);
            if (rg1.IsChecked == true)
            {
                rep.rRange.Text = "Aging >" + " " + cmbAging.Text + " days";
                if (GeneralFunctions.fnInt32(cmbAging.Text) > 0) rep.rRange.Text = rep.rRange.Text + "s";
            }
            if (rg2.IsChecked == true)
            {
                rep.rRange.Text = "Aging from" + " " + dtAging.DateTime.Date.ToString("d");
            }
            GeneralFunctions.MakeReportWatermark(rep);
            rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep.rReportHeader.Text = Settings.ReportHeader_Address;
            rep.Report.DataSource = dbtbl;
            rep.rSKU.DataBindings.Add("Text", dbtbl, "SKU");
            rep.rDesc.DataBindings.Add("Text", dbtbl, "Description");
            rep.rAge.DataBindings.Add("Text", dbtbl, "Aging");
            rep.rQty.DataBindings.Add("Text", dbtbl, "QtyOnHand");

            if (eventtype == "Preview")
            {
                // frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep.PrinterName = Settings.ReportPrinterName;
                    rep.CreateDocument();
                    rep.PrintingSystem.ShowMarginsWarning = false;
                    rep.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep;
                    window.ShowDialog();

                    //rep.ShowPreviewDialog();
                    
                }
                finally
                {
                    rep.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dbtbl.Dispose();

                }
            }

            if (eventtype == "Print")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep);
                }
                finally
                {
                    rep.Dispose();
                    dbtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "inv_aging.pdf";
                    GeneralFunctions.EmailReport(rep, attachfile,"Aging");
                }
                finally
                {
                    rep.Dispose();
                    dbtbl.Dispose();
                }
            }


        }

        private DataTable RearrangeData(DataTable dtbl)
        {
            DataTable dtbL = dtbl.Clone();

            DataTable dtb2 = dtbl.DefaultView.ToTable(true, "SKU");
            foreach (DataRow dr1 in dtb2.Rows)
            {
                string sku = "";
                foreach (DataRow dr in dtbl.Rows)
                {
                    object recvdt = Convert.DBNull;
                    int ag = 0;
                    double qty = 0;
                    recvdt = GeneralFunctions.fnDate(dr["LastReceived"].ToString());
                    ag = GeneralFunctions.fnInt32(dr["Aging"].ToString());
                    qty = GeneralFunctions.fnInt32(dr["QtyOnHand"].ToString());

                    if (dr1["SKU"].ToString() == dr["SKU"].ToString())
                    {
                        DataTable d = new DataTable();
                        d = dtbL;
                        bool f = false;
                        foreach (DataRow dr2 in dtbL.Rows)
                        {
                            if (dr["SKU"].ToString() == dr2["SKU"].ToString())
                            {
                                f = true;
                                break;
                            }
                        }
                        if (!f)
                        {
                            dtbL.Rows.Add(new object[] { dr["SKU"].ToString(), dr["Description"].ToString(), recvdt, ag, qty });
                        }
                        d.Dispose();
                    }
                }
            }
            dtb2.Dispose();
            return dtbL;
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (rg1.IsChecked == true)
                {
                    if (GeneralFunctions.fnInt32(cmbAging.Text) == 0) return;
                }
                if (rg2.IsChecked == true)
                {
                    if (dtAging.EditValue == null) return;
                    if (dtAging.DateTime.Date >= DateTime.Today.Date)
                    {
                        DocMessage.MsgInformation("Enter Valid Aging Date");
                        GeneralFunctions.SetFocus(dtAging);
                        return;
                    }
                }
                GetReportData(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
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

        private void CmbAging_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtAging_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
