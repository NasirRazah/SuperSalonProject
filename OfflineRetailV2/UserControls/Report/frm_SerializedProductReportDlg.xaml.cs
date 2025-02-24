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
    /// Interaction logic for frm_SerializedProductReportDlg.xaml
    /// </summary>
    public partial class frm_SerializedProductReportDlg : Window
    {
        public frm_SerializedProductReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateProduct()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dtbl = objProduct.FetchSerialProductData();
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CheckDepartment", System.Type.GetType("System.Boolean"));
            dtblGrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["SKU"].ToString(), dr["Description"].ToString(), false, strip });
            }
            grdGroup.ItemsSource = dtblGrp;
            dtbl.Dispose();
            dtblGrp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Serialized Product Report";
            PopulateProduct();
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

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
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
                    DocMessage.MsgInformation("Please check atleast one product");
                    return;
                }
                ExecuteGroupReport(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ExecuteGroupReport(string eventtype)
        {

            DataTable dtbl = new DataTable();
            PosDataObject.Product objCust = new PosDataObject.Product();
            objCust.Connection = SystemVariables.Conn;
            dtbl = objCust.FetchDataForSerializedProductReport(grdGroup.ItemsSource as DataTable);

            OfflineRetailV2.Report.Product.repSerializedProduct rep_SerializedProduct = new OfflineRetailV2.Report.Product.repSerializedProduct();

            rep_SerializedProduct.Report.DataSource = dtbl;
            GeneralFunctions.MakeReportWatermark(rep_SerializedProduct);
            rep_SerializedProduct.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_SerializedProduct.rReportHeader.Text = Settings.ReportHeader_Address;


            rep_SerializedProduct.GroupHeader1.GroupFields.Add(rep_SerializedProduct.CreateGroupField("ID"));

            rep_SerializedProduct.rGroupID.DataBindings.Add("Text", dtbl, "SKU");
            rep_SerializedProduct.rGroupDesc.DataBindings.Add("Text", dtbl, "ProductDesc");
            rep_SerializedProduct.rSL1.DataBindings.Add("Text", dtbl, "SL1");
            rep_SerializedProduct.rSL2.DataBindings.Add("Text", dtbl, "SL2");
            rep_SerializedProduct.rSL3.DataBindings.Add("Text", dtbl, "SL3");
            rep_SerializedProduct.rINV.DataBindings.Add("Text", dtbl, "InvoiceNo");

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_SerializedProduct.PrinterName = Settings.ReportPrinterName;
                    rep_SerializedProduct.CreateDocument();
                    rep_SerializedProduct.PrintingSystem.ShowMarginsWarning = false;
                    rep_SerializedProduct.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_SerializedProduct.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_SerializedProduct;
                    window.ShowDialog();

                }
                finally
                {
                    rep_SerializedProduct.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep_SerializedProduct.CreateDocument();
                rep_SerializedProduct.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_SerializedProduct);
                }
                finally
                {
                    rep_SerializedProduct.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_SerializedProduct.CreateDocument();
                rep_SerializedProduct.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "prod_sl.pdf";
                    GeneralFunctions.EmailReport(rep_SerializedProduct, attachfile, "Serialized Products");
                }
                finally
                {
                    rep_SerializedProduct.Dispose();
                    dtbl.Dispose();
                }
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
