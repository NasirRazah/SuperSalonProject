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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frmCloseoutDlg_CashOut.xaml
    /// </summary>
    public partial class frmCloseoutDlg_CashOut : UserControl
    {
        private int COUTID = 0;
        private string CTYPE = "";
        private string strCloseoutExportLocation = "";
        private DataTable dtblCloseout = null;

        public frmCloseoutDlg_CashOut()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        public CommandBase frmCloseoutDlg_CashOutClosed { get; set; }

        private int GetClosedRegisterForThisTerminal()
        {
            int closeoutId = GeneralFunctions.GetCloseOutID();
            PosDataObject.Closeout objc = new PosDataObject.Closeout();
            objc.Connection = SystemVariables.Conn;
            int trncnt = objc.FetchTransactionCount(closeoutId);
            if (trncnt > 0)
            {
                DocMessage.MsgInformation(Properties.Resources.Register_is_already_open);
                return 0;
            }
            else
            {
                int cashfloatcnt = objc.FetchCashFloatCount(closeoutId);
                if (cashfloatcnt > 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Register_is_already_open);
                    return 0;
                }
                else
                {
                    return closeoutId;
                }
            }
        }

        private void BtnOpenRegister_Click(object sender, RoutedEventArgs e)
        {
            if ((SystemVariables.CurrentUserID > 0) && (!SecurityPermission.AccessCashFloat))
            {
                DocMessage.MsgPermission();
                return;
            }

            int clsID = GetClosedRegisterForThisTerminal();
            if (clsID > 0)
            {
                blurGrid.Visibility = Visibility.Visible;
                frm_OpenRegisterDlg frm = new frm_OpenRegisterDlg();
                try
                {
                    frm.CloseoutID = clsID;
                    frm.ShowDialog();
                }
                finally
                {
                    frm.Close();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void PopulateTerminal()
        {
            DataTable dbtblTerminal = new DataTable();
            dbtblTerminal.Columns.Add("ID", System.Type.GetType("System.String"));
            dbtblTerminal.Columns.Add("Terminal", System.Type.GetType("System.String"));
            if ((Settings.OtherTerminalCloseout == "Y") && ((SystemVariables.CurrentUserID <= 0) || ((SystemVariables.CurrentUserID > 0) && (SecurityPermission.AccessOtherTerminalCloseout))))
            {
                //dtblCloseout = new DataTable();
                PosDataObject.Closeout oc = new PosDataObject.Closeout();
                oc.Connection = SystemVariables.Conn;
                DataTable dtbl = oc.GetActiveTerminals();
                foreach (DataRow dr in dtbl.Rows)
                {
                    dbtblTerminal.Rows.Add(new object[] { dr["TerminalName"].ToString(), dr["TerminalName"].ToString() });
                }
                dtbl.Dispose();

                bool blCheckCurrenTerminalExistOrNot = false;

                foreach (DataRow dr in dbtblTerminal.Rows)
                {
                    if (dr["ID"].ToString() == Settings.TerminalName)
                    {
                        blCheckCurrenTerminalExistOrNot = true;
                        break;
                    }
                }

                if (!blCheckCurrenTerminalExistOrNot)
                {
                    dbtblTerminal.Rows.Add(new object[] { Settings.TerminalName, Settings.TerminalName });
                }
            }
            else
            {
                dbtblTerminal.Rows.Add(new object[] { Settings.TerminalName, Settings.TerminalName });
            }
            lkup.ItemsSource = dbtblTerminal;
            lkup.DisplayMember = "Terminal";
            lkup.ValueMember = "ID";
            dbtblTerminal.Dispose();
        }

        public void PopulateEmployee()
        {
            DataTable dbtblEmp = new DataTable();
            dbtblEmp.Columns.Add("ID", System.Type.GetType("System.String"));
            dbtblEmp.Columns.Add("EmployeeID", System.Type.GetType("System.String"));
            dbtblEmp.Columns.Add("EmployeeName", System.Type.GetType("System.String"));
            if (SystemVariables.CurrentUserID <= 0)
            {
                dbtblEmp.Rows.Add(new object[] { SystemVariables.CurrentUserID.ToString(), SystemVariables.CurrentUserName.ToString(),
                                                 SystemVariables.CurrentUserName.ToString()});
            }
            else
            {
                dbtblEmp.Rows.Add(new object[] { SystemVariables.CurrentUserID.ToString(), SystemVariables.CurrentUserCode.ToString(),
                                                 SystemVariables.CurrentUserName.ToString()});
            }
            lkup.ItemsSource = dbtblEmp;
            lkup.DisplayMember = "EmployeeID";
            lkup.ValueMember = "ID";
            dbtblEmp.Dispose();
        }


        public void Load()
        {
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.PreviewMouseLeftButtonDown += BtnFrontOffice_PreviewMouseLeftButtonDown;
            //Title.Text = "Manage Register";
            dtblCloseout = new DataTable();
            dtblCloseout.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("TenderName", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("ActualReceipt", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("ExpectedReceipt", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("ExpectedCount", System.Type.GetType("System.String"));
            dtblCloseout.Columns.Add("Difference", System.Type.GetType("System.Double"));
            dtblCloseout.Columns.Add("Name", System.Type.GetType("System.String"));

            if (Settings.CloseoutOption == 0)
            {
                rg1.IsChecked = true;
                rg1.Visibility = Visibility.Hidden;
                rg2.Visibility = Visibility.Hidden;
                lkup.Visibility = Visibility.Hidden;
                btnOpenRegister.Visibility = Visibility.Hidden;
            }
            if (Settings.CloseoutOption == 1)
            {
                rg1.Visibility = Visibility.Visible;
                rg2.Visibility = Visibility.Visible;
                rg2.IsChecked = true;
                rg2.Content = Properties.Resources.By_Terminal;
                lkup.Visibility = Visibility.Visible;
                PopulateTerminal();
                lkup.EditValue = Settings.TerminalName;
            }
            if (Settings.CloseoutOption == 2)
            {
                rg1.Visibility = Visibility.Visible;
                rg2.Visibility = Visibility.Visible;
                rg2.IsChecked = true;
                rg2.Content = Properties.Resources.By_Employee;
                lkup.Visibility = Visibility.Visible;
                PopulateEmployee();
                lkup.EditValue = SystemVariables.CurrentUserID.ToString();
                btnOpenRegister.Visibility = Visibility.Hidden;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AccessBlindDrop) && (!SecurityPermission.AccessBlindCount) && (!SecurityPermission.AccessReconcileCount)  && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            blurGrid.Visibility = Visibility.Visible;
            frm_CloseoutEditDlg frm = new frm_CloseoutEditDlg();
            try
            {
                if (rg1.IsChecked == true)
                {
                    frm.CloseoutType = "C";
                }
                else
                {
                    if (Settings.CloseoutOption == 1)
                    {
                        frm.CloseoutType = "T";
                    }
                    else
                    {
                        frm.CloseoutType = "E";
                    }
                }
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnReprint_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_CloseoutReprintDlg frm_CloseoutReprintDlg = new frm_CloseoutReprintDlg();
            try
            {
                frm_CloseoutReprintDlg.CalledFor = "Reprint";
                frm_CloseoutReprintDlg.ShowDialog();
            }
            finally
            {
                frm_CloseoutReprintDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        // Fetch Data for Blind Tender

        private void GetInitialData()
        {
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
                dtbl1 = objC.FetchBlindTender(CTYPE, COUTID, GeneralFunctions.fnInt32(dr["ID"].ToString()));


                if (CTYPE == "T") blCashFloat = true;
                if (CTYPE == "C")
                {
                    if (objC.IsTerminalConsolidatedCloseout(COUTID))
                    {
                        blCashFloat = true;
                    }
                }

                if (blCashFloat)
                {
                    dblCashFloat = objC.FetchCashFloatAmount(CTYPE, COUTID);
                }



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
                                                   dr["DisplayAs"].ToString(),
                                                   amt,amt,cnt.ToString(),0,dr["Name"].ToString()});
                }
                else
                {
                    if ((dr["Name"].ToString() == "Cash") && (blCashFloat))
                    {
                        dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString(),
                                                   0,amt + dblCashFloat,cnt.ToString(),0-amt-dblCashFloat,dr["Name"].ToString()});
                    }
                    else
                    {
                        dtblCloseout.Rows.Add(new object[] {   dr["ID"].ToString(),
                                                   dr["DisplayAs"].ToString() ,
                                                   0,amt,cnt.ToString(),0-amt-dblCashFloat,dr["Name"].ToString()});
                    }
                }
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            SystemVariables.CurrentUserID = -1;
            SystemVariables.CurrentUserName = "";
            (Window.GetWindow(this) as MainWindow).LoggedInUserTextBlock.Text = Properties.Resources.NoLoggedInUser;
            this.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
            //(Window.GetWindow(this) as MainWindow).LoginSection = LoginSection.None;
            //(Window.GetWindow(this) as MainWindow).LoginControl.txtUser.Text = "";
            //(Window.GetWindow(this) as MainWindow).LoginControl.txtPswd.PasswordBox.Password = "";
            (Window.GetWindow(this) as MainWindow).UpdateLayout();
        }

        private void BtnBD_Click(object sender, RoutedEventArgs e)
        {
            if ((SystemVariables.CurrentUserID > 0) && (!SecurityPermission.AccessCashInOut))
           {
               DocMessage.MsgPermission();
               return;
           }

            blurGrid.Visibility = Visibility.Visible;
            frm_CashInOutDlg frm = new frm_CashInOutDlg();
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void ExecuteReport()
        {

            PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
            objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objCloseoutM.ExecuteCloseoutReportProc(CTYPE, COUTID, Settings.TerminalName, Settings.TaxInclusive,"N");

            if (Settings.DefaultCloseoutPrinter == "Receipt Printer")
            {
                blurGrid.Visibility = Visibility.Visible;
                POSSection.frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new POSSection.frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                    frm_POSInvoicePrintDlg.PrintType = "Closeout";
                    frm_POSInvoicePrintDlg.CloseoutID = COUTID;
                    frm_POSInvoicePrintDlg.CloseoutType = CTYPE;
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
                dtbl1 = objCloseout.ShowTenderRecord("T", COUTID, Settings.TerminalName);

                DataTable dtbl2 = new DataTable("TenderC");
                PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
                objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCloseout1.ShowTenderRecord("C", COUTID, Settings.TerminalName);

                DataTable dtbl3 = new DataTable("TenderOverShort");
                PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
                objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objCloseout2.ShowTenderRecord("R", COUTID, Settings.TerminalName);

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
                if (CTYPE == "C")
                    rep_CloseOut.rType.Text = Properties.Resources.Consolidated;
                if (CTYPE == "T")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Terminal;
                if (CTYPE == "E")
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
                rep_CloseoutMain.COType = CTYPE;
                //rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
                if (CTYPE == "T") rep_CloseoutMain2.tiprow.Visible = false;
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

                rep_CloseoutAdditional.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CloseoutAdditional.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_CloseoutAdditional.xrSubreport1.ReportSource = rep_SalesByHour;
                rep_SalesByHour.DecimalPlace = Settings.DecimalPlace;
                rep_SalesByHour.COType = CTYPE;
                rep_SalesByHour.DataSource = ds5;
                rep_CloseoutAdditional.xrSubreport2.ReportSource = rep_SalesByDept;
                rep_SalesByDept.COType = CTYPE;
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
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

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

        // Check for the access permission of Cash Drawer Opening

        private bool IsOpenCashDrawer()
        {
            bool val = false;
            if (SystemVariables.CurrentUserID <= 0) return true;
            else
            {
                val = SecurityPermission.AcssOpenCashDrawer;
                return val;
            }

        }

        private void BtnFrontOffice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginMenuBorder.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).LoginBorder.Visibility = Visibility.Collapsed;
            (Window.GetWindow(this) as MainWindow).LoginGrid.Visibility = Visibility.Visible;
            (Window.GetWindow(this) as MainWindow).btnFrontOffice.Visibility = Visibility.Hidden;
            (Window.GetWindow(this) as MainWindow).UpdateLayout();
            (Window.GetWindow(this) as MainWindow).GoToFrontOffice();
        }

        // Check if close out can be proceed or not

        private bool IsProceedCloseout()
        {
            if (rg1.IsChecked == true)
            {
                CTYPE = "C";
            }
            if (rg2.IsChecked == true)
            {
                if (Settings.CloseoutOption == 1) CTYPE = "T";
                if (Settings.CloseoutOption == 2) CTYPE = "E";
            }

            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            COUTID = objCloseout.GetCloseOutID("T", CTYPE, SystemVariables.CurrentUserID, Settings.CloseoutOption == 0 ? Settings.TerminalName : lkup.EditValue.ToString());

            //COUTID = 4368;
            if (COUTID == 0) return false;
            else return true;
        }

        // Get if any suspended transaction exists or not before proceeding for closeout

        private string OpenSuspendTranCount()
        {
            string strReturn = "";
            PosDataObject.Closeout objCloseout = new PosDataObject.Closeout();
            objCloseout.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int cnt = objCloseout.OpenSuspndItem(COUTID);
            if (cnt == 0)
            {
                strReturn = "";
            }
            else if (cnt == 1)
            {
                strReturn = Properties.Resources.There_is + cnt.ToString() + Properties.Resources.open_suspended_transaction_exists;
            }
            else
            {
                strReturn = Properties.Resources.There_are + cnt.ToString() + Properties.Resources.open_suspended_transactions_exists;
            }
            return strReturn;
        }

        private void BtnBC_Click(object sender, RoutedEventArgs e)
        {
            if (IsProceedCloseout())
            {
                PosDataObject.Closeout objCloseoutM = new PosDataObject.Closeout();
                objCloseoutM.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCloseoutM.ExecuteCloseoutReportProc(CTYPE, COUTID, Settings.TerminalName, Settings.TaxInclusive,"N");

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
                dtbl1 = objCloseout.ShowTenderRecord("T", COUTID, Settings.TerminalName);

                DataTable dtbl2 = new DataTable("TenderC");
                PosDataObject.Closeout objCloseout1 = new PosDataObject.Closeout();
                objCloseout1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCloseout1.ShowTenderRecord("C", COUTID, Settings.TerminalName);

                DataTable dtbl3 = new DataTable("TenderOverShort");
                PosDataObject.Closeout objCloseout2 = new PosDataObject.Closeout();
                objCloseout2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objCloseout2.ShowTenderRecord("R", COUTID, Settings.TerminalName);

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
                if (CTYPE == "C")
                    rep_CloseOut.rType.Text = Properties.Resources.Consolidated;
                if (CTYPE == "T")
                    rep_CloseOut.rType.Text = Properties.Resources.By_Terminal;
                if (CTYPE == "E")
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
                rep_CloseoutMain.COType = CTYPE;
                //rep_CloseoutMain1.DecimalPlace = Settings.DecimalPlace;
                rep_CloseoutMain2.DecimalPlace = Settings.DecimalPlace;
                if (CTYPE == "T") rep_CloseoutMain2.tiprow.Visible = false;
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
                //rep_CloseOut.xrSubreport5.Visible = false;
                //rep_CloseOut.xrSubreport6.Top = rep_CloseOut.xrSubreport5.Top;
                //rep_CloseOut.xrSubreport7.Top = rep_CloseOut.xrSubreport6.Top + rep_CloseOut.xrSubreport6.Height + 10;

                DataTable dtbl5 = new DataTable("SH");
                DataTable dtbl6 = new DataTable("SD");
                DataSet ds5 = new DataSet();
                DataSet ds6 = new DataSet();

                


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
            else
            {
                DocMessage.MsgInformation(Properties.Resources.No_transaction_found_for_close_out);
            }


        }

        private void BtnRC_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AccessReconcileCount) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            if (Settings.ReceiptPrinterName != "")
            {
                if (IsOpenCashDrawer())
                {
                    try
                    {
                        RawPrinterHelper.OpenCashDrawer1(Settings.ReceiptPrinterName, Settings.CashDrawerCode);
                    }
                    catch
                    {
                    }
                }
            }
            blurGrid.Visibility = Visibility.Visible;
            if (IsProceedCloseout())
            {
                frm_CloseoutCountDlg frm_CloseoutCountDlg = new frm_CloseoutCountDlg();
                try
                {
                    frm_CloseoutCountDlg.CloseoutID = COUTID;
                    frm_CloseoutCountDlg.CloseoutType = CTYPE;
                    frm_CloseoutCountDlg.CountType = "R";
                    frm_CloseoutCountDlg.ShowDialog();
                }
                finally
                {
                    frm_CloseoutCountDlg.Close();
                }

                /*string suspndstr = "";
                suspndstr = OpenSuspendTranCount();
                bool proceed = false;
                if (suspndstr == "") proceed = true;
                if (suspndstr != "")
                {
                    if (DocMessage.MsgConfirmation(suspndstr + "\n" + Properties.Resources.Do_you_want_to_continue_) == MessageBoxResult.Yes)
                    {
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;

                    }
                }
                if (proceed)
                {
                    
                }*/
            }
            else
            {
                DocMessage.MsgInformation(Properties.Resources.No_transaction_found_for_close_out);
            }

            blurGrid.Visibility = Visibility.Collapsed;
        }

        private void Lkup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
