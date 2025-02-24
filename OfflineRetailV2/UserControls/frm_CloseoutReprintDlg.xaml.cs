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
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_CloseoutReprintDlg.xaml
    /// </summary>
    public partial class frm_CloseoutReprintDlg : Window
    {
        private string strCalledFor;
        public string CalledFor
        {
            get { return strCalledFor; }
            set { strCalledFor = value; }
        }
        private bool boolOK;
        public frm_CloseoutReprintDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void FetchRecord()
        {
            PosDataObject.Closeout objco = new PosDataObject.Closeout();
            objco.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objco.ShowRecord(strCalledFor,SystemVariables.DateFormat);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdCloseout.ItemsSource = dtbl;
            dtbl.Dispose();
            dtblTemp.Dispose();
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnPrint.Visibility = Visibility.Hidden;
            btnEmail.Visibility = Visibility.Hidden;
            chkReceiptPrn.IsChecked = (Settings.DefaultCloseoutPrinter == "Receipt Printer");
            if (strCalledFor == "Reprint")
            {
                Title.Text = Properties.Resources.Reprint_Close_Out;
                lbData.Text = Properties.Resources.Select_the_Close_Out_Batch_for_reprint;

            }
            if (strCalledFor == "Report")
            {
                Title.Text = Properties.Resources.CLOSE_OUT_REPORT;
                lbData.Text = Properties.Resources.Select_the_Close_Out_Batch_for_which_you_would_like_a_report;

                btnPrint.Visibility = Visibility.Visible;
                btnEmail.Visibility = Visibility.Visible;
            }
            FetchRecord();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtNotes_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void BtnUp1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdCloseout.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle > 0))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            }
                
        }

        private void BtnDown1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdCloseout.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle != (grdCloseout.ItemsSource as DataTable).Rows.Count - 1))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            }
        }

        private void BtnCancel_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                await ExecutePreview();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                await ExecutePreview();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async Task PrepareForEmailPrint(string eventtype)
        {
            int RowID = 0;
            RowID = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            int intCloseoutID = 0;
            string strCloseoutType = "";
            intCloseoutID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colID));
            strCloseoutType = await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colType);
            if (await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colED) == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Actual_closeout_has_not_been_performed + "\n" + Properties.Resources.This_is_a_preliminary_closeout_report);
            }

            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive,"N");


            OfflineRetailV2.Report.Closeout.repCloseOut rep_CloseOut = new OfflineRetailV2.Report.Closeout.repCloseOut();
            OfflineRetailV2.Report.Closeout.repCloseoutMain rep_CloseoutMain = new OfflineRetailV2.Report.Closeout.repCloseoutMain();
            OfflineRetailV2.Report.Closeout.repCloseoutMain1 rep_CloseoutMain1 = new OfflineRetailV2.Report.Closeout.repCloseoutMain1();
            OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx rep_CloseoutMain1_tx = new OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx();
            OfflineRetailV2.Report.Closeout.repCloseoutMain2 rep_CloseoutMain2 = new OfflineRetailV2.Report.Closeout.repCloseoutMain2();
            OfflineRetailV2.Report.Closeout.repTender rep_Tender = new OfflineRetailV2.Report.Closeout.repTender();
            OfflineRetailV2.Report.Closeout.repTenderCount rep_TenderCount = new OfflineRetailV2.Report.Closeout.repTenderCount();
            OfflineRetailV2.Report.Closeout.repTenderOverShort rep_TenderOverShort = new OfflineRetailV2.Report.Closeout.repTenderOverShort();
            OfflineRetailV2.Report.Closeout.repReturn rep_Return = new OfflineRetailV2.Report.Closeout.repReturn();
            OfflineRetailV2.Report.Closeout.repCloseoutAdditional rep_CloseoutAdditional = new OfflineRetailV2.Report.Closeout.repCloseoutAdditional();
            OfflineRetailV2.Report.Closeout.repSalesByDept rep_SalesByDept = new OfflineRetailV2.Report.Closeout.repSalesByDept();
            OfflineRetailV2.Report.Closeout.repSalesByHour rep_SalesByHour = new OfflineRetailV2.Report.Closeout.repSalesByHour();

            OfflineRetailV2.Report.Closeout.repCloseoutTender rep_CloseoutTender = new OfflineRetailV2.Report.Closeout.repCloseoutTender();
            OfflineRetailV2.Report.Closeout.repCloseoutRent rep_CloseoutRent = new OfflineRetailV2.Report.Closeout.repCloseoutRent();
            OfflineRetailV2.Report.Closeout.repCloseoutRepair rep_CloseoutRepair = new OfflineRetailV2.Report.Closeout.repCloseoutRepair();

            DataTable dtbl1 = new DataTable("Tender");
            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl1 = objCloseout.ShowTenderRecord("T", intCloseoutID, Settings.TerminalName);

            DataTable dtbl2 = new DataTable("TenderC");
            PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
            objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl2 = objCloseout1.ShowTenderRecord("C", intCloseoutID, Settings.TerminalName);

            DataTable dtbl3 = new DataTable("TenderOverShort");
            PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
            objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl3 = objCloseout2.ShowTenderRecord("R", intCloseoutID, Settings.TerminalName);

            DataTable dtbl4 = new DataTable("Header");
            PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
            objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl4 = objCloseout3.ShowHeaderRecord(Settings.TerminalName, SystemVariables.DateFormat);

            int rnt = 0;
            int rpr = 0;
            double rprsecurity = 0;
            foreach (DataRow d in dtbl4.Rows)
            {
                rnt = GeneralFunctions.fnInt32(d["RentInvoiceCount"].ToString());
                rpr = GeneralFunctions.fnInt32(d["RepairInvoiceCount"].ToString());
                rprsecurity = GeneralFunctions.fnDouble(d["RepairDeposit"].ToString());
            }

            DataTable dtbl7 = new DataTable("RET");
            PosDataObject.Closeout objCloseout6 = new PosDataObject.Closeout();
            objCloseout6.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl7 = objCloseout6.ShowReturnedRecord(Settings.TerminalName);

            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dtbl1);

            DataSet ds2 = new DataSet();
            ds2.Tables.Add(dtbl2);

            DataSet ds3 = new DataSet();
            ds3.Tables.Add(dtbl3);

            DataSet ds4 = new DataSet();
            ds4.Tables.Add(dtbl4);

            DataSet ds7 = new DataSet();
            ds7.Tables.Add(dtbl7);
            GeneralFunctions.MakeReportWatermark(rep_CloseOut);
            rep_CloseOut.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CloseOut.rReportHeader.Text = Settings.ReportHeader_Address;
            if (strCloseoutType == "C")
                rep_CloseOut.rType.Text = Properties.Resources.Consolidated;
            if (strCloseoutType == "T")
                rep_CloseOut.rType.Text = Properties.Resources.By_Terminal;
            if (strCloseoutType == "E")
                rep_CloseOut.rType.Text = Properties.Resources.By_Employee;

            rep_CloseOut.xrSubreport1.ReportSource = rep_CloseoutMain;
            if (Settings.TaxInclusive == "N")
            {
                rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1;
                rep_CloseoutMain1.DataSource = ds4;
                rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
            }
            else
            {
                rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1_tx;
                rep_CloseoutMain1_tx.DataSource = ds4;
                rep_CloseoutMain1_tx.DecimalPlace = Settings.DecimalPlace;
            }
            rep_CloseOut.xrSubreport3.ReportSource = rep_CloseoutMain2;
            rep_CloseoutMain.DataSource = ds4;
            //rep_CloseoutMain1.DataSource = ds4;
            rep_CloseoutMain2.DataSource = ds4;
            rep_CloseoutMain.COType = strCloseoutType;
            //rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
            rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
            if (strCloseoutType == "T") rep_CloseoutMain2.tiprow.Visible = false;
            rep_CloseOut.xrSubreport7.ReportSource = rep_Tender;
            rep_Tender.DataSource = ds1;
            rep_CloseOut.xrSubreport8.ReportSource = rep_TenderCount;
            rep_TenderCount.DataSource = ds2;
            rep_TenderCount.DecimalPlace = Settings.DecimalPlace;
            rep_CloseOut.xrSubreport10.ReportSource = rep_TenderOverShort;
            rep_TenderOverShort.DataSource = ds3;
            rep_TenderOverShort.DecimalPlace = Settings.DecimalPlace;
            rep_CloseOut.xrSubreport4.ReportSource = rep_Return;
            rep_Return.DataSource = ds7;
            rep_Return.DecimalPlace = Settings.DecimalPlace;

            if (rnt > 0)
            {
                rep_CloseOut.xrSubreport5.ReportSource = rep_CloseoutRent;
                rep_CloseoutRent.DataSource = ds4;
                rep_CloseoutRent.DecimalPlace = Settings.DecimalPlace;
            }

            if ((rpr > 0) || (rprsecurity > 0))
            {
                rep_CloseOut.xrSubreport6.ReportSource = rep_CloseoutRepair;
                rep_CloseoutRepair.DataSource = ds4;
                rep_CloseoutRepair.DecimalPlace = Settings.DecimalPlace;
            }

            if ((rnt == 0) && (rpr == 0) && (rprsecurity == 0))
            {
                rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
            }

            if ((rnt == 0) && ((rpr > 0) || (rprsecurity > 0)))
            {
                rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;
                rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
            }

            if ((rnt > 0) && ((rpr == 0) && (rprsecurity == 0)))
            {
                rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top + rep_CloseOut.xrSubreport5.Height + 10;
                rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
            }


            DataTable dtbl5 = new DataTable("SH");
            DataTable dtbl6 = new DataTable("SD");
            DataSet ds5 = new DataSet();
            DataSet ds6 = new DataSet();

            if ((chkSD.IsChecked == true) || (chkSH.IsChecked == true))
            {
                rep_CloseOut.xrSubreport9.ReportSource = rep_CloseoutAdditional;
                rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                if (strCloseoutType == "C")
                    rep_CloseoutAdditional.rType.Text = Properties.Resources.Consolidated;
                if (strCloseoutType == "T")
                    rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Terminal;
                if (strCloseoutType == "E")
                    rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Employee;
                if (chkSD.IsChecked == true)
                {
                    rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                    rep_SalesByDept.DecimalPlace = Settings.DecimalPlace;
                    rep_SalesByDept.COType = strCloseoutType;
                    PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                    objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl6 = objCloseout5.ShowSalesByDeptRecord(Settings.TerminalName, SystemVariables.DateFormat);
                    ds6.Tables.Add(dtbl6);
                    rep_SalesByDept.DataSource = ds6;
                }

                if (chkSH.IsChecked == true)
                {
                    rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                    rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                    rep_SalesByHour.COType = strCloseoutType;
                    PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                    objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl5 = objCloseout4.ShowSalesByHourRecord(Settings.TerminalName, SystemVariables.DateFormat);
                    ds5.Tables.Add(dtbl5);
                    rep_SalesByHour.DataSource = ds5;
                }
            }

            if (eventtype == "Print")
            {
                rep_CloseOut.CreateDocument();
                rep_CloseOut.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CloseOut);
                }
                finally
                {
                    rep_CloseOut.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                    dtbl5.Dispose();
                    dtbl6.Dispose();
                    dtbl7.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CloseOut.CreateDocument();
                rep_CloseOut.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "closeout.pdf";
                    GeneralFunctions.EmailReport(rep_CloseOut, attachfile, "Closeout");
                }
                finally
                {
                    rep_CloseOut.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                    dtbl5.Dispose();
                    dtbl6.Dispose();
                    dtbl7.Dispose();
                }
            }

        }

        private async void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                await PrepareForEmailPrint("Email");
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                await PrepareForPrint();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async Task PrepareForPrint()
        {
            int RowID = 0;
            RowID = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            int intCloseoutID = 0;
            string strCloseoutType = "";
            intCloseoutID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colID));
            strCloseoutType = await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colType);
            if (await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colED) == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Actual_closeout_has_not_been_performed + "\n" + Properties.Resources.This_is_a_preliminary_closeout_report);
            }

            if (chkReceiptPrn.IsChecked == true)
            {
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_Define_Receipt_Printer_in_Setup);
                    return;
                }
                blurGrid.Visibility = Visibility.Visible;
                POSSection.frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new POSSection.frmPOSInvoicePrintDlg();
                try
                {
                    PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                    objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive,"N");
                    frm_POSInvoicePrintDlg.PrintType = "Closeout";
                    frm_POSInvoicePrintDlg.CloseoutID = intCloseoutID;
                    frm_POSInvoicePrintDlg.CloseoutType = strCloseoutType;
                    frm_POSInvoicePrintDlg.CloseoutSaleHour = chkSH.IsChecked == true ? true : false;
                    frm_POSInvoicePrintDlg.CloseoutSaleDept = chkSD.IsChecked == true ? true : false;
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.boolPrintCloseout = true;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    frm_POSInvoicePrintDlg.Close();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "N");


                OfflineRetailV2.Report.Closeout.repCloseOut rep_CloseOut = new OfflineRetailV2.Report.Closeout.repCloseOut();
                OfflineRetailV2.Report.Closeout.repCloseoutMain rep_CloseoutMain = new OfflineRetailV2.Report.Closeout.repCloseoutMain();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1 rep_CloseoutMain1 = new OfflineRetailV2.Report.Closeout.repCloseoutMain1();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx rep_CloseoutMain1_tx = new OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx();
                OfflineRetailV2.Report.Closeout.repCloseoutMain2 rep_CloseoutMain2 = new OfflineRetailV2.Report.Closeout.repCloseoutMain2();
                OfflineRetailV2.Report.Closeout.repTender rep_Tender = new OfflineRetailV2.Report.Closeout.repTender();
                OfflineRetailV2.Report.Closeout.repTenderCount rep_TenderCount = new OfflineRetailV2.Report.Closeout.repTenderCount();
                OfflineRetailV2.Report.Closeout.repTenderOverShort rep_TenderOverShort = new OfflineRetailV2.Report.Closeout.repTenderOverShort();
                OfflineRetailV2.Report.Closeout.repReturn rep_Return = new OfflineRetailV2.Report.Closeout.repReturn();
                OfflineRetailV2.Report.Closeout.repCloseoutAdditional rep_CloseoutAdditional = new OfflineRetailV2.Report.Closeout.repCloseoutAdditional();
                OfflineRetailV2.Report.Closeout.repSalesByDept rep_SalesByDept = new OfflineRetailV2.Report.Closeout.repSalesByDept();
                OfflineRetailV2.Report.Closeout.repSalesByHour rep_SalesByHour = new OfflineRetailV2.Report.Closeout.repSalesByHour();

                OfflineRetailV2.Report.Closeout.repCloseoutTender rep_CloseoutTender = new OfflineRetailV2.Report.Closeout.repCloseoutTender();
                OfflineRetailV2.Report.Closeout.repCloseoutRent rep_CloseoutRent = new OfflineRetailV2.Report.Closeout.repCloseoutRent();
                OfflineRetailV2.Report.Closeout.repCloseoutRepair rep_CloseoutRepair = new OfflineRetailV2.Report.Closeout.repCloseoutRepair();

                DataTable dtbl1 = new DataTable("Tender");
                PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
                objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objCloseout.ShowTenderRecord("T", intCloseoutID, Settings.TerminalName);

                DataTable dtbl2 = new DataTable("TenderC");
                PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
                objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCloseout1.ShowTenderRecord("C", intCloseoutID, Settings.TerminalName);

                DataTable dtbl3 = new DataTable("TenderOverShort");
                PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
                objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objCloseout2.ShowTenderRecord("R", intCloseoutID, Settings.TerminalName);

                DataTable dtbl4 = new DataTable("Header");
                PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
                objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl4 = objCloseout3.ShowHeaderRecord(Settings.TerminalName, SystemVariables.DateFormat);

                int rnt = 0;
                int rpr = 0;
                double rprsecurity = 0;
                foreach (DataRow d in dtbl4.Rows)
                {
                    rnt = GeneralFunctions.fnInt32(d["RentInvoiceCount"].ToString());
                    rpr = GeneralFunctions.fnInt32(d["RepairInvoiceCount"].ToString());
                    rprsecurity = GeneralFunctions.fnDouble(d["RepairDeposit"].ToString());
                }

                DataTable dtbl7 = new DataTable("RET");
                PosDataObject.Closeout objCloseout6 = new PosDataObject.Closeout();
                objCloseout6.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl7 = objCloseout6.ShowReturnedRecord(Settings.TerminalName);

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dtbl1);

                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dtbl2);

                DataSet ds3 = new DataSet();
                ds3.Tables.Add(dtbl3);

                DataSet ds4 = new DataSet();
                ds4.Tables.Add(dtbl4);

                DataSet ds7 = new DataSet();
                ds7.Tables.Add(dtbl7);
                GeneralFunctions.MakeReportWatermark(rep_CloseOut);
                rep_CloseOut.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseOut.rReportHeader.Text = Settings.ReportHeader_Address;
                if (strCloseoutType == "C")
                    rep_CloseOut.rType.Text = Properties.Resources.Consolidated;
                if (strCloseoutType == "T")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Terminal;
                if (strCloseoutType == "E")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Employee;

                rep_CloseOut.xrSubreport1.ReportSource = rep_CloseoutMain;
                if (Settings.TaxInclusive == "N")
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1;
                    rep_CloseoutMain1.DataSource = ds4;
                    rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                }
                else
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1_tx;
                    rep_CloseoutMain1_tx.DataSource = ds4;
                    rep_CloseoutMain1_tx.DecimalPlace = Settings.DecimalPlace;
                }
                rep_CloseOut.xrSubreport3.ReportSource = rep_CloseoutMain2;
                rep_CloseoutMain.DataSource = ds4;
                //rep_CloseoutMain1.DataSource = ds4;
                rep_CloseoutMain2.DataSource = ds4;
                rep_CloseoutMain.COType = strCloseoutType;
                //rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
                if (strCloseoutType == "T") rep_CloseoutMain2.tiprow.Visible = false;
                rep_CloseOut.xrSubreport7.ReportSource = rep_Tender;
                rep_Tender.DataSource = ds1;
                rep_CloseOut.xrSubreport8.ReportSource = rep_TenderCount;
                rep_TenderCount.DataSource = ds2;
                rep_TenderCount.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport10.ReportSource = rep_TenderOverShort;
                rep_TenderOverShort.DataSource = ds3;
                rep_TenderOverShort.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport4.ReportSource = rep_Return;
                rep_Return.DataSource = ds7;
                rep_Return.DecimalPlace = Settings.DecimalPlace;

                if (rnt > 0)
                {
                    rep_CloseOut.xrSubreport5.ReportSource = rep_CloseoutRent;
                    rep_CloseoutRent.DataSource = ds4;
                    rep_CloseoutRent.DecimalPlace = Settings.DecimalPlace;
                }

                if ((rpr > 0) || (rprsecurity > 0))
                {
                    rep_CloseOut.xrSubreport6.ReportSource = rep_CloseoutRepair;
                    rep_CloseoutRepair.DataSource = ds4;
                    rep_CloseoutRepair.DecimalPlace = Settings.DecimalPlace;
                }

                if ((rnt == 0) && (rpr == 0) && (rprsecurity == 0))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt == 0) && ((rpr > 0) || (rprsecurity > 0)))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt > 0) && ((rpr == 0) && (rprsecurity == 0)))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top + rep_CloseOut.xrSubreport5.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }


                DataTable dtbl5 = new DataTable("SH");
                DataTable dtbl6 = new DataTable("SD");
                DataSet ds5 = new DataSet();
                DataSet ds6 = new DataSet();

                if ((chkSD.IsChecked == true) || (chkSH.IsChecked == true))
                {
                    rep_CloseOut.xrSubreport9.ReportSource = rep_CloseoutAdditional;
                    rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                    if (strCloseoutType == "C")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.Consolidated;
                    if (strCloseoutType == "T")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Terminal;
                    if (strCloseoutType == "E")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Employee;
                    if (chkSD.IsChecked == true)
                    {
                        rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                        rep_SalesByDept.DecimalPlace = Settings.DecimalPlace;
                        rep_SalesByDept.COType = strCloseoutType;
                        PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                        objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl6 = objCloseout5.ShowSalesByDeptRecord(Settings.TerminalName, SystemVariables.DateFormat);
                        ds6.Tables.Add(dtbl6);
                        rep_SalesByDept.DataSource = ds6;
                    }

                    if (chkSH.IsChecked == true)
                    {
                        rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                        rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                        rep_SalesByHour.COType = strCloseoutType;
                        PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                        objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl5 = objCloseout4.ShowSalesByHourRecord(Settings.TerminalName, SystemVariables.DateFormat);
                        ds5.Tables.Add(dtbl5);
                        rep_SalesByHour.DataSource = ds5;
                    }
                }

                rep_CloseOut.CreateDocument();
                rep_CloseOut.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CloseOut);
                }
                finally
                {
                    rep_CloseOut.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                    dtbl5.Dispose();
                    dtbl6.Dispose();
                    dtbl7.Dispose();
                }


            }

        }

        private async Task ExecutePreview()
        {

            int RowID = 0;
            RowID = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle == -1) return;
            int intCloseoutID = 0;
            string strCloseoutType = "";
            bool iscloseout = true;
            intCloseoutID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colID));
            strCloseoutType = await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colType);
            if (await GeneralFunctions.GetCellValue1(RowID, grdCloseout, colED) == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Actual_closeout_has_not_been_performed + "\n" + Properties.Resources.This_is_a_preliminary_closeout_report);
                iscloseout = false;
            }
            if (chkReceiptPrn.IsChecked == true)
            {
                /*
                if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/
                blurGrid.Visibility = Visibility.Visible;
                POSSection.frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new POSSection.frmPOSInvoicePrintDlg();
                try
                {
                    PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                    objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "N");
                    frm_POSInvoicePrintDlg.PrintType = "Closeout";
                    frm_POSInvoicePrintDlg.CloseoutID = intCloseoutID;
                    frm_POSInvoicePrintDlg.IsCloseout = iscloseout;
                    frm_POSInvoicePrintDlg.CloseoutType = strCloseoutType;
                    frm_POSInvoicePrintDlg.CloseoutSaleHour = chkSH.IsChecked == true ? true : false; 
                    frm_POSInvoicePrintDlg.CloseoutSaleDept = chkSD.IsChecked == true ? true : false; 
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    frm_POSInvoicePrintDlg.Close();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "N");

                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();
                OfflineRetailV2.Report.Closeout.repCloseOut rep_CloseOut = new OfflineRetailV2.Report.Closeout.repCloseOut();
                OfflineRetailV2.Report.Closeout.repCloseoutMain rep_CloseoutMain = new OfflineRetailV2.Report.Closeout.repCloseoutMain();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1 rep_CloseoutMain1 = new OfflineRetailV2.Report.Closeout.repCloseoutMain1();
                OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx rep_CloseoutMain1_tx = new OfflineRetailV2.Report.Closeout.repCloseoutMain1_tx();
                OfflineRetailV2.Report.Closeout.repCloseoutMain2 rep_CloseoutMain2 = new OfflineRetailV2.Report.Closeout.repCloseoutMain2();
                OfflineRetailV2.Report.Closeout.repTender rep_Tender = new OfflineRetailV2.Report.Closeout.repTender();
                OfflineRetailV2.Report.Closeout.repTenderCount rep_TenderCount = new OfflineRetailV2.Report.Closeout.repTenderCount();
                OfflineRetailV2.Report.Closeout.repTenderOverShort rep_TenderOverShort = new OfflineRetailV2.Report.Closeout.repTenderOverShort();
                OfflineRetailV2.Report.Closeout.repReturn rep_Return = new OfflineRetailV2.Report.Closeout.repReturn();
                OfflineRetailV2.Report.Closeout.repCloseoutAdditional rep_CloseoutAdditional = new OfflineRetailV2.Report.Closeout.repCloseoutAdditional();
                OfflineRetailV2.Report.Closeout.repSalesByDept rep_SalesByDept = new OfflineRetailV2.Report.Closeout.repSalesByDept();
                OfflineRetailV2.Report.Closeout.repSalesByHour rep_SalesByHour = new OfflineRetailV2.Report.Closeout.repSalesByHour();

                OfflineRetailV2.Report.Closeout.repCloseoutTender rep_CloseoutTender = new OfflineRetailV2.Report.Closeout.repCloseoutTender();
                OfflineRetailV2.Report.Closeout.repCloseoutRent rep_CloseoutRent = new OfflineRetailV2.Report.Closeout.repCloseoutRent();
                OfflineRetailV2.Report.Closeout.repCloseoutRepair rep_CloseoutRepair = new OfflineRetailV2.Report.Closeout.repCloseoutRepair();

                DataTable dtbl1 = new DataTable("Tender");
                PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
                objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objCloseout.ShowTenderRecord("T", intCloseoutID, Settings.TerminalName);

                DataTable dtbl2 = new DataTable("TenderC");
                PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
                objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCloseout1.ShowTenderRecord("C", intCloseoutID, Settings.TerminalName);

                DataTable dtbl3 = new DataTable("TenderOverShort");
                PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
                objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objCloseout2.ShowTenderRecord("R", intCloseoutID, Settings.TerminalName);

                DataTable dtbl4 = new DataTable("Header");
                PosDataObject.Closeout objCloseout3 = new PosDataObject.Closeout();
                objCloseout3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl4 = objCloseout3.ShowHeaderRecord(Settings.TerminalName, SystemVariables.DateFormat);

                int rnt = 0;
                int rpr = 0;
                double rprsecurity = 0;
                double paidouts = 0;
                foreach (DataRow d in dtbl4.Rows)
                {
                    rnt = GeneralFunctions.fnInt32(d["RentInvoiceCount"].ToString());
                    rpr = GeneralFunctions.fnInt32(d["RepairInvoiceCount"].ToString());
                    rprsecurity = GeneralFunctions.fnDouble(d["RepairDeposit"].ToString());
                    paidouts = GeneralFunctions.fnDouble(d["PaidOuts"].ToString());
                }

                double dblOverShort = 0;

                foreach (DataRow dr in dtbl3.Rows)
                {
                    dblOverShort = dblOverShort + GeneralFunctions.fnDouble(dr["TenderAmount"].ToString());
                }

                dblOverShort = dblOverShort + (-paidouts);

                DataTable dtbl7 = new DataTable("RET");
                PosDataObject.Closeout objCloseout6 = new PosDataObject.Closeout();
                objCloseout6.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl7 = objCloseout6.ShowReturnedRecord(Settings.TerminalName);

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dtbl1);

                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dtbl2);

                DataSet ds3 = new DataSet();
                ds3.Tables.Add(dtbl3);

                DataSet ds4 = new DataSet();
                ds4.Tables.Add(dtbl4);

                DataSet ds7 = new DataSet();
                ds7.Tables.Add(dtbl7);
                GeneralFunctions.MakeReportWatermark(rep_CloseOut);
                rep_CloseOut.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseOut.rReportHeader.Text = Settings.ReportHeader_Address;
                if (strCloseoutType == "C")
                    rep_CloseOut.rType.Text = Properties.Resources.Consolidated;
                if (strCloseoutType == "T")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Terminal;
                if (strCloseoutType == "E")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Employee;

                rep_CloseOut.xrSubreport1.ReportSource = rep_CloseoutMain;
                if (Settings.TaxInclusive == "N")
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1;
                    rep_CloseoutMain1.DataSource = ds4;
                    rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                }
                else
                {
                    rep_CloseOut.xrSubreport2.ReportSource = rep_CloseoutMain1_tx;
                    rep_CloseoutMain1_tx.DataSource = ds4;
                    rep_CloseoutMain1_tx.DecimalPlace = Settings.DecimalPlace;
                }
                rep_CloseOut.xrSubreport3.ReportSource = rep_CloseoutMain2;
                rep_CloseoutMain.DataSource = ds4;
                //rep_CloseoutMain1.DataSource = ds4;
                rep_CloseoutMain2.DataSource = ds4;
                rep_CloseoutMain.COType = strCloseoutType;
                //rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
                if (strCloseoutType == "T") rep_CloseoutMain2.tiprow.Visible = false;
                rep_CloseOut.xrSubreport7.ReportSource = rep_Tender;
                rep_Tender.DataSource = ds1;
                rep_CloseOut.xrSubreport8.ReportSource = rep_TenderCount;
                rep_TenderCount.DataSource = ds2;
                rep_TenderCount.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport10.ReportSource = rep_TenderOverShort;
                rep_TenderOverShort.DataSource = ds3;
                rep_TenderOverShort.DecimalPlace = Settings.DecimalPlace;
                if (iscloseout)
                {
                    rep_TenderOverShort.tabz.Visible = true;
                    rep_TenderOverShort.zline1txt.Text = "Gross Over/ (Short)";
                    rep_TenderOverShort.rTotal2.Text = dblOverShort.ToString();
                }
                rep_CloseOut.xrSubreport4.ReportSource = rep_Return;
                rep_Return.DataSource = ds7;
                rep_Return.DecimalPlace = Settings.DecimalPlace;

                if (rnt > 0)
                {
                    rep_CloseOut.xrSubreport5.ReportSource = rep_CloseoutRent;
                    rep_CloseoutRent.DataSource = ds4;
                    rep_CloseoutRent.DecimalPlace = Settings.DecimalPlace;
                }

                if ((rpr > 0) || (rprsecurity > 0))
                {
                    rep_CloseOut.xrSubreport6.ReportSource = rep_CloseoutRepair;
                    rep_CloseoutRepair.DataSource = ds4;
                    rep_CloseoutRepair.DecimalPlace = Settings.DecimalPlace;
                }

                if ((rnt == 0) && (rpr == 0) && (rprsecurity == 0))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt == 0) && ((rpr > 0) || (rprsecurity > 0)))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }

                if ((rnt > 0) && ((rpr == 0) && (rprsecurity == 0)))
                {
                    rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                    rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport5.Top + rep_CloseOut.xrSubreport5.Height + 10;
                    rep_CloseOut.xrSubreport8.Top = rep_CloseOut.xrSubreport7.Top + rep_CloseOut.xrSubreport7.Height + 10;
                    rep_CloseOut.xrSubreport10.Top = rep_CloseOut.xrSubreport8.Top + rep_CloseOut.xrSubreport8.Height + 10;
                }
                //rep_CloseOut.xrSubreport5.Visible = false;
                //rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                //rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;

                DataTable dtbl5 = new DataTable("SH");
                DataTable dtbl6 = new DataTable("SD");
                DataSet ds5 = new DataSet();
                DataSet ds6 = new DataSet();

                if ((chkSD.IsChecked == true) || (chkSH.IsChecked == true))
                {
                    rep_CloseOut.xrSubreport9.ReportSource = rep_CloseoutAdditional;
                    rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                    if (strCloseoutType == "C")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.Consolidated;
                    if (strCloseoutType == "T")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Terminal;
                    if (strCloseoutType == "E")
                        rep_CloseoutAdditional.rType.Text = Properties.Resources.By_Employee;
                    if (chkSD.IsChecked == true)
                    {
                        rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                        rep_SalesByDept.DecimalPlace = Settings.DecimalPlace;
                        rep_SalesByDept.COType = strCloseoutType;
                        PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                        objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl6 = objCloseout5.ShowSalesByDeptRecord(Settings.TerminalName, SystemVariables.DateFormat);
                        ds6.Tables.Add(dtbl6);
                        rep_SalesByDept.DataSource = ds6;
                    }

                    if (chkSH.IsChecked == true)
                    {
                        rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                        rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                        rep_SalesByHour.COType = strCloseoutType;
                        PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                        objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl5 = objCloseout4.ShowSalesByHourRecord(Settings.TerminalName, SystemVariables.DateFormat);
                        ds5.Tables.Add(dtbl5);
                        rep_SalesByHour.DataSource = ds5;
                    }
                }


                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CloseOut.PrinterName = Settings.ReportPrinterName;
                    rep_CloseOut.CreateDocument();
                    rep_CloseOut.PrintingSystem.ShowMarginsWarning = false;
                    rep_CloseOut.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_CloseOut.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CloseOut;
                    window.ShowDialog();

                }
                finally
                {
                    rep_CloseoutMain.Dispose();
                    rep_CloseoutMain1.Dispose();
                    rep_CloseoutMain1_tx.Dispose();
                    rep_Tender.Dispose();
                    rep_TenderOverShort.Dispose();
                    rep_TenderCount.Dispose();
                    rep_CloseoutAdditional.Dispose();
                    rep_SalesByDept.Dispose();
                    rep_SalesByHour.Dispose();
                    rep_CloseOut.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                    dtbl5.Dispose();
                    dtbl6.Dispose();
                    dtbl7.Dispose();

                }
            }
        }
    }
}
