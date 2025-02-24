using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraCharts;

using OfflineRetailV2.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSLayawayBrwUC.xaml
    /// </summary>
    public partial class frm_POSLayawayBrwUC : UserControl
    {
        #region Variables

        private UserControls.POSControl frm_POSN;

        public UserControls.POSControl calledform
        {
            get { return frm_POSN; }
            set { frm_POSN = value; }
        }

        private int StartRowHandle = -1;
        private int CurrentRowHandle = -1;
        private string strTaxExempt;
        private int intCustomerID;
        private bool blFinalFlag;
        private DataTable dtblLayaway = null;
        private double dblStoreCr;
        private double dblCustAcctBalance;
        private double dblCustAcctLimit;

        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blAllowByAdmin = false;

        private int intMaxInvNo = 0;

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public DataTable Layaway
        {
            get { return dtblLayaway; }
            set { dtblLayaway = value; }
        }

        public string TaxExempt
        {
            get { return strTaxExempt; }
            set { strTaxExempt = value; }
        }

        public bool FinalFlag
        {
            get { return blFinalFlag; }
            set { blFinalFlag = value; }
        }

        public double StoreCr
        {
            get { return dblStoreCr; }
            set { dblStoreCr = value; }
        }


        public double CustAcctBalance
        {
            get { return dblCustAcctBalance; }
            set { dblCustAcctBalance = value; }
        }

        public double CustAcctLimit
        {
            get { return dblCustAcctLimit; }
            set { dblCustAcctLimit = value; }
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

        #endregion
        public frm_POSLayawayBrwUC()
        {
            InitializeComponent();
        }

        public void Load()
        {
            CreateDataTable();
            SetDecimalPlace();
            LoadInitialData();
            grdLayaway.ItemsSource = dtblLayaway;
            grdLayaway.SelectAll();
            GetSelectedRowData();

            if (Settings.LayawayPaymentOption == 0)
                rg1.IsChecked = true;
            else if (Settings.LayawayPaymentOption == 1)
                rg2.IsChecked = true;
            else
                rg3.IsChecked = true;


            lbCustID.Text = ""; ;
            lbCustName.Text = "";
            lbCustAddress.Text = "";
            lbCustBal.Text = "";
            lbCustTax.Text = "";

            string refTaxExempt = "";
            string refDiscountLevel = "";
            string refTaxID = "";
            string refStoreCr = "";
            string refCID = "";
            string refCName = "";
            string refCAdd = "";
            double dblBalance = 0;
            string refARCredit = "";
            FetchCustomer(intCustomerID, ref refCID, ref refCName, ref refCAdd,
                        ref refTaxExempt, ref refDiscountLevel, ref refTaxID, ref refStoreCr, ref refARCredit);
            strTaxExempt = refTaxExempt;
            //strDiscountLevel = refDiscountLevel;
            dblBalance = GetAccountBalance(intCustomerID);
            lbCustID.Text = Properties.Resources.Customer_ID_ + refCID;
            lbCustName.Text = refCName;
            lbCustAddress.Text = refCAdd;
            ArrangeCustomerLine(GeneralFunctions.fnDouble(refStoreCr), dblBalance, refTaxID);
            dblStoreCr = GeneralFunctions.fnDouble(refStoreCr);
            dblCustAcctLimit = GeneralFunctions.fnDouble(refARCredit);
            dblCustAcctBalance = dblBalance;
        }

        


        // set demicals upto 2/ 3 digits to the controls

        private void SetDecimalPlace()
        {
            if (Settings.DecimalPlace == 3)
            {
                GeneralFunctions.SetDecimal(numAmount, 3);
                GeneralFunctions.SetDecimal(numDeposit, 3);

                colQty.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colTotalSale.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colPayment.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f3",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colBalance.EditSettings = new TextEditSettings()
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
                GeneralFunctions.SetDecimal(numAmount, 2);
                GeneralFunctions.SetDecimal(numDeposit, 2);

                colQty.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colTotalSale.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colPayment.EditSettings = new TextEditSettings()
                {
                    DisplayFormat = "f",
                    MaskType = DevExpress.Xpf.Editors.MaskType.Numeric
                };
                colBalance.EditSettings = new TextEditSettings()
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
        }

        private void CreateDataTable()
        {
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
        }

        // Loading Screen

        #region Customer Info.

        private void FetchCustomer(int iCustID, ref string refCID, ref string refCName, ref string refCAdd,
                                    ref string refTaxExempt, ref string refDiscountLevel,
                                     ref string refTaxID, ref string refStoreCr, ref string refARCredit)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            DataTable dtblCust = objPOS.FetchCustomerRecord(iCustID);
            foreach (DataRow dr in dtblCust.Rows)
            {
                refTaxExempt = dr["TaxExempt"].ToString();
                refDiscountLevel = dr["DiscountLevel"].ToString();
                if (refDiscountLevel == "") refDiscountLevel = "A";
                refTaxID = dr["TaxID"].ToString();
                refStoreCr = dr["StoreCredit"].ToString();
                refARCredit = dr["ARCreditLimit"].ToString();

                refCID = dr["CustomerID"].ToString();
                refCName = dr["Salutation"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                refCAdd = dr["Address1"].ToString() + "\n" + dr["City"].ToString();
            }
            dtblCust.Dispose();
        }

        private double GetAccountBalance(int intCID)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            return objPOS.FetchCustomerAcctPayBalance(intCID);
        }

        private void ArrangeCustomerLine(double Scr, double ABal, string Tx)
        {
            string str1 = "";
            string str2 = "";
            if (Settings.DecimalPlace == 3)
            {
                if (Scr < 0) str1 = str1 + Properties.Resources.Store_Cr_ + " (" + Scr.ToString("f3").Remove(0, 1) + ")";
                else str1 = str1 + Properties.Resources.Store_Cr_ + Scr.ToString("f3");
                if (ABal < 0) str1 = str1 + "    " + Properties.Resources.Acct_Balance_ + " (" + ABal.ToString("f3").Remove(0, 1) + ")";
                else str1 = str1 + "    " + Properties.Resources.Acct_Balance_ + ABal.ToString("f3");
            }
            else
            {
                if (Scr < 0) str1 = str1 + Properties.Resources.Store_Cr_ + " (" + Scr.ToString("f").Remove(0, 1) + ")";
                else str1 = str1 + Properties.Resources.Store_Cr_ + Scr.ToString("f");
                if (ABal < 0) str1 = str1 + "    " + Properties.Resources.Acct_Balance_ + " (" + ABal.ToString("f3").Remove(0, 1) + ")";
                else str1 = str1 + "    " + Properties.Resources.Acct_Balance_ + ABal.ToString("f");
            }
            if (Tx != "") str2 = str2 + Properties.Resources.Tax_ID_ + Tx;
            else str2 = "";

            lbCustBal.Text = str1;
            lbCustTax.Text = str2;
        }

        #endregion

        // Fetch Active Layaway Header Data

        private void LoadInitialData()
        {
            DataTable dtbl = new DataTable();
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
            dtbl = objPOS.FetchLayawayHeader(intCustomerID);
            int intC = 0;
            double dblPayment = 0;
            double dblBalance = 0;

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

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
                                                strip});
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

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtbl = GetSelectedDatatable();
            if (dtbl.Rows.Count == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.No_item_selected_for_Layaway, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                dtbl.Dispose();
                return;
            }
            else dtbl.Dispose();

            SetIndividualDeposit();
            grdLayaway.ItemsSource = dtblLayaway;
            GetSelectedRowData();
        }


        #region Adjust Tender Amount in Grid

        private DataTable GetTableOfSelectedRows(TableView view)
        {
            DataTable resultTable = new DataTable();
            if (ResultGrid.ItemsSource is DataTable)
            {
                DataTable sourceTable = ResultGrid.ItemsSource as DataTable;
                resultTable = sourceTable.Clone();
                foreach (var rowHandle in view.GetSelectedRows())
                {
                    DataRow row = ResultGrid.GetRow(rowHandle.RowHandle) as DataRow;
                    resultTable.Rows.Add(row.ItemArray);
                }
                resultTable.AcceptChanges();
            }
            return resultTable;
        }

        GridControl ResultGrid = new GridControl();
        private DevExpress.Xpf.Grid.GridControl CloneGrid(DevExpress.Xpf.Grid.GridControl sourceGrid)
        {
            DevExpress.Xpf.Grid.GridControl resultGrid = new DevExpress.Xpf.Grid.GridControl();
            resultGrid.View= new DevExpress.Xpf.Grid.TableView();
            //resultGrid.MainView.Assign(sourceGrid.MainView, false);
            //Controls.Add(resultGrid);
            AddChild(resultGrid);
            resultGrid.Visibility = Visibility.Collapsed;
            return resultGrid;
        }

        private DevExpress.Xpf.Grid.GridControl GetGridWithSelection(DevExpress.Xpf.Grid.GridControl grid)
        {
            DevExpress.Xpf.Grid.GridControl clonedGrid = CloneGrid(grid);
            DataTable clonedTable = GetTableOfSelectedRows(grid.View as TableView);
            clonedGrid.ItemsSource = clonedTable;
            return clonedGrid;
        }

        private DataTable GetSelectedDatatable()
        {



            DataTable dtbl = (grdLayaway.ItemsSource as DataTable).Clone();
            foreach (DataRowView dv in grdLayaway.SelectedItems)
            {
                dtbl.Rows.Add(new object[] { dv["ID"].ToString(), dv["LayawayNo"].ToString(), dv["InvoiceNo"].ToString(), dv["SKU"].ToString(),
                        dv["Description"].ToString(), dv["Qty"].ToString(), dv["Cost"].ToString(), dv["DateDue"].ToString(),
                        dv["TotalSale"].ToString(), dv["ItemID"].ToString(), dv["ProductID"].ToString(), dv["ProductType"].ToString(),
                        dv["Payment"].ToString(), dv["Balance"].ToString(), dv["Deposit"].ToString(), dv["MatrixOptionID"].ToString(),
                        dv["OptionValue1"].ToString(), dv["OptionValue2"].ToString(), dv["OptionValue3"].ToString()});
            }
            return dtbl;
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
            numsTS.Text = dblA.ToString();
            numsP.Text = dblB.ToString();
            numsB.Text = dblC.ToString();
            numsD.Text = dblD.ToString();
            numAmount.Text = dblC.ToString();
        }

        private void SetIndividualDeposit()
        {
            double dblAmt = Convert.ToDouble(numAmount.Text);
            double dblB = 0;
            double dblD = 0;
            double dblPD = 0;

            DataTable dtbl = GetSelectedDatatable();
            int intRC = dtbl.Rows.Count;
            int intCRC = 0;

            if (Convert.ToDouble(numAmount.Text) >= Convert.ToDouble(numsB.Text))
            {
                foreach (DataRow dr1 in dtbl.Rows)
                {
                    if (Settings.DecimalPlace == 3)
                        dr1["Deposit"] = GeneralFunctions.fnDouble(dr1["Balance"].ToString()).ToString("f3");
                    else
                        dr1["Deposit"] = GeneralFunctions.fnDouble(dr1["Balance"].ToString()).ToString("f");
                }
            }

            else
            {
                if (rg1.IsChecked == true) // weighted avg on balance due (default)
                {

                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        intCRC++;
                        if (Settings.DecimalPlace == 3)
                        {
                            dblD = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr1["Balance"].ToString()) * dblAmt / Convert.ToDouble(numsB.Text)).ToString("f3"));
                            dblB = GeneralFunctions.fnDouble((dblAmt - dblPD).ToString("f3"));
                        }
                        else
                        {
                            dblD = GeneralFunctions.fnDouble((GeneralFunctions.fnDouble(dr1["Balance"].ToString()) * dblAmt / Convert.ToDouble(numsB.Text)).ToString("f"));
                            dblB = GeneralFunctions.fnDouble((dblAmt - dblPD).ToString("f"));
                        }
                        if (intCRC != intRC)
                        {
                            dr1["Deposit"] = dblD.ToString();
                            dblPD = dblPD + dblD;
                        }
                        else
                        {
                            if (dblD == dblB) dr1["Deposit"] = dblD;
                            else dr1["Deposit"] = dblB;
                        }
                    }

                }


                if (rg2.IsChecked == true) // weighted avg on total sale
                {

                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        intCRC++;
                        if (Settings.DecimalPlace == 3)
                        {
                            dblD = Math.Round((GeneralFunctions.fnDouble(dr1["TotalSale"].ToString()) * dblAmt / Convert.ToDouble(numsTS.Text)), 3);
                            dblB = Math.Round((dblAmt - dblPD), 3);
                        }
                        else
                        {
                            dblD = Math.Round((GeneralFunctions.fnDouble(dr1["TotalSale"].ToString()) * dblAmt / Convert.ToDouble(numsTS.Text)), 2);
                            dblB = Math.Round((dblAmt - dblPD), 2);
                        }
                        if (intCRC != intRC)
                        {
                            dr1["Deposit"] = dblD.ToString();
                            dblPD = dblPD + dblD;
                        }
                        else
                        {
                            if (dblD == dblB) dr1["Deposit"] = dblD;
                            else dr1["Deposit"] = dblB;
                        }
                    }

                }

                if (rg3.IsChecked == true) // top down
                {

                    foreach (DataRow dr1 in dtbl.Rows)
                    {
                        intCRC++;
                        if (Settings.DecimalPlace == 3)
                        {
                            dblD = Math.Round(GeneralFunctions.fnDouble(dr1["Balance"].ToString()), 3);
                            dblB = Math.Round((dblAmt - dblPD), 3);
                        }
                        else
                        {
                            dblD = Math.Round(GeneralFunctions.fnDouble(dr1["Balance"].ToString()), 2);
                            dblB = Math.Round((dblAmt - dblPD), 2);
                        }
                        if (dblB < 0)
                        {
                            if (dblAmt > 0)
                            {
                                dr1["Deposit"] = dblAmt.ToString();
                                dblPD = dblPD + dblAmt;
                            }

                        }
                        if ((dblAmt >= dblD) && (dblB >= 0))
                        {
                            if (dblB >= dblD)
                            {
                                dr1["Deposit"] = dblD.ToString();
                                dblPD = dblPD + dblD;
                            }
                            else
                            {
                                dr1["Deposit"] = dblB.ToString();
                                dblPD = dblPD + dblB;
                            }
                        }
                        if ((dblAmt < dblD) && (dblB >= 0))
                        {
                            if (dblPD == 0)
                            {
                                dr1["Deposit"] = dblAmt.ToString();
                                dblPD = dblPD + dblAmt;
                            }
                            else
                            {
                                dr1["Deposit"] = dblB;
                                dblPD = dblPD + dblB;
                            }
                        }

                    }

                }
            }

            if (dtblLayaway == null)
            {
                CreateDataTable();
                dtblLayaway = grdLayaway.ItemsSource as DataTable;
            }

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

            if (Convert.ToDouble(numAmount.Text) >= Convert.ToDouble(numsB.Text))
                numDeposit.Text = numsB.Text;
            else
                numDeposit.Text = numAmount.Text;

            numAmount.Text = "0";

            dtbl.Dispose();
        }

        private void SetIndividualDepositForRefund()
        {

            DataTable dtbl = GetSelectedDatatable();

            foreach (DataRow dr1 in dtbl.Rows)
            {
                dr1["Deposit"] = Convert.ToString(-GeneralFunctions.fnDouble(dr1["Payment"].ToString()));
            }

            if (dtblLayaway == null)
            {
                CreateDataTable();
                dtblLayaway = grdLayaway.ItemsSource as DataTable;
            }
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


        #endregion

        private void btnTender_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidTender())
            {
                ResMan.closeKeyboard();
                blurGrid.Visibility = Visibility.Visible;
                frmPOSTenderDlg frm_POSTenderDlg = new frmPOSTenderDlg();
                try
                {


                    frm_POSTenderDlg.ResumeTransaction = false;
                    frm_POSTenderDlg.ReturnItem = false;
                    frm_POSTenderDlg.NewLayaway = false;
                    frm_POSTenderDlg.FinalFlag = false;
                    frm_POSTenderDlg.Layaway = true;
                    frm_POSTenderDlg.LayawayRefund = false;
                    frm_POSTenderDlg.CustID = intCustomerID;
                    frm_POSTenderDlg.MaxInvNo = intMaxInvNo;
                    frm_POSTenderDlg.TaxExempt = strTaxExempt;
                    frm_POSTenderDlg.LayawayPayment = GetSelectedDatatable(); 
                    frm_POSTenderDlg.LayawayAmt = Convert.ToDouble(numDeposit.Text);
                    frm_POSTenderDlg.LayawayTotalSale = Convert.ToDouble(numDeposit.Text);
                    frm_POSTenderDlg.StoreCr = dblStoreCr;
                    frm_POSTenderDlg.CustAcctLimit = dblCustAcctLimit;
                    frm_POSTenderDlg.CustAcctBalance = dblCustAcctBalance;
                    frm_POSTenderDlg.calledfrm = frm_POSN;
                    frm_POSTenderDlg.ShowDialog();
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                    blFinalFlag = frm_POSTenderDlg.FinalFlag;
                    dtblLayaway = frm_POSTenderDlg.dtblForStockUpdate;
                    if (blFinalFlag) CloseWindow?.Invoke();
                }
            }
        }
        public CloseWindowCallback CloseWindow { get; set; }
        
        private void btnRefund_Click(object sender, RoutedEventArgs e)
        {
            /*if ((SystemVariables.CurrentUserID >  0) && (!SecurityPermission.AcssPOSLayawayRefund))
          {
              DocMessage.POSRestrictAccess();
              return;
          }*/

            if (!CheckFunctionButton("31v")) return;

            if (IsValidRefund())
            {
                ResMan.closeKeyboard();
                blurGrid.Visibility = Visibility.Visible;
                frmPOSTenderDlg frm_POSTenderDlg = new frmPOSTenderDlg();
                try
                {
                    SetIndividualDepositForRefund();
                    GetSelectedRowData();
                    frm_POSTenderDlg.ResumeTransaction = false;
                    frm_POSTenderDlg.ReturnItem = false;
                    frm_POSTenderDlg.NewLayaway = false;
                    frm_POSTenderDlg.FinalFlag = false;
                    frm_POSTenderDlg.Layaway = false;
                    frm_POSTenderDlg.LayawayRefund = true;
                    frm_POSTenderDlg.CustID = intCustomerID;
                    frm_POSTenderDlg.TaxExempt = strTaxExempt;
                    frm_POSTenderDlg.MaxInvNo = intMaxInvNo;
                    frm_POSTenderDlg.LayawayPayment = GetSelectedDatatable();
                    frm_POSTenderDlg.LayawayAmt = Convert.ToDouble(numsD.Text);
                    frm_POSTenderDlg.LayawayTotalSale = Convert.ToDouble(numsD.Text);
                    frm_POSTenderDlg.StoreCr = dblStoreCr;
                    frm_POSTenderDlg.CustAcctLimit = dblCustAcctLimit;
                    frm_POSTenderDlg.CustAcctBalance = dblCustAcctBalance;
                    frm_POSTenderDlg.SuperUserID = intSuperUserID;
                    frm_POSTenderDlg.FunctionBtnAccess = blFunctionBtnAccess;
                    //frm_POSTenderDlg.calledfrm = frm_POS;
                    frm_POSTenderDlg.ShowDialog();
                }
                finally
                {
                    blurGrid.Visibility = Visibility.Collapsed;
                    blFinalFlag = frm_POSTenderDlg.FinalFlag;
                    if (blFinalFlag)
                    {
                        CloseWindow?.Invoke();
                    }
                }
            }
        }

        // Check for valid Tender

        private bool IsValidTender()
        {
            int tempinv = 0;
            if (Convert.ToDouble(numDeposit.Text) == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Current_Deposit_Amount_needed_for_Tender, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            DataTable dtbl = GetSelectedDatatable(); 
            double totDeposit = 0;
            bool blfindzerodeposit = false;
            foreach (DataRow dr in dtbl.Rows)
            {
                if (Settings.DecimalPlace == 3)
                    totDeposit = GeneralFunctions.fnDouble((totDeposit + GeneralFunctions.fnDouble(dr["Deposit"].ToString())).ToString("f3"));
                else
                    totDeposit = GeneralFunctions.fnDouble((totDeposit + GeneralFunctions.fnDouble(dr["Deposit"].ToString())).ToString("f"));

                if (GeneralFunctions.fnDouble(dr["Deposit"].ToString()) == 0) blfindzerodeposit = true;

                tempinv = GeneralFunctions.fnInt32(dr["InvoiceNo"].ToString());
                if (tempinv > intMaxInvNo) intMaxInvNo = tempinv;
            }
            if (dtbl.Rows.Count == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.No_item_selected_for_Layaway_Payment, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                dtbl.Dispose();
                return false;
            }

            if (totDeposit == 0)
            {
                new MessageBoxWindow().Show(Properties.Resources.Nothing_has_been_applied_, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                dtbl.Dispose();
                return false;
            }

            if ((totDeposit != Convert.ToDouble(numDeposit.Text))) //if ((totDeposit != numDeposit.Value) || (blfindzerodeposit))
            {
                new MessageBoxWindow().Show(Properties.Resources.Applied_Deposit_not_equal_to_Current_Deposit, Properties.Resources.Layaway, MessageBoxButton.OK, MessageBoxImage.Information);
                dtbl.Dispose();
                return false;
            }
            dtbl.Dispose();
            return true;
        }

        // Check for valid refund

        private bool IsValidRefund()
        {
            int tempinv = 0;
            DataTable dtbl = GetSelectedDatatable();
            double totDeposit = 0;
            foreach (DataRow dr in dtbl.Rows)
            {
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtbl = GetSelectedDatatable();
            int intRC = dtbl.Rows.Count;
            int intCRC = 0;

            foreach (DataRow dr1 in dtbl.Rows)
            {
                dr1["Deposit"] = "0";
            }

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
            grdLayaway.ItemsSource = dtblLayaway;
            numAmount.Text = "0";
            numDeposit.Text = "0";
            GetSelectedRowData();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (btnSelect.Content.ToString() == "All")
            {
                grdLayaway.SelectAll();
                btnSelect.Content = "None";
                GetSelectedRowData();
                return;
            }
            else
            {
                grdLayaway.UnselectAll();
                btnSelect.Content = "All";
                GetSelectedRowData();
                return;
            }

            
        }

        private void grdLayaway_SelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e)
        {
            GetSelectedRowData();
        }


        // Check Function Button Access and open super user login ( if required )

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

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
    }
}
