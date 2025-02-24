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
    /// Interaction logic for frm_CRMVisitDlg.xaml
    /// </summary>
    public partial class frm_CRMVisitDlg : Window
    {
        private DataTable dtbl;
        private bool blChanged = false;
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_CRMVisitDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = "CRM By Visit";
            dtFrom.EditValue = DateTime.Today.AddMonths(-1);
            dtTo.EditValue = DateTime.Today;
            dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CustomerID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CustomerName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("MailAddress", System.Type.GetType("System.String"));
            dtbl.Columns.Add("ShipAddress", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Salutation", System.Type.GetType("System.String"));
            dtbl.Columns.Add("FirstName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("LastName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DateOfBirth", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Address1", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Address2", System.Type.GetType("System.String"));
            dtbl.Columns.Add("City", System.Type.GetType("System.String"));
            dtbl.Columns.Add("State", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Country", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtbl.Columns.Add("HomePhone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("WorkPhone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Fax", System.Type.GetType("System.String"));
            dtbl.Columns.Add("EMail", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Visit", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("ActiveStatus", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Image", System.Type.GetType("System.Byte[]"));
            blChanged = true;
            btnGenerate.IsEnabled = true;
            btnPrint.IsEnabled = false;
            btnPrintLabel.IsEnabled = false;
            btnExport.IsEnabled = false;
            btnEmail.IsEnabled = false;
            btnPreview.IsEnabled = false;
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                dtbl.Rows.Clear();

                DataTable dtbl1 = new DataTable();
                PosDataObject.Sales objs = new PosDataObject.Sales();
                objs.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objs.OperateStore = Settings.StoreCode;
                dtbl1 = objs.FetchCRMbyVisitReportData(dtFrom.DateTime, dtTo.DateTime);
                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                if (dtbl1.Rows.Count > 0)
                {
                    DataTable dtbl2 = new DataTable();
                    PosDataObject.Sales objs1 = new PosDataObject.Sales();
                    objs1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    objs1.OperateStore = Settings.StoreCode;
                    dtbl2 = objs1.FetchCustomer();
                    foreach (DataRow dc in dtbl2.Rows)
                    {
                        foreach (DataRow dv in dtbl1.Rows)
                        {
                            if (dc["ID"].ToString() != dv["CustomerID"].ToString()) continue;
                            dc["Field"] = GeneralFunctions.fnInt32(dc["Field"].ToString()) + GeneralFunctions.fnInt32(dv["Visit"].ToString());
                        }
                    }

                    foreach (DataRow dc1 in dtbl2.Rows)
                    {
                        if ((GeneralFunctions.fnInt32(dc1["Field"].ToString()) >= GeneralFunctions.fnInt32(visitFrom.Text))
                            && (GeneralFunctions.fnInt32(dc1["Field"].ToString()) <= GeneralFunctions.fnInt32(visitTo.Text)))
                        {

                            dtbl.Rows.Add(new object[] {
                        dc1["ID"].ToString(),
                        dc1["CustomerID"].ToString(),
                        dc1["CustomerName"].ToString(),
                        dc1["MailAddress"].ToString(),
                        dc1["ShipAddress"].ToString(),
                        dc1["Company"].ToString(),
                        dc1["Salutation"].ToString(),
                        dc1["FirstName"].ToString(),
                        dc1["LastName"].ToString(),
                        dc1["DateOfBirth"].ToString(),
                        dc1["Address1"].ToString(),
                        dc1["Address2"].ToString(),
                        dc1["City"].ToString(),
                        dc1["State"].ToString(),
                        dc1["Country"].ToString(),
                        dc1["Zip"].ToString(),
                        dc1["HomePhone"].ToString(),
                        dc1["WorkPhone"].ToString(),
                        dc1["MobilePhone"].ToString(),
                        dc1["Fax"].ToString(),
                        dc1["EMail"].ToString(),
                        GeneralFunctions.fnInt32(dc1["Field"].ToString()),
                        dc1["ActiveStatus"].ToString(),strip   });
                        }
                    }
                    //gridView1.SortInfo.Add(colVisit, DevExpress.Data.ColumnSortOrder.Descending);
                    grdCustomer.ItemsSource = dtbl;
                    dtbl2.Dispose();
                }
                dtbl1.Dispose();
                if (dtbl.Rows.Count > 0)
                {
                    blChanged = false;
                    btnGenerate.IsEnabled = false;
                    btnPrint.IsEnabled = true;
                    btnPrintLabel.IsEnabled = true;
                    btnExport.IsEnabled = true;
                    btnEmail.IsEnabled = true;
                    btnPreview.IsEnabled = true;
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteReport(string eventtype)
        {
            OfflineRetailV2.Report.CRM.repCRMvisit rep_CRMvisit = new OfflineRetailV2.Report.CRM.repCRMvisit();

            rep_CRMvisit.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_CRMvisit);
            rep_CRMvisit.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CRMvisit.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CRMvisit.rAddFooter.Text = "Visit :" + " " + visitFrom.Text + " - " + visitTo.Text;
            rep_CRMvisit.rAddHeader.Text = "From" + dtFrom.DateTime.ToString("MMMM dd, yyyy") + " to" + " " +
                                        dtTo.DateTime.ToString("MMMM dd, yyyy");

            rep_CRMvisit.rCustID.DataBindings.Add("Text", dtbl, "CustomerID");
            rep_CRMvisit.rCustName.DataBindings.Add("Text", dtbl, "CustomerName");
            rep_CRMvisit.rCompany.DataBindings.Add("Text", dtbl, "Company");
            rep_CRMvisit.rAddress.DataBindings.Add("Text", dtbl, "MailAddress");
            rep_CRMvisit.rPhone.DataBindings.Add("Text", dtbl, "WorkPhone");
            rep_CRMvisit.rEmail.DataBindings.Add("Text", dtbl, "EMail");
            rep_CRMvisit.rLastPurchaseDate.DataBindings.Add("Text", dtbl, "Visit");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CRMvisit.PrinterName = Settings.ReportPrinterName;
                    rep_CRMvisit.CreateDocument();
                    rep_CRMvisit.PrintingSystem.ShowMarginsWarning = false;
                    rep_CRMvisit.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CRMvisit;
                    window.ShowDialog();

                    //rep_CRMvisit.ShowPreviewDialog();


                }
                finally
                {
                    rep_CRMvisit.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_CRMvisit.CreateDocument();
                rep_CRMvisit.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CRMvisit);
                }
                finally
                {
                    rep_CRMvisit.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CRMvisit.CreateDocument();
                rep_CRMvisit.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "CRM_visit.pdf";
                    GeneralFunctions.EmailReport(rep_CRMvisit, attachfile, "CRM by Visits");
                }
                finally
                {
                    rep_CRMvisit.Dispose();
                }
            }
        }

        private void ExecuteLabel()
        {
            int intskip = 0;
            int r = 0;
            int c = 0;
            r = GeneralFunctions.fnInt32(spnrow.Text);
            c = GeneralFunctions.fnInt32(spncol.Text);
            if (r == 1)
            {
                intskip = c - 1;
            }
            else
            {
                intskip = (r * 3) - 3 + c - 1;
            }

            DataTable dtblActive = dtbl.Clone();
            DataRow[] d = dtbl.Select("ActiveStatus = 'Y' ");
            foreach (DataRow dr in d)
            {
                dtblActive.ImportRow(dr);
            }

            if (dtblActive.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record found for Printing");
                return;
            }

            DataTable dlbl = new DataTable();
            dlbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Address", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Attn", System.Type.GetType("System.String"));

            if (rgReportType1.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["Company"].ToString(),
                                                    dr1["MailAddress"].ToString(),
                                                    dr1["CustomerName"].ToString()});
                }
            }

            if (rgReportType2.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["Company"].ToString(),
                                                    dr1["ShipAddress"].ToString(),
                                                    dr1["CustomerName"].ToString()});
                }
            }


            dlbl.Rows.Add(new object[] { "", "", "" });

            OfflineRetailV2.Report.Customer.repCustomerLabel rep_CustomerLabel = new OfflineRetailV2.Report.Customer.repCustomerLabel(intskip, dlbl.Rows.Count);
            DataSet ds = new DataSet();
            ds.Tables.Add(dlbl);
            rep_CustomerLabel.DataSource = ds;

            if (Settings.UseCustomerNameInLabelPrint == "N")
            {
                rep_CustomerLabel.rAttn.Visible = false;
                rep_CustomerLabel.rAttnLb.Visible = false;
            }

            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                if (Settings.ReportPrinterName != "") rep_CustomerLabel.PrinterName = Settings.ReportPrinterName;
                rep_CustomerLabel.CreateDocument();
                rep_CustomerLabel.PrintingSystem.ShowMarginsWarning = false;
                rep_CustomerLabel.PrintingSystem.ShowPrintStatusDialog = false;
                rep_CustomerLabel.ShowPreviewDialog();

                //frm_PreviewControl.dv.DocumentSource = rep_CustomerLabel;
                //frm_PreviewControl.dv.PrintingSystem = rep_CustomerLabel.PrintingSystem;
                //frm_PreviewControl.dv.PrintingSystem.ShowMarginsWarning = false;
                //frm_PreviewControl.dv.PrintingSystem.ShowPrintStatusDialog = false;
                //frm_PreviewControl.ShowDialog();
            }
            finally
            {
                rep_CustomerLabel.Dispose();
                //frm_PreviewControl.dv.DocumentSource = null;
                //frm_PreviewControl.Dispose();
                dlbl.Dispose();
                dtblActive.Dispose();

            }
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

        private void BtnPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                ExecuteLabel();
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void OpenFile(string fileName)
        {
            if (DocMessage.MsgConfirmation("Do you want to open this file?") == MessageBoxResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DocMessage.MsgError("Cannot find an application on your system suitable for openning the file with exported data.");
                }
            }
            //progressBarControl1.Position = 0;
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
            Cursor = Cursors.Wait;
            try
            {
                string fileName = GeneralFunctions.ShowSaveFileDialog("CSV Document", "CSV Files|*.csv");
                if (fileName != "")
                {
                    FileInfo t = new FileInfo(fileName);
                    StreamWriter Tex = t.CreateText();
                    string hding = "Customer ID" + Settings.ExportDelimiter
                                    + "Salutation" + Settings.ExportDelimiter
                                    + "First Name" + Settings.ExportDelimiter
                                    + "Last Name" + Settings.ExportDelimiter
                                    + "Company" + Settings.ExportDelimiter
                                    + "Date Of Birth" + Settings.ExportDelimiter
                                    + "Address 1" + Settings.ExportDelimiter
                                    + "Address 2" + Settings.ExportDelimiter
                                    + "City" + Settings.ExportDelimiter
                                    + "State" + Settings.ExportDelimiter
                                    + "Zip" + Settings.ExportDelimiter
                                    + "Country" + Settings.ExportDelimiter
                                    + "Home Phone" + Settings.ExportDelimiter
                                    + "Work Phone" + Settings.ExportDelimiter
                                    + "Mobile Phone" + Settings.ExportDelimiter
                                    + "Fax" + Settings.ExportDelimiter
                                    + "EMail" + Settings.ExportDelimiter
                                    + "Visit";
                    Tex.WriteLine(hding);
                    int i = 0;
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        i++;

                        /*progressBarControl1.Position = i * GeneralFunctions.fnInt32(Math.Ceiling(Convert.ToDecimal(100 / dtbl.Rows.Count)));
                        if (i == dtbl.Rows.Count)
                        {
                            if (progressBarControl1.Position < 100)
                                progressBarControl1.Position = 100;
                        }
                        progressBarControl1.Update();*/
                        string str = dr["CustomerID"].ToString() + Settings.ExportDelimiter
                                    + dr["Salutation"].ToString() + Settings.ExportDelimiter
                                    + dr["FirstName"].ToString() + Settings.ExportDelimiter
                                    + dr["LastName"].ToString() + Settings.ExportDelimiter
                                    + dr["Company"].ToString() + Settings.ExportDelimiter
                                    + dr["DateOfBirth"].ToString() + Settings.ExportDelimiter
                                    + dr["Address1"].ToString() + Settings.ExportDelimiter
                                    + dr["Address2"].ToString() + Settings.ExportDelimiter
                                    + dr["City"].ToString() + Settings.ExportDelimiter
                                    + dr["State"].ToString() + Settings.ExportDelimiter
                                    + dr["Zip"].ToString() + Settings.ExportDelimiter
                                    + dr["Country"].ToString() + Settings.ExportDelimiter
                                    + dr["HomePhone"].ToString() + Settings.ExportDelimiter
                                    + dr["WorkPhone"].ToString() + Settings.ExportDelimiter
                                    + dr["MobilePhone"].ToString() + Settings.ExportDelimiter
                                    + dr["Fax"].ToString() + Settings.ExportDelimiter
                                    + dr["EMail"].ToString() + Settings.ExportDelimiter
                                    + dr["Visit"].ToString();
                        Tex.WriteLine(str);

                    }
                    Tex.Close();
                    //ExportTo(new ExportTxtProvider(fileName));
                    OpenFile(fileName);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void DtFrom_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            blChanged = true;
            btnGenerate.IsEnabled = true;
            btnPrint.IsEnabled = false;
            btnPrintLabel.IsEnabled = false;
            btnExport.IsEnabled = false;
            btnEmail.IsEnabled = false;
            btnPreview.IsEnabled = false;
        }

        private void Spnrow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
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


        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
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
    }
}
