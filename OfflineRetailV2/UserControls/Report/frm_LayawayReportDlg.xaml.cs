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
    /// Interaction logic for frm_LayawayReportDlg.xaml
    /// </summary>
    public partial class frm_LayawayReportDlg : Window
    {
        public frm_LayawayReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateCustomer()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dtbl = objCustomer.FetchLookup(Settings.StoreCode);
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CustomerID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Company", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckDepartment", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["CustomerID"].ToString(), dr["Customer"].ToString(), dr["Company"].ToString(), false });
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

        private void ExecuteCustomerReport(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }



            DataTable dtbl = new DataTable();
            PosDataObject.Sales objSales = new PosDataObject.Sales();
            objSales.Connection = SystemVariables.Conn;
            dtbl = objSales.FetchCustomerLayawayHeaderData(grdGroup.ItemsSource as DataTable, chkActive.IsChecked == true ? true : false);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Layaway.repLayawayCust rep_LayawayCust = new OfflineRetailV2.Report.Layaway.repLayawayCust();
            GeneralFunctions.MakeReportWatermark(rep_LayawayCust);
            rep_LayawayCust.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LayawayCust.rReportHeader.Text = Settings.ReportHeader_Address;
            //rep_LayawayCust.DecimalPlace = Settings.DecimalPlace;

            DataTable p = new DataTable("Parent");
            p.Columns.Add("CustID", System.Type.GetType("System.String"));
            p.Columns.Add("Customer", System.Type.GetType("System.String"));
            p.Columns.Add("Address", System.Type.GetType("System.String"));
            p.Columns.Add("Email", System.Type.GetType("System.String"));
            p.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            p.Columns.Add("LayawayNo", System.Type.GetType("System.Int32"));
            p.Columns.Add("SKU", System.Type.GetType("System.String"));
            p.Columns.Add("Description", System.Type.GetType("System.String"));
            p.Columns.Add("Qty", System.Type.GetType("System.String"));
            p.Columns.Add("TotalSale", System.Type.GetType("System.String"));
            p.Columns.Add("DateDue", System.Type.GetType("System.String"));
            p.Columns.Add("Paid", System.Type.GetType("System.String"));
            p.Columns.Add("Balance", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl.Rows)
            {
                DataRow r1 = p.NewRow();
                r1["CustID"] = dr["CustID"].ToString();
                r1["Customer"] = dr["Customer"].ToString();
                r1["Address"] = dr["Address"].ToString();
                r1["Email"] = dr["Email"].ToString();
                r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                r1["LayawayNo"] = GeneralFunctions.fnInt32(dr["LayawayNo"].ToString());
                r1["Description"] = dr["Description"].ToString();
                r1["Qty"] = dr["Qty"].ToString();
                r1["TotalSale"] = dr["TotalSale"].ToString();
                r1["DateDue"] = dr["DateDue"].ToString();
                r1["Paid"] = "0.00";
                r1["Balance"] = "0.00";

                p.Rows.Add(r1);
            }

            DataTable dtbl1 = new DataTable();
            PosDataObject.Sales objProduct1 = new PosDataObject.Sales();
            objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl1 = objProduct1.FetchCustomerLayawayDetailData(grdGroup.ItemsSource as DataTable, chkActive.IsChecked == true ? true : false);

            DataTable c = new DataTable("Child");

            c.Columns.Add("CustID", System.Type.GetType("System.String"));
            c.Columns.Add("Customer", System.Type.GetType("System.String"));
            c.Columns.Add("Address", System.Type.GetType("System.String"));
            c.Columns.Add("Email", System.Type.GetType("System.String"));
            c.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            c.Columns.Add("LayawayNo", System.Type.GetType("System.Int32"));
            c.Columns.Add("SKU", System.Type.GetType("System.String"));
            c.Columns.Add("Description", System.Type.GetType("System.String"));
            c.Columns.Add("Qty", System.Type.GetType("System.String"));
            c.Columns.Add("TotalSale", System.Type.GetType("System.String"));
            c.Columns.Add("DateDue", System.Type.GetType("System.String"));
            c.Columns.Add("Paid", System.Type.GetType("System.String"));
            c.Columns.Add("Balance", System.Type.GetType("System.String"));
            c.Columns.Add("TransactionNo", System.Type.GetType("System.String"));
            c.Columns.Add("PaymentDateTime", System.Type.GetType("System.String"));
            c.Columns.Add("PaymentDate", System.Type.GetType("System.String"));
            c.Columns.Add("Payment", System.Type.GetType("System.String"));
            c.Columns.Add("TenderType", System.Type.GetType("System.String"));


            foreach (DataRow dr in dtbl1.Rows)
            {
                double paidamt = 0;
                double balamt = 0;

                DataRow r1 = c.NewRow();
                string a1 = "", a2 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "", a21 = "";
                int a3 = 0;
                foreach (DataRow dr1 in p.Rows)
                {

                    if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                    {
                        DataRow[] drT = dtbl1.Select("InvoiceNo = '" + dr["InvoiceNo"].ToString() + "'");
                        foreach (DataRow d in drT)
                        {
                            paidamt = paidamt + GeneralFunctions.fnDouble(d["Payment"].ToString());
                        }
                        a1 = dr1["CustID"].ToString();
                        a2 = dr1["Customer"].ToString();
                        a21 = dr1["Address"].ToString();
                        a11 = dr1["Email"].ToString();
                        a3 = GeneralFunctions.fnInt32(dr1["LayawayNo"].ToString());
                        a4 = dr1["SKU"].ToString();
                        a5 = dr1["Description"].ToString();
                        a6 = dr1["TotalSale"].ToString();
                        a7 = dr1["DateDue"].ToString();
                        balamt = GeneralFunctions.fnDouble(dr1["TotalSale"].ToString()) - paidamt;

                        dr1["Paid"] = paidamt.ToString("f2");
                        dr1["Balance"] = balamt.ToString("f2");
                        break;
                    }
                }



                r1["CustID"] = a1;
                r1["Customer"] = a2;
                r1["Address"] = a21;
                r1["Email"] = a11;
                r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                r1["LayawayNo"] = a3;
                r1["SKU"] = a4;
                r1["Description"] = a5;
                r1["TotalSale"] = a6;
                r1["DateDue"] = a7;
                r1["Paid"] = paidamt.ToString("f2");
                r1["Balance"] = balamt.ToString("f2");
                r1["TransactionNo"] = dr["TransactionNo"].ToString();
                r1["PaymentDateTime"] = dr["PaymentDate"].ToString();
                r1["PaymentDate"] = GeneralFunctions.fnDate(dr["PaymentDate"].ToString()).ToString(SystemVariables.DateFormat);
                r1["Payment"] = dr["Payment"].ToString();
                r1["TenderType"] = dr["TenderType"].ToString();
                c.Rows.Add(r1);

            }

            DataSet ds = new DataSet();
            ds.Tables.Add(p);
            ds.Tables.Add(c);


            DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["InvoiceNo"],
            ds.Tables["Child"].Columns["InvoiceNo"]);
            ds.Relations.Add(relation);
            //relation.Nested = true;

            rep_LayawayCust.GroupHeader2.GroupFields.Add(rep_LayawayCust.CreateGroupField("CustID"));
            rep_LayawayCust.GroupHeader1.GroupFields.Add(rep_LayawayCust.CreateGroupField("LayawayNo"));
            rep_LayawayCust.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            rep_LayawayCust.GroupHeader3.GroupFields.Add(rep_LayawayCust.CreateGroupField("PaymentDateTime"));

            rep_LayawayCust.DataSource = ds;


            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_LayawayCust.PrinterName = Settings.ReportPrinterName;
                    rep_LayawayCust.CreateDocument();
                    rep_LayawayCust.PrintingSystem.ShowMarginsWarning = false;
                    rep_LayawayCust.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_LayawayCust.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_LayawayCust;
                    window.ShowDialog();

                }
                finally
                {
                    rep_LayawayCust.Dispose();


                    dtbl.Dispose();
                }
            }


            if (eventtype == "Print")
            {
                rep_LayawayCust.CreateDocument();
                rep_LayawayCust.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_LayawayCust);
                }
                finally
                {
                    rep_LayawayCust.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_LayawayCust.CreateDocument();
                rep_LayawayCust.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "layaway_cust.pdf";
                    GeneralFunctions.EmailReport(rep_LayawayCust, attachfile, "Customer wise Layaway");
                }
                finally
                {
                    rep_LayawayCust.Dispose();
                    dtbl.Dispose();
                }
            }
        }

        private void ExecuteProductReport(string eventtype)
        {


            DataTable dtbl = new DataTable();
            PosDataObject.Sales objCust = new PosDataObject.Sales();
            objCust.Connection = SystemVariables.Conn;
            dtbl = objCust.FetchProductLayawayData();

            OfflineRetailV2.Report.Layaway.repLayawayProduct rep_LayawayProduct = new OfflineRetailV2.Report.Layaway.repLayawayProduct();

            rep_LayawayProduct.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_LayawayProduct);
            rep_LayawayProduct.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LayawayProduct.rReportHeader.Text = Settings.ReportHeader_Address;


            rep_LayawayProduct.GroupHeader1.GroupFields.Add(rep_LayawayProduct.CreateGroupField("SKU"));

            rep_LayawayProduct.rSKU.DataBindings.Add("Text", dtbl, "SKU");
            rep_LayawayProduct.rProduct.DataBindings.Add("Text", dtbl, "Description");
            rep_LayawayProduct.rDept.DataBindings.Add("Text", dtbl, "Department");
            rep_LayawayProduct.rPrice.DataBindings.Add("Text", dtbl, "Price");
            rep_LayawayProduct.rohQty.DataBindings.Add("Text", dtbl, "OnHandQty");
            rep_LayawayProduct.rlQty.DataBindings.Add("Text", dtbl, "OnLayawayQty");
            rep_LayawayProduct.raQty.DataBindings.Add("Text", dtbl, "AdjQty");
            rep_LayawayProduct.rCustID.DataBindings.Add("Text", dtbl, "CustID");
            rep_LayawayProduct.rCustomer.DataBindings.Add("Text", dtbl, "Customer");
            rep_LayawayProduct.rEmail.DataBindings.Add("Text", dtbl, "Email");
            rep_LayawayProduct.rQty.DataBindings.Add("Text", dtbl, "Qty");

            if (eventtype == "Preview")
            {
                //frm_PreviewControl frm_PreviewControl = new frm_PreviewControl();
                try
                {
                    

                    if (Settings.ReportPrinterName != "") rep_LayawayProduct.PrinterName = Settings.ReportPrinterName;
                    rep_LayawayProduct.CreateDocument();
                    rep_LayawayProduct.PrintingSystem.ShowMarginsWarning = false;
                    rep_LayawayProduct.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_LayawayProduct.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_LayawayProduct;
                    window.ShowDialog();
                }

                finally
                {
                    //frm_PreviewControl.Close();
                    rep_LayawayProduct.Dispose();
                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_LayawayProduct.CreateDocument();
                rep_LayawayProduct.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_LayawayProduct);
                }
                finally
                {
                    rep_LayawayProduct.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_LayawayProduct.CreateDocument();
                rep_LayawayProduct.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "layaway_prod.pdf";
                    GeneralFunctions.EmailReport(rep_LayawayProduct, attachfile, "Product wise Layaway");
                }
                finally
                {
                    rep_LayawayProduct.Dispose();
                    dtbl.Dispose();
                }
            }
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (rgReportType1.IsChecked == true)
                {
                    int i = 0;
                    DataTable dG = grdGroup.ItemsSource as DataTable;

                    foreach (DataRow drG in dG.Rows)
                    {
                        if (Convert.ToBoolean(drG["CheckDepartment"].ToString()))
                        {
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        DocMessage.MsgInformation("Please check atleast one customer.");
                        return;
                    }
                    ExecuteCustomerReport(eventtype);
                }
                if (rgReportType2.IsChecked == true)
                {
                    ExecuteProductReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Layaway Report";
            rgReportType1.IsChecked = true;
            PopulateCustomer();
            chkGroup.IsChecked = true;
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

        private void RgReportType1_Checked(object sender, RoutedEventArgs e)
        {
            if (rgReportType1.IsChecked == true)
            {
                grdGroup.IsEnabled = true;
                chkGroup.IsEnabled = true;
                chkActive.IsEnabled = true;
            }
            if (rgReportType2.IsChecked == true)
            {
                grdGroup.IsEnabled = false;
                chkGroup.IsEnabled = false;
                chkActive.IsEnabled = false;
            }
        }

        private void ChkGroup_Checked(object sender, RoutedEventArgs e)
        {
            if (chkGroup.IsChecked == true)
            {
                chkGroup.Content = "Uncheck All";
                DataTable dtbl = grdGroup.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckDepartment"] = true;
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
                    dr1["CheckDepartment"] = false;
                }
                grdGroup.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
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
                    if (Convert.ToBoolean(dr["CheckDepartment"]))
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
    }
}
