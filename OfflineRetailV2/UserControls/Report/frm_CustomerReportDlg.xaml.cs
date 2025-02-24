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
    /// Interaction logic for frm_CustomerReportDlg.xaml
    /// </summary>
    public partial class frm_CustomerReportDlg : Window
    {
        private int intQty;
        private int intskip = 0;

        public frm_CustomerReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateOrderBy()
        {
            DataTable dtblOrderBy = new DataTable();
            dtblOrderBy.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblOrderBy.Columns.Add("Desc", System.Type.GetType("System.String"));
            dtblOrderBy.Rows.Add(new object[] { "1", "Customer ID" });
            dtblOrderBy.Rows.Add(new object[] { "2", "Customer Name" });
            dtblOrderBy.Rows.Add(new object[] { "3", "Company" });
            dtblOrderBy.Rows.Add(new object[] { "4", "Group" });
            dtblOrderBy.Rows.Add(new object[] { "5", "Class" });
            lkupOrderBy.ItemsSource = dtblOrderBy;

            lkupOrderBy.EditValue = "1";
            dtblOrderBy.Dispose();
        }

        private void PopulateGroup()
        {
            PosDataObject.Group objGroup = new PosDataObject.Group();
            objGroup.Connection = SystemVariables.Conn;
            DataTable dtbl = objGroup.FetchData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckGroup", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblGrp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }


            grdGroup.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void PopulateClass()
        {
            PosDataObject.Class objClass = new PosDataObject.Class();
            objClass.Connection = SystemVariables.Conn;
            DataTable dtbl = objClass.FetchData();
            DataTable dtblCls = new DataTable();
            dtblCls.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblCls.Columns.Add("CheckClass", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblCls.Rows.Add(new object[] { dr["ID"].ToString(), dr["Description"].ToString(), false });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblCls.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdClass.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblCls.Dispose();
            dtblTemp.Dispose();
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                int i = 0;
                int j = 0;
                DataTable dG = grdGroup.ItemsSource as DataTable;

                foreach (DataRow drG in dG.Rows)
                {
                    if (Convert.ToBoolean(drG["CheckGroup"].ToString()))
                    {
                        i++;
                    }
                }
                DataTable dC = grdClass.ItemsSource as DataTable;
                foreach (DataRow drC in dC.Rows)
                {
                    if (Convert.ToBoolean(drC["CheckClass"].ToString()))
                    {
                        j++;
                    }
                }
                dG.Dispose();
                dC.Dispose();
                if ((txtCustID.Text.Trim() == "") && (txtCustName.Text.Trim() == "") &&
                       (txtCompany.Text.Trim() == "") && (txtZip.Text.Trim() == "") &&
                       (i == 0) && (j == 0))
                {
                    if (DocMessage.MsgConfirmation("No criteria selected." + "\n" + "Do you want to go for all records?") == MessageBoxResult.Yes)
                    {
                        if (rgReportType1.IsChecked == true)
                        {
                            if (Settings.ReportHeader == "")
                            {
                                DocMessage.MsgInformation("Enter Store Details in Settings");
                                return;
                            }
                            ExecuteReport(eventtype);
                        }
                        else ExecuteLabel(eventtype);
                    }
                }
                else
                {
                    if (rgReportType1.IsChecked == true) ExecuteReport(eventtype);
                    else ExecuteLabel(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteReport(string eventtype)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCust = new PosDataObject.Customer();
            objCust.Connection = SystemVariables.Conn;
            dtbl = objCust.FetchDataForGeneralReport(txtCustID.Text.Trim(), txtCustName.Text.Trim(), txtCompany.Text.Trim(), txtZip.Text.Trim(),
                                                     grdGroup.ItemsSource as DataTable, grdClass.ItemsSource as DataTable,
                                                     lkupOrderBy.EditValue.ToString(), Settings.StoreCode);

            OfflineRetailV2.Report.Customer.repCustomerGeneral rep_CustomerGeneral = new OfflineRetailV2.Report.Customer.repCustomerGeneral();
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }
            rep_CustomerGeneral.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_CustomerGeneral);
            rep_CustomerGeneral.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_CustomerGeneral.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_CustomerGeneral.DecimalPlace = Settings.DecimalPlace;

            if (Settings.DecimalPlace == 3) rep_CustomerGeneral.rTotal.Summary.FormatString = "{0:0.000}";
            else rep_CustomerGeneral.rTotal.Summary.FormatString = "{0:0.00}";

            rep_CustomerGeneral.rCustID.DataBindings.Add("Text", dtbl, "CustomerID");
            rep_CustomerGeneral.rCustName.DataBindings.Add("Text", dtbl, "CustomerName");
            rep_CustomerGeneral.rCompany.DataBindings.Add("Text", dtbl, "Company");
            rep_CustomerGeneral.rAddress.DataBindings.Add("Text", dtbl, "Address1");
            rep_CustomerGeneral.rCity.DataBindings.Add("Text", dtbl, "City");
            rep_CustomerGeneral.rState.DataBindings.Add("Text", dtbl, "State");
            rep_CustomerGeneral.rZip.DataBindings.Add("Text", dtbl, "Zip");
            rep_CustomerGeneral.rPhone.DataBindings.Add("Text", dtbl, "WorkPhone");
            rep_CustomerGeneral.rEmail.DataBindings.Add("Text", dtbl, "EMail");

            rep_CustomerGeneral.rGroup.DataBindings.Add("Text", dtbl, "CGroup");
            rep_CustomerGeneral.rClass.DataBindings.Add("Text", dtbl, "CClass");

            rep_CustomerGeneral.rLastPurchaseDate.DataBindings.Add("Text", dtbl, "DateLastPurchase");
            rep_CustomerGeneral.rLastPurchase.DataBindings.Add("Text", dtbl, "AmountLastPurchase");
            rep_CustomerGeneral.rTotPurchase.DataBindings.Add("Text", dtbl, "TotalPurchases");

            rep_CustomerGeneral.rTotal.DataBindings.Add("Text", dtbl, "TotalPurchases");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
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
                }
            }

            if (eventtype == "Email")
            {
                rep_CustomerGeneral.CreateDocument();
                rep_CustomerGeneral.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cust.pdf";
                    GeneralFunctions.EmailReport(rep_CustomerGeneral, attachfile, "Customer");
                }
                finally
                {
                    rep_CustomerGeneral.Dispose();
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
                                        + "Company" + Settings.ExportDelimiter
                                        + "Mailing Address" + Settings.ExportDelimiter
                                        + "City" + Settings.ExportDelimiter
                                        + "State" + Settings.ExportDelimiter
                                        + "Zip" + Settings.ExportDelimiter
                                        + "Work Phone" + Settings.ExportDelimiter
                                        + "EMail" + Settings.ExportDelimiter
                                        + "Group" + Settings.ExportDelimiter
                                        + "Class" + Settings.ExportDelimiter
                                        + "Last Purchase on" + Settings.ExportDelimiter
                                        + "Last Purchase Amt." + Settings.ExportDelimiter
                                        + "Total Purchase Amt.";
                        Tex.WriteLine(hding);
                        int i = 0;
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            i++;
                            string str = dr["CustomerID"].ToString() + Settings.ExportDelimiter
                                        + dr["CustomerName"].ToString() + Settings.ExportDelimiter
                                        + dr["Company"].ToString() + Settings.ExportDelimiter
                                        + dr["Address1"].ToString() + Settings.ExportDelimiter
                                        + dr["City"].ToString() + Settings.ExportDelimiter
                                        + dr["State"].ToString() + Settings.ExportDelimiter
                                        + dr["Zip"].ToString() + Settings.ExportDelimiter
                                        + dr["WorkPhone"].ToString() + Settings.ExportDelimiter
                                        + dr["EMail"].ToString() + Settings.ExportDelimiter
                                        + dr["CGroup"].ToString() + Settings.ExportDelimiter
                                        + dr["CClass"].ToString() + Settings.ExportDelimiter
                                        + dr["DateLastPurchase"].ToString() + Settings.ExportDelimiter
                                        + dr["AmountLastPurchase"].ToString() + Settings.ExportDelimiter
                                        + dr["TotalPurchases"].ToString();
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

        private void setPrintPosition()
        {
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
        }

        private void ExecuteLabel(string eventtype)
        {
            setPrintPosition();

            DataTable dtbl = new DataTable();
            PosDataObject.Customer objCust = new PosDataObject.Customer();
            objCust.Connection = SystemVariables.Conn;
            dtbl = objCust.FetchDataForGeneralReport(txtCustID.Text.Trim(), txtCustName.Text.Trim(), txtCompany.Text.Trim(),
                                                     txtZip.Text.Trim(), grdGroup.ItemsSource as DataTable, grdClass.ItemsSource as DataTable,
                                                     lkupOrderBy.EditValue.ToString(), Settings.StoreCode);

            DataTable dlbl = new DataTable();
            dlbl.Columns.Add("Company", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Address", System.Type.GetType("System.String"));
            dlbl.Columns.Add("Attn", System.Type.GetType("System.String"));


            DataTable dtblActive = dtbl.Clone();
            DataRow[] d = dtbl.Select("ActiveStatus = 'Y' ");
            foreach (DataRow dr in d)
            {
                dtblActive.ImportRow(dr);
            }

            if (dtblActive.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                return;
            }



            if (rgReportType2.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["MailingLabelCompany"].ToString(),
                                                    dr1["MailingLabelAddress"].ToString(),
                                                    dr1["MailingLabelAttn"].ToString()});
                }
            }

            if (rgReportType2.IsChecked == true)
            {
                foreach (DataRow dr1 in dtblActive.Rows)
                {
                    dlbl.Rows.Add(new object[] {    dr1["ShippingLabelCompany"].ToString(),
                                                    dr1["ShippingLabelAddress"].ToString(),
                                                    dr1["ShippingLabelAttn"].ToString()});
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

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CustomerLabel.PrinterName = Settings.ReportPrinterName;
                    rep_CustomerLabel.CreateDocument();
                    rep_CustomerLabel.PrintingSystem.ShowMarginsWarning = false;
                    rep_CustomerLabel.PrintingSystem.ShowPrintStatusDialog = false;

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CustomerLabel;
                    window.ShowDialog();

                    //rep_CustomerLabel.ShowPreviewDialog();
                    
                }
                finally
                {
                    rep_CustomerLabel.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                    dtblActive.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_CustomerLabel.CreateDocument();
                rep_CustomerLabel.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_CustomerLabel);
                }
                finally
                {
                    rep_CustomerLabel.Dispose();
                    dtbl.Dispose();
                    dlbl.Dispose();
                    dtblActive.Dispose();
                }
            }

            if (eventtype == "Email")
            {
                rep_CustomerLabel.CreateDocument();
                rep_CustomerLabel.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cust_label.pdf";
                    GeneralFunctions.EmailReport(rep_CustomerLabel, attachfile, "Customer Label");
                }
                finally
                {
                    rep_CustomerLabel.Dispose();
                    dtbl.Dispose();
                    dlbl.Dispose();
                    dtblActive.Dispose();
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
                        string hding = "Company" + Settings.ExportDelimiter
                                        + "Address" + Settings.ExportDelimiter
                                        + "Attn";
                        Tex.WriteLine(hding);
                        int i = 0;
                        foreach (DataRow dr in dlbl.Rows)
                        {
                            i++;
                            if (i == dlbl.Rows.Count) continue;
                            string str = dr["Company"].ToString() + Settings.ExportDelimiter
                                        + dr["Address"].ToString() + Settings.ExportDelimiter
                                        + dr["Attn"].ToString();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Customer Report";
            PopulateGroup();
            PopulateClass();
            PopulateOrderBy();
            chkGroup.IsChecked = true;
            chkClass.IsChecked = true;
            rgReportType1.IsChecked = true;
        }

        private void ChkGroup_Checked(object sender, RoutedEventArgs e)
        {
            if (chkGroup.IsChecked == true)
            {
                chkGroup.Content = "Uncheck All";
                DataTable dtbl = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckGroup"] = true;
                }
                grdGroup.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkGroup.Content = "Check All";
                DataTable dtbl1 = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckGroup"] = false;
                }
                grdGroup.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void ChkClass_Checked(object sender, RoutedEventArgs e)
        {
            if (chkClass.IsChecked == true)
            {
                chkClass.Content = "Uncheck All";
                DataTable dtbl = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckClass"] = true;
                }
                grdClass.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkClass.Content = "Check All";
                DataTable dtbl1 = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckClass"] = false;
                }
                grdClass.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private void RgReportType1_Checked(object sender, RoutedEventArgs e)
        {
            pnlLpos.Visibility = rgReportType1.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGroup")
            {
                grdGroup.SetCellValue(e.RowHandle, colCheckGroup, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckGroup"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdGroup.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkGroup.IsChecked = true;
                    chkGroup.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkGroup.IsChecked = false;
                    chkGroup.Content = "Check All";
                }
                dtbl.Dispose();
            }
        }

        private void GridView2_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckClass")
            {
                grdClass.SetCellValue(e.RowHandle, colCheckClass, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdClass.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckClass"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdClass.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    chkClass.IsChecked = true;
                    chkClass.Content = "Uncheck All";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    chkClass.IsChecked = false;
                    chkClass.Content = "Check All";
                }
                dtbl.Dispose();
            }
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

        private void LkupOrderBy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
