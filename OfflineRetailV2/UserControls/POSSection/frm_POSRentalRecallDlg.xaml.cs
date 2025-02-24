using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

using Microsoft.PointOfService;

using OfflineRetailV2.Data;
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
using System.Windows.Threading;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSRentalRecallDlg.xaml
    /// </summary>
    public partial class frm_POSRentalRecallDlg : Window
    {

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        public frm_POSRentalRecallDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSRentalRecallDlg_Loaded;
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

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
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

        private async void Frm_POSRentalRecallDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Recall_Rent;
            lbElapsed.Visibility = Visibility.Collapsed;
            btnVoid.IsEnabled = false;
            
            if (Settings.DecimalPlace == 3)
            {
                colAmount.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colDeposit.EditSettings = new TextEditSettings()
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
                colDeposit.EditSettings = new TextEditSettings()
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
            dtblReturnItem.Columns.Add("RentType", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("RentDuration", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("RentDeposit", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("Taxable1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Taxable2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("Taxable3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TaxID1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TaxID2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TaxID3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TaxRate1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TaxRate2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TaxRate3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TX1ID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2ID", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3ID", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TX1TYPE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2TYPE", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3TYPE", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("TX1", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX2", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("TX3", System.Type.GetType("System.String"));

            dtblReturnItem.Columns.Add("FEESID", System.Type.GetType("System.String"));//62
            dtblReturnItem.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//63
            dtblReturnItem.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));//64
            dtblReturnItem.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));//65
            dtblReturnItem.Columns.Add("FEES", System.Type.GetType("System.String"));//66
            dtblReturnItem.Columns.Add("FEESTAX", System.Type.GetType("System.String"));//67
            dtblReturnItem.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//68
            dtblReturnItem.Columns.Add("FEESQTY", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("EditFlag", System.Type.GetType("System.String"));
            dtblReturnItem.Columns.Add("PromptPrice", System.Type.GetType("System.String"));

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
            cmbInvFilter.Items.Clear();
            cmbInvFilter.Items.Add("Open");
            cmbInvFilter.Items.Add("All");
            cmbInvFilter.SelectedIndex = 0;
            PopulateCustomer();
            PopulateSKU(0);
            gridView2.FocusedRowHandle = -1;
            await FetchHeaderData();
            blFetch = true;

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)),
                    await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colrentcalcflg),
                    await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colReturnFlag));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }

            if (intMainScreenCustID != 0)
            {
                LookUpEdit clkup = new LookUpEdit();
                //foreach (Control c in panel1.Controls) --Sam
                //{
                //    if (c is LookUpEdit)
                //    {
                //        if (c.Name == "cmbC") clkup = (LookUpEdit)c;
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
                gridView2.FocusedRowHandle = -1;
                await FetchHeaderData();
            }

            if (Settings.ScaleDevice == "Datalogic Scale")
            {
                PrepareDatalogicScanner();
            }
        }

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }


        private string SCAN = "";
        PosExplorer m_posExplorer = null;
        Scanner m_posScanner = null;


        private volatile int watchr = 0;
        private FileSystemWatcher watcher;
        private bool blCG = false;
        private string CGresp = "";
        private string CGresptxt = "";
        private string CGmonitor = "";
        private string CGrequestfile = "";
        private string CGanswerfile = "";
        private string CGtrantype = "";
        private double CGamt = 0;
        private int CGinv = 0;


        SortAndSearchLookUpEdit sortAndSearchLookUpEditC;
        private int intCustID;
        private bool blFetch = false;
        private int PrevInv = 0;
        private int CurrInv = 0;
        private int intMainScreenCustID;
        private DataTable dtblReturnItem = null;
        private double dbldep1;
        private double dbldep2;
        private double dbldep3;
        private string strInvNo;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blCardPayment = false;
        private bool IsReturnTransaction = false;

        private string strCalc;


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

        private string PrecidiaLogFile = "";
        private string PrecidiaLogPath = "";

        #region POSLink payment variables
        private double POSLink_CashBack = 0;
        private string POSLink_ResultCode = "";
        private string POSLink_ResultTxt = "";
        private string POSLink_RefNum = "";
        private string POSLink_AuthCode = "";
        private string POSLink_CardType = "";
        private string POSLink_BogusAccountNum = "";
        private double POSLink_RequestedAmt = 0;
        private double POSLink_ApprovedAmt = 0;
        private double POSLink_RemainingBalance = 0;
        private double POSLink_ExtraBalance = 0;

        private string POSLinkLogFile = "";
        private string POSLinkLogPath = "";

        #endregion

        public string Calc
        {
            get { return strCalc; }
            set { strCalc = value; }
        }

        public bool blReturnTransaction
        {
            get { return IsReturnTransaction; }
            set { IsReturnTransaction = value; }
        }

        public string InvNo
        {
            get { return strInvNo; }
            set { strInvNo = value; }
        }

        public DataTable ReturnItem
        {
            get { return dtblReturnItem; }
            set { dtblReturnItem = value; }
        }
        public int CustID
        {
            get { return intCustID; }
            set { intCustID = value; }
        }
        public int MainScreenCustID
        {
            get { return intMainScreenCustID; }
            set { intMainScreenCustID = value; }
        }
        public double dep1
        {
            get { return dbldep1; }
            set { dbldep1 = value; }
        }
        public double dep2
        {
            get { return dbldep2; }
            set { dbldep2 = value; }
        }
        public double dep3
        {
            get { return dbldep3; }
            set { dbldep3 = value; }
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

        #region POSLink Log

        private string POSLinkLogFilePath()
        {

            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\POSLink Logs";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\POSLink Logs";

            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + POSLinkLogFile;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + POSLinkLogFile;
            }
            return strfilename;
        }

        private void WriteToPOSLinkLogFile(string txt)
        {
            FileStream fileStrm;
            if (File.Exists(POSLinkLogPath)) fileStrm = new FileStream(POSLinkLogPath, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(POSLinkLogPath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine(txt);
            sw.Close();
            fileStrm.Close();
        }

        #endregion

        public class StopWatch
        {

            private DateTime startTime;
            private DateTime stopTime;
            private DateTime currentTime;
            private bool running = false;
            private string renttype = "";

            public void Start(DateTime st, string rtype)
            {
                this.startTime = st;
                this.renttype = rtype;
                this.running = true;
            }


            public void Stop()
            {
                this.stopTime = DateTime.Now;
                this.running = false;
            }



            // elaspsed time in milliseconds
            public string GetElapsedTimeString()
            {
                TimeSpan interval;

                if (!running)
                    return ""; //interval = DateTime.Now - startTime;
                else
                {
                    this.currentTime = DateTime.Now;
                    interval = currentTime - startTime;
                }

                int days = interval.Days;
                int hours = interval.Hours;
                int mins = interval.Minutes;
                int secs = interval.Seconds;
                string x = "";

                if (renttype == "MI")
                {
                    x += (days * 24 * 60 + hours * 60 + mins).ToString("00") + ":";
                    x += secs.ToString("00");
                }

                if (renttype == "HR")
                {
                    x += (days * 24 + hours).ToString("00") + ":";
                    x += mins.ToString("00");
                }

                if (renttype == "HD")
                {
                    if (hours >= 12)
                    {
                        if (days != 0) x += ((days * 2) + 1).ToString() + " half day  ";
                        else x += "1 half day";
                    }
                    else
                    {
                        if (days != 0) x += (days * 2).ToString() + " half day  ";
                    }
                    if (hours - 12 > 0)
                        x += (hours - 12).ToString("00") + " hr ";
                }

                if (renttype == "DY")
                {

                    if (days != 0) x += days.ToString() + " d  ";
                    x += hours.ToString("00") + " hr ";
                }

                if (renttype == "WK")
                {
                    decimal w = days / 7;
                    decimal wk = Math.Ceiling(w);
                    decimal remaindays = days - wk * 7;
                    if (wk != 0) x += wk.ToString() + " wk  ";
                    if (remaindays != 0) x += remaindays.ToString() + " d ";
                }

                if (renttype == "MN")
                {
                    decimal m = days / 30;
                    decimal mn = Math.Ceiling(m);
                    decimal remaindays = days - mn * 30;
                    if (mn != 0) x += mn.ToString() + " mn  ";
                    if (remaindays != 0) x += remaindays.ToString() + " d ";
                }

                return x;
            }
            public double GetElapsedMilliseconds()
            {
                TimeSpan interval;

                if (running)
                    interval = DateTime.Now - startTime;
                else
                    interval = stopTime - startTime;

                return interval.TotalMilliseconds;
            }


            // elaspsed time in seconds
            public double GetElapsedTimeSecs()
            {
                TimeSpan interval;

                if (running)
                    interval = DateTime.Now - startTime;
                else
                    interval = stopTime - startTime;

                return interval.TotalSeconds;
            }
        }

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

        

        private bool CheckActiveCustomer(int pCust)
        {
            PosDataObject.POS objpos = new PosDataObject.POS();
            objpos.Connection = SystemVariables.Conn;
            return objpos.IsActiveCustomer(pCust);
        }


        private async Task FetchHeaderData()
        {
            
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRentHeaderData(GeneralFunctions.fnInt32(cmbP.EditValue.ToString()),
                   GeneralFunctions.fnInt32(cmbC.EditValue.ToString()), cmbDate.SelectedIndex, dtF.DateTime, dtT.DateTime,
                   cmbAmount.SelectedIndex, Convert.ToDouble(numF.Text), Convert.ToDouble(numT.Text), GeneralFunctions.GetCloseOutID(),
                   cmbInvFilter.SelectedIndex, cmbStore.Text);

            foreach (DataRow dr in dtbl.Rows)
            {
                if (dr["CanVoid"].ToString() == "N") continue;
                PosDataObject.POS objPOS1 = new PosDataObject.POS();
                objPOS1.Connection = SystemVariables.Conn;
                bool f = objPOS1.IsRentParentExist(GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString()));
                if (f) dr["CanVoid"] = "N";
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

            grdHeader.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblTemp.Dispose();


            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)),
                                await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colrentcalcflg),
                                await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colReturnFlag));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));

                /*if ((await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRentStatus) == "15") &&
                    (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInfo4) == ""))
                {
                    timer1.Enabled = true;
                    stopWatch.Start(GeneralFunctions.fnDate(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, coldt)));
                    lbElapsed.Visible = true;
                }
                else
                {
                    timer1.Enabled = false;
                    stopWatch.Stop();
                    lbElapsed.Visible = false;
                }*/
            }
        }

        private async void FetchHeaderData_Specific()
        {

            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRentHeaderData_Specific(GeneralFunctions.fnInt32(txtInv.Text.Trim()),
                   GeneralFunctions.GetCloseOutID(),
                   cmbInvFilter.SelectedIndex, cmbStore.Text);

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
                dtblTemp.Dispose();
            }

            dtbl.Dispose();

            if (gridView2.FocusedRowHandle >= 0)
            {
                await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)),
                                await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colrentcalcflg),
                                await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colReturnFlag));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            }

            txtInv.Text = "";
            txtInv.Focus();
        }

        private async Task FetchDetailData(int INV, string CALC, string STR)
        {
            gridView1.FocusedRowHandle = -1;
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRentDetailData(INV, CALC, STR);

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

            dtblTemp.Dispose();

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
            lbReceipt.Text = "Receipt Number            " + RNO;

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

            colQty.Header = "Qty";
            colDuration.Visible = true;
            colCheck.Visible = true;
            colSKU.VisibleIndex = 0;
            colDesc.VisibleIndex = 1;
            colQty.VisibleIndex = 2;
            colDuration.VisibleIndex = 3;
            colCheck.VisibleIndex = 4;

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colRentStatus) == "16")
            {
                colQty.Header = "Return Qty";
                colDuration.Visible = false;
                colCheck.Visible = false;
            }

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colReturnFlag) == "Y")
            {
                colDuration.Visible = true;
                colCheck.Visible = false;
            }

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInfo4) == "void")
            {
                colDuration.Visible = false;
                colCheck.Visible = false;
            }

            bool flag = false;
            foreach (DataRow drt in dtbl.Rows)
            {
                if (drt["ProductType"].ToString() == "T")
                {
                    flag = true;
                    break;
                }
            }

            if (gridView2.FocusedRowHandle >= 0)
            {
                if (gridView1.FocusedRowHandle >= 0)
                {
                    if ((await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colRentStatus) == "15") &&
                    (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInfo4) == ""))
                    {
                        timer1.Start();
                        stopWatch.Start(GeneralFunctions.fnDate(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, coldt)),
                            await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colrenttype));
                        lbElapsed.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        timer1.Stop();
                        stopWatch.Stop();
                        lbElapsed.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }



        private async void gridView2_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

            if (gridView2.FocusedRowHandle >= 0)
            {
                if (blFetch)
                    await FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv)),
                                        await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colrentcalcflg),
                                        await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colReturnFlag));

                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                CurrInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));

                btnVoid.IsEnabled = (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colcanvoid) == "Y");

                /*if ((await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRentStatus) == "15") &&
                    (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInfo4) == ""))
                {
                    timer1.Enabled = true;
                    stopWatch.Start(GeneralFunctions.fnDate(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, coldt)));
                    lbElapsed.Visible = true;
                }
                else
                {
                    timer1.Enabled = false;
                    stopWatch.Stop();
                    lbElapsed.Visible = false;
                }*/
                if (PrevInv == 0)
                {
                    PrevInv = CurrInv;
                }
            }
        }

        private async void cmbDate_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbDate.SelectedIndex == 0)
            {

                dtF.Visibility = Visibility.Collapsed;
                dtT.Visibility = Visibility.Collapsed;
            }
            if ((cmbDate.SelectedIndex == 1) || (cmbDate.SelectedIndex == 2))
            {

                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Collapsed;
            }
            if (cmbDate.SelectedIndex == 3)
            {

                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Visible;
            }
            if (blFetch)
            {
                gridView2.FocusedRowHandle = -1;
                await FetchHeaderData();
            }
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

        private async void numF_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
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

        private async void numT_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private string GetUniqueString()
        {
            return Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) +
                Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        }

        private async void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            dtblReturnItem.Rows.Clear();
            /*if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID)) != 0)
            {
                if (!CheckActiveCustomer(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID))))
                {
                    DocMessage.MsgInformation(Properties.Resources."Transaction can not be possible for an inactive customer");
                    return;
                }
            }*/

            DataTable dtbl = new DataTable();
            dtbl = grdDetail.ItemsSource as DataTable;
            if (dtbl == null) return;
            double dqty = 0;
            double disc = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (!Convert.ToBoolean(dr["ReturnCheck"].ToString())) continue;
                dqty = 0;
                dqty = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                disc = 0;
                disc = GeneralFunctions.fnDouble(dr["Discount"].ToString());

                dtblReturnItem.Rows.Add(new object[] {dr["ID"].ToString(),
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
                                                dr["RentType"].ToString(),
                                                dr["RentDuration"].ToString(),
                                                dr["RentDeposit"].ToString(),
                                                dr["Taxable1"].ToString(),
                                                dr["Taxable2"].ToString(),
                                                dr["Taxable3"].ToString(),
                                                dr["TaxID1"].ToString(),
                                                dr["TaxID2"].ToString(),
                                                dr["TaxID3"].ToString(),
                                                dr["TaxRate1"].ToString(),
                                                dr["TaxRate2"].ToString(),
                                                dr["TaxRate3"].ToString(),
                                                dr["TaxID1"].ToString(),
                                                dr["TaxID2"].ToString(),
                                                dr["TaxID3"].ToString(),
                                                dr["TaxType1"].ToString(),
                                                dr["TaxType2"].ToString(),
                                                dr["TaxType3"].ToString(),
                                                dr["TaxTotal1"].ToString(),
                                                dr["TaxTotal2"].ToString(),
                                                dr["TaxTotal3"].ToString(),
                dr["FeesID"].ToString(),
                dr["FeesLogic"].ToString(),
                dr["FeesValue"].ToString(),
                dr["FeesTaxRate"].ToString(),
                dr["Fees"].ToString(),
                dr["FeesTax"].ToString(),
                dr["FeesText"].ToString(),
                dr["FeesQty"].ToString(),
                dr["EditFlag"].ToString(),
                dr["PromptPrice"].ToString()});
            }
            if (dtblReturnItem.Rows.Count > 0)
            {
                strCalc = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colrentcalcflg);
                strInvNo = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv);
                dbldep1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDeposit1));
                dbldep2 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDeposit2));
                dbldep3 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDeposit));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                DataTable dtblc = new DataTable();
                dtblc = dtblReturnItem;
                bool blmore = false;
                foreach (DataRow drt in dtblc.Rows)
                {
                    if (GeneralFunctions.fnDouble(drt["Qty"].ToString()) > 1)
                    {
                        blmore = true;
                        break;
                    }
                }

                if ((dbldep3 > 0) || (blmore) || (strCalc == "Y"))
                {
                    if ((strCalc == "Y"))
                    {
                        string strRentType = "NA";
                        double dblRentDuration = 0;
                        double dblRentValue = 0;

                        bool blExit = false;
                        blurGrid.Visibility = Visibility.Visible;
                        foreach (DataRow drnt in dtblReturnItem.Rows)
                        {
                            strRentType = drnt["RentType"].ToString();
                            dblRentDuration = GeneralFunctions.fnDouble(drnt["RentDuration"].ToString());
                            dblRentValue = GeneralFunctions.fnDouble(drnt["Price"].ToString());
                            frmPOSRentalSelectDlg frmrentsel = new frmPOSRentalSelectDlg();
                            try
                            {
                                frmrentsel.PID = GeneralFunctions.fnInt32(drnt["ProductID"].ToString());
                                frmrentsel.bcallforrentsetbeforereturn = true;
                                frmrentsel.RentType = strRentType;
                                frmrentsel.RentDuration = dblRentDuration;
                                frmrentsel.RentValue = dblRentValue;
                                frmrentsel.ShowDialog();
                                if (frmrentsel.DialogResult == true)
                                {
                                    dblRentDuration = frmrentsel.RentDuration;
                                    dblRentValue = frmrentsel.RentValue;
                                    drnt["Price"] = dblRentValue.ToString();
                                    drnt["RentDuration"] = dblRentDuration.ToString();
                                }
                                else blExit = true;
                            }
                            finally
                            {
                            }

                            if (blExit) break;
                        }
                        blurGrid.Visibility = Visibility.Collapsed;
                        if (blExit) return;
                    }
                    DialogResult = true;
                    ResMan.closeKeyboard();
                    CloseKeyboards();
                    Close();
                }
                else
                {
                    DataTable dtblPOS = new DataTable();

                    dtblPOS.Columns.Add("ID", System.Type.GetType("System.String"));//1
                    dtblPOS.Columns.Add("PRODUCT", System.Type.GetType("System.String"));//2
                    dtblPOS.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));//3
                    dtblPOS.Columns.Add("ONHANDQTY", System.Type.GetType("System.String"));//4
                    dtblPOS.Columns.Add("NORMALQTY", System.Type.GetType("System.String"));//5
                    dtblPOS.Columns.Add("COST", System.Type.GetType("System.String"));//6
                    dtblPOS.Columns.Add("QTY", System.Type.GetType("System.Double"));//7
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

                    dtblPOS.Columns.Add("DISCLOGIC", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("DISCVALUE", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("DISCOUNT", System.Type.GetType("System.String"));//33
                    dtblPOS.Columns.Add("DISCOUNTID", System.Type.GetType("System.String"));//34
                    dtblPOS.Columns.Add("DISCOUNTTEXT", System.Type.GetType("System.String"));//35
                    dtblPOS.Columns.Add("ITEMINDEX", System.Type.GetType("System.String"));//36

                    // for blankline
                    dtblPOS.Columns.Add("TAXID1", System.Type.GetType("System.String"));//21
                    dtblPOS.Columns.Add("TAXID2", System.Type.GetType("System.String"));//22
                    dtblPOS.Columns.Add("TAXID3", System.Type.GetType("System.String"));//23
                    dtblPOS.Columns.Add("TAXNAME1", System.Type.GetType("System.String"));//24
                    dtblPOS.Columns.Add("TAXNAME2", System.Type.GetType("System.String"));//25
                    dtblPOS.Columns.Add("TAXNAME3", System.Type.GetType("System.String"));//26
                    dtblPOS.Columns.Add("TAXRATE1", System.Type.GetType("System.String"));//27
                    dtblPOS.Columns.Add("TAXRATE2", System.Type.GetType("System.String"));//28
                    dtblPOS.Columns.Add("TAXRATE3", System.Type.GetType("System.String"));//29
                    dtblPOS.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));//30
                    dtblPOS.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));//31
                    dtblPOS.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));//32

                    // service type
                    dtblPOS.Columns.Add("SERVICE", System.Type.GetType("System.String"));//33

                    // for rent
                    dtblPOS.Columns.Add("RENTTYPE", System.Type.GetType("System.String"));//34
                    dtblPOS.Columns.Add("RENTDURATION", System.Type.GetType("System.Double"));//35
                    dtblPOS.Columns.Add("RENTAMOUNT", System.Type.GetType("System.Double"));//36
                    dtblPOS.Columns.Add("RENTDEPOSIT", System.Type.GetType("System.Double"));//37

                    // for Tax pickup from Tax Table
                    dtblPOS.Columns.Add("TX1TYPE", System.Type.GetType("System.Int32"));//41
                    dtblPOS.Columns.Add("TX2TYPE", System.Type.GetType("System.Int32"));//42
                    dtblPOS.Columns.Add("TX3TYPE", System.Type.GetType("System.Int32"));//43
                    dtblPOS.Columns.Add("TX1ID", System.Type.GetType("System.Int32"));//44
                    dtblPOS.Columns.Add("TX2ID", System.Type.GetType("System.Int32"));//45
                    dtblPOS.Columns.Add("TX3ID", System.Type.GetType("System.Int32"));//46
                    dtblPOS.Columns.Add("TX1", System.Type.GetType("System.Double"));//47
                    dtblPOS.Columns.Add("TX2", System.Type.GetType("System.Double"));//48
                    dtblPOS.Columns.Add("TX3", System.Type.GetType("System.Double"));//49

                    dtblPOS.Columns.Add("MIXMATCHID", System.Type.GetType("System.Int32"));//56
                    dtblPOS.Columns.Add("MIXMATCHFLAG", System.Type.GetType("System.String"));//57
                    dtblPOS.Columns.Add("MIXMATCHTYPE", System.Type.GetType("System.String"));//58
                    dtblPOS.Columns.Add("MIXMATCHVALUE", System.Type.GetType("System.Double"));//59
                    dtblPOS.Columns.Add("MIXMATCHQTY", System.Type.GetType("System.Int32"));//60
                    dtblPOS.Columns.Add("MIXMATCHUNIQUE", System.Type.GetType("System.Int32"));//61
                    dtblPOS.Columns.Add("MIXMATCHLAST", System.Type.GetType("System.String"));//61

                    // for Fees & Charges
                    dtblPOS.Columns.Add("FEESID", System.Type.GetType("System.String"));//62
                    dtblPOS.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//63
                    dtblPOS.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));//64
                    dtblPOS.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));//65
                    dtblPOS.Columns.Add("FEES", System.Type.GetType("System.String"));//66
                    dtblPOS.Columns.Add("FEESTAX", System.Type.GetType("System.String"));//67
                    dtblPOS.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//68
                    dtblPOS.Columns.Add("FEESQTY", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("SALEPRICEID", System.Type.GetType("System.Int32"));//56

                    dtblPOS.Columns.Add("DTXID", System.Type.GetType("System.Int32"));//65
                    dtblPOS.Columns.Add("DTXTYPE", System.Type.GetType("System.Int32"));//66
                    dtblPOS.Columns.Add("DTXRATE", System.Type.GetType("System.Double"));//67
                    dtblPOS.Columns.Add("DTX", System.Type.GetType("System.Double"));//68

                    dtblPOS.Columns.Add("EDITF", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("PROMPTPRICE", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("BUYNGETFREEHEADERID", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("BUYNGETFREECATEGORY", System.Type.GetType("System.String"));
                    dtblPOS.Columns.Add("SL", System.Type.GetType("System.Int32"));
                    dtblPOS.Columns.Add("BUYNGETFREENAME", System.Type.GetType("System.String"));
                    dtblPOS.DefaultView.Sort = "ITEMINDEX asc";
                    dtblPOS.DefaultView.ApplyDefaultSort = true;

                    double intQty = 0;
                    double dblPrice = 0;
                    double dblduration = 0;
                    int rowno = 0;
                    foreach (DataRow dr in dtblReturnItem.Rows)
                    {
                        string ss = dr["ID"].ToString();
                        intQty = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                        dblPrice = GeneralFunctions.fnDouble(dr["Price"].ToString());
                        dblduration = GeneralFunctions.fnDouble(dr["RentDuration"].ToString());
                        double tempprice = 0;
                        if (dr["DiscountID"].ToString() != "0") tempprice = (intQty * dblPrice * dblduration) - GeneralFunctions.fnDouble(dr["Discount"].ToString());
                        else tempprice = (intQty * dblPrice * dblduration);
                        rowno++;
                        dtblPOS.Rows.Add(new object[]
                                        {
                                              dr["ProductID"].ToString(),
                                              dr["Description"].ToString(),
                                              dr["ProductType"].ToString(),"0","0","0",
                                              dr["Qty"].ToString(),
                                              dr["Price"].ToString(),
                                              dr["Price"].ToString(),
                                              tempprice.ToString(),
                                              "0","0","0",dr["ID"].ToString(),"","","",GetUniqueString(),"2","",

                                              dr["DiscLogic"].ToString(),
                                              dr["DiscValue"].ToString(),
                                              dr["Discount"].ToString(),
                                              dr["DiscountID"].ToString(),
                                              dr["DiscountText"].ToString(),
                                              "1",

                                              "0","0","0","","","","0","0","0","N","N","N",

                                              "Rent",
                                              dr["RentType"].ToString(),
                                              dr["RentDuration"].ToString(),
                                              dr["Price"].ToString(),
                                              dr["RentDeposit"].ToString(),
                                              GeneralFunctions.fnInt32(dr["TX1TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX3TYPE"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX1ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX2ID"].ToString()),
                                              GeneralFunctions.fnInt32(dr["TX3ID"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX1"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX2"].ToString()),
                                              GeneralFunctions.fnDouble(dr["TX3"].ToString()),

                                              0,"","",0,0,0,"",

                                              "0","","0","0","0","0","",0,0,
                                              0,0,0,0,
                                              dr["EditFlag"].ToString(),
                                              dr["PromptPrice"].ToString(),
                                              "0","X",rowno.ToString(),""});
                    }

                    int intINV = 0;
                    string srterrmsg = "";
                    PosDataObject.POS objpos = new PosDataObject.POS();
                    objpos.Connection = SystemVariables.Conn;
                    objpos.EmployeeID = SystemVariables.CurrentUserID;
                    objpos.CustomerID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                    objpos.TransType = 16; // sales
                    objpos.ReceiptCnt = 1;
                    objpos.Status = 16;
                    objpos.Tax = 0;
                    double tempcoupon = 0;
                    objpos.Coupon = tempcoupon;
                    objpos.Discount = 0;
                    objpos.DiscountReason = "";
                    objpos.TotalSale = 0;
                    objpos.ItemDataTable = FinalDataTable(dtblPOS);

                    objpos.TaxID1 = 0;
                    objpos.TaxID2 = 0;
                    objpos.TaxID3 = 0;
                    objpos.Tax1 = 0;
                    objpos.Tax2 = 0;
                    objpos.Tax3 = 0;
                    objpos.ErrorMsg = "";
                    objpos.ChangeAmount = 0;
                    objpos.SuspendInvoiceNo = 0;

                    objpos.ChangedByAdmin = intSuperUserID;
                    objpos.FunctionButtonAccess = blFunctionBtnAccess;

                    objpos.TenderDataTable = null;
                    // static value
                    objpos.StoreID = 1;
                    objpos.RegisterID = 1;
                    objpos.CloseoutID = GeneralFunctions.GetCloseOutID();
                    objpos.TransNoteNo = 0;
                    objpos.LayawayNo = 0;
                    objpos.TransMSeconds = 0;
                    // static value
                    objpos.TerminalName = Settings.TerminalName;
                    objpos.Return = false;
                    objpos.NewLayaway = false;
                    objpos.Layaway = false;
                    objpos.LayawayRefund = false;
                    objpos.ApptDataTable = null;
                    objpos.RentReturn = true;
                    objpos.ServiceType = "Rent";
                    objpos.RentalSecurityDeposit = 0;
                    objpos.IssueRentInvNo = GeneralFunctions.fnInt32(strInvNo);
                    objpos.GCCentralFlag = Settings.CentralExportImport;
                    objpos.GCOPStore = Settings.StoreCode;

                    objpos.OperateStore = Settings.StoreCode;
                    objpos.IsRentCalculated = "N";
                    objpos.BeginTransaction();
                    if (objpos.CreateInvoice())
                    {
                        intINV = objpos.ID;
                    }
                    objpos.EndTransaction();
                    if (intINV > 0)
                    {
                        PrintInvoice(intINV);
                        IsReturnTransaction = true;
                        DialogResult = true;
                        ResMan.closeKeyboard();
                        Close();
                    }
                }
            }
        }

        private DataTable FinalDataTable(DataTable dtblPOSDatatbl)
        {
            DataTable dtblFinal = new DataTable();
            dtblFinal.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRODUCT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ONHANDQTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("NORMALQTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("COST", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("QTY", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("RATE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("NRATE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("PRICE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXID3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAXRATE3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DEPT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("CAT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMPRICE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("UOMDESC", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("MATRIXOV3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMID", System.Type.GetType("System.String"));
            // add for layaway Invoice
            dtblFinal.Columns.Add("TAX1", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX2", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX3", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TAX", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("TOTALSALE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTREASON", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("LAYAWAYAMOUNT", System.Type.GetType("System.String"));

            // add for invoice notes
            dtblFinal.Columns.Add("NOTES", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("DISCLOGIC", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCVALUE", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMDISCOUNT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTID", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("DISCOUNTTEXT", System.Type.GetType("System.String"));
            dtblFinal.Columns.Add("ITEMINDEX", System.Type.GetType("System.String"));

            dtblFinal.Columns.Add("RENTTYPE", System.Type.GetType("System.String"));//34
            dtblFinal.Columns.Add("RENTDURATION", System.Type.GetType("System.Double"));//35
            dtblFinal.Columns.Add("RENTAMOUNT", System.Type.GetType("System.Double"));//36
            dtblFinal.Columns.Add("RENTDEPOSIT", System.Type.GetType("System.Double"));//37

            dtblFinal.Columns.Add("TX1TYPE", System.Type.GetType("System.Int32"));//41
            dtblFinal.Columns.Add("TX2TYPE", System.Type.GetType("System.Int32"));//42
            dtblFinal.Columns.Add("TX3TYPE", System.Type.GetType("System.Int32"));//43
            dtblFinal.Columns.Add("TX1ID", System.Type.GetType("System.Int32"));//44
            dtblFinal.Columns.Add("TX2ID", System.Type.GetType("System.Int32"));//45
            dtblFinal.Columns.Add("TX3ID", System.Type.GetType("System.Int32"));//46
            dtblFinal.Columns.Add("TX1", System.Type.GetType("System.Double"));//47
            dtblFinal.Columns.Add("TX2", System.Type.GetType("System.Double"));//48
            dtblFinal.Columns.Add("TX3", System.Type.GetType("System.Double"));//49

            // fetch TaxID, TaxName first

            string strTaxID1 = "0";
            string strTaxID2 = "0";
            string strTaxID3 = "0";
            string strTaxName1 = "";
            string strTaxName2 = "";
            string strTaxName3 = "";
            string strTaxRate1 = "0";
            string strTaxRate2 = "0";
            string strTaxRate3 = "0";
            int intCount = 0;
            DataTable dtblTaxHeader = new DataTable();
            PosDataObject.Tax objTax = new PosDataObject.Tax();
            objTax.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtblTaxHeader = objTax.FetchActiveTax();
            foreach (DataRow dr in dtblTaxHeader.Rows)
            {
                intCount++;
                if (intCount == 1)
                {
                    strTaxID1 = dr["ID"].ToString();
                    strTaxName1 = dr["TaxName"].ToString();
                    strTaxRate1 = dr["TaxRate"].ToString();
                }
                if (intCount == 2)
                {
                    strTaxID2 = dr["ID"].ToString();
                    strTaxName2 = dr["TaxName"].ToString();
                    strTaxRate2 = dr["TaxRate"].ToString();
                }
                if (intCount == 3)
                {
                    strTaxID3 = dr["ID"].ToString();
                    strTaxName3 = dr["TaxName"].ToString();
                    strTaxRate3 = dr["TaxRate"].ToString();
                    break;
                }
            }
            dtblTaxHeader.Dispose();

            foreach (DataRow drR in dtblPOSDatatbl.Rows)
            {
                DataTable dtblR = new DataTable();
                PosDataObject.POS objR = new PosDataObject.POS();
                objR.Connection = new SqlConnection(SystemVariables.ConnectionString);
                int d = GeneralFunctions.fnInt32(drR["MATRIXOID"].ToString());
                dtblR = objR.FetchItemDetails(GeneralFunctions.fnInt32(drR["MATRIXOID"].ToString()));
                foreach (DataRow drR1 in dtblR.Rows)
                {
                    dtblFinal.Rows.Add(new object[] {
                                        drR1["ProductID"].ToString(),
                                        drR1["Description"].ToString(),
                                        drR1["ProductType"].ToString(),
                                        "0",
                                        "0",
                                        drR1["Cost"].ToString(),
                                        drR["Qty"].ToString(),
                                        drR1["Price"].ToString(),
                                        drR1["NormalPrice"].ToString(),
                                        drR1["Price"].ToString(),
                                        drR1["TaxID1"].ToString(),
                                        drR1["TaxID2"].ToString(),
                                        drR1["TaxID3"].ToString(),
                                        drR1["Taxable1"].ToString(),
                                        drR1["Taxable2"].ToString(),
                                        drR1["Taxable3"].ToString(),
                                        drR1["TaxRate1"].ToString(),
                                        drR1["TaxRate2"].ToString(),
                                        drR1["TaxRate3"].ToString(),
                                        drR1["SKU"].ToString(),
                                        drR1["DepartmentID"].ToString(),
                                        drR1["CategoryID"].ToString(),
                                        drR1["UOMCount"].ToString(),
                                        drR1["UOMPrice"].ToString(),
                                        drR1["UOMDesc"].ToString(),
                                        drR1["MatrixOptionID"].ToString(),
                                        drR1["OptionValue1"].ToString(),
                                        drR1["OptionValue2"].ToString(),
                                        drR1["OptionValue3"].ToString(),drR["MATRIXOID"].ToString(),
                                        "0","0","0","0","0","0","","0",drR["NOTES"].ToString(),
                                        drR1["DiscLogic"].ToString(),drR1["DiscValue"].ToString(),drR1["Discount"].ToString(),
                                        drR1["DiscountID"].ToString(),drR1["DiscountText"].ToString(),"1",
                                        drR["RENTTYPE"].ToString(),drR["RENTDURATION"].ToString(),
                                        drR["RENTAMOUNT"].ToString(),drR["RENTDEPOSIT"].ToString(),
                                        GeneralFunctions.fnInt32(drR["TX1TYPE"].ToString()),
                                        GeneralFunctions.fnInt32(drR["TX2TYPE"].ToString()),
                                        GeneralFunctions.fnInt32(drR["TX3TYPE"].ToString()),
                                        GeneralFunctions.fnInt32(drR["TX1ID"].ToString()),
                                        GeneralFunctions.fnInt32(drR["TX2ID"].ToString()),
                                        GeneralFunctions.fnInt32(drR["TX3ID"].ToString()),
                                        GeneralFunctions.fnDouble(drR["TX1"].ToString()),
                                        GeneralFunctions.fnDouble(drR["TX2"].ToString()),
                                        GeneralFunctions.fnDouble(drR["TX3"].ToString())});
                }
                dtblR.Dispose();
            }

            return dtblFinal;
        }

        private void PrintInvoice(int intINV)
        {
            if (Settings.GeneralReceiptPrint == "N")
            {
                /*if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/
                blurGrid.Visibility = Visibility.Visible;
                frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    frm_POSInvoicePrintDlg.PrintType = "Return Rent Item";
                    frm_POSInvoicePrintDlg.InvNo = intINV;
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
                }

                blCardPayment = IsCardPayment(intTranNo);

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
                OfflineRetailV2.Report.Sales.repInvCoupon rep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();
                rep_InvMain.rReprint.Text = "";
                if (Settings.ReceiptFooter == "")
                {
                    rep_InvMain.rReportFooter.HeightF = 1.0f;
                    rep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 2);
                    rep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 5);
                    rep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 5);

                    rep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 25);
                    rep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 25);
                    rep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 25);
                    rep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 25);

                    rep_InvMain.ReportFooter.Height = 60;
                    rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                }
                else
                {
                    rep_InvMain.ReportFooter.Height = 91;
                    rep_InvMain.rReportFooter.Text = Settings.ReceiptFooter;
                }

                rep_InvMain.subrepH1.ReportSource = rep_InvHeader1;
                rep_InvHeader1.Report.DataSource = dtbl;
                rep_InvHeader1.rReprint.Text = "";
                GeneralFunctions.MakeReportWatermark(rep_InvMain);
                rep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                rep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;

                if (strservice == "Rent")
                {
                    if (intHeaderStatus == 16) rep_InvHeader1.rType.Text = "Rent Item Returned";
                }
                if (strservice == "Repair")
                {
                    if (intHeaderStatus == 18)
                    {
                        rep_InvHeader1.rType.Text = "Repair Delivered";
                    }
                }
                rep_InvHeader1.rOrderNo.Text = intINV.ToString();
                if (Settings.PrintLogoInReceipt == "Y")
                {
                    if (!boolnulllogo) rep_InvHeader1.rPic.DataBindings.Add("Image", dtbl, "Logo");
                }
                rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "TransDate");

                rep_InvMain.xrBarCode.Text = intINV.ToString();

                if (intCID > 0)
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                    rep_InvHeader2.rCustName.DataBindings.Add("Text", dtbl, "CustName");
                    rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "CustCompany");
                }
                else
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustName.Text = "";
                    rep_InvHeader2.rCustID.Text = "";
                    rep_InvHeader2.rCompany.Text = "";
                    rep_InvHeader2.rlCustName.Text = "";
                    rep_InvHeader2.rlCustID.Text = "";
                    rep_InvHeader2.rlCompany.Text = "";
                }

                PosDataObject.POS objPOS2 = new PosDataObject.POS();
                objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                RearrangeForTaggedItemInInvoice(dtbl1);
                if ((intHeaderStatus == 16) && (calcrent == "Y"))
                {
                    foreach (DataRow d in dtbl1.Rows)
                    {
                        d["Qty"] = -GeneralFunctions.fnDouble(d["Qty"].ToString());
                        d["TotalPrice"] = -GeneralFunctions.fnDouble(d["TotalPrice"].ToString());
                    }
                }
                if (strservice == "Rent")
                {


                    if (intHeaderStatus == 16) // return
                    {
                        if (calcrent == "N")
                        {
                            rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                            rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentReturnLine.Report.DataSource = dtbl1;
                            rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                            rep_InvRentReturnLine.rlAmt.Visible = false;
                        }
                        if (calcrent == "Y")
                        {
                            rep_InvMain.subrepLine.ReportSource = rep_InvRentLine;
                            rep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentLine.Report.DataSource = dtbl1;
                            rep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            rep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            rep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                            rep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                            rep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                            rep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                            rep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                            rep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");
                        }
                    }


                }
                else if (strservice == "Repair")
                {
                    if (intHeaderStatus == 18) // return
                    {
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                        rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentReturnLine.Report.DataSource = dtbl1;
                        rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                    }
                }
                else
                {
                    rep_InvMain.subrepLine.ReportSource = rep_InvLine;
                    rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                    rep_InvLine.Report.DataSource = dtbl1;
                    rep_InvLine.rlSKU.DataBindings.Add("Text", dtbl1, "Qty");
                    rep_InvLine.rlIem.DataBindings.Add("Text", dtbl1, "SKU");
                    rep_InvLine.rlqty.DataBindings.Add("Text", dtbl1, "Description");
                    rep_InvLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                    rep_InvLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                    rep_InvLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                    rep_InvLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                    rep_InvLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");
                }

                foreach (DataRow dr12 in dtbl1.Rows)
                {
                    dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                }

                if (strservice == "Rent")
                {
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
                else
                {
                    rep_InvMain.subrepSubtotal.ReportSource = rep_InvSubtotal;
                    rep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                    rep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                    rep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                    rep_InvSubtotal.DR = strDiscountReason;
                    rep_InvSubtotal.rTax.Text = dblTax.ToString();
                }



                PosDataObject.POS objPOS4 = new PosDataObject.POS();
                objPOS4.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl3 = objPOS4.FetchInvoiceTender(intTranNo);

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
                rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");

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
                    rep_InvCoupon.Dispose();
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

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string receipttype = "";
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colRentStatus) == "15") receipttype = "Issue";
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colRentStatus) == "16") receipttype = "Return";
            int INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv));
            //int VOIDNO = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colVoidNo));
            int VOIDNO = 0;

            /*if (Settings.ReceiptPrinterName == "")
            {
                DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                return;
            }*/
            bool blf = false;
            if (VOIDNO > 0)
                blf = true;
            else
                blf = false;
            ReprintInvoice(INV, blf, receipttype);
        }

        private void ReprintInvoice(int intINV, bool blVoid, string receipttype)
        {
            UpdateReceiptCnt(intINV);
            if (Settings.GeneralReceiptPrint == "N")
            {
                /*if (Settings.ReceiptPrinterName == "")
                {
                    DocMessage.MsgInformation(Properties.Resources."Please Define Receipt Printer in Setup.","frmAttendanceBrw_msg_PleaseDefineReceiptPrinterinSe"));
                    return;
                }*/

                blurGrid.Visibility = Visibility.Visible;
                frmPOSInvoicePrintDlg frm_POSInvoicePrintDlg = new frmPOSInvoicePrintDlg();
                try
                {
                    if (receipttype == "Issue")
                    {
                        frm_POSInvoicePrintDlg.PrintType = "Invoice";
                        frm_POSInvoicePrintDlg.IsRentIssued = true;
                        frm_POSInvoicePrintDlg.IsRentReturned = false;
                    }
                    if (receipttype == "Return")
                    {
                        frm_POSInvoicePrintDlg.PrintType = "Return Rent Item";
                        frm_POSInvoicePrintDlg.IsRentIssued = false;
                        frm_POSInvoicePrintDlg.IsRentReturned = true;
                    }
                    frm_POSInvoicePrintDlg.InvNo = intINV;
                    frm_POSInvoicePrintDlg.ReprintCnt = GetReceiptCnt(intINV);
                    frm_POSInvoicePrintDlg.IsVoid = blVoid;
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
                }

                blCardPayment = IsCardPayment(intTranNo);

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
                OfflineRetailV2.Report.Sales.repInvRentLine rep_InvRentLine = new OfflineRetailV2.Report.Sales.repInvRentLine();
                OfflineRetailV2.Report.Sales.repInvRentSubTotal rep_InvRentSubTotal = new OfflineRetailV2.Report.Sales.repInvRentSubTotal();
                OfflineRetailV2.Report.Sales.repInvRentReturnLine rep_InvRentReturnLine = new OfflineRetailV2.Report.Sales.repInvRentReturnLine();
                OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal rep_InvRentReturnSubTotal = new OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal();

                if (blVoid)
                    rep_InvMain.rReprint.Text = "**** Reprinted Void Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";
                else
                    rep_InvMain.rReprint.Text = "**** Reprinted Receipt : " + GetReceiptCnt(intINV).ToString() + " ****";

                if (Settings.ReceiptFooter == "")
                {
                    rep_InvMain.rReportFooter.HeightF = 1.0f;
                    rep_InvMain.rReportFooter.LocationF = new System.Drawing.PointF(8, 2);
                    rep_InvMain.xrBarCode.LocationF = new System.Drawing.PointF(8, 5);
                    rep_InvMain.rCopy.LocationF = new System.Drawing.PointF(567, 5);

                    rep_InvMain.xrShape1.LocationF = new System.Drawing.PointF(581, 25);
                    rep_InvMain.xrPageInfo2.LocationF = new System.Drawing.PointF(594, 25);
                    rep_InvMain.xrPageInfo1.LocationF = new System.Drawing.PointF(681, 25);
                    rep_InvMain.xrShape2.LocationF = new System.Drawing.PointF(725, 25);

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
                    //rep_InvMain.rsign1.Visible = true;
                    //rep_InvMain.rsign2.Visible = true;
                }
                rep_InvMain.subrepH1.ReportSource = rep_InvHeader1;
                rep_InvHeader1.Report.DataSource = dtbl;
                rep_InvHeader1.rReprint.Text = "";
                GeneralFunctions.MakeReportWatermark(rep_InvMain);
                rep_InvHeader1.rReportHeaderCompany.Text = Settings.ReceiptHeader_Company;
                rep_InvHeader1.rReportHeader.Text = Settings.ReceiptHeader_Address;

                if (strservice == "Rent")
                {
                    if (intHeaderStatus == 15) rep_InvHeader1.rType.Text = "Rent Issued";
                    if (intHeaderStatus == 16) rep_InvHeader1.rType.Text = "Rent Item Returned";
                }
                if (strservice == "Repair")
                {
                    if (intHeaderStatus == 17)
                    {
                        if (strRepairDeliveryDate != "") rep_InvHeader1.rType.Text = "Repair In" + "      Expected Delivety Date : " + strRepairDeliveryDate;
                        else rep_InvHeader1.rType.Text = "Repair In";
                    }
                    if (intHeaderStatus == 18)
                    {
                        rep_InvHeader1.rType.Text = "Repair Delivered";
                    }
                }

                rep_InvHeader1.rOrderNo.Text = intINV.ToString();
                if (Settings.PrintLogoInReceipt == "Y")
                {
                    if (!boolnulllogo) rep_InvHeader1.rPic.DataBindings.Add("Image", dtbl, "Logo");
                }
                rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "TransDate");

                rep_InvMain.xrBarCode.Text = intINV.ToString();

                if (intCID > 0)
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                    rep_InvHeader2.rCustName.DataBindings.Add("Text", dtbl, "CustName");
                    rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "CustCompany");
                }
                else
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustName.Text = "";
                    rep_InvHeader2.rCustID.Text = "";
                    rep_InvHeader2.rCompany.Text = "";
                    rep_InvHeader2.rlCustName.Text = "";
                    rep_InvHeader2.rlCustID.Text = "";
                    rep_InvHeader2.rlCompany.Text = "";
                }

                PosDataObject.POS objPOS2 = new PosDataObject.POS();
                objPOS2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                RearrangeForTaggedItemInInvoice(dtbl1);
                if ((intHeaderStatus == 16) && (calcrent == "Y"))
                {
                    foreach (DataRow d in dtbl1.Rows)
                    {
                        d["Qty"] = -GeneralFunctions.fnDouble(d["Qty"].ToString());
                        d["TotalPrice"] = -GeneralFunctions.fnDouble(d["TotalPrice"].ToString());
                    }
                }
                if (strservice == "Rent")
                {
                    if (intHeaderStatus == 15) // issue
                    {
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentLine;
                        rep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentLine.Report.DataSource = dtbl1;
                        rep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        rep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                        rep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                        rep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                        rep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                        rep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");

                        if (Settings.ShowFeesInReceipt == "Y")
                        {
                            rep_InvRentLine.rFeesTxt.Visible = true;
                            rep_InvRentLine.rFeesTxt.DataBindings.Add("Text", dtbl1, "FeesText");
                        }
                        else
                        {
                            rep_InvRentLine.rFeesTxt.Visible = false;
                        }
                    }
                    if (intHeaderStatus == 16) // return
                    {
                        if (calcrent == "N")
                        {
                            rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                            rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentReturnLine.Report.DataSource = dtbl1;
                            rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        }
                        if (calcrent == "Y")
                        {
                            rep_InvMain.subrepLine.ReportSource = rep_InvRentLine;
                            rep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentLine.Report.DataSource = dtbl1;
                            rep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            rep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            rep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                            rep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                            rep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                            rep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                            rep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                            rep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");

                            if (Settings.ShowFeesInReceipt == "Y")
                            {
                                rep_InvRentLine.rFeesTxt.Visible = true;
                                rep_InvRentLine.rFeesTxt.DataBindings.Add("Text", dtbl1, "FeesText");
                            }
                            else
                            {
                                rep_InvRentLine.rFeesTxt.Visible = false;
                            }
                        }
                    }
                }
                else if (strservice == "Repair")
                {
                    if (intHeaderStatus == 17) // issue
                    {
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentLine;
                        rep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentLine.Report.DataSource = dtbl1;
                        rep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        rep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                        rep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                        rep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                        rep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                        rep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");

                        if (Settings.ShowFeesInReceipt == "Y")
                        {
                            rep_InvRentLine.rFeesTxt.Visible = true;
                            rep_InvRentLine.rFeesTxt.DataBindings.Add("Text", dtbl1, "FeesText");
                        }
                        else
                        {
                            rep_InvRentLine.rFeesTxt.Visible = false;
                        }
                    }
                    if (intHeaderStatus == 18) // return
                    {
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                        rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentReturnLine.Report.DataSource = dtbl1;
                        rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                    }
                }
                else
                {
                    rep_InvMain.subrepLine.ReportSource = rep_InvLine;
                    rep_InvLine.DecimalPlace = Settings.DecimalPlace;
                    rep_InvLine.Report.DataSource = dtbl1;
                    rep_InvLine.rlSKU.DataBindings.Add("Text", dtbl1, "Qty");
                    rep_InvLine.rlIem.DataBindings.Add("Text", dtbl1, "SKU");
                    rep_InvLine.rlqty.DataBindings.Add("Text", dtbl1, "Description");
                    rep_InvLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                    rep_InvLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                    rep_InvLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                    rep_InvLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                    rep_InvLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");

                    if (Settings.ShowFeesInReceipt == "Y")
                    {
                        rep_InvLine.rFeesTxt.Visible = true;
                        rep_InvLine.rFeesTxt.DataBindings.Add("Text", dtbl1, "FeesText");
                    }
                    else
                    {
                        rep_InvLine.rFeesTxt.Visible = false;
                    }
                }

                foreach (DataRow dr12 in dtbl1.Rows)
                {
                    dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                }

                //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

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
                    rep_InvMain.Dispose();
                    rep_InvHeader1.Dispose();
                    rep_InvHeader2.Dispose();
                    rep_InvLine.Dispose();
                    rep_InvSubtotal.Dispose();
                    rep_InvTax.Dispose();
                    rep_InvTendering.Dispose();
                    rep_InvGC.Dispose();
                    rep_InvMGC.Dispose();
                    rep_InvCoupon.Dispose();

                    dtbl.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    dtbl3.Dispose();
                    dtbl4.Dispose();
                    dtbl5.Dispose();
                    ccdtbl11mgc.Dispose();
                }


                if ((blCardPayment) && (Settings.IsDuplicateInvoice == "Y"))
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

                    strservice = "";
                    intHeaderStatus = 0;
                    dblRentDeposit = 0;
                    dblRentReturnDeposit = 0;
                    dblRepairAmount = 0;
                    dblRepairAdvanceAmount = 0;
                    strRepairDeliveryDate = "";

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
                        strDiscountReason = dr["DiscountReason"].ToString();

                        strservice = dr["ServiceType"].ToString();
                        intHeaderStatus = GeneralFunctions.fnInt32(dr["Status"].ToString());
                        dblRentDeposit = GeneralFunctions.fnDouble(dr["RentDeposit"].ToString());

                        dblRepairAmount = GeneralFunctions.fnDouble(dr["RepairAmount"].ToString());
                        dblRepairAdvanceAmount = GeneralFunctions.fnDouble(dr["RepairAdvanceAmount"].ToString());
                        if (dr["RepairDeliveryDate"].ToString() != "") strRepairDeliveryDate = GeneralFunctions.fnDate(dr["RepairDeliveryDate"].ToString()).ToShortDateString();
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
                    OfflineRetailV2.Report.Sales.repInvHA drep_InvHA = new OfflineRetailV2.Report.Sales.repInvHA();
                    OfflineRetailV2.Report.Sales.repInvSC drep_InvSC = new OfflineRetailV2.Report.Sales.repInvSC();
                    OfflineRetailV2.Report.Sales.repInvGC drep_InvGC = new OfflineRetailV2.Report.Sales.repInvGC();
                    OfflineRetailV2.Report.Sales.repInvMGC drep_InvMGC = new OfflineRetailV2.Report.Sales.repInvMGC();
                    OfflineRetailV2.Report.Sales.repInvCoupon drep_InvCoupon = new OfflineRetailV2.Report.Sales.repInvCoupon();
                    OfflineRetailV2.Report.Sales.repInvRentLine drep_InvRentLine = new OfflineRetailV2.Report.Sales.repInvRentLine();
                    OfflineRetailV2.Report.Sales.repInvRentSubTotal drep_InvRentSubTotal = new OfflineRetailV2.Report.Sales.repInvRentSubTotal();
                    OfflineRetailV2.Report.Sales.repInvRentReturnLine drep_InvRentReturnLine = new OfflineRetailV2.Report.Sales.repInvRentReturnLine();
                    OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal drep_InvRentReturnSubTotal = new OfflineRetailV2.Report.Sales.repInvRentReturnSubTotal();

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

                    if (strservice == "Rent")
                    {
                        if (intHeaderStatus == 15) drep_InvHeader1.rType.Text = "Rent Issued";
                        if (intHeaderStatus == 16) drep_InvHeader1.rType.Text = "Rent Item Returned";
                    }
                    if (strservice == "Repair")
                    {
                        if (intHeaderStatus == 17)
                        {
                            if (strRepairDeliveryDate != "") drep_InvHeader1.rType.Text = "Repair In" + "      Expected Delivety Date : " + strRepairDeliveryDate;
                            else drep_InvHeader1.rType.Text = "Repair In";
                        }
                        if (intHeaderStatus == 18)
                        {
                            drep_InvHeader1.rType.Text = "Repair Delivered";
                        }
                    }

                    drep_InvHeader1.rOrderNo.Text = intINV.ToString();
                    if (Settings.PrintLogoInReceipt == "Y")
                    {
                        if (!boolnulllogo) drep_InvHeader1.rPic.DataBindings.Add("Image", ddtbl, "Logo");
                    }
                    drep_InvHeader1.rOrderDate.DataBindings.Add("Text", ddtbl, "TransDate");

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
                    if (strservice == "Rent")
                    {
                        if (intHeaderStatus == 15) // issue
                        {
                            drep_InvMain.subrepLine.ReportSource = drep_InvRentLine;
                            drep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentLine.Report.DataSource = dtbl1;
                            drep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            drep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            drep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                            drep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                            drep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                            drep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                            drep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                            drep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");
                        }
                        if (intHeaderStatus == 16) // return
                        {
                            drep_InvMain.subrepLine.ReportSource = drep_InvRentReturnLine;
                            drep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentReturnLine.Report.DataSource = dtbl1;
                            drep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            drep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            drep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        }
                    }
                    else if (strservice == "Repair")
                    {
                        if (intHeaderStatus == 17) // issue
                        {
                            drep_InvMain.subrepLine.ReportSource = drep_InvRentLine;
                            drep_InvRentLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentLine.Report.DataSource = dtbl1;
                            drep_InvRentLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            drep_InvRentLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            drep_InvRentLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                            drep_InvRentLine.rDiscTxt.DataBindings.Add("Text", dtbl1, "DiscountText");
                            drep_InvRentLine.rlPrice.DataBindings.Add("Text", dtbl1, "NormalPrice");
                            drep_InvRentLine.rlDiscount.DataBindings.Add("Text", dtbl1, "Discount");
                            drep_InvRentLine.rlSurcharge.DataBindings.Add("Text", dtbl1, "Price");
                            drep_InvRentLine.rlTotal.DataBindings.Add("Text", dtbl1, "TotalPrice");
                        }
                        if (intHeaderStatus == 18) // return
                        {
                            drep_InvMain.subrepLine.ReportSource = drep_InvRentReturnLine;
                            drep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentReturnLine.Report.DataSource = dtbl1;
                            drep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                            drep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                            drep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
                        }
                    }
                    else
                    {
                        drep_InvMain.subrepLine.ReportSource = drep_InvLine;
                        drep_InvLine.DecimalPlace = Settings.DecimalPlace;
                        drep_InvLine.Report.DataSource = dtbl1;
                        drep_InvLine.rlSKU.DataBindings.Add("Text", ddtbl1, "Qty");
                        drep_InvLine.rlIem.DataBindings.Add("Text", ddtbl1, "SKU");
                        drep_InvLine.rlqty.DataBindings.Add("Text", ddtbl1, "Description");
                        drep_InvLine.rDiscTxt.DataBindings.Add("Text", ddtbl1, "DiscountText");
                        drep_InvLine.rlPrice.DataBindings.Add("Text", ddtbl1, "NormalPrice");
                        drep_InvLine.rlDiscount.DataBindings.Add("Text", ddtbl1, "Discount");
                        drep_InvLine.rlSurcharge.DataBindings.Add("Text", ddtbl1, "Price");
                        drep_InvLine.rlTotal.DataBindings.Add("Text", ddtbl1, "TotalPrice");
                    }

                    foreach (DataRow dr12 in ddtbl1.Rows)
                    {
                        dblOrderSubtotal = dblOrderSubtotal + GeneralFunctions.fnDouble(dr12["TotalPrice"].ToString()) + GeneralFunctions.fnDouble(dr12["Discount"].ToString());
                    }

                    //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

                    if (strservice == "Rent")
                    {
                        if (intHeaderStatus == 15) // issue
                        {
                            drep_InvMain.subrepSubtotal.ReportSource = drep_InvRentSubTotal;
                            drep_InvRentSubTotal.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentSubTotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                            drep_InvRentSubTotal.rDeposit.Text = dblRentDeposit.ToString();
                            drep_InvRentSubTotal.rDiscount.Text = dblDiscount.ToString();
                            drep_InvRentSubTotal.DR = strDiscountReason;
                            drep_InvRentSubTotal.rTax.Text = dblTax.ToString();
                        }

                        if (intHeaderStatus == 16) // return
                        {
                            if (dblOrderTotal != 0)
                            {
                                drep_InvMain.subrepSubtotal.ReportSource = drep_InvRentReturnSubTotal;
                                drep_InvRentReturnSubTotal.DecimalPlace = Settings.DecimalPlace;
                                drep_InvRentReturnSubTotal.rReturnDeposit.Text = dblOrderTotal.ToString();
                            }
                        }
                    }
                    else if (strservice == "Repair")
                    {
                        if (intHeaderStatus == 17) // issue
                        {
                            drep_InvMain.subrepSubtotal.ReportSource = drep_InvRentSubTotal;
                            drep_InvRentSubTotal.DecimalPlace = Settings.DecimalPlace;
                            drep_InvRentSubTotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                            drep_InvRentSubTotal.rDeposit.Text = dblRentDeposit.ToString();
                            drep_InvRentSubTotal.rDiscount.Text = dblDiscount.ToString();
                            drep_InvRentSubTotal.DR = strDiscountReason;
                            drep_InvRentSubTotal.rw1.Visible = false;
                            drep_InvRentSubTotal.rw2.Visible = false;
                            drep_InvRentSubTotal.rTax.Text = dblTax.ToString();
                        }
                    }
                    else
                    {
                        drep_InvMain.subrepSubtotal.ReportSource = drep_InvSubtotal;
                        drep_InvSubtotal.DecimalPlace = Settings.DecimalPlace;
                        drep_InvSubtotal.rSubTotal.Text = dblOrderSubtotal.ToString();
                        drep_InvSubtotal.rDiscount.Text = dblDiscount.ToString();
                        drep_InvSubtotal.DR = strDiscountReason;
                        drep_InvSubtotal.rTax.Text = dblTax.ToString();
                    }


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
                    drep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                    drep_InvTendering.rTenderName.DataBindings.Add("Text", ddtbl3, "DisplayAs");
                    drep_InvTendering.rTenderAmt.DataBindings.Add("Text", ddtbl3, "Amount");

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

                    if (strservice == "Repair")
                    {
                        if (intHeaderStatus == 17)
                        {
                            drep_InvTendering.rlbAdvance.Text = "Advance Amount";
                            drep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                        }
                        if (intHeaderStatus == 18)
                        {
                            drep_InvTendering.rlbAdvance.Text = "";
                            drep_InvTendering.rAdvance.Text = "";
                        }
                    }
                    else
                    {
                        drep_InvTendering.rlbAdvance.Text = "";
                        drep_InvTendering.rAdvance.Text = "";
                    }

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
                        //Todo: dfrm_PreviewControl.pnlCtrl.PrintingSystem = drep_InvMain.PrintingSystem;
                        //dfrm_PreviewControl.pnlCtrl.PrintingSystem.PreviewFormEx.Hide();
                        drep_InvMain.CreateDocument();
                        drep_InvMain.PrintingSystem.ShowMarginsWarning = false;
                        dfrm_PreviewControl.ShowDialog();
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
            }
        }

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

        private bool IsCardPayment(int intTrnNo)
        {
            PosDataObject.POS objposTT = new PosDataObject.POS();
            objposTT.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objposTT.IsCardPayment(intTrnNo);
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

        private void UpdateReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            objPOS.LoginUserID = SystemVariables.CurrentUserID;
            objPOS.FunctionButtonAccess = blFunctionBtnAccess;
            objPOS.ChangedByAdmin = intSuperUserID;
            string ret = objPOS.UpdateReceiptCount(invno, 0);
        }

        private int GetReceiptCnt(int invno)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.GetReceiptCount(invno);
        }

        private void cmbInvFilter_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (blFetch) FetchHeaderData();
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

        private void cmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            PopulateCustomer();
            if (blFetch) FetchHeaderData();
        }

        private async void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnVoid.IsEnabled = false;

                int intCardTranID = 0;
                if (gridView2.FocusedRowHandle < 0) return;

                /*if (GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID)) != 0)
                {
                    if (!CheckActiveCustomer(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID))))
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
                                            new MessageBoxWindow().Show(Properties.Resources."Error occured during transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                            GeneralFunctions.SetTransactionLog("Catch - Error inserting Card Trans", ex.Message);
                                            Cursor = Cursors.Arrow;
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
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
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
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
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
                                                    new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
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
                                            new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(Properties.Resources."Error occured during Card Transaction","frmPOSRentalRecallDlg_msg_Erroroccuredduringtransaction"), "Credit Card Transaction", MessageBoxButton.OK, MessageBoxImage.Information);
                                        flag = false;
                                        break;
                                    }
                                }
                                */

                                /*
                                if (pmntgwy == 7) // POSLink
                                {
                                    POSLinkLogPath = POSLinkLogFilePath();
                                    POSLink_ResultCode = "";
                                    POSLink_ResultTxt = "";
                                    POSLink_RefNum = "";
                                    POSLink_AuthCode = "";
                                    POSLink_CardType = "";
                                    POSLink_BogusAccountNum = "";
                                    POSLink_CashBack = 0;
                                    POSLink_RequestedAmt = 0;
                                    POSLink_ApprovedAmt = 0;
                                    POSLink_RemainingBalance = 0;
                                    POSLink_ExtraBalance = 0;

                                    PosDataObject.POS objPOS = new PosDataObject.POS();
                                    objPOS.Connection = SystemVariables.Conn;
                                    int refinv = objPOS.FetchMaxInvoiceNo();

                                    POSLink.PosLink pg = new POSLink.PosLink();

                                    pg.CommSetting = GeneralFunctions.GetPOSLinkCommSetup();

                                    POSLink.LogManagement plog = new POSLink.LogManagement();
                                    plog.LogLevel = 1;
                                    plog.LogFilePath = Path.GetDirectoryName(POSLinkLogPath);
                                    pg.LogManageMent = plog;

                                    POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

                                    if (val1 == "Credit Card")
                                    {
                                        paymentRequest.TenderType = paymentRequest.ParseTenderType("CREDIT");
                                        if (val20 == "Sale")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID SALE");
                                        }
                                        if (val20 == "Return")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID RETURN");
                                        }
                                    }

                                    if (val1 == "Debit Card")
                                    {
                                        paymentRequest.TenderType = paymentRequest.ParseTenderType("DEBIT");

                                        if (val20 == "Sale")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID SALE");
                                        }
                                        if (val20 == "Return")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID RETURN");
                                        }


                                    }

                                    if (val1 == "POSLink Gift Card")
                                    {
                                        paymentRequest.TenderType = paymentRequest.ParseTenderType("GIFT");

                                        if (val20 == "Sale")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID");
                                        }
                                        if (val20 == "Issue")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID");
                                        }
                                        if (val20 == "Reload")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID");
                                        }
                                    }

                                    if (val1 == "EBT Cash")
                                    {
                                        paymentRequest.TenderType = paymentRequest.ParseTenderType("EBT_CASHBENEFIT");

                                        if (val20 == "Sale")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID SALE");
                                        }
                                        if (val20 == "Return")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID RETURN");
                                        }


                                    }

                                    if (val1 == "Food Stamps")
                                    {
                                        paymentRequest.TenderType = paymentRequest.ParseTenderType("EBT_FOODSTAMP");

                                        if (val20 == "Sale")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID SALE");
                                        }
                                        if (val20 == "Return")
                                        {
                                            paymentRequest.TransType = paymentRequest.ParseTransType("VOID RETURN");
                                        }

                                    }



                                    paymentRequest.Amount = "0";

                                    paymentRequest.OrigRefNum = val3;
                                    paymentRequest.InvNum = refinv.ToString();
                                    paymentRequest.UserID = "";
                                    paymentRequest.PassWord = "";
                                    paymentRequest.ClerkID = "";
                                    paymentRequest.ServerID = "";
                                    paymentRequest.ECRRefNum = "1";

                                    pg.PaymentRequest = paymentRequest;

                                    POSLink.ProcessTransResult result = new POSLink.ProcessTransResult();

                                    result = pg.ProcessTrans();

                                    if (result.Code == POSLink.ProcessTransResultCode.OK)
                                    {
                                        POSLink.PaymentResponse paymentResponse = pg.PaymentResponse;
                                        if (paymentResponse != null && paymentResponse.ResultCode != null)
                                        {
                                            POSLink_ResultCode = paymentResponse.ResultCode;
                                            POSLink_ResultTxt = paymentResponse.ResultTxt;
                                            POSLink_RefNum = paymentResponse.RefNum;

                                            POSLink_RequestedAmt = GeneralFunctions.fnDouble(paymentResponse.RequestedAmount) / 100;
                                            POSLink_ApprovedAmt = GeneralFunctions.fnDouble(paymentResponse.ApprovedAmount) / 100;
                                            POSLink_RemainingBalance = GeneralFunctions.fnDouble(paymentResponse.RemainingBalance) / 100;
                                            POSLink_ExtraBalance = GeneralFunctions.fnDouble(paymentResponse.ExtraBalance) / 100;

                                            POSLink_BogusAccountNum = paymentResponse.BogusAccountNum;
                                            POSLink_CardType = paymentResponse.CardType;


                                            POSLink_AuthCode = paymentResponse.AuthCode;

                                            if (POSLink_ResultCode == "000000") // Approved
                                            {
                                                MercuryResponseOrigin = "";
                                                MercuryResponseReturnCode = "";
                                                MercuryTextResponse = "";
                                                MercuryTranCode = "";
                                                MercuryProcessData = "";
                                                CardType = val1;
                                                ApprovedAmt = POSLink_ApprovedAmt.ToString();
                                                MercuryProcessData = "";
                                                MercuryPurchaseAmount = POSLink_RequestedAmt;
                                                AuthCode = POSLink_AuthCode;
                                                TranID = POSLink_RefNum;
                                                RefNo = POSLink_RefNum;
                                                AcqRef = "";
                                                Token = "";
                                                CardNum = POSLink_BogusAccountNum;
                                                CardLogo = POSLink_CardType;
                                                strMercuryMerchantID = "";


                                            }
                                            else
                                            {
                                                new MessageBoxWindow().Show(POSLink_ResultTxt, "Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                                Cursor = Cursors.Arrow;
                                                flag = false;
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            Cursor = Cursors.Arrow;
                                            flag = false;
                                            break;
                                        }

                                    }
                                    else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
                                    {
                                        new MessageBoxWindow().Show("Action Timeout.", "Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                        Cursor = Cursors.Arrow;
                                        flag = false;
                                        break;
                                    }
                                    else
                                    {
                                        new MessageBoxWindow().Show(result.Msg, "Card Payment", MessageBoxButton.OK, MessageBoxImage.Information);
                                        Cursor = Cursors.Arrow;
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
                                    objcard.RefCardBalAmount = 0;
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
                                DocMessage.MsgInformation(Properties.Resources.Invoice__ + " " + INV.ToString() + " " + Properties.Resources.voided_successfully);
                                FetchHeaderData();
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

        private bool IsStoreCreditTendering(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsStoreCreditTenderingVoid(InvNo);
        }

        private bool IsHouseAccountTendering(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsHouseAccountTenderingVoid(InvNo);
        }

        private bool IsGiftCert(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsGiftCertVoid(InvNo);
        }

        private bool IsHouseAccount(int InvNo)
        {
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objp.IfExistsHouseAccountPaymentVoid(InvNo);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbElapsed.Text = stopWatch.GetElapsedTimeString();
        }
        StopWatch stopWatch = new StopWatch();
        DispatcherTimer timer1 = new DispatcherTimer()
        {
            Interval = new TimeSpan(0, 0, 1)
        };
        private async void gridView1_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                if (gridView1.FocusedRowHandle >= 0)
                {
                    if ((await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colRentStatus) == "15") &&
                    (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInfo4) == ""))
                    {
                        timer1.Start();
                        stopWatch.Start(GeneralFunctions.fnDate(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, coldt)),
                            await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle, grdDetail, colrenttype));
                        lbElapsed.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        timer1.Stop();
                        stopWatch.Stop();
                        lbElapsed.Visibility = Visibility.Collapsed;
                    }
                }
            }
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

        private double FetchCashBack(int TrnNo, double Amt)
        {
            PosDataObject.POS objpos3 = new PosDataObject.POS();
            objpos3.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos3.GetCashBackAmountFromCardTransaction1(TrnNo, Amt);
        }

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

        void _Scanner_DataEvent(object sender, DataEventArgs e)
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

                SCAN = str;
                txtInv.Text = SCAN;
                try
                {
                    m_posScanner.DeviceEnabled = false;
                    FetchHeaderData_Specific();
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

        private void txtInv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtInv.Text.Trim() == "") e.Handled = false;
                FetchHeaderData_Specific();
                e.Handled = true;
            }
        }

        private DataTable FetchInvFees(int pInvNo)
        {
            PosDataObject.POS objpos1 = new PosDataObject.POS();
            objpos1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos1.FetchFeesInInvoice(pInvNo);
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

        #endregion

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

        private async void CmbP_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch)
            {
                gridView2.FocusedRowHandle = -1;
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void CmbC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch)
            {
                gridView2.FocusedRowHandle = -1;
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void CmbInvFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (DevExpress.Xpf.Grid.LookUp.LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (DevExpress.Xpf.Grid.TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
