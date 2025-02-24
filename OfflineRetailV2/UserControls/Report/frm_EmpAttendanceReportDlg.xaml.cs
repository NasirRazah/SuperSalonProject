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
    /// Interaction logic for frm_EmpAttendanceReportDlg.xaml
    /// </summary>
    public partial class frm_EmpAttendanceReportDlg : Window
    {
        private int intReportID;
        public int ReportID
        {
            get { return intReportID; }
            set { intReportID = value; }
        }
        public frm_EmpAttendanceReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        public void PopulateEmployee()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            objEmployee.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblEmp = new DataTable();
            dbtblEmp = objEmployee.FetchModifiedLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblEmp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbEmployee.ItemsSource = dtblTemp;
            
            int SelectID = 0;
            foreach (DataRow dr in dbtblEmp.Rows)
            {
                SelectID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                break;
            }
            cmbEmployee.EditValue = SelectID;
            dbtblEmp.Dispose();
        }

        private void PopulateReportCategory()
        {
            DataTable dtblCategory = new DataTable();
            dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblCategory.Rows.Add(new object[] { "Detail", "Detail" });
            dtblCategory.Rows.Add(new object[] { "Summary", "Summary" });
            cmbCategory.ItemsSource = dtblCategory;
            cmbCategory.EditValue = "Detail";
            dtblCategory.Dispose();
        }

        private void GetLateDetailData(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());

            dbtbl = objAttendance.FetchLateData(intID, dtFrom.DateTime, dtTo.DateTime);

            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DEPTNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("STARTDATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("STARTTIME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ShiftDetails", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ShiftStartDate", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ShiftEndDate", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CalculatedTime", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;

            int intTime = 0;
            string strShiftDetail = "";
            TimeSpan ts = new TimeSpan();
            foreach (DataRow dr in dbtbl.Rows)
            {
                incRec = incRec + 1;
                strShiftDetail = dr["ShiftName"].ToString() + "   ( " + dr["ShiftStart"].ToString() +
                                    " - " + dr["ShiftEnd"].ToString() + " )";

                ts = GeneralFunctions.fnDate(dr["STARTDATETIME"].ToString()).Subtract(GeneralFunctions.fnDate(dr["ShiftStartDate"].ToString()));
                intTime = (ts.Days * 24 + ts.Hours) * 60 + ts.Minutes;
                if (intTime > 0)
                    dtbl.Rows.Add(new object[] { dr["EMPCODE"].ToString(),
                                                     dr["EMPNAME"].ToString(),
                                                     dr["DEPTNAME"].ToString(),
                                                     dr["STARTDATE"].ToString(),
                                                     dr["STARTTIME"].ToString(),
                                                     strShiftDetail,
                                                     dr["ShiftStartDate"].ToString(),
                                                     dr["ShiftEndDate"].ToString(),
                                                     intTime.ToString()});


            }
            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Employee.repLateDetails rep_LateDetails = new OfflineRetailV2.Report.Employee.repLateDetails();

            rep_LateDetails.rRange.Text = " From " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_LateDetails);
            rep_LateDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LateDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_LateDetails.Report.DataSource = dtbl;
            rep_LateDetails.GroupHeader1.GroupFields.Add(rep_LateDetails.CreateGroupField("EMPNAME"));

            rep_LateDetails.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_LateDetails.rName.DataBindings.Add("Text", dtbl, "EMPNAME");

            rep_LateDetails.rShift.DataBindings.Add("Text", dtbl, "ShiftDetails");
            rep_LateDetails.rSD.DataBindings.Add("Text", dtbl, "STARTDATE");

            rep_LateDetails.rLT.DataBindings.Add("Text", dtbl, "CalculatedTime");
            rep_LateDetails.rCount.DataBindings.Add("Text", dtbl, "STARTDATE");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_LateDetails.PrinterName = Settings.ReportPrinterName;
                    rep_LateDetails.CreateDocument();
                    rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                    rep_LateDetails.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_LateDetails;
                    window.ShowDialog();

                    //rep_LateDetails.ShowPreviewDialog();


                }
                finally
                {
                    rep_LateDetails.Dispose();
                    // frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_LateDetails.CreateDocument();
                rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_LateDetails);
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_LateDetails.CreateDocument();
                rep_LateDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_late_d.pdf";
                    GeneralFunctions.EmailReport(rep_LateDetails, attachfile, "Employee - Late Details");
                }
                finally
                {
                    rep_LateDetails.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void GetLateSummaryData(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());

            dbtbl = objAttendance.FetchLateData(intID, dtFrom.DateTime, dtTo.DateTime);

            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DEPTNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("RecCount", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;

            int intTime = 0;
            TimeSpan ts = new TimeSpan();

            string CEMPCODE = "";
            string CDEPTNAME = "";
            string CEMPNAME = "";

            string PEMPCODE = "";
            string PDEPTNAME = "";
            string PEMPNAME = "";

            int PrevCount = 0;
            int CurrCount = 0;


            foreach (DataRow dr in dbtbl.Rows)
            {
                incRec = incRec + 1;

                CEMPCODE = dr["EMPCODE"].ToString();
                CEMPNAME = dr["EMPNAME"].ToString();
                CDEPTNAME = dr["DEPTNAME"].ToString();

                ts = GeneralFunctions.fnDate(dr["STARTDATETIME"].ToString()).Subtract(GeneralFunctions.fnDate(dr["ShiftStartDate"].ToString()));
                intTime = (ts.Days * 24 + ts.Hours) * 60 + ts.Minutes;
                if (intTime > 0)
                {
                    CurrCount = 1;
                }
                else
                {
                    CurrCount = 0;
                }

                if (incRec == RecCount)
                {
                    if (PEMPCODE == "")
                    {
                        dtbl.Rows.Add(new object[] { CEMPCODE,
                                                     CEMPNAME,
                                                     CDEPTNAME,
                                                     CurrCount.ToString()});
                    }
                    else
                    {
                        if (CEMPCODE == PEMPCODE)
                        {

                            dtbl.Rows.Add(new object[] { PEMPCODE,
                                                         PEMPNAME,
                                                         PDEPTNAME,(PrevCount+CurrCount).ToString()});
                        }
                        else
                        {


                            dtbl.Rows.Add(new object[] { CEMPCODE,
                                                         CEMPNAME,
                                                         CDEPTNAME,
                                                         CurrCount.ToString()});



                            dtbl.Rows.Add(new object[] { PEMPCODE,
                                                         PEMPNAME,
                                                         PDEPTNAME,PrevCount.ToString()});
                        }
                    }
                }
                else
                {
                    if ((CEMPCODE != PEMPCODE) && (PEMPCODE != ""))
                    {
                        dtbl.Rows.Add(new object[] { PEMPCODE,
                                                     PEMPNAME,
                                                     PDEPTNAME,PrevCount.ToString()});
                        PrevCount = CurrCount;
                    }
                    else
                    {
                        PrevCount = PrevCount + CurrCount;
                    }
                    PDEPTNAME = CDEPTNAME;
                    PEMPCODE = CEMPCODE;
                    PEMPNAME = CEMPNAME;

                }
            }


            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Employee.repLateSummary rep_LateSummary = new OfflineRetailV2.Report.Employee.repLateSummary();

            rep_LateSummary.rRange.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");

            GeneralFunctions.MakeReportWatermark(rep_LateSummary);
            rep_LateSummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LateSummary.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_LateSummary.Report.DataSource = dtbl;

            rep_LateSummary.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_LateSummary.rName.DataBindings.Add("Text", dtbl, "EMPNAME");
            rep_LateSummary.rLT.DataBindings.Add("Text", dtbl, "RecCount");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_LateSummary.PrinterName = Settings.ReportPrinterName;
                    rep_LateSummary.CreateDocument();
                    rep_LateSummary.PrintingSystem.ShowMarginsWarning = false;
                    rep_LateSummary.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_LateSummary;
                    window.ShowDialog();

                    //rep_LateSummary.ShowPreviewDialog();

                }
                finally
                {
                    rep_LateSummary.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_LateSummary.CreateDocument();
                rep_LateSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_LateSummary);
                }
                finally
                {
                    rep_LateSummary.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_LateSummary.CreateDocument();
                rep_LateSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_late_s.pdf";
                    GeneralFunctions.EmailReport(rep_LateSummary, attachfile, "Employee Late Summary");
                }
                finally
                {
                    rep_LateSummary.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private bool GetTaskRecord(PosDataObject.Attendance objReportData, int pID, DateTime pStart, DateTime pEnd)
        {
            int intCount = 0;
            intCount = objReportData.IsTaskFound(pID, pStart, pEnd);
            if (intCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetHolidayRecord(PosDataObject.Attendance objReportData, DateTime pStart)
        {
            int intCount = 0;
            intCount = objReportData.IsHolidayFoundForAbsent(pStart);
            if (intCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsNotAbsent(int pID, DateTime pStart, DateTime pEnd)
        {
            PosDataObject.Attendance objReportData = new PosDataObject.Attendance();
            SqlConnection ReportConn = new SqlConnection(SystemVariables.ConnectionString);
            objReportData.Connection = ReportConn;

            bool blResult = GetTaskRecord(objReportData, pID, pStart, pEnd);
            if (!blResult)
            {
                blResult = GetHolidayRecord(objReportData, pStart);
            }
            ReportConn.Close();
            ReportConn.Dispose();

            return blResult;
        }

        private DataTable GetAbsentData()
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());

            dbtbl = objAttendance.FetchInitialAbsentData(intID);

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ABSENTDATE", System.Type.GetType("System.String"));

            DateTime FormatStartDate = new DateTime(dtFrom.DateTime.Year, dtFrom.DateTime.Month, dtFrom.DateTime.Day, 0, 0, 1);
            DateTime FormatEndDate = new DateTime(dtTo.DateTime.Year, dtTo.DateTime.Month, dtTo.DateTime.Day, 23, 59, 59);

            DateTime pStartDT = Convert.ToDateTime(null);
            DateTime pEndDT = Convert.ToDateTime(null);

            string CEMPID = "";
            string CEMPCODE = "";
            string CEMPNAME = "";
            string CDEPTNAME = "";

            string strAbsentDate = "";
            int intIndex = 0;

            DateTime InitializeStartDT = FormatStartDate;

            foreach (DataRow dr in dbtbl.Rows)
            {
                CEMPID = dr["EMPID"].ToString();
                CEMPCODE = dr["EMPCODE"].ToString();
                CEMPNAME = dr["EMPNAME"].ToString();

                FormatStartDate = InitializeStartDT;

                while (FormatStartDate <= FormatEndDate)
                {
                    pStartDT = new DateTime(FormatStartDate.Year, FormatStartDate.Month, FormatStartDate.Day, 0, 0, 1);
                    pEndDT = new DateTime(FormatStartDate.Year, FormatStartDate.Month, FormatStartDate.Day, 23, 59, 59);


                    if (!IsNotAbsent(GeneralFunctions.fnInt32(CEMPID), pStartDT, pEndDT))
                    {
                        strAbsentDate = Convert.ToString(pStartDT);
                        intIndex = strAbsentDate.IndexOf(" ");
                        strAbsentDate = strAbsentDate.Substring(0, intIndex);

                        dtbl.Rows.Add(new object[] { CEMPCODE, CEMPNAME, strAbsentDate });
                    }
                    FormatStartDate = FormatStartDate.AddDays(1);
                }
            }
            return dtbl;
        }

        private void GetAbsentDetailData(string eventtype)
        {
            DataTable dbtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());
            dbtbl = GetAbsentData();

            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ABSENTDATE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ABSENTCOUNT", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;

            string CEMPCODE = "";
            string CDEPTNAME = "";
            string CEMPNAME = "";

            string PEMPCODE = "";
            string PDEPTNAME = "";
            string PEMPNAME = "";

            string PrevString = "";
            string CurrString = "";
            string GetDate = "";

            int prevCount = 0;
            int currCount = 0;

            foreach (DataRow dr in dbtbl.Rows)
            {
                dtbl.Rows.Add(new object[] { dr["EMPCODE"].ToString(), dr["EMPNAME"].ToString(),
                                             GeneralFunctions.fnDate(dr["ABSENTDATE"]).ToString("MMM dd, yyyy").ToString(),0 });
            }

            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Employee.repAbsentDetails rep_AbsentDetails = new OfflineRetailV2.Report.Employee.repAbsentDetails();

            rep_AbsentDetails.rRange.Text = " From " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_AbsentDetails);
            rep_AbsentDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_AbsentDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_AbsentDetails.Report.DataSource = dtbl;

            rep_AbsentDetails.GroupHeader1.GroupFields.Add(rep_AbsentDetails.CreateGroupField("EMPNAME"));

            rep_AbsentDetails.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_AbsentDetails.rName.DataBindings.Add("Text", dtbl, "EMPNAME");
            rep_AbsentDetails.rLT.DataBindings.Add("Text", dtbl, "ABSENTDATE");
            rep_AbsentDetails.rCount.DataBindings.Add("Text", dtbl, "ABSENTDATE");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_AbsentDetails.PrinterName = Settings.ReportPrinterName;
                    rep_AbsentDetails.CreateDocument();
                    rep_AbsentDetails.PrintingSystem.ShowMarginsWarning = false;
                    rep_AbsentDetails.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_AbsentDetails;
                    window.ShowDialog();

                    //rep_AbsentDetails.ShowPreviewDialog();

                }
                finally
                {
                    rep_AbsentDetails.Dispose();
                    // frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_AbsentDetails.CreateDocument();
                rep_AbsentDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_AbsentDetails);
                }
                finally
                {
                    rep_AbsentDetails.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_AbsentDetails.CreateDocument();
                rep_AbsentDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_absent_d.pdf";
                    GeneralFunctions.EmailReport(rep_AbsentDetails, attachfile, "Employee Absent Details");
                }
                finally
                {
                    rep_AbsentDetails.Dispose();
                    dtbl.Dispose();
                }
            }
        }

        private void GetAbsentSummaryData(string eventtype)
        {
            DataTable dbtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());

            dbtbl = GetAbsentData();

            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("EMPCODE", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMPNAME", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ABSENTCOUNT", System.Type.GetType("System.String"));

            int incRec = 0;
            int RecCount = dbtbl.Rows.Count;

            string CEMPCODE = "";
            string CEMPNAME = "";

            string PEMPCODE = "";
            string PEMPNAME = "";

            int PrevCount = 0;
            int CurrCount = 0;

            foreach (DataRow dr in dbtbl.Rows)
            {
                incRec = incRec + 1;

                CEMPCODE = dr["EMPCODE"].ToString();
                CEMPNAME = dr["EMPNAME"].ToString();

                CurrCount = 1;



                if (incRec == RecCount)
                {
                    if (PEMPCODE == "")
                    {
                        dtbl.Rows.Add(new object[] { CEMPCODE,CEMPNAME,
                                                         CurrCount.ToString()});
                    }
                    else
                    {
                        if (CEMPCODE == PEMPCODE)
                        {
                            dtbl.Rows.Add(new object[] { PEMPCODE,PEMPNAME,
                                                         (PrevCount+CurrCount).ToString()});

                        }
                        else
                        {
                            dtbl.Rows.Add(new object[] { CEMPCODE,CEMPNAME,
                                                         CurrCount.ToString()});


                            dtbl.Rows.Add(new object[] { PEMPCODE,PEMPNAME,
                                                         PrevCount.ToString()});
                        }
                    }
                }
                else
                {
                    if ((CEMPCODE != PEMPCODE) && (PEMPCODE != ""))
                    {
                        dtbl.Rows.Add(new object[] { PEMPCODE,PEMPNAME,
                                                         PrevCount.ToString()});

                        PrevCount = CurrCount;
                    }
                    else
                    {
                        PrevCount = PrevCount + CurrCount;
                    }
                    PEMPCODE = CEMPCODE;
                    PEMPNAME = CEMPNAME;
                }

            }


            dbtbl.Dispose();

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            OfflineRetailV2.Report.Employee.repAbsentSummary rep_AbsentSummary = new OfflineRetailV2.Report.Employee.repAbsentSummary();

            rep_AbsentSummary.rRange.Text = " From " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_AbsentSummary);
            rep_AbsentSummary.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_AbsentSummary.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_AbsentSummary.Report.DataSource = dtbl;

            rep_AbsentSummary.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_AbsentSummary.rName.DataBindings.Add("Text", dtbl, "EMPNAME");
            rep_AbsentSummary.rAbsent.DataBindings.Add("Text", dtbl, "ABSENTCOUNT");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_AbsentSummary.PrinterName = Settings.ReportPrinterName;
                    rep_AbsentSummary.CreateDocument();
                    rep_AbsentSummary.PrintingSystem.ShowMarginsWarning = false;
                    rep_AbsentSummary.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_AbsentSummary;
                    window.ShowDialog();

                    //rep_AbsentSummary.ShowPreviewDialog();

                }
                finally
                {
                    rep_AbsentSummary.Dispose();
                    // frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_AbsentSummary.CreateDocument();
                rep_AbsentSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_AbsentSummary);
                }
                finally
                {
                    rep_AbsentSummary.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_AbsentSummary.CreateDocument();
                rep_AbsentSummary.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_absent_s.pdf";
                    GeneralFunctions.EmailReport(rep_AbsentSummary, attachfile, "Employee Absent Summary");
                }
                finally
                {
                    rep_AbsentSummary.Dispose();
                    dtbl.Dispose();
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

        private void GetAttendanceData(string eventtype)
        {
            PosDataObject.Attendance objAttendance = new PosDataObject.Attendance();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            int intID = 0;
            intID = GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString());

            dtbl = objAttendance.FetchAttendanceData(intID, dtFrom.DateTime, dtTo.DateTime);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            int sftcnt = objAttendance.GetTotalShifts();
            int dsft = objAttendance.GetDefaultShift();

            OfflineRetailV2.Report.Employee.repAttendanceDetails rep_AttendanceDetails = new OfflineRetailV2.Report.Employee.repAttendanceDetails();

            rep_AttendanceDetails.rRange.Text = "from" + " " + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " " + "to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");
            GeneralFunctions.MakeReportWatermark(rep_AttendanceDetails);
            rep_AttendanceDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_AttendanceDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_AttendanceDetails.Report.DataSource = dtbl;
            rep_AttendanceDetails.GroupHeader1.GroupFields.Add(rep_AttendanceDetails.CreateGroupField("EMPNAME"));

            rep_AttendanceDetails.rCode.DataBindings.Add("Text", dtbl, "EMPCODE");
            rep_AttendanceDetails.rName.DataBindings.Add("Text", dtbl, "EMPNAME");

            rep_AttendanceDetails.rShift.DataBindings.Add("Text", dtbl, "Shift");
            rep_AttendanceDetails.rSD.DataBindings.Add("Text", dtbl, "STARTDATE");
            rep_AttendanceDetails.rST.DataBindings.Add("Text", dtbl, "STARTTIME");
            rep_AttendanceDetails.rED.DataBindings.Add("Text", dtbl, "ENDDATE");
            rep_AttendanceDetails.rET.DataBindings.Add("Text", dtbl, "ENDTIME");
            rep_AttendanceDetails.rM.DataBindings.Add("Text", dtbl, "Modified");
            rep_AttendanceDetails.rT.DataBindings.Add("Text", dtbl, "Time");
            rep_AttendanceDetails.rsumT.DataBindings.Add("Text", dtbl, "Time");

            if ((sftcnt == 1) && (dsft == 1))
            {
                //rep_AttendanceDetails.rShift.Visible = rep_AttendanceDetails.rlShift.Visible = false;
                float TempWidth = rep_AttendanceDetails.rlShift.WidthF / 4;
                float T1 = rep_AttendanceDetails.rlStartDate.WidthF;
                float T2 = rep_AttendanceDetails.rlStartTime.WidthF;
                float T3 = rep_AttendanceDetails.rlEndDate.WidthF;
                float T4 = rep_AttendanceDetails.rlEndTime.WidthF;
                try
                {
                    rep_AttendanceDetails.rlShift.WidthF = rep_AttendanceDetails.rlStartDate.WidthF =
                        rep_AttendanceDetails.rlStartTime.WidthF = rep_AttendanceDetails.rlEndDate.WidthF = rep_AttendanceDetails.rlEndTime.WidthF = 0;
                    rep_AttendanceDetails.rlStartDate.WidthF = T1 + TempWidth;
                    rep_AttendanceDetails.rlStartTime.WidthF = T2 + TempWidth;
                    rep_AttendanceDetails.rlEndDate.WidthF = T3 + TempWidth;
                    rep_AttendanceDetails.rlEndTime.WidthF = T4 + TempWidth;

                    rep_AttendanceDetails.rShift.WidthF = rep_AttendanceDetails.rSD.WidthF =
                        rep_AttendanceDetails.rST.WidthF = rep_AttendanceDetails.rED.WidthF = rep_AttendanceDetails.rET.WidthF = 0;
                    rep_AttendanceDetails.rSD.WidthF = T1 + TempWidth;
                    rep_AttendanceDetails.rST.WidthF = T2 + TempWidth;
                    rep_AttendanceDetails.rED.WidthF = T3 + TempWidth;
                    rep_AttendanceDetails.rET.WidthF = T4 + TempWidth;


                    rep_AttendanceDetails.rShift.Visible = rep_AttendanceDetails.rlShift.Visible = false;
                }
                finally
                {
                    rep_AttendanceDetails.EndUpdate();
                }


            }

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_AttendanceDetails.PrinterName = Settings.ReportPrinterName;
                    rep_AttendanceDetails.CreateDocument();
                    rep_AttendanceDetails.PrintingSystem.ShowMarginsWarning = false;
                    rep_AttendanceDetails.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_AttendanceDetails.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_AttendanceDetails;
                    window.ShowDialog();
                }
                finally
                {
                    rep_AttendanceDetails.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_AttendanceDetails.CreateDocument();
                rep_AttendanceDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_AttendanceDetails);
                }
                finally
                {
                    rep_AttendanceDetails.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_AttendanceDetails.CreateDocument();
                rep_AttendanceDetails.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_attn_d.pdf";
                    GeneralFunctions.EmailReport(rep_AttendanceDetails, attachfile, "Employee Attendance");
                }
                finally
                {
                    rep_AttendanceDetails.Dispose();
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
                    if (intReportID == 1)
                    {
                        if (cmbCategory.EditValue.ToString() == "Detail")
                        {
                            GetLateDetailData(eventtype);
                        }
                        else
                        {
                            GetLateSummaryData(eventtype);
                        }
                    }
                    if (intReportID == 3)
                    {
                        if (dtTo.DateTime.Date > DateTime.Today)
                        {
                            DocMessage.MsgInformation("To Date can not be after Today");
                            GeneralFunctions.SetFocus(dtTo);
                            return;
                        }
                        if (cmbCategory.EditValue.ToString() == "Detail")
                        {
                            GetAbsentDetailData(eventtype);
                        }
                        else
                        {
                            GetAbsentSummaryData(eventtype);
                        }
                    }

                    if (intReportID == 2) // Attendance
                    {
                        if (dtTo.DateTime.Date > DateTime.Today)
                        {
                            DocMessage.MsgInformation("To Date can not be after Today");
                            GeneralFunctions.SetFocus(dtTo);
                            return;
                        }
                        GetAttendanceData(eventtype);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private async void LbEmployee_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frm_Lookups frm_Lookups = new frm_Lookups();
            try
            {
                frm_Lookups.Print = "N";
                frm_Lookups.SearchType = "Employee";
                frm_Lookups.ShowDialog();
                if (await frm_Lookups.GetID() > 0)
                {
                    cmbEmployee.EditValue = (await frm_Lookups.GetID()).ToString();
                }
            }
            finally
            {
                frm_Lookups.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateReportCategory();
            if (intReportID == 1)
            {
                Title.Text = " Late Report";
            }
            if (intReportID == 2)
            {
                Title.Text = " Attendance Report";
                pcBottom.Visibility = Visibility.Collapsed;
                pcBottom.Height = 1;
                this.Height = 275;
            }
            if (intReportID == 3)
            {
                Title.Text = " Absent Report";
            }
            PopulateEmployee();
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;

            if (intReportID == 2) cmbCategory.IsEnabled = false;
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

        private void CmbEmployee_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbCategory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
