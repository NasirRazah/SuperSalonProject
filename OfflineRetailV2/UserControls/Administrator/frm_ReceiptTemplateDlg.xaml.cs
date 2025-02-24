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
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Ribbon;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_ReceiptTemplateDlg.xaml
    /// </summary>
    public partial class frm_ReceiptTemplateDlg : Window
    {
        bool bSaveDesignerInNewMode = false;

        private frm_PrinterTemplateBrwUC frmBrowse;
        public frm_PrinterTemplateBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_PrinterTemplateBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }
        private string strTemplateName;
        private string strTemplateType;
        private string strTemplateSize;

        private DataTable dclonePrint = null;

        public string TemplateName
        {
            get { return strTemplateName; }
            set { strTemplateName = value; }
        }

        public string TemplateType
        {
            get { return strTemplateType; }
            set { strTemplateType = value; }
        }

        public string TemplateSize
        {
            get { return strTemplateSize; }
            set { strTemplateSize = value; }
        }
        private bool boolControlChanged;
        ReceiptTemplateControls.rt_GroupAll popupuc = null;
        bool blAddSpecificPosition = false;
        int intSpecificSL = 0;

        private bool blEditData = false;

        private string currGroupName = "";
        private int currOrderSL = 0;
        private int currID = 0;
        private int currSL = 0;
        private int currSubSL = 0;
        private bool boolLoadParam = false;
        private DataTable dtblTemplate = null;
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private DataTable dtblDummyTemplateData;

        private DataTable dtblstrm = null;

        private int intID;
        private int intNewID;
        

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        

        public frm_ReceiptTemplateDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            try
            {
                if (txtName.Text == "Label - 1 Up")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - 2 Up")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("TwoUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Butterfly")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Butterfly", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 5160 / NEBS 12650")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery5160", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 8195")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery8195", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

            }
            catch
            {

            }

            CloseKeyboards();
            Close();
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }


        private void PopulateTemplateTypes()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Name");
            dtbl.Rows.Add(new object[] { "Receipt" });
            dtbl.Rows.Add(new object[] { "Layaway" });
            dtbl.Rows.Add(new object[] { "Rent Item Issue" });
            dtbl.Rows.Add(new object[] { "Rent Item Return" });
            dtbl.Rows.Add(new object[] { "Repair Item Receive" });
            dtbl.Rows.Add(new object[] { "Repair Item Return" });
            dtbl.Rows.Add(new object[] { "WorkOrder" });
            dtbl.Rows.Add(new object[] { "Suspend Receipt" });
            dtbl.Rows.Add(new object[] { "Closeout" });
            dtbl.Rows.Add(new object[] { "No Sale" });
            dtbl.Rows.Add(new object[] { "Paid Out" });
            dtbl.Rows.Add(new object[] { "Paid In" });
            dtbl.Rows.Add(new object[] { "Safe Drop" });
            dtbl.Rows.Add(new object[] { "Lotto Payout" });
            dtbl.Rows.Add(new object[] { "Customer Label" });
            dtbl.Rows.Add(new object[] { "Item" });
            dtbl.Rows.Add(new object[] { "Gift Receipt" });
            dtbl.Rows.Add(new object[] { "Gift Aid Receipt" });
            dtbl.Rows.Add(new object[] { "Label - 1 Up" });
            dtbl.Rows.Add(new object[] { "Label - 2 Up" });
            dtbl.Rows.Add(new object[] { "Label - Butterfly" });
            dtbl.Rows.Add(new object[] { "Label - Avery 5160 / NEBS 12650" });
            dtbl.Rows.Add(new object[] { "Label - Avery 8195" });

            txtName.ItemsSource = dtbl;
            txtName.DisplayMember = "Name";
            txtName.ValueMember = "Name";
            dtbl.Dispose();
            txtName.EditValue = "Receipt";
        }



        public void ShowHeaderData()
        {
            PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
            objTenderTypes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objTenderTypes.ShowData(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtName.Text = dr["TemplateType"].ToString();
                txtTemplateName.Text = dr["TemplateName"].ToString();
                cmbTemplateSize.Text = dr["TemplateSize"].ToString();
                cmbPrintCopy.SelectedIndex = GeneralFunctions.fnInt32(dr["PrintCopy"].ToString()) - 1;
                txtTemplateW.Text = dr["LabelTemplateWidth"].ToString();
                txtTemplateH.Text = dr["LabelTemplateHeight"].ToString();
            }
            dbtbl.Dispose();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            dclonePrint = new DataTable();
            dtblDummyTemplateData = new DataTable();
            dtblDummyTemplateData.Columns.Add("GroupName", System.Type.GetType("System.String"));
            dtblDummyTemplateData.Columns.Add("GroupData", System.Type.GetType("System.String"));

            
            dtblDummyTemplateData.Rows.Add(new object[] { "Reprint/Void Caption", "Reprinted - 1" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Receipt Date", "Date: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "User Name", "Emp: " + "John" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Till Name", "Terminal: " + "001" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Printer Name", "Printer:1" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Item and Qty", "Item 1: 10" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Business Name", "Retail Company" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Business Address", "123 Ring Road\r\nUK" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Work Order Number", "Work Order Number: 1" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Work Order Date", "Date: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "Date", "Date: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "Rent - Issue", "Rent - Issue" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Rent - Return", "Rent - Return" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Receipt Number", "Receipt Number: 1234" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Company", "Company: Customer Company" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Name", "Customer: Ryan Harris" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Code", "Customer ID: 5467" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Address", "Address: 256 Sea Link Avenue" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Name", "Ryan Harris" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Address", "256 Sea Link Avenue" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Gift Aid Amount", "Amount: " + SystemVariables.CurrencySymbol + "100.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Mail Address", "256 Sea Link Avenue" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Phone", "Ph: 0123456789" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Customer Date of Birth", "Date of Birth: " + DateTime.Today.Date.AddYears(-27).ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "Subtotal Amount", "Sub Total: " + SystemVariables.CurrencySymbol + "10.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tax Amount", "Tax: " + SystemVariables.CurrencySymbol + "0.25" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Amount", "Discount: " + SystemVariables.CurrencySymbol + "0.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Total Amount", "Total: " + SystemVariables.CurrencySymbol + "10.25" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tender Amount", "Tender" + "\r\n" + "Cash: " + SystemVariables.CurrencySymbol + "12.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Change Due Amount", "Change: (" + SystemVariables.CurrencySymbol + "1.75)" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Gift Aid Caption", "** Gift Aid **" });

            dtblDummyTemplateData.Rows.Add(new object[] { "No Sale Caption", "*** No Sale ***" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Paid Out Caption", "*** Paid Outs ***" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid In Caption", "*** Paid In ***" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Safe Drop Caption", "*** Safe Drop ***" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid Out Explanation", "Paid out notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid Out Amount", SystemVariables.CurrencySymbol + "100.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid In Explanation", "Paid In notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid In Amount", SystemVariables.CurrencySymbol + "100.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Safe Drop Amount", SystemVariables.CurrencySymbol + "100.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Card Payment Reference", "Card No#: XXXXXXXXX8974" + "\r\n" + "Auth Code: 827634" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Card Holder Copy", "CARDHOLDER COPY" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Merchant Copy", "MERCHANT COPY" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Lotto Payout Caption", "*** Lotto Payout ***" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Lotto Payout Explanation", "notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Lotto Payout Amount", SystemVariables.CurrencySymbol + "100.00" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Fees Amount", "Sub Total: " + SystemVariables.CurrencySymbol + "0.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Cancellation Caption", "Layaway Cancellation" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Receipt/Layaway Number", "Layaway Number: 1234" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Payment Details", "01/10/2023 Cash: " + SystemVariables.CurrencySymbol + "1.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Balance Due Amount", "Balance: " + SystemVariables.CurrencySymbol + "5.25" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Due", "Due on: " + DateTime.Today.Date.AddDays(7).ToString(SystemVariables.DateFormat) });

            dtblDummyTemplateData.Rows.Add(new object[] { "Receipt Footer", "Thank you !!" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Policy", "Layaway Policy Text" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Rent Issue Caption", "Rent - Issue" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Security Deposit Amount", "Security Deposit: " + SystemVariables.CurrencySymbol + "1.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Return Deposit Amount", "Retun Amount: " + SystemVariables.CurrencySymbol + "1.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Due Amount", "Due: " + SystemVariables.CurrencySymbol + "0.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Reference Invoice Number", "Ref Invoice# : 78458" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Rent Return Caption", "Rent - Return" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Issue Caption", "Repair Issue" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Date In", "Date In: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "Delivery Date", "Delivery Date: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });
            dtblDummyTemplateData.Rows.Add(new object[] { "Notified Date", "Notified Date: " + DateTime.Today.Date.ToString(SystemVariables.DateFormat) });

            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Item Name", "Samsung Mobile Phone" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Item Serial No", "Serial #: 45670225" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Item Problem", "problem details" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Receipt Footer/Repair Disclaimer", "Thank you !!" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Transaction/ Advance Amount", "Tran Amount: " + SystemVariables.CurrencySymbol + "2.00" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Deliver Caption", "Repair Deliver" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Problem", "problem details" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Notes", "notes" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Report Caption", "CLOSEOUT REPORT" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Type", "By Terminal\r\nTerminal #:0001" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Transaction Summary", "TRANSACTION SUMMARY" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Number", "Close Out# : 45127" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Notes", "notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Begin Date/Time", "Begin: 10/02/2023 22.30 pm" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout End Date/Time", "Begin: 10/02/2023 23.46 pm" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Sales Caption", "SALES" });
            dtblDummyTemplateData.Rows.Add(new object[] { "No of Invoices (Sales)", "No. of Invoices: 41" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Product Sales Amount", "Product Sales: " + SystemVariables.CurrencySymbol + "25.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Product Sales Amount(Taxed)", "Taxed: " + SystemVariables.CurrencySymbol + "10.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Product Sales Amount(Non Taxed)", "Non Taxed: " + SystemVariables.CurrencySymbol + "15.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Service Sales Amount", "Service Sales: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Service Sales Amount(Taxed)", "Taxed: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Service Sales Amount(Non Taxed)", "Non Taxed: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Other Sales Amount", "Other Sales: " + SystemVariables.CurrencySymbol + "2.50" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Other Sales Amount(Taxed)", "Taxed: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Other Sales Amount(Non Taxed)", "Non Taxed: " + SystemVariables.CurrencySymbol + "2.50" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Tax Details (Sales)", "Sales Tax: " + SystemVariables.CurrencySymbol + "4.20" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Caption (Sales)", "DISCOUNTS" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Details (Sales)", "Item\r\nProduct\r\nService\r\nOther\r\nInvoice" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Free Items", "Free Items" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Fees Amount (Sales)", "Fees & Charges: " + SystemVariables.CurrencySymbol + "0.00\r\nFees & Charges Tax:" + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Total Amount", "Total: " + SystemVariables.CurrencySymbol + "27.50" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Rent Caption", "RENT" });
            dtblDummyTemplateData.Rows.Add(new object[] { "No of Invoices (Rent)", "No. of Invoices: 0" }); 
            dtblDummyTemplateData.Rows.Add(new object[] { "Net Issued (Rent)", "No. of Invoices: 41" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tax Details (Rent)", "Sales Tax: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Rent Deposit Amount", "Deposit: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Rent Deposit Return Amount", "Return: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Caption (Rent)", "DISCOUNTS" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Details (Rent)", "Item\r\nInvoice" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Fees Amount (Rent)", "Fees: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Caption", "REPAIR" });
            dtblDummyTemplateData.Rows.Add(new object[] { "No of Invoices (Repair)", "No. of Invoices: 0" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Sales Amount (Repair)", "Sales: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tax Details (Repair)", "Sales Tax: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Repair Deposit Amount", "Deposit: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Caption (Repair)", "DISCOUNTS" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Discount Details (Repair)", "Item\r\nInvoice" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Fees Amount (Repair)", "Fees: " + SystemVariables.CurrencySymbol + "0.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Non Sales Caption", "NON-SALES" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Deposit", "Layaway Deposits: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Refund", "Layaway Refunds: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Payment", "Layaway Payments: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Layaway Sales Posted", "Layaway Sales Posted: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Number of No Sale", "No Sale Count: 1" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Paid Out", "Paid Outs: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Lotto Payout", "Lotto Payout: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Gift Certificate Sold", "Gift Cert. Sold: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "House Account Payment", "House Account Payments:	" + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Bottle Refund", "Bottle Refund: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Mercury/Datacap/PosLink Gift Card Sold", "Gift Cert. Sold: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Store Credit Caption", "STORE CREDIT" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Store Credit Issued", "Issued: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Store Credit Redeemed", "Redeemed: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "House Account Caption", "HOUSE ACCOUNT" });
            dtblDummyTemplateData.Rows.Add(new object[] { "House Account Charged", "Charged: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "House Account Payment", "Account Payments: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Return Items", "RETURNS	    Invoice# " });
            dtblDummyTemplateData.Rows.Add(new object[] { "No Return Caption", "No Returns" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Tender Details", "TENDER RECONCILIATION\r\nTender\r\n Cash: " + SystemVariables.CurrencySymbol + "12.00\r\nDebit Card: " + SystemVariables.CurrencySymbol + "3.20\r\nTotal Amount: " + SystemVariables.CurrencySymbol + "15.20" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tender Count Details", "COUNTS\r\nTotal Counted: " + SystemVariables.CurrencySymbol + "0.00" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Tender Over/Short Details", "COUNTS\r\nTotal Over/(Short): " + SystemVariables.CurrencySymbol + "0.00" });

            dtblDummyTemplateData.Rows.Add(new object[] { "Total Amount", "Total: " + SystemVariables.CurrencySymbol + "27.50" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Hour Caption", "SALES BY HOUR" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Type (Sales by Hour)", "By Terminal\r\nTerminal #:0001" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Number (Sales by Hour)", "Close Out# : 45127" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Notes (Sales by Hour)", "notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Begin Date/Time (Sales by Hour)", "Begin: 10/02/2023 22.30 pm" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout End Date/Time (Sales by Hour)", "Begin: 10/02/2023 23.46 pm" });

           
            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Hour Details", "10:00 AM -  11:00 AM: " + SystemVariables.CurrencySymbol + "2.50\r\n" + "11:00 AM -  12:00 N :" + SystemVariables.CurrencySymbol + "12.50" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Hour Total Amount", "Total Sales: " + SystemVariables.CurrencySymbol + "25.50" });



            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Department Caption", "SALES BY DEPARTMENT" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Type (Sales by Department)", "By Terminal\r\nTerminal #:0001" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Number (Sales by Department)", "Close Out# : 45127" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Notes (Sales by Department)", "notes" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout Begin Date/Time (Sales by Department)", "Begin: 10/02/2023 22.30 pm" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Closeout End Date/Time (Sales by Department)", "Begin: 10/02/2023 23.46 pm" });


            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Department Details", "Grocery: " + SystemVariables.CurrencySymbol + "17.50\r\n" + "Soft Drinks: " + SystemVariables.CurrencySymbol + "8.25" });
            dtblDummyTemplateData.Rows.Add(new object[] { "Sales by Department Total Amount", "Total Sales: " + SystemVariables.CurrencySymbol + "25.50" });


            //txtTemplateName.Text = strTemplateName;
            //cmbTemplateSize.Text = strTemplateSize;


            popupuc = new ReceiptTemplateControls.rt_GroupAll();
            dtblTemplate = new DataTable();
            dtblTemplate.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("GroupName", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("GroupSL", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("GroupSubSL", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("GroupData", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("TextAlign", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("TextStyle", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("FontSize", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("CtrlWidth", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("CtrlHeight", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("Display", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("CustomImage", typeof(byte[]));
            dtblTemplate.Columns.Add("SL", System.Type.GetType("System.Int32"));
            dtblTemplate.Columns.Add("ShowHeader1", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("ShowHeader2", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("ShowHeader3", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("Header1Caption", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("Header2Caption", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("Header3Caption", System.Type.GetType("System.String"));
            dtblTemplate.Columns.Add("CtrlPositionTop", System.Type.GetType("System.Int32"));
            
            fkybrd = new FullKeyboard();


            PopulateTemplateTypes();

            if (intID == 0)
            {
                lbTitle.Text = "Add Printer Template";
            }
            else
            {
                lbTitle.Text = "Edit Printer Template";
                txtName.IsEnabled = false;
                ShowHeaderData();
            }


            tbctrl.Style = this.FindResource("TemplateOuterPOSTabControlStyle") as Style;
            tpEdit.Visibility = Visibility.Collapsed;

            if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
            {
                tbctrl.SelectedIndex = 2;
            }
            else
            {
                tbctrl.SelectedIndex = 1;
            }

            //tbctrl.SelectedIndex = txtName.Text == "1 Up Label" ? 2 : 1;

            /*PopulateDefaultParamData();
            LoadInitialLinkData();
            LoadTemplate();
            GetPrintStream();
            CreateFlowControl(dclonePrint);*/
            boolControlChanged = false;
            boolLoadParam = true;
        }



        public void PopulateDefaultParamData()
        {
            PosDataObject.ReceiptTemplate objCategory = new PosDataObject.ReceiptTemplate();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchDefaultParameterData(txtName.Text);

           

            cmbGroup.ItemsSource = dbtblCat;
            cmbGroup.DisplayMember = "GroupName";
            cmbGroup.ValueMember = "ID";
        }

        private void LoadTemplate()
        {
            pnlTemplate.Children.Clear();

            dtblTemplate.DefaultView.Sort = "SL asc";
            dtblTemplate.DefaultView.ApplyDefaultSort = true;


            //foreach (DataRowView dr in dtblTemplate.DefaultView)
            foreach (DataRowView dr in dtblTemplate.DefaultView)
            {
                if (dr["Display"].ToString() == "N") continue;

                ReceiptTemplateControls.rt_GroupAll ctrl = new ReceiptTemplateControls.rt_GroupAll();

                ctrl.ShowHeader1 = dr["ShowHeader1"].ToString();
                ctrl.ShowHeader2 = dr["ShowHeader2"].ToString();
                ctrl.ShowHeader3 = dr["ShowHeader3"].ToString();

                ctrl.Header1Caption = dr["Header1Caption"].ToString();
                ctrl.Header2Caption = dr["Header2Caption"].ToString();
                ctrl.Header3Caption = dr["Header3Caption"].ToString();



                ctrl.SL = GeneralFunctions.fnInt32(dr["SL"].ToString());


                ctrl.ID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                ctrl.GroupSL = GeneralFunctions.fnInt32(dr["GroupSL"].ToString());
                ctrl.GroupSubSL = GeneralFunctions.fnInt32(dr["GroupSubSL"].ToString());
                ctrl.GroupName = dr["GroupName"].ToString();
                
                ctrl.TextAlign = dr["TextAlign"].ToString();
                ctrl.TextStyle = dr["TextStyle"].ToString();
                ctrl.FontSize = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                ctrl.CtrlWidth = GeneralFunctions.fnInt32(dr["CtrlWidth"].ToString());
                ctrl.CtrlHeight = GeneralFunctions.fnInt32(dr["CtrlHeight"].ToString());
                ctrl.CtrlPositionTop = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());
                ctrl.byt = (dr["CustomImage"].GetType().ToString() == "System.DBNull") ? null : (byte[])dr["CustomImage"];

                string data = "";
                if (dr["GroupName"].ToString() == "Receipt Number") data = "Receipt Number: " + "6547";
                else if (dr["GroupName"].ToString() == "Subtotal Amount") data = "Sub Total: " + SystemVariables.CurrencySymbol + "10.25";
                else if (dr["GroupName"].ToString() == "Tax Amount") data = "Tax: " + SystemVariables.CurrencySymbol + "0.00";
                else if (dr["GroupName"].ToString() == "Discount Amount") data = "Discount: " + SystemVariables.CurrencySymbol + "0.00";
                else if (dr["GroupName"].ToString() == "Total Amount") data = "Total: " + SystemVariables.CurrencySymbol + "10.25";
                else if (dr["GroupName"].ToString() == "Tender Amount") data = "Tender" + "\r\n" + "Cash: " + SystemVariables.CurrencySymbol + "12.00";
                else if (dr["GroupName"].ToString() == "Change Due Amount") data = "Change: (" + SystemVariables.CurrencySymbol + "12.00)";
                else if (dr["GroupName"].ToString() == "Item/Price Header") data = "Unit Price       Net Wt/Ct       Total Price";
                else if (dr["GroupName"].ToString() == "Item/Price Line") data = "Item 1 " + "\r\n" + "10.00       1       10.00";
                else if (dr["GroupName"].ToString() == "Details") data = "Item 1 " + "\r\n" + "10.00       1       10.00";
                else data = dr["GroupData"].ToString();
                ctrl.GroupData = data;
                ctrl.Selected = false;
                (ctrl.FindName("imgEdit") as System.Windows.Controls.Image).PreviewMouseLeftButtonDown += Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown;
               
                (ctrl.FindName("imgAdd") as System.Windows.Controls.Image).PreviewMouseLeftButtonDown += Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown1;

                (ctrl.FindName("imgUp") as System.Windows.Controls.Image).PreviewMouseLeftButtonDown += Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown2;
                (ctrl.FindName("imgDown") as System.Windows.Controls.Image).PreviewMouseLeftButtonDown += Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown3;
                ctrl.PreviewMouseLeftButtonDown += Ctrl_PreviewMouseLeftButtonDown1;
                pnlTemplate.Children.Add(ctrl);
            }
        }

        private void Ctrl_PreviewMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            foreach(UIElement ctrl in pnlTemplate.Children)
            {
                (ctrl as ReceiptTemplateControls.rt_GroupAll).Selected = false;
            }
        }

        private void Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown3(object sender, MouseButtonEventArgs e)
        {
            ReceiptTemplateControls.rt_GroupAll uc = (((sender as System.Windows.Controls.Image).Parent as Grid).Parent as ReceiptTemplateControls.rt_GroupAll);
            intSpecificSL = uc.SL + 1;
            blAddSpecificPosition = true;
            svTemplate.Visibility = Visibility.Collapsed;
        }

        private void Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            ReceiptTemplateControls.rt_GroupAll uc = (((sender as System.Windows.Controls.Image).Parent as Grid).Parent as ReceiptTemplateControls.rt_GroupAll);
            intSpecificSL = uc.SL == 1 ? 0 : uc.SL;
            blAddSpecificPosition = true;
            svTemplate.Visibility = Visibility.Collapsed;

        }

        private void Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            ReceiptTemplateControls.rt_GroupAll uc = (((sender as System.Windows.Controls.Image).Parent as Grid).Parent as ReceiptTemplateControls.rt_GroupAll);
            bool flg = uc.Selected;
            uc.Selected = !flg;
        }

        private void Ctrl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private DataTable LoadInitialLinkData()
        {
            PosDataObject.ReceiptTemplate objCategory = new PosDataObject.ReceiptTemplate();
            objCategory.Connection = SystemVariables.Conn;

            bool bDummyData = false;
            if (intID == 0) bDummyData = true;
            if (intID > 0)
            {
                int countUserDefinedRow = objCategory.CheckLinkCount(intID);
                if (countUserDefinedRow == 0) bDummyData = true;
            } 
            
            dtblTemplate = bDummyData ? objCategory.FetchDummyLinkData(txtName.Text) : objCategory.FetchLinkData(intID);

            return dtblTemplate;
        }

        private void Frm_ReceiptTemplateDlg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReceiptTemplateControls.rt_GroupAll uc = (((sender as System.Windows.Controls.Image).Parent as Grid).Parent as ReceiptTemplateControls.rt_GroupAll);
            popupuc = uc;
            flyoutControl.PlacementTarget = sender as FrameworkElement;
            flyoutControl.IsOpen = true;
        }

        private bool IsValidAll()
        {
            if (txtName.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Template Type");
                GeneralFunctions.SetFocus(txtName);
                return false;
            }
            if (txtTemplateName.Text.Trim() == "")
            {
                DocMessage.MsgEnter("Template Name");
                GeneralFunctions.SetFocus(txtTemplateName);
                return false;
            }

           

            if (DuplicateCount() == 1)
            {
                DocMessage.MsgInformation("Duplicate Template Name");
                GeneralFunctions.SetFocus(txtTemplateName);
                return false;
            }

            return true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*if (txtName.Text == "1 Up Label")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }*/


                if (txtName.Text == "Label - 1 Up")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - 2 Up")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("TwoUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Butterfly")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Butterfly", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 5160 / NEBS 12650")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery5160", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 8195")
                {
                    if (intID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery8195", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            // Check if file is there  
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }

            }
            catch
            {

            }

            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

       

        private bool SaveData()
        {
            int PrevID = intID;
            string strError = "";
            PosDataObject.ReceiptTemplate objClass = new PosDataObject.ReceiptTemplate();
            objClass.Connection = SystemVariables.Conn;
            objClass.LoginUserID = SystemVariables.CurrentUserID;
            objClass.ID = intID;
            objClass.TemplateData = dtblTemplate;
            objClass.TemplateType = txtName.Text.Trim();
            objClass.TemplateName = txtTemplateName.Text.Trim();
            objClass.TemplateSize = cmbTemplateSize.Text;
            objClass.PrintCopy = cmbPrintCopy.SelectedIndex + 1;
            if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
            {
                objClass.LabelTemplateWidth = GeneralFunctions.fnInt32(txtTemplateW.Text);
                objClass.LabelTemplateHeight = GeneralFunctions.fnInt32(txtTemplateH.Text);
            }
            else
            {
                objClass.LabelTemplateWidth = 0;
                objClass.LabelTemplateHeight = 0;
            }
            string err = objClass.PostData();
            
            if (strError == "")
            {
                if (txtName.Text == "Label - 1 Up")
                {
                    if (PrevID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp","new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                            
                            if (fi.Exists)
                            {
                               
                                fi.MoveTo(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("OneUp", objClass.ID.ToString()));
                                
                            }
                        }
                    }
                }

                if (txtName.Text == "Label - 2 Up")
                {
                    if (PrevID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("TwoUp", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);

                            if (fi.Exists)
                            {

                                fi.MoveTo(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("TwoUp", objClass.ID.ToString()));

                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Butterfly")
                {
                    if (PrevID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Butterfly", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);

                            if (fi.Exists)
                            {

                                fi.MoveTo(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Butterfly", objClass.ID.ToString()));

                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 5160 / NEBS 12650")
                {
                    if (PrevID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery5160", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);

                            if (fi.Exists)
                            {

                                fi.MoveTo(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery5160", objClass.ID.ToString()));

                            }
                        }
                    }
                }

                if (txtName.Text == "Label - Avery 8195")
                {
                    if (PrevID == 0)
                    {
                        if (bSaveDesignerInNewMode)
                        {
                            string sourceFile = GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery8195", "new");

                            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);

                            if (fi.Exists)
                            {

                                fi.MoveTo(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave("Avery8195", objClass.ID.ToString()));

                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowData()
        {
            PosDataObject.GLedger objClass = new PosDataObject.GLedger();
            objClass.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objClass.ShowGLRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                
                
            }
        }

        private int DuplicateCount()
        {
            PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
            objTenderTypes.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objTenderTypes.DuplicateCount(intID, txtName.Text.Trim(), txtTemplateName.Text.Trim());
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new System.Windows.Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void CmbGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private int CalculateTopPositionFor1Uplabel()
        {
            if (dtblTemplate.Rows.Count == 0) return 0;
            else
            {
                dtblTemplate.DefaultView.Sort = "CtrlPositionTop asc";
                dtblTemplate.DefaultView.ApplyDefaultSort = true;
                int rows = dtblTemplate.Rows.Count;
                int indx = 0;
                int topval = 0;
                foreach (DataRowView dr in dtblTemplate.DefaultView)
                {
                    indx++;
                    if (indx < rows) continue;
                    if ((dr["GroupName"].ToString() == "SKU") || (dr["GroupName"].ToString() == "Item Name") || (dr["GroupName"].ToString() == "Item Price"))
                    {
                        int fontsize = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                        topval = fontsize + 6 + GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString()) + 2;
                    }
                    else
                    {
                        topval = GeneralFunctions.fnInt32(dr["CtrlHeight"].ToString()) + 6 + GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString()) + 2;
                    }
                   
                }
                return topval;
            }
        }


        private void BtnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            if (boolLoadParam)
            {
                if (txtName.Text == "Label - 1 Up")
                {
                    //OfflineRetailV2.Report.Product.OneUp rep = new OfflineRetailV2.Report.Product.OneUp();
                    //rep.SaveLayout(@"C:\MyProducts\OneUp.repx");
                    frmDesigner frm = new frmDesigner();
                    try
                    {
                        Cursor = Cursors.Wait;
                        frm.ID = intID;
                        frm.WindowState = WindowState.Maximized;
                        frm.TemplateName = txtTemplateName.Text;
                        frm.TemplateType = "OneUp";
                        frm.ShowDialog();
                        Cursor = Cursors.Arrow;
                        if (intID == 0)
                        {
                            bSaveDesignerInNewMode = frm.SaveDesigner;
                            if (bSaveDesignerInNewMode) boolControlChanged = true;
                        }
                    }
                    finally
                    {
                        frm.Close();
                    }
                    
                }
                else if (txtName.Text == "Label - 2 Up")
                {
                    
                    frmDesigner frm = new frmDesigner();
                    try
                    {
                        Cursor = Cursors.Wait;
                        frm.ID = intID;
                        frm.WindowState = WindowState.Maximized;
                        frm.TemplateName = txtTemplateName.Text;
                        frm.TemplateType = "TwoUp";
                        frm.ShowDialog();
                        Cursor = Cursors.Arrow;
                        if (intID == 0)
                        {
                            bSaveDesignerInNewMode = frm.SaveDesigner;
                            if (bSaveDesignerInNewMode) boolControlChanged = true;
                        }
                    }
                    finally
                    {
                        frm.Close();
                    }

                }
                else if (txtName.Text == "Label - Butterfly")
                {

                    frmDesigner frm = new frmDesigner();
                    try
                    {
                        Cursor = Cursors.Wait;
                        frm.ID = intID;
                        frm.WindowState = WindowState.Maximized;
                        frm.TemplateName = txtTemplateName.Text;
                        frm.TemplateType = "Butterfly";
                        frm.ShowDialog();
                        Cursor = Cursors.Arrow;
                        if (intID == 0)
                        {
                            bSaveDesignerInNewMode = frm.SaveDesigner;
                            if (bSaveDesignerInNewMode) boolControlChanged = true;
                        }
                    }
                    finally
                    {
                        frm.Close();
                    }

                }
                else if (txtName.Text == "Label - Avery 5160 / NEBS 12650")
                {

                    frmDesigner frm = new frmDesigner();
                    try
                    {
                        Cursor = Cursors.Wait;
                        frm.ID = intID;
                        frm.WindowState = WindowState.Maximized;
                        frm.TemplateName = txtTemplateName.Text;
                        frm.TemplateType = "Avery5160";
                        frm.ShowDialog();
                        Cursor = Cursors.Arrow;
                        if (intID == 0)
                        {
                            bSaveDesignerInNewMode = frm.SaveDesigner;
                            if (bSaveDesignerInNewMode) boolControlChanged = true;
                        }
                    }
                    finally
                    {
                        frm.Close();
                    }

                }
                else if (txtName.Text == "Label - Avery 8195")
                {

                    frmDesigner frm = new frmDesigner();
                    try
                    {
                        Cursor = Cursors.Wait;
                        frm.ID = intID;
                        frm.WindowState = WindowState.Maximized;
                        frm.TemplateName = txtTemplateName.Text;
                        frm.TemplateType = "Avery8195";
                        frm.ShowDialog();
                        Cursor = Cursors.Arrow;
                        if (intID == 0)
                        {
                            bSaveDesignerInNewMode = frm.SaveDesigner;
                            if (bSaveDesignerInNewMode) boolControlChanged = true;
                        }
                    }
                    finally
                    {
                        frm.Close();
                    }

                }
                else
                {
                    if (cmbGroup.EditValue != null)
                    {

                        currID = GeneralFunctions.fnInt32(cmbGroup.EditValue.ToString());
                        PosDataObject.ReceiptTemplate objCategory = new PosDataObject.ReceiptTemplate();
                        objCategory.Connection = SystemVariables.Conn;
                        DataTable dbtblCat = new DataTable();
                        dbtblCat = objCategory.FetchDefaultParameterData(currID);

                        foreach (DataRow dr in dbtblCat.Rows)
                        {
                            currGroupName = dr["GroupName"].ToString();
                            currSL = GeneralFunctions.fnInt32(dr["SlNo"].ToString());
                        }

                        dbtblCat.Dispose();

                        if ((currGroupName == "Logo") || (currGroupName == "Image") || (currGroupName == "Barcode"))
                        {

                            if ((currGroupName == "Logo") || (currGroupName == "Barcode"))
                            {
                                grdEditLabelText.Visibility = Visibility.Collapsed;
                                grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                                grdEditSeparator.Visibility = Visibility.Collapsed;
                                grdEditGeneral.Visibility = Visibility.Collapsed;
                                grdEditImage.Visibility = Visibility.Collapsed;
                                grdEditOrderHeader.Visibility = Visibility.Collapsed;
                                svTemplate.Visibility = Visibility.Collapsed;
                                grdEditLogoBarcode.Visibility = Visibility.Visible;

                                if ((currGroupName == "Logo"))
                                {
                                    txtHeight.Text = "48";
                                    txtWidth.Text = "48";
                                    cmbAlign1.SelectedIndex = 1;
                                }
                                if ((currGroupName == "Barcode"))
                                {
                                    txtHeight.Text = "40";
                                    txtWidth.Text = "180";
                                    cmbAlign1.SelectedIndex = 1;
                                }

                            }

                            if ((currGroupName == "Image"))
                            {
                                grdEditLabelText.Visibility = Visibility.Collapsed;
                                grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                                grdEditSeparator.Visibility = Visibility.Collapsed;
                                grdEditGeneral.Visibility = Visibility.Collapsed;
                                grdEditOrderHeader.Visibility = Visibility.Collapsed;
                                grdEditImage.Visibility = Visibility.Visible;
                                svTemplate.Visibility = Visibility.Collapsed;
                                grdEditLogoBarcode.Visibility = Visibility.Collapsed;

                                if ((currGroupName == "Image"))
                                {
                                    txtHeight2.Text = "48";
                                    txtWidth2.Text = "48";
                                    cmbAlign2.SelectedIndex = 1;
                                    pictPhoto.Source = null;
                                }

                            }


                        }
                        else if (currGroupName == "Item/Price Header")
                        {
                            grdEditLabelText.Visibility = Visibility.Collapsed;
                            grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                            grdEditSeparator.Visibility = Visibility.Collapsed;
                            grdEditGeneral.Visibility = Visibility.Collapsed;
                            grdEditOrderHeader.Visibility = Visibility.Visible;
                            grdEditImage.Visibility = Visibility.Collapsed;
                            grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                            svTemplate.Visibility = Visibility.Collapsed;

                            chkHeader1.IsChecked = true;
                            chkHeader2.IsChecked = true;
                            chkHeader3.IsChecked = true;

                            txtHeaderCaption1.Text = "Unit Price";
                            txtHeaderCaption2.Text = "Net Wt/Ct";
                            txtHeaderCaption3.Text = "Total Price";

                            cmbStyle3.SelectedIndex = 0;
                            txtFontSize3.Text = "8";

                        }
                        else if (currGroupName == "Separator")
                        {
                            grdEditLabelText.Visibility = Visibility.Collapsed;
                            grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                            grdEditSeparator.Visibility = Visibility.Visible;
                            grdEditGeneral.Visibility = Visibility.Collapsed;
                            grdEditOrderHeader.Visibility = Visibility.Collapsed;
                            grdEditImage.Visibility = Visibility.Collapsed;
                            grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                            svTemplate.Visibility = Visibility.Collapsed;

                            cmbAlign.SelectedIndex = 1;
                            cmbStyle.SelectedIndex = 0;
                            txtFontSize.Text = "8";



                        }
                        else
                        {
                            if (currGroupName == "Text")
                            {
                                txtText.Text = "";
                                txtText.IsReadOnly = false;
                            }
                            else
                            {
                                txtText.Text = currGroupName;
                                txtText.IsReadOnly = true;
                            }
                            grdEditLabelText.Visibility = Visibility.Collapsed;
                            grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                            grdEditSeparator.Visibility = Visibility.Collapsed;
                            grdEditGeneral.Visibility = Visibility.Visible;
                            grdEditOrderHeader.Visibility = Visibility.Collapsed;
                            grdEditImage.Visibility = Visibility.Collapsed;
                            grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                            svTemplate.Visibility = Visibility.Collapsed;

                            cmbAlign.SelectedIndex = 1;
                            cmbStyle.SelectedIndex = 0;
                            txtFontSize.Text = "8";

                            if ((currGroupName == "Item/Price Line") || (currGroupName == "Details"))
                            {
                                cmbAlign.IsEnabled = false;
                            }
                            else
                            {
                                cmbAlign.IsEnabled = true;
                            }

                        }

                    }
                    
                }
            }
            
        }

        private void CmbAlign_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void BtnSubmit1_Click(object sender, RoutedEventArgs e)
        {
            if (currGroupName == "Text")
            {
                if (txtText.Text.Trim() == "")
                {
                    DocMessage.MsgInformation("Enter Text");
                    txtText.Focus();
                    return;
                }
            }
            
            UpdateDatatable();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditGeneral.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void UpdateDatatableForLogoBarcode()
        {
            bool bFindData = false;
            if (blEditData)
            {
                bFindData = true;
            }
            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    currGroupName,
                    cmbAlign1.Text,
                    cmbStyle.Text,
                    GeneralFunctions.fnInt32(txtFontSize.Text),
                    GeneralFunctions.fnInt32(txtWidth.Text),
                    GeneralFunctions.fnInt32(txtHeight.Text),
                    "Y",null,GelSL });
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {

                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                        dr1["TextAlign"] = cmbAlign1.Text;
                        dr1["TextStyle"] = cmbStyle.Text;
                        dr1["FontSize"] = txtFontSize.Text;
                        dr1["CtrlWidth"] = txtWidth.Text;
                        dr1["CtrlHeight"] = txtHeight.Text;
                        break;
                    }
                }

                
            }
        }


        private void UpdateDatatableForLabelBarcode()
        {
            bool bFindData = false;
            if (blEditData)
            {
                bFindData = true;
            }
            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    currGroupName,
                    cmbAlignLB.Text,
                    cmbStyle.Text,
                    GeneralFunctions.fnInt32(txtFontSize.Text),
                    GeneralFunctions.fnInt32(txtWidthLB.Text),
                    GeneralFunctions.fnInt32(txtHeightLB.Text),
                    "Y",null,GelSL,"N","N","N","","","", GeneralFunctions.fnInt32(txtTopLocationLB.Text)  });
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {

                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                        dr1["TextAlign"] = cmbAlignLB.Text;
                        dr1["TextStyle"] = cmbStyle.Text;
                        dr1["FontSize"] = txtFontSize.Text;
                        dr1["CtrlWidth"] = txtWidthLB.Text;
                        dr1["CtrlHeight"] = txtHeightLB.Text;
                        dr1["CtrlPositionTop"] = txtTopLocationLB.Text;
                        break;
                    }
                }


            }
        }


        private void UpdateDatatableForImage()
        {
            bool bFindData = false;
            if (blEditData)
            {
                bFindData = true;
            }
            if (!bFindData)
            {

                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }


                currSubSL = GetSubSLForImage();

                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    currGroupName,
                    cmbAlign2.Text,
                    cmbStyle.Text,
                    GeneralFunctions.fnInt32(txtFontSize.Text),
                    GeneralFunctions.fnInt32(txtWidth2.Text),
                    GeneralFunctions.fnInt32(txtHeight2.Text),
                    "Y",GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source),GelSL });
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {

                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                        dr1["TextAlign"] = cmbAlign2.Text;
                        dr1["TextStyle"] = cmbStyle.Text;
                        dr1["FontSize"] = txtFontSize.Text;
                        dr1["CtrlWidth"] = txtWidth2.Text;
                        dr1["CtrlHeight"] = txtHeight2.Text;
                        dr1["CustomImage"] = GeneralFunctions.ConvertBitmapSourceToByteArray(pictPhoto.Source);
                        break;
                    }
                }

               
            }
        }


        private void UpdateDatatableForOrderHeader()
        {
            bool bFindData = false;

            //dtblTemplate.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            //dtblTemplate.DefaultView.ApplyDefaultSort = true;

            //foreach (DataRowView dr in dtblTemplate.DefaultView)

            if (blEditData)
            {
                bFindData = true;
            }



            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

                
                string data = "";
                if (chkHeader1.IsChecked == true)
                {
                    data = txtHeaderCaption1.Text.Trim();
                }
                if (chkHeader2.IsChecked == true)
                {
                    data = data == "" ? txtHeaderCaption2.Text.Trim() : data + "     " + txtHeaderCaption2.Text.Trim();
                }
                if (chkHeader3.IsChecked == true)
                {
                    data = data == "" ? txtHeaderCaption3.Text.Trim() : data + "     " + txtHeaderCaption3.Text.Trim();
                }
                
                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    data,
                    cmbAlign.Text,
                    cmbStyle3.Text,
                    GeneralFunctions.fnInt32(txtFontSize3.Text),0,0, "Y",null,GelSL,
                    chkHeader1.IsChecked == true ? "Y" : "N",
                    chkHeader2.IsChecked == true ? "Y" : "N",
                    chkHeader3.IsChecked == true ? "Y" : "N",
                    txtHeaderCaption1.Text.Trim(),
                    txtHeaderCaption2.Text.Trim(),
                    txtHeaderCaption3.Text.Trim()
                    });
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {

                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                      
                        dr1["TextStyle"] = cmbStyle3.Text;
                        dr1["FontSize"] = txtFontSize3.Text;
                        dr1["GroupData"] = txtText.Text;
                        dr1["ShowHeader1"] = chkHeader1.IsChecked == true ? "Y" : "N";
                        dr1["ShowHeader2"] = chkHeader2.IsChecked == true ? "Y" : "N";
                        dr1["ShowHeader3"] = chkHeader3.IsChecked == true ? "Y" : "N";
                        dr1["Header1Caption"] = txtHeaderCaption1.Text;
                        dr1["Header2Caption"] = txtHeaderCaption2.Text;
                        dr1["Header3Caption"] = txtHeaderCaption3.Text;
                        break;
                    }
                }
            }
        }

        private void UpdateDatatableForSeparator()
        {
            bool bFindData = false;

           
            if (blEditData)
            {
                bFindData = true;
            }



            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

              
                string data = "---- S E P A R A T O R ----";
                

                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    data,
                    "center",
                    "normal",
                    GeneralFunctions.fnInt32(8),0,0, "Y",null,GelSL });
            }
            else
            {
                
            }
        }

        private void UpdateDatatable()
        {
            bool bFindData = false;

            //dtblTemplate.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            //dtblTemplate.DefaultView.ApplyDefaultSort = true;

            //foreach (DataRowView dr in dtblTemplate.DefaultView)

            if (blEditData)
            {
                bFindData = true;
            }
            

            
            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }
                            
                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

                if (currGroupName == "Text")
                {
                    currSubSL = GetSubSLForText();
                }
                string data = "";
                if (currGroupName == "Receipt Number") data = "Receipt Number: " + "6547";
                else if (currGroupName == "Subtotal Amount") data = "Sub Total: " + SystemVariables.CurrencySymbol +  "10.25";
                else if (currGroupName == "Tax Amount") data = "Tax: " + SystemVariables.CurrencySymbol + "0.00";
                else if (currGroupName == "Discount Amount") data = "Discount: " + SystemVariables.CurrencySymbol + "0.00";
                else if (currGroupName == "Total Amount") data = "Total: " + SystemVariables.CurrencySymbol + "10.25";
                else if (currGroupName == "Tender Amount") data = "Tender"+"\r\n" + "Cash: " + SystemVariables.CurrencySymbol + "12.00";
                else if (currGroupName == "Change Due Amount") data = "Change: (" + SystemVariables.CurrencySymbol + "12.00)";
                else if (currGroupName == "Item/Price Header") data = "Unit Price       Net Wt/Ct       Total Price";
                else if (currGroupName == "Item/Price Line") data = "Item 1 " +"\r\n" + "10.00       1       10.00";
                else if (currGroupName == "Details") data = "Item 1 " + "\r\n" + "10.00       1       10.00";
                else data = txtText.Text.Trim();

                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    data,
                    cmbAlign.Text,
                    cmbStyle.Text,
                    GeneralFunctions.fnInt32(txtFontSize.Text),0,0, "Y",null,GelSL });
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {
                  
                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                        dr1["TextAlign"] = cmbAlign.Text;
                        dr1["TextStyle"] = cmbStyle.Text;
                        dr1["FontSize"] = txtFontSize.Text;
                        dr1["GroupData"] = txtText.Text;
                        break;
                    }
                }
            }
        }

        private void UpdateDatatableForLabelText()
        {
            bool bFindData = false;

            if (blEditData)
            {
                bFindData = true;
            }



            if (!bFindData)
            {
                int GelSL = 0;

                if (blAddSpecificPosition)
                {
                    if (intSpecificSL == 0)
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                        }
                        GelSL = 1;
                    }
                    else
                    {
                        foreach (DataRow dr1 in dtblTemplate.Rows)
                        {
                            if (GeneralFunctions.fnInt32(dr1["SL"].ToString()) < intSpecificSL) continue;
                            else
                            {
                                dr1["SL"] = GeneralFunctions.fnInt32(dr1["SL"].ToString()) + 1;
                            }

                        }
                        GelSL = intSpecificSL;
                    }

                }
                else
                {
                    GelSL = dtblTemplate.Rows.Count + 1;
                }

                if (currGroupName == "Text")
                {
                    currSubSL = GetSubSLForText();
                }
                string data = "";
                if (currGroupName == "SKU") data = "1234567890";
                else if (currGroupName == "Item Name") data = "Item 1";
                else data = SystemVariables.CurrencySymbol + "10.00";
                
                dtblTemplate.Rows.Add(new object[] { currID.ToString(),
                    currGroupName,
                    currSL,
                    currSubSL,
                    data,
                    cmbAlignL.Text,
                    cmbStyleL.Text,
                    GeneralFunctions.fnInt32(txtFontSizeL.Text),0,0, "Y",null,GelSL,"N","N","N","","","", GeneralFunctions.fnInt32(txtTopLocationL.Text)});
            }
            else
            {
                foreach (DataRow dr1 in dtblTemplate.Rows)
                {

                    if (currOrderSL == GeneralFunctions.fnInt32(dr1["SL"].ToString()))
                    {
                        dr1["TextAlign"] = cmbAlignL.Text;
                        dr1["TextStyle"] = cmbStyleL.Text;
                        dr1["FontSize"] = txtFontSizeL.Text;
                        dr1["CtrlPositionTop"] = txtTopLocationL.Text;
                        break;
                    }
                }
            }
        }

        private int GetSubSLForImage()
        {
            int maxSL = 0;

            //dtblTemplate.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            //dtblTemplate.DefaultView.ApplyDefaultSort = true;

            foreach (DataRowView dr in dtblTemplate.DefaultView)
            {
                if (dr["GroupName"].ToString() != "Image") continue;

                maxSL = GeneralFunctions.fnInt32(dr["GroupSubSL"].ToString());

            }
            return maxSL + 1;
        }

        private int GetSubSLForText()
        {
            int maxSL = 0;
            dtblTemplate.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            dtblTemplate.DefaultView.ApplyDefaultSort = true;

            foreach (DataRowView dr in dtblTemplate.DefaultView)
            {
                if (dr["GroupName"].ToString() != "Text") continue;

                maxSL = GeneralFunctions.fnInt32(dr["GroupSubSL"].ToString());

            }
            return maxSL + 1;
        }

        private void DeleteDatatable()
        {
            bool bFindData = false;

            //dtblTemplate.DefaultView.Sort = "GroupSL,GroupSubSL asc";
            //dtblTemplate.DefaultView.ApplyDefaultSort = true;

            foreach (DataRow dr in dtblTemplate.Rows)
            {
                //if (dr["Display"].ToString() == "N") continue;

                if (currOrderSL == GeneralFunctions.fnInt32(dr["SL"].ToString()))
                {
                    dr["Display"] = "N";
                    break;
                }
            }

            DataRow[] DRFilter = dtblTemplate.Select("Display = 'Y'");

            foreach (DataRow dr in DRFilter)
            {
                if (GeneralFunctions.fnInt32(dr["SL"].ToString()) <= currOrderSL) continue;

                dr["SL"] = GeneralFunctions.fnInt32(dr["SL"].ToString()) - 1;
                
            }

            DataTable dclone = dtblTemplate.Clone();
           
            foreach (DataRow dr in DRFilter)
            {
                dclone.ImportRow(dr);
            }

            dtblTemplate = dclone;
            dclone.Dispose();
        }

        private void CmbGroup_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void PopupEdit_Click(object sender, RoutedEventArgs e)
        {
            flyoutControl.IsOpen = false;
            boolLoadParam = false;

            grdHeader.Visibility = Visibility.Collapsed;

            blEditData = true;

            currOrderSL = popupuc.SL;
            currID = popupuc.ID;
            currSL = popupuc.GroupSL;
            currSubSL = popupuc.GroupSubSL;
            currGroupName = popupuc.GroupName;

            if (currGroupName == "Text")
            {
                txtText.Text = popupuc.GroupData;
            }
            else
            {
                txtText.Text = currGroupName;
            }

            
            cmbGroup.EditValue = popupuc.ID.ToString();

            if (txtName.Text == "1 Up Label")
            {
                if ((currGroupName == "Logo") || (currGroupName == "Barcode"))
                {
                    cmbAlignLB.Text = popupuc.TextAlign;
                    txtWidthLB.Text = popupuc.CtrlWidth.ToString();
                    txtHeightLB.Text = popupuc.CtrlHeight.ToString();
                    txtTopLocationLB.Text = popupuc.CtrlPositionTop.ToString();

                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Visible;
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    grdEditLabelText.Visibility = Visibility.Visible;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                    cmbAlignL.Text = popupuc.TextAlign;
                    cmbStyleL.Text = popupuc.TextStyle;
                    txtFontSizeL.Text = popupuc.FontSize.ToString();
                    txtTopLocationL.Text = popupuc.CtrlPositionTop.ToString();
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if ((currGroupName == "Logo") || (currGroupName == "Barcode"))
                {
                    cmbAlign1.Text = popupuc.TextAlign;
                    txtWidth.Text = popupuc.CtrlWidth.ToString();
                    txtHeight.Text = popupuc.CtrlHeight.ToString();

                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    grdEditLogoBarcode.Visibility = Visibility.Visible;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
                else if (currGroupName == "Image")
                {
                    byte[] content = popupuc.byt;
                    try
                    {
                        // assign byte array data into memory stream 
                        MemoryStream stream = new MemoryStream(content);

                        // set transparent bitmap with 32 X 32 size by memory stream data 
                        Bitmap b = new Bitmap(stream);
                        Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
                        output.MakeTransparent();

                        System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                        bi.BeginInit();
                        System.Drawing.Image tempImage = (System.Drawing.Image)output;
                        MemoryStream ms = new MemoryStream();
                        tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                        stream.Seek(0, SeekOrigin.Begin);

                        bi.StreamSource = stream;

                        bi.EndInit();

                        pictPhoto.Source = bi;

                    }
                    catch (Exception ex)
                    {
                        pictPhoto.Source = null;
                    }
                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;

                    cmbAlign2.Text = popupuc.TextAlign;
                    txtWidth2.Text = popupuc.CtrlWidth.ToString();
                    txtHeight2.Text = popupuc.CtrlHeight.ToString();
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Visible;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
                else if (currGroupName == "Item/Price Header")
                {

                    cmbStyle3.Text = popupuc.TextStyle;
                    txtFontSize3.Text = popupuc.FontSize.ToString();

                    if (popupuc.ShowHeader1 == "Y")
                    {
                        chkHeader1.IsChecked = true;
                        txtHeaderCaption1.Text = popupuc.Header1Caption;
                    }
                    else
                    {
                        chkHeader1.IsChecked = false;
                        txtHeaderCaption1.Text = "";
                    }

                    if (popupuc.ShowHeader2 == "Y")
                    {
                        chkHeader2.IsChecked = true;
                        txtHeaderCaption2.Text = popupuc.Header2Caption;
                    }
                    else
                    {
                        chkHeader2.IsChecked = false;
                        txtHeaderCaption2.Text = "";
                    }

                    if (popupuc.ShowHeader3 == "Y")
                    {
                        chkHeader3.IsChecked = true;
                        txtHeaderCaption3.Text = popupuc.Header3Caption;
                    }
                    else
                    {
                        chkHeader3.IsChecked = false;
                        txtHeaderCaption3.Text = "";
                    }

                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Visible;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
                else if (currGroupName == "Separator")
                {
                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                    grdEditSeparator.Visibility = Visibility.Visible;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Collapsed;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    grdEditLabelText.Visibility = Visibility.Collapsed;
                    grdEditLabelBarcode.Visibility = Visibility.Collapsed;
                    cmbAlign.Text = popupuc.TextAlign;
                    cmbStyle.Text = popupuc.TextStyle;
                    txtFontSize.Text = popupuc.FontSize.ToString();
                    grdEditSeparator.Visibility = Visibility.Collapsed;
                    grdEditOrderHeader.Visibility = Visibility.Collapsed;
                    grdEditGeneral.Visibility = Visibility.Visible;
                    grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                    grdEditImage.Visibility = Visibility.Collapsed;
                    svTemplate.Visibility = Visibility.Collapsed;
                }
            }

            boolLoadParam = true;
        }

        private void PopupDelete_Click(object sender, RoutedEventArgs e)
        {
            flyoutControl.IsOpen = false;
            boolLoadParam = false;

            currOrderSL = popupuc.SL;
            currID = popupuc.ID;
            currSL = popupuc.GroupSL;
            currSubSL = popupuc.GroupSubSL;
            currGroupName = popupuc.GroupName;


            DeleteDatatable();

            LoadTemplate();

            currOrderSL = 0;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            boolLoadParam = true;
            boolControlChanged = true;
        }

        private void BtnCancel1_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditGeneral.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnCancel2_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLogoBarcode.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnSubmit2_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatatableForLogoBarcode();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLogoBarcode.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void BtnCancel3_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditImage.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnSubmit3_Click(object sender, RoutedEventArgs e)
        {
            if (pictPhoto.Source == null) return;
            UpdateDatatableForImage();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditImage.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
           
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                
            }
        }

        private void CmbGroup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (svTemplate.Visibility == Visibility.Collapsed)
            {
                grdEditGeneral.Visibility = Visibility.Collapsed;
                grdEditImage.Visibility = Visibility.Collapsed;
                grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                grdHeader.Visibility = Visibility.Visible;
                svTemplate.Visibility = Visibility.Visible;
            }
        }

        private void CmbGroup_EditValueChanged_1(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            /*if (svTemplate.Visibility == Visibility.Collapsed)
            {
                grdEditGeneral.Visibility = Visibility.Collapsed;
                grdEditImage.Visibility = Visibility.Collapsed;
                grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                svTemplate.Visibility = Visibility.Visible;
            }*/
        }

        private void TxtName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DevExpress.Xpf.Editors.ComboBoxEdit).IsEnabled == false) return;
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void ChkEditTemplate_Checked(object sender, RoutedEventArgs e)
        {
            if (chkEditTemplate.IsChecked == true)
            {
                /*printCtrl.Visibility = Visibility.Collapsed;
                grdHeader.Visibility = Visibility.Visible;
                grdEditGeneral.Visibility = Visibility.Collapsed;
                grdEditImage.Visibility = Visibility.Collapsed;
                grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                svTemplate.Visibility = Visibility.Visible;*/

                tbctrl.Style = this.FindResource("TemplateOuterPOSTabControlStyleNormal") as Style;
                tpEdit.Visibility = Visibility.Visible;
                tbctrl.SelectedIndex = 0;

                if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
                {
                   

                    lbGeneral.Visibility = cmbGroup.Visibility = Visibility.Hidden;
                    btnAddGroup.Content = "DESIGNER";
                    pnlTemplate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    lbGeneral.Visibility = cmbGroup.Visibility = Visibility.Visible;
                    btnAddGroup.Content = "ADD";
                    pnlTemplate.Visibility = Visibility.Visible;
                }
            }
            else
            {
                /*printCtrl.Visibility = Visibility.Visible;
                grdHeader.Visibility = Visibility.Collapsed;
                grdEditGeneral.Visibility = Visibility.Collapsed;
                grdEditImage.Visibility = Visibility.Collapsed;
                grdEditLogoBarcode.Visibility = Visibility.Collapsed;
                svTemplate.Visibility = Visibility.Collapsed;
                */

                tbctrl.Style = this.FindResource("TemplateOuterPOSTabControlStyle") as Style;
                tpEdit.Visibility = Visibility.Collapsed;

                if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
                {
                    tbctrl.SelectedIndex = 2;
                    GetPrintLabel();
                }
                else
                {
                    tbctrl.SelectedIndex = 1;
                    GetPrintStream();
                    CreateFlowControl(dclonePrint);
                }
            }
        }

        private void GetPrintLabel()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("COMPANY", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DESC", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PRICE", System.Type.GetType("System.String"));


            
            dtbl.Rows.Add(new object[] { Settings.Company, "1234567890", "Item 1", SystemVariables.CurrencySymbol + " " + 10.ToString("f") });

            if ((txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
            {
                dtbl.Rows.Add(new object[] { Settings.Company, "1234567890", "Item 1", SystemVariables.CurrencySymbol + " " + 10.ToString("f") });
                dtbl.Rows.Add(new object[] { Settings.Company, "1234567890", "Item 1", SystemVariables.CurrencySymbol + " " + 10.ToString("f") });
                dtbl.Rows.Add(new object[] { Settings.Company, "1234567890", "Item 1", SystemVariables.CurrencySymbol + " " + 10.ToString("f") });
            }

            XtraReport fReport = new XtraReport();

            if (txtName.Text == "Label - 1 Up")
            {
                if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("OneUp", intID == 0 ? "new" : intID.ToString())))
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("OneUp", intID == 0 ? "new" : intID.ToString()), true);
                }
                else
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("OneUp"), true);
                }
            }

            if (txtName.Text == "Label - 2 Up")
            {
                if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("TwoUp", intID == 0 ? "new" : intID.ToString())))
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("TwoUp", intID == 0 ? "new" : intID.ToString()), true);
                }
                else
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("TwoUp"), true);
                }

                (fReport as OfflineRetailV2.Report.Product.TwoUp).skpno = 0;
                (fReport as OfflineRetailV2.Report.Product.TwoUp).prtno = 3;
                (fReport as OfflineRetailV2.Report.Product.TwoUp).firstp = true;
            }

            if (txtName.Text == "Label - Butterfly")
            {
                if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", intID == 0 ? "new" : intID.ToString())))
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", intID == 0 ? "new" : intID.ToString()), true);
                }
                else
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Butterfly"), true);
                }

               
            }

            if (txtName.Text == "Label - Avery 5160 / NEBS 12650")
            {
                if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", intID == 0 ? "new" : intID.ToString())))
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery5160", intID == 0 ? "new" : intID.ToString()), true);
                }
                else
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Avery5160"), true);
                }

                (fReport as OfflineRetailV2.Report.Product.Avery5160).skpno = 0;
                (fReport as OfflineRetailV2.Report.Product.Avery5160).prtno = 3;
                (fReport as OfflineRetailV2.Report.Product.Avery5160).firstp = true;
            }

            if (txtName.Text == "Label - Avery 8195")
            {
                if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", intID == 0 ? "new" : intID.ToString())))
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery8195", intID == 0 ? "new" : intID.ToString()), true);
                }
                else
                {
                    fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Avery8195"), true);
                }

                (fReport as OfflineRetailV2.Report.Product.Avery8195).skpno = 0;
                (fReport as OfflineRetailV2.Report.Product.Avery8195).prtno = 3;
                (fReport as OfflineRetailV2.Report.Product.Avery8195).firstp = true;
            }


            DataSet ds = new DataSet();
            ds.Tables.Add(dtbl);
            fReport.Report.DataSource = ds;
            fReport.DataSource = ds;

            fReport.CreateDocument();
            fReport.PrintingSystem.ShowMarginsWarning = false;
            fReport.PrintingSystem.ShowPrintStatusDialog = false;
            printlabel.DocumentSource = fReport;
            printlabel.ShowNavigationPane = false;


            /*
            dtblTemplate.DefaultView.Sort = "SL asc";
            dtblTemplate.DefaultView.ApplyDefaultSort = true;

            DataTable dtblLabel = new DataTable();
            dtblLabel.Columns.Add("GroupName", System.Type.GetType("System.String"));
            dtblLabel.Columns.Add("TextAlign", System.Type.GetType("System.String"));
            dtblLabel.Columns.Add("TextStyle", System.Type.GetType("System.String"));
            dtblLabel.Columns.Add("FontSize", System.Type.GetType("System.Int32"));
            dtblLabel.Columns.Add("CtrlWidth", System.Type.GetType("System.Int32"));
            dtblLabel.Columns.Add("CtrlHeight", System.Type.GetType("System.Int32"));
            dtblLabel.Columns.Add("CtrlPositionTop", System.Type.GetType("System.Int32"));
            dtblLabel.Columns.Add("Display", System.Type.GetType("System.String"));

            foreach (DataRowView dr in dtblTemplate.DefaultView)
            {
                dtblLabel.Rows.Add(new object[] { dr["GroupName"].ToString(),
                    dr["TextAlign"].ToString(),
                    dr["TextStyle"].ToString(),
                    dr["FontSize"].ToString(),
                    dr["CtrlWidth"].ToString(),
                    dr["CtrlHeight"].ToString(),
                    dr["CtrlPositionTop"].ToString(),
                    dr["Display"].ToString()});
            }

            bool bsku = false;
            bool bname = false;
            bool bprice = false;
            bool bbar = false;

            if (dtblLabel.Rows.Count > 0)
            {
                OfflineRetailV2.Report.Product.OneUpCustom rep = new OfflineRetailV2.Report.Product.OneUpCustom();

               
                XtraReport fReport = new XtraReport();

                rep.PageSize = new System.Drawing.Size(GeneralFunctions.fnInt32(txtTemplateW.Text), GeneralFunctions.fnInt32(txtTemplateH.Text));
                rep.Detail.HeightF = GeneralFunctions.fnInt32(txtTemplateH.Text) + 4;
               

                foreach (DataRow dr in dtblLabel.Rows)
                {
                    if (dr["GroupName"].ToString() == "SKU")
                    {
                        bsku = true;
                        string align = dr["TextAlign"].ToString();
                        int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                        string fs = dr["TextStyle"].ToString();

                        rep.lbSKU.WidthF = GeneralFunctions.fnInt32(txtTemplateW.Text) - 10;
                        rep.lbSKU.HeightF = fz + 6;
                        if (dr["TextAlign"].ToString() == "left") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                        if (dr["TextAlign"].ToString() == "center") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                        if (dr["TextAlign"].ToString() == "right") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                        rep.lbSKU.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                        if (fs == "normal") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                        if (fs == "bold") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                        if (fs == "italic") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);

                        
                        
                    }

                    if (dr["GroupName"].ToString() == "Item Name")
                    {
                        bname = true;

                        string align = dr["TextAlign"].ToString();
                        int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                        string fs = dr["TextStyle"].ToString();

                        rep.lbProduct.WidthF = GeneralFunctions.fnInt32(txtTemplateW.Text) - 10;
                        rep.lbProduct.HeightF = fz + 6;
                        if (dr["TextAlign"].ToString() == "left") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                        if (dr["TextAlign"].ToString() == "center") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                        if (dr["TextAlign"].ToString() == "right") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                        rep.lbProduct.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                        if (fs == "normal") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                        if (fs == "bold") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                        if (fs == "italic") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);
                    }

                    if (dr["GroupName"].ToString() == "Item Price")
                    {
                        bprice = true;

                        string align = dr["TextAlign"].ToString();
                        int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                        string fs = dr["TextStyle"].ToString();

                        rep.lbPrice.WidthF = GeneralFunctions.fnInt32(txtTemplateW.Text) - 10;
                        rep.lbPrice.HeightF = fz + 6;
                        if (dr["TextAlign"].ToString() == "left") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                        if (dr["TextAlign"].ToString() == "center") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                        if (dr["TextAlign"].ToString() == "right") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                        rep.lbPrice.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                        if (fs == "normal") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                        if (fs == "bold") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                        if (fs == "italic") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);

                    }

                    if (dr["GroupName"].ToString() == "Barcode")
                    {
                        bbar = true;

                        string align = dr["TextAlign"].ToString();
                        int fz = GeneralFunctions.fnInt32(dr["CtrlHeight"].ToString());
                        string fs = dr["TextStyle"].ToString();

                        rep.lbBarCode.WidthF = GeneralFunctions.fnInt32(txtTemplateW.Text) - 10;
                        rep.lbBarCode.HeightF = fz;
                        if (dr["TextAlign"].ToString() == "left") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                        if (dr["TextAlign"].ToString() == "center") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                        if (dr["TextAlign"].ToString() == "right") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                        rep.lbBarCode.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());
                    }
                }

                if (!bsku) rep.lbSKU.Visible = false;
                if (!bname) rep.lbProduct.Visible = false;
                if (!bprice) rep.lbPrice.Visible = false;
                if (!bbar) rep.lbBarCode.Visible = false;

                fReport = rep;
                DataSet ds = new DataSet();
                ds.Tables.Add(dtbl);
                fReport.Report.DataSource = ds;
                fReport.DataSource = ds;

                fReport.CreateDocument();
                fReport.PrintingSystem.ShowMarginsWarning = false;
                fReport.PrintingSystem.ShowPrintStatusDialog = false;


                printlabel.DocumentSource = fReport;

                printlabel.ShowNavigationPane = false;

                
            }
            */


        }

        private void GetPrintStream()
        {
            dtblstrm = new DataTable();

            dtblstrm.Columns.Add("Npos", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("Cpos", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("Fpos", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("param", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("paramnew", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("paramvalue", System.Type.GetType("System.String"));
            dtblstrm.Columns.Add("SL", System.Type.GetType("System.String"));

            dclonePrint = dtblTemplate.Clone();

            dtblTemplate.DefaultView.Sort = "SL asc";
            dtblTemplate.DefaultView.ApplyDefaultSort = true;


            //foreach (DataRowView dr in dtblTemplate.DefaultView)


            foreach (DataRowView dr in dtblTemplate.DefaultView)
            {
                dclonePrint.ImportRow((DataRow)dr.Row);
            }

            //dclonePrint = dtblTemplate.Clone();
            dclonePrint.Columns.Add("Value");

            foreach (DataRow dr in dclonePrint.Rows)
            {
                if ((dr["GroupName"].ToString() == "Logo") || (dr["GroupName"].ToString() == "Item/Price Header")
                    || (dr["GroupName"].ToString() == "Item/Price Line") || (dr["GroupName"].ToString() == "Image")
                    || (dr["GroupName"].ToString() == "Barcode") || (dr["GroupName"].ToString() == "Details"))
                {

                }
                else
                {
                    bool findDemoData = false;
                    string val = dr["GroupData"].ToString();
                    foreach (DataRow dr1 in dtblDummyTemplateData.Rows)
                    {
                        if (dr["GroupName"].ToString() != dr1["GroupName"].ToString()) continue;
                        val = dr1["GroupData"].ToString();
                        findDemoData = true;
                        break;
                    }
                    dr["Value"] = val;
                }
            }


            foreach(DataRow dr in dclonePrint.Rows)
            {
                if (dr["GroupName"].ToString() == "Separator")
                {

                    dtblstrm.Rows.Add(new object[] { "xx", "xx", "xx", "", "Y", "" });
                }
                else if (dr["GroupName"].ToString() == "Logo")
                {
                    string pval = FindTemplateParameterValueInDatatable(dr["GroupName"].ToString(), dr["SL"].ToString(), dclonePrint);
                    if (pval != "")
                    {
                        dtblstrm.Rows.Add(new object[] { "", "", "", "logo", "Y", pval });
                    }
                }
                else if (dr["GroupName"].ToString() == "Image")
                {
                    string pval = FindTemplateParameterValueInDatatableForImage(dr["GroupName"].ToString(), dr["GroupSubSL"].ToString(), dclonePrint);
                    dtblstrm.Rows.Add(new object[] { dr["GroupSubSL"].ToString(), "", "", "Image", "Y", pval });
                }
                else if (dr["GroupName"].ToString() == "Text")
                {
                    string pval = FindTemplateParameterValueInDatatableForText(dr["GroupName"].ToString(), dr["GroupSubSL"].ToString(), dclonePrint);
                    if (pval != "")
                    {
                        dtblstrm.Rows.Add(new object[] { dr["Value"].ToString(), "", "", "", "Y", pval });
                    }
                }
                else if (dr["GroupName"].ToString() == "Barcode")
                {
                    string pval = FindTemplateParameterValueInDatatable(dr["GroupName"].ToString(), dr["SL"].ToString(), dclonePrint);
                    dtblstrm.Rows.Add(new object[] { "barcode", "barcode", "barcode", "", "Y", pval });
                }
                else if (dr["GroupName"].ToString() == "Item/Price Header")
                {
                    string pval = FindTemplateParameterValueInDatatableOrderHeader(dr["GroupName"].ToString(), dr["SL"].ToString(), dclonePrint);
                    if (pval != "")
                    {
                        dtblstrm.Rows.Add(new object[] { dr["Value"].ToString(), "", "", "Header", "Y", pval });
                    }
                }
                else if ((dr["GroupName"].ToString() == "Item/Price Line") || (dr["GroupName"].ToString() == "Details"))
                {
                    string pval = FindTemplateParameterValueInDatatableOrderDetail(dr["GroupName"].ToString(), dr["SL"].ToString(), dclonePrint);
                    if (pval != "")
                    {
                        if (txtName.Text == "Gift Receipt")
                        {
                            dtblstrm.Rows.Add(new object[] { "GC# 12345678", "", "", "Item", "Y", pval });
                        }
                        else
                        {
                            dtblstrm.Rows.Add(new object[] { "Item 1", "", "", "Item", "Y", pval });
                        }

                        string pval1 = FindTemplateParameterValueInDatatableOrderHeader1("Item/Price Header", dclonePrint);
                        if (pval1 == "")
                        {
                            pval = pval + "|Y|Y|Y";
                        }
                        else
                        {
                            pval = pval + "|" + pval1;
                        }
                        dtblstrm.Rows.Add(new object[] { "10.00", "1", "10.00", "Detail", "Y", pval });
                    }
                }
                else
                {
                    string pval = FindTemplateParameterValueInDatatable(dr["GroupName"].ToString(), dr["SL"].ToString(), dclonePrint);
                    if (pval != "")
                    {
                        dtblstrm.Rows.Add(new object[] { dr["Value"].ToString(), "", "", "", "Y", pval });
                    }
                }
            }


            int i = 1;
            foreach (DataRow dr in dtblstrm.Rows)
            {
                dr["SL"] = i.ToString();
                i++;
            }
        }

        private void CreateFlowControl(DataTable dtblparam)
        {
            int receiptWidth = 0;

            if (cmbTemplateSize.Text == "80 mm")
            {
                receiptWidth = 270 - SystemVariables.PageAdjustmentForPrint; // 270


            }
            if (cmbTemplateSize.Text == "58 mm")
            {
                receiptWidth = 196 - SystemVariables.PageAdjustmentForPrint; // 206

            }

            //if (Settings.GeneralReceiptPrint == "Y") receiptWidth = 450; else receiptWidth = 245 + SystemVariables.PageAdjustmentForPrint;

            float yPos = 0;
            int count = 0;
            float leftMargin = 0;
            float topMargin = 0;

            FlowDocument FDoc = new FlowDocument();
            FDoc.Background = System.Windows.Media.Brushes.White; 
                                                                 
            FDoc.LineHeight = 1;
            FDoc.PageWidth = GeneralFunctions.fnDouble(receiptWidth);



            System.Windows.Media.FontFamily fontFamily = new System.Windows.Media.FontFamily("Arial");
            double fontDpiSize = 9;
            double fontHeight = Math.Ceiling(fontDpiSize * fontFamily.LineSpacing);
            int H = GeneralFunctions.fnInt32(fontHeight) + 1;

            int counter = 0;

            foreach (DataRow dr in dtblstrm.Rows)
            {
                ++counter;
                if (counter > 112)
                {

                }
                yPos = topMargin + (count * H);

                if ((dr["Npos"].ToString() == "xxxx") && (dr["Cpos"].ToString() == "xxxx") && (dr["Fpos"].ToString() == "xxxx"))
                {
                    Line pLine = new Line();
                    pLine.Stretch = Stretch.Fill;
                    pLine.Stroke = System.Windows.Media.Brushes.Black;
                    pLine.X1 = GeneralFunctions.fnDouble(leftMargin);
                    pLine.Y1 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));
                    pLine.X2 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(475 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(240 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y2 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));

                    Paragraph pgph = new Paragraph();
                    pgph.Inlines.Add(pLine);

                    FDoc.Blocks.Add(pgph);
                    //FDocPrint.Blocks.Add(pgph);
                }
                else if ((dr["Npos"].ToString() == "xxx") && (dr["Cpos"].ToString() == "xxx") && (dr["Fpos"].ToString() == "xxx"))
                {
                    Line pLine = new Line();
                    pLine.Stretch = Stretch.Fill;
                    pLine.Stroke = System.Windows.Media.Brushes.Black;
                    pLine.X1 = GeneralFunctions.fnDouble(leftMargin);
                    pLine.Y1 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));
                    pLine.X2 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(475 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(240 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y2 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));

                    DoubleCollection dashes = new DoubleCollection();
                    dashes.Add(2);
                    dashes.Add(2);
                    pLine.StrokeDashArray = dashes;
                    pLine.StrokeDashCap = PenLineCap.Round;

                    Paragraph pgph = new Paragraph();
                    pgph.Inlines.Add(pLine);

                    FDoc.Blocks.Add(pgph);
                    //FDocPrint.Blocks.Add(pgph);
                }
                else if ((dr["Npos"].ToString() == "xx") && (dr["Cpos"].ToString() == "xx") && (dr["Fpos"].ToString() == "xx"))
                {
                    Line pLine = new Line();
                    pLine.Stretch = Stretch.Fill;
                    pLine.Stroke = System.Windows.Media.Brushes.Black;
                    pLine.X1 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(265 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(60 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y1 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));
                    pLine.X2 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(475 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(240 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y2 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));

                    DoubleCollection dashes = new DoubleCollection();
                    dashes.Add(2);
                    dashes.Add(2);
                    pLine.StrokeDashArray = dashes;
                    pLine.StrokeDashCap = PenLineCap.Round;

                    Paragraph pgph = new Paragraph();
                    pgph.Inlines.Add(pLine);

                    FDoc.Blocks.Add(pgph);
                    //FDocPrint.Blocks.Add(pgph);
                }
                else if ((dr["Npos"].ToString() == "x") && (dr["Cpos"].ToString() == "x") && (dr["Fpos"].ToString() == "x"))
                {
                    Line pLine = new Line();
                    pLine.Stretch = Stretch.Fill;
                    pLine.Stroke = System.Windows.Media.Brushes.Black;
                    pLine.X1 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(350 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(120 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y1 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));
                    pLine.X2 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(475 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(240 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y2 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));

                    DoubleCollection dashes = new DoubleCollection();
                    dashes.Add(2);
                    dashes.Add(2);
                    pLine.StrokeDashArray = dashes;
                    pLine.StrokeDashCap = PenLineCap.Round;

                    Paragraph pgph = new Paragraph();
                    pgph.Inlines.Add(pLine);

                    FDoc.Blocks.Add(pgph);
                    //FDocPrint.Blocks.Add(pgph);
                }
                else if ((dr["Npos"].ToString() == "sig") && (dr["Cpos"].ToString() == "sig") && (dr["Fpos"].ToString() == "sig"))
                {
                    Line pLine = new Line();
                    pLine.Stretch = Stretch.Fill;
                    pLine.Stroke = System.Windows.Media.Brushes.Black;
                    pLine.X1 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(20 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(120 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y1 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));
                    pLine.X2 = Settings.GeneralReceiptPrint == "Y" ? GeneralFunctions.fnDouble(-70 + GeneralFunctions.fnInt32(leftMargin)) : GeneralFunctions.fnDouble(240 + SystemVariables.PageAdjustmentForPrint + GeneralFunctions.fnInt32(leftMargin));
                    pLine.Y2 = GeneralFunctions.fnDouble(GeneralFunctions.fnInt32(yPos));

                    DoubleCollection dashes = new DoubleCollection();
                    dashes.Add(2);
                    dashes.Add(2);
                    pLine.StrokeDashArray = dashes;
                    pLine.StrokeDashCap = PenLineCap.Round;

                    Paragraph pgph = new Paragraph();
                    pgph.Inlines.Add(pLine);

                    FDoc.Blocks.Add(pgph);
                    //FDocPrint.Blocks.Add(pgph);
                }
                else if ((dr["Npos"].ToString() == "barcode") && (dr["Cpos"].ToString() == "barcode") && (dr["Fpos"].ToString() == "barcode"))
                {
                    string palign = "";
                    string pstyle = "";
                    int pfont = 0;
                    int pcwidth = 0;
                    int pcheight = 0;
                    GetSeparateValue(dr["paramvalue"].ToString(), ref palign, ref pstyle, ref pfont, ref pcwidth, ref pcheight);

                    DevExpress.Xpf.Editors.BarCodeEdit barcd_run = new DevExpress.Xpf.Editors.BarCodeEdit();
                    barcd_run.Width = 180;
                    barcd_run.Height = 48;
                    barcd_run.Module = 2;
                    barcd_run.ShowText = false;
                    DevExpress.Xpf.Editors.Code128StyleSettings code128 = new DevExpress.Xpf.Editors.Code128StyleSettings();
                    barcd_run.StyleSettings = code128;
                    barcd_run.EditValue = "1234";



                    BlockUIContainer bc = new BlockUIContainer();
                  

                    StackPanel sp = new StackPanel();
                    sp.Width = pcwidth;
                    sp.Height = pcheight;
                    sp.Orientation = Orientation.Horizontal;
                    sp.HorizontalAlignment = GetRowAlignment(palign);
                    sp.Background = new SolidColorBrush(Colors.Transparent);

                    sp.Children.Add(barcd_run);
                    DevExpress.Xpf.Editors.BarCodeEdit br = new DevExpress.Xpf.Editors.BarCodeEdit();


                    bc.Child = sp;
                    FDoc.Blocks.Add(bc);
                  
                    
                }
                else
                {
                    if ((dr["param"].ToString() == "Image") && (dr["paramnew"].ToString() == "Y"))
                    {
                        string palign = "";
                        string pstyle = "";
                        int pfont = 0;
                        int pcwidth = 0;
                        int pcheight = 0;
                        GetSeparateValue(dr["paramvalue"].ToString(), ref palign, ref pstyle, ref pfont, ref pcwidth, ref pcheight);

                        System.Windows.Controls.Image moreImage = new System.Windows.Controls.Image();
                        SetMoreImage(moreImage, GetMoreImage("Image", dr["Npos"].ToString(), dtblparam));


                        moreImage.Width = (float)pcwidth;
                        moreImage.Height = (float)pcheight;
                        moreImage.Stretch = Stretch.Uniform;
                        moreImage.Margin = new Thickness(0, 0, 0, 0);



                        Grid grid = new Grid();
                        grid.Width = (float)pcwidth;
                        grid.Height = (float)pcheight;
                        grid.Children.Add(moreImage);

                        BlockUIContainer bc = new BlockUIContainer();
                        

                        StackPanel sp = new StackPanel();
                        sp.Width = (float)pcwidth;
                        sp.Height = (float)pcheight;
                        sp.Orientation = Orientation.Horizontal;

                        sp.HorizontalAlignment = GetRowAlignment(palign);
                        sp.Background = new SolidColorBrush(Colors.Transparent);

                        sp.Children.Add(grid);

                        bc.Child = sp;
                        FDoc.Blocks.Add(bc);
                    }
                    else if ((dr["param"].ToString() == "") && (dr["paramnew"].ToString() == "Y"))
                    {

                        string palign = "";
                        string pstyle = "";
                        int pfont = 0;
                        int pcwidth = 0;
                        int pcheight = 0;
                        GetSeparateValue(dr["paramvalue"].ToString(), ref palign, ref pstyle, ref pfont, ref pcwidth, ref pcheight);

                        Table tab = new Table();
                        tab.CellSpacing = 0;
                        FDoc.Blocks.Add(tab);
                        TableRowGroup trgrp = new TableRowGroup();
                        tab.RowGroups.Add(trgrp);
                        TableRow tr = new TableRow();



                        TableCell tc1 = new TableCell();
                        Paragraph para1 = new Paragraph();
                        para1.TextAlignment = GetTextAlignment(palign);
                        para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                        para1.FontSize = GetFontSize(pfont);
                        SetTextStyle(para1, pstyle);
                        para1.Foreground = System.Windows.Media.Brushes.Black;
                        para1.Inlines.Add(dr["Npos"].ToString());
                        tc1.Blocks.Add(para1);


                        tr.Cells.Add(tc1);
                        trgrp.Rows.Add(tr);

                    }
                    else if ((dr["param"].ToString() == "") && (dr["paramnew"].ToString() == ""))
                    {
                        Table tab = new Table();
                        tab.CellSpacing = 0;
                        FDoc.Blocks.Add(tab);
                       
                        TableRowGroup trgrp = new TableRowGroup();
                        tab.RowGroups.Add(trgrp);
                        TableRow tr = new TableRow();


                        TableCell tc1 = new TableCell();
                        Paragraph para1 = new Paragraph();
                        para1.TextAlignment = TextAlignment.Left;
                        para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                        para1.FontSize = 11f;
                        para1.Foreground = System.Windows.Media.Brushes.Black;
                        para1.Inlines.Add(dr["Npos"].ToString());
                        tc1.Blocks.Add(para1);

                        TableCell tc2 = new TableCell();
                        Paragraph para2 = new Paragraph();
                        para2.TextAlignment = TextAlignment.Center;
                        para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                        para2.FontSize = 11f;
                        para2.Foreground = System.Windows.Media.Brushes.Black;
                        para2.Inlines.Add(dr["Cpos"].ToString());
                        tc2.ColumnSpan = 2;
                        tc2.Blocks.Add(para2);

                        TableCell tc3 = new TableCell();
                        Paragraph para3 = new Paragraph();
                        para3.TextAlignment = TextAlignment.Right;
                        para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                        para3.Margin = new Thickness(0, 0, 5, 0);

                        para3.FontSize = 11f;
                        para3.Foreground = System.Windows.Media.Brushes.Black;
                        para3.Inlines.Add(dr["Fpos"].ToString());
                        tc3.Blocks.Add(para3);


                        AdjustColumnsInRow(dr, tc1, tc2, tc3, tr);

                        trgrp.Rows.Add(tr);

                    }

                   
                    else
                    {
                        if (dr["param"].ToString() == "logo")
                        {
                            System.Windows.Controls.Image imglogoD = new System.Windows.Controls.Image();
                            GeneralFunctions.LoadPhotofromDB("Logo", 0, imglogoD);
                            if (imglogoD.Source != null)
                            {
                                string palign = "";
                                string pstyle = "";
                                int pfont = 0;
                                int pcwidth = 0;
                                int pcheight = 0;
                                GetSeparateValue(dr["paramvalue"].ToString(), ref palign, ref pstyle, ref pfont, ref pcwidth, ref pcheight);


                                imglogoD.Width = (float)pcwidth;
                                imglogoD.Height = (float)pcheight;
                                imglogoD.Stretch = Stretch.Uniform;
                                imglogoD.Margin = new Thickness(0, 0, 0, 0);



                                Grid grid = new Grid();
                                grid.Width = (float)pcwidth;
                                grid.Height = (float)pcheight;
                                grid.Children.Add(imglogoD);

                                BlockUIContainer bc = new BlockUIContainer();
                                //barcd.Visibility = System.Windows.Visibility.Visible;

                                StackPanel sp = new StackPanel();
                                sp.Width = (float)pcwidth;
                                sp.Height = (float)pcheight;
                                sp.Orientation = Orientation.Horizontal;

                                sp.HorizontalAlignment = GetRowAlignment(palign);
                                sp.Background = new SolidColorBrush(Colors.Transparent);

                                sp.Children.Add(grid);

                                bc.Child = sp;
                                FDoc.Blocks.Add(bc);
                                //FDocPrint.Blocks.Add(bc);
                            }

                        }
                        else if (dr["param"].ToString() == "B")
                        {
                            Table tab = new Table();
                            tab.CellSpacing = 0;
                            FDoc.Blocks.Add(tab);
                            //FDocPrint.Blocks.Add(tab);
                            TableRowGroup trgrp = new TableRowGroup();
                            tab.RowGroups.Add(trgrp);
                            TableRow tr = new TableRow();


                            TableCell tc1 = new TableCell();
                            Paragraph para1 = new Paragraph();
                            para1.TextAlignment = TextAlignment.Left;
                            para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para1.FontSize = 12f;
                            para1.FontWeight = FontWeights.Bold;
                            para1.Foreground = System.Windows.Media.Brushes.Black;
                            para1.Inlines.Add(dr["Npos"].ToString());
                            tc1.Blocks.Add(para1);

                            TableCell tc2 = new TableCell();
                            Paragraph para2 = new Paragraph();
                            para2.TextAlignment = TextAlignment.Center;
                            para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para2.FontSize = 12f;
                            para2.FontWeight = FontWeights.Bold;
                            para2.Foreground = System.Windows.Media.Brushes.Black;
                            para2.Inlines.Add(dr["Cpos"].ToString());
                            tc2.Blocks.Add(para2);

                            TableCell tc3 = new TableCell();
                            Paragraph para3 = new Paragraph();
                            para3.TextAlignment = TextAlignment.Right;
                            para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para3.Margin = new Thickness(0, 0, 5, 0);
                            para3.FontSize = 12f;
                            para3.FontWeight = FontWeights.Bold;
                            para3.Foreground = System.Windows.Media.Brushes.Black;
                            para3.Inlines.Add(dr["Fpos"].ToString());
                            tc3.Blocks.Add(para3);


                            AdjustColumnsInRow(dr, tc1, tc2, tc3, tr);

                            trgrp.Rows.Add(tr);
                        }
                        else if (dr["param"].ToString() == "s")
                        {
                            Table tab = new Table();
                            tab.CellSpacing = 0;
                            FDoc.Blocks.Add(tab);
                            //FDocPrint.Blocks.Add(tab);
                            TableRowGroup trgrp = new TableRowGroup();
                            tab.RowGroups.Add(trgrp);
                            TableRow tr = new TableRow();


                            TableCell tc1 = new TableCell();
                            Paragraph para1 = new Paragraph();
                            para1.TextAlignment = TextAlignment.Left;
                            para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para1.FontSize = 8.75f;
                            para1.Foreground = System.Windows.Media.Brushes.Black;
                            para1.Inlines.Add(dr["Npos"].ToString());
                            tc1.Blocks.Add(para1);

                            TableCell tc2 = new TableCell();
                            Paragraph para2 = new Paragraph();
                            para2.TextAlignment = TextAlignment.Center;
                            para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para2.FontSize = 8.75f;
                            para2.Foreground = System.Windows.Media.Brushes.Black;
                            para2.Inlines.Add(dr["Cpos"].ToString());
                            tc2.Blocks.Add(para2);

                            TableCell tc3 = new TableCell();
                            Paragraph para3 = new Paragraph();
                            para3.TextAlignment = TextAlignment.Right;
                            para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para3.Margin = new Thickness(0, 0, 5, 0);
                            para3.FontSize = 8.75f;
                            para3.Foreground = System.Windows.Media.Brushes.Black;
                            para3.Inlines.Add(dr["Fpos"].ToString());
                            tc3.Blocks.Add(para3);


                            AdjustColumnsInRow(dr, tc1, tc2, tc3, tr);

                            trgrp.Rows.Add(tr);
                        }

                        else if (dr["param"].ToString() == "Header")
                        {
                            
                            string pstyle = "";
                            int pfont = 0;

                            string pcheckH1 = "";
                            string pcheckH2 = "";
                            string pcheckH3 = "";
                            string pcaption1 = "";
                            string pcaption2 = "";
                            string pcaption3 = "";

                            GetSeparateValueHeaderDetail(dr["paramvalue"].ToString(),  ref pstyle, ref pfont,
                               ref pcheckH1, ref pcheckH2, ref pcheckH3,
                               ref pcaption1, ref pcaption2, ref pcaption3);

                            if ((pcheckH1 == "Y") || (pcheckH2 == "Y") || (pcheckH2 == "Y"))
                            {

                                Table tab = new Table();
                                tab.CellSpacing = 0;
                                FDoc.Blocks.Add(tab);

                                TableRowGroup trgrp = new TableRowGroup();
                                tab.RowGroups.Add(trgrp);
                                TableRow tr = new TableRow();

                                TableCell tc1 = new TableCell();
                                Paragraph para1 = new Paragraph();
                                para1.TextAlignment = TextAlignment.Left;
                                para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para1.FontSize = GetFontSize(pfont);
                                para1.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para1, pstyle);
                                para1.Inlines.Add(pcaption1);
                                tc1.Blocks.Add(para1);

                                TableCell tc2 = new TableCell();
                                Paragraph para2 = new Paragraph();
                                para2.TextAlignment = TextAlignment.Center;
                                para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para2.FontSize = GetFontSize(pfont);
                                para2.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para2, pstyle);
                                para2.Inlines.Add(pcaption2);
                                tc2.Blocks.Add(para2);

                                TableCell tc3 = new TableCell();
                                Paragraph para3 = new Paragraph();
                                para3.TextAlignment = TextAlignment.Right;
                                para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para3.FontSize = GetFontSize(pfont);
                                para3.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para3, pstyle);
                                para3.Inlines.Add(pcaption3);
                                tc3.Blocks.Add(para3);


                                if (pcheckH1 == "Y")
                                {
                                    tr.Cells.Add(tc1);
                                }
                                if (pcheckH2 == "Y")
                                {
                                    tr.Cells.Add(tc2);
                                }
                                if (pcheckH3 == "Y")
                                {
                                    tr.Cells.Add(tc3);
                                }

                                trgrp.Rows.Add(tr);
                            }

                            
                        }
                        else if (dr["param"].ToString() == "Detail")
                        {

                            string pstyle = "";
                            int pfont = 0;

                            string pcheckH1 = "";
                            string pcheckH2 = "";
                            string pcheckH3 = "";
                            string pcaption1 = "";
                            string pcaption2 = "";
                            string pcaption3 = "";

                            GetSeparateValueHeaderDetail(dr["paramvalue"].ToString(), ref pstyle, ref pfont,
                               ref pcheckH1, ref pcheckH2, ref pcheckH3,
                               ref pcaption1, ref pcaption2, ref pcaption3);

                            if ((pcheckH1 == "Y") || (pcheckH2 == "Y") || (pcheckH2 == "Y"))
                            {

                                Table tab = new Table();
                                tab.CellSpacing = 0;
                                FDoc.Blocks.Add(tab);

                                TableRowGroup trgrp = new TableRowGroup();
                                tab.RowGroups.Add(trgrp);
                                TableRow tr = new TableRow();

                                TableCell tc1 = new TableCell();
                                Paragraph para1 = new Paragraph();
                                para1.TextAlignment = TextAlignment.Left;
                                para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para1.FontSize = GetFontSize(pfont);
                                para1.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para1, pstyle);
                                para1.Inlines.Add(dr["Npos"].ToString());
                                tc1.Blocks.Add(para1);

                                TableCell tc2 = new TableCell();
                                Paragraph para2 = new Paragraph();
                                para2.TextAlignment = TextAlignment.Center;
                                para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para2.FontSize = GetFontSize(pfont);
                                para2.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para2, pstyle);
                                para2.Inlines.Add(dr["Cpos"].ToString());
                                tc2.Blocks.Add(para2);

                                TableCell tc3 = new TableCell();
                                Paragraph para3 = new Paragraph();
                                para3.TextAlignment = TextAlignment.Right;
                                para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                                para3.FontSize = GetFontSize(pfont);
                                para3.Foreground = System.Windows.Media.Brushes.Black;
                                SetTextStyle(para3, pstyle);
                                para3.Inlines.Add(dr["Fpos"].ToString());
                                tc3.Blocks.Add(para3);


                                if (pcheckH1 == "Y")
                                {
                                    tr.Cells.Add(tc1);
                                }
                                if (pcheckH2 == "Y")
                                {
                                    tr.Cells.Add(tc2);
                                }
                                if (pcheckH3 == "Y")
                                {
                                    tr.Cells.Add(tc3);
                                }

                                trgrp.Rows.Add(tr);
                            }


                        }
                        else if (dr["param"].ToString() == "Item")
                        {

                            string pstyle = "";
                            int pfont = 0;

                           
                            GetSeparateValueItem(dr["paramvalue"].ToString(), ref pstyle, ref pfont);

                            Table tab = new Table();
                            tab.CellSpacing = 0;
                            FDoc.Blocks.Add(tab);

                            TableRowGroup trgrp = new TableRowGroup();
                            tab.RowGroups.Add(trgrp);
                            TableRow tr = new TableRow();

                            TableCell tc1 = new TableCell();
                            Paragraph para1 = new Paragraph();
                            para1.TextAlignment = TextAlignment.Left;
                            para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para1.FontSize = GetFontSize(pfont);
                            para1.Foreground = System.Windows.Media.Brushes.Black;
                            SetTextStyle(para1, pstyle);
                            para1.Inlines.Add(dr["Npos"].ToString());
                            tc1.Blocks.Add(para1);

                            tr.Cells.Add(tc1);

                            trgrp.Rows.Add(tr);


                        }
                        else
                        {
                            Table tab = new Table();
                            tab.CellSpacing = 0;
                            FDoc.Blocks.Add(tab);
                            //FDocPrint.Blocks.Add(tab);
                            TableRowGroup trgrp = new TableRowGroup();
                            tab.RowGroups.Add(trgrp);
                            TableRow tr = new TableRow();


                            TableCell tc1 = new TableCell();
                            Paragraph para1 = new Paragraph();
                            para1.TextAlignment = TextAlignment.Left;
                            para1.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para1.FontSize = 10f;
                            para1.Foreground = System.Windows.Media.Brushes.Black;
                            para1.Inlines.Add(dr["Npos"].ToString());
                            tc1.Blocks.Add(para1);

                            TableCell tc2 = new TableCell();
                            Paragraph para2 = new Paragraph();
                            para2.TextAlignment = TextAlignment.Center;
                            para2.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para2.FontSize = 10f;
                            para2.Foreground = System.Windows.Media.Brushes.Black;
                            para2.Inlines.Add(dr["Cpos"].ToString());
                            tc2.Blocks.Add(para2);

                            TableCell tc3 = new TableCell();
                            Paragraph para3 = new Paragraph();
                            para3.TextAlignment = TextAlignment.Right;
                            para3.FontFamily = new System.Windows.Media.FontFamily("Arial");
                            para3.Margin = new Thickness(0, 0, 5, 0);
                            para3.FontSize = 10f;
                            para3.Foreground = System.Windows.Media.Brushes.Black;
                            para3.Inlines.Add(dr["Fpos"].ToString());
                            tc3.Blocks.Add(para3);

                            AdjustColumnsInRow(dr, tc1, tc2, tc3, tr);

                            trgrp.Rows.Add(tr);
                        }
                    }
                }

                count++;
            }


            printCtrl.Document = FDoc;
            printCtrl.Zoom = 100;
        }


        private void AdjustColumnsInRow(DataRow dr, TableCell tc1, TableCell tc2, TableCell tc3, TableRow tr)
        {
            if (dr["Cpos"].ToString().Trim() == "" && dr["Fpos"].ToString().Trim() == "")
            {
                tc1.ColumnSpan = 3;
                tr.Cells.Add(tc1);
            }
            else if (dr["Cpos"].ToString().Trim() == "")
            {
                tc1.ColumnSpan = 2;
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc3);
            }
            else
            {
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tr.Cells.Add(tc3);
            }
        }

        private byte[] GetMoreImage(string paramdefination, string subslno, DataTable dtbl)
        {

            byte[] returnval = null;
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "' and GroupSubSL = " + subslno);
            foreach (DataRow dr in Drs)
            {
                returnval = (byte[])dr["CustomImage"];
            }
            return returnval;
        }

        private void SetMoreImage(System.Windows.Controls.Image imglogo, byte[] bytdata)
        {
            byte[] content = bytdata;
            try
            {
                // assign byte array data into memory stream 
                MemoryStream stream = new MemoryStream(content);

                // set transparent bitmap with 32 X 32 size by memory stream data 
                Bitmap b = new Bitmap(stream);
                Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
                output.MakeTransparent();

                System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                bi.BeginInit();
                System.Drawing.Image tempImage = (System.Drawing.Image)output;
                MemoryStream ms = new MemoryStream();
                tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Seek(0, SeekOrigin.Begin);

                bi.StreamSource = stream;

                bi.EndInit();

                imglogo.Source = bi;

            }
            catch (Exception ex)
            {
                imglogo.Source = null;
            }
        }

        private void GetSeparateValue(string pstring, ref string palign, ref string pstyle, ref int pfont, ref int pcwidth, ref int pcheight)
        {
            String[] splitstring = pstring.Split('|');
            int i = 0;
            foreach (string s in splitstring)
            {
                i++;
                if (i == 1) palign = s;
                if (i == 2) pstyle = s;
                if (i == 3) pfont = GeneralFunctions.fnInt32(s);
                if (i == 4) pcwidth = GeneralFunctions.fnInt32(s);
                if (i == 5) pcheight = GeneralFunctions.fnInt32(s);
            }
        }

        private void GetSeparateValueItem(string pstring, ref string pstyle, ref int pfont)
        {
            String[] splitstring = pstring.Split('|');
            int i = 0;
            foreach (string s in splitstring)
            {
                i++;
               
                if (i == 1) pstyle = s;
                if (i == 2) pfont = GeneralFunctions.fnInt32(s);
                
            }
        }

        private void GetSeparateValueHeaderDetail(string pstring, ref string pstyle, ref int pfont, 
            ref string pcheckH1, ref string pcheckH2, ref string pcheckH3,
            ref string pcaption1, ref string pcaption2, ref string pcaption3)
        {
            String[] splitstring = pstring.Split('|');
            int i = 0;
            foreach (string s in splitstring)
            {
                i++;
                
                if (i == 1) pstyle = s;
                if (i == 2) pfont = GeneralFunctions.fnInt32(s);
                if (i == 3) pcheckH1 = s;
                if (i == 4) pcheckH2 = s;
                if (i == 5) pcheckH3 = s;
                if (i == 6) pcaption1 = s;
                if (i == 7) pcaption2 = s;
                if (i == 8) pcaption3 = s;
            }
        }

        private System.Windows.HorizontalAlignment GetRowAlignment(string alignval)
        {
            if (alignval == "left") return HorizontalAlignment.Left;
            else if (alignval == "right") return HorizontalAlignment.Right;
            else return HorizontalAlignment.Center;
        }

        private System.Windows.TextAlignment GetTextAlignment(string alignval)
        {
            if (alignval == "left") return TextAlignment.Left;
            else if (alignval == "right") return TextAlignment.Right;
            else return TextAlignment.Center;
        }

        private void SetTextStyle(System.Windows.Documents.Paragraph para, string styleval)
        {
            if (styleval == "normal")
            {
                para.FontStyle = FontStyles.Normal;
            }
            if (styleval == "italic")
            {
                para.FontStyle = FontStyles.Italic;
            }
            if (styleval == "bold")
            {
                para.FontWeight = FontWeights.Bold;
            }
        }

        private double GetFontSize(int fsize)
        {
            if (fsize == 0) return 11f;
            else return (float)fsize;
        }


        private string FindTemplateParameterValueInDatatableOrderHeader(string paramdefination, string sl, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "' and SL = " + sl);
            foreach (DataRow dr in Drs)
            {
                returnstring = dr["TextStyle"].ToString() + "|" + dr["FontSize"].ToString() + "|" + dr["ShowHeader1"].ToString() + "|" + dr["ShowHeader2"].ToString() + "|" + dr["ShowHeader3"].ToString()
                    + "|" + dr["Header1Caption"].ToString() + "|" + dr["Header2Caption"].ToString() + "|" + dr["Header3Caption"].ToString();
            }
            return returnstring;
        }


        private string FindTemplateParameterValueInDatatableOrderHeader1(string paramdefination, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "'");
            foreach (DataRow dr in Drs)
            {
                returnstring =  dr["ShowHeader1"].ToString() + "|" + dr["ShowHeader2"].ToString() + "|" + dr["ShowHeader3"].ToString();
            }
            return returnstring;
        }

        private string FindTemplateParameterValueInDatatableOrderDetail(string paramdefination, string sl, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "'");
            foreach (DataRow dr in Drs)
            {
                returnstring = dr["TextStyle"].ToString() + "|" + dr["FontSize"].ToString();
            }
            return returnstring;
        }


        private string FindTemplateParameterValueInDatatable(string paramdefination, string sl, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "' and SL = " + sl);
            foreach (DataRow dr in Drs)
            {
                returnstring = dr["TextAlign"].ToString() + "|" + dr["TextStyle"].ToString() + "|" + dr["FontSize"].ToString() + "|" + dr["CtrlWidth"].ToString() + "|" + dr["CtrlHeight"].ToString();
            }
            return returnstring;
        }

        private string FindTemplateParameterValueInDatatableForText(string paramdefination, string subslno, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "' and GroupSubSL = " + subslno);
            foreach (DataRow dr in Drs)
            {
                returnstring = dr["TextAlign"].ToString() + "|" + dr["TextStyle"].ToString() + "|" + dr["FontSize"].ToString() + "|" + dr["CtrlWidth"].ToString() + "|" + dr["CtrlHeight"].ToString();
            }
            return returnstring;
        }

        private string FindTemplateParameterValueInDatatableForImage(string paramdefination, string subslno, DataTable dtbl)
        {

            string returnstring = "";
            DataRow[] Drs = dtbl.Select("GroupName='" + paramdefination + "' and GroupSubSL = " + subslno);
            foreach (DataRow dr in Drs)
            {
                returnstring = dr["TextAlign"].ToString() + "|" + dr["TextStyle"].ToString() + "|" + dr["FontSize"].ToString() + "|" + dr["CtrlWidth"].ToString() + "|" + dr["CtrlHeight"].ToString();
            }
            return returnstring;
        }

        private void ChkHeader1_Checked(object sender, RoutedEventArgs e)
        {
            if (chkHeader1.IsChecked == true)
            {
                if (txtHeaderCaption1.Text.Trim() == "") txtHeaderCaption1.Text = "Unit Price";
                txtHeaderCaption1.IsEnabled = true;
            }
            else
            {
                txtHeaderCaption1.Text = "";
                txtHeaderCaption1.IsEnabled = false;
            }
        }

        private void ChkHeader2_Checked(object sender, RoutedEventArgs e)
        {
            if (chkHeader2.IsChecked == true)
            {
                if (txtHeaderCaption2.Text.Trim() == "") txtHeaderCaption2.Text = "Net Wt/Ct";
                txtHeaderCaption2.IsEnabled = true;
            }
            else
            {
                txtHeaderCaption2.Text = "";
                txtHeaderCaption2.IsEnabled = false;
            }
        }

        private void ChkHeader2_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ChkHeader3_Checked(object sender, RoutedEventArgs e)
        {
            if (chkHeader3.IsChecked == true)
            {
                if (txtHeaderCaption3.Text.Trim() == "") txtHeaderCaption3.Text = "Total Price";
                txtHeaderCaption3.IsEnabled = true;
            }
            else
            {
                txtHeaderCaption3.Text = "";
                txtHeaderCaption3.IsEnabled = false;
            }
        }

        private void ChkHeader3_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel4_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditOrderHeader.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnSubmit4_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatatableForOrderHeader();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditOrderHeader.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void BtnCancel5_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditSeparator.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnSubmit5_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatatableForSeparator();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditSeparator.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void TxtName_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            txtTemplateName.Text = txtName.Text + " Template";
            if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
            {
                lbSize.Visibility = Visibility.Hidden;
                pnlTemplateSizeNormal.Visibility = Visibility.Hidden;
                //pnlTemplateSizeLabel.Visibility = Visibility.Visible;
                lbPrintCopy.Visibility = Visibility.Collapsed;
                cmbPrintCopy.Visibility = Visibility.Collapsed;
                tpView.Visibility = Visibility.Collapsed;
                tpViewL.Visibility = Visibility.Visible;
            }
            else
            {
                lbSize.Visibility = Visibility.Visible;
                pnlTemplateSizeNormal.Visibility = Visibility.Visible;
                //pnlTemplateSizeLabel.Visibility = Visibility.Hidden;
                lbPrintCopy.Visibility = Visibility.Visible;
                cmbPrintCopy.Visibility = Visibility.Visible;
                tpView.Visibility = Visibility.Visible;
                tpViewL.Visibility = Visibility.Collapsed;
            }

            PopulateDefaultParamData();

            LoadInitialLinkData();

            LoadTemplate();

            chkEditTemplate.IsChecked = false;

            tbctrl.Style = this.FindResource("TemplateOuterPOSTabControlStyle") as Style;
            tpEdit.Visibility = Visibility.Collapsed;

            if ((txtName.Text == "Label - 1 Up") || (txtName.Text == "Label - 2 Up") || (txtName.Text == "Label - Butterfly")
                || (txtName.Text == "Label - Avery 5160 / NEBS 12650") || (txtName.Text == "Label - Avery 8195"))
            {
                tbctrl.SelectedIndex = 2;
                GetPrintLabel();
            }
            else
            {
                tbctrl.SelectedIndex = 1;
                GetPrintStream();
                CreateFlowControl(dclonePrint);
            }


        }

       

        private void TxtName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void Tbctrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbctrl.SelectedIndex == 1)
            {
                
                GetPrintStream();
                CreateFlowControl(dclonePrint);
            }

            if (tbctrl.SelectedIndex == 2)
            {

                GetPrintLabel();
                
            }
        }

        private bool CheckIfGroupAlreadyExists()
        {
            bool blexists = false;
            foreach(DataRow dr in dtblTemplate.Rows)
            {
                if (dr["GroupName"].ToString() == currGroupName)
                {
                    blexists = true;
                    break;
                }
            }
            return blexists;
        }

        private void BtnSubmitL_Click(object sender, RoutedEventArgs e)
        {


            UpdateDatatableForLabelText();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLabelText.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void BtnCancelL_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLabelText.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void BtnSubmitLB_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatatableForLabelBarcode();
            LoadTemplate();

            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLabelBarcode.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
            boolControlChanged = true;
        }

        private void BtnCancelLB_Click(object sender, RoutedEventArgs e)
        {
            blAddSpecificPosition = false;
            intSpecificSL = 0;
            blEditData = false;
            currID = 0;
            currSL = 0;
            currSubSL = 0;
            currGroupName = "";

            grdEditLabelBarcode.Visibility = Visibility.Collapsed;
            grdHeader.Visibility = Visibility.Visible;
            svTemplate.Visibility = Visibility.Visible;
        }

        private void CmbPrintCopy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
