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
    /// Interaction logic for frm_CustomerOtherReportDlg.xaml
    /// </summary>
    public partial class frm_CustomerOtherReportDlg : Window
    {
        public frm_CustomerOtherReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateReport()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "1", "Total Purchases" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Last Visit" });
            dtblOrderBy.Rows.Add(new object[] { "3", "Special Events" });
            lkupGroupBy.ItemsSource = dtblOrderBy;
            lkupGroupBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private string GetCustomerGroup(int CID)
        {
            string rets = "";
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCG = new PosDataObject.Customer();
            objCG.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objCG.ShowGroup(CID);
            foreach (DataRow dr in dtbl.Rows)
            {
                if (rets == "") rets = dr["GroupIDName"].ToString();
                else rets = rets + ", " + dr["GroupIDName"].ToString();
            }
            dtbl.Dispose();
            return rets;
        }

        private string GetCustomerClass(int CID)
        {
            string rets = "";
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCC = new PosDataObject.Customer();
            objCC.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objCC.ShowClass(CID);
            foreach (DataRow dr in dtbl.Rows)
            {
                if (rets == "") rets = dr["ClassIDName"].ToString();
                else rets = rets + ", " + dr["ClassIDName"].ToString();
            }
            dtbl.Dispose();
            return rets;
        }

        private void ExecutePurchaseReport(string strType, string eventtype)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objSales.FetchCustomerPurchaseReportData(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.StoreCode);

            DataTable dtbl1 = new DataTable();
            dtbl1.Columns.Add("CID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Company", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Address1", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("City", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("State", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Phone", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("TotalPurchases", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("DateLastPurchase", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("AmountLastPurchase", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("Email", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CGroup", System.Type.GetType("System.String"));
            dtbl1.Columns.Add("CClass", System.Type.GetType("System.String"));

            DataTable dtbl2 = new DataTable();
            if (strType == "T")
            {
                dtbl2 = GetPurchaseAmount(GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue));
            }
            if (strType == "L")
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    dtbl1.Rows.Add(new object[] {   dr["CID"].ToString(),
                                                   dr["CustID"].ToString(),
                                                   dr["Company"].ToString(),
                                                   dr["Customer"].ToString(),
                                                   dr["Address1"].ToString(),
                                                   dr["City"].ToString(),
                                                   dr["State"].ToString(),
                                                   dr["Zip"].ToString(),
                                                   dr["Phone"].ToString(),
                                                   dr["TotalPurchases"].ToString(),
                                                   dr["DateLastPurchase"].ToString(),
                                                   dr["AmountLastPurchase"].ToString(),
                                                   dr["Email"].ToString(),
                    dr["CGroup"].ToString(),
                    dr["CClass"].ToString()});
                }
            }

            if (strType == "T")
            {
                foreach (DataRow dr in dtbl2.Rows)
                {
                    dtbl1.Rows.Add(new object[] {  dr["CID"].ToString(),
                                                   dr["CustID"].ToString(),
                                                   dr["Company"].ToString(),
                                                   dr["Customer"].ToString(),
                                                   dr["Address1"].ToString(),
                                                   dr["City"].ToString(),
                                                   dr["State"].ToString(),
                                                   dr["Zip"].ToString(),
                                                   dr["Phone"].ToString(),
                                                   dr["TotalPurchases"].ToString(),
                                                   dr["DateLastPurchase"].ToString(),
                                                   dr["AmountLastPurchase"].ToString(),
                                                   dr["Email"].ToString(),
                                                   dr["CGroup"].ToString(),
                                                   dr["CClass"].ToString()});
                }
            }

            OfflineRetailV2.Report.Customer.repCustomerGeneral rep_CustomerGeneral = new OfflineRetailV2.Report.Customer.repCustomerGeneral();
            if (dtbl1.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl1.Dispose();
                return;
            }
            rep_CustomerGeneral.rAddHeader.Visible = true;
            if (strType == "T")
            {
                rep_CustomerGeneral.rAddHeader.Text = "Total Purchase From " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " +
                        GeneralFunctions.fnDate(dtTo.Text).ToString("d");
                rep_CustomerGeneral.lblLastPurchase.WidthF = 30;
                rep_CustomerGeneral.lblLastPurchase.Text = "";
                rep_CustomerGeneral.lblLastDate.BorderColor = System.Drawing.Color.SlateGray;
                rep_CustomerGeneral.rLastPurchase.WidthF = 30;
                rep_CustomerGeneral.rLastPurchase.ForeColor = System.Drawing.Color.Transparent;
                rep_CustomerGeneral.lblLastDate.Text = "Last Date";
                //rep_CustomerGeneral.lblLastDate.Width = 200;
                //rep_CustomerGeneral.rLastPurchaseDate.Width = 200;

            }
            else
            {
                rep_CustomerGeneral.rAddHeader.Text = "Last Visit From " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " +
                        GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_CustomerGeneral.lblLastPurchase.WidthF = 20;
                rep_CustomerGeneral.lblLastPurchase.Text = "";
                rep_CustomerGeneral.lblLastPurchase.BorderColor = System.Drawing.Color.SlateGray;
                rep_CustomerGeneral.lblLastDate.BorderColor = System.Drawing.Color.SlateGray;
                rep_CustomerGeneral.rLastPurchase.WidthF = 20;
                rep_CustomerGeneral.rLastPurchase.ForeColor = System.Drawing.Color.Transparent;

                rep_CustomerGeneral.lblTotalPurchase.WidthF = 20;
                rep_CustomerGeneral.lblTotalPurchase.Text = "";
                rep_CustomerGeneral.rTotPurchase.WidthF = 20;
                rep_CustomerGeneral.rTotPurchase.ForeColor = System.Drawing.Color.Transparent;

                rep_CustomerGeneral.ReportFooter.Visible = false;
                rep_CustomerGeneral.lblLastDate.Text = "Last Visit";
                //rep_CustomerGeneral.lblLastPurchase.Width = 1;
                //rep_CustomerGeneral.lblTotalPurchase.Width = 1;
                //rep_CustomerGeneral.rLastPurchase.Width = 1;
                //rep_CustomerGeneral.lblTotalPurchase.Width = 1;
                //rep_CustomerGeneral.lblZip.Width = 125;
                //rep_CustomerGeneral.rZip.Width = 125;
                //rep_CustomerGeneral.lblPhone.Width = 175;
                //rep_CustomerGeneral.rPhone.Width = 175;
                //rep_CustomerGeneral.lblLastDate.Width = 200;
                //rep_CustomerGeneral.rLastPurchaseDate.Width = 200;

            }
            rep_CustomerGeneral.Report.DataSource = dtbl1;

            GeneralFunctions.MakeReportWatermark(rep_CustomerGeneral);
            rep_CustomerGeneral.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CustomerGeneral.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CustomerGeneral.DecimalPlace = Settings.DecimalPlace;

            if (Settings.DecimalPlace == 3) rep_CustomerGeneral.rTotal.Summary.FormatString = "{0:0.000}";
            else rep_CustomerGeneral.rTotal.Summary.FormatString = "{0:0.00}";

            rep_CustomerGeneral.rCustID.DataBindings.Add("Text", dtbl1, "CustID");
            rep_CustomerGeneral.rCustName.DataBindings.Add("Text", dtbl1, "Customer");
            rep_CustomerGeneral.rCompany.DataBindings.Add("Text", dtbl1, "Company");
            rep_CustomerGeneral.rAddress.DataBindings.Add("Text", dtbl1, "Address1");
            rep_CustomerGeneral.rCity.DataBindings.Add("Text", dtbl1, "City");
            rep_CustomerGeneral.rState.DataBindings.Add("Text", dtbl1, "State");
            rep_CustomerGeneral.rZip.DataBindings.Add("Text", dtbl1, "Zip");
            rep_CustomerGeneral.rPhone.DataBindings.Add("Text", dtbl1, "Phone");
            rep_CustomerGeneral.rEmail.DataBindings.Add("Text", dtbl1, "Email");
            rep_CustomerGeneral.rGroup.DataBindings.Add("Text", dtbl1, "CGroup");
            rep_CustomerGeneral.rClass.DataBindings.Add("Text", dtbl1, "CClass");

            rep_CustomerGeneral.rLastPurchaseDate.DataBindings.Add("Text", dtbl1, "DateLastPurchase");
            rep_CustomerGeneral.rLastPurchase.DataBindings.Add("Text", dtbl1, "AmountLastPurchase");
            rep_CustomerGeneral.rTotPurchase.DataBindings.Add("Text", dtbl1, "TotalPurchases");

            rep_CustomerGeneral.rTotal.DataBindings.Add("Text", dtbl1, "TotalPurchases");


            if (eventtype == "Preview")
            {

                try
                {
                    if (Settings.ReportPrinterName != "") rep_CustomerGeneral.PrinterName = Settings.ReportPrinterName;
                    rep_CustomerGeneral.CreateDocument();
                    rep_CustomerGeneral.PrintingSystem.ShowMarginsWarning = false;
                    rep_CustomerGeneral.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CustomerGeneral;
                    window.ShowDialog();

                    //rep_CustomerGeneral.ShowPreviewDialog();


                }
                finally
                {
                    rep_CustomerGeneral.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();

                }
            }

            if (eventtype == "Print")
            {
                rep_CustomerGeneral.CreateDocument();
                rep_CustomerGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CustomerGeneral);
                }
                finally
                {
                    rep_CustomerGeneral.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_CustomerGeneral.CreateDocument();
                rep_CustomerGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = (strType == "T") ? "cust_tot_pur.pdf" : "cust_last_visit.pdf";
                    GeneralFunctions.EmailReport(rep_CustomerGeneral, attachfile, (strType == "T") ? "Customer Total Purchase" : "Customer Last Visit");
                }
                finally
                {
                    rep_CustomerGeneral.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
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
                        string hding = "";
                        if (strType == "T")
                        {
                            hding = "Customer ID" + Settings.ExportDelimiter
                                        + "Customer Name" + Settings.ExportDelimiter
                                        + "Company" + Settings.ExportDelimiter
                                        + "Mailing Address" + Settings.ExportDelimiter
                                        + "City" + Settings.ExportDelimiter
                                        + "State" + Settings.ExportDelimiter
                                        + "Zip" + Settings.ExportDelimiter
                                        + "Phone" + Settings.ExportDelimiter
                                        + "EMail" + Settings.ExportDelimiter
                                        + "Group" + Settings.ExportDelimiter
                                        + "Class" + Settings.ExportDelimiter
                                        + "Last Purchase on" + Settings.ExportDelimiter
                                        + "Last Purchase Amt." + Settings.ExportDelimiter
                                        + "Total Purchase Amt.";
                        }
                        else
                        {
                            hding = "Customer ID" + Settings.ExportDelimiter
                                       + "Customer Name" + Settings.ExportDelimiter
                                       + "Company" + Settings.ExportDelimiter
                                       + "Mailing Address" + Settings.ExportDelimiter
                                       + "City" + Settings.ExportDelimiter
                                       + "State" + Settings.ExportDelimiter
                                       + "Zip" + Settings.ExportDelimiter
                                       + "Phone" + Settings.ExportDelimiter
                                       + "EMail" + Settings.ExportDelimiter
                                       + "Group" + Settings.ExportDelimiter
                                       + "Class" + Settings.ExportDelimiter
                                       + "Last Visit On";
                        }
                        Tex.WriteLine(hding);
                        int i = 0;
                        foreach (DataRow dr in dtbl1.Rows)
                        {
                            i++;
                            string str = "";
                            if (strType == "T")
                            {
                                str = dr["CustID"].ToString() + Settings.ExportDelimiter
                                        + dr["Customer"].ToString() + Settings.ExportDelimiter
                                        + dr["Company"].ToString() + Settings.ExportDelimiter
                                        + dr["Address1"].ToString() + Settings.ExportDelimiter
                                        + dr["City"].ToString() + Settings.ExportDelimiter
                                        + dr["State"].ToString() + Settings.ExportDelimiter
                                        + dr["Zip"].ToString() + Settings.ExportDelimiter
                                        + dr["Phone"].ToString() + Settings.ExportDelimiter
                                        + dr["EMail"].ToString() + Settings.ExportDelimiter
                                        + dr["CGroup"].ToString() + Settings.ExportDelimiter
                                        + dr["CClass"].ToString() + Settings.ExportDelimiter
                                        + dr["DateLastPurchase"].ToString() + Settings.ExportDelimiter
                                        + dr["AmountLastPurchase"].ToString() + Settings.ExportDelimiter
                                        + dr["TotalPurchases"].ToString();
                            }
                            else
                            {
                                str = dr["CustID"].ToString() + Settings.ExportDelimiter
                                        + dr["Customer"].ToString() + Settings.ExportDelimiter
                                        + dr["Company"].ToString() + Settings.ExportDelimiter
                                        + dr["Address1"].ToString() + Settings.ExportDelimiter
                                        + dr["City"].ToString() + Settings.ExportDelimiter
                                        + dr["State"].ToString() + Settings.ExportDelimiter
                                        + dr["Zip"].ToString() + Settings.ExportDelimiter
                                        + dr["Phone"].ToString() + Settings.ExportDelimiter
                                        + dr["EMail"].ToString() + Settings.ExportDelimiter
                                        + dr["CGroup"].ToString() + Settings.ExportDelimiter
                                        + dr["CClass"].ToString() + Settings.ExportDelimiter
                                        + dr["DateLastPurchase"].ToString();
                            }
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

        private void ExecuteSpecialEventReport(string eventtype)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objSales = new PosDataObject.Customer();
            objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objSales.FetchSpecialEvent("Customer", GeneralFunctions.fnDate(dtFrom.EditValue),
                                                GeneralFunctions.fnDate(dtTo.EditValue), rgSort1.IsChecked == true ? 0 : 1, Settings.StoreCode);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }
            OfflineRetailV2.Report.Misc.repNotes rep_Notes = new OfflineRetailV2.Report.Misc.repNotes();
            rep_Notes.rHeader.Text = "Customer Special Events Report";
            rep_Notes.rHeaderDate.Visible = true;
            rep_Notes.rHeaderDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " +
                        GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

            rep_Notes.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_Notes);
            rep_Notes.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_Notes.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_Notes.rH1.Text = "Cust. ID";
            rep_Notes.rH2.Text = "Customer";
            rep_Notes.rH3.Text = "Work Phone";
            rep_Notes.rH4.Text = "Home Phone";
            rep_Notes.rCustID.DataBindings.Add("Text", dtbl, "ID");
            rep_Notes.rCustName.DataBindings.Add("Text", dtbl, "Name");
            rep_Notes.rPh1.DataBindings.Add("Text", dtbl, "Phone1");
            rep_Notes.rPh2.DataBindings.Add("Text", dtbl, "Phone2");
            rep_Notes.rEmail.DataBindings.Add("Text", dtbl, "EMail");
            rep_Notes.rEvents.DataBindings.Add("Text", dtbl, "Event");
            rep_Notes.rDate.DataBindings.Add("Text", dtbl, "Date");
            rep_Notes.rGroup.DataBindings.Add("Text", dtbl, "G");
            rep_Notes.rClass.DataBindings.Add("Text", dtbl, "C");


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_Notes.PrinterName = Settings.ReportPrinterName;
                    rep_Notes.CreateDocument();
                    rep_Notes.PrintingSystem.ShowMarginsWarning = false;
                    rep_Notes.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_Notes;
                    window.ShowDialog();
                    //rep_Notes.ShowPreviewDialog();
                    
                }
                finally
                {
                    rep_Notes.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

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
                    attachfile = "cust_spl_event.pdf";
                    GeneralFunctions.EmailReport(rep_Notes, attachfile, "Customer Special Events");
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
                        string hding = "Customer ID" + Settings.ExportDelimiter
                                        + "Customer Name" + Settings.ExportDelimiter
                                        + "Work Phone" + Settings.ExportDelimiter
                                        + "Home Phone" + Settings.ExportDelimiter
                                        + "EMail" + Settings.ExportDelimiter
                                        + "Group" + Settings.ExportDelimiter
                                        + "Class" + Settings.ExportDelimiter
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
                                        + dr["G"].ToString() + Settings.ExportDelimiter
                                        + dr["C"].ToString() + Settings.ExportDelimiter
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
                if (lkupGroupBy.EditValue.ToString() == "1") ExecutePurchaseReport("T", eventtype);
                if (lkupGroupBy.EditValue.ToString() == "2") ExecutePurchaseReport("L", eventtype);
                if (lkupGroupBy.EditValue.ToString() == "3") ExecuteSpecialEventReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private DataTable GetPurchaseAmount(DateTime f, DateTime t)
        {
            PosDataObject.Sales objSales1 = new PosDataObject.Sales();
            objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable d11 = objSales1.FetchAllCustomerPurchaseAmount1(f, t);
            DataTable d12 = objSales1.FetchAllCustomerPurchaseAmount2(f, t);
            DataTable d1 = d11.DefaultView.ToTable();
            foreach (DataRow dr in d12.Rows)
            {
                bool fl = false;
                foreach (DataRow dr1 in d1.Rows)
                {
                    if (dr["CID"].ToString() == dr1["CID"].ToString())
                    {
                        dr1["TotalPurchases"] = GeneralFunctions.fnDouble(dr1["TotalPurchases"].ToString()) + GeneralFunctions.fnDouble(dr["TotalPurchases"].ToString());
                        fl = true;
                        break;
                    }
                }
                if (!fl)
                {
                    d1.ImportRow(dr);
                }
            }
            DataTable d2 = objSales1.FetchAllCustomerPaymentAmount(f, t);
            if (d2.Rows.Count > 0)
            {
                foreach (DataRow dr1 in d1.Rows)
                {
                    foreach (DataRow dr2 in d2.Rows)
                    {
                        if (GeneralFunctions.fnInt32(dr1["CID"].ToString()) != GeneralFunctions.fnInt32(dr2["CID"].ToString())) continue;
                        if (GeneralFunctions.fnInt32(dr1["CID"].ToString()) == GeneralFunctions.fnInt32(dr2["CID"].ToString()))
                        {
                            dr1["TotalPurchases"] = GeneralFunctions.fnDouble(dr1["TotalPurchases"].ToString()) - GeneralFunctions.fnDouble(dr2["Payment"].ToString());
                        }
                    }
                }
            }
            return d1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Customer Report";
            PopulateReport();
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

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Export");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LkupGroupBy_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (lkupGroupBy.EditValue.ToString() == "3")
            {
                rgSort1.Visibility = Visibility.Visible;
                rgSort2.Visibility = Visibility.Visible;
            }
            else
            {
                rgSort1.Visibility = Visibility.Collapsed;
                rgSort2.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void LkupGroupBy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
