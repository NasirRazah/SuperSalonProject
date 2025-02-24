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
    /// Interaction logic for frm_PackinglistDlg.xaml
    /// </summary>
    public partial class frm_PackinglistDlg : Window
    {
        private bool blFromPOS;
        public bool FromPOS
        {
            get { return blFromPOS; }
            set { blFromPOS = value; }
        }

        public frm_PackinglistDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private bool IsValidInput()
        {
            if (dtFrom.EditValue == null)
            {
                DocMessage.MsgInformation("Select From Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }

            if (dtTo.EditValue == null)
            {
                DocMessage.MsgInformation("Select To Date");
                GeneralFunctions.SetFocus(dtTo);
                return false;
            }
            return true;
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (IsValidInput()) ExecuteReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteReport(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings");
                    return;
                }


                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl = objSales.FetchPackingListData(cmbGroup.Text, GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue),
                                                     Settings.StoreCode);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                DataTable dtblC = new DataTable();
                PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objSales1.OperateStore = Settings.StoreCode;
                dtblC = objSales1.FetchCustomer();

                DataTable repdtbl = new DataTable();
                repdtbl.Columns.Add("SortOrder", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("ItemName", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("Qty", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("CID", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("CName", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("CInfo1", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("CInfo2", System.Type.GetType("System.String"));

                string prmCName = "";
                string prmCInfo1 = "";
                string prmCInfo2 = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    if (cmbGroup.SelectedIndex == 0)
                    {
                        if (GeneralFunctions.fnInt32(dr["CID"].ToString()) == 0) continue;
                    }

                    prmCName = "";
                    prmCInfo1 = "";
                    prmCInfo2 = "";

                    foreach (DataRow dr1 in dtblC.Rows)
                    {

                        if (dr1["ID"].ToString() != dr["CID"].ToString()) continue;
                        if (dr1["ID"].ToString() == dr["CID"].ToString())
                        {
                            prmCName = dr1["CustomerName"].ToString();
                            prmCInfo1 = dr1["MailAddress"].ToString();
                            if (cmbGroup.Text == "Customer")
                            {
                                if (dr1["WorkPhone"].ToString() != "")
                                    prmCInfo2 = "Ph." + " "
                                        + dr1["WorkPhone"].ToString();

                                if (dr1["Email"].ToString() != "")
                                {
                                    prmCInfo2 = prmCInfo2 == "" ? "Email" + " : "
                                        + dr1["Email"].ToString() : prmCInfo2 + "  "
                                        + "Email" + " : "
                                        + dr1["Email"].ToString();
                                }
                            }
                            else
                            {
                                prmCInfo2 = dr1["WorkPhone"].ToString();
                            }
                            break;
                        }
                    }

                    repdtbl.Rows.Add(new object[] {"C",
                                                   dr["SKU"].ToString(),
                                                   dr["ItemName"].ToString(),
                                                   dr["Qty"].ToString(),
                                                   dr["CID"].ToString(),
                                                   prmCName,prmCInfo1,prmCInfo2});
                }

                if (cmbGroup.SelectedIndex == 0)
                {
                    foreach (DataRow dr2 in dtbl.Rows)
                    {
                        if (GeneralFunctions.fnInt32(dr2["CID"].ToString()) != 0) continue;
                        repdtbl.Rows.Add(new object[] {
                                                   "B",
                                                   dr2["SKU"].ToString(),
                                                   dr2["ItemName"].ToString(),
                                                   dr2["Qty"].ToString(),
                                                   "0", "","",""});
                    }
                }

                if (cmbGroup.SelectedIndex == 0)
                {
                    OfflineRetailV2.Report.Sales.repPackingListCust rep_PackingListCust = new OfflineRetailV2.Report.Sales.repPackingListCust();

                    rep_PackingListCust.Report.DataSource = repdtbl;
                    GeneralFunctions.MakeReportWatermark(rep_PackingListCust);
                    rep_PackingListCust.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_PackingListCust.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_PackingListCust.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                    rep_PackingListCust.rHeader.Text = "Customerwise Packing List";

                    rep_PackingListCust.GroupHeader1.GroupFields.Add(rep_PackingListCust.CreateGroupField("CName"));
                    rep_PackingListCust.GroupHeader2.GroupFields.Add(rep_PackingListCust.CreateGroupField("SortOrder"));
                    rep_PackingListCust.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
                    rep_PackingListCust.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;

                    rep_PackingListCust.rGroupID.DataBindings.Add("Text", repdtbl, "CName");
                    rep_PackingListCust.rGroupDesc.DataBindings.Add("Text", repdtbl, "CInfo1");
                    rep_PackingListCust.rGroupParam.DataBindings.Add("Text", repdtbl, "CInfo2");
                    //rep_PackingListCust.rtest.DataBindings.Add("Text", repdtbl, "SortOrder");
                    rep_PackingListCust.rSKU.DataBindings.Add("Text", repdtbl, "SKU");
                    rep_PackingListCust.rProduct.DataBindings.Add("Text", repdtbl, "ItemName");
                    rep_PackingListCust.rQty.DataBindings.Add("Text", repdtbl, "Qty");

                    if (eventtype == "Preview")
                    {
                        //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                        try
                        {
                            if (Settings.ReportPrinterName != "") rep_PackingListCust.PrinterName = Settings.ReportPrinterName;
                            rep_PackingListCust.CreateDocument();
                            rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                            rep_PackingListCust.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_PackingListCust.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_PackingListCust;
                            window.ShowDialog();

                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();

                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }

                    if (eventtype == "Print")
                    {
                        rep_PackingListCust.CreateDocument();
                        rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_PackingListCust);
                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }


                    if (eventtype == "Email")
                    {
                        rep_PackingListCust.CreateDocument();
                        rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            attachfile = "packing_by_cust.pdf";
                            GeneralFunctions.EmailReport(rep_PackingListCust, attachfile, "Customer wise Packing List");
                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }

                }

                if (cmbGroup.SelectedIndex == 1)
                {
                    OfflineRetailV2.Report.Sales.repPackingListItem rep_PackingListCust = new OfflineRetailV2.Report.Sales.repPackingListItem();

                    rep_PackingListCust.Report.DataSource = repdtbl;
                    GeneralFunctions.MakeReportWatermark(rep_PackingListCust);
                    rep_PackingListCust.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_PackingListCust.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_PackingListCust.rDate.Text = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                    rep_PackingListCust.rHeader.Text = "Packing List (Group By Item)";

                    rep_PackingListCust.GroupHeader1.GroupFields.Add(rep_PackingListCust.CreateGroupField("SKU"));
                    rep_PackingListCust.rGroupID.DataBindings.Add("Text", repdtbl, "SKU");
                    rep_PackingListCust.rGroupDesc.DataBindings.Add("Text", repdtbl, "ItemName");

                    rep_PackingListCust.rCustName.DataBindings.Add("Text", repdtbl, "CName");
                    rep_PackingListCust.rCustAdd.DataBindings.Add("Text", repdtbl, "CInfo1");
                    rep_PackingListCust.rCustPhone.DataBindings.Add("Text", repdtbl, "CInfo2");
                    rep_PackingListCust.rQty.DataBindings.Add("Text", repdtbl, "Qty");

                    if (eventtype == "Preview")
                    {

                        //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                        try
                        {
                            if (Settings.ReportPrinterName != "") rep_PackingListCust.PrinterName = Settings.ReportPrinterName;
                            rep_PackingListCust.CreateDocument();
                            rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                            rep_PackingListCust.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_PackingListCust.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_PackingListCust;
                            window.ShowDialog();

                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();

                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }

                    if (eventtype == "Print")
                    {
                        rep_PackingListCust.CreateDocument();
                        rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_PackingListCust);
                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }


                    if (eventtype == "Email")
                    {
                        rep_PackingListCust.CreateDocument();
                        rep_PackingListCust.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            attachfile = "packing_by_cust.pdf";
                            GeneralFunctions.EmailReport(rep_PackingListCust, attachfile, "Item wise Packing List");
                        }
                        finally
                        {
                            rep_PackingListCust.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            dtblC.Dispose();
                        }
                    }

                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Packing List";
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
            ClickButton("Link");
        }

        private void CmbGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
