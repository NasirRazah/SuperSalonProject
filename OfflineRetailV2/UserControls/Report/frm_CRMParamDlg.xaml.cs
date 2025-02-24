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
    /// Interaction logic for frm_CRMParamDlg.xaml
    /// </summary>
    public partial class frm_CRMParamDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private DataTable dtbl;
        private bool blChanged = false;

        public frm_CRMParamDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void LoadClientDefinedParameters()
        {
            PosDataObject.Setup objSetup = new PosDataObject.Setup();
            objSetup.Connection = SystemVariables.Conn;
            DataTable dtblParam = new DataTable();
            dtblParam = objSetup.FetchClientParamData();
            foreach (DataRow dr in dtblParam.Rows)
            {
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam1"].ToString().Trim() == ""))
                {
                    lbParam1.Text = "1. " + "Undefined";
                    txtParamVal1.IsReadOnly = true;
                }
                else lbParam1.Text = "1. " + dr["ClientParam1"].ToString();
                if ((dr["ClientParam2"].ToString() == null) || (dr["ClientParam2"].ToString().Trim() == ""))
                {
                    lbParam2.Text = "2. " + "Undefined";
                    txtParamVal2.IsReadOnly = true;
                }
                else lbParam2.Text = "2. " + dr["ClientParam2"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam3"].ToString().Trim() == ""))
                {
                    lbParam3.Text = "3. " + "Undefined";
                    txtParamVal3.IsReadOnly = true;
                }
                else lbParam3.Text = "3. " + dr["ClientParam3"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam4"].ToString().Trim() == ""))
                {
                    lbParam4.Text = "4. " + "Undefined";
                    txtParamVal4.IsReadOnly = true;
                }
                else lbParam4.Text = "4. " + dr["ClientParam4"].ToString();
                if ((dr["ClientParam1"].ToString() == null) || (dr["ClientParam5"].ToString().Trim() == ""))
                {
                    lbParam5.Text = "5. " + "Undefined";
                    txtParamVal5.IsReadOnly = true;
                }
                else lbParam5.Text = "5. " + dr["ClientParam5"].ToString();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            Title.Text = "CRM By Parameters";
            LoadClientDefinedParameters();
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
            dtbl.Columns.Add("Visit", System.Type.GetType("System.String"));
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
                if ((txtParamVal1.Text.Trim() == "") && (txtParamVal2.Text.Trim() == "") &&
                    (txtParamVal3.Text.Trim() == "") && (txtParamVal4.Text.Trim() == "") &&
                    (txtParamVal5.Text.Trim() == ""))
                {
                    DocMessage.MsgEnter("atleast one parameter");
                    return;
                }
                dtbl.Rows.Clear();


                DataTable dtbl2 = new DataTable();
                PosDataObject.Sales objs1 = new PosDataObject.Sales();
                objs1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objs1.OperateStore = Settings.StoreCode;
                dtbl2 = objs1.FetchCustomerByParam(txtParamVal1.IsReadOnly, txtParamVal2.IsReadOnly,
                    txtParamVal3.IsReadOnly, txtParamVal4.IsReadOnly,
                    txtParamVal5.IsReadOnly, txtParamVal1.Text.Trim(), txtParamVal2.Text.Trim(),
                    txtParamVal3.Text.Trim(), txtParamVal4.Text.Trim(), txtParamVal5.Text.Trim());

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                foreach (DataRow dc1 in dtbl2.Rows)
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
                        dc1["ActiveStatus"].ToString(),
                        strip});

                }
                DataView dv1 = new DataView(dtbl);
                dv1.Sort = " CustomerName ASC ";
                grdCustomer.ItemsSource = dtbl;
                dtbl2.Dispose();

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
                else
                {
                    blChanged = true;
                    btnGenerate.IsEnabled = true;
                    btnPrint.IsEnabled = false;
                    btnPrintLabel.IsEnabled = false;
                    btnExport.IsEnabled = false;
                    btnEmail.IsEnabled = false;
                    btnPreview.IsEnabled = false;
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
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

        private void ExecuteReport(string eventtype)
        {
            OfflineRetailV2.Report.CRM.repCRMparam rep_CRMparam = new OfflineRetailV2.Report.CRM.repCRMparam();

            rep_CRMparam.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_CRMparam);
            rep_CRMparam.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CRMparam.rReportHeader.Text = Settings.ReportHeader_Address;
            string Param = "Parameters :" + "\n";
            if (txtParamVal1.IsReadOnly == false)
            {
                if (txtParamVal1.Text.Trim() != "") Param = Param + lbParam1.Text + " : " + txtParamVal1.Text.Trim() + "\n";
            }
            if (txtParamVal2.IsReadOnly == false)
            {
                if (txtParamVal2.Text.Trim() != "") Param = Param + lbParam2.Text + " : " + txtParamVal2.Text.Trim() + "\n";
            }
            if (txtParamVal3.IsReadOnly == false)
            {
                if (txtParamVal3.Text.Trim() != "") Param = Param + lbParam3.Text + " : " + txtParamVal3.Text.Trim() + "\n";
            }
            if (txtParamVal4.IsReadOnly == false)
            {
                if (txtParamVal4.Text.Trim() != "") Param = Param + lbParam4.Text + " : " + txtParamVal4.Text.Trim() + "\n";
            }
            if (txtParamVal5.IsReadOnly == false)
            {
                if (txtParamVal5.Text.Trim() != "") Param = Param + lbParam5.Text + " : " + txtParamVal5.Text.Trim() + "\n";
            }
            rep_CRMparam.rAddHeader.Text = Param;

            rep_CRMparam.rCustID.DataBindings.Add("Text", dtbl, "CustomerID");
            rep_CRMparam.rCustName.DataBindings.Add("Text", dtbl, "CustomerName");
            rep_CRMparam.rCompany.DataBindings.Add("Text", dtbl, "Company");
            rep_CRMparam.rAddress.DataBindings.Add("Text", dtbl, "MailAddress");
            rep_CRMparam.rEmail.DataBindings.Add("Text", dtbl, "EMail");
            rep_CRMparam.rPhone.DataBindings.Add("Text", dtbl, "WorkPhone");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CRMparam.PrinterName = Settings.ReportPrinterName;
                    rep_CRMparam.CreateDocument();
                    rep_CRMparam.PrintingSystem.ShowMarginsWarning = false;
                    rep_CRMparam.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CRMparam;
                    window.ShowDialog();

                    //rep_CRMparam.ShowPreviewDialog();

                    
                }
                finally
                {
                    rep_CRMparam.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();

                }
            }


            if (eventtype == "Print")
            {
                rep_CRMparam.CreateDocument();
                rep_CRMparam.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CRMparam);
                }
                finally
                {
                    rep_CRMparam.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CRMparam.CreateDocument();
                rep_CRMparam.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "CRM_param.pdf";
                    GeneralFunctions.EmailReport(rep_CRMparam, attachfile, "CRM by Parameters");
                }
                finally
                {
                    rep_CRMparam.Dispose();
                }
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
                                    + "EMail";
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
                                    + dr["EMail"].ToString();

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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void TxtParamVal1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
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


        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }
    }
}
