using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_GiftCertBrwUC.xaml
    /// </summary>
    public partial class frm_GiftCertBrwUC : UserControl
    {
        public frm_GiftCertBrwUC()
        {
            InitializeComponent();
        }

        private bool blPOS;
        private int rowPos = 0;
        private int intSelectedRowHandle;
        private GridColumn _searchcol;
        public bool bPOS
        {
            get { return blPOS; }
            set { blPOS = value; }
        }

        private string strcd;

        public string storecd
        {
            get { return strcd; }
            set { strcd = value; }
        }

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Host");
        }

        public void FetchGC()
        {
            PosDataObject.POS objcout = new PosDataObject.POS();
            objcout.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = GCRecords();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }



            grdGC.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dtbl.Dispose();
            
        }

        private DataTable GCRecords()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            DataTable dtbl = new DataTable();


            dtbl.Columns.Add("IssueStore", System.Type.GetType("System.String"));
            dtbl.Columns.Add("GiftCertID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Amount", System.Type.GetType("System.String"));

            PosDataObject.POS objcout1 = new PosDataObject.POS();
            objcout1.Connection = SystemVariables.Conn;
            dt1 = objcout1.ShowDistinctGiftCert(GeneralFunctions.fnDate(DateTime.Today));

            PosDataObject.POS objcout2 = new PosDataObject.POS();
            objcout2.Connection = SystemVariables.Conn;
            dt2 = objcout2.ShowGiftCertList(GeneralFunctions.fnDate(DateTime.Today), SystemVariables.DateFormat);

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (Settings.StoreCode == dr1["IssueStore"].ToString()) continue;
                double tenderamt = 0;
                double addamt = 0;
                foreach (DataRow dr2 in dt2.Rows)
                {
                    if ((dr1["IssueStore"].ToString() == dr2["IssueStore"].ToString()) &&
                        (dr1["GiftCertID"].ToString() == dr2["GiftCertID"].ToString()))
                    {
                        if (dr2["Tran"].ToString() == "I") addamt = addamt + Convert.ToDouble(dr2["Amount"].ToString());
                        if (dr2["Tran"].ToString() == "T") tenderamt = tenderamt + Convert.ToDouble(dr2["Amount"].ToString());
                    }
                }
                dtbl.Rows.Add(new object[] { dr1["IssueStore"].ToString(), dr1["GiftCertID"].ToString(),
                                             (addamt - tenderamt).ToString("#0.00")+"  "});
            }
            return dtbl;
        }

        
    }
}
