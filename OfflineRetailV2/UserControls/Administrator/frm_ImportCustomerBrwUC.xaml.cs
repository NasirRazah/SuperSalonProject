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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_ImportCustomerBrwUC.xaml
    /// </summary>
    public partial class frm_ImportCustomerBrwUC : UserControl
    {
        public frm_ImportCustomerBrwUC()
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

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdCustomer.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdCustomer, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdCustomer.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdCustomer, colCustomerID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdCustomer.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdCustomer, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdCustomer.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void PopulateStore()
        {

            PosDataObject.Central objcout = new PosDataObject.Central();
            objcout.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objcout.FetchOtherStoreForCustomers(Settings.StoreCode);
            cmbFilter.ItemsSource = dtbl;
            int rc = 0;
            string selectstr = "";
            foreach (DataRow dr in dtbl.Rows)
            {
                rc++;
                if (rc == 1) selectstr = dr["issuestore"].ToString();
            }

            if (selectstr != "") cmbFilter.EditValue = selectstr;

        }

        public void FetchData()
        {
            if (cmbFilter.EditValue != null)
            {
                PosDataObject.Central objcout = new PosDataObject.Central();
                objcout.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = objcout.FetchCustomers(Settings.StoreCode, cmbFilter.EditValue.ToString());


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



                grdCustomer.ItemsSource = dtblTemp;

                dtblTemp.Dispose();
                dtbl.Dispose();

                
            }

        }

        private async Task EditProcess()
        {
            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdCustomer.ItemsSource as DataTable).Rows.Count == 0) return;

            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            POSSection.frm_CustomerDlg frm_CustomerDlg = new POSSection.frm_CustomerDlg();
            try
            {
                frm_CustomerDlg.ID = await ReturnRowID();
                if (frm_CustomerDlg.ID > 0)
                {
                    frm_CustomerDlg.Duplicate = false;
                    frm_CustomerDlg.AddFromPOS = false;
                    frm_CustomerDlg.bPOS = blPOS;
                    frm_CustomerDlg.OtherStoreRecord = true;
                    //frm_CustomerDlg.BrowseFormUC = this;
                    frm_CustomerDlg.ShowDialog();
                    intNewRecID = frm_CustomerDlg.ID;
                }
            }
            finally
            {
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            await SetCurrentRow(intNewRecID);
        }

        private DataTable GetDeletedocsNphotosID(int refID)
        {
            DataTable db = new DataTable();
            db.Columns.Add("ID", System.Type.GetType("System.String"));
            string fileext = "";
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objNotes.FetchNoteData("", "Customer", refID, SystemVariables.DateFormat);
            foreach (DataRow dr in dbtbl.Rows)
            {
                if (dr["Attach"].ToString() == "Y") db.Rows.Add(new object[] { dr["ID"].ToString() });
            }
            return db;
        }

        private void DeletedocsNphotos(DataTable reftable)
        {

            string sdoc = "";
            string sscan = "";

            string csStorePath = "";
            csStorePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Customer\\Notes\\";
            else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Customer\\Notes\\";



            DirectoryInfo di = new DirectoryInfo(csStorePath);
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi)
            {
                foreach (DataRow dr in reftable.Rows)
                {
                    if ((f.Name.Contains(dr["ID"].ToString() + "_d")) || (f.Name.Contains(dr["ID"].ToString() + "_s")))
                    {
                        f.Delete();
                    }
                }
            }

        }

        public void GetCustomerGridDimensions()
        {
            try
            {

                //gridView6.BeginUpdate();

                colCustomerID.VisibleIndex = -1;
                colFirstName.VisibleIndex = -1;
                colLastName.VisibleIndex = -1;
                colCompany.VisibleIndex = -1;
                colWorkPhone.VisibleIndex = -1;
                colAddress1.VisibleIndex = -1;
                colCity.VisibleIndex = -1;

                colCustomerID.Width = 100;
                colFirstName.Width = 100;
                colLastName.Width = 100;
                colCompany.Width = 100;
                colWorkPhone.Width = 100;
                colAddress1.Width = 150;
                colCity.Width = 80;

                string[] itemvisible = Settings.GridCustomerParam1.Split('*');
                int[] visibleindex = new int[itemvisible.Length];
                for (int x = 0; x < itemvisible.Length; x++)
                {
                    visibleindex[x] = GeneralFunctions.fnInt32(itemvisible[x].ToString());
                }
                Array.Sort(visibleindex);
                string[] itemwidth = Settings.GridCustomerParam2.Split('*');
                string[] ItemGrid = Settings.CustGrid.Split('*'); ;
                int i = -1;

                gridView1.BeginInit();

                foreach (int val in visibleindex)
                {
                    if (val < 0) continue;
                    int findindex = -1;
                    foreach (string sval in itemvisible)
                    {
                        findindex++;
                        if (sval == val.ToString())
                        {
                            break;
                        }

                    }
                    if (findindex == 0) colCustomerID.VisibleIndex = val;
                    if (findindex == 1) colFirstName.VisibleIndex = val;
                    if (findindex == 2) colLastName.VisibleIndex = val;
                    if (findindex == 3) colCompany.VisibleIndex = val;
                    if (findindex == 4) colWorkPhone.VisibleIndex = val;
                    if (findindex == 5) colAddress1.VisibleIndex = val;
                    if (findindex == 6) colCity.VisibleIndex = val;
                }



                foreach (string s in ItemGrid)
                {
                    i++;
                    if (s == "CI")
                    {
                        //colCustomerID.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colCustomerID.VisibleIndex != -1) colCustomerID.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "FN")
                    {
                        //colFirstName.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colFirstName.VisibleIndex != -1) colFirstName.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "LN")
                    {
                        //colLastName.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colLastName.VisibleIndex != -1) colLastName.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "CO")
                    {
                        //colCompany.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colCompany.VisibleIndex != -1) colCompany.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "WP")
                    {
                        //colWorkPhone.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colWorkPhone.VisibleIndex != -1) colWorkPhone.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "AR")
                    {
                        //colAddress1.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colAddress1.VisibleIndex != -1) colAddress1.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                    if (s == "CI")
                    {
                        //colCity.VisibleIndex = GeneralFunctions.fnInt32(itemvisible[i]);
                        if (colCity.VisibleIndex != -1) colCity.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwidth[i]) * (pnlMain.ActualWidth - 5) / 100));
                    }
                }
            }
            finally
            {
                gridView1.EndInit();
            }
        }

        private async void barButtonItem1_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void barButtonItem2_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssCustomerPrint) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Enter_Store_Details_in_Settings_before_printing_the_report);
                return;
            }
            if ((grdCustomer.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                //frmPreviewControl frm_PreviewControl = new frmPreviewControl();
                DataTable dtbl = new DataTable();
                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();

                OfflineRetailV2.Report.Customer.repCustomerSnapDetail rep_CustomerSnapDetail = new OfflineRetailV2.Report.Customer.repCustomerSnapDetail();
                OfflineRetailV2.Report.Customer.repCustomerSnap rep_CustomerSnap = new OfflineRetailV2.Report.Customer.repCustomerSnap();
                OfflineRetailV2.Report.Customer.repCustomerGroup rep_CustomerGroup = new OfflineRetailV2.Report.Customer.repCustomerGroup();
                OfflineRetailV2.Report.Customer.repCustomerClass rep_CustomerClass = new OfflineRetailV2.Report.Customer.repCustomerClass();

                PosDataObject.Customer objCust = new PosDataObject.Customer();
                objCust.Connection = new SqlConnection(SystemVariables.ConnectionString);
                objCust.DecimalPlace = Settings.DecimalPlace;
                dtbl = objCust.FetchRecordForReport(await ReturnRowID());
                rep_CustomerSnapDetail.subrepGeneral.ReportSource = rep_CustomerSnap;

                rep_CustomerSnap.Report.DataSource = dtbl;

                GeneralFunctions.MakeReportWatermark(rep_CustomerSnapDetail);
                rep_CustomerSnap.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_CustomerSnap.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_CustomerSnap.DecimalPlace = Settings.DecimalPlace;

                rep_CustomerSnap.rPic.DataBindings.Add("Text", dtbl, "ID");

                //frm_PreviewControl.Report = rep_CustomerSnap;
                rep_CustomerSnap.rID.DataBindings.Add("Text", dtbl, "CustomerID");
                rep_CustomerSnap.rCompany.DataBindings.Add("Text", dtbl, "Company");
                rep_CustomerSnap.rName.DataBindings.Add("Text", dtbl, "Name");
                rep_CustomerSnap.rSalutation.DataBindings.Add("Text", dtbl, "Salutation");
                rep_CustomerSnap.rSpouse.DataBindings.Add("Text", dtbl, "Spouse");
                rep_CustomerSnap.rDOB.DataBindings.Add("Text", dtbl, "DateOfBirth");
                rep_CustomerSnap.rDOM.DataBindings.Add("Text", dtbl, "DateOfMarriage");
                rep_CustomerSnap.rBAdd1.DataBindings.Add("Text", dtbl, "Address1");
                rep_CustomerSnap.rBadd2.DataBindings.Add("Text", dtbl, "Address2");
                rep_CustomerSnap.rBcity.DataBindings.Add("Text", dtbl, "City");
                rep_CustomerSnap.rBzip.DataBindings.Add("Text", dtbl, "Zip");
                rep_CustomerSnap.rBstate.DataBindings.Add("Text", dtbl, "State");
                rep_CustomerSnap.rBcountry.DataBindings.Add("Text", dtbl, "Country");
                rep_CustomerSnap.rSAdd1.DataBindings.Add("Text", dtbl, "ShipAddress1");
                rep_CustomerSnap.rSadd2.DataBindings.Add("Text", dtbl, "ShipAddress2");
                rep_CustomerSnap.rScity.DataBindings.Add("Text", dtbl, "ShipCity");
                rep_CustomerSnap.rSzip.DataBindings.Add("Text", dtbl, "ShipZip");
                rep_CustomerSnap.rSstate.DataBindings.Add("Text", dtbl, "ShipState");
                rep_CustomerSnap.rScountry.DataBindings.Add("Text", dtbl, "ShipCountry");
                rep_CustomerSnap.rWphone.DataBindings.Add("Text", dtbl, "WorkPhone");
                rep_CustomerSnap.rHphone.DataBindings.Add("Text", dtbl, "HomePhone");
                rep_CustomerSnap.rMphone.DataBindings.Add("Text", dtbl, "MobilePhone");
                rep_CustomerSnap.rFax.DataBindings.Add("Text", dtbl, "Fax");
                rep_CustomerSnap.rEMail.DataBindings.Add("Text", dtbl, "EMail");

                rep_CustomerSnap.rTaxExempt.DataBindings.Add("Text", dtbl, "TaxExempt");
                rep_CustomerSnap.rTaxID.DataBindings.Add("Text", dtbl, "TaxID");
                rep_CustomerSnap.rPriceLebel.DataBindings.Add("Text", dtbl, "DiscountLevel");

                rep_CustomerSnap.rCL.DataBindings.Add("Text", dtbl, "ARCreditLimit");
                rep_CustomerSnap.rOB.DataBindings.Add("Text", dtbl, "ClosingBalance");
                rep_CustomerSnap.rCB.DataBindings.Add("Text", dtbl, "ClosingBalance");
                rep_CustomerSnap.rEP.DataBindings.Add("Text", dtbl, "Points");
                rep_CustomerSnap.rStoreCard.DataBindings.Add("Text", dtbl, "StoreCreditCard");



                PosDataObject.Customer objCustomer1 = new PosDataObject.Customer();
                objCustomer1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl1 = objCustomer1.ShowGroup(await ReturnRowID());

                if (dtbl1.Rows.Count > 0)
                {
                    rep_CustomerSnapDetail.subrepGroup.ReportSource = rep_CustomerGroup;
                    rep_CustomerGroup.Report.DataSource = dtbl1;
                    rep_CustomerGroup.rgrpid.DataBindings.Add("Text", dtbl1, "GroupIDName");
                    rep_CustomerGroup.rgrpname.DataBindings.Add("Text", dtbl1, "Description");
                }

                PosDataObject.Customer objCustomer2 = new PosDataObject.Customer();
                objCustomer2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                dtbl2 = objCustomer2.ShowClass(await ReturnRowID());

                if (dtbl2.Rows.Count > 0)
                {
                    rep_CustomerSnapDetail.subrepClass.ReportSource = rep_CustomerClass;
                    rep_CustomerClass.Report.DataSource = dtbl2;
                    rep_CustomerClass.rclsid.DataBindings.Add("Text", dtbl2, "ClassIDName");
                    rep_CustomerClass.rclsname.DataBindings.Add("Text", dtbl2, "Description");
                }

                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_CustomerSnapDetail.PrinterName = Settings.ReportPrinterName;
                    rep_CustomerSnapDetail.CreateDocument();
                    rep_CustomerSnapDetail.PrintingSystem.ShowMarginsWarning = false;
                    rep_CustomerSnapDetail.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_CustomerSnapDetail.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_CustomerSnapDetail;
                    window.ShowDialog();

                }
                finally
                {
                    rep_CustomerSnap.Dispose();
                    rep_CustomerGroup.Dispose();
                    rep_CustomerClass.Dispose();
                    rep_CustomerSnapDetail.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();
                    dtbl.Dispose();
                    dtbl1.Dispose();
                    dtbl2.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        

        private void cmbFilter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbFilter.EditValue != null)
            {
                FetchData();
            }
        }

        private async void grdCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private void GrdCustomer_CustomGroupDisplayText(object sender, CustomGroupDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colStore")
            {
                e.DisplayText = "Store" + " : " + e.Value.ToString();
            }
        }

        private async void BtnView_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private void CmbFilter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
