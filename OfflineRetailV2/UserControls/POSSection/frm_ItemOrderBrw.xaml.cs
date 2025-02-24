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
using System.Data.SqlClient;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_ItemOrderBrw.xaml
    /// </summary>
    public partial class frm_ItemOrderBrw : Window
    {
        public frm_ItemOrderBrw()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            Close();
        }

        int TotalCount;
        int VisibleCount;

        private int intGroupID;
        //private frm_ItemOrderBrwUC uC;
        public int GroupID
        {
            get { return intGroupID; }
            set { intGroupID = value; }
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            
            for (intColCtr = 0; intColCtr < (frm_ItemOrderBrwUC.grdCategory.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) frm_ItemOrderBrwUC.gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> ReturnRowID()
        {

            int intRowID = -1;
            int intRecID = -1;
            if ((frm_ItemOrderBrwUC.grdCategory.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (frm_ItemOrderBrwUC.gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = frm_ItemOrderBrwUC.gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colID)));
            return intRecID;
        }

        private void FetchData()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.Product objp = new PosDataObject.Product();
            objp.Connection = SystemVariables.Conn;
            dtbl = objp.FetchPOSDisplayOrderData(intGroupID);
            frm_ItemOrderBrwUC.grdCategory.ItemsSource = dtbl;

        }

        private int UpdateDisplayOrder(int pID, int pOrder, string pType)
        {
            PosDataObject.Product objCategory = new PosDataObject.Product();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.UpdatePosDisplayOrder(pID, intGroupID, pOrder, pType);
        }

        private string UpdateItemPOSVisible(int pID, string pVis)
        {
            PosDataObject.Product objCategory = new PosDataObject.Product();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.UpdateItemPOSVisible(pID, pVis);
        }

        public void EnableDisableButton()
        {
            if ((frm_ItemOrderBrwUC.grdCategory.ItemsSource as DataTable).Rows.Count <= 0)
            {
                frm_ItemOrderBrwUC.btnUP.IsEnabled = false;
                frm_ItemOrderBrwUC.btnDown.IsEnabled = false;
            }
            else if (frm_ItemOrderBrwUC.gridView1.FocusedRowHandle == 0)
            {
                frm_ItemOrderBrwUC.btnUP.IsEnabled = false;
                frm_ItemOrderBrwUC.btnDown.IsEnabled = true;
            }
            else if (frm_ItemOrderBrwUC.gridView1.FocusedRowHandle == (frm_ItemOrderBrwUC.grdCategory.ItemsSource as DataTable).Rows.Count - 1)
            {
                frm_ItemOrderBrwUC.btnUP.IsEnabled = true;
                frm_ItemOrderBrwUC.btnDown.IsEnabled = false;
            }
            else
            {
                frm_ItemOrderBrwUC.btnUP.IsEnabled = true;
                frm_ItemOrderBrwUC.btnDown.IsEnabled = true;
            }
        }

        private async Task SetVisibleCaption()
        {
            int intRowID = frm_ItemOrderBrwUC.gridView1.FocusedRowHandle;
            if (intRowID == -1) return;

            string getval = await GeneralFunctions.GetCellValue1(intRowID, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colVisible);
            if (getval == "Yes")
                frm_ItemOrderBrwUC.btnVisible.Content = "SET INVISIBLE IN POS SCREEN";
            else
                frm_ItemOrderBrwUC.btnVisible.Content = "SET VISIBLE IN POS SCREEN";
        }

        async void SetVisibleClick(object sender, RoutedEventArgs e)
        {
            int intRowID = frm_ItemOrderBrwUC.gridView1.FocusedRowHandle;
            if (intRowID == -1) return;

            string getval = await GeneralFunctions.GetCellValue1(intRowID, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colVisible);
            int intFocused = await ReturnRowID();
            frm_ItemOrderBrwUC.grdCategory.SetCellValue(frm_ItemOrderBrwUC.gridView1.FocusedRowHandle, frm_ItemOrderBrwUC.colVisible, getval);
            UpdateItemPOSVisible(intFocused, getval == "Yes" ? "N" : "Y");
            FetchData();
            await SetCurrentRow(intFocused);
            EnableDisableButton();
            await SetVisibleCaption();
        }

        async void BtnUPClick(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateDisplayOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(frm_ItemOrderBrwUC.gridView1.FocusedRowHandle, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colDisplay)), "P");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();
        }

        async void BtnDownClick(object sender, RoutedEventArgs e)
        {
            int intRowID = await ReturnRowID();
            UpdateDisplayOrder(intRowID, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(frm_ItemOrderBrwUC.gridView1.FocusedRowHandle, frm_ItemOrderBrwUC.grdCategory, frm_ItemOrderBrwUC.colDisplay)), "N");
            FetchData();
            await SetCurrentRow(intRowID);
            EnableDisableButton();

        }

        private async void GridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            EnableDisableButton();
            await SetVisibleCaption();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frm_ItemOrderBrwUC.btnVisible.Click += new RoutedEventHandler(SetVisibleClick);
            frm_ItemOrderBrwUC.btnUP.Click += new RoutedEventHandler(BtnUPClick);
            frm_ItemOrderBrwUC.btnDown.Click += new RoutedEventHandler(BtnDownClick);
            frm_ItemOrderBrwUC.gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            FetchData();
            EnableDisableButton();
        }

        
    }
}
