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
    /// Interaction logic for frm_ApptRepDlg.xaml
    /// </summary>
    public partial class frm_ApptRepDlg : Window
    {
        public frm_ApptRepDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void PopulateStaff(string opt)
        {
            PosDataObject.Employee objCategory = new PosDataObject.Employee();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.GetAppointmentEmployee(opt);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            emplkup.ItemsSource = dtblTemp;
           
            emplkup.EditValue = "0";
            dbtblCat.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Appointment Report";
            PopulateStaff("ALL");
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
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
                Cursor = Cursors.Hand;
            }
        }

        private void ExecuteReport(string eventtype)
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                GeneralFunctions.SetFocus(dtFrom);
                return;
            }

            DataTable dtbl = new DataTable();
            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = SystemVariables.Conn;
            dtbl = objp1.FetchApptHeaderData(dtFrom.DateTime, dtTo.DateTime, GeneralFunctions.fnInt32(emplkup.EditValue.ToString()), SystemVariables.DateFormat);

            foreach (DataRow dr1 in dtbl.Rows)
            {
                string srv = "";
                srv = GetService(GeneralFunctions.fnInt32(dr1["ID"].ToString()));

                dr1["ApptSubject"] = GeneralFunctions.fnDate(dr1["ApptStart"].ToString()).ToString("t") + "  -  "
                                        + GeneralFunctions.fnDate(dr1["ApptEnd"].ToString()).ToString("t");

                dr1["ApptStart"] = GeneralFunctions.fnDate(dr1["ApptStart"].ToString()).ToString("d");
                dr1["ApptEnd"] = GeneralFunctions.fnDate(dr1["ApptEnd"].ToString()).ToString("t");

                dr1["ApptDetail"] = srv;
            }

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

            OfflineRetailV2.Report.Misc.repAppointments rep_Logs = new OfflineRetailV2.Report.Misc.repAppointments();

            GeneralFunctions.MakeReportWatermark(rep_Logs);
            rep_Logs.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Logs.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Logs.rHDate.Text = "from" + " " + dtFrom.DateTime.ToShortDateString() + " " + "to" + " " + dtTo.DateTime.ToShortDateString();
            rep_Logs.Report.DataSource = dtbl;

            rep_Logs.GroupHeader1.GroupFields.Add(rep_Logs.CreateGroupField("EmployeeID"));

            rep_Logs.rStaff.DataBindings.Add("Text", dtbl, "Employee");
            rep_Logs.rCustomer.DataBindings.Add("Text", dtbl, "Customer");
            rep_Logs.rDate.DataBindings.Add("Text", dtbl, "ApptStart");
            rep_Logs.rTime.DataBindings.Add("Text", dtbl, "ApptSubject");
            rep_Logs.rService.DataBindings.Add("Text", dtbl, "ApptDetail");
            rep_Logs.rStatus.DataBindings.Add("Text", dtbl, "ApptStatus");
            rep_Logs.rRemarks.DataBindings.Add("Text", dtbl, "Remarks");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_Logs.PrinterName = Settings.ReportPrinterName;
                    rep_Logs.CreateDocument();
                    rep_Logs.PrintingSystem.ShowMarginsWarning = false;
                    rep_Logs.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_Logs;
                    window.ShowDialog();

                    //rep_Logs.ShowPreviewDialog();
                    
                }
                finally
                {
                    rep_Logs.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_Logs.CreateDocument();
                rep_Logs.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_Logs);
                }
                finally
                {
                    rep_Logs.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_Logs.CreateDocument();
                rep_Logs.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "appointment.pdf";
                    GeneralFunctions.EmailReport(rep_Logs, attachfile, "Appointment");
                }
                finally
                {
                    rep_Logs.Dispose();
                    dtbl.Dispose();
                }
            }

        }

        private string GetService(int pHID)
        {
            string val = "";
            DataTable dtblD = new DataTable();
            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = SystemVariables.Conn;
            dtblD = objp1.FetchApptMappingData(pHID);
            foreach (DataRow dr in dtblD.Rows)
            {
                if (val == "") val = dr["Service"].ToString(); else val = val + ", " + dr["Service"].ToString();
            }
            return val;
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

        private void Emplkup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
