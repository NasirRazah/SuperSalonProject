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
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;


namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_EmpAttendanceReportDlg.xaml
    /// </summary>
    public partial class frm_TrnJrnDReportDlg : Window
    {
        private int intReportID;
        public int ReportID
        {
            get { return intReportID; }
            set { intReportID = value; }
        }
        public frm_TrnJrnDReportDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        public void PopulateEmployee()
        {
            PosDataObject.Employee objEmployee = new PosDataObject.Employee();
            objEmployee.Connection = SystemVariables.Conn;
            DataTable dbtblEmp = new DataTable();
            dbtblEmp = objEmployee.FetchModifiedLookupData();

            

            cmbEmployee.ItemsSource = dbtblEmp;
            
            int SelectID = 0;
            foreach (DataRow dr in dbtblEmp.Rows)
            {
                SelectID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                break;
            }
            cmbEmployee.EditValue = "0";
            dbtblEmp.Dispose();
        }

        public void PopulateTerminal()
        {
            DataTable dbtblTerminal = new DataTable();
            dbtblTerminal.Columns.Add("ID", System.Type.GetType("System.String"));
            dbtblTerminal.Columns.Add("Terminal", System.Type.GetType("System.String"));
            PosDataObject.Closeout oc = new PosDataObject.Closeout();
            oc.Connection = SystemVariables.Conn;
            DataTable dtbl = oc.FetchTerminals();
            foreach (DataRow dr in dtbl.Rows)
            {
                dbtblTerminal.Rows.Add(new object[] { dr["TerminalName"].ToString().ToUpper(), dr["TerminalName"].ToString() });
            }
            dtbl.Dispose();

            dbtblTerminal.Rows.Add(new object[] { "ALL-TERMINAL-6473", "ALL" });

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblTerminal.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbTill.ItemsSource = dtblTemp;
            cmbTill.EditValue = "ALL-TERMINAL-6473";
            dbtblTerminal.Dispose();
        }

        private void PopulateReportCategory()
        {
            DataTable dtblCategory = new DataTable();
            dtblCategory.Columns.Add("Filter", System.Type.GetType("System.String"));
            dtblCategory.Columns.Add("FilterText", System.Type.GetType("System.String"));
            dtblCategory.Rows.Add(new object[] { "Detail", "Detail" });
            dtblCategory.Rows.Add(new object[] { "Summary", "Summary" });
            cmbCategory.ItemsSource = dtblCategory;
            cmbCategory.EditValue = "Detail";
            dtblCategory.Dispose();
        }

        

        private bool CheckBlankDate()
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null))
            {
                DocMessage.MsgInformation("Invalid Date");
                GeneralFunctions.SetFocus(dtFrom);
                return false;
            }
            return true;
        }

        

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                if (CheckBlankDate())
                {
                    if (dtTo.DateTime.Date > DateTime.Today)
                    {
                        DocMessage.MsgInformation("To Date can not be after Today");
                        GeneralFunctions.SetFocus(dtTo);
                        return;
                    }
                    GetReport(eventtype);
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private async void LbEmployee_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frm_Lookups frm_Lookups = new frm_Lookups();
            try
            {
                frm_Lookups.Print = "N";
                frm_Lookups.SearchType = "Employee";
                frm_Lookups.ShowDialog();
                if (await frm_Lookups.GetID() > 0)
                {
                    cmbEmployee.EditValue = (await frm_Lookups.GetID()).ToString();
                }
            }
            finally
            {
                frm_Lookups.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Title.Text = "Transaction Journal Detailed Report";
            PopulateEmployee();
            PopulateTerminal();
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            tmFrom.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0);
            tmTo.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);

        }


        private void GetReport(string eventtype)
        {
            PosDataObject.POS objAttendance = new PosDataObject.POS();
            objAttendance.Connection = SystemVariables.Conn;
            DataTable dtbl = new DataTable();
            int intID = 0;


            objAttendance.BeginTransaction();

            dtbl = objAttendance.FetchTransactionJournalD(new DateTime(dtFrom.DateTime.Year, dtFrom.DateTime.Month, dtFrom.DateTime.Day, tmFrom.DateTime.Hour, tmFrom.DateTime.Minute, tmFrom.DateTime.Second),
                new DateTime(dtTo.DateTime.Year, dtTo.DateTime.Month, dtTo.DateTime.Day, tmTo.DateTime.Hour, tmTo.DateTime.Minute, tmTo.DateTime.Second),
                cmbTill.EditValue.ToString(), GeneralFunctions.fnInt32(cmbEmployee.EditValue.ToString()), Settings.TaxInclusive, SystemVariables.DateFormat);
            objAttendance.EndTransaction();
            if (dtbl.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtbl.Dispose();
                return;
            }

            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }


            DataTable p = new DataTable("Parent");

            p.Columns.Add("Item", System.Type.GetType("System.String"));
            p.Columns.Add("TranNo", System.Type.GetType("System.Int32"));
            p.Columns.Add("TranType", System.Type.GetType("System.String"));
            p.Columns.Add("TranDate", System.Type.GetType("System.String"));
            p.Columns.Add("Terminal", System.Type.GetType("System.String"));
            p.Columns.Add("Employee", System.Type.GetType("System.String"));
            p.Columns.Add("InvNo", System.Type.GetType("System.String"));
            p.Columns.Add("InvAmt", System.Type.GetType("System.String"));
            p.Columns.Add("Customer", System.Type.GetType("System.String"));
            p.Columns.Add("Tax", System.Type.GetType("System.String"));

            int tcnt = 1;
            int Tinx = 0;
            int indx = 0;

            int TotalTender = 0;

            foreach (DataColumn dc in dtbl.Columns)
            {
                if (dc.ColumnName.StartsWith("T_"))
                {
                    TotalTender++;
                    if (Tinx == 0) Tinx = indx;
                    p.Columns.Add("T_" + tcnt.ToString(), System.Type.GetType("System.String"));
                    tcnt++;
                }
                indx++;
            }

            foreach (DataRow dr in dtbl.Rows)
            {
                DataRow r = p.NewRow();
                r["Item"] = "N";
                r["TranNo"] = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                r["TranType"] = dr["TranType"].ToString();
                r["TranDate"] = dr["TranDate"].ToString();
                r["Terminal"] = dr["Terminal"].ToString();
                r["Employee"] = dr["Employee"].ToString();
                r["InvNo"] = dr["InvNo"].ToString();
                r["InvAmt"] = dr["InvAmt"].ToString();
                r["Customer"] = dr["Customer"].ToString();
                r["Tax"] = dr["Tax"].ToString();

                int tcnt1 = 1;

                foreach (DataColumn dc in dtbl.Columns)
                {
                    if (dc.ColumnName.StartsWith("T_"))
                    {
                        if (tcnt1 == 1) r["T_1"] = dr[Tinx].ToString();
                        if (tcnt1 == 2) r["T_2"] = dr[Tinx + 1].ToString();
                        if (tcnt1 == 3) r["T_3"] = dr[Tinx + 2].ToString();
                        if (tcnt1 == 4) r["T_4"] = dr[Tinx + 3].ToString();
                        if (tcnt1 == 5) r["T_5"] = dr[Tinx + 4].ToString();
                        if (tcnt1 == 6) r["T_6"] = dr[Tinx + 5].ToString();
                        if (tcnt1 == 7) r["T_7"] = dr[Tinx + 6].ToString();
                        if (tcnt1 == 8) r["T_8"] = dr[Tinx + 7].ToString();
                        if (tcnt1 == 9) r["T_9"] = dr[Tinx + 8].ToString();
                        if (tcnt1 == 10) r["T_10"] = dr[Tinx + 9].ToString();

                        tcnt1++;
                    }
                }

                p.Rows.Add(r);
            }


            DataTable c = new DataTable("Child");
            c.Columns.Add("TranNo", System.Type.GetType("System.Int32"));
            c.Columns.Add("SKU", System.Type.GetType("System.String"));
            c.Columns.Add("Desc", System.Type.GetType("System.String"));
            c.Columns.Add("Qty", System.Type.GetType("System.String"));
            c.Columns.Add("Price", System.Type.GetType("System.String"));
            c.Columns.Add("Total", System.Type.GetType("System.String"));

            foreach (DataRow dr in p.Rows)
            {


                PosDataObject.POS objPOS = new PosDataObject.POS();
                objPOS.Connection = SystemVariables.Conn;
                DataTable dtblN = new DataTable();
                dtblN = objPOS.FetchTrnJournalDetail(GeneralFunctions.fnInt32(dr["TranNo"].ToString()), Settings.TaxInclusive);
                foreach (DataRow dr1 in dtblN.Rows)
                {
                    DataRow r = c.NewRow();
                    r["TranNo"] = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                    r["SKU"] = dr1["SKU"].ToString();
                    r["Desc"] = dr1["Description"].ToString();
                    r["Qty"] = dr1["Qty"].ToString();
                    r["Price"] = dr1["Price"].ToString();
                    r["Total"] = dr1["Total"].ToString();

                    c.Rows.Add(r);
                }

                if (dtblN.Rows.Count > 0)
                {
                    dr["Item"] = "Y";
                }
                else
                {
                    DataRow r = c.NewRow();
                    r["TranNo"] = GeneralFunctions.fnInt32(dr["TranNo"].ToString());
                    r["SKU"] = "";
                    r["Desc"] = "";
                    r["Qty"] = "0.00";
                    r["Price"] = "0.00";
                    r["Total"] = "0.00";

                    c.Rows.Add(r);
                }
            }









            DataSet ds = new DataSet();
            ds.Tables.Add(p);
            ds.Tables.Add(c);

            DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["TranNo"],
            ds.Tables["Child"].Columns["TranNo"]);
            ds.Relations.Add(relation);


            OfflineRetailV2.Report.Sales.repTJD rep = new OfflineRetailV2.Report.Sales.repTJD();
            rep.DataSource = ds;
            rep.rDate.Text = "From " + new DateTime(dtFrom.DateTime.Year, dtFrom.DateTime.Month, dtFrom.DateTime.Day, tmFrom.DateTime.Hour, tmFrom.DateTime.Minute, tmFrom.DateTime.Second).ToString(SystemVariables.DateFormat + " HH:mm") + " " + "to" + " " +
                                                    new DateTime(dtTo.DateTime.Year, dtTo.DateTime.Month, dtTo.DateTime.Day, tmTo.DateTime.Hour, tmTo.DateTime.Minute, tmTo.DateTime.Second).ToString(SystemVariables.DateFormat + " HH:mm");

            rep.rReportHeaderCompany.Text = Settings.ReportHeader;
            
            rep.ShowDetail = chkDetails.IsChecked == true ? true : false;

            rep.DataSource = ds;


            if (TotalTender == 9)
            {
                rep.rHT10.Visible = false;
            }

            if (TotalTender == 9)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;
                // rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF = rep.rHT5.WidthF = rep.rHT6.WidthF = rep.rHT7.WidthF = rep.rHT8.WidthF = rep.rHT9.WidthF = 500/9;
                // rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF = rep.rDT5.WidthF = rep.rDT6.WidthF = rep.rDT7.WidthF = rep.rDT8.WidthF = rep.rDT9.WidthF = 500 / 9;
                // rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF = rep.rTT5.WidthF = rep.rTT6.WidthF = rep.rTT7.WidthF = rep.rTT8.WidthF = rep.rTT9.WidthF = 500 / 9;
            }

            if (TotalTender == 8)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                // rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                //  rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF = rep.rHT5.WidthF = rep.rHT6.WidthF = rep.rHT7.WidthF = rep.rHT8.WidthF = 500 / 8;
                // rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF = rep.rDT5.WidthF = rep.rDT6.WidthF = rep.rDT7.WidthF = rep.rDT8.WidthF = 500 / 8;
                //  rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF = rep.rTT5.WidthF = rep.rTT6.WidthF = rep.rTT7.WidthF = rep.rTT8.WidthF =  500 / 8;
            }

            if (TotalTender == 7)
            {
                float P1 = rep.rH1.WidthF;
                float P2 = rep.rH2.WidthF;
                float P3 = rep.rH3.WidthF;
                float P4 = rep.rH4.WidthF;
                float P5 = rep.rH5.WidthF;
                float P6 = rep.rH6.WidthF;
                float P7 = rep.rH7.WidthF;
                float P8 = rep.rH8.WidthF;


                float P11 = rep.rTill.WidthF;
                float P21 = rep.rEmp.WidthF;
                float P31 = rep.rTranType.WidthF;
                float P41 = rep.rBillNo.WidthF;
                float P51 = rep.rBillDate.WidthF;
                float P61 = rep.rCustomer.WidthF;
                float P71 = rep.rTotalBill.WidthF;
                float P81 = rep.rTotalTax.WidthF;

                float ExtraW = (150) / 15;

                float PrevW = 50;
                rep.BeginUpdate();
                try
                {

                    rep.tHeader.WidthF = 1059;

                    //rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 2;



                    /* rep.rH1.WidthF = 0;
                     rep.rH2.WidthF = 0;
                     rep.rH3.WidthF = 0;
                     rep.rH4.WidthF = 0;
                     rep.rH5.WidthF = 0;
                     rep.rH6.WidthF = 0;
                     rep.rH7.WidthF = 0;
                     rep.rH8.WidthF = 0;

                     rep.rHT1.WidthF = 0;
                     rep.rHT2.WidthF = 0;
                     rep.rHT3.WidthF = 0;
                     rep.rHT4.WidthF = 0;
                     rep.rHT5.WidthF = 0;
                     rep.rHT6.WidthF = 0;
                     rep.rHT7.WidthF = 0;
                     rep.rHT8.WidthF = 0;
                     rep.rHT9.WidthF = 0;
                     rep.rHT10.WidthF = 0;











                     rep.rH1.WidthF = ExtraW + P1;
                     rep.rH2.WidthF = ExtraW + P2;
                     rep.rH3.WidthF = ExtraW + P3;
                     rep.rH4.WidthF = ExtraW + P4;
                     rep.rH5.WidthF = ExtraW + P5;
                     rep.rH6.WidthF = ExtraW + P6;
                     rep.rH7.WidthF = ExtraW + P7;
                     rep.rH8.WidthF = ExtraW + P8;


                     rep.rHT1.WidthF = PrevW + ExtraW;
                     rep.rHT2.WidthF = PrevW + ExtraW;
                     rep.rHT3.WidthF = PrevW + ExtraW;
                     rep.rHT4.WidthF = PrevW + ExtraW;
                     rep.rHT5.WidthF = PrevW + ExtraW;
                     rep.rHT6.WidthF = PrevW + ExtraW;
                     rep.rHT7.WidthF = PrevW + ExtraW;




                     rep.rTill.WidthF = ExtraW + rep.rTill.WidthF;
                     rep.rEmp.WidthF = ExtraW + rep.rEmp.WidthF;
                     rep.rTranType.WidthF = ExtraW + rep.rTranType.WidthF;
                     rep.rBillNo.WidthF = ExtraW + rep.rBillNo.WidthF;
                     rep.rBillDate.WidthF = ExtraW + rep.rBillDate.WidthF;
                     rep.rCustomer.WidthF = ExtraW + rep.rCustomer.WidthF;
                     rep.rTotalBill.WidthF = ExtraW + rep.rTotalBill.WidthF;
                     rep.rTotalTax.WidthF = ExtraW + rep.rTotalTax.WidthF;


                     rep.rTT1.WidthF = PrevW + ExtraW;
                     rep.rTT2.WidthF = PrevW + ExtraW;
                     rep.rTT3.WidthF = PrevW + ExtraW;
                     rep.rTT4.WidthF = PrevW + ExtraW;
                     rep.rTT5.WidthF = PrevW + ExtraW;
                     rep.rTT6.WidthF = PrevW + ExtraW;
                     rep.rTT7.WidthF = PrevW + ExtraW;
                     */
                    // rep.tHeader.ResumeLayout();

                    //rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF = rep.rHT5.WidthF = rep.rHT6.WidthF = rep.rHT7.WidthF = PrevW + ExtraW;
                    //rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF = rep.rDT5.WidthF = rep.rDT6.WidthF = rep.rDT7.WidthF = 494 / 7;
                    //rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF = rep.rTT5.WidthF = rep.rTT6.WidthF = rep.rTT7.WidthF = 494 / 7;

                    rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                    //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 2;

                    rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                    //rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 2;

                    rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                }
                finally
                {
                    rep.EndUpdate();
                }
            }

            if (TotalTender == 6)
            {
                rep.tHeader.WidthF = 1059;

                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                //rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                //rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 0;

                rep.rHT7.Visible = rep.rDT7.Visible = rep.rTT7.Visible = false;
                //rep.rHT7.WidthF = rep.rDT7.WidthF = rep.rTT7.WidthF = 0;

                //rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF = rep.rHT5.WidthF = rep.rHT6.WidthF = 500 / 6;
                //rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF = rep.rDT5.WidthF = rep.rDT6.WidthF = 500 / 6;
                //rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF = rep.rTT5.WidthF = rep.rTT6.WidthF = 500 / 6;
            }

            if (TotalTender == 5)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                //rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                //rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 0;

                rep.rHT7.Visible = rep.rDT7.Visible = rep.rTT7.Visible = false;
                //rep.rHT7.WidthF = rep.rDT7.WidthF = rep.rTT7.WidthF = 0;

                rep.rHT6.Visible = rep.rDT6.Visible = rep.rTT6.Visible = false;
                //rep.rHT6.WidthF = rep.rDT6.WidthF = rep.rTT6.WidthF = 0;

                // rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF = rep.rHT5.WidthF = 500 / 5;
                // rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF = rep.rDT5.WidthF =  500 / 5;
                // rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF = rep.rTT5.WidthF =  500 / 5;
            }

            if (TotalTender == 4)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                // rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                // rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                // rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 0;

                rep.rHT7.Visible = rep.rDT7.Visible = rep.rTT7.Visible = false;
                // rep.rHT7.WidthF = rep.rDT7.WidthF = rep.rTT7.WidthF = 0;

                rep.rHT6.Visible = rep.rDT6.Visible = rep.rTT6.Visible = false;
                //rep.rHT6.WidthF = rep.rDT6.WidthF = rep.rTT6.WidthF = 0;

                rep.rHT5.Visible = rep.rDT5.Visible = rep.rTT5.Visible = false;
                //rep.rHT5.WidthF = rep.rDT5.WidthF = rep.rTT5.WidthF = 0;

                //rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF = rep.rHT4.WidthF =  500 / 4;
                //rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF = rep.rDT4.WidthF =  500 / 4;
                // rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF = rep.rTT4.WidthF =  500 / 4;
            }

            if (TotalTender == 3)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                //rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                // rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 0;

                rep.rHT7.Visible = rep.rDT7.Visible = rep.rTT7.Visible = false;
                // rep.rHT7.WidthF = rep.rDT7.WidthF = rep.rTT7.WidthF = 0;

                rep.rHT6.Visible = rep.rDT6.Visible = rep.rTT6.Visible = false;
                //rep.rHT6.WidthF = rep.rDT6.WidthF = rep.rTT6.WidthF = 0;

                rep.rHT5.Visible = rep.rDT5.Visible = rep.rTT5.Visible = false;
                //rep.rHT5.WidthF = rep.rDT5.WidthF = rep.rTT5.WidthF = 0;

                rep.rHT4.Visible = rep.rDT4.Visible = rep.rTT4.Visible = false;
                //rep.rHT4.WidthF = rep.rDT4.WidthF = rep.rTT4.WidthF = 0;

                //rep.rHT1.WidthF = rep.rHT2.WidthF = rep.rHT3.WidthF =  500 / 3;
                // rep.rDT1.WidthF = rep.rDT2.WidthF = rep.rDT3.WidthF =  500 / 3;
                // rep.rTT1.WidthF = rep.rTT2.WidthF = rep.rTT3.WidthF =  500 / 3;
            }

            if (TotalTender == 2)
            {
                rep.tHeader.WidthF = 1059;
                rep.rHT10.Visible = rep.rDT10.Visible = rep.rTT10.Visible = false;
                //rep.rHT10.WidthF = rep.rDT10.WidthF = rep.rTT10.WidthF = 0;

                rep.rHT9.Visible = rep.rDT9.Visible = rep.rTT9.Visible = false;
                //rep.rHT9.WidthF = rep.rDT9.WidthF = rep.rTT9.WidthF = 0;

                rep.rHT8.Visible = rep.rDT8.Visible = rep.rTT8.Visible = false;
                //rep.rHT8.WidthF = rep.rDT8.WidthF = rep.rTT8.WidthF = 0;

                rep.rHT7.Visible = rep.rDT7.Visible = rep.rTT7.Visible = false;
                //rep.rHT7.WidthF = rep.rDT7.WidthF = rep.rTT7.WidthF = 0;

                rep.rHT6.Visible = rep.rDT6.Visible = rep.rTT6.Visible = false;
                // rep.rHT6.WidthF = rep.rDT6.WidthF = rep.rTT6.WidthF = 0;

                rep.rHT5.Visible = rep.rDT5.Visible = rep.rTT5.Visible = false;
                // rep.rHT5.WidthF = rep.rDT5.WidthF = rep.rTT5.WidthF = 0;

                rep.rHT4.Visible = rep.rDT4.Visible = rep.rTT4.Visible = false;
                //  rep.rHT4.WidthF = rep.rDT4.WidthF = rep.rTT4.WidthF = 0;

                rep.rHT3.Visible = rep.rDT3.Visible = rep.rTT3.Visible = false;
                //  rep.rHT3.WidthF = rep.rDT3.WidthF = rep.rTT3.WidthF = 0;

                //  rep.rHT1.WidthF = rep.rHT2.WidthF =  500 / 2;
                // rep.rDT1.WidthF = rep.rDT2.WidthF =  500 / 2;
                // rep.rTT1.WidthF = rep.rTT2.WidthF =  500 / 2;
            }


            foreach (DataRow dr in dtbl.Rows)
            {
                int tcnt1 = 1;

                foreach (DataColumn dc in dtbl.Columns)
                {
                    if (dc.ColumnName.StartsWith("T_"))
                    {
                        if (tcnt1 == 1)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT1.Text = objPOS.GetTenderName(TID);
                        }
                        if (tcnt1 == 2)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT2.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 3)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT3.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 4)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT4.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 5)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT5.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 6)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT6.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 7)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT7.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 8)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT8.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 9)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT9.Text = objPOS.GetTenderName(TID);
                        }

                        if (tcnt1 == 10)
                        {
                            int TID = GeneralFunctions.fnInt32(dc.Caption.Substring(2));

                            PosDataObject.POS objPOS = new PosDataObject.POS();
                            objPOS.Connection = SystemVariables.Conn;
                            rep.rHT10.Text = objPOS.GetTenderName(TID);
                        }

                        tcnt1++;
                    }
                }


            }



            /*if (cmbCategory.Text == "All")
            {
                rep_LateDetails.rHeader.Text = "Paid In/Out Report";
                
            }
            else
            {
                rep_LateDetails.rHeader.Text = cmbCategory.Text + " Report";
                rep_LateDetails.rGroupRow.Visible = false;
            }


            rep_LateDetails.rAddHeader.Text = "From " + dtFrom.DateTime.ToString(SystemVariables.DateFormat) + " " + "to" + " " +
                                                    dtTo.DateTime.ToString(SystemVariables.DateFormat);

            GeneralFunctions.MakeReportWatermark(rep_LateDetails);
            rep_LateDetails.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_LateDetails.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_LateDetails.Report.DataSource = dtbl;
            rep_LateDetails.GroupHeader1.GroupFields.Add(rep_LateDetails.CreateGroupField("TranType"));
            rep_LateDetails.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;

            rep_LateDetails.grpType.DataBindings.Add("Text", dtbl, "TranType");
            rep_LateDetails.rDate.DataBindings.Add("Text", dtbl, "TranDate");
            rep_LateDetails.rNotes.DataBindings.Add("Text", dtbl, "TranNotes");
            rep_LateDetails.rAmount.DataBindings.Add("Text", dtbl, "TranAmount");
            rep_LateDetails.rStaff.DataBindings.Add("Text", dtbl, "Staff");
            rep_LateDetails.rTotal.DataBindings.Add("Text", dtbl, "TranAmount");*/

            if (eventtype == "Preview")
            {
                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep.PrinterName = Settings.ReportPrinterName;
                    rep.CreateDocument();
                    rep.PrintingSystem.ShowMarginsWarning = false;
                    rep.PrintingSystem.ShowPrintStatusDialog = false;
                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep;
                    window.ShowDialog();

                }
                finally
                {
                    rep.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    //frm_PreviewControl.Dispose();

                    dtbl.Dispose();
                }
            }

            if (eventtype == "Print")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep);
                }
                finally
                {
                    rep.Dispose();
                    dtbl.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep.CreateDocument();
                rep.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    
                    GeneralFunctions.EmailReport(rep, "trnjournal_detailed.pdf", "Transaction Journal Detailed");
                }
                finally
                {
                    rep.Dispose();
                    dtbl.Dispose();
                }
            }

        }


        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ClickButton("Print");
        }

        private void CmbEmployee_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbCategory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void DtFrom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
