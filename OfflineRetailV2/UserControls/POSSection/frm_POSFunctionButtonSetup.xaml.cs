using System;
using System.Collections.Generic;
using System.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSFunctionButtonSetup.xaml
    /// </summary>
    public partial class frm_POSFunctionButtonSetup : Window
    {
        public frm_POSFunctionButtonSetup()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private bool boolControlChanged;
        private bool blFunctionBtnAccess;
        private bool blFunctionOrderChangeAccess;
        private DataTable dtblPOSFunc = null;
        private bool blSetPOSFunction;

        public bool ControlChanged
        {
            get { return boolControlChanged; }
            set { boolControlChanged = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        public bool FunctionOrderChangeAccess
        {
            get { return blFunctionOrderChangeAccess; }
            set { blFunctionOrderChangeAccess = value; }
        }

        public bool SetPOSFunction
        {
            get { return blSetPOSFunction; }
            set { blSetPOSFunction = value; }
        }

        public DataTable POSFunc
        {
            get { return dtblPOSFunc; }
            set { dtblPOSFunc = value; }
        }

        private void RearrangePOSFunctionButton(string OrderType, int DisplayOrder)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("FunctionName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtbl.Columns.Add("IsVisible", System.Type.GetType("System.String"));
            dtbl.Columns.Add("FunctionDescription", System.Type.GetType("System.String"));
            if (OrderType == "P")   // move previous
            {
                foreach (DataRow dr1 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr1["DisplayOrder"].ToString()) == DisplayOrder - 1)
                        break;
                    else
                    {
                        dtbl.Rows.Add(new object[] { dr1["ID"].ToString(),
                                                   dr1["FunctionName"].ToString(),
                                                   dr1["DisplayOrder"].ToString(),
                                                   dr1["IsVisible"].ToString(),
                                                   dr1["FunctionDescription"].ToString()});
                    }
                }

                foreach (DataRow dr2 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr2["DisplayOrder"].ToString()) != DisplayOrder) continue;
                    dtbl.Rows.Add(new object[] { dr2["ID"].ToString(),
                                                   dr2["FunctionName"].ToString(),
                                                   DisplayOrder - 1,
                                                   dr2["IsVisible"].ToString(),dr2["FunctionDescription"].ToString()});
                }

                foreach (DataRow dr3 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr3["DisplayOrder"].ToString()) != DisplayOrder - 1) continue;
                    dtbl.Rows.Add(new object[] { dr3["ID"].ToString(),
                                                   dr3["FunctionName"].ToString(),
                                                   DisplayOrder,
                                                   dr3["IsVisible"].ToString(),dr3["FunctionDescription"].ToString()});
                }

                foreach (DataRow dr4 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr4["DisplayOrder"].ToString()) <= DisplayOrder) continue;
                    dtbl.Rows.Add(new object[] {    dr4["ID"].ToString(),
                                                   dr4["FunctionName"].ToString(),
                                                   dr4["DisplayOrder"].ToString(),
                                                   dr4["IsVisible"].ToString(),dr4["FunctionDescription"].ToString()});

                }
            }


            if (OrderType == "N")   // move next
            {
                foreach (DataRow dr1 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr1["DisplayOrder"].ToString()) == DisplayOrder)
                        break;
                    else
                    {
                        dtbl.Rows.Add(new object[] { dr1["ID"].ToString(),
                                                   dr1["FunctionName"].ToString(),
                                                   dr1["DisplayOrder"].ToString(),
                                                   dr1["IsVisible"].ToString(),dr1["FunctionDescription"].ToString()});
                    }
                }

                foreach (DataRow dr2 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr2["DisplayOrder"].ToString()) != DisplayOrder + 1) continue;
                    dtbl.Rows.Add(new object[] { dr2["ID"].ToString(),
                                                   dr2["FunctionName"].ToString(),
                                                   DisplayOrder,
                                                   dr2["IsVisible"].ToString(),dr2["FunctionDescription"].ToString()});
                }

                foreach (DataRow dr3 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr3["DisplayOrder"].ToString()) != DisplayOrder) continue;
                    dtbl.Rows.Add(new object[] { dr3["ID"].ToString(),
                                                   dr3["FunctionName"].ToString(),
                                                   DisplayOrder + 1,
                                                   dr3["IsVisible"].ToString(),dr3["FunctionDescription"].ToString()});
                }

                foreach (DataRow dr4 in dtblPOSFunc.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr4["DisplayOrder"].ToString()) <= DisplayOrder + 1) continue;
                    dtbl.Rows.Add(new object[] {    dr4["ID"].ToString(),
                                                   dr4["FunctionName"].ToString(),
                                                   dr4["DisplayOrder"].ToString(),
                                                   dr4["IsVisible"].ToString(),dr4["FunctionDescription"].ToString()});

                }
            }

            dtblPOSFunc.Rows.Clear();
            dtblPOSFunc = dtbl;
            grdFuncBtn.ItemsSource = dtblPOSFunc;
            dtbl.Dispose();
        }

        // get all pos functions

        private void PopulatePOSFunctions()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchPOSFunctionData();
            dtblPOSFunc = dtbl;
            SetPOSFunctionResource(dtblPOSFunc);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblPOSFunc.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdFuncBtn.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();
        }

        // Set POS Function Button Resource
        private void SetPOSFunctionResource(DataTable d)
        {
            foreach (DataRow dr in d.Rows)
            {
                if (dr["FunctionName"].ToString() == "Help") dr["FunctionName"] = Properties.Resources.Help;
                else if (dr["FunctionName"].ToString() == "Down") dr["FunctionName"] = Properties.Resources.DownF;
                else if (dr["FunctionName"].ToString() == "Up") dr["FunctionName"] = Properties.Resources.UpF;
                else if (dr["FunctionName"].ToString() == "Paid Out") dr["FunctionName"] = Properties.Resources.Paid_Out;
                else if (dr["FunctionName"].ToString() == "No Sale") dr["FunctionName"] = Properties.Resources.No_Sale;
                else if (dr["FunctionName"].ToString() == "Cancel") dr["FunctionName"] = Properties.Resources.Cancel;
                else if (dr["FunctionName"].ToString() == "Layaway") dr["FunctionName"] = Properties.Resources.Layaway;
                else if (dr["FunctionName"].ToString() == "Acct Pay") dr["FunctionName"] = Properties.Resources.Acct_Pay;
                else if (dr["FunctionName"].ToString() == "Gift Cert") dr["FunctionName"] = Properties.Resources.Gift_Cert;
                else if (dr["FunctionName"].ToString() == "Resume/Suspend") dr["FunctionName"] = Properties.Resources.Resume_Suspend;
                else if (dr["FunctionName"].ToString() == "Return Reprint") dr["FunctionName"] = Properties.Resources.Return_Reprint;
                else if (dr["FunctionName"].ToString() == "Refresh Stock") dr["FunctionName"] = Properties.Resources.Refresh_Stock;
                else if (dr["FunctionName"].ToString() == "Select Apps") dr["FunctionName"] = Properties.Resources.Select_Apps;
                else if (dr["FunctionName"].ToString() == "Cust. Picture") dr["FunctionName"] = Properties.Resources.Cust__Picture;
                else if (dr["FunctionName"].ToString() == "Cust. Notes") dr["FunctionName"] = Properties.Resources.Cust__Notes;
                else if (dr["FunctionName"].ToString() == "Emp. Picture") dr["FunctionName"] = Properties.Resources.Emp__Picture;
                else if (dr["FunctionName"].ToString() == "Product Picture") dr["FunctionName"] = Properties.Resources.Product_Picture;
                else if (dr["FunctionName"].ToString() == "Product Notes") dr["FunctionName"] = Properties.Resources.Product_Notes;
                else if (dr["FunctionName"].ToString() == "View Product Price") dr["FunctionName"] = Properties.Resources.View_Product_Price;
                else if (dr["FunctionName"].ToString() == "Change Product Price") dr["FunctionName"] = Properties.Resources.Change_Product_Price;
                else if (dr["FunctionName"].ToString() == "Use Price Level") dr["FunctionName"] = Properties.Resources.Use_Price_Level;
                else if (dr["FunctionName"].ToString() == "Fast Cash") dr["FunctionName"] = Properties.Resources.Fast_Cash;
                else if (dr["FunctionName"].ToString() == "Gift Cert Balance") dr["FunctionName"] = Properties.Resources.Gift_Cert_Balance;
                else if (dr["FunctionName"].ToString() == "Invoice Item Notes") dr["FunctionName"] = Properties.Resources.Invoice_Item_Notes;
                else if (dr["FunctionName"].ToString() == "Work Order") dr["FunctionName"] = Properties.Resources.Work_Order;
                else if (dr["FunctionName"].ToString() == "Print Cust. Label") dr["FunctionName"] = Properties.Resources.Print_Cust__Label;
                else if (dr["FunctionName"].ToString() == "Print Gift Receipt") dr["FunctionName"] = Properties.Resources.Print_Gift_Receipt;
                else if (dr["FunctionName"].ToString() == "Discount Ticket") dr["FunctionName"] = Properties.Resources.Discount_Ticket;
                else if (dr["FunctionName"].ToString() == "Book Appt.") dr["FunctionName"] = Properties.Resources.Book_Appt_;
                else if (dr["FunctionName"].ToString() == "Recall Appt.") dr["FunctionName"] = Properties.Resources.Recall_Appt_;
                else if (dr["FunctionName"].ToString() == "Recall Rent") dr["FunctionName"] = Properties.Resources.Recall_Rent;
                else if (dr["FunctionName"].ToString() == "Recall Repair") dr["FunctionName"] = Properties.Resources.Recall_Repair;
                else if (dr["FunctionName"].ToString() == "Revert CARD Tran.") dr["FunctionName"] = Properties.Resources.Revert_CARD_Tran_;
                else if (dr["FunctionName"].ToString() == "Mercury Gift Card")
                {
                    if (Settings.POSCardPayment == "Y")
                    {
                        if (Settings.PaymentGateway == 3) dr["FunctionName"] = Properties.Resources.Mercury_Gift_Card;
                        if (Settings.PaymentGateway == 3) dr["FunctionName"] = Properties.Resources.Precidia_Gift_Card;
                        if (Settings.PaymentGateway == 5) dr["FunctionName"] = Properties.Resources.Datacap_Gift_Card;
                        if (Settings.PaymentGateway == 7) dr["FunctionName"] = Properties.Resources.POSLink_Gift_Card;
                    }
                    else
                    {
                        dr["FunctionName"] = Properties.Resources.Mercury_Gift_Card;
                    }
                }
                else if (dr["FunctionName"].ToString() == "Fast CC") dr["FunctionName"] = Properties.Resources.Fast_CC;
                else if (dr["FunctionName"].ToString() == "Fees & Charges Item") dr["FunctionName"] = Properties.Resources.Fees____Charges_Item;
                else if (dr["FunctionName"].ToString() == "EBT/ Mercury Gift Card Balance")
                {

                    if (Settings.POSCardPayment == "Y")
                    {
                        if (Settings.PaymentGateway == 3) dr["FunctionName"] = Properties.Resources.EBT__Mercury_Gift_Card_Balance;
                        if (Settings.PaymentGateway == 3) dr["FunctionName"] = Properties.Resources.EBT__Precidia_Gift_Card_Balance;
                        if (Settings.PaymentGateway == 5) dr["FunctionName"] = Properties.Resources.EBT__Datacap_Gift_Card_Balance;
                        if (Settings.PaymentGateway == 7) dr["FunctionName"] = Properties.Resources.EBT__POSLink_Gift_Card_Balance;
                    }
                    else
                    {
                        dr["FunctionName"] = Properties.Resources.EBT__Mercury_Gift_Card_Balance;
                    }
                }
                else if (dr["FunctionName"].ToString() == "Bottle Refund") dr["FunctionName"] = Properties.Resources.Bottle_Refund;
                else if (dr["FunctionName"].ToString() == "Discount Item") dr["FunctionName"] = Properties.Resources.Discount_Item;
                else if (dr["FunctionName"].ToString() == "Qty (+)") dr["FunctionName"] = Properties.Resources.Qty+"(+)";
                else if (dr["FunctionName"].ToString() == "Qty (-)") dr["FunctionName"] = Properties.Resources.Qty+"(-)";
                else if (dr["FunctionName"].ToString() == "Tare") dr["FunctionName"] = Properties.Resources.Tare;
                else if (dr["FunctionName"].ToString() == "Fees & Charges Ticket") dr["FunctionName"] = Properties.Resources.Fees____Charges_Ticket;
                //else if (dr["FunctionName"].ToString() == "Check In/Out") dr["FunctionName"] = Properties.Resources."Check In/Out", "DataObject_POS_FunctionBtn_44");
                else if (dr["FunctionName"].ToString() == "Points to Store Credit") dr["FunctionName"] = Properties.Resources.Points_to_Store_Credit;
                else if (dr["FunctionName"].ToString() == "Lotto Payout") dr["FunctionName"] = Properties.Resources.Lotto_Payout;
                else if (dr["FunctionName"].ToString() == "Paid In") dr["FunctionName"] = Properties.Resources.Paid_In;
                else if (dr["FunctionName"].ToString() == "Safe Drop") dr["FunctionName"] = Properties.Resources.Safe_Drop;
                else dr["FunctionName"] = "Gift Aid";
            }
        }





        public void EnableDisableButton()
        {
            //if (blFunctionOrderChangeAccess)
            //{
                if ((grdFuncBtn.ItemsSource as DataTable).Rows.Count == 0)
                {
                    btnUP.IsEnabled = false;
                    btnDown.IsEnabled = false;
                }
                else if (gridView1.FocusedRowHandle == 0)
                {
                    btnUP.IsEnabled = false;
                    btnDown.IsEnabled = true;
                }
                else if (gridView1.FocusedRowHandle == ((grdFuncBtn.ItemsSource as DataTable).Rows.Count - 1))
                {
                    btnUP.IsEnabled = true;
                    btnDown.IsEnabled = false;
                }
                else
                {
                    btnUP.IsEnabled = true;
                    btnDown.IsEnabled = true;
                }
            //}
        }

        private async void SetVisibleCaption()
        {
            int intRowID = gridView1.FocusedRowHandle;
            if (intRowID == -1) return;

            string getval = await GeneralFunctions.GetCellValue1(intRowID, grdFuncBtn, colVisible);
            if (getval == "Yes")
                btnVisible.Content = Properties.Resources.Set_Invisible + " " + Properties.Resources.in_POS_Screen;
            else
                btnVisible.Content = Properties.Resources.Set_Visible + " " + Properties.Resources.in_POS_Screen;
        }

        private void frmPOSFunctionButtonSetup_Load(object sender, EventArgs e)
        {
            Title.Text = Properties.Resources.Rearrange_POS_Function_Buttons;

            btnTurnOffAll.Content = Properties.Resources.Set_Invisible + " " + Properties.Resources.All_Apps
                + " " + Properties.Resources.in_POS_Screen;

            dtblPOSFunc = new DataTable();
            dtblPOSFunc.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionName", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("IsVisible", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionDescription", System.Type.GetType("System.String"));

            PopulatePOSFunctions();

            //if (!blFunctionOrderChangeAccess)
            //{
                //bar1.Visibility = Visibility.Collapsed;
                //btnTurnOffAll.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
                bar1.Visibility = Visibility.Visible;
                btnTurnOffAll.Visibility = Visibility.Visible;
            //}

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            PosDataObject.Setup objsetup = new PosDataObject.Setup();
            objsetup.Connection = SystemVariables.Conn;
            objsetup.LoginUserID = SystemVariables.CurrentUserID;
            objsetup.SplitDataTable = grdFuncBtn.ItemsSource as DataTable;
            string err = objsetup.PostPOSFunctionUpdate();
            blSetPOSFunction = true;
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnUP1_Click(object sender, RoutedEventArgs e)
        {
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;

            blSetPOSFunction = true;
        }

        private void btnDown1_Click(object sender, RoutedEventArgs e)
        {
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            blSetPOSFunction = true;
        }

        private async void btnUP_Click(object sender, RoutedEventArgs e)
        {
            //if (blFunctionOrderChangeAccess)
            //{
                int intRowID = gridView1.FocusedRowHandle;
                boolControlChanged = true;
                blSetPOSFunction = true;
                RearrangePOSFunctionButton("P", GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdFuncBtn, colDisplay)));
                gridView1.FocusedRowHandle = intRowID - 1;
                EnableDisableButton();
            //}
        }

        private async void btnDown_Click(object sender, RoutedEventArgs e)
        {
           // if (blFunctionOrderChangeAccess)
           // {
                int intRowID = gridView1.FocusedRowHandle;
                boolControlChanged = true;
                blSetPOSFunction = true;
                RearrangePOSFunctionButton("N", GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdFuncBtn, colDisplay)));
                gridView1.FocusedRowHandle = intRowID + 1;
                EnableDisableButton();
           // }
        }

        private void barStaticItem1_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnVisible_Click(object sender, RoutedEventArgs e)
        {
           // if (blFunctionOrderChangeAccess)
           // {
                int intRowID = gridView1.FocusedRowHandle;
                if (intRowID == -1) return;
                boolControlChanged = true;
                blSetPOSFunction = true;
                string getval = await GeneralFunctions.GetCellValue1(intRowID, grdFuncBtn, colVisible);
                if (getval == "Yes")
                    grdFuncBtn.SetCellValue(intRowID, colVisible, "No");
                else
                    grdFuncBtn.SetCellValue(intRowID, colVisible, "Yes");
                SetVisibleCaption();
            //}
        }

        private void btnTurnOffAll_Click(object sender, RoutedEventArgs e)
        {
            blSetPOSFunction = true;
            boolControlChanged = true;
            gridView1.FocusedRowHandle = -1;

            DataTable dtbl = new DataTable();
            dtbl = grdFuncBtn.ItemsSource as DataTable;

            foreach (DataRow dr in dtbl.Rows)
            {
                dr["IsVisible"] = "No";
            }

            grdFuncBtn.ItemsSource = dtbl;
            gridView1.FocusedRowHandle = 0;
            dtbl.Dispose();
            SetVisibleCaption();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            //if (blFunctionOrderChangeAccess)
            //{
                EnableDisableButton();
                SetVisibleCaption();
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Rearrange POS Function Buttons";

            btnTurnOffAll.Content = Properties.Resources.Set_Invisible + " " + Properties.Resources.All_Apps
               + " " + Properties.Resources.in_POS_Screen;

            dtblPOSFunc = new DataTable();
            dtblPOSFunc.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionName", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("DisplayOrder", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("IsVisible", System.Type.GetType("System.String"));
            dtblPOSFunc.Columns.Add("FunctionDescription", System.Type.GetType("System.String"));

            PopulatePOSFunctions();

           // if (!blFunctionOrderChangeAccess)
           // {
            //    bar1.Visibility = Visibility.Collapsed;
            //    btnTurnOffAll.Visibility = Visibility.Collapsed;
           // }
           // else
           // {
                bar1.Visibility = Visibility.Visible;
                btnTurnOffAll.Visibility = Visibility.Visible;
          // }
        }
    }
}
