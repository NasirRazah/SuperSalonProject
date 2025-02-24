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
    /// Interaction logic for frm_HAStatementDlg.xaml
    /// </summary>
    public partial class frm_HAStatementDlg : Window
    {
        public frm_HAStatementDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private void PopulateCustomer()
        {
            PosDataObject.Customer objDept = new PosDataObject.Customer();
            objDept.Connection = SystemVariables.Conn;
            DataTable dtbl = objDept.FetchLookupData1(Settings.StoreCode);
            DataTable dtblGrp = new DataTable();
            dtblGrp.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("CustomerID", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Customer", System.Type.GetType("System.String"));
            dtblGrp.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dtbl.Rows)
            {
                dtblGrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["CustomerID"].ToString(), dr["Customer"].ToString(), false });
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtblGrp.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }


            grd.ItemsSource = dtblTemp;
            dtbl.Dispose();
            dtblGrp.Dispose();
            dtblTemp.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "House Account Statement";
            chkgrd.IsChecked = false;
            dtF.EditValue = DateTime.Today;
            dtT.EditValue = DateTime.Today;
            PopulateCustomer();
        }


        private void ExecuteStatement(string eventtype)
        {
            if (Settings.ReportHeader == "")
            {
                DocMessage.MsgInformation("Enter Store Details in Settings");
                return;
            }

            DataTable dtblcust = new DataTable();

            dtblcust = grd.ItemsSource as DataTable;
            DataSet ds = new DataSet();
            DataSet dsF = new DataSet();
            DataTable dtblgrp = new DataTable();
            dtblgrp.Columns.Add("HID", System.Type.GetType("System.Int32"));
            dtblgrp.Columns.Add("CustID", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Account", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Amount", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Date", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("OB", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("LP", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("CB", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("OBDate", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("LPDate", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("TranType", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Description", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Qty", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Price", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("ExtPrice", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("TotalSale", System.Type.GetType("System.String"));
            dtblgrp.Columns.Add("Type", System.Type.GetType("System.String"));



            foreach (DataRow drC in dtblcust.Rows)
            {
                int intID = 0;
                if (Convert.ToBoolean(drC["Check"].ToString()))

                {
                    intID = GeneralFunctions.fnInt32(drC["ID"].ToString());
                    DataTable dtbl = new DataTable();
                    PosDataObject.Sales objSales = new PosDataObject.Sales();
                    objSales.Connection = SystemVariables.Conn;
                    dtbl = objSales.FetchCustomerHAHeaderData(intID, GeneralFunctions.fnDate(dtF.EditValue.ToString()), GeneralFunctions.fnDate(dtT.EditValue.ToString()));

                    if (dtbl.Rows.Count == 0)
                    {
                        //DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("No Record found for Printing","frmCardTranRepDlg_msg_NoRecordfoundforPrinting"));
                        //dtbl.Dispose();
                        continue;
                    }

                    DataTable dtblOP = new DataTable();
                    PosDataObject.Sales objSales1 = new PosDataObject.Sales();
                    objSales1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtblOP = objSales1.FetchCustomerHAOpening(intID);
                    string dtOpening = "";
                    string BalOpening = "0";
                    foreach (DataRow dr in dtblOP.Rows)
                    {
                        BalOpening = dr["Amount"].ToString();
                        if (GeneralFunctions.fnDouble(BalOpening) != 0)
                            dtOpening = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                    }
                    dtblOP.Dispose();

                    string CurBal = "0";

                    PosDataObject.POS objPOS = new PosDataObject.POS();
                    objPOS.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    CurBal = Convert.ToString(objPOS.FetchCustomerAcctPayBalance(intID));

                    DataTable dtblLP = new DataTable();
                    PosDataObject.POS objSales2 = new PosDataObject.POS();
                    objSales2.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtblLP = objSales2.FetchCustomerLastAcctPay(intID);
                    string dtLP = "";
                    string BalLP = "0";
                    foreach (DataRow dr in dtblLP.Rows)
                    {
                        BalLP = dr["Amount"].ToString();
                        if (GeneralFunctions.fnDouble(BalLP) != 0)
                            dtLP = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                    }
                    dtblLP.Dispose();

                    OfflineRetailV2.Report.Misc.repHAStatement rep_HAStatement = new OfflineRetailV2.Report.Misc.repHAStatement();

                    GeneralFunctions.MakeReportWatermark(rep_HAStatement);
                    rep_HAStatement.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                    rep_HAStatement.rReportHeader.Text = Settings.ReportHeader_Address;
                    rep_HAStatement.DecimalPlace = Settings.DecimalPlace;

                    DataTable p = new DataTable("Parent");
                    p.Columns.Add("HID", System.Type.GetType("System.Int32"));
                    p.Columns.Add("CustID", System.Type.GetType("System.String"));
                    p.Columns.Add("Account", System.Type.GetType("System.String"));
                    p.Columns.Add("Amount", System.Type.GetType("System.String"));
                    p.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                    p.Columns.Add("Date", System.Type.GetType("System.String"));
                    p.Columns.Add("OB", System.Type.GetType("System.String"));
                    p.Columns.Add("LP", System.Type.GetType("System.String"));
                    p.Columns.Add("CB", System.Type.GetType("System.String"));
                    p.Columns.Add("OBDate", System.Type.GetType("System.String"));
                    p.Columns.Add("LPDate", System.Type.GetType("System.String"));
                    p.Columns.Add("TranType", System.Type.GetType("System.String"));

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        string strAcct = "";
                        if (dr["Company"].ToString().Trim() != "")
                        {
                            strAcct = dr["Company"].ToString().Trim();
                        }
                        if (dr["Customer"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Customer"].ToString().Trim();
                            }
                            else strAcct = dr["Customer"].ToString().Trim();
                        }

                        if (dr["Address1"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Address1"].ToString().Trim();
                            }
                            else strAcct = dr["Address1"].ToString().Trim();
                        }

                        if (dr["Address2"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + dr["Address2"].ToString().Trim();
                            }
                            else strAcct = dr["Address2"].ToString().Trim();
                        }
                        string CSZ = "";
                        if (dr["City"].ToString().Trim() != "") CSZ = dr["City"].ToString().Trim();
                        if (dr["State"].ToString().Trim() != "")
                        {
                            if (CSZ != "")
                            {
                                CSZ = CSZ + ", " + dr["State"].ToString().Trim() + "  ";
                            }
                            else CSZ = dr["State"].ToString().Trim() + "  ";
                        }

                        if (dr["Zip"].ToString().Trim() != "")
                        {
                            if (CSZ != "")
                            {
                                CSZ = CSZ + dr["Zip"].ToString().Trim();
                            }
                            else CSZ = dr["Zip"].ToString().Trim();
                        }
                        if (CSZ != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + CSZ;
                            }
                            else strAcct = CSZ;
                        }

                        if (dr["Email"].ToString().Trim() != "")
                        {
                            if (strAcct != "")
                            {
                                strAcct = strAcct + "\n" + "Email: " + dr["Email"].ToString().Trim();
                            }
                            else strAcct = "Email: " + dr["Email"].ToString().Trim();
                        }

                        DataRow r1 = p.NewRow();
                        r1["HID"] = dr["HID"].ToString();
                        r1["CustID"] = dr["CustID"].ToString();
                        r1["Account"] = strAcct;
                        r1["Amount"] = dr["Amount"].ToString();
                        r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                        r1["Date"] = GeneralFunctions.fnDate(dr["Date"].ToString()).ToString(SystemVariables.DateFormat);
                        r1["OB"] = BalOpening;
                        r1["LP"] = BalLP;
                        r1["CB"] = CurBal;
                        r1["OBDate"] = dtOpening;
                        r1["LPDate"] = dtLP;
                        r1["TranType"] = dr["TranType"].ToString();
                        p.Rows.Add(r1);
                    }

                    DataTable dtbl1 = new DataTable();
                    PosDataObject.Sales objProduct1 = new PosDataObject.Sales();
                    objProduct1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                    dtbl1 = objProduct1.FetchCustomerHADetailData(intID, GeneralFunctions.fnDate(dtF.EditValue.ToString()), GeneralFunctions.fnDate(dtT.EditValue.ToString()));

                    DataTable c = new DataTable("Child");
                    c.Columns.Add("HID", System.Type.GetType("System.Int32"));
                    c.Columns.Add("CustID", System.Type.GetType("System.String"));
                    c.Columns.Add("Account", System.Type.GetType("System.String"));
                    c.Columns.Add("Amount", System.Type.GetType("System.String"));
                    c.Columns.Add("InvoiceNo", System.Type.GetType("System.String"));
                    c.Columns.Add("Date", System.Type.GetType("System.String"));
                    c.Columns.Add("OB", System.Type.GetType("System.String"));
                    c.Columns.Add("LP", System.Type.GetType("System.String"));
                    c.Columns.Add("CB", System.Type.GetType("System.String"));
                    c.Columns.Add("OBDate", System.Type.GetType("System.String"));
                    c.Columns.Add("LPDate", System.Type.GetType("System.String"));
                    c.Columns.Add("TranType", System.Type.GetType("System.String"));
                    c.Columns.Add("SKU", System.Type.GetType("System.String"));
                    c.Columns.Add("Description", System.Type.GetType("System.String"));
                    c.Columns.Add("Qty", System.Type.GetType("System.String"));
                    c.Columns.Add("Price", System.Type.GetType("System.String"));
                    c.Columns.Add("ExtPrice", System.Type.GetType("System.String"));
                    c.Columns.Add("TotalSale", System.Type.GetType("System.String"));
                    c.Columns.Add("Type", System.Type.GetType("System.String"));


                    foreach (DataRow dr in dtbl1.Rows)
                    {
                        double crgamt = 0;

                        DataRow r1 = c.NewRow();
                        string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                        foreach (DataRow dr1 in p.Rows)
                        {

                            if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                            {
                                a1 = dr1["CustID"].ToString();
                                a2 = dr1["Account"].ToString();
                                a3 = dr1["Amount"].ToString();
                                a4 = dr1["Date"].ToString();
                                a5 = dr1["OB"].ToString();
                                a6 = dr1["LP"].ToString();
                                a7 = dr1["CB"].ToString();
                                a8 = dr1["OBDate"].ToString();
                                a9 = dr1["LPDate"].ToString();
                                a10 = dr1["TranType"].ToString();
                                a11 = dr1["HID"].ToString();
                                break;
                            }
                        }

                        crgamt = GeneralFunctions.fnDouble(a3);

                        r1["CustID"] = a1;
                        r1["Account"] = a2;
                        r1["Amount"] = crgamt.ToString();
                        r1["InvoiceNo"] = dr["InvoiceNo"].ToString();
                        r1["Date"] = a4;
                        r1["OB"] = a5;
                        r1["LP"] = a6;
                        r1["CB"] = a7;
                        r1["OBDate"] = a8;
                        r1["LPDate"] = a9;

                        r1["SKU"] = dr["SKU"].ToString();
                        r1["Description"] = dr["Description"].ToString();
                        r1["Qty"] = dr["Qty"].ToString();
                        r1["Price"] = dr["Price"].ToString();
                        r1["ExtPrice"] = dr["ExtPrice"].ToString();
                        r1["TotalSale"] = dr["TotalSale"].ToString();
                        r1["Type"] = dr["ProductType"].ToString() == "W" ? "W" : "N"; // comment // "N";
                        r1["HID"] = a11;
                        c.Rows.Add(r1);

                        dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r1["CustID"].ToString(),
                                                    r1["Account"].ToString(),
                                                    r1["Amount"].ToString(),
                                                    r1["InvoiceNo"].ToString(),
                                                    r1["Date"].ToString(),
                                                    r1["OB"].ToString(),
                                                    r1["LP"].ToString(),
                                                    r1["CB"].ToString(),
                                                    r1["OBDate"].ToString(),
                                                    r1["LPDate"].ToString(),
                                                    a10,
                                                    r1["SKU"].ToString(),
                                                    r1["Description"].ToString(),
                                                    r1["Qty"].ToString(),
                                                    r1["Price"].ToString(),
                                                    r1["ExtPrice"].ToString(),
                                                    r1["TotalSale"].ToString(),
                                                    r1["Type"].ToString()});

                    }


                    string previnv = "";
                    foreach (DataRow dr in dtbl1.Rows)
                    {

                        double crgamt = 0;

                        DataRow r1 = c.NewRow();
                        string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                        foreach (DataRow dr1 in p.Rows)
                        {

                            if (dr["InvoiceNo"].ToString() == dr1["InvoiceNo"].ToString())
                            {
                                a1 = dr1["CustID"].ToString();
                                a2 = dr1["Account"].ToString();
                                a3 = dr1["Amount"].ToString();
                                a4 = dr1["Date"].ToString();
                                a5 = dr1["OB"].ToString();
                                a6 = dr1["LP"].ToString();
                                a7 = dr1["CB"].ToString();
                                a8 = dr1["OBDate"].ToString();
                                a9 = dr1["LPDate"].ToString();
                                a10 = dr1["TranType"].ToString();
                                a11 = dr1["HID"].ToString();
                                break;
                            }
                        }

                        crgamt = GeneralFunctions.fnDouble(a3);

                        if ((previnv == "") || (previnv != dr["InvoiceNo"].ToString()))
                        {
                            previnv = dr["InvoiceNo"].ToString();
                            if (GeneralFunctions.fnDouble(dr["Tax1"].ToString()) != 0)
                            {

                                DataRow r2 = c.NewRow();

                                r2["CustID"] = a1;
                                r2["Account"] = a2;
                                r2["Amount"] = crgamt.ToString();
                                r2["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r2["Date"] = a4;
                                r2["OB"] = a5;
                                r2["LP"] = a6;
                                r2["CB"] = a7;
                                r2["OBDate"] = a8;
                                r2["LPDate"] = a9;
                                r2["SKU"] = "";
                                r2["Description"] = dr["TaxName1"].ToString();
                                r2["Qty"] = "0";
                                r2["Price"] = "0";
                                r2["ExtPrice"] = dr["Tax1"].ToString();
                                r2["TotalSale"] = dr["TotalSale"].ToString();
                                r2["Type"] = "A";
                                r2["HID"] = a11;
                                c.Rows.Add(r2);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r2["CustID"].ToString(),
                                                    r2["Account"].ToString(),
                                                    r2["Amount"].ToString(),
                                                    r2["InvoiceNo"].ToString(),
                                                    r2["Date"].ToString(),
                                                    r2["OB"].ToString(),
                                                    r2["LP"].ToString(),
                                                    r2["CB"].ToString(),
                                                    r2["OBDate"].ToString(),
                                                    r2["LPDate"].ToString(),
                                                    a10,
                                                    r2["SKU"].ToString(),
                                                    r2["Description"].ToString(),
                                                    r2["Qty"].ToString(),
                                                    r2["Price"].ToString(),
                                                    r2["ExtPrice"].ToString(),
                                                    r2["TotalSale"].ToString(),
                                                    r2["Type"].ToString()});
                            }

                            if (GeneralFunctions.fnDouble(dr["Tax2"].ToString()) != 0)
                            {

                                DataRow r3 = c.NewRow();

                                r3["CustID"] = a1;
                                r3["Account"] = a2;
                                r3["Amount"] = crgamt.ToString();
                                r3["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r3["Date"] = a4;
                                r3["OB"] = a5;
                                r3["LP"] = a6;
                                r3["CB"] = a7;
                                r3["OBDate"] = a8;
                                r3["LPDate"] = a9;
                                r3["SKU"] = "";
                                r3["Description"] = dr["TaxName2"].ToString();
                                r3["Qty"] = "0";
                                r3["Price"] = "0";
                                r3["ExtPrice"] = dr["Tax2"].ToString();
                                r3["TotalSale"] = dr["TotalSale"].ToString();
                                r3["Type"] = "A";
                                r3["HID"] = a11;
                                c.Rows.Add(r3);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r3["CustID"].ToString(),
                                                    r3["Account"].ToString(),
                                                    r3["Amount"].ToString(),
                                                    r3["InvoiceNo"].ToString(),
                                                    r3["Date"].ToString(),
                                                    r3["OB"].ToString(),
                                                    r3["LP"].ToString(),
                                                    r3["CB"].ToString(),
                                                    r3["OBDate"].ToString(),
                                                    r3["LPDate"].ToString(),
                                                    a10,
                                                    r3["SKU"].ToString(),
                                                    r3["Description"].ToString(),
                                                    r3["Qty"].ToString(),
                                                    r3["Price"].ToString(),
                                                    r3["ExtPrice"].ToString(),
                                                    r3["TotalSale"].ToString(),
                                                    r3["Type"].ToString()});

                            }

                            if (GeneralFunctions.fnDouble(dr["Tax3"].ToString()) != 0)
                            {

                                DataRow r4 = c.NewRow();

                                r4["CustID"] = a1;
                                r4["Account"] = a2;
                                r4["Amount"] = crgamt.ToString();
                                r4["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r4["Date"] = a4;
                                r4["OB"] = a5;
                                r4["LP"] = a6;
                                r4["CB"] = a7;
                                r4["OBDate"] = a8;
                                r4["LPDate"] = a9;
                                r4["SKU"] = "";
                                r4["Description"] = dr["TaxName3"].ToString();
                                r4["Qty"] = "0";
                                r4["Price"] = "0";
                                r4["ExtPrice"] = dr["Tax3"].ToString();
                                r4["TotalSale"] = dr["TotalSale"].ToString();
                                r4["Type"] = "A";
                                r4["HID"] = a11;
                                c.Rows.Add(r4);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r4["CustID"].ToString(),
                                                    r4["Account"].ToString(),
                                                    r4["Amount"].ToString(),
                                                    r4["InvoiceNo"].ToString(),
                                                    r4["Date"].ToString(),
                                                    r4["OB"].ToString(),
                                                    r4["LP"].ToString(),
                                                    r4["CB"].ToString(),
                                                    r4["OBDate"].ToString(),
                                                    r4["LPDate"].ToString(),
                                                    a10,
                                                    r4["SKU"].ToString(),
                                                    r4["Description"].ToString(),
                                                    r4["Qty"].ToString(),
                                                    r4["Price"].ToString(),
                                                    r4["ExtPrice"].ToString(),
                                                    r4["TotalSale"].ToString(),
                                                    r4["Type"].ToString()});
                            }

                            if (GeneralFunctions.fnDouble(dr["Discount"].ToString()) != 0)
                            {
                                DataRow r5 = c.NewRow();
                                r5["CustID"] = a1;
                                r5["Account"] = a2;
                                r5["Amount"] = crgamt.ToString();
                                r5["InvoiceNo"] = dr["InvoiceNo"].ToString();
                                r5["Date"] = a4;
                                r5["OB"] = a5;
                                r5["LP"] = a6;
                                r5["CB"] = a7;
                                r5["OBDate"] = a8;
                                r5["LPDate"] = a9;
                                r5["SKU"] = "";
                                if (dr["DiscountReason"].ToString() != "")
                                    r5["Description"] = "Discount : " + dr["DiscountReason"].ToString();
                                else
                                    r5["Description"] = "Discount : ";
                                r5["Qty"] = "0";
                                r5["Price"] = "0";
                                r5["ExtPrice"] = dr["Discount"].ToString();
                                r5["TotalSale"] = dr["TotalSale"].ToString();
                                r5["Type"] = "A";
                                r5["HID"] = a11;
                                c.Rows.Add(r5);

                                dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r5["CustID"].ToString(),
                                                    r5["Account"].ToString(),
                                                    r5["Amount"].ToString(),
                                                    r5["InvoiceNo"].ToString(),
                                                    r5["Date"].ToString(),
                                                    r5["OB"].ToString(),
                                                    r5["LP"].ToString(),
                                                    r5["CB"].ToString(),
                                                    r5["OBDate"].ToString(),
                                                    r5["LPDate"].ToString(),
                                                    a10,
                                                    r5["SKU"].ToString(),
                                                    r5["Description"].ToString(),
                                                    r5["Qty"].ToString(),
                                                    r5["Price"].ToString(),
                                                    r5["ExtPrice"].ToString(),
                                                    r5["TotalSale"].ToString(),
                                                    r5["Type"].ToString()});
                            }
                        }

                    }


                    foreach (DataRow dr1 in p.Rows)
                    {

                        if ((dr1["InvoiceNo"].ToString() == "0") && (dr1["TranType"].ToString() == "1"))
                        {
                            DataRow r6 = c.NewRow();
                            string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "", a11 = "";
                            a1 = dr1["CustID"].ToString();
                            a2 = dr1["Account"].ToString();
                            a3 = dr1["Amount"].ToString();
                            a4 = dr1["Date"].ToString();
                            a5 = dr1["OB"].ToString();
                            a6 = dr1["LP"].ToString();
                            a7 = dr1["CB"].ToString();
                            a8 = dr1["OBDate"].ToString();
                            a9 = dr1["LPDate"].ToString();
                            a10 = dr1["TranType"].ToString();
                            a11 = dr1["HID"].ToString();

                            r6["HID"] = a11;
                            r6["CustID"] = a1;
                            r6["Account"] = a2;
                            r6["Amount"] = a3;
                            r6["InvoiceNo"] = "";
                            r6["Date"] = a4;
                            r6["OB"] = a5;
                            r6["LP"] = a6;
                            r6["CB"] = a7;
                            r6["OBDate"] = a8;
                            r6["LPDate"] = a9;
                            r6["SKU"] = "Adjustment";
                            r6["Description"] = "";
                            r6["Qty"] = "";
                            r6["Price"] = "";
                            r6["ExtPrice"] = a3;
                            r6["TotalSale"] = a3;
                            r6["Type"] = "A";
                            c.Rows.Add(r6);

                            dtblgrp.Rows.Add(new object[] {
                                                    a11,
                                                    r6["CustID"].ToString(),
                                                    r6["Account"].ToString(),
                                                    r6["Amount"].ToString(),
                                                    r6["InvoiceNo"].ToString(),
                                                    r6["Date"].ToString(),
                                                    r6["OB"].ToString(),
                                                    r6["LP"].ToString(),
                                                    r6["CB"].ToString(),
                                                    r6["OBDate"].ToString(),
                                                    r6["LPDate"].ToString(),
                                                    a10,
                                                    r6["SKU"].ToString(),
                                                    r6["Description"].ToString(),
                                                    r6["Qty"].ToString(),
                                                    r6["Price"].ToString(),
                                                    r6["ExtPrice"].ToString(),
                                                    r6["TotalSale"].ToString(),
                                                    r6["Type"].ToString()});
                        }
                    }


                    //ds.Tables.Add(p);
                    //ds.Tables.Add(c);


                }





            }





            if (dtblgrp.Rows.Count == 0)
            {
                DocMessage.MsgInformation("No Record Found");
                dtblgrp.Dispose();
                return;
            }

            /*DataRelation relation = new DataRelation("ParentChild",
            ds.Tables["Parent"].Columns["InvoiceNo"],
            ds.Tables["Child"].Columns["InvoiceNo"]);
            ds.Relations.Add(relation);*/
            //relation.Nested = true;

            //rep_HAStatement.GroupHeader1.GroupFields.Add(rep_HAStatement.CreateGroupField("Parent.CustID"));
            OfflineRetailV2.Report.Misc.repHAGroupStatement rep_HAGroupStatement = new OfflineRetailV2.Report.Misc.repHAGroupStatement();
            GeneralFunctions.MakeReportWatermark(rep_HAGroupStatement);
            rep_HAGroupStatement.Report.DataSource = dtblgrp;
            rep_HAGroupStatement.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
            rep_HAGroupStatement.rReportHeader.Text = Settings.ReportHeader_Address;
            rep_HAGroupStatement.rHDate.Text = "from" + " " + dtF.Text + " " + "to" + " " + dtT.Text;
            rep_HAGroupStatement.GroupHeader3.GroupFields.Add(rep_HAGroupStatement.CreateGroupField("CustID"));

            rep_HAGroupStatement.rAccount.DataBindings.Add("Text", dtblgrp, "CustID");
            rep_HAGroupStatement.rAddress.DataBindings.Add("Text", dtblgrp, "Account");


            rep_HAGroupStatement.rOBDate.DataBindings.Add("Text", dtblgrp, "OBDate");
            rep_HAGroupStatement.rOBAmt.DataBindings.Add("Text", dtblgrp, "OB");
            rep_HAGroupStatement.rLPDate.DataBindings.Add("Text", dtblgrp, "LPDate");
            rep_HAGroupStatement.rLPAmt.DataBindings.Add("Text", dtblgrp, "LP");
            rep_HAGroupStatement.rCBAmt.DataBindings.Add("Text", dtblgrp, "CB");

            rep_HAGroupStatement.GroupHeader2.GroupFields.Add(rep_HAGroupStatement.CreateGroupField("HID"));
            rep_HAGroupStatement.GroupHeader2.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;
            rep_HAGroupStatement.rInvoice.DataBindings.Add("Text", dtblgrp, "InvoiceNo");
            rep_HAGroupStatement.rDate.DataBindings.Add("Text", dtblgrp, "Date");

            rep_HAGroupStatement.rID.DataBindings.Add("Text", dtblgrp, "Type");
            rep_HAGroupStatement.rSKU.DataBindings.Add("Text", dtblgrp, "SKU");
            rep_HAGroupStatement.rProduct.DataBindings.Add("Text", dtblgrp, "Description");
            rep_HAGroupStatement.rQty.DataBindings.Add("Text", dtblgrp, "Qty");
            rep_HAGroupStatement.rPrice.DataBindings.Add("Text", dtblgrp, "Price");
            rep_HAGroupStatement.rExtPrice.DataBindings.Add("Text", dtblgrp, "ExtPrice");

            rep_HAGroupStatement.rTot1.DataBindings.Add("Text", dtblgrp, "TotalSale");
            rep_HAGroupStatement.rTot2.DataBindings.Add("Text", dtblgrp, "Amount");
            rep_HAGroupStatement.rCB.DataBindings.Add("Text", dtblgrp, "CB");

            if (eventtype == "Preview")
            {

                //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                try
                {
                    if (Settings.ReportPrinterName != "") rep_HAGroupStatement.PrinterName = Settings.ReportPrinterName;
                    rep_HAGroupStatement.CreateDocument();
                    rep_HAGroupStatement.PrintingSystem.ShowMarginsWarning = false;
                    rep_HAGroupStatement.PrintingSystem.ShowPrintStatusDialog = false;

                    //rep_HAGroupStatement.ShowPreviewDialog();

                    DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                    window.PreviewControl.DocumentSource = rep_HAGroupStatement;
                    window.ShowDialog();
                }
                finally
                {
                    rep_HAGroupStatement.Dispose();
                    //frm_PreviewControl.dv.DocumentSource = null;
                    // frm_PreviewControl.Dispose();
                    dtblgrp.Dispose();
                }

            }

            if (eventtype == "Print")
            {
                rep_HAGroupStatement.CreateDocument();
                rep_HAGroupStatement.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    GeneralFunctions.PrintReport(rep_HAGroupStatement);
                }
                finally
                {
                    rep_HAGroupStatement.Dispose();
                    dtblgrp.Dispose();
                }
            }


            if (eventtype == "Email")
            {
                rep_HAGroupStatement.CreateDocument();
                rep_HAGroupStatement.PrintingSystem.ShowMarginsWarning = false;
                try
                {
                    string attachfile = "";
                    attachfile = "cust_house_ac.pdf";
                    GeneralFunctions.EmailReport(rep_HAGroupStatement, attachfile, "Customer House Accounr Statement");
                }
                finally
                {
                    rep_HAGroupStatement.Dispose();
                    dtblgrp.Dispose();
                }
            }
        }

        private void ClickButton(string eventtype)
        {
            Cursor = Cursors.Wait;
            try
            {
                ExecuteStatement(eventtype);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            ExecuteStatement("Email");
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            ExecuteStatement("Preview");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            ExecuteStatement("Print");
        }

        private void Chkgrd_Checked(object sender, RoutedEventArgs e)
        {
            if (chkgrd.IsChecked == true)
            {
                chkgrd.Content = "Uncheck All";
                DataTable dtbl = grd.ItemsSource as DataTable;
                foreach (DataRow dr in dtbl.Rows)
                {
                    dr["Check"] = true;
                }
                grd.ItemsSource = dtbl;
                dtbl.Dispose();
            }
            else
            {
                chkgrd.Content = "Check All";
                DataTable dtbl1 = grd.ItemsSource as DataTable;
                foreach (DataRow dr1 in dtbl1.Rows)
                {
                    dr1["Check"] = false;
                }
                grd.ItemsSource = dtbl1;
                dtbl1.Dispose();
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
    }
}
