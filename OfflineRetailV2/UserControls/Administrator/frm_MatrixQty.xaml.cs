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
using OfflineRetailV2.Data;
using System.Data;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_MatrixQty.xaml
    /// </summary>
    public partial class frm_MatrixQty : Window
    {
        private string strConsolidated;
        private string strSKU;
        private string strStore;

        public string Consolidated
        {
            get { return strConsolidated; }
            set { strConsolidated = value; }
        }

        public string SKU
        {
            get { return strSKU; }
            set { strSKU = value; }
        }

        public string Store
        {
            get { return strStore; }
            set { strStore = value; }
        }



        public frm_MatrixQty()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void LoadQty()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Central objps = new PosDataObject.Central();
            objps.Connection = SystemVariables.Conn;
            dtbl = objps.ShowMatrixQty(strStore, strSKU, strConsolidated);
            int i = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                colOptionValue1.Header = dr["F1"].ToString() == "" ? " " : dr["F1"].ToString();
                colOptionValue2.Header = dr["F2"].ToString() == "" ? " " : dr["F2"].ToString();
                colOptionValue3.Header = dr["F3"].ToString() == "" ? " " : dr["F3"].ToString();
                break;
            }
            grdQty.ItemsSource = dtbl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadQty();
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
