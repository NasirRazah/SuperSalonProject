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
    /// Interaction logic for frm_POSCouponDlg.xaml
    /// </summary>
    public partial class frm_POSCouponDlg : Window
    {
        double posActionListBoxVerticalUpOffset = 0;
        double posActionListBoxVerticalDownOffset = 0;
        private DataTable dtblPOSCoupon = null;
        private int inStep = 0;
        private static int ITEMSEPARATOR = 5;
        private string strBrowseType;
        private string strDiscData;
        private string strDiscountOn;

        private int intCustID;

        public POSControl POSFormN { get; set; }

        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }

        public string DiscData
        {
            get { return strDiscData; }
            set { strDiscData = value; }
        }

        public string BrowseType
        {
            get { return strBrowseType; }
            set { strBrowseType = value; }
        }

        public string DiscountOn
        {
            get { return strDiscountOn; }
            set { strDiscountOn = value; }
        }
        public frm_POSCouponDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (POSActionScrollViewer is null) return;

            posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset -= 250;
            if (posActionListBoxVerticalDownOffset < 0)
            {
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = 0;
            }
            POSActionScrollViewer.ScrollToVerticalOffset(posActionListBoxVerticalDownOffset);
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (POSActionScrollViewer is null) return;

            if (posActionListBoxVerticalUpOffset < POSActionScrollViewer.ExtentHeight)
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset += 250;

            if (posActionListBoxVerticalDownOffset > POSActionScrollViewer.ExtentHeight)
            {
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = POSActionScrollViewer.ExtentHeight;
            }

            POSActionScrollViewer.ScrollToVerticalOffset(posActionListBoxVerticalUpOffset);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (strBrowseType == "Coupons")
            {
                if (strDiscountOn == "Item") Title.Text = Properties.Resources.Discount_on_Item;
                if (strDiscountOn == "Ticket") Title.Text = Properties.Resources.Discount_on_Ticket;
            }
            if (strBrowseType == "Service Charges") Title.Text = Properties.Resources.Service_Charges;
            if (strBrowseType == "Fees")
            {
                if (strDiscountOn == "Item") Title.Text = Properties.Resources.Fees___Charges_on_Item;
                if (strDiscountOn == "Ticket") Title.Text = Properties.Resources.Fees___Charges_on_Ticket;
            }
            dtblPOSCoupon = new DataTable();
            dtblPOSCoupon.Columns.Add("SL", System.Type.GetType("System.Int32"));
            dtblPOSCoupon.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("Name", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("Type", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("Amount", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("CheckDiscount", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("CheckTax", System.Type.GetType("System.String"));
            dtblPOSCoupon.Columns.Add("CheckQty", System.Type.GetType("System.String"));


            LoadItems();
        }

        private void LoadItems()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtblItemButtons = null;

            if (strBrowseType == "Coupons")
            {
                if (strDiscData != "") strDiscData = strDiscData.Substring(0, strDiscData.Length - 1);
                dtblItemButtons = objPOS.FetchDiscounts(strDiscData);
            }

            if (strBrowseType == "Fees")
            {
                if (strDiscData != "") strDiscData = strDiscData.Substring(0, strDiscData.Length - 1);
                dtblItemButtons = objPOS.FetchFees(strDiscData);
            }

            int intSL = 0;
            foreach (DataRow dr in dtblItemButtons.Rows)
            {
                intSL++;
                dtblPOSCoupon.Rows.Add(new object[]{
                            intSL,
                            dr["ID"].ToString(),
                            dr["Name"].ToString(),
                            dr["Type"].ToString(),
                            dr["Amount"].ToString(),
                            strBrowseType == "Coupons" ? "X" : dr["CheckDiscount"].ToString(),
                            strBrowseType == "Coupons" ? "X" : dr["CheckTax"].ToString(),
                            strBrowseType == "Coupons" ? "X" : dr["CheckQty"].ToString()
                    });
            }

            pnlBody.Children.Clear();

            foreach (DataRow dr in dtblPOSCoupon.Rows)
            {
                Button btn = new Button();

                POSControls.POSCoupon PosItem = new POSControls.POSCoupon();
                PosItem.Focusable = false;
                PosItem.ItemID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                PosItem.CurrencySymbol = SystemVariables.CurrencySymbol;
                PosItem.ItemName = dr["Name"].ToString();
                PosItem.ItemAmount = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(dr["Amount"].ToString()));
                PosItem.ItemType = dr["Type"].ToString();
                if (strBrowseType == "Fees")
                {
                    PosItem.CheckDiscount = dr["CheckDiscount"].ToString();
                    PosItem.CheckQty = dr["CheckQty"].ToString();
                    PosItem.CheckTax = dr["CheckQty"].ToString();
                }

                if (strBrowseType == "Coupons")
                {
                    if (strDiscountOn == "Item")
                    {
                        btn.Click += new RoutedEventHandler(POSFormN.posCouponItemClick);
                    }
                    if (strDiscountOn == "Ticket")
                    {
                        btn.Click += new RoutedEventHandler(POSFormN.posCouponItem1Click);
                    }
                }

                if (strBrowseType == "Fees")
                {
                    if (strDiscountOn == "Item")
                    {
                        btn.Click += new RoutedEventHandler(POSFormN.posCouponItem2Click);
                    }
                    if (strDiscountOn == "Ticket")
                    {
                        btn.Click += new RoutedEventHandler(POSFormN.posCouponItem3Click);
                    }
                }
                

                Grid grd = new Grid();
                grd.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                TextBlock txt = new TextBlock()
                {
                    Text = PosItem.DisplayText
                };
                Grid.SetRow(txt, 0);

                grd.Children.Add(txt);
                btn.Content = grd;
                btn.Tag = PosItem;
                pnlBody.Children.Add(btn);
            }

                
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
