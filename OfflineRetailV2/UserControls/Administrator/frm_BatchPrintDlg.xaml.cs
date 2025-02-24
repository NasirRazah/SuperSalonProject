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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_BatchPrintDlg.xaml
    /// </summary>
    public partial class frm_BatchPrintDlg : Window
    {
        private int intskip = 0;
        private int intTotal = 0;

        public frm_BatchPrintDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        private DataTable LoadPrintDataTable()
        {
            DataTable dtblB = new DataTable();
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("COMPANY", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DESC", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PRICE", System.Type.GetType("System.String"));

            PosDataObject.Product objProdt1 = new PosDataObject.Product();
            objProdt1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblB = objProdt1.FetchPrintlabelData();
            foreach (DataRow dr in dtblB.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["Qty"].ToString()) == 0) continue;
                string strPrice = "";
                if (dr["NoPriceOnLabel"].ToString() == "N")
                {
                    strPrice = dr["PriceA"].ToString();
                    if (GeneralFunctions.fnInt32(dr["DecimalPlace"].ToString()) == 3) strPrice = SystemVariables.CurrencySymbol + " " + GeneralFunctions.fnDouble(strPrice).ToString("f3");
                    else strPrice = SystemVariables.CurrencySymbol + " " + GeneralFunctions.fnDouble(strPrice).ToString("f");
                }

                for (int i = 1; i <= GeneralFunctions.fnInt32(dr["Qty"].ToString()); i++)
                {
                    dtbl.Rows.Add(new object[] { Settings.Company, dr["SKU"].ToString(), dr["Description"].ToString(), strPrice });
                }
                intTotal = intTotal + GeneralFunctions.fnInt32(dr["Qty"].ToString());
            }
            if (dtbl.Rows.Count > 0)
            {
                dtbl.Rows.Add(new object[] { Settings.Company, "Dummy", "Dummy", "Dummy" });
            }
            dtblB.Dispose();
            return dtbl;
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

        private void ExecuteLabel()
        {
            setPrintPosition();
            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

            DataTable dtbl = new DataTable();
            dtbl = LoadPrintDataTable();

            OfflineRetailV2.Report.Product.repPrintLabel1 rep_PrintLabel = new OfflineRetailV2.Report.Product.repPrintLabel1(intskip, intTotal);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtbl);
            rep_PrintLabel.DataSource = ds;

            try
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_PrintLabel.PrinterName = Settings.ReportPrinterName;
                    rep_PrintLabel.CreateDocument();
                    rep_PrintLabel.PrintingSystem.ShowMarginsWarning = false;
                    rep_PrintLabel.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_PrintLabel.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_PrintLabel;
                    window.ShowDialog();

                }
                finally
                {
                    rep_PrintLabel.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                }




                DataTable dtblB = new DataTable();
                PosDataObject.Product objProdt1 = new PosDataObject.Product();
                objProdt1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtblB = objProdt1.FetchPrintlabelData();

                foreach (DataRow dr in dtblB.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr["Qty"].ToString()) == 0) continue;

                    if (dr["NoPriceOnLabel"].ToString() == "N")
                    {
                        AdustData(dr["SKU"].ToString(), GeneralFunctions.fnInt32(dr["Qty"].ToString()));
                    }
                }
            }

            finally
            {
                //frm_PreviewControl.Dispose();
                rep_PrintLabel.Dispose();
                dtbl.Dispose();
            }
        }

        private void AdustData(string sku, int qty)
        {

            PosDataObject.Recv or = new PosDataObject.Recv();
            or.Connection = SystemVariables.Conn;
            string ret1 = or.UpdatePrintQty(sku, qty);

            PosDataObject.Recv or2 = new PosDataObject.Recv();
            or2.Connection = SystemVariables.Conn;
            string ret2 = or.DeletePrintLabel(sku);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Batch Print Barcode Label";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
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
    }
}
