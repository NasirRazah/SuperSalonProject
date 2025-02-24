
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using Microsoft.PointOfService;
using OfflineRetailV2.Data;
using OfflineRetailV2.XeposExternal;
using OfflineRetailV2.UserControls.Report;
using pos;
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
using DevExpress.Xpf.Printing;
using System.Drawing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSReturnReprintDlg.xaml
    /// </summary>
    public partial class frm_POSReturnReprintDlg : Window
    {
        private string strDiscountLevel = "A";
        private string strTaxExempt = "N";
        private bool blChangeCustomer = false;
        private int CustDTaxID = 0;
        private string CustDTaxName = "";
        private double CustDTaxRate = 0;
        private int CustDTaxType = 0;
        private bool bl100percinvdiscount = false;
        private int intTaxID1 = 0;
        private int intTaxID2 = 0;
        private int intTaxID3 = 0;

        private double dblTax1 = 0;
        private double dblTax2 = 0;
        private double dblTax3 = 0;
        private double dblFeesCrg = 0;
        private double dblCouponAmount = 0;
        private double dblCouponPerc = 0;
        private double dblCouponApplicableTotal = 0;

        private double dblFeesCouponAmount = 0;
        private double dblFeesCouponPerc = 0;
        private double dblFeesCouponApplicableTotal = 0;

        private double dblFeesCouponTaxAmount = 0;
        private double dblFeesCouponTaxRate = 0;

        private int intSuspendInvNo;
        private bool blResumeTransaction;
        private bool blWorkOrder;

        private bool blOpenCustomerOrder;

        private double dblSpecialMixMatchAmount = 0;
        DataTable dtblPOS = null;
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        DataTable dtblLayaway = null;
        double numsTS = 0;
        double numsP = 0;
        double numsB = 0;
        double numsD = 0;
        double numAmount = 0;
        int intMaxInvNo = 0;
        int LawAwayRecordCustID = 0;

        public frm_POSReturnReprintDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSReturnReprintDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new System.Windows.Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }

        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }


        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        #region Variables to get Device Drivers from Local Computer

        PosExplorer m_posExplorer = null;
        Scanner m_posScanner = null;

        #endregion Variables to get Device Drivers from Local Computer

        private volatile int watchr = 0; // not used
        private FileSystemWatcher watcher; // not used

        #region Precidia Transaction Variables

        private bool blCG = false;
        private string CGresp = "";
        private string CGresptxt = "";
        private string CGmonitor = "";
        private string CGrequestfile = "";
        private string CGanswerfile = "";
        private string CGtrantype = "";
        private double CGamt = 0;
        private int CGinv = 0;

        private string PrecidiaLogFile = "";
        private string PrecidiaLogPath = "";

        #endregion Precidia Transaction Variables

        #region Datacap payment variables

        private string Dcap_TranType = "";
        private string Dcap_TranCode = "";
        private string Dcap_CardType = "";
        private double Dcap_CashBkAmt = 0;
        private double Dcap_TranAmt = 0;
        private double Dcap_AuthAmt = 0;
        private double Dcap_BalAmt = 0;
        private string Dcap_Sign = "";

        private string Dcap_CmdStatus = "";
        private string Dcap_TextResponse = "";
        private string Dcap_AcctNo = "";
        private string Dcap_Merchant = "";
        private string Dcap_AuthCode = "";
        private string Dcap_RefNo = "";
        private string Dcap_AcqRefData = "";
        private string Dcap_RecordNo = "";
        private string Dcap_InvoiceNo = "";

        private string Dcap_ProcessData = "";
        private int CallPadResetCount = 0;

        private string Dcap_PPAD_CmdStatus = "";
        private string Dcap_PPAD_TextResponse = "";
        private string Dcap_PrintDraft = "";

        #endregion Datacap payment variables

        #region Common Variables for assign Card Transaction Response

        private string AuthCode = "";
        private string TranID = "";
        private string CardNum = "";
        private string CardExMM = "";
        private string CardExYY = "";
        private string CardLogo = "";
        private string CardType = "";
        private string ApprovedAmt = "";
        private string RefNo = "";
        private string CardEntry = "";
        private string AcqRef = "";
        private string Token = "";
        private string MerchantID = "";
        private string strMercuryMerchantID;
        private string MercuryProcessData;
        private string MercuryTranCode;
        private double MercuryPurchaseAmount = 0;
        private string MercuryResponseOrigin = "";
        private string MercuryRecordNo = "";
        private string MercuryResponseReturnCode = "";
        private string MercuryTextResponse = "";
        private double BalanceAmount = 0;
        private bool blExistCoupon = false;

        #endregion Common Variables for assign Card Transaction Response

        #region Other Variables

        private string SCAN = "";
        SortAndSearchLookUpEdit sortAndSearchLookUpEditC;

        private int rowPos = 0;
        private DevExpress.Xpf.Grid.GridColumn _searchcol;
        private DataTable dtblReturnItem = null;
        private DataTable dtblSelectTag = null;
        private int intCustID;
        private bool blFetch = false;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blAllowByAdmin = false;
        private bool blCardPayment = false;
        private bool blHouseAccountPayment = false;
        private int PrevInv = 0;
        private int CurrInv = 0;

        private int intMainScreenCustID;

        private bool MercuryCardPaymentCheck = false;

        private string dtCustomerDOB;

        private int intSelectedReturnInvoiceNo;

        #endregion Other Variables

        #region Public Declaration

        public int SelectedReturnInvoiceNo
        {
            get { return intSelectedReturnInvoiceNo; }
            set { intSelectedReturnInvoiceNo = value; }
        }

        public string CustomerDOB
        {
            get { return dtCustomerDOB; }
            set { dtCustomerDOB = value; }
        }

        public bool ExistCoupon
        {
            get { return blExistCoupon; }
            set { blExistCoupon = value; }
        }

        public DataTable ReturnItem
        {
            get { return dtblReturnItem; }
            set { dtblReturnItem = value; }
        }

        public int MainScreenCustID
        {
            get { return intMainScreenCustID; }
            set { intMainScreenCustID = value; }
        }

        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        #endregion Public Declaration

        // Get all stores for customers

        public void PopulateCustomerStores()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupCustomerStore();
            cmbStore.Items.Clear();
            foreach (DataRow dr in dbtblCust.Rows)
            {
                cmbStore.Items.Add(dr["IssueStore"].ToString());
            }
            dbtblCust.Dispose();
            cmbStore.Text = Settings.StoreCode;
        }

        // Get customers of a perticular store

        public void PopulateCustomer()
        {
            PosDataObject.Customer objCustomer = new PosDataObject.Customer();
            objCustomer.Connection = SystemVariables.Conn;
            objCustomer.DataObjectCulture_All = Settings.DataObjectCulture_All;
            DataTable dbtblCust = new DataTable();
            dbtblCust = objCustomer.FetchLookupData(cmbStore.Text);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCust.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbC.ItemsSource = dtblTemp;
            cmbC.EditValue = "0";

            dbtblCust.Dispose();
        }

        // Get Item SKU Lookup

        public void PopulateSKU(int intOption)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.FetchMLookupData(intOption);
            dbtblSKU.Rows.Add(new object[] { "0", Properties.Resources.All, Properties.Resources.All, "", "" });

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSKU.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbP.ItemsSource = dtblTemp;
            cmbP.EditValue = "0";

            dbtblSKU.Dispose();
        }

        // Get Staff Lookup

        public void PopulateEmployee()
        {
            PosDataObject.Employee objProduct = new PosDataObject.Employee();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.DataObjectCulture_All = Settings.DataObjectCulture_All;
            objProduct.DataObjectCulture_ADMIN = Settings.DataObjectCulture_ADMIN;
            objProduct.DataObjectCulture_Administrator = Settings.DataObjectCulture_Administrator;
            DataTable dbtblSKU = new DataTable();
            dbtblSKU = objProduct.LookupEmployee();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblSKU.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbE.ItemsSource = dtblTemp;
            cmbE.EditValue = "9999";

            dbtblSKU.Dispose();
        }

        public async Task RefreshData()
        {
            await FetchHeaderData();
            ChkReturned.IsChecked = true;
            ChkReturned.Content = "Unselect all receipt items";
        }

        // Loading Screen
        private async void Frm_POSReturnReprintDlg_Loaded(object sender, RoutedEventArgs e)
        {
            CreateDataTable();  //  For LayAwayRefund

            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Return___Reprint_Receipt;
            if (Settings.TaxInclusive == "N")
            {
                colGPrice.Visible = false;
            }
            if (Settings.TaxInclusive == "Y")
            {
                colRate.Visible = false;
                colGPrice.VisibleIndex = 3;
            }
            
            if (Settings.DecimalPlace == 3)
            {
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                GeneralFunctions.SetDecimal(numF, 3);
                GeneralFunctions.SetDecimal(numT, 3);
            }
            else
            {
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                GeneralFunctions.SetDecimal(numF, 2);
                GeneralFunctions.SetDecimal(numT, 2);
            }
            dtF.EditValue = DateTime.Today.Date;
            dtT.EditValue = DateTime.Today.Date;
            dtblReturnItem = new DataTable();
            dtblReturnItem.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("ProductType", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Price", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Qty", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DiscLogic", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DiscValue", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DiscountID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Discount", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DiscountText", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX1ID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2ID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3ID", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TX1TYPE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2TYPE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3TYPE", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TX1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TXRATE1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TXRATE2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TXRATE3", System.Type.GetType("System.String"));

            // for Fees & Charges

            dtblReturnItem.Columns.Add("FEESID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEES", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESTAX", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("FEESQTY", System.Type.GetType("System.String"));

            // for Destination Tax
            dtblReturnItem.Columns.Add("DTXID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DTXTYPE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DTXRATE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("DTX", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("EditFlag", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("QtyDecimal", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("PromptPrice", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("BUYNGETFREEHEADERID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("BUYNGETFREECATEGORY", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("BUYNGETFREENAME", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("SL", System.Type.GetType("System.Int32"));
            dtblReturnItem.Columns.Add("AGE", System.Type.GetType("System.Int32"));
            dtblReturnItem.Columns.Add("GRate", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("GPrice", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("UOM", System.Type.GetType("System.String"));

            dtblSelectTag = new DataTable();
            dtblSelectTag.Columns.Add("INV", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("PID", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("PNAME", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("PTYPE", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("QTY", System.Type.GetType("System.Double"));
            dtblSelectTag.Columns.Add("PRICE", System.Type.GetType("System.Double"));
            dtblSelectTag.Columns.Add("CINV", System.Type.GetType("System.String"));
            dtblSelectTag.Columns.Add("TQTY", System.Type.GetType("System.String"));

            lbReceipt.Text = "";
            lbCustCompany.Text = "";
            lbCustName.Text = "";
            lbCustID.Text = "";

            cmbDate.SelectedIndex = 0;
            cmbAmount.SelectedIndex = 0;

            //pictureBox1.Visible = false;
            //pictureBox2.Visible = false;
            dtF.Visibility = Visibility.Collapsed;
            dtT.Visibility = Visibility.Collapsed;

            numF.Visibility = Visibility.Collapsed;
            numT.Visibility = Visibility.Collapsed;

            if (Settings.CentralExportImport == "N")
            {
                cmbStore.Visibility = Visibility.Collapsed;
            }
            else
            {
                cmbStore.Visibility = Visibility.Visible;
                PopulateCustomerStores();
            }

            PopulateCustomer();
            PopulateSKU(0);
            PopulateEmployee();
            rg1.IsChecked = true;

            await FetchHeaderData();
            ChkReturned.IsChecked = true;
            ChkReturned.Content = "Unselect all receipt items";
            blFetch = true;

            /*if (gridView2.FocusedRowHandle >= 0)
            {
                FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));
            }*/

            if (intMainScreenCustID != 0)
            {
                LookUpEdit clkup = new LookUpEdit();
                //foreach (Control c in panel1.Controls)--Sam
                //{
                //    if (c is LookUpEdit)
                //    {
                //        if (c.Name == "cmbC")
                //        {
                //            clkup = (LookUpEdit)c;
                //            break;
                //        }
                //    }
                //}
                if (Settings.CentralExportImport == "Y")
                {
                    PosDataObject.POS objPOS = new PosDataObject.POS();
                    objPOS.Connection = SystemVariables.Conn;
                    string tstr = objPOS.FetchCustomerIssueStore(intMainScreenCustID);
                    if (tstr != Settings.StoreCode) cmbStore.Text = tstr;
                }
                clkup.EditValue = intMainScreenCustID.ToString();
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }

            txtInv.Focus();

            if (Settings.ScaleDevice == "Datalogic Scale")
            {
                PrepareDatalogicScanner();
            }
        }

        #region Initialize Data Logic Scanner

        private void PrepareDatalogicScanner()
        {
            SCAN = "";
            bool blFind = false;
            m_posExplorer = new PosExplorer();

            DeviceInfo deviceInfo = null;
            DeviceCollection deviceCollection = m_posExplorer.GetDevices();
            string deviceName = Settings.Datalogic_Scanner;
            for (int i = 0; i < deviceCollection.Count; i++)
            {
                deviceInfo = deviceCollection[i];
                if (deviceInfo.ServiceObjectName == deviceName)
                {
                    blFind = true;
                    break;
                }
            }

            if (blFind)
            {
                if (deviceInfo != null)
                {
                    if (m_posScanner != null) { m_posScanner.Release(); m_posScanner.Close(); }

                    try
                    {
                        m_posScanner = (Scanner)m_posExplorer.CreateInstance(deviceInfo);

                        m_posScanner.Open();
                        m_posScanner.Claim(20000);

                        m_posScanner.DeviceEnabled = true;
                        m_posScanner.DataEventEnabled = true;
                        m_posScanner.DecodeData = true;
                        m_posScanner.DataEvent += new DataEventHandler(_Scanner_DataEvent);
                    }
                    catch
                    {
                    }
                }
            }
        }

        async void _Scanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();

                byte[] b = m_posScanner.ScanData;

                string str = "";

                b = m_posScanner.ScanDataLabel;
                for (int i = 0; i < b.Length; i++)
                    str += (char)b[i];

                m_posScanner.DataEventEnabled = true;
                m_posScanner.DeviceEnabled = true;

                if (Settings.Scanner_8200 == "Y")
                {
                    try
                    {
                        str = str.Remove(0, 3);
                    }
                    catch
                    {
                    }
                }

                SCAN = str;
                txtInv.Text = SCAN;
                try
                {
                    m_posScanner.DeviceEnabled = false;
                    await FetchHeaderData_Specific();
                    SCAN = "";
                }
                finally
                {
                    m_posScanner.DeviceEnabled = true;
                }
            }
            catch (PosControlException)
            {
            }
            finally
            {
            }
        }

        #endregion Initialize Data Logic Scanner

        #region Get Invoices / Layaway with Details

        private async Task FetchHeaderData()
        {

            double numFDouble = 0.0;
            double numTDouble = 0.0;
            if (double.TryParse(numF.Text, out numFDouble))
            {
                numFDouble = Convert.ToDouble(numF.Text);
            }
            if (double.TryParse(numT.Text, out numTDouble))
            {
                numTDouble = Convert.ToDouble(numT.Text);
            }
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchReprintData(rg1.IsChecked == true ? 0 : 1, GeneralFunctions.fnInt32(cmbP.EditValue.ToString()),
                                            GeneralFunctions.fnInt32(cmbC.EditValue.ToString()), cmbDate.SelectedIndex,
                                            dtF.DateTime, dtT.DateTime, cmbAmount.SelectedIndex, numFDouble, numTDouble,
                                            GeneralFunctions.GetCloseOutID(), cmbStore.Text, GeneralFunctions.fnInt32(cmbE.EditValue.ToString()));

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

            grdHeader.ItemsSource = dtblTemp;

            dtbl.Dispose();
            dtblTemp.Dispose();

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }
        }

        private void LoadInitialDataForLayAwayRefund(int LayID)
        {
            DataTable dtbl = new DataTable();
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objPOS.FetchLayawayHeaderLayID(LayID,true);
            int intC = 0;
            double dblPayment = 0;
            double dblBalance = 0;

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            dtblLayaway.Rows.Clear();

            foreach (DataRow dr in dtbl.Rows)
            {
                intC++;

                if (Settings.DecimalPlace == 3)
                {
                    dblPayment = FetchTotalPayment(GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString()));
                    dblBalance = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TotalSale"].ToString()) - dblPayment).ToString("f3"));
                }
                else
                {
                    dblPayment = FetchTotalPayment(GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString()));
                    dblBalance = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TotalSale"].ToString()) - dblPayment).ToString("f"));
                }

                dtblLayaway.Rows.Add(new object[] { intC.ToString(),
                                                dr["LayawayNo"].ToString(),
                                                dr["InvoiceNo"].ToString(),
                                                dr["SKU"].ToString(),
                                                dr["Description"].ToString(),
                                                GeneralFunctions.fnDouble(dr["Qty"].ToString()),
                                                GeneralFunctions.fnDouble(dr["Cost"].ToString()),
                                                dr["DateDue"].ToString(),
                                                GeneralFunctions.fnDouble(dr["TotalSale"].ToString()),
                                                dr["ItemID"].ToString(),
                                                dr["ProductID"].ToString(),
                                                dr["ProductType"].ToString(),
                                                GeneralFunctions.fnDouble(dblPayment.ToString()),
                                                GeneralFunctions.fnDouble(dblBalance.ToString()),GeneralFunctions.fnDouble("0"),
                                                dr["MatrixOptionID"].ToString(),
                                                dr["OptionValue1"].ToString(),
                                                dr["OptionValue2"].ToString(),
                                                dr["OptionValue3"].ToString(),
                                                strip,
                                                dr["CustomerID"].ToString()});
            }
            dtbl.Dispose();
        }
        // Fetch Total Payment against a Layaway Record

        private double FetchTotalPayment(int InvNo)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objPOS.FetchLayawayPayment(InvNo);
        }
        private async Task FetchDetailData(int INV)
        {
            

            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchReprintRecord(INV, rg1.IsChecked == true ? 0 : 1);

            if (rg1.IsChecked == true)
            {
                RearrangeDataForTaggedItem(dtbl);
            }


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

            grdDetail.ItemsSource = dtblTemp;

            string RNO = "";
            string CC = "";
            string CI = "";
            string CN = "";
            string CID = "";
            RNO = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv);
            CC = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCC);
            CI = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCustID);
            CN = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCust);
            CID = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID);
            if (rg1.IsChecked == true) lbReceipt.Text = "Receipt Number            " + RNO;
            if (rg2.IsChecked == true) lbReceipt.Text = "Layaway Number            " + RNO;
            if (CID != "0")
            {
                lbCustCompany.Text = "Company                " + CC;
                lbCustName.Text = "Customer               " + CN;
                lbCustID.Text = "Customer ID            " + CI;
            }
            else
            {
                lbCustCompany.Text = "";
                lbCustName.Text = "";
                lbCustID.Text = "";
            }
            dtbl.Dispose();

            bool flag = false;
            if (rg1.IsChecked == true)
            {
                foreach (DataRow drt in dtbl.Rows)
                {
                    if (drt["ProductType"].ToString() == "T")
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    btnSelectTag.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnSelectTag.Visibility = Visibility.Visible;
                }
            }

            if (rg2.IsChecked == true)
            {
                LoadInitialDataForLayAwayRefund(INV);  //  for LayAway Refund 

                if ((grdDetail.ItemsSource as DataTable) != null)
                {

                    DataTable dtblTemp1 = grdDetail.ItemsSource as DataTable;
                    if (dtblTemp1.Rows.Count!=0)
                    {

                        ChkReturned.IsChecked = true;
                        ChkReturned.Content = "   Unselect all receipt items";
                        foreach (DataRow dr in dtblTemp1.Rows)
                        {
                            dr["CheckReturned"] = true;
                            //dr["Qty"] = "12345";
                        }
                        grdDetail.ItemsSource = dtblTemp1;
                        dtblTemp1.Dispose();
                    }
                }

            }

        }

        #endregion Get Invoices / Layaway with Details

        // Get Invoice after Scanning

        private async Task FetchHeaderData_Specific()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchReprintData_Specific(rg1.IsChecked == true ? 0 : 1, GeneralFunctions.GetCloseOutID(), GeneralFunctions.fnInt32(txtInv.Text.Trim()));

            if (dtbl.Rows.Count > 0)
            {
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
                grdHeader.ItemsSource = dtblTemp;
            }

            dtbl.Dispose();

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }

            txtInv.Text = "";
            txtInv.Focus();
        }

        // Rearrange for Tagged Item

        private void RearrangeDataForTaggedItem(DataTable rfdtbl)
        {
            int i = 0;
            System.Collections.ArrayList CheckArray = new System.Collections.ArrayList();
            foreach (DataRow dr in rfdtbl.Rows)
            {
                if (dr["ProductType"].ToString() == "T")
                {
                    double val = 0;
                    PosDataObject.POS objp = new PosDataObject.POS();
                    objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    val = objp.GetMaxReturnedTagQty(GeneralFunctions.fnInt32(dr["ID"].ToString()));
                    double val1 = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                    dr["Qty"] = val1 - val;
                }
                if (GeneralFunctions.fnDouble(dr["Qty"].ToString()) == 0)
                {
                    CheckArray.Add(dr["ID"].ToString());
                }
                i++;
            }

            for (int j = 0; j <= CheckArray.Count - 1; j++)
            {
                int intID = GeneralFunctions.fnInt32(CheckArray[j]);
                DeleteRowfromBasket(rfdtbl, intID);
            }
        }

        // Rearrange for Mix n Match Item

        private void RearrangeDataForMixNMatchItem(DataTable rfdtbl, int INV)
        {
            DataTable dtbl = new DataTable();
            dtbl = rfdtbl;

            PosDataObject.POS objp1 = new PosDataObject.POS();
            objp1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int i = objp1.GetTotalInvoiceItems(INV);

            if (i != dtbl.Select("checkreturned = true").Length) // ignore if all items of the invoice return
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (GeneralFunctions.fnInt32(dr["MixMatchID"].ToString()) > 0)
                    {
                        int RID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                        int MxID = GeneralFunctions.fnInt32(dr["MixMatchID"].ToString());
                        PosDataObject.POS objp = new PosDataObject.POS();
                        objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        double val = objp.GetMaxMixNMatchDiscount(INV, MxID);

                        DataTable dtmp = new DataTable();
                        PosDataObject.POS objp2 = new PosDataObject.POS();
                        objp2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtmp = objp2.GetMaxMixNMatchDiscountDetails(INV, MxID);

                        foreach (DataRow dr1 in rfdtbl.Rows)
                        {
                            if ((GeneralFunctions.fnInt32(dr1["ID"].ToString()) == RID) && (GeneralFunctions.fnInt32(dr1["MixMatchID"].ToString()) == MxID))
                            {
                                dr1["Discount"] = val.ToString();

                                foreach (DataRow dr4 in dtmp.Rows)
                                {
                                    dr1["DiscountID"] = dr4["DiscountID"].ToString();
                                    dr1["DiscValue"] = dr4["DiscValue"].ToString();
                                    dr1["DiscLogic"] = dr4["DiscLogic"].ToString();
                                    dr1["DiscountText"] = dr4["DiscountText"].ToString();
                                    dr1["TaxTotal1"] = dr4["TaxTotal1"].ToString();
                                    dr1["TaxTotal2"] = dr4["TaxTotal2"].ToString();
                                    dr1["TaxTotal3"] = dr4["TaxTotal3"].ToString();
                                }

                                break;
                            }
                        }

                        dtmp.Dispose();
                    }
                }
            }
        }

        // Delete

        private void DeleteRowfromBasket(DataTable BasketTbl, int DeleteID)
        {
            int delindx = -1;
            int i = 0;
            foreach (DataRow dr in BasketTbl.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["ID"].ToString()) == DeleteID)
                {
                    delindx = i;
                    break;
                }
                i++;
            }
            if (delindx != -1)
                BasketTbl.Rows[delindx].Delete();
        }

        private async void LookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void LookupEditE_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void gridView2_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                if (blFetch)
                    await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                dtCustomerDOB = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDOB);
                if (rg1.IsChecked == true)
                {
                    if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colVoidNo) == "0")
                        btnVoid.IsEnabled = true;
                    else
                        btnVoid.IsEnabled = false;

                    CurrInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
                    if (PrevInv == 0)
                    {
                        PrevInv = CurrInv;
                    }
                    else
                    {
                        dtblSelectTag.Rows.Clear();
                    }
                }
            }
        }

        private void repCheck_Checked(object sender, RoutedEventArgs e)
        {
            int intCheck = 0;
            int intUncheck = 0;
            string strNew = "";//Todo:e.NewValue.ToString();
            int intRow = gridView1.FocusedRowHandle;
            if (intRow == -1) return;
            grdDetail.SetCellValue(intRow, colCheck, strNew);
            DataTable dtbl = grdDetail.ItemsSource as DataTable;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (Convert.ToBoolean(dr["CheckReturned"]))
                {
                    intCheck++;
                }
                else
                {
                    intUncheck++;
                }
            }
            grdDetail.ItemsSource = dtbl;
            if (dtbl.Rows.Count == intCheck)
            {
                ChkReturned.IsChecked = true;
                ChkReturned.Content = "   Unselect all receipt items";
            }

            if (dtbl.Rows.Count == intUncheck)
            {
                ChkReturned.IsChecked = false;
                ChkReturned.Content = "   Select all receipt items";
            }
            dtbl.Dispose();
        }


        private void ChkReturned_Checked(object sender, RoutedEventArgs e)
        {
            if ((grdDetail.ItemsSource as DataTable) == null) return;
            if (ChkReturned.IsChecked == true)
            {
                ChkReturned.Content = "   Unselect all receipt items";
                DataTable dtbl = grdDetail.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["CheckReturned"] = true;
                }
                grdDetail.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                ChkReturned.Content = "   Select all receipt items";
                DataTable dtbl1 = grdDetail.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["CheckReturned"] = false;
                }
                grdDetail.ItemsSource = dtbl1;
                dtbl1.Dispose();
            }
        }

        private async void gridView2_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
                int VOIDNO = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colVoidNo));
                string GA = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colGA);
                /*if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/

                if (rg1.IsChecked == true)
                {
                    bool blf = false;
                    if (VOIDNO > 0)
                        blf = true;
                    else
                        blf = false;
                    PrintInvoice(INV, blf, GA);
                }
                else
                {
                    if (Settings.PreprintedReceipt == "N")
                    {
                        UpdateReceiptCnt(INV);
                        blurGrid.Visibility = Visibility.Visible;
                        frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                        try
                        {
                            DataTable dtblLayawayForInvoice = new DataTable();
                            dtblLayawayForInvoice.Columns.Add("LAYAWAYNO", System.Type.GetType("System.String"));
                            dtblLayawayForInvoice.Rows.Add(new object[] {
                                        INV.ToString() });
                            frm_POSInvoicePrintDlg.LayawayDtbl = dtblLayawayForInvoice;
                            dtblLayawayForInvoice.Dispose();
                            frm_POSInvoicePrintDlg.LayTran = FetchMaxLayawayTranNo(INV);
                            frm_POSInvoicePrintDlg.PrintType = "Reprint Layaway";
                            frm_POSInvoicePrintDlg.InvNo = INV;
                            frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                            frm_POSInvoicePrintDlg.ShowDialog();
                        }
                        finally
                        {
                            blurGrid.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        DataTable dtblLayawayForInvoice = new DataTable();
                        dtblLayawayForInvoice.Columns.Add("LAYAWAYNO", System.Type.GetType("System.String"));
                        dtblLayawayForInvoice.Rows.Add(new object[] {
                                        INV.ToString() });

                        ExecuteLayawayPreprintedReport(dtblLayawayForInvoice, INV, FetchMaxLayawayTranNo(INV));
                    }
                }
            }
        }

        // Get maximum Layway Tran No
        private int FetchMaxLayawayTranNo(int pLayNo)
        {
            PosDataObject.POS objposL2 = new PosDataObject.POS();
            objposL2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposL2.FetchLayawayMaxTranNo(pLayNo);
        }

        // Update Reprint Receipt count against the invoice
        private void UpdateReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            objPOS.LoginUserID = SystemVariables.CurrentUserID;
            objPOS.FunctionButtonAccess = blFunctionBtnAccess;
            objPOS.ChangedByAdmin = intSuperUserID;
            string ret = objPOS.UpdateReceiptCount(invno, rg1.IsChecked == true ? 0 : 1);
        }

        // Get Reprint Receipt count against the invoice
        private int GetReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.GetReceiptCount(invno);
        }

        private async void gridView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (rg1.IsChecked == true)
            {
                if (gridView1.FocusedRowHandle >= 0)
                {
                    bool blval = !Convert.ToBoolean(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colCheck));
                    grdDetail.SetCellValue(gridView1.FocusedRowHandle, "CheckReturned", blval);

                    int intCheck = 0;
                    int intUncheck = 0;

                    DataTable dtbl = grdDetail.ItemsSource as DataTable;
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        if (Convert.ToBoolean(dr["CheckReturned"]))
                        {
                            intCheck++;
                        }
                        else
                        {
                            intUncheck++;
                        }
                    }
                    grdDetail.ItemsSource = dtbl;
                    if (dtbl.Rows.Count == intCheck)
                    {
                        ChkReturned.IsChecked = true;
                        ChkReturned.Content = "   Unselect all receipt items";
                    }

                    if (dtbl.Rows.Count == intUncheck)
                    {
                        ChkReturned.IsChecked = false;
                        ChkReturned.Content = "   Select all receipt items";
                    }
                    dtbl.Dispose();
                }
            }
        }

        // Check if Customer is Active
        private bool CheckActiveCustomer(int pCust)
        {
            PosDataObject.POS objpos = new PosDataObject.POS();
            objpos.Connection = SystemVariables.Conn;
            return objpos.IsActiveCustomer(pCust);
        }

        private void LayAwayReturn()
        {

            //if (rg2.IsChecked == true)
            //{
            //    LayAwayReturn();
            //    return;
            //}

        }
        private async void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFunctionButton("31h")) return;
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colVoidNo) != "0") return;

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colGA) == "Y") return;

            /*if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCustID)) != 0)
            {
                if (!CheckActiveCustomer(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCustID))))
                {
                    DocMessage.MsgInformation(Properties.Resources."Transaction can not be possible for an inactive customer");
                    return;
                }
            }*/

            DataTable dtbl = new DataTable();
            dtbl = grdDetail.ItemsSource as DataTable;

            if (rg1.IsChecked == true)
                RearrangeDataForMixNMatchItem(dtbl, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));

            double dqty = 0;
            double disc = 0;
            double fees = 0;
            double feestax = 0;

            int RowNo = 0;

            foreach (DataRow dr in dtbl.Rows)
            {
                if (!Convert.ToBoolean(dr["CheckReturned"].ToString())) continue;

                if (dr["ProductType"].ToString() == "G")
                {
                    PosDataObject.POS objp = new PosDataObject.POS();
                    objp.Connection = SystemVariables.Conn;
                    double bal = objp.FetchGiftCertAmount(dr["ProductID"].ToString(), Settings.StoreCode, Settings.CentralExportImport);
                    if (bal < GeneralFunctions.fnDouble(dr["Price"].ToString())) continue;
                }

                dqty = 0;
                dqty = -(GeneralFunctions.fnDouble(dr["Qty"].ToString())); //-(GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["ReturnedItemCnt"].ToString()));
                disc = 0;
                disc = -(GeneralFunctions.fnDouble(dr["Discount"].ToString()));
                fees = 0;
                feestax = 0;
                fees = -(GeneralFunctions.fnDouble(dr["Fees"].ToString()));
                feestax = -(GeneralFunctions.fnDouble(dr["FeesTax"].ToString()));

                RowNo++;

                dtblReturnItem.Rows.Add(new object[] {  dr["ID"].ToString(),
                                                        dr["ProductID"].ToString(),
                                                        dr["Description"].ToString(),
                                                        dr["ProductType"].ToString(),
                                                        dr["Price"].ToString(),
                                                        dqty.ToString(),
                                                        dr["DiscLogic"].ToString(),
                                                        dr["DiscValue"].ToString(),
                                                        dr["DiscountID"].ToString(),
                                                        disc.ToString(),
                                                        dr["DiscountText"].ToString(),
                                                        dr["TaxID1"].ToString(),
                                                        dr["TaxID2"].ToString(),
                                                        dr["TaxID3"].ToString(),
                                                        dr["TaxType1"].ToString(),
                                                        dr["TaxType2"].ToString(),
                                                        dr["TaxType3"].ToString(),
                                                        dr["TaxTotal1"].ToString(),
                                                        dr["TaxTotal2"].ToString(),
                                                        dr["TaxTotal3"].ToString(),

                                                        dr["Taxable1"].ToString(),
                                                        dr["Taxable2"].ToString(),
                                                        dr["Taxable3"].ToString(),

                                                        dr["TaxRate1"].ToString(),
                                                        dr["TaxRate2"].ToString(),
                                                        dr["TaxRate3"].ToString(),

                                                        dr["FeesID"].ToString(),
                                                        dr["FeesLogic"].ToString(),
                                                        dr["FeesValue"].ToString(),
                                                        dr["FeesTaxRate"].ToString(),
                                                        fees.ToString(),
                                                        feestax.ToString(),
                                                        dr["FeesText"].ToString(),
                                                        dr["FeesQty"].ToString(),
                                                        dr["DTaxID"].ToString(),
                                                        dr["DTaxType"].ToString(),
                                                        dr["DTaxRate"].ToString(),
                                                        dr["DTax"].ToString(),
                                                        dr["EditFlag"].ToString(),
                                                        dr["QtyDecimal"].ToString(),
                                                        dr["PromptPrice"].ToString(),
                                                        dr["BuyNGetFreeHeaderID"].ToString(),
                                                        dr["BuyNGetFreeCategory"].ToString(),
                                                        dr["BuyNGetFreeName"].ToString(),
                                                        RowNo.ToString(),
                                                        dr["Age"].ToString(),
                                                        dr["GRate"].ToString(),
                                                        dr["GPrice"].ToString(),dr["UOM"].ToString()
                                                        });
            }

            if (dtblSelectTag.Rows.Count > 0)
            {
                DataTable dtblTemp = new DataTable();
                if (dtblReturnItem.Rows.Count > 0) dtblTemp = dtblReturnItem;

                dtbl = dtblSelectTag;
                dqty = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    bool fnd = false;
                    foreach (DataRow drt in dtblTemp.Rows)
                    {
                        if (drt["ID"].ToString() == dr["ID"].ToString())
                        {
                            fnd = true;
                            break;
                        }
                    }
                    if (!fnd)
                    {
                        dqty = 0;
                        dqty = -(GeneralFunctions.fnDouble(dr["QTY"].ToString())); //-(GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["ReturnedItemCnt"].ToString()));
                        disc = 0;
                        //disc = -(GeneralFunctions.fnDouble(dr["Discount"].ToString()));
                        dtblReturnItem.Rows.Add(new object[] {dr["ID"].ToString(),
                                                dr["PID"].ToString(),
                                                dr["PNAME"].ToString(),
                                                dr["PTYPE"].ToString(),
                                                dr["PRICE"].ToString(),
                                                dqty.ToString(),
                                                "",//dr["DiscLogic"].ToString(),
                                                "",//dr["DiscValue"].ToString(),
                                                "",//dr["DiscountID"].ToString(),
                                                disc.ToString(),
                                                ""});//dr["DiscountText"].ToString()});
                    }
                }
            }
            blExistCoupon = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colcoupon)) != 0;

            if (dtblReturnItem.Rows.Count > 0)
            {
                intSelectedReturnInvoiceNo = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
                DialogResult = true;
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
        }

        private async void rg1_Checked(object sender, RoutedEventArgs e)
        {
            if (rg1.IsChecked == true) // Invoice
            {
                colCheck.Visible = true;
                btnRefundLayAway.Visibility = Visibility.Collapsed;
                btnReturn.Visibility = Visibility.Visible;
                //btnSelectTag.Visible = true;

                btnVoid.Visibility = Visibility.Visible;
                colInv.Header = "Inv No.";
                ChkReturned.Visibility = Visibility.Visible;
                lbInv.Text = "Invoice #";
                
            }
            else    // Layaway
            {
                colCheck.Visible = false;
                btnRefundLayAway.Visibility = Visibility.Visible;
                btnReturn.Visibility = Visibility.Collapsed;
                btnVoid.Visibility = Visibility.Collapsed;
                colInv.Header = "Lay No.";
                ChkReturned.Visibility = Visibility.Collapsed;
                //btnSelectTag.Visible = false;
                lbInv.Text = "Layaway #";
            }
            await FetchHeaderData();
            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }
            else
            {
                grdDetail.ItemsSource = null;
                lbReceipt.Text = "";
                lbCustCompany.Text = "";
                lbCustName.Text = "";
                lbCustID.Text = "";
            }

            txtInv.Text = "";
            txtInv.Focus();
        }

        /// Check Security Permission
        private bool CheckFunctionButton(string scode)
        {
            blFunctionBtnAccess = false;
            if (SystemVariables.CurrentUserID <= 0)
            {
                blFunctionBtnAccess = true;
                return true;
            }

            PosDataObject.Security objSecurity = new PosDataObject.Security();
            objSecurity.Connection = new SqlConnection(SystemVariables.ConnectionString);
            int result = objSecurity.IsExistsPOSAccess(SystemVariables.CurrentUserID, scode);
            if (result == 0)
            {
                if (blAllowByAdmin)
                {
                    return true;
                }
                else
                {
                    bool bl2 = false;
                    blurGrid.Visibility = Visibility.Visible;
                    frm_POSLoginDlg frm_POSLoginDlg2 = new frm_POSLoginDlg();
                    try
                    {
                        frm_POSLoginDlg2.SecurityCode = scode;
                        frm_POSLoginDlg2.ShowDialog();
                        if (frm_POSLoginDlg2.DialogResult == true)
                        {
                            bl2 = true;
                            blAllowByAdmin = frm_POSLoginDlg2.AllowByAdmin;
                            intSuperUserID = frm_POSLoginDlg2.SuperUserID;
                        }
                    }
                    finally
                    {
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                    if (!bl2) return false;
                    else return true;
                }
            }
            else
            {
                blFunctionBtnAccess = true;
                return true;
            }
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
            int VOIDNO = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colVoidNo));

            string GA = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colGA);

            /*if (Settings.ReceiptPrinterName == "")
            {
                DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                return;
            }*/
            if (INV > 0)
            {
                if (rg1.IsChecked == true)
                {
                    bool blf = false;
                    if (VOIDNO > 0)
                        blf = true;
                    else
                        blf = false;
                    PrintInvoice(INV, blf, GA);
                }
                else
                {
                    if (Settings.PreprintedReceipt == "N")
                    {
                        UpdateReceiptCnt(INV);
                        blurGrid.Visibility = Visibility.Visible;
                        frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                        try
                        {
                            DataTable dtblLayawayForInvoice = new DataTable();
                            dtblLayawayForInvoice.Columns.Add("LAYAWAYNO", System.Type.GetType("System.String"));
                            dtblLayawayForInvoice.Rows.Add(new object[] {
                                        INV.ToString() });
                            frm_POSInvoicePrintDlg.LayawayDtbl = dtblLayawayForInvoice;
                            dtblLayawayForInvoice.Dispose();
                            frm_POSInvoicePrintDlg.LayTran = FetchMaxLayawayTranNo(INV);
                            frm_POSInvoicePrintDlg.PrintType = "Reprint Layaway";
                            frm_POSInvoicePrintDlg.InvNo = INV;
                            frm_POSInvoicePrintDlg.CashdrawerOpenFlag = false;
                            frm_POSInvoicePrintDlg.ShowDialog();
                        }
                        finally
                        {
                            blurGrid.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        DataTable dtblLayawayForInvoice = new DataTable();
                        dtblLayawayForInvoice.Columns.Add("LAYAWAYNO", System.Type.GetType("System.String"));
                        dtblLayawayForInvoice.Rows.Add(new object[] {
                                        INV.ToString() });

                        ExecuteLayawayPreprintedReport(dtblLayawayForInvoice, INV, FetchMaxLayawayTranNo(INV));
                    }
                }
            }
        }

        // Execute Layaway Preprint Report

        private void ExecuteLayawayPreprintedReport(DataTable dtblRef, int LyNo, int LyTran)
        {
            double tot = 0;
            double pmt = 0;
            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

            DataTable dtbl = new DataTable();
            PosDataObject.POS objSales = new PosDataObject.POS();
            objSales.Connection = SystemVariables.Conn;
            dtbl = objSales.FetchLayawayHeaderForPreprint(dtblRef, LyTran, true, Settings.StoreCode);

            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation(Properties.Resources.No_Record_found_for_Printing);
                dtbl.Dispose();
                return;
            }

            OfflineRetailV2.Report.Layaway.repLayawayPP rep_LayawayCust = new OfflineRetailV2.Report.Layaway.repLayawayPP();
            GeneralFunctions.MakeReportWatermark(rep_LayawayCust);
            rep_LayawayCust.rReportHeader.Text = Settings.ReportHeader;
            //rep_LayawayCust.DecimalPlace = Settings.DecimalPlace;

            DataTable p = new DataTable("Parent");
            p.Columns.Add("TransDate", System.Type.GetType("System.String"));
            p.Columns.Add("StoreID", System.Type.GetType("System.String"));
            p.Columns.Add("RegisterID", System.Type.GetType("System.String"));
            p.Columns.Add("EmpID", System.Type.GetType("System.String"));
            p.Columns.Add("CustID", System.Type.GetType("System.String"));
            p.Columns.Add("CustDetails", System.Type.GetType("System.String"));
            p.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            p.Columns.Add("LayawayNo", System.Type.GetType("System.Int32"));
            p.Columns.Add("SKU", System.Type.GetType("System.String"));
            p.Columns.Add("Description", System.Type.GetType("System.String"));
            p.Columns.Add("DiscountInfo", System.Type.GetType("System.String"));
            p.Columns.Add("Qty", System.Type.GetType("System.String"));
            p.Columns.Add("Price", System.Type.GetType("System.String"));
            p.Columns.Add("TotalSale", System.Type.GetType("System.String"));
            p.Columns.Add("DateDue", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl.Rows)
            {
                DataRow r1 = p.NewRow();
                r1["TransDate"] = dr["TransDate"].ToString();
                r1["StoreID"] = dr["StoreID"].ToString();
                r1["RegisterID"] = dr["RegisterID"].ToString();
                r1["EmpID"] = dr["EmpID"].ToString();
                r1["CustID"] = dr["CustID"].ToString();
                r1["CustDetails"] = dr["CustDetails"].ToString();
                r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                r1["LayawayNo"] = GeneralFunctions.fnInt32(dr["LayawayNo"].ToString());
                r1["SKU"] = dr["SKU"].ToString();
                r1["Description"] = dr["Description"].ToString();
                r1["DiscountInfo"] = dr["DiscountInfo"].ToString();
                r1["Qty"] = dr["Qty"].ToString();
                r1["Price"] = dr["Price"].ToString();
                r1["TotalSale"] = dr["TotalSale"].ToString();
                r1["DateDue"] = dr["DateDue"].ToString();
                p.Rows.Add(r1);

                tot = tot + GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
            }

            DataTable dtbl1 = new DataTable();
            PosDataObject.POS objProduct1 = new PosDataObject.POS();
            objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl1 = objProduct1.FetchLayawayPaymentPreprint(dtblRef, LyNo);

            foreach (DataRow dr2 in dtbl1.Rows)
            {
                pmt = pmt + GeneralFunctions.fnDouble(dr2["Payment"].ToString());
            }

            DataTable dtbl2 = new DataTable();
            PosDataObject.POS objProduct2 = new PosDataObject.POS();
            objProduct2.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl2 = objProduct2.FetchInvoiceTender(LyTran);

            dtbl2 = RearrangeTenderForCashBack(LyTran, dtbl2);

            string t1 = "";
            string t2 = "";
            foreach (DataRow dr3 in dtbl2.Rows)
            {
                t1 = (t1 == "") ? "Paid in " + dr3["DisplayAs"].ToString() : t1 + "\r\n" + "Paid in " + dr3["DisplayAs"].ToString();
                t2 = (t2 == "") ? SystemVariables.CurrencySymbol  + GeneralFunctions.fnDouble(dr3["Amount"].ToString()).ToString("f") : t2 + "\r\n" + SystemVariables.CurrencySymbol + GeneralFunctions.fnDouble(dr3["Amount"].ToString()).ToString("f");
            }
            DataTable c = new DataTable("Child");

            c.Columns.Add("TransDate", System.Type.GetType("System.String"));
            c.Columns.Add("StoreID", System.Type.GetType("System.String"));
            c.Columns.Add("RegisterID", System.Type.GetType("System.String"));
            c.Columns.Add("EmpID", System.Type.GetType("System.String"));
            c.Columns.Add("CustID", System.Type.GetType("System.String"));
            c.Columns.Add("CustDetails", System.Type.GetType("System.String"));
            c.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            c.Columns.Add("LayawayNo", System.Type.GetType("System.Int32"));
            c.Columns.Add("SKU", System.Type.GetType("System.String"));
            c.Columns.Add("Description", System.Type.GetType("System.String"));
            c.Columns.Add("DiscountInfo", System.Type.GetType("System.String"));
            c.Columns.Add("Qty", System.Type.GetType("System.String"));
            c.Columns.Add("Price", System.Type.GetType("System.String"));
            c.Columns.Add("TotalSale", System.Type.GetType("System.String"));

            c.Columns.Add("DateDue", System.Type.GetType("System.String"));
            c.Columns.Add("TransactionNo", System.Type.GetType("System.String"));
            c.Columns.Add("PaymentDateTime", System.Type.GetType("System.String"));
            c.Columns.Add("PaymentType", System.Type.GetType("System.String"));
            c.Columns.Add("PaymentDate", System.Type.GetType("System.String"));
            c.Columns.Add("Payment", System.Type.GetType("System.String"));
            c.Columns.Add("BalanceDue", System.Type.GetType("System.String"));
            c.Columns.Add("Tender1", System.Type.GetType("System.String"));
            c.Columns.Add("Tender2", System.Type.GetType("System.String"));

            foreach (DataRow dr in dtbl1.Rows)
            {
                double crgamt = 0;

                DataRow r1 = c.NewRow();
                string a1 = "", a2 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "", a12 = "", a13 = "",
                    a14 = "", a15 = "", a16 = "", a17 = "", a18 = "";
                int a3 = 0;
                foreach (DataRow dr1 in p.Rows)
                {
                    if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                    {
                        a11 = dr1["TransDate"].ToString();
                        a12 = dr1["StoreID"].ToString();
                        a13 = dr1["RegisterID"].ToString();
                        a14 = dr1["EmpID"].ToString();
                        a15 = dr1["DiscountInfo"].ToString();
                        a16 = dr1["Qty"].ToString();
                        a17 = dr1["Price"].ToString();
                        a1 = dr1["CustID"].ToString();
                        a2 = dr1["CustDetails"].ToString();
                        a3 = GeneralFunctions.fnInt32(dr1["LayawayNo"].ToString());
                        a4 = dr1["SKU"].ToString();
                        a5 = dr1["Description"].ToString();
                        a6 = dr1["TotalSale"].ToString();
                        a7 = dr1["DateDue"].ToString();
                        break;
                    }
                }

                r1["TransDate"] = a11;
                r1["StoreID"] = a12;
                r1["RegisterID"] = a13;
                r1["EmpID"] = a14;
                r1["DiscountInfo"] = a15;
                r1["Qty"] = a16;
                r1["Price"] = a17;
                r1["CustID"] = a1;
                r1["CustDetails"] = a2;
                r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                r1["LayawayNo"] = a3;
                r1["SKU"] = a4;
                r1["Description"] = a5;
                r1["TotalSale"] = a6;
                r1["DateDue"] = a7;
                r1["TransactionNo"] = dr["TransactionNo"].ToString();
                r1["PaymentDateTime"] = dr["PaymentDate"].ToString();
                r1["PaymentType"] = dr["PaymentType"].ToString();
                r1["PaymentDate"] = GeneralFunctions.fnDate(dr["PaymentDate"].ToString()).ToString(SystemVariables.DateFormat);
                r1["Payment"] = dr["Payment"].ToString();
                r1["BalanceDue"] = (tot - pmt).ToString();
                r1["Tender1"] = t1;
                r1["Tender2"] = t2;
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
            GeneralFunctions.MakeReportWatermark(rep_LayawayCust);
            rep_LayawayCust.rReportHeader.Text = Settings.MainReceiptHeader;
            rep_LayawayCust.GroupHeader2.GroupFields.Add(rep_LayawayCust.CreateGroupField("CustID"));
            rep_LayawayCust.GroupHeader1.GroupFields.Add(rep_LayawayCust.CreateGroupField("LayawayNo"));
            rep_LayawayCust.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            //rep_LayawayCust.GroupHeader3.GroupFields.Add(rep_LayawayCust.CreateGroupField("PaymentDateTime"));

            rep_LayawayCust.DataSource = ds;

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

        // Printing Function

        private void PrintInvoice(int intINV, bool blVoid, string GiftAid)
        {
            UpdateReceiptCnt(intINV);
            if (Settings.GeneralReceiptPrint == "N")
            {
                blurGrid.Visibility = Visibility.Visible;
                frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.PrintType = (GiftAid == "N") ? "Reprint Receipt" : "Gift Aid Receipt";
                    frm_POSInvoicePrintDlg.InvNo = intINV;
                    frm_POSInvoicePrintDlg.ReprintCnt = GetReceiptCnt(intINV);
                    frm_POSInvoicePrintDlg.IsVoid = blVoid;
                    frm_POSInvoicePrintDlg.ShowDialog();
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (GiftAid == "N")
                {
                    if (Settings.PreprintedReceipt == "N")
                    {
                        DataTable dtbl = new DataTable();
                        PosDataObject.POS objPOS1 = new PosDataObject.POS();
                        objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl = objPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                        DataTable dlogo = new DataTable();
                        objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dlogo = objPOS1.FetchStoreLogo();
                        bool boolnulllogo = false;
                        foreach (DataRow drl1 in dtbl.Rows)
                        {
                            foreach (DataRow drl2 in dlogo.Rows)
                            {
                                if (drl2["logo"] == null) boolnulllogo = true;
                                drl1["Logo"] = drl2["logo"];
                            }
                        }

                        int intTranNo = 0;
                        double dblOrderTotal = 0;
                        double dblOrderSubtotal = 0;
                        double dblDiscount = 0;
                        double dblCoupon = 0;
                        double dblTax = 0;
                        double dblSurcharge = 0;
                        int intCID = 0;
                        string strDiscountReason = "";
                        double dblTax1 = 0;
                        double dblTax2 = 0;
                        double dblTax3 = 0;
                        string strTaxNM1 = "";
                        string strTaxNM2 = "";
                        string strTaxNM3 = "";

                        double dblFees = 0;
                        double dblFeesTax = 0;

                        double dblFeesCoupon = 0;
                        double dblFeesCouponTax = 0;

                        string strDTaxNM = "";
                        double dblDTax = 0;

                        int intHeaderStatus = 0;
                        string strCustomerDOB = "";

                        foreach (DataRow dr in dtbl.Rows)
                        {
                            intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                            intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                            dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                            dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                            dblCoupon = GeneralFunctions.fnDouble(dr["Coupon"].ToString());
                            dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                            dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                            dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                            dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                            strTaxNM1 = dr["TaxNM1"].ToString();
                            strTaxNM2 = dr["TaxNM2"].ToString();
                            strTaxNM3 = dr["TaxNM3"].ToString();
                            dblFees = GeneralFunctions.fnDouble(dr["Fees"].ToString());
                            dblFeesTax = GeneralFunctions.fnDouble(dr["FeesTax"].ToString());
                            dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                            dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());
                            dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                            dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());
                            strDiscountReason = dr["DiscountReason"].ToString();
                            strDTaxNM = dr["DTaxName"].ToString();
                            dblDTax = GeneralFunctions.fnDouble(dr["DTax"].ToString());

                            intHeaderStatus = GeneralFunctions.fnInt32(dr["Status"].ToString());
                            if (Settings.POSIDRequired == "Y") strCustomerDOB = dr["CustomerDOB"].ToString();
                        }

                        blCardPayment = IsCardPayment(intTranNo);
                        blHouseAccountPayment = IsHAPayment(intTranNo);

                        if (blCardPayment)
                        {
                            if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 2))
                            {
                                bool b1 = IsMercuryCardPayment(intTranNo);
                                if (b1)
                                {
                                    double amt = GetMercuryCardPaymentAmount(intTranNo);
                                    if (amt < Settings.MercurySignAmount) MercuryCardPaymentCheck = true;
                                }
                            }
                        }

                        DataTable dtbl1 = new DataTable();
                        DataTable dtbl2 = new DataTable();
                        DataTable dtbl3 = new DataTable();
                        DataTable dtbl4 = new DataTable();
                        DataTable dtbl5 = new DataTable();
                        OfflineRetailV2.Report.Sales.repInvMain rep_InvMain = new OfflineRetailV2.Report.Sales.repInvMain();
                        OfflineRetailV2.Report.Sales.repInvHeader1 rep_InvHeader1 = new OfflineRetailV2.Report.Sales.repInvHeader1();
                        OfflineRetailV2.Report.Sales.repInvHeader2 rep_InvHeader2 = new OfflineRetailV2.Report.Sales.repInvHeader2();
                        OfflineRetailV2.Report.Sales.repInvLine rep_InvLine = new OfflineRetailV2.Report.Sales.repInvLine();
                        OfflineRetailV2.Report.Sales.repInvSubtotal rep_InvSubtotal = new OfflineRetailV2.Report.Sales.repInvSubtotal();
                        OfflineRetailV2.Report.Sales.repInvTax rep_InvTax = new OfflineRetailV2.Report.Sales.repInvTax();
                        OfflineRetailV2.Report.Sales.repPPInvTendering rep_InvTendering = new OfflineRetailV2.Report.Sales.repPPInvTendering();
                        OfflineRetailV2.Report.Sales.repInvGC rep_InvGC = new OfflineRetailV2.Report.Sales.repInvGC();
                        OfflineRetailV2.Report.Sales.repInvHA rep_InvHA = new OfflineRetailV2.Report.Sales.repInvHA();
                        OfflineRetailV2.Report.Sales.repInvSC rep_InvSC = new OfflineRetailV2.Report.Sales.repInvSC();
                        OfflineRetailV2.Report.Sales.repInvMGC rep_InvMGC = new OfflineRetailV2.Report.Sales.repInvMGC();
                        OfflineRetailV2.Report.Sales.repInvCoupon rep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();

                        if (blVoid)
                            rep_InvMain.rReprint.Text = "**** Reprinted Void Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                        else
                            rep_InvMain.rReprint.Text = "**** Reprinted Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";

                        if (Settings.ReceiptFooter == "")
                        {
                            rep_InvMain.rReportFooter.HeightF = 1.0f;
                            rep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 22);
                            rep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 25);
                            rep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 25);

                            rep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 45);
                            rep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 45);
                            rep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 45);
                            rep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 45);

                            rep_InvMain.ReportFooter.Height = 80;
                            rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                        }
                        else
                        {
                            rep_InvMain.ReportFooter.Height = 91;
                            rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                        }
                        if (blCardPayment)
                        {
                            //rep_InvMain.rsign1.Visible = true;
                            //rep_InvMain.rsign2.Visible = true;
                        }
                        rep_InvMain.subrepH1.ReportSource = rep_InvHeader1;
                        rep_InvHeader1.Report.DataSource = dtbl;
                        rep_InvHeader1.rReprint.Text = "";
                        GeneralFunctions.MakeReportWatermark(rep_InvMain);
                        rep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                        rep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;

                        int WO = FetchWorkorderNo(intINV);
                        if (WO != 0)
                        {
                            rep_InvHeader1.rType.Text = "Work Order # " + WO.ToString() + "     Date : " + GeneralFunctions.fnDate(FetchWorkorderDate(intINV)).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
                        }
                        else
                        {
                            rep_InvHeader1.rType.Text = "";
                        }
                        rep_InvHeader1.rOrderNo.Text = intINV.ToString();
                        if (Settings.PrintLogoInReceipt == "Y")
                        {
                            if (!boolnulllogo) rep_InvHeader1.rPic.DataBindings.Add("Image", dtbl, "Logo");
                        }
                        rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "TransDate");

                        rep_InvHeader1.rTraining.Visible = Settings.PrintTrainingMode == "Y";
                        if (intHeaderStatus == 3) rep_InvHeader1.rRefundCaption.Visible = dblOrderTotal < 0;
                        rep_InvMain.xrBarCode.Text = intINV.ToString();

                        if (intCID > 0)
                        {
                            rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                            rep_InvHeader2.Report.DataSource = dtbl;
                            rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                            rep_InvHeader2.rCustName.DataBindings.Add("Text", dtbl, "CustName");
                            rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "CustCompany");

                            if (intHeaderStatus == 3)
                            {
                                if (strCustomerDOB != "")
                                {
                                    rep_InvHeader2.rlCustDOB.Text = Properties.Resources.Date_of_Birth;
                                    rep_InvHeader2.rCustDOB.Text = GeneralFunctions.fnDate(strCustomerDOB).ToString(SystemVariables.DateFormat);
                                }
                            }
                        }
                        else
                        {
                            if (intHeaderStatus == 3)
                            {
                                if (strCustomerDOB != "")
                                {
                                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                                    rep_InvHeader2.Report.DataSource = dtbl;
                                    rep_InvHeader2.rCustName.Text = "";
                                    rep_InvHeader2.rCustID.Text = "";
                                    rep_InvHeader2.rCompany.Text = "";
                                    rep_InvHeader2.rlCustName.Text = "";
                                    rep_InvHeader2.rlCustID.Text = "";
                                    rep_InvHeader2.rlCompany.Text = "";

                                    if (intHeaderStatus == 3)
                                    {
                                        if (strCustomerDOB != "")
                                        {
                                            rep_InvHeader2.rlCustDOB.Text = Properties.Resources.Date_of_Birth;
                                            rep_InvHeader2.rCustDOB.Text = GeneralFunctions.fnDate(strCustomerDOB).ToString(SystemVariables.DateFormat);
                                        }
                                    }
                                }
                            }
                        }

                        PosDataObject.POS objPOS2 = new PosDataObject.POS();
                        objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl1 = objPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                        RearrangeForTaggedItemInInvoice(dtbl1);
                        RearrangeForLineDisplay(dtbl1);
                        rep_InvMain.subrepLine.ReportSource = rep_InvLine;
                        rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvLine.Report.DataSource = dtbl1;
                        rep_InvLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvLine.rlSKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        rep_InvLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                        rep_InvLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                        rep_InvLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                        if (Settings.TaxInclusive == "N")
                        {
                            rep_InvLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                        }
                        else
                        {
                            rep_InvLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "GRate");
                        }
                        rep_InvLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");
                        rep_InvLine.rManualWeight.DataBindings.Add("Text", dtbl1, "ExtraValue1");

                        if (Settings.ShowFeesInReceipt == "Y")
                        {
                            rep_InvLine.rFeesTxt.Visible = true;
                            rep_InvLine.rFeesTxt.DataBindings.Add("Text", dtbl1, "FeesText");
                        }
                        else
                        {
                            rep_InvLine.rFeesTxt.Visible = false;
                        }
                        foreach (DataRow dr12 in dtbl1.Rows)
                        {
                            //if (Settings.TaxInclusive == "N")
                            // {
                            dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                            //}
                        }

                        //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

                        rep_InvMain.subrepSubtotal.ReportSource = rep_InvSubtotal;
                        rep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                        rep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                        rep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                        rep_InvSubtotal.DR = strDiscountReason;
                        rep_InvSubtotal.rTax.Text = dblTax.ToString();

                        if (dblTax != 0)
                        {
                            dtbl2.Columns.Add("Name", System.Type.GetType("System.String"));
                            dtbl2.Columns.Add("Amount", System.Type.GetType("System.String"));

                            if (dblTax1 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM1, dblTax1.ToString() });
                            }

                            if (dblTax2 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM2, dblTax2.ToString() });
                            }

                            if (dblTax3 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM3, dblTax3.ToString() });
                            }

                            if (dblDTax != 0)
                            {
                                dtbl2.Rows.Add(new object[] { "Dest. Tax : " + strDTaxNM, dblDTax.ToString() });
                            }

                            rep_InvMain.subrepTax.ReportSource = rep_InvTax;
                            rep_InvTax.DecimalPlace = Settings.DecimalPlace;

                            rep_InvTax.Report.DataSource = dtbl2;
                            rep_InvTax.rDTax1.DataBindings.Add("Text", dtbl2, "Name");
                            rep_InvTax.rDTax2.DataBindings.Add("Text", dtbl2, "Amount");
                        }

                        PosDataObject.POS objPOS23 = new PosDataObject.POS();
                        objPOS23.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl5 = objPOS23.FetchInvoiceCoupons(intINV);
                        if (dtbl5.Rows.Count > 0)
                        {
                            rep_InvMain.subrepCoupon.ReportSource = rep_InvCoupon;
                            rep_InvCoupon.DecimalPlace = Settings.DecimalPlace;
                            rep_InvCoupon.Report.DataSource = dtbl5;
                            rep_InvCoupon.rAmt.Text = dblCoupon.ToString();
                            rep_InvCoupon.rDTax1.DataBindings.Add("Text", dtbl5, "Name");
                            rep_InvCoupon.rDTax2.DataBindings.Add("Text", dtbl5, "Amount");
                        }

                        PosDataObject.POS objPOS4 = new PosDataObject.POS();
                        objPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl3 = objPOS4.FetchInvoiceTender(intTranNo);
                        dtbl3 = RearrangeTenderForCashBack(intTranNo, dtbl3);

                        double dblTenderAmt = 0;
                        bool boolHATender = false;
                        bool boolSCrdtTender = false;

                        foreach (DataRow dr1 in dtbl3.Rows)
                        {
                            if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                            dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());

                            if (dr1["Name"].ToString() == "House Account") boolHATender = true;
                            if (dr1["Name"].ToString() == "Store Credit") boolSCrdtTender = true;
                        }

                        rep_InvMain.subrepTender.ReportSource = rep_InvTendering;
                        rep_InvTendering.Report.DataSource = dtbl3;
                        rep_InvTendering.DecimalPlace = Settings.DecimalPlace;

                        if (boolHATender && (Settings.HouseAccountBalanceInReceipt == "Y"))
                        {
                            PosDataObject.POS obcc99 = new PosDataObject.POS();
                            obcc99.Connection = SystemVariables.Conn;
                            double dval = obcc99.FetchHouseAccountBalanceForThisReceipt(intINV, intCID);
                            rep_InvMain.subrepHA.ReportSource = rep_InvHA;
                            rep_InvHA.DecimalPlace = Settings.DecimalPlace;
                            rep_InvHA.rAmt.Text = dval.ToString();
                        }

                        if (boolSCrdtTender) // Print Store Credit Balance
                        {
                            PosDataObject.POS objscrtbal = new PosDataObject.POS();
                            objscrtbal.Connection = SystemVariables.Conn;
                            double dval = objscrtbal.GetCustomerStoreCreditBalance(intCID);
                            rep_InvMain.subrepSCrdt.ReportSource = rep_InvSC;
                            rep_InvSC.DecimalPlace = Settings.DecimalPlace;
                            rep_InvSC.rAmt.Text = dval.ToString();
                        }

                        if (Settings.ShowFeesInReceipt == "Y")
                        {
                            bool bfdata = false;
                            bool bftx = false;
                            DataTable dFees = FetchInvFees(intINV);
                            if (dblFees + dblFeesCoupon != 0)
                            {
                                if (dFees.Rows.Count == 1) rep_InvTendering.lbFees.Text = dFees.Rows[0]["FeesName"].ToString();
                                rep_InvTendering.rFees.Text = (dblFees + dblFeesCoupon).ToString();
                                rep_InvTendering.rFees.Visible = true;
                                rep_InvTendering.lbFees.Visible = true;
                            }
                            else
                            {
                                bfdata = true;
                            }
                            if (dblFeesTax + dblFeesCouponTax != 0)
                            {
                                if (dFees.Rows.Count == 1) rep_InvTendering.lbFeeTx.Text = dFees.Rows[0]["FeesName"].ToString() + " " + "Tax";
                                rep_InvTendering.rFeeTx.Text = (dblFeesTax + dblFeesCouponTax).ToString();
                                rep_InvTendering.rFeeTx.Visible = true;
                                rep_InvTendering.lbFeeTx.Visible = true;
                            }
                            else
                            {
                                bftx = true;
                            }
                            if ((bfdata) && (bftx))
                            {
                                rep_InvTendering.ReportHeader.Visible = false;
                            }
                        }

                        rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                        rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                        rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");


                        rep_InvTendering.rlbAdvance.Text = "";
                        rep_InvTendering.rAdvance.Text = "";
                        rep_InvTendering.rlbDue.Text = "";
                        rep_InvTendering.rDue.Text = "";

                        rep_InvTendering.rtr1.HeightF = 1.0f;
                        rep_InvTendering.rtr2.HeightF = 1.0f;
                        rep_InvTendering.rtbl.HeightF = 55.0f;
                        rep_InvTendering.PageHeader.HeightF = 55.0f;

                        if (dblTenderAmt != dblOrderTotal)
                        {
                            rep_InvTendering.ChangeDue = true;
                            rep_InvTendering.ReportFooter.Visible = true;
                            rep_InvTendering.rChangeDueText.Text = "Change";
                            rep_InvTendering.rChangeDue.Text = Convert.ToString(dblTenderAmt - dblOrderTotal);
                        }
                        else
                        {
                            rep_InvTendering.ChangeDue = false;
                            rep_InvTendering.ReportFooter.Visible = false;
                        }

                        if (Settings.POSShowGiftCertBalance == "Y")
                        {
                            PosDataObject.POS objPOS5 = new PosDataObject.POS();
                            objPOS5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            dtbl4 = objPOS5.ActiveGiftCert(intINV, Settings.CentralExportImport, Settings.StoreCode);
                            if (dtbl4.Rows.Count > 0)
                            {
                                rep_InvMain.subrepGC.ReportSource = rep_InvGC;
                                rep_InvGC.Report.DataSource = dtbl4;
                                rep_InvGC.DecimalPlace = Settings.DecimalPlace;
                                rep_InvGC.rGCHeader.Text = "Gift Cert. with balance as on : " + DateTime.Today.Date.ToShortDateString();
                                rep_InvGC.rGCName.DataBindings.Add("Text", dtbl4, "GC");
                                rep_InvGC.rGCAmt.DataBindings.Add("Text", dtbl4, "GCAMT");
                            }
                        }

                        // EBT Balance on Receipt

                        PosDataObject.POS objPOS87 = new PosDataObject.POS();
                        objPOS87.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        DataTable dtblEBT = objPOS87.FetchEBTBalanceFromReceipt(intINV);
                        if (dtblEBT.Rows.Count > 0)
                        {
                            OfflineRetailV2.Report.Sales.repInvEBT rep_InvEBT = new OfflineRetailV2.Report.Sales.repInvEBT();
                            rep_InvMain.subrepEBT.ReportSource = rep_InvEBT;
                            rep_InvEBT.Report.DataSource = dtblEBT;
                            rep_InvEBT.DecimalPlace = Settings.DecimalPlace;

                            rep_InvEBT.rEBTCard.DataBindings.Add("Text", dtblEBT, "CardNo");
                            rep_InvEBT.rEBTBal.DataBindings.Add("Text", dtblEBT, "CardBalance");
                        }

                        int prmmgc = 0;
                        PosDataObject.POS obcc01mgc = new PosDataObject.POS();
                        obcc01mgc.Connection = SystemVariables.Conn;
                        prmmgc = obcc01mgc.GetTranIDFromInvoiceID(intINV);
                        DataTable ccdtbl11mgc = new DataTable();
                        PosDataObject.POS obcc11mgc = new PosDataObject.POS();
                        obcc11mgc.Connection = SystemVariables.Conn;
                        ccdtbl11mgc = obcc11mgc.FetchMercuryGiftCardData(prmmgc);

                        if (ccdtbl11mgc.Rows.Count > 0)
                        {
                            rep_InvMain.subrepMGC.ReportSource = rep_InvMGC;
                            rep_InvMGC.Report.DataSource = ccdtbl11mgc;
                            rep_InvMGC.DecimalPlace = Settings.DecimalPlace;
                            rep_InvMGC.rGCName.DataBindings.Add("Text", ccdtbl11mgc, "RefCardAct");
                            rep_InvMGC.rGCAmt.DataBindings.Add("Text", ccdtbl11mgc, "RefCardBalance");
                        }

                        //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                        try
                        {
                            if (Settings.ReportPrinterName != "") rep_InvMain.PrinterName = Settings.ReportPrinterName;
                            rep_InvMain.CreateDocument();
                            rep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                            rep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_InvMain.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_InvMain;
                            window.ShowDialog();
                        }
                        finally
                        {
                            rep_InvHeader1.Dispose();
                            rep_InvHeader2.Dispose();
                            rep_InvLine.Dispose();
                            rep_InvSubtotal.Dispose();
                            rep_InvTax.Dispose();
                            rep_InvTendering.Dispose();
                            rep_InvGC.Dispose();
                            rep_InvMGC.Dispose();
                            rep_InvCoupon.Dispose();
                            rep_InvMain.Dispose();

                            dtbl.Dispose();
                            dtbl1.Dispose();
                            dtbl2.Dispose();
                            dtbl3.Dispose();
                            dtbl4.Dispose();
                            dtbl5.Dispose();
                            ccdtbl11mgc.Dispose();
                        }

                        if (((blCardPayment) && (Settings.IsDuplicateInvoice == "Y") && (!MercuryCardPaymentCheck)) || (IsGCSales(intINV)))
                        {
                            DataTable ddtbl = new DataTable();
                            PosDataObject.POS dobjPOS1 = new PosDataObject.POS();
                            dobjPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl = dobjPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                            dlogo = new DataTable();
                            objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            dlogo = objPOS1.FetchStoreLogo();
                            boolnulllogo = false;
                            foreach (DataRow drl1 in ddtbl.Rows)
                            {
                                foreach (DataRow drl2 in dlogo.Rows)
                                {
                                    if (drl2["logo"] == null) boolnulllogo = true;
                                    drl1["Logo"] = drl2["logo"];
                                }
                            }

                            intTranNo = 0;
                            dblOrderTotal = 0;
                            dblOrderSubtotal = 0;
                            dblDiscount = 0;
                            dblCoupon = 0;
                            dblTax = 0;
                            dblSurcharge = 0;
                            intCID = 0;
                            strDiscountReason = "";
                            dblTax1 = 0;
                            dblTax2 = 0;
                            dblTax3 = 0;
                            strTaxNM1 = "";
                            strTaxNM2 = "";
                            strTaxNM3 = "";
                            dblFees = 0;
                            dblFeesTax = 0;

                            dblFeesCoupon = 0;
                            dblFeesCouponTax = 0;

                            strDTaxNM = "";
                            dblDTax = 0;

                            foreach (DataRow dr in ddtbl.Rows)
                            {
                                intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                                intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                                dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                                dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                                dblCoupon = GeneralFunctions.fnDouble(dr["Coupon"].ToString());
                                dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                                dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                                dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                                dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                                strTaxNM1 = dr["TaxNM1"].ToString();
                                strTaxNM2 = dr["TaxNM2"].ToString();
                                strTaxNM3 = dr["TaxNM3"].ToString();
                                dblFees = GeneralFunctions.fnDouble(dr["Fees"].ToString());
                                dblFeesTax = GeneralFunctions.fnDouble(dr["FeesTax"].ToString());

                                dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                                dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());

                                strDiscountReason = dr["DiscountReason"].ToString();
                                strDTaxNM = dr["DTaxName"].ToString();
                                dblDTax = GeneralFunctions.fnDouble(dr["DTax"].ToString());
                            }

                            DataTable ddtbl1 = new DataTable();
                            DataTable ddtbl2 = new DataTable();
                            DataTable ddtbl3 = new DataTable();
                            DataTable ddtbl4 = new DataTable();
                            DataTable ddtbl5 = new DataTable();
                            OfflineRetailV2.Report.Sales.repInvMain drep_InvMain = new OfflineRetailV2.Report.Sales.repInvMain();
                            OfflineRetailV2.Report.Sales.repInvHeader1 drep_InvHeader1 = new OfflineRetailV2.Report.Sales.repInvHeader1();
                            OfflineRetailV2.Report.Sales.repInvHeader2 drep_InvHeader2 = new OfflineRetailV2.Report.Sales.repInvHeader2();
                            OfflineRetailV2.Report.Sales.repInvLine drep_InvLine = new OfflineRetailV2.Report.Sales.repInvLine();
                            OfflineRetailV2.Report.Sales.repInvSubtotal drep_InvSubtotal = new OfflineRetailV2.Report.Sales.repInvSubtotal();
                            OfflineRetailV2.Report.Sales.repInvTax drep_InvTax = new OfflineRetailV2.Report.Sales.repInvTax();
                            OfflineRetailV2.Report.Sales.repPPInvTendering drep_InvTendering = new OfflineRetailV2.Report.Sales.repPPInvTendering();
                            OfflineRetailV2.Report.Sales.repInvGC drep_InvGC = new OfflineRetailV2.Report.Sales.repInvGC();
                            OfflineRetailV2.Report.Sales.repInvHA drep_InvHA = new OfflineRetailV2.Report.Sales.repInvHA();
                            OfflineRetailV2.Report.Sales.repInvSC drep_InvSC = new OfflineRetailV2.Report.Sales.repInvSC();
                            OfflineRetailV2.Report.Sales.repInvMGC drep_InvMGC = new OfflineRetailV2.Report.Sales.repInvMGC();
                            OfflineRetailV2.Report.Sales.repInvCoupon drep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();
                            if (blVoid)
                                drep_InvMain.rReprint.Text = "**** Reprinted Void Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                            else
                                drep_InvMain.rReprint.Text = "**** Reprinted Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                            if (Settings.ReceiptFooter == "")
                            {
                                drep_InvMain.rReportFooter.HeightF = 1.0f;
                                drep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 2);
                                drep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 5);
                                drep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 5);

                                drep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 25);
                                drep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 25);
                                drep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 25);
                                drep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 25);

                                drep_InvMain.ReportFooter.Height = 60;
                                drep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                            }
                            else
                            {
                                drep_InvMain.ReportFooter.Height = 91;
                                drep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                            }

                            drep_InvMain.subrepH1.ReportSource = drep_InvHeader1;
                            drep_InvHeader1.Report.DataSource = ddtbl;
                            drep_InvHeader1.rReprint.Text = "";
                            GeneralFunctions.MakeReportWatermark(drep_InvMain);
                            drep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                            drep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;

                            WO = FetchWorkorderNo(intINV);
                            if (WO != 0)
                            {
                                drep_InvHeader1.rType.Text = "Work Order # " + WO.ToString() + "     Date : " + GeneralFunctions.fnDate(FetchWorkorderDate(intINV)).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
                            }
                            else
                            {
                                drep_InvHeader1.rType.Text = "";
                            }
                            drep_InvHeader1.rOrderNo.Text = intINV.ToString();
                            if (Settings.PrintLogoInReceipt == "Y")
                            {
                                if (!boolnulllogo) drep_InvHeader1.rPic.DataBindings.Add("Image", ddtbl, "Logo");
                            }
                            drep_InvHeader1.rOrderDate.DataBindings.Add("Text", ddtbl, "TransDate");
                            drep_InvHeader1.rTraining.Visible = Settings.PrintTrainingMode == "Y";
                            if (intHeaderStatus == 3) drep_InvHeader1.rRefundCaption.Visible = dblOrderTotal < 0;
                            drep_InvMain.xrBarCode.Text = intINV.ToString();

                            if (intCID > 0)
                            {
                                drep_InvMain.subrepH2.ReportSource = drep_InvHeader2;
                                drep_InvHeader2.Report.DataSource = ddtbl;
                                drep_InvHeader2.rCustID.DataBindings.Add("Text", ddtbl, "CustID");
                                drep_InvHeader2.rCustName.DataBindings.Add("Text", ddtbl, "CustName");
                                drep_InvHeader2.rCompany.DataBindings.Add("Text", ddtbl, "CustCompany");
                            }
                            else
                            {
                                drep_InvMain.subrepH2.ReportSource = drep_InvHeader2;
                                drep_InvHeader2.Report.DataSource = ddtbl;
                                drep_InvHeader2.rCustName.Text = "";
                                drep_InvHeader2.rCustID.Text = "";
                                drep_InvHeader2.rCompany.Text = "";
                                drep_InvHeader2.rlCustName.Text = "";
                                drep_InvHeader2.rlCustID.Text = "";
                                drep_InvHeader2.rlCompany.Text = "";
                            }

                            PosDataObject.POS dobjPOS2 = new PosDataObject.POS();
                            dobjPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl1 = dobjPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                            RearrangeForTaggedItemInInvoice(ddtbl1);
                            RearrangeForLineDisplay(ddtbl1);
                            drep_InvMain.subrepLine.ReportSource = drep_InvLine;
                            drep_InvLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvLine.Report.DataSource = dtbl1;
                            drep_InvLine.rlqty.DataBindings.Add("Text", ddtbl1, "Qty");
                            drep_InvLine.rlSKU.DataBindings.Add("Text", ddtbl1, "SKU");
                            drep_InvLine.rlIem.DataBindings.Add("Text", ddtbl1, "Description");
                            drep_InvLine.rDiscTxt.DataBindings.Add("Text", ddtbl1, "DiscountText");
                            drep_InvLine.rlPrice.DataBindings.Add("Text", ddtbl1, "NormalPrice");
                            drep_InvLine.rlDiscount.DataBindings.Add("Text", ddtbl1, "Discount");
                            drep_InvLine.rlSurcharge.DataBindings.Add("Text", ddtbl1, "Price");
                            drep_InvLine.rlTotal.DataBindings.Add("Text", ddtbl1, "TotalPrice");
                            drep_InvLine.rManualWeight.DataBindings.Add("Text", ddtbl1, "ExtraValue1");
                            if (Settings.ShowFeesInReceipt == "Y")
                            {
                                drep_InvLine.rFeesTxt.Visible = true;
                                drep_InvLine.rFeesTxt.DataBindings.Add("Text", ddtbl1, "FeesText");
                            }
                            else
                            {
                                drep_InvLine.rFeesTxt.Visible = false;
                            }
                            foreach (DataRow dr12 in ddtbl1.Rows)
                            {
                                dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                            }

                            drep_InvMain.subrepSubtotal.ReportSource = drep_InvSubtotal;
                            drep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                            drep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                            drep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                            drep_InvSubtotal.DR = strDiscountReason;
                            drep_InvSubtotal.rTax.Text = dblTax.ToString();

                            if (dblTax != 0)
                            {
                                ddtbl2.Columns.Add("Name", System.Type.GetType("System.String"));
                                ddtbl2.Columns.Add("Amount", System.Type.GetType("System.String"));

                                if (dblTax1 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM1, dblTax1.ToString() });
                                }

                                if (dblTax2 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM2, dblTax2.ToString() });
                                }

                                if (dblTax3 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM3, dblTax3.ToString() });
                                }

                                if (dblDTax != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { "Dest. Tax : " + strDTaxNM, dblDTax.ToString() });
                                }

                                drep_InvMain.subrepTax.ReportSource = drep_InvTax;
                                drep_InvTax.DecimalPlace = Settings.DecimalPlace;

                                drep_InvTax.Report.DataSource = ddtbl2;
                                drep_InvTax.rDTax1.DataBindings.Add("Text", ddtbl2, "Name");
                                drep_InvTax.rDTax2.DataBindings.Add("Text", ddtbl2, "Amount");
                            }

                            PosDataObject.POS dobjPOS23 = new PosDataObject.POS();
                            dobjPOS23.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl5 = dobjPOS23.FetchInvoiceCoupons(intINV);
                            if (ddtbl5.Rows.Count > 0)
                            {
                                drep_InvMain.subrepCoupon.ReportSource = drep_InvCoupon;
                                drep_InvCoupon.DecimalPlace = Settings.DecimalPlace;
                                drep_InvCoupon.Report.DataSource = dtbl5;
                                drep_InvCoupon.rAmt.Text = dblCoupon.ToString();
                                drep_InvCoupon.rDTax1.DataBindings.Add("Text", ddtbl5, "Name");
                                drep_InvCoupon.rDTax2.DataBindings.Add("Text", ddtbl5, "Amount");
                            }

                            PosDataObject.POS dobjPOS4 = new PosDataObject.POS();
                            dobjPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl3 = dobjPOS4.FetchInvoiceTender(intTranNo);

                            ddtbl3 = RearrangeTenderForCashBack(intTranNo, ddtbl3);

                            dblTenderAmt = 0;
                            boolHATender = false;
                            boolSCrdtTender = false;

                            foreach (DataRow dr1 in ddtbl3.Rows)
                            {
                                if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                                dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());

                                if (dr1["Name"].ToString() == "House Account") boolHATender = true;
                                if (dr1["Name"].ToString() == "Store Credit") boolSCrdtTender = true;
                            }

                            drep_InvMain.subrepTender.ReportSource = drep_InvTendering;
                            drep_InvTendering.Report.DataSource = ddtbl3;
                            drep_InvTendering.DecimalPlace = Settings.DecimalPlace;

                            if (boolHATender && (Settings.HouseAccountBalanceInReceipt == "Y"))
                            {
                                PosDataObject.POS obcc99 = new PosDataObject.POS();
                                obcc99.Connection = SystemVariables.Conn;
                                double dval = obcc99.FetchHouseAccountBalanceForThisReceipt(intINV, intCID);
                                drep_InvMain.subrepHA.ReportSource = drep_InvHA;
                                drep_InvHA.DecimalPlace = Settings.DecimalPlace;
                                drep_InvHA.rAmt.Text = dval.ToString();
                            }

                            if (boolSCrdtTender) // Print Store Credit Balance
                            {
                                PosDataObject.POS objscrtbal1 = new PosDataObject.POS();
                                objscrtbal1.Connection = SystemVariables.Conn;
                                double dval = objscrtbal1.GetCustomerStoreCreditBalance(intCID);
                                drep_InvMain.subrepSCrdt.ReportSource = drep_InvSC;
                                drep_InvSC.DecimalPlace = Settings.DecimalPlace;
                                drep_InvSC.rAmt.Text = dval.ToString();
                            }

                            if (Settings.ShowFeesInReceipt == "Y")
                            {
                                bool bfdata = false;
                                bool bftx = false;
                                DataTable dFees = FetchInvFees(intINV);

                                if (dblFees + dblFeesCoupon != 0)
                                {
                                    if (dFees.Rows.Count == 1) drep_InvTendering.lbFees.Text = dFees.Rows[0]["FeesName"].ToString();
                                    drep_InvTendering.rFees.Text = (dblFees + dblFeesCoupon).ToString();
                                    drep_InvTendering.rFees.Visible = true;
                                    drep_InvTendering.lbFees.Visible = true;
                                }
                                else
                                {
                                    bfdata = true;
                                }
                                if (dblFeesTax + dblFeesCouponTax != 0)
                                {
                                    if (dFees.Rows.Count == 1) drep_InvTendering.lbFeeTx.Text = dFees.Rows[0]["FeesName"].ToString() + " " + "Tax";
                                    drep_InvTendering.rFeeTx.Text = (dblFeesTax + dblFeesCouponTax).ToString();
                                    drep_InvTendering.rFeeTx.Visible = true;
                                    drep_InvTendering.lbFeeTx.Visible = true;
                                }
                                else
                                {
                                    bftx = true;
                                }
                                if ((bfdata) && (bftx))
                                {
                                    drep_InvTendering.ReportHeader.Visible = false;
                                }
                            }

                            drep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                            drep_InvTendering.rTenderName.DataBindings.Add("Text", ddtbl3, "DisplayAs");
                            drep_InvTendering.rTenderAmt.DataBindings.Add("Text", ddtbl3, "Amount");

                            drep_InvTendering.rlbAdvance.Text = "";
                            drep_InvTendering.rAdvance.Text = "";
                            drep_InvTendering.rlbDue.Text = "";
                            drep_InvTendering.rDue.Text = "";

                            drep_InvTendering.rtr1.HeightF = 1.0f;
                            drep_InvTendering.rtr2.HeightF = 1.0f;
                            drep_InvTendering.rtbl.HeightF = 55.0f;
                            drep_InvTendering.PageHeader.HeightF = 55.0f;

                            if (dblTenderAmt != dblOrderTotal)
                            {
                                drep_InvTendering.ChangeDue = true;
                                drep_InvTendering.ReportFooter.Visible = true;
                                drep_InvTendering.rChangeDueText.Text = "Change";
                                drep_InvTendering.rChangeDue.Text = Convert.ToString(dblTenderAmt - dblOrderTotal);
                            }
                            else
                            {
                                drep_InvTendering.ChangeDue = false;
                                drep_InvTendering.ReportFooter.Visible = false;
                            }

                            if (Settings.POSShowGiftCertBalance == "Y")
                            {
                                PosDataObject.POS dobjPOS5 = new PosDataObject.POS();
                                dobjPOS5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                dtbl4 = dobjPOS5.ActiveGiftCert(intINV, Settings.CentralExportImport, Settings.StoreCode);
                                if (dtbl4.Rows.Count > 0)
                                {
                                    drep_InvMain.subrepGC.ReportSource = drep_InvGC;
                                    drep_InvGC.Report.DataSource = ddtbl4;
                                    drep_InvGC.DecimalPlace = Settings.DecimalPlace;
                                    drep_InvGC.rGCHeader.Text = "Gift Cert. with balance as on : " + DateTime.Today.Date.ToShortDateString();
                                    drep_InvGC.rGCName.DataBindings.Add("Text", ddtbl4, "GC");
                                    drep_InvGC.rGCAmt.DataBindings.Add("Text", ddtbl4, "GCAMT");
                                }
                            }

                            // EBT Balance on Receipt

                            PosDataObject.POS objPOS88 = new PosDataObject.POS();
                            objPOS88.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            DataTable ddtblEBT = objPOS88.FetchEBTBalanceFromReceipt(intINV);
                            if (ddtblEBT.Rows.Count > 0)
                            {
                                OfflineRetailV2.Report.Sales.repInvEBT drep_InvEBT = new OfflineRetailV2.Report.Sales.repInvEBT();
                                drep_InvMain.subrepEBT.ReportSource = drep_InvEBT;
                                drep_InvEBT.Report.DataSource = ddtblEBT;
                                drep_InvEBT.DecimalPlace = Settings.DecimalPlace;

                                drep_InvEBT.rEBTCard.DataBindings.Add("Text", ddtblEBT, "CardNo");
                                drep_InvEBT.rEBTBal.DataBindings.Add("Text", ddtblEBT, "CardBalance");
                            }

                            prmmgc = 0;
                            PosDataObject.POS obcc01mgc1 = new PosDataObject.POS();
                            obcc01mgc1.Connection = SystemVariables.Conn;
                            prmmgc = obcc01mgc1.GetTranIDFromInvoiceID(intINV);
                            DataTable ccdtbl11mgc1 = new DataTable();
                            PosDataObject.POS obcc11mgc1 = new PosDataObject.POS();
                            obcc11mgc1.Connection = SystemVariables.Conn;
                            ccdtbl11mgc1 = obcc11mgc1.FetchMercuryGiftCardData(prmmgc);

                            if (ccdtbl11mgc1.Rows.Count > 0)
                            {
                                drep_InvMain.subrepMGC.ReportSource = drep_InvMGC;
                                drep_InvMGC.Report.DataSource = ccdtbl11mgc1;
                                drep_InvMGC.DecimalPlace = Settings.DecimalPlace;
                                drep_InvMGC.rGCName.DataBindings.Add("Text", ccdtbl11mgc1, "RefCardAct");
                                drep_InvMGC.rGCAmt.DataBindings.Add("Text", ccdtbl11mgc1, "RefCardBalance");
                            }



                            frm_PreviewControl dfrm_PreviewControl = new frm_PreviewControl();
                            try
                            {
                                if (Settings.ReportPrinterName != "") drep_InvMain.PrinterName = Settings.ReportPrinterName;
                                //Todo: dfrm_PreviewControl.pnlCtrl.PrintingSystem = drep_InvMain.PrintingSystem; --Sam
                                //dfrm_PreviewControl.pnlCtrl.PrintingSystem.PreviewFormEx.Hide();
                                drep_InvMain.CreateDocument();
                                drep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                                drep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                                //rep_InvMain.ShowPreviewDialog();

                                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                                window.PreviewControl.DocumentSource = drep_InvMain;
                                window.ShowDialog();
                            }
                            finally
                            {
                                drep_InvMain.Dispose();
                                drep_InvHeader1.Dispose();
                                drep_InvHeader2.Dispose();
                                drep_InvLine.Dispose();
                                drep_InvSubtotal.Dispose();
                                drep_InvTax.Dispose();
                                drep_InvTendering.Dispose();
                                drep_InvGC.Dispose();
                                drep_InvMGC.Dispose();
                                drep_InvCoupon.Dispose();
                                ddtbl.Dispose();
                                ddtbl1.Dispose();
                                ddtbl2.Dispose();
                                ddtbl3.Dispose();
                                ddtbl4.Dispose();
                                ddtbl5.Dispose();
                                ccdtbl11mgc1.Dispose();
                            }
                        }

                        if (blHouseAccountPayment)
                        {
                            DataTable ddtbl = new DataTable();
                            PosDataObject.POS dobjPOS1 = new PosDataObject.POS();
                            dobjPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl = dobjPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                            dlogo = new DataTable();
                            objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            dlogo = objPOS1.FetchStoreLogo();
                            boolnulllogo = false;
                            foreach (DataRow drl1 in ddtbl.Rows)
                            {
                                foreach (DataRow drl2 in dlogo.Rows)
                                {
                                    if (drl2["logo"] == null) boolnulllogo = true;
                                    drl1["Logo"] = drl2["logo"];
                                }
                            }

                            intTranNo = 0;
                            dblOrderTotal = 0;
                            dblOrderSubtotal = 0;
                            dblDiscount = 0;
                            dblCoupon = 0;
                            dblTax = 0;
                            dblSurcharge = 0;
                            intCID = 0;
                            strDiscountReason = "";
                            dblTax1 = 0;
                            dblTax2 = 0;
                            dblTax3 = 0;
                            strTaxNM1 = "";
                            strTaxNM2 = "";
                            strTaxNM3 = "";
                            dblFees = 0;
                            dblFeesTax = 0;
                            dblFeesCoupon = 0;
                            dblFeesCouponTax = 0;

                            strDTaxNM = "";
                            dblDTax = 0;

                            foreach (DataRow dr in ddtbl.Rows)
                            {
                                intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                                intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                                dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                                dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                                dblCoupon = GeneralFunctions.fnDouble(dr["Coupon"].ToString());
                                dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                                dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                                dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                                dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                                strTaxNM1 = dr["TaxNM1"].ToString();
                                strTaxNM2 = dr["TaxNM2"].ToString();
                                strTaxNM3 = dr["TaxNM3"].ToString();
                                dblFees = GeneralFunctions.fnDouble(dr["Fees"].ToString());
                                dblFeesTax = GeneralFunctions.fnDouble(dr["FeesTax"].ToString());
                                dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                                dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());
                                strDiscountReason = dr["DiscountReason"].ToString();
                                strDTaxNM = dr["DTaxName"].ToString();
                                dblDTax = GeneralFunctions.fnDouble(dr["DTax"].ToString());
                            }

                            DataTable ddtbl1 = new DataTable();
                            DataTable ddtbl2 = new DataTable();
                            DataTable ddtbl3 = new DataTable();
                            DataTable ddtbl4 = new DataTable();
                            DataTable ddtbl5 = new DataTable();
                            OfflineRetailV2.Report.Sales.repInvMain drep_InvMain = new OfflineRetailV2.Report.Sales.repInvMain();
                            OfflineRetailV2.Report.Sales.repInvHeader1 drep_InvHeader1 = new OfflineRetailV2.Report.Sales.repInvHeader1();
                            OfflineRetailV2.Report.Sales.repInvHeader2 drep_InvHeader2 = new OfflineRetailV2.Report.Sales.repInvHeader2();
                            OfflineRetailV2.Report.Sales.repInvLine drep_InvLine = new OfflineRetailV2.Report.Sales.repInvLine();
                            OfflineRetailV2.Report.Sales.repInvSubtotal drep_InvSubtotal = new OfflineRetailV2.Report.Sales.repInvSubtotal();
                            OfflineRetailV2.Report.Sales.repInvTax drep_InvTax = new OfflineRetailV2.Report.Sales.repInvTax();
                            OfflineRetailV2.Report.Sales.repPPInvTendering drep_InvTendering = new OfflineRetailV2.Report.Sales.repPPInvTendering();
                            OfflineRetailV2.Report.Sales.repInvCC drep_InvCC = new OfflineRetailV2.Report.Sales.repInvCC();
                            OfflineRetailV2.Report.Sales.repInvMGC drep_InvMGC = new OfflineRetailV2.Report.Sales.repInvMGC();
                            OfflineRetailV2.Report.Sales.repInvCoupon drep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();
                            if (blVoid)
                                drep_InvMain.rReprint.Text = "**** Reprinted Void Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                            else
                                drep_InvMain.rReprint.Text = "**** Reprinted Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                            if (Settings.ReceiptFooter == "")
                            {
                                drep_InvMain.rReportFooter.HeightF = 1.0f;
                                drep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 2);
                                drep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 5);
                                drep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 5);

                                drep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 25);
                                drep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 25);
                                drep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 25);
                                drep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 25);

                                drep_InvMain.ReportFooter.Height = 60;
                                drep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                            }
                            else
                            {
                                drep_InvMain.ReportFooter.Height = 91;
                                drep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                            }

                            drep_InvMain.subrepH1.ReportSource = drep_InvHeader1;
                            drep_InvHeader1.Report.DataSource = ddtbl;
                            drep_InvHeader1.rReprint.Text = "";
                            GeneralFunctions.MakeReportWatermark(drep_InvMain);
                            drep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                            drep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;

                            WO = FetchWorkorderNo(intINV);
                            if (WO != 0)
                            {
                                drep_InvHeader1.rType.Text = "Work Order # " + WO.ToString() + "     Date : " + GeneralFunctions.fnDate(FetchWorkorderDate(intINV)).ToString(SystemVariables.DateFormat + " hh:mm:ss tt");
                            }
                            else
                            {
                                drep_InvHeader1.rType.Text = "";
                            }
                            drep_InvHeader1.rOrderNo.Text = intINV.ToString();
                            if (Settings.PrintLogoInReceipt == "Y")
                            {
                                if (!boolnulllogo) drep_InvHeader1.rPic.DataBindings.Add("Image", ddtbl, "Logo");
                            }
                            drep_InvHeader1.rOrderDate.DataBindings.Add("Text", ddtbl, "TransDate");

                            drep_InvMain.xrBarCode.Text = intINV.ToString();
                            drep_InvHeader1.rTraining.Visible = Settings.PrintTrainingMode == "Y";
                            if (intHeaderStatus == 3) drep_InvHeader1.rRefundCaption.Visible = dblOrderTotal < 0;
                            if (intCID > 0)
                            {
                                drep_InvMain.subrepH2.ReportSource = drep_InvHeader2;
                                drep_InvHeader2.Report.DataSource = ddtbl;
                                drep_InvHeader2.rCustID.DataBindings.Add("Text", ddtbl, "CustID");
                                drep_InvHeader2.rCustName.DataBindings.Add("Text", ddtbl, "CustName");
                                drep_InvHeader2.rCompany.DataBindings.Add("Text", ddtbl, "CustCompany");
                            }
                            else
                            {
                                drep_InvMain.subrepH2.ReportSource = drep_InvHeader2;
                                drep_InvHeader2.Report.DataSource = ddtbl;
                                drep_InvHeader2.rCustName.Text = "";
                                drep_InvHeader2.rCustID.Text = "";
                                drep_InvHeader2.rCompany.Text = "";
                                drep_InvHeader2.rlCustName.Text = "";
                                drep_InvHeader2.rlCustID.Text = "";
                                drep_InvHeader2.rlCompany.Text = "";
                            }

                            PosDataObject.POS dobjPOS2 = new PosDataObject.POS();
                            dobjPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl1 = dobjPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                            RearrangeForTaggedItemInInvoice(ddtbl1);
                            RearrangeForLineDisplay(ddtbl1);
                            drep_InvMain.subrepLine.ReportSource = drep_InvLine;
                            drep_InvLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvLine.Report.DataSource = ddtbl1;
                            drep_InvLine.rlqty.DataBindings.Add("Text", ddtbl1, "Qty");
                            drep_InvLine.rlSKU.DataBindings.Add("Text", ddtbl1, "SKU");
                            drep_InvLine.rlIem.DataBindings.Add("Text", ddtbl1, "Description");
                            drep_InvLine.rDiscTxt.DataBindings.Add("Text", ddtbl1, "DiscountText");
                            drep_InvLine.rlPrice.DataBindings.Add("Text", ddtbl1, "NormalPrice");
                            drep_InvLine.rlDiscount.DataBindings.Add("Text", ddtbl1, "Discount");
                            drep_InvLine.rlSurcharge.DataBindings.Add("Text", ddtbl1, "Price");
                            drep_InvLine.rlTotal.DataBindings.Add("Text", ddtbl1, "TotalPrice");
                            drep_InvLine.rManualWeight.DataBindings.Add("Text", ddtbl1, "ExtraValue1");

                            if (Settings.ShowFeesInReceipt == "Y")
                            {
                                drep_InvLine.rFeesTxt.Visible = true;
                                drep_InvLine.rFeesTxt.DataBindings.Add("Text", ddtbl1, "FeesText");
                            }
                            else
                            {
                                drep_InvLine.rFeesTxt.Visible = false;
                            }
                            foreach (DataRow dr12 in ddtbl1.Rows)
                            {
                                dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                            }

                            //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

                            drep_InvMain.subrepSubtotal.ReportSource = drep_InvSubtotal;
                            drep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                            drep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                            drep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                            drep_InvSubtotal.DR = strDiscountReason;
                            drep_InvSubtotal.rTax.Text = dblTax.ToString();

                            if (dblTax != 0)
                            {
                                ddtbl2.Columns.Add("Name", System.Type.GetType("System.String"));
                                ddtbl2.Columns.Add("Amount", System.Type.GetType("System.String"));

                                if (dblTax1 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM1, dblTax1.ToString() });
                                }

                                if (dblTax2 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM2, dblTax2.ToString() });
                                }

                                if (dblTax3 != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { strTaxNM3, dblTax3.ToString() });
                                }

                                if (dblDTax != 0)
                                {
                                    ddtbl2.Rows.Add(new object[] { "Dest. Tax : " + strDTaxNM, dblDTax.ToString() });
                                }

                                drep_InvMain.subrepTax.ReportSource = drep_InvTax;
                                drep_InvTax.DecimalPlace = Settings.DecimalPlace;

                                drep_InvTax.Report.DataSource = ddtbl2;
                                drep_InvTax.rDTax1.DataBindings.Add("Text", ddtbl2, "Name");
                                drep_InvTax.rDTax2.DataBindings.Add("Text", ddtbl2, "Amount");
                            }

                            PosDataObject.POS dobjPOS23 = new PosDataObject.POS();
                            dobjPOS23.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl5 = dobjPOS23.FetchInvoiceCoupons(intINV);
                            if (ddtbl5.Rows.Count > 0)
                            {
                                drep_InvMain.subrepCoupon.ReportSource = drep_InvCoupon;
                                drep_InvCoupon.DecimalPlace = Settings.DecimalPlace;
                                drep_InvCoupon.Report.DataSource = dtbl5;
                                drep_InvCoupon.rAmt.Text = dblCoupon.ToString();
                                drep_InvCoupon.rDTax1.DataBindings.Add("Text", ddtbl5, "Name");
                                drep_InvCoupon.rDTax2.DataBindings.Add("Text", ddtbl5, "Amount");
                            }

                            PosDataObject.POS dobjPOS4 = new PosDataObject.POS();
                            dobjPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            ddtbl3 = dobjPOS4.FetchInvoiceTenderForHouseAccount(intTranNo);
                            ddtbl3 = RearrangeTenderForCashBack(intTranNo, ddtbl3);
                            dblTenderAmt = 0;
                            foreach (DataRow dr1 in ddtbl3.Rows)
                            {
                                if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                                dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                            }

                            drep_InvMain.subrepTender.ReportSource = drep_InvTendering;
                            drep_InvTendering.Report.DataSource = ddtbl3;
                            drep_InvTendering.DecimalPlace = Settings.DecimalPlace;

                            if (Settings.ShowFeesInReceipt == "Y")
                            {
                                bool bfdata = false;
                                bool bftx = false;
                                DataTable dFees = FetchInvFees(intINV);
                                if (dblFees + dblFeesCoupon != 0)
                                {
                                    if (dFees.Rows.Count == 1) drep_InvTendering.lbFees.Text = dFees.Rows[0]["FeesName"].ToString();
                                    drep_InvTendering.rFees.Text = (dblFees + dblFeesCoupon).ToString();
                                    drep_InvTendering.rFees.Visible = true;
                                    drep_InvTendering.lbFees.Visible = true;
                                }
                                else
                                {
                                    bfdata = true;
                                }
                                if (dblFeesTax + dblFeesCouponTax != 0)
                                {
                                    if (dFees.Rows.Count == 1) drep_InvTendering.lbFeeTx.Text = dFees.Rows[0]["FeesName"].ToString() + " " + "Tax";
                                    drep_InvTendering.rFeeTx.Text = (dblFeesTax + dblFeesCouponTax).ToString();
                                    drep_InvTendering.rFeeTx.Visible = true;
                                    drep_InvTendering.lbFeeTx.Visible = true;
                                }
                                else
                                {
                                    bftx = true;
                                }
                                if ((bfdata) && (bftx))
                                {
                                    drep_InvTendering.ReportHeader.Visible = false;
                                }
                            }

                            drep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                            drep_InvTendering.rTenderName.DataBindings.Add("Text", ddtbl3, "DisplayAs");
                            drep_InvTendering.rTenderAmt.DataBindings.Add("Text", ddtbl3, "Amount");
                            drep_InvTendering.rChangeDue.Visible = false;
                            drep_InvTendering.rChangeDueText.Visible = false;
                            drep_InvTendering.rlbAdvance.Visible = false;

                            drep_InvMain.subrepGC.ReportSource = drep_InvCC;
                            drep_InvCC.xrTable1.Visible = false;
                            drep_InvCC.rTxt.Visible = false;

                            // frm_PreviewControl dfrm_PreviewControl = new frm_PreviewControl();
                            try
                            {
                                if (Settings.ReportPrinterName != "") drep_InvMain.PrinterName = Settings.ReportPrinterName;
                                //Todo: dfrm_PreviewControl.pnlCtrl.PrintingSystem = drep_InvMain.PrintingSystem; --Sam
                                //dfrm_PreviewControl.pnlCtrl.PrintingSystem.PreviewFormEx.Hide();
                                drep_InvMain.CreateDocument();
                                drep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                                drep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                                //rep_InvMain.ShowPreviewDialog();

                                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                                window.PreviewControl.DocumentSource = drep_InvMain;
                                window.ShowDialog();
                            }
                            finally
                            {
                                drep_InvMain.Dispose();
                                drep_InvHeader1.Dispose();
                                drep_InvHeader2.Dispose();
                                drep_InvLine.Dispose();
                                drep_InvSubtotal.Dispose();
                                drep_InvTax.Dispose();
                                drep_InvTendering.Dispose();
                                drep_InvCC.Dispose();
                                drep_InvMGC.Dispose();
                                drep_InvCoupon.Dispose();
                                ddtbl.Dispose();
                                ddtbl1.Dispose();
                                ddtbl2.Dispose();
                                ddtbl3.Dispose();
                                ddtbl4.Dispose();
                                ddtbl5.Dispose();
                            }
                        }
                    }
                    else // preprinted format
                    {
                        DataTable dtbl = new DataTable();
                        PosDataObject.POS objPOS1 = new PosDataObject.POS();
                        objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl = objPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                        DataTable dlogo = new DataTable();
                        objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dlogo = objPOS1.FetchStoreLogo();
                        bool boolnulllogo = false;
                        foreach (DataRow drl1 in dtbl.Rows)
                        {
                            foreach (DataRow drl2 in dlogo.Rows)
                            {
                                if (drl2["logo"] == null) boolnulllogo = true;
                                drl1["Logo"] = drl2["logo"];
                            }
                        }

                        int intTranNo = 0;
                        double dblOrderTotal = 0;
                        double dblOrderSubtotal = 0;
                        double dblDiscount = 0;
                        double dblCoupon = 0;
                        double dblTax = 0;
                        int intCID = 0;
                        string strDiscountReason = "";
                        double dblTax1 = 0;
                        double dblTax2 = 0;
                        double dblTax3 = 0;
                        string strTaxNM1 = "";
                        string strTaxNM2 = "";
                        string strTaxNM3 = "";
                        string strservice = "";
                        int intHeaderStatus = 0;
                        double dblRentDeposit = 0;
                        double dblRepairAmount = 0;
                        double dblRepairAdvanceAmount = 0;
                        string strRepairDeliveryDate = "";
                        string calcrent = "N";
                        string strCInfo = "";

                        foreach (DataRow dr in dtbl.Rows)
                        {
                            intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                            intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                            dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                            dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                            dblCoupon = GeneralFunctions.fnDouble(dr["Coupon"].ToString());
                            dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                            dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                            dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                            dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                            strTaxNM1 = dr["TaxNM1"].ToString();
                            strTaxNM2 = dr["TaxNM2"].ToString();
                            strTaxNM3 = dr["TaxNM3"].ToString();

                            strDiscountReason = dr["DiscountReason"].ToString();
                            strservice = dr["ServiceType"].ToString();
                            intHeaderStatus = GeneralFunctions.fnInt32(dr["Status"].ToString());
                            dblRentDeposit = GeneralFunctions.fnDouble(dr["RentDeposit"].ToString());

                            dblRepairAmount = GeneralFunctions.fnDouble(dr["RepairAmount"].ToString());
                            dblRepairAdvanceAmount = GeneralFunctions.fnDouble(dr["RepairAdvanceAmount"].ToString());
                            if (dr["RepairDeliveryDate"].ToString() != "") strRepairDeliveryDate = GeneralFunctions.fnDate(dr["RepairDeliveryDate"].ToString()).ToShortDateString();
                            calcrent = dr["IsRentCalculated"].ToString();
                            strCInfo = dr["CustDetails"].ToString();
                        }

                        blCardPayment = IsCardPayment(intTranNo);

                        DataTable dtbl1 = new DataTable();
                        DataTable dtbl2 = new DataTable();
                        DataTable dtbl3 = new DataTable();
                        DataTable dtbl4 = new DataTable();
                        DataTable dtbl5 = new DataTable();

                        OfflineRetailV2.Report.Sales.PrePrint.repPPInv rep_InvMain = new OfflineRetailV2.Report.Sales.PrePrint.repPPInv();
                        OfflineRetailV2.Report.Sales.PrePrint.repPPInvHeader rep_InvHeader1 = new OfflineRetailV2.Report.Sales.PrePrint.repPPInvHeader();
                        OfflineRetailV2.Report.Sales.PrePrint.repPPInvLine rep_InvLine = new OfflineRetailV2.Report.Sales.PrePrint.repPPInvLine();
                        OfflineRetailV2.Report.Sales.PrePrint.repPPInvSubtotal rep_InvSubtotal = new OfflineRetailV2.Report.Sales.PrePrint.repPPInvSubtotal();
                        OfflineRetailV2.Report.Sales.PrePrint.repPPInvTax rep_InvTax = new OfflineRetailV2.Report.Sales.PrePrint.repPPInvTax();
                        OfflineRetailV2.Report.Sales.PrePrint.repPPInvTendering rep_InvTendering = new OfflineRetailV2.Report.Sales.PrePrint.repPPInvTendering();
                        /*Report.Sales.repInvSubtotal rep_InvSubtotal = new Report.Sales.repInvSubtotal();
                        Report.Sales.repInvRentLine rep_InvRentLine = new Report.Sales.repInvRentLine();
                        Report.Sales.repInvRentSubTotal rep_InvRentSubTotal = new Report.Sales.repInvRentSubTotal();
                        Report.Sales.repInvRentReturnLine rep_InvRentReturnLine = new Report.Sales.repInvRentReturnLine();
                        Report.Sales.repInvRentReturnSubTotal rep_InvRentReturnSubTotal = new Report.Sales.repInvRentReturnSubTotal();
                        Report.Sales.repInvTax rep_InvTax = new Report.Sales.repInvTax();
                        Report.Sales.repPPInvTendering rep_InvTendering = new Report.Sales.repPPInvTendering();
                        Report.Sales.repInvGC rep_InvGC = new Report.Sales.repInvGC();
                        Report.Sales.repInvCC rep_InvCC = new Report.Sales.repInvCC();
                        Report.Sales.repInvCoupon rep_InvCoupon = new Report.Sales.repInvCoupon();*/

                        /*if (blCardPayment)
                        {
                            int prm = 0;
                            PosDataObject.POS obcc0 = new PosDataObject.POS();
                            obcc0.Connection = SystemVariables.Conn;
                            prm = obcc0.GetTranIDFromInvoiceID(intINV);
                            DataTable ccdtbl1 = new DataTable();
                            PosDataObject.POS obcc1 = new PosDataObject.POS();
                            obcc1.Connection = SystemVariables.Conn;
                            ccdtbl1 = obcc1.FetchCardData(prm);
                            foreach (DataRow ds in ccdtbl1.Rows)
                            {
                                if (ds["IsDebitCard"].ToString() == "N") CardType = "Credit";
                                if (ds["IsDebitCard"].ToString() == "Y") CardType = "Debit";
                                break;
                            }
                            rep_InvMain.subrepCC.ReportSource = rep_InvCC;
                            rep_InvCC.Report.DataSource = ccdtbl1;
                            rep_InvCC.rRef.Text = intINV.ToString();
                            if (Settings.PaymentGateway == 2) // Mercury Payment
                            {
                                rep_InvCC.rlMerch.Text = "MERCH ID";
                                //rep_InvCC.rMerch.Text = strMercuryMerchantID;
                            }
                            else
                            {
                                rep_InvCC.rlMerch.Text = "";
                                //rep_InvCC.rMerch.Text = "";
                            }

                            rep_InvCC.rAct.DataBindings.Add("Text", ccdtbl1, "RefCardAct");
                            rep_InvCC.rCard.DataBindings.Add("Text", ccdtbl1, "RefCardLogo");
                            rep_InvCC.rSwipe.DataBindings.Add("Text", ccdtbl1, "RefCardEntry");
                            rep_InvCC.rApprCode.DataBindings.Add("Text", ccdtbl1, "RefCardAuthID");
                            rep_InvCC.rTranID.DataBindings.Add("Text", ccdtbl1, "RefCardTranID");
                            rep_InvCC.rAmt.DataBindings.Add("Text", ccdtbl1, "RefCardAuthAmount");
                            rep_InvCC.rMerch.DataBindings.Add("Text", ccdtbl1, "RefCardMerchID");

                            rep_InvCC.rsign1.Visible = true;
                            rep_InvCC.rsign2.Visible = true;

                            rep_InvCC.rsign3.Visible = true;
                            rep_InvCC.rTxt.Visible = true;
                            rep_InvMain.rCopy.Visible = true;
                            rep_InvMain.rCopy.Text = "CARDHOLDER COPY";

                            if ((CardType == "Credit") || (CardType == "Credit Card"))
                            {
                                rep_InvCC.rTxt.Text = "I AGREE TO PAY ABOVE TOTAL AMOUNT ACCORDING TO CARD ISSUER AGREEMENT";
                                rep_InvCC.rlbl.Text = "CREDIT PURCHASE";
                            }
                            if ((CardType == "Debit") || (CardType == "Debit Card"))
                            {
                                rep_InvCC.rsign1.Visible = false;
                                rep_InvCC.rsign2.Visible = false;
                                rep_InvCC.rsign3.Visible = false;
                                rep_InvCC.rTxt.Text = "PIN USED \n SIGNATURE NOT REQUIRED";
                                rep_InvCC.rlbl.Text = "DEBIT PURCHASE";
                            }
                        }*/
                        GeneralFunctions.MakeReportWatermark(rep_InvMain);
                        rep_InvMain.rReportHeader.Text = Settings.MainReceiptHeader;
                        if (Settings.DemoVersion == "Y") rep_InvHeader1.rDemo.Visible = true;
                        else rep_InvHeader1.rDemo.Visible = false;

                        rep_InvMain.subrepH.ReportSource = rep_InvHeader1;
                        rep_InvHeader1.Report.DataSource = dtbl;

                        rep_InvHeader1.rOrder.Text = intINV.ToString();
                        rep_InvHeader1.rDate.DataBindings.Add("Text", dtbl, "TransDate");
                        rep_InvHeader1.rTime.DataBindings.Add("Text", dtbl, "TransDate");

                        if (intCID > 0)
                        {
                            rep_InvHeader1.rCustomer.DataBindings.Add("Text", dtbl, "CustID");
                            rep_InvHeader1.rCustDetails.DataBindings.Add("Text", dtbl, "CustDetails");
                        }
                        else
                        {
                            rep_InvHeader1.rCustomer.Visible = false;
                            rep_InvHeader1.rCustDetails.Visible = false;
                        }

                        rep_InvHeader1.rClerk.DataBindings.Add("Text", dtbl, "EmpID");
                        rep_InvHeader1.rStore.DataBindings.Add("Text", dtbl, "StoreID");
                        rep_InvHeader1.rRegister.DataBindings.Add("Text", dtbl, "RegisterID");

                        PosDataObject.POS objPOS2 = new PosDataObject.POS();
                        objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl1 = objPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                        RearrangeForTaggedItemInInvoice(dtbl1);

                        rep_InvMain.subrepL.ReportSource = rep_InvLine;
                        rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvLine.Report.DataSource = dtbl1;
                        rep_InvLine.rQty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvLine.rSKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvLine.rItem.DataBindings.Add("Text", dtbl1, "Description");
                        rep_InvLine.rRate.DataBindings.Add("Text", dtbl1, "Price");
                        rep_InvLine.rTot.DataBindings.Add("Text", dtbl1, "TotalPrice");
                        rep_InvLine.rDisc.DataBindings.Add("Text", dtbl1, "DiscountInfo");

                        foreach (DataRow dr12 in dtbl1.Rows)
                        {
                            dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()); // +GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                        }

                        //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

                        rep_InvMain.subrepS.ReportSource = rep_InvSubtotal;
                        rep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                        rep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();

                        if (dblTax != 0)
                        {
                            dtbl2.Columns.Add("Name", System.Type.GetType("System.String"));
                            dtbl2.Columns.Add("Amount", System.Type.GetType("System.String"));

                            if (dblTax1 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM1, dblTax1.ToString() });
                            }

                            if (dblTax2 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM2, dblTax2.ToString() });
                            }

                            if (dblTax3 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM3, dblTax3.ToString() });
                            }

                            rep_InvMain.subrepX.ReportSource = rep_InvTax;
                            rep_InvTax.DecimalPlace = Settings.DecimalPlace;

                            rep_InvTax.Report.DataSource = dtbl2;
                            rep_InvTax.rDTax1.DataBindings.Add("Text", dtbl2, "Name");
                            rep_InvTax.rDTax2.DataBindings.Add("Text", dtbl2, "Amount");
                        }

                        PosDataObject.POS objPOS4 = new PosDataObject.POS();
                        objPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl3 = objPOS4.FetchInvoiceTender(intTranNo);
                        dtbl3 = RearrangeTenderForCashBack(intTranNo, dtbl3);
                        double dblTenderAmt = 0;
                        foreach (DataRow dr1 in dtbl3.Rows)
                        {
                            if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                            dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                        }

                        rep_InvMain.subrepT.ReportSource = rep_InvTendering;
                        rep_InvTendering.Report.DataSource = dtbl3;
                        rep_InvTendering.DecimalPlace = Settings.DecimalPlace;
                        rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                        rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                        rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");

                        double EffectiveTotal = 0;

                        EffectiveTotal = dblOrderTotal;

                        if (dblTenderAmt != EffectiveTotal)
                        {
                            //rep_InvTendering.ChangeDue = true;
                            //rep_InvTendering.ReportFooter.Visible = true;
                            //rep_InvTendering.rChangeDueText.Text = "Change";
                            //rep_InvTendering.rChangeDue.Text = Convert.ToString(dblTenderAmt - EffectiveTotal);
                        }
                        else
                        {
                            //rep_InvTendering.ChangeDue = false;
                            //rep_InvTendering.ReportFooter.Visible = false;
                        }

                        /*
                        if (strservice == "Rent")
                        {
                            if (intHeaderStatus == 15) // issue
                            {
                                rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentSubTotal;
                                rep_InvRentSubTotal.DecimalPlace = Settings.DecimalPlace;
                                rep_InvRentSubTotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                                rep_InvRentSubTotal.rDeposit.Text = dblRentDeposit.ToString();
                                rep_InvRentSubTotal.rDiscount.Text = dblDiscount.ToString();
                                rep_InvRentSubTotal.DR = strDiscountReason;
                                rep_InvRentSubTotal.rTax.Text = dblTax.ToString();
                            }

                            if (intHeaderStatus == 16) // return
                            {
                                if (calcrent == "N")
                                {
                                    if (dblOrderTotal != 0)
                                    {
                                        rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentReturnSubTotal;
                                        rep_InvRentReturnSubTotal.DecimalPlace = Settings.DecimalPlace;
                                        rep_InvRentReturnSubTotal.rReturnDeposit.Text = dblOrderTotal.ToString();
                                    }
                                }

                                if (calcrent == "Y")
                                {
                                    rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentReturnSubTotal;
                                    rep_InvRentReturnSubTotal.DecimalPlace = Settings.DecimalPlace;
                                    rep_InvRentReturnSubTotal.rReturnDeposit.Text = (-dblRentDeposit).ToString();
                                }
                            }
                        }
                        else if (strservice == "Repair")
                        {
                            if (intHeaderStatus == 17) // issue
                            {
                                rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentSubTotal;
                                rep_InvRentSubTotal.DecimalPlace = Settings.DecimalPlace;
                                rep_InvRentSubTotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                                rep_InvRentSubTotal.rDeposit.Text = dblRentDeposit.ToString();
                                rep_InvRentSubTotal.rDiscount.Text = dblDiscount.ToString();
                                rep_InvRentSubTotal.DR = strDiscountReason;
                                rep_InvRentSubTotal.rw1.Visible = false;
                                rep_InvRentSubTotal.rw2.Visible = false;
                                rep_InvRentSubTotal.rTax.Text = dblTax.ToString();
                            }
                        }
                        else
                        {
                            rep_InvMain.subrepSubtotal.ReportSource = rep_InvSubtotal;
                            rep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                            rep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                            rep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                            rep_InvSubtotal.DR = strDiscountReason;
                            rep_InvSubtotal.rTax.Text = dblTax.ToString();
                        }

                        if (dblTax != 0)
                        {
                            dtbl2.Columns.Add("Name", System.Type.GetType("System.String"));
                            dtbl2.Columns.Add("Amount", System.Type.GetType("System.String"));

                            if (dblTax1 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM1, dblTax1.ToString() });
                            }

                            if (dblTax2 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM2, dblTax2.ToString() });
                            }

                            if (dblTax3 != 0)
                            {
                                dtbl2.Rows.Add(new object[] { strTaxNM3, dblTax3.ToString() });
                            }

                            rep_InvMain.subrepTax.ReportSource = rep_InvTax;
                            rep_InvTax.DecimalPlace = Settings.DecimalPlace;

                            rep_InvTax.Report.DataSource = dtbl2;
                            rep_InvTax.rDTax1.DataBindings.Add("Text", dtbl2, "Name");
                            rep_InvTax.rDTax2.DataBindings.Add("Text", dtbl2, "Amount");
                        }

                        PosDataObject.POS objPOS23 = new PosDataObject.POS();
                        objPOS23.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl5 = objPOS23.FetchInvoiceCoupons(intINV);
                        if (dtbl5.Rows.Count > 0)
                        {
                            rep_InvMain.subrepCoupon.ReportSource = rep_InvCoupon;
                            rep_InvCoupon.DecimalPlace = Settings.DecimalPlace;
                            rep_InvCoupon.Report.DataSource = dtbl5;
                            rep_InvCoupon.rAmt.Text = dblCoupon.ToString();
                            rep_InvCoupon.rDTax1.DataBindings.Add("Text", dtbl5, "Name");
                            rep_InvCoupon.rDTax2.DataBindings.Add("Text", dtbl5, "Amount");
                        }

                        PosDataObject.POS objPOS4 = new PosDataObject.POS();
                        objPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl3 = objPOS4.FetchInvoiceTender(intTranNo);

                        double dblTenderAmt = 0;
                        foreach (DataRow dr1 in dtbl3.Rows)
                        {
                            dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                        }

                        rep_InvMain.subrepTender.ReportSource = rep_InvTendering;
                        rep_InvTendering.Report.DataSource = dtbl3;
                        rep_InvTendering.DecimalPlace = Settings.DecimalPlace;
                        rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                        rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                        rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");

                        if (strservice == "Repair")
                        {
                            if (intHeaderStatus == 17)
                            {
                                rep_InvTendering.rlbAdvance.Text = "Advance Amount";
                                rep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                            }
                            if (intHeaderStatus == 18)
                            {
                                rep_InvTendering.rlbAdvance.Text = "";
                                rep_InvTendering.rAdvance.Text = "";
                            }
                        }
                        else
                        {
                            rep_InvTendering.rlbAdvance.Text = "";
                            rep_InvTendering.rAdvance.Text = "";
                        }

                        double EffectiveTotal = 0;
                        if ((intHeaderStatus == 15) && (calcrent == "Y")) EffectiveTotal = dblRentDeposit;
                        else EffectiveTotal = dblOrderTotal;

                        if (dblTenderAmt != EffectiveTotal)
                        {
                            rep_InvTendering.ChangeDue = true;
                            rep_InvTendering.ReportFooter.Visible = true;
                            rep_InvTendering.rChangeDueText.Text = "Change";
                            rep_InvTendering.rChangeDue.Text = Convert.ToString(dblTenderAmt - EffectiveTotal);
                        }
                        else
                        {
                            rep_InvTendering.ChangeDue = false;
                            rep_InvTendering.ReportFooter.Visible = false;
                        }

                        if (Settings.POSShowGiftCertBalance == "Y")
                        {
                            PosDataObject.POS objPOS5 = new PosDataObject.POS();
                            objPOS5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            dtbl4 = objPOS5.ActiveGiftCert(intINV, Settings.CentralExportImport, Settings.StoreCode);
                            if (dtbl4.Rows.Count > 0)
                            {
                                rep_InvMain.subrepGC.ReportSource = rep_InvGC;
                                rep_InvGC.Report.DataSource = dtbl4;
                                rep_InvGC.DecimalPlace = Settings.DecimalPlace;
                                rep_InvGC.rGCHeader.Text = "Gift Cert. with balance as on : " + DateTime.Today.Date.ToShortDateString();
                                rep_InvGC.rGCName.DataBindings.Add("Text", dtbl4, "GC");
                                rep_InvGC.rGCAmt.DataBindings.Add("Text", dtbl4, "GCAMT");
                            }
                        }*/

                        //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                        try
                        {
                            if (Settings.ReportPrinterName != "") rep_InvMain.PrinterName = Settings.ReportPrinterName;
                            rep_InvMain.CreateDocument();
                            rep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                            rep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_InvMain.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_InvMain;
                            window.ShowDialog();
                        }
                        finally
                        {
                            rep_InvHeader1.Dispose();
                            rep_InvLine.Dispose();
                            rep_InvSubtotal.Dispose();
                            rep_InvTax.Dispose();
                            rep_InvTendering.Dispose();
                            rep_InvMain.Dispose();

                            dtbl.Dispose();
                            dtbl1.Dispose();
                            dtbl2.Dispose();
                            dtbl3.Dispose();
                            dtbl4.Dispose();
                            dtbl5.Dispose();
                        }
                    }
                }
                else
                {
                    DataTable dtbl = new DataTable();
                    PosDataObject.POS objPOS1 = new PosDataObject.POS();
                    objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl = objPOS1.FetchInvoiceHeader(intINV, Settings.StoreCode);

                    DataTable dlogo = new DataTable();
                    objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dlogo = objPOS1.FetchStoreLogo();
                    bool boolnulllogo = false;
                    foreach (DataRow drl1 in dtbl.Rows)
                    {
                        foreach (DataRow drl2 in dlogo.Rows)
                        {
                            if (drl2["logo"] == null) boolnulllogo = true;
                            drl1["Logo"] = drl2["logo"];
                        }
                    }

                    int intTranNo = 0;
                    double dblOrderTotal = 0;
                    double dblOrderSubtotal = 0;
                    double dblDiscount = 0;
                    double dblCoupon = 0;
                    double dblTax = 0;
                    double dblSurcharge = 0;
                    int intCID = 0;
                    string strDiscountReason = "";
                    double dblTax1 = 0;
                    double dblTax2 = 0;
                    double dblTax3 = 0;
                    string strTaxNM1 = "";
                    string strTaxNM2 = "";
                    string strTaxNM3 = "";
                    string strservice = "";
                    int intHeaderStatus = 0;
                    double dblRentDeposit = 0;
                    double dblRentReturnDeposit = 0;
                    double dblRepairAmount = 0;
                    double dblRepairAdvanceAmount = 0;
                    string strRepairDeliveryDate = "";
                    string calcrent = "N";

                    double dblFees = 0;
                    double dblFeesTax = 0;

                    double dblFeesCoupon = 0;
                    double dblFeesCouponTax = 0;

                    string strDTaxNM = "";
                    double dblDTax = 0;

                    string strCustomerDOB = "";

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        intTranNo = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                        intCID = GeneralFunctions.fnInt32(dr["CID"].ToString());
                        dblOrderTotal = GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                        dblDiscount = GeneralFunctions.fnDouble(dr["Discount"].ToString());
                        dblCoupon = GeneralFunctions.fnDouble(dr["Coupon"].ToString());
                        dblTax = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                        dblTax1 = GeneralFunctions.fnDouble(dr["Tax1"].ToString());
                        dblTax2 = GeneralFunctions.fnDouble(dr["Tax2"].ToString());
                        dblTax3 = GeneralFunctions.fnDouble(dr["Tax3"].ToString());
                        strTaxNM1 = dr["TaxNM1"].ToString();
                        strTaxNM2 = dr["TaxNM2"].ToString();
                        strTaxNM3 = dr["TaxNM3"].ToString();

                        dblFees = GeneralFunctions.fnDouble(dr["Fees"].ToString());
                        dblFeesTax = GeneralFunctions.fnDouble(dr["FeesTax"].ToString());

                        dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                        dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());

                        strDiscountReason = dr["DiscountReason"].ToString();
                        strservice = dr["ServiceType"].ToString();
                        intHeaderStatus = GeneralFunctions.fnInt32(dr["Status"].ToString());
                        dblRentDeposit = GeneralFunctions.fnDouble(dr["RentDeposit"].ToString());

                        dblRepairAmount = GeneralFunctions.fnDouble(dr["RepairAmount"].ToString());
                        dblRepairAdvanceAmount = GeneralFunctions.fnDouble(dr["RepairAdvanceAmount"].ToString());
                        if (dr["RepairDeliveryDate"].ToString() != "") strRepairDeliveryDate = GeneralFunctions.fnDate(dr["RepairDeliveryDate"].ToString()).ToShortDateString();
                        calcrent = dr["IsRentCalculated"].ToString();

                        strDTaxNM = dr["DTaxName"].ToString();
                        dblDTax = GeneralFunctions.fnDouble(dr["DTax"].ToString());

                        if (Settings.POSIDRequired == "Y") strCustomerDOB = dr["CustomerDOB"].ToString();
                    }
                    if (intHeaderStatus == 17) dblOrderTotal = dblRepairAmount;
                    //bool blDatacapManualEntry = IsDatacapManual(intTranNo);
                    blCardPayment = IsCardPayment(intTranNo);
                    blHouseAccountPayment = IsHAPayment(intTranNo);
                    //bool blFSTender = IsFSTendering(intTranNo);
                    if (blCardPayment)
                    {
                        if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 2))
                        {
                            bool b1 = IsMercuryCardPayment(intTranNo);
                            if (b1)
                            {
                                double amt = GetMercuryCardPaymentAmount(intTranNo);
                                if (amt < Settings.MercurySignAmount) MercuryCardPaymentCheck = true;
                            }
                        }
                    }


                    //blRepairPrint = (strservice == "Repair") ? true : false;

                    DataTable dtbl1 = new DataTable();
                    DataTable dtbl2 = new DataTable();
                    DataTable dtbl3 = new DataTable();
                    DataTable dtbl4 = new DataTable();
                    DataTable dtbl5 = new DataTable();

                    OfflineRetailV2.Report.Sales.repInvMain rep_InvMain = new OfflineRetailV2.Report.Sales.repInvMain();
                    OfflineRetailV2.Report.Sales.repInvHeader1 rep_InvHeader1 = new OfflineRetailV2.Report.Sales.repInvHeader1();
                    OfflineRetailV2.Report.Sales.repInvHeader2 rep_InvHeader2 = new OfflineRetailV2.Report.Sales.repInvHeader2();
                    OfflineRetailV2.Report.Sales.repInvGALine rep_InvLine = new OfflineRetailV2.Report.Sales.repInvGALine();
                    OfflineRetailV2.Report.Sales.repInvSubtotal rep_InvSubtotal = new OfflineRetailV2.Report.Sales.repInvSubtotal();
                    OfflineRetailV2.Report.Sales.repInvRentLine rep_InvRentLine = new OfflineRetailV2.Report.Sales.repInvRentLine();
                    OfflineRetailV2.Report.Sales.repInvRentSubTotal rep_InvRentSubTotal = new OfflineRetailV2.Report.Sales.repInvRentSubTotal();
                    OfflineRetailV2.Report.Sales.repInvRentReturnLine rep_InvRentReturnLine = new OfflineRetailV2.Report.Sales.repInvRentReturnLine();
                    OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal rep_InvRentReturnSubTotal = new OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal();
                    OfflineRetailV2.Report.Sales.repInvTax rep_InvTax = new OfflineRetailV2.Report.Sales.repInvTax();
                    OfflineRetailV2.Report.Sales.repPPInvTendering rep_InvTendering = new OfflineRetailV2.Report.Sales.repPPInvTendering();
                    OfflineRetailV2.Report.Sales.repInvGC rep_InvGC = new OfflineRetailV2.Report.Sales.repInvGC();
                    OfflineRetailV2.Report.Sales.repInvMGC rep_InvMGC = new OfflineRetailV2.Report.Sales.repInvMGC();
                    OfflineRetailV2.Report.Sales.repInvCC rep_InvCC = new OfflineRetailV2.Report.Sales.repInvCC();
                    OfflineRetailV2.Report.Sales.repInvHA rep_InvHA = new OfflineRetailV2.Report.Sales.repInvHA();
                    OfflineRetailV2.Report.Sales.repInvSC rep_InvSC = new OfflineRetailV2.Report.Sales.repInvSC();

                    OfflineRetailV2.Report.Sales.repInvSign rep_InvSign = new OfflineRetailV2.Report.Sales.repInvSign();
                    OfflineRetailV2.Report.Sales.repInvCoupon rep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();
                    rep_InvMain.rReprint.Text = "";
                    if (Settings.ReceiptFooter == "")
                    {
                        rep_InvMain.rReportFooter.HeightF = 1.0f;
                        rep_InvMain.rReportFooter.LocationF = new PointF(8, 2);
                        rep_InvMain.xrBarCode.LocationF = new PointF(8, 5);
                        rep_InvMain.rCopy.LocationF = new PointF(567, 5);

                        rep_InvMain.xrShape1.LocationF = new PointF(581, 25);
                        rep_InvMain.xrPageInfo2.LocationF = new PointF(594, 25);
                        rep_InvMain.xrPageInfo1.LocationF = new PointF(681, 25);
                        rep_InvMain.xrShape2.LocationF = new PointF(725, 25);

                        rep_InvMain.ReportFooter.Height = 60;
                        rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                    }
                    else
                    {
                        rep_InvMain.ReportFooter.Height = 91;
                        rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;

                    }

                    if (blCardPayment)
                    {
                        if (blCardPayment)
                        {
                            int prm = 0;
                            PosDataObject.POS obcc0 = new PosDataObject.POS();
                            obcc0.Connection = SystemVariables.Conn;
                            prm = obcc0.GetTranIDFromInvoiceID(intINV);
                            DataTable ccdtbl1 = new DataTable();
                            PosDataObject.POS obcc1 = new PosDataObject.POS();
                            obcc1.Connection = SystemVariables.Conn;
                            ccdtbl1 = obcc1.FetchCardData(prm);
                            foreach (DataRow ds in ccdtbl1.Rows)
                            {
                                if ((ds["CardType"].ToString() == "Credit") || (ds["CardType"].ToString() == "Credit Card")) CardType = "Credit";
                                if ((ds["CardType"].ToString() == "Debit") || (ds["CardType"].ToString() == "Debit Card")) CardType = "Debit";
                                if (ds["CardType"].ToString() == "Mercury Gift Card") CardType = "Mercury";
                                if (ds["CardType"].ToString() == "Precidia Gift Card") CardType = "Precidia";
                                if (ds["CardType"].ToString() == "Datacap Gift Card") CardType = "Datacap";
                                if (ds["CardType"].ToString() == "POSLink Gift Card") CardType = "POSLink";
                                if (ds["CardType"].ToString() == "EBT") CardType = "EBT";

                                break;
                            }
                            rep_InvMain.subrepCC.ReportSource = rep_InvCC;
                            rep_InvCC.Report.DataSource = ccdtbl1;
                            rep_InvCC.rRef.Text = intINV.ToString();

                            rep_InvCC.rAct.DataBindings.Add("Text", ccdtbl1, "RefCardAct");
                            rep_InvCC.rCard.DataBindings.Add("Text", ccdtbl1, "RefCardLogo");

                            rep_InvCC.rApprCode.DataBindings.Add("Text", ccdtbl1, "RefCardAuthID");
                            rep_InvCC.rTranID.DataBindings.Add("Text", ccdtbl1, "RefCardTranID");
                            rep_InvCC.rAmt.DataBindings.Add("Text", ccdtbl1, "RefCardAuthAmount");


                            rep_InvCC.rsign1.Visible = true;
                            rep_InvCC.rsign2.Visible = true;

                            rep_InvCC.rsign3.Visible = true;
                            rep_InvCC.rTxt.Visible = true;
                            rep_InvMain.rCopy.Visible = true;
                            rep_InvMain.rCopy.Text = Properties.Resources.CARDHOLDER_COPY;

                            if ((CardType == "Credit") || (CardType == "Credit Card"))
                            {
                                rep_InvCC.rTxt.Text = Properties.Resources.I_AGREE_TO_PAY_ABOVE_TOTAL_AMOUNT_ACCORDING_TO_CARD_ISSUER_AGREEMENT;
                                rep_InvCC.rlbl.Text = Properties.Resources.CREDIT_PURCHASE;
                            }
                            if ((CardType == "Debit") || (CardType == "Debit Card"))
                            {
                                rep_InvCC.rsign1.Visible = false;
                                rep_InvCC.rsign2.Visible = false;
                                rep_InvCC.rsign3.Visible = false;
                                rep_InvCC.rTxt.Text = Properties.Resources.PIN_USED + "\n" + Properties.Resources.SIGNATURE_NOT_REQUIRED;
                                rep_InvCC.rlbl.Text = Properties.Resources.DEBIT_PURCHASE;
                            }
                        }
                    }
                    rep_InvMain.subrepH1.ReportSource = rep_InvHeader1;
                    rep_InvHeader1.Report.DataSource = dtbl;
                    rep_InvHeader1.rReprint.Text = "";
                    GeneralFunctions.MakeReportWatermark(rep_InvMain);
                    rep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                    rep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;
                    rep_InvHeader1.rTraining.Visible = Settings.PrintTrainingMode == "Y";
                    if (strservice == "Sales") rep_InvHeader1.rRefundCaption.Visible = dblOrderTotal < 0;
                    int WO = FetchWorkorderNo(intINV);

                    rep_InvHeader1.rType.Text = "Gift Aid Receipt";



                    rep_InvHeader1.xrTableCell2.Text = "Payment Ref ID";
                    rep_InvHeader1.xrTableCell4.Text = "Date";
                    rep_InvHeader1.rOrderNo.Text = intINV.ToString();
                    if (Settings.PrintLogoInReceipt == "Y")
                    {
                        if (!boolnulllogo) rep_InvHeader1.rPic.DataBindings.Add("Image", dtbl, "Logo");
                    }
                    rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "TransDate");

                    rep_InvMain.xrBarCode.Text = intINV.ToString();


                    PosDataObject.POS objPOS2 = new PosDataObject.POS();
                    objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl1 = objPOS2.FetchGiftAidDetails(intINV, Settings.TaxInclusive, false);

                    string c_name = "";
                    string c_address = "";
                    string c_total = "";


                    foreach (DataRow dr in dtbl1.Rows)
                    {
                        c_name = dr["DiscountText"].ToString();
                        c_address = dr["Notes"].ToString();
                        c_total = dr["TotalPrice"].ToString();
                    }

                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.rlCustName.Text = "Name";
                    rep_InvHeader2.rCustName.Text = c_name;
                    if (c_address.Trim() != "")
                    {
                        rep_InvHeader2.rlCustID.Text = "Address";
                        rep_InvHeader2.rCustID.Text = c_address;
                    }
                    else
                    {
                        rep_InvHeader2.rlCustID.Text = "";
                    }

                    rep_InvHeader2.rlCompany.Text = "";

                    rep_InvMain.subrepLine.ReportSource = rep_InvLine;
                    rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                    rep_InvLine.rlIem.Text = "Gift Aid";
                    rep_InvLine.rlTotal.Text = c_total;








                    PosDataObject.POS objPOS4 = new PosDataObject.POS();
                    objPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl3 = objPOS4.FetchInvoiceTender(intTranNo);
                    dtbl3 = RearrangeTenderForCashBack(intTranNo, dtbl3);

                    bool boolHATender = false;
                    bool boolSCrdtTender = false;

                    double dblTenderAmt = 0;
                    int TenderCount = 0;
                    TenderCount = dtbl3.Rows.Count;
                    foreach (DataRow dr1 in dtbl3.Rows)
                    {
                        if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                        dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());

                        if (dr1["Name"].ToString() == "House Account") boolHATender = true;
                        if (dr1["Name"].ToString() == "Store Credit") boolSCrdtTender = true;
                    }

                    rep_InvMain.subrepTender.ReportSource = rep_InvTendering;
                    rep_InvTendering.SubTotal.Text = "Total";
                    rep_InvTendering.Report.DataSource = dtbl3;
                    rep_InvTendering.DecimalPlace = Settings.DecimalPlace;

                    if (boolHATender && (Settings.HouseAccountBalanceInReceipt == "Y"))
                    {
                        PosDataObject.POS obcc99 = new PosDataObject.POS();
                        obcc99.Connection = SystemVariables.Conn;
                        double dval = obcc99.FetchHouseAccountBalanceForThisReceipt(intINV, intCID);
                        rep_InvMain.subrepHA.ReportSource = rep_InvHA;
                        rep_InvHA.DecimalPlace = Settings.DecimalPlace;
                        rep_InvHA.rAmt.Text = dval.ToString();
                    }

                    if (boolSCrdtTender)
                    {
                        PosDataObject.POS objscrtbal = new PosDataObject.POS();
                        objscrtbal.Connection = SystemVariables.Conn;
                        double dval = objscrtbal.GetCustomerStoreCreditBalance(intCID);
                        rep_InvMain.subrepSCrdt.ReportSource = rep_InvSC;
                        rep_InvSC.DecimalPlace = Settings.DecimalPlace;
                        rep_InvSC.rAmt.Text = dval.ToString();
                    }

                    if (TenderCount == 0) rep_InvTendering.lbTenderText.Text = "";

                    if (Settings.ShowFeesInReceipt == "Y")
                    {

                        bool bfdata = false;
                        bool bftx = false;
                        DataTable dFees = FetchInvFees(intINV);
                        if (dblFees + dblFeesCoupon != 0)
                        {
                            if (dFees.Rows.Count == 1) rep_InvTendering.lbFees.Text = dFees.Rows[0]["FeesName"].ToString();
                            rep_InvTendering.rFees.Text = (dblFees + dblFeesCoupon).ToString();
                            rep_InvTendering.rFees.Visible = true;
                            rep_InvTendering.lbFees.Visible = true;
                        }
                        else
                        {
                            bfdata = true;
                        }

                        if (dblFeesTax + dblFeesCouponTax != 0)
                        {
                            if (dFees.Rows.Count == 1) rep_InvTendering.lbFeeTx.Text = dFees.Rows[0]["FeesName"].ToString() + " " + "Tax";
                            rep_InvTendering.rFeeTx.Text = (dblFeesTax + dblFeesCouponTax).ToString();
                            rep_InvTendering.rFeeTx.Visible = true;
                            rep_InvTendering.lbFeeTx.Visible = true;
                        }
                        else
                        {
                            bftx = true;
                        }
                        if ((bfdata) && (bftx))
                        {
                            rep_InvTendering.ReportHeader.Visible = false;
                        }
                    }

                    if ((intHeaderStatus == 15) && (calcrent == "Y")) rep_InvTendering.rTotal.Text = dblRentDeposit.ToString();
                    else if ((intHeaderStatus == 15) && (calcrent == "N")) rep_InvTendering.rTotal.Text = (dblOrderTotal + dblRentDeposit).ToString();
                    else rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();

                    //rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();

                    rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                    rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");

                    rep_InvTendering.rlbAdvance.Text = "";
                    rep_InvTendering.rAdvance.Text = "";
                    rep_InvTendering.rlbDue.Text = "";
                    rep_InvTendering.rDue.Text = "";

                    rep_InvTendering.rtr1.HeightF = 1.0f;
                    rep_InvTendering.rtr2.HeightF = 1.0f;
                    rep_InvTendering.rtbl.HeightF = 55.0f;
                    rep_InvTendering.PageHeader.HeightF = 55.0f;

                    double EffectiveTotal = 0;
                    if ((intHeaderStatus == 15) && (calcrent == "Y")) EffectiveTotal = dblRentDeposit;
                    else if ((intHeaderStatus == 15) && (calcrent == "N")) EffectiveTotal = dblOrderTotal + dblRentDeposit;
                    else if (intHeaderStatus == 17) EffectiveTotal = dblRepairAdvanceAmount;
                    else if (intHeaderStatus == 18) EffectiveTotal = dblRepairAmount - dblRepairAdvanceAmount;
                    else EffectiveTotal = dblOrderTotal;

                    if (dblTenderAmt != EffectiveTotal)
                    {
                        rep_InvTendering.ChangeDue = true;
                        rep_InvTendering.ReportFooter.Visible = true;
                        rep_InvTendering.rChangeDueText.Text = Properties.Resources.Change;
                        rep_InvTendering.rChangeDue.Text = Convert.ToString(dblTenderAmt - EffectiveTotal);
                    }
                    else
                    {
                        rep_InvTendering.ChangeDue = false;
                        rep_InvTendering.ReportFooter.Visible = false;
                    }

                    if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
                    {
                        //rep_InvTendering.ChangeDue = false;
                        //rep_InvTendering.ReportFooter.Visibility=Visibility.Collapsed;
                    }

                    if (Settings.POSShowGiftCertBalance == "Y")
                    {
                        PosDataObject.POS objPOS5 = new PosDataObject.POS();
                        objPOS5.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        dtbl4 = objPOS5.ActiveGiftCert(intINV, Settings.CentralExportImport, Settings.StoreCode);
                        if (dtbl4.Rows.Count > 0)
                        {
                            rep_InvMain.subrepGC.ReportSource = rep_InvGC;
                            rep_InvGC.Report.DataSource = dtbl4;
                            rep_InvGC.DecimalPlace = Settings.DecimalPlace;
                            rep_InvGC.rGCHeader.Text = Properties.Resources.Gift_Cert__with_balance_as_on__ + DateTime.Today.Date.ToShortDateString();
                            rep_InvGC.rGCName.DataBindings.Add("Text", dtbl4, "GC");
                            rep_InvGC.rGCAmt.DataBindings.Add("Text", dtbl4, "GCAMT");
                        }
                    }

                    // EBT Balance on Receipt

                    PosDataObject.POS objPOS87 = new PosDataObject.POS();
                    objPOS87.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    DataTable dtblEBT = objPOS87.FetchEBTBalanceFromReceipt(intINV);
                    if (dtblEBT.Rows.Count > 0)
                    {
                        OfflineRetailV2.Report.Sales.repInvEBT rep_InvEBT = new OfflineRetailV2.Report.Sales.repInvEBT();
                        rep_InvMain.subrepEBT.ReportSource = rep_InvEBT;
                        rep_InvEBT.Report.DataSource = dtblEBT;
                        rep_InvEBT.DecimalPlace = Settings.DecimalPlace;

                        rep_InvEBT.rEBTCard.DataBindings.Add("Text", dtblEBT, "CardNo");
                        rep_InvEBT.rEBTBal.DataBindings.Add("Text", dtblEBT, "CardBalance");
                    }

                    int prmmgc = 0;
                    PosDataObject.POS obcc01mgc = new PosDataObject.POS();
                    obcc01mgc.Connection = SystemVariables.Conn;
                    prmmgc = obcc01mgc.GetTranIDFromInvoiceID(intINV);
                    DataTable ccdtbl11mgc = new DataTable();
                    PosDataObject.POS obcc11mgc = new PosDataObject.POS();
                    obcc11mgc.Connection = SystemVariables.Conn;
                    ccdtbl11mgc = obcc11mgc.FetchMercuryGiftCardData(prmmgc);

                    if (ccdtbl11mgc.Rows.Count > 0)
                    {
                        rep_InvMain.subrepMGC.ReportSource = rep_InvMGC;
                        rep_InvMGC.Report.DataSource = ccdtbl11mgc;
                        rep_InvMGC.DecimalPlace = Settings.DecimalPlace;
                        rep_InvMGC.rGCName.DataBindings.Add("Text", ccdtbl11mgc, "RefCardAct");
                        rep_InvMGC.rGCAmt.DataBindings.Add("Text", ccdtbl11mgc, "RefCardBalance");
                    }

                    if (Settings.POSPrintInvoice == 0)
                    {
                        //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                        try
                        {
                            if (Settings.ReportPrinterName != "") rep_InvMain.PrinterName = Settings.ReportPrinterName;
                            rep_InvMain.CreateDocument();
                            rep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                            rep_InvMain.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_InvMain.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_InvMain;
                            window.ShowDialog();

                        }
                        finally
                        {
                            rep_InvMain.Dispose();
                            rep_InvHeader1.Dispose();
                            rep_InvHeader2.Dispose();
                            rep_InvLine.Dispose();
                            rep_InvSubtotal.Dispose();
                            rep_InvTax.Dispose();
                            rep_InvTendering.Dispose();
                            rep_InvGC.Dispose();
                            rep_InvHA.Dispose();
                            rep_InvSign.Dispose();
                            rep_InvCoupon.Dispose();

                            dtbl.Dispose();
                            dtbl1.Dispose();
                            dtbl2.Dispose();
                            dtbl3.Dispose();
                            dtbl4.Dispose();
                            dtbl5.Dispose();
                            ccdtbl11mgc.Dispose();
                        }
                    }

                    if (Settings.POSPrintInvoice == 1)
                    {
                        try
                        {
                            rep_InvMain.CreateDocument();
                            rep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                            rep_InvMain.PrinterName = Settings.ReceiptPrinterName;
                            GeneralFunctions.PrintReport(rep_InvMain);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            rep_InvMain.Dispose();
                            rep_InvHeader1.Dispose();
                            rep_InvHeader2.Dispose();
                            rep_InvLine.Dispose();
                            rep_InvSubtotal.Dispose();
                            rep_InvTax.Dispose();
                            rep_InvTendering.Dispose();
                            rep_InvGC.Dispose();
                            rep_InvHA.Dispose();
                            rep_InvCoupon.Dispose();
                            rep_InvSign.Dispose();
                            dtbl.Dispose();
                            dtbl1.Dispose();
                            dtbl2.Dispose();
                            dtbl3.Dispose();
                            dtbl4.Dispose();
                            dtbl5.Dispose();
                        }
                    }
                }
            }
        }

        private bool IsGCSales(int pinvno)
        {
            if (Settings.PrintDuplicateGiftCertSaleReceipt == "N")
            {
                return false;
            }
            else
            {
                PosDataObject.POS objposTT = new PosDataObject.POS();
                objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
                return objposTT.CheckIfGiftCertSaleReceipt(pinvno);

            }
        }

        // Customised Tagged Item in Invoice
        private void RearrangeForTaggedItemInInvoice(DataTable dtbl)
        {
            foreach (DataRow dr in dtbl.Rows)
            {
                if ((dr["ProductType"].ToString() == "T") && (dr["TaggedInInvoice"].ToString() == "Y"))
                {
                    double qty = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                    DataTable ptbl = new DataTable();
                    PosDataObject.Product objp = new PosDataObject.Product();
                    objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    ptbl = objp.FetchTaggedData(GeneralFunctions.fnInt32(dr["ProductID"].ToString()));
                    string str = "";
                    foreach (DataRow dr1 in ptbl.Rows)
                    {
                        double val = qty * GeneralFunctions.fnDouble(dr1["ItemQty"].ToString());
                        if (str == "")
                        {
                            str = dr1["ItemName"].ToString() + "   " + val.ToString();
                        }
                        else
                        {
                            str = str + "\n" + dr1["ItemName"].ToString() + "   " + val.ToString();
                        }
                    }
                    string pval = dr["Description"].ToString() + "\n" + str;
                    dr["Description"] = pval;
                }
            }
        }

        // Customise Price, Normal Price, Manual Weight Data
        private void RearrangeForLineDisplay(DataTable dtbl)
        {
            foreach (DataRow dr in dtbl.Rows)
            {
                if (dr["ProductType"].ToString() == "W")
                {
                    dr["Price"] = dr["Price"].ToString() == "" ? "" : GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(dr["Price"].ToString()).ToString()) + "/" + dr["UOM"].ToString();
                    dr["NormalPrice"] = dr["NormalPrice"].ToString() == "" ? "" : GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(dr["NormalPrice"].ToString()).ToString()) + "/" + dr["UOM"].ToString();
                }
                else
                {
                    dr["Price"] = dr["Price"].ToString() == "" ? "" : GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(dr["Price"].ToString()).ToString());
                    dr["NormalPrice"] = dr["NormalPrice"].ToString() == "" ? "" : GeneralFunctions.FormatDoubleForPrint(GeneralFunctions.fnDouble(dr["NormalPrice"].ToString()).ToString());
                }

                dr["Qty"] = dr["Qty"].ToString() == "" ? "" : GetDisplayQty(dr["Qty"].ToString(), dr["QtyDecimal"].ToString(), dr["ProductType"].ToString(), dr["UOM"].ToString());

                if ((Settings.ScaleDevice == "(None)") && (dr["ProductType"].ToString() == "W"))
                {
                    dr["ExtraValue1"] = "Manual Weight";
                }
            }
        }

        private string GetDisplayQty(string pQty, string pDecimal, string pProdType, string pProdUOM)
        {
            string ReturnS = pQty;
            bool minusval = false;
            if (pQty.StartsWith("-"))
            {
                minusval = true;
                pQty = pQty.Remove(0, 1);
            }
            if (pDecimal == "")
            {
                ReturnS = pQty;
            }
            else
            {
                if ((pProdType == "Z") || (pProdType == "C") || (pProdType == "H"))
                {
                    ReturnS = pQty;
                }
                else
                {
                    decimal dQty = GeneralFunctions.fnDecimal(pQty);
                    int IPart = (int)Decimal.Truncate(dQty);
                    Decimal decimal_part = dQty - Decimal.Truncate(IPart);
                    if (decimal_part == 0)
                    {
                        ReturnS = IPart.ToString();
                    }
                    else
                    {
                        if (pDecimal == "0")
                        {
                            ReturnS = pQty;
                        }
                        else
                        {
                            string TempDecimal = "";
                            string strDecimal = decimal_part.ToString();
                            TempDecimal = strDecimal.Substring(2);

                            if (pDecimal == "1") ReturnS = String.Format("{0:0.0}", GeneralFunctions.fnDecimal(IPart + "." + TempDecimal));
                            if (pDecimal == "2") ReturnS = String.Format("{0:0.00}", GeneralFunctions.fnDecimal(IPart + "." + TempDecimal));
                            if (pDecimal == "3") ReturnS = String.Format("{0:0.000}", GeneralFunctions.fnDecimal(IPart + "." + TempDecimal));
                        }
                    }
                }
            }
            if (pProdType == "W")
            {
                ReturnS = ReturnS + " " + pProdUOM;
            }
            if (minusval)
            {
                ReturnS = "(" + ReturnS + ")";
            }
            return ReturnS;
        }

        /// Check if Card Payment Exists against the Transaction
        private bool IsCardPayment(int intTrnNo)
        {
            PosDataObject.POS objposTT = new PosDataObject.POS();
            objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposTT.IsCardPayment(intTrnNo);
        }

        /// Check if House Account Payment Exists against the Transaction
        private bool IsHAPayment(int intTrnNo)
        {
            PosDataObject.POS objposTT = new PosDataObject.POS();
            objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposTT.IsHouseAccountPayment(intTrnNo);
        }

        private int FetchWorkorderNo(int INV)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objPOS.FetchWorkOrderNo(INV);
        }

        private string FetchWorkorderDate(int INV)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objPOS.FetchWorkOrderDate(INV);
        }

        // Check if Mercury Card Payment Exists against the transaction
        private bool IsMercuryCardPayment(int intTrnNo)
        {
            PosDataObject.POS objposTT = new PosDataObject.POS();
            objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposTT.IsMercuryCreditCardPayment(intTrnNo);
        }

        // Get Mercury Card Payment Amount against the transaction
        private double GetMercuryCardPaymentAmount(int intTrnNo)
        {
            PosDataObject.POS objposTT = new PosDataObject.POS();
            objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposTT.GetMercuryCreditCardPayment(intTrnNo);
        }

        private async void cmbDate_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if ((cmbDate.SelectedIndex == 0) || (cmbDate.SelectedIndex == 2))
            {
                //pictureBox1.Visible = false;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Collapsed;
                dtT.Visibility = Visibility.Collapsed;
            }
            if (cmbDate.SelectedIndex == 1)
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = true;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Visible;
            }
            if (blFetch) await FetchHeaderData();
        }

        private async void dtF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtF.EditValue == null) return;
            if (blFetch) await FetchHeaderData();
        }

        private async void dtT_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (dtT.EditValue.ToString() == "") return;
            if (blFetch) await FetchHeaderData();
        }

        private async void cmbAmount_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbAmount.SelectedIndex == 0)
            {
                numF.Visibility = Visibility.Collapsed;
                numT.Visibility = Visibility.Collapsed;
            }
            if ((cmbAmount.SelectedIndex == 1) || (cmbAmount.SelectedIndex == 2))
            {
                numF.Visibility = Visibility.Visible;
                numT.Visibility = Visibility.Collapsed;
            }
            if (cmbAmount.SelectedIndex == 3)
            {
                numF.Visibility = Visibility.Visible;
                numT.Visibility = Visibility.Visible;
            }
            if (blFetch) await FetchHeaderData();
        }

        private async void numF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void numT_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private DataTable RearrangeTenderForCashBack(int pTranNo, DataTable dtbl)
        {
            DataTable refData = dtbl.Clone();

            foreach (DataRow dr in dtbl.Rows)
            {
                refData.Rows.Add(new object[] { dr["Name"].ToString(), dr["Amount"].ToString(), dr["Name"].ToString() });
                if (dr["Name"].ToString() == "Debit Card")
                {
                    double cashbk = 0;
                    cashbk = FetchCashBack(pTranNo, GeneralFunctions.fnDouble(dr["Amount"].ToString()));
                    if (cashbk != 0)
                    {
                        refData.Rows.Add(new object[] { "Cash Back", cashbk.ToString(), "Cash Back" });
                        refData.Rows.Add(new object[] { "Debit Card Total", (GeneralFunctions.fnDouble(dr["Amount"].ToString()) + cashbk).ToString(), "Debit Card Total" });
                    }
                }
            }

            return refData;
        }

        /// Get Cash Back Amount from Transaction
        private double FetchCashBack(int TrnNo, double Amt)
        {
            PosDataObject.POS objpos3 = new PosDataObject.POS();
            objpos3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos3.GetCashBackAmountFromCardTransaction1(TrnNo, Amt);
        }

        private async void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnVoid.IsEnabled = false;

                int intCardTranID = 0;
                if (gridView2.FocusedRowHandle < 0) return;

                /*if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCustID)) != 0)
                {
                    if (!CheckActiveCustomer(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCustID))))
                    {
                        DocMessage.MsgInformation(Properties.Resources."Transaction can not be possible for an inactive customer");
                        return;
                    }
                }*/

                int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
                if (new MessageBoxWindow().Show(Properties.Resources.Do_you_want_to_void_this_invoice__, Properties.Resources.Void_Transaction, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    bool flag = true;
                    if (ProceedForVoid(INV))
                    {
                        if (IsCardPayment1(INV))
                        {
                            if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 3))
                            {
                                PrecidiaLogFile = "";

                                PrecidiaLogFile = "Void_" + INV.ToString() + "_" + DateTime.Now.ToString(SystemVariables.DateFormat.Replace("/", "").Replace("-", "")) + "_" + DateTime.Now.ToString("HHmmss") + ".txt";

                                PrecidiaLogPath = PrecidiaLogFilePath();

                                WriteToPrecidiaLogFile("Start: " + DateTime.Now.ToString((SystemVariables.DateFormat.Replace("/", "-")) + " HH:mm:ss"));
                            }
                            BalanceAmount = 0;

                            int trn = GetTranID(INV);
                            DataTable dt = new DataTable();
                            dt = GetCardTransData(trn);
                            int CCID = 0;
                            string val1 = "";
                            string val2 = "";
                            string val3 = "";
                            string val4 = "";
                            string val5 = "";
                            string val6 = "";
                            string val7 = "";
                            string val8 = "";
                            string val9 = "";
                            string val10 = "";
                            string val20 = "";
                            string val21 = "";
                            int pmntgwy = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                CCID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                                val1 = dr["CardType"].ToString();
                                val2 = dr["CardAmount"].ToString();
                                val3 = dr["Reference"].ToString();
                                val9 = dr["MercuryProcessData"].ToString();
                                val4 = dr["MercuryInvoiceNo"].ToString();
                                val5 = dr["MercuryAcqRef"].ToString();
                                val6 = dr["MercuryToken"].ToString();
                                val7 = dr["AuthCode"].ToString();
                                val8 = dr["IsDebitCard"].ToString();
                                val10 = dr["RefCardTranID"].ToString();
                                val20 = dr["TransactionType"].ToString();
                                val21 = dr["RefCardAuthAmount"].ToString();
                                pmntgwy = GeneralFunctions.fnInt32(dr["PaymentGateway"].ToString());

                                // element temp out
                                /*
                                if (pmntgwy == 1) // element
                                {
                                    try
                                    {
                                        PosDataObject.POS objcard1 = new PosDataObject.POS();
                                        objcard1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                        objcard1.CustomerID = 0;
                                        objcard1.LoginUserID = SystemVariables.CurrentUserID;
                                        objcard1.EmployeeID = SystemVariables.CurrentUserID;
                                        objcard1.CardType = val1;
                                        objcard1.CardAmount = GeneralFunctions.fnDouble(val2);
                                        objcard1.PaymentGateway = Settings.PaymentGateway;

                                        try
                                        {
                                            string strerr = objcard1.InsertCardTrans();
                                        }
                                        catch (Exception ex)
                                        {
                                            new MessageBoxWindow().Show(Properties.Resources."Error occured during transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                            GeneralFunctions.SetTransactionLog("Catch - Error inserting Card Trans", ex.Message);
                                            Cursor.Current = Cursors.Default;
                                            break;
                                        }

                                        intCardTranID = objcard1.CardTranID;

                                        ElementExpress.ElementPS pg = new ElementExpress.ElementPS();
                                        pg.ElementApplicationID = Settings.ElementHPApplicationID;
                                        pg.ElementAccountID = Settings.ElementHPAccountID;
                                        pg.ElementAccountToken = Settings.ElementHPAccountToken;
                                        pg.ElementAcceptorID = Settings.ElementHPAcceptorID;
                                        pg.TranAmount = GeneralFunctions.FormatDouble1(GeneralFunctions.fnDouble(val2));
                                        pg.ElementTerminalID = Settings.ElementHPTerminalID.PadLeft(4, '0');
                                        pg.TranID = val10;
                                        pg.RefNo = val3;
                                        pg.TktNo = intCardTranID.ToString();
                                        pg.ApplicationVersion = GeneralFunctions.PaymentGatewayApplicationVersion();

                                        string msg1 = "";
                                        string msg2 = "";

                                        if (Settings.ElementHPMode == 0) pg.CreditVoidSale(ref msg1, ref msg2);
                                        if (Settings.ElementHPMode == 1) pg.TestCreditVoidSale(ref msg1, ref msg2);

                                        if (msg1 == "0")
                                        {
                                            TranID = pg.TranID;
                                            AuthCode = pg.ApprovalNo;
                                            MercuryProcessData = pg.AcquirerData;
                                            RefNo = pg.RefNo;
                                            string updtstre = "";
                                            PosDataObject.POS objcard2 = new PosDataObject.POS();
                                            objcard2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                            objcard2.CardType = "";
                                            objcard2.MercuryInvNo = TranID;
                                            objcard2.AuthCode = AuthCode;
                                            objcard2.Reference = RefNo;
                                            objcard2.AcqRefData = "";
                                            objcard2.TokenData = "";
                                            objcard2.MercuryProcessData = "";
                                            objcard2.MercuryRecordNo = "";
                                            objcard2.MercuryResponseOrigin = "";
                                            objcard2.MercuryResponseReturnCode = "";
                                            objcard2.MercuryTextResponse = "";
                                            objcard2.MercuryPurchaseAmount = 0;
                                            objcard2.MercuryTranCode = "";
                                            objcard2.CardAmount = GeneralFunctions.fnDouble(ApprovedAmt);

                                            objcard2.RefCardAct = "";
                                            objcard2.RefCardLogo = "";
                                            objcard2.RefCardEntry = "";
                                            objcard2.RefCardAuthID = AuthCode;
                                            objcard2.RefCardTranID = TranID;
                                            objcard2.RefCardMerchID = RefNo;
                                            objcard2.RefCardAuthAmount = GeneralFunctions.fnDouble(val2);
                                            objcard2.CardTranID = intCardTranID;
                                            objcard2.CardTranType = "Void";
                                            objcard2.AdjustFlag = "Y";
                                            objcard2.RefCardBalAmount = 0;
                                            objcard2.PrintXml = "";
                                            updtstre = objcard2.UpdateCardResponse();

                                            if (updtstre != "") flag = false;
                                        }
                                        else
                                        {
                                            flag = false;
                                            new MessageBoxWindow().Show(msg2, "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        flag = false;
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                        break;
                                    }
                                }

                                if (pmntgwy == 2)  // mercury
                                {
                                    MercuryPayment.clsMercuryPymnt mp = new MercuryPayment.clsMercuryPymnt();
                                    mp.MerchantID = Settings.MercuryHPMerchantID;
                                    mp.UserID = Settings.MercuryHPUserID;
                                    mp.COMPort = GeneralFunctions.fnInt32(Settings.MercuryHPPort);
                                    mp.InvNo = val4;
                                    mp.RefNo = val3;
                                    mp.AuthID = val7;
                                    mp.AcqRefData = val5;
                                    mp.Token = val6;
                                    mp.PurchaseAmount = GeneralFunctions.fnDouble(val2);
                                    mp.MercuryProcessData = val9;
                                    string msg1 = "";
                                    if (val1 == "Credit Card")
                                    {
                                        if (Settings.ElementHPMode == 0) mp.CreditVoidSale(ref msg1);
                                        if (Settings.ElementHPMode == 1) mp.TestCreditVoidSale(ref msg1);
                                    }
                                    if ((val1 == "Credit Card (STAND-IN)") || (val1 == "Credit Card - Voice Auth (STAND-IN)"))
                                    {
                                        if (Settings.ElementHPMode == 0) mp.StandInCreditVoidSale(ref msg1);
                                        if (Settings.ElementHPMode == 1) mp.TestStandInCreditVoidSale(ref msg1);
                                    }
                                    if (val1 == "Debit Card")
                                    {
                                        if (Settings.ElementHPMode == 0) mp.DebitVoidSale(ref msg1);
                                        if (Settings.ElementHPMode == 1) mp.TestDebitVoidSale(ref msg1);
                                    }

                                    if (val1 == "Mercury Gift Card")
                                    {
                                        if (val20 == "Sale")
                                        {
                                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidSales(ref msg1);
                                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidSales(ref msg1);
                                        }
                                        if (val20 == "Issue")
                                        {
                                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidIssue(ref msg1);
                                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidIssue(ref msg1);
                                        }

                                        if (val20 == "Reload")
                                        {
                                            if (Settings.ElementHPMode == 0) mp.GiftCardVoidReload(ref msg1);
                                            if (Settings.ElementHPMode == 1) mp.TestGiftCardVoidReload(ref msg1);
                                        }
                                    }

                                    GeneralFunctions.CreateMercuryTransactionXML(mp.MercuryXmlResponse, val6);
                                    if (msg1.ToUpper().Trim() == "APPROVED")
                                    {
                                        AuthCode = mp.AuthID;
                                        TranID = mp.TranID;
                                        CardNum = mp.CardNumber;
                                        CardExMM = mp.CardExMM;
                                        CardExYY = mp.CardExYY;
                                        CardLogo = mp.CardLogo;
                                        CardType = mp.CardType;
                                        ApprovedAmt = mp.ApprovedAmt;
                                        RefNo = mp.RefNo;
                                        CardEntry = mp.CardEntry;
                                        Token = mp.Token;
                                        AcqRef = mp.AcqRefData;
                                        strMercuryMerchantID = mp.MerchantID;
                                        MercuryProcessData = mp.MercuryProcessData;
                                        MercuryPurchaseAmount = mp.PurchaseAmount;
                                        MercuryTranCode = mp.MercuryTranCode;
                                        MercuryRecordNo = mp.MercuryRecordNo;
                                        MercuryResponseOrigin = mp.MercuryResponseOrigin;
                                        MercuryResponseReturnCode = mp.MercuryResponseReturnCode;
                                        MercuryTextResponse = mp.MercuryTextResponse;

                                        if (AuthCode == null) AuthCode = "";
                                        if (TranID == null) TranID = "";
                                        if (CardNum == null) CardNum = "";
                                        if (CardExMM == null) CardExMM = "";
                                        if (CardExYY == null) CardExYY = "";
                                        if (CardLogo == null) CardLogo = "";
                                        if (CardType == null) CardType = "";
                                        if (ApprovedAmt == null) ApprovedAmt = "0";
                                        if (RefNo == null) RefNo = "";
                                        if (CardEntry == null) CardEntry = "";
                                        if (Token == null) Token = "";
                                        if (AcqRef == null) AcqRef = "";

                                        if (MercuryTranCode == null) MercuryTranCode = "";
                                        if (MercuryRecordNo == null) MercuryRecordNo = "";
                                        if (MercuryResponseOrigin == null) MercuryResponseOrigin = "";
                                        if (MercuryResponseReturnCode == null) MercuryResponseReturnCode = "";
                                        if (MercuryTextResponse == null) MercuryTextResponse = "";
                                        if (MercuryProcessData == null) MercuryProcessData = "";
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                        flag = false;
                                        break;
                                    }
                                }

                                if (pmntgwy == 3)  // precidia
                                {
                                    string resp = "";
                                    string resptxt = "";

                                    //GeneralFunctions.TNPCG_Response("CCSALE", dblCardAmt, ref resp, ref resptxt);

                                    if (val1 == "Credit Card")
                                    {
                                        CGtrantype = "CCVOID";
                                    }
                                    if ((val1 == "Credit Card - Voice Auth") || (val1 == "Credit Card (STAND-IN)") || (val1 == "Credit Card - Voice Auth (STAND-IN)"))
                                    {
                                        CGtrantype = "CCVOID";
                                    }
                                    if (val1 == "Debit Card")
                                    {
                                        CGtrantype = "DCREFUND";
                                    }

                                    if (val1 == "Precidia Gift Card")
                                    {
                                        if (val20 == "Sale")
                                        {
                                            CGtrantype = "GCVOID";
                                        }
                                        if (val20 == "Issue")
                                        {
                                            CGtrantype = "GCVOIDACTIVATE";
                                        }

                                        if (val20 == "Reload")
                                        {
                                            CGtrantype = "GCVOID";
                                        }
                                    }

                                    if ((val1 == "Food Stamps") || (val1 == "EBT Cash") || (val1 == "EBT Voucher"))
                                    {
                                        CGtrantype = "EBTVOID";
                                    }

                                    WriteToPrecidiaLogFile("Start : " + CGtrantype);

                                    CGamt = GeneralFunctions.fnDouble(val2);

                                    XmlDocument XDoc = new XmlDocument();

                                    // Create root node.
                                    XmlElement XElemRoot = XDoc.CreateElement("PLRequest");

                                    XDoc.AppendChild(XElemRoot);

                                    XmlElement XTemp = XDoc.CreateElement("Command");
                                    XTemp.InnerText = CGtrantype;
                                    XElemRoot.AppendChild(XTemp);

                                    if (val1 != "Debit Card")
                                    {
                                        XTemp = XDoc.CreateElement("RecNum");
                                        XTemp.InnerText = val10.ToString();
                                        XElemRoot.AppendChild(XTemp);
                                    }

                                    XTemp = XDoc.CreateElement("Amount");
                                    XTemp.InnerText = CGamt.ToString("f");
                                    XElemRoot.AppendChild(XTemp);

                                    if (val1 == "Debit Card")
                                    {
                                        XTemp = XDoc.CreateElement("Input");
                                        XTemp.InnerText = Settings.PrecidiaUsePINPad == "Y" ? "EXTERNAL" : "SWIPED";
                                        XElemRoot.AppendChild(XTemp);
                                    }

                                    XmlDocument XmlResponse = new XmlDocument();

                                    XTemp = XDoc.CreateElement("KeepAlive");
                                    XTemp.InnerText = "N";
                                    XElemRoot.AppendChild(XTemp);

                                    XTemp = XDoc.CreateElement("ClientMAC");
                                    XTemp.InnerText = Settings.PrecidiaClientMAC;
                                    XElemRoot.AppendChild(XTemp);

                                    WriteToPrecidiaLogFile("Request XML : \n" + XDoc.OuterXml);

                                    bool bTelnet = false;
                                    try
                                    {
                                        SslTcpClient.RunClient(Settings.PrecidiaPOSLynxMAC, Settings.PrecidiaPort, XDoc, ref XmlResponse);
                                        bTelnet = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        bTelnet = false;
                                        WriteToPrecidiaLogFile("Socket Error : " + ex.Message);
                                    }

                                    if (bTelnet)
                                    {
                                        if (XmlResponse.InnerXml != "")
                                        {
                                            WriteToPrecidiaLogFile("Response XML : \n" + XmlResponse.InnerXml);

                                            SocketResponse_General(XmlResponse);

                                            resp = CGresp;
                                            resptxt = CGresptxt;

                                            if (resp != "")
                                            {
                                                if (resp != "APPROVED")
                                                {
                                                    new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                                    flag = false;
                                                    break;
                                                }
                                                else
                                                {
                                                }
                                            }
                                        }
                                        else
                                        {
                                            new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                        flag = false;
                                        break;
                                    }
                                }

                                if (pmntgwy == 5)  // Datacap
                                {
                                    Dcap_CmdStatus = "";
                                    Dcap_TextResponse = "";
                                    Dcap_AcctNo = "";
                                    Dcap_Merchant = "";
                                    Dcap_TranCode = "";
                                    Dcap_CardType = "";
                                    Dcap_AuthCode = "";
                                    Dcap_RefNo = "";
                                    Dcap_AcqRefData = "";
                                    Dcap_RecordNo = "";
                                    Dcap_InvoiceNo = "";
                                    Dcap_TranAmt = 0;
                                    Dcap_AuthAmt = 0;
                                    Dcap_CashBkAmt = 0;
                                    Dcap_BalAmt = 0;

                                    bool bproceed = true;
                                    string request_xml = "";
                                    string response_xml = "";
                                    DSIPDCXLib.DsiPDCX dsipdx = new DSIPDCXLib.DsiPDCX();

                                    dsipdx.ServerIPConfig(Settings.DatacapServer, 1);

                                    if (val1 == "Credit Card")
                                    {
                                        request_xml = GeneralFunctions.Datacap_CreditVoidSale_Request_XML(GeneralFunctions.fnDouble(val2), val4, val3, val7, val5, false);

                                        try
                                        {
                                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                        }
                                        catch
                                        {
                                            bproceed = false;
                                        }
                                    }

                                    if (val1 == "Datacap Gift Card")
                                    {
                                        if (val20 == "Sale")
                                        {
                                            request_xml = GeneralFunctions.Datacap_PrePaidVoidSale_Request_XML(GeneralFunctions.fnDouble(val2), val4, val3, val7);
                                            try
                                            {
                                                response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                            }
                                            catch
                                            {
                                                bproceed = false;
                                            }
                                        }

                                        if (val20 == "Issue")
                                        {
                                            request_xml = GeneralFunctions.Datacap_PrePaidVoidIssue_Request_XML(GeneralFunctions.fnDouble(val2), val4, val3, val7);
                                            try
                                            {
                                                response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                            }
                                            catch
                                            {
                                                bproceed = false;
                                            }
                                        }

                                        if (val20 == "Reload")
                                        {
                                            request_xml = GeneralFunctions.Datacap_PrePaidVoidReload_Request_XML(GeneralFunctions.fnDouble(val2), val4, val3, val7);
                                            try
                                            {
                                                response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                            }
                                            catch
                                            {
                                                bproceed = false;
                                            }
                                        }
                                    }

                                    if (val1 == "Debit Card")
                                    {
                                        request_xml = GeneralFunctions.Datacap_DebitVoidSale_Request_XML(GeneralFunctions.fnDouble(val2), val4, val3, GeneralFunctions.fnDouble(val21) - GeneralFunctions.fnDouble(val2));

                                        try
                                        {
                                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                        }
                                        catch
                                        {
                                            bproceed = false;
                                        }
                                    }

                                    if (val1 == "Food Stamps")
                                    {
                                        request_xml = GeneralFunctions.Datacap_EBTReturn_Request_XML(GeneralFunctions.fnDouble(val2), val4, false);
                                        try
                                        {
                                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                        }
                                        catch
                                        {
                                            bproceed = false;
                                        }
                                    }

                                    if (val1 == "EBT Cash")
                                    {
                                        request_xml = GeneralFunctions.Datacap_EBTCashReturn_Request_XML(GeneralFunctions.fnDouble(val2), val4, false);
                                        try
                                        {
                                            response_xml = dsipdx.ProcessTransaction(request_xml, 1, null, null);
                                        }
                                        catch
                                        {
                                            bproceed = false;
                                        }
                                    }

                                    if (bproceed)
                                    {
                                        GeneralFunctions.Datacap_General_Response(response_xml, ref Dcap_CmdStatus, ref Dcap_TextResponse, ref Dcap_AcctNo, ref Dcap_Merchant, ref Dcap_TranCode,
                                            ref Dcap_CardType, ref Dcap_AuthCode, ref Dcap_InvoiceNo, ref Dcap_RefNo, ref Dcap_AcqRefData, ref Dcap_RecordNo,
                                            ref Dcap_TranAmt, ref Dcap_AuthAmt, ref Dcap_CashBkAmt, ref Dcap_BalAmt);

                                        if (Dcap_CmdStatus == "Approved")
                                        {
                                            MercuryResponseOrigin = "";
                                            MercuryResponseReturnCode = "";
                                            MercuryTextResponse = "";
                                            MercuryTranCode = "";
                                            CardType = val1;
                                            ApprovedAmt = Dcap_AuthAmt.ToString();
                                            MercuryProcessData = "";
                                            MercuryPurchaseAmount = Dcap_TranAmt;
                                            AuthCode = Dcap_AuthCode;
                                            TranID = Dcap_InvoiceNo;
                                            RefNo = Dcap_RefNo;
                                            AcqRef = Dcap_AcqRefData;
                                            Token = Dcap_RecordNo;
                                            CardNum = Dcap_AcctNo;
                                            CardLogo = Dcap_CardType;
                                            strMercuryMerchantID = Dcap_Merchant;
                                            BalanceAmount = Dcap_BalAmt;
                                        }
                                        else
                                        {
                                            new MessageBoxWindow().Show(Dcap_CmdStatus + "\n\n" + Dcap_TextResponse, "Void Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                        flag = false;
                                        break;
                                    }
                                }

                                if (pmntgwy == 6)  // Datacap EMV
                                {
                                    CallPadResetCount = 0;
                                    CallEMVPadReset();
                                    if (CallPadResetCount == 5)
                                    {
                                        CallPadResetCount = 0;
                                        if (Dcap_PPAD_CmdStatus != "Success") new MessageBoxWindow().Show(Dcap_PPAD_CmdStatus + "\n\n" + Dcap_PPAD_TextResponse, "Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                        Cursor.Current = Cursors.Default;
                                        flag = false;
                                        break;
                                    }

                                    Dcap_CmdStatus = "";
                                    Dcap_TextResponse = "";
                                    Dcap_AcctNo = "";
                                    Dcap_Merchant = "";
                                    Dcap_TranCode = "";
                                    Dcap_CardType = "";
                                    Dcap_AuthCode = "";
                                    Dcap_RefNo = "";
                                    Dcap_AcqRefData = "";
                                    Dcap_RecordNo = "";
                                    Dcap_InvoiceNo = "";
                                    Dcap_TranAmt = 0;
                                    Dcap_AuthAmt = 0;
                                    Dcap_CashBkAmt = 0;
                                    Dcap_ProcessData = "";

                                    bool bproceed = true;
                                    string request_xml = "";
                                    string response_xml = "";

                                    DSIEMVXLib.DsiEMVX dsipdx = new DSIEMVXLib.DsiEMVX();

                                    if (val20 == "Sale")
                                    {
                                        if (val1 == "Credit Card")
                                        {
                                            request_xml = GeneralFunctions.PrepareEMVVoidSaleXML(GeneralFunctions.fnDouble(val2), val4, val3, val7, val5, val8, val6, false, 0);

                                            try
                                            {
                                                response_xml = dsipdx.ProcessTransaction(request_xml);
                                                //response_xml = " <RStream><CmdResponse><ResponseOrigin>Processor</ResponseOrigin><DSIXReturnCode>000000</DSIXReturnCode> "
                                                            //+ " <CmdStatus>Approved</CmdStatus><TextResponse>AP</TextResponse><UserTraceData>Dev1</UserTraceData></CmdResponse> "
                                                           // + " <TranResponse><MerchantID>755847002</MerchantID><TerminalID>1</TerminalID><AcctNo>XXXXXXXXXXXX0010</AcctNo> "
                                                          //  + " <ExpDate>XXXX</ExpDate><CardType>VISA</CardType><TranCode>VoidSale</TranCode><AuthCode>VOIDED</AuthCode><CaptureStatus>Captured</CaptureStatus> "
                                                         //   + " <RefNo>1001</RefNo><InvoiceNo>151</InvoiceNo><OperatorID>test</OperatorID><Amount><Purchase>2.01</Purchase><Authorize>2.01</Authorize></Amount> "
                                                        //    + " <AcqRefData>K</AcqRefData><RecordNo>oxN/EylObgQWNmGeWFGEThWl37N9bhNWxyXCh7FpR4siEgUQEyIQDcZE</RecordNo><ProcessData>|A4|510100601000</ProcessData></TranResponse></RStream> ";
                                            }
                                            catch
                                            {
                                                bproceed = false;
                                            }
                                        }

                                        if (val1 == "Debit Card")
                                        {
                                            request_xml = GeneralFunctions.PrepareEMVVoidSaleXML(GeneralFunctions.fnDouble(val2), val4, val3, val7, val5, val8, val6, true, GeneralFunctions.fnDouble(val21) - GeneralFunctions.fnDouble(val2));

                                            try
                                            {
                                                response_xml = dsipdx.ProcessTransaction(request_xml);
                                            }
                                            catch
                                            {
                                                bproceed = false;
                                            }
                                        }
                                    }

                                    if (val20 == "Return")
                                    {
                                        request_xml = GeneralFunctions.PrepareEMVVoidReturnXML(GeneralFunctions.fnDouble(val2), val4, val3, val7, val5, val8, val6);

                                        try
                                        {
                                            response_xml = dsipdx.ProcessTransaction(request_xml);
                                        }
                                        catch
                                        {
                                            bproceed = false;
                                        }
                                    }

                                    if (bproceed)
                                    {
                                        GeneralFunctions.DatacapEMV_General_Response(response_xml, ref Dcap_CmdStatus, ref Dcap_TextResponse, ref Dcap_AcctNo, ref Dcap_Merchant, ref Dcap_TranCode,
                                            ref Dcap_CardType, ref Dcap_AuthCode, ref Dcap_InvoiceNo, ref Dcap_RefNo, ref Dcap_AcqRefData, ref Dcap_RecordNo,
                                            ref Dcap_TranAmt, ref Dcap_AuthAmt, ref Dcap_CashBkAmt, ref Dcap_ProcessData, ref Dcap_PrintDraft);

                                        if (Dcap_CmdStatus == "Approved")
                                        {
                                            GeneralFunctions.StoreResponseSequence(response_xml);
                                            MercuryResponseOrigin = "";
                                            MercuryResponseReturnCode = "";
                                            MercuryTextResponse = "";
                                            MercuryTranCode = Dcap_TranCode;
                                            MercuryProcessData = Dcap_ProcessData;
                                            CardType = val1;
                                            ApprovedAmt = Dcap_AuthAmt.ToString();
                                            MercuryProcessData = "";
                                            MercuryPurchaseAmount = Dcap_TranAmt;
                                            AuthCode = Dcap_AuthCode;
                                            TranID = Dcap_InvoiceNo;
                                            RefNo = Dcap_RefNo;
                                            AcqRef = Dcap_AcqRefData;
                                            Token = Dcap_RecordNo;
                                            CardNum = Dcap_AcctNo;
                                            CardLogo = Dcap_CardType;
                                            strMercuryMerchantID = Dcap_Merchant;
                                            BalanceAmount = Dcap_BalAmt;
                                        }
                                        else
                                        {
                                            new MessageBoxWindow().Show(Dcap_CmdStatus + "\n\n" + Dcap_TextResponse, "Void Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSReturnReprintDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                        flag = false;
                                        break;
                                    }
                                }

                                */

                                if (flag)
                                {
                                    PosDataObject.POS objcard = new PosDataObject.POS();
                                    objcard.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                    objcard.CustomerID = intCustID;
                                    objcard.LoginUserID = SystemVariables.CurrentUserID;
                                    objcard.EmployeeID = SystemVariables.CurrentUserID;
                                    objcard.CardType = CardType;//CardLogo
                                    objcard.IsDebit = val8;
                                    objcard.CardAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                                    objcard.PaymentGateway = pmntgwy;
                                    objcard.MercuryInvNo = TranID;
                                    objcard.MercuryProcessData = MercuryProcessData;
                                    objcard.MercuryTranCode = MercuryTranCode;
                                    objcard.MercuryPurchaseAmount = MercuryPurchaseAmount;
                                    objcard.AuthCode = AuthCode;
                                    objcard.Reference = RefNo;
                                    objcard.AcqRefData = AcqRef;
                                    objcard.TokenData = Token;
                                    objcard.MercuryRecordNo = MercuryRecordNo;
                                    objcard.MercuryResponseOrigin = MercuryResponseOrigin;
                                    objcard.MercuryResponseReturnCode = MercuryResponseReturnCode;
                                    objcard.MercuryTextResponse = MercuryTextResponse;

                                    objcard.RefCardAct = CardNum;
                                    objcard.RefCardLogo = CardLogo;
                                    objcard.RefCardEntry = CardEntry;
                                    objcard.RefCardAuthID = AuthCode;
                                    objcard.RefCardTranID = TranID;
                                    objcard.RefCardMerchID = strMercuryMerchantID;
                                    objcard.RefCardAuthAmount = GeneralFunctions.fnDouble(ApprovedAmt);
                                    objcard.RefCardBalAmount = BalanceAmount;
                                    objcard.CardTranType = "Void";
                                    objcard.TerminalName = Settings.TerminalName;
                                    objcard.LogFileName = "";

                                    try
                                    {
                                        string strerr = objcard.InsertCardTrans1();
                                        if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 3)) WriteToPrecidiaLogFile("Card Trans updated");
                                    }
                                    catch (Exception ex)
                                    {
                                        if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 3)) WriteToPrecidiaLogFile("Card Trans not updated");
                                        new MessageBoxWindow().Show(Properties.Resources.Error_occured_during_transaction, "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                        GeneralFunctions.SetTransactionLog("Catch - Error inserting Card Trans", ex.Message);
                                        Cursor = Cursors.Arrow;
                                        break;
                                    }
                                    intCardTranID = objcard.CardTranID;

                                    PosDataObject.POS ob = new PosDataObject.POS();
                                    ob.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                    ob.LoginUserID = SystemVariables.CurrentUserID;
                                    ob.CardTranID = CCID;
                                    string s = ob.UpdateCardAdjustment();
                                }
                                else
                                {
                                    if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 3)) WriteToPrecidiaLogFile("Void unsuccessful");
                                }
                            }
                        }

                        if (flag)
                        {
                            int VoidTran = 0;
                            PosDataObject.POS objp = new PosDataObject.POS();
                            objp.Connection = SystemVariables.Conn;
                            objp.LoginUserID = SystemVariables.CurrentUserID;
                            VoidTran = objp.VoidTransaction(INV, Settings.TerminalName);
                            if (VoidTran == 0)
                            {
                                if ((Settings.POSCardPayment == "Y") && (Settings.PaymentGateway == 3)) WriteToPrecidiaLogFile("Void successful");
                                DocMessage.MsgInformation(Properties.Resources.Invoice_No___ + INV.ToString() + " " + Properties.Resources.voided_successfully);
                                await FetchHeaderData();
                                if (gridView2.FocusedRowHandle >= 0)
                                {
                                    await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                                    intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                btnVoid.IsEnabled = true;
            }
        }

        /// Check if Card Payment Exists against the Invoice
        private bool IsCardPayment1(int prm)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            return objp.IsCardPayment1(prm);
        }

        private int GetTranID(int prm)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            return objp.GetTranID(prm);
        }

        private DataTable GetCardTransData(int prm)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            return objp.FetchCardTransData(prm);
        }

        /// Check if the Invoice can be voided
        private bool ProceedForVoid(int InvNo)
        {
            if (IsHouseAccount(InvNo))
            {
                DocMessage.MsgInformation(Properties.Resources.Can_not_void_this_invoice_as_House_Account_Payment_exists_against_this_invoice_);
                return false;
            }

            if (IsGiftCert(InvNo))
            {
                DocMessage.MsgInformation(Properties.Resources.Can_not_void_this_invoice_as_Gift_Certificate_exists_against_this_invoice_);
                return false;
            }

            /*if (IsHouseAccountTendering(InvNo))
            {
                DocMessage.MsgInformation(Properties.Resources."Can not void this invoice as House Account Tendering exists against this invoice.");
                return false;
            }*/

            if (IsStoreCreditTendering(InvNo))
            {
                DocMessage.MsgInformation(Properties.Resources.Can_not_void_this_invoice_as_Store_Credit_Tendering_exists_against_this_invoice_);
                return false;
            }

            return true;
        }

        /// Check if Store Credit Tendering Exists in Invoice
        private bool IsStoreCreditTendering(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsStoreCreditTenderingVoid(InvNo);
        }

        /// Check if House Account Tendering Exists in Invoice
        private bool IsHouseAccountTendering(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsHouseAccountTenderingVoid(InvNo);
        }

        /// Check if Gift Cert Exists in Invoice
        private bool IsGiftCert(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsGiftCertVoid(InvNo);
        }

        /// Check if House Account Payment Exists in Invoice
        private bool IsHouseAccount(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsHouseAccountPaymentVoid(InvNo);
        }

        private async void btnSelectTag_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_POSSelectTagItemDlq frm_POSSelectTagItemDlq = new frm_POSSelectTagItemDlq();
            try
            {
                frm_POSSelectTagItemDlq.ItemID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colID));
                frm_POSSelectTagItemDlq.ParentID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colPID));
                frm_POSSelectTagItemDlq.InvNo = CurrInv;
                frm_POSSelectTagItemDlq.SelectData = dtblSelectTag;
                frm_POSSelectTagItemDlq.ShowDialog();
                if (frm_POSSelectTagItemDlq.DialogResult == true)
                {
                    dtblSelectTag = frm_POSSelectTagItemDlq.SelectData;
                }
            }
            finally
            {
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnTagReturn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFunctionButton("31h")) return;
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colVoidNo) != "0") return;
            DataTable dtbl = new DataTable();
            dtbl = dtblSelectTag;
            double dqty = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                dqty = 0;
                dqty = -(GeneralFunctions.fnDouble(dr["QTY"].ToString())); //-(GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["ReturnedItemCnt"].ToString()));
                dtblReturnItem.Rows.Add(new object[] {dr["ID"].ToString(),
                                                dr["PID"].ToString(),
                                                dr["PNAME"].ToString(),
                                                dr["PTYPE"].ToString(),
                                                dr["PRICE"].ToString(),
                                                dqty.ToString()});

                DialogResult = true;
                ResMan.closeKeyboard();
                CloseKeyboards();
                Close();
            }
        }

        private void cmbC_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void cmbP_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void cmbE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Tab)
            {
                LookUpEdit edit = (LookUpEdit)sender;
                e.SuppressKeyPress = true;
                pos.SortAndSearchPopupLookUpEditForm f = (edit as DevExpress.Utils.Win.IPopupControl).PopupWindow as pos.SortAndSearchPopupLookUpEditForm;

                object val = f.CurrentValue;
                edit.ClosePopup();
                edit.EditValue = val;

                e.Handled = true;
            }
        }

        private void grdHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DevExpress.Xpf.Grid.TableViewHitInfo hi = gridView2.CalcHitInfo(e.GetPosition(gridView2));
                if (hi.InColumnHeader)
                {
                    _searchcol = hi.Column;
                }
            }
        }

        private void grdHeader_StartSorting(object sender, RoutedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            rowPos = view.GetVisibleIndex(view.FocusedRowHandle) - view.TopRowIndex;
        }

        private void grdHeader_EndSorting(object sender, RoutedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            view.TopRowIndex = view.GetVisibleIndex(view.FocusedRowHandle) - rowPos;
            //view.FocusedRowHandle = rowPos;
            //Todo: view.FocusedColumn = _searchcol;
        }

        private async void cmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            PopulateCustomer();
            if (blFetch) await FetchHeaderData();
        }

        private async void txtInv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtInv.Text.Trim() != "")
                {
                    await FetchHeaderData_Specific();
                    e.Handled = true;
                }
            }
        }



        #region Precidia Log

        private string PrecidiaLogFilePath()
        {
            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";

            //csConnPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Precidia Logs";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Precidia Logs";

            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + PrecidiaLogFile;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + PrecidiaLogFile;
            }
            return strfilename;
        }

        private void WriteToPrecidiaLogFile(string txt)
        {
            FileStream fileStrm;
            if (File.Exists(PrecidiaLogPath)) fileStrm = new FileStream(PrecidiaLogPath, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(PrecidiaLogPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }


        #endregion Precidia Log

        private async void btnKeyEnter_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtInv.Text.Trim() != "")
                {
                    await FetchHeaderData_Specific();
                }
            }
            finally
            {
                txtInv.Focus();
            }
        }

        private void btnDuplicateTest_Click(object sender, RoutedEventArgs e)
        {
            /*
           if (rg.SelectedIndex == 0)
           {
               int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));
               double AMT = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colAmount));

               CallPadResetCount = 0;
               CallEMVPadReset();
               if (CallPadResetCount == 5)
               {
                   CallPadResetCount = 0;
                   if (Dcap_PPAD_CmdStatus != "Success") new MessageBoxWindow().Show(Dcap_PPAD_CmdStatus + "\n\n" + Dcap_PPAD_TextResponse, "Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
               }
               else
               {
                   Dcap_CmdStatus = "";
                   Dcap_TextResponse = "";
                   Dcap_AcctNo = "";
                   Dcap_Merchant = "";
                   Dcap_TranCode = "";
                   Dcap_CardType = "";
                   Dcap_AuthCode = "";
                   Dcap_RefNo = "";
                   Dcap_AcqRefData = "";
                   Dcap_RecordNo = "";
                   Dcap_InvoiceNo = "";
                   Dcap_TranAmt = 0;
                   Dcap_AuthAmt = 0;
                   Dcap_CashBkAmt = 0;
                   Dcap_ProcessData = "";

                   bool bproceed = true;
                   string request_xml = "";
                   string response_xml = "";
                   DSIEMVXLib.DsiEMVX dsiEMV = new DSIEMVXLib.DsiEMVX();
                   PosDataObject.POS objPOS = new PosDataObject.POS();
                   objPOS.Connection = SystemVariables.Conn;
                   int dcap_max_inv = objPOS.FetchMaxInvoiceNo();

                   request_xml = GeneralFunctions.PrepareEMVDuplicateSaleXML(INV, AMT, false, Settings.DatacapEMVManual);
                   try
                   {
                       response_xml = dsiEMV.ProcessTransaction(request_xml);
                   }
                   catch
                   {
                       bproceed = false;
                   }

                   if (bproceed)
                   {
                       GeneralFunctions.DatacapEMV_General_Response(response_xml, ref Dcap_CmdStatus, ref Dcap_TextResponse, ref Dcap_AcctNo, ref Dcap_Merchant, ref Dcap_TranCode,
                           ref Dcap_CardType, ref Dcap_AuthCode, ref Dcap_InvoiceNo, ref Dcap_RefNo, ref Dcap_AcqRefData, ref Dcap_RecordNo,
                           ref Dcap_TranAmt, ref Dcap_AuthAmt, ref Dcap_CashBkAmt, ref Dcap_ProcessData, ref Dcap_PrintDraft);

                       //response_xml = " <RStream><CmdResponse><ResponseOrigin>Processor</ResponseOrigin><DSIXReturnCode>000000</DSIXReturnCode> "
                                                        //  + " <CmdStatus>Approved</CmdStatus><TextResponse>AP</TextResponse><UserTraceData>Dev1</UserTraceData></CmdResponse> "
                                                        //  + " <TranResponse><MerchantID>755847002</MerchantID><TerminalID>1</TerminalID><AcctNo>XXXXXXXXXXXX0010</AcctNo> "
                                                        //  + " <ExpDate>XXXX</ExpDate><CardType>VISA</CardType><TranCode>VoidSale</TranCode><AuthCode>VOIDED</AuthCode><CaptureStatus>Captured</CaptureStatus> "
                                                       //   + " <RefNo>1001</RefNo><InvoiceNo>151</InvoiceNo><OperatorID>test</OperatorID><Amount><Purchase>2.01</Purchase><Authorize>2.01</Authorize></Amount> "
                                                       //   + " <AcqRefData>K</AcqRefData><RecordNo>oxN/EylObgQWNmGeWFGEThWl37N9bhNWxyXCh7FpR4siEgUQEyIQDcZE</RecordNo><ProcessData>|A4|510100601000</ProcessData></TranResponse></RStream> ";

                       DataTable dtbl = new DataTable();
                       dtbl.Columns.Add("CmdStatus", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("TextResponse", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("AcctNo", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("ExpDate", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("RefNo", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("AuthCode", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("Purchase", System.Type.GetType("System.String"));
                       dtbl.Columns.Add("Authorize", System.Type.GetType("System.String"));
                   }
               }
           }*/
        }

        /// Get Fees and Charge against the invoice
        private DataTable FetchInvFees(int pInvNo)
        {
            PosDataObject.POS objpos1 = new PosDataObject.POS();
            objpos1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos1.FetchFeesInInvoice(pInvNo);
        }

        #region LayAway Refund

        // Check for valid refund
        private DataTable GetSelectedDatatable()
        {
            DataTable clonedGrdDetail = grdDetail.ItemsSource as DataTable;
            DataTable dtbl = dtblLayaway.Clone();
            foreach (DataRow dv in clonedGrdDetail.Rows)
            {
                if (Convert.ToBoolean(dv["CheckReturned"]))
                {
                    foreach (DataRow dv1 in dtblLayaway.Rows)
                    {
                        if (dv["InvoiceNo"].ToString() == dv1["InvoiceNo"].ToString())
                        {
                            dtbl.Rows.Add(new object[] { dv1["ID"].ToString(), dv1["LayawayNo"].ToString(), dv1["InvoiceNo"].ToString(), dv1["SKU"].ToString(),
                            dv1["Description"].ToString(), dv1["Qty"].ToString(), dv1["Cost"].ToString(), dv1["DateDue"].ToString(),
                            dv1["TotalSale"].ToString(), dv1["ItemID"].ToString(), dv1["ProductID"].ToString(), dv1["ProductType"].ToString(),
                            dv1["Payment"].ToString(), dv1["Balance"].ToString(), dv1["Deposit"].ToString(), dv1["MatrixOptionID"].ToString(),
                            dv1["OptionValue1"].ToString(), dv1["OptionValue2"].ToString(), dv1["OptionValue3"].ToString(), dv1["Image"],
                            dv1["CustomerID"].ToString()});

                        }
                    }
                }
            }

            return dtbl;
        }
        private bool IsValidRefund()
        {
            int tempinv = 0;
            DataTable dtbl = GetSelectedDatatable();
            double totDeposit = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                LawAwayRecordCustID = Convert.ToInt32(dr["CustomerID"].ToString());
                totDeposit = totDeposit + GeneralFunctions.fnDouble(dr["Deposit"].ToString());
                tempinv = GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString());
                if (tempinv > intMaxInvNo) intMaxInvNo = tempinv;
            }
            if (dtbl.Rows.Count == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.No_item_selected_for_Refund, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                dtbl.Dispose();
                return false;
            }
            dtbl.Dispose();
            return true;
        }

        private void CreateDataTable()
        {
            dtblPOS = new DataTable();
            dtblPOS.Columns.Add("ID", System.Type.GetType("System.String"));//1
            dtblPOS.Columns.Add("PRODUCT", System.Type.GetType("System.String"));//2
            dtblPOS.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));//3
            dtblPOS.Columns.Add("ONHANDQTY", System.Type.GetType("System.String"));//4
            dtblPOS.Columns.Add("NORMALQTY", System.Type.GetType("System.String"));//5
            dtblPOS.Columns.Add("COST", System.Type.GetType("System.String"));//6
            dtblPOS.Columns.Add("QTY", System.Type.GetType("System.String"));//7
            dtblPOS.Columns.Add("RATE", System.Type.GetType("System.Double"));//8
            dtblPOS.Columns.Add("NRATE", System.Type.GetType("System.String"));//9
            dtblPOS.Columns.Add("PRICE", System.Type.GetType("System.Double"));//10
            dtblPOS.Columns.Add("UOMCOUNT", System.Type.GetType("System.String"));//11
            dtblPOS.Columns.Add("UOMPRICE", System.Type.GetType("System.String"));//12
            dtblPOS.Columns.Add("UOMDESC", System.Type.GetType("System.String"));//13
            dtblPOS.Columns.Add("MATRIXOID", System.Type.GetType("System.String"));//14
            dtblPOS.Columns.Add("MATRIXOV1", System.Type.GetType("System.String"));//15
            dtblPOS.Columns.Add("MATRIXOV2", System.Type.GetType("System.String"));//16
            dtblPOS.Columns.Add("MATRIXOV3", System.Type.GetType("System.String"));//17
            dtblPOS.Columns.Add("UNIQUE", System.Type.GetType("System.String"));//18
            dtblPOS.Columns.Add("DP", System.Type.GetType("System.String"));//19
            dtblPOS.Columns.Add("NOTES", System.Type.GetType("System.String"));//20

            dtblPOS.Columns.Add("DISCLOGIC", System.Type.GetType("System.String"));//21
            dtblPOS.Columns.Add("DISCVALUE", System.Type.GetType("System.String"));//22
            dtblPOS.Columns.Add("DISCOUNT", System.Type.GetType("System.String"));//23
            dtblPOS.Columns.Add("DISCOUNTID", System.Type.GetType("System.String"));//24
            dtblPOS.Columns.Add("DISCOUNTTEXT", System.Type.GetType("System.String"));//25
            dtblPOS.Columns.Add("ITEMINDEX", System.Type.GetType("System.String"));//26

            // for blankline
            dtblPOS.Columns.Add("TAXID1", System.Type.GetType("System.String"));//27
            dtblPOS.Columns.Add("TAXID2", System.Type.GetType("System.String"));//28
            dtblPOS.Columns.Add("TAXID3", System.Type.GetType("System.String"));//29
            dtblPOS.Columns.Add("TAXNAME1", System.Type.GetType("System.String"));//30
            dtblPOS.Columns.Add("TAXNAME2", System.Type.GetType("System.String"));//31
            dtblPOS.Columns.Add("TAXNAME3", System.Type.GetType("System.String"));//32
            dtblPOS.Columns.Add("TAXRATE1", System.Type.GetType("System.String"));//33
            dtblPOS.Columns.Add("TAXRATE2", System.Type.GetType("System.String"));//34
            dtblPOS.Columns.Add("TAXRATE3", System.Type.GetType("System.String"));//35
            dtblPOS.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));//36
            dtblPOS.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));//37
            dtblPOS.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));//38

            // service type
            dtblPOS.Columns.Add("SERVICE", System.Type.GetType("System.String"));//39

            // for rent
            dtblPOS.Columns.Add("RENTTYPE", System.Type.GetType("System.String"));//40
            dtblPOS.Columns.Add("RENTDURATION", System.Type.GetType("System.Double"));//41
            dtblPOS.Columns.Add("RENTAMOUNT", System.Type.GetType("System.Double"));//42
            dtblPOS.Columns.Add("RENTDEPOSIT", System.Type.GetType("System.Double"));//43

            // for repair
            dtblPOS.Columns.Add("REPAIRITEMTAG", System.Type.GetType("System.String"));//44
            dtblPOS.Columns.Add("REPAIRITEMSLNO", System.Type.GetType("System.String"));//45
            dtblPOS.Columns.Add("REPAIRITEMPURCHASEDATE", System.Type.GetType("System.String"));//46

            // for Tax pickup from Tax Table
            dtblPOS.Columns.Add("TX1TYPE", System.Type.GetType("System.Int32"));//47
            dtblPOS.Columns.Add("TX2TYPE", System.Type.GetType("System.Int32"));//48
            dtblPOS.Columns.Add("TX3TYPE", System.Type.GetType("System.Int32"));//49
            dtblPOS.Columns.Add("TX1ID", System.Type.GetType("System.Int32"));//50
            dtblPOS.Columns.Add("TX2ID", System.Type.GetType("System.Int32"));//51
            dtblPOS.Columns.Add("TX3ID", System.Type.GetType("System.Int32"));//52
            dtblPOS.Columns.Add("TX1", System.Type.GetType("System.Double"));//53
            dtblPOS.Columns.Add("TX2", System.Type.GetType("System.Double"));//54
            dtblPOS.Columns.Add("TX3", System.Type.GetType("System.Double"));//55

            // for Mix and Match
            dtblPOS.Columns.Add("MIXMATCHID", System.Type.GetType("System.Int32"));//56
            dtblPOS.Columns.Add("MIXMATCHFLAG", System.Type.GetType("System.String"));//57
            dtblPOS.Columns.Add("MIXMATCHTYPE", System.Type.GetType("System.String"));//58
            dtblPOS.Columns.Add("MIXMATCHVALUE", System.Type.GetType("System.Double"));//59
            dtblPOS.Columns.Add("MIXMATCHQTY", System.Type.GetType("System.Int32"));//60
            dtblPOS.Columns.Add("MIXMATCHUNIQUE", System.Type.GetType("System.Int32"));//61
            dtblPOS.Columns.Add("MIXMATCHLAST", System.Type.GetType("System.String"));//62

            // for Fees & Charges
            dtblPOS.Columns.Add("FEESID", System.Type.GetType("System.String"));//63
            dtblPOS.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//64
            dtblPOS.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));//65
            dtblPOS.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));//66
            dtblPOS.Columns.Add("FEES", System.Type.GetType("System.String"));//67
            dtblPOS.Columns.Add("FEESTAX", System.Type.GetType("System.String"));//68
            dtblPOS.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//69
            dtblPOS.Columns.Add("FEESQTY", System.Type.GetType("System.String"));//70

            //for Sale Price
            dtblPOS.Columns.Add("SALEPRICEID", System.Type.GetType("System.Int32"));//71


            // customer Destination Tax
            dtblPOS.Columns.Add("DTXID", System.Type.GetType("System.Int32"));//72
            dtblPOS.Columns.Add("DTXTYPE", System.Type.GetType("System.Int32"));//73
            dtblPOS.Columns.Add("DTXRATE", System.Type.GetType("System.Double"));//74
            dtblPOS.Columns.Add("DTX", System.Type.GetType("System.Double"));//75

            dtblPOS.Columns.Add("EDITF", System.Type.GetType("System.String"));//76

            dtblPOS.Columns.Add("PROMPTPRICE", System.Type.GetType("System.String"));//77

            // Buy 'n Get Free

            dtblPOS.Columns.Add("BUYNGETFREEHEADERID", System.Type.GetType("System.String"));
            dtblPOS.Columns.Add("BUYNGETFREECATEGORY", System.Type.GetType("System.String"));
            dtblPOS.Columns.Add("SL", System.Type.GetType("System.Int32"));
            dtblPOS.Columns.Add("BUYNGETFREENAME", System.Type.GetType("System.String"));

            // Age Restriction if Applicable for Item
            dtblPOS.Columns.Add("AGE", System.Type.GetType("System.Int32"));

            // Add for Tax Inclusive
            dtblPOS.Columns.Add("GRATE", System.Type.GetType("System.Double"));
            dtblPOS.Columns.Add("GPRICE", System.Type.GetType("System.Double"));

            dtblPOS.Columns.Add("UOM", System.Type.GetType("System.String"));

            dtblPOS.Columns.Add("DISPLAY_ITEM", System.Type.GetType("System.String"));
            dtblPOS.Columns.Add("DISPLAY_QTY", System.Type.GetType("System.String"));
            dtblPOS.Columns.Add("DISPLAY_RATE", System.Type.GetType("System.String"));
            dtblPOS.Columns.Add("DISPLAY_TOTAL", System.Type.GetType("System.String"));

            dtblPOS.Columns.Add("PM", System.Type.GetType("System.String"));

            dtblLayaway = new DataTable();
            dtblLayaway.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("LayawayNo", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("Qty", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("Cost", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("DateDue", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("TotalSale", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("ItemID", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("ProductType", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("Payment", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("Balance", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("Deposit", System.Type.GetType("System.Double"));
            dtblLayaway.Columns.Add("MatrixOptionID", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
            dtblLayaway.Columns.Add("Image", System.Type.GetType("System.Byte[]"));
            dtblLayaway.Columns.Add("CustomerID", System.Type.GetType("System.String"));
        }
        private void SetIndividualDepositForRefund()
        {

            DataTable dtbl = GetSelectedDatatable();

            foreach (DataRow dr1 in dtbl.Rows)
            {
                dr1["Deposit"] = Convert.ToString(-GeneralFunctions.fnDouble(dr1["Payment"].ToString()));
            }

            //if (dtblLayaway == null)
            //{
            //    CreateDataTable();
            //    dtblLayaway = grdLayaway.ItemsSource as DataTable;
            //}
            foreach (DataRow drM in dtblLayaway.Rows)
            {
                drM["Deposit"] = 0;
                foreach (DataRow drS in dtbl.Rows)
                {
                    if (drM["ID"].ToString() == drS["ID"].ToString())
                    {

                        drM["Deposit"] = drS["Deposit"].ToString();
                        break;
                    }
                }
            }
            dtbl.Dispose();
        }

        private void GetSelectedRowData()
        {
            DataTable dtbl = GetSelectedDatatable();
            double dblA = 0;
            double dblB = 0;
            double dblC = 0;
            double dblD = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (Settings.DecimalPlace == 3)
                {
                    dblA = dblA + GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                    dblB = dblB + GeneralFunctions.fnDouble(dr["Payment"].ToString());
                    dblC = dblC + GeneralFunctions.fnDouble(dr["Balance"].ToString());
                    dblD = dblD + GeneralFunctions.fnDouble(dr["Deposit"].ToString());
                }
                else
                {
                    dblA = dblA + GeneralFunctions.fnDouble(dr["TotalSale"].ToString());
                    dblB = dblB + GeneralFunctions.fnDouble(dr["Payment"].ToString());
                    dblC = dblC + GeneralFunctions.fnDouble(dr["Balance"].ToString());
                    dblD = dblD + GeneralFunctions.fnDouble(dr["Deposit"].ToString());
                }
            }
            dtbl.Dispose();


            numsTS = dblA;
            numsP = dblB;
            numsB = dblC;
            numsD = dblD;
            numAmount = dblC;
        }

        private void FetchCustomer(int iCustID, ref string refTaxExempt, ref string refStoreCr, ref string refARCredit)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtblCust = objPOS.FetchCustomerRecord(iCustID);
            foreach (DataRow dr in dtblCust.Rows)
            {
                refTaxExempt = dr["TaxExempt"].ToString();
                //refDiscountLevel = dr["DiscountLevel"].ToString();
                //if (refDiscountLevel == "") refDiscountLevel = "A";
                //refTaxID = dr["TaxID"].ToString();
                refStoreCr = dr["StoreCredit"].ToString();
                refARCredit = dr["ARCreditLimit"].ToString();

                //refCID = dr["CustomerID"].ToString();
                //refCName = dr["Salutation"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                //refCAdd = dr["Address1"].ToString() + "\n" + dr["City"].ToString();
            }
            dtblCust.Dispose();
        }
        private double GetAccountBalance(int intCID)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.FetchCustomerAcctPayBalance(intCID);
        }
        private async void StartRefundProcess()
        {

            if (!CheckFunctionButton("31v")) return;

            if (IsValidRefund())
            {
                blurGrid.Visibility = Visibility.Visible;
                frmPOSTenderDlg frm_POSTenderDlg = new frmPOSTenderDlg();
                try
                {
                    string refARCredit = "";
                    string refStoreCr = "";
                    string refTaxExempt = "";

                    FetchCustomer(LawAwayRecordCustID, ref refTaxExempt, ref refStoreCr, ref refARCredit);



                    string strTaxExempt = refTaxExempt;
                    double dblStoreCr = Convert.ToDouble(refStoreCr);
                    double dblCustAcctLimit = Convert.ToDouble(refARCredit);
                    double dblCustAcctBalance = GetAccountBalance(LawAwayRecordCustID);


                    SetIndividualDepositForRefund();
                    GetSelectedRowData();

                    frm_POSTenderDlg.ResumeTransaction = false;
                    frm_POSTenderDlg.ReturnItem = false;
                    frm_POSTenderDlg.NewLayaway = false;
                    frm_POSTenderDlg.FinalFlag = false;
                    frm_POSTenderDlg.Layaway = false;
                    frm_POSTenderDlg.LayawayRefund = true;
                    frm_POSTenderDlg.CustID = LawAwayRecordCustID;
                    frm_POSTenderDlg.TaxExempt = strTaxExempt;
                    frm_POSTenderDlg.MaxInvNo = intMaxInvNo;
                    frm_POSTenderDlg.LayawayPayment = GetSelectedDatatable();
                    frm_POSTenderDlg.LayawayAmt = numsD;
                    frm_POSTenderDlg.LayawayTotalSale = numsD;
                    frm_POSTenderDlg.StoreCr = dblStoreCr;
                    frm_POSTenderDlg.CustAcctLimit = dblCustAcctLimit;
                    frm_POSTenderDlg.CustAcctBalance = dblCustAcctBalance;
                    frm_POSTenderDlg.SuperUserID = intSuperUserID;
                    frm_POSTenderDlg.FunctionBtnAccess = blFunctionBtnAccess;
                    ////frm_POSTenderDlg.calledfrm = frm_POS;
                    frm_POSTenderDlg.ShowDialog();




                    //PosDataObject.POS objPOS = new PosDataObject.POS();
                    //objPOS.Connection = SystemVariables.Conn;
                    //objPOS.RefundStart(6011, 0);
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                    bool blFinalFlag = frm_POSTenderDlg.FinalFlag;
                    if (blFinalFlag)
                    {
                        CloseWindow?.Invoke();
                    }

                    //  reload detail table
                    if (gridView2.FocusedRowHandle >= 0)
                    {
                        await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));
                        intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                    }

                }
            }
        }
        private async void btnRefundLayAway_Click(object sender, RoutedEventArgs e)
        {
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colGA) == "Y") return;
            try
            {
                StartRefundProcess();

            }
            catch (Exception er)
            {
                new MessageBoxWindow().Show("Refund Failed due to:\n" + er.Message, "Refund Failed", MessageBoxButton.OK, MessageBoxImage.Information);

            }





        }

        #endregion

        public CloseWindowCallback CloseWindow { get; set; }

        private async void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colGA) == "Y") return;

            int selectedInvoiceNumber = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));

            DataTable dtbl = new DataTable();
            dtbl = grdDetail.ItemsSource as DataTable;

            int checkedrow = 0;

            foreach (DataRow dr in dtbl.Rows)
            {
                if (Convert.ToBoolean(dr["CheckReturned"].ToString())) checkedrow++;
            }
            if (checkedrow == 0) return;

            if (new MessageBoxWindow().Show(
             "Do you want to refund this invoice ?", "Info", MessageBoxButton.YesNo,
             MessageBoxImage.Information) == MessageBoxResult.Yes)
            {


                decimal amount = GeneralFunctions.fnDecimal(string.Format("{0:#.00}", Convert.ToDecimal(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colAmount))));

                if (selectedInvoiceNumber != 0)
                {
                    bool checkfullrefund = false;
                    checkfullrefund = await CheckFullOrPartRefund();

                    if (!checkfullrefund)
                    {
                        amount = GeneralFunctions.fnDecimal(string.Format("{0:#.00}", Convert.ToDecimal(-GetPartRefundAmount())));
                    }

                    PosDataObject.POS objPos = new PosDataObject.POS();
                    objPos.Connection = SystemVariables.Conn;
                    int XEConnectID = objPos.FetchIdFromXeConnect(selectedInvoiceNumber);
                    if (XEConnectID == 0) XEConnectID = selectedInvoiceNumber;

                    bool layAwaySelected = false;
                    if (rg2.IsChecked == true)
                    {
                        layAwaySelected = true;

                    }

                    var evoTransactionForm = new frm_EvoTransaction(-amount, XEConnectID, isRefundOrVoid: true, isExecuteVoid: true, dtblPart: (checkfullrefund ? null : dtblPOS), calledfrom: this, isLayAway: layAwaySelected);
                    blurGrid.Visibility = Visibility.Visible;
                    evoTransactionForm.ShowDialog();
                    blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }


        private async Task<bool> CheckFullOrPartRefund()
        {
            dtblReturnItem.Rows.Clear();
            bool boolFullRefund = false;

            DataTable dtbl = new DataTable();
            dtbl = grdDetail.ItemsSource as DataTable;

            if (rg1.IsChecked == true)
                RearrangeDataForMixNMatchItem(dtbl, GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)));

            double dqty = 0;
            double disc = 0;
            double fees = 0;
            double feestax = 0;

            int RowNo = 0;

            foreach (DataRow dr in dtbl.Rows)
            {
                if (!Convert.ToBoolean(dr["CheckReturned"].ToString())) continue;

                if (dr["ProductType"].ToString() == "G")
                {
                    PosDataObject.POS objp = new PosDataObject.POS();
                    objp.Connection = SystemVariables.Conn;
                    double bal = objp.FetchGiftCertAmount(dr["ProductID"].ToString(), Settings.StoreCode, Settings.CentralExportImport);
                    if (bal < GeneralFunctions.fnDouble(dr["Price"].ToString())) continue;
                }

                dqty = 0;
                dqty = -(GeneralFunctions.fnDouble(dr["Qty"].ToString())); //-(GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["ReturnedItemCnt"].ToString()));
                disc = 0;
                disc = -(GeneralFunctions.fnDouble(dr["Discount"].ToString()));
                fees = 0;
                feestax = 0;
                fees = -(GeneralFunctions.fnDouble(dr["Fees"].ToString()));
                feestax = -(GeneralFunctions.fnDouble(dr["FeesTax"].ToString()));

                RowNo++;

                dtblReturnItem.Rows.Add(new object[] {  dr["ID"].ToString(),
                                                        dr["ProductID"].ToString(),
                                                        dr["Description"].ToString(),
                                                        dr["ProductType"].ToString(),
                                                        dr["Price"].ToString(),
                                                        dqty.ToString(),
                                                        dr["DiscLogic"].ToString(),
                                                        dr["DiscValue"].ToString(),
                                                        dr["DiscountID"].ToString(),
                                                        disc.ToString(),
                                                        dr["DiscountText"].ToString(),
                                                        dr["TaxID1"].ToString(),
                                                        dr["TaxID2"].ToString(),
                                                        dr["TaxID3"].ToString(),
                                                        dr["TaxType1"].ToString(),
                                                        dr["TaxType2"].ToString(),
                                                        dr["TaxType3"].ToString(),
                                                        dr["TaxTotal1"].ToString(),
                                                        dr["TaxTotal2"].ToString(),
                                                        dr["TaxTotal3"].ToString(),

                                                        dr["Taxable1"].ToString(),
                                                        dr["Taxable2"].ToString(),
                                                        dr["Taxable3"].ToString(),

                                                        dr["TaxRate1"].ToString(),
                                                        dr["TaxRate2"].ToString(),
                                                        dr["TaxRate3"].ToString(),

                                                        dr["FeesID"].ToString(),
                                                        dr["FeesLogic"].ToString(),
                                                        dr["FeesValue"].ToString(),
                                                        dr["FeesTaxRate"].ToString(),
                                                        fees.ToString(),
                                                        feestax.ToString(),
                                                        dr["FeesText"].ToString(),
                                                        dr["FeesQty"].ToString(),
                                                        dr["DTaxID"].ToString(),
                                                        dr["DTaxType"].ToString(),
                                                        dr["DTaxRate"].ToString(),
                                                        dr["DTax"].ToString(),
                                                        dr["EditFlag"].ToString(),
                                                        dr["QtyDecimal"].ToString(),
                                                        dr["PromptPrice"].ToString(),
                                                        dr["BuyNGetFreeHeaderID"].ToString(),
                                                        dr["BuyNGetFreeCategory"].ToString(),
                                                        dr["BuyNGetFreeName"].ToString(),
                                                        RowNo.ToString(),
                                                        dr["Age"].ToString(),
                                                        dr["GRate"].ToString(),
                                                        dr["GPrice"].ToString(),dr["UOM"].ToString()
                                                        });
            }

            if (dtblSelectTag.Rows.Count > 0)
            {
                DataTable dtblTemp = new DataTable();
                if (dtblReturnItem.Rows.Count > 0) dtblTemp = dtblReturnItem;

                dtbl = dtblSelectTag;
                dqty = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    bool fnd = false;
                    foreach (DataRow drt in dtblTemp.Rows)
                    {
                        if (drt["ID"].ToString() == dr["ID"].ToString())
                        {
                            fnd = true;
                            break;
                        }
                    }
                    if (!fnd)
                    {
                        dqty = 0;
                        dqty = -(GeneralFunctions.fnDouble(dr["QTY"].ToString())); //-(GeneralFunctions.fnDouble(dr["Qty"].ToString()) - GeneralFunctions.fnDouble(dr["ReturnedItemCnt"].ToString()));
                        disc = 0;
                        //disc = -(GeneralFunctions.fnDouble(dr["Discount"].ToString()));
                        dtblReturnItem.Rows.Add(new object[] {dr["ID"].ToString(),
                                                dr["PID"].ToString(),
                                                dr["PNAME"].ToString(),
                                                dr["PTYPE"].ToString(),
                                                dr["PRICE"].ToString(),
                                                dqty.ToString(),
                                                "",//dr["DiscLogic"].ToString(),
                                                "",//dr["DiscValue"].ToString(),
                                                "",//dr["DiscountID"].ToString(),
                                                disc.ToString(),
                                                ""});//dr["DiscountText"].ToString()});
                    }
                }
            }
            blExistCoupon = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colcoupon)) != 0;

            if (dtblReturnItem.Rows.Count == 0)
            {
                boolFullRefund = true;
            }
            else
            {
                if (dtblReturnItem.Rows.Count == dtbl.Rows.Count)
                {
                    boolFullRefund = true;
                }
                else
                {
                    boolFullRefund = false;
                }
            }

            return boolFullRefund;
        }

        private string GetProductDescInCart1(string str, string did, string fid, string buyfree)
        {
            if (did != "0")
            {
                if (fid != "0")
                {
                    if (buyfree == "B")
                    {
                        return "\n" + str + "\n" + "\n";
                    }
                    else
                    {
                        return str + "\n" + "\n";
                    }
                }

                else
                {
                    if (buyfree == "B")
                    {
                        return "\n" + str + "\n";
                    }
                    else
                    {
                        return str + "\n";
                    }
                }
            }
            else
            {
                if (fid != "0")
                {
                    if (buyfree == "B")
                    {
                        return "\n" + str + "\n";
                    }
                    else
                    {
                        return str + "\n";
                    }
                }
                else
                {
                    if (buyfree == "B")
                    {
                        return "\n" + str;
                    }
                    else
                    {
                        return str;
                    }
                }
            }
        }

        private string GetUniqueString()
        {
            return Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) +
                Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        }

        private void DTaxDetails(int pCID, ref string val1, ref double val2, ref int val3)
        {
            PosDataObject.Tax objPOS = new PosDataObject.Tax();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            objPOS.GetDTaxDetails(pCID, ref val1, ref val2, ref val3);
        }

        private void FetchCustomer(int iCustID, ref string refCID, ref string refCName, ref string refCAdd,
                                ref string refTaxExempt, ref string refDiscountLevel, ref string refTaxID,
                                ref string refStoreCr, ref string refARCredit, ref string refPOSNotes,
                                ref int refDTaxID, ref string refDTaxName, ref double refDTaxRate, ref int refDTaxType)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtblCust = objPOS.FetchCustomerRecord(iCustID);
            foreach (DataRow dr in dtblCust.Rows)
            {
                refTaxExempt = dr["TaxExempt"].ToString();
                if (refTaxExempt.Trim() == "") refTaxExempt = "N";
                refDiscountLevel = dr["DiscountLevel"].ToString();
                refTaxID = dr["TaxID"].ToString();
                refStoreCr = dr["StoreCredit"].ToString();
                refARCredit = dr["ARCreditLimit"].ToString();
                refCID = dr["CustomerID"].ToString();
                if (dr["Salutation"].ToString() != "")
                    refCName = dr["Salutation"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                else
                    refCName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();

                refCAdd = dr["Address1"].ToString() + "\n" + dr["City"].ToString();
                refPOSNotes = dr["POSNotes"].ToString();
                refDTaxID = GeneralFunctions.fnInt32(dr["DTaxID"].ToString());

                if (refDTaxID > 0)
                {
                    bool b = false;
                    PosDataObject.Tax otx = new PosDataObject.Tax();
                    otx.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    b = otx.IsActiveDTax(refDTaxID);
                    if (b)
                        DTaxDetails(refDTaxID, ref refDTaxName, ref refDTaxRate, ref refDTaxType);
                    else refDTaxID = 0;
                }
            }
            dtblCust.Dispose();

            

        }

        private void CouponCalculation(DataTable dtbl, ref double resultAmount, ref double resultPerc, ref double TotAmount)
        {
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "H")
                    || (dr["PRODUCTTYPE"].ToString() == "Z")) continue;
                if ((dr["BUYNGETFREECATEGORY"].ToString() == "B") || (dr["BUYNGETFREECATEGORY"].ToString() == "F")) continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                TotAmount = TotAmount + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            double dpp = 0;
            double dAA = 0;
            double dp = 0;
            double dA = 0;
            double retamt = 0;
            resultAmount = 0;
            double AppAmount = 0;
           
            int cnt = -1;

            dtbl.DefaultView.Sort = "ITEMINDEX asc";
            dtbl.DefaultView.ApplyDefaultSort = true;

            foreach (DataRowView dr in dtbl.DefaultView)
            {
                cnt++;
                dp = 0;
                dA = 0;
                AppAmount = 0;
                if (dr["PRODUCTTYPE"].ToString() != "C") continue;
                if (dr["DISCLOGIC"].ToString() == "P") dp = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());
                if (dr["DISCLOGIC"].ToString() == "A") dA = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());


                if (CheckIfRestrictedItem(GeneralFunctions.fnInt32(dr["ID"].ToString())))
                {
                    // discount calculation on restricted items
                    AppAmount = RestrictItemApplicableAmount(GeneralFunctions.fnInt32(dr["ID"].ToString()));

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    
                    resultAmount = resultAmount + retamt;


                }
                else
                {
                    // discount calculation on all items

                    AppAmount = AllItemApplicableAmount();

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    
                    resultAmount = resultAmount + retamt;
                }
            }
            resultAmount = GeneralFunctions.FormatDouble(resultAmount);
            if (TotAmount != 0) resultPerc = dpp + GeneralFunctions.fnDouble(dAA * 100 / TotAmount);

        }
        private double AllItemApplicableAmount()
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["DISCOUNTID"].ToString() != "0") continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        private double AllItemApplicableAmount_Fees()
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["FEESID"].ToString() != "0") continue;
                ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        private bool IsNonDiscountableItem(int pID)
        {
            PosDataObject.POS ops = new PosDataObject.POS();
            ops.Connection = SystemVariables.Conn;
            return ops.IsNonDiscountableItem(pID);
        }
        private bool CheckIfRestrictedItem(int pID)
        {
            bool ret = false;
            PosDataObject.POS objps1 = new PosDataObject.POS();
            objps1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            ret = objps1.IsRestrictedDiscount(pID);
            return ret;
        }

        private string GetFStamp(int PID)
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objProduct.GetFoodStampFlag(PID);
        }

        private double RestrictItemApplicableAmount_FS(int DID)
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A") || (dr["PRODUCTTYPE"].ToString() == "H")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["DISCOUNTID"].ToString() != "0") continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                if (GetFStamp(GeneralFunctions.fnInt32(dr["ID"].ToString())) != "Y") continue;
                string chr = "N";
                PosDataObject.POS objp = new PosDataObject.POS();
                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objp.IsApplicableForRestrictedDiscount(GeneralFunctions.fnInt32(dr["ID"].ToString()), DID, ref chr);
                if (chr == "Y") ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }
        private double RestrictItemApplicableAmount(int DID)
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["DISCOUNTID"].ToString() != "0") continue;
                if (IsNonDiscountableItem(GeneralFunctions.fnInt32(dr["ID"].ToString()))) continue;
                string chr = "N";
                PosDataObject.POS objp = new PosDataObject.POS();
                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objp.IsApplicableForRestrictedDiscount(GeneralFunctions.fnInt32(dr["ID"].ToString()), DID, ref chr);
                if (chr == "Y") ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }
        private bool CheckIfRestrictedItem_Fees(int pID)
        {
            bool ret = false;
            PosDataObject.POS objps1 = new PosDataObject.POS();
            objps1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            ret = objps1.IsRestrictedFees(pID);
            return ret;
        }
        private double RestrictItemApplicableAmount_Fees(int DID)
        {
            double ret = 0;
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "Z")
                    || (dr["PRODUCTTYPE"].ToString() == "H")) continue;
                if (dr["PRICE"].ToString() == "") continue;
                if (dr["FEESID"].ToString() != "0") continue;

                string chr = "N";
                PosDataObject.POS objp = new PosDataObject.POS();
                objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objp.IsApplicableForRestrictedFees(GeneralFunctions.fnInt32(dr["ID"].ToString()), DID, ref chr);
                if (chr == "Y") ret = ret + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            return ret;
        }

        private void FeesCouponCalculation(DataTable dtbl, ref double resultAmount, ref double resultPerc, ref double TotAmount, ref double TxAmount)
        {
            foreach (DataRow dr in dtblPOS.Rows)
            {
                if ((dr["PRODUCTTYPE"].ToString() == "C") || (dr["PRODUCTTYPE"].ToString() == "G") || (dr["PRODUCTTYPE"].ToString() == "A")
                    || (dr["PRODUCTTYPE"].ToString() == "X") || (dr["PRODUCTTYPE"].ToString() == "O") || (dr["PRODUCTTYPE"].ToString() == "H")
                    || (dr["PRODUCTTYPE"].ToString() == "Z")) continue;
                TotAmount = TotAmount + GeneralFunctions.fnDouble(dr["PRICE"].ToString());
            }
            double dpp = 0;
            double dAA = 0;
            double dp = 0;
            double dA = 0;
            double retamt = 0;
            resultAmount = 0;
            double AppAmount = 0;
          
            int cnt = -1;

            dtbl.DefaultView.Sort = "ITEMINDEX asc";
            dtbl.DefaultView.ApplyDefaultSort = true;

            foreach (DataRowView dr in dtbl.DefaultView)
            {
                cnt++;
                dp = 0;
                dA = 0;
                AppAmount = 0;
                if (dr["PRODUCTTYPE"].ToString() != "H") continue;
                if (dr["DISCLOGIC"].ToString() == "P") dp = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());
                if (dr["DISCLOGIC"].ToString() == "A") dA = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());


                if (CheckIfRestrictedItem_Fees(GeneralFunctions.fnInt32(dr["ID"].ToString())))
                {
                    // discount calculation on restricted items
                    AppAmount = RestrictItemApplicableAmount_Fees(GeneralFunctions.fnInt32(dr["ID"].ToString()));

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    
                    resultAmount = resultAmount + retamt;

                }
                else
                {
                    // discount calculation on all items

                    AppAmount = AllItemApplicableAmount_Fees();

                    if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                    {
                        retamt = (AppAmount * dp / 100) + dA;
                        dAA = dAA + dA;
                        dpp = dpp + dp;
                    }
                    
                    resultAmount = resultAmount + retamt;
                }

                resultAmount = GeneralFunctions.FormatDouble(retamt);

                double feestax = 0;
                double taxrate = 0;
                string tx = "";
                if (dr["FEESQTY"].ToString() == "Y")
                {
                    PosDataObject.POS objpos = new PosDataObject.POS();
                    objpos.Connection = SystemVariables.Conn;
                    objpos.GetFeesTax(GeneralFunctions.fnInt32(dr["ID"].ToString()), ref taxrate, ref tx);
                    try
                    {
                        //feestax = GeneralFunctions.FormatDouble(retamt * taxrate / 100);

                        if (Settings.TaxInclusive == "N")
                        {
                            feestax = GeneralFunctions.FormatDouble(retamt * taxrate / 100);
                        }
                        else
                        {
                            double tempApplicableAmount = retamt / ((100 + taxrate) / 100);
                            feestax = GeneralFunctions.FormatDouble(retamt - tempApplicableAmount);
                        }
                    }
                    catch
                    {
                        feestax = 0;
                    }

                    TxAmount = TxAmount + feestax;
                }

                if (Settings.TaxInclusive == "Y")
                {
                    retamt = retamt - feestax;
                }

                dr["FEESID"] = dr["ID"].ToString();
                dr["FEESLOGIC"] = dr["DISCLOGIC"].ToString();
                dr["FEESVALUE"] = dr["DISCVALUE"].ToString();
                dr["FEESTAXRATE"] = taxrate.ToString();
                dr["FEES"] = retamt.ToString();
                dr["FEESTAX"] = GeneralFunctions.FormatDouble(feestax).ToString();

            }

            resultAmount = GeneralFunctions.FormatDouble(resultAmount);
            TxAmount = GeneralFunctions.FormatDouble(TxAmount);
            if (TotAmount != 0) resultPerc = dpp + GeneralFunctions.fnDouble(dAA * 100 / TotAmount);

        }

        private void SpecialMixMatchCalculation(DataTable dtbl, ref double resultAmount)
        {

            double dpp = 0;
            double dAA = 0;
            double dp = 0;
            double dA = 0;
            double retamt = 0;
            resultAmount = 0;
            double AppAmount = 0;
            
            int cnt = -1;

            dtbl.DefaultView.Sort = "ITEMINDEX asc";
            dtbl.DefaultView.ApplyDefaultSort = true;

            foreach (DataRowView dr in dtbl.DefaultView)
            {
                cnt++;
                dp = 0;
                dA = 0;
                AppAmount = 0;
                if (dr["PRODUCTTYPE"].ToString() != "Z") continue;
                if (dr["DISCLOGIC"].ToString() == "P") dp = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());
                if (dr["DISCLOGIC"].ToString() == "A") dA = GeneralFunctions.fnDouble(dr["DISCVALUE"].ToString());


                AppAmount = GeneralFunctions.fnDouble(dr["DTXRATE"].ToString());

                if ((((dp != 0) || (dA != 0))) && (AppAmount > 0))
                {
                    retamt = (AppAmount * dp / 100) + dA;
                    dAA = dAA + dA;
                    dpp = dpp + dp;
                }
               
                resultAmount = resultAmount + retamt;
            }
            resultAmount = GeneralFunctions.FormatDouble(resultAmount);

        }

        private double GetTaxRate(int ProductID, double ApplicableAmount, string Qty, ref int tx1ty, ref int tx1id, ref double tx1, ref int tx2ty, ref int tx2id, ref double tx2,
                             ref int tx3ty, ref int tx3id, ref double tx3, ref double TaxInclusiveRate)
        {
            DataTable dtblTax = new DataTable();
            double dblTax = 0;
            PosDataObject.Product objTax = new PosDataObject.Product();
            objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);

            dtblTax = objTax.ShowActiveTaxes(ProductID);
            
            int cnt = 0;

            double tqty = 0;
            if (Qty == "")
            {
                tqty = 1;
            }
            else
            {
                tqty = GeneralFunctions.fnDouble(Qty);
            }

            foreach (DataRow drT in dtblTax.Rows)
            {
                cnt++;
                if (drT["TaxRate"].ToString() != "")
                {
                    if (cnt == 1)
                    {
                        tx1ty = GeneralFunctions.fnInt32(drT["TaxType"].ToString());
                        tx1id = GeneralFunctions.fnInt32(drT["TaxID"].ToString());
                    }
                    if (cnt == 2)
                    {
                        tx2ty = GeneralFunctions.fnInt32(drT["TaxType"].ToString());
                        tx2id = GeneralFunctions.fnInt32(drT["TaxID"].ToString());
                    }
                    if (cnt == 3)
                    {
                        tx3ty = GeneralFunctions.fnInt32(drT["TaxType"].ToString());
                        tx3id = GeneralFunctions.fnInt32(drT["TaxID"].ToString());
                    }
                    double tx = 0;
                    if (drT["TaxType"].ToString() == "0")
                    {
                        if (Settings.TaxInclusive == "N")
                        {
                            tx = (GeneralFunctions.fnDouble(drT["TaxRate"].ToString()) * ApplicableAmount) / 100;
                        }
                        else
                        {

                            double TxApplicableAmount = GeneralFunctions.FormatDouble(ApplicableAmount / tqty);

                            //double tempApplicableAmount = TxApplicableAmount / ((100 + GeneralFunctions.fnDouble(drT["TaxRate"].ToString())) / 100);

                            //tx = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(TxApplicableAmount - tempApplicableAmount) * tqty);

                            double tempApplicableAmount = ApplicableAmount / ((100 + GeneralFunctions.fnDouble(drT["TaxRate"].ToString())) / 100);
                            tx = GeneralFunctions.FormatDouble(ApplicableAmount - tempApplicableAmount);

                        }
                    }
                    else
                    {
                        //tx = (GeneralFunctions.fnDouble(drT["TaxRate"].ToString()) * ApplicableAmount) / 100;
                        tx = GeneralFunctions.GetTaxFromTaxTable(GeneralFunctions.fnInt32(drT["TaxID"].ToString()), GeneralFunctions.fnDouble(drT["TaxRate"].ToString()), ApplicableAmount);
                    }
                    if (cnt == 1) tx1 = tx; // GeneralFunctions.FormatDouble(tx);
                    if (cnt == 2) tx2 = tx;// GeneralFunctions.FormatDouble(tx);
                    if (cnt == 3) tx3 = tx;// GeneralFunctions.FormatDouble(tx);

                    dblTax = dblTax + tx;
                }
            }

            if (Settings.TaxInclusive == "Y")
            {
                ApplicableAmount = GeneralFunctions.FormatDouble(ApplicableAmount / tqty);
                TaxInclusiveRate = (ApplicableAmount * tqty - dblTax) / tqty;
            }

            dtblTax.Dispose();
            return dblTax;
        }
        private double GetTaxRate(int ProductID, double ApplicableAmount)
        {
            DataTable dtblTax = new DataTable();
            double dblTax = 0;
            PosDataObject.Product objTax = new PosDataObject.Product();
            objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);

            dtblTax = objTax.ShowActiveTaxes(ProductID);

            foreach (DataRow drT in dtblTax.Rows)
            {

                if (drT["TaxRate"].ToString() != "")
                {
                    double tx = 0;
                    if (drT["TaxType"].ToString() == "0")
                    {
                        //tx = (GeneralFunctions.fnDouble(drT["TaxRate"].ToString()) * ApplicableAmount) / 100;

                        if (Settings.TaxInclusive == "N")
                        {
                            tx = (GeneralFunctions.fnDouble(drT["TaxRate"].ToString()) * ApplicableAmount) / 100;
                        }
                        else
                        {
                            double tempApplicableAmount = ApplicableAmount / ((100 + GeneralFunctions.fnDouble(drT["TaxRate"].ToString())) / 100);
                            tx = GeneralFunctions.FormatDouble(ApplicableAmount - tempApplicableAmount);
                        }
                    }
                    else
                    {
                        tx = GeneralFunctions.GetTaxFromTaxTable(GeneralFunctions.fnInt32(drT["TaxID"].ToString()), GeneralFunctions.fnDouble(drT["TaxRate"].ToString()), ApplicableAmount);
                    }
                    dblTax = dblTax + tx;
                }
            }
            dtblTax.Dispose();
            return dblTax;
        }

        private double GetDTaxAmount(int pTaxID, double pRate, int pType, double AppAmount)
        {
            double tx = 0;

            if (pType == 0)
            {
                tx = (pRate * AppAmount) / 100;
            }
            else
            {
                tx = GeneralFunctions.GetTaxFromTaxTable(pTaxID, pRate, AppAmount);
            }
            return tx;
        }

        private int GetTaxType(int TaxID)
        {
            PosDataObject.Tax objtx = new PosDataObject.Tax();
            objtx.Connection = SystemVariables.Conn;
            return objtx.GetTaxType(TaxID);
        }

        private double GetTotal()
        {
            
            bl100percinvdiscount = false;
            foreach (DataRow drc in dtblPOS.Rows)
            {
                if (drc["PRODUCTTYPE"].ToString() != "C") continue;
                if (drc["PRODUCTTYPE"].ToString() != "H") continue;
                if (drc["PRODUCTTYPE"].ToString() != "Z") continue;
                if ((drc["DISCLOGIC"].ToString() == "P") && (GeneralFunctions.fnDouble(drc["DISCVALUE"].ToString()) == 100))
                {
                    bl100percinvdiscount = true;
                    break;
                }
            }
            dblFeesCrg = 0;
            dblCouponAmount = 0;
            dblCouponPerc = 0;
            dblCouponApplicableTotal = 0;

            dblSpecialMixMatchAmount = 0;


            dblFeesCouponAmount = 0;
            dblFeesCouponPerc = 0;
            dblFeesCouponApplicableTotal = 0;
            dblFeesCouponTaxAmount = 0;
            dblFeesCouponTaxRate = 0;

            CouponCalculation(dtblPOS, ref dblCouponAmount, ref dblCouponPerc, ref dblCouponApplicableTotal);

            FeesCouponCalculation(dtblPOS, ref dblFeesCouponAmount, ref dblFeesCouponPerc, ref dblFeesCouponApplicableTotal, ref dblFeesCouponTaxAmount);

            SpecialMixMatchCalculation(dtblPOS, ref dblSpecialMixMatchAmount);

            string numSubTotal= "0";
            string numDiscount = "0";
            string numTotal = "0";
            string numTax = "0";
            double dblSubTotal = 0;
            double dblDisc = 0;
            double dblTax = 0;
            double dblDTax = 0;
            double rate = 0;
            double qty = 0;
            double renttime = 0;
            double fees = 0;
            double feestax = 0;
            string feeqty = "N";

            double totalTaxRate = 0;

            foreach (DataRow dr in dtblPOS.Rows)
            {


                if (dr["QTY"].ToString() == "") qty = 1; else qty = GeneralFunctions.fnDouble(dr["QTY"].ToString());




                if (((strTaxExempt == "N") && (blChangeCustomer)) || ((strTaxExempt == "N") && (intCustID == 0)))
                {
                    if ((dr["PRODUCTTYPE"].ToString() != "G") && (dr["PRODUCTTYPE"].ToString() != "A") && (dr["PRODUCTTYPE"].ToString() != "C")
                        && (dr["PRODUCTTYPE"].ToString() != "Z") && (dr["PRODUCTTYPE"].ToString() != "H")
                        && (dr["PRODUCTTYPE"].ToString() != "X") && (dr["PRODUCTTYPE"].ToString() != "O"))
                    {
                        if (!bl100percinvdiscount)
                        {
                            int tx1ty = 0;
                            int tx1id = 0;
                            double tx1 = 0;
                            int tx2ty = 0;
                            int tx2id = 0;
                            double tx2 = 0;
                            int tx3ty = 0;
                            int tx3id = 0;
                            double tx3 = 0;
                            double tempDTax = 0;
                            double taxInclusiveRate = 0;

                            if (dr["PRODUCTTYPE"].ToString() != "B")
                            {
                                if (dr["EDITF"].ToString() == "N")
                                {
                                    dblTax = dblTax + GeneralFunctions.fnDouble((GetTaxRate(GeneralFunctions.fnInt32(dr["ID"].ToString()), (Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())), dr["QTY"].ToString(), ref tx1ty, ref tx1id, ref tx1, ref tx2ty, ref tx2id, ref tx2, ref tx3ty, ref tx3id, ref tx3, ref taxInclusiveRate)) * (100 - dblCouponPerc) / 100);

                                    if (dr["SERVICE"].ToString() == "Sales")
                                    {
                                        if (CustDTaxID > 0)
                                        {
                                            tempDTax = GetDTaxAmount(CustDTaxID, CustDTaxRate, CustDTaxType, GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                                            dblDTax = dblDTax + tempDTax;
                                            dblTax = dblTax + tempDTax;
                                        }
                                    }
                                }
                                else
                                {
                                    double dblAppTax = 0;

                                    if (dr["TAXABLE1"].ToString() == "Y")
                                    {
                                        tx1id = GeneralFunctions.fnInt32(dr["TAXID1"].ToString());
                                        tx1ty = GetTaxType(tx1id);
                                        if (tx1ty == 0)
                                        {
                                            if (Settings.TaxInclusive == "N")
                                            {
                                                tx1 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                            }
                                            else
                                            {
                                                //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                                //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString())) / 100);
                                                //tx1 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                                double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString())) / 100);
                                                tx1 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                            }

                                        }
                                        else tx1 = GeneralFunctions.GetTaxFromTaxTable(tx1id, GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                        dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx1 * (100 - dblCouponPerc) / 100);
                                    }
                                    if (dr["TAXABLE2"].ToString() == "Y")
                                    {
                                        tx2id = GeneralFunctions.fnInt32(dr["TAXID2"].ToString());
                                        tx2ty = GetTaxType(tx2id);
                                        if (tx2ty == 0)
                                        {
                                            if (Settings.TaxInclusive == "N")
                                            {
                                                tx2 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                            }
                                            else
                                            {
                                                //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                                //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString())) / 100);
                                                //tx2 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                                double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString())) / 100);
                                                tx2 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                            }

                                        }
                                        else tx2 = GeneralFunctions.GetTaxFromTaxTable(tx2id, GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                        dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx2 * (100 - dblCouponPerc) / 100);
                                    }

                                    if (dr["TAXABLE3"].ToString() == "Y")
                                    {
                                        tx3id = GeneralFunctions.fnInt32(dr["TAXID3"].ToString());
                                        tx3ty = GetTaxType(tx3id);
                                        if (tx3ty == 0)
                                        {
                                            if (Settings.TaxInclusive == "N")
                                            {
                                                tx3 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                            }
                                            else
                                            {
                                                //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                                //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString())) / 100);
                                                //tx3 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                                double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString())) / 100);
                                                tx3 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                            }
                                        }
                                        else tx3 = GeneralFunctions.GetTaxFromTaxTable(tx3id, GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                        dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx3 * (100 - dblCouponPerc) / 100);
                                    }

                                    if (dr["SERVICE"].ToString() == "Sales")
                                    {
                                        if (CustDTaxID > 0)
                                        {
                                            tempDTax = GetDTaxAmount(CustDTaxID, CustDTaxRate, CustDTaxType, GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                                            dblDTax = dblDTax + tempDTax;
                                            dblTax = dblTax + tempDTax;
                                        }
                                    }

                                    if (Settings.TaxInclusive == "Y")
                                    {

                                        taxInclusiveRate = GeneralFunctions.FormatDouble((GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString()))) - GeneralFunctions.FormatDouble(tx1 + tx2 + tx3)) / qty);
                                        //taxInclusiveRate = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) - GeneralFunctions.FormatDouble(tx1 + tx2 + tx3)) / qty);
                                    }

                                    dblTax = dblTax + dblAppTax;
                                }
                            }
                            else
                            {
                                double dblAppTax = 0;

                                if (dr["TAXABLE1"].ToString() == "Y")
                                {
                                    tx1id = GeneralFunctions.fnInt32(dr["TAXID1"].ToString());
                                    tx1ty = GetTaxType(tx1id);
                                    if (tx1ty == 0)
                                    {
                                        totalTaxRate = totalTaxRate + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString());
                                        if (Settings.TaxInclusive == "N")
                                        {
                                            tx1 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                        }
                                        else
                                        {
                                            //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                            //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString())) / 100);
                                            //tx1 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                            double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString())) / 100);
                                            tx1 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                        }
                                    }
                                    else tx1 = GeneralFunctions.GetTaxFromTaxTable(tx1id, GeneralFunctions.fnDouble(dr["TAXRATE1"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                    dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx1 * (100 - dblCouponPerc) / 100);
                                }
                                if (dr["TAXABLE2"].ToString() == "Y")
                                {
                                    tx2id = GeneralFunctions.fnInt32(dr["TAXID2"].ToString());
                                    tx2ty = GetTaxType(tx2id);
                                    if (tx2ty == 0)
                                    {
                                        totalTaxRate = totalTaxRate + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString());
                                        if (Settings.TaxInclusive == "N")
                                        {
                                            tx2 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                        }
                                        else
                                        {
                                            //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                            //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString())) / 100);
                                            //tx2 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                            double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString())) / 100);
                                            tx2 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                        }
                                    }
                                    else tx2 = GeneralFunctions.GetTaxFromTaxTable(tx2id, GeneralFunctions.fnDouble(dr["TAXRATE2"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                    dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx2 * (100 - dblCouponPerc) / 100);
                                }

                                if (dr["TAXABLE3"].ToString() == "Y")
                                {
                                    tx3id = GeneralFunctions.fnInt32(dr["TAXID3"].ToString());
                                    tx3ty = GetTaxType(tx3id);
                                    if (tx3ty == 0)
                                    {
                                        totalTaxRate = totalTaxRate + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString());
                                        if (Settings.TaxInclusive == "N")
                                        {
                                            tx3 = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()) * GeneralFunctions.fnDouble(dr["PRICE"].ToString())) / 100);
                                        }
                                        else
                                        {
                                            //double unitAmount = GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) / qty);
                                            //double tempApplicableAmount = unitAmount / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString())) / 100);
                                            //tx3 = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble(unitAmount - tempApplicableAmount) * qty);

                                            double tempApplicableAmount = GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) / ((100 + GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString())) / 100);
                                            tx3 = GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - tempApplicableAmount);
                                        }
                                    }
                                    else tx3 = GeneralFunctions.GetTaxFromTaxTable(tx3id, GeneralFunctions.fnDouble(dr["TAXRATE3"].ToString()), GeneralFunctions.fnDouble(dr["PRICE"].ToString()));

                                    dblAppTax = GeneralFunctions.FormatDouble(dblAppTax + tx3 * (100 - dblCouponPerc) / 100);
                                }

                                if (dr["SERVICE"].ToString() == "Sales")
                                {
                                    if (CustDTaxID > 0)
                                    {
                                        tempDTax = GetDTaxAmount(CustDTaxID, CustDTaxRate, CustDTaxType, GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                                        dblDTax = dblDTax + tempDTax;
                                        dblTax = dblTax + tempDTax;
                                    }
                                }

                                if (Settings.TaxInclusive == "Y")
                                {
                                    //taxInclusiveRate = GeneralFunctions.FormatDouble(GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString())) - GeneralFunctions.FormatDouble(tx1 + tx2 + tx3)) / qty);
                                    //taxInclusiveRate = GeneralFunctions.FormatDouble((GeneralFunctions.FormatDouble((Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["PRICE"].ToString()) : GeneralFunctions.fnDouble(dr["GPRICE"].ToString()))) - GeneralFunctions.FormatDouble(tx1 + tx2 + tx3)) / qty);

                                    taxInclusiveRate = (GeneralFunctions.fnDouble(dr["GPRICE"].ToString()) - GeneralFunctions.FormatDouble(tx1 + tx2 + tx3)) / qty;
                                }

                                dblTax = dblTax + dblAppTax;
                            }

                            dr["TX1TYPE"] = tx1ty;
                            dr["TX1ID"] = tx1id;
                            dr["TX1"] = tx1;
                            dr["TX2TYPE"] = tx2ty;
                            dr["TX2ID"] = tx2id;
                            dr["TX2"] = tx2;
                            dr["TX3TYPE"] = tx3ty;
                            dr["TX3ID"] = tx3id;
                            dr["TX3"] = tx3;

                            dr["DTXTYPE"] = CustDTaxType;
                            dr["DTXID"] = CustDTaxID;
                            dr["DTXRATE"] = CustDTaxRate;
                            dr["DTX"] = tempDTax;

                            if (Settings.TaxInclusive == "Y")
                            {
                                taxInclusiveRate = -taxInclusiveRate;
                                dr["RATE"] = taxInclusiveRate;
                                /*if (GeneralFunctions.fnDouble(dr["DISCOUNT"].ToString()) == 0)
                                {
                                    dr["NRATE"] = taxInclusiveRate;
                                }*/
                                dr["PRICE"] = GeneralFunctions.FormatDouble((-taxInclusiveRate) * qty);
                            }
                        }
                    }
                }

                if (dr["RATE"].ToString() == "") rate = 0; else rate = GeneralFunctions.fnDouble(dr["RATE"].ToString());

                if ((dr["RENTDURATION"].ToString() == "") || (dr["RENTDURATION"].ToString() == "0"))
                {
                    renttime = 1;
                }
                else
                {
                    renttime = GeneralFunctions.fnDouble(dr["RENTDURATION"].ToString());
                }


                if (dr["PRODUCTTYPE"].ToString() != "C")
                {
                    if (dr["PRODUCTTYPE"].ToString() == "Z") continue;
                    if (dr["PRODUCTTYPE"].ToString() == "H") continue;
                    if (dr["PRODUCTTYPE"].ToString() != "O")
                    {
                        if ((dr["SERVICE"].ToString() == "Sales") || (dr["SERVICE"].ToString() == "Repair")) dblSubTotal = GeneralFunctions.FormatDouble(dblSubTotal + rate * qty);
                        if (dr["SERVICE"].ToString() == "Rent")
                        {
                            if (Settings.TaxInclusive == "N")
                            {
                                dblSubTotal = GeneralFunctions.FormatDouble(dblSubTotal + rate * qty * renttime);
                            }
                            else
                            {
                                dblSubTotal = GeneralFunctions.FormatDouble(dblSubTotal + rate * qty);
                            }
                        }
                        dblDisc = GeneralFunctions.FormatDouble(dblDisc + GeneralFunctions.fnDouble(dr["DISCOUNT"].ToString()));
                    }
                    else
                    {
                        dblSubTotal = GeneralFunctions.FormatDouble(dblSubTotal + GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                    }
                }

                if (GeneralFunctions.fnInt32(dr["FEESID"].ToString()) > 0)
                {
                    if (dr["PRODUCTTYPE"].ToString() != "H")
                    {
                        feeqty = dr["FEESQTY"].ToString();

                        fees = fees + (feeqty == "Y" ? -GeneralFunctions.FormatDouble(qty * GeneralFunctions.fnDouble(dr["FEES"].ToString())) : -GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["FEES"].ToString())));
                        feestax = feestax + (feeqty == "Y" ? -GeneralFunctions.FormatDouble(qty * GeneralFunctions.fnDouble(dr["FEESTAX"].ToString())) : -GeneralFunctions.FormatDouble(GeneralFunctions.fnDouble(dr["FEESTAX"].ToString())));
                    }
                }

            }

            if (dblSubTotal >= 0)
            {
                dblSubTotal = -dblSubTotal;
            }

            //CouponCalculation(dtblPOS, ref dblCouponAmount);

            /*
            if (Settings.TaxInclusive == "Y")
            {
                dblSubTotal = GeneralFunctions.FormatDouble(dblSubTotal - dblTax);
            }*/

            numSubTotal = dblSubTotal.ToString("f2");
            numDiscount = dblDisc.ToString("f2");
            numTax = dblTax.ToString("f2");

            if (Settings.TaxInclusive == "N")
            {
                numTotal = (Convert.ToDouble(numSubTotal) - Convert.ToDouble(numDiscount) + fees + feestax + Convert.ToDouble(numTax) + dblFeesCouponAmount + dblFeesCouponTaxAmount - dblCouponAmount - dblSpecialMixMatchAmount).ToString("f2");
            }
            else
            {
                numTotal = (Convert.ToDouble(numSubTotal) + fees + feestax + Convert.ToDouble(numTax) + dblFeesCouponAmount + dblFeesCouponTaxAmount - dblCouponAmount - dblSpecialMixMatchAmount).ToString("f2");
            }
            /*if (Settings.TaxInclusive == "N")
            {
                numTotal.Text = numSubTotal.Text - numDiscount.Text + fees + feestax + numTax.Text + dblFeesCouponAmount + dblFeesCouponTaxAmount - dblCouponAmount - dblSpecialMixMatchAmount;
            }
            else
            {
                numTotal.Text = numSubTotal.Text - numDiscount.Text + fees + feestax + dblFeesCouponAmount + dblFeesCouponTaxAmount - dblCouponAmount - dblSpecialMixMatchAmount;
            }*/

            dblFeesCrg = fees + feestax;

            return GeneralFunctions.fnDouble(numTotal);
        }

        private double GetPartRefundAmount()
        {
            dtblPOS.Rows.Clear();
            blChangeCustomer = true;
            string refTaxExempt = "";
            string refDiscountLevel = "";
            string refTaxID = "";
            string refStoreCr = "";
            string refCID = "";
            string refCName = "";
            string refCAdd = "";
            double dblBalance = 0;
            string refARCredit = "";
            string refPOSNotes = "";

            int refDTaxID = 0;
            string refDTax = "";
            double refDTaxRate = 0;
            int refDTaxType = 0;


            if (intCustID > 0)
            {
                FetchCustomer(intCustID, ref refCID, ref refCName, ref refCAdd, ref refTaxExempt, ref refDiscountLevel,
                            ref refTaxID, ref refStoreCr, ref refARCredit, ref refPOSNotes, ref refDTaxID, ref refDTax,
                            ref refDTaxRate, ref refDTaxType);

                CustDTaxID = refDTaxID;
                CustDTaxName = refDTax;
                CustDTaxRate = refDTaxRate;
                CustDTaxType = refDTaxType;

                strTaxExempt = refTaxExempt;
                strDiscountLevel = refDiscountLevel.Trim();
                if (strDiscountLevel == "") strDiscountLevel = "A";
                dblBalance = GetAccountBalance(intCustID);
                
                

            }

            double intQty = 0;
            double dblPrice = 0;
            foreach (DataRow dr in dtblReturnItem.Rows)
            {
                string ss = dr["ID"].ToString();
                intQty = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                dblPrice = Settings.TaxInclusive == "N" ? GeneralFunctions.fnDouble(dr["Price"].ToString()) : GeneralFunctions.fnDouble(dr["GRate"].ToString());
                double tempprice = 0;
                if (dr["ProductType"].ToString() != "O")
                {
                    if (GeneralFunctions.fnDouble(dr["Discount"].ToString()) != 0) tempprice = (intQty * dblPrice) - GeneralFunctions.fnDouble(dr["Discount"].ToString());
                    else tempprice = (intQty * dblPrice);
                }
                else
                {
                    tempprice = -GeneralFunctions.fnDouble(intQty * dblPrice);
                }

                dtblPOS.Rows.Add(new object[]
                                        {     dr["ProductID"].ToString(),GetProductDescInCart1(dr["Description"].ToString(),
                                              dr["DiscountID"].ToString(),dr["FEESID"].ToString(), dr["BuyNGetFreeCategory"].ToString()),dr["ProductType"].ToString(),"0","0","0",
                                              "-" + GeneralFunctions.GetDisplayQty(dr["Qty"].ToString().Remove(0,1),dr["QtyDecimal"].ToString(),dr["ProductType"].ToString()),
                                              dr["Price"].ToString(),dr["Price"].ToString(),
                                              tempprice.ToString(),"0","0","0",dr["ID"].ToString(),"","","",GetUniqueString(),
                                              "2","",dr["DiscLogic"].ToString(),dr["DiscValue"].ToString(),dr["Discount"].ToString(),
                                              dr["DiscountID"].ToString(),dr["DiscountText"].ToString(),"1",
                                              GeneralFunctions.fnInt32(dr["TX1ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX3ID"].ToString()),
                                              "","","",
                                              GeneralFunctions.fnDouble(dr["TXRATE1"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TXRATE2"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TXRATE3"].ToString()),
                                              dr["TAXABLE1"].ToString(),
                                              dr["TAXABLE2"].ToString(),
                                              dr["TAXABLE3"].ToString(),
                                              "Sales","NA","0","0","0","","","",
                                              GeneralFunctions.fnInt32(dr["TX1TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX1ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX3ID"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX1"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX2"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX3"].ToString()),
                                              0,"","",0,0,0,"",
                                              dr["FEESID"].ToString(),
                                              dr["FEESLOGIC"].ToString(),
                                              dr["FEESVALUE"].ToString(),
                                              dr["FEESTAXRATE"].ToString(),
                                              dr["FEES"].ToString(),
                                              dr["FEESTAX"].ToString(),
                                              dr["FEESTEXT"].ToString(),
                                              dr["FEESQTY"].ToString(),
                                              0,
                                              dr["DTXID"].ToString(),
                                              dr["DTXTYPE"].ToString(),
                                              dr["DTXRATE"].ToString(),
                                              dr["DTX"].ToString(),
                                              dr["EditFlag"].ToString(),
                                              dr["PromptPrice"].ToString(),
                                              dr["BuyNGetFreeHeaderID"].ToString(),
                                              dr["BuyNGetFreeCategory"].ToString(),
                                              dr["SL"].ToString(),
                                              dr["BuyNGetFreeName"].ToString(),
                                              GeneralFunctions.fnInt32(dr["Age"].ToString()),
                                              GeneralFunctions.fnDouble(dr["GRate"].ToString()),
                                              tempprice.ToString(),
                                              dr["UOM"].ToString(),"","","",""
                                        });
            }
            
            double val = GetTotal();

            return val;
        }



        private async void VoidReceipt_Click(object sender, RoutedEventArgs e)
        {
            int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
            if (new MessageBoxWindow().Show(Properties.Resources.Do_you_want_to_void_this_invoice__, Properties.Resources.Void_Transaction, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (INV != 0)
                {
                    try
                    {
                        PosDataObject.POS objPos = new PosDataObject.POS();
                        objPos.Connection = SystemVariables.Conn;
                        int XEConnectID = objPos.FetchIdFromXeConnect(INV);
                        if (XEConnectID == 0) XEConnectID = INV;

                        var evoTransactionForm = new frm_EvoTransaction(0, XEConnectID, isRefundOrVoid: true, isExecuteVoid: true, dtblPart: null, calledfrom: this);
                        blurGrid.Visibility = Visibility.Visible;
                        evoTransactionForm.ShowDialog();
                        blurGrid.Visibility = Visibility.Collapsed;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private async void CmbP_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void CmbC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void CmbE_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdHeader.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView2.FocusedRowHandle == 0) return;
            gridView2.FocusedRowHandle = gridView2.FocusedRowHandle - 1;
        }

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if ((grdHeader.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView2.FocusedRowHandle == (grdHeader.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView2.FocusedRowHandle = gridView2.FocusedRowHandle + 1;
        }

        private void BtnUpDetail_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDetail.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == 0) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        private void BtnDownDetail_Click(object sender, RoutedEventArgs e)
        {
            if ((grdDetail.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle == (grdDetail.ItemsSource as DataTable).Rows.Count - 1) return;
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }

        private void CmbStore_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbP_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtF_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void GridView1_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                grdDetail.SetCellValue(e.RowHandle, colCheck, e.Value);

                int intCheck = 0;
                int intUncheck = 0;
                DataTable dtbl = grdDetail.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (Convert.ToBoolean(dr["CheckReturned"]))
                    {
                        intCheck++;
                    }
                    else
                    {
                        intUncheck++;
                    }
                }
                grdDetail.ItemsSource = dtbl;
                if (dtbl.Rows.Count == intCheck)
                {
                    ChkReturned.IsChecked = true;
                    ChkReturned.Content = "Unselect all receipt items";
                }

                if (dtbl.Rows.Count == intUncheck)
                {
                    ChkReturned.IsChecked = false;
                    ChkReturned.Content = "Select all receipt items";
                }
                dtbl.Dispose();
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }


}
