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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frmPOSMoreFunctionsControl.xaml
    /// </summary>
    public partial class frmPOSMoreFunctionsControl : UserControl
    {
        private static int ITEMSEPARATOR = 5;

        private string strResumeSuspend;
        private int introwno = 0;
        private int intIgnoreCount = 13;

        private int frmValue1;
        private int frmValue2;
        private int frmValue3;
        private int frmValue4;
        private int frmValue5;

        public int FValue1
        {
            get { return frmValue1; }
            set { frmValue1 = value; }
        }

        public int FValue2
        {
            get { return frmValue2; }
            set { frmValue2 = value; }
        }

        public int FValue3
        {
            get { return frmValue3; }
            set { frmValue3 = value; }
        }

        public int FValue4
        {
            get { return frmValue4; }
            set { frmValue4 = value; }
        }

        public int FValue5
        {
            get { return frmValue5; }
            set { frmValue5 = value; }
        }

        public int IgnoreCount
        {
            get { return intIgnoreCount; }
            set { intIgnoreCount = value; }
        }

        public string ResumeSuspend
        {
            get { return strResumeSuspend; }
            set { strResumeSuspend = value; }
        }
        private POSControl frmPosDlg1;
        public POSControl POSFormN
        {
            get { return frmPosDlg1; }
            set { frmPosDlg1 = value; }
        }

        public frmPOSMoreFunctionsControl()
        {
            InitializeComponent();
        }
        public CommandBase btnHelpClickCommnand { get; set; }
        public CommandBase btnPaidOutClickCommnand { get; set; }
        public CommandBase btnNoSaleClickCommnand { get; set; }
        public CommandBase btnCancelClickCommnand { get; set; }
        public CommandBase btnLayawayClickCommnand { get; set; }
        public CommandBase btnAcctPayClickCommnand { get; set; }
        public CommandBase btnGiftCertClickCommnand { get; set; }
        public CommandBase btnResumeSuspendClickCommnand { get; set; }
        public CommandBase btnReprintClickCommnand { get; set; }
        public CommandBase btnup1ClickCommnand { get; set; }
        public CommandBase btndown1ClickCommnand { get; set; }
        public CommandBase btnStockRefreshClickCommnand { get; set; }
        public CommandBase btnCustomizeClickCommnand { get; set; }
        public CommandBase btnCustPhotoClickCommnand { get; set; }
        public CommandBase btnCustNotesClickCommnand { get; set; }
        public CommandBase btnEmpPhotoClickCommnand { get; set; }
        public CommandBase btnProductPhotoClickCommnand { get; set; }
        public CommandBase btnProductNoteClickCommnand { get; set; }
        public CommandBase btnViewPriceClickCommnand { get; set; }
        public CommandBase btnChangePriceClickCommnand { get; set; }
        public CommandBase btnUsePriceClickCommnand { get; set; }
        public CommandBase btnFastCashClickCommnand { get; set; }
        public CommandBase btnGiftCertBalanceClickCommnand { get; set; }
        public CommandBase btnINVNotesClickCommnand { get; set; }
        public CommandBase btnWorkOrderClickCommnand { get; set; }
        public CommandBase btnPrintLabelClickCommnand { get; set; }
        public CommandBase btnPrintGiftReceiptClickCommnand { get; set; }
        public CommandBase btnCouponClickCommnand { get; set; }
        public CommandBase btnApptBookClickCommnand { get; set; }
        public CommandBase btnApptRecallClickCommnand { get; set; }
        public CommandBase btnRentRecallClickCommnand { get; set; }
        public CommandBase btnRepairRecallClickCommnand { get; set; }
        public CommandBase btnCardTranClickCommnand { get; set; }
        public CommandBase btnMcryGiftCardClickCommnand { get; set; }
        public CommandBase btnFastCCClickCommnand { get; set; }
        public CommandBase btnfeesClickCommnand { get; set; }
        public CommandBase btnFeesCouponClickCommnand { get; set; }
        public CommandBase btnebtbalClickCommnand { get; set; }
        public CommandBase btnBottleRefundClickCommnand { get; set; }
        public CommandBase btnItemDiscountClickCommnand { get; set; }
        public CommandBase btnAddQtyClickCommnand { get; set; }
        public CommandBase btnMinusQtyClickCommnand { get; set; }
        public CommandBase btnTareClickCommnand { get; set; }
        public CommandBase btnCheckInOutClickCommnand { get; set; }
        public CommandBase btnPointToStoreCreditClickCommnand { get; set; }
        public CommandBase btnLottoPayoutClickCommnand { get; set; }
        public void LoadFunctionButtons()
        {
            bool blf = false;
            if (Settings.POSCardPayment == "Y")
            {
                if (Settings.PaymentGateway == 1) blf = false;
                else blf = true;
            }
            else
            {
                blf = false;
            }
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtblItemButtons = objPOS.FetchVisiblePOSFunction(blf, SystemVariables.CurrentUserID);
            int intTotal = 0;
            int intCount = 0;
            int intInc = 0;
            int intRowCount = 0;
            int intButtonWidth = 64;
            int c3 = 0;
            int r3 = 1;
            foreach (DataRow dr in dtblItemButtons.Rows)
            {
                intTotal++;
                if (intTotal <= intIgnoreCount) continue;

                intCount++;
                intInc++;
                Button PosItem = new Button();


                PosItem.Width = intButtonWidth;


                if (dr["FunctionName"].ToString() == "Help")
                {
                    PosItem.Content = Properties.Resources.Help;
                    PosItem.ToolTip = Properties.Resources.Help;
                    PosItem.Tag = "Help On posscreen";
                    PosItem.Click += btnHelpClick;
                }
                if (dr["FunctionName"].ToString() == "Paid Out")
                {
                    PosItem.Content =Properties.Resources.Paid_Out;
                    PosItem.ToolTip = Properties.Resources.Paid_Out ;
                    PosItem.Click += btnPaidOutClick;
                }
                if (dr["FunctionName"].ToString() == "No Sale")
                {
                    PosItem.Content = Properties.Resources.No_Sale;
                    PosItem.ToolTip = Properties.Resources.No_Sale;
                    PosItem.Click += btnNoSaleClick;
                }
                if (dr["FunctionName"].ToString() == "Cancel")
                {
                    PosItem.Content = Properties.Resources.Cancel;
                    PosItem.ToolTip = Properties.Resources.Cancel; 
                    PosItem.Click += btnCancelClick;
                }
                if (dr["FunctionName"].ToString() == "Layaway")
                {
                    PosItem.Content = Properties.Resources.Layaway;
                    PosItem.ToolTip = Properties.Resources.Layaway; 
                    PosItem.Click += btnLayawayClick;
                }
                if (dr["FunctionName"].ToString() == "Acct Pay")
                {
                    PosItem.Content = Properties.Resources.Acct_Pay;
                    PosItem.ToolTip = Properties.Resources.Acct_Pay; 
                    PosItem.Click += btnAcctPayClick;
                }
                if (dr["FunctionName"].ToString() == "Gift Cert")
                {
                    PosItem.Content = Properties.Resources.Gift_Cert;
                    PosItem.ToolTip = Properties.Resources.Gift_Cert;
                    PosItem.Click += btnGiftCertClick;
                }
                if (dr["FunctionName"].ToString() == "Resume/Suspend")
                {
                    if (strResumeSuspend == "Resume")
                    {
                        PosItem.Content = Properties.Resources.Resume;
                        PosItem.ToolTip = Properties.Resources.Resume;
                    }
                    if (strResumeSuspend == "Suspend")
                    {
                        PosItem.Content = Properties.Resources.Suspend;
                        PosItem.ToolTip = Properties.Resources.Suspend; 
                    }
                    PosItem.Click += btnResumeSuspendClick;
                }
                if (dr["FunctionName"].ToString() == "Return Reprint")
                {
                    PosItem.Content = Properties.Resources.Return_Reprint;
                    PosItem.ToolTip = Properties.Resources.Return_Reprint; 
                    PosItem.Click += btnReprintClick;
                }
                if (dr["FunctionName"].ToString() == "Refresh Stock")
                {
                    PosItem.Content = Properties.Resources.Refresh_Stock;
                    PosItem.ToolTip = Properties.Resources.Refresh_Stock; 
                    PosItem.Click += btnStockRefreshClick;
                }
                if (dr["FunctionName"].ToString() == "Select Apps")
                {
                    PosItem.Content = Properties.Resources.Select_Apps;
                    PosItem.ToolTip = Properties.Resources.Customize_Apps; 
                    PosItem.Click += btnCustomizeClick;
                }

                if (dr["FunctionName"].ToString() == "Cust. Picture")
                {
                    PosItem.Content = Properties.Resources.Cust__Picture;
                    PosItem.ToolTip = Properties.Resources.Customer_Picture;
                    PosItem.Click += btnCustPhotoClick;
                }

                if (dr["FunctionName"].ToString() == "Cust. Notes")
                {
                    PosItem.Content =Properties.Resources.Cust__Notes ;
                    PosItem.ToolTip = Properties.Resources.Customer_Notes ;
                    PosItem.Click += btnCustNotesClick;
                }

                if (dr["FunctionName"].ToString() == "Emp. Picture")
                {
                    PosItem.Content =Properties.Resources.Emp__Picture ;
                    PosItem.ToolTip = Properties.Resources.Employee_Picture;
                    PosItem.Click += btnEmpPhotoClick;
                }

                if (dr["FunctionName"].ToString() == "Product Picture")
                {
                    PosItem.Content =Properties.Resources.Product_Picture  ;
                    PosItem.ToolTip = Properties.Resources.Product_Picture  ;
                    PosItem.Click += btnProductPhotoClick;
                }

                if (dr["FunctionName"].ToString() == "Product Notes")
                {
                    PosItem.Content =Properties.Resources.Product_Notes ;
                    PosItem.ToolTip = Properties.Resources.Product_Notes;
                    PosItem.Click += btnProductNoteClick;
                }

                if (dr["FunctionName"].ToString() == "View Product Price")
                {
                    PosItem.Content =Properties.Resources.View_Product_Price;
                    PosItem.ToolTip = Properties.Resources.View_Product_Price ;
                    PosItem.Click += btnViewPriceClick;
                }

                if (dr["FunctionName"].ToString() == "Change Product Price")
                {
                    PosItem.Content =Properties.Resources.Change_Product_Price;
                    PosItem.ToolTip = Properties.Resources.Change_Product_Price ;
                    PosItem.Click += btnChangePriceClick;
                }

                if (dr["FunctionName"].ToString() == "Use Price Level")
                {
                    PosItem.Content =Properties.Resources.Use_Price_Level ;
                    PosItem.ToolTip = Properties.Resources.Use_Price_Level ;
                    PosItem.Click += btnUsePriceClick;
                }

                if (dr["FunctionName"].ToString() == "Fast Cash")
                {
                    PosItem.Content =Properties.Resources.Fast_Cash  ;
                    PosItem.ToolTip = Properties.Resources.Fast_Cash ;
                    PosItem.Click += btnFastCashClick;
                }

                if (dr["FunctionName"].ToString() == "Gift Cert Balance")
                {
                    PosItem.Content =Properties.Resources.Gift_Cert_Balance ;
                    PosItem.ToolTip = Properties.Resources.Gift_Certificate_Balance ;
                    PosItem.Click += btnGiftCertBalanceClick;
                }

                if (dr["FunctionName"].ToString() == "Invoice Item Notes")
                {
                    PosItem.Content =Properties.Resources.Invoice_Item_Notes;
                    PosItem.ToolTip = Properties.Resources.Invoice_Item_Notes ;
                    PosItem.Click += btnINVNotesClick;
                }

                if (dr["FunctionName"].ToString() == "Work Order")
                {
                    PosItem.Content =Properties.Resources.Work_Order  ;
                    PosItem.ToolTip = Properties.Resources.Work_Order;
                    PosItem.Click += btnWorkOrderClick;
                }

                if (dr["FunctionName"].ToString() == "Print Cust. Label")
                {
                    PosItem.Content =Properties.Resources.Print_Cust__Label;
                    PosItem.ToolTip = Properties.Resources.Print_Cust__Label ;
                    PosItem.Click += btnPrintLabelClick;
                }

                if (dr["FunctionName"].ToString() == "Print Gift Receipt")
                {
                    PosItem.Content =Properties.Resources.Print_Gift_Receipt;
                    PosItem.ToolTip = Properties.Resources.Print_Gift_Receipt ;
                    PosItem.Click += btnPrintGiftReceiptClick;
                }

                if (dr["FunctionName"].ToString() == "Discount Ticket")
                {
                    PosItem.Content =Properties.Resources.Discount_Ticket ;
                    PosItem.ToolTip = Properties.Resources.Discount_on_Ticket;
                    PosItem.Click += btnCouponClick;
                }

                if (dr["FunctionName"].ToString() == "Book Appt.")
                {
                    PosItem.Content =Properties.Resources.Book_Appt_  ;
                    PosItem.ToolTip = Properties.Resources.Appointment_Book ;
                    PosItem.Click += btnApptBookClick;
                }

                if (dr["FunctionName"].ToString() == "Recall Appt.")
                {
                    PosItem.Content =Properties.Resources.Recall_Appt_ ;
                    PosItem.ToolTip = Properties.Resources.Appointment_Recall;
                    PosItem.Click += btnApptRecallClick;
                }

                if (dr["FunctionName"].ToString() == "Recall Rent")
                {
                    PosItem.Content =Properties.Resources.Recall_Rent ;
                    PosItem.ToolTip = Properties.Resources.Recall_Rent;
                    PosItem.Click += btnRentRecallClick;
                }

                if (dr["FunctionName"].ToString() == "Recall Repair")
                {
                    PosItem.Content =Properties.Resources.Recall_Repair  ;
                    PosItem.ToolTip = Properties.Resources.Recall_Repair ;
                    PosItem.Click += btnRepairRecallClick;
                }

                if (dr["FunctionName"].ToString() == "Revert CARD Tran.")
                {
                    PosItem.Content =Properties.Resources.Revert_CARD_Tran_ ;
                    PosItem.ToolTip = Properties.Resources.Revert_Card_Transaction ;
                    PosItem.Click += btnCardTranClick;
                }

                if (dr["FunctionName"].ToString() == "Mercury Gift Card")
                {
                    if (Settings.PaymentGateway == 3)
                    {
                        PosItem.Content =Properties.Resources.Precidia_Gift_Card ;
                        PosItem.ToolTip = Properties.Resources.Precidia_Gift_Card ;
                    }
                    else if (Settings.PaymentGateway == 5)
                    {
                        PosItem.Content =Properties.Resources.Datacap_Gift_Card ;
                        PosItem.ToolTip = Properties.Resources.Datacap_Gift_Card  ;
                    }
                    else if (Settings.PaymentGateway == 7)
                    {
                        PosItem.Content =Properties.Resources.POSLink_Gift_Card  ;
                        PosItem.ToolTip = Properties.Resources.POSLink_Gift_Card ;
                    }
                    else
                    {
                        PosItem.Content =Properties.Resources.Mercury_Gift_Card ;
                        PosItem.ToolTip = Properties.Resources.Mercury_Gift_Card ;
                    }
                    PosItem.Click += btnMcryGiftCardClick;
                }

                if (dr["FunctionName"].ToString() == "Fast CC")
                {
                    PosItem.Content =Properties.Resources.Fast_CC ;
                    PosItem.ToolTip = Properties.Resources.Fast_CC ;
                    PosItem.Click += btnFastCCClick;
                }

                if (dr["FunctionName"].ToString() == "Fees & Charges Item")
                {
                    PosItem.Content =Properties.Resources.Fees____Charges_Item  ;
                    PosItem.ToolTip = Properties.Resources.Fees___Charges_on_Item  ;
                    PosItem.Click += btnfeesClick;
                }

                if (dr["FunctionName"].ToString() == "Fees & Charges Ticket")
                {
                    PosItem.Content =Properties.Resources.Fees____Charges_Ticket;
                    PosItem.ToolTip = Properties.Resources.Fees___Charges_on_Ticket;
                    PosItem.Click += btnFeesCouponClick;
                }

                if (dr["FunctionName"].ToString() == "EBT/ Mercury Gift Card Balance")
                {
                    if (Settings.PaymentGateway == 3)
                    {
                        PosItem.Content = Properties.Resources.EBT__Precidia_Gift_Card_Balance ;
                        PosItem.ToolTip = Properties.Resources.EBT__Precidia_Gift_Card_Balance ;
                    }
                    else if (Settings.PaymentGateway == 5)
                    {
                        PosItem.Content = Properties.Resources.EBT__Datacap_Gift_Card_Balance ;
                        PosItem.ToolTip = Properties.Resources.EBT__Datacap_Gift_Card_Balance ;
                    }
                    else if (Settings.PaymentGateway == 7)
                    {
                        PosItem.Content = Properties.Resources.EBT__POSLink_Gift_Card_Balance  ;
                        PosItem.ToolTip = Properties.Resources.EBT__POSLink_Gift_Card_Balance ;
                    }
                    else
                    {
                        PosItem.Content = Properties.Resources.EBT__Mercury_Gift_Card_Balance;
                        PosItem.ToolTip = Properties.Resources.EBT__Mercury_Gift_Card_Balance;
                    }
                    PosItem.Click += btnebtbalclick;
                }

                if (dr["FunctionName"].ToString() == "Bottle Refund")
                {
                    PosItem.Content =Properties.Resources.Bottle_Refund  ;
                    PosItem.ToolTip = Properties.Resources.Bottle_Refund;
                    PosItem.Click += btnBottleRefundClick;
                }

                if (dr["FunctionName"].ToString() == "Discount Item")
                {
                    PosItem.Content =Properties.Resources.Discount_Item ;
                    PosItem.ToolTip = Properties.Resources.Discount_on_Item;
                    PosItem.Click += btnItemDiscountClick;
                }

                if (dr["FunctionName"].ToString() == "Qty (+)")
                {
                    PosItem.Content =Properties.Resources.Qty  + " (+)";
                    PosItem.ToolTip = Properties.Resources.Qty   + " (+)";
                    PosItem.Click += btnAddQtyClick;
                }

                if (dr["FunctionName"].ToString() == "Qty (-)")
                {
                    PosItem.Content =Properties.Resources.Qty   + " (-)";
                    PosItem.ToolTip = Properties.Resources.Qty  + " (-)";
                    PosItem.Click += btnMinusQtyClick;
                }

                if (dr["FunctionName"].ToString() == "Tare")
                {
                    PosItem.Content =Properties.Resources.Tare  ;
                    PosItem.ToolTip = Properties.Resources.Tare ;
                    PosItem.Click += btnTareClick;
                }

                if (dr["FunctionName"].ToString() == "Check In/Out")
                {
                    PosItem.Content =Properties.Resources.Check_In_Out ;
                    PosItem.ToolTip = Properties.Resources.Check_In_Out ;
                    PosItem.Click += btnCheckInOutclick;
                }

                if (dr["FunctionName"].ToString() == "Points to Store Credit")
                {
                    PosItem.Content =Properties.Resources.Points_to_Store_Credit ;
                    PosItem.ToolTip = Properties.Resources.Points_to_Store_Credit  ;
                    PosItem.Click += btnPointToStoreCreditclick;
                }

                if (dr["FunctionName"].ToString() == "Lotto Payout")
                {
                    PosItem.Content =Properties.Resources.Lotto_Payout;
                    PosItem.ToolTip = Properties.Resources.Lotto_Payout  ;
                    PosItem.Click += btnLottoPayoutClick;
                }

                PosItem.Visibility = Visibility.Visible;
                pnlBody.Children.Add(PosItem);
            }
        }

        private void btnHelpClick(object sender, RoutedEventArgs e)
        {
            btnHelpClickCommnand?.Execute(null);
        }

        private void btnPaidOutClick(object sender, RoutedEventArgs e)
        {
            btnPaidOutClickCommnand?.Execute(null);
        }

        private void btnNoSaleClick(object sender, RoutedEventArgs e)
        {
            btnNoSaleClickCommnand?.Execute(null);
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            btnCancelClickCommnand?.Execute(null);
        }

        private void btnLayawayClick(object sender, RoutedEventArgs e)
        {
            btnLayawayClickCommnand?.Execute(null);
        }

        private void btnAcctPayClick(object sender, RoutedEventArgs e)
        {
            btnAcctPayClickCommnand?.Execute(null);
        }

        private void btnGiftCertClick(object sender, RoutedEventArgs e)
        {
            btnGiftCertClickCommnand?.Execute(null);
        }

        private void btnResumeSuspendClick(object sender, RoutedEventArgs e)
        {
            btnResumeSuspendClickCommnand?.Execute(null);
        }

        private void btnReprintClick(object sender, RoutedEventArgs e)
        {
            btnReprintClickCommnand?.Execute(null);
        }

        private void btnup1Click(object sender, RoutedEventArgs e)
        {
            btnup1ClickCommnand?.Execute(null);
        }

        private void btndown1Click(object sender, RoutedEventArgs e)
        {
            btndown1ClickCommnand?.Execute(null);
        }

        private void btnStockRefreshClick(object sender, RoutedEventArgs e)
        {
            btnStockRefreshClickCommnand?.Execute(null);
        }

        private void btnCustomizeClick(object sender, RoutedEventArgs e)
        {
            btnCustomizeClickCommnand?.Execute(null);
        }

        private void btnCustPhotoClick(object sender, RoutedEventArgs e)
        {
            btnCustPhotoClickCommnand?.Execute(null);
        }

        private void btnCustNotesClick(object sender, RoutedEventArgs e)
        {
            btnCustNotesClickCommnand?.Execute(null);
        }

        private void btnEmpPhotoClick(object sender, RoutedEventArgs e)
        {
            btnEmpPhotoClickCommnand?.Execute(null);
        }

        private void btnProductPhotoClick(object sender, RoutedEventArgs e)
        {
            btnProductPhotoClickCommnand?.Execute(null);
        }

        private void btnProductNoteClick(object sender, RoutedEventArgs e)
        {
            btnProductNoteClickCommnand?.Execute(null);
        }

        private void btnViewPriceClick(object sender, RoutedEventArgs e)
        {
            btnViewPriceClickCommnand?.Execute(null);
        }

        private void btnChangePriceClick(object sender, RoutedEventArgs e)
        {
            btnChangePriceClickCommnand?.Execute(null);
        }

        private void btnUsePriceClick(object sender, RoutedEventArgs e)
        {
            btnUsePriceClickCommnand?.Execute(null);
        }

        private void btnFastCashClick(object sender, RoutedEventArgs e)
        {
            btnFastCashClickCommnand?.Execute(null);
        }

        private void btnGiftCertBalanceClick(object sender, RoutedEventArgs e)
        {
            btnGiftCertBalanceClickCommnand?.Execute(null);
        }

        private void btnINVNotesClick(object sender, RoutedEventArgs e)
        {
            btnINVNotesClickCommnand?.Execute(null);
        }

        private void btnWorkOrderClick(object sender, RoutedEventArgs e)
        {
            btnWorkOrderClickCommnand?.Execute(null);
        }

        private void btnPrintLabelClick(object sender, RoutedEventArgs e)
        {
            btnPrintLabelClickCommnand?.Execute(null);
        }

        private void btnPrintGiftReceiptClick(object sender, RoutedEventArgs e)
        {
            btnPrintGiftReceiptClickCommnand?.Execute(null);
        }

        private void btnCouponClick(object sender, RoutedEventArgs e)
        {
            btnCouponClickCommnand?.Execute(null);
        }

        private void btnApptBookClick(object sender, RoutedEventArgs e)
        {
            btnApptBookClickCommnand?.Execute(null);
        }

        private void btnApptRecallClick(object sender, RoutedEventArgs e)
        {
            btnApptRecallClickCommnand?.Execute(null);
        }

        private void btnRentRecallClick(object sender, RoutedEventArgs e)
        {
            btnRentRecallClickCommnand?.Execute(null);
        }

        private void btnRepairRecallClick(object sender, RoutedEventArgs e)
        {
            btnRepairRecallClickCommnand?.Execute(null);
        }

        private void btnCardTranClick(object sender, RoutedEventArgs e)
        {
            btnCardTranClickCommnand?.Execute(null);
        }

        private void btnMcryGiftCardClick(object sender, RoutedEventArgs e)
        {
            btnMcryGiftCardClickCommnand?.Execute(null);
        }

        private void btnFastCCClick(object sender, RoutedEventArgs e)
        {
            btnFastCCClickCommnand?.Execute(null);
        }

        private void btnfeesClick(object sender, RoutedEventArgs e)
        {
            btnfeesClickCommnand?.Execute(null);
        }

        private void btnFeesCouponClick(object sender, RoutedEventArgs e)
        {
            btnFeesCouponClickCommnand?.Execute(null);
        }

        private void btnebtbalclick(object sender, RoutedEventArgs e)
        {
            btnebtbalClickCommnand?.Execute(null);
        }

        private void btnBottleRefundClick(object sender, RoutedEventArgs e)
        {
            btnBottleRefundClickCommnand?.Execute(null);
        }

        private void btnItemDiscountClick(object sender, RoutedEventArgs e)
        {
            btnItemDiscountClickCommnand?.Execute(null);
        }

        private void btnAddQtyClick(object sender, RoutedEventArgs e)
        {
            btnAddQtyClickCommnand?.Execute(null);
        }

        private void btnMinusQtyClick(object sender, RoutedEventArgs e)
        {
            btnMinusQtyClickCommnand?.Execute(null);
        }

        private void btnTareClick(object sender, RoutedEventArgs e)
        {
            btnTareClickCommnand?.Execute(null);
        }

        private void btnCheckInOutclick(object sender, RoutedEventArgs e)
        {
            btnCheckInOutClickCommnand?.Execute(null);
        }

        private void btnPointToStoreCreditclick(object sender, RoutedEventArgs e)
        {
            btnPointToStoreCreditClickCommnand?.Execute(null);
        }

        private void btnLottoPayoutClick(object sender, RoutedEventArgs e)
        {
            btnLottoPayoutClickCommnand?.Execute(null);
        }
    }
}
