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
using DevExpress.XtraCharts;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_DashboardReportDlg.xaml
    /// </summary>
    public partial class frm_DashboardReportDlg : Window
    {
        public frm_DashboardReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void ExecuteReport(string eventtype)
        {
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            int exe = objSales.ExecDashboardReport_Date(Settings.TerminalName, dtFrom.DateTime.Date, dtTo.DateTime.Date, Settings.TaxInclusive);

            DataTable dtbl = objSales.FetchDashboardReportData(Settings.TerminalName);

            DataTable dtblSH = new DataTable();

            dtblSH.Columns.Add("Hour", System.Type.GetType("System.String"));
            dtblSH.Columns.Add("Invoice", System.Type.GetType("System.Int32"));
            dtblSH.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSH.Columns.Add("Avg", System.Type.GetType("System.Double"));

            DataRow[] DR1 = dtbl.Select("RecordType = 'BY HOUR' ");

            foreach (DataRow dr in DR1)
            {
                if (GeneralFunctions.fnInt32(dr["RecordCount"].ToString()) > 0)
                {
                    dtblSH.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnInt32(dr["RecordCount"].ToString()), GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())),
                    GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())/GeneralFunctions.fnInt32(dr["RecordCount"].ToString()))});
                }
            }

            DataTable dtblSC = new DataTable();

            dtblSC.Columns.Add("Category", System.Type.GetType("System.String"));
            dtblSC.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSC.Columns.Add("Percent", System.Type.GetType("System.Double"));
            dtblSC.Columns.Add("Color", System.Type.GetType("System.String"));

            DataRow[] DR2 = dtbl.Select("RecordType = 'BY CATEGORY' ");

            double TotalCatAmount = 0;
            foreach (DataRow dr in DR2)
            {
                dtblSC.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), 0, "" });
                TotalCatAmount = TotalCatAmount + GeneralFunctions.fnDouble(dr["RecordAmount"].ToString());
            }



            dtblSC.DefaultView.Sort = "Category asc";
            dtblSC.DefaultView.ApplyDefaultSort = true;


            DataTable dtblSD = new DataTable();

            dtblSD.Columns.Add("Department", System.Type.GetType("System.String"));
            dtblSD.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblSD.Columns.Add("Percent", System.Type.GetType("System.Double"));
            DataRow[] DR10 = dtbl.Select("RecordType = 'BY DEPARTMENT' ");

            double TotalDeptAmount = 0;
            foreach (DataRow dr in DR10)
            {
                dtblSD.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), 0 });
                TotalDeptAmount = TotalDeptAmount + GeneralFunctions.fnDouble(dr["RecordAmount"].ToString());
            }



            dtblSD.DefaultView.Sort = "Department asc";
            dtblSD.DefaultView.ApplyDefaultSort = true;

            DataTable dtblT = new DataTable();

            dtblT.Columns.Add("TenderType", System.Type.GetType("System.String"));
            dtblT.Columns.Add("Amount", System.Type.GetType("System.Double"));
            dtblT.Columns.Add("Color", System.Type.GetType("System.String"));

            DataRow[] DR3 = dtbl.Select("RecordType = 'TENDER' ");

            foreach (DataRow dr in DR3)
            {
                dtblT.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()), "" });
            }

            DataTable dtblGC = new DataTable();
            dtblGC.Columns.Add("Qty", System.Type.GetType("System.Int32"));
            dtblGC.Columns.Add("Total", System.Type.GetType("System.Double"));
            dtblGC.Columns.Add("Avg", System.Type.GetType("System.Double"));


            DataRow[] DR4 = dtbl.Select("RecordType = 'GC' ");

            foreach (DataRow dr in DR4)
            {
                dtblGC.Rows.Add(new object[] { GeneralFunctions.fnInt32(dr["RecordCount"].ToString()), GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()),
                GeneralFunctions.fnInt32(dr["RecordCount"].ToString()) == 0 ? 0 : GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["RecordAmount"].ToString()) / GeneralFunctions.fnInt32(dr["RecordCount"].ToString()))});
            }


            DataTable dtblTx = new DataTable();
            dtblTx.Columns.Add("Tax", System.Type.GetType("System.String"));
            dtblTx.Columns.Add("Taxable", System.Type.GetType("System.Double"));
            dtblTx.Columns.Add("Collected", System.Type.GetType("System.Double"));

            DataRow[] DR5 = dtbl.Select("RecordType = 'TAX' ");
            DataRow[] DR6 = dtbl.Select("RecordType = 'TAXABLE' ");

            foreach (DataRow dr in DR5)
            {
                foreach (DataRow dr1 in DR6)
                {
                    if (GeneralFunctions.fnInt32(dr["RecordSL"].ToString()) == GeneralFunctions.fnInt32(dr1["RecordSL"].ToString()))
                    {
                        dtblTx.Rows.Add(new object[] { dr["RecordDescription"].ToString(), GeneralFunctions.fnDouble(dr1["RecordAmount"].ToString()),
                        GeneralFunctions.fnDouble(dr["RecordAmount"].ToString())});
                        break;
                    }
                }
            }

            OfflineRetailV2.Report.Sales.repDashboard repM = new OfflineRetailV2.Report.Sales.repDashboard();
            GeneralFunctions.MakeReportWatermark(repM);
            OfflineRetailV2.Report.Sales.repDashboard_SH rep_SHS = new OfflineRetailV2.Report.Sales.repDashboard_SH();
            OfflineRetailV2.Report.Sales.repDashboard_SC rep_SCS = new OfflineRetailV2.Report.Sales.repDashboard_SC();
            OfflineRetailV2.Report.Sales.repDashboard_SD rep_SDS = new OfflineRetailV2.Report.Sales.repDashboard_SD();
            OfflineRetailV2.Report.Sales.repDashboard_T rep_T = new OfflineRetailV2.Report.Sales.repDashboard_T();
            OfflineRetailV2.Report.Sales.repDashboard_GC rep_GC = new OfflineRetailV2.Report.Sales.repDashboard_GC();
            OfflineRetailV2.Report.Sales.repDashboard_TX rep_TX = new OfflineRetailV2.Report.Sales.repDashboard_TX();

            repM.srepSC.ReportSource = rep_SCS;
            repM.srepSH.ReportSource = rep_SHS;
            repM.srepSD.ReportSource = rep_SDS;

            repM.srepT.ReportSource = rep_T;
            repM.srepGC.ReportSource = rep_GC;
            repM.srepTX.ReportSource = rep_TX;
            GeneralFunctions.MakeReportWatermark(repM);
            repM.rDate.Text = "from" + " " + dtFrom.DateTime.Date.ToShortDateString() + " " + "to" + " " + dtTo.DateTime.Date.ToShortDateString();
            repM.rHeader.Text = "Dashboard - Sales";
            repM.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            repM.rReportHeader.Text = Settings.ReportHeader_Address;


            foreach (DataRow dr in dtblSH.Rows)
            {
                rep_SHS.xrChart1.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Hour"].ToString(), new double[] { 0, GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            foreach (DataRow dr in dtblSH.Rows)
            {
                rep_SHS.xrChart1.Series[1].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Hour"].ToString(), new double[] { 0, GeneralFunctions.fnDouble(dr["Invoice"].ToString()) }));
            }

            rep_SHS.xrChart1.Series[0].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SHS.xrChart1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

            rep_SHS.xrChart1.Series[1].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SHS.xrChart1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

            string resource1 = "Hourly Sales";
            string resource2 = "No. of Invoices";
            rep_SHS.xrChart1.Series[0].Name = "Hourly Sales";
            rep_SHS.xrChart1.Series[1].Name = "No. of Invoices";

            rep_SHS.xrChart1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            rep_SHS.xrChart1.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            string resource5 = "Hour";

            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Hour";
            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Sales";

            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.GanttDiagram).AxisY.Label.NumericOptions.Precision = 2;
            (rep_SHS.xrChart1.Diagram as DevExpress.XtraCharts.GanttDiagram).AxisY.Label.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;

            rep_SHS.Report.DataSource = dtblSH;
            rep_SHS.rHour.DataBindings.Add("Text", dtblSH, "Hour");
            rep_SHS.rSales.DataBindings.Add("Text", dtblSH, "Amount");
            rep_SHS.rInv.DataBindings.Add("Text", dtblSH, "Invoice");
            rep_SHS.rAvg.DataBindings.Add("Text", dtblSH, "Avg");
            rep_SHS.rTSales.DataBindings.Add("Text", dtblSH, "Amount");
            rep_SHS.rTInv.DataBindings.Add("Text", dtblSH, "Invoice");


            foreach (DataRow dr in dtblSD.Rows)
            {
                dr["Percent"] = GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString()) * 100 / TotalCatAmount).ToString());
            }

            Double TPercent1 = 0;
            foreach (DataRow dr in dtblSD.Rows)
            {
                TPercent1 = TPercent1 + GeneralFunctions.fnDouble(dr["Percent"].ToString());
            }

            double var1 = 100 - TPercent1;
            if (var1 != 0)
            {
                foreach (DataRow dr in dtblSD.Rows)
                {
                    dr["Percent"] = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Percent"].ToString()) + var1);
                    break;
                }
            }


            rep_SDS.xrChart1.Series.Clear();
            rep_SDS.xrChart1.Series.Add("", DevExpress.XtraCharts.ViewType.Bar);
            foreach (DataRow dr in dtblSD.Rows)
            {
                rep_SDS.xrChart1.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Department"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));

            }
            ((SideBySideBarSeriesView)rep_SDS.xrChart1.Series[0].View).ColorEach = true;

            rep_SDS.xrChart1.Series[0].ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            rep_SDS.xrChart1.Series[0].Label.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint;
            rep_SDS.xrChart1.Series[0].Label.PointOptions.ValueNumericOptions.Precision = 2;
            rep_SDS.xrChart1.Series[0].Label.TextColor = System.Drawing.Color.Black;
            rep_SDS.xrChart1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            rep_SDS.xrChart1.Series[0].Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            rep_SDS.xrChart1.Series[0].ShowInLegend = false;

            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Label.NumericOptions.Precision = 2;
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Label.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;

            string resource3 = "Sales";
            string resource4 = "Department";

            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Text = "Sales";
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Text = "Department";
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisX.Title.Visible = true;
            (rep_SDS.xrChart1.Diagram as DevExpress.XtraCharts.XYDiagram).AxisY.Title.Visible = true;
            /*

            rep_SDS.xrChart1.Series.Add("", DevExpress.XtraCharts.ViewType.Line);
            foreach (DataRow dr in dtblSD.Rows)
            {
                rep_SDS.xrChart1.Series[1].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Department"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Percent"].ToString()) }));

            }*/




            foreach (DataRow dr in dtblSC.Rows)
            {
                if (String.IsNullOrEmpty(dr["Category"].ToString()) == false)
                    rep_SCS.xrChart2.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Category"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            DevExpress.XtraCharts.PaletteRepository pr = rep_SCS.xrChart2.PaletteRepository;

            DevExpress.XtraCharts.Palette p = pr["MyPalette"];

            int colorcount = p.Count;
            int i = 0;
            foreach (DataRow dr in dtblSC.Rows)
            {
                if (i == colorcount)
                {
                    i = 0;
                }
                DevExpress.XtraCharts.PaletteEntry pe = p[i];
                dr["Color"] = pe.Color.ToArgb();
                i++;
            }

            foreach (DataRow dr in dtblSC.Rows)
            {
                dr["Percent"] = GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Amount"].ToString()) * 100 / TotalCatAmount).ToString());
            }

            Double TPercent = 0;
            foreach (DataRow dr in dtblSC.Rows)
            {
                TPercent = TPercent + GeneralFunctions.fnDouble(dr["Percent"].ToString());
            }

            double var = 100 - TPercent;
            if (var != 0)
            {
                foreach (DataRow dr in dtblSC.Rows)
                {
                    dr["Percent"] = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["Percent"].ToString()) + var);
                    break;
                }
            }
            rep_SCS.Report.DataSource = dtblSC;
            rep_SCS.xrLabel1.DataBindings.Add("Text", dtblSC, "Color");
            rep_SCS.xrLabel3.DataBindings.Add("Text", dtblSC, "Category");
            rep_SCS.xrLabel4.DataBindings.Add("Text", dtblSC, "Amount");
            rep_SCS.xrLabel6.DataBindings.Add("Text", dtblSC, "Percent");
            rep_SCS.rTAmount.DataBindings.Add("Text", dtblSC, "Amount");
            //rep_SCS.rTPerc.DataBindings.Add("Text", dtblSC, "Percent");


            foreach (DataRow dr in dtblT.Rows)
            {
                rep_T.xrChart2.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["TenderType"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Amount"].ToString()) }));
            }

            DevExpress.XtraCharts.PaletteRepository pr1 = rep_T.xrChart2.PaletteRepository;

            DevExpress.XtraCharts.Palette p1 = pr1["MyPalette"];

            int colorcount1 = p1.Count;
            int i1 = 0;
            foreach (DataRow dr in dtblT.Rows)
            {
                if (i1 == colorcount1)
                {
                    i1 = 0;
                }
                DevExpress.XtraCharts.PaletteEntry pe1 = p1[i1];
                dr["Color"] = pe1.Color.ToArgb();
                i1++;
            }
            rep_T.Report.DataSource = dtblT;
            rep_T.xrLabel1.DataBindings.Add("Text", dtblT, "Color");
            rep_T.xrLabel3.DataBindings.Add("Text", dtblT, "TenderType");
            rep_T.xrLabel4.DataBindings.Add("Text", dtblT, "Amount");
            rep_T.rTAmount.DataBindings.Add("Text", dtblT, "Amount");

            rep_GC.Report.DataSource = dtblGC;
            rep_GC.rQty.DataBindings.Add("Text", dtblGC, "Qty");
            rep_GC.rTotal.DataBindings.Add("Text", dtblGC, "Total");
            rep_GC.rAvg.DataBindings.Add("Text", dtblGC, "Avg");

            rep_TX.Report.DataSource = dtblTx;
            rep_TX.rTaxName.DataBindings.Add("Text", dtblTx, "Tax");
            rep_TX.rTaxable.DataBindings.Add("Text", dtblTx, "Taxable");
            rep_TX.rTax.DataBindings.Add("Text", dtblTx, "Collected");
            rep_TX.rTTax.DataBindings.Add("Text", dtblTx, "Collected");

            if (dtblSH.Rows.Count == 0)
            {
                rep_SHS.xrChart1.Visible = false;
                rep_SHS.xrTable1.Visible = false;
                rep_SHS.Detail.Visible = false;
                rep_SHS.ReportFooter.Visible = false;
                rep_SHS.lbNoData.BringToFront();
                rep_SHS.xrChart1.SendToBack();
            }

            if (dtblSD.Rows.Count == 0)
            {
                rep_SDS.xrChart1.Visible = false;
                rep_SDS.lbNoData.BringToFront();
                rep_SDS.xrChart1.SendToBack();
            }


            if (dtblSC.Rows.Count == 0)
            {
                rep_SCS.xrChart2.Visible = false;
                rep_SCS.Detail.Visible = false;
                rep_SCS.ReportFooter.Visible = false;
                rep_SCS.lbNoData.BringToFront();
                rep_SCS.xrChart2.SendToBack();
            }

            if (dtblT.Rows.Count == 0)
            {
                rep_T.xrChart2.Visible = false;
                rep_T.Detail.Visible = false;
                rep_T.ReportFooter.Visible = false;

                rep_T.lbNoData.BringToFront();
                rep_T.xrChart2.SendToBack();
            }

            if (dtblTx.Rows.Count == 0)
            {
                rep_TX.xrTable1.Visible = false;
                rep_TX.Detail.Visible = false;
                rep_TX.xrLine1.Visible = false;
                rep_TX.xrTable3.Visible = false;
                rep_TX.lbNoData.BringToFront();
                rep_TX.xrLine1.SendToBack();
                rep_TX.xrTable3.SendToBack();
            }
            else
            {
                rep_TX.lbNoData.SendToBack();
                rep_TX.lbNoData.Visible = false;
                rep_TX.xrLine1.Visible = true;
                rep_TX.xrTable3.Visible = true;
            }


            int HrCount = dtblSH.Rows.Count;

            if (HrCount > 4)
            {
                HrCount = HrCount - 4;
                rep_SHS.ReportHeader.HeightF = rep_SHS.ReportHeader.HeightF + HrCount * 20;
                rep_SHS.xrChart1.HeightF = rep_SHS.xrChart1.HeightF + HrCount * 20;
                rep_SHS.xrTable1.TopF = rep_SHS.xrTable1.TopF + HrCount * 19;
            }




            if (eventtype == "Preview")
            {
                try
                {
                    if (Settings.ReportPrinterName != "") repM.PrinterName = Settings.ReportPrinterName;
                    repM.CreateDocument();
                    repM.PrintingSystem.ShowMarginsWarning = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = repM;
                    window.ShowDialog();

                    //repM.ShowPreviewDialog();
                }
                finally
                {
                    repM.Dispose();
                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                repM.CreateDocument();
                repM.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(repM);
                }
                finally
                {
                    repM.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                repM.CreateDocument();
                repM.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "Dashboard_Sales.pdf";
                    GeneralFunctions.EmailReport(repM, attachfile, "Dashboard");
                }
                finally
                {
                    repM.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Sales - Dashboard";
            dtFrom.EditValue = DateTime.Today.Date;
            dtTo.EditValue = DateTime.Today.Date;
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
