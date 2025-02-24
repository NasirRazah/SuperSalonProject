using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.XtraEditors.Repository;
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
using DevExpress.Xpf.Printing;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSRepairRecallDlg.xaml
    /// </summary>
    public partial class frm_POSRepairRecallDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;


        public frm_POSRepairRecallDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSRepairRecallDlg_Loaded;
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
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInPOS == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
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


        private async void Frm_POSRepairRecallDlg_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();

            fkybrd = new FullKeyboard();

            Title.Text =Properties.Resources.Recall_Repair ;
            

            emptyEditor = new RepositoryItem();
            //grdHeader.RepositoryItems.Add(emptyEditor); --Sam

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

            }
            dtF.EditValue = DateTime.Today.Date;
            dtT.EditValue = DateTime.Today.Date;

            dtblDeliverItem = new DataTable();
            dtblDeliverItem.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("ProductID", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("ProductType", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("Price", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("Qty", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("DiscLogic", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("DiscValue", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("DiscountID", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("Discount", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("DiscountText", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("RepairItemTag", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("RepairItemSLNO", System.Type.GetType("System.String"));
            dtblDeliverItem.Columns.Add("RepairItemPurchaseDate", System.Type.GetType("System.String"));

            //lbReceipt.Text = "";--Sam
            //lbCustCompany.Text = "";
            //lbCustName.Text = "";
            //lbCustID.Text = "";

            cmbDate.SelectedIndex = 0;
            //pictureBox1.Visible = false;
            //pictureBox2.Visible = false;
            dtF.Visibility = Visibility.Collapsed;
            dtT.Visibility = Visibility.Collapsed;
            lbDate.Visibility = Visibility.Collapsed;
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
            /*cmbInvFilter.Items.Clear();
            cmbInvFilter.Items.Add("Open");
            cmbInvFilter.Items.Add("All");
            cmbInvFilter.SelectedIndex = 0;*/
            await FetchHeaderData();

            blFetch = true;

            if (gridView2.FocusedRowHandle >= 0)
            {
               await  FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv)),
                    await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRepairStatus));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));

                string rsts = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRepairStatus);
                string rvoid = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colvoid);

                btnEmail.IsEnabled = ((rsts == "In") && (rvoid == "Y"));
                btnVoid.IsEnabled = rvoid == "Y";
            }

            if (intMainScreenCustID != 0)
            {
                LookUpEdit clkup = new LookUpEdit();
                //foreach (Control c in panel1.Controls)
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
               await  FetchHeaderData();
            }

            if (Settings.REHost == "")
            {
                btnEmail.Visibility = Visibility.Collapsed;
            }

            if (Settings.ScaleDevice == "Datalogic Scale")
            {
                PrepareDatalogicScanner();
            }
        }

        private string SCAN = "";
        PosExplorer m_posExplorer = null;
        Scanner m_posScanner = null;

        SortAndSearchLookUpEdit sortAndSearchLookUpEditC;
        RepositoryItem emptyEditor;
        private int intCustID;
        private bool blFetch = false;
        private int PrevInv = 0;
        private int CurrInv = 0;
        private int intMainScreenCustID;
        private DataTable dtblDeliverItem = null;
        private double dblDueRepairAmt;
        private double dblDepositRepairAmt;

        private string strInvNo;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blCardPayment = false;
        private bool IsReturnTransaction = false;

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

        public DataTable DeliverItem
        {
            get { return dtblDeliverItem; }
            set { dtblDeliverItem = value; }
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
        public double DueRepairAmt
        {
            get { return dblDueRepairAmt; }
            set { dblDueRepairAmt = value; }
        }

        public double DepositRepairAmt
        {
            get { return dblDepositRepairAmt; }
            set { dblDepositRepairAmt = value; }
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


        private async Task FetchHeaderData()
        {
            
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRepairHeaderData(GeneralFunctions.fnInt32(cmbP.EditValue.ToString()),
                                                GeneralFunctions.fnInt32(cmbC.EditValue.ToString()),
                                                cmbDate.SelectedIndex, dtF.DateTime, dtT.DateTime,
                                                GeneralFunctions.fnInt32(txtRepairNo.Text), txtRepairItem.Text.Trim(),
                                                GeneralFunctions.GetCloseOutID(), cmbInvFilter.SelectedIndex, cmbStore.Text);

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
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));
            }
        }

        private async Task FetchHeaderData_Specific()
        {

            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRepairHeaderData_Specific(GeneralFunctions.fnInt32(txtInv.Text.Trim()),
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
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));
            }

            txtInv.Text = "";
            txtInv.Focus();
        }


        private async Task FetchDetailData(int INV, string STR)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            dtbl = objPOS.FetchRepairDetailData(INV, STR);

            grdDetail.ItemsSource = dtbl;

            string RNO = "";
            string CC = "";
            string CI = "";
            string CN = "";
            string CID = "";
            RNO = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv);
            CC = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCC);
            CI = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCustID);
            CN = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCust);
            CID = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID);
            //lbReceipt.Text = "Receipt Number            " + RNO;

            if (CID != "0")
            {
                //lbCustCompany.Text = "Company                " + CC;
                //lbCustName.Text = "Customer               " + CN;
                //lbCustID.Text = "Customer ID            " + CI;
            }
            else
            {
                //lbCustCompany.Text = "";
                //lbCustName.Text = "";
                //lbCustID.Text = "";
            }
            dtbl.Dispose();

            colCheck.Visible = true;
            colInfo.VisibleIndex = 0;
            colCheck.VisibleIndex = 1;

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRentStatus) == "18")
            {
                colCheck.Visible = false;
            }

            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRepairStatus) == "Delivered")
            {
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
        }

        private async void gridView2_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                if (blFetch)
                   await   FetchDetailData(GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv)),
                        await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRepairStatus));

                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));
                CurrInv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));

                string rsts = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRepairStatus);
                string rvoid = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colvoid);

                btnEmail.IsEnabled = ((rsts == "In") && (rvoid == "Y"));
                btnVoid.IsEnabled = rvoid == "Y";

                if (PrevInv == 0)
                {
                    PrevInv = CurrInv;
                }
            }
        }

        private async void LookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (blFetch)
            {
                grdDetail.ItemsSource = null;
                await FetchHeaderData();
            }
        }

        private async void cmbDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbDate.SelectedIndex == 0)
            {
                //pictureBox1.Visible = false;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Collapsed;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if ((cmbDate.SelectedIndex == 1) || (cmbDate.SelectedIndex == 2))
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = false;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Collapsed;
                lbDate.Visibility = Visibility.Collapsed;
            }
            if (cmbDate.SelectedIndex == 3)
            {
                //pictureBox1.Visible = true;
                //pictureBox2.Visible = true;
                dtF.Visibility = Visibility.Visible;
                dtT.Visibility = Visibility.Visible;
                lbDate.Visibility = Visibility.Visible;
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

        private string GetUniqueString()
        {
            return Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) +
                Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
        }

        private async void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtbl = new DataTable();
            dtbl = grdDetail.ItemsSource as DataTable;
            if (dtbl == null) return;
            double dqty = 0;
            double disc = 0;
            foreach (DataRow dr in dtbl.Rows)
            {

                if (!Convert.ToBoolean(dr["ReturnCheck"].ToString())) continue;
                dqty = 0;
                dqty = 1;//-(GeneralFunctions.fnDouble(dr["Qty"].ToString())); 
                disc = 0;
                disc = -(GeneralFunctions.fnDouble(dr["Discount"].ToString()));
                dtblDeliverItem.Rows.Add(new object[] {dr["ID"].ToString(),
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
                                                dr["RepairItemTag"].ToString(),
                                                dr["RepairItemSLNO"].ToString(),
                                                dr["RepairItemPurchaseDate"].ToString()});
            }
            if (dtblDeliverItem.Rows.Count > 0)
            {
                strInvNo = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv);
                dblDueRepairAmt = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colDeposit2));
                intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colCID));

                if (dblDueRepairAmt != 0)
                {
                    DialogResult = true;
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

                    dtblPOS.Columns.Add("REPAIRITEMTAG", System.Type.GetType("System.String"));//34
                    dtblPOS.Columns.Add("REPAIRITEMSLNO", System.Type.GetType("System.String"));//34
                    dtblPOS.Columns.Add("REPAIRITEMPURCHASEDATE", System.Type.GetType("System.String"));//34

                    dtblPOS.Columns.Add("MIXMATCHID", System.Type.GetType("System.Int32"));//56
                    dtblPOS.Columns.Add("MIXMATCHFLAG", System.Type.GetType("System.String"));//57
                    dtblPOS.Columns.Add("MIXMATCHTYPE", System.Type.GetType("System.String"));//58
                    dtblPOS.Columns.Add("MIXMATCHVALUE", System.Type.GetType("System.Double"));//59
                    dtblPOS.Columns.Add("MIXMATCHQTY", System.Type.GetType("System.Int32"));//60
                    dtblPOS.Columns.Add("MIXMATCHUNIQUE", System.Type.GetType("System.Int32"));//61
                    dtblPOS.Columns.Add("MIXMATCHLAST", System.Type.GetType("System.String"));//61

                    dtblPOS.Columns.Add("FEESID", System.Type.GetType("System.String"));//62
                    dtblPOS.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//63
                    dtblPOS.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));//64
                    dtblPOS.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));//65
                    dtblPOS.Columns.Add("FEES", System.Type.GetType("System.String"));//66
                    dtblPOS.Columns.Add("FEESTAX", System.Type.GetType("System.String"));//67
                    dtblPOS.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//68

                    dtblPOS.Columns.Add("SALEPRICEID", System.Type.GetType("System.Int32"));//56

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
                    foreach (DataRow dr in dtblDeliverItem.Rows)
                    {
                        rowno++;
                        string ss = dr["ID"].ToString();
                        intQty = GeneralFunctions.fnDouble(dr["Qty"].ToString());
                        dblPrice = GeneralFunctions.fnDouble(dr["Price"].ToString());
                        double tempprice = 0;
                        tempprice = (intQty * dblPrice);

                        dtblPOS.Rows.Add(new object[]
                                        {
                                              dr["ProductID"].ToString(),
                                              dr["Description"].ToString(),
                                              dr["ProductType"].ToString(),
                                              "0","0","0",
                                              dr["Qty"].ToString(),
                                              dr["Price"].ToString(),
                                              dr["Price"].ToString(),
                                              tempprice.ToString(),
                                              "0","0","0",dr["ID"].ToString(),
                                              "","","",GetUniqueString(),
                                              "2","",
                                              dr["DiscLogic"].ToString(),
                                              dr["DiscValue"].ToString(),
                                              dr["Discount"].ToString(),
                                              dr["DiscountID"].ToString(),
                                              dr["DiscountText"].ToString(),
                                              "1",
                                              "0","0","0","","","","0","0","0","N","N","N",
                                              "Repair","NA","0","0","0",
                                              dr["RepairItemTag"].ToString(),
                                              dr["RepairItemSLNO"].ToString(),
                                              dr["RepairItemPurchaseDate"].ToString(),
                                              0,"","",0,0,0,"",
                                              "0","","0","0","0","0","",
                                              "0","","0","0","0","0","",0,0,0,0,0,
                                               "0","X",rowno.ToString(),""});
                    }


                    int intINV = 0;
                    string srterrmsg = "";
                    PosDataObject.POS objpos = new PosDataObject.POS();
                    objpos.Connection = SystemVariables.Conn;
                    objpos.EmployeeID = SystemVariables.CurrentUserID;
                    objpos.CustomerID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
                    objpos.TransType = 18;
                    objpos.ReceiptCnt = 1;
                    objpos.Status = 18;
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
                    objpos.RentReturn = false;
                    objpos.ServiceType = "Repair";
                    objpos.RentalSecurityDeposit = 0;
                    objpos.RepairDeliveryDate = Convert.ToDateTime(null);
                    objpos.RepairNotifiedDate = Convert.ToDateTime(null);
                    objpos.RepairProblem = "";
                    objpos.RepairNotes = "";
                    objpos.RepairRemarks = "";

                    objpos.RepairAmount = 0;
                    objpos.RepairAdvanceAmount = 0;

                    objpos.RepairTenderAmount = 0;
                    objpos.IssueRepairInvNo = GeneralFunctions.fnInt32(strInvNo);
                    objpos.GCCentralFlag = Settings.CentralExportImport;
                    objpos.GCOPStore = Settings.StoreCode;

                    objpos.OperateStore = Settings.StoreCode;

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
                        CloseKeyboards();
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

            dtblFinal.Columns.Add("REPAIRITEMTAG", System.Type.GetType("System.String"));//34
            dtblFinal.Columns.Add("REPAIRITEMSLNO", System.Type.GetType("System.String"));//34
            dtblFinal.Columns.Add("REPAIRITEMPURCHASEDATE", System.Type.GetType("System.String"));//34

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
                                         drR["REPAIRITEMTAG"].ToString(),
                                        drR["REPAIRITEMSLNO"].ToString(),
                                        drR["REPAIRITEMPURCHASEDATE"].ToString()});
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
                    frm_POSInvoicePrintDlg.PrintType = "Repair Deliver";
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
                OfflineRetailV2.Report.Sales.repInvCC rep_InvCC = new OfflineRetailV2.Report.Sales.repInvCC();
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

                if (strservice == "Rent")
                {
                    if (intHeaderStatus == 16) // return
                    {
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                        rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentReturnLine.Report.DataSource = dtbl1;
                        rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
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

                //dblOrderSubtotal = Settings.TaxInclusive == "N" ? dblOrderSubtotal : dblOrderSubtotal - dblTax;

                if (strservice == "Rent")
                {
                    if (intHeaderStatus == 16) // return
                    {
                        if (dblOrderTotal != 0)
                        {
                            rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentReturnSubTotal;
                            rep_InvRentReturnSubTotal.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentReturnSubTotal.rReturnDeposit.Text = dblOrderTotal.ToString();
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
                dtbl3 = RearrangeTenderForCashBack(intTranNo, dtbl3);
                double dblTenderAmt = 0;
                foreach (DataRow dr1 in dtbl3.Rows)
                {
                    if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
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
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRentStatus) == "17") receipttype = "Issue";
            if (await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colRentStatus) == "18") receipttype = "Return";
            int INV = 0;
            int PINV = 0;
            if (receipttype == "Issue")
                INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));
            if (receipttype == "Return")
            {
                INV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));
                PINV = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colparent));
            }
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
            ReprintInvoice(INV, PINV, blf, receipttype);
        }

        private void ReprintInvoice(int intINV, int intPINV, bool blVoid, string receipttype)
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
                        frm_POSInvoicePrintDlg.PrintType = "Repair In";
                        frm_POSInvoicePrintDlg.IsRentIssued = true;
                        frm_POSInvoicePrintDlg.IsRentReturned = false;
                    }
                    if (receipttype == "Return")
                    {
                        frm_POSInvoicePrintDlg.PrintType = "Repair Deliver";
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

                double dblFees = 0;
                double dblFeesTax = 0;
                double dblFeesCoupon = 0;
                double dblFeesCouponTax = 0;

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
                    dblFees = GeneralFunctions.fnDouble(dr["Fees"].ToString());
                    dblFeesTax = GeneralFunctions.fnDouble(dr["FeesTax"].ToString());
                    dblFeesCoupon = GeneralFunctions.fnDouble(dr["FeesCoupon"].ToString());
                    dblFeesCouponTax = GeneralFunctions.fnDouble(dr["FeesCouponTax"].ToString());
                    strDiscountReason = dr["DiscountReason"].ToString();
                    calcrent = dr["IsRentCalculated"].ToString();
                    strservice = dr["ServiceType"].ToString();
                    intHeaderStatus = GeneralFunctions.fnInt32(dr["Status"].ToString());
                    dblRentDeposit = GeneralFunctions.fnDouble(dr["RentDeposit"].ToString());
                    dblRepairAmount = GeneralFunctions.fnDouble(dr["RepairAmount"].ToString());
                    dblRepairAdvanceAmount = GeneralFunctions.fnDouble(dr["RepairAdvanceAmount"].ToString());
                    if (dr["RepairDeliveryDate"].ToString() != "") strRepairDeliveryDate = GeneralFunctions.fnDate(dr["RepairDeliveryDate"].ToString()).ToShortDateString();
                }
                if (intHeaderStatus == 17) dblOrderTotal = dblRepairAmount;
                if (intPINV > 0)

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

                if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
                {
                    rep_InvHeader1.rOrderDate.DataBindings.Add("Text", dtbl, "RepairDateIn");
                }


                if (intCID > 0)
                {
                    rep_InvMain.subrepH2.ReportSource = rep_InvHeader2;
                    rep_InvHeader2.Report.DataSource = dtbl;
                    rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustID");
                    rep_InvHeader2.rCustName.DataBindings.Add("Text", dtbl, "CustName");
                    rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "CustCompany");

                    if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
                    {
                        rep_InvHeader2.rlCustID.Text = "Ph.";
                        rep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustMobile");

                        rep_InvHeader2.rlCompany.DataBindings.Add("Text", dtbl, "RepairItemName");
                        rep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "RepairItemSlNo");
                    }

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
                if (intPINV == 0) dtbl1 = objPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                if (intPINV > 0) dtbl1 = objPOS2.FetchInvoiceDetails1(intPINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                RearrangeForTaggedItemInInvoice(dtbl1);

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
                        rep_InvMain.subrepLine.ReportSource = rep_InvRentReturnLine;
                        rep_InvRentReturnLine.DecimalPlace = Settings.DecimalPlace;
                        rep_InvRentReturnLine.Report.DataSource = dtbl1;
                        rep_InvRentReturnLine.rlqty.DataBindings.Add("Text", dtbl1, "Qty");
                        rep_InvRentReturnLine.rISKU.DataBindings.Add("Text", dtbl1, "SKU");
                        rep_InvRentReturnLine.rlIem.DataBindings.Add("Text", dtbl1, "Description");
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
                        rep_InvRentReturnLine.rlAmt.DataBindings.Add("Text", dtbl1, "TotalPrice");
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
                        if (dblOrderTotal != 0)
                        {
                            rep_InvMain.subrepSubtotal.ReportSource = rep_InvRentReturnSubTotal;
                            rep_InvRentReturnSubTotal.DecimalPlace = Settings.DecimalPlace;
                            rep_InvRentReturnSubTotal.rReturnDeposit.Text = dblOrderTotal.ToString();
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
                if (intPINV == 0) dtbl5 = objPOS23.FetchInvoiceCoupons(intINV);
                if (intPINV > 0) dtbl5 = objPOS23.FetchInvoiceCoupons(intPINV);
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
                int TenderCount = 0;
                TenderCount = dtbl3.Rows.Count;
                foreach (DataRow dr1 in dtbl3.Rows)
                {
                    if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                    dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                }

                rep_InvMain.subrepTender.ReportSource = rep_InvTendering;
                rep_InvTendering.Report.DataSource = dtbl3;
                rep_InvTendering.DecimalPlace = Settings.DecimalPlace;
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

                rep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                rep_InvTendering.rTenderName.DataBindings.Add("Text", dtbl3, "DisplayAs");
                rep_InvTendering.rTenderAmt.DataBindings.Add("Text", dtbl3, "Amount");

                if (strservice == "Repair")
                {
                    string MFooter = "";
                    if ((Settings.ReceiptFooter == "") && (Settings.ReceiptLayawayPolicy == ""))
                    {
                        rep_InvMain.ReportFooter.Height = 35;
                        rep_InvMain.rReportFooter.Text = "";
                    }
                    else
                    {
                        if (Settings.ReceiptFooter != "") MFooter = "CUSTOMER AGREEMENT: " + Settings.ReceiptFooter;
                        if (Settings.ReceiptLayawayPolicy != "")
                        {
                            if (Settings.ReceiptFooter == "") MFooter = "REPAIR DISCLAIMER: " + Settings.ReceiptLayawayPolicy;
                            else MFooter = MFooter + " \n " + "REPAIR DISCLAIMER: " + Settings.ReceiptLayawayPolicy;
                        }
                        rep_InvMain.ReportFooter.Height = 120;
                        rep_InvMain.rReportFooter.Text = MFooter;

                    }

                    if (intHeaderStatus == 17)
                    {
                        rep_InvTendering.rlbAdvance.Text = "Deposit Amount";
                        rep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                        rep_InvTendering.rlbDue.Text = "Balance Due";
                        rep_InvTendering.rDue.Text = (dblRepairAmount - dblRepairAdvanceAmount).ToString();
                        //dblOrderTotal = dblRepairAdvanceAmount;
                        //if (dblRepairAdvanceAmount == 0) dblTenderAmt = dblOrderTotal;
                    }
                    if (intHeaderStatus == 18)
                    {
                        if (dblRepairAdvanceAmount > 0)
                        {
                            rep_InvTendering.rlbAdvance.Text = "Deposit Amount";
                            rep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                            rep_InvTendering.rlbDue.Text = "Balance Due";
                            rep_InvTendering.rDue.Text = (dblRepairAmount - dblRepairAdvanceAmount).ToString();
                        }
                        else
                        {
                            rep_InvTendering.rlbAdvance.Text = "";
                            rep_InvTendering.rAdvance.Text = "";
                            rep_InvTendering.rlbDue.Text = "";
                            rep_InvTendering.rDue.Text = "";
                        }
                        dblOrderTotal = dblRepairAmount - dblRepairAdvanceAmount;
                    }
                }
                else
                {
                    rep_InvTendering.rlbAdvance.Text = "";
                    rep_InvTendering.rAdvance.Text = "";
                    rep_InvTendering.rlbDue.Text = "";
                    rep_InvTendering.rDue.Text = "";
                }

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
                    rep_InvTendering.rChangeDueText.Text = "Change";
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
                    //rep_InvTendering.ReportFooter.Visible = false;
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

                    if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
                    {
                        drep_InvHeader1.rOrderDate.DataBindings.Add("Text", ddtbl, "RepairDateIn");
                    }
                    if (intCID > 0)
                    {
                        drep_InvMain.subrepH2.ReportSource = drep_InvHeader2;
                        drep_InvHeader2.Report.DataSource = ddtbl;
                        drep_InvHeader2.rCustID.DataBindings.Add("Text", ddtbl, "CustID");
                        drep_InvHeader2.rCustName.DataBindings.Add("Text", ddtbl, "CustName");
                        drep_InvHeader2.rCompany.DataBindings.Add("Text", ddtbl, "CustCompany");

                        if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
                        {
                            drep_InvHeader2.rlCustID.Text = "Ph.";
                            drep_InvHeader2.rCustID.DataBindings.Add("Text", dtbl, "CustMobile");

                            drep_InvHeader2.rlCompany.DataBindings.Add("Text", dtbl, "RepairItemName");
                            drep_InvHeader2.rCompany.DataBindings.Add("Text", dtbl, "RepairItemSlNo");
                        }
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
                    if (intPINV == 0) ddtbl1 = dobjPOS2.FetchInvoiceDetails1(intINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
                    if (intPINV > 0) ddtbl1 = dobjPOS2.FetchInvoiceDetails1(intPINV, dblOrderTotal < 0 ? true : false, Settings.TaxInclusive);
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
                            drep_InvRentReturnLine.rlAmt.DataBindings.Add("Text", dtbl1, "TotalPrice");
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
                    if (intPINV == 0) ddtbl5 = dobjPOS23.FetchInvoiceCoupons(intINV);
                    if (intPINV > 0) ddtbl5 = dobjPOS23.FetchInvoiceCoupons(intPINV);
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
                    foreach (DataRow dr1 in ddtbl3.Rows)
                    {
                        if (dr1["DisplayAs"].ToString() == "Debit Card Total") continue;
                        dblTenderAmt = dblTenderAmt + GeneralFunctions.fnDouble(dr1["Amount"].ToString());
                    }

                    drep_InvMain.subrepTender.ReportSource = drep_InvTendering;
                    drep_InvTendering.Report.DataSource = ddtbl3;
                    drep_InvTendering.DecimalPlace = Settings.DecimalPlace;
                    drep_InvTendering.rTotal.Text = dblOrderTotal.ToString();
                    drep_InvTendering.rTenderName.DataBindings.Add("Text", ddtbl3, "DisplayAs");
                    drep_InvTendering.rTenderAmt.DataBindings.Add("Text", ddtbl3, "Amount");


                    if (strservice == "Repair")
                    {
                        if (intHeaderStatus == 17)
                        {
                            drep_InvTendering.rlbAdvance.Text = "Deposit Amount";
                            drep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                            drep_InvTendering.rlbDue.Text = "Balance Due";
                            drep_InvTendering.rDue.Text = (dblRepairAmount - dblRepairAdvanceAmount).ToString();
                        }
                        if (intHeaderStatus == 18)
                        {
                            if (dblRepairAdvanceAmount > 0)
                            {
                                drep_InvTendering.rlbAdvance.Text = "Deposit Amount";
                                drep_InvTendering.rAdvance.Text = dblRepairAdvanceAmount.ToString();
                                drep_InvTendering.rlbDue.Text = "Balance Due";
                                drep_InvTendering.rDue.Text = (dblRepairAmount - dblRepairAdvanceAmount).ToString();
                            }
                            else
                            {
                                drep_InvTendering.rlbAdvance.Text = "";
                                drep_InvTendering.rAdvance.Text = "";
                                drep_InvTendering.rlbDue.Text = "";
                                drep_InvTendering.rDue.Text = "";
                            }
                        }
                    }
                    else
                    {
                        drep_InvTendering.rlbAdvance.Text = "";
                        drep_InvTendering.rAdvance.Text = "";
                        drep_InvTendering.rlbDue.Text = "";
                        drep_InvTendering.rDue.Text = "";
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

                    if ((intHeaderStatus == 17) || (intHeaderStatus == 18))
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
                    ccdtbl11mgc1 = obcc11mgc.FetchMercuryGiftCardData(prmmgc);

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

        private async void cmbInvFilter_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (blFetch)
            {
                await FetchHeaderData();
            }
        }

        private async void cmbStore_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            PopulateCustomer();
            if (blFetch) await FetchHeaderData();
        }

        private async void txtRepairNo_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (txtRepairNo.EditValue == null) return;
            if (blFetch) await FetchHeaderData();
        }

        private  async void txtRepairItem_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnVoid.IsEnabled = false;
                if (gridView2.FocusedRowHandle < 0) return;
                int InvID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));
                if (new MessageBoxWindow().Show(Properties.Resources.Do_you_want_to_void_this_repair_record__, Properties.Resources.Void_Transaction, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    int VoidTran = 0;
                    PosDataObject.POS objp = new PosDataObject.POS();
                    objp.Connection = SystemVariables.Conn;
                    objp.LoginUserID = SystemVariables.CurrentUserID;
                    VoidTran = objp.VoidRepair(InvID, Settings.TerminalName);
                    if (VoidTran == 0)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Repair__ + " " + InvID.ToString() + " " + Properties.Resources.voided_successfully);
                        await FetchHeaderData();
                    }
                    if (VoidTran == 1)
                    {
                        DocMessage.MsgInformation(Properties.Resources.Repair__ + " " + InvID.ToString() + " " + Properties.Resources.can_not_be_voided_after_delvered);
                    }
                }
            }
            finally
            {
                btnVoid.IsEnabled = true;
            }
        }

        private async void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0) return;
            int InvID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grdHeader, colInv));
            MessageBoxResult dlg = DocMessage.MsgConfirmation(Properties.Resources.Do_you_want_to_email_this_repair_form_to_the_customer_);

            if (dlg == MessageBoxResult.Yes)
            {
                System.Threading.Thread.Sleep(100);
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(wait_form));--Sam
                bool bmail = false;
                try
                {
                    bmail = EmailRepairForm(InvID);
                }
                finally
                {
                    //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    DocMessage.MsgInformation(bmail ? Properties.Resources.Email_send_successfully : Properties.Resources.Error_while_sending_Email);
                }
            }
        }

        private bool EmailRepairForm(int pRepairID)
        {
            bool val = false;
            try
            {
                string CustEmail = "";
                string CustName = "";
                OfflineRetailV2.Report.Customer.RepairForm repf1 = new OfflineRetailV2.Report.Customer.RepairForm();

                string repxFile = "";
                repxFile = "RepairForm.repx";

                PosDataObject.POS opos1 = new PosDataObject.POS();
                opos1.Connection = SystemVariables.Conn;
                DataTable dtbl = opos1.FetchRepairHeaderForEmail(pRepairID);

                DataTable d1 = new DataTable("Repair Info");
                d1.Columns.Add("Customer First Name", System.Type.GetType("System.String"));
                d1.Columns.Add("Customer Last Name", System.Type.GetType("System.String"));
                d1.Columns.Add("Customer Phone", System.Type.GetType("System.String"));
                d1.Columns.Add("Date In", System.Type.GetType("System.String"));
                d1.Columns.Add("Expected Delivery Date", System.Type.GetType("System.String"));
                d1.Columns.Add("Customer Notified Date", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair Item", System.Type.GetType("System.String"));
                d1.Columns.Add("Problem", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair", System.Type.GetType("System.String"));
                d1.Columns.Add("Remarks", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair Amount", System.Type.GetType("System.String"));
                d1.Columns.Add("Deposit Amount", System.Type.GetType("System.String"));
                d1.Columns.Add("Customer Agreement", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair Disclaimer", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair Item Serial", System.Type.GetType("System.String"));
                d1.Columns.Add("Repair No", System.Type.GetType("System.String"));
                d1.Columns.Add("Find Us", System.Type.GetType("System.String"));

                DataTable d2 = new DataTable("Repair Parts");
                d2.Columns.Add("Item Name", System.Type.GetType("System.String"));
                d2.Columns.Add("Tag", System.Type.GetType("System.String"));
                d2.Columns.Add("Serial", System.Type.GetType("System.String"));
                d2.Columns.Add("Quantity", System.Type.GetType("System.String"));
                d2.Columns.Add("SKU", System.Type.GetType("System.String"));

                PosDataObject.POS opos2 = new PosDataObject.POS();
                opos2.Connection = SystemVariables.Conn;
                DataTable dtbl2 = opos2.FetchRepairPartsForEmail(pRepairID);
                foreach (DataRow dr in dtbl2.Rows)
                {
                    d2.Rows.Add(new object[] { dr["Item Name"].ToString(),
                                               dr["Tag"].ToString(),
                                               dr["Serial"].ToString(),
                                               dr["Quantity"].ToString(),
                                               dr["Item SKU"].ToString()
                    });
                }

                foreach (DataRow dr in dtbl.Rows)
                {
                    d1.Rows.Add(new object[] { dr["FirstName"].ToString(),
                                               dr["LastName"].ToString(),
                                               dr["MobilePhone"].ToString(),
                                               GeneralFunctions.fnDate(dr["RepairDateIn"].ToString()).ToString("MM - dd - yyyy"),
                                               dr["RepairDeliveryDate"].ToString() == "" ? "" : GeneralFunctions.fnDate(dr["RepairDeliveryDate"].ToString()).ToString("MM - dd - yyyy"),
                                               dr["RepairNotifiedDate"].ToString() == "" ? "" : GeneralFunctions.fnDate(dr["RepairNotifiedDate"].ToString()).ToString("MM - dd - yyyy"),
                                               dr["RepairItemName"].ToString(),
                                               dr["RepairProblem"].ToString(),
                                               dr["RepairNotes"].ToString(),
                                               dr["RepairRemarks"].ToString(),
                                               dr["RepairAmount"].ToString(),
                                               dr["RepairAdvanceAmount"].ToString(),
                                               Settings.ReceiptFooter,
                                               Settings.ReceiptLayawayPolicy,
                                               dr["RepairItemSlNo"].ToString(),
                                               pRepairID.ToString(),
                                               dr["RepairFindUs"].ToString()
                     });

                    CustEmail = dr["EMail"].ToString();
                    CustName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                }

                string emailattachpth = "";
                emailattachpth = Path.GetTempPath();
                if (emailattachpth.EndsWith("\\")) emailattachpth = emailattachpth + SystemVariables.BrandName + "\\Email\\";
                else emailattachpth = emailattachpth + "\\" + SystemVariables.BrandName + "\\Email\\";
                if (!Directory.Exists(emailattachpth)) Directory.CreateDirectory(emailattachpth);
                else
                {
                    Directory.Delete(@emailattachpth, true);
                    Directory.CreateDirectory(emailattachpth);
                }

                XtraReport fReport = new XtraReport();


                if (File.Exists(GeneralFunctions.GetReportFilePath(repxFile)))
                    fReport = XtraReport.FromFile(GeneralFunctions.GetReportFilePath(repxFile), true);
                else
                {
                    fReport = repf1;
                }
                if (!File.Exists(GeneralFunctions.GetReportFilePath(repxFile)))
                {
                    string fileName = GeneralFunctions.GetReportPath(fReport, "repx");
                    fReport.SaveLayout(fileName);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(d1);
                ds.Tables.Add(d2);
                fReport.Report.DataSource = ds;
                fReport.DataSource = ds;
                fReport.CreateDocument();
                fReport.PrintingSystem.ShowMarginsWarning = false;

                bool do_email = false;

                try
                {
                    do_email = GeneralFunctions.EmailRepair(fReport, emailattachpth, pRepairID.ToString(), CustEmail, CustName);
                }
                catch
                {
                }

                val = do_email;


            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
            return val;
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

      async  void _Scanner_DataEvent(object sender, DataEventArgs e)
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

        private async void txtInv_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtInv.Text.Trim() == "") e.Handled = false;
                await FetchHeaderData_Specific();
                e.Handled = true;
            }
        }

        private DataTable FetchInvFees(int pInvNo)
        {
            PosDataObject.POS objpos1 = new PosDataObject.POS();
            objpos1.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objpos1.FetchFeesInInvoice(pInvNo);
        }

        private async void CmbP_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void CmbC_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blFetch) await FetchHeaderData();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frmPOSRepairInfoDlg frm_POSRepairInfoDlg = new frmPOSRepairInfoDlg();
            try
            {
                frm_POSRepairInfoDlg.CalledFor = "Edit";
                frm_POSRepairInfoDlg.CustomerID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,
                                                    grdHeader, colCID));
                frm_POSRepairInfoDlg.ID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,
                                                    grdHeader, colInv));
                frm_POSRepairInfoDlg.ShowDialog();
                if (frm_POSRepairInfoDlg.DialogResult == true)
                {
                    await FetchHeaderData();
                }
            }
            finally
            {
                frm_POSRepairInfoDlg.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            strInvNo = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colInv);
            dblDepositRepairAmt = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDeposit1));
            dblDueRepairAmt = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colDeposit2));
            intCustID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle, grdHeader, colCID));
            DialogResult = true;
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ResMan.closeKeyboard();
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

        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
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
