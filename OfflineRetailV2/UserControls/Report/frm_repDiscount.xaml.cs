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
    /// Interaction logic for frm_repDiscount.xaml
    /// </summary>
    public partial class frm_repDiscount : Window
    {
        public frm_repDiscount()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                ExecuteReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }


        private void ExecuteReport(string eventtype)
        {
            if (dtFrom.EditValue == null)
            {
                DocMessage.MsgEnter("From Date");
                GeneralFunctions.SetFocus(dtFrom);
                return;
            }
            if (dtTo.EditValue == null)
            {
                DocMessage.MsgEnter("To Date");
                GeneralFunctions.SetFocus(dtTo);
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }
            DataTable dtbl = new DataTable();

            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            if (rgrp1.IsChecked == true)
                dtbl = objSales.FetchItemDiscountReportData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
            if (rgrp2.IsChecked == true)
                dtbl = objSales.FetchInvoiceDiscountReportData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }


            if (rgrp1.IsChecked == true)
            {
                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("DiscountText", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("TDiscount", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("FilterDate", System.Type.GetType("System.DateTime"));
                repdtbl.Columns.Add("FilterDiscount", System.Type.GetType("System.String"));

                DataTable dtbl1 = new DataTable();

                PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                objSales1.Connection = SystemVariables.Conn;
                dtbl1 = objSales1.FetchItemDiscountReportData1(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));

                int prevInv = 0;
                int CurrInv = 0;

                string TempInv = "";
                string TempInvAmt = "";
                foreach (DataRow dr in dtbl.Rows)
                {
                    CurrInv = GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString());
                    if (CurrInv != prevInv)
                    {
                        TempInv = dr["InvoiceNo"].ToString();
                        TempInvAmt = dr["TDiscount"].ToString();
                    }
                    else
                    {
                        TempInv = "";
                        TempInvAmt = "";
                    }
                    foreach (DataRow dr1 in dtbl1.Rows)
                    {
                        if (dr["FilterDate"].ToString() != dr1["FilterDate"].ToString()) continue;
                        repdtbl.Rows.Add(new object[] {   dr["SKU"].ToString(),
                                                              dr["Description"].ToString(),
                                                              dr["Discount"].ToString(),
                                                              dr["DiscountText"].ToString(),
                                                              TempInv,
                                                              dr["InvoiceDate"].ToString(),
                                                              TempInvAmt,
                                                              dr["FilterDate"].ToString(),
                                                              dr1["Discount"].ToString()});
                    }

                    prevInv = CurrInv;
                }

                OfflineRetailV2.Report.Sales.repItemDiscount rep_Disc = new OfflineRetailV2.Report.Sales.repItemDiscount();

                rep_Disc.Report.DataSource = repdtbl;
                GeneralFunctions.MakeReportWatermark(rep_Disc);
                rep_Disc.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Disc.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Disc.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_Disc.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Disc.rTot.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Disc.rTot.Summary.FormatString = "{0:0.00}";
                }

                rep_Disc.GroupHeader2.GroupFields.Add(rep_Disc.CreateGroupField("FilterDate"));
                rep_Disc.rGroupDate.DataBindings.Add("Text", repdtbl, "FilterDate");
                rep_Disc.rGroupDateAmount.DataBindings.Add("Text", repdtbl, "FilterDiscount");

                rep_Disc.rInvNo.DataBindings.Add("Text", repdtbl, "InvoiceNo");
                rep_Disc.rInvAmt.DataBindings.Add("Text", repdtbl, "TDiscount");

                rep_Disc.rSKU.DataBindings.Add("Text", repdtbl, "SKU");
                rep_Disc.rProduct.DataBindings.Add("Text", repdtbl, "Description");
                rep_Disc.rDiscTxt.DataBindings.Add("Text", repdtbl, "DiscountText");
                rep_Disc.rDiscAmt.DataBindings.Add("Text", repdtbl, "Discount");

                rep_Disc.rTot.DataBindings.Add("Text", repdtbl, "TDiscount");

                GeneralFunctions.MakeReportWatermark(rep_Disc);

                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Disc.PrinterName = Settings.ReportPrinterName;
                        rep_Disc.CreateDocument();
                        rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                        rep_Disc.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_Disc.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Disc;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_Disc.Dispose();

                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Disc.CreateDocument();
                    rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Disc);
                    }
                    finally
                    {
                        rep_Disc.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Disc.CreateDocument();
                    rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "discount_item.pdf";
                        GeneralFunctions.EmailReport(rep_Disc, attachfile, "Item Discount");
                    }
                    finally
                    {
                        rep_Disc.Dispose();
                        dtbl.Dispose();
                    }
                }

            }

            if (rgrp2.IsChecked == true)
            {
                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Discount", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("InvoiceDate", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("TDiscount", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("FilterDate", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("FilterDiscount", System.Type.GetType("System.String"));

                DataTable dtbl1 = new DataTable();

                PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                objSales1.Connection = SystemVariables.Conn;
                dtbl1 = objSales1.FetchInvoiceDiscountReportData1(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));

                int prevInv = 0;
                int CurrInv = 0;

                string TempInv = "";
                string TempInvAmt = "";
                foreach (DataRow dr in dtbl.Rows)
                {
                    CurrInv = GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString());
                    if (CurrInv != prevInv)
                    {
                        TempInv = dr["InvoiceNo"].ToString();
                        TempInvAmt = dr["TDiscount"].ToString();
                    }
                    else
                    {
                        TempInv = "";
                        TempInvAmt = "";
                    }
                    foreach (DataRow dr1 in dtbl1.Rows)
                    {
                        if (dr["FilterDate"].ToString() != dr1["FilterDate"].ToString()) continue;
                        repdtbl.Rows.Add(new object[] {
                                                              dr["Description"].ToString(),
                                                              dr["Discount"].ToString(),
                                                              TempInv,
                                                              dr["InvoiceDate"].ToString(),
                                                              TempInvAmt,
                                                              dr["FilterDate"].ToString(),
                                                              dr1["Discount"].ToString()});
                    }
                }

                OfflineRetailV2.Report.Sales.repInvoiceDiscount rep_Disc = new OfflineRetailV2.Report.Sales.repInvoiceDiscount();
                rep_Disc.Report.DataSource = repdtbl;
                GeneralFunctions.MakeReportWatermark(rep_Disc);
                rep_Disc.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Disc.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Disc.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_Disc.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Disc.rTot.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Disc.rTot.Summary.FormatString = "{0:0.00}";
                }

                rep_Disc.GroupHeader2.GroupFields.Add(rep_Disc.CreateGroupField("FilterDate"));
                rep_Disc.rGroupDate.DataBindings.Add("Text", repdtbl, "FilterDate");
                rep_Disc.rGroupDateAmount.DataBindings.Add("Text", repdtbl, "FilterDiscount");
                rep_Disc.rInvNo.DataBindings.Add("Text", repdtbl, "InvoiceNo");
                rep_Disc.rInvAmt.DataBindings.Add("Text", repdtbl, "TDiscount");

                rep_Disc.rProduct.DataBindings.Add("Text", repdtbl, "Description");
                rep_Disc.rDiscAmt.DataBindings.Add("Text", repdtbl, "Discount");

                rep_Disc.rTot.DataBindings.Add("Text", repdtbl, "TDiscount");

                if (eventtype == "Preview")
                {

                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (Settings.ReportPrinterName != "") rep_Disc.PrinterName = Settings.ReportPrinterName;
                        rep_Disc.CreateDocument();
                        rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                        rep_Disc.PrintingSystem.ShowPrintStatusDialog = false;

                        //rep_Disc.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = rep_Disc;
                        window.ShowDialog();

                    }
                    finally
                    {
                        rep_Disc.Dispose();


                        dtbl.Dispose();
                    }
                }

                if (eventtype == "Print")
                {
                    rep_Disc.CreateDocument();
                    rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        GeneralFunctions.PrintReport(rep_Disc);
                    }
                    finally
                    {
                        rep_Disc.Dispose();
                        dtbl.Dispose();
                    }
                }


                if (eventtype == "Email")
                {
                    rep_Disc.CreateDocument();
                    rep_Disc.PrintingSystem.ShowMarginsWarning = false;
                    try
                    {
                        string attachfile = "";
                        attachfile = "discount_invoice.pdf";
                        GeneralFunctions.EmailReport(rep_Disc, attachfile, "Invoice Discount");
                    }
                    finally
                    {
                        rep_Disc.Dispose();
                        dtbl.Dispose();
                    }
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Discount Report";
            rgrp1.IsChecked = true;
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
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
    }
}
