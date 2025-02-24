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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_FeesBrwUC.xaml
    /// </summary>
    public partial class frm_FeesBrwUC : UserControl
    {
        public frm_FeesBrwUC()
        {
            InitializeComponent();
        }

        private FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        bool bOpenKeyBrd = false;




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

        public void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private int intSelectedRowHandle;

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.RedirectToSubmenu("Administrator");
        }

        public async Task SetCurrentRow(int RecordID)
        {
            int intRecID = 0;
            int intColCtr = 0;
            for (intColCtr = 0; intColCtr < (grdD.ItemsSource as DataTable).Rows.Count; intColCtr++)
            {
                intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intColCtr, grdD, colID)));
                if (RecordID == intRecID) break;
            }
            if (intColCtr >= 0) gridView1.FocusedRowHandle = intColCtr;
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdD, colID)));
        }

        public async Task<int> ReturnRowID()
        {
            int intRowID = -1;
            int intRecID = -1;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return intRecID;
            if (gridView1.FocusedRowHandle < 0) return intRecID;
            intRowID = gridView1.FocusedRowHandle;
            intRecID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdD, colID)));
            return intRecID;
        }

        public void SetRowIndex()
        {
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle < 0) return;
            intSelectedRowHandle = gridView1.FocusedRowHandle;
        }

        public void FetchData()
        {
            DataTable dbtbl = new DataTable();
            PosDataObject.Fees objClass = new PosDataObject.Fees();
            objClass.Connection = SystemVariables.Conn;
            dbtbl = objClass.FetchData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }



            grdD.ItemsSource = dtblTemp;

            dtblTemp.Dispose();
            dbtbl.Dispose();


            
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intNewRecID = 0;
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_FeesDlg frm_DiscountDlg = new frm_FeesDlg();
            try
            {
                frm_DiscountDlg.ID = 0;
                frm_DiscountDlg.ViewMode = false;
                //frm_DiscountDlg.InitialiseScreen();
                frm_DiscountDlg.BrowseForm = this;
                frm_DiscountDlg.ShowDialog();
                intNewRecID = frm_DiscountDlg.NewID;
            }
            finally
            {
                frm_DiscountDlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }
            if (intNewRecID > 0) await SetCurrentRow(intNewRecID);
        }


        private async Task EditProcess()
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }

            int intNewRecID = 0;
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }
            (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
            frm_FeesDlg fdlg = new frm_FeesDlg();
            try
            {
                fdlg.ID = await ReturnRowID();
                if (fdlg.ID > 0)
                {
                    fdlg.ViewMode = false;
                    //fdlg.InitialiseScreen();
                    //fdlg.ShowHeader();
                    fdlg.BrowseForm = this;
                    fdlg.ShowDialog();
                    intNewRecID = fdlg.ID;
                }
            }
            finally
            {
                fdlg.Close();
                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;
            }

            await SetCurrentRow(intNewRecID);
        }


        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((!SecurityPermission.AcssSetup) && (SystemVariables.CurrentUserID > 0))
            {
                DocMessage.MsgPermission();
                return;
            }
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0)
            {
                return;
            }

            int intRecdID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, grdD, colID));
            if (intRecdID > 0)
            {
                if (DocMessage.MsgDelete(Properties.Resources.Fees_and_Charges))
                {
                    if (GeneralFunctions.CheckFeesInOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_fees_as_it_is_already_used_in + " " + Properties.Resources.orders);
                        return;
                    }
                    if (GeneralFunctions.CheckFeesInWorkOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_fees_as_it_is_already_used_in + " " + Properties.Resources.workorders);
                        return;
                    }

                    if (GeneralFunctions.CheckFeesInSuspendOrder(intRecdID))
                    {
                        DocMessage.MsgInformation(Properties.Resources.Can_not_delete_this_fees_as_it_is_already_used_in + " " + Properties.Resources.suspend_orders);
                        return;
                    }

                    PosDataObject.Fees objCustomer = new PosDataObject.Fees();
                    objCustomer.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    int intrError = objCustomer.DeleteFees(intRecdID);
                    if (intrError != 0)
                    {

                    }
                    FetchData();
                }
            }
        }

        private async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditProcess();
        }

        private async void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((grdD.ItemsSource as DataTable).Rows.Count == 0) return;
            if (gridView1.FocusedRowHandle != -1)
            {
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store  Details in Settings before printing the report");
                    return;
                }
                int HID = await ReturnRowID();

                PosDataObject.Fees fq1 = new PosDataObject.Fees();
                fq1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                PosDataObject.Fees fq2 = new PosDataObject.Fees();
                fq2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                PosDataObject.Fees fq3 = new PosDataObject.Fees();
                fq3.Connection = new SqlConnection(SystemVariables.ConnectionString);
                PosDataObject.Fees fq4 = new PosDataObject.Fees();
                fq4.Connection = new SqlConnection(SystemVariables.ConnectionString);

                DataTable dtbl1 = new DataTable();
                DataTable dtbl2 = new DataTable();
                DataTable dtbl3 = new DataTable();
                DataTable dtbl4 = new DataTable();
                DataTable dtbl5 = new DataTable();
                dtbl1 = fq1.FetchRestrictedItem(HID, "F");
                dtbl2 = fq2.FetchRestrictedItem(HID, "D");
                dtbl3 = fq3.FetchRestrictedItem(HID, "G");
                dtbl4 = fq4.FetchRestrictedItem(HID, "I");

                string fmy = "";
                foreach (DataRow dr in dtbl1.Rows)
                {
                    fmy = fmy + dr["Item"].ToString() + "\n";
                }
                string dept = "";
                foreach (DataRow dr in dtbl2.Rows)
                {
                    dept = dept + dr["Item"].ToString() + "\n";
                }
                string cat = "";
                foreach (DataRow dr in dtbl3.Rows)
                {
                    cat = cat + dr["Item"].ToString() + "\n";
                }
                string itm = "";
                foreach (DataRow dr in dtbl4.Rows)
                {
                    itm = itm + dr["Item"].ToString() + "\n";
                }


                PosDataObject.Product objTaxProduct = new PosDataObject.Product();
                objTaxProduct.Connection = SystemVariables.Conn;
                objTaxProduct.DecimalPlace = Settings.DecimalPlace;
                dtbl5 = objTaxProduct.ShowTaxes(HID, "Fees");
                string tx = "";
                foreach (DataRow dr in dtbl5.Rows)
                {
                    tx = tx + dr["TaxName"].ToString() + "\n";
                }
                PosDataObject.Fees fq = new PosDataObject.Fees();
                fq.Connection = SystemVariables.Conn;
                DataTable dtbl = new DataTable();
                dtbl = fq.ShowRecord(HID);

                string valN = "";
                string valD = "";
                string valV = "";
                string valActive = "";
                string valAuto = "";
                string valDiscount = "";
                string valFS = "";
                string valPrice = "";
                string valQty = "";
                string valTax = "";
                string valRestrict = "";

                foreach (DataRow dr in dtbl.Rows)
                {
                    valN = dr["FeesName"].ToString();
                    valD = dr["FeesDescription"].ToString();
                    if (dr["FeesType"].ToString() == "A")
                    {
                        valV = "Amt. " + GeneralFunctions.fnDouble(dr["FeesAmount"].ToString()) + " applicable";
                    }
                    if (dr["FeesType"].ToString() == "P")
                    {
                        valV = GeneralFunctions.fnDouble(dr["FeesPercentage"].ToString()) + "% applicable";
                    }

                    valActive = dr["FeesStatus"].ToString();
                    valTax = dr["ChkTax"].ToString();

                    valRestrict = dr["ChkRestrictedItems"].ToString();
                    valDiscount = dr["ChkDiscount"].ToString();
                    valFS = dr["ChkFoodStamp"].ToString();
                    valPrice = dr["ChkInclude"].ToString();
                    valAuto = dr["ChkAutoApply"].ToString();
                    valQty = dr["ChkItemQty"].ToString();
                }

                dtbl.Dispose();

                DataTable dtblReportData = new DataTable();
                dtblReportData.Columns.Add("FeesName", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesDescription", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesValue", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesActive", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesAuto", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesDiscount", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesFS", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesPrice", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesQty", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesTax", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesRestricted", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesFamily", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesDepartment", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesCategory", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesItem", System.Type.GetType("System.String"));
                dtblReportData.Columns.Add("FeesTX", System.Type.GetType("System.String"));

                if (valTax == "N")
                {
                    tx = "";
                }

                if (valRestrict == "N")
                {
                    fmy = "";
                    dept = "";
                    cat = "";
                    itm = "";
                }

                dtblReportData.Rows.Add(new object[] { valN, valD, valV, valActive, valAuto, valDiscount, valFS, valPrice, valQty, valTax, valRestrict,
                fmy,dept,cat,itm,tx});

                OfflineRetailV2.Report.Misc.repFees rep = new OfflineRetailV2.Report.Misc.repFees();
                GeneralFunctions.MakeReportWatermark(rep);
                rep.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep.rReportHeader.Text = Settings.ReportHeader_Address;

                rep.Report.DataSource = dtblReportData;






                rep.rDiscountName.DataBindings.Add("Text", dtblReportData, "FeesName");
                rep.rDesc.DataBindings.Add("Text", dtblReportData, "FeesDescription");
                rep.rValueOff.DataBindings.Add("Text", dtblReportData, "FeesValue");
                rep.rActive.DataBindings.Add("Text", dtblReportData, "FeesActive");
                rep.rAuto1.DataBindings.Add("Text", dtblReportData, "FeesAuto");
                rep.rDis.DataBindings.Add("Text", dtblReportData, "FeesDiscount");
                rep.rFS1.DataBindings.Add("Text", dtblReportData, "FeesFS");
                rep.rPrice.DataBindings.Add("Text", dtblReportData, "FeesPrice");

                rep.rQty1.DataBindings.Add("Text", dtblReportData, "FeesQty");
                rep.rTax.DataBindings.Add("Text", dtblReportData, "FeesTax");
                rep.rTx.DataBindings.Add("Text", dtblReportData, "FeesTX");
                rep.rRestrict.DataBindings.Add("Text", dtblReportData, "FeesRestricted");
                rep.rfamydet.DataBindings.Add("Text", dtblReportData, "FeesFamily");
                rep.rdeptdet.DataBindings.Add("Text", dtblReportData, "FeesDepartment");
                rep.rcatdet.DataBindings.Add("Text", dtblReportData, "FeesCategory");
                rep.ritemdet.DataBindings.Add("Text", dtblReportData, "FeesItem");


                if (valRestrict == "N")
                {
                    rep.rRCat.Visible = rep.rRItem.Visible = rep.rRFamily.Visible = rep.rRDept.Visible = false;
                }


                (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Visible;
                try
                {
                    if (Settings.ReportPrinterName != "") rep.PrinterName = Settings.ReportPrinterName;
                    rep.CreateDocument();
                    rep.PrintingSystem.ShowMarginsWarning = false;

                    //rep.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep;
                    window.ShowDialog();
                }
                finally
                {
                    rep.Dispose();
                    dtblReportData.Dispose();
                    (Window.GetWindow(this) as MainWindow).blurGrid.Visibility = Visibility.Collapsed;

                }
            }
        }


        private void TxtSearchGrdData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.InfoText == "Search Fees")
            {
                txtSearchGrdData.Text = "";
            }

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            if (bOpenKeyBrd) return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }

        private void TxtSearchGrdData_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchGrdData.Text == "")
            {
                txtSearchGrdData.InfoText = "Search Fees";
            }

            bOpenKeyBrd = false;
            CloseKeyboards();
        }

        private void TxtSearchGrdData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchGrdData.Text == "Search Fees") return;
            if (txtSearchGrdData.Text == "")
            {
                grdD.FilterString = "";
                return;
            }
            string filterValue = txtSearchGrdData.Text;
            if (!String.IsNullOrEmpty(filterValue))
            {
                grdD.FilterString = "([FeesName] LIKE '%" + filterValue + "%')";
            }
        }

        private void TxtSearchGrdData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutFullKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
            {
                fkybrd = new FullKeyboard();

                var location = (sender as System.Windows.Controls.TextBox).PointToScreen(new Point(0, 0));
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
                fkybrd.IsWindow = false;
                fkybrd.calledusercontrol = this;
                fkybrd.UCEdit = sender as System.Windows.Controls.TextBox;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                bOpenKeyBrd = true;
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }
    }
}
