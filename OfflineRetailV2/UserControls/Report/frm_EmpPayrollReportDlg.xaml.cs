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
using System.IO;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_EmpPayrollReportDlg.xaml
    /// </summary>
    public partial class frm_EmpPayrollReportDlg : Window
    {
        private string strExportFlag;

        public string ExportFlag
        {
            get { return strExportFlag; }
            set { strExportFlag = value; }
        }

        public frm_EmpPayrollReportDlg()
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
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objAttendance.FetchPayrollDataForReport(dtFrom.DateTime, dtTo.DateTime);

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PCALCULATERATE", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;


            string CEMPCODE = "";
            string CEMPNAME = "";
            string CEMPRATE = "";
            int CCALCULATETIME = 0;
            double CCALCULATERATE = 0;

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
                                                     PEMPCODE,
                                                     PEMPNAME,PEMPRATE,
                                                     (CCALCULATETIME + PrevCalculateTime).ToString(),
                                                     (CCALCULATERATE+ PrevCalculateRate).ToString()});
                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] {
                                                     CEMPCODE,
                                                     CEMPNAME,CEMPRATE,
                                                     CCALCULATETIME.ToString(),CCALCULATERATE.ToString()});

                            dtbl.Rows.Add(new object[] {
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

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Employee.repPayroll rep_Payroll = new OfflineRetailV2.Report.Employee.repPayroll();

            rep_Payroll.rRange.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_Payroll);
            rep_Payroll.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Payroll.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Payroll.DecimalPlace = Settings.DecimalPlace;
            rep_Payroll.Report.DataSource = dtbl;

            rep_Payroll.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_Payroll.rName.DataBindings.Add("Text", dtbl, "EMPNAME");
            rep_Payroll.rTime.DataBindings.Add("Text", dtbl, "PCALCULATETIME");
            rep_Payroll.rRate.DataBindings.Add("Text", dtbl, "EMPRATE");
            rep_Payroll.rTotRate.DataBindings.Add("Text", dtbl, "PCALCULATERATE");

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
                    // frm_PreviewControl.dv.DocumentSource = null;
                    //  frm_PreviewControl.Dispose();

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
                    attachfile = "payroll.pdf";
                    GeneralFunctions.EmailReport(rep_Payroll, attachfile, "Employee Payroll");
                }
                finally
                {
                    rep_Payroll.Dispose();
                    dtbl.Dispose();
                }
            }
        }

        private void ExportPayroll()
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objAttendance.FetchPayrollDataForExport(dtFrom.DateTime, dtTo.DateTime);

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPRATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PCALCULATETIME", System.Type.GetType("System.String"));
            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;


            string CEMPCODE = "";
            string CEMPNAME = "";
            string CEMPRATE = "";
            int CCALCULATETIME = 0;
            double CCALCULATERATE = 0;

            string PEMPCODE = "";
            string PEMPNAME = "";
            string PEMPRATE = "";
            int PrevCalculateTime = 0;
            double PrevCalculateRate = 0;

            double CCASHTIP = 0;
            double CCCTIP = 0;
            double PCASHTIP = 0;
            double PCCTIP = 0;

            double CTOTTIP = 0;
            double PTOTTIP = 0;

            foreach (DataRow dr in dbtbl.Rows)
            {
                double tip = 0;
                double dtip = 0;
                incRec = incRec + 1;
                CEMPCODE = dr["EMPCODE"].ToString();
                CEMPNAME = dr["EMPNAME"].ToString();
                CEMPRATE = dr["EMPRATE"].ToString();

                CCALCULATETIME = GeneralFunctions.fnInt32(dr["PCALCULATETIME"].ToString());
                CCALCULATERATE = GeneralFunctions.fnDouble(dr["PCALCULATERATE"].ToString());

                if (incRec == RecCount)
                {
                    if (PEMPCODE == "")
                    {
                        dtbl.Rows.Add(new object[] {
                                                     CEMPCODE,
                                                     CEMPNAME,
                                                     CEMPRATE,
                                                     CCALCULATETIME.ToString()});
                    }
                    else
                    {
                        if (CEMPCODE == PEMPCODE)
                        {
                            dtbl.Rows.Add(new object[] {
                                                     PEMPCODE,
                                                     PEMPNAME,PEMPRATE,
                                                     (CCALCULATETIME + PrevCalculateTime).ToString()

                                                     });
                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] {
                                                     CEMPCODE,
                                                     CEMPNAME,CEMPRATE,
                                                     CCALCULATETIME.ToString()});

                            dtbl.Rows.Add(new object[] { PEMPCODE,
                                                     PEMPNAME,PEMPRATE,
                                                     PrevCalculateTime.ToString()});
                        }
                    }
                }
                else
                {
                    if ((CEMPCODE != PEMPCODE) && (PEMPCODE != ""))
                    {

                        dtbl.Rows.Add(new object[] { PEMPCODE,
                                                     PEMPNAME, PEMPRATE,
                                                     PrevCalculateTime.ToString()});
                        PrevCalculateTime = CCALCULATETIME;
                        PrevCalculateRate = CCALCULATERATE;

                    }
                    else
                    {
                        PrevCalculateTime = PrevCalculateTime + CCALCULATETIME;
                        PrevCalculateRate = PrevCalculateRate + CCALCULATERATE;
                    }

                    PEMPCODE = CEMPCODE;
                    PEMPNAME = CEMPNAME;
                    PEMPRATE = CEMPRATE;
                }
            }
            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Export.");
                return;
            }

            DataTable dtblE = new DataTable();
            dtblE.Columns.Add("Emp_Code", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Emp_Name", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Start_Date", System.Type.GetType("System.String"));
            dtblE.Columns.Add("End_Date", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Reg_Hour", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Reg_Hour_Rate", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Reg_Hour_Amt", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Overtime", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Overtime_Rate", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Overtime_Amt", System.Type.GetType("System.String"));
            dtblE.Columns.Add("Total_Amt", System.Type.GetType("System.String"));


            foreach (DataRow dr in dtbl.Rows)
            {
                string NHr = "";
                string NRate = "";
                string NAmt = "";
                string OHr = "";
                string ORate = "";
                string OAmt = "";
                string TAmt = "";
                CalculateExportData(GeneralFunctions.fnInt32(dr["PCALCULATETIME"].ToString()), GeneralFunctions.fnDouble(dr["EMPRATE"].ToString()),
                    ref NHr, ref NRate, ref NAmt, ref OHr, ref ORate, ref OAmt, ref TAmt);
                dtblE.Rows.Add(new object[] { dr["EMPCODE"].ToString(),dr["EMPNAME"].ToString(),dtFrom.DateTime.ToString(SystemVariables.DateFormat),dtTo.DateTime.ToString(SystemVariables.DateFormat),
                                                     NHr,NRate,NAmt,OHr,ORate,OAmt,TAmt });
            }

            int rtn = 0;
            string srFile = ExpFileName();
            if (dtblE.Rows.Count > 0)
            {

                StreamWriter writer = new StreamWriter(srFile);
                StringBuilder builder = new StringBuilder();
                try
                {
                    int prevlength = 0;
                    string sepChar = ",";

                    string sep = "";

                    foreach (DataColumn dc in dtblE.Columns)
                    {
                        builder.Append(sep).Append(dc.ColumnName);
                        sep = sepChar;
                    }
                    writer.WriteLine(builder.ToString());
                    prevlength = builder.Length;


                    foreach (DataRow drE in dtblE.Rows)
                    {
                        sep = "";
                        //builder.Remove(0, builder.Length);
                        foreach (DataColumn dc1 in dtblE.Columns)
                        {
                            builder.Append(sep).Append(drE[dc1.ColumnName]);
                            sep = sepChar;

                        }
                        writer.WriteLine(builder.ToString(prevlength, builder.Length - prevlength));
                        prevlength = builder.Length;

                    }
                    writer.Close();

                }
                catch
                {
                    rtn = 2;
                }
            }
            else
            {
                rtn = 1;
            }

            if (rtn == 0)
            {
                DocMessage.MsgInformation("Payroll exported successfully.");
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = srFile;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DocMessage.MsgError("Cannot find an application on your system suitable for openning the file with exported data.");
                }

            }
            if (rtn == 1)
            {
                DocMessage.MsgInformation("No data found for Export.");
            }
            if (rtn == 2)
            {
                DocMessage.MsgInformation("Permission error while exporting...");
            }
        }

        private string ExpFileName()
        {
            string strExportDir = GetExpDir();
            string tempfile = DateTime.Now.ToString("MMddyy") + ".csv";

            string expf = strExportDir + "\\" + tempfile;
            return expf;
        }

        private string GetExpDir()
        {
            string csConnPath = "";
            string strdirpath = "";
            csConnPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (csConnPath.EndsWith("\\"))
            {
                strdirpath = csConnPath + SystemVariables.BrandName;
            }
            else
            {
                strdirpath = csConnPath + "\\" + SystemVariables.BrandName;
            }
            if (!Directory.Exists(strdirpath))
            {
                Directory.CreateDirectory(strdirpath);
            }
            return strdirpath;
        }


        private void CalculateExportData(int intTotalTime, double dblEmpRate, ref string NHr, ref string NRate, ref string NAmt,
            ref string OHr, ref string ORate, ref string OAmt, ref string TAmt)
        {
            int intTaskTime = 0;
            int intNTaskTime = 0;
            int intOTaskTime = 0;
            intTaskTime = intTotalTime;
            int taskHH = 0;
            int cM = 0;
            int cH = 0;
            int coM = 0;
            int coH = 0;
            double HRate = 0;
            double MRate = 0;
            double OHRate = 0;
            double OMRate = 0;


            int NormalHour = 0;
            NormalHour = Settings.RegularHoursPerWeek;

            double OverTimeFactor = Settings.OverTimeFactor;

            if (intTaskTime > NormalHour * 60)
            {
                intNTaskTime = NormalHour * 60;
                intOTaskTime = intTaskTime - NormalHour * 60;
            }
            else
            {
                intNTaskTime = intTaskTime;
                intOTaskTime = 0;
            }

            if (dblEmpRate == 0)
            {
                HRate = 0;
                MRate = 0;
                OHRate = 0;
                OMRate = 0;

            }
            else
            {
                HRate = dblEmpRate;
                MRate = dblEmpRate / 60;
                OHRate = dblEmpRate * OverTimeFactor;
                OMRate = dblEmpRate * OverTimeFactor / 60;
            }

            if (intNTaskTime > 0)
            {
                if (intNTaskTime < 60)
                {
                    cM = intTaskTime;
                }
                else
                {
                    taskHH = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble(intNTaskTime / 60)));
                    if (intNTaskTime - taskHH * 60 == 0)
                    {
                        cH = taskHH;
                    }
                    else
                    {
                        cH = taskHH;
                        cM = intNTaskTime - taskHH * 60;
                    }
                }
            }

            double TP = cH * HRate + cM * MRate;


            if (intOTaskTime > 0)
            {
                if (intOTaskTime < 60)
                {
                    coM = intOTaskTime;
                }
                else
                {
                    taskHH = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble(intOTaskTime / 60)));
                    if (intOTaskTime - taskHH * 60 == 0)
                    {
                        coH = taskHH;
                    }
                    else
                    {
                        coH = taskHH;
                        coM = intOTaskTime - taskHH * 60;
                    }
                }
            }

            double OTP = coH * OHRate + coM * OMRate;

            // format data


            string sH = cH.ToString();
            string sM = cM.ToString();

            if (sH == "0") sH = "00";
            if (sH.Length == 1) sH = sH.PadLeft(2, '0');

            if (sM == "0") sM = "00";
            if (sM.Length == 1) sM = sM.PadLeft(2, '0');

            NHr = sH + ":" + sM;

            string soH = coH.ToString();
            string soM = coM.ToString();

            if (soH == "0") soH = "00";
            if (soH.Length == 1) soH = soH.PadLeft(2, '0');

            if (soM == "0") sM = "00";
            if (soM.Length == 1) soM = soM.PadLeft(2, '0');

            OHr = soH + ":" + soM;

            if (Settings.DecimalPlace == 3)
            {
                NRate = HRate.ToString("f3");
                NAmt = TP.ToString("f3");
                ORate = OHRate.ToString("f3");
                OAmt = OTP.ToString("f3");
                TAmt = (TP + OTP).ToString("f3");
            }
            else
            {
                NRate = HRate.ToString("f");
                NAmt = TP.ToString("f");
                ORate = OHRate.ToString("f");
                OAmt = OTP.ToString("f");
                TAmt = (TP + OTP).ToString("f");
            }
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (CheckBlankDate())
                {
                    if (strExportFlag == "N") GetReportData(eventtype);
                    if (strExportFlag == "Y") ExportPayroll();
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtFrom.DateTime = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
            dtTo.DateTime = dtFrom.DateTime.AddMonths(1).AddDays(-1);

            if (strExportFlag == "Y")
            {
                Title.Text = "Payroll Export";
                btnPrint.Content = "EXPORT";
                btnPrint.Style = this.FindResource("SaveButtonStyle") as Style;
                btnEmail.Visibility = Visibility.Collapsed;
                btnPreview.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title.Text = "Payroll Report";
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
