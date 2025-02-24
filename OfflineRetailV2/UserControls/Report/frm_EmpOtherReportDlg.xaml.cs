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
    /// Interaction logic for frm_EmpOtherReportDlg.xaml
    /// </summary>
    public partial class frm_EmpOtherReportDlg : Window
    {
        public frm_EmpOtherReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Employee Special Reports";

            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
        }

        private void ExecuteSpecialEventReport(string eventtype)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objSales = new PosDataObject.Customer();
            objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objSales.FetchSpecialEvent("Employee", GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue),
                                              rgSort1.IsChecked == true ? 0 : 1, Settings.StoreCode);

            OfflineRetailV2.Report.Misc.repNotes rep_Notes = new OfflineRetailV2.Report.Misc.repNotes();
            rep_Notes.rHeader.Text = "Employee Special Events Report";
            rep_Notes.rHeaderDate.Visible = true;
            rep_Notes.rHeaderDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " +
                                                    GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

            rep_Notes.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_Notes);
            rep_Notes.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Notes.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Notes.rH1.Text = "Emp. ID";
            rep_Notes.rH2.Text = "Employee";
            rep_Notes.rH3.Text = "Phone 1";
            rep_Notes.rH4.Text = "Phone 2";

            rep_Notes.rH5.Text = "";
            rep_Notes.rH6.Text = "";

            rep_Notes.rCustID.DataBindings.Add("Text", dtbl, "ID");
            rep_Notes.rCustName.DataBindings.Add("Text", dtbl, "Name");
            rep_Notes.rPh1.DataBindings.Add("Text", dtbl, "Phone1");
            rep_Notes.rPh2.DataBindings.Add("Text", dtbl, "Phone2");
            rep_Notes.rEmail.DataBindings.Add("Text", dtbl, "EMail");
            rep_Notes.rEvents.DataBindings.Add("Text", dtbl, "Event");
            rep_Notes.rDate.DataBindings.Add("Text", dtbl, "Date");
            rep_Notes.rGroup.Text = "";
            rep_Notes.rClass.Text = "";

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_Notes.PrinterName = Settings.ReportPrinterName;
                    rep_Notes.CreateDocument();
                    rep_Notes.PrintingSystem.ShowMarginsWarning = false;
                    rep_Notes.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_Notes.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_Notes;
                    window.ShowDialog();
                }
                finally
                {
                    rep_Notes.Dispose();
                    // frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_Notes.CreateDocument();
                rep_Notes.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_Notes);
                }
                finally
                {
                    rep_Notes.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_Notes.CreateDocument();
                rep_Notes.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "emp_spl_event.pdf";
                    GeneralFunctions.EmailReport(rep_Notes, attachfile, "Employee Special Event");
                }
                finally
                {
                    rep_Notes.Dispose();
                    dtbl.Dispose();
                }
            }

            if (eventtype == "Export")
            {
                try
                {
                    string fileName = GeneralFunctions.ShowSaveFileDialog("CSV Document", "CSV Files|*.csv");
                    if (fileName != "")
                    {
                        FileInfo t = new FileInfo(fileName);
                        StreamWriter Tex = t.CreateText();
                        string hding = "Emp ID" + Settings.ExportDelimiter
                                        + "Employee" + Settings.ExportDelimiter
                                        + "Phone 1" + Settings.ExportDelimiter
                                        + "Phone 2" + Settings.ExportDelimiter
                                        + "EMail" + Settings.ExportDelimiter
                                        + "Date" + Settings.ExportDelimiter
                                        + "Event";
                        Tex.WriteLine(hding);
                        int i = 0;
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            i++;
                            string str = dr["ID"].ToString() + Settings.ExportDelimiter
                                        + dr["Name"].ToString() + Settings.ExportDelimiter
                                        + dr["Phone1"].ToString() + Settings.ExportDelimiter
                                        + dr["Phone2"].ToString() + Settings.ExportDelimiter
                                        + dr["EMail"].ToString() + Settings.ExportDelimiter
                                        + dr["Date"].ToString() + Settings.ExportDelimiter
                                        + dr["Event"].ToString();
                            Tex.WriteLine(str);
                        }
                        Tex.Close();
                        GeneralFunctions.OpenFile(fileName);
                    }
                }
                finally
                {

                }
            }

        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                ExecuteSpecialEventReport(eventtype);
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

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Export");
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
