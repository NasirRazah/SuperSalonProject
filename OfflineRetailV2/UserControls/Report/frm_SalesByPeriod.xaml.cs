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
    /// Interaction logic for frm_SalesByPeriod.xaml
    /// </summary>
    public partial class frm_SalesByPeriod : Window
    {
        public frm_SalesByPeriod()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        public void PopulateCategory()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbCategory.ItemsSource = dtblTemp;
            cmbCategory.Text = "";
            dbtblCat.Dispose();
        }

        public void PopulateDept()
        {
            PosDataObject.Department objDepartment = new PosDataObject.Department();
            objDepartment.Connection = SystemVariables.Conn;
            DataTable dbtblDept = new DataTable();
            dbtblDept = objDepartment.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblDept.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbDept.ItemsSource = dtblTemp;
            
            cmbDept.Text = "";
            dbtblDept.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Sales by Period";
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;

            PopulateCategory();
            PopulateDept();
            cmbCategory.Visibility = cmbDept.Visibility = Visibility.Collapsed;
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

        private void ChkD_Checked(object sender, RoutedEventArgs e)
        {
            cmbDept.Visibility = chkD.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            if (chkD.IsChecked == false) cmbDept.EditValue = "0";
            chkC.IsEnabled = chkD.IsChecked == false;
        }

        private void ChkC_Checked(object sender, RoutedEventArgs e)
        {
            cmbCategory.Visibility = chkC.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            if (chkC.IsChecked == false) cmbCategory.EditValue = "0";
            chkD.IsEnabled = chkC.IsChecked == false;
        }

        private void ClickButton(string EventType)
        {
            if ((dtFrom.EditValue == null) || (dtTo.EditValue == null)) return;

            ExecuteReport(EventType);
        }

        private void ExecuteReport(string eventtype)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                string moreF = "";
                int moreFid = 0;

                if (chkD.IsChecked == true)
                {
                    moreF = "Department";
                    moreFid = GeneralFunctions.fnInt32(cmbDept.EditValue);
                }

                if (chkC.IsChecked == true)
                {
                    moreF = "POS Screen Category";
                    moreFid = GeneralFunctions.fnInt32(cmbCategory.EditValue);
                }

                int iPeriod = 0;
                if (rg1.IsChecked == true) iPeriod = 0;
                if (rg2.IsChecked == true) iPeriod = 1;
                if (rg3.IsChecked == true) iPeriod = 2;
                if (rg4.IsChecked == true) iPeriod = 3;
                if (rg5.IsChecked == true) iPeriod = 4;

                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;
                dtbl = objSales.FetchSaleByPeriodReportData(iPeriod, moreF, moreFid, GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.TaxInclusive);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                DataTable repdtbl = new DataTable();

                repdtbl.Columns.Add("Group2", System.Type.GetType("System.String"));

                repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Group1", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));

                double dblRev = 0;
                double dblCost = 0;
                double dblDCost = 0;
                double dblPRev = 0;
                double dblPCost = 0;
                double dblPDCost = 0;
                double dblProfit = 0;
                double dblMargin = 0;
                double dblPQty = 0;
                double dblRD = 0;

                int iSL = 1;

                foreach (DataRow dr in dtbl.Rows)
                {
                    bool blExists = false;
                    foreach (DataRow dr1 in repdtbl.Rows)
                    {
                        if ((dr1["Group2"].ToString() == dr["Group2"].ToString()) &&
                            (dr1["Group1"].ToString() == dr["Group1"].ToString()))
                        {
                            blExists = true;
                            break;
                        }
                    }


                    if (!blExists)
                    {
                        bool fSL = false;
                        foreach (DataRow dr2 in repdtbl.Rows)
                        {
                            if (dr2["Group1"].ToString() == dr["Group1"].ToString())
                            {
                                fSL = true;
                                break;
                            }
                        }

                        if (!fSL) iSL++;

                        dblRev = 0;
                        dblCost = 0;
                        dblDCost = 0;
                        dblRev = GeneralFunctions.fnDouble(dr["Price"].ToString());
                        dblCost = GeneralFunctions.fnDouble(dr["Cost"].ToString());
                        dblDCost = GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());


                        repdtbl.Rows.Add(new object[] {dr["Group2"].ToString(),
                                                   dblRev,
                                                   dblCost,
                                                   dblDCost,
                                                   0,0,
                                                   dr["Group1"].ToString(),iSL});
                    }
                    else
                    {
                        foreach (DataRow dr2 in repdtbl.Rows)
                        {
                            if ((dr2["Group2"].ToString() == dr["Group2"].ToString()) &&
                                (dr2["Group1"].ToString() == dr["Group1"].ToString()))
                            {
                                dblRev = 0;
                                dblCost = 0;
                                dblDCost = 0;
                                dblRev = GeneralFunctions.fnDouble(dr["Price"].ToString());
                                dblCost = GeneralFunctions.fnDouble(dr["Cost"].ToString());
                                dblDCost = GeneralFunctions.fnDouble(dr["DiscountedCost"].ToString());

                                dblPRev = GeneralFunctions.fnDouble(dr2["Revenue"].ToString()) + dblRev;
                                dblPCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString()) + dblCost;
                                dblPDCost = GeneralFunctions.fnDouble(dr2["DiscountedCost"].ToString()) + dblDCost;
                                dr2["Revenue"] = dblPRev;
                                dr2["Cost"] = dblPCost;
                                dr2["DiscountedCost"] = dblPDCost;
                            }
                        }
                    }
                }

                foreach (DataRow dr3 in repdtbl.Rows)
                {
                    dblProfit = 0;
                    dblMargin = 0;
                    if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = -99999;
                    }
                    else
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                    }

                    dr3["Profit"] = dblProfit;
                    dr3["Margin"] = dblMargin;
                }



                /*
                string fsort1 = "";

                if (lkupGroupBy.EditValue.ToString() != "0")
                {
                    if (cmbsort.SelectedIndex == 0) if (rgsort1.SelectedIndex == 0) fsort1 = "SKU asc"; else fsort1 = "SKU desc";
                    if (cmbsort.SelectedIndex == 1) if (rgsort1.SelectedIndex == 0) fsort1 = "Description asc"; else fsort1 = "Description desc";
                    if (cmbsort.SelectedIndex == 2) if (rgsort1.SelectedIndex == 0) fsort1 = "Brand asc"; else fsort1 = "Brand desc";
                    if (cmbsort.SelectedIndex == 3) if (rgsort1.SelectedIndex == 0) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
                    if (cmbsort.SelectedIndex == 4) if (rgsort1.SelectedIndex == 0) fsort1 = "Season asc"; else fsort1 = "Season desc";
                    if (cmbsort.SelectedIndex == 5) if (rgsort1.SelectedIndex == 0) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                    if (cmbsort.SelectedIndex == 6) if (rgsort1.SelectedIndex == 0) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                    if (cmbsort.SelectedIndex == 7) if (rgsort1.SelectedIndex == 0) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                    if (cmbsort.SelectedIndex == 8) if (rgsort1.SelectedIndex == 0) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                    if (cmbsort.SelectedIndex == 9) if (rgsort1.SelectedIndex == 0) fsort1 = "Margin asc"; else fsort1 = "Margin desc";
                }
                else
                {
                    if (cmbsort.SelectedIndex == 0) if (rgsort1.SelectedIndex == 0) fsort1 = "Description asc"; else fsort1 = "Description desc";
                    if (cmbsort.SelectedIndex == 1) if (rgsort1.SelectedIndex == 0) fsort1 = "Brand asc"; else fsort1 = "Brand desc";
                    if (cmbsort.SelectedIndex == 2) if (rgsort1.SelectedIndex == 0) fsort1 = "UPC asc"; else fsort1 = "UPC desc";
                    if (cmbsort.SelectedIndex == 3) if (rgsort1.SelectedIndex == 0) fsort1 = "Season asc"; else fsort1 = "Season desc";
                    if (cmbsort.SelectedIndex == 4) if (rgsort1.SelectedIndex == 0) fsort1 = "QtySold asc"; else fsort1 = "QtySold desc";
                    if (cmbsort.SelectedIndex == 5) if (rgsort1.SelectedIndex == 0) fsort1 = "Cost asc"; else fsort1 = "Cost desc";
                    if (cmbsort.SelectedIndex == 6) if (rgsort1.SelectedIndex == 0) fsort1 = "Revenue asc"; else fsort1 = "Revenue desc";
                    if (cmbsort.SelectedIndex == 7) if (rgsort1.SelectedIndex == 0) fsort1 = "Profit asc"; else fsort1 = "Profit desc";
                    if (cmbsort.SelectedIndex == 8) if (rgsort1.SelectedIndex == 0) fsort1 = "Margin asc"; else fsort1 = "Margin desc";
                }
                repdtbl.DefaultView.Sort = fsort1;
                repdtbl.DefaultView.ApplyDefaultSort = true;*/

                DataTable frepdtbl = new DataTable();

                frepdtbl.Columns.Add("Group2", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("DiscountedCost", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Group1", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));
                frepdtbl.Columns.Add("GroupMargin", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("TotalMargin", System.Type.GetType("System.Double"));


                foreach (DataRow dr in repdtbl.Rows)
                {
                    frepdtbl.Rows.Add(new object[] {
                                                     dr["Group2"].ToString(),
                                                     dr["Revenue"].ToString(),
                                                     dr["Cost"].ToString(),
                                                     dr["DiscountedCost"].ToString(),
                                                     dr["Profit"].ToString(),
                                                     dr["Margin"].ToString(),
                                                     dr["Group1"].ToString(),
                                                     GeneralFunctions.fnInt32(dr["GroupSortOrder"].ToString()),
                                                     0,0});
                }


                DataTable dtblMargin = new DataTable();
                dtblMargin.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));
                dtblMargin.Columns.Add("Margin", System.Type.GetType("System.Double"));
                dtblMargin.Columns.Add("TMargin", System.Type.GetType("System.Double"));

                DataTable dClone = frepdtbl.DefaultView.ToTable(true, "GroupSortOrder");


                double cost = 0;
                double rev = 0;

                foreach (DataRow dr1 in dClone.Rows)
                {
                    double tcost = 0;
                    double trev = 0;

                    foreach (DataRow dr in frepdtbl.Rows)
                    {

                        if (dr1["GroupSortOrder"].ToString() == dr["GroupSortOrder"].ToString())
                        {

                            tcost = tcost + GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            trev = trev + GeneralFunctions.fnDouble(dr["Revenue"].ToString());
                        }
                    }

                    cost = cost + tcost;
                    rev = rev + trev;

                    dblProfit = 0;
                    dblMargin = 0;

                    if (trev == 0)
                    {
                        dblProfit = trev - tcost;
                        dblMargin = -99999;
                    }
                    else
                    {
                        dblProfit = trev - tcost;
                        dblMargin = dblProfit * 100 / trev;
                    }

                    dtblMargin.Rows.Add(new object[] { dr1["GroupSortOrder"].ToString(), dblMargin, 0 });
                }

                dblProfit = 0;
                dblMargin = 0;

                if (rev == 0)
                {
                    dblProfit = rev - cost;
                    dblMargin = -99999;
                }
                else
                {
                    dblProfit = rev - cost;
                    dblMargin = dblProfit * 100 / rev;
                }


                foreach (DataRow dr in dtblMargin.Rows)
                {
                    dr["TMargin"] = dblMargin;
                }

                foreach (DataRow dr in dtblMargin.Rows)
                {
                    foreach (DataRow dr1 in frepdtbl.Rows)
                    {
                        if (dr1["GroupSortOrder"].ToString() == dr["GroupSortOrder"].ToString())
                        {
                            dr1["GroupMargin"] = GeneralFunctions.fnDouble(dr["Margin"].ToString());
                            dr1["TotalMargin"] = GeneralFunctions.fnDouble(dr["TMargin"].ToString());
                        }
                    }
                }

                OfflineRetailV2.Report.Sales.repSales_Period2 rep_Sales2 = new OfflineRetailV2.Report.Sales.repSales_Period2();
                OfflineRetailV2.Report.Sales.repSales_Period1 rep_Sales1 = new OfflineRetailV2.Report.Sales.repSales_Period1();

                rep_Sales1.Report.DataSource = frepdtbl;
                GeneralFunctions.MakeReportWatermark(rep_Sales1);
                rep_Sales1.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales1.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales1.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");
                rep_Sales1.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Sales1.rTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTot6.Summary.FormatString = "{0:0.000}";

                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Sales1.rTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTot6.Summary.FormatString = "{0:0.00}";

                    rep_Sales1.rTTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales1.rTTot6.Summary.FormatString = "{0:0.00}";
                }

                rep_Sales2.Report.DataSource = frepdtbl;
                GeneralFunctions.MakeReportWatermark(rep_Sales2);
                rep_Sales2.rReportHeaderCompany.Text = Settings.ReportHeader_Company;
                rep_Sales2.rReportHeader.Text = Settings.ReportHeader_Address;
                rep_Sales2.rDate.Text = GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + "  " + "to" + "  " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

                rep_Sales2.DecimalPlace = Settings.DecimalPlace;
                if (Settings.DecimalPlace == 3)
                {
                    rep_Sales2.rTTot2.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot3.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot4.Summary.FormatString = "{0:0.000}";
                    rep_Sales2.rTTot6.Summary.FormatString = "{0:0.000}";
                }
                else
                {
                    rep_Sales2.rTTot2.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot3.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot4.Summary.FormatString = "{0:0.00}";
                    rep_Sales2.rTTot6.Summary.FormatString = "{0:0.00}";
                }

                if (rg2.IsChecked == true) // Daily
                {
                    rep_Sales2.rHeader.Text = "Daily Sales";
                    rep_Sales2.rHGroup1.Text = "Day";
                    rep_Sales2.rHGroup2.Text = "";
                    rep_Sales2.rGroup2.Text = "";
                }

                if (rg3.IsChecked == true) // Weekly
                {
                    rep_Sales1.rHeader.Text = "Weekly Sales";
                    rep_Sales1.rHGroup1.Text = "Week";
                    rep_Sales1.rHGroup2.Text = "Day";
                }

                if (rg4.IsChecked == true) // Monthly
                {
                    rep_Sales1.rHeader.Text = "Monthly Sales";
                    rep_Sales1.rHGroup1.Text = "Month";
                    rep_Sales1.rHGroup2.Text = "Day";
                }

                if (rg5.IsChecked == true) // Yearly
                {
                    rep_Sales1.rHeader.Text = "Yearly Sales";
                    rep_Sales1.rHGroup1.Text = "Year";
                    rep_Sales1.rHGroup2.Text = "Month";
                }

                if (rg1.IsChecked == true) // Hourly
                {
                    rep_Sales1.rHeader.Text = "Hourly Sales";
                    rep_Sales1.rHGroup1.Text = "Day";
                    rep_Sales1.rHGroup2.Text = "Hour";
                }

                if (chkD.IsChecked == true)
                {
                    if (rg2.IsChecked == true)
                    {
                        rep_Sales2.rHeader.Text = rep_Sales2.rHeader.Text + (cmbDept.Text != "" ? " for Department : " + cmbDept.Text : " for all Departments");
                    }
                    else
                    {
                        rep_Sales1.rHeader.Text = rep_Sales1.rHeader.Text + (cmbDept.Text != "" ? " for Department : " + cmbDept.Text : " for all Departments");
                    }
                }
                else if (chkC.IsChecked == true)
                {

                    if (rg2.IsChecked == true)
                    {
                        rep_Sales2.rHeader.Text = rep_Sales2.rHeader.Text + (cmbDept.Text != "" ? " for POS Screen Category : " + cmbDept.Text : " for all POS Screen Categories");
                    }
                    else
                    {
                        rep_Sales1.rHeader.Text = rep_Sales1.rHeader.Text + (cmbDept.Text != "" ? " for POS Screen Category : " + cmbDept.Text : " for all POS Screen Categories");
                    }
                }
                else
                {

                }


                rep_Sales1.GroupHeader1.GroupFields.Add(rep_Sales1.CreateGroupField("GroupSortOrder"));
                rep_Sales1.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;

                rep_Sales1.rGroupID.DataBindings.Add("Text", frepdtbl, "Group1");

                rep_Sales1.rGroup2.DataBindings.Add("Text", frepdtbl, "Group2");

                rep_Sales1.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales1.rDCost.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_Sales1.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales1.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales1.rMargin.DataBindings.Add("Text", frepdtbl, "Margin");

                rep_Sales1.rTot5.DataBindings.Add("Text", frepdtbl, "GroupMargin");
                rep_Sales1.rTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales1.rTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales1.rTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales1.rTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");

                rep_Sales1.rTTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales1.rTTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_Sales1.rTTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales1.rTTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales1.rTTot5.DataBindings.Add("Text", frepdtbl, "TotalMargin");

                rep_Sales2.GroupHeader1.GroupFields.Add(rep_Sales1.CreateGroupField("GroupSortOrder"));
                rep_Sales2.GroupHeader1.GroupFields[0].SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending;

                rep_Sales2.rGroupID.DataBindings.Add("Text", frepdtbl, "Group1");

                rep_Sales2.rGroup2.DataBindings.Add("Text", frepdtbl, "Group2");

                rep_Sales2.rCost.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rDCost.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_Sales2.rRevenue.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rProfit.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales2.rMargin.DataBindings.Add("Text", frepdtbl, "Margin");

                rep_Sales2.rTTot2.DataBindings.Add("Text", frepdtbl, "Cost");
                rep_Sales2.rTTot6.DataBindings.Add("Text", frepdtbl, "DiscountedCost");
                rep_Sales2.rTTot3.DataBindings.Add("Text", frepdtbl, "Revenue");
                rep_Sales2.rTTot4.DataBindings.Add("Text", frepdtbl, "Profit");
                rep_Sales2.rTTot5.DataBindings.Add("Text", frepdtbl, "TotalMargin");


                if (eventtype == "Preview")
                {
                    //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        if (rg2.IsChecked == true)
                        {
                            if (Settings.ReportPrinterName != "") rep_Sales2.PrinterName = Settings.ReportPrinterName;
                            rep_Sales2.CreateDocument();
                            rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                            rep_Sales2.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_Sales2.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_Sales2;
                            window.ShowDialog();

                        }
                        else
                        {
                            if (Settings.ReportPrinterName != "") rep_Sales1.PrinterName = Settings.ReportPrinterName;
                            rep_Sales1.CreateDocument();
                            rep_Sales1.PrintingSystem.ShowMarginsWarning = false;

                            rep_Sales1.PrintingSystem.ShowPrintStatusDialog = false;

                            //rep_Sales1.ShowPreviewDialog();

                            DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                            window.PreviewControl.DocumentSource = rep_Sales1;
                            window.ShowDialog();

                        }

                    }
                    finally
                    {
                        rep_Sales1.Dispose();
                        rep_Sales2.Dispose();


                        dtbl.Dispose();
                        repdtbl.Dispose();
                        frepdtbl.Dispose();
                    }
                }


                if (eventtype == "Print")
                {
                    if (rg2.IsChecked == true)
                    {
                        rep_Sales2.CreateDocument();
                        rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_Sales2);
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            rep_Sales2.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                    else
                    {
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            GeneralFunctions.PrintReport(rep_Sales1);
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            rep_Sales2.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                }


                if (eventtype == "Email")
                {
                    if (rg2.IsChecked == true)
                    {
                        rep_Sales2.CreateDocument();
                        rep_Sales2.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            attachfile = "daily_sales.pdf";
                            GeneralFunctions.EmailReport(rep_Sales2, attachfile, "Sales");
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            rep_Sales2.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                    else
                    {
                        rep_Sales1.CreateDocument();
                        rep_Sales1.PrintingSystem.ShowMarginsWarning = false;
                        try
                        {
                            string attachfile = "";
                            if (rg3.IsChecked == true) attachfile = "weekly_sales.pdf";
                            if (rg4.IsChecked == true) attachfile = "monthly_sales.pdf";
                            if (rg5.IsChecked == true) attachfile = "yearly_sales.pdf";
                            if (rg1.IsChecked == true) attachfile = "hourly_sales.pdf";
                            GeneralFunctions.EmailReport(rep_Sales1, attachfile, "Sales");
                        }
                        finally
                        {
                            rep_Sales1.Dispose();
                            rep_Sales2.Dispose();
                            dtbl.Dispose();
                            repdtbl.Dispose();
                            frepdtbl.Dispose();
                        }
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (Settings.ReportHeader == "")
                {
                    DocMessage.MsgInformation("Enter Store Details in Settings before printing the report");
                    return;
                }

                string moreF = "";
                int moreFid = 0;

                if (chkD.IsChecked == true)
                {
                    moreF = "Department";
                    moreFid = GeneralFunctions.fnInt32(cmbDept.EditValue);
                }

                if (chkC.IsChecked == true)
                {
                    moreF = "POS Screen Category";
                    moreFid = GeneralFunctions.fnInt32(cmbCategory.EditValue);
                }

                int reportindex = 0;
                if (rg1.IsChecked == true) reportindex = 0;
                if (rg2.IsChecked == true) reportindex = 1;
                if (rg3.IsChecked == true) reportindex = 2;
                if (rg4.IsChecked == true) reportindex = 3;
                if (rg5.IsChecked == true) reportindex = 4;

                DataTable dtbl = new DataTable();
                PosDataObject.Sales objSales = new PosDataObject.Sales();
                objSales.Connection = SystemVariables.Conn;
                dtbl = objSales.FetchSaleByPeriodReportData(reportindex, moreF, moreFid, GeneralFunctions.fnDate(dtFrom.EditValue), GeneralFunctions.fnDate(dtTo.EditValue), Settings.TaxInclusive);

                if (dtbl.Rows.Count == 0)
                {
                    DocMessage.MsgInformation("No Record Found");
                    dtbl.Dispose();
                    return;
                }

                DataTable repdtbl = new DataTable();

                repdtbl.Columns.Add("Group2", System.Type.GetType("System.String"));

                repdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                repdtbl.Columns.Add("Group1", System.Type.GetType("System.String"));
                repdtbl.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));

                double dblRev = 0;
                double dblCost = 0;
                double dblPRev = 0;
                double dblPCost = 0;
                double dblProfit = 0;
                double dblMargin = 0;
                double dblPQty = 0;
                double dblRD = 0;

                int iSL = 1;

                foreach (DataRow dr in dtbl.Rows)
                {
                    bool blExists = false;
                    foreach (DataRow dr1 in repdtbl.Rows)
                    {
                        if ((dr1["Group2"].ToString() == dr["Group2"].ToString()) &&
                            (dr1["Group1"].ToString() == dr["Group1"].ToString()))
                        {
                            blExists = true;
                            break;
                        }
                    }


                    if (!blExists)
                    {
                        bool fSL = false;
                        foreach (DataRow dr2 in repdtbl.Rows)
                        {
                            if (dr2["Group1"].ToString() == dr["Group1"].ToString())
                            {
                                fSL = true;
                                break;
                            }
                        }

                        if (!fSL) iSL++;

                        dblRev = 0;
                        dblCost = 0;
                        dblRev = GeneralFunctions.fnDouble(dr["Price"].ToString());
                        dblCost = GeneralFunctions.fnDouble(dr["Cost"].ToString());


                        repdtbl.Rows.Add(new object[] {dr["Group2"].ToString(),
                                                   dblRev,
                                                   dblCost,
                                                   0,0,
                                                   dr["Group1"].ToString(),iSL});
                    }
                    else
                    {
                        foreach (DataRow dr2 in repdtbl.Rows)
                        {
                            if ((dr2["Group2"].ToString() == dr["Group2"].ToString()) &&
                                (dr2["Group1"].ToString() == dr["Group1"].ToString()))
                            {
                                dblRev = 0;
                                dblCost = 0;
                                dblRev = GeneralFunctions.fnDouble(dr["Price"].ToString());
                                dblCost = GeneralFunctions.fnDouble(dr["Cost"].ToString());

                                dblPRev = GeneralFunctions.fnDouble(dr2["Revenue"].ToString()) + dblRev;
                                dblPCost = GeneralFunctions.fnDouble(dr2["Cost"].ToString()) + dblCost;
                                dr2["Revenue"] = dblPRev;
                                dr2["Cost"] = dblPCost;
                            }
                        }
                    }
                }

                foreach (DataRow dr3 in repdtbl.Rows)
                {
                    dblProfit = 0;
                    dblMargin = 0;
                    if (GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) == 0)
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = -99999;
                    }
                    else
                    {
                        dblProfit = GeneralFunctions.fnDouble(dr3["Revenue"].ToString()) - GeneralFunctions.fnDouble(dr3["Cost"].ToString());
                        dblMargin = dblProfit * 100 / GeneralFunctions.fnDouble(dr3["Revenue"].ToString());
                    }

                    dr3["Profit"] = dblProfit;
                    dr3["Margin"] = dblMargin;
                }

                DataTable frepdtbl = new DataTable();

                frepdtbl.Columns.Add("Group2", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("Revenue", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Cost", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Profit", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Margin", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("Group1", System.Type.GetType("System.String"));
                frepdtbl.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));
                frepdtbl.Columns.Add("GroupMargin", System.Type.GetType("System.Double"));
                frepdtbl.Columns.Add("TotalMargin", System.Type.GetType("System.Double"));


                foreach (DataRow dr in repdtbl.Rows)
                {
                    frepdtbl.Rows.Add(new object[] {
                                                     dr["Group2"].ToString(),
                                                     dr["Revenue"].ToString(),
                                                     dr["Cost"].ToString(),
                                                     dr["Profit"].ToString(),
                                                     dr["Margin"].ToString(),
                                                     dr["Group1"].ToString(),
                                                     GeneralFunctions.fnInt32(dr["GroupSortOrder"].ToString()),
                                                     0,0});
                }


                DataTable dtblMargin = new DataTable();
                dtblMargin.Columns.Add("GroupSortOrder", System.Type.GetType("System.Int32"));
                dtblMargin.Columns.Add("Margin", System.Type.GetType("System.Double"));
                dtblMargin.Columns.Add("TMargin", System.Type.GetType("System.Double"));

                DataTable dClone = frepdtbl.DefaultView.ToTable(true, "GroupSortOrder");


                double cost = 0;
                double rev = 0;

                foreach (DataRow dr1 in dClone.Rows)
                {
                    double tcost = 0;
                    double trev = 0;

                    foreach (DataRow dr in frepdtbl.Rows)
                    {

                        if (dr1["GroupSortOrder"].ToString() == dr["GroupSortOrder"].ToString())
                        {

                            tcost = tcost + GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            trev = trev + GeneralFunctions.fnDouble(dr["Revenue"].ToString());
                        }
                    }

                    cost = cost + tcost;
                    rev = rev + trev;

                    dblProfit = 0;
                    dblMargin = 0;

                    if (trev == 0)
                    {
                        dblProfit = trev - tcost;
                        dblMargin = -99999;
                    }
                    else
                    {
                        dblProfit = trev - tcost;
                        dblMargin = dblProfit * 100 / trev;
                    }

                    dtblMargin.Rows.Add(new object[] { dr1["GroupSortOrder"].ToString(), dblMargin, 0 });
                }

                dblProfit = 0;
                dblMargin = 0;

                if (rev == 0)
                {
                    dblProfit = rev - cost;
                    dblMargin = -99999;
                }
                else
                {
                    dblProfit = rev - cost;
                    dblMargin = dblProfit * 100 / rev;
                }


                foreach (DataRow dr in dtblMargin.Rows)
                {
                    dr["TMargin"] = dblMargin;
                }

                foreach (DataRow dr in dtblMargin.Rows)
                {
                    foreach (DataRow dr1 in frepdtbl.Rows)
                    {
                        if (dr1["GroupSortOrder"].ToString() == dr["GroupSortOrder"].ToString())
                        {
                            dr1["GroupMargin"] = GeneralFunctions.fnDouble(dr["Margin"].ToString());
                            dr1["TotalMargin"] = GeneralFunctions.fnDouble(dr["TMargin"].ToString());
                        }
                    }
                }


                DataTable dClone2 = frepdtbl.DefaultView.ToTable(true, "Group1");

                DataTable dtblGraph = new DataTable();
                dtblGraph.Columns.Add("Argument", System.Type.GetType("System.String"));
                dtblGraph.Columns.Add("Value", System.Type.GetType("System.String"));


                cost = 0;
                rev = 0;

                int GG = 0;

                foreach (DataRow dr1 in dClone2.Rows)
                {
                    double tcost = 0;
                    double trev = 0;

                    foreach (DataRow dr in frepdtbl.Rows)
                    {

                        if (dr1["Group1"].ToString() == dr["Group1"].ToString())
                        {
                            tcost = tcost + GeneralFunctions.fnDouble(dr["Cost"].ToString());
                            trev = trev + GeneralFunctions.fnDouble(dr["Revenue"].ToString());
                        }
                    }

                    cost = cost + tcost;
                    rev = rev + trev;

                    GG++;
                    dtblGraph.Rows.Add(new object[] { dr1["Group1"].ToString() + " ", trev });
                }


                frm_Graph fGraph = new frm_Graph();
                try
                {
                    fGraph.GraphFor = "Sales By Period";
                    if (rg1.IsChecked == true) fGraph.GraphParameter1 = "Hourly";
                    if (rg2.IsChecked == true) fGraph.GraphParameter1 = "Daily";
                    if (rg3.IsChecked == true) fGraph.GraphParameter1 = "Weekly";
                    if (rg4.IsChecked == true) fGraph.GraphParameter1 = "Monthly";
                    if (rg5.IsChecked == true) fGraph.GraphParameter1 = "Yearly";


                    if (chkD.IsChecked == true)
                    {
                        fGraph.GraphParameter2 = cmbDept.EditValue != null ? " for Department : " + cmbDept.EditText : " for All Departments";
                    }
                    else if (chkC.IsChecked == true)
                    {
                        fGraph.GraphParameter2 = cmbCategory.EditValue != null ? " for POS Screen Category : " + cmbCategory.EditText : " for All POS Screen Categories";
                    }
                    else
                    {
                        fGraph.GraphParameter2 = "";
                    }

                    fGraph.GraphParameter3 = "from" + " " + GeneralFunctions.fnDate(dtFrom.EditValue).ToString("d") + " " + "to" + " " + GeneralFunctions.fnDate(dtTo.EditValue).ToString("d");

                    fGraph.ChartData = dtblGraph;
                    fGraph.ShowDialog();
                }
                finally
                {
                    fGraph.Close();
                }

            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

        }

        private void CmbDept_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
