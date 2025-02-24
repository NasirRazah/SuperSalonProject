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
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Editors;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_CloseoutCountDlg.xaml
    /// </summary>
    public partial class frm_CloseoutCountDlg : Window
    {

        private string strExportDir = "";
        private string strExportPath1 = "";
        private string strExportFile = "";
        private bool blViewPrevFile = false;

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        private int intCloseoutID;
        private string strCloseoutType;
        private string strCountType;
        private string strCloseoutExportLocation = "";
        private DataTable dtblCloseout = null;
        private DataTable dtblCurrencyCount = null;
        private bool bEditTendering;

        private double CashFloatAmount = 0;

        private int SuspendOrderCnt = 0;

        private int CashID = 0;
        private int CashRowHandle = -1;

        private bool boolOK;
        public frm_CloseoutCountDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public bool EditTendering
        {
            get { return bEditTendering; }
            set { bEditTendering = value; }
        }

        public int CloseoutID
        {
            get { return intCloseoutID; }
            set { intCloseoutID = value; }
        }

        public string CloseoutType
        {
            get { return strCloseoutType; }
            set { strCloseoutType = value; }
        }

        public string CountType
        {
            get { return strCountType; }
            set { strCountType = value; }
        }

        

        private void GetInitialData()
        {
            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            bool blf = true;
            bool blCashFloat = false;
            double dblCashFloat = 0;
            if (Settings.POSCardPayment == "Y") if (Settings.PaymentGateway == 1) blf = false;
            DataTable dtbl = new DataTable();
            PosDataObject.TenderTypes objTType = new PosDataObject.TenderTypes();
            objTType.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objTType.FetchPOSData(blf);
            foreach (DataRow dr in dtbl.Rows)
            {
                DataTable dtbl1 = new DataTable();
                PosDataObject.Closeout objC = new PosDataObject.Closeout();
                objC.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objC.FetchBlindTender(strCloseoutType, intCloseoutID, GeneralFunctions.fnInt32(dr["ID"].ToString()));


                if (strCloseoutType == "T") blCashFloat = true;
                if (strCloseoutType == "C")
                {
                    if (objC.IsTerminalConsolidatedCloseout(intCloseoutID))
                    {
                        blCashFloat = true;
                    }
                }

                if (blCashFloat)
                {
                    dblCashFloat = objC.FetchCashFloatAmount(strCloseoutType, intCloseoutID);
                }
                CashFloatAmount = dblCashFloat;

                int cnt = 0;
                double amt = 0;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    cnt = GeneralFunctions.fnInt32(dr1["ECount"].ToString());
                    amt = GeneralFunctions.fnDouble(dr1["EReceipt"].ToString());
                }
                if (dr["Name"].ToString() == "House Account")
                {
                    dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString() ,
                                                   amt,amt,cnt.ToString(),0,dr["Name"].ToString(),strip});
                }
                else
                {
                    /*
                    if ((dr["Name"].ToString() == "Cash") && (blCashFloat))
                    {
                        dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString() ,
                                                   0,amt + dblCashFloat,cnt.ToString(),0-amt-dblCashFloat,dr["Name"].ToString(), strip});
                    }
                    else
                    {
                        dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString() ,
                                                   0,amt,cnt.ToString(),0-amt,dr["Name"].ToString(), strip});
                    }*/

                    dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString() ,
                                                   0,amt,cnt.ToString(),0-amt,dr["Name"].ToString(), strip});
                }
            }


            

            subF.Text = totF.Text = SystemVariables.CurrencySymbol + CashFloatAmount.ToString("f2");
            subFV.Text = SystemVariables.CurrencySymbol + "0.00";

            PosDataObject.Closeout objC1 = new PosDataObject.Closeout();
            objC1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dinfo = objC1.GetCloseoutInfo(intCloseoutID);

            string trncnt = "";
            string terminal = "";
            string empname = "";
            string empdate = "";
            string emptime = "";
            foreach (DataRow dr in dinfo.Rows)
            {
                trncnt = dr["TRANCNT"].ToString();
                terminal = dr["TERMINAL"].ToString();
                empname = dr["EMPNAME"].ToString();
                empdate = dr["DATE"].ToString();
                emptime = dr["TIME"].ToString();
            }
            totT.Text = trncnt;
            lbCreateUser.Text = empname;
            lbCreateDate.Text = empdate;
            lbCreateTime.Text = emptime;

            if (strCloseoutType == "C")
            {
                lbTerminal.Text = "CONSOLIDATED";
            }
            else if (strCloseoutType == "E")
            {
                lbTerminal.Text = "Employee: " + empname;
            }
            else
            {
                lbTerminal.Text = "Terminal: " + terminal;
            }

            SuspendOrderCnt = objC1.OpenSuspndItem(strCloseoutType, intCloseoutID);

            lbOpenOrder.Text = SuspendOrderCnt + " Suspened Transactions";


            


        }

        private void GetInitialDataForEdit()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Closeout objTType = new PosDataObject.Closeout();
            objTType.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objTType.FetchCloseoutTenderRecord(intCloseoutID);

            foreach (DataRow dr in dtblCloseout.Rows)
            {
                foreach (DataRow dr1 in dtbl.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == GeneralFunctions.fnInt32(dr1["TenderID"].ToString()))
                    {
                        dr["ActualReceipt"] = GeneralFunctions.fnDouble(dr1["TenderAmount"].ToString());
                        dr["Difference"] = GeneralFunctions.fnDouble(dr1["TenderAmount"].ToString()) - GeneralFunctions.fnDouble(dr["ExpectedReceipt"].ToString());
                        break;
                    }
                }
            }

            txtNotes.Text = objTType.FetchCloseoutNotes(intCloseoutID);


            DataTable dtbl1 = new DataTable();
            dtbl1 = objTType.FetchCloseoutCurrencyCount(intCloseoutID);
            foreach (DataRow dr in dtbl1.Rows)
            {
                dtblCurrencyCount.Rows.Add(new object[] {
                                        dr["Penny"].ToString(),
                                        dr["TwoPenny"].ToString(),
                                        dr["Nickel"].ToString(),
                                        dr["Dime"].ToString(),
                                        dr["TwentyPenny"].ToString(),
                                        dr["Quarter"].ToString(),
                                        dr["Halve"].ToString(),
                                        dr["One"].ToString(),
                                        dr["Two"].ToString(),
                                        dr["Five"].ToString(),
                                        dr["Ten"].ToString(),
                                        dr["Twenty"].ToString(),
                                        dr["Fifty"].ToString(),
                                        dr["Hundred"].ToString(),
                                        dr["TwoHundred"].ToString(),
                                        dr["FiveHundred"].ToString(),
                                        dr["OneThousand"].ToString()
                });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = "Closeout Data";
            dtblCurrencyCount = new DataTable();
            dtblCurrencyCount.Columns.Add("Penny", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("TwoPenny", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Nickel", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Dime", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("TwentyPenny", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Quarter", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Halve", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("One", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Two", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Five", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Ten", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Twenty", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Fifty", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("Hundred", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("TwoHundred", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("FiveHundred", System.Type.GetType("System.String"));
            dtblCurrencyCount.Columns.Add("OneThousand", System.Type.GetType("System.String"));


            if (bEditTendering)
            {
                chkReceiptPrn.Visibility = Visibility.Hidden;
                Title.Text = Properties.Resources.Edit_Close_Out;
                btnOK.Content = "SAVE & CLOSE";
            }
            chkReceiptPrn.IsChecked = (Settings.DefaultCloseoutPrinter == "Receipt Printer");
            dtblCloseout = new DataTable();
            dtblCloseout.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("TenderName", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("ActualReceipt", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("ExpectedReceipt", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("ExpectedCount", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("Difference", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("Name", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            GetInitialData();
            if (bEditTendering) GetInitialDataForEdit();
            grdTender.ItemsSource = dtblCloseout;
            RecalculateTotal();

            PosDataObject.TenderTypes objTenderTypes = new PosDataObject.TenderTypes();
            objTenderTypes.Connection = SystemVariables.Conn;
            CashID = objTenderTypes.FetchCashID();

            GetCashTenderRowPosition();

            if (CashRowHandle >= 0)
            {
                pnlTerminal.Visibility = Visibility.Visible;
            }
            else
            {
                pnlTerminal.Visibility = Visibility.Collapsed;
            }


            if (strCountType == "C")
            {

                colDiff.Visible = false;
                colExpectedC.Visible = false;
                colExpectedR.Visible = false;
            }
            if (strCountType == "R")
            {
                
                colDiff.Visible = true;
            }

            SetDecimalPlace();
        }

        private void GetCashTenderRowPosition()
        {
            DataTable dtbl = grdTender.ItemsSource as DataTable;
            int i = -1;
            foreach(DataRow dr in dtbl.Rows)
            {
                i++;
                if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == CashID)
                {
                    CashRowHandle = i;
                    break;
                }
            }
        }

        private void SetDecimalPlace()
        {

            /*if (Settings.DecimalPlace == 3)
            {
                colActualR.DisplayFormat.FormatString = "f3";
                colExpectedR.DisplayFormat.FormatString = "f3";
                colDiff.DisplayFormat.FormatString = "f3";
                colActualR.SummaryItem.DisplayFormat = "{0:0.000}";
                colExpectedR.SummaryItem.DisplayFormat = "{0:0.000}";
                colDiff.SummaryItem.DisplayFormat = "{0:0.000}";
            }
            else
            {
                colActualR.DisplayFormat.FormatString = "f";
                colExpectedR.DisplayFormat.FormatString = "f";
                colDiff.DisplayFormat.FormatString = "f";
                colActualR.SummaryItem.DisplayFormat = "{0:0.00}";
                colExpectedR.SummaryItem.DisplayFormat = "{0:0.00}";
                colDiff.SummaryItem.DisplayFormat = "{0:0.00}";
            }*/
        }


        private void CloseKeyboards()
        {
            
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void TxtNotes_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (bEditTendering)
            {
                PosDataObject.Closeout objcls = new PosDataObject.Closeout();
                objcls.Connection = SystemVariables.Conn;
                objcls.CloseOutID = intCloseoutID;
                objcls.CloseoutType = strCloseoutType;
                objcls.Notes = txtNotes.Text.Trim();
                objcls.LoginUserID = SystemVariables.CurrentUserID;
                objcls.TenderDataTable = grdTender.ItemsSource as DataTable;
                objcls.CurrencyCalculateDataTable = dtblCurrencyCount;
                objcls.TerminalName = Settings.TerminalName;
                objcls.BeginTransaction();
                if (objcls.CloseOutTransaction_Editing())
                {

                }
                objcls.EndTransaction();
                string strerrmsg = objcls.ErrorMsg;
                if ((strerrmsg == "") || (strerrmsg == null))
                {
                    PosDataObject.Closeout objCloseoutM1 = new PosDataObject.Closeout();
                    objCloseoutM1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objCloseoutM1.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "Y");//Settings.CentralExportImport == "Y" ? "Y" : "N"

                }
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
            else
            {
                if (SuspendOrderCnt > 0)
                {
                    DocMessage.MsgInformation("Supended transaction(s) exists.");
                    return;
                }
                if (chkReceiptPrn.IsChecked == true)
                {
                    /*
                    if (Settings.ReceiptPrinterName == "")
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please Define Receipt Printer in Setup.", "frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                        return;
                    }*/
                }



                gridView1.PostEditor();
                PosDataObject.Closeout objcls = new PosDataObject.Closeout();
                objcls.Connection = SystemVariables.Conn;
                objcls.CloseOutID = intCloseoutID;
                objcls.CloseoutType = strCloseoutType;
                objcls.Notes = txtNotes.Text.Trim();
                objcls.LoginUserID = SystemVariables.CurrentUserID;
                objcls.TenderDataTable = grdTender.ItemsSource as DataTable;
                objcls.CurrencyCalculateDataTable = dtblCurrencyCount;
                objcls.TerminalName = Settings.TerminalName;
                objcls.BeginTransaction();
                if (objcls.CloseOutTransaction())
                {

                }
                objcls.EndTransaction();
                string strerrmsg = objcls.ErrorMsg;
                if ((strerrmsg == "") || (strerrmsg == null))
                {
                    Cursor = Cursors.Wait;
                    try
                    {
                        ExecuteReport();
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                    }
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    this.Close();
                }
            }
        }

        private void ExecuteReport()
        {

            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutReportProc(strCloseoutType, intCloseoutID, Settings.TerminalName, Settings.TaxInclusive, "Y"); // Settings.CentralExportImport == "Y" ? "Y" : "N"

            if (chkReceiptPrn.IsChecked == true)
            {
                blurGrid.Visibility = Visibility.Visible;
                POSSection.frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new POSSection.frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.PrintType = "Closeout";
                    frm_POSInvoicePrintDlg.CloseoutID = intCloseoutID;
                    frm_POSInvoicePrintDlg.CloseoutType = strCloseoutType;
                    frm_POSInvoicePrintDlg.IsCloseout = true;
                    frm_POSInvoicePrintDlg.CloseoutSaleHour = true;
                    frm_POSInvoicePrintDlg.CloseoutSaleDept = true;
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

                DataTable dtbl5 = new DataTable("SH");
                PosDataObject.Closeout objCloseout4 = new PosDataObject.Closeout();
                objCloseout4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl5 = objCloseout4.ShowSalesByHourRecord(Settings.TerminalName, SystemVariables.DateFormat);


                DataTable dtbl6 = new DataTable("SD");
                PosDataObject.Closeout objCloseout5 = new PosDataObject.Closeout();
                objCloseout5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl6 = objCloseout5.ShowSalesByDeptRecord(Settings.TerminalName, SystemVariables.DateFormat);

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

                DataSet ds5 = new DataSet();
                ds5.Tables.Add(dtbl5);

                DataSet ds6 = new DataSet();
                ds6.Tables.Add(dtbl6);
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
                rep_CloseoutMain2.DataSource = ds4;
                rep_CloseoutMain.COType = strCloseoutType;

                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
                if (strCloseoutType == "T") rep_CloseoutMain2.tiprow.Visible = false;
                rep_CloseOut.xrSubreport7.ReportSource = rep_Tender;
                rep_Tender.DataSource = ds1;
                rep_CloseOut.xrSubreport8.ReportSource = rep_TenderCount;
                rep_TenderCount.DataSource = ds2;
                rep_TenderCount.DecimalPlace = Settings.DecimalPlace;
                rep_CloseOut.xrSubreport10.ReportSource = rep_TenderOverShort;
                rep_TenderOverShort.DataSource = ds3;
                rep_TenderOverShort.tabz.Visible = true;
                rep_TenderOverShort.zline1txt.Text = "Gross Over/ (Short)";
                rep_TenderOverShort.rTotal2.Text = dblOverShort.ToString();
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

                rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                rep_SalesByHour.COType = strCloseoutType;
                rep_SalesByHour.DataSource = ds5;
                rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                rep_SalesByDept.COType = strCloseoutType;
                rep_SalesByDept.DecimalPlace = Settings.DecimalPlace;
                rep_SalesByDept.DataSource = ds6;

                /*if (Settings.CloseoutExport == "Y")
                {
                    int Expr = 0;
                    Expr = ExecuteExport(dtbl4, dtbl1);
                    if (Expr == 0)
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Export completed successfully." + "\n" + "Export data saved to " + strCloseoutExportLocation);
                    }
                    else
                    {
                        DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Error while exporting...");
                    }
                }*/


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
                    rep_CloseOut.Dispose();

                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();

                }


                /*if ((Settings.CentralExportImport == "Y") && (strCloseoutType == "C"))
                {
                    strExportPath1 = ExpFileName();

                    bool blproceed = true;
                    bool blprev = CheckIfExportedToday();
                    if (blprev)
                    {
                        if (DocMessage.MsgConfirmation(Translation.SetMultilingualTextInCodes("Data has already been exported to central office today." + "\n" + "Do you want to continue agian?") == DialogResult.Yes)
                        {
                            blproceed = true;
                        }
                        else
                        {
                            blproceed = false;
                        }
                    }
                    else
                    {
                        if (DocMessage.MsgConfirmation(Translation.SetMultilingualTextInCodes("Do you want to export data to central office?") == DialogResult.Yes)
                        {
                            blproceed = true;
                        }
                        else
                        {
                            blproceed = false;
                        }
                    }
                    if (!blproceed) return;
                    blViewPrevFile = false;
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int rtn = ExecuteExport();
                        if (rtn == 0)
                        {
                            UpdateSalesExportFlag();
                            UpdateEmployeeAttnExportFlag();
                            InsertExpImpLog("E", strExportFile, strExportPath1);

                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Data exported to central office successfully." + "\n" + "File saved in " + strExportPath1);
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = strExportPath1;
                            p.Start();
                        }
                        if (rtn == 1)
                        {
                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("No data found for export to central office.");
                        }
                        if (rtn == 2)
                        {
                            DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Permission error while exporting to central office...");
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }*/
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private async void GridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //btnCurrencyCalculator.Visibility = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdTender, colName) == "Cash" ? Visibility.Visible : Visibility.Hidden;
        }

        private async void BtnCurrencyCalculator_Click(object sender, RoutedEventArgs e)
        {
            GetCashTenderRowPosition();

            blurGrid.Visibility = Visibility.Visible;
            frm_CashCalculator fcash = new frm_CashCalculator();
            try
            {
                fcash.CurrencyCount = dtblCurrencyCount;
                fcash.ShowDialog();
                if (fcash.DialogResult == true)
                {
                    dtblCurrencyCount = fcash.CurrencyCount;
                    grdTender.SetCellValue(CashRowHandle, colActualR, fcash.Total.ToString("f2"));
                    double dblAR = 0;
                    double dblER = 0;
                    dblAR = fcash.Total;
                    dblER = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(CashRowHandle, grdTender, colExpectedR));

                    grdTender.SetCellValue(CashRowHandle, colDiff, dblAR - dblER);

                    RecalculateTotal();
                }
            }
            finally
            {
                fcash.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void GridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colActualR")
            {
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                if (e.Value == null)
                {
                    double dblAR = 0;
                    double dblER = 0;
                    dblAR = 0;
                    dblER = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdTender, colExpectedR));
                    grdTender.SetCellValue(intRowNum, colActualR, dblAR);
                    grdTender.SetCellValue(intRowNum, colDiff, dblAR - dblER);

                    RecalculateTotal();
                    e.Handled = true;
                    return;
                }
                else if (e.Value.ToString() == "")
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    double dblAR = 0;
                    double dblER = 0;
                    dblAR = GeneralFunctions.fnDouble(e.Value);
                    dblER = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdTender, colExpectedR));
                    grdTender.SetCellValue(intRowNum, colActualR, dblAR);
                    grdTender.SetCellValue(intRowNum, colDiff, dblAR - dblER);

                    RecalculateTotal();
                }
            }
        }

        private void BtnUp1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdTender.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle > 0))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            }
        }

        private void BtnDown1_Click(object sender, RoutedEventArgs e)
        {
            if (((grdTender.ItemsSource as DataTable).Rows.Count > 0) && (gridView1.FocusedRowHandle != (grdTender.ItemsSource as DataTable).Rows.Count - 1))
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            }
        }

        private void BtnKybrd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        

        private void GrdTender_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if ((e.Column.Name == "colActualR") || (e.Column.Name == "colExpectedR") || (e.Column.Name == "colDiff"))
            {
                e.DisplayText = GeneralFunctions.fnDouble(e.Value) >= 0 ? SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(e.Value).ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-GeneralFunctions.fnDouble(e.Value)).ToString("f2") + ")"; ;
            }
        }

        private void RecalculateTotal()
        {
            DataTable dtbl = grdTender.ItemsSource as DataTable;

            double dblCounted = 0;
            double dblExpected = 0;
            double dblVar = 0;

            foreach (DataRow dr in dtbl.Rows)
            {
                dblCounted = dblCounted + GeneralFunctions.fnDouble(dr["ActualReceipt"].ToString());
                dblExpected = dblExpected + GeneralFunctions.fnDouble(dr["ExpectedReceipt"].ToString());
                dblVar = dblVar + GeneralFunctions.fnDouble(dr["Difference"].ToString());
            }
            dtbl.Dispose();

            subC.Text = dblCounted >= 0 ? SystemVariables.CurrencySymbol + dblCounted.ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-dblCounted).ToString("f2") + ")";
            subE.Text = dblExpected >= 0 ? SystemVariables.CurrencySymbol + dblExpected.ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-dblExpected).ToString("f2") + ")";
            
            subV.Text = dblVar >= 0 ? SystemVariables.CurrencySymbol + dblVar.ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-dblVar).ToString("f2") + ")";


            footerC.Text = totC.Text = (dblCounted + CashFloatAmount) >= 0 ? SystemVariables.CurrencySymbol + (dblCounted + CashFloatAmount).ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-(dblCounted + CashFloatAmount)).ToString("f2") + ")";
            footerE.Text = totE.Text = (dblExpected + CashFloatAmount) >= 0 ? SystemVariables.CurrencySymbol + (dblExpected + CashFloatAmount).ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-(dblExpected + CashFloatAmount)).ToString("f2") + ")";
            footerV.Text = totV.Text = dblVar >= 0 ? SystemVariables.CurrencySymbol + dblVar.ToString("f2") : "(" + SystemVariables.CurrencySymbol + (-dblVar).ToString("f2") + ")"; 
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

        private void GridView1_ShownEditor(object sender, EditorEventArgs e)
        {
            if (e.Column.FieldName == "ActualReceipt")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0.00";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Closeout";
                    nkybrd.GridColumnName = "ActualReceipt";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView1.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0)); nkybrd.Left = location.X - 385;
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

        public async void UpdateGridValueByOnscreenKeyboard(string strgridname, string gridcol, int rindx, string val)
        {
            if (gridcol == "ActualReceipt")
            {
                grdTender.SetCellValue(rindx, colActualR, val);
                int intRowNum = 0;
                intRowNum = gridView1.FocusedRowHandle;
                double dblAR = 0;
                double dblER = 0;
                dblAR = GeneralFunctions.fnDouble(val);
                dblER = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(intRowNum, grdTender, colExpectedR));
                grdTender.SetCellValue(intRowNum, colActualR, dblAR);
                grdTender.SetCellValue(intRowNum, colDiff, dblAR - dblER);

                RecalculateTotal();
            }

        }
    }
}
