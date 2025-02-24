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
    /// Interaction logic for frm_EmpWageToSalesReportDlg.xaml
    /// </summary>
    public partial class frm_EmpWageToSalesReportDlg : Window
    {
        public frm_EmpWageToSalesReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
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

        private void GetReportData(string eventtype)
        {

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            PosDataObject.Sales objAttendance = new PosDataObject.Sales();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objAttendance.FetchEmployeeForWageToSalesReport(dtFrom.DateTime, dtTo.DateTime);

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;

            int CEMPID = 0;
            string CEMPCODE = "";
            string CEMPNAME = "";
            string CEMPRATE = "";
            int CCALCULATETIME = 0;
            double CCALCULATERATE = 0;

            int PEMPID = 0;
            string PEMPCODE = "";
            string PEMPNAME = "";
            string PEMPRATE = "";
            int PCALCULATETIME = 0;
            double PCALCULATERATE = 0;
            int ECalculateTime = 0;
            int PrevCalculateTime = 0;
            double PrevCalculateRate = 0;

            foreach (DataRow dr in dbtbl.Rows)
            {
                incRec = incRec + 1;
                CEMPID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                CEMPCODE = dr["EMPCODE"].ToString();
                CEMPNAME = dr["EMPNAME"].ToString();
                CEMPRATE = dr["EMPRATE"].ToString();

                //if (dr["PCALCULATETIME"].ToString() == "")
                //CCALCULATETIME = 0;
                //else
                CCALCULATETIME = GeneralFunctions.fnInt32(dr["PCALCULATETIME"].ToString());
                //if (dr["PCALCULATERATE"].ToString() == "")
                //CCALCULATERATE = 0;
                //else
                CCALCULATERATE = GeneralFunctions.fnDouble(dr["PCALCULATERATE"].ToString());

                if (incRec == RecCount)
                {
                    if (PEMPCODE == "")
                    {
                        dtbl.Rows.Add(new object[] {
                                                     true,
                                                     CEMPID,
                                                     CEMPCODE,
                                                     CEMPNAME,
                                                     CEMPRATE,
                                                     CCALCULATETIME.ToString(),
                                                     CCALCULATERATE.ToString()});
                    }
                    else
                    {
                        if (CEMPCODE == PEMPCODE)
                        {
                            dtbl.Rows.Add(new object[] {
                                                     true,
                                                     PEMPID,
                                                     PEMPCODE,
                                                     PEMPNAME,PEMPRATE,
                                                     (CCALCULATETIME + PrevCalculateTime).ToString(),
                                                     (CCALCULATERATE+ PrevCalculateRate).ToString()});
                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] {
                                                     true,
                                                     CEMPID,
                                                     CEMPCODE,
                                                     CEMPNAME,CEMPRATE,
                                                     CCALCULATETIME.ToString(),CCALCULATERATE.ToString()});

                            dtbl.Rows.Add(new object[] {
                                                     true,
                                                     PEMPID,
                                                     PEMPCODE,
                                                     PEMPNAME,PEMPRATE,
                                                     PrevCalculateTime.ToString(),PrevCalculateRate.ToString()});
                        }
                    }
                }
                else
                {
                    if ((CEMPCODE != PEMPCODE) && (PEMPCODE != ""))
                    {
                        dtbl.Rows.Add(new object[] {
                                                     true,
                                                     PEMPID,
                                                     PEMPCODE,
                                                     PEMPNAME, PEMPRATE,
                                                     PrevCalculateTime.ToString(),PrevCalculateRate.ToString()});
                        PrevCalculateTime = CCALCULATETIME;
                        PrevCalculateRate = CCALCULATERATE;
                    }
                    else
                    {
                        PrevCalculateTime = PrevCalculateTime + CCALCULATETIME;
                        PrevCalculateRate = PrevCalculateRate + CCALCULATERATE;
                    }
                    PEMPID = CEMPID;
                    PEMPCODE = CEMPCODE;
                    PEMPNAME = CEMPNAME;
                    PEMPRATE = CEMPRATE;
                }
            }
            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                return;
            }


            DataTable dtblS = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            dtblS = objSales.FetchSaleReportData("Employee", dtbl, GeneralFunctions.fnDate(dtFrom.EditValue),
                                                GeneralFunctions.fnDate(dtTo.EditValue), "");

            DataTable repdtbl = new DataTable();
            repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Description", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Brand", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("UPC", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("Season", System.Type.GetType("System.String"));

            repdtbl.Columns.Add("QtySold", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
            repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));

            repdtbl.Columns.Add("FilterID", System.Type.GetType("System.String"));
            repdtbl.Columns.Add("FilterDesc", System.Type.GetType("System.String"));

            double dblRev = 0;
            double dblCost = 0;
            double dblPRev = 0;
            double dblPCost = 0;
            double dblProfit = 0;
            double dblMargin = 0;
            double dblPQty = 0;
            double dblRD = 0;

            foreach (DataRow dr in dtblS.Rows)
            {
                bool blExists = false;
                foreach (DataRow dr1 in repdtbl.Rows)
                {
                    if ((dr1["SKU"].ToString() == dr["SKU"].ToString()) &&
                        (dr1["Description"].ToString() == dr["Description"].ToString()) &&
                        (dr1["FilterID"].ToString() == dr["FilterID"].ToString()))
                    {
                        blExists = true;
                        break;
                    }
                }

                if (!blExists)
                {
                    dblRev = 0;
                    dblCost = 0;
                    dblRev = (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                    - GeneralFunctions.fnDouble(dr["Discount"].ToString());
                    dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());


                    repdtbl.Rows.Add(new object[] {dr["SKU"].ToString(),
                                                   dr["Description"].ToString(),
                                                   dr["Brand"].ToString(),
                                                   dr["UPC"].ToString(),
                                                   dr["Season"].ToString(),
                                                   GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                   dblRev,
                                                   dblCost,
                                                   0,0,
                                                   dr["FilterID"].ToString(),
                                                   dr["FilterDesc"].ToString()});
                }
                else
                {
                    foreach (DataRow dr2 in repdtbl.Rows)
                    {
                        if ((dr2["SKU"].ToString() == dr["SKU"].ToString()) &&
                            (dr2["Description"].ToString() == dr["Description"].ToString()) &&
                            (dr2["FilterID"].ToString() == dr["FilterID"].ToString()))
                        {
                            dblRev = 0;
                            dblCost = 0;
                            dblRev = (GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Price"].ToString()))
                                - GeneralFunctions.fnDouble(dr["Discount"].ToString());
                            dblCost = GeneralFunctions.fnDouble(dr["Qty"].ToString()) * GeneralFunctions.fnDouble(dr["Cost"].ToString());

                            dblPRev = GeneralFunctions.fnDouble(dr2["Revenue"].ToString()) + dblRev;
                            dblPCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString()) + dblCost;
                            dblPQty = GeneralFunctions.fnDouble(dr2["QtySold"].ToString()) + GeneralFunctions.fnDouble(dr["Qty"].ToString());
                            dr2["Revenue"] = dblPRev;
                            dr2["Cost"] = dblPCost;
                            dr2["QtySold"] = dblPQty;
                        }
                    }
                }
            }

            foreach (DataRow dr3 in repdtbl.Rows)
            {
                dblProfit = 0;
                dblMargin = 0;
                if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                {
                    dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                    dblMargin = -99999;
                }
                else
                {
                    dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                    dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                }

                dr3["Profit"] = dblProfit;
                dr3["Margin"] = dblMargin;
            }

            DataTable dtblR = new DataTable();

            dtblR.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtblR.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtblR.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
            dtblR.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
            dtblR.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));
            dtblR.Columns.Add("SALES", System.Type.GetType("System.String"));
            dtblR.Columns.Add("PERCENTAGE", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl.Rows)
            {
                double salesval = 0;
                double wageval = 0;
                string ratio = "";
                foreach (DataRow dr1 in repdtbl.Rows)
                {
                    if (dr["EMPCODE"].ToString() == dr1["FILTERID"].ToString())
                    {
                        salesval = salesval + GeneralFunctions.fnDouble(dr1["Revenue"].ToString());
                    }
                }
                if (GeneralFunctions.fnDouble(dr["PCALCULATERATE"].ToString()) == 0) ratio = "";
                else if (salesval == 0) ratio = "";
                else ratio = ((GeneralFunctions.fnDouble(dr["PCALCULATERATE"].ToString()) * 100) / salesval).ToString("f");

                dtblR.Rows.Add(new object[] {dr["EMPCODE"].ToString(), dr["EMPNAME"].ToString(), dr["EMPRATE"].ToString(),
                                             dr["PCALCULATETIME"].ToString(),dr["PCALCULATERATE"].ToString(),salesval.ToString("f"),ratio});
            }

            OfflineRetailV2.Report.Employee.repEmployeeWageToSales rep_Payroll = new OfflineRetailV2.Report.Employee.repEmployeeWageToSales();

            rep_Payroll.rRange.Text = " From " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_Payroll);
            rep_Payroll.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Payroll.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Payroll.DecimalPlace = Settings.DecimalPlace;
            rep_Payroll.Report.DataSource = dtblR;

            rep_Payroll.rCode.DataBindings.Add("Text", dtblR, "EMPCODE");
            rep_Payroll.rName.DataBindings.Add("Text", dtblR, "EMPNAME");
            rep_Payroll.rTime.DataBindings.Add("Text", dtblR, "PCALCULATETIME");
            rep_Payroll.rRate.DataBindings.Add("Text", dtblR, "EMPRATE");
            rep_Payroll.rTotRate.DataBindings.Add("Text", dtblR, "PCALCULATERATE");
            rep_Payroll.rSales.DataBindings.Add("Text", dtblR, "SALES");
            rep_Payroll.rRatio.DataBindings.Add("Text", dtblR, "PERCENTAGE");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_Payroll.PrinterName = Settings.ReportPrinterName;
                    rep_Payroll.CreateDocument();
                    rep_Payroll.PrintingSystem.ShowMarginsWarning = false;
                    rep_Payroll.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_Payroll.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_Payroll;
                    window.ShowDialog();
                }
                finally
                {
                    rep_Payroll.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }

            }

            if (eventtype == "Print")
            {
                rep_Payroll.CreateDocument();
                rep_Payroll.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_Payroll);
                }
                finally
                {
                    rep_Payroll.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_Payroll.CreateDocument();
                rep_Payroll.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "empwagetosales.pdf";
                    GeneralFunctions.EmailReport(rep_Payroll, attachfile, "Employee Wages to Sales");
                }
                finally
                {
                    rep_Payroll.Dispose();
                    dtbl.Dispose();
                }
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Employee Wage To Sales Analysis";
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
