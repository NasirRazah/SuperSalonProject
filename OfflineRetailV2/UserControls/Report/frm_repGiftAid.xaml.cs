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
    public partial class frm_repGiftAid : Window
    {
        public frm_repGiftAid()
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
            

            dtbl = objSales.FetchGiftAidReportGroupData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }


            DataTable repdtbl = new DataTable();
            repdtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Name", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Address", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Amount", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("FilterDate", System.Type.GetType("System.DateTime"));
            repdtbl.Columns.Add("Total", System.Type.GetType("System.String"));

            DataTable dtbl1 = new DataTable();

            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = SystemVariables.Conn;
            dtbl1 = objSales1.FetchGiftAidReportDetailData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));

            
            foreach (DataRow dr in dtbl.Rows)
            {
               
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    if (dr["FilterDate"].ToString() != dr1["FilterDate"].ToString()) continue;
                    repdtbl.Rows.Add(new object[] {   dr1["RefNo"].ToString(),
                                                              dr1["Name"].ToString(),
                                                              dr1["Address"].ToString(),
                                                              dr1["Amount"].ToString(),
                                                              dr["FilterDate"].ToString(),
                                                              dr["Total"].ToString()});
                }

                
            }

            OfflineRetailV2.Report.Sales.repGiftAid rep_Disc = new OfflineRetailV2.Report.Sales.repGiftAid();

            rep_Disc.Report.DataSource = repdtbl;
            GeneralFunctions.MakeReportWatermark(rep_Disc);
            rep_Disc.rHeader.Text = rgrp1.IsChecked == true ? "Gift Aid (Detail)" : "Gift Aid (Summary)";
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
            rep_Disc.rGroupDateAmount.DataBindings.Add("Text", repdtbl, "Total");

            rep_Disc.rInvNo.DataBindings.Add("Text", repdtbl, "RefNo");
            rep_Disc.rName.DataBindings.Add("Text", repdtbl, "Name");
            rep_Disc.rAddress.DataBindings.Add("Text", repdtbl, "Address");
            rep_Disc.rAmount.DataBindings.Add("Text", repdtbl, "Amount");

            rep_Disc.rGTot.DataBindings.Add("Text", repdtbl, "Amount");
            rep_Disc.rTot.DataBindings.Add("Text", repdtbl, "Amount");

            if (rgrp1.IsChecked == true)
            {
                rep_Disc.xrTableCell6.Visible = false;
                rep_Disc.rGroupDateAmount.Visible = false;
                    
            }

                if (rgrp2.IsChecked == true)
            {
                rep_Disc.PageHeader.Visible = rep_Disc.Detail.Visible = rep_Disc.GroupFooter2.Visible = false;
            }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Gift Aid Report";
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
